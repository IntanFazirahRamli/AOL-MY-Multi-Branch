Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_RevenueReportByContractGrpIndustry
    Inherits System.Web.UI.Page
  
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        End Sub

   
        Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
            txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        ddlAccountType.SelectedIndex = 0
        txtServiceID.SelectedIndex = 0
        ddlIndustry.SelectedIndex = 0
    End Sub

        Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        
        If GetData() = True Then
            Session.Add("ReportType", "RevenueRptByContractGrpIndustry")
            Response.Redirect("RV_RevenueRptByContractGrpIndustry.aspx")
        End If

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

        If chkDistribution.Checked = True Then

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                If rbtnSelectDetSumm.SelectedValue = 1 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim row As IRow = sheet1.CreateRow(i + 2)

                        For j As Integer = 0 To dt.Columns.Count - 1
                            Dim cell As ICell = row.CreateCell(j)

                            If j = 6 Then
                                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)

                                End If
                                cell.CellStyle = _doubleCellStyle


                            Else
                                cell.SetCellValue(dt.Rows(i)(j).ToString)
                                cell.CellStyle = AllCellStyle

                            End If
                            If i = dt.Rows.Count - 1 Then
                                sheet1.AutoSizeColumn(j)
                            End If
                        Next
                    Next
                ElseIf rbtnSelectDetSumm.SelectedValue = 2 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim row As IRow = sheet1.CreateRow(i + 2)

                        For j As Integer = 0 To dt.Columns.Count - 1
                            Dim cell As ICell = row.CreateCell(j)

                            If j = 4 Then
                                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)

                                End If
                                cell.CellStyle = _doubleCellStyle


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

            Else

                If rbtnSelectDetSumm.SelectedValue = 1 Then

                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim row As IRow = sheet1.CreateRow(i + 2)

                        For j As Integer = 0 To dt.Columns.Count - 1
                            Dim cell As ICell = row.CreateCell(j)

                            If j = 5 Then
                                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)

                                End If
                                cell.CellStyle = _doubleCellStyle


                            Else
                                cell.SetCellValue(dt.Rows(i)(j).ToString)
                                cell.CellStyle = AllCellStyle

                            End If
                            If i = dt.Rows.Count - 1 Then
                                sheet1.AutoSizeColumn(j)
                            End If
                        Next
                    Next
                ElseIf rbtnSelectDetSumm.SelectedValue = 2 Then
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

            End If
        End If
        Dim criteria As String = ""

            Using exportData = New MemoryStream()
                Response.Clear()
            workbook.Write(exportData)

            If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
                criteria = "_" + txtSvcDateFrom.Text + "_" + txtSvcDateTo.Text
            End If

            If rbtnSelectDetSumm.SelectedValue = "1" Then
                criteria = criteria + "_Detail"
            Else
                criteria = criteria + "_Summary"
            End If

            criteria = criteria + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")

            Dim attachment As String = "attachment; filename=RevenueRptByContractGrp&Industry" + criteria

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
     
    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=RevenueRptByContractGrp&Industry.xls"
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


    Private Function GetData() As Boolean

        Dim selFormula As String
        selFormula = "{tblservicerecord1.status} = 'P'"
        Dim selection As String
        selection = ""
        Dim qry As String = ""
        
        If chkDistribution.Checked Then
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmd As MySqlCommand = New MySqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = conn

            '   InsertIntoTblWebEventLog("GetData", Convert.ToString(Session("LocationEnabled")), "1")
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                cmd.CommandText = "spRevenueByContractGrpIndustryDistributionLocation"
            Else
                cmd.CommandText = "spRevenueByContractGrpIndustryDistribution"

            End If



            cmd.Parameters.Clear()
            'InsertIntoTblWebEventLog("GetData", Session("UserID"), "3")
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                cmd.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
            End If
            cmd.Parameters.AddWithValue("pr_CreatedBy", Session("UserID"))
            If rbtnSelectDetSumm.SelectedValue = 1 Then
                cmd.Parameters.AddWithValue("pr_ReportName", "RevenueByCGIndustryDetail")

            ElseIf rbtnSelectDetSumm.SelectedValue = 2 Then
                cmd.Parameters.AddWithValue("pr_ReportName", "RevenueByCGIndustrySummary")

            End If

         
            If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                    lblAlert.Text = "INVALID INVOICE FROM DATE"
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
                    lblAlert.Text = "INVALID INVOICE TO DATE"
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

            If rbtnSelectDetSumm.SelectedValue = 1 Then
                qry = "SELECT "
                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    qry = qry + "Location as Branch, "
                End If
                qry = qry + "ContactType,ContractNo,ContractGroup,Industry,RefType as ServiceRecordNo,BilledAmount as ServiceValue"
                qry = qry + " FROM tbwrevenuereport where createdby='" & Session("UserID") & "'"
                qry = qry + "And ReportName = 'RevenueByCGIndustryDetail'"

            ElseIf rbtnSelectDetSumm.SelectedValue = 2 Then
                qry = "SELECT "
                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    qry = qry + "Location as Branch, "
                End If
                qry = qry + "ContactType,ContractGroup,Industry,sum(BilledAmount) as ServiceValue"
                qry = qry + " FROM tbwrevenuereport where createdby='" & Session("UserID") & "'"
                qry = qry + "And ReportName = 'RevenueByCGIndustrySummary'"

            End If


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
                    selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
                End If
                ' qry = qry + " AND serviceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            End If

            If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Else
                    MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                    Return False
                End If
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                If selection = "" Then
                    selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
                End If
                ' qry = qry + " AND tblservicerecord.serviceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

            End If


            'If ddlMainContractGroup.Text = "-1" Then
            'Else
            '    selFormula = selFormula + " and {tblContract1.ContractGroup} = '" + ddlMainContractGroup.Text + "'"
            '    If selection = "" Then
            '        selection = "ContractGroup : " + ddlMainContractGroup.Text
            '    Else
            '        selection = selection + ", ContractGroup : " + ddlMainContractGroup.Text
            '    End If
            '    qry = qry + " and tblcontract.ContractGroup = '" + ddlMainContractGroup.Text + "'"
            'End If

            Dim YrStrList2 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In txtServiceID.Items
                If item.Selected Then

                    YrStrList2.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList2.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)

                '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
                selFormula = selFormula + " and {tblcontract1.contractgroup} in [" + YrStr + "]"
                If selection = "" Then
                    selection = "ContractGroup : " + YrStr
                Else
                    selection = selection + ", ContractGroup : " + YrStr
                End If
                qry = qry + " and ContractGroup in (" + YrStr + ")"

            End If

            'If ddlIndustry.Text = "-1" Then
            'Else
            '    selFormula = selFormula + " and {tblContract1.Industry} = '" + ddlIndustry.Text + "'"
            '    If selection = "" Then
            '        selection = "Industry : " + ddlIndustry.Text
            '    Else
            '        selection = selection + ", Industry : " + ddlIndustry.Text
            '    End If
            '    qry = qry + " and tblcontract.Industry = '" + ddlIndustry.Text + "'"
            'End If

            Dim YrStrList3 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In ddlIndustry.Items
                If item.Selected Then

                    YrStrList3.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList3.Count > 0 Then

                Dim YrStr3 As [String] = [String].Join(",", YrStrList3.ToArray)

                '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
                selFormula = selFormula + " and {tblcontract1.Industry} in [" + YrStr3 + "]"
                If selection = "" Then
                    selection = "Industry : " + YrStr3
                Else
                    selection = selection + ", Industry : " + YrStr3
                End If
                qry = qry + " and Industry in (" + YrStr3 + ")"
            End If

            If ddlAccountType.Text = "-1" Then
            Else
                selFormula = selFormula + " and {tblservicerecord1.ContactType} = '" + ddlAccountType.Text + "'"
                If selection = "" Then
                    selection = "Account Type = " + ddlAccountType.Text
                Else
                    selection = selection + ", Account Type = " + ddlAccountType.Text
                End If
                qry = qry + " and ContactType = '" + ddlAccountType.Text + "'"
            End If


            If rbtnSelectDetSumm.SelectedValue = 2 Then
                qry = qry + " group by ContactType,ContractGroup,Industry"
            End If
        Else
            If rbtnSelectDetSumm.SelectedValue = 1 Then
                qry = "select tblcontract.ContactType,tblcontract.ContractGroup,tblcontract.ContractNo, replace(replace(tblcontract.Industry, char(10), ' '), char(13), ' ') as Industry,tblservicerecord.RecordNo,tblservicerecord.billamount as AmountToBill "
                qry = qry + " from tblcontract,tblservicerecord where tblcontract.contractno=tblservicerecord.contractno and tblservicerecord.status='P'"

            ElseIf rbtnSelectDetSumm.SelectedValue = 2 Then
                qry = "select tblcontract.ContactType,tblcontract.ContractGroup, replace(replace(tblcontract.Industry, char(10), ' '), char(13), ' ') as Industry,sum(tblservicerecord.billamount) as AmountToBill "
                qry = qry + " from tblcontract,tblservicerecord where tblcontract.contractno=tblservicerecord.contractno and tblservicerecord.status='P'"

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
                If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Else
                    MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                    Return False
                End If
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                If selection = "" Then
                    selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
                End If
                qry = qry + " AND tblservicerecord.serviceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            End If

            If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                Else
                    MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                    Return False
                End If
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                If selection = "" Then
                    selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
                End If
                qry = qry + " AND tblservicerecord.serviceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

            End If


            'If ddlMainContractGroup.Text = "-1" Then
            'Else
            '    selFormula = selFormula + " and {tblContract1.ContractGroup} = '" + ddlMainContractGroup.Text + "'"
            '    If selection = "" Then
            '        selection = "ContractGroup : " + ddlMainContractGroup.Text
            '    Else
            '        selection = selection + ", ContractGroup : " + ddlMainContractGroup.Text
            '    End If
            '    qry = qry + " and tblcontract.ContractGroup = '" + ddlMainContractGroup.Text + "'"
            'End If

            Dim YrStrList2 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In txtServiceID.Items
                If item.Selected Then

                    YrStrList2.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList2.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)

                '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
                selFormula = selFormula + " and {tblcontract1.contractgroup} in [" + YrStr + "]"
                If selection = "" Then
                    selection = "ContractGroup : " + YrStr
                Else
                    selection = selection + ", ContractGroup : " + YrStr
                End If
                qry = qry + " and tblcontract.ContractGroup in (" + YrStr + ")"

            End If

            'If ddlIndustry.Text = "-1" Then
            'Else
            '    selFormula = selFormula + " and {tblContract1.Industry} = '" + ddlIndustry.Text + "'"
            '    If selection = "" Then
            '        selection = "Industry : " + ddlIndustry.Text
            '    Else
            '        selection = selection + ", Industry : " + ddlIndustry.Text
            '    End If
            '    qry = qry + " and tblcontract.Industry = '" + ddlIndustry.Text + "'"
            'End If

            Dim YrStrList3 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In ddlIndustry.Items
                If item.Selected Then

                    YrStrList3.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList3.Count > 0 Then

                Dim YrStr3 As [String] = [String].Join(",", YrStrList3.ToArray)

                '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
                selFormula = selFormula + " and {tblcontract1.Industry} in [" + YrStr3 + "]"
                If selection = "" Then
                    selection = "Industry : " + YrStr3
                Else
                    selection = selection + ", Industry : " + YrStr3
                End If
                qry = qry + " and tblcontract.Industry in (" + YrStr3 + ")"
            End If

            If ddlAccountType.Text = "-1" Then
            Else
                selFormula = selFormula + " and {tblservicerecord1.ContactType} = '" + ddlAccountType.Text + "'"
                If selection = "" Then
                    selection = "Account Type = " + ddlAccountType.Text
                Else
                    selection = selection + ", Account Type = " + ddlAccountType.Text
                End If
                qry = qry + " and tblcontract.ContactType = '" + ddlAccountType.Text + "'"
            End If

            If rbtnSelectDetSumm.SelectedValue = 2 Then
                qry = qry + " group by tblcontract.ContactType,tblcontract.ContractGroup,tblcontract.Industry"
            End If

        End If

            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)

            txtQuery.Text = qry

            Return True
    End Function

End Class