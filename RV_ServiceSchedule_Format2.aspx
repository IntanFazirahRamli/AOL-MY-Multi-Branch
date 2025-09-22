<%@ Page Title="Service Schedule Report" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RV_ServiceSchedule_Format2.aspx.vb" Inherits="RV_ServiceSchedule_Format2" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%">
     <tr>
             <td style="text-align:left">
                      <asp:Button ID="btnPrintPDF" runat="server" visible="false" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print as PDF" Width="120px"/>
            
                  <asp:Button ID="btnExportToExcel" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="False" />
                  <a href="RV_Export_ServiceSchedule.aspx" target="_blank"><button class="button" style="background-color:#CFC6C0;font-weight:bold;width:100px;font-family:Calibri;font-size:14px;" type="button">View as PDF</button></a>

            </td>
            <td style="text-align: right;">
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Back" Width="90px" Height="30px" />
            </td>
            </tr>
        </table>
     <%--<div style="text-align:left">  <a style="color:brown; font-weight: bold;" href="RV_Select_ServiceSchedule.aspx"><span>Back</span></a></div>--%>
    <%--<a href="RV_ServiceSchedule.aspx">RV_ServiceSchedule.aspx</a>--%><%--<a href="RV_ServiceSchedule.aspx">RV_ServiceSchedule.aspx</a>--%><CR:CrystalReportViewer ID="CRVServiceSchedule" runat="server" AutoDataBind="true" 
            Height="947px" ReportSourceID="CRSServiceSchedule" Width="845px"
            ToolPanelView="None" 
            HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" 
            HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True" 
            ToolPanelWidth="0px" HasCrystalLogo ="false" HasPrintButton="False" />
    <CR:CrystalReportSource ID="CRSServiceSchedule" runat="server">
        <Report FileName="Reports\ServiceRecordReports\ServiceSchedule_Format2.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>

