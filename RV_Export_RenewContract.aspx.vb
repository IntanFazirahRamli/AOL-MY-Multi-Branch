
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net


Partial Class RV_Export_RenewContract
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                If Convert.ToString(Session("ReportType")) = "DueForRenewalContract01" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/DueForRenewalContract01.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "DueForRenewalContract02" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/DueForRenewalContract02.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "DueForRenewalContract03" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/DueForRenewalContract03.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "RenewalContract01" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/RenewalContract01.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "RenewalContract02" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/RenewalContract02.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "RenewalContract03" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/RenewalContract03.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "RenewedContract01" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/RenewedContract01.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "RenewedContract02" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/RenewedContract02.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "RenewedContract03" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/RenewedContract03.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "ExtensionContract01" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ExtensionContract01.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "ExtensionContract02" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ExtensionContract02.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "ExtensionContract03" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ExtensionContract03.rpt"))

                ElseIf Convert.ToString(Session("ReportType")) = "ContinuousContract01" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContinuousContract01.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "ContinuousContract02" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContinuousContract02.rpt"))
                ElseIf Convert.ToString(Session("ReportType")) = "ContinuousContract03" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContinuousContract03.rpt"))

                End If

                crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                crReportDocument.SetParameterValue("pSort5By", Convert.ToString(Session("sort5")))

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



                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_ServiceContract.PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=ServiceContract.pdf")
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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_ServiceContract.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
