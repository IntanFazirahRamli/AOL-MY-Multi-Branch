Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO
Imports System.Net
Imports System.Data


Public Class RV_ContractServiceSchedule
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()


    Dim expo As New ExportOptions


    Dim oDfDopt As New DiskFileDestinationOptions
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'Dim repPath As String = Server.MapPath("~/Reports/ContractServiceSchedule.rpt")
                '     crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContractServiceSchedule.rpt"))
                InsertIntoTblWebEventLog("ServiceSchReport", "1", Request.QueryString("Status"))

                 If Request.QueryString("Status") = "OpenPosted" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContractServiceSchedule.rpt"))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                    crReportDocument.SetParameterValue("pContractNo", Convert.ToString(Session("ContractNo")))

                    crReportDocument.SetParameterValue("pStatus", "[""""O"""", """"P""""]")
              
                ElseIf Request.QueryString("Status") = "Open" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContractServiceSchedule.rpt"))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                    crReportDocument.SetParameterValue("pContractNo", Convert.ToString(Session("ContractNo")))

                    crReportDocument.SetParameterValue("pStatus", "[""""O""""]")

                ElseIf Request.QueryString("Status") = "Services" Then
                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceContractReports/ContractServiceSchedule_Services.rpt"))
                    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                    crReportDocument.SetParameterValue("pContractNo", Convert.ToString(Session("ContractNo")))

                    crReportDocument.SetParameterValue("pServices", Convert.ToString(Session("Services")))

                End If
                InsertIntoTblWebEventLog("ServiceSchReport", "2", Convert.ToString(Session("ContractNo")))
                InsertIntoTblWebEventLog("ServiceSchReport", "3", Convert.ToString(Session("Services")))

                '   crReportDocument.SetParameterValue("pStatus", Convert.ToString(Session("Status")))
                '   crReportDocument.SetParameterValue("pStatus", "[""""O"""", """"P""""]")
                ' crReportDocument.SetParameterValue("pStatus", "[""""O""""]")
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
                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("ContractNo")) + ".PDF"))

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
                    Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("ContractNo")) & ".pdf")
                    Response.BinaryWrite(FileBuffer)
                End If
            End If
        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)
                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
            End Using
        End Try
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'Session("contractno") = txtContractNo.Text

        'Session("contractno") = txtContractNo.Text


        'Session("contractno") = txtContractNo.Text



        Session("contractdetailfrom") = "contract"
        'Response.Redirect("Contract.aspx")
        Response.Redirect("Contract.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("ContractNo")) + ".PDF"))

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
            insCmds.Parameters.AddWithValue("@LoginId", "contract -  report")
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