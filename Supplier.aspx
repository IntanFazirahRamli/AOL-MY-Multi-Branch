<%@ Page Title="Supplier" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Supplier.aspx.vb" Inherits="Supplier" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style type="text/css">
       .CellFormat1{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:15%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
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
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
      

                  <asp:ControlBundle Name="CalendarExtender_Bundle" />
       <%-- <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:controlBundle name="ListSearchExtender_Bundle"/>
        <asp:controlBundle name="TabContainer_Bundle"/>
        <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>
        <asp:controlBundle name="MaskedEditExtender_Bundle"/>--%>

   </ControlBundles>
    </asp:ToolkitScriptManager>     

    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Supplier</h3>
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
              
                <td style="width:30%;text-align:left;">
                  <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="true" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
                     <%--<asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="true" />--%>
                  
                  
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

                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="SupplierID" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True">
                         <Columns>
                               <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false" >
                <ControlStyle Width="150px" />
                <ItemStyle Width="150px" />
                </asp:CommandField>
                              <asp:TemplateField HeaderText="Active">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkINA" runat="server" Enabled="false" Checked='<%# IIf(Eval("Status").ToString().Equals("Active"), True, False)%>' />
                          </ItemTemplate>
                  </asp:TemplateField>
                                <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" SortExpression="SupplierID" ReadOnly="True">
                       <ControlStyle Width="5%"  />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="5%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                              <asp:BoundField DataField="Name" HeaderText="Supplier Name" SortExpression="Name">
                  <ControlStyle Width="26%" />
                  <HeaderStyle Width="250px" Wrap="False" />
                    <ItemStyle Width="26%" Wrap="False" HorizontalAlign="Left" />
                </asp:BoundField>
                                <asp:TemplateField HeaderText="Address" SortExpression="Address1">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox2" runat="server"  Text='<%# Eval("Address1") & " " & Eval("AddStreet") & " " & Eval("AddBuilding") & " " & Eval("AddCity") & " " & Eval("AddState") & " " & Eval("AddCountry")%>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label3" runat="server" Text='<%# Eval("Address1") & " " & Eval("AddStreet") & " " & Eval("AddBuilding") & " " & Eval("AddCity") & " " & Eval("AddState") & " " & Eval("AddCountry")%>'></asp:Label> 
                      </ItemTemplate>
                      <HeaderStyle Font-Bold="True" Width="250px" />
                      <ItemStyle Font-Names="Calibri" HorizontalAlign="Left" Width="250px" Wrap="False" />
                     </asp:TemplateField>

                  <%--<asp:BoundField DataField="AddPostal" HeaderText="Office Postal" SortExpression="AddPostal" >
                
                  <ItemStyle Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>--%>
                              <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
                       <ControlStyle Width="15%" />
                  <HeaderStyle Width="180px" Wrap="False" />
                       <ItemStyle Width="15%" Wrap="False" HorizontalAlign="Left" />
                </asp:BoundField>
                            <%--<asp:BoundField DataField="AddUnit" HeaderText="AddUnit" SortExpression="AddUnit" Visible="false" />--%>
                 <asp:BoundField DataField="AddBuilding" HeaderText="AddBuilding" SortExpression="AddBuilding" Visible="false" />
                 <asp:BoundField DataField="AddStreet" HeaderText="AddStreet" SortExpression="AddStreet" Visible="false" />
                
                  <asp:BoundField DataField="AddState" HeaderText="AddState" SortExpression="AddState" Visible="false" />
                  <asp:BoundField DataField="AddCity" HeaderText="AddCity" SortExpression="AddCity" Visible="false" />
                  <asp:BoundField DataField="AddCountry" HeaderText="AddCountry" SortExpression="AddCountry" Visible="false" />
                            <asp:BoundField DataField="Website" HeaderText="Website" SortExpression="Website" Visible="false" />
                
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
            <td class="CellFormat" style="width:38%">SupplierID<asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" Visible="False"></asp:Label></td>
            <td class="CellTextBox"><asp:TextBox ID="txtSupplierID" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="10" Width="20%" ReadOnly="TRUE"></asp:TextBox></td>

        </tr>
         <tr><td class="CellFormat">Supplier Name<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
<td class="CellTextBox"><asp:TextBox ID="txtSupplierName" runat="server" AutoPostBack="True" Height="16px" MaxLength="200" Width="62%"></asp:TextBox></td>
</tr>
         <tr>
            <td class="CellFormat">Status</td>
            <td class="CellTextBox">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="chzn-select" Width="20%">
                    <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                </asp:DropDownList></td>
</tr>
         <tr><td class="CellFormat">Registration No<%--<asp:Label ID="Label24" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>--%></td>
<td class="CellTextBox"><asp:TextBox ID="txtRegNo" runat="server" Height="16px" MaxLength="20" Width="20%"></asp:TextBox></td>
</tr>
         <tr><td class="CellFormat">GST Registration No</td>
<td class="CellTextBox"><asp:TextBox ID="txtGSTRegNo" runat="server" Height="16px" MaxLength="20" Width="20%"></asp:TextBox></td>
</tr>
         <tr><td class="CellFormat">Website</td>
<td class="CellTextBox"><asp:TextBox ID="txtWebsite" runat="server" Height="16px" MaxLength="50" Width="62%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat">Supplier Since</td>
<td class="CellTextBox"><asp:TextBox ID="txtStartDate" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtStartDate" TargetControlID="txtStartDate"></asp:CalendarExtender></td>
</tr>
<tr><td class="CellFormat">Comments</td>
<td class="CellTextBox" rowspan="2"><asp:TextBox ID="txtComments" runat="server" Font-Names="Calibri" Height="40px" MaxLength="2000" TextMode="MultiLine" Width="62%"></asp:TextBox></td>
</tr>
<tr><td></td>
</tr>

         <tr><td colspan="2">
           <%--  <asp:Panel ID="pnlOffAddrName" runat="server">
                 <table class="Centered" style="padding-top:5px;width:60%">
                     <tr><td><br /></td>
</tr>
<tr><td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Address</td>
</tr>
</table></asp:Panel>--%><%--<asp:Panel ID="pnlOffAddr" runat="server">--%>
            
             <table class="Centered" style="padding-top:5px;width:60%">
                  <tr><td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">Address</td>
</tr>
                 <tr><td class="CellFormat">Street Address1 </td>
<td class="CellTextBox" colspan="3"><asp:TextBox ID="txtAddress1" runat="server" Height="16px" MaxLength="100" Width="94%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat">Street Address2</td>
<td class="CellTextBox" colspan="3"><asp:TextBox ID="txtStreet" runat="server" Height="16px" MaxLength="100" Width="94%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat">&nbsp;Building &amp; Unit No. </td>
<td class="CellTextBox" colspan="3"><asp:TextBox ID="txtBuilding" runat="server" Height="16px" MaxLength="100" Width="94%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat">City </td>
<td class="CellTextBox"><asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="City" DataValueField="City" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td>
<td class="CellFormat1">State </td>
<td class="CellTextBox"><asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" Width="84%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td>
</tr>
<tr><td class="CellFormat">Country<%--<asp:Label ID="Label58" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>--%></td>
<td class="CellTextBox"><asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Country" DataValueField="Country" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td>
<td class="CellFormat1">Postal </td>
<td class="CellTextBox"><asp:TextBox ID="txtPostal" runat="server" Height="16px" MaxLength="20" Width="84%"></asp:TextBox></td>
</tr>
<tr><td colspan="4"><br /></td>
</tr>
<tr><td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">Contact Information</td>
</tr>

<tr><td class="CellFormat">Contact Person</td>
<td class="CellTextBox" colspan="3"><asp:TextBox ID="txtContactPerson" runat="server" Height="16px" MaxLength="50" Width="94%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat">Telephone</td>
<td class="CellTextBox"><asp:TextBox ID="txtTelephone" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
<td class="CellFormat1">Fax</td>
<td class="CellTextBox"><asp:TextBox ID="txtFax" runat="server" Height="16px" MaxLength="50" Width="84%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat">Telephone2</td>
<td class="CellTextBox"><asp:TextBox ID="txtTel2" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
<td class="CellFormat1">Mobile</td>
<td class="CellTextBox"><asp:TextBox ID="txtMobile" runat="server" Height="16px" MaxLength="50" Width="84%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat">Email</td>
<td class="CellTextBox" colspan="3"><asp:TextBox ID="txtEmail" runat="server" Height="16px" MaxLength="100" Width="94%"></asp:TextBox></td>
</tr>
</table><%--</asp:Panel>--%></td>
</tr>

          
           <tr>
               <td colspan="2" style="text-align:right">   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   </td>
           </tr>
          </table>
       
    
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblsupplier where rcno<>0"></asp:SqlDataSource>

       <asp:SqlDataSource ID="SqlDSState" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT State FROM tblstate WHERE (Rcno &lt;&gt; 0) ORDER BY State"></asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSCity" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSCountry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
       
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
    </div>
</asp:Content>





