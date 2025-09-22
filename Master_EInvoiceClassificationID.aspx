<%@ Page Title="E-Invoice Classification ID" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_EInvoiceClassificationID.aspx.vb" Inherits="Master_EInvoiceClassificationID"  enableeventvalidation="false" Culture="en-GB"%>
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
        
   </ControlBundles>
    </asp:ToolkitScriptManager>     
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">E-Invoice Classification ID</h3>
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
                     <asp:Button ID="btnImport" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0" visible="true" Font-Bold="True" Text="IMPORT FROM FILE" Width="200px" />
                  
                  
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

                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="ClassificationCode" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True">
                         <Columns>
                               <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" visible="false">
                <ControlStyle Width="150px" />
                <ItemStyle Width="150px" />
                </asp:CommandField>
                             <asp:BoundField DataField="ClassificationCode" HeaderText="ClassificationCode" ReadOnly="True" SortExpression="ClassificationCode" >
                                  <ControlStyle Width="200px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                              <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" >
                                  <ControlStyle Width="350px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="350px" HorizontalAlign="Left" />
                </asp:BoundField>
                           <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="ModifiedBy" HeaderText="EditedBy" SortExpression="ModifiedBy" />
                                                  <asp:BoundField DataField="ModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="ModifiedOn" />
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
                     
               
                               <td class="CellFormatADM">ClassificationID<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtClassificationID" runat="server" MaxLength="25" Height="16px" Width="30%"></asp:TextBox></td>
                     
                   
                 </tr>
              <tr>
                                            
                    <td class="CellFormatADM">Description</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Height="16px" Width="70%"></asp:TextBox></td>
                     
                 </tr>
           <tr>
               <td colspan="2" style="text-align:right">   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   </td>
           </tr>
          </table>
       
         <asp:Panel ID="pnlImportMsg" runat="server" BackColor="White" Width="450px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:center;padding-left:2px;">
            
                  <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label45" runat="server" Text="Import"></asp:Label>                          
    
                      </td>
                           </tr>
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
              
                 <tr>
                      <td class="CellFormat" style="text-align:left;padding-left:110px; margin-left:auto; margin-right:auto;"">
           
                          <asp:RadioButtonList ID="rdbImportOptions" runat="server">
                              <asp:ListItem>Import from JSON File</asp:ListItem>
                              <asp:ListItem>Import from Excel</asp:ListItem>
                          </asp:RadioButtonList>
                                         &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp; <asp:FileUpload ID="FileUpload1" runat="server" CssClass="Centered" Width="100%" />
                                                    <br />
                          <%--<asp:Label ID="Label2" runat="server" Text="Please update the customer contact details to ensure that there will be no errors."></asp:Label><br /><br />--%>
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkImportMsg" runat="server" CssClass="roundbutton" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelImportMsg" runat="server"  CssClass="roundbutton" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupImportMsg" runat="server" CancelControlID="btnCancelImportMsg" PopupControlID="pnlImportMsg" TargetControlID="btndummImportMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummImportMsg" runat="server" CssClass="dummybutton" />

       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblEInvoiceMalaysiaClassificationID where rcno<>0"></asp:SqlDataSource>

         <asp:TextBox ID="txtResult" runat="server" ForeColor ="black" /><br />
        <asp:TextBox ID="txtCount" runat="server" ForeColor ="black" /><br />
      
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtModifiedOn" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtFailureCount" runat="server" CssClass="dummybutton"></asp:TextBox>
<asp:TextBox ID="txtSuccessCount" runat="server" CssClass="dummybutton"></asp:TextBox>
<asp:TextBox ID="txtFailureString" runat="server" CssClass="dummybutton"></asp:TextBox>
         <asp:TextBox ID="txtWorkBookName" runat="server" CssClass="dummybutton"></asp:TextBox>
    </div>
</asp:Content>

