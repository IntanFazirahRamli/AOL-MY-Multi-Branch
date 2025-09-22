Imports MySql.Data.MySqlClient
Imports System.Data
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel
Imports System.IO
Imports System.Net

Partial Class ExcelDataImport
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedBy.Text = Convert.ToString(Session("UserID"))
        If Not IsPostBack Then
            btnExportToExcel.Visible = False

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
            insCmds.Parameters.AddWithValue("@LoginId", "Excel Data Import - " + Session("UserID"))
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

    Private Function Excel_To_DataTable(ByVal pRutaArchivo As String, ByVal pHojaIndex As Integer) As DataTable
        Dim Tabla As DataTable = Nothing

        Try

            If System.IO.File.Exists(pRutaArchivo) Then
                Dim workbook As IWorkbook = Nothing
                Dim worksheet As ISheet = Nothing
                Dim first_sheet_name As String = ""

                Using FS As FileStream = New FileStream(pRutaArchivo, FileMode.Open, FileAccess.Read)
                    workbook = WorkbookFactory.Create(FS)
                    worksheet = workbook.GetSheetAt(pHojaIndex)
                    first_sheet_name = worksheet.SheetName

                    If worksheet.SheetName.ToUpper = rdbModule.SelectedValue.ToString.ToUpper Then
                        '  InsertIntoTblWebEventLog("TestExcel0", worksheet.SheetName.ToUpper + worksheet.LastRowNum.ToString + "Aa", "")


                        If worksheet.LastRowNum > 2 Then
                           
                            Tabla = New DataTable(first_sheet_name)
                            Tabla.Rows.Clear()
                            Tabla.Columns.Clear()

                            For rowIndex As Integer = 0 To worksheet.LastRowNum

                                Dim NewReg As DataRow = Nothing
                                Dim row As IRow = worksheet.GetRow(rowIndex)
                                Dim row2 As IRow = Nothing
                                Dim row3 As IRow = Nothing

                                If rowIndex = 0 Then
                                    row2 = worksheet.GetRow(rowIndex + 2)
                                    row3 = worksheet.GetRow(rowIndex + 3)
                                End If
                                '    InsertIntoTblWebEventLog("TestExcel1", rowIndex.ToString, worksheet.LastRowNum.ToString)

                                If row IsNot Nothing Then
                                    If rowIndex > 1 Then NewReg = Tabla.NewRow()
                                    Dim colIndex As Integer = 0

                                    For Each cell As ICell In row.Cells
                                        Dim valorCell As Object = Nothing
                                        Dim cellType As String = ""
                                        Dim cellType2 As String() = New String(1) {}
                                        If rowIndex = 1 Then

                                        ElseIf rowIndex = 0 Then

                                            For i As Integer = 0 To 2 - 1
                                                Dim cell2 As ICell = Nothing

                                                If i = 0 Then
                                                    cell2 = row2.GetCell(cell.ColumnIndex)
                                                Else
                                                    cell2 = row3.GetCell(cell.ColumnIndex)
                                                End If
                                                '   InsertIntoTblWebEventLog("TestExcel5", cell.ColumnIndex.ToString, i.ToString)

                                                If cell2 IsNot Nothing Then

                                                    Select Case cell2.CellType
                                                        Case NPOI.SS.UserModel.CellType.Blank
                                                            'cellType2(i) = "System.String"
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
                                                '  InsertIntoTblWebEventLog("TestExcel4", "", cellType2(i).ToString)

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
                                            '   InsertIntoTblWebEventLog("TestExcel3", colName, cellType)

                                            Dim codigo As DataColumn = New DataColumn(colName, System.Type.[GetType](cellType))
                                            Tabla.Columns.Add(codigo)
                                            colIndex += 1
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
                        Else
                            lblMessage.Text = "Please import more than one Account details to proceed."
                        End If
                    Else
                        lblMessage.Text = "Sheet Name does not match with the selected template."
                    End If

                End Using
            Else
                Throw New Exception("ERROR 404")
            End If

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            InsertIntoTblWebEventLog("Excel_To_DataTable", ex.Message.ToString, txtCreatedBy.Text)
            Throw ex

        End Try

        Return Tabla
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        If FileUpload1.HasFile Then
            FileUpload1 = Nothing

        End If
        btnExportToExcel.Visible = False

    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        lblAlert.Text = ""
        lblMessage.Text = ""

        Dim ofilename As String = ""
        Dim sfilename As String = ""

        ofilename = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim ext As String = Path.GetExtension(ofilename)
        sfilename = ofilename.Split("."c)(0)

        Dim folderPath As String = Server.MapPath("~/Uploads/Excel/")
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
        If dt Is Nothing Then
            lblAlert.Text = "DATA NOT IMPORTED"
        Else
            Dim res As Boolean = True


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim count As String = dt.Rows.Count.ToString

            If rdbModule.SelectedValue.ToString = "Corporate" Then
                InsertCorporateData(dt, conn)
            ElseIf rdbModule.SelectedValue.ToString = "Residential" Then
                InsertResidentialData(dt, conn)
            ElseIf rdbModule.SelectedValue.ToString = "CorporateLocation" Then
                InsertCorporateLocationData(dt, conn)
            ElseIf rdbModule.SelectedValue.ToString = "ResidentialLocation" Then
                InsertResidentialLocationData(dt, conn)
            End If
            lblMessage.Text = "Total Records Imported : " + count + "<br>" + " Success : " + txtSuccessCount.Text + ", Failure : " + txtFailureCount.Text '+ " Failed AccountID : " + txtFailureString.Text
            If GridView1.Rows.Count > 0 Then
                btnExportToExcel.Visible = True

            End If

            
            conn.Close()
            conn.Dispose()

        End If

        'dt.Clear()
        'dt.Dispose()
        ' Session("Filename") = fileName
    End Sub

    Protected Function InsertCorporateLocationData(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable


        ' Try
        Dim qry As String = "INSERT INTO tblcompanylocation(CompanyID,Location,BranchID,Description,ContactPerson,Address1,Telephone,"
        qry = qry + "Mobile,Email,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,"
        qry = qry + "AddStreet,AddCity,AddState,AddCountry,AddPostal,LocateGrp,Fax,AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,"
        qry = qry + "Contact1Position,Telephone2,ContactPerson2,Contact2Position,Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,"
        qry = qry + "Contact2Email,ServiceAddress, ServiceLocationGroup,BillingNameSvc,BillAddressSvc,BillStreetSvc,BillBuildingSvc,"
        qry = qry + "BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,BillPosition1Svc,BillTelephone1Svc,BillFax1Svc,"
        qry = qry + "Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,BillFax2Svc,"
        qry = qry + "Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc, InChargeIdSvc, ArTermSvc, SalesmanSvc, SendServiceReportTo1, SendServiceReportTo2,"
        qry = qry + " Industry, MarketSegmentId, ContractGroup, Comments, CompanyGroupD, InActiveD, DefaultInvoiceFormat, ServiceEmailCC,"
        qry = qry + " EmailServiceNotificationOnly, SmartCustomer, BusinessHoursStart, BusinessHoursEnd, ExcludePIRDataDuringBusinessHours)VALUES"
        qry = qry + "(@CompanyID,@Location,@BranchID,@Description,@ContactPerson,@Address1,@Telephone,@Mobile,@Email,@CreatedBy,@CreatedOn,"
        qry = qry + "@LastModifiedBy,@LastModifiedOn,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddCity,@AddState,@AddCountry,"
        qry = qry + "@AddPostal,@LocateGrp,@Fax,@AccountID,@LocationID,@LocationPrefix,@LocationNo,@ServiceName,@Contact1Position,@Telephone2,"
        qry = qry + "@ContactPerson2,@Contact2Position,@Contact2Tel,@Contact2Fax,@Contact2Tel2,@Contact2Mobile,@Contact2Email,@ServiceAddress,"
        qry = qry + " @ServiceLocationGroup,@BillingNameSvc,@BillAddressSvc,@BillStreetSvc,@BillBuildingSvc,@BillCitySvc,@BillStateSvc,"
        qry = qry + "@BillCountrySvc,@BillPostalSvc,@BillContact1Svc,@BillPosition1Svc,@BillTelephone1Svc,@BillFax1Svc,@Billtelephone12Svc,"
        qry = qry + "@BillMobile1Svc,@BillEmail1Svc,@BillContact2Svc,@BillPosition2Svc,@BillTelephone2Svc,@BillFax2Svc,@Billtelephone22Svc,"
        qry = qry + "@BillMobile2Svc,@BillEmail2Svc, @InChargeIdSvc, @ArTermSvc, @SalesmanSvc, @SendServiceReportTo1, @SendServiceReportTo2,"
        qry = qry + " @Industry, @MarketSegmentId, @ContractGroup, @Comments, @CompanyGroupD, @InActiveD,  @DefaultInvoiceFormat,"
        qry = qry + " @ServiceEmailCC, @EmailServiceNotificationOnly, @SmartCustomer, @BusinessHoursStart, @BusinessHoursEnd, @ExcludePIRDataDuringBusinessHours);"

        Dim AccountID As String = ""
        Dim LocationID As String = ""
        Dim Exists As Boolean = True


        InsertIntoTblWebEventLog("InsertData", dt.Rows.Count.ToString, "")

        Dim drow As DataRow
        Dim dc1 As DataColumn = New DataColumn("AccountID", GetType(String))
        Dim dc5 As DataColumn = New DataColumn("LocationID", GetType(String))
        Dim dc2 As DataColumn = New DataColumn("Name", GetType(String))
        Dim dc3 As DataColumn = New DataColumn("Status", GetType(String))
        Dim dc4 As DataColumn = New DataColumn("Remarks", GetType(String))
        dtLog.Columns.Add(dc1)
        dtLog.Columns.Add(dc5)
        dtLog.Columns.Add(dc2)
        dtLog.Columns.Add(dc3)
        dtLog.Columns.Add(dc4)


        For Each r As DataRow In dt.Rows

            drow = dtLog.NewRow()

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CHECK IF ACCOUNTID FIELD IS BLANK
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If IsDBNull(r("AccountID")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "ACCOUNTID_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                '  InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("AccountID"))

                AccountID = r("AccountID")
                drow("AccountID") = AccountID

            End If

            ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''CHECK IF LOCATIONID FIELD IS BLANK
            ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'If IsDBNull(r("LocationID")) Then
            '    Failure = Failure + 1
            '    FailureString = FailureString + " " + LocationID + "(LOCATIONID_BLANK)"
            '    drow("Status") = "Failed"
            '    drow("Remarks") = "LOCATIONID_BLANK"
            '    dtLog.Rows.Add(drow)
            '    Continue For
            'Else
            '    InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("LocationID"))

            '    LocationID = r("LocationID")
            '    drow("LocationID") = LocationID

            'End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CHECK IF SERVICENAME FIELD IS BLANK
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If IsDBNull(r("ServiceName")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + LocationID + "(SERVICENAME_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "SERVICENAME_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                drow("Name") = r("ServiceName")
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CHECK IF ACCOUNTID FIELD EXISTS - exit  if IT DOES NOT exists
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT * FROM tblcompany where accountid=@id"
            command2.Parameters.AddWithValue("@id", AccountID)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count = 0 Then

                Failure = Failure + 1

                FailureString = FailureString + " " + AccountID + "(DOES NOT EXIST)"
                drow("Status") = "Failed"
                drow("Remarks") = "ACCOUNTID_DOES NOT EXIST"
                dtLog.Rows.Add(drow)
                Continue For

            Else
                ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''CHECK IF SERVICE LOCATION ALREADY EXISTS - EXIT IF EXISTS
                ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim addr As String = ""
                If IsDBNull(r("Address1")) = False Then
                    addr = r("Address1").ToString.Trim + " "
                Else
                    addr = " "
                End If
                If IsDBNull(r("AddStreet")) = False Then
                    addr = addr + r("AddStreet").ToString.Trim + " "
                Else
                    addr = addr + " "
                End If
                If IsDBNull(r("AddBuilding")) = False Then
                    addr = addr + r("AddBuilding").ToString.Trim
                Else
                    addr = addr + " "
                End If
                ' addr = addr.Trim

                Dim ContractGroup As String = ""
                If IsDBNull(r("ContractGroup")) = False Then
                    ContractGroup = r("ContractGroup").ToString.Trim
                End If

                Dim CompanyGroup As String = ""
                If IsDBNull(r("CompanyGroupD")) = False Then
                    CompanyGroup = r("CompanyGroupD").ToString.Trim
                End If

                If CheckCorporateLocationDuplicate(conn, AccountID, addr, ContractGroup, CompanyGroup) = False Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(DUPLICATE)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "SERVICE LOCATION_DUPLICATE"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If
           
                '''''''''''''''''''''''''''''''''''''''''''''''''''
                'OTHER VALIDATIONS
                '''''''''''''''''''''''''''''''''''''''''''''''''''

                ''Check for dropdownlist values, if it exists

                If IsDBNull(r("CompanyGroupD")) = False Then
                    If CheckCompanyGroupExists(r("CompanyGroupD"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CompanyGroup DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "COMPANYGROUP DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                Else
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(COMPANYGROUP_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "COMPANYGROUP_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If

                If IsDBNull(r("ContractGroup")) = False Then
                    If CheckContractGroupExists(r("ContractGroup"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CONTRACTGROUP DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CONTRACTGROUP DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                Else
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(CONTRACTGROUP_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "CONTRACTGROUP_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If

                If IsDBNull(r("AddCity")) = False Then
                    If CheckCityExists(r("AddCity"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CITY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CITY DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AddState")) = False Then
                    If CheckStateExists(r("AddState"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(STATE DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "STATE DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AddCountry")) = False Then
                    If CheckCountryExists(r("AddCountry"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(COUNTRY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "COUNTRY DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("Zone")) = False Then
                    If CheckLocationGroupExists(r("Zone"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(ZONE DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ZONE DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If


                If IsDBNull(r("Industry")) = False Then
                    If CheckIndustryExists(r("Industry"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(INDUSTRY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INDUSTRY DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If

                Else
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(INDUSTRY_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "INDUSTRY_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If

                If IsDBNull(r("Salesman")) = False Then
                    If CheckSalesmanExists(r("Salesman"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(SALESMAN DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "SALESMAN DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If

                Else
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(SALESMAN_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "SALESMAN_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If


                If IsDBNull(r("InchargeID")) = False Then
                    If CheckInchargeIDExists(r("InchargeID"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(InchargeID DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INCHARGEID DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("CreditTerms")) = False Then
                    If CheckTermsExists(r("CreditTerms"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CREDIT TERMS DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CREDIT TERMS DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("DefaultInvoiceFormat")) = False Then
                    If CheckInvoiceFormatExists(r("DefaultInvoiceFormat"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(INVOICE FORMAT DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INVOICE FORMAT DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If

                End If


                Dim cmd As MySqlCommand = conn.CreateCommand()
                '  Dim cmd As MySqlCommand = New MySqlCommand

                cmd.CommandType = CommandType.Text
                cmd.CommandText = qry
                cmd.Parameters.Clear()

                If IsDBNull(r("AccountID")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ACCOUNTID_BLANK"
                    If IsDBNull(r("ServiceName")) = False Then
                        drow("Name") = r("ServiceName")
                    End If
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@AccountID", r("AccountID").ToString.ToUpper)
                End If

                'If IsDBNull(r("Name")) Then
                '    Failure = Failure + 1
                '    FailureString = FailureString + " " + AccountID + "(NAME_BLANK)"
                '    drow("Status") = "Failed"
                '    drow("Remarks") = "NAME_BLANK"
                '    dtLog.Rows.Add(drow)
                '    Continue For
                'Else
                '    drow("Name") = r("Name")
                '    cmd.Parameters.AddWithValue("@Name", r("Name").ToString.ToUpper)
                'End If




                cmd.Parameters.AddWithValue("@InActiveD", False)

                If IsDBNull(r("ClientID")) Then
                    cmd.Parameters.AddWithValue("@CompanyID", "")
                Else
                    cmd.Parameters.AddWithValue("@CompanyID", r("ClientID").ToString.ToUpper)
                End If

                If IsDBNull(r("Location")) Then
                    cmd.Parameters.AddWithValue("@Location", "")
                Else
                    cmd.Parameters.AddWithValue("@Location", r("Location").ToString.ToUpper)
                End If

                If IsDBNull(r("ServiceLocationGroup")) Then
                    cmd.Parameters.AddWithValue("@ServiceLocationGroup", "")
                Else
                    cmd.Parameters.AddWithValue("@ServiceLocationGroup", r("ServiceLocationGroup").ToString.ToUpper)
                End If

                cmd.Parameters.AddWithValue("@CompanyGroupD", r("CompanyGroupD").ToString.ToUpper)
                cmd.Parameters.AddWithValue("@ContractGroup", r("ContractGroup").ToString.ToUpper)
                cmd.Parameters.AddWithValue("@ServiceName", r("ServiceName").ToString.ToUpper)

                If IsDBNull(r("Comments")) Then
                    cmd.Parameters.AddWithValue("@Comments", "")
                Else
                    cmd.Parameters.AddWithValue("@Comments", r("Comments").ToString.ToUpper)
                End If
                If IsDBNull(r("Description")) Then
                    cmd.Parameters.AddWithValue("@Description", "")
                Else
                    cmd.Parameters.AddWithValue("@Description", r("Description").ToString.ToUpper)
                End If

                If IsDBNull(r("Address1")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(STREET ADDRESS1_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "STREET ADDRESS1 BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@Address1", r("Address1").ToString.ToUpper)
                End If
                If IsDBNull(r("AddStreet")) Then
                    cmd.Parameters.AddWithValue("@AddStreet", "")
                Else
                    cmd.Parameters.AddWithValue("@AddStreet", r("AddStreet").ToString.ToUpper)
                End If
                If IsDBNull(r("AddBuilding")) Then
                    cmd.Parameters.AddWithValue("@AddBuilding", "")
                Else
                    cmd.Parameters.AddWithValue("@AddBuilding", r("AddBuilding").ToString.ToUpper)
                End If
                If IsDBNull(r("AddCity")) Then
                    cmd.Parameters.AddWithValue("@AddCity", "")
                Else
                    cmd.Parameters.AddWithValue("@AddCity", r("AddCity").ToString.ToUpper)
                End If

                If IsDBNull(r("AddState")) Then

                    cmd.Parameters.AddWithValue("@AddState", "")
                Else
                    cmd.Parameters.AddWithValue("@AddState", r("AddState").ToString.ToUpper)

                End If

                If IsDBNull(r("AddCountry")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(SERVICEADDRESS-COUNTRY_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "SERVICEADDRESS-COUNTRY_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@AddCountry", r("AddCountry").ToString.ToUpper)
                End If

                If IsDBNull(r("AddPostal")) Then
                    cmd.Parameters.AddWithValue("@AddPostal", "")
                Else
                    cmd.Parameters.AddWithValue("@AddPostal", r("AddPostal").ToString.ToUpper)
                End If
                If IsDBNull(r("Zone")) Then
                    cmd.Parameters.AddWithValue("@LocateGRP", "")

                Else
                    cmd.Parameters.AddWithValue("@LocateGRP", r("Zone").ToString.ToUpper)

                End If

                cmd.Parameters.AddWithValue("@Industry", r("Industry").ToString.ToUpper)

                If IsDBNull(r("MarketSegmentID")) Then
                    cmd.Parameters.AddWithValue("@MarketSegmentID", "")
                Else
                    cmd.Parameters.AddWithValue("@MarketSegmentID", r("MarketSegmentID").ToString.ToUpper)
                End If
                If IsDBNull(r("InChargeID")) Then
                    cmd.Parameters.AddWithValue("@InChargeIDSvc", "")
                Else
                    cmd.Parameters.AddWithValue("@InChargeIDSvc", r("InChargeID").ToString.ToUpper)
                End If
                cmd.Parameters.AddWithValue("@SalesManSvc", r("SalesMan").ToString.ToUpper)

                If IsDBNull(r("CreditTerms")) Then
                    cmd.Parameters.AddWithValue("@ARTermSvc", "")
                Else
                    cmd.Parameters.AddWithValue("@ARTermSvc", r("CreditTerms").ToString.ToUpper)
                End If

                If IsDBNull(r("ContactPerson")) Then
                    cmd.Parameters.AddWithValue("@ContactPerson", "")
                Else
                    cmd.Parameters.AddWithValue("@ContactPerson", r("ContactPerson").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact1Position")) Then
                    cmd.Parameters.AddWithValue("@Contact1Position", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact1Position", r("Contact1Position").ToString.ToUpper)
                End If

                If IsDBNull(r("Telephone")) Then
                    cmd.Parameters.AddWithValue("@Telephone", "")
                Else
                    cmd.Parameters.AddWithValue("@Telephone", r("Telephone").ToString.ToUpper)
                End If

                If IsDBNull(r("Fax")) Then
                    cmd.Parameters.AddWithValue("@Fax", "")
                Else
                    cmd.Parameters.AddWithValue("@Fax", r("Fax").ToString.ToUpper)
                End If

                If IsDBNull(r("Telephone2")) Then
                    cmd.Parameters.AddWithValue("@Telephone2", "")
                Else
                    cmd.Parameters.AddWithValue("@Telephone2", r("Telephone2").ToString.ToUpper)
                End If

                If IsDBNull(r("Mobile")) Then
                    cmd.Parameters.AddWithValue("@Mobile", "")
                Else
                    cmd.Parameters.AddWithValue("@Mobile", r("Mobile").ToString.ToUpper)
                End If

                If IsDBNull(r("Email")) Then
                    cmd.Parameters.AddWithValue("@Email", "")
                Else
                    cmd.Parameters.AddWithValue("@Email", r("Email").ToString.ToUpper)
                End If

                If IsDBNull(r("ContactPerson2")) Then
                    cmd.Parameters.AddWithValue("@ContactPerson2", "")
                Else
                    cmd.Parameters.AddWithValue("@ContactPerson2", r("ContactPerson2").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Position")) Then
                    cmd.Parameters.AddWithValue("@Contact2Position", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Position", r("Contact2Position").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Tel")) Then
                    cmd.Parameters.AddWithValue("@Contact2Tel", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Tel", r("Contact2Tel").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Fax")) Then
                    cmd.Parameters.AddWithValue("@Contact2Fax", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Fax", r("Contact2Fax").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Tel2")) Then
                    cmd.Parameters.AddWithValue("@Contact2Tel2", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Tel2", r("Contact2Tel2").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Mobile")) Then
                    cmd.Parameters.AddWithValue("@Contact2Mobile", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Mobile", r("Contact2Mobile").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Email")) Then
                    cmd.Parameters.AddWithValue("@Contact2Email", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Email", r("Contact2Email").ToString.ToUpper)
                End If

                If IsDBNull(r("ServiceEmailCC")) Then
                    cmd.Parameters.AddWithValue("@ServiceEmailCC", "")
                Else
                    cmd.Parameters.AddWithValue("@ServiceEmailCC", r("ServiceEmailCC").ToString.ToUpper)
                End If

                If IsDBNull(r("EmailServiceNotificationOnly")) Then
                    cmd.Parameters.AddWithValue("@EmailServiceNotificationOnly", 0)
                ElseIf r("EmailServiceNotificationOnly").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@EmailServiceNotificationOnly", 1)
                ElseIf r("EmailServiceNotificationOnly").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@EmailServiceNotificationOnly", 0)
                Else
                    cmd.Parameters.AddWithValue("@EmailServiceNotificationOnly", 0)
                End If

                If IsDBNull(r("BillingNameSvc")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(BILLINGNAME_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "BILLINGNAME_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@BillingNameSvc", r("BillingNameSvc").ToString.ToUpper)
                End If

                Dim CheckServiceAddress As Boolean = False

                cmd.Parameters.AddWithValue("@ServiceAddress", 0)

                If IsDBNull(r("ServiceAddress")) Then
                    ' cmd.Parameters.AddWithValue("@ServiceAddress", 0)
                    CheckServiceAddress = False
                ElseIf r("ServiceAddress").ToString.ToUpper = "TRUE" Then
                    '  cmd.Parameters.AddWithValue("@ServiceAddress", 1)
                    CheckServiceAddress = True
                ElseIf r("ServiceAddress").ToString.ToUpper = "FALSE" Then
                    ' cmd.Parameters.AddWithValue("@ServiceAddress", 0)
                    CheckServiceAddress = False
                Else
                    ' cmd.Parameters.AddWithValue("@ServiceAddress", 0)
                    CheckServiceAddress = False
                End If


                'If Billing Address is same as Service Address - set to True, then copy all Service Contact Info to Billing Contact Info


                If CheckServiceAddress = True Then

                    If IsDBNull(r("BillAddressSvc")) Then
                        If IsDBNull(r("Address1")) Then
                            cmd.Parameters.AddWithValue("@BillAddressSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillAddressSvc", r("Address1").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillAddressSvc", r("BillAddressSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillStreetSvc")) Then
                        If IsDBNull(r("AddStreet")) Then
                            cmd.Parameters.AddWithValue("@BillStreetSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillStreetSvc", r("AddStreet").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillStreetSvc", r("BillStreetSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillbuildingSvc")) Then
                        If IsDBNull(r("AddBuilding")) Then
                            cmd.Parameters.AddWithValue("@BillBuildingSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillBuildingSvc", r("AddBuilding").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillbuildingSvc", r("BillbuildingSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCitySvc")) Then
                        If IsDBNull(r("AddCity")) Then
                            cmd.Parameters.AddWithValue("@BillCitySvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillCitySvc", r("AddCity").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillCitySvc", r("BillCitySvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillStateSvc")) Then
                        If IsDBNull(r("AddState")) Then
                            cmd.Parameters.AddWithValue("@BillStateSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillStateSvc", r("AddState").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillStateSvc", r("BillStateSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCountrySvc")) Then
                        If IsDBNull(r("AddCountry")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS-COUNTRY_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLINGADDRESS-COUNTRY_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillCountrySvc", r("AddCountry").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillCountrySvc", r("BillCountrySvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPostalSvc")) Then
                        If IsDBNull(r("AddPostal")) Then
                            cmd.Parameters.AddWithValue("@BillPostalSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPostalSvc", r("AddPostal").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillPostalSvc", r("BillPostalSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact1Svc")) Then

                        If IsDBNull(r("ContactPerson")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Svc", r("ContactPerson").ToString.ToUpper)
                        End If

                    Else
                        cmd.Parameters.AddWithValue("@BillContact1Svc", r("BillContact1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPosition1Svc")) Then
                        If IsDBNull(r("Contact1Position")) Then
                            cmd.Parameters.AddWithValue("@BillPosition1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPosition1Svc", r("Contact1Position").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillPosition1Svc", r("BillContact1Position").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone1Svc")) Then
                        If IsDBNull(r("Telephone")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone1Svc", r("Telephone").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone1Svc", r("BillTelephone1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax1Svc")) Then
                        If IsDBNull(r("Fax")) Then
                            cmd.Parameters.AddWithValue("@BillFax1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillFax1Svc", r("Fax").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillFax1Svc", r("BillFax1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone12Svc")) Then
                        If IsDBNull(r("Telephone2")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone12Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone12Svc", r("Telephone2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone12Svc", r("BillTelephone12Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillMobile1Svc")) Then
                        If IsDBNull(r("Mobile")) Then
                            cmd.Parameters.AddWithValue("@BillMobile1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillMobile1Svc", r("Mobile").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillMobile1Svc", r("BillMobile1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillEmail1Svc")) Then
                        If IsDBNull(r("Email")) Then
                            cmd.Parameters.AddWithValue("@BillEmail1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillEmail1Svc", r("Email").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail1Svc", r("BillEmail1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact2Svc")) Then
                        If IsDBNull(r("ContactPerson2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Svc", r("ContactPerson2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Svc", r("BillContact2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPosition2Svc")) Then
                        If IsDBNull(r("Contact2Position")) Then
                            cmd.Parameters.AddWithValue("@BillPosition2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPosition2Svc", r("Contact2Position").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillPosition2Svc", r("BillContact1Position").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone2Svc")) Then
                        If IsDBNull(r("Contact2Tel")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone2Svc", r("Contact2Tel").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone2Svc", r("BillTelephone2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax2Svc")) Then
                        If IsDBNull(r("Contact2Fax")) Then
                            cmd.Parameters.AddWithValue("@BillFax2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillFax2Svc", r("Contact2Fax").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillFax2Svc", r("BillFax2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone22Svc")) Then
                        If IsDBNull(r("Contact2Tel2")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone22Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone22Svc", r("Contact2Tel2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone22Svc", r("BillTelephone22Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillMobile2Svc")) Then
                        If IsDBNull(r("Contact2Mobile")) Then
                            cmd.Parameters.AddWithValue("@BillMobile2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillMobile2Svc", r("Contact2Mobile").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillMobile2Svc", r("BillMobile2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillEmail2Svc")) Then
                        If IsDBNull(r("Contact2Email")) Then
                            cmd.Parameters.AddWithValue("@BillEmail2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillEmail2Svc", r("Contact2Email").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail2Svc", r("BillEmail2Svc").ToString.ToUpper)
                    End If

                Else
                    If IsDBNull(r("BillAddressSvc")) Then
                        cmd.Parameters.AddWithValue("@BillAddressSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillAddressSvc", r("BillAddressSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillStreetSvc")) Then
                        cmd.Parameters.AddWithValue("@BillStreetSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillStreetSvc", r("BillStreetSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillbuildingSvc")) Then
                        cmd.Parameters.AddWithValue("@BillbuildingSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillbuildingSvc", r("BillbuildingSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCitySvc")) Then
                        cmd.Parameters.AddWithValue("@BillCitySvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillCitySvc", r("BillCitySvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillStateSvc")) Then
                        cmd.Parameters.AddWithValue("@BillStateSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillStateSvc", r("BillStateSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCountrySvc")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS-COUNTRY_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLINGADDRESS-COUNTRY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillCountrySvc", r("BillCountrySvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPostalSvc")) Then
                        cmd.Parameters.AddWithValue("@BillPostalSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillPostalSvc", r("BillPostalSvc").ToString.ToUpper)
                    End If


                    If IsDBNull(r("BillContact1Svc")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillContact1Svc", r("BillContact1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPosition1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillPosition1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillPosition1Svc", r("BillPosition1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillTelephone1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone1Svc", r("BillTelephone1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillFax1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillFax1Svc", r("BillFax1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone12Svc")) Then
                        cmd.Parameters.AddWithValue("@BillTelephone12Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone12Svc", r("BillTelephone12Svc").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillMobile1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillMobile1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillMobile1Svc", r("BillMobile1Svc").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillEmail1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillEmail1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail1Svc", r("BillEmail1Svc").ToString.ToUpper)
                    End If


                    If IsDBNull(r("BillContact2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Svc", r("BillContact2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPosition2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillPosition2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillPosition2Svc", r("BillPosition2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillTelephone2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone2Svc", r("BillTelephone2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillFax2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillFax2Svc", r("BillFax2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone22Svc")) Then
                        cmd.Parameters.AddWithValue("@BillTelephone22Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone22Svc", r("BillTelephone22Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillMobile2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillMobile2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillMobile2Svc", r("BillMobile2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillEmail2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillEmail2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail2Svc", r("BillEmail2Svc").ToString.ToUpper)
                    End If

                End If

                If IsDBNull(r("SendServiceReportTo1")) Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo1", "N")
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo1", "Y")
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo1", "N")
                Else
                    cmd.Parameters.AddWithValue("@SendServiceReportTo1", "N")
                End If

                If IsDBNull(r("SendServiceReportTo2")) Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo2", "N")
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo2", "Y")
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo2", "N")
                Else
                    cmd.Parameters.AddWithValue("@SendServiceReportTo2", "N")
                End If

                If IsDBNull(r("DefaultInvoiceFormat")) Then
                    cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", "Format1")
                Else
                    cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", r("DefaultInvoiceFormat"))
                End If

                If IsDBNull(r("SmartCustomer")) Then
                    cmd.Parameters.AddWithValue("@SmartCustomer", 0)
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@SmartCustomer", 1)
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@SmartCustomer", 0)
                Else
                    cmd.Parameters.AddWithValue("@SmartCustomer", 0)
                End If

                If IsDBNull(r("BusinessHoursStart")) Then
                    cmd.Parameters.AddWithValue("@BusinessHoursStart", "")

                Else
                    cmd.Parameters.AddWithValue("@BusinessHoursStart", r("BusinessHoursStart").ToString.ToUpper)

                End If

                If IsDBNull(r("BusinessHoursEnd")) Then
                    cmd.Parameters.AddWithValue("@BusinessHoursEnd", "")

                Else
                    cmd.Parameters.AddWithValue("@BusinessHoursEnd", r("BusinessHoursEnd").ToString.ToUpper)

                End If

                If IsDBNull(r("ExcludePIRDataDuringBusinessHours")) Then
                    cmd.Parameters.AddWithValue("@ExcludePIRDataDuringBusinessHours", 0)
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@ExcludePIRDataDuringBusinessHours", 1)
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@ExcludePIRDataDuringBusinessHours", 0)
                Else
                    cmd.Parameters.AddWithValue("@ExcludePIRDataDuringBusinessHours", 0)
                End If
                cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text + "_IMPORT")
                cmd.Parameters.AddWithValue("@LastModifiedBy", txtCreatedBy.Text + "_IMPORT")

                'If IsDBNull(r("CreatedBy")) Then
                '    cmd.Parameters.AddWithValue("@CreatedBy", "EXCELIMPORT")

                'Else
                '    cmd.Parameters.AddWithValue("@CreatedBy", r("CreatedBy"))

                'End If

                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                 'If IsDBNull(r("CreatedBy")) Then
                '    cmd.Parameters.AddWithValue("@LastModifiedBy", "EXCELIMPORT")

                'Else
                '    cmd.Parameters.AddWithValue("@LastModifiedBy", r("CreatedBy"))

                'End If
                cmd.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))



                cmd.Parameters.AddWithValue("@AddBlock", "")
                cmd.Parameters.AddWithValue("@AddNos", "")
                cmd.Parameters.AddWithValue("@AddFloor", "")
                cmd.Parameters.AddWithValue("@AddUnit", "")
                cmd.Parameters.AddWithValue("@LocationPrefix", "")

                cmd.Parameters.AddWithValue("@BranchID", "")

                Dim lastnum As Int32 = GenerateLocationID(AccountID, conn)
                If lastnum = 0 Then
                    drow("Status") = "Failed"
                    drow("Remarks") = "CANNOT GENERATE LOCATIONID"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If
                LocationID = AccountID + "-" + lastnum.ToString("D4")
                '  lblAlert.Text = LocationID

                drow("LocationID") = LocationID

                cmd.Parameters.AddWithValue("@LocationNo", lastnum)

                cmd.Parameters.AddWithValue("@LocationID", LocationID.ToUpper)

                cmd.Connection = conn

                cmd.ExecuteNonQuery()
                '    cmd.Dispose()

                Success = Success + 1
                drow("Status") = "Success"
                drow("Remarks") = ""
                dtLog.Rows.Add(drow)
            End If

        Next



        txtSuccessCount.Text = Success.ToString
        txtFailureCount.Text = Failure.ToString
        txtFailureString.Text = FailureString

        GridView1.DataSource = dtLog
        GridView1.DataBind()

        dt.Clear()


        Return True
        'Catch ex As Exception
        '    txtSuccessCount.Text = Success.ToString
        '    txtFailureCount.Text = Failure.ToString
        '    txtFailureString.Text = FailureString
        '    lblAlert.Text = ex.Message.ToString


        '    InsertIntoTblWebEventLog("InsertCorporateLocationData", ex.Message.ToString, txtCreatedBy.Text)

        '    Return False

        'End Try
    End Function

    Protected Function InsertCorporateData(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable


        Try

            Dim qry As String = "INSERT INTO tblcompany(Id,Name,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddPostal,AddState,AddCity,AddCountry,Telephone,Fax,BillingAddress,"
            qry = qry + "BillBlock,BillNos,BillFloor,BillUnit,BillBuilding,BillStreet,BillPostal,BillState,BillCity,BillCountry,ContactPerson,Comments,RocNos,RocRegDate,AuthorizedCapital,"
            qry = qry + "PaidupCapital,CompanyType,Industry,FinanceCompanyId,FinanceCompany,ArLimit,ApLimit,SalesLimit,PurchaseLimit,ApCurrency,ArCurrency,SendStatement,GstRegistered,GstNos,Status,"
            qry = qry + "Address1,BillAddress1,ContactPersonEmail,Website,Source,ARBal,APBal,Sales,Purchase,LocateGRP,SalesGRP,Dealer,LoginID,Email,Password,WebLevel,ARTERM,APTERM,PriceGroup,InChargeID,"
            qry = qry + "Age0,Age30,Age60,Age90,Age120,SalesMan,StopSalesYN,StopPurchYN,SpecCode,ArWarning,StartDate,LicenseNumber,LicenseInfo,SalesGST,ArMethod,ApMethod,ProductM1,ProductM2,ProductM3,ProductM4,"
            qry = qry + "ProductF1,ProductF2,ProductF3,ProductF4,RentalTerm,CompanyGroup,Donor,Member,MemberType,MemberID,GIROID,DateJoin,DateExpired,DateTerminate,TemplateNo,ARLedger,ARSubLedger,APLedger,APSubLedger,"
            qry = qry + "SrcCompID,DiscountPct,PreferredCustYN,ChkGstInclusive,Reason,Boardmember,BoardDesignation,period,Intriducer,Organization,chkLetterIndemnity,LetterIndemnitySignedBy,LeterDate,CreatedBy,CreatedOn,"
            qry = qry + "LastModifiedBy,LastModifiedOn,BillTelephone,BillFax,Name2,WebLoginID,WebLoginPassWord,WebAccessLevel,WebOneTimePassWord,BillContactPerson,WebGroupDealer,WebDisable,WebID,OTPMobile,OTPYN,OTPGenerateDate,"
            qry = qry + "HideInStock,OverdueDaysLimit,OverdueDaysLimitActive,OverdueDaysWarning,OverdueDaysWarningActive,chkAR,DueDaysStopFreq,SubCompanyNo,SourceCompany,chkSendServiceReport,Telephone2,BillTelephone2,Mobile,BillMobile,"
            qry = qry + "SoPriceGroup,POPrefix,PONumber,LastStatus,OverdueMonthWarning,OverDueMonthLimit,AccountNo,FlowFrom,FlowTo,InActive,ShippingTerm,InterCompany,AutoEmailServ,ReportFormatServ,WebUploadDate,IsCustomer,IsSupplier,PaxBased,"
            qry = qry + "BillMonthly,DiscType,ARPDFFromat,EmailConsolidate,AccountID,OffContact1Position,OffContact1Tel,OffContact1Fax,OffContact1Tel2,OffContact1Mobile,BillContact1Position,BillContact2,BillContact2Position,"
            qry = qry + "BillContact1Email,BillContact2Email,BillContact2Tel,BillContact2Fax,BillContact2Tel2,BillContact2Mobile,OffContact1,OffContactPosition,BillingSettings,BillingName,TermsDay,Location, DefaultInvoiceFormat, AutoEmailInvoice,AutoEmailSOA)VALUES(@Id,@Name,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddPostal,@AddState,@AddCity,@AddCountry,@Telephone,@Fax,@BillingAddress,@BillBlock,@BillNos,"
            qry = qry + "@BillFloor,@BillUnit,@BillBuilding,@BillStreet,@BillPostal,@BillState,@BillCity,@BillCountry,@ContactPerson,@Comments,@RocNos,@RocRegDate,@AuthorizedCapital,@PaidupCapital,@CompanyType,@Industry,@FinanceCompanyId,@FinanceCompany,"
            qry = qry + "@ArLimit,@ApLimit,@SalesLimit,@PurchaseLimit,@ApCurrency,@ArCurrency,@SendStatement,@GstRegistered,@GstNos,@Status,@Address1,@BillAddress1,@ContactPersonEmail,@Website,@Source,@ARBal,@APBal,@Sales,@Purchase,@LocateGRP,@SalesGRP,@Dealer,"
            qry = qry + "@LoginID,@Email,@Password,@WebLevel,@ARTERM,@APTERM,@PriceGroup,@InChargeID,@Age0,@Age30,@Age60,@Age90,@Age120,@SalesMan,@StopSalesYN,@StopPurchYN,@SpecCode,@ArWarning,@StartDate,@LicenseNumber,@LicenseInfo,@SalesGST,@ArMethod,@ApMethod,"
            qry = qry + "@ProductM1,@ProductM2,@ProductM3,@ProductM4,@ProductF1,@ProductF2,@ProductF3,@ProductF4,@RentalTerm,@CompanyGroup,@Donor,@Member,@MemberType,@MemberID,@GIROID,@DateJoin,@DateExpired,@DateTerminate,@TemplateNo,@ARLedger,@ARSubLedger,"
            qry = qry + "@APLedger,@APSubLedger,@SrcCompID,@DiscountPct,@PreferredCustYN,@ChkGstInclusive,@Reason,@Boardmember,@BoardDesignation,@period,@Intriducer,@Organization,@chkLetterIndemnity,@LetterIndemnitySignedBy,@LeterDate,@CreatedBy,@CreatedOn,"
            qry = qry + "@LastModifiedBy,@LastModifiedOn,@BillTelephone,@BillFax,@Name2,@WebLoginID,@WebLoginPassWord,@WebAccessLevel,@WebOneTimePassWord,@BillContactPerson,@WebGroupDealer,@WebDisable,@WebID,@OTPMobile,@OTPYN,@OTPGenerateDate,@HideInStock,"
            qry = qry + "@OverdueDaysLimit,@OverdueDaysLimitActive,@OverdueDaysWarning,@OverdueDaysWarningActive,@chkAR,@DueDaysStopFreq,@SubCompanyNo,@SourceCompany,@chkSendServiceReport,@Telephone2,@BillTelephone2,@Mobile,@BillMobile,@SoPriceGroup,@POPrefix,"
            qry = qry + "@PONumber,@LastStatus,@OverdueMonthWarning,@OverDueMonthLimit,@AccountNo,@FlowFrom,@FlowTo,@InActive,@ShippingTerm,@InterCompany,@AutoEmailServ,@ReportFormatServ,@WebUploadDate,@IsCustomer,@IsSupplier,@PaxBased,@BillMonthly,@DiscType,@ARPDFFromat,@EmailConsolidate"
            qry = qry + ",@AccountID,@OffContact1Position,@OffContact1Tel,@OffContact1Fax,@OffContact1Tel2,@OffContact1Mobile,@BillContact1Position,@BillContact2,@BillContact2Position,@BillContact1Email,@BillContact2Email,@BillContact2Tel,@BillContact2Fax,@BillContact2Tel2,@BillContact2Mobile,@OffContact1,@OffContactPosition,@BillingSettings,@BillingName,@TermsDay,@Location, @DefaultInvoiceFormat, @AutoEmailInvoice,@AutoEmailSOA);"

            Dim AccountID As String = ""
            Dim Exists As Boolean = True


            InsertIntoTblWebEventLog("InsertData", dt.Rows.Count.ToString, "")

            Dim drow As DataRow
            Dim dc1 As DataColumn = New DataColumn("AccountID", GetType(String))
            Dim dc2 As DataColumn = New DataColumn("Name", GetType(String))
            Dim dc3 As DataColumn = New DataColumn("Status", GetType(String))
            Dim dc4 As DataColumn = New DataColumn("Remarks", GetType(String))
            dtLog.Columns.Add(dc1)
            dtLog.Columns.Add(dc2)
            dtLog.Columns.Add(dc3)
            dtLog.Columns.Add(dc4)

            For Each r As DataRow In dt.Rows

                drow = dtLog.NewRow()

                If IsDBNull(r("AccountID")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ACCOUNTID_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("AccountID"))

                    AccountID = r("AccountID")
                    drow("AccountID") = AccountID

                End If

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblcompany where accountid=@id"
                command2.Parameters.AddWithValue("@id", AccountID)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New System.Data.DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")
                    ' lblAlert.Text = "Account ID already exists!!!"
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(DUPLICATE)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ACCOUNTID_DUPLICATE"
                    If IsDBNull(r("Name")) = False Then
                        drow("Name") = r("Name")
                    End If
                    dtLog.Rows.Add(drow)

                Else
                    'Check for dropdownlist values, if it exists

                    If IsDBNull(r("Industry")) = False Then
                        If CheckIndustryExists(r("Industry"), conn) = False Then

                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(INDUSTRY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "INDUSTRY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("AddCity")) = False Then
                        If CheckCityExists(r("AddCity"), conn) = False Then

                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(CITY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "CITY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("AddState")) = False Then
                        If CheckStateExists(r("AddState"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(STATE DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "STATE DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("AddCountry")) = False Then
                        If CheckCountryExists(r("AddCountry"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(COUNTRY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "COUNTRY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("BillCity")) = False Then
                        If CheckCityExists(r("BillCity"), conn) = False Then

                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(CITY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "CITY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("BillState")) = False Then
                        If CheckStateExists(r("BillState"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(STATE DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "STATE DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("BillCountry")) = False Then
                        If CheckCountryExists(r("BillCountry"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(COUNTRY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "COUNTRY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("CreditTerms")) = False Then
                        If CheckTermsExists(r("CreditTerms"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(CREDIT TERMS DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "CREDIT TERMS DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("ARCurrency")) = False Then
                        If CheckCurrencyExists(r("ARCurrency"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(CURRENCY DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "CURRENCY DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If
                    End If

                    If IsDBNull(r("DefaultInvoiceFormat")) = False Then
                        If CheckInvoiceFormatExists(r("DefaultInvoiceFormat"), conn) = False Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(INVOICE FORMAT DOES NOT EXIST)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "INVOICE FORMAT DOES NOT EXIST"
                            If IsDBNull(r("Name")) = False Then
                                drow("Name") = r("Name")
                            End If
                            dtLog.Rows.Add(drow)
                            Continue For
                        End If

                    End If


                    Dim cmd As MySqlCommand = conn.CreateCommand()
                    '  Dim cmd As MySqlCommand = New MySqlCommand

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = qry
                    cmd.Parameters.Clear()

                    If IsDBNull(r("AccountID")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ACCOUNTID_BLANK"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)

                        Continue For

                    Else
                        cmd.Parameters.AddWithValue("@AccountID", r("AccountID").ToString.ToUpper)
                    End If

                    If IsDBNull(r("Name")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(NAME_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "NAME_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        drow("Name") = r("Name")
                        cmd.Parameters.AddWithValue("@Name", r("Name").ToString.ToUpper)
                    End If

                    '  If IsDBNull(r("OldName")) Then
                    If IsDBNull(r("OldName")) Then
                        cmd.Parameters.AddWithValue("@Name2", "")
                    Else
                        cmd.Parameters.AddWithValue("@Name2", r("OldName").ToString.ToUpper)
                    End If
                    cmd.Parameters.AddWithValue("@Status", "O")
                    cmd.Parameters.AddWithValue("@InActive", 0)

                    cmd.Parameters.AddWithValue("@CompanyGroup", "")
                    'If IsDBNull(r("CompanyGroup")) Then
                    '    Failure = Failure + 1
                    '    FailureString = FailureString + " " + AccountID + "(COMPANYGROUP_BLANK)"
                    '    drow("Status") = "Failed"
                    '    drow("Remarks") = "COMPANYGROUP_BLANK"
                    '    dtLog.Rows.Add(drow)
                    '    Continue For
                    'Else
                    '    cmd.Parameters.AddWithValue("@CompanyGroup", r("CompanyGroup"))
                    'End If
                    If IsDBNull(r("RegistrationNo")) Then
                        cmd.Parameters.AddWithValue("@RocNos", "")
                    Else
                        cmd.Parameters.AddWithValue("@RocNos", r("RegistrationNo").ToString.ToUpper)
                    End If
                    If IsDBNull(r("GstNos")) Then
                        cmd.Parameters.AddWithValue("@GstNos", "")
                    Else
                        cmd.Parameters.AddWithValue("@GstNos", r("GstNos").ToString.ToUpper)
                    End If

                    If IsDBNull(r("Website")) Then
                        cmd.Parameters.AddWithValue("@Website", "")
                    Else
                        cmd.Parameters.AddWithValue("@Website", r("Website").ToString.ToUpper)
                    End If
                    If IsDBNull(r("CustomerSince")) Then
                        cmd.Parameters.AddWithValue("@StartDate", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(r("CustomerSince")).ToString("yyyy-MM-dd"))
                    End If

                    If IsDBNull(r("Industry")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(Industry_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INDUSTRY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@Industry", r("Industry").ToString.ToUpper)

                    End If
                    cmd.Parameters.AddWithValue("@SalesMan", "")
                    cmd.Parameters.AddWithValue("@InChargeID", "")

                    'If IsDBNull(r("SalesMan")) Then
                    '    cmd.Parameters.AddWithValue("@SalesMan", "")
                    'Else
                    '    cmd.Parameters.AddWithValue("@SalesMan", r("SalesMan"))
                    'End If
                    'If IsDBNull(r("InChargeID")) Then
                    '    cmd.Parameters.AddWithValue("@InChargeID", "")
                    'Else
                    '    cmd.Parameters.AddWithValue("@InChargeID", r("InChargeID"))
                    'End If
                    If IsDBNull(r("Comments")) Then
                        cmd.Parameters.AddWithValue("@Comments", "")
                    Else
                        cmd.Parameters.AddWithValue("@Comments", r("Comments").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Address1")) Then
                        cmd.Parameters.AddWithValue("@Address1", "")
                    Else
                        cmd.Parameters.AddWithValue("@Address1", r("Address1").ToString.ToUpper)
                    End If


                    If IsDBNull(r("AddStreet")) Then
                        cmd.Parameters.AddWithValue("@AddStreet", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddStreet", r("AddStreet").ToString.ToUpper)
                    End If
                    If IsDBNull(r("AddBuilding")) Then
                        cmd.Parameters.AddWithValue("@AddBuilding", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddBuilding", r("AddBuilding").ToString.ToUpper)
                    End If
                    If IsDBNull(r("AddCity")) Then
                        cmd.Parameters.AddWithValue("@AddCity", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddCity", r("AddCity").ToString.ToUpper)
                    End If

                    If IsDBNull(r("AddState")) Then

                        cmd.Parameters.AddWithValue("@AddState", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddState", r("AddState").ToString.ToUpper)

                    End If

                    If IsDBNull(r("AddCountry")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(OFFICEADDRESS-COUNTRY_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "OFFICEADDRESS-COUNTRY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@AddCountry", r("AddCountry").ToString.ToUpper)
                    End If

                    If IsDBNull(r("AddPostal")) Then
                        cmd.Parameters.AddWithValue("@AddPostal", "")
                    Else
                        cmd.Parameters.AddWithValue("@AddPostal", r("AddPostal").ToString.ToUpper)
                    End If
                    If IsDBNull(r("ContactPerson")) Then
                        cmd.Parameters.AddWithValue("@ContactPerson", "")
                    Else
                        cmd.Parameters.AddWithValue("@ContactPerson", r("ContactPerson").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContactPosition")) Then
                        cmd.Parameters.AddWithValue("@OffContactPosition", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContactPosition", r("OffContactPosition").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Telephone")) Then
                        cmd.Parameters.AddWithValue("@Telephone", "")
                    Else
                        cmd.Parameters.AddWithValue("@Telephone", r("Telephone").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Fax")) Then
                        cmd.Parameters.AddWithValue("@Fax", "")
                    Else
                        cmd.Parameters.AddWithValue("@Fax", r("Fax").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Telephone2")) Then
                        cmd.Parameters.AddWithValue("@Telephone2", "")
                    Else
                        cmd.Parameters.AddWithValue("@Telephone2", r("Telephone2").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Mobile")) Then
                        cmd.Parameters.AddWithValue("@Mobile", "")
                    Else
                        cmd.Parameters.AddWithValue("@Mobile", r("Mobile").ToString.ToUpper)
                    End If


                    If IsDBNull(r("Email")) Then
                        cmd.Parameters.AddWithValue("@Email", "")
                    Else
                        cmd.Parameters.AddWithValue("@Email", r("Email").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1")) Then
                        cmd.Parameters.AddWithValue("@OffContact1", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1", r("OffContact1").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1Position")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Position", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Position", r("OffContact1Position").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1Tel")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Tel", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Tel", r("OffContact1Tel").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1Fax")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Fax", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Fax", r("OffContact1Fax").ToString.ToUpper)
                    End If
                    If IsDBNull(r("OffContact1Tel2")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Tel2", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Tel2", r("OffContact1Tel2").ToString.ToUpper)
                    End If

                    If IsDBNull(r("OffContact1Mobile")) Then
                        cmd.Parameters.AddWithValue("@OffContact1Mobile", "")
                    Else
                        cmd.Parameters.AddWithValue("@OffContact1Mobile", r("OffContact1Mobile").ToString.ToUpper)
                    End If
                    If IsDBNull(r("ContactPersonEmail")) Then
                        cmd.Parameters.AddWithValue("@ContactPersonEmail", "")
                    Else
                        cmd.Parameters.AddWithValue("@ContactPersonEmail", r("ContactPersonEmail").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillingAddress")) Then
                        cmd.Parameters.AddWithValue("@BillingAddress", 0)
                    ElseIf r("BillingAddress").ToString.ToUpper = "TRUE" Then
                        cmd.Parameters.AddWithValue("@BillingAddress", 1)
                    ElseIf r("BillingAddress").ToString.ToUpper = "FALSE" Then
                        cmd.Parameters.AddWithValue("@BillingAddress", 0)
                    End If

                    If IsDBNull(r("BillingName")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLINGNAME_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLINGNAME_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillingName", r("BillingName").ToString.ToUpper)
                    End If

                    'If Billing Address is same as Office Address - set to True, then copy all Office Contact Info to Billing Contact Info
                    Dim CheckBillingAddress As Boolean = False
                    If IsDBNull(r("BillingAddress")) Then
                        CheckBillingAddress = False
                    Else
                        If r("BillingAddress").ToString.ToUpper = "TRUE" Then
                            CheckBillingAddress = True
                        Else
                            CheckBillingAddress = False
                        End If
                    End If

                    If CheckBillingAddress = True Then
                        If IsDBNull(r("BillAddress1")) Then
                            If IsDBNull(r("Address1")) Then
                                cmd.Parameters.AddWithValue("@BillAddress1", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillAddress1", r("Address1").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillAddress1", r("BillAddress1").ToString.ToUpper)
                        End If
                        If IsDBNull(r("Billbuilding")) Then
                            If IsDBNull(r("AddBuilding")) Then
                                cmd.Parameters.AddWithValue("@BillBuilding", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillBuilding", r("AddBuilding").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@Billbuilding", r("Billbuilding").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillStreet")) Then
                            If IsDBNull(r("AddStreet")) Then
                                cmd.Parameters.AddWithValue("@BillStreet", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillStreet", r("AddStreet").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillStreet", r("BillStreet").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillCity")) Then
                            If IsDBNull(r("AddCity")) Then
                                cmd.Parameters.AddWithValue("@BillCity", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillCity", r("AddCity").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillCity", r("BillCity").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillState")) Then
                            If IsDBNull(r("AddState")) Then
                                cmd.Parameters.AddWithValue("@BillState", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillState", r("AddState").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillState", r("BillState").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillCountry")) Then
                            If IsDBNull(r("AddCountry")) Then
                                cmd.Parameters.AddWithValue("@BillCountry", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillCountry", r("AddCountry").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillCountry", r("BillCountry").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillPostal")) Then
                            If IsDBNull(r("AddPostal")) Then
                                cmd.Parameters.AddWithValue("@BillPostal", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillPostal", r("AddPostal").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillPostal", r("BillPostal").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContactPerson")) Then

                            If IsDBNull(r("ContactPerson")) Then
                                Failure = Failure + 1
                                FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                                drow("Status") = "Failed"
                                drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                                dtLog.Rows.Add(drow)
                                Continue For
                            Else
                                cmd.Parameters.AddWithValue("@BillContactPerson", r("ContactPerson").ToString.ToUpper)
                            End If

                        Else
                            cmd.Parameters.AddWithValue("@BillContactPerson", r("BillContactPerson").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact1Position")) Then
                            If IsDBNull(r("OffContactPosition")) Then
                                cmd.Parameters.AddWithValue("@BillContact1Position", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact1Position", r("OffContactPosition").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Position", r("BillContact1Position").ToString.ToUpper)
                        End If



                        If IsDBNull(r("BillTelephone")) Then
                            If IsDBNull(r("Telephone")) Then
                                cmd.Parameters.AddWithValue("@BillTelephone", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillTelephone", r("Telephone").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone", r("BillTelephone").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillFax")) Then
                            If IsDBNull(r("Fax")) Then
                                cmd.Parameters.AddWithValue("@BillFax", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillFax", r("Fax").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillFax", r("BillFax").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillTelephone2")) Then
                            If IsDBNull(r("Telephone2")) Then
                                cmd.Parameters.AddWithValue("@BillTelephone2", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillTelephone2", r("Telephone2").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone2", r("BillTelephone2").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillMobile")) Then
                            If IsDBNull(r("Mobile")) Then
                                cmd.Parameters.AddWithValue("@BillMobile", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillMobile", r("Mobile").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillMobile", r("BillMobile").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact1Email")) Then
                            If IsDBNull(r("Email")) Then
                                cmd.Parameters.AddWithValue("@BillContact1Email", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact1Email", r("Email").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Email", r("BillContact1Email").ToString.ToUpper)
                        End If


                        If IsDBNull(r("BillContact2")) Then
                            If IsDBNull(r("OffContact1")) Then
                                cmd.Parameters.AddWithValue("@BillContact2", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2", r("OffContact1").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2", r("BillContact2").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Position")) Then
                            If IsDBNull(r("OffContact1Position")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Position", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Position", r("OffContact1Position").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Position", r("BillContact2Position").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Tel")) Then
                            If IsDBNull(r("OffContact1Tel")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Tel", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Tel", r("OffContact1Tel").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel", r("BillContact2Tel").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Fax")) Then
                            If IsDBNull(r("OffContact1Fax")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Fax", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Fax", r("OffContact1Fax").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Fax", r("BillContact2Fax").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Tel2")) Then
                            If IsDBNull(r("OffContact1Tel2")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Tel2", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Tel2", r("OffContact1Tel2").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel2", r("BillContact2Tel2").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Mobile")) Then
                            If IsDBNull(r("OffContact1Mobile")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Mobile", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Mobile", r("OffContact1Mobile").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Mobile", r("BillContact2Mobile").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Email")) Then
                            If IsDBNull(r("ContactPersonEmail")) Then
                                cmd.Parameters.AddWithValue("@BillContact2Email", "")
                            Else
                                cmd.Parameters.AddWithValue("@BillContact2Email", r("ContactPersonEmail").ToString.ToUpper)
                            End If
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Email", r("BillContact2Email").ToString.ToUpper)
                        End If
                    Else
                        If IsDBNull(r("BillAddress1")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS1_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLINGADDRESS1_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillAddress1", r("BillAddress1").ToString.ToUpper)
                        End If
                        If IsDBNull(r("Billbuilding")) Then
                            cmd.Parameters.AddWithValue("@Billbuilding", "")
                        Else
                            cmd.Parameters.AddWithValue("@Billbuilding", r("Billbuilding").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillStreet")) Then
                            cmd.Parameters.AddWithValue("@BillStreet", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillStreet", r("BillStreet").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillCity")) Then
                            cmd.Parameters.AddWithValue("@BillCity", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillCity", r("BillCity").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillState")) Then
                            cmd.Parameters.AddWithValue("@BillState", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillState", r("BillState").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillCountry")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS-COUNTRY_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLINGADDRESS-COUNTRY_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillCountry", r("BillCountry").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillPostal")) Then
                            cmd.Parameters.AddWithValue("@BillPostal", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPostal", r("BillPostal").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContactPerson")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillContactPerson", r("BillContactPerson").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact1Position")) Then
                            cmd.Parameters.AddWithValue("@BillContact1Position", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Position", r("BillContact1Position").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillTelephone")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone", r("BillTelephone").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillFax")) Then
                            cmd.Parameters.AddWithValue("@BillFax", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillFax", r("BillFax").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillTelephone2")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone2", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone2", r("BillTelephone2").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillMobile")) Then
                            cmd.Parameters.AddWithValue("@BillMobile", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillMobile", r("BillMobile").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact1Email")) Then
                            cmd.Parameters.AddWithValue("@BillContact1Email", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Email", r("BillContact1Email").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2", r("BillContact2").ToString.ToUpper)
                        End If

                        If IsDBNull(r("BillContact2Position")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Position", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Position", r("BillContact2Position").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Tel")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Tel", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel", r("BillContact2Tel").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Fax")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Fax", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Fax", r("BillContact2Fax").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Tel2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Tel2", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel2", r("BillContact2Tel2").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Mobile")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Mobile", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Mobile", r("BillContact2Mobile").ToString.ToUpper)
                        End If
                        If IsDBNull(r("BillContact2Email")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Email", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Email", r("BillContact2Email").ToString.ToUpper)
                        End If
                    End If


                    If IsDBNull(r("CreditLimit")) Then
                        cmd.Parameters.AddWithValue("@ArLimit", 0.0)
                    Else
                        cmd.Parameters.AddWithValue("@ArLimit", r("CreditLimit"))
                    End If
                    If IsDBNull(r("CreditTerms")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CREDITTERMS_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CREDITTERMS_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@ARTERM", r("CreditTerms").ToString.ToUpper)
                    End If
                    If IsDBNull(r("ARCurrency")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CURRENCY_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CURRENCY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@ARCurrency", r("ARCurrency").ToString.ToUpper)
                    End If
                    If IsDBNull(r("SendStatement")) Then
                        cmd.Parameters.AddWithValue("@SendStatement", 0)
                    ElseIf r("SendStatement").ToString.ToUpper = "TRUE" Then
                        cmd.Parameters.AddWithValue("@SendStatement", 1)
                    ElseIf r("SendStatement").ToString.ToUpper = "FALSE" Then
                        cmd.Parameters.AddWithValue("@SendStatement", 0)
                    End If

                    If IsDBNull(r("AutoEmailInvoice")) Then
                        cmd.Parameters.AddWithValue("@AutoEmailInvoice", 0)
                    ElseIf r("AutoEmailInvoice").ToString.ToUpper = "TRUE" Then
                        cmd.Parameters.AddWithValue("@AutoEmailInvoice", 1)
                    ElseIf r("AutoEmailInvoice").ToString.ToUpper = "FALSE" Then
                        cmd.Parameters.AddWithValue("@AutoEmailInvoice", 0)
                    End If

                    If IsDBNull(r("AutoEmailSOA")) Then
                        cmd.Parameters.AddWithValue("@AutoEmailSOA", 0)
                    ElseIf r("AutoEmailSOA").ToString.ToUpper = "TRUE" Then
                        cmd.Parameters.AddWithValue("@AutoEmailSOA", 1)
                    ElseIf r("AutoEmailSOA").ToString.ToUpper = "FALSE" Then
                        cmd.Parameters.AddWithValue("@AutoEmailSOA", 0)
                    End If
                    If IsDBNull(r("DefaultInvoiceFormat")) Then
                        cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                    Else
                        cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", r("DefaultInvoiceFormat"))
                    End If
                    If IsDBNull(r("Location")) Then
                        cmd.Parameters.AddWithValue("@Location", "")
                    Else
                        cmd.Parameters.AddWithValue("@Location", r("Location").ToString.ToUpper)
                    End If
                    cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text + "_IMPORT")
                    cmd.Parameters.AddWithValue("@LastModifiedBy", txtCreatedBy.Text + "_IMPORT")


                    'If IsDBNull(r("CreatedBy")) Then
                    '    cmd.Parameters.AddWithValue("@CreatedBy", "EXCELIMPORT")

                    'Else
                    '    cmd.Parameters.AddWithValue("@CreatedBy", r("CreatedBy"))

                    'End If

                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    'If IsDBNull(r("CreatedBy")) Then
                    '    cmd.Parameters.AddWithValue("@LastModifiedBy", "EXCELIMPORT")

                    'Else
                    '    cmd.Parameters.AddWithValue("@LastModifiedBy", r("CreatedBy"))

                    'End If
                    cmd.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    cmd.Parameters.AddWithValue("@BillingSettings", "AccountID")



                    cmd.Parameters.AddWithValue("@Id", "")

                    cmd.Parameters.AddWithValue("@AddBlock", "")
                    cmd.Parameters.AddWithValue("@AddNos", "")
                    cmd.Parameters.AddWithValue("@AddFloor", "")
                    cmd.Parameters.AddWithValue("@AddUnit", "")

                    cmd.Parameters.AddWithValue("@BillBlock", "")
                    cmd.Parameters.AddWithValue("@BillNos", "")
                    cmd.Parameters.AddWithValue("@BillFloor", "")
                    cmd.Parameters.AddWithValue("@BillUnit", "")

                    cmd.Parameters.AddWithValue("@RocRegDate", DBNull.Value)

                    cmd.Parameters.AddWithValue("@AuthorizedCapital", 0)
                    cmd.Parameters.AddWithValue("@PaidupCapital", 0)
                    cmd.Parameters.AddWithValue("@CompanyType", "")


                    cmd.Parameters.AddWithValue("@FinanceCompanyId", "")
                    cmd.Parameters.AddWithValue("@FinanceCompany", "")

                    cmd.Parameters.AddWithValue("@ApLimit", 0)
                    cmd.Parameters.AddWithValue("@SalesLimit", 0)
                    cmd.Parameters.AddWithValue("@PurchaseLimit", 0)
                    cmd.Parameters.AddWithValue("@ApCurrency", "")
                    cmd.Parameters.AddWithValue("@GstRegistered", 0)


                    cmd.Parameters.AddWithValue("@Source", "")
                    cmd.Parameters.AddWithValue("@ARBal", 0)
                    cmd.Parameters.AddWithValue("@APBal", 0)
                    cmd.Parameters.AddWithValue("@Sales", 0)
                    cmd.Parameters.AddWithValue("@Purchase", 0)
                    cmd.Parameters.AddWithValue("@LocateGRP", "")


                    cmd.Parameters.AddWithValue("@SalesGRP", "")

                    cmd.Parameters.AddWithValue("@Dealer", "")
                    cmd.Parameters.AddWithValue("@LoginID", "")

                    cmd.Parameters.AddWithValue("@Password", "")
                    cmd.Parameters.AddWithValue("@WebLevel", "")

                    cmd.Parameters.AddWithValue("@APTERM", "")
                    cmd.Parameters.AddWithValue("@PriceGroup", "")


                    cmd.Parameters.AddWithValue("@Age0", 0)
                    cmd.Parameters.AddWithValue("@Age30", 0)
                    cmd.Parameters.AddWithValue("@Age60", 0)
                    cmd.Parameters.AddWithValue("@Age90", 0)
                    cmd.Parameters.AddWithValue("@Age120", 0)

                    cmd.Parameters.AddWithValue("@StopSalesYN", "")
                    cmd.Parameters.AddWithValue("@StopPurchYN", "")
                    cmd.Parameters.AddWithValue("@SpecCode", "")
                    cmd.Parameters.AddWithValue("@ArWarning", 0)

                    cmd.Parameters.AddWithValue("@LicenseNumber", 0)
                    cmd.Parameters.AddWithValue("@LicenseInfo", "")
                    cmd.Parameters.AddWithValue("@SalesGST", "")
                    cmd.Parameters.AddWithValue("@ArMethod", "")
                    cmd.Parameters.AddWithValue("@ApMethod", "")
                    cmd.Parameters.AddWithValue("@ProductM1", "")
                    cmd.Parameters.AddWithValue("@ProductM2", "")
                    cmd.Parameters.AddWithValue("@ProductM3", "")
                    cmd.Parameters.AddWithValue("@ProductM4", "")
                    cmd.Parameters.AddWithValue("@ProductF1", "")
                    cmd.Parameters.AddWithValue("@ProductF2", "")
                    cmd.Parameters.AddWithValue("@ProductF3", "")
                    cmd.Parameters.AddWithValue("@ProductF4", "")
                    cmd.Parameters.AddWithValue("@RentalTerm", "")


                    cmd.Parameters.AddWithValue("@Donor", 0)
                    cmd.Parameters.AddWithValue("@Member", 0)
                    cmd.Parameters.AddWithValue("@MemberType", "")
                    cmd.Parameters.AddWithValue("@MemberID", "")
                    cmd.Parameters.AddWithValue("@GIROID", "")
                    cmd.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                    cmd.Parameters.AddWithValue("@DateExpired", DBNull.Value)
                    cmd.Parameters.AddWithValue("@DateTerminate", DBNull.Value)
                    cmd.Parameters.AddWithValue("@TemplateNo", "")
                    cmd.Parameters.AddWithValue("@ARLedger", "")
                    cmd.Parameters.AddWithValue("@ARSubLedger", "")
                    cmd.Parameters.AddWithValue("@APLedger", "")
                    cmd.Parameters.AddWithValue("@APSubLedger", "")

                    cmd.Parameters.AddWithValue("@SrcCompID", "")
                    cmd.Parameters.AddWithValue("@DiscountPct", 0)
                    cmd.Parameters.AddWithValue("@PreferredCustYN", "")
                    cmd.Parameters.AddWithValue("@ChkGstInclusive", "")
                    cmd.Parameters.AddWithValue("@Reason", "")
                    cmd.Parameters.AddWithValue("@Boardmember", "")
                    cmd.Parameters.AddWithValue("@BoardDesignation", "")
                    cmd.Parameters.AddWithValue("@period", "")
                    cmd.Parameters.AddWithValue("@Intriducer", "")
                    cmd.Parameters.AddWithValue("@Organization", "")
                    cmd.Parameters.AddWithValue("@chkLetterIndemnity", 0)
                    cmd.Parameters.AddWithValue("@LetterIndemnitySignedBy", "")
                    cmd.Parameters.AddWithValue("@LeterDate", DBNull.Value)

                    cmd.Parameters.AddWithValue("@WebLoginID", "")
                    cmd.Parameters.AddWithValue("@WebLoginPassWord", "")
                    cmd.Parameters.AddWithValue("@WebAccessLevel", 0)
                    cmd.Parameters.AddWithValue("@WebOneTimePassWord", "")


                    cmd.Parameters.AddWithValue("@WebGroupDealer", 0)
                    cmd.Parameters.AddWithValue("@WebDisable", 0)
                    cmd.Parameters.AddWithValue("@WebID", "")
                    cmd.Parameters.AddWithValue("@OTPMobile", "")
                    cmd.Parameters.AddWithValue("@OTPYN", 0)
                    cmd.Parameters.AddWithValue("@OTPGenerateDate", DBNull.Value)
                    cmd.Parameters.AddWithValue("@HideInStock", 0)
                    cmd.Parameters.AddWithValue("@OverdueDaysLimit", 0)
                    cmd.Parameters.AddWithValue("@OverdueDaysLimitActive", 0)
                    cmd.Parameters.AddWithValue("@OverdueDaysWarning", 0)
                    cmd.Parameters.AddWithValue("@OverdueDaysWarningActive", 0)
                    cmd.Parameters.AddWithValue("@chkAR", 0)
                    cmd.Parameters.AddWithValue("@DueDaysStopFreq", "")
                    cmd.Parameters.AddWithValue("@SubCompanyNo", "")
                    cmd.Parameters.AddWithValue("@SourceCompany", "")
                    cmd.Parameters.AddWithValue("@chkSendServiceReport", 0)

                    cmd.Parameters.AddWithValue("@SoPriceGroup", "")
                    cmd.Parameters.AddWithValue("@POPrefix", "")
                    cmd.Parameters.AddWithValue("@PONumber", 0)
                    cmd.Parameters.AddWithValue("@LastStatus", "")
                    cmd.Parameters.AddWithValue("@OverdueMonthWarning", 0)
                    cmd.Parameters.AddWithValue("@OverDueMonthLimit", 0)
                    cmd.Parameters.AddWithValue("@AccountNo", "")
                    cmd.Parameters.AddWithValue("@FlowFrom", "")
                    cmd.Parameters.AddWithValue("@FlowTo", "")

                    cmd.Parameters.AddWithValue("@ShippingTerm", "")
                    cmd.Parameters.AddWithValue("@InterCompany", 0)
                    cmd.Parameters.AddWithValue("@AutoEmailServ", 0)
                    cmd.Parameters.AddWithValue("@ReportFormatServ", "")
                    cmd.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                    cmd.Parameters.AddWithValue("@IsCustomer", 0)

                    cmd.Parameters.AddWithValue("@IsSupplier", 0)

                    cmd.Parameters.AddWithValue("@PaxBased", 0)
                    cmd.Parameters.AddWithValue("@BillMonthly", 0)
                    cmd.Parameters.AddWithValue("@DiscType", "")
                    cmd.Parameters.AddWithValue("@ARPDFFromat", "")
                    cmd.Parameters.AddWithValue("@EmailConsolidate", 0)
                    cmd.Parameters.AddWithValue("@TermsDay", 0)


                    'cmd.Parameters.AddWithValue("@SmartCustomer", chkSmartCustomer.Checked)

                    cmd.Connection = conn

                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    Success = Success + 1
                    drow("Status") = "Success"
                    drow("Remarks") = ""
                    dtLog.Rows.Add(drow)
                End If
                ' InsertIntoTblWebEventLog("InserData2", Success.ToString, Failure.ToString)

            Next

            txtSuccessCount.Text = Success.ToString
            txtFailureCount.Text = Failure.ToString
            txtFailureString.Text = FailureString

            GridView1.DataSource = dtLog
            GridView1.DataBind()

            dt.Clear()


            Return True
        Catch ex As Exception
            txtSuccessCount.Text = Success.ToString
            txtFailureCount.Text = Failure.ToString
            txtFailureString.Text = FailureString
            lblAlert.Text = ex.Message.ToString


            InsertIntoTblWebEventLog("InsertCorporateData", ex.Message.ToString, txtCreatedBy.Text)

            Return False

        End Try
    End Function


    Protected Function InsertResidentialData(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable


        '    Try

        Dim qry As String = "INSERT INTO tblperson(Id,Salutation,Name,Nric,CountryBirth,DateBirth,Citizenship,Race,Sex,MartialStatus,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddPostal,AddCity,AddState,AddCountry,TelHome,TelMobile,TelPager,TelFax,BillingAddress,BillBlock,BillNos,BillFloor,BillUnit,BillBuilding,BillStreet,"
        qry = qry + "BillPostal,BillCity,BillState,BillCountry,Company,ComBlock,ComNos,ComFloor,ComUnit,ComBuilding,ComStreet,ComPostal,ComCity,ComState,ComCountry,ComTel,ComFax,Location,Department,Profession,Appointment,Email,TelDirect,TelExtension,Comments,FinanceCompanyId,FinanceCompany,ArLimit,ApLimit,SalesLimit,PurchaseLimit,ApCurrency,ArCurrency,SendStatement,Status,Address1,"
        qry = qry + "BillAddress1,ComAddress1,Source,LocateGRP,SalesGRP,PriceGroup,StopSalesYN,StopPurchYN,SpecCode,ArWarning,Name2,Language,RefNo1,RefNo2,ICType,WorkPass,PersonGroup,Patient,Donor,Member,Religion,Occupation,MemberType,DateJoin,DateExpired,DateTerminate,GIROID,MemberID,Proposer,TemplateNo,ARLedger,ARSubLedger,APLedger,APSubLedger,CreatedBy,CreatedOn,"
        qry = qry + "LastModifiedBy,LastModifiedOn,Reason,Education,Boardmember,BoardDesignation,period,Intriducer,Organization,DriveLicNo,DriveLicExp,DriveLicCountry,PassportNo,DriveLicIssueDate,WebLoginID,WebLoginPassWord,WebAccessLevel,WebOneTimePassWord,chkLetterIndemnity,ARTERM,APTERM,"
        qry = qry + "SalesGST,ChkGstInclusive,RentalTerm,ArMethod,ApMethod,Dealer,WebLevel,DiscountPct,LoginID,Password,Functions,BillContactPerson,Remarks,DrivingSince,WorkPassExpDt,AcupunctureStatement,CardPrinted,Note,InChargeID,DealerYN,WebDisableYN,OTPYN,WebID,SubCompanyNo,"
        qry = qry + "SourceCompany,chkSendServiceReport,SoPriceGroup,AccountNo,FlowFrom,FlowTo,Salesman,InActive,AutoEmailServ,ReportFormatServ,WebUploadDate,DriveLicEffDt,BillTelHome,BillTelFax,BillTelMobile,BillTelPager,BillEmail,IsCustomer,IsSupplier,WebCreateDeviceID,WebCreateSource,WebFlowFrom,WebFlowTo,WebEditSource,WebDeleteStatus,WebLastEditDevice,"
        qry = qry + "AccountID,Nationality,ContactPerson,ResCP2Tel,ResCP2Fax,ResCP2Tel2,ResCP2Mobile,ResCP2Email,ContactPerson2,BillContact1Position,BillContact2,BillContact2Position,BillContact2Email,BillContact2Tel,BillContact2Fax,BillContact2Tel2,BillContact2Mobile,BillingSettings,BillingName,TermsDay, DefaultInvoiceFormat, AutoEmailInvoice, AutoEmailSOA)"
        qry = qry + "VALUES(@Id,@Salutation,@Name,@Nric,@CountryBirth,@DateBirth,@Citizenship,@Race,@Sex,@MartialStatus,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddPostal,@AddCity,@AddState,@AddCountry,@TelHome,@TelMobile,@TelPager,@TelFax,@BillingAddress,"
        qry = qry + "@BillBlock,@BillNos,@BillFloor,@BillUnit,@BillBuilding,@BillStreet,@BillPostal,@BillCity,@BillState,@BillCountry,@Company,@ComBlock,@ComNos,@ComFloor,@ComUnit,@ComBuilding,@ComStreet,@ComPostal,@ComCity,@ComState,@ComCountry,@ComTel,@ComFax,@Location,"
        qry = qry + "@Department,@Profession,@Appointment,@Email,@TelDirect,@TelExtension,@Comments,@FinanceCompanyId,@FinanceCompany,@ArLimit,@ApLimit,@SalesLimit,@PurchaseLimit,@ApCurrency,@ArCurrency,@SendStatement,@Status,@Address1,@BillAddress1,@ComAddress1,@Source,"
        qry = qry + "@LocateGRP,@SalesGRP,@PriceGroup,@StopSalesYN,@StopPurchYN,@SpecCode,@ArWarning,@Name2,@Language,@RefNo1,@RefNo2,@ICType,@WorkPass,@PersonGroup,@Patient,@Donor,@Member,@Religion,@Occupation,@MemberType,@DateJoin,@DateExpired,@DateTerminate,"
        qry = qry + "@GIROID,@MemberID,@Proposer,@TemplateNo,@ARLedger,@ARSubLedger,@APLedger,@APSubLedger,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@Reason,@Education,@Boardmember,@BoardDesignation,@period,@Intriducer,@Organization,@DriveLicNo,@DriveLicExp,"
        qry = qry + "@DriveLicCountry,@PassportNo,@DriveLicIssueDate,@WebLoginID,@WebLoginPassWord,@WebAccessLevel,@WebOneTimePassWord,@chkLetterIndemnity,@ARTERM,@APTERM,@SalesGST,@ChkGstInclusive,@RentalTerm,@ArMethod,@ApMethod,@Dealer,@WebLevel,@DiscountPct,@LoginID,"
        qry = qry + "@Password,@Functions,@BillContactPerson,@Remarks,@DrivingSince,@WorkPassExpDt,@AcupunctureStatement,@CardPrinted,@Note,@InChargeID,@DealerYN,@WebDisableYN,@OTPYN,@WebID,@SubCompanyNo,@SourceCompany,@chkSendServiceReport,@SoPriceGroup,@AccountNo,"
        qry = qry + "@FlowFrom,@FlowTo,@Salesman,@InActive,@AutoEmailServ,@ReportFormatServ,@WebUploadDate,@DriveLicEffDt,@BillTelHome,@BillTelFax,@BillTelMobile,@BillTelPager,@BillEmail,@IsCustomer,@IsSupplier,@WebCreateDeviceID,@WebCreateSource,@WebFlowFrom,@WebFlowTo,@WebEditSource,@WebDeleteStatus,@WebLastEditDevice,"
        qry = qry + "@AccountID,@Nationality,@ContactPerson,@ResCP2Tel,@ResCP2Fax,@ResCP2Tel2,@ResCP2Mobile,@ResCP2Email,@ContactPerson2,@BillContact1Position,@BillContact2,@BillContact2Position,@BillContact2Email,@BillContact2Tel,@BillContact2Fax,@BillContact2Tel2,@BillContact2Mobile,@BillingSettings,@BillingName,@TermsDay, @DefaultInvoiceFormat, @AutoEmailInvoice, @AutoEmailSOA);"

        Dim AccountID As String = ""
        Dim Exists As Boolean = True


        InsertIntoTblWebEventLog("InsertData", dt.Rows.Count.ToString, "")

        Dim drow As DataRow
        Dim dc1 As DataColumn = New DataColumn("AccountID", GetType(String))
        Dim dc2 As DataColumn = New DataColumn("Name", GetType(String))
        Dim dc3 As DataColumn = New DataColumn("Status", GetType(String))
        Dim dc4 As DataColumn = New DataColumn("Remarks", GetType(String))
        dtLog.Columns.Add(dc1)
        dtLog.Columns.Add(dc2)
        dtLog.Columns.Add(dc3)
        dtLog.Columns.Add(dc4)

        For Each r As DataRow In dt.Rows

            drow = dtLog.NewRow()

            If IsDBNull(r("AccountID")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "ACCOUNTID_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("AccountID"))

                AccountID = r("AccountID")
                drow("AccountID") = AccountID

            End If

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT * FROM tblperson where accountid=@id"
            command2.Parameters.AddWithValue("@id", AccountID)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                '  MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")
                ' lblAlert.Text = "Account ID already exists!!!"
                Failure = Failure + 1
                FailureString = FailureString + " " + AccountID + "(DUPLICATE)"
                drow("Status") = "Failed"
                drow("Remarks") = "ACCOUNTID_DUPLICATE"
                If IsDBNull(r("Name")) = False Then
                    drow("Name") = r("Name")
                End If
                dtLog.Rows.Add(drow)

            Else
                'Check for dropdownlist values, if it exists

                If IsDBNull(r("Nationality")) = False Then
                    If CheckIndustryExists(r("Nationality"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(Nationality DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "NATIONALITY DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AddCity")) = False Then
                    If CheckCityExists(r("AddCity"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CITY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CITY DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AddState")) = False Then
                    If CheckStateExists(r("AddState"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(STATE DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "STATE DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AddCountry")) = False Then
                    If CheckCountryExists(r("AddCountry"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(COUNTRY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "COUNTRY DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("BillCity")) = False Then
                    If CheckCityExists(r("BillCity"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CITY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CITY DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("BillState")) = False Then
                    If CheckStateExists(r("BillState"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(STATE DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "STATE DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("BillCountry")) = False Then
                    If CheckCountryExists(r("BillCountry"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(COUNTRY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "COUNTRY DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("CreditTerms")) = False Then
                    If CheckTermsExists(r("CreditTerms"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CREDIT TERMS DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CREDIT TERMS DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("ARCurrency")) = False Then
                    If CheckCurrencyExists(r("ARCurrency"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CURRENCY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CURRENCY DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("DefaultInvoiceFormat")) = False Then
                    If CheckInvoiceFormatExists(r("DefaultInvoiceFormat"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(INVOICE FORMAT DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INVOICE FORMAT DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If

                End If


                Dim cmd As MySqlCommand = conn.CreateCommand()
                '  Dim cmd As MySqlCommand = New MySqlCommand

                cmd.CommandType = CommandType.Text
                cmd.CommandText = qry
                cmd.Parameters.Clear()

                If IsDBNull(r("AccountID")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ACCOUNTID_BLANK"
                    If IsDBNull(r("Name")) = False Then
                        drow("Name") = r("Name")
                    End If
                    dtLog.Rows.Add(drow)

                    Continue For

                Else
                    cmd.Parameters.AddWithValue("@AccountID", r("AccountID").ToString.ToUpper)
                End If

                If IsDBNull(r("Name")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(NAME_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "NAME_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    drow("Name") = r("Name")
                    cmd.Parameters.AddWithValue("@Name", r("Name").ToString.ToUpper)
                End If

                '  If IsDBNull(r("OldName")) Then
                If IsDBNull(r("OldName")) Then
                    cmd.Parameters.AddWithValue("@Name2", "")
                Else
                    cmd.Parameters.AddWithValue("@Name2", r("OldName").ToString.ToUpper)
                End If
                cmd.Parameters.AddWithValue("@Status", "O")
                cmd.Parameters.AddWithValue("@InActive", 0)
                If IsDBNull(r("Nric")) Then
                    cmd.Parameters.AddWithValue("@Nric", "")
                Else
                    cmd.Parameters.AddWithValue("@Nric", r("Nric"))
                End If
                If IsDBNull(r("Nationality")) Then
                    cmd.Parameters.AddWithValue("@Nationality", "")
                Else
                    cmd.Parameters.AddWithValue("@Nationality", r("Nationality").ToString.ToUpper)
                End If

                If IsDBNull(r("CustomerSince")) Then
                    cmd.Parameters.AddWithValue("@DateJoin", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@DateJoin", Convert.ToDateTime(r("CustomerSince")).ToString("yyyy-MM-dd"))
                End If
                If IsDBNull(r("Comments")) Then
                    cmd.Parameters.AddWithValue("@Comments", "")
                Else
                    cmd.Parameters.AddWithValue("@Comments", r("Comments").ToString.ToUpper)
                End If
                If IsDBNull(r("Address1")) Then
                    cmd.Parameters.AddWithValue("@Address1", "")
                Else
                    cmd.Parameters.AddWithValue("@Address1", r("Address1").ToString.ToUpper)
                End If

                If IsDBNull(r("AddStreet")) Then
                    cmd.Parameters.AddWithValue("@AddStreet", "")
                Else
                    cmd.Parameters.AddWithValue("@AddStreet", r("AddStreet").ToString.ToUpper)
                End If
                If IsDBNull(r("AddBuilding")) Then
                    cmd.Parameters.AddWithValue("@AddBuilding", "")
                Else
                    cmd.Parameters.AddWithValue("@AddBuilding", r("AddBuilding").ToString.ToUpper)
                End If
                If IsDBNull(r("AddCity")) Then
                    cmd.Parameters.AddWithValue("@AddCity", "")
                Else
                    cmd.Parameters.AddWithValue("@AddCity", r("AddCity").ToString.ToUpper)
                End If

                If IsDBNull(r("AddState")) Then

                    cmd.Parameters.AddWithValue("@AddState", "")
                Else
                    cmd.Parameters.AddWithValue("@AddState", r("AddState").ToString.ToUpper)

                End If

                If IsDBNull(r("AddCountry")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(OFFICEADDRESS-COUNTRY_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "OFFICEADDRESS-COUNTRY_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@AddCountry", r("AddCountry").ToString.ToUpper)
                End If

                If IsDBNull(r("AddPostal")) Then
                    cmd.Parameters.AddWithValue("@AddPostal", "")
                Else
                    cmd.Parameters.AddWithValue("@AddPostal", r("AddPostal").ToString.ToUpper)
                End If

                If IsDBNull(r("ContactPerson")) Then
                    cmd.Parameters.AddWithValue("@ContactPerson", "")
                Else
                    cmd.Parameters.AddWithValue("@ContactPerson", r("ContactPerson").ToString.ToUpper)
                End If

                If IsDBNull(r("Tel")) Then
                    cmd.Parameters.AddWithValue("@TelHome", "")
                Else
                    cmd.Parameters.AddWithValue("@TelHome", r("Tel").ToString.ToUpper)
                End If
                If IsDBNull(r("Fax")) Then
                    cmd.Parameters.AddWithValue("@TelFax", "")
                Else
                    cmd.Parameters.AddWithValue("@TelFax", r("Fax").ToString.ToUpper)
                End If
                If IsDBNull(r("Telephone2")) Then
                    cmd.Parameters.AddWithValue("@TelPager", "")
                Else
                    cmd.Parameters.AddWithValue("@TelPager", r("Telephone2").ToString.ToUpper)
                End If

                If IsDBNull(r("Mobile")) Then
                    cmd.Parameters.AddWithValue("@TelMobile", "")
                Else
                    cmd.Parameters.AddWithValue("@TelMobile", r("Mobile").ToString.ToUpper)
                End If

                If IsDBNull(r("Email")) Then
                    cmd.Parameters.AddWithValue("@Email", "")
                Else
                    cmd.Parameters.AddWithValue("@Email", r("Email").ToString.ToUpper)
                End If

                If IsDBNull(r("ContactPerson2")) Then
                    cmd.Parameters.AddWithValue("@ContactPerson2", "")
                Else
                    cmd.Parameters.AddWithValue("@ContactPerson2", r("ContactPerson2").ToString.ToUpper)
                End If

                If IsDBNull(r("TelCP2")) Then
                    cmd.Parameters.AddWithValue("@ResCP2Tel", "")
                Else
                    cmd.Parameters.AddWithValue("@ResCP2Tel", r("TelCP2").ToString.ToUpper)
                End If
                If IsDBNull(r("FaxCP2")) Then
                    cmd.Parameters.AddWithValue("@ResCP2Fax", "")
                Else
                    cmd.Parameters.AddWithValue("@ResCP2Fax", r("FaxCP2").ToString.ToUpper)
                End If

                If IsDBNull(r("Tel2CP2")) Then
                    cmd.Parameters.AddWithValue("@ResCP2Tel2", "")
                Else
                    cmd.Parameters.AddWithValue("@ResCP2Tel2", r("Tel2CP2").ToString.ToUpper)
                End If

                If IsDBNull(r("MobileCP2")) Then
                    cmd.Parameters.AddWithValue("@ResCP2Mobile", "")
                Else
                    cmd.Parameters.AddWithValue("@ResCP2Mobile", r("MobileCP2").ToString.ToUpper)
                End If

                If IsDBNull(r("EmailCP2")) Then
                    cmd.Parameters.AddWithValue("@ResCP2Email", "")
                Else
                    cmd.Parameters.AddWithValue("@ResCP2Email", r("EmailCP2").ToString.ToUpper)
                End If

                If IsDBNull(r("BillingAddress")) Then
                    cmd.Parameters.AddWithValue("@BillingAddress", 0)
                ElseIf r("BillingAddress").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@BillingAddress", 1)
                ElseIf r("BillingAddress").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@BillingAddress", 0)
                End If

                If IsDBNull(r("BillingName")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(BILLINGNAME_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "BILLINGNAME_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@BillingName", r("BillingName").ToString.ToUpper)
                End If

                'If Billing Address is same as Office Address - set to True, then copy all Office Contact Info to Billing Contact Info
                Dim CheckBillingAddress As Boolean = False
                If IsDBNull(r("BillingAddress")) Then
                    CheckBillingAddress = False
                Else
                    If r("BillingAddress").ToString.ToUpper = "TRUE" Then
                        CheckBillingAddress = True
                    Else
                        CheckBillingAddress = False
                    End If
                End If

                If CheckBillingAddress = True Then
                    If IsDBNull(r("BillAddress1")) Then
                        If IsDBNull(r("Address1")) Then
                            cmd.Parameters.AddWithValue("@BillAddress1", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillAddress1", r("Address1").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillAddress1", r("BillAddress1").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Billbuilding")) Then
                        If IsDBNull(r("AddBuilding")) Then
                            cmd.Parameters.AddWithValue("@BillBuilding", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillBuilding", r("AddBuilding").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@Billbuilding", r("Billbuilding").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillStreet")) Then
                        If IsDBNull(r("AddStreet")) Then
                            cmd.Parameters.AddWithValue("@BillStreet", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillStreet", r("AddStreet").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillStreet", r("BillStreet").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCity")) Then
                        If IsDBNull(r("AddCity")) Then
                            cmd.Parameters.AddWithValue("@BillCity", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillCity", r("AddCity").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillCity", r("BillCity").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillState")) Then
                        If IsDBNull(r("AddState")) Then
                            cmd.Parameters.AddWithValue("@BillState", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillState", r("AddState").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillState", r("BillState").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCountry")) Then
                        If IsDBNull(r("AddCountry")) Then
                            cmd.Parameters.AddWithValue("@BillCountry", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillCountry", r("AddCountry").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillCountry", r("BillCountry").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPostal")) Then
                        If IsDBNull(r("AddPostal")) Then
                            cmd.Parameters.AddWithValue("@BillPostal", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPostal", r("AddPostal").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillPostal", r("BillPostal").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContactPerson")) Then

                        If IsDBNull(r("ContactPerson")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillContactPerson", r("ContactPerson").ToString.ToUpper)
                        End If

                    Else
                        cmd.Parameters.AddWithValue("@BillContactPerson", r("BillContactPerson").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact1Position")) Then
                        cmd.Parameters.AddWithValue("@BillContact1Position", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact1Position", r("BillContact1Position").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTel")) Then
                        If IsDBNull(r("Tel")) Then
                            cmd.Parameters.AddWithValue("@BillTelHome", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelHome", r("Tel").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelHome", r("BillTel").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax")) Then
                        If IsDBNull(r("Fax")) Then
                            cmd.Parameters.AddWithValue("@BillTelFax", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelFax", r("Fax").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelFax", r("BillFax").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTel2")) Then
                        If IsDBNull(r("Telephone2")) Then
                            cmd.Parameters.AddWithValue("@BillTelPager", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelPager", r("Telephone2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelPager", r("BillTel2").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillMobile")) Then
                        If IsDBNull(r("Mobile")) Then
                            cmd.Parameters.AddWithValue("@BillTelMobile", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelMobile", r("Mobile").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelMobile", r("BillMobile").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillEmail")) Then
                        If IsDBNull(r("Email")) Then
                            cmd.Parameters.AddWithValue("@BillEmail", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillEmail", r("Email").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail", r("BillEmail").ToString.ToUpper)
                    End If


                    If IsDBNull(r("BillContact2")) Then
                        If IsDBNull(r("ContactPerson2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2", r("ContactPerson2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2", r("BillContact2").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact2Position")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Position", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Position", r("BillContact2Position").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact2Tel")) Then
                        If IsDBNull(r("TelCP2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Tel", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel", r("TelCP2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Tel", r("BillContact2Tel").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact2Fax")) Then
                        If IsDBNull(r("FaxCP2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Fax", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Fax", r("FaxCP2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Fax", r("BillContact2Fax").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContact2Tel2")) Then
                        If IsDBNull(r("Tel2CP2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Tel2", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Tel2", r("Tel2CP2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Tel2", r("BillContact2Tel2").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact2Mobile")) Then
                        If IsDBNull(r("MobileCP2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Mobile", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Mobile", r("MobileCP2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Mobile", r("BillContact2Mobile").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContact2Email")) Then
                        If IsDBNull(r("EmailCP2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Email", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Email", r("EmailCP2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Email", r("BillContact2Email").ToString.ToUpper)
                    End If
                Else
                    If IsDBNull(r("BillAddress1")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS1_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLINGADDRESS1_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillAddress1", r("BillAddress1").ToString.ToUpper)
                    End If
                    If IsDBNull(r("Billbuilding")) Then
                        cmd.Parameters.AddWithValue("@Billbuilding", "")
                    Else
                        cmd.Parameters.AddWithValue("@Billbuilding", r("Billbuilding").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillStreet")) Then
                        cmd.Parameters.AddWithValue("@BillStreet", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillStreet", r("BillStreet").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCity")) Then
                        cmd.Parameters.AddWithValue("@BillCity", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillCity", r("BillCity").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillState")) Then
                        cmd.Parameters.AddWithValue("@BillState", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillState", r("BillState").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillCountry")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS-COUNTRY_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLINGADDRESS-COUNTRY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillCountry", r("BillCountry").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillPostal")) Then
                        cmd.Parameters.AddWithValue("@BillPostal", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillPostal", r("BillPostal").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContactPerson")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillContactPerson", r("BillContactPerson").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact1Position")) Then
                        cmd.Parameters.AddWithValue("@BillContact1Position", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact1Position", r("BillContact1Position").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTel")) Then
                        cmd.Parameters.AddWithValue("@BillTelHome", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelHome", r("BillTel").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax")) Then
                        cmd.Parameters.AddWithValue("@BillTelFax", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelFax", r("BillFax").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTel2")) Then
                        cmd.Parameters.AddWithValue("@BillTelPager", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelPager", r("BillTel2").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillMobile")) Then
                        cmd.Parameters.AddWithValue("@BillTelMobile", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelMobile", r("BillMobile").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillEmail")) Then
                        cmd.Parameters.AddWithValue("@BillEmail", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail", r("BillEmail").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact2")) Then
                        cmd.Parameters.AddWithValue("@BillContact2", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2", r("BillContact2").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact2Position")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Position", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Position", r("BillContact2Position").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContact2Tel")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Tel", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Tel", r("BillContact2Tel").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContact2Fax")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Fax", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Fax", r("BillContact2Fax").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContact2Tel2")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Tel2", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Tel2", r("BillContact2Tel2").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContact2Mobile")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Mobile", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Mobile", r("BillContact2Mobile").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillContact2Email")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Email", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Email", r("BillContact2Email").ToString.ToUpper)
                    End If
                End If

                If IsDBNull(r("CreditLimit")) Then
                    cmd.Parameters.AddWithValue("@ArLimit", 0.0)
                Else
                    cmd.Parameters.AddWithValue("@ArLimit", r("CreditLimit"))
                End If

                If IsDBNull(r("CreditTerms")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(CREDITTERMS_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "CREDITTERMS_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@ARTERM", r("CreditTerms").ToString.ToUpper)
                End If

                If IsDBNull(r("ARCurrency")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(CURRENCY_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "CURRENCY_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@ARCurrency", r("ARCurrency").ToString.ToUpper)
                End If

                If IsDBNull(r("SendStatement")) Then
                    cmd.Parameters.AddWithValue("@SendStatement", 0)
                ElseIf r("SendStatement").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@SendStatement", 1)
                ElseIf r("SendStatement").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@SendStatement", 0)
                End If

                If IsDBNull(r("AutoEmailInvoice")) Then
                    cmd.Parameters.AddWithValue("@AutoEmailInvoice", 0)
                ElseIf r("AutoEmailInvoice").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@AutoEmailInvoice", 1)
                ElseIf r("AutoEmailInvoice").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@AutoEmailInvoice", 0)
                End If

                If IsDBNull(r("AutoEmailSOA")) Then
                    cmd.Parameters.AddWithValue("@AutoEmailSOA", 0)
                ElseIf r("AutoEmailSOA").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@AutoEmailSOA", 1)
                ElseIf r("AutoEmailSOA").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@AutoEmailSOA", 0)
                End If

                If IsDBNull(r("DefaultInvoiceFormat")) Then
                    cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", "")
                Else
                    cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", r("DefaultInvoiceFormat"))
                End If

                cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text + "_IMPORT")
                cmd.Parameters.AddWithValue("@LastModifiedBy", txtCreatedBy.Text + "_IMPORT")

                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                cmd.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                cmd.Parameters.AddWithValue("@BillingSettings", "AccountID")

                cmd.Parameters.AddWithValue("@Id", "")
                cmd.Parameters.AddWithValue("@Salutation", "")
                cmd.Parameters.AddWithValue("@CountryBirth", "")
                cmd.Parameters.AddWithValue("@DateBirth", DBNull.Value)
                cmd.Parameters.AddWithValue("@Citizenship", "")
                cmd.Parameters.AddWithValue("@Race", "")
                cmd.Parameters.AddWithValue("@Sex", "")

                cmd.Parameters.AddWithValue("@MartialStatus", "")
                cmd.Parameters.AddWithValue("@AddBlock", "")
                cmd.Parameters.AddWithValue("@AddNos", "")
                cmd.Parameters.AddWithValue("@AddFloor", "")
                cmd.Parameters.AddWithValue("@AddUnit", "")
                cmd.Parameters.AddWithValue("@BillBlock", "")
                cmd.Parameters.AddWithValue("@BillNos", "")
                cmd.Parameters.AddWithValue("@BillFloor", "")
                cmd.Parameters.AddWithValue("@BillUnit", "")

                cmd.Parameters.AddWithValue("@Company", "")
                cmd.Parameters.AddWithValue("@ComBlock", "")
                cmd.Parameters.AddWithValue("@ComNos", "")
                cmd.Parameters.AddWithValue("@ComFloor", "")
                cmd.Parameters.AddWithValue("@ComUnit", "")
                cmd.Parameters.AddWithValue("@ComBuilding", "")
                cmd.Parameters.AddWithValue("@ComStreet", "")
                cmd.Parameters.AddWithValue("@ComPostal", "")
                cmd.Parameters.AddWithValue("@ComCity", "")
                cmd.Parameters.AddWithValue("@ComState", "")
                cmd.Parameters.AddWithValue("@ComCountry", "")
                cmd.Parameters.AddWithValue("@ComTel", "")
                cmd.Parameters.AddWithValue("@ComFax", "")
                cmd.Parameters.AddWithValue("@Location", "")

                cmd.Parameters.AddWithValue("@Department", "")
                cmd.Parameters.AddWithValue("@Profession", "")
                cmd.Parameters.AddWithValue("@Appointment", "")
                cmd.Parameters.AddWithValue("@TelDirect", "")
                cmd.Parameters.AddWithValue("@TelExtension", "")
                cmd.Parameters.AddWithValue("@FinanceCompanyId", "")
                cmd.Parameters.AddWithValue("@FinanceCompany", "")
                cmd.Parameters.AddWithValue("@ApLimit", 0)
                cmd.Parameters.AddWithValue("@SalesLimit", 0)
                cmd.Parameters.AddWithValue("@PurchaseLimit", 0)
                cmd.Parameters.AddWithValue("@ApCurrency", 0)
                cmd.Parameters.AddWithValue("@ComAddress1", "")
                cmd.Parameters.AddWithValue("@Source", "")
                cmd.Parameters.AddWithValue("@LocateGRP", "")
                cmd.Parameters.AddWithValue("@SalesGRP", "")

                cmd.Parameters.AddWithValue("@PriceGroup", "")

                cmd.Parameters.AddWithValue("@StopSalesYN", "")
                cmd.Parameters.AddWithValue("@StopPurchYN", "")
                cmd.Parameters.AddWithValue("@SpecCode", "")
                cmd.Parameters.AddWithValue("@ArWarning", 0)

                cmd.Parameters.AddWithValue("@Language", "")
                cmd.Parameters.AddWithValue("@RefNo1", "")
                cmd.Parameters.AddWithValue("@RefNo2", "")

                cmd.Parameters.AddWithValue("@ICType", "")

                cmd.Parameters.AddWithValue("@WorkPass", "")
                cmd.Parameters.AddWithValue("@PersonGroup", "")

                cmd.Parameters.AddWithValue("@Patient", 0)
                cmd.Parameters.AddWithValue("@Donor", 0)
                cmd.Parameters.AddWithValue("@Member", 0)
                cmd.Parameters.AddWithValue("@Religion", "")
                cmd.Parameters.AddWithValue("@Occupation", "")
                cmd.Parameters.AddWithValue("@MemberType", "")

                cmd.Parameters.AddWithValue("@DateExpired", DBNull.Value)
                cmd.Parameters.AddWithValue("@DateTerminate", DBNull.Value)
                cmd.Parameters.AddWithValue("@GIROID", "")
                cmd.Parameters.AddWithValue("@MemberID", "")
                cmd.Parameters.AddWithValue("@Proposer", "")
                cmd.Parameters.AddWithValue("@TemplateNo", "")
                cmd.Parameters.AddWithValue("@ARLedger", "")
                cmd.Parameters.AddWithValue("@ARSubLedger", "")
                cmd.Parameters.AddWithValue("@APLedger", "")
                cmd.Parameters.AddWithValue("@APSubLedger", "")

                cmd.Parameters.AddWithValue("@Reason", "")
                cmd.Parameters.AddWithValue("@Education", "")
                cmd.Parameters.AddWithValue("@Boardmember", "")
                cmd.Parameters.AddWithValue("@BoardDesignation", "")
                cmd.Parameters.AddWithValue("@period", "")
                cmd.Parameters.AddWithValue("@Intriducer", "")
                cmd.Parameters.AddWithValue("@Organization", "")
                cmd.Parameters.AddWithValue("@DriveLicNo", "")
                cmd.Parameters.AddWithValue("@DriveLicExp", DBNull.Value)
                cmd.Parameters.AddWithValue("@DriveLicCountry", "")
                cmd.Parameters.AddWithValue("@PassportNo", "")
                cmd.Parameters.AddWithValue("@DriveLicIssueDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@WebLoginID", "")
                cmd.Parameters.AddWithValue("@WebLoginPassWord", "")
                cmd.Parameters.AddWithValue("@WebAccessLevel", 0)
                cmd.Parameters.AddWithValue("@WebOneTimePassWord", "")
                cmd.Parameters.AddWithValue("@chkLetterIndemnity", 0)
                cmd.Parameters.AddWithValue("@APTERM", "")
                cmd.Parameters.AddWithValue("@SalesGST", "")
                cmd.Parameters.AddWithValue("@ChkGstInclusive", "")
                cmd.Parameters.AddWithValue("@RentalTerm", "")
                cmd.Parameters.AddWithValue("@ArMethod", "")
                cmd.Parameters.AddWithValue("@ApMethod", "")
                cmd.Parameters.AddWithValue("@Dealer", "")
                cmd.Parameters.AddWithValue("@WebLevel", "")
                cmd.Parameters.AddWithValue("@DiscountPct", 0)
                cmd.Parameters.AddWithValue("@LoginID", "")
                cmd.Parameters.AddWithValue("@Password", "")
                cmd.Parameters.AddWithValue("@Functions", "")

                cmd.Parameters.AddWithValue("@Remarks", "")
                cmd.Parameters.AddWithValue("@DrivingSince", DBNull.Value)
                cmd.Parameters.AddWithValue("@WorkPassExpDt", DBNull.Value)
                cmd.Parameters.AddWithValue("@AcupunctureStatement", 0)
                cmd.Parameters.AddWithValue("@CardPrinted", 0)
                cmd.Parameters.AddWithValue("@Note", "")
                cmd.Parameters.AddWithValue("@InChargeID", "")

                cmd.Parameters.AddWithValue("@DealerYN", 0)
                cmd.Parameters.AddWithValue("@WebDisableYN", 0)
                cmd.Parameters.AddWithValue("@OTPYN", 0)
                cmd.Parameters.AddWithValue("@WebID", "")
                cmd.Parameters.AddWithValue("@SubCompanyNo", "")
                cmd.Parameters.AddWithValue("@SourceCompany", "")
                cmd.Parameters.AddWithValue("@chkSendServiceReport", 0)
                cmd.Parameters.AddWithValue("@SoPriceGroup", "")
                cmd.Parameters.AddWithValue("@AccountNo", "")
                cmd.Parameters.AddWithValue("@FlowFrom", "")
                cmd.Parameters.AddWithValue("@FlowTo", "")
                cmd.Parameters.AddWithValue("@SalesMan", "")

                cmd.Parameters.AddWithValue("@AutoEmailServ", 0)
                cmd.Parameters.AddWithValue("@ReportFormatServ", "")
                cmd.Parameters.AddWithValue("@WebUploadDate", DBNull.Value)
                cmd.Parameters.AddWithValue("@DriveLicEffDt", DBNull.Value)

                cmd.Parameters.AddWithValue("@IsCustomer", 0)

                cmd.Parameters.AddWithValue("@IsSupplier", 0)

                cmd.Parameters.AddWithValue("@WebCreateDeviceID", "")
                cmd.Parameters.AddWithValue("@WebCreateSource", "")
                cmd.Parameters.AddWithValue("@WebFlowFrom", "")
                cmd.Parameters.AddWithValue("@WebFlowTo", "")
                cmd.Parameters.AddWithValue("@WebEditSource", "")
                cmd.Parameters.AddWithValue("@WebDeleteStatus", "")
                cmd.Parameters.AddWithValue("@WebLastEditDevice", "")
                cmd.Parameters.AddWithValue("@TermsDay", 0)

                cmd.Connection = conn

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                Success = Success + 1
                drow("Status") = "Success"
                drow("Remarks") = ""
                dtLog.Rows.Add(drow)
            End If
            ' InsertIntoTblWebEventLog("InserData2", Success.ToString, Failure.ToString)

        Next

        txtSuccessCount.Text = Success.ToString
        txtFailureCount.Text = Failure.ToString
        txtFailureString.Text = FailureString

        GridView1.DataSource = dtLog
        GridView1.DataBind()

        dt.Clear()


        Return True
        'Catch ex As Exception
        '    txtSuccessCount.Text = Success.ToString
        '    txtFailureCount.Text = Failure.ToString
        '    txtFailureString.Text = FailureString
        '    lblAlert.Text = ex.Message.ToString


        '    InsertIntoTblWebEventLog("InsertResidentialData", ex.Message.ToString, txtCreatedBy.Text)

        '    Return False

        'End Try
    End Function


    Protected Function InsertResidentialLocationData(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable


        ' Try
        Dim qry As String = "INSERT INTO tblpersonlocation(personid,Location,BranchID,Description,ContactPerson,Address1,Telephone,Mobile,Email,CreatedBy,CreatedOn,"
        qry = qry + "LastModifiedBy,LastModifiedOn,AddBlock,AddNos,AddFloor,AddUnit,AddBuilding,AddStreet,AddCity,AddState,AddCountry,AddPostal,LocateGrp,Fax,"
        qry = qry + "AccountID,LocationID,LocationPrefix,LocationNo,ServiceName,Contact1Position,Telephone2,ContactPerson2,Contact2Position,"
        qry = qry + "Contact2Tel,Contact2Fax,Contact2Tel2,Contact2Mobile,Contact2Email,ServiceAddress, ServiceLocationGroup,BillingNameSvc,"
        qry = qry + "BillAddressSvc,BillStreetSvc,BillBuildingSvc,BillCitySvc,BillStateSvc,BillCountrySvc,BillPostalSvc,BillContact1Svc,BillPosition1Svc,"
        qry = qry + "BillTelephone1Svc,BillFax1Svc,Billtelephone12Svc,BillMobile1Svc,BillEmail1Svc,BillContact2Svc,BillPosition2Svc,BillTelephone2Svc,"
        qry = qry + "BillFax2Svc,Billtelephone22Svc,BillMobile2Svc,BillEmail2Svc, InChargeIdSvc, ArTermSvc, SalesmanSvc, SendServiceReportTo1, SendServiceReportTo2,"
        qry = qry + " ContractGroup, Comments, PersonGroupD, InActiveD, Industry, MarketSegmentId, DefaultInvoiceFormat, ServiceEmailCC, EmailServiceNotificationOnly,"
        qry = qry + " SmartCustomer)VALUES(@personid,@Location,@BranchID,@Description,@ContactPerson,@Address1,@Telephone,@Mobile,@Email,@CreatedBy,@CreatedOn,"
        qry = qry + "@LastModifiedBy,@LastModifiedOn,@AddBlock,@AddNos,@AddFloor,@AddUnit,@AddBuilding,@AddStreet,@AddCity,@AddState,@AddCountry,@AddPostal,@LocateGrp,@Fax,"
        qry = qry + "@AccountID,@LocationID,@LocationPrefix,@LocationNo,@ServiceName,@Contact1Position,@Tel2,@ContactPerson2,@Contact2Position,"
        qry = qry + "@Contact2Tel,@Contact2Fax,@Contact2Tel2,@Contact2Mobile,@Contact2Email,@ServiceAddress, @ServiceLocationGroup,@BillingNameSvc,"
        qry = qry + "@BillAddressSvc,@BillStreetSvc,@BillBuildingSvc,@BillCitySvc,@BillStateSvc,@BillCountrySvc,@BillPostalSvc,@BillContact1Svc,@BillPosition1Svc,@BillTelephone1Svc,@BillFax1Svc,@Billtelephone12Svc,"
        qry = qry + "@BillMobile1Svc,@BillEmail1Svc,@BillContact2Svc,@BillPosition2Svc,@BillTelephone2Svc,@BillFax2Svc,@Billtelephone22Svc,@BillMobile2Svc,@BillEmail2Svc,"
        qry = qry + " @InChargeIdSvc, @ArTermSvc, @SalesmanSvc, @SendServiceReportTo1, @SendServiceReportTo2, @ContractGroup, @Comments, @PersonGroupD, @InActiveD,  @Industry, "
        qry = qry + "@MarketSegmentId,  @DefaultInvoiceFormat, @ServiceEmailCC, @EmailServiceNotificationOnly, @SmartCustomer);"

        Dim AccountID As String = ""
        Dim LocationID As String = ""
        Dim Exists As Boolean = True


        InsertIntoTblWebEventLog("InsertData", dt.Rows.Count.ToString, "")

        Dim drow As DataRow
        Dim dc1 As DataColumn = New DataColumn("AccountID", GetType(String))
        Dim dc5 As DataColumn = New DataColumn("LocationID", GetType(String))
        Dim dc2 As DataColumn = New DataColumn("Name", GetType(String))
        Dim dc3 As DataColumn = New DataColumn("Status", GetType(String))
        Dim dc4 As DataColumn = New DataColumn("Remarks", GetType(String))
        dtLog.Columns.Add(dc1)
        dtLog.Columns.Add(dc5)
        dtLog.Columns.Add(dc2)
        dtLog.Columns.Add(dc3)
        dtLog.Columns.Add(dc4)


        For Each r As DataRow In dt.Rows

            drow = dtLog.NewRow()

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CHECK IF ACCOUNTID FIELD IS BLANK
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If IsDBNull(r("AccountID")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "ACCOUNTID_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                '  InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("AccountID"))

                AccountID = r("AccountID")
                drow("AccountID") = AccountID

            End If

            ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''CHECK IF LOCATIONID FIELD IS BLANK
            ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'If IsDBNull(r("LocationID")) Then
            '    Failure = Failure + 1
            '    FailureString = FailureString + " " + LocationID + "(LOCATIONID_BLANK)"
            '    drow("Status") = "Failed"
            '    drow("Remarks") = "LOCATIONID_BLANK"
            '    dtLog.Rows.Add(drow)
            '    Continue For
            'Else
            '    InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("LocationID"))

            '    LocationID = r("LocationID")
            '    drow("LocationID") = LocationID

            'End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CHECK IF SERVICENAME FIELD IS BLANK
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If IsDBNull(r("ServiceName")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + LocationID + "(SERVICENAME_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "SERVICENAME_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                drow("Name") = r("ServiceName")
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CHECK IF ACCOUNTID FIELD EXISTS - exit  if IT DOES NOT exists
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT * FROM tblperson where accountid=@id"
            command2.Parameters.AddWithValue("@id", AccountID)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count = 0 Then

                Failure = Failure + 1

                FailureString = FailureString + " " + AccountID + "(DOES NOT EXIST)"
                drow("Status") = "Failed"
                drow("Remarks") = "ACCOUNTID_DOES NOT EXIST"
                dtLog.Rows.Add(drow)
                Continue For

            Else
                ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''CHECK IF SERVICE LOCATION ALREADY EXISTS - EXIT IF EXISTS
                ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim addr As String = ""
                If IsDBNull(r("Address1")) = False Then
                    addr = r("Address1").ToString.Trim + " "
                Else
                    addr = " "
                End If
                If IsDBNull(r("AddStreet")) = False Then
                    addr = addr + r("AddStreet").ToString.Trim + " "
                Else
                    addr = addr + " "
                End If
                If IsDBNull(r("AddBuilding")) = False Then
                    addr = addr + r("AddBuilding").ToString.Trim
                Else
                    addr = addr + " "
                End If
                ' addr = addr.Trim

                Dim ContractGroup As String = ""
                If IsDBNull(r("ContractGroup")) = False Then
                    ContractGroup = r("ContractGroup").ToString.Trim
                End If

                Dim PersonGroup As String = ""
                If IsDBNull(r("PersonGroupD")) = False Then
                    PersonGroup = r("PersonGroupD").ToString.Trim
                End If

                If CheckResidentialLocationDuplicate(conn, AccountID, addr, ContractGroup, PersonGroup) = False Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(DUPLICATE)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "SERVICE LOCATION_DUPLICATE"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If

                '''''''''''''''''''''''''''''''''''''''''''''''''''
                'OTHER VALIDATIONS
                '''''''''''''''''''''''''''''''''''''''''''''''''''

                ''Check for dropdownlist values, if it exists

                If IsDBNull(r("PersonGroupD")) = False Then
                    If CheckCompanyGroupExists(r("PersonGroupD"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(PersonGroup DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "PERSON GROUP DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                Else
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(PERSONGROUP_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "PERSONGROUP_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If

                If IsDBNull(r("ContractGroup")) = False Then
                    If CheckContractGroupExists(r("ContractGroup"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CONTRACTGROUP DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CONTRACTGROUP DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                Else
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(CONTRACTGROUP_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "CONTRACTGROUP_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If

                If IsDBNull(r("AddCity")) = False Then
                    If CheckCityExists(r("AddCity"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CITY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CITY DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AddState")) = False Then
                    If CheckStateExists(r("AddState"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(STATE DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "STATE DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("AddCountry")) = False Then
                    If CheckCountryExists(r("AddCountry"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(COUNTRY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "COUNTRY DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("Zone")) = False Then
                    If CheckLocationGroupExists(r("Zone"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(ZONE DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "ZONE DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If


                If IsDBNull(r("Industry")) = False Then
                    If CheckIndustryExists(r("Industry"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(INDUSTRY DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INDUSTRY DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If

                Else
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(INDUSTRY_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "INDUSTRY_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If

                If IsDBNull(r("Salesman")) = False Then
                    If CheckSalesmanExists(r("Salesman"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(SALESMAN DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "SALESMAN DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If

                Else
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(SALESMAN_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "SALESMAN_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If


                If IsDBNull(r("InchargeID")) = False Then
                    If CheckInchargeIDExists(r("InchargeID"), conn) = False Then

                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(InchargeID DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INCHARGEID DOES NOT EXIST"
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("CreditTerms")) = False Then
                    If CheckTermsExists(r("CreditTerms"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(CREDIT TERMS DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CREDIT TERMS DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If
                End If

                If IsDBNull(r("DefaultInvoiceFormat")) = False Then
                    If CheckInvoiceFormatExists(r("DefaultInvoiceFormat"), conn) = False Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(INVOICE FORMAT DOES NOT EXIST)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "INVOICE FORMAT DOES NOT EXIST"
                        If IsDBNull(r("Name")) = False Then
                            drow("Name") = r("Name")
                        End If
                        dtLog.Rows.Add(drow)
                        Continue For
                    End If

                End If


                Dim cmd As MySqlCommand = conn.CreateCommand()
                '  Dim cmd As MySqlCommand = New MySqlCommand

                cmd.CommandType = CommandType.Text
                cmd.CommandText = qry
                cmd.Parameters.Clear()

                If IsDBNull(r("AccountID")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(ACCOUNTID_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ACCOUNTID_BLANK"
                    If IsDBNull(r("ServiceName")) = False Then
                        drow("Name") = r("ServiceName")
                    End If
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@AccountID", r("AccountID").ToString.ToUpper)
                End If

                cmd.Parameters.AddWithValue("@InActiveD", False)
              
                If IsDBNull(r("ClientID")) Then
                    cmd.Parameters.AddWithValue("@personid", "")
                Else
                    cmd.Parameters.AddWithValue("@personid", r("ClientID").ToString.ToUpper)
                End If

                If IsDBNull(r("Location")) Then
                    cmd.Parameters.AddWithValue("@Location", "")
                Else
                    cmd.Parameters.AddWithValue("@Location", r("Location").ToString.ToUpper)
                End If
             
                If IsDBNull(r("ServiceLocationGroup")) Then
                    cmd.Parameters.AddWithValue("@ServiceLocationGroup", "")
                Else
                    cmd.Parameters.AddWithValue("@ServiceLocationGroup", r("ServiceLocationGroup").ToString.ToUpper)
                End If
                cmd.Parameters.AddWithValue("@PersonGroupD", r("PersonGroupD").ToString.ToUpper)
                cmd.Parameters.AddWithValue("@ContractGroup", r("ContractGroup").ToString.ToUpper)
                cmd.Parameters.AddWithValue("@ServiceName", r("ServiceName").ToString.ToUpper)

                If IsDBNull(r("Comments")) Then
                    cmd.Parameters.AddWithValue("@Comments", "")
                Else
                    cmd.Parameters.AddWithValue("@Comments", r("Comments").ToString.ToUpper)
                End If
                If IsDBNull(r("Description")) Then
                    cmd.Parameters.AddWithValue("@Description", "")
                Else
                    cmd.Parameters.AddWithValue("@Description", r("Description").ToString.ToUpper)
                End If

                If IsDBNull(r("Address1")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + LocationID + "(STREET ADDRESS1_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "STREET ADDRESS1 BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@Address1", r("Address1").ToString.ToUpper)
                End If
                If IsDBNull(r("AddStreet")) Then
                    cmd.Parameters.AddWithValue("@AddStreet", "")
                Else
                    cmd.Parameters.AddWithValue("@AddStreet", r("AddStreet").ToString.ToUpper)
                End If
                If IsDBNull(r("AddBuilding")) Then
                    cmd.Parameters.AddWithValue("@AddBuilding", "")
                Else
                    cmd.Parameters.AddWithValue("@AddBuilding", r("AddBuilding").ToString.ToUpper)
                End If
                If IsDBNull(r("AddCity")) Then
                    cmd.Parameters.AddWithValue("@AddCity", "")
                Else
                    cmd.Parameters.AddWithValue("@AddCity", r("AddCity").ToString.ToUpper)
                End If

                If IsDBNull(r("AddState")) Then

                    cmd.Parameters.AddWithValue("@AddState", "")
                Else
                    cmd.Parameters.AddWithValue("@AddState", r("AddState").ToString.ToUpper)

                End If

                If IsDBNull(r("AddCountry")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(SERVICEADDRESS-COUNTRY_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "SERVICEADDRESS-COUNTRY_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@AddCountry", r("AddCountry").ToString.ToUpper)
                End If

                If IsDBNull(r("AddPostal")) Then
                    cmd.Parameters.AddWithValue("@AddPostal", "")
                Else
                    cmd.Parameters.AddWithValue("@AddPostal", r("AddPostal").ToString.ToUpper)
                End If

                If IsDBNull(r("Zone")) Then
                    cmd.Parameters.AddWithValue("@LocateGRP", "")
                Else
                    cmd.Parameters.AddWithValue("@LocateGRP", r("Zone").ToString.ToUpper)
                End If

                cmd.Parameters.AddWithValue("@Industry", r("Industry").ToString.ToUpper)

                If IsDBNull(r("MarketSegmentID")) Then
                    cmd.Parameters.AddWithValue("@MarketSegmentID", "")
                Else
                    cmd.Parameters.AddWithValue("@MarketSegmentID", r("MarketSegmentID").ToString.ToUpper)
                End If
                If IsDBNull(r("InChargeID")) Then
                    cmd.Parameters.AddWithValue("@InChargeIDSvc", "")
                Else
                    cmd.Parameters.AddWithValue("@InChargeIDSvc", r("InChargeID").ToString.ToUpper)
                End If
                cmd.Parameters.AddWithValue("@SalesManSvc", r("SalesMan").ToString.ToUpper)

                If IsDBNull(r("CreditTerms")) Then
                    cmd.Parameters.AddWithValue("@ARTermSvc", "")
                Else
                    cmd.Parameters.AddWithValue("@ARTermSvc", r("CreditTerms").ToString.ToUpper)
                End If

                If IsDBNull(r("ContactPerson")) Then
                    cmd.Parameters.AddWithValue("@ContactPerson", "")
                Else
                    cmd.Parameters.AddWithValue("@ContactPerson", r("ContactPerson").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact1Position")) Then
                    cmd.Parameters.AddWithValue("@Contact1Position", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact1Position", r("Contact1Position").ToString.ToUpper)
                End If

                If IsDBNull(r("Telephone")) Then
                    cmd.Parameters.AddWithValue("@Telephone", "")
                Else
                    cmd.Parameters.AddWithValue("@Telephone", r("Telephone").ToString.ToUpper)
                End If

                If IsDBNull(r("Fax")) Then
                    cmd.Parameters.AddWithValue("@Fax", "")
                Else
                    cmd.Parameters.AddWithValue("@Fax", r("Fax").ToString.ToUpper)
                End If

                If IsDBNull(r("Telephone2")) Then
                    cmd.Parameters.AddWithValue("@Tel2", "")
                Else
                    cmd.Parameters.AddWithValue("@Tel2", r("Telephone2").ToString.ToUpper)
                End If

                If IsDBNull(r("Mobile")) Then
                    cmd.Parameters.AddWithValue("@Mobile", "")
                Else
                    cmd.Parameters.AddWithValue("@Mobile", r("Mobile").ToString.ToUpper)
                End If

                If IsDBNull(r("Email")) Then
                    cmd.Parameters.AddWithValue("@Email", "")
                Else
                    cmd.Parameters.AddWithValue("@Email", r("Email").ToString.ToUpper)
                End If

                If IsDBNull(r("ContactPerson2")) Then
                    cmd.Parameters.AddWithValue("@ContactPerson2", "")
                Else
                    cmd.Parameters.AddWithValue("@ContactPerson2", r("ContactPerson2").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Position")) Then
                    cmd.Parameters.AddWithValue("@Contact2Position", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Position", r("Contact2Position").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Tel")) Then
                    cmd.Parameters.AddWithValue("@Contact2Tel", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Tel", r("Contact2Tel").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Fax")) Then
                    cmd.Parameters.AddWithValue("@Contact2Fax", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Fax", r("Contact2Fax").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Tel2")) Then
                    cmd.Parameters.AddWithValue("@Contact2Tel2", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Tel2", r("Contact2Tel2").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Mobile")) Then
                    cmd.Parameters.AddWithValue("@Contact2Mobile", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Mobile", r("Contact2Mobile").ToString.ToUpper)
                End If

                If IsDBNull(r("Contact2Email")) Then
                    cmd.Parameters.AddWithValue("@Contact2Email", "")
                Else
                    cmd.Parameters.AddWithValue("@Contact2Email", r("Contact2Email").ToString.ToUpper)
                End If

                If IsDBNull(r("ServiceEmailCC")) Then
                    cmd.Parameters.AddWithValue("@ServiceEmailCC", "")
                Else
                    cmd.Parameters.AddWithValue("@ServiceEmailCC", r("ServiceEmailCC").ToString.ToUpper)
                End If

                If IsDBNull(r("EmailServiceNotificationOnly")) Then
                    cmd.Parameters.AddWithValue("@EmailServiceNotificationOnly", 0)
                ElseIf r("EmailServiceNotificationOnly").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@EmailServiceNotificationOnly", 1)
                ElseIf r("EmailServiceNotificationOnly").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@EmailServiceNotificationOnly", 0)
                Else
                    cmd.Parameters.AddWithValue("@EmailServiceNotificationOnly", 0)
                End If

                If IsDBNull(r("BillingNameSvc")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + AccountID + "(BILLINGNAME_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "BILLINGNAME_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    cmd.Parameters.AddWithValue("@BillingNameSvc", r("BillingNameSvc").ToString.ToUpper)
                End If

                Dim CheckServiceAddress As Boolean = False

                cmd.Parameters.AddWithValue("@ServiceAddress", 0)

                If IsDBNull(r("ServiceAddress")) Then
                    ' cmd.Parameters.AddWithValue("@ServiceAddress", 0)
                    CheckServiceAddress = False
                ElseIf r("ServiceAddress").ToString.ToUpper = "TRUE" Then
                    '  cmd.Parameters.AddWithValue("@ServiceAddress", 1)
                    CheckServiceAddress = True
                ElseIf r("ServiceAddress").ToString.ToUpper = "FALSE" Then
                    ' cmd.Parameters.AddWithValue("@ServiceAddress", 0)
                    CheckServiceAddress = False
                Else
                    ' cmd.Parameters.AddWithValue("@ServiceAddress", 0)
                    CheckServiceAddress = False
                End If

                'If Billing Address is same as Service Address - set to True, then copy all Service Contact Info to Billing Contact Info

                If CheckServiceAddress = True Then

                    If IsDBNull(r("BillAddressSvc")) Then
                        If IsDBNull(r("Address1")) Then
                            cmd.Parameters.AddWithValue("@BillAddressSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillAddressSvc", r("Address1").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillAddressSvc", r("BillAddressSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillStreetSvc")) Then
                        If IsDBNull(r("AddStreet")) Then
                            cmd.Parameters.AddWithValue("@BillStreetSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillStreetSvc", r("AddStreet").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillStreetSvc", r("BillStreetSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillbuildingSvc")) Then
                        If IsDBNull(r("AddBuilding")) Then
                            cmd.Parameters.AddWithValue("@BillBuildingSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillBuildingSvc", r("AddBuilding").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillbuildingSvc", r("BillbuildingSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCitySvc")) Then
                        If IsDBNull(r("AddCity")) Then
                            cmd.Parameters.AddWithValue("@BillCitySvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillCitySvc", r("AddCity").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillCitySvc", r("BillCitySvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillStateSvc")) Then
                        If IsDBNull(r("AddState")) Then
                            cmd.Parameters.AddWithValue("@BillStateSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillStateSvc", r("AddState").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillStateSvc", r("BillStateSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCountrySvc")) Then
                        If IsDBNull(r("AddCountry")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS-COUNTRY_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLINGADDRESS-COUNTRY_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillCountrySvc", r("AddCountry").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillCountrySvc", r("BillCountrySvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPostalSvc")) Then
                        If IsDBNull(r("AddPostal")) Then
                            cmd.Parameters.AddWithValue("@BillPostalSvc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPostalSvc", r("AddPostal").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillPostalSvc", r("BillPostalSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact1Svc")) Then

                        If IsDBNull(r("ContactPerson")) Then
                            Failure = Failure + 1
                            FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                            drow("Status") = "Failed"
                            drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                            dtLog.Rows.Add(drow)
                            Continue For
                        Else
                            cmd.Parameters.AddWithValue("@BillContact1Svc", r("ContactPerson").ToString.ToUpper)
                        End If

                    Else
                        cmd.Parameters.AddWithValue("@BillContact1Svc", r("BillContact1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPosition1Svc")) Then
                        If IsDBNull(r("Contact1Position")) Then
                            cmd.Parameters.AddWithValue("@BillPosition1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPosition1Svc", r("Contact1Position").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillPosition1Svc", r("BillContact1Position").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone1Svc")) Then
                        If IsDBNull(r("Telephone")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone1Svc", r("Telephone").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone1Svc", r("BillTelephone1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax1Svc")) Then
                        If IsDBNull(r("Fax")) Then
                            cmd.Parameters.AddWithValue("@BillFax1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillFax1Svc", r("Fax").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillFax1Svc", r("BillFax1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone12Svc")) Then
                        If IsDBNull(r("Telephone2")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone12Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone12Svc", r("Telephone2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone12Svc", r("BillTelephone12Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillMobile1Svc")) Then
                        If IsDBNull(r("Mobile")) Then
                            cmd.Parameters.AddWithValue("@BillMobile1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillMobile1Svc", r("Mobile").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillMobile1Svc", r("BillMobile1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillEmail1Svc")) Then
                        If IsDBNull(r("Email")) Then
                            cmd.Parameters.AddWithValue("@BillEmail1Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillEmail1Svc", r("Email").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail1Svc", r("BillEmail1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillContact2Svc")) Then
                        If IsDBNull(r("ContactPerson2")) Then
                            cmd.Parameters.AddWithValue("@BillContact2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillContact2Svc", r("ContactPerson2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Svc", r("BillContact2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPosition2Svc")) Then
                        If IsDBNull(r("Contact2Position")) Then
                            cmd.Parameters.AddWithValue("@BillPosition2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillPosition2Svc", r("Contact2Position").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillPosition2Svc", r("BillContact1Position").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone2Svc")) Then
                        If IsDBNull(r("Contact2Tel")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone2Svc", r("Contact2Tel").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone2Svc", r("BillTelephone2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax2Svc")) Then
                        If IsDBNull(r("Contact2Fax")) Then
                            cmd.Parameters.AddWithValue("@BillFax2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillFax2Svc", r("Contact2Fax").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillFax2Svc", r("BillFax2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone22Svc")) Then
                        If IsDBNull(r("Contact2Tel2")) Then
                            cmd.Parameters.AddWithValue("@BillTelephone22Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillTelephone22Svc", r("Contact2Tel2").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone22Svc", r("BillTelephone22Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillMobile2Svc")) Then
                        If IsDBNull(r("Contact2Mobile")) Then
                            cmd.Parameters.AddWithValue("@BillMobile2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillMobile2Svc", r("Contact2Mobile").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillMobile2Svc", r("BillMobile2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillEmail2Svc")) Then
                        If IsDBNull(r("Contact2Email")) Then
                            cmd.Parameters.AddWithValue("@BillEmail2Svc", "")
                        Else
                            cmd.Parameters.AddWithValue("@BillEmail2Svc", r("Contact2Email").ToString.ToUpper)
                        End If
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail2Svc", r("BillEmail2Svc").ToString.ToUpper)
                    End If

                Else
                    If IsDBNull(r("BillAddressSvc")) Then
                        cmd.Parameters.AddWithValue("@BillAddressSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillAddressSvc", r("BillAddressSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillStreetSvc")) Then
                        cmd.Parameters.AddWithValue("@BillStreetSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillStreetSvc", r("BillStreetSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillbuildingSvc")) Then
                        cmd.Parameters.AddWithValue("@BillbuildingSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillbuildingSvc", r("BillbuildingSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCitySvc")) Then
                        cmd.Parameters.AddWithValue("@BillCitySvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillCitySvc", r("BillCitySvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillStateSvc")) Then
                        cmd.Parameters.AddWithValue("@BillStateSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillStateSvc", r("BillStateSvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillCountrySvc")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLINGADDRESS-COUNTRY_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLINGADDRESS-COUNTRY_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillCountrySvc", r("BillCountrySvc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPostalSvc")) Then
                        cmd.Parameters.AddWithValue("@BillPostalSvc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillPostalSvc", r("BillPostalSvc").ToString.ToUpper)
                    End If


                    If IsDBNull(r("BillContact1Svc")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + AccountID + "(BILLING-CONTACTPERSON_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "BILLING-CONTACTPERSON_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        cmd.Parameters.AddWithValue("@BillContact1Svc", r("BillContact1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPosition1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillPosition1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillPosition1Svc", r("BillPosition1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillTelephone1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone1Svc", r("BillTelephone1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillFax1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillFax1Svc", r("BillFax1Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone12Svc")) Then
                        cmd.Parameters.AddWithValue("@BillTelephone12Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone12Svc", r("BillTelephone12Svc").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillMobile1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillMobile1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillMobile1Svc", r("BillMobile1Svc").ToString.ToUpper)
                    End If
                    If IsDBNull(r("BillEmail1Svc")) Then
                        cmd.Parameters.AddWithValue("@BillEmail1Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail1Svc", r("BillEmail1Svc").ToString.ToUpper)
                    End If


                    If IsDBNull(r("BillContact2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillContact2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillContact2Svc", r("BillContact2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillPosition2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillPosition2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillPosition2Svc", r("BillPosition2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillTelephone2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone2Svc", r("BillTelephone2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillFax2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillFax2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillFax2Svc", r("BillFax2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillTelephone22Svc")) Then
                        cmd.Parameters.AddWithValue("@BillTelephone22Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillTelephone22Svc", r("BillTelephone22Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillMobile2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillMobile2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillMobile2Svc", r("BillMobile2Svc").ToString.ToUpper)
                    End If

                    If IsDBNull(r("BillEmail2Svc")) Then
                        cmd.Parameters.AddWithValue("@BillEmail2Svc", "")
                    Else
                        cmd.Parameters.AddWithValue("@BillEmail2Svc", r("BillEmail2Svc").ToString.ToUpper)
                    End If

                End If

                If IsDBNull(r("SendServiceReportTo1")) Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo1", "N")
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo1", "Y")
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo1", "N")
                Else
                    cmd.Parameters.AddWithValue("@SendServiceReportTo1", "N")
                End If

                If IsDBNull(r("SendServiceReportTo2")) Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo2", "N")
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo2", "Y")
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@SendServiceReportTo2", "N")
                Else
                    cmd.Parameters.AddWithValue("@SendServiceReportTo2", "N")
                End If

                If IsDBNull(r("DefaultInvoiceFormat")) Then
                    cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", "Format1")
                Else
                    cmd.Parameters.AddWithValue("@DefaultInvoiceFormat", r("DefaultInvoiceFormat"))
                End If

                If IsDBNull(r("SmartCustomer")) Then
                    cmd.Parameters.AddWithValue("@SmartCustomer", 0)
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "TRUE" Then
                    cmd.Parameters.AddWithValue("@SmartCustomer", 1)
                ElseIf r("SendServiceReportTo1").ToString.ToUpper = "FALSE" Then
                    cmd.Parameters.AddWithValue("@SmartCustomer", 0)
                Else
                    cmd.Parameters.AddWithValue("@SmartCustomer", 0)
                End If

                cmd.Parameters.AddWithValue("@BusinessHoursStart", "")

                cmd.Parameters.AddWithValue("@BusinessHoursEnd", "")

              
                cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text + "_IMPORT")
                cmd.Parameters.AddWithValue("@LastModifiedBy", txtCreatedBy.Text + "_IMPORT")

                'If IsDBNull(r("CreatedBy")) Then
                '    cmd.Parameters.AddWithValue("@CreatedBy", "EXCELIMPORT")

                'Else
                '    cmd.Parameters.AddWithValue("@CreatedBy", r("CreatedBy"))

                'End If

                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                'If IsDBNull(r("CreatedBy")) Then
                '    cmd.Parameters.AddWithValue("@LastModifiedBy", "EXCELIMPORT")

                'Else
                '    cmd.Parameters.AddWithValue("@LastModifiedBy", r("CreatedBy"))

                'End If
                cmd.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))



                cmd.Parameters.AddWithValue("@AddBlock", "")
                cmd.Parameters.AddWithValue("@AddNos", "")
                cmd.Parameters.AddWithValue("@AddFloor", "")
                cmd.Parameters.AddWithValue("@AddUnit", "")
                cmd.Parameters.AddWithValue("@LocationPrefix", "")

                cmd.Parameters.AddWithValue("@BranchID", "")

                Dim lastnum As Int32 = GenerateLocationID(AccountID, conn)
                If lastnum = 0 Then
                    drow("Status") = "Failed"
                    drow("Remarks") = "CANNOT GENERATE LOCATIONID"
                    dtLog.Rows.Add(drow)
                    Continue For
                End If
                LocationID = AccountID + "-" + lastnum.ToString("D4")
                '  lblAlert.Text = LocationID

                drow("LocationID") = LocationID

                cmd.Parameters.AddWithValue("@LocationNo", lastnum)

                cmd.Parameters.AddWithValue("@LocationID", LocationID.ToUpper)

                cmd.Connection = conn

                cmd.ExecuteNonQuery()
                '    cmd.Dispose()

                Success = Success + 1
                drow("Status") = "Success"
                drow("Remarks") = ""
                dtLog.Rows.Add(drow)
            End If

        Next



        txtSuccessCount.Text = Success.ToString
        txtFailureCount.Text = Failure.ToString
        txtFailureString.Text = FailureString

        GridView1.DataSource = dtLog
        GridView1.DataBind()

        dt.Clear()


        Return True
        'Catch ex As Exception
        '    txtSuccessCount.Text = Success.ToString
        '    txtFailureCount.Text = Failure.ToString
        '    txtFailureString.Text = FailureString
        '    lblAlert.Text = ex.Message.ToString


        '    InsertIntoTblWebEventLog("InsertCorporateLocationData", ex.Message.ToString, txtCreatedBy.Text)

        '    Return False

        'End Try
    End Function

    Private Function Validation(AccountID As String, Name As String) As Boolean
        If String.IsNullOrEmpty(Name.Trim) = True Then
            ' MessageBox.Message.Alert(Page, "Name cannot be blank!!!", "str")
            lblAlert.Text = "NAME CANNOT BE BLANK"



        End If
        Return True
    End Function

    Protected Sub btnCorporateTemplate_Click(sender As Object, e As ImageClickEventArgs) Handles btnCorporateTemplate.Click
        Try
            Dim filePath As String = ""
            If rdbModule.SelectedValue.ToString = "Corporate" Then
                filePath = "Corporate_ExcelTemplate.xlsx"
            ElseIf rdbModule.SelectedValue.ToString = "Residential" Then
                filePath = "Residential_ExcelTemplate.xlsx"
            ElseIf rdbModule.SelectedValue.ToString = "CorporateLocation" Then
                filePath = "CorporateLocation_ExcelTemplate.xlsx"
            ElseIf rdbModule.SelectedValue.ToString = "ResidentialLocation" Then
                filePath = "ResidentialLocation_ExcelTemplate.xlsx"
            End If

            filePath = Server.MapPath("~/Uploads/Excel/ExcelTemplates/") + filePath
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            InsertIntoTblWebEventLog("Template", ex.Message.ToString, rdbModule.SelectedValue.ToString)
        End Try
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Try

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.ClearHeaders()
            Response.Charset = ""
            Dim FileName As String = "ExcelImport_Log_" & DateTime.Now & ".xls"
            Dim strwritter As StringWriter = New StringWriter()
            Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
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

    Private Function CheckIndustryExists(Industry As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT Industry FROM tblIndustry where Industry =@Industry)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Industry", Industry)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckCityExists(City As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT city FROM tblcity where city =@city)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@city", City)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckStateExists(State As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT State FROM tblState where State =@State)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@State", State)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckCountryExists(Country As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT Country FROM tblCountry where Country =@Country)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Country", Country)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckTermsExists(Terms As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT Terms FROM tblTerms where Terms =@Terms)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Terms", Terms)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckCurrencyExists(Currency As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT Currency FROM tblCurrency where Currency =@Currency)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Currency", Currency)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckCompanyGroupExists(CompanyGroup As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT CompanyGroup FROM tblCompanyGroup where CompanyGroup =@CompanyGroup)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@CompanyGroup", CompanyGroup)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckContractGroupExists(ContractGroup As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT ContractGroup FROM tblContractGroup where ContractGroup =@ContractGroup)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@ContractGroup", ContractGroup)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckLocationGroupExists(LocationGroup As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT LocationGroup FROM tblLocationGroup where LocationGroup =@LocationGroup)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@LocationGroup", LocationGroup)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckInchargeIDExists(InchargeID As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT distinct (StaffID) FROM tblStaff where Roles='TECHNICAL' and Status = 'O' AND STAFFID=@InchargeID)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@InchargeID", InchargeID)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckSalesmanExists(Salesman As String, conn As MySqlConnection) As Boolean
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT EXISTS(SELECT distinct (StaffID) FROM tblStaff where Roles='SALES MAN' and Status = 'O' AND STAFFID=@Salesman)"
        cmd.Connection = conn
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@Salesman", Salesman)
        Return cmd.ExecuteScalar
    End Function

    Private Function CheckInvoiceFormatExists(InvoiceFormat As String, conn As MySqlConnection) As Boolean
        Select Case InvoiceFormat
            Case "Format1"
                Return True
            Case "Format2"
                Return True
            Case "Format3"
                Return True
            Case "Format4"
                Return True
            Case "Format5"
                Return True
            Case "Format6"
                Return True
            Case "Format7"
                Return True
            Case "Format8"
                Return True
            Case Else
                Return False
        End Select

    End Function

    Private Function CheckNationalityExists(InvoiceFormat As String, conn As MySqlConnection) As Boolean
        Select Case InvoiceFormat
            Case "SINGAPOREAN"
                Return True
            Case "MALAYSIAN"
                Return True
            Case "INDIAN"
                Return True
            Case "INDONESIAN"
                Return True
            Case "CHINESE"
                Return True
            Case "PHILIPPINE"
                Return True
            Case "SOUTH KOREAN"
                Return True
            Case "OTHERS"
                Return True
            Case Else
                Return False
        End Select

    End Function

    Private Function CheckCorporateLocationDuplicate(conn As MySqlConnection, AccountID As String, addr As String, ContractGroup As String, CompanyGroup As String) As Boolean

        Dim commandLoc As MySqlCommand = New MySqlCommand

        commandLoc.CommandType = CommandType.Text

        commandLoc.CommandText = "SELECT * FROM tblcompanylocation where accountid=@id"
        commandLoc.Parameters.AddWithValue("@id", AccountID)
        commandLoc.Connection = conn

        Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
        Dim dtLoc As New System.Data.DataTable
        dtLoc.Load(drLoc)

        '   Dim addr As String = r("Address1").ToString.Trim + " " + r("AddStreet").ToString.Trim + " " + r("AddBuilding").ToString.Trim
        Dim dataaddr As String
        If dtLoc.Rows.Count > 0 Then
            For i As Integer = 0 To dtLoc.Rows.Count - 1

                dataaddr = dtLoc.Rows(i)("Address1").ToString.Trim + " " + dtLoc.Rows(i)("AddStreet").ToString.Trim + " " + dtLoc.Rows(i)("AddBuilding").ToString.Trim

                If addr = dataaddr And dtLoc.Rows(i)("ContractGroup").ToString.Trim = ContractGroup And dtLoc.Rows(i)("CompanyGroupD").ToString.Trim = CompanyGroup Then
                    Return False
                End If
            Next

        End If
        Return True
    End Function

    Private Function CheckResidentialLocationDuplicate(conn As MySqlConnection, AccountID As String, addr As String, ContractGroup As String, PersonGroup As String) As Boolean

        Dim commandLoc As MySqlCommand = New MySqlCommand

        commandLoc.CommandType = CommandType.Text

        commandLoc.CommandText = "SELECT * FROM tblpersonlocation where accountid=@id"
        commandLoc.Parameters.AddWithValue("@id", AccountID)
        commandLoc.Connection = conn

        Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
        Dim dtLoc As New System.Data.DataTable
        dtLoc.Load(drLoc)

        '   Dim addr As String = r("Address1").ToString.Trim + " " + r("AddStreet").ToString.Trim + " " + r("AddBuilding").ToString.Trim
        Dim dataaddr As String
        If dtLoc.Rows.Count > 0 Then
            For i As Integer = 0 To dtLoc.Rows.Count - 1

                dataaddr = dtLoc.Rows(i)("Address1").ToString.Trim + " " + dtLoc.Rows(i)("AddStreet").ToString.Trim + " " + dtLoc.Rows(i)("AddBuilding").ToString.Trim

                If addr = dataaddr And dtLoc.Rows(i)("ContractGroup").ToString.Trim = ContractGroup And dtLoc.Rows(i)("PersonGroupD").ToString.Trim = PersonGroup Then
                    Return False
                End If
            Next

        End If
        Return True
    End Function

    Protected Function GenerateLocationID(AccountID As String, conn As MySqlConnection) As Int32
        Dim lastnum As Int32 = 0

        Try
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT locationid,locationprefix,locationno FROM tblpersonlocation where accountid='" & AccountID & "' order by locationno desc;"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                lastnum = Convert.ToInt64(dt.Rows(0)("locationno"))
                lastnum = lastnum + 1
                Return lastnum
            Else
                lastnum = 1
                Return lastnum
            End If

        Catch ex As Exception
            Return lastnum

            InsertIntoTblWebEventLog("GenerateLocationID", ex.Message.ToString, AccountID)

        End Try
    End Function


End Class
