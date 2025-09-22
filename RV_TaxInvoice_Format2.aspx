<%@ Page Language="vb" MasterPageFile="~/MasterPage.master"  AutoEventWireup="false" CodeFile="RV_TaxInvoice_Format2.aspx.vb" Inherits="RV_TaxInvoice_Format2" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
    <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;text-align:center;">Tax Invoice</h3><br />
<table style="width: 100%; text-align: center;">
        <tr>
            <td style="text-align: right;">
              <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px"
                   OnClick="btnQuit_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    Height="947px" ReportSourceID="CrystalReportSource1" Width="845px"
                    ToolPanelView="None" EnableParameterPrompt="False"
                    HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False"
                    HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True"
                    ToolPanelWidth="0px" HasCrystalLogo="false" />--%>
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                    <Report FileName="Reports\ARReports\TaxInvoice_Format2.rpt">
                    </Report>
                </CR:CrystalReportSource>
            </td>
        </tr>
    </table>
</asp:Content>