
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net
Imports System.Data


Partial Class RV_Export_InvoicePrinting
    Inherits System.Web.UI.Page


    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")


                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format1.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))

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

                '   If Request.QueryString("Type") = "Print" Then
                crReportDocument.RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                'ElseIf Request.QueryString("Type") = "Invoice" Then
                '    crReportDocument.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                'End If



                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_InvoicePrinting.PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=InvoicePrinting.pdf")
                    Response.BinaryWrite(FileBuffer)
                End If
                '    End If

                UpdatePrintCount()

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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_ServiceRecordPrinting.PDF"))

        If File.Exists(FilePath) Then
            '      File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Private Sub UpdatePrintCount()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()


        Dim qry As String
        qry = ""
        'Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.Text

        command.CommandText = Convert.ToString(Session("Query"))
        command.Parameters.Clear()
        command.Connection = conn
        command.ExecuteNonQuery()

        conn.Close()
        conn.Dispose()
        command.Dispose()
    End Sub
End Class
