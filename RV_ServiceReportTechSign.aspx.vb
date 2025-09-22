Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net
Imports System.Data
Imports MySql.Data
Imports Ionic.Zip

Partial Class RV_ServiceReportTechSign
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions


    Dim oDfDopt As New DiskFileDestinationOptions

   
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
                Dim ImgLoc As String = ImagesLocation()

                If ImgLoc = "DB" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceReportTechSign.rpt"))
                    crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                    crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                    crReportDocument.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                    crReportDocument.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

                    crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                    crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                    crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

                ElseIf ImgLoc = "Directory" Then
                    DownloadPhotos(SvcRecordNo)

                    crReportDocument.Load(Server.MapPath("~/Reports/ServiceReportTechSignNew.rpt"))
                    crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                    crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                    crReportDocument.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                    crReportDocument.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

                    crReportDocument.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                    crReportDocument.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                    crReportDocument.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

                    crReportDocument.SetParameterValue("pPhotoPathMain", "E:\WEBSITE FILES\AnticimexSingaporeBeta\SERVICE REPORTS\" + SvcRecordNo + "\REPORT PHOTOS\")

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
                '     lblRecordNo.Text = "SERV201701-010826"
                crReportDocument.RecordSelectionFormula = "{tblservicerecord1.Recordno} = '" + lblRecordNo.Text + "'"


                If Request.QueryString("Export") = "PDF" Then
                    Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + ".PDF"))

                    If File.Exists(FilePath) Then
                        File.Delete(FilePath)

                    End If

                    oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

                    expo = crReportDocument.ExportOptions

                    expo.ExportDestinationType = ExportDestinationType.DiskFile

                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    ' expo.ExportFormatType = ExportFormatType.WordForWindows

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
                    ' expo.ExportFormatType = ExportFormatType.WordForWindows

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
        'Session("recordno") = lblRecordNo.Text
        'Session("servicefrom") = "servicedetails"
        'Session("Query") = txtQuery.Text
        'Session("SvcRcNo") = txtSvcRcno.Text
        'Server.Transfer("Service.aspx")

        Session("recordno") = lblRecordNo.Text
        Session("servicefrom") = "serviceprint"
        'Session("servicefrom") = "servicedetails"
        'Session("Query") = txtQuery.Text
        Session("SvcRcNo") = txtSvcRcno.Text
        'Server.Transfer("Service.aspx")
        Response.Redirect("Service.aspx")
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

    Private Function ImagesLocation() As String

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        ''''''''''''''''''''''''''''''''''''''''''''''''
        Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
        commandServiceRecordMasterSetup.CommandType = CommandType.Text
        commandServiceRecordMasterSetup.CommandText = "SELECT ImagesFromDB,ImagesFromDir FROM tblservicerecordmastersetup"
        commandServiceRecordMasterSetup.Connection = conn

        Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
        Dim dtServiceRecordMasterSetup As New DataTable
        dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

        Dim Loc As String = "DB"
        If dtServiceRecordMasterSetup.Rows(0)("ImagesFromDB").ToString = "1" Then
            Loc = "DB"

        End If
        If dtServiceRecordMasterSetup.Rows(0)("ImagesFromDir").ToString = "1" Then
            Loc = "Directory"

        End If
        commandServiceRecordMasterSetup.Dispose()
        drServiceRecordMasterSetup.Close()
        dtServiceRecordMasterSetup.Dispose()
        conn.Close()
        conn.Dispose()

        Return Loc
    End Function

    Private Sub DownloadPhotos(RecordNo As String)
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataImagesConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblservicephoto where recordno='" + lblRecordNo.Text + "' and (ServiceRecordRcno is null or ServiceRecordRcno='' or ServiceRecordRcno=0)"
        command1.Connection = conn
        command1.Dispose()

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            Dim folderPath As String = Server.MapPath("~/ServiceReports/" & RecordNo & "/REPORT PHOTOS/")
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim bytes As Byte() = CType(dt.Rows(i)("Photo"), Byte())
                ' File.WriteAllBytes(Server.MapPath("~\ServiceReports\" & RecordNo & "\REPORT PHOTOS\" & dt.Rows(i)("rcno") & ".png"), bytes)
                File.WriteAllBytes(folderPath & dt.Rows(i)("rcno") & ".png", bytes)
            Next
          
        End If

        dt.Clear()
        dt.Dispose()
        dr.Close()
        command1.Dispose()

        conn.Close()
        conn.Dispose()
    End Sub

End Class
