<%@ Page Language="vb" AutoEventWireup="false" CodeFile="RV_Select_RevenueReportForAccounts.aspx.vb" MasterPageFile="~/MasterPage_Report.Master"
    Inherits="RV_Selection_RevenueReportForAccounts" Title="Revenue Report For Accounts" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

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
    <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">REVENUE REPORT FOR ACCOUNTS</h4>

    <table style="width: 100%; text-align: center;">
        <tr>
            <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
            </td>
        </tr>
    </table>

    <table style="width: 100%; font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: left; padding-left: 20px; line-height: 25px;">
        <tr>
            <td class="CellFormat" style="width: 20%">ServiceDate</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                &nbsp;&nbsp;&nbsp;TO&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
            </td>
             <td></td>
        </tr>

        <tr>
            <td class="CellFormat">Company Group</td>
            <td class="CellTextBox">
                <%--<asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="35%" Height="25px" AppendDataBoundItems="True">
                    <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="45%" DropDownBoxBoxWidth="45%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
            </td>
               <td class="CellTextBox" rowspan="7" style="text-align:left;padding-left:0px;">
                           <asp:Panel ID="Panel3" runat="server" Height="95%" BorderStyle="Solid" BorderColor="#FF9900" BorderWidth="2px" Width="280px" HorizontalAlign="Center">
                                  <div style="font-size:15px;padding-top:10px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Display By</div>
                   <div style="padding-left:20px;text-align:left;padding-bottom:20px;">
                                 <asp:RadioButtonList ID="chkGrouping" runat="server" CellSpacing="2" CellPadding="2"
                            RepeatDirection="vertical" AutoPostBack="true" OnSelectedIndexChanged="chkGrouping_SelectedIndexChanged">
                            <asp:ListItem Text="ContractGroup" Value="1" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Industry" Value="2"></asp:ListItem>
                            <asp:ListItem Text="ContractGroup & BillingFrequency" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Summary" Value="4"></asp:ListItem>
                        </asp:RadioButtonList></div>
                                        </asp:Panel>
                    </td>
        </tr>

        <tr>
            <td class="CellFormat">Contract Group</td>
            <td class="CellTextBox">
                <%--<asp:DropDownList ID="ddlMainContractGroup" runat="server" Width="35%" Height="20px"
                    DataTextField="contractgroup" DataValueField="contractgroup" CssClass="chzn-select" DataSourceID="SqlDSMainContractGroup" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                    <cc1:dropdowncheckboxes ID="txtServiceID" runat="server" Width="45%" UseSelectAllNode = "true" DataSourceID="SqlDSMainContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="45%" DropDownBoxBoxWidth="45%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
            </td>
        </tr>

        <tr>
            <td class="CellFormat">Billing Frequency</td>
            <td class="CellTextBox">
                <asp:DropDownList ID="ddlBillingFrequency" runat="server" Width="45%" Height="20px"
                    DataTextField="Frequency" DataValueField="Frequency" CssClass="chzn-select" DataSourceID="SqlDSBillingFrequency" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td class="CellFormat">Industry</td>
            <td class="CellTextBox">
               <%-- <asp:DropDownList ID="ddlIndustry" runat="server" Width="35%" Height="20px"
                    DataTextField="Industry" DataValueField="Industry" CssClass="chzn-select" DataSourceID="SqlDSIndustry" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                <cc1:dropdowncheckboxes ID="ddlIndustry" runat="server" Width="45%" UseSelectAllNode = "true" DataSourceID="SqlDSIndustry" DataTextField="Industry" DataValueField="Industry" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="45%" DropDownBoxBoxWidth="45%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
            </td>
        </tr>
          <tr>
                      <td class="CellFormat">Zone</td>
                    <td class="CellTextBox">
                          <cc1:dropdowncheckboxes ID="ddlZone" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSLocateGroup" DataTextField="locationgroup" DataValueField="locationgroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="45%" DropDownBoxBoxWidth="45%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                       
                         </td>
                 
                  </tr>
        <tr>
            <td colspan="2" style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: #800000; text-align: left; text-decoration: underline; padding-left: 10%;">Account Information</td>
            <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: #800000; text-align: left; text-decoration: underline; padding-left: 10%;">&nbsp;</td>
        </tr>

        <tr>
            <td class="CellFormat">AccountType</td>
            <td class="CellTextBox">
                <asp:DropDownList ID="ddlAccountType" runat="server" Width="45%" Height="20px" AutoPostBack="true"
                    DataTextField="ContType" DataValueField="ContType" CssClass="chzn-select">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                    <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                    <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                </asp:DropDownList>
            </td>
          
        </tr>
        <tr>
            <td class="CellFormat">AccountID</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtAccountID" runat="server" MaxLength="10" Height="16px" Width="45%"></asp:TextBox>
                <asp:ImageButton ID="btnClient" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" />
            </td>
            <td class="CellTextBox">&nbsp;</td>
        </tr>
        <tr>
            <td class="CellFormat">AccountName</td>
            <td class="CellTextBox" colspan="2">
                <asp:TextBox ID="txtCustName" runat="server" MaxLength="200" Height="16px" Width="50%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="2" style="padding-left: 20%; padding-top: 1%">
            
            </td>
        </tr>

        <tr>
            <td colspan="2" style="padding-left: 20%; padding-top: 1%">
                <asp:Panel ID="Panel2" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="77%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 20px;">
                        <asp:RadioButtonList ID="rbtnLstStatus" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                            <asp:ListItem Text="Complete" Value="Complete"></asp:ListItem>
                            <asp:ListItem Text="All" Value="All" Selected="true"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>
        </tr>
         <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                      <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
               
              </tr>
    </table>
    
                <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>

               <asp:Panel ID="pnlFormat" runat="server" BackColor="White" Width="350px" Height="200px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
                   
         <div style="text-align: center; padding-bottom: 20px;padding-top:20px;">
                        <asp:RadioButtonList ID="rbtnLstFormat" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Horizontal" Width="187px" CssClass="CellFormat" Font-Bold="True" Font-Names="Calibri" ForeColor="Black">
                            <asp:ListItem Text="Format1" Value="1" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Format2" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                   <br />
                   <div style="text-align:center;">
                       <asp:Button ID="btnOkFormat" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="80px"/>
                   &nbsp;&nbsp;&nbsp;        <asp:Button ID="btnCloseFormat" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="80px"/>
                  
                                </div>
                </asp:Panel>

                    <asp:ModalPopupExtender ID="mdlpnlFormat" runat="server" CancelControlID="btnCloseFormat" PopupControlID="pnlFormat"
                                    TargetControlID="btndummyFormat" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                  <asp:Button ID="btndummyFormat" runat="server" Text="Button" CssClass="dummybutton" />

    <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="900px" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
        <table>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <h4 style="color: #000000">Customer</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: center;">
                    <asp:TextBox ID="txtPopUpClient" runat="server" MaxLength="50" Height="16px" Width="400px" Text="Search Here for AccountID or Client details" ForeColor="Gray" AutoPostBack="True"></asp:TextBox>
                    <asp:ImageButton ID="btnPopUpClientSearch" OnClick="btnPopUpClientSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                    <asp:ImageButton ID="btnPopUpClientReset" OnClick="btnPopUpClientReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                </td>
                <td></td>
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

                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False">
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

                    <asp:BoundField DataField="Address1">
                        <ControlStyle Width="300px" />
                        <HeaderStyle Width="300px" />
                        <ItemStyle Width="300px" Wrap="False" />
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
                SelectCommand="SELECT * FROM tblcompany WHERE rcno <> 0 order by name"></asp:SqlDataSource>
        </div>
    </asp:Panel>

    <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient"
        TargetControlID="btndummyClient" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Button ID="btndummyClient" runat="server" Text="Button" CssClass="dummybutton" />

    <%--<asp:ModalPopupExtender ID="mdlPopUpFormat" runat="server" CancelControlID="btnPnlFormatClose" PopupControlID="pnlPopUpFormat"
        TargetControlID="rbtnLstFormat" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>--%>

    <asp:SqlDataSource ID="SqlDSMainContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSBillingFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Frequency FROM tblServiceFrequency order by Frequency"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Industry FROM tblIndustry order by Industry"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
</asp:Content>




