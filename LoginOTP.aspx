<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginOTP.aspx.vb" Inherits="LoginOTP" Culture="en-GB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <link id="Link1" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon"/>
                <link id="Link2" rel="icon" href="~/Images/favicon.ico" type="image/ico"/>
   
  <%--  <script>
        // Set the date we're counting down to
        var countDownDate = new Date().getTime();

        // Update the count down every 1 second
        var x = setInterval(function () {

            // Get todays date and time
            var now = new Date().getTime();

            // Find the distance between now an the count down date
            var distance = countDownDate - document.getElementById("lblSysTime").innerHTML;

            // Time calculations for days, hours, minutes and seconds
            // var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            //  var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            // Output the result in an element with id="demo"
            document.getElementById("demo").innerHTML = minutes + "m " + seconds + "s ";

            // If the count down is over, write some text 
            if (distance < 0) {
                clearInterval(x);
                document.getElementById("demo").innerHTML = "EXPIRED";
            }
        }, 1000);
</script>
  --%>
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


             var strct = y + "/" + mm + "/" + dd + " " + hh + ":" + MM + ":" + ss;
             var endct = new Date(strct);
             document.getElementById("<%=lblSysDate.ClientID%>").value = dd + "/" + mm + "/" + y;

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
       <p id="demo"></p>

<%--<button onclick="clearInterval(myVar)">Stop time</button>--%>

<%--<script>
    var myVar = setInterval(myTimer, 1000);
    function myTimer() {
        var currentTime = new Date();

         var MM = currentTime.getMinutes();
        var ss = currentTime.getSeconds();

        var expirytime = new Date();
        expirytime = document.getElementById("txtOTPExpiry").innerHTML;

        var MM1 = expirytime
      //  document.getElementById("demo").innerHTML = d.toLocaleTimeString();
        // Get todays date and time
        var now = new Date().getTime();
        //alert(now);
        //alert(document.getElementById("txtOTPExpiry").innerHTML);

        // Find the distance between now an the count down date
        var distance = now - document.getElementById("txtOTPExpiry").innerHTML;
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        // Output the result in an element with id="demo"
        document.getElementById("demo").innerHTML = minutes + "m " + seconds + "s ";
    }
</script>--%>
     <asp:TextBox ID="lblSysTime" runat="server" Font-Names="Calibri" Visible="false"></asp:TextBox>
         <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblTime" runat="server" ForeColor="Red" visible="false"/>
        <asp:Timer ID="Timer1" runat="server" OnTick="GetTime" Interval="1000" />
    </ContentTemplate>
            </asp:UpdatePanel>

      <%--  <asp:Timer runat="server" id="UpdateTimer" interval="1000" ontick="UpdateTimer_Tick" />
        <asp:UpdatePanel runat="server" id="TimedPanel" updatemode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:Label runat="server" id="DateStampLabel" />
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    
       <table width="100%" style="color: #E0E0E0">
           <tr style="height:10px;vertical-align: top; padding-top: 0px;padding-left:10px;">
               <td style="width:60%;padding-left:10px;" rowspan="2"><a href="http://www.anticimex.com"><img src="Images/Anticimex1.png" width="295" height="45" border="0"/>
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
          <asp:Panel ID="Panel1" runat="server" Width="500px"><asp:Table ID="Table1" runat="server">
                                                 <asp:TableRow>
                                                     <asp:TableCell ColumnSpan="3" HorizontalAlign="Center"><h2 style="color: #000099;padding-right:5px;">&nbsp;&nbsp;OTP for Login to AX Pest Online</h2></asp:TableCell></asp:TableRow>
                                               
                                                   <asp:TableRow>
                                                     <asp:TableCell ColumnSpan="3" Font-Size="14" HorizontalAlign="Center">Enter the 6 digit OTP sent to your Email</asp:TableCell>
                                                         </asp:TableRow>
                <asp:TableRow>
                                                     <asp:TableCell ColumnSpan="3"><br /></asp:TableCell>
                                                 </asp:TableRow>
                                                 <asp:TableRow>
                                                     
                                                     <asp:TableCell  ColumnSpan="3" HorizontalAlign="Center">
                                                         <asp:Label ID="lblOTPPrefix" runat="server" Text=""></asp:Label><asp:TextBox ID="txtOTPNumberInput" runat="server" BackColor="White" Width="150px" MaxLength="30"></asp:TextBox>
                                                         &nbsp;<asp:Button ID="btnSMSOTP" runat="server" Text="Get OTP via Whatsapp" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return currentdatetimestartdate()" Width="160px" /></asp:TableCell>
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
                                                     <asp:TableCell HorizontalAlign="Center"><asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return currentdatetimestartdate()" /></asp:TableCell>
                                                     <asp:TableCell HorizontalAlign="Center"><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="roundbutton1" Font-Bold="True" /></asp:TableCell>
                                                 </asp:TableRow>
                                             </asp:Table>
                                         </asp:Panel> 
           </div> <%--</div>--%>  
             </asp:TableCell>
                <%--<asp:TableCell></asp:TableCell>--%>
             </asp:TableRow>
          
             <asp:TableRow>
                <asp:TableCell Height="50px" HorizontalAlign="Center">
    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Calibri" Font-Bold="True" Font-Size="18px" ForeColor="Brown"></asp:Label></asp:TableCell>
            </asp:TableRow>
              <asp:TableRow>
                <asp:TableCell Height="300px">    <%--<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>--%>
                   <%-- <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="1000"></asp:Timer>--%> </asp:TableCell>
            </asp:TableRow>
           
        </asp:Table>
  
   <%-- <div style="text-align:right; font-family: Calibri; font-size: small; vertical-align: bottom;height:70px;background-color:beige;padding-top:60px;">
               Copyright <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> @ CWBINFOTECH PTE LTD. All Right Reserved.<asp:Image ID="Image1" runat="server" ImageAlign="Right" ImageUrl="~/Images/cwb.png" />
           </div>
         --%>
        <asp:TextBox ID="lblSysDate" runat="server" Font-Names="Calibri" BorderStyle="None"></asp:TextBox>
        <asp:TextBox ID="txtOTP" runat="server" Visible="false"></asp:TextBox>
      <asp:TextBox ID="txtOTPNumber" runat="server" Visible="false"></asp:TextBox>
     <asp:TextBox ID="txtOTPExpiry" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtToMobile" runat="server" Visible="false"></asp:TextBox>
    <%-- <asp:Timer ID="Timer2" runat="server" ontick="Timer2_Tick" 
            Interval="600000">
        </asp:Timer>--%>
        <br /><br />
       
            <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
    </form>
</body>
</html>
