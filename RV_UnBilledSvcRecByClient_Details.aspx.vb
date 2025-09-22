Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports System.Globalization
Imports CrystalDecisions.Shared



Partial Class RV_UnBilledSvcRecByClient_Details
    Inherits System.Web.UI.Page


    Dim crReportDocument As New ReportDocument()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")
                Dim repPath As String = Server.MapPath("~/Reports/ARReports/UnbilledServiceRecordReports/UnbilledServiceRecordDetails_Client.rpt")
                Dim myReport As New ReportDocument()
                myReport.Load(repPath)
                myReport.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                myReport.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))
                'FIND TAX RATE AND TAX CODE
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text
                command1.CommandText = "Select TaxRatePct from tbltaxtype where TaxType = 'SR'"
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()

                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("TaxRatePct").ToString <> "" Then
                        Dim rate As Decimal = dt.Rows(0)("TaxRatePct")

                        myReport.SetParameterValue("pTaxCode", "SR")
                        myReport.SetParameterValue("pTaxRate", rate)

                    End If
                End If

                conn.Close()
                Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
                Dim myTables As Tables = myReport.Database.Tables

                For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                    Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
                    myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
                    myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                    myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
                    myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")
                    myTable.ApplyLogOnInfo(myTableLogonInfo)
                    myTableLogonInfo.ConnectionInfo = myConnectionInfo
                Next
                CrystalReportViewer1.ReportSource = myReport
                CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))

            Else
                CrystalReportViewer1.RefreshReport()
            End If

        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)
                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
            End Using
        End Try
    End Sub
    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        '  Response.Redirect("Reports.aspx")
    End Sub
End Class

