
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Partial Class RV_Select_ManpowerProdTeamMemberDetail
    Inherits System.Web.UI.Page

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.SelectedIndex = 0
        'ddlIncharge.SelectedIndex = 0
        txtIncharge.Text = ""
        ddlCompanyGrp.SelectedIndex = 0

    End Sub

    'Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click

    '    '    Dim stopwatch As Stopwatch = stopwatch.StartNew()
    '    Try
    '        Dim qrySvcRec As String = "SELECT RecordNo,VehNo,ServiceDate,Duration,AccountID,CustName,TimeIn,TimeOut,BillAmount,location,ContractNo,Status,CompanyGroup,LocateGrp FROM tblservicerecord where status='P'"
    '        Dim selection As String
    '        selection = ""
    '        '   InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "1")

    '        Dim selFormula As String = ""
    '        If rdbSelect.Text = "Detail" Then
    '            selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberDet' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

    '            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '                qrySvcRec = qrySvcRec + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"

    '                If selection = "" Then
    '                    selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
    '                Else
    '                    selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
    '                End If
    '            End If

    '        ElseIf rdbSelect.Text = "Summary" Then
    '            selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberSummary' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

    '            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '                selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
    '                qrySvcRec = qrySvcRec + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"

    '                If selection = "" Then
    '                    selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
    '                Else
    '                    selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
    '                End If
    '            End If
    '        ElseIf rdbSelect.Text = "SummaryByTeamMember" Then

    '            selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberSummary' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

    '            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
    '                selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
    '                qrySvcRec = qrySvcRec + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"

    '                If selection = "" Then
    '                    selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
    '                Else
    '                    selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
    '                End If
    '            End If
    '        End If


    '        ' InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "2")


    '        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
    '            Dim d As DateTime
    '            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

    '            Else
    '                ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
    '                lblAlert.Text = "INVALID SERVICE DATE FROM"
    '                Return
    '            End If

    '            qrySvcRec = qrySvcRec + " and ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
    '            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

    '            If selection = "" Then
    '                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
    '            Else
    '                selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
    '            End If

    '        End If
    '        '   InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "3")

    '        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
    '            Dim d As DateTime
    '            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

    '            Else
    '                '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
    '                lblAlert.Text = "INVALID SERVICE DATE TO"
    '                Return
    '            End If
    '            qrySvcRec = qrySvcRec + " and ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
    '            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

    '            If selection = "" Then
    '                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
    '            Else
    '                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
    '            End If
    '        End If
    '        '   InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "4")


    '        If txtServiceID.Text = "-1" Then
    '        Else

    '            selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} = '" + txtServiceID.SelectedItem.Text + "'"
    '            If selection = "" Then
    '                selection = "ServiceID = " + txtServiceID.SelectedItem.Text
    '            Else
    '                selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
    '            End If
    '        End If


    '        'If ddlIncharge.Text = "-1" Then
    '        'Else
    '        '    selFormula = selFormula + " and {tblrptserviceanalysis1.StaffID} = '" + ddlIncharge.Text + "'"


    '        '    'qrySvcRec = qrySvcRec + " and ServiceBy = '" + ddlIncharge.Text + "'"
    '        '    qrySvcRec = qrySvcRec + " and recordno in (SELECT RecordNo FROM tblservicerecordstaff where staffid = '" + ddlIncharge.Text.Trim + "')"

    '        '    If selection = "" Then
    '        '        selection = "ServiceBy : " + ddlIncharge.Text
    '        '    Else
    '        '        selection = selection + ", ServiceBy : " + ddlIncharge.Text
    '        '    End If
    '        'End If

    '        If String.IsNullOrEmpty(txtIncharge.Text) = True Then
    '        Else
    '            selFormula = selFormula + " and {tblrptserviceanalysis1.StaffID} = '" + txtIncharge.Text + "'"

    '            'qrySvcRec = qrySvcRec + " and ServiceBy = '" + txtIncharge.Text + "'"
    '            qrySvcRec = qrySvcRec + " and recordno in (SELECT RecordNo FROM tblservicerecordstaff where staffid = '" + txtIncharge.Text.Trim + "')"

    '            If selection = "" Then
    '                selection = "ServiceBy : " + txtIncharge.Text
    '            Else
    '                selection = selection + ", ServiceBy : " + txtIncharge.Text
    '            End If
    '        End If

    '        'If ddlCompanyGrp.Text = "-1" Then
    '        'Else
    '        '    qrySvcRec = qrySvcRec + " and CompanyGroup = '" + ddlCompanyGrp.Text + "'"
    '        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"

    '        '    If selection = "" Then
    '        '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
    '        '    Else
    '        '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
    '        '    End If

    '        'End If

    '        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

    '        For Each item As ListItem In ddlCompanyGrp.Items
    '            If item.Selected Then

    '                YrStrList1.Add("""" + item.Value + """")

    '            End If
    '        Next

    '        If YrStrList1.Count > 0 Then

    '            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
    '            selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"

    '            If selection = "" Then
    '                selection = "CompanyGroup : " + YrStr
    '            Else
    '                selection = selection + ", CompanyGroup : " + YrStr
    '            End If
    '            qrySvcRec = qrySvcRec + " and tblservicerecord.CompanyGroup in (" + YrStr + ")"

    '        End If

    '        Dim YrStrListZone As List(Of [String]) = New List(Of String)()

    '        For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
    '            If item.Selected Then

    '                YrStrListZone.Add("""" + item.Value + """")

    '            End If
    '        Next

    '        If YrStrListZone.Count > 0 Then

    '            Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)


    '            selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
    '            If selection = "" Then
    '                selection = "Zone : " + YrStrZone
    '            Else
    '                selection = selection + ", Zone : " + YrStrZone
    '            End If
    '            qrySvcRec = qrySvcRec + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"
    '        End If

    '        '  InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "5")


    '        Dim conn As MySqlConnection = New MySqlConnection()

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        Dim cmdSvcRec As MySqlCommand = New MySqlCommand
    '        cmdSvcRec.CommandType = CommandType.Text
    '        cmdSvcRec.CommandText = qrySvcRec
    '        cmdSvcRec.Connection = conn

    '        ' InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test3", "")

    '        Dim drSvcRec As MySqlDataReader = cmdSvcRec.ExecuteReader()
    '        Dim dtSvcRec As New DataTable
    '        dtSvcRec.Load(drSvcRec)

    '        cmdSvcRec.Dispose()

    '        '  InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test2", "")


    '        If dtSvcRec.Rows.Count > 0 Then

    '            '    InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test", dtSvcRec.Rows.Count.ToString)

    '            Dim command2 As MySqlCommand = New MySqlCommand

    '            command2.CommandType = CommandType.Text

    '            If rdbSelect.Text = "Detail" Then
    '                command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberDet';"

    '            ElseIf rdbSelect.Text = "Summary" Then
    '                command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberSummary';"
    '            ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
    '                command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberSummary';"

    '            End If


    '            command2.Connection = conn

    '            command2.ExecuteNonQuery()
    '            command2.Dispose()

    '            ' InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test - " + dtSvcRec.Rows.Count.ToString, "")



    '            For i As Integer = 0 To dtSvcRec.Rows.Count - 1

    '                Dim cmdSvcRecStaff As MySqlCommand = New MySqlCommand

    '                cmdSvcRecStaff.CommandType = CommandType.Text

    '                cmdSvcRecStaff.CommandText = "Select count(staffid),staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


    '                cmdSvcRecStaff.Connection = conn

    '                Dim drSvcRecStaff As MySqlDataReader = cmdSvcRecStaff.ExecuteReader()
    '                Dim dtSvcRecStaff As New DataTable
    '                dtSvcRecStaff.Load(drSvcRecStaff)

    '                cmdSvcRecStaff.Dispose()

    '                'Dim staffid As String = ""
    '                'If dtSvcRecStaff.Rows.Count > 0 Then
    '                '    For k As Integer = 0 To dtSvcRecStaff.Rows.Count - 1
    '                '        If staffid <> "" Then
    '                '            staffid = staffid + "," + dtSvcRecStaff.Rows(k)("Staffid").ToString
    '                '        End If
    '                '    Next
    '                'End If
    '                Dim cmdSvcRecDet As MySqlCommand = New MySqlCommand

    '                cmdSvcRecDet.CommandType = CommandType.Text

    '                If txtServiceID.Text = "-1" Then
    '                    cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"
    '                Else
    '                    cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "' and serviceid='" + txtServiceID.SelectedItem.Text + "'"

    '                End If

    '                '     InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test1", "")

    '                cmdSvcRecDet.Connection = conn

    '                Dim drSvcRecDet As MySqlDataReader = cmdSvcRecDet.ExecuteReader()
    '                Dim dtSvcRecDet As New DataTable
    '                dtSvcRecDet.Load(drSvcRecDet)
    '                Dim targetid As String = ""
    '                Dim servicecost As Decimal = 0

    '                If dtSvcRecDet.Rows.Count > 0 Then

    '                    For j As Integer = 0 To dtSvcRecDet.Rows.Count - 1
    '                        If dtSvcRecDet.Rows(j)("TargetID").ToString <> DBNull.Value.ToString Then
    '                            If targetid = "" Then
    '                                targetid = dtSvcRecDet.Rows(j)("TargetID")
    '                            Else

    '                                targetid = targetid + "," + dtSvcRecDet.Rows(j)("TargetID")
    '                            End If
    '                        End If

    '                        If dtSvcRecDet.Rows(j)("ServiceCost").ToString = DBNull.Value.ToString Then
    '                        Else

    '                            servicecost = servicecost + dtSvcRecDet.Rows(j)("ServiceCost")

    '                        End If

    '                    Next
    '                End If
    '                cmdSvcRecDet.Dispose()

    '                ''''''Delete records that exists already
    '                'Dim command1 As MySqlCommand = New MySqlCommand

    '                'command1.CommandType = CommandType.Text

    '                'command1.CommandText = "SELECT * FROM tblrptserviceanalysis where recordno='" + dtSvcRec.Rows(i)("RecordNo") + "'"
    '                'command1.Connection = conn

    '                'Dim dr As MySqlDataReader = command1.ExecuteReader()
    '                'Dim dt As New DataTable
    '                'dt.Load(dr)

    '                'If dt.Rows.Count > 0 Then




    '                'End If

    '                Dim cmdSvcRecStaff1 As MySqlCommand = New MySqlCommand

    '                cmdSvcRecStaff1.CommandType = CommandType.Text

    '                cmdSvcRecStaff1.CommandText = "Select staffid,staffname from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


    '                cmdSvcRecStaff1.Connection = conn

    '                Dim drSvcRecStaff1 As MySqlDataReader = cmdSvcRecStaff1.ExecuteReader()
    '                Dim dtSvcRecStaff1 As New DataTable
    '                dtSvcRecStaff1.Load(drSvcRecStaff1)

    '                '  InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test2", "")

    '                '''''''''''''''''''''''''''''''''''''''''
    '                'Retrieve ContractGroup from tblcontract
    '                '''''''''''''''''''''''''''''''''''''''''

    '                Dim cmdContract As MySqlCommand = New MySqlCommand

    '                cmdContract.CommandType = CommandType.Text

    '                cmdContract.CommandText = "Select contractgroup from tblcontract where contractno='" + dtSvcRec.Rows(i)("Contractno").ToString + "'"


    '                cmdContract.Connection = conn

    '                Dim drContract As MySqlDataReader = cmdContract.ExecuteReader()
    '                Dim dtContract As New DataTable
    '                dtContract.Load(drContract)



    '                If dtSvcRecStaff1.Rows.Count > 0 Then
    '                    For j As Integer = 0 To dtSvcRecStaff1.Rows.Count - 1

    '                        Dim command As MySqlCommand = New MySqlCommand

    '                        command.CommandType = CommandType.Text
    '                        Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report,Status,CompanyGroup,LocateGrp)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report,@Status,@CompanyGroup,@LocateGrp);"
    '                        command.CommandText = qry
    '                        command.Parameters.Clear()
    '                        command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
    '                        command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)
    '                        'If txtActSvcDate.Text = "" Then
    '                        '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
    '                        'Else
    '                        '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
    '                        'End If
    '                        command.Parameters.AddWithValue("@ServiceDate", dtSvcRec.Rows(i)("ServiceDate"))
    '                        command.Parameters.AddWithValue("@NoPerson", dtSvcRecStaff.Rows(0)("count(staffid)"))
    '                        command.Parameters.AddWithValue("@Duration", dtSvcRec.Rows(i)("Duration"))
    '                        command.Parameters.AddWithValue("@AccountID", dtSvcRec.Rows(i)("AccountID").ToString)
    '                        command.Parameters.AddWithValue("@Client", dtSvcRec.Rows(i)("CustName").ToString)
    '                        command.Parameters.AddWithValue("@TimeIn", dtSvcRec.Rows(i)("TimeIn"))
    '                        command.Parameters.AddWithValue("@TimeOut", dtSvcRec.Rows(i)("Timeout"))
    '                        command.Parameters.AddWithValue("@Service", targetid)
    '                        command.Parameters.AddWithValue("@ServiceValue", dtSvcRec.Rows(i)("BillAmount"))
    '                        command.Parameters.AddWithValue("@ServiceCost", servicecost)
    '                        'If dtSvcRecStaff.Rows(0)("count(staffid)") <> 0 Then
    '                        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount") / dtSvcRecStaff.Rows(0)("count(staffid)"))
    '                        'Else
    '                        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount"))

    '                        'End If
    '                        command.Parameters.AddWithValue("@ManpowerCost", 0)

    '                        '  command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
    '                        '  command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID"))

    '                        command.Parameters.AddWithValue("@StaffID", dtSvcRecStaff1.Rows(j)("StaffID"))
    '                        command.Parameters.AddWithValue("@TeamID", dtSvcRecStaff1.Rows(j)("StaffName"))

    '                        command.Parameters.AddWithValue("@NormalHour", 0)
    '                        command.Parameters.AddWithValue("@OTHour", 0)
    '                        If dtContract.Rows.Count > 0 Then
    '                            command.Parameters.AddWithValue("@ServDateType", dtContract.Rows(0)("ContractGroup").ToString)
    '                        Else
    '                            command.Parameters.AddWithValue("@ServDateType", "")
    '                        End If

    '                        command.Parameters.AddWithValue("@OTRate", 0)
    '                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
    '                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                        If rdbSelect.Text = "Detail" Then
    '                            command.Parameters.AddWithValue("@Report", "ProdTeamMemberDet")
    '                        ElseIf rdbSelect.Text = "Summary" Then
    '                            command.Parameters.AddWithValue("@Report", "ProdTeamMemberSummary")
    '                        ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
    '                            command.Parameters.AddWithValue("@Report", "ProdTeamMemberSummary")

    '                        End If
    '                        command.Parameters.AddWithValue("@Status", dtSvcRec.Rows(i)("Status"))
    '                        command.Parameters.AddWithValue("@CompanyGroup", dtSvcRec.Rows(i)("CompanyGroup"))
    '                        command.Parameters.AddWithValue("@LocateGrp", dtSvcRec.Rows(i)("LocateGrp"))
    '                        command.Connection = conn

    '                        command.ExecuteNonQuery()
    '                        command.Dispose()


    '                    Next
    '                End If
    '                dtSvcRecDet.Clear()
    '                dtSvcRecDet.Dispose()
    '                drSvcRecDet.Close()

    '                dtSvcRecStaff.Clear()
    '                dtSvcRecStaff.Dispose()
    '                drSvcRecStaff.Close()

    '                dtSvcRecStaff1.Clear()
    '                dtSvcRecStaff1.Dispose()

    '                drSvcRecStaff1.Close()

    '            Next

    '        End If
    '        dtSvcRec.Clear()
    '        dtSvcRec.Dispose()

    '        drSvcRec.Close()
    '        conn.Close()
    '        conn.Dispose()

    '        'stopwatch.[Stop]()
    '        'InsertIntoTblWebEventLog("PrintTest", qrySvcRec.ToString, stopwatch.ElapsedMilliseconds.ToString, "TEST")

    '        Session.Add("selFormula", selFormula)
    '        Session.Add("selection", selection)

    '        If rdbSelect.SelectedValue = "Detail" Then
    '            Session.Add("ReportType", "ManpowerProductivityByTeamMemberDetail")
    '            Response.Redirect("RV_ManpowerProductivityByTeamMemberDetail.aspx")
    '        ElseIf rdbSelect.SelectedValue = "Summary" Then
    '            Session.Add("ReportType", "ManpowerProductivityByTeamMemberSummary")
    '            Response.Redirect("RV_ManpowerProductivityByTeamMemberSummary.aspx")
    '        ElseIf rdbSelect.SelectedValue = "SummaryByTeamMember" Then

    '            Session.Add("ReportType", "ManpowerProductivityByTeamMemberSummaryByTeamMember")
    '            Response.Redirect("RV_ManpowerProductivityByTeamMemberSummary.aspx")

    '        End If

    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", ex.Message.ToString, "")
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub GetData()

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'Dim command2 As MySqlCommand = New MySqlCommand

        'command2.CommandType = CommandType.Text

        'If rdbSelect.Text = "Detail" Then
        '    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberDet';"

        'ElseIf rdbSelect.Text = "Summary" Then
        '    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberSummary';"
        'ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
        '    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberSummary';"

        'End If


        'command2.Connection = conn

        'command2.ExecuteNonQuery()
        'command2.Dispose()

        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.StoredProcedure

        If chkTimeSheet.Checked Then
            If rdbDisplayDate.SelectedValue = "ScheduledDate" Then
                command.CommandText = "SaveProductivityByTeamMemberNew1TimeSheet"
            ElseIf rdbDisplayDate.SelectedValue = "ServiceDate" Then
                command.CommandText = "SaveProductivityByTeamMemberNewTimeSheet"
            End If

            InsertIntoTblWebEventLog("SaveProductivityByTeamMemberTimeSheet", "GetData", chkTimeSheet.Checked.ToString, txtCreatedBy.Text)
        Else
            If rdbDisplayDate.SelectedValue = "ScheduledDate" Then
                command.CommandText = "SaveProductivityByTeamMemberNew1"
            ElseIf rdbDisplayDate.SelectedValue = "ServiceDate" Then
                command.CommandText = "SaveProductivityByTeamMemberNew"
            End If

            InsertIntoTblWebEventLog("SaveProductivityByTeamMember", "GetData", chkTimeSheet.Checked.ToString, txtCreatedBy.Text)
        End If


            command.Parameters.Clear()

            command.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))

            command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
            If rdbSelect.Text = "Detail" Then
                command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberDet")
            ElseIf rdbSelect.Text = "Summary" Then
                command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberSummary")
            ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
                command.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberSummary")

            End If
            command.Connection = conn
            command.ExecuteScalar()

            command.Dispose()
            If chkDistribution.Checked = True Then

                Dim commandD As MySqlCommand = New MySqlCommand
                commandD.CommandType = CommandType.StoredProcedure
                commandD.CommandText = "SaveProductivityByTeamMemberDistribution"

                commandD.Parameters.Clear()

                commandD.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
                commandD.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))

                commandD.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
                If rdbSelect.Text = "Detail" Then
                    commandD.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberDet")
                ElseIf rdbSelect.Text = "Summary" Then
                    commandD.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberSummary")
                ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
                    commandD.Parameters.AddWithValue("@pr_Report", "ProdTeamMemberSummary")

                End If
                commandD.Connection = conn
                commandD.ExecuteScalar()

                commandD.Dispose()

            End If
            conn.Close()
            conn.Dispose()
    End Sub


    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click

        '    Dim stopwatch As Stopwatch = stopwatch.StartNew()
        Try

           
            'Dim qrySvcRec As String = "SELECT RecordNo,VehNo,ServiceDate,Duration,AccountID,CustName,TimeIn,TimeOut,BillAmount,location,ContractNo,Status,CompanyGroup,LocateGrp FROM tblservicerecord where status='P'"
            Dim qrySvcRec As String = "SELECT a.RecordNo,a.VehNo,a.ServiceDate,a.Duration,a.AccountID,a.CustName,a.TimeIn,a.TimeOut,a.BillAmount,"
            qrySvcRec = qrySvcRec + "a.location,a.ContractNo,a.Status,a.CompanyGroup,a.LocateGrp,c.contractgroup,"
            qrySvcRec = qrySvcRec + "(select ifnull(sum(b.servicecost),0) from tblservicerecorddet b where a.recordno=b.recordno group by b.recordno) as ServiceCost,"
            qrySvcRec = qrySvcRec + "(select group_concat(b.targetid) from tblservicerecorddet b where a.recordno=b.recordno group by b.recordno) as TargetID,"
            qrySvcRec = qrySvcRec + "(select COUNT(STAFFID) from tblservicerecordstaff s where a.recordno=s.recordno) AS NoofPerson"
            qrySvcRec = qrySvcRec + ",d.staffid as StaffID,d.staffname as StaffName FROM tblservicerecord a "
            qrySvcRec = qrySvcRec + " left join tblcontract c on a.contractno=c.contractno "
            qrySvcRec = qrySvcRec + " left join tblservicerecordstaff d on a.recordno=d.recordno WHERE A.STATUS='P'"




            Dim selection As String
            selection = ""
            '     InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "1")

            Dim selFormula As String = ""
            If rdbSelect.Text = "Detail" Then
                selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberDet' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    qrySvcRec = qrySvcRec + " and a.location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If

            ElseIf rdbSelect.Text = "Summary" Then
                selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberSummary' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                    qrySvcRec = qrySvcRec + " and a.location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If
            ElseIf rdbSelect.Text = "SummaryByTeamMember" Then

                selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' and {tblrptserviceanalysis1.Report}='ProdTeamMemberSummary' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                    qrySvcRec = qrySvcRec + " and a.location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If
            End If


            '    InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "2")


            If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                    lblAlert.Text = "INVALID SERVICE DATE FROM"
                    Return
                End If

                qrySvcRec = qrySvcRec + " and a.schServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
                selFormula = selFormula + "and {tblservicerecord1.schServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

                If selection = "" Then
                    selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
                End If

            End If
            InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "3")

            If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                    lblAlert.Text = "INVALID SERVICE DATE TO"
                    Return
                End If
                qrySvcRec = qrySvcRec + " and a.schServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
                selFormula = selFormula + "and {tblservicerecord1.schServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

                If selection = "" Then
                    selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
                End If
            End If
            '   InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "4")


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

            'If ddlIncharge.Text = "-1" Then
            'Else
            '    selFormula = selFormula + " and {tblrptserviceanalysis1.StaffID} = '" + ddlIncharge.Text + "'"


            '    'qrySvcRec = qrySvcRec + " and ServiceBy = '" + ddlIncharge.Text + "'"
            '    qrySvcRec = qrySvcRec + " and recordno in (SELECT RecordNo FROM tblservicerecordstaff where staffid = '" + ddlIncharge.Text.Trim + "')"

            '    If selection = "" Then
            '        selection = "ServiceBy : " + ddlIncharge.Text
            '    Else
            '        selection = selection + ", ServiceBy : " + ddlIncharge.Text
            '    End If
            'End If

            If String.IsNullOrEmpty(txtIncharge.Text) = True Then
            Else
                selFormula = selFormula + " and {tblrptserviceanalysis1.StaffID} = '" + txtIncharge.Text + "'"

                'qrySvcRec = qrySvcRec + " and ServiceBy = '" + txtIncharge.Text + "'"
                qrySvcRec = qrySvcRec + " and a.recordno in (SELECT RecordNo FROM tblservicerecordstaff where staffid = '" + txtIncharge.Text.Trim + "')"

                If selection = "" Then
                    selection = "ServiceBy : " + txtIncharge.Text
                Else
                    selection = selection + ", ServiceBy : " + txtIncharge.Text
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
                qrySvcRec = qrySvcRec + " and a.CompanyGroup in (" + YrStr + ")"

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
                qrySvcRec = qrySvcRec + " and a.LocateGrp in (" + YrStrZone + ")"
            End If

          


            GetData()

            ''   InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "5")

            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            ''Dim cmdSvcRec As MySqlCommand = New MySqlCommand
            ''cmdSvcRec.CommandType = CommandType.Text
            ''cmdSvcRec.CommandText = qrySvcRec
            ''cmdSvcRec.Connection = conn
            ' ''
            ' ''     InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test3", "")

            ''Dim drSvcRec As MySqlDataReader = cmdSvcRec.ExecuteReader()
            ''Dim dtSvcRec As New DataTable
            ''dtSvcRec.Load(drSvcRec)

            ''cmdSvcRec.Dispose()

            ' ''    InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test2", "")


            ''If dtSvcRec.Rows.Count > 0 Then

            ''    InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test", dtSvcRec.Rows.Count.ToString)

            ''    Dim command2 As MySqlCommand = New MySqlCommand

            ''    command2.CommandType = CommandType.Text

            ''    If rdbSelect.Text = "Detail" Then
            ''        command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberDet';"

            ''    ElseIf rdbSelect.Text = "Summary" Then
            ''        command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberSummary';"
            ''    ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
            ''        command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberSummary';"

            ''    End If


            ''    command2.Connection = conn

            ''    command2.ExecuteNonQuery()
            ''    command2.Dispose()

            ''    '  InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test - " + dtSvcRec.Rows.Count.ToString, "")



            ''    For i As Integer = 0 To dtSvcRec.Rows.Count - 1




            ''        Dim command As MySqlCommand = New MySqlCommand

            ''        command.CommandType = CommandType.Text
            ''        Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report,Status,CompanyGroup,LocateGrp)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report,@Status,@CompanyGroup,@LocateGrp);"
            ''        command.CommandText = qry
            ''        command.Parameters.Clear()
            ''        command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
            ''        command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)
            ''        'If txtActSvcDate.Text = "" Then
            ''        '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
            ''        'Else
            ''        '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
            ''        'End If
            ''        command.Parameters.AddWithValue("@ServiceDate", dtSvcRec.Rows(i)("ServiceDate"))
            ''        command.Parameters.AddWithValue("@NoPerson", dtSvcRec.Rows(i)("NoofPerson"))
            ''        command.Parameters.AddWithValue("@Duration", dtSvcRec.Rows(i)("Duration"))
            ''        command.Parameters.AddWithValue("@AccountID", dtSvcRec.Rows(i)("AccountID").ToString)
            ''        command.Parameters.AddWithValue("@Client", dtSvcRec.Rows(i)("CustName").ToString)
            ''        command.Parameters.AddWithValue("@TimeIn", dtSvcRec.Rows(i)("TimeIn"))
            ''        command.Parameters.AddWithValue("@TimeOut", dtSvcRec.Rows(i)("Timeout"))
            ''        command.Parameters.AddWithValue("@Service", dtSvcRec.Rows(i)("TargetID"))
            ''        command.Parameters.AddWithValue("@ServiceValue", dtSvcRec.Rows(i)("BillAmount"))
            ''        command.Parameters.AddWithValue("@ServiceCost", dtSvcRec.Rows(i)("ServiceCost"))
            ''        'If dtSvcRecStaff.Rows(0)("count(staffid)") <> 0 Then
            ''        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount") / dtSvcRecStaff.Rows(0)("count(staffid)"))
            ''        'Else
            ''        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount"))

            ''        'End If
            ''        command.Parameters.AddWithValue("@ManpowerCost", 0)

            ''        '  command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
            ''        '  command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID"))

            ''        command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("StaffID"))
            ''        command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("StaffName"))

            ''        command.Parameters.AddWithValue("@NormalHour", 0)
            ''        command.Parameters.AddWithValue("@OTHour", 0)
            ''        command.Parameters.AddWithValue("@ServDateType", dtSvcRec.Rows(i)("ContractGroup").ToString)


            ''        command.Parameters.AddWithValue("@OTRate", 0)
            ''        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
            ''        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            ''        If rdbSelect.Text = "Detail" Then
            ''            command.Parameters.AddWithValue("@Report", "ProdTeamMemberDet")
            ''        ElseIf rdbSelect.Text = "Summary" Then
            ''            command.Parameters.AddWithValue("@Report", "ProdTeamMemberSummary")
            ''        ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
            ''            command.Parameters.AddWithValue("@Report", "ProdTeamMemberSummary")

            ''        End If
            ''        command.Parameters.AddWithValue("@Status", dtSvcRec.Rows(i)("Status"))
            ''        command.Parameters.AddWithValue("@CompanyGroup", dtSvcRec.Rows(i)("CompanyGroup"))
            ''        command.Parameters.AddWithValue("@LocateGrp", dtSvcRec.Rows(i)("LocateGrp"))
            ''        command.Connection = conn

            ''        command.ExecuteNonQuery()
            ''        command.Dispose()



            ''    Next

            ''End If
            ''dtSvcRec.Clear()
            ''dtSvcRec.Dispose()

            ''drSvcRec.Close()



            'conn.Close()
            'conn.Dispose()

            ''stopwatch.[Stop]()
            ''InsertIntoTblWebEventLog("PrintTest", qrySvcRec.ToString, stopwatch.ElapsedMilliseconds.ToString, "TEST")

            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)

            If rdbSelect.SelectedValue = "Detail" Then
                Session.Add("ReportType", "ManpowerProductivityByTeamMemberDetail")
                Response.Redirect("RV_ManpowerProductivityByTeamMemberDetail.aspx")
            ElseIf rdbSelect.SelectedValue = "Summary" Then
                Session.Add("ReportType", "ManpowerProductivityByTeamMemberSummary")
                Response.Redirect("RV_ManpowerProductivityByTeamMemberSummary.aspx")
            ElseIf rdbSelect.SelectedValue = "SummaryByTeamMember" Then

                Session.Add("ReportType", "ManpowerProductivityByTeamMemberSummaryByTeamMember")
                Response.Redirect("RV_ManpowerProductivityByTeamMemberSummary.aspx")

            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", ex.Message.ToString, "")
            Exit Sub
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
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID

        txtCreatedOn.Attributes.Add("readonly", "readonly")

        SqlDSStaff.SelectCommand = "SELECT StaffId, Name, NRIC from tblstaff where roles='TECHNICAL' ORDER BY StaffId"
    End Sub

    Protected Sub ImgBtnInCharge_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnInCharge.Click
        Try
            'txtStaffSelect.Text = "SourceInCharge"
            txtPopupStaffSearch.Text = ""
            txtPopupStaff.Text = ""
            'updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtIncharge.Text.Trim) = False Then
                txtPopupStaffSearch.Text = txtIncharge.Text.Trim
                txtPopupStaff.Text = txtPopupStaffSearch.Text
                'updPanelMassChange1.Update()

                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where  1=1  and (upper(staffid) like '%" + txtPopupStaff.Text.Trim.ToUpper + "%' or upper(Name) Like '%" + txtPopupStaffSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            Else
                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where 1 =1  and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            End If
            mdlPopUpStaff.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "ImgBtnInCharge_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPopUpStaffSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffSearch.Click
        Try
            If txtPopupStaff.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter Name", "str")
            Else
                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where upper(staffid) like '%" + txtPopupStaff.Text.Trim.ToUpper + "%' or upper(Name) Like '%" + txtPopupStaff.Text.Trim.ToUpper + "%'"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
                mdlPopUpStaff.Show()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnPopUpStaffSearch_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtPopupStaff_TextChanged(sender As Object, e As EventArgs) Handles txtPopupStaff.TextChanged
        Try
            If txtPopupStaff.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter Staff name", "str")
            Else
                'SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where 1=1 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
                SqlDSStaff.SelectCommand = "SELECT  StaffId, Name, NRIC  From tblStaff where 1=1 and roles='TECHNICAL' and (upper(staffid) like '%" + txtPopupStaff.Text.Trim.ToUpper + "%' or Name like '%" + txtPopupStaff.Text.Trim.ToUpper + "%') and status <> 'N' order by Name"

                SqlDSStaff.DataBind()
                gvStaff.DataBind()
                mdlPopUpStaff.Show()
                'txtIsPopup.Text = "Staff"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "txtPopupStaff_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPopUpStaffReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffReset.Click
        Try
            txtPopupStaff.Text = "Search Here for Staff"
            SqlDSStaff.SelectCommand = "SELECT  StaffId, name, NRIC  From tblStaff where 1=1 and roles='TECHNICAL' and Status <> 'N' order by Name"
            SqlDSStaff.DataBind()
            gvStaff.DataBind()
            mdlPopUpStaff.Show()
            'txtIsPopup.Text = "Staff"
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnPopUpStaffReset_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvStaff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStaff.PageIndexChanging
        gvStaff.PageIndex = e.NewPageIndex
        If txtPopupStaff.Text.Trim = "" Then
            SqlDSStaff.SelectCommand = "SELECT  StaffId, name, NRIC  From tblStaff where 1=1 and roles='TECHNICAL' and Status <> 'N' order by Name"
            SqlDSStaff.DataBind()
            gvStaff.DataBind()
        Else
            SqlDSStaff.SelectCommand = "SELECT StaffId, name, NRIC From tblStaff where upper(staffid) like '%" + txtPopupStaff.Text.Trim.ToUpper + "%' or upper(Name) Like '%" + txtPopupStaff.Text.Trim.ToUpper + "%'"
            SqlDSStaff.DataBind()
            gvStaff.DataBind()
            mdlPopUpStaff.Show()
        End If
        mdlPopUpStaff.Show()

    End Sub

    Protected Sub gvStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStaff.SelectedIndexChanged
        Try
            'txtIsPopup.Text = ""
            'If txtStaffSelect.Text = "SourceInCharge" Then
            If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
                txtIncharge.Text = ""
            Else
                txtIncharge.Text = gvStaff.SelectedRow.Cells(1).Text
            End If

            'ElseIf txtStaffSelect.Text = "SourceServiceBy" Then
            'If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
            '    txtServiceBy.Text = ""
            'Else
            '    txtServiceBy.Text = gvStaff.SelectedRow.Cells(1).Text
            'End If



            'ElseIf txtStaffSelect.Text = "DestinationInCharge" Then
            'If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
            '    txtTeamDetailsIncharge.Text = ""
            'Else
            '    txtTeamDetailsIncharge.Text = gvStaff.SelectedRow.Cells(1).Text
            'End If

            'ElseIf txtStaffSelect.Text = "DestinationServiceBy" Then
            'If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
            '    txtTeamDetailsServiceBy.Text = ""
            'Else
            '    txtTeamDetailsServiceBy.Text = gvStaff.SelectedRow.Cells(1).Text
            'End If

            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "gvStaff_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData1() = True Then
            'lblAlert.Text = rdbSelect.Text + " " + txtQuery.Text
            '  InsertIntoTblWebEventLog("Manpower", "ExportToExcel", txtQuery.Text, txtCreatedBy.Text)

            'Return

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=ManpowerProductivityTeamMember.xls"
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

    Private Function GetData1() As Boolean
        GetData()

        Dim qry As String = ""


        Dim selection As String
        selection = ""
        Dim selFormula As String = ""
        If chkDistribution.Checked Then
            If rdbSelect.Text = "Detail" Then
                qry = "Select StaffID,ServiceDate,ContractNo,ServDateType as ContractGroup,Percentage/100 as Percentage,VehNo,RecordNo,TimeIn,TimeOut,Client,Service,ServiceValue,NoPerson,Duration,ServiceCost,Manpowercost,Productivity as Profit"
                qry = qry + " from tblrptserviceanalysis1 where report = 'ProdTeamMemberDet' and createdby='" & Session("UserID") & "'"

            ElseIf rdbSelect.Text = "Summary" Then
                qry = "Select A.StaffID,A.ServiceDate,count(A.recordno) as NoofServices,Sum(Productivity) as Profit"
                qry = qry + " from tblrptserviceanalysis1 A where A.report = 'ProdTeamMemberSummary' and A.createdby='" & Session("UserID") & "'"
            ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
                qry = "Select A.StaffID,count(A.recordno) as NoofServices,Sum(Productivity) as Profit"
                qry = qry + " from tblrptserviceanalysis1 A where A.report = 'ProdTeamMemberSummary' and A.createdby='" & Session("UserID") & "'"

            End If
        Else
            If rdbSelect.Text = "Detail" Then
                qry = "Select StaffID,ServiceDate,VehNo,RecordNo,TimeIn,TimeOut,Client,Service,ServiceValue,NoPerson,Duration,ServiceCost,Manpowercost,ServiceValue-ManpowerCost as Profit"
                qry = qry + " from tblrptserviceanalysis A where report = 'ProdTeamMemberDet' and createdby='" & Session("UserID") & "'"

            ElseIf rdbSelect.Text = "Summary" Then
                qry = "Select A.StaffID,A.ServiceDate,count(A.recordno) as NoofServices,Sum((A.ServiceValue-A.ManpowerCost)/A.NoPerson) as Profit"
                qry = qry + " from tblrptserviceanalysis A where A.status='P' and A.report = 'ProdTeamMemberSummary' and A.createdby='" & Session("UserID") & "'"
            ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
                qry = "Select A.StaffID,count(A.recordno) as NoofServices,Sum((A.ServiceValue-A.ManpowerCost)/A.NoPerson) as Profit"
                qry = qry + " from tblrptserviceanalysis A where A.status='P' and A.report = 'ProdTeamMemberSummary' and A.createdby='" & Session("UserID") & "'"

            End If

        End If
       

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + " and location in (" + Convert.ToString(Session("Branch")) + ")"

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
            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
            End If

            qry = qry + " and ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
        End If

        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                lblAlert.Text = "INVALID SERVICE DATE TO"
                Return False
            End If
            qry = qry + " and ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
            qry = qry + " and ServDateType in (" + YrStr + ")"
            'If rdbSelect.Text = "Summary" Then
            '    qry = qry + " and C.ContractGroup in (" + YrStr + ")"
            'ElseIf rdbSelect.Text = "Detail" Then
            '    qry = qry + " and A.ServDateType in (" + YrStr + ")"
            'End If
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If

        End If

        If String.IsNullOrEmpty(txtIncharge.Text) = True Then
       
            Else
            qry = qry + " and StaffID = '" + txtIncharge.Text + "'"
                'If rdbSelect.Text = "Detail" Then
                '    qry = qry + " and A.StaffID = '" + ddlIncharge.Text + "'"
                'Else
                '    qry = qry + " and A.ServiceBy = '" + ddlIncharge.Text + "'"
            'End If
            If selection = "" Then
                selection = "ServiceBy : " + txtIncharge.Text
            Else
                selection = selection + ", ServiceBy : " + txtIncharge.Text
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
            qry = qry + " and CompanyGroup in (" + YrStr + ")"

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

            qry = qry + " and LocateGrp in (" + YrStrZone + ")"
            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
            End If

        Session.Add("selection", selection)

            If rdbSelect.Text = "Detail" Then
            txtQuery.Text = qry + " order by StaffID,ServiceDate"
            ElseIf rdbSelect.Text = "Summary" Then
                txtQuery.Text = qry + " group by A.StaffID,A.ServiceDate"

            ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
                txtQuery.Text = qry + " group by A.StaffID"

            End If

        '
        InsertIntoTblWebEventLog("ManpowerProd", "GetData", txtQuery.Text, Session("UserID"))

            Return True
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
        'cell1.SetCellValue(Session("Selection").ToString)
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

        Dim _percentCellStyle As ICellStyle = Nothing

        If _percentCellStyle Is Nothing Then
            _percentCellStyle = workbook.CreateCellStyle()
            _percentCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _percentCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00%")
        End If

        Dim AllCellStyle As ICellStyle = Nothing

        AllCellStyle = workbook.CreateCellStyle()
        AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

        If rdbSelect.Text = "Detail" Then
            If chkDistribution.Checked = True Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 11 Or j = 13 Or j = 14 Or j = 15 Or j = 16 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                        ElseIf j = 4 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00%")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _percentCellStyle
                        ElseIf j = 12 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Int32 = Convert.ToInt32("0")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _intCellStyle

                        ElseIf j = 1 Then
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
            Else
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 8 Or j = 10 Or j = 11 Or j = 12 Or j = 13 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                        ElseIf j = 9 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Int32 = Convert.ToInt32("0")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _intCellStyle

                        ElseIf j = 1 Then
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
            End If
          
        ElseIf rdbSelect.Text = "Summary" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 3 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 2 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

                    ElseIf j = 1 Then
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

        ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 2 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 1 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

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
            Dim attachment As String = "attachment; filename=ManpowerProductivityTeamMember"


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

    Protected Sub btnEmailExcel_Click(sender As Object, e As EventArgs) Handles btnEmailExcel.Click
        'If GetData1() = True Then
        '    Dim conn As New MySqlConnection()
        '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        '    conn.Open()

        '    Dim command As MySqlCommand = New MySqlCommand

        '    command.CommandType = CommandType.Text
        '    Dim qry1 As String = "INSERT INTO tbwmanpowerreportgenerate(Generated,BatchNo,CreatedBy,CreatedOn,FileType,ReportType,TimeSheet,DateFrom,DateTo,Selection,Selformula,qry,RetryCount,ContractGroup,DomainName)"
        '    qry1 = qry1 + "VALUES(@Generated,@BatchNo,@CreatedBy,@CreatedOn,@FileType,@ReportType,@TimeSheet,@DateFrom,@DateTo,@Selection,@Selformula,@qry,@RetryCount,@ContractGroup,@DomainName);"

        '    command.CommandText = qry1
        '    command.Parameters.Clear()

        '    command.Parameters.AddWithValue("@Generated", 0)
        '    command.Parameters.AddWithValue("@BatchNo", txtCreatedBy.Text + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        '    command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
        '    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


        '    command.Parameters.AddWithValue("@FileType", "Excel")
        '    If rdbSelect.SelectedValue = "Detail" Then
        '        command.Parameters.AddWithValue("@ReportType", "ProdTeamMemberOTDet")

        '    Else
        '        command.Parameters.AddWithValue("@ReportType", "ProdTeamMemberOTSumm")

        '    End If
        '    If chkTimeSheet.Checked Then
        '        command.Parameters.AddWithValue("@TimeSheet", "Yes")
        '    Else
        '        command.Parameters.AddWithValue("@TimeSheet", "No")
        '    End If
        '    command.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd"))
        '    command.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd"))
        '    command.Parameters.AddWithValue("@Selection", Session("Selection"))
        '    command.Parameters.AddWithValue("@SelFormula", Session("SelFormula"))
        '    command.Parameters.AddWithValue("@qry", qry)
        '    command.Parameters.AddWithValue("@RetryCount", 0)
        '    If deleteqry = "delete from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm'" Then
        '        command.Parameters.AddWithValue("@ContractGroup", "-")
        '    Else
        '        command.Parameters.AddWithValue("@ContractGroup", deleteqry)
        '    End If
        '    command.Parameters.AddWithValue("@DomainName", ConfigurationManager.AppSettings("DomainName").ToString())

        '    command.Connection = conn

        '    command.ExecuteNonQuery()

        '    command.Dispose()
        '    conn.Close()
        '    conn.Dispose()
        '    mdlPopupMsg.Show()
        'End If

    End Sub
End Class
