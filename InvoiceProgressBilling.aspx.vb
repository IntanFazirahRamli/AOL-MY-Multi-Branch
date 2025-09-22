Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web

Imports System.Globalization
Imports System.Threading
Imports System.Drawing


Partial Class InvoiceProgressBilling
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

    Public lGLCode As String
    Public lGLDescription As String
    Public lCreditAmount As Decimal

    Public gBillNo As String
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim Query As String

        'Restrict users manual date entries

        txtAccountIdBilling.Attributes.Add("readonly", "readonly")
        txtAccountName.Attributes.Add("readonly", "readonly")
        txtBillAddress.Attributes.Add("readonly", "readonly")
        txtBillBuilding.Attributes.Add("readonly", "readonly")
        txtBillStreet.Attributes.Add("readonly", "readonly")
        txtBillCountry.Attributes.Add("readonly", "readonly")
        txtBillPostal.Attributes.Add("readonly", "readonly")
        txtTotal.Attributes.Add("readonly", "readonly")
        txtTaxRatePct.Attributes.Add("readonly", "readonly")


        txtBillingPeriod.Attributes.Add("readonly", "readonly")
        txtCompanyGroup.Attributes.Add("readonly", "readonly")
        txtAccountType.Attributes.Add("readonly", "readonly")
        'txtInvoiceAmount.Attributes.Add("readonly", "readonly")


        'txtDiscountAmount.Attributes.Add("readonly", "readonly")
        'txtAmountWithDiscount.Attributes.Add("readonly", "readonly")

        txtGSTAmount.Attributes.Add("readonly", "readonly")
        txtNetAmount.Attributes.Add("readonly", "readonly")

        txtContractNo.Attributes.Add("readonly", "readonly")
        txtContractValue.Attributes.Add("readonly", "readonly")
        txtAdjustedContractValue.Attributes.Add("readonly", "readonly")

        txtPreviousClaim.Attributes.Add("readonly", "readonly")
        txtTotalClaim.Attributes.Add("readonly", "readonly")
        txtPreviousRetentionAmount.Attributes.Add("readonly", "readonly")

        txtTotalRetentionAmount.Attributes.Add("readonly", "readonly")
        'txtBillingPeriod.Attributes.Add("readonly", "readonly")
        'txtBillingPeriod.Attributes.Add("readonly", "readonly")

        'txtCreatedOn.Attributes.Add("readonly", "readonly")
        'txtServTimeOut.Attributes.Add("onchange", "getTheDiffTime()")

        If Not Page.IsPostBack Then
            'mdlPopUpClient.Hide()

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

            '''''''''''''''''''''''''''''

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

            ''Dim command1 As MySqlCommand = New MySqlCommand
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


            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='OTHER DEBTORS'"

            'Dim command10 As MySqlCommand = New MySqlCommand
            'command10.CommandType = CommandType.Text
            'command10.CommandText = sql
            'command10.Connection = conn

            'Dim dr10 As MySqlDataReader = command10.ExecuteReader()

            'Dim dt10 As New DataTable
            'dt10.Load(dr10)

            'If dt10.Rows.Count > 0 Then
            '    If dt10.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt10.Rows(0)("COACode").ToString : End If
            '    If dt10.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt10.Rows(0)("Description").ToString : End If
            'End If


            ' '''''''''''''''''''''''''''''''''''


            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLType='GST OUTPUT'"

            ''Dim command1 As MySqlCommand = New MySqlCommand
            'command1.CommandType = CommandType.Text
            'command1.CommandText = sql
            'command1.Connection = conn

            'Dim dr2 As MySqlDataReader = command1.ExecuteReader()

            'Dim dt2 As New DataTable
            'dt2.Load(dr2)

            'If dt2.Rows.Count > 0 Then
            '    If dt2.Rows(0)("COACode").ToString <> "" Then : txtGSTOutputCode.Text = dt2.Rows(0)("COACode").ToString : End If
            '    If dt2.Rows(0)("Description").ToString <> "" Then : txtGSTOutputDescription.Text = dt2.Rows(0)("Description").ToString : End If
            'End If
            conn.Close()

            MakeMeNull()
            DisableControls()

            updPnlBillingRecs.Update()
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


            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='OTHER DEBTORS'"
            sql = "Select COACode, Description from tblchartofaccounts where GLtype='OTHER DEBTORS'"
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
            'updPnlBillingRecs.Update()
            updPnlBillingRecs.Update()

            conn.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ''Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        ''Dim objTextInfo As TextInfo = cultureInfo.TextInfo

        lblAlert.Text = ""
        lblMessage.Text = ""

        Try
            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            MakeMeNull()
            MakeMeNullBillingDetails()

            txtMode.Text = "VIEW"
            'btnSvcEdit.Enabled = False
            'btnSvcDelete.Enabled = False

            'btnSvcEdit.Enabled = False
            'btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            'btnSvcDelete.Enabled = False
            'btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            Dim editindex As Integer = GridView1.SelectedIndex

            '
            txtRcno.Text = 0
            txtRcno.Text = GridView1.SelectedRow.Cells(21).Text.Trim

            If (GridView1.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtPostStatus.Text = ""
            Else
                txtPostStatus.Text = GridView1.SelectedRow.Cells(2).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(4).Text = "&nbsp;") Then
                txtInvoiceNo.Text = ""
            Else
                txtInvoiceNo.Text = GridView1.SelectedRow.Cells(4).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(5).Text = "&nbsp;") Then
                txtInvoiceDate.Text = ""
            Else
                txtInvoiceDate.Text = GridView1.SelectedRow.Cells(5).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(6).Text = "&nbsp;") Then
                txtBillingPeriod.Text = ""
            Else
                txtBillingPeriod.Text = GridView1.SelectedRow.Cells(6).Text.Trim
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
                txtAccountType.Text = ""
            Else
                txtAccountType.Text = GridView1.SelectedRow.Cells(9).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(10).Text = "&nbsp;") Then
                txtAccountName.Text = ""
            Else
                txtAccountName.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(10).Text.Trim)
            End If


            If (GridView1.SelectedRow.Cells(11).Text = "&nbsp;") Then
                txtBillAddress.Text = ""
            Else
                txtBillAddress.Text = GridView1.SelectedRow.Cells(11).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(12).Text = "&nbsp;") Then
                txtBillStreet.Text = ""
            Else
                txtBillStreet.Text = GridView1.SelectedRow.Cells(12).Text.Trim
            End If


            If (GridView1.SelectedRow.Cells(13).Text = "&nbsp;") Then
                txtBillBuilding.Text = ""
            Else
                txtBillBuilding.Text = GridView1.SelectedRow.Cells(13).Text.Trim
            End If


            If (GridView1.SelectedRow.Cells(14).Text = "&nbsp;") Then
                txtBillPostal.Text = ""
            Else
                txtBillPostal.Text = GridView1.SelectedRow.Cells(14).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(15).Text = "&nbsp;") Then
                ddlSalesmanBilling.SelectedIndex = 0
            Else
                ddlSalesmanBilling.Text = GridView1.SelectedRow.Cells(15).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(16).Text = "&nbsp;") Then
                txtInvoiceAmount.Text = 0
            Else
                txtInvoiceAmount.Text = GridView1.SelectedRow.Cells(16).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(23).Text = "&nbsp;") Then
                txtBillCountry.Text = ""
            Else
                txtBillCountry.Text = GridView1.SelectedRow.Cells(23).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(24).Text = "&nbsp;") Then
                txtPONo.Text = ""
            Else
                txtPONo.Text = GridView1.SelectedRow.Cells(24).Text.Trim
            End If


            If (GridView1.SelectedRow.Cells(25).Text = "&nbsp;") Then
                txtOurReference.Text = ""
            Else
                txtOurReference.Text = GridView1.SelectedRow.Cells(25).Text.Trim
            End If


            If (GridView1.SelectedRow.Cells(26).Text = "&nbsp;") Then
                txtYourReference.Text = ""
            Else
                txtYourReference.Text = GridView1.SelectedRow.Cells(26).Text.Trim
            End If


            If (GridView1.SelectedRow.Cells(27).Text = "&nbsp;") Then
                ddlCreditTerms.SelectedIndex = 0
            Else
                ddlCreditTerms.Text = GridView1.SelectedRow.Cells(27).Text.Trim
            End If



            'If (GridView1.SelectedRow.Cells(28).Text = "&nbsp;") Then
            '    txtDiscountAmount.Text = 0
            'Else
            '    txtDiscountAmount.Text = GridView1.SelectedRow.Cells(28).Text.Trim
            'End If

            'If (GridView1.SelectedRow.Cells(33).Text = "&nbsp;") Then
            '    txtAmountWithDiscount.Text = 0
            'Else
            '    txtAmountWithDiscount.Text = GridView1.SelectedRow.Cells(33).Text.Trim
            'End If

            If (GridView1.SelectedRow.Cells(29).Text = "&nbsp;") Then
                txtGSTAmount.Text = 0
            Else
                txtGSTAmount.Text = GridView1.SelectedRow.Cells(29).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(30).Text = "&nbsp;") Then
                txtNetAmount.Text = 0
            Else
                txtNetAmount.Text = GridView1.SelectedRow.Cells(30).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(31).Text = "&nbsp;") Then
                txtBatchNo.Text = ""
            Else
                txtBatchNo.Text = GridView1.SelectedRow.Cells(31).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(32).Text = "&nbsp;") Then
                txtComments.Text = ""
            Else
                txtComments.Text = GridView1.SelectedRow.Cells(32).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(34).Text = "&nbsp;") Then
                txtRetentionAmount.Text = ""
            Else
                txtRetentionAmount.Text = GridView1.SelectedRow.Cells(34).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(35).Text = "&nbsp;") Then
                txtRetentionGSTAmount.Text = ""
            Else
                txtRetentionGSTAmount.Text = GridView1.SelectedRow.Cells(35).Text.Trim
            End If

            If (GridView1.SelectedRow.Cells(36).Text = "N") Then
                chkRecurringInvoice.Checked = False
            Else
                chkRecurringInvoice.Checked = True
            End If

            'txtRcnoServiceRecord.Text = lblid14.Text
            'txtRcnoServiceRecordDetail.Text = lblid15.Text
            'txtContractNo.Text = lblid16.Text
            'txtRcnoInvoice.Text = lblid17.Text
            'txtRowSelected.Text = rowindex1.ToString
            'txtRcnoservicebillingdetail.Text = lblid18.Text


            ''''''''''''''''''''''''''''''''''''''
            populateGLCodes()

            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()
            'Dim sql As String
            'sql = ""

            'Dim command21 As MySqlCommand = New MySqlCommand

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

            ''Dim command1 As MySqlCommand = New MySqlCommand
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

            'conn.Close()

            ''''''''''''''''''''''''''''''''''''''''

            PopulateServiceGrid()
            CalculateSumAmounts()
            DisplayGLGrid()


            If txtPostStatus.Text = "P" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                btnCopy.Enabled = True
                btnChangeStatus.Enabled = True
                btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Gray
                btnPrint.Enabled = True
                btnPrintCertificate.Enabled = True
                btnPost.Enabled = False
                'btnDelete.Enabled = True
                grvServiceRecDetails.Enabled = True
            Else
                btnEdit.Enabled = True
                btnEdit.ForeColor = System.Drawing.Color.Black
                btnCopy.Enabled = True
                btnChangeStatus.Enabled = True
                btnDelete.Enabled = True
                btnDelete.ForeColor = System.Drawing.Color.Black
                btnPrint.Enabled = True
                btnPrintCertificate.Enabled = True
                btnPost.Enabled = True
                'btnDelete.Enabled = True
            End If

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
            If Date.TryParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
                strdate = d.ToShortDateString
            End If


            lPrefix = Format(CDate(strdate), "yyyyMM")
            lInvoiceNo = "ARIN" + lPrefix + "-"
            lMonth = Right(lPrefix, 2)
            lYear = Left(lPrefix, 4)



            lPrefix = "ARIN" + lYear
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
            txtInvoiceNo.Text = lInvoiceNo + lSuffix
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

            txtAccountId.Text = ""
            txtClientName.Text = ""
            txtLocationId.Text = ""
            ddlContractNo.SelectedIndex = 0
            txtRowSelected.Text = "0"

            txtSearch1Status.Text = "O"
            txtBatchNo.Text = ""
            txtMode.Text = "NEW"
            txtRcno.Text = "0"
            Label43.Text = "SERVICE BILLING DETAILS"
            btnEdit.Enabled = False
            btnCopy.Enabled = False
            btnChangeStatus.Enabled = False
            btnDelete.Enabled = False
            btnPrint.Enabled = False
            btnPrintCertificate.Enabled = False
            btnPost.Enabled = False
            btnDelete.Enabled = False
            updPnlMsg.Update()

            DisableControls()

            FirstGridViewRowBillingDetailsRecs()
            FirstGridViewRowServiceRecs()

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimeinvoice();", True)
            updpnlServiceRecs.Update()
            updPnlBillingRecs.Update()
            updPnlMsg.Update()
            UpdatePanel1.Update()
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

        txtInvoiceNo.Enabled = False
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
        'txtDiscountAmount.Enabled = False
        'txtAmountWithDiscount.Enabled = False
        txtGSTAmount.Enabled = False
        txtNetAmount.Enabled = False
        txtOurReference.Enabled = False
        txtYourReference.Enabled = False
        txtComments.Enabled = False
        txtCreditDays.Enabled = False

        btnSaveInvoice.Enabled = False
        btnSave.Enabled = False
        btnShowRecords.Enabled = False

        grvBillingDetails.Enabled = False
        grvServiceRecDetails.Enabled = False


        ddlContactType.Enabled = False
        txtAccountId.Enabled = False
        'txtContractNo.Enabled = False
        txtClientName.Enabled = False
        txtLocationId.Enabled = False
        'ddlContractGrp.Enabled = False
        ddlCompanyGrp.Enabled = False
        ddlContractNo.Enabled = False
        txtDateFrom.Enabled = False
        txtDateTo.Enabled = False
        btnDelete.Enabled = False

        'rdbAll.Attributes.Add("disabled", "disabled")
        'rdbCompleted.Attributes.Add("readonly", "readonly")
        'rdbNotCompleted.Attributes.Add("readonly", "readonly")
        rdbAll.Enabled = False
        rdbCompleted.Enabled = False
        rdbNotCompleted.Enabled = False
        btnClient.Visible = False
        BtnLocation.Visible = False

        ddlServiceFrequency.Enabled = False
        ddlBillingFrequency.Enabled = False
        ddlContractGroup.Enabled = False
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

        txtAccountIdBilling.Enabled = True


        txtInvoiceNo.Enabled = True
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
        'txtDiscountAmount.Enabled = True
        'txtAmountWithDiscount.Enabled = True
        txtGSTAmount.Enabled = True
        txtNetAmount.Enabled = True
        txtOurReference.Enabled = True
        txtYourReference.Enabled = True
        txtComments.Enabled = True
        txtCreditDays.Enabled = True

        'btnSaveInvoice.Enabled = True
        'btnSave.Enabled = True
        btnShowRecords.Enabled = True

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

        grvBillingDetails.Enabled = True
        grvServiceRecDetails.Enabled = True
        updPnlBillingRecs.Update()
        updpnlServiceRecs.Update()
        updpnlBillingDetails.Update()
        updPanelSave.Update()
        UpdatePanel1.Update()
        btnClient.Visible = True
        BtnLocation.Visible = True

        ddlServiceFrequency.Enabled = True
        ddlBillingFrequency.Enabled = True
        ddlContractGroup.Enabled = True
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

            txtMode.Text = "NEW"
            lblMessage.Text = "ACTION: ADD RECORD"
            txtBillingPeriod.Text = Year(Convert.ToDateTime(txtInvoiceDate.Text)) & Format(Month(Convert.ToDateTime(txtInvoiceDate.Text)), "00")


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

  

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblAlert.Text = ""
        Try
            If txtPostStatus.Text = "P" Then
                lblAlert.Text = "Invoice has already been POSTED.. Cannot be DELETED"
                'Dim message1 As String = "alert('Contract has already been POSTED.. Cannot be DELETED!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message1, True)

                Exit Sub
            End If

            If txtPostStatus.Text = "V" Then
                lblAlert.Text = "Invoice is VOID.. Cannot be DELETED"
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

                Dim qry1 As String = "DELETE from tblSales where Rcno= @Rcno "

                command1.CommandText = qry1
                command1.Parameters.Clear()

                command1.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                command1.Connection = conn
                command1.ExecuteNonQuery()


                'Dim command2 As MySqlCommand = New MySqlCommand
                'command2.CommandType = CommandType.Text

                'Dim qry2 As String = "DELETE from tblSalesDetail where InvoiceNumber= @InvoiceNumber "

                'command2.CommandText = qry2
                'command2.Parameters.Clear()

                'command2.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                'command2.Connection = conn
                'command2.ExecuteNonQuery()



                Dim command3 As MySqlCommand = New MySqlCommand
                command3.CommandType = CommandType.Text

                Dim qry3 As String = "DELETE from tblservicebillingdetail where RcnoInvoice= @RcnoInvoice "

                command3.CommandText = qry3
                command3.Parameters.Clear()

                command3.Parameters.AddWithValue("@RcnoInvoice", txtRcno.Text)
                command3.Connection = conn
                command3.ExecuteNonQuery()


                Dim command4 As MySqlCommand = New MySqlCommand
                command4.CommandType = CommandType.Text

                Dim qry4 As String = "DELETE from tblservicebillingdetailItem where RcnoInvoice= @RcnoInvoice "

                command4.CommandText = qry4
                command4.Parameters.Clear()

                command4.Parameters.AddWithValue("@RcnoInvoice", txtRcno.Text)
                command4.Connection = conn
                command4.ExecuteNonQuery()


                conn.Close()

                'Dim message As String = "alert('Contract is deleted Successfully!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                lblMessage.Text = "DELETE: INVOICE SUCCESSFULLY DELETED"

                'btnADD_Click(sender, e)

                'txt.Text = "SELECT * From tblContract where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"
                SQLDSInvoice.SelectCommand = txt.Text
                SQLDSInvoice.DataBind()
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
                '    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName,VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, DueDate, GLtype,"
                '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @DueDate, @GLtype,"
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


                'If Convert.ToDecimal(txtRetentionAmount.Text) > 0.0 Then
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
                '    commandAR.Parameters.AddWithValue("@GLCode", txtARCode10.Text)
                '    commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription10.Text)
                '    commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)
                '    commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtRetentionAmount.Text) + Convert.ToDecimal(txtRetentionGSTAmount.Text))
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
                '    commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtGSTAmount.Text) + Convert.ToDecimal(txtRetentionGSTAmount.Text))
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
                commandValues.CommandText = "SELECT *  FROM tblservicebillingdetailitem where  ItemCode <> 'IN-RET' and BatchNo ='" & txtBatchNo.Text.Trim & "'"
                commandValues.Connection = conn

                Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
                Dim dtValues As New DataTable
                dtValues.Load(drValues)



                For Each row As DataRow In dtValues.Rows

                    'If Convert.ToDecimal(row("PriceWithDisc")) > 0.0 Then
                    If Convert.ToDecimal(row("TotalPrice")) > 0.0 Then
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
                        'commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtRetentionAmount.Text))
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

        'btnSave.Enabled = True
        'btnSave.ForeColor = System.Drawing.Color.Black
        'btnCancel.Enabled = True
        'btnCancel.ForeColor = System.Drawing.Color.Black
        'btnClient.Visible = True
        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black

        'btnDelete.Enabled = False
        'btnDelete.ForeColor = System.Drawing.Color.Gray



        'rdbNotCompleted.Enabled = True
        'rdbCompleted.Enabled = True
        'rdbAll.Enabled = True
        'ddlCompanyGrp.Enabled = True
        'ddlContactType.Enabled = True
        'txtClientName.Enabled = True
        'txtAccountId.Enabled = True
        'txtLocationId.Enabled = True
        'ddlContractNo.Enabled = True
        'txtDateFrom.Enabled = True
        'txtDateTo.Enabled = True

        'txtAccountIdBilling.Enabled = True


        txtInvoiceNo.Enabled = True
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
        'txtDiscountAmount.Enabled = True
        'txtAmountWithDiscount.Enabled = True
        txtGSTAmount.Enabled = True
        txtNetAmount.Enabled = True
        txtOurReference.Enabled = True
        txtYourReference.Enabled = True
        txtComments.Enabled = True
        'btnSaveInvoice.Enabled = True
        btnSave.Enabled = True
        btnShowRecords.Enabled = True
        btnSaveInvoice.Enabled = True
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

        'grvBillingDetails.Enabled = True
        grvServiceRecDetails.Enabled = True
        updPnlBillingRecs.Update()
        updpnlServiceRecs.Update()
        updpnlBillingDetails.Update()
        updPanelSave.Update()
        'UpdatePanel1.Update()

        'EnableControls()

        'UpdatePanel1.Update()
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
    End Sub




    Protected Sub btnQuickSearch_Click(sender As Object, e As EventArgs) Handles btnQuickSearch.Click
        Try
            Dim strsql As String

           
            strsql = "Select PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal,  "
            strsql = strsql & " AppliedBase, Salesman, PoNo, OurRef, yourRef, CreditTerms, DiscountAmount, GSTAmount, NetAmount, GLPeriod, CompanyGroup, ContactType, BatchNo, Salesman, Comments, AmountWithDiscount , RetentionAmount, RetentionGST, STBilling, RecurringInvoice,  CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno from tblSales where 1=1 and STBilling = 'Y' "

            If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
                Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                For Each str As String In stringList
                    str = "'" + str + "'"
                    YrStrList.Add(str.ToUpper)
                Next

                Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                strsql = strsql + " and GlStatus in (" + YrStr + ")"

            End If


            If String.IsNullOrEmpty(txtBillingPeriodSearch.Text) = False Then
                strsql = strsql & " and GLPeriod like '%" & txtBillingPeriodSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtInvoicenoSearch.Text) = False Then
                strsql = strsql & " and InvoiceNumber like '%" & txtInvoicenoSearch.Text.Trim + "%'"
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

            If (ddlSalesmanSearch.SelectedIndex > 0) Then
                strsql = strsql & " and Salesman like '%" & ddlSalesmanSearch.Text.Trim + "%'"
            End If

            strsql = strsql + " order by custname;"
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

        FirstGridViewRowServiceRecs()
        PopulateServiceGrid()
        updpnlServiceRecs.Update()
        'updPnlBillingRecs.Update()
        updPanelInvoice.Update()

        'btnSave.Enabled = True
        'updPanelSave.Update()
    End Sub

    Private Sub PopulateServiceGrid()

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
            conn.Open()

            Dim sql As String
            sql = ""

            If txtMode.Text = "VIEW" Then

                sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.Name,  A.LocationId, A.Address1, "
                sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef,  A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup,"
                sql = sql + " A.Salesman, A.BillAddress1, A.BillBuilding, A.BillStreet, A.BillPostal, A.BillCity, A.BillCountry, "
                sql = sql + " A.BillingFrequency, A.RcnoInvoice, A.Rcno as rcnotblservicebillingdetail, A.ContractGroup, RetentionAmt "
                sql = sql + " FROM tblservicebillingdetail A "
                sql = sql + " where 1 = 1 and RcnoInvoice =" & txtRcno.Text

            Else
                If ddlContactType.SelectedIndex = 0 Then
                    'sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                    'sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, "
                    'sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                    'sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup, C.RetentionPerc  "
                    'sql = sql + " FROM tblservicerecord A, tblCompany B, tblContract C "
                    'sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and  A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and C.ConTractGroup = 'ST' and A.ContactType = '" & ddlContactType.Text.Trim & "'"
                    If ddlServiceFrequency.Text.Trim = "-1" Then
                        sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                        sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, "
                        sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                        sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup, C.RetentionPerc  "
                        sql = sql + " FROM tblservicerecord A, tblCompany B, tblContract C "
                        sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and  A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and C.ConTractGroup = 'ST' and A.ContactType = '" & ddlContactType.Text.Trim & "'"
                    Else
                        sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                        sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, "
                        sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                        sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup, C.RetentionPerc  "
                        sql = sql + " FROM tblservicerecord A, tblCompany B, tblContract C, tblcontractdet D "
                        sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and  A.AccountId = B.AccountId and C.ContractNo = D.ContractNo  and A.ContractNo = C.ContractNo and C.ConTractGroup = 'ST' and A.ContactType = '" & ddlContactType.Text.Trim & "'"
                    End If

                ElseIf ddlContactType.SelectedIndex = 1 Then
                    'sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                    'sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, "
                    'sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                    'sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup, C.RetentionPerc  "
                    'sql = sql + " FROM tblservicerecord A, tblPerson B, tblContract C "
                    'sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and  A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and C.ConTractGroup = 'ST' and A.ContactType = '" & ddlContactType.Text.Trim & "'"
                    If ddlServiceFrequency.Text.Trim = "-1" Then
                        sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                        sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, "
                        sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                        sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup, C.RetentionPerc  "
                        sql = sql + " FROM tblservicerecord A, tblPerson B, tblContract C "
                        sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and  A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and C.ConTractGroup = 'ST' and A.ContactType = '" & ddlContactType.Text.Trim & "'"
                    Else
                        sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                        sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, "
                        sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                        sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup, C.RetentionPerc  "
                        sql = sql + " FROM tblservicerecord A, tblPerson B, tblContract C, tblcontractdet D "
                        sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and  A.AccountId = B.AccountId and C.ContractNo = D.ContractNo  and A.ContractNo = C.ContractNo and C.ConTractGroup = 'ST' and A.ContactType = '" & ddlContactType.Text.Trim & "'"
                    End If
                End If


                If String.IsNullOrEmpty(txtAccountId.Text) = False Then
                    sql = sql + " and  A.AccountID like '%" & txtAccountId.Text & "%'"
                End If

                If String.IsNullOrEmpty(txtClientName.Text) = False Then
                    sql = sql + " and  A.ServiceName like '%" & txtClientName.Text & "%'"
                End If

                If ddlCompanyGrp.Text.Trim <> "-1" Then
                    sql = sql + " and   A.CompanyGroup = '" & ddlCompanyGrp.Text.Trim & "'"
                End If

                If ddlContractNo.Text.Trim <> "-1" Then
                    sql = sql + " and   A.ContractNo = '" & ddlContractNo.Text & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text) = False Then
                    sql = sql + " and   A.ServiceDate >= '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.ServiceDate <= '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text) = False And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.ServiceDate between '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If


                If String.IsNullOrEmpty(txtLocationId.Text) = False Then
                    sql = sql + " and   A.LocationId = '" & txtLocationId.Text & "'"
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

            End If
            'sql = sql + " order by A.AccountID, A.ContractNo, A.ServiceDate"


            If rdbGrouping.SelectedIndex = 0 Then
                sql = sql + " order by A.ContractNo, A.ServiceDate"
            ElseIf rdbGrouping.SelectedIndex = 1 Then
                sql = sql + " order by A.AccountID, A.LocationId"
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

                    Dim TextBoxServiceAddress As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceAddressGV"), TextBox)

                    If txtMode.Text = "VIEW" Then
                        TextBoxServiceAddress.Text = Convert.ToString(dt.Rows(rowIndex)("Address1"))
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

                    End If

                    Dim TextBoxLocationId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)
                    TextBoxLocationId.Text = Convert.ToString(dt.Rows(rowIndex)("LocationId"))

                    Dim TextBoxToBillAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtToBillAmtGV"), TextBox)

                    If txtMode.Text = "VIEW" Then
                        TextBoxToBillAmt.Text = Convert.ToString(Convert.ToDecimal(dt.Rows(rowIndex)("BillAmount")))
                    Else
                        TextBoxToBillAmt.Text = Convert.ToString(Convert.ToDecimal(dt.Rows(rowIndex)("BillAmount")) - (Convert.ToDecimal(dt.Rows(rowIndex)("BillAmount")) * Convert.ToDecimal(dt.Rows(rowIndex)("RetentionPerc")) * 0.01))
                    End If



                    Dim TextBoxRetentionAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRetentionAmtGV"), TextBox)

                    If txtMode.Text = "VIEW" Then
                        TextBoxRetentionAmt.Text = Convert.ToString(Convert.ToDecimal(dt.Rows(rowIndex)("RetentionAmt")))
                    Else
                        TextBoxRetentionAmt.Text = Convert.ToString(Convert.ToDecimal(dt.Rows(rowIndex)("BillAmount")) - (Convert.ToDecimal(TextBoxToBillAmt.Text)))
                    End If
                    

                    Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoServiceRecordGV"), TextBox)
                    TextBoxRcnoServiceRecord.Text = Convert.ToString(dt.Rows(rowIndex)("Rcno"))

                    Dim TextBoxName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountNameGV"), TextBox)
                    TextBoxName.Text = Convert.ToString(dt.Rows(rowIndex)("Name"))

                    Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillAddress1GV"), TextBox)
                    TextBoxBillAddress1.Text = Convert.ToString(dt.Rows(rowIndex)("BillAddress1"))

                    Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillBuildingGV"), TextBox)
                    TextBoxBillBuilding.Text = Convert.ToString(dt.Rows(rowIndex)("BillBuilding"))

                    Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillStreetGV"), TextBox)
                    TextBoxBillStreet.Text = Convert.ToString(dt.Rows(rowIndex)("BillStreet"))

                    Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillCountryGV"), TextBox)
                    TextBoxBillCountry.Text = Convert.ToString(dt.Rows(rowIndex)("BillCountry"))

                    Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillPostalGV"), TextBox)
                    TextBoxBillPostal.Text = Convert.ToString(dt.Rows(rowIndex)("BillPostal"))

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
            'End If
            Label43.Text = "SERVICE BILLING DETAILS : Total Records :" & grvServiceRecDetails.Rows.Count.ToString
            updpnlServiceRecs.Update()
            'txtClientName.Text = sql

            Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

            If txtMode.Text = "VIEW" Then

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
            dt.Columns.Add(New DataColumn("RetentionAmt", GetType(String)))
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
            dr("RetentionAmt") = 0
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

            dt.Rows.Add(dr)

            ViewState("CurrentTableServiceRec") = dt

            grvServiceRecDetails.DataSource = dt
            grvServiceRecDetails.DataBind()

            'Dim btnAdd As Button = CType(grvServiceRecDetails.FooterRow.Cells(1).FindControl("btnViewEdit"), Button)
            'Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            Throw ex
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
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(9).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContactTypeGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtCompanyGroupGV"), TextBox)

                        Dim TextBoxAccountName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(13).FindControl("txtAccountNameGV"), TextBox)
                        Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(14).FindControl("txtBillAddress1GV"), TextBox)
                        Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(15).FindControl("txtBillBuildingGV"), TextBox)
                        Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(16).FindControl("txtBillStreetGV"), TextBox)
                        Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(17).FindControl("txtBillCountryGV"), TextBox)
                        Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(18).FindControl("txtBillPostalGV"), TextBox)
                        Dim TextBoxOurReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(19).FindControl("txtOurReferenceGV"), TextBox)
                        Dim TextBoxYourReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(20).FindControl("txtYourReferenceGV"), TextBox)
                        Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(21).FindControl("txtPoNoGV"), TextBox)
                        Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(22).FindControl("txtCreditTermsGV"), TextBox)
                        Dim TextBoxSalesman As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(23).FindControl("txtSalesmanGV"), TextBox)
                        Dim TextBoxRcnoServicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(24).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                        Dim TextBoxDept As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtDeptGV"), TextBox)
                        Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtStatusGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxRetentionAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(25).FindControl("txtRetentionAmtGV"), TextBox)
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
                        dtCurrentTable.Rows(i - 1)("RetentionAmt") = TextBoxRetentionAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ContactType") = TextBoxContactType.Text
                        dtCurrentTable.Rows(i - 1)("CompanyGroup") = TextBoxCompanyGroup.Text

                        dtCurrentTable.Rows(i - 1)("AccountName") = TextBoxAccountName.Text
                        dtCurrentTable.Rows(i - 1)("BillAddress1") = TextBoxBillAddress1.Text
                        dtCurrentTable.Rows(i - 1)("BillBuilding") = TextBoxBillBuilding.Text
                        dtCurrentTable.Rows(i - 1)("BillStreet") = TextBoxBillStreet.Text
                        dtCurrentTable.Rows(i - 1)("BillCountry") = TextBoxBillCountry.Text
                        dtCurrentTable.Rows(i - 1)("BillPostal") = TextBoxBillPostal.Text
                        dtCurrentTable.Rows(i - 1)("OurReference") = TextBoxOurReference.Text
                        dtCurrentTable.Rows(i - 1)("YourReference") = TextBoxYourReference.Text
                        dtCurrentTable.Rows(i - 1)("PoNo") = TextBoxPoNo.Text
                        dtCurrentTable.Rows(i - 1)("CreditTerms") = TextBoxCreditTerms.Text
                        dtCurrentTable.Rows(i - 1)("Salesman") = TextBoxSalesman.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServicebillingdetail") = TextBoxRcnoServicebillingdetail.Text

                        dtCurrentTable.Rows(i - 1)("Dept") = TextBoxDept.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("Status") = TextBoxStatus.Text



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
            Throw ex
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
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(9).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContactTypeGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtCompanyGroupGV"), TextBox)

                        Dim TextBoxAccountName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(13).FindControl("txtAccountNameGV"), TextBox)
                        Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(14).FindControl("txtBillAddress1GV"), TextBox)
                        Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(15).FindControl("txtBillBuildingGV"), TextBox)
                        Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(16).FindControl("txtBillStreetGV"), TextBox)
                        Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(17).FindControl("txtBillCountryGV"), TextBox)
                        Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(18).FindControl("txtBillPostalGV"), TextBox)
                        Dim TextBoxOurReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(19).FindControl("txtOurReferenceGV"), TextBox)
                        Dim TextBoxYourReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(20).FindControl("txtYourReferenceGV"), TextBox)
                        Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(21).FindControl("txtPoNoGV"), TextBox)
                        Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(22).FindControl("txtCreditTermsGV"), TextBox)
                        Dim TextBoxSalesman As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(23).FindControl("txtSalesmanGV"), TextBox)
                        Dim TextBoxRcnoServicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(24).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                        Dim TextBoxDept As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtDeptGV"), TextBox)
                        Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtStatusGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxRetentionAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(25).FindControl("txtRetentionAmtGV"), TextBox)

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
                        dtCurrentTable.Rows(i - 1)("RetentionAmt") = TextBoxRetentionAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ContactType") = TextBoxContactType.Text
                        dtCurrentTable.Rows(i - 1)("CompanyGroup") = TextBoxCompanyGroup.Text

                        dtCurrentTable.Rows(i - 1)("AccountName") = TextBoxAccountName.Text
                        dtCurrentTable.Rows(i - 1)("BillAddress1") = TextBoxBillAddress1.Text
                        dtCurrentTable.Rows(i - 1)("BillBuilding") = TextBoxBillBuilding.Text
                        dtCurrentTable.Rows(i - 1)("BillStreet") = TextBoxBillStreet.Text
                        dtCurrentTable.Rows(i - 1)("BillCountry") = TextBoxBillCountry.Text
                        dtCurrentTable.Rows(i - 1)("BillPostal") = TextBoxBillPostal.Text
                        dtCurrentTable.Rows(i - 1)("OurReference") = TextBoxOurReference.Text
                        dtCurrentTable.Rows(i - 1)("YourReference") = TextBoxYourReference.Text
                        dtCurrentTable.Rows(i - 1)("PoNo") = TextBoxPoNo.Text
                        dtCurrentTable.Rows(i - 1)("CreditTerms") = TextBoxCreditTerms.Text
                        dtCurrentTable.Rows(i - 1)("Salesman") = TextBoxSalesman.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServicebillingdetail") = TextBoxRcnoServicebillingdetail.Text

                        dtCurrentTable.Rows(i - 1)("Dept") = TextBoxDept.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("Status") = TextBoxStatus.Text
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
            Throw ex
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
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(9).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContactTypeGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtCompanyGroupGV"), TextBox)

                        Dim TextBoxAccountName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(13).FindControl("txtAccountNameGV"), TextBox)
                        Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(14).FindControl("txtBillAddress1GV"), TextBox)
                        Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(15).FindControl("txtBillBuildingGV"), TextBox)
                        Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(16).FindControl("txtBillStreetGV"), TextBox)
                        Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(17).FindControl("txtBillCountryGV"), TextBox)
                        Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(18).FindControl("txtBillPostalGV"), TextBox)
                        Dim TextBoxOurReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(19).FindControl("txtOurReferenceGV"), TextBox)
                        Dim TextBoxYourReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(20).FindControl("txtYourReferenceGV"), TextBox)
                        Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(21).FindControl("txtPoNoGV"), TextBox)
                        Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(22).FindControl("txtCreditTermsGV"), TextBox)
                        Dim TextBoxSalesman As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(23).FindControl("txtSalesmanGV"), TextBox)
                        Dim TextBoxRcnoServicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(24).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                        Dim TextBoxDept As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtDeptGV"), TextBox)
                        Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtStatusGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxRetentionAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(25).FindControl("txtRetentionAmtGV"), TextBox)

                        TextBoxSelect.Text = dt.Rows(i)("SelRec").ToString()
                        TextBoxAccountId.Text = dt.Rows(i)("AccountId").ToString()
                        TextBoxClientName.Text = dt.Rows(i)("ClientName").ToString()
                        TextBoxServiceRecordNo.Text = dt.Rows(i)("ServiceRecordNo").ToString()
                        TextBoxServiceDate.Text = dt.Rows(i)("ServiceDate").ToString()
                        TextBoxBillingFrequency.Text = dt.Rows(i)("BillingFrequency").ToString()
                        TextBoxLocationId.Text = dt.Rows(i)("LocationId").ToString()
                        TextBoxServiceAddress.Text = dt.Rows(i)("ServiceAddress").ToString()
                        TextBoxToBillAmt.Text = dt.Rows(i)("ToBillAmt").ToString()
                        TextBoxRetentionAmt.Text = dt.Rows(i)("RetentionAmt").ToString()

                        TextBoxRcnoServiceRecord.Text = dt.Rows(i)("RcnoServiceRecord").ToString()
                        TextBoxRcnoServiceRecordDetail.Text = dt.Rows(i)("RcnoServiceRecordDetail").ToString()
                        TextBoxRcnoInvoice.Text = dt.Rows(i)("RcnoInvoice").ToString()
                        TextBoxContactType.Text = dt.Rows(i)("ContactType").ToString()
                        TextBoxCompanyGroup.Text = dt.Rows(i)("CompanyGroup").ToString()

                        TextBoxAccountName.Text = dt.Rows(i)("AccountName").ToString()

                        TextBoxBillAddress1.Text = dt.Rows(i)("BillAddress1").ToString()
                        TextBoxBillBuilding.Text = dt.Rows(i)("BillBuilding").ToString()
                        TextBoxBillStreet.Text = dt.Rows(i)("BillStreet").ToString()
                        TextBoxBillCountry.Text = dt.Rows(i)("BillCountry").ToString()
                        TextBoxBillPostal.Text = dt.Rows(i)("BillPostal").ToString()
                        TextBoxOurReference.Text = dt.Rows(i)("OurReference").ToString()
                        TextBoxYourReference.Text = dt.Rows(i)("YourReference").ToString()
                        TextBoxPoNo.Text = dt.Rows(i)("PoNo").ToString()
                        TextBoxCreditTerms.Text = dt.Rows(i)("CreditTerms").ToString()
                        TextBoxSalesman.Text = dt.Rows(i)("Salesman").ToString()
                        TextBoxRcnoServicebillingdetail.Text = dt.Rows(i)("RcnoServicebillingdetail").ToString()

                        TextBoxDept.Text = dt.Rows(i)("Dept").ToString()
                        TextBoxContractNo.Text = dt.Rows(i)("ContractNo").ToString()
                        TextBoxStatus.Text = dt.Rows(i)("Status").ToString()


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
            Throw ex
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
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(9).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContactTypeGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtCompanyGroupGV"), TextBox)

                        Dim TextBoxAccountName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(13).FindControl("txtAccountNameGV"), TextBox)
                        Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(14).FindControl("txtBillAddress1GV"), TextBox)
                        Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(15).FindControl("txtBillBuildingGV"), TextBox)
                        Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(16).FindControl("txtBillStreetGV"), TextBox)
                        Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(17).FindControl("txtBillCountryGV"), TextBox)
                        Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(18).FindControl("txtBillPostalGV"), TextBox)
                        Dim TextBoxOurReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(19).FindControl("txtOurReferenceGV"), TextBox)
                        Dim TextBoxYourReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(20).FindControl("txtYourReferenceGV"), TextBox)
                        Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(21).FindControl("txtPoNoGV"), TextBox)
                        Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(22).FindControl("txtCreditTermsGV"), TextBox)
                        Dim TextBoxSalesman As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(23).FindControl("txtSalesmanGV"), TextBox)
                        Dim TextBoxRcnoServicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(24).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                        Dim TextBoxDept As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtDeptGV"), TextBox)
                        Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtStatusGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxRetentionAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(25).FindControl("txtRetentionAmtGV"), TextBox)

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
                        dtCurrentTable.Rows(i - 1)("RetentionAmt") = TextBoxRetentionAmt.Text

                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ContactType") = TextBoxContactType.Text
                        dtCurrentTable.Rows(i - 1)("CompanyGroup") = TextBoxCompanyGroup.Text

                        dtCurrentTable.Rows(i - 1)("AccountName") = TextBoxAccountName.Text
                        dtCurrentTable.Rows(i - 1)("BillAddress1") = TextBoxBillAddress1.Text
                        dtCurrentTable.Rows(i - 1)("BillBuilding") = TextBoxBillBuilding.Text
                        dtCurrentTable.Rows(i - 1)("BillStreet") = TextBoxBillStreet.Text
                        dtCurrentTable.Rows(i - 1)("BillCountry") = TextBoxBillCountry.Text
                        dtCurrentTable.Rows(i - 1)("BillPostal") = TextBoxBillPostal.Text
                        dtCurrentTable.Rows(i - 1)("OurReference") = TextBoxOurReference.Text
                        dtCurrentTable.Rows(i - 1)("YourReference") = TextBoxYourReference.Text
                        dtCurrentTable.Rows(i - 1)("PoNo") = TextBoxPoNo.Text
                        dtCurrentTable.Rows(i - 1)("CreditTerms") = TextBoxCreditTerms.Text
                        dtCurrentTable.Rows(i - 1)("Salesman") = TextBoxSalesman.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServicebillingdetail") = TextBoxRcnoServicebillingdetail.Text

                        dtCurrentTable.Rows(i - 1)("Dept") = TextBoxDept.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("Status") = TextBoxStatus.Text
                        rowIndex += 1
                    Next i

                    ViewState("CurrentTableServiceRec") = dtCurrentTable


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataServiceRecs()
        Catch ex As Exception
            Throw ex
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

            'dt.Columns.Add(New DataColumn("DiscPerc", GetType(String)))
            'dt.Columns.Add(New DataColumn("DiscAmount", GetType(String)))
            'dt.Columns.Add(New DataColumn("PriceWithDisc", GetType(String)))

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

            dt.Columns.Add(New DataColumn("RetentionAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalRetentionAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("RetentionGSTAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("RetentionWithGST", GetType(String)))
            dt.Columns.Add(New DataColumn("ServiceRecordNo", GetType(String)))
            dr = dt.NewRow()

            dr("ItemType") = String.Empty
            dr("ItemCode") = String.Empty
            dr("ItemDescription") = String.Empty
            dr("UOM") = String.Empty
            dr("Qty") = 0
            dr("PricePerUOM") = 0.0
            dr("TotalPrice") = 0

            'dr("DiscPerc") = 0.0
            'dr("DiscAmount") = 0
            'dr("PriceWithDisc") = 0

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

            dr("RetentionAmt") = 0
            dr("TotalRetentionAmt") = 0
            dr("RetentionGSTAmt") = 0
            dr("RetentionWithGST") = 0
            dr("ServiceRecordNo") = String.Empty
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtUOMGV"), DropDownList)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtPricePerUOMGV"), TextBox)
                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalPriceGV"), TextBox)

                        'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtDiscPercGV"), TextBox)
                        'Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtPriceWithDiscGV"), TextBox)

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtContractNoGV"), TextBox)

                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtServiceStatusGV"), TextBox)
                        Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtTaxTypeGV"), DropDownList)

                        Dim TextBoxRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRetentionAmtGV"), TextBox)
                        Dim TextBoxRetentionGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(20).FindControl("txtRetentionGSTAmtGV"), TextBox)
                        Dim TextBoxTotalRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(21).FindControl("txtTotalRetentionGV"), TextBox)
                        Dim TextBoxRetentionWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtRetentionWithGSTGV"), TextBox)
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

                        'dtCurrentTable.Rows(i - 1)("DiscPerc") = TextBoxDiscPerc.Text
                        'dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        'dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
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

                        dtCurrentTable.Rows(i - 1)("RetentionAmt") = TextBoxRetentionAmt.Text
                        dtCurrentTable.Rows(i - 1)("RetentionGSTAmt") = TextBoxRetentionGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalRetentionAmt") = TextBoxTotalRetentionAmt.Text
                        dtCurrentTable.Rows(i - 1)("RetentionWithGST") = TextBoxRetentionWithGST.Text
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
                        'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                        Query = "Select * from tblbillingproducts "
                        PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

                        Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        Query = "Select * from tblunitms order by UnitMs"
                        PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM1)


                        'Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                        'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                        'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

                        'Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        'Query = "Select * from tblunitms order by UnitMs"
                        'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM1)


                        Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

                        'Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)



                        'If TextBoxItemType1.Text = "SERVICE" Then
                        '    TextBoxQty1.Enabled = False
                        '    TextBoxQty1.Text = 1
                        '    TextBoxItemCode1.Enabled = False
                        '    TextBoxItemDescription1.Enabled = False
                        'End If

                        'If rowIndex2 = 0 Then
                        '    If TextBoxItemType1.Text = "SERVICE" Then
                        '        TextBoxItemType1.Enabled = False
                        '    End If
                        'End If

                        'If rowIndex2 = 1 Then
                        '    If TextBoxItemType1.Text = "SERVICE" Then
                        '        TextBoxItemType1.Enabled = False
                        '    End If
                        'End If


                        If TextBoxItemType1.Text = "SERVICE" Then
                            TextBoxQty1.Enabled = False
                            TextBoxQty1.Text = 1
                            TextBoxItemCode1.Enabled = False
                            'TextBoxItemDescription1.Enabled = False
                            'TextBoxItemType1.Enabled = False
                        End If

                        If TextBoxItemType1.Text = "SERVICE" And (rowIndex = 0 Or rowIndex = 1) Then
                            'TextBoxQty1.Enabled = False
                            'TextBoxQty1.Text = 1
                            TextBoxItemCode1.Enabled = False
                            TextBoxItemDescription1.Enabled = False
                            'TextBoxItemType1.Enabled = False
                        End If
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtUOMGV"), DropDownList)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtPricePerUOMGV"), TextBox)
                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalPriceGV"), TextBox)

                        'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtDiscPercGV"), TextBox)
                        'Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtPriceWithDiscGV"), TextBox)

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtContractNoGV"), TextBox)

                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtServiceStatusGV"), TextBox)
                        Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtTaxTypeGV"), DropDownList)
                        Dim TextBoxRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRetentionAmtGV"), TextBox)
                        Dim TextBoxRetentionGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(20).FindControl("txtRetentionGSTAmtGV"), TextBox)
                        Dim TextBoxTotalRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(21).FindControl("txtTotalRetentionGV"), TextBox)
                        Dim TextBoxRetentionWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtRetentionWithGSTGV"), TextBox)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtServiceRecordGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("PricePerUOM") = TextBoxPricePerUOM.Text
                        dtCurrentTable.Rows(i - 1)("TotalPrice") = TextBoxTotalPrice.Text

                        'dtCurrentTable.Rows(i - 1)("DiscPerc") = TextBoxDiscPerc.Text
                        'dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        'dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
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

                        dtCurrentTable.Rows(i - 1)("RetentionAmt") = TextBoxRetentionAmt.Text
                        dtCurrentTable.Rows(i - 1)("RetentionGSTAmt") = TextBoxRetentionGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalRetentionAmt") = TextBoxTotalRetentionAmt.Text
                        dtCurrentTable.Rows(i - 1)("RetentionWithGST") = TextBoxRetentionWithGST.Text
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtUOMGV"), DropDownList)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtPricePerUOMGV"), TextBox)
                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalPriceGV"), TextBox)

                        'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtDiscPercGV"), TextBox)
                        'Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtPriceWithDiscGV"), TextBox)

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtContractNoGV"), TextBox)

                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtServiceStatusGV"), TextBox)
                        Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtTaxTypeGV"), DropDownList)
                        Dim TextBoxRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRetentionAmtGV"), TextBox)
                        Dim TextBoxRetentionGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(20).FindControl("txtRetentionGSTAmtGV"), TextBox)
                        Dim TextBoxTotalRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(21).FindControl("txtTotalRetentionGV"), TextBox)
                        Dim TextBoxRetentionWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtRetentionWithGSTGV"), TextBox)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtServiceRecordGV"), TextBox)

                        TextBoxItemType.Text = dt.Rows(i)("ItemType").ToString()
                        TextBoxItemCode.Text = dt.Rows(i)("ItemCode").ToString()
                        TextBoxItemDescription.Text = dt.Rows(i)("ItemDescription").ToString()
                        TextBoxUOM.Text = dt.Rows(i)("UOM").ToString()
                        TextBoxQty.Text = dt.Rows(i)("Qty").ToString()
                        TextBoxPricePerUOM.Text = dt.Rows(i)("PricePerUOM").ToString()
                        TextBoxTotalPrice.Text = dt.Rows(i)("TotalPrice").ToString()

                        'TextBoxDiscPerc.Text = dt.Rows(i)("DiscPerc").ToString()
                        'TextBoxDiscAmount.Text = dt.Rows(i)("DiscAmount").ToString()
                        'TextBoxPriceWithDisc.Text = dt.Rows(i)("PriceWithDisc").ToString()

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

                        TextBoxRetentionAmt.Text = dt.Rows(i)("RetentionAmt").ToString()
                        TextBoxRetentionGSTAmt.Text = dt.Rows(i)("RetentionGSTAmt").ToString()
                        TextBoxTotalRetentionAmt.Text = dt.Rows(i)("TotalRetentionAmt").ToString()
                        TextBoxRetentionWithGST.Text = dt.Rows(i)("RetentionWithGST").ToString()
                        TextBoxServiceRecordNo.Text = dt.Rows(i)("ServiceRecordNo").ToString()

                        Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

                        Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)

                        Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        '''''''''''''

                        'If TextBoxItemType1.Text = "SERVICE" Then
                        '    TextBoxQty1.Enabled = False
                        '    TextBoxQty1.Text = 1
                        '    TextBoxItemCode1.Enabled = False
                        '    TextBoxItemDescription1.Enabled = False
                        'End If

                        'If rowIndex = 0 Then
                        '    If TextBoxItemType1.Text = "SERVICE" Then
                        '        TextBoxItemType1.Enabled = False
                        '    End If
                        'End If

                        'If rowIndex = 1 Then
                        '    If TextBoxItemType1.Text = "SERVICE" Then
                        '        TextBoxItemType1.Enabled = False
                        '    End If
                        'End If

                        If TextBoxItemType1.Text = "SERVICE" Then
                            TextBoxQty1.Enabled = False
                            TextBoxQty1.Text = 1
                            'TextBoxItemCode1.Enabled = False
                            'TextBoxItemDescription1.Enabled = False
                            TextBoxItemType1.Enabled = False
                        End If

                        If TextBoxItemType1.Text = "SERVICE" And (rowIndex = 0 Or rowIndex = 1) Then
                            'TextBoxQty1.Enabled = False
                            'TextBoxQty1.Text = 1
                            TextBoxItemCode1.Enabled = False
                            'TextBoxItemDescription1.Enabled = False
                            'TextBoxItemType1.Enabled = False
                        End If

                        'If rowIndex > 1 Then
                        Dim query As String
                      




                        'If rowIndex = 0 Or rowIndex = 1 Then


                        '    Dim TextBoxItemCode12 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)


                        '    query = "Select * from tblbillingproducts"
                        '    PopulateDropDownList(query, "ProductCode", "ProductCode", TextBoxItemCode12)

                        '    Dim TextBoxUOM12 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        '    TextBoxUOM12.Text = "NO"
                        '    query = "Select * from tblunitms order by UnitMs"
                        '    PopulateDropDownList(query, "UnitMs", "UnitMs", TextBoxUOM12)
                        'Else
                        '    'TextBoxItemCode1.Items.Clear()
                        '    query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                        '    PopulateDropDownList(query, "ProductCode", "ProductCode", TextBoxItemCode1)

                        '    'TextBoxUOM1.Items.Clear()
                        '    query = "Select * from tblunitms order by UnitMs"
                        '    PopulateDropDownList(query, "UnitMs", "UnitMs", TextBoxUOM1)
                        'End If

                        ''TextBoxItemCode1.Items.Clear()
                        ''query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                        ''PopulateDropDownList(query, "ProductCode", "ProductCode", TextBoxItemCode1)

                        ''If String.IsNullOrEmpty(TextBoxItemType.Text) = False And (TextBoxItemType.Text) <> "-1" Then

                        ''    'Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                        ''    query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                        ''    PopulateDropDownList(query, "ProductCode", "ProductCode", TextBoxItemCode1)


                        ''    query = "Select * from tblunitms order by UnitMs"
                        ''    PopulateDropDownList(query, "UnitMs", "UnitMs", TextBoxUOM1)
                        ''End If

                       
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtUOMGV"), DropDownList)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtPricePerUOMGV"), TextBox)
                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalPriceGV"), TextBox)

                        'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtDiscPercGV"), TextBox)
                        'Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtPriceWithDiscGV"), TextBox)

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtContractNoGV"), TextBox)

                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtServiceStatusGV"), TextBox)
                        Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtTaxTypeGV"), DropDownList)
                        Dim TextBoxRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRetentionAmtGV"), TextBox)
                        Dim TextBoxRetentionGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(20).FindControl("txtRetentionGSTAmtGV"), TextBox)
                        Dim TextBoxTotalRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(21).FindControl("txtTotalRetentionGV"), TextBox)
                        Dim TextBoxRetentionWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtRetentionWithGSTGV"), TextBox)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtServiceRecordGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("PricePerUOM") = TextBoxPricePerUOM.Text
                        dtCurrentTable.Rows(i - 1)("TotalPrice") = TextBoxTotalPrice.Text

                        'dtCurrentTable.Rows(i - 1)("DiscPerc") = TextBoxDiscPerc.Text
                        'dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        'dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text

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

                        dtCurrentTable.Rows(i - 1)("RetentionAmt") = TextBoxRetentionAmt.Text
                        dtCurrentTable.Rows(i - 1)("RetentionGSTAmt") = TextBoxRetentionGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalRetentionAmt") = TextBoxTotalRetentionAmt.Text
                        dtCurrentTable.Rows(i - 1)("RetentionWithGST") = TextBoxRetentionWithGST.Text
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
            Throw ex
        End Try

    End Sub

  

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

    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)



        If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' order by name"
            End If
        Else
            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' and (upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
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

   

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs)

        'lblAlert.Text = ""

        'txtAccountId.Text = ""

        'Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
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
        If txtSearch.Text = "" Then
            txtAccountId.Text = ""
            txtClientName.Text = ""
            If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                txtAccountId.Text = ""
            Else
                txtAccountId.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            End If



            If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                txtClientName.Text = ""
            Else
                txtClientName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
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
        End If

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


    Protected Sub btnViewEdit_Click(sender As Object, e As EventArgs)
        Try
            lblAlert.Text = ""

            'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
            Dim btn1 As Button = DirectCast(sender, Button)
            'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
            Dim lblid1 As TextBox = CType(xrow1.FindControl("txtAccountIdGV"), TextBox)
            Dim lblid2 As TextBox = CType(xrow1.FindControl("txtAccountNameGV"), TextBox)
            Dim lblid3 As TextBox = CType(xrow1.FindControl("txtBillAddress1GV"), TextBox)
            Dim lblid4 As TextBox = CType(xrow1.FindControl("txtBillBuildingGV"), TextBox)
            Dim lblid5 As TextBox = CType(xrow1.FindControl("txtBillStreetGV"), TextBox)
            Dim lblid6 As TextBox = CType(xrow1.FindControl("txtBillCountryGV"), TextBox)
            Dim lblid7 As TextBox = CType(xrow1.FindControl("txtBillPostalGV"), TextBox)
            Dim lblid8 As TextBox = CType(xrow1.FindControl("txtOurReferenceGV"), TextBox)
            Dim lblid9 As TextBox = CType(xrow1.FindControl("txtYourReferenceGV"), TextBox)
            Dim lblid10 As TextBox = CType(xrow1.FindControl("txtPoNoGV"), TextBox)
            Dim lblid11 As TextBox = CType(xrow1.FindControl("txtCreditTermsGV"), TextBox)
            Dim lblid12 As TextBox = CType(xrow1.FindControl("txtSalesmanGV"), TextBox)
            Dim lblid13 As TextBox = CType(xrow1.FindControl("txtToBillAmtGV"), TextBox)
            Dim lblid14 As TextBox = CType(xrow1.FindControl("txtRcnoServiceRecordGV"), TextBox)
            Dim lblid15 As TextBox = CType(xrow1.FindControl("txtServiceRecordNoGV"), TextBox)
            Dim lblid16 As TextBox = CType(xrow1.FindControl("txtCompanyGroupGV"), TextBox)
            Dim lblid17 As TextBox = CType(xrow1.FindControl("txtRcnoInvoiceGV"), TextBox)
            Dim lblid18 As TextBox = CType(xrow1.FindControl("txtRcnoServicebillingdetailGV"), TextBox)
            Dim lblid19 As TextBox = CType(xrow1.FindControl("txtContactTypeGV"), TextBox)
            Dim lblid20 As TextBox = CType(xrow1.FindControl("txtInvoiceDateGV"), TextBox)

            Dim lblid21 As TextBox = CType(xrow1.FindControl("txtDiscAmountGV"), TextBox)
            Dim lblid22 As TextBox = CType(xrow1.FindControl("txtContractNoGV"), TextBox)
            'Dim lblid23 As TextBox = CType(xrow1.FindControl("txtToBillAmtGV"), TextBox)
            Dim lblid24 As TextBox = CType(xrow1.FindControl("txtServiceRecordNoGV"), TextBox)
            Dim lblid25 As TextBox = CType(xrow1.FindControl("txtStatusGV"), TextBox)
            Dim lblid26 As TextBox = CType(xrow1.FindControl("txtRetentionAmtGV"), TextBox)

            Label41.Text = "INVOICE DETAILS : " & lblid24.Text

            Dim rowindex1 As Integer = xrow1.RowIndex
            If txtMode.Text = "NEW" Then
                txtAccountType.Text = lblid19.Text
                txtCompanyGroup.Text = lblid16.Text
                'txtInvoiceDate.Text = lblid20.Text
                txtAccountIdBilling.Text = lblid1.Text
                txtAccountName.Text = lblid2.Text
                txtBillAddress.Text = lblid3.Text
                txtBillBuilding.Text = lblid4.Text
                txtBillStreet.Text = lblid5.Text
                txtBillCountry.Text = lblid6.Text
                txtBillPostal.Text = lblid7.Text
                txtOurReference.Text = lblid8.Text
                txtYourReference.Text = lblid9.Text
                txtPONo.Text = lblid10.Text
            End If

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            'Dim sql As String
            'sql = ""

            'Dim command21 As MySqlCommand = New MySqlCommand

            'Sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

            ''Dim command1 As MySqlCommand = New MySqlCommand
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


            'Sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='OTHER DEBTORS'"

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

            PopulateGLCodes()
            updPnlBillingRecs.Update()

            conn.Close()


            'Start: Populate the grid
            txtRcnoServiceRecord.Text = lblid14.Text
            txtRcnoServiceRecordDetail.Text = lblid15.Text
            txtContractNo.Text = lblid22.Text
            txtRcnoInvoice.Text = lblid17.Text
            txtRowSelected.Text = rowindex1.ToString
            txtRcnoservicebillingdetail.Text = lblid18.Text
            txtRcnotblServiceBillingDetail.Text = lblid18.Text

            If String.IsNullOrEmpty(txtBatchNo.Text) = True Or txtBatchNo.Text = "0" Then
                txtBatchNo.Text = txtRcnotblServiceBillingDetail.Text
            End If
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

            If String.IsNullOrEmpty(txtRcnoservicebillingdetail.Text) = True Then
                txtRcnoservicebillingdetail.Text = "0"
            End If

            Dim Query As String

            Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
            'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
            Query = "Select * from tblbillingproducts  "
            PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

            Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtUOMGV"), DropDownList)
            Query = "Select * from tblunitms order by UnitMs"
            PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM1)


            'If Convert.ToInt64(txtRcnoInvoice.Text) = 0 Then

            Dim TextBoxTotalPrice10 As TextBox
            Dim TextBoxTotalPriceWithGST10 As TextBox
            Dim TextBoxGSTAmt10 As TextBox
            Dim TextBoxRetentionGSTAmt10 As TextBox

            If Convert.ToInt64(txtRcnoservicebillingdetail.Text) = 0 Then
                Dim dt As New DataTable

                '''''''''''''''''''''''''

                'Get Item desc, price Id

                'Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'If conn.State = ConnectionState.Closed Then
                conn.Open()
                'End If

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text

                'If lblid25.Text = "P" Then
                '    command1.CommandText = "SELECT * FROM tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "' and  ProductCode = 'IN-SRV'"
                'ElseIf lblid25.Text = "O" Then
                '    command1.CommandText = "SELECT * FROM tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "' and  ProductCode = 'IN-DEF'"
                'End If

                If lblid25.Text = "P" Then
                    command1.CommandText = "SELECT * FROM tblbillingproducts  where   ProductCode = 'IN-SRV'"
                ElseIf lblid25.Text = "O" Then
                    command1.CommandText = "SELECT * FROM tblbillingproducts  where   ProductCode = 'IN-DEF'"
                End If

                command1.Connection = conn

                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count = 0 Then
                    lblAlert.Text = "Billing Code not Found"
                    updPnlMsg.Update()
                    Exit Sub
                End If

                Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                TextBoxItemType.Text = "SERVICE"
                TextBoxItemType.Enabled = False


                Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                TextBoxItemCode.Items.Clear()
                TextBoxItemCode.Items.Add(dt1.Rows(0)("ProductCode").ToString())

                'TextBoxItemCode.Text = dt1.Rows(0)("ProductCode").ToString()

                'Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                ''TextBoxItemCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemCode"))

                'TextBoxItemCode.Items.Clear()
                'TextBoxItemCode.Items.Add(Convert.ToString(dtServiceBillingDetailItem.Rows(rowindex1)("ItemCode")))


                Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
                'TextBoxItemDescription.Text = ""
                TextBoxItemDescription.Text = dt1.Rows(0)("Description").ToString()


                Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                TextBoxOtherCode.Text = dt1.Rows(0)("COACode").ToString()


                Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                TextBoxGLDescription.Text = dt1.Rows(0)("COADescription").ToString()

                Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtUOMGV"), DropDownList)
                TextBoxUOM.Text = "--SELECT--"

                Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtQtyGV"), TextBox)
                TextBoxQty.Text = "1"
                TextBoxQty.Enabled = False

                'Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                'TextBoxPriceWithDisc.Text = Convert.ToString(Convert.ToDecimal(lblid13.Text).ToString("N2"))

                Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
                TextBoxPricePerUOM.Text = Convert.ToString(Convert.ToDecimal(lblid13.Text).ToString("N2"))



                Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGSTPercGV"), TextBox)
                TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N4")

                Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid13.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))

                Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
                TextBoxTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblid13.Text).ToString("N2"))

                Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                TextBoxTotalPriceWithGST.Text = Convert.ToString(Convert.ToDecimal(TextBoxPricePerUOM.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text))


                Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtContractNoGV"), TextBox)
                TextBoxContractNo.Text = Convert.ToString(txtContractNo.Text)

                Dim TextBoxServiceStatus As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtServiceStatusGV"), TextBox)
                TextBoxServiceStatus.Text = Convert.ToString(lblid25.Text)


                Dim TextBoxRetentionAmt As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtRetentionAmtGV"), TextBox)
                TextBoxRetentionAmt.Text = Convert.ToString(Convert.ToDecimal("0.00").ToString("N2"))

                Dim TextBoxRetentionGSTAmt As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtRetentionGSTAmtGV"), TextBox)
                TextBoxRetentionGSTAmt.Text = Convert.ToString(Convert.ToDecimal("0.00").ToString("N2"))

                TextBoxTotalPriceWithGST10 = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtRetentionWithGSTGV"), TextBox)
                TextBoxTotalPriceWithGST10.Text = Convert.ToString(Convert.ToDecimal("0.00"))

                TextBoxTotalPrice10 = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtTotalRetentionGV"), TextBox)
                TextBoxTotalPrice10.Text = Convert.ToString(Convert.ToDecimal("0.00"))

                Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtServiceRecordGV"), TextBox)
                TextBoxServiceRecord.Text = Convert.ToString(lblid24.Text)

                txtTotal.Text = (Convert.ToDecimal(TextBoxTotalPrice.Text)).ToString("N2")
                txtTotalGSTAmt.Text = (Convert.ToDecimal(TextBoxGSTAmt.Text)).ToString("N2")
                txtTotalWithGST.Text = (Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)).ToString("N2")


                txtRetentionTotal.Text = (Convert.ToDecimal("0.00")).ToString("N2")
                txtTotalRetentionGSTAmt.Text = (Convert.ToDecimal("0.00")).ToString("N2")
                txtTotalRetentionWithGST.Text = (Convert.ToDecimal("0.00")).ToString("N2")

                '''''''''''''''''''Retention Amt '''''''''''''''''''''''


                If Convert.ToDecimal(lblid26.Text) > 0.0 Then

                    'Dim TextBoxItemCode12 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                    'Query = "Select * from tblbillingproducts"
                    'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode12)

                    'Dim TextBoxUOM12 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtUOMGV"), DropDownList)
                    'Query = "Select * from tblunitms order by UnitMs"
                    'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM12)



                    AddNewRowBillingDetailsRecs()
                    'AddNewRowWithDetailRecBillingDetailsRecs()
                    'Dim TextBoxItemCode12 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                    'Query = "Select * from tblbillingproducts"
                    'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode12)

                    'Dim TextBoxUOM12 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtUOMGV"), DropDownList)
                    'Query = "Select * from tblunitms order by UnitMs"
                    'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM12)

                    Dim command10 As MySqlCommand = New MySqlCommand
                    command10.CommandType = CommandType.Text

                    'If lblid25.Text = "P" Then
                    '    command10.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
                    'ElseIf lblid25.Text = "O" Then
                    '    command10.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
                    'End If

                    'If lblid25.Text = "P" Then
                    '    command10.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
                    'ElseIf lblid25.Text = "O" Then
                    'command10.CommandText = "SELECT * FROM tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "' and  ProductCode = 'IN-RET'"
                    command10.CommandText = "SELECT * FROM tblbillingproducts  where   ProductCode = 'IN-RET'"
                    'End If

                    command10.Connection = conn

                    Dim dr10 As MySqlDataReader = command10.ExecuteReader()
                    Dim dt10 As New DataTable
                    dt10.Load(dr10)



                    Dim TextBoxItemType10 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                    TextBoxItemType10.Text = "SERVICE"
                    'TextBoxItemType10.Enabled = False

                    Dim TextBoxItemCode10 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                    'TextBoxItemCode10.Text = dt10.Rows(0)("ProductCode").ToString()
                    TextBoxItemCode10.Text = "IN-RET"

                    Dim TextBoxItemDescription10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
                    'TextBoxItemDescription.Text = ""
                    TextBoxItemDescription10.Text = dt10.Rows(0)("Description").ToString()


                    Dim TextBoxOtherCode10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(1).FindControl("txtOtherCodeGV"), TextBox)
                    TextBoxOtherCode10.Text = dt10.Rows(0)("COACode").ToString()


                    Dim TextBoxGLDescription10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                    TextBoxGLDescription10.Text = dt10.Rows(0)("COADescription").ToString()

                    Dim TextBoxUOM10 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtUOMGV"), DropDownList)
                    TextBoxUOM10.Text = "--SELECT--"

                    Dim TextBoxQty10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtQtyGV"), TextBox)
                    TextBoxQty10.Text = "1"
                    TextBoxQty10.Enabled = False

                    'Dim TextBoxPriceWithDisc10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                    'TextBoxPriceWithDisc10.Text = lblid26.Text

                    Dim TextBoxRetentionAmt10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtRetentionAmtGV"), TextBox)
                    TextBoxRetentionAmt10.Text = Convert.ToString(Convert.ToDecimal(lblid26.Text).ToString("N2"))

                    TextBoxRetentionGSTAmt10 = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtRetentionGSTAmtGV"), TextBox)
                    TextBoxRetentionGSTAmt10.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid26.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))

                    Dim TextBoxPricePerUOM10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
                    TextBoxPricePerUOM10.Text = Convert.ToString(Convert.ToDecimal("0.00").ToString("N2"))

                    Dim TextBoxGSTPerc10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtGSTPercGV"), TextBox)
                    TextBoxGSTPerc10.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N4")

                    'Dim TextBoxGSTAmt10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    TextBoxGSTAmt = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal("0.00").ToString("N2"))

                    TextBoxTotalPrice10 = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtTotalRetentionGV"), TextBox)
                    TextBoxTotalPrice10.Text = lblid26.Text

                    'Dim TextBoxTotalPriceWithGST10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    'TextBoxTotalPriceWithGST10 = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    'TextBoxTotalPriceWithGST10.Text = Convert.ToString(Convert.ToDecimal(TextBoxRetentionAmt10.Text) + Convert.ToDecimal(TextBoxRetentionGSTAmt10.Text))


                    Dim TextBoxContractNo10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtContractNoGV"), TextBox)
                    TextBoxContractNo10.Text = Convert.ToString(txtContractNo.Text)

                    Dim TextBoxServiceStatus10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtServiceStatusGV"), TextBox)
                    TextBoxServiceStatus10.Text = Convert.ToString(lblid25.Text)

                    TextBoxTotalPriceWithGST10 = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtRetentionWithGSTGV"), TextBox)
                    TextBoxTotalPriceWithGST10.Text = Convert.ToString(Convert.ToDecimal(TextBoxRetentionAmt10.Text) + Convert.ToDecimal(TextBoxRetentionGSTAmt10.Text))

                    TextBoxTotalPriceWithGST = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    TextBoxTotalPriceWithGST.Text = Convert.ToString(Convert.ToDecimal("0.00"))

                    TextBoxServiceRecord = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtServiceRecordGV"), TextBox)
                    TextBoxServiceRecord.Text = Convert.ToString(lblid24.Text)


                    'Dim TextBoxDiscPerc10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtDiscPercGV"), TextBox)
                    'TextBoxDiscPerc10.Text = "0.00"

                    'Dim TextBoxDiscAmount10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtDiscAmountGV"), TextBox)
                    'TextBoxDiscAmount10.Text = "0.00"


                    TextBoxTotalPrice = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
                    TextBoxTotalPrice.Text = Convert.ToString(Convert.ToDecimal("0.00").ToString("N2"))


                    Dim TextBoxItemCode11 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                    'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                    Query = "Select * from tblbillingproducts "
                    PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode11)

                    'Dim TextBoxUOM11 As DropDownList = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtUOMGV"), DropDownList)
                    'Query = "Select * from tblunitms order by UnitMs"
                    'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM11)
                End If


                '''''''''''''''''Retention Amt '''''''''''''''''''''''

                txtTotalDiscAmt.Text = (0.0).ToString("N2")
                'txtTotalGSTAmt.Text = Convert.ToDecimal(TextBoxGSTAmt.Text).ToString("N2")


                txtRetentionTotal.Text = (Convert.ToDecimal(TextBoxTotalPrice10.Text)).ToString("N2")
                txtTotalRetentionGSTAmt.Text = (Convert.ToDecimal(TextBoxRetentionGSTAmt10.Text)).ToString("N2")
                txtTotalRetentionWithGST.Text = (Convert.ToDecimal(TextBoxTotalPriceWithGST10.Text)).ToString("N2")
                'txtTotalRetentionGSTAmt.Text = (Convert.ToDecimal(TextBoxGSTAmt10.Text)).ToString("N2")
                'txtTotalRetentionGSTAmt.Text = (Convert.ToDecimal(TextBoxGSTAmt10.Text)).ToString("N2")

                txtTotalWithDiscAmt.Text = txtTotal.Text

                '''''''''''''''''''''

            Else

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

                'Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim cmdServiceBillingDetailItem As MySqlCommand = New MySqlCommand
                cmdServiceBillingDetailItem.CommandType = CommandType.Text
                cmdServiceBillingDetailItem.CommandText = "SELECT * FROM tblservicebillingdetailitem where Rcnoservicebillingdetail=" & Convert.ToInt32(txtRcnoservicebillingdetail.Text)
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        TextBoxItemType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemType"))

                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                        TextBoxItemCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemCode"))

                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
                        TextBoxItemDescription.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemDescription"))

                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                        TextBoxOtherCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("OtherCode"))

                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtUOMGV"), DropDownList)
                        If String.IsNullOrEmpty(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("UOM"))) = True Then

                        Else
                            TextBoxUOM.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("UOM"))
                        End If


                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
                        TextBoxQty.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("Qty"))
                        'TextBoxQty.Enabled = False

                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtPricePerUOMGV"), TextBox)
                        TextBoxPricePerUOM.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("PricePerUOM"))


                        Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscPercGV"), TextBox)
                        TextBoxDiscPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("DiscPerc"))

                        Dim TextBoxDiscAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
                        TextBoxDiscAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("DiscAmount"))


                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtPriceWithDiscGV"), TextBox)
                        TextBoxPriceWithDisc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("PriceWithDisc"))


                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtTaxTypeGV"), DropDownList)
                        TextBoxTaxType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("TaxType"))

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtGSTPercGV"), TextBox)
                        TextBoxGSTPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GSTPerc"))

                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtGSTAmtGV"), TextBox)
                        TextBoxGSTAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GSTAmt"))

                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtTotalPriceGV"), TextBox)
                        TextBoxTotalPrice.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("TotalPrice"))

                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                        TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("TotalPriceWithGST"))

                        Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceRecordGV"), TextBox)
                        TextBoxServiceRecord.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ServiceRecordNo"))

                        Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtInvoiceTypeGV"), TextBox)
                        TextBoxInvoiceType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("InvoiceType"))

                        Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
                        TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
                        TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
                        'txtAmountWithDiscount.Text =  Total - TotalDiscAmt
                        TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                        TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

                        'Dim Query As String

                        Dim TextBoxItemCode2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                        'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                        Query = "Select * from tblbillingproducts"
                        PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode2)

                        Dim TextBoxUOM2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        Query = "Select * from tblunitms order by UnitMs"
                        PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM2)


                        Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

                        If TextBoxItemType1.Text = "SERVICE" Then
                            TextBoxQty1.Enabled = False
                            TextBoxQty1.Text = 1
                            TextBoxItemType1.Enabled = False
                        End If

                        rowIndex += 1

                    Next row
                    'AddNewRowBillingDetailsRecs()
                    'SetPreviousDataBillingDetailsRecs()
                    'AddNewRow()
                    'SetPreviousData()
                    txtTotal.Text = Total.ToString("N2")
                    txtTotalWithGST.Text = TotalWithGST.ToString("N2")
                    txtTotalDiscAmt.Text = TotalDiscAmt.ToString("N2")
                    txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")

                    'txtTotalDiscAmt.Text = 0.0
                    txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt

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
            End If

            AddNewRowBillingDetailsRecs()
            grvBillingDetails.Enabled = True

            btnSaveInvoice.Enabled = False

            If txtPostStatus.Text <> "P" Then
                btnSave.Enabled = True
            End If

            'btnSave.Enabled = True
            updpnlServiceRecs.Update()
            updpnlBillingDetails.Update()
            'End: Billing Details
            updPanelSave.Update()
            'End: Populate the grid
            updPnlBillingRecs.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
        End Try
    End Sub

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


    Protected Sub txtRetentionAmtGV_TextChanged(sender As Object, e As EventArgs)
        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)

        CalculateRetentionPrice()
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

        'Dim lblid4 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscPercGV"), TextBox)
        'Dim lblid5 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscAmountGV"), TextBox)
        'Dim lblid6 As TextBox = CType(xgrvBillingDetails.FindControl("txtPriceWithDiscGV"), TextBox)

        Dim lblid7 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTPercGV"), TextBox)
        Dim lblid8 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTAmtGV"), TextBox)
        Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceWithGSTGV"), TextBox)

        Dim dblQty As String
        Dim dblPricePerUOM As String
        Dim dblTotalPrice As String

        'Dim dblDiscPerc As String
        'Dim dblDisAmount As String
        'Dim dblPriceWithDisc As String

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

        'If String.IsNullOrEmpty(lblid4.Text) = True Then
        '    lblid4.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid5.Text) = True Then
        '    lblid5.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid6.Text) = True Then
        '    lblid6.Text = "0.00"
        'End If

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

        'dblDiscPerc = (lblid4.Text)
        'dblDisAmount = (lblid5.Text)
        'dblPriceWithDisc = (lblid6.Text)

        dblGSTPerc = (lblid7.Text)
        dblGSTAmt = (lblid8.Text)
        dblTotalPriceWithGST = (lblid9.Text)

        lblid3.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid1.Text) * Convert.ToDecimal(lblid2.Text)).ToString("N2"))
        'lblid5.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid3.Text) * Convert.ToDecimal(lblid4.Text) * 0.01).ToString("N2"))
        'lblid6.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid3.Text)) - Convert.ToDecimal(lblid5.Text)).ToString("N2"))
        lblid8.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid3.Text) * Convert.ToDecimal(lblid7.Text) * 0.01).ToString("N2"))
        lblid9.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid3.Text)) + Convert.ToDecimal(lblid8.Text)).ToString("N2"))

        CalculateTotalPrice()


    End Sub


    Private Sub CalculateTotalPrice()

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

                'Dim TextBoxDisAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtDiscAmountGV"), TextBox)
                'Dim TextBoxTotalWithDiscAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtPriceWithDiscGV"), TextBox)

                Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtGSTAmtGV"), TextBox)

                If String.IsNullOrEmpty(TextBoxTotal.Text) = True Then
                    TextBoxTotal.Text = "0.00"
                End If

                'If String.IsNullOrEmpty(TextBoxDisAmt.Text) = True Then
                '    TextBoxDisAmt.Text = "0.00"
                'End If

                'If String.IsNullOrEmpty(TextBoxTotalWithDiscAmt.Text) = True Then
                '    TextBoxTotalWithDiscAmt.Text = "0.00"
                'End If

                If String.IsNullOrEmpty(TextBoxGSTAmt.Text) = True Then
                    TextBoxGSTAmt.Text = "0.00"
                End If

                If String.IsNullOrEmpty(TextBoxTotalWithGST.Text) = True Then
                    TextBoxTotalWithGST.Text = "0.00"
                End If

                TotalAmt = TotalAmt + Convert.ToDecimal(TextBoxTotal.Text)
                TotalAmtWithGST = TotalAmtWithGST + Convert.ToDecimal(TextBoxTotalWithGST.Text)

                'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDisAmt.Text)
                TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                'TotalWithDiscAmt = TotalWithDiscAmt + Convert.ToDecimal(TextBoxTotalWithDiscAmt.Text)
            Next i

        End If


        txtTotal.Text = TotalAmt.ToString
        txtTotalWithGST.Text = TotalAmtWithGST.ToString

        'txtTotalDiscAmt.Text = TotalDiscAmt.ToString
        txtTotalGSTAmt.Text = TotalGSTAmt.ToString

        'txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString
        updPanelSave.Update()
    End Sub




    Private Sub CalculateRetentionPrice()
        Dim lblid1 As TextBox = CType(xgrvBillingDetails.FindControl("txtQtyGV"), TextBox)
        Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtRetentionAmtGV"), TextBox)
        Dim lblid3 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalRetentionGV"), TextBox)

        'Dim lblid4 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscPercGV"), TextBox)
        'Dim lblid5 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscAmountGV"), TextBox)
        'Dim lblid6 As TextBox = CType(xgrvBillingDetails.FindControl("txtPriceWithDiscGV"), TextBox)

        Dim lblid7 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTPercGV"), TextBox)
        Dim lblid8 As TextBox = CType(xgrvBillingDetails.FindControl("txtRetentionGSTAmtGV"), TextBox)
        Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtRetentionWithGSTGV"), TextBox)

        Dim dblQty As String
        Dim dblPricePerUOM As String
        Dim dblTotalPrice As String

        'Dim dblDiscPerc As String
        'Dim dblDisAmount As String
        'Dim dblPriceWithDisc As String

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

        'If String.IsNullOrEmpty(lblid4.Text) = True Then
        '    lblid4.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid5.Text) = True Then
        '    lblid5.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid6.Text) = True Then
        '    lblid6.Text = "0.00"
        'End If

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

        'dblDiscPerc = (lblid4.Text)
        'dblDisAmount = (lblid5.Text)
        'dblPriceWithDisc = (lblid6.Text)

        dblGSTPerc = (lblid7.Text)
        dblGSTAmt = (lblid8.Text)
        dblTotalPriceWithGST = (lblid9.Text)

        lblid3.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid1.Text) * Convert.ToDecimal(lblid2.Text)).ToString("N2"))
        'lblid5.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid3.Text) * Convert.ToDecimal(lblid4.Text) * 0.01).ToString("N2"))
        'lblid6.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid3.Text)) - Convert.ToDecimal(lblid5.Text)).ToString("N2"))
        lblid8.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid3.Text) * Convert.ToDecimal(lblid7.Text) * 0.01).ToString("N2"))
        lblid9.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid3.Text)) + Convert.ToDecimal(lblid8.Text)).ToString("N2"))

        CalculateTotalRetentionPrice()


    End Sub


    Private Sub CalculateTotalRetentionPrice()

        Dim TotalAmt As Decimal = 0
        Dim TotalDiscAmt As Decimal = 0
        Dim TotalWithDiscAmt As Decimal = 0
        Dim TotalGSTAmt As Decimal = 0
        Dim TotalAmtWithGST As Decimal = 0
        Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)


        If (table.Rows.Count > 0) Then

            For i As Integer = 0 To (table.Rows.Count) - 1

                Dim TextBoxTotal As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtTotalRetentionGV"), TextBox)
                Dim TextBoxTotalWithGST As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtRetentionWithGSTGV"), TextBox)

                'Dim TextBoxDisAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtDiscAmountGV"), TextBox)
                'Dim TextBoxTotalWithDiscAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtPriceWithDiscGV"), TextBox)

                Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtRetentionGSTAmtGV"), TextBox)

                If String.IsNullOrEmpty(TextBoxTotal.Text) = True Then
                    TextBoxTotal.Text = "0.00"
                End If

                'If String.IsNullOrEmpty(TextBoxDisAmt.Text) = True Then
                '    TextBoxDisAmt.Text = "0.00"
                'End If

                'If String.IsNullOrEmpty(TextBoxTotalWithDiscAmt.Text) = True Then
                '    TextBoxTotalWithDiscAmt.Text = "0.00"
                'End If

                If String.IsNullOrEmpty(TextBoxGSTAmt.Text) = True Then
                    TextBoxGSTAmt.Text = "0.00"
                End If

                If String.IsNullOrEmpty(TextBoxTotalWithGST.Text) = True Then
                    TextBoxTotalWithGST.Text = "0.00"
                End If

                TotalAmt = TotalAmt + Convert.ToDecimal(TextBoxTotal.Text)
                TotalAmtWithGST = TotalAmtWithGST + Convert.ToDecimal(TextBoxTotalWithGST.Text)

                'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDisAmt.Text)
                TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                'TotalWithDiscAmt = TotalWithDiscAmt + Convert.ToDecimal(TextBoxTotalWithDiscAmt.Text)
            Next i

        End If


        txtRetentionTotal.Text = TotalAmt.ToString
        txtTotalRetentionWithGST.Text = TotalAmtWithGST.ToString

        'txtTotalDiscAmt.Text = TotalDiscAmt.ToString
        txtTotalRetentionGSTAmt.Text = TotalGSTAmt.ToString

        'txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString
        updPanelSave.Update()
    End Sub
    Private Sub MakeMeNullBillingDetails()
        chkRecurringInvoice.Checked = False
        txtInvoiceNo.Text = ""
        'txtInvoiceDate.Text = ""
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

        txtInvoiceAmount.Text = 0.0
        'txtDiscountAmount.Text = 0.0
        'txtAmountWithDiscount.Text = 0.0
        txtGSTAmount.Text = 0.0
        txtNetAmount.Text = 0.0

        txtTotal.Text = "0.00"
        txtTotalWithGST.Text = "0.00"
        txtTotalGSTAmt.Text = "0.00"
        txtTotalDiscAmt.Text = "0.00"
        txtTotalWithDiscAmt.Text = "0.00"

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
                            lblAlert.Focus()
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
            'Dim lblid20 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtContractNoGV"), TextBox)
            Dim lblid21 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceAddressGV"), TextBox)
            Dim lblid22 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtRetentionAmtGV"), TextBox)

            If txtMode.Text = "NEW" Then

                'Dim command As MySqlCommand = New MySqlCommand
                'command.CommandType = CommandType.Text

                If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
                    txtRcnotblServiceBillingDetail.Text = 0
                End If

                'If Convert.ToInt64(txtBatchNo.Text) = 0 Then
                qry = "INSERT INTO tblServiceBillingDetail( AccountId, CustName, LocationId, Name, RecordNo, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, "
                qry = qry + " ServiceDate, BillAmount, DiscountAmount,  GSTAmount, TotalWithGST, NetAmount, OurRef,YourRef,ContractNo, PoNo, RcnoServiceRecord, BillingFrequency, Salesman, ContactType, CompanyGroup,   "
                qry = qry + " ContractGroup, Status, Address1, RetentionAmt,  "
                qry = qry + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                qry = qry + " (@AccountId, @ClientName, @LocationId, @AccountName, @ServiceRecordNo, @BillAddress1, @BillBuilding, @BillStreet, @BillCountry, @BillPostal, "
                qry = qry + " @ServiceDate, @BillAmount, @DiscountAmount,  @GSTAmount, @TotalWithGST, @NetAmount, @OurRef, @YourRef, @ContractNo, @PoNo, @RcnoServiceRecord, @BillingFrequency, @Salesman, @ContactType, @CompanyGroup,   "
                qry = qry + " @ContractGroup, @Status,  @Address1, @RetentionAmt, "
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
                command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
                command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
                command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
                command.Parameters.AddWithValue("@RetentionAmt", Convert.ToDecimal(lblid22.Text))

                command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
                command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
                command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                command.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
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
                End If
                'End If
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
                    command.Parameters.AddWithValue("@DiscountAmount", 0.0)
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
                        'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDiscPercGV"), TextBox)
                        'Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtPriceWithDiscGV"), TextBox)
                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTaxTypeGV"), DropDownList)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceStatus As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtServiceStatusGV"), TextBox)

                        Dim TextBoxTotalRetentionPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalRetentionGV"), TextBox)
                        Dim TextBoxRetentionGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtRetentionGSTAmtGV"), TextBox)
                        Dim TextBoxTotalRetentionPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtRetentionWithGSTGV"), TextBox)


                        If String.IsNullOrEmpty(lbd10) = False Then
                            If (Convert.ToInt64(lbd10) > 0) Then

                                ''Start:Detail
                                Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                                commandSalesDetail.CommandType = CommandType.Text
                                Dim qryDetail As String = "INSERT INTO tblServiceBillingDetailItem(RcnoServiceBillingDetail,Itemtype, ItemCode, Itemdescription, UOM, Qty,  "
                                qryDetail = qryDetail + " PricePerUOM, TotalPrice,DiscPerc, DiscAmount, PriceWithDisc, GSTPerc, GSTAmt, TotalPriceWithGST, TaxType, ARCode, GSTCode, OtherCode, GLDescription,  RcnoServiceRecord, BatchNo,  CompanyGroup, ContractNo, ServiceStatus, ContractGroup, ServiceRecordNo, ServiceDate, InvoiceType, "
                                qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qryDetail = qryDetail + "(@RcnoServiceBillingDetail, @Itemtype, @ItemCode, @Itemdescription, @UOM, @Qty,"
                                qryDetail = qryDetail + " @PricePerUOM, @TotalPrice, @DiscPerc, @DiscAmount, @PriceWithDisc, @GSTPerc, @GSTAmt, @TotalPriceWithGST, @TaxType, @ARCode, @GSTCode,  @OtherCode,@GLDescription, @RcnoServiceRecord, @BatchNo, @CompanyGroup, @ContractNo,  @ServiceStatus, @ContractGroup, @ServiceRecordNo, @ServiceDate, @InvoiceType, "
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
                                command.Parameters.AddWithValue("@PricePerUOM", Convert.ToDecimal(TextBoxPricePerUOMGV.Text) + Convert.ToDecimal(TextBoxTotalRetentionPrice.Text))
                                'command.Parameters.AddWithValue("@BillAmount", 0.0)

                                If String.IsNullOrEmpty(TextBoxTotalPrice.Text) = True Then
                                    command.Parameters.AddWithValue("@TotalPrice", 0.0)
                                Else
                                    command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(TextBoxTotalPrice.Text) + Convert.ToDecimal(TextBoxTotalRetentionPrice.Text))
                                End If

                                'If String.IsNullOrEmpty(TextBoxDiscPerc.Text) = True Then
                                '    command.Parameters.AddWithValue("@DiscPerc", 0.0)
                                'Else
                                '    command.Parameters.AddWithValue("@DiscPerc", Convert.ToDecimal(TextBoxDiscPerc.Text))
                                'End If

                                'If String.IsNullOrEmpty(TextBoxDiscAmount.Text) = True Then
                                '    command.Parameters.AddWithValue("@DiscAmount", 0.0)
                                'Else
                                '    command.Parameters.AddWithValue("@DiscAmount", Convert.ToDecimal(TextBoxDiscAmount.Text))
                                'End If

                                'command.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text))

                                command.Parameters.AddWithValue("@DiscPerc", 0.0)
                                command.Parameters.AddWithValue("@DiscAmount", 0.0)
                                command.Parameters.AddWithValue("@PriceWithDisc", 0.0)

                                command.Parameters.AddWithValue("@GSTPerc", Convert.ToDecimal(TextBoxGSTPerc.Text))
                                command.Parameters.AddWithValue("@GSTAmt", Convert.ToDecimal(TextBoxGSTAmt.Text) + Convert.ToDecimal(TextBoxRetentionGSTAmt.Text))
                                command.Parameters.AddWithValue("@TotalPriceWithGST", Convert.ToDecimal(TextBoxTotalPriceWithGST.Text) + Convert.ToDecimal(TextBoxTotalRetentionPriceWithGST.Text))
                                command.Parameters.AddWithValue("@TaxType", TextBoxTaxType.Text)
                                command.Parameters.AddWithValue("@RcnoServiceRecord", Convert.ToInt64(lblid7.Text))
                                command.Parameters.AddWithValue("@ARCode", "")
                                command.Parameters.AddWithValue("@GSTCode", "")
                                command.Parameters.AddWithValue("@OtherCode", TextBoxOtherCode.Text)
                                command.Parameters.AddWithValue("@GLDescription", TextBoxGLDescription.Text)
                                command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
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

                                If TextBoxItemTypeGV.Text = "SERVICE" Then
                                    command.Parameters.AddWithValue("@InvoiceType", "PROGRESS CLAIM")
                                Else
                                    command.Parameters.AddWithValue("@InvoiceType", TextBoxItemTypeGV.Text)
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

                Dim lblid10 As CheckBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("chkSelectGV"), CheckBox)
                Dim lblid11 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtToBillAmtGV"), TextBox)
                'Dim lblid12 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtDiscAmountGV"), TextBox)
                Dim lblid13 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtGSTAmountGV"), TextBox)
                Dim lblid14 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtNetAmountGV"), TextBox)
                Dim lblid15 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                lblid10.Checked = True
                lblid11.Text = Convert.ToDecimal(txtTotal.Text).ToString("N2")
                'lblid12.Text = Convert.ToDecimal(txtTotalDiscAmt.Text).ToString("N2")
                lblid13.Text = Convert.ToDecimal(txtTotalGSTAmt.Text).ToString("N2")
                lblid14.Text = Convert.ToDecimal(txtTotalWithGST.Text).ToString("N2")
                lblid15.Text = Convert.ToInt64(txtRcnotblServiceBillingDetail.Text)

                'FirstGridViewRowBillingDetailsRecs()

                'Start: Update Invoice Amounts

                Dim TotalInvoiceAmount As Decimal = 0
                Dim TotalDiscountAmount As Decimal = 0
                Dim TotalGSTAmount As Decimal = 0
                Dim TotalNetAmount As Decimal = 0
                Dim TotalRetentionAmount As Decimal = 0
                Dim TotalRetentionGSTAmount As Decimal = 0.0

                Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

                If (table.Rows.Count > 0) Then

                    For i As Integer = 0 To (table.Rows.Count) - 1

                        Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)

                        If (TextBoxchkSelect.Checked = True) Then

                            Dim TextBoxInvoiceAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtToBillAmtGV"), TextBox)
                            'Dim TextBoxDiscountAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtDiscAmountGV"), TextBox)
                            Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtGSTAmountGV"), TextBox)
                            Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtNetAmountGV"), TextBox)
                            'Dim TextBoxRetentionGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtRetentionGSTAmtGV"), TextBox)
                            Dim TextBoxRetentionAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtRetentionAmtGV"), TextBox)


                            'If String.IsNullOrEmpty(TextBoxRetentionGSTAmount.Text) = True Then
                            '    TextBoxRetentionGSTAmount.Text = "0.00"
                            'End If
                            If String.IsNullOrEmpty(TextBoxRetentionAmount.Text) = True Then
                                TextBoxRetentionAmount.Text = "0.00"
                            End If

                            If String.IsNullOrEmpty(TextBoxInvoiceAmount.Text) = True Then
                                TextBoxInvoiceAmount.Text = "0.00"
                            End If

                            'If String.IsNullOrEmpty(TextBoxDiscountAmount.Text) = True Then
                            '    TextBoxDiscountAmount.Text = "0.00"
                            'End If

                            If String.IsNullOrEmpty(TextBoxGSTAmount.Text) = True Then
                                TextBoxGSTAmount.Text = "0.00"
                            End If

                            If String.IsNullOrEmpty(TextBoxNetAmount.Text) = True Then
                                TextBoxNetAmount.Text = "0.00"
                            End If

                            TotalInvoiceAmount = TotalInvoiceAmount + Convert.ToDecimal(TextBoxInvoiceAmount.Text)
                            'TotalDiscountAmount = TotalDiscountAmount + Convert.ToDecimal(TextBoxDiscountAmount.Text)

                            TotalGSTAmount = TotalGSTAmount + Convert.ToDecimal(TextBoxGSTAmount.Text)
                            TotalNetAmount = TotalNetAmount + Convert.ToDecimal(TextBoxNetAmount.Text)

                            TotalRetentionAmount = TotalRetentionAmount + Convert.ToDecimal(TextBoxRetentionAmount.Text)

                        End If
                    Next i
                    TotalRetentionGSTAmount = TotalRetentionGSTAmount + Convert.ToDecimal(txtTotalRetentionGSTAmt.Text)
                End If

                'txtInvoiceAmount.Text = TotalInvoiceAmount.ToString("N2")
                ''txtDiscountAmount.Text = TotalDiscountAmount.ToString("N2")

                ''txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString("N2")
                ''txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text)).ToString("N2")
                'txtGSTAmount.Text = TotalGSTAmount.ToString("N2")
                'txtNetAmount.Text = TotalNetAmount.ToString("N2")
                'txtRetentionAmount.Text = TotalRetentionAmount.ToString("N2")
                'txtRetentionGSTAmount.Text = TotalRetentionGSTAmount.ToString("N2")
                ''txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString

                updPnlBillingRecs.Update()
                'updPanelSave.Update()

                'updPnlBillingRecs.Update()
                'End: Update Invoice Amounts


            End If '04.01.17

            conn.Close()

            'DisplayGLGrid()

            'btnSaveInvoice_Click(sender, e)


            'If txtMode.Text = "NEW" Then
            '    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
            'Else
            '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
            'End If

            'DisableControls()
            lblAlert.Text = ""


            btnSave.Enabled = False
            btnSaveInvoice.Enabled = True

            updPnlMsg.Update()
            updpnlServiceRecs.Update()
            updPnlBillingRecs.Update()
            'FirstGridViewRowBillingDetailsRecs()

            txtTotal.Text = "0.00"
            txtTotalWithGST.Text = "0.00"
            txtTotalGSTAmt.Text = "0.00"
            'txtTotalDiscAmt.Text = "0.00"
            txtTotalWithDiscAmt.Text = "0.00"
            Label41.Text = "INVOICE DETAILS"


            txtRcnotblServiceBillingDetail.Text = "0"
            'txtMode.Text = "EDIT"

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
        End Try
    End Sub

    Private Sub DisplayGLGrid()


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


        If Convert.ToDecimal(txtRetentionAmount.Text) > 0.0 Then
            rowIndex3 += 1
            AddNewRowGL()


            ''AR values

            Dim TextBoxGLCodeAR10 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            TextBoxGLCodeAR10.Text = txtARCode10.Text

            Dim TextBoxGLDescriptionAR10 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            TextBoxGLDescriptionAR10.Text = txtARDescription10.Text

            Dim TextBoxDebitAmountAR10 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            TextBoxDebitAmountAR10.Text = (Convert.ToDecimal(txtRetentionAmount.Text) + Convert.ToDecimal(txtRetentionGSTAmount.Text)).ToString("N2")

            Dim TextBoxCreditAmountAR10 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
            TextBoxCreditAmountAR10.Text = (0.0).ToString("N2")
        End If


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
        TextBoxCreditAmountGST.Text = (Convert.ToDecimal(txtGSTAmount.Text) + Convert.ToDecimal(txtRetentionGSTAmount.Text)).ToString("N2")
        ''GST Values



        rowIndex3 += 1
        AddNewRowGL()
        Dim conn As MySqlConnection = New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim cmdGL As MySqlCommand = New MySqlCommand
        cmdGL.CommandType = CommandType.Text
        cmdGL.CommandText = "SELECT ItemCode, OtherCode, GLDescription, PriceWithDisc,TotalPrice   FROM tblservicebillingdetailitem where ItemCode <> 'IN-RET' and  BatchNo ='" & txtBatchNo.Text.Trim & "' order by OtherCode"
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
                    'lCreditAmount = lCreditAmount + row("PriceWithDisc")
                    lCreditAmount = lCreditAmount + row("TotalPrice")
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
                    lCreditAmount = row("TotalPrice")

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
        'TextBoxCreditAmount1.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")
        TextBoxCreditAmount1.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtRetentionAmount.Text)).ToString("N2")

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
    End Sub

    Protected Sub btnSaveInvoice_Click(sender As Object, e As EventArgs) Handles btnSaveInvoice.Click
     lblAlert.Text = ""
        If String.IsNullOrEmpty(txtInvoiceDate.Text) = True Then
            lblAlert.Text = "PLEASE ENTER INVOICE DATE"
            Exit Sub
        End If

        Dim rowselected As Integer
        rowselected = 0
        Try


            Dim conn As MySqlConnection = New MySqlConnection()
            SetRowDataServiceRecs()
            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

            If tableAdd IsNot Nothing Then


                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Open()

                '''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''''''''''''''''''''''''

                'Dim conn As MySqlConnection = New MySqlConnection()

                'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'If conn.State = ConnectionState.Open Then
                '    conn.Close()
                'End If
                'conn.Open()
                Dim sql As String
                sql = ""

                'Dim command21 As MySqlCommand = New MySqlCommand

                'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

                ''Dim command1 As MySqlCommand = New MySqlCommand
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

                PopulateGLCodes()

                updPnlBillingRecs.Update()

                'If conn.State = ConnectionState.Open Then
                '    conn.Close()
                'End If

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim ToBillAmt As Decimal
                Dim DiscAmount As Decimal
                Dim GSTAmount As Decimal
                Dim NetAmount As Decimal
                Dim RetentionAmount As Decimal
                Dim RetentionGSTAmount As Decimal
                'Dim ToBillAmt As Decimal

                ToBillAmt = 0.0
                DiscAmount = 0.0
                GSTAmount = 0.0
                NetAmount = 0.0
                RetentionAmount = 0.0
                RetentionGSTAmount = 0.0

                For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
                    'string txSpareId = row.ItemArray[0] as string;
                    Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)

                    If (TextBoxchkSelect.Checked = True) Then
                        Dim qry As String
                        qry = ""
                        'Header
                        Dim command As MySqlCommand = New MySqlCommand
                        command.CommandType = CommandType.Text
                        rowselected = rowselected + 1

                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim lblid1 As TextBox = CType(grvServiceRecDetails.Rows(0).FindControl("txtAccountIdGV"), TextBox)

                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)


                        If rowselected = 1 Then
                            'Dim lblid1 As TextBox = CType(grvServiceRecDetails.Rows(0).FindControl("txtAccountIdGV"), TextBox)

                            If String.IsNullOrEmpty(txtInvoiceNo.Text) = True Then
                                'If vppPeriodCurrent = "" Then
                                '    Me.txtCalendarPeriod.Text = cls00Regional.Period_Calendar(Me.mskSalesDate.Text)
                                'Else
                                '    Me.txtCalendarPeriod.Text = vppPeriodCurrent
                                'End If
                                'If Me.txtCalendarPeriod.Text <> "" Then txtGlPeriod.Text = cls00Regional.Period_Accounting(txtCalendarPeriod.Text)

                                GenerateInvoiceNo()
                            End If
                        End If
                        'Dim conn As MySqlConnection = New MySqlConnection()

                        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        'conn.Open()


                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        'btnSave_Click(sender, e)


                        Dim lblid31 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtAccountIdGV"), TextBox)
                        Dim lblid2 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtClientNameGV"), TextBox)
                        Dim lblid3 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtLocationIdGV"), TextBox)
                        Dim lblid4 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceRecordNoGV"), TextBox)
                        Dim lblid5 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceDateGV"), TextBox)
                        Dim lblid6 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtBillingFrequencyGV"), TextBox)
                        Dim lblid7 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim lblid8 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtDeptGV"), TextBox)
                        Dim lblid9 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtStatusGV"), TextBox)
                        Dim lblid20 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtContractNoGV"), TextBox)
                        Dim lblid21 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceAddressGV"), TextBox)
                        'Dim lblid22 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceDateGV"), TextBox)
                        Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtToBillAmtGV"), TextBox)
                        Dim lblid22 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtRetentionAmtGV"), TextBox)

                        Dim lblidDiscAmount41 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
                        Dim lblidGSTAmt42 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtGSTAmountGV"), TextBox)
                        Dim lblidNetAmount43 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtNetAmountGV"), TextBox)

                        'Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtGSTAmountGV"), TextBox)

                        ToBillAmt = ToBillAmt + Convert.ToDecimal(lblid23.Text)
                        'DiscAmount = DiscAmount + Convert.ToDecimal(lblidDiscAmount41.Text)
                        'GSTAmount = GSTAmount + Convert.ToDecimal(lblidGSTAmt42.Text)
                        'NetAmount = NetAmount + Convert.ToDecimal(lblidNetAmount43.Text)

                        If String.IsNullOrEmpty(lblidGSTAmt42.Text) = True Then
                            'command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(lblid23.Text))
                            DiscAmount = 0.0
                            GSTAmount = GSTAmount + Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
                            'command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                            NetAmount = NetAmount + (Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))

                            RetentionGSTAmount = RetentionGSTAmount + Convert.ToDecimal(lblid22.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
                            RetentionAmount = RetentionAmount + (Convert.ToDecimal(lblid22.Text))
                        Else
                            'command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(lblid23.Text))
                            'DiscAmount = DiscAmount + Convert.ToDecimal(lblidDiscAmount41.Text)
                            DiscAmount = 0.0
                            GSTAmount = GSTAmount + Convert.ToDecimal(lblidGSTAmt42.Text)
                            'command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblidNetAmount43.Text))
                            NetAmount = NetAmount + Convert.ToDecimal(lblidNetAmount43.Text)

                            RetentionGSTAmount = RetentionGSTAmount + Convert.ToDecimal(lblid22.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
                            RetentionAmount = RetentionAmount + (Convert.ToDecimal(lblid22.Text))
                        End If


                        If String.IsNullOrEmpty(lblidDiscAmount41.Text) = True Then
                            lblidDiscAmount41.Text = "0.00"
                        End If

                        If String.IsNullOrEmpty(lblidGSTAmt42.Text) = True Then
                            lblidGSTAmt42.Text = "0.00"
                        End If

                        If String.IsNullOrEmpty(lblidNetAmount43.Text) = True Then
                            lblidNetAmount43.Text = "0.00"
                        End If

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
                                qry = qry + " ContractGroup, Status, Address1,  RetentionAmt, "
                                qry = qry + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qry = qry + " (@AccountId, @ClientName, @LocationId, @AccountName, @ServiceRecordNo, @BillAddress1, @BillBuilding, @BillStreet, @BillCountry, @BillPostal, "
                                qry = qry + " @ServiceDate, @BillAmount, @DiscountAmount,  @GSTAmount, @TotalWithGST, @NetAmount, @OurRef, @YourRef, @ContractNo, @PoNo, @RcnoServiceRecord, @BillingFrequency, @Salesman, @ContactType, @CompanyGroup,   "
                                qry = qry + " @ContractGroup, @Status,  @Address1,  @RetentionAmt,"
                                qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                command.CommandText = qry
                                command.Parameters.Clear()

                                command.Parameters.AddWithValue("@AccountId", lblid31.Text)
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
                                'Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtGSTAmountGV"), TextBox)
                                'Dim lbd30 As String = TextBoxGSTAmount.Text

                                'If String.IsNullOrEmpty(TextBoxGSTAmount.Text) = True Then
                                command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(lblid23.Text))
                                command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                                command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                                command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                                command.Parameters.AddWithValue("@NetAmount", (Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)))
                                command.Parameters.AddWithValue("@RetentionAmt", Convert.ToDecimal(lblid22.Text))
                                'Else
                                'command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(ToBillAmt))
                                'command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                'command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                                'command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(NetAmount))
                                'command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(NetAmount))
                                'End If

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

                                    '''''''''''''''''''''
                                    qry = "Update tblServiceBillingDetail set BatchNo = '" & txtBatchNo.Text & "' where rcno = " & txtBatchNo.Text

                                    command.CommandText = qry
                                    command.Parameters.Clear()
                                    command.Connection = conn
                                    command.ExecuteNonQuery()
                                    ''''''''''''''''''''''
                                End If

                                '''''''' Detail

                                ''Start:Detail
                                ' First Record

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
                                    command1.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
                                End If

                                command1.Connection = conn

                                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                                Dim dt1 As New DataTable
                                dt1.Load(dr1)

                                Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                                commandSalesDetail.CommandType = CommandType.Text
                                Dim qryDetail As String = "INSERT INTO tblServiceBillingDetailItem(RcnoServiceBillingDetail,Itemtype, ItemCode, Itemdescription, UOM, Qty, PricePerUOM, TotalPrice,DiscPerc, DiscAmount, PriceWithDisc, GSTPerc, GSTAmt, TotalPriceWithGST, "
                                qryDetail = qryDetail + "  TaxType, ARCode, GSTCode, OtherCode, GLDescription,  RcnoServiceRecord, BatchNo,  CompanyGroup, ContractNo, ServiceStatus, ContractGroup, ServiceRecordNo, ServiceDate, InvoiceType, RetentionAmount,"
                                qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qryDetail = qryDetail + "(@RcnoServiceBillingDetail, @Itemtype, @ItemCode, @Itemdescription, @UOM, @Qty, @PricePerUOM, @TotalPrice, @DiscPerc, @DiscAmount, @PriceWithDisc, @GSTPerc, @GSTAmt, @TotalPriceWithGST,"
                                qryDetail = qryDetail + " @TaxType, @ARCode, @GSTCode,  @OtherCode,@GLDescription, @RcnoServiceRecord, @BatchNo, @CompanyGroup, @ContractNo,  @ServiceStatus, @ContractGroup, @ServiceRecordNo, @ServiceDate, @InvoiceType, @RetentionAmount,"
                                qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                command.CommandText = qryDetail
                                command.Parameters.Clear()

                                command.Parameters.AddWithValue("@RcnoServiceBillingDetail", Convert.ToInt64(txtRcnotblServiceBillingDetail.Text))
                                command.Parameters.AddWithValue("@Itemtype", "SERVICE")
                                command.Parameters.AddWithValue("@ItemCode", "IN-SRV")
                                command.Parameters.AddWithValue("@Itemdescription", dt1.Rows(0)("Description").ToString())
                                command.Parameters.AddWithValue("@UOM", "")
                                command.Parameters.AddWithValue("@Qty", 1)
                                command.Parameters.AddWithValue("@PricePerUOM", Convert.ToDecimal(lblid23.Text))
                                'command.Parameters.AddWithValue("@BillAmount", 0.0)
                                command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(lblid23.Text))
                                command.Parameters.AddWithValue("@DiscPerc", 0.0)
                                command.Parameters.AddWithValue("@DiscAmount", 0.0)
                                command.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(lblid23.Text))
                                command.Parameters.AddWithValue("@GSTPerc", Convert.ToDecimal(txtTaxRatePct.Text))
                                command.Parameters.AddWithValue("@GSTAmt", Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                                command.Parameters.AddWithValue("@TotalPriceWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                                command.Parameters.AddWithValue("@RetentionAmount", 0.0)

                                command.Parameters.AddWithValue("@TaxType", "SR")
                                command.Parameters.AddWithValue("@RcnoServiceRecord", Convert.ToInt64(lblid7.Text))
                                command.Parameters.AddWithValue("@ARCode", "")
                                command.Parameters.AddWithValue("@GSTCode", "")
                                command.Parameters.AddWithValue("@OtherCode", dt1.Rows(0)("COACode").ToString())
                                command.Parameters.AddWithValue("@GLDescription", dt1.Rows(0)("COADescription").ToString())
                                command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                                'command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                                command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
                                command.Parameters.AddWithValue("@ServiceStatus", lblid9.Text)
                                command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                                command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)

                                command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                                'If TextBoxItemTypeGV.Text = "SERVICE" Then
                                command.Parameters.AddWithValue("@InvoiceType", "PROGRESS CLAIM")
                                'Else
                                '    command.Parameters.AddWithValue("@InvoiceType", TextBoxItemTypeGV.Text)
                                'End If
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

                                If String.IsNullOrEmpty(txtBatchNo.Text) = True Or txtBatchNo.Text = "0" Then
                                    txtBatchNo.Text = txtRcnotblServiceBillingDetail.Text

                                    '''''''''''''''''''''

                                    qry = "Update tblServiceBillingDetail set BatchNo = '" & txtBatchNo.Text & "' where rcno = " & txtBatchNo.Text

                                    command.CommandText = qry
                                    command.Parameters.Clear()
                                    command.Connection = conn
                                    command.ExecuteNonQuery()
                                    ''''''''''''''''''''''
                                End If

                                '2nd Record


                                Dim command12 As MySqlCommand = New MySqlCommand
                                command12.CommandType = CommandType.Text

                                'If lblid9.Text = "P" Then
                                '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode = 'IN-SRV'"
                                'ElseIf lblid9.Text = "O" Then
                                'command12.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'and ProductCode = 'IN-RET'"
                                command12.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-RET'"
                                'End If

                                command12.Connection = conn

                                Dim dr12 As MySqlDataReader = command12.ExecuteReader()
                                Dim dt12 As New DataTable
                                dt12.Load(dr12)

                                Dim commandSalesDetail12 As MySqlCommand = New MySqlCommand

                                commandSalesDetail.CommandType = CommandType.Text
                                qryDetail = "INSERT INTO tblServiceBillingDetailItem(RcnoServiceBillingDetail,Itemtype, ItemCode, Itemdescription, UOM, Qty,  "
                                qryDetail = qryDetail + " PricePerUOM, TotalPrice,DiscPerc, DiscAmount, PriceWithDisc, GSTPerc, GSTAmt, TotalPriceWithGST, TaxType, ARCode, GSTCode, OtherCode, GLDescription,  RcnoServiceRecord, BatchNo,  CompanyGroup, ContractNo, ServiceStatus, ContractGroup, ServiceRecordNo, ServiceDate, InvoiceType, RetentionAmount, "
                                qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qryDetail = qryDetail + "(@RcnoServiceBillingDetail, @Itemtype, @ItemCode, @Itemdescription, @UOM, @Qty,"
                                qryDetail = qryDetail + " @PricePerUOM, @TotalPrice, @DiscPerc, @DiscAmount, @PriceWithDisc, @GSTPerc, @GSTAmt, @TotalPriceWithGST, @TaxType, @ARCode, @GSTCode,  @OtherCode,@GLDescription, @RcnoServiceRecord, @BatchNo, @CompanyGroup, @ContractNo,  @ServiceStatus, @ContractGroup, @ServiceRecordNo, @ServiceDate, @InvoiceType, @RetentionAmount, "
                                qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                command.CommandText = qryDetail
                                command.Parameters.Clear()

                                command.Parameters.AddWithValue("@RcnoServiceBillingDetail", Convert.ToInt64(txtRcnotblServiceBillingDetail.Text))
                                command.Parameters.AddWithValue("@Itemtype", "SERVICE")
                                command.Parameters.AddWithValue("@ItemCode", dt12.Rows(0)("ProductCode").ToString())
                                command.Parameters.AddWithValue("@Itemdescription", dt12.Rows(0)("Description").ToString())
                                command.Parameters.AddWithValue("@UOM", "")
                                command.Parameters.AddWithValue("@Qty", 1)

                                'command.Parameters.AddWithValue("@PricePerUOM", Convert.ToDecimal(lblid22.Text))
                                command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(lblid22.Text))
                                'command.Parameters.AddWithValue("@DiscPerc", 0.0)
                                'command.Parameters.AddWithValue("@DiscAmount", 0.0)
                                'command.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(lblid22.Text))
                                'command.Parameters.AddWithValue("@GSTPerc", Convert.ToDecimal(txtTaxRatePct.Text))
                                'command.Parameters.AddWithValue("@GSTAmt", Convert.ToDecimal(lblid22.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                                'command.Parameters.AddWithValue("@TotalPriceWithGST", Convert.ToDecimal(lblid22.Text) + (Convert.ToDecimal(lblid22.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))

                                command.Parameters.AddWithValue("@PricePerUOM", 0.0)
                                'command.Parameters.AddWithValue("@TotalPrice", 0.0)
                                command.Parameters.AddWithValue("@DiscPerc", 0.0)
                                command.Parameters.AddWithValue("@DiscAmount", 0.0)
                                command.Parameters.AddWithValue("@PriceWithDisc", 0.0)
                                command.Parameters.AddWithValue("@GSTPerc", 0.0)
                                command.Parameters.AddWithValue("@GSTAmt", 0.0)
                                command.Parameters.AddWithValue("@TotalPriceWithGST", 0.0)


                                command.Parameters.AddWithValue("@RetentionAmount", Convert.ToDecimal(lblid22.Text))


                                command.Parameters.AddWithValue("@TaxType", "SR")
                                command.Parameters.AddWithValue("@RcnoServiceRecord", Convert.ToInt64(lblid7.Text))
                                command.Parameters.AddWithValue("@ARCode", "")
                                command.Parameters.AddWithValue("@GSTCode", "")
                                command.Parameters.AddWithValue("@OtherCode", dt12.Rows(0)("COACode").ToString())
                                command.Parameters.AddWithValue("@GLDescription", dt12.Rows(0)("COADescription").ToString())
                                command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                                'command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                                command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
                                command.Parameters.AddWithValue("@ServiceStatus", lblid9.Text)
                                command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                                command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)

                                command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                                'If TextBoxItemTypeGV.Text = "SERVICE" Then
                                command.Parameters.AddWithValue("@InvoiceType", "PROGRESS CLAIM")
                                'Else
                                '    command.Parameters.AddWithValue("@InvoiceType", TextBoxItemTypeGV.Text)
                                'End If
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

                                '2nd Record

                                '''''''' Detail
                            End If 'end of If dtExistServiceBillingDetail.Rows.Count = 0 Then
                        Else
                            If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
                                txtRcnotblServiceBillingDetail.Text = 0
                            End If
                            If Convert.ToInt64(txtRcnotblServiceBillingDetail.Text) > 0 Then
                                qry = "Update tblServiceBillingDetail set BillAmount = @BillAmount, DiscountAmount= @DiscountAmount,  GSTAmount =@GSTAmount,  "
                                qry = qry + "TotalWithGST = @TotalWithGST, NetAmount =@NetAmount, OurRef = @OurRef ,YourRef =@YourRef, PoNo =@PoNo, Salesman =@Salesman,    "
                                qry = qry + " LastModifiedBy =@LastModifiedBy,LastModifiedOn = @LastModifiedOn; "
                                qry = qry + " where rcno =" & txtRcnotblServiceBillingDetail.Text

                                command.CommandText = qry
                                command.Parameters.Clear()

                                command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(ToBillAmt))
                                command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                                command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(NetAmount))
                                command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(NetAmount))
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


                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                        'Dim command As MySqlCommand = New MySqlCommand

                        'command.CommandType = CommandType.Text
                        'Dim qry As String

                        If String.IsNullOrEmpty(txtRcno.Text) = True Then
                            txtRcno.Text = "0"
                        End If
                        If txtMode.Text = "NEW" And Convert.ToInt64(txtRcno.Text) = 0 Then
                            qry = "INSERT INTO tblSales(InvoiceNumber, CustName, AccountId, BillAddress1, BillBuilding, BillStreet, BillPostal,  "
                            qry = qry + "  ServiceRecordNo, SalesDate, OurRef,YourRef, PoNo, RcnoServiceRecord,  AppliedBase, Salesman, CreditTerms, CreditDays, "
                            qry = qry + "  DiscountAmount, GSTAmount, NetAmount, Comments, ContactType, CompanyGroup, GLPeriod, AmountWithDiscount, BatchNo, RetentionAmount, RetentionGST, STBilling , RecurringInvoice, "
                            qry = qry + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                            qry = qry + "(@InvoiceNumber, @ClientName, @AccountId, @BillAddress1, @BillBuilding, @BillStreet, @BillPostal, "
                            qry = qry + "@ServiceRecordNo, @SalesDate, @ourRef, @YourRef,  @PoNo, @RcnoServiceRecord, @AppliedBase, @Salesman, @CreditTerms, @CreditDays,"
                            qry = qry + " @DiscountAmount, @GSTAmount, @NetAmount, @Comments, @ContactType, @CompanyGroup, @GLPeriod, @AmountWithDiscount, @BatchNo, @RetentionAmount, @RetentionGST, @STBilling, @RecurringInvoice, "
                            qry = qry + "@CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                            command.CommandText = qry
                            command.Parameters.Clear()

                            command.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                            command.Parameters.AddWithValue("@ClientName", txtAccountName.Text)
                            command.Parameters.AddWithValue("@AccountId", lblid1.Text)
                            command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text)
                            command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text)
                            command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text)
                            command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text)

                            If txtInvoiceDate.Text.Trim = "" Then
                                command.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                            End If

                            'command.Parameters.AddWithValue("@SalesDate", Now)
                            'command.Parameters.AddWithValue("@BillAmount", 0.0)
                            command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
                            command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
                            command.Parameters.AddWithValue("@Comments", txtComments.Text)

                            command.Parameters.AddWithValue("@ServiceRecordNo", txtRecordNo.Text)

                            If String.IsNullOrEmpty(txtRcnoServiceRecord.Text) = False Then
                                command.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                            Else
                                command.Parameters.AddWithValue("@RcnoServiceRecord", 0)
                            End If

                            'command.Parameters.AddWithValue("@terms", ddlCreditTerms.Text)
                            command.Parameters.AddWithValue("@PoNo", txtPONo.Text)


                            'command.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(txtInvoiceAmount.Text))
                            'command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                            'command.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text))
                            'command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                            'command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))
                            'command.Parameters.AddWithValue("@RetentionAmount", Convert.ToDecimal(txtRetentionAmount.Text))
                            'command.Parameters.AddWithValue("@RetentionGST", Convert.ToDecimal(txtRetentionGSTAmount.Text))

                            command.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(ToBillAmt))
                            command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                            command.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) + Convert.ToDecimal(DiscAmount))
                            command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                            command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(NetAmount))
                            command.Parameters.AddWithValue("@RetentionAmount", Convert.ToDecimal(RetentionAmount))
                            command.Parameters.AddWithValue("@RetentionGST", Convert.ToDecimal(RetentionGSTAmount))

                            command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                            command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                            command.Parameters.AddWithValue("@GLPeriod", txtBillingPeriod.Text)
                            command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                            command.Parameters.AddWithValue("@STBilling", "Y")
                            If ddlSalesmanBilling.Text = "-1" Then
                                command.Parameters.AddWithValue("@Salesman", "")
                            Else
                                command.Parameters.AddWithValue("@Salesman", ddlSalesmanBilling.Text)
                            End If

                            If ddlCreditTerms.Text = "-1" Then
                                command.Parameters.AddWithValue("@CreditTerms", "")
                            Else
                                command.Parameters.AddWithValue("@CreditTerms", ddlCreditTerms.Text)
                            End If

                            If String.IsNullOrEmpty(txtCreditDays.Text) = False Then
                                command.Parameters.AddWithValue("@CreditDays", txtCreditDays.Text)
                            Else
                                command.Parameters.AddWithValue("@CreditDays", 0)
                            End If

                            If chkRecurringInvoice.Checked = True Then
                                command.Parameters.AddWithValue("@RecurringInvoice", "Y")
                            Else
                                command.Parameters.AddWithValue("@RecurringInvoice", "N")
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
                            sqlLastId = "SELECT last_insert_id() from tblSales"

                            Dim commandRcno As MySqlCommand = New MySqlCommand
                            commandRcno.CommandType = CommandType.Text
                            commandRcno.CommandText = sqlLastId
                            commandRcno.Parameters.Clear()
                            commandRcno.Connection = conn
                            txtRcno.Text = commandRcno.ExecuteScalar()


                        Else

                            qry = "Update tblSales set InvoiceNumber = @InvoiceNumber, CustName =@ClientName, AccountId =@AccountId, BillAddress1 =@BillAddress1, BillBuilding =@BillBuilding,   "
                            qry = qry + " BillStreet = @BillStreet, BillPostal= @BillPostal, ServiceRecordNo = @ServiceRecordNo,  SalesDate =@SalesDate,   "
                            qry = qry + " OurRef = @ourRef, YourRef =@YourRef, PoNo = @PoNo,  AppliedBase = @AppliedBase, Salesman = @Salesman, CreditTerms = @CreditTerms, CreditDays = @CreditDays, "
                            qry = qry + " DiscountAmount = @DiscountAmount, GSTAmount = @GSTAmount, NetAmount = @NetAmount, Comments = @Comments, ContactType = @ContactType, CompanyGroup = @CompanyGroup,  GLPeriod = @GLPeriod, AmountWithDiscount = @AmountWithDiscount, RetentionAmount =@RetentionAmount, RetentionGST = @RetentionGST, RecurringInvoice = @RecurringInvoice, "
                            qry = qry + " LastModifiedBy = @LastModifiedBy, LastModifiedOn = @LastModifiedOn "
                            qry = qry + " where Rcno = @Rcno;"

                            command.CommandText = qry
                            command.Parameters.Clear()

                            command.Parameters.AddWithValue("@Rcno", Convert.ToInt64(txtRcno.Text))
                            command.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                            command.Parameters.AddWithValue("@ClientName", txtAccountName.Text)
                            command.Parameters.AddWithValue("@AccountId", lblid1.Text)
                            command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text)
                            command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text)
                            command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text)
                            command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text)

                            If txtInvoiceDate.Text.Trim = "" Then
                                command.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                            Else
                                command.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                            End If

                            'command.Parameters.AddWithValue("@SalesDate", Now)
                            'command.Parameters.AddWithValue("@BillAmount", 0.0)
                            command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
                            command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
                            command.Parameters.AddWithValue("@Comments", txtComments.Text)

                            command.Parameters.AddWithValue("@ServiceRecordNo", txtRecordNo.Text)
                            'command.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                            'command.Parameters.AddWithValue("@terms", ddlCreditTerms.Text)
                            command.Parameters.AddWithValue("@PoNo", txtPONo.Text)

                            'command.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(txtInvoiceAmount.Text))
                            'command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                            'command.Parameters.AddWithValue("@AmountWithDiscount", 0.0)
                            'command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                            'command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))
                            'command.Parameters.AddWithValue("@RetentionAmount", Convert.ToDecimal(txtRetentionAmount.Text))
                            'command.Parameters.AddWithValue("@RetentionGST", Convert.ToDecimal(txtRetentionGSTAmount.Text))

                            command.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(ToBillAmt))
                            command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                            command.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) + Convert.ToDecimal(DiscAmount))
                            command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                            command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(NetAmount))
                            command.Parameters.AddWithValue("@RetentionAmount", Convert.ToDecimal(RetentionAmount))
                            command.Parameters.AddWithValue("@RetentionGST", Convert.ToDecimal(RetentionGSTAmount))

                            command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                            command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                            command.Parameters.AddWithValue("@GLPeriod", txtBillingPeriod.Text)

                            If ddlSalesmanBilling.Text = "-1" Then
                                command.Parameters.AddWithValue("@Salesman", "")
                            Else
                                command.Parameters.AddWithValue("@Salesman", ddlSalesmanBilling.Text)
                            End If

                            If ddlCreditTerms.Text = "-1" Then
                                command.Parameters.AddWithValue("@CreditTerms", "")
                            Else
                                command.Parameters.AddWithValue("@CreditTerms", ddlCreditTerms.Text)
                            End If

                            If String.IsNullOrEmpty(txtCreditDays.Text) = False Then
                                command.Parameters.AddWithValue("@CreditDays", txtCreditDays.Text)
                            Else
                                command.Parameters.AddWithValue("@CreditDays", 0)
                            End If

                            If chkRecurringInvoice.Checked = True Then
                                command.Parameters.AddWithValue("@RecurringInvoice", "Y")
                            Else
                                command.Parameters.AddWithValue("@RecurringInvoice", "N")
                            End If
                            'command.Parameters.AddWithValue("@CreditDays", txtCreditDays.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                            command.Connection = conn
                            command.ExecuteNonQuery()
                        End If

                        'End If  'rowIndex =0
                        'Header

                        Dim commandServiceBillingDetail As MySqlCommand = New MySqlCommand
                        commandServiceBillingDetail.CommandType = CommandType.Text
                        Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & txtInvoiceNo.Text & "',  BatchNo = '" & txtBatchNo.Text & "', rcnoInvoice = " & Convert.ToInt64(txtRcno.Text) & "  where RcnoServiceRecord =" & TextBoxRcnoServiceRecord.Text
                        commandServiceBillingDetail.CommandText = sqlUpdateServiceBillingDetail
                        commandServiceBillingDetail.Parameters.Clear()
                        commandServiceBillingDetail.Connection = conn
                        commandServiceBillingDetail.ExecuteNonQuery()


                        Dim commandServiceBillingDetailItem As MySqlCommand = New MySqlCommand
                        commandServiceBillingDetailItem.CommandType = CommandType.Text
                        Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & txtInvoiceNo.Text & "' where BatchNo = '" & txtBatchNo.Text & "'"
                        commandServiceBillingDetailItem.CommandText = sqlUpdateServiceBillingDetailItem
                        commandServiceBillingDetailItem.Parameters.Clear()
                        commandServiceBillingDetailItem.Connection = conn
                        commandServiceBillingDetailItem.ExecuteNonQuery()

                        'Start: Update tblServiceRecord
                        Dim command4 As MySqlCommand = New MySqlCommand
                        command4.CommandType = CommandType.Text

                        'Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y', BilledAmt = " & Convert.ToDecimal(txtAmountWithDiscount.Text) & ", BillNo = '" & txtInvoiceNo.Text & "' where Rcno= @Rcno "
                        Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y' where Rcno= @Rcno "

                        command4.CommandText = qry4
                        command4.Parameters.Clear()

                        command4.Parameters.AddWithValue("@Rcno", TextBoxRcnoServiceRecord.Text)
                        command4.Connection = conn
                        command4.ExecuteNonQuery()

                        'End: Update tblServiceRecord
                    End If

                Next rowIndex

                'txtInvoiceAmount.Text = Convert.ToDecimal(ToBillAmt)
                'txtDiscountAmount.Text = Convert.ToDecimal(DiscAmount)
                'txtAmountWithDiscount.Text = (Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount)).ToString("N2")
                'txtGSTAmount.Text = Convert.ToDecimal(GSTAmount)
                'txtNetAmount.Text = Convert.ToDecimal(NetAmount)
                ''txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString

                updPnlBillingRecs.Update()


                '''''''''''''''''''''''''''''''

                CalculateSumAmounts()
                'txtInvoiceAmount.Text = ToBillAmt.ToString("N2")
                ''txtDiscountAmount.Text = TotalDiscountAmount.ToString("N2")

                ''txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString("N2")
                ''txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text)).ToString("N2")
                'txtGSTAmount.Text = Convert.ToDecimal(GSTAmount)
                'txtNetAmount.Text = Convert.ToDecimal(NetAmount)
                'txtRetentionAmount.Text = RetentionAmount.ToString("N2")
                'txtRetentionGSTAmount.Text = RetentionGSTAmount.ToString("N2")
                ''txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString

                ''''''''''''''''''''''''''''''
                updPnlBillingRecs.Update()
                'End: Update Invoice Amounts

                '''''''''''''''''''''''''''''''''''''
                DisplayGLGrid()

                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If


            'MakeMeNull()

            If rowselected = 0 Then
                lblAlert.Text = "PLEASE SELECT A RECORD"
                Exit Sub
            End If


            lblMessage.Text = "ADD: INVOICE RECORD SUCCESSFULLY ADDED"
            lblAlert.Text = ""

            GridView1.DataSourceID = "SQLDSInvoice"
            GridView1.DataBind()

            conn.Close()
            'txtBatchNo.Text = "0"
            DisableControls()
            'FirstGridViewRowServiceRecs()
            btnSaveInvoice.Enabled = False
            updPnlMsg.Update()
            updPnlSearch.Update()
            updpnlServiceRecs.Update()
            updPnlBillingRecs.Update()
            UpdatePanel1.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub CalculateSumAmounts()
        Try
            Dim lTotalPriceInvoice As Decimal
            Dim lTotalPriceRetention As Decimal
            Dim lTotalPriceGSTInvoice As Decimal
            Dim lTotalPriceGSTRetention As Decimal

            lTotalPriceInvoice = 0.0
            lTotalPriceRetention = 0.0
            lTotalPriceGSTInvoice = 0.0
            lTotalPriceGSTRetention = 0.0


            Dim command1 As MySqlCommand = New MySqlCommand
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()


            command1.CommandType = CommandType.Text


            'Invoice Amount
            command1.CommandText = "SELECT sum(TotalPrice) as TotalPriceInvoiceSum FROM tblservicebillingdetailitem where InvoiceNo = '" & txtInvoiceNo.Text & "' and (ItemCode = 'IN-SRV' or ItemCode = 'IN-DEF')"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)


            If String.IsNullOrEmpty(dt1.Rows(0)("TotalPriceInvoiceSum").ToString) = False Then
                lTotalPriceInvoice = dt1.Rows(0)("TotalPriceInvoiceSum").ToString
            End If




            'Invoice GST Amount
            command1.CommandText = "SELECT sum(GSTAmt) as TotalPriceGSTInvoiceSum FROM tblservicebillingdetailitem where InvoiceNo = '" & txtInvoiceNo.Text & "' and (ItemCode = 'IN-SRV' or ItemCode = 'IN-DEF')"
            command1.Connection = conn

            Dim dr2 As MySqlDataReader = command1.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr2)


            If String.IsNullOrEmpty(dt2.Rows(0)("TotalPriceGSTInvoiceSum").ToString) = False Then
                lTotalPriceGSTInvoice = dt2.Rows(0)("TotalPriceGSTInvoiceSum").ToString
            End If



            'Retention Invoice Amount
            command1.CommandText = "SELECT sum(TotalPrice) as TotalPriceRetentionSum FROM tblservicebillingdetailitem where InvoiceNo = '" & txtInvoiceNo.Text & "' and (ItemCode = 'IN-RET')"
            command1.Connection = conn

            Dim dr3 As MySqlDataReader = command1.ExecuteReader()
            Dim dt3 As New DataTable
            dt3.Load(dr3)

            If String.IsNullOrEmpty(dt3.Rows(0)("TotalPriceRetentionSum").ToString) = False Then
                lTotalPriceRetention = dt3.Rows(0)("TotalPriceRetentionSum").ToString
            End If

            'Retention GST Amount
            command1.CommandText = "SELECT sum(GSTAmt) as TotalPriceGSTRetentionSum FROM tblservicebillingdetailitem where InvoiceNo = '" & txtInvoiceNo.Text & "' and (ItemCode = 'IN-RET')"
            command1.Connection = conn

            Dim dr4 As MySqlDataReader = command1.ExecuteReader()
            Dim dt4 As New DataTable
            dt4.Load(dr4)

            If String.IsNullOrEmpty(dt4.Rows(0)("TotalPriceGSTRetentionSum").ToString) = False Then
                lTotalPriceGSTRetention = dt4.Rows(0)("TotalPriceGSTRetentionSum").ToString
            End If

            txtInvoiceAmount.Text = lTotalPriceInvoice.ToString("N2")
            ''txtDiscountAmount.Text = TotalDiscountAmount.ToString("N2")
            ''txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString("N2")
            ''txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text)).ToString("N2")
            txtGSTAmount.Text = lTotalPriceGSTInvoice.ToString
            txtNetAmount.Text = (lTotalPriceInvoice + lTotalPriceGSTInvoice).ToString("N2")
            txtRetentionAmount.Text = lTotalPriceRetention.ToString("N2")
            txtRetentionGSTAmount.Text = lTotalPriceGSTRetention.ToString("N2")

            ''txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString

            conn.Close()
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

            CalculatePrice()
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
        Dim lblid8 As DropDownList = CType(xgrvBillingDetails.FindControl("txtItemTypeGV"), DropDownList)
        Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtGLDescriptionGV"), TextBox)

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

                Dim TextBoxRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                If (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
                    If (Convert.ToInt32(TextBoxRcno.Text) > 0) Then

                        Dim conn As MySqlConnection = New MySqlConnection(constr)
                        conn.Open()

                        Dim commandUpdGS As MySqlCommand = New MySqlCommand
                        commandUpdGS.CommandType = CommandType.Text
                        commandUpdGS.CommandText = "Delete from tblservicebillingdetailitem where rcno = " & TextBoxRcno.Text
                        commandUpdGS.Connection = conn
                        commandUpdGS.ExecuteNonQuery()

                        conn.Close()
                    End If
                End If

                If dt.Rows.Count > 1 Then
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
            txtClientName.Text = gvLocation.SelectedRow.Cells(2).Text
            'lblid2.Text = txtServiceName.Text
        End If


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
            exstr = ex.ToString
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = exstr
            'Dim message As String = "alert('" + exstr + "')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        End Try
    End Sub

    Protected Sub txtAccountId_TextChanged(sender As Object, e As EventArgs) Handles txtAccountId.TextChanged
        Dim query As String
        query = "Select ContractNo from tblContract where AccountId = '" & txtAccountId.Text & "'"
        PopulateDropDownList(query, "ContractNo", "ContractNo", ddlContractNo)
    End Sub

    Protected Sub btnQuickReset_Click(sender As Object, e As EventArgs) Handles btnQuickReset.Click
        txtInvoicenoSearch.Text = ""
        txtAccountIdSearch.Text = ""
        txtBillingPeriodSearch.Text = ""
        txtClientNameSearch.Text = ""
        ddlCompanyGrpSearch.SelectedIndex = 0
        ddlSalesmanSearch.SelectedIndex = 0
        txtSearch1Status.Text = "O"
        ddlCompanyGrpSearch.SelectedIndex = 0

        btnQuickSearch_Click(sender, e)


        'btnSearch1Status_Click(sender, e)
    End Sub

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        lblAlert.Text = ""
        txtSearch.Text = ""
        If String.IsNullOrEmpty(ddlContactType.Text) Or ddlContactType.Text = "--SELECT--" Then
            '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
            lblAlert.Text = "SELECT CUSTOMER TYPE TO PROCEED"
            Exit Sub
        End If


        If String.IsNullOrEmpty(txtAccountId.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountId.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text

            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contactperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            updPanelInvoice.Update()
        Else

            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O'  order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O'  order by name"
            End If


            SqlDSClient.DataBind()
            gvClient.DataBind()
        End If
        mdlPopUpClient.Show()
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

    Protected Sub grvServiceRecDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvServiceRecDetails.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Dim status1 As TextBox = CType(e.Row.FindControl("txtStatusGV"), TextBox)
            ''status1.ForeColor = Color.Red


            'If status1.Text = "P" Then
            '    e.Row.ForeColor = Color.Red
            '    'e.Row.Cells(7).Text = "IN"
            '    'e.Row.Cells(2).BackColor = Color.Blue
            '    'status1.ForeColor = Color.Red
            '    'e.Row.Cells(7).ForeColor = Color.Red
            'End If

            'For Each cell As DataControlFieldCell In e.Row.Cells
            '    ' check all cells in one row
            '    For Each control As Control In cell.Controls

            '        Dim abc As TextBox = TryCast(e.Row.FindControl("txtStatusGV"), TextBox)

            '        If abc.Text = "P" Then
            '            e.Row.Cells(7).ForeColor = Drawing.Color.Blue

            '        End If
            '    Next control
            'Next cell
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

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then
                ''''''''''''''' Insert tblAR

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
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

                If Convert.ToDecimal(txtNetAmount.Text) > 0.0 Then
                    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName,VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, DueDate, GLtype, BillingPeriod, InvoiceType, SalesMan,"
                    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @DueDate, @GLtype, @BillingPeriod, @InvoiceType, @SalesMan,"
                    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    commandAR.CommandText = qryAR
                    commandAR.Parameters.Clear()
                    commandAR.Parameters.AddWithValue("@VoucherNumber", txtInvoiceNo.Text)
                    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)

                    If txtInvoiceDate.Text.Trim = "" Then
                        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                    Else
                        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    commandAR.Parameters.AddWithValue("@BillingPeriod", txtBillingPeriod.Text)
                    commandAR.Parameters.AddWithValue("@ContractNo", "")
                    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                    commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
                    commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
                    commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtNetAmount.Text))
                    commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
                    commandAR.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                    commandAR.Parameters.AddWithValue("@ModuleName", "Invoice")
                    commandAR.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(txtInvoiceDate.Text).AddDays(Convert.ToInt64(txtCreditDays.Text)).ToString("yyyy-MM-dd"))
                    commandAR.Parameters.AddWithValue("@GLType", "Debtor")
                    commandAR.Parameters.AddWithValue("@InvoiceType", "Progress Claim")

                    If ddlSalesmanBilling.SelectedIndex > 0 Then
                        commandAR.Parameters.AddWithValue("@SalesMan", ddlSalesmanBilling.Text)
                    Else
                        commandAR.Parameters.AddWithValue("@SalesMan", "")
                    End If
                    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    commandAR.Connection = conn
                    commandAR.ExecuteNonQuery()
                End If


                If Convert.ToDecimal(txtRetentionAmount.Text) > 0.0 Then
                    qryAR = "INSERT INTO tblAR( VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup,  ContractNo, ModuleName,  BillingPeriod, InvoiceType, SalesMan,  "
                    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ModuleName, @BillingPeriod, @InvoiceType, @SalesMan, "
                    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    commandAR.CommandText = qryAR
                    commandAR.Parameters.Clear()

                    commandAR.Parameters.AddWithValue("@VoucherNumber", txtInvoiceNo.Text)
                    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                    If txtInvoiceDate.Text.Trim = "" Then
                        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                    Else
                        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    commandAR.Parameters.AddWithValue("@BillingPeriod", txtBillingPeriod.Text)
                    commandAR.Parameters.AddWithValue("@ContractNo", "")
                    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                    commandAR.Parameters.AddWithValue("@GLCode", txtARCode10.Text)
                    commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription10.Text)
                    commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtRetentionAmount.Text) + Convert.ToDecimal(txtRetentionGSTAmount.Text))
                    commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
                    commandAR.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                    commandAR.Parameters.AddWithValue("@ModuleName", "Invoice")
                    commandAR.Parameters.AddWithValue("@InvoiceType", "Progress Claim")
                    If ddlSalesmanBilling.SelectedIndex > 0 Then
                        commandAR.Parameters.AddWithValue("@SalesMan", ddlSalesmanBilling.Text)
                    Else
                        commandAR.Parameters.AddWithValue("@SalesMan", "")
                    End If

                    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    commandAR.Connection = conn
                    commandAR.ExecuteNonQuery()
                End If


                If Convert.ToDecimal(txtGSTAmount.Text) > 0.0 Then
                    qryAR = "INSERT INTO tblAR( VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup,  ContractNo, ModuleName, BillingPeriod, InvoiceType, SalesMan, GLType, "
                    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ModuleName, @BillingPeriod, @InvoiceType, @SalesMan, @GLType, "
                    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    commandAR.CommandText = qryAR
                    commandAR.Parameters.Clear()

                    commandAR.Parameters.AddWithValue("@VoucherNumber", txtInvoiceNo.Text)
                    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                    If txtInvoiceDate.Text.Trim = "" Then
                        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                    Else
                        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    commandAR.Parameters.AddWithValue("@BillingPeriod", txtBillingPeriod.Text)
                    commandAR.Parameters.AddWithValue("@ContractNo", "")
                    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                    commandAR.Parameters.AddWithValue("@GLCode", txtGSTOutputCode.Text)
                    commandAR.Parameters.AddWithValue("@GLDescription", txtGSTOutputDescription.Text)
                    commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)
                    commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtGSTAmount.Text) + Convert.ToDecimal(txtRetentionGSTAmount.Text))
                    commandAR.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                    commandAR.Parameters.AddWithValue("@ModuleName", "Invoice")
                    commandAR.Parameters.AddWithValue("@InvoiceType", "Service")
                    If ddlSalesmanBilling.SelectedIndex > 0 Then
                        commandAR.Parameters.AddWithValue("@SalesMan", ddlSalesmanBilling.Text)
                    Else
                        commandAR.Parameters.AddWithValue("@SalesMan", "")
                    End If
                    commandAR.Parameters.AddWithValue("@GLType", "GSTOut")
                    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    commandAR.Connection = conn
                    commandAR.ExecuteNonQuery()
                End If


                'Start:Loop thru' Credit values


                Dim commandValues As MySqlCommand = New MySqlCommand

                commandValues.CommandType = CommandType.Text
                'commandValues.CommandText = "SELECT *  FROM tblservicebillingdetailitem where  ItemCode <> 'IN-RET' and BatchNo ='" & txtBatchNo.Text.Trim & "'"
                commandValues.CommandText = "SELECT *  FROM tblservicebillingdetailitem where   BatchNo ='" & txtBatchNo.Text.Trim & "'"
                commandValues.Connection = conn

                Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
                Dim dtValues As New DataTable
                dtValues.Load(drValues)

                For Each row As DataRow In dtValues.Rows

                    'If Convert.ToDecimal(row("PriceWithDisc")) > 0.0 Then
                    If Convert.ToDecimal(row("TotalPrice")) > 0.0 Then
                        'qryAR = "INSERT INTO tblAR( VoucherNumber,  AccountId, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ServiceStatus, RcnoServiceRecord, ContractGroup, ModuleName, ItemCode, ServiceRecordNo, "
                        qryAR = "INSERT INTO tblAR(  VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount,  "
                        qryAR = qryAR + " CreditAmount, BatchNo, CompanyGroup, ContractNo, ServiceStatus, RcnoServiceRecord, ContractGroup, BillingPeriod,  "
                        qryAR = qryAR + " ModuleName, ItemCode, ServiceRecordNo, ServiceDate, InvoiceType, SalesMan, ItemType, "
                        qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                        'qryAR = qryAR + " (@VoucherNumber, @AccountId, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ServiceStatus, @RcnoServiceRecord, @ContractGroup, @ModuleName, @ItemCode, @ServiceRecordNo, "
                        qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount,  "
                        qryAR = qryAR + " @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ServiceStatus, @RcnoServiceRecord, @ContractGroup,  @BillingPeriod,"
                        qryAR = qryAR + " @ModuleName, @ItemCode, @ServiceRecordNo, @ServiceDate, @InvoiceType, @SalesMan,  @ItemType,"
                        qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                        commandAR.CommandText = qryAR
                        commandAR.Parameters.Clear()

                        commandAR.Parameters.AddWithValue("@VoucherNumber", txtInvoiceNo.Text)
                        commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                        commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                        If txtInvoiceDate.Text.Trim = "" Then
                            commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                        Else
                            commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                        End If
                        commandAR.Parameters.AddWithValue("@BillingPeriod", txtBillingPeriod.Text)
                        commandAR.Parameters.AddWithValue("@ContractNo", row("ContractNo"))
                        commandAR.Parameters.AddWithValue("@RcnoServiceRecord", row("RcnoServiceRecord"))
                        commandAR.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                        commandAR.Parameters.AddWithValue("@GLCode", row("OtherCode"))
                        commandAR.Parameters.AddWithValue("@GLDescription", row("GLDescription"))
                        commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)

                        'commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtRetentionAmount.Text))
                        commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(row("PriceWithDisc")))
                        commandAR.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                        commandAR.Parameters.AddWithValue("@ServiceStatus", row("ServiceStatus"))
                        commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                        commandAR.Parameters.AddWithValue("@ContractGroup", row("ContractGroup"))
                        commandAR.Parameters.AddWithValue("@ModuleName", "Invoice")
                        commandAR.Parameters.AddWithValue("@ItemCode", row("ItemCode"))
                        commandAR.Parameters.AddWithValue("@ServiceRecordNo", row("ServiceRecordNo"))

                        'commandAR.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                        'Else
                        commandAR.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(row("ServiceDate")).ToString("yyyy-MM-dd"))
                        'End If
                        commandAR.Parameters.AddWithValue("@InvoiceType", "Service")
                        If ddlSalesmanBilling.SelectedIndex > 0 Then
                            commandAR.Parameters.AddWithValue("@SalesMan", ddlSalesmanBilling.Text)
                        Else
                            commandAR.Parameters.AddWithValue("@SalesMan", "")
                        End If

                        If row("ItemType") = "SERVICE" Then
                            commandAR.Parameters.AddWithValue("@ItemType", "PROGRESS CLIAM")
                        Else
                            commandAR.Parameters.AddWithValue("@ItemType", row("ItemType"))
                        End If
                        commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        commandAR.Connection = conn
                        commandAR.ExecuteNonQuery()


                        'Start: Update tblServiceRecord
                        If row("ItemCode") = "IN-DEF" Or row("ItemCode") = "IN-SRV" Then
                            Dim command4 As MySqlCommand = New MySqlCommand
                            command4.CommandType = CommandType.Text

                            Dim qry4 As String = "Update tblservicerecord Set BilledAmt = " & Convert.ToDecimal(row("PriceWithDisc")) & ", BillNo = '" & txtInvoiceNo.Text & "' where Rcno= @Rcno "
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
                Dim sqlUpdateInvoice As String = "Update tblsales set GLStatus = 'P'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

                commandUpdateInvoice.CommandText = sqlUpdateInvoice
                commandUpdateInvoice.Parameters.Clear()
                commandUpdateInvoice.Connection = conn
                commandUpdateInvoice.ExecuteNonQuery()

                GridView1.DataBind()


                Dim commandUpdateServicebillingdetail As MySqlCommand = New MySqlCommand
                commandUpdateServicebillingdetail.CommandType = CommandType.Text
                Dim sqlUpdateServicebillingdetail As String = "Update tblservicebillingdetail set Posted = 'P'  where InvoiceNo  ='" & txtInvoiceNo.Text & "'"

                commandUpdateServicebillingdetail.CommandText = sqlUpdateServicebillingdetail
                commandUpdateServicebillingdetail.Parameters.Clear()
                commandUpdateServicebillingdetail.Connection = conn
                commandUpdateServicebillingdetail.ExecuteNonQuery()

                Dim commandUpdateServicebillingdetailItem As MySqlCommand = New MySqlCommand
                commandUpdateServicebillingdetailItem.CommandType = CommandType.Text
                Dim sqlUpdateServicebillingdetailItem As String = "Update tblservicebillingdetailitem set Posted = 'P'  where InvoiceNo  ='" & txtInvoiceNo.Text & "'"

                commandUpdateServicebillingdetailItem.CommandText = sqlUpdateServicebillingdetailItem
                commandUpdateServicebillingdetailItem.Parameters.Clear()
                commandUpdateServicebillingdetailItem.Connection = conn
                commandUpdateServicebillingdetailItem.ExecuteNonQuery()

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
            End If
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

    Protected Sub btnReceipts_Click(sender As Object, e As EventArgs) Handles btnReceipts.Click
        Session("receiptfrom") = "invoicePB"

        If String.IsNullOrEmpty(txtInvoiceNo.Text) = False Then
            Session("invoiceno") = txtInvoiceNo.Text
        End If


        If String.IsNullOrEmpty(txtInvoiceNo.Text) = False Then
            'Session("contractno") = txtContractNo.Text
            'txt.Text = SQLDSContract.SelectCommand
            Session("gridsql") = txt.Text
            Session("rcno") = txtRcno.Text
            Session("AccountId") = txtAccountIdBilling.Text
            Session("AccountName") = txtAccountName.Text
            Session("ContactType") = ddlContactType.Text
            Session("CompanyGroup") = txtCompanyGroup.Text
            Session("Salesman") = ddlSalesmanBilling.Text
            'Session("Scheduler") = ddlScheduler.Text
            'Session("Team") = txtTeam.Text
            'Session("InCharge") = txtTeamIncharge.Text
            'Session("ServiceBy") = txtServiceBy.Text
            'Session("ScheduleType") = ddlScheduleType.Text
            '''''''''''''''''''''''''''''
        End If
        Response.Redirect("Receipts.aspx")
    End Sub

    Protected Sub btnClientSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientSearch.Click
        lblAlert.Text = ""
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
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contactperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            updPanelInvoice.Update()
        Else

            If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1  order by name"
            ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1  order by name"
            End If


            SqlDSClient.DataBind()
            gvClient.DataBind()
        End If
        mdlPopUpClient.Show()
    End Sub

    Protected Sub ddlCreditTerms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCreditTerms.SelectedIndexChanged
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
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
        conn.Close()
    End Sub

    Protected Sub txtInvoiceDate_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceDate.TextChanged
        txtBillingPeriod.Text = Year(Convert.ToDateTime(txtInvoiceDate.Text)) & Format(Month(Convert.ToDateTime(txtInvoiceDate.Text)), "00")
    End Sub


    ''''
    Protected Sub chkSelectGV_CheckedChanged(sender As Object, e As EventArgs)
        lblAlert.Text = ""

        Dim chk1 As CheckBox = DirectCast(sender, CheckBox)
        'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
        Dim xrow1 As GridViewRow = CType(chk1.NamingContainer, GridViewRow)

        Dim lblid16 As TextBox = CType(xrow1.FindControl("txtCompanyGroupGV"), TextBox)
        Dim lblid26 As TextBox = CType(xrow1.FindControl("txtContractNoGV"), TextBox)
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

        If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            If txtContractNo.Text <> lblid26.Text Then
                lblAlert.Text = "DIFFERENT CONTRACT NO. CANNOT BE SELECTED"
                updPnlMsg.Update()
                lblid27.Checked = False
                Exit Sub
            End If
        End If

        If String.IsNullOrEmpty(txtCompanyGroup.Text) = True Then

            Dim lblid1 As TextBox = CType(xrow1.FindControl("txtAccountIdGV"), TextBox)
            Dim lblid2 As TextBox = CType(xrow1.FindControl("txtAccountNameGV"), TextBox)
            Dim lblid3 As TextBox = CType(xrow1.FindControl("txtBillAddress1GV"), TextBox)
            Dim lblid4 As TextBox = CType(xrow1.FindControl("txtBillBuildingGV"), TextBox)
            Dim lblid5 As TextBox = CType(xrow1.FindControl("txtBillStreetGV"), TextBox)
            Dim lblid6 As TextBox = CType(xrow1.FindControl("txtBillCountryGV"), TextBox)
            Dim lblid7 As TextBox = CType(xrow1.FindControl("txtBillPostalGV"), TextBox)
            Dim lblid8 As TextBox = CType(xrow1.FindControl("txtOurReferenceGV"), TextBox)
            Dim lblid9 As TextBox = CType(xrow1.FindControl("txtYourReferenceGV"), TextBox)
            Dim lblid10 As TextBox = CType(xrow1.FindControl("txtPoNoGV"), TextBox)
            Dim lblid11 As TextBox = CType(xrow1.FindControl("txtCreditTermsGV"), TextBox)
            Dim lblid12 As TextBox = CType(xrow1.FindControl("txtSalesmanGV"), TextBox)
            Dim lblid14 As TextBox = CType(xrow1.FindControl("txtRcnoServiceRecordGV"), TextBox)
            Dim lblid15 As TextBox = CType(xrow1.FindControl("txtServiceRecordNoGV"), TextBox)
            Dim lblid17 As TextBox = CType(xrow1.FindControl("txtRcnoInvoiceGV"), TextBox)
            Dim lblid18 As TextBox = CType(xrow1.FindControl("txtRcnoServicebillingdetailGV"), TextBox)
            Dim lblid19 As TextBox = CType(xrow1.FindControl("txtContactTypeGV"), TextBox)
            Dim lblid20 As TextBox = CType(xrow1.FindControl("txtInvoiceDateGV"), TextBox)
            Dim lblid21 As TextBox = CType(xrow1.FindControl("txtDiscAmountGV"), TextBox)
            'Dim lblid23 As TextBox = CType(xrow1.FindControl("txtToBillAmtGV"), TextBox)
            Dim lblid24 As TextBox = CType(xrow1.FindControl("txtServiceRecordNoGV"), TextBox)
            Dim lblid25 As TextBox = CType(xrow1.FindControl("txtStatusGV"), TextBox)

            Dim lblid28 As TextBox = CType(xrow1.FindControl("txtSalesmanGV"), TextBox)

            'Label41.Text = "INVOICE DETAILS : " & lblid24.Text
            txtAccountType.Text = lblid19.Text
            txtCompanyGroup.Text = lblid16.Text
            'txtInvoiceDate.Text = lblid20.Text
            txtAccountIdBilling.Text = lblid1.Text
            txtAccountName.Text = lblid2.Text
            txtBillAddress.Text = lblid3.Text
            txtBillBuilding.Text = lblid4.Text
            txtBillStreet.Text = lblid5.Text
            txtBillCountry.Text = lblid6.Text
            txtBillPostal.Text = lblid7.Text
            txtOurReference.Text = lblid8.Text
            txtYourReference.Text = lblid9.Text
            txtPONo.Text = lblid10.Text
            txtContractNo.Text = lblid26.Text
            ddlSalesmanBilling.Text = lblid28.Text.ToUpper

            ''''''''''''''''''''''''''''''''
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT AgreeValue FROM tblcontract where ContractNo ='" & txtContractNo.Text & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                txtContractValue.Text = dt1.Rows(0)("AgreeValue").ToString
                txtAdjustedContractValue.Text = dt1.Rows(0)("AgreeValue").ToString
            End If



            Dim command2 As MySqlCommand = New MySqlCommand
            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT sum(PriceWithDisc) as previousclaim, sum(RetentionAmount) as previousretention FROM tblservicebillingdetailitem where ContractNo ='" & txtContractNo.Text & "'"
            command2.Connection = conn

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then
                txtPreviousClaim.Text = dt2.Rows(0)("previousclaim").ToString
                txtPreviousRetentionAmount.Text = dt2.Rows(0)("previousretention").ToString
            End If

            ''''''''''''''''''''''''''''''''
        End If

        If String.IsNullOrEmpty(txtPreviousClaim.Text) = True Then
            txtPreviousClaim.Text = "0.00"
        End If

        If String.IsNullOrEmpty(txtPreviousRetentionAmount.Text) = True Then
            txtPreviousRetentionAmount.Text = "0.00"
        End If
        '''''''''''''''''''''''

        SetRowDataServiceRecs()
        Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

        If tableAdd IsNot Nothing Then
            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'conn.Open()

            Dim TotalSelectedBillValue As Double
            Dim TotalSelectedRetentionValue As Double

            TotalSelectedBillValue = 0.0
            TotalSelectedRetentionValue = 0.0

            For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtToBillAmtGV"), TextBox)
                Dim lblid24 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtRetentionAmtGV"), TextBox)

                'Dim lblid13 As TextBox = CType(xrow1.FindControl("txtToBillAmtGV"), TextBox)
                'Dim lblid13 As TextBox = CType(xrow1.FindControl("txtToBillAmtGV"), TextBox)

                If (TextBoxchkSelect.Checked = True) Then
                    TotalSelectedBillValue = TotalSelectedBillValue + Convert.ToDecimal(lblid23.Text)
                    TotalSelectedRetentionValue = TotalSelectedRetentionValue + Convert.ToDecimal(lblid24.Text)
                End If

            Next rowIndex


            txtInvoiceAmount.Text = (TotalSelectedBillValue).ToString("N2")
            txtRetentionAmount.Text = (TotalSelectedRetentionValue).ToString("N2")
        End If


        txtGSTAmount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")
        txtNetAmount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtGSTAmount.Text)).ToString("N2")



        txtTotalClaim.Text = (Convert.ToDecimal(txtPreviousClaim.Text) + Convert.ToDecimal(txtInvoiceAmount.Text)).ToString("N2")
        txtTotalRetentionAmount.Text = (Convert.ToDecimal(txtPreviousRetentionAmount.Text) + Convert.ToDecimal(txtRetentionAmount.Text)).ToString("N2")
        '''''''''''''''''''''''

        btnSaveInvoice.Enabled = True
        updPanelSave.Update()
        updPanelInvoice.Update()
    End Sub
    Protected Sub CheckUncheckAll(sender As Object, e As System.EventArgs)
        Dim chk1 As CheckBox
        chk1 = DirectCast(grvServiceRecDetails.HeaderRow.Cells(0).FindControl("CheckBox1"), CheckBox)
        For Each row As GridViewRow In grvServiceRecDetails.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelectGV"), CheckBox)
            chk.Checked = chk1.Checked
        Next
    End Sub

    ''''

    Protected Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Try
            Dim rowIndex As Integer = 0
            Dim Total As Decimal
            Dim TotalWithGST As Decimal
            Dim TotalDiscAmt As Decimal
            Dim TotalGSTAmt As Decimal
            Dim TotalPriceWithDiscountAmt As Decimal

            Dim TextBoxGSTAmt As TextBox
            Dim TextBoxTotalPrice As TextBox
            Dim TextBoxTotalPriceWithGST As TextBox
            Dim TextBoxDiscAmt As TextBox
            Dim TextBoxPriceWithDisc As TextBox

            Dim TextBoxTotalPrice10 As TextBox
            Dim TextBoxTotalPriceWithGST10 As TextBox
            Dim TextBoxGSTAmt10 As TextBox
            Dim TextBoxRetentionGSTAmt10 As TextBox

            Total = 0.0
            TotalWithGST = 0.0
            TotalDiscAmt = 0.0
            TotalGSTAmt = 0.0
            TotalPriceWithDiscountAmt = 0.0

            Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)


            If (table.Rows.Count > 0) Then
                FirstGridViewRowBillingDetailsRecs()
                For i As Integer = 0 To (table.Rows.Count) - 1

                    Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)

                    If (TextBoxchkSelect.Checked = True) Then

                        Dim TextBoxInvoiceAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtToBillAmtGV"), TextBox)
                        Dim TextBoxDiscountAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtDiscAmountGV"), TextBox)
                        Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtGSTAmountGV"), TextBox)
                        Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtNetAmountGV"), TextBox)

                        Dim lblid1 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                        Dim lblid2 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtAccountNameGV"), TextBox)
                        Dim lblid3 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillAddress1GV"), TextBox)
                        Dim lblid4 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillBuildingGV"), TextBox)
                        Dim lblid5 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillStreetGV"), TextBox)
                        Dim lblid6 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillCountryGV"), TextBox)
                        Dim lblid7 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillPostalGV"), TextBox)
                        Dim lblid8 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtOurReferenceGV"), TextBox)
                        Dim lblid9 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtYourReferenceGV"), TextBox)
                        Dim lblid10 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtPoNoGV"), TextBox)
                        Dim lblid11 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtCreditTermsGV"), TextBox)
                        Dim lblid12 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtSalesmanGV"), TextBox)
                        Dim lblid13 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtToBillAmtGV"), TextBox)
                        Dim lblid14 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtRcnoServiceRecordGV"), TextBox)

                        Dim lblid16 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)
                        Dim lblid17 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim lblid18 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtRcnoServicebillingdetailGV"), TextBox)
                        Dim lblid19 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtContactTypeGV"), TextBox)
                        Dim lblid20 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)

                        Dim lblid21 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtDiscAmountGV"), TextBox)

                        'Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtToBillAmtGV"), TextBox)

                        Dim lblid15 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtServiceRecordNoGV"), TextBox)
                        'Dim lblid24 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtServiceRecordNoGV"), TextBox)
                        Dim lblid25 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtStatusGV"), TextBox)

                        Dim lblid22 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtContractNoGV"), TextBox)
                        'Dim lblid26 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtContractNoGV"), TextBox)

                        Dim lblid27 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtRetentionAmtGV"), TextBox)
                        'CType(xrow1.FindControl("txtRetentionAmtGV"), TextBox)

                        If txtMode.Text = "NEW" Then
                            txtAccountType.Text = lblid19.Text
                            txtCompanyGroup.Text = lblid16.Text
                            'txtInvoiceDate.Text = lblid20.Text
                            txtAccountIdBilling.Text = lblid1.Text
                            txtAccountName.Text = lblid2.Text
                            txtBillAddress.Text = lblid3.Text
                            txtBillBuilding.Text = lblid4.Text
                            txtBillStreet.Text = lblid5.Text
                            txtBillCountry.Text = lblid6.Text
                            txtBillPostal.Text = lblid7.Text
                            txtOurReference.Text = lblid8.Text
                            txtYourReference.Text = lblid9.Text
                            txtPONo.Text = lblid10.Text
                            txtContractNo.Text = lblid22.Text
                        End If

                        ''''''''''''''''''''''''''''''''''''''''''''''''

                        Dim conn As MySqlConnection = New MySqlConnection()

                        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        conn.Open()
                        Dim sql As String
                        sql = ""

                        Dim command21 As MySqlCommand = New MySqlCommand

                        sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

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


                        '''''''''''''''''''''''''''''''''''''''

                        sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='OTHER DEBTORS'"

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

                        '''''''''''''''''''''''''''''''''''
                        sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLType='GST OUTPUT'"

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

                        Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
                        Dim drCurrentRowLoc As DataRow = Nothing

                        'If String.IsNullOrEmpty(txtRcnoServiceRecord.Text) = True Or txtRcnoServiceRecord.Text = "0" Then


                        '    For j As Integer = 0 To grvBillingDetails.Rows.Count - 1
                        '        dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                        '        drCurrentRowLoc = dtScdrLoc.NewRow()
                        '        ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
                        '        grvBillingDetails.DataSource = dtScdrLoc
                        '        grvBillingDetails.DataBind()

                        '        SetPreviousDataBillingDetailsRecs()
                        '    Next j
                        '    FirstGridViewRowBillingDetailsRecs()
                        'End If


                        'Start: Populate the grid
                        'txtRcnoServiceRecord.Text = lblid14.Text
                        'txtRcnoServiceRecordDetail.Text = lblid15.Text
                        'txtContractNo.Text = lblid22.Text
                        'txtRcnoInvoice.Text = lblid17.Text
                        'txtRowSelected.Text = rowindex1.ToString


                        'txtRcnoservicebillingdetail.Text = lblid18.Text
                        'txtRcnotblServiceBillingDetail.Text = lblid18.Text


                        If String.IsNullOrEmpty(txtRcnoservicebillingdetail.Text) = True Then
                            txtRcnoservicebillingdetail.Text = "0"
                        End If


                        If String.IsNullOrEmpty(txtBatchNo.Text) = True Or txtBatchNo.Text = "0" Then
                            txtBatchNo.Text = txtRcnotblServiceBillingDetail.Text
                        End If
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

                        'Dim Query As String
                        'Dim Query1 As String

                        'Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                        'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                        'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

                        'Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtUOMGV"), DropDownList)
                        'Query1 = "Select * from tblunitms order by UnitMs"
                        'PopulateDropDownList(Query1, "UnitMs", "UnitMs", TextBoxUOM1)



                        'If Convert.ToInt64(txtRcnoServiceBillingDetail.Text) = 0 Then
                        'If txtMode.Text = "NEW" Then
                        If Convert.ToInt64(lblid18.Text) = 0 Then
                            Dim dt As New DataTable

                            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                            conn.Open()
                            'Dim command1 As MySqlCommand = New MySqlCommand
                            'command1.CommandType = CommandType.Text

                            'If lblid25.Text = "P" Then
                            '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode = 'IN-SRV'"
                            'ElseIf lblid25.Text = "O" Then
                            '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'and ProductCode = 'IN-DEF'"
                            'End If

                            'command1.Connection = conn

                            'Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                            'Dim dt1 As New DataTable
                            'dt1.Load(dr1)
                            'AddNewRowBillingDetailsRecs()

                            'Dim totrec As Integer
                            'totrec = 0

                            'If grvBillingDetails.Rows.Count = 1 Then
                            '    totrec = 0
                            'Else
                            '    totrec = grvBillingDetails.Rows.Count
                            '    totrec = totrec - 1
                            'End If
                            AddNewRowBillingDetailsRecs()
                            'totrec = grvBillingDetails.Rows.Count - 1
                            'AddNewRowBillingDetailsRecs()
                            'If (grvBillingDetails.Rows.Count > totrec + 1) Then


                            'AddNewRowWithDetailRecBillingDetailsRecs()
                            'AddNewRowBillingDetailsRecs()


                            Dim command1 As MySqlCommand = New MySqlCommand
                            command1.CommandType = CommandType.Text

                            'If lblid25.Text = "P" Then
                            '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode = 'IN-SRV'"
                            'ElseIf lblid25.Text = "O" Then
                            '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'and ProductCode = 'IN-DEF'"
                            'End If

                            If lblid25.Text = "P" Then
                                command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-SRV'"
                            ElseIf lblid25.Text = "O" Then
                                command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-DEF'"
                            End If


                            command1.Connection = conn
                            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                            Dim dt1 As New DataTable
                            dt1.Load(dr1)

                            Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                            TextBoxItemType.Text = "SERVICE"
                            TextBoxItemType.Enabled = False

                            Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                            TextBoxItemCode.Text = dt1.Rows(0)("ProductCode").ToString()
                            TextBoxItemCode.Enabled = False

                            Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
                            'TextBoxItemDescription.Text = ""
                            TextBoxItemDescription.Text = dt1.Rows(0)("Description").ToString()


                            Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtOtherCodeGV"), TextBox)
                            TextBoxOtherCode.Text = dt1.Rows(0)("COACode").ToString()


                            Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                            TextBoxGLDescription.Text = dt1.Rows(0)("COADescription").ToString()

                            Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                            TextBoxUOM.Text = "--SELECT--"

                            Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
                            TextBoxQty.Text = "1"
                            TextBoxQty.Enabled = False

                            'TextBoxPriceWithDisc = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                            'TextBoxPriceWithDisc.Text = lblid13.Text

                            Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
                            TextBoxPricePerUOM.Text = lblid13.Text

                            Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTPercGV"), TextBox)
                            TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N4")

                            'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                            TextBoxGSTAmt = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                            TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid13.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))

                            TextBoxTotalPrice = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
                            TextBoxTotalPrice.Text = lblid13.Text

                            TextBoxTotalPriceWithGST = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                            TextBoxTotalPriceWithGST.Text = Convert.ToString(Convert.ToDecimal(TextBoxPricePerUOM.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text))


                            Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                            TextBoxContractNo.Text = Convert.ToString(txtContractNo.Text)

                            Dim TextBoxServiceStatus As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceStatusGV"), TextBox)
                            TextBoxServiceStatus.Text = Convert.ToString(lblid25.Text)


                            'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtDiscPercGV"), TextBox)
                            'TextBoxDiscPerc.Text = Convert.ToString("0.00")

                            'TextBoxDiscAmt = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
                            'TextBoxDiscAmt.Text = Convert.ToString("0.00")

                            Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceRecordGV"), TextBox)
                            TextBoxServiceRecord.Text = Convert.ToString(lblid15.Text)


                            Dim TextBoxRetentionAmt10 As TextBox = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtRetentionAmtGV"), TextBox)
                            TextBoxRetentionAmt10.Text = Convert.ToString(Convert.ToDecimal(lblid27.Text).ToString("N2"))

                            TextBoxRetentionGSTAmt10 = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtRetentionGSTAmtGV"), TextBox)
                            TextBoxRetentionGSTAmt10.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid27.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))


                            TextBoxTotalPrice10 = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtTotalRetentionGV"), TextBox)
                            TextBoxTotalPrice10.Text = lblid27.Text


                            TextBoxTotalPriceWithGST10 = CType(grvBillingDetails.Rows(1).Cells(0).FindControl("txtRetentionWithGSTGV"), TextBox)
                            TextBoxTotalPriceWithGST10.Text = Convert.ToString(Convert.ToDecimal(TextBoxRetentionAmt10.Text) + Convert.ToDecimal(TextBoxRetentionGSTAmt10.Text))



                            'Dim TextBoxRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRetentionAmtGV"), TextBox)
                            'TextBoxRetentionAmt.Text = Convert.ToString(Convert.ToDecimal("0.00").ToString("N2"))

                            'Dim TextBoxRetentionGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRetentionGSTAmtGV"), TextBox)
                            'TextBoxRetentionGSTAmt.Text = Convert.ToString(Convert.ToDecimal("0.00").ToString("N2"))

                            'TextBoxTotalPriceWithGST10 = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRetentionWithGSTGV"), TextBox)
                            'TextBoxTotalPriceWithGST10.Text = Convert.ToString(Convert.ToDecimal("0.00"))

                            'TextBoxTotalPrice10 = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalRetentionGV"), TextBox)
                            'TextBoxTotalPrice10.Text = Convert.ToString(Convert.ToDecimal("0.00"))



                            'txtTotal.Text = Convert.ToDecimal(txtTotal.Text) + Convert.ToDecimal(TextBoxTotalPrice.Text)
                            'txtTotalWithGST.Text = Convert.ToDecimal(txtTotalWithGST.Text) + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)

                            'txtTotalDiscAmt.Text = (0.0).ToString("N2")
                            'txtTotalGSTAmt.Text = (Convert.ToDecimal(txtTotalGSTAmt.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)).ToString("N2")
                            'txtTotalWithDiscAmt.Text = Convert.ToDecimal(txtTotal.Text)

                            '''''''''''''''''''''
                            'Dim TextBoxItemCode2 As DropDownList = CType(grvBillingDetails.Rows(totrec).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                            'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                            'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode2)

                            'Dim TextBoxUOM2 As DropDownList = CType(grvBillingDetails.Rows(totrec).Cells(0).FindControl("txtUOMGV"), DropDownList)
                            'Query = "Select * from tblunitms order by UnitMs"
                            'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM2)


                            Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
                            TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
                            'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
                            'txtAmountWithDiscount.Text = Total - TotalDiscAmt
                            TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                            'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

                            rowIndex = rowIndex + 1
                        Else

                            'Start: From tblBillingDetailItem

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

                            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                            conn.Open()

                            Dim cmdServiceBillingDetailItem As MySqlCommand = New MySqlCommand
                            cmdServiceBillingDetailItem.CommandType = CommandType.Text
                            cmdServiceBillingDetailItem.CommandText = "SELECT * FROM tblservicebillingdetailitem where Rcnoservicebillingdetail=" & Convert.ToInt32(lblid18.Text)
                            cmdServiceBillingDetailItem.Connection = conn

                            Dim drcmdServiceBillingDetailItem As MySqlDataReader = cmdServiceBillingDetailItem.ExecuteReader()
                            Dim dtServiceBillingDetailItem As New DataTable
                            dtServiceBillingDetailItem.Load(drcmdServiceBillingDetailItem)

                            Dim TotDetailRecordsLoc = dtServiceBillingDetailItem.Rows.Count
                            If dtServiceBillingDetailItem.Rows.Count > 0 Then

                                Dim rowIndex1 = 0

                                For Each row As DataRow In dtServiceBillingDetailItem.Rows
                                    If (TotDetailRecordsLoc > (rowIndex + 1)) Then
                                        AddNewRowBillingDetailsRecs()
                                        'AddNewRow()
                                    End If

                                    If rowIndex = grvBillingDetails.Rows.Count Then
                                        AddNewRowBillingDetailsRecs()
                                    End If


                                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                                    TextBoxItemType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("ItemType"))

                                    Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                                    'TextBoxItemCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemCode"))

                                    TextBoxItemCode.Items.Clear()
                                    TextBoxItemCode.Items.Add(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("ItemCode")))

                                    Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
                                    TextBoxItemDescription.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("ItemDescription"))

                                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                                    TextBoxOtherCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("OtherCode"))

                                    Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtUOMGV"), DropDownList)
                                    'If String.IsNullOrEmpty(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("UOM"))) = True Then

                                    'Else
                                    '    TextBoxUOM.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("UOM"))
                                    'End If

                                    TextBoxUOM.Items.Clear()
                                    TextBoxUOM.Items.Add(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("UOM")))


                                    Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
                                    TextBoxQty.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("Qty"))
                                    'TextBoxQty.Enabled = False

                                    Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtPricePerUOMGV"), TextBox)
                                    TextBoxPricePerUOM.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("PricePerUOM"))


                                    'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscPercGV"), TextBox)
                                    'TextBoxDiscPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("DiscPerc"))

                                    'TextBoxDiscAmt = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
                                    'TextBoxDiscAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("DiscAmount"))


                                    'TextBoxPriceWithDisc = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtPriceWithDiscGV"), TextBox)
                                    'TextBoxPriceWithDisc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("PriceWithDisc"))


                                    Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtTaxTypeGV"), DropDownList)
                                    'TextBoxTaxType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TaxType"))

                                    TextBoxTaxType.Items.Clear()
                                    TextBoxTaxType.Items.Add(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TaxType")))


                                    Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtGSTPercGV"), TextBox)
                                    TextBoxGSTPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("GSTPerc"))

                                    TextBoxGSTAmt = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtGSTAmtGV"), TextBox)
                                    TextBoxGSTAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("GSTAmt"))

                                    TextBoxTotalPrice = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtTotalPriceGV"), TextBox)
                                    TextBoxTotalPrice.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TotalPrice"))

                                    TextBoxTotalPriceWithGST = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                                    TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TotalPriceWithGST"))


                                    Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtServiceRecordGV"), TextBox)
                                    TextBoxServiceRecord.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TotalPriceWithGST"))


                                    Dim TextBoxRetentionAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRetentionAmtGV"), TextBox)
                                    TextBoxRetentionAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("RetentionAmt"))

                                    Dim TextBoxRetentionGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRetentionGSTAmtGV"), TextBox)
                                    TextBoxRetentionGSTAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TotalPriceWithGST"))

                                    TextBoxTotalPriceWithGST10 = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRetentionWithGSTGV"), TextBox)
                                    TextBoxTotalPriceWithGST10.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TotalPriceWithGST"))

                                    TextBoxTotalPrice10 = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalRetentionGV"), TextBox)
                                    TextBoxTotalPrice10.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TotalPriceWithGST"))


                                    Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
                                    TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
                                    'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
                                    'txtAmountWithDiscount.Text = Total - TotalDiscAmt
                                    TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                                    'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

                                    'Dim Query As String

                                    'Dim TextBoxItemCode2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                                    'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                                    'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode2)

                                    'Dim TextBoxUOM2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                                    'Query = "Select * from tblunitms order by UnitMs"
                                    'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM2)


                                    Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                                    Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

                                    If TextBoxItemType1.Text = "SERVICE" Then
                                        TextBoxQty1.Enabled = False
                                        TextBoxQty1.Text = 1
                                        TextBoxItemType1.Enabled = False
                                    End If

                                    rowIndex = rowIndex + 1
                                    rowIndex1 = rowIndex1 + 1


                                Next row

                                'txtTotal.Text = Total.ToString("N2")
                                'txtTotalWithGST.Text = TotalWithGST.ToString("N2")
                                'txtTotalDiscAmt.Text = TotalDiscAmt.ToString("N2")
                                'txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")

                                'txtTotalDiscAmt.Text = 0.0
                                txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
                                AddNewRowBillingDetailsRecs()
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
                        End If

                        If txtMode.Text <> "NEW" Then
                            AddNewRowBillingDetailsRecs()
                        End If

                        grvBillingDetails.Enabled = True

                        btnSave.Enabled = False
                        If txtPostStatus.Text <> "P" Then
                            'btnSave.Enabled = True
                            btnSaveInvoice.Enabled = True
                        End If

                        updpnlServiceRecs.Update()
                        updpnlBillingDetails.Update()
                        'End: Billing Details
                        updPanelSave.Update()
                        'End: Populate the grid
                        updPnlBillingRecs.Update()


                        '''''''''''''''''''''''''''''''''''''''''''''''''



                        'If String.IsNullOrEmpty(TextBoxInvoiceAmount.Text) = True Then
                        '    TextBoxInvoiceAmount.Text = "0.00"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxDiscountAmount.Text) = True Then
                        '    TextBoxDiscountAmount.Text = "0.00"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxGSTAmount.Text) = True Then
                        '    TextBoxGSTAmount.Text = "0.00"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxNetAmount.Text) = True Then
                        '    TextBoxNetAmount.Text = "0.00"
                        'End If

                        'TotalInvoiceAmount = TotalInvoiceAmount + Convert.ToDecimal(TextBoxInvoiceAmount.Text)
                        'TotalDiscountAmount = TotalDiscountAmount + Convert.ToDecimal(TextBoxDiscountAmount.Text)

                        'TotalGSTAmount = TotalGSTAmount + Convert.ToDecimal(TextBoxGSTAmount.Text)
                        'TotalNetAmount = TotalNetAmount + Convert.ToDecimal(TextBoxNetAmount.Text)


                        'TextBoxGSTAmt.Text = "0.00"
                        'TextBoxTotalPrice.Text = "0.00"
                        'TextBoxTotalPriceWithGST.Text = "0.00"
                        'TextBoxDiscAmt.Text = "0.00"
                        'TextBoxPriceWithDisc.Text = "0.00"

                        'Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
                        'TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
                        'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
                        ''txtAmountWithDiscount.Text =  Total - TotalDiscAmt
                        'TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                        'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

                        'rowIndex = rowIndex + 1
                    End If
                Next i

                txtTotal.Text = Total.ToString("N2")
                'txtTotalDiscAmt.Text = TotalDiscAmt.ToString("N2")
                'txtTotalWithDiscAmt.Text = (Total - TotalDiscAmt).ToString("N2")
                txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
                txtTotalWithGST.Text = TotalWithGST.ToString("N2")

                grvBillingDetails.Enabled = False
            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
        'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "'  or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"

        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
            SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O'  order by name"
        ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
            SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O'  order by name"
        End If
        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
        txtIsPopup.Text = "Client"
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter client name", "str")
        Else
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            'SqlDSClient.SelectCommand = "SELECT ContType, AccountID, ContID, ContName, ContPerson, ContAddress1, ContHP, ContEmail,  ContLocationGroup, ContGroup, ContAddBlock, ContAddNos, ContAddFloor, ContAddUnit, ContAddStreet, ContAddBuilding, ContAddCity, ContAddState, ContAddCountry, ContAddPostal, ContFax, Mobile, ContTel, ContSales From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"


            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '%" + txtPopUpClient.Text + "%' or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '%" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            txtIsPopup.Text = "Client"
        End If

        'txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
    End Sub

    Protected Sub GridView1_Sorted(sender As Object, e As EventArgs) Handles GridView1.Sorted

    End Sub

    Protected Sub BtnLocation_Click(sender As Object, e As ImageClickEventArgs) Handles BtnLocation.Click
        mdlPopupLocation.Show()
    End Sub

    Protected Sub txtInvoiceAmount_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceAmount.TextChanged
        txtGSTAmount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")
        txtNetAmount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtGSTAmount.Text)).ToString("N2")
        txtTotalClaim.Text = (Convert.ToDecimal(txtPreviousClaim.Text) + Convert.ToDecimal(txtInvoiceAmount.Text)).ToString("N2")
    End Sub

    Protected Sub txtRetentionAmount_TextChanged(sender As Object, e As EventArgs) Handles txtRetentionAmount.TextChanged
        txtTotalRetentionAmount.Text = (Convert.ToDecimal(txtPreviousRetentionAmount.Text) + Convert.ToDecimal(txtRetentionAmount.Text)).ToString("N2")
    End Sub
End Class
