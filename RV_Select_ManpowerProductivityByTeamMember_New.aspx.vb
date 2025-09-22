
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Diagnostics



Partial Class RV_Select_ManpowerProductivityByTeamMember_New
    Inherits System.Web.UI.Page



    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.SelectedIndex = 0
        'ddlIncharge.SelectedIndex = 0
        txtIncharge.Text = ""
        ddlCompanyGrp.SelectedIndex = 0

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
            InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "1")

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


            InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "2")


            If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                        ' MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                    lblAlert.Text = "INVALID SERVICE DATE FROM"
                    Return
                    End If

                qrySvcRec = qrySvcRec + " and a.ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

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
                qrySvcRec = qrySvcRec + " and a.ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

                If selection = "" Then
                    selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
                    End If
                End If
                '   InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "4")


            If txtServiceID.Text = "-1" Then
            Else

                selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} = '" + txtServiceID.SelectedItem.Text + "'"
                If selection = "" Then
                    selection = "ServiceID = " + txtServiceID.SelectedItem.Text
                Else
                    selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
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

            InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", qrySvcRec, "5")
          
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmdSvcRec As MySqlCommand = New MySqlCommand
            cmdSvcRec.CommandType = CommandType.Text
            cmdSvcRec.CommandText = qrySvcRec
            cmdSvcRec.Connection = conn

            InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test3", "")

            Dim drSvcRec As MySqlDataReader = cmdSvcRec.ExecuteReader()
            Dim dtSvcRec As New DataTable
            dtSvcRec.Load(drSvcRec)

            cmdSvcRec.Dispose()

            InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test2", "")


            If dtSvcRec.Rows.Count > 0 Then

                InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test", dtSvcRec.Rows.Count.ToString)

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                If rdbSelect.Text = "Detail" Then
                    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberDet';"

                ElseIf rdbSelect.Text = "Summary" Then
                    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberSummary';"
                ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
                    command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='ProdTeamMemberSummary';"

                    End If


                command2.Connection = conn

                command2.ExecuteNonQuery()
                command2.Dispose()

                InsertIntoTblWebEventLog("MANPOWER-MEMBER - " + Session("UserID"), "ImageButton10_Click", "Test - " + dtSvcRec.Rows.Count.ToString, "")



                For i As Integer = 0 To dtSvcRec.Rows.Count - 1




                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report,Status,CompanyGroup,LocateGrp)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report,@Status,@CompanyGroup,@LocateGrp);"
                    command.CommandText = qry
                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
                    command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)
                    'If txtActSvcDate.Text = "" Then
                    '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                    'Else
                    '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
                    'End If
                    command.Parameters.AddWithValue("@ServiceDate", dtSvcRec.Rows(i)("ServiceDate"))
                    command.Parameters.AddWithValue("@NoPerson", dtSvcRec.Rows(i)("NoofPerson"))
                    command.Parameters.AddWithValue("@Duration", dtSvcRec.Rows(i)("Duration"))
                    command.Parameters.AddWithValue("@AccountID", dtSvcRec.Rows(i)("AccountID").ToString)
                    command.Parameters.AddWithValue("@Client", dtSvcRec.Rows(i)("CustName").ToString)
                    command.Parameters.AddWithValue("@TimeIn", dtSvcRec.Rows(i)("TimeIn"))
                    command.Parameters.AddWithValue("@TimeOut", dtSvcRec.Rows(i)("Timeout"))
                    command.Parameters.AddWithValue("@Service", dtSvcRec.Rows(i)("TargetID"))
                    command.Parameters.AddWithValue("@ServiceValue", dtSvcRec.Rows(i)("BillAmount"))
                    command.Parameters.AddWithValue("@ServiceCost", dtSvcRec.Rows(i)("ServiceCost"))
                    'If dtSvcRecStaff.Rows(0)("count(staffid)") <> 0 Then
                    '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount") / dtSvcRecStaff.Rows(0)("count(staffid)"))
                    'Else
                    '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount"))

                    'End If
                    command.Parameters.AddWithValue("@ManpowerCost", 0)

                    '  command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
                    '  command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID"))

                    command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("StaffID"))
                    command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("StaffName"))

                    command.Parameters.AddWithValue("@NormalHour", 0)
                    command.Parameters.AddWithValue("@OTHour", 0)
                    command.Parameters.AddWithValue("@ServDateType", dtSvcRec.Rows(i)("ContractGroup").ToString)


                    command.Parameters.AddWithValue("@OTRate", 0)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    If rdbSelect.Text = "Detail" Then
                        command.Parameters.AddWithValue("@Report", "ProdTeamMemberDet")
                    ElseIf rdbSelect.Text = "Summary" Then
                        command.Parameters.AddWithValue("@Report", "ProdTeamMemberSummary")
                    ElseIf rdbSelect.Text = "SummaryByTeamMember" Then
                        command.Parameters.AddWithValue("@Report", "ProdTeamMemberSummary")

                    End If
                    command.Parameters.AddWithValue("@Status", dtSvcRec.Rows(i)("Status"))
                    command.Parameters.AddWithValue("@CompanyGroup", dtSvcRec.Rows(i)("CompanyGroup"))
                    command.Parameters.AddWithValue("@LocateGrp", dtSvcRec.Rows(i)("LocateGrp"))
                    command.Connection = conn

                    command.ExecuteNonQuery()
                    command.Dispose()



                Next

                End If
            dtSvcRec.Clear()
            dtSvcRec.Dispose()

            drSvcRec.Close()
            conn.Close()
            conn.Dispose()

                'stopwatch.[Stop]()
                'InsertIntoTblWebEventLog("PrintTest", qrySvcRec.ToString, stopwatch.ElapsedMilliseconds.ToString, "TEST")

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

                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where  1=1  and (upper(Name) Like '%" + txtPopupStaffSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by Name"
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
                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where upper(Name) Like '%" + txtPopupStaff.Text.Trim.ToUpper + "%'"
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
                SqlDSStaff.SelectCommand = "SELECT  StaffId, Name, NRIC  From tblStaff where 1=1 and roles='TECHNICAL' and Name like '%" + txtPopupStaff.Text.Trim.ToUpper + "%' and status <> 'N' order by Name"

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
End Class

