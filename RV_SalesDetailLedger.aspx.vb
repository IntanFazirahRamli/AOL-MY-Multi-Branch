

Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Partial Class RV_SalesDetailLedger
    Inherits System.Web.UI.Page

    Public crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions
    Shared prevPage As String = [String].Empty


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '  If Not IsPostBack Then
            '   btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")


            If Not IsPostBack Then
                prevPage = Request.UrlReferrer.ToString()
            End If
            If Convert.ToString(Session("Distribution")) = "YES" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/SalesInvListingReports/SalesInvoiceLedgerDetailDistribution.rpt"))

            Else
                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/SalesInvListingReports/SalesInvoiceLedgerDetail.rpt"))

            End If
             crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
            crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))
            crReportDocument.SetParameterValue("pSort1By", "")
            crReportDocument.SetParameterValue("pSort2By", "")
            crReportDocument.SetParameterValue("pSort3By", "")

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

            Session.Add("ReportType", "Detail")

        
            'Else
            'CrystalReportViewer1.RefreshReport()
            'End If
        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)
                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
            End Using
        End Try
    End Sub
    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        crReportDocument.Close()
        crReportDocument.Dispose()
        Response.Redirect(prevPage)

        ' Response.Redirect("Reports.aspx")
        '    
        '  ScriptManager.RegisterStartupScript(Page,TypeOf(Page),"str","history.back(); return false;",true)
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script>window.history.back(); return false;</Script>", False)
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        crReportDocument.Close()
        crReportDocument.Dispose()

    End Sub
End Class
