<%@ Page Title="Scheduled Service Vs Completed Service" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_SchServiceVsCompleteService.aspx.vb" Inherits="RV_Select_SchServiceVsCompleteService" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
             </asp:ToolkitScriptManager>  
    <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">SCHEDULED SERVICE Vs COMPLETED SERVICE</h4>

    <table style="width: 100%; text-align: center;">
        <tr>
            <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
            </td>
        </tr>
    </table>

    <table style="width: 100%; font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: left; padding-left: 20px; line-height: 25px;">
        <tr>
            <td class="CellFormat" style="width: 25%">ServiceDate</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                &nbsp;&nbsp;&nbsp;TO&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
            </td>
        </tr>
          <tr>
            <td class="CellFormat">CompanyGroup</td>
            <td class="CellTextBox">
              <%--  <asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="37.5%" Height="25px" AppendDataBoundItems="True">
                    <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                 <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="37.5%" DropDownBoxBoxWidth="37.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
            </td>
        </tr>
         <tr>
                             <td class="CellFormat">Service By
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataSourceID="SqlDSIncharge" DataTextField="inchargeId" DataValueField="inchargeId" Width="37.5%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>         </td>
                              
                           </tr>
         <tr>
                      <td class="CellFormat">ContractGroup</td>
                    <td class="CellTextBox"> 
                         <cc1:dropdowncheckboxes ID="ddlContractGroup" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSContractGroup" DataTextField="contractgroup" DataValueField="contractgroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="37.5%" DropDownBoxBoxWidth="37.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                       
                         </td>
                 
                  </tr>
      
           <tr>
               <td colspan="2" style="padding-left:20%;padding-top:1%">   <asp:Panel ID="Panel1" runat="server" Height="95%" BorderStyle="Solid" BorderColor="#FF9900" BorderWidth="2px" Width="50%" HorizontalAlign="Center">
                                  <div style="font-size:15px;padding-top:5px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Display By</div>
                   <div style="padding-left:20px;text-align:left;padding-bottom:20px;">
                               <asp:radiobuttonList ID="chkGrouping" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Horizontal">
                                   <asp:ListItem Text="Date" Value="Date" Selected="true"></asp:ListItem>
                                  <%--   <asp:ListItem Text="AccountCode" Value="AccountCode"></asp:ListItem>
                                 --%>
                                    <asp:ListItem Text="Company Group" Value="Company Group"></asp:ListItem>
                                   <asp:ListItem Text="Zone" Value="Zone"></asp:ListItem>
                                 
                               </asp:radiobuttonList></div>
                                        </asp:Panel>
               </td>

           </tr>
      
              <tr style="text-align:center;">
          
            <td class="CENTERED" colspan="2" style="padding-top: 1%;text-align:center;">      
                           
                                   <asp:CheckBox ID="chkDistribution" runat="server" Text="Display based on Contract Distribution" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="true" />
  <br /></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="1" style="text-align: left;">
                <asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="Print" Width="100px" />
                <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="100px" />
            </td>
          
        </tr>
    </table>
      <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
       <asp:TextBox ID="txtQueryTotal" runat="server" CssClass="dummybutton" ></asp:TextBox>
      <asp:TextBox ID="txtQueryTotal2" runat="server" CssClass="dummybutton" ></asp:TextBox>
   
       <asp:Panel ID="pnlSel" runat="server" BackColor="White" Width="40%" Height="30%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                       <%--  <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Team Information</h4>
                             </td>
                         </tr>
                      
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertTeamSel" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                    <tr>
                             <td class="CellFormat">Team
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:TextBox ID="txtTeam" runat="server" MaxLength="30" Height="16px" Width="35%"></asp:TextBox>
                                   <asp:ImageButton ID="btnTeam" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" />
                            </td>
                           </tr>--%>
                <tr>
                    <td class="CellTextBox" colspan="3" style="padding-left: 10%;padding-top:20px;padding-bottom:20px; text-align:left; width:600px; word-spacing: 20px; text-indent: 40px;">
                      <asp:RadioButtonList ID="rdbSelect" runat="server" Visible="true" CellPadding="2" CellSpacing="2" RepeatDirection="Horizontal" Width="350px">
                          <asp:ListItem Selected="True">Detail</asp:ListItem>
                          <asp:ListItem>Summary</asp:ListItem>
                          
                      </asp:RadioButtonList></td>
              </tr>
                        
                            <tr style="padding-top:40px;padding-top:10px;">
                             <td colspan="2" style="text-align:left;padding-left:10%"><asp:Button ID="btnPrintSel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="120px"/>
                                 <asp:Button ID="btnCloseSel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

      <asp:ModalPopupExtender ID="mdlSel" runat="server" CancelControlID="btnCloseSel" PopupControlID="pnlSel"
                                    TargetControlID="btndummySel" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                  <asp:Button ID="btndummySel" runat="server" Text="Button" CssClass="dummybutton" />

      <asp:Panel ID="pnlZone" runat="server" BackColor="White" Width="40%" Height="30%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <br /><br />
                             </td>
                         </tr>
                       <%--  <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Team Information</h4>
                             </td>
                         </tr>
                      
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertTeamSel" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                    <tr>
                             <td class="CellFormat">Team
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:TextBox ID="txtTeam" runat="server" MaxLength="30" Height="16px" Width="35%"></asp:TextBox>
                                   <asp:ImageButton ID="btnTeam" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" />
                            </td>
                           </tr>--%>
                 <tr>
                      <td class="CellFormat">Zone</td>
                    <td class="CellTextBox">
                          <cc1:dropdowncheckboxes ID="ddlZone" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSLocateGroup" DataTextField="locationgroup" DataValueField="locationgroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="37.5%" DropDownBoxBoxWidth="37.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                       
                         </td>
                 
                  </tr>
                        
                            <tr style="padding-top:40px;padding-top:10px;">
                             <td colspan="2" style="text-align:left;padding-left:10%">
                                 <asp:Button ID="btnPrintZone" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="120px"/>
                                 <asp:Button ID="btnExportZone" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export to Excel" Width="150px"/>
                                 <asp:Button ID="btnCloseZone" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

      <asp:ModalPopupExtender ID="mdlZone" runat="server" CancelControlID="btnCloseZone" PopupControlID="pnlZone"
                                    TargetControlID="btndummyZone" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                  <asp:Button ID="btndummyZone" runat="server" Text="Button" CssClass="dummybutton" />

    <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup"></asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (inchargeId) FROM tblteam ORDER BY inchargeID"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup">
                       
            </asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
</asp:Content>

