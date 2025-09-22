<%@ Page Language="vb" AutoEventWireup="false" CodeFile="RV_ContractServiceSchedule.aspx.vb" 
    Inherits="RV_ContractServiceSchedule" MasterPageFile="~/MasterPage.Master"
    Title="Contract Service Schedule" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            color: #FFFFFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align:center">
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Contract Service Schedule</h3>
    <br />
    <table style="width: 100%; text-align: center;">
        <tr>
            <td style="text-align: right;">
                <a href="RV_ContractServiceSchedule.aspx"><span class="auto-style1">RV_ContractServiceSchedule.aspx</span></a><asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px"
                   OnClick="btnQuit_Click" />
            </td>
        </tr>
        <tr>
            <td style="width:auto">
               <%-- <a href="RV_ContractServiceSchedule.aspx"><span class="auto-style1">RV_ContractServiceSchedule.aspx</span></a><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    Height="947px" ReportSourceID="CrystalReportSource1" Width="846px"
                    ToolPanelView="None" EnableParameterPrompt="False"
                    HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False"
                    HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True"
                    ToolPanelWidth="0px" HasCrystalLogo="false" />--%>
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                    <Report FileName="Reports\ContractServiceSchedule.rpt">
                    </Report>
                </CR:CrystalReportSource>
            </td>
        </tr>
    </table>
        </div>
</asp:Content>