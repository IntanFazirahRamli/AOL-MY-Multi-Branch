

Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports System.IO
Imports System.Net
Imports MySql.Data
Imports System.Data
Imports EASendMail
Imports System.Globalization
''Imports System.Net.Mail
Imports System.Text.RegularExpressions




Partial Class DragDrop
    Inherits System.Web.UI.Page


   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Fetch data from mysql database
        'Dim conn As New MySqlConnection("server=localhost;uid=root;password=priya123;database=test; pooling=false;")

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
        conn.Open()


        'conn.Open()
        Dim cmd As String = "select * from drag_drop"
        Dim dAdapter As New MySqlDataAdapter(cmd, conn)
        Dim objDs As New DataSet()
        dAdapter.Fill(objDs)
        GridView1.DataSource = objDs.Tables(0)
        GridView1.DataBind()
        Dim dt As New DataTable()
        objDs.Tables(0).Clear()
        dt = objDs.Tables(0)
        dt.Rows.Add()
        DetailsView1.DataSource = dt
        DetailsView1.DataBind()
    End Sub

   
    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.className='block sel-row'")
            e.Row.Attributes.Add("onmouseout", "this.className='block'")
        End If
    End Sub
End Class
