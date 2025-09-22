<%@ Page Title="Lock Service Record" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="LockServiceRecord.aspx.vb" Inherits="LockServiceRecord" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  

    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Lock Service &amp; Contract Record</h3>
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
                      <asp:Button ID="btnHistory" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="HISTORY" Width="100px" Visible="False" />
                  
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
 <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" AllowPaging="True">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="150px" />
                <ItemStyle Width="150px" />
                </asp:CommandField>
                <asp:BoundField DataField="SvcDateFrom" HeaderText="Service Date From" ReadOnly="True" SortExpression="SvcDateFrom" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>
                   <asp:BoundField DataField="SvcDateTo" HeaderText="Service Date To" ReadOnly="True" SortExpression="SvcDateTo" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractLock" HeaderText="Contract Lock" SortExpression="ContractLock">
                 <HeaderStyle Font-Size="12pt" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="SvcLock" HeaderText="Service Lock" SortExpression="SvcLock">
                 <HeaderStyle Font-Size="12pt" />
                 <ItemStyle HorizontalAlign="Center" Wrap="False" />
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
                                                  <asp:BoundField DataField="CreatedOn"  HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn"  HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                 <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" SortExpression="CountryCode" Visible="False" />
                 
                <asp:TemplateField ><ItemTemplate> <asp:Button ID="btnEditHistory" runat="server"  Text="History" OnClick="btnEditHistory_Click" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top"   Width="80px"   />
</ItemTemplate></asp:TemplateField>
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
                      <td class="CellFormatADM">ServiceDate<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="20%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;<b>TO</b>&nbsp; <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="20%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                   
               
             
                  </tr>
            <tr>
                     

                    <td class="CellFormatADM">Contract Lock</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:RadioButtonList ID="rdbContractLock" runat="server" Font-Bold="True" RepeatDirection="Horizontal" Width="164px" >
                            <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                     

                 </tr>
           
            <tr>
                     

                    <td class="CellFormatADM">Service Lock</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:RadioButtonList ID="rdbServiceLock" runat="server" Font-Bold="True" RepeatDirection="Horizontal" Width="164px" >
                            <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                     

                 </tr>
           
            <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM">
                        &nbsp;</td>
                     

                 </tr>
           
           <tr>
               <td colspan="2" style="text-align:right">  <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                  
           </tr>
          </table>
      

                               <%-- Start:View Edit History--%>
              
              
              <asp:Panel ID="pnlViewEditHistory" runat="server" BackColor="White" Width="800px" Height="95%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">View Record History</h4> 
  </td> <td>
                               <asp:TextBox ID="TextBox5" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label43" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewEditHistory" runat="server" DataSourceID="sqlDSViewEditHistory" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%" PageSize="20"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
                        
             
                <asp:BoundField DataField="SvcDateFrom" HeaderText="Service Date From" ReadOnly="True" SortExpression="SvcDateFrom" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>
                   <asp:BoundField DataField="SvcDateTo" HeaderText="Service Date To" ReadOnly="True" SortExpression="SvcDateTo" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractLock" HeaderText="Contract Lock" SortExpression="ContractLock">
                 <HeaderStyle Font-Size="12pt" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="SvcLock" HeaderText="Service Lock" SortExpression="SvcLock">
                 <HeaderStyle Font-Size="12pt" />
                 <ItemStyle HorizontalAlign="Center" Wrap="False" />
                 </asp:BoundField>
                
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn"  HeaderText="EditedOn" SortExpression="LastModifiedOnLog" />
                 <asp:BoundField DataField="TriggerType" HeaderText="Action" />
                 
             
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

        </asp:GridView><br />
            
                  <asp:SqlDataSource ID="sqlDSViewEditHistory" runat="server" ConnectionString="<%$ ConnectionStrings:sitadata_RecordLogConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadata_RecordLogConnectionString.ProviderName %>">
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:20px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="Button8" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
              
             

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewEditHistory" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlViewEditHistory" TargetControlID="btnDummyViewEditHistory" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewEditHistory" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Edit History--%>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tbllockservicerecord order by svcdatefrom desc"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtCountryCode" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtWS" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
              <asp:TextBox ID="txtLogDocNo" runat="server" Visible="False"></asp:TextBox> 
    </div>
 
</asp:Content>


