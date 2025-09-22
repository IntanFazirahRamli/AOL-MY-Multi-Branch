<%@ Page Title="Service Records for Billing" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_ServiceRecordBilling.aspx.vb" Inherits="RV_Select_ServiceRecordBilling" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <script type="text/javascript" src="JS/jquery-1.6.1.min.js" ></script>

    <script type="text/javascript" src="JS/Dropdownscript.js" ></script>
    <script type="text/javascript" src="JS/Dropdownscript.min.js" ></script>
   <link rel="Stylesheet" type="text/css" href="CSS/DefaultStyles.css" />
   
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">SERVICE RECORD FOR BILLING REPORT</h4>
    
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
                      <td class="CellFormat" style="width:25%">CompanyGroup</td>
                    <td class="CellTextBox">  <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="26.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="26.5%" DropDownBoxBoxWidth="26.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                           </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
               <tr>
                      <td class="CellFormat">ServiceDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="26%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="26%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                   
               
             
                  </tr>
           <tr>
                      <td class="CellFormat">
                         ServiceStatus
                             </td>
                      <td colspan="2" style="padding-left:10px;">
                           <asp:radiobuttonlist ID="rdbBillStatus" runat="server" Width="60%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3" CellPadding="1" CellSpacing="1" TextAlign="Right" Enabled="True">
                               <asp:ListItem>All</asp:ListItem>
                                <asp:ListItem Selected="True">Completed</asp:ListItem>
                               <asp:ListItem>Open</asp:ListItem>
                                   
                               </asp:radiobuttonlist>
                            
                      </td>
                   
                         </tr>
              <tr>
                      <td class="CellFormat">
                        
                             </td>
                      <td colspan="2" style="padding-left:10px;">
                                 <asp:CheckBox ID="chkZeroValueServices" runat="server" ForeColor="Black" Text="Include Zero Value Services" />
                         <br />   <asp:CheckBox ID="chkPartialBilled" runat="server" ForeColor="Black" Text="Include Partially Billed Services" />
                   <br />    <asp:CheckBox ID="chkCNServices" runat="server" ForeColor="Black" Text="Include Services with Credit Notes" />
                      
                      </td>
                   
                         </tr>
     
                   <tr><td><br /></td></tr>
       <%--    <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Sort By</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
               
                <tr>
                    
                    <td class="CellTextBox" colspan="2" style="text-align:center;padding-left:150px">
                        <table>
                            <tr>
                                <td rowspan="2"><asp:ListBox ID="lstSort1" runat="server" width="120px" Rows="6" SelectionMode="Multiple">
                                    <asp:ListItem Text="Status" Value="Status">Status</asp:ListItem>
                                    <asp:ListItem Text="Client Name" Value="CustName">Client Name</asp:ListItem>
                                    <asp:ListItem Text="ServiceBy" Value="ServiceBy">Service By</asp:ListItem>
                                    <asp:ListItem Text="ServiceDate" Value="ServiceDate">Service Date</asp:ListItem>
                                    <asp:ListItem Text="TimeIn" Value="TimeIn">Time In</asp:ListItem>
                                    </asp:ListBox></td>
                                <td><asp:Button ID="btnSort1" runat="server" Text=">>" /></td>
                                 <td rowspan="2"><asp:ListBox ID="lstSort2" runat="server" width="120px" Rows="6" SelectionMode="Multiple"></asp:ListBox></td>
                            </tr>
                            <tr>
<td colspan="1"><asp:Button ID="btnSort2" runat="server" Text="<<" /></td>
                            </tr>
                        </table>
                         
                                    </td>
                  </tr>--%>
           <%--   <tr>
                  <td><br /></td>
              </tr>--%>
              <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" Visible="False" />
                      <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>
        </table>
      <asp:GridView ID="GridView1" runat="server" Width="744px" CssClass="Centered" >
                          <Columns>
           
                          </Columns>
                           <EditRowStyle BackColor="#999999" />
                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                   <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                   <SortedAscendingCellStyle BackColor="#E9E7E2" />
                   <SortedAscendingHeaderStyle BackColor="#506C8C" />
                   <SortedDescendingCellStyle BackColor="#FFFDF8" />
                   <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                      </asp:GridView>
                 
                <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
    
     
            <asp:TextBox ID="txtCriteria" runat="server" CssClass="dummybutton" ></asp:TextBox>


        <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
    
    <asp:TextBox ID="txtModal" runat="server" Visible="false"></asp:TextBox>
     <asp:TextBox ID="txtModalClient" runat="server" Visible="false"></asp:TextBox>
    </asp:Content>