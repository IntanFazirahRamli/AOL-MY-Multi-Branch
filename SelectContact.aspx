<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="SelectContact.aspx.vb" Inherits="SelectContact" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Select Contact Person</title>
    <style type="text/css">
        .button {
            margin-right:10px;
            border-radius:1px; 
            box-shadow: 2px 2px 1px #808080; 
            height:30px;
            width:100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>
      <div style="text-align:center;padding-left:20px;padding-bottom:5px;">      
          <h4>Contact Person</h4>        
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    
               <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" Width="380px" RowStyle-HorizontalAlign="Left" PageSize ="15">
                   <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                   <Columns>
                 
                         <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="50px" />
                <ItemStyle Width="50px" />
                </asp:CommandField>
                       <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" >
                       <ControlStyle Width="150px" />
                       <HeaderStyle Width="150px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>
                   </Columns>
                   <EditRowStyle BackColor="#999999" />
                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                   <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                   <SortedAscendingCellStyle BackColor="#E9E7E2" />
                   <SortedAscendingHeaderStyle BackColor="#506C8C" />
                   <SortedDescendingCellStyle BackColor="#FFFDF8" />
                   <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                 </asp:GridView>
                         </ContentTemplate>
            </asp:UpdatePanel>
                      </div>
            
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct id,name FROM tblcompanystaff WHERE (Rcno &lt;&gt; 0) order by name">
                       
            </asp:SqlDataSource>
            
     
                    <asp:TextBox ID="txtActive" runat="server" Visible="False"></asp:TextBox>
        
      </form>
</body>
</html>

