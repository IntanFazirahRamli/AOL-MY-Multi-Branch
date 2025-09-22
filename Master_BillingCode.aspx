<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_BillingCode.aspx.vb" Inherits="Master_BillingCode" Title="Billing Codes"  Culture="en-GB"%>

<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="JS/gridviewScroll.min.js"></script>
    <link href="CSS/GridviewScroll.css" rel="stylesheet" type="text/css" />
     <script lang="javascript" type="text/javascript">
         $(document).ready(function () {
             gridviewScroll();
         });

         function gridviewScroll() {
             gridView1 = $('#<%=GridView1.ClientID %>').gridviewScroll({
                 width: 690,
                 height: 300,
                 railcolor: "#F0F0F0",
                 barcolor: "#CDCDCD",
                 barhovercolor: "#606060",
                 bgcolor: "#F0F0F0",
                 freezesize: 1,
                 arrowsize: 30,
                 varrowtopimg: "Images/arrowvt.png",
                 varrowbottomimg: "Images/arrowvb.png",
                 harrowleftimg: "Images/arrowhl.png",
                 harrowrightimg: "Images/arrowhr.png",
                 headerrowcount: 1,
                 railsize: 16,
                 barsize: 8
             });
         }
	</script>
    --%>
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
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Billing Codes</h3>
     
        <asp:UpdatePanel ID="updpnlSave" runat="server"> <ContentTemplate>
        
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

                     <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" />

                       <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CH. STATUS" Width="92px" />

                       </td>
                <td style="text-align: right">
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" /></td>
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">
  <asp:GridView ID="GridView1" runat="server" CssClass="centered" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="ProductCode" Font-Size="15px" AllowSorting="True" AllowPaging="True" HorizontalAlign="Center">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="50px" />
                <ItemStyle Width="50px" />
                </asp:CommandField>

                        <asp:TemplateField HeaderText="InActive">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkINA" runat="server" Enabled="false" Checked='<%# Eval("Inactive")%>' />
                          </ItemTemplate>
                  </asp:TemplateField>

                 <asp:BoundField DataField="CompanyGroup" HeaderText="Company" SortExpression="CompanyGroup" >
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                <asp:BoundField DataField="ProductCode" HeaderText="Billing Code" ReadOnly="True" SortExpression="ProductCode">
                <ControlStyle Width="100px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                <ControlStyle Width="320px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="320px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="COACode" HeaderText="GL Code" />
                 <asp:BoundField DataField="COADescription" HeaderText="GL Description" >
                 <ControlStyle Width="350px" />
                 <ItemStyle HorizontalAlign="Left" Width="350px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="TaxType" HeaderText="Tax Type" />
                 <asp:BoundField DataField="TaxRate" HeaderText="Tax Rate" />
                 <asp:BoundField DataField="Price" SortExpression="Price" >
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
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
                    <td class="CellFormatADM">&nbsp;</td>
                    <td class="CellTextBoxADM">
                        <asp:DropDownList runat="server" AppendDataBoundItems="True" Height="20px" Width="45.5%" ID="ddlCompanyGrp" Visible="False"><asp:ListItem>--SELECT--</asp:ListItem>
                    </asp:DropDownList>

                    </td>
                     

                 </tr>
            <tr>
                     

                    <td class="CellFormatADM">InActive</td>
                     

                    <td class="CellTextBoxADM"> 
                        <asp:CheckBox ID="chkInactive" runat="server" CssClass="CellFormat" Text=" " Enabled="False" />
                    </td>
                     

                 </tr>
               <tr>
                   <td class="CellFormatADM">Billing Code<asp:Label ID="Label23" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" visible="true"></asp:Label>
                   </td>
                   <td class="CellTextBoxADM">
                       <asp:TextBox ID="txtProductCode" runat="server" Height="16px" MaxLength="50" Width="45%"></asp:TextBox>
                   </td>
               </tr>
              <tr>
                     

                    <td class="CellFormatADM">Description</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Height="16px" Width="45%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">GL Code<asp:Label ID="Label24" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" visible="true"></asp:Label>
                    </td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:DropDownList ID="ddlCOACode" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="45.5%" TabIndex="25" AutoPostBack="True">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">GL Description</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtCOADescription" runat="server" MaxLength="100" Height="16px" Width="45%" BackColor="#CCCCCC"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Tax Type<asp:Label ID="Label26" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" visible="true"></asp:Label>
                    </td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:DropDownList ID="ddlTaxType" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDataSource3" Height="25px" Width="45.5%" TabIndex="25" DataTextField="TaxType" DataValueField="TaxType" AutoPostBack="True">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Tax Rate </td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtTaxRate" runat="server" MaxLength="100" Height="16px" Width="45%" BackColor="#CCCCCC"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtPrice" runat="server" MaxLength="100" Height="16px" Width="45%" CssClass="dummybutton"></asp:TextBox></td>
                     

                 </tr>
           <tr>
               <td colspan="2" style="text-align:right">   
                   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
                   <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
               </td>
           </tr>
          </table>
    
          





                     <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="35%" Height="30%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
 
                         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
           
                
               <tr>
                             <td><br /><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label ID="lblStatusMessage1" runat="server"></asp:Label>
                         
                      </td>
                           </tr>
                            <tr>
                             <td><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="button" OnClientClick="currentdatetime();" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                              <asp:Button ID="Button1" runat="server" CssClass="dummybutton" />

            <asp:Button ID="btnConfirmNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

           <asp:ModalPopupExtender ID="mdlPopupStatus" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlStatus" TargetControlID="btndummy1" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
    <asp:Button ID="btndummy1" runat="server" CssClass="dummybutton" />

              </ContentTemplate></asp:UpdatePanel>
          <%-- end--%>

       </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblbillingproducts order by ProductCode"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
       
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tbltaxtype order by TaxType"></asp:SqlDataSource>
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
     <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
 
    </asp:Content>