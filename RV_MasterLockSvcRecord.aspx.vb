Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO

Partial Class RV_MasterLockSvcRecord
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim repPath As String = Server.MapPath("~/Reports/AdministrationReports/Rpt_Master_LockServiceRecord.rpt")
                crReportDocument.Load(repPath)
                Dim selectFormula As String = "{tblTarget1.RcNo} <> 0"
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
        Response.Redirect("LockServiceRecord.aspx")
    End Sub
End Class
