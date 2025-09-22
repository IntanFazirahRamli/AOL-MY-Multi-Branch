Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Imports MySql.Data
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text

Public Class RV_ReceiptConfirmation
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()


    Dim expo As New ExportOptions


    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Try
        If Not IsPostBack Then

            Dim RecordNo As String = Convert.ToString(Session("ReceiptNumber"))
            lblRecordNo.Text = RecordNo
          
            crReportDocument.Load(Server.MapPath("~/Reports/ARReports/ReceiptConfirmation.rpt"))

            '  crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("Title")))

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

            If Request.QueryString("Export") = "PDF" Then

                crReportDocument.RecordSelectionFormula = "{m02Recv.ReceiptNumber}='" + lblRecordNo.Text + "'"

                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=" & lblRecordNo.Text & ".pdf")
                    Response.BinaryWrite(FileBuffer)
                End If



            ElseIf Request.QueryString("Export") = "Word" Then


                crReportDocument.RecordSelectionFormula = "{m02Recv.ReceiptNumber}='" + lblRecordNo.Text + "'"

                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".doc"))

                If File.Exists(FilePath) Then
                    File.Delete(FilePath)

                End If
                oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

                expo = crReportDocument.ExportOptions

                expo.ExportDestinationType = ExportDestinationType.DiskFile

                expo.ExportFormatType = ExportFormatType.WordForWindows

                expo.DestinationOptions = oDfDopt

                '    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

                crReportDocument.Export()

                Dim User As New WebClient()
                Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)
                If FileBuffer IsNot Nothing Then
                    Response.ContentType = "application/doc"
                    Response.AddHeader("content-length", FileBuffer.Length.ToString())
                    Response.AddHeader("content-disposition", "inline; filename=" & lblRecordNo.Text & ".doc")
                    Response.BinaryWrite(FileBuffer)
                End If
            End If
        End If

        '     lblRecordNo.Text = "SERV201701-010826"

        'Catch ex As Exception
        '    Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"

        '    Using w As StreamWriter = File.AppendText(ErrOtLo)
        '        w.WriteLine(ex.Message.ToString + vbLf & vbLf)
        '    End Using

        'End Try
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        Response.Redirect("Receipts.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".PDF"))


        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
