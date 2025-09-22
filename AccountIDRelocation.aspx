<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="AccountIDRelocation.aspx.vb" Inherits="AccountIDRelocation" Title="AccountID Relocation"  Culture="en-GB" EnableEventValidation="false" ValidateRequest="false"  %>


<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
            .loading-panel {
                background: rgba(0, 0, 0, 0.2) none repeat scroll 0 0;
                /*background: #fff none repeat scroll 0 0;*/
                position: relative;
                width: 100%;
            }

            .loading-container {
                /*background: rgba(49, 133, 156, 0.4) none repeat scroll 0 0;*/
                background: rgba(192,192,192,0.3) none repeat scroll 0 0;
                color: #fff;
                font-size: 90px;
                height: 100%;
                left: 0;
                padding-top: 15%;
                position: fixed;
                text-align: center;
                top: 0;
                width: 100%;
                z-index: 999999;
            }
        </style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="updPanelCompany">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>

    
     <asp:UpdatePanel ID="updPanelCompany" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                  <asp:controlBundle name="ComboBox_Bundle"/>   
               <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>          
            </ControlBundles>
        </asp:ToolkitScriptManager>
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
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Relocation Of Account ID</h3>
   <table border="0" style="width:100%;text-align:center;">
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
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="BACK" Width="100px" /></td>
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
            <tr>
                     

                    <td class="CellFormatADM">Existing Contact Type</td>
                     

                    <td class="CellTextBoxADM"> 
                                    <asp:TextBox ID="txtContactTypeRelocate" runat="server" Height="16px" MaxLength="50" Width="50%" Enabled="False"></asp:TextBox>
                      </td>
                     

                 </tr>
            <tr>
                     

                    <td class="CellFormatADM">Existing Service Name</td>
                     

                    <td class="CellTextBoxADM"> 
                                    <asp:TextBox ID="txtServiceNameRelocate" runat="server" Height="16px" MaxLength="50" Width="50%" Enabled="False"></asp:TextBox>
                      </td>
                     

                 </tr>
            <tr>
                     

                    <td class="CellFormatADM">Existing Location ID</td>
                     

                    <td class="CellTextBoxADM"> 
                                    <asp:TextBox ID="txtLocationIDRelocate" runat="server" Height="16px" MaxLength="50" Width="50%" Enabled="False"></asp:TextBox>
                      </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM">
                                  &nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">New Contact Type </td>
                     

                    <td class="CellTextBoxADM">
             <asp:DropDownList ID="txtAccountType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="50%" AutoPostBack="True" TabIndex="22"><asp:ListItem>--SELECT--</asp:ListItem><asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem><asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem></asp:DropDownList>
                              </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Existing Account ID to Relocate</td>
                     

                    <td class="CellTextBoxADM">
                                  <asp:TextBox ID="txtAccountIDRelocate" runat="server" Height="16px" MaxLength="25" Width="50%" AutoPostBack="True"></asp:TextBox>
                              &nbsp;<asp:ImageButton ID="btnClient1" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                              </td>
                     

                 </tr>
              <tr>
                    <td class="CellFormatADM">&nbsp;Billing Name</td>
                    <td class="CellTextBoxADM">
                                  <asp:TextBox ID="txtBillingNameRelocate" runat="server" Height="16px" MaxLength="25" Width="50%" Enabled="False"></asp:TextBox>



                    </td>
                     

                 </tr>
              <tr>
                    <td class="CellFormatADM">New Location ID</td>
                    <td class="CellTextBoxADM">
                                 <asp:TextBox ID="txtLocationIDRelocateNew" runat="server" Height="16px" MaxLength="50" Width="50%" Enabled="False"></asp:TextBox>



                    </td>
                     

                 </tr>
              <tr>
                    <td class="CellFormatADM">&nbsp;</td>
                    <td class="CellTextBoxADM">
                        &nbsp;</td>
                     

                 </tr>
              <tr>
                    <td class="CellFormatADM">Contracts:</td>
                    <td class="CellTextBoxADM">
                        &nbsp;</td>
                     

                 </tr>
              <tr>
                    <td class="CellFormatADM">&nbsp;</td>
                    <td class="CellTextBoxADM">


                        <asp:GridView ID="grvContract"  runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" CellPadding="1"  Height="12px" Font-Size="13px" Style="text-align: left" Width="80%" DataSourceID="SqlDSContractDetail" GridLines="Horizontal" BorderStyle="None">
             <Columns>
                 <asp:TemplateField><ItemTemplate><asp:CheckBox ID="chkSelectRecContractB" runat="server" Font-Size="12px" Enabled="true" Height="18px" Visible="false"  Width="4%" AutoPostBack="false" Checked="true"     onchange="checkselectrecContract()"  CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGVB" runat="server" Text='<%# bind("ContractNo") %>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 <asp:TemplateField HeaderText="Location ID"><ItemTemplate><asp:TextBox ID="txtLocationIdGVB" runat="server" Text='<%# Bind("LocationID")%>' Height="15px" Width="90px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Contract Group"><ItemTemplate><asp:TextBox ID="txtContractGroupGVB" runat="server" Text='<%# Bind("ContractGroup")%>' Height="15px" Width="80px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGVB" runat="server" Text='<%# Bind("ServiceAddress")%>' Height="15px" Width="180px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
       
                      <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtRcnoGVB" runat="server"  Height="15px" Width="0px" Font-Size="11px" Enabled="False" Visible="false"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
             </Columns>
                            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#666666" Font-Names="Calibri"/><SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" /></asp:GridView>

                    </td>
                     

                 </tr>
              <tr>
                    <td class="CellFormatADM" colspan="2">


                        &nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
             <asp:SqlDataSource ID="SqlDSContractDetail" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" DataSourceMode="DataReader"></asp:SqlDataSource></td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
                        Services:</td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM">


                        <asp:GridView ID="grvService"  runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" CellPadding="1"  Height="12px" Font-Size="13px" Style="text-align: left" Width="65%" DataSourceID="SqlDSService" GridLines="Horizontal" BorderStyle="None">
             <Columns>
                 <asp:TemplateField><ItemTemplate><asp:CheckBox ID="chkSelectServiceRecS" runat="server" Font-Size="12px" Enabled="true" Height="18px" Visible="false"  Width="4%" AutoPostBack="false" Checked="true"    CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Record No."><ItemTemplate><asp:TextBox ID="txtRecordNoGVBS" runat="server" Text='<%# Bind("RecordNo")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
          
                        <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGVBS" runat="server" Text='<%# Bind("ContractNo")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
            
                 <asp:TemplateField HeaderText="Location ID"><ItemTemplate><asp:TextBox ID="txtLocationIdGVBS" runat="server" Text='<%# Bind("LocationID")%>' Height="15px" Width="90px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="Sch. Service Date"><ItemTemplate><asp:TextBox ID="txtSchServiceDateGVBS" runat="server" Text='<%# Bind("SchServiceDate", "{0:dd/MM/yyyy}")%>' Height="15px" Width="100px" Font-Size="12px" Enabled="False"  DataFormatString="{0:dd/MM/yyyy}"></asp:TextBox></ItemTemplate></asp:TemplateField>
       
                      <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtRcnoGVBS" runat="server" Text='<%# Bind("Rcno")%>' Height="15px" Width="10px" Font-Size="11px" Enabled="False" Visible="false"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
             </Columns>
                            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#666666" Font-Names="Calibri"/><SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" /></asp:GridView>

                                 </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
             <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" DataSourceMode="DataReader"></asp:SqlDataSource></td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
                        Invoices:</td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM">


                        <asp:GridView ID="grvInvoice"  runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" CellPadding="1"  Height="12px" Font-Size="13px" Style="text-align: left" Width="65%" DataSourceID="SqlDSInvoice" GridLines="Horizontal" BorderStyle="None">
             <Columns>
                 <asp:TemplateField><ItemTemplate><asp:CheckBox ID="chkSelectServiceRecI" runat="server" Font-Size="12px" Enabled="true" Height="18px" Visible="false"  Width="4%" AutoPostBack="false" Checked="true"    CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="Invoice No."><ItemTemplate><asp:TextBox ID="txtInvoiveNumberGVBI" runat="server" Text='<%# Bind("InvoiceNumber")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 <asp:TemplateField HeaderText="Invoice Date"><ItemTemplate><asp:TextBox ID="txtSalesDateGVBI" runat="server" Text='<%# Bind("SalesDate", "{0:dd/MM/yyyy}")%>' Height="15px" Width="80px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
           
                    <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGVBI" runat="server" Text='<%# Bind("CostCode")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 <asp:TemplateField HeaderText="Record No."><ItemTemplate><asp:TextBox ID="txtRecordNoGVBI" runat="server" Text='<%# Bind("RefType")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
              
                       <asp:TemplateField HeaderText="Location ID"><ItemTemplate><asp:TextBox ID="txtLocationIdGVBI" runat="server" Text='<%# Bind("LocationID")%>' Height="15px" Width="90px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="SubCode"><ItemTemplate><asp:TextBox ID="txtSubCodeGVBI" runat="server" Text='<%# Bind("SubCode")%>' Height="15px" Width="120px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
       
                      <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtRcnoGVBI" runat="server"  Height="15px" Width="10px" Font-Size="11px" Enabled="False" Visible="false"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
                 </Columns>
                            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#666666" Font-Names="Calibri"/><SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" /></asp:GridView>

                                 </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
             <asp:SqlDataSource ID="SqlDSInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" DataSourceMode="DataReader"></asp:SqlDataSource></td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
                        Receipts:</td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
                        &nbsp;</td>
                     

                    <td class="CellTextBoxADM">


                        <asp:GridView ID="grvReceipts"  runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" CellPadding="1"  Height="12px" Font-Size="13px" Style="text-align: left" Width="65%" DataSourceID="SqlDSReceipts" GridLines="Horizontal" BorderStyle="None">
             <Columns>
                 <asp:TemplateField><ItemTemplate><asp:CheckBox ID="chkSelectServiceRecI0" runat="server" Font-Size="12px" Enabled="true" Height="18px" Visible="false"  Width="4%" AutoPostBack="false" Checked="true"    CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="Receipt Number"><ItemTemplate><asp:TextBox ID="txtReceiptNoGVBR" runat="server" Text='<%# Bind("ReceiptNumber")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 <asp:TemplateField HeaderText="Receipt Date"><ItemTemplate><asp:TextBox ID="txtReceiptDateGVBR" runat="server" Text='<%# Bind("ReceiptDate", "{0:dd/MM/yyyy}")%>' Height="15px" Width="80px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
             
                     <asp:TemplateField HeaderText="Invoice Number"><ItemTemplate><asp:TextBox ID="txtRefTypeGVBR" runat="server" Text='<%# Bind("RefType")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 <asp:TemplateField HeaderText="Customer"><ItemTemplate><asp:TextBox ID="txtReceiptFromGVBR" runat="server" Text='<%# Bind("ReceiptFrom")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 <asp:TemplateField HeaderText="Amount"><ItemTemplate><asp:TextBox ID="txtAppliedBaseGVBR" runat="server" Text='<%# Bind("Appliedbase")%>' Height="15px" Width="90px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
       
                      <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtRcnoGVBR" runat="server"  Height="15px" Width="10px" Font-Size="11px" Enabled="False" Visible="false"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
             </Columns>
                            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#666666" Font-Names="Calibri"/><SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" /></asp:GridView>

                                 </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
             <asp:SqlDataSource ID="SqlDSReceipts" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" DataSourceMode="DataReader"></asp:SqlDataSource></td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
                        Credit/Debit Notes:</td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
                        &nbsp;</td>
                     

                    <td class="CellTextBoxADM">


                        <asp:GridView ID="grvCNDN"  runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" CellPadding="1"  Height="12px" Font-Size="13px" Style="text-align: left" Width="65%" DataSourceID="SqlDSCNDN" GridLines="Horizontal" BorderStyle="None">
             <Columns>
                 <asp:TemplateField><ItemTemplate><asp:CheckBox ID="chkSelectServiceRecI1" runat="server" Font-Size="12px" Enabled="true" Height="18px" Visible="false"  Width="4%" AutoPostBack="false" Checked="true"    CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="CN/DN No."><ItemTemplate><asp:TextBox ID="txtInvoiceNumberGVBCN" runat="server" Text='<%# Bind("InvoiceNumber")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                <asp:TemplateField HeaderText="CN/DN Date"><ItemTemplate><asp:TextBox ID="txtInvoiceDateGVBCN" runat="server" Text='<%# Bind("SalesDate", "{0:dd/MM/yyyy}")%>' Height="15px" Width="80px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
           
                       <asp:TemplateField HeaderText="Record No."><ItemTemplate><asp:TextBox ID="txtRecordNoGVBCN" runat="server" Text='<%# Bind("RefType")%>' Height="15px" Width="150px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
             <asp:TemplateField HeaderText="Invoice No."><ItemTemplate><asp:TextBox ID="txtSourceInvoiveGVBCN" runat="server" Text='<%# Bind("SourceInvoice")%>' Height="15px" Width="120px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
           
                       <asp:TemplateField HeaderText="Location ID"><ItemTemplate><asp:TextBox ID="txtLocationIdGVBCN" runat="server" Text='<%# Bind("LocationID")%>' Height="15px" Width="90px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="SubCode"><ItemTemplate><asp:TextBox ID="txtSubCodeGVBCN" runat="server" Text='<%# Bind("SubCode")%>' Height="15px" Width="120px" Font-Size="12px" Enabled="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
       
                      <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtRcnoGVBCN" runat="server"  Height="15px" Width="10px" Font-Size="11px" Enabled="False" Visible="false"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
             </Columns>
                            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#666666" Font-Names="Calibri"/><SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" /></asp:GridView>

                                 </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
             <asp:SqlDataSource ID="SqlDSCNDN" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" DataSourceMode="DataReader"></asp:SqlDataSource></td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
                        Journals:</td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
                        &nbsp;</td>
                     

                    <td class="CellTextBoxADM">


                        <asp:GridView ID="grvJournals"  runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" CellPadding="1"  Height="12px" Font-Size="13px" Style="text-align: left" Width="65%" DataSourceID="SqlDSJournals" GridLines="Horizontal" BorderStyle="None">
             <Columns>
                 <asp:TemplateField><ItemTemplate><asp:CheckBox ID="chkSelectServiceRecI2" runat="server" Font-Size="12px" Enabled="true" Height="18px" Visible="false"  Width="4%" AutoPostBack="false" Checked="true"    CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="Journal No."><ItemTemplate><asp:TextBox ID="txtInvoiveNumberGVBJV" runat="server" Text='<%# Bind("VoucherNumber")%>' Height="15px" Width="120px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
             <asp:TemplateField HeaderText="Journal Date"><ItemTemplate><asp:TextBox ID="txtContractNoGVBJV" runat="server" Text='<%# Bind("JournalDate", "{0:dd/MM/yyyy}")%>' Height="15px" Width="80px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
           
                       <asp:TemplateField HeaderText="Invoice No."><ItemTemplate><asp:TextBox ID="txtRecordNoGVBJV" runat="server" Text='<%# Bind("RefType")%>' Height="15px" Width="120px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 <asp:TemplateField HeaderText="Journal Date"><ItemTemplate><asp:TextBox ID="txtCreditbaseGVBJV" runat="server" Text='<%# Bind("Creditbase")%>' Height="15px" Width="80px" Font-Size="12px" Enabled="False"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
           
         
                      <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtRcnoGVBJV" runat="server"  Height="15px" Width="10px" Font-Size="11px" Enabled="False" Visible="false"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
             </Columns>
                            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#666666" Font-Names="Calibri"/><SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" /></asp:GridView>

                                 </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
             <asp:SqlDataSource ID="SqlDSJournals" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" DataSourceMode="DataReader"></asp:SqlDataSource></td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM"><asp:Button ID="btnSaveRelocate" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Transfer Account" Width="130px" OnClientClick="currentdatetime()"/>
                                 </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
        
          </table>


                     <%--Start: SAVE: Edit Agree Value: No Change --%>
                                              
                 <asp:Panel ID="pnlCompleteConfirm" runat="server" BackColor="White" Width="400px" Height="140px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEditAgreeValueNoChange" runat="server" Text="ACCOUNT TRANSFER"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label18" runat="server" Text="Account Transfer is Complete."></asp:Label>
                        
                      </td>
                           </tr>

                
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditAgreeValueNoChange" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlCompleteConfirm" runat="server" CancelControlID="" PopupControlID="pnlCompleteConfirm" TargetControlID="btnDummyCompleteConfirm" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyCompleteConfirm" runat="server" CssClass="dummybutton" />

             <%-- End: SAVE: Edit Agree Value : No Change--%>

       <%-- Start--%>

        <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="98%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center" ScrollBars="Horizontal">
    <table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
    </td></tr>
        
        <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160;
         <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
    <asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" Visible="False"></asp:ImageButton>
    <asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg"  Width="24px"></asp:ImageButton>
                   <asp:TextBox ID="txtUpdated" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
  

