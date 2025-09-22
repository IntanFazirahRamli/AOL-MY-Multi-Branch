

Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO
Imports System.Net
Imports System.Data

Public Class RV_TaxInvoice_Format8
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()


    Dim expo As New ExportOptions


    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Dim FilePath As String = ""
        Dim FilePath1 As String = ""

        If Convert.ToString(Session("PrintType")) = "Print" Then
            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + ".doc"))
            FilePath1 = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + ".pdf"))

        ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices.doc"))
            FilePath1 = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices.pdf"))

        End If
        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If

        If File.Exists(FilePath1) Then
            File.Delete(FilePath1)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TAX INVOICE FORMAT-8", Left(Convert.ToString(Session("InvoiceNumber")), 50), "ADD", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, Left(Convert.ToString(Session("InvoiceNumber")), 20), "", "")

                'If Request.QueryString("Export") = "PDF" Then
                '    PrintCount()

                'End If

                If Request.QueryString("Export") = "PDF" Then

                    Dim lInvNo, lInvNoMulti As String
                    lInvNo = ""
                    lInvNoMulti = ""

                    lInvNo = Convert.ToString(Session("InvoiceNo"))
                    lInvNoMulti = Session("InvoiceNumber")


                    If lInvNo = "INVOICE" Then

                        Dim stringList As List(Of String) = lInvNoMulti.Split(",").ToList()
                        Dim YrStrList As List(Of [String]) = New List(Of String)()

                        For Each str As String In stringList
                            str = str.Replace("""", "")
                            PrintCountNew(str)
                        Next
                    Else
                        lInvNo = lInvNo.Replace("""", "")
                        PrintCountNew(lInvNo)
                    End If
                End If

                ' crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format8.rpt"))

                If Convert.ToString(Session("InvoiceType")) = "Invoice" Then
                    If Request.QueryString("Export") = "PDF" Then
                        crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format8.rpt"))
                    ElseIf Request.QueryString("Export") = "Word" Or Request.QueryString("Export") = "View" Then
                        crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format8_New.rpt"))

                    End If
                Else
                    If Request.QueryString("Export") = "PDF" Then
                        crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoiceE_Format8.rpt"))
                    ElseIf Request.QueryString("Export") = "Word" Or Request.QueryString("Export") = "View" Then
                        crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoiceE_Format8_New.rpt"))
                    End If
                    crReportDocument.SetParameterValue("pUUID", Convert.ToString(Session("UUID")))
                    crReportDocument.SetParameterValue("pLongID", Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo") + ".png")))

                    Dim img As New WebClient
                    img.DownloadFile("https://nossl.quickchart.io/qr?size=100&text=https://myinvois.hasil.gov.my/" & Convert.ToString(Session("UUID")) & "/share/" & Convert.ToString(Session("LongID")), Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo") + ".png")))

                    ' img.DownloadFile("https://nossl.quickchart.io/qr?size=100&text=https://preprod.myinvois.hasil.gov.my/" & Convert.ToString(Session("UUID")) & "/share/" & Convert.ToString(Session("LongID")), Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo") + ".png")))
                End If


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

                crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"

                If Request.QueryString("Export") = "PDF" Or Request.QueryString("Export") = "View" Then

                    Dim FilePath As String = ""

                    If Convert.ToString(Session("PrintType")) = "Print" Then
                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + ".PDF"))
                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices.PDF"))

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
                            Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("InvoiceNumber")) & ".pdf")
                        ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices.PDF"))
                            Response.AddHeader("content-disposition", "inline; filename=Invoices.pdf")

                        End If
                        Response.BinaryWrite(FileBuffer)
                    End If
                ElseIf Request.QueryString("Export") = "Word" Then

                    Dim FilePath As String = ""

                    If Convert.ToString(Session("PrintType")) = "Print" Then
                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + ".doc"))
                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices.doc"))

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
                            Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("InvoiceNumber")) & ".doc")
                        ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices.doc"))
                            Response.AddHeader("content-disposition", "inline; filename=Invoices.doc")

                        End If
                        Response.BinaryWrite(FileBuffer)
                    End If
                End If

            End If

          Catch ex As Exception
            InsertIntoTblWebEventLog("Page_Load", ex.Message.ToString, Left(Convert.ToString(Session("InvoiceNumber")), 100))
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub PrintCountNew(lPrintInvoice As String)
        Try

            '15.11.19
            Dim lPrint As String
            lPrint = ""
            lPrint = lPrintInvoice
            '15.11.19

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            'command1.CommandText = "SELECT PrintCounter FROM tblsales where invoicenumber='" + Convert.ToString(Session("InvoiceNo")) + "';"
            command1.CommandText = "SELECT PrintCounter FROM tblsales where invoicenumber='" + lPrint + "';"

            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                'Dim qry As String = "update tblsales set PrintCounter=@PrintCounter where invoicenumber='" + Convert.ToString(Session("InvoiceNo")) + "';"
                Dim qry As String = "update tblsales set PrintCounter=@PrintCounter where invoicenumber='" + lPrint + "';"


                command.CommandText = qry
                command.Parameters.Clear()
                Dim count As Int16 = 0
                If String.IsNullOrEmpty(dt1.Rows(0)("PrintCounter").ToString) Or dt1.Rows(0)("PrintCounter").ToString = "" Then
                    count = 0
                Else
                    count = dt1.Rows(0)("PrintCounter")
                End If
                command.Parameters.AddWithValue("@PrintCounter", count + 1)

                command.Connection = conn

                command.ExecuteNonQuery()
                command.Dispose()

            End If
            conn.Close()
            conn.Dispose()
            dt1.Dispose()
            dt1.Clear()
            dr1.Close()
            command1.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PrintCount", ex.Message.ToString, Convert.ToString(Session("InvoiceNumber")))
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        Response.Redirect("Invoice.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload

        Dim FilePath As String = ""
        Dim FilePath1 As String = ""

        If Convert.ToString(Session("PrintType")) = "Print" Then
            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + ".doc"))
            FilePath1 = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + ".pdf"))

        ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices.doc"))
            FilePath1 = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices.pdf"))

        End If
        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If

        If File.Exists(FilePath1) Then
            File.Delete(FilePath1)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Protected Sub PrintCount()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT PrintCounter FROM tblsales where invoicenumber='" + Convert.ToString(Session("InvoiceNo")) + "';"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "update tblsales set PrintCounter=@PrintCounter where invoicenumber='" + Convert.ToString(Session("InvoiceNo")) + "';"


                command.CommandText = qry
                command.Parameters.Clear()
                Dim count As Int16 = 0
                If String.IsNullOrEmpty(dt1.Rows(0)("PrintCounter").ToString) Or dt1.Rows(0)("PrintCounter").ToString = "" Then
                    count = 0
                Else
                    count = dt1.Rows(0)("PrintCounter")
                End If
                command.Parameters.AddWithValue("@PrintCounter", count + 1)

                command.Connection = conn

                command.ExecuteNonQuery()
                command.Dispose()

            End If
            conn.Close()
            conn.Dispose()
            dt1.Dispose()
            dt1.Clear()
            dr1.Close()
            command1.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("PrintCount", ex.Message.ToString, Convert.ToString(Session("InvoiceNumber")))
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
            insCmds.Parameters.AddWithValue("@LoginId", "RV-TAXINVOICE8 - " + Session("userid"))
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
End Class
