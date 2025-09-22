Imports System.Timers
Imports System.IO
Imports System.Net
Imports MySql.Data
Imports System.Data
Imports MySql.Data.MySqlClient

Partial Class LoginOTP
    Inherits System.Web.UI.Page

    Public timeleft As Int16

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtOTP.Text = Convert.ToString(Session("OTP"))
        txtOTPNumber.Text = Convert.ToString(Session("OTPNumber"))
        lblOTPPrefix.Text = Convert.ToString(Session("OTPPrefix")) + "-"
        txtOTPExpiry.Text = Convert.ToDateTime(Session("OTPExpiry")).ToString

        If String.IsNullOrEmpty(txtOTP.Text) Then
            Response.Redirect("Login.aspx")
        End If

        lblSysTime.Text = Convert.ToDateTime(Session("SysTime")).ToString

        If Convert.ToString(Session("MobileSMS")) = "Yes" Then
            btnSMSOTP.Visible = True
            txtToMobile.Text = Convert.ToString(Session("MobileNumber"))
        Else
            btnSMSOTP.Visible = False
            txtToMobile.Text = 0
        End If
        lblDomainName.Text = ConfigurationManager.AppSettings("DomainName").ToString()
        If lblDomainName.Text = "SINGAPORE-NEW" Then
            btnSMSOTP.Visible = True
        ElseIf lblDomainName.Text = "SINGAPORE-NEW2" Then
            btnSMSOTP.Visible = True
        ElseIf lblDomainName.Text = "PEST-PRO" Then
            btnSMSOTP.Visible = True
        ElseIf lblDomainName.Text = "SINGAPORE-NEW (Beta)" Then
            btnSMSOTP.Visible = True
        Else
            btnSMSOTP.Visible = False
            txtToMobile.Text = 0
        End If

        btnSMSOTP.Visible = False

        If lblDomainName.Text = "SINGAPORE-NEW (Beta)" Then
            btnSMSOTP.Visible = True
        End If


        If Not Me.IsPostBack Then
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
        End If


        'Label3.Text = lblSysTime.Text
        'Label4.Text = txtOTPExpiry.Text
        ''  Return
        'Label1.Text = lblSysTime.Text
        'Label2.Text = txtOTPExpiry.Text

        ' Timer1.Enabled = True

        'timeleft = 60

        'Dim minutediff = DateDiff(DateInterval.Minute, Convert.ToDateTime(lblSysTime.Text), Convert.ToDateTime(txtOTPExpiry.Text))
        'Dim seconddiff = DateDiff(DateInterval.Second, Convert.ToDateTime(lblSysTime.Text), Convert.ToDateTime(txtOTPExpiry.Text))

        'Dim difference As TimeSpan = Convert.ToDateTime(txtOTPExpiry.Text) - Convert.ToDateTime(lblSysTime.Text)
        'lblTime.Text = difference.Minutes.ToString + " minutes " + difference.Seconds.ToString + " seconds"


        '  If Not IsPostBack Then
        '    timeleft = (Convert.ToDateTime(txtOTPExpiry.Text) - Convert.ToDateTime(lblSysTime.Text)).TotalMinutes * 60
        ' timeleft = 60

        'Dim minutediff = DateDiff(DateInterval.Minute, Convert.ToDateTime(lblSysTime.Text), Convert.ToDateTime(txtOTPExpiry.Text))
        'Dim seconddiff = DateDiff(DateInterval.Second, Convert.ToDateTime(lblSysTime.Text), Convert.ToDateTime(txtOTPExpiry.Text))

        'Dim difference As TimeSpan = Convert.ToDateTime(txtOTPExpiry.Text) - Convert.ToDateTime(lblSysTime.Text)
        ''   Label3.Text = difference.Minutes.ToString + " minutes " + difference.Seconds.ToString + " seconds"
        '  End If

        '  Timer2.Enabled = True
        '    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:setInterval(); ", true)
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtOTP.Text = ""
        txtOTPNumber.Text = ""
        lblOTPPrefix.Text = ""
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        If txtOTPNumberInput.Text.Trim = txtOTPNumber.Text Then


            Session.Add("UserID", Convert.ToString(Session("User")))

          

            ''''''''''''''''''''''''''''''''''''''
            Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            Dim conn As MySqlConnection = New MySqlConnection(constr)
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text

            command.Connection = conn
            'Convert.ToString(Session("UserID"))
            command.CommandText = "Select loginid, loggedOn from tbllogin where LoginId ='" & Convert.ToString(Session("User")) & "' order by rcno desc limit 1"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            'Dim content As String = ""
            'MessageBox.Message.Alert(Page, (dt.Rows(0)("loginid").ToString), "STR")
            'lblAlert.Text = dt.Rows(0)("loginid").ToString

            'lblAlert.Text = Convert.ToDateTime(dt.Rows(0)("loggedOn")).ToString("yyyy-MM-dd HH:mm:ss")

            'lblAlert.Text = DateAdd(DateInterval.Minute, 10, Convert.ToDateTime(dt.Rows(0)("loggedOn").ToString))
            'lblAlert.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB"))


            'lblMessage.Text = DateAdd(DateInterval.Minute, 10, Convert.ToDateTime(dt.Rows(0)("loggedOn").ToString)) & " and " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB"))
            If dt.Rows.Count > 0 Then
                'MessageBox.Message.Alert(Page, Convert.ToDateTime(dt.Rows(0)("loggedOn").ToString("yyyy-MM-dd HH:mm:ss")), "STR")
                'MessageBox.Message.Alert(Page, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), "STR")
                'MessageBox.Message.Alert(Page, DateAdd(DateInterval.Minute, 10, Convert.ToDateTime(dt.Rows(0)("loggedOn").ToString("yyyy-MM-dd HH:mm:ss"))), "STR")


                If DateAdd(DateInterval.Minute, 10, Convert.ToDateTime(dt.Rows(0)("loggedOn").ToString)) < DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")) Then
                    'lblAlert.Text = "OTP has Expired-1"
                    MessageBox.Message.Alert(Page, "OTP has Expired", "STR")
                    Return
                    'ElseIf DateAdd(DateInterval.Minute, 10, Convert.ToDateTime(dt.Rows(0)("loggedOn").ToString)) > DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")) Then
                    '    lblAlert.Text = "OTP has Expired-2"
                    '    Return

                End If

            End If    'Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Dim conn As MySqlConnection = New MySqlConnection(constr)
            'conn.Open()

            'Dim commandDeleteFromTemp As MySqlCommand = New MySqlCommand
            'commandDeleteFromTemp.CommandType = CommandType.StoredProcedure
            ''If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then

            'commandDeleteFromTemp.CommandText = "DeleteTempTables"
            'commandDeleteFromTemp.Parameters.Clear()

            ''commandDeleteFromTemp.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            'commandDeleteFromTemp.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
            'commandDeleteFromTemp.Connection = conn
            'commandDeleteFromTemp.ExecuteScalar()

            'commandDeleteFromTemp.Dispose()

            ''''''''''''
            Response.Redirect("Home.aspx")
        Else
            MessageBox.Message.Alert(Page, "Invalid OTP", "STR")
            Return

        End If
    End Sub

    'Protected Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
    '    '   Label2.Text = Convert.ToString(Session("SysTime"))
    '    If timeleft > 0 Then
    '        ' Display the new time left 
    '        ' by updating the Time Left label.
    '        timeleft = timeleft - 1
    '        Label2.Text = timeleft.ToString + " seconds"
    '    Else
    '        ' If the user ran out of time, stop the timer, show 
    '        ' a MessageBox, and fill in the answers.
    '        Timer2.Enabled = False
    '        Label2.Text = "Time's up!"
    '        '
    '        btnSubmit.Enabled = False
    '        Response.Redirect("Login.aspx")
    '    End If
    'End Sub


    Protected Sub GetTime(sender As Object, e As EventArgs)
        lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
        ' Dim d As Double = DateTime.Now.Subtract(Convert.ToDateTime(txtOTPExpiry.Text)).TotalSeconds
        '  btnSubmit.Text = DateTime.Now.Subtract(Convert.ToDateTime(txtOTPExpiry.Text)).TotalSeconds.ToString

        If lblTime.Text = Convert.ToDateTime(txtOTPExpiry.Text).ToString("hh:mm:ss tt") Then
            btnSubmit.Enabled = False
            Session.Clear()

            Response.Redirect("Login.aspx")
        End If
        '   timeleft = timeleft - 1
        '  lblTime.Text = timeleft
        'If timeleft > 0 Then
        '    btnSubmit.Enabled = False
        '    ' Display the new time left 
        '    ' by updating the Time Left label.
        '    timeleft = timeleft - 1
        '    lblTime.Text = timeleft.ToString + " seconds"
        'Else
        '    ' If the user ran out of time, stop the timer, show 
        '    ' a MessageBox, and fill in the answers.
        '    '     Timer2.Enabled = False
        '    lblTime.Text = "Time's up!"
        '    '
        '    btnSubmit.Enabled = True
        '    Response.Redirect("Login.aspx")
        'End If
    End Sub

    'Protected Sub UpdateTimer_Tick(sender As Object, e As EventArgs) Handles UpdateTimer.Tick
    '    '  DateStampLabel.Text = Convert.ToDateTime(txtOTPExpiry.Text).ToLongDateString

    '    If timeleft > 0 Then
    '        btnSubmit.Enabled = False
    '        ' Display the new time left 
    '        ' by updating the Time Left label.
    '        timeleft = timeleft - 1
    '        Label2.Text = timeleft.ToString + " seconds"
    '    Else
    '        ' If the user ran out of time, stop the timer, show 
    '        ' a MessageBox, and fill in the answers.
    '        '     Timer2.Enabled = False
    '        Label2.Text = "Time's up!"
    '        '
    '        btnSubmit.Enabled = True
    '        Response.Redirect("Login.aspx")
    '    End If
    'End Sub

    'Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    If timeleft > 0 Then
    '        ' Display the new time left 
    '        ' by updating the Time Left label.
    '        timeleft = timeleft - 1
    '        Label4.Text = timeleft.ToString + " seconds"
    '    Else
    '        ' If the user ran out of time, stop the timer, show 
    '        ' a MessageBox, and fill in the answers.
    '        '     Timer2.Enabled = False
    '        Label4.Text = "Time's up!"
    '        '
    '        btnSubmit.Enabled = False
    '        Response.Redirect("Login.aspx")
    '    End If
    'End Sub

    Protected Sub btnSMSOTP_Click(sender As Object, e As EventArgs) Handles btnSMSOTP.Click

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn

            command.CommandText = "Select *,trim(replace(REPLACE(SMSFormat,'\r\n','\\n '),'""','''')) as SMSFormat1 from tblsmsSetUp where Module='LOGIN OTP'"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            Dim content As String = ""

            If dt.Rows.Count > 0 Then

                content = dt.Rows(0)("SMSFormat1").ToString

            End If

            ' content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            'content = content.Replace("StaffName", StaffName)
            content = content.Replace("Domain", ConfigurationManager.AppSettings("DomainName").ToString())
            content = content.Replace("OTPNO", txtOTP.Text)
            content = content.Replace("SYSTIME", lblSysTime.Text)

            '   Dim smstext As String = " Welcome to " + ConfigurationManager.AppSettings("DomainName").ToString() + ".\n Your One-Time Password ((OTP)) is : " + txtOTP.Text + ".\n Take note that this OTP is valid only for 10 minutes.\n Time Generated: " + lblSysTime.Text + "."
            Dim SMSTEXT As String = content
            Dim WhatsappAPIUserID As String = ConfigurationManager.AppSettings("WhatsappAPIUserID").ToString()
            Dim WhatsappAPIPassword As String = ConfigurationManager.AppSettings("WhatsappAPIPassword").ToString()

            Dim ToMobile As String = txtToMobile.Text.Replace(" ", "").ToString

            If ToMobile.Length = 8 Then
                ToMobile = "65" + ToMobile
            ElseIf ToMobile.First = "+" Then
                ' ToMobile = ToMobile.Remove(0).ToString
                ToMobile = ToMobile.Replace("+", "").ToString
            End If
            'MessageBox.Message.Alert(Page, smstext, "str")
            'Return

            Dim postData As String = "{""userName"":""" + WhatsappAPIUserID + """,""password"":""" + WhatsappAPIPassword + """, ""toMobile"":""" + ToMobile + """,""recipientName"":""" + Convert.ToString(Session("User")) + """, ""senderId"":""6582920371"",""waText"":""" + SMSTEXT + """,""waType"":""S1"",""taskName"":""T001"", ""timeOffset"":""0"", ""priority"":""3"" }"


            Dim webRequest As HttpWebRequest = Net.WebRequest.CreateHttp("Https://wa.CommSwift.com/Api/WAAPI.svc/WA/Post/SendWA")
            Dim data As Byte() = Encoding.ASCII.GetBytes(postData)
            webRequest.Accept = "application/xml" ' Accept is needed only for XML output. For JSON this line is not needed.
            webRequest.Method = "POST"
            webRequest.ContentType = "application/json"
            webRequest.ContentLength = data.Length
            Dim reqStream As System.IO.Stream = webRequest.GetRequestStream()
            reqStream.Write(data, 0, data.Length)
            Dim respStream = webRequest.GetResponse().GetResponseStream()
            Dim res As String = (New StreamReader(respStream)).ReadToEnd()

            If lblDomainName.Text = "SINGAPORE-NEW (Beta)" Then
                lblAlert.Text = res
                MessageBox.Message.Alert(Page, lblAlert.Text, "str")

                Return
            End If

           

            If res = "OK" Or res.Contains("OK") Then

            Else


                Dim res1 As rapidsms.ResultCodes = New rapidsms.ResultCodes()

                Dim myapi As rapidsms.mySMS_SQLSoapClient = New rapidsms.mySMS_SQLSoapClient()

                myapi.SendSMS2("APM.Admin", "PowerToSMS0771+-", txtToMobile.Text, Convert.ToString(Session("User")), "+6582920371", smstext, "EWAPI", "", "t1", 0, 3)
                btnSMSOTP.Enabled = False
            End If
            'Dim wc As New WebClient()
            'wc.Headers.Add("Accept", "application/xml") ' Add header for XML output. 

            'Dim res As String = wc.DownloadString("https://app.CommSwift.com/Api/WAAPI.svc/WA/SendWA?userName=APM.ApmAdmin&password=Cs12345678&toMobile=" + txtToMobile.Text + "&recipientName=" + Convert.ToString(Session("User")) + "&senderId=6582920371&waText=" + smstext + "&waType=S1&taskName=T001&timeOffset=0&priority=3")




            btnSMSOTP.Enabled = False
        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.Message.ToString, "STR")
            Exit Sub

        End Try

        'Try


        'Dim smstext As String = "Welcome to AOL " + ConfigurationManager.AppSettings("DomainName").ToString() + ". Your One-Time Password(OTP) for your Anticimex login is : " + txtOTP.Text + ". Please note that this OTP is valid only for 10 minutes. Time Generated: " + lblSysTime.Text + ". Thank You.-AOL Secure Login."

        'Dim res As rapidsms.ResultCodes = New rapidsms.ResultCodes()

        'Dim myapi As rapidsms.mySMS_SQLSoapClient = New rapidsms.mySMS_SQLSoapClient()

        'myapi.SendSMS2("APM.Admin", "PowerToSMS0771+-", txtToMobile.Text, Convert.ToString(Session("User")), "+6582920371", smstext, "EWAPI", "", "t1", 0, 3)
        'btnSMSOTP.Enabled = False
        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, ex.Message.ToString, "STR")
        '    Exit Sub

        'End Try
    End Sub
End Class
