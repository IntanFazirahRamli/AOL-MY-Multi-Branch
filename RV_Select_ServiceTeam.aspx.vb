
Partial Class RV_Select_ServiceTeam
    Inherits System.Web.UI.Page

    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs)

        If txtModal.Text = "TeamFrom" Then
            If gvTeam.SelectedRow.Cells(1).Text = "&nbsp;" Then
                txtTeamFrom.Text = " "
            Else
                txtTeamFrom.Text = gvTeam.SelectedRow.Cells(2).Text
            End If
        ElseIf txtModal.Text = "TeamTo" Then
            If gvTeam.SelectedRow.Cells(1).Text = "&nbsp;" Then
                txtTeamTo.Text = " "
            Else
                txtTeamTo.Text = gvTeam.SelectedRow.Cells(2).Text
            End If
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
        txtModal.Text = "TeamFrom"
        If String.IsNullOrEmpty(txtTeamFrom.Text.Trim) = False Then
            txtPopupTeamSearch.Text = txtTeamFrom.Text.Trim
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


    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        Dim selection As String
        selection = ""
        selFormula = "{tblteam1.rcno} <> 0"
      
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblteam1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            '  qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If
        If String.IsNullOrEmpty(txtTeamFrom.Text) = False Then
            selFormula = selFormula + " and {tblteam1.TeamID} >= '" + txtTeamFrom.Text + "'"
            If selection = "" Then
                selection = "TeamID From >= " + txtTeamFrom.Text
            Else
                selection = selection + ", TeamID From >= " + txtTeamFrom.Text
            End If
        End If
        If String.IsNullOrEmpty(txtTeamTo.Text) = False Then
            selFormula = selFormula + " and {tblteam1.TeamID} <= '" + txtTeamTo.Text + "'"
            If selection = "" Then
                selection = "TeamID To <= " + txtTeamTo.Text
            Else
                selection = selection + ", TeamID To <= " + txtTeamTo.Text
            End If
        End If
        If ddlInchargeID.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblTeam1.InchargeID} = '" + ddlInchargeID.Text + "'"
            If selection = "" Then
                selection = "InchargeID = " + ddlInchargeID.Text
            Else
                selection = selection + ", InchargeID = " + ddlInchargeID.Text
            End If
        End If

        If ddl2ndInchargeID.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblTeam1.SecondInchargeID} = '" + ddl2ndInchargeID.Text + "'"
            If selection = "" Then
                selection = "Second InchargeID = " + ddl2ndInchargeID.Text
            Else
                selection = selection + ", Second InchargeID = " + ddl2ndInchargeID.Text
            End If
        End If

        If chkGrouping.SelectedValue = "Team Vehicle" Then
            selFormula = selFormula + " and {tblteamasset1.Type} = 'VEHICLE'"

        ElseIf chkGrouping.SelectedValue = "Tools & Equipment" Then
            selFormula = selFormula + " and {tblteamasset1.Type} = 'EQUIPMENT'"

        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If chkGrouping.SelectedValue = "Team Member" Then
            Session.Add("ReportType", "ServiceTeamMember")
            Response.Redirect("RV_ServiceTeamMember.aspx")
        ElseIf chkGrouping.SelectedValue = "Team Vehicle" Then
            Session.Add("ReportType", "ServiceTeamVehicle")
            Response.Redirect("RV_ServiceTeamVehicle.aspx")
        ElseIf chkGrouping.SelectedValue = "Tools & Equipment" Then
            Session.Add("ReportType", "ServiceTeamEquipment")
            Response.Redirect("RV_ServiceTeamEquipment.aspx")
        End If

    End Sub

    Protected Sub btnTeamTo_Click(sender As Object, e As ImageClickEventArgs) Handles btnTeamTo.Click
        mdlPopUpTeam.TargetControlID = "btnTeamTo"
        txtModal.Text = "TeamTo"
        If String.IsNullOrEmpty(txtTeamTo.Text.Trim) = False Then
            txtPopupTeamSearch.Text = txtTeamTo.Text.Trim
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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class
