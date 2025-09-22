Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO

Public Class RV_RevenueRptForAccountsByContractGrpAndBillFreq_Format1
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                Dim repPath As String = Server.MapPath("~/Reports/RevenueReports/RevenueRptForAccountsByContractGrpAndBillFreq_Format1.rpt")
                crReportDocument.Load(repPath)
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

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        '  Response.Redirect("Reports.aspx")
    End Sub
End Class