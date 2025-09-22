
Partial Class FilterVehiclePopup
    Inherits System.Web.UI.Page

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
         Dim qry As String
        qry = "select * from tblasset where rcno <> 0"
        If String.IsNullOrEmpty(txtAssetNo.Text) = False Then
            qry = qry + " and assetno = '" + txtAssetNo.Text + "'"
        End If
        If String.IsNullOrEmpty(txtAssetRegNo.Text) = False Then
            qry = qry + " and assetregno = '" + txtAssetRegNo.Text + "'"
        End If

        If String.IsNullOrEmpty(txtStatus.Text) = False Then
            qry = qry + " and status = '" + txtStatus.Text + "'"
        End If
        'MessageBox.Message.Alert(Page, ddlInchargeID.Text + " " + TextBox1.Text, "str")
        If String.IsNullOrEmpty(ddlInchargeID.Text) = False Then

            qry = qry + " and InchargeID = '" + ddlInchargeID.Text + "'"
        End If
        qry = qry + " order by assetno;"
        txtQuery.Text = qry
        Session.Add("Query", txtQuery.Text)
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        '    SqlDataSource2.SelectCommand = "select staffid from tblstaff order by staffid"
        '    SqlDataSource2.DataBind()
        '    ddlInchargeID.DataSourceID = "SqlDataSource2"
        '    ddlInchargeID.DataBind()
        'End If


    End Sub
End Class
