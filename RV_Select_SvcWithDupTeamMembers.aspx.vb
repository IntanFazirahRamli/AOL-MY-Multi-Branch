Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient

Partial Class RV_Select_SvcWithDupTeamMembers
    Inherits System.Web.UI.Page

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
    End Sub


    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click

       

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
        Else
            lblAlert.Text = "ENTER SERVICE DATE FROM"
            Return
        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
        Else
            lblAlert.Text = "ENTER SERVICE DATE TO"
            Return
        End If

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT staff.recordno,staffID,count(StaffID) as cnt,rec.servicedate FROM tblservicerecordstaff as staff left outer join tblservicerecord as rec on staff.recordno = rec.recordno where servicedate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "' and servicedate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "' group by recordno,staffid having cnt>1;"
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        '  Dim recno As String = ""

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text
        command2.CommandText = "delete from tblrptserviceanalysis where Report='SvcRecWithDuplicateMembers' and CreatedBy='" + txtCreatedBy.Text + "'"

        command2.Connection = conn

        command2.ExecuteNonQuery()

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                'If i = 0 Then
                '    recno = """" + dt.Rows(i)("RecordNo") + """"
                'Else
                '    recno = recno + "," + """" + dt.Rows(i)("RecordNo") + """"
                'End If

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblrptserviceanalysis(RecordNo,NoPerson,StaffID,CreatedBy,CreatedOn,Report)VALUES(@RecordNo,@NoPerson,@StaffID,@CreatedBy,@CreatedOn,@Report);"
                command.CommandText = qry
                command.Parameters.Clear()
                command.Parameters.AddWithValue("@RecordNo", dt.Rows(i)("RecordNo").ToString)

                command.Parameters.AddWithValue("@NoPerson", dt.Rows(i)("cnt"))
                command.Parameters.AddWithValue("@StaffID", dt.Rows(i)("StaffID").ToString)

                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@Report", "SvcRecWithDuplicateMembers")

                command.Connection = conn

                command.ExecuteNonQuery()

            Next

        End If

        dt.Clear()
        dr.Close()


        conn.Close()

        Dim selFormula As String
        Dim selection As String
        selection = ""
        ' If recno = "" Then
        selFormula = "{tblservicerecord1.rcno} <> 0 and {tblrptserviceanalysis1.Report}='SvcRecWithDuplicateMembers' and {tblrptserviceanalysis1.CreatedBy}='" + txtCreatedBy.Text + "'"
        'Else
        '    selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.recordno} in [" + recno + "] "

        'End If
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            '  qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If rdbFormat.SelectedValue = "Format1" Then
            Session.Add("ReportType", "SvcRecWithDuplicateTeamMembers_Format1")
            Response.Redirect("RV_SvcRecWithDuplicateTeamMembers_Format1.aspx")
        ElseIf rdbFormat.SelectedValue = "Format2" Then
            Session.Add("ReportType", "SvcRecWithDuplicateTeamMembers_Format2")
            Response.Redirect("RV_SvcRecWithDuplicateTeamMembers_Format2.aspx")
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        txtCreatedBy.Text = UserID

        txtCreatedOn.Attributes.Add("readonly", "readonly")
    End Sub
End Class
