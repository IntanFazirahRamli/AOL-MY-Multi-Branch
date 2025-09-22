


Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net
Imports MySql.Data
Imports System.Data


Partial Class RV_Export_ServiceRecordPrinting
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
         Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")


                crReportDocument.Load(Server.MapPath("~/Reports/ServiceRecordReports/ServiceRecordPrinting.rpt"))

                crReportDocument.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                crReportDocument.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                crReportDocument.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                crReportDocument.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))

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

           

                If Request.QueryString("Type") = "Print" Then
                    crReportDocument.RecordSelectionFormula = Convert.ToString(Session("selFormula"))

                ElseIf Request.QueryString("Type") = "Invoice" Then
                    '      InsertIntoTblWebEventLog(Convert.ToString(Session("PrintType")), Convert.ToString(Session("InvoiceNumber")), "1")
                    txtSvc.Text = RetrieveSvcRecord(Convert.ToString(Session("InvoiceNumber")), Convert.ToString(Session("PrintType")))
                    '        InsertIntoTblWebEventLog("Print", txtSvc.Text, "2")

                    '   crReportDocument.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "] and {tblservicerecord1.Status} = 'P'"
                    crReportDocument.RecordSelectionFormula = "{tblservicerecord1.RecordNo} in [" & txtSvc.Text & "] and {tblservicerecord1.Status} = 'P'"

                End If

             

                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_ServiceRecordPrinting.PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=ServiceRecordPrinting.pdf")
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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_ServiceRecordPrinting.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Private Function RetrieveSvcRecord(InvoiceNumber As String, PrintType As String) As String
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        If PrintType = "Print" Then
            command2.CommandText = "SELECT RefType FROM tblsalesdetail where InvoiceNumber='" + InvoiceNumber.Trim("""") + "'"
        ElseIf PrintType = "MultiPrint" Then
            command2.CommandText = "SELECT RefType FROM tblsalesdetail where InvoiceNumber in (" + InvoiceNumber + ")"
        End If

        command2.Connection = conn

        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
        Dim dt2 As New DataTable
        dt2.Load(dr2)

        Dim SvcRecNo As String = ""

        If dt2.Rows.Count > 0 Then
            For i As Int16 = 0 To dt2.Rows.Count - 1
                If SvcRecNo = "" Then
                    SvcRecNo = """" + dt2.Rows(i)("RefType").ToString + """"
                Else
                    SvcRecNo = SvcRecNo + ",""" + dt2.Rows(i)("RefType").ToString + """"
                End If
            Next
        End If

        command2.Dispose()
        dt2.Clear()
        dt2.Dispose()
        dr2.Close()
        conn.Close()

        '    txtSvc.Text = SvcRecNo
        Return SvcRecNo

    End Function

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
            insCmds.Parameters.AddWithValue("@LoginId", "INVOICE SERVICE REPORT")
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

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub
   
End Class
