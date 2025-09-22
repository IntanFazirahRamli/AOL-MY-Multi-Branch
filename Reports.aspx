<%@ Page Title="Reports" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reports.aspx.vb" Inherits="Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        var defaultTextTeam = "Search Here for Team, Incharge or ServiceBy";
        function WaterMarkTeam(txt, evt) {
            if (txt.value.length == 0 && evt.type == "blur") {
                txt.style.color = "gray";
                txt.value = defaultTextTeam;
            }
            if (txt.value == defaultTextTeam && evt.type == "focus") {
                txt.style.color = "black";
                txt.value = "";
            }
        }

        var defaultTextClient = "Search Here for AccountID or Client details";
        function WaterMarkClient(txt, evt) {
            if (txt.value.length == 0 && evt.type == "blur") {
                txt.style.color = "gray";
                txt.value = defaultTextClient;
            }
            if (txt.value == defaultTextClient && evt.type == "focus") {
                txt.style.color = "black";
                txt.value = "";
            }
        }
    </script>
       <style type="text/css">
      
          .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:30%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
      .CellTextBox{
         font-family:'Calibri';
        color:black;
        text-align:left;
     padding-left:20px;
    }
         .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}
     
           .auto-style1 {
               width: 100%;
               height: 29px;
           }
     
           </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>      
               <asp:controlBundle name="TabContainer_Bundle"/>
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
    
          <div style="text-align:center">

     
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">REPORTS</h3>
       
        <table style="width:100%;text-align:center;">
            <tr>
                <td colspan="2"><br /></td>
            </tr>
            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>  

                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
          
            <tr>
                <td colspan="2" style="width:100%;text-align:RIGHT;"> 
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px" Height="30px" />
            </td>
            </tr>
            <tr>
     <td colspan="2" style="text-align:left">
         <asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="1" Font-Names="Calibri" Height="100%" Width="100%" CssClass="ajax__tab_xp" >
    <asp:TabPanel runat="server" HeaderText="SERVICE RECORD" ID="TabPanel1">
        <HeaderTemplate>
            Service Record
        </HeaderTemplate>
        
        <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Service Record Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top-left-radius : 20px;border-top-right-radius:25px; border-top-style: none; border-top-color: inherit; border-top-width: 1px;" class="auto-style1">SERVICE RECORD</td></tr>

       
                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkServiceRecordsList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black" AutoPostBack="True">
                            
                            <asp:ListItem Value="SERVICE RECORD LISTING">1. Service Record Listing</asp:ListItem>
                            <asp:ListItem Value="SERVICE RECORD DETAIL LISTING">2. Service Record Detail Listing</asp:ListItem>
                             <asp:ListItem Value="SERVICE RECORD DETAIL LISTING BY CLIENT">3. Service Record Detail Listing by Client</asp:ListItem>
                             <asp:ListItem Value="SERVICE RECORD LISTING BY STAFF">4. Service Record Listing by Staff</asp:ListItem>
                             <asp:ListItem Value="SERVICE RECORD DETAIL LISTING BY STAFF">5. Service Record Detail Listing by Staff</asp:ListItem>
                               <asp:ListItem Value="SCHEDULED SERVICE LISTING">6. Scheduled Service Listing</asp:ListItem>
                            <asp:ListItem Value="COMPLETED SERVICE LISTING">7. Completed Service Listing</asp:ListItem>
                            <asp:ListItem Value="SERVICE RECORD PRINTING">8. Service Record Printing</asp:ListItem>
                         <asp:ListItem Value="SERVICE RECORD WITH DUPLICATE TEAM MEMBERS">9. Service Record with Duplicate Team Members</asp:ListItem>
                         <asp:ListItem Value="SERVICE RECORD WITH DIFFERENT SCHEDULE DATE AND ACTUAL SERVICE DATE">10. Service Record with Different Schedule Date and Actual Service Date</asp:ListItem>
                         
                         </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
                
            <br />
                 <asp:Panel ID="Panel11" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Service Team Vehicle Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">SERVICE TEAM VEHICLE</td></tr>

                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkServiceTeamVehicleList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Service Team Member Listing">1. Service Team Member Listing</asp:ListItem>
                            <asp:ListItem Value="Service Team Vehicle Listing">2. Service Team Vehicle Listing</asp:ListItem>
                             <asp:ListItem Value="Service Team Equipment & Tools Listing">3. Service Team Equipment & Tools Listing</asp:ListItem>
                                   </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
             
              <asp:TabPanel runat="server" HeaderText="SERVICE CONTRACT" ID="TabPanel3">
        <HeaderTemplate>
            Service Contract
        </HeaderTemplate>
        
        <ContentTemplate>
            <table>
                <tr>
                     <td style="width:50%">
                             <asp:Panel ID="Panel3" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Service Contract Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">SERVICE CONTRACT</td></tr>

                              <tr>
                                  <td style="padding-left:70px; line-height: 25px;">
   <asp:CheckBoxList ID="chkServiceContractList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black" AutoPostBack="True">
                            
                            <asp:ListItem Value="Service Contract Listing">1. Service Contract Listing</asp:ListItem>
                            <asp:ListItem Value="Service Contract Listing by Incharge">2. Service Contract Listing by Incharge</asp:ListItem>
                             <asp:ListItem Value="Service Contract Listing by Support Staff">3. Service Contract Listing by Support Staff</asp:ListItem>
        <asp:ListItem Value="Service Contract Expiry/Renewal Listing">4. Service Contract Expiry/Renewal Listing</asp:ListItem>
                          <asp:ListItem Value="Offset Contract Expiry/Renewal Listing">5. Offset Contract Expiry/Renewal Listing</asp:ListItem>
             <asp:ListItem Value="Service Contract Quote Price Listing">6. Service Contract Quote Price Listing</asp:ListItem>
     
                                   </asp:CheckBoxList>
                                  </td>
                                
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
                    </td>
                    <td style="width:50%">
                             <asp:Panel ID="Panel4" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Due for Renewal Service Contract Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">DUE FOR RENEWAL SERVICE CONTRACT</td></tr>

                              <tr>
                                  <td style="padding-left:70px; line-height: 25px;">
   <asp:CheckBoxList ID="chkDueRenewalServiceContractList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Due for Renewal Service Contract Listing by Client ID">1. Due for Renewal Service Contract Listing by Client ID</asp:ListItem>
                            <asp:ListItem Value="Due for Renewal Service Contract Listing by Contract Group">2. Due for Renewal Service Contract Listing by Contract Group</asp:ListItem>
                             <asp:ListItem Value="Due for Renewal Service Contract Listing by Salesman">3. Due for Renewal Service Contract Listing by Salesman</asp:ListItem>
      
                                   </asp:CheckBoxList>
                                      <br /><br /><br />
                                  </td>
                                
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
                    </td>
                </tr>
                  <tr>
                     <td style="width:50%">
                             <asp:Panel ID="Panel5" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Active Service Contract Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">ACTIVE SERVICE CONTRACT</td></tr>

                              <tr>
                                  <td style="padding-left:70px; line-height: 25px;">
   <asp:CheckBoxList ID="chkActiveContractList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Active Service Contract Listing by Client ID">1. Active Service Contract Listing by Client ID</asp:ListItem>
                            <asp:ListItem Value="Active Service Contract Listing by Contract Group">2. Active Service Contract Listing by Contract Group</asp:ListItem>
                          
                                   </asp:CheckBoxList><br />
                                  </td>
                                
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
                    </td>
                    <td style="width:50%">
                             <asp:Panel ID="Panel6" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Renewed Service Contract Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">RENEWED SERVICE CONTRACT</td></tr>

                              <tr>
                                  <td style="padding-left:70px; line-height: 25px;">
   <asp:CheckBoxList ID="chkRenewedContractList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Renewed Service Contract Listing by Client ID">1. Renewed Service Contract Listing by Client ID</asp:ListItem>
                            <asp:ListItem Value="Renewed Service Contract Listing by Contract Group">2. Renewed Service Contract Listing by Contract Group</asp:ListItem>
                             <asp:ListItem Value="Renewed Service Contract Listing by Salesman">3. Renewed Service Contract Listing by Salesman</asp:ListItem>
      
                                   </asp:CheckBoxList>
                                   
                                  </td>
                                
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
                    </td>
                </tr>
                    <tr>
                     <td style="width:50%">
                             <asp:Panel ID="Panel1" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="New Service Contract Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">NEW SERVICE CONTRACT</td></tr>

                              <tr>
                                  <td style="padding-left:70px; line-height: 25px;">
   <asp:CheckBoxList ID="chkNewContractList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="New Service Contract Listing by Client ID">1. New Service Contract Listing by Client ID</asp:ListItem>
                            <asp:ListItem Value="New Service Contract Listing by Contract Group">2. New Service Contract Listing by Contract Group</asp:ListItem>
                           <asp:ListItem Value="New Service Contract Listing by Salesman">3. New Service Contract Listing by Salesman</asp:ListItem>
                          
                                   </asp:CheckBoxList>
                                  </td>
                                
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
                    </td>
                    <td style="width:50%">
                             <asp:Panel ID="Panel7" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Renewal Service Contract Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">RENEWAL SERVICE CONTRACT</td></tr>

                              <tr>
                                  <td style="padding-left:70px; line-height: 25px;">
   <asp:CheckBoxList ID="chkRenewalContractList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Renewal Service Contract Listing by Client ID">1. Renewal Service Contract Listing by Client ID</asp:ListItem>
                            <asp:ListItem Value="Renewal Service Contract Listing by Contract Group">2. Renewal Service Contract Listing by Contract Group</asp:ListItem>
                             <asp:ListItem Value="Renewal Service Contract Listing by Salesman">3. Renewal Service Contract Listing by Salesman</asp:ListItem>
      
                                   </asp:CheckBoxList>
                                   
                                  </td>
                                
                              </tr>
                                </table>
                           
                     
               </asp:Panel>
                    </td>
                </tr>
                                 <tr>
                     <td style="width:50%">
                             <asp:Panel ID="Panel8" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Terminated Service Contract Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">TERMINATED SERVICE CONTRACT</td></tr>

                              <tr>
                                  <td style="padding-left:70px; line-height: 25px;">
   <asp:CheckBoxList ID="chkTerminatedContractList" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Terminated Service Contract Listing by Client ID">1. Terminated Service Contract Listing by Client ID</asp:ListItem>
                            <asp:ListItem Value="Terminated Contract Listing by Contract Group">2. Terminated Service Contract Listing by Contract Group</asp:ListItem>
                           <asp:ListItem Value="Terminated Contract Listing by Salesman">3. Terminated Service Contract Listing by Salesman</asp:ListItem>
                          
                                   </asp:CheckBoxList>
                                  </td>
                                
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
                    </td>
                    <td style="width:50%">
                   
                    </td>
                </tr>
            </table>
            </ContentTemplate>
        </asp:TabPanel>
              <asp:TabPanel runat="server" HeaderText="MANAGEMENT" ID="TabPanel4">
        <HeaderTemplate>
            Management
        </HeaderTemplate>
        
        <ContentTemplate>
              <asp:Panel ID="Panel9" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Management Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">PRODUCTIVITY</td></tr>

       
                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkManagementList" runat="server" Font-Bold="false" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Team Productivity Report - Details">1. Team Productivity Report - Details</asp:ListItem>
                            <asp:ListItem Value="Team Productivity Report - Summary">2. Team Productivity Report - Summary</asp:ListItem>
                             <asp:ListItem Value="Manpower Productivity by Team Leader - Details">3. Manpower Productivity by Team Leader - Details</asp:ListItem>
                             <asp:ListItem Value="Manpower Productivity by Team Leader - Summary">4. Manpower Productivity by Team Leader - Summary</asp:ListItem>
                             <asp:ListItem Value="Manpower Productivity by Team Members - Details">5. Manpower Productivity by Team Members - Details</asp:ListItem>
                               <asp:ListItem Value="Manpower Productivity by Team Members - Summary">6. Manpower Productivity by Team Members - Summary</asp:ListItem>
                            <asp:ListItem Value="Manpower Productivity by Team Members - Over Time Details">7. Manpower Productivity by Team Members - Over Time Summary</asp:ListItem>
                            <asp:ListItem Value="Manpower Productivity by Team Members - Over Time Summary">8. Manpower Productivity by Team Members - Over Time Summary</asp:ListItem>
                         <asp:ListItem Value="Vehicle Productivity Report - Details">9. Vehicle Productivity Report - Details</asp:ListItem>
                         <asp:ListItem Value="Vehicle Productivity Report - Summary">10. Vehicle Productivity Report - Summary</asp:ListItem>
                         
                         </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
                     
               </asp:Panel>
                
         
            </ContentTemplate>
        </asp:TabPanel>
              <asp:TabPanel runat="server" HeaderText="REVENUE" ID="TabPanel2">
        <HeaderTemplate>
            Revenue
        </HeaderTemplate>
        
        <ContentTemplate>
              <asp:Panel ID="Panel10" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Actual Revenue Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">ACTUAL REVENUE</td></tr>

       
                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkActualRevenueList" runat="server" Font-Bold="false" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Actual Revenue Report by Date">1. Actual Revenue Report by Date</asp:ListItem>
                            <asp:ListItem Value="Actual Revenue Report by Team">2. Actual Revenue Report by Team</asp:ListItem>
                             <asp:ListItem Value="Actual Revenue Report by Client">3. Actual Revenue Report by Client</asp:ListItem>
                             <asp:ListItem Value="Actual Revenue Report by Account Code">4. Actual Revenue Report by Account Code</asp:ListItem>
                             <asp:ListItem Value="Actual Revenue Report by Postal Code">5. Actual Revenue Report by Postal Code</asp:ListItem>
                                   </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
               </asp:Panel>
                <br />
             <asp:Panel ID="Panel12" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Forecasted Revenue Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">FORECASTED REVENUE</td></tr>

       
                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkForcastedRevenueList" runat="server" Font-Bold="false" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Forecasted Revenue Report by Date">1. Forecasted Revenue Report by Date</asp:ListItem>
                            <asp:ListItem Value="Forecasted Revenue Report by Team">2. Forecasted Revenue Report by Team</asp:ListItem>
                             <asp:ListItem Value="Forecasted Revenue Report by Client">3. Forecasted Revenue Report by Client</asp:ListItem>
                             <asp:ListItem Value="Forecasted Revenue Report by Account Code">4. Forecasted Revenue Report by Account Code</asp:ListItem>
                             <asp:ListItem Value="Forecasted Revenue Report by Postal Code">5. Forecasted Revenue Report by Postal Code</asp:ListItem>
                                   </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
               </asp:Panel>
             <br />
             <asp:Panel ID="Panel13" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Other Revenue Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">OTHERS</td></tr>

       
                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkOtherRevenueList" runat="server" Font-Bold="false" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Zero Value Listing">1. Zero Value Listing</asp:ListItem>
                                      </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
               </asp:Panel>
               <br />
             <asp:Panel ID="Panel14" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Accounts Revenue Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">ACCOUNTS</td></tr>

       
                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkAccountsRevenueList" runat="server" Font-Bold="false" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Revenue Report for Accounts by Contract Group">1. Revenue Report for Accounts by Contract Group</asp:ListItem>
                            <asp:ListItem Value="Revenue Report for Accounts by Contract Group and Billing Frequency">2. Revenue Report for Accounts by Contract Group and Billing Frequency</asp:ListItem>
                             <asp:ListItem Value="Revenue Report for Accounts by Industry">3. Revenue Report for Accounts by Industry</asp:ListItem>
                                    </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
               </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
              <asp:TabPanel runat="server" HeaderText="PORTFOLIO" ID="TabPanel5">
        <HeaderTemplate>
            Portfolio
        </HeaderTemplate>
        
        <ContentTemplate>
              <asp:Panel ID="Panel15" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Portfolio Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">PORTFOLIO</td></tr>

       
                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkPortfolioList" runat="server" Font-Bold="false" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Contract Group Value Report">1. Contract Group Value Report</asp:ListItem>
                            <asp:ListItem Value="Portfolio Movement Detail">2. Portfolio Movement Detail</asp:ListItem>
                             <asp:ListItem Value="Portfolio Movement Summary">3. Portfolio Movement Summary</asp:ListItem>
       <asp:ListItem Value="Portfolio Value Summary by Contract Group Category">4. Portfolio Value Summary by Contract Group Category</asp:ListItem>
                                     </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
               </asp:Panel>
            <br />
             <asp:Panel ID="Panel16" runat="server" CssClass="roundbutton" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Schedule Summary Reports" HorizontalAlign="Center">
                          <table style="width:100%;text-align:left"><tr>
                               <td colspan="3" style="width:100%;font-size:17px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:30px;padding-top:5px;background-color:lightgray;vertical-align:middle; border-top: 1px;border-top-left-radius : 20px;border-top-right-radius:25px; ">SCHEDULE SUMMARY</td></tr>

       
                              <tr>
                                  <td style="padding-left:100px; line-height: 25px;">
   <asp:CheckBoxList ID="chkScheduleSummaryList" runat="server" Font-Bold="false" Font-Names="Calibri" Font-Size="16px" ForeColor="Black">
                            
                            <asp:ListItem Value="Scheduled Service VS Completed Service">1. Scheduled Service VS Completed Service</asp:ListItem>
                                    </asp:CheckBoxList>
                                  </td>
                              </tr>
                          </table>
                           
               </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
        </asp:TabContainer>
         </td>
                </tr>
            </table>


                 <asp:Panel ID="pnlServiceRecordListing" runat="server" BackColor="White" Width="65%" Height="98%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;line-height:20px;">
                <tr>
                     <td colspan="2" style="text-align:center">
                             <h5 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#800000;text-decoration:underline;">SERVICE RECORD LISTING</h5>
       
                     </td>
                 </tr>   
               <tr>
                      <td class="CellFormat">
                         Status
                             </td>
                      <td>
                            <asp:checkboxlist ID="chkStatusSearch" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4" CellPadding="2" CellSpacing="2" TextAlign="Right" Enabled="True">
                                   <asp:ListItem Value="O">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="C">C - Cancelled</asp:ListItem>
                                     <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                    <asp:ListItem Value="H">H - On Hold</asp:ListItem>  
                                     <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                    <asp:ListItem Value="B">B - Job cannot complete</asp:ListItem>                              
                                    <asp:ListItem Value="P">P - Posted</asp:ListItem> 
                               <%--<asp:ListItem Value="ALL">ALL STATUS</asp:ListItem>--%>
                               </asp:checkboxlist>
                      </td>
                         </tr>
                           <tr>
                      <td class="CellFormat">
                         Service Record
                             </td>
                     <td class="CellTextBox" colspan="1"><asp:TextBox ID="txtServiceRecord" runat="server" MaxLength="30" Height="16px" Width="50%"></asp:TextBox> </td>
                               </tr>
                <tr>
                      <td class="CellFormat">
                         Service ID
                             </td>
                     <td class="CellTextBox" colspan="1"> <asp:dropdownlist ID="txtServiceID" runat="server" Width="50%" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
                          
                               </tr>
               <tr>
                             <td class="CellFormat">Service By
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:TextBox ID="txtServiceBy" runat="server" MaxLength="30" Height="16px" Width="45%"></asp:TextBox>
                                   <asp:ImageButton ID="btnTeam" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" />
                            </td>
                           </tr>
                 
                      <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="50%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>
                           </td>
                  </tr>
            <tr>
                      <td class="CellFormat">AccountType</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlAccountType" runat="server" Width="50%" Height="20px" AutoPostBack="true"
                                    DataTextField="ContType" DataValueField="ContType" CssClass="chzn-select" >
                                    <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />
                                    <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                                    <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                </asp:DropDownList>   </td>
                  </tr>
                 <tr>
                      <td class="CellFormat">AccountID</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtAccountID" runat="server" MaxLength="10" Height="16px" Width="45%"></asp:TextBox>
                           <asp:ImageButton ID="btnClient" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
 
                 
                    </td>
                  </tr>
               <tr>
                      <td class="CellFormat">AccountName</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtCustName" runat="server" MaxLength="200" Height="16px" Width="80%" ReadOnly="True"></asp:TextBox>
                               </td>
                  </tr>
                  <tr>
                      <td class="CellFormat">ServiceDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="22%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="22%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
              <tr>
                  <td class="CellFormat">ServiceFrequency</td>
                  <td class="CellTextBox">  <asp:dropdownlist ID="ddlServiceFrequency" runat="server" Width="50%" DataSourceID="SqlDSFrequency" DataTextField="Frequency" AppendDataBoundItems="TRUE" DataValueField="Frequency" >
                <asp:ListItem Selected="TRUE" Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
              </tr>
              <tr>
                  <td class="CellFormat">BillingFrequency</td>
                  <td class="CellTextBox"> <asp:dropdownlist ID="ddlBillingFrequency" runat="server" AppendDataBoundItems="TRUE" Width="50%" DataSourceID="SqlDSBillingFrequency" DataTextField="Frequency" DataValueField="Frequency" >
                <asp:ListItem Selected="TRUE" Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
              </tr>
                <tr>
                  <td class="CellFormat">TargetID</td>
                  <td class="CellTextBox"> <asp:dropdownlist ID="ddlTargetID" runat="server" AppendDataBoundItems="TRUE" Width="50%" DataSourceID="SqlDSTarget" DataTextField="TargetID" DataValueField="Descrip1" >
                <asp:ListItem Selected="TRUE" Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
              </tr>

                <tr>
                      <td class="CellFormat">To Bill Amt</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtBillAmtFrom" runat="server" MaxLength="10" Height="16px" Width="22%"></asp:TextBox>                        
                                  &nbsp;TO&nbsp; <asp:TextBox ID="txtBillAmtTo" runat="server" MaxLength="10" Height="16px" Width="22%"></asp:TextBox>                        
                          </td>
                  </tr>

                <tr>
                      <td class="CellFormat">Notes</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtNotes" runat="server" MaxLength="10" Height="32px" Width="80%" TextMode="MultiLine"></asp:TextBox>                        
                                    </td>
                  </tr>
                <tr>
                      <td class="CellFormat">SortBy</td>
                    <td class="CellTextBox">
                        <table>
                            <tr>
                                <td rowspan="2"><asp:ListBox ID="lstSort1" runat="server" width="120px" Rows="5" SelectionMode="Multiple">
                                    <asp:ListItem Text="Status" Value="Status">Status</asp:ListItem>
                                    <asp:ListItem Text="Client Name" Value="CustName">Client Name</asp:ListItem>
                                    <asp:ListItem Text="ServiceBy" Value="ServiceBy">Service By</asp:ListItem>
                                    <asp:ListItem Text="ServiceDate" Value="ServiceDate">Service Date</asp:ListItem>
                                    <asp:ListItem Text="TimeIn" Value="TimeIn">Time In</asp:ListItem>
                                    </asp:ListBox></td>
                                <td><asp:Button ID="btnSort1" runat="server" Text=">>" /></td>
                                 <td rowspan="2"><asp:ListBox ID="lstSort2" runat="server" width="120px" Rows="5" SelectionMode="Multiple"></asp:ListBox></td>
                            </tr>
                            <tr>
