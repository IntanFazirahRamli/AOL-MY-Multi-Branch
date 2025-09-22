Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Diagnostics
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel
Imports System.IO

Partial Class RV_Select_ManpowerProductivityByTeamMemberAppointment
    Inherits System.Web.UI.Page

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.SelectedIndex = 0
      

    End Sub

    'Private Sub GetData()

    '    Dim conn As MySqlConnection = New MySqlConnection()

    '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    conn.Open()

    '    Dim command2 As MySqlCommand = New MySqlCommand

    '    command2.CommandType = CommandType.Text

    '    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberAppt';"

    '    command2.Connection = conn

    '    command2.ExecuteNonQuery()
    '    command2.Dispose()

    '    Dim ContractGroup As String = ""

    '    If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
    '        Dim d As DateTime
    '        If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

    '        Else
    '            ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
    '            lblAlert.Text = "INVALID SERVICE DATE FROM"
    '            Return
    '        End If
    '    End If

    '    If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
    '        Dim d As DateTime
    '        If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

    '        Else
    '            '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
    '            lblAlert.Text = "INVALID SERVICE DATE TO"
    '            Return
    '        End If

    '    End If

    '    Dim YrStrList2 As List(Of [String]) = New List(Of String)()

    '    For Each item As ListItem In txtServiceID.Items
    '        If item.Selected Then

    '            YrStrList2.Add("""" + item.Value + """")

    '        End If
    '    Next

    '    If YrStrList2.Count > 0 Then

    '        Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
    '        ContractGroup = YrStr

    '    End If



    '    Dim command As MySqlCommand = New MySqlCommand
    '    command.CommandType = CommandType.StoredProcedure
    '    If chkTimeSheet.Checked Then
    '        command.CommandText = "SaveProductivityByTeamMemberAppointmentTimeSheet"
    '    Else
    '        command.CommandText = "SaveProductivityByTeamMemberAppointment"
    '    End If

    '    command.Parameters.Clear()

    '    command.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
    '    command.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))
    '    command.Parameters.AddWithValue("@pr_ContractGroup", ContractGroup)

    '    command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
    '    command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberAppt")

    '    command.Connection = conn
    '    command.ExecuteScalar()

    '    command.Dispose()
    '    conn.Close()
    '    conn.Dispose()
    'End Sub


    Public Sub InsertIntoTblWebEventLog(LoginID As String, events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", LoginID)
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

        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID

        txtCreatedOn.Attributes.Add("readonly", "readonly")

    End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click


        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim command2 As MySqlCommand = New MySqlCommand

        'command2.CommandType = CommandType.Text

        'command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberAppt';"

        'command2.Connection = conn

        'command2.ExecuteNonQuery()
        'command2.Dispose()

        Dim ContractGroup As String = ""
        Dim selection As String
        selection = ""

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                lblAlert.Text = "INVALID SERVICE DATE FROM"
                Return
            End If

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
                '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                lblAlert.Text = "INVALID SERVICE DATE TO"
                Return
            End If
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("'" + item.Value + "'")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
            '  ContractGroup = String.Concat("(", YrStr, ")")
            ContractGroup = YrStr
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If
        End If

        'lblAlert.Text = ContractGroup
        'Return

        Dim sda As New MySqlDataAdapter()
        Dim dt As New DataTable

        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.StoredProcedure

        If rdbSelect.SelectedValue = "Summary" Then
            If chkDistribution.Checked Then
                If chkTimeSheet.Checked Then
                    command.CommandText = "SaveProductivityByTeamMemberAppointmentTimeSheetDist"
                Else
                    command.CommandText = "SaveProductivityByTeamMemberAppointmentDist"
                End If
            Else
                If chkTimeSheet.Checked Then
                    command.CommandText = "SaveProductivityByTeamMemberAppointmentTimeSheet"
                Else
                    command.CommandText = "SaveProductivityByTeamMemberAppointment"
                End If
            End If
          

            command.Parameters.Clear()

            command.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_ContractGroup", ContractGroup)

            command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
            command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberAppt")


            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                command.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
            End If


            command.Connection = conn
        ElseIf rdbSelect.SelectedValue = "Detail" Then

            If chkDistribution.Checked Then
                If chkTimeSheet.Checked Then
                    command.CommandText = "SaveProductivityByTeamMemberAppointmentTimeSheetDetailDist"
                Else
                    command.CommandText = "SaveProductivityByTeamMemberAppointmentDetailDist"
                End If
            Else

                If chkTimeSheet.Checked Then
                    command.CommandText = "SaveProductivityByTeamMemberAppointmentTimeSheetDetail"
                Else
                    command.CommandText = "SaveProductivityByTeamMemberAppointmentDetail"
                End If
            End If

            command.Parameters.Clear()

            command.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_ContractGroup", ContractGroup)

            command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
            command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberApptDetail")

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                command.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
            End If


            command.Connection = conn
        End If

        '  command.ExecuteScalar()
        Session.Add("selection", selection)

        Try

            sda.SelectCommand = command
            sda.Fill(dt)
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            command.Dispose()
            conn.Dispose()
        End Try

        WriteExcelWithNPOI(dt, "xlsx")
        Return


        'Dim attachment As String = "attachment; filename=ManpowerProductivityTeamMemberAppointment.xls"
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



    End Sub



    'Private Function GetDataSet() As DataTable
    '    Dim dt As New DataTable()
    '    Dim conn As MySqlConnection = New MySqlConnection()
    '    Dim cmd As MySqlCommand = New MySqlCommand

    '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

    '    Dim sda As New MySqlDataAdapter()
    '    cmd.CommandType = CommandType.Text
    '    cmd.CommandText = txtQuery.Text

    '    cmd.Connection = conn
    '    Try
    '        conn.Open()
    '        sda.SelectCommand = cmd
    '        sda.Fill(dt)

    '        Return dt
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        conn.Close()
    '        sda.Dispose()
    '        conn.Dispose()
    '    End Try
    'End Function

    'Private Function GetData1() As Boolean
    '    GetData()
    '    Return True

    '    Dim qry As String = ""


    '    Dim selection As String
    '    selection = ""
    '    Dim selFormula As String = ""

    '    qry = "Select StaffID,ServiceDate,VehNo,RecordNo,TimeIn,TimeOut,Client,Service,ServiceValue,NoPerson,Duration,ServiceCost,Manpowercost,ServiceValue-ManpowerCost as Profit"
    '    qry = qry + " from tblrptserviceanalysis A where report = 'ProdTeamMemberAppt' and createdby='" & Session("UserID") & "'"


    '    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '        qry = qry + " and A.location in (" + Convert.ToString(Session("Branch")) + ")"

    '    End If


    '    If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
    '        Dim d As DateTime
    '        If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

    '        Else
    '            ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
    '            lblAlert.Text = "INVALID SERVICE DATE FROM"
    '            Return False
    '        End If

    '        qry = qry + " and A.ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
    '    End If

    '    If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
    '        Dim d As DateTime
    '        If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

    '        Else
    '            '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
    '            lblAlert.Text = "INVALID SERVICE DATE TO"
    '            Return False
    '        End If
    '        qry = qry + " and A.ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

    '    End If

    '    Dim YrStrList2 As List(Of [String]) = New List(Of String)()

    '    For Each item As ListItem In txtServiceID.Items
    '        If item.Selected Then

    '            YrStrList2.Add("""" + item.Value + """")

    '        End If
    '    Next

    '    If YrStrList2.Count > 0 Then

    '        Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
    '        qry = qry + " and A.ServDateType in (" + YrStr + ")"
    '        'If rdbSelect.Text = "Summary" Then
    '        '    qry = qry + " and C.ContractGroup in (" + YrStr + ")"
    '        'ElseIf rdbSelect.Text = "Detail" Then
    '        '    qry = qry + " and A.ServDateType in (" + YrStr + ")"
    '        'End If
    '    End If

    '    txtQuery.Text = qry + " order by A.StaffID,A.ServiceDate"


    '    Return True
    'End Function

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
            If rdbSelect.SelectedValue = "Detail" Then

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        '   If j = dt.Columns.Count - 7 Or j = dt.Columns.Count - 8 Or j = dt.Columns.Count - 9 Or j = dt.Columns.Count - 10 Or j = dt.Columns.Count - 11 Or j = dt.Columns.Count - 17 Then
                        If j = dt.Columns.Count - 8 Or j = dt.Columns.Count - 9 Or j = dt.Columns.Count - 10 Or j = dt.Columns.Count - 11 Or j = dt.Columns.Count - 12 Or j = dt.Columns.Count - 18 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                            '   ElseIf j >= 2 And j <= dt.Columns.Count - 25 Then
                        ElseIf j >= 2 And j <= dt.Columns.Count - 26 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                                cell.CellStyle = _intCellStyle

                            Else
                                '    Dim d As Int32 = Convert.ToInt32("0")
                                cell.SetCellValue("")
                                cell.CellStyle = AllCellStyle
                            End If

                            '    ElseIf j = dt.Columns.Count - 14 Or j = dt.Columns.Count - 16 Or j = dt.Columns.Count - 19 Or j = dt.Columns.Count - 21 Then
                        ElseIf j = dt.Columns.Count - 15 Or j = dt.Columns.Count - 17 Or j = dt.Columns.Count - 20 Or j = dt.Columns.Count - 22 Then

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

            ElseIf rdbSelect.SelectedValue = "Summary" Then

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        '   If j = dt.Columns.Count - 7 Or j = dt.Columns.Count - 8 Or j = dt.Columns.Count - 9 Or j = dt.Columns.Count - 10 Or j = dt.Columns.Count - 11 Or j = dt.Columns.Count - 17 Then
                        If j = dt.Columns.Count - 8 Or j = dt.Columns.Count - 9 Or j = dt.Columns.Count - 10 Or j = dt.Columns.Count - 11 Or j = dt.Columns.Count - 12 Or j = dt.Columns.Count - 18 Then

                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                        ElseIf j >= 2 And j <= dt.Columns.Count - 24 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                                cell.CellStyle = _intCellStyle

                            Else
                                '    Dim d As Int32 = Convert.ToInt32("0")
                                cell.SetCellValue("")
                                cell.CellStyle = AllCellStyle
                            End If

                        ElseIf j = dt.Columns.Count - 15 Or j = dt.Columns.Count - 17 Or j = dt.Columns.Count - 20 Or j = dt.Columns.Count - 22 Then
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

        Else

            If rdbSelect.SelectedValue = "Detail" Then

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = dt.Columns.Count - 7 Or j = dt.Columns.Count - 8 Or j = dt.Columns.Count - 9 Or j = dt.Columns.Count - 10 Or j = dt.Columns.Count - 11 Or j = dt.Columns.Count - 17 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                        ElseIf j >= 2 And j <= dt.Columns.Count - 25 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                                cell.CellStyle = _intCellStyle

                            Else
                                '    Dim d As Int32 = Convert.ToInt32("0")
                                cell.SetCellValue("")
                                cell.CellStyle = AllCellStyle
                            End If

                        ElseIf j = dt.Columns.Count - 14 Or j = dt.Columns.Count - 16 Or j = dt.Columns.Count - 19 Or j = dt.Columns.Count - 21 Then
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

            ElseIf rdbSelect.SelectedValue = "Summary" Then

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = dt.Columns.Count - 7 Or j = dt.Columns.Count - 8 Or j = dt.Columns.Count - 9 Or j = dt.Columns.Count - 10 Or j = dt.Columns.Count - 11 Or j = dt.Columns.Count - 17 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                        ElseIf j >= 2 And j <= dt.Columns.Count - 23 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                                cell.CellStyle = _intCellStyle

                            Else
                                '    Dim d As Int32 = Convert.ToInt32("0")
                                cell.SetCellValue("")
                                cell.CellStyle = AllCellStyle
                            End If

                        ElseIf j = dt.Columns.Count - 14 Or j = dt.Columns.Count - 16 Or j = dt.Columns.Count - 19 Or j = dt.Columns.Count - 21 Then
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
        End If

        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            '   Dim attachment As String = "attachment; filename=ManpowerProductivityTeamMemberAppointment"
            Dim attachment As String = ""

            If rdbSelect.SelectedValue = "Detail" Then

                If chkTimeSheet.Checked Then
                    attachment = "attachment; filename=Manpower Productivity by Team Member - Appointment & Vehicle Detail(TimeSheet)"
                Else
                    attachment = "attachment; filename=Manpower Productivity by Team Member - Appointment & Vehicle Detail"
                End If

            ElseIf rdbSelect.SelectedValue = "Summary" Then

                If chkTimeSheet.Checked Then
                    attachment = "attachment; filename=Manpower Productivity by Team Member - Appointment & Vehicle Summary(TimeSheet)"
                Else
                    attachment = "attachment; filename=Manpower Productivity by Team Member - Appointment & Vehicle Summary"
                End If

            End If

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

    Protected Sub btnEmailExcel_Click(sender As Object, e As EventArgs) Handles btnEmailExcel.Click

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim ContractGroup As String = ""
        Dim selection As String
        selection = ""

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                lblAlert.Text = "INVALID SERVICE DATE FROM"
                Return
            End If

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
                '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                lblAlert.Text = "INVALID SERVICE DATE TO"
                Return
            End If
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("'" + item.Value + "'")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
            '  ContractGroup = String.Concat("(", YrStr, ")")
            ContractGroup = YrStr
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If
        End If

        Session.Add("Selection", selection)
  
        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text
        Dim qry1 As String = "INSERT INTO tbwmanpowerreportgenerate(Generated,BatchNo,CreatedBy,CreatedOn,FileType,ReportType,TimeSheet,DateFrom,DateTo,Selection,Selformula,qry,RetryCount,ContractGroup,DomainName)"
        qry1 = qry1 + "VALUES(@Generated,@BatchNo,@CreatedBy,@CreatedOn,@FileType,@ReportType,@TimeSheet,@DateFrom,@DateTo,@Selection,@Selformula,@qry,@RetryCount,@ContractGroup,@DomainName);"

        command.CommandText = qry1
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@Generated", 0)
        command.Parameters.AddWithValue("@BatchNo", txtCreatedBy.Text + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


        command.Parameters.AddWithValue("@FileType", "Excel")
        If rdbSelect.SelectedValue = "Detail" Then
            command.Parameters.AddWithValue("@ReportType", "ProdTeamMemberApptDetail")
        ElseIf rdbSelect.SelectedValue = "Summary" Then
            command.Parameters.AddWithValue("@ReportType", "ProdTeamMemberAppt")
        End If

        If chkTimeSheet.Checked Then
            command.Parameters.AddWithValue("@TimeSheet", "Yes")
        Else
            command.Parameters.AddWithValue("@TimeSheet", "No")
        End If
        command.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@Selection", Session("Selection"))
        command.Parameters.AddWithValue("@SelFormula", "-")
        command.Parameters.AddWithValue("@qry", "-")
        command.Parameters.AddWithValue("@RetryCount", 0)
        command.Parameters.AddWithValue("@ContractGroup", ContractGroup)
        command.Parameters.AddWithValue("@DomainName", ConfigurationManager.AppSettings("DomainName").ToString())

        command.Connection = conn

        command.ExecuteNonQuery()

        command.Dispose()
        conn.Close()
        conn.Dispose()
        mdlPopupMsg.Show()
      
    End Sub
End Class
