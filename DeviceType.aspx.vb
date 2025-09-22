Imports Microsoft.VisualBasic
Imports System
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI

#Region "Device Type"
Partial Class DeviceType
    Inherits System.Web.UI.Page

    'Public Logo1uploadedfilename As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            loadGrid()
        End If
    End Sub


    Private Sub loadGrid()

        Dim list As New List(Of DeviceTypeModel)

        Dim bytes As Byte()
        Dim base64String As String = ""

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text


                Dim insQuery As String = "SELECT * FROM tbldevicetype"

                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                Do While reader.Read()

                    Dim item As New DeviceTypeModel()

                    item.RcNo = CType(reader("RcNo"), Integer)

                    If Not IsDBNull(reader("DeviceType")) Then
                        item.DeviceType = CType(reader("DeviceType"), String)
                    Else
                        item.DeviceType = ""
                    End If

                    If Not IsDBNull(reader("DeviceDescription")) Then
                        item.DeviceDescription = CType(reader("DeviceDescription"), String)
                    Else
                        item.DeviceDescription = ""
                    End If

                    If Not IsDBNull(TryCast(reader("Icon"), Byte())) Then
                        bytes = TryCast(reader("Icon"), Byte())

                        Try
                            base64String = Convert.ToBase64String(bytes, 0, bytes.Length)
                            item.ImageURL = "data:image/png;base64," & base64String
                        Catch ex As Exception
                            base64String = ""
                            item.ImageURL = "~/Images/noImage.png"
                        End Try

                    End If


                    list.Add(item)
                Loop
                con.Close()
            End Using

            DeviceTypeGrid.DataSource = list
            DeviceTypeGrid.DataBind()
        End Using

    End Sub
    Protected Sub btnAddDeviceType_Click(sender As Object, e As EventArgs)
        HiddenisLogoClear.Value = 0
        hiddenRcNo.Value = 0
        txtDeviceType.Text = ""
        txtDeviceType.Enabled = True
        txtDeviceDescription.Text = ""
        'Image1.ImageUrl = Nothing
        lblPopupHeading.Text = "Create New Device Type"
        Image1.ImageUrl = "~/Images/noImage.png"
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showpopup();", True)

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Using row1 As GridViewRow = CType((CType(sender, LinkButton)).Parent.Parent, GridViewRow)
            Dim idx As Integer = row1.RowIndex

            If idx <= DeviceTypeGrid.Rows.Count Then

                Dim bytes As Byte()
                Dim base64String As String = ""


                Dim row As GridViewRow = DeviceTypeGrid.Rows(idx)
                Dim hdnRcNo As Label = TryCast(row.FindControl("hdnRcNo"), Label)
                Dim lblDeviceType As Label = TryCast(row.FindControl("lblDeviceType"), Label)
                Dim lblDeviceDescription As Label = TryCast(row.FindControl("lblDeviceDescription"), Label)

                Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                Using con As New MySqlConnection(constr)
                    Using cmd As New MySqlCommand()
                        cmd.CommandType = CommandType.Text
                        Dim insQuery As String = "SELECT * FROM tbldevicetype WHERE RcNo =  " & hdnRcNo.Text & ""

                        cmd.CommandText = insQuery
                        cmd.Connection = con
                        con.Open()
                        Dim reader As MySqlDataReader = cmd.ExecuteReader()
                        Do While reader.Read()
                            If Not IsDBNull(TryCast(reader("Icon"), Byte())) Then
                                bytes = TryCast(reader("Icon"), Byte())

                                Try
                                    base64String = Convert.ToBase64String(bytes, 0, bytes.Length)
                                Catch ex As Exception
                                    base64String = ""
                                End Try

                            End If

                        Loop
                        con.Close()
                    End Using
                End Using
                HiddenisLogoClear.Value = 0
                hiddenRcNo.Value = hdnRcNo.Text
                txtDeviceType.Text = lblDeviceType.Text
                txtDeviceType.Enabled = False
                txtDeviceDescription.Text = lblDeviceDescription.Text
                txtDeviceType.ForeColor = System.Drawing.Color.Black

                If (base64String <> "") Then
                    Image1.ImageUrl = "data:image/png;base64," & base64String
                Else
                    Image1.ImageUrl = "~/Images/noImage.png"
                End If
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "showpopup();", True)
            End If
        End Using

        lblPopupHeading.Text = "Edit Device Type"

        loadGrid()
    End Sub
    Protected Sub btnSaveDevicetype_Click(sender As Object, e As EventArgs)

        'Dim contentType As String = avatarUpload.PostedFile.ContentType

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        If hiddenRcNo.Value = 0 Then

            'Add new Device Type
            Using con As New MySqlConnection(constr)
                Dim query As String = "INSERT INTO tbldevicetype(DeviceType,DeviceDescription) VALUES(@DeviceType,@DeviceDescription)"
                Using cmd As New MySqlCommand(query)
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@DeviceType", txtDeviceType.Text)
                    cmd.Parameters.AddWithValue("@DeviceDescription", txtDeviceDescription.Text)
                    con.Open()
                    cmd.ExecuteNonQuery()

                    'If Not cmd.LastInsertedId Is Nothing Then
                    cmd.Parameters.AddWithValue("newId", cmd.LastInsertedId)
                    hiddenRcNo.Value = Convert.ToInt32(cmd.Parameters("@newId").Value)
                    'End If
                    con.Close()
                End Using
            End Using
        Else
            'Update existing Device Type

            Using con As New MySqlConnection(constr)
                Dim query As String = "UPDATE tbldevicetype SET DeviceType = @DeviceType , DeviceDescription = @DeviceDescription WHERE RcNo = @RcNo"
                Using cmd As New MySqlCommand(query)
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@DeviceType", txtDeviceType.Text)
                    cmd.Parameters.AddWithValue("@DeviceDescription", txtDeviceDescription.Text)
                    cmd.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        End If

        Dim filename As String = Path.GetFileName(avatarUpload.PostedFile.FileName)
        'If filename = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "alertpopup();", True)
        '    Return
        'End If

        If (HiddenisLogoClear.Value = 1) Or (filename <> "") Then

            If (HiddenisLogoClear.Value = 1) Then
                ' save null value to  icon field when clear image
                Dim bytes As Byte() = Nothing
                Using con As New MySqlConnection(constr)
                    Dim query As String = "UPDATE tbldevicetype SET Icon = @Icon WHERE RcNo = @RcNo"
                    Using cmd As New MySqlCommand(query)
                        cmd.Connection = con
                        cmd.Parameters.AddWithValue("@Icon", bytes)
                        cmd.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
            ElseIf (filename <> "") Then
                Using fs As Stream = avatarUpload.PostedFile.InputStream
                    Using br As New BinaryReader(fs)
                        Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                        Using con As New MySqlConnection(constr)
                            Dim query As String = "UPDATE tbldevicetype SET Icon = @Icon WHERE RcNo = @RcNo"
                            Using cmd As New MySqlCommand(query)
                                cmd.Connection = con
                                cmd.Parameters.AddWithValue("@Icon", bytes)
                                cmd.Parameters.AddWithValue("@RcNo", hiddenRcNo.Value)
                                con.Open()
                                cmd.ExecuteNonQuery()
                                con.Close()
                            End Using
                        End Using
                    End Using
                End Using
                ' clear image clicked without upload

            End If


        End If


        loadGrid()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "hidepopup();", True)

    End Sub


    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim RcNo As Integer = Convert.ToInt32((CType(sender, LinkButton)).CommandArgument)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Dim query As String = "DELETE FROM tbldevicetype WHERE RcNo = @RcNo"
            Using cmd As New MySqlCommand(query)
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@RcNo", RcNo)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowStatus", "deletepopup();", True)
        loadGrid()
    End Sub

    Protected Sub btnClearImage_Click(sender As Object, e As EventArgs)
        'isLogoClear = True
        'Image1.ImageUrl = "~/Images/NoUser.jpg"
    End Sub
End Class

#End Region

#Region "Helper class"
Public Class DeviceTypeModel
    Public Property RcNo() As Integer
    Public Property DeviceType() As String
    Public Property DeviceDescription() As String
    Public Property Icon() As Byte
    Public Property ImageURL() As String

End Class

#End Region