<td><asp:Button ID="btnSort2" runat="server" Text="<<" /></td>
                            </tr>
                        </table>
                         
                                    </td>
                  </tr>
           <%--   <tr>
                  <td><br /></td>
              </tr>--%>
              <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                                  &nbsp;<asp:Button ID="btnpnlCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" />
                        </td>
              </tr>
        </table>
           </asp:Panel>

                <asp:Panel ID="pnlServiceSchedule" runat="server" BackColor="White" Width="40%" Height="50%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          <br />    <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;line-height:25px;">
                 <tr>
                     <td colspan="2"  style="text-align:center">
                             <h5 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#800000;text-decoration:underline;">Scheduled Service Listing</h5>
       
                     </td>
                 </tr>
                  <tr>
                      <td class="CellFormat">Schedule Date</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSchDateFrom" runat="server" MaxLength="10" Height="16px" Width="30%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtSchDateFrom" TargetControlID="txtSchDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtSchDateTo" runat="server" MaxLength="10" Height="16px" Width="30%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtSchDateTo" TargetControlID="txtSchDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
      
              <tr>
                  <td><br /></td>
              </tr>
              <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceSchedule" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                                  &nbsp;<asp:Button ID="btnCloseServiceSchedule" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" />
                        </td>
              </tr>
        </table>
           </asp:Panel>
                <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="900px" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
          <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpClient" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for AccountID or Client details" ForeColor = "Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpClientSearch" OnClick="btnPopUpClientSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpClientReset" OnClick="btnPopUpClientReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
       
        <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="700px" OnSelectedIndexChanged="gvClient_SelectedIndexChanged" PageSize="15">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" >
                     <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" ReadOnly="True">
                       <ControlStyle Width="5%" />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="5%" Wrap="False" />
                </asp:BoundField>
                
                    <asp:BoundField DataField="Name" HeaderText="Client" SortExpression="Name">
                        <ControlStyle Width="300px" />
                        <HeaderStyle Width="300px" />
                        <ItemStyle Width="300px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="False" />
                    </asp:BoundField>
                   
                    <asp:BoundField DataField="Address1" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Telephone2" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                   <asp:BoundField DataField="Email" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="LocateGrp" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="CompanyGroup" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddBlock" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddNos" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddFloor" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddUnit" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddStreet" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddBuilding" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddCity" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddState" >                   
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddCountry" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddPostal" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Fax" >
                       <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                       <asp:BoundField DataField="Mobile" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Telephone" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ContID" Visible="False">
                        <ControlStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" Wrap="False" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT * FROM tblcompany WHERE rcno <> 0 order by name">
         
            </asp:SqlDataSource>
        </div>
    </asp:Panel>

                <asp:modalpopupextender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient"
                                    TargetControlID="btndummyClient" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
                                </asp:modalpopupextender>
                <asp:Button ID="btndummyClient" runat="server" Text="Button" CssClass="dummybutton" />
           

              <asp:Panel ID="pnlPopUpTeam" runat="server" BackColor="White" Width="700px" Height="500px" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">

         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Team</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpTeam" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for Team, Incharge or ServiceBy" ForeColor = "Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpTeamSearch" OnClick="btnPopUpTeamSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpTeamReset" OnClick="btnPopUpTeamReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
        <asp:TextBox ID="txtPopupTeamSearch" runat="server" Visible="False"></asp:TextBox>
            <br />

            <asp:GridView ID="gvTeam" CssClass="Centered" runat="server" DataSourceID="SqlDSTeam" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="2" GridLines="None" Width="600px" OnSelectedIndexChanged="gvTeam_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="TeamID" HeaderText="Id" SortExpression="TeamID">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TeamName" HeaderText="Name" SortExpression="TeamName">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InChargeId" HeaderText="InCharge Id" SortExpression="InChargeId">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="True" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDSTeam" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblTeam where status <> 'N' order by TeamName"></asp:SqlDataSource>
        
    </asp:Panel>
             <asp:ModalPopupExtender ID="mdlPopUpTeam" runat="server" CancelControlID="btnPnlTeamClose" PopupControlID="pnlPopUpTeam"
                                    TargetControlID="btndummyTeam" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
                                </asp:ModalPopupExtender>
                  <asp:Button ID="btndummyTeam" runat="server" Text="Button" CssClass="dummybutton" />
         
      <asp:ModalPopupExtender ID="mdlPopupSvcRecordListing" runat="server" CancelControlID="" PopupControlID="pnlServiceRecordListing" TargetControlID="btndummySvcRecord" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummySvcRecord" runat="server" Text="Button" CssClass="dummybutton" />
                    <asp:ModalPopupExtender ID="mdlPopupSvcSchedule" runat="server" CancelControlID="" PopupControlID="pnlServiceSchedule" TargetControlID="btndummySvcRecord" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummySvcSchedule" runat="server" Text="Button" CssClass="dummybutton" />
              </div>
               <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct * FROM tblservicefrequency order by frequency"></asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlDSBillingFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblfrequency order by frequency"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSTarget" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblTarget order by TargetID"></asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblProduct order by ProductID"></asp:SqlDataSource>   
</asp:Content>

