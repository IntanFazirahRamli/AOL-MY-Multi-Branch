<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_ContractGroup.aspx.vb" Inherits="Master_ContractGroup" Title="Contract Group"  Culture="en-GB"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="JS/gridviewScroll.min.js"></script>
   <%-- <link href="CSS/GridviewScroll.css" rel="stylesheet" type="text/css" />--%>
     <script lang="javascript" type="text/javascript">
         //$(document).ready(function () {
         //    gridviewScroll();
         //});

        
	</script>
    
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
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Contract Group</h3>
    
        <table border="0" style="width:100%;text-align:left;">
            
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
           <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
           <asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
           <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="True" />
                   
                  
                   <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px"  />
                   
                  
                       </td>
             
                
            </tr>
            

              <tr>
                <td><br /></td>
            </tr>
                 <tr style="text-align:center;">
                <td style="width:100%;text-align:left">
                     <div style="padding-left:0%;">

       <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Auto" style="text-align:left;  margin-left:auto; margin-right:auto;" Visible="true" Width="1400px">
          
             <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" AllowSorting="True">
            <Columns>
                
                 <%--<asp:BoundField DataField="Rcno" HeaderText="Rcno" InsertVisible="False" ReadOnly="True" SortExpression="Rcno" Visible="False" />--%>
                
                 <%--   <asp:BoundField DataField="ContractValueAllowEdit" HeaderText="ContractValue AllowEdit" />--%><%--<asp:BoundField DataField="IncludeinPortfolio" HeaderText="Include in Portfolio" >--%><%-- </asp:BoundField>--%><%--<asp:BoundField DataField="ShowExpiryNotification" HeaderText="Show Expiry Notification" />--%><%-- <asp:BoundField DataField="AutoExpireContract" HeaderText="Auto-Expire Contract" />--%>
                
                 <asp:CommandField HeaderText="Select" ShowSelectButton="True" >
                 <ItemStyle Width="60px" />
                 </asp:CommandField>
                 <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" SortExpression="ContractGroup" >
                 <ItemStyle Wrap="False" Width="120px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="GroupDescription" HeaderText="Description" SortExpression="GroupDescription" >
                 <ItemStyle Wrap="False" Width="250px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" >

                 <ItemStyle Width="120px" />
                 </asp:BoundField>

                 <asp:BoundField DataField="DefaultServiceID" HeaderText="Def. Service ID" />

                 <asp:BoundField DataField="FixedContinuous" HeaderText="Duration" />
                <asp:BoundField DataField="ReportGroup" HeaderText="ReportGroup" />
                 <asp:BoundField DataField="TaxType" HeaderText="Tax Code" />
                 <asp:BoundField DataField="CommPct" HeaderText="Commission Pct">
                 <ItemStyle HorizontalAlign="Right" />
                 </asp:BoundField>


                 <asp:TemplateField HeaderText="Contract Value Allow Edit">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkContractValueAllowEdit" runat="server" Enabled="false" Checked='<%# Eval("ContractValueAllowEdit")%>' />
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                  </asp:TemplateField>

                      <asp:TemplateField HeaderText="Contract Value Allow Edit After Expiry">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkContractValueAllowEditAfterExpiry" runat="server" Enabled="false" Checked='<%# Eval("ContractValueAllowEditAfterExpiry")%>' />
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                  </asp:TemplateField>


                  <asp:TemplateField HeaderText="Include in Portfolio">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkIncludeinPortfolio" runat="server" Enabled="false" Checked='<%# Eval("IncludeinPortfolio")%>' />
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                  </asp:TemplateField>


                 <asp:BoundField DataField="PriceIncreaseLimit" HeaderText="Price Increase %" />
                 <asp:BoundField DataField="PriceDecreaseLimit" HeaderText="Price Decrease %" />

                
                

                 <asp:BoundField DataField="RevisionIncreaseLimit" HeaderText="Revision Increase %" />
                 <asp:BoundField DataField="RevisionDecreaseLimit" HeaderText="Revision Decrease %" />
                 <asp:TemplateField HeaderText="Show Expiry Notification">
                     <ItemTemplate>
                         <asp:CheckBox ID="chkShowExpiryNotification" runat="server" Checked='<%# Eval("ShowExpiryNotification")%>' Enabled="false" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Auto-Expire Contract">
                     <ItemTemplate>
                         <asp:CheckBox ID="chkAutoExpireContract" runat="server" Checked='<%# Eval("AutoExpireContract")%>' Enabled="false" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Add Back-Date Contract">
                     <ItemTemplate>
                         <asp:CheckBox ID="chkBackDateContract" runat="server" Checked='<%# Eval("BackDateContract")%>' Enabled="false" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Back-Date Contract Termination">
                     <ItemTemplate>
                         <asp:CheckBox ID="chkBackDateContractTermination" runat="server" Checked='<%# Eval("BackDateContractTermination")%>' Enabled="false" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Allow Extension">
                     <ItemTemplate>
                         <asp:CheckBox ID="chkAllowExtension" runat="server" Checked='<%# Eval("AllowExtension")%>' Enabled="false" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Revise Terminated Contract">
                     <ItemTemplate>
                         <asp:CheckBox ID="chkAllowReviseTerminatedContract" runat="server" Checked='<%# Eval("ReviseTerminatedContract")%>' Enabled="false" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
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
            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                      <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#e4e4e4" />
            <SortedAscendingHeaderStyle BackColor="#000066" />
            <SortedDescendingCellStyle BackColor="#e4e4e4" />
            <SortedDescendingHeaderStyle BackColor="#000066" />
        </asp:GridView>
           </asp:Panel>
    </div>
       </td>
            </tr>

              <tr>
                <td><br /></td>
            </tr>
            <tr style="text-align:center;">
                <td style="text-align:center;">
                
                    
                    
                        <table style="width:100%;text-align:center;">
                       
                        <tr><td style="width:40%"></td>
                            <td style="width:15%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Contract Group<asp:Label ID="Label23" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" visible="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtContractGroup" runat="server" MaxLength="50" Height="16px" Width="200px"></asp:TextBox></td>
                        </tr>
                         <tr style="padding-top:3px;"><td style="width:40%"></td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Description</td>
                             <td style="text-align:left;">
                                <asp:TextBox ID="txtGroupDescription" runat="server" MaxLength="100" Height="16px" Width="200px"></asp:TextBox></td>
                        </tr>
                         <tr style="padding-top:3px;"><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Category</td>
                             <td style="text-align:left;">
                                 <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="SqlDataSource2" CssClass="chzn-select" DataTextField="Category" DataValueField="Category" Width="206px" Height="22px">
                                 </asp:DropDownList>
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px;"><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Default Service ID</td>
                             <td style="text-align:left;">
                                 <asp:DropDownList ID="ddlDefServiceID" runat="server" CssClass="chzn-select" Width="206px" Height="22px" AppendDataBoundItems="True">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px;"><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Duration Type<asp:Label ID="Label24" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" visible="true"></asp:Label>
                             </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%;">
                                 <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ValidationGroup="ContTypeFixedContinuous" BorderStyle="None" Font-Size="15px" ForeColor="Black" Width="30%" ID="rbtFixedContinuous" onchange="CalculateDates()"><asp:ListItem Text="FIXED" Value="F"></asp:ListItem>
                                <asp:ListItem Text="CONTINUOUS" Value="C"></asp:ListItem>
                                </asp:RadioButtonList>
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px;"><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Report Group<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" visible="true"></asp:Label>
                             </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;">
                                  <asp:Dropdownlist ID="ddlReportGroup" runat="server" CssClass="chzn-select" Width="206px" Height="22px" AppendDataBoundItems="True">
                                       <asp:ListItem>--SELECT--</asp:ListItem>
                                </asp:Dropdownlist>
                             </td>
                        </tr>

                         <tr style="padding-top:3px;"><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Tax Code</td>
                             <td style="text-align:left;">
                                  <asp:Dropdownlist ID="ddlTaxCode" runat="server" CssClass="chzn-select" Width="206px" Height="22px" AppendDataBoundItems="True">
                                       <asp:ListItem>--SELECT--</asp:ListItem>
                                </asp:Dropdownlist>
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px;"><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Commision Percentage</td>
                             <td style="text-align:left;">
                                <asp:TextBox ID="txtCommPct" runat="server" MaxLength="100" Height="16px" Width="200px"></asp:TextBox>
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px;"><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Contract Value Allow Edit</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkContractValueAllowEdit" runat="server" />
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Allow to Edit After Expiry</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkContractValueAllowEditAfterExpiry" runat="server" />
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Include in Portfolio</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkIncludeInPortfolio" runat="server" AutoPostBack="True" />
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Show Expiry Notification</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkShowExpiryNotification" runat="server" />
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Auto Expire Contract</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkAutoExpireContract" runat="server" />
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Allow to Add Backdated Contract</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkAllowtoAddBackDatedContract" runat="server" />
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Allow to Backdate Contract Termination</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkAllowToBackdateContractTermination" runat="server" />
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Allow Extension</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkAllowExtension" runat="server" AutoPostBack="True" />
                             </td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Price Increase Limit </td>
                             <td style="text-align:left; color:black;font-family:'Calibri';font-size:15px;">
                                 <asp:TextBox ID="txtPriceIncreaseLimit" runat="server" Width="60" MaxLength="5"></asp:TextBox>
                                     &nbsp;%</td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Price Decrease Limit </td>
                             <td style="text-align:left; color:black;font-family:'Calibri';font-size:15px;">
                                 <asp:TextBox ID="txtPriceDecreaseLimit" runat="server" Width="60" MaxLength="6"></asp:TextBox>
                                     &nbsp;%</td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Revision Increase Limit </td>
                             <td style="text-align:left; color:black;font-family:'Calibri';font-size:15px;">
                                 <asp:TextBox ID="txtRevisionIncreaseLimit" runat="server" Width="60" MaxLength="6"></asp:TextBox>
                                     &nbsp;%</td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Revision Decrease Limit </td>
                             <td style="text-align:left; color:black;font-family:'Calibri';font-size:15px;">
                                 <asp:TextBox ID="txtRevisionDecreaseLimit" runat="server" Width="60" MaxLength="6"></asp:TextBox>
                                     &nbsp;%</td>
                        </tr>

                 
                         <tr style="padding-top:3px; "><td style="width:40%">&nbsp;</td>
                            <td style="width:10%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Allow Revise Terminated Contract</td>
                             <td style="text-align:left;">
                                 <asp:CheckBox ID="chkAllowReviseTerminatedContract" runat="server" AutoPostBack="True" />
                             </td>
                        </tr>

                 
           <tr>
               <td colspan="3" style="text-align:right">   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   </td>
           </tr>

                     

                    </table>




                </td>

            </tr>




           
       
            
            
            
        </table>
        </div>         
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT *  FROM tblContractGroup order by ContractGroup"></asp:SqlDataSource>

       
        
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT *  FROM tblContractGroupCategory WHERE (Rcno &lt;&gt; 0)"></asp:SqlDataSource>

       
        
                    <br />

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
   
 
    </asp:Content>