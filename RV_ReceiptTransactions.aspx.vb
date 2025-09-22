
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net
Imports MySql.Data
Imports System.Data



Partial Class RV_ReceiptTransactions
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        InsertIntoTblWebEventLog("Page_load", Convert.ToString(Session("AccountID")), txtAccountID.Text)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            InsertIntoTblWebEventLog("Page_load", Convert.ToString(Session("AccountID")), txtAccountID.Text)

            '  If Not IsPostBack Then
            Dim AccountID As String = Convert.ToString(Session("AccountID"))
            txtAccountID.Text = AccountID

            '   btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

            crReportDocument.Load(Server.MapPath("~/Reports/ARReports/ReceiptTransaction.rpt"))
            crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
            crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))

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
            crReportDocument.RecordSelectionFormula = "{Command.AccountId}='" + txtAccountID.Text + "'"

            Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + txtAccountID.Text + "_Transaction.PDF"))

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
                Response.AddHeader("content-disposition", "inline; filename=" & txtAccountID.Text & "_TransactionSummary.pdf")

                Response.BinaryWrite(FileBuffer)
            End If


        Catch ex As Exception
            InsertIntoTblWebEventLog("Page_load", ex.Message.ToString, txtAccountID.Text)

            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)
                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
            End Using
        End Try
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + txtAccountID.Text + "_Transaction.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        '  Response.Redirect("Reports.aspx")
    End Sub

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "RECEIPT TRANSACTIONS - " + Session("UserID"))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, txtAccountID.Text)
        End Try
    End Sub
End Class

