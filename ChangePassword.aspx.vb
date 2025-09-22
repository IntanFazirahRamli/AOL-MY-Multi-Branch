Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class ChangePassword
    Inherits System.Web.UI.Page

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim UserID As String = Convert.ToString(Session("UserID"))
        lblUserID.Text = UserID
    End Sub

    Protected Sub btnSavePswd_Click(sender As Object, e As EventArgs) Handles btnSavePswd.Click
        If String.IsNullOrEmpty(txtOldPswd.Text) Then
            lblAlertPswd.Text = "ENTER OLD PASSWORD"
            txtOldPswd.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtNewPswd.Text) Then
            lblAlertPswd.Text = "ENTER NEW PASSWORD"
            txtNewPswd.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtNewPswd2.Text) Then
            lblAlertPswd.Text = "ENTER CONFIRM PASSWORD"
            txtNewPswd2.Focus()
            Return
        End If
        If txtNewPswd.Text.Trim <> txtNewPswd2.Text.Trim Then
            lblAlertPswd.Text = "NEW PASSWORD AND CONFIRM PASSWORD DO NOT MATCH"
            Return
        Else


            Dim NameEncodein(txtPassword.Text.Length - 1) As Byte
            NameEncodein = System.Text.Encoding.UTF8.GetBytes(txtOldPswd.Text)
            Dim EcodedName As String = Convert.ToBase64String(NameEncodein)
            ' txtPassword.Text = EcodedName

            Try

                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                command.CommandText = "SELECT SecWebPassword, StaffID, Name, SecGroupAuthority FROM tblstaff where SecWebLoginID = @userid;"
                command.Parameters.AddWithValue("@userid", lblUserID.Text)
                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    If EcodedName = dt.Rows(0)("SecWebPassword").ToString() Then

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "UPDATE tblsTAFF SET SecWebPassword=@SecWebPassword where SecWebLoginID = @userid;"
                        '     command1.Parameters.Clear()

                        command1.Parameters.AddWithValue("@userid", lblUserID.Text)


                        '''''''''''''''''''''''''''''''''
                        'Password encryption
                        '''''''''''''''''''''''''''''''''''
                        Dim NameEncodein1 As Byte() = New Byte(txtNewPswd.Text.Length - 1) {}
                        NameEncodein1 = System.Text.Encoding.UTF8.GetBytes(txtNewPswd.Text)
                        Dim EcodedName1 As String = Convert.ToBase64String(NameEncodein1)

                        command1.Parameters.AddWithValue("@SecWebPassword", EcodedName1)

                        'lblMessagePswd.Text = lblUserID.Text + " " + EcodedName1 + " " + EcodedName

                        command1.Connection = conn

                        command1.ExecuteNonQuery()

                        mdlPopupConfirmPost.Show()

                        ' ScriptManager.RegisterStartupScript(Page, Page.GetType, "str", "ConfirmLogin();", False)

                        '    MessageBox.Message.Alert(Page, "Password Updated Successfully!\nPlease Login using your New Password!", "str")
                        '    Dim confirmValue As String = Request.Form("confirm_value")
                        '    Response.Redirect("Login.aspx")

                    Else
                        lblAlertPswd.Text = "INCORRECT OLD PASSWORD"
                    
                        Return
                    End If

                End If

                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.Message.ToString, "str")

            End Try


        End If
    End Sub

    Protected Sub btnConfirmYes_Click(sender As Object, e As EventArgs) Handles btnConfirmYes.Click
        mdlPopupConfirmPost.Hide()
        Response.Redirect("Login.aspx")

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("Home.aspx")

    End Sub
End Class
