Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web

Imports System.IO
Imports System.Net
Imports System.Text
' Include this namespace if it is not already there

Imports System.Globalization
Imports System.Threading
Imports System.Drawing

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Reflection


Partial Class Master_Industry
    Inherits System.Web.UI.Page


    Public rcno As String
    Public HasDuplicateTarget As Boolean
    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtIndustry.Text = ""
        txtDescription.Text = ""
        ddlMarketSegmentID.SelectedIndex = 0
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
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black
        txtIndustry.Enabled = False
        txtDescription.Enabled = False
        ddlMarketSegmentID.Enabled = False
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

        txtIndustry.Enabled = True
        txtDescription.Enabled = True
        ddlMarketSegmentID.Enabled = True
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
            txtIndustry.Text = ""
        Else

            txtIndustry.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtDescription.Text = ""
        Else
            txtDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If

    

        If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Or String.IsNullOrEmpty(GridView1.SelectedRow.Cells(4).Text) = True Then
            ddlMarketSegmentID.SelectedIndex = 0
        Else
            ddlMarketSegmentID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString.Trim & " - " & Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString.Trim
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
        txtIndustry.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim Query As String

            'Query = "Select MarketSegmentID, Description from tblindustrysegment  "
            'PopulateDropDownList(Query, "MarketSegmentID", "Description", ddlMarketSegmentID)

            Query = "Select ContractGroup from tblContractGroup order by ContractGroup"

            PopulateDropDownList(Query, "ContractGroup", "ContractGroup", ddlContractGroup)

        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")

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

            'ddl.DataTextField = textField & " - " & valueField
            'ddl.DataValueField = textField & " - " & valueField
            ddl.DataBind()
            con.Close()
        End Using
        'End Using
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
                'command.CommandText = "SELECT X0104,  X0104Add, X0104Edit, X0104Delete, X0104Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT X0104,  X0104Add, X0104Edit, X0104Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()
                If dt.Rows.Count > 0 Then

                    If Not IsDBNull(dt.Rows(0)("x0104")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0104"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0104")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0104Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0104Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0104Add").ToString()
                        End If
                    End If

                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0104Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0104Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0104Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0104Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0104Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0104Delete").ToString()
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
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtIndustry.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
            lblAlert.Text = "INDUSTRY CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblindustry where industry=@ind"
                command1.Parameters.AddWithValue("@ind", txtIndustry.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtIndustry.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblindustry(Industry,Description, MarketSegmentID, MarketSegmentDescription, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@Industry,@Description,@MarketSegmentID, @MarketSegmentDescription, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@Industry", txtIndustry.Text.ToUpper)
                        command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper)
                        If ddlMarketSegmentID.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@MarketSegmentID", "")
                            command.Parameters.AddWithValue("@MarketSegmentDescription", "")
                        Else
                            command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                            command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
                        End If


                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@Industry", txtIndustry.Text)
                        command.Parameters.AddWithValue("@Description", txtDescription.Text)

                        If ddlMarketSegmentID.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@MarketSegmentID", "")
                            command.Parameters.AddWithValue("@MarketSegmentDescription", "")
                        Else
                            command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                            command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
                        End If

                        'command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                        'command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
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
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INDUSTRY", txtIndustry.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INDUSTRY", txtIndustry.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
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

                command2.CommandText = "SELECT * FROM tblindustry where industry=@ind and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@ind", txtIndustry.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Industry already exists!!!", "str")
                    lblAlert.Text = "INDUSTRY ALREADY EXISTS"
                    txtIndustry.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblindustry where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String

                        If txtExists.Text = "True" Then
                            qry = "update tblindustry set Description=@Description, MarketSegmentID =@MarketSegmentID, MarketSegmentDescription = @MarketSegmentDescription, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tblindustry set Industry=@Industry,Description=@Description, MarketSegmentID =@MarketSegmentID, MarketSegmentDescription = @MarketSegmentDescription, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        End If


                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@Industry", txtIndustry.Text.ToUpper)
                            command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper)

                            If ddlMarketSegmentID.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@MarketSegmentID", "")
                                command.Parameters.AddWithValue("@MarketSegmentDescription", "")
                            Else
                                command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                                command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
                            End If

                            'command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                            'command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@Industry", txtIndustry.Text)
                            command.Parameters.AddWithValue("@Description", txtDescription.Text)

                            If ddlMarketSegmentID.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@MarketSegmentID", "")
                                command.Parameters.AddWithValue("@MarketSegmentDescription", "")
                            Else
                                command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                                command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
                            End If

                            'command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                            'command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
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
            txtIndustry.Enabled = False
        Else
            txtIndustry.Enabled = True
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

                command1.CommandText = "SELECT * FROM tblindustry where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblindustry where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    ' MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INDUSTRY", txtIndustry.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterIndustry.aspx")



    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblcompany where industry=@data"
        command1.Parameters.AddWithValue("@data", txtIndustry.Text)
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

    'Start: 09.01.22

    Protected Sub btnRevenueDistribution_Click(sender As Object, e As EventArgs)



        Dim btn1 As Button = DirectCast(sender, Button)

        Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
        Dim rowindex1 As Integer = xrow1.RowIndex

        GridView1.SelectedIndex = rowindex1

        Dim strIndustry As String = GridView1.Rows(rowindex1).Cells(1).Text
        Dim strIndustryDescription As String = GridView1.Rows(rowindex1).Cells(2).Text

        'Dim row1 As GridViewRow = grvContractGroupDistributionDetails.SelectedRow
        lblIndustryDescription.Text = strIndustryDescription.Trim
        lblIndustry.Text = strIndustry.Trim
        UpdatePanel1.Update()

        'lblIndustry.Text = txtIndustry.Text
        btnCancelContractGroupDistribution.Enabled = True
        btnCancelContractGroupDistribution.ForeColor = Color.Black
        'Start: Target
        SetRowDataContractGroupDistribution()


        Dim dtScdrTarget1 As DataTable = CType(ViewState("CurrentTableCGDistribution"), DataTable)
        Dim drCurrentRowTarget1 As DataRow = Nothing

        For i As Integer = 0 To grvContractGroupDistributionDetails.Rows.Count - 1
            dtScdrTarget1.Rows.Remove(dtScdrTarget1.Rows(0))
            drCurrentRowTarget1 = dtScdrTarget1.NewRow()
            ViewState("CurrentTableCGDistribution") = dtScdrTarget1
            grvContractGroupDistributionDetails.DataSource = dtScdrTarget1
            grvContractGroupDistributionDetails.DataBind()

            SetPreviousDataContractGroupDistribution()

        Next i


        FirstGridViewRowContractGroupDistribution()

        'Dim conn As MySqlConnection = New MySqlConnection(constr)
        'conn.Open()

        Dim conn As MySqlConnection = New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim cmdContratTarget As MySqlCommand = New MySqlCommand
        cmdContratTarget.CommandType = CommandType.Text
        cmdContratTarget.CommandText = "SELECT Rcno, Industry, ContractGroupMain, ContractGroup, Percentage FROM tblIndustrycontractValueDistribution where Industry ='" & strIndustry.Trim & "'"
        cmdContratTarget.Connection = conn

        Dim drTarget As MySqlDataReader = cmdContratTarget.ExecuteReader()
        Dim dtTarget As New DataTable
        dtTarget.Load(drTarget)

        Dim TotDetailRecordsLoc = dtTarget.Rows.Count



        If dtTarget.Rows.Count > 0 Then
            txtNoofContractDistribution.Text = 1

            ddlContractGroup.Text = dtTarget.Rows(0)("ContractGroupMain").ToString()
            Dim rowIndex = 0

            For Each row As DataRow In dtTarget.Rows
                If (TotDetailRecordsLoc > (rowIndex + 1)) Then
                    AddNewRowContractGroupDistribution()
                End If

                Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                TextBoxTargetDesc.Text = Convert.ToString(dtTarget.Rows(rowIndex)("ContractGroup"))

                Dim TextBoxTargtId As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("txtPercContractGroupDistributionGV"), TextBox)
                TextBoxTargtId.Text = Convert.ToString(dtTarget.Rows(rowIndex)("Percentage"))

                Dim TextBoxContractNo As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("txtIndustryContractGroupDistributionGV"), TextBox)
                TextBoxContractNo.Text = Convert.ToString(dtTarget.Rows(rowIndex)("Industry"))


                Dim TextBoxRcno As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoContractGroupDistributionGV"), TextBox)
                TextBoxRcno.Text = Convert.ToString(dtTarget.Rows(rowIndex)("Rcno"))


                Dim Query As String
                Dim TextBoxTargetDesc2 As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                Query = "Select ContractGroup from tblContractGroup order by ContractGroup"
                PopulateDropDownList(Query, "ContractGroup", "ContractGroup", TextBoxTargetDesc2)
                rowIndex += 1

            Next row

            AddNewRowContractGroupDistribution()
            SetPreviousDataContractGroupDistribution()

        Else
            'FirstGridViewRowContractGroupDistribution()
            'Dim Query As String
            'Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(0).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
            'Query = "Select ContractGroup from tblContractGroup order by ContractGroup"

            'PopulateDropDownList(Query, "ContractGroup", "ContractGroup", TextBoxTargetDesc)

            'AddNewRowContractGroupDistribution()
            ''TextBoxTargetDesc.Text = ddlContractGrp.Text
            ''TextBoxTargetDesc.Enabled = False
            txtNoofContractDistribution.Text = 0
        End If

        'End: Target


        cmdContratTarget.Dispose()

        dtTarget.Dispose()

        'dtScdrTarget1.Dispose()


        CalculatePercentage()

        'If txtNoofContractDistribution.Text = 0 Then
        '    Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(0).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
        '    TextBoxTargetDesc.Text = ddlContractGrp.Text
        'End If

        'mdlContractGroupDistribution.Show()
    End Sub


    Protected Sub btnAddDetailContractGroupDistribution_Click(ByVal sender As Object, ByVal e As EventArgs)


        AddNewRowContractGroupDistribution()

    End Sub


    Protected Sub btnSaveContractGroupDistribution_Click(sender As Object, e As EventArgs) Handles btnSaveContractGroupDistribution.Click

        Try
            lblAlertContractGroupDistribution.Text = ""

            If Val(txtTotalPercent.Text) <> 100 Then
                lblAlertContractGroupDistribution.Text = "Total Percentage should be 100"
                mdlContractGroupDistribution.Show()
                Exit Sub
            End If
            SetRowDataContractGroupDistribution()

            Dim table As DataTable = TryCast(ViewState("CurrentTableCGDistribution"), DataTable)

            If table IsNot Nothing Then

                For rowIndex As Integer = 0 To table.Rows.Count - 1
                    'string txSpareId = row.ItemArray[0] as string;
                    Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                    Dim TextBoxTargtId As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(1).FindControl("txtPercContractGroupDistributionGV"), TextBox)


                    If (String.IsNullOrEmpty(TextBoxTargetDesc.Text) = False) And (TextBoxTargetDesc.Text <> "0") And (TextBoxTargetDesc.Text <> "-1") Then


                        'New
                        Dim TextBoxRcno As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoContractGroupDistributionGV"), TextBox)

                        If (String.IsNullOrEmpty(TextBoxRcno.Text) = True) Or (TextBoxRcno.Text = "0") Then


                            Dim conn As MySqlConnection = New MySqlConnection()
                            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                            conn.Open()

                            Dim command As MySqlCommand = New MySqlCommand
                            command.CommandType = CommandType.Text

                            Dim qry As String = "INSERT INTO tblIndustryContractValueDistribution(Industry, ContractGroupMain, ContractGroup, Percentage,"
                            qry = qry + "  CreatedBy, CreatedOn,"
                            qry = qry + " LastModifiedBy, LastModifiedOn)"

                            qry = qry + " VALUES(@Industry, @ContractGroupMain, @ContractGroup, @Percentage,"
                            qry = qry + "@CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn)"


                            command.CommandText = qry
                            command.Parameters.Clear()

                            command.Parameters.AddWithValue("@Industry", lblIndustry.Text.Trim)
                            command.Parameters.AddWithValue("@ContractGroupMain", ddlContractGroup.Text.Trim)
                            command.Parameters.AddWithValue("@ContractGroup", TextBoxTargetDesc.Text.Trim)
                            command.Parameters.AddWithValue("@Percentage", TextBoxTargtId.Text)


                            command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            'command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            command.Connection = conn
                            command.ExecuteNonQuery()

                        Else
                            'Dim TextBoxTargtId As TextBox = CType(grvTargetDetails.Rows(rowIndex).Cells(1).FindControl("txtTargtIdGV"), TextBox)

                            Dim conn As MySqlConnection = New MySqlConnection()
                            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                            conn.Open()

                            Dim command As MySqlCommand = New MySqlCommand
                            command.CommandType = CommandType.Text

                            Dim qry As String = "Update tblIndustryContractValueDistribution set ContractGroupMain = @ContractGroupMain, ContractGroup = @ContractGroup,  Percentage = @Percentage,"
                            qry = qry + " LastModifiedBy = @LastModifiedBy , LastModifiedOn = @LastModifiedOn where Rcno = @Rcno"


                            command.CommandText = qry
                            command.Parameters.Clear()
                            command.Parameters.AddWithValue("@ContractGroupMain", ddlContractGroup.Text.Trim)
                            command.Parameters.AddWithValue("@ContractGroup", TextBoxTargetDesc.Text.Trim)
                            command.Parameters.AddWithValue("@Percentage", TextBoxTargtId.Text)

                            command.Parameters.AddWithValue("@Rcno", TextBoxRcno.Text.Trim)
                            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))

                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            command.Connection = conn
                            command.ExecuteNonQuery()

                        End If

                        'If String.IsNullOrEmpty(txtTargetDesc.Text) = False Then
                        '    txtTargetDesc.Text = txtTargetDesc.Text + ", " + TextBoxTargetDesc.Text.Trim
                        '    txtTargetId.Text = txtTargetId.Text + ", " + TextBoxTargtId.Text.Trim
                        'Else
                        '    txtTargetDesc.Text = TextBoxTargetDesc.Text.Trim
                        '    txtTargetId.Text = TextBoxTargtId.Text.Trim
                        'End If
                    End If

                Next rowIndex


            End If


            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INDUSTRY", txtIndustry.Text, "btnSaveContractGroupDistribution", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, "", "", txtRcno.Text)

            CalculatePercentage()

            mdlContractGroupDistribution.Hide()

        Catch ex As Exception
            lblAlertContractGroupDistribution.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "btnSaveContractGroupDistribution_Click", ex.Message.ToString, txtIndustry.Text)
            mdlContractGroupDistribution.Show()
            Exit Sub
        End Try
    End Sub


    Protected Sub ddlContractGroupDistributionGV_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try


            lblMsgContractGroupDistribution.Text = ""
            lblAlertContractGroupDistribution.Text = ""

            'updPanelContract1.Update()

            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

            Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(xrow1.FindControl("ddlContractGroupDistributionGV"), DropDownList)
            'Dim lblid2 As TextBox = CType(xrow1.FindControl("txtTargtIdGV"), TextBox)


            'lTargetDesciption = lblid1.Text

            Dim rowindex1 As Integer = xrow1.RowIndex

           
            HasDuplicateTarget = HighlightDuplicateContractGroupDistribution(grvContractGroupDistributionDetails)

            txtDuplicateTarget.Text = "N"
            txtRecordAdded.Text = "N"

            If HasDuplicateTarget = True Then
                txtDuplicateTarget.Text = "Y"
                'Dim message As String = "alert('Duplicate Target/Pests has been selected!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                lblAlert.Text = "Duplicate Contract Group has been selected"
                mdlContractGroupDistribution.Show()
                Exit Sub
            End If


            If rowindex1 = grvContractGroupDistributionDetails.Rows.Count - 1 Then
                btnAddDetailContractGroupDistribution_Click(sender, e)
                txtRecordAdded.Text = "Y"
            End If
            'txtTargetDesc.Text = txtTargetDesc.Text & lblid1.Text & ", "
            'conn.Close()
            'conn.Dispose()
            'command1.Dispose()
            'dt.Dispose()
            mdlContractGroupDistribution.Show()
        Catch ex As Exception
            Throw ex
            InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "FUNCTION ddlContractGroupDistributionGV_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub



    Private Sub FirstGridViewRowContractGroupDistribution()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing
            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add(New DataColumn("ContractGroup", GetType(String)))
            dt.Columns.Add(New DataColumn("Percentage", GetType(String)))
            dt.Columns.Add(New DataColumn("Industry", GetType(String)))
            dt.Columns.Add(New DataColumn("Rcno", GetType(String)))

            dr = dt.NewRow()

            dr("ContractGroup") = String.Empty
            dr("Percentage") = 0
            dr("Industry") = String.Empty

            dr("Rcno") = 0
            dt.Rows.Add(dr)

            ViewState("CurrentTableCGDistribution") = dt

            grvContractGroupDistributionDetails.DataSource = dt
            grvContractGroupDistributionDetails.DataBind()

            Dim btnAdd As Button = CType(grvContractGroupDistributionDetails.FooterRow.Cells(1).FindControl("btnAddDetailContractGroupDistribution"), Button)
            Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            Throw ex
            InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "FUNCTION FirstGridViewRowContractGroupDistribution", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub AddNewRowContractGroupDistribution()
        Try
            Dim rowIndex As Integer = 0
            Dim Query As String

            If ViewState("CurrentTableCGDistribution") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableCGDistribution"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                        Dim TextBoxTargtId As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(1).FindControl("txtPercContractGroupDistributionGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(2).FindControl("txtIndustryContractGroupDistributionGV"), TextBox)

                        Dim TextBoxRcno As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoContractGroupDistributionGV"), TextBox)
                        drCurrentRow = dtCurrentTable.NewRow()

                        drCurrentRow("ContractGroup") = ""
                        drCurrentRow("Percentage") = 0.0
                        drCurrentRow("Industry") = txtIndustry.Text
                        drCurrentRow("Rcno") = 0

                        dtCurrentTable.Rows(i - 1)("ContractGroup") = TextBoxTargetDesc.SelectedValue
                        dtCurrentTable.Rows(i - 1)("Percentage") = TextBoxTargtId.Text
                        dtCurrentTable.Rows(i - 1)("Industry") = TextBoxContractNo.Text

                        dtCurrentTable.Rows(i - 1)("Rcno") = TextBoxRcno.Text
                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableCGDistribution") = dtCurrentTable

                    grvContractGroupDistributionDetails.DataSource = dtCurrentTable
                    grvContractGroupDistributionDetails.DataBind()

                    Dim rowIndex2 As Integer = 0
                    Dim j As Integer = 1
                    Do While j <= (rowIndex)

                        Dim TextBoxTargetDesc1 As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex2).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                        Query = "Select ContractGroup from tblContractGroup order by ContractGroup"
                        PopulateDropDownList(Query, "ContractGroup", "ContractGroup", TextBoxTargetDesc1)

                        If rowIndex2 = 0 Then
                            'TextBoxTargetDesc1.Text = ddlContractGroup.Text
                            TextBoxTargetDesc1.Enabled = False
                            UpdatePanel1.Update()
                        End If

                        'Dim i2 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(rowIndex2).Cells(0).FindControl("ddlSpareIdGV"), DropDownList), "Select SpareId, SpareDesc from spare where VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & " and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")) & " order by SpareDesc", "SpareDesc", "SpareId")
                        rowIndex2 += 1
                        j += 1
                    Loop

                    Dim TextBoxTargetDesc2 As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                    Query = "Select ContractGroup from tblContractGroup order by ContractGroup"
                    PopulateDropDownList(Query, "ContractGroup", "ContractGroup", TextBoxTargetDesc2)

                    dtCurrentTable.Dispose()
                End If
            Else
                Response.Write("ViewState is null")
            End If

            SetPreviousDataContractGroupDistribution()
        Catch ex As Exception
            Throw ex
            InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "FUNCTION AddNewRow", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub




    Private Sub SetPreviousDataContractGroupDistribution()
        Try
            Dim rowIndex As Integer = 0

            Dim Query As String
            'txtTargetDesc.Text = ""
            If ViewState("CurrentTableCGDistribution") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("CurrentTableCGDistribution"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                        Dim TextBoxTargtId As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(1).FindControl("txtPercContractGroupDistributionGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(2).FindControl("txtIndustryContractGroupDistributionGV"), TextBox)

                        Dim TextBoxRcno As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoContractGroupDistributionGV"), TextBox)

                        TextBoxTargetDesc.Text = dt.Rows(i)("ContractGroup").ToString()
                        TextBoxTargtId.Text = dt.Rows(i)("Percentage").ToString()
                        TextBoxContractNo.Text = dt.Rows(i)("Industry").ToString()

                        TextBoxRcno.Text = dt.Rows(i)("Rcno").ToString()

                        If (TextBoxTargtId.Text <> "0") And (TextBoxTargtId.Text <> "") And (String.IsNullOrEmpty(TextBoxTargtId.Text) = False) Then
                            Dim TextBoxTargetDesc2 As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(1).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                            Query = "Select ContractGroup from tblContractGroup order by ContractGroup"
                            PopulateDropDownList(Query, "ContractGroup", "ContractGroup", TextBoxTargetDesc2)

                            If rowIndex = 0 Then
                                TextBoxTargetDesc2.Text = ddlContractGroup.Text
                                TextBoxTargetDesc2.Enabled = False
                            End If
                        End If

                        rowIndex += 1
                    Next i
                End If
                dt.Dispose()
            End If
        Catch ex As Exception
            Throw ex
            Exit Sub
        End Try
    End Sub

    Private Sub SetRowDataContractGroupDistribution()
        Dim rowIndex As Integer = 0
        Try
            If ViewState("CurrentTableCGDistribution") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableCGDistribution"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count


                        Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                        Dim TextBoxTargtId As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(1).FindControl("txtPercContractGroupDistributionGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(2).FindControl("txtIndustryContractGroupDistributionGV"), TextBox)

                        Dim TextBoxRcno As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoContractGroupDistributionGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("ContractGroup") = TextBoxTargetDesc.Text
                        dtCurrentTable.Rows(i - 1)("Percentage") = TextBoxTargtId.Text
                        dtCurrentTable.Rows(i - 1)("Industry") = TextBoxContractNo.Text

                        dtCurrentTable.Rows(i - 1)("Rcno") = TextBoxRcno.Text

                        rowIndex += 1
                    Next i

                    ViewState("CurrentTableCGDistribution") = dtCurrentTable

                    dtCurrentTable.Dispose()
                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataContractGroupDistribution()
        Catch ex As Exception
            Throw ex
            Exit Sub
        End Try

    End Sub

    Public Function HighlightDuplicateContractGroupDistribution(ByVal gridview As GridView) As Boolean
        HasDuplicateTarget = False
        For currentRow As Integer = 0 To gridview.Rows.Count - 2
            Dim rowToCompare As GridViewRow = gridview.Rows(currentRow)

            For otherRow As Integer = currentRow + 1 To gridview.Rows.Count - 1
                Dim row As GridViewRow = gridview.Rows(otherRow)
                Dim duplicateRow As Boolean = False
                Dim TextBoxTargetDescGV As DropDownList = CType(grvContractGroupDistributionDetails.Rows(currentRow).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                Dim TextBoxTargetDescGV1 As DropDownList = CType(grvContractGroupDistributionDetails.Rows(otherRow).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)



                If ((TextBoxTargetDescGV.Text) = (TextBoxTargetDescGV1.Text)) Then

                    If TextBoxTargetDescGV.Text = "-1" And TextBoxTargetDescGV1.Text = "-1" Then
                        GoTo nextrec
                    End If

                    duplicateRow = True

                    TextBoxTargetDescGV.BackColor = Drawing.Color.LightCoral
                    TextBoxTargetDescGV1.BackColor = Drawing.Color.LightCoral

                   

                    HasDuplicateTarget = True
                    Return HasDuplicateTarget


                Else
                    duplicateRow = False
                    HasDuplicateTarget = False
                End If

nextrec:
            Next otherRow
        Next currentRow

        Return HasDuplicateTarget
    End Function

    Protected Sub grvContractGroupDistributionDetails_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Try
            grvContractGroupDistributionDetails.PageIndex = e.NewPageIndex
        Catch ex As Exception
            Throw ex
            Exit Sub
        End Try
    End Sub


    Protected Sub grvContractGroupDistributionDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grvContractGroupDistributionDetails.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                ' Delete

                For Each cell As DataControlFieldCell In e.Row.Cells
                    ' check all cells in one row
                    For Each control As Control In cell.Controls

                        Dim button As ImageButton = TryCast(control, ImageButton)
                        If button IsNot Nothing AndAlso button.CommandName = "Delete" Then
                            'button.OnClientClick = "if (!confirm('Are you sure to DELETE this record?')) return;"
                            button.OnClientClick = "Confirm()"
                        End If
                    Next control
                Next cell

            End If

        Catch ex As Exception
            Throw ex
            Exit Sub
        End Try
    End Sub


    Protected Sub grvContractGroupDistributionDetails_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles grvContractGroupDistributionDetails.RowDeleting
        Try

            If txtRecordDeleted.Text = "Y" Then
                txtRecordDeleted.Text = "N"
                Exit Sub
            End If

            lblAlert.Text = ""
            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then

                txtRecordDeleted.Text = "N"
                Dim Query As String
                SetRowDataContractGroupDistribution()

                Dim dt As DataTable = CType(ViewState("CurrentTableCGDistribution"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)



                Dim TextBoxRcno As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(4).FindControl("txtRcnoContractGroupDistributionGV"), TextBox)

                Dim TextBoxContractGroup As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)

                If TextBoxContractGroup.Text = ddlContractGroup.Text.Trim Then
                    lblAlertContractGroupDistribution.Text = "This Contract Group cannot be Deleted"
                    UpdatePanel1.Update()
                    mdlContractGroupDistribution.Show()
                    Exit Sub
                End If

                If (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
                    If (Convert.ToInt32(TextBoxRcno.Text) > 0) Then

                        'Dim conn As MySqlConnection = New MySqlConnection(constr)
                        'conn.Open()

                        Dim conn As MySqlConnection = New MySqlConnection()
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        conn.Open()

                        Dim commandUpdGS As MySqlCommand = New MySqlCommand
                        commandUpdGS.CommandType = CommandType.Text
                        commandUpdGS.CommandText = "Delete from tblindustrycontractvaluedistribution where rcno = " & TextBoxRcno.Text
                        commandUpdGS.Connection = conn
                        commandUpdGS.ExecuteNonQuery()
                        conn.Close()
                        conn.Dispose()
                        'UpdateTargetDescription()

                    End If
                End If

                If dt.Rows.Count > 1 Then
                    dt.Rows.Remove(dt.Rows(rowIndex))
                    drCurrentRow = dt.NewRow()
                    ViewState("CurrentTableCGDistribution") = dt
                    grvContractGroupDistributionDetails.DataSource = dt
                    grvContractGroupDistributionDetails.DataBind()

                    SetPreviousDataContractGroupDistribution()

                    Dim TextBoxTargetDesc2 As DropDownList = CType(grvContractGroupDistributionDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                    Query = "Select ContractGroup from tblContractGroup order by ContractGroup"
                    PopulateDropDownList(Query, "ContractGroup", "ContractGroup", TextBoxTargetDesc2)

                    ''''''''''''''''''''''''''
                    'HasDuplicateTarget = HighlightDuplicateContractGroupDistribution(grvContractGroupDistributionDetails)

                    txtDuplicateTarget.Text = "N"

                    If HasDuplicateTarget = True Then
                        txtDuplicateTarget.Text = "Y"
                        'Dim message As String = "alert('Duplicate Target/Pests has been selected!!!')"
                        'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                        lblAlertContractGroupDistribution.Text = "Duplicate Contract Group has been selected"
                        mdlContractGroupDistribution.Show()
                        Exit Sub
                    End If


                    If txtRecordAdded.Text = "N" Then
                        btnAddDetailContractGroupDistribution_Click(sender, e)
                    End If

                    txtRecordDeleted.Text = "Y"

                End If


                ' '''''''''''''''''''''''
                'Start:Target
                SetRowDataContractGroupDistribution()

                Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableCGDistribution"), DataTable)

                If tableAdd IsNot Nothing Then
                    'txtTargetDesc.Text = ""

                    For rowIndex1 As Integer = 0 To tableAdd.Rows.Count - 1
                        'string txSpareId = row.ItemArray[0] as string;
                        Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex1).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                        'Dim TextBoxTargetId As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex1).Cells(0).FindControl("txtPercContractGroupDistributionGV"), TextBox)

                        Dim TextBoxTargetDesc3 As DropDownList = CType(grvContractGroupDistributionDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                        Query = "Select ContractGroup from tblContractGroup order by ContractGroup"
                        PopulateDropDownList(Query, "ContractGroup", "ContractGroup", TextBoxTargetDesc3)


                    Next rowIndex1
                End If


                lblMsgContractGroupDistribution.Text = "DELETE : RECORD SUCCESSFULLY UPDATED"

                dt.Dispose()
                'CalculateTotalDistribution()
                CalculatePercentage()

                If Val(txtTotalPercent.Text) = 0 Or Val(txtTotalPercent.Text) = 100 Then
                    btnCancelContractGroupDistribution.Enabled = True
                    btnCancelContractGroupDistribution.ForeColor = Color.Black
                Else
                    btnCancelContractGroupDistribution.Enabled = False
                    btnCancelContractGroupDistribution.ForeColor = Color.Gray
                End If
                mdlContractGroupDistribution.Show()
            End If
        Catch ex As Exception
            Throw ex
            InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "FUNCTION grvContractGroupDistributionDetails_RowDeleting", ex.Message.ToString, "")
            Exit Sub
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
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub


    Protected Sub txtPercContractGroupDistributionGV_TextChanged(sender As Object, e As EventArgs)
        lblAlertContractGroupDistribution.Text = ""
        lblMsgContractGroupDistribution.Text = ""
        CalculatePercentage()

    End Sub

    Private Sub CalculatePercentage()
        'Start:Target
        SetRowDataContractGroupDistribution()

        Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableCGDistribution"), DataTable)

        If tableAdd IsNot Nothing Then
            'txtTargetDesc.Text = ""
            txtTotalPercent.Text = 0
            For rowIndex1 As Integer = 0 To tableAdd.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(rowIndex1).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
                Dim TextBoxTargetId As TextBox = CType(grvContractGroupDistributionDetails.Rows(rowIndex1).Cells(0).FindControl("txtPercContractGroupDistributionGV"), TextBox)

              
                txtTotalPercent.Text = Val(txtTotalPercent.Text) + Val(TextBoxTargetId.Text)


                'If rowIndex1 = 0 Then
                '    TextBoxTargetDesc.Enabled = False
                'End If

            Next rowIndex1
        End If
        txtTotalPercent.Text = Convert.ToDecimal(txtTotalPercent.Text).ToString("N2")

        UpdatePanel1.Update()

        mdlContractGroupDistribution.Show()
        'UpdatePanel1.Update()
    End Sub

    'Private Sub CalculateTotalDistribution()
    '    'Start: Contract Group %
    '    Dim conn As MySqlConnection = New MySqlConnection()
    '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    conn.Open()

    '    Dim commandContractGroupPerc As MySqlCommand = New MySqlCommand

    '    commandContractGroupPerc.CommandType = CommandType.Text
    '    commandContractGroupPerc.CommandText = "SELECT count(ContractGroup) as CGDetails FROM tblIndustryContractValueDistribution where ContractNo = '" & txtIndustry.Text & "'"
    '    commandContractGroupPerc.Connection = conn

    '    Dim drContractGroupPerc As MySqlDataReader = commandContractGroupPerc.ExecuteReader()
    '    Dim dtContractGroupPerc As New DataTable
    '    dtContractGroupPerc.Load(drContractGroupPerc)

    '    If dtContractGroupPerc.Rows.Count > 0 Then
    '        'btnContractGroupDistribution.Text = "DISTRIBUTION [" & Val(dtContractGroupPerc.Rows(0)("CGDetails").ToString).ToString & "]"
    '    End If

    '    conn.Close()
    '    conn.Dispose()

    '    commandContractGroupPerc.Dispose()
    '    dtContractGroupPerc.Dispose()
    '    drContractGroupPerc.Dispose()

    '    'End : Contract Multiple PO
    'End Sub

   
    'End: 09.01.22

    Protected Sub ddlContractGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContractGroup.SelectedIndexChanged
        lblAlertContractGroupDistribution.Text = ""
        If txtNoofContractDistribution.Text = 0 Then

            FirstGridViewRowContractGroupDistribution()

            AddNewRowContractGroupDistribution()

            Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(0).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)

            TextBoxTargetDesc.Text = ddlContractGroup.Text
            'mdlContractGroupDistribution.Show()


            'FirstGridViewRowContractGroupDistribution()
            'Dim Query As String
            'Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(0).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)
            'Query = "Select ContractGroup from tblContractGroup order by ContractGroup"

            'PopulateDropDownList(Query, "ContractGroup", "ContractGroup", TextBoxTargetDesc)

            'AddNewRowContractGroupDistribution()


            'mdlContractGroupDistribution.Show()
            ''txtNoofContractDistribution.Text = 0
        Else
            Dim TextBoxTargetDesc As DropDownList = CType(grvContractGroupDistributionDetails.Rows(0).Cells(0).FindControl("ddlContractGroupContractGroupDistributionGV"), DropDownList)

            TextBoxTargetDesc.Text = ddlContractGroup.Text
        End If
        mdlContractGroupDistribution.Show()

     
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Try

                Dim dt As DataTable = GetDataSet()
                WriteExcelWithNPOI(dt, "xlsx")
                Return

                Dim attachment As String = "attachment; filename=Service.xls"
                Response.ClearContent()
                Response.AddHeader("content-disposition", attachment)
                Response.ContentType = "application/vnd.ms-excel"
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
                Response.[End]()

                dt.Clear()


            ''  ExportGridToExcel()

            'If String.IsNullOrEmpty(txt.Text) = False Then

            '    Dim dt As DataTable = GetDataSet()
            '    Dim attachment As String = "attachment; filename=Service.xls"
            '    Response.ClearContent()
            '    Response.AddHeader("content-disposition", attachment)
            '    Response.ContentType = "application/vnd.ms-excel"
            '    Response.ContentEncoding = System.Text.Encoding.GetEncoding("Windows-1252")
            '    Dim tab As String = ""
            '    For Each dc As DataColumn In dt.Columns
            '        Response.Write(tab + dc.ColumnName)
            '        tab = vbTab
            '    Next
            '    Response.Write(vbLf)
            '    Dim i As Integer
            '    For Each dr As DataRow In dt.Rows
            '        tab = ""
            '        For i = 0 To dt.Columns.Count - 1
            '            Response.Write(tab & dr(i).ToString())
            '            tab = vbTab
            '        Next
            '        Response.Write(vbLf)
            '    Next

            '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERV", "", "btnExportToExcel_Click", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountID.Text, "", txtRcno.Text)
            '    Response.End()
            '    dt.Clear()


            '    'Response.ContentType = ContentType
            '    'Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            '    'Response.WriteFile(filePath)
            '    'Response.End()
            'Else
            '    lblAlert.Text = "NO DATA TO EXPORT"
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("INDUSTRY", "btnExportToExcel_Click", ex.Message.ToString, txtCreatedBy.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub


    Private Function GetDataSet() As DataTable
        Try
            Dim qry As String = ""



            Dim query As String = "SELECT Industry, Description, MarketSegmentID, MarketSegmentDescription, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn FROM tblindustry WHERE RCNO<>0;"
            'query = qry + query.Substring(query.IndexOf("where"))


            Dim dt As New DataTable()
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim cmd As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim sda As New MySqlDataAdapter()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = query

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
            InsertIntoTblWebEventLog("INDUSTRY", Session("UserID"), "FUNCTION GetDataSet", ex.Message.ToString)
            'InsertIntoTblWebEventLog("GetDataSet", ex.Message.ToString, txtReceiptNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Function
    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String)
        Dim workbook As IWorkbook

        If extension = "xlsx" Then
            workbook = New XSSFWorkbook()
        ElseIf extension = "xls" Then
            workbook = New HSSFWorkbook()
        Else
            Throw New Exception("This format is not supported")
        End If

        Dim sheet1 As ISheet = workbook.CreateSheet("Sheet1")

        'Add Selection Criteria

        Dim row1 As IRow = sheet1.CreateRow(0)
        Dim cell1 As ICell = row1.CreateCell(0)
        '  cell1.SetCellValue(Session("Selection").ToString)
        'cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + Session("Selection").ToString)
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ") - Industry Listing")

        cell1.CellStyle.WrapText = True
        Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8)
        sheet1.AddMergedRegion(cra)

        'Add Column Heading
        row1 = sheet1.CreateRow(1)

        Dim testeStyle As ICellStyle = workbook.CreateCellStyle()
        testeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

        testeStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index
        testeStyle.FillPattern = FillPattern.SolidForeground
        '  testeStyle.FillForegroundColor = IndexedColors.White.Index
        testeStyle.Alignment = HorizontalAlignment.Center

        Dim RowFont As IFont = workbook.CreateFont()
        RowFont.Color = IndexedColors.White.Index
        RowFont.IsBold = True

        testeStyle.SetFont(RowFont)

        For j As Integer = 0 To dt.Columns.Count - 1
            Dim cell As ICell = row1.CreateCell(j)
            ' InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

            Dim columnName As String = dt.Columns(j).ToString()
            cell.SetCellValue(columnName)
            ' cell.Row.RowStyle.FillBackgroundColor = IndexedColors.LightBlue.Index
            cell.CellStyle = testeStyle

        Next

        'Add details
        Dim _doubleCellStyle As ICellStyle = Nothing

        If _doubleCellStyle Is Nothing Then
            _doubleCellStyle = workbook.CreateCellStyle()
            _doubleCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
        End If

        Dim _intCellStyle As ICellStyle = Nothing

        If _intCellStyle Is Nothing Then
            _intCellStyle = workbook.CreateCellStyle()
            _intCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0")
        End If

        Dim dateCellStyle As ICellStyle = Nothing

        If dateCellStyle Is Nothing Then
            dateCellStyle = workbook.CreateCellStyle()
            dateCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-mm-yyyy")
        End If

        Dim AllCellStyle As ICellStyle = Nothing

        AllCellStyle = workbook.CreateCellStyle()
        AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim row As IRow = sheet1.CreateRow(i + 2)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row.CreateCell(j)

                If j = 5 Or j = 7 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                        cell.SetCellValue(d)
                    End If

                    cell.CellStyle = dateCellStyle
                Else
                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                    cell.CellStyle = AllCellStyle

                End If
                    If i = dt.Rows.Count - 1 Then
                        sheet1.AutoSizeColumn(j)
                    End If
            Next
        Next

        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = ""

            'attachment = "attachment; filename=PortfolioSummary" + txtCriteria.Text + "_By" + rbtnSelect.SelectedItem.Text
            attachment = "attachment; filename=Industry"

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "PortfolioSummary.xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub
End Class
