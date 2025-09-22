<%@ Page Title="Contract Code" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_ContractCode.aspx.vb" Inherits="Master_ContractCode" EnableEventValidation="false" Culture="en-GB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script type="text/javascript">


    function currentdatetime() {
        var currentTime = new Date();


        var dd = currentTime.getDate();
        var mm = currentTime.getMonth() + 1;
        var y = currentTime.getFullYear();

        var hh = currentTime.getHours();
        var MM = currentTime.getMinutes();
        var ss = currentTime.getSeconds();

        if (dd < 10) {
            dd = "0" + dd;
        }
        if (mm < 10)
            mm = "0" + mm;


        var strct = y + "/" + mm + "/" + dd + " " + hh + ":" + MM + ":" + ss;
        var endct = new Date(strct);
        document.getElementById("<%=txtCreatedOn.ClientID%>").value = dd + "/" + mm + "/" + y + " " + hh + ":" + MM + ":" + ss;
    }
</script>
    <style type="text/css">
        .auto-style1 {
            width: 36%;
        }
        .auto-style2 {
            font-size: 15px;
            font-weight: bold;
            font-family: 'Calibri';
            color: black;
            text-align: right; /*width:30%;*/ /*table-layout:fixed;
        overflow:hidden;*/;
            border-collapse: collapse;
            border-spacing: 0;
            line-height: 10px;
            width: 36%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Contract Code</h3>
    
       <table style="width:100%;text-align:center;">
            <%--<tr>
                <td colspan="2"><br /></td>
            </tr>--%>
            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
                 <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                       <asp:Label ID="txtMode" runat="server" Text="" CssClass="dummybutton"></asp:Label>
</td>
                     
            </tr>
            
            <tr>
              
                <td style="text-align:left;" class="auto-style1">
                  
                     <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
                
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" />
            
                       </td>
                <td style="text-align: right">
            
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />

                </td>
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">

                     <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True">
            <Columns>
                  <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False" >
                <ControlStyle Width="100px" />
                <ItemStyle Width="100px" />
                </asp:CommandField>
                 

                    <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkStatus" runat="server"  Enabled="false" Checked='<%#If(Eval("Status").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code">
                     <ControlStyle Width="75px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="75px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                     <ControlStyle Width="470px" />
                  <ItemStyle Width="470px" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="AgreementType" HeaderText="Agreement Type" />

                 <asp:TemplateField HeaderText="New Customer">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkNewCustomer" runat="server"  Enabled="false" Checked='<%#If(Eval("NewCustomer").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Existing Customer">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkExistingCustomer" runat="server"  Enabled="false" Checked='<%#If(Eval("ExistingCustomer").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="New Site">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkNewSite" runat="server"  Enabled="false" Checked='<%#If(Eval("NewSite").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Existing Site">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkExistingSite" runat="server"  Enabled="false" Checked='<%#If(Eval("ExistingSite").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                


                
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                 </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#e4e4e4" />
            <SortedAscendingHeaderStyle BackColor="#000066" />
            <SortedDescendingCellStyle BackColor="#e4e4e4" />
            <SortedDescendingHeaderStyle BackColor="#000066" />
        </asp:GridView>
    
                    </td>
                </tr>
          <tr>
              <td colspan="2"><br /></td>
          </tr>
            <tr>
                     

                    <td class="auto-style2">Status</td>
                     

                    <td class="CellTextBoxADM"> 
                        <asp:CheckBox ID="chkStatus" runat="server" />
                    </td>
                     

                 </tr>
            <tr>
                     

                    <td class="auto-style2">Code<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtCode" runat="server" MaxLength="5" Height="16px" Width="20%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="auto-style2">Description<asp:Label ID="Label3" runat="server" visible="False" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtDescription" runat="server" MaxLength="200" Height="16px" Width="70%" TextMode="SingleLine"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="auto-style2">Agreement Type</td>
                     

                    <td class="CellTextBoxADM"> 
                            <asp:DropDownList runat="server" AppendDataBoundItems="True" Height="20px" Width="70%" ID="ddlAgreementType" style="vertical-align:top;"><asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>NEW</asp:ListItem>
                                <asp:ListItem>ADDITION</asp:ListItem>
                                <asp:ListItem>REVISION</asp:ListItem>
                                <asp:ListItem>RENEWAL</asp:ListItem>
                                <asp:ListItem>EXTENSION</asp:ListItem>
</asp:DropDownList>

                            </td>
                     

                 </tr>
              <tr>
                     

                    <td class="auto-style2">New Customer</td>
                     

                    <td class="CellTextBoxADM">  
                        <asp:CheckBox ID="chkNewCustomer" runat="server" />
                    </td>
                     

                 </tr>
              <tr>
                     

                    <td class="auto-style2">Existing Customer</td>
                     

                    <td class="CellTextBoxADM">  
                        <asp:CheckBox ID="chkExistingCustomer" runat="server" />
                    </td>
                     

                 </tr>
              <tr>
                     

                    <td class="auto-style2">New Site</td>
                     

                    <td class="CellTextBoxADM">  
                        <asp:CheckBox ID="chkNewSite" runat="server" />
                    </td>
                     

                 </tr>
              <tr>
                     

                    <td class="auto-style2">Existing Site</td>
                     

                    <td class="CellTextBoxADM">  
                        <asp:CheckBox ID="chkExistingSite" runat="server" />
                    </td>
                     

                 </tr>
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblcontractcode order by code "></asp:SqlDataSource>

       
        
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Code, Description, Status, Rcno, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn FROM tblcontractcode ">
                       
                    </asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtCountryCode" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>
 

</asp:Content>

