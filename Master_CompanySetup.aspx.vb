
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_CompanySetup
    Inherits System.Web.UI.Page

    Public rcno As String


    Private Sub AccessControl()
        Try
            '''''''''''''''''''Access Control 
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT X0163,  X0163Add, X0163Edit, X0163Delete, X0163Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0101,   x0101Edit FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0101")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0101"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0101")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    'If Not IsDBNull(dt.Rows(0)("X0163Add")) Then
                    '    If String.IsNullOrEmpty(dt.Rows(0)("X0163Add")) = False Then
                    '        Me.btnADD.Enabled = dt.Rows(0)("X0163Add").ToString()
                    '    End If
                    'End If

                    'If txtMode.Text = "View" Then
                    If Not IsDBNull(dt.Rows(0)("x0101Edit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0101Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0101Edit").ToString()
                        End If
                    End If

                    'If Not IsDBNull(dt.Rows(0)("X0176Delete")) Then
                    '    If String.IsNullOrEmpty(dt.Rows(0)("X0176Delete")) = False Then
                    '        Me.btnDelete.Enabled = dt.Rows(0)("X0176Delete").ToString()
                    '    End If
                    'End If
                Else
                    Me.btnEdit.Enabled = False
                    Me.btnDelete.Enabled = False
                End If

                'If btnADD.Enabled = True Then
                '    btnADD.ForeColor = System.Drawing.Color.Black
                'Else
                '    btnADD.ForeColor = System.Drawing.Color.Gray
                'End If


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
            'End If
            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub
    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

        txtCompanyName.Enabled = False
        txtOfficeAddress.Enabled = False
        txtBusinessRegNumber.Enabled = False
        txtGSTNumber.Enabled = False
        txtTelNumber.Enabled = False
        txtFaxNumber.Enabled = False
        txtWebsite.Enabled = False
        txtEmail.Enabled = False
        txtMobile.Enabled = False
        txtInvoiceEmail.Enabled = False

        txtBankName.Enabled = False
        txtBankCode.Enabled = False
        txtBranchCode.Enabled = False
        txtAccountName.Enabled = False
        txtAccountCode.Enabled = False
        txtSWIFTCode.Enabled = False
        FileUpload1.Enabled = False

        AccessControl()
    End Sub

    Private Sub DisableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        txtCompanyName.Enabled = True
        txtOfficeAddress.Enabled = True
        txtBusinessRegNumber.Enabled = True
        txtGSTNumber.Enabled = True
        txtTelNumber.Enabled = True
        txtFaxNumber.Enabled = True
        txtWebsite.Enabled = True
        txtEmail.Enabled = True
        txtMobile.Enabled = True
        txtInvoiceEmail.Enabled = True

        txtBankName.Enabled = True
        txtBankCode.Enabled = True
        txtBranchCode.Enabled = True
        txtAccountName.Enabled = True
        txtAccountCode.Enabled = True
        txtSWIFTCode.Enabled = True
        FileUpload1.Enabled = True

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            EnableControls()

            RetrieveData()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")

    End Sub

    Private Sub RetrieveData()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblcompanyinfo where rcno=1"
        '  command1.Parameters.AddWithValue("@city", txtCity.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then

            txtCompanyName.Text = dt.Rows(0)("CompanyName").ToString

            txtOfficeAddress.Text = dt.Rows(0)("OfficeAddress1").ToString
            txtBusinessRegNumber.Text = dt.Rows(0)("BusinessRegistrationNumber").ToString
            txtGSTNumber.Text = dt.Rows(0)("GSTNumber").ToString
            txtTelNumber.Text = dt.Rows(0)("TelephoneNumber").ToString
            txtFaxNumber.Text = dt.Rows(0)("FaxNumber").ToString
            txtWebsite.Text = dt.Rows(0)("Website").ToString
            txtEmail.Text = dt.Rows(0)("Email").ToString
            txtMobile.Text = dt.Rows(0)("Mobile").ToString
            txtInvoiceEmail.Text = dt.Rows(0)("InvoiceEmail").ToString

            txtBankName.Text = dt.Rows(0)("BankName").ToString
            txtBankCode.Text = dt.Rows(0)("BankCode").ToString
            txtBranchCode.Text = dt.Rows(0)("BranchCode").ToString
            txtAccountName.Text = dt.Rows(0)("AccountName").ToString
            txtAccountCode.Text = dt.Rows(0)("AccountCode").ToString
            txtSWIFTCode.Text = dt.Rows(0)("SWIFTCode").ToString

            If IsDBNull(dt.Rows(0)("Logo")) = False Then
                Dim bytes As Byte() = DirectCast(dt.Rows(0)("Logo"), Byte())
                Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)
            End If

        End If

    End Sub


    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        EnableControls()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        '   Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

   

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblcompanyinfo where rcno=1"
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry As String
            qry = "UPDATE tblcompanyinfo SET CompanyName = @CompanyName,OfficeAddress1 = @OfficeAddress1,OfficeAddress2 = @OfficeAddress2,BusinessRegistrationNumber = @BusinessRegistrationNumber,GSTNumber = @GSTNumber,TelephoneNumber = @TelephoneNumber,FaxNumber = @FaxNumber,Website = @Website,Email = @Email,Mobile = @Mobile,InvoiceEmail = @InvoiceEmail, BankName=@BankName, BankCode=@BankCode, BranchCode=@BranchCode, AccountName=@AccountName, AccountCode=@AccountCode, SWIFTCode=@SWIFTCode,Logo=@Logo, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn WHERE RcNo = 1;"

            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text)
            command.Parameters.AddWithValue("@OfficeAddress1", txtOfficeAddress.Text)
            command.Parameters.AddWithValue("@OfficeAddress2", "")
            command.Parameters.AddWithValue("@BusinessRegistrationNumber", txtBusinessRegNumber.Text)
            command.Parameters.AddWithValue("@GSTNumber", txtGSTNumber.Text)
            command.Parameters.AddWithValue("@TelephoneNumber", txtTelNumber.Text)
            command.Parameters.AddWithValue("@FaxNumber", txtFaxNumber.Text)
            command.Parameters.AddWithValue("@Website", txtWebsite.Text)
            command.Parameters.AddWithValue("@Email", txtEmail.Text)
            command.Parameters.AddWithValue("@Mobile", txtMobile.Text)
            command.Parameters.AddWithValue("@InvoiceEmail", txtInvoiceEmail.Text)

            command.Parameters.AddWithValue("@BankName", txtBankName.Text)
            command.Parameters.AddWithValue("@BankCode", txtBankCode.Text)
            command.Parameters.AddWithValue("@BranchCode", txtBranchCode.Text)
            command.Parameters.AddWithValue("@AccountName", txtAccountName.Text)
            command.Parameters.AddWithValue("@AccountCode", txtAccountCode.Text)
            command.Parameters.AddWithValue("@SWIFTCode", txtSWIFTCode.Text)


            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            'Upload image
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If FileUpload1.HasFile Then

                'getting length of uploaded file
                Dim length As Integer = FileUpload1.PostedFile.ContentLength
                '     MessageBox.Message.Alert(Page, length.ToString, "str")
                'create a byte array to store the binary image data
                Dim imgbyte As Byte() = New Byte(length - 1) {}
                'store the currently selected file in memeory
                Dim img As HttpPostedFile = FileUpload1.PostedFile
                'set the binary data
                img.InputStream.Read(imgbyte, 0, length)
                Dim bytes As Byte() = FileUpload1.FileBytes

                Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes)


                command.Parameters.AddWithValue("@Logo", imgbyte)
            Else
                command.Parameters.AddWithValue("@Logo", "")

            End If

            command.Connection = conn

            command.ExecuteNonQuery()

             lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
            lblAlert.Text = ""
            command.Dispose()

        End If

        conn.Close()
        conn.Dispose()

        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "COMPANYSETUP", "MASTERSETUP", "EDIT", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, "", "", txtRcno.Text)

        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, "Error!!!", "str")
        'End Try
        EnableControls()

    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")

    End Sub
End Class
