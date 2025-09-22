Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient

Imports System.Configuration
Imports System.Data.SqlClient


Partial Class RV_Select_PortfolioDateSummary
    Inherits System.Web.UI.Page

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click

        Try

       
        ' '''''''''''''''''''''''''''''''''''''''''''''''''''
        'Dim strSql As String = "INSERT INTO tblEventLog (StaffID,Module,DocRef,Action,ComputerName," & _
        '      "Serial, LogDate, Comments,SOURCESQLID) " & _
        '      "VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate,  @Comments, @SOURCESQLID)"
        ''"VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate, @Amount, @BaseValue, @BaseGSTValue, @CustCode, @Comments, @SOURCESQLID)"


        'Dim command As MySqlCommand = New MySqlCommand
        'command.CommandType = CommandType.Text
        'command.CommandText = strSql
        'command.Parameters.Clear()
        ''Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd"))
        'command.Parameters.AddWithValue("@StaffID", Session("UserID"))
        'command.Parameters.AddWithValue("@Module", "REPORTS")
        'command.Parameters.AddWithValue("@DocRef", "PORTFOLIOSUMMARY")
        'command.Parameters.AddWithValue("@Action", "")
        'command.Parameters.AddWithValue("@ComputerName", Strings.Left(My.Computer.Name.ToString, 20))
        'command.Parameters.AddWithValue("@Serial", "")
        ''command.Parameters.AddWithValue("@LogDate", Convert.ToString(Session("SysDate")))
        'command.Parameters.AddWithValue("@LogDate", Convert.ToDateTime(Session("SysTime")))
        ''command.Parameters.AddWithValue("@Amount", 0)
        ''command.Parameters.AddWithValue("@BaseValue", 0)
        ''command.Parameters.AddWithValue("@BaseGSTValue", 0)
        ''command.Parameters.AddWithValue("@CustCode", "")
        'command.Parameters.AddWithValue("@Comments", "")
        'command.Parameters.AddWithValue("@SOURCESQLID", 0)
        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        ''Dim conn As MySqlConnection = New MySqlConnection(constr)
        'conn.Open()
        'command.Connection = conn
        'command.ExecuteNonQuery()

        'conn.Close()
        'conn.Dispose()
        'command.Dispose()

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim selFormula As String
            selFormula = "{tbwportfolio1.CreatedBy} = '" + txtCreatedBy.Text + "' and {tbwportfolio1.Report}='DateSummary'"
        Dim selection As String
        selection = ""

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
                Return
            End If
        
            '   selFormula = selFormula + " and {Command.EndDate} >" + "#" + d.ToString("MM-dd-yyyy") + "#"
            '   selFormula = selFormula + " and ({Command.ActualEnd} is null) or {Command.ActualEnd} >" + "#" + d.ToString("MM-dd-yyyy") + "#)"
            ' selFormula = selFormula + " and {Command.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            '   Session.Add("StartDate", d.ToString("MM-dd-yyyy"))
            If selection = "" Then
                selection = "From Date >= " + startdate.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", From Date >= " + startdate.ToString("dd-MM-yyyy")
            End If
        Else
            MessageBox.Message.Alert(Page, "Date From is invalid", "str")
            Return
        End If


        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            '  Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, enddate) Then
            Else

            End If
            ' selFormula = selFormula + " and {Command.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            '  Session.Add("EndDate", d.ToString("MM-dd-yyyy"))
            If selection = "" Then
                selection = "To Date <= " + enddate.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", To Date <= " + enddate.ToString("dd-MM-yyyy")
            End If
        Else
            MessageBox.Message.Alert(Page, "Date To is invalid", "str")
            Return
        End If

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

         
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                command.CommandText = "PortfolioValueSummaryNewLocation"
            Else
                command.CommandText = "PortfolioValueSummaryNew"
            End If

            command.Parameters.Clear()

            'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            'command.Parameters.AddWithValue("@pr_Query", txtQuery.Text)
            command.Parameters.AddWithValue("@pr_startdate", startdate)
            command.Parameters.AddWithValue("@pr_enddate", enddate)
            command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                command.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
            End If
            command.Connection = conn
            command.ExecuteScalar()
            command.Dispose()
        
            conn.Close()
            conn.Dispose()

            'Dim command2 As MySqlCommand = New MySqlCommand

            'command2.CommandType = CommandType.Text
            'command2.CommandText = "delete from tbwportfolio where CreatedBy='" + Session("UserID") + "'"

            'command2.Connection = conn

            'command2.ExecuteNonQuery()
            'command2.Dispose()

            ' While startdate <= enddate


            '    Dim cmdvwPortfolio As MySqlCommand = New MySqlCommand

            '    cmdvwPortfolio.CommandType = CommandType.Text

            '    cmdvwPortfolio.CommandText = "Select sum(portfoliovalue) from vwcontractPortfolionew where startdate <= '" + Convert.ToDateTime(startdate).ToString("yyyy-MM-dd") + "' And portfolioterminationdate >'" + Convert.ToDateTime(startdate).ToString("yyyy-MM-dd") + "'"

            '    cmdvwPortfolio.Connection = conn

            '    Dim drvwPortfolio As MySqlDataReader = cmdvwPortfolio.ExecuteReader()
            '    Dim dtvwPortfolio As New DataTable
            '    dtvwPortfolio.Load(drvwPortfolio)

            '    Dim command As MySqlCommand = New MySqlCommand

            '    command.CommandType = CommandType.Text
            '    Dim qry As String = "INSERT INTO tbwportfolio(startdate,value,createdby,createdon)values(@startdate,@value,@createdby,@createdon)"

            '    command.CommandText = qry
            '    command.Parameters.Clear()
            '    command.Parameters.AddWithValue("@StartDate", startdate)

            '    command.Parameters.AddWithValue("@Value", dtvwPortfolio.Rows(0)("sum(portfoliovalue)"))
            '    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
            '    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    command.Connection = conn

            '    command.ExecuteNonQuery()

            '    startdate = startdate.AddDays(1)
            '   End While


            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)



            If rbtnSelect.SelectedValue = "2" Then
                Session.Add("ReportType", "PortfolioDateSummary")
                Response.Redirect("RV_PortfolioDateSummary.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()


            ElseIf rbtnSelect.SelectedValue = "3" Then
                Session.Add("ReportType", "PortfolioDateSummaryGraph")
                Response.Redirect("RV_PortfolioDateSummaryGraph.aspx")

            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("Print", ex.Message.ToString, "")

        End Try


    End Sub


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

        Dim qry As String = "select StartDate,Value from tbwportfolio where createdby = '" + txtCreatedBy.Text + "'"
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
        Else
            MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                Return False
        End If

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

        command.CommandText = "PortfolioValueSummaryNew"

        command.Parameters.Clear()

        'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
        'command.Parameters.AddWithValue("@pr_Query", txtQuery.Text)
        command.Parameters.AddWithValue("@pr_startdate", startdate)
        command.Parameters.AddWithValue("@pr_enddate", enddate)
        command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
        command.Connection = conn
        command.ExecuteScalar()
        conn.Close()
        conn.Dispose()
        command.Dispose()





        'Dim cmdvwPortfolio As MySqlCommand = New MySqlCommand

        'cmdvwPortfolio.CommandType = CommandType.Text

        'cmdvwPortfolio.CommandText = "Select sum(portfoliovalue) from vwcontractPortfolionew where startdate <= '" + Convert.ToDateTime(startdate).ToString("yyyy-MM-dd") + "' And portfolioterminationdate >'" + Convert.ToDateTime(startdate).ToString("yyyy-MM-dd") + "'"

        'cmdvwPortfolio.Connection = conn

        'Dim drvwPortfolio As MySqlDataReader = cmdvwPortfolio.ExecuteReader()
        'Dim dtvwPortfolio As New DataTable
        'dtvwPortfolio.Load(drvwPortfolio)

        'Dim command As MySqlCommand = New MySqlCommand

        'command.CommandType = CommandType.Text
        'Dim qry1 As String = "INSERT INTO tbwportfolio(startdate,value,createdby,createdon)values(@startdate,@value,@createdby,@createdon)"

        'command.CommandText = qry1
        'command.Parameters.Clear()
        'command.Parameters.AddWithValue("@StartDate", startdate)

        'command.Parameters.AddWithValue("@Value", dtvwPortfolio.Rows(0)("sum(portfoliovalue)"))
        'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
        'command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        'command.Connection = conn

        'command.ExecuteNonQuery()

        'command.Dispose()
        'cmdvwPortfolio.Dispose()
        'dtvwPortfolio.Clear()
        'dtvwPortfolio.Dispose()
        'drvwPortfolio.Close()

        'startdate = startdate.AddDays(1)
        'End While

        'conn.Close()
        'conn.Dispose()




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
            Dim attachment As String = "attachment; filename=PortfolioDateSummary.xls"
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
End Class
