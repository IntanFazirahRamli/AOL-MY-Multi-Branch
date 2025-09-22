<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RV_IndustryList.aspx.vb" Inherits="RV_IndustryList" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
    <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;text-align:center;">Industry List Report</h3><br />
    <table><tr><td style="width:20%"></td>
        <td>  <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="CrystalReport.rpt">
            </Report>
        </CR:CrystalReportSource>
       <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource1" ToolPanelView="None" />
           </td>
        <td style="width:20%"></td>
           </tr></table>
   
</asp:Content>

