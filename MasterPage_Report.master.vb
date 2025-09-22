Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web

Partial Class MasterPage_Report
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim conn As MySqlConnection = New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        '''''' User Access
        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT x2427ServiceContract, x2427ServiceRecord, x2427Portfolio, x2427Revenue, x2427Management, x2427Others FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.Connection = conn

            Dim dr1 As MySqlDataReader = command.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                If Not IsDBNull(dt1.Rows(0)("x2427ServiceRecord")) Then
                    If Convert.ToBoolean(dt1.Rows(0)("x2427ServiceRecord")) = False Then
                        href_service.Visible = False
                    End If
                End If

                If Not IsDBNull(dt1.Rows(0)("x2427ServiceContract")) Then
                    If Convert.ToBoolean(dt1.Rows(0)("x2427ServiceContract")) = False Then
                        href_contract.Visible = False
                    End If
                End If

                If Not IsDBNull(dt1.Rows(0)("x2427Management")) Then
                    If Convert.ToBoolean(dt1.Rows(0)("x2427Management")) = False Then
                        href_management.Visible = False
                    End If
                End If

                If Not IsDBNull(dt1.Rows(0)("x2427Revenue")) Then
                    If Convert.ToBoolean(dt1.Rows(0)("x2427Revenue")) = False Then
                        href_revenue.Visible = False
                    End If
                End If

                If Not IsDBNull(dt1.Rows(0)("x2427Portfolio")) Then
                    If Convert.ToBoolean(dt1.Rows(0)("x2427Portfolio")) = False Then
                        href_portfolio.Visible = False
                    End If
                End If

                If Not IsDBNull(dt1.Rows(0)("x2427Others")) Then
                    If Convert.ToBoolean(dt1.Rows(0)("x2427Others")) = False Then
                        href_armodule.Visible = False
                    End If
                End If
            End If

            command.Dispose()
            dt1.Dispose()
            dr1.Close()
        End If
        '''''' User Access

        If Not IsPostBack Then


            'Find if Branch/Location Enabled
            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            commandServiceRecordMasterSetup.CommandText = "SELECT DisplayRecordsLocationWise FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
            Session.Add("LocationEnabled", txtDisplayRecordsLocationwise.Text)

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                command.CommandText = "select locationID from tblgroupaccesslocation where GroupAccess = '" & Session("SecGroupAuthority") & "' order by LocationID"
                command.Connection = conn

                Dim dr1 As MySqlDataReader = command.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                If dt1.Rows.Count > 0 Then
                    For i As Int16 = 0 To dt1.Rows.Count - 1
                        '   YrStrList.Add(dt1.Rows(i)("LocationID"))
                        '  chkBranch.Items.Add(dt1.Rows(i)("LocationID"))
                        YrStrList.Add("""" + dt1.Rows(i)("LocationID") + """")
                    Next
                    Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                    Session.Add("Branch", YrStr)
                End If


                chkBranch.DataSource = command.ExecuteReader()
                chkBranch.DataTextField = "LOCATIONID"
                chkBranch.DataValueField = "LOCATIONID"
                chkBranch.DataBind()

                For Each item As ListItem In chkBranch.Items
                    item.Selected = True
                Next

                command.Dispose()
                dt1.Dispose()
                dr1.Close()
            Else
                Label1.Visible = False
                chkBranch.Visible = False
                chkSelectAll.Visible = False
            End If

            commandServiceRecordMasterSetup.Dispose()
            dtServiceRecordMasterSetup.Dispose()

            'Dim Roles As String = ""
            'Dim command As MySqlCommand = New MySqlCommand

            'command.CommandType = CommandType.Text
            'command.CommandText = "SELECT StaffID, Name, Roles FROM tblstaff where staffid = @userid;"
            'command.Parameters.AddWithValue("@userid", Session("UserID"))
            'command.Connection = conn

            'Dim dr As MySqlDataReader = command.ExecuteReader()
            'Dim dt As New DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then
            '    Roles = "SALESMAN"
            'Else
            '    Roles = ""
            'End If

            'Session.Add("Roles", Roles)

            'command.Dispose()
            'dt.Clear()
            'dr.Close()
            'dt.Dispose()
            'command.Dispose()

        End If

      

        conn.Close()
        conn.Dispose()


    End Sub

    Protected Sub chkBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkBranch.SelectedIndexChanged
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        Dim count As Int16 = 0

        For Each item As ListItem In chkBranch.Items
            If item.Selected = True Then
                YrStrList.Add("""" + item.Value + """")
                count = count + 1
            End If
        Next
        If count = 0 Then
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                lblAlert.Text = "SELECT BRANCH/LOCATION"
            End If


        End If
        Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
        Session.Add("Branch", YrStr)
    End Sub
End Class

