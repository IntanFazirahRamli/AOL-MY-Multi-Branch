

Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO
Imports System.Net
Imports System.Data
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Class RV_TaxInvoice_Format15
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
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TAX INVOICE FORMAT-15", Left(Convert.ToString(Session("InvoiceNumber")), 50), "ADD", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, Left(Convert.ToString(Session("InvoiceNumber")), 20), "", "")

                'If Request.QueryString("Export") = "PDF" Then
                '    PrintCount()
                '    '   ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then

                'End If
                InsertIntoTblWebEventLog("Page_Load1", Request.QueryString("Export"), Session("InvoiceNo").ToString)


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

                IndividualorMergedFile()

                InsertIntoTblWebEventLog("Page_Load2", Request.QueryString("Export"), Session("InvoiceNo").ToString)


                'crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format1_New.rpt"))
                'crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))

                'Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
                'Dim myTables As Tables = crReportDocument.Database.Tables

                'For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                '    Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
                '    myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
                '    myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                '    myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
                '    myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")
                '    myTable.ApplyLogOnInfo(myTableLogonInfo)
                '    myTableLogonInfo.ConnectionInfo = myConnectionInfo
                'Next

                'crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & Convert.ToString(Session("InvoiceNumber")) & "]"
                'Dim FilePath As String = ""

                '    '    If Convert.ToString(Session("PrintType")) = "Print" Then
                '    FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + "_New.PDF"))
                '    'ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                '    '  FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.PDF"))

                '    '  End If

                '    If File.Exists(FilePath) Then
                '        File.Delete(FilePath)

                '    End If
                '    oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

                '    expo = crReportDocument.ExportOptions

                '    expo.ExportDestinationType = ExportDestinationType.DiskFile

                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat

                '    expo.DestinationOptions = oDfDopt

                '    '    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

                '    crReportDocument.Export()

                '    Dim crReportDocument1 As New ReportDocument

                '    crReportDocument1.Load(Server.MapPath("~/Reports/ServiceRecordReports/ServiceRecordPrinting.rpt"))
                '    Dim myConnectionInfo1 As ConnectionInfo = New ConnectionInfo()

                '    Dim myTables1 As Tables = crReportDocument1.Database.Tables

                '    For Each myTable1 As CrystalDecisions.CrystalReports.Engine.Table In myTables1
                '        Dim myTableLogonInfo1 As TableLogOnInfo = myTable1.LogOnInfo
                '        myConnectionInfo1.ServerName = ConfigurationManager.AppSettings("ServerName")
                '        myConnectionInfo1.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                '        myConnectionInfo1.UserID = ConfigurationManager.AppSettings("UserName")
                '        myConnectionInfo1.Password = ConfigurationManager.AppSettings("Password")
                '        myTable1.ApplyLogOnInfo(myTableLogonInfo1)
                '        myTableLogonInfo1.ConnectionInfo = myConnectionInfo1

                '    Next


                '    crReportDocument1.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "] and {tblservicerecord1.Status} = 'P'"
                '    Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF")

                '    If File.Exists(FilePath1) Then
                '        File.Delete(FilePath1)

                '    End If

                '    oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

                '    expo = crReportDocument1.ExportOptions

                '    expo.ExportDestinationType = ExportDestinationType.DiskFile

                '    expo.ExportFormatType = ExportFormatType.PortableDocFormat

                '    expo.DestinationOptions = oDfDopt

                '    crReportDocument1.Export()
                '    crReportDocument1.Close()

                '    Dim FilePathMerge As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF")
                '    'Dim SourceFile() As String = {"FilePath", "FilePath1"}
                '    'MergeFiles(FilePathMerge, SourceFile)
                '    'MergePDF(FilePath, FilePath1)

                '    Dim User As New WebClient()
                '    Dim FileBuffer As [Byte]() = User.DownloadData(FilePathMerge)

                '    If txtFileType.Text = "Individual" Then

                '    Else
                '        Dim FilePath2 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")


                '        MergePDF(FilePath2, FilePath1)

                '    End If


                '    If Request.QueryString("Export") = "PDF" Or Request.QueryString("Export") = "View" Then

                '        If FileBuffer IsNot Nothing Then
                '            Response.ContentType = "application/pdf"
                '            Response.AddHeader("content-length", FileBuffer.Length.ToString())
                '            If Convert.ToString(Session("PrintType")) = "Print" Then
                '                Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("InvoiceNumber")) & " & ServiceReports.pdf")
                '            ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                '                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.PDF"))
                '                Response.AddHeader("content-disposition", "inline; filename=Invoices_New.pdf")

                '            End If
                '            Response.BinaryWrite(FileBuffer)
                '        End If

                '    ElseIf Request.QueryString("Export") = "Word" Then
                '        'Dim FilePath As String = ""

                '        'If Convert.ToString(Session("PrintType")) = "Print" Then
                '        '    FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + "_New.doc"))
                '        'ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                '        '    FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.doc"))

                '        'End If

                '        'If File.Exists(FilePath) Then
                '        '    File.Delete(FilePath)

                '        'End If
                '        'oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

                '        'expo = crReportDocument.ExportOptions

                '        'expo.ExportDestinationType = ExportDestinationType.DiskFile

                '        'expo.ExportFormatType = ExportFormatType.WordForWindows

                '        'expo.DestinationOptions = oDfDopt

                '        ''    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

                '        'crReportDocument.Export()

                '        'Dim User As New WebClient()
                '        'Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)
                '        If FileBuffer IsNot Nothing Then
                '            Response.ContentType = "application/doc"
                '            Response.AddHeader("content-length", FileBuffer.Length.ToString())
                '            If Convert.ToString(Session("PrintType")) = "Print" Then
                '                Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("InvoiceNumber")) & "_New.doc")
                '            ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                '                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.doc"))
                '                Response.AddHeader("content-disposition", "inline; filename=Invoices_New.doc")

                '            End If
                '            Response.BinaryWrite(FileBuffer)
                '        End If
                '    End If

                'End If

                InsertIntoTblWebEventLog("Page_Load3", Convert.ToString(Session("UserId")), Session("InvoiceNo").ToString)

                If Request.QueryString("Export") = "PDF" Or Request.QueryString("Export") = "View" Then


                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format1_New.rpt"))
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
                    InsertIntoTblWebEventLog("Page_Load4", Convert.ToString(Session("InvoiceNumber")), Session("InvoiceNo").ToString)


                    Dim FilePath As String = ""

                    '    If Convert.ToString(Session("PrintType")) = "Print" Then
                    FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + "_New.PDF"))
                    'ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                    '  FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.PDF"))

                    '  End If

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

                    Dim crReportDocument1 As New ReportDocument

                    'crReportDocument1.Load(Server.MapPath("~/Reports/ARReports/ServiceRecordPrintingInvoice.rpt"))

                    ''crReportDocument1.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                    ''crReportDocument1.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                    ''crReportDocument1.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                    ''crReportDocument1.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))

                    crReportDocument1.Load(Server.MapPath("~/Reports/ServiceReportBatchPrinting.rpt"))
                    crReportDocument1.SetParameterValue("pCompanyName", Convert.ToString(Session("CompanyName")))
                    crReportDocument1.SetParameterValue("pCompanyAddress", Convert.ToString(Session("OfficeAddress")))
                    crReportDocument1.SetParameterValue("pRegNo", Convert.ToString(Session("BusinessRegNumber")))
                    crReportDocument1.SetParameterValue("pGstRegNos", Convert.ToString(Session("GSTNumber")))

                    crReportDocument1.SetParameterValue("pCompanyTel", "Tel : " + Convert.ToString(Session("TelNumber")) + "  Fax : " + Convert.ToString(Session("FaxNumber")))
                    crReportDocument1.SetParameterValue("pCompanyEmail", "Email: <a href=""mailto:""" + Convert.ToString(Session("CompanyEmail")) + """ style = ""color:blue"">" + Convert.ToString(Session("CompanyEmail")) + "</a>")
                    crReportDocument1.SetParameterValue("pCompanyWebsite", "Website: <a href=""" + Convert.ToString(Session("Website")) + """ style = ""color:blue"">" + Convert.ToString(Session("Website")) + "</a>")

                    If ConfigurationManager.AppSettings("DomainName").ToString() = "PEST-PRO" Then

                    Else
                        'crReportDocument1.SetParameterValue("pSort1By", Convert.ToString(Session("sort1")))
                        'crReportDocument1.SetParameterValue("pSort2By", Convert.ToString(Session("sort2")))
                        'crReportDocument1.SetParameterValue("pSort3By", Convert.ToString(Session("sort3")))
                        'crReportDocument1.SetParameterValue("pSort4By", Convert.ToString(Session("sort4")))

                    End If

                    InsertIntoTblWebEventLog("Page_Load5", ConfigurationManager.AppSettings("DomainName").ToString(), Session("InvoiceNo").ToString)

                    Dim myConnectionInfo1 As ConnectionInfo = New ConnectionInfo()

                    Dim myTables1 As Tables = crReportDocument1.Database.Tables

                    For Each myTable1 As CrystalDecisions.CrystalReports.Engine.Table In myTables1
                        Dim myTableLogonInfo1 As TableLogOnInfo = myTable1.LogOnInfo
                        myConnectionInfo1.ServerName = ConfigurationManager.AppSettings("ServerName")
                        myConnectionInfo1.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                        myConnectionInfo1.UserID = ConfigurationManager.AppSettings("UserName")
                        myConnectionInfo1.Password = ConfigurationManager.AppSettings("Password")
                        myTable1.ApplyLogOnInfo(myTableLogonInfo1)
                        myTableLogonInfo1.ConnectionInfo = myConnectionInfo1

                    Next


                    crReportDocument1.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "] and {tblservicerecord1.Status} = 'P'"
                    Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF")

                    If File.Exists(FilePath1) Then
                        File.Delete(FilePath1)

                    End If

                    oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

                    expo = crReportDocument1.ExportOptions

                    expo.ExportDestinationType = ExportDestinationType.DiskFile

                    expo.ExportFormatType = ExportFormatType.PortableDocFormat

                    expo.DestinationOptions = oDfDopt

                    crReportDocument1.Export()
                    crReportDocument1.Close()

                    InsertIntoTblWebEventLog("Page_Load1", txtFileType.Text, Session("InvoiceNo").ToString)

                    'Dim SourceFile() As String = {"FilePath", "FilePath1"}
                    'MergeFiles(FilePathMerge, SourceFile)
                    'MergePDF(FilePath, FilePath1)

                    Dim FilePathMerge As String

                 
                    If txtFileType.Text = "Individual" Then
                        Dim User As New WebClient()
                        Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)

                        If FileBuffer IsNot Nothing Then
                            Response.ContentType = "application/pdf"
                            Response.AddHeader("content-length", FileBuffer.Length.ToString())
                            If Convert.ToString(Session("PrintType")) = "Print" Then
                                Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("InvoiceNumber")) & " & ServiceReports.pdf")
                            ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.PDF"))
                                Response.AddHeader("content-disposition", "inline; filename=Invoices_New.pdf")

                            End If
                            Response.BinaryWrite(FileBuffer)
                        End If
                    Else
                        FilePathMerge = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF")

                        Dim FilePath2 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + ".PDF")
                     

                        MergePDF(FilePath, FilePath1)

                        Dim User As New WebClient()
                        Dim FileBuffer As [Byte]() = User.DownloadData(FilePathMerge)

                        InsertIntoTblWebEventLog("Page_Load2", FileBuffer.Length.ToString, txtFileType.Text)

                        If FileBuffer IsNot Nothing Then
                            Response.ContentType = "application/pdf"
                            Response.AddHeader("content-length", FileBuffer.Length.ToString())
                            If Convert.ToString(Session("PrintType")) = "Print" Then
                                Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("InvoiceNumber")) & " & ServiceReports.pdf")
                            ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                                FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.PDF"))
                                Response.AddHeader("content-disposition", "inline; filename=Invoices_New.pdf")

                            End If
                            Response.BinaryWrite(FileBuffer)
                        End If
                    End If
                    InsertIntoTblWebEventLog("Page_Load3", Convert.ToString(Session("InvoiceNumber")), txtFileType.Text)


                ElseIf Request.QueryString("Export") = "Word" Then

                    crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format1_New.rpt"))
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


                    Dim FilePath As String = ""

                    If Convert.ToString(Session("PrintType")) = "Print" Then
                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + Convert.ToString(Session("InvoiceNo")) + "_New.doc"))
                    ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                        FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.doc"))

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


                    'Dim crReportDocument1 As New ReportDocument

                    'crReportDocument1.Load(Server.MapPath("~/Reports/ServiceRecordReports/ServiceRecordPrinting.rpt"))
                    'Dim myConnectionInfo1 As ConnectionInfo = New ConnectionInfo()

                    'Dim myTables1 As Tables = crReportDocument1.Database.Tables

                    'For Each myTable1 As CrystalDecisions.CrystalReports.Engine.Table In myTables1
                    '    Dim myTableLogonInfo1 As TableLogOnInfo = myTable1.LogOnInfo
                    '    myConnectionInfo1.ServerName = ConfigurationManager.AppSettings("ServerName")
                    '    myConnectionInfo1.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                    '    myConnectionInfo1.UserID = ConfigurationManager.AppSettings("UserName")
                    '    myConnectionInfo1.Password = ConfigurationManager.AppSettings("Password")
                    '    myTable1.ApplyLogOnInfo(myTableLogonInfo1)
                    '    myTableLogonInfo1.ConnectionInfo = myConnectionInfo1

                    'Next


                    'crReportDocument1.RecordSelectionFormula = "{tblservicerecord1.BillNo} in [" & Convert.ToString(Session("InvoiceNumber")) & "] and {tblservicerecord1.Status} = 'P'"
                    'Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.doc")

                    'If File.Exists(FilePath1) Then
                    '    File.Delete(FilePath1)

                    'End If

                    'oDfDopt.DiskFileName = FilePath1 'path of file where u want to locate ur PDF

                    'expo = crReportDocument.ExportOptions

                    'expo.ExportDestinationType = ExportDestinationType.DiskFile

                    'expo.ExportFormatType = ExportFormatType.WordForWindows

                    'expo.DestinationOptions = oDfDopt

                    ''    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

                    'crReportDocument1.Export()

                    ''Dim sourceFile1 As String = "source file 1"
                    ''Dim sourceFile2 As String = "source file 2"
                    ''Dim openFile1 As Stream = New FileStream(sourceFile1, FileMode.Open)
                    ''Dim openFile2 As Stream = New FileStream(sourceFile2, FileMode.Open)
                    ''Dim documentStreams As List(Of Stream) = New List(Of Stream)()
                    ''documentStreams.Add(openFile1)
                    ''documentStreams.Add(openFile2)
                    ''Dim result As DocumentResult = New DocumentHandler().Join(documentStreams)
                    ''Dim documentStream As Stream = result.Stream
                    ''Dim fileStream = File.Create("output path" & "OutPut." & result.FileFormat)
                    ''documentStream.CopyTo(fileStream)
                    ''documentStream.Close()


                    Dim User As New WebClient()
                    Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)
                    If FileBuffer IsNot Nothing Then
                        Response.ContentType = "application/doc"
                        Response.AddHeader("content-length", FileBuffer.Length.ToString())
                        If Convert.ToString(Session("PrintType")) = "Print" Then
                            Response.AddHeader("content-disposition", "inline; filename=" & Convert.ToString(Session("InvoiceNumber")) & "_New.doc")
                        ElseIf Convert.ToString(Session("PrintType")) = "MultiPrint" Then
                            FilePath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_Invoices_New.doc"))
                            Response.AddHeader("content-disposition", "inline; filename=Invoices_New.doc")
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

    Private Sub MergePDF(ByVal File1 As String, ByVal File2 As String)
        Dim fileArray As String() = New String(2) {}
        fileArray(0) = File1
        fileArray(1) = File2
        Dim reader As PdfReader = Nothing
        Dim sourceDocument As Document = Nothing
        Dim pdfCopyProvider As PdfCopy = Nothing
        Dim importedPage As PdfImportedPage
        Dim outputPdfPath As String = ""
        '  Dim FilePath1 As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "_ServiceReports.PDF")

        outputPdfPath = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo")) + "withReports.PDF")
        sourceDocument = New Document()
        pdfCopyProvider = New PdfCopy(sourceDocument, New System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create))
        sourceDocument.Open()
        '    InsertIntoTblWebEventLog("MergePDF", outputPdfPath, "")

        For f As Integer = 0 To fileArray.Length - 1 - 1
            Dim pages As Integer = TotalPageCount(fileArray(f))
            reader = New PdfReader(fileArray(f))

            For i As Integer = 1 To pages
                importedPage = pdfCopyProvider.GetImportedPage(reader, i)
                pdfCopyProvider.AddPage(importedPage)
            Next
            InsertIntoTblWebEventLog("MergePDF", f.ToString, pages.ToString)

            reader.Close()
        Next

        sourceDocument.Close()
    End Sub

    Private Shared Function TotalPageCount(ByVal file As String) As Integer
        Using sr As StreamReader = New StreamReader(System.IO.File.OpenRead(file))
            Dim regex As Regex = New Regex("/Type\s*/Page[^s]")
            Dim matches As MatchCollection = regex.Matches(sr.ReadToEnd())
            Return matches.Count
        End Using
    End Function

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

    Public Shared Sub MergeFiles(ByVal destinationFile As String, ByVal sourceFiles As String())
        Try
            Dim f As Integer = 0
            Dim reader As PdfReader = New PdfReader(sourceFiles(f))
            'TextBox1.text = ""
            Dim n As Integer = reader.NumberOfPages
            Dim document As Document = New Document(reader.GetPageSizeWithRotation(1))
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(destinationFile, FileMode.Create))
            document.Open()
            Dim cb As PdfContentByte = writer.DirectContent
            Dim page As PdfImportedPage
            Dim rotation As Integer

            While f < sourceFiles.Length
                Dim i As Integer = 0

                While i < n
                    i += 1
                    document.SetPageSize(reader.GetPageSizeWithRotation(i))
                    document.NewPage()
                    page = writer.GetImportedPage(reader, i)
                    rotation = reader.GetPageRotation(i)

                    If rotation = 90 OrElse rotation = 270 Then
                        cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(i).Height)
                    Else
                        cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0)
                    End If
                End While

                f += 1

                If f < sourceFiles.Length Then
                    reader = New PdfReader(sourceFiles(f))
                    n = reader.NumberOfPages
                End If
            End While

            document.Close()
        Catch e As Exception
            Dim strOb As String = e.Message
        End Try
    End Sub

    Public Function CountPageNo(ByVal strFileName As String) As Integer
        Dim reader As PdfReader = New PdfReader(strFileName)
        Return reader.NumberOfPages
    End Function


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
            insCmds.Parameters.AddWithValue("@LoginId", "RV-TAXINVOICE15 - " + Session("userid"))
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

    Private Sub IndividualorMergedFile()
        txtFileType.Text = ""
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        ''''''''''''''''''''''''''''''''''''''''''''''''
        Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
        commandServiceRecordMasterSetup.CommandType = CommandType.Text
        commandServiceRecordMasterSetup.CommandText = "SELECT InvoiceSvcReportIndividualFile,InvoiceSvcReportMergedFile FROM tblservicerecordmastersetup where rcno=1"
        commandServiceRecordMasterSetup.Connection = conn

        Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
        Dim dtServiceRecordMasterSetup As New DataTable
        dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

        If String.IsNullOrEmpty(dtServiceRecordMasterSetup.Rows(0)("InvoiceSvcReportIndividualFile")) = False Then
            If dtServiceRecordMasterSetup.Rows(0)("InvoiceSvcReportIndividualFile") = 1 Then
                txtFileType.Text = "Individual"
            End If
        End If
        If String.IsNullOrEmpty(dtServiceRecordMasterSetup.Rows(0)("InvoiceSvcReportMergedFile")) = False Then
            If dtServiceRecordMasterSetup.Rows(0)("InvoiceSvcReportMergedFile") = 1 Then
                txtFileType.Text = "Merged"
            End If
        End If

        If String.IsNullOrEmpty(txtFileType.Text) Then
            txtFileType.Text = "Merged"
        End If

        commandServiceRecordMasterSetup.Dispose()
        dtServiceRecordMasterSetup.Dispose()
        drServiceRecordMasterSetup.Close()
        conn.Close()
        conn.Dispose()

    End Sub

    'Public Shared Sub Merge(ByVal filesToMerge As String(), ByVal outputFilename As String, ByVal insertPageBreaks As Boolean, ByVal documentTemplate As String)
    '    Dim defaultTemplate As Object = documentTemplate
    '    Dim missing As Object = System.Type.Missing
    '    Dim pageBreak As Object = Word.WdBreakType.wdSectionBreakNextPage
    '    Dim outputFile As Object = outputFilename
    '    Dim wordApplication As Word._Application = New Word.Application()

    '    Try
    '        Dim wordDocument As Word.Document = wordApplication.Documents.Add(missing, missing, missing, missing)
    '        Dim selection As Word.Selection = wordApplication.Selection
    '        Dim documentCount As Integer = filesToMerge.Length
    '        Dim breakStop As Integer = 0

    '        For Each file As String In filesToMerge
    '            breakStop += 1
    '            selection.InsertFile(file, missing, missing, missing, missing)

    '            If insertPageBreaks AndAlso breakStop <> documentCount Then
    '                selection.InsertBreak(pageBreak)
    '            End If
    '        Next

    '        wordDocument.SaveAs(outputFile, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing)
    '        wordDocument = Nothing
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        wordApplication.Quit(missing, missing, missing)
    '    End Try
    'End Sub
End Class
