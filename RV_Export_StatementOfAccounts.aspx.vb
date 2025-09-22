


Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net

Partial Class RV_Export_StatementOfAccounts
    Inherits System.Web.UI.Page


    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                Dim ReportType As String = Convert.ToString(Session("ReportType"))

                Dim lselformula As String
                lselformula = Convert.ToString(Session("selFormula"))


                'Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("10" + vbLf & vbLf)
                'End Using


                'Dim lselformula As String
                'lselformula = Convert.ToString(Session("selFormula"))

                'If ReportType = "Format1" Then
                '    If Convert.ToString(Session("Type")) = "Today" Then
                '        crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcBaseCurFormat1.rpt"))
                '        crReportDocument.SetParameterValue("pBal", "DEBIT")
                '    ElseIf Convert.ToString(Session("Type")) = "CutOff" Then
                '        crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcCutOff.rpt"))
                '        crReportDocument.SetParameterValue("pBal", "DEBIT")
                '    ElseIf Convert.ToString(Session("Type")) = "TodayZERO" Then
                '        crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcBaseCurFormat1.rpt"))
                '        crReportDocument.SetParameterValue("pBal", "ZERO")
                '    ElseIf Convert.ToString(Session("Type")) = "CutOffZERO" Then
                '        crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcCutOff.rpt"))
                '        crReportDocument.SetParameterValue("pBal", "ZERO")
                '    End If

                '    '   crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcBaseCurFormat1.rpt"))
                '    crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
                '    crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))
                '    crReportDocument.SetParameterValue("pToday", Convert.ToDateTime(Session("PrintDate")))



                'ElseIf ReportType = "InvRecv" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcInvRecvBaseCur.rpt"))
                crReportDocument.SetParameterValue("pStatementDate", Convert.ToDateTime(Session("PrintDate")))


                If Convert.ToString(Session("Type")) = "DEBIT" Then
                    'Label1.Text = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "' AND {vwcustomermainbillinginfo1.SendStatement}=True and {m02AR22.Balance} <> 0 "

                    crReportDocument.SetParameterValue("pBal", "DEBIT")
                ElseIf Convert.ToString(Session("Type")) = "ZERO" Then
                    'Label1.Text = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "' AND {vwcustomermainbillinginfo1.SendStatement}=True and {m02AR22.Balance} <= 0 "

                    crReportDocument.SetParameterValue("pBal", "ZERO")
                End If



                'If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                '    Label1.Text = Label1.Text + " and {m02Sales.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                '    'qry = qry + " and tblsales.location in (" + Convert.ToString(Session("Branch")) + ")"
                '    'qryrecv = qryrecv + " and tblrecv.location in (" + Convert.ToString(Session("Branch")) + ")"
                '    'qryrecv1 = qryrecv1 + " and tblrecv.location in (" + Convert.ToString(Session("Branch")) + ")"

                '    'If selection = "" Then
                '    '    selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                '    'Else
                '    '    selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                '    'End If
                'End If

                'End If

                'Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("11" + Label1.Text + vbLf & vbLf)
                'End Using

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

                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("12" + vbLf & vbLf)
                'End Using

                crReportDocument.RecordSelectionFormula = Convert.ToString(Session("selFormula"))
                'crReportDocument.RecordSelectionFormula = Label1.Text


                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("13" + Convert.ToString(Session("selFormula")) + vbLf & vbLf)
                'End Using

                Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_StatementOfAccounts.PDF"))

                If File.Exists(FilePath) Then
                    File.Delete(FilePath)

                End If
                oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("13a" + FilePath + vbLf & vbLf)
                'End Using

                expo = crReportDocument.ExportOptions


                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("13b" + FilePath + vbLf & vbLf)
                'End Using

                expo.ExportDestinationType = ExportDestinationType.DiskFile


                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("13c" + FilePath + vbLf & vbLf)
                'End Using

                expo.ExportFormatType = ExportFormatType.PortableDocFormat


                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("13d" + FilePath + vbLf & vbLf)
                'End Using

                expo.DestinationOptions = oDfDopt



                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("13e" + FilePath + vbLf & vbLf)
                'End Using


                '    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

                crReportDocument.Export()



                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("13f" + FilePath + vbLf & vbLf)
                'End Using


                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("14" + vbLf & vbLf)
                'End Using

                Dim User As New WebClient()
                Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)
                If FileBuffer IsNot Nothing Then
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-length", FileBuffer.Length.ToString())
                    Response.AddHeader("content-disposition", "inline; filename=StatementOfAccounts.pdf")
                    Response.BinaryWrite(FileBuffer)
                End If
                '    End If

                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("15" + vbLf & vbLf)
                'End Using
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
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_StatementOfAccounts.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class
