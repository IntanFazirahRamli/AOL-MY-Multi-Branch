Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
'Imports OfficeOpenXml
'Imports OfficeOpenXml.Style
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel
Partial Class RV_Select_ServicesMovedToNextMonth

    Inherits System.Web.UI.Page

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If GetData() = True Then
            lblAlert.Text = txtQuery.Text

            Session.Add("ReportType", "PostponedServices")

            Response.Redirect("RV_ServiceRecordToNextMonth.aspx")

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

  
    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click


        txtSvcDateFrom.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        ddlIncharge.SelectedIndex = 0
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

                If j = 3 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Double = Convert.ToDouble("0.00")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _doubleCellStyle
                ElseIf j = 1 Or j = 8 Or j = 10 Then
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
            Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "PostponedServices" + criteria + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "PostponedServices" + criteria + ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        If GetData() = True Then

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


        selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} in ['O','H'] "

        Dim qry As String = ""

        If chkDistribution.Checked Then
            qry = "select A.RecordNo, A.Servicedate, A.Scheduler, A.BillAmount*B.Percentage/100 as BillAmount, A.LocationID,"
            qry = qry + "A.CustName,replace(replace(A.Remarks, char(10), ' '), char(13), ' ') as Remarks,"
            qry = qry + " A.CreatedBy, A.CreatedON, A.LastmodifiedBy, A.LastModifiedOn"
            qry = qry + " From tblServiceRecord A left join tblContractValueDistribution B on A.contractno=B.contractno "
            qry = qry + "where A.recordno<>'' AND A.status in ('O','H') "


        Else
            qry = "select RecordNo, Servicedate, Scheduler, Billamount, LocationID, CustName,replace(replace(Remarks, char(10), ' '), char(13), ' ') as Remarks,"
            qry = qry + " CreatedBy, CreatedON, LastmodifiedBy, LastModifiedOn"
            qry = qry + " From tblServiceRecord where recordno<>'' AND status in ('O','H')"


        End If

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
            If Date.TryParseExact(txtSvcDateFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                lblAlert.Text = "Service Date From is invalid"
                Return False
            End If
            If selection = "" Then
                selection = "Service Date  Period = " + d.ToString("yyyyMM")
            Else
                selection = selection + ", Service Date Period = " + d.ToString("yyyyMM")
            End If
            d = d.AddMonths(1)

            selFormula = selFormula + "and {tblservicerecord1.RecordNo} like '*" + txtSvcDateFrom.Text + "*' and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            ' Dim d1 As New DateTime(d.Year, d.Month, 1)

            If chkDistribution.Checked Then
                qry = qry + " AND A.RecordNo like '%" + txtSvcDateFrom.Text + "%' and A.ServiceDate >= '" + d.ToString("yyyy-MM-dd") + "'"
                qry = qry + " and B.distributiondate in (select max(distributiondate) from tblcontractvaluedistribution C"
                qry = qry + " where C.contractno=A.ContractNo and C.distributiondate<=  '" + d.ToString("yyyy-MM-dd") + "')"

            Else
                qry = qry + " AND RecordNo like '%" + txtSvcDateFrom.Text + "%' and ServiceDate >= '" + d.ToString("yyyy-MM-dd") + "'"

            End If

        Else
            lblAlert.Text = "Enter Service Date Period"
        End If

        'If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
        '    Dim d As DateTime
        '    If Date.TryParseExact(txtSvcDateTo.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
        '    Else
        '        MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
        '        Return False
        '    End If
        '    selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '    If selection = "" Then
        '        selection = "Service Date <= " + d.ToString("yyyyMM")
        '    Else
        '        selection = selection + ", Service Date <= " + d.ToString("yyyyMM")
        '    End If
        '    qry = qry + " AND tblservicerecord.ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyyMM") + "'"

        'End If

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
            qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
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
