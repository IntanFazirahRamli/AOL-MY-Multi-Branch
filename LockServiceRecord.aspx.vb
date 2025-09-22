Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class LockServiceRecord
    Inherits System.Web.UI.Page



    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        rdbServiceLock.SelectedIndex = 0
        rdbContractLock.SelectedIndex = 0
        'txtCreatedBy.Text = ""
        'txtCreatedOn.Text = ""
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
        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black
        txtSvcDateFrom.Enabled = False
        txtSvcDateTo.Enabled = False
        rdbServiceLock.Enabled = False
        rdbContractLock.Enabled = False
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

        txtSvcDateFrom.Enabled = True
        txtSvcDateTo.Enabled = True
        rdbServiceLock.Enabled = True
        rdbContractLock.Enabled = True
        '   AccessControl()

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
            txtSvcDateFrom.Text = ""
        Else

            txtSvcDateFrom.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtSvcDateTo.Text = ""
        Else
            txtSvcDateTo.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            rdbContractLock.SelectedValue = ""
        Else
            rdbContractLock.SelectedValue = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Then
            rdbServiceLock.SelectedValue = ""
        Else
            rdbServiceLock.SelectedValue = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
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

      
        EnableControls()
    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()



        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtSvcDateFrom.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT x0112,  x0112Add, x0112Edit, x0112Delete, x0112Print FROM tblgroupauthority2 where GroupAuthority = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0167,  x0167Add, x0167Edit, x0167Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0167")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0167"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0167")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If


                    If Not IsDBNull(dt.Rows(0)("x0167Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0167Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0167Add").ToString()
                        End If
                    End If


                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0167Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0167Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0167Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0167Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0167Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0167Delete").ToString()
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
            End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If txtSvcDateFrom.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Country cannot be blank!!!", "str")
                lblAlert.Text = "SERVICE DATE FROM CANNOT BE BLANK"
                Return

            End If
            If txtSvcDateTo.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Country cannot be blank!!!", "str")
                lblAlert.Text = "SERVICE DATE TO CANNOT BE BLANK"
                Return

            End If
            If txtMode.Text = "New" Then

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tbllockservicerecord where svcdatefrom=@svcdatefrom and svcdateto=@svcdateto"
                command1.Parameters.AddWithValue("@svcdatefrom", Convert.ToDateTime(txtSvcDateFrom.Text))
                command1.Parameters.AddWithValue("@svcdateto", Convert.ToDateTime(txtSvcDateTo.Text))

                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtSvcDateFrom.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tbllockservicerecord(svcdatefrom,svcdateto,ContractLock, svclock,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn) "
                    qry = qry + "VALUES(@svcdatefrom,@svcdateto,@ContractLock, @lock,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@svcdatefrom", Convert.ToDateTime(txtSvcDateFrom.Text))
                    command.Parameters.AddWithValue("@svcdateto", Convert.ToDateTime(txtSvcDateTo.Text))

                    If rdbContractLock.SelectedItem.Text = "Yes" Then
                        command.Parameters.AddWithValue("@ContractLock", "Y")
                    Else
                        command.Parameters.AddWithValue("@ContractLock", "N")
                    End If

                    If rdbServiceLock.SelectedItem.Text = "Yes" Then
                        command.Parameters.AddWithValue("@lock", "Y")
                    Else
                        command.Parameters.AddWithValue("@lock", "N")
                    End If
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    command.Connection = conn

                    command.ExecuteNonQuery()

                    txtRcno.Text = command.LastInsertedId


                    '''''''''''''''''''''''''''''''
                    'Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand

                    'commandUpdateServiceRecord.CommandType = CommandType.Text
                    'Dim qry1 As String = "Update tblservicerecord set LockSt = 'Y' where ServiceDate between @svcdatefrom and @svcdateto"

                    'commandUpdateServiceRecord.CommandText = qry1
                    'commandUpdateServiceRecord.Parameters.Clear()

                    'commandUpdateServiceRecord.Parameters.AddWithValue("@svcdatefrom", Convert.ToDateTime(txtSvcDateFrom.Text))
                    'commandUpdateServiceRecord.Parameters.AddWithValue("@svcdateto", Convert.ToDateTime(txtSvcDateTo.Text))

                    ''commandUpdateServiceRecord.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    ''commandUpdateServiceRecord.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    'commandUpdateServiceRecord.Connection = conn

                    'commandUpdateServiceRecord.ExecuteNonQuery()

                    '''''''''''''''''''''''''''''''''
                    '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                End If
                conn.Close()


                EnableControls()
                'txtMode.Text = ""
            ElseIf txtMode.Text = "Edit" Then
                If txtRcno.Text = "" Then
                    '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                    lblAlert.Text = "SELECT RECORD TO EDIT"
                    Return

                End If

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtcountry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tbllockservicerecord where svcdatefrom=@svcdatefrom and svcdateto=@svcdateto and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@svcdatefrom", Convert.ToDateTime(txtSvcDateFrom.Text))
                command2.Parameters.AddWithValue("@svcdateto", Convert.ToDateTime(txtSvcDateTo.Text))

                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Country already exists!!!", "str")
                    lblAlert.Text = "DATE RANGE ALREADY EXISTS"
                    txtSvcDateFrom.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tbllockservicerecord where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String

                        qry = "update tbllockservicerecord set svcdatefrom=@svcdatefrom,svcdateto=@svcdateto, ContractLock=@ContractLock, svclock=@lock, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)


                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@svcdatefrom", Convert.ToDateTime(txtSvcDateFrom.Text))
                        command.Parameters.AddWithValue("@svcdateto", Convert.ToDateTime(txtSvcDateTo.Text))

                        If rdbContractLock.SelectedItem.Text = "Yes" Then
                            command.Parameters.AddWithValue("@ContractLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@ContractLock", "N")
                        End If

                        If rdbServiceLock.SelectedItem.Text = "Yes" Then
                            command.Parameters.AddWithValue("@lock", "Y")
                        Else
                            command.Parameters.AddWithValue("@lock", "N")
                        End If
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))


                        command.Connection = conn

                        command.ExecuteNonQuery()

                        '''''''''''''''''''''''''''''''
                        'Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand

                        'commandUpdateServiceRecord.CommandType = CommandType.Text
                        ''Dim qry1 As String = "Update tblservicerecord set LockSt = @Lockst, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where ServiceDate between @svcdatefrom and @svcdateto"
                        'Dim qry1 As String = "Update tblservicerecord set LockSt = @Lockst where ServiceDate between @svcdatefrom and @svcdateto"

                        'commandUpdateServiceRecord.CommandText = qry1
                        'commandUpdateServiceRecord.Parameters.Clear()

                        'If rdbServiceLock.SelectedItem.Text = "Yes" Then
                        '    commandUpdateServiceRecord.Parameters.AddWithValue("@lockst", "Y")
                        'Else
                        '    commandUpdateServiceRecord.Parameters.AddWithValue("@lockst", "N")
                        'End If
                        'commandUpdateServiceRecord.Parameters.AddWithValue("@svcdatefrom", Convert.ToDateTime(txtSvcDateFrom.Text))
                        'commandUpdateServiceRecord.Parameters.AddWithValue("@svcdateto", Convert.ToDateTime(txtSvcDateTo.Text))
                        ''commandUpdateServiceRecord.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        ''commandUpdateServiceRecord.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'commandUpdateServiceRecord.Connection = conn

                        'commandUpdateServiceRecord.ExecuteNonQuery()

                        '''''''''''''''''''''''''''''''''

                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"

                        'If txtExists.Text = "True" Then
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so Country cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO COUNTRY CANNOT BE UPDATED"
                        '    lblAlert.Text = ""
                        'Else
                        '    '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        '    lblAlert.Text = ""
                        'End If
                    End If
                End If

                conn.Close()

            End If

            If txtMode.Text = "New" Then
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "LOCK", txtSvcDateFrom.Text & " TO " & txtSvcDateTo.Text, "ADD", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, "", "", txtRcno.Text)
            Else
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "LOCK", txtSvcDateFrom.Text & " TO " & txtSvcDateTo.Text, "EDIT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, "", "", txtRcno.Text)
            End If
            EnableControls()
            txtMode.Text = ""

            GridView1.DataSourceID = "SqlDataSource1"
            ' MakeMeNull()
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try
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
          

            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblLockservicerecord where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tbllockservicerecord where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                End If
                conn.Close()
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "LOCK", txtSvcDateFrom.Text & " TO " & txtSvcDateTo.Text, "DELETE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_Masterlocksvcrecord.aspx")



    End Sub

    Protected Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where  Module='LOCK' order by logdate desc"
        sqlDSViewEditHistory.DataBind()

        grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
        grdViewEditHistory.DataBind()

        mdlViewEditHistory.Show()

    End Sub

    Protected Sub grdViewEditHistory_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdViewEditHistory.PageIndexChanging
        grdViewEditHistory.PageIndex = e.NewPageIndex

        'sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where Module='LOCK' and  DocRef = '" & txtLogDocNo.Text & "' order by logdate desc"
        sqlDSViewEditHistory.SelectCommand = "Select * from tbllockservicerecord_log where " & txtLogDocNo.Text & "  order by LastModifiedOnLog desc"

        sqlDSViewEditHistory.DataBind()

        grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
        grdViewEditHistory.DataBind()

        mdlViewEditHistory.Show()

        grdViewEditHistory.DataBind()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            'GridView1.DataSourceID = "SqlDataSource1"
            'SqlDataSource2.SelectCommand = txtDetail.Text
            'SqlDataSource2.DataBind()
            'GridView2.DataBind()

        Catch ex As Exception
            'InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "GridView2_PageIndexChanging", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnEditHistory_Click(sender As Object, e As EventArgs)
        Try

            If txtMode.Text = "Add" Or txtMode.Text = "Edit" Or txtMode.Text = "Copy" Then
                lblAlert.Text = "RECORD IS IN ADD/EDIT MODE, CLICK SAVE OR CANCEL TO VIEW HISTORY"
                Return
            End If

            lblMessage.Text = ""
            lblAlert.Text = ""

            Dim btn1 As Button = DirectCast(sender, Button)

            Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
            Dim rowindex1 As Integer = xrow1.RowIndex


            Dim lblidRcno As String = TryCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text

            txtRcno.Text = lblidRcno

            GridView1.SelectedIndex = rowindex1

            Dim strRecordNo As String = GridView1.Rows(rowindex1).Cells(1).Text & " TO " & GridView1.Rows(rowindex1).Cells(2).Text
            'txtLogDocNo.Text = strRecordNo

            txtLogDocNo.Text = " 1=1 and svcDateFrom = '" & Convert.ToDateTime(GridView1.Rows(rowindex1).Cells(1).Text).ToString("yyyy-MM-dd") & "' and svcDateTo = '" & Convert.ToDateTime(GridView1.Rows(rowindex1).Cells(2).Text).ToString("yyyy-MM-dd") & "'"
            'sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where  DocRef = '" & strRecordNo & "' order by logdate desc"
            'sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where Module='LOCK' and  DocRef = '" & strRecordNo & "' order by logdate desc"

            sqlDSViewEditHistory.SelectCommand = "Select * from tbllockservicerecord_log where " & txtLogDocNo.Text & "  order by LastModifiedOnLog desc"

            sqlDSViewEditHistory.DataBind()

            grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
            grdViewEditHistory.DataBind()

            mdlViewEditHistory.Show()


        Catch ex As Exception
            InsertIntoTblWebEventLog("LOCKSERVICERECORD - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString)
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try

    End Sub

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn1 As New MySqlConnection()
            conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "AUTOEMAIL")
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn1.Open()
            insCmds.Connection = conn1
            insCmds.ExecuteNonQuery()
            conn1.Close()
            conn1.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, ID)
        End Try
    End Sub
End Class
