
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
'Imports OfficeOpenXml
'Imports OfficeOpenXml.Style
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Partial Class RV_Select_Distribution
    Inherits System.Web.UI.Page

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If GetData() = True Then
            lblAlert.Text = txtQuery.Text

            Session.Add("ReportType", "Distribution")

            Response.Redirect("RV_Distribution.aspx")

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
            insCmds.Parameters.AddWithValue("@LoginId", "DistributionReport - " + Session("UserID"))
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


    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click

        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""

    End Sub

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

        'Add details
        Dim _percentCellStyle As ICellStyle = Nothing

        If _percentCellStyle Is Nothing Then
            _percentCellStyle = workbook.CreateCellStyle()
            _percentCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _percentCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00%")
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

                    If j = 8 Or j = 14 Then
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
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00%")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _percentCellStyle
                    ElseIf j = 7 Or j = 11 Or j = 18 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                            cell.SetCellValue(d)
                            'Else
                            '    Dim d As Double = Convert.ToDouble("0.00")
                            '    cell.SetCellValue(d)

                        End If
                        cell.CellStyle = dateCellStyle
                    ElseIf j = 17 Then
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
                    If i = dt.Rows.Count - 1 Then
                        sheet1.AutoSizeColumn(j)
                    End If
                    'If j = 2 And j = 3 Then
                    '    sheet1.SetColumnWidth(2, 50 * 256)
                    '    sheet1.SetColumnWidth(3, 50 * 256)
                    'Else
                    '    sheet1.AutoSizeColumn(j)
                    'End If
                    sheet1.SetColumnWidth(4, 50 * 256)
                    sheet1.SetColumnWidth(5, 50 * 256)
                    sheet1.SetColumnWidth(15, 50 * 256)

                Next

            Next

        Else

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 7 Or j = 13 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = 12 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00%")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _percentCellStyle
                    ElseIf j = 6 Or j = 10 Or j = 17 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                            cell.SetCellValue(d)
                            'Else
                            '    Dim d As Double = Convert.ToDouble("0.00")
                            '    cell.SetCellValue(d)

                        End If
                        cell.CellStyle = dateCellStyle
                    ElseIf j = 16 Then
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
                    If i = dt.Rows.Count - 1 Then
                        sheet1.AutoSizeColumn(j)
                    End If
                    'If j = 2 And j = 3 Then
                    '    sheet1.SetColumnWidth(2, 50 * 256)
                    '    sheet1.SetColumnWidth(3, 50 * 256)
                    'Else
                    '    sheet1.AutoSizeColumn(j)
                    'End If
                    sheet1.SetColumnWidth(4, 50 * 256)
                    sheet1.SetColumnWidth(5, 50 * 256)
                    sheet1.SetColumnWidth(15, 50 * 256)

                Next

            Next


        End If
     
        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + txtSvcDateTo.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "ContractValueDistribution" + criteria + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "ContractValueDistribution" + criteria + ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        If GetData() = True Then
            'lblAlert.Text = txtQuery.Text
            'Return

            Dim dt As DataTable = GetDataSet()
            WriteExcelWithNPOI(dt, "xlsx")
            Return

            '  InsertIntoTblWebEventLog("PostponedServices", txtQuery.Text, dt.Rows.Count.ToString())
            Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")

            'Dim excel As ExcelPackage = New ExcelPackage()
            'Dim workSheet = excel.Workbook.Worksheets.Add("Sheet1")
            'workSheet.Cells("A1").LoadFromDataTable(dt, True)
            'workSheet.Cells("B2").Style.Numberformat.Format = "dd-MM-yyyy"

            'Dim memoryStream = New MemoryStream()
            'Dim attachment As String = "attachment; filename=PostponedServices" + criteria + ".xls"

            'Response.AddHeader("content-disposition", attachment)
            'Response.ContentType = "application/vnd.ms-excel"
            'excel.SaveAs(memoryStream)
            'memoryStream.WriteTo(Response.OutputStream)
            'Response.Flush()
            'Response.[End]()
            'dt.Clear()


            Dim attachment As String = "attachment; filename=PostponedServices" + criteria + ".xls"
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
    Private Function GetData() As Boolean
        Dim selFormula As String
        Dim selection As String
        selection = ""


        '  selFormula = "{tblcontractvaluedistribution1.Percentage} <> 100 "
        selFormula = "{tblcontractvaluedistribution1.rcno} <> 0 "

        Dim qry As String = ""
        Dim qry2 As String = ""
        qry = "SELECT "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "B.Location as Branch,"
        End If

        qry = qry + "A.ContractNo,B.Status,B.ContactType, B.AccountID, B.CustName, B.ServiceAddress, B.StartDate, B.AgreeValue, B.Industry, "
        qry = qry + "B.Salesman,A.DistributionDate, A.ContractGroup,A.Percentage/100 as Percentage,B.AgreeValue*A.Percentage/100 as DistributedValue,"
        qry = qry + "(select group_concat(TargetDesc) from tblcontractservingtarget C where C.ContractNo=B.ContractNo) as TargetPest,B.Comments as ServiceInstruction,"
        qry = qry + "(select count(TargetDesc) from tblcontractservingtarget C where C.ContractNo=B.ContractNo) as NoOfPestCovered,"
        qry = qry + "B.RenewalDate,B.OContractNo as OldContractNo,B.TerminationCode,B.TerminationDescription"
        qry = qry + " FROM tblcontractvaluedistribution A inner join tblcontract B on A.ContractNo = B.ContractNo "
        '  qry = qry + "inner join tblcontractvaluedistributionsummary C on A.BatchNo=C.BatchNo "
        'qry = qry + "where percentage<>100"
        qry = qry + "where A.rcno<>0 and B.Status<>'V'"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and B.location in (" + Convert.ToString(Session("Branch")).Replace("""", "'") + ")"
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
                lblAlert.Text = "Distribution Date From is invalid"
                Return False
            End If
            If selection = "" Then
                selection = "Distribution Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Distribution Date >= " + d.ToString("dd-MM-yyyy")
            End If

            selFormula = selFormula + "and {tblcontractvaluedistribution1.DistributionDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            qry = qry + " and A.DistributionDate >='" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
            qry2 = qry2 + " and B.DistributionDate >='" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        Else
            lblAlert.Text = "Enter Distribution Date From"
        End If

        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                lblAlert.Text = "Distribution Date To is invalid"
                Return False
            End If
            If selection = "" Then
                selection = "Distribution Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Distribution Date >= " + d.ToString("dd-MM-yyyy")
            End If

            selFormula = selFormula + "and {tblcontractvaluedistribution1.DistributionDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            qry = qry + " and A.DistributionDate <='" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
            qry2 = qry2 + " and B.DistributionDate <='" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

        Else
            lblAlert.Text = "Enter Distribution Date To"
        End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlContractGroup.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)

            '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
            selFormula = selFormula + " and {tblcontractvaluedistribution1.contractgroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If
            qry = qry + " and A.ContractGroup in (" + YrStr + ")"
        End If

        If chkLatestDistribution.Checked Then
            qry = qry + " and A.DistributionDate in (select max(B.distributiondate) from tblcontractvaluedistribution B where B.contractno=A.ContractNo " + qry2 + ")"

        End If
         qry = qry + " order by A.ContractNo"
        txtQuery.Text = qry
        lblAlert.Text = txtQuery.Text


        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)


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

            '   InsertIntoTblWebEventLog("GetDataSet", txtQuery.Text, dt.Rows.Count.ToString)
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

End Class
