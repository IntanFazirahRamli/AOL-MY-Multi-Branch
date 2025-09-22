Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_Stock
    Inherits System.Web.UI.Page

    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtItemID.Text = ""
        txtItemName.Text = ""
        txtUnitMS.SelectedIndex = 0
        ' ddlcountry.Text = ""
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""

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

        txtItemID.Enabled = False
        txtItemName.Enabled = False
        txtUnitMS.Enabled = False
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

        txtItemID.Enabled = True
        txtItemName.Enabled = True
        txtUnitMS.Enabled = True

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
            txtItemID.Text = ""
        Else

            txtItemID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtItemName.Text = ""
        Else
            txtItemName.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtUnitMS.Text = ""
        Else
            txtUnitMS.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
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
        txtItemID.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim query As String

            query = "Select UnitMS from tblUnitMS Order by UnitMS"
            PopulateDropDownList(query, "UnitMS", "UnitMS", txtUnitMS)
        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")

    End Sub

    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Try
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
                cmd.Dispose()
            End Using
            'End Using
        Catch ex As Exception
            InsertIntoTblWebEventLog("STOCK MASTER - " + Session("UserID"), "FUNCTION PopulateDropDownList", ex.Message.ToString, textField.Trim() & valueField.Trim())
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AccessControl()
        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            'command.CommandText = "SELECT X0107,  X0107Add, X0107Edit, X0107Delete, X0107Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.CommandText = "SELECT x0182,  x0182Add, x0182Edit, x0182Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)
            conn.Close()

            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0)("x0182")) Then
                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0182"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0182")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0182Add")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0182Add")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0182Add").ToString()
                    End If
                End If


                If txtMode.Text = "View" Then
                    If Not IsDBNull(dt.Rows(0)("x0182Edit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0182Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0182Edit").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0182Delete")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0182Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("x0182Delete").ToString()
                        End If
                    End If
                Else
                    Me.btnEdit.Enabled = False
                    Me.btnDelete.Enabled = False
                End If

                'If String.IsNullOrEmpty(dt.Rows(0)("X0107Print")) = False Then
                '    Me.btnDelete.Enabled = dt.Rows(0)("X0107Print").ToString()
                'End If

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
        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtItemID.Text.Trim = "" Then
            ' MessageBox.Message.Alert(Page, "City cannot be blank!!!", "str")
            lblAlert.Text = "ID CANNOT BE BLANK"
            Return

        End If

        If txtItemName.Text.Trim = "" Then
            ' MessageBox.Message.Alert(Page, "City cannot be blank!!!", "str")
            lblAlert.Text = "ITEM NAME CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblstockiTEM where ItemId =@ItemId"
                command1.Parameters.AddWithValue("@ItemId", txtItemID.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtItemID.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblStockItem(ItemId, ItemName,UnitMS, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@ItemId,@ItemName, @UnitMS,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@ItemId", txtItemID.Text.ToUpper)
                        command.Parameters.AddWithValue("@ItemName", txtItemName.Text.ToUpper)
                        command.Parameters.AddWithValue("@UnitMS", txtUnitMS.Text.ToUpper)

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@ItemId", txtItemID.Text)
                        command.Parameters.AddWithValue("@ItemName", txtItemName.Text)
                        command.Parameters.AddWithValue("@UnitMS", txtUnitMS.Text)
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
                MessageBox.Message.Alert(Page, "Error!!!" + ex.Message.ToString, "str")
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
                'ind = txtcity.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblStockItem where Itemid=@Itemid and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@Itemid", txtItemID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "city already exists!!!", "str")
                    lblAlert.Text = "ITEM ALREADY EXISTS"
                    txtItemID.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblStockItem where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "update tblStockItem set Itemid=@Itemid, ItemName=@ItemName, UnitMS=@UnitMS, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        Else
                            qry = "update tblStockItem set Itemid=@Itemid, ItemName=@ItemName, UnitMS=@UnitMS, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@Itemid", txtItemID.Text.ToUpper)
                            command.Parameters.AddWithValue("@ItemName", txtItemName.Text.ToUpper)
                            command.Parameters.AddWithValue("@UnitMS", txtUnitMS.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@Itemid", txtItemID.Text)
                            command.Parameters.AddWithValue("@ItemName", txtItemName.Text)
                            command.Parameters.AddWithValue("@UnitMS", txtUnitMS.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        'If txtExists.Text = "True" Then
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO ITEM CANNOT BE UPDATED"
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
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "STOCKITEM", txtItemID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "STOCKITEM", txtItemID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, "Error!!!", "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        '   MakeMeNull()
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

                command1.CommandText = "SELECT * FROM tblStockItem where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblStockItem where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '   MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                End If
                conn.Close()
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "STOCKITEM", txtItemID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        'Response.Redirect("RV_MasterStockItem.aspx")
    End Sub


    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblServiceRecordIssuedItem where ItemID=@ItemID"
        command1.Parameters.AddWithValue("@ItemID", txtItemID.Text)
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


    Protected Sub txtSearchTarget_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub txtSearchChemical_TextChanged(sender As Object, e As EventArgs) Handles txtSearchStockItem.TextChanged
        txtSearchText.Text = txtSearchStockItem.Text
        Dim qry As String
        qry = "select * from tblStockItem where 1=1"
        If String.IsNullOrEmpty(txtSearchStockItem.Text) = False Then

            qry = qry + " and (ItemID like '%" + txtSearchStockItem.Text + "%'"

            qry = qry + " or ItemName like '%" + txtSearchStockItem.Text + "%')"
        End If

        qry = qry + " order by ItemID;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchStockItem.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        txtSearchStockItem.Text = "Search Here"
    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        MakeMeNull()
        EnableControls()

        txt.Text = "SELECT * FROM tblStockItem  order by ItemID"
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        lblMessage.Text = ""
    End Sub



    Protected Sub btnGoCust_Click(sender As Object, e As EventArgs) Handles btnGoCust.Click
        txtSearchText.Text = txtSearchStockItem.Text

        If txtSearchStockItem.Text <> "Search Here" Then
            Dim qry As String
            qry = "select * from tblStock where 1=1"
            If String.IsNullOrEmpty(txtSearchStockItem.Text) = False Then

                qry = qry + " and (StockID like '%" + txtSearchStockItem.Text + "%'"

                qry = qry + " or Descrip1 like '%" + txtSearchStockItem.Text + "%')"
            End If

            qry = qry + " order by StockID;"
            txt.Text = qry
            MakeMeNull()
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            lblMessage.Text = "SEARCH CRITERIA : " + txtSearchStockItem.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString


        Else
            txtSearchStockItem.Text = "Search Here"
        End If

    End Sub
End Class
