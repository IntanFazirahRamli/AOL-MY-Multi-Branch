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

Partial Class CNProgressBilling
    Inherits System.Web.UI.Page
    Public rcno As String
    Private Shared GridSelected As String = String.Empty
    Private Shared gScheduler, gSalesman As String
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
    Public sqltext As String

    Public lGLCode As String
    Public lGLDescription As String
    Public lCreditAmount As Decimal
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim Query As String

        'Restrict users manual date entries

        'txtAccountIdBilling.Attributes.Add("readonly", "readonly")
        txtAccountName.Attributes.Add("readonly", "readonly")
        'txtBillAddress.Attributes.Add("readonly", "readonly")
        'txtBillBuilding.Attributes.Add("readonly", "readonly")
        'txtBillStreet.Attributes.Add("readonly", "readonly")
        'txtBillCountry.Attributes.Add("readonly", "readonly")
        'txtBillPostal.Attributes.Add("readonly", "readonly")
        'txtTotal.Attributes.Add("readonly", "readonly")
        'txtTaxRatePct.Attributes.Add("readonly", "readonly")


        txtReceiptPeriod.Attributes.Add("readonly", "readonly")
        txtCompanyGroup.Attributes.Add("readonly", "readonly")
        ddlContactType.Attributes.Add("readonly", "readonly")
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

        If Not Page.IsPostBack Then
            mdlPopUpClient.Hide()

            '    Dim conn As MySqlConnection = New MySqlConnection()

            '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    conn.Open()

            '    Dim sql As String
            '    sql = ""
            '    sql = "Select TaxRatePct from tbltaxtype where TaxType = 'SR'"

            '    Dim command1 As MySqlCommand = New MySqlCommand
            '    command1.CommandType = CommandType.Text
            '    command1.CommandText = sql
            '    command1.Connection = conn

            '    Dim dr As MySqlDataReader = command1.ExecuteReader()

            '    Dim dt As New DataTable
            '    dt.Load(dr)

            '    If dt.Rows.Count > 0 Then
            '        If dt.Rows(0)("TaxRatePct").ToString <> "" Then : txtTaxRatePct.Text = dt.Rows(0)("TaxRatePct").ToString : End If
            '    End If

            '    conn.Close()

            MakeMeNull()
            DisableControls()
            'Dim Query As String

            'Query = "Select CompanyGroup from tblCompanyGroup"
            'PopulateDropDownList(Query, "CompanyGroup", "CompanyGroup", txtCompanyGroup)
            ''PopulateDropDownList(Query, "CompanyGroup", "CompanyGroup", ddlSearchCompanyGroup)

            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'Dim sql As String
            'sql = ""

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

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
            'If Session("receiptfrom") = "invoice" Then
            '    txtInvoiceNoSearch.Text = Session("invoiceno")
            '    sqltext = "SELECT A.PostStatus, A.BankStatus, A.GlStatus, A.CNNumber, A.CNDate, A.AccountId, A.AppliedBase, A.GSTAmount, A.BaseAmount, A.CustomerName,  A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno FROM tblCN A, tblCNdet B where A.CNNumber = B.CNNumber and B.InvoiceNo = '" & txtReceiptnoSearch.Text & "' ORDER BY Rcno DESC, CustomerName"
            '    SQLDSCN.SelectCommand = sqltext
            '    btnBack.Visible = True
            '    btnQuit.Visible = False
            'Else
            '    sqltext = "SELECT PostStatus, BankStatus, GlStatus, CNNumber, CNDate, AccountId, AppliedBase, GSTAmount, BaseAmount, CustomerName,  NetAmount, GlPeriod, CompanyGroup, ContactType, Cheque, ChequeDate, BankId,  LedgerCode, LedgerName, Comments, PaymentType, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno FROM tblCN ORDER BY Rcno DESC, CustomerName"
            '    SQLDSCN.SelectCommand = sqltext
            'End If

            'SQLDSCN.DataBind()
            'GridView1.DataBind()

            'updPnlBillingRecs.Update()
        Else
            If txtIsPopup.Text = "Team" Then
                txtIsPopup.Text = "N"
                'mdlPopUpTeam.Show()
            ElseIf txtIsPopup.Text = "Client" Then
                txtIsPopup.Text = "N"
                mdlPopUpClient.Show()
            End If
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ''Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        ''Dim objTextInfo As TextInfo = cultureInfo.TextInfo

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
            txtRcno.Text = GridView1.SelectedRow.Cells(26).Text.Trim

            If (GridView1.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtPostStatus.Text = ""
            Else
                txtPostStatus.Text = GridView1.SelectedRow.Cells(2).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(4).Text = "&nbsp;") Then
                txtCNNo.Text = ""
            Else
                txtCNNo.Text = GridView1.SelectedRow.Cells(4).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(5).Text = "&nbsp;") Then
                txtCNDate.Text = ""
            Else
                txtCNDate.Text = GridView1.SelectedRow.Cells(5).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(6).Text = "&nbsp;") Then
                txtReceiptPeriod.Text = ""
            Else
                txtReceiptPeriod.Text = GridView1.SelectedRow.Cells(6).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(7).Text = "&nbsp;") Then
                txtCompanyGroup.Text = ""
            Else
                txtCompanyGroup.Text = GridView1.SelectedRow.Cells(7).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(8).Text = "&nbsp;") Then
                txtAccountIdBilling.Text = ""
            Else
                txtAccountIdBilling.Text = GridView1.SelectedRow.Cells(8).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(9).Text = "&nbsp;") Then
                ddlContactType.Text = ""
            Else
                ddlContactType.Text = GridView1.SelectedRow.Cells(9).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(10).Text = "&nbsp;") Then
                txtAccountName.Text = ""
            Else
                txtAccountName.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(10).Text.Trim)
            End If

            If (GridView1.SelectedRow.Cells(11).Text = "&nbsp;") Then
                txtCNAmount.Text = ""
            Else
                txtCNAmount.Text = GridView1.SelectedRow.Cells(11).Text.Trim
            End If


            If (GridView1.SelectedRow.Cells(18).Text = "&nbsp;") Then
                txtComments.Text = ""
            Else
                txtComments.Text = GridView1.SelectedRow.Cells(18).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(19).Text = "&nbsp;") Then
                ddlContractNo.Text = ""
            Else
                ddlContractNo.Text = GridView1.SelectedRow.Cells(19).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(20).Text = "&nbsp;") Then
                ddlItemCode.Text = ""
            Else
                ddlItemCode.Text = GridView1.SelectedRow.Cells(20).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(21).Text = "&nbsp;") Then
                txtARDescription10.Text = ""
            Else
                txtARDescription10.Text = GridView1.SelectedRow.Cells(21).Text.Trim
            End If



            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'Dim sql As String
            'sql = ""

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

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


            ''Dim sql As String
            'sql = ""
            'sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode = 'CN-RET' and CompanyGroup = '" & txtCompanyGroup.Text & "'"

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

            'txtRcnoServiceRecord.Text = lblid14.Text
            'txtRcnoServiceRecordDetail.Text = lblid15.Text
            'txtContractNo.Text = lblid16.Text
            'txtRcnoInvoice.Text = lblid17.Text
            'txtRowSelected.Text = rowindex1.ToString
            'txtRcnoservicebillingdetail.Text = lblid18.Text

            'PopulateServiceGrid()

            'updpnlBillingDetails.Update()

            updPanelSave.Update()
            updPnlBillingRecs.Update()

            If txtPostStatus.Text = "P" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Black
                btnCopy.Enabled = True
                btnChangeStatus.Enabled = True
                btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Black
                btnPrint.Enabled = True
                btnPost.Enabled = False
                'btnDelete.Enabled = True

            Else
                btnEdit.Enabled = True
                btnEdit.ForeColor = System.Drawing.Color.Black
                btnCopy.Enabled = True
                btnChangeStatus.Enabled = True
                btnDelete.Enabled = True
                btnDelete.ForeColor = System.Drawing.Color.Black
                btnPrint.Enabled = True
                btnPost.Enabled = True
                'btnDelete.Enabled = True
            End If

            updPnlMsg.Update()


            PopulateArCode()
            DisplayGLGrid()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
            sql = "Select COACode, Description from tblchartofaccounts where GLtype='TRADE DEBTOR'"

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
            sql = "Select COACode, Description from tblchartofaccounts where  GLtype='OTHER DEBTORS'"
            Dim command122 As MySqlCommand = New MySqlCommand
            command122.CommandType = CommandType.Text
            command122.CommandText = sql
            command122.Connection = conn

            Dim dr22 As MySqlDataReader = command122.ExecuteReader()

            Dim dt22 As New DataTable
            dt22.Load(dr22)

            If dt22.Rows.Count > 0 Then
                If dt22.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt22.Rows(0)("COACode").ToString : End If
                If dt22.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt22.Rows(0)("Description").ToString : End If
            End If


            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()


            ''Dim sql As String
            'sql = ""
            ''sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode = 'CN-RET' and CompanyGroup = '" & txtCompanyGroup.Text & "'"
            'sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode = 'CN-RET' "
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
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub
    'Function

    Private Sub GenerateCNNo()
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
            If Date.TryParseExact(txtCNDate.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
                strdate = d.ToShortDateString
            End If

            lPrefix = Format(CDate(strdate), "yyyyMM")
            lInvoiceNo = "ARCN" + lPrefix + "-"
            lMonth = Right(lPrefix, 2)
            lYear = Left(lPrefix, 4)

            lPrefix = "ARCN" + lYear
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
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub
    Public Sub MakeMeNull()

        Try

            lblMessage.Text = ""
            lblAlert.Text = ""
            txtMode.Text = ""
            txtSearch1Status.Text = "O"
            txtMode.Text = "NEW"

            btnEdit.Enabled = False
            btnCopy.Enabled = False
            btnChangeStatus.Enabled = False
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
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub


    Private Sub DisableControls()
        'btnSave.Enabled = False
        'btnSave.ForeColor = System.Drawing.Color.Gray
        'btnCancel.Enabled = False
        'btnCancel.ForeColor = System.Drawing.Color.Gray
        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black
        'btnClient.Visible = False
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black

        txtCNNo.Enabled = False
        txtCNDate.Enabled = False
        txtReceiptPeriod.Enabled = False
        txtCompanyGroup.Enabled = False
        txtAccountIdBilling.Enabled = False
        ddlContactType.Enabled = False
        txtAccountName.Enabled = False

        txtCNAmount.Enabled = False
        ddlContractNo.Enabled = False
        ddlItemCode.Enabled = False
        txtARDescription10.Enabled = False

        'txtBankGLCode.Enabled = False

        'ddlBankCode.Enabled = True
        'txtBankGLCode.Enabled = True
        'ddlPaymentMode.Enabled = True
        'txtChequeNo.Enabled = True
        'txtChequeDate.Enabled = True

        txtComments.Enabled = False
        btnShowInvoices.Enabled = False
        btnSave.Enabled = False
        'btnShowRecords.Enabled = False

        grvBillingDetails.Enabled = False

        btnDelete.Enabled = False
        btnClient.Visible = False
    End Sub

    Private Sub EnableControls()
        'btnSave.Enabled = True
        'btnSave.ForeColor = System.Drawing.Color.Black
        'btnCancel.Enabled = True
        'btnCancel.ForeColor = System.Drawing.Color.Black
        'btnClient.Visible = True
        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black

        'btnDelete.Enabled = False
        'btnDelete.ForeColor = System.Drawing.Color.Gray

        'rdbAll.Attributes.Remove("disabled")
        'rdbCompleted.Attributes.Add("readonly", "readonly")
        'rdbNotCompleted.Attributes.Add("readonly", "readonly")

        'rdbAll.Enabled = True
        'rdbCompleted.Enabled = True
        'rdbNotCompleted.Enabled = True

        'txtCNNo.Enabled = True
        txtCNDate.Enabled = True
        txtReceiptPeriod.Enabled = True
        txtCompanyGroup.Enabled = True
        'txtAccountId.Enabled = True
        'ddlContactType.Enabled = True
        'txtAccountIdBilling.Enabled = True
        txtAccountName.Enabled = True

        'txtCNAmount.Enabled = True
        'ddlContractNo.Enabled = True
        'ddlItemCode.Enabled = True
        txtARDescription10.Enabled = True
      


        'ddlBankCode.Enabled = True
        'txtBankGLCode.Enabled = True
        'ddlPaymentMode.Enabled = True

        'txtChequeNo.Enabled = True
        'txtChequeDate.Enabled = True

        txtComments.Enabled = True
        btnShowInvoices.Enabled = True
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
        btnClient.Visible = True
        grvBillingDetails.Enabled = True
        'grvServiceRecDetails.Enabled = True
        updPnlBillingRecs.Update()
        'updpnlServiceRecs.Update()
        updpnlBillingDetails.Update()
        updPanelSave.Update()
        'updPanelSearchServiceRec.Update()
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

            'DisableControls()
            EnableControls()

            txtMode.Text = "NEW"
            lblMessage.Text = "ACTION: ADD RECORD"
            txtReceiptPeriod.Text = Year(Convert.ToDateTime(txtCNDate.Text)) & Format(Month(Convert.ToDateTime(txtCNDate.Text)), "00")

        Catch ex As Exception
            lblAlert.Text = ex.Message
            'MsgBox("Error" & ex.Message)
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
        End Using
        'End Using
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



                Dim command3 As MySqlCommand = New MySqlCommand
                command3.CommandType = CommandType.Text

                Dim qry3 As String = "DELETE from tblCNDet where ReceiptNumber= @ReceiptNumber "

                command3.CommandText = qry3
                command3.Parameters.Clear()

                command3.Parameters.AddWithValue("@ReceiptNumber", txtCNNo.Text)
                command3.Connection = conn
                command3.ExecuteNonQuery()


                conn.Close()

                'Dim message As String = "alert('Contract is deleted Successfully!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                lblMessage.Text = "DELETE: CREDIT NOTE SUCCESSFULLY DELETED"

                'btnADD_Click(sender, e)

                'txt.Text = "SELECT * From tblContract where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"
                SQLDSCN.SelectCommand = txt.Text
                SQLDSCN.DataBind()
                'GridView1.DataSourceID = "SqlDSContract"

                MakeMeNull()
                GridView1.DataBind()
                updPnlMsg.Update()
                updPnlSearch.Update()
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
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""


            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If

            Dim confirmValue As String
            confirmValue = ""
            Dim conn As MySqlConnection = New MySqlConnection()

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then
                ''''''''''''''' Insert tblAR



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
                '    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName,  "
                '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName,"
                '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                '    commandAR.CommandText = qryAR
                '    commandAR.Parameters.Clear()
                '    commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    If txtCNDate.Text.Trim = "" Then
                '        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    Else
                '        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '    End If
                '    commandAR.Parameters.AddWithValue("@ContractNo", "")
                '    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
                '    commandAR.Parameters.AddWithValue("@GLCode", txtARCode10.Text)
                '    commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription10.Text)
                '    commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)
                '    commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtCNAmount.Text))
                '    commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                '    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '    commandAR.Connection = conn
                '    commandAR.ExecuteNonQuery()

                '    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, GLtype, "
                '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @GLtype,"
                '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                '    commandAR.CommandText = qryAR
                '    commandAR.Parameters.Clear()
                '    commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    If txtCNDate.Text.Trim = "" Then
                '        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    Else
                '        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '    End If
                '    commandAR.Parameters.AddWithValue("@ContractNo", "")
                '    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
                '    commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
                '    commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
                '    commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtCNAmount.Text))
                '    commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
                '    commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    commandAR.Parameters.AddWithValue("@ModuleName", "CN")
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


                Dim commandUpdateCN As MySqlCommand = New MySqlCommand
                commandUpdateCN.CommandType = CommandType.Text
                Dim sqlUpdateCN As String = "Update tblCN set GLStatus = 'O'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

                commandUpdateCN.CommandText = sqlUpdateCN
                commandUpdateCN.Parameters.Clear()
                commandUpdateCN.Connection = conn
                commandUpdateCN.ExecuteNonQuery()

                GridView1.DataBind()

                lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"
                lblAlert.Text = ""
                updPnlSearch.Update()
                updPnlMsg.Update()
                updpnlBillingDetails.Update()
                'updpnlServiceRecs.Update()
                updpnlBillingDetails.Update()
            End If

            ''''''''''''''' Insert tblAR


        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
        End Try
    End Sub


    ''''''' Start: Service Details



    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        'Session("serviceschedulefrom") = ""
        'Session("contractno") = ""
        'Session("contractdetailfrom") = ""

        'txt.Text = "SELECT * From tblCN where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"

        txtAccountIdSearch.Text = ""
        txtBillingPeriodSearch.Text = ""
        'txtInvoiceNoSearch.Text = ""
        txtClientNameSearch.Text = ""
        'txtBillingPeriodSearch.Text = ""
        'ddlSalesmanSearch.SelectedIndex = 0
        txtSearch1Status.Text = "O"
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

        If txtRcno.Text = "" Then
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return
        End If

        lblMessage.Text = "ACTION: EDIT RECORD"

        txtMode.Text = "EDIT"
        EnableControls()

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
        Try
            Dim strsql As String

            'PostStatus, BankStatus, GlStatus, CNNumber, CNDate, AccountId, AppliedBase, GSTAmount, BaseAmount, CustomerName, NetAmount, GlPeriod, CompanyGroup, ContactType, Cheque, ChequeDate, BankId, LedgerCode, LedgerName, Comments, PaymentType,

            'If Session("receiptfrom") = "invoice" Then
            'txtReceiptnoSearch.Text = Session("invoiceno")


            'SELECT PostStatus, BankStatus, GlStatus, CNNumber, CNDate, AccountId, AppliedBase, GSTAmount, BaseAmount, CustomerName, NetAmount, GlPeriod, CompanyGroup, ContactType, Cheque, ChequeDate, BankId, LedgerCode, LedgerName, Comments, PaymentType, ItemCode, ItemDescription, ContractNo,CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno FROM tblcn where GLStatus = 'O' and ModuleType='CN' ORDER BY Rcno DESC, CustomerName


            strsql = "SELECT A.PostStatus, A.BankStatus, A.GlStatus, A.CNNumber, A.CNDate, A.AccountId, A.AppliedBase, A.GSTAmount, A.BaseAmount, A.CustomerName,  A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.ItemCode, A.ItemDescription, A.ContractNo, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno FROM tblcn A  where  A.GLStatus = 'O' and A.ModuleType='CNPB'  "
            'SQLDSReceipt.SelectCommand = sqltext
            'Else
            'sqltext = "SELECT PostStatus, BankStatus, GlStatus, ReceiptNumber, ReceiptDate, AccountId, AppliedBase, GSTAmount, BaseAmount, ReceiptFrom, ReceiptDate, NetAmount, GlPeriod, CompanyGroup, ContactType, Cheque, ChequeDate, BankId,  LedgerCode, LedgerName, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno FROM tblrecv ORDER BY Rcno DESC, ReceiptFrom"
            'SQLDSReceipt.SelectCommand = sqltext
            'End If


            If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
                Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                For Each str As String In stringList
                    str = "'" + str + "'"
                    YrStrList.Add(str.ToUpper)
                Next

                Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                strsql = strsql + " and A.GLStatus in (" + YrStr + ")"

            End If


            If String.IsNullOrEmpty(txtBillingPeriodSearch.Text) = False Then
                strsql = strsql & " and A.GLPeriod like '%" & txtBillingPeriodSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtReceiptnoSearch.Text) = False Then
                strsql = strsql & " and A.CNNumber like '%" & txtReceiptnoSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtAccountIdSearch.Text) = False Then
                strsql = strsql & " and (A.AccountId like '%" & txtAccountIdSearch.Text & "%' or A.AccountId like '%" & txtAccountIdSearch.Text & "%')"
            End If

            If String.IsNullOrEmpty(txtClientNameSearch.Text) = False Then
                strsql = strsql & " and A.CustomerName like '%" & txtClientNameSearch.Text & "%'"
            End If

            'If String.IsNullOrEmpty(txtInvoiceNoSearch.Text) = False Then
            '    strsql = strsql & " and B.InvoiceNo ='" & txtInvoiceNoSearch.Text.Trim + "'"
            'End If

            'If (ddlSalesmanSearch.SelectedIndex > 0) Then
            '    strsql = strsql & " and Salesman like '%" & ddlSalesmanSearch.Text.Trim + "%'"
            'End If

            strsql = strsql + " order by A.CustomerName;"
            txt.Text = strsql
            txtComments.Text = strsql

            SQLDSCN.SelectCommand = strsql
            SQLDSCN.DataBind()
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

                sql = "Select A.Rcno, A.InvoiceNo, A.InvoicePrice, A.InvoiceGST, A.InvoiceValue,"
                sql = sql + "  A.CNValue, A.InvoiceDate "
                sql = sql + " FROM tblcndet A "
                sql = sql + " where 1 = 1 "
                sql = sql + " and CNNumber = '" & txtCNNo.Text & "'"

            Else
                If ddlContactType.SelectedIndex = 0 Then
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, "
                    sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, B.Address1, "
                    sql = sql + " B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                    sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, A.ContactType, A.CompanyGroup  "
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

            Total = 0.0
            TotalWithGST = 0.0
            TotalDiscAmt = 0.0
            TotalGSTAmt = 0.0
            TotalPriceWithDiscountAmt = 0.0
            TotalReceiptAmt = 0.0

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

                    Dim TextBoxTotalInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    TextBoxTotalInvoiceNo.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceNo"))

                    Dim TextBoxTotalInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
                    TextBoxTotalInvoiceDate.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceDate"))

                    'Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                    'TextBoxTotalPriceWithDisc.Text = Convert.ToString(dt.Rows(rowIndex)("InvoicePrice"))

                    'Dim TextBoxTotalInvoiceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    'TextBoxTotalInvoiceValue.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceGST"))


                    Dim TextBoxTotalTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    TextBoxTotalTotalPriceWithGST.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceValue"))


                    'Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
                    'TextBoxTotalReceiptAmt.Text = Convert.ToString(dt.Rows(rowIndex)("ReceiptValue"))


                    '''''''''''''''''''''''''''''''''''''''
                    sql = "Select sum(TotalReceiptAmount) as Totalreceipt from tblsales where CompanyGroup = '" & txtCompanyGroup.Text & "' and InvoiceNumber='" & Convert.ToString(dt.Rows(rowIndex)("InvoiceNo")) & "'"

                    Dim command23 As MySqlCommand = New MySqlCommand
                    command23.CommandType = CommandType.Text
                    command23.CommandText = sql
                    command23.Connection = conn

                    Dim dr23 As MySqlDataReader = command23.ExecuteReader()

                    Dim dt23 As New DataTable
                    dt23.Load(dr23)

                    'If dt23.Rows.Count > 0 Then
                    '    If dt23.Rows(0)("COACode").ToString <> "" Then : txtGSTOutputCode.Text = dt23.Rows(0)("COACode").ToString : End If
                    '    If dt23.Rows(0)("Description").ToString <> "" Then : txtGSTOutputDescription.Text = dt23.Rows(0)("Description").ToString : End If
                    'End If

                    ''''''''''''''''''''''''''''''''''''''''''

                    Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
                    TextBoxTotalReceiptAmt.Text = Convert.ToString(dt23.Rows(0)("Totalreceipt"))



                    '''''''''''''''''''''''''''''''''''''''
                    sql = "Select sum(TotalCNAmount) as TotalCN from tblsales where CompanyGroup = '" & txtCompanyGroup.Text & "' and InvoiceNumber='" & Convert.ToString(dt.Rows(rowIndex)("InvoiceNo")) & "'"

                    Dim command24 As MySqlCommand = New MySqlCommand
                    command24.CommandType = CommandType.Text
                    command24.CommandText = sql
                    command24.Connection = conn

                    Dim dr24 As MySqlDataReader = command24.ExecuteReader()

                    Dim dt24 As New DataTable
                    dt24.Load(dr24)

                    'If dt23.Rows.Count > 0 Then
                    '    If dt23.Rows(0)("COACode").ToString <> "" Then : txtGSTOutputCode.Text = dt23.Rows(0)("COACode").ToString : End If
                    '    If dt23.Rows(0)("Description").ToString <> "" Then : txtGSTOutputDescription.Text = dt23.Rows(0)("Description").ToString : End If
                    'End If

                    ''''''''''''''''''''''''''''''''''''''''''
                    Dim totalCNAmount As Decimal
                    totalCNAmount = 0.0
                    If String.IsNullOrEmpty(dt24.Rows(0)("TotalCN").ToString) = True Then
                        totalCNAmount = 0.0
                    Else
                        totalCNAmount = Convert.ToDecimal(dt24.Rows(0)("TotalCN").ToString)
                    End If

                    Dim TextBoxTotalCNAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
                    TextBoxTotalCNAmt.Text = (Convert.ToString(totalCNAmount))


                    Dim TextBoxTotalReceipt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
                    TextBoxTotalReceipt.Text = Convert.ToString(dt.Rows(rowIndex)("CNValue"))


                    ''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
                    'TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalTotalPriceWithGST.Text)
                    'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
                    ''txtAmountWithDiscount.Text = Total - TotalDiscAmt
                    'TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxTotalInvoiceValue.Text)
                    'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text)
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

            'txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
            'txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
            'txtTotalWithGST.Text = TotalWithGST.ToString("N2")
            txtReceiptAmt.Text = TotalReceiptAmt.ToString("N2")


            Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

            'If txtMode.Text = "VIEW" Then

            '    If (table.Rows.Count > 0) Then
            '        For i As Integer = 0 To (table.Rows.Count) - 1
            '            Dim TextBoxSel As CheckBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("chkSelectGV"), CheckBox)
            '            TextBoxSel.Enabled = False
            '            TextBoxSel.Checked = True
            '        Next i
            '    End If
            'End If
            'End: Service Recods


        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub
   

    'Start: Billing Details Grid


    Private Sub FirstGridViewRowBillingDetailsRecs()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing
            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add(New DataColumn("SelRec", GetType(String)))
            dt.Columns.Add(New DataColumn("InvoiceNo", GetType(String)))
            dt.Columns.Add(New DataColumn("InvoiceDate", GetType(String)))

            'dt.Columns.Add(New DataColumn("Location", GetType(String)))
            'dt.Columns.Add(New DataColumn("ItemType", GetType(String)))
            'dt.Columns.Add(New DataColumn("ItemCode", GetType(String)))
            'dt.Columns.Add(New DataColumn("ItemDescription", GetType(String)))
            'dt.Columns.Add(New DataColumn("UOM", GetType(String)))
            'dt.Columns.Add(New DataColumn("Qty", GetType(String)))
            'dt.Columns.Add(New DataColumn("PriceWithDisc", GetType(String)))
            'dt.Columns.Add(New DataColumn("TaxType", GetType(String)))
            'dt.Columns.Add(New DataColumn("GSTPerc", GetType(String)))
            'dt.Columns.Add(New DataColumn("GSTAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalPriceWithGST", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalReceiptAmt", GetType(String)))
            'dt.Columns.Add(New DataColumn("TotalCrediteNoteAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("ReceiptAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoReceipt", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoInvoice", GetType(String)))
            dt.Columns.Add(New DataColumn("ARCode", GetType(String)))
            dt.Columns.Add(New DataColumn("GSTCode", GetType(String)))
            'dt.Columns.Add(New DataColumn("OtherCode", GetType(String)))

            dr = dt.NewRow()

            dr("SelRec") = String.Empty
            dr("InvoiceNo") = String.Empty
            dr("InvoiceDate") = String.Empty
            'dr("ItemType") = String.Empty
            'dr("ItemCode") = String.Empty
            'dr("ItemDescription") = String.Empty
            'dr("UOM") = String.Empty
            'dr("Qty") = 0
            'dr("PriceWithDisc") = 0
            'dr("TaxType") = String.Empty
            'dr("GSTPerc") = 0.0
            'dr("GSTAmt") = 0
            dr("TotalPriceWithGST") = 0
            dr("TotalReceiptAmt") = 0
            'dr("TotalCrediteNoteAmt") = 0
            dr("ReceiptAmt") = 0
            dr("RcnoReceipt") = 0
            dr("RcnoInvoice") = 0
            dr("ARCode") = String.Empty
            dr("GSTCode") = 0
            'dr("OtherCode") = 0

            dt.Rows.Add(dr)

            ViewState("CurrentTableBillingDetailsRec") = dt

            grvBillingDetails.DataSource = dt
            grvBillingDetails.DataBind()

            Dim btnAdd As Button = CType(grvBillingDetails.FooterRow.Cells(1).FindControl("btnAddDetail"), Button)
            Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            Throw ex
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
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtInvoiceDateGV"), TextBox)
                        'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtItemTypeGV"), DropDownList)
                        'Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtItemCodeGV"), TextBox)
                        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtItemDescriptionGV"), TextBox)
                        'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtQtyGV"), TextBox)
                        'Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtPriceWithDiscGV"), TextBox)
                        'Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtTaxTypeGV"), DropDownList)
                        'Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtGSTPercGV"), TextBox)
                        'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtTotalReceiptAmtGV"), TextBox)
                        'Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalCreditNoteAmtGV"), TextBox)

                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxRcnoReceipt = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtRcnoReceiptGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxARCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtARCodeGV"), TextBox)
                        Dim TextBoGSTCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGSTCodeGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()

                        'Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGSTPercGV"), TextBox)
                        'TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N4")

                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceDate") = TextBoxInvoiceDate.Text
                        'dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        'dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        'dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        'dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        'dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text                 
                        'dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
                        'dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        'dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        'dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text

                        dtCurrentTable.Rows(i - 1)("TotalReceiptAmt") = TextBoxTotalReceiptAmt.Text
                        'dtCurrentTable.Rows(i - 1)("TotalCrediteNoteAmt") = TextBoxBalanceAmt.Text

                        dtCurrentTable.Rows(i - 1)("ReceiptAmt") = TextBoxReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoReceipt") = TextBoxRcnoReceipt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ARCode") = TextBoxARCode.Text
                        dtCurrentTable.Rows(i - 1)("GSTCode") = TextBoGSTCode.Text
                        'dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text

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

                        Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtInvoiceDateGV"), TextBox)
                        'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtItemTypeGV"), DropDownList)
                        'Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtItemCodeGV"), TextBox)
                        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtItemDescriptionGV"), TextBox)
                        'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtQtyGV"), TextBox)
                        'Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtPriceWithDiscGV"), TextBox)
                        'Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtTaxTypeGV"), DropDownList)
                        'Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtGSTPercGV"), TextBox)
                        'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtTotalReceiptAmtGV"), TextBox)
                        'Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalCreditNoteAmtGV"), TextBox)

                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxRcnoReceipt = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtRcnoReceiptGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxARCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtARCodeGV"), TextBox)
                        Dim TextBoGSTCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGSTCodeGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceDate") = TextBoxInvoiceDate.Text
                        'dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        'dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        'dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        'dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        'dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        'dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
                        'dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        'dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        'dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text
                        dtCurrentTable.Rows(i - 1)("TotalReceiptAmt") = TextBoxTotalReceiptAmt.Text
                        'dtCurrentTable.Rows(i - 1)("TotalCrediteNoteAmt") = TextBoxBalanceAmt.Text

                        dtCurrentTable.Rows(i - 1)("ReceiptAmt") = TextBoxReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoReceipt") = TextBoxRcnoReceipt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ARCode") = TextBoxARCode.Text
                        dtCurrentTable.Rows(i - 1)("GSTCode") = TextBoGSTCode.Text
                        'dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text

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

                        Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtInvoiceDateGV"), TextBox)
                        'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtItemTypeGV"), DropDownList)
                        'Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtItemCodeGV"), TextBox)
                        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtItemDescriptionGV"), TextBox)
                        'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtPriceWithDiscGV"), TextBox)
                        'Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtTaxTypeGV"), DropDownList)
                        'Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtTotalReceiptAmtGV"), TextBox)
                        'Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalCreditNoteAmtGV"), TextBox)


                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxRcnoReceipt = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtRcnoReceiptGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxARCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtARCodeGV"), TextBox)
                        Dim TextBoGSTCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGSTCodeGV"), TextBox)

                        TextBoxSelect.Text = dt.Rows(i)("SelRec").ToString()
                        TextBoxInvoiceNo.Text = dt.Rows(i)("InvoiceNo").ToString()
                        TextBoxInvoiceDate.Text = dt.Rows(i)("InvoiceDate").ToString()
                        'TextBoxItemType.Text = dt.Rows(i)("ItemType").ToString()
                        'TextBoxItemCode.Text = dt.Rows(i)("ItemCode").ToString()
                        'TextBoxItemDescription.Text = dt.Rows(i)("ItemDescription").ToString()
                        'TextBoxUOM.Text = dt.Rows(i)("UOM").ToString()
                        'TextBoxQty.Text = dt.Rows(i)("Qty").ToString()

                        'TextBoxPriceWithDisc.Text = dt.Rows(i)("PriceWithDisc").ToString()
                        'TextBoxTaxType.Text = dt.Rows(i)("TaxType").ToString()
                        'TextBoxGSTPerc.Text = dt.Rows(i)("GSTPerc").ToString()
                        'TextBoxGSTAmt.Text = dt.Rows(i)("GSTAmt").ToString()
                        TextBoxTotalPriceWithGST.Text = dt.Rows(i)("TotalPriceWithGST").ToString()

                        TextBoxTotalReceiptAmt.Text = dt.Rows(i)("TotalReceiptAmt").ToString()
                        'TextBoxBalanceAmt.Text = dt.Rows(i)("TotalCrediteNoteAmt").ToString().ToString()

                        'dtCurrentTable.Rows(i - 1)("ReceiptAmt") = TextBoxReceiptAmt.Text

                        TextBoxReceiptAmt.Text = dt.Rows(i)("ReceiptAmt").ToString()
                        TextBoxRcnoReceipt.Text = dt.Rows(i)("RcnoReceipt").ToString()
                        TextBoxRcnoInvoice.Text = dt.Rows(i)("RcnoInvoice").ToString()
                        TextBoxARCode.Text = dt.Rows(i)("ARCode").ToString()
                        TextBoGSTCode.Text = dt.Rows(i)("GSTCode").ToString()
                        'TextBoxOtherCode.Text = dt.Rows(i)("OtherCode").ToString()


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

                        Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtInvoiceDateGV"), TextBox)
                        'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtItemTypeGV"), DropDownList)
                        'Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtItemCodeGV"), TextBox)
                        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtItemDescriptionGV"), TextBox)
                        'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtQtyGV"), TextBox)
                        'Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtPriceWithDiscGV"), TextBox)
                        'Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtTaxTypeGV"), DropDownList)
                        'Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtGSTPercGV"), TextBox)
                        'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtTotalReceiptAmtGV"), TextBox)
                        'Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalCreditNoteAmtGV"), TextBox)

                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxRcnoReceipt = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtRcnoReceiptGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxARCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtARCodeGV"), TextBox)
                        Dim TextBoGSTCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGSTCodeGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceDate") = TextBoxInvoiceDate.Text
                        'dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        'dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        'dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        'dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        'dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        'dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
                        'dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        'dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        'dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text
                        dtCurrentTable.Rows(i - 1)("TotalReceiptAmt") = TextBoxTotalReceiptAmt.Text
                        'dtCurrentTable.Rows(i - 1)("TotalCrediteNoteAmt") = TextBoxBalanceAmt.Text

                        dtCurrentTable.Rows(i - 1)("ReceiptAmt") = TextBoxReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoReceipt") = TextBoxRcnoReceipt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ARCode") = TextBoxARCode.Text
                        dtCurrentTable.Rows(i - 1)("GSTCode") = TextBoGSTCode.Text
                        'dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
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

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged

        lblAlert.Text = ""

        'txtAccountIdBilling.Text = ""

        If txtSearch.Text = "" Then
            txtAccountIdBilling.Text = ""
            txtAccountIdBilling.Text = ""
            If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                txtAccountIdBilling.Text = ""
            Else
                txtAccountIdBilling.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            End If



            If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                txtAccountName.Text = ""
            Else
                txtAccountName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
            End If

            'txtSearch.Text = ""
        Else
            txtAccountIdSearch.Text = ""
            txtClientNameSearch.Text = ""
            If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                txtAccountIdSearch.Text = ""
            Else
                txtAccountIdSearch.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            End If


            If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                txtClientNameSearch.Text = ""
            Else
                txtClientNameSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
            End If
            'txtSearch.Text = ""
            updPnlSearch.Update()
        End If
        'lblAlert.Text = ""

        'txtAccountIdBilling.Text = ""


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
        sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode = 'CN-RET' "
        Dim command10 As MySqlCommand = New MySqlCommand
        command10.CommandType = CommandType.Text
        command10.CommandText = sql
        command10.Connection = conn

        Dim dr10 As MySqlDataReader = command10.ExecuteReader()

        Dim dt10 As New DataTable
        dt10.Load(dr10)

        If dt10.Rows.Count > 0 Then
            If dt10.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt10.Rows(0)("Description").ToString : End If
            If dt10.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt10.Rows(0)("COACode").ToString : End If
        End If

        '''''''''''''''''''''''''''''''''''
        'Dim query As String

        'ddlContractNo.Items.Clear()
        'ddlContractNo.Items.Add("--SELECT--")

        'sql = ""
        'sql = "Select ContractNo from tblContract where Status = 'O' and CompanyGroup ='" & txtCompanyGroup.Text & "' and ContractGroup='ST'"

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
        'query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
        'PopulateDropDownList(query, "ProductCode", "ProductCode", ddlItemCode)
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
 

    Private Sub MakeMeNullBillingDetails()
        txtCNNo.Text = ""
        'txtInvoiceDate.Text = ""
        txtAccountIdBilling.Text = ""
        txtAccountName.Text = ""
        txtCompanyGroup.Text = ""
        'ddlBankCode.SelectedIndex = 0
        'txtBankName.Text = ""
        'txtBankGLCode.Text = ""

        txtTotalWithGST.Text = "0.00"
        txtTotalGSTAmt.Text = "0.00"
        'txtTotalDiscAmt.Text = "0.00"
        txtTotalWithDiscAmt.Text = "0.00"
        txtReceiptAmt.Text = "0.00"
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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim rowselected As Integer
        rowselected = 0
        lblAlert.Text = ""


        'If String.IsNullOrEmpty(txtCNDate.Text) = True Then
        '    lblAlert.Text = "PLEASE ENTER INVOICE DATE"
        '    Exit Sub
        'End If

        'If String.IsNullOrEmpty(txtBankGLCode.Text) = True Then
        '    lblAlert.Text = "PLEASE SELECT BANK CODE"
        '    Exit Sub
        'End If



        Try

            PopulateArCode()
            Dim conn As MySqlConnection = New MySqlConnection()

            SetRowDataBillingDetailsRecs()
            'SetRowDataServiceRecs()
            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If tableAdd IsNot Nothing Then

                For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
                    'string txSpareId = row.ItemArray[0] as string;
                    Dim TextBoxchkSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)

                    If (TextBoxchkSelect.Checked = True) Then

                        'Header
                        rowselected = rowselected + 1

                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                        'Dim TextBoxInvoicePrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                        'Dim TextBoxInvoiceGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxInvoiceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)

                        If rowselected = 1 Then
                            'Dim lblid1 As TextBox = CType(grvBillingDetails.Rows(0).FindControl("txtAccountIdGV"), TextBox)

                            If String.IsNullOrEmpty(txtCNNo.Text) = True Then
                                'If vppPeriodCurrent = "" Then
                                '    Me.txtCalendarPeriod.Text = cls00Regional.Period_Calendar(Me.mskSalesDate.Text)
                                'Else
                                '    Me.txtCalendarPeriod.Text = vppPeriodCurrent
                                'End If
                                'If Me.txtCalendarPeriod.Text <> "" Then txtGlPeriod.Text = cls00Regional.Period_Accounting(txtCalendarPeriod.Text)

                                GenerateCNNo()
                            End If

                            'Dim conn As MySqlConnection = New MySqlConnection()

                            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                            conn.Open()

                            Dim command As MySqlCommand = New MySqlCommand

                            command.CommandType = CommandType.Text
                            Dim qry As String

                            If txtMode.Text = "NEW" Then
                                qry = "INSERT INTO tblcn(CNNumber, CustomerName, AccountId,   "
                                qry = qry + "  CNDate, Cheque, ChequeDate, BankId, LedgerCode, LedgerName,  PaymentType, ContractNo, ItemCode, ItemDescription,   "
                                qry = qry + " BaseAmount, GSTAmount,  NetAmount, BankAmount, Comments, ContactType, CompanyGroup, GLPeriod,  ModuleType, Salesman,"
                                qry = qry + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qry = qry + " (@CNNumber, @CustomerName, @AccountId,  "
                                qry = qry + " @CNDate, @Cheque, @ChequeDate, @BankId, @LedgerCode, @LedgerName,  @PaymentType, @ContractNo, @ItemCode, @ItemDescription, "
                                qry = qry + " @BaseAmount, @GSTAmount,  @NetAmount, @BankAmount, @Comments, @ContactType, @CompanyGroup, @GLPeriod,  @ModuleType, @Salesman,"
                                qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                command.CommandText = qry
                                command.Parameters.Clear()

                                command.Parameters.AddWithValue("@CNNumber", txtCNNo.Text)
                                command.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                                command.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)

                                If String.IsNullOrEmpty(txtCNDate.Text.Trim) = True Then
                                    command.Parameters.AddWithValue("@CNDate", DBNull.Value)
                                Else
                                    command.Parameters.AddWithValue("@CNDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                                End If

                                'command.Parameters.AddWithValue("@ReceiptDate", Convert.ToDateTime(txtReceiptDate.Text))
                                'command.Parameters.AddWithValue("@BillAmount", 0.0)
                                'command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
                                'command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
                                command.Parameters.AddWithValue("@Comments", txtComments.Text)
                                command.Parameters.AddWithValue("@Cheque", "")
                                command.Parameters.AddWithValue("@PaymentType", "")
                                command.Parameters.AddWithValue("@ContractNo", ddlContractNo.Text)

                                command.Parameters.AddWithValue("@ItemCode", txtARCode10.Text)
                                command.Parameters.AddWithValue("@ItemDescription", txtARDescription10.Text)

                                'If String.IsNullOrEmpty(txtChequeDate.Text.Trim) = True Then
                                command.Parameters.AddWithValue("@ChequeDate", DBNull.Value)
                                'Else
                                '    command.Parameters.AddWithValue("@ChequeDate", Convert.ToDateTime(txtChequeDate.Text).ToString("yyyy-MM-dd"))
                                'End If


                                command.Parameters.AddWithValue("@BaseAmount", Convert.ToDecimal(txtTotalWithDiscAmt.Text))
                                command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                                'command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
                                command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtCNAmount.Text))
                                command.Parameters.AddWithValue("@BankAmount", Convert.ToDecimal(txtReceiptAmt.Text))

                                command.Parameters.AddWithValue("@BankId", "")
                                command.Parameters.AddWithValue("@LedgerCode", "")
                                command.Parameters.AddWithValue("@LedgerName", "")

                                command.Parameters.AddWithValue("@ContactType", ddlContactType.Text)
                                command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                                command.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)

                             
                                command.Parameters.AddWithValue("@ModuleType", "CNPB")

                                'If txtSalesman.Text = "-1" Then
                                command.Parameters.AddWithValue("@Salesman", "")
                                'Else
                                '    command.Parameters.AddWithValue("@Salesman", txtSalesman.Text)
                                'End If



                                command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                command.Parameters.AddWithValue("@CreatedOn", Now)
                                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                command.Parameters.AddWithValue("@LastModifiedOn", Now)

                                command.Connection = conn
                                command.ExecuteNonQuery()

                                Dim sqlLastId As String
                                sqlLastId = "SELECT last_insert_id() from tblCN"

                                Dim commandRcno As MySqlCommand = New MySqlCommand
                                commandRcno.CommandType = CommandType.Text
                                commandRcno.CommandText = sqlLastId
                                commandRcno.Parameters.Clear()
                                commandRcno.Connection = conn
                                txtRcno.Text = commandRcno.ExecuteScalar()


                            Else
                                qry = "Update tblcn set CNNumber =@CNNumber, CustomerName =@CustomerName, AccountId =@AccountId, ContractNo = @ContractNo,  "
                                qry = qry + " CNDate =@CNDate, Cheque =@Cheque, ChequeDate =@ChequeDate, BankId = @BankId, LedgerCode =@LedgerCode, LedgerName =@LedgerName,  PaymentType =@PaymentType,   "
                                qry = qry + " BaseAmount =@BaseAmount, GSTAmount =@GSTAmount,  NetAmount =@NetAmount, BankAmount =@BankAmount, Comments =@Comments, ContactType =@ContactType, CompanyGroup =@CompanyGroup, GLPeriod =@GLPeriod, "
                                qry = qry + " LastModifiedBy = @LastModifiedBy, LastModifiedOn = @LastModifiedOn "
                                qry = qry + " where Rcno = @Rcno;"


                                command.CommandText = qry
                                command.Parameters.Clear()

                                command.Parameters.AddWithValue("@Rcno", Convert.ToInt64(txtRcno.Text))
                                command.Parameters.AddWithValue("@CNNumber", txtCNNo.Text)
                                command.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                                command.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)

                                If String.IsNullOrEmpty(txtCNDate.Text.Trim) = True Then
                                    command.Parameters.AddWithValue("@CNDate", DBNull.Value)
                                Else
                                    command.Parameters.AddWithValue("@CNDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                                End If

                                'command.Parameters.AddWithValue("@ReceiptDate", Convert.ToDateTime(txtReceiptDate.Text))
                                'command.Parameters.AddWithValue("@BillAmount", 0.0)
                                'command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
                                'command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
                                command.Parameters.AddWithValue("@Comments", txtComments.Text)
                                command.Parameters.AddWithValue("@Cheque", "")
                                command.Parameters.AddWithValue("@PaymentType", "")
                                command.Parameters.AddWithValue("@ContractNo", ddlContractNo.Text)
                                'If String.IsNullOrEmpty(txtChequeDate.Text.Trim) = True Then
                                command.Parameters.AddWithValue("@ChequeDate", DBNull.Value)
                                'Else
                                '    command.Parameters.AddWithValue("@ChequeDate", Convert.ToDateTime(txtChequeDate.Text).ToString("yyyy-MM-dd"))
                                'End If

                                command.Parameters.AddWithValue("@BaseAmount", Convert.ToDecimal(txtTotalWithDiscAmt.Text))
                                command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                                'command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
                                command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtCNAmount.Text))
                                command.Parameters.AddWithValue("@BankAmount", Convert.ToDecimal(txtReceiptAmt.Text))


                                command.Parameters.AddWithValue("@BankId", "")
                                command.Parameters.AddWithValue("@LedgerCode", "")
                                command.Parameters.AddWithValue("@LedgerName", "")

                                command.Parameters.AddWithValue("@ContactType", ddlContactType.Text)
                                command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                                command.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)


                                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                command.Parameters.AddWithValue("@LastModifiedOn", Now)

                                command.Connection = conn
                                command.ExecuteNonQuery()
                            End If

                        End If  'rowIndex =0
                        'Header

                        'Dim commandServiceBillingDetail As MySqlCommand = New MySqlCommand
                        'commandServiceBillingDetail.CommandType = CommandType.Text
                        'Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set rcnoInvoice = " & Convert.ToInt64(txtRcno.Text) & "  where RcnoServiceRecord =" & TextBoxRcnoServiceRecord.Text

                        'commandServiceBillingDetail.CommandText = sqlUpdateServiceBillingDetail
                        'commandServiceBillingDetail.Parameters.Clear()
                        'commandServiceBillingDetail.Connection = conn
                        'commandServiceBillingDetail.ExecuteNonQuery()


                        'Start:Detail
                        Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                        commandSalesDetail.CommandType = CommandType.Text
                        Dim qrySalesDetail As String = "INSERT INTO tblCnDet(CNNumber, InvoiceNo, InvoiceDate, InvoicePrice, InvoiceGST, InvoiceValue,  "
                        qrySalesDetail = qrySalesDetail + "CNValue, LedgerCode, LedgerName, ItemCode, ItemDescription,"
                        qrySalesDetail = qrySalesDetail + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                        qrySalesDetail = qrySalesDetail + "(@CNNumber, @InvoiceNo, @InvoiceDate, @InvoicePrice, @InvoiceGST,  @InvoiceValue, "
                        qrySalesDetail = qrySalesDetail + "@CNValue, @LedgerCode, @LedgerName, @ItemCode, @ItemDescription,"
                        qrySalesDetail = qrySalesDetail + "@CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                        commandSalesDetail.CommandText = qrySalesDetail
                        commandSalesDetail.Parameters.Clear()

                        commandSalesDetail.Parameters.AddWithValue("@CNNumber", txtCNNo.Text)
                        commandSalesDetail.Parameters.AddWithValue("@InvoiceNo", TextBoxInvoiceNo.Text)

                        If TextBoxInvoiceDate.Text.Trim = "" Then
                            commandSalesDetail.Parameters.AddWithValue("@InvoiceDate", DBNull.Value)
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@InvoiceDate", Convert.ToDateTime(TextBoxInvoiceDate.Text).ToString("yyyy-MM-dd"))
                        End If

                        commandSalesDetail.Parameters.AddWithValue("@InvoicePrice", Convert.ToDecimal(TextBoxInvoiceValue.Text))
                        commandSalesDetail.Parameters.AddWithValue("@InvoiceGST", 0.0)
                        commandSalesDetail.Parameters.AddWithValue("@InvoiceValue", Convert.ToDecimal(TextBoxInvoiceValue.Text))
                        commandSalesDetail.Parameters.AddWithValue("@CNValue", Convert.ToDecimal(TextBoxReceiptAmt.Text))

                        commandSalesDetail.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                        commandSalesDetail.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)

                        commandSalesDetail.Parameters.AddWithValue("@ItemCode", txtARCode10.Text)
                        commandSalesDetail.Parameters.AddWithValue("@ItemDescription", txtARDescription10.Text)

                        'commandSalesDetail.Parameters.AddWithValue("@ValueBase", 1)
                        'commandSalesDetail.Parameters.AddWithValue("@GSTBase", 1)
                        'commandSalesDetail.Parameters.AddWithValue("@AppliedBase", 1)


                        commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        commandSalesDetail.Parameters.AddWithValue("@CreatedOn", Now)
                        commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", Now)

                        commandSalesDetail.Connection = conn
                        commandSalesDetail.ExecuteNonQuery()
                        'conn.Close()

                        ''Start: Update tblSales

                        ''''''''''''''''''''
                        Dim lTotalReceipt As Decimal
                        Dim lInvoiceAmount As Decimal
                        Dim lTotalcn As Decimal

                        lTotalReceipt = 0.0
                        lInvoiceAmount = 0.0
                        lTotalcn = 0.0
                        'Get Item desc, price Id

                        'Dim conn As MySqlConnection = New MySqlConnection()

                        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        'conn.Open()
                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT NetAmount FROM tblSales where InvoiceNumber = '" & TextBoxInvoiceNo.Text & "'"
                        command1.Connection = conn

                        Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                        Dim dt1 As New DataTable
                        dt1.Load(dr1)

                        If dt1.Rows.Count > 0 Then
                            lInvoiceAmount = dt1.Rows(0)("NetAmount").ToString
                        End If

                        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        'conn.Open()
                        Dim command2 As MySqlCommand = New MySqlCommand
                        command2.CommandType = CommandType.Text
                        command2.CommandText = "SELECT sum(ReceiptValue) as totalreceipt FROM tblrecvdet where InvoiceNo = '" & TextBoxInvoiceNo.Text & "'"
                        command2.Connection = conn

                        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                        Dim dt2 As New DataTable
                        dt2.Load(dr2)

                        'If dt2.Rows.Count > 0 Then
                        '    lTotalReceipt = dt2.Rows(0)("totalreceipt").ToString
                        'End If

                        If dt2.Rows.Count > 0 Then
                            If String.IsNullOrEmpty(dt2.Rows(0)("totalreceipt").ToString) = True Then
                                lTotalReceipt = 0.0
                            Else
                                lTotalReceipt = dt2.Rows(0)("totalreceipt").ToString
                            End If
                        End If
                        ''''''''''''''''''''''''

                        Dim command3 As MySqlCommand = New MySqlCommand
                        command3.CommandType = CommandType.Text
                        command3.CommandText = "SELECT sum(CNValue) as totalcn FROM tblcndet where InvoiceNo = '" & TextBoxInvoiceNo.Text & "'"
                        command3.Connection = conn

                        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                        Dim dt3 As New DataTable
                        dt3.Load(dr3)

                        If dt3.Rows.Count > 0 Then
                            If String.IsNullOrEmpty(dt3.Rows(0)("totalcn").ToString) = True Then
                                lTotalcn = 0.0
                            Else
                                lTotalcn = dt3.Rows(0)("totalcn").ToString
                            End If
                        End If
                        ''''''''''''''''''''''''
                        Dim lstatus As String
                        lstatus = ""

                        If lInvoiceAmount = lTotalReceipt + lTotalcn Then
                            lstatus = "F"
                        ElseIf lInvoiceAmount > lTotalReceipt + lTotalcn Then
                            lstatus = "P"
                        ElseIf lTotalReceipt + lTotalcn > 0 Then
                            lstatus = "O"
                        End If

                        Dim command4 As MySqlCommand = New MySqlCommand
                        command4.CommandType = CommandType.Text

                        Dim qry4 As String = "Update tblSales Set PaidStatus = '" & lstatus & "', TotalCNAmount = " & lTotalcn & " where InvoiceNumber = @InvoiceNumber "

                        command4.CommandText = qry4
                        command4.Parameters.Clear()

                        command4.Parameters.AddWithValue("@InvoiceNumber", TextBoxInvoiceNo.Text)
                        command4.Connection = conn
                        command4.ExecuteNonQuery()


                        ''End: Update tblServiceRecord
                    End If

                Next rowIndex

                GridView1.DataSourceID = "SQLDSCN"
                GridView1.DataBind()
                'lblMessage.Text = "ADD: CREDIT NOTE SUCCESSFULLY ADDED"
            End If



            'FirstGridViewRowBillingDetailsRecs()
            'FirstGridViewRowGL()
            'MakeMeNull()

            conn.Close()


            If rowselected = 0 Then
                lblAlert.Text = "PLEASE SELECT A RECORD"
                btnShowInvoices.Enabled = False
                updPnlMsg.Update()
                Exit Sub
            End If

            DisableControls()
            'txtRcno.Text = command.LastInsertedId
            ' End If

            'FirstGridViewRowServiceRecs()

            If txtMode.Text = "NEW" Then
                lblMessage.Text = "ADD: CREDIT NOTE RECORD SUCCESSFULLY ADDED"
            Else
                lblMessage.Text = "EDIT: CREDIT NOTE RECORD SUCCESSFULLY UPDATED"
            End If


            lblAlert.Text = ""

            updPnlMsg.Update()
            updPnlSearch.Update()
            updPnlBillingRecs.Update()
            'updpnlServiceRecs.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub btnShowInvoices_Click(sender As Object, e As EventArgs) Handles btnShowInvoices.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            'If String.IsNullOrEmpty(txtAccountIdBilling.Text) = True Then
            '    lblAlert.Text = "Please Select Account Id"
            '    updPnlMsg.Update()
            '    Exit Sub
            'End If



            ''''''''''''''''''

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

            Dim Total As Decimal
            Dim TotalWithGST As Decimal
            Dim TotalDiscAmt As Decimal
            Dim TotalGSTAmt As Decimal
            Dim TotalPriceWithDiscountAmt As Decimal


            Total = 0.0
            TotalWithGST = 0.0
            TotalDiscAmt = 0.0
            TotalGSTAmt = 0.0
            TotalPriceWithDiscountAmt = 0.0

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmdServiceBillingDetailItem As MySqlCommand = New MySqlCommand
            cmdServiceBillingDetailItem.CommandType = CommandType.Text
            'cmdServiceBillingDetailItem.CommandText = "SELECT * FROM tblsales where Rcnoservicebillingdetail=" & Convert.ToInt32(txtRcnoservicebillingdetail.Text)

            If String.IsNullOrEmpty(ddlContractNo.Text) = True Then
                cmdServiceBillingDetailItem.CommandText = "SELECT ContractNo, ContractDate, AgreeValue, RetentionValue FROM tblContract where Status = 'O' and CompanyGroup ='" & txtCompanyGroup.Text & "' and ContractGroup = 'ST'  and AccountId = '" & txtAccountIdBilling.Text & "'"
            Else
                cmdServiceBillingDetailItem.CommandText = "SELECT ContractNo, ContractDate, AgreeValue, RetentionValue FROM tblContract where Status = 'O' and CompanyGroup ='" & txtCompanyGroup.Text & "' and ContractGroup = 'ST'  and AccountId = '" & txtAccountIdBilling.Text & "' and ContractNo ='" & ddlContractNo.Text & "'"
            End If

            cmdServiceBillingDetailItem.Connection = conn

            Dim drcmdServiceBillingDetailItem As MySqlDataReader = cmdServiceBillingDetailItem.ExecuteReader()
            Dim dtServiceBillingDetailItem As New DataTable
            dtServiceBillingDetailItem.Load(drcmdServiceBillingDetailItem)

            Dim TotDetailRecordsLoc = dtServiceBillingDetailItem.Rows.Count
            If dtServiceBillingDetailItem.Rows.Count > 0 Then

                Dim rowIndex = 0

                For Each row As DataRow In dtServiceBillingDetailItem.Rows
                    If (TotDetailRecordsLoc > (rowIndex + 1)) Then
                        AddNewRowBillingDetailsRecs()
                        'AddNewRow()
                    End If


                    Dim TextBoxtxtInvoiceNoGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    TextBoxtxtInvoiceNoGV.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ContractNo"))

                    Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
                    TextBoxInvoiceDate.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ContractDate"))

                    Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("AgreeValue"))

                    Dim TextBoxTotalTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
                    TextBoxTotalTotalReceiptAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("RetentionValue"))

                    'Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
                    'TextBoxBalanceAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("TotalCNAmount"))

                    'Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
                    'TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
                    'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
                    ''txtAmountWithDiscount.Text = Total - TotalDiscAmt
                    'TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                    'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

                    'Dim Query As String

                    'Dim TextBoxItemCode2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                    'Query = "Select * from tblbillingproducts"
                    'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode2)

                    'Dim TextBoxUOM2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                    'Query = "Select * from tblunitms"
                    'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM2)


                    'Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                    'Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

                    'If TextBoxItemType1.Text = "SERVICE" Then
                    '    TextBoxQty1.Enabled = False
                    '    TextBoxQty1.Text = 1
                    '    TextBoxItemType1.Enabled = False
                    'End If

                    rowIndex += 1

                Next row

                'txtTotal.Text = Total.ToString("N2")

                'txtTotalDiscAmt.Text = TotalDiscAmt.ToString("N2")


                'txtTotalDiscAmt.Text = 0.0
                'txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
                'txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
                'txtTotalWithGST.Text = TotalWithGST.ToString("N2")
            Else
                FirstGridViewRowBillingDetailsRecs()
                'FirstGridViewRowTarget()
                'Dim Query As String
                'Dim TextBoxTargetDesc As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                'Query = "Select * from tblTarget"

                'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc)
            End If

            'End: Detail Records


            'End: From tblBillingDetailItem
            'End If

            'AddNewRowBillingDetailsRecs()

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
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub


    Protected Sub txtTaxTypeGV_SelectedIndexChanged(sender As Object, e As EventArgs)

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
        command1.CommandText = "SELECT * FROM tblbillingproducts  where  ProductCode = '" & lblid1.Text & "'"
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


        'If rowindex1 = grvBillingDetails.Rows.Count - 1 Then
        '    btnAddDetail_Click(sender, e)
        '    'txtRecordAdded.Text = "Y"
        'End If
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

                'Dim TextBoxRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                'If (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
                '    If (Convert.ToInt32(TextBoxRcno.Text) > 0) Then

                '        Dim conn As MySqlConnection = New MySqlConnection(constr)
                '        conn.Open()

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

                'CalculateTotalPrice()

            End If
        Catch ex As Exception
            Throw ex
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
            Throw ex
        End Try
    End Sub

    Protected Sub btnSearch1Status_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch1Status.Click
        'mdlPopupStatusSearch.Show()
    End Sub


    Protected Sub txtAccountId_TextChanged(sender As Object, e As EventArgs)
        'Dim query As String
        'query = "Select ContractNo from tblContract where AccountId = '" & txtAccountId.Text & "'"
        'PopulateDropDownList(query, "ContractNo", "ContractNo", ddlContractNo)
    End Sub

    Protected Sub btnQuickReset_Click(sender As Object, e As EventArgs) Handles btnQuickReset.Click
        txtReceiptnoSearch.Text = ""
        txtAccountIdSearch.Text = ""
        txtBillingPeriodSearch.Text = ""
        txtClientNameSearch.Text = ""
        ddlCompanyGrpSearch.SelectedIndex = 0
        'ddlSalesmanSearch.SelectedIndex = 0
        txtSearch1Status.Text = "O"
        ddlCompanyGrpSearch.SelectedIndex = 0

        btnQuickSearch_Click(sender, e)


        'btnSearch1Status_Click(sender, e)
    End Sub

    Protected Sub chkSelectGV_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk1 As CheckBox = DirectCast(sender, CheckBox)
        xgrvBillingDetails = CType(chk1.NamingContainer, GridViewRow)


        Dim lblid1 As TextBox = CType(chk1.FindControl("txtTotalPriceWithGSTGV"), TextBox)
        Dim lblid2 As TextBox = CType(chk1.FindControl("txtTotalReceiptAmtGV"), TextBox)
        Dim lblid3 As TextBox = CType(chk1.FindControl("txtReceiptAmtGV"), TextBox)

        If chk1.Checked = True Then
            lblid3.Text = lblid2.Text
        Else
            lblid3.Text = "0.00"
        End If
        'lblid2.Text = lblid1.Text

        calculateTotalReceipt()
    End Sub

    Private Sub calculateTotalReceipt()

        Dim TotalReceiptAmt As Decimal = 0
        Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)


        If (table.Rows.Count > 0) Then

            For i As Integer = 0 To (table.Rows.Count) - 1

                Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)
                Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtReceiptAmtGV"), TextBox)

                If String.IsNullOrEmpty(TextBoxReceiptAmt.Text) = True Then
                    TextBoxReceiptAmt.Text = "0.00"
                End If

                If TextBoxSelect.Checked = True Then
                    TotalReceiptAmt = TotalReceiptAmt + Convert.ToDecimal(TextBoxReceiptAmt.Text)
                End If
            Next i

        End If


        txtReceiptAmt.Text = TotalReceiptAmt.ToString("N2")

        updPanelSave.Update()
    End Sub

    Protected Sub txtReceiptAmtGV_TextChanged(sender As Object, e As EventArgs)

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


        If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON order by name"
            End If
        Else
            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopUpClient.Text + "%' or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1 and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
            End If

            'SqlDSClient.DataBind()
            'gvClient.DataBind()
            'mdlPopUpClient.Show()
        End If

        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
        'End If

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

    End Sub

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        lblAlert.Text = ""
        txtSearch.Text = ""
        If String.IsNullOrEmpty(ddlContactType.Text) Or ddlContactType.Text = "--SELECT--" Then
            '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
            lblAlert.Text = "SELECT CUSTOMER TYPE TO PROCEED"
            Exit Sub
        End If


        If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountIdBilling.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text

            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contactperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1 and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
        Else

            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1  order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1  order by name"
            End If


            SqlDSClient.DataBind()
            gvClient.DataBind()
        End If
        mdlPopUpClient.Show()
    End Sub

   

    Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""


            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If

            Dim confirmValue As String
            confirmValue = ""
            Dim conn As MySqlConnection = New MySqlConnection()

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then
                ''''''''''''''' Insert tblAR



                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command5 As MySqlCommand = New MySqlCommand
                command5.CommandType = CommandType.Text

                Dim qry5 As String = "DELETE from tblAR where BatchNo = '" & txtCNNo.Text & "'"

                command5.CommandText = qry5
                'command1.Parameters.Clear()
                command5.Connection = conn
                command5.ExecuteNonQuery()

                Dim qryAR As String
                Dim commandAR As MySqlCommand = New MySqlCommand
                commandAR.CommandType = CommandType.Text

                If Convert.ToDecimal(txtCNAmount.Text) > 0.0 Then
                    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, BillingPeriod, "
                    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @BillingPeriod,"
                    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    commandAR.CommandText = qryAR
                    commandAR.Parameters.Clear()
                    commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                    If txtCNDate.Text.Trim = "" Then
                        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                    Else
                        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    commandAR.Parameters.AddWithValue("@BillingPeriod", txtReceiptPeriod.Text)
                    commandAR.Parameters.AddWithValue("@ContractNo", "")
                    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
                    commandAR.Parameters.AddWithValue("@GLCode", txtARCode10.Text)
                    commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription10.Text)
                    commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtCNAmount.Text))
                    commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
                    commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                    commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    commandAR.Connection = conn
                    commandAR.ExecuteNonQuery()

                    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, GLtype,  BillingPeriod,"
                    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @GLtype, @BillingPeriod,"
                    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    commandAR.CommandText = qryAR
                    commandAR.Parameters.Clear()
                    commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                    If txtCNDate.Text.Trim = "" Then
                        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                    Else
                        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    commandAR.Parameters.AddWithValue("@BillingPeriod", txtReceiptPeriod.Text)
                    commandAR.Parameters.AddWithValue("@ContractNo", "")
                    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
                    commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
                    commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
                    commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)
                    commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtCNAmount.Text))
                    commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                    commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                    commandAR.Parameters.AddWithValue("@GLType", "Debtor")

                    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    commandAR.Connection = conn
                    commandAR.ExecuteNonQuery()
                End If


                Dim commandUpdateCN As MySqlCommand = New MySqlCommand
                commandUpdateCN.CommandType = CommandType.Text
                Dim sqlUpdateCN As String = "Update tblCN set GLStatus = 'P'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

                commandUpdateCN.CommandText = sqlUpdateCN
                commandUpdateCN.Parameters.Clear()
                commandUpdateCN.Connection = conn
                commandUpdateCN.ExecuteNonQuery()

                GridView1.DataBind()

                lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
                lblAlert.Text = ""
                updPnlSearch.Update()
                updPnlMsg.Update()
                updpnlBillingDetails.Update()
                'updpnlServiceRecs.Update()
                updpnlBillingDetails.Update()
                btnQuickSearch_Click(sender, e)
            End If

            ''''''''''''''' Insert tblAR

           
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtMode.Text = ""
        MakeMeNullBillingDetails()
        MakeMeNull()
        DisableControls()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Session("receiptfrom") = "invoice"

        Response.Redirect("Invoice.aspx")
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter client name", "str")
        Else
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            'SqlDSClient.SelectCommand = "SELECT ContType, AccountID, ContID, ContName, ContPerson, ContAddress1, ContHP, ContEmail,  ContLocationGroup, ContGroup, ContAddBlock, ContAddNos, ContAddFloor, ContAddUnit, ContAddStreet, ContAddBuilding, ContAddCity, ContAddState, ContAddCountry, ContAddPostal, ContFax, Mobile, ContTel, ContSales From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"


            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountID like '%" + txtPopUpClient.Text.Trim + "%' or contactperson like '%" + txtPopUpClient.Text.Trim + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1 and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '%" + txtPopUpClient.Text.Trim + "%' or contACTperson like '%" + txtPopUpClient.Text.Trim + "%') order by name"
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            txtIsPopup.Text = "Client"
        End If

        'txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
    End Sub


    Protected Sub ddlItemCode_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim sql As String
        sql = ""
        'sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode = 'CN-RET' and CompanyGroup = '" & txtCompanyGroup.Text & "'"
        sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode = 'CN-RET' "
        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.Text
        command1.CommandText = sql
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt.Rows(0)("Description").ToString : End If
            If dt.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt.Rows(0)("COACode").ToString : End If
        End If

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
            TextBoxGLCodeAR.Text = txtARCode10.Text

            Dim TextBoxGLDescriptionAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            TextBoxGLDescriptionAR.Text = txtARDescription10.Text

            Dim TextBoxDebitAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            TextBoxDebitAmountAR.Text = Convert.ToDecimal(txtCNAmount.Text).ToString("N2")

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
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'Dim cmdGL As MySqlCommand = New MySqlCommand
            'cmdGL.CommandType = CommandType.Text
            'cmdGL.CommandText = "SELECT LedgerCode, LedgerName, ReceiptValue   FROM tblcndet where ReceiptNumber ='" & txtCNNo.Text.Trim & "' order by LedgerCode"
            ''cmdGL.CommandText = "SELECT * FROM tblAR where BatchNo ='" & txtBatchNo.Text.Trim & "'"
            'cmdGL.Connection = conn

            'Dim drcmdGL As MySqlDataReader = cmdGL.ExecuteReader()
            'Dim dtGL As New DataTable
            'dtGL.Load(drcmdGL)

            ''FirstGridViewRowGL()


            'Dim TotDetailRecordsLoc = dtGL.Rows.Count
            'If dtGL.Rows.Count > 0 Then

            '    lGLCode = ""
            '    lGLDescription = ""
            '    lCreditAmount = 0.0


            '    lGLCode = dtGL.Rows(0)("LedgerCode").ToString()
            '    lGLDescription = dtGL.Rows(0)("LedgerName").ToString()
            '    lCreditAmount = 0.0

            '    Dim rowIndex4 = 0

            '    For Each row As DataRow In dtGL.Rows

            '        If lGLCode = row("LedgerCode") Then
            '            lCreditAmount = lCreditAmount + row("ReceiptValue")
            '        Else


            '            If (TotDetailRecordsLoc > (rowIndex4 + 1)) Then
            '                AddNewRowGL()
            '            End If

            '            Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            '            TextBoxGLCode.Text = lGLCode

            '            Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            '            TextBoxGLDescription.Text = lGLDescription

            '            Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            '            TextBoxDebitAmount.Text = (0.0).ToString("N2")

            '            Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
            '            TextBoxCreditAmount.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")

            '            lGLCode = row("LedgerCode")
            '            lGLDescription = row("LedgerName")
            '            lCreditAmount = row("ReceiptValue")

            '            rowIndex3 += 1
            '            rowIndex4 += 1
            '        End If
            '    Next row

            'End If


            'AddNewRowGL()

            Dim TextBoxGLCode1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            TextBoxGLCode1.Text = txtARCode.Text

            Dim TextBoxGLDescription1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            TextBoxGLDescription1.Text = txtARDescription.Text

            Dim TextBoxDebitAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            TextBoxDebitAmount1.Text = (0.0).ToString("N2")

            Dim TextBoxCreditAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
            TextBoxCreditAmount1.Text = Convert.ToDecimal(Convert.ToDecimal(txtCNAmount.Text).ToString("N2"))


            SetRowDataGL()
            Dim dtScdrLoc1 As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
            Dim drCurrentRowLoc1 As DataRow = Nothing

            dtScdrLoc1.Rows.Remove(dtScdrLoc1.Rows(rowIndex3 + 1))
            drCurrentRowLoc1 = dtScdrLoc1.NewRow()
            ViewState("CurrentTableGL") = dtScdrLoc1
            grvGL.DataSource = dtScdrLoc1
            grvGL.DataBind()
            SetPreviousDataGL()

            ''''''''''''''''' End: Display GL Grid
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
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
            Throw ex
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

    Protected Sub txtCNDate_TextChanged(sender As Object, e As EventArgs) Handles txtCNDate.TextChanged
        txtReceiptPeriod.Text = Year(Convert.ToDateTime(txtCNDate.Text)) & Format(Month(Convert.ToDateTime(txtCNDate.Text)), "00")
    End Sub

    

    Protected Sub btnClientSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientSearch.Click
        lblAlert.Text = ""
        txtSearch.Text = ""

        If String.IsNullOrEmpty(ddlContactType.Text) Or ddlContactType.Text = "--SELECT--" Then
            '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
            lblAlert.Text = "SELECT CUSTOMER TYPE TO PROCEED"
            Exit Sub
        End If

        txtSearch.Text = "Search"
        If String.IsNullOrEmpty(txtAccountIdSearch.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountIdSearch.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text

            If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contactperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()

            updPanelCN.Update()
        Else

            If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' order by name"
            ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' order by name"
            End If


            SqlDSClient.DataBind()
            gvClient.DataBind()
        End If
        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
        'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "'  or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"

        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
            SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1  order by name"
        ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
            SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1  order by name"
        End If
        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
        txtIsPopup.Text = "Client"
    End Sub
End Class
