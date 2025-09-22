<%@ Page Title="Revenue Report By Contract Group & Industry" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_RevenueReportByContractGrpIndustry.aspx.vb" Inherits="RV_Select_RevenueReportByContractGrpIndustry" %>
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
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">REVENUE REPORT BY CONTRACT GROUP AND INDUSTRY</h4>
    
              <table style="width: 100%; text-align: center;">
        <tr>
            <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
            </td>
        </tr>
    </table>

    <table class="Centered" style="width: 100%; font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: left; padding-left: 100px; line-height: 25px;">
        <tr>
            <td class="CellFormat" style="width: 20%">ServiceDate</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                &nbsp;&nbsp;&nbsp;TO&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
            </td>
            
        </tr>

         
        <tr>
            <td class="CellFormat">AccountType</td>
            <td class="CellTextBox">
                <asp:DropDownList ID="ddlAccountType" runat="server" Width="35%" Height="20px" AutoPostBack="true"
                    DataTextField="ContType" DataValueField="ContType" CssClass="chzn-select">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                    <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                    <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                </asp:DropDownList>
            </td>
          
        </tr>
        <tr>
            <td class="CellFormat">Contract Group</td>
            <td class="CellTextBox">
             <%--   <asp:DropDownList ID="ddlMainContractGroup" runat="server" Width="35%" Height="20px"
                    DataTextField="contractgroup" DataValueField="contractgroup" CssClass="chzn-select" DataSourceID="SqlDSMainContractGroup" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                  <cc1:dropdowncheckboxes ID="txtServiceID" runat="server" Width="35%" UseSelectAllNode = "true" DataSourceID="SqlDSMainContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35%" DropDownBoxBoxWidth="35%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
            </td>
        </tr>

      
        <tr>
            <td class="CellFormat">Industry</td>
            <td class="CellTextBox">
               <%-- <asp:DropDownList ID="ddlIndustry" runat="server" Width="35%" Height="20px"
                    DataTextField="Industry" DataValueField="Industry" CssClass="chzn-select" DataSourceID="SqlDSIndustry" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                 <cc1:dropdowncheckboxes ID="ddlIndustry" runat="server" Width="35%" UseSelectAllNode = "true" DataSourceID="SqlDSIndustry" DataTextField="Industry" DataValueField="Industry" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35%" DropDownBoxBoxWidth="35%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
            </td>
        </tr>
          <tr>
                 <td class="CellFormat">ReportType</td>
            <td colspan="1" style="text-align: left;padding-left:20px ">
                <asp:Panel ID="Panel1" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="50%" HorizontalAlign="Center">
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
          
            <td class="CENTERED" colspan="2" style="padding-top: 1%;text-align:center;">      
                           
                                   <asp:CheckBox ID="chkDistribution" runat="server" Text="Display based on Contract Distribution" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="true" />
           <br />     </td>
                                    
                             </tr>
        <tr>
            <td colspan="2" style="padding-left: 20%; padding-top: 1%">
            
            </td>
        </tr>
   <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" Visible="FALSE"/>
                      <asp:Button ID="btnExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                
              </tr>
    </table>
    
                <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
    
     <asp:SqlDataSource ID="SqlDSMainContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Industry FROM tblIndustry order by Industry"></asp:SqlDataSource>
            
</asp:Content>


