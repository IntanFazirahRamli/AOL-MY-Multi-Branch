<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_Period.aspx.vb" Inherits="Master_Period" Title="Period" EnableEventValidation="false"  Culture="en-GB"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Start:View Edit History--%>
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

    function ResetScrollPosition() {
        setTimeout("window.scrollTo(0,0)", 0);
    }

    function ConfirmEmailInvoice() {

        var confirm_value = document.createElement("INPUT");
        confirm_value.value = "";
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (document.getElementById("<%=txtCalendarPeriod.ClientID%>").value != '') {

                if (confirm("Do you want to Email Invoices for the period  " + document.getElementById("<%=txtCalendarPeriod.ClientID%>").value + " ?")) {
                    confirm_value.value = "Yes";

                } else {
                    confirm_value.value = "No";
                }

                document.forms[0].appendChild(confirm_value);
            }
    }

  
</script>
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

          .tbborder {
              border: 1px solid black;
              border-collapse: collapse;
          }

        </style>
        <asp:UpdateProgress ID="updateProgress" runat="server">
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
              <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
              <ControlBundles>
             <asp:controlBundle name="ModalPopupExtender_Bundle"/>
                  </ControlBundles>
    </asp:ToolkitScriptManager>     
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Period</h3>
        <table style="width:100%;text-align:center;">
            <%--<tr>
                <td colspan="2"><br /></td>
            </tr>--%>
            <tr>
               <td colspan="3" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
                 <tr>
               <td colspan="3" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                       <asp:Label ID="txtMode" runat="server" Text="" CssClass="dummybutton"></asp:Label>
</td>
                     
            </tr>
            
            <tr>
              
                <td style="width:40%;text-align:left;">
                  <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="true" />
                    <asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Visible="False"/>

                     <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
                      <asp:Button ID="btnEmailInvoice" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="EMAIL INVOICE" Width="150px" Visible="false" />

                       </td>
                <td colspan="2" style="text-align: right">
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" /></td>
                
            </tr>
            <tr>
                <td colspan="3">
                      <table style="text-align:right;width:100%">
            <tr>
                  <td style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td>
                </td>
            </tr>
        </table>
                </td>
            </tr>
                  <tr>
              <td colspan="3">
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="3">
  <asp:GridView ID="GridView1" runat="server" CssClass="centered"  OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="CalendarPeriod" Font-Size="15px" AllowSorting="True" AllowPaging="True" HorizontalAlign="Center" PageSize="12">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                <ControlStyle Width="50px" />
                <ItemStyle Width="50px" />
                </asp:CommandField>
                <asp:BoundField DataField="CalendarPeriod" HeaderText="Calendar Period" SortExpression="CalendarPeriod">
                </asp:BoundField>
<asp:BoundField DataField="AccountingPeriod" HeaderText="Accounting Period" SortExpression="AccountingPeriod" ReadOnly="True">

