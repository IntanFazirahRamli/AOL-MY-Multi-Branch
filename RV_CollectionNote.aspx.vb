
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO
Imports System.Net


Partial Class RV_CollectionNote
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()


    Dim expo As New ExportOptions


    Dim oDfDopt As New DiskFileDestinationOptions
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Try
        If Not IsPostBack Then

            Dim RecordNo As String = Convert.ToString(Session("ReceiptNumber"))
            lblRecordNo.Text = RecordNo

            crReportDocument.Load(Server.MapPath("~/Reports/ARReports/CollectionNote.rpt"))

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
            '     lblRecordNo.Text = "SERV201701-010826"
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
        End If
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
