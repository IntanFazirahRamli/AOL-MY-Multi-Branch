Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO

Public Class RV_SchServiceVsCompService
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                If Convert.ToString(Session("ReportType")) = "SchServiceVsCompService" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/SchServiceVsCompService.rpt"))
                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                    crReportDocument.SetParameterValue("Selection", Convert.ToString(Session("selFormula")), crReportDocument.Subreports(0).Name.ToString)

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

                    crReportDocument.Subreports.Item(0).RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                    CrystalReportViewer1.ReportSource = crReportDocument

                    CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))


                ElseIf Convert.ToString(Session("ReportType")) = "SchServiceVsCompServiceZone" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/SchServiceVsCompService_Zone.rpt"))
                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                    crReportDocument.SetParameterValue("Selection", Convert.ToString(Session("selFormula")), crReportDocument.Subreports(0).Name.ToString)

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

                    crReportDocument.Subreports.Item(0).RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                    CrystalReportViewer1.ReportSource = crReportDocument

                    CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))


                ElseIf Convert.ToString(Session("ReportType")) = "SchServiceVsCompServiceCompGrpDet" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/SchServiceVsCompService_CompanyGrpDet.rpt"))
                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                    '  crReportDocument.SetParameterValue("Selection", Convert.ToString(Session("selFormula")), crReportDocument.Subreports(0).Name.ToString)

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

                    '   crReportDocument.Subreports.Item(0).RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                    CrystalReportViewer1.ReportSource = crReportDocument

                    CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))


                ElseIf Convert.ToString(Session("ReportType")) = "SchServiceVsCompServiceCompGrpSumm" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/PortfolioReports/SchServiceVsCompService_CompanyGrpSumm.rpt"))
                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                    '  crReportDocument.SetParameterValue("Selection", Convert.ToString(Session("selFormula")), crReportDocument.Subreports(0).Name.ToString)

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

                    '   crReportDocument.Subreports.Item(0).RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                    CrystalReportViewer1.ReportSource = crReportDocument

                    CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))


                End If

                'crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                'crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                'crReportDocument.SetParameterValue("Selection", Convert.ToString(Session("selFormula")), crReportDocument.Subreports(0).Name.ToString)




            Else
                CrystalReportViewer1.RefreshReport()
            End If
        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"

            Using w As StreamWriter = File.AppendText(ErrOtLo)
                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
            End Using

        End Try
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        Response.Redirect("Reports.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class