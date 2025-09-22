


Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Diagnostics


Partial Class RV_Select_ManpowerProdTeamMemberOTNew
    Inherits System.Web.UI.Page


    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.SelectedIndex = 0
        ddlIncharge.SelectedIndex = 0

        ddlCompanyGrp.SelectedIndex = 0

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
            insCmds.Parameters.AddWithValue("@LoginId", "MANPOWER PRODUCTIVITY BY TEAM MEMBER-OT - " + txtCreatedBy.Text)
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()
            lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim errcode As String
        errcode = ""

        Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''
            errcode = "1"
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
            commandEventLog.Parameters.AddWithValue("@DocRef", "MANPOWER-TEAMMEMBEROT")
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
            errcode = "2"

            '   Dim stopwatch As Stopwatch = stopwatch.StartNew()
            Dim qrySvcRec As String = "SELECT servicedate,recordno,dayname(servicedate) FROM tblservicerecord where status='P' and servicedate is not null and (timein is not null and timeout is not null) and (timein <>"" and timeout <> "") and"
            qrySvcRec = qrySvcRec + " (timein<>'0' and timeout<>'0') and timein<>'  :' and timeout<>'  :'"
            Dim selection As String
            selection = ""

            Dim selFormula As String
            If rdbSelect.Text = "Detail" Then
                errcode = "3"
                selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberOTDet' AND {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "' and {tblrptserviceanalysis1.OTRate} in [2.00, 1.50]"

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"

                    qrySvcRec = qrySvcRec + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If


            ElseIf rdbSelect.Text = "Summary" Then
                errcode = "4"
                selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberOTSumm' AND {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "' and {tblrptserviceanalysis1.OTRate} in [2.00, 1.50]"

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                    qrySvcRec = qrySvcRec + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If
            End If


            If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
                errcode = "5"
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                    lblAlert.Text = "INVALID SERVICE DATE FROM"
                    Return
                End If

                qrySvcRec = qrySvcRec + " and ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

                If selection = "" Then
                    selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
                End If

            End If
            If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
                errcode = "6"
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                    lblAlert.Text = "INVALID SERVICE DATE TO"
                    Return
                End If
                qrySvcRec = qrySvcRec + " and ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

                If selection = "" Then
                    selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
                End If
            End If
            If txtServiceID.Text = "-1" Then
            Else

                selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} = '" + txtServiceID.SelectedItem.Text + "'"
                If selection = "" Then
                    selection = "ServiceID = " + txtServiceID.SelectedItem.Text
                Else
                    selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
                End If
            End If
            If ddlStaffDept.Text = "-1" Then
            Else

                selFormula = selFormula + " and {tblstaff1.Department} = '" + ddlStaffDept.Text + "'"
                If selection = "" Then
                    selection = "Department = " + ddlStaffDept.Text
                Else
                    selection = selection + ", Department = " + ddlStaffDept.Text
                End If
            End If
            If ddlIncharge.Text = "-1" Then
            Else
                selFormula = selFormula + " and {tblrptserviceanalysis1.StaffID} = '" + ddlIncharge.Text + "'"


                '   qrySvcRec = qrySvcRec + " and ServiceBy = '" + ddlIncharge.Text + "'"
                qrySvcRec = qrySvcRec + " and recordno in (SELECT RecordNo FROM tblservicerecordstaff where staffid = '" + ddlIncharge.Text.Trim + "')"

                If selection = "" Then
                    selection = "ServiceBy : " + ddlIncharge.Text
                Else
                    selection = selection + ", ServiceBy : " + ddlIncharge.Text
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
                selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"

                If selection = "" Then
                    selection = "CompanyGroup : " + YrStr
                Else
                    selection = selection + ", CompanyGroup : " + YrStr
                End If
                qrySvcRec = qrySvcRec + " and tblservicerecord.CompanyGroup in (" + YrStr + ")"

            End If

            Dim YrStrListZone As List(Of [String]) = New List(Of String)()

            For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
                If item.Selected Then

                    YrStrListZone.Add("""" + item.Value + """")

                End If
            Next

            If YrStrListZone.Count > 0 Then

                Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)


                selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
                If selection = "" Then
                    selection = "Zone : " + YrStrZone
                Else
                    selection = selection + ", Zone : " + YrStrZone
                End If
                qrySvcRec = qrySvcRec + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"
            End If

            ' errcode = "8"
            GetData()
            ' errcode = "9"
            '    Return

            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'Dim cmdSvcRec As MySqlCommand = New MySqlCommand

            'cmdSvcRec.CommandType = CommandType.Text

            'cmdSvcRec.CommandText = qrySvcRec
            'cmdSvcRec.CommandTimeout = 0

            'cmdSvcRec.Connection = conn

            'Dim drSvcRec As MySqlDataReader = cmdSvcRec.ExecuteReader()
            'Dim dtSvcRec As New DataTable
            'dtSvcRec.Load(drSvcRec)
            'errcode = "8"
            'If dtSvcRec.Rows.Count > 0 Then
            '    errcode = "9"
            '    Dim command2 As MySqlCommand = New MySqlCommand

            '    command2.CommandType = CommandType.Text
            '    If rdbSelect.Text = "Detail" Then
            '        command2.CommandText = "delete from tblrptserviceanalysis where Report='ProdTeamMemberOTDet' and CreatedBy='" + txtCreatedBy.Text + "';"

            '    ElseIf rdbSelect.Text = "Summary" Then
            '        command2.CommandText = "delete from tblrptserviceanalysis where Report='ProdTeamMemberOTSumm' and CreatedBy='" + txtCreatedBy.Text + "';"

            '    End If

            '    command2.Connection = conn

            '    command2.ExecuteNonQuery()



            'errcode = "10"
            '    '    For i As Integer = 0 To dtSvcRec.Rows.Count - 1
            '    'Dim cmdSvcRecStaff As MySqlCommand = New MySqlCommand

            '    'cmdSvcRecStaff.CommandType = CommandType.Text

            '    'cmdSvcRecStaff.CommandText = "Select count(staffid),staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


            '    'cmdSvcRecStaff.Connection = conn

            '    'Dim drSvcRecStaff As MySqlDataReader = cmdSvcRecStaff.ExecuteReader()
            '    'Dim dtSvcRecStaff As New DataTable
            '    'dtSvcRecStaff.Load(drSvcRecStaff)

            '    ''Dim staffid As String = ""
            '    ''If dtSvcRecStaff.Rows.Count > 0 Then
            '    ''    For k As Integer = 0 To dtSvcRecStaff.Rows.Count - 1
            '    ''        If staffid <> "" Then
            '    ''            staffid = staffid + "," + dtSvcRecStaff.Rows(k)("Staffid").ToString
            '    ''        End If
            '    ''    Next
            '    ''End If
            '    'Dim cmdSvcRecDet As MySqlCommand = New MySqlCommand

            '    'cmdSvcRecDet.CommandType = CommandType.Text

            '    'If txtServiceID.Text = "-1" Then
            '    '    cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"
            '    'Else
            '    '    cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "' and serviceid='" + txtServiceID.SelectedItem.Text + "'"

            '    'End If


            '    'cmdSvcRecDet.Connection = conn
            '    'errcode = "11"
            '    'Dim drSvcRecDet As MySqlDataReader = cmdSvcRecDet.ExecuteReader()
            '    'Dim dtSvcRecDet As New DataTable
            '    'dtSvcRecDet.Load(drSvcRecDet)
            '    'Dim targetid As String = ""
            '    'Dim servicecost As Decimal = 0
            '    'errcode = "12"
            '    'If dtSvcRecDet.Rows.Count > 0 Then

            '    '    For j As Integer = 0 To dtSvcRecDet.Rows.Count - 1
            '    '        If dtSvcRecDet.Rows(j)("TargetID").ToString <> DBNull.Value.ToString Then
            '    '            If targetid = "" Then
            '    '                targetid = dtSvcRecDet.Rows(j)("TargetID")
            '    '            Else

            '    '                targetid = targetid + "," + dtSvcRecDet.Rows(j)("TargetID")
            '    '            End If
            '    '        End If

            '    '        If dtSvcRecDet.Rows(j)("ServiceCost").ToString = DBNull.Value.ToString Then
            '    '        Else

            '    '            servicecost = servicecost + dtSvcRecDet.Rows(j)("ServiceCost")

            '    '        End If

            '    '    Next
            '    'End If
            '    'errcode = "13"

            '    ' ''''''''''''''Normal and OT HOURS CALCULATION''''''
            '    ' ''''check for holidays''''''''''''
            '    'Dim ot2hour As Decimal = 0

            '    'Dim ot15hour As Decimal = 0
            '    'Dim otnormal As Decimal = 0
            '    'Dim otRate As Decimal = 0
            '    'Dim timediff As Decimal = 0

            '    'Dim svcdt As DateTime

            '    'svcdt = dtSvcRec.Rows(i)("ServiceDate")

            '    ''    Dim dtSvcSetup As New DataTable
            '    'Dim cmdHoliday As MySqlCommand = New MySqlCommand

            '    'cmdHoliday.CommandType = CommandType.Text

            '    'cmdHoliday.CommandText = "Select holiday from tblholiday where holiday = '" + svcdt.ToString("yyyy-MM-dd") + "'"

            '    'cmdHoliday.Connection = conn

            '    'Dim drHoliday As MySqlDataReader = cmdHoliday.ExecuteReader()
            '    'Dim dtHoliday As New DataTable
            '    'dtHoliday.Load(drHoliday)
            '    'errcode = "14"
            '    'If ((dtSvcRec.Rows(i)("TimeIn").ToString <> "" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "") And (String.IsNullOrEmpty(dtSvcRec.Rows(i)("TimeIn").ToString) = False Or String.IsNullOrEmpty(dtSvcRec.Rows(i)("TimeOut").ToString) = False) And (dtSvcRec.Rows(i)("TimeIn").ToString <> "0" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "0") And (dtSvcRec.Rows(i)("TimeIn").ToString <> "  :" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "  :")) Then
            '    '    If dtHoliday.Rows.Count > 0 Then
            '    '        ot2hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn")), Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut")))
            '    '        otRate = 2
            '    '    Else
            '    '        ''''Check for Saturday and Sunday'''''


            '    '        If svcdt.DayOfWeek.ToString = "Sunday" Then
            '    '            ot2hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn")), Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut")))
            '    '            otRate = 2
            '    '        ElseIf svcdt.DayOfWeek.ToString = "Saturday" Then

            '    '            ''''''''find saturday working time''''

            '    '            Dim cmdSvcSetup As MySqlCommand = New MySqlCommand

            '    '            cmdSvcSetup.CommandType = CommandType.Text

            '    '            cmdSvcSetup.CommandText = "Select satstart,satend from tblservicerecordmastersetup;"

            '    '            cmdSvcSetup.Connection = conn

            '    '            Dim drSvcSetup As MySqlDataReader = cmdSvcSetup.ExecuteReader()
            '    '            Dim dtSvcSetup As New DataTable
            '    '            dtSvcSetup.Load(drSvcSetup)

            '    '            errcode = "15"
            '    '            If dtSvcSetup.Rows.Count > 0 Then
            '    '                Dim timein As New DateTime
            '    '                timein = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn"))
            '    '                Dim timeout As New DateTime
            '    '                timeout = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut"))
            '    '                Dim satstart As New DateTime
            '    '                satstart = Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart"))
            '    '                Dim satend As New DateTime
            '    '                satend = Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd"))

            '    '                'Normal working hours on Saturday : SatStart-8:30 and SatEnd-12:30
            '    '                Dim servicetime As TimeSpan
            '    '                '  Dim dur As String

            '    '                servicetime = DateTime.Parse(timeout).Subtract(DateTime.Parse(timein))

            '    '                If servicetime.TotalMinutes < 0 Then
            '    '                    timeout = timeout.AddDays(1)
            '    '                End If


            '    '                If timein = satstart Then

            '    '                    If timeout > satstart Then
            '    '                        If timeout = satend Then
            '    '                            otnormal = DateDiff(DateInterval.Minute, timein, timeout)
            '    '                            otRate = 1.0
            '    '                        ElseIf timeout < satend Then 'Eg: TimeIn->8:30 and TimeOut->9:30
            '    '                            otnormal = DateDiff(DateInterval.Minute, timein, timeout)
            '    '                            otRate = 1.0
            '    '                        ElseIf timeout > satend Then 'Eg: TimeIn->8:30 and TimeOut->12:50
            '    '                            ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
            '    '                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))
            '    '                            otRate = 1.5
            '    '                        End If
            '    '                    End If

            '    '                ElseIf timein < satstart Then
            '    '                    If timeout <= satstart Then
            '    '                        ot15hour = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->7:00 and TimeOut->8:00
            '    '                        otRate = 1.5
            '    '                    ElseIf timeout >= satstart Then

            '    '                        If timeout <= satend Then 'Eg: TimeIn->7:50 and TimeOut->8:50
            '    '                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
            '    '                            ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")))
            '    '                            otRate = 1.5
            '    '                        ElseIf timeout >= satend Then 'Eg: TimeIn->7:50 and TimeOut->12:50
            '    '                            ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart"))) + DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
            '    '                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))
            '    '                            otRate = 1.5
            '    '                        End If
            '    '                    End If

            '    '                ElseIf timein > satstart Then
            '    '                    If timeout <= satend Then
            '    '                        otnormal = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->10:50 and TimeOut->11:50
            '    '                        otRate = 1
            '    '                    ElseIf timeout >= satend Then
            '    '                        If timein <= satend Then 'Eg: TimeIn->11:50 and TimeOut->12:50
            '    '                            ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)

            '    '                            otnormal = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))

            '    '                            otRate = 1.5
            '    '                        ElseIf timein >= satend Then 'Eg: TimeIn->1:50 and TimeOut->2:50
            '    '                            ot15hour = DateDiff(DateInterval.Minute, timein, timeout)

            '    '                            otRate = 1.5
            '    '                        End If
            '    '                    End If

            '    '                End If

            '    '            End If
            '    '        Else

            '    '            errcode = "16"
            '    '            ''''''''find weekdays working time''''

            '    '            Dim cmdSvcSetup As MySqlCommand = New MySqlCommand

            '    '            cmdSvcSetup.CommandType = CommandType.Text

            '    '            cmdSvcSetup.CommandText = "Select weekdaystart,weekdayend from tblservicerecordmastersetup;"

            '    '            cmdSvcSetup.Connection = conn

            '    '            Dim drSvcSetup As MySqlDataReader = cmdSvcSetup.ExecuteReader()
            '    '            Dim dtSvcSetup As New DataTable
            '    '            dtSvcSetup.Load(drSvcSetup)
            '    '            errcode = "17"
            '    '            If dtSvcSetup.Rows.Count > 0 Then
            '    '                'Normal working hours on Weekday : WeekdayStart-8:30 and WeekdayEnd-5:30
            '    '                '   lblAlert.Text = dtSvcRec.Rows(i)("TimeIn").ToString + " " + dtSvcRec.Rows(i)("Timeout").ToString
            '    '                Dim timein As New DateTime
            '    '                timein = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn"))
            '    '                Dim timeout As New DateTime
            '    '                timeout = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut"))
            '    '                Dim weekdaystart As New DateTime
            '    '                weekdaystart = Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart"))
            '    '                Dim weekdayend As New DateTime
            '    '                weekdayend = Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd"))

            '    '                Dim servicetime As TimeSpan
            '    '                '  Dim dur As String

            '    '                servicetime = DateTime.Parse(timeout).Subtract(DateTime.Parse(timein))

            '    '                If servicetime.TotalMinutes < 0 Then
            '    '                    timeout = timeout.AddDays(1)
            '    '                End If


            '    '                If timein = weekdaystart Then

            '    '                    If timeout > weekdaystart Then
            '    '                        If timeout = weekdayend Then
            '    '                            otnormal = DateDiff(DateInterval.Minute, timein, timeout)
            '    '                            otRate = 1.0

            '    '                        ElseIf timeout < weekdayend Then 'Eg: TimeIn->8:30 and TimeOut->9:30
            '    '                            otnormal = DateDiff(DateInterval.Minute, timein, timeout)
            '    '                            otRate = 1.0
            '    '                        ElseIf timeout > weekdayend Then 'Eg: TimeIn->8:30 and TimeOut->12:50
            '    '                            ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
            '    '                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
            '    '                            otRate = 1.5
            '    '                        End If
            '    '                    End If

            '    '                ElseIf timein < weekdaystart Then

            '    '                    If timeout <= weekdaystart Then
            '    '                        ot15hour = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->7:00 and TimeOut->8:00
            '    '                        'Eg: TimeIn->7:00 and TimeOut->8:00
            '    '                        otRate = 1.5
            '    '                    ElseIf timeout >= weekdaystart Then

            '    '                        If timeout <= weekdayend Then 'Eg: TimeIn->7:50 and TimeOut->8:50

            '    '                            '  otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(i)("WeekDayEnd")), Convert.ToDateTime(dtSvcRec.Rows(0)("TimeOut")))
            '    '                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), timeout)

            '    '                            ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")))
            '    '                            otRate = 1.5
            '    '                        ElseIf timeout >= weekdayend Then 'Eg: TimeIn->7:50 and TimeOut->12:50
            '    '                            ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart"))) + DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
            '    '                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
            '    '                            otRate = 1.5
            '    '                        End If
            '    '                    End If

            '    '                ElseIf timein > weekdaystart Then

            '    '                    If timeout <= weekdayend Then
            '    '                        otnormal = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->10:50 and TimeOut->11:50
            '    '                        otRate = 1
            '    '                    ElseIf timeout >= weekdayend Then
            '    '                        If dtSvcRec.Rows(i)("TimeIn") <= dtSvcSetup.Rows(0)("WeekDayEnd") Then 'Eg: TimeIn->11:50 and TimeOut->12:50
            '    '                            ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
            '    '                            otnormal = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
            '    '                            otRate = 1.5
            '    '                        ElseIf timein >= weekdayend Then 'Eg: TimeIn->1:50 and TimeOut->2:50
            '    '                            ot15hour = DateDiff(DateInterval.Minute, timein, timeout)
            '    '                            otRate = 1.5
            '    '                        End If
            '    '                    End If
            '    '                End If
            '    '            End If

            '    '        End If
            '    '    End If
            '    'End If


            '    ' ''''''Delete records that exists already
            '    ''Dim command1 As MySqlCommand = New MySqlCommand

            '    ''command1.CommandType = CommandType.Text

            '    ''command1.CommandText = "SELECT * FROM tblrptserviceanalysis where recordno='" + dtSvcRec.Rows(i)("RecordNo") + "'"
            '    ''command1.Connection = conn

            '    ''Dim dr As MySqlDataReader = command1.ExecuteReader()
            '    ''Dim dt As New DataTable
            '    ''dt.Load(dr)

            '    ''If dt.Rows.Count > 0 Then




            '    ''End If
            '    'errcode = "18"
            '    'Dim cmdSvcRecStaff1 As MySqlCommand = New MySqlCommand

            '    'cmdSvcRecStaff1.CommandType = CommandType.Text

            '    'cmdSvcRecStaff1.CommandText = "Select staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


            '    'cmdSvcRecStaff1.Connection = conn

            '    'Dim drSvcRecStaff1 As MySqlDataReader = cmdSvcRecStaff1.ExecuteReader()
            '    'Dim dtSvcRecStaff1 As New DataTable
            '    'dtSvcRecStaff1.Load(drSvcRecStaff1)

            '    'errcode = "19"
            '    ' '''''''''''''''''''''''''''''''''''''''''
            '    ''Retrieve ContractGroup from tblcontract
            '    ' '''''''''''''''''''''''''''''''''''''''''

            '    'Dim cmdContract As MySqlCommand = New MySqlCommand

            '    'cmdContract.CommandType = CommandType.Text

            '    'cmdContract.CommandText = "Select contractgroup,Agreevalue from tblcontract where contractno='" + dtSvcRec.Rows(i)("Contractno").ToString + "'"


            '    'cmdContract.Connection = conn

            '    'Dim drContract As MySqlDataReader = cmdContract.ExecuteReader()
            '    'Dim dtContract As New DataTable
            '    'dtContract.Load(drContract)

            '    'errcode = "20" + "-" + dtSvcRec.Rows(i)("Recordno").ToString + "-" + dtSvcRecStaff1.Rows.Count.ToString
            '    'If dtSvcRecStaff1.Rows.Count > 0 Then
            '    '    errcode = "21"
            '    '    For s As Integer = 0 To dtSvcRecStaff1.Rows.Count - 1
            '    '        errcode = "22" + "-" + dtSvcRecStaff1.Rows.Count.ToString + "-" + dtSvcRec.Rows(i)("RecordNo").ToString

            '    '        Dim command As MySqlCommand = New MySqlCommand

            '    '        command.CommandType = CommandType.Text
            '    '        Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report,CompanyGroup,ContractNo,LocateGrp)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report,@CompanyGroup,@ContractNo,@LocateGrp);"
            '    '        command.CommandText = qry
            '    '        command.Parameters.Clear()
            '    '        command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
            '    '        command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)
            '    '        'If txtActSvcDate.Text = "" Then
            '    '        '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
            '    '        'Else
            '    '        '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
            '    '        'End If
            '    '        errcode = "221" + "-" + dtSvcRecStaff1.Rows.Count.ToString + "-" + dtSvcRec.Rows(i)("RecordNo").ToString
            '    '        command.Parameters.AddWithValue("@ServiceDate", dtSvcRec.Rows(i)("ServiceDate"))
            '    '        command.Parameters.AddWithValue("@NoPerson", dtSvcRecStaff.Rows(0)("count(staffid)"))
            '    '        command.Parameters.AddWithValue("@Duration", dtSvcRec.Rows(i)("Duration"))
            '    '        command.Parameters.AddWithValue("@AccountID", dtSvcRec.Rows(i)("AccountID").ToString)
            '    '        command.Parameters.AddWithValue("@Client", dtSvcRec.Rows(i)("CustName").ToString)
            '    '        command.Parameters.AddWithValue("@TimeIn", dtSvcRec.Rows(i)("TimeIn"))
            '    '        command.Parameters.AddWithValue("@TimeOut", dtSvcRec.Rows(i)("Timeout"))
            '    '        command.Parameters.AddWithValue("@Service", targetid)
            '    '        command.Parameters.AddWithValue("@ServiceValue", dtSvcRec.Rows(i)("BillAmount"))
            '    '        command.Parameters.AddWithValue("@ServiceCost", servicecost)
            '    '        errcode = "222" + "-" + dtSvcRecStaff1.Rows.Count.ToString + "-" + dtSvcRec.Rows(i)("RecordNo").ToString
            '    '        'If dtSvcRecStaff.Rows(0)("count(staffid)") <> 0 Then
            '    '        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount") / dtSvcRecStaff.Rows(0)("count(staffid)"))
            '    '        'Else
            '    '        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount"))

            '    '        'End If
            '    '        command.Parameters.AddWithValue("@ManpowerCost", 0)
            '    '        '  command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
            '    '        command.Parameters.AddWithValue("@StaffID", dtSvcRecStaff1.Rows(s)("StaffID"))
            '    '        command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID").ToString)

            '    '        command.Parameters.AddWithValue("@NormalHour", otnormal)
            '    '        command.Parameters.AddWithValue("@OTHour", ot15hour + ot2hour)
            '    '        If dtContract.Rows.Count > 0 Then
            '    '            command.Parameters.AddWithValue("@ServDateType", dtContract.Rows(0)("ContractGroup").ToString)
            '    '        Else
            '    '            command.Parameters.AddWithValue("@ServDateType", "")
            '    '        End If
            '    '        errcode = "223" + "-" + dtSvcRecStaff1.Rows.Count.ToString + "-" + dtSvcRec.Rows(i)("RecordNo").ToString

            '    '        command.Parameters.AddWithValue("@OTRate", otRate)
            '    '        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
            '    '        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '        If rdbSelect.Text = "Detail" Then
            '    '            command.Parameters.AddWithValue("@Report", "ProdTeamMemberOTDet")
            '    '        ElseIf rdbSelect.Text = "Summary" Then
            '    '            command.Parameters.AddWithValue("@Report", "ProdTeamMemberOTSumm")

            '    '        End If
            '    '        errcode = "224" + "-" + dtSvcRecStaff1.Rows.Count.ToString + "-" + dtSvcRec.Rows(i)("RecordNo").ToString

            '    '        If dtContract.Rows.Count > 0 Then
            '    '            command.Parameters.AddWithValue("@CompanyGroup", Convert.ToString(dtContract.Rows(0)("Agreevalue")))
            '    '        Else
            '    '            command.Parameters.AddWithValue("@CompanyGroup", "0")
            '    '        End If
            '    '        command.Parameters.AddWithValue("@ContractNo", dtSvcRec.Rows(i)("ContractNo").ToString)
            '    '        command.Parameters.AddWithValue("@LocateGrp", dtSvcRec.Rows(i)("LocateGrp"))

            '    '        command.Connection = conn
            '    '        errcode = "225" + "-" + dtSvcRecStaff1.Rows.Count.ToString + "-" + dtSvcRec.Rows(i)("RecordNo").ToString
            '    '        command.ExecuteNonQuery()

            '    '        command.Dispose()

            '    '    Next


            '    'End If



            '    'drSvcRecDet.Close()
            '    'dtSvcRecDet.Clear()
            '    'drSvcRecStaff.Close()
            '    'dtSvcRecStaff.Clear()
            '    'drSvcRecStaff1.Close()
            '    'dtSvcRecStaff1.Clear()
            '    'cmdSvcRecStaff1.Dispose()

            '    '  Next

            'End If
            'errcode = "30"
            'dtSvcRec.Clear()
            'drSvcRec.Close()
            'conn.Close()
            'conn.Dispose()

            'stopwatch.[Stop]()
            'InsertIntoTblWebEventLog("PrintTest", qrySvcRec.ToString, stopwatch.ElapsedMilliseconds.ToString)
            'Return

            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)

            If chkGrouping.SelectedValue = "Date" Then
                If rdbSelect.SelectedValue = "Detail" Then
                    Session.Add("ReportType", "ManpowerProductivityByTeamMemberOTDetail")
                    Response.Redirect("RV_ManpowerProductivityByTeamMemberOTDetail.aspx")
                ElseIf rdbSelect.SelectedValue = "Summary" Then
                    Session.Add("ReportType", "ManpowerProductivityByTeamMemberOTSummary")
                    Response.Redirect("RV_ManpowerProductivityByTeamMemberOTSummary.aspx")

                End If
            ElseIf chkGrouping.SelectedValue = "Client" Then
                If rdbSelect.SelectedValue = "Detail" Then
                    Session.Add("ReportType", "ManpowerProductivityByTeamMemberOTDetail_Client")
                    Response.Redirect("RV_ManpowerProductivityByTeamMemberOTDetail_Client.aspx")
                ElseIf rdbSelect.SelectedValue = "Summary" Then
                    Session.Add("ReportType", "ManpowerProductivityByTeamMemberOTSummary_Client")
                    Response.Redirect("RV_ManpowerProductivityByTeamMemberOTSummary_Client.aspx")

                End If
            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("btnPrintServiceRecordList_Click", ex.Message.ToString, errcode)
        End Try
    End Sub

    Private Sub GetData()
        '  Try


        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        If rdbSelect.Text = "Detail" Then
            command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberOTDet';"

        ElseIf rdbSelect.Text = "Summary" Then
            command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberOTSumm';"

        End If


        command2.Connection = conn

        command2.ExecuteNonQuery()
        command2.Dispose()

        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = "SaveProductivityByTeamMemberOT"
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))

        command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
        If rdbSelect.Text = "Detail" Then
            command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberOTDet")
        ElseIf rdbSelect.Text = "Summary" Then
            command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberOTSumm")

        End If
        command.Connection = conn
        command.ExecuteScalar()

        command.Dispose()

        ''''''''''Update OT CALCULATION''''''''''''''''''''

        Dim ot2hour As Decimal = 0

        Dim ot15hour As Decimal = 0
        Dim otnormal As Decimal = 0
        Dim otRate As Decimal = 0
        Dim timediff As Decimal = 0

        Dim cmdSvcRec As MySqlCommand = New MySqlCommand

        cmdSvcRec.CommandType = CommandType.Text

        If rdbSelect.Text = "Detail" Then
            cmdSvcRec.CommandText = "Select * from tblrptserviceanalysis where Report='ProdTeamMemberOTDet' and CreatedBy='" + txtCreatedBy.Text + "' and serviceday not in ('Sunday','Holiday');"
        ElseIf rdbSelect.Text = "Summary" Then
            cmdSvcRec.CommandText = "Select * from tblrptserviceanalysis where Report='ProdTeamMemberOTSumm' and CreatedBy='" + txtCreatedBy.Text + "' and serviceday not in ('Sunday','Holiday');"

        End If

        cmdSvcRec.CommandTimeout = 0

        cmdSvcRec.Connection = conn

        Dim drSvcRec As MySqlDataReader = cmdSvcRec.ExecuteReader()
        Dim dtSvcRec As New DataTable
        dtSvcRec.Load(drSvcRec)

        If dtSvcRec.Rows.Count > 0 Then
            Dim svcdt As DateTime
            Dim svcday As String
            Dim timein As New DateTime
            Dim timeout As New DateTime
            Dim satstart As New DateTime
            Dim satend As New DateTime
            Dim weekdaystart As New DateTime
            Dim weekdayend As New DateTime

            Dim servicetime As TimeSpan

            ''''''''find saturday working time''''

            Dim cmdSvcSetup As MySqlCommand = New MySqlCommand

            cmdSvcSetup.CommandType = CommandType.Text

            cmdSvcSetup.CommandText = "Select satstart,satend from tblservicerecordmastersetup;"

            cmdSvcSetup.Connection = conn

            Dim drSvcSetup As MySqlDataReader = cmdSvcSetup.ExecuteReader()
            Dim dtSvcSetup As New DataTable
            dtSvcSetup.Load(drSvcSetup)

            If dtSvcSetup.Rows.Count > 0 Then

                satstart = Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart"))

                satend = Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd"))

            End If


            Dim cmdSvcSetup1 As MySqlCommand = New MySqlCommand

            cmdSvcSetup1.CommandType = CommandType.Text

            cmdSvcSetup1.CommandText = "Select weekdaystart,weekdayend from tblservicerecordmastersetup;"

            cmdSvcSetup1.Connection = conn

            Dim drSvcSetup1 As MySqlDataReader = cmdSvcSetup1.ExecuteReader()
            Dim dtSvcSetup1 As New DataTable
            dtSvcSetup1.Load(drSvcSetup1)

            If dtSvcSetup1.Rows.Count > 0 Then
                'Normal working hours on Weekday : WeekdayStart-8:30 and WeekdayEnd-5:30
                '   lblAlert.Text = dtSvcRec.Rows(i)("TimeIn").ToString + " " + dtSvcRec.Rows(i)("Timeout").ToString


                weekdaystart = Convert.ToDateTime(dtSvcSetup1.Rows(0)("WeekDayStart"))

                weekdayend = Convert.ToDateTime(dtSvcSetup1.Rows(0)("WeekDayEnd"))

            End If

            For i As Integer = 0 To dtSvcRec.Rows.Count - 1



                ''''''''''''''Normal and OT HOURS CALCULATION''''''
                ''''check for holidays''''''''''''




                svcdt = dtSvcRec.Rows(i)("ServiceDate")
                svcday = dtSvcRec.Rows(i)("ServiceDay")
                'If svcday = "Holiday" Then
                '    'ot2hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn")), Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut")))
                '    'otRate = 2
                'ElseIf svcday = "Sunday" Then
                '    ot2hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn")), Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut")))
                '    otRate = 2

                If svcday = "Saturday" Then
                    '  dtSvcSetup.Load(drSvcSetup)

                    ''''''''find saturday working time''''
                    If dtSvcSetup.Rows.Count > 0 Then

                        timein = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn"))

                        timeout = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut"))



                        'Normal working hours on Saturday : SatStart-8:30 and SatEnd-12:30

                        '  Dim dur As String

                        servicetime = DateTime.Parse(timeout).Subtract(DateTime.Parse(timein))

                        If servicetime.TotalMinutes < 0 Then
                            timeout = timeout.AddDays(1)
                        End If


                        If timein = satstart Then

                            If timeout > satstart Then
                                If timeout = satend Then
                                    otnormal = DateDiff(DateInterval.Minute, timein, timeout)
                                    otRate = 1.0
                                ElseIf timeout < satend Then 'Eg: TimeIn->8:30 and TimeOut->9:30
                                    otnormal = DateDiff(DateInterval.Minute, timein, timeout)
                                    otRate = 1.0
                                ElseIf timeout > satend Then 'Eg: TimeIn->8:30 and TimeOut->12:50
                                    ot15hour = DateDiff(DateInterval.Minute, satend, timeout)
                                    otnormal = DateDiff(DateInterval.Minute, satstart, satend)
                                    otRate = 1.5
                                End If
                            End If

                        ElseIf timein < satstart Then
                            If timeout <= satstart Then
                                ot15hour = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->7:00 and TimeOut->8:00
                                otRate = 1.5
                            ElseIf timeout >= satstart Then

                                If timeout <= satend Then 'Eg: TimeIn->7:50 and TimeOut->8:50
                                    otnormal = DateDiff(DateInterval.Minute, satend, timeout)
                                    ot15hour = DateDiff(DateInterval.Minute, timein, satstart)
                                    otRate = 1.5
                                ElseIf timeout >= satend Then 'Eg: TimeIn->7:50 and TimeOut->12:50
                                    ot15hour = DateDiff(DateInterval.Minute, timein, satstart) + DateDiff(DateInterval.Minute, satend, timeout)
                                    otnormal = DateDiff(DateInterval.Minute, satstart, satend)
                                    otRate = 1.5
                                End If
                            End If

                        ElseIf timein > satstart Then
                            If timeout <= satend Then
                                otnormal = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->10:50 and TimeOut->11:50
                                otRate = 1
                            ElseIf timeout >= satend Then
                                If timein <= satend Then 'Eg: TimeIn->11:50 and TimeOut->12:50
                                    ot15hour = DateDiff(DateInterval.Minute, satend, timeout)

                                    otnormal = DateDiff(DateInterval.Minute, timein, satend)

                                    otRate = 1.5
                                ElseIf timein >= satend Then 'Eg: TimeIn->1:50 and TimeOut->2:50
                                    ot15hour = DateDiff(DateInterval.Minute, timein, timeout)

                                    otRate = 1.5
                                End If
                            End If

                        End If
                    End If


                Else

                    ''''''''find weekdays working time''''
                    '    dtSvcSetup1.Load(drSvcSetup1)


                    If dtSvcSetup1.Rows.Count > 0 Then
                        'Normal working hours on Weekday : WeekdayStart-8:30 and WeekdayEnd-5:30
                        '   lblAlert.Text = dtSvcRec.Rows(i)("TimeIn").ToString + " " + dtSvcRec.Rows(i)("Timeout").ToString

                        timein = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn"))

                        timeout = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut"))


                        servicetime = DateTime.Parse(timeout).Subtract(DateTime.Parse(timein))

                        If servicetime.TotalMinutes < 0 Then
                            timeout = timeout.AddDays(1)
                        End If


                        If timein = weekdaystart Then

                            If timeout > weekdaystart Then
                                If timeout = weekdayend Then
                                    otnormal = DateDiff(DateInterval.Minute, timein, timeout)
                                    otRate = 1.0

                                ElseIf timeout < weekdayend Then 'Eg: TimeIn->8:30 and TimeOut->9:30
                                    otnormal = DateDiff(DateInterval.Minute, timein, timeout)
                                    otRate = 1.0
                                ElseIf timeout > weekdayend Then 'Eg: TimeIn->8:30 and TimeOut->12:50
                                    ot15hour = DateDiff(DateInterval.Minute, weekdayend, timeout)
                                    otnormal = DateDiff(DateInterval.Minute, weekdaystart, weekdayend)
                                    otRate = 1.5
                                End If
                            End If

                        ElseIf timein < weekdaystart Then

                            If timeout <= weekdaystart Then
                                ot15hour = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->7:00 and TimeOut->8:00
                                'Eg: TimeIn->7:00 and TimeOut->8:00
                                otRate = 1.5
                            ElseIf timeout >= weekdaystart Then

                                If timeout <= weekdayend Then 'Eg: TimeIn->7:50 and TimeOut->8:50

                                    '  otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(i)("WeekDayEnd")), Convert.ToDateTime(dtSvcRec.Rows(0)("TimeOut")))
                                    otnormal = DateDiff(DateInterval.Minute, weekdaystart, timeout)

                                    ot15hour = DateDiff(DateInterval.Minute, timein, weekdaystart)
                                    otRate = 1.5
                                ElseIf timeout >= weekdayend Then 'Eg: TimeIn->7:50 and TimeOut->12:50
                                    ot15hour = DateDiff(DateInterval.Minute, timein, weekdaystart) + DateDiff(DateInterval.Minute, weekdayend, timeout)
                                    otnormal = DateDiff(DateInterval.Minute, weekdaystart, weekdayend)
                                    otRate = 1.5
                                End If
                            End If

                        ElseIf timein > weekdaystart Then

                            If timeout <= weekdayend Then
                                otnormal = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->10:50 and TimeOut->11:50
                                otRate = 1
                            ElseIf timeout >= weekdayend Then
                                If timein <= weekdayend Then 'Eg: TimeIn->11:50 and TimeOut->12:50
                                    ot15hour = DateDiff(DateInterval.Minute, weekdayend, timeout)
                                    otnormal = DateDiff(DateInterval.Minute, timein, weekdayend)
                                    otRate = 1.5
                                ElseIf timein >= weekdayend Then 'Eg: TimeIn->1:50 and TimeOut->2:50
                                    ot15hour = DateDiff(DateInterval.Minute, timein, timeout)
                                    otRate = 1.5
                                End If
                            End If
                        End If
                    End If


                End If


                'Update OT CALCULATION IN TEMP TABLE''

                Dim commandUpdate As MySqlCommand = New MySqlCommand

                commandUpdate.CommandType = CommandType.Text

                If rdbSelect.Text = "Detail" Then
                    commandUpdate.CommandText = "update tblrptserviceanalysis set otrate=@OTRate,othour=@OTHour,NormalHour=@NormalHour where Report='ProdTeamMemberOTDet' and CreatedBy='" + txtCreatedBy.Text + "' AND RCNO=@RCNO;"
                ElseIf rdbSelect.Text = "Summary" Then
                    commandUpdate.CommandText = "update tblrptserviceanalysis set otrate=@OTRate,othour=@OTHour,NormalHour=@NormalHour where Report='ProdTeamMemberOTSumm' and CreatedBy='" + txtCreatedBy.Text + "' AND RCNO=@RCNO;"

                End If

                commandUpdate.Parameters.Clear()

                commandUpdate.Parameters.AddWithValue("@OTHour", ot15hour + ot2hour)
                commandUpdate.Parameters.AddWithValue("@OTRate", otRate)
                commandUpdate.Parameters.AddWithValue("@NormalHour", otnormal)
                commandUpdate.Parameters.AddWithValue("@RCNO", dtSvcRec.Rows(i)("Rcno"))



                commandUpdate.Connection = conn

                commandUpdate.ExecuteNonQuery()
            Next

            dtSvcSetup.Clear()
            dtSvcSetup.Dispose()

            dtSvcSetup1.Clear()
            dtSvcSetup1.Dispose()

            drSvcSetup.Close()
            drSvcSetup1.Close()

            cmdSvcSetup.Dispose()
            cmdSvcSetup1.Dispose()

        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        dtSvcRec.Clear()
        dtSvcRec.Dispose()
        drSvcRec.Close()
        cmdSvcRec.Dispose()

        conn.Close()
        conn.Dispose()
        'Catch ex As Exception
        '    Dim st As New StackTrace(True)
        '    st = New StackTrace(ex, True)
        '    InsertIntoTblWebEventLog("GetData()", ex.Message.ToString, st.GetFrame(0).GetFileLineNumber().ToString)
        'End Try

    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID

        txtCreatedOn.Attributes.Add("readonly", "readonly")
        If Not IsPostBack Then
            SqlDSStaffDept.SelectCommand = "Select distinct department from tblstaff group by department"
            SqlDSStaffDept.DataBind()
            ddlStaffDept.DataBind()

        End If

    End Sub


End Class