<HeaderStyle Font-Size="12pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
               
                 <asp:BoundField DataField="Location" HeaderText="Location">
                 </asp:BoundField>
               
              <%--  <asp:BoundField DataField="ARLOCK" HeaderText="AR LOCK">
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>--%>
               
                 <asp:TemplateField HeaderText="AR LOCK">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkARLOCK" runat="server"  Enabled="false" Checked='<%#If(Eval("ARLOCK").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="AR LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkARLOCKE" runat="server"  Enabled="false" Checked='<%#If(Eval("ARLOCKE").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="CN LOCK">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkCNLOCK" runat="server"  Enabled="false" Checked='<%#If(Eval("CNLOCK").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="CN LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkCNLOCKE" runat="server"  Enabled="false" Checked='<%#If(Eval("CNLOCKE").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="RV LOCK">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkRVLOCK" runat="server"  Enabled="false" Checked='<%#If(Eval("RVLOCK").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="RV LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkRVLOCKE" runat="server"  Enabled="false" Checked='<%#If(Eval("RVLOCKE").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="JN LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkJNLOCK" runat="server"  Enabled="false" Checked='<%#If(Eval("JNLOCK").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="JN LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkJNLOCKE" runat="server"  Enabled="false" Checked='<%#If(Eval("JNLOCKE").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="EINV SUBMIT LOCK">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkEINVSubmitLock" runat="server"  Enabled="false" Checked='<%#If(Eval("EINVSubmitLock").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="EINV CANCEL LOCK">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkEINVCancelLock" runat="server"  Enabled="false" Checked='<%#If(Eval("EINVCancelLock").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                
               <%--  <asp:BoundField DataField="ARLOCKE" HeaderText="AR LOCKE" />--%>
               
               <%--  <asp:BoundField DataField="CNLOCK" HeaderText="CN LOCK" />
                 <asp:BoundField DataField="CNLOCKE" HeaderText="CN LOCKE" />
               
                 <asp:BoundField DataField="RVLOCK" HeaderText="RV LOCK" />
                <asp:BoundField DataField="RVLOCKE" HeaderText="RV LOCKE"></asp:BoundField>
                 <asp:BoundField DataField="JNLOCK" HeaderText="JN LOCK" />
                
                <asp:BoundField DataField="JNLOCKE" HeaderText="JN LOCKE"></asp:BoundField>--%>
                 <asp:BoundField DataField="GSTType" HeaderText="GST Type" />
                 <asp:BoundField DataField="GSTRate" HeaderText="GST Rate" />
                 <asp:BoundField DataField="AUTOEMAIL" HeaderText="AUTOEMAILINVOICE" />
                   <asp:BoundField DataField="AUTOEMAILSOA" HeaderText="AUTOEMAILSOA" />
                   <asp:TemplateField HeaderText="ADDITIONALFILE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkAddtnlFile" runat="server"  Enabled="false" Checked='<%#If(Eval("AdditionalFile").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
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
               
                 <asp:BoundField DataField="GstDate" >
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                 <asp:BoundField DataField="GstTime" >
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
               
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
              <td colspan="3"><br /></td>
          </tr>
            <tr>
                     

                    <td class="CellFormatADM">Calendar Period<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td colspan="2" class="CellTextBoxADM"> <asp:TextBox ID="txtCalendarPeriod" runat="server" MaxLength="50" Height="16px" Width="20%"></asp:TextBox></td>
                     

                 </tr>
            <tr>
                     

                    <td class="CellFormatADM">Accounting Period</td>
                     

                    <td colspan="2" class="CellTextBoxADM"> <asp:TextBox ID="txtAccountingPeriod" runat="server" MaxLength="50" Height="16px" Width="20%"></asp:TextBox></td>
                     

                 </tr>

            <tr >
                     

                    <td class="CellFormatADM">
                        <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
                    </td>
                     

                    <td colspan="2" class="CellTextBoxADM">
                        <asp:DropDownList runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select" Width="20%" ID="txtLocationId"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
