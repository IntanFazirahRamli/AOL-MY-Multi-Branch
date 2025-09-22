Imports System.Globalization
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Drawing

Partial Class DashboardSummary
    Inherits System.Web.UI.Page

#Region "Page Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            ChangedFloorPlanRadioButton()

            dtpFromLocationStatusDate.Attributes.Add("readonly", "readonly")
            dtpToLocationStatusDate.Attributes.Add("readonly", "readonly")

            dtpFromLocationStatusDate.Text = Now.AddDays(-6).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            dtpToLocationStatusDate.Text = Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)


            'Bind Week Drop down
            Dim WeekNumberList As New List(Of ListItem)

            Dim startDate As DateTime, endDate As DateTime
            startDate = New DateTime(DateTime.Now.Year, 1, 1)
            endDate = startDate.AddDays(6)
            Dim currentWeekNumber As Integer = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday)
            For i As Integer = 1 To currentWeekNumber - 1

                Dim item As New ListItem()
                item.Value = i
                Dim sValue As String = "Week " + i.ToString() + " " + startDate.ToShortDateString() + "-" + endDate.ToShortDateString()
                item.Text = sValue
                WeekNumberList.Add(item)

                startDate = startDate.AddDays(7)
                endDate = startDate.AddDays(6)
            Next

            ddlfromweekList.DataSource = WeekNumberList
            ddlfromweekList.DataTextField = "Text"
            ddlfromweekList.DataValueField = "Value"
            ddlfromweekList.DataBind()

            ddltoweekList.DataSource = WeekNumberList
            ddltoweekList.DataTextField = "Text"
            ddltoweekList.DataValueField = "Value"
            ddltoweekList.DataBind()


            If WeekNumberList.Count > 0 Then
                ddlfromweekList.SelectedIndex = 0
                ddltoweekList.SelectedIndex = 0
            End If

            'Bind Month drop down list

            Dim MonthlyList As New List(Of ListItem)
            Dim month1 As DateTime

            month1 = DateTime.Now.AddMonths(-12)
            Dim selectedIndex As Integer = 0

            For i As Integer = 1 To 24 - 1
                If month1.Year = DateTime.Now.Year And month1.Month = DateTime.Now.Month Then
                    selectedIndex = i - 1
                End If

                Dim sValue As String = month1.ToString("MMMM") + "-" + month1.Year.ToString()

                Dim item As New ListItem()
                item.Value = i
                item.Text = sValue
                MonthlyList.Add(item)

                month1 = month1.AddMonths(1)
            Next

            ddlfrommonthyear.DataSource = MonthlyList
            ddlfrommonthyear.DataTextField = "Text"
            ddlfrommonthyear.DataValueField = "Value"
            ddlfrommonthyear.DataBind()

            ddltomonthyear.DataSource = MonthlyList
            ddltomonthyear.DataTextField = "Text"
            ddltomonthyear.DataValueField = "Value"
            ddltomonthyear.DataBind()

            If MonthlyList.Count > 0 Then
                ddlfrommonthyear.SelectedIndex = selectedIndex
                ddltomonthyear.SelectedIndex = selectedIndex
            End If

            'Bind Year drop down list

            Dim YearlyList As New List(Of ListItem)

            Dim Year1 As DateTime
            Year1 = DateTime.Now.AddYears(-5)
            Dim currentYear As Integer = 0

            For i As Integer = 1 To 10

                If Year1.Year = DateTime.Now.Year Then
                    currentYear = i - 1
                End If
                Dim item As New ListItem()
                Dim sValue As String = Year1.ToString("yyyy")

                item.Value = i
                item.Text = sValue
                YearlyList.Add(item)

                Year1 = Year1.AddYears(1)

            Next

            ddlfromyear.DataSource = YearlyList
            ddlfromyear.DataTextField = "Text"
            ddlfromyear.DataValueField = "Value"
            ddlfromyear.DataBind()

            ddltoyear.DataSource = YearlyList
            ddltoyear.DataTextField = "Text"
            ddltoyear.DataValueField = "Value"
            ddltoyear.DataBind()

            If YearlyList.Count > 0 Then
                ddlfromyear.SelectedIndex = currentYear
                ddltoyear.SelectedIndex = currentYear
            End If

        End If

    End Sub

