
Partial Class EventLog
    Inherits System.Web.UI.Page

    Dim selformula As String

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtModule.Text = ""
        txtStaffID.Text = ""
        txtDocRefNo.Text = ""
        txtAccountID.Text = ""
        txtDateFrom.Text = ""
        txtDateTo.Text = ""
        txtComments.Text = ""
    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Dim dt As Date
        If String.IsNullOrEmpty(txtDateFrom.Text) = False Then
            If Date.TryParseExact(txtDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt) Then
                txtDateFrom.Text = dt.ToShortDateString

            Else
                MessageBox.Message.Alert(Page, "Date From is invalid", "str")

                Return
                Exit Sub

            End If
        End If
        Dim dt1 As Date
        If String.IsNullOrEmpty(txtDateTo.Text) = False Then
            If Date.TryParseExact(txtDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt1) Then
                txtDateTo.Text = dt1.ToShortDateString

            Else
                MessageBox.Message.Alert(Page, "Date To is invalid", "str")

                Return
                Exit Sub

            End If
        End If
       Dim qry As String
        qry = "select * from tbleventlog where rcno<>0"
        selformula = "{tbleventlog1.rcno<>0}"
        If String.IsNullOrEmpty(txtModule.Text) = False Then
            qry = qry + " and module = '" + txtModule.Text + "'"
            selformula = selformula + "{tbleventlog1.module}= '" + txtModule.Text + "'"
        End If
        If String.IsNullOrEmpty(txtStaffID.Text) = False Then
            qry = qry + " and StaffID LIKE '%" + txtStaffID.Text + "%'"
            selformula = selformula + "{tbleventlog1.staffid}= '" + txtStaffID.Text + "'"

        End If
        If String.IsNullOrEmpty(txtDocRefNo.Text) = False Then
            qry = qry + " and DocRef = '" + txtDocRefNo.Text + "'"
            selformula = selformula + "{tbleventlog1.docref}= '" + txtDocRefNo.Text + "'"

        End If
        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            qry = qry + " and custcode = '" + txtAccountID.Text + "'"
            selformula = selformula + "{tbleventlog1.custcode}= '" + txtAccountID.Text + "'"

        End If
        If String.IsNullOrEmpty(txtDateFrom.Text) = False Then
            qry = qry + " and LogDate >= '" + Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") + " 00:00:00'"
            selformula = selformula + "{tbleventlog1.logdate} >=" + "#" + dt.ToString("MM-dd-yyyy") + "#"

        End If
        If String.IsNullOrEmpty(txtDateTo.Text) = False Then
            qry = qry + " and LogDate <= '" + Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") + " 23:59:59'"
            selformula = selformula + "{tbleventlog1.logdate} <=" + "#" + dt1.ToString("MM-dd-yyyy") + "#"

        End If
        If String.IsNullOrEmpty(txtComments.Text) = False Then
            qry = qry + " and Comments LIKE '%" + txtComments.Text + "%'"
            selformula = selformula + "{tbleventlog1.comments} like '*" + txtComments.Text + "*'"

        End If

        qry = qry + " order by LogDate desc,rcno desc;"

        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        GridView1.PageIndex = 0
        txt.Text = qry

        If txtRowCount.Text = "0" Then
            '  MakeMeNull()
        End If

        lblMessage.Text = "NUMBER OF RECORDS FOUND : " + txtRowCount.Text

    End Sub

    Protected Sub SqlDataSource1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected
        txtRowCount.Text = e.AffectedRows.ToString

    End Sub

    Protected Sub gvModule_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvModule.PageIndexChanging
        If String.IsNullOrEmpty(txtPopupModuleSearch.Text.Trim) Then
          
            SqlDSModule.SelectCommand = "SELECT distinct * From tblmodule order by module"
        Else
            SqlDSModule.SelectCommand = "SELECT distinct * From tblmodule where Module like '%" + txtPopupModuleSearch.Text.ToUpper + "%' or description like '%" + txtPopupModuleSearch.Text + "%' order by module"
            End If

        SqlDSModule.DataBind()
        gvModule.DataBind()
        gvModule.PageIndex = e.NewPageIndex

        mdlPopupModule.Show()
    End Sub

    Protected Sub gvModule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvModule.SelectedIndexChanged
        If (gvModule.SelectedRow.Cells(1).Text = "&nbsp;") Then
            txtModule.Text = ""
        Else
            txtModule.Text = gvModule.SelectedRow.Cells(1).Text.Trim
        End If

    End Sub

    Protected Sub btnPopUpModuleSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpModuleSearch.Click
        mdlPopupModule.Show()

    End Sub

    Protected Sub txtPopUpModule_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpModule.TextChanged
        If txtPopUpModule.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupModuleSearch.Text = txtPopUpModule.Text
            SqlDSModule.SelectCommand = "SELECT distinct * From tblmodule where Module like '%" + txtPopupModuleSearch.Text.ToUpper + "%' or description like '%" + txtPopupModuleSearch.Text + "%' order by module"

            SqlDSModule.DataBind()
            gvModule.DataBind()
            mdlPopupModule.Show()

        End If

        txtPopUpModule.Text = "Search Here for Module"
    End Sub

    Protected Sub btnPopUpModuleReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpModuleReset.Click
        txtPopUpModule.Text = ""
        txtPopupModuleSearch.Text = ""
      SqlDSModule.SelectCommand = "SELECT distinct * From tblmodule order by module"

        SqlDSModule.DataBind()

        gvModule.DataBind()
        mdlPopupModule.Show()

    End Sub

    Protected Sub btnImgModule_Click(sender As Object, e As ImageClickEventArgs) Handles btnImgModule.Click

     
        If String.IsNullOrEmpty(txtModule.Text.Trim) = False Then
            txtPopUpModule.Text = txtModule.Text
            txtPopupModuleSearch.Text = txtPopUpModule.Text
            SqlDSModule.SelectCommand = "SELECT distinct * From tblmodule where Module like '%" + txtPopupModuleSearch.Text.ToUpper + "%' or description like '%" + txtPopupModuleSearch.Text + "%' order by module"

            SqlDSModule.DataBind()
            gvModule.DataBind()
        Else
          SqlDSModule.SelectCommand = "SELECT distinct * From tblmodule order by module"

            SqlDSModule.DataBind()
            gvModule.DataBind()
        End If
        mdlPopupModule.Show()
    End Sub

    Protected Sub gvStaff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStaff.PageIndexChanging
        If String.IsNullOrEmpty(txtPopupStaffSearch.Text.Trim) Then

            SqlDSStaff.SelectCommand = "SELECT distinct * From tblstaff order by staffid"
        Else
            SqlDSStaff.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"
        End If

        SqlDSStaff.DataBind()
        gvStaff.DataBind()
        gvStaff.PageIndex = e.NewPageIndex

        mdlPopupStaff.Show()
    End Sub

    Protected Sub gvStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStaff.SelectedIndexChanged
        If (gvStaff.SelectedRow.Cells(1).Text = "&nbsp;") Then
            txtStaffID.Text = ""
        Else
            txtStaffID.Text = gvStaff.SelectedRow.Cells(1).Text.Trim
        End If
    End Sub

    Protected Sub btnPopUpStaffSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffSearch.Click
        mdlPopupStaff.Show()

    End Sub

    Protected Sub txtPopUpStaff_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpStaff.TextChanged
        If txtPopUpStaff.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupStaffSearch.Text = txtPopUpStaff.Text
            SqlDSStaff.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

            SqlDSStaff.DataBind()
            gvStaff.DataBind()
            mdlPopupStaff.Show()

        End If

        txtPopUpStaff.Text = "Search Here for Staff"
    End Sub

    Protected Sub btnPopUpStaffReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffReset.Click
        txtPopUpStaff.Text = ""
        txtPopupStaffSearch.Text = ""
        SqlDSStaff.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

        SqlDSStaff.DataBind()

        gvStaff.DataBind()
        mdlPopupStaff.Show()
    End Sub

    Protected Sub btnImgStaff_Click(sender As Object, e As ImageClickEventArgs) Handles btnImgStaff.Click

        If String.IsNullOrEmpty(txtStaffID.Text.Trim) = False Then
            txtPopUpStaff.Text = txtStaffID.Text
            txtPopupStaffSearch.Text = txtPopUpStaff.Text
            SqlDSStaff.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

            SqlDSStaff.DataBind()
            gvStaff.DataBind()
        Else
            SqlDSStaff.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

            SqlDSStaff.DataBind()
            gvStaff.DataBind()
        End If
        mdlPopupStaff.Show()
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
             SqlDataSource1.SelectCommand = "select * from tbleventlog where rcno=0"
            SqlDataSource1.DataBind()
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Session.Add("selFormula", selformula)
        Response.Redirect("RV_EventLog.aspx")
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        SqlDataSource1.SelectCommand = txt.Text
        GridView1.DataBind()
    End Sub
End Class
