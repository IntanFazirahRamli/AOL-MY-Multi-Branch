Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Supplier
    Inherits System.Web.UI.Page

    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtRcno.Text = ""
        txtSupplierID.Text = ""
        txtSupplierName.Text = ""
        ddlStatus.SelectedIndex = 0
        txtRegNo.Text = ""
        txtGSTRegNo.Text = ""
        txtWebsite.Text = ""
        txtStartDate.Text = ""
        txtComments.Text = ""
        txtAddress1.Text = ""
        txtBuilding.Text = ""
        txtStreet.Text = ""
        ddlCity.SelectedIndex = 0
        ddlState.SelectedIndex = 0
        ddlCountry.SelectedIndex = 0
        txtPostal.Text = ""
        txtTelephone.Text = ""
        txtTel2.Text = ""
        txtFax.Text = ""
        txtMobile.Text = ""
        txtEmail.Text = ""
        txtContactPerson.Text = ""

    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray
        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        'btnPrint.Enabled = True
        'btnPrint.ForeColor = System.Drawing.Color.Black
        txtSupplierID.Enabled = False
        txtSupplierName.Enabled = False
        ddlStatus.Enabled = False
        txtRegNo.Enabled = False
        txtGSTRegNo.Enabled = False
        txtWebsite.Enabled = False
        txtStartDate.Enabled = False
        txtComments.Enabled = False
        txtAddress1.Enabled = False
        txtBuilding.Enabled = False
        txtStreet.Enabled = False
        ddlCity.Enabled = False
        ddlState.Enabled = False
        ddlCountry.Enabled = False
        txtPostal.Enabled = False
        txtTelephone.Enabled = False
        txtTel2.Enabled = False
        txtFax.Enabled = False
        txtMobile.Enabled = False
        txtEmail.Enabled = False
        txtContactPerson.Enabled = False

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

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        'btnPrint.Enabled = False
        'btnPrint.ForeColor = System.Drawing.Color.Gray

        txtSupplierID.Enabled = True
        txtSupplierName.Enabled = True
        ddlStatus.Enabled = True
        txtRegNo.Enabled = True
        txtGSTRegNo.Enabled = True
        txtWebsite.Enabled = True
        txtStartDate.Enabled = True
        txtComments.Enabled = True
        txtAddress1.Enabled = True
        txtBuilding.Enabled = True
        txtStreet.Enabled = True
        ddlCity.Enabled = True
        ddlState.Enabled = True
        ddlCountry.Enabled = True
        txtPostal.Enabled = True
        txtTelephone.Enabled = True
        txtTel2.Enabled = True
        txtFax.Enabled = True
        txtMobile.Enabled = True
        txtEmail.Enabled = True
        txtContactPerson.Enabled = True

        AccessControl()
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If

        MakeMeNull()

        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblsupplier where rcno=" & Convert.ToInt32(txtRcno.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        ' dr.Read()
        Dim dt As New DataTable
        dt.Load(dr)
        If dt.Rows.Count > 0 Then
            txtSupplierID.Text = dt.Rows(0)("SupplierID").ToString
            txtSupplierName.Text = dt.Rows(0)("Name").ToString
            If String.IsNullOrEmpty(dt.Rows(0)("Status").ToString) = False Then
                ddlStatus.Text = dt.Rows(0)("Status").ToString
            End If

            txtRegNo.Text = dt.Rows(0)("RegNo").ToString
            txtGSTRegNo.Text = dt.Rows(0)("GstRegNo").ToString
            txtWebsite.Text = dt.Rows(0)("Website").ToString
            If dt.Rows(0)("StartDate").ToString = DBNull.Value.ToString Then
            Else
                txtStartDate.Text = Convert.ToDateTime(dt.Rows(0)("StartDate")).ToString("dd/MM/yyyy")
            End If
            txtAddress1.Text = dt.Rows(0)("Address1").ToString
            txtStreet.Text = dt.Rows(0)("AddStreet").ToString


            txtBuilding.Text = dt.Rows(0)("AddBuilding").ToString
            If String.IsNullOrEmpty(dt.Rows(0)("AddCity").ToString) = False Then
                ddlCity.Text = dt.Rows(0)("AddCity").ToString
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("AddCountry").ToString) = False Then
                If Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'pore" Or Server.HtmlDecode(dt.Rows(0)("AddCountry")).ToString = "S'PORE" Then
                    ddlCountry.Text = "SINGAPORE"
                Else
                    ddlCountry.Text = dt.Rows(0)("AddCountry").ToString.Trim

                End If
            End If
            If String.IsNullOrEmpty(dt.Rows(0)("AddState").ToString) = False Then
                ddlState.Text = dt.Rows(0)("AddState").ToString
            End If

            txtPostal.Text = dt.Rows(0)("AddPostal").ToString

            txtContactPerson.Text = dt.Rows(0)("ContactPerson").ToString
            txtMobile.Text = dt.Rows(0)("Mobile").ToString

            txtTelephone.Text = dt.Rows(0)("Telephone").ToString
            txtFax.Text = dt.Rows(0)("Fax").ToString
            txtTel2.Text = dt.Rows(0)("Telephone2").ToString
            txtEmail.Text = dt.Rows(0)("Email").ToString
            txtComments.Text = dt.Rows(0)("Comments").ToString
        End If

        dt.Clear()
        dt.Dispose()
        dr.Close()
        command1.Dispose()
        conn.Close()

     
        txtMode.Text = "View"
        'txtMode.Text = "Edit"
        ' DisableControls()
        txtSupplierID.Enabled = False


        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If

        'EnableControls()
        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            AccessControl()
        End If

    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()
        txtSupplierID.Enabled = False

        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtSupplierName.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            txtDDLText.Text = "-1"
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID
        End If
    End Sub

    Private Sub AccessControl()
        '   Return
        Try
            '''''''''''''''''''Access Control 
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT X0109,  X0109Add, X0109Edit, X0109Delete, X0109Print FROM tblGroupAccess where BrandAccess = '" & Session("SecGroupAuthority") & "'"
                '  command.CommandText = "SELECT X0109,  X0109Add, X0109Edit, X0109Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT X1302AssetSupplierView,  X1302AssetSupplierAdd, X1302AssetSupplierEdit, X1302AssetSupplierDelete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x1302AssetSupplierView")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x1302AssetSupplierView"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x1302AssetSupplierView")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x1302AssetSupplierAdd")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x1302AssetSupplierAdd")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x1302AssetSupplierAdd").ToString()
                        End If
                    End If

                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x1302AssetSupplierEdit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x1302AssetSupplierEdit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x1302AssetSupplierEdit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x1302AssetSupplierDelete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x1302AssetSupplierDelete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x1302AssetSupplierDelete").ToString()
                            End If
                        End If
                    Else
                        Me.btnEdit.Enabled = False
                        Me.btnDelete.Enabled = False
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

                    If btnDelete.Enabled = True Then
                        btnDelete.ForeColor = System.Drawing.Color.Black
                    Else
                        btnDelete.ForeColor = System.Drawing.Color.Gray
                    End If


                    'If btnPrint.Enabled = True Then
                    '    btnPrint.ForeColor = System.Drawing.Color.Black
                    'Else
                    '    btnPrint.ForeColor = System.Drawing.Color.Gray
                    'End If
                End If
                conn.Close()
                conn.Dispose()
                command.Dispose()
                dt.Dispose()
                dr.Close()
            End If


            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'If txtSupplierID.Text = "" Then
        '    '  MessageBox.Message.Alert(Page, "Company Brand cannot be blank!!!", "str")
        '    lblAlert.Text = "SUPPLIER ID CANNOT BE BLANK"
        '    Return
        'End If

        If txtSupplierName.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Company Brand cannot be blank!!!", "str")
            lblAlert.Text = "SUPPLIER NAME CANNOT BE BLANK"
            Return
        End If

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblsupplier where suppliername=@suppliername"
                'command1.Parameters.AddWithValue("@supplierid", txtSupplierName.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                '    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                '    lblAlert.Text = "SUPPLIER ID ALREADY EXISTS"
                '    txtSupplierName.Focus()
                '    Exit Sub
                'End If

                'dt.Clear()
                'dt.Dispose()
                'dr.Close()
                'command1.Dispose()

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblsupplier where name=@suppliername"
                command1.Parameters.AddWithValue("@suppliername", txtSupplierName.Text)
                command1.Connection = conn
                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)


                If dt.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "SUPPLIER NAME ALREADY EXISTS"
                    txtSupplierName.Focus()
                    Exit Sub
                End If

                dt.Clear()
                dt.Dispose()
                dr.Close()
                command1.Dispose()

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblsupplier(SupplierID,Name,Status,RegNo,GstRegNo,Website,StartDate,Address1,AddBuilding,AddStreet,AddPostal,AddState,AddCity,AddCountry,Telephone,Fax,Email,Telephone2,Mobile,ContactPerson,Comments,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                qry = qry + "VALUES(@SupplierID,@Name,@Status,@RegNo,@GstRegNo,@Website,@StartDate,@Address1,@AddBuilding,@AddStreet,@AddPostal,@AddState,@AddCity,@AddCountry,@Telephone,@Fax,@Email,@Telephone2,@Mobile,@ContactPerson,@Comments,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                   command.Parameters.AddWithValue("@Name", txtSupplierName.Text.ToUpper)
                    command.Parameters.AddWithValue("@Status", ddlStatus.Text)
                    command.Parameters.AddWithValue("@RegNo", txtRegNo.Text.ToUpper)
                    command.Parameters.AddWithValue("@GstRegNo", txtGSTRegNo.Text.ToUpper)
                    command.Parameters.AddWithValue("@Website", txtWebsite.Text.ToUpper)
                    If txtStartDate.Text = "" Then
                        command.Parameters.AddWithValue("@StartDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    command.Parameters.AddWithValue("@Address1", txtAddress1.Text.ToUpper)
                    command.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text.ToUpper)
                    command.Parameters.AddWithValue("@AddStreet", txtStreet.Text.ToUpper)
                    command.Parameters.AddWithValue("@AddPostal", txtPostal.Text.ToUpper)

                    If ddlState.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddState", "")
                    Else
                        command.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                    End If

                    If ddlCity.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddCity", "")
                    Else
                        command.Parameters.AddWithValue("@AddCity", ddlCity.Text.ToUpper)
                    End If
                    If ddlCountry.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddCountry", "")
                    Else
                        command.Parameters.AddWithValue("@AddCountry", ddlCountry.Text.ToUpper)
                    End If
                    command.Parameters.AddWithValue("@Telephone", txtTelephone.Text.ToUpper)
                    command.Parameters.AddWithValue("@Fax", txtFax.Text.ToUpper)
                    command.Parameters.AddWithValue("@Telephone2", txtTel2.Text.ToUpper)
                    command.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToUpper)
                    command.Parameters.AddWithValue("@Email", txtEmail.Text.ToUpper)
                    command.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.ToUpper)
                    command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)

                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
          
                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    '  command.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text.ToUpper)
                    command.Parameters.AddWithValue("@Name", txtSupplierName.Text)
                    command.Parameters.AddWithValue("@Status", ddlStatus.Text)
                    command.Parameters.AddWithValue("@RegNo", txtRegNo.Text)
                    command.Parameters.AddWithValue("@GstRegNo", txtGSTRegNo.Text)
                    command.Parameters.AddWithValue("@Website", txtWebsite.Text)
                    If txtStartDate.Text = "" Then
                        command.Parameters.AddWithValue("@StartDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    command.Parameters.AddWithValue("@Address1", txtAddress1.Text)
                    command.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text)
                    command.Parameters.AddWithValue("@AddStreet", txtStreet.Text)
                    command.Parameters.AddWithValue("@AddPostal", txtPostal.Text)

                    If ddlState.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddState", "")
                    Else
                        command.Parameters.AddWithValue("@AddState", ddlState.Text)
                    End If

                    If ddlCity.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddCity", "")
                    Else
                        command.Parameters.AddWithValue("@AddCity", ddlCity.Text)
                    End If
                    If ddlCountry.Text = txtDDLText.Text Then
                        command.Parameters.AddWithValue("@AddCountry", "")
                    Else
                        command.Parameters.AddWithValue("@AddCountry", ddlCountry.Text)
                    End If
                    command.Parameters.AddWithValue("@Telephone", txtTelephone.Text)
                    command.Parameters.AddWithValue("@Fax", txtFax.Text)
                    command.Parameters.AddWithValue("@Telephone2", txtTel2.Text)
                    command.Parameters.AddWithValue("@Mobile", txtMobile.Text)
                    command.Parameters.AddWithValue("@Email", txtEmail.Text)
                    command.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text)
                    command.Parameters.AddWithValue("@Comments", txtComments.Text)

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                End If


                GenerateSupplierID()
                command.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text.ToUpper)
                If txtSupplierID.Text = "" Then
                    '  MessageBox.Message.Alert(Page, "Company Brand cannot be blank!!!", "str")
                    lblAlert.Text = "SUPPLIER ID CANNOT BE BLANK"
                    Return
                End If
                command.Connection = conn

                command.ExecuteNonQuery()
                txtRcno.Text = command.LastInsertedId
                'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""

                conn.Close()

            Catch ex As Exception
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
                lblAlert.Text = ex.Message
            End Try
            EnableControls()
            txtMode.Text = ""
        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            'If txtExists.Text = "True" Then
            '    '  MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
            '    lblAlert.Text = "RECORD IS IN USE, SO CANNOT BE MODIFIED"
            '    Return
            'End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtIndustry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblsupplier where supplierID=@SupplierID and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Company Brand already exists!!!", "str")
                    lblAlert.Text = "SUPPLIER ID ALREADY EXISTS"
                    txtSupplierID.Focus()
                    Exit Sub
             
                End If


                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblsupplier where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                        qry = "update tblsupplier set Name = @Name, Status = @Status,RegNo=@RegNo, GstRegNo=@GstRegNo,StartDate = @StartDate,Website = @Website, Address1 = @Address1, AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddPostal = @AddPostal,AddState = @AddState,AddCity = @AddCity,AddCountry = @AddCountry,Telephone = @Telephone, Telephone2 = @Telephone2, Mobile = @Mobile,Fax = @Fax, Email=@Email, ContactPerson = @ContactPerson,Comments = @Comments, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                        qry = "update tblsupplier set Name = @Name, Status = @Status,RegNo=@RegNo, GstRegNo=@GstRegNo,StartDate = @StartDate,Website = @Website, Address1 = @Address1, AddBuilding = @AddBuilding,AddStreet = @AddStreet,AddPostal = @AddPostal,AddState = @AddState,AddCity = @AddCity,AddCountry = @AddCountry,Telephone = @Telephone, Telephone2 = @Telephone2, Mobile = @Mobile,Fax = @Fax, Email=@Email, ContactPerson = @ContactPerson,Comments = @Comments, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()
                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                        '   command.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text.ToUpper)
                        command.Parameters.AddWithValue("@Name", txtSupplierName.Text.ToUpper)
                        command.Parameters.AddWithValue("@Status", ddlStatus.Text)
                        command.Parameters.AddWithValue("@RegNo", txtRegNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@GstRegNo", txtGSTRegNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@Website", txtWebsite.Text.ToUpper)
                        If txtStartDate.Text = "" Then
                            command.Parameters.AddWithValue("@StartDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
                        End If
                        command.Parameters.AddWithValue("@Address1", txtAddress1.Text.ToUpper)
                        command.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text.ToUpper)
                        command.Parameters.AddWithValue("@AddStreet", txtStreet.Text.ToUpper)
                        command.Parameters.AddWithValue("@AddPostal", txtPostal.Text.ToUpper)

                        If ddlState.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddState", "")
                        Else
                            command.Parameters.AddWithValue("@AddState", ddlState.Text.ToUpper)
                        End If

                        If ddlCity.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddCity", "")
                        Else
                            command.Parameters.AddWithValue("@AddCity", ddlCity.Text.ToUpper)
                        End If
                        If ddlCountry.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddCountry", "")
                        Else
                            command.Parameters.AddWithValue("@AddCountry", ddlCountry.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@Telephone", txtTelephone.Text.ToUpper)
                        command.Parameters.AddWithValue("@Fax", txtFax.Text.ToUpper)
                        command.Parameters.AddWithValue("@Telephone2", txtTel2.Text.ToUpper)
                        command.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@Email", txtEmail.Text.ToUpper)
                        command.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.ToUpper)
                        command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)

                        '    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        '    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        'command.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text.ToUpper)
                        command.Parameters.AddWithValue("@Name", txtSupplierName.Text)
                        command.Parameters.AddWithValue("@Status", ddlStatus.Text)
                        command.Parameters.AddWithValue("@RegNo", txtRegNo.Text)
                        command.Parameters.AddWithValue("@GstRegNo", txtGSTRegNo.Text)
                        command.Parameters.AddWithValue("@Website", txtWebsite.Text)
                        If txtStartDate.Text = "" Then
                            command.Parameters.AddWithValue("@StartDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
                        End If
                        command.Parameters.AddWithValue("@Address1", txtAddress1.Text)
                        command.Parameters.AddWithValue("@AddBuilding", txtBuilding.Text)
                        command.Parameters.AddWithValue("@AddStreet", txtStreet.Text)
                        command.Parameters.AddWithValue("@AddPostal", txtPostal.Text)

                        If ddlState.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddState", "")
                        Else
                            command.Parameters.AddWithValue("@AddState", ddlState.Text)
                        End If

                        If ddlCity.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddCity", "")
                        Else
                            command.Parameters.AddWithValue("@AddCity", ddlCity.Text)
                        End If
                        If ddlCountry.Text = txtDDLText.Text Then
                            command.Parameters.AddWithValue("@AddCountry", "")
                        Else
                            command.Parameters.AddWithValue("@AddCountry", ddlCountry.Text)
                        End If
                        command.Parameters.AddWithValue("@Telephone", txtTelephone.Text)
                        command.Parameters.AddWithValue("@Fax", txtFax.Text)
                        command.Parameters.AddWithValue("@Telephone2", txtTel2.Text)
                        command.Parameters.AddWithValue("@Mobile", txtMobile.Text)
                        command.Parameters.AddWithValue("@Email", txtEmail.Text)
                        command.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text)
                        command.Parameters.AddWithValue("@Comments", txtComments.Text)

                        '  command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text")
                        '      command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    End If


                        command.Connection = conn

                        command.ExecuteNonQuery()

                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"

                        'If txtExists.Text = "True" Then
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED."
                        '    lblAlert.Text = ""
                        'Else
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        '    lblAlert.Text = ""
                        'End If
                    End If
                
                    conn.Close()

                    txtMode.Text = ""

                    If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SUPPLIER", txtSupplierID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                    Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SUPPLIER", txtSupplierID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                    End If
            Catch ex As Exception
                lblAlert.Text = ex.Message
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        '  MakeMeNull()
        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtRcno.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"
        txtSupplierID.Enabled = False

        If txtExists.Text = "True" Then
            txtSupplierName.Enabled = False
        Else
            txtSupplierName.Enabled = True
        End If

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        MakeMeNull()
        EnableControls()

    End Sub


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'Me.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Close", "window.close()", True)
        Response.Redirect("Home.aspx")

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return

        End If
        lblMessage.Text = "ACTION: DELETE RECORD"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then

            If txtExists.Text = "True" Then
                ' MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
                lblAlert.Text = "RECORD IS IN USE, CANNOT BE DELETED"
                Return
            End If


            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblSUPPLIER where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblsupplier where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SUPPLIER", txtSupplierID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()


            GridView1.DataSourceID = "SqlDataSource1"
            MakeMeNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If

    End Sub

    'Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    '    Response.Redirect("RV_MasterCompanyBrand.aspx")
    'End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblasset where SupplierCode=@data1 and SupplierName=@data2"
        command1.Parameters.AddWithValue("@data1", txtSupplierID.Text)
        command1.Parameters.AddWithValue("@data2", txtSupplierName.Text)
        command1.Connection = conn

        Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then
            Return True
        End If

        conn.Close()

        Return False
    End Function

    Protected Sub GenerateSupplierID()

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand
            Dim command As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT Period01,WIDTH,Prefix FROM tbldoccontrol where Prefix = 'SU';"
            '  command1.Parameters.AddWithValue("@code", txtAcctCode.Text)
            command1.Connection = conn
            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)
            If dt.Rows.Count > 0 Then
                Dim length As String = "D" + dt.Rows(0)("Width").ToString

                Dim lastnum As Int64 = Convert.ToInt64(dt.Rows(0)("Period01"))
                lastnum = lastnum + 1

                command.CommandType = CommandType.Text

                command.CommandText = "UPDATE tbldoccontrol set Period01=@lastnum where Prefix = 'SU';"
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@lastnum", lastnum)

                command.Connection = conn

                command.ExecuteNonQuery()
                txtSupplierID.Text = dt.Rows(0)("Prefix") + (lastnum).ToString(length)

            Else
                command.CommandType = CommandType.Text

                command.CommandText = "insert into tbldoccontrol(Prefix,Period01,width) values ('SU',0,4);"
                command.Connection = conn

                command.ExecuteNonQuery()

                txtSupplierID.Text = "SU0001"

            End If

            conn.Close()
        Catch ex As Exception

            MessageBox.Message.Alert(Page, ex.Message.ToString, "")
            ' InsertIntoTblWebEventLog("GenerateSupplierID", ex.Message.ToString, txtSupplierID.Text)
        End Try
    End Sub

End Class



