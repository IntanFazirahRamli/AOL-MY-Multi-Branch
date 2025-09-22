Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_Service
    Inherits System.Web.UI.Page

     Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtProdID.Text = ""
        txtDescription.Text = ""
        txtEstVal.Text = "0.00"
        txtCostVal.Text = "0.00"
        txtAction.Text = ""
        txtTarget.Text = ""
        txtRcno.Text = ""
        txtSalesCommissionPerc.Text = "0.00"
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        chkWithArea.Checked = False
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
        txtProdID.Enabled = False
        txtDescription.Enabled = False
        txtEstVal.Enabled = False
        txtCostVal.Enabled = False
        txtAction.Enabled = False
        txtTarget.Enabled = False
        chkWithArea.Enabled = False
        txtSalesCommissionPerc.Enabled = False

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

        txtProdID.Enabled = True
        txtDescription.Enabled = True
        txtEstVal.Enabled = True
        txtCostVal.Enabled = True
        txtAction.Enabled = True
        txtTarget.Enabled = True
        chkWithArea.Enabled = True
        txtSalesCommissionPerc.Enabled = True

        AccessControl()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If
        'EnableControls()
        MakeMeNull()
        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()

        If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtProdID.Text = ""
        Else
            txtProdID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtDescription.Text = ""
        Else
            txtDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtEstVal.Text = ""
        Else
            txtEstVal.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Then
            txtCostVal.Text = ""
        Else
            txtCostVal.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
            txtAction.Text = ""
        Else
            txtAction.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
            txtTarget.Text = ""
        Else
            txtTarget.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
        End If

        If Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString = "Y" Then
            chkWithArea.Checked = True
        Else
            chkWithArea.Checked = False
        End If

        If GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
            txtSalesCommissionPerc.Text = ""
        Else
            txtSalesCommissionPerc.Text = (GridView1.SelectedRow.Cells(8).Text).ToString
        End If

        txtMode.Text = "View"

        'txtMode.Text = "Edit"
        '  DisableControls()
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
        txtProdID.Focus()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

        End If
    End Sub

    Private Sub AccessControl()
        Try
            '''''''''''''''''''Access Control 
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            'command.CommandText = "SELECT x0159,  x0159Add, x0159Edit, x0159Delete, x0159Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.CommandText = "SELECT x0159,  x0159Add, x0159Edit, x0159Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If Not IsDBNull(dt.Rows(0)("x0159")) Then
                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0159"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0159")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If
                End If


                If Not IsDBNull(dt.Rows(0)("x0159Add")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0159Add")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0159Add").ToString()
                    End If
                End If


                'Me.btnInsert.Enabled = vpSec2412Add
                If txtMode.Text = "View" Then
                    If Not IsDBNull(dt.Rows(0)("x0159Edit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0159Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0159Edit").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0159Delete")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0159Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("x0159Delete").ToString()
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


                If btnPrint.Enabled = True Then
                    btnPrint.ForeColor = System.Drawing.Color.Black
                Else
                    btnPrint.ForeColor = System.Drawing.Color.Gray
                End If


            End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtProdID.Text.Trim = "" Then
            '  MessageBox.Message.Alert(Page, "Product ID cannot be blank!!!", "str")
            lblAlert.Text = "PRODUCT ID CANNOT BE BLANK"
            Return
        End If
        If txtDescription.Text.Trim = "" Then
            ' MessageBox.Message.Alert(Page, "Product Description cannot be blank!!!", "str")
            lblAlert.Text = "PRODUCT DESCRIPTION CANNOT BE BLANK"
            Return
        End If
        If IsNumeric(txtSalesCommissionPerc.Text) = False Then
            '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
            lblAlert.Text = "SALES COMMISSION PERCENT SHOULD BE NUMERIC ONLY"
            Return

        End If
        If IsNumeric(txtEstVal.Text) = False Then
            '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
            lblAlert.Text = "ESTIMATE VALUE SHOULD BE NUMERIC ONLY"
            Return

        End If
        If IsNumeric(txtCostVal.Text) = False Then
            '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
            lblAlert.Text = "COST VALUE SHOULD BE NUMERIC ONLY"
            Return

        End If
        '#Region "NewRecord"

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text
                command1.CommandText = "SELECT * FROM tblProduct where ProductID=@ProdId"
                command1.Parameters.AddWithValue("@ProdId", txtProdID.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtProdID.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblProduct(ProductID,ProductDesc,EstimateValue,Action,CostValue,Target, WithArea, SalesCommissionPerc, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@ProductID,@ProductDesc,@EstimateValue,@Action,@CostValue,@Target, @WithArea, @SalesCommissionPerc, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@ProductID", txtProdID.Text.ToUpper)
                        command.Parameters.AddWithValue("@ProductDesc", txtDescription.Text.ToUpper)
                        If txtEstVal.Text.Trim = "" Then
                            txtEstVal.Text = 0
                        End If
                        If txtCostVal.Text.Trim = "" Then
                            txtCostVal.Text = 0
                        End If
                        command.Parameters.AddWithValue("@EstimateValue", txtEstVal.Text.ToUpper)
                        command.Parameters.AddWithValue("@Action", txtAction.Text.ToUpper)
                        command.Parameters.AddWithValue("@CostValue", txtCostVal.Text.ToUpper)
                        command.Parameters.AddWithValue("@Target", txtTarget.Text.ToUpper)

                        If chkWithArea.Checked = True Then
                            command.Parameters.AddWithValue("@WithArea", "Y")
                        Else
                            command.Parameters.AddWithValue("@WithArea", "N")
                        End If

                        command.Parameters.AddWithValue("@SalesCommissionPerc", txtSalesCommissionPerc.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@ProductID", txtProdID.Text.ToUpper)
                        command.Parameters.AddWithValue("@ProductDesc", txtDescription.Text.ToUpper)

                        If txtEstVal.Text.Trim = "" Then
                            txtEstVal.Text = 0
                        End If
                        If txtCostVal.Text.Trim = "" Then
                            txtCostVal.Text = 0
                        End If

                        command.Parameters.AddWithValue("@EstimateValue", txtEstVal.Text)
                        command.Parameters.AddWithValue("@Action", txtAction.Text)
                        command.Parameters.AddWithValue("@CostValue", txtCostVal.Text)
                        command.Parameters.AddWithValue("@Target", txtTarget.Text)

                        If chkWithArea.Checked = True Then
                            command.Parameters.AddWithValue("@WithArea", "Y")
                        Else
                            command.Parameters.AddWithValue("@WithArea", "N")
                        End If

                        command.Parameters.AddWithValue("@SalesCommissionPerc", txtSalesCommissionPerc.Text)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    End If

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId

                    '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                    End If
                    conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""



        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                '      MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblProduct where ProductID=@ProdId and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@ProdId", txtProdID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then
                    '  MessageBox.Message.Alert(Page, "Product already exists!!!", "str")
                    lblAlert.Text = "PRODUCT ALREADY EXISTS"
                    txtProdID.Focus()
                    Exit Sub
                Else
                    Dim command1 As MySqlCommand = New MySqlCommand
                    command1.CommandType = CommandType.Text
                    command1.CommandText = "SELECT * FROM tblProduct where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "update tblProduct set ProductDesc=@Description,EstimateValue=@EstimateValue,Action=@Action,CostValue=@CostValue,Target=@Target, WithArea= @WithArea, SalesCommissionPerc=@SalesCommissionPerc, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tblProduct set ProductID=@ProductID,ProductDesc=@Description,EstimateValue=@EstimateValue,Action=@Action,CostValue=@CostValue,Target=@Target, WithArea= @WithArea, SalesCommissionPerc=@SalesCommissionPerc, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        End If


                        command.CommandText = qry
                        command.Parameters.Clear()
                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@ProductID", txtProdID.Text.ToUpper)
                            command.Parameters.AddWithValue("@ProductDesc", txtDescription.Text.ToUpper)
                            If txtEstVal.Text.Trim = "" Then
                                txtEstVal.Text = 0
                            End If
                            If txtCostVal.Text.Trim = "" Then
                                txtCostVal.Text = 0
                            End If
                            command.Parameters.AddWithValue("@EstimateValue", txtEstVal.Text.ToUpper)
                            command.Parameters.AddWithValue("@Action", txtAction.Text.ToUpper)
                            command.Parameters.AddWithValue("@CostValue", txtCostVal.Text.ToUpper)
                            command.Parameters.AddWithValue("@Target", txtTarget.Text.ToUpper)

                            If chkWithArea.Checked = True Then
                                command.Parameters.AddWithValue("@WithArea", "Y")
                            Else
                                command.Parameters.AddWithValue("@WithArea", "N")
                            End If

                            command.Parameters.AddWithValue("@SalesCommissionPerc", txtSalesCommissionPerc.Text.ToUpper)
                             command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@ProductID", txtProdID.Text.ToUpper)
                            command.Parameters.AddWithValue("@ProductDesc", txtDescription.Text.ToUpper)
                            If txtEstVal.Text.Trim = "" Then
                                txtEstVal.Text = 0
                            End If
                            If txtCostVal.Text.Trim = "" Then
                                txtCostVal.Text = 0
                            End If
                            command.Parameters.AddWithValue("@EstimateValue", txtEstVal.Text)
                            command.Parameters.AddWithValue("@Action", txtAction.Text)
                            command.Parameters.AddWithValue("@CostValue", txtCostVal.Text)
                            command.Parameters.AddWithValue("@Target", txtTarget.Text)

                            If chkWithArea.Checked = True Then
                                command.Parameters.AddWithValue("@WithArea", "Y")
                            Else
                                command.Parameters.AddWithValue("@WithArea", "N")
                            End If

                            command.Parameters.AddWithValue("@SalesCommissionPerc", txtSalesCommissionPerc.Text)
                             command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                        End If
                        command.Connection = conn
                        command.ExecuteNonQuery()
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"

                        'If txtExists.Text = "True" Then
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO PRODUCT ID AND DESCRIPTION CANNOT BE UPDATED"
                        '    lblAlert.Text = ""
                        'Else
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        '    lblAlert.Text = ""
                        'End If
                    End If
                End If

                conn.Close()
                '#End Region
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
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

        If txtExists.Text = "True" Then
            txtProdID.Enabled = False
        Else
            txtProdID.Enabled = True
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

                command1.CommandText = "SELECT * FROM tblProduct where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblProduct where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
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
        Response.Redirect("RV_MasterService.aspx")
    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()


        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text

        command3.CommandText = "SELECT * FROM tblcontractdet where ServiceID=@data limit 1"
        command3.Parameters.AddWithValue("@data", txtProdID.Text)
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            Return True
        End If

        Dim command4 As MySqlCommand = New MySqlCommand

        command4.CommandType = CommandType.Text

        command4.CommandText = "SELECT * FROM tblservicerecorddet where ServiceID=@data limit 1"
        command4.Parameters.AddWithValue("@data", txtProdID.Text)
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
End Class