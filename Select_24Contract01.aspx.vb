Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization

Public Class Select_24Contract01
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs)
        Dim selFormula As String
        selFormula = "{tblContract1.rcno} <> 0"

        If String.IsNullOrEmpty(chkListStatus.Text) = False Then
            Dim listStatus As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In chkListStatus.Items
                If item.Selected Then
                    If item.Value = "ALL" Then
                    Else
                        listStatus.Add("""" + item.Value + """")
                    End If
                End If
            Next

            Dim strStatus As [String] = [String].Join(",", listStatus.ToArray())
            selFormula = selFormula + " and {tblContract1.Status} in [" + strStatus + "]"
        End If

        If String.IsNullOrEmpty(chkListRenewalSt.Text) = False Then
            Dim listRenewalSt As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In chkListRenewalSt.Items
                If item.Selected Then
                    If item.Value = "ALL" Then
                    Else
                        listRenewalSt.Add("""" + item.Value + """")
                    End If
                End If
            Next

            Dim strRenewalSt As [String] = [String].Join(",", listRenewalSt.ToArray())
            selFormula = selFormula + " and {tblContract1.RenewalSt} in [" + strRenewalSt + "]"
        End If

        If String.IsNullOrEmpty(chkListNotedSt.Text) = False Then
            Dim listNotedSt As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In chkListNotedSt.Items
                If item.Selected Then
                    If item.Value = "ALL" Then
                    Else
                        listNotedSt.Add("""" + item.Value + """")
                    End If
                End If
            Next

            Dim strNotedSt As [String] = [String].Join(",", listNotedSt.ToArray())
            selFormula = selFormula + " and {tblContract1.NotedSt} in [" + strNotedSt + "]"
        End If

        If String.IsNullOrEmpty(chkListSettlementType.Text) = False Then
            Dim listSettle As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In chkListSettlementType.Items
                If item.Selected Then
                    If item.Value = "ALL" Then
                    Else
                        listSettle.Add("""" + item.Value + """")
                    End If
                End If
            Next

            Dim strSettle As [String] = [String].Join(",", listSettle.ToArray())
            selFormula = selFormula + " and {tblContract1.Settle} in [" + strSettle + "]"
        End If

        If String.IsNullOrEmpty(txtCompanyGrp.Text.Trim) = False Then
            selFormula = selFormula + " and {tblContract1.CompanyGroup} = '" + txtCompanyGrp.Text.Trim + "'"
        End If

        If String.IsNullOrEmpty(txtContractGrp.Text.Trim) = False Then
            selFormula = selFormula + " and {tblContract1.ContractGroup} = '" + txtContractGrp.Text.Trim + "'"
        End If

        If String.IsNullOrEmpty(txtContractDt.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtContractDt.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Contract Date From is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblContract1.ContractDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        End If

        If String.IsNullOrEmpty(txtContractDtTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtContractDtTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Contract Date To is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblContract1.ContractDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        End If

        If String.IsNullOrEmpty(txtStartDt.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtStartDt.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Start Date From is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblContract1.StartDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        End If

        If String.IsNullOrEmpty(txtStartDtTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtStartDtTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Start Date To is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblContract1.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        End If

        If String.IsNullOrEmpty(txtEndDt.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtEndDt.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "End Date From is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblContract1.EndDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        End If

        If String.IsNullOrEmpty(txtEndDtTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtEndDtTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "End Date To is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblContract1.EndDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        End If

        If String.IsNullOrEmpty(txtActualEnd.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtActualEnd.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Actual End From is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblContract1.ActualEnd} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        End If

        If String.IsNullOrEmpty(txtActualEndTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtActualEndTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Actual End To is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblContract1.ActualEnd} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        End If

        'If (listSort2.Items.Count > 0) Then
        '    Dim lstSort As List(Of [String]) = New List(Of String)()

        '    For Each item As ListItem In listSort2.Items
        '        If item.Selected Then
        '            lstSort.Add(item.Value.ToString)
        '        End If
        '    Next

        '    Dim strSort As [String] = [String].Join(",", lstSort.ToArray())
        '    selFormula = selFormula + " order by " + strSort
        'End If

        Session.Add("selFormula", selFormula)
        Response.Redirect("RV_Contract01.aspx")
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        chkListStatus.Items(0).Selected = False
        chkListStatus.Items(1).Selected = False
        chkListStatus.Items(2).Selected = False
        chkListStatus.Items(3).Selected = False
        chkListStatus.Items(4).Selected = False
        chkListStatus.Items(5).Selected = False
        chkListStatus.Items(6).Selected = False
        chkListStatus.Items(7).Selected = False
        chkListStatus.Items(8).Selected = False

        chkListRenewalSt.Items(0).Selected = False
        chkListRenewalSt.Items(1).Selected = False
        chkListRenewalSt.Items(2).Selected = False
        chkListRenewalSt.Items(3).Selected = False
        chkListRenewalSt.Items(4).Selected = False

        chkListNotedSt.Items(0).Selected = False
        chkListNotedSt.Items(1).Selected = False
        chkListNotedSt.Items(2).Selected = False

        chkListSettlementType.Items(0).Selected = False
        chkListSettlementType.Items(1).Selected = False
        chkListSettlementType.Items(2).Selected = False
        chkListSettlementType.Items(3).Selected = False

        txtCompanyGrp.Text = ""
        txtContractGrp.Text = ""
        'txtClientId.Text = ""
        'txtClientName.Text = ""
        'txtClientType.Text = ""
        'txtScheduler.Text = ""
        'txtSalesman.Text = ""
        'txtTechSupport.Text = ""
        'txtServiceId.Text = ""
        'txtIncharge.Text = ""
        txtActualEnd.Text = ""
        txtActualEndTo.Text = ""
        txtStartDt.Text = ""
        txtStartDtTo.Text = ""
        txtContractDt.Text = ""
        txtContractDtTo.Text = ""
        txtEndDt.Text = ""
        txtEndDtTo.Text = ""

        listSort2.Items.Clear()
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        Response.Redirect("Home.aspx")
    End Sub

    Protected Sub gvCompanyGrp_SelectedIndexChanged(sender As Object, e As EventArgs)
        If (gvCompanyGrp.SelectedRow.Cells(1).Text = "&nbsp;") Then
            txtCompanyGrp.Text = ""
        Else
            txtCompanyGrp.Text = gvCompanyGrp.SelectedRow.Cells(1).Text.Trim
        End If
    End Sub

    Protected Sub gvContractGrp_SelectedIndexChanged(sender As Object, e As EventArgs)
        If (gvContractGrp.SelectedRow.Cells(1).Text = "&nbsp;") Then
            txtContractGrp.Text = ""
        Else
            txtContractGrp.Text = gvContractGrp.SelectedRow.Cells(1).Text.Trim
        End If
    End Sub

    'Protected Sub btnPopUpClientSearch_Click(sender As Object, e As EventArgs) Handles btnPopUpClientSearch.Click
    '    If txtPopUpClient.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter client name", "str")
    '    Else
    '        SqlDSClient.SelectCommand = "Select distinct * From tblContactMaster where rcno <>0 And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%'"
    '        SqlDSClient.DataBind()
    '        gvClient.DataBind()
    '        mdlPopUpClient.Show()
    '    End If
    'End Sub

    'Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs) Handles btnPopUpClientReset.Click
    '    txtPopUpClient.Text = ""
    '    ''SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%'"
    '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 "
    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    mdlPopUpClient.Show()
    'End Sub

    'Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
    '        txtClientId.Text = ""
    '    Else
    '        txtClientId.Text = gvClient.SelectedRow.Cells(1).Text.Trim
    '    End If

    '    If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
    '        txtClientName.Text = ""
    '    Else
    '        txtClientName.Text = gvClient.SelectedRow.Cells(2).Text.Trim
    '    End If

    '    If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
    '        txtClientType.Text = ""
    '    Else
    '        txtClientType.Text = gvClient.SelectedRow.Cells(3).Text.Trim
    '    End If
    'End Sub

    'Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging
    '    gvClient.PageIndex = e.NewPageIndex
    '    If txtPopUpClient.Text.Trim = "" Then
    '        SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 "
    '    Else
    '        SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%'"
    '    End If
    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    mdlPopUpClient.Show()
    'End Sub

    'Protected Sub btnPopUpSalesmanSearch_Click(sender As Object, e As EventArgs) Handles btnPopUpSalesmanSearch.Click
    '    If txtPopUpSalesman.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter Salesman", "str")
    '    Else
    '        SqlDSSalesman.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 and upper(StaffId) Like '%" + txtPopUpSalesman.Text.Trim.ToUpper + "%'"
    '        SqlDSSalesman.DataBind()
    '        gvSalesman.DataBind()
    '        mdlPopUpSalesman.Show()
    '    End If
    'End Sub

    'Protected Sub btnPopUpSalesmanReset_Click(sender As Object, e As EventArgs) Handles btnPopUpSalesmanReset.Click
    '    txtPopUpSalesman.Text = ""
    '    SqlDSSalesman.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 "
    '    SqlDSSalesman.DataBind()
    '    gvSalesman.DataBind()
    '    mdlPopUpSalesman.Show()
    'End Sub

    'Protected Sub gvSalesman_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If (gvSalesman.SelectedRow.Cells(2).Text = "&nbsp;") Then
    '        txtSalesman.Text = ""
    '    Else
    '        txtSalesman.Text = gvSalesman.SelectedRow.Cells(2).Text.Trim
    '    End If
    'End Sub

    'Protected Sub gvSalesman_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSalesman.PageIndexChanging
    '    gvSalesman.PageIndex = e.NewPageIndex
    '    If txtPopUpSalesman.Text.Trim = "" Then
    '        SqlDSSalesman.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 "
    '    Else
    '        SqlDSSalesman.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 And upper(StaffId) Like '%" + txtPopUpSalesman.Text.Trim.ToUpper + "%'"
    '    End If
    '    SqlDSSalesman.DataBind()
    '    gvSalesman.DataBind()
    '    mdlPopUpSalesman.Show()
    'End Sub

    'Protected Sub btnPopUpSchedulerSearch_Click(sender As Object, e As EventArgs) Handles btnPopUpSchedulerSearch.Click
    '    If txtPopUpScheduler.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter Scheduler", "str")
    '    Else
    '        SqlDSScheduler.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 and upper(StaffId) Like '%" + txtPopUpSalesman.Text.Trim.ToUpper + "%'"
    '        SqlDSScheduler.DataBind()
    '        gvScheduler.DataBind()
    '        mdlPopUpScheduler.Show()
    '    End If
    'End Sub

    'Protected Sub btnPopUpSchedulerReset_Click(sender As Object, e As EventArgs) Handles btnPopUpSchedulerReset.Click
    '    txtPopUpScheduler.Text = ""
    '    SqlDSScheduler.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 "
    '    SqlDSScheduler.DataBind()
    '    gvScheduler.DataBind()
    '    mdlPopUpScheduler.Show()
    'End Sub

    'Protected Sub gvScheduler_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If (gvScheduler.SelectedRow.Cells(2).Text = "&nbsp;") Then
    '        txtScheduler.Text = ""
    '    Else
    '        txtScheduler.Text = gvScheduler.SelectedRow.Cells(2).Text.Trim
    '    End If
    'End Sub

    'Protected Sub gvScheduler_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvScheduler.PageIndexChanging
    '    gvScheduler.PageIndex = e.NewPageIndex
    '    If txtPopUpScheduler.Text.Trim = "" Then
    '        SqlDSScheduler.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 "
    '    Else
    '        SqlDSScheduler.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 And upper(StaffId) Like '%" + txtPopUpSalesman.Text.Trim.ToUpper + "%'"
    '    End If
    '    SqlDSScheduler.DataBind()
    '    gvScheduler.DataBind()
    '    mdlPopUpScheduler.Show()
    'End Sub

    'Protected Sub btnPopUpTechSupportSearch_Click(sender As Object, e As EventArgs) Handles btnPopUpTechSupportSearch.Click
    '    If txtPopUpTechSupport.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter Technical Support", "str")
    '    Else
    '        SqlDSTechSupport.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 and upper(StaffId) Like '%" + txtPopUpTechSupport.Text.Trim.ToUpper + "%'"
    '        SqlDSTechSupport.DataBind()
    '        gvTechSupport.DataBind()
    '        mdlPopUpTechSupport.Show()
    '    End If
    'End Sub

    'Protected Sub btnPopUpTechSupportReset_Click(sender As Object, e As EventArgs) Handles btnPopUpTechSupportReset.Click
    '    txtPopUpTechSupport.Text = ""
    '    SqlDSTechSupport.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 "
    '    SqlDSTechSupport.DataBind()
    '    gvTechSupport.DataBind()
    '    mdlPopUpTechSupport.Show()
    'End Sub

    'Protected Sub gvTechSupport_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If (gvTechSupport.SelectedRow.Cells(2).Text = "&nbsp;") Then
    '        txtTechSupport.Text = ""
    '    Else
    '        txtTechSupport.Text = gvTechSupport.SelectedRow.Cells(2).Text.Trim
    '    End If
    'End Sub

    'Protected Sub gvTechSupport_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTechSupport.PageIndexChanging
    '    gvTechSupport.PageIndex = e.NewPageIndex
    '    If txtPopUpTechSupport.Text.Trim = "" Then
    '        SqlDSTechSupport.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 "
    '    Else
    '        SqlDSTechSupport.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 And upper(StaffId) Like '%" + txtPopUpSalesman.Text.Trim.ToUpper + "%'"
    '    End If
    '    SqlDSTechSupport.DataBind()
    '    gvTechSupport.DataBind()
    '    mdlPopUpTechSupport.Show()
    'End Sub

    'Protected Sub btnPopUpInchargeSearch_Click(sender As Object, e As EventArgs) Handles btnPopUpInchargeSearch.Click
    '    If txtPopUpIncharge.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter Incharge", "str")
    '    Else
    '        SqlDSIncharge.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and upper(InchargeId) Like '%" + txtPopUpIncharge.Text.Trim.ToUpper + "%'"
    '        SqlDSIncharge.DataBind()
    '        gvIncharge.DataBind()
    '        mdlPopUpIncharge.Show()
    '    End If
    'End Sub

    'Protected Sub btnPopUpInchargeReset_Click(sender As Object, e As EventArgs) Handles btnPopUpInchargeReset.Click
    '    txtPopUpIncharge.Text = ""
    '    SqlDSIncharge.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 "
    '    SqlDSIncharge.DataBind()
    '    gvIncharge.DataBind()
    '    mdlPopUpIncharge.Show()
    'End Sub

    'Protected Sub gvIncharge_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If (gvIncharge.SelectedRow.Cells(1).Text = "&nbsp;") Then
    '        txtIncharge.Text = ""
    '    Else
    '        txtIncharge.Text = gvIncharge.SelectedRow.Cells(1).Text.Trim
    '    End If
    'End Sub

    'Protected Sub gvIncharge_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvIncharge.PageIndexChanging
    '    gvIncharge.PageIndex = e.NewPageIndex
    '    If txtPopUpIncharge.Text.Trim = "" Then
    '        SqlDSIncharge.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 "
    '    Else
    '        SqlDSIncharge.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 And upper(InchargeId) Like '%" + txtPopUpIncharge.Text.Trim.ToUpper + "%'"
    '    End If
    '    SqlDSIncharge.DataBind()
    '    gvIncharge.DataBind()
    '    mdlPopUpIncharge.Show()
    'End Sub

    'Protected Sub btnPopUpServiceIdSearch_Click(sender As Object, e As EventArgs) Handles btnPopUpServiceIdSearch.Click
    '    If txtPopUpServiceId.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter Service Id", "str")
    '    Else
    '        SqlDSServiceId.SelectCommand = "SELECT distinct * From tblProduct where rcno <>0 and upper(ProductId) Like '%" + txtPopUpServiceId.Text.Trim.ToUpper + "%'"
    '        SqlDSServiceId.DataBind()
    '        gvServiceId.DataBind()
    '        mdlPopUpServiceId.Show()
    '    End If
    'End Sub

    'Protected Sub btnPopUpServiceIdReset_Click(sender As Object, e As EventArgs) Handles btnPopUpServiceIdReset.Click
    '    txtPopUpServiceId.Text = ""
    '    SqlDSServiceId.SelectCommand = "SELECT distinct * From tblProduct where rcno <>0 "
    '    SqlDSServiceId.DataBind()
    '    gvServiceId.DataBind()
    '    mdlPopUpServiceId.Show()
    'End Sub

    'Protected Sub gvServiceId_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If (gvServiceId.SelectedRow.Cells(1).Text = "&nbsp;") Then
    '        txtServiceId.Text = ""
    '    Else
    '        txtServiceId.Text = gvServiceId.SelectedRow.Cells(1).Text.Trim
    '    End If
    'End Sub

    'Protected Sub gvServiceId_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvServiceId.PageIndexChanging
    '    gvServiceId.PageIndex = e.NewPageIndex
    '    If txtPopUpServiceId.Text.Trim = "" Then
    '        SqlDSServiceId.SelectCommand = "SELECT distinct * From tblProduct where rcno <>0 "
    '    Else
    '        SqlDSServiceId.SelectCommand = "SELECT distinct * From tblProduct where rcno <>0 And upper(ProductId) Like '%" + txtPopUpServiceId.Text.Trim.ToUpper + "%'"
    '    End If
    '    SqlDSServiceId.DataBind()
    '    gvServiceId.DataBind()
    '    mdlPopUpServiceId.Show()
    'End Sub

    Protected Sub btnSortMove_Click(sender As Object, e As EventArgs)
        For Each item As ListItem In listSort1.Items
            If item.Selected Then
                listSort2.Items.Add(item)
            End If
        Next
    End Sub

    Protected Sub btnSortRemove_Click(sender As Object, e As EventArgs)
        For i As Int32 = listSort2.Items.Count - 1 To 0 Step -1
            If (listSort2.Items(i).Selected = True) Then
                listSort2.Items.RemoveAt(i)
            End If
        Next
    End Sub

End Class