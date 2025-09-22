
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Partial Class RV_Export_DebitNote
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                If Convert.ToString(Session("ReportType")) = "DebitNoteListingByClient_Details" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingByClient_Details.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingByClient_Summary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingByClient_Summary.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingByDocumentNo_Details" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingByDocumentNo_Details.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingByDocumentNo_Summary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingByDocumentNo_Summary.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingByCompanyGrp_Details" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingByCompanyGrp_Details.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingByCompanyGrp_Summary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingByCompanyGrp_Summary.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingByGLCode_Details" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingByGLCode_Details.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingByGLCode_Summary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingByGLCode_Summary.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingBySalesPerson_Details" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingBySalesPerson_Details.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "DebitNoteListingBySalesPerson_Summary" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/DebitNoteReports/DebitNoteListingBySalesPerson_Summary.rpt"))

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



                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_DN.PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=DN.pdf")
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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_DN.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
