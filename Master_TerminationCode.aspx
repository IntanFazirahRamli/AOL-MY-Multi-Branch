<%@ Page Title="Termination Code" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_TerminationCode.aspx.vb" Inherits="Master_TerminationCode" EnableEventValidation="false" Culture="en-GB" %>
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
 
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                  <asp:controlBundle name="ComboBox_Bundle"/>               
            </ControlBundles>
        </asp:ToolkitScriptManager>

    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Termination Code</h3>
    
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
                  <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code">
                     <ControlStyle Width="75px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="75px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                     <ControlStyle Width="470px" />
                  <ItemStyle Width="470px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>

                 <asp:TemplateField ><ItemTemplate> <asp:Button ID="btnStatus" runat="server" OnClick="btnStatus_Click" Text="STATUS" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top"   Width="80px"   />
                  </ItemTemplate></asp:TemplateField>

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
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtCode" runat="server" MaxLength="20" Height="16px" Width="20%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="auto-style2">Description<asp:Label ID="Label3" runat="server" visible="False" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtDescription" runat="server" MaxLength="200" Height="16px" Width="70%" TextMode="SingleLine"></asp:TextBox></td>
                     

                 </tr>

            <tr>
                     

                    <td class="auto-style2">Status </td>
                     

                    <td class="CellTextBoxADM"> 
        
                     <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="left" AllowSorting="True" AllowCustomPaging="True" EmptyDataText=" -">
            <Columns>
                
                  <asp:BoundField DataField="Status" HeaderText="Status" />
               
                <asp:BoundField DataField="Comments" HeaderText="Comments">
                     <ControlStyle Width="470px" />
                  <ItemStyle Width="470px" HorizontalAlign="Left" />
                </asp:BoundField>
              
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
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblterminationcode order by code "></asp:SqlDataSource>

       
        
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
                    </asp:SqlDataSource>

                         <%-- Start:View Edit History--%>
              
              
              <asp:Panel ID="pnlViewEditHistory" runat="server" BackColor="White" Width="55%" Height="55%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
        
                      
                <table style="width:100%;text-align:center;">
                           <tr>
                               <td style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="font-size:18px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> 
                        VIEW TERMINATION STATUS 
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="dummybutton" Visible="true" Width="20%"></asp:TextBox>
  </td> </tr>
                
           
                    <tr><td style="text-align:CENTER" colspan="2"><asp:Label ID="Label43" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="dummybutton" Visible="true" Width="20%"></asp:TextBox>
                        </td>
                    </tr>
                          
        </table>

                   <table style="width:100%;text-align:center;">
                                      
                           <tr>
                               <td style="width:30%;text-align:center;color:black;font-size:16px;font-weight:bold;font-family:'Calibri';"> <asp:Label ID="Label7" runat="server" Text="Code"></asp:Label></td>
                               <td style="width:70%">  <asp:Label ID="Label8" runat="server" Text=""></asp:Label></td>
                           </tr>
        </table>
              <div style="text-align: center; padding-left: 15px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewTerminationStatus" runat="server" DataSourceID="sqlDSViewEditHistory" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
              <asp:TemplateField HeaderText="Select"> 
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelectGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="3%" AutoPostBack="false"  Checked='<%#If(Eval("Selected").ToString() = "Y", True, False)%>'    CommandName="CHECK" >
                       </asp:CheckBox>
                   </ItemTemplate>
              </asp:TemplateField>            
        
          
             <asp:TemplateField HeaderText="Status"><ItemTemplate><asp:TextBox ID="txtStatusGVB" runat="server" Text='<%# Bind("Status")%>' Font-Size="12px" ReadOnly="true"  BorderStyle="None" Height="18px"  Width="15%"></asp:TextBox></ItemTemplate>
                 <HeaderStyle Width="12%" HorizontalAlign="Left" />
                 <ItemStyle Width="12%" />
                </asp:TemplateField>
             <asp:TemplateField HeaderText="Comments"><ItemTemplate><asp:TextBox ID="txtCommentsGVB" runat="server" Text='<%# Bind("Comments")%>' Font-Size="12px" ReadOnly="true"  BorderStyle="None" Height="18px"  Width="90%"></asp:TextBox></ItemTemplate>
                 <HeaderStyle Width="85%" HorizontalAlign="Left" />
                 <ItemStyle Width="85%" />
                </asp:TemplateField>
             <asp:TemplateField ><ItemTemplate><asp:TextBox ID="txtCodeGVB" runat="server" Text='<%# Bind("Code")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Visible="false"  Width="2%"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
            

           
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#0099CC" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

        </asp:GridView><br />
            
                  <asp:SqlDataSource ID="sqlDSViewEditHistory" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  </asp:SqlDataSource>

                   
              </div>

             <table style="width:100%;text-align:right;">
      
            <tr style="padding-top:40px; text-align:right; width:auto; " >

                 <td colspan="3" style="text-align:right">    
                    <asp:Button ID="btnSaveStatus" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>
                    <asp:Button ID="btnCancelStatus" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
                 
            </tr>
                <tr>
                    <td colspan="3"><br /></td>
                </tr>
              
             

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewEditHistory" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlViewEditHistory" TargetControlID="btnDummyViewEditHistory" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewEditHistory" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Edit History--%>
        
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

