Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data

Partial Class RV_Select_UnprintedInvoiceListing
    Inherits System.Web.UI.Page

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
       
        If GetData() = True Then
            'lblAlert.Text = txtQuery.Text

            'Return

            Dim dt As DataTable = GetDataSet()


            Dim attachment As String = "attachment; filename=UnPrintedInvoice.xls"
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

    Private Function GetData() As Boolean

        Dim qry As String = ""
        qry = "select CreatedBy,InvoiceNumber,AccountID,CustName from tblSales where PostStatus='P' and DocType='ARIN' and (PrintCounter=0 or PrintCounter='' or PrintCounter is null)"


        Dim selFormula As String
        Dim selection As String
        selection = ""
        ' selFormula = "{tblcontract1.rcno} <> 0 and {tblcontract1.status}<>""V"" and {tblcontract1.status}=""O"" and {tblcontract1.status}<>""P"" "
        selFormula = "{tblsales1.PostStatus}=""P"" AND {tblsales1.DocType}=""ARIN"" AND (isnull({tblsales1.PrintCounter}) or {tblsales1.PrintCounter}="""" or {tblsales1.PrintCounter}=""0"")"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblsales1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblsales.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID INVOICE FROM DATE"
                Return False
            End If
            qry = qry + " and tblsales.salesdate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {tblsales1.SalesDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Invoice Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Invoice Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID INVOICE TO DATE"
                Return False
            End If
            qry = qry + " and tblsales.salesdate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {tblsales1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Invoice Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Invoice Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        qry = qry + " order by CreatedBy; "
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        txtQuery.Text = qry

        Return True

    End Function

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If GetData() = True Then
            Session.Add("ReportType", "UnPrintedInvoice")
            Response.Redirect("RV_UnPrintedInvoice.aspx")
          
        Else
            Return

        End If
    End Sub
End Class
