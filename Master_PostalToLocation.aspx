<%@ Page Title="Postal to Location Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_PostalToLocation.aspx.vb" Inherits="Master_PostalToLocation"  Culture="en-GB"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 23%;
        }
        .auto-style2 {
            width: 116px;
        }
        .auto-style3 {
        }
        .auto-style4 {
            width: 136px;
        }
        .auto-style5 {
            width: 164px;
        }
        .auto-style6 {
            width: 5%;
        }
    </style>
      
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
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Postal To Location</h3>
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

                      <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" Width="740px" AllowPaging="True">
            <Columns>
                  <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="50px" />
                </asp:CommandField>
                <asp:BoundField DataField="PostalBeginWith" HeaderText="Postal Begin With" SortExpression="PostalBeginWith">
                     <ControlStyle Width="100px" />
                <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Zone" HeaderText="Zone" SortExpression="Zone">
                     <ControlStyle Width="200px" />
                <ItemStyle Width="75px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="District" HeaderText="District" SortExpression="District" >
                  <ItemStyle Width="75px" />
                </asp:BoundField>
                 <asp:BoundField DataField="LocateGRP" HeaderText="Locate GRP" SortExpression="LocateGRP" />
                
                  <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks">
                  <ItemStyle Width="200px" />
                  </asp:BoundField>
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
                     

                    <td class="CellFormatADM">Postal begin with<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtPostalBeginWith" runat="server" MaxLength="20" Height="16px" Width="30%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Zone</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtZone" runat="server" MaxLength="25" Height="16px" Width="30%"></asp:TextBox></td>
                     

                 </tr>
          <tr>
                     

                    <td class="CellFormatADM">District</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtDistrict" runat="server" MaxLength="35" Height="16px" Width="30%"></asp:TextBox></td>
                     

                 </tr>
          <tr>
                     

                    <td class="CellFormatADM">LocateGRP</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtLocateGRP" runat="server" MaxLength="25" Height="16px" Width="30%"></asp:TextBox></td>
                     

                 </tr>
          <tr>
                     

                    <td class="CellFormatADM">Remarks</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtRemarks" runat="server" MaxLength="25" Height="16px" Width="30%"></asp:TextBox></td>
                     

                 </tr>
      
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
    
     
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblpostaltolocation  where rcno&lt;&gt;0 order by postalbeginwith"></asp:SqlDataSource>

       
        
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT PostalBeginWith,Zone,District,LocateGRP,Remarks, Rcno, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn FROM tblpostaltolocation WHERE (Rcno &lt;&gt; 0) order by postalbeginwith">
                       
                    </asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtCountryCode" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>
    </div>
 


</asp:Content>

