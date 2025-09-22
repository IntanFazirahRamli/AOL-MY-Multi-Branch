
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_ServiceRecordBilling
    Inherits System.Web.UI.Page




    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        GetData()
        Response.Redirect("RV_ServiceRecordBilling.aspx")

    End Sub

  

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'chkStatusSearch.Attributes.Add("onchange", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")
            chkPartialBilled.Checked = True
        End If
    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
         ddlCompanyGrp.SelectedIndex = 0
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        chkPartialBilled.Checked = False


    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        'If GetData() = True Then
        '    lblAlert.Text = txtQuery.Text
        '    Return
        'End If

        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return
            'lblAlert.Text = txtQuery.Text
            'Return

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            GridView1.DataSource = dt
            GridView1.DataBind()

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.ClearHeaders()
            Response.Charset = ""
            Dim FileName As String = "ServicesforBilling" + txtCriteria.Text + ".xls"
            Dim strwritter As StringWriter = New StringWriter()
            Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            '  Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & FileName)
            GridView1.GridLines = GridLines.Both
            GridView1.HeaderStyle.Font.Bold = True
            GridView1.RenderControl(htmltextwrtter)
            Response.Write(strwritter.ToString())
            Response.Flush()

            Response.[End]()

            'Dim attachment As String = ""
            'attachment = "attachment; filename=ServicesforBilling" + txtCriteria.Text + ".xls"
            'Response.ClearContent()
            'Response.AddHeader("content-disposition", attachment)
            'Response.ContentType = "application/vnd.ms-excel"
            'Dim tab As String = ""
            'For Each dc As DataColumn In dt.Columns
            '    Response.Write(tab + dc.ColumnName)
            '    tab = vbTab
            'Next
            'Response.Write(vbLf)
            'Dim i As Integer
            'For Each dr As DataRow In dt.Rows
            '    tab = ""
            '    For i = 0 To dt.Columns.Count - 1
            '        Response.Write(tab & dr(i).ToString())
            '        tab = vbTab
            '    Next
            '    Response.Write(vbLf)
            'Next
            'Response.[End]()

            'dt.Clear()

        End If


    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Private Function GetData() As Boolean
        Dim criteria As String = ""

        Dim selFormula As String
        Dim selection As String
        'Dim qry As String = "SELECT tblservicerecord.CompanyGroup,tblCONTRACT.ContractGroup,tblservicerecord.ContractNo,tblservicerecord.Status,tblservicerecord.ServiceBy,tblservicerecord.RecordNo,tblservicerecord.ServiceDate,tblservicerecord.ContactType,tblservicerecord.AccountID,tblservicerecord.LocationID,tblservicerecord.CustName,replace(replace(replace(tblservicerecord.Address1, char(10), ' '), char(13), ' '),'\t',' ') as ServiceAddress,"
        'qry = qry + "tblcontract.BillingFrequency,tblservicerecord.BillAmount,tblservicerecord.BilledAmt,replace(replace(tblservicerecord.Notes, char(10), ' '), char(13), ' ') as ServiceDescription,tblcontract.ServiceStart,tblcontract.LastBillDate,tblservicerecord.Scheduler,replace(replace(tblcontract.Notes, char(10), ' '), char(13), ' ') AS ContractNotes FROM tblservicerecord LEFT OUTER JOIN tblcontract "
        'qry = qry + "on tblservicerecord.ContractNo = tblcontract.ContractNo where tblservicerecord.rcno<>0 and tblservicerecord.BillAmount>0 "

        Dim qry As String = "select C.CompanyGroup, C.ContractGroup, A.ContractNo, A.Status, A.ServiceBy, A.RecordNo, A.ServiceDate, A.ContactType, A.AccountID"
        qry = qry + ",A.LocationID,A.CustName,replace(replace(replace(A.CustAddress1, char(10), ' '), char(13), ' '),'\t',' ') as CustAddress,C.BillingFrequency,A.BillAmount,A.BilledAmt,A.BillNo,B.BilledAmount"
        qry = qry + ",REPLACE(REPLACE(A.Notes, CHAR(13), ''), CHAR(10), '') as ServiceDescription,C.ServiceStart,C.LastBillDate,C.Scheduler,REPLACE(REPLACE(C.Notes, CHAR(13), ''), CHAR(10), '') as ContractNotes,E.StaffDepartment,E.LedgerCode,E.SubLedgerCode "
        qry = qry + "from tblServiceRecord A inner join tblContract C on A.ContractNo=C.ContractNo left outer join vsservicerecordbilledamount B on A.RecordNo=B.RecordNo left join tblstaff D ON A.ServiceBy=D.StaffID "
        qry = qry + " left join tblstaffdepartment E on D.Department=E.StaffDepartment where A.rcno<>0"
        qry = qry + " and (A.BillNo is null or (trim(A.BillNo) not like '%BILLED%' and trim(A.BillNo) not like '%INV%' and trim(A.BillNo) not like 'R%'))"

        selection = ""
        selFormula = "{tblservicerecord1.rcno} <> 0 "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and A.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If rdbBillStatus.SelectedValue = "Completed" Then
            selFormula = selFormula + " and {tblservicerecord1.status}= 'P'"
            If selection = "" Then
                selection = "Status = Completed"
            Else
                selection = selection + ", Status = Completed"
            End If
            criteria = criteria + "_CompletedServices"
            qry = qry + " and A.status = 'P'"
        ElseIf rdbBillStatus.SelectedValue = "Open" Then
            selFormula = selFormula + " and {tblservicerecord1.Status}= 'O'"
            If selection = "" Then
                selection = "Status = Open"
            Else
                selection = selection + "Status = Open"
            End If
            qry = qry + " and A.Status = 'O'"
        ElseIf rdbBillStatus.SelectedValue = "All" Then
            selFormula = selFormula + " and {tblservicerecord1.Status} IN ['O','P']"

            If selection = "" Then
                selection = "Status = All"
            Else
                selection = selection + ", Status = All"
            End If
            criteria = criteria + "_AllServices"
            qry = qry + " and A.Status IN ('O','P')"
        End If



        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray())
            Dim YrStr1 As [String] = [String].Join("+", YrStrList1.ToArray())

            selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            criteria = criteria + "_" + YrStr1
            qry = qry + " and A.CompanyGroup in (" + YrStr + ")"
        End If


        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + " and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
            End If
            criteria = criteria + "_" + d.ToString("yyyyMMdd")
            qry = qry + " AND A.serviceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
            criteria = criteria + "_" + d.ToString("yyyyMMdd")
            qry = qry + " AND A.serviceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

        End If

        If chkZeroValueServices.Checked = True Then
            selFormula = selFormula + " and {tblservicerecord1.BillAmount} >= 0"
            If selection = "" Then
                selection = "BillStatus = Zero Value Services Included"
            Else
                selection = selection + ", BillStatus = Zero Value Services Included"
            End If
            qry = qry + " and A.BillAmount>=0"
        ElseIf chkZeroValueServices.Checked = False Then
            selFormula = selFormula + " and {tblservicerecord1.BillAmount} > 0"
            If selection = "" Then
                selection = "BillStatus = Zero Value Services not Included"
            Else
                selection = selection + ", BillStatus = Zero Value Services not Included"
            End If
            qry = qry + " and A.BillAmount>0"
        End If

        If chkPartialBilled.Checked = True Then
            '   selFormula = selFormula + " and {tblservicerecord1.BilledAmt} < {tblservicerecord1.BillAmount} "

            selFormula = selFormula + " and (isnull({tblservicerecord1.BilledAmt}) = false or {tblservicerecord1.BillAmount} > {tblservicerecord1.BilledAmt} "
            If selection = "" Then
                selection = "BillStatus = Partial Billed Included"
            Else
                selection = selection + ", BillStatus = Partial Billed Included"
            End If
            ' qry = qry + " and tblservicerecord.BilledAmt < tblservicerecord.BillAmount"
            qry = qry + " and (B.BilledAmount is null or A.BillAmount>B.BilledAmount)"
        Else
            selFormula = selFormula + " and isnull({tblservicerecord1.BillNo})"
            If selection = "" Then
                selection = "BillStatus = Partial Billed Not Included"
            Else
                selection = selection + ", BillStatus = Partial Billed Not Included"
            End If
            qry = qry + " and (B.BilledAmount is null or B.BilledAmount =0)"
        End If

        If chkCNServices.Checked = True Then
            ' selFormula = selFormula + " and {tblservicerecord1.BillAmount} >= 0"
            If selection = "" Then
                selection = "BillStatus = Services with Credit Note Included"
            Else
                selection = selection + ", BillStatus = Services with Credit Note Included"
            End If
            qry = qry + " and A.RecordNo not in (select RefType from vwservicerecordwithcn_priceincrease)"

        End If

        qry = qry + " order by C.CompanyGroup,C.ContractGroup,A.LocationID,A.ContractNo,A.ServiceDate,A.ServiceBy"

        txtCriteria.Text = criteria


        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        Session.Add("ReportType", "ServiceRecordBilling")


        txtQuery.Text = qry


        Return True

        'Dim criteria As String = ""

        'Dim selFormula As String
        'Dim selection As String
        'Dim qry As String = "SELECT tblservicerecord.CompanyGroup,tblCONTRACT.ContractGroup,tblservicerecord.ContractNo,tblservicerecord.Status,tblservicerecord.ServiceBy,tblservicerecord.RecordNo,tblservicerecord.ServiceDate,tblservicerecord.ContactType,tblservicerecord.AccountID,tblservicerecord.LocationID,tblservicerecord.CustName,replace(replace(replace(tblservicerecord.Address1, char(10), ' '), char(13), ' '),'\t',' ') as ServiceAddress,"
        'qry = qry + "tblcontract.BillingFrequency,tblservicerecord.BillAmount,tblservicerecord.BilledAmt,replace(replace(tblservicerecord.Notes, char(10), ' '), char(13), ' ') as ServiceDescription,tblcontract.ServiceStart,tblcontract.LastBillDate,tblservicerecord.Scheduler,replace(replace(tblcontract.Notes, char(10), ' '), char(13), ' ') AS ContractNotes FROM tblservicerecord LEFT OUTER JOIN tblcontract "
        'qry = qry + "on tblservicerecord.ContractNo = tblcontract.ContractNo where tblservicerecord.rcno<>0 and tblservicerecord.BillAmount>0 "

        'selection = ""
        'selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.BillAmount} > 0"
        'If Convert.ToString(Session("LocationEnabled")) = "Y" Then
        '    selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
        '    qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"

        '    If selection = "" Then
        '        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
        '    Else
        '        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
        '    End If
        'End If

        'If rdbBillStatus.SelectedValue = "Completed" Then
        '    selFormula = selFormula + " and {tblservicerecord1.status}= 'P'"
        '    If selection = "" Then
        '        selection = "Status = Completed"
        '    Else
        '        selection = selection + ", Status = Completed"
        '    End If
        '    criteria = criteria + "_CompletedServices"
        '    qry = qry + " and tblservicerecord.status = 'P'"
        'ElseIf rdbBillStatus.SelectedValue = "Open" Then
        '    selFormula = selFormula + " and {tblservicerecord1.Status}= 'O'"
        '    If selection = "" Then
        '        selection = "Status = Open"
        '    Else
        '        selection = selection + "Status = Open"
        '    End If
        '    qry = qry + " and tblservicerecord.Status = 'O'"
        'ElseIf rdbBillStatus.SelectedValue = "All" Then
        '    selFormula = selFormula + " and {tblservicerecord1.Status} IN ['O','P']"

        '    If selection = "" Then
        '        selection = "Status = All"
        '    Else
        '        selection = selection + ", Status = All"
        '    End If
        '    criteria = criteria + "_AllServices"
        '    qry = qry + " and tblservicerecord.Status IN ('O','P')"
        'End If



        'Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        'For Each item As ListItem In ddlCompanyGrp.Items
        '    If item.Selected Then

        '        YrStrList1.Add("""" + item.Value + """")

        '    End If
        'Next

        'If YrStrList1.Count > 0 Then

        '    Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray())
        '    Dim YrStr1 As [String] = [String].Join("+", YrStrList1.ToArray())

        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
        '    If selection = "" Then
        '        selection = "CompanyGroup : " + YrStr
        '    Else
        '        selection = selection + ", CompanyGroup : " + YrStr
        '    End If
        '    criteria = criteria + "_" + YrStr1
        '    qry = qry + " and tblservicerecord.CompanyGroup in (" + YrStr + ")"
        'End If


        'If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
        '    Dim d As DateTime
        '    If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

        '    Else
        '        MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
        '        '  lblAlert.Text = "INVALID START DATE"
        '        Return False
        '    End If
        '    selFormula = selFormula + " and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '    If selection = "" Then
        '        selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
        '    Else
        '        selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
        '    End If
        '    criteria = criteria + "_" + d.ToString("yyyyMMdd")
        '    qry = qry + " AND tblservicerecord.serviceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
        'End If
        'If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
        '    Dim d As DateTime
        '    If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

        '    Else
        '        MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
        '        '  lblAlert.Text = "INVALID START DATE"
        '        Return False
        '    End If
        '    selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '    If selection = "" Then
        '        selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
        '    Else
        '        selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
        '    End If
        '    criteria = criteria + "_" + d.ToString("yyyyMMdd")
        '    qry = qry + " AND tblservicerecord.serviceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

        'End If


        'If chkPartialBilled.Checked = True Then

        '    selFormula = selFormula + " and {tblservicerecord1.BilledAmt} < {tblservicerecord1.BillAmount}"
        '    If selection = "" Then
        '        selection = "BillStatus = Partial Billed Included"
        '    Else
        '        selection = selection + ", BillStatus = Partial Billed Included"
        '    End If
        '    qry = qry + " and tblservicerecord.BilledAmt < tblservicerecord.BillAmount"
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.BilledAmt} =0"
        '    If selection = "" Then
        '        selection = "BillStatus = Partial Billed Not Included"
        '    Else
        '        selection = selection + ", BillStatus = Partial Billed Not Included"
        '    End If
        '    qry = qry + " and tblservicerecord.BilledAmt =0"
        'End If


        'qry = qry + " ORDER BY tblservicerecord.recordno"

        'txtCriteria.Text = criteria


        'Session.Add("selFormula", selFormula)
        'Session.Add("selection", selection)
        'Session.Add("ReportType", "ServiceRecordBilling")


        'txtQuery.Text = qry


        'Return True
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
        '    cell1.SetCellValue(Session("Selection").ToString)
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + Session("Selection").ToString)

        cell1.CellStyle.WrapText = True
       
        Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 10)
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
            'InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

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

                If j = 13 Or j = 14 Or j = 16 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Double = Convert.ToDouble("0.00")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _doubleCellStyle

                ElseIf j = 6 Or j = 18 Or j = 19 Then
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
       


        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "ServicesforBilling" + txtCriteria.Text + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "ServicesforBilling" + txtCriteria.Text + ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub
End Class
