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

Partial Class RV_CreditNote
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()


    Dim expo As New ExportOptions


    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'btnQuit.Attributes.Add("onClick", "javascript:history.go(-1); return false;")

                Dim RecordNo As String = Convert.ToString(Session("RecordNo"))
                lblRecordNo.Text = RecordNo
                'Response.Write(Session("RecordNo"))
                'Return

                If Request.QueryString("Format") = "Format1" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ReportPrinting/CreditNote.RPT"))
                ElseIf Request.QueryString("Format") = "Format2" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ReportPrinting/CreditNote_Format2.RPT"))

                End If

                If Convert.ToString(Session("PrintType")) = "Print" Then
                    crReportDocument.SetParameterValue("pRptTitle", Convert.ToString(Session("RecordNo")))
                ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                    crReportDocument.SetParameterValue("pRptTitle", "CN/DN")
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
                '       crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("RecordNo")) & "]"
                '      crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber}='" & Convert.ToString(Session("RecordNo")) & "'"

                If Request.QueryString("Export") = "PDF" Then

                    Dim FilePath As String = ""

                    If Convert.ToString(Session("PrintType")) = "Print" Then
                        crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber}='" & Convert.ToString(Session("RecordNo")) & "'"

                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Session("RecordNo") + ".PDF"))

                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then

                        crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("RecordNo")) & "]"
                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_CN.PDF"))

                    End If

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
                        If Convert.ToString(Session("PrintType")) = "Print" Then
                            Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("RecordNo")) & ".pdf")
                        ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_CN.PDF"))
                            Response.AddHeader("content-disposition", "inline; filename=CN.pdf")

                        End If
                        Response.BinaryWrite(FileBuffer)
                    End If
                ElseIf Request.QueryString("Export") = "Word" Then

                    Dim FilePath As String = ""
                    If Convert.ToString(Session("PrintType")) = "Print" Then
                        crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber}='" & Convert.ToString(Session("RecordNo")) & "'"

                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Session("RecordNo") + ".doc"))
                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                        crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("RecordNo")) & "]"
                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_CN.doc"))

                    End If
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
                        If Convert.ToString(Session("PrintType")) = "Print" Then
                            Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("RecordNo")) & ".doc")
                        ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_CN.doc"))
                            Response.AddHeader("content-disposition", "inline; filename=CN.doc")

                        End If
                        Response.BinaryWrite(FileBuffer)
                    End If
                End If
            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("Page_Load", ex.Message.ToString, lblRecordNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try

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
            insCmds.Parameters.AddWithValue("@LoginId", "RV-CNPRINT - " + Session("userid"))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()
            'lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub
    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Session("recordno") = lblRecordNo.Text
        Session("servicefrom") = "print"
        'Session("servicefrom") = "servicedetails"
        'Session("Query") = txtQuery.Text
        Session("CNRcNo") = txtRcno.Text
        Response.Redirect("CN.aspx")


        'Session("servicefrom") = "serviceprint"
        ''Session("servicefrom") = "servicedetails"
        ''Session("Query") = txtQuery.Text
        'Session("SvcRcNo") = txtSvcRcno.Text
        'Server.Transfer("Service.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Dim FilePath As String = ""

        If Convert.ToString(Session("PrintType")) = "Print" Then
            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Session("RecordNo") + ".PDF"))
        ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_CN.PDF"))

        End If
        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
