Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class SelectContact
    Inherits System.Web.UI.Page

    Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

    Public rcno As String

    
    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        txtActive.Text = GridView2.SelectedRow.Cells(2).Text
        Session.Add("BillContact", txtActive.Text)
        ' Response.Write("<script>window.close();</script>")
    End Sub
End Class