#End Region

#Region "submit click"

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) 'Handles btnSubmitDaily.Click
        LoadGrid()
    End Sub

#End Region
#Region "Bind Grid"

    Protected Sub gridLocationStatus_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gridLocationStatus.RowDataBound

        If e.Row.RowIndex >= 0 Then

            Dim totHoursWithActivityCount As Integer = 0
            Dim locationID As String = ""

            Dim labellocationID = DirectCast(e.Row.FindControl("lblLocationID"), Label)
            locationID = labellocationID.Text

            Dim labeltotHours = DirectCast(e.Row.FindControl("lblTotHoursWithActivity"), Label)
            Dim totHours As String = labeltotHours.Text

            If (totHours <> "") Then


                totHoursWithActivityCount = Convert.ToInt32(totHours)

                Dim threshold As New tbldeviceeventthreshold()
                threshold = GetThreshold(locationID)

                If totHoursWithActivityCount <= threshold.TotLow Then

                    e.Row.Cells(3).BackColor = ColorTranslator.FromHtml(threshold.TotLowColor)

                ElseIf totHoursWithActivityCount <= threshold.TotMedium Then

                    e.Row.Cells(3).BackColor = ColorTranslator.FromHtml(threshold.TotMediumColor)

                ElseIf totHoursWithActivityCount <= threshold.TotHigh Then

                    e.Row.Cells(3).BackColor = ColorTranslator.FromHtml(threshold.TotHighColor)

                ElseIf totHoursWithActivityCount <= threshold.TotVeryHigh Then

                    e.Row.Cells(3).BackColor = ColorTranslator.FromHtml(threshold.TotVeryHighColor)

                Else
                    e.Row.Cells(3).BackColor = ColorTranslator.FromHtml(threshold.TotVeryHighColor)

                End If
            End If


        End If
    End Sub

    Public Function GetThreshold(ByVal LocationID As String) As tbldeviceeventthreshold
        Dim item As New tbldeviceeventthreshold()
        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text
                Dim insQuery As String = "SELECT * FROM tbldeviceeventthreshold WHERE LocationID = '" & LocationID & "'"
                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()

                    If rdoDay.Checked Then
                        item.LocationID = CType(reader("LocationID"), String)
                        item.TotHigh = CType(reader("TotalDailyHigh"), Integer)
                        item.TotHighColor = CType(reader("TotalDailyHighColor"), String)
                        item.TotLow = CType(reader("TotalDailyLow"), Integer)
                        item.TotLowColor = CType(reader("TotalDailyLowColor"), String)
                        item.TotMedium = CType(reader("TotalDailyMedium"), Integer)
                        item.TotMediumColor = CType(reader("TotalDailyMediumColor"), String)
                        item.TotVeryHigh = CType(reader("TotalDailyVeryHigh"), Integer)
                        item.TotVeryHighColor = CType(reader("TotalDailyVeryHighColor"), String)
                    ElseIf rdoWeek.Checked Then
                        item.LocationID = CType(reader("LocationID"), String)
                        item.TotHigh = CType(reader("TotalWeeklyHigh"), Integer)
                        item.TotHighColor = CType(reader("TotalWeeklyHighColor"), String)
                        item.TotLow = CType(reader("TotalWeeklyLow"), Integer)
                        item.TotLowColor = CType(reader("TotalWeeklyLowColor"), String)
                        item.TotMedium = CType(reader("TotalWeeklyMedium"), Integer)
                        item.TotMediumColor = CType(reader("TotalWeeklyMediumColor"), String)
                        item.TotVeryHigh = CType(reader("TotalWeeklyVeryHigh"), Integer)
                        item.TotVeryHighColor = CType(reader("TotalWeeklyVeryHighColor"), String)
                    ElseIf rdoMonth.Checked Then
                        item.LocationID = CType(reader("LocationID"), String)
                        item.TotHigh = CType(reader("TotalMonthlyHigh"), Integer)
                        item.TotHighColor = CType(reader("TotalMonthlyHighColor"), String)
                        item.TotLow = CType(reader("TotalMonthlyLow"), Integer)
                        item.TotLowColor = CType(reader("TotalMonthlyLowColor"), String)
                        item.TotMedium = CType(reader("TotalMonthlyMedium"), Integer)
                        item.TotMediumColor = CType(reader("TotalMonthlyMediumColor"), String)
                        item.TotVeryHigh = CType(reader("TotalMonthlyVeryHigh"), Integer)
                        item.TotVeryHighColor = CType(reader("TotalMonthlyVeryHighColor"), String)
                    ElseIf rdoYear.Checked Then
                        item.LocationID = CType(reader("LocationID"), String)
                        item.TotHigh = CType(reader("TotalYearlyHigh"), Integer)
                        item.TotHighColor = CType(reader("TotalYearlyHighColor"), String)
                        item.TotLow = CType(reader("TotalYearlyLow"), Integer)
                        item.TotLowColor = CType(reader("TotalYearlyLowColor"), String)
                        item.TotMedium = CType(reader("TotalYearlyMedium"), Integer)
                        item.TotMediumColor = CType(reader("TotalYearlyMediumColor"), String)
                        item.TotVeryHigh = CType(reader("TotalYearlyVeryHigh"), Integer)
                        item.TotVeryHighColor = CType(reader("TotalYearlyVeryHighColor"), String)
                    End If
                Loop
                con.Close()
            End Using
        End Using

        If item.LocationID Is Nothing Or item.LocationID = "" Then
            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text
                    Dim insQuery As String = "SELECT * FROM tbldeviceeventthreshold WHERE DeviceType = 'DEFAULT'"
                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    Do While reader.Read()

                        If rdoDay.Checked Then
                            item.TotHigh = CType(reader("TotalDailyHigh"), Integer)
                            item.TotHighColor = CType(reader("TotalDailyHighColor"), String)
                            item.TotLow = CType(reader("TotalDailyLow"), Integer)
                            item.TotLowColor = CType(reader("TotalDailyLowColor"), String)
                            item.TotMedium = CType(reader("TotalDailyMedium"), Integer)
                            item.TotMediumColor = CType(reader("TotalDailyMediumColor"), String)
                            item.TotVeryHigh = CType(reader("TotalDailyVeryHigh"), Integer)
                            item.TotVeryHighColor = CType(reader("TotalDailyVeryHighColor"), String)
                        ElseIf rdoWeek.Checked Then
                            item.TotHigh = CType(reader("TotalWeeklyHigh"), Integer)
                            item.TotHighColor = CType(reader("TotalWeeklyHighColor"), String)
                            item.TotLow = CType(reader("TotalWeeklyLow"), Integer)
                            item.TotLowColor = CType(reader("TotalWeeklyLowColor"), String)
                            item.TotMedium = CType(reader("TotalWeeklyMedium"), Integer)
                            item.TotMediumColor = CType(reader("TotalWeeklyMediumColor"), String)
                            item.TotVeryHigh = CType(reader("TotalWeeklyVeryHigh"), Integer)
                            item.TotVeryHighColor = CType(reader("TotalWeeklyVeryHighColor"), String)
                        ElseIf rdoMonth.Checked Then
                            item.TotHigh = CType(reader("TotalMonthlyHigh"), Integer)
                            item.TotHighColor = CType(reader("TotalMonthlyHighColor"), String)
                            item.TotLow = CType(reader("TotalMonthlyLow"), Integer)
                            item.TotLowColor = CType(reader("TotalMonthlyLowColor"), String)
                            item.TotMedium = CType(reader("TotalMonthlyMedium"), Integer)
                            item.TotMediumColor = CType(reader("TotalMonthlyMediumColor"), String)
                            item.TotVeryHigh = CType(reader("TotalMonthlyVeryHigh"), Integer)
                            item.TotVeryHighColor = CType(reader("TotalMonthlyVeryHighColor"), String)
                        ElseIf rdoYear.Checked Then
                            item.TotHigh = CType(reader("TotalYearlyHigh"), Integer)
                            item.TotHighColor = CType(reader("TotalYearlyHighColor"), String)
                            item.TotLow = CType(reader("TotalYearlyLow"), Integer)
                            item.TotLowColor = CType(reader("TotalYearlyLowColor"), String)
                            item.TotMedium = CType(reader("TotalYearlyMedium"), Integer)
                            item.TotMediumColor = CType(reader("TotalYearlyMediumColor"), String)
                            item.TotVeryHigh = CType(reader("TotalYearlyVeryHigh"), Integer)
                            item.TotVeryHighColor = CType(reader("TotalYearlyVeryHighColor"), String)
                        End If

                    Loop
                    con.Close()
                End Using
            End Using

        End If

        Return item
    End Function


    Private Sub LoadGrid()

        Dim UserID As String

        If Not Session("UserID") Is Nothing Then
            UserID = Session("UserID").ToString()
        End If

        If Not UserID Is Nothing Then

            Dim RepStartDate As String = ""
            Dim RepFinishDate As String = ""


            If rdoDay.Checked Then

                Dim startDate As DateTime
                startDate = DateTime.ParseExact(dtpFromLocationStatusDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Dim endDate As DateTime
                endDate = DateTime.ParseExact(dtpToLocationStatusDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)

                RepStartDate = startDate.Year & "-" & startDate.Month & "-" & startDate.Day
                RepFinishDate = endDate.Year & "-" & endDate.Month & "-" & endDate.Day

            ElseIf rdoWeek.Checked Then

                Dim Weeklylist1 As New List(Of WeeklyList)

                Dim startDate1 As DateTime
                Dim endDate1 As DateTime
                Dim startDate As DateTime, endDate As DateTime

                Dim isListAdd As Boolean = False
                startDate = New DateTime(DateTime.Now.Year, 1, 1)
                endDate = startDate.AddDays(6)
                Dim currentWeekNumber As Integer = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday)
                For i As Integer = 1 To currentWeekNumber - 1

                    If isListAdd = True Then
                        Dim item As New WeeklyList()
                        item.StartDate = startDate
                        item.EndDate = endDate
                        item.WeekNumber = "Week " + i.ToString()
                        item.TextValue = "Week Line"
                        Weeklylist1.Add(item)
                    End If

                    If (i = Convert.ToInt32(ddlfromweekList.SelectedValue)) Then
                        isListAdd = True
                        startDate1 = startDate

                        Dim item As New WeeklyList()
                        item.StartDate = startDate
                        item.EndDate = endDate
                        item.WeekNumber = "Week " + i.ToString()
                        item.TextValue = "Week Line"
                        Weeklylist1.Add(item)
                    End If
                    If (i = Convert.ToInt32(ddltoweekList.SelectedValue)) Then
                        endDate1 = endDate
                        Exit For
                    End If

                    startDate = startDate.AddDays(7)
                    endDate = startDate.AddDays(6)

                Next

                RepStartDate = startDate1.Year & "-" & startDate1.Month & "-" & startDate1.Day
                RepFinishDate = endDate1.Year & "-" & endDate1.Month & "-" & endDate1.Day

            ElseIf rdoMonth.Checked Then

                Dim Monthlylist1 As New List(Of MonthlyList)
                Dim startDate1 As DateTime
                Dim endDate1 As DateTime

                Dim isListAdd As Boolean = False
                Dim month1 As DateTime

                month1 = DateTime.Now.AddMonths(-12)
                Dim selectedIndex As Integer = 0

                For i As Integer = 1 To 24 - 1

                    If isListAdd = True Then
                        Dim item As New MonthlyList()
                        item.Startdate1 = New DateTime(month1.Year, month1.Month, 1)
                        item.EndDate = item.Startdate1.AddMonths(1).AddDays(-1)
                        item.MonthYear = month1.ToString("MMMM") + "-" + month1.Year.ToString()
                        item.TextValue = "Month Line"
                        Monthlylist1.Add(item)
                    End If
                    If (i = Convert.ToInt32(ddlfrommonthyear.SelectedValue)) Then
                        isListAdd = True

                        Dim item As New MonthlyList()
                        item.Startdate1 = New DateTime(month1.Year, month1.Month, 1)
                        item.EndDate = item.Startdate1.AddMonths(1).AddDays(-1)
                        item.MonthYear = month1.ToString("MMMM") + "-" + month1.Year.ToString()
                        item.TextValue = "Month Line"
                        Monthlylist1.Add(item)
                    End If
                    If (i = Convert.ToInt32(ddltomonthyear.SelectedValue)) Then
                        Exit For
                    End If


                    month1 = month1.AddMonths(1)
                Next


                month1 = DateTime.Now.AddMonths(-12)
                startDate1 = month1.AddMonths(Convert.ToInt32(ddlfrommonthyear.SelectedValue) - 1)
                endDate1 = month1.AddMonths(Convert.ToInt32(ddltomonthyear.SelectedValue) - 1)

                startDate1 = New DateTime(startDate1.Year, startDate1.Month, 1)
                endDate1 = New DateTime(endDate1.Year, endDate1.Month, 1)
                endDate1 = endDate1.AddMonths(1).AddDays(-1)


                RepStartDate = startDate1.Year & "-" & startDate1.Month & "-" & startDate1.Day
                RepFinishDate = endDate1.Year & "-" & endDate1.Month & "-" & endDate1.Day

            ElseIf rdoYear.Checked Then

                Dim isListAdd As Boolean = False
                Dim YearlyList1 As New List(Of YearlyList)

                Dim Year1 As DateTime
                Year1 = DateTime.Now.AddYears(-5)
                Dim currentYear As Integer = 0

                For i As Integer = 1 To 10

                    If isListAdd = True Then

                        Dim item As New YearlyList()
                        item.Year = Year1.Year
                        item.TextValue = "Yearly Line"
                        item.Startdate1 = New DateTime(Year1.Year, 1, 1)
                        item.EndDate = New DateTime(Year1.Year, 12, 31)
                        YearlyList1.Add(item)
                    End If
                    If (i = Convert.ToInt32(ddlfromyear.SelectedValue)) Then
                        isListAdd = True

                        Dim item As New YearlyList()
                        item.Year = Year1.Year
                        item.TextValue = "Yearly Line"
                        item.Startdate1 = New DateTime(Year1.Year, 1, 1)
                        item.EndDate = New DateTime(Year1.Year, 12, 31)
                        YearlyList1.Add(item)
                    End If
                    If (i = Convert.ToInt32(ddltoyear.SelectedValue)) Then
                        Exit For
                    End If

                    Year1 = Year1.AddYears(1)
                Next


                Dim startDate1 As DateTime
                Dim endDate1 As DateTime

                Year1 = DateTime.Now.AddYears(-5)
                startDate1 = Year1.AddYears(Convert.ToInt32(ddlfromyear.SelectedValue) - 1)
                endDate1 = Year1.AddYears(Convert.ToInt32(ddltoyear.SelectedValue) - 1)

                startDate1 = New DateTime(startDate1.Year, 1, 1)
                endDate1 = New DateTime(endDate1.Year, 12, 31)

                RepStartDate = startDate1.Year & "-" & startDate1.Month & "-" & startDate1.Day
                RepFinishDate = endDate1.Year & "-" & endDate1.Month & "-" & endDate1.Day

            End If


            Dim Locationlist As New List(Of LocationStatusModel)

            Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text

                    Dim insQuery As String = "SELECT cp.locationid AS LocationID  FROM tblcustomerportaluseraccesslocation cp" & _
                                               " INNER JOIN tblcustomerlocationdevices cl ON cl.locationid =cp.locationid  WHERE cp.Userid = '" & UserID & "'"

                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()

                    Do While reader.Read()

                        Dim item As New LocationStatusModel()
                        item.LocationID = reader("LocationID").ToString()
                        Locationlist.Add(item)
                    Loop
                    con.Close()
                End Using
            End Using

            If Locationlist.Count > 0 Then


                Dim mainList = (From queryItem In Locationlist
                             Group By LocationID = queryItem.LocationID Into SIds = Group
                             Select New LocationStatusModel With { _
                             .LocationID = LocationID, _
                             .DeviceWithActivity = SIds.Count()}).ToList()




                For Each item In mainList

                    Dim DeviceIDList As New List(Of DeviceIDList)

                    Using con As New MySqlConnection(constr)
                        Using cmd As New MySqlCommand()
                            cmd.CommandType = CommandType.Text

                            Dim Query As String = "SELECT address1 AS Address,serviceName AS SiteName  FROM tblcustomerportaluseraccesslocation WHERE LocationID='" & item.LocationID & "' AND userID = '" & UserID & "'"

                            cmd.CommandText = Query
                            cmd.Connection = con
                            con.Open()
                            Dim reader As MySqlDataReader = cmd.ExecuteReader()

                            Do While reader.Read()
                                item.Address = reader("Address").ToString()
                                item.SiteName = reader("SiteName").ToString()
                            Loop
                            con.Close()
                        End Using
                    End Using

                    Using con As New MySqlConnection(constr)
                        Using cmd As New MySqlCommand()
                            cmd.CommandType = CommandType.Text

                            Dim Query As String = "SELECT DEVICEID FROM tblcustomerlocationdevices WHERE LocationID='" & item.LocationID & "'"

                            cmd.CommandText = Query
                            cmd.Connection = con
                            con.Open()
                            Dim reader As MySqlDataReader = cmd.ExecuteReader()

                            Do While reader.Read()
                                Dim ditem As New DeviceIDList
                                ditem.DeviceID = reader("DEVICEID").ToString()
                                DeviceIDList.Add(ditem)
                            Loop
                            con.Close()
                        End Using
                    End Using



                    item.TotHoursWithActivity = 0
                    For Each itemDevice As DeviceIDList In DeviceIDList

                        Using con As New MySqlConnection(constr)
                            Using cmd As New MySqlCommand()
                                cmd.CommandType = CommandType.Text

                                Dim insQuery As String = "SELECT SUM(HasActivity) AS HoursWithActivity  FROM tbldeviceevents e  WHERE e.deviceid = '" & itemDevice.DeviceID & "' " & _
                                                            " AND  e.date BETWEEN '" & RepStartDate & "' AND '" & RepFinishDate & "'"
                                '  Dim insQuery As String = "SELECT SUM(HasActivity) AS HoursWithActivity  FROM tbldeviceevents e  WHERE e.deviceid =  '" & item.DeviceID & "' AND  e.date BETWEEN '" & RepStartDate & "' AND '" & RepFinishDate & "'"

                                cmd.CommandText = insQuery
                                cmd.Connection = con
                                con.Open()
                                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                                Do While reader.Read()
                                    If Not Convert.IsDBNull(reader("HoursWithActivity")) Then
                                        item.TotHoursWithActivity = item.TotHoursWithActivity + CType(reader("HoursWithActivity"), Integer)
                                    Else
                                        item.TotHoursWithActivity = item.TotHoursWithActivity + 0
                                    End If

                                Loop
                                con.Close()
                            End Using
                        End Using

                    Next


                    '  item.DeviceWithActivity = Locationlist.Where(Function(x) x.LocationID = item.LocationID).Count()
                    item.Status = ""

                Next item

                'For Each deviceitem As LocationStatusModel In Locationlist
                '    deviceitem.DeviceWithActivity = Locationlist.Where(Function(x) x.LocationID = deviceitem.LocationID).Count()
                'Next

                gridLocationStatus.DataSource = mainList
                gridLocationStatus.DataBind()

            End If

        End If
    End Sub

