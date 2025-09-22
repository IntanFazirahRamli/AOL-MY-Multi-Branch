<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" Culture="en-GB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link id="Link1" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon"/>
                <link id="Link2" rel="icon" href="~/Images/favicon.ico" type="image/ico"/>
 
     <script type = "text/javascript">
         function currentdatetimestartdate() {
             var currentTime = new Date();


             var dd = currentTime.getDate();
             var mm = currentTime.getMonth() + 1;
             var y = currentTime.getFullYear();

             var hh = currentTime.getHours();
             var MM = currentTime.getMinutes();
             var ss = currentTime.getSeconds();

             if (dd < 10) {
                 dd = "0" + dd;
             }
             if (mm < 10)
                 mm = "0" + mm;

             if (hh < 10)
                 hh = "0" + hh;
             if (MM < 10)
                 MM = "0" + MM;
             if (ss < 10)
                 ss = "0" + ss;
             var strct = y + "/" + mm + "/" + dd + " " + hh + ":" + MM + ":" + ss;
             var endct = new Date(strct);
             document.getElementById("<%=lblSysDate.ClientID%>").value = dd + "/" + mm + "/" + y;
             document.getElementById("<%=lblSysTime.ClientID%>").value = dd + "/" + mm + "/" + y + " " + hh + ":" + MM + ":" + ss;

         }

         function stopRKey(evt) {
             var evt = (evt) ? evt : ((event) ? event : null);
             var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
             if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
         }

         document.onkeypress = stopRKey;
              </script> 
    <style type="text/css">
        .button {
            margin-right:10px;
            border-radius:1px; 
            box-shadow: 2px 2px 1px #808080; 
            height:30px;
            width:100px;
        }
          .dummybutton {
          display: none;
      }

          .roundbutton1 {
            border: 2px solid #a1a1a1;   
    background: #dddddd;   
    border-radius: 25px;
    height:30px;
    width:100px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">

       <table width="100%" style="color: #E0E0E0">
           <tr style="height:10px;vertical-align: top; padding-top: 0px;padding-left:10px;">
               <td style="width:60%;padding-left:10px;" rowspan="2"><a id="link" runat="server" href="http://www.anticimex.com"><img src="Images/Anticimex1.png" width="295" height="45" border="0"/>
</a> <br />
       <div style="padding-left:120px;"> <asp:Label ID="lblDomainName" runat="server" Font-Names="Calibri" Font-Size="18px" Font-Bold="True" ForeColor="#000099"></asp:Label>
            </div>
                 </td><td style="width:40%"> 
    
                   
                                           </td>
           </tr>
         
             </table>
        <asp:Table ID="Table3" runat="server" Width="100%" Height="500px" Font-Names="Calibri">
            <asp:TableRow Height="10px"> </asp:TableRow>
            <asp:TableRow>
                <%--<asp:TableCell Width="250px" Height="300px" VerticalAlign="Top"></asp:TableCell>--%>
                         <asp:TableCell  HorizontalAlign="Center">
                              <%--<div style="text-align:center;">--%>
                               <div style="margin-right:15px; border-radius:2px; box-shadow: 4px 4px 2px #808080; background-color: #E8E8E8; width: 500px; height: 250px;">
          <asp:Panel ID="Panel1" runat="server" Width="500px">
                
                                             <asp:Table ID="Table1" runat="server">
                                                 <asp:TableRow>
                                                     <asp:TableCell ColumnSpan="3" HorizontalAlign="Center"><h2 style="color: #000099;padding-right:5px;">&nbsp;&nbsp;Login to AX Pest Online</h2></asp:TableCell></asp:TableRow>
                                                 <asp:TableRow>
                                                     <asp:TableCell></asp:TableCell>
                                                 </asp:TableRow>
                                                 <asp:TableRow>
                                                     <asp:TableCell Width="30px"></asp:TableCell>
                                                     <asp:TableCell Font-Size="14">User ID</asp:TableCell>
                                                     <asp:TableCell><asp:TextBox ID="txtUserID" runat="server" BackColor="White" Width="150px" MaxLength="30"></asp:TextBox></asp:TableCell>
                                                 </asp:TableRow>
                                                 <asp:TableRow>
                                                     <asp:TableCell></asp:TableCell>
                                                     <asp:TableCell Font-Size="14">Password</asp:TableCell>
                                                     <asp:TableCell><asp:TextBox ID="txtPassword" runat="server" BackColor="White" TextMode="Password"  Width="150px" MaxLength="24"></asp:TextBox></asp:TableCell>
                                                 </asp:TableRow>
                                                 <asp:TableRow Height="10px">
                                                     <asp:TableCell></asp:TableCell>
                                                     <asp:TableCell></asp:TableCell>
                                                     <asp:TableCell></asp:TableCell>
                                                 </asp:TableRow>
                                                  <asp:TableRow Height="10px">
                                                     <asp:TableCell><br /></asp:TableCell>
                                                     <asp:TableCell></asp:TableCell>
                                                     <asp:TableCell></asp:TableCell>
                                                 </asp:TableRow>
                                                 <asp:TableRow>
                                                     <asp:TableCell></asp:TableCell>
                                                     <asp:TableCell HorizontalAlign="Center"><asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetimestartdate(); if(this.value === 'Logging...') { return false; } else { this.value = 'Logging...'; }" /></asp:TableCell>
                                                     <asp:TableCell HorizontalAlign="Center"><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="roundbutton1" Font-Bold="True" /></asp:TableCell>
                                                 </asp:TableRow>
                                             </asp:Table>
                                         </asp:Panel> 
           </div> <%--</div>--%>  
             </asp:TableCell>
                <%--<asp:TableCell></asp:TableCell>--%>
             </asp:TableRow>
          
             <asp:TableRow>
                <asp:TableCell Height="50px" HorizontalAlign="Center"><br /><asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Calibri" Font-Bold="True" Font-Size="18px" ForeColor="Brown"></asp:Label></asp:TableCell>
            </asp:TableRow>
              <asp:TableRow>
                <asp:TableCell Height="300px"></asp:TableCell>
            </asp:TableRow>
           
        </asp:Table>
   <%-- <div style="text-align:right; font-family: Calibri; font-size: small; vertical-align: bottom;height:70px;background-color:beige;padding-top:60px;">
               Copyright <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> @ CWBINFOTECH PTE LTD. All Right Reserved.<asp:Image ID="Image1" runat="server" ImageAlign="Right" ImageUrl="~/Images/cwb.png" />
           </div>
         --%>
        <asp:TextBox ID="lblSysDate" runat="server" Font-Names="Calibri" BorderStyle="None"></asp:TextBox>
         <asp:TextBox ID="lblSysTime" runat="server" Font-Names="Calibri" cssclass="dummybutton"></asp:TextBox>
      
        <asp:TextBox ID="txtOTP" runat="server" cssclass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtOTPPrefix" runat="server" cssclass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtOTPNumber" runat="server" cssclass="dummybutton"></asp:TextBox>
    </form>
</body>
</html>
