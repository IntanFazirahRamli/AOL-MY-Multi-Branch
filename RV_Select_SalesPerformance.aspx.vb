

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data


Partial Class RV_Select_SalesPerformance
    Inherits System.Web.UI.Page


    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
    
        ddlCompanyGrp.SelectedIndex = 0
        ddlContractGroup.SelectedIndex = 0
        ddlSalesMan.SelectedIndex = 0
        ddlIndustry.SelectedIndex = 0

    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        '''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strSql As String = "INSERT INTO tblEventLog (StaffID,Module,DocRef,Action,ComputerName," & _
              "Serial, LogDate, Comments,SOURCESQLID) " & _
              "VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate,  @Comments, @SOURCESQLID)"
        '"VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate, @Amount, @BaseValue, @BaseGSTValue, @CustCode, @Comments, @SOURCESQLID)"


        Dim commandEventLog As MySqlCommand = New MySqlCommand
        commandEventLog.CommandType = CommandType.Text
        commandEventLog.CommandText = strSql
        commandEventLog.Parameters.Clear()
        'Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd"))
        commandEventLog.Parameters.AddWithValue("@StaffID", Session("UserID"))
        commandEventLog.Parameters.AddWithValue("@Module", "REPORTS")
        commandEventLog.Parameters.AddWithValue("@DocRef", "SALES PERFORMANCE")
        commandEventLog.Parameters.AddWithValue("@Action", "")
        commandEventLog.Parameters.AddWithValue("@ComputerName", Strings.Left(My.Computer.Name.ToString, 20))
        commandEventLog.Parameters.AddWithValue("@Serial", "")
        'command.Parameters.AddWithValue("@LogDate", Convert.ToString(Session("SysDate")))
        commandEventLog.Parameters.AddWithValue("@LogDate", Convert.ToDateTime(Session("SysTime")))
        'command.Parameters.AddWithValue("@Amount", 0)
        'command.Parameters.AddWithValue("@BaseValue", 0)
        'command.Parameters.AddWithValue("@BaseGSTValue", 0)
        'command.Parameters.AddWithValue("@CustCode", "")
        commandEventLog.Parameters.AddWithValue("@Comments", "")
        commandEventLog.Parameters.AddWithValue("@SOURCESQLID", 0)
        Dim connEventLog As MySqlConnection = New MySqlConnection()

        connEventLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Dim conn As MySqlConnection = New MySqlConnection(constr)
        connEventLog.Open()
        commandEventLog.Connection = connEventLog
        commandEventLog.ExecuteNonQuery()

        connEventLog.Close()
        connEventLog.Dispose()
        commandEventLog.Dispose()

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim selFormula As String
        selFormula = "{tblcontract1.rcno} <> 0 and (not ({tblcontract1.Status} in [""V"", ""R""])) and {tblcontract1.CategoryID} <> """""
        Dim selection As String
        selection = ""
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            '  qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"
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
                ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                lblAlert.Text = "INVALID DATE FROM"
                Return
            End If

            selFormula = selFormula + " and (if {tblcontract1.CategoryID}=""ADHOC"" then {tblcontract1.ServiceStart} >=" + "#" + d.ToString("MM-dd-yyyy") + "# else {tblcontract1.StartDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#)"
            If selection = "" Then
                selection = "Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + "<br>Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                lblAlert.Text = "INVALID DATE TO"
                Return
            End If
            '    selFormula = selFormula + "and {tblcontract1.ContractDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            selFormula = selFormula + " and (if {tblcontract1.CategoryID}=""ADHOC"" then {tblcontract1.ServiceStart} <=" + "#" + d.ToString("MM-dd-yyyy") + "# else {tblcontract1.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#)"

            If selection = "" Then
                selection = "Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + "<br>Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
    

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
                selection = selection + "<br>CompanyGroup : " + YrStr
            End If
        End If

        Dim YrStrListZone As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
            If item.Selected Then

                YrStrListZone.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListZone.Count > 0 Then

            Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)

            selFormula = selFormula + " and {tblcontract1.LocateGrp} in [" + YrStrZone + "]"
            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + "<br>Zone : " + YrStrZone
            End If
            '  qrySvcRec = qrySvcRec + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"
        End If


        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "Department = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + "<br>Department = " + ddlContractGroup.Text
        '    End If
        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlContractGroup.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
            selFormula = selFormula + " and {tblcontract1.ContractGroup} in [" + YrStr + "]"
           
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If

        End If
        If ddlSalesMan.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.Salesman} = '" + ddlSalesMan.Text + "'"
            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + "<br>Salesman = " + ddlSalesMan.Text
            End If
        End If

        If ddlIndustry.Text = "-1" Then
        Else
            selFormula = selFormula + " and ({tblcontract1.Industry} = '" + ddlIndustry.Text + "'"
            If selection = "" Then
                selection = "Industry = " + ddlIndustry.Text
            Else
                selection = selection + "<br>Industry = " + ddlIndustry.Text
            End If
        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        If rbtnSelect.SelectedValue = "1" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Session.Add("ReportType", "SalesPerformanceDetailsBySalesman")
                Response.Redirect("RV_SalesPerformanceDetailsBySalesman.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Session.Add("ReportType", "SalesPerformanceSummaryBySalesman")
                Response.Redirect("RV_SalesPerformanceSummaryBySalesman.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "2" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Session.Add("ReportType", "SalesPerformanceDetailsByCompanyGrp")
                Response.Redirect("RV_SalesPerformanceDetailsByCompanyGrp.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Session.Add("ReportType", "SalesPerformanceSummaryByCompanyGrp")
                Response.Redirect("RV_SalesPerformanceSummaryByCompanyGrp.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "3" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Session.Add("ReportType", "SalesPerformanceDetailsByDepartment")
                Response.Redirect("RV_SalesPerformanceDetailsByDepartment.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Session.Add("ReportType", "SalesPerformanceSummaryByDepartment")
                Response.Redirect("RV_SalesPerformanceSummaryByDepartment.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "4" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Session.Add("ReportType", "SalesPerformanceDetailsByMarketSegment")
                Response.Redirect("RV_SalesPerformanceDetailsByMarketSegment.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Session.Add("ReportType", "SalesPerformanceSummaryByMarketSegment")
                Response.Redirect("RV_SalesPerformanceSummaryByMarketSegment.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "5" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Session.Add("ReportType", "SalesPerformanceDetailsByClient")
                Response.Redirect("RV_SalesPerformanceDetailsByClient.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Session.Add("ReportType", "SalesPerformanceSummaryByClient")
                Response.Redirect("RV_SalesPerformanceSummaryByClient.aspx")
            End If
        End If


    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID

        txtCreatedOn.Attributes.Add("readonly", "readonly")
    End Sub

    Protected Sub ddlCompanyGrp_Changed(sender As Object, e As EventArgs)
        Dim ddl_cg As Saplin.Controls.DropDownCheckBoxes = DirectCast(sender, Saplin.Controls.DropDownCheckBoxes)
        lblCompanyGroup.Text = ""

        For Each item As ListItem In ddlCompanyGrp.Items

            If item.Selected Then
                lblCompanyGroup.Text += item.Value + ","
            End If
        Next
        lblCompanyGroup.Text = lblCompanyGroup.Text.Trim().Remove(lblCompanyGroup.Text.Length - 1)

    End Sub
End Class

