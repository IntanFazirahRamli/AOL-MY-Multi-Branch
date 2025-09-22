Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Partial Class RV_TransactionSummaryCutOff
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim dt As Date = Convert.ToDateTime(Session("CutOffDate"))


                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")
                If Session("ReportType") = "NewTransSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TransactionSummaryCutOffNew.rpt"))
                Else
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TransactionSummaryCutOff.rpt"))
                End If

                 crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))
                crReportDocument.SetParameterValue("pCutOffDate", Session("CutOffDate"))
                crReportDocument.SetParameterValue("pContractNo", Session("ContractNo"))

                Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
                Dim myTables As Tables = crReportDocument.Database.Tables

                myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
                myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
                myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")

                For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                    Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo

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

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
       
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        '  Response.Redirect("Reports.aspx")
    End Sub
End Class

