<%@ Page Title="Outstanding Invoice Statement" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RV_OutstandingInvoiceStatement.aspx.vb" Inherits="RV_OutstandingInvoiceStatement" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="text-align:center">
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Outstanding Invoice Statement</h3>
    <br />
    <table style="width: 100%; text-align: center;">
       <%-- <tr>
            <td style="text-align: right;">
              <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Back" Width="100px"
                   OnClick="btnQuit_Click" />
            </td>
        </tr>--%>
        <tr>
            <td>
              <%--  <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    Height="947px"  Width="845px"
                    ToolPanelView="None" EnableParameterPrompt="False"
                    HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False"
                    HasZoomFactorList="False" ReuseParameterValuesOnRefresh="True"
                    ToolPanelWidth="0px" HasCrystalLogo="false" />--%>
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                    <Report FileName="Reports\ARReports\TransactionSummary.rpt">
                    </Report>
                </CR:CrystalReportSource>
                <asp:TextBox ID="txtAccountID" runat="server" CssClass="dummybutton"></asp:TextBox>
                 <asp:TextBox ID="txtAccountType" runat="server" CssClass="dummybutton"></asp:TextBox>
                 <asp:TextBox ID="txtCutOffDate" runat="server" CssClass="dummybutton"></asp:TextBox>
                  <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
     <asp:TextBox ID="txtQueryRecv" runat="server" CssClass="dummybutton" ></asp:TextBox>
                   <asp:TextBox ID="txtQueryRecv1" runat="server" CssClass="dummybutton" ></asp:TextBox>
   <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="dummybutton"></asp:TextBox>
            </td>
        </tr>
    </table>
         </div>
</asp:Content>

