<%@ Page Title="Active Service Contract Listing Report Selection" Language="vb" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" Codefile="RV_Select_ActiveContract.aspx.vb" Inherits="RV_Select_ActiveContract" %>
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
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">ACTIVE SERVICE CONTRACT LISTING REPORT</h4>
    
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
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Contract Details</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
               <tr>
                      <td class="CellFormat">ActiveAsOfDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtActiveAsOfDate" runat="server" MaxLength="10" Height="16px" Width="25%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtActiveAsOfDate" TargetControlID="txtActiveAsOfDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender><%-- &nbsp;TO&nbsp; <asp:TextBox ID="txtContractDateTo" runat="server" MaxLength="10" Height="16px" Width="25%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtContractDateTo" TargetControlID="txtContractDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>--%>

                    </td>
               
             
                    <td class="CellTextBox" rowspan="6">
                           <asp:Panel ID="Panel1" runat="server" Height="95%" BorderStyle="Solid" BorderColor="#FF9900" BorderWidth="2px" Width="200px" HorizontalAlign="Center">
                                  <div style="font-size:15px;padding-top:10px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Display By</div>
                   <div style="padding-left:20px;text-align:left;padding-bottom:20px;">
                               <asp:radiobuttonList ID="chkGrouping" runat="server" CellSpacing="2" CellPadding="2">
                                  <asp:ListItem Text="Contract Group" Value="ContractGroup" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="AccountID" Value="AccountID"></asp:ListItem>
                                  <%--  <asp:ListItem Text="Salesman" Value="Salesman"></asp:ListItem>--%>
                               </asp:radiobuttonList></div>
                                        </asp:Panel>
                    </td>
               
             
                  </tr>
           <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox">  <%--<asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="60%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>--%>
                          <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="60.5%" DropDownBoxBoxWidth="60.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                           </td>
                
                  </tr>
               <tr>
                      <td class="CellFormat">ContractGroup</td>
                    <td class="CellTextBox"><cc1:dropdowncheckboxes ID="ddlContractGroup" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="60.5%" DropDownBoxBoxWidth="60.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes> <%--<asp:DropDownList ID="ddlContractGroup" runat="server" CssClass="chzn-select" DataSourceID="SqlDSContractGroup" DataTextField="contractgroup" DataValueField="contractgroup" Width="60%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>--%>
                         </td>
                 
                  </tr>

           <tr>
                      <td class="CellFormat">CategoryID</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlCategoryID" runat="server" Width="60%" Height="20px" AutoPostBack="FALSE"
                                     CssClass="chzn-select" >
                                    <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />
                                    <asp:ListItem Value="ADHOC">ADHOC</asp:ListItem>
                                    <asp:ListItem Value="CONTRACT">CONTRACT</asp:ListItem>
                         <asp:ListItem Value="PROJECT">PROJECT</asp:ListItem>
                                </asp:DropDownList>   </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>

            <tr>
                      <td class="CellFormat">Salesman</td>
                    <td class="CellTextBox"> <asp:DropDownList ID="ddlSalesMan" runat="server" CssClass="chzn-select" DataSourceID="SqlDSSalesman" DataTextField="staffid" DataValueField="staffid" Width="60%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>
                         </td>
                
                  </tr>
              <tr>
                      <td class="CellFormat">Scheduler</td>
                    <td class="CellTextBox"> <asp:DropDownList ID="ddlScheduler" runat="server" CssClass="chzn-select" DataSourceID="SqlDSScheduler" DataTextField="staffid" DataValueField="staffid" Width="60%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>
                         </td>
                  
                  </tr>
           <tr>
                      <td class="CellFormat">Incharge</td>
                    <td class="CellTextBox"> <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataSourceID="SqlDSIncharge" DataTextField="inchargeId" DataValueField="inchargeId" Width="60%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                         </td>
                   
                  </tr>
              <tr>
                      <td class="CellFormat">LocationGroup</td>
                    <td class="CellTextBox"> <asp:DropDownList ID="ddlLocateGrp" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSLocateGroup" DataTextField="locationgroup" DataValueField="locationgroup" Width="60%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" />
                        </asp:DropDownList>
                         </td>
                   
                  </tr>
              <tr>
                      <td class="CellFormat">Industry</td>
                    <td class="CellTextBox"><%--<asp:DropDownList ID="ddlIndustry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSIndustry" DataTextField="UPPER(industry)" DataValueField="UPPER(industry)" Width="60%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>--%>
                       <cc1:dropdowncheckboxes ID="ddlIndustry" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSIndustry" DataTextField="UPPER(industry)" DataValueField="UPPER(industry)" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="60.5%" DropDownBoxBoxWidth="60.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>

                    </td>
                   
                  </tr>
        
        <%--   <tr>
                      <td class="CellFormat">StartDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtStartDateFrom" runat="server" MaxLength="10" Height="16px" Width="25%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtStartDateFrom" TargetControlID="txtStartDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtStartDateTo" runat="server" MaxLength="10" Height="16px" Width="25%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtStartDateTo" TargetControlID="txtStartDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
                    <td class="CellTextBox">&nbsp;</td>
                 </tr>
            <tr>
                      <td class="CellFormat">EndDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtEndDateFrom" runat="server" MaxLength="10" Height="16px" Width="25%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtEndDateFrom" TargetControlID="txtEndDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtEndDateTo" runat="server" MaxLength="10" Height="16px" Width="25%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtEndDateTo" TargetControlID="txtEndDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
                    <td class="CellTextBox">&nbsp;</td>
                 </tr>
            <tr>
                      <td class="CellFormat">ActualEnd</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtActualEndFrom" runat="server" MaxLength="10" Height="16px" Width="25%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="txtActualEndFrom" TargetControlID="txtActualEndFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtActualEndTo" runat="server" MaxLength="10" Height="16px" Width="25%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="txtActualEndTo" TargetControlID="txtActualEndTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
                    <td class="CellTextBox">&nbsp;</td>
                 </tr>--%>
               <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Account Information</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
                      
                    <tr>
                      <td class="CellFormat">AccountType</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlAccountType" runat="server" Width="60%" Height="20px" AutoPostBack="true"
                                    DataTextField="ContType" DataValueField="ContType" CssClass="chzn-select" >
                                    <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />
                                    <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                                    <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                </asp:DropDownList>   </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
                 <tr>
                      <td class="CellFormat">AccountID</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtAccountID" runat="server" MaxLength="10" Height="16px" Width="60%"></asp:TextBox>
                           <asp:ImageButton ID="btnClient" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
 
                 
                    </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
               <tr>
                      <td class="CellFormat">AccountName</td>
                    <td class="CellTextBox" colspan="2">  <asp:TextBox ID="txtCustName" runat="server" MaxLength="200" Height="16px" Width="90%"></asp:TextBox>
                               </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
                     <tr>
                <td colspan="3" style="text-align: left; vertical-align: top;padding-left: 10.5%">
                <asp:CheckBox ID="chkIncludeContactInfo" runat="server" Text="Show Service Contact Information" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" />
                     
                          </td>
                         </tr>

         <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Sort By</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
               
                <tr>
                     <%-- <td class="CellFormat">SortBy</td>--%>
                    <td class="CellTextBox" colspan="3" style="text-align:center;padding-left:150px">
                        <table>
                            <tr>
                                <td rowspan="2"><asp:ListBox ID="lstSort1" runat="server" width="120px" Rows="6" SelectionMode="Multiple">
                                    <asp:ListItem Text="AccountID" Value="AccountID">AccountID</asp:ListItem>
                                    <asp:ListItem Text="Account Name" Value="CustName">Account Name</asp:ListItem>
                                     <asp:ListItem Text="ContractDate" Value="ContractDate">Contract Date</asp:ListItem>
                                   <asp:ListItem Text="Salesman" Value="Salesman">Salesman</asp:ListItem>
                                    <asp:ListItem Text="Scheduler" Value="Scheduler">Scheduler</asp:ListItem>
                                   <%--  <asp:ListItem Text="EndDate" Value="EndDate">EndDate</asp:ListItem>--%>
                                    </asp:ListBox></td>
                                <td colspan="2"><asp:Button ID="btnSort1" runat="server" Text=">>" /></td>
                                 <td rowspan="2"><asp:ListBox ID="lstSort2" runat="server" width="120px" Rows="6" SelectionMode="Multiple">
                                      <asp:ListItem Text="ContractNo" Value="ContractNo">ContractNo</asp:ListItem>
                                                 </asp:ListBox></td>
                            </tr>
                            <tr>
