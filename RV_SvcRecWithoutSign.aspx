<%@ Page Title="Service Record without Client Signature" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RV_SvcRecWithoutSign.aspx.vb" Inherits="RV_SvcRecWithoutSign" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            color: #FFFFFF;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%">
       <tr>
             <td style="text-align:left">
                      <a href="RV_Export_ServiceRecordDetail.aspx" target="_blank"><button class="button" style="background-color:#CFC6C0;font-weight:bold;width:100px;font-family:Calibri;font-size:14px;" type="button">View as PDF</button></a>

            </td>
            <td style="text-align: right;">
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Back" Width="90px" Height="30px" />
            </td>
            </tr>
    </table>
  <%--  <a href="RV_ServiceRecord02.aspx"><span style="color: #FFFFFF">RV_ServiceRecord02.aspx</span></a>--%><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" 
            Height="947px" ReportSourceID="CrystalReportSource1" Width="745px"
            ToolPanelView="None" 
            HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" 
            HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True" 
            ToolPanelWidth="0px" HasCrystalLogo ="false" EnableParameterPrompt="False" HasPrintButton="False" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="Reports\ServiceRecordReports\SvcRecWithoutSign.rpt">
        </Report>
    </CR:CrystalReportSource>

</asp:Content>


