Imports MySql.Data.MySqlClient
Imports System.Data
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel
Imports System.IO
Imports System.Net

Partial Class RV_Select_PortfolioCompare
    Inherits System.Web.UI.Page

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        btnExcel.Visible = False

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Check if records exists in tblportfoliocompare1

        Dim command0 As MySqlCommand = New MySqlCommand

        command0.CommandType = CommandType.Text

        command0.Connection = conn

        command0.CommandText = "select * from tblportfoliocompare1 where createdby='" & txtCreatedBy.Text & "';"
        command0.Connection = conn

        Dim dr0 As MySqlDataReader = command0.ExecuteReader()
        Dim dt0 As New DataTable
        dt0.Load(dr0)

        If dt0.Rows.Count = 0 Then
            lblAlert.Text = "UPLOAD FILE1 FOR COMPARISON"
            Exit Sub

        End If

        command0.Dispose()
        dt0.Clear()
        dt0.Dispose()
        dr0.Close()

        'Check if records exists in tblportfoliocompare2

        Dim command01 As MySqlCommand = New MySqlCommand

        command01.CommandType = CommandType.Text

        command01.Connection = conn

        command01.CommandText = "select * from tblportfoliocompare2 where createdby='" & txtCreatedBy.Text & "';"
        command01.Connection = conn

        Dim dr01 As MySqlDataReader = command01.ExecuteReader()
        Dim dt01 As New DataTable
        dt01.Load(dr01)

        If dt01.Rows.Count = 0 Then
            lblAlert.Text = "UPLOAD FILE2 FOR COMPARISON"
            Exit Sub

        End If

        command0.Dispose()
        dt0.Clear()
        dt0.Dispose()
        dr0.Close()

        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.StoredProcedure


        command1.CommandText = "spportfoliocompare"


        command1.Parameters.Clear()

        command1.Parameters.AddWithValue("@pr_CreatedBy", txtCreatedBy.Text)

        command1.Connection = conn
        command1.ExecuteScalar()

        command1.Dispose()
        'lblAlert.Text = "Total Records Imported : " + count + "<br>" + " Success : " + txtSuccessCount.Text + ", Failure : " + txtFailureCount.Text '+ " Failed AccountID : " + txtFailureString.Text


        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text

        command.Connection = conn

        command.CommandText = "select ContractNo,ContractGroup,Category1,date_format(PortfolioDate1,'%d-%m-%Y') as PortfolioDate1,PortfolioValue1,Category2,date_format(PortfolioDate2,'%d-%m-%Y') as PortfolioDate2,PortfolioValue2,DifferenceValue from tblportfoliocompare3 where differencevalue<>0 and createdby='" & txtCreatedBy.Text & "' and category1 not in ('NET GAIN','CLOSING');"
        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            GridView1.DataSource = dt
            GridView1.DataBind()

            btnExcel.Visible = True
            lblAlert.Text = "COMPARISON COMPLETED"
        Else

            GridView1.Visible = False

            btnExcel.Visible = False
            lblAlert.Text = "COMPARISON COMPLETED - NO RECORDS TO DISPLAY"
        End If
        dt.Clear()
        dt.Dispose()
        dr.Close()
        command.Dispose()

        conn.Close()
        conn.Dispose()


    End Sub

    Protected Function InsertPortfolioData(tablename As String, dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable

        InsertIntoTblWebEventLog("InsertPortfolioData1", dt.Rows.Count.ToString, txtCreatedBy.Text)
        'InsertIntoTblWebEventLog("InsertPortfolioData12", dt.Columns.Count.ToString, txtCreatedBy.Text)
        Try
            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry1 As String = "delete from " & tablename & " where createdby = '" & txtCreatedBy.Text & "';"

            command.CommandText = qry1

            command.Connection = conn

            command.ExecuteNonQuery()

            Dim qry As String = "insert into " & tablename & "(Category,SubCategory,ContractNo,AgreeValue,PortfolioDate,PortfolioValue,CreatedOn,CreatedBy,ContractGroup)"
            qry = qry + "VALUES(@Category,@SubCategory,@ContractNo,@AgreeValue,@PortfolioDate,@PortfolioValue,@CreatedOn,@CreatedBy,@ContractGroup)"
            ' InsertIntoTblWebEventLog("InsertPortfolioData2", qry, txtCreatedBy.Text)
            Dim drow As DataRow

            For Each r As DataRow In dt.Rows

                drow = dtLog.NewRow()
                Dim cmd As MySqlCommand = conn.CreateCommand()
                '  Dim cmd As MySqlCommand = New MySqlCommand

                cmd.CommandType = CommandType.Text
                cmd.CommandText = qry
                cmd.Parameters.Clear()

                If IsDBNull(r("Category")) Then
                    cmd.Parameters.AddWithValue("@Category", "")
                Else
                    cmd.Parameters.AddWithValue("@Category", r("Category").ToString.ToUpper)
                End If
                If IsDBNull(r("SubCategory")) Then
                    cmd.Parameters.AddWithValue("@SubCategory", "")
                Else
                    cmd.Parameters.AddWithValue("@SubCategory", r("SubCategory").ToString.ToUpper)
                End If
                If IsDBNull(r("ContractNo")) Then
                    cmd.Parameters.AddWithValue("@ContractNo", "")
                Else
                    cmd.Parameters.AddWithValue("@ContractNo", r("ContractNo").ToString.ToUpper)
                End If
                If IsDBNull(r("LatestAgreeValue")) Then
                    cmd.Parameters.AddWithValue("@AgreeValue", 0.0)
                Else
                    cmd.Parameters.AddWithValue("@AgreeValue", r("LatestAgreeValue"))
                End If
                If IsDBNull(r("TransactionDate")) Then
                    cmd.Parameters.AddWithValue("@PortfolioDate", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@PortfolioDate", Convert.ToDateTime(r("TransactionDate")).ToString("yyyy-MM-dd"))
                End If
                If IsDBNull(r("PortfolioValue")) Then
                    cmd.Parameters.AddWithValue("@PortfolioValue", 0.0)
                Else
                    cmd.Parameters.AddWithValue("@PortfolioValue", r("PortfolioValue"))
                End If

                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)

                If IsDBNull(r("ContractGroup")) Then
                    cmd.Parameters.AddWithValue("@ContractGroup", "")
                Else
                    cmd.Parameters.AddWithValue("@ContractGroup", r("ContractGroup").ToString.ToUpper)
                End If
                cmd.Connection = conn

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                Success = Success + 1

                dtLog.Rows.Add(drow)

                ' InsertIntoTblWebEventLog("InserData2", Success.ToString, Failure.ToString)

            Next


            dt.Clear()


            Return True
        Catch ex As Exception
            lblAlert.Text = tablename + " " + ex.Message.ToString


            InsertIntoTblWebEventLog("InsertPortfolioData", tablename + " " + ex.Message.ToString, txtCreatedBy.Text)

            Return False

        End Try

    End Function

    Private Function Excel_To_DataTable(ByVal pRutaArchivo As String, ByVal pHojaIndex As Integer) As DataTable
        Dim Tabla As DataTable = Nothing

        ' Try

        If System.IO.File.Exists(pRutaArchivo) Then
            Dim workbook As IWorkbook = Nothing
            Dim worksheet As ISheet = Nothing
            Dim first_sheet_name As String = ""

            Using FS As FileStream = New FileStream(pRutaArchivo, FileMode.Open, FileAccess.Read)
                workbook = WorkbookFactory.Create(FS)
                worksheet = workbook.GetSheetAt(pHojaIndex)
                first_sheet_name = worksheet.SheetName

                'If worksheet.SheetName.ToUpper = rdbModule.SelectedValue.ToString.ToUpper Then
                '    '  InsertIntoTblWebEventLog("TestExcel0", worksheet.SheetName.ToUpper + worksheet.LastRowNum.ToString + "Aa", "")


                If worksheet.LastRowNum > 2 Then
                    'Dim row0 As IRow = worksheet.GetRow(0)
                    'worksheet.RemoveRow(row0)

                    Tabla = New DataTable(first_sheet_name)
                    Tabla.Rows.Clear()
                    Tabla.Columns.Clear()

                    For rowIndex As Integer = 1 To worksheet.LastRowNum

                        Dim NewReg As DataRow = Nothing
                        Dim row As IRow = worksheet.GetRow(rowIndex)
                        Dim row2 As IRow = Nothing
                        Dim row3 As IRow = Nothing

                        If rowIndex = 1 Then
                            row2 = worksheet.GetRow(rowIndex + 1)
                            row3 = worksheet.GetRow(rowIndex + 2)
                        End If

                        'InsertIntoTblWebEventLog("TestExcel1", rowIndex.ToString, worksheet.LastRowNum.ToString)
                        'InsertIntoTblWebEventLog("TestExcel11", row.RowNum.ToString, row2.RowNum.ToString + " " + row3.RowNum.ToString)

                        If row IsNot Nothing Then
                            '  InsertIntoTblWebEventLog("TestExcel11", row.LastCellNum.ToString, row.RowNum.ToString)

                            If rowIndex > 1 Then NewReg = Tabla.NewRow()
                            Dim colIndex As Integer = 0
                            '   InsertIntoTblWebEventLog("TestExcel11", row.Cells.Count.ToString, row.RowNum.ToString)

                            For Each cell As ICell In row.Cells
                                Dim valorCell As Object = Nothing
                                Dim cellType As String = ""
                                Dim cellType2 As String() = New String(1) {}
                                ' 
                                If rowIndex = 1 Then

                                    ' ElseIf rowIndex = 2 Then

                                    '   InsertIntoTblWebEventLog("TestExcel12", rowIndex.ToString, cell.ColumnIndex.ToString)
                                    For i As Integer = 0 To 2 - 1
                                        Dim cell2 As ICell = Nothing

                                        If i = 0 Then
                                            cell2 = row2.GetCell(cell.ColumnIndex)
                                        Else
                                            cell2 = row3.GetCell(cell.ColumnIndex)
                                        End If
                                        '     InsertIntoTblWebEventLog("TestExcel5", cell.ColumnIndex.ToString, row2.GetCell(cell.ColumnIndex).ToString)

                                        If cell2 IsNot Nothing Then
                                            '  InsertIntoTblWebEventLog("TestExcel5", cell.ColumnIndex.ToString, row2.GetCell(cell.ColumnIndex).ToString)

                                            Select Case cell2.CellType
                                                Case NPOI.SS.UserModel.CellType.Blank
                                                    cellType2(i) = "System.String"
                                                Case NPOI.SS.UserModel.CellType.Boolean
                                                    cellType2(i) = "System.Boolean"
                                                Case NPOI.SS.UserModel.CellType.String
                                                    cellType2(i) = "System.String"
                                                Case NPOI.SS.UserModel.CellType.Numeric

                                                    If HSSFDateUtil.IsCellDateFormatted(cell2) Then
                                                        cellType2(i) = "System.DateTime"
                                                    Else
                                                        cellType2(i) = "System.Double"
                                                    End If

                                                Case NPOI.SS.UserModel.CellType.Formula
                                                    Dim continuar As Boolean = True

                                                    Select Case cell2.CachedFormulaResultType
                                                        Case NPOI.SS.UserModel.CellType.Boolean
                                                            cellType2(i) = "System.Boolean"
                                                        Case NPOI.SS.UserModel.CellType.String
                                                            cellType2(i) = "System.String"
                                                        Case NPOI.SS.UserModel.CellType.Numeric

                                                            If HSSFDateUtil.IsCellDateFormatted(cell2) Then
                                                                cellType2(i) = "System.DateTime"
                                                            Else

                                                                Try

                                                                    If cell2.CellFormula = "TRUE()" Then
                                                                        cellType2(i) = "System.Boolean"
                                                                        continuar = False
                                                                    End If

                                                                    If continuar AndAlso cell2.CellFormula = "FALSE()" Then
                                                                        cellType2(i) = "System.Boolean"
                                                                        continuar = False
                                                                    End If

                                                                    If continuar Then
                                                                        cellType2(i) = "System.Double"
                                                                        continuar = False
                                                                    End If

                                                                Catch
                                                                End Try
                                                            End If
                                                    End Select

                                                Case Else
                                                    cellType2(i) = "System.String"
                                            End Select
                                        Else
                                            cellType2(i) = "System.String"
                                        End If
                                        InsertIntoTblWebEventLog("TestExcel4", cell.ColumnIndex.ToString, cellType2(i).ToString)

                                    Next

                                    If cellType2(0) = cellType2(1) Then
                                        cellType = cellType2(0)
                                    Else
                                        If cellType2(0) Is Nothing Then cellType = cellType2(1)
                                        If cellType2(1) Is Nothing Then cellType = cellType2(0)
                                        If cellType = "" Then cellType = "System.String"
                                    End If

                                    Dim colName As String = "Column_{0}"

                                    Try
                                        colName = cell.StringCellValue
                                    Catch
                                        colName = String.Format(colName, colIndex)
                                    End Try
                                    ' InsertIntoTblWebEventLog("TestExcel2", colName, colIndex)


                                    For Each col As DataColumn In Tabla.Columns
                                        If col.ColumnName = colName Then colName = String.Format("{0}_{1}", colName, colIndex)
                                    Next
                                    InsertIntoTblWebEventLog("TestExcel3", colName, cellType)

                                    Dim codigo As DataColumn = New DataColumn(colName, System.Type.[GetType](cellType))
                                    Tabla.Columns.Add(codigo)
                                    colIndex += 1

                                    'Dim codigo1 As DataColumn = New DataColumn("Category", "System.String")
                                    'Tabla.Columns.Add(codigo)

                                Else

                                    Select Case cell.CellType
                                        Case NPOI.SS.UserModel.CellType.Blank
                                            valorCell = DBNull.Value
                                        Case NPOI.SS.UserModel.CellType.Boolean
                                            'Select Case cell.BooleanCellValue
                                            '    Case True
                                            valorCell = cell.BooleanCellValue
                                            '    Case False
                                            '        valorCell = cell.BooleanCellValue
                                            '    Case Else
                                            '        valorCell = False

                                            'End Select

                                        Case NPOI.SS.UserModel.CellType.String
                                            valorCell = cell.StringCellValue
                                        Case NPOI.SS.UserModel.CellType.Numeric

                                            If HSSFDateUtil.IsCellDateFormatted(cell) Then
                                                valorCell = cell.DateCellValue
                                            Else
                                                valorCell = cell.NumericCellValue
                                            End If

                                        Case NPOI.SS.UserModel.CellType.Formula

                                            Select Case cell.CachedFormulaResultType
                                                Case NPOI.SS.UserModel.CellType.Blank
                                                    valorCell = DBNull.Value
                                                Case NPOI.SS.UserModel.CellType.String
                                                    valorCell = cell.StringCellValue
                                                Case NPOI.SS.UserModel.CellType.Boolean
                                                    valorCell = cell.BooleanCellValue
                                                Case NPOI.SS.UserModel.CellType.Numeric

                                                    If HSSFDateUtil.IsCellDateFormatted(cell) Then
                                                        valorCell = cell.DateCellValue
                                                    Else
                                                        valorCell = cell.NumericCellValue
                                                    End If
                                            End Select

                                        Case Else
                                            valorCell = cell.StringCellValue
                                    End Select
                                    '   InsertIntoTblWebEventLog("TestExcel6", cell.ColumnIndex.ToString + " " + cell.CellType.ToString, valorCell.ToString)

                                    'If cell.CellType.ToString = "Boolean" Then
                                    '    If valorCell.ToString = "TRUE" Or valorCell.ToString = "FALSE" Then

                                    '    Else
                                    '        Continue For

                                    '    End If
                                    'End If

                                    If cell.ColumnIndex <= Tabla.Columns.Count - 1 Then NewReg(cell.ColumnIndex) = valorCell
                                End If
                            Next
                        End If

                        If rowIndex > 1 Then Tabla.Rows.Add(NewReg)
                    Next


                    Tabla.AcceptChanges()
                    'Else
                    '    lblMessage.Text = "Please import more than one Account details to proceed."
                End If


            End Using
        Else
            Throw New Exception("ERROR 404")
        End If

        'Catch ex As Exception
        '    lblAlert.Text = ex.Message.ToString

        '    InsertIntoTblWebEventLog("Excel_To_DataTable", ex.Message.ToString, txtCreatedBy.Text)
        '    Throw ex

        'End Try

        Return Tabla
    End Function

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
            insCmds.Parameters.AddWithValue("@LoginId", "PORTFOLIO COMPARE - " + Session("UserID"))
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

            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, txtCreatedBy.Text)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID

        FileUpload1.Attributes("onchange") = "UploadFile1(this)"
        FileUpload2.Attributes("onchange") = "UploadFile2(this)"
    End Sub

    Protected Sub btnUpload1_Click(sender As Object, e As EventArgs) Handles btnUpload1.Click
        lblAlert.Text = ""
        '   lblMessage.Text = ""
        Label1.Text = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        '  If FileUpload1.HasFile Then
        Dim ofilename As String = ""
        Dim sfilename As String = ""

        ofilename = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim ext As String = Path.GetExtension(ofilename)
        sfilename = ofilename.Split("."c)(0)

        Dim folderPath As String = Server.MapPath("~/Uploads/Portfolio/")
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If
        Dim fileName As String = folderPath + sfilename + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + "_" + txtCreatedBy.Text + ext

        If System.IO.File.Exists(fileName) Then

            System.IO.File.Delete(fileName)
        End If
        'Save the File to the Directory (Folder).
        FileUpload1.PostedFile.SaveAs(fileName)

        txtWorkBookName.Text = sfilename
        Dim file As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)
        Dim dropdownworkbook = New XSSFWorkbook(file)
        file.Close()
        file.Dispose()

        Dim dt As New DataTable
        dt = Excel_To_DataTable(fileName, 0)
        ' Response.Write(dt.Rows.Count.ToString)
        '  InsertIntoTblWebEventLog("ExportToExcel", dt.Rows.Count.ToString, txtCreatedBy.Text)
        ' InsertIntoTblWebEventLog("ExportToExcel2", dt.Columns.Count.ToString, txtCreatedBy.Text)

        If dt Is Nothing Then
            lblAlert.Text = "FILE1 DATA NOT IMPORTED"
        Else
            Dim res As Boolean = True

            Dim count As String = dt.Rows.Count.ToString

            If InsertPortfolioData("tblportfoliocompare1", dt, conn) = True Then

                lblAlert.Text = "FILE1 UPLOADED"
                'Else
                '    lblAlert.Text = "FILE1 CANNOT BE UPLOADED"
            End If

            'Else
            'lblAlert.Text = "SELECT FILE1 TO UPLOAD"
        End If

        conn.Close()
        conn.Dispose()
    End Sub

    Protected Sub btnUpload2_Click(sender As Object, e As EventArgs) Handles btnUpload2.Click
        lblAlert.Text = ""
        '   lblMessage.Text = ""
        Label2.Text = Path.GetFileName(FileUpload2.PostedFile.FileName)
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        '  If FileUpload2.HasFile Then
        Dim ofilename As String = ""
        Dim sfilename As String = ""

        ofilename = Path.GetFileName(FileUpload2.PostedFile.FileName)
        Dim ext As String = Path.GetExtension(ofilename)
        sfilename = ofilename.Split("."c)(0)

        Dim folderPath As String = Server.MapPath("~/Uploads/Portfolio/")
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If
        Dim fileName As String = folderPath + sfilename + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + "_" + txtCreatedBy.Text + ext

        If System.IO.File.Exists(fileName) Then

            System.IO.File.Delete(fileName)
        End If
        'Save the File to the Directory (Folder).
        FileUpload2.PostedFile.SaveAs(fileName)

        txtWorkBookName.Text = sfilename
        Dim file As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)
        Dim dropdownworkbook = New XSSFWorkbook(file)
        file.Close()
        file.Dispose()

        Dim dt As New DataTable
        dt = Excel_To_DataTable(fileName, 0)
        ' Response.Write(dt.Rows.Count.ToString)
        '  InsertIntoTblWebEventLog("ExportToExcel", dt.Rows.Count.ToString, txtCreatedBy.Text)
        ' InsertIntoTblWebEventLog("ExportToExcel2", dt.Columns.Count.ToString, txtCreatedBy.Text)

        If dt Is Nothing Then
            lblAlert.Text = "FILE1 DATA NOT IMPORTED"
        Else
            Dim res As Boolean = True

            Dim count As String = dt.Rows.Count.ToString


            If InsertPortfolioData("tblportfoliocompare2", dt, conn) = True Then

                lblAlert.Text = "FILE2 UPLOADED"
                'Else

                '    lblAlert.Text = "FILE2 CANNOT BE UPLOADED"
            End If

            'Else
            '    lblAlert.Text = "SELECT FILE2 TO UPLOAD"
        End If

        conn.Close()
        conn.Dispose()
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As ImageClickEventArgs) Handles btnExcel.Click
        Try

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.ClearHeaders()
            Response.Charset = ""
            Dim FileName As String = "PortfolioComparison_Log_" & DateTime.Now & ".xls"
            Dim strwritter As StringWriter = New StringWriter()
            Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & FileName)
            GridView1.GridLines = GridLines.Both
            GridView1.HeaderStyle.Font.Bold = True
            GridView1.RenderControl(htmltextwrtter)
            Response.Write(strwritter.ToString())
            Response.[End]()


        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            InsertIntoTblWebEventLog("btnExportToExcel_Click", ex.Message.ToString, "")
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
End Class