<td><asp:Button ID="btnSort2" runat="server" Text="<<" /></td>
                            </tr>
                        </table>
                         
                                    </td>
                    <td class="CellTextBox" style="text-align:center;padding-left:150px">
                        &nbsp;</td>
                  </tr>
           <%--   <tr>
                  <td><br /></td>
              </tr>--%>
              <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceContractList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick="currentdatetime()"/>
                      &nbsp;<asp:Button ID="btnExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true" OnClientClick="currentdatetime()"/>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceContractList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>
        </table>

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
                   
                  <%--  <asp:BoundField DataField="Address1" >
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
                    </asp:BoundField>--%>
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
                                    TargetControlID="btndummyClient" BackgroundCssClass="modalBackground">
                                </asp:modalpopupextender>
                <asp:Button ID="btndummyClient" runat="server" Text="Button" CssClass="dummybutton" />
           
      <asp:TextBox ID="txtQuery" runat="server" Visible="false"></asp:TextBox>

        <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup">
                       
            </asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlDSSalesman" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select StaffId from tblStaff where Roles = 'SALES MAN'">
                       
            </asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSScheduler" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select StaffId from tblStaff where SecGroupAuthority like  'SCHEDULER%' and Status = 'O'">
                       
            </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (inchargeId) FROM tblteam ORDER BY inchargeID"></asp:SqlDataSource>
          <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblProduct order by ProductID"></asp:SqlDataSource>

      <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
          <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(industry) FROM tblindustry ORDER BY industry"></asp:SqlDataSource>
        
</asp:Content>

