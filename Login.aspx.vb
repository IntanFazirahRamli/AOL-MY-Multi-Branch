Imports System
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports EASendMail


Partial Class Login
    Inherits System.Web.UI.Page

    Dim count As Int16 = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Label1.Text = DateTime.Now.Year
        txtUserID.Focus()
        '    lblSysDate.Attributes.Add("readonly", "readonly")
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimestartdate();", True)
        lblDomainName.Text = ConfigurationManager.AppSettings("DomainName").ToString()

        Session.Remove("UserID")
        Session.Clear()


        If lblDomainName.Text = "SINGAPORE-NEW" Then
            link.HRef = "https://www.anticimex.com/en-SG/"
        ElseIf lblDomainName.Text = "SINGAPORE-NEW (Beta)" Then
            link.HRef = "https://www.anticimex.com/en-SG/"
        ElseIf lblDomainName.Text = "MALAYSIA-NEW" Then
            link.HRef = "https://www.anticimex.com/en-MY/"
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrEmpty(txtUserID.Text) = True Then
            MessageBox.Message.Alert(Me, "Enter User ID", "Keystr1")
            Return
        End If

        Try
            Dim NameEncodein(txtPassword.Text.Length - 1) As Byte
            NameEncodein = System.Text.Encoding.UTF8.GetBytes(txtPassword.Text)
            Dim EcodedName As String = Convert.ToBase64String(NameEncodein)
            txtPassword.Text = EcodedName

            'Dim data() As Byte = Convert.FromBase64String(EcodedName)
            'Dim decodedString As String = System.Text.Encoding.UTF8.GetString(data)
            'txtPassword.Text = decodedString

            Session.Remove("contractfrom")
            Session.Remove("contractdetailfrom")
            Session.Remove("contractno")
            Session.Remove("serviceschedulefrom")
            Session.Remove("servicefrom")

            Session.Remove("contractdate")
            Session.Remove("contracttype")
            Session.Remove("client")
            Session.Remove("custname")
            Session.Remove("contact")
            Session.Remove("servstart")
            Session.Remove("servend")
            Session.Remove("agreedvalue")
            Session.Remove("discamt")
            Session.Remove("status")
            Session.Remove("accountid")
            Session.Remove("gridsqlCompany")
            Session.Remove("gridsqlPerson")
            ' Try

            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()



            command.CommandType = CommandType.Text
            command.CommandText = "SELECT Rcno, SecWebPassword, StaffID, Name, SecGroupAuthority,loginattempt,EmailPerson,TelMobile FROM tblstaff where SecWebLoginID = @userid and SystemUser='Y' and Status = 'O';"
            command.Parameters.AddWithValue("@userid", txtUserID.Text)
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then


                If txtPassword.Text = dt.Rows(0)("SecWebPassword").ToString() Then
                    Session.Add("UserID", txtUserID.Text.ToUpper)
                    Session.Add("ID", dt.Rows(0)("StaffID").ToString().ToUpper)
                    Session.Add("Name", dt.Rows(0)("Name").ToString().ToUpper)
                    Session.Add("SysDate", lblSysDate.Text)
                    Session.Add("SysTime", lblSysTime.Text)
                    Session.Add("StaffID", dt.Rows(0)("Staffid").ToString().ToUpper)
                    Session.Add("SecGroupAuthority", dt.Rows(0)("SecGroupAuthority").ToString())
                    Session.Add("UserRcno", dt.Rows(0)("Rcno").ToString())



                    If ConfigurationManager.AppSettings("OTPEnabled").ToString() = "Yes" Or ConfigurationManager.AppSettings("OTPEnabled").ToString() = "YES" Then


                        Session.Remove("UserID")
                        Session.Add("User", txtUserID.Text.ToUpper)

                        Const charsprefix As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
                        Const charsnumber As String = "0123456789"

                        Dim otp As String = ""
                        otp = RandomString(charsprefix, 3)
                        txtOTPPrefix.Text = otp
                        txtOTPNumber.Text = RandomString(charsnumber, 6)
                        otp = txtOTPPrefix.Text + "-" + txtOTPNumber.Text
                        txtOTP.Text = otp
                        If String.IsNullOrEmpty(dt.Rows(0)("TelMobile").ToString()) = False Then
                            Session.Add("MobileSMS", "Yes")
                            Session.Add("MobileNumber", dt.Rows(0)("TelMobile").ToString())
                        Else
                            Session.Add("MobileSMS", "No")
                        End If



                        If String.IsNullOrEmpty(dt.Rows(0)("EmailPerson").ToString()) = False Then

                            SendEmail(dt.Rows(0)("EmailPerson").ToString(), otp, dt.Rows(0)("Name").ToString().ToUpper)
                            '   SendEmail("sasi.vishwa@gmail.com", otp)
                            'If successful login, then update loginattempt to 0

                            Session.Add("OTP", txtOTP.Text)
                            Session.Add("OTPPrefix", txtOTPPrefix.Text)
                            Session.Add("OTPNumber", txtOTPNumber.Text)
                            Session.Add("SysTime", lblSysTime.Text)
                            Session.Add("OTPExpiry", Convert.ToDateTime(lblSysTime.Text).AddMinutes(10))



                            Dim command1 As MySqlCommand = New MySqlCommand

                            command1.CommandType = CommandType.Text
                            Dim qry As String = "UPDATE tblstaff SET loginattempt=0 where SecWebLoginID=@userid;"

                            command1.CommandText = qry
                            command1.Parameters.Clear()

                            command1.Parameters.AddWithValue("@userid", txtUserID.Text)
                            command1.Connection = conn

                            command1.ExecuteNonQuery()
                            command1.Dispose()


                            '''''''''''''''''''''''

                            ''''''''''''''''''
                            qry = "INSERT INTO tbllogin(LoginID, StaffId, LoggedOn) VALUES (@LoginID, @StaffId, @LoggedOn) "

                            Dim commandLogin As MySqlCommand = New MySqlCommand
                            commandLogin.CommandText = qry
                            commandLogin.Parameters.Clear()

                            commandLogin.Parameters.AddWithValue("@LoginID", txtUserID.Text)
                            commandLogin.Parameters.AddWithValue("@StaffId", dt.Rows(0)("Staffid").ToString().ToUpper())
                            'commandLogin.Parameters.AddWithValue("@LoggedOn", Convert.ToDateTime(lblSysTime.Text))
                            commandLogin.Parameters.AddWithValue("@LoggedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            commandLogin.Connection = conn
                            commandLogin.ExecuteNonQuery()
                            commandLogin.Dispose()

                            conn.Close()
                            'command1.Dispose()
                            '''''''''''''''''''''''''


                            IsDisplayRecordsLocationwise()



                            CheckSMSQuota(conn, dt.Rows(0)("EmailPerson").ToString())

                            ''''''''''''''''''''''''
                            '     Response.Redirect("Home.aspx")
                            Response.Redirect("LoginOTP.aspx")
                        Else
                            MessageBox.Message.Alert(Page, "Register your EmailID to receive the OTP. Please contact your administrator.", "str")
                            Return

                        End If
                    Else

                        'If successful login, then update loginattempt to 0


                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblstaff SET loginattempt=0 where SecWebLoginID=@userid;"

                        command1.CommandText = qry
                        command1.Parameters.Clear()

                        command1.Parameters.AddWithValue("@userid", txtUserID.Text)
                        command1.Connection = conn

                        command1.ExecuteNonQuery()
                        command1.Dispose()


                        ''''''''''''''''''
                        qry = "INSERT INTO tbllogin(LoginID, StaffId, LoggedOn) VALUES (@LoginID, @StaffId, @LoggedOn) "

                        Dim commandLogin As MySqlCommand = New MySqlCommand
                        commandLogin.CommandText = qry
                        commandLogin.Parameters.Clear()

                        commandLogin.Parameters.AddWithValue("@LoginID", txtUserID.Text)
                        commandLogin.Parameters.AddWithValue("@StaffId", dt.Rows(0)("Staffid").ToString().ToUpper())
                        'commandLogin.Parameters.AddWithValue("@LoggedOn", Convert.ToDateTime(lblSysTime.Text))
                        commandLogin.Parameters.AddWithValue("@LoggedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        commandLogin.Connection = conn
                        commandLogin.ExecuteNonQuery()
                        commandLogin.Dispose()

                        conn.Close()
                        commandLogin.Dispose()
                        'command1.Dispose()
                        '''''''''''''''''''''''''

                        IsDisplayRecordsLocationwise()
                        Response.Redirect("Home.aspx", False)
                    End If

                Else ' failed attempt
                    Dim count As Int16 = 0
                    Dim attempt As Int16 = 0

                    If String.IsNullOrEmpty(dt.Rows(0)("LoginAttempt").ToString) Then
                        'first failed attempt
                        count = 0

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblstaff SET loginattempt=1 where SecWebLoginID=@userid;"

                        command1.CommandText = qry
                        command1.Parameters.Clear()

                        command1.Parameters.AddWithValue("@userid", txtUserID.Text)
                        command1.Connection = conn

                        command1.ExecuteNonQuery()
                        lblMessage.Text = "You have made 1 unsuccessful attempt. You have 9 remaining attempts."
                        command1.Dispose()
                    ElseIf String.IsNullOrEmpty(dt.Rows(0)("LoginAttempt")) Or dt.Rows(0)("LoginAttempt").ToString = "" Or dt.Rows(0)("LoginAttempt") = 0 Then
                        'first failed attempt
                        count = 0

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblstaff SET loginattempt=1 where SecWebLoginID=@userid;"

                        command1.CommandText = qry
                        command1.Parameters.Clear()

                        command1.Parameters.AddWithValue("@userid", txtUserID.Text)
                        command1.Connection = conn

                        command1.ExecuteNonQuery()
                        lblMessage.Text = "You have made 1 unsuccessful attempt. You have 9 remaining attempts."
                        command1.Dispose()
                    ElseIf dt.Rows(0)("LoginAttempt") = 9 Then 'fifth failed attempt, makes the login status disabled
                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblstaff SET loginattempt=10,status='D',statusRemarks='Too many failed login attempts' where SecWebLoginID=@userid;"

                        command1.CommandText = qry
                        command1.Parameters.Clear()

                        command1.Parameters.AddWithValue("@userid", txtUserID.Text)
                        command1.Connection = conn

                        command1.ExecuteNonQuery()

                        'SEND EMAIL TO ADMIN WHEN LOGIN IS LOCKED OR DISABLED

                        Dim command4 As MySqlCommand = New MySqlCommand

                        command4.CommandType = CommandType.Text

                        command4.Connection = conn

                        command4.CommandText = "Select EmailPerson from tblstaff where StaffId='ADMIN'"
                        command4.Connection = conn

                        Dim dr1 As MySqlDataReader = command4.ExecuteReader()
                        Dim dt1 As New DataTable
                        dt1.Load(dr1)

                        If dt1.Rows.Count > 0 Then
                            Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
                            Dim oSmtp As New SmtpClient()

                            oMail.Subject = "NOTIFICATION: AOL ACCOUNT DISBLED"

                            Dim content As String = ""
                            content = "System Action : Account Disabled"
                            content = content + " <br/>" + "Date : " + DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-GB"))
                            content = content + " <br/>" + "Time : " + DateTime.Now.ToString("HH:mm:ss", New System.Globalization.CultureInfo("en-GB"))
                            content = content + " <br/>" + "Reason : Too many failed login attempts"
                            content = content + " <br/>" + "Staff ID : " + dt.Rows(0)("StaffID").ToString().ToUpper
                            content = content + " <br/>" + "Staff Name : " + dt.Rows(0)("Name").ToString().ToUpper
                            content = content + " <br/>" + "Domain : " + ConfigurationManager.AppSettings("DomainName").ToString()


                            oMail.HtmlBody = content

                            Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
                            oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
                            oServer.ConnectType = SmtpConnectType.ConnectDirectSSL

                            oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()
                            oMail.To = dt1.Rows(0)("EmailPerson").ToString

                            oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                            oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

                            oSmtp.SendMail(oServer, oMail)

                        End If

                        lblMessage.Text = "Your Account has been Locked, as you have exceeded your maximum unsuccessful attempts."
                        command1.Dispose()
                    ElseIf dt.Rows(0)("LoginAttempt") > 0 And dt.Rows(0)("LoginAttempt") < 9 Then
                        'failed attempt - count the failed attempt
                        count = dt.Rows(0)("LoginAttempt")
                        count = count + 1
                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblstaff SET loginattempt=@count where SecWebLoginID=@userid;"

                        command1.CommandText = qry
                        command1.Parameters.Clear()
                        command1.Parameters.AddWithValue("@count", count)

                        command1.Parameters.AddWithValue("@userid", txtUserID.Text)
                        command1.Connection = conn

                        command1.ExecuteNonQuery()

                        lblMessage.Text = "You have made " + count.ToString() + " unsuccessful attempts. You have " + (10 - count).ToString + " remaining attempts."
                        command1.Dispose()

                    End If



                    MessageBox.Message.Alert(Me, "Login Failed!!!", "Keystr")
                    Return
                End If
            Else

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text
                command2.CommandText = "SELECT SecWebPassword, StaffID, Name, SecGroupAuthority,loginattempt,statusRemarks,status FROM tblstaff where SecWebLoginID = @userid and SystemUser='Y';"
                command2.Parameters.AddWithValue("@userid", txtUserID.Text)
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    If dt2.Rows(0)("status").ToString = "D" Then
                        lblMessage.Text = "Your Account is disabled. " + dt2.Rows(0)("statusremarks").ToString

                    End If
                End If

                MessageBox.Message.Alert(Me, "Login Failed!!!", "Keystr")
                txtUserID.Text = ""
                txtPassword.Text = ""
                command2.Dispose()
                Return
            End If


            dr.Close()

            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()

        Catch ex As Exception
            'lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("LOGIN - " + Session("UserID"), "btnLogin_Click", ex.Message.ToString, "")
            Exit Sub
        End Try


    End Sub

    Public Sub IsDisplayRecordsLocationwise()
        Try
            Dim IsLock As String
            IsLock = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            commandServiceRecordMasterSetup.CommandText = "SELECT showSConScreenLoad, ServiceContractMaxRec,DisplayRecordsLocationWise FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("ServiceContractMaxRec")
            'txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString


            If dtServiceRecordMasterSetup.Rows.Count > 0 Then
                'MsgBox(dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString)
                Session.Add("Locationwise", dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString)
                'txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
            End If


            conn.Close()
            conn.Dispose()
            commandServiceRecordMasterSetup.Dispose()
            dtServiceRecordMasterSetup.Dispose()
        Catch ex As Exception
            'InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "IsDisplayRecordsLocationwise", ex.Message.ToString, "")
        End Try
    End Sub
    Public Sub InsertIntoTblWebEventLog(LoginID As String, events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", LoginID)
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
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        txtUserID.Text = ""
        txtPassword.Text = ""

    End Sub

    Private Shared random As New Random()
    Public Shared Function RandomString(chars As String, length As Integer) As String
        '    Const chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        Return New String(Enumerable.Repeat(chars, length).[Select](Function(s) s(random.[Next](s.Length))).ToArray())
    End Function

    Private Sub SendEmail(ToEmail As String, otp As String, StaffName As String)
        Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
        Dim oSmtp As New SmtpClient()

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text

        command.Connection = conn

        command.CommandText = "Select * from tblEmailSetUp where SetUpID='LOGIN OTP'"
        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        Dim subject As String = ""
        Dim content As String = ""

        If dt.Rows.Count > 0 Then
            subject = dt.Rows(0)("Subject").ToString
            content = dt.Rows(0)("Contents").ToString

        End If


        content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
        content = content.Replace("StaffName", StaffName)
        content = content.Replace("Domain", lblDomainName.Text)
        content = content.Replace("OTPNO", otp)
        content = content.Replace("SYSTIME", lblSysTime.Text)

        dt.Clear()
        dr.Close()
        conn.Close()
        conn.Dispose()

        oMail.From = ConfigurationManager.AppSettings("SecureEmailFrom").ToString()
        '  oMail.Subject = "ANTICIMEX LOGIN OTP"
        '  oMail.HtmlBody = "Hi " + StaffName + ",<br/><br/>Welcome to " + lblDomainName.Text + ".<br/>Your One-Time Password(OTP) is : " + otp + "<br/>Take note that this OTP is valid only for 10 minutes.<br/>Time Generated: " + lblSysTime.Text + "<br/><br/>Thank You.<br/><br/>-AOL Secure Login."

        oMail.Subject = subject
        oMail.HtmlBody = content

        'Dim pattern As String
        'pattern = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

        'If String.IsNullOrEmpty(txtTo.Text) = False Then
        '    Dim ToAddress As String() = txtTo.Text.Split(";"c)
        '    If ToAddress.Count() > 0 Then
        '        For i As Integer = 0 To ToAddress.Count() - 1
        '            If Regex.IsMatch(ToAddress(i).ToString(), pattern) Then

        '            Else
        '                MessageBox.Message.Alert(Page, "Enter valid 'TO' Email address" + " (" + ToAddress(i).ToString() + ")", "str")
        '                Return
        '            End If
        '            oMail.[To].Add(New MailAddress(ToAddress(i).ToString()))
        '        Next
        '    End If
        'End If

        oMail.To = ToEmail

        Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
        oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
        oServer.ConnectType = SmtpConnectType.ConnectDirectSSL
        oServer.User = ConfigurationManager.AppSettings("SecureEmailFrom").ToString()
        oServer.Password = ConfigurationManager.AppSettings("SecureEmailPassword").ToString()

        oSmtp.SendMail(oServer, oMail)
        oSmtp.Close()

    End Sub

    'Private Sub SendSMS(ToMobile As String, otp As String)
    '    Dim res As rapidsms.ResultCodes = New rapidsms.ResultCodes()

    '    Dim myapi As rapidsms.mySMS_SQLSoapClient = New rapidsms.mySMS_SQLSoapClient()

    '    myapi.SendSMS2("APM.Admin", "PowerToSMS0771+-", ToMobile, txtlog, "+6582920371", txtMessageSMS.Text, "EWAPI", "", "t1", 0, 3)

    'End Sub

    Private Sub CheckSMSQuota(conn As MySqlConnection, ToEmail As String)
        If lblDomainName.Text = "SINGAPORE-NEW" Then
            If txtUserID.Text = "ADMIN" Then
                '  Dim res As rapidsms.ResultCodes = New rapidsms.ResultCodes()
                ' Dim res As rapidsms.ProcessingResult = New rapidsms.ResultCodes()
                Dim myapi As rapidsms.mySMS_SQLSoapClient = New rapidsms.mySMS_SQLSoapClient()

                Dim i As Integer = myapi.CheckQuota("APM.Admin", "PowerToSMS0771+-")

                InsertIntoTblWebEventLog("LOGIN", "SMS - CHECK QUOTA", i.ToString(), txtUserID.Text)

                If i < 1000 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    command.CommandText = "SELECT * FROM tblwebeventlog WHERE EVENT='EMAIL SMS QUOTA' AND DATE(CREATEDON)=CURDATE();"
                    command.Parameters.AddWithValue("@userid", txtUserID.Text)
                    command.Connection = conn

                    Dim dr As MySqlDataReader = command.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count = 0 Then

                        InsertIntoTblWebEventLog("LOGIN", "EMAIL SMS QUOTA", ToEmail, txtUserID.Text)


                        Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
                        Dim oSmtp As New SmtpClient()

                        oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()
                        oMail.Subject = "SMS QUOTA LOW"
                        oMail.HtmlBody = "Hi " + txtUserID.Text + ",<br/><br/> The SMS QUOTA is running low. Please assist to top up.<br/><br/>Thank You.<br/><br/>-AOL."

                        oMail.To = ToEmail
                        oMail.Bcc = "sasi.vishwa@gmail.com"
                        Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
                        oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
                        oServer.ConnectType = SmtpConnectType.ConnectDirectSSL
                        oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                        oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

                        oSmtp.SendMail(oServer, oMail)
                        oSmtp.Close()
                    End If

                End If
            End If
        End If
    End Sub

End Class
