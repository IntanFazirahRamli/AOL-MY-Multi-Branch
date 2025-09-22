<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ServiceReport.aspx.vb" Inherits="ServiceReport" EnableEventValidation="false" %>
<object id = "Object1" name="Pdf2" 
type="application/pdf" WIDTH="1" HEIGHT="1" >
            <PARAM NAME='SRC' VALUE='<%= SReportFileName %>'>
</object>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
    <div>
        <asp:Panel ID="Panel1" runat="server">
            <%--<div>--%> 
             <asp:Table runat="server" Width="100%">
            <asp:TableRow Width="100%">
                <asp:tablecell Width="100%">
                    <pre style="font-family:Arial;font-size:8px; "><b>ServiceDate  : </b><asp:Label ID="lblServiceDate" runat="server" Text="" Font-Bold="False"></asp:Label><b>   Sch Type : </b><asp:Label ID="lblSchType" runat="server" Text="" Font-Bold="False" Width="80px"></asp:Label> <b>   Time In : </b><asp:Label ID="lblTimeIn" runat="server" Text="" Font-Bold="False" Width="80px"></asp:Label><b>   Time Out : </b><asp:Label ID="lblTimeOut" runat="server" Text="" Font-Bold="False"></asp:Label><%--</pre></div>--%>
<b>Customer ID  : </b><asp:Label ID="lblCustomerID" runat="server" Text=""></asp:Label>
<b>Customer Name  : </b><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
<b>Service Address   : </b><asp:Label ID="lblSvcAddress" runat="server" Text=""></asp:Label>   </pre>                
                </asp:tablecell>
                <asp:TableCell HorizontalAlign="RIGHT" VerticalAlign="Middle">
                    <pre style="font-family:Arial;font-size:8px;text-align:right; "><b>SERVICE REPORT</b>
                     <asp:Label ID="lblRecordNo" runat="server" Text=""></asp:Label></pre>
                </asp:TableCell>
                
            </asp:TableRow>
      </asp:Table>
         <asp:Label ID="lblAttention" runat="server" Text=""></asp:Label> 
             <asp:Label ID="lblContractNo" runat="server" Text=""></asp:Label> 
            <asp:Label ID="lblTargetPest" runat="server" Text=""></asp:Label> 
             <asp:Label ID="lblReason" runat="server" Text=""></asp:Label> 
             <asp:Label ID="lblAction" runat="server" Text=""></asp:Label>
             <asp:Label ID="lblClientSignName" runat="server" Text=""></asp:Label>
               <asp:Label ID="lblServicedBy" runat="server" Text=""></asp:Label> 
    </asp:Panel>
       
    </div>
    </form>
</body>
</html>
