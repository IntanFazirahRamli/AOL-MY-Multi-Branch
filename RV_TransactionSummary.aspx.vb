Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Imports System.Drawing
Imports System.Data
Imports MySql.Data

Partial Class RV_TransactionSummary
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '   Try
        '  If Not IsPostBack Then
        Dim AccountID As String = Convert.ToString(Session("AccountID"))
        txtAccountID.Text = AccountID
        txtCutOffDate.Text = Convert.ToString(Session("cutoffoscustomer"))
        '''''''''''''''''''''''
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = "SaveTbwARDetail1TransSummary"
        command.Parameters.Clear()

        'DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")
        'command.Parameters.AddWithValue("@pr_CutOffdate", DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-GB")))
        command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

        command.Parameters.AddWithValue("@pr_AccountType", "")
        command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountID.Text.Trim)
        command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountID.Text.Trim)


        command.Connection = conn
        command.ExecuteScalar()
        conn.Close()
        conn.Dispose()
        command.Dispose()

        '''''''''''''''''''''''''''''''''''''
       

        crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TransactionSummary.rpt"))
        crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
        crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))

        'crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
        'crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))
        'crReportDocument.SetParameterValue("pCutOffDate", DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-GB")))
        crReportDocument.SetParameterValue("pCutOffDate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))

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
        crReportDocument.RecordSelectionFormula = "{Command.AccountId}='" + txtAccountID.Text + "' and {Command.CreatedBy} = '" & Convert.ToString(Session("UserId")) & "'"

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


        'Catch ex As Exception
        '    Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
        '    Using w As StreamWriter = File.AppendText(ErrOtLo)
        '        w.WriteLine(ex.Message.ToString + vbLf & vbLf)
        '    End Using
        'End Try
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
End Class