</asp:DropDownList>

                    </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">All</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" />
                        </td>
                     <td rowspan="8" class="CellTextBoxADM">
                         <table border="1" style="width:400px;">
                             <tr style="background-color:lightgray;text-align:center">
                                 <th>Document</th>
                                 <th>Open</th>
                                 <th>Posted</th>
                                 <th>Total</th>
                             </tr>
                             <tr>
                                 <td>Invoice</td>
                                 <td style="text-align:right"><asp:Label ID="lblInvOpenCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblInvPostedCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblInvTotalCount" runat="server" Text=""></asp:Label></td>
                             </tr>
                              <tr>
                                 <td>CreditNote</td>
                                 <td style="text-align:right"><asp:Label ID="lblCNOpenCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblCNPostedCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblCNTotalCount" runat="server" Text=""></asp:Label></td>
                             </tr>
                              <tr>
                                 <td>DebitNote</td>
                                 <td style="text-align:right"><asp:Label ID="lblDNOpenCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblDNPostedCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblDNTotalCount" runat="server" Text=""></asp:Label></td>
                             </tr>
                              <tr>
                                 <td>Receipt</td>
                                 <td style="text-align:right"><asp:Label ID="lblRecOpenCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblRecPostedCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblRecTotalCount" runat="server" Text=""></asp:Label></td>
                             </tr>
                              <tr>
                                 <td>Journal</td>
                                 <td style="text-align:right"><asp:Label ID="lblJNOpenCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblJNPostedCount" runat="server" Text=""></asp:Label></td>
                                  <td style="text-align:right"><asp:Label ID="lblJNTotalCount" runat="server" Text=""></asp:Label></td>
                             </tr>
                         </table>
                     </td>

                 </tr>
              <tr>
                  <td class="CellFormatADM">Invoice Add Lock</td>
                  <td class="CellTextBoxADM">
                      <asp:CheckBox ID="chkInvoiceAddLock" runat="server" AutoPostBack="True" />
                  </td>
        </tr>
              <tr>
                     

                    <td class="CellFormatADM">Invoice Edit Lock</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:CheckBox ID="chkInvoiceEditLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Credit Note&nbsp; Add Lock</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:CheckBox ID="chkCNAddLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Credit Note Edit Lock</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:CheckBox ID="chkCNEditLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;Receipt Add Lock</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:CheckBox ID="chkReceiptAddLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;Receipt Edit Lock</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:CheckBox ID="chkReceiptEditLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;Journal Add Lock</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:CheckBox ID="chkJournalAddLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Journal Edit Lock</td>
                     

                    <td colspan="2" class="CellTextBoxADM">
                        <asp:CheckBox ID="chkJournalEditLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
           <tr>
                     

                    <td class="CellFormatADM">Malaysia EInvoice Submission Lock</td>
                     

                    <td colspan="2" class="CellTextBoxADM">
                        <asp:CheckBox ID="chkEInvSubmitLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
           <tr>
                     

                    <td class="CellFormatADM">Malaysia EInvoice Cancellation Lock</td>
                     

                    <td colspan="2" class="CellTextBoxADM">
                        <asp:CheckBox ID="chkEInvCancelLock" runat="server" AutoPostBack="True" />
                        </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">GST Type</td>
                     

                    <td colspan="2" class="CellTextBoxADM"><asp:TextBox ID="txtGSTType" runat="server" MaxLength="100" Height="16px" Width="20%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">GST Percent</td>
                     

                    <td colspan="2" class="CellTextBoxADM"><asp:TextBox ID="txtGSTRate" runat="server" MaxLength="100" Height="16px" Width="20%"></asp:TextBox></td>
                     

                 </tr>
              <tr style="display:none">
                     

                    <td class="CellFormatADM">Used in Module</td>
                     

                    <td colspan="2" class="CellTextBoxADM">
                        <asp:CheckBox ID="chkInvoice" runat="server" Text="Invoice" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkReceipt" runat="server" Text="Receipt" />
                         <asp:CheckBox ID="chkJournal" runat="server" Text="Journal" />
                    </td>
                     

                 </tr>
              
               <tr>
                     

                    <td class="CellFormatADM">Auto Email Invoice</td>
                     

                    <td colspan="2" class="CellTextBoxADM">
                        <asp:Button ID="btnAutoEmail" runat="server" BackColor="#99CCFF" CssClass="roundbutton1" Font-Bold="True" ForeColor="Black" OnClientClick="currentdatetime()" Text="Enable" Width="100px" />
                          &nbsp;&nbsp;<asp:Label ID="lblInvoice" runat="server" Text="" CssClass="CellFormat"></asp:Label> 
                          &nbsp;&nbsp; <asp:CheckBox ID="chkAdditionalFile" runat="server" Text="Send Additional File" /> 
                         <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="30px" Height="25px" OnClientClick="currentdatetime()" />
                      </td>
                     

                 </tr>
             <tr>
                     

                    <td class="CellFormatADM">Auto Email SOA</td>
                     

                    <td colspan="2" class="CellTextBoxADM">
                        <asp:Button ID="btnAutoEmailSOA" runat="server" BackColor="#99CCFF" CssClass="roundbutton1" Font-Bold="True" ForeColor="Black" OnClientClick="currentdatetime()" Text="Enable" Width="100px" />
                      &nbsp;&nbsp;<asp:Label ID="lblSOA" runat="server" Text="" CssClass="CellFormat"></asp:Label>  </td>
                     

                 </tr>
           <tr>
               <td colspan="3" style="text-align:right">   
                   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
                   <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
               </td>
           </tr>
          </table>
    
       </div>

                <%--Confirm message--%>
                                              
                 <asp:Panel ID="pnlConfirmMsg" runat="server" BackColor="White" Width="400px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEvent" runat="server" Text="Confirm Auto Email Invoice"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label id="lblQuery" runat="server" Text="Do you want to Email Invoices?"></asp:Label> </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnConfirmNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />

             <%-- Confirm Message--%>


                          <%-- Start:View Edit History--%>
              
              
