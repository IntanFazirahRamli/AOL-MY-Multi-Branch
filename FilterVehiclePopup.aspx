<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FilterVehiclePopup.aspx.vb" Inherits="FilterVehiclePopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Filter Vehicle</title>
    <base target="_self"/>
     <script lang="javascript">


         function GetValue() {


             window.location.reload();


         }

         function GetValueOld() {

             document.getElementById('Button1').click();

             window.location.reload();

             var String = document.getElementById('txtQuery').value;
             //  window.returnValue = String;
             // to handle in IE 7.0  
             if (window.showModalDialog) {
                 window.returnValue = String;

                 window.parent.location.reload();
                 window.close();

             }
                 // to handle in Firefox
             else {
                 if ((window.opener != null) && (!window.opener.closed)) {
                     RetrieveControl();
                     // Access the control.       
                     window.opener.document.getElementById(ctr[1]).value = String;
                 }
                 window.close();
             }

         }

         function RetrieveControl() {
             //Retrieve the query string
             queryStr = window.location.search.substring(1);
             // Seperate the control and its value
             ctrlArray = queryStr.split("&");
             ctrl1 = ctrlArray[0];
             //Retrieve the control passed via querystring
             ctr = ctrl1.split("=");
         }
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <br /><br />
        <table style="width:100%;">
            <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Asset No
                             </td>
                           <td>    <asp:TextBox ID="txtAssetNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
            <tr><td style="width:10%"></td>
                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Asset RegNo
                               </td>
                           <td>    <asp:TextBox ID="txtAssetRegNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
            <tr><td style="width:10%"></td>
                  <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">InCharge ID</td>
                           <td>           
                               <asp:DropDownList ID="ddlInchargeID" runat="server" DataSourceID="SqlDataSource2" DataTextField="StaffId" DataValueField="StaffId" Width="155px" AutoPostBack="false">
                               </asp:DropDownList>
                            </td>
            </tr>
            <tr><td style="width:10%"></td>
                 <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Status</td>
                           <td>      
                               <asp:DropDownList ID="txtStatus" runat="server" Width="155px">
                                   <asp:ListItem Selected="True" Value="O">O - Operational</asp:ListItem>
                                   <asp:ListItem Value="H">H - Hire</asp:ListItem>
                                   <asp:ListItem Value="S">S - Sold</asp:ListItem>
                                   <asp:ListItem Value="X">X - Scrap</asp:ListItem>
                                   <asp:ListItem Value="L">L - Loan</asp:ListItem>
                                   <asp:ListItem Value="F">F - Faulty</asp:ListItem>
                                   <asp:ListItem Value="M">M - Missing</asp:ListItem>
                                   <asp:ListItem Value="N">N - New</asp:ListItem>
                               </asp:DropDownList>
                               </td>
            </tr>
            <tr><td style="width:10%"></td><td><br /></td><td></td></tr>
            <tr><td style="width:10%"></td><td><asp:Button ID="btnSearch" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Search" Width="100px" OnClientClick="return true;"/></td>
                <td><asp:Button ID="btnCancel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
            </tr>
        </table>
        <asp:TextBox ID="txtQuery" runat="server" Visible="false"></asp:TextBox> 
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblasset where rcno<>0"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId FROM tblstaff ORDER BY STAFFID"></asp:SqlDataSource>
    </form>
</body>
</html>
