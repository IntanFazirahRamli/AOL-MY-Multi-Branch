Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Drawing
Imports System

Public Class Master_MobileDevice
    Inherits System.Web.UI.Page

    Public rcno As String
    Public chSTrcno As String
    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""

        ddlCountry.Items.Clear()
        ddlCountry.Items.Add("--SELECT--")

        Dim Query As String
        Query = "SELECT Country, webServiceURL, Active,rcno FROM tblmobiledevicecountry where Active = 'Y'"

        PopulateDropDownList(Query, "Country", "rcno", ddlCountry)


        txtMode.Text = ""
        txtDeviceId.Text = ""
        ddlStatus.SelectedIndex = 0
        txtIMEI.Text = ""
        txtStaff.Text = ""
        txtDeviceType.Text = ""
        txtRemarks.Text = ""
        txtRcno.Text = ""
        txtCreatedOn.Text = ""
        txtLastModifiedOn.Text = ""
        ddlCountry.SelectedIndex = 0
        txtWebServiceURL.Text = ""
        '  ddlCountry.SelectedIndex = 0
        'txtWebServiceURLUpdate.Text = ""
        txtEmail.Text = ""

        txtStaffNameNew.Text = ""
        txtMobileNo.Text = ""
        txtConfirmationCode.Text = ""

    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black
        'btnEdit.Enabled = True
        'btnEdit.ForeColor = System.Drawing.Color.Black
        'btnUpdateURL.Enabled = True
        'btnUpdateURL.ForeColor = System.Drawing.Color.Black
        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnUpdateURL.Enabled = False
        btnUpdateURL.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        'btnPrint.Enabled = True
        'btnPrint.ForeColor = System.Drawing.Color.Black
        txtDeviceId.Enabled = False
        ddlStatus.Enabled = False
        txtIMEI.Enabled = False
        txtStaff.Enabled = False
        txtDeviceType.Enabled = False
        txtRemarks.Enabled = False
        ddlCountry.Enabled = False
        txtWebServiceURL.Enabled = False
        txtEmail.Enabled = False

        txtStaffNameNew.Enabled = False
        txtMobileNo.Enabled = False

        AccessControl()
    End Sub

    Private Sub DisableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black

        btnADD.Enabled = False
        btnADD.ForeColor = System.Drawing.Color.Gray

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnUpdateURL.Enabled = False
        btnUpdateURL.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray
        txtDeviceId.Enabled = False
        ddlStatus.Enabled = True
        txtIMEI.Enabled = True
        txtStaff.Enabled = True
        txtDeviceType.Enabled = True
        txtRemarks.Enabled = True
        ddlCountry.Enabled = True
        txtWebServiceURL.Enabled = False
        txtEmail.Enabled = True

        txtStaffNameNew.Enabled = True
        txtMobileNo.Enabled = True

        AccessControl()
    End Sub

    Private Sub AccessControl()
        Try
            '''''''''''''''''''Access Control 
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT x1721Add, x1721Edit, x1721Delete, x1721Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0156, x0156Add, x0156Edit,x0156Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()
                If dt.Rows.Count > 0 Then

                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0156"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0156")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If


                    If Not IsDBNull(dt.Rows(0)("x0156Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0156Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0156Add").ToString()
                        End If
                    End If

                    'Me.btnInsert.Enabled = vpSec2412Add
                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0156Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0156Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0156Edit").ToString()
                            End If
                        End If

                        'If Not IsDBNull(dt.Rows(0)("x0156Delete")) Then
                        '    If String.IsNullOrEmpty(dt.Rows(0)("x0156Delete")) = False Then
                        '        Me.btnDelete.Enabled = dt.Rows(0)("x0156Delete").ToString()
                        '    End If
                        'End If

                    Else
                        Me.btnEdit.Enabled = False
                        'Me.btnDelete.Enabled = False
                    End If

                    If btnADD.Enabled = True Then
                        btnADD.ForeColor = System.Drawing.Color.Black
                    Else
                        btnADD.ForeColor = System.Drawing.Color.Gray
                    End If


                    If btnEdit.Enabled = True Then
                        btnEdit.ForeColor = System.Drawing.Color.Black
                    Else
                        btnEdit.ForeColor = System.Drawing.Color.Gray
                    End If

                    'If btnDelete.Enabled = True Then
                    '    btnDelete.ForeColor = System.Drawing.Color.Black
                    'Else
                    '    btnDelete.ForeColor = System.Drawing.Color.Gray
                    'End If


                    If btnPrint.Enabled = True Then
                        btnPrint.ForeColor = System.Drawing.Color.Black
                    Else
                        btnPrint.ForeColor = System.Drawing.Color.Gray
                    End If


                End If
            End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MOBILEDEVICE - " + txtCreatedBy.Text, "GridView1_PageIndexChanging", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    ' TryCast(e.Row.Cells(6).FindControl("btnEndPoint"), Button).Text = "End"


        '    Dim lblrcno As Label = DirectCast(e.Row.FindControl("Label1"), Label)

        '    Dim conn As MySqlConnection = New MySqlConnection()

        '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        '    conn.Open()
        '    Dim command1 As MySqlCommand = New MySqlCommand

        '    command1.CommandType = CommandType.Text

        '    command1.CommandText = "SELECT * FROM tblmobiledeviceendpoint where mobiledevicercno=" & Convert.ToInt32(lblrcno.Text)
        '    command1.Connection = conn

        '    Dim dr As MySqlDataReader = command1.ExecuteReader()

        '    ' dr.Read()
        '    Dim dt As New DataTable
        '    dt.Load(dr)

        '    If dt.Rows.Count > 0 Then

        '        TryCast(e.Row.Cells(6).FindControl("btnEndPoint"), Button).Text = "ENDPOINT [" + dt.Rows.Count.ToString + "]"
        '    Else
        '        TryCast(e.Row.Cells(6).FindControl("btnEndPoint"), Button).Text = "ENDPOINT"
        '    End If
        '    dr.Close()
        '    dt.Clear()
        '    dt.Dispose()
        '    command1.Dispose()

        '    conn.Close()
        '    conn.Dispose()
        'End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        'lblAlert.Text = "TEST"
        'Return

        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If
        EnableControls()

        MakeMeNull()
        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()


        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblmobiledevice where rcno=" & Convert.ToInt32(txtRcno.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        ' dr.Read()
        Dim dt As New DataTable
        dt.Load(dr)


        If dt.Rows.Count > 0 Then
            txtDeviceId.Text = dt.Rows(0)("DeviceId").ToString
            txtDeviceType.Text = dt.Rows(0)("DeviceType").ToString
            txtIMEI.Text = dt.Rows(0)("IMEI").ToString

            txtConfirmationCode.Text = dt.Rows(0)("Rcno").ToString
            If String.IsNullOrEmpty(dt.Rows(0)("StaffID").ToString) = False Then
                txtStaff.Text = dt.Rows(0)("StaffID").ToString
            End If
            If String.IsNullOrEmpty(dt.Rows(0)("Country").ToString) = False Then
                ddlCountry.SelectedItem.Text = dt.Rows(0)("Country").ToString
            End If

            txtRemarks.Text = dt.Rows(0)("Remarks").ToString
            ddlStatus.SelectedValue = dt.Rows(0)("Status").ToString
            txtWebServiceURL.Text = dt.Rows(0)("WebServiceURL").ToString

            txtStaffNameNew.Text = dt.Rows(0)("StaffName").ToString
            txtMobileNo.Text = dt.Rows(0)("MobileNo").ToString
            txtEmail.Text = dt.Rows(0)("Email").ToString

        End If

        dt.Dispose()
        dr.Close()
        command1.Dispose()
        conn.Close()
        conn.Dispose()

        'If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
        '    txtDeviceId.Text = ""
        'Else
        '    txtDeviceId.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        'End If

        'If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
        '    txtDeviceType.Text = ""
        'Else
        '    txtDeviceType.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        'End If

        'If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
        '    txtIMEI.Text = ""
        'Else
        '    txtIMEI.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        'End If

        'If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
        '    txtStaff.Text = ""
        'Else
        '    txtStaff.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
        '    ddlCountry.SelectedIndex = 0
        'Else
        '    ddlCountry.SelectedValue = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString.ToUpper()
        '    'End If
        'If GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
        '    txtRemarks.Text = ""
        'Else
        '    txtRemarks.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(8).Text).ToString
        'End If

        'If GridView1.SelectedRow.Cells(9).Text = "&nbsp;" Then
        '    ddlStatus.SelectedIndex = 0
        'Else
        '    ddlStatus.SelectedValue = Server.HtmlDecode(GridView1.SelectedRow.Cells(9).Text).ToString.ToUpper()
        'End If

        'txtWebServiceURL.Text = DirectCast(GridView1.Rows(editindex).FindControl("lblURL"), Label).Text

        'If GridView1.SelectedRow.Cells(13).Text = "&nbsp;" Then
        '    txtStaffNameNew.Text = ""
        'Else
        '    txtStaffNameNew.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(13).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(14).Text = "&nbsp;" Then
        '    txtMobileNo.Text = ""
        'Else
        '    txtMobileNo.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(14).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(15).Text = "&nbsp;" Then
        '    txtEmail.Text = ""
        'Else
        '    txtEmail.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(15).Text).ToString
        'End If
        ''txtMode.Text = "Edit"
        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnUpdateURL.Enabled = True
        btnUpdateURL.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black
        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            AccessControl()
        End If
    End Sub

    Private Function RetrieveURL(Country As String) As String
        Dim url As String = ""
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblmobiledevicecountry where country='" & Country & "'"
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        ' dr.Read()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            url = dt.Rows(0)("WebServiceURL").ToString
        End If

        Return url
    End Function

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()
        DisableControls()
        txtMode.Text = "New"
        lblMessage.Text = "ACTION: ADD RECORD"
        txtDeviceType.Focus()
        'ddlCountry.Text = ConfigurationManager.AppSettings("DomainName").ToString()
        'If ddlCountry.Text = "SINGAPORE" Then
        '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceSingapore").ToString()
        'ElseIf ddlCountry.Text = "MALAYSIA" Then
        '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceMalaysia").ToString()
        'End If

    End Sub

    Private Function GenerateIdentityNo() As String
        Dim temp As String = "NO"

        Dim otp As Int64 = 0
        Do While temp = "NO"
            otp = 0
            otp = GenerateOTP()

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand()
            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT IMEI FROM tblMobileDevice WHERE IMEI='" & otp.ToString & "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)
            '  InsertIntoTblWebEventLog("MobileDevice", "Save_Click", otp.ToString, dt.Rows.Count.ToString)

            If dt.Rows.Count > 0 Then
                temp = "NO"
            Else
                temp = "YES"
                Exit Do
            End If

            dt.Clear()
            dr.Close()
            command1.Dispose()

            conn.Close()
            conn.Dispose()
        Loop
        Return otp.ToString

    End Function

    Private Function GenerateOTP() As Int64
        Dim rnd As New Random()

        Dim otp As Int64 = rnd.Next(99999999, 999999999)

        Return otp
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            ddlCountry.Items.Clear()
            ddlCountry.Items.Add("--SELECT--")

            Dim Query As String
            Query = "SELECT Country, webServiceURL, Active,rcno FROM tblmobiledevicecountry where Active = 'Y'"

            PopulateDropDownList(Query, "Country", "rcno", ddlCountry)

            ' PopulateDropDownList(Query, "Country", "rcno", ddlCountryUpdate)
            'PopulateDropDownList(Query, "Country", "Country", ddlCountryAddEndPoint)

            txt.Text = "SELECT * FROM tblMobileDevice WHERE (Rcno <> 0) order by rcno desc"

        End If

        '   ScriptManager.RegisterStartupScript(Page, Me.[GetType](), "Key", "<script>MakeStaticHeader('" + GridView2.ClientID + "', 500, 1350 , 50 ,false); </script>", False)

    End Sub

    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Dim con As MySqlConnection = New MySqlConnection()

        con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Using con As New MySqlConnection(constr)
        Using cmd As New MySqlCommand(query)
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            con.Open()
            ddl.DataSource = cmd.ExecuteReader()
            ddl.DataTextField = textField.Trim()
            ddl.DataValueField = valueField.Trim()
            ddl.DataBind()
            con.Close()
            con.Dispose()
        End Using
        'End Using
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If ddlStatus.SelectedValue = "" Then
            lblAlert.Text = "STATUS CANNOT BE BLANK"
            Return
        End If


        If ddlCountry.SelectedValue = "" Then
            lblAlert.Text = "COUNTRY CANNOT BE BLANK"
            Return
        End If
        '   InsertIntoTblWebEventLog("MobileDevice", "Save_Click", txtMode.Text, "1")

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text
                'command1.CommandText = "SELECT * FROM tblMobileDevice where Upper(IMEI)=@IMEI"
                'command1.Parameters.AddWithValue("@IMEI", txtIMEI.Text.Trim.ToUpper)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then
                '    lblAlert.Text = "RECORD ALREADY EXISTS"
                'Else
                Dim DeviceId As String = GenerateDeviceId()
                txtDeviceId.Text = DeviceId
                '    InsertIntoTblWebEventLog("MobileDevice", "Save_Click", txtDeviceId.Text, "2")

                txtIMEI.Text = GenerateIdentityNo()

                If txtIMEI.Text = "" Then
                    lblAlert.Text = "IMEI CANNOT BE BLANK"
                    Return
                End If

                '   InsertIntoTblWebEventLog("MobileDevice", "Save_Click", txtIMEI.Text, "3")

                Dim command As MySqlCommand = New MySqlCommand
                command.CommandType = CommandType.Text

                Dim qry As String = "INSERT INTO tblmobiledevice(IMEI,Status,StaffId,DeviceType,DeviceId,Remarks,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,Country,WebServiceURL,staffname,mobileno,Email)"
                qry = qry + "VALUES(@IMEI,@Status,@StaffId,@DeviceType,@DeviceId,@Remarks,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@Country,@WebServiceURL,@staffname,@mobileno,@Email);"
                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.ToString)
                command.Parameters.AddWithValue("@StaffId", txtStaff.Text.Trim)

                If (Not String.IsNullOrEmpty(txtDeviceType.Text.Trim)) Then
                    command.Parameters.AddWithValue("@DeviceType", txtDeviceType.Text.Trim.ToUpper())
                Else
                    command.Parameters.AddWithValue("@DeviceType", txtDeviceType.Text.Trim)
                End If

                If (Not String.IsNullOrEmpty(txtRemarks.Text.Trim)) Then
                    command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim.ToUpper())
                Else
                    command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim)
                End If

                command.Parameters.AddWithValue("@DeviceId", DeviceId)
                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                'command.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now.ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                'command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                command.Parameters.AddWithValue("@Country", ddlCountry.SelectedItem.ToString)
                command.Parameters.AddWithValue("@WebServiceURL", txtWebServiceURL.Text.Trim)
                command.Parameters.AddWithValue("@StaffName", txtStaffNameNew.Text.Trim)
                command.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim)
                command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim)

                command.Parameters.AddWithValue("@IMEI", txtIMEI.Text.Trim.ToUpper)

                command.Connection = conn
                command.ExecuteNonQuery()
                txtRcno.Text = command.LastInsertedId
                txtConfirmationCode.Text = txtRcno.Text.Trim
                '      InsertIntoTblWebEventLog("MobileDevice", "Save_Click", txtRcno.Text, "4")


                'ADD ENDPOINTS

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text
                '  InsertIntoTblWebEventLog("MobileDevice", "btnSaveEndPoint_Click", "2", lblidCountryRcNo.Text)

                command2.CommandText = "INSERT INTO tblmobiledeviceendpoint(MobileDeviceRcno,EndPointID,CreatedBy,CreatedOn)VALUES(@MobileDeviceRcno,@EndPointID,@CreatedBy,@CreatedOn);"
                command2.Parameters.Clear()
                command2.Parameters.AddWithValue("@MobileDeviceRcno", txtRcno.Text)
                command2.Parameters.AddWithValue("@EndPointID", ddlCountry.SelectedValue)
                command2.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                command2.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                command2.Connection = conn

                command2.ExecuteNonQuery()

                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""
                SqlDataSource1.SelectCommand = "SELECT * FROM tblMobileDevice WHERE (Rcno <> 0) order by rcno desc"

                '   End If
                conn.Close()

                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILEDEV", txtIMEI.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

            Catch ex As Exception
                Dim exstr As String
                exstr = ex.ToString
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return
            End If
            If txtIMEI.Text = "" Then
                lblAlert.Text = "IMEI CANNOT BE BLANK"
                Return
            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblMobileDevice where Upper(IMEI)=@IMEI and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@IMEI", txtIMEI.Text.Trim.ToUpper)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                Else
                    Dim command1 As MySqlCommand = New MySqlCommand
                    command1.CommandType = CommandType.Text
                    command1.CommandText = "SELECT * FROM tblMobileDevice where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "update tblMobileDevice set Status=@Status,Country=@Country,WebServiceURL=@WebServiceURL,Remarks=@Remarks,StaffId=@StaffId,DeviceType=@DeviceType,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn,staffname=@staffname,mobileno=@mobileno,Email=@Email where rcno=" & Convert.ToInt32(txtRcno.Text)
                        command.CommandText = qry
                        command.Parameters.Clear()

                        '       command.Parameters.AddWithValue("@IMEI", txtIMEI.Text.Trim.ToUpper)
                        command.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@StaffId", txtStaff.Text.Trim)

                        If (Not String.IsNullOrEmpty(txtDeviceType.Text.Trim)) Then
                            command.Parameters.AddWithValue("@DeviceType", txtDeviceType.Text.Trim.ToUpper())
                        Else
                            command.Parameters.AddWithValue("@DeviceType", txtDeviceType.Text.Trim)
                        End If

                        If (Not String.IsNullOrEmpty(txtRemarks.Text.Trim)) Then
                            command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim.ToUpper())
                        Else
                            command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim)
                        End If

                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        'command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString("yyyy-MM-dd"))
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                        command.Parameters.AddWithValue("@Country", ddlCountry.SelectedItem.ToString)
                        command.Parameters.AddWithValue("@WebServiceURL", txtWebServiceURL.Text.Trim)
                        command.Parameters.AddWithValue("@StaffName", txtStaffNameNew.Text.Trim)
                        command.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim)
                        command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim)

                        command.Connection = conn
                        command.ExecuteNonQuery()
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""
                        txtMode.Text = ""
                        SqlDataSource1.SelectCommand = txt.Text

                    End If
                End If

                conn.Close()
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILEDEV", txtIMEI.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
        End If
        SqlDataSource1.SelectCommand = "SELECT * FROM tblMobileDevice  order by createdon desc"
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        ' GridView1.DataSourceID = "SqlDataSource1"
    End Sub

    'Function GenerateDeviceId() As String
    '    Dim DeviceId As String = ""
    '    Dim LastRecDeviceId As String = ""
    '    Try
    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()
    '        Dim command1 As MySqlCommand = New MySqlCommand()
    '        command1.CommandType = CommandType.Text
    '        command1.CommandText = "SELECT DeviceId FROM tblMobileDevice order by RcNo desc Limit 1"
    '        command1.Connection = conn
    '        Dim dr As MySqlDataReader = command1.ExecuteReader()
    '        If dr.HasRows Then
    '            While dr.Read()
    '                LastRecDeviceId = dr(0).ToString()
    '                If (Not String.IsNullOrEmpty(LastRecDeviceId)) Then
    '                    Dim s As String = LastRecDeviceId.Substring(LastRecDeviceId.Length - 5)
    '                    Dim num As Integer = Convert.ToInt32(s) + 1
    '                    Select Case (num.ToString().Length)
    '                        Case 1
    '                            DeviceId = "AX-0000" + num.ToString
    '                        Case 2
    '                            DeviceId = "AX-000" + num.ToString
    '                        Case 3
    '                            DeviceId = "AX-00" + num.ToString
    '                        Case 4
    '                            DeviceId = "AX-0" + num.ToString
    '                        Case 5
    '                            DeviceId = "AX-" + num.ToString
    '                    End Select
    '                End If
    '            End While
    '        Else
    '            DeviceId = "AX-00001"
    '        End If

    '        Return DeviceId
    '    Catch ex As Exception
    '        DeviceId = ex.Message.ToString()
    '        Return DeviceId
    '    End Try
    'End Function

    Function GenerateDeviceId() As String
        Dim DeviceId As String = ""
        Dim LastRecDeviceId As String = ""
        Try
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand()
            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT DeviceId FROM tblMobileDevice order by RcNo desc Limit 1"
            command1.Connection = conn
            Dim dr As MySqlDataReader = command1.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    LastRecDeviceId = dr(0).ToString()
                    If (Not String.IsNullOrEmpty(LastRecDeviceId)) Then
                        Dim s As String = LastRecDeviceId.Substring(LastRecDeviceId.Length - 5)
                        Dim num As Integer = Convert.ToInt32(s) + 1
                        Select Case (num.ToString().Length)
                            Case 1
                                DeviceId = "AXMD-0000" + num.ToString
                            Case 2
                                DeviceId = "AXMD-000" + num.ToString
                            Case 3
                                DeviceId = "AXMD-00" + num.ToString
                            Case 4
                                DeviceId = "AXMD-0" + num.ToString
                            Case 5
                                DeviceId = "AXMD-" + num.ToString
                        End Select
                    End If
                End While
            Else
                DeviceId = "AXMD-00001"
            End If

            Return DeviceId
        Catch ex As Exception
            DeviceId = ex.Message.ToString()
            Return DeviceId
        End Try
    End Function

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtRcno.Text = "" Then
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return
        End If
        DisableControls()
        '  ddlCountry.Enabled = False
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        MakeMeNull()
        EnableControls()
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")
    End Sub

    'Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    '    lblMessage.Text = ""
    '    If txtRcno.Text = "" Then
    '        lblAlert.Text = "SELECT RECORD TO DELETE"
    '        Return
    '    End If

    '    lblMessage.Text = "ACTION: DELETE RECORD"
    '    Dim confirmValue As String = Request.Form("confirm_value")
    '    If confirmValue = "Yes" Then
    '        Try
    '            Dim conn As MySqlConnection = New MySqlConnection()

    '            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '            conn.Open()
    '            Dim command1 As MySqlCommand = New MySqlCommand
    '            command1.CommandType = CommandType.Text
    '            command1.CommandText = "SELECT * FROM tblMobileDevice where rcno=" & Convert.ToInt32(txtRcno.Text)
    '            command1.Connection = conn

    '            Dim dr As MySqlDataReader = command1.ExecuteReader()
    '            Dim dt As New DataTable
    '            dt.Load(dr)

    '            If dt.Rows.Count > 0 Then

    '                Dim command As MySqlCommand = New MySqlCommand
    '                command.CommandType = CommandType.Text
    '                Dim qry As String = "delete from tblMobileDevice where rcno=" & Convert.ToInt32(txtRcno.Text)

    '                command.CommandText = qry
    '                command.Connection = conn
    '                command.ExecuteNonQuery()
    '                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
    '                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILEDEV", txtIMEI.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

    '            End If
    '            conn.Close()

    '        Catch ex As Exception
    '            MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        End Try
    '        EnableControls()
    '        GridView1.DataSourceID = "SqlDataSource1"
    '        MakeMeNull()
    '        lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
    '    End If

    'End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Response.Redirect("RV_MasterMobileDevice.aspx")
    End Sub

    Protected Sub btnPopUpStaffSearch_Click(sender As Object, e As EventArgs)
        If txtStaffName.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter Staff name", "str")
        Else
            sqlDSStaff.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 and upper(Name) Like '%" + txtStaffName.Text.Trim.ToUpper + "%'"
            sqlDSStaff.DataBind()
            gvStaff.DataBind()
            modPopUPStaff.Show()
        End If
    End Sub

    Protected Sub btnPopUpStaffReset_Click(sender As Object, e As EventArgs)
        txtStaffName.Text = ""
        sqlDSStaff.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0"
        sqlDSStaff.DataBind()
        gvStaff.DataBind()
        modPopUPStaff.Show()
    End Sub

    Protected Sub gvStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStaff.SelectedIndexChanged
        If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtStaff.Text = " "
        Else
            txtStaff.Text = gvStaff.SelectedRow.Cells(1).Text
        End If
    End Sub

    Protected Sub gvStaff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStaff.PageIndexChanging
        gvStaff.PageIndex = e.NewPageIndex
        If txtStaffName.Text.Trim = "" Then
            sqlDSStaff.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 "
        Else
            sqlDSStaff.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 And upper(Name) Like '%" + txtStaffName.Text.Trim.ToUpper + "%'"
        End If

        sqlDSStaff.DataBind()
        gvStaff.DataBind()
        modPopUPStaff.Show()
    End Sub

    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        'txtSearchText.Text = txtSearch.Text


        Dim qry As String = "select * from tblmobiledevice where rcno <> 0"

        If String.IsNullOrEmpty(txtSearch.Text) = False Then
            qry = qry + " and (IMEI like '%" + txtSearch.Text.Trim + "%'"
            qry = qry + " or DeviceId like '%" + txtSearch.Text.Trim + "%'"
            qry = qry + " or Upper(StaffId) like '%" + txtSearch.Text.Trim.ToUpper() + "%'"
            qry = qry + " or Upper(remarks) like '%" + txtSearch.Text.Trim.ToUpper() + "%')"
        End If

        qry = qry + " order by createdon desc;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        If GridView1.Rows.Count > 0 Then
            GridView1.SelectedIndex = 0
            GridView1_SelectedIndexChanged(sender, e)
        End If

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearch.Text.Trim + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        txtSearch.Text = "Search Here"


    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        MakeMeNull()
        EnableControls()
        txt.Text = "SELECT * FROM tblMobileDevice WHERE (Rcno <> 0) order by rcno desc"
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        lblMessage.Text = ""
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If (e.CommandName = "ChST_Click") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)


        End If
    End Sub

    Protected Sub btnChST_Click(sender As Object, e As EventArgs)
        Try
            Dim row As GridViewRow = DirectCast(DirectCast(sender, Button).Parent.Parent, GridViewRow)

            txtChStatusRcNo.Text = DirectCast(row.FindControl("btnChST"), Button).CommandArgument ''row.Cells(8).Text


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblmobiledevice where rcno=" & Convert.ToInt32(txtChStatusRcNo.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            ' dr.Read()
            Dim dt As New DataTable
            dt.Load(dr)


            If dt.Rows.Count > 0 Then
                txtChStatusDeviceId.Text = dt.Rows(0)("DeviceId").ToString

                Dim status As String = dt.Rows(0)("Status").ToString
                If (status <> "PENDING") Then
                    rbtnPopUpChStatus.SelectedValue = status
                End If
                txtPopUpChStatusDeviceId.Text = dt.Rows(0)("DeviceId").ToString

                txtPopUpChStatusDeviceType.Text = dt.Rows(0)("DeviceType").ToString
                txtPopUpChStatusIMEI.Text = dt.Rows(0)("IMEI").ToString
                txtPopUpChStatusRemarks.Text = dt.Rows(0)("Remarks").ToString
                '     InsertIntoTblWebEventLog("Mobile", "CHST", dt.Rows(0)("StaffID").ToString.Trim.ToUpper, "")

                If ddlPopUpChStatusStaff.Items.FindByText(dt.Rows(0)("StaffID").ToString) Is Nothing Then
                    ddlPopUpChStatusStaff.Items.Add(dt.Rows(0)("StaffID").ToString)
                    ddlPopUpChStatusStaff.Text = dt.Rows(0)("StaffID").ToString
                Else
                    ddlPopUpChStatusStaff.Text = dt.Rows(0)("StaffID").ToString.Trim().ToUpper()
                End If

                'If String.IsNullOrEmpty(dt.Rows(0)("StaffID").ToString) = False Then
                '    ddlPopUpChStatusStaff.Text = dt.Rows(0)("StaffID").ToString.ToUpper
                'End If

                'If String.IsNullOrEmpty(dt.Rows(0)("Country").ToString) = False Then
                '    ddlCountryUpdate.SelectedItem.Text = dt.Rows(0)("Country").ToString
                'End If
                'txtWebServiceURLUpdate.Text = RetrieveURL(ddlCountryUpdate.SelectedItem.Text)

            End If

            dt.Dispose()
            dr.Close()
            command1.Dispose()
            conn.Close()
            conn.Dispose()

            mdlPopUpChStatus.Show()

            'txtChStatusDeviceId.Text = row.Cells(1).Text

            'Dim status As String = row.Cells(9).Text
            'If (status <> "PENDING") Then
            '    rbtnPopUpChStatus.SelectedValue = status
            'End If

            'txtPopUpChStatusDeviceId.Text = row.Cells(1).Text

            'If (row.Cells(2).Text = "&nbsp;") Then
            '    txtPopUpChStatusDeviceType.Text = ""
            'Else
            '    txtPopUpChStatusDeviceType.Text = row.Cells(2).Text
            'End If

            'txtPopUpChStatusIMEI.Text = row.Cells(3).Text

            'If (row.Cells(8).Text = "&nbsp;") Then
            '    txtPopUpChStatusRemarks.Text = ""
            'Else
            '    txtPopUpChStatusRemarks.Text = row.Cells(8).Text
            'End If

            'Dim st As String = row.Cells(5).Text
            'If (String.IsNullOrEmpty(st) Or st = "&nbsp;") Then
            '    ddlPopUpChStatusStaff.SelectedIndex = 0
            'Else
            '    ddlPopUpChStatusStaff.SelectedValue = st
            'End If
            'If (String.IsNullOrEmpty(row.Cells(7).Text) Or row.Cells(7).Text = "&nbsp;") Then
            '    ddlCountryUpdate.SelectedIndex = 0
            'Else
            '    ddlCountryUpdate.SelectedValue = row.Cells(7).Text
            'End If
            'If ddlCountryUpdate.Text = "SINGAPORE" Then
            '    txtWebServiceURLUpdate.Text = ConfigurationManager.AppSettings("WebServiceSingapore").ToString()
            'ElseIf ddlCountryUpdate.Text = "SINGAPOREBETA" Then
            '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceSingaporeBeta").ToString()
            'ElseIf ddlCountryUpdate.Text = "MALAYSIA" Then
            '    txtWebServiceURLUpdate.Text = ConfigurationManager.AppSettings("WebServiceMalaysia").ToString()
            'End If
            'mdlPopUpChStatus.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MOBILE DEVICE - " + txtCreatedBy.Text, "GridView1_SelectedIndexChanged", ex.Message.ToString, "")

        End Try
    End Sub


    Protected Sub btnEndPoint_Click(sender As Object, e As EventArgs)
        ' lblAlert.Text = "Hi"
        Try
            Dim row As GridViewRow = DirectCast(DirectCast(sender, Button).Parent.Parent, GridViewRow)

            txtEndPointRcno.Text = DirectCast(row.FindControl("btnEndPoint"), Button).CommandArgument ''row.Cells(8).Text


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblmobiledevice where rcno=" & Convert.ToInt32(txtEndPointRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            ' dr.Read()
            Dim dt As New DataTable
            dt.Load(dr)


            If dt.Rows.Count > 0 Then
                txtEndPointDeviceID.Text = dt.Rows(0)("DeviceId").ToString
                txtEndPointDeviceType.Text = dt.Rows(0)("DeviceType").ToString
                txtEndPointIMEI.Text = dt.Rows(0)("IMEI").ToString
            End If

            dt.Dispose()
            dr.Close()
            command1.Dispose()
            conn.Close()
            conn.Dispose()
            Dim qry As String = "select True as Selected,B.Country,B.WebServiceUrl,B.Rcno from tblmobiledeviceendpoint A INNER JOIN tblmobiledevicecountry B on A.endpointid=B.rcno where mobiledevicercno=" & Convert.ToInt32(txtEndPointRcno.Text) & " union "
            qry = qry + "select false as Selected,Country,WebServiceUrl,rcno from tblmobiledevicecountry where Active='Y' AND rcno not in "
            qry = qry + "(select B.rcno from tblmobiledeviceendpoint A INNER JOIN tblmobiledevicecountry B on A.endpointid=B.rcno where mobiledevicercno=" & Convert.ToInt32(txtEndPointRcno.Text) & ")"

            SqlDataSource2.SelectCommand = qry
            GridView2.DataSourceID = "SqlDataSource2"
            GridView2.DataBind()

            mdlPopupEndPoint.Show()


        Catch ex As Exception
            InsertIntoTblWebEventLog("MOBILE DEVICE - " + txtCreatedBy.Text, "btnEndPoint_Click", ex.Message.ToString, "")

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

            lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("MOBILE DEVICE" + txtCreatedBy.Text, "InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub

    Protected Sub btnPopUpChStatusOk_Click(sender As Object, e As EventArgs)
        If (txtChStatusRcNo.Text = "") Then
            Exit Sub
        End If

        Dim conn As MySqlConnection = New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.Text
        command1.CommandText = "SELECT * FROM tblMobileDevice where rcno=" & Convert.ToInt32(txtChStatusRcNo.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        If (dr.HasRows) Then
            dr.Close()
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text
            Dim qry As String = "update tblMobileDevice set Status=@Status,StaffId=@StaffId,Remarks=@Remarks,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtChStatusRcNo.Text)
            command.CommandText = qry
            command.Parameters.Clear()

            If (rbtnPopUpChStatus.SelectedValue.ToString() = "") Then
                command.Parameters.AddWithValue("@Status", "PENDING")
            Else
                command.Parameters.AddWithValue("@Status", rbtnPopUpChStatus.SelectedValue.ToString())
            End If

            If (ddlPopUpChStatusStaff.SelectedIndex = 0) Then
                command.Parameters.AddWithValue("@StaffId", "")
            Else
                command.Parameters.AddWithValue("@StaffId", ddlPopUpChStatusStaff.SelectedValue.ToString())
            End If

            If (Not String.IsNullOrEmpty(txtPopUpChStatusRemarks.Text.Trim)) Then
                command.Parameters.AddWithValue("@Remarks", txtPopUpChStatusRemarks.Text.Trim.ToUpper())
            Else
                command.Parameters.AddWithValue("@Remarks", txtPopUpChStatusRemarks.Text.Trim)
            End If
            'command.Parameters.AddWithValue("@Country", ddlCountryUpdate.Text)
            'command.Parameters.AddWithValue("@WebServiceURL", txtWebServiceURLUpdate.Text)
            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
            command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString("yyyy-MM-dd"))



            'ddlCountry.SelectedItem.Text = ddlCountryUpdate.Text
            'txtWebServiceURL.Text = txtWebServiceURLUpdate.Text





            command.Connection = conn
            command.ExecuteNonQuery()
            lblMessage.Text = "DEVICE " & txtChStatusDeviceId.Text & " UPDATED SUCCESSFULLY"
            lblAlert.Text = ""
        End If
        conn.Close()
        ''  CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILEDEV", txtIMEI.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        ' GridView1.DataSourceID = "SqlDataSource1"
    End Sub

    Protected Sub btnPopUPChStatusCancel_Click(sender As Object, e As EventArgs)
        mdlPopUpChStatus.Hide()
    End Sub

    Protected Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If txtSearch.Text <> "Search Here" Then

            Dim qry As String = "select * from tblmobiledevice where rcno <> 0"

            If String.IsNullOrEmpty(txtSearch.Text) = False Then
                qry = qry + " and (IMEI like '%" + txtSearch.Text.Trim + "%'"
                qry = qry + " or DeviceId like '%" + txtSearch.Text.Trim + "%'"
                qry = qry + " or Upper(StaffId) like '%" + txtSearch.Text.Trim.ToUpper() + "%'"
                qry = qry + " or Upper(remarks) like '%" + txtSearch.Text.Trim.ToUpper() + "%')"
            End If

            qry = qry + " order by rcno desc;"
            txt.Text = qry

            MakeMeNull()
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                GridView1.SelectedIndex = 0
                GridView1_SelectedIndexChanged(sender, e)
            End If


            lblMessage.Text = "SEARCH CRITERIA : " + txtSearch.Text.Trim + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString
        Else
            txtSearch.Text = "Search Here"
        End If

    End Sub

    Protected Sub btnUpdateURL_Click(sender As Object, e As EventArgs) Handles btnUpdateURL.Click
        If ConfigurationManager.AppSettings("DomainName").ToString() = "SINGAPORE" Then
            lblQuery.Text = "Would you like to update WebService URL for the mobile device " + txtDeviceId.Text + "?" ' <br /><br /><br /><b>Country = " + ConfigurationManager.AppSettings("DomainName").ToString() + " <br /><br /><br /> WebServiceURL = " + ConfigurationManager.AppSettings("WebServiceSingapore").ToString() + " </b>"
        ElseIf ConfigurationManager.AppSettings("DomainName").ToString() = "SINGAPORE (Beta)" Then
            lblQuery.Text = "Would you like to update WebService URL for the mobile device " + txtDeviceId.Text + "?" ' <br /><br /><br /><b>Country = " + ConfigurationManager.AppSettings("DomainName").ToString() + " <br /><br /><br /> WebServiceURL = " + ConfigurationManager.AppSettings("WebServiceSingapore").ToString() + " </b>"

        ElseIf ConfigurationManager.AppSettings("DomainName").ToString() = "MALAYSIA" Then
            lblQuery.Text = "Would you like to update for all the records? <br /><br /><br /><b>Country = " + ConfigurationManager.AppSettings("DomainName").ToString() + " <br /><br /><br /> WebServiceURL = " + ConfigurationManager.AppSettings("WebServiceMalaysia").ToString() + " </b>"
        End If
        mdlPopupMsg.Show()

    End Sub

    Protected Sub btnConfirmOk_Click(sender As Object, e As EventArgs) Handles btnConfirmOk.Click

        'Try
        '    Dim conn As MySqlConnection = New MySqlConnection()

        '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        '    conn.Open()



        '    Dim command As MySqlCommand = New MySqlCommand

        '    command.CommandType = CommandType.Text
        '    Dim qry As String = "update tblMobileDevice set Country=@Country,WebServiceURL=@WebServiceURL where rcno=" + txtRcno.Text

        '    command.CommandText = qry
        '    command.Parameters.Clear()

        '    command.Parameters.AddWithValue("@Country", ddlCountryUpdate.Text)
        '    command.Parameters.AddWithValue("@WebServiceURL", txtWebServiceURLUpdate.Text)

        '    command.Connection = conn
        '    command.ExecuteNonQuery()
        '    lblMessage.Text = "WEB SERVICE URL UPDATED"
        '    lblAlert.Text = ""
        '    txtMode.Text = ""
        '    ddlCountry.SelectedItem.Text = ddlCountryUpdate.Text
        '    txtWebServiceURL.Text = txtWebServiceURLUpdate.Text

        '    conn.Close()
        '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILEDEV", txtIMEI.Text, "UPDATEWEBSERVICE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, ex.ToString, "str")
        'End Try

        'txt.Text = "SELECT * FROM tblMobileDevice WHERE (Rcno <> 0) order by DeviceId"
        'SqlDataSource1.SelectCommand = txt.Text
        'SqlDataSource1.DataBind()
        'GridView1.DataBind()
        'mdlPopupMsg.Hide()

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."

            Dim lblrcno As Label = DirectCast(e.Row.FindControl("Label1"), Label)

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblmobiledeviceendpoint where mobiledevicercno=" & Convert.ToInt32(lblrcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            ' dr.Read()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                TryCast(e.Row.Cells(6).FindControl("btnEndPoint"), Button).Text = "ENDPOINT [" + dt.Rows.Count.ToString + "]"
            Else
                TryCast(e.Row.Cells(6).FindControl("btnEndPoint"), Button).Text = "ENDPOINT"
            End If
            dr.Close()
            dt.Clear()
            dt.Dispose()
            command1.Dispose()

            conn.Close()
            conn.Dispose()
        End If
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        For Each row As GridViewRow In GridView1.Rows
            If row.RowIndex = GridView1.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#00ccff")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If

            End If
        Next
    End Sub

    Protected Sub ddlCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCountry.SelectedIndexChanged

        'Query = "SELECT Country, webServiceURL, Active FROM tblmobiledevicecountry where Active = 'Y'"

        'Dim IsLock As String
        'IsLock = ""
        txtWebServiceURL.Text = RetrieveURL(ddlCountry.SelectedItem.Text.ToUpper)

        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()

        'Dim command1 As MySqlCommand = New MySqlCommand

        'command1.CommandType = CommandType.Text
        'command1.CommandText = "SELECT webServiceURL FROM tblmobiledevicecountry where Country='" & ddlCountry.Text.ToUpper & "'"
        'command1.Connection = conn

        'Dim dr As MySqlDataReader = command1.ExecuteReader()
        'Dim dt As New DataTable
        'dt.Load(dr)


        'If dt.Rows.Count > 0 Then
        '    txtWebServiceURL.Text = dt.Rows(0)("webServiceURL").ToString
        'End If

        'conn.Close()
        'conn.Dispose()
        'command1.Dispose()
        'dt.Dispose()
        'dr.Close()

        'If ddlCountry.Text = "SINGAPORE" Then
        '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceSingapore").ToString()
        'ElseIf ddlCountry.Text = "SINGAPOREBETA" Then
        '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceSingaporeBeta").ToString()
        'ElseIf ddlCountry.Text = "MALAYSIA" Then
        '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceMalaysia").ToString()
        'End If

        'If ddlCountry.Text = "SINGAPORE" Then
        '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceSingapore").ToString()
        'ElseIf ddlCountry.Text = "SINGAPOREBETA" Then
        '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceSingaporeBeta").ToString()
        'ElseIf ddlCountry.Text = "MALAYSIA" Then
        '    txtWebServiceURL.Text = ConfigurationManager.AppSettings("WebServiceMalaysia").ToString()
        'End If
    End Sub

    'Protected Sub ddlCountryUpdate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCountryUpdate.SelectedIndexChanged

    '    txtWebServiceURLUpdate.Text = RetrieveURL(ddlCountryUpdate.SelectedItem.Text.ToUpper)
    '    'Dim conn As MySqlConnection = New MySqlConnection()

    '    'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    'conn.Open()

    '    'Dim command1 As MySqlCommand = New MySqlCommand

    '    'command1.CommandType = CommandType.Text
    '    'command1.CommandText = "SELECT webServiceURL FROM tblmobiledevicecountry where Country='" & ddlCountryUpdate.Text.ToUpper & "'"
    '    'command1.Connection = conn

    '    'Dim dr As MySqlDataReader = command1.ExecuteReader()
    '    'Dim dt As New DataTable
    '    'dt.Load(dr)


    '    'If dt.Rows.Count > 0 Then
    '    '    txtWebServiceURLUpdate.Text = dt.Rows(0)("webServiceURL").ToString
    '    'End If

    '    'conn.Close()
    '    'conn.Dispose()
    '    'command1.Dispose()
    '    'dt.Dispose()
    '    'dr.Close()
    '    'If ddlCountryUpdate.Text = "SINGAPORE" Then
    '    '    txtWebServiceURLUpdate.Text = ConfigurationManager.AppSettings("WebServiceSingapore").ToString()
    '    'ElseIf ddlCountryUpdate.Text = "SINGAPOREBETA" Then
    '    '    txtWebServiceURLUpdate.Text = ConfigurationManager.AppSettings("WebServiceSingaporeBeta").ToString()
    '    'ElseIf ddlCountryUpdate.Text = "MALAYSIA" Then
    '    '    txtWebServiceURLUpdate.Text = ConfigurationManager.AppSettings("WebServiceMalaysia").ToString()
    '    'End If

    '    mdlPopUpChStatus.Show()


    'End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return

        End If
        lblMessage.Text = "ACTION: DELETE RECORD"
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then

            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()



                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                ' Dim qry As String = "delete from tblperson where rcno=" & Convert.ToInt32(txtRcno.Text)
                Dim qry As String = "DELETE from tblmobiledevice where rcno=" & Convert.ToInt32(txtRcno.Text)

                command.CommandText = qry

                command.Connection = conn

                command.ExecuteNonQuery()



                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILEDEVICE", txtRcno.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtRcno.Text, "", txtRcno.Text)


                conn.Close()
                conn.Dispose()
                command.Dispose()


            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            '  GridView1.DataSourceID = "SqlDataSource1"
            MakeMeNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
            ddlStatus.SelectedIndex = 0

        End If
    End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Try
            '  ExportGridToExcel()

            ' If String.IsNullOrEmpty(txt.Text) = False Then

            Dim dt As DataTable = GetDataSet()
            Dim attachment As String = "attachment; filename=MobileDevice.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.ms-excel"
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("Windows-1252")
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

            '   CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERV", "", "btnExportToExcel_Click", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)
            Response.End()
            dt.Clear()


            'Response.ContentType = ContentType
            'Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            'Response.WriteFile(filePath)
            'Response.End()
            'Else
            'lblAlert.Text = "NO DATA TO EXPORT"
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog(Session("UserID"), "btnExportToExcel_Click", ex.Message.ToString, "")
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Function GetDataSet() As DataTable
        Try
            Dim qry As String = ""

            qry = "SELECT IMEI,Latitude,Longitude,Status,StaffId,DeviceType,DeviceAssetTag,DeviceId,Remarks,Country,WebServiceURL,StaffName,"
            qry = qry + "MobileNo,MobileLastLoginDate,MobileAppVersion,Email FROM tblMobileDevice order by DeviceId"
            ' txt.Text = qry


            'Dim query As String = txt.Text
            'query = qry + query.Substring(query.IndexOf("where"))


            Dim dt As New DataTable()
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim cmd As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim sda As New MySqlDataAdapter()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = qry


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
        Catch ex As Exception
            InsertIntoTblWebEventLog(Session("UserID"), "GetDataSet", ex.Message.ToString, "")
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Function

    'Protected Sub btnAddNewEndPoint_Click(sender As Object, e As EventArgs) Handles btnAddNewEndPoint.Click
    '    mdlPopupAddEndPoint.Show()

    'End Sub

    Protected Sub btnSaveEndPoint_Click(sender As Object, e As EventArgs) Handles btnSaveEndPoint.Click

        Try
            Dim totalRows As Long
            totalRows = 0

            For rowIndex1 As Integer = 0 To GridView2.Rows.Count - 1
                Dim TextBoxchkSelect1 As CheckBox = CType(GridView2.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)
                If (TextBoxchkSelect1.Checked = True) Then
                    totalRows = totalRows + 1
                    '  GoTo insertRec2
                End If
            Next rowIndex1
            'End If


            If totalRows = 0 Then
                mdlPopupEndPoint.Show()
                Dim message0 As String = "alert('PLEASE SELECT A RECORD')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message0, True)

                Exit Sub
            End If


            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            'Dim ctrChecked As Integer
            'ctrChecked = 0

            Dim commandDel As MySqlCommand = New MySqlCommand

            commandDel.CommandType = CommandType.Text
            Dim qryDel As String = "Delete from tblmobiledeviceendpoint where mobiledevicercno ='" & Convert.ToInt32(txtEndPointRcno.Text) & "'"
            commandDel.CommandText = qryDel
            commandDel.Parameters.Clear()

            commandDel.Connection = conn

            commandDel.ExecuteNonQuery()

            Dim rowselected As Integer
            rowselected = 0

            '  InsertIntoTblWebEventLog("MobileDevice", "btnSaveEndPoint_Click", "1", txtEndPointRcno.Text)

            'Dim message As String = "alert('1')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

            'rowselected = grvBillingDetails.Rows.Count - 1 '26.10.17

            For rowIndex As Integer = 0 To GridView2.Rows.Count - 1
                Dim TextBoxchkSelect As CheckBox = CType(GridView2.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                Dim lblidCountry As Label = CType(GridView2.Rows(rowIndex).Cells(1).FindControl("txtCountryGV"), Label)
                Dim lblidWebServiceURL As Label = CType(GridView2.Rows(rowIndex).Cells(2).FindControl("txtWebServiceURLGV"), Label)
                Dim lblidCountryRcNo As Label = CType(GridView2.Rows(rowIndex).Cells(3).FindControl("txtCountryRcnoGV"), Label)

                If TextBoxchkSelect.Checked = True Then
                    '  ctrChecked = ctrChecked + 1
                    'If txtCPModeUserAccess.Text = "Add" Then
                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    '  InsertIntoTblWebEventLog("MobileDevice", "btnSaveEndPoint_Click", "2", lblidCountryRcNo.Text)

                    command.CommandText = "INSERT INTO tblmobiledeviceendpoint(MobileDeviceRcno,EndPointID,CreatedBy,CreatedOn)VALUES(@MobileDeviceRcno,@EndPointID,@CreatedBy,@CreatedOn);"
                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@MobileDeviceRcno", Convert.ToInt32(txtEndPointRcno.Text))
                    command.Parameters.AddWithValue("@EndPointID", Convert.ToInt32(lblidCountryRcNo.Text))
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    '     txtCPRcno.Text = command.LastInsertedId

                    '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessageEndPoint.Text = "END POINTS UPDATED"
                    lblAlert.Text = ""

                    'EnableCPControls()

                End If
            Next
            conn.Close()
            conn.Dispose()
            '  InsertIntoTblWebEventLog("MobileDevice", "btnSaveEndPoint_Click", "2", txtEndPointRcno.Text)

            Dim qry As String = "select True as Selected,B.Country,B.WebServiceUrl,B.Rcno from tblmobiledeviceendpoint A INNER JOIN tblmobiledevicecountry B on A.endpointid=B.rcno where mobiledevicercno=" & Convert.ToInt32(txtEndPointRcno.Text) & " union "
            qry = qry + "select false as Selected,Country,WebServiceUrl,rcno from tblmobiledevicecountry where Active='Y' AND rcno not in "
            qry = qry + "(select B.rcno from tblmobiledeviceendpoint A INNER JOIN tblmobiledevicecountry B on A.endpointid=B.rcno where mobiledevicercno=" & Convert.ToInt32(txtEndPointRcno.Text) & ")"

            SqlDataSource2.SelectCommand = qry
            GridView2.DataSourceID = "SqlDataSource2"
            GridView2.DataBind()

            GridView1.DataBind()

            ' mdlPopupEndPoint.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("MOBILE DEVICE - " + Session("UserID"), "btnSaveEndPoint_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try

    End Sub

    'Protected Sub ddlCountryAddEndPoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCountryAddEndPoint.SelectedIndexChanged
    '    '  txtWebServiceUrlAddEndPoint.Text = RetrieveURL(ddlCountryAddEndPoint.Text.ToUpper)
    '    Dim url As String = ""
    '    Dim conn As MySqlConnection = New MySqlConnection()

    '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    conn.Open()
    '    Dim command1 As MySqlCommand = New MySqlCommand

    '    command1.CommandType = CommandType.Text

    '    command1.CommandText = "SELECT * FROM tblmobiledevicecountry where country='" & ddlCountryAddEndPoint.Text.ToUpper & "'"
    '    command1.Connection = conn

    '    Dim dr As MySqlDataReader = command1.ExecuteReader()

    '    ' dr.Read()
    '    Dim dt As New DataTable
    '    dt.Load(dr)

    '    If dt.Rows.Count > 0 Then
    '        txtWebServiceUrlAddEndPoint.Text = dt.Rows(0)("WebServiceURL").ToString
    '        txtAddEndPointRcNo.Text = dt.Rows(0)("RcNo").ToString
    '    End If
    '    '    mdlPopupAddEndPoint.Show()

    'End Sub

    'Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView2.PageIndexChanging
    '    Try
    '        GridView2.PageIndex = e.NewPageIndex

    '        Dim qry As String = "select True as Selected,B.Country,B.WebServiceUrl,B.Rcno from tblmobiledeviceendpoint A INNER JOIN tblmobiledevicecountry B on A.endpointid=B.rcno where mobiledevicercno=" & Convert.ToInt32(txtEndPointRcno.Text) & " union "
    '        qry = qry + "select false as Selected,Country,WebServiceUrl,rcno from tblmobiledevicecountry where Active='Y' AND rcno not in "
    '        qry = qry + "(select B.rcno from tblmobiledeviceendpoint A INNER JOIN tblmobiledevicecountry B on A.endpointid=B.rcno where mobiledevicercno=" & Convert.ToInt32(txtEndPointRcno.Text) & ")"

    '        SqlDataSource2.SelectCommand = qry
    '        GridView2.DataSourceID = "SqlDataSource2"
    '        GridView2.DataBind()

    '        mdlPopupEndPoint.Show()

    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("MOBILEDEVICE - " + txtCreatedBy.Text, "GridView1_PageIndexChanging", ex.Message.ToString, "")
    '    End Try
    'End Sub

    Protected Sub OnRowDataBoundg1(sender As Object, e As GridViewRowEventArgs)
     
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."


        End If
    End Sub

    Protected Sub OnSelectedIndexChangedg1(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            For Each row As GridViewRow In GridView1.Rows
              
                If row.RowIndex = GridView1.SelectedIndex Then
                    row.BackColor = ColorTranslator.FromHtml("#AEE4FF")
                    row.ToolTip = String.Empty
                Else
                    If row.RowIndex Mod 2 = 0 Then
                        row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                        row.ToolTip = "Click to select this row."
                    Else
                        row.BackColor = ColorTranslator.FromHtml("#ffffff")
                        row.ToolTip = "Click to select this row."
                    End If
                End If
            Next
        Catch ex As Exception
            InsertIntoTblWebEventLog("MobileDevice", "OnSelectedIndexChangedg1", ex.Message.ToString, txtRcno.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim qry As String
            qry = "select * from tblmobiledevice where 1=1 "
            If String.IsNullOrEmpty(txtSearchDeviceID.Text) = False Then
                txtDeviceId.Text = txtSearchDeviceID.Text

                qry = qry + " and deviceid like '%" + txtSearchDeviceID.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchIdentityNo.Text) = False Then
                txtIMEI.Text = txtSearchIdentityNo.Text
                qry = qry + " and IMEI like '%" + txtSearchIdentityNo.Text + "%'"
            End If
            If ddlSearchStatus.SelectedValue.ToString <> "" Then
                ' txtStatus.Text = ddlSearchStatus.Text
                qry = qry + " and status = '" + ddlSearchStatus.SelectedValue.ToString + "'"
            End If
            If String.IsNullOrEmpty(txtSearchStaffID.Text) = False Then
                txtStaff.Text = txtSearchStaffID.Text
                qry = qry + " and StaffID like '%" + txtSearchStaffID.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchEmailAddress.Text) = False Then
                txtEmail.Text = txtSearchEmailAddress.Text
                qry = qry + " and Email like '%" + txtSearchEmailAddress.Text + "%'"
            End If
            If String.IsNullOrEmpty(txtSearchRemarks.Text) = False Then
                '  txtAssetRegNo.Text = txtSearchCurrentUser.Text
                qry = qry + " and Remarks like '%" + txtSearchRemarks.Text + "%'"
            End If

            qry = qry + " order by createdon desc;"

            txt.Text = qry
            'lblMessage.Text = qry
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            txtSearchDeviceID.Text = ""
            'txtSearchAssetRegNo.Text = ""
            ddlSearchStatus.SelectedIndex = 0
            txtSearchIdentityNo.Text = ""
            'UpdatePanel1.Update()
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try


    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        mdlPopupSearch.Show()

    End Sub
End Class