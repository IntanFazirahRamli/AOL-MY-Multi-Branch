<%@ Page Title="Log Master Setup" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_LogDetailsSetup.aspx.vb" Inherits="Master_LogDetailsSetup" Culture="en-GB" %>
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
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Log Details Setup</h3>
    
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

                      <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                           <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">&nbsp;WEB - TABLET SETTING</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"> DOWNLOAD SETTING</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkWebToZSoft" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Tablet Records - AutoSet Submit Status to 'S'</asp:ListItem>
                                             <asp:ListItem Value="2">Tablet Records - AutoSet File Status to 'P'</asp:ListItem>
                                             <asp:ListItem Value="3">Tablet Records - AutoSet Detail Status to 'C'</asp:ListItem>
                                             <asp:ListItem Value="4">Tablet Records - Auto Update Status to 'P'</asp:ListItem>
                                             <asp:ListItem Value="5">Tablet Records - Auto Generate PDF Copy of Service Record</asp:ListItem>
                                         </asp:CheckBoxList></td></tr>

                              </table></td></tr>

                            <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px; display:none">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"> UPLOAD SETTING</td>
                                     <td class="CellTextBoxADM">
                                         <asp:TextBox ID="txtUploadDays" runat="server" Width="50px"></asp:TextBox> Days Scheduled Service To Upload</td></tr>

                              </table></td></tr>
                      </table><br />

                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;display:none">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">QUICK SERVICE SETTING</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkQuickServiceSetting" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">New Quick Service Auto File when Save</asp:ListItem>
                                             <asp:ListItem Value="2">New Quick Service Auto Generate PDF Copy of Service Record</asp:ListItem>
                                             <asp:ListItem Value="3">New Quick Service Auto Submit when Save</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table>

                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">SCHEDULE AUTO ASSIGN</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Auto Assign By</td>
                                <td class="CellTextBoxADM" colspan="1">
                                     <asp:RadioButtonList ID="rdbScheduleAutoAssign" runat="server" RepeatDirection="Horizontal" Width="100%">
                                           <asp:ListItem>Vehicle</asp:ListItem>
                                              <asp:ListItem>Team</asp:ListItem>
                                            <asp:ListItem>Incharge</asp:ListItem>
                                     </asp:RadioButtonList>
                                      </td></tr>
                                         <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Allocation Method</td>
                               
                                        <td class="CellTextBoxADM" colspan="1">
                                     <asp:RadioButtonList ID="rdbAllocationMethod" runat="server" RepeatDirection="Horizontal" Width="70%">
                                           <asp:ListItem>TimeOut</asp:ListItem>
                                              <asp:ListItem>AllocateTime</asp:ListItem>
                                    
                                          </asp:RadioButtonList>
                                      </td></tr>

                                      <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Auto Assign Start</td>
                               
                                        <td class="CellTextBoxADM" colspan="1">
                                            <asp:TextBox ID="txtAssignStart" runat="server" Width="80px"></asp:TextBox> 
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b class="CellFormatADM">End</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     <asp:TextBox ID="txtAssignEnd" runat="server" Width="80px"></asp:TextBox>

                                      </td></tr>

                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Auto Assign Interval</td>
                               
                                        <td class="CellTextBoxADM" colspan="1">
                                            <asp:TextBox ID="txtAssignInterval" runat="server" width="50px"></asp:TextBox>

                                   &nbsp;&nbsp;mins
                                      </td></tr>
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Auto Assign Duration</td>
                               
                                        <td class="CellTextBoxADM" colspan="1">
                                            <asp:TextBox ID="txtAssignDuration" runat="server" width="50px"></asp:TextBox>

                                   &nbsp;&nbsp;mins
                                      </td></tr>
                              </table></td></tr>
                      </table>

                      
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">AUTO UPDATE CLIENT DETAILS</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkAutoUpdateClient" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Auto Update Client Details in Service Record</asp:ListItem>
                                             <asp:ListItem Value="2">Auto Update Client Details in Service Contract</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table>

                           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;display:none">
                           <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">JOB ORDER</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Default Printer</td>
                                     <td class="CellTextBoxADM">
                                         <asp:TextBox ID="txtDefaultPrinter" runat="server" Width="150px"></asp:TextBox></td></tr>

                              </table></td></tr>
                      </table>

                      
                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">GENERAL SETTINGS</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Approval Screen View Past Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtApprovalScreenView" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Job Order Record Period</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtJobOrderRecord" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Service Record Record Period</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSvcRecRecord" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Service Contract Record Period</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSvcContractRecord" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                    <tr>
                                <td class="CellTextBoxADM" colspan="2">
                                    <asp:CheckBoxList ID="chkDisableDelete" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Disable Delete Service Record</asp:ListItem>
                                          <asp:ListItem Value="2">Disable Delete Service Contract</asp:ListItem>
                                        
                                    </asp:CheckBoxList>
                                      </td></tr>

                              </table></td></tr>
                      </table><br />


                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">ENABLE LOG</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkLogDetail" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Enable Log for Customer</asp:ListItem>
                                             <asp:ListItem Value="2">Enable Log for Contract</asp:ListItem>
                                              <asp:ListItem Value="3">Enable Log for Service</asp:ListItem>
                                              <asp:ListItem Value="4">Enable Log for Invoice</asp:ListItem>
                                              <asp:ListItem Value="5">Enable Log for Receipt</asp:ListItem>
                                              <asp:ListItem Value="6">Enable Log for CN/DN</asp:ListItem>
                                              <asp:ListItem Value="7">Enable Log for Journal</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table><br />
                                         
                       
      
                      
                      
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px; ">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">CALENDAR</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBox ID="chkCalendar" runat="server" Text="Job Order Assignment / Completion" Visible="False" /></td></tr>

                              </table></td></tr>
                      </table><br />  
                  </td>
                 </tr>
             
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblcity where rcno<>0"></asp:SqlDataSource>

       
        
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Country, Citizenship, Comments, Rcno, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, CountryCode, WS FROM tblcountry WHERE (Rcno &lt;&gt; 0)">
                       
                    </asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtCountryCode" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>
 

</asp:Content>



