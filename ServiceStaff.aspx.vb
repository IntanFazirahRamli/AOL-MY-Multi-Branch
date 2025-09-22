Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class ServiceStaff
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim SvcRecordNo As String = Convert.ToString(Session("SvcRecordNo"))
            txtRecordNo.Text = SvcRecordNo
            Dim Query As String = Convert.ToString(Session("Query"))
            txtQuery.Text = Query
            Dim SvcRcNo As String = Convert.ToString(Session("SvcRcNo"))
            txtSvcRcno.Text = SvcRcNo
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            txtCreatedOn.Attributes.Add("readonly", "readonly")
        End If

    End Sub

    Private Sub MakeMeNull()
        ddlID.SelectedIndex = 0
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCostValue.Text = ""
        txtCreatedOn.Text = ""
        'txtCreatedBy.Text = ""
        txtMode.Text = ""
    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnAdd.Enabled = True
        btnAdd.ForeColor = System.Drawing.Color.Black
     
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

        ddlID.Enabled = False
        txtCostValue.Enabled = False

    End Sub

    Private Sub DisableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black

        'btnAdd.Enabled = False
        'btnAdd.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        ddlID.Enabled = True
        txtCostValue.Enabled = True

    End Sub
    Public rcno As String
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        MakeMeNull()
        txtMode.Text = "Edit"
        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()
        '  MessageBox.Message.Alert(Page, GridView1.SelectedRow.Cells(1).Text + " [" + GridView1.SelectedRow.Cells(2).Text + "]", "str")
        ddlID.SelectedValue = GridView1.SelectedRow.Cells(1).Text
        'MessageBox.Message.Alert(Page, ddlID.SelectedValue.ToString, "str")
        txtCostValue.Text = GridView1.SelectedRow.Cells(3).Text
        DisableControls()

    End Sub


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        '  ClientScript.RegisterStartupScript(Me.GetType(), "script", "window.close();", True)
        Session("recordno") = txtRecordNo.Text
        Session("servicefrom") = "servicedetails"
        Session("Query") = txtQuery.Text
        Session("SvcRcNo") = txtSvcRcno.Text
        Response.Redirect("Service.aspx")
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        MakeMeNull()
        EnableControls()



    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        MakeMeNull()
        txtMode.Text = "New"
        DisableControls()

        ddlID.Focus()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim d As Double
        If String.IsNullOrEmpty(txtCostValue.Text) = False Then
            If Double.TryParse(txtCostValue.Text, d) = False Then
                MessageBox.Message.Alert(Page, "Enter valid Cost Value!!", "str")
                Return
            End If
        Else
            txtCostValue.Text = "0"
        End If
        If ddlID.Text = "--SELECT--" Then
            MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            Return
        End If

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblservicerecordstaff where recordno=@recordno and staffid=@staffid"
                command1.Parameters.AddWithValue("@recordno", txtRecordNo.Text)
                command1.Parameters.AddWithValue("@staffid", ddlID.SelectedValue.ToString)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    MessageBox.Message.Alert(Page, "Selected Staff already assigned for this service!!!", "str")

                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblservicerecordstaff(StaffID,StaffName,CostValue,RecordNo,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,CreateDeviceID,CreateSource,FlowFrom,FlowTo,EditSource,DeleteStatus,LastEditDevice)VALUES(@StaffID,@StaffName,@CostValue,@RecordNo,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@CreateDeviceID,@CreateSource,@FlowFrom,@FlowTo,@EditSource,@DeleteStatus,@LastEditDevice);"
                    command.CommandText = qry
                    command.Parameters.Clear()
                   
                    command.Parameters.AddWithValue("@StaffID", ddlID.SelectedValue.ToString)
                    Dim name As String = ddlID.SelectedItem.Text.Substring(ddlID.SelectedItem.Text.IndexOf("["))
                    name = name.Replace("]", "")
                    name = name.Replace("[", "")
                    command.Parameters.AddWithValue("@StaffName", name)
                    command.Parameters.AddWithValue("@CostValue", txtCostValue.Text)
                    command.Parameters.AddWithValue("@RecordNo", txtRecordNo.Text)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command.Parameters.AddWithValue("@CreateDeviceID", "")
                    command.Parameters.AddWithValue("@CreateSource", "")
                    command.Parameters.AddWithValue("@FlowFrom", "")
                    command.Parameters.AddWithValue("@FlowTo", "")
                    command.Parameters.AddWithValue("@EditSource", "")
                    command.Parameters.AddWithValue("@DeleteStatus", "")
                    command.Parameters.AddWithValue("@LastEditDevice", "")


                    command.Connection = conn

                    command.ExecuteNonQuery()

                    MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, "Error!!!" + ex.Message.ToString, "str")
            End Try
            EnableControls()

        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text
                command2.CommandText = "SELECT * FROM tblservicerecordstaff where recordno=@recordno and staffid=@staffid and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@recordno", txtRecordNo.Text)
                command2.Parameters.AddWithValue("@staffid", ddlID.SelectedValue.ToString)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    MessageBox.Message.Alert(Page, "Selected Staff already assigned for this service!!!", "str")

                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblservicerecordstaff where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "UPDATE tblservicerecordstaff SET StaffID = @StaffID,StaffName = @StaffName,CostValue = @CostValue,RecordNo = @RecordNo,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE  rcno=" & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@StaffID", ddlID.SelectedValue.ToString)
                        Dim name As String = ddlID.SelectedItem.Text.Substring(ddlID.SelectedItem.Text.IndexOf("["))
                        name = name.Replace("]", "")
                        name = name.Replace("[", "")
                        command.Parameters.AddWithValue("@StaffName", name)
                        command.Parameters.AddWithValue("@CostValue", txtCostValue.Text)
                        command.Parameters.AddWithValue("@RecordNo", txtRecordNo.Text)
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Connection = conn

                        command.ExecuteNonQuery()

                        MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                    End If
                End If

                conn.Close()

            Catch ex As Exception

                MessageBox.Message.Alert(Page, "Error!!! " + ex.ToString, "str")
            End Try
            EnableControls()

        End If

        GridView1.DataSourceID = "SqlDSServiceStaff"
        MakeMeNull()
    End Sub
End Class
