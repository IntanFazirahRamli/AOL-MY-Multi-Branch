<%@ Page Title="Distribution Records" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_Distribution.aspx.vb" Inherits="RV_Select_Distribution" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <script type="text/javascript">

          function onCalendarShown2() {

              var cal = $find("calendar2");
              //Setting the default mode to month
              cal._switchMode("months", true);

              //Iterate every month Item and attach click event to it
              if (cal._monthsBody) {
                  for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                      var row = cal._monthsBody.rows[i];
                      for (var j = 0; j < row.cells.length; j++) {
                          Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call2);
                      }
                  }
              }
          }

          function onCalendarHidden2() {
              var cal = $find("calendar2");
              //Iterate every month Item and remove click event from it
              if (cal._monthsBody) {
                  for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                      var row = cal._monthsBody.rows[i];
                      for (var j = 0; j < row.cells.length; j++) {
                          Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call2);
                      }
                  }
              }

          }

          function call2(eventElement) {
              var target = eventElement.target;
              switch (target.mode) {
                  case "month":
                      var cal = $find("calendar2");
                      cal._visibleDate = target.date;
                      cal.set_selectedDate(target.date);
                      cal._switchMonth(target.date);
                      cal._blur.post(true);
                      cal.raiseDateSelectionChanged();
                      break;
              }
          }


    </script>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
             </asp:ToolkitScriptManager>  
    <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">Contract Value Distribution Records</h4>

    <table style="width: 100%; text-align: center;">
        <tr>
            <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
            </td>
        </tr>
    </table>

    <table style="width: 100%; font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: left; padding-left: 20px; line-height: 25px;">
        <tr>
            <td class="CellFormat" style="width: 25%">Distribution Date</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom"  OnClientShown="onCalendarShown2" OnClientHidden="onCalendarHidden2" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True" BehaviorID="calendar2"></asp:CalendarExtender>
                &nbsp;&nbsp;&nbsp;TO&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
            </td>
        </tr>
          <tr>
                      <td class="CellFormat">ContractGroup</td>
                    <td class="CellTextBox"> 
                         <cc1:dropdowncheckboxes ID="ddlContractGroup" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSContractGroup" DataTextField="contractgroup" DataValueField="contractgroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="37.5%" DropDownBoxBoxWidth="37.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                       
                         </td>
                 
                  </tr>

            <tr style="text-align:center;">
          <td></td>
            <td class="CENTERED" colspan="1" style="padding-top: 1%;text-align:left;">      
                           
                                   <asp:CheckBox ID="chkLatestDistribution" runat="server" Text="Only show the latest Distribution record" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="true" />
           <br />     </td>
                                    
                             </tr>
         <%-- <tr>
            <td class="CellFormat">CompanyGroup</td>
            <td class="CellTextBox">
              
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
         
      --%>
          <tr>
            <td colspan="2"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="1" style="text-align: left;">
                <asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" Visible="false" BackColor="#CFC6C0" Font-Bold="True" Text="Print" Width="100px" />
                <asp:Button ID="btnExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="100px" />
            </td>
          
        </tr>
    </table>
      <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
      <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup">
                       
            </asp:SqlDataSource>
<%--
    <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup"></asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (inchargeId) FROM tblteam ORDER BY inchargeID"></asp:SqlDataSource>
       
       <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                    
            </asp:SqlDataSource>  --%> 
</asp:Content>



