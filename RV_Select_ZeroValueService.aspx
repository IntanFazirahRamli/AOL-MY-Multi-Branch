<%@ Page Language="vb" AutoEventWireup="false" CodeFile="RV_Select_ZeroValueService.aspx.vb" MasterPageFile="~/MasterPage_Report.Master"
    Inherits="RV_Selection_ZeroValueService" Title="Zero Value Service Report" %>
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
    <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">ZERO VALUE SERVICE REPORT</h4>

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
            <td class="CellFormat">ContractGroup
            </td>
            <td class="CellTextBox" colspan="1">
                <%--<asp:DropDownList ID="ddlServiceID" runat="server" Width="37.5%" DataSourceID="SqlDSService" DataTextField="ContractGroup" DataValueField="ContractGroup" AutoPostBack="FALSE" AppendDataBoundItems="TRUE">
                    <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                    <cc1:dropdowncheckboxes ID="txtServiceID" runat="server" Width="38%" UseSelectAllNode = "true" DataSourceID="SqlDSService" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="38%" DropDownBoxBoxWidth="38%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
            </td>
        </tr>
        <tr>
            <td class="CellFormat">CompanyGroup</td>
            <td class="CellTextBox">
                <%--<asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="37.5%" Height="25px" AppendDataBoundItems="True">
                    <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="37.5%" DropDownBoxBoxWidth="37.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
            </td>
        </tr>
         <tr>
            <td class="CellFormat">Schedule Type</td>
            <td class="CellTextBox">
                <asp:DropDownList ID="ddlScheduleType" runat="server" CssClass="chzn-select" DataSourceID="SqlDSScheduleType" DataTextField="ScheduleType" DataValueField="ScheduleType" Width="37.5%" Height="25px" AppendDataBoundItems="True">
                    <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:DropDownList>
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
        <tr>
            <td colspan="2"><br /></td>
        </tr>
          <tr>
                 <td></td>
                  <td colspan="1" style="text-align:left;padding-left:20px;"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick="currentdatetime()"/>
                           <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                <%--  <td style="text-align:center">&nbsp;</td>--%>
              </tr>
    </table>
       <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
 
    <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblContractGroup order by ContractGroup"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSScheduleType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT ScheduleType FROM tblScheduleType order by ScheduleType"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
</asp:Content>


