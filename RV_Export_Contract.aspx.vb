
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net


Partial Class RV_Export_Contract
    Inherits System.Web.UI.Page


    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                If Convert.ToString(Session("ReportType")) = "Contract01" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/Contract01.rpt"))

                    crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                    crReportDocument.SetParameterValue("pSort5By", Convert.ToString(Session("sort5")))
                    crReportDocument.SetParameterValue("pSort6By", Convert.ToString(Session("sort6")))
                    crReportDocument.SetParameterValue("pSort7By", Convert.ToString(Session("sort7")))

                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))

                ElseIf Convert.ToString(Session("ReportType")) = "Contract01_Incharge" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/Contract01_Incharge.rpt"))

                    crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                    crReportDocument.SetParameterValue("pSort5By", Convert.ToString(Session("sort5")))

                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))


                ElseIf Convert.ToString(Session("ReportType")) = "Contract01_SupportStaff" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/Contract01_SupportStaff.rpt"))

                    crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                    crReportDocument.SetParameterValue("pSort5By", Convert.ToString(Session("sort5")))

                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))


                ElseIf Convert.ToString(Session("ReportType")) = "Contract02" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/Contract02.rpt"))

                    crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                    crReportDocument.SetParameterValue("pSort5By", Convert.ToString(Session("sort5")))
                    crReportDocument.SetParameterValue("pSort6By", Convert.ToString(Session("sort6")))
                    crReportDocument.SetParameterValue("pSort7By", Convert.ToString(Session("sort7")))

                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))

                ElseIf Convert.ToString(Session("ReportType")) = "Contract02_Incharge" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/Contract02_Incharge.rpt"))

                    crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))
                    crReportDocument.SetParameterValue("pSort5By", Convert.ToString(Session("sort5")))

                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))

                ElseIf Convert.ToString(Session("ReportType")) = "ContractWithoutPriceChange" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContractWithoutPriceChange.rpt"))

                    crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))

                End If
             

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
