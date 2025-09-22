Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class ServiceDetails
    Inherits System.Web.UI.Page


    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim targetstring As String = ""
        For Each row As GridViewRow In gvTarget.Rows
            ' Access the CheckBox
            Dim cb As CheckBox = DirectCast(row.FindControl("chkSelect"), CheckBox)
            If cb IsNot Nothing AndAlso cb.Checked Then
                Dim targetID As String = row.Cells(1).Text
                If targetstring = "" Then
                    targetstring = row.Cells(1).Text
                Else
                    targetstring = targetstring + "," + row.Cells(1).Text
                End If

            End If
        Next
        txtTargetID.Text = targetstring

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        mdlPopUpTarget.Hide()

    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        If GridView2.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtServices.Text = ""
        Else
            txtServices.Text = GridView2.SelectedRow.Cells(1).Text
        End If
        If GridView2.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtDescription.Text = ""
        Else
            txtDescription.Text = GridView2.SelectedRow.Cells(2).Text
        End If
       
    End Sub

    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID


            Dim SvcRecordNo As String = Convert.ToString(Session("SvcRecordNo"))
            txtRecordNo.Text = SvcRecordNo
            Dim Query As String = Convert.ToString(Session("Query"))
            txtQuery.Text = Query
            Dim SvcRcNo As String = Convert.ToString(Session("SvcRcNo"))
            txtSvcRcno.Text = SvcRcNo

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblservicerecorddet where recordno='" & txtRecordNo.Text & "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                ddlStatus.SelectedValue = dt.Rows(0)("Status").ToString
                txtJobOrder.Text = dt.Rows(0)("Joborder").ToString
                If dt.Rows(0)("JobOrderDate").ToString = DBNull.Value.ToString Then
                Else
                    txtJobDate.Text = Convert.ToDateTime(dt.Rows(0)("JobOrderDate")).ToString("dd/MM/yyyy")
                End If
                txtJobContact.Text = dt.Rows(0)("JOBCONTACT").ToString
                txtProspectID.Text = dt.Rows(0)("ProspectID").ToString
                txtContractNo.Text = dt.Rows(0)("ContractNo").ToString
                txtTargetID.Text = dt.Rows(0)("TargetID").ToString
                txtProject.Text = dt.Rows(0)("ProjectCode").ToString
                txtLocation.Text = dt.Rows(0)("Location").ToString
                txtBranchID.Text = dt.Rows(0)("BranchID").ToString

                txtServices.Text = dt.Rows(0)("ServiceID").ToString
                txtDescription.Text = dt.Rows(0)("ProductServices").ToString
                txtMainJobNo.Text = dt.Rows(0)("MainJobOrder").ToString

                txtAllocatedTime.Text = dt.Rows(0)("AllocatedSvcTime").ToString
                txtSvcTime.Text = dt.Rows(0)("ServiceTime").ToString
                txtContractTime.Text = dt.Rows(0)("Duration").ToString
                txtProgTime.Text = dt.Rows(0)("ProgTime").ToString
                txtTotDuration.Text = dt.Rows(0)("ActualDuration").ToString

                txtWarranty.Text = dt.Rows(0)("Warranty").ToString
                txtFault.Text = dt.Rows(0)("Reason").ToString
                txtAction.Text = dt.Rows(0)("Action").ToString
                txtServiceCost.Text = dt.Rows(0)("ServiceCost").ToString

                txtRemarkOffice.Text = dt.Rows(0)("RemarkOffice").ToString
                txtRemarkClient.Text = dt.Rows(0)("RemarkClient").ToString

                txtRcno.Text = dt.Rows(0)("Rcno").ToString

            End If
            txtRecordNo.Enabled = False
        End If

    End Sub


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        '  ClientScript.RegisterStartupScript(Me.GetType(), "script", "window.close();", True)
        'Session("recordno") = txtRecordNo.Text
        'Session("servicefrom") = "servicedetails"
        'Session("Query") = txtQuery.Text
        'Session("SvcRcNo") = txtSvcRcno.Text
        'Server.Transfer("Service.aspx")

        'Session("recordno") = lblRecordNo.Text
        Session("servicefrom") = "serviceprint"
        'Session("servicefrom") = "servicedetails"
        'Session("Query") = txtQuery.Text
        Session("SvcRcNo") = txtSvcRcno.Text
        Server.Transfer("Service.aspx")
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtRecordNo.Text = "" Then
            MessageBox.Message.Alert(Page, "Record Number cannot be blank!!!", "str")
            Return

        End If
        If String.IsNullOrEmpty(txtJobDate.Text) = False Then
            If IsDate(txtJobDate.Text) = False Then
                MessageBox.Message.Alert(Page, "Job Date is invalid", "str")
                Return
            End If
        End If

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblservicerecord where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "UPDATE tblservicerecorddet SET JobOrder = @JobOrder,JobOrderDate = @JobOrderDate,ProspectID = @ProspectID,ContractNo = @ContractNo,Warranty = @Warranty,Reason = @Reason,Action = @Action,Duration = @Duration,Status = @Status,JOBCONTACT = @JOBCONTACT,ActualDuration = @ActualDuration,ServiceTime = @ServiceTime,ProgTime = @ProgTime,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,ProjectCode = @ProjectCode,AllocatedSvcTime = @AllocatedSvcTime,MainJobOrder = @MainJobOrder,ProductServices = @ProductServices,RemarkOffice = @RemarkOffice,RemarkClient = @RemarkClient,Location = @Location,BranchID = @BranchID,TargetID = @TargetID,ServiceCost = @ServiceCost,ServiceID = @ServiceID WHERE  rcno=" & Convert.ToInt32(txtRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@JobOrder", txtJobOrder.Text)
                If String.IsNullOrEmpty(txtJobDate.Text) Then
                    command.Parameters.AddWithValue("@JobOrderDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@JobOrderDate", Convert.ToDateTime(txtJobDate.Text).ToString("yyyy-MM-dd"))
                End If

                command.Parameters.AddWithValue("@ProspectID", txtProspectID.Text)
                command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                command.Parameters.AddWithValue("@Warranty", txtWarranty.Text)
                command.Parameters.AddWithValue("@Reason", txtFault.Text)
                command.Parameters.AddWithValue("@Action", txtAction.Text)
                If String.IsNullOrEmpty(txtTotDuration.Text) Then
                    command.Parameters.AddWithValue("@ActualDuration", 0)
                Else
                    command.Parameters.AddWithValue("@ActualDuration", txtTotDuration.Text)
                End If
                If ddlStatus.Text = "--SELECT--" Then
                    command.Parameters.AddWithValue("@Status", "")
                Else
                    command.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue)
                End If
                If String.IsNullOrEmpty(txtSvcTime.Text) Then
                    command.Parameters.AddWithValue("@ServiceTime", 0)
                Else
                    command.Parameters.AddWithValue("@ServiceTime", txtSvcTime.Text)
                End If
                If String.IsNullOrEmpty(txtProgTime.Text) Then
                    command.Parameters.AddWithValue("@ProgTime", 0)
                Else
                    command.Parameters.AddWithValue("@ProgTime", txtProgTime.Text)
                End If
                If String.IsNullOrEmpty(txtContractTime.Text) Then
                    command.Parameters.AddWithValue("@Duration", 0)
                Else
                    command.Parameters.AddWithValue("@Duration", txtContractTime.Text)
                End If
                If String.IsNullOrEmpty(txtAllocatedTime.Text) Then
                    command.Parameters.AddWithValue("@AllocatedSvcTime", 0)
                Else
                    command.Parameters.AddWithValue("@AllocatedSvcTime", txtAllocatedTime.Text)
                End If
                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@JOBCONTACT", txtJobContact.Text)
                command.Parameters.AddWithValue("@ProjectCode", txtProject.Text)
                command.Parameters.AddWithValue("@MainJobOrder", txtMainJobNo.Text)
                command.Parameters.AddWithValue("@ProductServices", txtDescription.Text)
                command.Parameters.AddWithValue("@RemarkOffice", txtRemarkOffice.Text)
                command.Parameters.AddWithValue("@RemarkClient", txtRemarkClient.Text)
                command.Parameters.AddWithValue("@Location", txtLocation.Text)
                command.Parameters.AddWithValue("@BranchID", txtBranchID.Text)
                command.Parameters.AddWithValue("@TargetID", txtTargetID.Text)
                If String.IsNullOrEmpty(txtServiceCost.Text) Then
                    command.Parameters.AddWithValue("@ServiceCost", 0)
                Else
                    command.Parameters.AddWithValue("@ServiceCost", txtServiceCost.Text)
                End If
                command.Parameters.AddWithValue("@ServiceID", txtServices.Text)
                command.Connection = conn

                command.ExecuteNonQuery()

                MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
            End If

                conn.Close()

        Catch ex As Exception

            MessageBox.Message.Alert(Page, "Error!!! " + ex.ToString, "str")
        End Try

    End Sub
End Class