</td><td>
                <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>

            </td></tr>


    </table><div style="text-align:center; padding-left: 20px; padding-bottom: 5px;"><div class="AlphabetPager">
       
                
</div><br />
                    <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                       CellPadding="2" GridLines="Vertical" Font-Size="15px" Width="80%"  CellSpacing="6">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
        <ControlStyle Width="5%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Width="5%" />
        </asp:CommandField>
            <asp:BoundField DataField="AccountType" HeaderText="Account Type" />
        <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" >
        <ControlStyle Width="8%" />

        <HeaderStyle Wrap="False" HorizontalAlign="Left" />

        </asp:BoundField>
        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
        <ControlStyle Width="8%" />

        <HeaderStyle Width="100px" HorizontalAlign="Left" />

        <ItemStyle Width="10%" Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="Client Name" SortExpression="Name">
        <ControlStyle Width="35%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        </Columns>

        <EditRowStyle BackColor="#999999" />

        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />

        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />

        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />

        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

        <SortedAscendingCellStyle BackColor="#E9E7E2" />

        <SortedAscendingHeaderStyle BackColor="#506C8C" />

        <SortedDescendingCellStyle BackColor="#FFFDF8" />

        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>



                <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  
        </asp:SqlDataSource>
                
             
        </div></asp:Panel>
       <%-- end--%>
    
                 <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlClientClose" Enabled="True" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient">
                                    </asp:ModalPopupExtender>
       </div>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False" Width="5px"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False" Width="5px"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False" Width="5px"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False" Width="5px"></asp:TextBox>
        
                    <asp:TextBox ID="txtLocationNo" runat="server" Visible="False" Width="5px"></asp:TextBox>
                    <asp:TextBox ID="txtLocationPrefix" runat="server" Visible="False" Width="5px"></asp:TextBox>
                       <asp:Button ID="btnDummyClient" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                    <asp:TextBox ID="txtFrom" runat="server" Visible="False" Width="5px"></asp:TextBox>
                       <asp:TextBox ID="txtRcnoContact" runat="server" Visible="False" Width="5px"></asp:TextBox>  
              
                     </ContentTemplate> 
          <Triggers>
           <asp:AsyncPostBackTrigger  ControlID="btnSaveRelocate" />
        </Triggers>
     </asp:UpdatePanel>             
     </asp:Content>