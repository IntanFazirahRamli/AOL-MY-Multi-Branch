<%@ Page Title="Sales Performance Report Selection" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_SalesPerformance.aspx.vb" Inherits="RV_Select_SalesPerformance" %>
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
</script>
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
               
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
          
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">SALES PERFORMANCE REPORT</h4>
    
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
                      <td class="CellFormat" style="width:25%">Date</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;&nbsp;&nbsp;TO&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
                  </tr>
           
             <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox">  <%--<asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="35.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>--%>
                        <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35.5%" DropDownBoxBoxWidth="35.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>&nbsp;<asp:Label ID="lblCompanyGroup" runat="server" Text=""></asp:Label>
                           </td>
                   
                  </tr>
            <tr>
                      <td class="CellFormat">ContractGroup</td>
                    <td class="CellTextBox"> <%--<asp:DropDownList ID="ddlContractGroup" runat="server" CssClass="chzn-select" DataSourceID="SqlDSContractGroup" DataTextField="contractgroup" DataValueField="contractgroup" Width="35.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>--%>
                         <cc1:dropdowncheckboxes ID="ddlContractGroup" runat="server" Width="35%" UseSelectAllNode = "true" DataSourceID="SqlDSContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35%" DropDownBoxBoxWidth="35%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
                         </td>
                 
                  </tr>
           <tr>
                      <td class="CellFormat">Industry</td>
                    <td class="CellTextBox"><asp:DropDownList ID="ddlIndustry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSIndustry" DataTextField="UPPER(industry)" DataValueField="UPPER(industry)" Width="35.5%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                         </td>
                   
                  </tr>
        
                <tr>
                      <td class="CellFormat">Salesman</td>
                    <td class="CellTextBox"> <asp:DropDownList ID="ddlSalesMan" runat="server" CssClass="chzn-select" DataSourceID="SqlDSSalesman" DataTextField="staffid" DataValueField="staffid" Width="35.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>
                         </td>
                
                  </tr>
             <tr>
                      <td class="CellFormat">Zone</td>
                    <td class="CellTextBox">
                          <cc1:dropdowncheckboxes ID="ddlZone" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSLocateGroup" DataTextField="locationgroup" DataValueField="locationgroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35.5%" DropDownBoxBoxWidth="35.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                       
                         </td>
                 
                  </tr>
           <tr>
                          <td class="CellFormat">DisplayBy</td>
            <td colspan="1" style="padding-left: 2%; padding-top: 1%">
                <asp:Panel ID="Panel2" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="80%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 10px;">
                        <asp:RadioButtonList ID="rbtnSelect" runat="server" CellSpacing="7" CellPadding="7" RepeatDirection="Vertical" RepeatColumns="3" Width="90%">
                               <asp:ListItem Text="Salesman" Value="1" Selected="true"></asp:ListItem>
                              <asp:ListItem Text="CompanyGroup" Value="2"></asp:ListItem>                       
                          <asp:ListItem Text="Department" Value="3"></asp:ListItem>
                         
                             <asp:ListItem Text="MarketSegment" Value="4"></asp:ListItem>
                             <asp:ListItem Text="Top 10 Customers" Value="5"></asp:ListItem>
                          
                          </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>
        </tr>
         <tr>
                 <td class="CellFormat">ReportType</td>
            <td colspan="1" style="padding-left: 2%; padding-top: 1%">
                <asp:Panel ID="Panel1" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="40%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 10px;">
                        <asp:RadioButtonList ID="rbtnSelectDetSumm" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="horizontal" Width="80%">
                             <asp:ListItem Text="Detail" Value="1" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Summary" Value="2"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>
        </tr>
      
           <%--    <tr>
                           <td class="CellFormat">Status</td>
                            <td class="CellTextBox"> <asp:DropDownList ID="ddlStatus" runat="server" Width="97%">
                                
                                  <asp:ListItem Value="O">O - Open</asp:ListItem>
                                  <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                <asp:ListItem Value="R">R - Resigned</asp:ListItem>
                                </asp:DropDownList></td></tr>--%>
           <tr>
               <td colspan="2"><br /></td>
           </tr>
                 <%--<tr>
                    <td class="CellTextBox" colspan="3" style="padding-left: 30%;padding-top:20px;padding-bottom:20px; text-align:left; width:600px; word-spacing: 20px; text-indent: 40px;">
                      <asp:RadioButtonList ID="rdbSelect" runat="server" CellPadding="2" CellSpacing="2" RepeatDirection="Horizontal" Width="350px">
                          <asp:ListItem Selected="True">Detail</asp:ListItem>
                          <asp:ListItem>Summary</asp:ListItem>
                          
                      </asp:RadioButtonList></td>
              </tr>--%>
             <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick="currentdatetime()"/>
                                  &nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>
           </table>

      <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
         <%--<asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblProduct order by ProductID"></asp:SqlDataSource>--%>
   
        <%--<asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (inchargeId) FROM tblteam ORDER BY inchargeID"></asp:SqlDataSource>--%>
     <asp:SqlDataSource ID="SqlDSSalesman" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select StaffId from tblStaff where Roles = 'SALES MAN'">
                       
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup">
                       
            </asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(industry) FROM tblindustry ORDER BY industry"></asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
</asp:Content>




