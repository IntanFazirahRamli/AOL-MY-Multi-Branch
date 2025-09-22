<%@ Page Title="Branch" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_Location.aspx.vb" Inherits="Master_Location"  Culture="en-GB"%>

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
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Branch/Location</h3>
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
              
                <td style="width:50%;text-align:left;">
                  <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="true" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
                     <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
                  
                  
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

                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="locationID" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True">
                         <Columns>
                               <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="150px" />
                <ItemStyle Width="150px" />
                </asp:CommandField>
                             <asp:BoundField DataField="LocationID" HeaderText="LocationID" ReadOnly="True" SortExpression="LocationID" >
                                  <ControlStyle Width="200px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                              <asp:BoundField DataField="Location" HeaderText="Description" ReadOnly="True" SortExpression="Location" >
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
                
                  <td colspan="2">
                      <table style="width:100%;text-align:left;">
                            <tr>
                     

                    <td class="CellFormatADM" style="width:40%">Branch/Location ID<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtLocationID" runat="server" MaxLength="15" Height="16px" Width="30%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Description</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtDescription" runat="server" MaxLength="50" Height="16px" Width="70%"></asp:TextBox></td>
                     

                 </tr>
                      </table><br />
                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:60%; border-radius: 25px;padding: 2px; height:60px; background-color: #FFF;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">BRANCH INFORMATION</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Branch Name</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtCompanyName" runat="server" width="350px" MaxLength="500"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Office Address</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtOfficeAddress" runat="server" width="350px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Business Entity Name</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBusinessEntityName" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                       </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Business Registration Number</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBusinessRegNumber" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">GST Number</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtGSTNumber" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Telephone Number</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtTelNumber" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Fax Number</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtFaxNumber" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Website</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtWebsite" runat="server" width="350px" MaxLength="200"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Email</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtEmail" runat="server" width="350px" MaxLength="200"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">InvoiceEmail</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtInvoiceEmail" runat="server" width="350px" MaxLength="200"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Mobile</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtMobile" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Bank Name</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBankName" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                        </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Bank Code</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBankCode" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                        </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Branch Code</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBranchCode" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                        </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Account Name</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtAccountName" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                        </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">Bank Account No.</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBankAccountNo" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                        </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:30px;width:30%;text-align:RIGHT">SWIFT Code</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSWIFTCode" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                        </td></tr>
                                  

                              </table></td></tr>
                      </table><br />

                       
  
                  </td>
                 </tr>
           <tr>
               <td colspan="2" style="text-align:right">   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   </td>
           </tr>
          </table>
       
    
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Rcno, location, locationid, CreatedBY, CreatedOn, LastModifiedBy, LastModifiedOn FROM tbllocation"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>
</asp:Content>

