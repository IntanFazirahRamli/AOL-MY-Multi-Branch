Imports System.IO
Imports System.Threading
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports EASendMail

Public Class Service1

    Private Timer As System.Timers.Timer = New System.Timers.Timer()
    Public enGB As CultureInfo = New CultureInfo("en-GB")

    Protected Overrides Sub OnStart(ByVal args() As String)
        Me.WriteToFile("Schedule Service started at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
        AddHandler Timer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Timer_Elapsed)
        Timer.Interval = 6000 '1 minute (1*60*1000)
        Timer.Enabled = True
        Timer.Start()
        '  Me.ScheduleService()
    End Sub

    Protected Overrides Sub OnStop()

        Try
            Timer.[Stop]()
            Timer.Enabled = False
        Catch ex As Exception
            Me.WriteToFile("Schedule Service stopped at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
        End Try
        '  Me.Schedular.Dispose()
    End Sub

    Private Sub Timer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Try
            Timer.[Stop]()
            Timer.Enabled = False
            ' Me.WriteToFile("Timer_Elapsed : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))

            Try
                Dim conn As MySqlConnection = New MySqlConnection()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                ScheduleService()

                conn.Close()


            Catch ex As Exception
                InsertIntoTblWebEventLog("Timer_Elapsed", ex.Message.ToString, "")
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try


            Timer.Enabled = True
            Timer.Start()
        Catch ex As Exception
            WriteToFile("Method Timer_Elapsed ~ " & DateTime.Now.ToString() & " ~ " + ex.Message)
        Finally
            Timer.Enabled = True
            Timer.Start()
        End Try
    End Sub

    '   Private Schedular As Timer

    Public Sub ScheduleService()
        '   Me.WriteToFile("Schedule Service at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
        Dim ContractNo As String = ""
        Dim Staff As String = ""
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()



            Dim command4 As New MySqlCommand
            command4.CommandType = CommandType.Text
            command4.Connection = conn
            command4.CommandText = "Select * from tbwserviceschedulegenerate where Generated=0 order by rcno desc"

            Dim dr4 As MySqlDataReader = command4.ExecuteReader
            Dim dt4 As New DataTable
            dt4.Load(dr4)

            If dt4.Rows.Count > 0 Then
                ContractNo = dt4.Rows(0)("ContractNo").ToUpper
                Me.WriteToFile("ContractNo Started :  " + ContractNo + " at " + DateTime.Now.ToString)
                Staff = dt4.Rows(0)("CreatedBy")

                Dim commandInsertIntoTblServiceRecord As MySqlCommand = New MySqlCommand
                commandInsertIntoTblServiceRecord.Connection = conn

                commandInsertIntoTblServiceRecord.CommandType = CommandType.StoredProcedure
                commandInsertIntoTblServiceRecord.CommandText = "SaveServiceScheduleToServiceNew"
                commandInsertIntoTblServiceRecord.CommandTimeout = 0

                commandInsertIntoTblServiceRecord.Parameters.Clear()
                commandInsertIntoTblServiceRecord.Parameters.AddWithValue("@pr_AccountId", dt4.Rows(0)("AccountID").ToUpper)
                commandInsertIntoTblServiceRecord.Parameters.AddWithValue("@pr_ContactType", dt4.Rows(0)("ContactType").ToUpper)
                commandInsertIntoTblServiceRecord.Parameters.AddWithValue("@pr_ContractNo", dt4.Rows(0)("ContractNo").ToUpper)
                commandInsertIntoTblServiceRecord.Parameters.AddWithValue("@pr_CustName", dt4.Rows(0)("CustName").ToUpper)
                commandInsertIntoTblServiceRecord.Parameters.AddWithValue("@pr_CompanyGroup", dt4.Rows(0)("CompanyGroup"))
                commandInsertIntoTblServiceRecord.Parameters.AddWithValue("@pr_UserID", dt4.Rows(0)("CreatedBy"))
                commandInsertIntoTblServiceRecord.Connection = conn
                commandInsertIntoTblServiceRecord.ExecuteNonQuery()
                commandInsertIntoTblServiceRecord.Dispose()

                'Update Schedule Generated

                Dim command As MySqlCommand = New MySqlCommand
                command.Connection = conn
                command.CommandType = CommandType.Text

                command.CommandText = "update tbwserviceschedulegenerate set Generated=1,ScheduleGeneratedOn=@ScheduleGeneratedOn where rcno=" & Convert.ToInt32(dt4.Rows(0)("RcNo"))
                '    InsertIntoTblWebEventLog("AUTOSERVICESCHEDULE", "update tbwserviceschedulegenerate set Generated=1,ScheduleGeneratedOn=@ScheduleGeneratedOn where rcno=" & Convert.ToInt32(dt4.Rows(0)("RcNo")), ContractNo)

                command.Parameters.Clear()
                command.Parameters.AddWithValue("@ScheduleGeneratedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                command.ExecuteNonQuery()

                command.Dispose()


                Dim lServiceNo1 As Integer

                lServiceNo1 = 0

                Dim commandMaxServiceDate1 As MySqlCommand = New MySqlCommand
                commandMaxServiceDate1.CommandType = CommandType.Text
                'commandMaxServiceDate.CommandText = "SELECT count(distinct(ServiceDate)) as totservices FROM tbwserviceschedule where BatchNo='" & txtBatchNo.Text & "'"
                commandMaxServiceDate1.CommandText = "SELECT count(distinct(ServiceDate)) as 'totservices' FROM tbwserviceschedule where ContractNo='" & ContractNo & "'"
                commandMaxServiceDate1.Connection = conn

                Dim drMaxServiceDate1 As MySqlDataReader = commandMaxServiceDate1.ExecuteReader()
                Dim dtMaxServiceDate1 As New DataTable
                dtMaxServiceDate1.Load(drMaxServiceDate1)

                If dtMaxServiceDate1.Rows.Count > 0 Then
                    lServiceNo1 = Convert.ToInt32(dtMaxServiceDate1.Rows(0)("totservices").ToString)
                End If

                InsertIntoTblWebEventLog("SERVICE SCHEDULE_NEW - " + dt4.Rows(0)("CreatedBy"), "btnGenerateServiceSchedule_Click", "3-New")


                Dim commandUpdGS As MySqlCommand = New MySqlCommand
                commandUpdGS.CommandType = CommandType.Text

                If dt4.Rows(0)("DurationType").ToString = "FIXED" Then
                    commandUpdGS.CommandText = "Update tblcontract set GSt='P', ServiceNo= " & lServiceNo1 & ", ServiceBal = " & lServiceNo1 & " where ContractNo = '" & ContractNo & "'"
                Else
                    If dt4.Rows(0)("TotalServiceRecord").ToString = 0 Then
                        commandUpdGS.CommandText = "Update tblcontract set GSt='P', ServiceNo= " & lServiceNo1 & ", ServiceBal = " & lServiceNo1 & " where ContractNo = '" & ContractNo & "'"
                    Else
                        commandUpdGS.CommandText = "Update tblcontract set GSt='P', ServiceNo= " & lServiceNo1 & ", ServiceBal = " & lServiceNo1 & ", ServiceStart= '" & Convert.ToDateTime(dt4.Rows(0)("ServiceStart")).ToString("yyyy-MM-dd") & "', EndOfLastSchedule = '" & Convert.ToDateTime(dt4.Rows(0)("ServiceEnd")).ToString("yyyy-MM-dd") & "'  where ContractNo = '" & ContractNo & "'"
                    End If
                End If

                commandUpdGS.Connection = conn
                commandUpdGS.ExecuteNonQuery()

                commandUpdGS.Dispose()
                commandMaxServiceDate1.Dispose()
                dtMaxServiceDate1.Dispose()
                drMaxServiceDate1.Close()


                ''''Retrieve Staff Details''
                Dim commandStaff As MySqlCommand = New MySqlCommand
                commandStaff.CommandType = CommandType.Text
                commandStaff.CommandText = "SELECT StaffID, Name, EmailPerson FROM tblstaff where StaffID = @userid;"
                commandStaff.Parameters.AddWithValue("@userid", dt4.Rows(0)("CreatedBy").ToString)
                commandStaff.Connection = conn

                Dim drStaff As MySqlDataReader = commandStaff.ExecuteReader()
                Dim dtStaff As New DataTable
                dtStaff.Load(drStaff)
                Dim ToEmail As String = ""
                Dim StaffName As String = ""

                If dtStaff.Rows.Count > 0 Then

                    '  Dim ToEmail As String = "Christian.Reyes@anticimex.com.sg;sasi.vishwa@gmail.com"
                    ' ToEmail = "sasi.vishwa@gmail.com"
                    ToEmail = dtStaff.Rows(0)("EmailPerson")
                    StaffName = dtStaff.Rows(0)("Name")
                End If

                commandStaff.Dispose()
                dtStaff.Clear()
                dtStaff.Dispose()
                drStaff.Close()

                '                The Schedule for the following contract is generated successfully.
                'Contract No: [Contract No.]
                'Account ID: [Account ID]
                'Name:           [CustName]()
                '                Service Address : [ServiceAddress]()
                'Duration: [Duration] [Duration MS]
                'Start Date: [StartDate]
                'End Date: [End Date]

                Dim content As String = "Hi " + StaffName + ",<br/><br/>The Schedule for the following contract is generated successfully."
                content = content + "<br/> Contract No : <b>" + ContractNo + "</b> "
                content = content + "<br/>Account ID : <b>" + dt4.Rows(0)("AccountID").ToUpper + "</b> "
                content = content + "<br/>Name : <b>" + dt4.Rows(0)("CustName").ToUpper + "</b>  "
                content = content + "<br/>Service Address : <b>" + dt4.Rows(0)("ServiceAddress").ToUpper + "</b>  "
                content = content + "<br/>Duration : <b>" + dt4.Rows(0)("Duration").ToUpper + " " + dt4.Rows(0)("DurationMs").ToUpper + "</b> "
                content = content + "<br/>Start Date : <b>" + dt4.Rows(0)("Servicestart") + "</b>  "
                content = content + "<br/>End Date : <b>" + dt4.Rows(0)("ServiceEnd") + "</b>  "
                content = content + "<br/><br/>Thank You.<br/><br/>-AOL."

                SendEmailNotification(ToEmail, "SERVICE SCHEDULE GENERATED NOTIFICATION", StaffName, content, ContractNo)


                command4.Dispose()
                dt4.Clear()
                dr4.Close()
                dt4.Dispose()
                Me.WriteToFile("ContractNo Ended :  " + ContractNo + " at " + DateTime.Now.ToString)
            End If


            conn.Close()
            conn.Dispose()

            'Stop the Windows Service.
            '    Dim svc As System.ServiceProcess.ServiceController = New System.ServiceProcess.ServiceController("AutoScheduleService")
            ' Me.Stop()

            '    svc.Stop()

            'Using serviceController As New System.ServiceProcess.ServiceController("AutoScheduleService")
            '    serviceController.[Stop]()
            'End Using

        Catch ex As Exception
            WriteToFile("Schedule Service Error on: {0} " + ex.Message + ex.StackTrace)
            InsertIntoTblWebEventLog("AUTOSERVICE SCHEDULE_NEW", "btnGenerateServiceSchedule_Click", ContractNo)
            '   Dim ToEmail As String = "Christian.Reyes@anticimex.com.sg;sasi.vishwa@gmail.com"
            Dim ToEmail As String = "sasi.vishwa@gmail.com"
            Dim content As String = "Hi CHRISTIAN" + ",<br/><br/>Error Generating Service Records for the Contract No : "
            content = content + "<b>" + ContractNo + ".</b><br/>Error : " + ex.Message.ToString + "<br/>Time Generated : " + DateTime.Now.ToString

            content = content + "<br/><br/>Thank You.<br/><br/>-AOL."
            SendEmailNotification(ToEmail, "ERROR : SERVICE SCHEDULE GENERATION", Staff, content, ContractNo)
            'Stop the Windows Service.
            Using serviceController As New System.ServiceProcess.ServiceController("AutoScheduleService")
                serviceController.[Stop]()
            End Using
        End Try
    End Sub

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn1 As New MySqlConnection()
            conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "AUTOEMAIL")
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn1.Open()
            insCmds.Connection = conn1
            insCmds.ExecuteNonQuery()
            conn1.Close()
            conn1.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, ID)
        End Try
    End Sub



    Private Sub WriteToFile(text As String)
        Dim path As String = "C:\AutoServiceSchedule\LogError\AutoServiceScheduleException.txt"
        Using writer As New StreamWriter(path, True)
            writer.WriteLine(String.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")))
            writer.Close()
        End Using
    End Sub

    Private Sub SendEmailNotification(ToEmail As String, subject As String, StaffName As String, content As String, ContractNo As String)
        Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
        Dim oSmtp As New SmtpClient()

        ToEmail = ValidateEmail(ToEmail)

        If ToEmail.Last.ToString = ";" Then
            ToEmail = ToEmail.Remove(ToEmail.Length - 1)
        End If

        If ToEmail.First.ToString = ";" Then
            ToEmail = ToEmail.Remove(0)
        End If

        Dim pattern As String
        pattern = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

        If String.IsNullOrEmpty(ToEmail) = False Then
            Dim ToAddress As String() = ToEmail.Split(";"c)
            If ToAddress.Count() > 0 Then
                For i As Integer = 0 To ToAddress.Count() - 1
                    If Regex.IsMatch(ToAddress(i).ToString.Trim, pattern) Then

                    Else
                        InsertIntoTblWebEventLog("SendEmail", "INVALID EMAIL ADDRESS", ContractNo)
                        '   MessageBox.Message.Alert(Page, "Enter valid 'TO' Email address" + " (" + ToAddress(i).ToString() + ")", "str")
                        Return
                    End If
                    oMail.[To].Add(New MailAddress(ToAddress(i).ToString.Trim))
                Next
            End If
        End If

        oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()
        oMail.Subject = subject
        oMail.HtmlBody = content

        Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
        oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
        oServer.ConnectType = SmtpConnectType.ConnectDirectSSL
        oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
        oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

        oSmtp.SendMail(oServer, oMail)
        oSmtp.Close()

    End Sub

    Protected Function ValidateEmail(ByVal EmailId As String) As String
        Dim resEmail As String = ""
        If EmailId.Contains(","c) Then EmailId = EmailId.Replace(","c, ";"c)
        If EmailId.Contains("/"c) Then EmailId = EmailId.Replace("/"c, ";"c)
        If EmailId.Contains(":"c) Then EmailId = EmailId.Replace(":"c, ";"c)
        resEmail = EmailId.TrimEnd(";"c)
        Return resEmail
    End Function
End Class

