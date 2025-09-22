<%@ Page Title="Sales Performance Details By Department" Language="VB" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="false" CodeFile="RV_SalesPerformanceDetailsByDepartment.aspx.vb" Inherits="RV_SalesPerformanceDetailsByDepartment" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="text-align:center">
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Sales Performance Details By Department Report</h3>
    <br />
    <table style="width: 100%; text-align: center;">
        <tr>
             <td style="text-align:left">
                      <a href="RV_Export_SalesPerformance.aspx" target="_blank"><button class="button" style="background-color:#CFC6C0;font-weight:bold;width:100px;font-family:Calibri;font-size:14px;" type="button">View as PDF</button></a>

            </td>
            <td style="text-align: right;">
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Back" Width="90px" Height="30px" />
            </td>
            </tr>
        <tr>
            <td colspan="2">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    Height="947px" ReportSourceID="CrystalReportSource1" Width="845px"
                    ToolPanelView="None" EnableParameterPrompt="False" HasPrintButton="false"
                    HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False"
                    HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True"
                    ToolPanelWidth="0px" HasCrystalLogo="false" />
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                    <Report FileName="Reports\ManagementReports\SalesPerformanceReports\SalesPerformanceDetailsByDepartment.rpt">
                    </Report>
                </CR:CrystalReportSource>
            </td>
        </tr>
    </table>
   </div>
</asp:Content>





