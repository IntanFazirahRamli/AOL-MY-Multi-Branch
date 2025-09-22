Imports System.IO
Imports System.Net
Imports System.Text
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class ViewManualReport
    Inherits System.Web.UI.Page

    Public Message As String = String.Empty
    ' To store the Error or Message
    Private Word As Microsoft.Office.Interop.Word.ApplicationClass
    ' The Interop Object for Word
    Private Excel As Microsoft.Office.Interop.Excel.ApplicationClass
    ' The Interop Object for Excel
    Private Unknown As Object = Type.Missing
    ' For passing Empty values
    Public Enum StatusType
        SUCCESS
        FAILED
    End Enum
    ' To Specify Success or Failure Types
    Public Status As StatusType

    Protected Sub PreviewFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            InsertIntoTblWebEventLog("PreviewFile", Convert.ToString(Request.QueryString(0)), Session("UserID"))
             ' Dim filePath As String = Convert.ToString(Session("FileNameLink"))
            TextBox1.Text = Convert.ToString(Session("FileNameLink"))
            '    Dim filepath As String = Request.QueryString(0)
            Dim filepath As String = RetrieveRecord(Convert.ToString(Session("FileNameLink")))

            '   MessageBox.Message.Alert(Page, Convert.ToString(Session("FileNameLink")), "STR")

            If filepath = "" Then
                filepath = Convert.ToString(Session("FileNameLink"))
            End If
            InsertIntoTblWebEventLog("PreviewFile1", filePath, Session("UserID"))

            Dim ext As String = Path.GetExtension(filePath)
            filePath = "Uploads/Service/" + filePath
            '   Response.Write(filePath)
            InsertIntoTblWebEventLog("PreviewFile2", filePath, Session("UserID"))

            ext = ext.ToLower

            '  filePath = Server.MapPath("~/Uploads/") + filePath
            '    frmWord.Attributes["src"] = http://localhost/MyApp/resume.doc;
            ' iframeid.Attributes.Add("src", Server.HtmlDecode("D:\1_CWBInfotech\A_Sitapest\Program\Sitapest\Uploads\10000145_photo (1).JPG"))
            If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
                Dim strFilePath As String = Server.MapPath("Uploads\Service\")
                Dim strFile As String = CType(sender, LinkButton).CommandArgument
                Dim File As String() = strFile.Split("."c)
                Dim strExtension As String = ext
                Dim strUrl As String = "http://" + Request.Url.Authority + "/WordinIFrame/ConvertedLocation/"

                Dim Filename As String = strFilePath + strFile.Split("."c)(0) & Convert.ToString(".html")

                If System.IO.File.Exists(Filename) Then
                    System.IO.File.Delete(Filename)
                End If

                If ext = ".doc" Or ext = ".docx" Then
                    ConvertHTMLFromWord(strFilePath & strFile, strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html"))

                ElseIf ext = ".xls" Or ext = ".xlsx" Then
                    ConvertHtmlFromExcel(strFilePath + strFile, strFilePath + "A" + strFile.Split("."c)(0) + ".html")
                End If

                iframeid.Attributes("src") = "Uploads/Service/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

            Else

                InsertIntoTblWebEventLog("PreviewFile3", filePath, Session("UserID"))

                filePath = filePath.Replace("#", "%23")

                InsertIntoTblWebEventLog("PreviewFile4", filePath, Session("UserID"))


                iframeid.Attributes.Add("src", filePath)
                ' iframeid.Attributes.Add("src", "Uploads/Service/SERV202212-021268_STAR%20LEARNERS%20GROUP%20-%20134%20JALAN%20BUKIT%20MERAH%20%2301-1420.PDF")
            End If
            '  iframeid.Attributes.Add("src", "https://docs.google.com/viewer?url={D:/1_CWBInfotech/A_Sitapest/Program/Sitapest/Uploads/10000145_ActualVsForecast_Format1.pdf?pid=explorer&efh=false&a=v&chrome=false&embedded=true")
        Catch ex As Exception
            ' InsertIntoTblWebEventLog("PreviewFile", ex.Message.ToString, txtTechRcNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Function RetrieveRecord(filename As String) As String
        Try

            Dim str As String = ""
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim commandRec As MySqlCommand = New MySqlCommand

            commandRec.CommandType = CommandType.Text
            filename = filename.Replace("'", "''")
            commandRec.CommandText = "select * from tblfileupload where filenamelink like '" & filename & "%'"
            commandRec.Connection = conn

            Dim drRec As MySqlDataReader = commandRec.ExecuteReader()
            Dim dtRec As New System.Data.DataTable
            dtRec.Load(drRec)

            If dtRec.Rows.Count > 0 Then
                str = dtRec.Rows(0)("FileNameLink").ToString

            End If

            Return str


        Catch ex As Exception

            InsertIntoTblWebEventLog("RetrieveRecord", ex.Message.ToString, Session("UserID"))
            Return ""
        End Try
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Session.Add("FileNameLink", Request.QueryString("FileNameLink"))
        PreviewFile(sender, e)


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
            insCmds.Parameters.AddWithValue("@LoginId", "ViewManualReport - " + Session("UserID"))
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




    Public Sub ConvertHTMLFromWord(Source As Object, Target As Object)
        If Word Is Nothing Then
            ' Check for the prior instance of the OfficeWord Object
            Word = New Microsoft.Office.Interop.Word.ApplicationClass()
        End If

        Try
            ' To suppress window display the following code will help
            Word.Visible = False
            Word.Application.Visible = False
            Word.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMinimize



            Word.Documents.Open(Source, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown)

            Dim format As Object = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML

            Word.ActiveDocument.SaveAs(Target, format, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown)

            Status = StatusType.SUCCESS
            Message = Status.ToString()
        Catch e As Exception
            Message = "Error :" + e.Message.ToString().Trim()
        Finally
            If Word IsNot Nothing Then
                Word.Documents.Close(Unknown, Unknown, Unknown)
                Word.Quit(Unknown, Unknown, Unknown)
            End If
        End Try
    End Sub

    Public Sub ConvertHtmlFromExcel(Source As String, Target As String)
        If Excel Is Nothing Then
            Excel = New Microsoft.Office.Interop.Excel.ApplicationClass()
        End If

        Try
            Excel.Visible = False
            Excel.Application.Visible = False
            Excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMinimized

            Excel.Workbooks.Open(Source, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown)

            Dim format As Object = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml

            Excel.Workbooks(1).SaveAs(Target, format, Unknown, Unknown, Unknown, Unknown, _
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Unknown, Unknown, Unknown, Unknown, Unknown)

            Status = StatusType.SUCCESS

            Message = Status.ToString()
        Catch e As Exception
            Message = "Error :" + e.Message.ToString().Trim()
        Finally
            If Excel IsNot Nothing Then
                Excel.Workbooks.Close()
                Excel.Quit()
            End If
        End Try
    End Sub
End Class
