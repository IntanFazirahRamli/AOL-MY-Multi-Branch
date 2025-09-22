<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RV_Export_MonthlyCatchSummaryReport.aspx.vb" Inherits="RV_Export_MonthlyCatchSummaryReport" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="text-align:center">
        <br />
    <table style="width: 100%; text-align: center;">
        <tr>
            <td style="text-align:left">
              
            </td>
            <td style="text-align: right;">
              <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Back" Width="100px"
                   OnClick="btnQuit_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
               <%-- <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    Height="947px"  Width="845px"
                    ToolPanelView="None" EnableParameterPrompt="False"
                    HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False"
                    HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True"
                    ToolPanelWidth="0px" HasCrystalLogo="false" />--%>
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                    <Report FileName="Reports\ServiceRecordReports\ServiceRecordPrinting.rpt">
                    </Report>
                </CR:CrystalReportSource>
            </td>
        </tr>
    </table>
           <asp:TextBox ID="txtSvc" runat="server" Height="16px" MaxLength="10" Width="20%" CssClass="dummybutton"></asp:TextBox>

         </div>
</asp:Content>


