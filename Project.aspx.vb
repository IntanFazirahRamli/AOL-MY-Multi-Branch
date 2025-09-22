Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
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

Partial Class Project
    Inherits System.Web.UI.Page
    Public rcno As String
    Private Shared GridSelected As String = String.Empty
    Private Shared gScheduler, gSalesman As String
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Dim client As String
    'Public rcno As String

    Public TotDetailRecords As Integer
    Dim gFreqMTD As String
    Dim gSeq As String
    Dim gServiceDate As Date
    Dim gdayofWeek As String
    Dim gServiceDatestr As String

    Dim rowdeleted As Boolean
    Dim RowNumber As Integer
    Dim RowIndexSch As Integer

    Dim gDOW As String
    Dim gDayNo As Integer
    Dim gMonthNo As Integer
    Dim gWeekNo As Integer
    Dim mode As String
    Dim vStrStatus, vStrRenewalStatus, vStrNotedStatus As String

    Public HasDuplicateTarget As Boolean
    Public HasDuplicateLocaion As Boolean
    Public HasDuplicateServices As Boolean
    Public HasDuplicateFrequency As Boolean

    Public IsPopUpTeam As String


    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()
    End Sub


    Private Sub AccessControl()

        '''''''''''''''''''Access Control 
        Try
             ' '''''''''''''''''''Access Control 
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT x1207,  x1207Add, x1207Edit, x1207Delete,  x1207Status FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x1207"))) = False Then
                    If Convert.ToBoolean(dt.Rows(0)("x1207")) = False Then
                        Response.Redirect("Home.aspx")
                    End If
                End If



                If String.IsNullOrEmpty(dt.Rows(0)("x1207Add")) = False Then
                    Me.btnADD.Enabled = dt.Rows(0)("x1207Add").ToString()
                End If

                'Me.btnInsert.Enabled = vpSec2412Add

                If String.IsNullOrEmpty(dt.Rows(0)("x1207Edit")) = False Then
                    Me.btnEdit.Enabled = dt.Rows(0)("x1207Edit").ToString()
                End If

                If String.IsNullOrEmpty(dt.Rows(0)("x1207Delete")) = False Then
                    Me.btnDelete.Enabled = dt.Rows(0)("x1207Delete").ToString()
                End If

                'If String.IsNullOrEmpty(dt.Rows(0)("x1207Print")) = False Then
                '    Me.btnPrint.Enabled = dt.Rows(0)("x1207Print").ToString()
                'End If

                If String.IsNullOrEmpty(dt.Rows(0)("x1207Status")) = False Then
                    Me.BtnChSt.Enabled = dt.Rows(0)("x1207Status").ToString()
                End If


                If btnADD.Enabled = True Then
                    btnADD.ForeColor = System.Drawing.Color.Black
                Else
                    btnADD.ForeColor = System.Drawing.Color.Gray
                End If

                If btnCopy.Enabled = True Then
                    btnCopy.ForeColor = System.Drawing.Color.Black
                Else
                    btnCopy.ForeColor = System.Drawing.Color.Gray
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

                If BtnChSt.Enabled = True Then
                    BtnChSt.ForeColor = System.Drawing.Color.Black
                Else
                    BtnChSt.ForeColor = System.Drawing.Color.Gray
                End If



                If btnPrint.Enabled = True Then
                    btnPrint.ForeColor = System.Drawing.Color.Black
                Else
                    btnPrint.ForeColor = System.Drawing.Color.Gray
                End If

                If btnClaim.Enabled = True Then
                    btnClaim.ForeColor = System.Drawing.Color.Black
                Else
                    btnClaim.ForeColor = System.Drawing.Color.Gray
                End If
            End If

            ''''''''''''''''''''Access Control 
            ''''''''
        Catch ex As Exception
            'InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "FUNCTION ACCESS CONTROL", ex.Message.ToString, "")
            Exit Sub
        End Try
        '''''''''''''''''''Access Control 
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Query As String


        txtContractDate.Attributes.Add("readonly", "readonly")
        txtContractStart.Attributes.Add("readonly", "readonly")
        txtContractEnd.Attributes.Add("readonly", "readonly")
        txtServStart.Attributes.Add("readonly", "readonly")
        txtServEnd.Attributes.Add("readonly", "readonly")

        txtCreatedOn.Attributes.Add("readonly", "readonly")

        txtTotContVal.Attributes.Add("readonly", "readonly")

        txtActualEndChSt.Attributes.Add("readonly", "readonly")
        txtServStartDay.Attributes.Add("readonly", "readonly")
        TxtServEndDay.Attributes.Add("readonly", "readonly")
        txtRetentionValue.Attributes.Add("readonly", "readonly")
        txtBillingAddress.Attributes.Add("readonly", "readonly")
        txtServiceAddressCons.Attributes.Add("readonly", "readonly")
        txtAgreeVal.Attributes.Add("onchange", "getValuePerMonth()")
        txtConDetVal.Attributes.Add("onchange", "getNetAmount()")
        txtContractStart.Attributes.Add("onchange", "ValidateDatesContract()")
        txtContractEnd.Attributes.Add("onchange", "ValidateDatesContract()")
        txtServStart.Attributes.Add("onchange", "ValidateDatesService()")
        txtServEnd.Attributes.Add("onchange", "ValidateDatesService()")
        ddlContactType.Attributes.Add("onchange", "getContactType()")
        txtAccountId.Attributes.Add("onchange", "getContactType()")
        ddlStatusChSt.Attributes.Add("OnSelectedIndexChanged", "statuschange()")


        gvClient.Attributes.Add("onchange", " if (!confirm('Please click OK to change. Otherwise click CANCEL?')) return false;")

        If Not Page.IsPostBack Then
            mdlPopUpClient.Hide()
            MakeMeNull()
            ModalPopupExtender1.Hide()
            'tb1.Tabs(1).Visible = False


            '' '''''''''''''''''''Access Control 
            'Dim conn As MySqlConnection = New MySqlConnection()
            'Dim command As MySqlCommand = New MySqlCommand

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'command.CommandType = CommandType.Text
            'command.CommandText = "SELECT x1207,  x1207Add, x1207Edit, x1207Delete,  x1207Status FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            'command.Connection = conn

            'Dim dr As MySqlDataReader = command.ExecuteReader()
            'Dim dt As New DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then

            '    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x1207"))) = False Then
            '        If Convert.ToBoolean(dt.Rows(0)("x1207")) = False Then
            '            Response.Redirect("Home.aspx")
            '        End If
            '    End If



            '    If String.IsNullOrEmpty(dt.Rows(0)("x1207Add")) = False Then
            '        Me.btnADD.Enabled = dt.Rows(0)("x1207Add").ToString()
            '    End If

            '    'Me.btnInsert.Enabled = vpSec2412Add

            '    If String.IsNullOrEmpty(dt.Rows(0)("x1207Edit")) = False Then
            '        Me.btnEdit.Enabled = dt.Rows(0)("x1207Edit").ToString()
            '    End If

            '    If String.IsNullOrEmpty(dt.Rows(0)("x1207Delete")) = False Then
            '        Me.btnDelete.Enabled = dt.Rows(0)("x1207Delete").ToString()
            '    End If

            '    'If String.IsNullOrEmpty(dt.Rows(0)("x1207Print")) = False Then
            '    '    Me.btnPrint.Enabled = dt.Rows(0)("x1207Print").ToString()
            '    'End If

            '    If String.IsNullOrEmpty(dt.Rows(0)("x1207Status")) = False Then
            '        Me.BtnChSt.Enabled = dt.Rows(0)("x1207Status").ToString()
            '    End If


            '    If btnADD.Enabled = True Then
            '        btnADD.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnADD.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If btnCopy.Enabled = True Then
            '        btnCopy.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnCopy.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If btnEdit.Enabled = True Then
            '        btnEdit.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnEdit.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If btnDelete.Enabled = True Then
            '        btnDelete.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnDelete.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If BtnChSt.Enabled = True Then
            '        BtnChSt.ForeColor = System.Drawing.Color.Black
            '    Else
            '        BtnChSt.ForeColor = System.Drawing.Color.Gray
            '    End If



            '    If btnPrint.Enabled = True Then
            '        btnPrint.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnPrint.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If btnClaim.Enabled = True Then
            '        btnClaim.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnClaim.ForeColor = System.Drawing.Color.Gray
            '    End If
            'End If

            ''''''''''''''''''''Access Control 

            EnableSvcControls()
            IsPopUpTeam = "N"
            'DisableControls()
            tb1.ActiveTabIndex = 0
            ViewState("ClientCurrentAlphabet") = "A"
            GenerateClientAlphabets()
            gvClient.DataSourceID = "SqlDSClient"




            Query = "Select ContractGroup from tblcontractgroup"
            PopulateDropDownList(Query, "ContractGroup", "ContractGroup", ddlContractGrp)
            PopulateDropDownList(Query, "ContractGroup", "ContractGroup", ddlSearchContractGroup)


            Query = "Select LocationGroup from tbllocationgroup"
            PopulateDropDownList(Query, "LocationGroup", "LocationGroup", ddlLocateGrp)
            PopulateDropDownList(Query, "LocationGroup", "LocationGroup", ddlSearchLocationGroup)



            Query = "Select CompanyGroup from tblCompanyGroup"
            PopulateDropDownList(Query, "CompanyGroup", "CompanyGroup", ddlCompanyGrp)
            PopulateDropDownList(Query, "CompanyGroup", "CompanyGroup", ddlSearchCompanyGroup)

            Query = "Select StaffId from tblStaff where SecGroupAuthority <> 'GUEST' and Status = 'O'"
            'Query = "Select StaffId from tblStaff where SecGroupAuthority like  'SCHEDULER%' and Status = 'O'"
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlScheduler)
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchScheduler)

            Query = "Select StaffId from tblStaff where Roles = 'SALES MAN'"
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesman)
            PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)




            If String.IsNullOrEmpty(Session("accountid")) = True And String.IsNullOrEmpty(Session("contractno")) = True Then
                Session.Remove("contractfrom")
                Session.Remove("contractdetailfrom")
                Session.Remove("contractno")
                Session.Remove("serviceschedulefrom")

                Session.Remove("contractdate")
                Session.Remove("contracttype")
                Session.Remove("client")
                Session.Remove("custname")
                Session.Remove("contact")
                Session.Remove("servstart")
                Session.Remove("servend")
                Session.Remove("agreedvalue")
                Session.Remove("discamt")
                Session.Remove("status")
                Session.Remove("accountid")
                lblAccountID.Text = ""

                'Session("accountid") = txtAccountId.Text
                'btnADD_Click(sender, e)
            Else

                If String.IsNullOrEmpty(Session("contractfrom")) = False And String.IsNullOrEmpty(Session("servicefrom")) = True Then
                    btnADD_Click(sender, e)
                    txtclientid.Text = Session("accountid")

                    txtClient.Text = Session("clientid")
                    ddlContactType.Text = Session("contracttype")
                    lblAccountIdContact.Visible = True
                    lblAccountIdContact1.Text = Session("accountid")

                    btnQuit.Text = "BACK"

                    If String.IsNullOrEmpty(Session("locationid")) = False Then
                        lblAccountIdContactLocation.Visible = True
                        lblAccountIdContactLocation1.Text = Session("locationid")
                    End If

                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        txtContType1.Text = "CORPORATE"
                        txtContType2.Text = "COMPANY"
                        txtContType3.Text = "RESIDENTIAL"
                        txtContType4.Text = "PERSON"
                    Else
                        txtContType1.Text = "RESIDENTIAL"
                        txtContType2.Text = "PERSON"
                        txtContType3.Text = "CORPORATE"
                        txtContType4.Text = "COMPANY"
                    End If

                    ddlCompanyGrp.Text = Session("companygroup")
                    'txtclientid.Text = Session("clientid")
                    txtCustName.Text = Session("custname")
                    'txtContactPerson.Text = Session("contactperson")
                    'txtConPerMobile.Text = Session("conpermobile")
                    'txtAccCode.Text = Session("acctcode")
                    'txtTelephone.Text = Session("telephone")
                    'txtFax.Text = Session("fax")
                    txtPostal.Text = Session("offpostal")
                    txtOfficeAddress.Text = Session("sevaddress")
                    ddlLocateGrp.Text = Session("locategrp")
                    ddlSalesman.Text = Session("salesman")


                    txtAccountId.Text = Session("accountid")
                    txtCustName.Text = Session("custname")



                    txtClient.Enabled = False
                    ddlContactType.Enabled = False
                    ddlCompanyGrp.Enabled = False
                    txtCustName.Enabled = False
                    'txtAccCode.Enabled = False
                    txtPostal.Enabled = False
                    btnClient.Visible = False
                    ddlSalesman.Enabled = False
                    txtAccountId.Enabled = False

                    'txtAccountId.Enabled = True
                    'txtCustName.Enabled = True
                    'ddlSalesman.Enabled = True
                    'ddlCompanyGrp.Enabled = True
                    ''btnClient.Enabled = True
                    'btnClient.Visible = True


                    'txtOfficeAddress.Text = Session("offaddress1") + ", " + Session("offstreet") + ", " + Session("offbuilding")

                    If (String.IsNullOrEmpty(Session("offaddress1")) = True) Then
                        txtOfficeAddress.Text = ""
                    Else
                        txtOfficeAddress.Text = Session("offaddress1")
                    End If

                    If (String.IsNullOrEmpty(Session("offstreet")) = True) Then
                    Else
                        txtOfficeAddress.Text = txtOfficeAddress.Text + ", " + Session("offstreet")
                    End If

                    If (String.IsNullOrEmpty(Session("offbuilding")) = True) Then
                    Else
                        txtOfficeAddress.Text = txtOfficeAddress.Text + ", " + Session("offbuilding")
                    End If

                    txtcontractfrom.Text = Session("contractfrom")

                    If String.IsNullOrEmpty(lblAccountIdContactLocation1.Text) = False Then
                        GridView1.DataSourceID = "SQLDSContractClientIdLocation"
                        GridSelected = "SQLDSContractClientIdLocation"
                    Else
                        GridView1.DataSourceID = "SQLDSContractClientId"
                        GridSelected = "SQLDSContractClientId"
                    End If



                    Session.Remove("contractfrom")
                    Session.Remove("clientid")
                    'Session.Remove("contracttype")
                    Session.Remove("companygroup")
                    Session.Remove("clientid")
                    Session.Remove("custname")
                    Session.Remove("contactperson")
                    Session.Remove("conpermobile")
                    Session.Remove("acctcode")
                    Session.Remove("telephone")
                    Session.Remove("fax")
                    Session.Remove("postal")
                    Session.Remove("sevaddress")
                    Session.Remove("locategrp")
                    Session.Remove("salesman")
                    Session.Remove("accountid")



                    'GridView1.d = "ContractNo"
                ElseIf String.IsNullOrEmpty(Session("contractdetailfrom")) = False Then

                    '''''''''''''''''''
                    txtContractNo.Text = Session("contractno")
                    txtRcno.Text = Session("rcno")
                    txtcontractfrom.Text = Session("contractdetailfrom")

                    '''''''''''''''''''''''
                    If String.IsNullOrEmpty(Session("accountid")) = False Then
                        txtAccountIdSearch.Text = Session("accountid")
                    End If

                    If String.IsNullOrEmpty(Session("searchstatus")) = False Then
                        txtSearch1Status.Text = Session("searchstatus")
                    End If

                    If String.IsNullOrEmpty(Session("searchteam")) = False Then
                        txtProjectNameSearch.Text = Session("searchteam")
                    End If

                    If String.IsNullOrEmpty(Session("searchincharge")) = False Then
                        txtInchargeSearch.Text = Session("searchincharge")
                    End If

                    If String.IsNullOrEmpty(Session("searchaccountid")) = False Then
                        txtAccountIdSearch.Text = Session("searchaccountid")
                    End If

                    If String.IsNullOrEmpty(Session("searchclientname")) = False Then
                        txtClientNameSearch.Text = Session("searchclientname")
                    End If


                    '''''''''''''''''''''''

                    txt.Text = Session("gridsql")
                    SQLDSContract.SelectCommand = Session("gridsql")
                    GridView1.DataSourceID = "SqlDSContract"
                    GridView1.DataBind()
                    'GridView1.DataSourceID = "SqlDSContractNo"
                    'GridView1.DataSourceID = Session("gridsql")
                    GridView1_SelectedIndexChanged(New Object(), New EventArgs)

                    ''''''''''''''''''''''''''''
                    'txtContractNo.Text = Session("contractno")
                    'txtRcno.Text = Session("rcno")
                    'txtcontractfrom.Text = Session("contractdetailfrom")
                    'txtAccountIdSearch.Text = Session("accountid")
                    'GridView1.Visible = True
                    'GridView1.DataSourceID = "SqlDSContractNo"
                    'GridView1.DataSourceID = txt.Text
                    'GridView1_SelectedIndexChanged(New Object(), New EventArgs)

                    Session.Remove("contractdetailfrom")
                    Session.Remove("contractno")
                    Session.Remove("accountid")
                    Session.Remove("contractdate")
                    Session.Remove("contracttype")
                    Session.Remove("client")
                    Session.Remove("custname")
                    Session.Remove("contact")
                    Session.Remove("servstart")
                    Session.Remove("servend")
                    Session.Remove("agreedvalue")
                    Session.Remove("discamt")
                    Session.Remove("status")
                    'Session.Remove("accountid")
                    'Session("contractdetailfrom") = ""
                    'Session("contractno") = ""
                    'GridSelected = "SQLDSContractNo"
                    'GridSelected = "SQLDSContract"
                ElseIf String.IsNullOrEmpty(Session("serviceschedulefrom")) = False Then


                    txtContractNo.Text = Session("contractno")
                    txtRcno.Text = Session("rcno")
                    txtcontractfrom.Text = Session("contractdetailfrom")
                    'GridView1.DataSourceID = "SqlDSContract"


                    SQLDSContract.SelectCommand = Session("gridsql")
                    GridView1.DataSourceID = "SqlDSContract"
                    GridView1.DataBind()

                    'GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                    Session.Remove("contractdetailfrom")
                    Session.Remove("contractno")

                    Session.Remove("contractdate")
                    Session.Remove("contracttype")
                    Session.Remove("client")
                    Session.Remove("custname")
                    Session.Remove("contact")
                    Session.Remove("servstart")
                    Session.Remove("servend")
                    Session.Remove("agreedvalue")
                    Session.Remove("discamt")
                    Session.Remove("status")
                    Session.Remove("accountid")

                    ''Session("serviceschedulefrom") = ""
                    ''Session("contractno") = ""
                    'GridSelected = "SQLDSContractNo"
                    GridSelected = "SQLDSContractNo"


                    ''''''
                ElseIf String.IsNullOrEmpty(Session("servicefrom")) = False Then
                    'Session("servicefrom") = "contract"

                    If String.IsNullOrEmpty(Session("lblaccountid")) = False Then
                        lblAccountIdContact.Visible = True
                        lblAccountIdContact1.Text = Session("lblaccountid")

                        txtclientid.Text = Session("lblaccountid")
                        txtClient.Text = Session("clientid")
                        txtcontractfrom.Text = Session("contractfrom")


                        If String.IsNullOrEmpty(lblAccountIdContactLocation1.Text) = False Then
                            GridView1.DataSourceID = "SQLDSContractClientIdLocation"
                            GridSelected = "SQLDSContractClientIdLocation"
                        Else
                            GridView1.DataSourceID = "SQLDSContractClientId"
                            GridSelected = "SQLDSContractClientId"
                        End If

                        'GridView1.DataSourceID = "SQLDSContractClientId"
                        'GridSelected = "SQLDSContractClientId"

                        'ddlContactType.Text = Session("contracttype")
                        'lblAccountIdContact.Visible = True
                        'lblAccountIdContact1.Text = Session("accountid")

                        'GridView1.DataSourceID = "SQLDSContractClientId"
                        'GridSelected = "SQLDSContractClientId"
                        'Session("lblaccountid") = lblAccountIdContact1.Text
                    Else

                        If String.IsNullOrEmpty(Session("contractno")) = False Then

                            txtContractNo.Text = Session("contractno")
                            txtRcno.Text = Session("rcno")
                            txtcontractfrom.Text = Session("contractfrom")

                            '''''''''''''''''''''
                            'SQLDSContract.SelectCommand = ""
                            txt.Text = Session("gridsql")
                            SQLDSContract.SelectCommand = txt.Text
                            SQLDSContract.DataBind()

                            GridView1.DataSourceID = "SQLDSContract"
                            GridView1.DataBind()
                            GridView1.Visible = True
                            GridSelected = "SQLDSContract"

                            ''''''''''''''''''''''''

                            'SQLDSContract.SelectCommand = Session("gridsql")
                            'GridView1.DataSourceID = "SqlDSContract"
                            'GridView1.DataBind()
                            'GridView1.DataSourceID = "SqlDSContractNo"
                            'GridView1.DataSourceID = Session("gridsql")
                            GridView1_SelectedIndexChanged(New Object(), New EventArgs)




                        End If
                    End If
                    Session("servicefrom") = ""

                Else
                    Session.Remove("contractdetailfrom")
                    Session.Remove("contractno")
                    Session.Remove("serviceschedulefrom")
                    'Session("serviceschedulefrom") = ""
                    'Session("contractno") = ""
                    'Session("contractdetailfrom") = ""
                    btnADD_Click(sender, e)
                End If
            End If
            txt.Text = SQLDSContract.SelectCommand


        Else

            If txtIsPopup.Text = "Team" Then
                txtIsPopup.Text = "N"
                'mdlPopUpTeam.Show()
            ElseIf txtIsPopup.Text = "Client" Then
                txtIsPopup.Text = "N"
                mdlPopUpClient.Show()
            ElseIf txtIsPopup.Text = "Status" Then
                txtIsPopup.Text = "N"
                ModalPopupExtender5.Show()
            End If
        End If

        tb1.Visible = True

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ''Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        ''Dim objTextInfo As TextInfo = cultureInfo.TextInfo

        If txtMode.Text = "EDIT" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If
        EnableControls()

        txtIsPopup.Text = "N"
        txtMode.Text = "VIEW"
        Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
        MakeMeNull()
        MakeSearchNull()
        'btnSvcEdit.Enabled = False
        'btnSvcDelete.Enabled = False

        'btnSvcEdit.Enabled = False
        'btnSvcEdit.ForeColor = System.Drawing.Color.Gray
        'btnSvcDelete.Enabled = False
        'btnSvcDelete.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = True
        txtContractStart.ForeColor = Drawing.Color.Black
        txtServStart.ForeColor = Drawing.Color.Black

        'SASI - 17/04/2017 - COMMISSION 

        'MakeCommNull()
        'EnablecommControls()

        'SASI - END
        Dim editindex As Integer = GridView1.SelectedIndex

        If String.IsNullOrEmpty(Session("contractdetailfrom")) = False Then
            rcno = Session("rcno")
        ElseIf String.IsNullOrEmpty(Session("serviceschedulefrom")) = False Then
            rcno = Session("rcno")
        ElseIf String.IsNullOrEmpty(Session("servicefrom")) = False Then
            rcno = Session("rcno")
        Else
            rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        End If

        If String.IsNullOrEmpty(rcno) = True Then
            txtRcno.Text = "0"
        Else
            txtRcno.Text = rcno.ToString()
        End If

        '  ddlIndustry.SelectedIndex = -1
        '  ddlMarketSegmentID.SelectedIndex = -1
        'ddlServiceTypeID.SelectedIndex = -1
        'ddlCategoryID.SelectedIndex = -1
        'ddlPortfolioYN.SelectedIndex = -1


        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            Try
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

            Catch ex As MySql.Data.MySqlClient.MySqlException
                lblAlert.Text = ex.Message.ToString
            End Try
            Dim sql As String
            sql = ""
            sql = "Select AccountID, Status,  ContractDate, CustName, InChargeId, AgreeValue, ContractValue, StartDate, EndDate, ActualEnd, "
            sql = sql + "Scheduler, SalesMan, ContactType, CustCode, CustAddr, Duration, DurationMs,  "
            sql = sql + "Notes, Comments, "
            sql = sql + " YourReference, ServiceStart, ContractGroup,  "
            sql = sql + "ServiceEnd, TimeIn, TimeOut, ContractValue, PerServiceValue, Disc_Percent, DiscAmt, BillingFrequency, Support, TeamID, Postal,"
            sql = sql + "LocateGrp, ContactPersonMobile, OurReference, PrintingRemarks, AmtCompleted, AmtBalance, ValuePerMonth, BillingAmount, AllocatedSvcTime, Remarks,"
            sql = sql + "CompanyGroup, ScheduleType, "
            sql = sql + "ProjectNo, CustName, RetentionPerc, RetentionValue, RetentionClaimDate, PONo, ServiceAddress, BillAddress1, ProjectName, "
            sql = sql + "SiteAddress , SiteBuilding , SiteStreet, SiteAddCity , SiteAddState, SiteAddCountry, SiteAddPostal "
            sql = sql + "  FROM tblProject "
            sql = sql + "where rcno = " & Convert.ToInt64(txtRcno.Text)

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            'command1.CommandText = "SELECT * FROM tblcontract where rcno=" & Convert.ToInt64(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Try

            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString
            End Try

            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If dt.Rows(0)("AccountID").ToString <> "" Then : txtAccountId.Text = dt.Rows(0)("AccountID").ToString : End If

                txtOriginalAccountId.Text = txtAccountId.Text

                If dt.Rows(0)("Status").ToString <> "" Then : txtStatus.Text = dt.Rows(0)("Status").ToString.Trim() : End If
                'If dt.Rows(0)("RenewalSt").ToString <> "" Then : txtRs.Text = dt.Rows(0)("RenewalSt").ToString.ToUpper.Trim() : End If
                'If dt.Rows(0)("NotedSt").ToString <> "" Then : txtNS.Text = dt.Rows(0)("NotedSt").ToString : End If
                'If dt.Rows(0)("GSt").ToString <> "" Then : txtGS.Text = dt.Rows(0)("GSt").ToString : End If
                If dt.Rows(0)("ProjectNo").ToString <> "" Then : txtContractNo.Text = dt.Rows(0)("ProjectNo").ToString : End If
                txtContractNoSelected.Text = txtContractNo.Text

                If dt.Rows(0)("ContractDate").ToString <> "" Then : txtContractDate.Text = Convert.ToDateTime(dt.Rows(0)("ContractDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("CustName").ToString <> "" Then : txtCustName.Text = dt.Rows(0)("CustName").ToString : End If
                'If dt.Rows(0)("InChargeId").ToString <> "" Then : txtTeamIncharge.Text = dt.Rows(0)("InChargeId").ToString : End If
                If dt.Rows(0)("AgreeValue").ToString <> "" Then : txtAgreeVal.Text = Convert.ToDecimal(dt.Rows(0)("AgreeValue")).ToString("f2") : End If
                If dt.Rows(0)("ContractValue").ToString <> "" Then : txtTotContVal.Text = Convert.ToDecimal(dt.Rows(0)("ContractValue")).ToString("f2") : End If
                If dt.Rows(0)("StartDate").ToString <> "" Then : txtContractStart.Text = Convert.ToDateTime(dt.Rows(0)("StartDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("EndDate").ToString <> "" Then : txtContractEnd.Text = Convert.ToDateTime(dt.Rows(0)("EndDate")).ToString("dd/MM/yyyy") : End If

                'If dt.Rows(0)("ActualEnd").ToString <> "" Then : txtActualEnd.Text = Convert.ToDateTime(dt.Rows(0)("ActualEnd")).ToString("dd/MM/yyyy") : End If

                If dt.Rows(0)("Scheduler").ToString <> "" Then
                    'Dim gScheduler As String

                    gScheduler = dt.Rows(0)("Scheduler").ToString.ToUpper()

                    If ddlScheduler.Items.FindByValue(gScheduler) Is Nothing Then
                        ''If String.IsNullOrEmpty(ddlScheduler.Items.FindByValue(gScheduler) = True Then
                        ddlScheduler.Items.Add(gScheduler)
                        ddlScheduler.Text = gScheduler
                    Else
                        ddlScheduler.Text = dt.Rows(0)("Scheduler").ToString.Trim().ToUpper()
                    End If
                End If

                If dt.Rows(0)("SalesMan").ToString <> "" Then
                    'Dim gSalesman As String

                    gSalesman = dt.Rows(0)("SalesMan").ToString.ToUpper()

                    If ddlSalesman.Items.FindByValue(gSalesman) Is Nothing Then
                        ddlSalesman.Items.Add(gSalesman)
                        ddlSalesman.Text = gSalesman
                    Else
                        ddlSalesman.Text = dt.Rows(0)("SalesMan").ToString.Trim().ToUpper()
                    End If
                End If



                'If dt.Rows(0)("SalesMan").ToString <> "" Then : ddlSalesman.Text = dt.Rows(0)("SalesMan").ToString.ToUpper.Trim().ToUpper() : End If
                'If dt.Rows(0)("ProspectId").ToString <> "" Then : txtProspectId.Text = dt.Rows(0)("ProspectId").ToString : End If
                'If dt.Rows(0)("StaffId").ToString <> "" Then : txtStaffId.Text = dt.Rows(0)("StaffId").ToString : End If


                If dt.Rows(0)("ContactType").ToString <> "" Then
                    If dt.Rows(0)("ContactType").ToString = "CORPORATE" Or dt.Rows(0)("ContactType").ToString = "COMPANY" Then
                        ddlContactType.Text = "COMPANY"
                    End If
                    If dt.Rows(0)("ContactType").ToString = "RESIDENTIAL" Or dt.Rows(0)("ContactType").ToString = "PERSON" Then
                        ddlContactType.Text = "PERSON"
                    End If

                    'If dt.Rows(0)("ContactType").ToString = "CORPORATE" Or dt.Rows(0)("ContactType").ToString = "COMPANY" Then
                    '    ddlContactType.Text = "CORPORATE"
                    'End If
                    'If dt.Rows(0)("ContactType").ToString = "RESIDENTIAL" Or dt.Rows(0)("ContactType").ToString = "PERSON" Then
                    '    ddlContactType.Text = "RESIDENTIAL"
                    'End If
                End If
                If dt.Rows(0)("CustCode").ToString <> "" Then : txtClient.Text = dt.Rows(0)("CustCode").ToString : End If
                If dt.Rows(0)("CustAddr").ToString <> "" Then : txtOfficeAddress.Text = dt.Rows(0)("CustAddr").ToString : End If
                'If dt.Rows(0)("Contact").ToString <> "" Then : txtContactPerson.Text = dt.Rows(0)("Contact").ToString : End If
                'If dt.Rows(0)("Telephone").ToString <> "" Then : txtTelephone.Text = dt.Rows(0)("Telephone").ToString : End If
                'If dt.Rows(0)("Fax").ToString <> "" Then : txtFax.Text = dt.Rows(0)("Fax").ToString : End If
                'If dt.Rows(0)("Duration").ToString <> "" Then : txtDuration.Text = dt.Rows(0)("Duration").ToString : End If

                'If dt.Rows(0)("DurationMs").ToString <> "" Then
                '    If dt.Rows(0)("DurationMs").ToString = "Days" Then
                '        rbtLstDuration.SelectedIndex = 0
                '    ElseIf dt.Rows(0)("DurationMs").ToString = "Weeks" Then
                '        rbtLstDuration.SelectedIndex = 1
                '    ElseIf dt.Rows(0)("DurationMs").ToString = "Months" Then
                '        rbtLstDuration.SelectedIndex = 2
                '    ElseIf dt.Rows(0)("DurationMs").ToString = "Years" Then
                '        rbtLstDuration.SelectedIndex = 3
                '    End If
                'End If


                'If dt.Rows(0)("ServiceNo").ToString <> "" Then : txtServiceNo.Text = dt.Rows(0)("ServiceNo").ToString : End If
                'If dt.Rows(0)("ServiceBal").ToString <> "" Then : txtServiceBal.Text = dt.Rows(0)("ServiceBal").ToString : End If
                'If dt.Rows(0)("ServiceAmt").ToString <> "" Then : txtServiceAmt.Text = dt.Rows(0)("ServiceAmt").ToString : End If
                'If dt.Rows(0)("HourNo").ToString <> "" Then : txtHourNo.Text = dt.Rows(0)("HourNo").ToString : End If
                'If dt.Rows(0)("HourBal").ToString <> "" Then : txtHourBal.Text = dt.Rows(0)("HourBal").ToString : End If
                'If dt.Rows(0)("HourAmt").ToString <> "" Then : txtHourAmt.Text = dt.Rows(0)("HourAmt").ToString : End If
                'If dt.Rows(0)("CallNo").ToString <> "" Then : txtCallNo.Text = dt.Rows(0)("CallNo").ToString : End If
                'If dt.Rows(0)("CallBal").ToString <> "" Then : txtCallBal.Text = dt.Rows(0)("CallBal").ToString : End If
                'If dt.Rows(0)("CallAmt").ToString <> "" Then : txtCallAmt.Text = dt.Rows(0)("CallAmt").ToString : End If
                'If dt.Rows(0)("ResponseHours").ToString <> "" Then : txtResponse.Text = dt.Rows(0)("ResponseHours").ToString : End If
                'If dt.Rows(0)("CancelCharges").ToString <> "" Then : txtCancelCharges.Text = dt.Rows(0)("CancelCharges").ToString : End If
                'If dt.Rows(0)("CompensatePct").ToString <> "" Then : txtCompensatePct.Text = dt.Rows(0)("CompensatePct").ToString : End If
                'If dt.Rows(0)("CompensateMax").ToString <> "" Then : txtCompensateMax.Text = dt.Rows(0)("CompensateMax").ToString : End If
                If dt.Rows(0)("Notes").ToString <> "" Then : txtContractNotes.Text = dt.Rows(0)("Notes").ToString : End If
                'If dt.Rows(0)("Comments").ToString <> "" Then : txtServInst.Text = dt.Rows(0)("Comments").ToString : End If
                'If dt.Rows(0)("ActualStaff").ToString <> "" Then : txtActualStaff.Text = dt.Rows(0)("ActualStaff").ToString : End If
                'If dt.Rows(0)("ServiceNoActual").ToString <> "" Then : txtServiceNoActual.Text = dt.Rows(0)("ServiceNoActual").ToString : End If
                'If dt.Rows(0)("HourNoActual").ToString <> "" Then : txtHourNoActual.Text = dt.Rows(0)("HourNoActual").ToString : End If
                'If dt.Rows(0)("CallNoActual").ToString <> "" Then : txtCallNoActual.Text = dt.Rows(0)("CallNoActual").ToString : End If
                'If dt.Rows(0)("MinDuration").ToString <> "" Then : txtMinDuration.Text = dt.Rows(0)("MinDuration").ToString : End If
                'If dt.Rows(0)("OContractNo").ToString <> "" Then : txtOContract.Text = dt.Rows(0)("OContractNo").ToString : End If
                'If dt.Rows(0)("RenewalContractNo").ToString <> "" Then : txtRenewed.Text = dt.Rows(0)("RenewalContractNo").ToString : End If

                'If dt.Rows(0)("RenewalDate").ToString <> "" Then : txtRenewalDate.Text = Convert.ToDateTime(dt.Rows(0)("RenewalDate")).ToString("dd/MM/yyyy") : End If
                'If dt.Rows(0)("UnitNo").ToString <> "" Then : txtUnitNo.Text = dt.Rows(0)("UnitNo").ToString : End If
                'If dt.Rows(0)("UnitBal").ToString <> "" Then : txtUnitBal.Text = dt.Rows(0)("UnitBal").ToString : End If
                'If dt.Rows(0)("UnitAmt").ToString <> "" Then : txtUnitAmt.Text = dt.Rows(0)("UnitAmt").ToString : End If
                'If dt.Rows(0)("UnitNoActual").ToString <> "" Then : txtUnitNoActual.Text = dt.Rows(0)("UnitNoActual").ToString : End If
                'If dt.Rows(0)("NotedDate").ToString <> "" Then : txtNotedDate.Text = dt.Rows(0)("NotedDate").ToString : End If

                'If dt.Rows(0)("settle").ToString <> "" Then
                '    If dt.Rows(0)("settle").ToString = "UC" Then
                '        rbtnLSettle.SelectedIndex = 0
                '    ElseIf dt.Rows(0)("settle").ToString = "OC" Then
                '        rbtnLSettle.SelectedIndex = 1
                '    ElseIf dt.Rows(0)("settle").ToString = "CB" Then
                '        rbtnLSettle.SelectedIndex = 2
                '    End If
                'End If
                'If dt.Rows(0)("ActualServHrs").ToString <> "" Then : txtActualServHrs.Text = dt.Rows(0)("ActualServHrs").ToString : End If
                If dt.Rows(0)("YourReference").ToString <> "" Then : txtYourRef.Text = dt.Rows(0)("YourReference").ToString : End If
                If dt.Rows(0)("ContractGroup").ToString <> "" Then : ddlContractGrp.Text = dt.Rows(0)("ContractGroup").ToString.ToUpper : End If
                'If dt.Rows(0)("ServDayMethod").ToString <> "" Then : txtServDayMethod.Text = dt.Rows(0)("ServDayMethod").ToString : End If
                'If dt.Rows(0)("ServDay").ToString <> "" Then : txtServDay.Text = dt.Rows(0)("ServDay").ToString : End If
                If dt.Rows(0)("ServiceStart").ToString <> "" Then : txtServStart.Text = Convert.ToDateTime(dt.Rows(0)("ServiceStart")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("ServiceEnd").ToString <> "" Then : txtServEnd.Text = Convert.ToDateTime(dt.Rows(0)("ServiceEnd")).ToString("dd/MM/yyyy") : End If
                'If dt.Rows(0)("ServiceFrequence").ToString <> "" Then : txtServiceFrequence.Text = dt.Rows(0)("ServiceFrequence").ToString : End If
                'If dt.Rows(0)("TimeIn").ToString <> "" Then : txtServTimeIn.Text = dt.Rows(0)("TimeIn").ToString : End If
                'If dt.Rows(0)("TimeOut").ToString <> "" Then : txtServTimeOut.Text = dt.Rows(0)("TimeOut").ToString : End If
                'Convert.ToDateTime(Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString).ToString("dd/MM/yyyy")
                'If dt.Rows(0)("WarrantyStart").ToString <> "" Then : txtWarrStart.Text = Convert.ToDateTime(dt.Rows(0)("WarrantyStart")).ToString("dd/MM/yyyy") : End If
                'If dt.Rows(0)("WarrantyEnd").ToString <> "" Then : txtWarrEnd.Text = Convert.ToDateTime(dt.Rows(0)("WarrantyEnd")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("ContractValue").ToString <> "" Then : txtConDetVal.Text = Convert.ToDecimal(dt.Rows(0)("ContractValue")).ToString("f2") : End If
                'If dt.Rows(0)("PerServiceValue").ToString <> "" Then : txtPerServVal.Text = Convert.ToDecimal(dt.Rows(0)("PerServiceValue")).ToString("f2") : End If
                'If dt.Rows(0)("Disc_Percent").ToString <> "" Then : txtDisPercent.Text = Convert.ToDecimal(dt.Rows(0)("Disc_Percent")).ToString("f2") : End If
                'If dt.Rows(0)("DiscAmt").ToString <> "" Then : txtDisAmt.Text = Convert.ToDecimal(dt.Rows(0)("DiscAmt")).ToString("f2") : End If
                'If dt.Rows(0)("BillingFrequency").ToString <> "" Then : ddlBillingFreq.Text = dt.Rows(0)("BillingFrequency").ToString.ToUpper() : End If
                'If dt.Rows(0)("DayService1").ToString <> "" Then : txtDayService1.Text = dt.Rows(0)("DayService1").ToString : End If
                'If dt.Rows(0)("DayService2").ToString <> "" Then : txtDayService2.Text = dt.Rows(0)("DayService2").ToString : End If
                'If dt.Rows(0)("DayService3").ToString <> "" Then : txtDayService3.Text = dt.Rows(0)("DayService3").ToString : End If
                'If dt.Rows(0)("DayService4").ToString <> "" Then : txtDayService4.Text = dt.Rows(0)("DayService4").ToString : End If
                'If dt.Rows(0)("Support").ToString <> "" Then : txtServiceBy.Text = dt.Rows(0)("Support").ToString : End If
                'If dt.Rows(0)("Supervisor").ToString <> "" Then : txtSupervisor.Text = dt.Rows(0)("Supervisor").ToString : End If



                'If dt.Rows(0)("TeamID").ToString <> "" Then : txtTeam.Text = dt.Rows(0)("TeamID").ToString : End If
                If dt.Rows(0)("Postal").ToString <> "" Then : txtPostal.Text = dt.Rows(0)("Postal").ToString : End If
                If dt.Rows(0)("LocateGrp").ToString <> "" Then : ddlLocateGrp.Text = dt.Rows(0)("LocateGrp").ToString.ToUpper() : End If
                'If dt.Rows(0)("ContactPersonMobile").ToString <> "" Then : txtConPerMobile.Text = dt.Rows(0)("ContactPersonMobile").ToString : End If
                If dt.Rows(0)("OurReference").ToString <> "" Then : txtOurRef.Text = dt.Rows(0)("OurReference").ToString : End If
                'If dt.Rows(0)("ServiceDescription").ToString <> "" Then : txtServiceDescription.Text = dt.Rows(0)("ServiceDescription").ToString : End If
                'If dt.Rows(0)("PrintingRemarks").ToString <> "" Then : txtPrintBody.Text = dt.Rows(0)("PrintingRemarks").ToString : End If
                'If dt.Rows(0)("Rev").ToString <> "" Then : txtRev.Text = dt.Rows(0)("Rev").ToString : End If
                'If dt.Rows(0)("MainContractNo").ToString <> "" Then : txtMainContractNo.Text = dt.Rows(0)("MainContractNo").ToString : End If
                'If dt.Rows(0)("AmtCompleted").ToString <> "" Then : txtAmtCompleted.Text = dt.Rows(0)("AmtCompleted").ToString : End If
                'If dt.Rows(0)("AmtCompleted").ToString <> "" Then : txtServiceAmtActual.Text = dt.Rows(0)("AmtCompleted").ToString : End If

                'If dt.Rows(0)("AmtBalance").ToString <> "" Then : txtAmtBalance.Text = dt.Rows(0)("AmtBalance").ToString : End If
                'If dt.Rows(0)("AmtBalance").ToString <> "" Then : txtServiceAmtBal.Text = dt.Rows(0)("AmtBalance").ToString : End If

                'If dt.Rows(0)("ValuePerMonth").ToString <> "" Then : txtValPerMnth.Text = dt.Rows(0)("ValuePerMonth").ToString : End If
                'If dt.Rows(0)("BillingAmount").ToString <> "" Then : txtBillingAmount.Text = dt.Rows(0)("BillingAmount").ToString : End If

                'If dt.Rows(0)("AllocatedSvcTime").ToString <> "" Then : txtAllocTime.Text = dt.Rows(0)("AllocatedSvcTime").ToString : End If
                If dt.Rows(0)("Remarks").ToString <> "" Then : txtRemarks.Text = dt.Rows(0)("Remarks").ToString : End If
                'If dt.Rows(0)("QuotePrice").ToString <> "" Then : txtQuotePrice.Text = dt.Rows(0)("QuotePrice").ToString : End If
                'If dt.Rows(0)("QuoteUnitMS").ToString <> "" Then : txtQuoteUnit.Text = dt.Rows(0)("QuoteUnitMS").ToString : End If
                If dt.Rows(0)("CompanyGroup").ToString <> "" Then : ddlCompanyGrp.Text = dt.Rows(0)("CompanyGroup").ToString : End If
                'If dt.Rows(0)("SalesGRP").ToString <> "" Then : txtSalesGRP.Text = dt.Rows(0)("SalesGRP").ToString : End If
                'If dt.Rows(0)("GSTNos").ToString <> "" Then : txtGstNo.Text = dt.Rows(0)("GSTNos").ToString : End If

                'If dt.Rows(0)("ScheduleType").ToString <> "" Then
                '    ddlScheduleType.Text = dt.Rows(0)("ScheduleType").ToString
                'End If

                'SASI - 17/04/2017 - COMMISSION

                'If dt.Rows(0)("SalesCommissionPerc").ToString <> "" Then : txtSalesCommPercent.Text = Convert.ToDecimal(dt.Rows(0)("SalesCommissionPerc")).ToString("f2") : End If
                'If dt.Rows(0)("SalesCommissionAmt").ToString <> "" Then : txtSalesCommAmt.Text = Convert.ToDecimal(dt.Rows(0)("SalesCommissionAmt")).ToString("f2") : End If
                'If dt.Rows(0)("TechCommissionPerc").ToString <> "" Then : txtTechCommPercent.Text = Convert.ToDecimal(dt.Rows(0)("TechCommissionPerc")).ToString("f2") : End If
                'If dt.Rows(0)("TechCommissionAmt").ToString <> "" Then : txtTechCommAmt.Text = Convert.ToDecimal(dt.Rows(0)("TechCommissionAmt")).ToString("f2") : End If

                'SASI - END

                'SASI - Industry Segment 28/04/2017

                'If dt.Rows(0)("ServiceTypeID").ToString = "&nbsp;" Or String.IsNullOrEmpty(dt.Rows(0)("ServiceTypeID").ToString) = True Then
                '    ddlServiceTypeID.SelectedIndex = 0
                'Else
                '    ddlServiceTypeID.Text = (dt.Rows(0)("ServiceTypeID").ToString.Trim & " - " & dt.Rows(0)("ServiceTypeDescription").ToString.Trim)
                'End If

                'If dt.Rows(0)("ServiceTypeID").ToString <> "" Then : ddlServiceTypeID.Text = dt.Rows(0)("ServiceTypeID").ToString : End If
                'If dt.Rows(0)("ServiceTypeDescription").ToString <> "" Then : ddlServiceTypeID.SelectedValue = dt.Rows(0)("ServiceTypeDescription").ToString : End If
                'If dt.Rows(0)("CategoryID").ToString <> "" Then : ddlCategoryID.Text = dt.Rows(0)("CategoryID").ToString : End If
                'If dt.Rows(0)("PortfolioYN").ToString <> "" Then : ddlPortfolioYN.SelectedValue = dt.Rows(0)("PortfolioYN").ToString : End If
                'If dt.Rows(0)("PortfolioValue").ToString <> "" Then : txtPortfolioValue.Text = Convert.ToDecimal(dt.Rows(0)("PortfolioValue")).ToString("f2") : End If

                'If dt.Rows(0)("MarketSegmentID").ToString = "&nbsp;" Or String.IsNullOrEmpty(dt.Rows(0)("MarketSegmentID").ToString) = True Then
                '    ddlMarketSegmentID.Text = ""
                'Else
                '    ddlMarketSegmentID.Text = Server.HtmlDecode(dt.Rows(0)("MarketSegmentID").ToString)
                'End If
                'If dt.Rows(0)("MarketSegmentID").ToString <> "" Then : ddlMarketSegmentID.Text = dt.Rows(0)("MarketSegmentID").ToString : End If

                'If dt.Rows(0)("Industry").ToString = "&nbsp;" Or String.IsNullOrEmpty(dt.Rows(0)("Industry").ToString) = True Then
                '    ddlIndustry.Text = ""
                'Else
                '    ddlIndustry.Text = Server.HtmlDecode(dt.Rows(0)("Industry").ToString)
                'End If
                'If dt.Rows(0)("Industry").ToString <> "" Then : ddlIndustry.SelectedItem.Text = dt.Rows(0)("Industry").ToString : End If

                'SASI - End Industry Segment


                'If dt.Rows(0)("Warranty").ToString = "Y" Then
                '    ChkWithWarranty.Checked = True
                'Else
                '    ChkWithWarranty.Checked = False
                'End If

                'If ChkWithWarranty.Checked = True Then
                '    Command.Parameters.AddWithValue("@Warranty", "Y")
                'Else
                '    Command.Parameters.AddWithValue("@Warranty", "N")
                'End If

                'If dt.Rows(0)("WarrantyDuration").ToString <> "" Then : txtWarrDurtion.Text = dt.Rows(0)("WarrantyDuration").ToString : End If


                'If ChkRequireInspection.Checked = True Then
                '    Command.Parameters.AddWithValue("@RequireInspecton", "Y")
                'Else
                '    Command.Parameters.AddWithValue("@RequireInspecton", "N")
                'End If

                'If dt.Rows(0)("RequireInspecton").ToString = "Y" Then
                '    ChkRequireInspection.Checked = True
                'Else
                '    ChkRequireInspection.Checked = False
                'End If

                'If ddlInspectionFrequency.SelectedIndex = 0 Then
                '    Command.Parameters.AddWithValue("@InspectionFrequency", "")
                'Else
                '    Command.Parameters.AddWithValue("@InspectionFrequency", ddlInspectionFrequency.SelectedValue.ToString)
                'End If

                'If dt.Rows(0)("InspectionFrequency").ToString <> "" Then : ddlInspectionFrequency.Text = dt.Rows(0)("InspectionFrequency").ToString : End If

                'If TxtInspectionStart.Text.Trim = "" Then
                '    Command.Parameters.AddWithValue("@InspectionStart", DBNull.Value)
                'Else
                '    Command.Parameters.AddWithValue("@InspectionStart", Convert.ToDateTime(TxtInspectionStart.Text).ToString("yyyy-MM-dd"))

                'End If

                'If TxtInspectionEnd.Text.Trim = "" Then
                '    Command.Parameters.AddWithValue("@InspectionEnd", DBNull.Value)
                'Else
                '    Command.Parameters.AddWithValue("@InspectionEnd", Convert.ToDateTime(TxtInspectionEnd.Text).ToString("yyyy-MM-dd"))

                'End If

                'If dt.Rows(0)("InspectionStart").ToString <> "" Then : TxtInspectionStart.Text = Convert.ToDateTime(dt.Rows(0)("InspectionStart")).ToString("dd/MM/yyyy") : End If
                'If dt.Rows(0)("InspectionEnd").ToString <> "" Then : TxtInspectionEnd.Text = Convert.ToDateTime(dt.Rows(0)("InspectionEnd")).ToString("dd/MM/yyyy") : End If


                'If dt.Rows(0)("ComputationMethod").ToString <> "" Then
                '    If dt.Rows(0)("ComputationMethod").ToString = "Monthly" Then
                '        rbtComputationMethod.SelectedIndex = 0
                '    ElseIf dt.Rows(0)("ComputationMethod").ToString = "Fixed" Then
                '        rbtComputationMethod.SelectedIndex = 1

                '    End If
                'End If

                'If rbtComputationMethod.SelectedIndex = 0 Then
                '    Command.Parameters.AddWithValue("@ComputationMethod", "Monthly")
                'Else
                '    Command.Parameters.AddWithValue("@ComputationMethod", "Fixed")
                'End If


                If dt.Rows(0)("AccountId").ToString <> "" Then : lblAccountID.Text = dt.Rows(0)("AccountId").ToString : End If
                txtAccountIdSelection.Text = lblAccountID.Text
                If dt.Rows(0)("ProjectNo").ToString <> "" Then : lblContractNo.Text = dt.Rows(0)("ProjectNo").ToString : End If
                If dt.Rows(0)("CustName").ToString <> "" Then : lblName.Text = dt.Rows(0)("CustName").ToString : End If
                'If dt.Rows(0)("OfficeAddress").ToString <> "" Then : txtOfficeAddress.Text = dt.Rows(0)("OfficeAddress").ToString : End If

                'SASI - 17/04/2017 | Commission Tab

                'If dt.Rows(0)("AccountId").ToString <> "" Then : lblAccountID2.Text = dt.Rows(0)("AccountId").ToString : End If
                ''txtAccountIdSelection.Text = lblAccountID.Text
                ''If dt.Rows(0)("ContractNo").ToString <> "" Then : lblContractNo2.Text = dt.Rows(0)("ContractNo").ToString : End If
                'If dt.Rows(0)("CustName").ToString <> "" Then : lblAccountName.Text = dt.Rows(0)("CustName").ToString : End If


                'SASI - 17/04/2017 End

                'If dt.Rows(0)("AccountId").ToString <> "" Then : lblAccountID1.Text = dt.Rows(0)("AccountId").ToString : End If
                ''txtAccountIdSelection.Text = lblAccountID.Text
                ''If dt.Rows(0)("ContractNo").ToString <> "" Then : lblContractNo1.Text = dt.Rows(0)("ContractNo").ToString : End If
                'If dt.Rows(0)("CustName").ToString <> "" Then : lblName1.Text = dt.Rows(0)("CustName").ToString : End If


                'If dt.Rows(0)("TotalArea").ToString <> "" Then : txtTotalArea.Text = dt.Rows(0)("TotalArea").ToString : End If
                'If dt.Rows(0)("CompletedArea").ToString <> "" Then : txtCompletedArea.Text = dt.Rows(0)("CompletedArea").ToString : End If
                'If dt.Rows(0)("BalanceArea").ToString <> "" Then : txtBalanceArea.Text = dt.Rows(0)("BalanceArea").ToString : End If

                If dt.Rows(0)("RetentionPerc").ToString <> "" Then : txtRetentionPerc.Text = dt.Rows(0)("RetentionPerc").ToString : End If
                If dt.Rows(0)("RetentionValue").ToString <> "" Then : txtRetentionValue.Text = dt.Rows(0)("RetentionValue").ToString : End If
                If dt.Rows(0)("RetentionClaimDate").ToString <> "" Then : txtRetentionReleaseDate.Text = Convert.ToDateTime(dt.Rows(0)("RetentionClaimDate")).ToString("dd/MM/yyyy") : End If

                If dt.Rows(0)("PONo").ToString <> "" Then : txtPONo.Text = dt.Rows(0)("PONo").ToString : End If
                If dt.Rows(0)("ServiceAddress").ToString <> "" Then : txtServiceAddressCons.Text = dt.Rows(0)("ServiceAddress").ToString : End If

                If dt.Rows(0)("BillAddress1").ToString <> "" Then : txtBillingAddress.Text = dt.Rows(0)("BillAddress1").ToString : End If

                If dt.Rows(0)("ProjectName").ToString <> "" Then : txtProjectName.Text = dt.Rows(0)("ProjectName").ToString : End If

                If dt.Rows(0)("SiteAddress").ToString <> "" Then : txtSiteAddress.Text = dt.Rows(0)("SiteAddress").ToString : End If
                If dt.Rows(0)("SiteBuilding").ToString <> "" Then : txtSiteBuilding.Text = dt.Rows(0)("SiteBuilding").ToString : End If
                If dt.Rows(0)("SiteStreet").ToString <> "" Then : txtSiteStreet.Text = dt.Rows(0)("SiteStreet").ToString : End If
                If dt.Rows(0)("SiteAddCity").ToString <> "" Then : ddlSiteCity.Text = dt.Rows(0)("SiteAddCity").ToString : End If
                If dt.Rows(0)("SiteAddState").ToString <> "" Then : ddlSiteState.Text = dt.Rows(0)("SiteAddState").ToString : End If
                If dt.Rows(0)("SiteAddCountry").ToString <> "" Then : ddlSiteCountry.Text = dt.Rows(0)("SiteAddCountry").ToString : End If
                If dt.Rows(0)("SiteAddPostal").ToString <> "" Then : txtSitePostal.Text = dt.Rows(0)("SiteAddPostal").ToString : End If

                'SiteAddress , SiteBuilding , SiteStreet, SiteAddCity , SiteAddState, SiteAddCountry, SiteAddPostal

                'If txtStatus.Text = "T" Then
                '    If dt.Rows(0)("TerminationCode").ToString <> "" Then : ddlTerminationCode.Text = dt.Rows(0)("TerminationDescription").ToString + "-" + dt.Rows(0)("TerminationCode").ToString : End If
                'End If



                'If Convert.ToDecimal(txtRetentionPerc.Text) = 0.0 Then
                '    chkGenerateCreditNote.Visible = True
                '    chkGenerateCreditNote.Checked = False
                'End If

                'txtWarrEnd.Text = Convert.ToDateTime(dt.Rows(0)("WarrantyEnd")).ToString("dd/MM/yyyy") : End If
                'Command.Parameters.AddWithValue("@RetentionPerc", txtRetentionPerc.Text)
                'Command.Parameters.AddWithValue("@RetentionValue", txtRetentionValue.Text)
                'If txtRetentionReleaseDate.Text.Trim = "" Then
                '    Command.Parameters.AddWithValue("@RetentionClaimDate", DBNull.Value)
                'Else
                '    Command.Parameters.AddWithValue("@RetentionClaimDate", Convert.ToDateTime(txtRetentionReleaseDate.Text).ToString("yyyy-MM-dd"))
                'End If





                tb1.ActiveTabIndex = 0
            End If

            ''Start:Retrive Service Records

            'Dim commandService As MySqlCommand = New MySqlCommand

            'commandService.CommandType = CommandType.Text
            'commandService.CommandText = "SELECT count(*) as totservicerec FROM tblservicerecord where contractno ='" & txtContractNo.Text & "'"
            'commandService.Connection = conn

            'Dim drService As MySqlDataReader = commandService.ExecuteReader()
            'Dim dtService As New DataTable
            'dtService.Load(drService)

            'If dtService.Rows.Count > 0 Then
            '    btnServiceRecords.Text = "SERVICE [" + Val(dtService.Rows(0)("totservicerec").ToString).ToString + "]"
            'End If

            'End:Retrieve Service Records
            'conn.Close()

            txtServStartDay.Text = DateTime.Parse(txtServStart.Text).DayOfWeek.ToString().ToUpper
            TxtServEndDay.Text = DateTime.Parse(txtServEnd.Text).DayOfWeek.ToString().ToUpper

            If ((ddlContactType.Text.Trim = "CORPORATE") Or (ddlContactType.Text.Trim = "COMPANY")) Then
                txtContType1.Text = "CORPORATE"
                txtContType2.Text = "COMPANY"
                txtContType3.Text = "RESIDENTIAL"
                'txtContType4.Text = "PERSON"
            Else
                txtContType1.Text = "RESIDENTIAL"
                txtContType2.Text = "PERSON"
                txtContType3.Text = "CORPORATE"
                'txtContType4.Text = "COMPANY"
            End If

            If GridSelected = "SQLDSContract" Then
                SQLDSContract.SelectCommand = txt.Text
                SQLDSContract.DataBind()
            ElseIf GridSelected = "SQLDSContractClientId" Then
                'SqlDataSource1.SelectCommand = txt.Text
                'SQLDSContractClientId.DataBind()
            ElseIf GridSelected = "SQLDSContractNo" Then
                'SqlDataSource1.SelectCommand = txt.Text
                SqlDSContractNo.DataBind()
            End If

            Session("contractdetailfrom") = ""
            Session("contractno") = ""
            Session("serviceschedulefrom") = ""
            'Session("contractfrom") = ""


            ddlStatusChSt.Items.Clear()
            ddlStatusChSt.Items.Add("--SELECT--")
            'CHETXV()
            If txtStatus.Text = "O" Then

                ddlStatusChSt.Items.Add("C - Completed")
                ddlStatusChSt.Items.Add("P - Post")
                ddlStatusChSt.Items.Add("S - Suspend")
                ddlStatusChSt.Items.Add("V - Void")
            Else
                ddlStatusChSt.Items.Add("O - Open")
            End If



        Catch ex As Exception
            'MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try
        'txtMode.Text = "Edit"
        'EnabledControls()
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        'btnQuit.Enabled = True
        'btnQuit.ForeColor = System.Drawing.Color.Black
        'btnCopyAdd.Enabled = True
        'btnCopyAdd.ForeColor = System.Drawing.Color.Black
        'txtID.Enabled = False

        'txtMode.Text = "EDIT"
        'EnableControls()

        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black

        btnCopy.Enabled = True
        btnCopy.ForeColor = System.Drawing.Color.Black

        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        'btnQuit.Enabled = True
        'btnQuit.ForeColor = System.Drawing.Color.Black


        'btnContractDetail.Enabled = True
        'btnTerminate.Enabled = True
        'btnEarlyCompletion.Enabled = True
        'btnContractRenewal.Enabled = True
        'btnCancelByCompany.Enabled = True

        btnChangeStatus.Enabled = True
        'btnRevision.Enabled = True
        'btnPrint.Enabled = True


        btnClaim.Enabled = True

        'If txtGS.Text = "P" Then
        '    'btnServiceSchedule.Enabled = False

        'Else
        '    'btnServiceSchedule.Enabled = True
        '    'btnSave.Enabled = True
        'End If

        'btnSvcEdit.Enabled = False
        'btnSvcDelete.Enabled = False
    End Sub


    'Function

    Private Sub GeneratePorjectNo()

        Dim lPrefix As String
        Dim lYear As String
        Dim lMonth As String
        Dim lContractNo As String
        Dim lSuffixVal As String
        Dim lSuffix As String
        Dim lSetWidth As Integer
        Dim lSetZeroes As String
        Dim lSeparator As String

        Dim strUpdate As String
        lSeparator = "-"
        strUpdate = ""

        'lMonth = Month(CDate(Me.txtContractDate.Text))

        'If Len(lMonth) = 1 Then
        '    lMonth = "0" & lMonth
        'End If
        'lYear = Year(CDate(Me.txtContractDate.Text))
        'lPrefix = lYear & lMonth
        'lContractNo = "SVCN" + lPrefix + "-"

        Dim strdate As Date
        Dim d As Date
        If Date.TryParseExact(txtContractDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            strdate = d.ToShortDateString
        End If

        'If Date.TryParseExact(txtContractDate.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d then
        '    strdate = d.ToShortDateString
        'End If

        'Command.Parameters.AddWithValue("@ServiceDate", strdate.ToString("yyyy-MM-dd"))
        'strdate = strdate.ToString("MM-dd-yyyy")
        'lPrefix = Format(CDate(Me.txtContractDate.Text), "yyyyMM")


        'lPrefix = Format(CDate(Me.txtContractDate.Text), "yyyyMM")
        lPrefix = Format(CDate(strdate), "yyyyMM")
        lContractNo = "PROJ" + lPrefix + "-"
        lMonth = Right(lPrefix, 2)
        lYear = Left(lPrefix, 4)


        'lPrefix = Format(CDate(Me.txtContractDate.Text), "yyyyMM")
        'lPrefix = Format(CDate(strdate), "yyyyMM")
        'lContractNo = "SVCN" + lPrefix +
        lPrefix = "PROJ" + lYear
        lSuffixVal = 0

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim commandDocControl As MySqlCommand = New MySqlCommand
        commandDocControl.CommandType = CommandType.Text
        commandDocControl.CommandText = "SELECT * FROM tbldoccontrol where prefix='" & lPrefix & "'"
        commandDocControl.Connection = conn

        Dim dr As MySqlDataReader = commandDocControl.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then

            If lMonth = "01" Then
                lSuffixVal = dt.Rows(0)("Period01").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period01 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "02" Then
                lSuffixVal = dt.Rows(0)("Period02").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString
                strUpdate = " Update tbldoccontrol set Period02 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "03" Then
                lSuffixVal = dt.Rows(0)("Period03").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period03 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "04" Then
                lSuffixVal = dt.Rows(0)("Period04").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period04 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "05" Then
                lSuffixVal = dt.Rows(0)("Period05").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period05 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "06" Then
                lSuffixVal = dt.Rows(0)("Period06").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period06 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "07" Then
                lSuffixVal = dt.Rows(0)("Period07").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period07 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "08" Then
                lSuffixVal = dt.Rows(0)("Period08").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString
                strUpdate = " Update tbldoccontrol set Period08 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "09" Then
                lSuffixVal = dt.Rows(0)("Period09").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period09 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "10" Then
                lSuffixVal = dt.Rows(0)("Period10").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period10 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "11" Then
                lSuffixVal = dt.Rows(0)("Period11").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period11 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            ElseIf lMonth = "12" Then
                lSuffixVal = dt.Rows(0)("Period12").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString
                strUpdate = " Update tbldoccontrol set Period12 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

            End If

            Dim commandDocControlEdit As MySqlCommand = New MySqlCommand

            commandDocControlEdit.CommandType = CommandType.Text
            commandDocControlEdit.CommandText = strUpdate
            commandDocControlEdit.Connection = conn

            Dim dr2 As MySqlDataReader = commandDocControlEdit.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr)
        Else


            Dim lSuffixVal1 As String
            Dim lSuffixVal2 As String
            Dim lSuffixVal3 As String
            Dim lSuffixVal4 As String
            Dim lSuffixVal5 As String
            Dim lSuffixVal6 As String
            Dim lSuffixVal7 As String
            Dim lSuffixVal8 As String
            Dim lSuffixVal9 As String
            Dim lSuffixVal10 As String
            Dim lSuffixVal11 As String
            Dim lSuffixVal12 As String

            lSuffixVal1 = 0
            lSuffixVal2 = 0
            lSuffixVal3 = 0
            lSuffixVal4 = 0
            lSuffixVal5 = 0
            lSuffixVal6 = 0
            lSuffixVal7 = 0
            lSuffixVal8 = 0
            lSuffixVal9 = 0
            lSuffixVal10 = 0
            lSuffixVal11 = 0
            lSuffixVal12 = 0

            If lMonth = "01" Then
                lSuffixVal1 = 1
            ElseIf lMonth = "02" Then
                lSuffixVal2 = 1
            ElseIf lMonth = "03" Then
                lSuffixVal3 = 1
            ElseIf lMonth = "04" Then
                lSuffixVal4 = 1
            ElseIf lMonth = "05" Then
                lSuffixVal5 = 1
            ElseIf lMonth = "06" Then
                lSuffixVal6 = 1
            ElseIf lMonth = "07" Then
                lSuffixVal7 = 1
            ElseIf lMonth = "08" Then
                lSuffixVal8 = 1
            ElseIf lMonth = "09" Then
                lSuffixVal9 = 1
            ElseIf lMonth = "10" Then
                lSuffixVal10 = 1
            ElseIf lMonth = "11" Then
                lSuffixVal11 = 1
            ElseIf lMonth = "12" Then
                lSuffixVal12 = 1
            End If
            Dim commandDocControlInsert As MySqlCommand = New MySqlCommand

            commandDocControlInsert.CommandType = CommandType.Text
            'commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
            '               "('" & Left(lPrefix, Len(lPrefix) - 2) & "','M','" & lSeparator & "',6,0,0,0,0,0,0,0,0,0,0,0,0)"
            commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                     "('" & lPrefix & "','M','" & lSeparator & "',6," & lSuffixVal1 & "," & lSuffixVal2 & "," & lSuffixVal3 & "," & lSuffixVal4 & "," & lSuffixVal5 & "," & lSuffixVal6 & "," & lSuffixVal7 & "," & lSuffixVal8 & "," & lSuffixVal9 & "," & lSuffixVal10 & "," & lSuffixVal11 & "," & lSuffixVal12 & ")"
            commandDocControlInsert.Connection = conn

            Dim dr2 As MySqlDataReader = commandDocControlInsert.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr)

            lSetWidth = 6
            lSuffixVal = 1

        End If

        lSetZeroes = ""

        Dim i As Integer
        If lSetWidth > 0 Then
            For i = 1 To lSetWidth - (Len(lSuffixVal))
                lSetZeroes = lSetZeroes & "0"
            Next i
            'ElseIf pLength = 0 Then                     ' Use 6 and save it in Doc Control
            '    strZeros = "000000"
            '    setWidth = 6
            'Else                                        ' Use vLength and save it in Doc Control
            '    For i = 1 To pLength
            '        strZeros = strZeros & "0"
            '    Next i
            '    setWidth = pLength
        End If
        lSuffix = lSetZeroes + lSuffixVal.ToString()
        txtContractNo.Text = lContractNo + lSuffix
    End Sub
    Public Sub MakeMeNull()

        If txtMode.Text <> "VIEW" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimecontract();", True)
        End If

        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""

        If String.IsNullOrEmpty(lblAccountIdContact1.Text) = True Then
            txtAccountId.Text = ""
            txtCustName.Text = ""
            ddlSalesman.SelectedIndex = 0
            ddlCompanyGrp.SelectedIndex = 0
        End If

        txtContractNo.Text = ""
        txtContractNoSelected.Text = ""
        txtOurRef.Text = ""
        txtYourRef.Text = ""
        'txtTelephone.Text = ""
        'txtFax.Text = ""
        'txtConPerMobile.Text = ""
        txtAgreeVal.Text = "0.00"
        'txtDuration.Text = "1"
        'rbtLstDuration.SelectedIndex = 3
        'rbtComputationMethod.SelectedIndex = 0
        'txtContractStart.Text = ""
        'txtServStart.Text = ""
        'txtContractEnd.Text = ""
        'txtServEnd.Text = ""

        ''txtWarrStart.Text = ""
        'txtWarrEnd.Text = ""
        txtServStartDay.Text = ""
        TxtServEndDay.Text = ""
        'txtServEndDay.Text = DateTime.Parse(txtServEnd.Text).DayOfWeek.ToString()
        'txtServTimeIn.Text = ""
        'txtServTimeOut.Text = ""
        'txtAllocTime.Text = ""

        'txtWarrEnd.Text = ""
        'txtValPerMnth.Text = "0.00"
        'txtBillingAmount.Text = "0.00"
        txtContractNotes.Text = ""
        'txtServInst.Text = ""
        txtStatus.Text = "O"

        'txtRs.Text = "O"
        txtAddress.Text = ""
        txtOfficeAddress.Text = ""
        'txtNS.Text = "O"
        'txtProspectId.Text = ""
        'txtGstNo.Text = ""
        'txtAccCode.Text = ""
        txtPostal.Text = ""
        'txtGS.Text = "O"
        'txtResponse.Text = ""
        'txtCancelCharges.Text = ""
        'txtMinDuration.Text = ""
        txtConDetVal.Text = "0.00"
        'txtPerServVal.Text = "0.00"
        'txtDisPercent.Text = "0.00"
        'txtDisAmt.Text = "0.00"
        txtTotContVal.Text = "0.00"
        'txtOContract.Text = ""
        'txtRenewalDate.Text = ""
        'txtRenewed.Text = ""
        'txtQuotePrice.Text = "0.00"
        'txtQuoteUnit.Text = ""
        txtRemarks.Text = ""
        'txtPrintBody.Text = ""
        txtClient.Text = ""
        'txtContactPerson.Text = ""
        'txtSalesMan.Text = ""
        'txtScheduler.Text = ""
        'txtTeamIncharge.Text = ""
        'txtTeam.Text = ""
        'txtServiceBy.Text = ""
        'txtSupervisor.Text = ""
        'txtServiceNo.Text = "0"
        'txtServiceNoActual.Text = "0"
        'txtServiceBal.Text = "0"
        ''txtPrevContract.Text = ""
        'txtServiceAmt.Text = "0.00"
        'txtServiceAmtActual.Text = "0.00"
        'txtServiceAmtBal.Text = "0.00"
        'txtHourAmt.Text = "0.00"
        'txtHourBal.Text = "0.00"
        'txtHourNo.Text = "0"
        'txtHourNoActual.Text = "0"
        'txtUnitAmt.Text = "0.00"
        'txtUnitBal.Text = "0.00"
        'txtUnitNo.Text = "0.00"
        'txtUnitNoActual.Text = "0.00"
        'txtCallAmt.Text = "0.00"
        'txtCallBal.Text = "0.00"
        'txtCallNo.Text = "0"
        'txtCallNoActual.Text = "0"
        'txtCompensateMax.Text = "0.00"
        'txtCompensatePct.Text = "0.00"
        'txtAmtBalance.Text = "0.00"
        'txtAmtCompleted.Text = "0.00"

        txtPONo.Text = ""
        txtBillingAddress.Text = ""
        txtServiceAddressCons.Text = ""

        'txtContractnoSearch.Text = ""
        'txtClientNameSearch.Text = ""
        'txtAccountIdSearch.Text = ""
        chkSearchAll.Checked = False
        'chkSearchOpen.Checked = False
        'chkSearchVoid.Checked = False
        'chkSearchOnHold.Checked = False
        'chkSearchCancelled.Checked = False
        'chkSearchCompleted.Checked = False
        'chkSearchPost.Checked = False
        'chkSearchEarlyComplete.Checked = False
        'chkSearchTerminated.Checked = False
        'chkSearchRevised.Checked = False


        txtSiteAddress.Text = ""
        txtSiteBuilding.Text = ""
        txtSiteStreet.Text = ""
        ddlSiteCity.SelectedIndex = 0
        ddlSiteState.SelectedIndex = 0
        ddlSiteCountry.SelectedIndex = 0
        txtSitePostal.Text = ""

        txtModeRenew.Text = ""
        txtRcnoRenew.Text = 0

        If String.IsNullOrEmpty(lblAccountIdContact1.Text) = True Then
            ddlContactType.Text = "COMPANY"
            txtContType1.Text = "CORPORATE"
            txtContType2.Text = "COMPANY"
        End If

        'txtTeamSelection.Text = ""

        ddlLocateGrp.SelectedIndex = 0
        'ddlScheduleType.SelectedIndex = 0
        ddlContractGrp.SelectedIndex = 0

        'ddlBillingFreq.SelectedIndex = 0

        'ddlScheduler.SelectedIndex = 0


        ddlScheduler.Text = Session("StaffID")
        txtProjectName.Text = ""



        btnEdit.Enabled = False
        'btnEdit.ForeColor = System.Drawing.Color.Gray

        btnCopy.Enabled = False
        'btnCopy.ForeColor = System.Drawing.Color.Gray

        'btnCancelByCompany.Enabled = False
        'btnEarlyCompletion.Enabled = False
        'btnTerminate.Enabled = False
        btnChangeStatus.Enabled = False
        'btnServiceSchedule.Enabled = False
        btnClaim.Enabled = False
        'btnContractRenewal.Enabled = False
        'btnContractDetail.Enabled = False
        'btnRevision.Enabled = False
        btnDelete.Enabled = False
        btnPrint.Enabled = False
        'btnClaim.Text = "SERVICE "
        txtSearch1Status.Text = "O,P"

        txtOriginalAccountId.Text = ""

        'txtTotalArea.Text = "0.00"
        'txtCompletedArea.Text = "0.00"
        'txtBalanceArea.Text = "0.00"

        'ddlTerminationCode.SelectedIndex = 0

        txtRetentionPerc.Text = "0.00"
        txtRetentionValue.Text = "0.00"
        txtRetentionReleaseDate.Text = ""
        chkGenerateCreditNote.Visible = False
        chkGenerateCreditNote.Checked = False
        'ddlSalesman.Items.Clear()
        'Query = "Select * from tblStaff where Roles = 'SALES MAN'"
        'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSalesman)
        'PopulateDropDownList(Query, "StaffId", "StaffId", ddlSearchSalesman)

        DisableControls()
        EnableSvcControls()


    End Sub







    Private Sub pFindContractEndDate()
        'If Me.txtDuration.ReadOnly = True Then Exit Sub
        'Try

        '    txtContractStart.Text = Now.Date.ToString("dd/MM/yyyy")
        '    txtServStart.Text = Now.Date.ToString("dd/MM/yyyy")
        '    txtWarrStart.Text = Now.Date.ToString("dd/MM/yyyy")

        '    Dim d As Date

        '    If Date.TryParseExact(txtServStart.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
        '        txtServStartDay.Text = DateTime.Parse(d.ToShortDateString).DayOfWeek.ToString()
        '    End If

        '    Dim dateStart, dateEnd As Date
        '    dateStart = CDate(Me.txtContractStart.Text)

        '    If rbtLstDuration.SelectedIndex = 0 Then
        '        dateEnd = DateAdd(DateInterval.Day, Val(Me.txtDuration.Text), dateStart)
        '        txtContractEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '        txtServEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '        txtWarrEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '    ElseIf rbtLstDuration.SelectedIndex = 1 Then
        '        dateEnd = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Day, (Val(Me.txtDuration.Text) * 7), dateStart))
        '        txtContractEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '        txtServEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '        txtWarrEnd.Text = dateEnd.ToString("dd/MM/yyyy")

        '    ElseIf rbtLstDuration.SelectedIndex = 2 Then
        '        dateEnd = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, Val(Me.txtDuration.Text), dateStart))
        '        txtContractEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '        txtServEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '        txtWarrEnd.Text = dateEnd.ToString("dd/MM/yyyy")

        '    ElseIf rbtLstDuration.SelectedIndex = 3 Then
        '        dateEnd = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Year, Val(Me.txtDuration.Text), dateStart))
        '        txtContractEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '        txtServEnd.Text = dateEnd.ToString("dd/MM/yyyy")
        '        txtWarrEnd.Text = dateEnd.ToString("dd/MM/yyyy")

        '    End If



        '    If Date.TryParseExact(txtServEnd.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
        '        TxtServEndDay.Text = DateTime.Parse(d.ToShortDateString).DayOfWeek.ToString()
        '    End If



        '    'Dim NoofMonth As Integer
        '    'NoofMonth = DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text))
        '    'txtValPerMnth.Text = NoofMonth.ToString()

        '    ''txtServEndDay.Text = DateTime.Parse(txtServEnd.Text).DayOfWeek.ToString()
        '    Dim NoofMonth As Integer
        '    NoofMonth = 0.0
        '    NoofMonth = Int(DateDiff(DateInterval.Month, CDate(txtServStart.Text), CDate(txtServEnd.Text)))
        '    txtNoofMonth.Text = NoofMonth
        '    If NoofMonth = 0 Then
        '        txtValPerMnth.Text = Convert.ToDecimal(Convert.ToDecimal(txtAgreeVal.Text))
        '    Else
        '        txtValPerMnth.Text = Convert.ToDecimal(Convert.ToDecimal(txtAgreeVal.Text) / NoofMonth)
        '    End If


        'Catch ex As Exception
        '    MsgBox("Error" & ex.Message)
        'End Try
    End Sub
    Private Sub DisableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray
        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black
        btnClient.Visible = False
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black

        txtAccountId.Enabled = False
        txtContractNo.Enabled = False
        txtContractDate.Enabled = False
        txtOurRef.Enabled = False
        txtYourRef.Enabled = False
        txtPONo.Enabled = False
        'txtTelephone.Enabled = False
        'txtFax.Enabled = False
        'txtConPerMobile.Enabled = False
        txtAgreeVal.Enabled = False
        'txtDuration.Enabled = False
        txtContractStart.Enabled = False
        txtContractEnd.Enabled = False
        txtServStart.Enabled = False
        txtServStartDay.Enabled = False
        txtServEnd.Enabled = False
        TxtServEndDay.Enabled = False

        'ChkWithWarranty.Enabled = False
        'ChkRequireInspection.Enabled = False
        'txtWarrDurtion.Enabled = False
        'ddlInspectionFrequency.Enabled = False
        'TxtInspectionStart.Enabled = False
        'TxtInspectionEnd.Enabled = False

        'txtWarrStart.Enabled = False
        'txtWarrEnd.Enabled = False
        'txtValPerMnth.Enabled = False
        txtContractNotes.Enabled = False

        txtStatus.Enabled = False
        txtCustName.Enabled = False
        'txtRs.Enabled = False
        txtAddress.Enabled = False
        txtOfficeAddress.Enabled = False
        'txtNS.Enabled = False
        'txtProspectId.Enabled = False
        'txtGstNo.Enabled = False
        'txtAccCode.Enabled = False
        txtPostal.Enabled = False
        'txtGS.Enabled = False
        'txtResponse.Enabled = False
        'txtCancelCharges.Enabled = False
        'txtMinDuration.Enabled = False
        txtConDetVal.Enabled = False
        'txtPerServVal.Enabled = False
        'txtDisPercent.Enabled = False
        'txtDisAmt.Enabled = False
        'txtTotContVal.Enabled = False
        'txtRenewed.Enabled = False
        'txtRenewalDate.Enabled = False
        'txtOContract.Enabled = False
        'txtQuotePrice.Enabled = False
        'txtQuoteUnit.Enabled = False
        txtRemarks.Enabled = False
        'txtPrintBody.Enabled = False
        txtClient.Enabled = False
        'txtContactPerson.Enabled = False
        ddlSalesman.Enabled = False
        ddlScheduler.Enabled = False

        'rbtLstDuration.Enabled = False
        'rbtComputationMethod.Enabled = False
        'ddlScheduleType.Enabled = False
        'txtServTimeIn.Enabled = False
        'txtServTimeOut.Enabled = False
        'txtAllocTime.Enabled = False
        'txtTeamIncharge.Enabled = False
        'txtTeam.Enabled = False
        'txtServiceBy.Enabled = False
        'txtServInst.Enabled = False

        'txtNoOfHrsServBal.Enabled = False
        'txtNoOfHrsServComp.Enabled = False
        'txtNoOfHrsTotServ.Enabled = False
        'txtNoOfHrsTotAmt.Enabled = False
        'txtNoOfPhServBal.Enabled = False
        'txtNoOfPhServComp.Enabled = False
        'txtNoOfPhTotAmt.Enabled = False
        'txtNoOfPhTotServ.Enabled = False
        'txtNoOfServBal.Enabled = False
        'txtNoOfServComp.Enabled = False
        'txtNoOfServTotAmt.Enabled = False
        'txtNoOfServTotServ.Enabled = False
        'txtNoOfUnitServBal.Enabled = False
        'txtNoOfUnitServComp.Enabled = False
        'txtNoOfUnitTotAmt.Enabled = False
        'txtNoOfUnitTotServ.Enabled = False
        'txtUnExpBal.Enabled = False
        'txtUnExpTotServ.Enabled = False


        'txtBillingAmount.Enabled = False
        'txtActualEnd.Enabled = False
        'txtServiceAmt.Enabled = False
        'txtServiceNo.Enabled = False
        'txtHourAmt.Enabled = False
        'txtHourNo.Enabled = False
        'txtUnitAmt.Enabled = False
        'txtUnitNo.Enabled = False
        'txtCallAmt.Enabled = False
        'txtCallNo.Enabled = False
        'txtAmtCompleted.Enabled = False


        ddlContactType.Enabled = False
        ddlLocateGrp.Enabled = False

        ddlContractGrp.Enabled = False
        ddlCompanyGrp.Enabled = False

        'txtSearchProjectName.Enabled = False
        'ddlBillingFreq.Enabled = False

        'SASI - Industry Segment 28/04/2017

        'ddlIndustry.Enabled = False
        'ddlServiceTypeID.Enabled = False
        'ddlCategoryID.Enabled = False
        'ddlMarketSegmentID.Enabled = False
        'ddlPortfolioYN.Enabled = False
        'txtPortfolioValue.Enabled = False

        'SASI - End Industry Segment

        'txtTotalArea.Enabled = False
        txtRetentionPerc.Enabled = False
        txtRetentionValue.Enabled = False
        txtRetentionReleaseDate.Enabled = False

        btnDelete.Enabled = False
        'btnContractDetail.Enabled = False
        'btnTerminate.Enabled = False
        'btnEarlyCompletion.Enabled = False
        'btnContractRenewal.Enabled = False
        'btnCancelByCompany.Enabled = False
        'btnServiceSchedule.Enabled = False
        btnClaim.Enabled = False

        txtProjectName.Enabled = False
        txtSiteAddress.Enabled = False
        txtSiteBuilding.Enabled = False
        txtSiteStreet.Enabled = False
        ddlSiteCity.Enabled = False
        ddlSiteState.Enabled = False
        ddlSiteCountry.Enabled = False
        txtSitePostal.Enabled = False
    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black
        btnClient.Visible = True
        'btnADD.Enabled = False
        'btnADD.ForeColor = System.Drawing.Color.Gray

        'btnDelete.Enabled = False
        'btnDelete.ForeColor = System.Drawing.Color.Gray

        btnClient.Visible = False

        If String.IsNullOrEmpty(lblAccountIdContact1.Text) = True Then
            txtAccountId.Enabled = True
            txtCustName.Enabled = True
            ddlSalesman.Enabled = True
            ddlCompanyGrp.Enabled = True
            'btnClient.Enabled = True
            btnClient.Visible = True
            ddlContactType.Enabled = True
        End If



        txtContractNo.Enabled = True
        txtContractDate.Enabled = True
        txtOurRef.Enabled = True
        txtYourRef.Enabled = True
        txtPONo.Enabled = True
        'txtTelephone.Enabled = True
        'txtFax.Enabled = True
        'txtConPerMobile.Enabled = True
        txtAgreeVal.Enabled = True
        'txtDuration.Enabled = True
        txtContractStart.Enabled = True
        txtContractEnd.Enabled = True
        txtServStart.Enabled = True
        txtServStartDay.Enabled = True
        txtServEnd.Enabled = True
        TxtServEndDay.Enabled = True

        'ChkWithWarranty.Enabled = True
        'ChkRequireInspection.Enabled = True
        'txtWarrDurtion.Enabled = True
        'ddlInspectionFrequency.Enabled = True
        'TxtInspectionStart.Enabled = True
        'TxtInspectionEnd.Enabled = True

        'txtWarrStart.Enabled = True
        'txtWarrEnd.Enabled = True
        'txtValPerMnth.Enabled = True
        txtContractNotes.Enabled = True

        'txtStatus.Enabled = True

        'txtRs.Enabled = True
        txtAddress.Enabled = True
        txtOfficeAddress.Enabled = True
        'txtNS.Enabled = True
        'txtProspectId.Enabled = True
        'txtGstNo.Enabled = True
        'txtAccCode.Enabled = True
        txtPostal.Enabled = True
        'txtGS.Enabled = True
        'txtResponse.Enabled = True
        'txtCancelCharges.Enabled = True
        'txtMinDuration.Enabled = True
        txtConDetVal.Enabled = True
        'txtPerServVal.Enabled = True
        'txtDisPercent.Enabled = True
        'txtDisAmt.Enabled = True
        'txtTotContVal.Enabled = True
        'txtRenewed.Enabled = True
        'txtRenewalDate.Enabled = True
        'txtOContract.Enabled = True
        'txtQuotePrice.Enabled = True
        'txtQuoteUnit.Enabled = True
        txtRemarks.Enabled = True
        'txtPrintBody.Enabled = True

        txtClient.Enabled = True
        'txtContactPerson.Enabled = True

        ddlScheduler.Enabled = True
        'rbtLstDuration.Enabled = True
        'rbtComputationMethod.Enabled = True
        'ddlScheduleType.Enabled = True
        'txtTeamIncharge.Enabled = True
        'txtTeam.Enabled = True
        'txtServiceBy.Enabled = True
        'txtServTimeIn.Enabled = True
        'txtServTimeOut.Enabled = True
        'txtAllocTime.Enabled = True
        'txtServInst.Enabled = True

        'txtNoOfHrsServBal.Enabled = True
        'txtNoOfHrsServComp.Enabled = True
        'txtNoOfHrsTotServ.Enabled = True
        'txtNoOfHrsTotAmt.Enabled = True
        'txtNoOfPhServBal.Enabled = True
        'txtNoOfPhServComp.Enabled = True
        'txtNoOfPhTotAmt.Enabled = True
        'txtNoOfPhTotServ.Enabled = True
        'txtNoOfServBal.Enabled = True
        'txtNoOfServComp.Enabled = True
        'txtNoOfServTotAmt.Enabled = True
        'txtNoOfServTotServ.Enabled = True
        'txtNoOfUnitServBal.Enabled = True
        'txtNoOfUnitServComp.Enabled = True
        'txtNoOfUnitTotAmt.Enabled = True
        'txtNoOfUnitTotServ.Enabled = True
        'txtUnExpBal.Enabled = True
        'txtUnExpTotServ.Enabled = True

        'txtBillingAmount.Enabled = True
        'txtActualEnd.Enabled = True
        'txtServiceAmt.Enabled = True
        'txtServiceNo.Enabled = True
        'txtHourAmt.Enabled = True
        'txtHourNo.Enabled = True
        'txtUnitAmt.Enabled = True
        'txtUnitNo.Enabled = True
        'txtCallAmt.Enabled = True
        'txtCallNo.Enabled = True
        'txtAmtCompleted.Enabled = True

        'txtTotalArea.Enabled = True
        txtRetentionPerc.Enabled = True
        txtRetentionValue.Enabled = True
        txtRetentionReleaseDate.Enabled = True

        ddlLocateGrp.Enabled = True
        ddlContractGrp.Enabled = True

        txtProjectName.Enabled = True

        txtSiteAddress.Enabled = True
        txtSiteBuilding.Enabled = True
        txtSiteStreet.Enabled = True
        ddlSiteCity.Enabled = True
        ddlSiteState.Enabled = True
        ddlSiteCountry.Enabled = True
        txtSitePostal.Enabled = True

        'ddlBillingFreq.Enabled = True

        'btnContractDetail.Enabled = True
        'btnTerminate.Enabled = True
        'btnEarlyCompletion.Enabled = True
        'btnContractRenewal.Enabled = True
        'btnCancelByCompany.Enabled = True

        'SASI - Industry Segment 28/04/2017

        '       ddlIndustry.Enabled = True
        'ddlServiceTypeID.Enabled = True
        'ddlCategoryID.Enabled = True
        ''        ddlMarketSegmentID.Enabled = True

        'If Session("UserID") = "admin" Or Session("UserID") = "ADMIN" Then
        '    ddlPortfolioYN.Enabled = True

        'End If
        ' txtPortfolioValue.Enabled = True

        'SASI - End Industry Segment
        'ddlMarketSegmentID.Enabled = True

        txtConDetVal.Attributes.Remove("readonly")
        txtAgreeVal.Attributes.Remove("readonly")
        'txtDisPercent.Attributes.Remove("readonly")
        'txtDisAmt.Attributes.Remove("readonly")

        txtConDetVal.BackColor = txtContractnoSearch.BackColor
        txtAgreeVal.BackColor = txtContractnoSearch.BackColor
        'txtDisPercent.BackColor = txtContractnoSearch.BackColor
        'txtDisAmt.BackColor = txtContractnoSearch.BackColor
    End Sub


    Private Sub EnableSvcControls()
        'btnSvcSave.Enabled = False
        'btnSvcSave.ForeColor = System.Drawing.Color.Gray
        'btnSvcCancel.Enabled = False
        'btnSvcCancel.ForeColor = System.Drawing.Color.Gray

        'btnSvcAdd.Enabled = True
        'btnSvcAdd.ForeColor = System.Drawing.Color.Black
        'btnSvcEdit.Enabled = True
        'btnSvcEdit.ForeColor = System.Drawing.Color.Black
        'btnSvcDelete.Enabled = True
        'btnSvcDelete.ForeColor = System.Drawing.Color.Black


        'grvServiceLocation.Enabled = False
        'grvServices.Enabled = False
        'grvFreqDetails.Enabled = False
        'grvTargetDetails.Enabled = False

        'txtContactPerson.Enabled = False
        'BtnTeam.Visible = False

        'txtContactPerson.Enabled = False
        'txtContactPerson2.Enabled = False
        'txtPosition.Enabled = False
        'txtPosition2.Enabled = False
        'txtTelephone.Enabled = False
        'txtTelephone2.Enabled = False
        'txtFax.Enabled = False
        'txtFax2.Enabled = False
        'txtCP2Telephone.Enabled = False
        'txtCP2Telephone2.Enabled = False
        'txtConPerMobile.Enabled = False
        'txtConPerMobile2.Enabled = False
        'txtEmail1.Enabled = False
        'txtEmail2.Enabled = False
        txtAddress.Enabled = False

        'ddlScheduleType.Enabled = False
        'txtTeamIncharge.Enabled = False
        'txtTeam.Enabled = False
        'txtServiceBy.Enabled = False
        'txtSupervisor.Enabled = False
        'txtServTimeIn.Enabled = False
        'txtServTimeOut.Enabled = False
        'txtAllocTime.Enabled = False
        'txtServInst.Enabled = False
    End Sub


    'Function

    'Button-click

    Protected Sub ShowMessage(sender As Object, e As EventArgs, message As String)
        ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        Try
            txtMode.Text = ""
            MakeMeNull()
            EnableControls()
            MakeSearchNull()
            txtContractNo.Focus()

            txtMode.Text = "NEW"
            lblMessage.Text = "ACTION: ADD RECORD"
            tb1.ActiveTabIndex = 0
            'ShowMessage(UpdatePanel1, UpdatePanel1.GetType(), "ok")
            ''MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")

            'Dim message As String = "Record added successfully!!!"

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> LoadDefaultDates();;</Script>", False)
        Catch ex As Exception
            MsgBox("Error" & ex.Message)
        End Try
    End Sub


    Private Sub ConvertUpperGeneral()
        txtCustName.Text = txtCustName.Text.ToUpper
        txtOurRef.Text = txtOurRef.Text.ToUpper
        txtYourRef.Text = txtYourRef.Text.ToUpper
        txtPONo.Text = txtPONo.Text.ToUpper
        txtOfficeAddress.Text = txtOfficeAddress.Text.ToUpper
        txtPostal.Text = txtPostal.Text.ToUpper
        txtContractNotes.Text = txtContractNotes.Text.ToUpper
        txtRemarks.Text = txtRemarks.Text.ToUpper
    End Sub


    'Button click

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            Dim lAddress1 As String
            Dim lTelephone As String
            Dim lContactPerson As String
            Dim lIndustry As String
            Dim lMobile As String
            Dim lFax As String

            lAddress1 = ""
            lTelephone = ""
            lContactPerson = ""
            lIndustry = ""
            lMobile = ""
            lFax = ""


            Dim connBillingDetails As MySqlConnection = New MySqlConnection()

            connBillingDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connBillingDetails.Open()

            Dim commandBillingDetails As MySqlCommand = New MySqlCommand
            commandBillingDetails.CommandType = CommandType.Text

            If ddlContactType.Text = "COMPANY" Then

                commandBillingDetails.CommandText = "SELECT Industry, BillAddress1, BillTelephone, BillMobile, BillFax , BillContactPerson  FROM tblCompany where AccountId ='" & txtAccountId.Text & "'"
                commandBillingDetails.Connection = connBillingDetails

                Dim drBillingDetails As MySqlDataReader = commandBillingDetails.ExecuteReader()
                Dim dtBillingDetails As New DataTable
                dtBillingDetails.Load(drBillingDetails)

                If dtBillingDetails.Rows.Count > 0 Then
                    lAddress1 = dtBillingDetails.Rows(0)("BillAddress1").ToString
                    lTelephone = dtBillingDetails.Rows(0)("BillTelephone").ToString
                    lContactPerson = dtBillingDetails.Rows(0)("BillContactPerson").ToString
                    'lIndustry = dtBillingDetails.Rows(0)("Industry").ToString
                    lMobile = dtBillingDetails.Rows(0)("BillMobile").ToString
                    lFax = dtBillingDetails.Rows(0)("BillFax").ToString

                    'SASI - Industry Segment 28/04/2017

                    'ddlIndustry.SelectedItem.Text = lIndustry

                    'If String.IsNullOrEmpty(lIndustry) = False Then
                    '    Dim command1 As MySqlCommand = New MySqlCommand

                    '    command1.CommandType = CommandType.Text

                    '    command1.CommandText = "SELECT industry,marketsegmentid FROM tblindustry where industry='" & lIndustry & "'"
                    '    command1.Connection = connBillingDetails

                    '    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    '    Dim dt As New DataTable
                    '    dt.Load(dr)
                    '    ddlMarketSegmentID.SelectedItem.Text = dt.Rows(0)("marketsegmentid").ToString
                    '    If dt.Rows.Count > 0 Then

                    '    End If
                    'End If
                End If

                'SASI - End Industry Segment
            ElseIf ddlContactType.Text = "PERSON" Then
                commandBillingDetails.CommandText = "SELECT  BillAddress1,BillTelHome, BillTelMobile, BillTelFax , BillContactPerson  FROM tblPerson where AccountId ='" & txtAccountId.Text & "'"
                commandBillingDetails.Connection = connBillingDetails

                Dim drBillingDetails As MySqlDataReader = commandBillingDetails.ExecuteReader()
                Dim dtBillingDetails As New DataTable
                dtBillingDetails.Load(drBillingDetails)

                If dtBillingDetails.Rows.Count > 0 Then
                    lAddress1 = dtBillingDetails.Rows(0)("BillAddress1").ToString
                    lTelephone = dtBillingDetails.Rows(0)("BillTelHome").ToString
                    lContactPerson = dtBillingDetails.Rows(0)("BillContactPerson").ToString
                    lMobile = dtBillingDetails.Rows(0)("BillTelMobile").ToString
                    'lIndustry = ""
                    lMobile = dtBillingDetails.Rows(0)("BillTelMobile").ToString
                    lFax = dtBillingDetails.Rows(0)("BillTelFax").ToString
                End If
            End If


            connBillingDetails.Close()

            If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                ConvertUpperGeneral()
            End If

            If txtMode.Text = "NEW" Then

                If txtModeRenew.Text <> "REV" Then
                    GeneratePorjectNo()

                End If

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'Dim conn As MySqlConnection = New MySqlConnection(constr)
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text
                command1.CommandText = "SELECT ProjectNo FROM tblProject where ProjectNo=@ProjectNo"
                command1.Parameters.AddWithValue("@ProjectNo", txtContractNo.Text.Trim)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    'MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                Else
                    'Dim strdate As DateTime
                    'Dim GstNo As String

                    'input check for numbers
                    If (txtAgreeVal.Text.Trim = "") Then
                        txtAgreeVal.Text = 0
                    End If

                    'If (txtDuration.Text.Trim = "") Then
                    '    txtDuration.Text = 0
                    'End If

                    'If (txtDisPercent.Text.Trim = "") Then
                    '    txtDisPercent.Text = 0
                    'End If

                    'If (txtDisAmt.Text.Trim = "") Then
                    '    txtDisAmt.Text = 0
                    'End If

                    'If (txtAllocTime.Text.Trim = "") Then
                    '    txtAllocTime.Text = 0
                    'End If

                    'If (txtQuotePrice.Text.Trim = "") Then
                    '    txtQuotePrice.Text = 0
                    'End If

                    'If (txtResponse.Text.Trim = "") Then
                    '    txtResponse.Text = 0
                    'End If

                    'If (txtCancelCharges.Text.Trim = "") Then
                    '    txtCancelCharges.Text = 0
                    'End If

                    'If (txtMinDuration.Text.Trim = "") Then
                    '    txtMinDuration.Text = 0
                    'End If

                    'If (txtPerServVal.Text.Trim = "") Then
                    '    txtPerServVal.Text = 0
                    'End If

                    ''''''''''''''''''
                    'If (txtServiceAmt.Text.Trim = "") Then
                    '    txtServiceAmt.Text = 0
                    'End If

                    'If (txtServiceNo.Text.Trim = "") Then
                    '    txtServiceNo.Text = 0
                    'End If

                    'If (txtServiceNoActual.Text.Trim = "") Then
                    '    txtServiceNoActual.Text = 0
                    'End If

                    'If (txtServiceNoActual.Text.Trim = "") Then
                    '    txtServiceNoActual.Text = 0
                    'End If


                    'If (txtHourAmt.Text.Trim = "") Then
                    '    txtHourAmt.Text = 0
                    'End If

                    'If (txtHourNo.Text.Trim = "") Then
                    '    txtHourNo.Text = 0
                    'End If

                    'If (txtHourNoActual.Text.Trim = "") Then
                    '    txtHourNoActual.Text = 0
                    'End If

                    'If (txtHourNoActual.Text.Trim = "") Then
                    '    txtHourNoActual.Text = 0
                    'End If

                    'If (txtCallAmt.Text.Trim = "") Then
                    '    txtCallAmt.Text = 0
                    'End If

                    'If (txtCallNo.Text.Trim = "") Then
                    '    txtCallNo.Text = 0
                    'End If

                    'If (txtCallNoActual.Text.Trim = "") Then
                    '    txtCallNoActual.Text = 0
                    'End If

                    'If (txtCallNoActual.Text.Trim = "") Then
                    '    txtCallNoActual.Text = 0
                    'End If

                    'If (txtUnitAmt.Text.Trim = "") Then
                    '    txtUnitAmt.Text = 0
                    'End If

                    'If (txtUnitNo.Text.Trim = "") Then
                    '    txtUnitNo.Text = 0
                    'End If

                    'If (txtUnitNoActual.Text.Trim = "") Then
                    '    txtUnitNoActual.Text = 0
                    'End If

                    'If (txtUnitNoActual.Text.Trim = "") Then
                    '    txtUnitNoActual.Text = 0
                    'End If

                    'If (txtCompensateMax.Text.Trim = "") Then
                    '    txtCompensateMax.Text = 0
                    'End If

                    'If (txtCompensatePct.Text.Trim = "") Then
                    '    txtCompensatePct.Text = 0
                    'End If



                    'If (txtAmtBalance.Text.Trim = "") Then
                    '    txtAmtBalance.Text = 0
                    'End If

                    'If (txtAmtCompleted.Text.Trim = "") Then
                    '    txtAmtCompleted.Text = 0
                    'End If


                    '''''''''''
                    If (txtConDetVal.Text.Trim = "") Then
                        txtConDetVal.Text = 0
                    End If

                    'If String.IsNullOrEmpty(txtPortfolioValue.Text) = True Then
                    '    txtPortfolioValue.Text = 0
                    'End If

                    If String.IsNullOrEmpty(txtRetentionPerc.Text) = True Then
                        txtRetentionPerc.Text = 0
                    End If

                    'GstNo = txtGstNo.Text.Trim

                    'SASI - Industry Segment 28/04/2017
                    'If (txtPortfolioValue.Text.Trim = "") Then
                    '    txtPortfolioValue.Text = 0
                    'End If

                    'SASI - End Industry Segment

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text


                    'txtSiteAddress.Enabled = False
                    'txtSiteBuilding.Enabled = False
                    'txtSiteStreet.Enabled = False
                    'ddlSiteCity.Enabled = False
                    'ddlSiteState.Enabled = False
                    'ddlSiteCountry.Enabled = False
                    'txtSitePostal


                    Dim qry As String = "INSERT INTO tblProject (ProjectNo, ContractDate, ContactType, CustCode, EntryDate,"
                    qry = qry + "OurReference, YourReference, AgreeValue, "
                    qry = qry + " StartDate, EndDate, ServiceStart, ServiceEnd, "
                    qry = qry + " Notes,    Remarks,"
                    qry = qry + " Scheduler, SalesMan, Status, CustName, CustAddr,  "
                    qry = qry + " Postal, LocateGrp, ContractGroup, "
                    qry = qry + "ContractValue, PerServiceValue, CompanyGroup,  "
                    qry = qry + "AccountID,   "
                    qry = qry + " RetentionPerc, RetentionValue, RetentionClaimDate,  "
                    qry = qry + "BillAddress1, BillTelephone, BillMobile,  BillFax, BillContactPerson, "
                    qry = qry + "PONo, ServiceAddress, ProjectName, "
                    qry = qry + "SiteAddress, SiteBuilding, SiteStreet, SiteAddCity, SiteAddState, SiteAddCountry, SiteAddPostal, "
                    'qry = qry + "TimeIn, TimeOut, AllocatedSvcTime, ScheduleType,  TeamId, InchargeId, Support, Comments, "
                    qry = qry + "CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn)"

                    qry = qry + " VALUES(@ProjectNo, @ContractDate, @ContactType, @CustCode, @EntryDate,  @OurReference, @YourReference, "
                    qry = qry + " @AgreeValue,  @StartDate, @EndDate,"
                    qry = qry + "@ServiceStart, @ServiceEnd,  @Notes,"
                    qry = qry + " @Remarks,  @Scheduler, "
                    qry = qry + "@SalesMan, @Status, @CustName, @CustAddr, @Postal, "
                    qry = qry + "@LocateGrp,   @ContractGroup, @ContractValue,"
                    qry = qry + "@PerServiceValue, @CompanyGroup,   "
                    qry = qry + "@AccountID,   "
                    qry = qry + "@RetentionPerc, @RetentionValue, @RetentionClaimDate, "
                    qry = qry + "@BillAddress1, @BillTelephone, @BillMobile,  @BillFax, @BillContactPerson, "
                    qry = qry + "@PONo, @ServiceAddress, @ProjectName, "
                    qry = qry + "@SiteAddress, @SiteBuilding, @SiteStreet, @SiteAddCity, @SiteAddState, @SiteAddCountry, @SiteAddPostal, "
                    'qry = qry + " @TimeIn, @TimeOut, @AllocatedSvcTime, @ScheduleType, @TeamId, @InchargeId, @Support, @Comments,"
                    qry = qry + "@CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@ProjectNo", txtContractNo.Text.Trim)

                    If txtContractDate.Text.Trim = "" Then
                        command.Parameters.AddWithValue("@ContractDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@ContractDate", Convert.ToDateTime(txtContractDate.Text).ToString("yyyy-MM-dd"))
                    End If

                    command.Parameters.AddWithValue("@ContactType", txtContType2.Text)
                    command.Parameters.AddWithValue("@CustCode", txtClient.Text.Trim)
                    command.Parameters.AddWithValue("@EntryDate", Now.AddHours(13))

                    command.Parameters.AddWithValue("@OurReference", txtOurRef.Text.Trim)
                    command.Parameters.AddWithValue("@YourReference", txtYourRef.Text.Trim)
                    'command.Parameters.AddWithValue("@Telephone", txtTelephone.Text.Trim)
                    'command.Parameters.AddWithValue("@Fax", txtFax.Text.Trim)
                    'command.Parameters.AddWithValue("@Contact", txtContactPerson.Text.Trim)
                    'command.Parameters.AddWithValue("@ContactPersonMobile", txtConPerMobile.Text.Trim)
                    command.Parameters.AddWithValue("@AgreeValue", txtAgreeVal.Text.Trim)
                    'command.Parameters.AddWithValue("@Duration", txtDuration.Text.Trim)
                    'command.Parameters.AddWithValue("@DurationMs", rbtLstDuration.SelectedValue.ToString)

                    If txtContractStart.Text = "" Then
                        command.Parameters.AddWithValue("@StartDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd"))
                    End If

                    If txtContractEnd.Text.Trim = "" Then
                        command.Parameters.AddWithValue("@EndDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtContractEnd.Text).ToString("yyyy-MM-dd"))
                    End If

                    If txtServStart.Text.Trim = "" Then
                        command.Parameters.AddWithValue("@ServiceStart", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@ServiceStart", Convert.ToDateTime(txtServStart.Text).ToString("yyyy-MM-dd"))
                    End If

                    If txtServEnd.Text.Trim = "" Then
                        command.Parameters.AddWithValue("@ServiceEnd", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@ServiceEnd", Convert.ToDateTime(txtServEnd.Text).ToString("yyyy-MM-dd"))
                    End If


                    'If txtWarrStart.Text.Trim = "" Then
                    '    command.Parameters.AddWithValue("@WarrantyStart", DBNull.Value)
                    'Else
                    '    command.Parameters.AddWithValue("@WarrantyStart", Convert.ToDateTime(txtWarrStart.Text).ToString("yyyy-MM-dd"))

                    'End If

                    'If txtWarrEnd.Text.Trim = "" Then
                    '    command.Parameters.AddWithValue("@WarrantyEnd", DBNull.Value)
                    'Else
                    '    command.Parameters.AddWithValue("@WarrantyEnd", Convert.ToDateTime(txtWarrEnd.Text).ToString("yyyy-MM-dd"))

                    'End If

                    command.Parameters.AddWithValue("@Notes", txtContractNotes.Text.Trim)

                    'command.Parameters.AddWithValue("@OContractNo", txtOContract.Text.Trim)
                    'command.Parameters.AddWithValue("@PrintBody", txtPrintBody.Text.Trim)
                    command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim)
                    'command.Parameters.AddWithValue("@QuotePrice", txtQuotePrice.Text.Trim)
                    'command.Parameters.AddWithValue("@QuoteUnitMs", txtQuoteUnit.Text.Trim)

                    If ddlScheduler.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@Scheduler", "")
                    Else
                        command.Parameters.AddWithValue("@Scheduler", ddlScheduler.Text.Trim)
                    End If


                    If ddlSalesman.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@SalesMan", "")
                    Else
                        command.Parameters.AddWithValue("@SalesMan", ddlSalesman.Text.Trim)
                    End If

                    command.Parameters.AddWithValue("@Status", txtStatus.Text.Trim)
                    command.Parameters.AddWithValue("@CustName", txtCustName.Text.Trim)
                    'command.Parameters.AddWithValue("@RenewalSt", txtRs.Text.Trim)
                    command.Parameters.AddWithValue("@CustAddr", txtOfficeAddress.Text.Trim)
                    'command.Parameters.AddWithValue("@NotedST", txtNS.Text.Trim)
                    'command.Parameters.AddWithValue("@ProspectId", txtProspectId.Text.Trim)
                    'command.Parameters.AddWithValue("@GstNos", GstNo)
                    command.Parameters.AddWithValue("@Postal", txtPostal.Text.Trim)
                    command.Parameters.AddWithValue("@LocateGrp", ddlLocateGrp.SelectedValue.ToString)
                    'command.Parameters.AddWithValue("@GST", txtGS.Text.Trim)

                    If ddlContractGrp.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@ContractGroup", "")
                    Else
                        command.Parameters.AddWithValue("@ContractGroup", ddlContractGrp.SelectedValue.ToString)
                    End If


                    'command.Parameters.AddWithValue("@MainContractNo", txtMainContractNo.Text.Trim)
                    command.Parameters.AddWithValue("@ContractValue", txtConDetVal.Text.Trim)
                    command.Parameters.AddWithValue("@PerServiceValue", 0.0)
                    command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.SelectedValue.ToString)
                    'command.Parameters.AddWithValue("@Disc_Percent", txtDisPercent.Text.Trim)
                    'command.Parameters.AddWithValue("@DiscAmt", txtDisAmt.Text.Trim)
                    'command.Parameters.AddWithValue("@ResponseHours", txtResponse.Text.Trim)
                    'command.Parameters.AddWithValue("@CancelCharges", txtCancelCharges.Text.Trim)

                    'If ddlBillingFreq.SelectedIndex = 0 Then
                    '    command.Parameters.AddWithValue("@BillingFrequency", "")
                    'Else
                    '    command.Parameters.AddWithValue("@BillingFrequency", ddlBillingFreq.SelectedValue.ToString)
                    'End If

                    'command.Parameters.AddWithValue("@MinDuration", txtMinDuration.Text.Trim)
                    'command.Parameters.AddWithValue("@Settle", rbtnLSettle.SelectedValue.ToString)
                    'command.Parameters.AddWithValue("@Settle", "CB")
                    'command.Parameters.AddWithValue("@ServiceNo", txtServiceNo.Text.Trim)
                    'command.Parameters.AddWithValue("@ServiceBal", txtServiceBal.Text.Trim)
                    'command.Parameters.AddWithValue("@ServiceAmt", txtAgreeVal.Text.Trim)
                    'command.Parameters.AddWithValue("@ServiceNoActual", txtServiceNoActual.Text)

                    'command.Parameters.AddWithValue("@HourNo", txtHourNo.Text.Trim)
                    'command.Parameters.AddWithValue("@HourBal", txtHourBal.Text.Trim)
                    'command.Parameters.AddWithValue("@HourAmt", txtHourAmt.Text.Trim)
                    'command.Parameters.AddWithValue("@HourNoActual", txtHourNoActual.Text.Trim)
                    'command.Parameters.AddWithValue("@CallNo", txtCallNo.Text.Trim)
                    'command.Parameters.AddWithValue("@CallBal", txtCallBal.Text.Trim)
                    'command.Parameters.AddWithValue("@CallAmt", txtCallAmt.Text.Trim)
                    'command.Parameters.AddWithValue("@CallNoActual", txtCallNoActual.Text.Trim)
                    'command.Parameters.AddWithValue("@UnitNo", txtUnitNo.Text.Trim)
                    'command.Parameters.AddWithValue("@UnitBal", txtUnitBal.Text.Trim)
                    'command.Parameters.AddWithValue("@UnitAmt", txtUnitAmt.Text.Trim)
                    'command.Parameters.AddWithValue("@UnitNoActual", txtUnitNoActual.Text.Trim)

                    'command.Parameters.AddWithValue("@CompensateMax", txtCompensateMax.Text.Trim)
                    'command.Parameters.AddWithValue("@CompensatePct", txtCompensatePct.Text.Trim)

                    'command.Parameters.AddWithValue("@AmtCompleted", txtAmtCompleted.Text.Trim)
                    'command.Parameters.AddWithValue("@AmtBalance", txtAgreeVal.Text.Trim)

                    'If String.IsNullOrEmpty(txtValPerMnth.Text) = True Then
                    '    command.Parameters.AddWithValue("@ValuePerMonth", 0.0)
                    'Else
                    '    command.Parameters.AddWithValue("@ValuePerMonth", txtValPerMnth.Text.Trim)
                    'End If

                    'If String.IsNullOrEmpty(txtBillingAmount.Text) = True Then
                    '    command.Parameters.AddWithValue("@BillingAmount", 0.0)
                    'Else
                    '    command.Parameters.AddWithValue("@BillingAmount", txtBillingAmount.Text.Trim)
                    'End If


                    'If txtActualEnd.Text.Trim = "" Then
                    '    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
                    'Else
                    '    command.Parameters.AddWithValue("@ActualEnd", Convert.ToDateTime(txtActualEnd.Text).ToString("yyyy-MM-dd"))
                    'End If


                    command.Parameters.AddWithValue("@AccountID", txtAccountId.Text.Trim)

                    'If ChkWithWarranty.Checked = True Then
                    '    command.Parameters.AddWithValue("@Warranty", "Y")
                    'Else
                    '    command.Parameters.AddWithValue("@Warranty", "N")
                    'End If

                    'command.Parameters.AddWithValue("@WarrantyDuration", txtWarrDurtion.Text.Trim)

                    'If ChkRequireInspection.Checked = True Then
                    '    command.Parameters.AddWithValue("@RequireInspecton", "Y")
                    'Else
                    '    command.Parameters.AddWithValue("@RequireInspecton", "N")
                    'End If



                    'If ddlInspectionFrequency.SelectedIndex = 0 Then
                    '    command.Parameters.AddWithValue("@InspectionFrequency", "")
                    'Else
                    '    command.Parameters.AddWithValue("@InspectionFrequency", ddlInspectionFrequency.SelectedValue.ToString)
                    'End If


                    'If TxtInspectionStart.Text.Trim = "" Then
                    '    command.Parameters.AddWithValue("@InspectionStart", DBNull.Value)
                    'Else
                    '    command.Parameters.AddWithValue("@InspectionStart", Convert.ToDateTime(TxtInspectionStart.Text).ToString("yyyy-MM-dd"))

                    'End If

                    'If TxtInspectionEnd.Text.Trim = "" Then
                    '    command.Parameters.AddWithValue("@InspectionEnd", DBNull.Value)
                    'Else
                    '    command.Parameters.AddWithValue("@InspectionEnd", Convert.ToDateTime(TxtInspectionEnd.Text).ToString("yyyy-MM-dd"))

                    'End If

                    'If rbtComputationMethod.SelectedIndex = 0 Then
                    '    command.Parameters.AddWithValue("@ComputationMethod", "Monthly")
                    'Else
                    '    command.Parameters.AddWithValue("@ComputationMethod", "Fixed")
                    'End If

                    'If String.IsNullOrEmpty(txtTotalArea.Text) = True Then
                    '    command.Parameters.AddWithValue("@TotalArea", 0.0)
                    'Else
                    '    command.Parameters.AddWithValue("@TotalArea", txtTotalArea.Text)
                    'End If

                    'If String.IsNullOrEmpty(txtCompletedArea.Text) = True Then
                    '    command.Parameters.AddWithValue("@CompletedArea", 0.0)
                    'Else
                    '    command.Parameters.AddWithValue("@CompletedArea", txtCompletedArea.Text)
                    'End If

                    'If String.IsNullOrEmpty(txtBalanceArea.Text) = True Then
                    '    command.Parameters.AddWithValue("@BalanceArea", 0.0)
                    'Else
                    '    command.Parameters.AddWithValue("@BalanceArea", txtBalanceArea.Text)
                    'End If

                    If String.IsNullOrEmpty(txtRetentionPerc.Text) = True Then
                        command.Parameters.AddWithValue("@RetentionPerc", 0.0)
                    Else
                        command.Parameters.AddWithValue("@RetentionPerc", Convert.ToDecimal(txtRetentionPerc.Text))
                    End If

                    If String.IsNullOrEmpty(txtRetentionValue.Text) = True Then
                        command.Parameters.AddWithValue("@RetentionValue", 0.0)
                    Else
                        command.Parameters.AddWithValue("@RetentionValue", Convert.ToDecimal(txtRetentionValue.Text))
                    End If


                    If txtRetentionReleaseDate.Text.Trim = "" Then
                        command.Parameters.AddWithValue("@RetentionClaimDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@RetentionClaimDate", Convert.ToDateTime(txtRetentionReleaseDate.Text).ToString("yyyy-MM-dd"))
                    End If


                    'command.Parameters.AddWithValue("@Industry", ddlIndustry.Text)
                    command.Parameters.AddWithValue("@BillAddress1", lAddress1)
                    command.Parameters.AddWithValue("@BillTelephone", lTelephone)
                    command.Parameters.AddWithValue("@BillMobile", lMobile)
                    command.Parameters.AddWithValue("@BillFax", lFax)
                    command.Parameters.AddWithValue("@BillContactPerson", lContactPerson)

                    command.Parameters.AddWithValue("@PONo", txtPONo.Text)
                    command.Parameters.AddWithValue("@ServiceAddress", txtServiceAddressCons.Text)
                    command.Parameters.AddWithValue("@ProjectName", txtProjectName.Text.ToUpper)


                    command.Parameters.AddWithValue("@SiteAddress", txtSiteAddress.Text)
                    command.Parameters.AddWithValue("@SiteBuilding", txtSiteBuilding.Text)
                    command.Parameters.AddWithValue("@SiteStreet", txtSiteStreet.Text.ToUpper)
                    command.Parameters.AddWithValue("@SiteAddCity", ddlSiteCity.Text)
                    command.Parameters.AddWithValue("@SiteAddState", ddlSiteState.Text)
                    command.Parameters.AddWithValue("@SiteAddCountry", ddlSiteCountry.Text.ToUpper)
                    command.Parameters.AddWithValue("@SiteAddPostal", txtSitePostal.Text)



                    qry = qry + "@SiteAddress, @SiteBuilding, @SiteStreet, @SiteAddCity, @SiteAddState, @SiteAddCountry, @SiteAddPostal, "


                    'command.Parameters.AddWithValue("@OfficeAddress", txtOfficeAddress.Text.Trim)

                    'command.Parameters.AddWithValue("@ScheduleType", ddlScheduleType.SelectedValue.ToString.ToUpper)
                    'command.Parameters.AddWithValue("@TimeIn", txtServTimeIn.Text.Trim)
                    'command.Parameters.AddWithValue("@TimeOut", txtServTimeOut.Text.Trim)
                    'command.Parameters.AddWithValue("@AllocatedSvcTime", txtAllocTime.Text.Trim)

                    'command.Parameters.AddWithValue("@TeamId", txtTeam.Text.Trim)
                    'command.Parameters.AddWithValue("@InchargeId", txtTeamIncharge.Text.Trim)
                    'command.Parameters.AddWithValue("@Support", txtServiceBy.Text.Trim)
                    'command.Parameters.AddWithValue("@Comments", txtServInst.Text.Trim)


                    command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    'SASI - Industry Segment 28/04/2017
                    'Dim index As Integer = ddlServiceTypeID.SelectedItem.Text.IndexOf("-")
                    'Dim st As String = ddlServiceTypeID.SelectedItem.Text.Substring(1, index - 2)

                    'If ddlServiceTypeID.SelectedIndex = 0 Then
                    '    command.Parameters.AddWithValue("@ServiceTypeID", "")
                    '    command.Parameters.AddWithValue("@ServiceTypedescription", "")
                    'Else
                    '    command.Parameters.AddWithValue("@ServiceTypeID", Left(ddlServiceTypeID.Text, 4).ToUpper)
                    '    command.Parameters.AddWithValue("@ServiceTypedescription", Mid(ddlServiceTypeID.Text, 8, 65).ToUpper)
                    'End If

                    'If ddlCategoryID.SelectedIndex = 0 Then
                    '    command.Parameters.AddWithValue("@CategoryID", "")
                    'Else
                    '    command.Parameters.AddWithValue("@CategoryID", ddlCategoryID.Text.Trim)
                    'End If

                    'If ddlPortfolioYN.SelectedItem.Text = "YES" Then
                    '    command.Parameters.AddWithValue("@PortfolioYN", 1)
                    'Else
                    '    command.Parameters.AddWithValue("@PortfolioYN", 0)
                    'End If

                    'If String.IsNullOrEmpty(txtPortfolioValue.Text) = True Then
                    '    command.Parameters.AddWithValue("@PortfolioValue", 0.0)
                    'Else
                    '    command.Parameters.AddWithValue("@PortfolioValue", Convert.ToDecimal(txtPortfolioValue.Text))
                    'End If


                    'If ddlMarketSegmentID.Text = "--SELECT--" Then
                    '    command.Parameters.AddWithValue("@MarketSegmentID", "")
                    'Else
                    '    command.Parameters.AddWithValue("@MarketSegmentID", ddlMarketSegmentID.Text.Trim)
                    'End If

                    'SASI - End Industry Segment



                    command.Connection = conn
                    command.ExecuteNonQuery()
                    ''MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")


                    Dim sqlLastId As String
                    sqlLastId = "SELECT last_insert_id() from tblProject"


                    Dim commandRcno As MySqlCommand = New MySqlCommand
                    commandRcno.CommandType = CommandType.Text
                    commandRcno.CommandText = sqlLastId
                    commandRcno.Parameters.Clear()
                    commandRcno.Connection = conn
                    txtRcno.Text = commandRcno.ExecuteScalar()

                    'lblContractNo.Text = txtContractNo.Text
                    ''lblAccountID.Text = txtClient.Text
                    'lblAccountID.Text = txtAccountId.Text
                    'txtAccountIdSelection.Text = lblAccountID.Text
                    'lblName.Text = txtCustName.Text

                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                    'Dim message As String = "alert('Record added successfully!!!')"
                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                    'Dim MyMasterPage As MasterPageClassName = CType(Page.Master, MasterPageClassName)

                    'MyMasterPage.Functionname()

                    'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVCONT", txtContractNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountId.Text, "", txtRcno.Text)

                    'Dim mainMaster = TryCast(Master.MasterPageFile, MasterPage)
                    'If mainMaster Is Nothing Then
                    'Else
                    '    mainMaster.EventLog_Insert(Session("UserID"), "SERVCONT", txtContractNo.Text, "ADD", Now, 0, 0, 0, txtAccountId.Text, "", sqlLastId)

                    'End If


                    'EventLog_Insert(Session("UserID"), "SERVCONT", txtContractNo.Text, "ADD", Now, 0, 0, 0, txtAccountId.Text, "", sqlLastId)
                End If






                conn.Close()



                DisableControls()
                txtMode.Text = ""
                'btnContractDetail.Enabled = True
            ElseIf txtMode.Text = "EDIT" Then

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'Dim conn As MySqlConnection = New MySqlConnection(constr)
                conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand
                'command1.CommandType = CommandType.Text
                'command1.CommandText = "SELECT ProjectNo FROM tblProject where ProjectNo = @ProjectNo"
                'command1.Parameters.AddWithValue("@ProjectNo", txtContractNo.Text.Trim)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                '    MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                'Else
                'Dim strdate As DateTime
                'Dim GstNo As String

                'input check for numbers
                If (txtAgreeVal.Text.Trim = "") Then
                    txtAgreeVal.Text = 0
                End If

                'If (txtDuration.Text.Trim = "") Then
                '    txtDuration.Text = 0
                'End If

                'If (txtDisPercent.Text.Trim = "") Then
                '    txtDisPercent.Text = 0
                'End If

                'If (txtDisAmt.Text.Trim = "") Then
                '    txtDisAmt.Text = 0
                'End If

                'If (txtAllocTime.Text.Trim = "") Then
                '    txtAllocTime.Text = 0
                'End If

                'If (txtQuotePrice.Text.Trim = "") Then
                '    txtQuotePrice.Text = 0
                'End If

                'If (txtResponse.Text.Trim = "") Then
                '    txtResponse.Text = 0
                'End If

                'If (txtCancelCharges.Text.Trim = "") Then
                '    txtCancelCharges.Text = 0
                'End If

                'If (txtMinDuration.Text.Trim = "") Then
                '    txtMinDuration.Text = 0
                'End If

                'If (txtPerServVal.Text.Trim = "") Then
                '    txtPerServVal.Text = 0
                'End If

                'If (txtServiceAmt.Text.Trim = "") Then
                '    txtServiceAmt.Text = 0
                'End If

                'If (txtServiceNo.Text.Trim = "") Then
                '    txtServiceNo.Text = 0
                'End If

                'If (txtServiceNoActual.Text.Trim = "") Then
                '    txtServiceNoActual.Text = 0
                'End If

                'If (txtServiceNoActual.Text.Trim = "") Then
                '    txtServiceNoActual.Text = 0
                'End If


                'If (txtHourAmt.Text.Trim = "") Then
                '    txtHourAmt.Text = 0
                'End If

                'If (txtHourNo.Text.Trim = "") Then
                '    txtHourNo.Text = 0
                'End If

                'If (txtHourNoActual.Text.Trim = "") Then
                '    txtHourNoActual.Text = 0
                'End If

                'If (txtHourNoActual.Text.Trim = "") Then
                '    txtHourNoActual.Text = 0
                'End If

                'If (txtCallAmt.Text.Trim = "") Then
                '    txtCallAmt.Text = 0
                'End If

                'If (txtCallNo.Text.Trim = "") Then
                '    txtCallNo.Text = 0
                'End If

                'If (txtCallNoActual.Text.Trim = "") Then
                '    txtCallNoActual.Text = 0
                'End If

                'If (txtCallNoActual.Text.Trim = "") Then
                '    txtCallNoActual.Text = 0
                'End If



                'If (txtUnitAmt.Text.Trim = "") Then
                '    txtUnitAmt.Text = 0
                'End If

                'If (txtUnitNo.Text.Trim = "") Then
                '    txtUnitNo.Text = 0
                'End If

                'If (txtUnitNoActual.Text.Trim = "") Then
                '    txtUnitNoActual.Text = 0
                'End If

                'If (txtUnitNoActual.Text.Trim = "") Then
                '    txtUnitNoActual.Text = 0
                'End If



                'If (txtCompensateMax.Text.Trim = "") Then
                '    txtCompensateMax.Text = 0
                'End If

                'If (txtCompensatePct.Text.Trim = "") Then
                '    txtCompensatePct.Text = 0
                'End If



                'If (txtAmtBalance.Text.Trim = "") Then
                '    txtAmtBalance.Text = 0
                'End If

                'If (txtAmtCompleted.Text.Trim = "") Then
                '    txtAmtCompleted.Text = 0
                'End If


                If (txtConDetVal.Text.Trim = "") Then
                    txtConDetVal.Text = 0
                End If

                'GstNo = txtGstNo.Text.Trim

                'SASI - Industry Segment 28/04/2017


                If String.IsNullOrEmpty(txtRetentionPerc.Text) = True Then
                    txtRetentionPerc.Text = 0
                End If

                'If String.IsNullOrEmpty(txtPortfolioValue.Text) = True Then
                '    txtPortfolioValue.Text = 0
                'End If


                '''''''''''''''''''''''''''

                'Dim commandProjectCode As MySqlCommand = New MySqlCommand
                'commandProjectCode.CommandType = CommandType.Text
                'commandProjectCode.CommandText = "SELECT ProjectCode FROM tblContract where ProjectCode = @ProjectCode"
                'commandProjectCode.Parameters.AddWithValue("@ProjectCode", txtContractNo.Text.Trim)
                'commandProjectCode.Connection = conn

                'Dim drProjectCode As MySqlDataReader = commandProjectCode.ExecuteReader()
                'Dim dtProjectCode As New DataTable
                'dtProjectCode.Load(drProjectCode)

                'If dtProjectCode.Rows.Count > 0 Then
                '    txtProjectName.Enabled = False
                'Else
                '    txtProjectName.Enabled = True
                'End If
                ''''''''''''''''''''''''''''


                'SASI - End Industry Segment

                Dim command As MySqlCommand = New MySqlCommand
                command.CommandType = CommandType.Text

                Dim qry As String = "Update tblProject set ProjectNo = @ProjectNo, ContractDate = @ContractDate, ContactType = @ContactType, CustCode = @CustCode,"
                qry = qry + "OurReference = @OurReference, YourReference = @YourReference ,  AgreeValue = @AgreeValue, "
                qry = qry + " StartDate =@StartDate, EndDate =@EndDate, ServiceStart =@ServiceStart, ServiceEnd =@ServiceEnd, "
                qry = qry + "Notes = @Notes, Remarks =@Remarks,"
                qry = qry + "Scheduler =@Scheduler, SalesMan =@SalesMan, Status =@Status, CustName =@CustName, CustAddr =@CustAddr,  "
                qry = qry + " Postal =@Postal, LocateGrp =@LocateGrp,  ContractGroup =@ContractGroup,"
                qry = qry + "ContractValue = @ContractValue, PerServiceValue =@PerServiceValue, CompanyGroup =@CompanyGroup,  "
                qry = qry + "AccountID=@AccountID,   "
                'qry = qry + "TimeIn =@TimeIn, TimeOut =@TimeOut, AllocatedSvcTime = @AllocatedSvcTime,  ScheduleType =@ScheduleType, Comments = @Comments, TeamId =@TeamId, InchargeId =@InchargeId, Support =@Support,  "
                qry = qry + "RetentionPerc =@RetentionPerc, RetentionValue = @RetentionValue, RetentionClaimDate = @RetentionClaimDate, "
                qry = qry + "BillAddress1 = @BillAddress1, BillTelephone = @BillTelephone, BillMobile = @BillMobile,  BillFax = @BillFax, BillContactPerson = @BillContactPerson,  PONo = @PoNo, ServiceAddress = @ServiceAddress, ProjectName =@ProjectName,  "

                qry = qry + "SiteAddress =@SiteAddress, SiteBuilding =@SiteBuilding, SiteStreet=@SiteStreet, SiteAddCity =@SiteAddCity, SiteAddState=@SiteAddState, SiteAddCountry=@SiteAddCountry, SiteAddPostal=@SiteAddPostal, "

                qry = qry + "LastModifiedBy = @LastModifiedBy, LastModifiedOn = @LastModifiedOn where rcno = " & Convert.ToInt32(txtRcno.Text)
                'total 77 fields

                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@ProjectNo", txtContractNo.Text.Trim)

                If txtContractDate.Text.Trim = "" Then
                    command.Parameters.AddWithValue("@ContractDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@ContractDate", Convert.ToDateTime(txtContractDate.Text).ToString("yyyy-MM-dd"))
                End If

                command.Parameters.AddWithValue("@ContactType", txtContType2.Text)
                command.Parameters.AddWithValue("@CustCode", txtClient.Text.Trim)
                command.Parameters.AddWithValue("@EntryDate", Now.AddHours(13))

                command.Parameters.AddWithValue("@OurReference", txtOurRef.Text.Trim)
                command.Parameters.AddWithValue("@YourReference", txtYourRef.Text.Trim)
                'command.Parameters.AddWithValue("@Telephone", txtTelephone.Text.Trim)
                'command.Parameters.AddWithValue("@Fax", txtFax.Text.Trim)
                'command.Parameters.AddWithValue("@Contact", txtContactPerson.Text.Trim)
                'command.Parameters.AddWithValue("@ContactPersonMobile", txtConPerMobile.Text.Trim)
                command.Parameters.AddWithValue("@AgreeValue", txtAgreeVal.Text.Trim)
                'command.Parameters.AddWithValue("@Duration", txtDuration.Text.Trim)
                'command.Parameters.AddWithValue("@DurationMs", rbtLstDuration.SelectedValue.ToString)

                If txtContractStart.Text = "" Then
                    command.Parameters.AddWithValue("@StartDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd"))
                End If

                If txtContractEnd.Text.Trim = "" Then
                    command.Parameters.AddWithValue("@EndDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtContractEnd.Text).ToString("yyyy-MM-dd"))
                End If

                If txtServStart.Text.Trim = "" Then
                    command.Parameters.AddWithValue("@ServiceStart", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@ServiceStart", Convert.ToDateTime(txtServStart.Text).ToString("yyyy-MM-dd"))
                End If

                If txtServEnd.Text.Trim = "" Then
                    command.Parameters.AddWithValue("@ServiceEnd", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@ServiceEnd", Convert.ToDateTime(txtServEnd.Text).ToString("yyyy-MM-dd"))
                End If


                'If txtWarrStart.Text.Trim = "" Then
                '    command.Parameters.AddWithValue("@WarrantyStart", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@WarrantyStart", Convert.ToDateTime(txtWarrStart.Text).ToString("yyyy-MM-dd"))

                'End If

                'If txtWarrEnd.Text.Trim = "" Then
                '    command.Parameters.AddWithValue("@WarrantyEnd", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@WarrantyEnd", Convert.ToDateTime(txtWarrEnd.Text).ToString("yyyy-MM-dd"))

                'End If

                command.Parameters.AddWithValue("@Notes", txtContractNotes.Text.Trim)

                'command.Parameters.AddWithValue("@OContractNo", txtOContract.Text.Trim)
                'command.Parameters.AddWithValue("@PrintBody", txtPrintBody.Text.Trim)
                command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim)
                'command.Parameters.AddWithValue("@QuotePrice", txtQuotePrice.Text.Trim)
                'command.Parameters.AddWithValue("@QuoteUnitMs", txtQuoteUnit.Text.Trim)

                If ddlScheduler.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@Scheduler", "")
                Else
                    command.Parameters.AddWithValue("@Scheduler", ddlScheduler.Text.Trim)
                End If


                If ddlSalesman.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@SalesMan", "")
                Else
                    command.Parameters.AddWithValue("@SalesMan", ddlSalesman.Text.Trim)
                End If

                command.Parameters.AddWithValue("@Status", txtStatus.Text.Trim)
                command.Parameters.AddWithValue("@CustName", txtCustName.Text.Trim)
                'command.Parameters.AddWithValue("@RenewalSt", txtRs.Text.Trim)
                command.Parameters.AddWithValue("@CustAddr", txtOfficeAddress.Text.Trim)
                'command.Parameters.AddWithValue("@NotedST", txtNS.Text.Trim)
                'command.Parameters.AddWithValue("@ProspectId", txtProspectId.Text.Trim)
                'command.Parameters.AddWithValue("@GstNos", GstNo)
                command.Parameters.AddWithValue("@Postal", txtPostal.Text.Trim)
                command.Parameters.AddWithValue("@LocateGrp", ddlLocateGrp.SelectedValue.ToString)
                'command.Parameters.AddWithValue("@GST", txtGS.Text.Trim)

                If ddlContractGrp.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@ContractGroup", "")
                Else
                    command.Parameters.AddWithValue("@ContractGroup", ddlContractGrp.SelectedValue.ToString)
                End If


                'command.Parameters.AddWithValue("@MainContractNo", txtMainContractNo.Text.Trim)
                command.Parameters.AddWithValue("@ContractValue", txtConDetVal.Text.Trim)
                command.Parameters.AddWithValue("@PerServiceValue", 0.0)
                command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.SelectedValue.ToString)
                'command.Parameters.AddWithValue("@Disc_Percent", txtDisPercent.Text.Trim)
                'command.Parameters.AddWithValue("@DiscAmt", txtDisAmt.Text.Trim)
                'command.Parameters.AddWithValue("@ResponseHours", txtResponse.Text.Trim)
                'command.Parameters.AddWithValue("@CancelCharges", txtCancelCharges.Text.Trim)

                'If ddlBillingFreq.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@BillingFrequency", "")
                'Else
                '    command.Parameters.AddWithValue("@BillingFrequency", ddlBillingFreq.SelectedValue.ToString)
                'End If

                'command.Parameters.AddWithValue("@MinDuration", txtMinDuration.Text.Trim)
                'command.Parameters.AddWithValue("@Settle", rbtnLSettle.SelectedValue.ToString)
                'command.Parameters.AddWithValue("@Settle", "CB")
                'command.Parameters.AddWithValue("@ServiceNo", txtServiceNo.Text.Trim)
                'command.Parameters.AddWithValue("@ServiceBal", txtServiceBal.Text.Trim)
                'command.Parameters.AddWithValue("@ServiceAmt", txtAgreeVal.Text.Trim)
                'command.Parameters.AddWithValue("@ServiceNoActual", txtServiceNoActual.Text)

                'command.Parameters.AddWithValue("@HourNo", txtHourNo.Text.Trim)
                'command.Parameters.AddWithValue("@HourBal", txtHourBal.Text.Trim)
                'command.Parameters.AddWithValue("@HourAmt", txtHourAmt.Text.Trim)
                'command.Parameters.AddWithValue("@HourNoActual", txtHourNoActual.Text.Trim)
                'command.Parameters.AddWithValue("@CallNo", txtCallNo.Text.Trim)
                'command.Parameters.AddWithValue("@CallBal", txtCallBal.Text.Trim)
                'command.Parameters.AddWithValue("@CallAmt", txtCallAmt.Text.Trim)
                'command.Parameters.AddWithValue("@CallNoActual", txtCallNoActual.Text.Trim)
                'command.Parameters.AddWithValue("@UnitNo", txtUnitNo.Text.Trim)
                'command.Parameters.AddWithValue("@UnitBal", txtUnitBal.Text.Trim)
                'command.Parameters.AddWithValue("@UnitAmt", txtUnitAmt.Text.Trim)
                'command.Parameters.AddWithValue("@UnitNoActual", txtUnitNoActual.Text.Trim)

                'command.Parameters.AddWithValue("@CompensateMax", txtCompensateMax.Text.Trim)
                'command.Parameters.AddWithValue("@CompensatePct", txtCompensatePct.Text.Trim)

                'command.Parameters.AddWithValue("@AmtCompleted", txtAmtCompleted.Text.Trim)
                'command.Parameters.AddWithValue("@AmtBalance", txtAgreeVal.Text.Trim)

                'If String.IsNullOrEmpty(txtValPerMnth.Text) = True Then
                '    command.Parameters.AddWithValue("@ValuePerMonth", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@ValuePerMonth", txtValPerMnth.Text.Trim)
                'End If

                'If String.IsNullOrEmpty(txtBillingAmount.Text) = True Then
                '    command.Parameters.AddWithValue("@BillingAmount", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@BillingAmount", txtBillingAmount.Text.Trim)
                'End If


                'If txtActualEnd.Text.Trim = "" Then
                '    command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@ActualEnd", Convert.ToDateTime(txtActualEnd.Text).ToString("yyyy-MM-dd"))
                'End If


                command.Parameters.AddWithValue("@AccountID", txtAccountId.Text.Trim)

                'If ChkWithWarranty.Checked = True Then
                '    command.Parameters.AddWithValue("@Warranty", "Y")
                'Else
                '    command.Parameters.AddWithValue("@Warranty", "N")
                'End If

                'command.Parameters.AddWithValue("@WarrantyDuration", txtWarrDurtion.Text.Trim)

                'If ChkRequireInspection.Checked = True Then
                '    command.Parameters.AddWithValue("@RequireInspecton", "Y")
                'Else
                '    command.Parameters.AddWithValue("@RequireInspecton", "N")
                'End If



                'If ddlInspectionFrequency.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@InspectionFrequency", "")
                'Else
                '    command.Parameters.AddWithValue("@InspectionFrequency", ddlInspectionFrequency.SelectedValue.ToString)
                'End If


                'If TxtInspectionStart.Text.Trim = "" Then
                '    command.Parameters.AddWithValue("@InspectionStart", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@InspectionStart", Convert.ToDateTime(TxtInspectionStart.Text).ToString("yyyy-MM-dd"))

                'End If

                'If TxtInspectionEnd.Text.Trim = "" Then
                '    command.Parameters.AddWithValue("@InspectionEnd", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@InspectionEnd", Convert.ToDateTime(TxtInspectionEnd.Text).ToString("yyyy-MM-dd"))

                'End If

                'If rbtComputationMethod.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@ComputationMethod", "Monthly")
                'Else
                '    command.Parameters.AddWithValue("@ComputationMethod", "Fixed")
                'End If

                'If String.IsNullOrEmpty(txtTotalArea.Text) = True Then
                '    command.Parameters.AddWithValue("@TotalArea", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@TotalArea", txtTotalArea.Text)
                'End If

                'If String.IsNullOrEmpty(txtCompletedArea.Text) = True Then
                '    command.Parameters.AddWithValue("@CompletedArea", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@CompletedArea", txtCompletedArea.Text)
                'End If

                'If String.IsNullOrEmpty(txtBalanceArea.Text) = True Then
                '    command.Parameters.AddWithValue("@BalanceArea", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@BalanceArea", txtBalanceArea.Text)
                'End If

                If String.IsNullOrEmpty(txtRetentionPerc.Text) = True Then
                    command.Parameters.AddWithValue("@RetentionPerc", 0.0)
                Else
                    command.Parameters.AddWithValue("@RetentionPerc", Convert.ToDecimal(txtRetentionPerc.Text))
                End If

                If String.IsNullOrEmpty(txtRetentionValue.Text) = True Then
                    command.Parameters.AddWithValue("@RetentionValue", 0.0)
                Else
                    command.Parameters.AddWithValue("@RetentionValue", Convert.ToDecimal(txtRetentionValue.Text))
                End If


                If txtRetentionReleaseDate.Text.Trim = "" Then
                    command.Parameters.AddWithValue("@RetentionClaimDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@RetentionClaimDate", Convert.ToDateTime(txtRetentionReleaseDate.Text).ToString("yyyy-MM-dd"))
                End If


                'command.Parameters.AddWithValue("@Industry", ddlIndustry.Text)
                command.Parameters.AddWithValue("@BillAddress1", lAddress1)
                command.Parameters.AddWithValue("@BillTelephone", lTelephone)
                command.Parameters.AddWithValue("@BillMobile", lMobile)
                command.Parameters.AddWithValue("@BillFax", lFax)
                command.Parameters.AddWithValue("@BillContactPerson", lContactPerson)

                command.Parameters.AddWithValue("@PONo", txtPONo.Text)
                command.Parameters.AddWithValue("@ServiceAddress", txtServiceAddressCons.Text)
                command.Parameters.AddWithValue("@ProjectName", txtProjectName.Text)

                command.Parameters.AddWithValue("@SiteAddress", txtSiteAddress.Text)
                command.Parameters.AddWithValue("@SiteBuilding", txtSiteBuilding.Text)
                command.Parameters.AddWithValue("@SiteStreet", txtSiteStreet.Text.ToUpper)
                command.Parameters.AddWithValue("@SiteAddCity", ddlSiteCity.Text)
                command.Parameters.AddWithValue("@SiteAddState", ddlSiteState.Text)
                command.Parameters.AddWithValue("@SiteAddCountry", ddlSiteCountry.Text.ToUpper)
                command.Parameters.AddWithValue("@SiteAddPostal", txtSitePostal.Text)



                'command.Parameters.AddWithValue("@OfficeAddress", txtOfficeAddress.Text.Trim)

                'command.Parameters.AddWithValue("@ScheduleType", ddlScheduleType.SelectedValue.ToString.ToUpper)
                'command.Parameters.AddWithValue("@TimeIn", txtServTimeIn.Text.Trim)
                'command.Parameters.AddWithValue("@TimeOut", txtServTimeOut.Text.Trim)
                'command.Parameters.AddWithValue("@AllocatedSvcTime", txtAllocTime.Text.Trim)

                'command.Parameters.AddWithValue("@TeamId", txtTeam.Text.Trim)
                'command.Parameters.AddWithValue("@InchargeId", txtTeamIncharge.Text.Trim)
                'command.Parameters.AddWithValue("@Support", txtServiceBy.Text.Trim)
                'command.Parameters.AddWithValue("@Comments", txtServInst.Text.Trim)

                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                'SASI - Industry Segment 28/04/2017

                'If ddlServiceTypeID.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@ServiceTypeID", "")
                '    command.Parameters.AddWithValue("@ServiceTypedescription", "")
                'Else
                '    command.Parameters.AddWithValue("@ServiceTypeID", Left(ddlServiceTypeID.Text, 4).ToUpper)
                '    command.Parameters.AddWithValue("@ServiceTypedescription", Mid(ddlServiceTypeID.Text, 8, 65).ToUpper)
                'End If

                'If ddlCategoryID.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@CategoryID", "")
                'Else
                '    command.Parameters.AddWithValue("@CategoryID", ddlCategoryID.Text.Trim)
                'End If

                'If ddlPortfolioYN.SelectedItem.Text = "YES" Then
                '    command.Parameters.AddWithValue("@PortfolioYN", 1)
                'Else
                '    command.Parameters.AddWithValue("@PortfolioYN", 0)
                'End If

                'If String.IsNullOrEmpty(txtPortfolioValue.Text) = True Then
                '    command.Parameters.AddWithValue("@PortfolioValue", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@PortfolioValue", Convert.ToDecimal(txtPortfolioValue.Text))
                'End If


                'If String.IsNullOrEmpty(ddlMarketSegmentID.Text) = True Then
                '    command.Parameters.AddWithValue("@MarketSegmentID", "")
                'Else
                '    command.Parameters.AddWithValue("@MarketSegmentID", ddlMarketSegmentID.Text.Trim)
                'End If
                'Dim index As Integer = ddlServiceTypeID.SelectedItem.Text.IndexOf("-")
                'Dim st As String = ddlServiceTypeID.SelectedItem.Text.Substring(1, index - 2)

                'If ddlServiceTypeID.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@ServiceTypeID", "")
                '    command.Parameters.AddWithValue("@ServiceTypedescription", "")
                'Else
                '    command.Parameters.AddWithValue("@ServiceTypeID", st.Trim)
                '    command.Parameters.AddWithValue("@ServiceTypedescription", ddlServiceTypeID.SelectedValue.Trim)
                'End If

                'If ddlCategoryID.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@CategoryID", "")
                'Else
                '    command.Parameters.AddWithValue("@CategoryID", ddlCategoryID.Text.Trim)
                'End If

                'If ddlPortfolioYN.SelectedItem.Text = "YES" Then
                '    command.Parameters.AddWithValue("@PortfolioYN", 1)
                'Else
                '    command.Parameters.AddWithValue("@PortfolioYN", 0)
                'End If

                'If String.IsNullOrEmpty(txtPortfolioValue.Text) = True Then
                '    command.Parameters.AddWithValue("@PortfolioValue", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@PortfolioValue", Convert.ToDecimal(txtPortfolioValue.Text))
                'End If


                'If ddlMarketSegmentID.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@MarketSegmentID", "")
                'Else
                '    command.Parameters.AddWithValue("@MarketSegmentID", ddlMarketSegmentID.Text.Trim)
                'End If

                'SASI - End Industry Segment

                'command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtOurRef.Text).ToString("yyyy-MM-dd"))
                command.Connection = conn
                command.ExecuteNonQuery()
                ''MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")

                'Dim message As String = "alert('Record updated successfully!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)



                'Start:  Delete Detail Records if different AccountId

                'Dim conn As MySqlConnection = New MySqlConnection()

                'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand
                'command1.CommandType = CommandType.Text

                'Dim qry1 As String = "DELETE from tblContract where ContractNo= @ContractNo "

                'command1.CommandText = qry1
                'command1.Parameters.Clear()

                'command1.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                'command1.Connection = conn
                'command1.ExecuteNonQuery()

                'If txtOriginalAccountId.Text <> txtAccountId.Text Then
                '    Dim command2 As MySqlCommand = New MySqlCommand
                '    command2.CommandType = CommandType.Text

                '    Dim qry2 As String = "DELETE from tblContractDet where ContractNo= @ContractNo "

                '    command2.CommandText = qry2
                '    command2.Parameters.Clear()

                '    command2.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                '    command2.Connection = conn
                '    command2.ExecuteNonQuery()



                '    Dim command3 As MySqlCommand = New MySqlCommand
                '    command3.CommandType = CommandType.Text

                '    Dim qry3 As String = "DELETE from tblContractServingTarget where ContractNo= @ContractNo "

                '    command3.CommandText = qry3
                '    command3.Parameters.Clear()

                '    command3.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                '    command3.Connection = conn
                '    command3.ExecuteNonQuery()


                '    Dim command4 As MySqlCommand = New MySqlCommand
                '    command4.CommandType = CommandType.Text

                '    Dim qry4 As String = "DELETE from tblServiceContractFrequency where ContractNo= @ContractNo "

                '    command4.CommandText = qry4
                '    command4.Parameters.Clear()

                '    command4.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                '    command4.Connection = conn
                '    command4.ExecuteNonQuery()

                'End If


                'Dim message As String = "alert('Contract is deleted Successfully!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                'lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                'btnADD_Click(sender, e)
                'GridView1.DataBind()

                'End:  Delete Detail Records if different AccountId 




                lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                lblAlert.Text = ""
                'End If
                conn.Close()

                DisableControls()
                txtMode.Text = ""

            End If

            If String.IsNullOrEmpty(lblAccountIdContact1.Text) = True Then
                GridView1.DataSourceID = "SQLDSContract"
            Else
                If String.IsNullOrEmpty(lblAccountIdContactLocation1.Text) = False Then
                    GridView1.DataSourceID = "SQLDSContractClientIdLocation"
                    GridSelected = "SQLDSContractClientIdLocation"
                Else
                    GridView1.DataSourceID = "SQLDSContractClientId"
                    GridSelected = "SQLDSContractClientId"
                End If

            End If

            GridView1.DataBind()

            'grvContractDetail.DataSourceID = "SqlDSContractDet"
            'grvContractDetail.DataBind()

            lblContractNo.Text = txtContractNo.Text
            'lblAccountID.Text = txtClient.Text
            lblAccountID.Text = txtAccountId.Text
            txtAccountIdSelection.Text = lblAccountID.Text
            lblName.Text = txtCustName.Text


            'btnSvcEdit.Enabled = False
            'btnSvcDelete.Enabled = False
            'btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            'btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            If txtMode.Text = "NEW" Then
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PROJECT", txtContractNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountId.Text, "", txtRcno.Text)
            Else
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PROJECT", txtContractNo.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountId.Text, "", txtRcno.Text)
            End If


        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")

            'Dim message As String = "alert('" + exstr + "')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        End Try
    End Sub



    'Pop-up

    Private Sub GenerateClientAlphabets()
        Dim alphabets As New List(Of ListItem)()
        Dim alphabet As New ListItem()
        alphabet.Value = "A"
        alphabet.Selected = alphabet.Value.Equals(ViewState("ClientCurrentAlphabet"))
        alphabets.Add(alphabet)
        For i As Integer = 66 To 90
            alphabet = New ListItem()
            alphabet.Value = [Char].ConvertFromUtf32(i)
            alphabet.Selected = alphabet.Value.Equals(ViewState("ClientCurrentAlphabet"))
            alphabets.Add(alphabet)
        Next
        rptClientAlphabets.DataSource = alphabets
        rptClientAlphabets.DataBind()
    End Sub

    Protected Sub ClientAlphabet_Click(sender As Object, e As EventArgs)
        'please check when user enter search criteria for one alphabet and then without clearing the textPoPUp client
        'select another alphabet ---records are not selected

        Dim lnkAlphabet As LinkButton = DirectCast(sender, LinkButton)
        ViewState("ClientCurrentAlphabet") = lnkAlphabet.Text
        Me.GenerateClientAlphabets()
        gvClient.PageIndex = 0
        'If txtPopUpClient.Text.Trim = "" Then
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 And ContName Like '" + lnkAlphabet.Text + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
        'Else
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + lnkAlphabet.Text + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
        'End If

        If txtPopUpClient.Text.Trim = "" Then
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 And ContName Like '" + lnkAlphabet.Text + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
            If ddlContactType.SelectedValue.ToString = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where 1=1 And Name Like '" + lnkAlphabet.Text + "%'"
            Else
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPerson where 1=1 And Name Like '" + lnkAlphabet.Text + "%'"
            End If

        Else
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + lnkAlphabet.Text + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
            If ddlContactType.SelectedValue.ToString = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where 1=1 and Name like '" + lnkAlphabet.Text + "%' And upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%'"
            Else
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPerson where 1=1 and Name like '" + lnkAlphabet.Text + "%' And upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%'"
            End If
        End If

        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
    End Sub







    Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs) Handles btnPopUpClientReset.Click

    End Sub


    'Protected Sub btnPopUpTeamReset_Click(sender As Object, e As EventArgs)

    'End Sub






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


    Public Sub PopulateComboBox(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal cmb As AjaxControlToolkit.ComboBox)
        Dim con As MySqlConnection = New MySqlConnection()

        con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Using con As New MySqlConnection(constr)
        Using cmd As New MySqlCommand(query)
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            con.Open()
            cmb.DataSource = cmd.ExecuteReader()
            cmb.DataTextField = textField.Trim()
            cmb.DataValueField = valueField.Trim()
            cmb.DataBind()
            con.Close()
        End Using
        'End Using
    End Sub



    Protected Sub IsServiceDateHoliday()
        Dim conn As MySqlConnection = New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text
        command1.CommandText = "SELECT * FROM tblHoliday where Holiday= '" & Convert.ToDateTime(txtServStart.Text).ToString("yyyy-MM-dd") & "'"
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("Holiday").ToString <> "" Then
                txtServStart.ForeColor = Drawing.Color.Red
            End If

        Else
            txtServStart.ForeColor = Drawing.Color.Black
        End If

        command1.Dispose()
    End Sub
    Protected Sub txtServStart_TextChanged(sender As Object, e As EventArgs) Handles txtServStart.TextChanged
        IsServiceDateHoliday()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'txtRemarks.Text = txtSearchID.Text
        MakeMeNull()
        Dim qry As String
        qry = "select Status, ProjectNo, ProjectName, ContractDate, AccountId, CustName, CustAddr, InchargeId, AgreeValue, StartDate, EndDate, ActualEnd, "
        qry = qry + " Scheduler, ServiceAddress, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno from tblProject where 1 =1 "

        If String.IsNullOrEmpty(txtSearchID.Text.TrimEnd(",").TrimStart(",")) = False Then
            'If String.IsNullOrEmpty(txtSearchID.Text) = False Then
            'txtID.Text = txtSearchID.Text
            qry = qry + " and ProjectNo like '%" + txtSearchID.Text.TrimEnd(",").TrimStart(",") + "%'"
        End If

        If String.IsNullOrEmpty(txtSearchProjectName.Text.TrimEnd(",").TrimStart(",")) = False Then
            'txtID.Text = txtSearchID.Text
            qry = qry + " and ProjectName like '%" + txtSearchProjectName.Text.TrimEnd(",").TrimStart(",") + "%'"
        End If

        If (ddlSearchStatus.Text.Replace(",", String.Empty)) <> "-1" Then
            If ddlSearchStatus.Text <> txtDDLText.Text Then
                'ddlStatus.Text = ddlSearchStatus.Text
                qry = qry + " and status = '" + ddlSearchStatus.Text.TrimEnd(",").TrimStart(",") + "'"
            End If
        Else
            qry = qry + " and status = 'O'"
        End If


        'If String.IsNullOrEmpty(txtSearchCustCode.Text) = False Then
        '    'txtNameE.Text = txtSearchCompany.Text
        '    qry = qry + " and custcode like '%" + txtSearchCustCode.Text + "%'"
        'End If

        If String.IsNullOrEmpty(txtSearchCustCode.Text.Replace(",", String.Empty)) = False Then
            'txtNameE.Text = txtSearchCompany.Text
            qry = qry + " and Accountid like '%" + txtSearchCustCode.Text.TrimEnd(",").TrimStart(",") + "%'"
        End If

        If String.IsNullOrEmpty(txtSearchCompany.Text.Replace(",", String.Empty)) = False Then
            'txtNameE.Text = txtSearchCompany.Text
            qry = qry + " and custname like ""%" + txtSearchCompany.Text.TrimEnd(",").TrimStart(",") + "%"""
        End If
        If String.IsNullOrEmpty(txtSearchAddress.Text.Replace(",", String.Empty)) = False Then

            qry = qry + " and (custaddr like '%" + txtSearchAddress.Text.TrimEnd(",").TrimStart(",") + "%')"
            'qry = qry + " or addbuilding like '%" + txtSearchAddress.Text + "%'"
            'qry = qry + " or addstreet like '%" + txtSearchAddress.Text + "%')"
        End If

        If String.IsNullOrEmpty(txtSearchContactNo.Text.Replace(",", String.Empty)) = False Then
            'txtNameE.Text = txtSearchCompany.Text
            qry = qry + " and (telephone = '" + txtSearchContactNo.Text + "'"
            qry = qry + " or contactpersonmobile = '" + txtSearchContactNo.Text.TrimEnd(",").TrimStart(",") + "')"
        End If

        If String.IsNullOrEmpty(txtSearchPostal.Text.Replace(",", String.Empty)) = False Then
            'txtPostal.Text = txtSearchPostal.Text
            qry = qry + " and postal  = '" + txtSearchPostal.Text.TrimEnd(",").TrimStart(",") + "'"
        End If
        If String.IsNullOrEmpty(txtSearchContact.Text.Replace(",", String.Empty)) = False Then
            qry = qry + " and contact like '%" + txtSearchContact.Text.TrimEnd(",").TrimStart(",") + "%'"

        End If
        If (ddlSearchSalesman.Text.Replace(",", String.Empty)) <> "-1" Then

            qry = qry + " and salesman  = '" + ddlSearchSalesman.Text.TrimEnd(",").TrimStart(",") + "'"
        End If
        If (ddlSearchContactType.Text.Replace(",", String.Empty)) <> "-1" Then
            'If ddlSearchContactType.Text <> txtDDLText.Text Then
            qry = qry + " and ContactType  = '" + ddlSearchContactType.Text.TrimEnd(",").TrimStart(",") + "'"
            'End If
        End If
        If (ddlSearchScheduler.Text.Replace(",", String.Empty)) <> "-1" Then
            qry = qry + " and Scheduler  = '" + ddlSearchScheduler.Text.TrimEnd(",").TrimStart(",") + "'"
        End If


        If (ddlSearchInChargeId.Text.Replace(",", String.Empty)) <> "-1" Then
            qry = qry + " and Inchargeid = '" + ddlSearchInChargeId.Text.TrimEnd(",").TrimStart(",") + "'"
        End If

        If (ddlSearchContractGroup.Text.Replace(",", String.Empty)) <> "-1" Then
            qry = qry + " and ContractGroup  = '" + ddlSearchContractGroup.Text.TrimEnd(",").TrimStart(",") + "'"
        End If

        If (ddlSearchCompanyGroup.Text.Replace(",", String.Empty)) <> "-1" Then
            qry = qry + " and CompanyGroup  = '" + ddlSearchCompanyGroup.Text.TrimEnd(",").TrimStart(",") + "'"
        End If

        If (ddlSearchLocationGroup.Text.Replace(",", String.Empty)) <> "-1" Then
            qry = qry + " and LocateGRp  = '" + ddlSearchLocationGroup.Text.TrimEnd(",").TrimStart(",") + "'"
        End If


        If String.IsNullOrEmpty(txtSearchOurRef.Text.TrimEnd(",").TrimStart(",")) = False Then
            'txtPostal.Text = txtSearchPostal.Text
            qry = qry + " and ourreference  like '%" + txtSearchOurRef.Text.TrimEnd(",").TrimStart(",") + "%'"
        End If


        If String.IsNullOrEmpty(txtSearchYourRef.Text.Replace(",", String.Empty)) = False Then
            qry = qry + " and yourreference  like '%" + txtSearchYourRef.Text.TrimEnd(",").TrimStart(",") + "%'"
        End If



        If String.IsNullOrEmpty(txtSearchContractDateFrom.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchContractDateFrom.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ContractDate>= '" + Convert.ToDateTime(txtSearchContractDateFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If

        If String.IsNullOrEmpty(txtSearchContractDateTo.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchContractDateTo.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ContractDate  <= '" + Convert.ToDateTime(txtSearchContractDateTo.Text).ToString("yyyy-MM-dd") & "'"

        End If


        If String.IsNullOrEmpty(txtSearchStartDateFrom.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchStartDateFrom.Text.Replace(",", String.Empty)) Then
            qry = qry + " and StartDate  >= '" + Convert.ToDateTime(txtSearchStartDateFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If

        If String.IsNullOrEmpty(txtSearchStartDateTo.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchStartDateTo.Text.Replace(",", String.Empty)) Then
            qry = qry + " and StartDate  <= '" + Convert.ToDateTime(txtSearchStartDateTo.Text).ToString("yyyy-MM-dd") & "'"
        End If


        If String.IsNullOrEmpty(txtSearchEndDateFrom.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchEndDateFrom.Text.Replace(",", String.Empty)) Then
            qry = qry + " and EndDate  >= '" + Convert.ToDateTime(txtSearchEndDateFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If

        If String.IsNullOrEmpty(txtSearchEndDateTo.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchEndDateTo.Text.Replace(",", String.Empty)) Then
            qry = qry + " and EndDate  <= '" + Convert.ToDateTime(txtSearchEndDateTo.Text).ToString("yyyy-MM-dd") & "'"
        End If



        If String.IsNullOrEmpty(txtSearchServiceStartFrom.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchServiceStartFrom.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ServiceStart  >= '" + Convert.ToDateTime(txtSearchServiceStartFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If

        If String.IsNullOrEmpty(txtSearchServiceStartTo.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchServiceStartTo.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ServiceStart  <= '" + Convert.ToDateTime(txtSearchServiceStartTo.Text).ToString("yyyy-MM-dd") & "'"
        End If


        If String.IsNullOrEmpty(txtSearchServiceEndFrom.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchServiceEndFrom.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ServiceEnd  >= '" + Convert.ToDateTime(txtSearchServiceEndFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If

        If String.IsNullOrEmpty(txtSearchServiceEndTo.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchServiceEndTo.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ServiceEnd  <= '" + Convert.ToDateTime(txtSearchServiceEndTo.Text).ToString("yyyy-MM-dd") & "'"
        End If


        If String.IsNullOrEmpty(txtSearchActualEndFrom.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchActualEndFrom.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ActualEnd  >= '" + Convert.ToDateTime(txtSearchActualEndFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If

        If String.IsNullOrEmpty(txtSearchActualEndTo.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchActualEndTo.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ActualEnd  <= '" + Convert.ToDateTime(txtSearchActualEndTo.Text).ToString("yyyy-MM-dd") & "'"
        End If


        If String.IsNullOrEmpty(txtSearchActualEndFrom.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchActualEndFrom.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ActualEnd  >= '" + Convert.ToDateTime(txtSearchActualEndFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If

        If String.IsNullOrEmpty(txtSearchActualEndTo.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchActualEndTo.Text.Replace(",", String.Empty)) Then
            qry = qry + " and ActualEnd  <= '" + Convert.ToDateTime(txtSearchStartDateFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If



        If String.IsNullOrEmpty(txtSearchEntryDateFrom.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchEntryDateFrom.Text.Replace(",", String.Empty)) Then
            qry = qry + " and EntryDate  >= '" + Convert.ToDateTime(txtSearchEntryDateFrom.Text).ToString("yyyy-MM-dd") & "'"
        End If

        If String.IsNullOrEmpty(txtSearchEntryDateTo.Text.Replace(",", String.Empty)) = False And IsDate(txtSearchEntryDateTo.Text.Replace(",", String.Empty)) Then
            qry = qry + " and EntryDate  <= '" + Convert.ToDateTime(txtSearchEntryDateTo.Text).ToString("yyyy-MM-dd") & "'"
        End If



        qry = qry + " order by custname;"
        txt.Text = qry

        SQLDSContract.SelectCommand = qry
        SQLDSContract.DataBind()
        GridView1.DataBind()
        txtSearchID.Text = ""
        GridSelected = "SQLDSContract"

        lblAccountIdContact.Visible = False
        lblAccountIdContact1.Visible = False
        'lblAccountIdContact1.Text = ""

        lblAccountIdContactLocation.Visible = False
        lblAccountIdContactLocation1.Visible = False
        'lblAccountIdContactLocation1.Text = ""

        MakeSearchNull()

    End Sub


    Public Sub MakeSearchNull()
        txtSearchID.Text = ""
        txtSearchProjectName.Text = ""
        txtSearchCustCode.Text = ""
        txtSearchCompany.Text = ""
        txtSearchAddress.Text = ""
        txtSearchContact.Text = ""
        txtSearchContactNo.Text = ""
        txtSearchPostal.Text = ""

        txtSearchOurRef.Text = ""
        txtSearchYourRef.Text = ""

        ddlSearchSalesman.ClearSelection()
        ddlSearchScheduler.ClearSelection()
        ddlSearchStatus.ClearSelection()
        ddlSearchContractGroup.ClearSelection()
        ddlSearchCompanyGroup.ClearSelection()
        ddlSearchLocationGroup.ClearSelection()
        'ddlSearchRenewalStatus.ClearSelection()
        ddlSearchInChargeId.ClearSelection()

        txtSearchContractDateFrom.Text = ""
        txtSearchContractDateTo.Text = ""
        txtSearchServiceStartFrom.Text = ""
        txtSearchServiceStartTo.Text = ""
        txtSearchServiceEndFrom.Text = ""
        txtSearchServiceEndTo.Text = ""
        txtSearchActualEndFrom.Text = ""
        txtSearchActualEndTo.Text = ""

        txtSearchStartDateFrom.Text = ""
        txtSearchStartDateTo.Text = ""

        txtSearchEndDateFrom.Text = ""
        txtSearchEndDateTo.Text = ""
        txtSearchEntryDateFrom.Text = ""
        txtSearchEntryDateTo.Text = ""

        txtActualEndChSt.Text = ""
        txtActualEndChSt.Text = txtContractDate.Text
        'txtActualEndChSt.Text = strChStDate
        txtActualEndChSt.Text = txtActualEndChSt.Text.TrimEnd(",", "").TrimStart(",", "")
        txtCommentChSt.Text = ""


        'txtRemarks.Text = txtSearchID.Text
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click

        If Session("contracttype") = "CORPORATE" Then

            Session("contractfrom") = "clients"
            Session("accountid") = lblAccountIdContact1.Text
            Session("locationid") = lblAccountIdContactLocation1.Text
            Response.Redirect("Company.aspx")
        ElseIf Session("contracttype") = "RESIDENTIAL" Then
            Session("contractfrom") = "clients"
            Session("accountid") = lblAccountIdContact1.Text
            Session("locationid") = lblAccountIdContactLocation1.Text
            Response.Redirect("Person.aspx")
        Else

            Response.Redirect("Home.aspx")
        End If

    End Sub



    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        If GridSelected = "SQLDSContract" Then
            SQLDSContract.SelectCommand = txt.Text
            SQLDSContract.DataBind()
        ElseIf GridSelected = "SQLDSContractClientId" Then
            'SqlDataSource1.SelectCommand = txt.Text
            SQLDSContractClientId.DataBind()
        ElseIf GridSelected = "SQLDSContractNo" Then
            ''SqlDataSource1.SelectCommand = txt.Text
            SqlDSContractNo.DataBind()
        End If

        GridView1.DataBind()
    End Sub


    Protected Sub txtSearchContact_TextChanged(sender As Object, e As EventArgs) Handles txtSearchContact.TextChanged

    End Sub

    Protected Sub BtnChSt_Click(sender As Object, e As EventArgs) Handles BtnChSt.Click
        Try
            'If ddlStatusChSt.SelectedIndex = "-1" Then
            '    Dim message As String = "alert('Please Select Status!!!!!!')"
            '    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
            '    Exit Sub

            'End If
            lblAlert.Text = ""
            txtRemarks.Text = txtActualEndChSt.Text
            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_valueChSt")


            If Right(confirmValue, 3) = "Yes" Then

                Dim strdate As DateTime
                strdate = DateTime.Parse(Left(txtActualEndChSt.Text.Trim, 10))
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'Dim conn As MySqlConnection = New MySqlConnection(constr)
                conn.Open()

                ''''''''''''''''''E
                If Left(ddlStatusChSt.Text, 1) = "P" Then
                    'Dim commandE1 As MySqlCommand = New MySqlCommand

                    'commandE1.CommandType = CommandType.Text
                    'commandE1.CommandText = "select 1 from tblServiceRecord where (Status='O' or Status='' or Status is null) and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                    'commandE1.Connection = conn

                    'Dim drservice As MySqlDataReader = commandE1.ExecuteReader()
                    'Dim dtservice As New DataTable
                    'dtservice.Load(drservice)

                    'If dtservice.Rows.Count > 0 Then
                    '    Dim commandE2 As MySqlCommand = New MySqlCommand
                    '    commandE2.CommandType = CommandType.Text
                    '    'Exit Sub
                    '    commandE2.CommandText = "update  tblServiceRecord set Status='V' where (Status='O' or Status='' or Status is null)  and SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                    '    commandE2.Connection = conn
                    '    commandE2.ExecuteNonQuery()

                    'End If


                    Dim commandE As MySqlCommand = New MySqlCommand
                    commandE.CommandType = CommandType.Text

                    Dim qry As String = "UPDATE tblProject SET  Status ='P', ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "


                    commandE.CommandText = qry
                    commandE.Parameters.Clear()


                    If txtActualEndChSt.Text = "" Then
                        commandE.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
                    Else
                        'strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
                        commandE.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
                    End If

                    commandE.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)
                    commandE.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
                    commandE.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
                    commandE.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                    commandE.Connection = conn
                    commandE.ExecuteNonQuery()


                    'MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")


                    conn.Close()
                    'Dim message As String = "alert('Contract Updated successfully!!!')"
                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)


                    ''''''
                    GridView1.DataBind()
                    'txtServInst.Text = txtCommentChSt.Text
                    txtStatus.Text = "P"

                    '''''''''''''''''''E

                    '''''''''''''''''''T
                ElseIf Left(ddlStatusChSt.Text, 1) = "V" Then

                    ''If ddlTerminationCode.SelectedIndex = 0 Then
                    ''    lblAlert.Text = "Please Select Termination Code"
                    ''    Exit Sub
                    ''End If
                    'Dim commandT1 As MySqlCommand = New MySqlCommand

                    'commandT1.CommandType = CommandType.Text
                    'commandT1.CommandText = "select 1 from tblServiceRecord where (Status='O' or Status='' or Status is null) and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                    'commandT1.Connection = conn

                    'Dim drserviceT As MySqlDataReader = commandT1.ExecuteReader()
                    'Dim dtserviceT As New DataTable
                    'dtserviceT.Load(drserviceT)

                    'If dtserviceT.Rows.Count > 0 Then
                    '    Dim commandT2 As MySqlCommand = New MySqlCommand
                    '    commandT2.CommandType = CommandType.Text
                    '    'Exit Sub
                    '    commandT2.CommandText = "update  tblServiceRecord set Status='V' where (Status='O' or Status='' or Status is null)  and SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                    '    commandT2.Connection = conn
                    '    commandT2.ExecuteNonQuery()
                    'End If

                    Dim commandT As MySqlCommand = New MySqlCommand
                    commandT.CommandType = CommandType.Text

                    Dim qryT As String = "UPDATE tblProject SET  Status ='V', ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "


                    commandT.CommandText = qryT
                    commandT.Parameters.Clear()


                    If txtActualEndChSt.Text = "" Then
                        commandT.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
                    Else
                        'strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
                        commandT.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
                    End If


                    commandT.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)
                    'commandT.Parameters.AddWithValue("@TerminationCode", Right(ddlTerminationCode.Text, 5))
                    'commandT.Parameters.AddWithValue("@TerminationDescription", Left(ddlTerminationCode.Text, Len(ddlTerminationCode.Text) - 6))

                    commandT.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
                    commandT.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
                    commandT.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                    commandT.Connection = conn
                    commandT.ExecuteNonQuery()

                    'Dim message As String = "alert('Contract Terminated successfully!!!!!!')"
                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)


                    conn.Close()
                    'Dim messageT As String = "alert('Contract Updated successfully!!!')"
                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", messageT, True)

                    GridView1.DataBind()
                    'txtServInst.Text = txtCommentChSt.Text
                    txtStatus.Text = "V"

                    ''''''''''''''''''T

                    '''''''''''''''''''X
                ElseIf Left(ddlStatusChSt.Text, 1) = "S" Then
                    'Dim commandX1 As MySqlCommand = New MySqlCommand

                    'commandX1.CommandType = CommandType.Text
                    'commandX1.CommandText = "select 1 from tblServiceRecord where (Status='O' or Status='' or Status is null) and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                    'commandX1.Connection = conn

                    'Dim drserviceX As MySqlDataReader = commandX1.ExecuteReader()
                    'Dim dtserviceX As New DataTable
                    'dtserviceX.Load(drserviceX)

                    'If dtserviceX.Rows.Count > 0 Then
                    '    Dim commandX2 As MySqlCommand = New MySqlCommand
                    '    commandX2.CommandType = CommandType.Text
                    '    'Exit Sub
                    '    commandX2.CommandText = "update  tblServiceRecord set Status='V' where (Status='O' or Status='' or Status is null)  and SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                    '    commandX2.Connection = conn
                    '    commandX2.ExecuteNonQuery()
                    'End If

                    Dim commandX As MySqlCommand = New MySqlCommand
                    commandX.CommandType = CommandType.Text

                    Dim qryX As String = "UPDATE tblProject SET  Status ='S', ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "

                    commandX.CommandText = qryX
                    commandX.Parameters.Clear()


                    If txtActualEndChSt.Text = "" Then
                        commandX.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
                    Else
                        'strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
                        commandX.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
                    End If

                    commandX.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)
                    commandX.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
                    commandX.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
                    commandX.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                    commandX.Connection = conn
                    commandX.ExecuteNonQuery()
                    'Dim message As String = "alert('Contract Cancelled successfully!!!!!!')"
                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                    conn.Close()
                    'Dim messageX As String = "alert('Contract Updated successfully!!!')"
                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", messageX, True)

                    GridView1.DataBind()
                    'txtServInst.Text = txtCommentChSt.Text
                    txtStatus.Text = "S"


                    ''''''''''''''''''X


                    ''''''O
                ElseIf Left(ddlStatusChSt.Text, 1) = "O" Then
                    'Dim command1 As MySqlCommand = New MySqlCommand

                    'command1.CommandType = CommandType.Text
                    'command1.CommandText = "select 1 from tblServiceRecord where (Status <> 'O') and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                    'command1.Connection = conn

                    'Dim drserviceE As MySqlDataReader = command1.ExecuteReader()
                    'Dim dtserviceE As New DataTable
                    'dtserviceE.Load(drserviceE)

                    'If dtserviceE.Rows.Count > 0 Then
                    '    Dim command2 As MySqlCommand = New MySqlCommand
                    '    command2.CommandType = CommandType.Text

                    '    Dim qry1 As String = "update  tblServiceRecord set Status= @status where  SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"

                    '    command2.CommandText = qry1
                    '    command2.Parameters.Clear()

                    '    If ddlStatusChSt.SelectedIndex <> "-1" Then
                    '        command2.Parameters.AddWithValue("@Status", Left(ddlStatusChSt.Text, 1))
                    '    Else
                    '        command2.Parameters.AddWithValue("@Status", txtStatus.Text)
                    '    End If

                    '    command2.Connection = conn
                    '    command2.ExecuteNonQuery()
                    'End If

                    ''''''''''' Contract

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text

                    Dim qryE As String = "UPDATE tblProject SET  Status = @status,  ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "


                    command.CommandText = qryE
                    command.Parameters.Clear()

                    If ddlStatusChSt.SelectedIndex <> "-1" Then
                        command.Parameters.AddWithValue("@Status", Left(ddlStatusChSt.Text, 1))
                    Else
                        command.Parameters.AddWithValue("@Status", txtStatus.Text)
                    End If

                    'If ddlRenewalStatus.SelectedIndex <> "0" Then
                    '    command.Parameters.AddWithValue("@RenewalSt", Left(ddlRenewalStatus.Text, 1))
                    'Else
                    '    command.Parameters.AddWithValue("@RenewalSt", txtRs.Text)
                    'End If



                    If txtActualEndChSt.Text = "" Then
                        command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
                    Else
                        'strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
                        command.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
                    End If


                    command.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)
                    command.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
                    command.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                    command.Connection = conn
                    command.ExecuteNonQuery()
                    ''Dim message As String = "alert('Contract Status Changed successfully!!!!!!')"
                    ''ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                    conn.Close()
                    'Dim messageE As String = "alert('Contract Updated successfully!!!')"
                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", messageE, True)


                    GridView1.DataBind()
                    'txtServInst.Text = txtCommentChSt.Text
                    txtStatus.Text = Left(ddlStatusChSt.Text, 1)

                    'If ddlRenewalStatus.SelectedIndex <> "0" Then
                    '    txtRs.Text = Left(ddlRenewalStatus.Text, 1)
                    'End If

                    ''''''''''' Contrct

                Else
                    'Dim command1 As MySqlCommand = New MySqlCommand

                    'command1.CommandType = CommandType.Text
                    'command1.CommandText = "select 1 from tblServiceRecord where (Status='O') and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
                    'command1.Connection = conn

                    'Dim drserviceE As MySqlDataReader = command1.ExecuteReader()
                    'Dim dtserviceE As New DataTable
                    'dtserviceE.Load(drserviceE)

                    'If dtserviceE.Rows.Count > 0 Then
                    '    Dim command2 As MySqlCommand = New MySqlCommand
                    '    command2.CommandType = CommandType.Text

                    '    Dim qry1 As String = "update  tblServiceRecord set Status= @status where  SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"

                    '    command2.CommandText = qry1
                    '    command2.Parameters.Clear()

                    '    If ddlStatusChSt.SelectedIndex <> "-1" Then
                    '        command2.Parameters.AddWithValue("@Status", Left(ddlStatusChSt.Text, 1))
                    '    Else
                    '        command2.Parameters.AddWithValue("@Status", txtStatus.Text)
                    '    End If

                    '    command2.Connection = conn
                    '    command2.ExecuteNonQuery()
                    'End If



                    ''Contract

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text

                    Dim qryE As String = "UPDATE tblProject SET  Status = @status,  ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "


                    command.CommandText = qryE
                    command.Parameters.Clear()

                    If ddlStatusChSt.SelectedIndex <> "-1" Then
                        command.Parameters.AddWithValue("@Status", Left(ddlStatusChSt.Text, 1))
                    Else
                        command.Parameters.AddWithValue("@Status", txtStatus.Text)
                    End If

                    'If ddlRenewalStatus.SelectedIndex <> "0" Then
                    '    command.Parameters.AddWithValue("@RenewalSt", Left(ddlRenewalStatus.Text, 1))
                    'Else
                    '    command.Parameters.AddWithValue("@RenewalSt", txtRs.Text)
                    'End If



                    If txtActualEndChSt.Text = "" Then
                        command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
                    Else
                        'strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
                        command.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
                    End If


                    command.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)
                    command.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
                    command.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                    command.Connection = conn
                    command.ExecuteNonQuery()
                    ''Dim message As String = "alert('Contract Status Changed successfully!!!!!!')"
                    ''ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                    conn.Close()
                    'Dim messageE As String = "alert('Contract Updated successfully!!!')"
                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", messageE, True)


                    GridView1.DataBind()
                    'txtServInst.Text = txtCommentChSt.Text
                    txtStatus.Text = Left(ddlStatusChSt.Text, 1)

                    'If ddlRenewalStatus.SelectedIndex <> "0" Then
                    '    txtRs.Text = Left(ddlRenewalStatus.Text, 1)
                    'End If



                End If '  If Left(ddlStatusChSt.Text, 1) = "E" Then

                ddlStatusChSt.SelectedIndex = 0
                txtActualEndChSt.Text = ""
                txtCommentChSt.Text = ""

                GridView1.DataBind()
                ModalPopupExtender5.Hide()
                lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                lblAlert.Text = ""
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PROJECT", txtContractNo.Text, "CHST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountId.Text, "", txtRcno.Text)
            Else
                ModalPopupExtender5.Show()
            End If

InvalidStatus:
            txtIsPopup.Text = "N"
            If txtChangeStatus.Text = "000" Then

                lblAlertStatus.Text = "SELECTED STATUS IS SAME AS CURRENT STATUS"
                Exit Sub
            ElseIf txtChangeStatus.Text = "001" Then
                lblAlertStatus.Text = "CONTRACT STATUS SHOULD BE [O-OPEN]"
                Exit Sub
            ElseIf txtChangeStatus.Text = "002" Then
                lblAlertStatus.Text = "PLEASE SELECT VALID STATUS"
                Exit Sub
            ElseIf txtChangeStatus.Text = "003" Then
                lblAlertStatus.Text = "ACTUAL END DATE CANNOT BE BLANK"
                Exit Sub
            End If
            'txtIsPopup.Text = "N"
            ModalPopupExtender5.Hide()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub



    Protected Sub RetrieveLocation()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblpostaltolocation where postalbeginwith=@postalbeginwith"
        command1.Parameters.AddWithValue("@postalbeginwith", txtPostal.Text.Substring(0, 2))
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("LocateGRP").ToString <> "" Then
                ddlLocateGrp.Text = dt.Rows(0)("LocateGRP").ToString
            End If
        End If

    End Sub
    Protected Sub txtPostal_TextChanged(sender As Object, e As EventArgs) Handles txtPostal.TextChanged
        Try
            If String.IsNullOrEmpty(txtPostal.Text) = False Then
                RetrieveLocation()
            End If
        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub


    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        lblAlert.Text = ""
        If String.IsNullOrEmpty(ddlContactType.Text) Or ddlContactType.Text = "--SELECT--" Then
            '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
            lblAlert.Text = "SELECT CUSTOMER TYPE TO PROCEED"
            Exit Sub
        End If

        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
            txtContType1.Text = "CORPORATE"
            txtContType2.Text = "COMPANY"
            txtContType3.Text = "CORPORATE"
            txtContType4.Text = "COMPANY"
        Else
            txtContType1.Text = "RESIDENTIAL"
            txtContType2.Text = "PERSON"
            txtContType3.Text = "RESIDENTIAL"
            txtContType4.Text = "PERSON"
        End If


        If String.IsNullOrEmpty(txtOriginalAccountId.Text) = False Then
            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then

            Else
                Exit Sub
            End If
        End If

        'Exit Sub

        'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"

        If String.IsNullOrEmpty(txtAccountId.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountId.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text

            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman,Industry From tblCompany where 1=1 and status = 'O' and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contactperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman, '' as Industry From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
        Else

            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman, Industry From tblCompany where 1=1 and status = 'O' order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman, '' as Industry From tblPERSON where 1=1 and status = 'O' order by name"
            End If


            SqlDSClient.DataBind()
            gvClient.DataBind()
        End If
        mdlPopUpClient.Show()
    End Sub

    Protected Sub IsContractDateHoliday()
        Dim conn As MySqlConnection = New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text
        command1.CommandText = "SELECT * FROM tblHoliday where Holiday= '" & Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd") & "'"
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("Holiday").ToString <> "" Then
                txtContractStart.ForeColor = Drawing.Color.Red
            End If

        Else
            txtContractStart.ForeColor = Drawing.Color.Black
        End If


        'If dt.Rows.Count > 0 Then
        '    If dt.Rows(0)("Holiday").ToString <> "" Then
        '        txtContractStart.ForeColor = Drawing.Color.Red
        '    Else
        '        txtContractStart.ForeColor = Drawing.Color.Black
        '    End If
        'End If
        command1.Dispose()
    End Sub


    Protected Sub txtContractStart_TextChanged(sender As Object, e As EventArgs) Handles txtContractStart.TextChanged
        IsContractDateHoliday()

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblAlert.Text = ""
        Try
            If txtStatus.Text = "P" Then
                lblAlert.Text = "Project has already been POSTED.. CANNOT BE DELETED"
                'Dim message1 As String = "alert('Contract has already been POSTED.. Cannot be DELETED!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message1, True)

                Exit Sub
            End If

            If txtStatus.Text = "V" Then
                lblAlert.Text = "Project is VOID.. CANNOT BE DELETED"
                'Dim message2 As String = "alert('Contract is VOID.. Cannot be DELETED!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message2, True)

                Exit Sub
            End If

            ''''''''''''''''''''''''''''
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandProjectCode As MySqlCommand = New MySqlCommand
            commandProjectCode.CommandType = CommandType.Text
            commandProjectCode.CommandText = "SELECT ProjectCode FROM tblContract where ProjectCode = @ProjectCode"
            commandProjectCode.Parameters.AddWithValue("@ProjectCode", txtContractNo.Text.Trim)
            commandProjectCode.Connection = conn

            Dim drProjectCode As MySqlDataReader = commandProjectCode.ExecuteReader()
            Dim dtProjectCode As New DataTable
            dtProjectCode.Load(drProjectCode)

            If dtProjectCode.Rows.Count > 0 Then
                lblAlert.Text = "PROJECT IS IN USE.. CANNOT BE DELETED"
                Exit Sub
            End If


            ''''''''''''''''''''''''''''

            'If txtGS.Text = "P" Then
            '    lblAlert.Text = "CONTRACT HAS ALREADY BEEN SCHEDULED.. CANNOT BE DELETED"
            '    Exit Sub
            'End If

            lblAlert.Text = ""
            lblMessage.Text = "ACTION: DELETE RECORD"

            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then


                ''''''''''''''''''''''''



                '''''''''''''''''''''''''


                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text

                Dim qry1 As String = "DELETE from tblProject where ProjectNo= @ContractNo "

                command1.CommandText = qry1
                command1.Parameters.Clear()
                command1.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                command1.Connection = conn
                command1.ExecuteNonQuery()

                'Dim command2 As MySqlCommand = New MySqlCommand
                'command2.CommandType = CommandType.Text

                'Dim qry2 As String = "DELETE from tblContractDet where ContractNo= @ContractNo "

                'command2.CommandText = qry2
                'command2.Parameters.Clear()
                'command2.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                'command2.Connection = conn
                'command2.ExecuteNonQuery()

                'Dim command3 As MySqlCommand = New MySqlCommand
                'command3.CommandType = CommandType.Text

                'Dim qry3 As String = "DELETE from tblContractServingTarget where ContractNo= @ContractNo "

                'command3.CommandText = qry3
                'command3.Parameters.Clear()
                'command3.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                'command3.Connection = conn
                'command3.ExecuteNonQuery()

                'Dim command4 As MySqlCommand = New MySqlCommand
                'command4.CommandType = CommandType.Text

                'Dim qry4 As String = "DELETE from tblServiceContractFrequency where ContractNo= @ContractNo "

                'command4.CommandText = qry4
                'command4.Parameters.Clear()
                'command4.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                'command4.Connection = conn
                'command4.ExecuteNonQuery()

                conn.Close()

                'Dim message As String = "alert('Contract is deleted Successfully!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PROJECT", txtContractNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountId.Text, "", txtRcno.Text)

                btnADD_Click(sender, e)

                'txt.Text = "SELECT * From tblContract where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"
                SQLDSContract.SelectCommand = txt.Text
                SQLDSContract.DataBind()
                'GridView1.DataSourceID = "SqlDSContract"

                GridView1.DataBind()

            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString

            lblAlert.Text = exstr
            'Dim message As String = "alert('" + exstr + "!!!')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)


        End Try
    End Sub


    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting

        If GridSelected = "SQLDSContract" Then
            SQLDSContract.SelectCommand = txt.Text
            SQLDSContract.DataBind()
        ElseIf GridSelected = "SQLDSContractClientId" Then
            'SqlDataSource1.SelectCommand = txt.Text
            SQLDSContractClientId.DataBind()
        ElseIf GridSelected = "SQLDSContractNo" Then
            ''SqlDataSource1.SelectCommand = txt.Text
            SqlDSContractNo.DataBind()
        End If

        GridView1.DataBind()
    End Sub



    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        'txtActualEndChSt.Text = Date()
        MakeSearchNull()
        lblAlert.Text = ""
        ModalPopupExtender5.Show()

    End Sub


    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Session("serviceschedulefrom") = ""
        Session("contractno") = ""
        Session("contractdetailfrom") = ""

        lblAccountIdContact.Visible = False
        'lblAccountIdContact1.Text = ""

        txt.Text = "SELECT * From tblProject where (1=1)  and (Status ='O' or Status ='P')  order by rcno desc, CustName limit 50"
        SQLDSContract.SelectCommand = txt.Text
        SQLDSContract.DataBind()
        GridView1.DataSourceID = "SqlDSContract"

        GridView1.DataBind()
        MakeSearchNull()

    End Sub



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtMode.Text = ""
        MakeMeNull()
        DisableControls()

        '   txt.Text = "select * from tblasset where rcno<>0;"
        'SqlDataSource1.SelectCommand = "SELECT * FROM tblcompany WHERE  Inactive=0 order by AccountId desc limit 100;"
        'SqlDataSource1.DataBind()
        SQLDSContract.DataBind()
        GridView1.DataBind()
    End Sub


    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        lblMessage.Text = ""
        'txtMode.Text = ""
        If txtStatus.Text = "O" Then
            'btnSave.Enabled = True
        Else
            'btnSave.Enabled = False
            lblAlert.Text = "PROJECT STATUS SHOULD BE [O-OPEN]"
            Exit Sub
        End If

        'If txtGS.Text = "P" Then
        '    lblAlert.Text = "CONTRACT HAS ALREADY BEEN SCHEDULED"
        '    Exit Sub
        'End If

        If txtRcno.Text = "" Then
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return
        End If
        'DisableControls()
        'txtMode.Text = "EDIT"
        lblMessage.Text = "ACTION: EDIT RECORD"

        txtMode.Text = "EDIT"
        EnableControls()
        txtOriginalAccountId.Text = txtAccountId.Text
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black


        '''''' User Access

        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        command.CommandType = CommandType.Text
        command.CommandText = "SELECT 1207EditAgreeValue FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
        command.Connection = conn

        Dim dr1 As MySqlDataReader = command.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then
            If Convert.ToBoolean(dt1.Rows(0)("1207EditAgreeValue")) = False Then
                'If dt.Rows(0)("1207EditAgreeValue").ToString() = False Then
                'Me.btnADD.Enabled = dt.Rows(0)("1207EditAgreeValue").ToString()
                txtConDetVal.Attributes.Add("readonly", "readonly")
                txtAgreeVal.Attributes.Add("readonly", "readonly")
                'txtDisPercent.Attributes.Add("readonly", "readonly")
                'txtDisAmt.Attributes.Add("readonly", "readonly")

                txtConDetVal.BackColor = txtContractNo.BackColor
                txtAgreeVal.BackColor = txtContractNo.BackColor
                'txtDisPercent.BackColor = txtContractNo.BackColor
                'txtDisAmt.BackColor = txtContractNo.BackColor
            End If

        End If

        '''''' User Access


        Dim commandProjectCode As MySqlCommand = New MySqlCommand
        commandProjectCode.CommandType = CommandType.Text
        commandProjectCode.CommandText = "SELECT ProjectCode FROM tblContract where ProjectCode = @ProjectCode"
        commandProjectCode.Parameters.AddWithValue("@ProjectCode", txtContractNo.Text.Trim)
        commandProjectCode.Connection = conn

        Dim drProjectCode As MySqlDataReader = commandProjectCode.ExecuteReader()
        Dim dtProjectCode As New DataTable
        dtProjectCode.Load(drProjectCode)

        If dtProjectCode.Rows.Count > 0 Then
            txtProjectName.Enabled = False
        Else
            txtProjectName.Enabled = True
        End If
    End Sub


    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientSearch.Click
        'If txtPopUpClient.Text.Trim = "" Then
        '    MessageBox.Message.Alert(Page, "Please enter client name", "str")
        'Else
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"

        '    SqlDSClient.DataBind()
        '    gvClient.DataBind()
        '    mdlPopUpClient.Show()
        'End If
    End Sub

    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging

        gvClient.PageIndex = e.NewPageIndex

        'If txtPopUpClient.Text.Trim = "" Then
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster "
        'Else
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
        'End If
        'SqlDSClient.DataBind()
        'gvClient.DataBind()
        'mdlPopUpClient.Show()


        'If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and  (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "' or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and  (Upper(ContType) = '" + txtContType2.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"


        'Else
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "' or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') and (Upper(ContType) = '" + txtContType2.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"
        'End If


        If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where status = 'O' order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where status = 'O' order by name"
            End If
        Else
            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' and (upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
            End If

            'SqlDSClient.DataBind()
            'gvClient.DataBind()
            'mdlPopUpClient.Show()
        End If

        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
        'End If

    End Sub



    Protected Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click

        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            lblAlert.Text = "SELECT RECORD TO COPY"
            Return

        End If
        'DisableControls()
        'txtMode.Text = "EDIT"
        lblMessage.Text = "ACTION: COPY RECORD"

        txtMode.Text = "NEW"
        EnableControls()
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black


        'If Not (txtStatus.Text = "O" Or txtStatus.Text = "P" Or txtStatus.Text = "E" Or txtStatus.Text = "T" Or txtStatus.Text = "C") Then
        '    Dim message As String = "alert('Contract Status should be [O/C/E/P/T]!!!')"
        '    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        '    Exit Sub
        'End If



        txtContractNo.Text = ""
        txtContractDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtConDetVal.Text = "0.00"
        txtAgreeVal.Text = "0.00"
        txtContractStart.Text = Now.Date.ToString("dd/MM/yyyy")

        'txtContractEnd.Text = ""
        txtServStart.Text = Now.Date.ToString("dd/MM/yyyy")
        txtServStartDay.Text = DateTime.Parse(txtServStart.Text).DayOfWeek.ToString().ToUpper
        'txtServEnd.Text = ""
        'txtServEndDay.Text = ""
        'TxtServEndDay.Text = DateTime.Parse(txtServEnd.Text).DayOfWeek.ToString()
        'txtServTimeIn.Text = ""
        'txtServTimeOut.Text = ""
        'txtAllocTime.Text = "0"
        ''txtWarrStart.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtWarrEnd.Text = ""
        'txtValPerMnth.Text = ""
        'txtContractNotes.Text = ""
        'txtServInst.Text = ""
        txtStatus.Text = "O"
        'txtRs.Text = "O"
        'txtNS.Text = "O"
        'txtProspectId.Text = ""
        'txtGS.Text = "O"
        'txtRemarks.Text = ""
        'txtPrintBody.Text = ""
        'txtClient.Text = ""
        'txtContactPerson.Text = ""

        'txtPortfolioValue.Text = "0.00"

        'txtServiceNo.Text = "0.00"
        'txtServiceAmt.Text = "0.00"
        'txtServiceNoActual.Text = "0.00"
        'txtServiceBal.Text = "0.00"
        'txtServiceAmtActual.Text = "0.00"
        'txtTotalArea.Text = "0.00"
        'txtCompletedArea.Text = "0.00"
        'txtBalanceArea.Text = "0.00"
        'txtServiceAmtBal.Text = "0.00"

        'txtBillingAmount.Text = "0.00"
        txtRetentionPerc.Text = "0"
        txtRetentionValue.Text = "0.00"
    End Sub



    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter client name", "str")
        Else
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            'SqlDSClient.SelectCommand = "SELECT ContType, AccountID, ContID, ContName, ContPerson, ContAddress1, ContHP, ContEmail,  ContLocationGroup, ContGroup, ContAddBlock, ContAddNos, ContAddFloor, ContAddUnit, ContAddStreet, ContAddBuilding, ContAddCity, ContAddState, ContAddCountry, ContAddPostal, ContFax, Mobile, ContTel, ContSales From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"


            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman, Industry From tblCompany where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '%" + txtPopUpClient.Text + "%' or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman, '' as Industry From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '%" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            txtIsPopup.Text = "Client"
        End If

        'txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
    End Sub

    Protected Sub btnQuickSearch_Click(sender As Object, e As EventArgs) Handles btnQuickSearch.Click
        Try
            Dim strsql As String

            strsql = "Select Status,  ProjectNo, ProjectName,  ContractDate, AccountId, CustName, CustAddr, InchargeId, AgreeValue, StartDate, EndDate, ActualEnd, "
            strsql = strsql + "Scheduler, ContractGroup, ServiceAddress, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno from tblProject where 1=1 "

            txtIsPopup.Text = "N"

            '' /Status : Check filtering criteria for Status
            'If chkSearchAll.Checked = False Then
            '    If chkSearchOpen.Checked = True Then
            '        vStrStatus = "AND (Status = 'O' OR Status = '' OR Status IS NULL "
            '    End If

            '    If chkSearchCompleted.Checked = True Then
            '        If vStrStatus = "" Then
            '            vStrStatus = "AND (Status = 'C' "
            '        Else
            '            vStrStatus = vStrStatus & "OR Status = 'C' "
            '        End If
            '    End If

            '    If chkSearchPost.Checked = True Then
            '        If vStrStatus = "" Then
            '            vStrStatus = "AND (Status = 'P' "
            '        Else
            '            vStrStatus = vStrStatus & "OR Status = 'P' "
            '        End If
            '    End If

            '    If chkSearchEarlyComplete.Checked = True Then
            '        If vStrStatus = "" Then
            '            vStrStatus = "AND (Status = 'E' "
            '        Else
            '            vStrStatus = vStrStatus & "OR Status = 'E' "
            '        End If
            '    End If

            '    If chkSearchVoid.Checked = True Then
            '        If vStrStatus = "" Then
            '            vStrStatus = "AND (Status = 'V' "
            '        Else
            '            vStrStatus = vStrStatus & "OR Status = 'V' "
            '        End If
            '    End If

            '    If chkSearchTerminated.Checked = True Then
            '        If vStrStatus = "" Then
            '            vStrStatus = "AND (Status = 'T' "
            '        Else
            '            vStrStatus = vStrStatus & "OR Status = 'T' "
            '        End If
            '    End If

            '    If chkSearchCancelled.Checked = True Then
            '        If vStrStatus = "" Then
            '            vStrStatus = "AND (Status = 'X' "
            '        Else
            '            vStrStatus = vStrStatus & "OR Status = 'X' "
            '        End If
            '    End If


            '    If chkSearchRevised.Checked = True Then
            '        If vStrStatus = "" Then
            '            vStrStatus = "AND (Status = 'R' "
            '        Else
            '            vStrStatus = vStrStatus & "OR Status = 'R' "
            '        End If
            '    End If


            '    If chkSearchOnHold.Checked = True Then
            '        If vStrStatus = "" Then
            '            vStrStatus = "AND (Status = 'H' "
            '        Else
            '            vStrStatus = vStrStatus & "OR Status = 'H' "
            '        End If
            '    End If

            '    If vStrStatus <> "" Then
            '        vStrStatus = vStrStatus & ") "
            '    End If
            'End If '/Status


            'If chkSearchOpen.Checked = True Then
            '    strsql = strsql & " and (status = 'O')"
            'End If




            If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
                Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                For Each str As String In stringList
                    str = "'" + str + "'"
                    YrStrList.Add(str.ToUpper)
                Next

                Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                strsql = strsql + " and status in (" + YrStr + ")"

            End If

            'strsql = strsql & vStrStatus

            If String.IsNullOrEmpty(txtContractnoSearch.Text) = False Then
                strsql = strsql & " and projectno like '%" & txtContractnoSearch.Text.Trim + "%'"
            End If

            If String.IsNullOrEmpty(txtProjectNameSearch.Text) = False Then
                strsql = strsql & " and ProjectName like '%" & txtProjectNameSearch.Text.Trim + "%'"
            End If


            'If String.IsNullOrEmpty(txtInchargeSearch.Text) = False Then
            '    strsql = strsql & " and InChargeId like '%" & txtInchargeSearch.Text.Trim + "%'"
            'End If

            If String.IsNullOrEmpty(txtAccountIdSearch.Text) = False Then
                strsql = strsql & " and (CustCode like '%" & txtAccountIdSearch.Text & "%' or AccountId like '%" & txtAccountIdSearch.Text & "%')"
            End If

            If String.IsNullOrEmpty(txtClientNameSearch.Text) = False Then
                strsql = strsql & " and CustName like ""%" & txtClientNameSearch.Text & "%"""
            End If

            strsql = strsql + " order by custname;"
            txt.Text = strsql

            SQLDSContract.SelectCommand = ""
            SQLDSContract.SelectCommand = strsql
            SQLDSContract.DataBind()

            GridView1.DataSourceID = "SQLDSContract"
            GridView1.DataBind()
            GridView1.Visible = True
            GridSelected = "SQLDSContract"

            lblAccountIdContact.Visible = False
            lblAccountIdContact1.Visible = False
            'lblAccountIdContact1.Text = ""

            lblAccountIdContactLocation.Visible = False
            lblAccountIdContactLocation1.Visible = False
            'lblAccountIdContactLocation1.Text = ""

            lblMessage.Text = ""
            lblMessage.Text = "NUMBER OF RECORDS FOUND : " + txtRowCount.Text
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = exstr
            'Dim message As String = "alert('" + exstr + "')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        End Try
    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        MakeSearchNull()
        ModalPopupExtender1.Show()
        'txtRemarks.Text = txtSearchID.Text

    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        lblAlert.Text = ""

        txtContType1.Text = "CORPORATE"
        txtContType2.Text = "COMPANY"
        txtContType3.Text = "RESIDENTIAL"
        txtContType4.Text = "PERSON"

        mdlPopUpClient.Show()
    End Sub


    Protected Sub btnSearch1Status_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch1Status.Click
        If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
            Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
            Dim YrStrList As List(Of [String]) = New List(Of String)()

            For Each str As String In stringList
                For Each item As ListItem In chkStatusSearch.Items
                    If item.Value = str Then
                        item.Selected = True
                    End If
                Next
            Next


        End If
        mdlPopupStatusSearch.Show()
    End Sub

    'Protected Sub rdbStatusSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdbStatusSearch.SelectedIndexChanged
    '    If rdbStatusSearch.SelectedValue = "ALL" Then
    '        chkStatusSearch.ClearSelection()
    '        chkStatusSearch.Enabled = False
    '    Else
    '        chkStatusSearch.Enabled = True
    '    End If
    '    mdlPopupStatusSearch.Show()

    'End Sub

    Protected Sub btnStatusSearch_Click(sender As Object, e As EventArgs) Handles btnStatusSearch.Click
        Try
            Dim YrStrList As List(Of [String]) = New List(Of String)()

            'If rdbStatusSearch.SelectedValue = "ALL" Then
            '    For Each item As ListItem In chkStatusSearch.Items
            '        YrStrList.Add(item.Value)
            '    Next
            'Else
            For Each item As ListItem In chkStatusSearch.Items
                If item.Selected Then
                    YrStrList.Add(item.Value)
                End If
            Next



            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

            If String.IsNullOrEmpty(YrStr) = True Then
                Dim message As String = "alert('PLEASE SELECT A STATUS')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                mdlPopupStatusSearch.Show()
                Exit Sub
            End If

            txtSearch1Status.Text = YrStr
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = exstr
            'Dim message As String = "alert('" + exstr + "')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        End Try
    End Sub

    Protected Sub btnQuickReset_Click(sender As Object, e As EventArgs) Handles btnQuickReset.Click
        txtContractnoSearch.Text = ""
        txtAccountIdSearch.Text = ""
        txtProjectNameSearch.Text = ""
        txtInchargeSearch.Text = ""
        txtClientNameSearch.Text = ""
        txtSearch1Status.Text = "O,P,H,R"
        btnQuickSearch_Click(sender, e)
    End Sub


    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            Session("contractno") = txtContractNo.Text

            ''''''''''''''''''''''''''''''
            If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
                Session("searchstatus") = txtSearch1Status.Text
            End If

            If String.IsNullOrEmpty(txtProjectNameSearch.Text) = False Then
                Session("searchteam") = txtProjectNameSearch.Text
            End If

            If String.IsNullOrEmpty(txtInchargeSearch.Text) = False Then
                Session("searchincharge") = txtInchargeSearch.Text
            End If

            If String.IsNullOrEmpty(txtAccountIdSearch.Text) = False Then
                Session("searchaccountid") = txtAccountIdSearch.Text
            End If

            If String.IsNullOrEmpty(txtClientNameSearch.Text) = False Then
                Session("searchclientname") = txtClientNameSearch.Text
            End If

            ''''''''''''''''''''''''''''''

            Session("gridsql") = txt.Text
            Session("rcno") = txtRcno.Text
            'Session("accountid") = txtAccountIdSearch.Text
        End If

        'Session("contractno") = txtContractNo.Text
        'Session("accountid") = txtAccountIdSearch.Text
        Session("contractdetailfrom") = "contract"
        'Session("sqlquery") = txt.Text
        'Session("rcno") = txtRcno.Text
        'Response.Redirect("ContractDetail.aspx")
        Response.Redirect("RV_ContractServiceSchedule.aspx")
    End Sub

    Protected Sub txtContractDate_TextChanged(sender As Object, e As EventArgs) Handles txtContractDate.TextChanged
        IsContractDateHoliday()
        IsServiceDateHoliday()
    End Sub


    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
        'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "'  or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"

        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
            SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman, Industry From tblCompany where 1=1 and status ='O' order by name"
        ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
            SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman, '' as Industry From tblPERSON where 1=1 and status ='O' order by name"
        End If
        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
        txtIsPopup.Text = "Client"
    End Sub

    Protected Sub SQLDSContract_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SQLDSContract.Selected
        txtRowCount.Text = e.AffectedRows.ToString
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        'System.Threading.Thread.Sleep(5000)
        'Label50.Visible = True
        lblAlert.Text = ""

        If txtContType2.Text.Trim = txtContType4.Text.Trim Then
            If txtStatus.Text = "O" Then

                txtAccountId.Text = ""
                txtClient.Text = ""
                txtCustName.Text = ""
                'txtContactPerson.Text = ""
                txtOfficeAddress.Text = ""
                'txtTelephone.Text = ""

                ddlCompanyGrp.SelectedIndex = 0
                ddlSalesman.SelectedIndex = 0
                'txtFax.Text = ""
                'txtConPerMobile.Text = ""
                txtPostal.Text = ""

                If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    txtAccountId.Text = ""
                Else
                    txtAccountId.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                    txtClient.Text = ""
                Else
                    txtClient.Text = gvClient.SelectedRow.Cells(2).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                    txtCustName.Text = ""
                Else
                    txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
                End If

                'If (gvClient.SelectedRow.Cells(4).Text = "&nbsp;") Then
                '    txtContactPerson.Text = ""
                'Else
                '    txtContactPerson.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(4).Text.Trim)
                'End If


                'If (gvClient.SelectedRow.Cells(6).Text = "&nbsp;") Then
                '    txtAddress.Text = ""
                'Else
                '    txtAddress.Text = gvClient.SelectedRow.Cells(6).Text.Trim
                'End If

                If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
                    txtOfficeAddress.Text = ""
                Else
                    txtOfficeAddress.Text = gvClient.SelectedRow.Cells(5).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(14).Text = "&nbsp;") Then
                Else
                    txtOfficeAddress.Text = txtOfficeAddress.Text + ", " + gvClient.SelectedRow.Cells(14).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(15).Text = "&nbsp;") Then
                Else
                    txtOfficeAddress.Text = txtOfficeAddress.Text + ", " + gvClient.SelectedRow.Cells(15).Text.Trim
                End If

                'If (gvClient.SelectedRow.Cells(6).Text = "&nbsp;") Then
                '    txtTelephone.Text = ""
                'Else
                '    txtTelephone.Text = gvClient.SelectedRow.Cells(6).Text.Trim
                'End If

                If (gvClient.SelectedRow.Cells(9).Text = "&nbsp;") Then
                    ddlCompanyGrp.Text = ""
                Else
                    ddlCompanyGrp.Text = gvClient.SelectedRow.Cells(9).Text.Trim
                End If



                'If (gvClient.SelectedRow.Cells(20).Text = "&nbsp;") Then
                '    txtFax.Text = ""
                'Else
                '    txtFax.Text = gvClient.SelectedRow.Cells(20).Text.Trim
                'End If

                'If (gvClient.SelectedRow.Cells(10).Text = "&nbsp;") Then
                '    txtConPerMobile.Text = ""
                'Else
                '    txtConPerMobile.Text = gvClient.SelectedRow.Cells(10).Text.Trim
                'End If

                If (gvClient.SelectedRow.Cells(19).Text = "&nbsp;") Then
                    txtPostal.Text = ""
                Else
                    txtPostal.Text = gvClient.SelectedRow.Cells(19).Text.Trim
                End If

                Dim lsalesman As String

                lsalesman = gvClient.SelectedRow.Cells(23).Text

                If String.IsNullOrEmpty(lsalesman) = False Then

                    'gSalesman = dt.Rows(0)("SalesMan").ToString.ToUpper()

                    If ddlSalesman.Items.FindByValue(lsalesman) Is Nothing Then
                        ddlSalesman.Items.Add(lsalesman)
                        ddlSalesman.Text = lsalesman
                    Else
                        ddlSalesman.Text = lsalesman
                    End If
                Else
                    ddlSalesman.SelectedIndex = 0
                End If

                'txtRemarks.Text = gvClient.SelectedRow.Cells(24).Text.Trim
                'If (gvClient.SelectedRow.Cells(24).Text = "&nbsp;") Or String.IsNullOrEmpty(gvClient.SelectedRow.Cells(24).Text) = True Then
                '    ddlIndustry.Text = ""
                'Else
                '    ddlIndustry.Text = gvClient.SelectedRow.Cells(24).Text.Trim
                'End If

                'ddlIndustry.SelectedItem.Text = lIndustry

                'If String.IsNullOrEmpty(ddlIndustry.Text) = False Then

                '    FindMarketSegmentID()

                'End If


                'If dt.Rows(0)("SalesMan").ToString <> "" Then

                '     gSalesman = dt.Rows(0)("SalesMan").ToString.ToUpper()

                '     If ddlSalesman.Items.FindByValue(gSalesman) Is Nothing Then
                '         ddlSalesman.Items.Add(gSalesman)
                '         ddlSalesman.Text = gSalesman
                '     Else
                '         ddlSalesman.Text = dt.Rows(0)("SalesMan").ToString.Trim().ToUpper()
                '     End If
                ' End If



                'If (gvClient.SelectedRow.Cells(23).Text = "&nbsp;") Then
                '    ddlSalesman.SelectedIndex = 0
                'Else
                '    ddlSalesman.Text = gvClient.SelectedRow.Cells(23).Text.Trim.ToUpper
                'End If
                txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
                If String.IsNullOrEmpty(txtPostal.Text) = False Then
                    RetrieveLocation()
                End If

            Else
                lblAlert.Text = "Account Id can be selected only for Open Projects"
            End If
        Else


            If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                txtAccountIdSearch.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            Else
                txtAccountIdSearch.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            End If

            If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                txtClientNameSearch.Text = ""
            Else
                txtClientNameSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
            End If
        End If
        txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
        mdlPopUpClient.Hide()
        'Dim lblidlocation As ImageButton = CType(grvServiceLocation.Rows(0).FindControl("BtnLocation"), ImageButton)
        'lblidlocation.Visible = False
    End Sub




    Protected Sub tb1_ActiveTabChanged(sender As Object, e As EventArgs) Handles tb1.ActiveTabChanged

        'If String.IsNullOrEmpty(txtContractNo.Text) = True Then
        '    lblAlert.Text = "Please Select a Contract to proceed."
        '    tb1.TabIndex = 0
        '    Exit Sub
        'End If
        lblAlert.Text = ""

        If tb1.ActiveTabIndex = 1 Then
            If txtMode.Text = "Add" Or txtMode.Text = "Edit" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 0
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtContractNo.Text) Then
                lblAlert.Text = "Select a Project to proceed"
                tb1.ActiveTabIndex = 0
                Exit Sub
            End If


            'GridView1.Visible = False


            'SASI - 17/04/2017
        ElseIf tb1.ActiveTabIndex = 2 Then
            If txtMode.Text = "Add" Or txtMode.Text = "Edit" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                tb1.ActiveTabIndex = 0
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtContractNo.Text) Then
                lblAlert.Text = "Select a Project to proceed"
                tb1.ActiveTabIndex = 0
                Exit Sub
            End If


            'GridView1.Visible = False
        End If

        If tb1.ActiveTabIndex = 0 Or tb1.ActiveTabIndex = 2 Or tb1.ActiveTabIndex = 3 Then
            'txtSvcMode.Text = "NEW"
            'If txtSvcMode.Text = "NEW" Or txtSvcMode.Text = "EDIT" Then
            '    lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
            '    tb1.ActiveTabIndex = 1
            '    Exit Sub
            'End If

            'GridView1.Visible = True
        ElseIf tb1.ActiveTabIndex = 0 Or tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 3 Then
            ''txtSvcMode.Text = "NEW"
            'If txtCommMode.Text = "EDIT" Then
            '    lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
            '    tb1.ActiveTabIndex = 2
            '    Exit Sub
            'End If

            'GridView1.Visible = False

        End If




    End Sub





    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        filePath = Server.MapPath("~/Uploads/") + filePath
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
        Response.WriteFile(filePath)
        Response.End()
    End Sub


    Protected Sub btnClaim_Click(sender As Object, e As EventArgs) Handles btnClaim.Click
        '''''''''''''''''''''''''''''''
        lblAlert.Text = ""
        Try
            Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            Dim conn As MySqlConnection = New MySqlConnection(constr)
            conn.Open()

            Dim cmdContractDet As MySqlCommand = New MySqlCommand

            cmdContractDet.CommandType = CommandType.Text

            cmdContractDet.CommandText = "SELECT * FROM tblcontractdet where ContractNo ='" & txtContractNo.Text & "'"
            cmdContractDet.Connection = conn

            Dim drservicecontractDet As MySqlDataReader = cmdContractDet.ExecuteReader()
            Dim dtservicecontractDet As New DataTable
            dtservicecontractDet.Load(drservicecontractDet)


            If dtservicecontractDet.Rows.Count = 0 Then
                lblAlert.Text = "Please Enter Project Detail first"
                Exit Sub
            End If


            '''''''''''''''''''''''''''''''''''

            Session("servicefrom") = "contract"

            If String.IsNullOrEmpty(lblAccountIdContact1.Text) = False Then
                Session("lblaccountid") = lblAccountIdContact1.Text
            End If

            If String.IsNullOrEmpty(txtContractNo.Text) = False Then
                Session("contractno") = txtContractNo.Text
                'txt.Text = SQLDSContract.SelectCommand
                Session("gridsql") = txt.Text
                Session("rcno") = txtRcno.Text
                Session("AccountId") = txtAccountId.Text
                Session("CustName") = txtCustName.Text
                Session("ContactType") = ddlContactType.Text
                Session("CompanyGroup") = ddlCompanyGrp.Text

                Session("Scheduler") = ddlScheduler.Text
                'Session("Team") = txtTeam.Text
                'Session("InCharge") = txtTeamIncharge.Text
                'Session("ServiceBy") = txtServiceBy.Text
                'Session("Supervisor") = txtSupervisor.Text

                'Session("ScheduleType") = ddlScheduleType.Text
                '''''''''''''''''''''''''''''
            End If

            Response.Redirect("Service.aspx")

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
        End Try
    End Sub

    Protected Sub btnDummyT_Click(sender As Object, e As EventArgs) Handles btnDummyT.Click

    End Sub

    Protected Sub txtActualEndChSt_TextChanged(sender As Object, e As EventArgs) Handles txtActualEndChSt.TextChanged
      

    End Sub
End Class
