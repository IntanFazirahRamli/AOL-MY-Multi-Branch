<%@ Page Title="Actual Revenue Report Selection" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_ActualRevenue.aspx.vb" Inherits="RV_Select_ActualRevenue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     
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
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
               
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
          
        <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">ACTUAL REVENUE REPORT</h4>
    
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
      <tr>
                      <td class="CellFormat" style="width:25%">ServiceDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;&nbsp;&nbsp;TO&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
                  <%--  <td class="CellTextBox" rowspan="5" style="text-align:left;padding-left:0px;">
                        
                    </td>--%>
                  </tr>
         
             <tr>
                      <td class="CellFormat">
                         ContractGroup
                             </td>
                     <td class="CellTextBox" colspan="1"> <%--<asp:dropdownlist ID="txtServiceID" runat="server" Width="37.5%" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist>--%>
                         <cc1:dropdowncheckboxes ID="txtServiceID" runat="server" Width="38%" UseSelectAllNode = "true" DataSourceID="SqlDSService" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="38%" DropDownBoxBoxWidth="38%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
                     </td>
                          
                 
                          
                               </tr>
             <tr>
                      <td class="CellFormat">
                         Team
                             </td>
                     <td class="CellTextBox" colspan="1">  <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataSourceID="SqlDSIncharge" DataTextField="inchargeId" DataValueField="inchargeId" Width="37.5%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                          </td>
                          
                 
                          
                               </tr>
             <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox">  <%--<asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="37.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>--%>
                        <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="37.5%" DropDownBoxBoxWidth="37.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                           </td>
                   
                  </tr>

               <tr>
                      <td class="CellFormat">Zone</td>
                    <td class="CellTextBox">
                          <cc1:dropdowncheckboxes ID="ddlZone" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSLocateGroup" DataTextField="locationgroup" DataValueField="locationgroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="37.5%" DropDownBoxBoxWidth="37.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                       
                         </td>
                 
                  </tr>
            <%--     <tr>
                    <td class="CellTextBox" colspan="3" style="padding-left: 30%;padding-top:20px;padding-bottom:20px; text-align:left; width:600px; word-spacing: 20px; text-indent: 40px;">
                      <asp:RadioButtonList ID="rdbSelect" runat="server" CellPadding="2" CellSpacing="2" RepeatDirection="Horizontal" Width="350px">
                          <asp:ListItem Selected="True">Detail</asp:ListItem>
                          <asp:ListItem>Summary</asp:ListItem>
                          
                      </asp:RadioButtonList></td>
              </tr>--%>
           <tr>
               <td colspan="2" style="padding-left:20%;padding-top:1%">   <asp:Panel ID="Panel1" runat="server" Height="95%" BorderStyle="Solid" BorderColor="#FF9900" BorderWidth="2px" Width="50%" HorizontalAlign="Center">
                                  <div style="font-size:15px;padding-top:5px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Display By</div>
                   <div style="padding-left:20px;text-align:left;padding-bottom:20px;">
                               <asp:radiobuttonList ID="chkGrouping" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Horizontal">
                                   <asp:ListItem Text="Date" Value="Date" Selected="true"></asp:ListItem>
                                  <%--   <asp:ListItem Text="AccountCode" Value="AccountCode"></asp:ListItem>
                                 --%>
                                    <asp:ListItem Text="Client" Value="Client"></asp:ListItem>
                                    <asp:ListItem Text="Team" Value="Team"></asp:ListItem>
                                    <asp:ListItem Text="PostalCode" Value="PostalCode"></asp:ListItem>
                                 
                               </asp:radiobuttonList></div>
                                        </asp:Panel>
               </td>

           </tr>
              <tr>
                 <td class="CellFormat">ReportType</td>
            <td colspan="1" style="padding-left: 2%; padding-top: 1%">
                <asp:Panel ID="Panel2" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="40%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 10px;">
                        <asp:RadioButtonList ID="rbtnSelectDetSumm" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="horizontal" Width="80%">
                             <asp:ListItem Text="Detail" Value="1" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Summary" Value="2"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>
        </tr>
           <tr style="text-align:center;">
          
            <td class="CENTERED" colspan="3" style="padding-top: 1%;text-align:center;">      
                           
                                   <asp:CheckBox ID="chkDistribution" runat="server" Text="Display based on Contract Distribution" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="true" />
           <br />     </td>
                                    
                             </tr>
        <tr>
                 <td></td>
                  <td colspan="1" style="text-align:left;padding-left:20px;"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick="currentdatetime()"/>
                           <asp:Button ID="btnExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>
           </table>
    
    <asp:Panel ID="pnlClientID" runat="server" BackColor="White" Width="50%" Height="50%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Client Information</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageClientID" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertClientID" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                             <tr>
                      <td class="CellFormat">ContractGroup</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlContractGroup" runat="server" Width="60.5%" Height="20px"
                                    DataTextField="contractgroup" DataValueField="contractgroup" CssClass="chzn-select" DataSourceID="SqlDSContractGroup" AppendDataBoundItems="True">
                                    <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />
                                   
                                </asp:DropDownList>   </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
                        <tr>
                      <td class="CellFormat">AccountType</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlAccountType" runat="server" Width="60.5%" Height="20px" AutoPostBack="true"
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
                           <asp:ImageButton ID="btnClient" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False"/>
 
                 
                    </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
               <tr>
                      <td class="CellFormat">AccountName</td>
                    <td class="CellTextBox" colspan="2">  <asp:TextBox ID="txtCustName" runat="server" MaxLength="200" Height="16px" Width="90%"></asp:TextBox>
                               </td>
                  </tr>
                            <tr>
                      <td class="CellFormat">LocationID</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtServiceLocationID" runat="server" MaxLength="15" Height="16px" Width="60%"></asp:TextBox>
                           <asp:ImageButton ID="btnSvcLocation" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
  <asp:TextBox ID="txtModal" runat="server" CssClass="dummybutton"></asp:TextBox>
                 
                 
                    </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintClientID" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="120px" Height="30px"/>
                                 <asp:Button ID="btnCancelClientID" runat="server" CssClass="bUtton" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" Height="30px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
        <asp:modalpopupextender ID="mdlPpClientID" runat="server" CancelControlID="btnCancelClientID" PopupControlID="pnlClientID"
                                    TargetControlID="btndummyClientID" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
                                </asp:modalpopupextender>
            <asp:Button ID="btndummyClientID" runat="server" Text="Button" CssClass="dummybutton" />
         
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
                 <asp:BoundField DataField="LocationID" HeaderText="Location ID">
                    <ItemStyle Wrap="False" />
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
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
        </div>
    </asp:Panel>

                <asp:modalpopupextender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient"
                                    TargetControlID="btndummyClient" BackgroundCssClass="modalBackground">
                                </asp:modalpopupextender>
                <asp:Button ID="btndummyClient" runat="server" Text="Button" CssClass="dummybutton" />
           
     <asp:Panel ID="pnlTeamSel" runat="server" BackColor="White" Width="45%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
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
                           </tr>
                <tr>
                    <td class="CellTextBox" colspan="3" style="padding-left: 10%;padding-top:20px;padding-bottom:20px; text-align:left; width:600px; word-spacing: 20px; text-indent: 40px;">
                      <asp:RadioButtonList ID="rdbSelect" runat="server" Visible="false" CellPadding="2" CellSpacing="2" RepeatDirection="Horizontal" Width="350px">
                          <asp:ListItem Selected="True">Detail</asp:ListItem>
                          <asp:ListItem>Summary</asp:ListItem>
                          
                      </asp:RadioButtonList></td>
              </tr>
                        
                            <tr style="padding-top:40px;padding-top:10px;">
                             <td colspan="2" style="text-align:left;padding-left:10%"><asp:Button ID="btnPrintTeamSel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="120px"/>
                                 <asp:Button ID="btnCloseTeamSel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

      <asp:ModalPopupExtender ID="mdlTeamSel" runat="server" CancelControlID="btnCloseTeamSel" PopupControlID="pnlTeamSel"
                                    TargetControlID="btndummyTeamSel" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                  <asp:Button ID="btndummyTeamSel" runat="server" Text="Button" CssClass="dummybutton" />

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
    <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup">
                       
            </asp:SqlDataSource>

      <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblContractGroup order by ContractGroup"></asp:SqlDataSource>
   
       <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (inchargeId) FROM tblteam ORDER BY inchargeID"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
          <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
 
</asp:Content>


