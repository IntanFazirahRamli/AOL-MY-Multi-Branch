<%@ page title="Master Setup" language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" inherits="Master_ServiceRecordSetup" CodeFile="Master_ServiceRecordSetup.aspx.vb" culture="en-GB" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
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
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Contract and Service Module Setup</h3>
    
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

                      
                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
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
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">MAXIMUM GRID RECORDS</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left; display:none" >Job Order Maximum Displayed Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtJobOrderMaxRecs" runat="server" width="150px" Visible="False"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Service Record Maximum Displayed Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSvcRecMaxRecs" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Service Contract Maximum Displayed Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSvcContractMaxRecs" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                 </table></td></tr>
                      </table><br />

                      
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">GRID RECORDS ON SCREEN LOAD</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkShowGridRecords" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Show Service Records on Screen Load</asp:ListItem>
                                             <asp:ListItem Value="2">Show Service Contracts on Screen Load</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table><br />
  
                      
                      
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">SERVICE CONTRACT</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkServiceContract" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Schedule Service After Update Contract</asp:ListItem>
                                             <asp:ListItem Value="2">Confirmation Needed for Amend Service Address</asp:ListItem>
                                             <asp:ListItem Value="3">Post Your Ref/Our Ref to Service Record</asp:ListItem>
                                             <asp:ListItem Value="4">Team ID Mandatory</asp:ListItem>
                                             <asp:ListItem Value="5">Service By Mandatory</asp:ListItem>
                                             <asp:ListItem Value="6">Generate Schedule as a Backrground Process</asp:ListItem>
                                             <asp:ListItem Value="7">Allow Termination before the Last Service Date</asp:ListItem>
                                        
                                           </asp:CheckBoxList></td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Contract Revision New Contract Code  <asp:DropDownList ID="ddlContractRevisionNewContractCode" runat="server" Width="80%" AppendDataBoundItems="true" DataSourceID="SqlDSContractCode" DataTextField="CC" DataValueField="CC">
                                 <asp:ListItem>--SELECT--</asp:ListItem>
                                           </asp:DropDownList> 
                                     </td>
                                   </tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Contract Revision Termination Code  <asp:DropDownList ID="ddlTerminationCode" runat="server" Width="80%" AppendDataBoundItems="true" DataSourceID="SqlDSTerminationCode" DataTextField="TC" DataValueField="TC">
                                 <asp:ListItem>--SELECT--</asp:ListItem>
                                           </asp:DropDownList> 
                                     </td>
                                   </tr>

                              

                                      <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Contract Renewal New Contract Code  <asp:DropDownList ID="ddlContractRenewalNewContractCode" runat="server" Width="80%" AppendDataBoundItems="true" DataSourceID="SqlDSContractCode" DataTextField="CC" DataValueField="CC">
                                 <asp:ListItem>--SELECT--</asp:ListItem>
                                           </asp:DropDownList> 
                                     </td>
                                   </tr>

                                      <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Contract Renewal Termination Code  <asp:DropDownList ID="ddlContractRenewalTerminationCode" runat="server" Width="80%" AppendDataBoundItems="true" DataSourceID="SqlDSTerminationCode" DataTextField="TC" DataValueField="TC">
                                 <asp:ListItem>--SELECT--</asp:ListItem>
                                           </asp:DropDownList> 
                                     </td>
                                   </tr>
                                  
                                        <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Contract Void Code  <asp:DropDownList ID="ddlContractVoidCode" runat="server" Width="80%" AppendDataBoundItems="true" DataSourceID="SqlDSTerminationCode" DataTextField="TC" DataValueField="TC">
                                 <asp:ListItem>--SELECT--</asp:ListItem>
                                           </asp:DropDownList> 
                                     </td>
                                   </tr>
                                  
                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                                  <asp:CheckBox ID="chkAutoRenewal" runat="server" Text="Auto Renewal" />
                                         <asp:CheckBox ID="chkContinuousContract" runat="server" Text="Continuous Contract" />

                                     </td>
                                   </tr>


                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Doc No. Prefix <asp:TextBox ID="txtPrefixDocNoContract" runat="server" Width="60" MaxLength="2"></asp:TextBox>
                                     &nbsp;</td>
                                   </tr>


                                    <tr style="display:none"><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Price Increase Limit <asp:TextBox ID="txtPriceIncreaseLimit" runat="server" Width="60" MaxLength="5" AutoPostBack="True"></asp:TextBox>
                                     &nbsp;%</td>
                                   </tr>


                                    <tr style="display:none"><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Price Decrease Limit <asp:TextBox ID="txtPriceDecreaseLimit" runat="server" Width="60" MaxLength="6" AutoPostBack="True"></asp:TextBox>
                                     &nbsp;%</td>
                                   </tr>


                                  <tr style=""><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                      No. of Days Before Renewal Message <asp:TextBox ID="txtDaysBeforeRenewalMessage" runat="server" Width="60" MaxLength="5" AutoPostBack="True"></asp:TextBox>
                                         &nbsp;</td>
                                   </tr>


                                    <tr style="display:none"><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                      No. of Days Before Schedule Generation <asp:TextBox ID="txtDaysBeforeScheduleGeneration" runat="server" Width="60" MaxLength="6" AutoPostBack="True"></asp:TextBox>
                                         &nbsp;</td>
                                   </tr>


                                   <tr ><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                        <asp:CheckBox ID="chkLoadContractAddButton" runat="server" Text="Enable ADD button on Direct Loading of Contract Page " />
                                     </td>
                                   </tr>


                                   <tr style="display:none"><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                        <asp:CheckBox ID="chkDisplayFixedDurationType" runat="server" Text="Display Fixed DurationType " />
                                     </td>
                                   </tr>


                                   <tr style="display:none"><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                        <asp:CheckBox ID="chkDisplayContinuousDurationType" runat="server" Text="Display Continuous DurationType " />
                                     </td>
                                   </tr>
                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
          
                                          <asp:Panel ID="pnlRenewalNotification" runat="server" BorderColor="#993300" BorderStyle="Solid" BorderWidth="1px" Font-Size="15px" Height="19%" HorizontalAlign="left" Width="80%">
                       

                          <table style="width:100%;text-align:left;padding-left:2px;padding-top:4px;">
                            

                                     <tr>
                                        <td class="CellTextBoxADM" colspan="2">
                                            <asp:CheckBox ID="chkSendEmailNotificationForRenewal" runat="server" Text="Send Email Notification For Renewal" />
                                        </td>
                                      </tr>

                                      <tr>
                                        <td class="CellTextBoxADM">
                                            Contract Group 
                                        </td>

                                          <td class="CellTextBoxADM">
                                             <cc1:DropDownCheckBoxes ID="ddlContractGroupForEmailNotificationOfContractRenewal" runat="server" AddJQueryReference="False" DataSourceID="SqlDSContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup"  UseButtons="True" UseSelectAllNode="false" Width="200px"><style2 dropdownboxboxheight="200" dropdownboxboxwidth="200px" selectboxwidth="200px" /></cc1:DropDownCheckBoxes>
                                        </td>
                                      </tr>

                                      <tr>
                                        <td class="CellTextBoxADM">
                                            Days Before Expiry 
                                        </td>

                                        <td class="CellTextBoxADM">
                                            <asp:TextBox ID="txtDaysBeforeExpiry" runat="server" Width="60" MaxLength="2"></asp:TextBox>
                                        </td>
                                      </tr>
                              
                                       <tr>
                                     <td class="CellTextBoxADM" colspan="2">
                                      
                                               <asp:TextBox ID="txtEmailAddressForEmailNotificationOfContractRenewal" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                               </td>
                                   </tr>

                                  </table>
                   </asp:Panel>
                                          </td>
                                   </tr>
                            

                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
          
                                          <asp:Panel ID="pnlExpiredNotification" runat="server" BorderColor="#993300" BorderStyle="Solid" BorderWidth="1px" Font-Size="15px" Height="19%" HorizontalAlign="left" Width="80%">
                       

                          <table style="width:100%;text-align:left;padding-left:2px;padding-top:4px;">
                            
                               <tr>
                                        <td class="CellTextBoxADM">
                                            <asp:CheckBox ID="chkSendEmailNotificationForExpiredContracts" runat="server" Text="Send Email Notification For Expired Contracts" />
                                        </td>
                                      </tr>
                                   
                              
                                       <tr>
                                     <td class="CellTextBoxADM">
                                      
                                               <asp:TextBox ID="txtEmailAddressForEmailNotificationForExpiredContracts" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                               </td>
                                   </tr>

                               <tr>
                                        <td class="CellTextBoxADM">
                                            <asp:CheckBox ID="chkEmailNotificationforContractNoSchedule" runat="server" Text="Send Email Notification For Contracts with Issues" />
                                        </td>
                                      </tr>
                                   
                              
                                       <tr>
                                     <td class="CellTextBoxADM">
                                      
                                               <asp:TextBox ID="txtEmailAddressforContractsNoSchedule" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                               </td>
                                   </tr>

                                 <tr>
                                        <td class="CellTextBoxADM">
                                              <asp:CheckBox ID="chkEmailNotificationForContractStatusSalesman" runat="server" Text="Send Email Notification to Individual Salesman" />
                                    
                                             <br />    <asp:CheckBox ID="chkEmailNotificationForContractStatusChange" runat="server" Text="Send Email Notification Summary For Contracts Status Change" />
                                             
                                        </td>
                                      </tr>
                                   
                              
                                       <tr>
                                     <td class="CellTextBoxADM">
                                      
                                               <asp:TextBox ID="txtEmailAddressForContractStatusChange" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                               </td>
                                   </tr>

                              </table>
                   </asp:Panel>
                                </td>
                                   </tr>


                              </table></td></tr>
                      </table><br />
  
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">SERVICE RECORD SETTINGS</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Sort Order</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:radiobuttonlist ID="rdbSvcRecOrder" runat="server" RepeatColumns="2" RepeatDirection="Vertical" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                             <asp:ListItem Value="dec">Descending</asp:ListItem>
                                           </asp:radiobuttonlist>
                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Service Record</td>
                                <td class="CellTextBoxADM" colspan="1">
                                      <asp:CheckBoxList ID="chkSvcRecord" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">E-Approval Auto Update Service Record</asp:ListItem>
                                             <asp:ListItem Value="2">Not Allow Save if Detail Status is O</asp:ListItem>
                                               <asp:ListItem Value="3">Calculate Total Duration by Time In/Time Out</asp:ListItem>
                                           <asp:ListItem Value="5">Display Time In and Time Out in the Service Report </asp:ListItem>
                                          <asp:ListItem Value="7">Allow to Edit Billed Amt. and Bill No. </asp:ListItem>
                                           <asp:ListItem Value="6">Enable Email Address Validation </asp:ListItem>
                                               <asp:ListItem Value="4">Skip Retry Auto Sending Email after</asp:ListItem>
                                   
                                        </asp:CheckBoxList>
                                          <asp:TextBox ID="txtAttempts" runat="server" Width="60"></asp:TextBox> attempts 
                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Doc No. Prefix <asp:TextBox ID="txtPrefixDocNoService" runat="server" Width="60" MaxLength="2"></asp:TextBox>
                                     </td>
                                   </tr>
                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Schedule Change Notifications</td>
                                     <td class="CellTextBoxADM">
                                            <asp:TextBox ID="txtEmailNotiifcationsScheduleChange" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                         
                                     </td>
                                   </tr>
                             </table></td></tr>
                      </table><br />


                    
                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">CUSTOMER REQUEST</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:4px;">
                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left"></td>
                                     <td class="CellTextBoxADM">
                                       Doc No. Prefix <asp:TextBox ID="txtPrefixDocNoCustomerRequest" runat="server" Width="60" MaxLength="2"></asp:TextBox>
                                     </td>
                                   </tr>
                          

                           

                                

                             </table></td></tr>
                      </table><br />
                     
                          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">MOBILE APP</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">

                           <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Schedule Jobs Upper Limit</td>
                                <td class="CellTextBoxADM">
                                     <asp:TextBox ID="txtSchDaysLimit" runat="server" Width="30px"></asp:TextBox> Days
                                                  
                                      </td>
                                <td class="CellTextBoxADM">
                                      </td>
                                <td class="CellTextBoxADM">
                                       </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Supervisor Records</td>
                                <td class="CellTextBoxADM">
                                     <asp:TextBox ID="txtSupervisorRec" runat="server" Width="30px"></asp:TextBox> Days&nbsp;&nbsp;
                                                  
                                     &nbsp;&nbsp;
                                                  
                                      </td>
                                <td class="CellTextBoxADM">
                                                  <asp:CheckBox ID="chkEnableSaveUploadLater" runat="server" Text="Enable Save &amp; Upload Later" />

                                      </td>
                                <td class="CellTextBoxADM">
                                     <asp:CheckBox ID="chkEnableAdhocReport" runat="server" Text="Enable Adhoc Service Report" />

                                      </td></tr>

                           

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Mobile App Service Date Method</td>
                                <td class="CellTextBoxADM" colspan="2">
                                     <asp:DropDownList ID="ddlMobileAppServiceDateMethod" runat="server" Width="50%" AppendDataBoundItems="true">
                                 <asp:ListItem>--SELECT--</asp:ListItem>
                                           <asp:ListItem>SERVER DATE</asp:ListItem>
                                         <asp:ListItem>SCHEDULED DATE</asp:ListItem>
                                           </asp:DropDownList> 
                                                  
                                      </td>
                                <td class="CellTextBoxADM">
                                     &nbsp;</td></tr>

                             </table></td></tr>
                      </table><br />
                      
                             <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">SERVICE REPORT AUTOMATIC EMAIL SENDING</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">

                           

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">
                                         <asp:CheckBoxList ID="chkAttachServiceReport" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="113%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Attach Service Report</asp:ListItem>
                                             <asp:ListItem Value="2">Attach Supplementary Service Report</asp:ListItem>
                                           </asp:CheckBoxList></td>
                                <td class="CellTextBoxADM" colspan="1">
                                     &nbsp;</td></tr>

                                       

                             </table></td></tr>



                      </table><br />



                    <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">LOCATIONWISE DISPLAY</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">

                           

                                        <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">
                                         <asp:CheckBoxList ID="chkDisplayRecordsLocationwise" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="113%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Display Records Locationwise</asp:ListItem>
                                          
                                           </asp:CheckBoxList></td>
                                <td class="CellTextBoxADM" colspan="1">
                                     &nbsp;</td></tr>

                             </table></td></tr>
                      </table><br />

                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">WORKING HOURS</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%; text-align:center; padding-left:10px; padding-top:10px;">
                                  <tr  style="text-align:center;border: 1px solid #000000;">
                                      <td style="width:40%"></td>
                                      <td class="CellFormatADM" style="width:20%;text-align:center">Start</td>
                                      <td class="CellFormatADM" style="width:20%;text-align:center;">End</td>
                                      <td class="CellFormatADM" style="width:20%;text-align:center">OT Rate</td>
                                  </tr>
                           

                                        <tr><td class="CellFormatADM" style="padding-left:150px;width:40%;text-align:left">
                                        WeekDay</td>
                                <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtWeekDayStart" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                             <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtWeekDayEnd" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                             <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtWeekDayOTRate" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                        </tr>

                                     <tr><td class="CellFormatADM" style="padding-left:150px;width:40%;text-align:left">
                                        Saturday</td>
                                <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtSatStart" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                             <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtSatEnd" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                             <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtSatOTRate" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                        </tr>

                                     <tr><td class="CellFormatADM" style="padding-left:150px;width:40%;text-align:left">
                                        Sunday</td>
                                <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtSunStart" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                             <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtSunEnd" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                             <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtSunOTRate" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                        </tr>

                                    <tr><td class="CellFormatADM" style="padding-left:150px;width:40%;text-align:left">
                                        Holiday</td>
                                <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtHolidayStart" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                             <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtHolidayEnd" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                             <td class="CellTextBoxADM" colspan="1"> <asp:TextBox ID="txtHolidayOTRate" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                        </tr>

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

                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:100px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">SMS & WHATSAPP REMINDERS</td>
                    </tr>
                          
                                    <tr cellpadding="2">
                                <td class="CellTextBoxADM" colspan="2" style="padding-left:50px">
                                    <asp:CheckBoxList ID="chkSMSServices" runat="server" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="3" Width="403px">
                                            <asp:ListItem Value="1">Residential Services</asp:ListItem>
                                          <asp:ListItem Value="2">Corporate Services</asp:ListItem>
                                        
                                    </asp:CheckBoxList>
                                      </td></tr>
                            <tr cellpadding="2">
                               <td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left" colspan="2"> SMS Reminder
                                
                                     <asp:TextBox ID="txtSMSDays" runat="server" Width="30px"></asp:TextBox> Days Before the Scheduled Service
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
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblcity where rcno<>0"></asp:SqlDataSource>

       
        
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Country, Citizenship, Comments, Rcno, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, CountryCode, WS FROM tblcountry WHERE (Rcno &lt;&gt; 0)">
                       
                    </asp:SqlDataSource>

       <asp:SqlDataSource ID="SqlDSTerminationCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(Code, ' - ', Description) AS TC FROM tblterminationcode 
where Status = 'Y' ORDER BY Code">
                                                
                                  </asp:SqlDataSource>

          <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

        
       <asp:SqlDataSource ID="SqlDSContractCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(Code, ' : ', Description) AS CC FROM tblcontractcode 
where Status = 'Y' ORDER BY Code">
                                                
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



