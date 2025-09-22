
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class RV_Select_MonthlyCatchSummary
    Inherits System.Web.UI.Page
 


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

            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

            mdlPopUpClient.Show()

        End If

        txtPopUpClient.Text = "Search Here for AccountID or Client details"
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
      
        If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
            txtServiceLocationID.Text = ""
        Else
            txtServiceLocationID.Text = gvClient.SelectedRow.Cells(1).Text.Trim
        End If
     

    End Sub

 

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtServiceLocationID.Text = ""

    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If String.IsNullOrEmpty(txtServiceLocationID.Text) Then
            lblAlert.Text = "ENTER LOCATION ID"
            Exit Sub
        End If
        'If String.IsNullOrEmpty(txtSvcDateFrom.Text) Then
        '    lblAlert.Text = "ENTER SERVICE MONTH"
        '    Exit Sub

        'End If
        RetrieveSelection()
        InsertIntoTblWebEventLog("Criteria", Session("selFormula"), Session("UserID"))
        Dim url As String = "RV_Export_MonthlyCatchSummaryReport.aspx"
        '   Dim s As String = "window.open('" & url + "', 'Monthly Catch Services Summary Report');"
        Dim s As String = "window.open('" & url + "', '_blank');"
        ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

        ' Response.Redirect("RV_MonthlyCatchSummary.aspx")

    End Sub

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "SMARTSummaryReport")
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

            ' lblAlert.Text = errorMsg
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        Catch ex As Exception
            '  InsertIntoTblWebEventLog("COMPANY.ASPX" + txtCreatedBy.Text, "InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub

    Private Sub RetrieveSelection()
        Dim selFormula As String
        Dim selection As String

        selection = ""
        selFormula = "{tblpest1.ShowCatchDataInSmartPortal}=TRUE"
        ' selFormula = "{tblcompanylocation1.LocationID}<>''"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblcompanylocation1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
          
        End If

      
        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "MMM yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            Session.Add("SMARTStartDate", d.ToString("01-01-yyyy").ToString)
            selFormula = selFormula + " and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-01-yyyy") + "#"
            Session.Add("SMARTEndDate", d.AddMonths(1).AddDays(-1).ToString("dd-MM-yyyy").ToString)
            selFormula = selFormula + " and {tblservicerecord1.ServiceDate} <=" + "#" + d.AddMonths(1).AddDays(-1).ToString("MM-dd-yyyy") + "#"
         End If
      
        If String.IsNullOrEmpty(txtServiceLocationID.Text) = False Then
            selFormula = selFormula + " and {tblcompanylocation1.LocationID} = '" + txtServiceLocationID.Text + "'"
      
        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

    End Sub

   

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
     
    End Sub


    Protected Sub btnSvcLocation_Click(sender As Object, e As ImageClickEventArgs) Handles btnSvcLocation.Click
        If String.IsNullOrEmpty(txtServiceLocationID.Text.Trim) = False Then
            txtPopUpClient.Text = txtServiceLocationID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

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
          
            qry = "(SELECT  b.LocationId, b.Address1 as ServiceAddress1 From tblCompanyLocation B where B.SmartCustomer=1 and (B.Locationid like '%" + txtPopupClientSearch.Text + "%' or B.Address1 like '%" + txtPopupClientSearch.Text + "%'))"
                qry = qry + " union "
            qry = qry + "(SELECT  b.LocationId, b.Address1 as ServiceAddress1 From tblPersonLocation B where B.SmartCustomer=1 and (B.Locationid like '%" + txtPopupClientSearch.Text + "%' or B.Address1 like '%" + txtPopupClientSearch.Text + "%'))"

        ElseIf type = "Reset" Then
           
            qry = "(SELECT  b.LocationId, b.Address1 as ServiceAddress1 From tblCompanyLocation B where B.SmartCustomer=1)"
            qry = qry + " union "
            qry = qry + "(SELECT  b.LocationId, b.Address1 as ServiceAddress1 From tblPersonLocation B where B.SmartCustomer=1)"

        End If
        SqlDSClient.SelectCommand = qry
        SqlDSClient.DataBind()
        gvClient.DataBind()

    End Sub

End Class

