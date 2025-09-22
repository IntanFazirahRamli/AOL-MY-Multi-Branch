<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_Bank.aspx.vb" EnableEventValidation="false" Inherits="Master_Bank" Title="Bank Master"  Culture="en-GB"%>

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
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Bank</h3>
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

                       </td>
                <td style="text-align: right">
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" /></td>
                
            </tr>
                  <tr>
              <td colspan="2">
              <asp:SqlDataSource ID="SqlDSMarketSegmentID" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(MarketSegmentID, ' - ', Description) AS MarketSegmentID FROM tblindustrysegment ORDER BY MarketSegmentID">
                         </asp:SqlDataSource>
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">
  <asp:GridView ID="GridView1" runat="server" CssClass="centered" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="BankID" Font-Size="15px" AllowSorting="True" AllowPaging="True" HorizontalAlign="Center" PageSize="15">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false" >
                <ControlStyle Width="70px" />
                <ItemStyle Width="70px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="BankID" HeaderText="Bank ID" ReadOnly="True" SortExpression="BankID">
                <ControlStyle Width="270px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Bank" HeaderText="Bank" SortExpression="Bank">
                <HeaderStyle Font-Size="12pt" />
                 <ItemStyle Width="400px" />
                </asp:BoundField>
                 <asp:BoundField DataField="RecvPrefix" HeaderText="Receipt Prefix">
                 <ControlStyle Width="350px" />
                 <HeaderStyle Width="80px" />
                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="PayvPrefix" HeaderText="Payment Prefix">
                 <ItemStyle Width="80px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="LedgerCode" HeaderText="Ledger Code" SortExpression="LedgerCode">
                 <ControlStyle Width="100px" />
                 <ItemStyle Width="100px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="LedgerRecvDist" HeaderText="Receipt Voucher Ledger">
                 <ControlStyle Width="100px" />
                 <ItemStyle Width="100px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="LedgerPayvDist" HeaderText="Payment Voucher Ledger">
                 <ControlStyle Width="100px" />
                 <ItemStyle Width="100px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Currency" HeaderText="Currency" />
                 <asp:BoundField DataField="Location" HeaderText="Location" />
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
                     

                    <td class="CellFormatADM">Bank ID</td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtBankID" runat="server" MaxLength="30" Height="16px" Width="44.5%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Bank Name</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtBankName" runat="server" MaxLength="100" Height="16px" Width="44.5%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Receipt Voucher Prefix</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtReceiptVoucherPrefix" runat="server" MaxLength="4" Height="16px" Width="44.5%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Payment Voucher Prefix</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtPaymentVoucherPrefix" runat="server" MaxLength="4" Height="16px" Width="44.5%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Bank Ledger</td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:DropDownList ID="ddlBankLedger" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataValueField="MarketSegmentID" Height="25px" Width="45%" TabIndex="25">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Receipt Voucher Ledger</td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:DropDownList ID="ddlReceiptVoucherLedger" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="45%" TabIndex="25">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Payment Voucher Ledger</td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:DropDownList ID="ddlPaymentVoucherLedger" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="45%" TabIndex="25">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Currency</td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:DropDownList ID="ddlCurrency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="45%" TabIndex="25">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">
             <asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label>
                    </td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="45%" TabIndex="25">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                     

                 </tr>
           <tr>
               <td colspan="2" style="text-align:right">   
                   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
                   <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
               </td>
           </tr>
          </table>
    
       </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>
            
                    <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
 
    </asp:Content>