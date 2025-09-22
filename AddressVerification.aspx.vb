Imports Microsoft.VisualBasic
Imports System
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Threading
Imports System.Threading.Tasks

Partial Class AddressVerification
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            lblMismatchedRecords.Visible = False

        End If

    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            Dim sfilename As String = ""
            sfilename = Path.GetFileName(fileUpload.PostedFile.FileName)

            Dim folderPath As String = Server.MapPath("~/Uploads/AddressVerification/")
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If
            Dim fileName As String = folderPath + sfilename
            If System.IO.File.Exists(fileName) Then

                System.IO.File.Delete(fileName)
            End If
            'Save the File to the Directory (Folder).
            fileUpload.PostedFile.SaveAs(fileName)

            txtWorkbookName.Text = sfilename
            Dim file As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim dropdownworkbook = New XSSFWorkbook(file)
            file.Close()
            file.Dispose()


            Session("Filename") = fileName

            Dim sheetList As New List(Of sheetNameList)

            For index = 0 To (dropdownworkbook.NumberOfSheets - 1)
                Dim sheet As New sheetNameList
                sheet.sheetName = dropdownworkbook(index).SheetName
                sheetList.Add(sheet)
            Next

            ddlSheet.DataSource = sheetList
            ddlSheet.DataTextField = "sheetName"
            ddlSheet.DataValueField = "sheetName"
            ddlSheet.DataBind()


            'hssfwb = Nothing
            'hssfwb.Dispose()
            dropdownworkbook = Nothing
            'dropdownworkbook.Dispose()
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub UnLoad()

        ddlSheet.Items.Clear()
        ddlSheet.DataSource = Nothing
        ddlSheet.DataBind()

        GridAddressdetails.DataSource = Nothing
        GridAddressdetails.DataBind()

        gridresults.DataSource = Nothing
        gridresults.DataBind()

        GridmismatchedRecords.DataSource = Nothing
        GridmismatchedRecords.DataBind()

    End Sub



    Protected Sub btnVerify_Click(sender As Object, e As EventArgs)
        Dim dtAddress As New DataTable
        dtAddress = readExcelData(txtAddress.Text, txtPostalCode.Text, txtRowStart.Text, txtRowEnd.Text)
        If dtAddress Is Nothing Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "warningpopup();", True)
        Else
            If dtAddress.Rows.Count > 0 Then
                'GridAddressdetails.DataSource = dtAddress
                'GridAddressdetails.DataBind()
                Dim drAddress() As DataRow
                drAddress = dtAddress.Select("Address = '' OR Address IS NULL")
                Dim drpostalCode() As DataRow
                drpostalCode = dtAddress.Select("PostalCode = '' OR  PostalCode='-' OR  PostalCode IS NULL")

                Dim dtverify As New DataTable
                dtverify.Columns.Add("FileResults")
                dtverify.Columns.Add("Total")
                dtverify.Columns.Add("Processed")
                dtverify.Columns.Add("Skipped")

                Dim drverifyAddress As DataRow
                drverifyAddress = dtverify.NewRow()

                drverifyAddress("FileResults") = "Street Address Records"
                drverifyAddress("Total") = dtAddress.Rows.Count
                drverifyAddress("Processed") = dtAddress.Rows.Count
                drverifyAddress("Skipped") = drAddress.Count
                dtverify.Rows.Add(drverifyAddress)

                Dim drverifyPostal As DataRow
                drverifyPostal = dtverify.NewRow()

                drverifyPostal("FileResults") = "Postal Code Records"
                drverifyPostal("Total") = dtAddress.Rows.Count
                drverifyPostal("Processed") = dtAddress.Rows.Count
                drverifyPostal("Skipped") = drpostalCode.Count
                dtverify.Rows.Add(drverifyPostal)

                gridresults.DataSource = dtverify
                gridresults.DataBind()


                Dim dtAddressdetails = dtAddress.AsEnumerable().[Select](Function(row) New With { _
                Key .Address = row.Field(Of String)("Address"), _
                Key .PostalCode = row.Field(Of String)("PostalCode"), _
                Key .AddressAndPostalCellName = row.Field(Of String)("AddressAndPostalCellName") _
               }).Distinct()

                Dim misMatchedRecordsList As New List(Of MisMatchedRecordsFromAPI)

                'Ignore word in Excel address and OneMap address start
                Dim ignoreWordList As New List(Of IgnoreSearchWords)

                Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                Using con As New MySqlConnection(constr)
                    Using cmd As New MySqlCommand()
                        cmd.CommandType = CommandType.Text

                        Dim insQuery1 As String = "SELECT * FROM tblIgnoreWords"
                        cmd.CommandText = insQuery1
                        cmd.Connection = con
                        con.Open()
                        Dim reader As MySqlDataReader = cmd.ExecuteReader()

                        Do While reader.Read()
                            Dim IgnoreSearchWords As New IgnoreSearchWords
                            IgnoreSearchWords.IgnoreWordID = CType(reader("IgnoreWordsID"), String)
                            IgnoreSearchWords.IgnoreWord = reader("IgnoreWords")
                            ignoreWordList.Add(IgnoreSearchWords)
                        Loop
                        con.Close()
                    End Using
                End Using

                'Equivalent word in Excel address and OneMap address start
                Dim equivalentWordList As New List(Of EquivalentSearchWords)
                Using con As New MySqlConnection(constr)
                    Using cmd As New MySqlCommand()
                        cmd.CommandType = CommandType.Text

                        Dim insQuery1 As String = "SELECT * FROM tblequivalentwords"
                        cmd.CommandText = insQuery1
                        cmd.Connection = con
                        con.Open()
                        Dim reader As MySqlDataReader = cmd.ExecuteReader()

                        Do While reader.Read()
                            Dim EquivalentSearchWords As New EquivalentSearchWords
                            EquivalentSearchWords.Word = reader("Word")
                            EquivalentSearchWords.EquivalentWord = reader("EquivalentWord")
                            equivalentWordList.Add(EquivalentSearchWords)
                        Loop
                        con.Close()
                    End Using
                End Using

                For i As Integer = 0 To dtAddressdetails.Count - 1
                    Dim client As New WebClient
                    Dim results As String
                    If dtAddressdetails(i).PostalCode <> "" Then

                        Dim postalCode As String = dtAddressdetails(i).PostalCode.TrimStart(New Char() {"0"c})
                        'Dim url As String = " https://developers.onemap.sg/commonapi/search?searchVal=" + postalCode + "&returnGeom=N&getAddrDetails=Y"
                        Dim url As String = " https://www.onemap.gov.sg/api/common/elastic/search?searchVal=" + postalCode + "&returnGeom=N&getAddrDetails=Y"
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                        results = client.DownloadString(url)

                        Dim apiGetAddressAndPostalCode As List(Of AddressAndPostalCodesFromAPI) = New List(Of AddressAndPostalCodesFromAPI)()
                        Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(results)
                        Dim resultFromAPI As String

                        resultFromAPI = jObject("results").ToString()

                        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                        apiGetAddressAndPostalCode = serializer.Deserialize(Of List(Of AddressAndPostalCodesFromAPI))(resultFromAPI)
                        Dim addressIsExist As Boolean
                        Dim addressRetrievedFromAPI As String
                        Dim searchAddressInExcel As String = ""

                        If apiGetAddressAndPostalCode.Count > 0 Then

                            'Address in Excel


                            searchAddressInExcel = dtAddressdetails(i).Address
                            searchAddressInExcel = dtAddressdetails(i).Address.TrimStart(New Char() {"0"c})


                            Dim addressRetrievedFromAPIObj As New AddressAndPostalCodesFromAPI
                            addressRetrievedFromAPIObj = apiGetAddressAndPostalCode.Where(Function(x) x.POSTAL = postalCode).FirstOrDefault()
                            ' Address in One Map.
                            If Not (addressRetrievedFromAPIObj Is Nothing) Then
                                addressRetrievedFromAPI = addressRetrievedFromAPIObj.ADDRESS
                            Else
                                addressRetrievedFromAPI = ""
                            End If


                            ' ignoreWordList start
                            Dim searchAddressInExcel2 As String = ""
                            If ignoreWordList.Count > 0 Then
                                
                                Dim array2 As String() = searchAddressInExcel.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                For Each sword As String In array2
                                    For Each word As IgnoreSearchWords In ignoreWordList
                                        If sword.ToUpper() = word.IgnoreWord.ToUpper() Then
                                            sword = ""
                                            Exit For
                                        End If
                                    Next
                                    searchAddressInExcel2 = searchAddressInExcel2 & sword & " "

                                Next
                            Else
                                searchAddressInExcel2 = searchAddressInExcel
                            End If
                            searchAddressInExcel = searchAddressInExcel2
                            ' ignoreWordList end

                            ' equivalentWordList start
                            Dim searchAddressInExcel1 As String = ""
                            If equivalentWordList.Count > 0 Then

                                Dim array1 As String() = searchAddressInExcel.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                                For Each sword As String In array1

                                    For Each equiword As EquivalentSearchWords In equivalentWordList
                                        If sword.ToUpper() = equiword.Word.ToUpper() Then
                                            sword = equiword.EquivalentWord
                                            Exit For
                                        End If
                                    Next
                                    searchAddressInExcel1 = searchAddressInExcel1 & sword & " "
                                Next
                            Else
                                searchAddressInExcel1 = searchAddressInExcel
                            End If
                            searchAddressInExcel = searchAddressInExcel1
                            'equivalentWordList end

                            'Check the address are same and if not same inserting a record in mismatched records list
                            If Not (addressRetrievedFromAPI.ToUpper().Contains((searchAddressInExcel.Trim().ToUpper()))) Then

                                Dim misMatchedRecord As New MisMatchedRecordsFromAPI
                                misMatchedRecord.Columns = dtAddressdetails(i).AddressAndPostalCellName
                                misMatchedRecord.PostalCode = dtAddressdetails(i).PostalCode.TrimStart(New Char() {"0"c})
                                misMatchedRecord.StreetAddress = dtAddressdetails(i).Address
                                misMatchedRecord.OneMapAdddressRetrieved = If(Not (addressRetrievedFromAPIObj Is Nothing), addressRetrievedFromAPIObj.ADDRESS, "")
                                misMatchedRecordsList.Add(misMatchedRecord)

                            End If
                        End If


                    End If
                Next

                If misMatchedRecordsList.Count > 0 Then

                    Dim dtMatched As New DataTable

                    dtMatched.Columns.Add("Columns")
                    dtMatched.Columns.Add("StreetAddress")
                    dtMatched.Columns.Add("PostalCode")
                    dtMatched.Columns.Add("OneMapAddressRetrieved")

                    For Each item As MisMatchedRecordsFromAPI In misMatchedRecordsList

                        Dim row As DataRow
                        row = dtMatched.NewRow()

                        row("Columns") = item.Columns
                        row("StreetAddress") = item.StreetAddress
                        row("PostalCode") = item.PostalCode
                        row("OneMapAddressRetrieved") = item.OneMapAdddressRetrieved
                        dtMatched.Rows.Add(row)

                    Next
                    lblMismatchedRecords.Visible = True
                    GridmismatchedRecords.DataSource = dtMatched
                    GridmismatchedRecords.DataBind()

                End If

            End If
        End If

        fileUpload.Dispose()

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "HideStatus", "stopLoading();", True)

    End Sub

    Private Function readExcelData(ByVal addresscolumn As String, ByVal PostalCodeColumn As String, ByVal startcolumn As String, ByVal endcolumn As String) As DataTable

        Dim sheetName As String
        Dim cntrowtocheck As Integer = 0

        sheetName = ddlSheet.SelectedValue

        Dim fileLocation As String = ""
        If Not Session("Filename") Is Nothing Then
            fileLocation = Session("Filename")
        End If

        Dim dataSet1 As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Dim draddress As DataRow
        Dim dtrowcount As Integer
        dt.Columns.Add("Address")
        dt.Columns.Add("AddressCellRange")
        dt.Columns.Add("PostalCode")
        dt.Columns.Add("AddressCellName")
        dt.Columns.Add("AddressAndPostalCellName")

        Dim dtGridAddressdetails As DataTable = New DataTable()
        Dim drGridAddressdetails As DataRow
        dtGridAddressdetails.Columns.Add("Address")
        dtGridAddressdetails.Columns.Add("PostalCode")

        Try

            Dim file As FileStream = New FileStream(fileLocation, FileMode.Open, FileAccess.Read)
            Dim dropdownworkbook = New XSSFWorkbook(file)
            file.Close()
            file.Dispose()

            'Dim dropdownworkbook = New XSSFWorkbook(fileLocation)
            If Not dropdownworkbook Is Nothing Then


                Dim worksheet = CType(dropdownworkbook.GetSheet(sheetName), ISheet)
                'Dim addressrange = "B14:B68"
                Dim addressrange = ""
                Dim postalcoderange = ""

                If startcolumn = "" And endcolumn = "" Then
                    addressrange = addresscolumn + ":" + addresscolumn.Substring(0, 1) + worksheet.LastRowNum.ToString()
                    postalcoderange = PostalCodeColumn + ":" + PostalCodeColumn.Substring(0, 1) + worksheet.LastRowNum.ToString()

                ElseIf startcolumn = "" And endcolumn <> "" Then
                    addressrange = addresscolumn + ":" + addresscolumn.Substring(0, 1) + endcolumn
                    postalcoderange = PostalCodeColumn + ":" + PostalCodeColumn.Substring(0, 1) + endcolumn

                ElseIf endcolumn = "" And startcolumn <> "" Then
                    addressrange = addresscolumn + startcolumn + ":" + addresscolumn.Substring(0, 1) + worksheet.LastRowNum.ToString()
                    postalcoderange = PostalCodeColumn + startcolumn + ":" + PostalCodeColumn.Substring(0, 1) + worksheet.LastRowNum.ToString()
                Else
                    addressrange = addresscolumn + startcolumn + ":" + addresscolumn.Substring(0, 1) + endcolumn
                    postalcoderange = PostalCodeColumn + startcolumn + ":" + PostalCodeColumn.Substring(0, 1) + endcolumn
                End If


                'Dim addressrange = addresscolumn + ":" + addresscolumn.Substring(0, 1) + worksheet.LastRowNum.ToString()
                'Dim postalcoderange = PostalCodeColumn + ":" + PostalCodeColumn.Substring(0, 1) + worksheet.LastRowNum.ToString()
              

                Dim addresscellrange = CellRangeAddress.ValueOf(addressrange)
                Dim postalcodecellrange = CellRangeAddress.ValueOf(postalcoderange)

                For index = addresscellrange.FirstRow To addresscellrange.LastRow

                    Dim row As IRow = worksheet.GetRow(index)
                    If Not row Is Nothing Then
                        For j = addresscellrange.FirstColumn To addresscellrange.LastColumn
                            Dim cell As ICell = row.GetCell(j)
                            If Not cell Is Nothing Then
                                Dim sAddress As String = GetCellAsString(cell)
                                If sAddress = "" Then
                                    Continue For
                                Else
                                    draddress = dt.NewRow()
                                    draddress("Address") = sAddress
                                    draddress("AddressCellRange") = index
                                    draddress("AddressCellName") = addresscolumn.Substring(0, 1) + (index + 1).ToString
                                    draddress("AddressAndPostalCellName") = ""
                                    dt.Rows.Add(draddress)
                                End If
                            End If
                        Next
                    End If
                Next
                If dt.Rows.Count = 0 Then
                    dt = Nothing
                    Return dt
                Else
                    dtrowcount = 0
                    Do While dtrowcount < dt.Rows.Count
                        For index1 = postalcodecellrange.FirstRow To postalcodecellrange.LastRow
                            Dim row As IRow = worksheet.GetRow(index1)
                            If Not row Is Nothing Then
                                For j = postalcodecellrange.FirstColumn To postalcodecellrange.LastColumn
                                    Dim cell As ICell = row.GetCell(j)
                                    If Not cell Is Nothing Then
                                        Dim sPostalCode As String = ""
                                        Select Case cell.CellType
                                            Case CellType.Blank
                                                sPostalCode = ""
                                            Case CellType.Boolean
                                                sPostalCode = cell.BooleanCellValue.ToString()
                                            Case CellType.[Error]
                                                sPostalCode = cell.ErrorCellValue.ToString()
                                            Case CellType.Formula
                                                sPostalCode = cell.StringCellValue.ToString()
                                            Case CellType.Numeric
                                                sPostalCode = If(DateUtil.IsCellDateFormatted(cell), cell.DateCellValue.ToString("dd/MM/yyyy"), cell.NumericCellValue.ToString("000000"))
                                            Case CellType.String
                                                sPostalCode = cell.StringCellValue.ToString()
                                            Case CellType.Unknown
                                                sPostalCode = "UnKnown"
                                            Case Else
                                                sPostalCode = "Error"
                                        End Select


                                        If dt.Rows(dtrowcount)("AddressCellrange") = index1 Then
                                            'If cntrowtocheck <= 4 Then
                                            '    If Not sPostalCode.Contains("Postal Code") Then
                                            '        If ((Not Regex.IsMatch(sPostalCode, "^[0-9 ]+$")) Or (sPostalCode.Length <> 6)) Then
                                            '            dt = Nothing
                                            '            Return dt
                                            '        End If
                                            '    End If
                                            'End If

                                            dt.Rows(dtrowcount)("PostalCode") = sPostalCode
                                            dt.Rows(dtrowcount)("AddressAndPostalCellName") = dt.Rows(dtrowcount)("AddressCellName") + "," + (PostalCodeColumn.Substring(0, 1) + (index1 + 1).ToString())
                                            dtrowcount = dtrowcount + 1
                                            cntrowtocheck = cntrowtocheck + 1

                                        Else
                                            If sPostalCode = "" Then
                                                Continue For
                                            End If


                                        End If
                                    End If

                                Next
                            End If

                        Next
                    Loop
                End If
                

                worksheet = Nothing
                dropdownworkbook = Nothing
            End If
        Catch ex As Exception

        End Try


        Dim icount As Integer = 0

        For Each row As DataRow In dt.Rows

            If icount > 0 And icount < 6 Then
                drGridAddressdetails = dtGridAddressdetails.NewRow()
                drGridAddressdetails("Address") = row.Item("Address")
                drGridAddressdetails("PostalCode") = row.Item("PostalCode")
                dtGridAddressdetails.Rows.Add(drGridAddressdetails)
            ElseIf (icount = 6) Then
                Exit For
            End If
            icount = icount + 1
        Next row
        Session("dtAddressPostalCode") = dt

        If dtGridAddressdetails.Rows.Count > 0 Then
            GridAddressdetails.DataSource = dtGridAddressdetails
            GridAddressdetails.DataBind()

        End If

        Return dt
    End Function



    Private Function GetCellAsString(ByVal cell As ICell) As String
        Select Case cell.CellType
            Case CellType.Blank
                Return ""
            Case CellType.Boolean
                Return cell.BooleanCellValue.ToString()
            Case CellType.[Error]
                Return cell.ErrorCellValue.ToString()
            Case CellType.Formula
                Return cell.StringCellValue.ToString()
            Case CellType.Numeric
                Return If(DateUtil.IsCellDateFormatted(cell), cell.DateCellValue.ToString("dd/MM/yyyy"), cell.NumericCellValue.ToString())
            Case CellType.String
                Return cell.StringCellValue.ToString()
            Case CellType.Unknown
                Return "UnKnown"
            Case Else
                Return "Error"
        End Select
    End Function


    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        txtAddress.Text = ""
        txtWorkbookName.Text = ""
        txtPostalCode.Text = ""
        fileUpload.Dispose()
        lblMismatchedRecords.Visible = False
        UnLoad()

    End Sub
End Class

Public Class sheetNameList
    Public Property sheetName() As String
End Class
Public Class IgnoreSearchWords
    Public Property IgnoreWordID() As Integer
    Public Property IgnoreWord() As String
End Class
Public Class EquivalentSearchWords
    Public Property Word() As String
    Public Property EquivalentWord() As String
End Class


Public Class AddressAndPostalCodesFromAPI
    Public Property SEARCHVAL() As String
    Public Property BLK_NO() As String
    Public Property ROAD_NAME() As String
    Public Property BUILDING() As String
    Public Property ADDRESS() As String
    Public Property POSTAL() As String
End Class

Public Class MisMatchedRecordsFromAPI
    Public Property Columns() As String
    Public Property StreetAddress() As String
    Public Property PostalCode() As String
    Public Property OneMapAdddressRetrieved() As String
End Class


