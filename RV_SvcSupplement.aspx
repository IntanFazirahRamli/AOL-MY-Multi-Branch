<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RV_SvcSupplement.aspx.vb" Inherits="WebApplication1.RV_SvcSupplement"  MasterPageFile="~/Site.Master" %>--%>


<%@ Page Language="vb" AutoEventWireup="false" CodeFile="RV_SvcSupplement.aspx.vb" Inherits="RV_SvcSupplement" 
    MasterPageFile="~/MasterPage.Master" Title="Service Supplement Report"%>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
      <table style="width:100%">
           <tr>
                <td colspan="2" style="width:100%;text-align:RIGHT;"> 
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Visible="true" Text="QUIT" Width="90px" Height="30px" />
            </td>
            </tr>
 <tr>
     <td colspan="2" style="width:100%;text-align:center;">
     
       <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
           <Report FileName="Reports\ServiceSupplementReport.rpt">
           </Report>
       
    </CR:CrystalReportSource>
         </td>
 </tr>
     
             </table>

       <asp:Label ID="lblRecordNo" runat="server" Text="" Visible="false"></asp:Label>
          <asp:TextBox ID="txtQuery" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtSvcRcno" runat="server" Visible="False"></asp:TextBox>
</asp:Content>
