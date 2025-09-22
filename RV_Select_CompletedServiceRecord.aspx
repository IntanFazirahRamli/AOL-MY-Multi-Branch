<%@ Page Title="Completed Service Record Report Selection" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_CompletedServiceRecord.aspx.vb" Inherits="RV_Select_CompletedServiceRecord" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">COMPLETED SERVICE RECORD LISTING REPORT</h4>
    
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

       <table style="WIDTH:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;line-height:25px;">
                 <%--<tr>
                     <td colspan="2"  style="text-align:center">
                             <h5 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#800000;text-decoration:underline;">Scheduled Service Listing</h5>
       
                     </td>
                 </tr>--%>
                  <tr>
                      <td class="CellFormat" style="width:35%">Schedule Date</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSchDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtSchDateFrom" TargetControlID="txtSchDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtSchDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtSchDateTo" TargetControlID="txtSchDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
             <tr>
                      <td class="CellFormat">ServiceDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
                  </tr>
               <tr>
                      <td class="CellFormat">
                         Service ID
                             </td>
                     <td class="CellTextBox" colspan="1"> <asp:dropdownlist ID="txtServiceID" runat="server" Width="35.5%" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
                          
                 
                          
                               </tr>
              <tr>
                      <td class="CellFormat">
                         Service ID Desc
                             </td>
                     <td class="CellTextBox" colspan="1"> <asp:dropdownlist ID="ddlProduct" runat="server" Width="35.5%" DataSourceID="SqlDSService" DataTextField="ProductDesc" DataValueField="ProductID" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist><asp:CheckBox ID="chkProductAll" runat="server" Text="ALL" /></td>
                          
                               </tr>
            <tr>
                      <td class="CellFormat">
                         ScheduleType
                             </td>
                     <td class="CellTextBox" colspan="1">
                         <%--<asp:dropdownlist ID="ddlScheduleType" runat="server" Width="35.5%" DataSourceID="SqlDSScheduleType" DataTextField="ScheduleType" DataValueField="ScheduleType" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist>--%>
                       
                         <cc1:dropdowncheckboxes ID="ddlScheduleType" runat="server" Width="35.5%" UseSelectAllNode = "true" DataSourceID="SqlDSScheduleType" DataTextField="ScheduleType" DataValueField="ScheduleType" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35.5%" DropDownBoxBoxWidth="35.5%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
                     </td>
                          
                               </tr>
               <tr>
                             <td class="CellFormat">Team
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:TextBox ID="txtTeam" runat="server" MaxLength="30" Height="16px" Width="35%"></asp:TextBox>
                                   <asp:ImageButton ID="btnTeam" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" />
                            </td>
                           </tr>
               <tr>
                             <td class="CellFormat">Service By
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataSourceID="SqlDSIncharge" DataTextField="inchargeId" DataValueField="inchargeId" Width="35.5%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>         </td>
                           </tr>

                           <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox">  <%--<asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="35.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>--%>
                         <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="35.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35.5%" DropDownBoxBoxWidth="35.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                           </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
           <tr>
                      <td class="CellFormat" style="width:25%">ContractNo</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtContractNo" runat="server" MaxLength="30" Height="16px" Width="30%"></asp:TextBox>
                           </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
              <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:20%;">Account Information</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
                      
                    <tr>
                      <td class="CellFormat">AccountType</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlAccountType" runat="server" Width="35.5%" Height="20px" AutoPostBack="true"
                                    DataTextField="ContType" DataValueField="ContType" CssClass="chzn-select" >
                                    <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />
                                    <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                                    <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                </asp:DropDownList>   </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
                 <tr>
                      <td class="CellFormat">AccountID</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtAccountID" runat="server" MaxLength="10" Height="16px" Width="35%"></asp:TextBox>
                           <asp:ImageButton ID="btnClient" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
 
                 
                    </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
               <tr>
                      <td class="CellFormat">AccountName</td>
                    <td class="CellTextBox" colspan="2">  <asp:TextBox ID="txtCustName" runat="server" MaxLength="200" Height="16px" Width="70%"></asp:TextBox>
                               </td>
                  </tr>
                   
       <%--    <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Sort By</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
               
                <tr>
                 
                    <td class="CellTextBox" colspan="2" style="text-align:center;padding-left:150px">
                        <table>
                            <tr>
                                <td rowspan="2"><asp:ListBox ID="lstSort1" runat="server" width="120px" Rows="6" SelectionMode="Multiple">
                                     <asp:ListItem Text="ServiceDate" Value="ServiceDate">Service Date</asp:ListItem>
                                    <asp:ListItem Text="ServiceID" Value="ServiceID">Service ID</asp:ListItem>
                                   <asp:ListItem Text="ServiceBy" Value="ServiceBy">Service By</asp:ListItem>
                                    <asp:ListItem Text="AccountID" Value="AccountID">AccountID</asp:ListItem>
                                    <asp:ListItem Text="Client Name" Value="CustName">Client Name</asp:ListItem>
                                 
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
         <tr>
                    <td class="CellTextBox" colspan="3" style="padding-left: 180px;padding-top:20px;padding-bottom:20px; text-align:left; width:600px; word-spacing: 20px; text-indent: 40px;">
                      <asp:RadioButtonList ID="rdbFormat" runat="server" CellPadding="4" CellSpacing="4" RepeatDirection="Horizontal" Width="545px">
                          <asp:ListItem Selected="True">Format1</asp:ListItem>
                          <asp:ListItem>Format2</asp:ListItem>
                            <asp:ListItem>Format3</asp:ListItem>
                      </asp:RadioButtonList></td>
              </tr>
             <%-- <tr>
                  <td><br /></td>
              </tr>--%>
              <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                                   &nbsp;<asp:Button ID="btnExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true" OnClientClick="currentdatetime()"/>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
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
                   
<%--                    <asp:BoundField DataField="Address1" >
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
                                    TargetControlID="btndummyTeam" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                  <asp:Button ID="btndummyTeam" runat="server" Text="Button" CssClass="dummybutton" />
         
      <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblProduct order by ProductID"></asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (inchargeId) FROM tblteam ORDER BY inchargeID"></asp:SqlDataSource>
 <asp:SqlDataSource ID="SqlDSScheduleType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT ScheduleType FROM tblscheduletype ORDER BY Scheduletype"></asp:SqlDataSource>
    <asp:TextBox ID="txtModal" runat="server" Visible="false"></asp:TextBox>
      <asp:TextBox ID="txtQuery" runat="server" Visible="false"></asp:TextBox>

</asp:Content>

