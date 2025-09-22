
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel
Imports System.IO
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class RV_Select_PortfolioSummary
    Inherits System.Web.UI.Page

    'Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click

    '    Try


    '        ' '''''''''''''''''''''''''''''''''''''''''''''''''''
    '        'Dim strSql As String = "INSERT INTO tblEventLog (StaffID,Module,DocRef,Action,ComputerName," & _
    '        '      "Serial, LogDate, Comments,SOURCESQLID) " & _
    '        '      "VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate,  @Comments, @SOURCESQLID)"
    '        ''"VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate, @Amount, @BaseValue, @BaseGSTValue, @CustCode, @Comments, @SOURCESQLID)"


    '        'Dim command As MySqlCommand = New MySqlCommand
    '        'command.CommandType = CommandType.Text
    '        'command.CommandText = strSql
    '        'command.Parameters.Clear()
    '        ''Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd"))
    '        'command.Parameters.AddWithValue("@StaffID", Session("UserID"))
    '        'command.Parameters.AddWithValue("@Module", "REPORTS")
    '        'command.Parameters.AddWithValue("@DocRef", "PORTFOLIOSUMMARY")
    '        'command.Parameters.AddWithValue("@Action", "")
    '        'command.Parameters.AddWithValue("@ComputerName", Strings.Left(My.Computer.Name.ToString, 20))
    '        'command.Parameters.AddWithValue("@Serial", "")
    '        ''command.Parameters.AddWithValue("@LogDate", Convert.ToString(Session("SysDate")))
    '        'command.Parameters.AddWithValue("@LogDate", Convert.ToDateTime(Session("SysTime")))
    '        ''command.Parameters.AddWithValue("@Amount", 0)
    '        ''command.Parameters.AddWithValue("@BaseValue", 0)
    '        ''command.Parameters.AddWithValue("@BaseGSTValue", 0)
    '        ''command.Parameters.AddWithValue("@CustCode", "")
    '        'command.Parameters.AddWithValue("@Comments", "")
    '        'command.Parameters.AddWithValue("@SOURCESQLID", 0)
    '        'Dim conn As MySqlConnection = New MySqlConnection()

    '        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        ''Dim conn As MySqlConnection = New MySqlConnection(constr)
    '        'conn.Open()
    '        'command.Connection = conn
    '        'command.ExecuteNonQuery()

    '        'conn.Close()
    '        'conn.Dispose()
    '        'command.Dispose()

    '        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '        Dim selFormula As String
    '        selFormula = "{tbwportfolio1.CreatedBy} = '" + txtCreatedBy.Text + "'"
    '        Dim selection As String
    '        selection = ""

    '        'If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '        '    selFormula = selFormula + " {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"

    '        '    '  qrySvcRec = qrySvcRec + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"

    '        '    If selection = "" Then
    '        '        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
    '        '    Else
    '        '        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
    '        '    End If
    '        'End If
    '        Dim startdate As DateTime
    '        Dim enddate As DateTime

    '        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
    '            '   Dim d As DateTime
    '            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, startdate) Then
    '            Else
    '                MessageBox.Message.Alert(Page, "Date From is invalid", "str")
    '                Return
    '            End If

    '            '   selFormula = selFormula + " and {Command.EndDate} >" + "#" + d.ToString("MM-dd-yyyy") + "#"
    '            '   selFormula = selFormula + " and ({Command.ActualEnd} is null) or {Command.ActualEnd} >" + "#" + d.ToString("MM-dd-yyyy") + "#)"
    '            ' selFormula = selFormula + " and {Command.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
    '            '   Session.Add("StartDate", d.ToString("MM-dd-yyyy"))
    '            If selection = "" Then
    '                selection = "From Date >= " + startdate.ToString("dd-MM-yyyy")
    '            Else
    '                selection = selection + ", From Date >= " + startdate.ToString("dd-MM-yyyy")
    '            End If
    '        Else
    '            MessageBox.Message.Alert(Page, "Date From is invalid", "str")
    '            Return
    '        End If


    '        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
    '            '  Dim d As DateTime
    '            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, enddate) Then
    '            Else

    '            End If
    '            ' selFormula = selFormula + " and {Command.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

    '            '  Session.Add("EndDate", d.ToString("MM-dd-yyyy"))
    '            If selection = "" Then
    '                selection = "To Date <= " + enddate.ToString("dd-MM-yyyy")
    '            Else
    '                selection = selection + ", To Date <= " + enddate.ToString("dd-MM-yyyy")
    '            End If
    '        Else
    '            MessageBox.Message.Alert(Page, "Date To is invalid", "str")
    '            Return
    '        End If

    '        Dim conn As MySqlConnection = New MySqlConnection()

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()


    '        Dim command As MySqlCommand = New MySqlCommand
    '        command.CommandType = CommandType.StoredProcedure

    '        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '            command.CommandText = "PortfolioValueSummaryNewLocation"
    '        Else
    '            command.CommandText = "PortfolioValueSummaryNew"
    '        End If

    '        command.Parameters.Clear()

    '        'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
    '        'command.Parameters.AddWithValue("@pr_Query", txtQuery.Text)
    '        command.Parameters.AddWithValue("@pr_startdate", startdate)
    '        command.Parameters.AddWithValue("@pr_enddate", enddate)
    '        command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
    '        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '            command.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
    '        End If

    '        command.Connection = conn
    '        command.ExecuteScalar()
    '        command.Dispose()

    '        conn.Close()
    '        conn.Dispose()

    '        'Dim command2 As MySqlCommand = New MySqlCommand

    '        'command2.CommandType = CommandType.Text
    '        'command2.CommandText = "delete from tbwportfolio where CreatedBy='" + Session("UserID") + "'"

    '        'command2.Connection = conn

    '        'command2.ExecuteNonQuery()
    '        'command2.Dispose()

    '        ' While startdate <= enddate


    '        '    Dim cmdvwPortfolio As MySqlCommand = New MySqlCommand

    '        '    cmdvwPortfolio.CommandType = CommandType.Text

    '        '    cmdvwPortfolio.CommandText = "Select sum(portfoliovalue) from vwcontractPortfolionew where startdate <= '" + Convert.ToDateTime(startdate).ToString("yyyy-MM-dd") + "' And portfolioterminationdate >'" + Convert.ToDateTime(startdate).ToString("yyyy-MM-dd") + "'"

    '        '    cmdvwPortfolio.Connection = conn

    '        '    Dim drvwPortfolio As MySqlDataReader = cmdvwPortfolio.ExecuteReader()
    '        '    Dim dtvwPortfolio As New DataTable
    '        '    dtvwPortfolio.Load(drvwPortfolio)

    '        '    Dim command As MySqlCommand = New MySqlCommand

    '        '    command.CommandType = CommandType.Text
    '        '    Dim qry As String = "INSERT INTO tbwportfolio(startdate,value,createdby,createdon)values(@startdate,@value,@createdby,@createdon)"

    '        '    command.CommandText = qry
    '        '    command.Parameters.Clear()
    '        '    command.Parameters.AddWithValue("@StartDate", startdate)

    '        '    command.Parameters.AddWithValue("@Value", dtvwPortfolio.Rows(0)("sum(portfoliovalue)"))
    '        '    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '        '    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '        '    command.Connection = conn

    '        '    command.ExecuteNonQuery()

    '        '    startdate = startdate.AddDays(1)
    '        '   End While


    '        Session.Add("selFormula", selFormula)
    '        Session.Add("selection", selection)



    '        If rbtnSelect.SelectedValue = "2" Then
    '            Session.Add("ReportType", "PortfolioDateSummary")
    '            Response.Redirect("RV_PortfolioDateSummary.aspx", False)
    '            HttpContext.Current.ApplicationInstance.CompleteRequest()


    '        ElseIf rbtnSelect.SelectedValue = "3" Then
    '            Session.Add("ReportType", "PortfolioDateSummaryGraph")
    '            Response.Redirect("RV_PortfolioDateSummaryGraph.aspx")

    '        End If

    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("Print", ex.Message.ToString, "")

    '    End Try


    'End Sub


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
            insCmds.Parameters.AddWithValue("@LoginId", "PortfolioSummary - " + Convert.ToString(Session("UserID")))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID


        txtCreatedOn.Attributes.Add("readonly", "readonly")
    End Sub



    'Protected Sub btnPrintExportToExcel_Click(sender As Object, e As EventArgs) Handles btnPrintExportToExcel.Click

    '    If GetData() = True Then
    '        'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
    '        'Return

    '        Dim dt As DataTable = GetDataSet()

    '        Dim attachment As String = "attachment; filename=PortfolioSummary.xls"
    '        Response.ClearContent()
    '        Response.AddHeader("content-disposition", attachment)
    '        Response.ContentType = "application/vnd.ms-excel"
    '        Dim tab As String = ""
    '        For Each dc As DataColumn In dt.Columns
    '            'InsertIntoTblWebEventLog("EXCEL", dc.ColumnName.ToString, "TEST")

    '            Response.Write(tab + dc.ColumnName)
    '            tab = vbTab
    '        Next
    '        Response.Write(vbLf)
    '        Dim i As Integer
    '        For Each dr As DataRow In dt.Rows
    '            '  InsertIntoTblWebEventLog("EXCEL", dr.Item("startdate").ToString + " " + dt.Columns.Count.ToString, "TEST")
    '            tab = ""
    '            For i = 0 To dt.Columns.Count - 1

    '                Response.Write(tab & dr(i).ToString())
    '                tab = vbTab
    '            Next
    '            Response.Write(vbLf)
    '        Next
    '        Response.[End]()

    '        dt.Clear()

    '    End If

    'End Sub

    Private Function GetData() As Boolean

        Dim selection As String
        selection = ""
        Dim criteria As String = ""

        Dim qry As String = "select StartDate,EndDate,OpeningValue,ClosingValue from tbwportfolio where createdby = '" + txtCreatedBy.Text + "' and Report='" + rbtnSelect.SelectedValue.ToString + "'"
        lblAlert.Text = ""

        'If Convert.ToString(Session("LocationEnabled")) = "Y" Then
        '    selFormula = selFormula + " {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"

        '    '  qrySvcRec = qrySvcRec + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"

        '    If selection = "" Then
        '        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
        '    Else
        '        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
        '    End If
        'End If
        Dim startdate As DateTime
        Dim enddate As DateTime

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            '   Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, startdate) Then
            Else
                MessageBox.Message.Alert(Page, "Date From is invalid", "str")
                Return False
            End If
            '  qry = qry + " and startdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

            '   selFormula = selFormula + " and {Command.EndDate} >" + "#" + d.ToString("MM-dd-yyyy") + "#"
            '   selFormula = selFormula + " and ({Command.ActualEnd} is null) or {Command.ActualEnd} >" + "#" + d.ToString("MM-dd-yyyy") + "#)"
            ' selFormula = selFormula + " and {Command.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            '   Session.Add("StartDate", d.ToString("MM-dd-yyyy"))
            If selection = "" Then
                selection = "From Date >= " + startdate.ToString("dd-MM-yyyy")
                criteria = "_" + startdate.ToString("yyyyMMdd")
            Else
                selection = selection + ", From Date >= " + startdate.ToString("dd-MM-yyyy")
            End If
        Else
            MessageBox.Message.Alert(Page, "Date From is invalid", "str")
            Return False
        End If


        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            '  Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, enddate) Then
            Else

            End If
            '     qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

            ' selFormula = selFormula + " and {Command.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            '  Session.Add("EndDate", d.ToString("MM-dd-yyyy"))
            If selection = "" Then
                selection = "To Date <= " + enddate.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", To Date <= " + enddate.ToString("dd-MM-yyyy")
            End If
            criteria = criteria + "_" + enddate.ToString("yyyyMMdd")
        Else
            MessageBox.Message.Alert(Page, "Date To is invalid", "str")
            Return False
        End If

        txtCriteria.Text = criteria

        Session.Add("selection", selection)

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim command2 As MySqlCommand = New MySqlCommand

        'command2.CommandType = CommandType.Text
        'command2.CommandText = "delete from tbwportfolio where CreatedBy='" + Session("UserID") + "'"

        'command2.Connection = conn

        'command2.ExecuteNonQuery()

        'command2.Dispose()

        'While startdate <= enddate
        '  InsertIntoTblWebEventLog("GETDATA", startdate.ToShortDateString + " " + enddate.ToString, "TEST")

        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.StoredProcedure

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            command.CommandText = "PortfolioSummaryDatewiseLocation"
        Else
            command.CommandText = "PortfolioSummaryDatewise"
        End If



        command.Parameters.Clear()

        'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
        'command.Parameters.AddWithValue("@pr_Query", txtQuery.Text)
        command.Parameters.AddWithValue("@pr_startdate", startdate)
        command.Parameters.AddWithValue("@pr_enddate", enddate)
        command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
        command.Parameters.AddWithValue("@pr_Report", rbtnSelect.SelectedValue.ToString)
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            command.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
        End If

        command.Connection = conn
        command.ExecuteScalar()
        conn.Close()
        conn.Dispose()
        command.Dispose()




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
        '   InsertIntoTblWebEventLog("GetDataSet", txtQuery.Text, "TEST")

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



    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()
            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=PortfolioSummary.xls"
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
            ' InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

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

                If j = 2 Or j = 3 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Double = Convert.ToDouble("0.00")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _doubleCellStyle
                ElseIf j = 0 Or j = 1 Then
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
            Dim attachment As String = ""

            attachment = "attachment; filename=PortfolioSummary" + txtCriteria.Text + "_By" + rbtnSelect.SelectedItem.Text
         

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "PortfolioSummary.xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub
End Class

