Imports Microsoft.VisualBasic
Imports System
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class CustomerList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
      

        Dim list As New List(Of Customer)

        Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand()
                cmd.CommandType = CommandType.Text


                Dim insQuery As String = "SELECT AccountID,LocationID,ServiceName,Address1 from tblcompanylocation where smartcustomer = true"

                cmd.CommandText = insQuery
                cmd.Connection = con
                con.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                Do While reader.Read()

                    Dim item As New Customer()
                    item.AccountID = reader("AccountID").ToString()
                    item.LocationID = reader("LocationID").ToString()
                    item.ServiceName = reader("ServiceName").ToString()
                    item.Address1 = reader("Address1").ToString()

                    list.Add(item)
                Loop
                con.Close()
            End Using
            GridCustomer.DataSource = list
            GridCustomer.DataBind()
        End Using
      
    End Sub
    Protected Sub btnGetCustomerlist_Click(sender As Object, e As EventArgs) Handles btnGetCustomer.Click
        Dim sCustomername As String
        Dim dt As New DataTable
        Dim dr As DataRow
        sCustomername = txtCustomer.Text
        If Not Session("dtcustomer") Is Nothing Then
            dt = Session("dtcustomer")
        End If
        If Not sCustomername = "" Then

            Dim list As New List(Of Customer)

            Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand()
                    cmd.CommandType = CommandType.Text


                    Dim insQuery As String = "SELECT AccountID,LocationID,ServiceName,Address1 from tblcompanylocation where (ServiceName like '%" & sCustomername & "%' or Address1 like '%" & sCustomername & "%' or LocationID like '%" & sCustomername & "%') AND smartcustomer = true"

                    cmd.CommandText = insQuery
                    cmd.Connection = con
                    con.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()

                    Do While reader.Read()

                        Dim item As New Customer()
                        item.AccountID = reader("AccountID").ToString()
                        item.LocationID = reader("LocationID").ToString()
                        item.ServiceName = reader("ServiceName").ToString()
                        item.Address1 = reader("Address1").ToString()

                        list.Add(item)
                    Loop
                    con.Close()
                End Using
                GridCustomer.DataSource = list
                GridCustomer.DataBind()
            End Using



            ''dr = 'dt.Select("CustomerName='Customer1')
            'GridCustomer.DataSource = dt
            'GridCustomer.DataBind()
        End If
    End Sub
End Class
Public Class Customer
    Public Property AccountID() As String
    Public Property LocationID() As String
    Public Property ServiceName() As String
    Public Property Address1() As String

End Class
