<%@ Page Language="vb" AutoEventWireup="false" CodeFile="RV_ReceiptByClient_Summary.aspx.vb" Inherits="RV_ReceiptByClient_Summary"
    MasterPageFile="~/MasterPage.Master" Title="Receipt Summary - By Client"%>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="text-align:center">
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Receipt Summary - By Client</h3>
    <br />
    <table style="width: 100%; text-align: center;">
        <tr>
            <td style="text-align: right;">
              <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Back" Width="100px"
                   OnClick="btnQuit_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    Height="947px"  Width="845px"
                    ToolPanelView="None" EnableParameterPrompt="False"
                    HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False"
                    HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True"
                    ToolPanelWidth="0px" HasCrystalLogo="false" />
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                    <Report FileName="Reports\ARReports\ReceiptReports\ReceiptByClient_Summary.rpt">
                    </Report>
                </CR:CrystalReportSource>
            </td>
        </tr>
    </table>
         </div>
</asp:Content>