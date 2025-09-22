<%@ Page Title="Service Action" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_ServiceAction.aspx.vb" Inherits="Master_ServiceAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      
<script type="text/javascript">

    var defaultText1 = "Search Here";
    function WaterMark1(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultText1;
        }
        if (txt.value == defaultText1 && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

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
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Service Action</h3>
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
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm()"/>
                     <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="true" />
                  
                  
                       </td>
                <td style="text-align: right">
                   
                  <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />
                </td>
                
            </tr>
                  <tr>
                   <td style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                   <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td>
              <td colspan="1" style="text-align:right">
                  <asp:TextBox ID="txtSearch" runat="server" Width="350px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True"></asp:TextBox>      
                        &nbsp;<asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="RESET SEARCH" />
               <br /></td>
            </tr>
                  <tr>
              <td colspan="2">
                  &nbsp;</td>
            </tr>
            <tr class="Centered">
                <td colspan="2">

                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" AllowPaging="True">
                         <Columns>
                               <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="150px" />
                <ItemStyle Width="150px" />
                </asp:CommandField>
                             <asp:BoundField DataField="ActionID" HeaderText="ActionID" ReadOnly="True" SortExpression="ActionID" >
                                  <ControlStyle Width="200px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                              <asp:BoundField DataField="ServiceAction" HeaderText="ServiceAction" ReadOnly="True" SortExpression="ServiceAction" >
                                  <ControlStyle Width="350px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="350px" HorizontalAlign="Left" />
                </asp:BoundField>
                              <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                 <EditItemTemplate>
                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
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
                     

                    <td class="CellFormatADM" style="width:10%">ActionID<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtActionID" runat="server" MaxLength="25" Height="16px" Width="90%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">ServiceAction<asp:Label ID="Label24" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtServiceAction" runat="server" MaxLength="100" Height="45px" Width="90%" TextMode="MultiLine"></asp:TextBox></td>
                     

                 </tr>
           <tr>
               <td colspan="2" style="text-align:right">   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   </td>
           </tr>
          </table>
       
    
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblserviceaction where rcno<>0"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>
</asp:Content>



