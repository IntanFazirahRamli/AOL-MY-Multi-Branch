<%@ Page Title="Service Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_Service.aspx.vb" Inherits="Master_Service"  Culture="en-GB" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
      
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
    
    <script type="text/javascript">
        function DoValidation(parameter) {
            currentdatetime()

            var valid = true;
            var str = document.getElementById("<%=txtEstVal.ClientID%>").value;
            var str1 = document.getElementById("<%=txtCostVal.ClientID%>").value;

            if (str != "" && isNaN(str)) {
                alert("Enter Only Numbers for Estimate Value");
                valid = false;
            }
            if (str != "" && isNaN(str1)) {
                alert("Enter Only Numbers for Cost Value");
                valid = false;
            }
            return valid;
        };
    </script>


    <div style="text-align: center">
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">Service Master</h3>
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
     <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" 
                        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="ProductID" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True">
                        <Columns>
                            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                                <ControlStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="ProductID" HeaderText="Service ID" ReadOnly="True" SortExpression="ProductID">
                                <ControlStyle Width="160px" />
                                <ItemStyle Width="160px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductDesc" HeaderText="Description" SortExpression="ProductDesc">
                                <ControlStyle Width="350px" />
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="EstimateValue" HeaderText="Estimate Value" SortExpression="EstimateValue" DataFormatString="{0:0.00}">
                                <ControlStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CostValue" HeaderText="Cost Value" SortExpression="CostValue" DataFormatString="{0:0.00}">
                                <ControlStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action">
                                <ControlStyle Width="230px" CssClass="dummybutton" />
                                <HeaderStyle CssClass="dummybutton" />
                                <ItemStyle Width="230px" HorizontalAlign="Left" CssClass="dummybutton" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Target" HeaderText="Target" SortExpression="Target">
                                <ControlStyle Width="230px" CssClass="dummybutton" />
                                <HeaderStyle CssClass="dummybutton" />
                                <ItemStyle Width="230px" HorizontalAlign="Left" CssClass="dummybutton" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WithArea" HeaderText="With Area">
                            <ControlStyle Width="8%" />
                            <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SalesCommissionPerc" HeaderText="Sales Commission %" />
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
                    <td class="CellFormatADM">Service ID<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                    <td class="CellTextBoxADM">  <asp:TextBox ID="txtProdID" runat="server" MaxLength="50" Height="16px" Width="30%" AutoCompleteType="Disabled"></asp:TextBox></td>
      </tr>
              <tr>
                    <td class="CellFormatADM">Description<asp:Label ID="Label2" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                   <td class="CellTextBoxADM">  <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox></td>
                  </tr>
               <tr>
                    <td class="CellFormatADM">Estimated Value</td>
                   <td class="CellTextBoxADM">   <asp:TextBox ID="txtEstVal" runat="server" MaxLength="100" Height="16px" Width="30%" AutoCompleteType="Disabled"></asp:TextBox></td>
         </tr>
               <tr>
                    <td class="CellFormatADM">Cost Value</td>
                   <td class="CellTextBoxADM">    <asp:TextBox ID="txtCostVal" runat="server" MaxLength="100" Height="16px" Width="30%" AutoCompleteType="Disabled"></asp:TextBox></td>
                   </tr>
               <tr>
                    <td class="CellFormatADM">Sales Commission (%)</td>
                   <td class="CellTextBoxADM">    <asp:TextBox ID="txtSalesCommissionPerc" runat="server" MaxLength="100" Height="16px" Width="30%" AutoCompleteType="Disabled"></asp:TextBox></td>
                   </tr>
               <tr>
                    <td class="CellFormatADM">&nbsp;</td>
                   <td class="CellTextBoxADM">       
                       <asp:CheckBox ID="chkWithArea" runat="server" Visible="False" />
                    </td>
                   </tr>
               <tr>
                    <td class="CellFormatADM">&nbsp;</td>
                   <td class="CellTextBoxADM">  <asp:TextBox ID="txtAction" runat="server" MaxLength="100" Height="16px" Width="80%" AutoCompleteType="Disabled" Visible="False"></asp:TextBox></td>
    </tr>
               <tr>
                    <td class="CellFormatADM">&nbsp;</td>
                   <td class="CellTextBoxADM">   <asp:TextBox ID="txtTarget" runat="server" MaxLength="100" Height="16px" Width="80%" AutoCompleteType="Disabled" Visible="False"></asp:TextBox></td>
                      </tr>
               <tr>
                    <td class="CellFormatADM">&nbsp;</td>
                   <td class="CellTextBoxADM">   
                       &nbsp;</td>
                      </tr>
           <tr>
               <td colspan="2" style="text-align:right">  <asp:Button ID="btnSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="SAVE" Width="100px"
                         OnClientClick="return DoValidation()" />
<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>

     

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" 
            SelectCommand="SELECT ProductID, ProductDesc, EstimateValue, CostValue, Action, Target, WithArea, SalesCommissionPerc, Rcno, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn FROM tblProduct WHERE (Rcno &lt;&gt; 0)"></asp:SqlDataSource>

        <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>

</asp:Content>


