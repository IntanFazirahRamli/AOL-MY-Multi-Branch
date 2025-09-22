
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO
Imports System.Net

Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web


Partial Class RV_BatchInvoice
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()


    Dim expo As New ExportOptions


    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lBillSchedule As String
        Try
            If Not IsPostBack Then


                lBillSchedule = ""
                lBillSchedule = Convert.ToString(Session("BillSchedule"))

                Dim lInvoiceNo As String = ""
                Dim i As Long = 0

                ''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim sql As String
                sql = ""
                sql = "Select InvoiceNumber, PrintCounter from tblSales where BillSchedule = '" & lBillSchedule & "'"

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text
                command1.CommandText = sql
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()

                Dim dt As New DataTable
                dt.Load(dr)

                For Each row As DataRow In dt.Rows
                    Dim lPrincounter As Integer
                    lPrincounter = 0

                    If dt.Rows.Count > 0 Then
                        If dt.Rows(i)("PrintCounter").ToString <> "" Then : lPrincounter = Convert.ToInt32(dt.Rows(i)("PrintCounter").ToString) : End If
                    End If

                    command1.Dispose()

                    '''''
                    lPrincounter = lPrincounter + 1
                    lInvoiceNo = dt.Rows(i)("InvoiceNumber").ToString()
                    Dim qry As String
                    qry = ""
                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text

                    qry = "Update tblSales set    "
                    qry = qry + " PrintCounter = @PrintCounter "
                    qry = qry + " where InvoiceNumber = '" & lInvoiceNo & "'"

                    command.CommandText = qry
                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@PrintCounter", lPrincounter)
                    command.Connection = conn
                    command.ExecuteNonQuery()
                    command.Dispose()
                    i = i + 1
                Next

                conn.Close()
                conn.Dispose()
                command1.Dispose()

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


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

                crReportDocument.RecordSelectionFormula = "{tblSales1.BillSchedule} = '" & Convert.ToString(Session("BillSchedule")) & "' and {tblSales1.PostStatus} = 'P'"

                Dim FilePath As String = ""
                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("BillSchedule")) + ".PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("BillSchedule")) & ".pdf")

                    Response.BinaryWrite(FileBuffer)
                End If
            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("Page_Load", ex.Message.ToString, lBillSchedule)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        Response.Redirect("InvoiceSchedule.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Dim FilePath As String = ""
        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("BillSchedule")) + ".PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Private Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "RV-BATCHINVOICE - " + Convert.ToString(Session("UserID")))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            '   lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub
End Class
