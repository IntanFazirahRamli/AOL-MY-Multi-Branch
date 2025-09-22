
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net


Partial Class RV_Export_SalesPerformance
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                If Convert.ToString(Session("ReportType")) = "SalesPerformanceDetailsByClient" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceDetailsByClient.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceDetailsByCompanyGrp" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceDetailsByCompanyGrp.rpt"))
               
                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceDetailsByDepartment" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceDetailsByDepartment.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceDetailsByMarketSegment" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceDetailsByMarketSegment.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceDetailsBySalesman" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceDetailsBySalesman.rpt"))
                    '  crReportDocument.SetParameterValue("pPrintDate", DateTime.Now.ToShortDateString)

                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceSummaryByClient" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceSummaryByClient.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceSummaryByCompanyGrp" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceSummaryByCompanyGrp.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceSummaryByDepartment" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceSummaryByDepartment.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceSummaryByMarketSegment" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceSummaryByMarketSegment.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SalesPerformanceSummaryBySalesman" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/SalesPerformanceReports/SalesPerformaceSummaryBySalesman.rpt"))

                End If

                crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))


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

                crReportDocument.RecordSelectionFormula = Convert.ToString(Session("selFormula"))



                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_SalesPerformance.PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=SalesPerformance.pdf")
                    Response.BinaryWrite(FileBuffer)
                End If
                '    End If


            End If

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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_SalesPerformance.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
