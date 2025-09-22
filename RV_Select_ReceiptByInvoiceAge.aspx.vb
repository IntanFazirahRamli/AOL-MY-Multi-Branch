Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Partial Class RV_Select_ReceiptByInvoiceAge
    Inherits System.Web.UI.Page


    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If GetData() = True Then
            Response.Redirect("RV_SalesDetailLedger.aspx")
           
        Else
            Return

        End If

    End Sub

 
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'chkStatusSearch.Attributes.Add("onchange", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")

        End If
    End Sub

 
    Private Function GetData() As Boolean
        lblAlert.Text = ""

        If String.IsNullOrEmpty(txtDueDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtDueDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID DUE FROM DATE"
                Return False
            End If
            'qry = qry + " and C.DueDate>= '" + Convert.ToDateTime(txtDueDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            'If selection = "" Then
            '    selection = "Due Date >= " + d.ToString("dd-MM-yyyy")
            'Else
            '    selection = selection + ", Due Date >= " + d.ToString("dd-MM-yyyy")
            'End If
        Else
            lblAlert.Text = "ENTER DUE DATE CUT OFF"
            Return False

        End If
            Dim selection As String
            selection = ""

        Dim qry As String = "(select "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "A.Location as Branch, "
        End If
        qry = qry + "A.ReceiptDate as DocumentDate,A.ReceiptNumber as DocumentNumber,A.AppliedBase as AmountReceived,A.ContactType,A.AccountID,A.ReceiptFrom,"
        qry = qry + " B.RefType as InvoiceNumber,C.AppliedBase as InvoiceAmount,B.AppliedBase as AppliedAmount,C.BalanceBase,C.SalesDate,C.Terms,DueDate,(datediff('" + Convert.ToDateTime(txtDueDateFrom.Text).ToString("yyyy-MM-dd") + "',DueDate)) as Age"
        qry = qry + " from tblrecv A inner join tblrecvdet B on A.ReceiptNumber=B.ReceiptNumber inner join tblsales C on  B.RefType=C.InvoiceNumber"
        qry = qry + " where A.poststatus='P'"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            '  selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and A.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If


            If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
                Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID RECEIPT FROM DATE"
                Return False
            End If
                qry = qry + " and A.ReceiptDate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

                If selection = "" Then
                    selection = "Receipt Date >= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Receipt Date >= " + d.ToString("dd-MM-yyyy")
                End If

            End If

            If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
                Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID RECEIPT TO DATE"
                Return False
            End If
                qry = qry + " and A.ReceiptDate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

                If selection = "" Then
                    selection = "Receipt Date <= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Receipt Date <= " + d.ToString("dd-MM-yyyy")
                End If
            End If

            '

            'If String.IsNullOrEmpty(txtDueDateTo.Text) = False Then
            '    Dim d As DateTime
        '    If Date.TryParseExact(txtDueDateTo.Text, "dd/MM/yyyy", sYSTEM.Globalization.CultureInfo.InvariantCulture, sYSTEM.Globalization.DateTimeStyles.None, d) Then

            '    Else
            '        '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
            '        lblAlert.Text = "INVALID DUE TO DATE"
            '        Return False
            '    End If
            '    qry = qry + " and C.DueDate <= '" + Convert.ToDateTime(txtDueDateTo.Text).ToString("yyyy-MM-dd") + "'"

            '    If selection = "" Then
            '        selection = "Receipt Date <= " + d.ToString("dd-MM-yyyy")
            '    Else
            '        selection = selection + ", Receipt Date <= " + d.ToString("dd-MM-yyyy")
            '    End If
            'End If

            If String.IsNullOrEmpty(txtAgeFrom.Text) = False Then
            Dim d As Int16

            If Int16.TryParse(txtAgeFrom.Text, d) Then
                qry = qry + " and Age >= '" + d + "'"

                If selection = "" Then
                    selection = "Age >= " + d
                Else
                    selection = selection + ", Age >= " + d
                End If

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID AGE FROM DAYS"
                Return False

            End If
        End If

        If String.IsNullOrEmpty(txtAgeTo.Text) = False Then
            Dim d As Int16

            If Int16.TryParse(txtAgeTo.Text, d) Then
                qry = qry + " and Age <= '" + d + "'"

                If selection = "" Then
                    selection = "Age <= " + d
                Else
                    selection = selection + ", Age <= " + d
                End If

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID AGE TO DAYS"
                Return False

            End If
        End If

        Session.Add("selection", selection)

        If rbtnSelect.SelectedValue = "2" Then
            Dim qryCNDN As String = GetDataCNDN()
            Dim qryJrn As String = GetDataJrn()

            qry = qry + ") union all (" + qryCNDN + ") union all (" + qryJrn + ")"
        Else
            qry = qry + ")"
        End If
      

        qry = qry + " order by DocumentDate,DocumentNumber"


        txtQuery.Text = qry


        Return True

    End Function

    Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQuery.Text

        cmd.Connection = conn

        'txtQuery.Visible = True

        'lblAlert.Text = txtQuery.Text

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



    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()
            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=ReceiptListingByInvoiceAge.xls"
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
            Response.[End]()
            dt.Clear()

        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtDueDateFrom.Text = ""
         txtInvDateFrom.Text = ""
        txtInvDateTo.Text = ""
        txtAgeFrom.Text = ""
        txtAgeTo.Text = ""
    
        lblAlert.Text = ""
    End Sub


    Private Function GetDataCNDN() As String

        Dim selection As String
        selection = ""

        Dim qry As String = "SELECT "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "A.Location as Branch, "
        End If
        qry = qry + "A.SalesDate as DocumentDate,A.InvoiceNumber as DocumentNumber,A.AppliedBase as AmountReceived,A.ContactType,A.AccountId,A.CustName,"
        qry = qry + "B.SourceInvoice,C.AppliedBase as InvoiceAmount,C.CreditBase as AppliedAmount,C.ReceiptBase,C.SalesDate,C.Terms,"
        qry = qry + "C.DueDate,(datediff('" + Convert.ToDateTime(txtDueDateFrom.Text).ToString("yyyy-MM-dd") + "',C.DueDate)) as Age "
        qry = qry + "FROM tblsales A inner join tblsalesdetail B on A.invoicenumber=B.invoicenumber"
        qry = qry + " inner join tblsales C on  B.sourceinvoice=C.InvoiceNumber"
        qry = qry + " where A.poststatus='P' AND A.doctype IN ('ARCN','ARDN')"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            '  selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and A.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If


        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then

            qry = qry + " and A.SalesDate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then

            qry = qry + " and A.SalesDate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

        End If

        If String.IsNullOrEmpty(txtAgeFrom.Text) = False Then
            Dim d As Int16

            If Int16.TryParse(txtAgeFrom.Text, d) Then
                qry = qry + " and Age >= '" + d + "'"


            End If
        End If

        If String.IsNullOrEmpty(txtAgeTo.Text) = False Then
            Dim d As Int16

            If Int16.TryParse(txtAgeTo.Text, d) Then
                qry = qry + " and Age <= '" + d + "'"


            End If
        End If
        qry = qry + " group by B.SourceInvoice"
    
        Return qry
    End Function


    Private Function GetDataJrn() As String

        Dim selection As String
        selection = ""

        Dim qry As String = "SELECT "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "A.Location as Branch, "
        End If
        qry = qry + "A.JournalDate as DocumentDate,A.VoucherNumber as DocumentNumber,B.CreditBase-B.DebitBase as AmountReceived,B.ContactType,B.AccountId,B.CustName,"
        qry = qry + "B.RefType,C.AppliedBase as InvoiceAmount,B.CreditBase-B.DebitBase as AppliedAmount,C.BalanceBase,C.SalesDate,C.Terms,"
        qry = qry + "C.DueDate,(datediff('" + Convert.ToDateTime(txtDueDateFrom.Text).ToString("yyyy-MM-dd") + "',C.DueDate)) as Age FROM tbljrnv A INNER JOIN tbljrnvdet b ON A.VoucherNumber=B.VoucherNumber"
        qry = qry + " inner join tblsales C on  B.reftype=C.InvoiceNumber"
        qry = qry + " where A.poststatus='P'"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            '  selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and A.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If


        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then

            qry = qry + " and A.JournalDate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then

            qry = qry + " and A.JournalDate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

        End If

        If String.IsNullOrEmpty(txtAgeFrom.Text) = False Then
            Dim d As Int16

            If Int16.TryParse(txtAgeFrom.Text, d) Then
                qry = qry + " and Age >= '" + d + "'"


            End If
        End If

        If String.IsNullOrEmpty(txtAgeTo.Text) = False Then
            Dim d As Int16

            If Int16.TryParse(txtAgeTo.Text, d) Then
                qry = qry + " and Age <= '" + d + "'"


            End If
        End If

    
        Return qry
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
        ' cell1.SetCellValue(Session("Selection").ToString)
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

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 3 Or j = 8 Or j = 9 Or j = 10 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 14 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

                    ElseIf j = 1 Or j = 11 Or j = 13 Then
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


        Else

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 2 Or j = 7 Or j = 8 Or j = 9 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 13 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

                    ElseIf j = 0 Or j = 10 Or j = 12 Then
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

        End If


        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = "attachment; filename=ReceiptListingByInvoiceAge"


            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub
End Class
