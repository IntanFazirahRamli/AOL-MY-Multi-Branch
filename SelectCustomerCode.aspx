<%@ Page Language="VB" Title="Product" Culture="en-GB"%>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.Sqlclient" %>
<%@ Import Namespace="MessageBox" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Button1.OnClientClick = "window.close();"
    End Sub
   
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim str As New StringBuilder
        Dim str1 As String
        Dim str2 As String
        Dim str3 As String
        Dim str4 As String
        'Dim str5 As String
        'Dim str6 As String
        'str6 = ""
        str1 = GridView1.SelectedRow.Cells(0).Text
        str2 = GridView1.SelectedRow.Cells(1).Text
        str3 = GridView1.SelectedRow.Cells(2).Text
        str4 = GridView1.SelectedRow.Cells(3).Text
        ''Session.Add("strproductcodex", str1)
        
        'userid = Session("strNewLogin").ToString
         
       
        
        str.Append("<script language=JavaScript>")
        str.Append("window.opener.document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$txtCustomerCode.value ='" + str1 + "';")
        str.Append("window.opener.document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$txtCustomerCodex.value ='" + str1 + "';")
        ' str.Append("window.opener.document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$txtSampleSponsor.value ='" + str2 + "';")
        str.Append("window.opener.document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$txtCustomerName.value ='" + str2 + "';")
        'str.Append("window.opener.document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$txtTestSponsor.value ='" + str3 + "';")
        str.Append("window.opener.document.forms['aspnetForm'].ctl00$ContentPlaceHolder1$txtSponsorContactPerson.value ='" + str4 + "';")
        str.Append("window.close();")
        str.Append("<" & Chr(47) & "script>")
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "subscribescript", str.ToString())
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Select Customer Code</title> 
      
  
</head>

<body>

    <form id="form1" runat="server">
    <div>
        <table style="border-right: #5d7b9d thin double; border-top: #5d7b9d thin double; border-left: #5d7b9d thin double;
            border-bottom: #5d7b9d thin double; width: 539px;">
            <tr>
                <td style="width: 328px; background-color: white; text-align: center">
        <asp:Button ID="Button1" runat="server" Text="Close" Font-Bold="True" /></td>
            </tr>
            <tr>
                <td style="background-color: white; width: 328px;">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Calibri"
                        Font-Size="14pt" ForeColor="Black" Style="text-align: center" Text="Select Customer"
                        Width="512px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 328px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataSourceID="SqlDataSource1" Height="25px"
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" UseAccessibleHeader="False" Width="520px" Font-Names="Arial" Font-Size="9pt" CellPadding="4" ForeColor="#333333" GridLines="None">
            
            <Columns>
                <asp:DynamicField DataField="name" HeaderText="name" />
                <asp:CommandField ShowSelectButton="True" />
            </Columns><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" Font-Size="9pt" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="10pt" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
                </td>
            </tr>
        </table>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" SelectCommand="SELECT name  FROM tblcompanystaff  order by name" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
        </asp:SqlDataSource>
    </form>
</body>
</html>
