
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net


Partial Class RV_JournalListing
    Inherits System.Web.UI.Page


    Public crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '  If Not IsPostBack Then
            btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

            If Request.QueryString("Type") = "Summary" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/JournalReports/JournalVoucherListing.rpt"))

                'crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                'crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                'crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))

                crReportDocument.SetParameterValue("pSort1By", "")
                crReportDocument.SetParameterValue("pSort2By", "")
                crReportDocument.SetParameterValue("pSort3By", "")
                Session.Add("ReportType", "Summary")

            ElseIf Request.QueryString("Type") = "Detail" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/JournalReports/JournalVoucherDetailListing.rpt"))
                Session.Add("ReportType", "Detail")
            ElseIf Request.QueryString("Type") = "DetailLedger" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/JournalReports/JournalDetailLedgerSummary.rpt"))
                Session.Add("ReportType", "DetailLedger")
            End If
            crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
            crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))

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

            CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))


        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)
                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
            End Using
        End Try
    End Sub
    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        ' Response.Redirect("Reports.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        crReportDocument.Close()
        crReportDocument.Dispose()

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
End Class