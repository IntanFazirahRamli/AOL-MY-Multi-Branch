Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Drawing
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel

Partial Class Master_EInvoiceMSICode
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtIndustrialClassificationCode.Text = ""
        txtMode.Text = ""
        txtRcno.Text = ""
        txtDescription.Text = ""
        ''txtCreatedBy.Text = ""
        'txtCreatedOn.Text = ""
        'txtModifiedBy.Text = ""
        'txtModifiedOn.Text = ""

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
        txtIndustrialClassificationCode.Enabled = False
        txtDescription.Enabled = False


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

        txtIndustrialClassificationCode.Enabled = True
        txtDescription.Enabled = True

        AccessControl()
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

        If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtIndustrialClassificationCode.Text = ""
        Else

            txtIndustrialClassificationCode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtDescription.Text = ""
        Else

            txtDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If

        txtMode.Text = "View"
        'txtMode.Text = "Edit"
        ' DisableControls()


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

        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtIndustrialClassificationCode.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then

            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtModifiedBy.Text = UserID
        End If
    End Sub

    Private Sub AccessControl()

        Try
            '''''''''''''''''''Access Control 
            'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            'command.CommandText = "SELECT X0109,  X0109Add, X0109Edit, X0109Delete, X0109Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.CommandText = "SELECT x0197EinvoiceMSIC,  x0197EinvoiceMSICAdd, x0197EinvoiceMSICEdit, x0197EinvoiceMSICDelete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)
            conn.Close()

            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0)("x0197EinvoiceMSIC")) Then
                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0197EinvoiceMSIC"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0197EinvoiceMSIC")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0197EinvoiceMSICAdd")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0197EinvoiceMSICAdd")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0197EinvoiceMSICAdd").ToString()
                    End If
                End If

                If txtMode.Text = "View" Then
                    If Not IsDBNull(dt.Rows(0)("x0197EinvoiceMSICEdit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0197EinvoiceMSICEdit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0197EinvoiceMSICEdit").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0197EinvoiceMSICDelete")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0197EinvoiceMSICDelete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("x0197EinvoiceMSICDelete").ToString()
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
            'End If


            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtIndustrialClassificationCode.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
            lblAlert.Text = "UNIT CODE CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tbleinvoicemalaysiaSICode where IndustrialClassificationCode=@IndustrialClassificationCode"
                command1.Parameters.AddWithValue("@IndustrialClassificationCode", txtIndustrialClassificationCode.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtIndustrialClassificationCode.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tbleinvoicemalaysiaSICode(IndustrialClassificationCode, Description, CreatedBy,CreatedOn,ModifiedBy,ModifiedOn)"
                    qry = qry + "VALUES(@IndustrialClassificationCode, @Description, @CreatedBy,@CreatedOn,@ModifiedBy,@ModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@IndustrialClassificationCode", txtIndustrialClassificationCode.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@ModifiedBy", txtModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@ModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@IndustrialClassificationCode", txtIndustrialClassificationCode.Text.Trim)
                        command.Parameters.AddWithValue("@Description", txtDescription.Text.Trim)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@ModifiedBy", txtModifiedBy.Text)
                        command.Parameters.AddWithValue("@ModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId
                    'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                End If
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

                command2.CommandText = "SELECT * FROM tbleinvoicemalaysiaSICode where IndustrialClassificationCode=@IndustrialClassificationCode and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@IndustrialClassificationCode", txtIndustrialClassificationCode.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Company Group already exists!!!", "str")
                    lblAlert.Text = "UNIT CODE ALREADY EXISTS"
                    txtIndustrialClassificationCode.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tbleinvoicemalaysiaSICode where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "update tbleinvoicemalaysiaSICode set ModifiedBy=@ModifiedBy,ModifiedOn=@ModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tbleinvoicemalaysiaSICode set IndustrialClassificationCode=@IndustrialClassificationCode, Description =@Description, ModifiedBy=@ModifiedBy,ModifiedOn=@ModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@IndustrialClassificationCode", txtIndustrialClassificationCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper.Trim)
                            command.Parameters.AddWithValue("@ModifiedBy", txtModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@ModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@IndustrialClassificationCode", txtIndustrialClassificationCode.Text)
                            command.Parameters.AddWithValue("@Description", txtDescription.Text.Trim)
                            command.Parameters.AddWithValue("@ModifiedBy", txtModifiedBy.Text)
                            command.Parameters.AddWithValue("@ModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


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
                End If

                conn.Close()

                txtMode.Text = ""

                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MSICode", txtIndustrialClassificationCode.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MSICode", txtIndustrialClassificationCode.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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

        If txtExists.Text = "True" Then
            txtIndustrialClassificationCode.Enabled = False
        Else
            txtIndustrialClassificationCode.Enabled = True
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

                command1.CommandText = "SELECT * FROM tbleinvoicemalaysiaSICode where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tbleinvoicemalaysiaSICode where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ClassificationID", txtIndustrialClassificationCode.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
    '    Response.Redirect("RV_MasterCompanyGroup.aspx")
    'End Sub

    Private Function CheckIfExists() As Boolean

        Return False

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblcompany where companygroup=@data"
        command1.Parameters.AddWithValue("@data", txtIndustrialClassificationCode.Text)
        command1.Connection = conn

        Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then
            Return True
        End If

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT * FROM tblperson where persongroup=@data"
        command2.Parameters.AddWithValue("@data", txtIndustrialClassificationCode.Text)
        command2.Connection = conn

        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
        Dim dt2 As New DataTable
        dt2.Load(dr2)

        If dt2.Rows.Count > 0 Then
            Return True
        End If

        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text

        command3.CommandText = "SELECT * FROM tblcontract where companygroup=@data"
        command3.Parameters.AddWithValue("@data", txtIndustrialClassificationCode.Text)
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            Return True
        End If

        Dim command4 As MySqlCommand = New MySqlCommand

        command4.CommandType = CommandType.Text

        command4.CommandText = "SELECT * FROM tblservicerecord where companygroup=@data"
        command4.Parameters.AddWithValue("@data", txtIndustrialClassificationCode.Text)
        command4.Connection = conn

        Dim dr4 As MySqlDataReader = command4.ExecuteReader()
        Dim dt4 As New DataTable
        dt4.Load(dr4)

        If dt4.Rows.Count > 0 Then
            Return True
        End If
        conn.Close()

        Return False
    End Function

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
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

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
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

    Protected Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click

        mdlPopupImportMsg.Show()

    End Sub

    Protected Sub btnOkImportMsg_Click(sender As Object, e As EventArgs) Handles btnOkImportMsg.Click

        If rdbImportOptions.SelectedIndex = 0 Then
            ImportfromJSON()
            GridView1.DataSourceID = "SqlDataSource1"

            lblMessage.Text = "Total Records Imported : " + txtCount.Text + "<br>" + " Success : " + txtSuccessCount.Text + ", Failure : " + txtFailureCount.Text '+ " Failed AccountID : " + txtFailureString.Text
            mdlPopupImportMsg.Hide()

        ElseIf rdbImportOptions.SelectedIndex = 1 Then
            ImportFromExcel()
            GridView1.DataSourceID = "SqlDataSource1"

            lblMessage.Text = "Total Records Imported : " + txtCount.Text + "<br>" + " Success : " + txtSuccessCount.Text + ", Failure : " + txtFailureCount.Text '+ " Failed AccountID : " + txtFailureString.Text
            mdlPopupImportMsg.Hide()

        End If
    End Sub

    Private Sub ImportfromJSON()
        DownloadURL()


        Dim dt As New System.Data.DataTable
        Dim jsonInput As String = File.ReadAllText(Server.MapPath("~/json/ClassificationCodes.json"))
        ' Dim jsonInput As String = File.ReadAllText("https://sdk.myinvois.hasil.gov.my/files/ClassificationCodes.json")
        dt = Newtonsoft.Json.JsonConvert.DeserializeObject(Of System.Data.DataTable)(jsonInput)
        txtCount.Text = dt.Rows.Count.ToString

        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        txtResult.Text = InsertClassificationCodeData(dt, conn)
        conn.Close()
    End Sub

    Private Sub DownloadURL()

        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
        Dim webClient As WebClient = New WebClient()

        webClient.DownloadFile("https://sdk.myinvois.hasil.gov.my/files/ClassificationCodes.json", Server.MapPath("~\json\ClassificationCodes.json"))

    End Sub

    Protected Function InsertClassificationCodeData(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable

        Dim query As String = "INSERT INTO tbleinvoicemalaysiaSICode(IndustrialClassificationCode,Description,CreatedBy,CreatedOn,"
        query = query + "ModifiedBy,ModifiedOn,Source) values(@IndustrialClassificationCode,@Description,@CreatedBy,@CreatedOn,"
        query = query + "@ModifiedBy,@ModifiedOn,@Source)"

        Dim IndustrialClassificationCode As String = ""
        Dim Description As String = ""
        Dim Exists As Boolean = True

        Dim drow As DataRow
        Dim dc1 As DataColumn = New DataColumn("IndustrialClassificationCode", GetType(String))
        Dim dc2 As DataColumn = New DataColumn("Description", GetType(String))
        Dim dc3 As DataColumn = New DataColumn("Status", GetType(String))
        Dim dc4 As DataColumn = New DataColumn("Remarks", GetType(String))
        dtLog.Columns.Add(dc1)
        dtLog.Columns.Add(dc2)
        dtLog.Columns.Add(dc3)
        dtLog.Columns.Add(dc4)

        For Each r As DataRow In dt.Rows

            drow = dtLog.NewRow()

            If IsDBNull(r("Code")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + IndustrialClassificationCode + "(CODE_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "CODE_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                ' InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("AccountID"))

                IndustrialClassificationCode = r("Code")
                drow("IndustrialClassificationCode") = IndustrialClassificationCode
                Description = r("Description")

            End If

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT * FROM tbleinvoicemalaysiaSICode where IndustrialClassificationCode=@code and Description=@desc"
            command2.Parameters.AddWithValue("@code", IndustrialClassificationCode)
            command2.Parameters.AddWithValue("@desc", Description)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                '  MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")
                ' lblAlert.Text = "Account ID already exists!!!"
                Failure = Failure + 1
                FailureString = FailureString + " " + IndustrialClassificationCode + "(DUPLICATE)"
                drow("Status") = "Failed"
                drow("Remarks") = "CODE_DUPLICATE"
                If IsDBNull(r("Description")) = False Then
                    drow("Description") = r("Description")
                End If
                dtLog.Rows.Add(drow)
            Else
                Dim command3 As MySqlCommand = New MySqlCommand

                command3.CommandType = CommandType.Text

                command3.CommandText = "SELECT * FROM tbleinvoicemalaysiaSICode where IndustrialClassificationCode=@code"
                command3.Parameters.AddWithValue("@code", IndustrialClassificationCode)
                command3.Connection = conn

                Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                Dim dt3 As New System.Data.DataTable
                dt3.Load(dr3)

                If dt3.Rows.Count > 0 Then

                Else
                    Dim cmd As MySqlCommand = conn.CreateCommand()
                    '  Dim cmd As MySqlCommand = New MySqlCommand

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = query
                    cmd.Parameters.Clear()

                    If IsDBNull(r("Code")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + IndustrialClassificationCode + "(CODE_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "CODE_BLANK"
                        If IsDBNull(r("Description")) = False Then
                            drow("Name") = r("Description")
                        End If
                        dtLog.Rows.Add(drow)

                        Continue For

                    Else
                        cmd.Parameters.AddWithValue("@IndustrialClassificationCode", r("Code").ToString.ToUpper)
                    End If

                    If IsDBNull(r("Description")) Then
                        Failure = Failure + 1
                        FailureString = FailureString + " " + IndustrialClassificationCode + "(DESC_BLANK)"
                        drow("Status") = "Failed"
                        drow("Remarks") = "DESC_BLANK"
                        dtLog.Rows.Add(drow)
                        Continue For
                    Else
                        drow("Description") = r("Description")
                        cmd.Parameters.AddWithValue("@Description", r("Description").ToString.ToUpper)
                    End If

                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text + "_IMPORT")
                    cmd.Parameters.AddWithValue("@ModifiedBy", txtCreatedBy.Text + "_IMPORT")
                    cmd.Parameters.AddWithValue("@ModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    cmd.Parameters.AddWithValue("@Source", "IMPORT")

                    cmd.Connection = conn

                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    Success = Success + 1
                    drow("Status") = "Success"
                    drow("Remarks") = ""
                    dtLog.Rows.Add(drow)
                End If

            End If
        Next

        txtSuccessCount.Text = Success.ToString
        txtFailureCount.Text = Failure.ToString
        txtFailureString.Text = FailureString

    End Function

    Private Sub ImportFromExcel()
        Dim ofilename As String = ""
        Dim sfilename As String = ""

        ofilename = Path.GetFileName(FileUpload1.PostedFile.FileName)
        Dim ext As String = Path.GetExtension(ofilename)
        sfilename = ofilename.Split("."c)(0)

        Dim folderPath As String = Server.MapPath("~/Uploads/Excel/")
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If
        Dim fileName As String = folderPath + sfilename + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + "_" + txtCreatedBy.Text + ext

        If System.IO.File.Exists(fileName) Then

            System.IO.File.Delete(fileName)
        End If
        'Save the File to the Directory (Folder).
        FileUpload1.PostedFile.SaveAs(fileName)

        txtWorkBookName.Text = sfilename
        Dim file As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)
        Dim dropdownworkbook = New XSSFWorkbook(file)
        file.Close()
        file.Dispose()

        Dim dt As New DataTable
        dt = Excel_To_DataTable(fileName, 0)
        txtCount.Text = dt.Rows.Count.ToString

        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        txtResult.Text = InsertClassificationCodeData(dt, conn)
        conn.Close()
    End Sub

    Private Function Excel_To_DataTable(ByVal pRutaArchivo As String, ByVal pHojaIndex As Integer) As DataTable
        Dim Tabla As DataTable = Nothing

        Try

            If System.IO.File.Exists(pRutaArchivo) Then
                Dim workbook As IWorkbook = Nothing
                Dim worksheet As ISheet = Nothing
                Dim first_sheet_name As String = ""

                Using FS As FileStream = New FileStream(pRutaArchivo, FileMode.Open, FileAccess.Read)
                    workbook = WorkbookFactory.Create(FS)
                    worksheet = workbook.GetSheetAt(pHojaIndex)
                    first_sheet_name = worksheet.SheetName

                    'If worksheet.SheetName.ToUpper = rdbModule.SelectedValue.ToString.ToUpper Then
                    '  InsertIntoTblWebEventLog("TestExcel0", worksheet.SheetName.ToUpper + worksheet.LastRowNum.ToString + "Aa", "")


                    If worksheet.LastRowNum > 2 Then

                        Tabla = New DataTable(first_sheet_name)
                        Tabla.Rows.Clear()
                        Tabla.Columns.Clear()

                        For rowIndex As Integer = 0 To worksheet.LastRowNum

                            Dim NewReg As DataRow = Nothing
                            Dim row As IRow = worksheet.GetRow(rowIndex)
                            Dim row2 As IRow = Nothing
                            Dim row3 As IRow = Nothing

                            If rowIndex = 0 Then
                                row2 = worksheet.GetRow(rowIndex + 2)
                                row3 = worksheet.GetRow(rowIndex + 3)
                            End If
                            '    InsertIntoTblWebEventLog("TestExcel1", rowIndex.ToString, worksheet.LastRowNum.ToString)

                            If row IsNot Nothing Then
                                If rowIndex > 1 Then NewReg = Tabla.NewRow()
                                Dim colIndex As Integer = 0

                                For Each cell As ICell In row.Cells
                                    Dim valorCell As Object = Nothing
                                    Dim cellType As String = ""
                                    Dim cellType2 As String() = New String(1) {}
                                    If rowIndex = 1 Then

                                    ElseIf rowIndex = 0 Then

                                        For i As Integer = 0 To 2 - 1
                                            Dim cell2 As ICell = Nothing

                                            If i = 0 Then
                                                cell2 = row2.GetCell(cell.ColumnIndex)
                                            Else
                                                cell2 = row3.GetCell(cell.ColumnIndex)
                                            End If
                                            '   InsertIntoTblWebEventLog("TestExcel5", cell.ColumnIndex.ToString, i.ToString)

                                            If cell2 IsNot Nothing Then

                                                Select Case cell2.CellType
                                                    Case NPOI.SS.UserModel.CellType.Blank
                                                        'cellType2(i) = "System.String"
                                                    Case NPOI.SS.UserModel.CellType.Boolean
                                                        cellType2(i) = "System.Boolean"
                                                    Case NPOI.SS.UserModel.CellType.String
                                                        cellType2(i) = "System.String"
                                                    Case NPOI.SS.UserModel.CellType.Numeric

                                                        If HSSFDateUtil.IsCellDateFormatted(cell2) Then
                                                            cellType2(i) = "System.DateTime"
                                                        Else
                                                            cellType2(i) = "System.Double"
                                                        End If

                                                    Case NPOI.SS.UserModel.CellType.Formula
                                                        Dim continuar As Boolean = True

                                                        Select Case cell2.CachedFormulaResultType
                                                            Case NPOI.SS.UserModel.CellType.Boolean
                                                                cellType2(i) = "System.Boolean"
                                                            Case NPOI.SS.UserModel.CellType.String
                                                                cellType2(i) = "System.String"
                                                            Case NPOI.SS.UserModel.CellType.Numeric

                                                                If HSSFDateUtil.IsCellDateFormatted(cell2) Then
                                                                    cellType2(i) = "System.DateTime"
                                                                Else

                                                                    Try

                                                                        If cell2.CellFormula = "TRUE()" Then
                                                                            cellType2(i) = "System.Boolean"
                                                                            continuar = False
                                                                        End If

                                                                        If continuar AndAlso cell2.CellFormula = "FALSE()" Then
                                                                            cellType2(i) = "System.Boolean"
                                                                            continuar = False
                                                                        End If

                                                                        If continuar Then
                                                                            cellType2(i) = "System.Double"
                                                                            continuar = False
                                                                        End If

                                                                    Catch
                                                                    End Try
                                                                End If
                                                        End Select

                                                    Case Else
                                                        cellType2(i) = "System.String"
                                                End Select
                                            Else
                                                cellType2(i) = "System.String"
                                            End If
                                            '  InsertIntoTblWebEventLog("TestExcel4", "", cellType2(i).ToString)

                                        Next

                                        If cellType2(0) = cellType2(1) Then
                                            cellType = cellType2(0)
                                        Else
                                            If cellType2(0) Is Nothing Then cellType = cellType2(1)
                                            If cellType2(1) Is Nothing Then cellType = cellType2(0)
                                            If cellType = "" Then cellType = "System.String"
                                        End If

                                        Dim colName As String = "Column_{0}"

                                        Try
                                            colName = cell.StringCellValue
                                        Catch
                                            colName = String.Format(colName, colIndex)
                                        End Try
                                        ' InsertIntoTblWebEventLog("TestExcel2", colName, colIndex)


                                        For Each col As DataColumn In Tabla.Columns
                                            If col.ColumnName = colName Then colName = String.Format("{0}_{1}", colName, colIndex)
                                        Next
                                        '   InsertIntoTblWebEventLog("TestExcel3", colName, cellType)

                                        Dim codigo As DataColumn = New DataColumn(colName, System.Type.[GetType](cellType))
                                        Tabla.Columns.Add(codigo)
                                        colIndex += 1
                                    Else

                                        Select Case cell.CellType
                                            Case NPOI.SS.UserModel.CellType.Blank
                                                valorCell = DBNull.Value
                                            Case NPOI.SS.UserModel.CellType.Boolean
                                                'Select Case cell.BooleanCellValue
                                                '    Case True
                                                valorCell = cell.BooleanCellValue
                                                '    Case False
                                                '        valorCell = cell.BooleanCellValue
                                                '    Case Else
                                                '        valorCell = False

                                                'End Select

                                            Case NPOI.SS.UserModel.CellType.String
                                                valorCell = cell.StringCellValue
                                            Case NPOI.SS.UserModel.CellType.Numeric

                                                If HSSFDateUtil.IsCellDateFormatted(cell) Then
                                                    valorCell = cell.DateCellValue
                                                Else
                                                    valorCell = cell.NumericCellValue
                                                End If

                                            Case NPOI.SS.UserModel.CellType.Formula

                                                Select Case cell.CachedFormulaResultType
                                                    Case NPOI.SS.UserModel.CellType.Blank
                                                        valorCell = DBNull.Value
                                                    Case NPOI.SS.UserModel.CellType.String
                                                        valorCell = cell.StringCellValue
                                                    Case NPOI.SS.UserModel.CellType.Boolean
                                                        valorCell = cell.BooleanCellValue
                                                    Case NPOI.SS.UserModel.CellType.Numeric

                                                        If HSSFDateUtil.IsCellDateFormatted(cell) Then
                                                            valorCell = cell.DateCellValue
                                                        Else
                                                            valorCell = cell.NumericCellValue
                                                        End If
                                                End Select

                                            Case Else
                                                valorCell = cell.StringCellValue
                                        End Select
                                        '   InsertIntoTblWebEventLog("TestExcel6", cell.ColumnIndex.ToString + " " + cell.CellType.ToString, valorCell.ToString)

                                        'If cell.CellType.ToString = "Boolean" Then
                                        '    If valorCell.ToString = "TRUE" Or valorCell.ToString = "FALSE" Then

                                        '    Else
                                        '        Continue For

                                        '    End If
                                        'End If

                                        If cell.ColumnIndex <= Tabla.Columns.Count - 1 Then NewReg(cell.ColumnIndex) = valorCell
                                    End If
                                Next
                            End If

                            If rowIndex > 1 Then Tabla.Rows.Add(NewReg)
                        Next


                        Tabla.AcceptChanges()
                    Else
                        lblMessage.Text = "Please import more than one Code details to proceed."
                    End If
                    'Else
                    '    lblMessage.Text = "Sheet Name does not match with the selected template."
                    '  End If

                End Using
            Else
                Throw New Exception("ERROR 404")
            End If

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            '  InsertIntoTblWebEventLog("Excel_To_DataTable", ex.Message.ToString, txtCreatedBy.Text)
            Throw ex

        End Try

        Return Tabla
    End Function
End Class

