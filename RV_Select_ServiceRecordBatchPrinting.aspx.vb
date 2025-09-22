
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data


Partial Class RV_Select_ServiceRecordBatchPrinting
    Inherits System.Web.UI.Page
    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs)
        If txtModal.Text = "btnTeam" Then
            If gvTeam.SelectedRow.Cells(1).Text = "&nbsp;" Then
                txtTeam.Text = " "
            Else
                txtTeam.Text = gvTeam.SelectedRow.Cells(1).Text
            End If
        ElseIf txtModal.Text = "btnSvcBy" Then
            'If gvTeam.SelectedRow.Cells(2).Text = "&nbsp;" Then
            '    txtServiceBy.Text = " "
            'Else
            '    txtServiceBy.Text = gvTeam.SelectedRow.Cells(2).Text
            'End If

        End If




    End Sub

    Protected Sub gvTeam_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTeam.PageIndexChanging
        gvTeam.PageIndex = e.NewPageIndex

        If txtPopUpTeam.Text.Trim = "" Then
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status<>'N' order by TeamName"
        Else
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            '  SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%' and Status <> 'N'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"

        End If

        SqlDSTeam.DataBind()
        gvTeam.DataBind()
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub btnPopUpTeamSearch_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnPopUpTeamReset_Click(sender As Object, e As EventArgs)
        txtPopUpTeam.Text = ""
        txtPopupTeamSearch.Text = ""
        '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

        SqlDSTeam.DataBind()
        gvTeam.DataBind()
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub txtPopUpTeam_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpTeam.TextChanged
        If txtPopUpTeam.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupTeamSearch.Text = txtPopUpTeam.Text
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
            SqlDSTeam.DataBind()
            gvTeam.DataBind()
            mdlPopUpTeam.Show()
        End If
        txtPopUpTeam.Text = "Search Here for Team, Incharge or ServiceBy"
    End Sub

    Protected Sub btnTeam_Click(sender As Object, e As ImageClickEventArgs) Handles btnTeam.Click
        mdlPopUpTeam.TargetControlID = "btnTeam"
        txtModal.Text = "btnTeam"
        If String.IsNullOrEmpty(txtTeam.Text.Trim) = False Then
            txtPopupTeamSearch.Text = txtTeam.Text.Trim
            txtPopUpTeam.Text = txtPopupTeamSearch.Text
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
            SqlDSTeam.DataBind()
            gvTeam.DataBind()
        Else
            'txtPopUpTeam.Text = ""
            'txtPopupTeamSearch.Text = ""
            '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

            SqlDSTeam.DataBind()
            gvTeam.DataBind()
        End If
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        'If String.IsNullOrEmpty(ddlAccountType.Text) Then
        '    '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
        '    lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
        '    Return
        'End If
        'If ddlAccountType.Text = "-1" Then
        '    '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
        '    lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
        '    Return
        'End If
        txtModal.Text = "ID"

        If String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

        ElseIf String.IsNullOrEmpty(txtServiceLocationID.Text.Trim) = False Then
            txtPopUpClient.Text = txtServiceLocationID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

            'ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            '    txtPopUpClient.Text = txtCustName.Text
            '    txtPopupClientSearch.Text = txtPopUpClient.Text
            '    ClientQuery("Search")

        Else
            ' txtPopUpClient.Text = ""
            ' txtPopupClientSearch.Text = ""
            ClientQuery("Reset")

        End If
        mdlPopUpClient.Show()
    End Sub


    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging

        If String.IsNullOrEmpty(txtPopupClientSearch.Text.Trim) Then
            ClientQuery("Reset")


        Else
            ClientQuery("Search")

        End If


        gvClient.PageIndex = e.NewPageIndex

        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As EventArgs)
        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs)
        txtPopUpClient.Text = "Search Here for AccountID or Client details"
        txtPopupClientSearch.Text = ""
        ClientQuery("Reset")

        mdlPopUpClient.Show()
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else

            ClientQuery("Search")

            mdlPopUpClient.Show()

        End If

        txtPopUpClient.Text = "Search Here for AccountID or Client details"
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
            txtAccountID.Text = ""
        Else
            txtAccountID.Text = gvClient.SelectedRow.Cells(2).Text.Trim
        End If
        If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
            txtServiceLocationID.Text = ""
        Else
            txtServiceLocationID.Text = gvClient.SelectedRow.Cells(3).Text.Trim
        End If
        'If (gvClient.SelectedRow.Cells(4).Text = "&nbsp;") Then
        '    txtCustName.Text = ""
        'Else
        '    txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(4).Text.Trim).ToString
        'End If

    End Sub

    Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
        txtAccountID.Text = ""
        'txtCustName.Text = ""

    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        chkStatusSearch.ClearSelection()
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.Text = ""
        ddlTargetID.SelectedIndex = 0
        txtTeam.Text = ""
        ' txtServiceBy.Text = ""
        ddlIncharge.SelectedIndex = 0
        ddlCompanyGrp.SelectedIndex = 0
        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        'txtCustName.Text = ""

    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click

        RetrieveSelection()

        GenerateWarranty()

        Response.Redirect("RV_ServiceReportWithPhotos.aspx")

    End Sub

    Protected Sub btnSort1_Click(sender As Object, e As EventArgs) Handles btnSort1.Click
        Dim i As Integer = lstSort1.SelectedIndex

        If i = -1 Then
            Exit Sub 'skip if no item is selected
        End If

        lstSort2.Items.Add(lstSort1.Items(i))
        lstSort1.Items.RemoveAt(i)

    End Sub

    Protected Sub btnSort2_Click(sender As Object, e As EventArgs) Handles btnSort2.Click
        Dim i As Integer = lstSort2.SelectedIndex

        If i = -1 Then
            Exit Sub 'skip if no item is selected
        End If

        lstSort1.Items.Add(lstSort2.Items(i))
        lstSort2.Items.RemoveAt(i)

    End Sub

    Private Sub RetrieveSelection()
        Dim selFormula As String
        Dim selection As String
        Dim qry As String = ""
        selection = ""
        selFormula = "{tblservicerecord1.rcno} <> 0"
        qry = "select recordno from tblservicerecord where rcno <> 0 "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If String.IsNullOrEmpty(chkStatusSearch.Text) = False Then
            Dim YrStrList As List(Of [String]) = New List(Of String)()


            For Each item As ListItem In chkStatusSearch.Items
                If item.Selected Then
                    If item.Value = "ALL" Then
                    Else

                        YrStrList.Add("""" + item.Value + """")
                    End If

                End If
            Next


            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

            selFormula = selFormula + " and {tblservicerecord1.Status} in [" + YrStr + "]"
            qry = qry + " and tblservicerecord.Status in (" + YrStr + ")"
            selection = selection + "Status = " + YrStr
        End If
        If String.IsNullOrEmpty(txtServiceRecord.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.Recordno} = '" + txtServiceRecord.Text + "'"
            qry = qry + " and tblservicerecord.Recordno = '" + txtServiceRecord.Text + "'"
            selection = selection + ", Record No = " + txtServiceRecord.Text
        End If
        If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.Contractno} = '" + txtContractNo.Text + "'"
            qry = qry + " and tblservicerecord.Contractno = '" + txtContractNo.Text + "'"
            If selection = "" Then
                selection = "Contract No = " + txtContractNo.Text
            Else
                selection = selection + ", Contract No = " + txtContractNo.Text
            End If

        End If
        If txtServiceID.Text = "-1" Then
        Else

            selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} = '" + txtServiceID.SelectedItem.Text + "'"
            qry = qry + " and tblservicerecord.ServiceID = '" + txtServiceID.SelectedItem.Text + "'"

            selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
        End If
        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            qry = qry + " AND tblservicerecord.serviceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            qry = qry + " AND tblservicerecord.serviceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

            selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
        End If
        If String.IsNullOrEmpty(txtTeam.Text) = False Then

            selFormula = selFormula + " and {tblservicerecord1.TeamID} = '" + txtTeam.Text + "'"
            qry = qry + " and tblservicerecord.TeamID = '" + txtTeam.Text + "'"

            If selection = "" Then
                selection = "TeamID : " + txtTeam.Text
            Else
                selection = selection + ", TeamID : " + txtTeam.Text
            End If
        End If
        If ddlIncharge.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.serviceby} = '" + ddlIncharge.Text + "'"
            qry = qry + " and tblservicerecord.serviceby = '" + ddlIncharge.Text + "'"

            If selection = "" Then
                selection = "ServiceBy : " + ddlIncharge.Text
            Else
                selection = selection + ", ServiceBy : " + ddlIncharge.Text
            End If
        End If

        If ddlTargetID.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecorddet1.TargetID} = '" + ddlTargetID.SelectedItem.Text + "'"
            qry = qry + " and tblservicerecorddet.TargetID = '" + ddlTargetID.SelectedItem.Text + "'"
            If selection = "" Then
                selection = " TargetID = " + ddlTargetID.SelectedItem.Text
            Else
                selection = selection + ", TargetID = " + ddlTargetID.SelectedItem.Text

            End If
        End If

        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

            selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
            qry = qry + " and tblservicerecord.CompanyGroup in (" + YrStr + ")"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            ' qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
        '    End If

        'End If

        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.ContactType} = '" + ddlAccountType.Text + "'"
            qry = qry + " and tblservicerecord.ContactType = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.AccountID} = '" + txtAccountID.Text + "'"
            qry = qry + " and tblservicerecord.AccountID = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID : " + txtAccountID.Text
            Else
                selection = selection + ", AccountID : " + txtAccountID.Text
            End If
        End If

        'If String.IsNullOrEmpty(txtCustName.Text) = False Then
        '    txtCustName.Text = txtCustName.Text.Replace("'", "''")
        '    selFormula = selFormula + " and {tblservicerecord1.CustName} like '*" + txtCustName.Text + "*'"
        '    If selection = "" Then
        '        selection = "CustName : " + txtCustName.Text
        '    Else
        '        selection = selection + ", CustName : " + txtCustName.Text
        '    End If
        '    qry = qry + " and tblservicerecord.CustName like '%" + txtCustName.Text + "%'"
        'End If
        If String.IsNullOrEmpty(txtServiceLocationID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.LocationID} = '" + txtServiceLocationID.Text + "'"
            qry = qry + " and tblservicerecord.LocationID = '" + txtServiceLocationID.Text + "'"
            If selection = "" Then
                selection = "LocationID : " + txtServiceLocationID.Text
            Else
                selection = selection + ", LocationID : " + txtServiceLocationID.Text
            End If
        End If

        If String.IsNullOrEmpty(lstSort2.Text) = False Then
            If lstSort2.Items(0).Selected = True Then


            End If
            Dim YrStrList As List(Of [String]) = New List(Of String)()
            For Each item As ListItem In lstSort2.Items
                If item.Selected Then

                    YrStrList.Add(item.Value)

                End If
            Next



            If YrStrList.Count > 0 Then
                For i As Integer = 0 To YrStrList.Count - 1
                    If i = 0 Then
                        Session.Add("sort1", YrStrList.Item(i).ToString)
                    ElseIf i = 1 Then
                        Session.Add("sort2", YrStrList.Item(i).ToString)
                    ElseIf i = 2 Then
                        Session.Add("sort3", YrStrList.Item(i).ToString)
                    ElseIf i = 3 Then
                        Session.Add("sort4", YrStrList.Item(i).ToString)
                        'ElseIf i = 4 Then
                        '    Session.Add("sort5", YrStrList.Item(i).ToString)

                    End If

                Next
            End If

            '  Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

            '  selFormula = selFormula + " order by " + YrStr
            '  Session.Add("sort", YrStr)
        End If
        txtQuery.Text = qry

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

    End Sub

    Protected Sub GenerateWarranty()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim commandRec As MySqlCommand = New MySqlCommand

            commandRec.CommandType = CommandType.Text

            commandRec.CommandText = txtQuery.Text
            commandRec.Connection = conn

            Dim drRec As MySqlDataReader = commandRec.ExecuteReader()
            Dim dtRec As New System.Data.DataTable
            dtRec.Load(drRec)

            If dtRec.Rows.Count > 0 Then
                For j As Int16 = 0 To dtRec.Rows.Count - 1

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT rcno,frequency,targetid FROM tblservicerecorddet where recordno='" & dtRec.Rows(j)("RecordNo").ToString & "'"
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New System.Data.DataTable
                    dt.Load(dr)

                    Dim warranty As String = ""
                    Dim targetid As String = ""
                    Dim targetdesc As String = ""

                    If dt.Rows.Count > 0 Then
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            warranty = ""
                            If dt.Rows(i)("Frequency").ToString <> "" Then
                                warranty = dt.Rows(i)("Frequency").ToString
                            End If

                            targetid = dt.Rows(i)("TargetID").ToString
                            If String.IsNullOrEmpty(targetid) = False Then
                                Dim stringList As List(Of String) = targetid.Split(","c).ToList()
                                Dim YrStrList As List(Of [String]) = New List(Of String)()
                                targetdesc = ""

                                For Each str As String In stringList

                                    Dim command2 As MySqlCommand = New MySqlCommand

                                    command2.CommandType = CommandType.Text

                                    command2.CommandText = "SELECT descrip1 FROM tbltarget where targetid='" & str.Trim & "'"
                                    command2.Connection = conn

                                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                                    Dim dt2 As New System.Data.DataTable
                                    dt2.Load(dr2)

                                    If dt2.Rows.Count > 0 Then
                                        If targetdesc = "" Then
                                            targetdesc = dt2.Rows(0)("descrip1").ToString
                                        Else
                                            targetdesc = targetdesc + "," + dt2.Rows(0)("descrip1").ToString
                                        End If

                                    End If

                                    command2.Dispose()
                                    dt2.Clear()
                                    dt2.Dispose()
                                    dr2.Close()

                                Next
                                warranty = warranty + " - " + targetdesc

                            End If

                            Dim command As MySqlCommand = New MySqlCommand

                            command.CommandType = CommandType.Text
                            command.CommandText = "update tblservicerecorddet set TargetDescription=@desc where rcno=" & dt.Rows(i)("RcNo")
                            command.Parameters.Clear()
                            command.Parameters.AddWithValue("@desc", warranty)
                            command.Connection = conn
                            command.ExecuteNonQuery()
                            command.Dispose()

                        Next



                    End If
                    command1.Dispose()
                    dt.Clear()
                    dt.Dispose()
                    dr.Close()


                Next
            End If
            commandRec.Dispose()
            dtRec.Clear()
            dtRec.Dispose()
            drRec.Close()
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            ' InsertIntoTblWebEventLog("GenerateWarranty", ex.Message.ToString, txtSvcRecord.Text)
        End Try
    End Sub

    'Protected Sub btnSvcBy_Click(sender As Object, e As ImageClickEventArgs) Handles btnSvcBy.Click
    '    mdlPopUpTeam.TargetControlID = "btnSvcBy"
    '    txtModal.Text = "btnSvcBy"
    '    If String.IsNullOrEmpty(txtServiceBy.Text.Trim) = False Then
    '        txtPopupTeamSearch.Text = txtServiceBy.Text.Trim
    '        txtPopUpTeam.Text = txtPopupTeamSearch.Text
    '        ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
    '        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
    '        SqlDSTeam.DataBind()
    '        gvTeam.DataBind()
    '    Else
    '        'txtPopUpTeam.Text = ""
    '        'txtPopupTeamSearch.Text = ""
    '        '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
    '        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

    '        SqlDSTeam.DataBind()
    '        gvTeam.DataBind()
    '    End If
    '    mdlPopUpTeam.Show()
    'End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'chkStatusSearch.Attributes.Add("onchange", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")

        End If
    End Sub


    Protected Sub btnSvcLocation_Click(sender As Object, e As ImageClickEventArgs) Handles btnSvcLocation.Click
        txtModal.Text = "Location"
        If String.IsNullOrEmpty(txtServiceLocationID.Text.Trim) = False Then
            txtPopUpClient.Text = txtServiceLocationID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

        ElseIf String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

            'ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            '    txtPopUpClient.Text = txtCustName.Text
            '    txtPopupClientSearch.Text = txtPopUpClient.Text
            '    ClientQuery("Search")

        Else
            ClientQuery("Reset")

        End If
        mdlPopUpClient.Show()

    End Sub

    Protected Sub btnPopUpClientReset_Click1(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        txtPopUpClient.Text = "Search Here for AccountID or Client details"
        txtPopupClientSearch.Text = ""
        ClientQuery("Reset")

        mdlPopUpClient.Show()
        ' txtIsPopUp.Text = "Client"
    End Sub

    Protected Sub OnRowDataBoundgClient(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvClient, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgClient(sender As Object, e As EventArgs)
        For Each row As GridViewRow In gvClient.Rows
            If row.RowIndex = gvClient.SelectedIndex Then
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    Private Sub ClientQuery(type As String)
        Dim qry As String = ""

        If type = "Search" Then
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '%" + txtPopupClientSearch.Text + "%' or B.Locationid like '%" + txtPopupClientSearch.Text + "%' or A.contACTperson like '%" + txtPopupClientSearch.Text + "%'))  order by Accountid, LocationId"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblPERSON A Left join tblPersonLocation B on A.Accountid = B.Accountid   where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '%" + txtPopupClientSearch.Text + "%' or B.Locationid like '%" + txtPopupClientSearch.Text + "%' or A.contACTperson like '%" + txtPopupClientSearch.Text + "%'))  order by Accountid, LocationId"
            Else
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '%" + txtPopupClientSearch.Text + "%' or B.Locationid like '%" + txtPopupClientSearch.Text + "%' or A.contACTperson like '%" + txtPopupClientSearch.Text + "%'))"
                qry = qry + " union "
                qry = qry + "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblPERSON A Left join tblPersonLocation B on A.Accountid = B.Accountid   where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '%" + txtPopupClientSearch.Text + "%' or B.Locationid like '%" + txtPopupClientSearch.Text + "%' or A.contACTperson like '%" + txtPopupClientSearch.Text + "%'))  order by Accountid, LocationId"

            End If
        ElseIf type = "Reset" Then
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> ''))  order by Accountid, LocationId"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblPERSON A Left join tblPersonLocation B on A.Accountid = B.Accountid   where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> ''))  order by Accountid, LocationId"
            Else
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> ''))"
                qry = qry + " union "
                qry = qry + "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblPERSON A Left join tblPersonLocation B on A.Accountid = B.Accountid   where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> ''))  order by Accountid, LocationId"

            End If

        End If
        SqlDSClient.SelectCommand = qry
        SqlDSClient.DataBind()
        gvClient.DataBind()

    End Sub

    'Protected Sub btnEmail_Click(sender As Object, e As EventArgs) Handles btnEmail.Click

    '    RetrieveSelection()

    '    Response.Redirect("Email.aspx?Type=ServiceRecordPrinting")
    'End Sub
End Class
