Imports Microsoft.VisualBasic
Imports System
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO

Partial Class DeviceEventThreshold
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            loadGrid()
            bindAccountIDdropdown()
            bindDeviceTypedropdown()
        End If

    End Sub

    Private Sub loadGrid()
        Dim list As New List(Of DeviceEventThresholdModel)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text


                Dim insQuery As String = "SELECT * FROM tbldeviceeventthreshold"

                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                Do While reader.Read()

                    Dim item As New DeviceEventThresholdModel()

                    If Not IsDBNull(reader("RcNo")) Then
                        item.RcNo = CType(reader("RcNo"), String)
                    Else
                        item.RcNo = 0
                    End If

                    If Not IsDBNull(reader("AccountID")) Then
                        item.AccountID = CType(reader("AccountID"), String)
                    Else
                        item.AccountID = ""
                    End If

                    If Not IsDBNull(reader("DeviceType")) Then
                        item.DeviceType = CType(reader("DeviceType"), String)
                    Else
                        item.DeviceType = ""
                    End If

                    'Daily Start
                    If Not IsDBNull(reader("DailyLow")) Then
                        item.DailyLow = CType(reader("DailyLow"), Integer)
                    Else
                        item.DailyLow = 0
                    End If

                    If Not IsDBNull(reader("DailyLowColor")) Then
                        item.DailyLowColor = CType(reader("DailyLowColor"), String)
                    Else
                        item.DailyLowColor = ""
                    End If

                    If Not IsDBNull(reader("DailyMedium")) Then
                        item.DailyMedium = CType(reader("DailyMedium"), Integer)
                    Else
                        item.DailyMedium = 0
                    End If

                    If Not IsDBNull(reader("DailyMediumColor")) Then
                        item.DailyMediumColor = CType(reader("DailyMediumColor"), String)
                    Else
                        item.DailyMediumColor = ""
                    End If
                    If Not IsDBNull(reader("DailyHigh")) Then
                        item.DailyHigh = CType(reader("DailyHigh"), Integer)
                    Else
                        item.DailyHigh = 0
                    End If

                    If Not IsDBNull(reader("DailyHighColor")) Then
                        item.DailyHighColor = CType(reader("DailyHighColor"), String)
                    Else
                        item.DailyHighColor = ""
                    End If

                    If Not IsDBNull(reader("DailyVeryHigh")) Then
                        item.DailyVeryHigh = CType(reader("DailyVeryHigh"), Integer)
                    Else
                        item.DailyVeryHigh = 0
                    End If

                    If Not IsDBNull(reader("DailyVeryHighColor")) Then
                        item.DailyVeryHighColor = CType(reader("DailyVeryHighColor"), String)
                    Else
                        item.DailyVeryHighColor = ""
                    End If
                    'Daily End

                    'Total Daily Start
                    If Not IsDBNull(reader("TotalDailyLow")) Then
                        item.TotalDailyLow = CType(reader("TotalDailyLow"), Integer)
                    Else
                        item.TotalDailyLow = 0
                    End If
                    If Not IsDBNull(reader("TotalDailyLowColor")) Then
                        item.TotalDailyLowColor = CType(reader("TotalDailyLowColor"), String)
                    Else
                        item.TotalDailyLowColor = ""
                    End If

                    If Not IsDBNull(reader("TotalDailyMedium")) Then
                        item.TotalDailyMedium = CType(reader("TotalDailyMedium"), String)
                    Else
                        item.TotalDailyMedium = ""
                    End If
                    If Not IsDBNull(reader("TotalDailyMediumColor")) Then
                        item.TotalDailyMediumColor = CType(reader("TotalDailyMediumColor"), String)
                    Else
                        item.TotalDailyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("TotalDailyHigh")) Then
                        item.TotalDailyHigh = CType(reader("TotalDailyHigh"), String)
                    Else
                        item.TotalDailyHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalDailyHighColor")) Then
                        item.TotalDailyHighColor = CType(reader("TotalDailyHighColor"), String)
                    Else
                        item.TotalDailyHighColor = ""
                    End If

                    If Not IsDBNull(reader("TotalDailyVeryHigh")) Then
                        item.TotalDailyVeryHigh = CType(reader("TotalDailyVeryHigh"), String)
                    Else
                        item.TotalDailyVeryHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalDailyVeryHighColor")) Then
                        item.TotalDailyVeryHighColor = CType(reader("TotalDailyVeryHighColor"), String)
                    Else
                        item.TotalDailyVeryHighColor = ""
                    End If
                    'Total Daily End

                    'Total Ratio Daily Start
                    If Not IsDBNull(reader("TotalRatioDailyLow")) Then
                        item.TotalRatioDailyLow = CType(reader("TotalRatioDailyLow"), Integer)
                    Else
                        item.TotalRatioDailyLow = 0
                    End If
                    If Not IsDBNull(reader("TotalRatioDailyLowColor")) Then
                        item.TotalRatioDailyLowColor = CType(reader("TotalRatioDailyLowColor"), String)
                    Else
                        item.TotalRatioDailyLowColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioDailyMedium")) Then
                        item.TotalRatioDailyMedium = CType(reader("TotalRatioDailyMedium"), String)
                    Else
                        item.TotalRatioDailyMedium = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioDailyMediumColor")) Then
                        item.TotalRatioDailyMediumColor = CType(reader("TotalRatioDailyMediumColor"), String)
                    Else
                        item.TotalRatioDailyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioDailyHigh")) Then
                        item.TotalRatioDailyHigh = CType(reader("TotalRatioDailyHigh"), String)
                    Else
                        item.TotalRatioDailyHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioDailyHighColor")) Then
                        item.TotalRatioDailyHighColor = CType(reader("TotalRatioDailyHighColor"), String)
                    Else
                        item.TotalRatioDailyHighColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioDailyVeryHigh")) Then
                        item.TotalRatioDailyVeryHigh = CType(reader("TotalRatioDailyVeryHigh"), String)
                    Else
                        item.TotalRatioDailyVeryHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioDailyVeryHighColor")) Then
                        item.TotalRatioDailyVeryHighColor = CType(reader("TotalRatioDailyVeryHighColor"), String)
                    Else
                        item.TotalRatioDailyVeryHighColor = ""
                    End If
                    'Total Ratio Daily End

                    'Weekly Start
                    If Not IsDBNull(reader("WeeklyLow")) Then
                        item.WeeklyLow = CType(reader("WeeklyLow"), Integer)
                    Else
                        item.WeeklyLow = 0
                    End If

                    If Not IsDBNull(reader("WeeklyLowColor")) Then
                        item.WeeklyLowColor = CType(reader("WeeklyLowColor"), String)
                    Else
                        item.WeeklyLowColor = ""
                    End If

                    If Not IsDBNull(reader("WeeklyMedium")) Then
                        item.WeeklyMedium = CType(reader("WeeklyMedium"), Integer)
                    Else
                        item.WeeklyMedium = 0
                    End If

                    If Not IsDBNull(reader("WeeklyMediumColor")) Then
                        item.WeeklyMediumColor = CType(reader("WeeklyMediumColor"), String)
                    Else
                        item.WeeklyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("WeeklyHigh")) Then
                        item.WeeklyHigh = CType(reader("WeeklyHigh"), Integer)
                    Else
                        item.WeeklyHigh = 0
                    End If
                    If Not IsDBNull(reader("WeeklyHighColor")) Then
                        item.WeeklyHighColor = CType(reader("WeeklyHighColor"), String)
                    Else
                        item.WeeklyHighColor = ""
                    End If

                    If Not IsDBNull(reader("WeeklyVeryHigh")) Then
                        item.WeeklyVeryHigh = CType(reader("WeeklyVeryHigh"), Integer)
                    Else
                        item.WeeklyVeryHigh = 0
                    End If

                    If Not IsDBNull(reader("WeeklyVeryHighColor")) Then
                        item.WeeklyVeryHighColor = CType(reader("WeeklyVeryHighColor"), String)
                    Else
                        item.WeeklyVeryHighColor = ""
                    End If
                    'Weekly End

                    'Total Weekly Start
                    If Not IsDBNull(reader("TotalWeeklyLow")) Then
                        item.TotalWeeklyLow = CType(reader("TotalWeeklyLow"), String)
                    Else
                        item.TotalWeeklyLow = ""
                    End If
                    If Not IsDBNull(reader("TotalWeeklyLowColor")) Then
                        item.TotalWeeklyLowColor = CType(reader("TotalWeeklyLowColor"), String)
                    Else
                        item.TotalWeeklyLowColor = ""
                    End If

                    If Not IsDBNull(reader("TotalWeeklyMedium")) Then
                        item.TotalWeeklyMedium = CType(reader("TotalWeeklyMedium"), String)
                    Else
                        item.TotalWeeklyMedium = ""
                    End If
                    If Not IsDBNull(reader("TotalWeeklyMediumColor")) Then
                        item.TotalWeeklyMediumColor = CType(reader("TotalWeeklyMediumColor"), String)
                    Else
                        item.TotalWeeklyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("TotalWeeklyHigh")) Then
                        item.TotalWeeklyHigh = CType(reader("TotalWeeklyHigh"), String)
                    Else
                        item.TotalWeeklyHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalWeeklyHighColor")) Then
                        item.TotalWeeklyHighColor = CType(reader("TotalWeeklyHighColor"), String)
                    Else
                        item.TotalWeeklyHighColor = ""
                    End If


                    If Not IsDBNull(reader("TotalWeeklyVeryHigh")) Then
                        item.TotalWeeklyVeryHigh = CType(reader("TotalWeeklyVeryHigh"), String)
                    Else
                        item.TotalWeeklyVeryHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalWeeklyVeryHighColor")) Then
                        item.TotalWeeklyVeryHighColor = CType(reader("TotalWeeklyVeryHighColor"), String)
                    Else
                        item.TotalWeeklyVeryHighColor = ""
                    End If
                    'Total Weekly End

                    'Total Ratio Weekly Start
                    If Not IsDBNull(reader("TotalRatioWeeklyLow")) Then
                        item.TotalRatioWeeklyLow = CType(reader("TotalRatioWeeklyLow"), String)
                    Else
                        item.TotalRatioWeeklyLow = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioWeeklyLowColor")) Then
                        item.TotalRatioWeeklyLowColor = CType(reader("TotalRatioWeeklyLowColor"), String)
                    Else
                        item.TotalRatioWeeklyLowColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioWeeklyMedium")) Then
                        item.TotalRatioWeeklyMedium = CType(reader("TotalRatioWeeklyMedium"), String)
                    Else
                        item.TotalRatioWeeklyMedium = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioWeeklyMediumColor")) Then
                        item.TotalRatioWeeklyMediumColor = CType(reader("TotalRatioWeeklyMediumColor"), String)
                    Else
                        item.TotalRatioWeeklyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioWeeklyHigh")) Then
                        item.TotalRatioWeeklyHigh = CType(reader("TotalRatioWeeklyHigh"), String)
                    Else
                        item.TotalRatioWeeklyHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioWeeklyHighColor")) Then
                        item.TotalRatioWeeklyHighColor = CType(reader("TotalRatioWeeklyHighColor"), String)
                    Else
                        item.TotalRatioWeeklyHighColor = ""
                    End If


                    If Not IsDBNull(reader("TotalRatioWeeklyVeryHigh")) Then
                        item.TotalRatioWeeklyVeryHigh = CType(reader("TotalRatioWeeklyVeryHigh"), String)
                    Else
                        item.TotalRatioWeeklyVeryHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioWeeklyVeryHighColor")) Then
                        item.TotalRatioWeeklyVeryHighColor = CType(reader("TotalRatioWeeklyVeryHighColor"), String)
                    Else
                        item.TotalRatioWeeklyVeryHighColor = ""
                    End If
                    'Total Ratio Weekly End

                    'Monthly Start
                    If Not IsDBNull(reader("MonthlyLow")) Then
                        item.MonthlyLow = CType(reader("MonthlyLow"), Integer)
                    Else
                        item.MonthlyLow = 0
                    End If

                    If Not IsDBNull(reader("MonthlyLowColor")) Then
                        item.MonthlyLowColor = CType(reader("MonthlyLowColor"), String)
                    Else
                        item.MonthlyLowColor = ""
                    End If

                    If Not IsDBNull(reader("MonthlyMedium")) Then
                        item.MonthlyMedium = CType(reader("MonthlyMedium"), Integer)
                    Else
                        item.MonthlyMedium = 0
                    End If

                    If Not IsDBNull(reader("MonthlyMediumColor")) Then
                        item.MonthlyMediumColor = CType(reader("MonthlyMediumColor"), String)
                    Else
                        item.MonthlyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("MonthlyHigh")) Then
                        item.MonthlyHigh = CType(reader("MonthlyHigh"), Integer)
                    Else
                        item.MonthlyHigh = 0
                    End If

                    If Not IsDBNull(reader("MonthlyHighColor")) Then
                        item.MonthlyHighColor = CType(reader("MonthlyHighColor"), String)
                    Else
                        item.MonthlyHighColor = ""
                    End If

                    If Not IsDBNull(reader("MonthlyVeryHigh")) Then
                        item.MonthlyVeryHigh = CType(reader("MonthlyVeryHigh"), Integer)
                    Else
                        item.MonthlyVeryHigh = 0
                    End If
                    If Not IsDBNull(reader("MonthlyVeryHighColor")) Then
                        item.MonthlyVeryHighColor = CType(reader("MonthlyVeryHighColor"), String)
                    Else
                        item.MonthlyVeryHighColor = ""
                    End If
                    'Monthly End

                    'Total Monthly Start
                    If Not IsDBNull(reader("TotalMonthlyLow")) Then
                        item.TotalMonthlyLow = CType(reader("TotalMonthlyLow"), String)
                    Else
                        item.TotalMonthlyLow = ""
                    End If
                    If Not IsDBNull(reader("TotalMonthlyLowColor")) Then
                        item.TotalMonthlyLowColor = CType(reader("TotalMonthlyLowColor"), String)
                    Else
                        item.TotalMonthlyLowColor = ""
                    End If

                    If Not IsDBNull(reader("TotalMonthlyMedium")) Then
                        item.TotalMonthlyMedium = CType(reader("TotalMonthlyMedium"), String)
                    Else
                        item.TotalMonthlyMedium = ""
                    End If
                    If Not IsDBNull(reader("TotalMonthlyMediumColor")) Then
                        item.TotalMonthlyMediumColor = CType(reader("TotalMonthlyMediumColor"), String)
                    Else
                        item.TotalMonthlyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("TotalMonthlyHigh")) Then
                        item.TotalMonthlyHigh = CType(reader("TotalMonthlyHigh"), String)
                    Else
                        item.TotalMonthlyHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalMonthlyHighColor")) Then
                        item.TotalMonthlyHighColor = CType(reader("TotalMonthlyHighColor"), String)
                    Else
                        item.TotalMonthlyHighColor = ""
                    End If

                    If Not IsDBNull(reader("TotalMonthlyVeryHigh")) Then
                        item.TotalMonthlyVeryHigh = CType(reader("TotalMonthlyVeryHigh"), String)
                    Else
                        item.TotalMonthlyVeryHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalMonthlyVeryHighColor")) Then
                        item.TotalMonthlyVeryHighColor = CType(reader("TotalMonthlyVeryHighColor"), String)
                    Else
                        item.TotalMonthlyVeryHighColor = ""
                    End If
                    'Total Monthly End

                    'Total Ratio Monthly Start
                    If Not IsDBNull(reader("TotalRatioMonthlyLow")) Then
                        item.TotalRatioMonthlyLow = CType(reader("TotalRatioMonthlyLow"), String)
                    Else
                        item.TotalRatioMonthlyLow = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioMonthlyLowColor")) Then
                        item.TotalRatioMonthlyLowColor = CType(reader("TotalRatioMonthlyLowColor"), String)
                    Else
                        item.TotalRatioMonthlyLowColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioMonthlyMedium")) Then
                        item.TotalRatioMonthlyMedium = CType(reader("TotalRatioMonthlyMedium"), String)
                    Else
                        item.TotalRatioMonthlyMedium = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioMonthlyMediumColor")) Then
                        item.TotalRatioMonthlyMediumColor = CType(reader("TotalRatioMonthlyMediumColor"), String)
                    Else
                        item.TotalRatioMonthlyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioMonthlyHigh")) Then
                        item.TotalRatioMonthlyHigh = CType(reader("TotalRatioMonthlyHigh"), String)
                    Else
                        item.TotalRatioMonthlyHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioMonthlyHighColor")) Then
                        item.TotalRatioMonthlyHighColor = CType(reader("TotalRatioMonthlyHighColor"), String)
                    Else
                        item.TotalRatioMonthlyHighColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioMonthlyVeryHigh")) Then
                        item.TotalRatioMonthlyVeryHigh = CType(reader("TotalRatioMonthlyVeryHigh"), String)
                    Else
                        item.TotalRatioMonthlyVeryHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioMonthlyVeryHighColor")) Then
                        item.TotalRatioMonthlyVeryHighColor = CType(reader("TotalRatioMonthlyVeryHighColor"), String)
                    Else
                        item.TotalRatioMonthlyVeryHighColor = ""
                    End If
                    'Total Ratio Monthly End

                    'Yearly Start
                    If Not IsDBNull(reader("YearlyLow")) Then
                        item.YearlyLow = CType(reader("YearlyLow"), Integer)
                    Else
                        item.YearlyLow = 0
                    End If

                    If Not IsDBNull(reader("YearlyLowColor")) Then
                        item.YearlyLowColor = CType(reader("YearlyLowColor"), String)
                    Else
                        item.YearlyLowColor = ""
                    End If

                    If Not IsDBNull(reader("YearlyMedium")) Then
                        item.YearlyMedium = CType(reader("YearlyMedium"), Integer)
                    Else
                        item.YearlyMedium = 0
                    End If

                    If Not IsDBNull(reader("YearlyMediumColor")) Then
                        item.YearlyMediumColor = CType(reader("YearlyMediumColor"), String)
                    Else
                        item.YearlyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("YearlyHigh")) Then
                        item.YearlyHigh = CType(reader("YearlyHigh"), Integer)
                    Else
                        item.YearlyHigh = 0
                    End If

                    If Not IsDBNull(reader("YearlyHighColor")) Then
                        item.YearlyHighColor = CType(reader("YearlyHighColor"), String)
                    Else
                        item.YearlyHighColor = ""
                    End If

                    If Not IsDBNull(reader("YearlyVeryHigh")) Then
                        item.YearlyVeryHigh = CType(reader("YearlyVeryHigh"), Integer)
                    Else
                        item.YearlyVeryHigh = 0
                    End If

                    If Not IsDBNull(reader("YearlyVeryHighColor")) Then
                        item.YearlyVeryHighColor = CType(reader("YearlyVeryHighColor"), String)
                    Else
                        item.YearlyVeryHighColor = ""
                    End If
                    'Yearly End

                    'Total Yearly Start
                    If Not IsDBNull(reader("TotalYearlyLow")) Then
                        item.TotalYearlyLow = CType(reader("TotalYearlyLow"), String)
                    Else
                        item.TotalYearlyLow = ""
                    End If
                    If Not IsDBNull(reader("TotalYearlyLowColor")) Then
                        item.TotalYearlyLowColor = CType(reader("TotalYearlyLowColor"), String)
                    Else
                        item.TotalYearlyLowColor = ""
                    End If

                    If Not IsDBNull(reader("TotalYearlyMedium")) Then
                        item.TotalYearlyMedium = CType(reader("TotalYearlyMedium"), String)
                    Else
                        item.TotalYearlyMedium = ""
                    End If
                    If Not IsDBNull(reader("TotalYearlyMediumColor")) Then
                        item.TotalYearlyMediumColor = CType(reader("TotalYearlyMediumColor"), String)
                    Else
                        item.TotalYearlyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("TotalYearlyHigh")) Then
                        item.TotalYearlyHigh = CType(reader("TotalYearlyHigh"), String)
                    Else
                        item.TotalYearlyHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalYearlyHighColor")) Then
                        item.TotalYearlyHighColor = CType(reader("TotalYearlyHighColor"), String)
                    Else
                        item.TotalYearlyHighColor = ""
                    End If

                    If Not IsDBNull(reader("TotalYearlyVeryHigh")) Then
                        item.TotalYearlyVeryHigh = CType(reader("TotalYearlyVeryHigh"), String)
                    Else
                        item.TotalYearlyVeryHigh = ""
                    End If

                    If Not IsDBNull(reader("TotalYearlyVeryHighColor")) Then
                        item.TotalYearlyVeryHighColor = CType(reader("TotalYearlyVeryHighColor"), String)
                    Else
                        item.TotalYearlyVeryHighColor = ""
                    End If
                    'Total Yearly End

                    'Total Ratio Yearly Start
                    If Not IsDBNull(reader("TotalRatioYearlyLow")) Then
                        item.TotalRatioYearlyLow = CType(reader("TotalRatioYearlyLow"), String)
                    Else
                        item.TotalRatioYearlyLow = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioYearlyLowColor")) Then
                        item.TotalRatioYearlyLowColor = CType(reader("TotalRatioYearlyLowColor"), String)
                    Else
                        item.TotalRatioYearlyLowColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioYearlyMedium")) Then
                        item.TotalRatioYearlyMedium = CType(reader("TotalRatioYearlyMedium"), String)
                    Else
                        item.TotalRatioYearlyMedium = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioYearlyMediumColor")) Then
                        item.TotalRatioYearlyMediumColor = CType(reader("TotalRatioYearlyMediumColor"), String)
                    Else
                        item.TotalRatioYearlyMediumColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioYearlyHigh")) Then
                        item.TotalRatioYearlyHigh = CType(reader("TotalRatioYearlyHigh"), String)
                    Else
                        item.TotalRatioYearlyHigh = ""
                    End If
                    If Not IsDBNull(reader("TotalRatioYearlyHighColor")) Then
                        item.TotalRatioYearlyHighColor = CType(reader("TotalRatioYearlyHighColor"), String)
                    Else
                        item.TotalRatioYearlyHighColor = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioYearlyVeryHigh")) Then
                        item.TotalRatioYearlyVeryHigh = CType(reader("TotalRatioYearlyVeryHigh"), String)
                    Else
                        item.TotalRatioYearlyVeryHigh = ""
                    End If

                    If Not IsDBNull(reader("TotalRatioYearlyVeryHighColor")) Then
                        item.TotalRatioYearlyVeryHighColor = CType(reader("TotalRatioYearlyVeryHighColor"), String)
                    Else
                        item.TotalRatioYearlyVeryHighColor = ""
                    End If
                    'Total Ratio Yearly End

                    If Not IsDBNull(reader("DailyLowLabel")) Then
                        item.DailyLowLabel = CType(reader("DailyLowLabel"), String)
                    Else
                        item.DailyLowLabel = ""
                    End If

                    If Not IsDBNull(reader("DailyMediumLabel")) Then
                        item.DailyMediumLabel = CType(reader("DailyMediumLabel"), String)
                    Else
                        item.DailyMediumLabel = ""
                    End If
                    If Not IsDBNull(reader("DailyHighLabel")) Then
                        item.DailyHighLabel = CType(reader("DailyHighLabel"), String)
                    Else
                        item.DailyHighLabel = ""
                    End If
                    If Not IsDBNull(reader("DailyVeryHighLabel")) Then
                        item.DailyVeryHighLabel = CType(reader("DailyVeryHighLabel"), String)
                    Else
                        item.DailyVeryHighLabel = ""
                    End If
                    If Not IsDBNull(reader("WeeklyLowLabel")) Then
                        item.WeeklyLowLabel = CType(reader("WeeklyLowLabel"), String)
                    Else
                        item.WeeklyLowLabel = ""
                    End If

                    If Not IsDBNull(reader("WeeklyMediumLabel")) Then
                        item.WeeklyMediumLabel = CType(reader("WeeklyMediumLabel"), String)
                    Else
                        item.WeeklyMediumLabel = ""
                    End If
                    If Not IsDBNull(reader("WeeklyHighLabel")) Then
                        item.WeeklyHighLabel = CType(reader("WeeklyHighLabel"), String)
                    Else
                        item.WeeklyHighLabel = ""
                    End If
                    If Not IsDBNull(reader("WeeklyVeryHighLabel")) Then
                        item.WeeklyVeryHighLabel = CType(reader("WeeklyVeryHighLabel"), String)
                    Else
                        item.WeeklyVeryHighLabel = ""
                    End If
                    If Not IsDBNull(reader("MonthlyLowLabel")) Then
                        item.MonthlyLowLabel = CType(reader("MonthlyLowLabel"), String)
                    Else
                        item.MonthlyLowLabel = ""
                    End If

                    If Not IsDBNull(reader("MonthlyMediumLabel")) Then
                        item.MonthlyMediumLabel = CType(reader("MonthlyMediumLabel"), String)
                    Else
                        item.MonthlyMediumLabel = ""
                    End If
                    If Not IsDBNull(reader("MonthlyHighLabel")) Then
                        item.MonthlyHighLabel = CType(reader("MonthlyHighLabel"), String)
                    Else
                        item.MonthlyHighLabel = ""
                    End If
                    If Not IsDBNull(reader("MonthlyVeryHighLabel")) Then
                        item.MonthlyVeryHighLabel = CType(reader("MonthlyVeryHighLabel"), String)
                    Else
                        item.MonthlyVeryHighLabel = ""
                    End If

                    If Not IsDBNull(reader("YearlyLowLabel")) Then
                        item.YearlyLowLabel = CType(reader("YearlyLowLabel"), String)
                    Else
                        item.YearlyLowLabel = ""
                    End If

                    If Not IsDBNull(reader("YearlyMediumLabel")) Then
                        item.YearlyMediumLabel = CType(reader("YearlyMediumLabel"), String)
                    Else
                        item.YearlyMediumLabel = ""
                    End If
                    If Not IsDBNull(reader("YearlyHighLabel")) Then
                        item.YearlyHighLabel = CType(reader("YearlyHighLabel"), String)
                    Else
                        item.YearlyHighLabel = ""
                    End If
                    If Not IsDBNull(reader("YearlyVeryHighLabel")) Then
                        item.YearlyVeryHighLabel = CType(reader("YearlyVeryHighLabel"), String)
                    Else
                        item.YearlyVeryHighLabel = ""
                    End If

                    list.Add(item)
                Loop
                con.Close()
            End Using

            GridDeviceEvent.DataSource = list
            GridDeviceEvent.DataBind()
        End Using
    End Sub

    Private Sub bindAccountIDdropdown()

        Dim accountIDlist As New List(Of DeviceEventThresholdModel)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text

                Dim insQuery2 As String = "select distinct AccountID from tblcompanylocation where AccountID is not Null"
                cmd.CommandText = insQuery2
                cmd.Connection = con
                con.Open()

                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()

                    Dim item As New DeviceEventThresholdModel()

                    item.AccountID = CType(reader("AccountID"), String)

                    accountIDlist.Add(item)
                Loop
                con.Close()

            End Using
        End Using

        ddlAccountID.DataSource = accountIDlist
        ddlAccountID.DataTextField = "AccountID"
        ddlAccountID.DataValueField = "AccountID"
        ddlAccountID.DataBind()
        'ddlAccountID.Items.Insert(0, New ListItem("", ""))
    End Sub

    Private Sub bindDeviceTypedropdown()

        Dim deviceTypelist As New List(Of DeviceEventThresholdModel)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text

                Dim insQuery2 As String = "SELECT * FROM tbldevicetype"
                cmd.CommandText = insQuery2
                cmd.Connection = con
                con.Open()

                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Do While reader.Read()

                    Dim item As New DeviceEventThresholdModel()

                    If Not IsDBNull(reader("DeviceType")) Then
                        item.DeviceType = CType(reader("DeviceType"), String)
                    Else
                        item.DeviceType = ""
                    End If

                    deviceTypelist.Add(item)
                Loop
                con.Close()

            End Using
        End Using

        ddlDeviceType.DataSource = deviceTypelist
        ddlDeviceType.DataTextField = "DeviceType"
        ddlDeviceType.DataValueField = "DeviceType"
        ddlDeviceType.DataBind()
        'ddlDeviceType.Items.Insert(0, New ListItem("DEFAULT", "DEFAULT"))
    End Sub

    Protected Sub btnAddDeviceEventThreshold_Click(sender As Object, e As EventArgs)

        hiddenRcNo.Value = 0
        ddlAccountID.SelectedIndex = 0
        ddlDeviceType.SelectedIndex = 0

        ddlAccountID.Visible = True
        ddlDeviceType.Visible = True
        lblAccountID.Visible = True
        lblDeviceType.Visible = True
        lblHeading.Text = "Device & Site Event Threshold"


        txtDailyLow.Text = "0"
        txtDailyLowColor.Text = ""

        txtTotalDailyLow.Text = ""
        txtTotalDailyLowColor.Text = ""

        txtTotalRatioDailyLow.Text = ""
        txtTotalRatioDailyLowColor.Text = ""

        txtDailyMedium.Text = "1"
        txtDailyMediumColor.Text = ""

        txtTotalDailyMedium.Text = ""
        txtTotalDailyMediumColor.Text = ""

        txtTotalRatioDailyMedium.Text = ""
        txtTotalRatioDailyMediumColor.Text = ""

        txtDailyHigh.Text = "3"
        txtDailyHighColor.Text = ""

        txtTotalDailyHigh.Text = ""
        txtTotalDailyHighColor.Text = ""

        txtTotalRatioDailyHigh.Text = ""
        txtTotalRatioDailyHighColor.Text = ""

        txtDailyVeryHigh.Text = "5"
        txtDailyVeryHighColor.Text = ""

        txtTotalDailyVeryHigh.Text = ""
        txtTotalDailyVeryHighColor.Text = ""

        txtTotalRatioDailyVeryHigh.Text = ""
        txtTotalRatioDailyVeryHighColor.Text = ""

        hdnisLogoClearDailylow.Value = 0
        hdnisLogoClearDailyMedium.Value = 0
        hdnisLogoClearDailyHigh.Value = 0
        hdnisLogoClearDailyVeryHigh.Value = 0

        hdnisLogoClearTotalDailylow.Value = 0
        hdnisLogoClearTotalDailyMedium.Value = 0
        hdnisLogoClearTotalDailyHigh.Value = 0
        hdnisLogoClearTotalDailyVeryHigh.Value = 0

        hdnisLogoClearTotalRatioDailylow.Value = 0
        hdnisLogoClearTotalRatioDailyMedium.Value = 0
        hdnisLogoClearTotalRatioDailyHigh.Value = 0
        hdnisLogoClearTotalRatioDailyVeryHigh.Value = 0

        txtWeeklyLow.Text = ""
        txtWeeklyLowColor.Text = ""

        txtTotalWeeklyLow.Text = ""
        txtTotalWeeklyLowColor.Text = ""

        txtTotalRatioWeeklyLow.Text = ""
        txtTotalRatioWeeklyLowColor.Text = ""

        txtWeeklyMedium.Text = ""
        txtWeeklyMediumColor.Text = ""

        txtTotalWeeklyMedium.Text = ""
        txtTotalWeeklyMediumColor.Text = ""

        txtTotalRatioWeeklyMedium.Text = ""
        txtTotalRatioWeeklyMediumColor.Text = ""

        txtWeeklyHigh.Text = ""
        txtWeeklyHighColor.Text = ""

        txtTotalWeeklyHigh.Text = ""
        txtTotalWeeklyHighColor.Text = ""

        txtTotalRatioWeeklyHigh.Text = ""
        txtTotalRatioWeeklyHighColor.Text = ""

        txtWeeklyVeryHigh.Text = ""
        txtWeeklyVeryHighColor.Text = ""

        txtTotalWeeklyVeryHigh.Text = ""
        txtTotalWeeklyVeryHighColor.Text = ""

        txtTotalRatioWeeklyVeryHigh.Text = ""
        txtTotalRatioWeeklyVeryHighColor.Text = ""

        hdnisLogoClearWeeklylow.Value = 0
        hdnisLogoClearWeeklyMedium.Value = 0
        hdnisLogoClearWeeklyHigh.Value = 0
        hdnisLogoClearWeeklyVeryHigh.Value = 0

        hdnisLogoClearTotalWeeklylow.Value = 0
        hdnisLogoClearTotalWeeklyMedium.Value = 0
        hdnisLogoClearTotalWeeklyHigh.Value = 0
        hdnisLogoClearTotalWeeklyVeryHigh.Value = 0

        hdnisLogoClearTotalRatioWeeklylow.Value = 0
        hdnisLogoClearTotalRatioWeeklyMedium.Value = 0
        hdnisLogoClearTotalRatioWeeklyHigh.Value = 0
        hdnisLogoClearTotalRatioWeeklyVeryHigh.Value = 0

        txtMonthlyLow.Text = ""
        txtMonthlyLowColor.Text = ""

        txtTotalMonthlyLow.Text = ""
        txtTotalMonthlyLowColor.Text = ""

        txtTotalRatioMonthlyLow.Text = ""
        txtTotalRatioMonthlyLowColor.Text = ""

        txtMonthlyMedium.Text = ""
        txtMonthlyMediumColor.Text = ""

        txtTotalMonthlyMedium.Text = ""
        txtTotalMonthlyMediumColor.Text = ""

        txtTotalRatioMonthlyMedium.Text = ""
        txtTotalRatioMonthlyMediumColor.Text = ""

        txtMonthlyHigh.Text = ""
        txtMonthlyHighColor.Text = ""

        txtTotalMonthlyHigh.Text = ""
        txtTotalMonthlyHighColor.Text = ""

        txtTotalRatioMonthlyHigh.Text = ""
        txtTotalRatioMonthlyHighColor.Text = ""

        txtMonthlyVeryHigh.Text = ""
        txtMonthlyVeryHighColor.Text = ""

        txtTotalMonthlyVeryHigh.Text = ""
        txtTotalMonthlyVeryHighColor.Text = ""

        txtTotalRatioMonthlyVeryHigh.Text = ""
        txtTotalRatioMonthlyVeryHighColor.Text = ""

        hdnisLogoClearMonthlylow.Value = 0
        hdnisLogoClearMonthlyMedium.Value = 0
        hdnisLogoClearMonthlyHigh.Value = 0
        hdnisLogoClearMonthlyVeryHigh.Value = 0

        hdnisLogoClearTotalMonthlylow.Value = 0
        hdnisLogoClearTotalMonthlyMedium.Value = 0
        hdnisLogoClearTotalMonthlyHigh.Value = 0
        hdnisLogoClearTotalMonthlyVeryHigh.Value = 0

        hdnisLogoClearTotalRatioMonthlylow.Value = 0
        hdnisLogoClearTotalRatioMonthlyMedium.Value = 0
        hdnisLogoClearTotalRatioMonthlyHigh.Value = 0
        hdnisLogoClearTotalRatioMonthlyVeryHigh.Value = 0

        txtYearlyLow.Text = ""
        txtYearlyLowColor.Text = ""

        txtTotalYearlyLow.Text = ""
        txtTotalYearlyLowColor.Text = ""

        txtTotalRatioYearlyLow.Text = ""
        txtTotalRatioYearlyLowColor.Text = ""

        txtYearlyMedium.Text = ""
        txtYearlyMediumColor.Text = ""

        txtTotalYearlyMedium.Text = ""
        txtTotalYearlyMediumColor.Text = ""

        txtTotalRatioYearlyMedium.Text = ""
        txtTotalRatioYearlyMediumColor.Text = ""

        txtYearlyHigh.Text = ""
        txtYearlyHighColor.Text = ""

        txtTotalYearlyHigh.Text = ""
        txtTotalYearlyHighColor.Text = ""

        txtTotalRatioYearlyHigh.Text = ""
        txtTotalRatioYearlyHighColor.Text = ""

        txtYearlyVeryHigh.Text = ""
        txtYearlyVeryHighColor.Text = ""

        txtTotalYearlyVeryHigh.Text = ""
        txtTotalYearlyVeryHighColor.Text = ""

        txtTotalRatioYearlyVeryHigh.Text = ""
        txtTotalRatioYearlyVeryHighColor.Text = ""

        hdnisLogoClearYearlylow.Value = 0
        hdnisLogoClearYearlyMedium.Value = 0
        hdnisLogoClearYearlyHigh.Value = 0
        hdnisLogoClearYearlyVeryHigh.Value = 0

        hdnisLogoClearTotalYearlylow.Value = 0
        hdnisLogoClearTotalYearlyMedium.Value = 0
        hdnisLogoClearTotalYearlyHigh.Value = 0
        hdnisLogoClearTotalYearlyVeryHigh.Value = 0

        hdnisLogoClearTotalRatioYearlylow.Value = 0
        hdnisLogoClearTotalRatioYearlyMedium.Value = 0
        hdnisLogoClearTotalRatioYearlyHigh.Value = 0
        hdnisLogoClearTotalRatioYearlyVeryHigh.Value = 0

        txtDailyLowLabel.Text = "No Activity"
        txtDailyMediumLabel.Text = "Low Activity"
        txtDailyHighLabel.Text = "Medium Activity"
        txtDailyVeryHighLabel.Text = "High Activity"

        txtWeeklyLowLabel.Text = "No Activity"
        txtWeeklyMediumLabel.Text = "Low Activity"
        txtWeeklyHighLabel.Text = "Medium Activity"
        txtWeeklyVeryHighLabel.Text = "High Activity"

        txtMonthlyLowLabel.Text = "No Activity"
        txtMonthlyMediumLabel.Text = "Low Activity"
        txtMonthlyHighLabel.Text = "Medium Activity"
        txtMonthlyVeryHighLabel.Text = "High Activity"

        txtYearlyLowLabel.Text = "No Activity"
        txtYearlyMediumLabel.Text = "Low Activity"
        txtYearlyHighLabel.Text = "Medium Activity"
        txtYearlyVeryHighLabel.Text = "High Activity"

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showpopup();", True)
    End Sub




    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Using row1 As GridViewRow = CType((CType(sender, LinkButton)).Parent.Parent, GridViewRow)
            Dim idx As Integer = row1.RowIndex

            If idx <= GridDeviceEvent.Rows.Count Then


                Dim row As GridViewRow = GridDeviceEvent.Rows(idx)
                Dim hdnRcNo As Label = TryCast(row.FindControl("hdnRcNo"), Label)
                Dim lblAccountID As Label = TryCast(row.FindControl("lblAccountID"), Label)
                Dim lblDeviceType As Label = TryCast(row.FindControl("lblDeviceType"), Label)

                'Daily Start
                Dim lblDailyLow As Label = TryCast(row.FindControl("lblDailyLow"), Label)
                Dim lblDailyLowColor As Label = TryCast(row.FindControl("lblDailyLowColor"), Label)
                Dim lblDailyMedium As Label = TryCast(row.FindControl("lblDailyMedium"), Label)
                Dim lblDailyMediumColor As Label = TryCast(row.FindControl("lblDailyMediumColor"), Label)
                Dim lblDailyHigh As Label = TryCast(row.FindControl("lblDailyHigh"), Label)
                Dim lblDailyHighColor As Label = TryCast(row.FindControl("lblDailyHighColor"), Label)
                Dim lblDailyVeryHigh As Label = TryCast(row.FindControl("lblDailyVeryHigh"), Label)
                Dim lblDailyVeryHighColor As Label = TryCast(row.FindControl("lblDailyVeryHighColor"), Label)

                'Total Daily Start
                Dim lblTotalDailyLow As Label = TryCast(row.FindControl("lblTotalDailyLow"), Label)
                Dim lblTotalDailyLowColor As Label = TryCast(row.FindControl("lblTotalDailyLowColor"), Label)
                Dim lblTotalDailyMedium As Label = TryCast(row.FindControl("lblTotalDailyMedium"), Label)
                Dim lblTotalDailyMediumColor As Label = TryCast(row.FindControl("lblTotalDailyMediumColor"), Label)
                Dim lblTotalDailyHigh As Label = TryCast(row.FindControl("lblTotalDailyHigh"), Label)
                Dim lblTotalDailyHighColor As Label = TryCast(row.FindControl("lblTotalDailyHighColor"), Label)
                Dim lblTotalDailyVeryHigh As Label = TryCast(row.FindControl("lblTotalDailyVeryHigh"), Label)
                Dim lblTotalDailyVeryHighColor As Label = TryCast(row.FindControl("lblTotalDailyVeryHighColor"), Label)

                'Total Ratio Daily Start
                Dim lblTotalRatioDailyLow As Label = TryCast(row.FindControl("lblTotalRatioDailyLow"), Label)
                Dim lblTotalRatioDailyLowColor As Label = TryCast(row.FindControl("lblTotalRatioDailyLowColor"), Label)
                Dim lblTotalRatioDailyMedium As Label = TryCast(row.FindControl("lblTotalRatioDailyMedium"), Label)
                Dim lblTotalRatioDailyMediumColor As Label = TryCast(row.FindControl("lblTotalRatioDailyMediumColor"), Label)
                Dim lblTotalRatioDailyHigh As Label = TryCast(row.FindControl("lblTotalRatioDailyHigh"), Label)
                Dim lblTotalRatioDailyHighColor As Label = TryCast(row.FindControl("lblTotalRatioDailyHighColor"), Label)
                Dim lblTotalRatioDailyVeryHigh As Label = TryCast(row.FindControl("lblTotalRatioDailyVeryHigh"), Label)
                Dim lblTotalRatioDailyVeryHighColor As Label = TryCast(row.FindControl("lblTotalRatioDailyVeryHighColor"), Label)

                'Weekly Start
                Dim lblWeeklyLow As Label = TryCast(row.FindControl("lblWeeklyLow"), Label)
                Dim lblWeeklyLowColor As Label = TryCast(row.FindControl("lblWeeklyLowColor"), Label)
                Dim lblWeeklyMedium As Label = TryCast(row.FindControl("lblWeeklyMedium"), Label)
                Dim lblWeeklyMediumColor As Label = TryCast(row.FindControl("lblWeeklyMediumColor"), Label)
                Dim lblWeeklyHigh As Label = TryCast(row.FindControl("lblWeeklyHigh"), Label)
                Dim lblWeeklyHighColor As Label = TryCast(row.FindControl("lblWeeklyHighColor"), Label)
                Dim lblWeeklyVeryHigh As Label = TryCast(row.FindControl("lblWeeklyVeryHigh"), Label)
                Dim lblWeeklyVeryHighColor As Label = TryCast(row.FindControl("lblWeeklyVeryHighColor"), Label)

                'Total Weekly Start
                Dim lblTotalWeeklyLow As Label = TryCast(row.FindControl("lblTotalWeeklyLow"), Label)
                Dim lblTotalWeeklyLowColor As Label = TryCast(row.FindControl("lblTotalWeeklyLowColor"), Label)
                Dim lblTotalWeeklyMedium As Label = TryCast(row.FindControl("lblTotalWeeklyMedium"), Label)
                Dim lblTotalWeeklyMediumColor As Label = TryCast(row.FindControl("lblTotalWeeklyMediumColor"), Label)
                Dim lblTotalWeeklyHigh As Label = TryCast(row.FindControl("lblTotalWeeklyHigh"), Label)
                Dim lblTotalWeeklyHighColor As Label = TryCast(row.FindControl("lblTotalWeeklyHighColor"), Label)
                Dim lblTotalWeeklyVeryHigh As Label = TryCast(row.FindControl("lblTotalWeeklyVeryHigh"), Label)
                Dim lblTotalWeeklyVeryHighColor As Label = TryCast(row.FindControl("lblTotalWeeklyVeryHighColor"), Label)

                'Total Ratio Weekly Start
                Dim lblTotalRatioWeeklyLow As Label = TryCast(row.FindControl("lblTotalRatioWeeklyLow"), Label)
                Dim lblTotalRatioWeeklyLowColor As Label = TryCast(row.FindControl("lblTotalRatioWeeklyLowColor"), Label)
                Dim lblTotalRatioWeeklyMedium As Label = TryCast(row.FindControl("lblTotalRatioWeeklyMedium"), Label)
                Dim lblTotalRatioWeeklyMediumColor As Label = TryCast(row.FindControl("lblTotalRatioWeeklyMediumColor"), Label)
                Dim lblTotalRatioWeeklyHigh As Label = TryCast(row.FindControl("lblTotalRatioWeeklyHigh"), Label)
                Dim lblTotalRatioWeeklyHighColor As Label = TryCast(row.FindControl("lblTotalRatioWeeklyHighColor"), Label)
                Dim lblTotalRatioWeeklyVeryHigh As Label = TryCast(row.FindControl("lblTotalRatioWeeklyVeryHigh"), Label)
                Dim lblTotalRatioWeeklyVeryHighColor As Label = TryCast(row.FindControl("lblTotalRatioWeeklyVeryHighColor"), Label)

                'Monthly Start
                Dim lblMonthlyLow As Label = TryCast(row.FindControl("lblMonthlyLow"), Label)
                Dim lblMonthlyLowColor As Label = TryCast(row.FindControl("lblMonthlyLowColor"), Label)
                Dim lblMonthlyMedium As Label = TryCast(row.FindControl("lblMonthlyMedium"), Label)
                Dim lblMonthlyMediumColor As Label = TryCast(row.FindControl("lblMonthlyMediumColor"), Label)
                Dim lblMonthlyHigh As Label = TryCast(row.FindControl("lblMonthlyHigh"), Label)
                Dim lblMonthlyHighColor As Label = TryCast(row.FindControl("lblMonthlyHighColor"), Label)
                Dim lblMonthlyVeryHigh As Label = TryCast(row.FindControl("lblMonthlyVeryHigh"), Label)
                Dim lblMonthlyVeryHighColor As Label = TryCast(row.FindControl("lblMonthlyVeryHighColor"), Label)

                'Total Monthly Start
                Dim lblTotalMonthlyLow As Label = TryCast(row.FindControl("lblTotalMonthlyLow"), Label)
                Dim lblTotalMonthlyLowColor As Label = TryCast(row.FindControl("lblTotalMonthlyLowColor"), Label)
                Dim lblTotalMonthlyMedium As Label = TryCast(row.FindControl("lblTotalMonthlyMedium"), Label)
                Dim lblTotalMonthlyMediumColor As Label = TryCast(row.FindControl("lblTotalMonthlyMediumColor"), Label)
                Dim lblTotalMonthlyHigh As Label = TryCast(row.FindControl("lblTotalMonthlyHigh"), Label)
                Dim lblTotalMonthlyHighColor As Label = TryCast(row.FindControl("lblTotalMonthlyHighColor"), Label)
                Dim lblTotalMonthlyVeryHigh As Label = TryCast(row.FindControl("lblTotalMonthlyVeryHigh"), Label)
                Dim lblTotalMonthlyVeryHighColor As Label = TryCast(row.FindControl("lblTotalMonthlyVeryHighColor"), Label)

                'Total Ratio Monthly Start
                Dim lblTotalRatioMonthlyLow As Label = TryCast(row.FindControl("lblTotalRatioMonthlyLow"), Label)
                Dim lblTotalRatioMonthlyLowColor As Label = TryCast(row.FindControl("lblTotalRatioMonthlyLowColor"), Label)
                Dim lblTotalRatioMonthlyMedium As Label = TryCast(row.FindControl("lblTotalRatioMonthlyMedium"), Label)
                Dim lblTotalRatioMonthlyMediumColor As Label = TryCast(row.FindControl("lblTotalRatioMonthlyMediumColor"), Label)
                Dim lblTotalRatioMonthlyHigh As Label = TryCast(row.FindControl("lblTotalRatioMonthlyHigh"), Label)
                Dim lblTotalRatioMonthlyHighColor As Label = TryCast(row.FindControl("lblTotalRatioMonthlyHighColor"), Label)
                Dim lblTotalRatioMonthlyVeryHigh As Label = TryCast(row.FindControl("lblTotalRatioMonthlyVeryHigh"), Label)
                Dim lblTotalRatioMonthlyVeryHighColor As Label = TryCast(row.FindControl("lblTotalRatioMonthlyVeryHighColor"), Label)

                'Yearly Start
                Dim lblYearlyLow As Label = TryCast(row.FindControl("lblYearlyLow"), Label)
                Dim lblYearlyLowColor As Label = TryCast(row.FindControl("lblYearlyLowColor"), Label)
                Dim lblYearlyMedium As Label = TryCast(row.FindControl("lblYearlyMedium"), Label)
                Dim lblYearlyMediumColor As Label = TryCast(row.FindControl("lblYearlyMediumColor"), Label)
                Dim lblYearlyHigh As Label = TryCast(row.FindControl("lblYearlyHigh"), Label)
                Dim lblYearlyHighColor As Label = TryCast(row.FindControl("lblYearlyHighColor"), Label)
                Dim lblYearlyVeryHigh As Label = TryCast(row.FindControl("lblYearlyVeryHigh"), Label)
                Dim lblYearlyVeryHighColor As Label = TryCast(row.FindControl("lblYearlyVeryHighColor"), Label)

                'Total Yearly Start
                Dim lblTotalYearlyLow As Label = TryCast(row.FindControl("lblTotalYearlyLow"), Label)
                Dim lblTotalYearlyLowColor As Label = TryCast(row.FindControl("lblTotalYearlyLowColor"), Label)
                Dim lblTotalYearlyMedium As Label = TryCast(row.FindControl("lblTotalYearlyMedium"), Label)
                Dim lblTotalYearlyMediumColor As Label = TryCast(row.FindControl("lblTotalYearlyMediumColor"), Label)
                Dim lblTotalYearlyHigh As Label = TryCast(row.FindControl("lblTotalYearlyHigh"), Label)
                Dim lblTotalYearlyHighColor As Label = TryCast(row.FindControl("lblTotalYearlyHighColor"), Label)
                Dim lblTotalYearlyVeryHigh As Label = TryCast(row.FindControl("lblTotalYearlyVeryHigh"), Label)
                Dim lblTotalYearlyVeryHighColor As Label = TryCast(row.FindControl("lblTotalYearlyVeryHighColor"), Label)

                'Total Ratio Yearly Start
                Dim lblTotalRatioYearlyLow As Label = TryCast(row.FindControl("lblTotalRatioYearlyLow"), Label)
                Dim lblTotalRatioYearlyLowColor As Label = TryCast(row.FindControl("lblTotalRatioYearlyLowColor"), Label)
                Dim lblTotalRatioYearlyMedium As Label = TryCast(row.FindControl("lblTotalRatioYearlyMedium"), Label)
                Dim lblTotalRatioYearlyMediumColor As Label = TryCast(row.FindControl("lblTotalRatioYearlyMediumColor"), Label)
                Dim lblTotalRatioYearlyHigh As Label = TryCast(row.FindControl("lblTotalRatioYearlyHigh"), Label)
                Dim lblTotalRatioYearlyHighColor As Label = TryCast(row.FindControl("lblTotalRatioYearlyHighColor"), Label)
                Dim lblTotalRatioYearlyVeryHigh As Label = TryCast(row.FindControl("lblTotalRatioYearlyVeryHigh"), Label)
                Dim lblTotalRatioYearlyVeryHighColor As Label = TryCast(row.FindControl("lblTotalRatioYearlyVeryHighColor"), Label)

                Dim DailyLowLabel As Label = TryCast(row.FindControl("DailyLowLabel"), Label)
                Dim DailyMediumLabel As Label = TryCast(row.FindControl("DailyMediumLabel"), Label)
                Dim DailyHighLabel As Label = TryCast(row.FindControl("DailyHighLabel"), Label)
                Dim DailyVeryHighLabel As Label = TryCast(row.FindControl("DailyVeryHighLabel"), Label)

                Dim WeeklyLowLabel As Label = TryCast(row.FindControl("WeeklyLowLabel"), Label)
                Dim WeeklyMediumLabel As Label = TryCast(row.FindControl("WeeklyMediumLabel"), Label)
                Dim WeeklyHighLabel As Label = TryCast(row.FindControl("WeeklyHighLabel"), Label)
                Dim WeeklyVeryHighLabel As Label = TryCast(row.FindControl("WeeklyVeryHighLabel"), Label)

                Dim MonthlyLowLabel As Label = TryCast(row.FindControl("MonthlyLowLabel"), Label)
                Dim MonthlyMediumLabel As Label = TryCast(row.FindControl("MonthlyMediumLabel"), Label)
                Dim MonthlyHighLabel As Label = TryCast(row.FindControl("MonthlyHighLabel"), Label)
                Dim MonthlyVeryHighLabel As Label = TryCast(row.FindControl("MonthlyVeryHighLabel"), Label)

                Dim YearlyLowLabel As Label = TryCast(row.FindControl("YearlyLowLabel"), Label)
                Dim YearlyMediumLabel As Label = TryCast(row.FindControl("YearlyMediumLabel"), Label)
                Dim YearlyHighLabel As Label = TryCast(row.FindControl("YearlyHighLabel"), Label)
                Dim YearlyVeryHighLabel As Label = TryCast(row.FindControl("YearlyVeryHighLabel"), Label)

                hiddenRcNo.Value = hdnRcNo.Text


                If lblDeviceType.Text.ToUpper = "DEFAULT" Then
                    ddlAccountID.Visible = False
                    ddlDeviceType.Visible = False
                    lblAccountID.Visible = False
                    lblDeviceType.Visible = False
                    lblHeading.Text = "Default Device & Site Event Threshold"
                Else
                    ddlAccountID.Visible = True
                    ddlDeviceType.Visible = True
                    lblAccountID.Visible = True
                    lblDeviceType.Visible = True
                    lblHeading.Text = "Device & Site Event Threshold"

                    If lblAccountID.Text = Nothing Then
                        ddlAccountID.SelectedIndex = 0
                    Else
                        ddlAccountID.SelectedValue = lblAccountID.Text
                    End If

                    If lblDeviceType.Text = Nothing Then
                        ddlDeviceType.SelectedIndex = 0
                    Else
                        ddlDeviceType.SelectedValue = lblDeviceType.Text
                    End If
                End If




                'Dim destinationPath As String = Server.MapPath("~/Images_Threshold")
                'Dim destinationFile As String
                Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

                'daily start
                txtDailyLow.Text = lblDailyLow.Text
                txtDailyLowColor.Text = lblDailyLowColor.Text

                txtTotalDailyLow.Text = lblTotalDailyLow.Text
                txtTotalDailyLowColor.Text = lblTotalDailyLowColor.Text

                txtTotalRatioDailyLow.Text = lblTotalRatioDailyLow.Text
                txtTotalRatioDailyLowColor.Text = lblTotalRatioDailyLowColor.Text

                txtDailyMedium.Text = lblDailyMedium.Text
                txtDailyMediumColor.Text = lblDailyMediumColor.Text

                txtTotalDailyMedium.Text = lblTotalDailyMedium.Text
                txtTotalDailyMediumColor.Text = lblTotalDailyMediumColor.Text

                txtTotalRatioDailyMedium.Text = lblTotalRatioDailyMedium.Text
                txtTotalRatioDailyMediumColor.Text = lblTotalRatioDailyMediumColor.Text

                txtDailyHigh.Text = lblDailyHigh.Text
                txtDailyHighColor.Text = lblDailyHighColor.Text

                txtTotalDailyHigh.Text = lblTotalDailyHigh.Text
                txtTotalDailyHighColor.Text = lblTotalDailyHighColor.Text

                txtTotalRatioDailyHigh.Text = lblTotalRatioDailyHigh.Text
                txtTotalRatioDailyHighColor.Text = lblTotalRatioDailyHighColor.Text

                txtDailyVeryHigh.Text = lblDailyVeryHigh.Text
                txtDailyVeryHighColor.Text = lblDailyVeryHighColor.Text

                txtTotalDailyVeryHigh.Text = lblTotalDailyVeryHigh.Text
                txtTotalDailyVeryHighColor.Text = lblTotalDailyVeryHighColor.Text

                txtTotalRatioDailyVeryHigh.Text = lblTotalRatioDailyVeryHigh.Text
                txtTotalRatioDailyVeryHighColor.Text = lblTotalRatioDailyVeryHighColor.Text

                txtDailyLowLabel.Text = DailyLowLabel.Text
                txtDailyMediumLabel.Text = DailyMediumLabel.Text
                txtDailyHighLabel.Text = DailyHighLabel.Text
                txtDailyVeryHighLabel.Text = DailyVeryHighLabel.Text

                hdnisLogoClearDailylow.Value = 0
                hdnisLogoClearDailyMedium.Value = 0
                hdnisLogoClearDailyHigh.Value = 0
                hdnisLogoClearDailyVeryHigh.Value = 0

                hdnisLogoClearTotalDailylow.Value = 0
                hdnisLogoClearTotalDailyMedium.Value = 0
                hdnisLogoClearTotalDailyHigh.Value = 0
                hdnisLogoClearTotalDailyVeryHigh.Value = 0

                hdnisLogoClearTotalRatioDailylow.Value = 0
                hdnisLogoClearTotalRatioDailyMedium.Value = 0
                hdnisLogoClearTotalRatioDailyHigh.Value = 0
                hdnisLogoClearTotalRatioDailyVeryHigh.Value = 0

                Dim DailyLowFileName As String = ""
                Dim DailyMediumFileName As String = ""
                Dim DailyHighFileName As String = ""
                Dim DailyVeryHighFileName As String = ""


                Using con As New MySqlConnection(constr)
                    Using cmd As New MySqlCommand()
                        cmd.CommandType = CommandType.Text
                        Dim insQuery As String = "SELECT * FROM tbldeviceeventthreshold WHERE RcNo =  " & hdnRcNo.Text & ""

                        cmd.CommandText = insQuery
                        cmd.Connection = con
                        con.Open()
                        Dim reader As MySqlDataReader = cmd.ExecuteReader()
                        Do While reader.Read()

                            If Not IsDBNull(reader("DailyLowImage")) Then
                                DailyLowFileName = CType(reader("DailyLowImage"), String)
                            Else
                                DailyLowFileName = ""
                            End If

                            If Not IsDBNull(reader("DailyMediumImage")) Then
                                DailyMediumFileName = CType(reader("DailyMediumImage"), String)
                            Else
                                DailyMediumFileName = ""
                            End If

                            If Not IsDBNull(reader("DailyHighImage")) Then
                                DailyHighFileName = CType(reader("DailyHighImage"), String)
                            Else
                                DailyHighFileName = ""
                            End If

                            If Not IsDBNull(reader("DailyVeryHighImage")) Then
                                DailyVeryHighFileName = CType(reader("DailyVeryHighImage"), String)
                            Else
                                DailyVeryHighFileName = ""
                            End If

                        Loop
                        con.Close()
                    End Using
                End Using
                'daily end

                'Weekly start
                txtWeeklyLow.Text = lblWeeklyLow.Text
                txtWeeklyLowColor.Text = lblWeeklyLowColor.Text

                txtTotalWeeklyLow.Text = lblTotalWeeklyLow.Text
                txtTotalWeeklyLowColor.Text = lblTotalWeeklyLowColor.Text

                txtTotalRatioWeeklyLow.Text = lblTotalRatioWeeklyLow.Text
                txtTotalRatioWeeklyLowColor.Text = lblTotalRatioWeeklyLowColor.Text

                txtWeeklyMedium.Text = lblWeeklyMedium.Text
                txtWeeklyMediumColor.Text = lblWeeklyMediumColor.Text

                txtTotalWeeklyMedium.Text = lblTotalWeeklyMedium.Text
                txtTotalWeeklyMediumColor.Text = lblTotalWeeklyMediumColor.Text

                txtTotalRatioWeeklyMedium.Text = lblTotalRatioWeeklyMedium.Text
                txtTotalRatioWeeklyMediumColor.Text = lblTotalRatioWeeklyMediumColor.Text

                txtWeeklyHigh.Text = lblWeeklyHigh.Text
                txtWeeklyHighColor.Text = lblWeeklyHighColor.Text

                txtTotalWeeklyHigh.Text = lblTotalWeeklyHigh.Text
                txtTotalWeeklyHighColor.Text = lblTotalWeeklyHighColor.Text

                txtTotalRatioWeeklyHigh.Text = lblTotalRatioWeeklyHigh.Text
                txtTotalRatioWeeklyHighColor.Text = lblTotalRatioWeeklyHighColor.Text

                txtWeeklyVeryHigh.Text = lblWeeklyVeryHigh.Text
                txtWeeklyVeryHighColor.Text = lblWeeklyVeryHighColor.Text

                txtTotalWeeklyVeryHigh.Text = lblTotalWeeklyVeryHigh.Text
                txtTotalWeeklyVeryHighColor.Text = lblTotalWeeklyVeryHighColor.Text

                txtTotalRatioWeeklyVeryHigh.Text = lblTotalRatioWeeklyVeryHigh.Text
                txtTotalRatioWeeklyVeryHighColor.Text = lblTotalRatioWeeklyVeryHighColor.Text

                txtWeeklyLowLabel.Text = WeeklyLowLabel.Text
                txtWeeklyMediumLabel.Text = WeeklyMediumLabel.Text
                txtWeeklyHighLabel.Text = WeeklyHighLabel.Text
                txtWeeklyVeryHighLabel.Text = WeeklyVeryHighLabel.Text

                hdnisLogoClearWeeklylow.Value = 0
                hdnisLogoClearWeeklyMedium.Value = 0
                hdnisLogoClearWeeklyHigh.Value = 0
                hdnisLogoClearWeeklyVeryHigh.Value = 0

                hdnisLogoClearTotalWeeklylow.Value = 0
                hdnisLogoClearTotalWeeklyMedium.Value = 0
                hdnisLogoClearTotalWeeklyHigh.Value = 0
                hdnisLogoClearTotalWeeklyVeryHigh.Value = 0

                hdnisLogoClearTotalRatioWeeklylow.Value = 0
                hdnisLogoClearTotalRatioWeeklyMedium.Value = 0
                hdnisLogoClearTotalRatioWeeklyHigh.Value = 0
                hdnisLogoClearTotalRatioWeeklyVeryHigh.Value = 0

                Dim WeeklyLowFileName As String = ""
                Dim WeeklyMediumFileName As String = ""
                Dim WeeklyHighFileName As String = ""
                Dim WeeklyVeryHighFileName As String = ""

                Using con As New MySqlConnection(constr)
                    Using cmd As New MySqlCommand()
                        cmd.CommandType = CommandType.Text
                        Dim insQuery As String = "SELECT * FROM tbldeviceeventthreshold WHERE RcNo =  " & hdnRcNo.Text & ""

                        cmd.CommandText = insQuery
                        cmd.Connection = con
                        con.Open()
                        Dim reader As MySqlDataReader = cmd.ExecuteReader()
                        Do While reader.Read()

                            If Not IsDBNull(reader("WeeklyLowImage")) Then
                                WeeklyLowFileName = CType(reader("WeeklyLowImage"), String)
                            Else
                                WeeklyLowFileName = ""
                            End If

                            If Not IsDBNull(reader("WeeklyMediumImage")) Then
                                WeeklyMediumFileName = CType(reader("WeeklyMediumImage"), String)
                            Else
                                WeeklyMediumFileName = ""
                            End If

                            If Not IsDBNull(reader("WeeklyHighImage")) Then
                                WeeklyHighFileName = CType(reader("WeeklyHighImage"), String)
                            Else
                                WeeklyHighFileName = ""
                            End If

                            If Not IsDBNull(reader("WeeklyVeryHighImage")) Then
                                WeeklyVeryHighFileName = CType(reader("WeeklyVeryHighImage"), String)
                            Else
                                WeeklyVeryHighFileName = ""
                            End If

                        Loop
                        con.Close()
                    End Using
                End Using

                'Weekly end

                'Monthly start
                txtMonthlyLow.Text = lblMonthlyLow.Text
                txtMonthlyLowColor.Text = lblMonthlyLowColor.Text

                txtTotalMonthlyLow.Text = lblTotalMonthlyLow.Text
                txtTotalMonthlyLowColor.Text = lblTotalMonthlyLowColor.Text

                txtTotalRatioMonthlyLow.Text = lblTotalRatioMonthlyLow.Text
                txtTotalRatioMonthlyLowColor.Text = lblTotalRatioMonthlyLowColor.Text

                txtMonthlyMedium.Text = lblMonthlyMedium.Text
                txtMonthlyMediumColor.Text = lblMonthlyMediumColor.Text

                txtTotalMonthlyMedium.Text = lblTotalMonthlyMedium.Text
                txtTotalMonthlyMediumColor.Text = lblTotalMonthlyMediumColor.Text

                txtTotalRatioMonthlyMedium.Text = lblTotalRatioMonthlyMedium.Text
                txtTotalRatioMonthlyMediumColor.Text = lblTotalRatioMonthlyMediumColor.Text

                txtMonthlyHigh.Text = lblMonthlyHigh.Text
                txtMonthlyHighColor.Text = lblMonthlyHighColor.Text

                txtTotalMonthlyHigh.Text = lblTotalMonthlyHigh.Text
                txtTotalMonthlyHighColor.Text = lblTotalMonthlyHighColor.Text

                txtTotalRatioMonthlyHigh.Text = lblTotalRatioMonthlyHigh.Text
                txtTotalRatioMonthlyHighColor.Text = lblTotalRatioMonthlyHighColor.Text

                txtMonthlyVeryHigh.Text = lblMonthlyVeryHigh.Text
                txtMonthlyVeryHighColor.Text = lblMonthlyVeryHighColor.Text

                txtTotalMonthlyVeryHigh.Text = lblTotalMonthlyVeryHigh.Text
                txtTotalMonthlyVeryHighColor.Text = lblTotalMonthlyVeryHighColor.Text

                txtTotalRatioMonthlyVeryHigh.Text = lblTotalRatioMonthlyVeryHigh.Text
                txtTotalRatioMonthlyVeryHighColor.Text = lblTotalRatioMonthlyVeryHighColor.Text

                txtMonthlyLowLabel.Text = MonthlyLowLabel.Text
                txtMonthlyMediumLabel.Text = MonthlyMediumLabel.Text
                txtMonthlyHighLabel.Text = MonthlyHighLabel.Text
                txtMonthlyVeryHighLabel.Text = MonthlyVeryHighLabel.Text

                hdnisLogoClearMonthlylow.Value = 0
                hdnisLogoClearMonthlyMedium.Value = 0
                hdnisLogoClearMonthlyHigh.Value = 0
                hdnisLogoClearMonthlyVeryHigh.Value = 0

                hdnisLogoClearTotalMonthlylow.Value = 0
                hdnisLogoClearTotalMonthlyMedium.Value = 0
                hdnisLogoClearTotalMonthlyHigh.Value = 0
                hdnisLogoClearTotalMonthlyVeryHigh.Value = 0

                hdnisLogoClearTotalRatioMonthlylow.Value = 0
                hdnisLogoClearTotalRatioMonthlyMedium.Value = 0
                hdnisLogoClearTotalRatioMonthlyHigh.Value = 0
                hdnisLogoClearTotalRatioMonthlyVeryHigh.Value = 0

                Dim MonthlyLowFileName As String = ""
                Dim MonthlyMediumFileName As String = ""
                Dim MonthlyHighFileName As String = ""
                Dim MonthlyVeryHighFileName As String = ""

                Using con As New MySqlConnection(constr)
                    Using cmd As New MySqlCommand()
                        cmd.CommandType = CommandType.Text
                        Dim insQuery As String = "SELECT * FROM tbldeviceeventthreshold WHERE RcNo =  " & hdnRcNo.Text & ""

                        cmd.CommandText = insQuery
                        cmd.Connection = con
                        con.Open()
                        Dim reader As MySqlDataReader = cmd.ExecuteReader()
                        Do While reader.Read()

                            If Not IsDBNull(reader("MonthlyLowImage")) Then
                                MonthlyLowFileName = CType(reader("MonthlyLowImage"), String)
                            Else
                                MonthlyLowFileName = ""
                            End If

                            If Not IsDBNull(reader("MonthlyMediumImage")) Then
                                MonthlyMediumFileName = CType(reader("MonthlyMediumImage"), String)
                            Else
                                MonthlyMediumFileName = ""
                            End If

                            If Not IsDBNull(reader("MonthlyHighImage")) Then
                                MonthlyHighFileName = CType(reader("MonthlyHighImage"), String)
                            Else
                                MonthlyHighFileName = ""
                            End If

                            If Not IsDBNull(reader("MonthlyVeryHighImage")) Then
                                MonthlyVeryHighFileName = CType(reader("MonthlyVeryHighImage"), String)
                            Else
                                MonthlyVeryHighFileName = ""
                            End If

                        Loop
                        con.Close()
                    End Using
                End Using

                'Monthly end

                'Yearly start
                txtYearlyLow.Text = lblYearlyLow.Text
                txtYearlyLowColor.Text = lblYearlyLowColor.Text

                txtTotalYearlyLow.Text = lblTotalYearlyLow.Text
                txtTotalYearlyLowColor.Text = lblTotalYearlyLowColor.Text

                txtTotalRatioYearlyLow.Text = lblTotalRatioYearlyLow.Text
                txtTotalRatioYearlyLowColor.Text = lblTotalRatioYearlyLowColor.Text

                txtYearlyMedium.Text = lblYearlyMedium.Text
                txtYearlyMediumColor.Text = lblYearlyMediumColor.Text

                txtTotalYearlyMedium.Text = lblTotalYearlyMedium.Text
                txtTotalYearlyMediumColor.Text = lblTotalYearlyMediumColor.Text

                txtTotalRatioYearlyMedium.Text = lblTotalRatioYearlyMedium.Text
                txtTotalRatioYearlyMediumColor.Text = lblTotalRatioYearlyMediumColor.Text

                txtYearlyHigh.Text = lblYearlyHigh.Text
                txtYearlyHighColor.Text = lblYearlyHighColor.Text

                txtTotalYearlyHigh.Text = lblTotalYearlyHigh.Text
                txtTotalYearlyHighColor.Text = lblTotalYearlyHighColor.Text

                txtTotalRatioYearlyHigh.Text = lblTotalRatioYearlyHigh.Text
                txtTotalRatioYearlyHighColor.Text = lblTotalRatioYearlyHighColor.Text

                txtYearlyVeryHigh.Text = lblYearlyVeryHigh.Text
                txtYearlyVeryHighColor.Text = lblYearlyVeryHighColor.Text

                txtTotalYearlyVeryHigh.Text = lblTotalYearlyVeryHigh.Text
                txtTotalYearlyVeryHighColor.Text = lblTotalYearlyVeryHighColor.Text

                txtTotalRatioYearlyVeryHigh.Text = lblTotalRatioYearlyVeryHigh.Text
                txtTotalRatioYearlyVeryHighColor.Text = lblTotalRatioYearlyVeryHighColor.Text

                txtYearlyLowLabel.Text = YearlyLowLabel.Text
                txtYearlyMediumLabel.Text = YearlyMediumLabel.Text
                txtYearlyHighLabel.Text = YearlyHighLabel.Text
                txtYearlyVeryHighLabel.Text = YearlyVeryHighLabel.Text

                hdnisLogoClearYearlylow.Value = 0
                hdnisLogoClearYearlyMedium.Value = 0
                hdnisLogoClearYearlyHigh.Value = 0
                hdnisLogoClearYearlyVeryHigh.Value = 0

                hdnisLogoClearTotalYearlylow.Value = 0
                hdnisLogoClearTotalYearlyMedium.Value = 0
                hdnisLogoClearTotalYearlyHigh.Value = 0
                hdnisLogoClearTotalYearlyVeryHigh.Value = 0

                hdnisLogoClearTotalRatioYearlylow.Value = 0
                hdnisLogoClearTotalRatioYearlyMedium.Value = 0
                hdnisLogoClearTotalRatioYearlyHigh.Value = 0
                hdnisLogoClearTotalRatioYearlyVeryHigh.Value = 0


                Dim YearlyLowFileName As String = ""
                Dim YearlyMediumFileName As String = ""
                Dim YearlyHighFileName As String = ""
                Dim YearlyVeryHighFileName As String = ""

               'Yearly end

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showpopup();", True)

            End If
        End Using


    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim RcNo As Integer = Convert.ToInt32((CType(sender, LinkButton)).CommandArgument)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Dim query As String = "DELETE FROM tbldeviceeventthreshold WHERE RcNo = @RcNo"
            Using cmd As New MySqlCommand(query)
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@RcNo", RcNo)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
        loadGrid()
    End Sub

    Protected Sub lnkDuplicate_Click(sender As Object, e As EventArgs)
        Dim RcNo As Integer = Convert.ToInt32((CType(sender, LinkButton)).CommandArgument)
        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Using con As New MySqlConnection(constr)
            Dim query As String = "INSERT INTO tbldeviceeventthreshold(AccountID,DeviceType,DailyLow,DailyLowColor,DailyMedium,DailyMediumColor,DailyHigh,DailyHighColor,DailyVeryHigh,DailyVeryHighColor,WeeklyLow,WeeklyLowColor,WeeklyMedium," & _
                                    "WeeklyMediumColor, WeeklyHigh, WeeklyHighColor, WeeklyVeryHigh, WeeklyVeryHighColor, MonthlyLow, MonthlyLowColor, MonthlyMedium, MonthlyMediumColor, MonthlyHigh, MonthlyHighColor, MonthlyVeryHigh, MonthlyVeryHighColor," & _
                                    "YearlyLow,YearlyLowColor,YearlyMedium,YearlyMediumColor,YearlyHigh,YearlyHighColor,YearlyVeryHigh,YearlyVeryHighColor) " & _
                                    " SELECT AccountID,DeviceType,DailyLow,DailyLowColor,DailyMedium,DailyMediumColor,DailyHigh,DailyHighColor,DailyVeryHigh,DailyVeryHighColor,WeeklyLow,WeeklyLowColor,WeeklyMedium," & _
                                    "WeeklyMediumColor, WeeklyHigh, WeeklyHighColor, WeeklyVeryHigh, WeeklyVeryHighColor, MonthlyLow, MonthlyLowColor, MonthlyMedium, MonthlyMediumColor, MonthlyHigh, MonthlyHighColor, MonthlyVeryHigh, MonthlyVeryHighColor," & _
                                    "YearlyLow,YearlyLowColor,YearlyMedium,YearlyMediumColor,YearlyHigh,YearlyHighColor,YearlyVeryHigh,YearlyVeryHighColor " & _
                                    " FROM tbldeviceeventthreshold WHERE RcNo = @RcNo"
            Using cmd As New MySqlCommand(query)
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@RcNo", RcNo)
                con.Open()
                cmd.ExecuteNonQuery()
                'To Get Last inserted id
                cmd.Parameters.AddWithValue("newId", cmd.LastInsertedId)
                hiddenRcNo.Value = Convert.ToInt32(cmd.Parameters("@newId").Value)

                con.Close()
            End Using
        End Using


        'Dim fileName As String
        'Dim destinationFile As String
        'Dim sourceFile As String
        'Dim destinationPath As String = Server.MapPath("~/Images_Threshold")

        'Using con As New MySqlConnection(constr)
        '    Using cmd As New MySqlCommand()
        '        cmd.CommandType = CommandType.Text
        '        Dim insQuery As String = "SELECT * FROM tbldeviceeventthreshold WHERE RcNo =  " & RcNo & ""

        '        cmd.CommandText = insQuery
        '        cmd.Connection = con
        '        con.Open()
        '        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        '        Do While reader.Read()

        '            ''Daily Start
        '            'If Not IsDBNull(reader("DailyLowImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("DailyLowImage"), String))

        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "DailyLow" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName
        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conDailyLowImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET DailyLowImage = @DailyLowImage WHERE RcNo = @RcNo"
        '            '        Using cmdDailyLowImage As New MySqlCommand(query)
        '            '            cmdDailyLowImage.Connection = conDailyLowImage
        '            '            cmdDailyLowImage.Parameters.AddWithValue("@DailyLowImage", fileName)
        '            '            cmdDailyLowImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conDailyLowImage.Open()
        '            '            cmdDailyLowImage.ExecuteNonQuery()
        '            '            conDailyLowImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("DailyMediumImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("DailyMediumImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "DailyMedium" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName
        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conDailyMediumImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET DailyMediumImage = @DailyMediumImage WHERE RcNo = @RcNo"
        '            '        Using cmdDailyMediumImage As New MySqlCommand(query)
        '            '            cmdDailyMediumImage.Connection = conDailyMediumImage
        '            '            cmdDailyMediumImage.Parameters.AddWithValue("@DailyMediumImage", fileName)
        '            '            cmdDailyMediumImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conDailyMediumImage.Open()
        '            '            cmdDailyMediumImage.ExecuteNonQuery()
        '            '            conDailyMediumImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("DailyHighImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("DailyHighImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "DailyHigh" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conDailyHighImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET DailyHighImage = @DailyHighImage WHERE RcNo = @RcNo"
        '            '        Using cmdDailyHighImage As New MySqlCommand(query)
        '            '            cmdDailyHighImage.Connection = conDailyHighImage
        '            '            cmdDailyHighImage.Parameters.AddWithValue("@DailyHighImage", fileName)
        '            '            cmdDailyHighImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conDailyHighImage.Open()
        '            '            cmdDailyHighImage.ExecuteNonQuery()
        '            '            conDailyHighImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("DailyVeryHighImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("DailyVeryHighImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "DailyVeryHigh" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conDailyVeryHighImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET DailyVeryHighImage = @DailyVeryHighImage WHERE RcNo = @RcNo"
        '            '        Using cmdDailyVeryHighImage As New MySqlCommand(query)
        '            '            cmdDailyVeryHighImage.Connection = conDailyVeryHighImage
        '            '            cmdDailyVeryHighImage.Parameters.AddWithValue("@DailyVeryHighImage", fileName)
        '            '            cmdDailyVeryHighImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conDailyVeryHighImage.Open()
        '            '            cmdDailyVeryHighImage.ExecuteNonQuery()
        '            '            conDailyVeryHighImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If


        '            ''Daily End

        '            ''Weekly Start
        '            'If Not IsDBNull(reader("WeeklyLowImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("WeeklyLowImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "WeeklyLow" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conWeeklyLowImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET WeeklyLowImage = @WeeklyLowImage WHERE RcNo = @RcNo"
        '            '        Using cmdWeeklyLowImage As New MySqlCommand(query)
        '            '            cmdWeeklyLowImage.Connection = conWeeklyLowImage
        '            '            cmdWeeklyLowImage.Parameters.AddWithValue("@WeeklyLowImage", fileName)
        '            '            cmdWeeklyLowImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conWeeklyLowImage.Open()
        '            '            cmdWeeklyLowImage.ExecuteNonQuery()
        '            '            conWeeklyLowImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("WeeklyMediumImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("WeeklyMediumImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "WeeklyMedium" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conWeeklyMediumImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET WeeklyMediumImage = @WeeklyMediumImage WHERE RcNo = @RcNo"
        '            '        Using cmdWeeklyMediumImage As New MySqlCommand(query)
        '            '            cmdWeeklyMediumImage.Connection = conWeeklyMediumImage
        '            '            cmdWeeklyMediumImage.Parameters.AddWithValue("@WeeklyMediumImage", fileName)
        '            '            cmdWeeklyMediumImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conWeeklyMediumImage.Open()
        '            '            cmdWeeklyMediumImage.ExecuteNonQuery()
        '            '            conWeeklyMediumImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("WeeklyHighImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("WeeklyHighImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "WeeklyHigh" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conWeeklyHighImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET WeeklyHighImage = @WeeklyHighImage WHERE RcNo = @RcNo"
        '            '        Using cmdWeeklyHighImage As New MySqlCommand(query)
        '            '            cmdWeeklyHighImage.Connection = conWeeklyHighImage
        '            '            cmdWeeklyHighImage.Parameters.AddWithValue("@WeeklyHighImage", fileName)
        '            '            cmdWeeklyHighImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conWeeklyHighImage.Open()
        '            '            cmdWeeklyHighImage.ExecuteNonQuery()
        '            '            conWeeklyHighImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("WeeklyVeryHighImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("WeeklyVeryHighImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "WeeklyVeryHigh" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conWeeklyVeryHighImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET WeeklyVeryHighImage = @WeeklyVeryHighImage WHERE RcNo = @RcNo"
        '            '        Using cmdWeeklyVeryHighImage As New MySqlCommand(query)
        '            '            cmdWeeklyVeryHighImage.Connection = conWeeklyVeryHighImage
        '            '            cmdWeeklyVeryHighImage.Parameters.AddWithValue("@WeeklyVeryHighImage", fileName)
        '            '            cmdWeeklyVeryHighImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conWeeklyVeryHighImage.Open()
        '            '            cmdWeeklyVeryHighImage.ExecuteNonQuery()
        '            '            conWeeklyVeryHighImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If


        '            ''Weekly End
        '            ''Monthly Start
        '            'If Not IsDBNull(reader("MonthlyLowImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("MonthlyLowImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "MonthlyLow" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conMonthlyLowImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET MonthlyLowImage = @MonthlyLowImage WHERE RcNo = @RcNo"
        '            '        Using cmdMonthlyLowImage As New MySqlCommand(query)
        '            '            cmdMonthlyLowImage.Connection = conMonthlyLowImage
        '            '            cmdMonthlyLowImage.Parameters.AddWithValue("@MonthlyLowImage", fileName)
        '            '            cmdMonthlyLowImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conMonthlyLowImage.Open()
        '            '            cmdMonthlyLowImage.ExecuteNonQuery()
        '            '            conMonthlyLowImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("MonthlyMediumImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("MonthlyMediumImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "MonthlyMedium" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conMonthlyMediumImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET MonthlyMediumImage = @MonthlyMediumImage WHERE RcNo = @RcNo"
        '            '        Using cmdMonthlyMediumImage As New MySqlCommand(query)
        '            '            cmdMonthlyMediumImage.Connection = conMonthlyMediumImage
        '            '            cmdMonthlyMediumImage.Parameters.AddWithValue("@MonthlyMediumImage", fileName)
        '            '            cmdMonthlyMediumImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conMonthlyMediumImage.Open()
        '            '            cmdMonthlyMediumImage.ExecuteNonQuery()
        '            '            conMonthlyMediumImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("MonthlyHighImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("MonthlyHighImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "MonthlyHigh" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conMonthlyHighImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET MonthlyHighImage = @MonthlyHighImage WHERE RcNo = @RcNo"
        '            '        Using cmdMonthlyHighImage As New MySqlCommand(query)
        '            '            cmdMonthlyHighImage.Connection = conMonthlyHighImage
        '            '            cmdMonthlyHighImage.Parameters.AddWithValue("@MonthlyHighImage", fileName)
        '            '            cmdMonthlyHighImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conMonthlyHighImage.Open()
        '            '            cmdMonthlyHighImage.ExecuteNonQuery()
        '            '            conMonthlyHighImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("MonthlyVeryHighImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("MonthlyVeryHighImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "MonthlyVeryHigh" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conMonthlyVeryHighImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET MonthlyVeryHighImage = @MonthlyVeryHighImage WHERE RcNo = @RcNo"
        '            '        Using cmdMonthlyVeryHighImage As New MySqlCommand(query)
        '            '            cmdMonthlyVeryHighImage.Connection = conMonthlyVeryHighImage
        '            '            cmdMonthlyVeryHighImage.Parameters.AddWithValue("@MonthlyVeryHighImage", fileName)
        '            '            cmdMonthlyVeryHighImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conMonthlyVeryHighImage.Open()
        '            '            cmdMonthlyVeryHighImage.ExecuteNonQuery()
        '            '            conMonthlyVeryHighImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If


        '            ''Monthly End

        '            ''Yearly Start
        '            'If Not IsDBNull(reader("YearlyLowImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("YearlyLowImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "YearlyLow" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conYearlyLowImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET YearlyLowImage = @YearlyLowImage WHERE RcNo = @RcNo"
        '            '        Using cmdYearlyLowImage As New MySqlCommand(query)
        '            '            cmdYearlyLowImage.Connection = conYearlyLowImage
        '            '            cmdYearlyLowImage.Parameters.AddWithValue("@YearlyLowImage", fileName)
        '            '            cmdYearlyLowImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conYearlyLowImage.Open()
        '            '            cmdYearlyLowImage.ExecuteNonQuery()
        '            '            conYearlyLowImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("YearlyMediumImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("YearlyMediumImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "YearlyMedium" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conYearlyMediumImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET YearlyMediumImage = @YearlyMediumImage WHERE RcNo = @RcNo"
        '            '        Using cmdYearlyMediumImage As New MySqlCommand(query)
        '            '            cmdYearlyMediumImage.Connection = conYearlyMediumImage
        '            '            cmdYearlyMediumImage.Parameters.AddWithValue("@YearlyMediumImage", fileName)
        '            '            cmdYearlyMediumImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conYearlyMediumImage.Open()
        '            '            cmdYearlyMediumImage.ExecuteNonQuery()
        '            '            conYearlyMediumImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("YearlyHighImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("YearlyHighImage"), String))


        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "YearlyHigh" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conYearlyHighImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET YearlyHighImage = @YearlyHighImage WHERE RcNo = @RcNo"
        '            '        Using cmdYearlyHighImage As New MySqlCommand(query)
        '            '            cmdYearlyHighImage.Connection = conYearlyHighImage
        '            '            cmdYearlyHighImage.Parameters.AddWithValue("@YearlyHighImage", fileName)
        '            '            cmdYearlyHighImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conYearlyHighImage.Open()
        '            '            cmdYearlyHighImage.ExecuteNonQuery()
        '            '            conYearlyHighImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If
        '            'If Not IsDBNull(reader("YearlyVeryHighImage")) Then
        '            '    sourceFile = destinationPath & "/" & Path.GetFileName(CType(reader("YearlyVeryHighImage"), String))



        '            '    If System.IO.File.Exists(sourceFile) Then
        '            '        fileName = "YearlyVeryHigh" & hiddenRcNo.Value & Path.GetExtension(sourceFile)
        '            '        destinationFile = destinationPath & "/" & fileName

        '            '        If Not System.IO.File.Exists(destinationFile) Then
        '            '            System.IO.File.Copy(sourceFile, destinationFile)
        '            '        End If
        '            '    End If

        '            '    Using conYearlyVeryHighImage As New MySqlConnection(constr)
        '            '        Dim query As String = "UPDATE tbldeviceeventthreshold SET YearlyVeryHighImage = @YearlyVeryHighImage WHERE RcNo = @RcNo"
        '            '        Using cmdYearlyVeryHighImage As New MySqlCommand(query)
        '            '            cmdYearlyVeryHighImage.Connection = conYearlyVeryHighImage
        '            '            cmdYearlyVeryHighImage.Parameters.AddWithValue("@YearlyVeryHighImage", fileName)
        '            '            cmdYearlyVeryHighImage.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
        '            '            conYearlyVeryHighImage.Open()
        '            '            cmdYearlyVeryHighImage.ExecuteNonQuery()
        '            '            conYearlyVeryHighImage.Close()
        '            '        End Using
        '            '    End Using
        '            'End If


        '            'Yearly End
        '        Loop
        '        con.Close()
        '    End Using
        'End Using



        loadGrid()
    End Sub

    Protected Sub btnSaveDeviceEventThreshold_Click(sender As Object, e As EventArgs)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        If hiddenRcNo.Value = 0 Then
            'Add new Device Event Threshold
            Using con As New MySqlConnection(constr)
                Dim query As String = "INSERT INTO tbldeviceeventthreshold(AccountID,DeviceType,DailyLow,DailyLowColor,DailyMedium,DailyMediumColor,DailyHigh,DailyHighColor,DailyVeryHigh,DailyVeryHighColor,WeeklyLow,WeeklyLowColor,WeeklyMedium," & _
                                        "WeeklyMediumColor, WeeklyHigh, WeeklyHighColor, WeeklyVeryHigh, WeeklyVeryHighColor, MonthlyLow, MonthlyLowColor, MonthlyMedium, MonthlyMediumColor, MonthlyHigh, MonthlyHighColor, MonthlyVeryHigh, MonthlyVeryHighColor," & _
                                        "YearlyLow,YearlyLowColor,YearlyMedium,YearlyMediumColor,YearlyHigh,YearlyHighColor,YearlyVeryHigh,YearlyVeryHighColor," & _
                                        "TotalDailyLow,TotalDailyLowColor,TotalDailyMedium,TotalDailyMediumColor,TotalDailyHigh,TotalDailyHighColor,TotalDailyVeryHigh,TotalDailyVeryHighColor,TotalWeeklyLow,TotalWeeklyLowColor,TotalWeeklyMedium,TotalWeeklyMediumColor,TotalWeeklyHigh,TotalWeeklyHighColor,TotalWeeklyVeryHigh,TotalWeeklyVeryHighColor,TotalMonthlyLow,TotalMonthlyLowColor,TotalMonthlyMedium,TotalMonthlyMediumColor,TotalMonthlyHigh,TotalMonthlyHighColor,TotalMonthlyVeryHigh,TotalMonthlyVeryHighColor,TotalYearlyLow,TotalYearlyLowColor,TotalYearlyMedium,TotalYearlyMediumColor,TotalYearlyHigh,TotalYearlyHighColor,TotalYearlyVeryHigh,TotalYearlyVeryHighColor," & _
                                        "TotalRatioDailyLow,TotalRatioDailyLowColor,TotalRatioDailyMedium,TotalRatioDailyMediumColor,TotalRatioDailyHigh,TotalRatioDailyHighColor,TotalRatioDailyVeryHigh,TotalRatioDailyVeryHighColor,TotalRatioWeeklyLow,TotalRatioWeeklyLowColor,TotalRatioWeeklyMedium,TotalRatioWeeklyMediumColor,TotalRatioWeeklyHigh,TotalRatioWeeklyHighColor,TotalRatioWeeklyVeryHigh,TotalRatioWeeklyVeryHighColor,TotalRatioMonthlyLow,TotalRatioMonthlyLowColor,TotalRatioMonthlyMedium,TotalRatioMonthlyMediumColor,TotalRatioMonthlyHigh,TotalRatioMonthlyHighColor,TotalRatioMonthlyVeryHigh,TotalRatioMonthlyVeryHighColor,TotalRatioYearlyLow,TotalRatioYearlyLowColor,TotalRatioYearlyMedium,TotalRatioYearlyMediumColor,TotalRatioYearlyHigh,TotalRatioYearlyHighColor,TotalRatioYearlyVeryHigh,TotalRatioYearlyVeryHighColor," & _
                                        "DailyLowLabel,DailyMediumLabel,DailyHighLabel,DailyVeryHighLabel, WeeklyLowLabel, WeeklyMediumLabel, WeeklyHighLabel, WeeklyVeryHighLabel, MonthlyLowLabel, MonthlyMediumLabel, MonthlyHighLabel, MonthlyVeryHighLabel, YearlyLowLabel, YearlyMediumLabel, YearlyHighLabel, YearlyVeryHighLabel) " & _
                                        " VALUES(@AccountID,@DeviceType,@DailyLow,@DailyLowColor,@DailyMedium,@DailyMediumColor,@DailyHigh,@DailyHighColor,@DailyVeryHigh,@DailyVeryHighColor,@WeeklyLow,@WeeklyLowColor,@WeeklyMedium, " & _
                                        "@WeeklyMediumColor,@WeeklyHigh,@WeeklyHighColor,@WeeklyVeryHigh,@WeeklyVeryHighColor,@MonthlyLow,@MonthlyLowColor,@MonthlyMedium,@MonthlyMediumColor,@MonthlyHigh,@MonthlyHighColor,@MonthlyVeryHigh,@MonthlyVeryHighColor," & _
                                        "@YearlyLow,@YearlyLowColor,@YearlyMedium,@YearlyMediumColor,@YearlyHigh,@YearlyHighColor,@YearlyVeryHigh,@YearlyVeryHighColor," & _
                                        "@TotalDailyLow,@TotalDailyLowColor,@TotalDailyMedium,@TotalDailyMediumColor,@TotalDailyHigh,@TotalDailyHighColor,@TotalDailyVeryHigh,@TotalDailyVeryHighColor,@TotalWeeklyLow,@TotalWeeklyLowColor,@TotalWeeklyMedium,@TotalWeeklyMediumColor,@TotalWeeklyHigh,@TotalWeeklyHighColor,@TotalWeeklyVeryHigh,@TotalWeeklyVeryHighColor,@TotalMonthlyLow,@TotalMonthlyLowColor,@TotalMonthlyMedium,@TotalMonthlyMediumColor,@TotalMonthlyHigh,@TotalMonthlyHighColor,@TotalMonthlyVeryHigh,@TotalMonthlyVeryHighColor,@TotalYearlyLow,@TotalYearlyLowColor,@TotalYearlyMedium,@TotalYearlyMediumColor,@TotalYearlyHigh,@TotalYearlyHighColor,@TotalYearlyVeryHigh,@TotalYearlyVeryHighColor," & _
                                        "@TotalRatioDailyLow,@TotalRatioDailyLowColor,@TotalRatioDailyMedium,@TotalRatioDailyMediumColor,@TotalRatioDailyHigh,@TotalRatioDailyHighColor,@TotalRatioDailyVeryHigh,@TotalRatioDailyVeryHighColor,@TotalRatioWeeklyLow,@TotalRatioWeeklyLowColor,@TotalRatioWeeklyMedium,@TotalRatioWeeklyMediumColor,@TotalRatioWeeklyHigh,@TotalRatioWeeklyHighColor,@TotalRatioWeeklyVeryHigh,@TotalRatioWeeklyVeryHighColor,@TotalRatioMonthlyLow,@TotalRatioMonthlyLowColor,@TotalRatioMonthlyMedium,@TotalRatioMonthlyMediumColor,@TotalRatioMonthlyHigh,@TotalRatioMonthlyHighColor,@TotalRatioMonthlyVeryHigh,@TotalRatioMonthlyVeryHighColor,@TotalRatioYearlyLow,@TotalRatioYearlyLowColor,@TotalRatioYearlyMedium,@TotalRatioYearlyMediumColor,@TotalRatioYearlyHigh,@TotalRatioYearlyHighColor,@TotalRatioYearlyVeryHigh,@TotalRatioYearlyVeryHighColor," & _
                                        "@DailyLowLabel,@DailyMediumLabel,@DailyHighLabel,@DailyVeryHighLabel,@WeeklyLowLabel,@WeeklyMediumLabel,@WeeklyHighLabel,@WeeklyVeryHighLabel,@MonthlyLowLabel,@MonthlyMediumLabel,@MonthlyHighLabel,@MonthlyVeryHighLabel,@YearlyLowLabel,@YearlyMediumLabel,@YearlyHighLabel,@YearlyVeryHighLabel)"
                Using cmd As New MySqlCommand(query)
                    cmd.Connection = con
                    If ddlDeviceType.Visible = True Then
                        cmd.Parameters.AddWithValue("@AccountID", ddlAccountID.SelectedValue)
                        cmd.Parameters.AddWithValue("@DeviceType", ddlDeviceType.SelectedValue)
                    Else
                        cmd.Parameters.AddWithValue("@AccountID", Nothing)
                        cmd.Parameters.AddWithValue("@DeviceType", "DEFAULT")
                    End If

                    cmd.Parameters.AddWithValue("@DailyLow", txtDailyLow.Text)
                    cmd.Parameters.AddWithValue("@DailyLowColor", txtDailyLowColor.Text)

                    cmd.Parameters.AddWithValue("@TotalDailyLow", txtTotalDailyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyLowColor", txtTotalDailyLowColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioDailyLow", txtTotalRatioDailyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyLowColor", txtTotalRatioDailyLowColor.Text)

                    cmd.Parameters.AddWithValue("@DailyMedium", txtDailyMedium.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumColor", txtDailyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@TotalDailyMedium", txtTotalDailyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyMediumColor", txtTotalDailyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioDailyMedium", txtTotalRatioDailyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyMediumColor", txtTotalRatioDailyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@DailyHigh", txtDailyHigh.Text)
                    cmd.Parameters.AddWithValue("@DailyHighColor", txtDailyHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalDailyHigh", txtTotalDailyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyHighColor", txtTotalDailyHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioDailyHigh", txtTotalRatioDailyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyHighColor", txtTotalRatioDailyHighColor.Text)

                    cmd.Parameters.AddWithValue("@DailyVeryHigh", txtDailyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@DailyVeryHighColor", txtDailyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalDailyVeryHigh", txtTotalDailyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyVeryHighColor", txtTotalDailyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioDailyVeryHigh", txtTotalRatioDailyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyVeryHighColor", txtTotalRatioDailyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@WeeklyLow", txtWeeklyLow.Text)
                    cmd.Parameters.AddWithValue("@WeeklyLowColor", txtWeeklyLowColor.Text)

                    cmd.Parameters.AddWithValue("@TotalWeeklyLow", txtTotalWeeklyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyLowColor", txtTotalWeeklyLowColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyLow", txtTotalRatioWeeklyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyLowColor", txtTotalRatioWeeklyLowColor.Text)

                    cmd.Parameters.AddWithValue("@WeeklyMedium", txtWeeklyMedium.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumColor", txtWeeklyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@TotalWeeklyMedium", txtTotalWeeklyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyMediumColor", txtTotalWeeklyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyMedium", txtTotalRatioWeeklyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyMediumColor", txtTotalRatioWeeklyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@WeeklyHigh", txtWeeklyHigh.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighColor", txtWeeklyHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalWeeklyHigh", txtTotalWeeklyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyHighColor", txtTotalWeeklyHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyHigh", txtTotalRatioWeeklyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyHighColor", txtTotalRatioWeeklyHighColor.Text)

                    cmd.Parameters.AddWithValue("@WeeklyVeryHigh", txtWeeklyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@WeeklyVeryHighColor", txtWeeklyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalWeeklyVeryHigh", txtTotalWeeklyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyVeryHighColor", txtTotalWeeklyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyVeryHigh", txtTotalRatioWeeklyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyVeryHighColor", txtTotalRatioWeeklyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@MonthlyLow", txtMonthlyLow.Text)
                    cmd.Parameters.AddWithValue("@MonthlyLowColor", txtMonthlyLowColor.Text)

                    cmd.Parameters.AddWithValue("@TotalMonthlyLow", txtTotalMonthlyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyLowColor", txtTotalMonthlyLowColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyLow", txtTotalRatioMonthlyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyLowColor", txtTotalRatioMonthlyLowColor.Text)

                    cmd.Parameters.AddWithValue("@MonthlyMedium", txtMonthlyMedium.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumColor", txtMonthlyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@TotalMonthlyMedium", txtTotalMonthlyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyMediumColor", txtTotalMonthlyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyMedium", txtTotalRatioMonthlyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyMediumColor", txtTotalRatioMonthlyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@MonthlyHigh", txtMonthlyHigh.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighColor", txtMonthlyHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalMonthlyHigh", txtTotalMonthlyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyHighColor", txtTotalMonthlyHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyHigh", txtTotalRatioMonthlyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyHighColor", txtTotalRatioMonthlyHighColor.Text)

                    cmd.Parameters.AddWithValue("@MonthlyVeryHigh", txtMonthlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@MonthlyVeryHighColor", txtMonthlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalMonthlyVeryHigh", txtTotalMonthlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyVeryHighColor", txtTotalMonthlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyVeryHigh", txtTotalRatioMonthlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyVeryHighColor", txtTotalRatioMonthlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@YearlyLow", txtYearlyLow.Text)
                    cmd.Parameters.AddWithValue("@YearlyLowColor", txtYearlyLowColor.Text)

                    cmd.Parameters.AddWithValue("@TotalYearlyLow", txtTotalYearlyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyLowColor", txtTotalYearlyLowColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioYearlyLow", txtTotalRatioYearlyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyLowColor", txtTotalRatioYearlyLowColor.Text)

                    cmd.Parameters.AddWithValue("@YearlyMedium", txtYearlyMedium.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumColor", txtYearlyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@TotalYearlyMedium", txtTotalYearlyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyMediumColor", txtTotalYearlyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioYearlyMedium", txtTotalRatioYearlyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyMediumColor", txtTotalRatioYearlyMediumColor.Text)

                    cmd.Parameters.AddWithValue("@YearlyHigh", txtYearlyHigh.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighColor", txtYearlyHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalYearlyHigh", txtTotalYearlyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyHighColor", txtTotalYearlyHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioYearlyHigh", txtTotalRatioYearlyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyHighColor", txtTotalRatioYearlyHighColor.Text)

                    cmd.Parameters.AddWithValue("@YearlyVeryHigh", txtYearlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@YearlyVeryHighColor", txtYearlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalYearlyVeryHigh", txtTotalYearlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyVeryHighColor", txtTotalYearlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioYearlyVeryHigh", txtTotalRatioYearlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyVeryHighColor", txtTotalRatioYearlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@DailyLowLabel", txtDailyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumLabel", txtDailyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyHighLabel", txtDailyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyVeryHighLabel", txtDailyVeryHighLabel.Text)

                    cmd.Parameters.AddWithValue("@WeeklyLowLabel", txtWeeklyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumLabel", txtWeeklyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighLabel", txtWeeklyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyVeryHighLabel", txtWeeklyVeryHighLabel.Text)

                    cmd.Parameters.AddWithValue("@MonthlyLowLabel", txtMonthlyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumLabel", txtMonthlyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighLabel", txtMonthlyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyVeryHighLabel", txtMonthlyVeryHighLabel.Text)

                    cmd.Parameters.AddWithValue("@YearlyLowLabel", txtYearlyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumLabel", txtYearlyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighLabel", txtYearlyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyVeryHighLabel", txtYearlyVeryHighLabel.Text)


                    con.Open()
                    cmd.ExecuteNonQuery()
                    'To Get Last inserted id
                    cmd.Parameters.AddWithValue("newId", cmd.LastInsertedId)
                    hiddenRcNo.Value = Convert.ToInt32(cmd.Parameters("@newId").Value)

                    con.Close()
                End Using
            End Using
        Else

            'Update existing Device Event Threshold
            Using con As New MySqlConnection(constr)
                Dim query As String = " UPDATE tbldeviceeventthreshold SET AccountID = @AccountID ,DeviceType = @DeviceType ,DailyLow = @DailyLow ,DailyLowColor = @DailyLowColor ,DailyMedium = @DailyMedium " & _
                                        ",DailyMediumColor = @DailyMediumColor ,DailyHigh = @DailyHigh ,DailyHighColor = @DailyHighColor ,DailyVeryHigh = @DailyVeryHigh ,DailyVeryHighColor = @DailyVeryHighColor " & _
                                        ",WeeklyLow = @WeeklyLow ,WeeklyLowColor = @WeeklyLowColor,WeeklyMedium = @WeeklyMedium ,WeeklyMediumColor = @WeeklyMediumColor ,WeeklyHigh = @WeeklyHigh ,WeeklyHighColor = @WeeklyHighColor" & _
                                        ",WeeklyVeryHigh = @WeeklyVeryHigh ,WeeklyVeryHighColor = @WeeklyVeryHighColor,MonthlyLow = @MonthlyLow ,MonthlyLowColor = @MonthlyLowColor ,MonthlyMedium = @MonthlyMedium ,MonthlyMediumColor = @MonthlyMediumColor" & _
                                        ",MonthlyHigh = @MonthlyHigh ,MonthlyHighColor = @MonthlyHighColor ,MonthlyVeryHigh = @MonthlyVeryHigh ,MonthlyVeryHighColor = @MonthlyVeryHighColor ,YearlyLow = @YearlyLow ,YearlyLowColor = @YearlyLowColor" & _
                                        ",YearlyMedium = @YearlyMedium ,YearlyMediumColor = @YearlyMediumColor ,YearlyHigh = @YearlyHigh ,YearlyHighColor = @YearlyHighColor ,YearlyVeryHigh = @YearlyVeryHigh ,YearlyVeryHighColor = @YearlyVeryHighColor" & _
                                         ",TotalDailyLow=@TotalDailyLow,TotalDailyLowColor=@TotalDailyLowColor,TotalDailyMedium=@TotalDailyMedium,TotalDailyMediumColor=@TotalDailyMediumColor" & _
                                         ",TotalDailyHigh=@TotalDailyHigh,TotalDailyHighColor=@TotalDailyHighColor,TotalDailyVeryHigh=@TotalDailyVeryHigh,TotalDailyVeryHighColor=@TotalDailyVeryHighColor" & _
                                         ",TotalWeeklyLow=@TotalWeeklyLow,TotalWeeklyLowColor=@TotalWeeklyLowColor,TotalWeeklyMedium=@TotalWeeklyMedium,TotalWeeklyMediumColor=@TotalWeeklyMediumColor" & _
                                         ",TotalWeeklyHigh=@TotalWeeklyHigh,TotalWeeklyHighColor=@TotalWeeklyHighColor,TotalWeeklyVeryHigh=@TotalWeeklyVeryHigh,TotalWeeklyVeryHighColor=@TotalWeeklyVeryHighColor" & _
                                         ",TotalMonthlyLow=@TotalMonthlyLow,TotalMonthlyLowColor=@TotalMonthlyLowColor,TotalMonthlyMedium=@TotalMonthlyMedium,TotalMonthlyMediumColor=@TotalMonthlyMediumColor" & _
                                         ",TotalMonthlyHigh=@TotalMonthlyHigh,TotalMonthlyHighColor=@TotalMonthlyHighColor,TotalMonthlyVeryHigh=@TotalMonthlyVeryHigh,TotalMonthlyVeryHighColor=@TotalMonthlyVeryHighColor" & _
                                         ",TotalYearlyLow=@TotalYearlyLow,TotalYearlyLowColor=@TotalYearlyLowColor,TotalYearlyMedium=@TotalYearlyMedium,TotalYearlyMediumColor=@TotalYearlyMediumColor" & _
                                         ",TotalYearlyHighColor=@TotalYearlyHighColor,TotalYearlyVeryHighColor=@TotalYearlyVeryHighColor" & _
                                         ",TotalRatioDailyLow=@TotalRatioDailyLow,TotalRatioDailyLowColor=@TotalRatioDailyLowColor,TotalRatioDailyMedium=@TotalRatioDailyMedium,TotalRatioDailyMediumColor=@TotalRatioDailyMediumColor" & _
                                         ",TotalRatioDailyHigh=@TotalRatioDailyHigh,TotalRatioDailyHighColor=@TotalRatioDailyHighColor,TotalRatioDailyVeryHigh=@TotalRatioDailyVeryHigh,TotalRatioDailyVeryHighColor=@TotalRatioDailyVeryHighColor" & _
                                         ",TotalRatioWeeklyLow=@TotalRatioWeeklyLow,TotalRatioWeeklyLowColor=@TotalRatioWeeklyLowColor,TotalRatioWeeklyMedium=@TotalRatioWeeklyMedium,TotalRatioWeeklyMediumColor=@TotalRatioWeeklyMediumColor" & _
                                         ",TotalRatioWeeklyHigh=@TotalRatioWeeklyHigh,TotalRatioWeeklyHighColor=@TotalRatioWeeklyHighColor,TotalRatioWeeklyVeryHigh=@TotalRatioWeeklyVeryHigh,TotalRatioWeeklyVeryHighColor=@TotalRatioWeeklyVeryHighColor" & _
                                         ",TotalRatioMonthlyLow=@TotalRatioMonthlyLow,TotalRatioMonthlyLowColor=@TotalRatioMonthlyLowColor,TotalRatioMonthlyMedium=@TotalRatioMonthlyMedium,TotalRatioMonthlyMediumColor=@TotalRatioMonthlyMediumColor" & _
                                         ",TotalRatioMonthlyHigh=@TotalRatioMonthlyHigh,TotalRatioMonthlyHighColor=@TotalRatioMonthlyHighColor,TotalRatioMonthlyVeryHigh=@TotalRatioMonthlyVeryHigh,TotalRatioMonthlyVeryHighColor=@TotalRatioMonthlyVeryHighColor" & _
                                         ",TotalRatioYearlyLow=@TotalRatioYearlyLow,TotalRatioYearlyLowColor=@TotalRatioYearlyLowColor,TotalRatioYearlyMedium=@TotalRatioYearlyMedium,TotalRatioYearlyMediumColor=@TotalRatioYearlyMediumColor" & _
                                         ",TotalRatioYearlyHigh=@TotalRatioYearlyHigh,TotalRatioYearlyHighColor=@TotalRatioYearlyHighColor,TotalRatioYearlyVeryHigh=@TotalRatioYearlyVeryHigh,TotalRatioYearlyVeryHighColor=@TotalRatioYearlyVeryHighColor" & _
                                         ",DailyLowLabel = @DailyLowLabel ,DailyMediumLabel = @DailyMediumLabel ,TotalYearlyHigh=@TotalYearlyHigh,DailyHighLabel = @DailyHighLabel ,TotalYearlyVeryHigh=@TotalYearlyVeryHigh,DailyVeryHighLabel = @DailyVeryHighLabel ,WeeklyLowLabel = @WeeklyLowLabel ,WeeklyMediumLabel = @WeeklyMediumLabel ,WeeklyHighLabel = @WeeklyHighLabel ,WeeklyVeryHighLabel = @WeeklyVeryHighLabel,MonthlyLowLabel = @MonthlyLowLabel ,MonthlyMediumLabel = @MonthlyMediumLabel ,MonthlyHighLabel = @MonthlyHighLabel ,MonthlyVeryHighLabel = @MonthlyVeryHighLabel,YearlyLowLabel = @YearlyLowLabel ,YearlyMediumLabel = @YearlyMediumLabel ,YearlyHighLabel = @YearlyHighLabel ,YearlyVeryHighLabel = @YearlyVeryHighLabel " & _
                                        "WHERE RcNo = @RcNo "

                Using cmd As New MySqlCommand(query)
                    cmd.Connection = con

                    cmd.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)

                    If ddlDeviceType.Visible = True Then
                        cmd.Parameters.AddWithValue("@AccountID", ddlAccountID.SelectedValue)
                        cmd.Parameters.AddWithValue("@DeviceType", ddlDeviceType.SelectedValue)
                    Else
                        cmd.Parameters.AddWithValue("@AccountID", Nothing)
                        cmd.Parameters.AddWithValue("@DeviceType", "DEFAULT")
                    End If

                    cmd.Parameters.AddWithValue("@DailyLow", txtDailyLow.Text)
                    cmd.Parameters.AddWithValue("@DailyLowColor", txtDailyLowColor.Text)
                    cmd.Parameters.AddWithValue("@DailyMedium", txtDailyMedium.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumColor", txtDailyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@DailyHigh", txtDailyHigh.Text)
                    cmd.Parameters.AddWithValue("@DailyHighColor", txtDailyHighColor.Text)
                    cmd.Parameters.AddWithValue("@DailyVeryHigh", txtDailyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@DailyVeryHighColor", txtDailyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalDailyLow", txtTotalDailyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyLowColor", txtTotalDailyLowColor.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyMedium", txtTotalDailyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyMediumColor", txtTotalDailyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyHigh", txtTotalDailyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyHighColor", txtTotalDailyHighColor.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyVeryHigh", txtTotalDailyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalDailyVeryHighColor", txtTotalDailyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioDailyLow", txtTotalRatioDailyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyLowColor", txtTotalRatioDailyLowColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyMedium", txtTotalRatioDailyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyMediumColor", txtTotalRatioDailyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyHigh", txtTotalRatioDailyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyHighColor", txtTotalRatioDailyHighColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyVeryHigh", txtTotalRatioDailyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioDailyVeryHighColor", txtTotalRatioDailyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@WeeklyLow", txtWeeklyLow.Text)
                    cmd.Parameters.AddWithValue("@WeeklyLowColor", txtWeeklyLowColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMedium", txtWeeklyMedium.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumColor", txtWeeklyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHigh", txtWeeklyHigh.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighColor", txtWeeklyHighColor.Text)
                    cmd.Parameters.AddWithValue("@WeeklyVeryHigh", txtWeeklyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@WeeklyVeryHighColor", txtWeeklyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalWeeklyLow", txtTotalWeeklyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyLowColor", txtTotalWeeklyLowColor.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyMedium", txtTotalWeeklyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyMediumColor", txtTotalWeeklyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyHigh", txtTotalWeeklyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyHighColor", txtTotalWeeklyHighColor.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyVeryHigh", txtTotalWeeklyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalWeeklyVeryHighColor", txtTotalWeeklyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyLow", txtTotalRatioWeeklyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyLowColor", txtTotalRatioWeeklyLowColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyMedium", txtTotalRatioWeeklyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyMediumColor", txtTotalRatioWeeklyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyHigh", txtTotalRatioWeeklyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyHighColor", txtTotalRatioWeeklyHighColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyVeryHigh", txtTotalRatioWeeklyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioWeeklyVeryHighColor", txtTotalRatioWeeklyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@MonthlyLow", txtMonthlyLow.Text)
                    cmd.Parameters.AddWithValue("@MonthlyLowColor", txtMonthlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMedium", txtMonthlyMedium.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumColor", txtMonthlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHigh", txtMonthlyHigh.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighColor", txtMonthlyHighColor.Text)
                    cmd.Parameters.AddWithValue("@MonthlyVeryHigh", txtMonthlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@MonthlyVeryHighColor", txtMonthlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalMonthlyLow", txtTotalMonthlyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyLowColor", txtTotalMonthlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyMedium", txtTotalMonthlyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyMediumColor", txtTotalMonthlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyHigh", txtTotalMonthlyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyHighColor", txtTotalMonthlyHighColor.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyVeryHigh", txtTotalMonthlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalMonthlyVeryHighColor", txtTotalMonthlyVeryHighColor.Text)


                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyLow", txtTotalRatioMonthlyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyLowColor", txtTotalRatioMonthlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyMedium", txtTotalRatioMonthlyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyMediumColor", txtTotalRatioMonthlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyHigh", txtTotalRatioMonthlyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyHighColor", txtTotalRatioMonthlyHighColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyVeryHigh", txtTotalRatioMonthlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioMonthlyVeryHighColor", txtTotalRatioMonthlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@YearlyLow", txtYearlyLow.Text)
                    cmd.Parameters.AddWithValue("@YearlyLowColor", txtYearlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyMedium", txtYearlyMedium.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumColor", txtYearlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyHigh", txtYearlyHigh.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighColor", txtYearlyHighColor.Text)
                    cmd.Parameters.AddWithValue("@YearlyVeryHigh", txtYearlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@YearlyVeryHighColor", txtYearlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalYearlyLow", txtTotalYearlyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyLowColor", txtTotalYearlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyMedium", txtTotalYearlyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyMediumColor", txtTotalYearlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyHigh", txtTotalYearlyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyHighColor", txtTotalYearlyHighColor.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyVeryHigh", txtTotalYearlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalYearlyVeryHighColor", txtTotalYearlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@TotalRatioYearlyLow", txtTotalRatioYearlyLow.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyLowColor", txtTotalRatioYearlyLowColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyMedium", txtTotalRatioYearlyMedium.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyMediumColor", txtTotalRatioYearlyMediumColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyHigh", txtTotalRatioYearlyHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyHighColor", txtTotalRatioYearlyHighColor.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyVeryHigh", txtTotalRatioYearlyVeryHigh.Text)
                    cmd.Parameters.AddWithValue("@TotalRatioYearlyVeryHighColor", txtTotalRatioYearlyVeryHighColor.Text)

                    cmd.Parameters.AddWithValue("@DailyLowLabel", txtDailyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyMediumLabel", txtDailyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyHighLabel", txtDailyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@DailyVeryHighLabel", txtDailyVeryHighLabel.Text)

                    cmd.Parameters.AddWithValue("@WeeklyLowLabel", txtWeeklyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyMediumLabel", txtWeeklyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyHighLabel", txtWeeklyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@WeeklyVeryHighLabel", txtWeeklyVeryHighLabel.Text)

                    cmd.Parameters.AddWithValue("@MonthlyLowLabel", txtMonthlyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyMediumLabel", txtMonthlyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyHighLabel", txtMonthlyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@MonthlyVeryHighLabel", txtMonthlyVeryHighLabel.Text)

                    cmd.Parameters.AddWithValue("@YearlyLowLabel", txtYearlyLowLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyMediumLabel", txtYearlyMediumLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyHighLabel", txtYearlyHighLabel.Text)
                    cmd.Parameters.AddWithValue("@YearlyVeryHighLabel", txtYearlyVeryHighLabel.Text)

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                End Using
            End Using

        End If
        loadGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "hidepopup();", True)

    End Sub

    Protected Sub GridDeviceEvent_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim deviceType As String = TryCast(e.Row.FindControl("lblDeviceType"), Label).Text
            If deviceType.ToUpper() = "DEFAULT" Then
                TryCast(e.Row.FindControl("lnkDelete"), LinkButton).Visible = False
                TryCast(e.Row.FindControl("lnkDuplicate"), LinkButton).Visible = False
            End If
        End If
    End Sub
End Class
Public Class DeviceEventThresholdModel

    Public Property RcNo() As Integer
    Public Property AccountID() As String
    Public Property DeviceType() As String

    'Daily
    Public Property DailyLow() As Integer
    Public Property DailyLowColor() As String

    Public Property DailyMedium() As Integer
    Public Property DailyMediumColor() As String

    Public Property DailyHigh() As Integer
    Public Property DailyHighColor() As String

    Public Property DailyVeryHigh() As Integer
    Public Property DailyVeryHighColor() As String

    Public Property TotalDailyLow() As String
    Public Property TotalDailyLowColor() As String

    Public Property TotalDailyMedium() As String
    Public Property TotalDailyMediumColor() As String

    Public Property TotalDailyHigh() As String
    Public Property TotalDailyHighColor() As String

    Public Property TotalDailyVeryHigh() As String
    Public Property TotalDailyVeryHighColor() As String

    Public Property TotalRatioDailyLow() As String
    Public Property TotalRatioDailyLowColor() As String

    Public Property TotalRatioDailyMedium() As String
    Public Property TotalRatioDailyMediumColor() As String

    Public Property TotalRatioDailyHigh() As String
    Public Property TotalRatioDailyHighColor() As String

    Public Property TotalRatioDailyVeryHigh() As String
    Public Property TotalRatioDailyVeryHighColor() As String

    'Weekly
    Public Property WeeklyLow() As Integer
    Public Property WeeklyLowColor() As String

    Public Property WeeklyMedium() As Integer
    Public Property WeeklyMediumColor() As String

    Public Property WeeklyHigh() As Integer
    Public Property WeeklyHighColor() As String

    Public Property WeeklyVeryHigh() As Integer
    Public Property WeeklyVeryHighColor() As String

    Public Property TotalWeeklyLow() As String
    Public Property TotalWeeklyLowColor() As String

    Public Property TotalWeeklyMedium() As String
    Public Property TotalWeeklyMediumColor() As String

    Public Property TotalWeeklyHigh() As String
    Public Property TotalWeeklyHighColor() As String

    Public Property TotalWeeklyVeryHigh() As String
    Public Property TotalWeeklyVeryHighColor() As String

    Public Property TotalRatioWeeklyLow() As String
    Public Property TotalRatioWeeklyLowColor() As String

    Public Property TotalRatioWeeklyMedium() As String
    Public Property TotalRatioWeeklyMediumColor() As String

    Public Property TotalRatioWeeklyHigh() As String
    Public Property TotalRatioWeeklyHighColor() As String

    Public Property TotalRatioWeeklyVeryHigh() As String
    Public Property TotalRatioWeeklyVeryHighColor() As String

    'Monthly
    Public Property MonthlyLow() As Integer
    Public Property MonthlyLowColor() As String

    Public Property MonthlyMedium() As Integer
    Public Property MonthlyMediumColor() As String

    Public Property MonthlyHigh() As Integer
    Public Property MonthlyHighColor() As String

    Public Property MonthlyVeryHigh() As Integer
    Public Property MonthlyVeryHighColor() As String

    Public Property TotalMonthlyLow() As String
    Public Property TotalMonthlyLowColor() As String

    Public Property TotalMonthlyMedium() As String
    Public Property TotalMonthlyMediumColor() As String

    Public Property TotalMonthlyHigh() As String
    Public Property TotalMonthlyHighColor() As String

    Public Property TotalMonthlyVeryHigh() As String
    Public Property TotalMonthlyVeryHighColor() As String

    Public Property TotalRatioMonthlyLow() As String
    Public Property TotalRatioMonthlyLowColor() As String

    Public Property TotalRatioMonthlyMedium() As String
    Public Property TotalRatioMonthlyMediumColor() As String

    Public Property TotalRatioMonthlyHigh() As String
    Public Property TotalRatioMonthlyHighColor() As String

    Public Property TotalRatioMonthlyVeryHigh() As String
    Public Property TotalRatioMonthlyVeryHighColor() As String

    'Yearly
    Public Property YearlyLow() As Integer
    Public Property YearlyLowColor() As String

    Public Property YearlyMedium() As Integer
    Public Property YearlyMediumColor() As String

    Public Property YearlyHigh() As Integer
    Public Property YearlyHighColor() As String

    Public Property YearlyVeryHigh() As Integer
    Public Property YearlyVeryHighColor() As String

    Public Property TotalYearlyLow() As String
    Public Property TotalYearlyLowColor() As String

    Public Property TotalYearlyMedium() As String
    Public Property TotalYearlyMediumColor() As String

    Public Property TotalYearlyHigh() As String
    Public Property TotalYearlyHighColor() As String

    Public Property TotalYearlyVeryHigh() As String
    Public Property TotalYearlyVeryHighColor() As String

    Public Property TotalRatioYearlyLow() As String
    Public Property TotalRatioYearlyLowColor() As String

    Public Property TotalRatioYearlyMedium() As String
    Public Property TotalRatioYearlyMediumColor() As String

    Public Property TotalRatioYearlyHigh() As String
    Public Property TotalRatioYearlyHighColor() As String

    Public Property TotalRatioYearlyVeryHigh() As String
    Public Property TotalRatioYearlyVeryHighColor() As String

    Public Property DailyLowLabel() As String
    Public Property DailyMediumLabel() As String
    Public Property DailyHighLabel() As String
    Public Property DailyVeryHighLabel() As String

    Public Property WeeklyLowLabel() As String
    Public Property WeeklyMediumLabel() As String
    Public Property WeeklyHighLabel() As String
    Public Property WeeklyVeryHighLabel() As String

    Public Property MonthlyLowLabel() As String
    Public Property MonthlyMediumLabel() As String
    Public Property MonthlyHighLabel() As String
    Public Property MonthlyVeryHighLabel() As String

    Public Property YearlyLowLabel() As String
    Public Property YearlyMediumLabel() As String
    Public Property YearlyHighLabel() As String
    Public Property YearlyVeryHighLabel() As String

End Class
Public Class AccountIDListModel
    Public Property AccountID() As String
End Class


