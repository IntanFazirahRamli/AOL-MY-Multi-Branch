
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Partial Class RV_Export_Portfolio
    Inherits System.Web.UI.Page


    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                If Convert.ToString(Session("ReportType")) = "ContractGroupValueReport" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/ContractGroupValueReport.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SchServiceVsCompService" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/SchServiceVsCompService.rpt"))
                    crReportDocument.SetParameterValue("Selection", Convert.ToString(Session("selFormula")), crReportDocument.Subreports(0).Name.ToString)

                ElseIf Convert.ToString(Session("ReportType")) = "SchServiceVsCompServiceZone" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/SchServiceVsCompService_Zone.rpt"))
                    crReportDocument.SetParameterValue("Selection", Convert.ToString(Session("selFormula")), crReportDocument.Subreports(0).Name.ToString)

                ElseIf Convert.ToString(Session("ReportType")) = "SchServiceVsCompServiceCompGrpDet" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/SchServiceVsCompService_CompanyGrpDet.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "SchServiceVsCompServiceCompGrpSumm" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/SchServiceVsCompService_CompanyGrpSumm.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ServiceDateSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/ServiceDateSummaryByContractGrp.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioMovementDetail" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioMovementDetail.rpt"))
                    crReportDocument.SetParameterValue("pOpeningDetail", Convert.ToBoolean(Session("OpeningDetail")))

                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioMovementSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioMovementSummary.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioMovementSummaryByContactType" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioMovementSummaryByContactType.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioValueSummaryByContGrpCategory" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioValueSummaryByContGrpCategory.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioValueSummaryByContGrpCategoryByContactType" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioValueSummaryByContGrpCategoryByContactType.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioValueSummaryByReportGrpCategory" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioValueSummaryByReportGrpCategory.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioValueSummaryByReportGrpCategoryByContactType" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioValueSummaryByReportGrpCategoryByContactType.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioDateSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioDateSummary.rpt"))
                   
                ElseIf Convert.ToString(Session("ReportType")) = "PortfolioDateSummaryGraph" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/PortfolioDateSummaryGraph.rpt"))

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
                'If Convert.ToString(Session("ReportType")) = "PortfolioDateSummary" Then
                'ElseIf Convert.ToString(Session("ReportType")) = "PortfolioDateSummaryGraph" Then

                'Else
                crReportDocument.RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                ' End If
                If Convert.ToString(Session("ReportType")) = "SchServiceVsCompService" Then

                    crReportDocument.Subreports.Item(0).RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                End If


                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Portfolio.PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=Portfolio.pdf")
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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Portfolio.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
