

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data


Partial Class Master_AssetSetup
    Inherits System.Web.UI.Page

    Public rcno As String

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

        txtEmailNotification.Enabled = False

    End Sub

    Private Sub DisableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        txtEmailNotification.Enabled = True

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            EnableControls()

            RetrieveData()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")

    End Sub

    Private Sub RetrieveData()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT AssetEmailNotification FROM tblservicerecordmastersetup where rcno=1"
        '  command1.Parameters.AddWithValue("@city", txtCity.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then

            txtEmailNotification.Text = dt.Rows(0)("AssetEmailNotification").ToString

        End If

    End Sub

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtEmailNotification.Text = ""

        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
    
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        '  MakeMeNull()
        RetrieveData()
        EnableControls()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        '   Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()



        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblservicerecordmastersetup where rcno=1"
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry As String
            qry = "UPDATE tblservicerecordmastersetup SET AssetEmailNotification=@AssetEmailNotification WHERE RcNo = 1;"

            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@AssetEmailNotification", txtEmailNotification.Text)
        
            'command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            'command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


            command.Connection = conn

            command.ExecuteNonQuery()

            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
            lblAlert.Text = ""
            command.Dispose()

        End If

        conn.Close()
        conn.Dispose()

   
        'Catch ex As Exception
        '    MessageBox.Message.Alert(Page, "Error!!!", "str")
        'End Try
        EnableControls()

    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")

    End Sub
End Class
