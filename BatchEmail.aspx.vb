
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports EASendMail
Imports System.Globalization
''Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports CrystalDecisions.Web
Imports System.Data.Odbc
Imports CrystalDecisions.Shared


Partial Class BatchEmail
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        '    Response.Redirect("Home.aspx")
        'End If
        'btnSendEmail.Enabled = False
        If Not IsPostBack Then
            LocationAccess()

        End If
     
        AccessControl()
    End Sub

    Private Sub LocationAccess()
        Dim conn As MySqlConnection = New MySqlConnection()
      
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Find if Branch/Location Enabled
        Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
        commandServiceRecordMasterSetup.CommandType = CommandType.Text
        commandServiceRecordMasterSetup.CommandText = "SELECT DisplayRecordsLocationWise FROM tblservicerecordmastersetup"
        commandServiceRecordMasterSetup.Connection = conn

        Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
        Dim dtServiceRecordMasterSetup As New DataTable
        dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

        txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
        Session.Add("LocationEnabled", txtDisplayRecordsLocationwise.Text)

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            '  GridView1.Rows.Item(4).Visible = True

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            command.CommandText = "select locationID from tblgroupaccesslocation where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.Connection = conn

            Dim dr1 As MySqlDataReader = command.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)
            Dim YrStrList As List(Of [String]) = New List(Of String)()

            If dt1.Rows.Count > 0 Then
                For i As Int16 = 0 To dt1.Rows.Count - 1
                    '   YrStrList.Add(dt1.Rows(i)("LocationID"))
                    '  chkBranch.Items.Add(dt1.Rows(i)("LocationID"))
                    YrStrList.Add("""" + dt1.Rows(i)("LocationID") + """")
                Next
                Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                Session.Add("Branch", YrStr)
            End If


            chkBranch.DataSource = command.ExecuteReader()
            chkBranch.DataTextField = "LOCATIONID"
            chkBranch.DataValueField = "LOCATIONID"
            chkBranch.DataBind()

            For Each item As ListItem In chkBranch.Items
                item.Selected = True
            Next

            command.Dispose()
            dt1.Dispose()
            dr1.Close()
        Else
            Label1.Visible = False
            chkBranch.Visible = False
            '    GridView1.Rows.Item(4).Visible = False

        End If

        commandServiceRecordMasterSetup.Dispose()
        dtServiceRecordMasterSetup.Dispose()
    
        conn.Close()
        conn.Dispose()

    End Sub

    Protected Sub chkBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkBranch.SelectedIndexChanged
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        Dim count As Int16 = 0

        For Each item As ListItem In chkBranch.Items
            If item.Selected = True Then
                YrStrList.Add("""" + item.Value + """")
                count = count + 1
            End If
        Next
        If count = 0 Then
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                lblAlert.Text = "SELECT BRANCH/LOCATION"
            End If


        End If
        Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
        Session.Add("Branch", YrStr)
    End Sub

    Private Sub AccessControl()
        '''''''''''''''''''Access Control 
        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT x0174, x0174_SendEmail FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If String.IsNullOrEmpty(dt.Rows(0)("x0174")) = False Then
                    If dt.Rows(0)("x0174").ToString() = False Then
                        Response.Redirect("Home.aspx")
                    End If
                End If


                If String.IsNullOrEmpty(dt.Rows(0)("x0174_SendEmail")) = False Then
                    btnSendEmail.Enabled = dt.Rows(0)("x0174_SendEmail").ToString()
                End If


                If btnSendEmail.Enabled = True Then
                    btnSendEmail.ForeColor = System.Drawing.Color.Black
                Else
                    btnSendEmail.ForeColor = System.Drawing.Color.Gray
                End If


            End If
            conn.Close()
            conn.Dispose()
        End If

        '''''''''''''''''''Access Control 
    End Sub


    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        ddlIncharge.SelectedIndex = 0
        SQLDSService.SelectCommand = ""
        GridView1.DataSourceID = "SQLDSService"
        GridView1.DataBind()
        btnSendEmail.Enabled = False

        lblAlert.Text = ""
        lblMessage.Text = ""
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
       
    End Sub

    Public Function GenerateQuery() As String
        Dim ServiceBy As String = ""
        Dim SvcFromDate As String = ""
        Dim SvcToDate As String = ""
        Dim sqlSelect As String = ""

       
        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                sqlSelect = "INVALID SERVICE DATE FROM"
                Return sqlSelect
            End If
            SvcFromDate = d.ToString("yyyy-MM-dd")
        Else
            sqlSelect = "PLEASE ENTER SERVICE DATE FROM"
            Return sqlSelect
        End If

        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d1 As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d1) Then

            Else
                sqlSelect = "INVALID SERVICE DATE TO"
                Return sqlSelect
            End If
            SvcToDate = d1.ToString("yyyy-MM-dd")
        Else
            sqlSelect = "PLEASE ENTER SERVICE DATE TO"
            Return sqlSelect
        End If

        If ddlIncharge.Text = "-1" Then
        Else
            ServiceBy = ddlIncharge.SelectedItem.Text
        End If

      



        'If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
        '    Dim d As DateTime
        '    If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

        '    Else
        '        '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
        '        lblAlert.Text = "INVALID SERVICE DATE TO"
        '        Return
        '    End If
        '    SvcToDate = d.ToString("yyyy-MM-dd")
        'End If

        sqlSelect = "Select rcno, Status, location,EmailSent, ContractNo, AccountId, LocationId, ServiceDate, OurRef, InchargeId, CustName, ServiceBy, RecordNo, "
        sqlSelect += "CustomerSign, Email, Contact2Email, OtherEmail, Address1, AddUnit, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal,SignatureYN "
        sqlSelect += " from tblServiceRecord where status='P' and (EmailSent=0 or EmailSent is null) and ((Email <> '' or Email <> null) or (Contact2Email<>'' or Contact2Email <> null) or (OtherEmail<>'' or OtherEmail <> null))"

        If (SvcFromDate <> "") Then
            sqlSelect += " and ServiceDate>='" + SvcFromDate + "'"
        End If

        If (SvcToDate <> "") Then
            sqlSelect += " and ServiceDate<='" + SvcToDate + "'"
        Else
            sqlSelect += " and ServiceDate<='" + SvcFromDate + "'"
        End If

        If (ServiceBy <> "") Then
            sqlSelect += " and ServiceBy='" + ServiceBy + "'"
        End If
        If chkSign.Checked = True Then
            '  sqlSelect += " and SignatureYN = 'N'"
        Else
            sqlSelect += " and SignatureYN = 'Y' and CustomerSign is not null and CustomerSignDate is not null "
        End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            sqlSelect += " and Location in (" + Convert.ToString(Session("Branch")) + ")"

           
        End If

        If String.IsNullOrEmpty(txtSearch1ClientName.Text) = False Then
            sqlSelect = sqlSelect + " and custname like '%" + txtSearch1ClientName.Text + "%'"
        End If
    
        If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            sqlSelect = sqlSelect + " and Contractno = '" + txtContractNo.Text + "'"
        End If

        If String.IsNullOrEmpty(txtServiceLocationID.Text) = False Then
            sqlSelect = sqlSelect + " and LocationID = '" + txtServiceLocationID.Text + "'"
        End If

        sqlSelect = sqlSelect + " and recordno in (select recordno from tblservicerecorddet where trim(action) <> '' or action <> null)"

        'If (SvcFromDate <> "" And SvcToDate <> "") Then
        '    sqlSelect += " and ServiceDate >='" + SvcFromDate + "'"
        '    sqlSelect += " and ServiceDate <='" + SvcToDate + "'"
        'ElseIf (SvcFromDate <> "" And SvcToDate = "") Then
        '    sqlSelect += " and ServiceDate >='" + SvcFromDate + "'"
        'End If

        Return sqlSelect
    End Function

    Dim SuccessCount As Int16 = 0
    Dim FailureCount As Int16 = 0

    Protected Sub btnSendEmail_Click(sender As Object, e As EventArgs) Handles btnSendEmail.Click
        Try


            Dim rec As String = ""
            Dim YrStrList As List(Of [String]) = New List(Of String)()
            If GridView1.Rows.Count > 0 Then
                For Each row As GridViewRow In GridView1.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkSelectMultiPrintGV"), CheckBox)
                        If chkRow.Checked Then
                            YrStrList.Add(row.Cells(5).Text)
                            SendEmail(row.Cells(5).Text)
                            System.Threading.Thread.Sleep(1500)
                        End If
                    End If
                Next
                'Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                'If String.IsNullOrEmpty(YrStr) = False Then
                '    rec = YrStr
                'End If
                ''  Session("InvoiceNumber") = rec
                GridView1.DataSourceID = "SQLDSService"
                GridView1.DataBind()

                lblQuery.Text = lblQuery.Text + "<br><br><br> Success : " + (SuccessCount - FailureCount).ToString + " Failure : " + FailureCount.ToString
                mdlPopupMsg.Show()

            End If
            ' lblAlert.Text = rec
        Catch ex As Exception
            ' FailureCount = FailureCount + 1
            InsertIntoTblWebEventLog("SendEmail", ex.Message.ToString, lblRecordNo.Text)
        End Try
        Exit Sub

        Dim RecordNo As String = ""
        Try
            btnSendEmail.Enabled = False
            Dim ToEmailId As String = ""
            Dim subject As String = ""
            Dim body As String = ""
            Dim RemarkOffice As String = ""
            Dim Notes As String = ""
            Dim OfficeEmail As String = ""
            Dim isRemarkOffice As [Boolean] = False

            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                    If chkRow.Checked Then
                        Dim ContractNo As String = row.Cells(4).Text
                        RecordNo = row.Cells(5).Text
                        Dim ServiceDate As String = row.Cells(6).Text
                        Dim CustName As String = row.Cells(7).Text
                        Dim LocationId As String = row.Cells(8).Text
                        Dim ServiceBy As String = row.Cells(9).Text
                        Dim AccountId As String = row.Cells(10).Text
                        Dim InchargeId As String = row.Cells(11).Text
                        Dim OurRef As String = row.Cells(12).Text
                        Dim Address1 As String = row.Cells(13).Text
                        Dim AddUnit As String = row.Cells(14).Text
                        Dim AddBuilding As String = row.Cells(15).Text
                        Dim AddStreet As String = row.Cells(16).Text
                        Dim AddCity As String = row.Cells(17).Text
                        Dim AddState As String = row.Cells(18).Text
                        Dim AddCountry As String = row.Cells(19).Text
                        Dim AddPostal As String = row.Cells(20).Text
                        Dim Email As String = row.Cells(21).Text
                        Dim Contact2Email As String = row.Cells(22).Text
                        Dim OtherEmail As String = row.Cells(23).Text

                        Dim isCustSign As Boolean = False

                        If (IsDBNull(row.Cells(24)) = True) Then
                            isCustSign = False
                        Else
                            isCustSign = True
                        End If

                        If (Email <> "&nbsp;") Then
                            ToEmailId += Email + ";"
                        End If

                        If (Contact2Email <> "&nbsp;") Then
                            ToEmailId += Contact2Email + ";"
                        End If

                        If (OtherEmail <> "&nbsp;") Then
                            ToEmailId += OtherEmail + ";"
                        End If

                        '   ToEmailId = ToEmailId.TrimEnd(";") ''original

                        Dim Address As String = ""

                        If (Address1 <> "&nbsp;") Then
                            Address += Address1 + ", "
                        End If

                        If (AddUnit <> "&nbsp;") Then
                            Address += AddUnit + ", "
                        End If

                        If (AddBuilding <> "&nbsp;") Then
                            Address += AddBuilding + ", "
                        End If

                        If (AddStreet <> "&nbsp;") Then
                            Address += AddStreet + ", "
                        End If

                        If (AddCity <> "&nbsp;") Then
                            Address += AddCity + ", "
                        End If

                        If (AddCountry <> "&nbsp;") Then
                            Address += AddCountry + ", "
                        End If

                        If (AddPostal <> "&nbsp;") Then
                            Address += AddPostal + ", "
                        End If

                        Dim CustAddress As String = Address.Trim()
                        CustAddress = CustAddress.TrimEnd(",")


                        'ToEmailId = "alams@cwbinfotech.com;alamsvenkat2000@gmail.com" ''testing
                        'OfficeEmail = "alams@cwbinfotech.com;alamsvenkat2000@gmail.com" ''testing


                        If isCustSign = True Then
                            If ToEmailId <> "" Then
                                GenerateServiceReport(RecordNo, ContractNo, ServiceDate, ServiceBy, CustName, CustAddress, AccountId, LocationId, InchargeId, OurRef, ToEmailId, "Client", "", "")
                            End If
                        End If

                        Dim dsInfo As DataSet = GetServiceRecordInfo(RecordNo)

                        If (dsInfo IsNot Nothing) Then
                            If (dsInfo.Tables(1).Rows.Count > 0) Then
                                '          OfficeEmail = dsInfo.Tables(0).Rows(0)("OfficeEmail").ToString()   ''original
                            End If

                            subject = "FOR OFFICE ACTION :  " + RecordNo + " SERVICE ON " + ServiceDate + " AT " + CustName + " - " + CustAddress
                            body = "<b>Service By : </b>" + ServiceBy + " <br/>"
                            body += "<b>Service Record No. : </b>" + RecordNo + " <br/>"
                            body += "<b>Contract No. :  </b>" + ContractNo + " <br/>"
                            body += "<b>Service Date : </b>" + ServiceDate + " <br/>"
                            body += "<b>Account ID : </b>" + AccountId + " <br/>"
                            body += "<b>Customer Name : </b>" + CustName + " <br/>"
                            body += "<b>Service Location ID : </b>" + LocationId + " <br/>"
                            body += "<b>Service Address : </b>" + CustAddress + " <br/><br/>"

                            For i As Integer = 0 To dsInfo.Tables(1).Rows.Count - 1
                                RemarkOffice = dsInfo.Tables(1).Rows(i)("RemarkOffice").ToString()
                                Notes = dsInfo.Tables(1).Rows(i)("TargetDescription").ToString()

                                If RemarkOffice <> "" Then
                                    isRemarkOffice = True
                                    body += "<b>Service Description : </b>" + Notes + " <br/>"
                                    body += "<b>Remarks to Office : </b>" + RemarkOffice + " <br/><br/>"
                                End If
                            Next

                            body += "Please do not reply to this email"

                            If isRemarkOffice = True Then
                                If OfficeEmail <> "" Then
                                    GenerateServiceReport(RecordNo, ContractNo, ServiceDate, ServiceBy, CustName, CustAddress, AccountId, LocationId, InchargeId, OurRef, OfficeEmail, "Office", subject, body)
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            lblAlert.Text = "System Error Captured for " & RecordNo & "-" & ex.Message.ToString()
        End Try
    End Sub

    Public Sub GenerateServiceReport(RecordNo As String, ContractNo As String, ServiceDate As String, ServiceBy As String, CustName As String,
                                      CustAddress As String, AccountId As String, LocationId As String, InchargeId As String, OurRef As String,
                                                        RecipientEmail As String, Recipient As String, emSubject As String, emBody As String)
        Dim MainRptFileName As String = ""
        Dim SuppRptFileName As String = ""
        Dim arrMain As [Byte]() = Nothing
        Dim arrSupp As [Byte]() = Nothing
        Dim Notes As String = ""
        Dim RemarkOffice As String = ""
        Dim stMain As Stream, stSupp As Stream

        GenerateWarranty(RecordNo)

        Using crMain As New ReportDocument()
            crMain.Load(Server.MapPath("~/Reports/ServiceRecordReports/ServiceRptBatchEmail.rpt"))

            '' crMain.Load(Server.MapPath("~/ServiceReportBatchEmail.rpt"))
            crMain.RecordSelectionFormula = "{tblservicerecord1.Recordno} = '" + RecordNo + "'"
            MainRptFileName = RecordNo + "_Service" + ".pdf"
            stMain = crMain.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
            arrMain = ReadFully(stMain)
            crMain.Close()
            crMain.Dispose()
        End Using

        Dim ds1 As DataSet = New DataSet()
        Dim dsSupp As dsSupplement = New dsSupplement()

        ds1 = GetSvcSuppReport(RecordNo)

        If (ds1 IsNot Nothing) Then
            If (ds1.Tables(0).Rows.Count > 0) Then

                Dim fCol As New List(Of Byte())()
                Dim SCol As New List(Of Byte())()
                Dim i As Integer = 0
                For Each ro As DataRow In ds1.Tables(0).Rows
                    i += 1
                    If i Mod 2 <> 0 Then
                        fCol.Add(DirectCast(ro("Photo"), Byte()))
                    Else
                        SCol.Add(DirectCast(ro("Photo"), Byte()))
                    End If
                Next

                If fCol.Count > 0 Then
                    For j As Integer = 0 To fCol.Count - 1
                        Dim dr As DataRow = dsSupp.Tables("dtSupp").NewRow()
                        dr("RecordNo") = RecordNo
                        dr("ServiceDate") = ServiceDate
                        dr("OurRef") = OurRef
                        dr("InchargeId") = InchargeId
                        dr("FColPhoto") = DirectCast(fCol(j), Byte())

                        If SCol.Count > 0 Then
                            If j <> SCol.Count Then
                                dr("SColPhoto") = DirectCast(SCol(j), Byte())
                            End If
                        End If
                        dsSupp.Tables("dtSupp").Rows.Add(dr)
                    Next
                End If

                Using crSupp As New ReportDocument()
                    crSupp.Load(Server.MapPath("~/ServiceSupplementRptBatchEmail.rpt"))
                    crSupp.SetDataSource(dsSupp.Tables("dtSupp"))
                    SuppRptFileName = RecordNo + "_SvcSupplement" + ".pdf"
                    stSupp = crSupp.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)
                    arrSupp = ReadFully(stSupp)
                    crSupp.Close()
                    crSupp.Dispose()
                End Using
            End If
        End If

        If (arrMain IsNot Nothing) Then
            Dim supp As [String] = ""
            Dim mainSvc As String = ""
            mainSvc = Convert.ToBase64String(arrMain)

            If (arrSupp IsNot Nothing) Then
                supp = Convert.ToBase64String(arrSupp)
            End If
        End If

        Dim dsEmail As New DataSet()
        dsEmail = GetEmailSetUp()

        Dim subject As String = ""
        Dim body As String = ""
        Dim EmailSetUpCC As String = ""

        If (Recipient = "Client") Then
            If (dsEmail.Tables(0).Rows.Count > 0) Then
                '    EmailSetUpCC = dsEmail.Tables(0).Rows(0)("ReceiverCC").ToString()  ''original

                subject = dsEmail.Tables(0).Rows(0)("Subject").ToString()
                body = dsEmail.Tables(0).Rows(0)("Contents").ToString()
            End If

            body = System.Text.RegularExpressions.Regex.Replace(body, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            body = body.Replace("CLIENT NAME", CustName)
            body = body.Replace("CUSTOMER ADDRESS", CustAddress)
            body = body.Replace("SERVICE DATE", ServiceDate)
            body = body.Replace("SERVICE BY", ServiceBy)
            body = body.Replace("COMPANY NAME", "ANTICIMEX PEST MANAGEMENT PTE. LTD.")
            body = body.Replace("SERVICE RECORD NUMBER", RecordNo)

            subject = System.Text.RegularExpressions.Regex.Replace(subject, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            subject = subject.Replace("CLIENT NAME", CustName)
            subject = subject.Replace("SERVICE DATE", ServiceDate)
            subject = subject.Replace("SERVICE RECORD NUMBER", RecordNo)
            subject = subject.Replace("CUSTOMER ADDRESS", CustAddress)

            SendServiceReport(arrMain, arrSupp, body, subject, MainRptFileName, SuppRptFileName, RecipientEmail, EmailSetUpCC, Recipient)

            '' Update_tblServiceRecord_EmailSent(RecordNo)  ''original

            lblMessage.Text = "Email To the Client is Success"
        End If

        If (Recipient = "Office") Then
            body = emBody
            subject = emSubject
            SendServiceReport(arrMain, arrSupp, body, subject, MainRptFileName, SuppRptFileName, RecipientEmail, EmailSetUpCC, Recipient)
        End If
    End Sub

    Public Sub SendServiceReport(arrMain As Byte(), arrSupp As Byte(), body As String, subject As String, MainRptFileName As String,
                                 SuppRptFileName As String, ToEmailId As String, EmailSetUpCC As String, Recipient As String)
        'Dim Mail As New MailMessage
        'Dim SMTP As New SmtpClient("smtp.gmail.com")

        'Mail.Subject = "Service Report"
        'Mail.IsBodyHtml = True

        'Mail.From = New MailAddress("sita.test.web@gmail.com")
        'SMTP.Credentials = New System.Net.NetworkCredential("sita.test.web@gmail.com", "sita1234") '<-- Password Here

        'Mail.To.Add("alams@cwbinfotech.com")

        'Mail.Body = body

        'SMTP.EnableSsl = True
        'SMTP.Port = "587"

        'If (arrMain IsNot Nothing) Then
        '    Mail.Attachments.Add(New Attachment(New MemoryStream(arrMain), MainRptFileName))
        'End If

        'If (arrSupp IsNot Nothing) Then
        '    Mail.Attachments.Add(New Attachment(New MemoryStream(arrSupp), SuppRptFileName))
        'End If

        'System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s As Object, certificate As System.Security.Cryptography.X509Certificates.X509Certificate, chain As System.Security.Cryptography.X509Certificates.X509Chain, sslPolicyErrors As System.Net.Security.SslPolicyErrors) True
        'SMTP.Send(Mail)

        Dim oServer As SmtpServer = ConfigurationManager.AppSettings("EmailSMTP").ToString()
        oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
        oServer.ConnectType = SmtpConnectType.ConnectDirectSSL
        oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
        oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

        Dim oMail As SmtpMail = New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
        Dim oSmtp As SmtpClient = New SmtpClient()
        oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()
        oMail.Subject = subject
        oMail.HtmlBody = body
        oMail.To.Clear()
        oMail.Cc.Clear()



        'ToEmailId = "alams@cwbinfotech.com;alamsvenkat2000@gmail.com" ''testing
        'OfficeEmail = "alams@cwbinfotech.com;alamsvenkat2000@gmail.com" ''testing



        If (Recipient = "Office") Then
            Dim OfficeAddress As String() = ToEmailId.Split(";")
            If (OfficeAddress.Count() > 0) Then
                For j As Integer = 0 To OfficeAddress.Count - 1
                    oMail.To.Add(New MailAddress(OfficeAddress(j).ToString()))
                Next
            End If
        End If

        If (Recipient = "Client") Then
            Dim ToAddress As String() = ToEmailId.Split(";")
            If (ToAddress.Count() > 0) Then
                For j As Integer = 0 To ToAddress.Count - 1
                    oMail.To.Add(New MailAddress(ToAddress(j).ToString()))
                Next
            End If

            If (EmailSetUpCC <> "") Then
                Dim cA As String() = EmailSetUpCC.Split(";")
                If (cA.Count > 0) Then
                    For j As Integer = 0 To cA.Count - 1
                        oMail.Cc.Add(New MailAddress(cA(j).ToString()))
                    Next
                End If
            End If
        End If

        If (arrMain IsNot Nothing) Then
            oMail.AddAttachment(MainRptFileName, arrMain)
        End If

        If (arrSupp IsNot Nothing) Then
            oMail.AddAttachment(SuppRptFileName, arrSupp)
        End If

        oSmtp.SendMail(oServer, oMail)
        '' oMail.ClearAttachments()
    End Sub

    Protected Sub GenerateWarranty(RecordNo As String)
        Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString)
            conn.Open()
            Dim command1 As New MySqlCommand()

            command1.CommandType = CommandType.Text
            command1.CommandText = (Convert.ToString("SELECT * FROM tblservicerecorddet where recordno='") & RecordNo) + "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable()
            dt.Load(dr)

            Dim warranty As String = ""
            Dim targetid As String = ""
            Dim targetdesc As String = ""

            If dt.Rows.Count > 0 Then
                For i As Int16 = 0 To dt.Rows.Count - 1
                    warranty = ""
                    If Not String.IsNullOrEmpty(dt.Rows(i)("Frequency").ToString()) Then
                        warranty = dt.Rows(i)("Frequency").ToString()
                    End If

                    targetid = dt.Rows(i)("TargetID").ToString()
                    If String.IsNullOrEmpty(targetid) = False Then
                        Dim stringList As List(Of String) = targetid.Split(","c).ToList()
                        Dim YrStrList As List(Of [String]) = New List(Of String)()
                        targetdesc = ""


                        For Each str As String In stringList
                            Dim command2 As New MySqlCommand()

                            command2.CommandType = CommandType.Text

                            command2.CommandText = "SELECT descrip1 FROM tbltarget where targetid='" + str.Trim() + "'"
                            command2.Connection = conn

                            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                            Dim dt2 As New DataTable()
                            dt2.Load(dr2)

                            If dt2.Rows.Count > 0 Then
                                If String.IsNullOrEmpty(targetdesc) Then
                                    targetdesc = dt2.Rows(0)("descrip1").ToString()
                                Else
                                    targetdesc = (targetdesc & Convert.ToString(",")) + dt2.Rows(0)("descrip1").ToString()

                                End If
                            End If

                            command2.Dispose()
                            dt2.Clear()
                            dt2.Dispose()

                            dr2.Close()
                        Next
                        warranty = Convert.ToString(warranty & Convert.ToString(" - ")) & targetdesc
                    End If

                    Dim command As New MySqlCommand()
                    command.CommandType = CommandType.Text
                    command.CommandText = "update tblservicerecorddet set TargetDescription=@desc where rcno=" & dt.Rows(i)("RcNo")
                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@desc", warranty)
                    command.Connection = conn
                    command.ExecuteNonQuery()
                    command.Dispose()
                Next
            End If
            command1.Dispose()
            dt.Clear()
            dt.Dispose()
            dr.Close()

            conn.Close()
            conn.Dispose()
        End Using
    End Sub

    Public Sub Update_tblServiceRecord_EmailSent(RecordNo As String)
        Try
            Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString)
                Dim updCmd As New MySqlCommand()
                updCmd.CommandType = CommandType.Text

                Dim updQry As String = "UPDATE tblservicerecord SET "
                updQry += "EmailSent = @EmailSent, "
                updQry += "EmailSentDate = @EmailSentDate "
                updQry += "WHERE RecordNo = @RecordNo"

                updCmd.CommandText = updQry
                updCmd.Parameters.Clear()
                updCmd.Parameters.AddWithValue("@EmailSent", 1)
                updCmd.Parameters.AddWithValue("@EmailSentDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New CultureInfo("en-GB")))
                updCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
                conn.Open()
                updCmd.Connection = conn
                updCmd.ExecuteNonQuery()
                conn.Close()
                conn.Dispose()
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Function GetEmailSetUp() As DataSet
        Try
            Dim ds As New DataSet()
            Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString)
                Dim selCmd As New MySqlCommand()
                selCmd.CommandType = CommandType.Text
                Dim sqlSelect As String = "Select * from tblEmailSetUp where SetUpID='SERV-REC-1'"
                selCmd.CommandText = sqlSelect
                conn.Open()
                selCmd.Connection = conn
                Dim da As New MySqlDataAdapter()
                da.SelectCommand = selCmd
                da.Fill(ds)
                conn.Close()
                conn.Dispose()
            End Using
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub PopulateGrid(sqlSelect As String)
        Dim ds As New DataSet()
        Dim dt As New DataTable()
        ds.Tables.Add(dt)

        Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString)
            Try
                Dim selCmd As New MySqlCommand()
                selCmd.CommandType = CommandType.Text
                selCmd.CommandText = sqlSelect
                conn.Open()
                selCmd.Connection = conn
                Dim da As New MySqlDataAdapter()
                da.SelectCommand = selCmd
                da.Fill(ds.Tables(0))
                conn.Close()
            Catch ex As Exception

            End Try
        End Using

        If (ds.Tables(0).Rows.Count > 0) Then
            SQLDSService.SelectCommand = sqlSelect
            GridView1.DataSourceID = "SQLDSService"
            GridView1.DataBind()
            lblMessage.Text = "NUMBER OF RECORDS FOUND : " & ds.Tables(0).Rows.Count
            btnSendEmail.Enabled = True
            AccessControl()
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                GridView1.Columns(4).Visible = True


            Else
                GridView1.Columns(4).Visible = False

            End If


        Else
            lblMessage.Text = "NUMBER OF RECORDS FOUND : 0"
            btnSendEmail.Enabled = False
            GridView1.DataSource = Nothing
            GridView1.DataBind()
        End If

    End Sub

    Public Function GetServiceRecordInfo(RecordNo As String) As DataSet
        Try
            Dim ds1 As New DataSet()
            Dim OfficeEmail As String = ""
            Dim Scheduler As String = ""
            Dim sqlSelect As String = ""

            Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString)
                Dim selCmd As New MySqlCommand()
                selCmd.CommandType = CommandType.Text
                sqlSelect = "Select b.Scheduler, b.Salesman from tblServiceRecord a, tblContract b "
                sqlSelect += " where a.ContractNo = b.ContractNo "
                sqlSelect += (Convert.ToString(" and a.RecordNo = '") & RecordNo) + "'"
                selCmd.CommandText = sqlSelect
                conn.Open()
                selCmd.Connection = conn
                Dim dr As MySqlDataReader = selCmd.ExecuteReader()
                If dr.HasRows Then
                    While dr.Read()
                        If Not String.IsNullOrEmpty(dr("Scheduler").ToString()) Then
                            Scheduler = dr("Scheduler").ToString()
                        End If
                    End While
                End If
                conn.Close()
                conn.Dispose()

                sqlSelect = ""

                'if (Scheduler != "" && Salesman != "")
                '    sqlSelect = "Select EmailPerson, EmailCompany, HomeEmail from tblStaff where staffId in ('" + Scheduler + "','" + Salesman + "')";

                If Scheduler <> "" Then
                    sqlSelect = (Convert.ToString("Select EmailPerson, EmailCompany, HomeEmail from tblStaff where staffId= '") & Scheduler) + "'"
                End If

                'if (Scheduler == "" && Salesman != "")
                '    sqlSelect = "Select EmailPerson, EmailCompany, HomeEmail from tblStaff where staffId= '" + Salesman + "'";

                If sqlSelect <> "" Then
                    Dim ds As New DataSet()
                    selCmd = New MySqlCommand()
                    selCmd.CommandText = sqlSelect
                    conn.Open()
                    selCmd.Connection = conn
                    Dim da As New MySqlDataAdapter()
                    da.SelectCommand = selCmd
                    da.Fill(ds)
                    conn.Close()
                    conn.Dispose()

                    If ds.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If Not String.IsNullOrEmpty(ds.Tables(0).Rows(i)("EmailPerson").ToString()) Then
                                OfficeEmail += ds.Tables(0).Rows(i)("EmailPerson").ToString() + ";"
                            End If

                            If Not String.IsNullOrEmpty(ds.Tables(0).Rows(i)("EmailCompany").ToString()) Then
                                OfficeEmail += ds.Tables(0).Rows(i)("EmailCompany").ToString() + ";"
                            End If

                            If Not String.IsNullOrEmpty(ds.Tables(0).Rows(i)("HomeEmail").ToString()) Then
                                OfficeEmail += ds.Tables(0).Rows(i)("HomeEmail").ToString() + ";"
                            End If
                        Next
                        OfficeEmail = OfficeEmail.TrimEnd(";"c)
                        OfficeEmail = OfficeEmail.Trim()
                    End If
                End If

                Dim OfEmail As String = ""
                selCmd = New MySqlCommand()
                selCmd.CommandType = CommandType.Text
                sqlSelect = "Select OfficeEmail from tblServiceRecordMasterSetUp"
                selCmd.CommandText = sqlSelect
                conn.Open()
                selCmd.Connection = conn
                Dim dr3 As MySqlDataReader = selCmd.ExecuteReader()

                If dr3.HasRows Then
                    While dr3.Read()
                        OfEmail = dr3("OfficeEmail").ToString()
                    End While
                End If

                conn.Close()
                conn.Dispose()

                If OfEmail <> "" Then
                    OfficeEmail += Convert.ToString(";") & OfEmail
                End If

                ds1.Tables.Add("dt1")
                ds1.Tables(0).Columns.Add("OfficeEmail")

                Dim drow As DataRow
                drow = ds1.Tables(0).NewRow()
                drow("OfficeEmail") = OfficeEmail.Trim()
                ds1.Tables("dt1").Rows.Add(drow)

                sqlSelect = "Select RemarkOffice, TargetDescription from tblServiceRecordDet where RecordNo='" & RecordNo & "'"
                selCmd = New MySqlCommand()
                selCmd.CommandText = sqlSelect
                conn.Open()
                selCmd.Connection = conn
                Dim dr1 As MySqlDataReader = selCmd.ExecuteReader()
                Dim dt2 As New DataTable()
                dt2.Load(dr1)
                conn.Close()
                conn.Dispose()

                If dt2.Rows.Count > 0 Then
                    Dim dt3 As New DataTable()
                    dt3.Columns.Add("RemarkOffice")
                    dt3.Columns.Add("TargetDescription")

                    For i As Integer = 0 To dt2.Rows.Count - 1
                        Dim dr2 As DataRow = dt3.NewRow()
                        dr2("RemarkOffice") = dt2.Rows(i)("RemarkOffice").ToString()
                        dr2("TargetDescription") = dt2.Rows(i)("TargetDescription").ToString()
                        dt3.Rows.Add(dr2)
                    Next

                    ds1.Tables.Add(dt3)
                End If

                conn.Close()
                conn.Dispose()
            End Using

            Return ds1
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Public Function GetFreqTarget(t As String()) As String
    '    Try
    '        Dim FreqTarg As String = ""
    '        Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString)
    '            Dim selCmd As New MySqlCommand()
    '            selCmd.CommandType = CommandType.Text
    '            Dim sqlSelect As String = "Select Descrip1 from tblTarget where Upper(TargetId) in ("

    '            For i As Integer = 0 To t.Count() - 1
    '                sqlSelect += "'" + t(i) + "',"
    '            Next
    '            sqlSelect = sqlSelect.TrimEnd(","c)
    '            sqlSelect += ")"

    '            selCmd.CommandText = sqlSelect
    '            conn.Open()
    '            selCmd.Connection = conn
    '            Dim dr As MySqlDataReader = selCmd.ExecuteReader()

    '            If dr.HasRows Then
    '                Dim dt2 As New DataTable()
    '                dt2.Load(dr)
    '                If dt2.Rows.Count > 0 Then
    '                    For j As Integer = 0 To dt2.Rows.Count - 1
    '                        FreqTarg += dt2.Rows(j)("Descrip1").ToString() + ","
    '                    Next
    '                End If
    '            End If
    '            conn.Close()
    '            conn.Dispose()
    '        End Using

    '        Return FreqTarg
    '    Catch ex As Exception
    '        Return ex.Message.ToString()
    '    End Try
    'End Function

    'Public Function GetSvcMainReport(RecordNo As String) As dsServiceRpt
    '    Try
    '        Dim ds As New dsServiceRpt()
    '        Dim dsSvc As New DataSet()
    '        Dim dsSvcDet As New DataSet()
    '        Dim dsTeam As New DataSet()

    '        If RecordNo <> "" Then
    '            Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString)
    '                Dim selCmd As New MySqlCommand()
    '                selCmd.CommandType = CommandType.Text

    '                Dim sqlSelect As String = "Select "
    '                sqlSelect += "RecordNo, "
    '                sqlSelect += "ContractNo, "
    '                sqlSelect += "AccountId as 'CustCode', "
    '                sqlSelect += "CustName, "
    '                sqlSelect += "Address1, "
    '                sqlSelect += "AddUnit, "
    '                sqlSelect += "AddBuilding, "
    '                sqlSelect += "AddStreet, "
    '                sqlSelect += "AddCity, "
    '                sqlSelect += "AddState, "
    '                sqlSelect += "AddCountry, "
    '                sqlSelect += "AddPostal, "
    '                sqlSelect += "ContactPersonID as 'Contact', "
    '                sqlSelect += "DATE_FORMAT(ServiceDate,'%d/%m/%Y') as 'ServiceDate', "
    '                sqlSelect += "TimeIn, "
    '                sqlSelect += "TimeOut, "
    '                sqlSelect += "OurRef, "
    '                sqlSelect += "ScheduleType, "
    '                sqlSelect += "Settle, "
    '                sqlSelect += "RefNo, "
    '                sqlSelect += "LocationID as 'PoNo', "
    '                sqlSelect += "ServiceBy, "
    '                sqlSelect += "InchargeId, "
    '                sqlSelect += "CollectAmt, "
    '                sqlSelect += "CollectPayment, "
    '                sqlSelect += "CustomerSignName, "
    '                sqlSelect += "CustomerSign, "
    '                sqlSelect += "ServiceBySign "
    '                sqlSelect += "from tblServiceRecord "
    '                sqlSelect += "where "
    '                sqlSelect += "RecordNo=@RecordNo"

    '                selCmd.CommandText = sqlSelect
    '                selCmd.Parameters.Clear()
    '                selCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
    '                conn.Open()
    '                selCmd.Connection = conn
    '                Dim da As New MySqlDataAdapter()
    '                da.SelectCommand = selCmd
    '                da.Fill(dsSvc)
    '                conn.Close()
    '                conn.Dispose()

    '                '   sqlSelect = "Select RecordNo, Concat(Frequency, ':', TargetId) as 'FreqTarget', ";
    '                sqlSelect = "Select RecordNo, frequency, targetId, "
    '                sqlSelect += "Action, "
    '                sqlSelect += "RemarkClient, "
    '                sqlSelect += "Material "
    '                sqlSelect += "from tblServiceRecordDet "
    '                sqlSelect += "where "
    '                sqlSelect += "RecordNo=@RecordNo"
    '                selCmd.CommandText = sqlSelect
    '                selCmd.Parameters.Clear()
    '                selCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
    '                conn.Open()
    '                selCmd.Connection = conn
    '                da = New MySqlDataAdapter()
    '                da.SelectCommand = selCmd
    '                da.Fill(dsSvcDet)
    '                conn.Close()
    '                conn.Dispose()

    '                If dsSvc IsNot Nothing AndAlso dsSvcDet IsNot Nothing Then
    '                    Dim dr As DataRow = ds.Tables("dtSvcRpt").NewRow()
    '                    If dsSvc.Tables(0).Rows.Count > 0 Then
    '                        dr("ContractNo") = dsSvc.Tables(0).Rows(0)("ContractNo").ToString()
    '                        dr("CustCode") = dsSvc.Tables(0).Rows(0)("CustCode").ToString()
    '                        dr("CustName") = dsSvc.Tables(0).Rows(0)("CustName").ToString()

    '                        Dim Address As String = ""
    '                        Dim CustAddress As String = ""

    '                        If Not String.IsNullOrEmpty(dsSvc.Tables(0).Rows(0)("Address1").ToString()) Then
    '                            Address += dsSvc.Tables(0).Rows(0)("Address1").ToString() + ", "
    '                        End If

    '                        If Not String.IsNullOrEmpty(dsSvc.Tables(0).Rows(0)("AddUnit").ToString()) Then
    '                            Address += dsSvc.Tables(0).Rows(0)("AddUnit").ToString() + ", "
    '                        End If

    '                        If Not String.IsNullOrEmpty(dsSvc.Tables(0).Rows(0)("AddBuilding").ToString()) Then
    '                            Address += dsSvc.Tables(0).Rows(0)("AddBuilding").ToString() + ", "
    '                        End If

    '                        If Not String.IsNullOrEmpty(dsSvc.Tables(0).Rows(0)("AddStreet").ToString()) Then
    '                            Address += dsSvc.Tables(0).Rows(0)("AddStreet").ToString() + ", "
    '                        End If

    '                        If Not String.IsNullOrEmpty(dsSvc.Tables(0).Rows(0)("AddCity").ToString()) Then
    '                            Address += dsSvc.Tables(0).Rows(0)("AddCity").ToString() + ", "
    '                        End If

    '                        If Not String.IsNullOrEmpty(dsSvc.Tables(0).Rows(0)("AddState").ToString()) Then
    '                            Address += dsSvc.Tables(0).Rows(0)("AddState").ToString() + ", "
    '                        End If

    '                        If Not String.IsNullOrEmpty(dsSvc.Tables(0).Rows(0)("AddCountry").ToString()) Then
    '                            Address += dsSvc.Tables(0).Rows(0)("AddCountry").ToString() + ", "
    '                        End If

    '                        If Not String.IsNullOrEmpty(dsSvc.Tables(0).Rows(0)("AddPostal").ToString()) Then
    '                            Address += dsSvc.Tables(0).Rows(0)("AddPostal").ToString() + ", "
    '                        End If

    '                        CustAddress = Address.Trim()
    '                        CustAddress = CustAddress.TrimEnd(","c)
    '                        dr("CustAddress") = CustAddress

    '                        'dr["CustAddress"] = dsSvc.Tables[0].Rows[0]["CustAddress"].ToString();
    '                        dr("Contact") = dsSvc.Tables(0).Rows(0)("Contact").ToString()
    '                        dr("ServiceDate") = dsSvc.Tables(0).Rows(0)("ServiceDate").ToString()
    '                        dr("TimeIn") = dsSvc.Tables(0).Rows(0)("TimeIn").ToString()
    '                        dr("TimeOut") = dsSvc.Tables(0).Rows(0)("TimeOut").ToString()
    '                        dr("OurRef") = dsSvc.Tables(0).Rows(0)("OurRef").ToString()
    '                        dr("ScheduleType") = dsSvc.Tables(0).Rows(0)("ScheduleType").ToString()
    '                        dr("Settle") = dsSvc.Tables(0).Rows(0)("Settle").ToString()
    '                        dr("RefNo") = dsSvc.Tables(0).Rows(0)("RefNo").ToString()
    '                        dr("PoNo") = dsSvc.Tables(0).Rows(0)("PoNo").ToString()
    '                        dr("ServiceBy") = dsSvc.Tables(0).Rows(0)("ServiceBy").ToString()
    '                        dr("CollectAmt") = dsSvc.Tables(0).Rows(0)("CollectAmt").ToString()
    '                        dr("CollectPayment") = dsSvc.Tables(0).Rows(0)("CollectPayment").ToString()
    '                        dr("CustomerSignName") = dsSvc.Tables(0).Rows(0)("CustomerSignName").ToString()


    '                        If Not (DBNull.Value.Equals(dsSvc.Tables(0).Rows(0)("CustomerSign"))) Then
    '                            dr("CustomerSign") = DirectCast(dsSvc.Tables(0).Rows(0)("CustomerSign"), Byte())
    '                        End If

    '                        If Not (DBNull.Value.Equals(dsSvc.Tables(0).Rows(0)("ServiceBySign"))) Then
    '                            dr("ServiceBySign") = DirectCast(dsSvc.Tables(0).Rows(0)("ServiceBySign"), Byte())
    '                        End If

    '                        dr("RecordNo") = dsSvc.Tables(0).Rows(0)("RecordNo").ToString()
    '                        dr("InchargeId") = dsSvc.Tables(0).Rows(0)("InchargeId").ToString()

    '                        sqlSelect = "Select distinct StaffName "
    '                        sqlSelect += "from tblServiceRecordStaff "
    '                        sqlSelect += "where "
    '                        sqlSelect += "RecordNo=@RecordNo"
    '                        selCmd.CommandText = sqlSelect
    '                        selCmd.Parameters.Clear()
    '                        selCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
    '                        conn.Open()
    '                        selCmd.Connection = conn
    '                        da = New MySqlDataAdapter()
    '                        da.SelectCommand = selCmd
    '                        da.Fill(dsTeam)

    '                        conn.Close()
    '                        conn.Dispose()

    '                        If dsTeam IsNot Nothing Then
    '                            If dsTeam.Tables(0).Rows.Count > 0 Then
    '                                Dim team As String = ""
    '                                For Each r As DataRow In dsTeam.Tables(0).Rows
    '                                    team = team & String.Concat(r("StaffName").ToString(), ", ")
    '                                Next
    '                                If team <> "" Then
    '                                    dr("TeamMembers") = team.TrimEnd(","c)
    '                                Else
    '                                    dr("TeamMembers") = team
    '                                End If
    '                            End If
    '                        End If

    '                        ds.Tables("dtSvcRpt").Rows.Add(dr)
    '                    End If

    '                    If dsSvcDet.Tables(0).Rows.Count > 0 Then
    '                        For i As Integer = 0 To dsSvcDet.Tables(0).Rows.Count - 1
    '                            Dim dr1 As DataRow = ds.Tables("dtSvcDet").NewRow()
    '                            dr1("RecordNo") = dsSvcDet.Tables(0).Rows(i)("RecordNo").ToString()

    '                            Dim freq As String = dsSvcDet.Tables(0).Rows(i)("frequency").ToString()
    '                            Dim Targ As String = dsSvcDet.Tables(0).Rows(i)("TargetId").ToString()
    '                            Dim Descrip As String = ""

    '                            If Not String.IsNullOrEmpty(Targ) AndAlso Targ <> "" Then
    '                                Dim t As String() = Targ.Split(","c)
    '                                Descrip = GetFreqTarget(t)
    '                                Descrip = Descrip.TrimEnd(","c)
    '                            End If

    '                            dr1("FreqTarget") = Convert.ToString(freq & Convert.ToString(" - ")) & Descrip
    '                            dr1("Action") = dsSvcDet.Tables(0).Rows(i)("Action").ToString()
    '                            dr1("RemarkClient") = dsSvcDet.Tables(0).Rows(i)("RemarkClient").ToString()
    '                            dr1("Material") = dsSvcDet.Tables(0).Rows(i)("Material").ToString()

    '                            ds.Tables("dtSvcDet").Rows.Add(dr1)
    '                        Next
    '                    End If
    '                End If
    '            End Using
    '        End If

    '        Return ds
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    Public Function GetSvcSuppReport(RecordNo As String) As DataSet
        Try
            Dim ds As New DataSet()
            ds.Tables.Add("DataTable1")

            If RecordNo <> "" Then
                Using conn As New MySqlConnection(ConfigurationManager.ConnectionStrings("sitadataImagesConnectionString").ConnectionString)
                    Dim selCmd As New MySqlCommand()
                    selCmd.CommandType = CommandType.Text
                    Dim sqlSelect As String = "Select "
                    sqlSelect += "RecordNo, "
                    sqlSelect += "FileType, "
                    sqlSelect += "FileSize, "
                    sqlSelect += "Photo "
                    sqlSelect += "from tblServicePhoto "
                    sqlSelect += "where "
                    sqlSelect += "RecordNo=@RecordNo"
                    selCmd.CommandText = sqlSelect
                    selCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
                    conn.Open()
                    selCmd.Connection = conn
                    Dim da As New MySqlDataAdapter()
                    da.SelectCommand = selCmd
                    da.Fill(ds, "DataTable1")
                    conn.Close()
                    conn.Dispose()
                End Using
            End If
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function ReadFully(input As Stream) As Byte()
        Try
            Using ms As New MemoryStream()
                input.CopyTo(ms)
                Return ms.ToArray()
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        GridView1.DataBind()
    End Sub

    Private Sub RetrieveSvcInfo(recordno As String)
        Dim CustAddress As String = ""
        Dim Address As String = ""
        Dim CustName As String = ""
        Dim ServiceDate As String = ""
        Dim ServiceBy As String = ""
        Dim RemarksClient As String = ""
        Dim RemarksClientSub As String = ""
        Dim ScheduleType As String = ""

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblservicerecord where recordno='" & recordno & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)
            Dim ToEmailID As String = ""
            Dim CCEmailID As String = ""


            If dt1.Rows.Count > 0 Then

                'If dt1.Rows(0)("Email").ToString <> "" Or String.IsNullOrEmpty(dt1.Rows(0)("Email").ToString) = False Then
                '    ToEmailID = ToEmailID + dt1.Rows(0)("Email").ToString + ";"
                'End If

                'If dt1.Rows(0)("Contact2Email").ToString <> "" Or String.IsNullOrEmpty(dt1.Rows(0)("Contact2Email").ToString) = False Then
                '    ToEmailID = ToEmailID + dt1.Rows(0)("Contact2Email").ToString + ";"
                'End If

                'If dt1.Rows(0)("OtherEmail").ToString <> "" Or String.IsNullOrEmpty(dt1.Rows(0)("OtherEmail").ToString) = False Then
                '    ToEmailID = ToEmailID + dt1.Rows(0)("OtherEmail").ToString + ";"
                'End If
                'txtTo.Text = ToEmailID


                If dt1.Rows(0)("Email").ToString <> "" Or String.IsNullOrEmpty(dt1.Rows(0)("Email").ToString) = False Then
                    ToEmailID = ToEmailID + dt1.Rows(0)("Email").ToString.Trim + ";"
                End If

                If dt1.Rows(0)("Contact2Email").ToString <> "" Or String.IsNullOrEmpty(dt1.Rows(0)("Contact2Email").ToString) = False Then
                    ToEmailID = ToEmailID + dt1.Rows(0)("Contact2Email").ToString.Trim + ";"
                End If

                If dt1.Rows(0)("OtherEmail").ToString <> "" Or String.IsNullOrEmpty(dt1.Rows(0)("OtherEmail").ToString) = False Then
                    CCEmailID = dt1.Rows(0)("OtherEmail").ToString.Trim + ";"
                End If

                If Right(ToEmailID.Trim(), 1) = ";" Then
                    ToEmailID = (ToEmailID.Remove(ToEmailID.Length - 1, 1))
                End If

                txtTo.Text = ToEmailID.Trim()
                txtCC.Text = CCEmailID

                If String.IsNullOrEmpty(txtTo.Text) Then
                    txtTo.Text = txtCC.Text
                    txtCC.Text = ""
                End If

                'Testing 
                '  txtTo.Text = "sasi.vishwa@cwbinfotech.com"
                '   txtTo.Text = "Christian.Reyes@anticimex.com.sg;LiuChenhong7.11@gmail.com;thina.geran@anticimex.com.sg;"

                CustName = dt1.Rows(0)("CustName").ToString
                ServiceBy = dt1.Rows(0)("ServiceBy").ToString
                ServiceDate = Convert.ToDateTime(dt1.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
                ScheduleType = dt1.Rows(0)("ScheduleType").ToString

                If Not String.IsNullOrEmpty(dt1.Rows(0)("Address1").ToString()) Then
                    Address += dt1.Rows(0)("Address1").ToString() + ", "
                End If

                If Not String.IsNullOrEmpty(dt1.Rows(0)("AddUnit").ToString()) Then
                    Address += dt1.Rows(0)("AddUnit").ToString() + ", "
                End If

                If Not String.IsNullOrEmpty(dt1.Rows(0)("AddBuilding").ToString()) Then
                    Address += dt1.Rows(0)("AddBuilding").ToString() + ", "
                End If

                If Not String.IsNullOrEmpty(dt1.Rows(0)("AddStreet").ToString()) Then
                    Address += dt1.Rows(0)("AddStreet").ToString() + ", "
                End If

                If Not String.IsNullOrEmpty(dt1.Rows(0)("AddCity").ToString()) Then
                    Address += dt1.Rows(0)("AddCity").ToString() + ", "
                End If

                If Not String.IsNullOrEmpty(dt1.Rows(0)("AddState").ToString()) Then
                    Address += dt1.Rows(0)("AddState").ToString() + ", "
                End If

                If Not String.IsNullOrEmpty(dt1.Rows(0)("AddCountry").ToString()) Then
                    Address += dt1.Rows(0)("AddCountry").ToString() + ", "
                End If

                If Not String.IsNullOrEmpty(dt1.Rows(0)("AddPostal").ToString()) Then
                    Address += dt1.Rows(0)("AddPostal").ToString() + ", "
                End If

                CustAddress = Address.Trim()
                CustAddress = CustAddress.TrimEnd(","c)


            End If


            dt1.Clear()
            dr1.Close()

            ' Retrieve Remarks for Client from tblservicerecorddet

            Dim commandR As MySqlCommand = New MySqlCommand

            commandR.CommandType = CommandType.Text

            commandR.Connection = conn

            commandR.CommandText = "Select RemarkClient from tblServiceRecordDet where RecordNo='" + recordno + "'"
            commandR.Connection = conn

            Dim drR As MySqlDataReader = commandR.ExecuteReader()
            Dim dtR As New DataTable
            dtR.Load(drR)

            If dtR.Rows.Count > 0 Then
                For i As Int16 = 0 To dtR.Rows.Count - 1

                    If dtR.Rows(i)("RemarkClient").ToString <> DBNull.Value.ToString Then
                        If String.IsNullOrEmpty(dtR.Rows(i)("RemarkClient")) = False Then
                            RemarksClient += dtR.Rows(i)("RemarkClient").ToString.Trim + " <br/>"
                            If RemarksClientSub = "" Then
                                RemarksClientSub = dtR.Rows(i)("RemarkClient").ToString.Trim
                            Else
                                RemarksClientSub = RemarksClientSub + ", " + dtR.Rows(i)("RemarkClient").ToString.Trim
                            End If

                        End If
                    End If
                Next
            End If

            dtR.Clear()

            drR.Close()
            commandR.Dispose()

            ' Retrieve Specific Location Name from tblservicerecord2
            Dim LocationName As String = ""
            Dim commandL As MySqlCommand = New MySqlCommand

            commandL.CommandType = CommandType.Text

            commandL.Connection = conn

            commandL.CommandText = "Select SpecificLocationName from tblServiceRecord2 where RecordNo='" + recordno + "'"
            commandL.Connection = conn

            Dim drL As MySqlDataReader = commandL.ExecuteReader()
            Dim dtL As New DataTable
            dtL.Load(drL)

            If dtL.Rows.Count > 0 Then

                If dtL.Rows(0)("SpecificLocationName").ToString <> DBNull.Value.ToString Then
                    If String.IsNullOrEmpty(dtL.Rows(0)("SpecificLocationName")) = False Then
                        LocationName = dtL.Rows(0)("SpecificLocationName").ToString.Trim

                    End If
                End If

            End If


            dtL.Clear()

            drL.Close()
            commandL.Dispose()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn

            command.CommandText = "Select * from tblEmailSetUp where SetUpID='SERV-REC-1'"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            Dim subject As String = ""
            Dim content As String = ""

            If dt.Rows.Count > 0 Then
                subject = dt.Rows(0)("Subject").ToString
                content = dt.Rows(0)("Contents").ToString
                txtCC.Text = txtCC.Text + dt.Rows(0)("ReceiverCC").ToString.Trim
            End If


            content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            content = content.Replace("CLIENT NAME", CustName)
            content = content.Replace("CUSTOMER ADDRESS", CustAddress)
            content = content.Replace("SERVICE DATE", ServiceDate)
            content = content.Replace("COMPANY NAME", ConfigurationManager.AppSettings("CompanyName").ToString())

            content = content.Replace("SERVICE RECORD NUMBER", recordno)
            content = content.Replace("SERVICE BY", ServiceBy)
            content = content.Replace("REMARKS TO CLIENT", RemarksClient)
            content = content.Replace("STAFF ID", Convert.ToString(Session("UserID")))
            content = content.Replace("EMAIL SENT DATE", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", New System.Globalization.CultureInfo("en-GB")))
            content = content.Replace("LOCATION", "LOCATION : " + LocationName)

            subject = System.Text.RegularExpressions.Regex.Replace(subject, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            subject = subject.Replace("CLIENT NAME", CustName)
            subject = subject.Replace("SERVICE DATE", ServiceDate)
            subject = subject.Replace("SERVICE RECORD NUMBER", recordno)
            subject = subject.Replace("CUSTOMER ADDRESS", CustAddress)
            subject = subject.Replace("REMARKS CLIENT", RemarksClientSub)
            subject = subject.Replace("SCHEDULE TYPE", ScheduleType)
            subject = subject.Replace("LOCATION", LocationName)

            txtSubject.Text = subject
            txtContent.Text = content

            dt.Clear()
            dr.Close()
            conn.Close()
        Catch ex As Exception
            lblAlert.Text = lblRecordNo.Text + ": " + ex.Message.ToString()
            InsertIntoTblWebEventLog("GenerateAttachment", ex.Message.ToString, lblRecordNo.Text)
        End Try

    End Sub

    Private Sub GenerateAttachment(recordno As String)
        Try
            crReportDocument.Load(Server.MapPath("~/Reports/ServiceReportTechSign.rpt"))
            crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
            crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
            crReportDocument.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
            crReportDocument.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

            crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
            crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
            crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

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

            Dim FilePath As String = ""
            crReportDocument.RecordSelectionFormula = "{tblservicerecord1.Recordno} = '" + recordno + "'"
            FilePath = Server.MapPath("~/PDFs/BatchEmail/" + recordno + ".PDF")

            If File.Exists(FilePath) Then
                File.Delete(FilePath)

            End If
            oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

            expo = crReportDocument.ExportOptions

            expo.ExportDestinationType = ExportDestinationType.DiskFile

            expo.ExportFormatType = ExportFormatType.PortableDocFormat

            expo.DestinationOptions = oDfDopt

            crReportDocument.Export()

            crReportDocument.Close()
            '    crReportDocument.Dispose()





            'Dim ds1 As DataSet = New DataSet()

            'ds1 = GetSvcImages(lblRecordNo.Text)
            'If (ds1.Tables(0).Rows.Count > 0) Then
            '    crReportDocument.Load(Server.MapPath("~/Reports/ServiceSupplementReport.rpt"))

            '    Dim InchargeId As String = ""
            '    Dim ServiceDate As String = ""
            '    Dim OurRef As String = ""
            '    Dim dsSupplement As dsSvcPhoto = New dsSvcPhoto()

            '    Dim dsDetails As DataSet = GetSvcDetails(lblRecordNo.Text)
            '    If (dsDetails.Tables(0).Rows.Count > 0) Then
            '        InchargeId = dsDetails.Tables(0).Rows(0)("InchargeId").ToString()
            '        ServiceDate = Convert.ToDateTime(dsDetails.Tables(0).Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
            '    End If


            '    Dim fCol As New List(Of Byte())()
            '    Dim SCol As New List(Of Byte())()

            '    Dim i As Integer = 0

            '    For Each ro As DataRow In ds1.Tables(0).Rows
            '        i += 1
            '        If i Mod 2 <> 0 Then
            '            fCol.Add(DirectCast(ro("Photo"), Byte()))
            '        Else
            '            SCol.Add(DirectCast(ro("Photo"), Byte()))
            '        End If
            '    Next

            '    If (fCol.Count > 0) Then
            '        For j As Integer = 0 To fCol.Count - 1
            '            Dim dr As DataRow = dsSupplement.Tables("dtSupp").NewRow()
            '            dr("RecordNo") = lblRecordNo.Text
            '            dr("ServiceDate") = ServiceDate
            '            dr("OurRef") = OurRef
            '            dr("InchargeId") = InchargeId
            '            dr("FColPhoto") = DirectCast(fCol(j), [Byte]())

            '            If (SCol.Count > 0) Then
            '                If (j <> SCol.Count) Then
            '                    dr("SColPhoto") = DirectCast(SCol(j), [Byte]())
            '                End If
            '            End If
            '            dsSupplement.Tables("dtSupp").Rows.Add(dr)
            '        Next
            '    End If


            '    crReportDocument.Load(Server.MapPath("~/Reports/ServiceSupplementReport.rpt"))

            '    Dim myConnectionInfo1 As ConnectionInfo = New ConnectionInfo()


            '    Dim myTables1 As Tables = crReportDocument.Database.Tables

            '    For Each myTable1 As CrystalDecisions.CrystalReports.Engine.Table In myTables1
            '        Dim myTableLogonInfo1 As TableLogOnInfo = myTable1.LogOnInfo
            '        myConnectionInfo1.ServerName = ConfigurationManager.AppSettings("ServerName")
            '        myConnectionInfo1.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
            '        myConnectionInfo1.UserID = ConfigurationManager.AppSettings("UserName")
            '        myConnectionInfo1.Password = ConfigurationManager.AppSettings("Password")
            '        myTable1.ApplyLogOnInfo(myTableLogonInfo1)
            '        myTableLogonInfo1.ConnectionInfo = myConnectionInfo1

            '    Next
            '    crReportDocument.SetDataSource(dsSupplement.Tables("dtSupp"))

            '    Dim FilePath1 As String = ""

            '    FilePath1 = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF")


            '    If File.Exists(FilePath1) Then
            '        File.Delete(FilePath1)

            '    End If
            '    oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

            '    expo = crReportDocument.ExportOptions

            '    expo.ExportDestinationType = ExportDestinationType.DiskFile

            '    expo.ExportFormatType = ExportFormatType.PortableDocFormat

            '    expo.DestinationOptions = oDfDopt

            '    crReportDocument.Export()

            'End If
        Catch ex As Exception
            lblAlert.Text = lblRecordNo.Text + ": " + ex.Message.ToString()
            InsertIntoTblWebEventLog("GenerateAttachment", ex.Message.ToString, lblRecordNo.Text)
        End Try


    End Sub

    Public Function GetSvcDetails(RecordNo As String) As DataSet
        Try
            Dim ds As New DataSet()
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim selCmd As New MySqlCommand()
            selCmd.CommandType = CommandType.Text
            Dim sqlSelect As String = "Select * from tblServiceRecord where RecordNo=@RecordNo"
            selCmd.CommandText = sqlSelect
            selCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
            conn.Open()
            selCmd.Connection = conn
            Dim da As New MySqlDataAdapter()
            da.SelectCommand = selCmd
            da.Fill(ds)
            conn.Close()
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetSvcImages(RecordNo As String) As DataSet
        Try
            Dim ds As New DataSet()
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataImagesConnectionString").ConnectionString

            Dim selCmd As New MySqlCommand()
            selCmd.CommandType = CommandType.Text
            Dim sqlSelect As String = "Select "
            sqlSelect += "RecordNo, "
            sqlSelect += "FileType, "
            sqlSelect += "FileSize, "
            sqlSelect += "Photo "
            sqlSelect += "from tblServicePhoto "
            sqlSelect += "where "
            sqlSelect += "RecordNo=@RecordNo limit 20"
            selCmd.CommandText = sqlSelect
            selCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
            conn.Open()
            selCmd.Connection = conn
            Dim da As New MySqlDataAdapter()
            da.SelectCommand = selCmd
            da.Fill(ds, "DataTable1")
            conn.Close()
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub Send()

        Try
            'Dim ToAddress As String() = txtTo.Text.Split(";"c)
            'txtTo.Text = ""
            ' If ToAddress.Count() > 0 Then
            '    For i As Integer = 0 To ToAddress.Count() - 1
            '        ' oMail.[To].Add(New MailAddress(ToAddress(i).ToString()))
            '        txtTo.Text = ToAddress(i).ToString().Trim
            '    Next
            'End If

            '    txtTo.Text = "sasi.vishwa@gmail.com; sasi_vishwanath@yahoo.com; sasi.vishwa@gmail.com;"
            '   txtCC.Text = ""


            If txtTo.Text.Last.ToString = ";" Then
                txtTo.Text = txtTo.Text.Remove(txtTo.Text.Length - 1)

            End If

            If String.IsNullOrEmpty(txtCC.Text) = False Then
                If txtCC.Text.Last.ToString = ";" Then
                    txtCC.Text = txtCC.Text.Remove(txtCC.Text.Length - 1)

                End If
            End If


            txtTo.Text = Regex.Replace(txtTo.Text, "\s", "")
            txtCC.Text = Regex.Replace(txtCC.Text, "\s", "")

            txtTo.Text = Regex.Replace(txtTo.Text, "\r\n", ";")
            txtCC.Text = Regex.Replace(txtCC.Text, "\r\n", ";")

            txtTo.Text = Regex.Replace(txtTo.Text, ";;", ";")
            txtCC.Text = Regex.Replace(txtCC.Text, ";;", ";")


            '  txtSearch1ClientName.Text = txtTo.Text

            '  txtTo.Text = "sasi.vishwa@gmail.com; sasi_vishwanath@yahoo.com; sasi.vishwa@gmail.com"
            '  txtCC.Text = ""





            Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
            Dim oSmtp As New SmtpClient()

            oMail.Subject = txtSubject.Text
            oMail.HtmlBody = txtContent.Text
            Dim pattern As String
            pattern = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

            If String.IsNullOrEmpty(txtTo.Text) = False Then
                Dim ToAddress As String() = txtTo.Text.Split(";"c)
                If ToAddress.Count() > 0 Then
                    For i As Integer = 0 To ToAddress.Count() - 1
                        If Regex.IsMatch(ToAddress(i).ToString.Trim, pattern) Then

                        Else
                            MessageBox.Message.Alert(Page, "Enter valid 'TO' Email address" + " (" + ToAddress(i).ToString() + ")", "str")
                            FailureCount = FailureCount + 1
                            Return
                        End If
                        oMail.[To].Add(New MailAddress(ToAddress(i).ToString()))
                    Next
                End If
            End If


            If String.IsNullOrEmpty(txtCC.Text) = False Then
                Dim CCAddress As String() = txtCC.Text.Split(";"c)
                If CCAddress.Count() > 0 Then
                    For i As Integer = 0 To CCAddress.Count() - 1
                        If Regex.IsMatch(CCAddress(i).ToString(), pattern) Then

                        Else
                            MessageBox.Message.Alert(Page, "Enter valid 'CC' Email address" + " (" + CCAddress(i).ToString() + ")", "str")
                            FailureCount = FailureCount + 1
                            Return
                        End If
                        oMail.[Cc].Add(New MailAddress(CCAddress(i).ToString()))
                    Next
                End If
            End If


            Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
            oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
            oServer.ConnectType = SmtpConnectType.ConnectDirectSSL


            ' oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))

            oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

            oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
            oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

            oMail.AddAttachment(Server.MapPath("~/PDFs/BatchEmail/" + lblRecordNo.Text + ".PDF"))

            'If File.Exists(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF")) Then
            '    oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF"))
            'End If


            oSmtp.SendMail(oServer, oMail)

            UpdateEmailSentField()

            System.IO.File.Delete(Server.MapPath("~/PDFs/BatchEmail/" + lblRecordNo.Text + ".PDF"))

            'If File.Exists(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF")) Then

            '    System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF"))
            'End If


            oSmtp.Close()


        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            MessageBox.Message.Alert(Page, exstr, "str")
            InsertIntoTblWebEventLog("Send", ex.Message.ToString, lblRecordNo.Text)
            FailureCount = FailureCount + 1
        End Try

    End Sub

    Private Sub UpdateEmailSentField()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn
            command.CommandText = "UPDATE tblservicerecord SET EmailSent = @EmailSent,EmailSentDate = @EmailSentDate WHERE RecordNo = @RecordNo"
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@EmailSent", 1)
            command.Parameters.AddWithValue("@EmailSentDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            command.Parameters.AddWithValue("@RecordNo", lblRecordNo.Text)
            command.ExecuteNonQuery()

            command.Dispose()

            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            FailureCount = FailureCount + 1
            InsertIntoTblWebEventLog("UpdateEmailSendField", ex.Message.ToString, lblRecordNo.Text)
        End Try
    End Sub

    Private Sub SendEmail(recordno As String)
        'Dim SuccessCount As Int16 = 0
        'Dim FailureCount As Int16 = 0
        Try
            lblQuery.Text = "Mail Sent Successfully"
            lblRecordNo.Text = recordno
            '  InsertIntoTblWebEventLog("BatchEmail Test1", recordno, lblRecordNo.Text)
            RetrieveSvcInfo(recordno)
            GenerateWarranty(recordno)

            GenerateAttachment(recordno)
            Send()
            SuccessCount = SuccessCount + 1
        Catch ex As Exception
            FailureCount = FailureCount + 1
            InsertIntoTblWebEventLog("SendEmail", ex.Message.ToString, lblRecordNo.Text)
        End Try
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
            insCmds.Parameters.AddWithValue("@LoginId", "BATCHEMAIL - " + Session("UserID"))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click1(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim query As String = GenerateQuery()
        lblAlert.Text = ""
        lblMessage.Text = ""
        If (query = "INVALID SERVICE DATE FROM") Then
            lblAlert.Text = query
            Exit Sub
        End If

        If (query = "PLEASE ENTER SERVICE DATE FROM") Then
            lblAlert.Text = query
            Exit Sub
        End If

        If (query = "INVALID SERVICE DATE TO") Then
            lblAlert.Text = query
            Exit Sub
        End If

        If (query = "PLEASE ENTER SERVICE DATE TO") Then
            lblAlert.Text = query
            Exit Sub
        End If
        PopulateGrid(query)
    End Sub


End Class
