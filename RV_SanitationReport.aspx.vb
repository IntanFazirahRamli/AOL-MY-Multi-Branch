

Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net


Partial Class RV_SanitationReport
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()


    Dim expo As New ExportOptions


    Dim oDfDopt As New DiskFileDestinationOptions

  
    'Dim strCrystalReportFilePath As String

    'Dim strPdfFileDestinationPath As String

    'Public Function SetCrystalReportFilePath(ByVal CrystalReportFileNameFullPath As String)

    '    strCrystalReportFilePath = CrystalReportFileNameFullPath

    'End Function

    'Public Function SetPdfDestinationFilePath(ByVal pdfFileNameFullPath As String)

    '    strPdfFileDestinationPath = pdfFileNameFullPath

    'End Function

    'Public Function SetRecordSelectionFormula(ByVal recSelFormula As String)

    '    sRecSelFormula = recSelFormula

    'End Function

    'Public Function Transfer()

    '    oRDoc.Load(Server.MapPath("~/Reports/ServiceReport.rpt")) 'loads the crystalreports in to the memory

    '    oRDoc.RecordSelectionFormula = "{tblservicerecord1.Recordno} = 'SERV201701-010826'" 'used if u want pass the query to u r crystal form

    '    oDfDopt.DiskFileName = "c:\csharp.net-informations.pdf" 'path of file where u want to locate ur PDF

    '    expo = oRDoc.ExportOptions

    '    expo.ExportDestinationType = ExportDestinationType.DiskFile

    '    expo.ExportFormatType = ExportFormatType.PortableDocFormat

    '    expo.DestinationOptions = oDfDopt

    '    '    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

    '    oRDoc.Export()

    'End Function
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'btnQuit.Attributes.Add("onClick", "javascript:history.go(-1); return false;")

                Dim SvcRecordNo As String = Convert.ToString(Session("SvcRecordNo"))
                lblRecordNo.Text = SvcRecordNo
                Dim Query As String = Convert.ToString(Session("Query"))
                txtQuery.Text = Query
                Dim SvcRcNo As String = Convert.ToString(Session("SvcRcNo"))
                txtSvcRcno.Text = SvcRcNo

                '    MessageBox.Message.Alert(Page, Convert.ToString(Session("selFormula")), "str")

                ' txtServiceRecordNo.Text = Convert.ToString(Session("JobNo"))
                '      Dim repPath As String = Server.MapPath("~/24ServiceRecord02.rpt")
                '      Dim repPath As String = Server.MapPath("~/Reports/ServiceReport.rpt")

                crReportDocument.Load(Server.MapPath("~/Reports/SanitationReport.rpt"))
                crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")
             

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
                '     lblRecordNo.Text = "SERV201701-010826"
                '    crReportDocument.RecordSelectionFormula = "{tblservicerecord1.Recordno} = '" + lblRecordNo.Text + "'"

                If Request.QueryString("Export") = "PDF" Then
                    Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".PDF"))

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
                        Response.AddHeader("content-disposition", "inline; filename=" & lblRecordNo.Text & ".pdf")
                        Response.BinaryWrite(FileBuffer)
                    End If

                ElseIf Request.QueryString("Export") = "Word" Then
                    Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".doc"))

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
                        Response.AddHeader("content-disposition", "inline; filename=" & lblRecordNo.Text & ".doc")
                        Response.BinaryWrite(FileBuffer)
                    End If
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
        Session("recordno") = lblRecordNo.Text
        Session("servicefrom") = "serviceprint"
        'Session("servicefrom") = "servicedetails"
        'Session("Query") = txtQuery.Text
        Session("SvcRcNo") = txtSvcRcno.Text
        Response.Redirect("Service.aspx")


        'Session("servicefrom") = "serviceprint"
        ''Session("servicefrom") = "servicedetails"
        ''Session("Query") = txtQuery.Text
        'Session("SvcRcNo") = txtSvcRcno.Text
        'Server.Transfer("Service.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Protected Sub Page_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

End Class
