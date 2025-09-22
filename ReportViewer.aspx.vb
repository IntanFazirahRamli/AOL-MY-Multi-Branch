
Partial Class ReportViewer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        CrystalReportViewer1.ReportSource = "CrystalReport.rpt"


        CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
        'CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.ServerName = "localhost"
        CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
        CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
        CrystalReportViewer1.LogOnInfo(0).ConnectionInfo.Password = ConfigurationManager.AppSettings("Password")


        CrystalReportViewer1.RefreshReport()


    End Sub

    Protected Sub CrystalReportViewer1_Init(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Init

    End Sub
End Class
