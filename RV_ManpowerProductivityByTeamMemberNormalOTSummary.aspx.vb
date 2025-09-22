

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO
Imports System.Data


Partial Class RV_ManpowerProductivityByTeamMemberNormalOTSummary
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

                Dim repPath As String = Server.MapPath("~/Reports/ManagementReports/ManpowerProductivityByTeamMemberNormalOTSummary.rpt")
                crReportDocument.Load(repPath)

                crReportDocument.SetParameterValue("pselectedFields", Convert.ToString(Session("selection")))
                crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))


                Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()

                Dim myTables As Tables = crReportDocument.Database.Tables

                For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                    Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
                    myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
                    myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                    myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
                    myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")
                    myTable.ApplyLogOnInfo(myTableLogonInfo)
                    myTableLogonInfo.ConnectionInfo = myConnectionInfo

                Next

                CrystalReportViewer1.ReportSource = crReportDocument
                CrystalReportViewer1.SelectionFormula = Convert.ToString(Session("selFormula"))

            Else
                CrystalReportViewer1.RefreshReport()
            End If
        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"

            Using w As StreamWriter = File.AppendText(ErrOtLo)
                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
            End Using

        End Try
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        '   Response.Redirect("Reports.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()
            Dim attachment As String = "attachment; filename=ManpowerProductivity.xlsx"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.ms-excel"
            Dim tab As String = ""
            For Each dc As DataColumn In dt.Columns
                Response.Write(tab + dc.ColumnName)
                tab = vbTab
            Next
            Response.Write(vbLf)
            Dim i As Integer
            For Each dr As DataRow In dt.Rows
                tab = ""
                For i = 0 To dt.Columns.Count - 1
                    Response.Write(tab & dr(i).ToString())
                    tab = vbTab
                Next
                Response.Write(vbLf)
            Next
            Response.[End]()

            dt.Clear()

        End If
    End Sub

    Private Function GetData() As Boolean
        'Dim qry As String = "SELECT staffid,"
        'qry = qry + "(select count(staffid) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid"
        'qry = qry + " group by staffid) as 'NoofNormalSvcs'"
        'qry = qry + " FROM tblrptserviceanalysis a where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm'"
        'qry = qry + "group by staffid order by staffid"

        'txtQuery.Text = qry
        'Return True
        Dim qry As String = "SELECT StaffID,"
        qry = qry + "ifnull((select count(staffid) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid"
        qry = qry + " group by staffid),0) as 'NoofNormalSvcs',"

        qry = qry + "ifnull((select format(sum(servicevalue),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1 and staffid=a.staffid"
        qry = qry + " group by staffid),0) as NormalHourValue,"

        qry = qry + "format(sum(normalhour/60),2) as NormalHourSpent,"

        qry = qry + "ifnull((select count(staffid) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2) and staffid=a.staffid"
        qry = qry + " group by staffid),0) as NoofOTSvcs,"
        qry = qry + "ifnull((select format(sum(servicevalue),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1.5,2) and staffid=a.staffid"
        qry = qry + " group by staffid),0) as OTHourValue,"
        qry = qry + "ifnull((select format(sum(OThour/60),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=1.5 and staffid=a.staffid"
        qry = qry + " group by staffid),0) as 'OT1.5HourSpent',"
        qry = qry + "ifnull((select format(sum(OThour/60),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate=2 and staffid=a.staffid"
        qry = qry + " group by staffid),0) as 'OT2.0HourSpent',"
        qry = qry + "ifnull((select format(sum(servicevalue),2) FROM tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and otrate in (1,1.5,2) and staffid=a.staffid"
        qry = qry + " group by staffid),0) as TotalValue"

        qry = qry + " FROM tblrptserviceanalysis a where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm' and staffid <> ''"
        qry = qry + " group by staffid order by staffid"

        txtQuery.Text = qry
        Return True
    End Function

    Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQuery.Text

        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function
End Class