<%--              <asp:Panel ID="pnlViewEditHistory" runat="server" BackColor="White" Width="1300px" Height="95%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0" style="width: 1271px">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">View Record History</h4> 
  </td> <td>
                               <asp:TextBox ID="TextBox3" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label7" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 6px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewEditHistory1" runat="server" DataSourceID="sqlDSViewEditHistory" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="14px"
         CellPadding="1" GridLines="None" Width="99%" PageSize="20"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
                        
             
                  <asp:BoundField DataField="CalendarPeriod" HeaderText="Calendar Period" SortExpression="CalendarPeriod">
                </asp:BoundField>
<asp:BoundField DataField="AccountingPeriod" HeaderText="Accounting Period" SortExpression="AccountingPeriod" ReadOnly="True">

<HeaderStyle Font-Size="12pt" HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
               
                 <asp:BoundField DataField="Location" HeaderText="Location">
                 </asp:BoundField>
         
               
                 <asp:TemplateField HeaderText="AR LOCK">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkARLOCK" runat="server"  Enabled="false" Checked='<%#If(Eval("ARLOCK").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="AR LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkARLOCKE" runat="server"  Enabled="false" Checked='<%#If(Eval("ARLOCKE").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="CN LOCK">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkCNLOCK" runat="server"  Enabled="false" Checked='<%#If(Eval("CNLOCK").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="CN LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkCNLOCKE" runat="server"  Enabled="false" Checked='<%#If(Eval("CNLOCKE").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="RV LOCK">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkRVLOCK" runat="server"  Enabled="false" Checked='<%#If(Eval("RVLOCK").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="RV LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkRVLOCKE" runat="server"  Enabled="false" Checked='<%#If(Eval("RVLOCKE").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="JN LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkJNLOCK" runat="server"  Enabled="false" Checked='<%#If(Eval("JNLOCK").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="JN LOCKE">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkJNLOCKE" runat="server"  Enabled="false" Checked='<%#If(Eval("JNLOCKE").ToString() = "Y", True, False)%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                
            
                 <asp:BoundField DataField="GSTType" HeaderText="GST Type" />
                 <asp:BoundField DataField="GSTRate" HeaderText="GST Rate" />
                 <asp:BoundField DataField="AUTOEMAIL" HeaderText="AUTOEMAILINVOICE" />
                   <asp:BoundField DataField="AUTOEMAILSOA" HeaderText="AUTOEMAILSOA" />
                <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn"  HeaderText="EditedOn" SortExpression="LastModifiedOnLog" >
               
              
               
            
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
               
               <asp:BoundField DataField="TriggerType" HeaderText="Action" >
               
            
                  <ItemStyle Wrap="False" />
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

        </asp:GridView><br />
            
                  <asp:SqlDataSource ID="sqlDSViewEditHistory" runat="server" ConnectionString="<%$ ConnectionStrings:sitadata_RecordLogConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadata_RecordLogConnectionString.ProviderName %>">
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:20px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="Button2" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

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
  
    --%>
             <%-- End:View Edit History--%>

                       <%--Msg for Open Transactions--%>
                                              
                 <asp:Panel ID="pnlViewMsgOpenTransactions" runat="server" BackColor="White" Width="350px" Height="180px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label2" runat="server" Text="Open Transactions"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label id="Label3" runat="server" Text="There are still Open Transactions for this Period."></asp:Label> </td>
                           </tr>
                         <tr>
                             <td colspan="2"><br /></td>
                </tr>   
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label id="Label4" runat="server" Text="Please check the list and  "></asp:Label> </td>
                           </tr>
                            

             <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label id="Label5" runat="server" Text=" Post/Update before you can fully lock the period."></asp:Label> </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnViewMsgOpenTransactions" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px" OnClientClick="currentdatetime()"/>
                                
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlViewMsgOpenTransactions" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlViewMsgOpenTransactions" TargetControlID="btnDummyMsgOpenTransactions" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyMsgOpenTransactions" runat="server" CssClass="dummybutton" />

             <%-- Msg for Open Transactions--%>


                               <%-- Start:View Open Transactions--%>
              
              
              <asp:Panel ID="pnlViewOpenTransactions" runat="server" BackColor="White" Width="600px" Height="95%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0" style="width: 570px">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">View Open Transactions</h4> 
  </td> <td>
                               <asp:TextBox ID="TextBox5" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label43" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 20px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewEditHistory" runat="server" DataSourceID="sqlDSViewOpenTransactions" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%" PageSize="20"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
              
                <asp:BoundField DataField="DocumentType" HeaderText="Document Type" >
                <ControlStyle Width="10%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" >
                <ControlStyle Width="12%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="DocumentNumber" HeaderText="Document Number">
                <ControlStyle Width="16%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="16%" />
                </asp:BoundField>
              
                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" >
                <ControlStyle Width="10%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="10%" />
                </asp:BoundField>
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
            
                  <asp:SqlDataSource ID="sqlDSViewOpenTransactions" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
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

                <asp:ModalPopupExtender ID="mdlViewOpenTransactions" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlViewOpenTransactions" TargetControlID="btnDummyViewOpenTransactions" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewOpenTransactions" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Open Transactions--%>


              
               <%-- Start:View No Details--%>
              
              
              <asp:Panel ID="pnlViewNoDetails" runat="server" BackColor="White" Width="600px" Height="95%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">View No Details</h4> 
  </td> <td>
                               <asp:TextBox ID="TextBox2" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label6" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 20px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewNoDetails" runat="server" DataSourceID="sqlDSViewNoDetails" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%" PageSize="20"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
              
                <asp:BoundField DataField="DocumentType" HeaderText="Document Type" >
                <ControlStyle Width="10%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" >
                <ControlStyle Width="12%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="DocumentNumber" HeaderText="Document Number">
                <ControlStyle Width="16%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="16%" />
                </asp:BoundField>
              
                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" DataFormatString="{0:dd/MM/yyyy}" >
                <ControlStyle Width="10%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="10%" />
                </asp:BoundField>
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
            
                  <asp:SqlDataSource ID="sqlDSViewNoDetails" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:20px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="Button1" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
              
             

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewNoDetails" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlViewNoDetails" TargetControlID="btnDummyViewNoDetails" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewNoDetails" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View No Detail--%>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblPeriod order by calendarperiod desc"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
                  <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Height="16px" MaxLength="50" Visible="false" Width="1px"></asp:TextBox>
                  <asp:TextBox ID="txtDefaulTaxType" runat="server" Height="16px" MaxLength="50" Visible="false" Width="1px"></asp:TextBox>
                  <asp:TextBox ID="txtDefaultTaxRate" runat="server" Height="16px" MaxLength="50" Visible="false" Width="1px"></asp:TextBox>
                                  
        
     <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
              <asp:TextBox ID="txtLogDocNo" runat="server" Visible="False"></asp:TextBox>
              
  </ContentTemplate> 
             <Triggers>
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
                 </Triggers>
     </asp:UpdatePanel>
</asp:Content>
