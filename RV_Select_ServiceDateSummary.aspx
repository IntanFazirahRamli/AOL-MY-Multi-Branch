<%@ Page Title="Service Date Summary" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_ServiceDateSummary.aspx.vb" Inherits="RV_Select_ServiceDateSummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
             </asp:ToolkitScriptManager>  
    <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">SERVICE DATE SUMMARY BY CONTRACT GROUP</h4>

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
            <td class="CellFormat">CompanyGroup</td>
            <td class="CellTextBox">
              <%--  <asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="37.5%" Height="25px" AppendDataBoundItems="True">
                    <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:DropDownList>--%>
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
      
          <tr>
            <td colspan="2"><br /></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="1" style="text-align: left;">
                <asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="Print" Width="100px" />
                &nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="100px" />
            </td>
          
        </tr>
    </table>

    <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup"></asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (inchargeId) FROM tblteam ORDER BY inchargeID"></asp:SqlDataSource>
       
</asp:Content>