#End Region

#Region "Daily"
    Protected Sub rdoDay_CheckedChanged(sender As Object, e As EventArgs)
        ChangedFloorPlanRadioButton()
    End Sub
#End Region

#Region "Week"
    Protected Sub rdoWeek_CheckedChanged(sender As Object, e As EventArgs)
        ChangedFloorPlanRadioButton()
    End Sub
#End Region

#Region "Month"
    Protected Sub rdoMonth_CheckedChanged(sender As Object, e As EventArgs)
        ChangedFloorPlanRadioButton()
    End Sub
#End Region

#Region "Year"
    Protected Sub rdoYear_CheckedChanged(sender As Object, e As EventArgs)
        ChangedFloorPlanRadioButton()
    End Sub
#End Region

#Region "Common Methods"
    Private Sub ChangedFloorPlanRadioButton()

        If rdoDay.Checked = True Then

            lblFromDate.Visible = True
            lblFromWeek.Visible = False
            lblFromMonth.Visible = False
            lblFromYear.Visible = False

            DivFromlocationStatusDate.Visible = True
            dtpFromLocationStatusDate.Visible = True
            ddlfromweekList.Visible = False
            ddlfrommonthyear.Visible = False
            ddlfromyear.Visible = False

            lblToDate.Visible = True
            lblToWeek.Visible = False
            lblToMonth.Visible = False
            lblToYear.Visible = False

            DivTolocationStatusDate.Visible = True
            dtpToLocationStatusDate.Visible = True
            ddltoweekList.Visible = False
            ddltomonthyear.Visible = False
            ddltoyear.Visible = False

        ElseIf rdoWeek.Checked = True Then
            lblFromDate.Visible = False
            lblFromWeek.Visible = True
            lblFromMonth.Visible = False
            lblFromYear.Visible = False

            DivFromlocationStatusDate.Visible = False
            dtpFromLocationStatusDate.Visible = False
            ddlfromweekList.Visible = True
            ddlfrommonthyear.Visible = False
            ddlfromyear.Visible = False

            lblToDate.Visible = False
            lblToWeek.Visible = True
            lblToMonth.Visible = False
            lblToYear.Visible = False

            DivTolocationStatusDate.Visible = False
            dtpToLocationStatusDate.Visible = False
            ddltoweekList.Visible = True
            ddltomonthyear.Visible = False
            ddltoyear.Visible = False

        ElseIf rdoMonth.Checked = True Then

            lblFromDate.Visible = False
            lblFromWeek.Visible = False
            lblFromMonth.Visible = True
            lblFromYear.Visible = False

            DivFromlocationStatusDate.Visible = False
            dtpFromLocationStatusDate.Visible = False
            ddlfromweekList.Visible = False
            ddlfrommonthyear.Visible = True
            ddlfromyear.Visible = False

            lblToDate.Visible = False
            lblToWeek.Visible = False
            lblToMonth.Visible = True
            lblToYear.Visible = False

            DivTolocationStatusDate.Visible = False
            dtpToLocationStatusDate.Visible = False
            ddltoweekList.Visible = False
            ddltomonthyear.Visible = True
            ddltoyear.Visible = False

        ElseIf rdoYear.Checked = True Then
            lblFromDate.Visible = False
            lblFromWeek.Visible = False
            lblFromMonth.Visible = False
            lblFromYear.Visible = True

            DivFromlocationStatusDate.Visible = False
            dtpFromLocationStatusDate.Visible = False
            ddlfromweekList.Visible = False
            ddlfrommonthyear.Visible = False
            ddlfromyear.Visible = True

            lblToDate.Visible = False
            lblToWeek.Visible = False
            lblToMonth.Visible = False
            lblToYear.Visible = True

            DivTolocationStatusDate.Visible = False
            dtpToLocationStatusDate.Visible = False
            ddltoweekList.Visible = False
            ddltomonthyear.Visible = False
            ddltoyear.Visible = True
        Else
        End If
    End Sub
