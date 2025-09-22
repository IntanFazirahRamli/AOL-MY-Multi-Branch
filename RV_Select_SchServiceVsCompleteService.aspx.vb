Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_SchServiceVsCompleteService
    Inherits System.Web.UI.Page
    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        selFormula = "{tblservicerecord1.rcno} <> 0 and ({tblservicerecord1.Status} = 'P' or {tblservicerecord1.Status} = 'O') "
        Dim selection As String
        selection = ""
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            '  qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                Return
            End If
            selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
        '    End If
        'End If

        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

            selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            '  qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If

        If ddlIncharge.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.serviceby} = '" + ddlIncharge.Text + "'"
            If selection = "" Then
                selection = "ServiceBy = " + ddlIncharge.Text
            Else
                selection = selection + ", ServiceBy = " + ddlIncharge.Text
            End If
        End If

        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    '  qry = qry + " and tblcontract1.ContractGroup = '" + ddlContractGroup.Text + "'"

        '    selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "ContractGroup = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + ", ContractGroup = " + ddlContractGroup.Text
        '    End If
        'End If

        Dim YrStrListContGrp As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlContractGroup.Items
            If item.Selected Then

                YrStrListContGrp.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListContGrp.Count > 0 Then

            Dim YrStrContGrp As [String] = [String].Join(",", YrStrListContGrp.ToArray)

            selFormula = selFormula + " and {tblcontract1.ContractGroup} in [" + YrStrContGrp + "]"
            If selection = "" Then
                selection = "ContractGroup : " + YrStrContGrp
            Else
                selection = selection + ", ContractGroup : " + YrStrContGrp
            End If
            '   qry = qry + " and tblservicerecord.LocateGrp in [" + YrStrContGrp + "]"
        End If


        'If ddlZone.Text = "-1" Then
        'Else

        '    selFormula = selFormula + " and {tblservicerecord1.locategrp} = '" + ddlZone.Text + "'"
        '    If selection = "" Then
        '        selection = "Zone = " + ddlZone.Text
        '    Else
        '        selection = selection + ", Zone = " + ddlZone.Text
        '    End If
        'End If

        InsertIntoTblWebEventLog("btnPrint", selFormula, "")



        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If chkGrouping.SelectedIndex = 0 Then
            Session.Add("ReportType", "SchServiceVsCompService")

            Response.Redirect("RV_SchServiceVsCompService.aspx")
        ElseIf chkGrouping.SelectedIndex = 1 Then
            mdlSel.Show()

        ElseIf chkGrouping.SelectedIndex = 2 Then
            mdlZone.Show()
        End If

    End Sub

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "SchSvcVsCompleteSvc - " + Session("UserID"))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnPrintZone_Click(sender As Object, e As EventArgs) Handles btnPrintZone.Click
        Dim selFormula As String = Convert.ToString(Session("selFormula"))
        Dim selection As String = Convert.ToString(Session("selection"))

        Dim YrStrListZone As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
            If item.Selected Then

                YrStrListZone.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListZone.Count > 0 Then

            Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)

            selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
            '   qry = qry + " and tblservicerecord.LocateGrp in [" + YrStrZone + "]"
        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        Session.Add("ReportType", "SchServiceVsCompServiceZone")

        Response.Redirect("RV_SchServiceVsCompService.aspx")
    End Sub

    Protected Sub btnExportZone_Click(sender As Object, e As EventArgs) Handles btnExportZone.Click
        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()
            Dim attachment As String = "attachment; filename=SchServiceVsCompletedService.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.ms-excel"
            Dim tab As String = ""
            For Each dc As DataColumn In dt.Columns
                Response.Write(tab + dc.ColumnName)
                tab = vbTab
            Next
            Response.Write(vbLf)
            Dim i As Integer
            For Each dr As DataRow In dt.Rows
                tab = ""
                For i = 0 To dt.Columns.Count - 1
                    Response.Write(tab & dr(i).ToString())
                    tab = vbTab
                Next
                Response.Write(vbLf)
            Next
            '   Response.[End]()

            dt.Clear()

            '    If GetData() = True Then
            dt = GetDataSetTotal()
            '  Response.Write(vbLf)
            '  Response.Write(vbLf)
            For Each dr As DataRow In dt.Rows
                tab = ""
                For i = 0 To dt.Columns.Count - 1
                    If i = 0 Then
                        Response.Write(tab)
                    ElseIf i = 1 Then
                        Response.Write(tab)
                    Else
                        Response.Write(tab & dr(i).ToString())
                    End If

                    tab = vbTab
                Next
                Response.Write(vbLf)
            Next

            dt.Clear()
            'End If

            If GetData2() = True Then
                dt = GetDataSet()
                Response.Write(vbLf)
                Response.Write(vbLf)
                For Each dr As DataRow In dt.Rows
                    tab = ""
                    For i = 0 To dt.Columns.Count - 1
                        Response.Write(tab & dr(i).ToString())
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                Next
                Response.[End]()

                dt.Clear()
            End If
        End If
    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click


        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        ddlIncharge.SelectedIndex = 0
    End Sub

    Protected Sub btnPrintSel_Click(sender As Object, e As EventArgs) Handles btnPrintSel.Click
        If rdbSelect.SelectedIndex = 0 Then
            Session.Add("ReportType", "SchServiceVsCompServiceCompGrpDet")

            Response.Redirect("RV_SchServiceVsCompService.aspx")
        ElseIf rdbSelect.SelectedIndex = 1 Then
            Session.Add("ReportType", "SchServiceVsCompServiceCompGrpSumm")

            Response.Redirect("RV_SchServiceVsCompService.aspx")
        End If
    End Sub


    'Public Sub ConvertExcel(ByVal fileNames As String)
    '    Dim pageCount As Integer = 0
    '    Dim reader As iTextSharp.text.pdf.PdfReader = New iTextSharp.text.pdf.PdfReader(fileNames)
    '    pageCount = reader.NumberOfPages
    '    Dim ext As String = System.IO.Path.GetExtension(fileNames)
    '    Dim extractor As CSVExtractor = New CSVExtractor()
    '    Dim outfilePDFExcel1 As String = fileNames.Replace((System.IO.Path.GetFileName(fileNames)), (System.IO.Path.GetFileName(fileNames).Replace(".pdf", "") & "_rez" & ".xls"))
    '    extractor.RegistrationName = "demo"
    '    extractor.RegistrationKey = "demo"
    '    Dim folderName As String = "C:\Users\Dafina\Desktop\PDF_EditProject\PDF_EditProject\PDFs"
    '    Dim pathString As String = System.IO.Path.Combine(folderName, System.IO.Path.GetFileName(fileNames).Replace(".pdf", "")) & "-CSVs"
    '    System.IO.Directory.CreateDirectory(pathString)

    '    For i As Integer = 0 To pageCount - 1
    '        Dim outfilePDF As String = fileNames.Replace((System.IO.Path.GetFileName(fileNames)), (System.IO.Path.GetFileName(fileNames).Replace(".pdf", "") & "_" & (i + 1).ToString()) & ext)
    '        extractor.LoadDocumentFromFile(outfilePDF)
    '        Dim outfile As String = fileNames.Replace((System.IO.Path.GetFileName(fileNames)), (System.IO.Path.GetFileName(fileNames).Replace(".pdf", "") & "-CSVs\" & "Sheet_" & (i + 1).ToString()) & ".csv")
    '        extractor.SaveCSVToFile(outfile)
    '    Next

    '    Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()

    '    If xlApp Is Nothing Then
    '        Console.WriteLine("Excel is not properly installed!!")
    '        Return
    '    End If

    '    Dim xlWorkBook As Excel.Workbook
    '    Dim misValue As Object = System.Reflection.Missing.Value
    '    xlWorkBook = xlApp.Workbooks.Add(misValue)
    '    Dim cvsFiles As String() = Directory.GetFiles(pathString)
    '    Array.Sort(cvsFiles, New AlphanumComparatorFast())
    '    Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet

    '    For i As Integer = 0 To cvsFiles.Length - 1
    '        Dim sheet As Integer = i + 1
    '        xlWorkSheet = xlWorkBook.Sheets(sheet)

    '        If i < cvsFiles.Length - 1 Then
    '            xlWorkBook.Worksheets.Add(Type.Missing, xlWorkSheet, Type.Missing, Type.Missing)
    '        End If

    '        Dim sheetRow As Integer = 1
    '        Dim objEncoding As Encoding = Encoding.[Default]
    '        Dim readerd As StreamReader = New StreamReader(File.OpenRead(cvsFiles(i)))
    '        Dim ColumLength As Integer = 0

    '        While Not readerd.EndOfStream
    '            Dim line As String = readerd.ReadLine()
    '            Console.WriteLine(line)

    '            Try
    '                Dim columns As String() = line.Split((New Char() {""""c}))

    '                For col As Integer = 0 To columns.Length - 1

    '                    If ColumLength < columns.Length Then
    '                        ColumLength = columns.Length
    '                    End If

    '                    If col Mod 2 = 0 Then
    '                    ElseIf columns(col) = "" Then
    '                    Else
    '                        xlWorkSheet.Cells(sheetRow, col + 1) = columns(col).Replace("""", "")
    '                    End If
    '                Next

    '                sheetRow += 1
    '            Catch e As Exception
    '                Dim msg As String = e.Message
    '            End Try
    '        End While

    '        Dim k As Integer = 1

    '        For s As Integer = 1 To ColumLength
    '            xlWorkSheet.Columns(k).Delete()
    '            k += 1
    '        Next

    '        releaseObject(xlWorkSheet)
    '        readerd.Close()
    '    Next

    '    xlWorkBook.SaveAs(outfilePDFExcel1, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
    '    xlWorkBook.Close(True, misValue, misValue)
    '    xlApp.Quit()
    '    releaseObject(xlWorkBook)
    '    releaseObject(xlApp)
    '    Dim dir = New DirectoryInfo(pathString)
    '    dir.Attributes = dir.Attributes And Not FileAttributes.[ReadOnly]
    '    dir.Delete(True)
    'End Sub


    'Private Sub ExportPDFToExcel(ByVal fileName As String)




    '    Dim text As StringBuilder = New StringBuilder()
    '    Dim pdfReader As PdfReader = New PdfReader(fileName)

    '    For page As Integer = 1 To pdfReader.NumberOfPages

    '        Dim strategy As ITextExtractionStrategy = New LocationTextExtractionStrategy()
    '        Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
    '        currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.[Default], Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)))
    '        text.Append(currentText)
    '        pdfReader.Close()
    '    Next

    '    Response.Clear()
    '    Response.Buffer = True
    '    Response.AddHeader("content-disposition", "attachment;filename=ReceiptExport.xls")
    '    Response.Charset = ""
    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.Write(text)
    '    Response.Flush()
    '    Response.[End]()


    'End Sub

    'Protected Sub ExportToExcel(filename As String)
    '    If String.IsNullOrEmpty(filename) = False Then
    '        Dim currentText As String = String.Empty
    '        Dim text As StringBuilder = New StringBuilder()
    '        Dim pdfReader As PdfReader = New PdfReader(filename)

    '        For page As Integer = 1 To pdfReader.NumberOfPages
    '            Dim strategy As ITextExtractionStrategy = New LocationTextExtractionStrategy()
    '            currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
    '            currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.[Default], Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)))
    '            text.Append(currentText)
    '            pdfReader.Close()
    '        Next

    '        Dim data As String() = currentText.Split(vbLf)
    '        Dim dt As DataTable = New DataTable("PdfTable")
    '        Dim headers As String() = data(0).Split(" "c)

    '        For j As Integer = 0 To headers.Length - 1

    '            If Not String.IsNullOrEmpty(headers(j)) Then
    '                dt.Columns.Add(headers(j), GetType(String))
    '            End If
    '        Next

    '        For i As Integer = 1 To data.Length - 1
    '            Dim content As String() = data(i).Split(" "c)
    '            dt.Rows.Add()

    '            'For k As Integer = 0 To content.Length - 1

    '            '    If Not String.IsNullOrEmpty(content(k)) Then
    '            '        dt.Rows(dt.Rows.Count - 1)(k) = content(k)
    '            '    End If
    '            'Next
    '        Next

    '        ' Using wb As New Excel.Workbook
    '        '  wb.Worksheets.Add(dt)
    '        Dim wb As New Excel.Workbook
    '        wb.Worksheets.Add(dt)
    '        Response.Clear()
    '        Response.Buffer = True
    '        Response.Charset = ""
    '        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '        Response.AddHeader("content-disposition", "attachment;filename=Excel.xls")

    '        Using MyMemoryStream As MemoryStream = New MemoryStream()
    '            wb.SaveAs(MyMemoryStream)
    '            MyMemoryStream.WriteTo(Response.OutputStream)
    '            Response.Flush()
    '            Response.[End]()
    '        End Using
    '        'End Using
    '    End If
    'End Sub

    'Private Sub SurroundingSub(filename As String)
    '    Dim pathToPdf As String = filename
    '    Dim pathToExcel As String = System.IO.Path.ChangeExtension(pathToPdf, ".xls")
    '    Dim f As SautinSoft.PdfFocus = New SautinSoft.PdfFocus()
    '    f.ExcelOptions.ConvertNonTabularDataToSpreadsheet = True
    '    f.ExcelOptions.PreservePageLayout = True
    '    f.OpenPdf(pathToPdf)

    '    If f.PageCount > 0 Then
    '        Dim result As Integer = f.ToExcel(pathToExcel)

    '        If result = 0 Then
    '            System.Diagnostics.Process.Start(pathToExcel)
    '        End If
    '    End If
    'End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=SchServiceVsCompletedService.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.ms-excel"
            Dim tab As String = ""
            For Each dc As DataColumn In dt.Columns
                Response.Write(tab + dc.ColumnName)
                tab = vbTab
            Next
            Response.Write(vbLf)
            Dim i As Integer
            For Each dr As DataRow In dt.Rows
                tab = ""
                For i = 0 To dt.Columns.Count - 1
                    Response.Write(tab & dr(i).ToString())
                    tab = vbTab
                Next
                Response.Write(vbLf)
            Next
            '   Response.[End]()

            dt.Clear()

            '    If GetData() = True Then
            dt = GetDataSetTotal()
            '  Response.Write(vbLf)
            '  Response.Write(vbLf)
            For Each dr As DataRow In dt.Rows
                tab = ""
                For i = 0 To dt.Columns.Count - 1
                    If i = 0 Then
                        Response.Write(tab)
                    ElseIf i = 1 Then
                        Response.Write(tab)
                    Else
                        Response.Write(tab & dr(i).ToString())
                    End If

                    tab = vbTab
                Next
                Response.Write(vbLf)
            Next

            dt.Clear()
            'End If

            If GetData2() = True Then
                dt = GetDataSet()
                Response.Write(vbLf)
                Response.Write(vbLf)
                '' For Each dc As DataColumn In dt.Columns
                'Response.Write(vbTab + "ContractGroup")
                'Response.Write(vbTab + "TotalAmount")
                'Response.Write(vbTab + "CorporateAmount")
                'Response.Write(vbTab + "ResidentialAmount")
                'Response.Write(vbTab + "TotalTimeSpent")
                'tab = vbTab
                ''  Next
                'Response.Write(vbLf)
                For Each dc As DataColumn In dt.Columns
                    Response.Write(dc.ColumnName + tab)
                    tab = vbTab
                Next
                Response.Write(vbLf)
                For Each dr As DataRow In dt.Rows
                    tab = ""
                    For i = 0 To dt.Columns.Count - 1
                        Response.Write(tab & dr(i).ToString())
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                Next
              

                dt.Clear()

                dt = GetDataSetTotal2()
              
                For Each dr As DataRow In dt.Rows
                    tab = ""
                    For i = 0 To dt.Columns.Count - 1
                        If i = 0 Then
                            Response.Write("GrandTotal")
                        Else
                            Response.Write(tab & dr(i).ToString())
                        End If

                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                Next
                Response.[End]()

                dt.Clear()
            End If
        End If

    End Sub
    Private Function GetData() As Boolean
        Dim selFormula As String
        Dim selection As String
        selection = ""
        selFormula = "{tblservicerecord1.rcno} <> 0 and ({tblservicerecord1.Status} = 'P' or {tblservicerecord1.Status} = 'O') "

        Dim qry As String = "select "
        If chkGrouping.SelectedIndex = 2 Then
            qry = qry + "tblservicerecord.LocateGrp As Zone, "
        End If
        qry = qry + "ServiceDate, DAYNAME(ServiceDate) As DOW,"
        qry = qry + " count(Case When tblservicerecord.status In ('O','P') then RecordNo end) as TotalScheduleBooked,ifnull(sum(case when tblservicerecord.status in ('O','P') then billamount end),0) as TotalAmount,"
        qry = qry + " count(case when tblservicerecord.status in ('O') then RecordNo end) as ActiveTotalScheduleBooked,ifnull(sum(case when tblservicerecord.status in ('O') then billamount end),0) as ActiveTotalAmount,"
        qry = qry + " count(case when tblservicerecord.status in ('P') then RecordNo end) as CompletedTotalScheduleBooked,ifnull(sum(case when tblservicerecord.status in ('P') then billamount end),0) as CompletedTotalAmount"
        qry = qry + " FROM tblservicerecord,tblcontract where tblservicerecord.contractno=tblcontract.contractno and tblservicerecord.status in ('O','P')"

        selFormula = "{tblservicerecord1.rcno} <> 0 and ({tblservicerecord1.Status} = 'P' or {tblservicerecord1.Status} = 'O') "

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                Return False
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "SchService Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", SchService Date >= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblservicerecord.ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        End If

        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                Return False
            End If
            selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "SchService Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", SchService Date <= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblservicerecord.ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

        End If

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
        '    End If
        'End If

        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

            selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            qry = qry + " and tblservicerecord.CompanyGroup in (" + YrStr + ")"
        End If

        If ddlIncharge.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.serviceby} = '" + ddlIncharge.Text + "'"
            If selection = "" Then
                selection = "ServiceBy = " + ddlIncharge.Text
            Else
                selection = selection + ", ServiceBy = " + ddlIncharge.Text
            End If
            qry = qry + " and tblservicerecord.serviceby = '" + ddlIncharge.Text + "'"
        End If

        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    qry = qry + " and tblcontract.ContractGroup = '" + ddlContractGroup.Text + "'"

        '    selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "ContractGroup = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + ", ContractGroup = " + ddlContractGroup.Text
        '    End If
        'End If

        Dim YrStrListContGrp As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlContractGroup.Items
            If item.Selected Then

                YrStrListContGrp.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListContGrp.Count > 0 Then

            Dim YrStrContGrp As [String] = [String].Join(",", YrStrListContGrp.ToArray)

            selFormula = selFormula + " and {tblcontract1.ContractGroup} in [" + YrStrContGrp + "]"
            If selection = "" Then
                selection = "ContractGroup : " + YrStrContGrp
            Else
                selection = selection + ", ContractGroup : " + YrStrContGrp
            End If
            qry = qry + " and tblcontract.ContractGroup in (" + YrStrContGrp + ")"
        End If

        'If ddlZone.Text = "-1" Then
        'Else
        '    qry = qry + " and tblservicerecord.LOCATEGRP = '" + ddlZone.Text + "'"
        '    selFormula = selFormula + " and {tblservicerecord1.locategrp} = '" + ddlZone.Text + "'"
        '    If selection = "" Then
        '        selection = "Zone = " + ddlZone.Text
        '    Else
        '        selection = selection + ", Zone = " + ddlZone.Text
        '    End If
        'End If

        Dim YrStrListZone As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
            If item.Selected Then

                YrStrListZone.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListZone.Count > 0 Then

            Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)

            selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
            qry = qry + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"
        End If

        Session.Add("selection", selection)
        txtQueryTotal.Text = qry


        If chkGrouping.SelectedIndex = 0 Then
            qry = qry + " group by ServiceDate"
        ElseIf chkGrouping.SelectedIndex = 1 Then
            qry = qry + " group by ServiceDate"

        ElseIf chkGrouping.SelectedIndex = 2 Then
            qry = qry + " group by tblservicerecord.LocateGrp,ServiceDate"
        End If


        txtQuery.Text = qry

        Return True

    End Function

    Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand
        '   Dim cmd1 As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQuery.Text

        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            '' Dim drRow As New DataRow
            'DataRow drRow = dt.NewRow()

            'Dim sda1 As New MySqlDataAdapter()
            'cmd1.CommandType = CommandType.Text
            'cmd1.CommandText = txtQueryTotal.Text

            'cmd1.Connection = conn

            'sda.SelectCommand = cmd1
            'sda1.
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function



    Private Function GetData2() As Boolean
        Dim selFormula As String
        Dim selection As String
        selection = ""

        If chkDistribution.Checked Then

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmd As MySqlCommand = New MySqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = conn

            '   InsertIntoTblWebEventLog("GetData", Convert.ToString(Session("LocationEnabled")), "1")
            'If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            '    cmd.CommandText = "spActualRevenueDistributionLocation"
            'Else
            cmd.CommandText = "spSchServiceCompServiceDistribution"

            '  End If



            cmd.Parameters.Clear()
            'InsertIntoTblWebEventLog("GetData", Session("UserID"), "3")
            'If Convert.ToString(Session("LocationEnabled")) = "Y" Then

            '    cmd.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
            'End If
            cmd.Parameters.AddWithValue("pr_CreatedBy", Session("UserID"))
            cmd.Parameters.AddWithValue("pr_ReportName", "SchSvcVsCompSvc")

            If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                    lblAlert.Text = "INVALID SERVICE DATE FROM"
                    Return False
                End If
                cmd.Parameters.AddWithValue("pr_startdate", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))

            Else
                cmd.Parameters.AddWithValue("pr_startdate", DBNull.Value)

            End If

            If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                    lblAlert.Text = "INVALID SERVICE DATE TO"
                    Return False
                End If
                cmd.Parameters.AddWithValue("pr_enddate", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))

            Else
                cmd.Parameters.AddWithValue("pr_enddate", DBNull.Value)

            End If

            cmd.ExecuteScalar()

            cmd.Dispose()

            conn.Close()
            conn.Dispose()

            Dim qry As String = "SELECT ContractGroup,sum(TotalAmount) as TotalAmount,"
            qry = qry + "sum(if(ContactType='COMPANY',TotalAmount,0)) as CorporateRevenue,"
            qry = qry + "sum(if(ContactType='PERSON',TotalAmount,0)) as ResidentialRevenue,"
            qry = qry + "SUM(Duration) as TotalTimeSpent FROM tbwSchSvcComSvc where rcno <> 0 "

            selFormula = "{tblservicerecord1.rcno} <> 0 and ({tblservicerecord1.Status} = 'P' or {tblservicerecord1.Status} = 'O') "

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                qry = qry + " and location in (" + Convert.ToString(Session("Branch")) + ")"
                If selection = "" Then
                    selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                Else
                    selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                End If
            End If

            If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Else
                    MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                    Return False
                End If
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                If selection = "" Then
                    selection = "SchService Date >= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", SchService Date >= " + d.ToString("dd-MM-yyyy")
                End If
                qry = qry + " AND ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            End If

            If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Else
                    MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                    Return False
                End If
                selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                If selection = "" Then
                    selection = "SchService Date <= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", SchService Date <= " + d.ToString("dd-MM-yyyy")
                End If
                qry = qry + " AND ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

            End If

            'If ddlCompanyGrp.Text = "-1" Then
            'Else
            '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
            '    If selection = "" Then
            '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
            '    Else
            '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
            '    End If
            'End If

            Dim YrStrList1 As List(Of [String]) = New List(Of String)()

            For Each item As System.Web.UI.WebControls.ListItem In ddlCompanyGrp.Items
                If item.Selected Then

                    YrStrList1.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList1.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

                selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
                If selection = "" Then
                    selection = "CompanyGroup : " + YrStr
                Else
                    selection = selection + ", CompanyGroup : " + YrStr
                End If
                qry = qry + " and CompanyGroup in (" + YrStr + ")"
            End If

            If ddlIncharge.Text = "-1" Then
            Else
                selFormula = selFormula + " and {tblservicerecord1.serviceby} = '" + ddlIncharge.Text + "'"
                If selection = "" Then
                    selection = "ServiceBy = " + ddlIncharge.Text
                Else
                    selection = selection + ", ServiceBy = " + ddlIncharge.Text
                End If
                qry = qry + " and serviceby = '" + ddlIncharge.Text + "'"
            End If

            '    qry = qry + " group by tblcontract1.ContractGroup"
            'If ddlContractGroup.Text = "-1" Then
            'Else
            '    qry = qry + " and tblcontract.ContractGroup = '" + ddlContractGroup.Text + "'"

            '    selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
            '    If selection = "" Then
            '        selection = "ContractGroup = " + ddlContractGroup.Text
            '    Else
            '        selection = selection + ", ContractGroup = " + ddlContractGroup.Text
            '    End If
            'End If

            Dim YrStrListContGrp As List(Of [String]) = New List(Of String)()

            For Each item As System.Web.UI.WebControls.ListItem In ddlContractGroup.Items
                If item.Selected Then

                    YrStrListContGrp.Add("""" + item.Value + """")

                End If
            Next

            If YrStrListContGrp.Count > 0 Then

                Dim YrStrContGrp As [String] = [String].Join(",", YrStrListContGrp.ToArray)

                selFormula = selFormula + " and {tblcontract1.ContractGroup} in [" + YrStrContGrp + "]"
                If selection = "" Then
                    selection = "ContractGroup : " + YrStrContGrp
                Else
                    selection = selection + ", ContractGroup : " + YrStrContGrp
                End If
                qry = qry + " and ContractGroup in (" + YrStrContGrp + ")"
            End If

            'If ddlZone.Text = "-1" Then
            'Else
            '    qry = qry + " and tblservicerecord.LOCATEGRP = '" + ddlZone.Text + "'"
            '    selFormula = selFormula + " and {tblservicerecord1.locategrp} = '" + ddlZone.Text + "'"
            '    If selection = "" Then
            '        selection = "Zone = " + ddlZone.Text
            '    Else
            '        selection = selection + ", Zone = " + ddlZone.Text
            '    End If
            'End If

            Dim YrStrListZone As List(Of [String]) = New List(Of String)()

            For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
                If item.Selected Then
                    YrStrListZone.Add("""" + item.Value + """")
                End If
            Next

            If YrStrListZone.Count > 0 Then

                Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)

                selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
                If selection = "" Then
                    selection = "Zone : " + YrStrZone
                Else
                    selection = selection + ", Zone : " + YrStrZone
                End If
                qry = qry + " and LocateGrp in (" + YrStrZone + ")"
            End If
            txtQueryTotal2.Text = qry

            qry = qry + " group by ContractGroup"

            txtQuery.Text = qry

            Return True

        Else

            Dim qry As String = "SELECT tblcontract1.ContractGroup, sum(tblservicerecord1.BillAmount) as TotalAmount,sum(if(tblservicerecord1.ContactType='COMPANY',tblservicerecord1.BillAmount,0)) as CorporateRevenue"
            qry = qry + ",sum(if(tblservicerecord1.ContactType='PERSON',tblservicerecord1.BillAmount,0)) as ResidentialRevenue,SUM(tblservicerecord1.Duration) as TotalTimeSpent"
            qry = qry + " FROM tblservicerecord tblservicerecord1 LEFT OUTER JOIN tblcontract tblcontract1 "
            qry = qry + "ON trim(tblservicerecord1.ContractNo)=tblcontract1.ContractNo where tblservicerecord1.status in ('O','P')"

            selFormula = "{tblservicerecord1.rcno} <> 0 and ({tblservicerecord1.Status} = 'P' or {tblservicerecord1.Status} = 'O') "

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                qry = qry + " and tblservicerecord1.location in (" + Convert.ToString(Session("Branch")) + ")"
                If selection = "" Then
                    selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                Else
                    selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                End If
            End If

            If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Else
                    MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                    Return False
                End If
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                If selection = "" Then
                    selection = "SchService Date >= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", SchService Date >= " + d.ToString("dd-MM-yyyy")
                End If
                qry = qry + " AND tblservicerecord1.ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            End If

            If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Else
                    MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                    Return False
                End If
                selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                If selection = "" Then
                    selection = "SchService Date <= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", SchService Date <= " + d.ToString("dd-MM-yyyy")
                End If
                qry = qry + " AND tblservicerecord1.ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

            End If

            'If ddlCompanyGrp.Text = "-1" Then
            'Else
            '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
            '    If selection = "" Then
            '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
            '    Else
            '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
            '    End If
            'End If

            Dim YrStrList1 As List(Of [String]) = New List(Of String)()

            For Each item As System.Web.UI.WebControls.ListItem In ddlCompanyGrp.Items
                If item.Selected Then

                    YrStrList1.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList1.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

                selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
                If selection = "" Then
                    selection = "CompanyGroup : " + YrStr
                Else
                    selection = selection + ", CompanyGroup : " + YrStr
                End If
                qry = qry + " and tblservicerecord1.CompanyGroup in (" + YrStr + ")"
            End If

            If ddlIncharge.Text = "-1" Then
            Else
                selFormula = selFormula + " and {tblservicerecord1.serviceby} = '" + ddlIncharge.Text + "'"
                If selection = "" Then
                    selection = "ServiceBy = " + ddlIncharge.Text
                Else
                    selection = selection + ", ServiceBy = " + ddlIncharge.Text
                End If
                qry = qry + " and tblservicerecord1.serviceby = '" + ddlIncharge.Text + "'"
            End If

            '    qry = qry + " group by tblcontract1.ContractGroup"
            'If ddlContractGroup.Text = "-1" Then
            'Else
            '    qry = qry + " and tblcontract.ContractGroup = '" + ddlContractGroup.Text + "'"

            '    selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
            '    If selection = "" Then
            '        selection = "ContractGroup = " + ddlContractGroup.Text
            '    Else
            '        selection = selection + ", ContractGroup = " + ddlContractGroup.Text
            '    End If
            'End If

            Dim YrStrListContGrp As List(Of [String]) = New List(Of String)()

            For Each item As System.Web.UI.WebControls.ListItem In ddlContractGroup.Items
                If item.Selected Then

                    YrStrListContGrp.Add("""" + item.Value + """")

                End If
            Next

            If YrStrListContGrp.Count > 0 Then

                Dim YrStrContGrp As [String] = [String].Join(",", YrStrListContGrp.ToArray)

                selFormula = selFormula + " and {tblcontract1.ContractGroup} in [" + YrStrContGrp + "]"
                If selection = "" Then
                    selection = "ContractGroup : " + YrStrContGrp
                Else
                    selection = selection + ", ContractGroup : " + YrStrContGrp
                End If
                qry = qry + " and tblcontract1.ContractGroup in (" + YrStrContGrp + ")"
            End If

            'If ddlZone.Text = "-1" Then
            'Else
            '    qry = qry + " and tblservicerecord.LOCATEGRP = '" + ddlZone.Text + "'"
            '    selFormula = selFormula + " and {tblservicerecord1.locategrp} = '" + ddlZone.Text + "'"
            '    If selection = "" Then
            '        selection = "Zone = " + ddlZone.Text
            '    Else
            '        selection = selection + ", Zone = " + ddlZone.Text
            '    End If
            'End If

            Dim YrStrListZone As List(Of [String]) = New List(Of String)()

            For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
                If item.Selected Then

                    YrStrListZone.Add("""" + item.Value + """")

                End If
            Next

            If YrStrListZone.Count > 0 Then

                Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)

                selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
                If selection = "" Then
                    selection = "Zone : " + YrStrZone
                Else
                    selection = selection + ", Zone : " + YrStrZone
                End If
                qry = qry + " and tblservicerecord1.LocateGrp in (" + YrStrZone + ")"
            End If
            txtQueryTotal2.Text = qry

            qry = qry + " group by tblcontract1.ContractGroup"

            txtQuery.Text = qry

            Return True
        End If

    End Function

    Private Function GetDataSet2() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQuery.Text

        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function

    Private Function GetDataSetTotal() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQueryTotal.Text

        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function

    Private Function GetDataSetTotal2() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQueryTotal2.Text

        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function

    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String)
        Dim workbook As IWorkbook

        If extension = "xlsx" Then
            workbook = New XSSFWorkbook()
        ElseIf extension = "xls" Then
            workbook = New HSSFWorkbook()
        Else
            Throw New Exception("This format is not supported")
        End If

        Dim sheet1 As ISheet = workbook.CreateSheet("Sheet1")

        'Add Selection Criteria

        Dim row1 As IRow = sheet1.CreateRow(0)
        Dim cell1 As ICell = row1.CreateCell(0)
        '  cell1.SetCellValue(Session("Selection").ToString)
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + Session("Selection").ToString)

        cell1.CellStyle.WrapText = True
        Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8)
        sheet1.AddMergedRegion(cra)

        'Add Column Heading
        row1 = sheet1.CreateRow(1)

        Dim testeStyle As ICellStyle = workbook.CreateCellStyle()
        testeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

        testeStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index
        testeStyle.FillPattern = FillPattern.SolidForeground
        '  testeStyle.FillForegroundColor = IndexedColors.White.Index
        testeStyle.Alignment = HorizontalAlignment.Center

        Dim RowFont As IFont = workbook.CreateFont()
        RowFont.Color = IndexedColors.White.Index
        RowFont.IsBold = True

        testeStyle.SetFont(RowFont)

        For j As Integer = 0 To dt.Columns.Count - 1
            Dim cell As ICell = row1.CreateCell(j)
            '  InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

            Dim columnName As String = dt.Columns(j).ToString()
            cell.SetCellValue(columnName)
            ' cell.Row.RowStyle.FillBackgroundColor = IndexedColors.LightBlue.Index
            cell.CellStyle = testeStyle

        Next

        'Add details
        Dim _doubleCellStyle As ICellStyle = Nothing

        If _doubleCellStyle Is Nothing Then
            _doubleCellStyle = workbook.CreateCellStyle()
            _doubleCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
        End If

        Dim _intCellStyle As ICellStyle = Nothing

        If _intCellStyle Is Nothing Then
            _intCellStyle = workbook.CreateCellStyle()
            _intCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0")
        End If

        Dim dateCellStyle As ICellStyle = Nothing

        If dateCellStyle Is Nothing Then
            dateCellStyle = workbook.CreateCellStyle()
            dateCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-mm-yyyy")
        End If

        Dim AllCellStyle As ICellStyle = Nothing

        AllCellStyle = workbook.CreateCellStyle()
        AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium


            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                If j = 3 Or j = 5 Or j = 7 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Double = Convert.ToDouble("0.00")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _doubleCellStyle
                ElseIf j = 2 Or j = 4 Or j = 6 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Int32 = Convert.ToInt32("0")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _intCellStyle
                ElseIf j = 0 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                        cell.SetCellValue(d)
                        'Else
                        '    Dim d As Double = Convert.ToDouble("0.00")
                        '    cell.SetCellValue(d)

                    End If
                    cell.CellStyle = dateCellStyle
                Else
                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                    cell.CellStyle = AllCellStyle
                End If
                If i = dt.Rows.Count - 1 Then
                    sheet1.AutoSizeColumn(j)
                End If
            Next

            Next



        dt.Clear()

        '    If GetData() = True Then
        dt = GetDataSetTotal()
        '  Response.Write(vbLf)
        '  Response.Write(vbLf)

        '  sheet1.CreateRow(sheet1.LastRowNum + 1)
        '  sheet1.CreateRow(sheet1.LastRowNum + 1)

        '  InsertIntoTblWebEventLog("WriteExcel", dt.Rows.Count.ToString, "")

        For i As Integer = 0 To dt.Rows.Count - 1
            '   InsertIntoTblWebEventLog("WriteExcel", dt.Columns.Count.ToString, i.ToString)
            Dim rownum As Integer = sheet1.LastRowNum
            Dim row As IRow = sheet1.CreateRow(rownum + 1)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row.CreateCell(j)
                If j = 0 Then
                    cell.SetCellValue("Total")
                    cell.CellStyle = AllCellStyle
                ElseIf j = 1 Then
                    cell.SetCellValue("")
                    cell.CellStyle = AllCellStyle
                ElseIf j = 3 Or j = 5 Or j = 7 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Double = Convert.ToDouble("0.00")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _doubleCellStyle
                ElseIf j = 2 Or j = 4 Or j = 6 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Int32 = Convert.ToInt32("0")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _intCellStyle
                Else
                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                    cell.CellStyle = AllCellStyle
                End If
                'If i = dt.Rows.Count - 1 Then
                '    sheet1.AutoSizeColumn(j)
                'End If
            Next

        Next

        dt.Clear()

        If GetData2() = True Then
            dt = GetDataSet()

            Dim row2 As IRow = sheet1.CreateRow(sheet1.LastRowNum + 1)
            row2 = sheet1.CreateRow(sheet1.LastRowNum + 1)

            row2 = sheet1.CreateRow(sheet1.LastRowNum + 1)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row2.CreateCell(j)
                '  InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

                Dim columnName As String = dt.Columns(j).ToString()
                cell.SetCellValue(columnName)
                ' cell.Row.RowStyle.FillBackgroundColor = IndexedColors.LightBlue.Index
                cell.CellStyle = testeStyle

            Next
            Dim rownum As Integer = sheet1.LastRowNum
            For i As Integer = 0 To dt.Rows.Count - 1
                rownum = rownum + 1
                Dim row As IRow = sheet1.CreateRow(rownum)
              
                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)
                 
                    If j = 1 Or j = 2 Or j = 3 Or j = 4 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                        'ElseIf j = 4 Then
                        '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        '        Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                        '        cell.SetCellValue(d)
                        '    Else
                        '        Dim d As Int32 = Convert.ToInt32("0")
                        '        cell.SetCellValue(d)

                        '    End If
                        '    cell.CellStyle = _intCellStyle

                    Else
                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                        cell.CellStyle = AllCellStyle
                    End If
                    If i = dt.Rows.Count - 1 Then
                        sheet1.AutoSizeColumn(j)
                    End If
                Next
            Next

        End If

        dt.Clear()

        dt = GetDataSetTotal2()

        For i As Integer = 0 To dt.Rows.Count - 1
            '   InsertIntoTblWebEventLog("WriteExcel", dt.Columns.Count.ToString, i.ToString)
            Dim rownum As Integer = sheet1.LastRowNum
            Dim row As IRow = sheet1.CreateRow(rownum + 1)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row.CreateCell(j)
                If j = 0 Then
                    cell.SetCellValue("GrandTotal")
                    cell.CellStyle = AllCellStyle
                ElseIf j = 1 Or j = 2 Or j = 3 Or j = 4 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Double = Convert.ToDouble("0.00")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _doubleCellStyle
                    'ElseIf j = 4 Then
                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                    '        Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                    '        cell.SetCellValue(d)
                    '    Else
                    '        Dim d As Int32 = Convert.ToInt32("0")
                    '        cell.SetCellValue(d)

                    '    End If
                    '    cell.CellStyle = _intCellStyle
                Else
                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                    cell.CellStyle = AllCellStyle
                End If
                'If i = dt.Rows.Count - 1 Then
                '    sheet1.AutoSizeColumn(j)
                'End If
            Next

        Next

        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "SchServiceVsCompletedService.xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "SchServiceVsCompletedService.xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub

End Class
