<%@ Page Title="Consolidated Invoice" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RV_ConsolidatedInvoice.aspx.vb" Inherits="RV_SanitationReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            color: #FFFFFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
      <table style="width:100%">
           <tr>
                <td colspan="2" style="width:100%;text-align:RIGHT;"> 
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Visible="true" Text="QUIT" Width="90px" Height="30px" />
            </td>
            </tr>
 <tr>
     <td colspan="2" style="width:100%;text-align:center;">
       <%--  <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" 
            Height="947px" ReportSourceID="CrystalReportSource1" Width="868px"
            ToolPanelView="None" 
            HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" 
            HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True" 
            ToolPanelWidth="0px" HasCrystalLogo ="false" EnableParameterPrompt="False" HasExportButton="False" />--%>
       <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
           <Report FileName="Reports\ARReports\ConsolidatedEInvFormat.rpt">
           </Report>
       
    </CR:CrystalReportSource>
         </td>
 </tr>
     
             </table>

       <asp:Label ID="lblRecordNo" runat="server" Text="" Visible="false"></asp:Label>
          <asp:TextBox ID="txtQuery" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtSvcRcno" runat="server" Visible="False"></asp:TextBox>
</asp:Content>


