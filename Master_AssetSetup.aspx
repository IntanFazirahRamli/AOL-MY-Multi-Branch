<%@ Page Title="Asset Module Setup" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_AssetSetup.aspx.vb" Inherits="Master_AssetSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">


         function currentdatetime() {
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
             document.getElementById("<%=txtCreatedOn.ClientID%>").value = dd + "/" + mm + "/" + y + " " + hh + ":" + MM + ":" + ss;
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
        <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
       
   </ControlBundles>
    </asp:ToolkitScriptManager>
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Asset Module Setup</h3>
    
       <table style="width:100%;text-align:center;">
            <%--<tr>
                <td colspan="2"><br /></td>
            </tr>--%>
            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
                 <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                       <asp:Label ID="txtMode" runat="server" Text="" CssClass="dummybutton"></asp:Label>
</td>
                     
            </tr>
            
            <tr>
              
                <td style="width:50%;text-align:left;">
                  
                     <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" Visible="False" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Visible="False" />
                
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
            
                       </td>
                <td style="text-align: right">
            
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />

                </td>
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
        
            <tr>
                
                  <td colspan="2">

                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">Asset Module Setup</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">

                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Email Notification Recipient</td>
                                     <td class="CellTextBoxADM">
                                            <asp:TextBox ID="txtEmailNotification" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                         
                                     </td>
                                   </tr>
                          
                                
                              </table></td></tr>
                      </table><br />

                       
  
                  </td>
                 </tr>
             
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblservicerecordmastersetup where rcno=1"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
    </div>
 

</asp:Content>

