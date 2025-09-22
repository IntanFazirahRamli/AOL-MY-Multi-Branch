
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_IndividualProductivity
    Inherits System.Web.UI.Page

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        '  txtServiceID.SelectedIndex = 0
        ddlIncharge.SelectedIndex = 0

        ddlCompanyGrp.SelectedIndex = 0

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
        commandEventLog.Parameters.AddWithValue("@DocRef", "MONTHLY PRODUCTIVITY")
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
        Dim qrySvcRec As String = "SELECT * FROM tblservicerecord where status='P'"

        Dim selFormula As String
        '    If rdbSelect.Text = "Detail" Then
        selFormula = "{tblrptserviceanalysis1.rcno} <> 0 and {tblrptserviceanalysis1.Status} = 'P' and {tblrptserviceanalysis1.Report}='IndividualProductivity' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

        'ElseIf rdbSelect.Text = "Summary" Then
        'selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberSummary'"

        'End If
        Dim selection As String
        selection = ""

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            ' selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"

            qrySvcRec = qrySvcRec + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"

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
                lblAlert.Text = "INVALID SERVICE DATE FROM"
                Return
            End If

            qrySvcRec = qrySvcRec + " and ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
            selFormula = selFormula + " and {tblrptserviceanalysis1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + "<br>Service Date >= " + d.ToString("dd-MM-yyyy")
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
            qrySvcRec = qrySvcRec + " and ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
            selFormula = selFormula + " and {tblrptserviceanalysis1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + "<br>Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        'If txtServiceID.Text = "-1" Then
        'Else

        '    selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} = '" + txtServiceID.SelectedItem.Text + "'"
        '    If selection = "" Then
        '        selection = "ServiceID = " + txtServiceID.SelectedItem.Text
        '    Else
        '        selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
        '    End If
        'End If

        If ddlIncharge.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblrptserviceanalysis1.StaffID} = '" + ddlIncharge.Text + "'"


            qrySvcRec = qrySvcRec + " and ServiceBy = '" + ddlIncharge.Text + "'"

            If selection = "" Then
                selection = "ServiceBy : " + ddlIncharge.Text
            Else
                selection = selection + "<br>ServiceBy : " + ddlIncharge.Text
            End If
        End If

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    qrySvcRec = qrySvcRec + " and CompanyGroup = '" + ddlCompanyGrp.Text + "'"
        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"

        '    If selection = "" Then
        '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
        '    End If

        'End If

        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
            selFormula = selFormula + " and {tblrptserviceanalysis1.CompanyGroup} in [" + YrStr + "]"

            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + "<br>CompanyGroup : " + YrStr
            End If
            qrySvcRec = qrySvcRec + " and CompanyGroup in (" + YrStr + ")"

        End If

        Dim YrStrListZone As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
            If item.Selected Then

                YrStrListZone.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListZone.Count > 0 Then

            Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)
          
                selFormula = selFormula + " and {tblrptserviceanalysis1.LocateGrp} in [" + YrStrZone + "]"
        
            ' selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
            qrySvcRec = qrySvcRec + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"
        End If

      
        GetData()

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        If rbtnSelect.SelectedValue = "1" Then
            Session.Add("ReportType", "IndividualProductivity")
            Response.Redirect("RV_IndividualProductivity.aspx")
        ElseIf rbtnSelect.SelectedValue = "2" Then
            selFormula = selFormula + " and {tblcontract1.CompanyGroup} in [MQ,MQ(Adhoc)]"
            Session.Add("Title", "MONTHLY PRODUCTIVITY FOR MQ DIVISION")
            Session.Add("ReportType", "MonthlyProductivityByContractGroup")
            Response.Redirect("RV_MonthlyProductivityByContractGroup.aspx")
        ElseIf rbtnSelect.SelectedValue = "3" Then
            selFormula = selFormula + " and {tblcontract1.CompanyGroup} in [CP,CP(Adhoc)]"
            Session.Add("Title", "MONTHLY PRODUCTIVITY FOR CP DIVISION")
            Session.Add("ReportType", "MonthlyProductivityByContractGroup")
            Response.Redirect("RV_MonthlyProductivityByContractGroup.aspx")
        ElseIf rbtnSelect.SelectedValue = "4" Then
            selFormula = selFormula + " and {tblcontract1.CompanyGroup} not in [CP,CP(Adhoc),MQ,MQ(Adhoc),Fumigation,Others]"
            Session.Add("Title", "MONTHLY PRODUCTIVITY FOR ST DIVISION")
            Session.Add("ReportType", "MonthlyProductivityByContractGroup")
            Response.Redirect("RV_MonthlyProductivityByContractGroup.aspx")
        End If
     

    End Sub

    'Private Sub GetData()

    '    Dim conn As MySqlConnection = New MySqlConnection()

    '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    conn.Open()

    '    Dim cmdSvcRec As MySqlCommand = New MySqlCommand

    '    cmdSvcRec.CommandType = CommandType.Text

    '    cmdSvcRec.CommandText = qrySvcRec


    '    cmdSvcRec.Connection = conn

    '    Dim drSvcRec As MySqlDataReader = cmdSvcRec.ExecuteReader()
    '    Dim dtSvcRec As New DataTable
    '    dtSvcRec.Load(drSvcRec)

    '    If dtSvcRec.Rows.Count > 0 Then
    '        Dim command2 As MySqlCommand = New MySqlCommand

    '        command2.CommandType = CommandType.Text

    '        command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text + "' and Report='IndividualProductivity';"

    '        command2.Connection = conn

    '        command2.ExecuteNonQuery()

    '        For i As Integer = 0 To dtSvcRec.Rows.Count - 1
    '            Dim cmdSvcRecStaff As MySqlCommand = New MySqlCommand

    '            cmdSvcRecStaff.CommandType = CommandType.Text

    '            cmdSvcRecStaff.CommandText = "Select count(staffid),staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


    '            cmdSvcRecStaff.Connection = conn

    '            Dim drSvcRecStaff As MySqlDataReader = cmdSvcRecStaff.ExecuteReader()
    '            Dim dtSvcRecStaff As New DataTable
    '            dtSvcRecStaff.Load(drSvcRecStaff)

    '            Dim cmdSvcRecDet As MySqlCommand = New MySqlCommand

    '            cmdSvcRecDet.CommandType = CommandType.Text


    '            cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


    '            cmdSvcRecDet.Connection = conn

    '            Dim drSvcRecDet As MySqlDataReader = cmdSvcRecDet.ExecuteReader()
    '            Dim dtSvcRecDet As New DataTable
    '            dtSvcRecDet.Load(drSvcRecDet)
    '            Dim targetid As String = ""
    '            Dim servicecost As Decimal = 0

    '            If dtSvcRecDet.Rows.Count > 0 Then

    '                For j As Integer = 0 To dtSvcRecDet.Rows.Count - 1
    '                    If dtSvcRecDet.Rows(j)("TargetID").ToString <> DBNull.Value.ToString Then
    '                        If targetid = "" Then
    '                            targetid = dtSvcRecDet.Rows(j)("TargetID")
    '                        Else

    '                            targetid = targetid + "," + dtSvcRecDet.Rows(j)("TargetID")
    '                        End If
    '                    End If

    '                    If dtSvcRecDet.Rows(j)("ServiceCost").ToString = DBNull.Value.ToString Then
    '                    Else

    '                        servicecost = servicecost + dtSvcRecDet.Rows(j)("ServiceCost")

    '                    End If

    '                Next
    '            End If

    '            Dim command As MySqlCommand = New MySqlCommand

    '            command.CommandType = CommandType.Text
    '            Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report,Status,CompanyGroup,ContractNo,LocateGrp)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report,@Status,@CompanyGroup,@ContractNo,@LocateGrp);"
    '            command.CommandText = qry
    '            command.Parameters.Clear()
    '            command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
    '            command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)

    '            command.Parameters.AddWithValue("@ServiceDate", dtSvcRec.Rows(i)("ServiceDate"))
    '            command.Parameters.AddWithValue("@NoPerson", dtSvcRecStaff.Rows(0)("count(staffid)"))
    '            command.Parameters.AddWithValue("@Duration", dtSvcRec.Rows(i)("Duration"))
    '            command.Parameters.AddWithValue("@AccountID", dtSvcRec.Rows(i)("AccountID").ToString)
    '            command.Parameters.AddWithValue("@Client", dtSvcRec.Rows(i)("CustName").ToString)
    '            command.Parameters.AddWithValue("@TimeIn", dtSvcRec.Rows(i)("TimeIn"))
    '            command.Parameters.AddWithValue("@TimeOut", dtSvcRec.Rows(i)("Timeout"))
    '            command.Parameters.AddWithValue("@Service", targetid)
    '            command.Parameters.AddWithValue("@ServiceValue", dtSvcRec.Rows(i)("BillAmount"))
    '            command.Parameters.AddWithValue("@ServiceCost", servicecost)

    '            command.Parameters.AddWithValue("@ManpowerCost", 0)
    '            command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
    '            command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID"))
    '            command.Parameters.AddWithValue("@NormalHour", 0)
    '            command.Parameters.AddWithValue("@OTHour", 0)
    '            If dtSvcRecDet.Rows.Count > 0 Then
    '                command.Parameters.AddWithValue("@ServDateType", dtSvcRecDet.Rows(0)("ServiceID").ToString)
    '            Else
    '                command.Parameters.AddWithValue("@ServDateType", "")
    '            End If

    '            command.Parameters.AddWithValue("@OTRate", 0)
    '            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))

    '            command.Parameters.AddWithValue("@Report", "IndividualProductivity")
    '            command.Parameters.AddWithValue("@Status", dtSvcRec.Rows(i)("Status"))
    '            command.Parameters.AddWithValue("@CompanyGroup", dtSvcRec.Rows(i)("Companygroup"))
    '            command.Parameters.AddWithValue("@ContractNo", dtSvcRec.Rows(i)("ContractNo"))
    '            command.Parameters.AddWithValue("@LocateGrp", dtSvcRec.Rows(i)("LocateGrp"))

    '            command.Connection = conn

    '            command.ExecuteNonQuery()

    '            dtSvcRecDet.Clear()
    '            dtSvcRecStaff.Clear()
    '            drSvcRecDet.Close()
    '            drSvcRecStaff.Close()


    '        Next

    '    End If

    '    dtSvcRec.Clear()
    '    drSvcRec.Close()

    '    conn.Close()
    '    conn.Dispose()


    'End Sub

    Private Sub GetData()

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='IndividualProductivity';"

        command2.Connection = conn

        command2.ExecuteNonQuery()
        command2.Dispose()

        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = "SaveIndividualProductivity"
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtsvcdatefrom.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtsvcdateto.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
        command.Parameters.AddWithValue("@pr_Report", "IndividualProductivity")

        command.Connection = conn
        command.ExecuteScalar()

        command.Dispose()
        conn.Close()
        conn.Dispose()
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

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        If GetData1() = True Then
            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return
        End If

    End Sub
    Private Function GetData1() As Boolean
        GetData()

        Dim qrySvcRec As String = ""

        qrySvcRec = "Select A.StaffID,C.StaffDepartment,C.LedgerCode,C.SubLedgerCode,sum((ServiceValue-ManpowerCost)/NoPerson) as Profit"
        qrySvcRec = qrySvcRec + " from tblrptserviceanalysis A left join tblstaff B on A.sTAFFid=B.StaffID"
        qrySvcRec = qrySvcRec + " LEFT JOIN tblstaffdepartment C ON B.Department=C.StaffDepartment where report = 'IndividualProductivity' and A.createdby='" & Session("UserID") & "'"

         Dim selection As String
        selection = ""

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
        
            qrySvcRec = qrySvcRec + " and A.location in (" + Convert.ToString(Session("Branch")) + ")"

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
                lblAlert.Text = "INVALID SERVICE DATE FROM"
                Return False
            End If

            qrySvcRec = qrySvcRec + " and A.ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
         
            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + "<br>Service Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                lblAlert.Text = "INVALID SERVICE DATE TO"
                Return False
            End If
            qrySvcRec = qrySvcRec + " and A.ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
         
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + "<br>Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
       
        If ddlIncharge.Text = "-1" Then
        Else

            qrySvcRec = qrySvcRec + " and A.ServiceBy = '" + ddlIncharge.Text + "'"

            If selection = "" Then
                selection = "ServiceBy : " + ddlIncharge.Text
            Else
                selection = selection + "<br>ServiceBy : " + ddlIncharge.Text
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
        
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + "<br>CompanyGroup : " + YrStr
            End If
            qrySvcRec = qrySvcRec + " and A.CompanyGroup in (" + YrStr + ")"

        End If

        Dim YrStrListZone As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
            If item.Selected Then

                YrStrListZone.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListZone.Count > 0 Then

            Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)

            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
            qrySvcRec = qrySvcRec + " and A.LocateGrp in (" + YrStrZone + ")"
        End If

        txtQuery.Text = qrySvcRec + " group by A.StaffID"

      
        Session.Add("selection", selection)

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

                If j = 4 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Double = Convert.ToDouble("0.00")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _doubleCellStyle
                    'ElseIf j = 2 Then
                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                    '        Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                    '        cell.SetCellValue(d)

                    '    End If
                    '    cell.CellStyle = dateCellStyle
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

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "IndividualProductivity.xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "IndividualProductivity.xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub
End Class
