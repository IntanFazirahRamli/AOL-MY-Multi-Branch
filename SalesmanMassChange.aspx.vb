Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization
Imports System.Drawing

Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web

Imports System.IO
Imports System.Net
Imports System.Text
' Include this namespace if it is not already there

Imports System.Threading

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Reflection


Partial Class SalesmanMassChange
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Public rcno As String
    Public gridQuery As String
    Shared random As New Random()

  
    'Private Sub AccessControl()
    '    Try
    '        '''''''''''''''''''Access Control 
    '        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
    '            Dim conn As MySqlConnection = New MySqlConnection()
    '            Dim command As MySqlCommand = New MySqlCommand

    '            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '            conn.Open()

    '            command.CommandType = CommandType.Text
    '            'command.CommandText = "SELECT X0104,  X0104Add, X0104Edit, X0104Delete, X0104Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
    '            command.CommandText = "SELECT x0193 FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

    '            command.Connection = conn

    '            Dim dr As MySqlDataReader = command.ExecuteReader()
    '            Dim dt As New DataTable
    '            dt.Load(dr)
    '            conn.Close()
    '            If dt.Rows.Count > 0 Then

    '                If Not IsDBNull(dt.Rows(0)("x0169")) Then
    '                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0169"))) = False Then
    '                        If Convert.ToBoolean(dt.Rows(0)("x0169")) = False Then
    '                            Response.Redirect("Home.aspx")
    '                        End If
    '                    End If
    '                End If

    '                If Not IsDBNull(dt.Rows(0)("x0169Add")) Then
    '                    If String.IsNullOrEmpty(dt.Rows(0)("x0169Add")) = False Then
    '                        Me.btnADD.Enabled = dt.Rows(0)("x0169Add").ToString()
    '                    End If
    '                End If

    '                If txtMode.Text = "View" Then
    '                    If Not IsDBNull(dt.Rows(0)("x0169Edit")) Then
    '                        If String.IsNullOrEmpty(dt.Rows(0)("x0169Edit")) = False Then
    '                            Me.btnEdit.Enabled = dt.Rows(0)("x0169Edit").ToString()
    '                        End If
    '                    End If

    '                    If Not IsDBNull(dt.Rows(0)("x0169Delete")) Then
    '                        If String.IsNullOrEmpty(dt.Rows(0)("x0169Delete")) = False Then
    '                            Me.btnDelete.Enabled = dt.Rows(0)("x0169Delete").ToString()
    '                        End If
    '                    End If
    '                Else
    '                    Me.btnEdit.Enabled = False
    '                    Me.btnDelete.Enabled = False

    '                End If

    '                If btnADD.Enabled = True Then
    '                    btnADD.ForeColor = System.Drawing.Color.Black
    '                Else
    '                    btnADD.ForeColor = System.Drawing.Color.Gray
    '                End If


    '                If btnEdit.Enabled = True Then
    '                    btnEdit.ForeColor = System.Drawing.Color.Black
    '                Else
    '                    btnEdit.ForeColor = System.Drawing.Color.Gray
    '                End If

    '                If btnDelete.Enabled = True Then
    '                    btnDelete.ForeColor = System.Drawing.Color.Black
    '                Else
    '                    btnDelete.ForeColor = System.Drawing.Color.Gray
    '                End If


    '                If btnPrint.Enabled = True Then
    '                    btnPrint.ForeColor = System.Drawing.Color.Black
    '                Else
    '                    btnPrint.ForeColor = System.Drawing.Color.Gray
    '                End If


    '            End If
    '        End If

    '        '''''''''''''''''''Access Control 
    '    Catch ex As Exception
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        lblAlert.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Query As String = ""

            'Restrict users manual date entries
            'txtPercChange.Attributes.Add("readonly", "readonly")
            'txtEffectiveDate.Attributes.Add("readonly", "readonly")
            'txtClient.Attributes.Add("readonly", "readonly")
            'txtName.Attributes.Add("readonly", "readonly")
            'txtTeam.Attributes.Add("readonly", "readonly")
            'txtIncharge.Attributes.Add("readonly", "readonly")
            'txtServiceBy.Attributes.Add("readonly", "readonly")

            'txtTeamDetailsId.Attributes.Add("readonly", "readonly")
            'txtTeamDetailsIncharge.Attributes.Add("readonly", "readonly")
            'txtTeamDetailsServiceBy.Attributes.Add("readonly", "readonly")

            If Not Page.IsPostBack Then
                'mdlPopUpClient.Hide()
                'mdlPopUpTeam.Hide()
                MakeMeNull()

                lblAlert.Text = ""
                lblMessage.Text = ""

                Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN' ORDER BY STAFFID"
                PopulateDropDownList(Query, "StaffId", "StaffId", ddlOldSalesman)

                Query = "SELECT StaffId FROM tblstaff where roles= 'SALES MAN' and status ='O' ORDER BY STAFFID"
                PopulateDropDownList(Query, "StaffId", "StaffId", ddlNewSalesman)


                'chkUpdateServiceRecords.Checked = True

                ' '''
                'Dim conn As MySqlConnection = New MySqlConnection()
                'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'conn.Open()

                'Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
                'commandServiceRecordMasterSetup.CommandType = CommandType.Text
                ''commandServiceRecordMasterSetup.CommandText = "SELECT showSConScreenLoad, ServiceContractMaxRec,DisplayRecordsLocationWise, BackDateContract, BackDateContractTermination, ContractRevisionTerminationCode, PrefixDocNoContract, AutoRenewal FROM tblservicerecordmastersetup"
                'commandServiceRecordMasterSetup.CommandText = "SELECT showSConScreenLoad, ServiceContractMaxRec,DisplayRecordsLocationWise, ContractRevisionTerminationCode, PrefixDocNoContract, AutoRenewal, ContinuousContract, TeamIDMandatory, ServiceByMandatory, DefaultTaxCode, AllowTerminationBeforeLastService, PriceIncreaseLimit, PriceDecreaseLimit FROM tblservicerecordmastersetup"

                'commandServiceRecordMasterSetup.Connection = conn

                'Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
                'Dim dtServiceRecordMasterSetup As New DataTable
                'dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)


                ''txtPriceIncreaseLimit.Text = dtServiceRecordMasterSetup.Rows(0)("PriceIncreaseLimit")
                ''txtPriceDecreaseLimit.Text = dtServiceRecordMasterSetup.Rows(0)("PriceDecreaseLimit")

                'conn.Close()
                'conn.Dispose()
                'commandServiceRecordMasterSetup.Dispose()
                'drServiceRecordMasterSetup.Close()
                'dtServiceRecordMasterSetup.Dispose()

                mdlWarning.Hide()
                '''
            Else
                'If txtIsPopup.Text = "Team" Then
                '    txtIsPopup.Text = "N"
                '    mdlPopUpTeam.Show()
                'ElseIf txtIsPopup.Text = "Staff" Then
                '    txtIsPopup.Text = "N"
                '    mdlPopUpStaff.Show()
                'ElseIf txtIsPopup.Text = "Client" Then
                '    txtIsPopup.Text = "N"
                '    mdlPopUpClient.Show()
                'ElseIf txtIsPopup.Text = "Contract" Then
                '    txtIsPopup.Text = "N"
                '    'mdlPopUpContractNo.Show()
                'End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SALESMAN - " + Session("UserID"), "Page_Load", ex.Message.ToString, "")
            Exit Sub
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
            con.Dispose()
        End Using
        'End Using
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        MakeMeNull()
        'GridView1.DataSource = Nothing
        'GridView1.DataBind()
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'Me.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Close", "window.close()", True)
        Response.Redirect("Home.aspx")
    End Sub

    Public Sub MakeMeNull()
        'txtPercChange.Text = ""
        txtEffectiveDate.Text = ""
        'txtClient.Text = ""
        'txtName.Text = ""

        '  lblIncreaseDecrease.Text = ""
        txtConfirmationCode.Text = ""

        txtRcno.Text = ""
        txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""

        txtCorporateTotal.Text = "0"
        btnProcess.Enabled = False
        'GridView1.Enabled = False
    End Sub

    Private Sub EnableControls()
        btnGo.Enabled = False
        btnGo.ForeColor = System.Drawing.Color.Gray
        btnReset.Enabled = False
        btnReset.ForeColor = System.Drawing.Color.Gray
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

    End Sub

    Private Sub FindSalesman(ByVal modulename As String)
        Try
            Dim sql As String
            sql = ""

            'Company 

            If modulename = "Corporate" Then
                sql = "SELECT count(a.rcno) as totCorporate FROM tblCompanyLocation a, tblCompany b where a.Accountid=b.Accountid and a.Salesmansvc ='" & ddlOldSalesman.Text.Trim & "'"
            ElseIf modulename = "CorporateLocation" Then
                sql = "SELECT count(a.rcno) as totCorporateLocation FROM tblCompanyLocation a where a.Salesmansvc ='" & ddlOldSalesman.Text.Trim & "'"
            ElseIf modulename = "Residential" Then
                sql = "SELECT count(a.rcno) as totResidential FROM tblPersonLocation a, tblPerson b where a.Accountid=b.Accountid and a.Salesmansvc ='" & ddlOldSalesman.Text.Trim & "'"
            ElseIf modulename = "ResidentialLocation" Then
                sql = "SELECT count(a.rcno) as totResidentialLocation FROM tblPersonLocation a where a.Salesmansvc ='" & ddlOldSalesman.Text.Trim & "'"
            ElseIf modulename = "Contract" Then
                sql = "SELECT count(a.rcno) as totContract FROM tblContract a where a.Salesman ='" & ddlOldSalesman.Text.Trim & "'"
            ElseIf modulename = "Sales" Then
                sql = "SELECT count(a.rcno) as totInvoice FROM tblSales a where a.StaffCode ='" & ddlOldSalesman.Text.Trim & "'  and Salesdate >= '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "'"
            End If


            Dim commandSalesman1 As MySqlCommand = New MySqlCommand

            Dim connSalesman1 As MySqlConnection = New MySqlConnection()
            connSalesman1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connSalesman1.Open()

            commandSalesman1.CommandType = CommandType.Text
            commandSalesman1.CommandText = sql
            commandSalesman1.Connection = connSalesman1

            Dim drSalesman1 As MySqlDataReader = commandSalesman1.ExecuteReader()
            Dim dtSalesman1 As New DataTable
            dtSalesman1.Load(drSalesman1)

            If dtSalesman1.Rows.Count > 0 Then
                If modulename = "Corporate" Then
                    txtCorporateTotal.Text = dtSalesman1.Rows(0)("totCorporate").ToString
                ElseIf modulename = "CorporateLocation" Then
                    txtCorporateLocationTotal.Text = dtSalesman1.Rows(0)("totCorporateLocation").ToString
                ElseIf modulename = "Residential" Then
                    txtResidentialTotal.Text = dtSalesman1.Rows(0)("totResidential").ToString
                ElseIf modulename = "ResidentialLocation" Then
                    txtResidentialLocationTotal.Text = dtSalesman1.Rows(0)("totResidentialLocation").ToString
                ElseIf modulename = "Contract" Then
                    txtContractTotal.Text = dtSalesman1.Rows(0)("totContract").ToString
                ElseIf modulename = "Sales" Then
                    txtSalesInvoiceTotal.Text = dtSalesman1.Rows(0)("totInvoice").ToString
                End If

            End If

            updPanelMassChangeSalesman.Update()
            'commandSalesman1.Cancel()
            'commandSalesman1.Dispose()


            'dtSalesman1.Clear()
            'dtSalesman1.Dispose()
            'drSalesman1.Close()
            connSalesman1.Close()
            connSalesman1.Dispose()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("MASS SALESMAN - " + Session("UserID"), "FindSalesman", ex.Message.ToString, "")
            Exit Sub
        End Try

    End Sub




    Protected Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""

            UpdatePanel1.Update()

            'If GridView1.Rows.Count = 0 Then
            '    lblAlert.Text = "No Record to Process"
            '    Exit Sub
            'End If

            txtCreatedOn.Text = ""
            txtConfirmationCode.Text = ""

            gridQuery = ""
            Dim frmDate As String = ""
            Dim toDate As String = ""
            'Dim strdate As DateTime
            Dim allSearch As Boolean = False
            Dim multiSearch As Boolean = False

            If ddlOldSalesman.SelectedIndex = 0 Then
                lblAlert.Text = "Please Select Current Salesman"
                ddlOldSalesman.Focus()
                Exit Sub
            End If

            If ddlNewSalesman.SelectedIndex = 0 Then
                lblAlert.Text = "Please Select Current Salesman"
                ddlNewSalesman.Focus()
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) = True Then
                lblAlert.Text = "Please Enter Effective Date"
                txtEffectiveDate.Focus()
                Exit Sub
            End If

            txtCorporateTotal.Text = "0"
            txtCorporateLocationTotal.Text = "0"
            txtResidentialTotal.Text = "0"
            txtResidentialLocationTotal.Text = "0"
            txtContractTotal.Text = "0"
            txtSalesInvoiceTotal.Text = "0"


            FindSalesman("Corporate")
            FindSalesman("CorporateLocation")
            FindSalesman("Residential")
            FindSalesman("ResidentialLocation")
            FindSalesman("Contract")
            FindSalesman("Sales")


            'Dim sql As String
            'sql = ""

            ''Company 
            'sql = "SELECT count(a.rcno) as totCorporate FROM tblCompanyLocation a, tblCompany b where a.Accountid=b.Accountid and a.Salesmansvc ='" & ddlOldSalesman.Text.Trim & "'"

            'Dim commandSalesman1 As MySqlCommand = New MySqlCommand

            'Dim connSalesman1 As MySqlConnection = New MySqlConnection()
            'connSalesman1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'connSalesman1.Open()

            'commandSalesman1.CommandType = CommandType.Text
            'commandSalesman1.CommandText = sql
            'commandSalesman1.Connection = connSalesman1

            'Dim drSalesman1 As MySqlDataReader = commandSalesman1.ExecuteReader()
            'Dim dtSalesman1 As New DataTable
            'dtSalesman1.Load(drSalesman1)

            'If dtSalesman1.Rows.Count > 0 Then
            '    txtCorporateTotal.Text = dtSalesman1.Rows(0)("totCorporate").ToString
            'End If

            'updPanelMassChangeSalesman.Update()
            'commandSalesman1.Cancel()
            'commandSalesman1.Dispose()


            'dtSalesman1.Clear()
            'dtSalesman1.Dispose()
            'drSalesman1.Close()
            'connSalesman1.Close()
            'connSalesman1.Dispose()

            ''UpdatePanel1.Update()
            ''Company Location

            'sql = ""

            'sql = "SELECT count(a.rcno) as totCorporateLocation FROM tblCompanyLocation a where a.Salesmansvc ='" & ddlOldSalesman.Text.Trim & "'"

            'Dim commandSalesman2 As MySqlCommand = New MySqlCommand

            'Dim connSalesman2 As MySqlConnection = New MySqlConnection()
            'connSalesman2.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'connSalesman2.Open()

            'commandSalesman2.CommandType = CommandType.Text
            'commandSalesman2.CommandText = sql
            'commandSalesman2.Connection = connSalesman2

            'Dim drSalesman2 As MySqlDataReader = commandSalesman2.ExecuteReader()
            'Dim dtSalesman2 As New DataTable
            'dtSalesman2.Load(drSalesman2)

            'If dtSalesman2.Rows.Count > 0 Then
            '    txtCorporateLocationTotal.Text = dtSalesman2.Rows(0)("totCorporateLocation").ToString
            'End If

            'updPanelMassChangeSalesman.Update()
            'commandSalesman2.Cancel()
            'commandSalesman2.Dispose()


            'dtSalesman2.Clear()
            'dtSalesman2.Dispose()
            'drSalesman2.Close()
            'connSalesman2.Close()
            'connSalesman2.Dispose()
            ''updPanelMassChangeSalesman.Update()
            ' ''Residential

            'sql = ""

            'sql = "SELECT count(a.rcno) as totResidential FROM tblPersonLocation a, tblPerson b where a.Accountid=b.Accountid and a.Salesmansvc ='" & ddlOldSalesman.Text.Trim & "'"

            'Dim commandSalesman3 As MySqlCommand = New MySqlCommand

            'Dim connSalesman3 As MySqlConnection = New MySqlConnection()
            'connSalesman3.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'connSalesman3.Open()


            'commandSalesman3.CommandType = CommandType.Text
            'commandSalesman3.CommandText = sql
            'commandSalesman3.Connection = connSalesman3

            'Dim drSalesman3 As MySqlDataReader = commandSalesman3.ExecuteReader()
            'Dim dtSalesman3 As New DataTable
            'dtSalesman3.Load(drSalesman3)

            'If dtSalesman3.Rows.Count > 0 Then
            '    txtResidentialTotal.Text = dtSalesman3.Rows(0)("totResidential").ToString
            'End If
            'updPanelMassChangeSalesman.Update()
            'commandSalesman3.Cancel()
            'commandSalesman3.Dispose()

            'dtSalesman3.Clear()
            'dtSalesman3.Dispose()
            'drSalesman3.Close()
            'drSalesman3.Dispose()
            'connSalesman3.Close()
            'connSalesman3.Dispose()
            ''updPanelMassChangeSalesman.Update()
            ' ''Residential Location

            'sql = ""

            'sql = "SELECT count(a.rcno) as totResidentialLocation FROM tblPersonLocation a where a.Salesmansvc ='" & ddlOldSalesman.Text.Trim & "'"

            'Dim commandSalesman4 As MySqlCommand = New MySqlCommand

            'Dim connSalesman4 As MySqlConnection = New MySqlConnection()
            'connSalesman4.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'connSalesman4.Open()

            'commandSalesman4.CommandType = CommandType.Text
            'commandSalesman4.CommandText = sql
            'commandSalesman4.Connection = connSalesman4

            'Dim drSalesman4 As MySqlDataReader = commandSalesman4.ExecuteReader()
            'Dim dtSalesman4 As New DataTable
            'dtSalesman4.Load(drSalesman4)

            'If dtSalesman4.Rows.Count > 0 Then
            '    txtResidentialLocationTotal.Text = dtSalesman4.Rows(0)("totResidentialLocation").ToString
            'End If
            'updPanelMassChangeSalesman.Update()
            'commandSalesman4.Cancel()
            'commandSalesman4.Dispose()

            'dtSalesman4.Clear()
            'drSalesman4.Close()
            'dtSalesman4.Dispose()
            'connSalesman4.Close()
            'connSalesman4.Dispose()


            ''updPanelMassChangeSalesman.Update()
            ' ''Contract

            'sql = ""

            'sql = "SELECT count(a.rcno) as totContract FROM tblContract a where a.Salesman ='" & ddlOldSalesman.Text.Trim & "'"

            ''Dim commandSalesman As MySqlCommand = New MySqlCommand


            'Dim commandSalesman5 As MySqlCommand = New MySqlCommand
            'Dim connSalesman5 As MySqlConnection = New MySqlConnection()
            'connSalesman5.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'connSalesman5.Open()


            'commandSalesman5.CommandType = CommandType.Text
            'commandSalesman5.CommandText = sql
            'commandSalesman5.Connection = connSalesman5

            'Dim drSalesman5 As MySqlDataReader = commandSalesman5.ExecuteReader()
            'Dim dtSalesman5 As New DataTable
            'dtSalesman5.Load(drSalesman5)

            'If dtSalesman5.Rows.Count > 0 Then
            '    txtContractTotal.Text = dtSalesman5.Rows(0)("totContract").ToString
            'End If
            'updPanelMassChangeSalesman.Update()
            'commandSalesman5.Cancel()
            'commandSalesman5.Dispose()

            'dtSalesman5.Clear()

            'drSalesman5.Close()
            'dtSalesman5.Dispose()
            'connSalesman5.Close()
            'connSalesman5.Dispose()
            ''updPanelMassChangeSalesman.Update()
            ' ''Sales Invoice

            'sql = ""

            'sql = "SELECT count(a.rcno) as totInvoice FROM tblSales a where a.StaffCode ='" & ddlOldSalesman.Text.Trim & "'"

            ''Dim commandSalesman As MySqlCommand = New MySqlCommand

            'Dim commandSalesman6 As MySqlCommand = New MySqlCommand
            'Dim connSalesman6 As MySqlConnection = New MySqlConnection()
            'connSalesman6.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'connSalesman6.Open()

            'commandSalesman6.CommandType = CommandType.Text
            'commandSalesman6.CommandText = sql
            'commandSalesman6.Connection = connSalesman6

            'Dim drSalesman6 As MySqlDataReader = commandSalesman6.ExecuteReader()
            'Dim dtSalesman6 As New DataTable
            'dtSalesman6.Load(drSalesman6)

            'If dtSalesman6.Rows.Count > 0 Then
            '    txtSalesInvoiceTotal.Text = dtSalesman6.Rows(0)("totInvoice").ToString
            'End If

            'updPanelMassChangeSalesman.Update()
            'commandSalesman6.Cancel()
            'commandSalesman6.Dispose()

            'dtSalesman6.Clear()
            'dtSalesman6.Dispose()
            'drSalesman6.Close()
            'connSalesman6.Close()
            'connSalesman6.Dispose()
            ''updPanelMassChangeSalesman.Update()

            btnProcess.Enabled = True
            'GridView1.Enabled = True
            'chkUpdateServiceContract.Enabled = True
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("MASS SALESMAN - " + Session("UserID"), "btnGo_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)
        'Select All Check Boxes

        lblAlert.Text = ""
        lblMessage.Text = ""
        UpdatePanel1.Update()

        'For Each row As GridViewRow In GridView1.Rows
        '    Dim chkSelectCtrl As CheckBox = DirectCast(row.FindControl("chkGrid"), CheckBox)
        '    If chkSelectCtrl.Checked = False Then
        '        chkSelectCtrl.Checked = True
        '    Else
        '        chkSelectCtrl.Checked = False
        '    End If
        'Next
    End Sub



    Protected Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try

            'If txtProcessed.Text = "Y" Then
            '    txtProcessed.Text = "N"
            '    Exit Sub
            'End If

            lblAlert.Text = ""
            lblMessage.Text = ""
            lblWarningAlert.Text = ""
            UpdatePanel1.Update()

            lblRandom.Text = random.Next(100000, 900000)
            updPanelMassChangeSalesman.Update()
            'updPanelMassChange1.Update()
            'lblLine4EditAgreeValueSave.Text = txtRandom.Text
            'If chkUpdateServiceRecords.Checked = False Then
            mdlWarning.Show()
            'Else
            '    ProcessUpdate()
            'End If

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("MASS SALESMAN - " + Session("UserID"), "btnProcess_Click", ex.Message.ToString, "")
            Exit Sub
        End Try

    End Sub

    Private Sub ProcessUpdate()
        Try
            txtConfirmationCode.Text = ""
            Dim TotRecsSelected, RecsProcessed As Long
            TotRecsSelected = 0
            RecsProcessed = 0
            '            For Each row As GridViewRow In GridView1.Rows
            '                Dim chkbox As CheckBox = row.FindControl("chkGrid")

            '                If chkbox.Checked = True Then
            '                    TotRecsSelected = 1
            '                    GoTo IsRecords
            '                End If
            '            Next

            '            If TotRecsSelected = 0 Then
            '                lblAlert.Text = "NO RECORD IS SELECTED"
            '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            '                Exit Sub
            '            End If
            'IsRecords:


            '            If GridView1.Rows.Count = 0 Then
            '                lblAlert.Text = "NO RECORD TO PROCESS"
            '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            '                'txtProcessed.Text = "N"
            '                Exit Sub
            '            End If

            '            'If String.IsNullOrEmpty(txtPercChange.Text.Trim) = True Then
            '            '    lblAlert.Text = "Please Enter % Change"
            '            '    txtPercChange.Focus()
            '            '    Exit Sub
            '            'End If

            '            'If (txtPercChange.Text.Trim) = "0" Then
            '            '    lblAlert.Text = "% Change cannot be 0"
            '            '    txtPercChange.Focus()
            '            '    Exit Sub
            '            'End If

            '            If String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) = True Then
            '                lblAlert.Text = "Please Enter Effective Date"
            '                txtEffectiveDate.Focus()
            '                Exit Sub
            '            End If

            '            btnProcess.Enabled = False

            '            TotRecsSelected = 0

            '            For rowIndex As Integer = 0 To GridView1.Rows.Count - 1
            '                'Dim chkbox As CheckBox = row.FindControl("chkGrid")

            '                Dim TextBoxchkSelect As CheckBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("chkGrid"), CheckBox)

            '                If TextBoxchkSelect.Checked = True Then

            '                    Dim conn As MySqlConnection = New MySqlConnection()

            '                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '                    conn.Open()

            '                    Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand
            '                    'For rowIndex As Integer = 0 To GridView1.Rows.Count - 1

            '                    'Dim lblid13 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtAgreeValueGV"), TextBox)
            '                    'Dim lblid14 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtNewAgreeValueGV"), TextBox)
            '                    'Dim lblid27 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtRcnoContractNoGV"), TextBox)
            '                    'Dim lblid28 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)

            '                    Dim lblid1 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtAgreeValueGV"), TextBox)
            '                    Dim lblid2 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtNewAgreeValueGV"), TextBox)
            '                    Dim lblid3 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtRcnoContractNoGV"), TextBox)
            '                    Dim lblid4 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)

            '                    Dim lblid5 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtStatusGV"), TextBox)
            '                    Dim lblid6 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtContractGroupGV"), TextBox)
            '                    Dim lblid7 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
            '                    Dim lblid8 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtClientNameGV"), TextBox)

            '                    Dim lblid9 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtStartDateGV"), TextBox)
            '                    Dim lblid10 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtEndDateGV"), TextBox)
            '                    Dim lblid11 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtServiceAddressGV"), TextBox)
            '                    Dim lblid12 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtEndOfLastScheduleGV"), TextBox)
            '                    '''

            '                    Dim commandService As MySqlCommand = New MySqlCommand

            '                    commandService.CommandType = CommandType.Text
            '                    commandService.CommandText = "SELECT count(ContractNo) as totrec FROM tblContractPriceHistory where contractno ='" & lblid4.Text & "'"
            '                    commandService.Connection = conn

            '                    Dim drService As MySqlDataReader = commandService.ExecuteReader()
            '                    Dim dtService As New DataTable
            '                    dtService.Load(drService)

            '                    'txtTotServicerec.Text = "0"

            '                    If dtService.Rows(0)("totrec").ToString > 0 Then
            '                        Dim commandService1 As MySqlCommand = New MySqlCommand

            '                        commandService1.CommandType = CommandType.Text
            '                        commandService1.CommandText = "SELECT max(Date) as Maxdate FROM tblContractPriceHistory where contractno ='" & lblid4.Text & "'"
            '                        commandService1.Connection = conn

            '                        Dim drService1 As MySqlDataReader = commandService1.ExecuteReader()
            '                        Dim dtService1 As New DataTable
            '                        dtService1.Load(drService1)

            '                        'txtContractNo.Text = dtService1.Rows(0)("Maxdate").ToString
            '                        If Convert.ToDateTime(dtService1.Rows(0)("Maxdate").ToString).ToString("yyyy-MM-dd") > Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") Then

            '                            '''
            '                            Dim commandInsertIntoBatchContractPriceChangeFail As MySqlCommand = New MySqlCommand

            '                            commandInsertIntoBatchContractPriceChangeFail.CommandType = CommandType.Text
            '                            Dim qryfail As String = "INSERT INTO tblBatchContractPriceChange(ContractNo, Status, ContractGroup, AccountID, Name,StartDate,EndDate, ServiceAddress,CurrentAgreedValue,NewAgreedValue, PercentChange,EffectiveDate, StaffID,ProcessedOn,ProcessStatus,ErrorMessage)"
            '                            qryfail = qryfail + "VALUES(@ContractNo, @Status, @ContractGroup, @AccountID, @Name,@StartDate,@EndDate, @ServiceAddress,@CurrentAgreedValue,@NewAgreedValue, @PercentChange,@EffectiveDate, @StaffID,@ProcessedOn,@ProcessStatus,@ErrorMessage);"

            '                            commandInsertIntoBatchContractPriceChangeFail.CommandText = qryfail
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.Clear()

            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ContractNo", lblid4.Text.ToUpper)
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@Status", lblid5.Text.ToUpper)
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ContractGroup", lblid6.Text.ToUpper)
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@AccountID", lblid7.Text.ToUpper)
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@Name", lblid8.Text.ToUpper)
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(lblid9.Text).ToString("yyyy-MM-dd"))

            '                            If String.IsNullOrEmpty(lblid10.Text) = True Then
            '                                commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid12.Text).ToString("yyyy-MM-dd"))
            '                            Else
            '                                commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
            '                            End If
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ServiceAddress", lblid11.Text.ToUpper)
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@CurrentAgreedValue", Convert.ToDecimal(lblid1.Text))
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@NewAgreedValue", Convert.ToDecimal(lblid2.Text))
            '                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@PercentChange", txtPercChange.Text)
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))

            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@StaffID", Session("UserID").ToUpper)
            '                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessedOn", Convert.ToDateTime(txtCreatedOn.Text).ToString("yyyy-MM-dd"))

            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))


            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessStatus", "FAIL")
            '                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ErrorMessage", "Effective Date is earlier than the latest record in Contract Price History")

            '                            commandInsertIntoBatchContractPriceChangeFail.Connection = conn
            '                            commandInsertIntoBatchContractPriceChangeFail.ExecuteNonQuery()

            '                            ''
            '                            GoTo ProcessNextRec
            '                        End If
            '                    End If
            '                    ''


            '                    Dim command As MySqlCommand = New MySqlCommand

            '                    command.CommandType = CommandType.StoredProcedure
            '                    command.CommandText = "MassUpdateAgreeValue"
            '                    command.Parameters.Clear()

            '                    command.Parameters.AddWithValue("@pr_Rcno", Convert.ToInt32(lblid3.Text))
            '                    command.Parameters.AddWithValue("@pr_ContractNo", lblid4.Text)
            '                    command.Parameters.AddWithValue("@pr_OriginalAgreeValue", lblid1.Text)
            '                    command.Parameters.AddWithValue("@pr_NewAgreeValue", Convert.ToDecimal(lblid2.Text))

            '                    'command.Parameters.AddWithValue("@pr_percChange", txtPercChange.Text)

            '                    'If chkUpdateServiceRecords.Checked = True Then
            '                    '    command.Parameters.AddWithValue("@pr_UpdateServiceRecord", "Y")
            '                    'Else
            '                    '    command.Parameters.AddWithValue("@pr_UpdateServiceRecord", "N")
            '                    'End If

            '                    command.Parameters.AddWithValue("@pr_LastModifiedBy", Session("UserID"))
            '                    'command.Parameters.AddWithValue("@pr_LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '                    command.Parameters.AddWithValue("@pr_LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))


            '                    command.Parameters.AddWithValue("@pr_Date", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))
            '                    command.Parameters.AddWithValue("@pr_UpdateType", "MassUpdate")
            '                    command.Connection = conn
            '                    command.ExecuteScalar()

            '                    'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVCONT", txtContractNo.Text, "EDITAGREEVALUE", Convert.ToDateTime(txtCreatedOn.Text), txtAgreeVal.Text, txtAgreeValueToUpdate.Text, 0, txtAccountId.Text, "", txtRcno.Text)


            '                    Dim commandInsertIntoBatchContractPriceChange As MySqlCommand = New MySqlCommand

            '                    commandInsertIntoBatchContractPriceChange.CommandType = CommandType.Text
            '                    Dim qry As String = "INSERT INTO tblBatchContractPriceChange(ContractNo, Status, ContractGroup, AccountID, Name,StartDate,EndDate, ServiceAddress,CurrentAgreedValue,NewAgreedValue, PercentChange,EffectiveDate, StaffID,ProcessedOn,ProcessStatus,ErrorMessage)"
            '                    qry = qry + "VALUES(@ContractNo, @Status, @ContractGroup, @AccountID, @Name,@StartDate,@EndDate, @ServiceAddress,@CurrentAgreedValue,@NewAgreedValue, @PercentChange,@EffectiveDate, @StaffID,@ProcessedOn,@ProcessStatus,@ErrorMessage);"

            '                    commandInsertIntoBatchContractPriceChange.CommandText = qry
            '                    commandInsertIntoBatchContractPriceChange.Parameters.Clear()

            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ContractNo", lblid4.Text.ToUpper)
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@Status", lblid5.Text.ToUpper)
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ContractGroup", lblid6.Text.ToUpper)
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@AccountID", lblid7.Text.ToUpper)
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@Name", lblid8.Text.ToUpper)
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(lblid9.Text).ToString("yyyy-MM-dd"))

            '                    If String.IsNullOrEmpty(lblid10.Text) = True Then
            '                        commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid12.Text).ToString("yyyy-MM-dd"))
            '                    Else
            '                        commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
            '                    End If
            '                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ServiceAddress", lblid11.Text.ToUpper)
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@CurrentAgreedValue", Convert.ToDecimal(lblid1.Text))
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@NewAgreedValue", Convert.ToDecimal(lblid2.Text))
            '                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@PercentChange", txtPercChange.Text)
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))

            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@StaffID", Session("UserID").ToUpper)
            '                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessedOn", Convert.ToDateTime(txtCreatedOn.Text).ToString("yyyy-MM-dd"))

            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))

            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessStatus", "SUCCESS")
            '                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ErrorMessage", "")


            '                    commandInsertIntoBatchContractPriceChange.Connection = conn
            '                    commandInsertIntoBatchContractPriceChange.ExecuteNonQuery()
            '                    RecsProcessed = RecsProcessed + 1
            '                    'Next rowIndex

            '                    commandUpdateServiceRecord.Dispose()


            '                    'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONTRACTBATHPRICECHANGE", lblid4.Text.ToUpper, "EDITAGREEVALUE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToDecimal(lblid1.Text), Convert.ToDecimal(lblid2.Text), 0, lblid7.Text.ToUpper, "Processed Record No. : " & RecsProcessed & "; Effective Date : " & Convert.ToDateTime(txtEffectiveDate.Text).ToString("dd/MM/yyyy") & "; Percentage Change : " & txtPercChange.Text, txtRcno.Text)


            '                End If
            'ProcessNextRec:
            '                TotRecsSelected = TotRecsSelected + 1
            '            Next


            '            lblMessage.Text = RecsProcessed & " out of " & TotRecsSelected & " Records Sucessfully Updated"
            '            'UpdatePanel1.Update()

            '            btnProcess.Enabled = False
            '            GridView1.Enabled = False
            '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            '            'SqlDataSource1.SelectCommand = ""
            '            'SqlDataSource1.DataBind()

            '            'GridView1.DataSourceID = "SqlDataSource1"
            '            'GridView1.DataBind()


            '            'SqlDataSource1.SelectCommand = txt.Text
            '            'SqlDataSource1.DataBind()

            '            'GridView1.DataSourceID = "SqlDataSource1"
            '            'GridView1.DataBind()
            '            Exit Sub
        Catch ex As Exception
            'txtProcessed.Text = "N"
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("MASS SALESMAN - " + Session("UserID"), "btnProcess_Click", ex.Message.ToString, "")
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
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SALESMAN - " + Session("UserID"), "InsertIntoTblWebEventLog", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub




    Protected Sub txtEffectiveDate_TextChanged(sender As Object, e As EventArgs) Handles txtEffectiveDate.TextChanged
        Try
            ''Start:Check for Service Lock
            lblAlert.Text = ""

            'Dim sqlstr As String
            'sqlstr = ""

            'sqlstr = "SELECT svcLock FROM tbllockservicerecord where '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "' between svcdatefrom and svcdateto"

            'Dim commandLocked As MySqlCommand = New MySqlCommand

            'Dim connLocked As MySqlConnection = New MySqlConnection()
            'connLocked.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'connLocked.Open()

            'commandLocked.CommandType = CommandType.Text
            'commandLocked.CommandText = sqlstr
            'commandLocked.Connection = connLocked

            'Dim drLocked As MySqlDataReader = commandLocked.ExecuteReader()
            'Dim dtLocked As New DataTable
            'dtLocked.Load(drLocked)

            'If dtLocked.Rows.Count > 0 Then
            '    If dtLocked.Rows(0)("svcLock").ToString = "Y" Then
            '        lblAlert.Visible = True
            '        lblAlert.Text = "Service Period is Locked for this DATE"
            '        txtEffectiveDate.Text = ""
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '        Exit Sub
            '    End If
            'End If


            'Dim CurrentYear, currentmonth As String
            'CurrentYear = DateTime.Now.Year.ToString()
            'currentmonth = DateTime.Now.Month.ToString()

            'If Year(txtEffectiveDate.Text) = CurrentYear And Month(txtEffectiveDate.Text) < currentmonth Then
            '    lblAlert.Text = "Effective Date should be within current month "
            '    txtEffectiveDate.Text = ""
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If

            'If Year(txtEffectiveDate.Text) < CurrentYear Then
            '    lblAlert.Text = "Effective Date should be within current month "
            '    txtEffectiveDate.Text = ""
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If

            'If Year(txtEffectiveDate.Text) = CurrentYear And Month(txtEffectiveDate.Text) = currentmonth Then
            'Else
            '    lblAlert.Text = "Effective Date should be within current month "
            '    txtEffectiveDate.Text = ""
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If
            'connLocked.Close()
            'connLocked.Dispose()
            'commandLocked.Dispose()
            'drLocked.Close()


            'End: Check for Service Lock
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SALESMAN - " + Session("UserID"), "InsertIntoTblWebEventLog", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub btnEditSalesmanSaveYes_Click(sender As Object, e As EventArgs) Handles btnEditSalesmanSaveYes.Click
        Try

            If String.IsNullOrEmpty(txtConfirmationCode.Text.Trim) = True Then
                lblWarningAlert.Text = "Please Enter Confirmation Code"
                mdlWarning.Show()
                Exit Sub
            End If

            If txtConfirmationCode.Text.Trim <> lblRandom.Text.Trim Then
                lblWarningAlert.Text = "Confirmation Code does not match"
                mdlWarning.Show()
                Exit Sub
            End If
            Dim qry As String


            Dim commandCorporate As MySqlCommand = New MySqlCommand
            commandCorporate.CommandType = CommandType.Text
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            qry = ""
            qry = "UPDATE tblCompanyLocation SET Salesmansvc =@Salesman where Salesmansvc= '" & ddlOldSalesman.Text.Trim & "'"

            commandCorporate.CommandText = qry
            commandCorporate.Parameters.Clear()

            commandCorporate.Parameters.AddWithValue("@Salesman", ddlNewSalesman.Text.ToUpper)

            commandCorporate.Connection = conn
            commandCorporate.ExecuteNonQuery()

            commandCorporate.Dispose()


            Dim commandResidential As MySqlCommand = New MySqlCommand
            commandResidential.CommandType = CommandType.Text
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()
            qry = ""
            qry = "UPDATE tblPersonLocation SET Salesmansvc =@Salesman where Salesmansvc = '" & ddlOldSalesman.Text.Trim & "'"

            commandResidential.CommandText = qry
            commandResidential.Parameters.Clear()

            commandResidential.Parameters.AddWithValue("@Salesman", ddlNewSalesman.Text.ToUpper)

            commandResidential.Connection = conn
            commandResidential.ExecuteNonQuery()

            commandResidential.Dispose()


            Dim commandContract As MySqlCommand = New MySqlCommand
            commandContract.CommandType = CommandType.Text
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()
            qry = ""
            qry = "UPDATE tblcontract SET Salesman =@Salesman where Salesman= '" & ddlOldSalesman.Text.Trim & "'"

            commandContract.CommandText = qry
            commandContract.Parameters.Clear()

            commandContract.Parameters.AddWithValue("@Salesman", ddlNewSalesman.Text.ToUpper)

            commandContract.Connection = conn
            commandContract.ExecuteNonQuery()

            commandContract.Dispose()


            Dim commandSales As MySqlCommand = New MySqlCommand
            commandSales.CommandType = CommandType.Text
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            qry = ""
            qry = "UPDATE tblSales SET StaffCode =@Salesman where StaffCode= '" & ddlOldSalesman.Text.Trim & "' and Salesdate >= '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "'"

            commandSales.CommandText = qry
            commandSales.Parameters.Clear()

            commandSales.Parameters.AddWithValue("@Salesman", ddlNewSalesman.Text.ToUpper)

            commandSales.Connection = conn
            commandSales.ExecuteNonQuery()

            commandSales.Dispose()

            conn.Close()
            conn.Dispose()

            lblMessage.Text = "Salesman Updated Successfully"
        Catch ex As Exception
            'txtProcessed.Text = "N"
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("MASS SALESMAN - " + Session("UserID"), "btnEditAgreeValueSaveYes_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

   
End Class