#End Region

#Region "Common Classes"

    Public Class LocationStatusModel
        Public Property LocationID() As String
        Public Property Address() As String
        Public Property SiteName() As String
        Public Property Status() As String
        Public Property DeviceID() As String
        Public Property TotHoursWithActivity() As Integer
        Public Property DeviceWithActivity() As Integer

    End Class

    Public Class WeeklyList
        Public Property floorPlanID() As Integer
        Public Property WeekNumber() As String
        Public Property StartDate() As DateTime
        Public Property EndDate() As DateTime
        Public Property CountValue() As Integer
        Public Property TextValue() As String
    End Class
    Public Class MonthlyList
        Public Property floorPlanID() As Integer
        Public Property MonthYear() As String
        Public Property CountValue() As Integer
        Public Property TextValue() As String
        Public Property StartDate() As String
        Public Property Startdate1() As DateTime
        Public Property EndDate() As DateTime
        Public Property DeviceEvent() As Integer

    End Class

    Public Class YearlyList
        Public Property floorPlanID() As Integer
        Public Property Year() As String
        Public Property CountValue() As Integer
        Public Property TextValue() As String
        Public Property StartDate() As String
        Public Property Startdate1() As DateTime
        Public Property EndDate() As DateTime
        Public Property DeviceEvent() As Integer

    End Class
    Public Class DeviceIDList
        Public Property DeviceID() As String

    End Class

    Public Class tbldeviceeventthreshold

        Public Property LocationID() As String
        Public Property TotLow() As Integer
        Public Property TotLowColor() As String

        Public Property TotMedium() As Integer
        Public Property TotMediumColor() As String

        Public Property TotHigh() As Integer
        Public Property TotHighColor() As String

        Public Property TotVeryHigh() As Integer
        Public Property TotVeryHighColor() As String


        ''Daily
        'Public Property TotDailyLow() As Integer
        'Public Property TotDailyLowColor() As String

        'Public Property TotDailyMedium() As Integer
        'Public Property TotDailyMediumColor() As String

        'Public Property TotDailyHigh() As Integer
        'Public Property TotDailyHighColor() As String

        'Public Property TotDailyVeryHigh() As Integer
        'Public Property TotDailyVeryHighColor() As String

        ''Weekly
        'Public Property TotWeeklyLow() As Integer
        'Public Property TotWeeklyLowColor() As String

        'Public Property TotWeeklyMedium() As Integer
        'Public Property TotWeeklyMediumColor() As String

        'Public Property TotWeeklyHigh() As Integer
        'Public Property TotWeeklyHighColor() As String

        'Public Property TotWeeklyVeryHigh() As Integer
        'Public Property TotWeeklyVeryHighColor() As String

        ''Monthly
        'Public Property TotMonthlyLow() As Integer
        'Public Property TotMonthlyLowColor() As String

        'Public Property TotMonthlyMedium() As Integer
        'Public Property TotMonthlyMediumColor() As String

        'Public Property TotMonthlyHigh() As Integer
        'Public Property TotMonthlyHighColor() As String

        'Public Property TotMonthlyVeryHigh() As Integer
        'Public Property TotMonthlyVeryHighColor() As String

        ''Yearly
        'Public Property TotYearlyLow() As Integer
        'Public Property TotYearlyLowColor() As String

        'Public Property TotYearlyMedium() As Integer
        'Public Property TotYearlyMediumColor() As String

        'Public Property TotYearlyyHigh() As Integer
        'Public Property TotYearlyHighColor() As String

        'Public Property TotYearlyVeryHigh() As Integer
        'Public Property TotYearlyVeryHighColor() As String
    End Class
#End Region

    Protected Sub lnkView_Click(sender As Object, e As EventArgs)

        Dim locationID As String = (CType(sender, LinkButton)).CommandArgument.ToString()

        Response.Redirect("~/Dashboard.aspx?locationID=" + locationID)


    End Sub
End Class
