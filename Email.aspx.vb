Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text.html
'Imports iTextSharp.text.html.simpleparser
Imports System.Drawing
Imports System.IO
'Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.Net
Imports EASendMail
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Class Email
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim crReportDocument1 As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions

  
    Public isInPage As Boolean = False
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
    Public EmailSent As String = "No"

    Public AdditionalFile As String = "N"
    Public Period As String = ""
    Public InvDate As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Try
        If Not IsPostBack Then
            Dim RecordNo As String = ""
            Dim UserID As String = Convert.ToString(Session("UserID"))

            txtManualRpt.Text = ""
            '     Dim Query As String = Convert.ToString(Session("Query"))
            txtQuery.Text = Request.QueryString("Type")
            lnkPreview1.Text = ""

            lnkPreview2.Text = ""

            '    lnkPreview.Text = lblRecordNo.Text
            If txtQuery.Text = "ServiceReportTechSign" Then
                RecordNo = Convert.ToString(Session("SvcRecordNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text

                RetrieveSvcInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "SvcSupplement" Then
                RecordNo = Convert.ToString(Session("SvcRecordNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text + "_Supplement.PDF"
                RetrieveSvcInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "ServiceReport" Then
                RecordNo = Convert.ToString(Session("SvcRecordNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text + "_ServiceForm.PDF"
                RetrieveSvcInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "SanitationReport" Then
                RecordNo = Convert.ToString(Session("SvcRecordNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text + "_SanitationForm.PDF"
                RetrieveSvcInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "ServiceRecordPrinting" Then
                RecordNo = Convert.ToString(Session("selFormula"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = "Service Reports"
                RetrieveEmailInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "SOA" Then
                RecordNo = Convert.ToString(Session("selFormula"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = "Statement of Accounts"
                RetrieveSOAInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "CustSOA" Then
                Dim AccountID As String = Convert.ToString(Session("AccountID"))
                Dim AccountName As String = Convert.ToString(Session("CustName"))
                Dim AccountType As String = Convert.ToString(Session("AccountTypeSOA"))
                '  Dim CutOffDate As String = Convert.ToString(Session("SysDate"))
                Dim CutOffDate As String = Convert.ToString(Session("cutoffoscustomer"))
                GetDataInvRecvTest(AccountID, AccountType, CutOffDate, UserID)

                '    RecordNo = Convert.ToString(Session("selFormula"))
                lblRecordNo.Text = AccountID
                lnkPreview.Text = "Outstanding Invoice Statement"
                txtSubject.Text = "ANTICIMEX STATEMENT OF ACCOUNTS : " & AccountID & " - " & AccountName
                txtContent.Text = "Please find the Statement of Accounts attached."
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "ReceiptTransactions" Then
                Dim AccountID As String = Convert.ToString(Session("AccountID"))

                RecordNo = AccountID
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = "Receipt Transactions"
                txtSubject.Text = "ANTICIMEX RECEIPT TRANSACTIONS"
                txtContent.Text = "Please find the Receipt Transactions attached."
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "TransactionSummary" Then
                Dim AccountID As String = Convert.ToString(Session("AccountID"))

                RecordNo = AccountID
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = "Transaction Summary"
                txtSubject.Text = "ANTICIMEX TRANSACTION SUMMARY"
                txtContent.Text = "Please find the Transaction Summary attached."
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "ContractServiceSchedule" Then
                RecordNo = Convert.ToString(Session("contractno"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveContractInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat1" Then
                '    InsertIntoTblWebEventLog("Email", "1", UserID)

                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()

                '  InsertIntoTblWebEventLog("Email", "2", txtTo.Text)

                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

                '  InsertIntoTblWebEventLog("Email", "3", UserID)

            ElseIf txtQuery.Text = "InvoiceFormat2" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat3" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat4" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat5" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat6" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat7" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat8" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat9" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat10" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat11" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                IndividualorMergedFile()

                If txtFileType.Text = "Individual" Then
                    lnkPreview.Text = lblRecordNo.Text
                    lnkPreview1.Text = lblRecordNo.Text + "_ServiceReports"
                Else
                    lnkPreview.Text = lblRecordNo.Text + "withReports"
                End If

                InsertIntoTblWebEventLog(lnkPreview.Text, lblRecordNo.Text, txtFileType.Text)

                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat12" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat13" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat14" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "InvoiceFormat15" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo
                IndividualorMergedFile()

                If txtFileType.Text = "Individual" Then
                    lnkPreview.Text = lblRecordNo.Text
                    lnkPreview1.Text = lblRecordNo.Text + "_ServiceReports"
                Else
                    lnkPreview.Text = lblRecordNo.Text + "withReports"
                End If


                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "ServiceReportWithPhotos" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo

                lnkPreview.Text = lblRecordNo.Text + "_ServiceReportPhotos"
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "ServiceReportWithoutPhotos" Then
                RecordNo = Convert.ToString(Session("InvoiceNo"))
                lblRecordNo.Text = RecordNo

                lnkPreview.Text = lblRecordNo.Text + "_ServiceReport"
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "CreditNote" Then
                RecordNo = Convert.ToString(Session("RecordNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "CreditNote2" Then
                RecordNo = Convert.ToString(Session("RecordNo"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveInvoiceInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "Receipt" Then
                RecordNo = Convert.ToString(Session("ReceiptNumber"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveReceiptInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "CollectionNote" Then
                RecordNo = Convert.ToString(Session("ReceiptNumber"))
                lblRecordNo.Text = RecordNo
                lnkPreview.Text = lblRecordNo.Text
                RetrieveReceiptInfo()
                txtFrom.Text = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                GenerateAttachment()

            ElseIf txtQuery.Text = "FileUpload" Then

                RecordNo = Convert.ToString(Session("SvcRecordNo"))
                lblRecordNo.Text = RecordNo
                'lnkPreview.Text = lblRecordNo.Text

                RetrieveSvcInfo()
                'txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()


                lnkPreview.Text = Convert.ToString(Session("FileName"))
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()

                'ElseIf txtQuery.Text = "InvoiceFileUpload" Then

                '    RecordNo = Convert.ToString(Session("InvoiceNo"))
                '    lblRecordNo.Text = RecordNo
                '    'lnkPreview.Text = lblRecordNo.Text

                '    RetrieveInvoiceInfo()
                '    'txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                '    GenerateAttachment()


                '    lnkPreview.Text = Convert.ToString(Session("FileName"))
                '    txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()

            ElseIf txtQuery.Text = "ContractFileUpload" Then

                RecordNo = Convert.ToString(Session("ContractNo"))
                lblRecordNo.Text = RecordNo
                'lnkPreview.Text = lblRecordNo.Text

                RetrieveContractInfo()
                'txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()


                lnkPreview.Text = Convert.ToString(Session("FileName"))
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()

            ElseIf txtQuery.Text = "AssetFileUpload" Then

                RecordNo = Convert.ToString(Session("AssetNo"))
                lblRecordNo.Text = RecordNo
                'lnkPreview.Text = lblRecordNo.Text

                ' RetrieveSvcInfo()
                'txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
                GenerateAttachment()


                lnkPreview.Text = Convert.ToString(Session("FileName"))
                txtFrom.Text = ConfigurationManager.AppSettings("EmailFrom").ToString()
            End If

            If AdditionalFile = "Y" Then
                lnkPreview2.Text = "Price Increase Notification"
                ImageButton7.Visible = True
            Else
                ImageButton7.Visible = False
            End If

            '   lnkPreview.Text = "Invoice"
            'Dim SvcRcNo As String = Convert.ToString(Session("SvcRcNo"))
            'txtSvcRcno.Text = SvcRcNo
            '    InsertIntoTblWebEventLog("Email", "4", UserID)

            PreviewFile(sender, e)
            '  InsertIntoTblWebEventLog("Email", "5", UserID)


            '      txtCC.Text = ConfigurationManager.AppSettings("EmailCC").ToString()
            ImageButton1.Visible = False
            ImageButton2.Visible = False
            ImageButton3.Visible = False
            ImageButton4.Visible = False
            ImageButton5.Visible = False
            ImageButton8.Visible = False
            ImageButton9.Visible = False

            If EmailValidation() = True Then

                Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())


                '  Dim oMail As New SmtpMail("TryIt")
                Dim oSmtp As New SmtpClient()
                Dim oServer As New SmtpServer("")
                'Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
                'oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
                'oServer.ConnectType = SmtpConnectType.ConnectDirectSSL

                'Dim pattern As String
                'pattern = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"



                If String.IsNullOrEmpty(txtTo.Text) = False Then
                    '  InsertIntoTblWebEventLog("Email", "6", UserID)
                    Dim invalidemail As String = ""
                    lblToEmail.Text = ""
                    lblWarningEmail.Text = ""

                    lblToEmail.Text = txtTo.Text
                    Dim ToAddress As String() = txtTo.Text.Trim.Split(";"c)
                    '  InsertIntoTblWebEventLog("Email", "7", ToAddress.Count().ToString)

                    If ToAddress.Count() > 0 Then
                        invalidemail = ""

                        For i As Integer = 0 To ToAddress.Count() - 1
                            '  InsertIntoTblWebEventLog("Email", "8", ToAddress(i).ToString.Trim)

                            'If Regex.IsMatch(ToAddress(i).ToString.Trim, pattern) Then

                            'Else
                            '    MessageBox.Message.Alert(Page, "Enter valid 'TO' Email address" + " (" + ToAddress(i).ToString() + ")", "str")
                            '    Return
                            'End If
                            oMail.To = ToAddress(i).ToString.Trim
                            '  oMail.[To].Add(New MailAddress(ToAddress(i).ToString.Trim))
                            Try
                                oSmtp.TestRecipients(oServer, oMail)
                                '   InsertIntoTblWebEventLog("TestEmailRecipientSuccess", ToAddress(i).ToString.Trim, lblRecordNo.Text)

                            Catch ex As Exception
                                If ToAddress(i).ToString.Trim = "" Or ToAddress(i).ToString.Trim = " " Then

                                Else
                                    invalidemail = invalidemail + ToAddress(i).ToString + ";"
                                    txtTo.Text = txtTo.Text.Replace(ToAddress(i).ToString + ";", "")
                                    txtTo.Text = txtTo.Text.Replace(ToAddress(i).ToString, "")
                                    InsertIntoTblWebEventLog("TestEmailRecipientFailed", ToAddress(i).ToString.Trim, lblRecordNo.Text)

                                End If
                            End Try
                        Next
                    End If

                    If invalidemail = "" Then

                    Else
                        lblWarningEmail.Text = invalidemail
                        mdlPopupWarningMsg.Show()
                    End If


                End If
            End If

            ''   InsertIntoTblWebEventLog("Email", "9", UserID)
            'Dim AdditionalFile1 As Boolean = False

            'Dim command2 As MySqlCommand = New MySqlCommand

            'command2.CommandType = CommandType.Text

            'command2.Connection = conn
            'command2.CommandText = "Select * from tblemailadditionalpriceincrease where accountid='" + AccountID + "' and ContractNo in ('" + ContractNo + "') and AddtionalFile='Y'"

            'Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            'Dim dt2 As New System.Data.DataTable
            'dt2.Load(dr2)

            'If dt2.Rows.Count = 0 Then

            'End If

            'command2.Dispose()
            'dt2.Clear()
            'dt2.Dispose()
            'dr2.Close()
            'mdlPopupAttchWarning.Show()
            '    InsertIntoTblWebEventLog("EmailLoad", txtQuery.Text.Substring(0, 13).ToString, lblRecordNo.Text)
            If txtQuery.Text.Substring(0, 7) = "Invoice" Then
                If txtQuery.Text.Substring(0, 13) = "InvoiceFormat" Then
                    Dim conn As MySqlConnection = New MySqlConnection()

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                    conn.Open()
                    Dim qry As String = "SELECT recordno,filename,servicedate,filenamelink,filedescription FROM tblfileupload LEFT JOIN TBLSERVICErecord on tblfileupload.fileref = tblservicerecord.recordno"
                    qry = qry + " where manualreport='Y' and fileref in (SELECT RefType FROM tblsalesdetail where InvoiceNumber = '" + lblRecordNo.Text + "');"
                    Dim check As Boolean = False
                    'Start:Retrive Service Records
                    Dim commandService As MySqlCommand = New MySqlCommand

                    commandService.CommandType = CommandType.Text
                    commandService.CommandText = qry
                    commandService.Connection = conn

                    Dim drService As MySqlDataReader = commandService.ExecuteReader()
                    Dim dtService As New DataTable
                    dtService.Load(drService)

                    If dtService.Rows.Count > 0 Then
                        check = True
                    Else
                        check = False
                    End If

                    conn.Close()
                    conn.Dispose()

                    If check = True Then
                        mdlPopupAttchManualRpt.Show()

                    End If

                End If
            End If

        End If
        'Catch ex As Exception
        '    InsertIntoTblWebEventLog("Page_Load", ex.Message.ToString, lblRecordNo.Text)
        '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        'End Try
    End Sub

    Private Sub GenerateAttachment()
        Dim AccountID As String = ""

        Try
            If txtQuery.Text = "ServiceReportTechSign" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ServiceReportTechSign.rpt"))
                crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                crReportDocument.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                crReportDocument.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

                crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

                'Dim InchargeId As String = ""
                'Dim ServiceDate As String = ""
                'Dim OurRef As String = ""

                'Dim dsDetails As DataSet = GetSvcDetails(lblRecordNo.Text)
                'If (dsDetails.Tables(0).Rows.Count > 0) Then
                '    InchargeId = dsDetails.Tables(0).Rows(0)("InchargeId").ToString()
                '    ServiceDate = Convert.ToDateTime(dsDetails.Tables(0).Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
                'End If


                'Dim ds1 As DataSet = New DataSet()
                'Dim dsSupplement As dsSvcPhoto = New dsSvcPhoto()

                'ds1 = GetSvcImages(lblRecordNo.Text)
                'If (ds1.Tables(0).Rows.Count > 0) Then
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
                '    crReportDocument1.Load(Server.MapPath("~/Reports/ServiceSupplementReport.rpt"))
                '    crReportDocument1.SetDataSource(dsSupplement.Tables("dtSupp"))
                '    Dim FilePath1 As String = ""

                '    FilePath1 = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF")


                '    If File.Exists(FilePath1) Then
                '        File.Delete(FilePath1)

                '    End If
                '    oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

                '    expo = crReportDocument1.ExportOptions

                '    expo.ExportDestinationType = ExportDestinationType.DiskFile

                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat

                '    expo.DestinationOptions = oDfDopt

                '    crReportDocument1.Export()
                '    lnkPreview1.Text = lblRecordNo.Text + "_Supplement.PDF"
                'End If
            ElseIf txtQuery.Text = "SvcSupplement" Then
                'Dim InchargeId As String = ""
                'Dim ServiceDate As String = ""
                'Dim OurRef As String = ""

                'Dim dsDetails As DataSet = GetSvcDetails(lblRecordNo.Text)
                'If (dsDetails.Tables(0).Rows.Count > 0) Then
                '    InchargeId = dsDetails.Tables(0).Rows(0)("InchargeId").ToString()
                '    ServiceDate = Convert.ToDateTime(dsDetails.Tables(0).Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
                'End If


                'Dim ds1 As DataSet = New DataSet()
                'Dim dsSupplement As dsSvcPhoto = New dsSvcPhoto()

                'ds1 = GetSvcImages(lblRecordNo.Text)
                'If (ds1.Tables(0).Rows.Count > 0) Then
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
                '    crReportDocument.SetDataSource(dsSupplement.Tables("dtSupp"))
                'crReportDocument1.Load(Server.MapPath("~/Reports/ServiceSupplementReport.rpt"))
                'crReportDocument1.SetDataSource(dsSupplement.Tables("dtSupp"))
                'Dim FilePath1 As String = ""

                'FilePath1 = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF")


                'If File.Exists(FilePath1) Then
                '    File.Delete(FilePath1)

                'End If
                'oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

                'expo = crReportDocument1.ExportOptions

                'expo.ExportDestinationType = ExportDestinationType.DiskFile

                'expo.ExportFormatType = ExportFormatType.PortableDocFormat

                'expo.DestinationOptions = oDfDopt

                'crReportDocument1.Export()
                'lnkPreview1.Text = lblRecordNo.Text + "_Supplement.PDF"
                '  End If
                crReportDocument.Load(Server.MapPath("~/Reports/ServiceSupplementReport_New.rpt"))

                crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                crReportDocument.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                crReportDocument.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

                crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")



            ElseIf txtQuery.Text = "ServiceReport" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ServiceReport.rpt"))
                crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                crReportDocument.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                crReportDocument.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

                crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

            ElseIf txtQuery.Text = "SanitationReport" Then
                crReportDocument.Load(Server.MapPath("~/Reports/SanitationReport.rpt"))
                crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

            ElseIf txtQuery.Text = "ServiceRecordPrinting" Then
                '  crReportDocument.Load(Server.MapPath("~/Reports/ServiceRecordReports/ServiceRecordPrinting.rpt"))
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/ServiceRecordPrintingInvoice.rpt"))

                crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))

            ElseIf txtQuery.Text = "SOA" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcInvRecvBaseCur.rpt"))
                crReportDocument.SetParameterValue("pStatementDate", Convert.ToDateTime(Session("PrintDate")))
                If Convert.ToString(Session("Type")) = "DEBIT" Then
                    crReportDocument.SetParameterValue("pBal", "DEBIT")
                ElseIf Convert.ToString(Session("Type")) = "ZERO" Then
                    crReportDocument.SetParameterValue("pBal", "ZERO")
                End If
            ElseIf txtQuery.Text = "CustSOA" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcInvRecvBaseCurOS_Cust.rpt"))
                crReportDocument.SetParameterValue("pBal", "ALL")
                crReportDocument.SetParameterValue("pStatementDate", Convert.ToDateTime(Session("cutoffoscustomer")))

            ElseIf txtQuery.Text = "ReceiptTransactions" Then
                AccountID = Convert.ToString(Session("AccountID"))

                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/ReceiptTransaction.rpt"))
                crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))

            ElseIf txtQuery.Text = "TransactionSummary" Then
                AccountID = Convert.ToString(Session("AccountID"))

                '''''''''''''''''''''''
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()
                Dim command As MySqlCommand = New MySqlCommand
                command.CommandType = CommandType.StoredProcedure
                command.CommandText = "SaveTbwARDetail1TransSummary"
                command.Parameters.Clear()

                'DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")
                command.Parameters.AddWithValue("@pr_CutOffdate", DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-GB")))
                'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

                command.Parameters.AddWithValue("@pr_AccountType", "")
                command.Parameters.AddWithValue("@pr_AccountIdFrom", AccountID.Trim)
                command.Parameters.AddWithValue("@pr_AccountIdTo", AccountID.Trim)


                command.Connection = conn
                command.ExecuteScalar()
                conn.Close()
                conn.Dispose()
                command.Dispose()

                '''''''''''''''''''''''''''''''''''''
                'txtAccountID.Text = Convert.ToString(Session("UserId"))

                '   btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TransactionSummary.rpt"))
                crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))

                'crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                'crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))
                crReportDocument.SetParameterValue("pCutOffDate", DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-GB")))

            ElseIf txtQuery.Text = "ContractServiceSchedule" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContractServiceSchedule.rpt"))


                crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                crReportDocument.SetParameterValue("pContractNo", lblRecordNo.Text)
                crReportDocument.SetParameterValue("pStatus", "[""""O""""]")
            ElseIf txtQuery.Text = "InvoiceFormat1" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format1_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat2" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format2_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat3" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format3_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat4" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format4_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat5" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format5_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat6" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format6_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat7" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format7_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat8" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format8_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat9" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format9.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat10" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format10_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat11" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format1_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))


                crReportDocument1.Load(Server.MapPath("~/Reports/ServiceRecordReports/ServiceRecordPrinting.rpt"))

                If ConfigurationManager.AppSettings("DomainName").ToString() = "PEST-PRO" Or ConfigurationManager.AppSettings("DomainName").ToString() = "MALAYSIA" Then

                Else
                    crReportDocument1.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument1.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument1.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument1.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                End If

                'crReportDocument1.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                'crReportDocument1.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                'crReportDocument1.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                'crReportDocument1.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))

                Dim myConnectionInfo1 As ConnectionInfo = New ConnectionInfo()

                Dim myTables1 As Tables = crReportDocument1.Database.Tables

                For Each myTable1 As CrystalDecisions.CrystalReports.Engine.Table In myTables1
                    Dim myTableLogonInfo1 As TableLogOnInfo = myTable1.LogOnInfo
                    myConnectionInfo1.ServerName = ConfigurationManager.AppSettings("ServerName")
                    myConnectionInfo1.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                    myConnectionInfo1.UserID = ConfigurationManager.AppSettings("UserName")
                    myConnectionInfo1.Password = ConfigurationManager.AppSettings("Password")
                    myTable1.ApplyLogOnInfo(myTableLogonInfo1)
                    myTableLogonInfo1.ConnectionInfo = myConnectionInfo1

                Next


                crReportDocument1.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "] and {tblservicerecord1.Status} = 'P'"
                Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF")

                If File.Exists(FilePath1) Then
                    File.Delete(FilePath1)

                End If

                oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

                expo = crReportDocument1.ExportOptions

                expo.ExportDestinationType = ExportDestinationType.DiskFile

                expo.ExportFormatType = ExportFormatType.PortableDocFormat

                expo.DestinationOptions = oDfDopt

                crReportDocument1.Export()
                crReportDocument1.Close()

            ElseIf txtQuery.Text = "InvoiceFormat12" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format12.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat13" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format13.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat14" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format14.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf txtQuery.Text = "InvoiceFormat15" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format1_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))


                crReportDocument1.Load(Server.MapPath("~/Reports/ServiceReportBatchPrinting.rpt"))
                crReportDocument1.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                crReportDocument1.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                crReportDocument1.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                crReportDocument1.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

                crReportDocument1.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                crReportDocument1.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                crReportDocument1.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

                If ConfigurationManager.AppSettings("DomainName").ToString() = "PEST-PRO" Or ConfigurationManager.AppSettings("DomainName").ToString() = "MALAYSIA" Then

                Else
                    crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))

                End If

                Dim myConnectionInfo1 As ConnectionInfo = New ConnectionInfo()

                Dim myTables1 As Tables = crReportDocument1.Database.Tables

                For Each myTable1 As CrystalDecisions.CrystalReports.Engine.Table In myTables1
                    Dim myTableLogonInfo1 As TableLogOnInfo = myTable1.LogOnInfo
                    myConnectionInfo1.ServerName = ConfigurationManager.AppSettings("ServerName")
                    myConnectionInfo1.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                    myConnectionInfo1.UserID = ConfigurationManager.AppSettings("UserName")
                    myConnectionInfo1.Password = ConfigurationManager.AppSettings("Password")
                    myTable1.ApplyLogOnInfo(myTableLogonInfo1)
                    myTableLogonInfo1.ConnectionInfo = myConnectionInfo1

                Next


                crReportDocument1.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "] and {tblservicerecord1.Status} = 'P'"
                Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF")

                If File.Exists(FilePath1) Then
                    File.Delete(FilePath1)

                End If

                oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

                expo = crReportDocument1.ExportOptions

                expo.ExportDestinationType = ExportDestinationType.DiskFile

                expo.ExportFormatType = ExportFormatType.PortableDocFormat

                expo.DestinationOptions = oDfDopt

                crReportDocument1.Export()
                crReportDocument1.Close()
            ElseIf txtQuery.Text = "ServiceReportWithPhotos" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ServiceReportBatchPrinting.rpt"))
                crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                crReportDocument.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                crReportDocument.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

                crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

                If ConfigurationManager.AppSettings("DomainName").ToString() = "PEST-PRO" Or ConfigurationManager.AppSettings("DomainName").ToString() = "MALAYSIA" Then

                Else
                    crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))

                End If

            ElseIf txtQuery.Text = "ServiceReportWithoutPhotos" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ServiceRecordReports/ServiceRecordPrinting.rpt"))


                If ConfigurationManager.AppSettings("DomainName").ToString() = "PEST-PRO" Or ConfigurationManager.AppSettings("DomainName").ToString() = "MALAYSIA" Then

                Else
                    crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                End If


            ElseIf txtQuery.Text = "CreditNote" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ReportPrinting/CreditNote.RPT"))
                If Convert.ToString(Session("PrintType")) = "Print" Then
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("RecordNo")))
                ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                    crReportDocument.SetParameterValue("pRptTitle", "CN/DN")
                End If

            ElseIf txtQuery.Text = "CreditNote2" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ReportPrinting/CreditNote_Format2.RPT"))
                If Convert.ToString(Session("PrintType")) = "Print" Then
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("RecordNo")))
                ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                    crReportDocument.SetParameterValue("pRptTitle", "CN/DN")
                End If

            ElseIf txtQuery.Text = "Receipt" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/ReceiptConfirmation.rpt"))
            ElseIf txtQuery.Text = "CollectionNote" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/CollectionNote.rpt"))

            End If

            'If txtQuery.Text = "FileUpload" Or txtQuery.Text = "ContractFileUpload" Or txtQuery.Text = "AssetFileUpload" Then

            'Else

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

            '  End If

            Dim FilePath As String = ""
            If txtQuery.Text = "ServiceReportTechSign" Then
                crReportDocument.RecordSelectionFormula = "{tblservicerecord1.Recordno} = '" + lblRecordNo.Text + "'"

                '   InsertIntoTblWebEventLog("GenerateAttachment", txtSvcAddr.Text, lblRecordNo.Text)

                txtSvcAddr.Text = txtSvcAddr.Text.Replace(",", " ")
                txtSvcAddr.Text = txtSvcAddr.Text.Replace("/", " ")
                txtSvcAddr.Text = txtSvcAddr.Text.Replace("\", " ")
                '   txtSvcAddr.Text = txtSvcAddr.Text.Replace("\\", " ")
                txtSvcAddr.Text = txtSvcAddr.Text.Replace("\n", " ")

                txtSvcAddr.Text = txtSvcAddr.Text.Replace(":", " ")
                txtSvcAddr.Text = txtSvcAddr.Text.Replace("\r", " ")
                txtSvcAddr.Text = txtSvcAddr.Text.Replace("\t", " ")
                txtSvcAddr.Text = txtSvcAddr.Text.Replace("\v", " ")
                txtSvcAddr.Text = txtSvcAddr.Text.Replace("  ", " ")
                '     InsertIntoTblWebEventLog("GenerateAttachment1", txtSvcAddr.Text, lblRecordNo.Text)

                FilePath = Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF")

                lnkPreview.Text = lblRecordNo.Text + "_" + txtSvcAddr.Text

            ElseIf txtQuery.Text = "SvcSupplement" Then
                FilePath = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF")

            ElseIf txtQuery.Text = "ServiceReport" Then
                FilePath = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_ServiceForm.PDF")

            ElseIf txtQuery.Text = "SanitationReport" Then
                FilePath = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_SanitationForm.PDF")
            ElseIf txtQuery.Text = "ServiceRecordPrinting" Then
                crReportDocument.RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                FilePath = Server.MapPath("~/PDFs/ServiceReports.PDF")
            ElseIf txtQuery.Text = "SOA" Then
                crReportDocument.RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                FilePath = Server.MapPath("~/PDFs/SOA.PDF")
            ElseIf txtQuery.Text = "CustSOA" Then
                crReportDocument.RecordSelectionFormula = "{m02AR22.Balance} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "'"

                FilePath = Server.MapPath("~/PDFs/CustSOA.PDF")

            ElseIf txtQuery.Text = "ReceiptTransactions" Then
                crReportDocument.RecordSelectionFormula = "{Command.AccountId}='" + AccountID + "'"

                FilePath = Server.MapPath("~/PDFs/ReceiptTransactions.PDF")

            ElseIf txtQuery.Text = "TransactionSummary" Then
                crReportDocument.RecordSelectionFormula = "{Command.AccountId}='" + AccountID + "' and {Command.CreatedBy} = '" & Convert.ToString(Session("UserId")) & "'"

                FilePath = Server.MapPath("~/PDFs/TransactionSummary.PDF")

            ElseIf txtQuery.Text = "ContractServiceSchedule" Then
                FilePath = Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat1" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat2" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat3" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat4" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat5" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat6" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat7" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat8" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat9" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat10" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
            ElseIf txtQuery.Text = "InvoiceFormat11" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"
                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")

            ElseIf txtQuery.Text = "InvoiceFormat12" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")

            ElseIf txtQuery.Text = "InvoiceFormat13" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")

            ElseIf txtQuery.Text = "InvoiceFormat14" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")

            ElseIf txtQuery.Text = "InvoiceFormat15" Then
                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"
                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")

            ElseIf txtQuery.Text = "ServiceReportWithPhotos" Then
                txtSvc.Text = RetrieveSvcRecord(Convert.ToString(Session("InvoiceNumber")), Convert.ToString(Session("PrintType")))
                '        InsertIntoTblWebEventLog("Print", txtSvc.Text, "2")

                '   crReportDocument.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "] and {tblservicerecord1.Status} = 'P'"
                crReportDocument.RecordSelectionFormula = "{tblservicerecord1.RecordNo} in [" & txtSvc.Text & "] and {tblservicerecord1.Status} = 'P'"
                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReportPhotos.PDF")

            ElseIf txtQuery.Text = "ServiceReportWithoutPhotos" Then

                '      InsertIntoTblWebEventLog(Convert.ToString(Session("PrintType")), Convert.ToString(Session("InvoiceNumber")), "1")
                txtSvc.Text = RetrieveSvcRecord(Convert.ToString(Session("InvoiceNumber")), Convert.ToString(Session("PrintType")))
                '        InsertIntoTblWebEventLog("Print", txtSvc.Text, "2")

                '   crReportDocument.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "] and {tblservicerecord1.Status} = 'P'"
                crReportDocument.RecordSelectionFormula = "{tblservicerecord1.RecordNo} in [" & txtSvc.Text & "] and {tblservicerecord1.Status} = 'P'"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReport.PDF")


            ElseIf txtQuery.Text = "CreditNote" Then

                If Convert.ToString(Session("PrintType")) = "Print" Then
                    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber}='" & Convert.ToString(Session("RecordNo")) & "'"

                    FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("RecordNo") + ".PDF"))

                ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then

                    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("RecordNo")) & "]"
                    FilePath = Server.MapPath("~/PDFs/CN.PDF")

                End If

            ElseIf txtQuery.Text = "CreditNote2" Then

                If Convert.ToString(Session("PrintType")) = "Print" Then
                    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber}='" & Convert.ToString(Session("RecordNo")) & "'"

                    FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("RecordNo") + ".PDF"))

                ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then

                    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("RecordNo")) & "]"
                    FilePath = Server.MapPath("~/PDFs/CN.PDF")

                End If

            ElseIf txtQuery.Text = "Receipt" Then
                crReportDocument.RecordSelectionFormula = "{m02Recv.ReceiptNumber}='" & Convert.ToString(Session("ReceiptNumber")) & "'"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("ReceiptNumber") + ".PDF"))

            ElseIf txtQuery.Text = "CollectionNote" Then
                crReportDocument.RecordSelectionFormula = "{m02Recv.ReceiptNumber}='" & Convert.ToString(Session("ReceiptNumber")) & "'"

                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("ReceiptNumber") + ".PDF"))

            ElseIf txtQuery.Text = "FileUpload" Then
                '  crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"
                '  FilePath = Convert.ToString(Session("FilePath"))
                FilePath = Server.MapPath("~/Uploads/Service/" + Convert.ToString(Session("FileName")))
            ElseIf txtQuery.Text = "ContractFileUpload" Then
                '  crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                FilePath = Server.MapPath("~/Uploads/Contract/" + Convert.ToString(Session("FileName")))
            ElseIf txtQuery.Text = "AssetFileUpload" Then
                '  crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"
                FilePath = Convert.ToString(Session("FilePath"))
                '  FilePath = Server.MapPath("~/Uploads/Service/" + Convert.ToString(Session("FileName")))
            End If


            If File.Exists(FilePath) Then
                File.Delete(FilePath)

            End If

            oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

            expo = crReportDocument.ExportOptions

            expo.ExportDestinationType = ExportDestinationType.DiskFile

            expo.ExportFormatType = ExportFormatType.PortableDocFormat

            expo.DestinationOptions = oDfDopt

            crReportDocument.Export()

            If txtQuery.Text = "ServiceReportTechSign" Then

                Dim pdfFile As String = Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF")
                Dim reader As New PdfReader(pdfFile)

                Dim Filename As String = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_ServiceReport.PDF")

                If System.IO.File.Exists(Filename) Then
                    System.IO.File.Delete(Filename)
                End If
                Dim stamper As New PdfStamper(reader, New FileStream(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_ServiceReport.PDF"), FileMode.Create), PdfWriter.VERSION_1_5)
                stamper.FormFlattening = True
                stamper.SetFullCompression()
                stamper.Close()
            ElseIf txtQuery.Text = "InvoiceFormat11" Then

                If txtFileType.Text = "Individual" Then

                Else
                    Dim FilePath2 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")

                    Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF")

                    MergePDF(FilePath2, FilePath1)

                End If
            ElseIf txtQuery.Text = "InvoiceFormat15" Then

                If txtFileType.Text = "Individual" Then

                Else
                    Dim FilePath2 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")

                    Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF")

                    MergePDF(FilePath2, FilePath1)

                End If
            End If

            If AdditionalFile = "Y" Then
                InsertIntoTblWebEventLog("GenerateAttachment", Period, lblRecordNo.Text)

                Dim dt As New DateTime(Period.Substring(0, 4), Period.Substring(4, 2), 1)

                Dim crReportDocument1 As New ReportDocument()
                crReportDocument1.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\ARReports\Price Increase.rpt")
                InsertIntoTblWebEventLog("GenerateAttachment", InvDate, lblRecordNo.Text)

                Dim d As DateTime
                If Date.TryParseExact(InvDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                End If

                InsertIntoTblWebEventLog("GenerateAttachment", d.ToShortDateString, lblRecordNo.Text)


                crReportDocument1.RecordSelectionFormula = "{tblsalesdetail1.InvoiceNumber} = '" & lblRecordNo.Text & "' and {tblcontractpricehistory1.Type} = 'Price Increase' and {tblcontractpricehistory1.Date} <=" + "#" + d.ToString("MM-dd-yyyy") + "# and {tblcontractpricehistory1.Date} >=" + "#" + dt.ToString("MM-dd-yyyy") + "# AND {tblcontractpricehistory1.ContractGroup} = 'CP'"

                '   crReportDocument1.RecordSelectionFormula = "{tblsalesdetail1.InvoiceNumber} = '" & lblRecordNo.Text & "' and {tblcontractpricehistory1.Type} = 'Price Increase' and {tblcontractpricehistory1.Date} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                Dim FilePath1 As String = ""
                If (Not System.IO.Directory.Exists(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "/"))) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "/"))
                End If
                FilePath1 = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "/Price Increase Notification.PDF")
                '   InsertIntoTblWebEventLog("GenerateAttachment2", FilePath1, "str")
                '   FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")

                If File.Exists(FilePath1) Then
                    File.Delete(FilePath1)

                End If

                oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

                expo = crReportDocument1.ExportOptions

                expo.ExportDestinationType = ExportDestinationType.DiskFile

                expo.ExportFormatType = ExportFormatType.PortableDocFormat

                expo.DestinationOptions = oDfDopt
                '  InsertIntoTblWebEventLog("GenerateAttachment3", FilePath1, "str")

                crReportDocument1.Export()
                crReportDocument1.Close()
                ' InsertIntoTblWebEventLog("GenerateAttachment4", FilePath1, "str")

            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("GenerateAttachment", ex.Message.ToString, lblRecordNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Sub RetrieveEmailInfo()
        txtSubject.Text = "ANTICIMEX SERVICE REPORTS"
        txtContent.Text = "Attached are the Service Reports for the pest control services performed at your premises."
    End Sub

    Private Sub RetrieveSOAInfo()
        txtSubject.Text = "ANTICIMEX STATEMENT OF ACCOUNTS"
        txtContent.Text = "Please find the Statement of Accounts attached."

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text

        command.Connection = conn

        command.CommandText = "Select * from tblEmailSetUp where SetUpID='SOA'"
        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        Dim subject As String = ""
        Dim content As String = ""

        If dt.Rows.Count > 0 Then
            subject = dt.Rows(0)("Subject").ToString
            content = dt.Rows(0)("Contents").ToString

        End If


        content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
        content = content.Replace("CLIENTNAME", Convert.ToString(Session("ClientName")))
        content = content.Replace("CUTOFFDATE", Convert.ToString(Session("CutOffDate")))
        ' content = content.Replace("COMPANY NAME", "ANTICIMEX PEST MANAGEMENT PTE. LTD.")
        content = content.Replace("COMPANYNAME", ConfigurationManager.AppSettings("CompanyName").ToString())

        Dim AccountID As String = Convert.ToString(Session("AccountID"))
        Dim AccountName As String = Convert.ToString(Session("CustName"))

        txtSubject.Text = subject & " : " & AccountID & " - " & AccountName
        txtContent.Text = content

        dt.Clear()
        dr.Close()
        conn.Close()
        conn.Dispose()
    End Sub

    Private Sub RetrieveSvcInfo()
        Try
            Dim CustAddress As String = ""
            Dim Address As String = ""
            Dim CustName As String = ""
            Dim ServiceDate As String = ""
            Dim ServiceBy As String = ""
            Dim RemarksClient As String = ""
            Dim RemarksClientSub As String = ""
            Dim ScheduleType As String = ""
            Dim Actions As String = ""
            Dim Materials As String = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            If txtQuery.Text = "FileUpload" Then
                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.Connection = conn

                command2.CommandText = "Select * from tblfileupload where FileNameLink=@filename"
                command2.Parameters.AddWithValue("@filename", Convert.ToString(Session("FileName")))
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count = 1 Then

                    lblRecordNo.Text = dt2.Rows(0)("FileRef").ToString

                End If
                dt2.Clear()
                dt2.Dispose()
                dr2.Close()
                command2.Dispose()
            End If

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT *,REPLACE(Address1,'\t','') as SvcAddr FROM tblservicerecord where recordno='" & lblRecordNo.Text & "' and Status <> 'V'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                Dim ToEmailID As String = ""
                Dim CCEmailID As String = ""

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

                If dt1.Rows(0)("EmailSent").ToString() = "1" Then
                    EmailSent = "Yes"
                Else
                    EmailSent = "No"
                End If

                'Testing 
                '  txtTo.Text = "sasi.vishwa@gmail.com"
                '   txtTo.Text = "Christian.Reyes@anticimex.com.sg;LiuChenhong7.11@gmail.com;thina.geran@anticimex.com.sg;"

                CustName = dt1.Rows(0)("CustName").ToString
                ServiceBy = dt1.Rows(0)("ServiceBy").ToString
                ServiceDate = Convert.ToDateTime(dt1.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
                ScheduleType = dt1.Rows(0)("ScheduleType").ToString

                'If Not String.IsNullOrEmpty(dt1.Rows(0)("Address1").ToString()) Then
                '    Address += dt1.Rows(0)("Address1").ToString() + ", "
                '    txtSvcAddr.Text = dt1.Rows(0)("Address1").ToString()
                'End If

                If Not String.IsNullOrEmpty(dt1.Rows(0)("SvcAddr").ToString()) Then
                    Address += dt1.Rows(0)("SvcAddr").ToString() + ", "
                    txtSvcAddr.Text = dt1.Rows(0)("SvcAddr").ToString()
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

                txtBranchLocation.Text = dt1.Rows(0)("location").ToString()

            End If


            dt1.Clear()
            dr1.Close()


            ' Retrieve Remarks for Client from tblservicerecorddet

            Dim commandR As MySqlCommand = New MySqlCommand

            commandR.CommandType = CommandType.Text

            commandR.Connection = conn

            commandR.CommandText = "Select RemarkClient,Action,Material from tblServiceRecordDet where RecordNo='" + lblRecordNo.Text + "'"
            commandR.Connection = conn

            Dim drR As MySqlDataReader = commandR.ExecuteReader()
            Dim dtR As New DataTable
            dtR.Load(drR)

            If dtR.Rows.Count > 0 Then
                For i As Int16 = 0 To dtR.Rows.Count - 1

                    If dtR.Rows(i)("RemarkClient").ToString <> DBNull.Value.ToString Then
                        If String.IsNullOrEmpty(dtR.Rows(i)("RemarkClient")) = False Then
                            If dtR.Rows(i)("RemarkClient").ToString.Trim + " <br/>" = RemarksClient.Trim Then

                            Else
                                RemarksClient += dtR.Rows(i)("RemarkClient").ToString.Trim + " <br/>"
                            End If

                            If RemarksClientSub = "" Then
                                RemarksClientSub = dtR.Rows(i)("RemarkClient").ToString.Trim
                            Else
                                RemarksClientSub = RemarksClientSub + ", " + dtR.Rows(i)("RemarkClient").ToString.Trim
                            End If

                        End If
                    End If

                    If dtR.Rows(i)("Action").ToString <> DBNull.Value.ToString Then
                        If String.IsNullOrEmpty(dtR.Rows(i)("Action")) = False Then
                            InsertIntoTblWebEventLog("RetrieveSvcInfo", dtR.Rows(i)("Action").ToString.Trim, Actions.Trim)

                            If dtR.Rows(i)("Action").ToString.Trim + " <br/>      " = Actions.Trim Then

                            Else
                                Actions += dtR.Rows(i)("Action").ToString.Trim + " <br/>      "
                            End If


                            'If RemarksClientSub = "" Then
                            '    RemarksClientSub = dtR.Rows(i)("RemarkClient").ToString.Trim
                            'Else
                            '    RemarksClientSub = RemarksClientSub + ", " + dtR.Rows(i)("RemarkClient").ToString.Trim
                            'End If
                            InsertIntoTblWebEventLog("RetrieveSvcInfo", dtR.Rows(i)("Action").ToString.Trim, Actions.Trim)

                        End If
                    End If
                    InsertIntoTblWebEventLog("RetrieveSvcInfo", "Test", lblRecordNo.Text)

                    If dtR.Rows(i)("Material").ToString <> DBNull.Value.ToString Then
                        If String.IsNullOrEmpty(dtR.Rows(i)("Material")) = False Then
                            If dtR.Rows(i)("Material").ToString.Trim + " <br/>" = Materials.Trim Then

                            Else
                                Materials += dtR.Rows(i)("Material").ToString.Trim + " <br/>"
                            End If

                            'If RemarksClientSub = "" Then
                            '    RemarksClientSub = dtR.Rows(i)("RemarkClient").ToString.Trim
                            'Else
                            '    RemarksClientSub = RemarksClientSub + ", " + dtR.Rows(i)("RemarkClient").ToString.Trim
                            'End If

                        End If
                    End If
                Next
            End If
            InsertIntoTblWebEventLog("RetrieveSvcInfo", "Test2", lblRecordNo.Text)

            ' RemarksClientSub = RemarksClientSub.TrimEnd(",")


            'If RemarksClientSub.TrimEnd( Then
            '     RemarksClientSub = RemarksClientSub.Trim.Remove(RemarksClientSub.Length - 1)

            'End If

            dtR.Clear()

            drR.Close()
            commandR.Dispose()

            ' Retrieve Specific Location Name from tblservicerecord2
            Dim LocationName As String = ""
            Dim commandL As MySqlCommand = New MySqlCommand

            commandL.CommandType = CommandType.Text

            commandL.Connection = conn

            commandL.CommandText = "Select SpecificLocationName,ReportServiceStart from tblServiceRecord2 where RecordNo='" + lblRecordNo.Text + "'"
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

                If dtL.Rows(0)("ReportServiceStart").ToString <> DBNull.Value.ToString Then
                    If String.IsNullOrEmpty(dtL.Rows(0)("ReportServiceStart")) = False Then
                        ServiceDate = dtL.Rows(0)("ReportServiceStart").ToString.Trim

                    End If
                End If
            End If


            dtL.Clear()

            drL.Close()
            commandL.Dispose()
            InsertIntoTblWebEventLog("RetrieveSvcInfo", "Test3", lblRecordNo.Text)

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn

            command.CommandText = "Select * from tblEmailSetUp where SetUpID='SERV-REC-2'"
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
            'content = content.Replace("COMPANY NAME", "ANTICIMEX PEST MANAGEMENT PTE. LTD.")
            content = content.Replace("COMPANY NAME", ConfigurationManager.AppSettings("CompanyName").ToString())


            content = content.Replace("SERVICE RECORD NUMBER", lblRecordNo.Text)
            content = content.Replace("SERVICE BY", ServiceBy)

            If RemarksClient = "" Or String.IsNullOrEmpty(RemarksClient) Then
                content = content.Replace("REMARKSCLIENT", RemarksClient)
            Else
                content = content.Replace("REMARKSCLIENT - ", "")
            End If


            content = content.Replace("STAFF ID", Convert.ToString(Session("UserID")))
            content = content.Replace("EMAIL SENT DATE", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", New System.Globalization.CultureInfo("en-GB")))

            If LocationName = "" Or String.IsNullOrEmpty(LocationName) Then
                content = content.Replace("LOCATION", "LOCATION : " + LocationName)
            Else
                content = content.Replace("LOCATION - ", "")
            End If

            content = content.Replace("ACTION", Actions)
            content = content.Replace("MATERIAL", Materials)

            subject = System.Text.RegularExpressions.Regex.Replace(subject, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            subject = subject.Replace("CLIENT NAME", CustName)
            subject = subject.Replace("SERVICE DATE", ServiceDate)
            subject = subject.Replace("SERVICE RECORD NUMBER", lblRecordNo.Text)
            subject = subject.Replace("CUSTOMER ADDRESS", CustAddress)
            '  subject = subject.Replace("REMARKS CLIENT", RemarksClientSub)
            subject = subject.Replace("SCHEDULE TYPE", ScheduleType)
            '  subject = subject.Replace("LOCATION", LocationName)
            If LocationName <> "" Or String.IsNullOrEmpty(LocationName) = False Then
                subject = subject.Replace("LOCATION", "LOCATION : " + LocationName)
            Else
                subject = subject.Replace("LOCATION - ", "")
            End If

            If RemarksClient <> "" Or String.IsNullOrEmpty(RemarksClient) = False Then
                subject = subject.Replace("REMARKS CLIENT", RemarksClient)
            Else
                subject = subject.Replace("REMARKS CLIENT -", "")
            End If

            If txtQuery.Text = "FileUpload" Then

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.Connection = conn

                command2.CommandText = "Select * from tblfileupload where FileNameLink=@filename"
                command2.Parameters.AddWithValue("@filename", Convert.ToString(Session("FileName")))
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count = 1 Then
                    If dt2.Rows(0)("ManualReport").ToString = "Y" Then
                    Else
                        content = ""
                    End If
                End If
                dt2.Clear()
                dr2.Close()
                command2.Dispose()

            End If



            If EmailSent = "Yes" Then
                txtSubject.Text = "(RESEND) " + subject
            Else
                txtSubject.Text = subject
            End If

            ' txtSubject.Text = subject
            txtContent.Text = content

            dt.Clear()
            dr.Close()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveSvcInfo", ex.Message.ToString, lblRecordNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub


    Private Sub RetrieveContractInfo()
        Try
            Dim EmailAddress As String = ""
            Dim CustName As String = ""
            Dim ContractDate As String = ""
            Dim YrStrList As List(Of [String]) = New List(Of String)()


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT contractno,custname,ContractDate,Contact,accountid,contacttype,location FROM tblcontract where contractno='" & lblRecordNo.Text & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then


                'Testing 
                '  txtTo.Text = "sasi.vishwa@gmail.com"
                '   txtTo.Text = "Christian.Reyes@anticimex.com.sg;LiuChenhong7.11@gmail.com;thina.geran@anticimex.com.sg;"

                CustName = dt1.Rows(0)("CustName").ToString
                ContractDate = Convert.ToDateTime(dt1.Rows(0)("ContractDate")).ToString("dd/MM/yyyy")

                txtBranchLocation.Text = dt1.Rows(0)("location").ToString()

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT distinct locationid FROM tblcontractdet where contractno='" & lblRecordNo.Text & "'"


                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    For i As Int16 = 0 To dt2.Rows.Count - 1
                        Dim commandLoc As MySqlCommand = New MySqlCommand

                        commandLoc.CommandType = CommandType.Text

                        If dt1.Rows(0)("ContactType").ToString = "COMPANY" Then
                            commandLoc.CommandText = "SELECT email,contact2email FROM tblcompanylocation where locationid='" & dt2.Rows(i)("LocationID").ToString & "'"
                        ElseIf dt1.Rows(0)("ContactType").ToString = "PERSON" Then
                            commandLoc.CommandText = "SELECT email,contact2email FROM tblPERSONlocation where locationid='" & dt2.Rows(i)("LocationID").ToString & "'"

                        End If


                        commandLoc.Connection = conn

                        Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
                        Dim dtLoc As New DataTable
                        dtLoc.Load(drLoc)

                        If dtLoc.Rows.Count > 0 Then
                            If String.IsNullOrEmpty(dtLoc.Rows(0)("Email").ToString) = False Then
                                YrStrList.Add(dtLoc.Rows(0)("Email").ToString)
                            End If
                            If String.IsNullOrEmpty(dtLoc.Rows(0)("Contact2Email").ToString) = False Then
                                YrStrList.Add(dtLoc.Rows(0)("Contact2Email").ToString)
                            End If
                        End If
                        dtLoc.Clear()
                        drLoc.Close()

                    Next


                    Dim YrStr As [String] = [String].Join(";", YrStrList.ToArray())
                    If String.IsNullOrEmpty(YrStr) = False Then
                        txtTo.Text = YrStr
                    End If

                End If
                dt2.Clear()
                dr2.Close()
            End If


            dt1.Clear()
            dr1.Close()


            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn

            command.CommandText = "Select * from tblEmailSetUp where SetUpID='SERVCONT-1'"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            Dim subject As String = ""
            Dim content As String = ""

            If dt.Rows.Count > 0 Then
                subject = dt.Rows(0)("Subject").ToString
                content = dt.Rows(0)("Contents").ToString

            End If


            content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            content = content.Replace("CLIENT NAME", CustName)
            content = content.Replace("CONTRACT DATE", ContractDate)
            ' content = content.Replace("COMPANY NAME", "ANTICIMEX PEST MANAGEMENT PTE. LTD.")
            content = content.Replace("COMPANY NAME", ConfigurationManager.AppSettings("CompanyName").ToString())

            content = content.Replace("STAFF ID", Convert.ToString(Session("UserID")))
            content = content.Replace("EMAIL SENT DATE", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            subject = System.Text.RegularExpressions.Regex.Replace(subject, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            subject = subject.Replace("CLIENT NAME", CustName)
            subject = subject.Replace("CONTRACT DATE", ContractDate)
            subject = subject.Replace("CONTRACT NUMBER", lblRecordNo.Text)

            txtSubject.Text = subject
            txtContent.Text = content

            dt.Clear()
            dr.Close()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveContractInfo", ex.Message.ToString, lblRecordNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Function RetrieveSvcRecord(InvoiceNumber As String, PrintType As String) As String
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        If PrintType = "Print" Then
            command2.CommandText = "SELECT RefType FROM tblsalesdetail where InvoiceNumber='" + InvoiceNumber.Trim("""") + "' order by locationid,costcode,servicedate,RefType"
        ElseIf PrintType = "MultiPrint" Then
            command2.CommandText = "SELECT RefType FROM tblsalesdetail where InvoiceNumber in (" + InvoiceNumber + ") order by locationid,costcode,servicedate,RefType"
        End If

        command2.Connection = conn

        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
        Dim dt2 As New DataTable
        dt2.Load(dr2)

        Dim SvcRecNo As String = ""

        If dt2.Rows.Count > 0 Then
            For i As Int16 = 0 To dt2.Rows.Count - 1
                If SvcRecNo = "" Then
                    SvcRecNo = """" + dt2.Rows(i)("RefType").ToString + """"
                Else
                    SvcRecNo = SvcRecNo + ",""" + dt2.Rows(i)("RefType").ToString + """"
                End If
            Next
        End If

        command2.Dispose()
        dt2.Clear()
        dt2.Dispose()
        dr2.Close()
        conn.Close()

        '    txtSvc.Text = SvcRecNo
        Return SvcRecNo

    End Function

    Private Sub RetrieveReceiptInfo()
        Try
            Dim CustName As String = ""
            Dim ReceiptDate As String = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT ReceiptNumber,ReceiptDate,ReceiptFrom,ContactType FROM tblrecv where receiptnumber='" & lblRecordNo.Text & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                CustName = dt1.Rows(0)("ReceiptFrom").ToString
                If dt1.Rows(0)("ReceiptDate").ToString = DBNull.Value.ToString Then
                Else
                    ReceiptDate = Convert.ToDateTime(dt1.Rows(0)("ReceiptDate")).ToString("dd/MM/yyyy")
                End If

                RetrieveReceiptEmailInfo(conn, dt1.Rows(0)("ReceiptNumber").ToString, dt1.Rows(0)("ContactType").ToString)

            End If

            dt1.Clear()
            dt1.Dispose()
            dr1.Close()
            command1.Dispose()


            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn

            command.CommandText = "Select * from tblEmailSetUp where SetUpID='RCPT'"

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            Dim subject As String = ""
            Dim content As String = ""

            If dt.Rows.Count > 0 Then
                subject = dt.Rows(0)("Subject").ToString
                content = dt.Rows(0)("Contents").ToString

            End If


            content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")

            'content = content.Replace("COMPANY NAME", "ANTICIMEX PEST MANAGEMENT PTE. LTD.")
            content = content.Replace("COMPANY NAME", ConfigurationManager.AppSettings("CompanyName").ToString())
            content = content.Replace("CLIENT NAME", CustName)
            content = content.Replace("RECEIPT NUMBER", lblRecordNo.Text)

            content = content.Replace("STAFF ID", Convert.ToString(Session("UserID")))
            content = content.Replace("EMAIL SENT DATE", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", New System.Globalization.CultureInfo("en-GB")))

            subject = System.Text.RegularExpressions.Regex.Replace(subject, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")

            subject = subject.Replace("RECEIPT NUMBER", lblRecordNo.Text)
            '   subject = subject.Replace("INVOICE NUMBER", CustAddress)

            txtSubject.Text = subject
            txtContent.Text = content

            dt.Clear()
            dr.Close()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveReceiptInfo", ex.Message.ToString, lblRecordNo.Text)


        End Try
    End Sub

    Private Sub RetrieveReceiptEmailInfo(conn As MySqlConnection, receiptno As String, ContactType As String)
        Try
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT group_concat(distinct RefType separator ''',''') as ConcatInvoiceNo from tblrecvdet where ReceiptNumber ='" & receiptno & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                Dim commandSalesDet As MySqlCommand = New MySqlCommand

                commandSalesDet.CommandType = CommandType.Text

                commandSalesDet.CommandText = "SELECT group_concat(distinct locationid separator ''',''') as ConcatLocationID from tblsalesdetail where invoicenumber in ('" & dt1.Rows(0)("ConcatInvoiceNo") & "')"

                commandSalesDet.Connection = conn

                Dim drSalesDet As MySqlDataReader = commandSalesDet.ExecuteReader()
                Dim dtSalesDet As New DataTable
                dtSalesDet.Load(drSalesDet)

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'InsertIntoTblWebEventLog("GetInvoiceNumber - 1", dtSalesDet.Rows(0)("ConcatLocationID").ToString, InvoiceNumber)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                If dtSalesDet.Rows.Count > 0 Then

                    'Retrieve EmailID from the Service Locations 

                    Dim commandLoc As MySqlCommand = New MySqlCommand

                    commandLoc.CommandType = CommandType.Text

                    '  commandLoc.CommandText = "SELECT group_concat(distinct locationid separator ''',''') as ConcatLocationID,DefaultInvoiceFormat from tblLocail where invoicenumber='" & InvoiceNumber & "'"""
                    If ContactType = "COMPANY" Then
                        commandLoc.CommandText = "SELECT group_concat(billemail1svc separator ';') as ConcatBillEmail1,group_concat(billemail2svc separator ';') as ConcatBillEmail2 from tblcompanylocation where LocationID in ('" & dtSalesDet.Rows(0)("ConcatLocationID").ToString & "')"

                    ElseIf ContactType = "PERSON" Then
                        commandLoc.CommandText = "SELECT group_concat(billemail1svc separator ';') as ConcatBillEmail1,group_concat(billemail2svc separator ';') as ConcatBillEmail2 from tblpersonlocation where LocationID in ('" & dtSalesDet.Rows(0)("ConcatLocationID").ToString & "')"

                    End If
                    commandLoc.Connection = conn

                    Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
                    Dim dtLoc As New DataTable
                    dtLoc.Load(drLoc)



                    If dtLoc.Rows.Count > 0 Then
                        'If dtLoc.Rows(0)("ConcatFormat").ToString.Length > 8 Then
                        'Else
                        '    InvoiceFormat = dtLoc.Rows(0)("DefaultInvoiceFormat").ToString
                        'End If

                        If dtLoc.Rows(0)("ConcatBillEmail1").ToString <> "" Or String.IsNullOrEmpty(dtLoc.Rows(0)("ConcatBillEmail1").ToString) = False Then
                            txtTo.Text = dtLoc.Rows(0)("ConcatBillEmail1").ToString + ";" + dtLoc.Rows(0)("ConcatBillEmail2").ToString
                        Else
                            txtTo.Text = dtLoc.Rows(0)("ConcatBillEmail2").ToString
                        End If

                    End If

                    dtLoc.Clear()
                    dtLoc.Dispose()
                    drLoc.Close()
                    commandLoc.Dispose()

                End If
            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveReceiptEmailInfo", ex.Message.ToString, lblRecordNo.Text)

        End Try
    End Sub

    Private Sub RetrieveCNEmailInfo(conn As MySqlConnection, invoiceno As String, ContactType As String)
        Try
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT group_concat(distinct SourceInvoice separator ''',''') as ConcatInvoiceNo from tblSALESDETAIL where InvoiceNumber ='" & invoiceno & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                Dim commandSalesDet As MySqlCommand = New MySqlCommand

                commandSalesDet.CommandType = CommandType.Text

                commandSalesDet.CommandText = "SELECT group_concat(distinct locationid separator ''',''') as ConcatLocationID from tblsalesdetail where invoicenumber in ('" & dt1.Rows(0)("ConcatInvoiceNo") & "')"

                commandSalesDet.Connection = conn

                Dim drSalesDet As MySqlDataReader = commandSalesDet.ExecuteReader()
                Dim dtSalesDet As New DataTable
                dtSalesDet.Load(drSalesDet)

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'InsertIntoTblWebEventLog("GetInvoiceNumber - 1", dtSalesDet.Rows(0)("ConcatLocationID").ToString, InvoiceNumber)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                If dtSalesDet.Rows.Count > 0 Then

                    'Retrieve EmailID from the Service Locations 

                    Dim commandLoc As MySqlCommand = New MySqlCommand

                    commandLoc.CommandType = CommandType.Text

                    '  commandLoc.CommandText = "SELECT group_concat(distinct locationid separator ''',''') as ConcatLocationID,DefaultInvoiceFormat from tblLocail where invoicenumber='" & InvoiceNumber & "'"""
                    If ContactType = "COMPANY" Then
                        commandLoc.CommandText = "SELECT group_concat(billemail1svc separator ';') as ConcatBillEmail1,group_concat(billemail2svc separator ';') as ConcatBillEmail2 from tblcompanylocation where LocationID in ('" & dtSalesDet.Rows(0)("ConcatLocationID").ToString & "')"

                    ElseIf ContactType = "PERSON" Then
                        commandLoc.CommandText = "SELECT group_concat(billemail1svc separator ';') as ConcatBillEmail1,group_concat(billemail2svc separator ';') as ConcatBillEmail2 from tblpersonlocation where LocationID in ('" & dtSalesDet.Rows(0)("ConcatLocationID").ToString & "')"

                    End If
                    commandLoc.Connection = conn

                    Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
                    Dim dtLoc As New DataTable
                    dtLoc.Load(drLoc)



                    If dtLoc.Rows.Count > 0 Then
                        'If dtLoc.Rows(0)("ConcatFormat").ToString.Length > 8 Then
                        'Else
                        '    InvoiceFormat = dtLoc.Rows(0)("DefaultInvoiceFormat").ToString
                        'End If

                        If dtLoc.Rows(0)("ConcatBillEmail1").ToString <> "" Or String.IsNullOrEmpty(dtLoc.Rows(0)("ConcatBillEmail1").ToString) = False Then
                            txtTo.Text = dtLoc.Rows(0)("ConcatBillEmail1").ToString + ";" + dtLoc.Rows(0)("ConcatBillEmail2").ToString
                        Else
                            txtTo.Text = dtLoc.Rows(0)("ConcatBillEmail2").ToString
                        End If

                    End If

                    dtLoc.Clear()
                    dtLoc.Dispose()
                    drLoc.Close()
                    commandLoc.Dispose()

                End If
            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveReceiptEmailInfo", ex.Message.ToString, lblRecordNo.Text)

        End Try
    End Sub

    Private Sub RetrieveInvoiceInfo()
        Try
            Dim EmailAddress As String = ""
            Dim CustName As String = ""
            Dim SalesDate As String = ""
            Dim YrStrList As List(Of [String]) = New List(Of String)()
            Dim DueDate As String = ""
            Dim StaffEmailID As String = ""
            Dim AccountID As String = ""
            Dim ContractNo As String = ""
            AdditionalFile = "N"

            Dim ContactType As String = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT invoicenumber,custname,SalesDate,accountid,contacttype,DUEDATE,location,createdby FROM tblsales where invoicenumber='" & lblRecordNo.Text & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                CustName = dt1.Rows(0)("CustName").ToString
                If dt1.Rows(0)("SalesDate").ToString = DBNull.Value.ToString Then
                Else
                    SalesDate = Convert.ToDateTime(dt1.Rows(0)("SalesDate")).ToString("dd/MM/yyyy")
                    InvDate = dt1.Rows(0)("SalesDate")
                End If

                If dt1.Rows(0)("DueDate").ToString = DBNull.Value.ToString Then
                Else
                    DueDate = Convert.ToDateTime(dt1.Rows(0)("DueDate")).ToString("dd/MM/yyyy")
                End If

                txtBranchLocation.Text = dt1.Rows(0)("location").ToString()
                ContactType = dt1.Rows(0)("ContactType").ToString



                For i As Int32 = 0 To dt1.Rows.Count - 1

                    ''Start :Copied

                    Dim commandSalesDet As MySqlCommand = New MySqlCommand

                    commandSalesDet.CommandType = CommandType.Text

                    commandSalesDet.CommandText = "SELECT group_concat(distinct locationid separator ''',''') as ConcatLocationID from tblsalesdetail where invoicenumber='" & lblRecordNo.Text & "'"

                    commandSalesDet.Connection = conn

                    Dim drSalesDet As MySqlDataReader = commandSalesDet.ExecuteReader()
                    Dim dtSalesDet As New DataTable
                    dtSalesDet.Load(drSalesDet)

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'InsertIntoTblWebEventLog("GetInvoiceNumber - 1", dtSalesDet.Rows(0)("ConcatLocationID").ToString, InvoiceNumber)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    If dtSalesDet.Rows.Count > 0 Then

                        'Retrieve EmailID from the Service Locations 

                        Dim commandLoc As MySqlCommand = New MySqlCommand

                        commandLoc.CommandType = CommandType.Text

                        '  commandLoc.CommandText = "SELECT group_concat(distinct locationid separator ''',''') as ConcatLocationID,DefaultInvoiceFormat from tblLocail where invoicenumber='" & InvoiceNumber & "'"""
                        If dt1.Rows(i)("ContactType").ToString = "COMPANY" Then
                            commandLoc.CommandText = "SELECT group_concat(billemail1svc separator ';') as ConcatBillEmail1,group_concat(billemail2svc separator ';') as ConcatBillEmail2 from tblcompanylocation where LocationID in ('" & dtSalesDet.Rows(0)("ConcatLocationID").ToString & "')"

                        ElseIf dt1.Rows(i)("ContactType").ToString = "PERSON" Then
                            commandLoc.CommandText = "SELECT group_concat(billemail1svc separator ';') as ConcatBillEmail1,group_concat(billemail2svc separator ';') as ConcatBillEmail2 from tblpersonlocation where LocationID in ('" & dtSalesDet.Rows(0)("ConcatLocationID").ToString & "')"

                        End If
                        commandLoc.Connection = conn

                        Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
                        Dim dtLoc As New DataTable
                        dtLoc.Load(drLoc)



                        If dtLoc.Rows.Count > 0 Then
                            'If dtLoc.Rows(0)("ConcatFormat").ToString.Length > 8 Then
                            'Else
                            '    InvoiceFormat = dtLoc.Rows(0)("DefaultInvoiceFormat").ToString
                            'End If

                            If dtLoc.Rows(0)("ConcatBillEmail1").ToString <> "" Or String.IsNullOrEmpty(dtLoc.Rows(0)("ConcatBillEmail1").ToString) = False Then
                                txtTo.Text = dtLoc.Rows(0)("ConcatBillEmail1").ToString + ";" + dtLoc.Rows(0)("ConcatBillEmail2").ToString
                            Else
                                txtTo.Text = dtLoc.Rows(0)("ConcatBillEmail2").ToString
                            End If

                        End If

                        dtLoc.Clear()
                        dtLoc.Dispose()
                        drLoc.Close()
                        commandLoc.Dispose()


                        'Retrieve ContractNo
                        Dim commandContractNo As MySqlCommand = New MySqlCommand

                        commandContractNo.CommandType = CommandType.Text

                        commandContractNo.CommandText = "SELECT group_concat(distinct costcode separator ''',''') as ConcatContract from tblsalesdetail where invoicenumber='" & lblRecordNo.Text & "'"

                        commandContractNo.Connection = conn

                        Dim drContractNo As MySqlDataReader = commandContractNo.ExecuteReader()
                        Dim dtContractNo As New DataTable
                        dtContractNo.Load(drContractNo)

                        If dtContractNo.Rows.Count > 0 Then
                            ContractNo = dtContractNo.Rows(0)("ConcatContract")
                        End If

                        InsertIntoTblWebEventLog("GetInvoiceNumberC", ContractNo, lblRecordNo.Text)
                        InsertIntoTblWebEventLog("GetInvoiceNumberI", SalesDate, lblRecordNo.Text)

                        'Additional Attachment - Price Increase

                        Dim AddFile As String = "N"
                        '    Dim Period As String = ""

                        Dim commandPeriod As MySqlCommand = New MySqlCommand

                        commandPeriod.CommandType = CommandType.Text

                        commandPeriod.CommandText = "SELECT CalendarPeriod,AdditionalFile FROM tblperiod where ARLock='Y' and ARLockE='Y' and AutoEmail='Y' ORDER BY CalendarPeriod desc;"

                        commandPeriod.Connection = conn

                        Dim drPeriod As MySqlDataReader = commandPeriod.ExecuteReader()
                        Dim dtPeriod As New DataTable
                        dtPeriod.Load(drPeriod)

                        If dtPeriod.Rows.Count > 0 Then
                            Period = dtPeriod.Rows(0)("CalendarPeriod").ToString
                            AddFile = dtPeriod.Rows(0)("AdditionalFile").ToString
                        End If

                        If AddFile = "Y" Then

                            Dim PriceIncrease As Boolean = False

                            PriceIncrease = CheckPriceIncrease(conn, lblRecordNo.Text, dt1.Rows(i)("AccountID").ToString, ContractNo, SalesDate, Period)

                            If PriceIncrease = True Then
                                AdditionalFile = "Y"
                            Else
                                AdditionalFile = "N"
                            End If
                        End If

                        '   InsertIntoTblWebEventLog("GetInvoiceNumberP", PriceIncrease.ToString, InvoiceNumber)
                        InsertIntoTblWebEventLog("GetInvoiceNumberP", AdditionalFile, lblRecordNo.Text)
                    End If
                    'End : Copied



                Next

                If txtQuery.Text = "CreditNote" Then
                    If String.IsNullOrEmpty(txtTo.Text) Then
                        RetrieveCNEmailInfo(conn, lblRecordNo.Text, ContactType)
                    End If
                End If

                'Find EmailID of the staff who created the invoice

                StaffEmailID = RetrieveStaffEmailID(conn, dt1.Rows(0)("CreatedBy").ToString)

                If StaffEmailID = "" Then
                    'If the staff who created the invoice is not active, find email id of the staff who is sending the invoice.

                    StaffEmailID = RetrieveStaffEmailID(conn, Session("UserID"))
                End If

                If StaffEmailID = "" Then
                    'If EmailPerson is null in tblstaff, retrieve the EmailID from tblCompanyInfo or from Branch

                    Dim commandEm As MySqlCommand = New MySqlCommand

                    commandEm.CommandType = CommandType.Text

                    commandEm.CommandText = "SELECT Email FROM tblcompanyinfo where rcno=1"
                    commandEm.Connection = conn

                    Dim drEm As MySqlDataReader = commandEm.ExecuteReader()
                    Dim dtEm As New DataTable
                    dtEm.Load(drEm)

                    If dtEm.Rows.Count > 0 Then
                        If String.IsNullOrEmpty(dtEm.Rows(0)("Email").ToString()) = False Then
                            StaffEmailID = dtEm.Rows(0)("Email").ToString()
                        End If
                    End If

                    dtEm.Clear()
                    drEm.Close()
                    commandEm.Dispose()
                End If
            End If


            'Start : 28.04.20

            'If dt1.Rows.Count > 0 Then

            '    CustName = dt1.Rows(0)("CustName").ToString
            '    If dt1.Rows(0)("SalesDate").ToString = DBNull.Value.ToString Then
            '    Else
            '        SalesDate = Convert.ToDateTime(dt1.Rows(0)("SalesDate")).ToString("dd/MM/yyyy")
            '    End If

            '    If dt1.Rows(0)("DueDate").ToString = DBNull.Value.ToString Then
            '    Else
            '        DueDate = Convert.ToDateTime(dt1.Rows(0)("DueDate")).ToString("dd/MM/yyyy")
            '    End If

            '    txtBranchLocation.Text = dt1.Rows(0)("location").ToString()

            '    Dim command2 As MySqlCommand = New MySqlCommand

            '    command2.CommandType = CommandType.Text

            '    If dt1.Rows(0)("ContactType").ToString = "COMPANY" Then
            '        command2.CommandText = "SELECT BillContact1Email,BillContact2Email FROM tblcompany where accountid='" & dt1.Rows(0)("accountid").ToString & "'"

            '        command2.Connection = conn

            '        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            '        Dim dt2 As New DataTable
            '        dt2.Load(dr2)

            '        If dt2.Rows.Count > 0 Then
            '            If dt2.Rows(0)("BillContact1Email").ToString <> "" Or String.IsNullOrEmpty(dt2.Rows(0)("BillContact1Email").ToString) = False Then
            '                txtTo.Text = dt2.Rows(0)("BillContact1Email").ToString + ";" + dt2.Rows(0)("BillContact2Email").ToString
            '            Else
            '                txtTo.Text = dt2.Rows(0)("BillContact2Email").ToString
            '            End If

            '        End If

            '        dt2.Clear()
            '        dr2.Close()
            '    ElseIf dt1.Rows(0)("ContactType").ToString = "PERSON" Then
            '        command2.CommandText = "SELECT BillEmail,BillContact2Email FROM tblPERSON where accountid='" & dt1.Rows(0)("accountid").ToString & "'"

            '        command2.Connection = conn

            '        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            '        Dim dt2 As New DataTable
            '        dt2.Load(dr2)

            '        If dt2.Rows.Count > 0 Then
            '            If dt2.Rows(0)("BillEmail").ToString <> "" Or String.IsNullOrEmpty(dt2.Rows(0)("BillEmail").ToString) = False Then
            '                txtTo.Text = dt2.Rows(0)("BillEmail").ToString + ";" + dt2.Rows(0)("BillContact2Email").ToString
            '            Else
            '                txtTo.Text = dt2.Rows(0)("BillContact2Email").ToString
            '            End If
            '        End If
            '        dt2.Clear()
            '        dr2.Close()
            '    End If

            'End If


            'End: 28.04.20

            dt1.Clear()
            dr1.Close()





            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn

            If txtQuery.Text = "CreditNote" Then

                command.CommandText = "Select * from tblEmailSetUp where SetUpID='CN'"

            ElseIf txtQuery.Text = "CreditNote2" Then
                command.CommandText = "Select * from tblEmailSetUp where SetUpID='CN'"

            ElseIf txtQuery.Text = "ServiceReportWithPhotos" Then
                command.CommandText = "Select * from tblEmailSetUp where SetUpID='SRINV'"

            ElseIf txtQuery.Text = "ServiceReportWithoutPhotos" Then
                command.CommandText = "Select * from tblEmailSetUp where SetUpID='SRINV'"
            Else
                command.CommandText = "Select * from tblEmailSetUp where SetUpID='SLSINV'"
            End If


            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            Dim subject As String = ""
            Dim content As String = ""

            If dt.Rows.Count > 0 Then
                subject = dt.Rows(0)("Subject").ToString
                content = dt.Rows(0)("Contents").ToString

            End If


            content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            content = content.Replace("CLIENT NAME", CustName)
            content = content.Replace("INVOICE DUE DATE", DueDate)
            content = content.Replace("CN DATE", SalesDate)
            content = content.Replace("SALES DATE", SalesDate)
            'content = content.Replace("COMPANY NAME", "ANTICIMEX PEST MANAGEMENT PTE. LTD.")
            content = content.Replace("COMPANY NAME", ConfigurationManager.AppSettings("CompanyName").ToString())

            content = content.Replace("INVOICE NUMBER", lblRecordNo.Text)
            content = content.Replace("CN NUMBER", lblRecordNo.Text)
            content = content.Replace("STAFF ID", Convert.ToString(Session("UserID")))
            content = content.Replace("EMAIL SENT DATE", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", New System.Globalization.CultureInfo("en-GB")))
            content = content.Replace("EmailID", StaffEmailID)

            subject = System.Text.RegularExpressions.Regex.Replace(subject, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            subject = subject.Replace("CLIENT NAME", CustName)
            subject = subject.Replace("SALES DATE", SalesDate)
            subject = subject.Replace("INVOICE NUMBER", lblRecordNo.Text)
            '   subject = subject.Replace("INVOICE NUMBER", CustAddress)

            txtSubject.Text = subject
            txtContent.Text = content

            dt.Clear()
            dr.Close()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveInvoiceInfo", ex.Message.ToString, lblRecordNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Function ValidateEmail(ByVal EmailId As String) As String
        Dim resEmail As String = ""
        If EmailId.Contains(","c) Then EmailId = EmailId.Replace(","c, ";"c)
        If EmailId.Contains("/"c) Then EmailId = EmailId.Replace("/"c, ";"c)
        If EmailId.Contains(":"c) Then EmailId = EmailId.Replace(":"c, ";"c)
        If EmailId.Contains("; ") Then EmailId = EmailId.Replace("; ", ";"c)
        resEmail = EmailId.TrimEnd(";"c)
        Return resEmail
    End Function

    Protected Function RetrieveStaffEmailID(conn As MySqlConnection, UserID As String) As String
        Dim StaffEmailID As String = ""

        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text
        command.CommandText = "SELECT EmailPerson FROM tblstaff where SecWebLoginID = @userid and status='O';"
        command.Parameters.AddWithValue("@userid", UserID)
        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If String.IsNullOrEmpty(dt.Rows(0)("EmailPerson").ToString()) = False Then
                StaffEmailID = dt.Rows(0)("EmailPerson").ToString()
            End If
        End If

        command.Dispose()
        dt.Clear()
        dr.Close()
        dt.Dispose()

        Return StaffEmailID

    End Function

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        If String.IsNullOrEmpty(txtTo.Text) Then
            MessageBox.Message.Alert(Page, "Enter email address in TO field!!!", "str")
            Return
        End If

        If String.IsNullOrEmpty(txtFrom.Text) Then
            MessageBox.Message.Alert(Page, "FROM field cannot be empty!!!", "str")
            Return
        End If

        If String.IsNullOrEmpty(txtContent.Text) Then
            MessageBox.Message.Alert(Page, "This email message is empty, please enter some message before pressing Send.", "str")
            Return
        End If

        'Dim mail As New MailMessage

        'mail.From = New MailAddress(txtFrom.Text)
        'mail.To.Add(New MailAddress(txtTo.Text.Replace(";", " ")))
        'mail.CC.Add(New MailAddress(txtCC.Text))

        'mail.Subject = txtSubject.Text

        'mail.IsBodyHtml = False

        'mail.Body = txtContent.Text
        'mail.Attachments.Add(New Attachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".PDF"))))
        'If fuAttachment.HasFile Then
        '    Dim FileName As String = Path.GetFileName(fuAttachment.PostedFile.FileName)
        '    mail.Attachments.Add(New Attachment(fuAttachment.PostedFile.InputStream, FileName))

        'End If
        'Dim smtp As New SmtpClient(ConfigurationManager.AppSettings("EmailSMTP").ToString())

        'smtp.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
        'smtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("EmailFrom").ToString(), ConfigurationManager.AppSettings("EmailPassword").ToString())
        'smtp.EnableSsl = True
        ''   smtp.EnableSsl = ConfigurationManager.AppSettings("EnableSsl").ToString()

        'smtp.Send(mail)

        ''   mail = Nothing
        'mail.Dispose()

        Try
            txtTo.Text = ValidateEmail(txtTo.Text)
            If txtTo.Text.Last.ToString = ";" Then
                txtTo.Text = txtTo.Text.Remove(txtTo.Text.Length - 1)
            End If

            If txtTo.Text.First.ToString = ";" Then
                txtTo.Text = txtTo.Text.Remove(0)
            End If

            If String.IsNullOrEmpty(txtCC.Text) = False Then
                txtCC.Text = ValidateEmail(txtCC.Text)
                If txtCC.Text.Last.ToString = ";" Then
                    txtCC.Text = txtCC.Text.Remove(txtCC.Text.Length - 1)
                End If
                If txtCC.Text.First.ToString = ";" Then
                    txtCC.Text = txtCC.Text.Remove(0)
                End If
            End If


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Retrieve sender email address
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim ReplySender As String = ""

            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT EmailPerson FROM tblstaff where SecWebLoginID = @userid;"
            command.Parameters.AddWithValue("@userid", Session("UserID"))
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                If String.IsNullOrEmpty(dt.Rows(0)("EmailPerson").ToString()) = False Then
                    ReplySender = dt.Rows(0)("EmailPerson").ToString()
                End If
            End If

            command.Dispose()
            dt.Clear()
            dr.Close()
            dt.Dispose()

            If ReplySender = "" Then


                'If EmailPerson is null in tblstaff, retrieve the EmailID from tblCompanyInfo or from Branch


                'Find if Branch/Location Enabled
                Dim LocationEnabled As String = ""
                Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
                commandServiceRecordMasterSetup.CommandType = CommandType.Text
                commandServiceRecordMasterSetup.CommandText = "SELECT DisplayRecordsLocationWise FROM tblservicerecordmastersetup"
                commandServiceRecordMasterSetup.Connection = conn

                Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
                Dim dtServiceRecordMasterSetup As New DataTable
                dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

                LocationEnabled = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
                If LocationEnabled = "N" Then
                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT Email FROM tblcompanyinfo where rcno=1"
                    command1.Connection = conn

                    Dim drEm As MySqlDataReader = command1.ExecuteReader()
                    Dim dtEm As New DataTable
                    dtEm.Load(drEm)

                    If dtEm.Rows.Count > 0 Then
                        If String.IsNullOrEmpty(dtEm.Rows(0)("Email").ToString()) = False Then
                            ReplySender = dtEm.Rows(0)("Email").ToString()

                        End If

                        dtEm.Clear()
                        drEm.Close()
                        command1.Dispose()
                    End If
                End If
                If LocationEnabled = "Y" Then

                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text

                    command2.CommandText = "SELECT Email FROM tbllocation where locationid='" + txtBranchLocation.Text + "'"
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    If dt2.Rows.Count > 0 Then
                        If String.IsNullOrEmpty(dt2.Rows(0)("Email").ToString()) = False Then
                            ReplySender = dt2.Rows(0)("Email").ToString()
                        End If
                    End If
                    dt2.Clear()
                    dr2.Close()
                    command2.Dispose()

                    If ReplySender = "" Then
                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT Email FROM tblcompanyinfo where rcno=1"
                        command1.Connection = conn

                        Dim drEm As MySqlDataReader = command1.ExecuteReader()
                        Dim dtEm As New DataTable
                        dtEm.Load(drEm)

                        If dtEm.Rows.Count > 0 Then
                            If String.IsNullOrEmpty(dtEm.Rows(0)("Email").ToString()) = False Then
                                ReplySender = dtEm.Rows(0)("Email").ToString()

                            End If

                            dtEm.Clear()
                            drEm.Close()
                            command1.Dispose()
                        End If
                    End If


                End If

            End If



            conn.Close()

            '   InsertIntoTblWebEventLog("ReplySender" + txtBranchLocation.Text, ReplySender, txtSvcRcno.Text)


            Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
            Dim oSmtp As New SmtpClient()

            Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
            oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
            oServer.ConnectType = SmtpConnectType.ConnectDirectSSL


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
                            Return
                        End If
                        oMail.[To].Add(New MailAddress(ToAddress(i).ToString.Trim))
                        'Try
                        '    oSmtp.TestRecipients(oServer, oMail)

                        'Catch ex As Exception
                        '    InsertIntoTblWebEventLog("TestEmailRecipient", ToAddress(i).ToString.Trim, lblRecordNo.Text)
                        'End Try
                    Next
                End If

            End If


            '  txtCC.Text = txtCC.Text + ";sasi.vishwa@gmail.com;"

            If String.IsNullOrEmpty(txtCC.Text) = False Then
                Dim CCAddress As String() = txtCC.Text.Split(";"c)
                If CCAddress.Count() > 0 Then
                    For i As Integer = 0 To CCAddress.Count() - 1
                        If Regex.IsMatch(CCAddress(i).ToString(), pattern) Then

                        Else
                            MessageBox.Message.Alert(Page, "Enter valid 'CC' Email address" + " (" + CCAddress(i).ToString() + ")", "str")
                            Return
                        End If
                        oMail.[Cc].Add(New MailAddress(CCAddress(i).ToString()))
                    Next
                End If
            End If




            ' oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))

            If txtQuery.Text = "ServiceReportTechSign" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else

                    InsertIntoTblWebEventLog("btnSend_Click", txtSvcAddr.Text, "")

                    Dim Filename As String = Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_" + txtSvcAddr.Text + ".PDF")
                    '    Dim Filename As String = Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF")

                    If System.IO.File.Exists(Filename) Then
                        System.IO.File.Delete(Filename)
                    End If
                    File.Move(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_ServiceReport.PDF"), Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_" + txtSvcAddr.Text + ".PDF"))

                    ' File.Move(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_ServiceReport.PDF"), Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))

                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_" + txtSvcAddr.Text + ".PDF"))
                    ' oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))
                End If

                If lnkPreview1.Text = "" Then

                Else

                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF"))

                End If
            ElseIf txtQuery.Text = "SvcSupplement" Then

                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF"))

                End If


            ElseIf txtQuery.Text = "ServiceReport" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_ServiceForm.PDF"))

                End If

            ElseIf txtQuery.Text = "SanitationReport" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_SanitationForm.PDF"))

                End If

            ElseIf txtQuery.Text = "ServiceRecordPrinting" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/ServiceReports.PDF"))

                End If

            ElseIf txtQuery.Text = "SOA" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/SOA.PDF"))

                End If

            ElseIf txtQuery.Text = "CustSOA" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/CustSOA.PDF"))

                End If

            ElseIf txtQuery.Text = "ReceiptTransactions" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/ReceiptTransactions.PDF"))

                End If

            ElseIf txtQuery.Text = "TransactionSummary" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/TransactionSummary.PDF"))

                End If

            ElseIf txtQuery.Text = "ContractServiceSchedule" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat1" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat2" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat3" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat4" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat5" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat6" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat7" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat8" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat9" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat10" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat11" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                InsertIntoTblWebEventLog("btnSend", lnkPreview.Text + " " + txtFileType.Text, lblRecordNo.Text)

                If lnkPreview.Text = "" Then

                Else
                    If txtFileType.Text = "Individual" Then
                        oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                        oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF"))

                    Else
                        oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF"))
                        InsertIntoTblWebEventLog("btnSend2", lnkPreview.Text + " " + txtFileType.Text, lblRecordNo.Text)

                    End If

                End If


            ElseIf txtQuery.Text = "InvoiceFormat12" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If



            ElseIf txtQuery.Text = "InvoiceFormat13" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If



            ElseIf txtQuery.Text = "InvoiceFormat14" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                End If

            ElseIf txtQuery.Text = "InvoiceFormat15" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                InsertIntoTblWebEventLog("btnSend", lnkPreview.Text + " " + txtFileType.Text, lblRecordNo.Text)

                If lnkPreview.Text = "" Then

                Else
                    If txtFileType.Text = "Individual" Then
                        oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                        oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF"))

                    Else
                        oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF"))
                        InsertIntoTblWebEventLog("btnSend2", lnkPreview.Text + " " + txtFileType.Text, lblRecordNo.Text)

                    End If

                End If

            ElseIf txtQuery.Text = "ServiceReportWithPhotos" Then

                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReportPhotos.PDF"))

                End If


            ElseIf txtQuery.Text = "ServiceReportWithoutPhotos" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReport.PDF"))

                End If


            ElseIf txtQuery.Text = "CreditNote" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    If Convert.ToString(Session("PrintType")) = "Print" Then

                        oMail.AddAttachment(Server.MapPath("~/PDFs/" + Session("RecordNo") + ".PDF"))

                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then

                        oMail.AddAttachment(Server.MapPath("~/PDFs/CN.PDF"))

                    End If
                End If

            ElseIf txtQuery.Text = "CreditNote2" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    If Convert.ToString(Session("PrintType")) = "Print" Then

                        oMail.AddAttachment(Server.MapPath("~/PDFs/" + Session("RecordNo") + ".PDF"))

                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then

                        oMail.AddAttachment(Server.MapPath("~/PDFs/CN.PDF"))

                    End If
                End If


            ElseIf txtQuery.Text = "Receipt" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Session("ReceiptNumber") + ".PDF"))

                End If

            ElseIf txtQuery.Text = "CollectionNote" Then
                oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
                If lnkPreview.Text = "" Then

                Else
                    oMail.AddAttachment(Server.MapPath("~/PDFs/" + Session("ReceiptNumber") + ".PDF"))

                End If
            ElseIf txtQuery.Text = "FileUpload" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else

                    oMail.AddAttachment(Convert.ToString(Session("FilePath")))
                End If
            ElseIf txtQuery.Text = "ContractFileUpload" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else

                    oMail.AddAttachment(Convert.ToString(Session("FilePath")))
                End If
            ElseIf txtQuery.Text = "AssetFileUpload" Then
                oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

                oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()
                If lnkPreview.Text = "" Then

                Else

                    oMail.AddAttachment(Convert.ToString(Session("FilePath")))
                End If
            End If
            InsertIntoTblWebEventLog("btnSend_Click", AdditionalFile, lblRecordNo.Text)

            If lnkPreview2.Text = "" Then

            Else
                AdditionalFile = "Y"
            End If

            If AdditionalFile = "Y" Then

                oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "/Price Increase Notification.PDF"))
            End If
            'If fuAttachment.HasFile Then
            '    Dim FileName As String = Path.GetFileName(fuAttachment.PostedFile.FileName)
            '    oMail.AddAttachment(fuAttachment.PostedFile.FileName.ToString)

            'End If

            If String.IsNullOrEmpty(lnkAttach1.Text) = False Then
                If txtManualRpt.Text = "Yes" Then
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Service/") + lnkAttach1.Text)

                Else
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")) + "/" + lnkAttach1.Text)

                End If
            End If
            If String.IsNullOrEmpty(lnkAttach2.Text) = False Then
                If txtManualRpt.Text = "Yes" Then
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Service/") + lnkAttach2.Text)

                Else
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")) + "/" + lnkAttach2.Text)

                End If
            End If
            If String.IsNullOrEmpty(lnkAttach3.Text) = False Then
                If txtManualRpt.Text = "Yes" Then
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Service/") + lnkAttach3.Text)

                Else
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")) + "/" + lnkAttach3.Text)

                End If
            End If

            If String.IsNullOrEmpty(lnkAttach4.Text) = False Then
                If txtManualRpt.Text = "Yes" Then
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Service/") + lnkAttach4.Text)

                Else
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")) + "/" + lnkAttach4.Text)

                End If
            End If

            If String.IsNullOrEmpty(lnkAttach5.Text) = False Then
                If txtManualRpt.Text = "Yes" Then
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Service/") + lnkAttach5.Text)

                Else
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")) + "/" + lnkAttach5.Text)

                End If
            End If

            If String.IsNullOrEmpty(lnkAttach6.Text) = False Then
                If txtManualRpt.Text = "Yes" Then
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Service/") + lnkAttach6.Text)

                Else
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")) + "/" + lnkAttach6.Text)

                End If
            End If

            If String.IsNullOrEmpty(lnkAttach7.Text) = False Then
                If txtManualRpt.Text = "Yes" Then
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Service/") + lnkAttach7.Text)

                Else
                    oMail.AddAttachment(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")) + "/" + lnkAttach7.Text)

                End If
            End If

            oMail.ReplyTo = ReplySender

            Try
                oSmtp.SendMail(oServer, oMail)

                iframeid.Attributes.Add("src", "about:blank")
                If System.IO.Directory.Exists(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID"))) Then
                    System.IO.Directory.Delete(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")), True)
                End If

                ' Delete pdf attachment
                'System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))
                If txtQuery.Text = "ServiceReportTechSign" Then
                    UpdateEmailSentField()

                    '  System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_" + txtSvcAddr.Text + ".PDF"))
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_ServiceReport" + ".PDF"))
                    '  ElseIf txtQuery.Text = "SvcSupplement" Then
                    ' System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF"))
                ElseIf txtQuery.Text = "SvcSupplement" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_Supplement.PDF"))
                ElseIf txtQuery.Text = "ServiceReport" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_ServiceForm.PDF"))

                ElseIf txtQuery.Text = "SanitationReport" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "_SanitationForm.PDF"))
                ElseIf txtQuery.Text = "ServiceRecordPrinting" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/ServiceReports.PDF"))
                ElseIf txtQuery.Text = "SOA" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/SOA.PDF"))
                ElseIf txtQuery.Text = "CustSOA" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/CustSOA.PDF"))
                ElseIf txtQuery.Text = "ReceiptTransactions" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/ReceiptTransactions.PDF"))
                ElseIf txtQuery.Text = "TransactionSummary" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/TransactionSummary.PDF"))
                ElseIf txtQuery.Text = "ContractServiceSchedule" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat1" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat2" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat3" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat4" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat5" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat6" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat7" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat8" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat9" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                ElseIf txtQuery.Text = "InvoiceFormat10" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))

                ElseIf txtQuery.Text = "InvoiceFormat11" Then
                    UpdateInvoiceEmailSentField()
                    If txtFileType.Text = "Individual" Then
                        System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                        System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF"))

                    Else
                        System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF"))

                    End If

                ElseIf txtQuery.Text = "InvoiceFormat12" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))

                ElseIf txtQuery.Text = "InvoiceFormat13" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))


                ElseIf txtQuery.Text = "InvoiceFormat14" Then
                    UpdateInvoiceEmailSentField()

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))


                ElseIf txtQuery.Text = "InvoiceFormat15" Then
                    UpdateInvoiceEmailSentField()
                    If txtFileType.Text = "Individual" Then
                        System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                        System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF"))

                    Else
                        System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF"))

                    End If
                ElseIf txtQuery.Text = "ServiceReportWithPhotos" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReportPhotos.PDF"))

                ElseIf txtQuery.Text = "ServiceReportWithoutPhotos" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReport.PDF"))

                ElseIf txtQuery.Text = "CreditNote" Then
                    If Convert.ToString(Session("PrintType")) = "Print" Then
                        System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("RecordNo")) + ".PDF"))
                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                        System.IO.File.Delete(Server.MapPath("~/PDFs/CN.PDF"))
                    End If

                ElseIf txtQuery.Text = "CreditNote2" Then
                    If Convert.ToString(Session("PrintType")) = "Print" Then
                        System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("RecordNo")) + ".PDF"))
                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                        System.IO.File.Delete(Server.MapPath("~/PDFs/CN.PDF"))
                    End If

                ElseIf txtQuery.Text = "Receipt" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("ReceiptNumber")) + ".PDF"))
                ElseIf txtQuery.Text = "CollectionNote" Then
                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + Convert.ToString(Session("ReceiptNumber")) + ".PDF"))

                End If

                InsertIntoTblWebEventLog("btnSend", AdditionalFile, lblRecordNo.Text)

                If AdditionalFile = "Y" Then

                    UpdateAdditionalAttachmentSentField(lblRecordNo.Text)

                    System.IO.File.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text + "/Price Increase Notification.PDF"))
                    System.IO.Directory.Delete(Server.MapPath("~/PDFs/" + lblRecordNo.Text))


                End If
            Catch ex As Exception
                mdlMsg.Show()

                lblMsg.Text = ex.ToString()

            End Try

            oSmtp.Close()

            txtTo.Enabled = False
            txtFrom.Enabled = False
            txtCC.Enabled = False
            'txtBCC.Enabled = False
            txtSubject.Enabled = False
            txtContent.Enabled = False
            txtAttachment.Enabled = False
            btnSend.Enabled = False
            lnkPreview.Enabled = False

            '     MessageBox.Message.Alert(Page, "Mail Sent Successfully", "str")
            mdlPopupMsg.Show()

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            MessageBox.Message.Alert(Page, exstr, "str")
            InsertIntoTblWebEventLog("btnSend_Click", ex.Message.ToString, lblRecordNo.Text)

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
            command.Parameters.AddWithValue("@EmailSentDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            command.Parameters.AddWithValue("@RecordNo", lblRecordNo.Text)
            command.ExecuteNonQuery()

            command.Dispose()

            conn.Close()
            conn.Dispose()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("UpdateEmailSendField", ex.Message.ToString, lblRecordNo.Text)
        End Try
    End Sub

    Private Sub UpdateInvoiceEmailSentField()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn

            If lblRecordNo.Text = "INVOICE" Then
                command.CommandText = "UPDATE tblsales SET EmailSentStatus = 'Y',EmailSentDate = @EmailSentDate,EmailSentBy=@EmailSentBy WHERE InvoiceNumber in (" & Session("InvoiceNumber") & ")"
                command.Parameters.Clear()

                ' command.Parameters.AddWithValue("@EmailSent", 1)
                command.Parameters.AddWithValue("@EmailSentDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                '   command.Parameters.AddWithValue("@InvoiceNumber", lblRecordNo.Text)
                command.Parameters.AddWithValue("@EmailSentBy", Session("UserID"))
            Else
                command.CommandText = "UPDATE tblsales SET EmailSentStatus = 'Y',EmailSentDate = @EmailSentDate,EmailSentBy=@EmailSentBy WHERE InvoiceNumber = @InvoiceNumber"
                command.Parameters.Clear()

                ' command.Parameters.AddWithValue("@EmailSent", 1)
                command.Parameters.AddWithValue("@EmailSentDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                command.Parameters.AddWithValue("@InvoiceNumber", lblRecordNo.Text)
                command.Parameters.AddWithValue("@EmailSentBy", Session("UserID"))
            End If


            command.ExecuteNonQuery()

            command.Dispose()

            conn.Close()
            conn.Dispose()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("UpdateInvoiceEmailSendField", ex.Message.ToString, lblRecordNo.Text)
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
            insCmds.Parameters.AddWithValue("@LoginId", "EMAIL - " + Session("UserID"))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, lblRecordNo.Text)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'ClientScript.RegisterStartupScript(Me.GetType(), "script", "window.close();", True)
        Session("SvcRecordNo") = lblRecordNo.Text
        '  Session("servicefrom") = "servicedetails"
        Session("Query") = txtQuery.Text
        Session("SvcRcNo") = txtSvcRcno.Text
        ClientScript.RegisterStartupScript(Page.[GetType](), "test", "<script>window.close();</script>")

    End Sub

    Protected Sub PreviewFile(ByVal sender As Object, ByVal e As EventArgs)
        'Dim filePath As String = CType(sender, LinkButton).CommandArgument
        'Dim ext As String = Path.GetExtension(filePath)
        'filePath = "Uploads/" + filePath
        ''  filePath = Server.MapPath("~/Uploads/") + filePath
        ''    frmWord.Attributes["src"] = http://localhost/MyApp/resume.doc;
        'iframeid.Attributes.Add("src", "PDFs/ServiceReports.PDF")
        'Return

        '    iframeid.Attributes.Add("src", filePath)
        Dim FilePath As String = ""
        If txtQuery.Text = "ServiceReportTechSign" Then
            FilePath = "PDFs/" + lblRecordNo.Text + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "SvcSupplement" Then
            FilePath = "PDFs/" + lblRecordNo.Text + "_Supplement.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "ServiceReport" Then
            FilePath = "PDFs/" + lblRecordNo.Text + "_ServiceForm.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "SanitationReport" Then
            FilePath = "PDFs/" + lblRecordNo.Text + "_SanitationForm.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "ServiceRecordPrinting" Then
            FilePath = "PDFs/ServiceReports.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "SOA" Then
            FilePath = "PDFs/SOA.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "CustSOA" Then
            FilePath = "PDFs/CustSOA.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "ReceiptTransactions" Then
            FilePath = "PDFs/ReceiptTransactions.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "TransactionSummary" Then
            FilePath = "PDFs/TransactionSummary.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "ContractServiceSchedule" Then
            FilePath = "PDFs/" + lblRecordNo.Text + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat1" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat2" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat3" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat4" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat5" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat6" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat7" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat8" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat9" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat10" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat11" Then
            If txtFileType.Text = "Individual" Then
                FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            Else
                FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF"
            End If

            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat12" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat13" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat14" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "InvoiceFormat15" Then
            If txtFileType.Text = "Individual" Then
                FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF"
            Else
                FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF"
            End If

            iframeid.Attributes.Add("src", FilePath)
        ElseIf txtQuery.Text = "ServiceReportWithPhotos" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReportPhotos.PDF"
            iframeid.Attributes.Add("src", FilePath)


        ElseIf txtQuery.Text = "ServiceReportWithoutPhotos" Then
            FilePath = "PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReport.PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "CreditNote" Then
            FilePath = "PDFs/" + Convert.ToString(Session("RecordNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "CreditNote2" Then
            FilePath = "PDFs/" + Convert.ToString(Session("RecordNo")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "Receipt" Then
            FilePath = "PDFs/" + Convert.ToString(Session("ReceiptNumber")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "CollectionNote" Then
            FilePath = "PDFs/" + Convert.ToString(Session("ReceiptNumber")) + ".PDF"
            iframeid.Attributes.Add("src", FilePath)

        ElseIf txtQuery.Text = "FileUpload" Then
            '  FilePath = Convert.ToString(Session("FilePath"))
            Dim ext As String = Path.GetExtension(Convert.ToString(Session("FileName")))
            ext = ext.ToLower
            FilePath = "Uploads/Service/" + Convert.ToString(Session("FileName"))

            ' Response.Write(FilePath + " " + ext)
            If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
                Dim strFilePath As String = Server.MapPath("Uploads\Service\")
                Dim strFile As String = Convert.ToString(Session("FileName"))
                Dim File As String() = strFile.Split("."c)
                Dim strExtension As String = ext
                Dim strUrl As String = "http://" + Request.Url.Authority + "/WordinIFrame/ConvertedLocation/"

                Dim Filename As String = strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html")

                If System.IO.File.Exists(Filename) Then
                    System.IO.File.Delete(Filename)
                End If

                If ext = ".doc" Or ext = ".docx" Then
                    ConvertHTMLFromWord(strFilePath & strFile, strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html"))

                ElseIf ext = ".xls" Or ext = ".xlsx" Then
                    ConvertHtmlFromExcel(strFilePath + strFile, strFilePath + "A" + strFile.Split("."c)(0) + ".html")
                End If

                iframeid.Attributes("src") = "Uploads/Service/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

            Else
                FilePath = FilePath.Replace("#", "%23")

                iframeid.Attributes.Add("src", FilePath)
                ' iframeid.Attributes.Add("src", "Uploads/Service/SERV201711-006222_SERV201711-018002_ONMIR %2350-01.PDF")
            End If
        ElseIf txtQuery.Text = "ContractFileUpload" Then
            '  FilePath = Convert.ToString(Session("FilePath"))
            Dim ext As String = Path.GetExtension(Convert.ToString(Session("FileName")))
            ext = ext.ToLower
            FilePath = "Uploads/Contract/" + Convert.ToString(Session("FileName"))

            ' Response.Write(FilePath + " " + ext)
            If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
                Dim strFilePath As String = Server.MapPath("Uploads\Contract\")
                Dim strFile As String = Convert.ToString(Session("FileName"))
                Dim File As String() = strFile.Split("."c)
                Dim strExtension As String = ext
                Dim strUrl As String = "http://" + Request.Url.Authority + "/WordinIFrame/ConvertedLocation/"

                Dim Filename As String = strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html")

                If System.IO.File.Exists(Filename) Then
                    System.IO.File.Delete(Filename)
                End If

                If ext = ".doc" Or ext = ".docx" Then
                    ConvertHTMLFromWord(strFilePath & strFile, strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html"))

                ElseIf ext = ".xls" Or ext = ".xlsx" Then
                    ConvertHtmlFromExcel(strFilePath + strFile, strFilePath + "A" + strFile.Split("."c)(0) + ".html")
                End If

                iframeid.Attributes("src") = "Uploads/Contract/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

            Else
                FilePath = FilePath.Replace("#", "%23")

                iframeid.Attributes.Add("src", FilePath)
                ' iframeid.Attributes.Add("src", "Uploads/Service/SERV201711-006222_SERV201711-018002_ONMIR %2350-01.PDF")
            End If
        ElseIf txtQuery.Text = "AssetFileUpload" Then
            '  FilePath = Convert.ToString(Session("FilePath"))
            Dim ext As String = Path.GetExtension(Convert.ToString(Session("FileName")))
            ext = ext.ToLower
            FilePath = "Uploads/Asset/" + Convert.ToString(Session("FileName"))

            ' Response.Write(FilePath + " " + ext)
            If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
                Dim strFilePath As String = Server.MapPath("Uploads\Asset\")
                Dim strFile As String = Convert.ToString(Session("FileName"))
                Dim File As String() = strFile.Split("."c)
                Dim strExtension As String = ext
                Dim strUrl As String = "http://" + Request.Url.Authority + "/WordinIFrame/ConvertedLocation/"

                Dim Filename As String = strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html")

                If System.IO.File.Exists(Filename) Then
                    System.IO.File.Delete(Filename)
                End If

                If ext = ".doc" Or ext = ".docx" Then
                    ConvertHTMLFromWord(strFilePath & strFile, strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html"))

                ElseIf ext = ".xls" Or ext = ".xlsx" Then
                    ConvertHtmlFromExcel(strFilePath + strFile, strFilePath + "A" + strFile.Split("."c)(0) + ".html")
                End If

                iframeid.Attributes("src") = "Uploads/Asset/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

            Else
                FilePath = FilePath.Replace("#", "%23")

                iframeid.Attributes.Add("src", FilePath)
                ' iframeid.Attributes.Add("src", "Uploads/Service/SERV201711-006222_SERV201711-018002_ONMIR %2350-01.PDF")
            End If

        End If

        ' FilePreview(FilePath)

        'Dim User As New WebClient()
        'Dim FileBuffer As [Byte]() = User.DownloadData(filePath)
        'If FileBuffer IsNot Nothing Then
        '    Response.ContentType = "application/pdf"
        '    Response.AddHeader("content-length", FileBuffer.Length.ToString())
        '    Response.AddHeader("content-disposition", "inline; filename=" & lblRecordNo.Text & ".pdf")

        '    Response.BinaryWrite(FileBuffer)
        'End If
    End Sub

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
            Return ds
        Catch ex As Exception
            InsertIntoTblWebEventLog("GetSvcImages", ex.Message.ToString, lblRecordNo.Text)
            Return Nothing
        End Try
    End Function

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
            conn.Dispose()
            Return ds
        Catch ex As Exception
            InsertIntoTblWebEventLog("GetSvcDetails", ex.Message.ToString, lblRecordNo.Text)
            Return Nothing

        End Try
    End Function

    Protected Sub btnConfirmOk_Click(sender As Object, e As EventArgs) Handles btnConfirmOk.Click
        ClientScript.RegisterStartupScript(Page.[GetType](), "test", "<script>window.close();</script>")

    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload

        crReportDocument.Close()
        crReportDocument.Dispose()

    End Sub

    Protected Sub FilePreview(filePath As String)
        '  Dim filePath As String = CType(sender, LinkButton).CommandArgument
        Dim ext As String = Path.GetExtension(filePath)
        '  filePath = "Uploads/Customer/" + filePath
        ext = ext.ToLower


        If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
            Dim strFilePath As String = Server.MapPath("PDFs\")
            Dim strFile As String = filePath
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

            iframeid.Attributes("src") = "PDFs/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

        Else
            iframeid.Attributes.Add("src", filePath)

        End If
        '  iframeid.Attributes.Add("src", "https://docs.google.com/viewer?url={D:/1_CWBInfotech/A_Sitapest/Program/Sitapest/Uploads/10000145_ActualVsForecast_Format1.pdf?pid=explorer&efh=false&a=v&chrome=false&embedded=true")

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
            'Excel.Visible = False
            'Excel.Application.Visible = False
            'Excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMinimized

            'Excel.Workbooks.Open(Source, Unknown, Unknown, Unknown, Unknown, Unknown, _
            '    Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
            '    Unknown, Unknown, Unknown)

            'Dim format As Object = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml

            'Excel.Workbooks(1).SaveAs(Target, format, Unknown, Unknown, Unknown, Unknown, _
            '    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Unknown, Unknown, Unknown, Unknown, Unknown)

            'Status = StatusType.SUCCESS

            'Message = Status.ToString()

            Dim format As Object = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml

            Dim excel As New Microsoft.Office.Interop.Excel.Application
            Dim xls As Microsoft.Office.Interop.Excel.Workbook
            xls = excel.Workbooks.Open(Source)
            xls.SaveAs(Target, format)
            xls.Close()
        Catch e As Exception
            Message = "Error :" + e.Message.ToString().Trim()
        Finally
            If Excel IsNot Nothing Then
                Excel.Workbooks.Close()
                Excel.Quit()
            End If
        End Try
    End Sub

    Protected Sub lnkPreview_Click(sender As Object, e As EventArgs) Handles lnkPreview.Click
        PreviewFile(sender, e)
    End Sub


    Protected Sub lnkPreview1_Click(sender As Object, e As EventArgs) Handles lnkPreview1.Click

        Dim FilePath As String = ""

        FilePath = "PDFs/" + lblRecordNo.Text + "_ServiceReports.PDF"

        iframeid.Attributes.Add("src", FilePath)

    End Sub

    Public Shared AttachCount As Int16 = 0

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try

            If FileUpload1.HasFile Then

                Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim ext As String = Path.GetExtension(fileName)
                If Not Directory.Exists(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID"))) Then
                    Directory.CreateDirectory(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")))
                End If

                FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")) + "/" + fileName))

                If lnkAttach1.Text = "" Then
                    lnkAttach1.Text = fileName
                    AttachCount = AttachCount + 1
                    ImageButton1.Visible = True

                ElseIf lnkAttach2.Text = "" Then
                    lnkAttach2.Text = fileName
                    AttachCount = AttachCount + 1
                    ImageButton2.Visible = True

                ElseIf lnkAttach3.Text = "" Then
                    lnkAttach3.Text = fileName
                    AttachCount = AttachCount + 1
                    ImageButton3.Visible = True

                ElseIf lnkAttach4.Text = "" Then
                    lnkAttach4.Text = fileName
                    AttachCount = AttachCount + 1
                    ImageButton4.Visible = True

                ElseIf lnkAttach5.Text = "" Then
                    lnkAttach5.Text = fileName
                    AttachCount = AttachCount + 1
                    ImageButton5.Visible = True

                ElseIf lnkAttach6.Text = "" Then
                    lnkAttach6.Text = fileName
                    AttachCount = AttachCount + 1
                    ImageButton8.Visible = True

                ElseIf lnkAttach7.Text = "" Then
                    lnkAttach7.Text = fileName
                    AttachCount = AttachCount + 1
                    ImageButton9.Visible = True
                Else
                    mdlMsg.Show()
                    lblMsg.Text = "CANNOT ATTACH MORE THAN 7 FILES"
                End If

            Else

                mdlMsg.Show()
                lblMsg.Text = "SELECT FILE TO UPLOAD"

            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADD ATTACHMENT", ex.Message.ToString, FileUpload1.PostedFile.FileName)
        End Try
    End Sub

    Protected Sub btnOkMsg_Click(sender As Object, e As EventArgs) Handles btnOkMsg.Click
        mdlMsg.Hide()
    End Sub

    Protected Sub lnkAttach1_Click(sender As Object, e As EventArgs) Handles lnkAttach1.Click
        Dim FilePath As String = ""
        If txtManualRpt.Text = "Yes" Then
            FilePath = "Uploads/Service/" + lnkAttach1.Text
        Else
            FilePath = "Uploads/Email/" + Convert.ToString(Session("UserID")) + "/" + lnkAttach1.Text
        End If

        PreviewFile(FilePath, lnkAttach1.Text)
        '  iframeid.Attributes.Add("src", FilePath)
        ImageButton1.Visible = True
    End Sub

    Protected Sub lnkAttach2_Click(sender As Object, e As EventArgs) Handles lnkAttach2.Click
        Dim FilePath As String = ""
        If txtManualRpt.Text = "Yes" Then
            FilePath = "Uploads/Service/" + lnkAttach2.Text
        Else
            FilePath = "Uploads/Email/" + Convert.ToString(Session("UserID")) + "/" + lnkAttach2.Text
        End If

        PreviewFile(FilePath, lnkAttach2.Text)
        '  iframeid.Attributes.Add("src", FilePath)
        ImageButton2.Visible = True
    End Sub

    Protected Sub lnkAttach3_Click(sender As Object, e As EventArgs) Handles lnkAttach3.Click
        Dim FilePath As String = ""

        If txtManualRpt.Text = "Yes" Then
            FilePath = "Uploads/Service/" + lnkAttach3.Text
        Else
            FilePath = "Uploads/Email/" + Convert.ToString(Session("UserID")) + "/" + lnkAttach3.Text
        End If

        PreviewFile(FilePath, lnkAttach3.Text)
        '  iframeid.Attributes.Add("src", FilePath)
        ImageButton3.Visible = True
    End Sub

    Protected Sub lnkAttach4_Click(sender As Object, e As EventArgs) Handles lnkAttach4.Click
        Dim FilePath As String = ""

        If txtManualRpt.Text = "Yes" Then
            FilePath = "Uploads/Service/" + lnkAttach4.Text
        Else
            FilePath = "Uploads/Email/" + Convert.ToString(Session("UserID")) + "/" + lnkAttach4.Text
        End If
        PreviewFile(FilePath, lnkAttach4.Text)
        '  iframeid.Attributes.Add("src", FilePath)
        ImageButton4.Visible = True
    End Sub

    Protected Sub lnkAttach5_Click(sender As Object, e As EventArgs) Handles lnkAttach5.Click
        Dim FilePath As String = ""

        If txtManualRpt.Text = "Yes" Then
            FilePath = "Uploads/Service/" + lnkAttach5.Text
        Else
            FilePath = "Uploads/Email/" + Convert.ToString(Session("UserID")) + "/" + lnkAttach5.Text
        End If
        PreviewFile(FilePath, lnkAttach5.Text)
        '  iframeid.Attributes.Add("src", FilePath)
        ImageButton5.Visible = True
    End Sub

    Protected Sub PreviewFile(filepath As String, attachfilename As String)

        Dim ext As String = Path.GetExtension(attachfilename)

        ext = ext.ToLower

        If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
            Dim strFilePath As String = Server.MapPath("Uploads\Email\") + Convert.ToString(Session("UserID")) + "\"
            Dim strFile As String = attachfilename
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

            iframeid.Attributes("src") = "Uploads/Email/" + Convert.ToString(Session("UserID")) + "/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

        Else
            iframeid.Attributes.Add("src", filepath)

        End If
        '  iframeid.Attributes.Add("src", "https://docs.google.com/viewer?url={D:/1_CWBInfotech/A_Sitapest/Program/Sitapest/Uploads/10000145_ActualVsForecast_Format1.pdf?pid=explorer&efh=false&a=v&chrome=false&embedded=true")
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        lnkAttach1.Text = ""
        ImageButton1.Visible = False
        AttachCount = AttachCount - 1
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        lnkAttach2.Text = ""
        ImageButton2.Visible = False
        AttachCount = AttachCount - 1
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        lnkAttach3.Text = ""
        ImageButton3.Visible = False
        AttachCount = AttachCount - 1
    End Sub

    Protected Sub ImageButton4_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton4.Click
        lnkAttach4.Text = ""
        ImageButton4.Visible = False
        AttachCount = AttachCount - 1
    End Sub

    Protected Sub ImageButton5_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton5.Click
        lnkAttach5.Text = ""
        ImageButton5.Visible = False
        AttachCount = AttachCount - 1
    End Sub

    Protected Sub ImageButton6_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton6.Click
        lnkPreview.Text = ""
        ImageButton6.Visible = False
    End Sub

    'Private Function GetDataInvRecv(AccountID As String, AccountType As String, CutOffDate As String) As Boolean
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
    '    qry = qry + " and tblsales.Accountid = '" + AccountID + "'"
    '    '    End If
    '    qryrecv = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,tblrecvdet.appliedbase,tblrecvdet.valuebase"
    '    'qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID<>'CONTRA'"
    '    qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque, tblrecvdet.rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "
    '    qryrecv = qryrecv + " and tblrecv.Accountid = '" + AccountID + "'"

    '    qryrecv1 = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,-tblrecvdet.appliedbase as appliedbase,-tblrecvdet.valuebase as valuebase"
    '    'qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) AND BankID='CONTRA'"
    '    qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque, tblrecvdet.rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "
    '    qryrecv1 = qryrecv1 + " and tblrecv.Accountid = '" + AccountID + "'"



    '    qry = qry + " ORDER BY AccountId, InvoiceNumber"
    '    qryrecv = qryrecv + " ORDER BY AccountId, tblrecv.ReceiptNumber"
    '    qryrecv1 = qryrecv1 + " ORDER BY AccountId, tblrecv.ReceiptNumber"


    '    txtQuery1.Text = qry
    '    '  txtQueryRecv.Text = qryrecv
    '    '  txtQueryRecv1.Text = qryrecv1

    '    'InsertM02AR22()

    '    InsertM02AR22Sen(AccountID, AccountType, CutOffDate, qryrecv, qryrecv1)

    '    Session.Add("selFormula", selFormula)
    '    Session.Add("selection", selection)
    '    Session.Add("PrintDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

    '    Return True
    'End Function

    'Private Sub InsertM02AR22Sen(AccountID As String, AccountType As String, CutOffDate As String, QueryRecv As String, QueryRecv1 As String)
    '    Try

    '        Dim sqlst, sqlst1, isWhere, isWhere1 As String
    '        Dim inIsWhere, inIsWhere1 As Integer

    '        sqlst = ""
    '        sqlst1 = ""

    '        isWhere = txtQuery1.Text
    '        inIsWhere = isWhere.IndexOf("where")

    '        'txtCustName.Text = ""

    '        sqlst = "insert into tblar22 (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT SalesDate, 'INVOICE',  InvoiceNumber, appliedbase, (CreditBase+ReceiptBase), OPeriodBalance, contacttype, AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' ,comments, SalesDate,'" & Session("UserID") & "', CustName, InvoiceNumber, Rcno, 'ARIN' from tblSales "
    '        sqlst = sqlst + " " + txtQuery1.Text.Substring(inIsWhere)

    '        ''''''''''''''''''''''''
    '        'txtCustName.Text = txtQueryRecv.Text
    '        isWhere1 = QueryRecv
    '        inIsWhere1 = isWhere1.IndexOf("where")

    '        'txtCustName.Text = ""

    '        sqlst1 = "insert into tblar22 (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT tblrecv.ReceiptDate, 'RECEIPT',  tblrecv.Cheque, 0, tblrecvdet.appliedbase, tblrecvdet.appliedbase * (-1), tblrecv.contacttype, tblrecv.AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' , tblrecv.comments, tblrecv.ReceiptDate,'" & Session("UserID") & "', tblrecv.ReceiptFrom, tblrecv.ReceiptNumber, tblRecvdet.Rcno, 'RECV' from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber  "
    '        sqlst1 = sqlst1 + " " + QueryRecv.Substring(inIsWhere1)

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

    '        command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(CutOffDate).ToString("yyyy-MM-dd"))
    '        command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

    '        command.Parameters.AddWithValue("@pr_AccountType", AccountType)
    '        command.Parameters.AddWithValue("@pr_AccountIdFrom", AccountID.Trim)
    '        command.Parameters.AddWithValue("@pr_AccountIdTo", AccountID.Trim)


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

    '        command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(CutOffDate).ToString("yyyy-MM-dd"))
    '        command5.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
    '        command5.Connection = conn
    '        command5.ExecuteScalar()
    '        conn.Close()
    '        conn.Dispose()
    '        command5.Dispose()

    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("InserM02AR22", ex.Message.ToString, "")

    '    End Try

    'End Sub

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

            GetDataInvRecv(conn, AccountID, AccountType, CutOffDate, UserID)

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


        Dim qryJrnl As String = ""
        Dim qryJrnl1 As String = ""

        qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase,appliedbase, CreditBase, ReceiptBase,"
        qry = qry + "BalanceBase,"
        qry = qry + "StaffCode,comments,contacttype, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo, Rcno FROM tblsales"
        'qry = qry + " where balancebase <> 0 and doctype='ARIN' and poststatus='P'"
        qry = qry + " where balancebase <> 0  and poststatus='P'"
        qry = qry + " and tblsales.Accountid = '" + AccountID + "'"
        '    End If
        qryrecv = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,tblrecvdet.appliedbase,tblrecvdet.valuebase"
        'qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID<>'CONTRA'"
        qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque, tblrecvdet.rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "
        qryrecv = qryrecv + " and tblrecv.Accountid = '" + AccountID + "'"

        qryrecv1 = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,-tblrecvdet.appliedbase as appliedbase,-tblrecvdet.valuebase as valuebase"
        'qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) AND BankID='CONTRA'"
        qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque, tblrecvdet.rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "
        qryrecv1 = qryrecv1 + " and tblrecv.Accountid = '" + AccountID + "'"

        qryJrnl = "select tbljrnv.vouchernumber,tblrecv.journaldate,tblrecv.receiptfrom,tbljrnv.comments, tbljrnv.Location"
        'qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque, tblrecvdet.SubCode,tblrecvdet.RefType from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID<>'CONTRA'"
        qryJrnl = qryJrnl + ",tbljrnvdet.debitbase,tbljrnvdet.creditbase, tbljrnvdet.LedgerCode,tblrecvdet.LedgerName, tbljrnvdet.ContactType, tbljrnvdet.Reftype ,tbljrnvdet.AccountId, tbljrnvdet.LocationId ,tbljrnvdet.ItemType,  tbljrnvdet.Rcno tbljrnvdet.Rcno from tbljrnv left outer join tbljrnvdet on tbljrnv.vouchernumber=tbljrnvdet.vouchernumber "
        qryJrnl = qryJrnl + " where tbljrnvdet.ItemType ='RECEIPT' and 1=1 and tbljrnvdet.AccountId = '" + AccountID + "'"


        qry = qry + " ORDER BY AccountId, InvoiceNumber"
        qryrecv = qryrecv + " ORDER BY AccountId, tblrecv.ReceiptNumber"
        qryrecv1 = qryrecv1 + " ORDER BY AccountId, tblrecv.ReceiptNumber"

        qryJrnl = qryJrnl + " ORDER BY tbljrnvdet.AccountId, tbljrnv.VoucherNumber"
        qryJrnl1 = qryJrnl1 + " ORDER BY tbljrnvdet.AccountId, tbljrnv.VoucherNumber"



        InsertM02AR22(Conn, AccountID, AccountType, CutOffDate, qry, qryrecv, qryrecv1, qryJrnl, UserID)

        Return True
    End Function

    Private Sub InsertM02AR22(conn As MySqlConnection, AccountID As String, AccountType As String, CutOffDate As String, Query As String, QueryRecv As String, QueryRecv1 As String, QueryJrnl As String, UserID As String)
        Try

            Dim sqlst, sqlst1, isWhere, isWhere1, sqlst2, isWhere2 As String
            Dim inIsWhere, inIsWhere1, inIsWhere2 As Integer

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
            isWhere2 = QueryJrnl
            InsertIntoTblWebEventLog("Journal", QueryJrnl, "SEN")
            inIsWhere2 = isWhere2.IndexOf("where")
            InsertIntoTblWebEventLog("Journal", inIsWhere2, "SEN")

            sqlst2 = "insert into tbwar22autoemail (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT tbljrnv.JournalDate, 'JOURNAL',  tbljrnv.VoucherNumber, 0, tblJrnvDet.debitbase-tblJrnvDet.Creditbase, tblJrnvDet.debitbase-tblJrnvDet.Creditbase, tblJrnvDet.contacttype, tblJrnvDet.AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' , tblJrnv.comments, tblJrnv.JournalDate,'" & Session("UserID") & "', tblJrnvDet.CustName,  tblJrnvdet.VoucherNumber, tblJrnvDet.Rcno, 'JRNV' from tblJrnv left outer join tblJrnvdet on tblJrnv.VoucherNumber=tblJrnvdet.VoucherNumber  "
            sqlst2 = sqlst2 + " " + QueryJrnl.Substring(inIsWhere2)


            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure
            '    command.CommandText = "SaveTbwAR22AutoEmail"
            command.CommandText = "SaveAutoTbwAR22New"
            command.Parameters.Clear()

            'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            'command.Parameters.AddWithValue("@pr_Query", txtQuery.Text)
            command.Parameters.AddWithValue("@pr_Query", sqlst)
            command.Parameters.AddWithValue("@pr_Query1", sqlst1)
            command.Parameters.AddWithValue("@pr_Query2", sqlst2)
            command.Parameters.AddWithValue("@pr_CreatedBy", UserID)
            command.Connection = conn
            command.ExecuteScalar()
            command.CommandTimeout = 0

            command.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("CustSOA - InserM02AR22", ex.Message.ToString, AccountID)

        End Try

    End Sub

    Private Sub MergePDF(ByVal File1 As String, ByVal File2 As String)
        InsertIntoTblWebEventLog("MergePDF", txtFileType.Text, lblRecordNo.Text)

        Dim fileArray As String() = New String(2) {}
        fileArray(0) = File1
        fileArray(1) = File2
        Dim reader As PdfReader = Nothing
        Dim sourceDocument As Document = Nothing
        Dim pdfCopyProvider As PdfCopy = Nothing
        Dim importedPage As PdfImportedPage
        Dim outputPdfPath As String = ""
        '  Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF")

        outputPdfPath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF")
        sourceDocument = New Document()
        pdfCopyProvider = New PdfCopy(sourceDocument, New System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create))
        sourceDocument.Open()
        '   InsertIntoTblWebEventLog("MergePDF", outputPdfPath, "")

        For f As Integer = 0 To fileArray.Length - 1 - 1
            Dim pages As Integer = TotalPageCount(fileArray(f))
            reader = New PdfReader(fileArray(f))

            For i As Integer = 1 To pages
                importedPage = pdfCopyProvider.GetImportedPage(reader, i)
                pdfCopyProvider.AddPage(importedPage)
            Next
            '  InsertIntoTblWebEventLog("MergePDF", f.ToString, pages.ToString)

            reader.Close()
        Next

        sourceDocument.Close()
    End Sub

    Private Shared Function TotalPageCount(ByVal file As String) As Integer
        Using sr As StreamReader = New StreamReader(System.IO.File.OpenRead(file))
            Dim regex As Regex = New Regex("/Type\s*/Page[^s]")
            Dim matches As MatchCollection = regex.Matches(sr.ReadToEnd())
            Return matches.Count
        End Using
    End Function

    Private Sub IndividualorMergedFile()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        ''''''''''''''''''''''''''''''''''''''''''''''''
        Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
        commandServiceRecordMasterSetup.CommandType = CommandType.Text
        commandServiceRecordMasterSetup.CommandText = "SELECT InvoiceSvcReportIndividualFile,InvoiceSvcReportMergedFile FROM tblservicerecordmastersetup where rcno=1"
        commandServiceRecordMasterSetup.Connection = conn

        Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
        Dim dtServiceRecordMasterSetup As New DataTable
        dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

        If String.IsNullOrEmpty(dtServiceRecordMasterSetup.Rows(0)("InvoiceSvcReportIndividualFile")) = False Then
            If dtServiceRecordMasterSetup.Rows(0)("InvoiceSvcReportIndividualFile") = 1 Then
                txtFileType.Text = "Individual"
            End If
        End If
        If String.IsNullOrEmpty(dtServiceRecordMasterSetup.Rows(0)("InvoiceSvcReportMergedFile")) = False Then
            If dtServiceRecordMasterSetup.Rows(0)("InvoiceSvcReportMergedFile") = 1 Then
                txtFileType.Text = "Merged"
            End If
        End If

        If String.IsNullOrEmpty(txtFileType.Text) Then
            txtFileType.Text = "Merged"
        End If

        commandServiceRecordMasterSetup.Dispose()
        dtServiceRecordMasterSetup.Dispose()
        drServiceRecordMasterSetup.Close()
        conn.Close()
        conn.Dispose()

    End Sub


    Private Function EmailValidation() As Boolean
        Dim check As Boolean = False

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        ''''''''''''''''''''''''''''''''''''''''''''''''
        Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
        commandServiceRecordMasterSetup.CommandType = CommandType.Text
        commandServiceRecordMasterSetup.CommandText = "SELECT EnableEmailValidation FROM tblservicerecordmastersetup where rcno=1"
        commandServiceRecordMasterSetup.Connection = conn

        Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
        Dim dtServiceRecordMasterSetup As New DataTable
        dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

        If String.IsNullOrEmpty(dtServiceRecordMasterSetup.Rows(0)("EnableEmailValidation")) = False Then
            If dtServiceRecordMasterSetup.Rows(0)("EnableEmailValidation") = 1 Then
                check = True
            Else
                check = False
            End If
        Else
            check = False
        End If


        commandServiceRecordMasterSetup.Dispose()
        dtServiceRecordMasterSetup.Dispose()
        drServiceRecordMasterSetup.Close()
        conn.Close()
        conn.Dispose()
        Return check

    End Function

    Protected Sub btnOkWarningMsg_Click(sender As Object, e As EventArgs) Handles btnOkWarningMsg.Click
        If rdbWarningOptions.SelectedIndex = 0 Then
            txtTo.Text = lblToEmail.Text
        ElseIf rdbWarningOptions.SelectedIndex = 1 Then

        Else
            '  MessageBox.Message.Alert(Page, "Please choose an option", "str")
            mdlPopupWarningMsg.Show()

        End If
    End Sub

    Private Function CheckPriceIncrease(conn As MySqlConnection, AccountID As String, InvoiceNumber As String, ContractNo As String, InvoiceDate As DateTime, Period As String) As Boolean
        Try
            '  InsertIntoTblWebEventLog("CheckPriceIncrease1", InvoiceDate.ToShortDateString, InvoiceNumber)


            Dim dt As New DateTime(Period.Substring(0, 4), Period.Substring(4, 2), 1)

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.Connection = conn
            ' command1.CommandText = "Select * from tblcontractpricemasschange where accountid='" + AccountID + "' and ContractNo = '" + ContractNo + "' and ProcessedOn <= '" + Convert.ToDateTime(InvoiceDate) + "'"
            command1.CommandText = "Select * from tblcontractpricehistory where ContractNo in ('" + ContractNo + "') and Type='price increase' and Date <= '" + InvoiceDate.ToString("yyyy-MM-dd") + "' and Date >= '" + dt.ToString("yyyy-MM-dd") + "' and ContractGroup='CP'"
            '  InsertIntoTblWebEventLog("CheckPriceIncrease2", InvoiceDate, Convert.ToDateTime(InvoiceDate).ToString("yyyy-MM-dd"))

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            InsertIntoTblWebEventLog("CheckPriceIncrease3", dt1.Rows.Count.ToString, InvoiceNumber)

            If dt1.Rows.Count > 0 Then
                '  InsertIntoTblWebEventLog("CheckSOALog", "Exists - EmailSent", AccountID)
                '    InsertIntoTblWebEventLog("CheckPriceIncrease4", InvoiceDate, Convert.ToDateTime(InvoiceDate).ToString("yyyy-MM-dd"))
                Return True

            End If
            command1.Dispose()
            dt1.Clear()
            dt1.Dispose()
            dr1.Close()

            Return False

        Catch ex As Exception
            InsertIntoTblWebEventLog("CheckPriceIncrease", ex.Message.ToString, AccountID)
            Return False

        End Try
    End Function

    Private Sub UpdateAdditionalAttachmentSentField(InvoiceNumber As String)
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim InvoiceDate As String = ""
            Dim AccountID As String = ""

            Dim ContractNo As String = ""


            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT invoicenumber,custname,SalesDate,accountid,contacttype,DUEDATE,location,createdby FROM tblsales where invoicenumber='" & lblRecordNo.Text & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then


                If dt1.Rows(0)("SalesDate").ToString = DBNull.Value.ToString Then
                Else
                    InvoiceDate = Convert.ToDateTime(dt1.Rows(0)("SalesDate")).ToString("dd/MM/yyyy")
                End If

                AccountID = dt1.Rows(0)("AccountID").ToString()

                'Retrieve ContractNo

                Dim commandContractNo As MySqlCommand = New MySqlCommand

                commandContractNo.CommandType = CommandType.Text

                commandContractNo.CommandText = "SELECT group_concat(distinct costcode separator ''',''') as ConcatContract from tblsalesdetail where invoicenumber='" & InvoiceNumber & "'"

                commandContractNo.Connection = conn

                Dim drContractNo As MySqlDataReader = commandContractNo.ExecuteReader()
                Dim dtContractNo As New DataTable
                dtContractNo.Load(drContractNo)

                If dtContractNo.Rows.Count > 0 Then
                    ContractNo = dtContractNo.Rows(0)("ConcatContract")
                End If

            End If

            InsertIntoTblWebEventLog("UpdateAdditionalSentField", AccountID, InvoiceNumber)

            Dim separator As String() = {"','"}
            Dim strlist As String() = ContractNo.Split(separator, StringSplitOptions.RemoveEmptyEntries)

            For Each s As String In strlist



                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblemailadditionalpriceincrease(AccountID,ContractNo,InvoiceNumber,"
                qry = qry + "InvoiceDate,AddtionalFile,PriceIncreasePercent,EmailSentOn,CreatedBy,CreatedOn)"
                qry = qry + "VALUES(@AccountID,@ContractNo,@InvoiceNumber,@InvoiceDate,@AddtionalFile,@PriceIncreasePercent,@EmailSentOn,@CreatedBy,@CreatedOn);"


                command.CommandText = qry
                command.Parameters.Clear()


                command.Parameters.AddWithValue("@AccountID", AccountID)
                command.Parameters.AddWithValue("@ContractNo", s)
                command.Parameters.AddWithValue("@InvoiceNumber", lblRecordNo.Text)
                Dim d As DateTime
                If Date.TryParseExact(InvoiceDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                End If

                command.Parameters.AddWithValue("@InvoiceDate", d.ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@AddtionalFile", "Y")

                command.Parameters.AddWithValue("@PriceIncreasePercent", 0)
                command.Parameters.AddWithValue("@EmailSentOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))


                command.Connection = conn

                command.ExecuteNonQuery()

                command.Dispose()

            Next

            conn.Close()
            conn.Dispose()
            'conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("UpdateAdditionalSentField", ex.Message.ToString, InvoiceNumber)
        End Try
    End Sub

    Protected Sub lnkPreview2_Click(sender As Object, e As EventArgs) Handles lnkPreview2.Click
        Dim FilePath As String = ""

        FilePath = "PDFs/" + lblRecordNo.Text + "/Price Increase Notification.PDF"

        iframeid.Attributes.Add("src", FilePath)
    End Sub

    Protected Sub ImageButton7_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton7.Click
        lnkPreview2.Text = ""
        ImageButton7.Visible = False
    End Sub

    Protected Sub btnYesAttchManualRpt_Click(sender As Object, e As EventArgs) Handles btnYesAttchManualRpt.Click
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim qry As String = "SELECT recordno,filename,servicedate,filenamelink,filedescription FROM tblfileupload LEFT JOIN TBLSERVICErecord on tblfileupload.fileref = tblservicerecord.recordno"
        qry = qry + " where manualreport='Y' and fileref in (SELECT RefType FROM tblsalesdetail where InvoiceNumber = '" + lblRecordNo.Text + "');"
        Dim check As Boolean = False
        'Start:Retrive Service Records
        Dim commandService As MySqlCommand = New MySqlCommand

        commandService.CommandType = CommandType.Text
        commandService.CommandText = qry
        commandService.Connection = conn

        Dim drService As MySqlDataReader = commandService.ExecuteReader()
        Dim dtService As New DataTable
        dtService.Load(drService)

        If dtService.Rows.Count > 0 Then


            'If Not Directory.Exists(Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString) Then
            '    Directory.CreateDirectory(Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString)
            'End If

            Dim filepath As String = ""

            For i = 0 To dtService.Rows.Count - 1

                If i = 0 Then
                    If lnkAttach1.Text = "" Then
                        '  FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString))
                        filepath = Server.MapPath("~/Uploads/Service/" + dtService.Rows(0)("FileNameLink").ToString)
                        lnkAttach1.Text = dtService.Rows(0)("FileNameLink").ToString
                        AttachCount = AttachCount + 1
                        ImageButton1.Visible = True
                        txtManualRpt.Text = "Yes"
                    End If

                ElseIf i = 1 Then
                    If lnkAttach2.Text = "" Then
                        filepath = ""
                        '  FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString))
                        filepath = Server.MapPath("~/Uploads/Service/" + dtService.Rows(1)("FileNameLink").ToString)
                        lnkAttach2.Text = dtService.Rows(1)("FileNameLink").ToString
                        AttachCount = AttachCount + 1
                        ImageButton2.Visible = True
                        txtManualRpt.Text = "Yes"
                    End If

                ElseIf i = 2 Then
                    If lnkAttach3.Text = "" Then
                        filepath = ""
                        '  FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString))
                        filepath = Server.MapPath("~/Uploads/Service/" + dtService.Rows(2)("FileNameLink").ToString)
                        lnkAttach3.Text = dtService.Rows(2)("FileNameLink").ToString
                        AttachCount = AttachCount + 1
                        ImageButton3.Visible = True
                        txtManualRpt.Text = "Yes"
                    End If

                ElseIf i = 3 Then
                    If lnkAttach4.Text = "" Then
                        filepath = ""
                        '  FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString))
                        filepath = Server.MapPath("~/Uploads/Service/" + dtService.Rows(3)("FileNameLink").ToString)
                        lnkAttach4.Text = dtService.Rows(3)("FileNameLink").ToString
                        AttachCount = AttachCount + 1
                        ImageButton4.Visible = True
                        txtManualRpt.Text = "Yes"
                    End If

                ElseIf i = 4 Then
                    If lnkAttach5.Text = "" Then
                        filepath = ""
                        '  FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString))
                        filepath = Server.MapPath("~/Uploads/Service/" + dtService.Rows(4)("FileNameLink").ToString)
                        lnkAttach5.Text = dtService.Rows(4)("FileNameLink").ToString
                        AttachCount = AttachCount + 1
                        ImageButton5.Visible = True
                        txtManualRpt.Text = "Yes"
                    End If

                ElseIf i = 5 Then
                    If lnkAttach6.Text = "" Then
                        filepath = ""
                        '  FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString))
                        filepath = Server.MapPath("~/Uploads/Service/" + dtService.Rows(3)("FileNameLink").ToString)
                        lnkAttach6.Text = dtService.Rows(3)("FileNameLink").ToString
                        AttachCount = AttachCount + 1
                        ImageButton8.Visible = True
                        txtManualRpt.Text = "Yes"
                    End If

                ElseIf i = 6 Then
                    If lnkAttach7.Text = "" Then
                        filepath = ""
                        '  FileUpload1.PostedFile.SaveAs((Server.MapPath("~/Uploads/Service/") + dtService.Rows(0)("FileNameLink").ToString))
                        filepath = Server.MapPath("~/Uploads/Service/" + dtService.Rows(3)("FileNameLink").ToString)
                        lnkAttach7.Text = dtService.Rows(3)("FileNameLink").ToString
                        AttachCount = AttachCount + 1
                        ImageButton9.Visible = True
                        txtManualRpt.Text = "Yes"
                    End If
                End If

                'ElseIf lnkAttach2.Text = "" Then
                '    lnkAttach2.Text = fileName
                '    AttachCount = AttachCount + 1
                '    ImageButton2.Visible = True

                'ElseIf lnkAttach3.Text = "" Then
                '    lnkAttach3.Text = fileName
                '    AttachCount = AttachCount + 1
                '    ImageButton3.Visible = True

                'ElseIf lnkAttach4.Text = "" Then
                '    lnkAttach4.Text = fileName
                '    AttachCount = AttachCount + 1
                '    ImageButton4.Visible = True

                'ElseIf lnkAttach5.Text = "" Then
                '    lnkAttach5.Text = fileName
                '    AttachCount = AttachCount + 1
                '    ImageButton5.Visible = True
                'Else
                '    mdlMsg.Show()

                '    lblMsg.Text = "CANNOT ATTACH MORE THAN 5 FILES"

            Next

        End If
        conn.Close()
        conn.Dispose()

    End Sub

    Protected Sub ImageButton8_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton8.Click
        lnkAttach6.Text = ""
        ImageButton8.Visible = False
        AttachCount = AttachCount - 1
    End Sub

    Protected Sub ImageButton9_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton9.Click
        lnkAttach7.Text = ""
        ImageButton9.Visible = False
        AttachCount = AttachCount - 1
    End Sub

    Protected Sub lnkAttach6_Click(sender As Object, e As EventArgs) Handles lnkAttach6.Click
        Dim FilePath As String = ""

        If txtManualRpt.Text = "Yes" Then
            FilePath = "Uploads/Service/" + lnkAttach6.Text
        Else
            FilePath = "Uploads/Email/" + Convert.ToString(Session("UserID")) + "/" + lnkAttach6.Text
        End If
        PreviewFile(FilePath, lnkAttach6.Text)
        '  iframeid.Attributes.Add("src", FilePath)
        ImageButton8.Visible = True
    End Sub

    Protected Sub lnkAttach7_Click(sender As Object, e As EventArgs) Handles lnkAttach7.Click
        Dim FilePath As String = ""

        If txtManualRpt.Text = "Yes" Then
            FilePath = "Uploads/Service/" + lnkAttach7.Text
        Else
            FilePath = "Uploads/Email/" + Convert.ToString(Session("UserID")) + "/" + lnkAttach7.Text
        End If
        PreviewFile(FilePath, lnkAttach7.Text)
        '  iframeid.Attributes.Add("src", FilePath)
        ImageButton9.Visible = True
    End Sub
End Class
