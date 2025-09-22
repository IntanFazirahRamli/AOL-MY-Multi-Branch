Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net
Imports System.Data

Public Class RV_SvcSupplement
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Try
        '    Dim InchargeId As String = ""
        '    Dim ServiceDate As String = ""
        '    Dim OurRef As String = ""

        '    If Not IsPostBack Then

        '        '' Dim SvcRecordNo As String = "SERV201702-003720" ''Convert.ToString(Session("RecordNo"))



        '        Dim SvcRecordNo As String = Convert.ToString(Session("SvcRecordNo"))
        '        lblRecordNo.Text = SvcRecordNo
        '        Dim Query As String = Convert.ToString(Session("Query"))
        '        txtQuery.Text = Query
        '        Dim SvcRcNo As String = Convert.ToString(Session("SvcRcNo"))
        '        txtSvcRcno.Text = SvcRcNo


        '        Dim dsDetails As DataSet = GetSvcDetails(SvcRecordNo)
        '        If (dsDetails.Tables(0).Rows.Count > 0) Then
        '            InchargeId = dsDetails.Tables(0).Rows(0)("InchargeId").ToString()
        '            ServiceDate = Convert.ToDateTime(dsDetails.Tables(0).Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
        '        End If


        '        Dim ds1 As DataSet = New DataSet()
        '        Dim dsSupplement As dsSvcPhoto = New dsSvcPhoto()

        '        ds1 = GetSvcImages(SvcRecordNo)
        '        If (ds1.Tables(0).Rows.Count > 0) Then
        '            Dim fCol As New List(Of Byte())()
        '            Dim SCol As New List(Of Byte())()
        '            Dim fDescription As String = ""
        '            Dim sDescription As String = ""
        '            Dim i As Integer = 0

        '            For Each ro As DataRow In ds1.Tables(0).Rows
        '                i += 1
        '                If i Mod 2 <> 0 Then
        '                    fCol.Add(DirectCast(ro("Photo"), Byte()))
        '                    fDescription = ds1.Tables(0).Rows(0)("Description").ToString()
        '                Else
        '                    SCol.Add(DirectCast(ro("Photo"), Byte()))
        '                    sDescription = ds1.Tables(0).Rows(0)("Description").ToString()
        '                End If

        '            Next

        '            If (fCol.Count > 0) Then
        '                For j As Integer = 0 To fCol.Count - 1
        '                    Dim dr As DataRow = dsSupplement.Tables("dtSupp").NewRow()
        '                    dr("RecordNo") = SvcRecordNo
        '                    dr("ServiceDate") = ServiceDate
        '                    dr("OurRef") = OurRef
        '                    dr("InchargeId") = InchargeId
        '                    dr("FColPhoto") = DirectCast(fCol(j), [Byte]())
        '                    dr("FDescription") = fDescription
        '                    If (SCol.Count > 0) Then
        '                        If (j <> SCol.Count) Then
        '                            dr("SColPhoto") = DirectCast(SCol(j), [Byte]())
        '                            dr("SDescription") = sDescription
        '                        End If
        '                    End If
        '                    dsSupplement.Tables("dtSupp").Rows.Add(dr)
        '                Next
        '            End If
        '        End If


        '        crReportDocument.Load(Server.MapPath("~/Reports/ServiceSupplementReport.rpt"))
        '        '' crReportDocument.SetDataSource(dsSupplement.Tables("dtSupp"))


        '        Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()

        '        Dim myTables As Tables = crReportDocument.Database.Tables

        '        For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
        '            Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
        '            myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
        '            myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
        '            myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
        '            myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")
        '            myTable.ApplyLogOnInfo(myTableLogonInfo)
        '            myTableLogonInfo.ConnectionInfo = myConnectionInfo

        '        Next
        '        crReportDocument.SetDataSource(dsSupplement.Tables("dtSupp"))

        '        If Request.QueryString("Export") = "PDF" Then
        '            Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + "_SvcSupplement.PDF"))

        '            If File.Exists(FilePath) Then
        '                File.Delete(FilePath)

        '            End If
        '            oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

        '            expo = crReportDocument.ExportOptions

        '            expo.ExportDestinationType = ExportDestinationType.DiskFile

        '            expo.ExportFormatType = ExportFormatType.PortableDocFormat

        '            expo.DestinationOptions = oDfDopt

        '            '    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

        '            crReportDocument.Export()

        '            Dim User As New WebClient()
        '            Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)
        '            If FileBuffer IsNot Nothing Then
        '                Response.ContentType = "application/pdf"
        '                Response.AddHeader("content-length", FileBuffer.Length.ToString())
        '                Response.AddHeader("content-disposition", "inline; filename=" & lblRecordNo.Text & ".pdf")
        '                Response.BinaryWrite(FileBuffer)
        '            End If
        '        ElseIf Request.QueryString("Export") = "Word" Then
        '            Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + "_SvcSupplement.doc"))

        '            If File.Exists(FilePath) Then
        '                File.Delete(FilePath)

        '            End If
        '            oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

        '            expo = crReportDocument.ExportOptions

        '            expo.ExportDestinationType = ExportDestinationType.DiskFile

        '            expo.ExportFormatType = ExportFormatType.WordForWindows

        '            expo.DestinationOptions = oDfDopt

        '            '    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

        '            crReportDocument.Export()

        '            Dim User As New WebClient()
        '            Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)
        '            If FileBuffer IsNot Nothing Then
        '                Response.ContentType = "application/doc"
        '                Response.AddHeader("content-length", FileBuffer.Length.ToString())
        '                Response.AddHeader("content-disposition", "inline; filename=" & lblRecordNo.Text & ".doc")
        '                Response.BinaryWrite(FileBuffer)
        '            End If
        '        End If

        '    End If
        'Catch ex As Exception
        '    Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"

        '    Using w As StreamWriter = File.AppendText(ErrOtLo)
        '        w.WriteLine(ex.Message.ToString + vbLf & vbLf)
        '    End Using

        'End Try

        Try
            If Not IsPostBack Then
                'btnQuit.Attributes.Add("onClick", "javascript:history.go(-1); return false;")
                Dim SvcRecordNo As String = Convert.ToString(Session("SvcRecordNo"))
                lblRecordNo.Text = SvcRecordNo
                Dim Query As String = Convert.ToString(Session("Query"))
                txtQuery.Text = Query
                Dim SvcRcNo As String = Convert.ToString(Session("SvcRcNo"))
                txtSvcRcno.Text = SvcRcNo

                crReportDocument.Load(Server.MapPath("~/Reports/ServiceSupplementReport_New.rpt"))
                crReportDocument.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                crReportDocument.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                crReportDocument.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                crReportDocument.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

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
                crReportDocument.RecordSelectionFormula = "{tblservicerecord1.Recordno} = '" + lblRecordNo.Text + "'"


                If Request.QueryString("Export") = "PDF" Then
                    Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + "_Supp.PDF"))

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
                    Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + "_Supp.doc"))

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


    Public Function GetSvcImages(RecordNo As String) As DataSet
        Try
            Dim ds As New DataSet()
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataImagesConnectionString").ConnectionString

            Dim selCmd As New MySqlCommand()
            selCmd.CommandType = CommandType.Text
            Dim sqlSelect As String = "Select "
            sqlSelect += "RecordNo, "
            sqlSelect += "FileType, "
            sqlSelect += "FileSize, "
            sqlSelect += "Photo,Description "
            sqlSelect += "from tblServicePhoto "
            sqlSelect += "where "
            sqlSelect += "RecordNo=@RecordNo"
            selCmd.CommandText = sqlSelect
            selCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
            conn.Open()
            selCmd.Connection = conn
            Dim da As New MySqlDataAdapter()
            da.SelectCommand = selCmd
            da.Fill(ds, "DataTable1")
            conn.Close()
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetSvcDetails(RecordNo As String) As DataSet
        Try
            Dim ds As New DataSet()
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim selCmd As New MySqlCommand()
            selCmd.CommandType = CommandType.Text
            Dim sqlSelect As String = "Select * from tblServiceRecord where RecordNo=@RecordNo"
            selCmd.CommandText = sqlSelect
            selCmd.Parameters.AddWithValue("@RecordNo", RecordNo)
            conn.Open()
            selCmd.Connection = conn
            Dim da As New MySqlDataAdapter()
            da.SelectCommand = selCmd
            da.Fill(ds)
            conn.Close()
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Session("recordno") = lblRecordNo.Text
        Session("servicefrom") = "serviceprint"
        'Session("servicefrom") = "servicedetails"
        'Session("Query") = txtQuery.Text
        Session("SvcRcNo") = txtSvcRcno.Text
        Response.Redirect("Service.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + "_SvcSupplement.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub


    Protected Sub Page_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + lblRecordNo.Text + "_SvcSupplement.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub
End Class