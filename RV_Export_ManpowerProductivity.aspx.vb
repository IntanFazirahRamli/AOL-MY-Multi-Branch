
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net


Partial Class RV_Export_ManpowerProductivity
    Inherits System.Web.UI.Page


    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                If Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberDetail" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberDetail.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamLeadSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamLeadSummary.rpt"))
                 
                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamLeadDetail" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamLeadDetail.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberSummary.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberSummaryByTeamMember" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberSummaryByTeamMember.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberOTDetail" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberOTDetail.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberOTSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberOTSummary.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberOTDetail_Client" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberOTDetail_Client.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberOTSummary_Client" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberOTSummary_Client.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberOTDetail_StaffDept" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberOTDetail_StaffDept.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberOTSummary_StaffDept" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberOTSummary_StaffDept.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberNormalDetail" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberNormalDetail.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberNormalSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberNormalSummary.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberNormalOTDetail" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberNormalOTDetail.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ManpowerProductivityByTeamMemberNormalOTSummary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberNormalOTSummary.rpt"))

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



                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Productivity.PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=Productivity.pdf")
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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Productivity.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
