<%@ Page Title="Portfolio Movement Report Selection" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_Portfolio.aspx.vb" Inherits="RV_Select_Portfolio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <style>
            .loading-panel {
                background: rgba(0, 0, 0, 0.2) none repeat scroll 0 0;
                /*background: #fff none repeat scroll 0 0;*/
                position: relative;
                width: 100%;
            }

            .loading-container {
                /*background: rgba(49, 133, 156, 0.4) none repeat scroll 0 0;*/
                background: rgba(192,192,192,0.3) none repeat scroll 0 0;
                color: #fff;
                font-size: 90px;
                height: 100%;
                left: 0;
                padding-top: 15%;
                position: fixed;
                text-align: center;
                top: 0;
                width: 100%;
                z-index: 999999;
            }

         </style>

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
    
    <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>
 
     <asp:UpdatePanel ID="updPanelCompany" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
               
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release" AsyncPostBackTimeout="600">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
                    <asp:ControlBundle Name="CalendarExtender_Bundle" />                     
            </ControlBundles>
        </asp:ToolkitScriptManager>  

    <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">PORTFOLIO MOVEMENT REPORT</h4>

    <table style="width: 100%; text-align: center;">
        <tr>
            <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
            </td>
        </tr>
    </table>

    <table style="width: 100%; font-family: Calibri; font-size: 15px; font-weight: bold; color: black;padding-left: 20px; line-height: 25px;">
        <tr>
            <td class="CellFormat" style="width: 33%">ContractNo</td>
            <td class="CellTextBox" colspan="4">
                <asp:TextBox ID="txtContractNo" runat="server" MaxLength="25" Height="15px" Width="38%"></asp:TextBox>
                 </td>
        </tr>
         <tr>
            <td class="CellFormat" style="width: 25%">Date</td>
            <td class="CellTextBox" colspan="4">
                <asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="15%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                &nbsp;&nbsp; &nbsp;TO&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="15%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
            </td>
        </tr>
          <tr>
            <td class="CellFormat">CompanyGroup</td>
            <td class="CellTextBox" colspan="4">
                <asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="37.5%" Height="25px" AppendDataBoundItems="True">
                    <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td class="CellFormat">Contract Group</td>
            <td class="CellTextBox" colspan="4">
              <%--  <asp:DropDownList ID="ddlContractGroup" runat="server" Width="37.5%" Height="20px"
                    DataTextField="contractgroup" DataValueField="contractgroup" CssClass="chzn-select" DataSourceID="SqlDSContractGroup" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
                  <cc1:dropdowncheckboxes ID="txtServiceID" runat="server" Width="37%" UseSelectAllNode = "true" DataSourceID="SqlDSContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="37.5%" DropDownBoxBoxWidth="37.5%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
            </td>
        </tr>
          <%--  <tr>
            <td class="CellFormat">Contract Group Category</td>
            <td class="CellTextBox" colspan="4">
                <asp:DropDownList ID="ddlCategory" runat="server" Width="37.5%" Height="20px"
                    DataTextField="category" DataValueField="category" CssClass="chzn-select" DataSourceID="SqlDSContractGrpCategory" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Text="--SELECT--" Value="-1" />
                </asp:DropDownList>
            </td>
        </tr>--%>
            <tr>
            <td class="CellFormat">Salesman</td>
            <td class="CellTextBox" colspan="4">
               <asp:DropDownList ID="ddlSalesMan" runat="server" CssClass="chzn-select" DataSourceID="SqlDSSalesman" DataTextField="staffid" DataValueField="staffid" Width="37.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>
            </td>
        </tr>
     <%--   <tr>
           
            <td colspan="3"></td>
        </tr>--%>



          <tr style="text-align:center;">
                 

               <%-- <td colspan="1" style="text-align: right; vertical-align: top;padding-top: 1.5%">
                 <br />    
                          </td>--%>
            <td colspan="5" style="padding-left: 30%; padding-top: 1%;text-align:center;">
                <asp:Panel ID="Panel2" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="450px" HorizontalAlign="Center">
                   <div style="padding-left: 2px; text-align: center; padding-bottom: 20px;">
                         <table style="width: 100%;text-align:center">
                             <tr style="width:100%;text-align:left">
                                 <td colspan="1" rowspan="3" style="width:100%">   
                                       <asp:RadioButtonList ID="rbtnSelect" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Vertical" AutoPostBack="True">
                           <%-- <asp:ListItem Text="Contract Group Value Report" Value="1" Selected="true"></asp:ListItem>--%>
                            <asp:ListItem Text="Portfolio Movement Detail" Value="2" Selected="true"></asp:ListItem>                                
                                        <asp:ListItem Text="Portfolio Movement Summary" Value="3"></asp:ListItem>
                             <asp:ListItem Text="Portfolio Movement Summary by Contract Group Category" Value="4"></asp:ListItem>
                          <asp:ListItem Text="Portfolio Movement Summary by Report Group Category" Value="5"></asp:ListItem>
                           <asp:ListItem Text="Portfolio Movement Details with Location ID" Value="6"></asp:ListItem>
                        
                        </asp:RadioButtonList>
                                     <asp:CheckBox ID="chkIncludeOpeningDetails" runat="server" Text="Show Details in Opening" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Checked="True" Visible="False" />
                </td>                                    
              </tr>                           
                             </table>
                       
                     </div>
               </asp:Panel>
                </td>
              </tr>

                   
          <tr style="text-align:center;">
                 

               <%-- <td colspan="1" style="text-align: right; vertical-align: top;padding-top: 1.5%">
                 <br />    
                          </td>--%>
            <td colspan="5" style="padding-left: 30%; padding-top: 1%;text-align:center;">      
                            <asp:Panel ID="Panel1" runat="server" Visible="false" Height="50%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="450px" HorizontalAlign="Center">
                     <div style="padding-left: 2px; text-align: center; padding-bottom: 20px;"> 
                                <table style="width: 100%;text-align:left">
                         <tr style="width:100%;">
                                 <td colspan="1" rowspan="3" style="width:100%">  
                                        <asp:CheckBox ID="chkZeroValueRevisions" runat="server" Text="Include Zero Value Revisions" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="false" Visible="true" />
           <br />
                                       <asp:CheckBox ID="chkSalesmanIndividual" runat="server" Text="Generate Individual Files for Each Salesman" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="false" Visible="FALSE" />
           <br />
                                   <asp:CheckBox ID="chkDistribution" runat="server" Text="Display based on Contract Distribution" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="true" Visible="false" />
           <br />
                         <asp:CheckBox ID="chkGroupByContactType" runat="server" Text="Display Customer Type" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="true" Visible="false" />
             <br />  <asp:CheckBox ID="chkExcludeTerminationContracts" runat="server" Text="Do not include Excluded Termination Contracts" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Visible="False" />
                </td>
                                    
                             </tr>
                           
                             </table>
                          </div>
               </asp:Panel>
                          </td>
                                   
                                     </tr>

          
                      
          <tr>
            <td colspan="5">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Visible="False">
                    <asp:ListItem Text="Old Concept" Value="1"></asp:ListItem>
                     <asp:ListItem Text="New Concept" Value="2" Selected="true"></asp:ListItem>
                </asp:RadioButtonList><br /></td>
        </tr>
        <tr>
            <td style="width:5%">
                <asp:CheckBox ID="chkNew" runat="server" BorderColor="White" BorderStyle="None" Checked="True" Visible="False" />
            </td>
          
            <td style="text-align: left;width:5%">
                <asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Print" Width="100%" OnClientClick="currentdatetime()" visible="false" />
            </td>
          
            <td style="text-align: left;width:15%">
                <asp:Button ID="btnPrintExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Export to Excel" Width="100%" OnClientClick="currentdatetime()" />
            </td>
          
            <td style="text-align: left; width:15%">
                <asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="100%" OnClientClick="currentdatetime()"/>
            </td>
          
            <td style="text-align: left; width:25%">
                &nbsp;</td>
          
        </tr>
    </table>
     <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
     <asp:TextBox ID="txtQuerySumm" runat="server" CssClass="dummybutton" ></asp:TextBox>
       <asp:TextBox ID="txtQueryCGSumm" runat="server" CssClass="dummybutton" ></asp:TextBox>
     <asp:TextBox ID="txtQueryRGSumm" runat="server" CssClass="dummybutton" ></asp:TextBox>

     <asp:TextBox ID="txtCriteria" runat="server" CssClass="dummybutton" ></asp:TextBox>

    <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup where IncludeInPortfolio = True order by contractgroup">  </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDSContractGrpCategory" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT category FROM tblcontractgroupcategory order by category">
                   
            </asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDSSalesman" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select StaffId from tblStaff where Roles = 'SALES MAN'">
                       
            </asp:SqlDataSource>
                </ContentTemplate> 
          <Triggers>
              <%--<asp:AsyncPostBackTrigger ControlID="rbtnSelect" EventName="SelectedIndexChanged" />--%>
         <asp:PostBackTrigger ControlID="btnPrintExportToExcel" />
             <%--   <asp:PostBackTrigger ControlID="btnPrintServiceRecordList" />--%>
             
        </Triggers>
     </asp:UpdatePanel>
</asp:Content>

