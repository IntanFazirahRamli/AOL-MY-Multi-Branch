Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class RV_Select_VehicleProductivity
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID

        txtCreatedOn.Attributes.Add("readonly", "readonly")
    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.SelectedIndex = 0
        ddlVehicle.SelectedIndex = 0

        ddlCompanyGrp.SelectedIndex = 0

    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        'Dim qrySvcRec As String = "SELECT * FROM tblservicerecord where status='P'"
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
            selFormula = "{tblrptserviceanalysis1.rcno} <> 0 and {tblrptserviceanalysis1.Status} = 'P' and {tblrptserviceanalysis1.Report}='VehProdDet'"

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                qrySvcRec = qrySvcRec + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"

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
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            qrySvcRec = qrySvcRec + " and ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
            If rdbSelect.Text = "Detail" Then
                selFormula = selFormula + "and {tblrptserviceanalysis1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            ElseIf rdbSelect.Text = "Summary" Then
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"

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
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            qrySvcRec = qrySvcRec + " and ServiceDate <= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            If rdbSelect.Text = "Detail" Then
                selFormula = selFormula + "and {tblrptserviceanalysis1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            ElseIf rdbSelect.Text = "Summary" Then
                selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

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
        '        selFormula = selFormula + " and {tblcontract1.ServiceID} = '" + txtServiceID.SelectedItem.Text + "'"
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
                selFormula = selFormula + " and {tblcontract1.ServiceID} in [" + YrStr + "]"
            ElseIf rdbSelect.Text = "Detail" Then
                selFormula = selFormula + " and {tblrptserviceanalysis1.ServDateType} in [" + YrStr + "]"
            End If


            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If

        End If

            If ddlVehicle.Text = "-1" Then
            Else
                If rdbSelect.Text = "Summary" Then
                    selFormula = selFormula + " and {tblservicerecord1.VehNo} = '" + ddlVehicle.Text + "'"
                ElseIf rdbSelect.Text = "Detail" Then
                    selFormula = selFormula + " and {tblrptserviceanalysis1.VehNo} = '" + ddlVehicle.Text + "'"
                End If
                qrySvcRec = qrySvcRec + " and VehNo = '" + ddlVehicle.Text + "'"

                If selection = "" Then
                    selection = "VehNo : " + ddlVehicle.Text
                Else
                    selection = selection + ", VehNo : " + ddlVehicle.Text
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

            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'Dim cmdSvcRec As MySqlCommand = New MySqlCommand

            'cmdSvcRec.CommandType = CommandType.Text

            'cmdSvcRec.CommandText = qrySvcRec


            'cmdSvcRec.Connection = conn

            'Dim drSvcRec As MySqlDataReader = cmdSvcRec.ExecuteReader()
            'Dim dtSvcRec As New DataTable
            'dtSvcRec.Load(drSvcRec)

            'If dtSvcRec.Rows.Count > 0 Then
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



            '        ''''''Delete records that exists already
            '        'Dim command1 As MySqlCommand = New MySqlCommand

            '        'command1.CommandType = CommandType.Text

            '        'command1.CommandText = "SELECT * FROM tblrptserviceanalysis where recordno='" + dtSvcRec.Rows(i)("RecordNo") + "'"
            '        'command1.Connection = conn

            '        'Dim dr As MySqlDataReader = command1.ExecuteReader()
            '        'Dim dt As New DataTable
            '        'dt.Load(dr)

            '        'If dt.Rows.Count > 0 Then

            '        Dim command2 As MySqlCommand = New MySqlCommand

            '        command2.CommandType = CommandType.Text
            '        command2.CommandText = "delete from tblrptserviceanalysis where recordno='" + dtSvcRec.Rows(i)("RecordNo") + "' and Report='VehProdDet';"

            '        command2.Connection = conn

            '        command2.ExecuteNonQuery()


            '        'End If

            '        Dim command As MySqlCommand = New MySqlCommand

            '        command.CommandType = CommandType.Text
            '        Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,VehNo,ServiceDate,NoPerson,Duration,AccountID,Client,TimeIn,TimeOut,Service,ServiceValue,ServiceCost,ManpowerCost,StaffID,TeamID,NormalHour,OTHour,ServDateType,OTRate,CreatedBy,CreatedOn,Report,Status,CompanyGroup,LocateGrp)VALUES(@RecordNo,@VehNo,@ServiceDate,@NoPerson,@Duration,@AccountID,@Client,@TimeIn,@TimeOut,@Service,@ServiceValue,@ServiceCost,@ManpowerCost,@StaffID,@TeamID,@NormalHour,@OTHour,@ServDateType,@OTRate,@CreatedBy,@CreatedOn,@Report,@Status,@CompanyGroup,@LocateGrp);"
            '        command.CommandText = qry
            '        command.Parameters.Clear()
            '        command.Parameters.AddWithValue("@RecordNo", dtSvcRec.Rows(i)("RecordNo").ToString)
            '        command.Parameters.AddWithValue("@VehNo", dtSvcRec.Rows(i)("VehNo").ToString)
            '        'If txtActSvcDate.Text = "" Then
            '        '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
            '        'Else
            '        '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
            '        'End If
            '        command.Parameters.AddWithValue("@ServiceDate", dtSvcRec.Rows(i)("ServiceDate"))
            '        command.Parameters.AddWithValue("@NoPerson", dtSvcRecStaff.Rows(0)("count(staffid)"))
            '        command.Parameters.AddWithValue("@Duration", dtSvcRec.Rows(i)("Duration"))
            '        command.Parameters.AddWithValue("@AccountID", dtSvcRec.Rows(i)("AccountID").ToString)
            '        command.Parameters.AddWithValue("@Client", dtSvcRec.Rows(i)("CustName").ToString)
            '        command.Parameters.AddWithValue("@TimeIn", dtSvcRec.Rows(i)("TimeIn"))
            '        command.Parameters.AddWithValue("@TimeOut", dtSvcRec.Rows(i)("Timeout"))
            '        command.Parameters.AddWithValue("@Service", targetid)
            '        command.Parameters.AddWithValue("@ServiceValue", dtSvcRec.Rows(i)("BillAmount"))
            '        command.Parameters.AddWithValue("@ServiceCost", servicecost)
            '        'If dtSvcRecStaff.Rows(0)("count(staffid)") <> 0 Then
            '        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount") / dtSvcRecStaff.Rows(0)("count(staffid)"))
            '        'Else
            '        '    command.Parameters.AddWithValue("@ManpowerCost", dtSvcRec.Rows(i)("BillAmount"))

            '        'End If
            '        command.Parameters.AddWithValue("@ManpowerCost", 0)
            '        command.Parameters.AddWithValue("@StaffID", dtSvcRec.Rows(i)("ServiceBy"))
            '        command.Parameters.AddWithValue("@TeamID", dtSvcRec.Rows(i)("TeamID"))
            '        command.Parameters.AddWithValue("@NormalHour", 0)
            '        command.Parameters.AddWithValue("@OTHour", 0)
            '        If dtContract.Rows.Count > 0 Then
            '            command.Parameters.AddWithValue("@ServDateType", dtContract.Rows(0)("ContractGroup").ToString)
            '        Else
            '            command.Parameters.AddWithValue("@ServDateType", "")
            '        End If

            '        command.Parameters.AddWithValue("@OTRate", 0)
            '        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
            '        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '        command.Parameters.AddWithValue("@Report", "VehProdDet")
            '        command.Parameters.AddWithValue("@Status", dtSvcRec.Rows(i)("Status"))
            '        command.Parameters.AddWithValue("@CompanyGroup", dtSvcRec.Rows(i)("CompanyGroup"))
            '        command.Parameters.AddWithValue("@LocateGrp", dtSvcRec.Rows(i)("LocateGrp"))


            '        command.Connection = conn

            '        command.ExecuteNonQuery()

            '    Next

            'End If
            'conn.Close()

        End If


        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If rdbSelect.SelectedValue = "Detail" Then
            Session.Add("ReportType", "VehicleProductivityDetail")
            Response.Redirect("RV_VehicleProductivityDetail.aspx")
        ElseIf rdbSelect.SelectedValue = "Summary" Then
            Session.Add("ReportType", "VehicleProductivitySummary")
            Response.Redirect("RV_VehicleProductivitySummary.aspx")

        End If

    End Sub

    Private Sub GetData()

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "delete from tblrptserviceanalysis where CREATEDBY='" + txtCreatedBy.Text.Trim + "' and Report='VehProdDet';"

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
        command.Parameters.AddWithValue("@pr_Report", "VehProdDet")

        command.Connection = conn
        command.ExecuteScalar()

        command.Dispose()
        conn.Close()
        conn.Dispose()
    End Sub
End Class
