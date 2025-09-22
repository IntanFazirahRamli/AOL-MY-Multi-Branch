Imports System.Configuration
Imports System.Web.Configuration
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        lblUserID.Text = UserID
        Session.Add("UserID", lblUserID.Text)
        Dim ID As String = Convert.ToString(Session("ID"))
        lblID.Text = ID

        Dim Name As String = Convert.ToString(Session("Name"))
        lblName.Text = Name
        Dim sysdate As String = Convert.ToString(Session("SysDate"))
        lblSysDate.Text = sysdate

        Dim SecGroupAuthority As String = Convert.ToString(Session("SecGroupAuthority"))
        'lblName.Text = Name

        lblWelcome.Text = "Welcome " + lblName.Text + "    [User Group : " + SecGroupAuthority + "]"
        lblDomainName.Text = ConfigurationManager.AppSettings("DomainName").ToString()

        If lblDomainName.Text = "SINGAPORE" Then
            link.HRef = "https://www.anticimex.com/en-SG/"
            href_master_mobiledevice.Visible = True
        ElseIf lblDomainName.Text = "SINGAPOREBETA" Then
            link.HRef = "https://www.anticimex.com/en-SG/"
            href_master_mobiledevice.Visible = False
        ElseIf lblDomainName.Text = "MALAYSIA" Then
            link.HRef = "https://www.anticimex.com/en-MY/"
            href_master_mobiledevice.Visible = False
        End If


        ' 14.04.21

        Dim connIsActiveUser As MySqlConnection = New MySqlConnection()
        Dim commandIsActiveUser As MySqlCommand = New MySqlCommand

        connIsActiveUser.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connIsActiveUser.Open()

        commandIsActiveUser.CommandType = CommandType.Text
        commandIsActiveUser.CommandText = "SELECT  StaffID FROM tblstaff where SecWebLoginID = @userid and SystemUser='Y' and Status = 'O';"
        commandIsActiveUser.Parameters.AddWithValue("@userid", UserID)
        commandIsActiveUser.Connection = connIsActiveUser

        Dim drIsActiveUser As MySqlDataReader = commandIsActiveUser.ExecuteReader()
        Dim dtIsActiveUser As New DataTable
        dtIsActiveUser.Load(drIsActiveUser)

        If dtIsActiveUser.Rows.Count = 0 Then
            'MsgBox("Inactive User")
            Response.Write("<script type=""text/javascript"">alert('Inactive User');</script>")
            Response.Redirect("Login.aspx")
        End If
        ' 14.04.21 
        connIsActiveUser.Close()
        connIsActiveUser.Dispose()
        dtIsActiveUser.Dispose()
        drIsActiveUser.Close()

        If Session("SecGroupAuthority") = "GUEST" Then
            href_admin.Visible = False
            href_customer.Visible = False
            href_project.Visible = False
            href_contract.Visible = False
            href_service.Visible = False
            href_armodule.Visible = False
            href_reports.Visible = False
            'href_tools.Visible = False
            href_Asset.Visible = False
        End If

        If String.IsNullOrEmpty(lblUserID.Text) Then
            Response.Redirect("Login.aspx")
        End If

        If Session("SecGroupAuthority") = "ADMINISTRATOR" Then
            If Session("Locationwise") = "N" Then
                href_master_location.Visible = False
            End If
        End If


        '''''' User Access
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        href_admin.Visible = False
        href_customer.Visible = False
        href_project.Visible = False
        href_contract.Visible = False
        href_service.Visible = False
        href_armodule.Visible = False
        href_reports.Visible = False
        href_tools.Visible = False
        href_Asset.Visible = False
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        command.CommandType = CommandType.Text
        command.CommandText = "SELECT * FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
        command.Connection = conn

        Dim dr1 As MySqlDataReader = command.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then

            '-------------Masters
            '1
            If Convert.IsDBNull(dt1.Rows(0)("x0251")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0251")) = False Then
                    href_master_billingcode.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_billingcode.Visible = True
                End If
            Else
                href_master_billingcode.Visible = False
            End If

            '2
            If Convert.IsDBNull(dt1.Rows(0)("x0209")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0209")) = False Then
                    href_master_coa.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_coa.Visible = True
                End If
            Else
                href_master_coa.Visible = False
            End If

            '3
            If Convert.IsDBNull(dt1.Rows(0)("x0107")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0107")) = False Then
                    href_master_city.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_city.Visible = True
                End If
            Else
                href_master_city.Visible = False
            End If

            '4
            If Convert.IsDBNull(dt1.Rows(0)("x0109")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0109")) = False Then
                    href_master_companygroup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_companygroup.Visible = True
                End If
            Else
                href_master_companygroup.Visible = False
            End If

            '5
            If Convert.IsDBNull(dt1.Rows(0)("x0112")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0112")) = False Then
                    href_master_country.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_country.Visible = True
                End If
            Else
                href_master_country.Visible = False
            End If

            '6
            If Convert.IsDBNull(dt1.Rows(0)("x0113")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0113")) = False Then
                    href_master_currency.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_currency.Visible = True
                End If
            Else
                href_master_currency.Visible = False
            End If

            '7
            If Convert.IsDBNull(dt1.Rows(0)("x0110")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0110")) = False Then
                    href_master_emailsetup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_emailsetup.Visible = True
                End If
            Else
                href_master_emailsetup.Visible = False
            End If

            '8
            If Convert.IsDBNull(dt1.Rows(0)("x0104")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0104")) = False Then
                    href_master_industry.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_industry.Visible = True
                End If
            Else
                href_master_industry.Visible = False
            End If

            '9
            If Convert.IsDBNull(dt1.Rows(0)("x1721Access")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1721Access")) = False Then
                    href_master_holiday.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_holiday.Visible = True
                End If
            Else
                href_master_holiday.Visible = False
            End If

            '10
            If Convert.IsDBNull(dt1.Rows(0)("x0118")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0118")) = False Then
                    href_master_locationgroup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_locationgroup.Visible = True
                End If
            Else
                href_master_locationgroup.Visible = False
            End If

            '11
            If Convert.IsDBNull(dt1.Rows(0)("x0103")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0103")) = False Then
                    href_master_scheduletype.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_scheduletype.Visible = True
                End If
            Else
                href_master_scheduletype.Visible = False
            End If

            '12
            If Convert.IsDBNull(dt1.Rows(0)("x0304")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0304")) = False Then
                    href_master_staff.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_staff.Visible = True
                End If
            Else
                href_master_staff.Visible = False
            End If

            '13
            If Convert.IsDBNull(dt1.Rows(0)("x0125")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0125")) = False Then
                    href_master_state.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_state.Visible = True
                End If
            Else
                href_master_state.Visible = False
            End If

            '14
            If Convert.IsDBNull(dt1.Rows(0)("x0128")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0128")) = False Then
                    href_master_taxrate.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_taxrate.Visible = True
                End If
            Else
                href_master_taxrate.Visible = False
            End If

            'x0129, x0704, x2415, 

            '15
            If Convert.IsDBNull(dt1.Rows(0)("x0129")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0129")) = False Then
                    href_master_uom.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_uom.Visible = True
                End If
            Else
                href_master_uom.Visible = False
            End If

            '16
            If Convert.IsDBNull(dt1.Rows(0)("x0704")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0704")) = False Then
                    href_master_useraccess.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_useraccess.Visible = True
                End If
            Else
                href_master_useraccess.Visible = False
            End If

            '17
            '    If Convert.IsDBNull(dt1.Rows(0)("x2415")) = False Then
            '        If Convert.ToBoolean(dt1.Rows(0)("x2415")) = False Then
            '            href_master_vehicle.Visible = False

            '            '18
            '            If Convert.IsDBNull(dt1.Rows(0)("x0151")) = False Then
            '                If Convert.ToBoolean(dt1.Rows(0)("x0151")) = False Then
            '                    href_master_contractgroupcategory.Visible = False
            '                Else
            '                    href_admin.Visible = True
            '                    href_master_contractgroupcategory.Visible = True
            '                End If
            '            Else
            '                href_master_contractgroupcategory.Visible = False
            '            End If

            '            '19
            '            If Convert.IsDBNull(dt1.Rows(0)("x0152")) = False Then
            '                If Convert.ToBoolean(dt1.Rows(0)("x0152")) = False Then
            '                    href_master_contractgroup.Visible = False
            '                Else
            '                    href_admin.Visible = True
            '                    href_master_contractgroup.Visible = True
            '                End If
            '            Else
            '                href_admin.Visible = True
            '                href_master_vehicle.Visible = True
            '            End If
            '        Else
            '            href_master_vehicle.Visible = False
            '        End If

            '    'x0151, x0152, x0153, x0154, x0155, 
            'Else
            '    href_master_contractgroup.Visible = False
            'End If


            'If Convert.IsDBNull(dt1.Rows(0)("x2415")) = False Then
            '    If Convert.ToBoolean(dt1.Rows(0)("x2415")) = False Then
            '        href_master_vehicle.Visible = False

            '        '18
            '        If Convert.IsDBNull(dt1.Rows(0)("x0151")) = False Then
            '            If Convert.ToBoolean(dt1.Rows(0)("x0151")) = False Then
            '                href_master_contractgroupcategory.Visible = False
            '            Else
            '                href_admin.Visible = True
            '                href_master_contractgroupcategory.Visible = True
            '            End If
            '        Else
            '            href_master_contractgroupcategory.Visible = False
            '        End If

            '        '19
            '        If Convert.IsDBNull(dt1.Rows(0)("x0152")) = False Then
            '            If Convert.ToBoolean(dt1.Rows(0)("x0152")) = False Then
            '                href_master_contractgroup.Visible = False
            '            Else
            '                href_admin.Visible = True
            '                href_master_contractgroup.Visible = True
            '            End If
            '        Else
            '            href_admin.Visible = True
            '            href_master_vehicle.Visible = True
            '        End If
            '    Else
            '        href_master_vehicle.Visible = False
            '    End If

            '    'x0151, x0152, x0153, x0154, x0155, 
            'Else
            '    href_master_contractgroup.Visible = False
            'End If


            If Convert.IsDBNull(dt1.Rows(0)("x2415")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2415")) = False Then
                    href_master_vehicle.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_vehicle.Visible = True
                End If
            Else
                href_master_vehicle.Visible = False
            End If


            If Convert.IsDBNull(dt1.Rows(0)("x0151")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0151")) = False Then
                    href_master_contractgroupcategory.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_contractgroupcategory.Visible = True
                End If
            Else
                href_master_contractgroupcategory.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0152")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0152")) = False Then
                    href_master_contractgroup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_contractgroup.Visible = True
                End If
            Else
                href_master_contractgroup.Visible = False
            End If

            '20
            If Convert.IsDBNull(dt1.Rows(0)("x0153")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0153")) = False Then
                    href_master_eventlog.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_eventlog.Visible = True
                End If
            Else
                href_master_eventlog.Visible = False
            End If

            '21
            If Convert.IsDBNull(dt1.Rows(0)("x0154")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0154")) = False Then
                    href_master_invoicefrequency.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_invoicefrequency.Visible = True
                End If
            Else
                href_master_invoicefrequency.Visible = False
            End If

            '22
            If Convert.IsDBNull(dt1.Rows(0)("x0155")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0155")) = False Then
                    href_master_servicemasschange.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_servicemasschange.Visible = True
                End If
            Else
                href_master_servicemasschange.Visible = False
            End If

            'x0156, x0157, x0158, x0159, x0160, x0161, x0162
            '23
            If Convert.IsDBNull(dt1.Rows(0)("x0156")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0156")) = False Then
                    href_master_mobiledevice.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_mobiledevice.Visible = True
                End If
            Else
                href_master_mobiledevice.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0156")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0156")) = False Then
                    href_master_mobiledeviceEndpoint.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_mobiledeviceEndpoint.Visible = True
                End If
            Else
                href_master_mobiledeviceEndpoint.Visible = False
            End If
            '24
            If Convert.IsDBNull(dt1.Rows(0)("x0157")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0157")) = False Then
                    href_master_postaltolocation.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_postaltolocation.Visible = True
                End If
            Else
                href_master_postaltolocation.Visible = False
            End If
            '25
            If Convert.IsDBNull(dt1.Rows(0)("x0158")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0158")) = False Then
                    href_master_servicefrequency.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_servicefrequency.Visible = True
                End If
            Else
                href_master_servicefrequency.Visible = False
            End If

            '26
            If Convert.IsDBNull(dt1.Rows(0)("x0159")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0159")) = False Then
                    href_master_service.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_service.Visible = True
                End If
            Else
                href_master_service.Visible = False
            End If

            '27
            If Convert.IsDBNull(dt1.Rows(0)("x0160")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0160")) = False Then
                    href_master_target.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_target.Visible = True
                End If
            Else
                href_master_target.Visible = False
            End If

            '28
            If Convert.IsDBNull(dt1.Rows(0)("x0161")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0161")) = False Then
                    href_master_team.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_team.Visible = True
                End If
            Else
                href_master_team.Visible = False
            End If

            '29
            If Convert.IsDBNull(dt1.Rows(0)("x0162")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0162")) = False Then
                    href_master_terminationcode.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_terminationcode.Visible = True
                End If
            Else
                href_master_terminationcode.Visible = False
            End If

            '30
            If Convert.IsDBNull(dt1.Rows(0)("x0102")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0102")) = False Then
                    href_master_terms.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_terms.Visible = True
                End If
            Else
                href_master_terms.Visible = False
            End If


            '31
            If Convert.IsDBNull(dt1.Rows(0)("x0163")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0163")) = False Then
                    href_master_marketsegment.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_marketsegment.Visible = True
                End If
            Else
                href_master_marketsegment.Visible = False
            End If

            '32
            If Convert.IsDBNull(dt1.Rows(0)("x0164")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0164")) = False Then
                    href_master_servicetypesegment.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_servicetypesegment.Visible = True
                End If
            Else
                href_master_servicetypesegment.Visible = False
            End If


            '33
            'If Convert.IsDBNull(dt1.Rows(0)("x0165")) = False Then
            '    If Convert.ToBoolean(dt1.Rows(0)("x0165")) = False Then
            '        href_master_servicerecordsetup.Visible = False
            '    Else
            '        href_admin.Visible = True
            '        href_master_servicerecordsetup.Visible = True
            '    End If
            'Else
            '    href_master_servicerecordsetup.Visible = False
            'End If

            '34
            If Convert.IsDBNull(dt1.Rows(0)("x0166")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0166")) = False Then
                    href_master_contactsetup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_contactsetup.Visible = True
                End If
            Else
                href_master_contactsetup.Visible = False
            End If

            '35
            If Convert.IsDBNull(dt1.Rows(0)("x0167")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0167")) = False Then
                    href_master_lockservicerecord.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_lockservicerecord.Visible = True
                End If
            Else
                href_master_lockservicerecord.Visible = False
            End If

            '36
            If Convert.IsDBNull(dt1.Rows(0)("x0168")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0168")) = False Then
                    href_master_chemical.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_chemical.Visible = True
                End If
            Else
                href_master_chemical.Visible = False
            End If

            '37
            If Convert.IsDBNull(dt1.Rows(0)("x0169")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0169")) = False Then
                    href_master_bank.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_bank.Visible = True
                End If
            Else
                href_master_bank.Visible = False
            End If

            '38
            If Convert.IsDBNull(dt1.Rows(0)("x0170")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0170")) = False Then
                    href_master_notestemplate.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_notestemplate.Visible = True
                End If
            Else
                href_master_notestemplate.Visible = False
            End If

            '39
            If Convert.IsDBNull(dt1.Rows(0)("x0171")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0171")) = False Then
                    href_master_settletype.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_settletype.Visible = True
                End If
            Else
                href_master_settletype.Visible = False
            End If

            '40
            If Convert.IsDBNull(dt1.Rows(0)("x0172")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0172")) = False Then
                    href_master_serviceaction.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_serviceaction.Visible = True
                End If
            Else
                href_master_serviceaction.Visible = False
            End If

            '41
            If Convert.IsDBNull(dt1.Rows(0)("x0173")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0173")) = False Then
                    href_master_period.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_period.Visible = True
                End If
            Else
                href_master_period.Visible = False
            End If

            '42
            If Convert.IsDBNull(dt1.Rows(0)("x0174")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0174")) = False Then
                    href_master_batchemail.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_batchemail.Visible = True
                End If
            Else
                href_master_batchemail.Visible = False
            End If


            '43
            If Convert.IsDBNull(dt1.Rows(0)("x0175")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0175")) = False Then
                    href_master_location.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_location.Visible = True
                End If
            Else
                href_master_location.Visible = False
            End If

            If Session("Locationwise") = "N" Then
                href_master_location.Visible = False
            End If


            '44
            If Convert.IsDBNull(dt1.Rows(0)("x0176")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0176")) = False Then
                    href_master_OPSModuleSetup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_OPSModuleSetup.Visible = True
                End If
            Else
                href_master_OPSModuleSetup.Visible = False
            End If

            '45
            If Convert.IsDBNull(dt1.Rows(0)("x0177")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0177")) = False Then
                    href_master_ARModuleSetup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_ARModuleSetup.Visible = True
                End If
            Else
                href_master_ARModuleSetup.Visible = False
            End If

            '46
            If Convert.IsDBNull(dt1.Rows(0)("x0178")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0178")) = False Then
                    href_master_SMSSetup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_SMSSetup.Visible = True
                End If
            Else
                href_master_SMSSetup.Visible = False
            End If


            '46
            If Convert.IsDBNull(dt1.Rows(0)("x0179")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0179")) = False Then
                    href_master_loginlog.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_loginlog.Visible = True
                End If
            Else
                href_master_loginlog.Visible = False
            End If


            '47
            If Convert.IsDBNull(dt1.Rows(0)("x0180")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0180")) = False Then
                    href_master_DocumentType.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_DocumentType.Visible = True
                End If
            Else
                href_master_DocumentType.Visible = False
            End If


            '48
            If Convert.IsDBNull(dt1.Rows(0)("x0181")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0181")) = False Then
                    href_CustomerPortal.Visible = False
                Else
                    href_admin.Visible = True
                    href_CustomerPortal.Visible = True
                End If
            Else
                href_CustomerPortal.Visible = False
            End If

            '49
            If Convert.IsDBNull(dt1.Rows(0)("x0182")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0182")) = False Then
                    href_master_stock.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_stock.Visible = True
                End If
            Else
                href_master_stock.Visible = False
            End If


            '50
            If Convert.IsDBNull(dt1.Rows(0)("x0183DeviceType")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0183DeviceType")) = False Then
                    href_master_devicetype.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_devicetype.Visible = True
                End If
            Else
                href_master_devicetype.Visible = False
            End If


            '52
            If Convert.IsDBNull(dt1.Rows(0)("x0184deviceEventThreshold")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0184deviceEventThreshold")) = False Then
                    href_master_deviceeventthreshold.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_deviceeventthreshold.Visible = True
                End If
            Else
                href_master_deviceeventthreshold.Visible = False
            End If

            '53
            If Convert.IsDBNull(dt1.Rows(0)("x0185")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0185")) = False Then
                    href_master_pest.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_pest.Visible = True
                End If
            Else
                href_master_pest.Visible = False
            End If

            '54
            If Convert.IsDBNull(dt1.Rows(0)("x0186")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0186")) = False Then
                    href_master_pestlevelofinfestation.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_pestlevelofinfestation.Visible = True
                End If
            Else
                href_master_pestlevelofinfestation.Visible = False
            End If

            '55
            If Convert.IsDBNull(dt1.Rows(0)("x0187")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0187")) = False Then
                    href_master_pestgender.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_pestgender.Visible = True
                End If
            Else
                href_master_pestgender.Visible = False
            End If

            '56
            If Convert.IsDBNull(dt1.Rows(0)("x0188")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0188")) = False Then
                    href_master_pestlifestage.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_pestlifestage.Visible = True
                End If
            Else
                href_master_pestlifestage.Visible = False
            End If

            '57
            If Convert.IsDBNull(dt1.Rows(0)("x0189")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0189")) = False Then
                    href_master_pestspecies.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_pestspecies.Visible = True
                End If
            Else
                href_master_pestspecies.Visible = False
            End If

            '58
            If Convert.IsDBNull(dt1.Rows(0)("x0190")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0190")) = False Then
                    href_master_pesttraptype.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_pesttraptype.Visible = True
                End If
            Else
                href_master_pesttraptype.Visible = False
            End If

            '59
            If Convert.IsDBNull(dt1.Rows(0)("x0191")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0191")) = False Then
                    href_master_HoldCode.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_HoldCode.Visible = True
                End If
            Else
                href_master_HoldCode.Visible = False
            End If

            '60
            If Convert.IsDBNull(dt1.Rows(0)("x0101")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0101")) = False Then
                    href_master_companysetup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_companysetup.Visible = True
                End If
            Else
                href_master_companysetup.Visible = False
            End If


            '61
            If Convert.IsDBNull(dt1.Rows(0)("x0194")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0194")) = False Then
                    href_master_contractcode.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_contractcode.Visible = True
                End If
            Else
                href_master_contractcode.Visible = False
            End If

            '49
            If Convert.IsDBNull(dt1.Rows(0)("x0401ToolsAddressverification")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0401ToolsAddressverification")) = False Then
                    href_address.Visible = False
                Else
                    href_tools.Visible = True
                    href_address.Visible = True
                End If
            Else
                href_address.Visible = False
            End If

            '50

            If Convert.IsDBNull(dt1.Rows(0)("x0401ToolsFloorPlan")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0401ToolsFloorPlan")) = False Then
                    href_customerlist.Visible = False
                Else
                    href_tools.Visible = True
                    href_customerlist.Visible = True
                End If
            Else
                href_customerlist.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0401ExcelImport")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0401ExcelImport")) = False Then
                    href_DataImport.Visible = False
                Else
                    href_tools.Visible = True
                    href_DataImport.Visible = True
                End If
            Else
                href_DataImport.Visible = False
            End If

            '51

            If Convert.IsDBNull(dt1.Rows(0)("x0192")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0192")) = False Then
                    href_ContractBatchPriceChange.Visible = False
                Else
                    href_admin.Visible = True
                    href_ContractBatchPriceChange.Visible = True
                End If
            Else
                href_ContractBatchPriceChange.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0193")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0193")) = False Then
                    href_master_salesmanmasschange.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_salesmanmasschange.Visible = True
                End If
            Else
                href_master_salesmanmasschange.Visible = False
            End If

            '52
            If Convert.IsDBNull(dt1.Rows(0)("x0193SetupLogDetails")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0193SetupLogDetails")) = False Then
                    href_master_LogDetailsSetup.Visible = False
                Else
                    href_admin.Visible = True
                    href_master_LogDetailsSetup.Visible = True
                End If
            Else
                href_master_LogDetailsSetup.Visible = False
            End If


            '------------Masters

            '------------Customer
            If Convert.IsDBNull(dt1.Rows(0)("x0302")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0302")) = False Then
                    href_master_company.Visible = False
                Else
                    href_customer.Visible = True
                    href_master_company.Visible = True
                End If
            Else
                href_master_company.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0303")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0303")) = False Then
                    href_master_person.Visible = False
                Else
                    href_customer.Visible = True
                    href_master_person.Visible = True
                End If
            Else
                href_master_person.Visible = False
            End If

            '------------Customer

            '------------Project

            If Convert.IsDBNull(dt1.Rows(0)("x1207")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1207")) = False Then
                    href_project.Visible = False
                Else
                    href_project.Visible = True
                    'href_master_company.Visible = True
                End If
            Else
                href_project.Visible = False
            End If

            '------------Project
            '------------Asset

            If Convert.IsDBNull(dt1.Rows(0)("x1301AssetView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1301AssetView")) = False Then
                    href_Asset.Visible = False
                Else
                    href_Asset.Visible = True
                    'href_contract.Visible = True
                End If
            Else
                href_Asset.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x1302AssetSupplierView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1302AssetSupplierView")) = False Then
                    href_AssetSupplier.Visible = False
                Else
                    href_AssetSupplier.Visible = True
                End If
            Else
                href_AssetSupplier.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x1302AssetBrandView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1302AssetBrandView")) = False Then
                    href_AssetBrand.Visible = False
                Else
                    href_AssetBrand.Visible = True
                End If
            Else
                href_AssetBrand.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x1302AssetModelView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1302AssetModelView")) = False Then
                    href_AssetModel.Visible = False
                Else
                    href_AssetModel.Visible = True
                End If
            Else
                href_AssetModel.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x1302AssetClassView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1302AssetClassView")) = False Then
                    href_AssetClass.Visible = False
                Else
                    href_AssetClass.Visible = True
                End If
            Else
                href_AssetClass.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x1302AssetColorView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1302AssetColorView")) = False Then
                    href_AssetColor.Visible = False
                Else
                    href_AssetColor.Visible = True
                End If
            Else
                href_AssetColor.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x1302AssetGroupView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1302AssetGroupView")) = False Then
                    href_AssetGroup.Visible = False
                Else
                    href_AssetGroup.Visible = True
                End If
            Else
                href_AssetGroup.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x1302AssetStatusView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1302AssetStatusView")) = False Then
                    href_AssetStatus.Visible = False
                Else
                    href_AssetStatus.Visible = True
                End If
            Else
                href_AssetStatus.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x1302AssetMovementTypeView")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x1302AssetMovementTypeView")) = False Then
                    href_AssetMovementType.Visible = False
                Else
                    href_AssetMovementType.Visible = True
                End If
            Else
                href_AssetMovementType.Visible = False
            End If

            '------------Asset
            '------------Contract

            If Convert.IsDBNull(dt1.Rows(0)("x2412")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2412")) = False Then
                    href_contract.Visible = False
                Else
                    href_contract.Visible = True
                    'href_contract.Visible = True
                End If
            Else
                href_contract.Visible = False
            End If

            '------------Contract

            '------------Service
            If Convert.IsDBNull(dt1.Rows(0)("x2413")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2413")) = False Then
                    href_service.Visible = False
                Else
                    href_service.Visible = True
                    'href_service.Visible = True
                End If
            Else
                href_service.Visible = False
            End If

            '------------Service


            '------------AR Module



            If Convert.IsDBNull(dt1.Rows(0)("x0252")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0252")) = False Then
                    href_invoice.Visible = False
                Else
                    href_invoice.Visible = True
                    href_armodule.Visible = True
                End If
            Else
                href_invoice.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0253")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0253")) = False Then
                    href_invoiceschedule.Visible = False
                Else
                    href_invoiceschedule.Visible = True
                    href_armodule.Visible = True
                End If
            Else
                href_invoiceschedule.Visible = False
            End If
            'If Convert.IsDBNull(dt1.Rows(0)("x0253")) = False Then
            '    If Convert.ToBoolean(dt1.Rows(0)("x0253")) = False Then
            '        href_invoice.Visible = False
            '    Else
            '        href_invoice.Visible = True
            '        href_armodule.Visible = True
            '    End If
            'Else
            '    href_invoice.Visible = False
            'End If

            'If Convert.IsDBNull(dt1.Rows(0)("x0254")) = False Then
            '    If Convert.ToBoolean(dt1.Rows(0)("x0254")) = False Then
            '        href_invoiceprogressbilling.Visible = False
            '    Else
            '        href_invoiceprogressbilling.Visible = True
            '        href_armodule.Visible = True
            '    End If
            'Else
            '    href_invoiceprogressbilling.Visible = False
            'End If


            If Convert.IsDBNull(dt1.Rows(0)("x0252")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0252")) = False Then
                    href_invoiceprogressbilling.Visible = False
                Else
                    href_invoiceprogressbilling.Visible = True
                    href_armodule.Visible = True
                End If
            Else
                href_invoiceprogressbilling.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0255")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0255")) = False Then
                    href_receipts.Visible = False
                Else
                    href_receipts.Visible = True
                    href_armodule.Visible = True
                End If
            Else
                href_receipts.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0256")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0256")) = False Then
                    href_cn.Visible = False
                Else
                    href_cn.Visible = True
                    href_armodule.Visible = True
                End If
            Else
                href_cn.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x0256")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0256")) = False Then
                    href_cnprogressbilling.Visible = False
                Else
                    href_cnprogressbilling.Visible = True
                    href_armodule.Visible = True
                End If
            Else
                href_cnprogressbilling.Visible = False
            End If

            'If Convert.IsDBNull(dt1.Rows(0)("x0257")) = False Then
            '    If Convert.ToBoolean(dt1.Rows(0)("x0257")) = False Then
            '        href_cnprogressbilling.Visible = False
            '    Else
            '        href_cnprogressbilling.Visible = True
            '        href_armodule.Visible = True
            '    End If
            'Else
            '    href_cnprogressbilling.Visible = False
            'End If

            If Convert.IsDBNull(dt1.Rows(0)("x0258")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x0258")) = False Then
                    href_adjustmentnote.Visible = False
                Else
                    href_adjustmentnote.Visible = True
                    href_armodule.Visible = True
                End If
            Else
                href_adjustmentnote.Visible = False
            End If


            '------------AR Module

            '------------Reports


            If Convert.IsDBNull(dt1.Rows(0)("x2427ServiceRecord")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2427ServiceRecord")) = False Then
                    'href_adjustmentnote.Visible = False
                Else
                    'href_adjustmentnote.Visible = True
                    href_reports.Visible = True
                End If
            Else
                'href_adjustmentnote.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x2427ServiceContract")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2427ServiceContract")) = False Then
                    'href_adjustmentnote.Visible = False
                Else
                    'href_adjustmentnote.Visible = True
                    href_reports.Visible = True
                End If
            Else
                'href_adjustmentnote.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x2427Management")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2427Management")) = False Then
                    'href_adjustmentnote.Visible = False
                Else
                    'href_adjustmentnote.Visible = True
                    href_reports.Visible = True
                End If
            Else
                'href_adjustmentnote.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x2427Revenue")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2427Revenue")) = False Then
                    'href_adjustmentnote.Visible = False
                Else
                    'href_adjustmentnote.Visible = True
                    href_reports.Visible = True
                End If
            Else
                'href_adjustmentnote.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x2427Portfolio")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2427Portfolio")) = False Then
                    'href_adjustmentnote.Visible = False
                Else
                    'href_adjustmentnote.Visible = True
                    href_reports.Visible = True
                End If
            Else
                'href_adjustmentnote.Visible = False
            End If

            If Convert.IsDBNull(dt1.Rows(0)("x2427Others")) = False Then
                If Convert.ToBoolean(dt1.Rows(0)("x2427Others")) = False Then
                    'href_adjustmentnote.Visible = False
                Else
                    'href_adjustmentnote.Visible = True
                    href_reports.Visible = True
                End If
            Else
                'href_adjustmentnote.Visible = False
            End If



            'If Convert.ToBoolean(dt1.Rows(0)("x2427ServiceRecord")) = False Then
            '    href_service.Visible = False
            'End If

            'If Convert.ToBoolean(dt1.Rows(0)("x2427ServiceContract")) = False Then
            '    href_contract.Visible = False
            'End If

            'If Convert.ToBoolean(dt1.Rows(0)("x2427Management")) = False Then
            '    'href_management.Visible = False
            'End If

            'If Convert.ToBoolean(dt1.Rows(0)("x2427Revenue")) = False Then
            '    'href_revenue.Visible = False
            'End If

            'If Convert.ToBoolean(dt1.Rows(0)("x2427Portfolio")) = False Then
            '    'href_portfolio.Visible = False
            'End If

            '------------Reports
        End If

        conn.Close()
        conn.Dispose()
        command.Dispose()
        dt1.Dispose()
        dr1.Close()
        'End If
        '''''' User Access



        'If Not IsPostBack Then
        '    Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoad", "DisplaySessionTimeout()", True)
        'End If
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ''    If Not IsPostBack Then
        'Session("Reset") = True
        'Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.Config")
        'Dim section As SessionStateSection = DirectCast(config.GetSection("system.web/sessionState"), SessionStateSection)
        'Dim timeout As Integer = CInt(section.Timeout.TotalMinutes) * 1000 * 60
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "SessionAlert", "SessionExpireAlert(" & timeout & ");", True)
        '   End If

        If Not IsPostBack Then
            'Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tbllocation where rcno=1"
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then

                    If String.IsNullOrEmpty(dt2.Rows(0)("CompanyName").ToString) = False Then
                        Session.Add("CompanyName", dt2.Rows(0)("CompanyName"))
                        Session.Add("OfficeAddress", dt2.Rows(0)("OfficeAddress1").ToString)
                        Session.Add("BusinessRegNumber", dt2.Rows(0)("BusinessRegistrationNumber").ToString)
                        Session.Add("GSTNumber", dt2.Rows(0)("GSTNumber").ToString)
                        Session.Add("TelNumber", dt2.Rows(0)("TelephoneNumber").ToString)
                        Session.Add("FaxNumber", dt2.Rows(0)("FaxNumber").ToString)
                        Session.Add("Website", dt2.Rows(0)("Website").ToString)
                        Session.Add("CompanyEmail", dt2.Rows(0)("Email").ToString)
                        Session.Add("InvoiceEmail", dt2.Rows(0)("InvoiceEmail").ToString)
                    Else

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT * FROM tblcompanyinfo where rcno=1"
                        command1.Connection = conn

                        Dim dr As MySqlDataReader = command1.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then
                            Session.Add("CompanyName", dt.Rows(0)("CompanyName"))
                            Session.Add("OfficeAddress", dt.Rows(0)("OfficeAddress1").ToString)
                            Session.Add("BusinessRegNumber", dt.Rows(0)("BusinessRegistrationNumber").ToString)
                            Session.Add("GSTNumber", dt.Rows(0)("GSTNumber").ToString)
                            Session.Add("TelNumber", dt.Rows(0)("TelephoneNumber").ToString)
                            Session.Add("FaxNumber", dt.Rows(0)("FaxNumber").ToString)
                            Session.Add("Website", dt.Rows(0)("Website").ToString)
                            Session.Add("CompanyEmail", dt.Rows(0)("Email").ToString)
                            Session.Add("InvoiceEmail", dt.Rows(0)("InvoiceEmail").ToString)
                        End If

                        dt.Clear()
                        dr.Close()
                        command1.Dispose()

                    End If


                End If

                dt2.Clear()
                dr2.Close()
                command2.Dispose()
            Else
                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblcompanyinfo where rcno=1"
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    Session.Add("CompanyName", dt.Rows(0)("CompanyName"))
                    Session.Add("OfficeAddress", dt.Rows(0)("OfficeAddress1").ToString)
                    Session.Add("BusinessRegNumber", dt.Rows(0)("BusinessRegistrationNumber").ToString)
                    Session.Add("GSTNumber", dt.Rows(0)("GSTNumber").ToString)
                    Session.Add("TelNumber", dt.Rows(0)("TelephoneNumber").ToString)
                    Session.Add("FaxNumber", dt.Rows(0)("FaxNumber").ToString)
                    Session.Add("Website", dt.Rows(0)("Website").ToString)
                    Session.Add("CompanyEmail", dt.Rows(0)("Email").ToString)
                    Session.Add("InvoiceEmail", dt.Rows(0)("InvoiceEmail").ToString)
                End If

                dt.Clear()
                dr.Close()
                command1.Dispose()

            End If


            conn.Close()
            conn.Dispose()

        End If
    End Sub



    Protected Sub OnMenuItemDataBound(sender As Object, e As MenuEventArgs)
        If SiteMap.CurrentNode IsNot Nothing Then
            If e.Item.Text = SiteMap.CurrentNode.Title Then
                If e.Item.Parent IsNot Nothing Then
                    e.Item.Parent.Selected = True
                Else
                    e.Item.Selected = True
                End If
            End If
        End If
    End Sub

    'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHNGACCT", txtLocationIDRelocate.Text, "INVOICE", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, txtAccountIDRelocate.Text, TextBoxInvoiceNoGVBI.Text, txtLocationIDRelocateNew.Text)

    Public Function EventLog_Insert( _
         ByVal vStaffID As String, _
         ByVal vModule As String, _
         ByVal vDocRef As String, _
         ByVal vAction As String, _
         ByVal vLogDate As String, _
         ByVal vAmount As Double, _
         ByVal vBaseValue As Double, _
         ByVal vBaseGSTValue As Double, _
         ByVal vCustCode As String, _
         ByVal vComments As String, _
         Optional ByVal vSOURCESQLID As String = "") As Boolean
        Try
            EventLog_Insert = False
            Dim strSql As String = "INSERT INTO tblEventLog (StaffID,Module,DocRef,Action,ComputerName," & _
                "Serial, LogDate, Amount,BaseValue,BaseGSTValue,CustCode,Comments,SOURCESQLID) " & _
                "VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate, @Amount, @BaseValue, @BaseGSTValue, @CustCode, @Comments, @SOURCESQLID)"

            ''"'" & (vModule) & "', " & _
            '"'" & (vDocRef) & "', " & _
            '"'" & (vAction) & "', " & _
            '"'" & (Strings.Left(My.Computer.Name.ToString, 20)) & "'," & _
            '"''," & _
            'Convert.ToDateTime(vLogDate) & ", " & _
            'Val(vAmount) & "," & Val(vBaseValue) & "," & Val(vBaseGSTValue) & "," & _
            '"'" & (vCustCode) & "','" & (vComments) & "','" & (vSOURCESQLID) & "') ")

            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text
            command.CommandText = strSql
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@StaffID", vStaffID)
            command.Parameters.AddWithValue("@Module", vModule)
            command.Parameters.AddWithValue("@DocRef", vDocRef)
            command.Parameters.AddWithValue("@Action", vAction)
            command.Parameters.AddWithValue("@ComputerName", Strings.Left(My.Computer.Name.ToString, 20))
            command.Parameters.AddWithValue("@Serial", "")
            'command.Parameters.AddWithValue("@LogDate", Convert.ToDateTime(vLogDate))
            command.Parameters.AddWithValue("@LogDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            command.Parameters.AddWithValue("@Amount", Val(vAmount))
            command.Parameters.AddWithValue("@BaseValue", Val(vBaseValue))
            command.Parameters.AddWithValue("@BaseGSTValue", vBaseGSTValue)
            command.Parameters.AddWithValue("@CustCode", vCustCode)
            command.Parameters.AddWithValue("@Comments", vComments)
            command.Parameters.AddWithValue("@SOURCESQLID", vSOURCESQLID)
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Dim conn As MySqlConnection = New MySqlConnection(constr)
            conn.Open()
            Command.Connection = conn
            Command.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()
            command.Dispose()

            EventLog_Insert = True
        Catch ex As Exception
            MsgBox("EventLog_Insert " & ex.Message.ToString)
            EventLog_Insert = False
        End Try

        '"'" & Get_DiskSerial("C") & "'," & _
        '"" & Convert.ToDateTime(vLogDate).ToString("yyyy-MM-dd") & ", " & _
    End Function

    'Public Function Get_DiskSerial(ByVal vDriveLetter As String) As String
    '    Try
    '        Dim strSerial As String = ""
    '        Dim FSO = Server.CreateObject("Scripting.FileSystemObject")
    '        Dim vDrive = Server.CreateObject("Scripting.Drive")
    '        'Dim FSO As New Scripting.FileSystemObject()
    '        'Dim vDrive As Scripting.Drive
    '        For Each vDrive In FSO.Drives
    '            If vDrive.DriveLetter = vDriveLetter Then
    '                If vDrive.IsReady Then
    '                    strSerial = vDrive.SerialNumber
    '                Else
    '                    MsgBox("Drive C:\ is Not Available!")
    '                End If
    '            End If
    '        Next vDrive
    '        Get_DiskSerial = strSerial
    '    Catch ex As Exception
    '        MsgBox("<z07Maintain.Get_DiskSerial> " & ex.Message)
    '        Get_DiskSerial = ""
    '    End Try
    'End Function

    Protected Sub lnkChangePassword_Click(sender As Object, e As EventArgs) Handles lnkChangePassword.Click
        Session.Add("UserID", lblUserID.Text)

        Response.Redirect("ChangePassword.aspx")
    End Sub


    Public Sub UpdateContract(ContractNo As String)

        ''''Update Contract
        '''''''''''''''''''''''''''''''''''''''
        'Retrieve Contract details
        '''''''''''''''''''''''''''''''''''''''''
        Try
            Dim TotService As Decimal = 0.0
            Dim TotServiceActual As Decimal = 0.0
            Dim TotServiceAmt As Decimal = 0.0
            Dim TotAmtCompleted As Decimal = 0.0

            Dim TotServiceBalance As Decimal = 0.0
            Dim TotAmtBalance As Decimal = 0.0

            Dim totarea0 As Decimal = 0
            Dim totbait0 As Integer = 0
            Dim totareaprice0 As Decimal = 0
            Dim totbaitprice0 As Decimal = 0

            Dim totarea1 As Decimal = 0
            Dim totbait1 As Integer = 0
            Dim totareaprice1 As Decimal = 0
            Dim totbaitprice1 As Decimal = 0

            Dim actualstartdate As Date

            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT ContractNo,agreevalue FROM tblcontract where CONTRACTNO='" & ContractNo & "'"
            command2.Connection = conn

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then
                '''''''Retrieve all services under that contract 
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim command0 As MySqlCommand = New MySqlCommand

                command0.CommandType = CommandType.Text

                'command0.CommandText = "SELECT count(RecordNo) as TotalService FROM tblservicerecord where CONTRACTNO='" & ContractNo & "'"
                command0.CommandText = "SELECT count(RecordNo) as TotalService FROM tblservicerecord where CONTRACTNO='" & ContractNo & "' and ((STATUS='P') or (STATUS='O') or (STATUS='H'))"
                command0.Connection = conn

                Dim dr0 As MySqlDataReader = command0.ExecuteReader()
                Dim dt0 As New DataTable
                dt0.Load(dr0)

                '''''''Retrieve all posted services under that contract 
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                ''28.01.20

                Dim command10 As MySqlCommand = New MySqlCommand

                command10.CommandType = CommandType.Text

                'command0.CommandText = "SELECT count(RecordNo) as TotalService FROM tblservicerecord where CONTRACTNO='" & ContractNo & "'"
                command10.CommandText = "SELECT sum(BillAmount) as TotalServiceAmt FROM tblservicerecord where CONTRACTNO='" & ContractNo & "' and ((STATUS='P') or (STATUS='O') or (STATUS='H'))"
                command10.Connection = conn

                Dim dr10 As MySqlDataReader = command10.ExecuteReader()
                Dim dt10 As New DataTable
                dt10.Load(dr10)

                ''28.01.20


                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT ifnull(count(RecordNo),0) as TotalActualService, ifnull(sum(BillAmount),0) as TotalCompletedAmt FROM tblservicerecord where CONTRACTNO='" & ContractNo & "' AND STATUS='P'"
                command1.Connection = conn

                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If IsDBNull(dt0.Rows(0)("TotalService")) = False Then
                    TotService = dt0.Rows(0)("TotalService")
                End If

                If IsDBNull(dt1.Rows(0)("TotalActualService")) = False Then
                    TotServiceActual = dt1.Rows(0)("TotalActualService")
                End If

                TotServiceBalance = TotService - TotServiceActual

                'If IsDBNull(dt2.Rows(0)("Agreevalue")) = False Then
                '    TotServiceAmt = dt2.Rows(0)("Agreevalue")
                'End If

                If IsDBNull(dt10.Rows(0)("TotalServiceAmt")) = False Then
                    TotServiceAmt = dt10.Rows(0)("TotalServiceAmt")
                End If

                If IsDBNull(dt1.Rows(0)("TotalCompletedAmt")) = False Then
                    TotAmtCompleted = dt1.Rows(0)("TotalCompletedAmt")
                End If


                TotAmtBalance = TotServiceAmt - TotAmtCompleted
                dt2.Clear()
                dr2.Close()
                dt2.Dispose()

                dt0.Clear()
                dr0.Close()
                dt0.Dispose()

                '''''''''''''''''''''''''''''''''

                Dim command3 As MySqlCommand = New MySqlCommand

                command3.CommandType = CommandType.Text
                command3.CommandText = "SELECT sum(ServiceArea) as TotalServiceArea,sum(BaitStationInstalled) as TotalBaitStation,sum(PriceOfAreaCompleted) as totalPrice, sum(TotalPriceForBaitStation) as totalbaitprice FROM tblservicerecorddet where ContractNo ='" & ContractNo & "'"
                command3.Connection = conn

                Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                Dim dt3 As New DataTable
                dt3.Load(dr3)

                totarea0 = 0

                If dt3.Rows.Count > 0 Then
                    If IsDBNull(dt3.Rows(0)("TotalServiceArea")) = False Then
                        totarea0 = dt3.Rows(0)("TotalServiceArea")
                    End If

                    If IsDBNull(dt3.Rows(0)("TotalBaitStation")) = False Then
                        totbait0 = dt3.Rows(0)("TotalBaitStation")
                    End If

                    If IsDBNull(dt3.Rows(0)("totalprice")) = False Then
                        totareaprice0 = dt3.Rows(0)("totalprice")
                    End If

                    If IsDBNull(dt3.Rows(0)("totalbaitprice")) = False Then
                        totbaitprice0 = dt3.Rows(0)("totalbaitprice")
                    End If

                End If
                dt3.Clear()
                dr3.Close()
                dt3.Dispose()

                ''''''''''''''''''''''''''''

                Dim command4 As MySqlCommand = New MySqlCommand

                command4.CommandType = CommandType.Text
                command4.CommandText = "SELECT min(servicedate) as actualservicedate, sum(AreaCompleted) as TotalAreaCompleted,sum(BaitStationInstalled) as TotalBaitStation,sum(PriceOfAreaCompleted) as totalPrice, sum(TotalPriceForBaitStation) as totalbaitprice FROM tblservicerecorddet, tblservicerecord where tblservicerecord.RecordNo = tblservicerecorddet.RecordNo and tblservicerecorddet.ContractNo ='" & ContractNo & "' and tblservicerecord.Status ='P'"
                command4.Connection = conn

                Dim dr4 As MySqlDataReader = command4.ExecuteReader()
                Dim dt4 As New DataTable
                dt4.Load(dr4)

                totarea1 = 0

                If dt4.Rows.Count > 0 Then
                    If IsDBNull(dt4.Rows(0)("TotalAreaCompleted")) = False Then
                        totarea1 = dt4.Rows(0)("TotalAreaCompleted")
                    End If

                    If IsDBNull(dt4.Rows(0)("TotalBaitStation")) = False Then
                        totbait1 = dt4.Rows(0)("TotalBaitStation")
                    End If

                    If IsDBNull(dt4.Rows(0)("totalprice")) = False Then
                        totareaprice1 = dt4.Rows(0)("totalprice")
                    End If

                    If IsDBNull(dt4.Rows(0)("totalbaitprice")) = False Then
                        totbaitprice1 = dt4.Rows(0)("totalbaitprice")
                    End If

                    If IsDBNull(dt4.Rows(0)("actualservicedate")) = False Then
                        actualstartdate = dt4.Rows(0)("actualservicedate")
                    End If

                End If

                command0.Dispose()
                command1.Dispose()
                'command2.Dispose()

                command3.Dispose()
                command4.Dispose()

                dt4.Clear()
                dr4.Close()
                dt4.Dispose()

                '''''''''''''''''''''''''''''
                command.CommandType = CommandType.Text
                command.CommandText = "UPDATE tblcontract SET ServiceNo=@ServiceNo,servicenoactual=@servicenoactual,servicebal=@servicebal,serviceamt=@serviceamt,amtcompleted=@amtcompleted,amtbalance=@amtbalance, totalarea=@totalarea, completedarea=@completedarea,balancearea=@balancearea,BaitStationTotal=@BaitStationTotal, BaitStationInstalled=@BaitStationInstalled,BaitStationBalance=@BaitStationBalance, actualservicestartdate=@actualservicestartdate where contractno='" & ContractNo & "'"
                command.Parameters.Clear()
                command.Parameters.AddWithValue("@serviceno", TotService)
                command.Parameters.AddWithValue("@servicenoactual", TotServiceActual)
                command.Parameters.AddWithValue("@servicebal", TotServiceBalance)

                command.Parameters.AddWithValue("@serviceamt", TotServiceAmt)
                command.Parameters.AddWithValue("@amtcompleted", TotAmtCompleted)
                command.Parameters.AddWithValue("@amtbalance", TotAmtBalance)

                command.Parameters.AddWithValue("@totalarea", totarea0)
                command.Parameters.AddWithValue("@completedarea", totarea1)
                command.Parameters.AddWithValue("@balancearea", totarea0 - totarea1)
                'End If
                command.Parameters.AddWithValue("@BaitStationTotal", totbait0)
                command.Parameters.AddWithValue("@BaitStationInstalled", totbait1)
                command.Parameters.AddWithValue("@BaitStationBalance", totbait0 - totbait1)

                If String.IsNullOrEmpty(actualstartdate) = True Then
                    command.Parameters.AddWithValue("@actualservicestartdate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@actualservicestartdate", actualstartdate.ToString("yyyy-MM-dd"))
                End If

                command.Connection = conn

                command.ExecuteNonQuery()


            End If ' If dt1.Rows.Count > 0 Then

            conn.Close()
            conn.Dispose()
            command.Dispose()
            command2.Dispose()


            'Update Actual Service Start Date
            'If dt1.Rows.Count = 1 Then
            '    Dim commandContract As MySqlCommand = New MySqlCommand

            '    commandContract.CommandType = CommandType.Text

            '    commandContract.CommandText = "Update tblContract set actualservicestartdate=@ServiceDate where contractno=@contractno "
            '    commandContract.Parameters.AddWithValue("@contractno", ContractNo)
            '    If String.IsNullOrEmpty(txtActSvcDate.Text) = True Then
            '        commandContract.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtScheduleDate.Text).ToString("yyyy-MM-dd"))
            '    Else
            '        commandContract.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(txtActSvcDate.Text).ToString("yyyy-MM-dd"))
            '    End If
            '    commandContract.Connection = conn

            '    commandContract.ExecuteNonQuery()
            'End If
            'End If 'If dt2.Rows.Count > 0 Then


        Catch ex As Exception
            'InsertIntoTblWebEventLog("UpdateContract", ex.Message.ToString, ContractNo)
        End Try
    End Sub

    Public Sub UpdateInvoiceBal(InvNo As String)
        Try
            Dim lTotalReceipt As Decimal
            Dim lInvoiceAmount As Decimal
            Dim lTotalcn As Decimal
            lTotalReceipt = 0.0
            lInvoiceAmount = 0.0
            lTotalcn = 0.0
            'Get Item desc, price Id

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand


            '''''''''''''''''''''
            Dim commandReceipt As MySqlCommand = New MySqlCommand
            commandReceipt.CommandType = CommandType.Text

            commandReceipt.CommandText = "UPDATE tblSales SET ReceiptBase = (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblRecvDet A, tblRecv B WHERE " & _
                  "A.ReceiptNumber=B.ReceiptNumber AND A.SubCode = 'ARIN'  AND A.RefType = tblSales.InvoiceNumber AND " & _
                  "B.PostStatus = 'P' ) WHERE InvoiceNumber = '" & InvNo & "' "
            commandReceipt.Connection = conn

            commandReceipt.ExecuteNonQuery()
            commandReceipt.Dispose()

            '''''''''''''''''''

            Dim commandCN As MySqlCommand = New MySqlCommand
            commandCN.CommandType = CommandType.Text


            commandCN.CommandText = "SELECT  ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalcn FROM tblSalesDetail A, tblSales B WHERE " & _
                "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN')  and B.PostStatus = 'P' and A.SourceInvoice = '" & InvNo & "'"

            commandCN.Connection = conn


            Dim dr2 As MySqlDataReader = commandCN.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then
                lTotalcn = dt2.Rows(0)("totalcn").ToString
            End If
            'lTotalcn = lTotalcn * (-1)

            commandCN.Dispose()
            ''''''''''''''''''''''''


            '''''''''''''' Journal

            Dim lTotalJV As Decimal
            Dim commandJournal As MySqlCommand = New MySqlCommand
            commandJournal.CommandType = CommandType.Text

            commandJournal.CommandText = "SELECT   ifnull(SUM(ifnull(A.DebitBase,0)),0) as debitbase, ifnull(SUM(ifnull(A.CreditBase,0)),0) as creditbase  FROM tbljrnvdet A, tbljrnv B WHERE " & _
               "A.VoucherNumber=B.VoucherNumber AND  B.PostStatus = 'P'  and A.RefType = '" & InvNo & "' "

            commandJournal.Connection = conn

            Dim drJournal As MySqlDataReader = commandJournal.ExecuteReader()
            Dim dtJournal As New DataTable
            dtJournal.Load(drJournal)

            If dtJournal.Rows.Count > 0 Then
                'If dtJournal.Rows(0)("debitbase").ToString > 0.0 Then
                '    lTotalJV = dtJournal.Rows(0)("debitbase").ToString
                'Else
                '    lTotalJV = dtJournal.Rows(0)("creditbase").ToString
                'End If
                lTotalJV = Convert.ToDecimal(dtJournal.Rows(0)("debitbase").ToString - dtJournal.Rows(0)("creditbase").ToString)
            End If

            ''''''''''''''' Journal

            Dim lbalance As Decimal
            Dim command3 As MySqlCommand = New MySqlCommand
            command3.CommandType = CommandType.Text
            command3.CommandText = "SELECT ValueBase, GSTBase , AppliedBase , ReceiptBase, CreditBase, CreditBase FROM tblSales where InvoiceNumber = '" & InvNo & "'"

            command3.Connection = conn
            command3.ExecuteNonQuery()

            Dim dr3 As MySqlDataReader = command3.ExecuteReader()
            Dim dt3 As New DataTable
            dt3.Load(dr3)

            If dt3.Rows.Count > 0 Then


                If String.IsNullOrEmpty(dt3.Rows(0)("AppliedBase").ToString) = False Then
                    lbalance = dt3.Rows(0)("AppliedBase").ToString
                Else
                    lbalance = 0.0
                End If

                If String.IsNullOrEmpty(dt3.Rows(0)("ReceiptBase").ToString) = False Then
                    lbalance = lbalance - dt3.Rows(0)("ReceiptBase").ToString
                Else
                    'lbalance = 0.0
                End If

                lbalance = lbalance + lTotalcn + lTotalJV
                'If String.IsNullOrEmpty(dt3.Rows(0)("CreditBase").ToString) = False Then
                '    lbalance = lbalance - dt3.Rows(0)("CreditBase").ToString
                'Else
                '    'lbalance = 0.0
                'End If

            End If

            ''''''''''' Journal


            Dim lcredbal As Decimal
            lcredbal = 0.0
            lcredbal = (lTotalcn + lTotalJV) * (-1)

            Dim command4 As MySqlCommand = New MySqlCommand
            command4.CommandType = CommandType.Text

            Dim qry4 As String = "Update tblSales Set CreditBase = " & lcredbal & ", BalanceBase = " & lbalance & " where InvoiceNumber = @InvoiceNumber "

            command4.CommandText = qry4
            command4.Parameters.Clear()

            command4.Parameters.AddWithValue("@InvoiceNumber", InvNo)
            command4.Connection = conn
            command4.ExecuteNonQuery()

            '    'End: Update tblSales

            'End
            conn.Close()
            conn.Dispose()

            command1.Dispose()
            'command2.Dispose()
            'commandCN.Dispose()
            command3.Dispose()
            command4.Dispose()

            'dt1.Dispose()
            dt2.Dispose()
            dt3.Dispose()

            'dr1.Close()
            dr2.Close()
            dr3.Close()

            'SQLDSInvoice.SelectCommand = txt.Text
            'SQLDSInvoice.DataBind()
            'GridView1.DataSourceID = "SQLDSInvoice"
            'GridView1.DataBind()

            'SQLDSInvoice.SelectCommand = txt.Text
            'SQLDSInvoice.DataBind()
            'GridView1.DataBind()
            'updPanelInvoice.Update()
        Catch ex As Exception
            'InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "ReCalculate", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


End Class

