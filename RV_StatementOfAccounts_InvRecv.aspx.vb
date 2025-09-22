
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net
Imports System.Data

Partial Class RV_StatementOfAccounts_InvRecv
    Inherits System.Web.UI.Page


    Public crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Label1.Text = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "' AND {vwcustomermainbillinginfo1.SendStatement}=1"
        'Label1.Text = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "' AND {vwcustomermainbillinginfo1.SendStatement}=True and {m02AR22.Balance} <> 0 "


        'Convert.ToString(Session("selformula"))
        Try
            '  If Not IsPostBack Then
            btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")
            UnLockReport()
            If Request.QueryString("Type") = "DEBIT" Then
                Label1.Text = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "' AND {vwcustomermainbillinginfo1.SendStatement}=True and {m02AR22.Balance} <> 0 "

                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcInvRecvBaseCur.rpt"))
                crReportDocument.SetParameterValue("pBal", "DEBIT")
                Session.Add("Type", "DEBIT")
            ElseIf Request.QueryString("Type") = "ZERO" Then
                Label1.Text = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "' AND {vwcustomermainbillinginfo1.SendStatement}=True and {m02AR22.Balance} <= 0 "

                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcInvRecvBaseCur.rpt"))
                crReportDocument.SetParameterValue("pBal", "ZERO")
                Session.Add("Type", "ZERO")
            End If
            'crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
            'crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))
            crReportDocument.SetParameterValue("pStatementDate", Convert.ToDateTime(Session("PrintDate")))




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
            CrystalReportViewer1.ReportSource = crReportDocument

            '    CrystalReportViewer1.SelectionFormula = Label1.Text
            CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))

            Session.Add("ReportType", "InvRecv")



            'Else
            'CrystalReportViewer1.RefreshReport()
            'End If
        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)
                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
            End Using

            UnLockReport()
        End Try
    End Sub
    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        ' Response.Redirect("Reports.aspx")
    End Sub


    Private Sub UnLockReport()
        ' Locked?


        Dim connUnLock As MySqlConnection = New MySqlConnection()
        Dim commandUnLock As MySqlCommand = New MySqlCommand

        connUnLock.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connUnLock.Open()

        Dim strSqlUnLock As String = "UPDATE tbllock SET  EndDateTime = @EndDateTime, Status= @Status where Reportname = @ReportName and Status = 'Locked'"

        commandUnLock.CommandType = CommandType.Text
        commandUnLock.CommandText = strSqlUnLock
        commandUnLock.Parameters.Clear()

        'commandUnLock.Parameters.AddWithValue("@RunBy", Session("UserID"))
        commandUnLock.Parameters.AddWithValue("@ReportName", "SOA-Report")
        commandUnLock.Parameters.AddWithValue("@Status", "UnLocked")
        commandUnLock.Parameters.AddWithValue("@EndDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        commandUnLock.Connection = connUnLock
        commandUnLock.ExecuteNonQuery()

        connUnLock.Close()
        connUnLock.Dispose()
        commandUnLock.Dispose()

        'Locked? 
    End Sub
    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        '    crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/SalesInvListingReports/SalesInvListingDetailByInvNo.rpt"))

        '    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
        '    crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))

        '    Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
        '    Dim myTables As Tables = crReportDocument.Database.Tables

        '    For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
        '        Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
        '        myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
        '        myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
        '        myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
        '        myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")
        '        myTable.ApplyLogOnInfo(myTableLogonInfo)
        '        myTableLogonInfo.ConnectionInfo = myConnectionInfo
        '    Next
        '    crReportDocument.RecordSelectionFormula = Convert.ToString(Session("selFormula"))

        ''    crReportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, True, "SalesInvoiceListing")

        '    Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_SalesInvListing.xls"))

        '    '   Dim FilePath As String = "C:/Downloaded Excel Files/" + Convert.ToString(Session("UserID") + "_SalesInvListingDetail.xls")

        '    'Dim path1 As String = "C:\Downloaded Excel Files"

        '    'If Not Directory.Exists(Path1) Then

        '    '    Directory.CreateDirectory(path1)
        '    'End If


        '    If File.Exists(FilePath) Then
        '        File.Delete(FilePath)

        '    End If
        '    oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

        '    expo = crReportDocument.ExportOptions

        '    expo.ExportDestinationType = ExportDestinationType.DiskFile

        '    expo.ExportFormatType = ExportFormatType.Excel

        '    expo.DestinationOptions = oDfDopt

        '    '    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

        '    crReportDocument.Export()

        '    Dim User As New WebClient()
        '    Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)
        '    If FileBuffer IsNot Nothing Then
        '        Response.ContentType = "application/xls"
        '        Response.AddHeader("content-length", FileBuffer.Length.ToString())
        '        Response.AddHeader("content-disposition", "inline; filename=SalesInvListing.xls")
        '        Response.BinaryWrite(FileBuffer)
        '    End If

        '    '  MessageBox.Message.Alert(Page, "File saved in location " + Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_SalesInvListing.xls")), "str")
    End Sub

    Protected Sub btnPrintPDF_Click(sender As Object, e As EventArgs) Handles btnPrintPDF.Click
        'crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/SalesInvListingReports/SalesInvListingDetailByInvNo.rpt"))

        'crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
        'crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))

        'Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
        'Dim myTables As Tables = crReportDocument.Database.Tables

        'For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
        '    Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
        '    myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
        '    myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
        '    myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
        '    myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")
        '    myTable.ApplyLogOnInfo(myTableLogonInfo)
        '    myTableLogonInfo.ConnectionInfo = myConnectionInfo
        'Next

        'crReportDocument.PrintToPrinter(1, False, 0, 0)
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        crReportDocument.Close()
        crReportDocument.Dispose()

    End Sub

    Protected Sub CrystalReportViewer1_Init(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Init

    End Sub
End Class

