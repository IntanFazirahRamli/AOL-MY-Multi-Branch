
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO


Partial Class RV_MasterServiceAction
    Inherits System.Web.UI.Page


    Dim crReportDocument As New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim repPath As String = Server.MapPath("~/Reports/AdministrationReports/Rpt_Master_ServiceAction.rpt")
                crReportDocument.Load(repPath)
                Dim selectFormula As String = "{tblServiceaction1.RcNo} <> 0"
                CrystalReportViewer1.SelectionFormula = selectFormula

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

                CrystalReportViewer1.ReportSource = crReportDocument
                'Dim orpt As CrystalDecisions.CrystalReports.Engine.ReportDocument
                'orpt = DirectCast(CrystalReportViewer1.ReportSource, CrystalDecisions.CrystalReports.Engine.ReportDocument)
                'orpt.ExportToDisk(ExportFormatType.PortableDocFormat, "D:\1_CWBInfotech\A_Sitapest\Program\Sitapest\PDFs\PdfFileName.pdf")

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
        Response.Redirect("Master_ServiceAction.aspx")
    End Sub

    Protected Sub btnQuit0_Click(sender As Object, e As EventArgs) Handles btnQuit0.Click
        Dim repPath As String = Server.MapPath("~/Reports/AdministrationReports/Rpt_Master_City.rpt")
        crReportDocument.Load(repPath)
        Dim selectFormula As String = "{tblSettleType1.RcNo} <> 0"
        CrystalReportViewer1.SelectionFormula = selectFormula

        crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))

        'crReportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, True, "city")

        'crReportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, True, "city")
    End Sub
End Class