
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Partial Class RV_Export_Revenue
    Inherits System.Web.UI.Page


    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                If Convert.ToString(Session("ReportType")) = "RevenueRptByTeamSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByTeamSummary.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByTeamDetail" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByTeamDetail_withSubRpt.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByPostalCode" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByPostalCode_withSubRpt.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByPostalCodeSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByPostalCode_Summary.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByDate" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByDate_withSubRpt.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByDateSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByDate_Summary.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))


                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByClient" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByClient_withSubRpt.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByClientSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByClient_Summary.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByAccountCode" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByAccountCode_withSubRpt.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptForAccountsByIndustry_Summary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueRptForAccountsByIndustry_Summary.rpt"))
                 
                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptForAccountsByIndustry" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueRptForAccountsByIndustry.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptForAccountsByContractGrpAndBillFreq_Format2" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueRptForAccountsByContractGrpAndBillFreq_Format2.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptForAccountsByContractGrpAndBillFreq_Format1" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueRptForAccountsByContractGrpAndBillFreq_Format1.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptForAccountsByContractGrp" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueRptForAccountsByContractGrp.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ZeroValueService" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/ZeroValueService.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByContractGrpIndustry" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByContractGroup&Industry.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "RevenueRptByZoneSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/RevenueReports/RevenueReportByZone.rpt"))
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("rptTitle")))

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



                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Revenue.PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=Revenue.pdf")
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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Revenue.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
