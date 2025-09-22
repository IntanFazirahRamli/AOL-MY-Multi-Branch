Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_TeamProductivity
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID

        txtCreatedOn.Attributes.Add("readonly", "readonly")
    End Sub

    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs)

        If gvTeam.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtTeam.Text = " "
        Else
            txtTeam.Text = gvTeam.SelectedRow.Cells(2).Text
        End If



    End Sub

    Protected Sub gvTeam_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTeam.PageIndexChanging
        gvTeam.PageIndex = e.NewPageIndex

        If txtPopUpTeam.Text.Trim = "" Then
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status<>'N' order by TeamName"
        Else
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            '  SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%' and Status <> 'N'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"

        End If

        SqlDSTeam.DataBind()
        gvTeam.DataBind()
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub btnPopUpTeamSearch_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnPopUpTeamReset_Click(sender As Object, e As EventArgs)
        txtPopUpTeam.Text = ""
        txtPopupTeamSearch.Text = ""
        '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

        SqlDSTeam.DataBind()
        gvTeam.DataBind()
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub txtPopUpTeam_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpTeam.TextChanged
        If txtPopUpTeam.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupTeamSearch.Text = txtPopUpTeam.Text
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
            SqlDSTeam.DataBind()
            gvTeam.DataBind()
            mdlPopUpTeam.Show()
        End If
        txtPopUpTeam.Text = "Search Here for Team, Incharge or ServiceBy"
    End Sub

    Protected Sub btnTeam_Click(sender As Object, e As ImageClickEventArgs) Handles btnTeam.Click
        mdlPopUpTeam.TargetControlID = "btnTeam"
        If String.IsNullOrEmpty(txtTeam.Text.Trim) = False Then
            txtPopupTeamSearch.Text = txtTeam.Text.Trim
            txtPopUpTeam.Text = txtPopupTeamSearch.Text
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
            SqlDSTeam.DataBind()
            gvTeam.DataBind()
        Else
            'txtPopUpTeam.Text = ""
            'txtPopupTeamSearch.Text = ""
            '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

            SqlDSTeam.DataBind()
            gvTeam.DataBind()
        End If
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.SelectedIndex = 0
        txtTeam.Text = ""
     
        ddlCompanyGrp.SelectedIndex = 0

    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        '  Dim qrySvcRec As String = "SELECT * FROM tblservicerecord where status='P'"
        Dim qrySvcRec As String = "SELECT a.RecordNo,a.VehNo,a.ServiceDate,a.Duration,a.AccountID,a.CustName,a.TimeIn,a.TimeOut,a.BillAmount,"
        qrySvcRec = qrySvcRec + "a.location,a.ContractNo,a.Status,a.CompanyGroup,a.LocateGrp,c.contractgroup,"
        qrySvcRec = qrySvcRec + "(select ifnull(sum(b.servicecost),0) from tblservicerecorddet b where a.recordno=b.recordno group by b.recordno) as ServiceCost,"
        qrySvcRec = qrySvcRec + "(select group_concat(b.targetid) from tblservicerecorddet b where a.recordno=b.recordno group by b.recordno) as TargetID,"
        qrySvcRec = qrySvcRec + "(select COUNT(STAFFID) from tblservicerecordstaff s where a.recordno=s.recordno) AS NoofPerson"
        qrySvcRec = qrySvcRec + " FROM tblservicerecord a "
        qrySvcRec = qrySvcRec + " left join tblcontract c on a.contractno=c.contractno "
        qrySvcRec = qrySvcRec + " WHERE A.STATUS='P'"

        Dim selection As String
        selection = ""
        Dim selFormula As String = ""
        If rdbSelect.Text = "Detail" Then
            selFormula = "{tblrptserviceanalysis1.rcno} <> 0 and {tblrptserviceanalysis1.Status} = 'P' and {tblrptserviceanalysis1.Report}='TeamProdDet' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                qrySvcRec = qrySvcRec + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"
                selFormula = selFormula + " and {tblrptserviceanalysis1.Location} in [" + Convert.ToString(Session("Branch")) + "]"

                If selection = "" Then
                    selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                Else
                    selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                End If
            End If

        ElseIf rdbSelect.Text = "Summary" Then
            selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P'"

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                '  qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"

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
            If rdbSelect.Text = "Detail" Then
                selFormula = selFormula + " and {tblrptserviceanalysis1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            ElseIf rdbSelect.Text = "Summary" Then
                selFormula = selFormula + " and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            End If
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
            If rdbSelect.Text = "Detail" Then
                selFormula = selFormula + " and {tblrptserviceanalysis1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            ElseIf rdbSelect.Text = "Summary" Then
                selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            End If
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        'If txtServiceID.Text = "-1" Then
        'Else
        '    If rdbSelect.Text = "Summary" Then
        '        selFormula = selFormula + " and {tblcontract1.contractgroup} = '" + txtServiceID.SelectedItem.Text + "'"
        '    ElseIf rdbSelect.Text = "Detail" Then
        '        selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} = '" + txtServiceID.SelectedItem.Text + "'"
        '    End If
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
            If rdbSelect.Text = "Summary" Then
                selFormula = selFormula + " and {tblcontract1.contractgroup} in [" + YrStr + "]"
            ElseIf rdbSelect.Text = "Detail" Then
                selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} in [" + YrStr + "]"
            End If
              If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If

        End If

            If String.IsNullOrEmpty(txtTeam.Text) = False Then
                If rdbSelect.Text = "Detail" Then
                    selFormula = selFormula + "and {tblrptserviceanalysis1.TeamID} = '" + txtTeam.Text + "'"

                ElseIf rdbSelect.Text = "Summary" Then
                    selFormula = selFormula + "and {tblservicerecord1.TeamID} = '" + txtTeam.Text + "'"

                End If
                qrySvcRec = qrySvcRec + " and TeamID = '" + txtTeam.Text + "'"

                If selection = "" Then
                    selection = "TeamID : " + txtTeam.Text
                Else
                    selection = selection + ", TeamID : " + txtTeam.Text
                End If
            End If


            'If ddlCompanyGrp.Text = "-1" Then
            'Else
            '    qrySvcRec = qrySvcRec + " and CompanyGroup = '" + ddlCompanyGrp.Text + "'"
            '    If rdbSelect.Text = "Summary" Then
            '        selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
            '    ElseIf rdbSelect.Text = "Detail" Then
            '        selFormula = selFormula + " and {tblrptserviceanalysis1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
            '    End If
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
                If rdbSelect.Text = "Summary" Then
                    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
                ElseIf rdbSelect.Text = "Detail" Then
                    selFormula = selFormula + " and {tblrptserviceanalysis1.CompanyGroup} in [" + YrStr + "]"
                End If
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
                If rdbSelect.Text = "Summary" Then
                    selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
                ElseIf rdbSelect.Text = "Detail" Then
                    selFormula = selFormula + " and {tblrptserviceanalysis1.LocateGrp} in [" + YrStrZone + "]"
                End If

                ' selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
                If selection = "" Then
                    selection = "Zone : " + YrStrZone
                Else
                    selection = selection + ", Zone : " + YrStrZone
                End If
                qrySvcRec = qrySvcRec + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"
            End If


            If rdbSelect.Text = "Detail" Then

                GetData()

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


                'Dim command2 As MySqlCommand = New MySqlCommand

                'command2.CommandType = CommandType.Text
                'command2.CommandText = "delete from tblrptserviceanalysis where Report='TeamProdDet' and CreatedBy='" + txtCreatedBy.Text + "'"

                'command2.Connection = conn

                'command2.ExecuteNonQuery()

                '    If dtSvcRec.Rows.Count > 0 Then
                '        For i As Integer = 0 To dtSvcRec.Rows.Count - 1
                '            Dim cmdSvcRecStaff As MySqlCommand = New MySqlCommand

                '            cmdSvcRecStaff.CommandType = CommandType.Text

                '            cmdSvcRecStaff.CommandText = "Select count(staffid),staffid from tblservicerecordstaff where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"


                '            cmdSvcRecStaff.Connection = conn

                '            Dim drSvcRecStaff As MySqlDataReader = cmdSvcRecStaff.ExecuteReader()
                '            Dim dtSvcRecStaff As New DataTable
                '            dtSvcRecStaff.Load(drSvcRecStaff)

                '            'Dim staffid As String = ""
                '            'If dtSvcRecStaff.Rows.Count > 0 Then
                '            '    For k As Integer = 0 To dtSvcRecStaff.Rows.Count - 1
                '            '        If staffid <> "" Then
                '            '            staffid = staffid + "," + dtSvcRecStaff.Rows(k)("Staffid").ToString
                '            '        End If
                '            '    Next
                '            'End If
                '            Dim cmdSvcRecDet As MySqlCommand = New MySqlCommand

                '            cmdSvcRecDet.CommandType = CommandType.Text

                '            If txtServiceID.Text = "-1" Then
                '                cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "'"
                '            Else
                '                cmdSvcRecDet.CommandText = "Select targetid,servicecost,serviceid from tblservicerecorddet where recordno='" + dtSvcRec.Rows(i)("Recordno").ToString + "' and serviceid='" + txtServiceID.SelectedItem.Text + "'"

                '            End If


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


                '            ''''''Delete records that exists already
                '            'Dim command1 As MySqlCommand = New MySqlCommand

                '            'command1.CommandType = CommandType.Text

                '            'command1.CommandText = "SELECT * FROM tblrptserviceanalysis where recordno='" + dtSvcRec.Rows(i)("RecordNo") + "'"
                '            'command1.Connection = conn

                '            'Dim dr As MySqlDataReader = command1.ExecuteReader()
                '            'Dim dt As New DataTable
                '            'dt.Load(dr)

                '            'If dt.Rows.Count > 0 Then

                '        'Dim command2 As MySqlCommand = New MySqlCommand

                '        'command2.CommandType = CommandType.Text
                '        'command2.CommandText = "delete from tblrptserviceanalysis where recordno='" + dtSvcRec.Rows(i)("RecordNo") + "' and Report='TeamProdDet';"

                '        'command2.Connection = conn

                '        'command2.ExecuteNonQuery()


                '            'End If

                '            Dim command As MySqlCommand = New MySqlCommand

                '            command.CommandType = CommandType.Text
                '        Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report,Status,CompanyGroup,LocateGrp)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report,@Status,@CompanyGroup,@LocateGrp);"
                '            command.CommandText = qry
                '            command.Parameters.Clear()
                '            command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
                '            command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)
                '            'If txtActSvcDate.Text = "" Then
                '            '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                '            'Else
                '            '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
                '            'End If
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
                '            'If dtSvcRecStaff.Rows(0)("count(staffid)") <> 0 Then
                '            '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount") / dtSvcRecStaff.Rows(0)("count(staffid)"))
                '            'Else
                '            '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount"))

                '            'End If
                '            command.Parameters.AddWithValue("@ManpowerCost", 0)
                '            command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
                '            command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID"))
                '            command.Parameters.AddWithValue("@NormalHour", 0)
                '            command.Parameters.AddWithValue("@OTHour", 0)
                '        If dtContract.Rows.Count > 0 Then
                '            command.Parameters.AddWithValue("@ServDateType", dtContract.Rows(0)("ContractGroup").ToString)
                '        Else
                '            command.Parameters.AddWithValue("@ServDateType", "")
                '        End If

                '            command.Parameters.AddWithValue("@OTRate", 0)
                '            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                '            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '            command.Parameters.AddWithValue("@Report", "TeamProdDet")
                '            command.Parameters.AddWithValue("@Status", dtSvcRec.Rows(i)("Status"))
                '            command.Parameters.AddWithValue("@CompanyGroup", dtSvcRec.Rows(i)("CompanyGroup"))
                '        command.Parameters.AddWithValue("@LocateGrp", dtSvcRec.Rows(i)("LocateGrp"))


                '            command.Connection = conn

                '            command.ExecuteNonQuery()

                '        dtSvcRecDet.Clear()
                '        dtSvcRecStaff.Clear()
                '        drSvcRecDet.Close()
                '        drSvcRecStaff.Close()
                '        Next

                'End If

                'dtSvcRec.Clear()
                'drSvcRec.Close()

                'conn.Close()

            End If

            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)

            If rdbSelect.SelectedValue = "Detail" Then
                Session.Add("ReportType", "TeamProductivityDetail")
                Response.Redirect("RV_TeamProductivityDetail.aspx")
            ElseIf rdbSelect.SelectedValue = "Summary" Then
                Session.Add("ReportType", "TeamProductivitySummary")
                Response.Redirect("RV_TeamProductivitySummary.aspx")

            End If

    End Sub

    Private Sub GetData()

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='TeamProdDet';"

        command2.Connection = conn

        command2.ExecuteNonQuery()
        command2.Dispose()

        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = "SaveTeamProductivity"
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@pr_ServiceDate1", Convert.ToDateTime(txtsvcdatefrom.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@pr_ServiceDate2", Convert.ToDateTime(txtsvcdateto.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@pr_UserID", Session("UserID"))
        command.Parameters.AddWithValue("@pr_Report", "TeamProdDet")
      
        command.Connection = conn
        command.ExecuteScalar()

        command.Dispose()
        conn.Close()
        conn.Dispose()
    End Sub

    Private Function GetData1() As Boolean
        GetData()

        Dim qry As String = ""

       
        Dim selection As String
        selection = ""
        Dim selFormula As String = ""
        If rdbSelect.Text = "Detail" Then
            qry = "Select TeamID,ServiceDate,VehNo,RecordNo,TimeIn,TimeOut,Client,Service,ServiceValue,NoPerson,Duration,ServiceCost,Manpowercost,ServiceValue-ManpowerCost as Profit"
            qry = qry + " from tblrptserviceanalysis A where report = 'TeamProdDet' and createdby='" & Session("UserID") & "'"

        ElseIf rdbSelect.Text = "Summary" Then
            qry = "Select A.TeamID,count(A.recordno) as NoofServices,Sum(A.billamount) as TotalBillAmount"
            qry = qry + " from tblservicerecord A left join tblcontract c on a.contractno=c.contractno where A.status='P'"

        End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + " and A.location in (" + Convert.ToString(Session("Branch")) + ")"
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
            qry = qry + " and A.ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
        End If

        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                lblAlert.Text = "INVALID SERVICE DATE TO"
                Return False
            End If
            qry = qry + " and A.ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
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
            If rdbSelect.Text = "Summary" Then
                qry = qry + " and c.ContractGroup in (" + YrStr + ")"
            ElseIf rdbSelect.Text = "Detail" Then
                qry = qry + " and A.ServDateType in (" + YrStr + ")"
            End If
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If
        End If

        If String.IsNullOrEmpty(txtTeam.Text) = False Then
        
            qry = qry + " and A.TeamID = '" + txtTeam.Text + "'"
            If selection = "" Then
                selection = "Team : " + txtTeam.Text
            Else
                selection = selection + ", Team : " + txtTeam.Text
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
            qry = qry + " and A.CompanyGroup in (" + YrStr + ")"
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
           
            qry = qry + " and A.LocateGrp in (" + YrStrZone + ")"
            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
        End If


        If rdbSelect.Text = "Detail" Then
            txtQuery.Text = qry + " order by A.TeamID,A.ServiceDate"
        ElseIf rdbSelect.Text = "Summary" Then
            txtQuery.Text = qry + " group by A.TeamID"
        End If

        Session.Add("selection", selection)

        Return True

    End Function

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData1() = True Then
            'lblAlert.Text = rdbSelect.Text + " " + txtQuery.Text

            'Return

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=TeamProductivity.xls"
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
        '   cell1.SetCellValue(Session("Selection").ToString)
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

        Dim AllCellStyle As ICellStyle = Nothing

        AllCellStyle = workbook.CreateCellStyle()
        AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

        If rdbSelect.Text = "Detail" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 8 Or j = 11 Or j = 12 Or j = 13 Or j = 9 Or j = 10 Then
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


        ElseIf rdbSelect.Text = "Summary" Then
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
            Dim attachment As String = "attachment; filename=TeamProductivity"

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
End Class
