
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Public Class RV_Select_ActiveContract
    Inherits System.Web.UI.Page

  
        Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
            txtAccountID.Text = ""
            txtCustName.Text = ""

        End Sub

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        If String.IsNullOrEmpty(ddlAccountType.Text) Then
            '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If
        If ddlAccountType.Text = "-1" Then
            '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If

        If String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            txtPopUpClient.Text = txtCustName.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        Else
            ' txtPopUpClient.Text = ""
            ' txtPopupClientSearch.Text = ""
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        End If
        mdlPopUpClient.Show()
    End Sub


    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging

        If txtPopupClientSearch.Text.Trim = "" Then
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where rcno <>0 order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"
            End If


        Else
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
        End If

        SqlDSClient.DataBind()
        gvClient.DataBind()
        gvClient.PageIndex = e.NewPageIndex

        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As EventArgs)
        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs)
        txtPopUpClient.Text = "Search Here for AccountID or Client details"
        txtPopupClientSearch.Text = ""
        If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
            SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where rcno <>0 order by name"
        ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
            SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"
        End If
        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
        End If

        txtPopUpClient.Text = "Search Here for AccountID or Client details"
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
            txtAccountID.Text = ""
        Else
            txtAccountID.Text = gvClient.SelectedRow.Cells(2).Text.Trim
        End If

        If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
            txtCustName.Text = ""
        Else
            txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim).ToString
        End If

    End Sub

    Protected Sub btnSort1_Click(sender As Object, e As EventArgs) Handles btnSort1.Click
        Dim i As Integer = lstSort1.SelectedIndex

        If i = -1 Then
            Exit Sub 'skip if no item is selected
        End If

        lstSort2.Items.Add(lstSort1.Items(i))
        lstSort1.Items.RemoveAt(i)


    End Sub

    Protected Sub btnSort2_Click(sender As Object, e As EventArgs) Handles btnSort2.Click
        Dim i As Integer = lstSort2.SelectedIndex

        If i = -1 Then
            Exit Sub 'skip if no item is selected
        End If

        lstSort1.Items.Add(lstSort2.Items(i))
        lstSort2.Items.RemoveAt(i)


    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtActiveAsOfDate.Text = Convert.ToString(Session("SysDate"))

        End If



    End Sub

    Protected Sub btnCloseServiceContractList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceContractList.Click
        txtActiveAsOfDate.Text = ""
        '   txtContractDateTo.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        ddlContractGroup.SelectedIndex = 0
        ddlSalesMan.SelectedIndex = 0
        ddlScheduler.SelectedIndex = 0
        ddlIncharge.SelectedIndex = 0
        ddlIndustry.SelectedIndex = 0
        ddlLocateGrp.SelectedIndex = 0
        ddlCategoryID.SelectedIndex = 0

        'txtStartDateFrom.Text = ""
        'txtStartDateTo.Text = ""
        'txtEndDateFrom.Text = ""
        'txtEndDateTo.Text = ""
        'txtActualEndFrom.Text = ""
        'txtActualEndTo.Text = ""

        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""
        lstSort2.Items.Clear()
    End Sub

    Protected Sub btnPrintServiceContractList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceContractList.Click
        If String.IsNullOrEmpty(txtActiveAsOfDate.Text) Then
            lblAlert.Text = "ENTER ACTIVE AS OF DATE"
            Return
        End If

        If GetData() = True Then

            If chkGrouping.SelectedValue = "ContractGroup" Then
                If chkIncludeContactInfo.Checked = True Then
                    Session.Add("IncludeContactInfo", "Yes")
                Else
                    Session.Add("IncludeContactInfo", "No")

                End If
                Session.Add("ReportType", "ActiveContract02")
                Response.Redirect("RV_ActiveContract02.aspx")
            ElseIf chkGrouping.SelectedValue = "AccountID" Then
                Session.Add("ReportType", "ActiveContract01")
                Response.Redirect("RV_ActiveContract01.aspx")
                'ElseIf chkGrouping.SelectedValue = "Salesman" Then
                '    Response.Redirect("RV_NewContract03.aspx")

            End If
        Else
            Return

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
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + Session("Selection").ToString)

        ' cell1.SetCellValue(Session("Selection").ToString)
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

        '28.11

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 19 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 20 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

                    ElseIf j = 26 Or j = 22 Or j = 23 Or j = 24 Or j = 25 Then
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

                    If j = 14 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 15 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

                    ElseIf j = 17 Or j = 18 Or j = 19 Or j = 20 Or j = 21 Then
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


        '28.11



        'If Convert.ToString(Session("LocationEnabled")) = "Y" Then

        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        Dim row As IRow = sheet1.CreateRow(i + 2)

        '        For j As Integer = 0 To dt.Columns.Count - 1
        '            Dim cell As ICell = row.CreateCell(j)

        '            If j = 15 Then
        '                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
        '                    Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
        '                    cell.SetCellValue(d)
        '                Else
        '                    Dim d As Double = Convert.ToDouble("0.00")
        '                    cell.SetCellValue(d)

        '                End If
        '                cell.CellStyle = _doubleCellStyle

        '            ElseIf j = 16 Then
        '                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
        '                    Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
        '                    cell.SetCellValue(d)
        '                Else
        '                    Dim d As Int32 = Convert.ToInt32("0")
        '                    cell.SetCellValue(d)

        '                End If
        '                cell.CellStyle = _intCellStyle

        '            ElseIf j = 22 Or j = 18 Or j = 19 Or j = 20 Or j = 21 Then
        '                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
        '                    Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
        '                    cell.SetCellValue(d)
        '                    'Else
        '                    '    Dim d As Double = Convert.ToDouble("0.00")
        '                    '    cell.SetCellValue(d)

        '                End If
        '                cell.CellStyle = dateCellStyle
        '            Else
        '                cell.SetCellValue(dt.Rows(i)(j).ToString)
        '                cell.CellStyle = AllCellStyle

        '            End If
        '            If i = dt.Rows.Count - 1 Then
        '                sheet1.AutoSizeColumn(j)
        '            End If
        '        Next
        '    Next

        'Else

        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        Dim row As IRow = sheet1.CreateRow(i + 2)

        '        For j As Integer = 0 To dt.Columns.Count - 1
        '            Dim cell As ICell = row.CreateCell(j)

        '            If j = 14 Then
        '                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
        '                    Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
        '                    cell.SetCellValue(d)
        '                Else
        '                    Dim d As Double = Convert.ToDouble("0.00")
        '                    cell.SetCellValue(d)

        '                End If
        '                cell.CellStyle = _doubleCellStyle

        '            ElseIf j = 15 Then
        '                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
        '                    Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
        '                    cell.SetCellValue(d)
        '                Else
        '                    Dim d As Int32 = Convert.ToInt32("0")
        '                    cell.SetCellValue(d)

        '                End If
        '                cell.CellStyle = _intCellStyle

        '            ElseIf j = 17 Or j = 18 Or j = 19 Or j = 20 Or j = 21 Then
        '                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
        '                    Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
        '                    cell.SetCellValue(d)
        '                    'Else
        '                    '    Dim d As Double = Convert.ToDouble("0.00")
        '                    '    cell.SetCellValue(d)

        '                End If
        '                cell.CellStyle = dateCellStyle
        '            Else
        '                cell.SetCellValue(dt.Rows(i)(j).ToString)
        '                cell.CellStyle = AllCellStyle

        '            End If
        '            If i = dt.Rows.Count - 1 Then
        '                sheet1.AutoSizeColumn(j)
        '            End If
        '        Next
        '    Next

        'End If



        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = ""
            attachment = "attachment; filename=ActiveContract_" + Convert.ToDateTime(txtActiveAsOfDate.Text).ToString("yyyyMMdd")

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
        If String.IsNullOrEmpty(txtActiveAsOfDate.Text) Then
            lblAlert.Text = "ENTER ACTIVE AS OF DATE"
            Return
        End If

        If GetData() = True Then
            'lblAlert.Text = txtQuery.Text
            'Return

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=ActiveContract.xls"
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

        Dim qry As String = ""
        qry = "select distinct "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "A.location as Branch,"
        End If

        qry = qry + "A.ContactType,D.Location as MasterBranch,A.Location as ContractBranch,A.Status,A.GSt,A.Salesman,A.ContractNo,A.AccountID, C.BillingName as AccountName, A.CustName as ServiceName,replace(replace(trim(A.ServiceAddress), char(10), ' '), char(13), ' ') AS ServiceAddress, C.City, C.State, C.Country, C.Postal "
        qry = qry + ",A.CompanyGroup,A.ContractGroup,A.Industry,A.AgreeValue,A.Duration,A.DurationMS,A.StartDate,A.EndDate,A.ServiceStart,A.ServiceEnd,"
        qry = qry + "(case when A.ActualEnd is null then A.EndDate when A.ActualEnd is not null then A.ActualEnd end) as ActualContractEnd,replace(replace(trim(ServiceDescription), char(10), ' '), char(13), ' ') as ServiceDescription"

        If chkIncludeContactInfo.Checked = True Then
            qry = qry + ",C.ContactPerson,C.Telephone,C.Mobile,replace(replace(trim(C.Email), char(10), ' '), char(13), ' ') as Email,C.ContactPerson2,C.Contact2Tel,C.Contact2Mobile,replace(replace(trim(C.Contact2Email), char(10), ' '), char(13), ' ') as Contact2Email,TRIM(C.ServiceEmailCC) AS ServiceEmailCC"
            qry = qry + ",C.BillContact1Svc,replace(replace(trim(BillEmail1Svc), char(10), ' '), char(13), ' ') as BillEmail1Svc,C.BillContact2Svc,replace(replace(trim(C.BillEmail2Svc), char(10), ' '), char(13), ' ') as BillEmail2Svc"
        End If

        qry = qry + " from tblContract A inner join tblContractDet B on A.ContractNo=B.ContractNo inner join vwcustomerservicecontactinfo C on B.LocationID=C.LocationID "
        qry = qry + " left outer join tblCompany D on A.AccountID=D.AccountID"
        qry = qry + " where A.rcno<>''"
        '   qry = qry + " A.Status in ('O','P')"


        Dim selFormula As String = ""
        Dim selection As String
        selection = ""
        ' selFormula = "{tblcontract1.rcno} <> 0 and {tblcontract1.status}<>""V"" and {tblcontract1.status}=""O"" and {tblcontract1.status}<>""P"" "
        '   selFormula = "{tblcontract1.status} in [""O"",""P""]"
        selFormula = "{tblcontract1.rcno} <> 0"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and A.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If String.IsNullOrEmpty(txtActiveAsOfDate.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtActiveAsOfDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Active AsOf Date is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + " and (if isnull({tblcontract1.ActualEnd}) and isnull({tblcontract1.EndDate}) then {tblcontract1.rcno} <> 0 else {@fDate} > " + "#" + d.ToString("MM-dd-yyyy") + "#)"
            selFormula = selFormula + " and {tblcontract1.StartDate} < " + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Active AsOf Date > " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Active AsOf Date > " + d.ToString("dd-MM-yyyy")
            End If

            qry = qry + " and A.StartDate < '" + Convert.ToDateTime(txtActiveAsOfDate.Text).ToString("yyyy-MM-dd") + "'"
            qry = qry + " and (case when A.ActualEnd is null and A.EndDATE IS NULL THEN A.rcno<>'' "
            qry = qry + " when A.ActualEnd is null then A.EndDate > '" + Convert.ToDateTime(txtActiveAsOfDate.Text).ToString("yyyy-MM-dd") + "'"
            qry = qry + " when A.ActualEnd is not null then A.ActualEnd > '" + Convert.ToDateTime(txtActiveAsOfDate.Text).ToString("yyyy-MM-dd") + "'end)"


        End If

        'If String.IsNullOrEmpty(txtContractDateTo.Text) = False Then
        '    Dim d As DateTime
        '    If Date.TryParseExact(txtContractDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

        '    Else
        '        MessageBox.Message.Alert(Page, "Contract Date To is invalid", "str")
        '        '  lblAlert.Text = "INVALID START DATE"
        '        Return False
        '    End If
        '    selFormula = selFormula + "and {tblcontract1.ContractDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '    If selection = "" Then
        '        selection = "Contract Date <= " + d.ToString("dd-MM-yyyy")
        '    Else
        '        selection = selection + ", Contract Date <= " + d.ToString("dd-MM-yyyy")
        '    End If
        '    qry = qry + " and A.ContractDate <= '" + Convert.ToDateTime(txtContractDateTo.Text).ToString("yyyy-MM-dd") + "'"

        'End If

        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

            selFormula = selFormula + " and {tblcontract1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            qry = qry + " and A.CompanyGroup in (" + YrStr + ")"
        End If


        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "Department = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + ", Department = " + ddlContractGroup.Text
        '    End If
        '    qry = qry + " and A.ContractGroup = '" + ddlContractGroup.Text + "'"
        'End If

        Dim YrStrList3 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlContractGroup.Items
            If item.Selected Then

                YrStrList3.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList3.Count > 0 Then

            Dim YrStr3 As [String] = [String].Join(",", YrStrList3.ToArray)

            selFormula = selFormula + " and {tblcontract1.ContractGroup} in [" + YrStr3 + "]"
            If selection = "" Then
                selection = "ContractGroup : " + YrStr3
            Else
                selection = selection + ", ContractGroup : " + YrStr3
            End If
            qry = qry + " and A.ContractGroup in (" + YrStr3 + ")"
        End If

        If ddlCategoryID.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.CategoryID} = '" + ddlCategoryID.Text + "'"
            If selection = "" Then
                selection = "CategoryID = " + ddlCategoryID.Text
            Else
                selection = selection + ", CategoryID = " + ddlCategoryID.Text
            End If
            qry = qry + " and A.CategoryID = '" + ddlCategoryID.Text + "'"
        End If

        If ddlSalesMan.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.Salesman} = '" + ddlSalesMan.Text + "'"
            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + ", Salesman = " + ddlSalesMan.Text
            End If
            qry = qry + " and A.Salesman = '" + ddlSalesMan.Text + "'"
        End If

        If ddlScheduler.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.Scheduler} = '" + ddlScheduler.Text + "'"
            If selection = "" Then
                selection = "Scheduler = " + ddlScheduler.Text
            Else
                selection = selection + ", Scheduler = " + ddlScheduler.Text
            End If
            qry = qry + " and A.Scheduler = '" + ddlScheduler.Text + "'"
        End If

        If ddlIncharge.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.InchargeID} = '" + ddlIncharge.Text + "'"
            If selection = "" Then
                selection = "Incharge = " + ddlIncharge.Text
            Else
                selection = selection + ", Incharge = " + ddlIncharge.Text
            End If
            qry = qry + " and A.Inchargeid = '" + ddlIncharge.Text + "'"
        End If

        If ddlLocateGrp.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.LocateGrp} = '" + ddlLocateGrp.Text + "'"
            If selection = "" Then
                selection = "LocateGrp = " + ddlLocateGrp.Text
            Else
                selection = selection + ", LocateGrp = " + ddlLocateGrp.Text
            End If
            qry = qry + " and A.Locategrp = '" + ddlLocateGrp.Text + "'"
        End If

        'If ddlIndustry.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblcontract1.Industry} = '" + ddlIndustry.Text + "'"
        '    If selection = "" Then
        '        selection = "Industry = " + ddlIndustry.Text
        '    Else
        '        selection = selection + ", Industry = " + ddlIndustry.Text
        '    End If
        '    qry = qry + " and A.Industry = '" + ddlIndustry.Text + "'"
        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlIndustry.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr2 As [String] = [String].Join(",", YrStrList2.ToArray)

            selFormula = selFormula + " and {tblcontract1.Industry} in [" + YrStr2 + "]"
            If selection = "" Then
                selection = "Industry : " + YrStr2
            Else
                selection = selection + ", Industry : " + YrStr2
            End If
            qry = qry + " and A.Industry in (" + YrStr2 + ")"
        End If

        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "Account Type = " + ddlAccountType.Text
            Else
                selection = selection + ", Account Type = " + ddlAccountType.Text
            End If
            qry = qry + " and A.ContactType = '" + ddlAccountType.Text + "'"
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblcontract1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID = " + txtAccountID.Text
            Else
                selection = selection + ", AccountID = " + txtAccountID.Text
            End If
            qry = qry + " and A.AccountID = '" + txtAccountID.Text + "'"
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblcontract1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "Client Name = " + txtCustName.Text
            Else
                selection = selection + ", Client Name = " + txtCustName.Text
            End If
            qry = qry + " and A.CustName = '" + txtCustName.Text + "'"
        End If

        If String.IsNullOrEmpty(lstSort2.Text) = False Then
            If lstSort2.Items(0).Selected = True Then


            End If
            Dim YrStrList As List(Of [String]) = New List(Of String)()
            For Each item As ListItem In lstSort2.Items
                If item.Selected Then

                    YrStrList.Add(item.Value)

                End If
            Next
            If YrStrList.Count > 0 Then
                For i As Integer = 0 To YrStrList.Count - 1
                    If i = 0 Then
                        Session.Add("sort1", YrStrList.Item(i).ToString)
                    ElseIf i = 1 Then
                        Session.Add("sort2", YrStrList.Item(i).ToString)
                    ElseIf i = 2 Then
                        Session.Add("sort3", YrStrList.Item(i).ToString)
                    ElseIf i = 3 Then
                        Session.Add("sort4", YrStrList.Item(i).ToString)
                    ElseIf i = 4 Then
                        Session.Add("sort5", YrStrList.Item(i).ToString)
                    ElseIf i = 5 Then
                        Session.Add("sort6", YrStrList.Item(i).ToString)
                        'ElseIf i = 6 Then
                        '    Session.Add("sort7", YrStrList.Item(i).ToString)
                    End If

                Next
            End If

        End If

        qry = qry + " order by D.Location,A.AccountID,A.Location,A.ContractNo,A.CustName,A.StartDate; "
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        txtQuery.Text = qry

        Return True

    End Function

  
End Class

