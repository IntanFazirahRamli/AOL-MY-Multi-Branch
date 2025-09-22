
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO


Partial Class RV_ServiceRecordBilling
    Inherits System.Web.UI.Page
    Dim crReportDocument As New ReportDocument()
    '  Shared prevPage As String = [String].Empty

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")
                '      prevPage = Request.UrlReferrer.ToString()
                '    MessageBox.Message.Alert(Page, Convert.ToString(Session("selFormula")), "str")

                ' txtServiceRecordNo.Text = Convert.ToString(Session("JobNo"))
                '      Dim repPath As String = Server.MapPath("~/24ServiceRecord02.rpt")
                Dim repPath As String = Server.MapPath("~/Reports/ServiceRecordReports/ServiceRecordBilling.rpt")

                crReportDocument.Load(repPath)
                crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                crReportDocument.SetParameterValue("pSort5By", Convert.ToString(Session("sort5")))

                crReportDocument.SetParameterValue("pSelection", Convert.ToString(Session("selection")))
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
                CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))

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

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click

        '  Response.Redirect(prevPage)
        '  Response.Redirect("Reports.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
