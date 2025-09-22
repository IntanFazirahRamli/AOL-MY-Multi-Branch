<%@ Page Title="Country Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_Country.aspx.vb" Inherits="Master_Country"  Culture="en-GB"%>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Country</h3>
    <br />
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
              
                <td style="width:40%;text-align:left;">
                 <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="true" />

                    <asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
                      <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="true" />
                  
                       </td>
                <td style="text-align: right">
                        &nbsp;<asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" /></td>
                
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">
 <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Country" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="150px" />
                <ItemStyle Width="150px" />
                </asp:CommandField>
                <asp:BoundField DataField="Country" HeaderText="Country" ReadOnly="True" SortExpression="Country">
               <ControlStyle Width="200px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Citizenship" HeaderText="Citizenship" SortExpression="Citizenship">
                 <ControlStyle Width="200px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" >
                     <ControlStyle Width="350px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="350px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                   <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" SortExpression="CountryCode" Visible="False" />
                 <asp:BoundField DataField="WS" HeaderText="WS" SortExpression="WS" Visible="False" />
                
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
                     

                    <td class="CellFormatADM">Country<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtCountry" runat="server" MaxLength="50" Height="16px" Width="30%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Citizenship</td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtCitizenship" runat="server" MaxLength="50" Height="16px" Width="30%"></asp:TextBox></td>
                     

                 </tr>
            <tr>
                     

                    <td class="CellFormatADM">Comments</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtComments" runat="server" MaxLength="100" Height="16px" Width="60%"></asp:TextBox></td>
                     

                 </tr>
           <tr>
               <td colspan="2" style="text-align:right">  <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                  
           </tr>
          </table>
      
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblcountry where rcno<>0"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtCountryCode" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtWS" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>
 
</asp:Content>

