<%@ Page Title="Contract Excluded from Batch Price Change" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_ContractWithoutPriceChange.aspx.vb" Inherits="RV_Select_ContractWithoutPriceChange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <style type="text/css">
      
          .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:25%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
              padding-right:5px;
    }
          </style>
  
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
             </asp:ToolkitScriptManager>  
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Contract Excluded from Batch Price Change</h4>
    
      <table style="width:100%;text-align:center;">
             <%--  <tr>
                     <td colspan="2" style="text-align:center">
                             <h5 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#800000;text-decoration:underline;">SERVICE RECORD LISTING</h5>
       
                     </td>
                 </tr>--%>
         
<%--            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>  

                      </td> 
            </tr>--%>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
          
          <%--  <tr>
                <td colspan="2" style="width:100%;text-align:RIGHT;"> 
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px" Height="30px" />
            </td>
            </tr>--%>
            </table>

      <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;line-height:20px;">
               
               <tr>
                      <td class="CellFormat" style="width:25%">
                         Status
                             </td>
                      <td colspan="1" style="width:75%">
                         <%--   <asp:checkboxlist ID="chkStatus" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="5" CellPadding="3" CellSpacing="3" TextAlign="Right" Enabled="True">
                              
                                 <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                                   <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                               </asp:checkboxlist>--%>
                            <asp:radiobuttonlist ID="chkStatus" runat="server" Width="50%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3" CellPadding="1" CellSpacing="1" TextAlign="Right" Enabled="True">
                               <asp:ListItem>Active</asp:ListItem>
                               <asp:ListItem>Inactive</asp:ListItem>
                               <asp:ListItem Selected="True">All</asp:ListItem>
                                  
                               </asp:radiobuttonlist>
                          
                      </td>
                    
                         </tr>
          
        
               <tr>
                      <td class="CellFormat">ContractDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtContractDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtContractDateFrom" TargetControlID="txtContractDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtContractDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtContractDateTo" TargetControlID="txtContractDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               </tr>
                 
             <tr>
                      <td class="CellFormat">
                         ContractGroup
                             </td>
                     <td class="CellTextBox" colspan="1"> <%--<asp:dropdownlist ID="txtServiceID" runat="server" Width="37.5%" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist>--%>
                         <cc1:dropdowncheckboxes ID="ddlContractGroup" runat="server" Width="38%" UseSelectAllNode = "true" DataSourceID="SqlDSContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="38%" DropDownBoxBoxWidth="38%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
                     </td>
                          
                 
                          
                               </tr>
                 
          <tr>
              <td colspan="3"><br /></td>
          </tr>
           
                 <tr>
                <td></td>
                  <td colspan="1" style="text-align:left;padding-left:20px;"><asp:Button ID="btnPrintServiceContractList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick="currentdatetime()"/>
                           <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceContractList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>
            
        </table>

               
        <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup">
                       
            </asp:SqlDataSource>
     
          <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
 
</asp:Content>

