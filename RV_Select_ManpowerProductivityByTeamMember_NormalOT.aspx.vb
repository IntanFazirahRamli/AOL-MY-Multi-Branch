
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel
Imports System.IO



Partial Class RV_Select_ManpowerProductivityByTeamMember_NormalOT
    Inherits System.Web.UI.Page



    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.SelectedIndex = 0
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
        commandEventLog.Parameters.AddWithValue("@DocRef", "MANPOWER-TEAMMEMBERNORMALOT")
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

        Dim qrySvcRec As String = "SELECT * FROM tblservicerecord where status='P' and servicedate is not null and (timein is not null and timeout is not null) and (timein <>"" and timeout <> "") and"
        qrySvcRec = qrySvcRec + " (timein<>'0' and timeout<>'0') and timein<>'  :' and timeout<>'  :'"
        Dim selection As String
        selection = ""

        Dim selFormula As String
        If rdbSelect.Text = "Detail" Then
            selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberNormalOTDet' AND {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

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
            selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberNormalOTSumm' AND {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

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
        'If txtServiceID.Text = "-1" Then
        'Else

        '    selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} = '" + txtServiceID.SelectedItem.Text + "'"
        '    If selection = "" Then
        '        selection = "ServiceID = " + txtServiceID.SelectedItem.Text
        '    Else
        '        selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
        '    End If
        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
            selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} in [" + YrStr + "]"
          
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
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

        GetData1()

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

        'If dtSvcRec.Rows.Count > 0 Then
        '    Dim command2 As MySqlCommand = New MySqlCommand

        '    command2.CommandType = CommandType.Text
        '    If rdbSelect.Text = "Detail" Then
        '        command2.CommandText = "delete from tblrptserviceanalysis where Report='ProdTeamMemberNormalOTDet' and CreatedBy='" + txtCreatedBy.Text + "';"

        '    ElseIf rdbSelect.Text = "Summary" Then
        '        command2.CommandText = "delete from tblrptserviceanalysis where Report='ProdTeamMemberNormalOTSumm' and CreatedBy='" + txtCreatedBy.Text + "';"

        '    End If

        '    command2.Connection = conn

        '    command2.ExecuteNonQuery()

        '    For i As Integer = 0 To dtSvcRec.Rows.Count - 1
        '        Dim cmdSvcRecStaff As MySqlCommand = New MySqlCommand

        '        cmdSvcRecStaff.CommandType = CommandType.Text

        '        cmdSvcRecStaff.CommandText = "Select count(staffid),staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


        '        cmdSvcRecStaff.Connection = conn

        '        Dim drSvcRecStaff As MySqlDataReader = cmdSvcRecStaff.ExecuteReader()
        '        Dim dtSvcRecStaff As New DataTable
        '        dtSvcRecStaff.Load(drSvcRecStaff)

        '        'Dim staffid As String = ""
        '        'If dtSvcRecStaff.Rows.Count > 0 Then
        '        '    For k As Integer = 0 To dtSvcRecStaff.Rows.Count - 1
        '        '        If staffid <> "" Then
        '        '            staffid = staffid + "," + dtSvcRecStaff.Rows(k)("Staffid").ToString
        '        '        End If
        '        '    Next
        '        'End If
        '        Dim cmdSvcRecDet As MySqlCommand = New MySqlCommand

        '        cmdSvcRecDet.CommandType = CommandType.Text

        '        If txtServiceID.Text = "-1" Then
        '            cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"
        '        Else
        '            cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "' and serviceid='" + txtServiceID.SelectedItem.Text + "'"

        '        End If


        '        cmdSvcRecDet.Connection = conn

        '        Dim drSvcRecDet As MySqlDataReader = cmdSvcRecDet.ExecuteReader()
        '        Dim dtSvcRecDet As New DataTable
        '        dtSvcRecDet.Load(drSvcRecDet)
        '        Dim targetid As String = ""
        '        Dim servicecost As Decimal = 0

        '        If dtSvcRecDet.Rows.Count > 0 Then

        '            For j As Integer = 0 To dtSvcRecDet.Rows.Count - 1
        '                If dtSvcRecDet.Rows(j)("TargetID").ToString <> DBNull.Value.ToString Then
        '                    If targetid = "" Then
        '                        targetid = dtSvcRecDet.Rows(j)("TargetID")
        '                    Else

        '                        targetid = targetid + "," + dtSvcRecDet.Rows(j)("TargetID")
        '                    End If
        '                End If

        '                If dtSvcRecDet.Rows(j)("ServiceCost").ToString = DBNull.Value.ToString Then
        '                Else

        '                    servicecost = servicecost + dtSvcRecDet.Rows(j)("ServiceCost")

        '                End If

        '            Next
        '        End If


        '        ''''''''''''''Normal and OT HOURS CALCULATION''''''
        '        ''''check for holidays''''''''''''
        '        Dim ot2hour As Decimal = 0

        '        Dim ot15hour As Decimal = 0
        '        Dim otnormal As Decimal = 0
        '        Dim otRate As Decimal = 0
        '        Dim timediff As Decimal = 0

        '        Dim svcdt As DateTime

        '        svcdt = dtSvcRec.Rows(i)("ServiceDate")

        '        '    Dim dtSvcSetup As New DataTable
        '        Dim cmdHoliday As MySqlCommand = New MySqlCommand

        '        cmdHoliday.CommandType = CommandType.Text

        '        cmdHoliday.CommandText = "Select holiday from tblholiday where holiday = '" + svcdt.ToString("yyyy-MM-dd") + "'"

        '        cmdHoliday.Connection = conn

        '        Dim drHoliday As MySqlDataReader = cmdHoliday.ExecuteReader()
        '        Dim dtHoliday As New DataTable
        '        dtHoliday.Load(drHoliday)

        '        If ((dtSvcRec.Rows(i)("TimeIn").ToString <> "" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "") And (String.IsNullOrEmpty(dtSvcRec.Rows(i)("TimeIn").ToString) = False Or String.IsNullOrEmpty(dtSvcRec.Rows(i)("TimeOut").ToString) = False) And (dtSvcRec.Rows(i)("TimeIn").ToString <> "0" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "0") And (dtSvcRec.Rows(i)("TimeIn").ToString <> "  :" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "  :")) Then
        '            If dtHoliday.Rows.Count > 0 Then
        '                ot2hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn")), Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut")))
        '                otRate = 2
        '            Else
        '                ''''Check for Saturday and Sunday'''''


        '                If svcdt.DayOfWeek.ToString = "Sunday" Then
        '                    ot2hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn")), Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut")))
        '                    otRate = 2
        '                ElseIf svcdt.DayOfWeek.ToString = "Saturday" Then

        '                    ''''''''find saturday working time''''

        '                    Dim cmdSvcSetup As MySqlCommand = New MySqlCommand

        '                    cmdSvcSetup.CommandType = CommandType.Text

        '                    cmdSvcSetup.CommandText = "Select satstart,satend from tblservicerecordmastersetup;"

        '                    cmdSvcSetup.Connection = conn

        '                    Dim drSvcSetup As MySqlDataReader = cmdSvcSetup.ExecuteReader()
        '                    Dim dtSvcSetup As New DataTable
        '                    dtSvcSetup.Load(drSvcSetup)

        '                    If dtSvcSetup.Rows.Count > 0 Then
        '                        Dim timein As New DateTime
        '                        timein = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn"))
        '                        Dim timeout As New DateTime
        '                        timeout = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut"))
        '                        Dim satstart As New DateTime
        '                        satstart = Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart"))
        '                        Dim satend As New DateTime
        '                        satend = Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd"))

        '                        'Normal working hours on Saturday : SatStart-8:30 and SatEnd-12:30
        '                        Dim servicetime As TimeSpan
        '                        '  Dim dur As String

        '                        servicetime = DateTime.Parse(timeout).Subtract(DateTime.Parse(timein))

        '                        If servicetime.TotalMinutes < 0 Then
        '                            timeout = timeout.AddDays(1)
        '                        End If


        '                        If timein = satstart Then

        '                            If timeout > satstart Then
        '                                If timeout = satend Then
        '                                    otnormal = DateDiff(DateInterval.Minute, timein, timeout)
        '                                    otRate = 1.0
        '                                ElseIf timeout < satend Then 'Eg: TimeIn->8:30 and TimeOut->9:30
        '                                    otnormal = DateDiff(DateInterval.Minute, timein, timeout)
        '                                    otRate = 1.0
        '                                ElseIf timeout > satend Then 'Eg: TimeIn->8:30 and TimeOut->12:50
        '                                    ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
        '                                    otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))
        '                                    otRate = 1.5
        '                                End If
        '                            End If

        '                        ElseIf timein < satstart Then
        '                            If timeout <= satstart Then
        '                                ot15hour = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->7:00 and TimeOut->8:00
        '                                otRate = 1.5
        '                            ElseIf timeout >= satstart Then

        '                                If timeout <= satend Then 'Eg: TimeIn->7:50 and TimeOut->8:50
        '                                    otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
        '                                    ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")))
        '                                    otRate = 1.5
        '                                ElseIf timeout >= satend Then 'Eg: TimeIn->7:50 and TimeOut->12:50
        '                                    ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart"))) + DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
        '                                    otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))
        '                                    otRate = 1.5
        '                                End If
        '                            End If

        '                        ElseIf timein > satstart Then
        '                            If timeout <= satend Then
        '                                otnormal = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->10:50 and TimeOut->11:50
        '                                otRate = 1
        '                            ElseIf timeout >= satend Then
        '                                If timein <= satend Then 'Eg: TimeIn->11:50 and TimeOut->12:50
        '                                    ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)

        '                                    otnormal = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))

        '                                    otRate = 1.5
        '                                ElseIf timein >= satend Then 'Eg: TimeIn->1:50 and TimeOut->2:50
        '                                    ot15hour = DateDiff(DateInterval.Minute, timein, timeout)

        '                                    otRate = 1.5
        '                                End If
        '                            End If

        '                        End If

        '                    End If
        '                Else


        '                    ''''''''find weekdays working time''''

        '                    Dim cmdSvcSetup As MySqlCommand = New MySqlCommand

        '                    cmdSvcSetup.CommandType = CommandType.Text

        '                    cmdSvcSetup.CommandText = "Select weekdaystart,weekdayend from tblservicerecordmastersetup;"

        '                    cmdSvcSetup.Connection = conn

        '                    Dim drSvcSetup As MySqlDataReader = cmdSvcSetup.ExecuteReader()
        '                    Dim dtSvcSetup As New DataTable
        '                    dtSvcSetup.Load(drSvcSetup)

        '                    If dtSvcSetup.Rows.Count > 0 Then
        '                        'Normal working hours on Weekday : WeekdayStart-8:30 and WeekdayEnd-5:30
        '                        '   lblAlert.Text = dtSvcRec.Rows(i)("TimeIn").ToString + " " + dtSvcRec.Rows(i)("Timeout").ToString
        '                        Dim timein As New DateTime
        '                        timein = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn"))
        '                        Dim timeout As New DateTime
        '                        timeout = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut"))
        '                        Dim weekdaystart As New DateTime
        '                        weekdaystart = Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart"))
        '                        Dim weekdayend As New DateTime
        '                        weekdayend = Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd"))

        '                        Dim servicetime As TimeSpan
        '                        '  Dim dur As String

        '                        servicetime = DateTime.Parse(timeout).Subtract(DateTime.Parse(timein))

        '                        If servicetime.TotalMinutes < 0 Then
        '                            timeout = timeout.AddDays(1)
        '                        End If


        '                        If timein = weekdaystart Then

        '                            If timeout > weekdaystart Then
        '                                If timeout = weekdayend Then
        '                                    otnormal = DateDiff(DateInterval.Minute, timein, timeout)
        '                                    otRate = 1.0

        '                                ElseIf timeout < weekdayend Then 'Eg: TimeIn->8:30 and TimeOut->9:30
        '                                    otnormal = DateDiff(DateInterval.Minute, timein, timeout)
        '                                    otRate = 1.0
        '                                ElseIf timeout > weekdayend Then 'Eg: TimeIn->8:30 and TimeOut->12:50
        '                                    ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
        '                                    otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
        '                                    otRate = 1.5
        '                                End If
        '                            End If

        '                        ElseIf timein < weekdaystart Then

        '                            If timeout <= weekdaystart Then
        '                                ot15hour = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->7:00 and TimeOut->8:00
        '                                'Eg: TimeIn->7:00 and TimeOut->8:00
        '                                otRate = 1.5
        '                            ElseIf timeout >= weekdaystart Then

        '                                If timeout <= weekdayend Then 'Eg: TimeIn->7:50 and TimeOut->8:50

        '                                    '  otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(i)("WeekDayEnd")), Convert.ToDateTime(dtSvcRec.Rows(0)("TimeOut")))
        '                                    otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), timeout)

        '                                    ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")))
        '                                    otRate = 1.5
        '                                ElseIf timeout >= weekdayend Then 'Eg: TimeIn->7:50 and TimeOut->12:50
        '                                    ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart"))) + DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
        '                                    otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
        '                                    otRate = 1.5
        '                                End If
        '                            End If

        '                        ElseIf timein > weekdaystart Then

        '                            If timeout <= weekdayend Then
        '                                otnormal = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->10:50 and TimeOut->11:50
        '                                otRate = 1
        '                            ElseIf timeout >= weekdayend Then
        '                                If dtSvcRec.Rows(i)("TimeIn") <= dtSvcSetup.Rows(0)("WeekDayEnd") Then 'Eg: TimeIn->11:50 and TimeOut->12:50
        '                                    ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
        '                                    otnormal = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
        '                                    otRate = 1.5
        '                                ElseIf timein >= weekdayend Then 'Eg: TimeIn->1:50 and TimeOut->2:50
        '                                    ot15hour = DateDiff(DateInterval.Minute, timein, timeout)
        '                                    otRate = 1.5
        '                                End If
        '                            End If
        '                        End If
        '                    End If

        '                End If
        '            End If
        '        End If


        '        ''''''Delete records that exists already
        '        'Dim command1 As MySqlCommand = New MySqlCommand

        '        'command1.CommandType = CommandType.Text

        '        'command1.CommandText = "SELECT * FROM tblrptserviceanalysis where recordno='" + dtSvcRec.Rows(i)("RecordNo") + "'"
        '        'command1.Connection = conn

        '        'Dim dr As MySqlDataReader = command1.ExecuteReader()
        '        'Dim dt As New DataTable
        '        'dt.Load(dr)

        '        'If dt.Rows.Count > 0 Then




        '        'End If

        '        Dim cmdSvcRecStaff1 As MySqlCommand = New MySqlCommand

        '        cmdSvcRecStaff1.CommandType = CommandType.Text

        '        cmdSvcRecStaff1.CommandText = "Select staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


        '        cmdSvcRecStaff1.Connection = conn

        '        Dim drSvcRecStaff1 As MySqlDataReader = cmdSvcRecStaff1.ExecuteReader()
        '        Dim dtSvcRecStaff1 As New DataTable
        '        dtSvcRecStaff1.Load(drSvcRecStaff1)


        '        '''''''''''''''''''''''''''''''''''''''''
        '        'Retrieve ContractGroup from tblcontract
        '        '''''''''''''''''''''''''''''''''''''''''

        '        Dim cmdContract As MySqlCommand = New MySqlCommand

        '        cmdContract.CommandType = CommandType.Text

        '        cmdContract.CommandText = "Select contractgroup from tblcontract where contractno='" + dtSvcRec.Rows(i)("Contractno").ToString + "'"


        '        cmdContract.Connection = conn

        '        Dim drContract As MySqlDataReader = cmdContract.ExecuteReader()
        '        Dim dtContract As New DataTable
        '        dtContract.Load(drContract)


        '        If dtSvcRecStaff1.Rows.Count > 0 Then
        '            For s As Integer = 0 To dtSvcRecStaff1.Rows.Count - 1

        '                Dim command As MySqlCommand = New MySqlCommand

        '                command.CommandType = CommandType.Text
        '                Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report,Status,CompanyGroup,LocateGrp)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report,@Status,@CompanyGroup,@LocateGrp);"
        '                command.CommandText = qry
        '                command.Parameters.Clear()
        '                command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
        '                command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)
        '                'If txtActSvcDate.Text = "" Then
        '                '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
        '                'Else
        '                '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
        '                'End If
        '                command.Parameters.AddWithValue("@ServiceDate", dtSvcRec.Rows(i)("ServiceDate"))
        '                command.Parameters.AddWithValue("@NoPerson", dtSvcRecStaff.Rows(0)("count(staffid)"))
        '                command.Parameters.AddWithValue("@Duration", dtSvcRec.Rows(i)("Duration"))
        '                command.Parameters.AddWithValue("@AccountID", dtSvcRec.Rows(i)("AccountID").ToString)
        '                command.Parameters.AddWithValue("@Client", dtSvcRec.Rows(i)("CustName").ToString)
        '                command.Parameters.AddWithValue("@TimeIn", dtSvcRec.Rows(i)("TimeIn"))
        '                command.Parameters.AddWithValue("@TimeOut", dtSvcRec.Rows(i)("Timeout"))
        '                command.Parameters.AddWithValue("@Service", targetid)
        '                command.Parameters.AddWithValue("@ServiceValue", dtSvcRec.Rows(i)("BillAmount"))
        '                command.Parameters.AddWithValue("@ServiceCost", servicecost)
        '                'If dtSvcRecStaff.Rows(0)("count(staffid)") <> 0 Then
        '                '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount") / dtSvcRecStaff.Rows(0)("count(staffid)"))
        '                'Else
        '                '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount"))

        '                'End If
        '                command.Parameters.AddWithValue("@ManpowerCost", 0)
        '                '  command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
        '                command.Parameters.AddWithValue("@StaffID", dtSvcRecStaff1.Rows(s)("StaffID"))
        '                command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID").ToString)

        '                command.Parameters.AddWithValue("@NormalHour", otnormal)
        '                command.Parameters.AddWithValue("@OTHour", ot15hour + ot2hour)
        '                If dtContract.Rows.Count > 0 Then
        '                    command.Parameters.AddWithValue("@ServDateType", dtContract.Rows(0)("ContractGroup").ToString)
        '                Else
        '                    command.Parameters.AddWithValue("@ServDateType", "")
        '                End If

        '                command.Parameters.AddWithValue("@OTRate", otRate)
        '                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
        '                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
        '                If rdbSelect.Text = "Detail" Then
        '                    command.Parameters.AddWithValue("@Report", "ProdTeamMemberNormalOTDet")
        '                ElseIf rdbSelect.Text = "Summary" Then
        '                    command.Parameters.AddWithValue("@Report", "ProdTeamMemberNormalOTSumm")

        '                End If
        '                command.Parameters.AddWithValue("@Status", dtSvcRec.Rows(i)("Status"))
        '                command.Parameters.AddWithValue("@CompanyGroup", dtSvcRec.Rows(i)("CompanyGroup"))
        '                command.Parameters.AddWithValue("@LocateGrp", dtSvcRec.Rows(i)("LocateGrp"))
        '                command.Connection = conn

        '                command.ExecuteNonQuery()

        '                command.Dispose()

        '            Next


        '        End If



        '        drSvcRecDet.Close()
        '        dtSvcRecDet.Clear()
        '        drSvcRecStaff.Close()
        '        dtSvcRecStaff.Clear()
        '        drSvcRecStaff1.Close()
        '        dtSvcRecStaff1.Clear()
        '        cmdSvcRecStaff1.Dispose()

        '    Next

        'End If

        'dtSvcRec.Clear()
        'drSvcRec.Close()
        'conn.Close()
        'conn.Dispose()

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If rdbSelect.SelectedValue = "Detail" Then
            Session.Add("ReportType", "ManpowerProductivityByTeamMemberNormalOTDetail")
            Response.Redirect("RV_ManpowerProductivityByTeamMemberNormalOTDetail.aspx")
        ElseIf rdbSelect.SelectedValue = "Summary" Then
            Session.Add("ReportType", "ManpowerProductivityByTeamMemberNormalOTSummary")
            Response.Redirect("RV_ManpowerProductivityByTeamMemberNormalOTSummary.aspx")

        End If

    End Sub

    Private Sub GetData1()
        '  Try


        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

       
        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.StoredProcedure
       
            If chkTimeSheet.Checked Then
                If rdbSelect.SelectedValue = "Detail" Then
                    command.CommandText = "SaveProductivityByTeamMemberOTNewTimeSheet"
                ElseIf rdbSelect.SelectedValue = "Summary" Then
                    command.CommandText = "SaveProductivityByTeamMemberOTNew1TimeSheet"
                End If

                ' InsertIntoTblWebEventLog("SaveProductivityByTeamMemberTimeSheet", "GetData", chkTimeSheet.Checked.ToString, txtCreatedBy.Text)
            Else
                command.CommandText = "SaveProductivityByTeamMemberOTNew"
                '  InsertIntoTblWebEventLog("SaveProductivityByTeamMember", "GetData", chkTimeSheet.Checked.ToString, txtCreatedBy.Text)
            End If

            command.Parameters.Clear()

            command.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))

            command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
            If rdbSelect.Text = "Detail" Then
                command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberNormalOTDet")
            ElseIf rdbSelect.Text = "Summary" Then
                command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberNormalOTSumm")

            End If
            command.Connection = conn
            command.ExecuteScalar()

            command.Dispose()


            'DELETE RECORDS THAT DO NOT BELONG TO THE SELECTED CONTRACT GROUP
            Dim YrStrList2 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In txtServiceID.Items
                If item.Selected Then

                    YrStrList2.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList2.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)


                Dim command21 As MySqlCommand = New MySqlCommand

                command21.CommandType = CommandType.Text

                If rdbSelect.SelectedValue = "Detail" Then
                command21.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberNormalOTDet' and servdatetype NOT in (" + YrStr + ")"
                ElseIf rdbSelect.SelectedValue = "Summary" Then
                command21.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberNormalOTSumm' and servdatetype NOT in (" + YrStr + ")"
                End If

                command21.Connection = conn

                command21.ExecuteNonQuery()
                command21.Dispose()


            End If



            ''''''''''Update OT CALCULATION''''''''''''''''''''

            Dim ot2hour As Decimal = 0

            Dim ot15hour As Decimal = 0
            Dim otnormal As Decimal = 0
            Dim otRate As Decimal = 0
            Dim timediff As Decimal = 0

            Dim cmdSvcRec As MySqlCommand = New MySqlCommand

            cmdSvcRec.CommandType = CommandType.Text


            If rdbSelect.Text = "Detail" Then
                cmdSvcRec.CommandText = "Select * from tblrptserviceanalysis where Report='ProdTeamMemberNormalOTDet' and CreatedBy='" + txtCreatedBy.Text + "'  and TIME_TO_SEC(timediff(timeout,timein))/60<0 and serviceday not in ('Sunday','Holiday');"
            ElseIf rdbSelect.Text = "Summary" Then
                cmdSvcRec.CommandText = "Select * from tblrptserviceanalysis where Report='ProdTeamMemberNormalOTSumm' and CreatedBy='" + txtCreatedBy.Text + "'  and TIME_TO_SEC(timediff(timeout,timein))/60<0 and serviceday not in ('Sunday','Holiday');"

            End If

            'If rdbSelect.Text = "Detail" Then
            '    cmdSvcRec.CommandText = "Select * from tblrptserviceanalysis where Report='ProdTeamMemberNormalOTDet' and CreatedBy='" + txtCreatedBy.Text + "' and serviceday not in ('Sunday','Holiday');"
            'ElseIf rdbSelect.Text = "Summary" Then
            '    cmdSvcRec.CommandText = "Select * from tblrptserviceanalysis where Report='ProdTeamMemberNormalOTSumm' and CreatedBy='" + txtCreatedBy.Text + "' and serviceday not in ('Sunday','Holiday');"

            'End If

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
                        commandUpdate.CommandText = "update tblrptserviceanalysis set otrate=@OTRate,othour=@OTHour,NormalHour=@NormalHour where Report='ProdTeamMemberNormalOTDet' and CreatedBy='" + txtCreatedBy.Text + "' AND RCNO=@RCNO;"
                    ElseIf rdbSelect.Text = "Summary" Then
                        commandUpdate.CommandText = "update tblrptserviceanalysis set otrate=@OTRate,othour=@OTHour,NormalHour=@NormalHour where Report='ProdTeamMemberNormalOTSumm' and CreatedBy='" + txtCreatedBy.Text + "' AND RCNO=@RCNO;"

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

        Dim commandD As MySqlCommand = New MySqlCommand
        commandD.CommandType = CommandType.StoredProcedure
        If chkDistribution.Checked = True Then
                If rdbSelect.SelectedValue = "Detail" Then
                    commandD.CommandText = "SaveProductivityByTeamMemberOTNewTimeSheetDistribution"
                ElseIf rdbSelect.SelectedValue = "Summary" Then
                    commandD.CommandText = "SaveProductivityByTeamMemberOTNew1TimeSheetDistribution"
                End If
          
        End If

        commandD.Parameters.Clear()

        commandD.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
        commandD.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))

        commandD.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
        If rdbSelect.Text = "Detail" Then
            commandD.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberNormalOTDet")
        ElseIf rdbSelect.Text = "Summary" Then
            commandD.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberNormalOTSumm")

        End If
        commandD.Connection = conn
        commandD.ExecuteScalar()

        commandD.Dispose()

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

    'Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String)
    '    Dim workbook As IWorkbook

    '    If extension = "xlsx" Then
    '        workbook = New XSSFWorkbook()
    '    ElseIf extension = "xls" Then
    '        workbook = New HSSFWorkbook()
    '    Else
    '        Throw New Exception("This format is not supported")
    '    End If

    '    Dim sheet1 As ISheet = workbook.CreateSheet("Sheet 1")
    '    Dim row1 As IRow = sheet1.CreateRow(0)

    '    For j As Integer = 0 To dt.Columns.Count - 1
    '        Dim cell As ICell = row1.CreateCell(j)
    '        Dim columnName As String = dt.Columns(j).ToString()
    '        cell.SetCellValue(columnName)
    '    Next

    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        Dim row As IRow = sheet1.CreateRow(i + 1)

    '        For j As Integer = 0 To dt.Columns.Count - 1
    '            Dim cell As ICell = row.CreateCell(j)
    '            Dim columnName As String = dt.Columns(j).ToString()
    '            cell.SetCellValue(dt.Rows(i)(columnName).ToString())
    '        Next
    '    Next

    '    Using exportData = New MemoryStream()
    '        Response.Clear()
    '        workbook.Write(exportData)

    '        If extension = "xlsx" Then
    '            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '            Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "ManpowerProductivity.xlsx"))
    '            Response.BinaryWrite(exportData.ToArray())
    '        ElseIf extension = "xls" Then
    '            Response.ContentType = "application/vnd.ms-excel"
    '            Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "ManpowerProductivity.xls"))
    '            Response.BinaryWrite(exportData.GetBuffer())
    '        End If

    '        Response.[End]()
    '    End Using
    'End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()
            InsertIntoTblWebEventLog("Export", "", "")

            WriteExcelWithNPOI(dt, "xlsx")

            InsertIntoTblWebEventLog("Export1", "", "")
            Return

            '  Dim excel As OfficeOpenXml.ExcelPackage = New OfficeOpenXml.ExcelPackage()
            '  Dim workSheet = excel.Workbook.Worksheets.Add("Sheet1")

            '  workSheet.Cells(1, 1).LoadFromCollection(dt, True)
      
            Dim attachment As String = "attachment; filename=ManpowerProductivity.xlsx"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"


            'Dim attachment As String = "attachment; filename=ManpowerProductivity.xls"
            'Response.ClearContent()
            'Response.AddHeader("content-disposition", attachment)
            'Response.ContentType = "application/vnd.ms-excel"
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

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblwebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "Manpower Productivity NORMAT&OT")
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Function GetData() As Boolean

        Dim selection As String
        selection = ""
        Dim qry As String = ""
        If chkDistribution.Checked = True Then

            If rdbSelect.SelectedValue = "Detail" Then

                qry = "SELECT A.StaffID,RecordNo,ContractNo,Percentage,ServDateType as ContractGroup,ServiceDate as ServiceStartDate,TimeIn,ServiceEndDate,TimeOut,Duration as ServiceDuration,Client,Service,NoPerson,ReportServiceStart,time_format(reportservicestart,'%H:%i') as ReportServiceStartTime,"
                qry = qry + "ReportServiceEnd,time_format(reportserviceend,'%H:%i') as ReportServiceEndTime,ReportDuration,ServiceValue,BreakdownServiceValue,Productivity,NormalHourValue,"
                qry = qry + "concat(normalhour div 60,' Hours ',format(mod((normalHour/60)*60,60),0),' Mins') as 'NormalHourSpent (HH:MM)',"
                qry = qry + "normalhour as 'NormalHourSpent (Mins)',format(normalhour/60,2) as 'NormalHourSpent (Decimal)',"
                qry = qry + "OTHourValue,concat(OT15Hour div 60,' Hours ',format(mod((OT15Hour/60)*60,60),0),' Mins') as 'OT1.5HourSpent (HH:MM)',"
                qry = qry + "OT15Hour as 'OT1.5HourSpent (Mins)',format(OT15hour/60,2) as 'OT1.5HourSpent (Decimal)',"
                qry = qry + "concat(OT2Hour div 60,' Hours ',format(mod((OT2Hour/60)*60,60),0),' Mins') as 'OT2HourSpent (HH:MM)',"
                qry = qry + "OT2Hour as 'OT2HourSpent (Mins)',format(OT2hour/60,2) as 'OT2HourSpent (Decimal)'"
                qry = qry + ",C.StaffDepartment,C.LedgerCode,C.SubLedgerCode"
                qry = qry + " FROM tblrptserviceanalysis1 A left join tblstaff B on A.sTAFFid=B.StaffID"
                qry = qry + " LEFT JOIN tblstaffdepartment C ON B.Department=C.StaffDepartment where A.createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and A.staffid <> ''"

            ElseIf rdbSelect.SelectedValue = "Summary" Then
                qry = "SELECT StaffID,CountNormalSvcs as 'NoofNormalSvcs',NormalHourValue,"
                 qry = qry + "concat(normalhour div 60,' Hours ',format(mod((normalHour/60)*60,60),0),' Mins') as 'NormalHourSpent (HH:MM)',"
                qry = qry + "normalhour as 'NormalHourSpent (Mins)',format(normalhour/60,2) as 'NormalHourSpent (Decimal)',"
                qry = qry + "CountOTSvcs as NoofOTSvcs,OTHourValue,concat(OT15Hour div 60,' Hours ',format(mod((OT15Hour/60)*60,60),0),' Mins') as 'OT1.5HourSpent (HH:MM)',"
                qry = qry + "OT15Hour as 'OT1.5HourSpent (Mins)',format(OT15hour/60,2) as 'OT1.5HourSpent (Decimal)',"
                qry = qry + "concat(OT2Hour div 60,' Hours ',format(mod((OT2Hour/60)*60,60),0),' Mins') as 'OT2HourSpent (HH:MM)',"
                qry = qry + "OT2Hour as 'OT2HourSpent (Mins)',format(OT2hour/60,2) as 'OT2HourSpent (Decimal)',"
                qry = qry + "ServiceValue as TotalValue"
                qry = qry + " FROM tblrptserviceanalysis1 where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and staffid <> ''"

            End If

        Else

            If rdbSelect.SelectedValue = "Detail" Then

                qry = "SELECT A.StaffID,RecordNo,ContractNo,ServDateType as ContractGroup,ServiceDate as ServiceStartDate,MIN(TimeIn),ServiceEndDate,max(TimeOut),sum(Duration) as ServiceDuration,Client,Service,NoPerson,ReportServiceStart,time_format(reportservicestart,'%H:%i') as ReportServiceStartTime,"
                qry = qry + "ReportServiceEnd,time_format(reportserviceend,'%H:%i') as ReportServiceEndTime,ReportDuration,ServiceValue,format((ServiceValue-ManpowerCost)/NoPerson,2) as Productivity,"
                qry = qry + "ifnull((select format((ServiceValue-ManpowerCost)/NoPerson,2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid  and recordno=a.recordno group by staffid),0) as NormalHourValue,"
                qry = qry + "ifnull((select concat(sum(normalhour) div 60,' Hours ',format(mod((sum(normalHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid,recordno),'0 Hours 00 Mins') as 'NormalHourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(normalhour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'NormalHourSpent (Mins)',"
                qry = qry + "ifnull((select format(sum(normalhour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'NormalHourSpent (Decimal)',"

                qry = qry + "ifnull((select format((ServiceValue-ManpowerCost)/NoPerson,2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate in (1.5,2) and staffid=a.staffid  and recordno=a.recordno group by staffid),0) as OTHourValue,"
                qry = qry + "ifnull((select concat(sum(OTHour) div 60,' Hours ',format(mod((sum(OTHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'OT1.5HourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(OThour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT1.5HourSpent (Mins)',"
                qry = qry + "ifnull((select format(sum(OThour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT1.5HourSpent (Decimal)',"

                qry = qry + "ifnull((select concat(sum(OTHour) div 60,' Hours ',format(mod((sum(OTHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'OT2HourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(OThour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT2HourSpent (Mins)',"
                qry = qry + "ifnull((select format(sum(OThour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT2HourSpent (Decimal)'"
                qry = qry + ",C.StaffDepartment,C.LedgerCode,C.SubLedgerCode"
                qry = qry + " FROM tblrptserviceanalysis a left join tblstaff B on A.sTAFFid=B.StaffID"
                qry = qry + " LEFT JOIN tblstaffdepartment C ON B.Department=C.StaffDepartment where A.createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and A.staffid <> ''"

                'qry = "SELECT StaffID,ServiceDate,TimeIn,TimeOut,Client,Service,NoPerson,ReportServiceStart,time_format(reportservicestart,'%H:%i') as ReportServiceStartTime,"
                'qry = qry + "ReportServiceEnd,time_format(reportserviceend,'%H:%i') as ReportServiceEndTime,ServiceValue,format((ServiceValue-ManpowerCost)/NoPerson,2) as Productivity,"
                'qry = qry + "ifnull((select format(SUM(servicevalue),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid),0) GROUP BY STAFFID as NormalHourValue,"
                ''   qry = qry + "concat(format(normalhour/60,0),' Hours ',format(mod((normalhour/60)*60,60),0),' Mins') as NormalHourSpent,"
                'qry = qry + "ifnull((select concat(normalhour div 60,' Hours ',format(mod((normalHour/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid and recordno=a.recordno),'0 Hours 00 Mins') as NormalHourSpent,"

                'qry = qry + "ifnull((select format((SUM(servicevalue),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate in (1.5,2) and staffid=a.staffid),0) GROUP BY STAFFID as OTHourValue,"

                'qry = qry + "ifnull((select concat(OTHour div 60,' Hours ',format(mod((OTHour/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno),'0 Hours 00 Mins') as OT15HourSpent,"
                'qry = qry + "ifnull((select concat(OTHour div 60,' Hours ',format(mod((OTHour/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=2 and staffid=a.staffid and recordno=a.recordno),'0 Hours 00 Mins') as OT2HourSpent"
                ' qry = qry + " FROM tblrptserviceanalysis a where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and staffid <> ''"

            ElseIf rdbSelect.SelectedValue = "Summary" Then

                qry = "SELECT StaffID,"
                qry = qry + "ifnull((select count(staffid) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid"
                qry = qry + " group by staffid),0) as 'NoofNormalSvcs',"

                qry = qry + "ifnull((select format(sum((ServiceValue-ManpowerCost)/NoPerson),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid"
                qry = qry + " group by staffid),0) as NormalHourValue,"

                '  qry = qry + "format(sum(normalhour/60),2) as NormalHourSpent,"

                qry = qry + "ifnull((select concat(sum(normalhour) div 60,' Hours ',format(mod((sum(normalHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'NormalHourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(normalhour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'NormalHourSpent (Mins)',"
                qry = qry + "ifnull((select format(sum(normalhour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'NormalHourSpent (Decimal)',"


                qry = qry + "ifnull((select count(staffid) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2) and staffid=a.staffid"
                qry = qry + " group by staffid),0) as NoofOTSvcs,"
                qry = qry + "ifnull((select format(sum((ServiceValue-ManpowerCost)/NoPerson),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2) and staffid=a.staffid"
                qry = qry + " group by staffid),0) as OTHourValue,"
                qry = qry + "ifnull((select concat(sum(othour) div 60,' Hours ',format(mod((sum(otHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'OT1.5HourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(othour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT1.5HourSpent (Mins)',"

                qry = qry + "ifnull((select format(sum(OThour/60),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid"
                qry = qry + " group by staffid),0) as 'OT1.5HourSpent (Decimal)',"
                qry = qry + "ifnull((select concat(sum(othour) div 60,' Hours ',format(mod((sum(otHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'OT2HourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(othour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT2HourSpent (Mins)',"

                qry = qry + "ifnull((select format(sum(OThour/60),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid"
                qry = qry + " group by staffid),0) as 'OT2.0HourSpent (Decimal)',"
                qry = qry + "ifnull((select format(sum((ServiceValue-ManpowerCost)/NoPerson),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1,1.5,2) and staffid=a.staffid"
                qry = qry + " group by staffid),0) as TotalValue"

                qry = qry + " FROM tblrptserviceanalysis a where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and staffid <> ''"


                'qry = "SELECT StaffID,"
                'qry = qry + "ifnull((select count(distinct recordno) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid"
                'qry = qry + " group by staffid),0) as 'NoofNormalSvcs',"

                ''qry = qry + "ifnull((select format(sum(normalsumval),2) FROM (select distinct recordno,staffid,format(((ServiceValue-ManpowerCost)/NoPerson),2) as normalsumval"
                ''qry = qry + " from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate =1 group by staffid,recordno) as normalval"
                ''qry = qry + "  where staffid=a.staffid group by staffid),0) as NormalHourValue,"
                'qry = qry + "ifnull((select format(sum((ServiceValue-ManpowerCost)/NoPerson)),2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1)"
                'qry = qry + "  and staffid=a.staffid group by staffid),0) as OTHourValue,"

                ' ''  qry = qry + "format(sum(normalhour/60),2) as NormalHourSpent,"

                'qry = qry + "ifnull((select concat(sum(normalhour) div 60,' Hours ',format(mod((sum(normalHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid group by staffid),'0 Hours 00 Mins') as 'NormalHourSpent (HH:MM)',"
                'qry = qry + "ifnull((select sum(normalhour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid group by staffid),0) as 'NormalHourSpent (Mins)',"
                'qry = qry + "ifnull((select format(sum(normalhour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid group by staffid),0) as 'NormalHourSpent (Decimal)',"


                'qry = qry + "ifnull((select count(distinct recordno) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2) and staffid=a.staffid"
                'qry = qry + " group by staffid),0) as NoofOTSvcs,"
                ''qry = qry + "ifnull((select format(sum(distinct ((ServiceValue-ManpowerCost)/NoPerson)),2) FROM (select distinct recordno,staffid,servicevalue,manpowercost,noperson from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2)) as otvalue"
                ''qry = qry + "  and staffid=a.staffid group by staffid),0) as OTHourValue,"
                'qry = qry + "ifnull((select format(sum(otsumval),2) FROM (select distinct recordno,staffid,format(((ServiceValue-ManpowerCost)/NoPerson),2) as otsumval"
                'qry = qry + " from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2) group by staffid,recordno) as otval"
                'qry = qry + "  where staffid=a.staffid group by staffid),0) as OTHourValue,"

                'qry = qry + "ifnull((select concat(sum(othour) div 60,' Hours ',format(mod((sum(otHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid group by staffid),'0 Hours 00 Mins') as 'OT1.5HourSpent (HH:MM)',"
                'qry = qry + "ifnull((select sum(othour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid group by staffid),0) as 'OT1.5HourSpent (Mins)',"

                'qry = qry + "ifnull((select format(sum(OThour/60),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid"
                'qry = qry + " group by staffid),0) as 'OT1.5HourSpent (Decimal)',"
                'qry = qry + "ifnull((select concat(sum(othour) div 60,' Hours ',format(mod((sum(otHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid group by staffid),'0 Hours 00 Mins') as 'OT2HourSpent (HH:MM)',"
                'qry = qry + "ifnull((select sum(othour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid group by staffid),0) as 'OT2HourSpent (Mins)',"

                'qry = qry + "ifnull((select format(sum(OThour/60),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid"
                'qry = qry + " group by staffid),0) as 'OT2.0HourSpent (Decimal)',"
                ''qry = qry + "ifnull((select format(sum(distinct ((ServiceValue-ManpowerCost)/NoPerson)),2) FROM (select distinct recordno,staffid,servicevalue,manpowercost,noperson from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1,1.5,2)) as totalvalue"
                ''qry = qry + "  and staffid=a.staffid group by staffid),0) as TotalValue"
                'qry = qry + "ifnull((select format(sum(totalsumval),2) FROM (select distinct recordno,staffid,format(((ServiceValue-ManpowerCost)/NoPerson),2) as totalsumval"
                'qry = qry + " from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1,1.5,2) group by staffid,recordno) as totalval"
                'qry = qry + "  where staffid=a.staffid group by staffid),0) as TotalValue"

                'qry = qry + " FROM tblrptserviceanalysis a where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and staffid <> ''"

            End If


        End If
       
        Dim qrySvcRec As String = "SELECT * FROM tblservicerecord where status='P' and servicedate is not null and (timein is not null and timeout is not null) and (timein <>"" and timeout <> "") and"
        qrySvcRec = qrySvcRec + " (timein<>'0' and timeout<>'0') and timein<>'  :' and timeout<>'  :'"

        Dim selFormula As String
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

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
                Return False
            End If

            qrySvcRec = qrySvcRec + " and ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
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
                Return False
            End If
            qrySvcRec = qrySvcRec + " and ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        'If txtServiceID.Text = "-1" Then
        'Else
        '    qry = qry + " and servdatetype = '" + txtServiceID.SelectedItem.Text + "'"

        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)

            qry = qry + " and servdatetype in (" + YrStr + ")"

            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If
          
            'Dim conn1 As MySqlConnection = New MySqlConnection()

            'conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn1.Open()

            'Dim command2 As MySqlCommand = New MySqlCommand

            'command2.CommandType = CommandType.Text

            'If rdbSelect.SelectedValue = "Detail" Then
            '    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberNormalOTDet' and servdatetype NOT in (" + YrStr + ")"
            'ElseIf rdbSelect.SelectedValue = "Summary" Then
            '    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberNormalOTSumm' and servdatetype NOT in (" + YrStr + ")"
            'End If

            'command2.Connection = conn1

            'command2.ExecuteNonQuery()
            'command2.Dispose()

            'conn1.Close()
            'conn1.Dispose()

        End If


        If ddlStaffDept.Text = "-1" Then
        Else
            qry = qry + " and STAFFID in (SELECT STAFFID FROM tblstaff where DEPARTMENT = '" + ddlStaffDept.Text.Trim + "')"

            ' selFormula = selFormula + " and {tblstaff1.Department} = '" + ddlStaffDept.Text + "'"
            If selection = "" Then
                selection = "ServiceDepartment : " + ddlStaffDept.Text
            Else
                selection = selection + ", ServiceDepartment : " + ddlStaffDept.Text
            End If
        End If

        If ddlIncharge.Text = "-1" Then
        Else
            qrySvcRec = qrySvcRec + " and recordno in (SELECT RecordNo FROM tblservicerecordstaff where staffid = '" + ddlIncharge.Text.Trim + "')"
            qry = qry + " and staffid = '" + ddlIncharge.Text.Trim + "'"

            If selection = "" Then
                selection = "ServiceStaff : " + ddlIncharge.Text
            Else
                selection = selection + ", ServiceStaff : " + ddlIncharge.Text
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

            qrySvcRec = qrySvcRec + " and tblservicerecord.CompanyGroup in (" + YrStr + ")"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
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

            qrySvcRec = qrySvcRec + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"


            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
        End If
        Session.Add("selection", selection)

        If chkDistribution.Checked Then
            If rdbSelect.SelectedValue = "Detail" Then
                qry = qry + " order by staffid"
            ElseIf rdbSelect.SelectedValue = "Summary" Then
                qry = qry + " order by staffid"
            End If
        Else
            If rdbSelect.SelectedValue = "Detail" Then
                qry = qry + " group by staffid,recordno order by staffid"
            ElseIf rdbSelect.SelectedValue = "Summary" Then
                qry = qry + " group by staffid order by staffid"
            End If
        End If
      

        txtQuery.Text = qry


        InsertIntoTblWebEventLog("GetData", qry, rdbSelect.Text)
        InsertIntoTblWebEventLog("GetData2", qrySvcRec, "")


        GetData1()
        Return True


        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim cmdSvcRec As MySqlCommand = New MySqlCommand

        cmdSvcRec.CommandType = CommandType.Text

        cmdSvcRec.CommandText = qrySvcRec
        cmdSvcRec.CommandTimeout = 0

        cmdSvcRec.Connection = conn

        Dim drSvcRec As MySqlDataReader = cmdSvcRec.ExecuteReader()
        Dim dtSvcRec As New DataTable
        dtSvcRec.Load(drSvcRec)

        If dtSvcRec.Rows.Count > 0 Then
            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text
            If rdbSelect.Text = "Detail" Then
                command2.CommandText = "delete from tblrptserviceanalysis where Report='ProdTeamMemberNormalOTDet' and CreatedBy='" + txtCreatedBy.Text + "';"

            ElseIf rdbSelect.Text = "Summary" Then
                command2.CommandText = "delete from tblrptserviceanalysis where Report='ProdTeamMemberNormalOTSumm' and CreatedBy='" + txtCreatedBy.Text + "';"

            End If

            command2.Connection = conn

            command2.ExecuteNonQuery()

            InsertIntoTblWebEventLog("GetData3", qrySvcRec, "")

            For i As Integer = 0 To dtSvcRec.Rows.Count - 1
                Dim cmdSvcRecStaff As MySqlCommand = New MySqlCommand

                cmdSvcRecStaff.CommandType = CommandType.Text

                cmdSvcRecStaff.CommandText = "Select count(staffid),staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


                cmdSvcRecStaff.Connection = conn

                Dim drSvcRecStaff As MySqlDataReader = cmdSvcRecStaff.ExecuteReader()
                Dim dtSvcRecStaff As New DataTable
                dtSvcRecStaff.Load(drSvcRecStaff)

                'Dim staffid As String = ""
                'If dtSvcRecStaff.Rows.Count > 0 Then
                '    For k As Integer = 0 To dtSvcRecStaff.Rows.Count - 1
                '        If staffid <> "" Then
                '            staffid = staffid + "," + dtSvcRecStaff.Rows(k)("Staffid").ToString
                '        End If
                '    Next
                'End If
                Dim cmdSvcRecDet As MySqlCommand = New MySqlCommand

                cmdSvcRecDet.CommandType = CommandType.Text

                If YrStrList2.Count = 0 Then
                    cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"
                Else
                    Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)

                    cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "' and serviceid in (" + YrStr + ")"


                End If

                'If txtServiceID.Text = "-1" Then
                '    cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"
                'Else
                '    cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "' and serviceid='" + txtServiceID.SelectedItem.Text + "'"

                'End If


                cmdSvcRecDet.Connection = conn

                Dim drSvcRecDet As MySqlDataReader = cmdSvcRecDet.ExecuteReader()
                Dim dtSvcRecDet As New DataTable
                dtSvcRecDet.Load(drSvcRecDet)
                Dim targetid As String = ""
                Dim servicecost As Decimal = 0

                If dtSvcRecDet.Rows.Count > 0 Then

                    For j As Integer = 0 To dtSvcRecDet.Rows.Count - 1
                        If dtSvcRecDet.Rows(j)("TargetID").ToString <> DBNull.Value.ToString Then
                            If targetid = "" Then
                                targetid = dtSvcRecDet.Rows(j)("TargetID")
                            Else

                                targetid = targetid + "," + dtSvcRecDet.Rows(j)("TargetID")
                            End If
                        End If

                        If dtSvcRecDet.Rows(j)("ServiceCost").ToString = DBNull.Value.ToString Then
                        Else

                            servicecost = servicecost + dtSvcRecDet.Rows(j)("ServiceCost")

                        End If

                    Next
                End If

                ' InsertIntoTblWebEventLog("GetData4", qrySvcRec, "")

                ''''''''''''''Normal and OT HOURS CALCULATION''''''
                ''''check for holidays''''''''''''
                Dim ot2hour As Decimal = 0

                Dim ot15hour As Decimal = 0
                Dim otnormal As Decimal = 0
                Dim otRate As Decimal = 0
                Dim timediff As Decimal = 0

                Dim svcdt As DateTime

                svcdt = dtSvcRec.Rows(i)("ServiceDate")

                '    Dim dtSvcSetup As New DataTable
                Dim cmdHoliday As MySqlCommand = New MySqlCommand

                cmdHoliday.CommandType = CommandType.Text

                cmdHoliday.CommandText = "Select holiday from tblholiday where holiday = '" + svcdt.ToString("yyyy-MM-dd") + "'"

                cmdHoliday.Connection = conn

                Dim drHoliday As MySqlDataReader = cmdHoliday.ExecuteReader()
                Dim dtHoliday As New DataTable
                dtHoliday.Load(drHoliday)

                If ((dtSvcRec.Rows(i)("TimeIn").ToString <> "" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "") And (String.IsNullOrEmpty(dtSvcRec.Rows(i)("TimeIn").ToString) = False Or String.IsNullOrEmpty(dtSvcRec.Rows(i)("TimeOut").ToString) = False) And (dtSvcRec.Rows(i)("TimeIn").ToString <> "0" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "0") And (dtSvcRec.Rows(i)("TimeIn").ToString <> "  :" Or dtSvcRec.Rows(i)("TimeOut").ToString <> "  :")) Then
                    If dtHoliday.Rows.Count > 0 Then
                        ot2hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn")), Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut")))
                        otRate = 2
                    Else
                        ''''Check for Saturday and Sunday'''''


                        If svcdt.DayOfWeek.ToString = "Sunday" Then
                            ot2hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn")), Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut")))
                            otRate = 2
                        ElseIf svcdt.DayOfWeek.ToString = "Saturday" Then

                            ''''''''find saturday working time''''

                            Dim cmdSvcSetup As MySqlCommand = New MySqlCommand

                            cmdSvcSetup.CommandType = CommandType.Text

                            cmdSvcSetup.CommandText = "Select satstart,satend from tblservicerecordmastersetup;"

                            cmdSvcSetup.Connection = conn

                            Dim drSvcSetup As MySqlDataReader = cmdSvcSetup.ExecuteReader()
                            Dim dtSvcSetup As New DataTable
                            dtSvcSetup.Load(drSvcSetup)

                            If dtSvcSetup.Rows.Count > 0 Then
                                Dim timein As New DateTime
                                timein = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn"))
                                Dim timeout As New DateTime
                                timeout = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut"))
                                Dim satstart As New DateTime
                                satstart = Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart"))
                                Dim satend As New DateTime
                                satend = Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd"))

                                'Normal working hours on Saturday : SatStart-8:30 and SatEnd-12:30
                                Dim servicetime As TimeSpan
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
                                            ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
                                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))
                                            otRate = 1.5
                                        End If
                                    End If

                                ElseIf timein < satstart Then
                                    If timeout <= satstart Then
                                        ot15hour = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->7:00 and TimeOut->8:00
                                        otRate = 1.5
                                    ElseIf timeout >= satstart Then

                                        If timeout <= satend Then 'Eg: TimeIn->7:50 and TimeOut->8:50
                                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
                                            ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")))
                                            otRate = 1.5
                                        ElseIf timeout >= satend Then 'Eg: TimeIn->7:50 and TimeOut->12:50
                                            ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart"))) + DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)
                                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))
                                            otRate = 1.5
                                        End If
                                    End If

                                ElseIf timein > satstart Then
                                    If timeout <= satend Then
                                        otnormal = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->10:50 and TimeOut->11:50
                                        otRate = 1
                                    ElseIf timeout >= satend Then
                                        If timein <= satend Then 'Eg: TimeIn->11:50 and TimeOut->12:50
                                            ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")), timeout)

                                            otnormal = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("SatEnd")))

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

                            Dim cmdSvcSetup As MySqlCommand = New MySqlCommand

                            cmdSvcSetup.CommandType = CommandType.Text

                            cmdSvcSetup.CommandText = "Select weekdaystart,weekdayend from tblservicerecordmastersetup;"

                            cmdSvcSetup.Connection = conn

                            Dim drSvcSetup As MySqlDataReader = cmdSvcSetup.ExecuteReader()
                            Dim dtSvcSetup As New DataTable
                            dtSvcSetup.Load(drSvcSetup)

                            If dtSvcSetup.Rows.Count > 0 Then
                                'Normal working hours on Weekday : WeekdayStart-8:30 and WeekdayEnd-5:30
                                '   lblAlert.Text = dtSvcRec.Rows(i)("TimeIn").ToString + " " + dtSvcRec.Rows(i)("Timeout").ToString
                                Dim timein As New DateTime
                                timein = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeIn"))
                                Dim timeout As New DateTime
                                timeout = Convert.ToDateTime(dtSvcRec.Rows(i)("TimeOut"))
                                Dim weekdaystart As New DateTime
                                weekdaystart = Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart"))
                                Dim weekdayend As New DateTime
                                weekdayend = Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd"))

                                Dim servicetime As TimeSpan
                                '  Dim dur As String

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
                                            ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
                                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
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
                                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), timeout)

                                            ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")))
                                            otRate = 1.5
                                        ElseIf timeout >= weekdayend Then 'Eg: TimeIn->7:50 and TimeOut->12:50
                                            ot15hour = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart"))) + DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
                                            otnormal = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayStart")), Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
                                            otRate = 1.5
                                        End If
                                    End If

                                ElseIf timein > weekdaystart Then

                                    If timeout <= weekdayend Then
                                        otnormal = DateDiff(DateInterval.Minute, timein, timeout) 'Eg: TimeIn->10:50 and TimeOut->11:50
                                        otRate = 1
                                    ElseIf timeout >= weekdayend Then
                                        If dtSvcRec.Rows(i)("TimeIn") <= dtSvcSetup.Rows(0)("WeekDayEnd") Then 'Eg: TimeIn->11:50 and TimeOut->12:50
                                            ot15hour = DateDiff(DateInterval.Minute, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")), timeout)
                                            otnormal = DateDiff(DateInterval.Minute, timein, Convert.ToDateTime(dtSvcSetup.Rows(0)("WeekDayEnd")))
                                            otRate = 1.5
                                        ElseIf timein >= weekdayend Then 'Eg: TimeIn->1:50 and TimeOut->2:50
                                            ot15hour = DateDiff(DateInterval.Minute, timein, timeout)
                                            otRate = 1.5
                                        End If
                                    End If
                                End If
                            End If

                        End If
                    End If
                End If

                '    InsertIntoTblWebEventLog("GetData5", qrySvcRec, "")


                ''''''Delete records that exists already
                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblrptserviceanalysis where recordno='" + dtSvcRec.Rows(i)("RecordNo") + "'"
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then




                'End If

                Dim cmdSvcRecStaff1 As MySqlCommand = New MySqlCommand

                cmdSvcRecStaff1.CommandType = CommandType.Text

                If ddlStaffDept.Text = "-1" Then
                    cmdSvcRecStaff1.CommandText = "Select staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"
                Else
                    cmdSvcRecStaff1.CommandText = "Select staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "' and staffid in (select staffid from tblstaff where department = '" + ddlStaffDept.Text + "')"

                End If


                cmdSvcRecStaff1.Connection = conn

                Dim drSvcRecStaff1 As MySqlDataReader = cmdSvcRecStaff1.ExecuteReader()
                Dim dtSvcRecStaff1 As New DataTable
                dtSvcRecStaff1.Load(drSvcRecStaff1)

                If dtSvcRecStaff1.Rows.Count > 0 Then
                    For s As Integer = 0 To dtSvcRecStaff1.Rows.Count - 1

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry1 As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report);"
                        command.CommandText = qry1
                        command.Parameters.Clear()
                        command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
                        command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)
                        'If txtActSvcDate.Text = "" Then
                        '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                        'Else
                        '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
                        'End If
                        command.Parameters.AddWithValue("@ServiceDate", dtSvcRec.Rows(i)("ServiceDate"))
                        command.Parameters.AddWithValue("@NoPerson", dtSvcRecStaff.Rows(0)("count(staffid)"))
                        command.Parameters.AddWithValue("@Duration", dtSvcRec.Rows(i)("Duration"))
                        command.Parameters.AddWithValue("@AccountID", dtSvcRec.Rows(i)("AccountID").ToString)
                        command.Parameters.AddWithValue("@Client", dtSvcRec.Rows(i)("CustName").ToString)
                        command.Parameters.AddWithValue("@TimeIn", dtSvcRec.Rows(i)("TimeIn"))
                        command.Parameters.AddWithValue("@TimeOut", dtSvcRec.Rows(i)("Timeout"))
                        command.Parameters.AddWithValue("@Service", targetid)
                        command.Parameters.AddWithValue("@ServiceValue", dtSvcRec.Rows(i)("BillAmount"))
                        command.Parameters.AddWithValue("@ServiceCost", servicecost)
                        'If dtSvcRecStaff.Rows(0)("count(staffid)") <> 0 Then
                        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount") / dtSvcRecStaff.Rows(0)("count(staffid)"))
                        'Else
                        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount"))

                        'End If
                        command.Parameters.AddWithValue("@ManpowerCost", 0)
                        '  command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
                        command.Parameters.AddWithValue("@StaffID", dtSvcRecStaff1.Rows(s)("StaffID"))
                        command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID").ToString)

                        command.Parameters.AddWithValue("@NormalHour", otnormal)
                        command.Parameters.AddWithValue("@OTHour", ot15hour + ot2hour)
                        If dtSvcRecDet.Rows.Count > 0 Then
                            command.Parameters.AddWithValue("@ServDateType", dtSvcRecDet.Rows(0)("ServiceID").ToString)
                        Else
                            command.Parameters.AddWithValue("@ServDateType", "")
                        End If

                        command.Parameters.AddWithValue("@OTRate", otRate)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        If rdbSelect.Text = "Detail" Then
                            command.Parameters.AddWithValue("@Report", "ProdTeamMemberNormalOTDet")
                        ElseIf rdbSelect.Text = "Summary" Then
                            command.Parameters.AddWithValue("@Report", "ProdTeamMemberNormalOTSumm")

                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        command.Dispose()

                    Next


                End If

                ' InsertIntoTblWebEventLog("GetData6", qrySvcRec, "")


                drSvcRecDet.Close()
                dtSvcRecDet.Clear()
                drSvcRecStaff.Close()
                dtSvcRecStaff.Clear()
                drSvcRecStaff1.Close()
                dtSvcRecStaff1.Clear()
                cmdSvcRecStaff1.Dispose()

            Next

        End If

        dtSvcRec.Clear()
        drSvcRec.Close()
        conn.Close()
        conn.Dispose()

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

        If rdbSelect.SelectedValue = "Detail" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    '   If j = 5 Or j = 14 Or j = 15 Or j = 16 Or j = 18 Or j = 19 Or j = 20 Then
                    If j = 9 Or j = 17 Or j = 18 Or j = 19 Or j = 20 Or j = 21 Or j = 23 Or j = 24 Or j = 25 Or j = 27 Or j = 28 Or j = 30 Or j = 31 Then

                        '  If j = 8 Or j = 16 Or j = 17 Or j = 18 Or j = 19 Or j = 21 Or j = 22 Or j = 23 Or j = 25 Or j = 26 Or j = 28 Or j = 29 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = 3 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                    ElseIf j = 12 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

                    ElseIf j = 5 Or j = 7 Or j = 14 Or j = 16 Then
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
                InsertIntoTblWebEventLog("EXCEL OT", i.ToString, dt.Rows(i)("NoofOTSVCs").ToString + " " + dt.Rows(i)("NoofNormalSVCs").ToString)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 2 Or j = 4 Or j = 5 Or j = 7 Or j = 9 Or j = 10 Or j = 12 Or j = 13 Or j = 14 Or j = 1 Or j = 6 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                        'ElseIf j = 6 Then
                        '    InsertIntoTblWebEventLog("EXCEL OT", i.ToString, dt.Rows(i)(j).ToString)


                        '    If (dt.Rows(i)(j).ToString = "0" Or dt.Rows(i)(j).ToString = "0.00") Then
                        '        Dim d As Int32 = Convert.ToInt32("0")
                        '        cell.SetCellValue(d)

                        '    ElseIf String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        '        Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                        '        cell.SetCellValue(d)
                        '    Else
                        '        Dim d As Int32 = Convert.ToInt32("0")
                        '        cell.SetCellValue(d)

                        '    End If
                        '    cell.CellStyle = _intCellStyle

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


        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = ""
            If chkTimeSheet.Checked Then
                attachment = "attachment; filename=Manpower Productivity by Team Member - Normal & OT (TimeSheet)"
            Else
                attachment = "attachment; filename=Manpower Productivity by Team Member - Normal & OT"
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

    Protected Sub btnEmailPDF_Click(sender As Object, e As EventArgs) Handles btnEmailPDF.Click
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
        commandEventLog.Parameters.AddWithValue("@DocRef", "MANPOWER-TEAMMEMBERNORMALOT")
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

        Dim qrySvcRec As String = "SELECT * FROM tblservicerecord where status='P' and servicedate is not null and (timein is not null and timeout is not null) and (timein <>"" and timeout <> "") and"
        qrySvcRec = qrySvcRec + " (timein<>'0' and timeout<>'0') and timein<>'  :' and timeout<>'  :'"
        Dim selection As String
        selection = ""

        Dim selFormula As String
        If rdbSelect.Text = "Detail" Then
            selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberNormalOTDet' AND {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

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
            selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberNormalOTSumm' AND {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

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
        'If txtServiceID.Text = "-1" Then
        'Else

        '    selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} = '" + txtServiceID.SelectedItem.Text + "'"
        '    If selection = "" Then
        '        selection = "ServiceID = " + txtServiceID.SelectedItem.Text
        '    Else
        '        selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
        '    End If
        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
            selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} in [" + YrStr + "]"

            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
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

        Session.Add("Selection", selection)
        Session.Add("selFormula", selFormula)

        Dim conn As New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text
        Dim qry As String = "INSERT INTO tbwmanpowerreportgenerate(Generated,BatchNo,CreatedBy,CreatedOn,FileType,ReportType,TimeSheet,DateFrom,DateTo,Selection,Selformula,qry,RetryCount,ContractGroup,DomainName)"
        qry = qry + "VALUES(@Generated,@BatchNo,@CreatedBy,@CreatedOn,@FileType,@ReportType,@TimeSheet,@DateFrom,@DateTo,@Selection,@Selformula,@qry,@RetryCount,@ContractGroup,@DomainName);"

        command.CommandText = qry
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@Generated", 0)
        command.Parameters.AddWithValue("@BatchNo", txtCreatedBy.Text + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


        command.Parameters.AddWithValue("@FileType", "PDF")
        If rdbSelect.SelectedValue = "Detail" Then
            command.Parameters.AddWithValue("@ReportType", "ProdTeamMemberNormalOTDet")

        Else
            command.Parameters.AddWithValue("@ReportType", "ProdTeamMemberNormalOTSumm")

        End If

        If chkTimeSheet.Checked Then
            command.Parameters.AddWithValue("@TimeSheet", "Yes")
        Else
            command.Parameters.AddWithValue("@TimeSheet", "No")
        End If
        command.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@Selection", Session("Selection"))
        command.Parameters.AddWithValue("@SelFormula", Session("SelFormula"))
        command.Parameters.AddWithValue("@qry", "")
        command.Parameters.AddWithValue("@RetryCount", 0)
        command.Parameters.AddWithValue("@ContractGroup", "-")
        command.Parameters.AddWithValue("@DomainName", ConfigurationManager.AppSettings("DomainName").ToString())

        command.Connection = conn

        command.ExecuteNonQuery()

        command.Dispose()
        conn.Close()
        conn.Dispose()
        mdlPopupMsg.Show()

        ' lblAlert.Text = "Report Generation is in process. The report will be emailed to you."
    End Sub

    Protected Sub btnEmailExcel_Click(sender As Object, e As EventArgs) Handles btnEmailExcel.Click
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
        commandEventLog.Parameters.AddWithValue("@DocRef", "MANPOWER-TEAMMEMBERNORMALOT")
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


        Dim selection As String
        selection = ""
        Dim qry As String = ""
        Dim deleteqry As String = ""
       

        Dim selFormula As String = ""
        If chkDistribution.Checked = True Then

            If rdbSelect.SelectedValue = "Detail" Then

                qry = "SELECT StaffID,RecordNo,ContractNo,Percentage,ServDateType as ContractGroup,ServiceDate as ServiceStartDate,TimeIn,ServiceEndDate,TimeOut,Duration as ServiceDuration,Client,Service,NoPerson,ReportServiceStart,time_format(reportservicestart,'%H:%i') as ReportServiceStartTime,"
                qry = qry + "ReportServiceEnd,time_format(reportserviceend,'%H:%i') as ReportServiceEndTime,ReportDuration,ServiceValue,BreakdownServiceValue,Productivity,NormalHourValue,"
                qry = qry + "concat(normalhour div 60,' Hours ',format(mod((normalHour/60)*60,60),0),' Mins') as 'NormalHourSpent (HH:MM)',"
                qry = qry + "normalhour as 'NormalHourSpent (Mins)',format(normalhour/60,2) as 'NormalHourSpent (Decimal)',"
                qry = qry + "OTHourValue,concat(OT15Hour div 60,' Hours ',format(mod((OT15Hour/60)*60,60),0),' Mins') as 'OT1.5HourSpent (HH:MM)',"
                qry = qry + "OT15Hour as 'OT1.5HourSpent (Mins)',format(OT15hour/60,2) as 'OT1.5HourSpent (Decimal)',"
                qry = qry + "concat(OT2Hour div 60,' Hours ',format(mod((OT2Hour/60)*60,60),0),' Mins') as 'OT2HourSpent (HH:MM)',"
                qry = qry + "OT2Hour as 'OT2HourSpent (Mins)',format(OT2hour/60,2) as 'OT2HourSpent (Decimal)'"

                qry = qry + " FROM tblrptserviceanalysis1 where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and staffid <> ''"

            ElseIf rdbSelect.SelectedValue = "Summary" Then
                qry = "SELECT StaffID,CountNormalSvcs as 'NoofNormalSvcs',NormalHourValue,"
                qry = qry + "concat(normalhour div 60,' Hours ',format(mod((normalHour/60)*60,60),0),' Mins') as 'NormalHourSpent (HH:MM)',"
                qry = qry + "normalhour as 'NormalHourSpent (Mins)',format(normalhour/60,2) as 'NormalHourSpent (Decimal)',"
                qry = qry + "CountOTSvcs as NoofOTSvcs,OTHourValue,concat(OT15Hour div 60,' Hours ',format(mod((OT15Hour/60)*60,60),0),' Mins') as 'OT1.5HourSpent (HH:MM)',"
                qry = qry + "OT15Hour as 'OT1.5HourSpent (Mins)',format(OT15hour/60,2) as 'OT1.5HourSpent (Decimal)',"
                qry = qry + "concat(OT2Hour div 60,' Hours ',format(mod((OT2Hour/60)*60,60),0),' Mins') as 'OT2HourSpent (HH:MM)',"
                qry = qry + "OT2Hour as 'OT2HourSpent (Mins)',format(OT2hour/60,2) as 'OT2HourSpent (Decimal)',"
                qry = qry + "ServiceValue as TotalValue"
                qry = qry + " FROM tblrptserviceanalysis1 where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and staffid <> ''"

            End If

        Else
            If rdbSelect.SelectedValue = "Detail" Then

                qry = "SELECT StaffID,RecordNo,ContractNo,ServDateType as ContractGroup,ServiceDate as ServiceStartDate,min(TimeIn),ServiceEndDate,max(TimeOut),sum(Duration) as ServiceDuration,Client,Service,NoPerson,ReportServiceStart,time_format(reportservicestart,'%H:%i') as ReportServiceStartTime,"
                qry = qry + "ReportServiceEnd,time_format(reportserviceend,'%H:%i') as ReportServiceEndTime,ReportDuration,ServiceValue,format((ServiceValue-ManpowerCost)/NoPerson,2) as Productivity,"
                qry = qry + "ifnull((select format((ServiceValue-ManpowerCost)/NoPerson,2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid  and recordno=a.recordno group by staffid),0) as NormalHourValue,"
                qry = qry + "ifnull((select concat(sum(normalhour) div 60,' Hours ',format(mod((sum(normalHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid,recordno),'0 Hours 00 Mins') as 'NormalHourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(normalhour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'NormalHourSpent (Mins)',"
                qry = qry + "ifnull((select format(sum(normalhour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'NormalHourSpent (Decimal)',"

                qry = qry + "ifnull((select format((ServiceValue-ManpowerCost)/NoPerson,2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate in (1.5,2) and staffid=a.staffid  and recordno=a.recordno group by staffid),0) as OTHourValue,"
                qry = qry + "ifnull((select concat(sum(OTHour) div 60,' Hours ',format(mod((sum(OTHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'OT1.5HourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(OThour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT1.5HourSpent (Mins)',"
                qry = qry + "ifnull((select format(sum(OThour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT1.5HourSpent (Decimal)',"

                qry = qry + "ifnull((select concat(sum(OTHour) div 60,' Hours ',format(mod((sum(OTHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'OT2HourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(OThour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT2HourSpent (Mins)',"
                qry = qry + "ifnull((select format(sum(OThour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT2HourSpent (Decimal)'"

                qry = qry + " FROM tblrptserviceanalysis a where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and staffid <> ''"

                'qry = "SELECT StaffID,ServiceDate,TimeIn,TimeOut,Client,Service,NoPerson,ReportServiceStart,time_format(reportservicestart,'%H:%i') as ReportServiceStartTime,"
                'qry = qry + "ReportServiceEnd,time_format(reportserviceend,'%H:%i') as ReportServiceEndTime,ServiceValue,format((ServiceValue-ManpowerCost)/NoPerson,2) as Productivity,"
                'qry = qry + "ifnull((select format(SUM(servicevalue),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid),0) GROUP BY STAFFID as NormalHourValue,"
                ''   qry = qry + "concat(format(normalhour/60,0),' Hours ',format(mod((normalhour/60)*60,60),0),' Mins') as NormalHourSpent,"
                'qry = qry + "ifnull((select concat(normalhour div 60,' Hours ',format(mod((normalHour/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1 and staffid=a.staffid and recordno=a.recordno),'0 Hours 00 Mins') as NormalHourSpent,"

                'qry = qry + "ifnull((select format((SUM(servicevalue),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate in (1.5,2) and staffid=a.staffid),0) GROUP BY STAFFID as OTHourValue,"

                'qry = qry + "ifnull((select concat(OTHour div 60,' Hours ',format(mod((OTHour/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno),'0 Hours 00 Mins') as OT15HourSpent,"
                'qry = qry + "ifnull((select concat(OTHour div 60,' Hours ',format(mod((OTHour/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and otrate=2 and staffid=a.staffid and recordno=a.recordno),'0 Hours 00 Mins') as OT2HourSpent"
                'qry = qry + " FROM tblrptserviceanalysis a where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTDet' and staffid <> ''"

            ElseIf rdbSelect.SelectedValue = "Summary" Then
                qry = "SELECT StaffID,"
                qry = qry + "ifnull((select count(staffid) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid"
                qry = qry + " group by staffid),0) as 'NoofNormalSvcs',"

                qry = qry + "ifnull((select format(sum((ServiceValue-ManpowerCost)/NoPerson),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid"
                qry = qry + " group by staffid),0) as NormalHourValue,"

                '  qry = qry + "format(sum(normalhour/60),2) as NormalHourSpent,"

                qry = qry + "ifnull((select concat(sum(normalhour) div 60,' Hours ',format(mod((sum(normalHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'NormalHourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(normalhour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'NormalHourSpent (Mins)',"
                qry = qry + "ifnull((select format(sum(normalhour)/60,2) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'NormalHourSpent (Decimal)',"


                qry = qry + "ifnull((select count(staffid) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2) and staffid=a.staffid"
                qry = qry + " group by staffid),0) as NoofOTSvcs,"
                qry = qry + "ifnull((select format(sum((ServiceValue-ManpowerCost)/NoPerson),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2) and staffid=a.staffid"
                qry = qry + " group by staffid),0) as OTHourValue,"
                qry = qry + "ifnull((select concat(sum(othour) div 60,' Hours ',format(mod((sum(otHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'OT1.5HourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(othour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT1.5HourSpent (Mins)',"

                qry = qry + "ifnull((select format(sum(OThour/60),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid"
                qry = qry + " group by staffid),0) as 'OT1.5HourSpent (Decimal)',"
                qry = qry + "ifnull((select concat(sum(othour) div 60,' Hours ',format(mod((sum(otHour)/60)*60,60),0),' Mins') from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),'0 Hours 00 Mins') as 'OT2HourSpent (HH:MM)',"
                qry = qry + "ifnull((select sum(othour) from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid and recordno=a.recordno group by staffid),0) as 'OT2HourSpent (Mins)',"

                qry = qry + "ifnull((select format(sum(OThour/60),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid"
                qry = qry + " group by staffid),0) as 'OT2.0HourSpent (Decimal)',"
                qry = qry + "ifnull((select format(sum((ServiceValue-ManpowerCost)/NoPerson),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1,1.5,2) and staffid=a.staffid"
                qry = qry + " group by staffid),0) as TotalValue"

                qry = qry + " FROM tblrptserviceanalysis a where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and staffid <> ''"

            End If
        End If

        Dim qrySvcRec As String = "SELECT * FROM tblservicerecord where status='P' and servicedate is not null and (timein is not null and timeout is not null) and (timein <>"" and timeout <> "") and"
        qrySvcRec = qrySvcRec + " (timein<>'0' and timeout<>'0') and timein<>'  :' and timeout<>'  :'"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

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
            qrySvcRec = qrySvcRec + " and ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        'If txtServiceID.Text = "-1" Then
        'Else
        '    qry = qry + " and servdatetype = '" + txtServiceID.SelectedItem.Text + "'"

        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)

            qry = qry + " and servdatetype in (" + YrStr + ")"
            deleteqry = deleteqry + " and servdatetype not in (" + YrStr + ")"
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If
        End If


        If ddlStaffDept.Text = "-1" Then
        Else
            qry = qry + " and STAFFID in (SELECT STAFFID FROM tblstaff where DEPARTMENT = '" + ddlStaffDept.Text.Trim + "')"

            ' selFormula = selFormula + " and {tblstaff1.Department} = '" + ddlStaffDept.Text + "'"
            If selection = "" Then
                selection = "ServiceDepartment : " + ddlStaffDept.Text
            Else
                selection = selection + ", ServiceDepartment : " + ddlStaffDept.Text
            End If
        End If

        If ddlIncharge.Text = "-1" Then
        Else
            qrySvcRec = qrySvcRec + " and recordno in (SELECT RecordNo FROM tblservicerecordstaff where staffid = '" + ddlIncharge.Text.Trim + "')"
            qry = qry + " and staffid = '" + ddlIncharge.Text.Trim + "'"

            If selection = "" Then
                selection = "ServiceStaff : " + ddlIncharge.Text
            Else
                selection = selection + ", ServiceStaff : " + ddlIncharge.Text
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

            qrySvcRec = qrySvcRec + " and tblservicerecord.CompanyGroup in (" + YrStr + ")"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
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
            '   selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
            qrySvcRec = qrySvcRec + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"


            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
        End If
        Session.Add("selection", selection)

        If chkDistribution.Checked Then
            If rdbSelect.SelectedValue = "Detail" Then
                qry = qry + " order by staffid"
            ElseIf rdbSelect.SelectedValue = "Summary" Then
                qry = qry + " order by staffid"
            End If
        Else
            If rdbSelect.SelectedValue = "Detail" Then
                qry = qry + " group by staffid,recordno order by staffid"
            ElseIf rdbSelect.SelectedValue = "Summary" Then
                qry = qry + " group by staffid order by staffid"
            End If
        End If
      


        txtQuery.Text = qry
        txtDeleteQuery.Text = deleteqry



        Session.Add("Selection", selection)
        Session.Add("selFormula", selFormula)

        Dim conn As New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text
        Dim qry1 As String = "INSERT INTO tbwmanpowerreportgenerate(Generated,BatchNo,CreatedBy,CreatedOn,FileType,ReportType,TimeSheet,DateFrom,DateTo,Selection,Selformula,qry,RetryCount,ContractGroup,DomainName,Distribution)"
        qry1 = qry1 + "VALUES(@Generated,@BatchNo,@CreatedBy,@CreatedOn,@FileType,@ReportType,@TimeSheet,@DateFrom,@DateTo,@Selection,@Selformula,@qry,@RetryCount,@ContractGroup,@DomainName,@Distribution);"

        command.CommandText = qry1
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@Generated", 0)
        command.Parameters.AddWithValue("@BatchNo", txtCreatedBy.Text + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


        command.Parameters.AddWithValue("@FileType", "Excel")
        If rdbSelect.SelectedValue = "Detail" Then
            command.Parameters.AddWithValue("@ReportType", "ProdTeamMemberNormalOTDet")

        Else
            command.Parameters.AddWithValue("@ReportType", "ProdTeamMemberNormalOTSumm")

        End If
        If chkTimeSheet.Checked Then
            command.Parameters.AddWithValue("@TimeSheet", "Yes")
        Else
            command.Parameters.AddWithValue("@TimeSheet", "No")
        End If
        command.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@Selection", Session("Selection"))
        command.Parameters.AddWithValue("@SelFormula", Session("SelFormula"))
        command.Parameters.AddWithValue("@qry", qry)
        command.Parameters.AddWithValue("@RetryCount", 0)
        If deleteqry = "delete from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm'" Then
            command.Parameters.AddWithValue("@ContractGroup", "-")
        Else
            command.Parameters.AddWithValue("@ContractGroup", deleteqry)
        End If
        command.Parameters.AddWithValue("@DomainName", ConfigurationManager.AppSettings("DomainName").ToString())
        command.Parameters.AddWithValue("@Distribution", chkDistribution.Checked)

        command.Connection = conn

        command.ExecuteNonQuery()

        command.Dispose()
        conn.Close()
        conn.Dispose()
        mdlPopupMsg.Show()

        '  lblAlert.Text = "Report Generation is in process. The report will be emailed to you."
    End Sub
End Class
