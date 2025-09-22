Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Globalization



Partial Class Master_BillingCode
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtProductCode.Text = ""
        txtDescription.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        ddlCOACode.SelectedIndex = 0
        txtCOADescription.Text = ""
        ddlTaxType.SelectedIndex = 0
        txtTaxRate.Text = "0.00"
        txtPrice.Text = "0.00"
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        chkInactive.Checked = False
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
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black
        txtProductCode.Enabled = False
        txtDescription.Enabled = False
        txtPrice.Enabled = False
        ddlCompanyGrp.Enabled = False
        ddlCOACode.Enabled = False
        txtCOADescription.Enabled = False
        ddlTaxType.Enabled = False
        txtTaxRate.Enabled = False

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

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray
        ddlCompanyGrp.Enabled = True

        'If txtMode.Text = "New" Then
        '    txtProductCode.Enabled = True
        'End If

        'txtDescription.Enabled = True
        txtPrice.Enabled = True

        txtDescription.Enabled = True
        ddlCOACode.Enabled = True
        txtCOADescription.Enabled = True
        ddlTaxType.Enabled = True
        txtTaxRate.Enabled = True

        AccessControl()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        lblAlert.Text = ""
        Try
            If txtMode.Text = "Edit" Then
                lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
                Return
            End If
            'EnableControls()
            MakeMeNull()
            Dim editindex As Integer = GridView1.SelectedIndex
            rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
            txtRcno.Text = rcno.ToString()


            '17.09.20


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblbillingproducts where rcno=" & Convert.ToInt32(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Inactive").ToString = "1" Then
                    chkInactive.Checked = True
                Else
                    chkInactive.Checked = False
                End If

                txtProductCode.Text = dt.Rows(0)("ProductCode").ToString
                txtDescription.Text = dt.Rows(0)("Description").ToString
                txtCOADescription.Text = dt.Rows(0)("COADescription").ToString
                txtTaxRate.Text = dt.Rows(0)("TaxRate").ToString
                txtPrice.Text = dt.Rows(0)("Price").ToString

                ddlTaxType.Text = dt.Rows(0)("TaxType").ToString
                ddlCOACode.Text = dt.Rows(0)("COACode").ToString & " : " & dt.Rows(0)("COADescription").ToString
            End If

            '17.09.20


            ''If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            ''    ddlCompanyGrp.SelectedIndex = 0
            ''Else
            ''    ddlCompanyGrp.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
            ''End If

            ''If GridView1.SelectedRow.Cells(1).Text = "1" Then
            ''    chkInactive.Checked = True
            ''Else
            ''    chkInactive.Checked = False
            ''End If

            ''chkInactive.Checked = (GridView1.SelectedRow.Cells(1).Text).ToString

            'If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            '    txtProductCode.Text = ""
            'Else

            '    txtProductCode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text)
            'End If


            'If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Then
            '    txtDescription.Text = ""
            'Else
            '    txtDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
            'End If





            'Query = "SELECT concat (COACode, ' : ', Description) as COACode, Description FROM tblchartofaccounts ORDER BY Description"


            'If GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
            '    txtCOADescription.Text = ""
            'Else
            '    txtCOADescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
            'End If



            'If GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
            '    txtTaxRate.Text = ""
            'Else
            '    txtTaxRate.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(8).Text).ToString
            'End If

            'If GridView1.SelectedRow.Cells(9).Text = "&nbsp;" Then
            '    txtPrice.Text = "0.00"
            'Else
            '    txtPrice.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(9).Text).ToString
            'End If

            Dim strCOA As String
            strCOA = ""
            strCOA = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString & " : " & Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString


            If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
                ddlCOACode.Text = ""
            Else
                ddlCOACode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString & " : " & Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
            End If

            'If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
            '    ddlCOACode.Text = ""
            'Else
            '    ddlCOACode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
            'End If

            If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
                ddlTaxType.Text = ""
            Else
                ddlTaxType.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
            End If

            txtMode.Text = "View"

            'txtMode.Text = "Edit"
            ' DisableControls()
            btnEdit.Enabled = True
            btnEdit.ForeColor = System.Drawing.Color.Black
            'btnDelete.Enabled = True
            'btnDelete.ForeColor = System.Drawing.Color.Black
            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black





            'lblStatusMessage1.Text = "Athjya"
            If CheckIfExists() = True Then
                txtExists.Text = "True"
            Else
                txtExists.Text = "False"
            End If


            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                AccessControl()
            End If



            'lblStatusMessage1.Text = "TEST"

        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()
        txtMode.Text = "New"

        txtProductCode.Enabled = True
        txtProductCode.ForeColor = System.Drawing.Color.Black
        DisableControls()

        'txtMode.Text = "New"
        lblMessage.Text = "ACTION: ADD RECORD"
        txtProductCode.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim Query As String
            Query = "Select CompanyGroup from tblCompanyGroup"
            PopulateDropDownList(Query, "CompanyGroup", "CompanyGroup", ddlCompanyGrp)

            Query = ""
            'Query = "SELECT COACode, Description, Area, Function, Organization, ServiceType, CostCenter, GLAccount, DistCode, Rcno, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn FROM tblchartofaccounts ORDER BY Description"
            Query = "SELECT concat (COACode, ' : ', Description) as COACode, Description FROM tblchartofaccounts ORDER BY Description"

            'PopulateDropDownList(Query, "COACode", "Description", ddlCOACode)
            PopulateDropDownList(Query, "COACode", "COACode", ddlCOACode)

            txtCreatedOn.Attributes.Add("readonly", "readonly")
            txtCOADescription.Attributes.Add("readonly", "readonly")
            txtTaxRate.Attributes.Add("readonly", "readonly")
        End If

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
                'command.CommandText = "SELECT x0251,  x0251Add, x0251Edit, x0251Delete, x0251Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0251,  x0251Add, x0251Edit, x0251Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0251"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0251")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If


                    If String.IsNullOrEmpty(dt.Rows(0)("x0251Add")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0251Add").ToString()
                       
                    End If


                    If txtMode.Text = "View" Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0251Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0251Edit").ToString()
                        End If

                        'If String.IsNullOrEmpty(dt.Rows(0)("x0251Delete")) = False Then
                        '    Me.btnDelete.Enabled = dt.Rows(0)("x0251Delete").ToString()
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
        End Using
        'End Using
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        lblAlert.Text = ""
        'lblMessage.Text = ""

        If txtProductCode.Text = "" Then
            lblAlert.Text = "PRODUCT CODE CANNOT BE BLANK"
            Return
        End If


        If String.IsNullOrEmpty(ddlCOACode.Text) = True Or ddlCOACode.Text = "-1" Then
            lblAlert.Text = "GL CODE CANNOT BE BLANK"
            Return
        End If

        If String.IsNullOrEmpty(ddlTaxType.Text) = True Or ddlTaxType.Text = "-1" Then
            lblAlert.Text = "TAX TYPE CANNOT BE BLANK"
            Return
        End If

        If IsNumeric(txtTaxRate.Text) = False Then
            lblAlert.Text = "TAX RATE SHOULD BE NUMERIC"
            Return
        End If

        If IsNumeric(txtPrice.Text) = False Then
            lblAlert.Text = "PRICE SHOULD BE NUMERIC"
            Return
        End If


        Dim lCOACode As String
        lCOACode = ddlCOACode.SelectedItem.ToString
        Dim cA As String() = ddlCOACode.SelectedItem.ToString.Split(":"c)

        'txtDescription.Text = cA(0).Trim.ToUpper

        'For i = -1 To UBound(cA)
        '    lCOACode = (cA(0))
        'Next i

        'txtDescription.Text = cA(1).Trim.ToUpper

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblbillingproducts where ProductCode=@ProductCode"
                command1.Parameters.AddWithValue("@ProductCode", txtProductCode.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtProductCode.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblbillingproducts(CompanyGroup, ProductCode,Description, COACode, COADescription, TaxType, TaxRate, Price, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@CompanyGroup, @ProductCode,@Description, @COACode, @COADescription, @TaxType, @TaxRate, @Price, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text.ToUpper)
                        command.Parameters.AddWithValue("@ProductCode", txtProductCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper)

                        'If ddlCOACode.SelectedIndex = 0 Then
                        '    command.Parameters.AddWithValue("@COACode", "")
                        'Else
                        '    command.Parameters.AddWithValue("@COACode", txtCOADescription.Text.ToUpper)
                        'End If

                        'command.Parameters.AddWithValue("@COADescription", ddlCOACode.SelectedItem.ToString.ToUpper)


                        If ddlCOACode.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@COACode", "")
                        Else
                            command.Parameters.AddWithValue("@COACode", cA(0).Trim.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@COADescription", txtCOADescription.Text.ToUpper)

                        If ddlCOACode.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@TaxType", "")
                        Else
                            command.Parameters.AddWithValue("@TaxType", ddlTaxType.Text.ToUpper)
                        End If


                        command.Parameters.AddWithValue("@TaxRate", Convert.ToDecimal(txtTaxRate.Text))
                        command.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text))

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text)
                        command.Parameters.AddWithValue("@ProductCode", txtProductCode.Text)
                        command.Parameters.AddWithValue("@Description", txtDescription.Text)


                        'If ddlCOACode.SelectedIndex = 0 Then
                        '    command.Parameters.AddWithValue("@COACode", "")
                        'Else
                        '    command.Parameters.AddWithValue("@COACode", txtCOADescription.Text.ToUpper)
                        'End If

                        'command.Parameters.AddWithValue("@COADescription", ddlCOACode.SelectedItem.ToString.ToUpper)

                        If ddlCOACode.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@COACode", "")
                        Else
                            command.Parameters.AddWithValue("@COACode", cA(0).Trim)
                        End If

                        command.Parameters.AddWithValue("@COADescription", txtCOADescription.Text)


                        If ddlCOACode.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@TaxType", "")
                        Else
                            command.Parameters.AddWithValue("@TaxType", (ddlTaxType.Text))
                        End If


                        command.Parameters.AddWithValue("@TaxRate", Convert.ToDecimal(txtTaxRate.Text))
                        command.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text))

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId

                    ' MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                End If
                conn.Close()

            Catch ex As Exception
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
                lblAlert.Text = ex.ToString
            End Try
            EnableControls()
            txtMode.Text = ""
        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtIndustry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode=@ProductCode and rcno<>" & Convert.ToInt32(txtRcno.Text)
                'command2.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text)
                command2.Parameters.AddWithValue("@ProductCode", txtProductCode.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Industry already exists!!!", "str")
                    lblAlert.Text = "BILLING CODE ALREADY EXISTS"
                    txtProductCode.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblbillingproducts where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String

                        If txtExists.Text = "True" Then
                            qry = "update tblbillingproducts set CompanyGroup=@CompanyGroup, Description=@Description,  COACode= @COACode, COADescription= @COADescription, TaxType = @TaxType, TaxRate= @TaxRate, Price=@Price,  LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tblbillingproducts set CompanyGroup=@CompanyGroup, ProductCode=@ProductCode,Description=@Description, COACode= @COACode, COADescription= @COADescription, TaxType = @TaxType, TaxRate= @TaxRate, Price=@Price, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        End If


                        command.CommandText = qry
                        command.Parameters.Clear()


                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text.ToUpper)
                            command.Parameters.AddWithValue("@ProductCode", txtProductCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper)
                            command.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text))

                         

                            'If ddlCOACode.SelectedIndex = 0 Then
                            '    command.Parameters.AddWithValue("@COACode", "")
                            'Else
                            '    command.Parameters.AddWithValue("@COACode", txtCOADescription.Text.ToUpper)
                            'End If

                            'command.Parameters.AddWithValue("@COADescription", ddlCOACode.SelectedItem.ToString.ToUpper)

                            If ddlCOACode.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@COACode", "")
                            Else
                                command.Parameters.AddWithValue("@COACode", cA(0).Trim.ToUpper)
                            End If

                            command.Parameters.AddWithValue("@COADescription", txtCOADescription.Text.ToUpper)


                            command.Parameters.AddWithValue("@TaxType", ddlTaxType.Text.ToUpper)
                            command.Parameters.AddWithValue("@TaxRate", Convert.ToDecimal(txtTaxRate.Text))

                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text)
                            command.Parameters.AddWithValue("@ProductCode", txtProductCode.Text)
                            command.Parameters.AddWithValue("@Description", txtDescription.Text)
                            command.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text))

                        

                            'If ddlCOACode.SelectedIndex = 0 Then
                            '    command.Parameters.AddWithValue("@COACode", "")
                            'Else
                            '    command.Parameters.AddWithValue("@COACode", txtCOADescription.Text)
                            'End If

                            'command.Parameters.AddWithValue("@COADescription", ddlCOACode.SelectedItem.ToString)

                            If ddlCOACode.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@COACode", "")
                            Else
                                command.Parameters.AddWithValue("@COACode", cA(0).Trim)
                            End If

                            command.Parameters.AddWithValue("@COADescription", txtCOADescription.Text)

                            command.Parameters.AddWithValue("@TaxType", (ddlTaxType.Text))
                            command.Parameters.AddWithValue("@TaxRate", Convert.ToDecimal(txtTaxRate.Text))

                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        End If


                        command.Connection = conn

                        command.ExecuteNonQuery()
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"

                        'If txtExists.Text = "True" Then
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO INDUSTRY CANNOT BE UPDATED"
                        '    lblAlert.Text = ""
                        'Else
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        '    lblAlert.Text = ""
                        'End If
                    End If
                End If

                conn.Close()

                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BILLCODE", txtProductCode.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BILLCODE", txtProductCode.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
                lblAlert.Text = ex.ToString
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
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"

        'If txtExists.Text = "True" Then
        '    txtProductCode.Enabled = False
        'Else
        '    txtProductCode.Enabled = True
        'End If
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



            '   MessageBox.Message.Confirm(Page, "Do you want to delete the selected record?", "str", vbYesNo)
            If txtExists.Text = "True" Then
                '  MessageBox.Message.Alert(Page, "Record is in use, cannot be deleted!!!", "str")
                lblAlert.Text = "RECORD IS IN USE, CANNOT BE DELETED"
                Return
            End If


            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblbillingproducts where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblbillingproducts where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    ' MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
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

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Response.Redirect("RV_MasterBillingCode.aspx")



    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT rcno FROM tblsalesdetail where ItemCode=@data limit 2"
        command1.Parameters.AddWithValue("@data", txtProductCode.Text)
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

    Protected Sub ddlCOACode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCOACode.SelectedIndexChanged

        'txtCOADescription.Text = ddlCOACode.SelectedValue

     
        Dim lCOACode As String
        lCOACode = ddlCOACode.SelectedItem.ToString
        Dim cA As String() = ddlCOACode.SelectedItem.ToString.Split(":"c)
        txtCOADescription.Text = cA(1).Trim.ToUpper

        Dim strCOA As String
        strCOA = ""
        strCOA = lCOACode


    End Sub

    Protected Sub ddlTaxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTaxType.SelectedIndexChanged
        'Start:Retrive Service Records

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim commandService As MySqlCommand = New MySqlCommand

        commandService.CommandType = CommandType.Text
        commandService.CommandText = "SELECT TaxType, TaxratePct FROM tbltaxtype where TaxType ='" & ddlTaxType.Text & "'"
        commandService.Connection = conn

        Dim drService As MySqlDataReader = commandService.ExecuteReader()
        Dim dtService As New DataTable
        dtService.Load(drService)

        If dtService.Rows.Count > 0 Then
            txtTaxRate.Text = Val(dtService.Rows(0)("TaxratePct").ToString).ToString("N4")
        End If
        conn.Close()
        'End:Retrieve Service Records
    End Sub

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        lblStatusMessage1.Text = ""
        If chkInactive.Checked = True Then
            '  lblOldStatus.Text = "Inactive"
            lblStatusMessage1.Text = "<u>INACTIVE CODE</u>" + "<br><br><br>" + "Billing Code : " + txtProductCode.Text + ", Description : " + txtDescription.Text + "<br><br><br>" + "Are you sure to make the Code 'ACTIVE'?"
        Else
            ' lblOldStatus.Text = "Active"
            lblStatusMessage1.Text = "<u>ACTIVE CODE</u>" + "<br><br><br>" + "Billing Code : " + txtProductCode.Text + ", Description : " + txtDescription.Text + "<br><br><br>" + "Are you sure to make the Code 'INACTIVE'?"
        End If
        mdlPopupStatus.Show()
    End Sub

    Protected Sub btnConfirmYes_Click(sender As Object, e As EventArgs) Handles btnConfirmYes.Click
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            If chkInactive.Checked = True Then

                command.CommandText = "UPDATE tblbillingproducts SET InActive= 0 where rcno=" & Convert.ToInt32(txtRcno.Text)
                chkInactive.Checked = False

            ElseIf chkInactive.Checked = False Then

                command.CommandText = "UPDATE tblbillingproducts SET InActive= 1 where rcno=" & Convert.ToInt32(txtRcno.Text)
                chkInactive.Checked = True

            End If

            'command.CommandText = "UPDATE tblCompany SET InActive='" + ddlNewStatus.SelectedValue + "' where rcno=" & Convert.ToInt32(txtRcno.Text)
            command.Connection = conn
            command.ExecuteNonQuery()

            '   UpdateContractActSvcDate(conn)

            conn.Close()
            conn.Dispose()
            command.Dispose()

            'ddlStatus.Text = ddlNewStatus.Text
            '  ddlNewStatus.SelectedIndex = 0

            lblMessage.Text = "ACTION: STATUS UPDATED"

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST", txtProductCode.Text, "CHST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtProductCode.Text, "", txtRcno.Text)
            'txtPostStatus.Text = ddlNewStatus.SelectedValue


            'GridView1.DataSourceID = "SqlDataSource1"

            'SqlDataSource1.SelectCommand = txt.Text
            'SqlDataSource1.DataBind()
            GridView1.DataBind()

            'InsertNewLog()


            mdlPopupStatus.Hide()
        Catch ex As Exception
            'InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "CHST - BTNCONFIRMYES_CLICK", ex.Message.ToString, txtAccountID.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

        End Try
    End Sub
End Class
