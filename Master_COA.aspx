<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_COA.aspx.vb" Inherits="Master_COA" Title="Chart Of Accounts"  Culture="en-GB"%>

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

    var defaultText1 = "Search Here";
    function WaterMark1(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultText1;
        }
        if (txt.value == defaultText1 && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

</script>
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Chart Of Accounts</h3>
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
               <br /></td>
            </tr>



                    <tr>
     <td colspan="2" style="text-align:right">
           <table style="text-align:right;width:100%">
            <tr>
                <td style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="text-align:right;width:45%;display:inline;">
                    
         <asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="RESET SEARCH" /></td>
                    <td style="text-align:left;width:35%">    <asp:TextBox ID="txtSearchCOA" runat="server" Width="380px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True"></asp:TextBox> &nbsp; <asp:ImageButton ID="btnGo" runat="server" ImageUrl="~/Images/gobutton.jpg" Height="25px" Width="50px" ToolTip="RESET SEARCH" Visible="False" />
                            
               <asp:TextBox ID="txtSearchText" runat="server" CssClass="dummybutton" ></asp:TextBox>                </td>
            </tr>
        </table>
</td>
            </tr>


            <tr class="Centered">
                <td colspan="2">
  <asp:GridView ID="GridView1" runat="server" CssClass="centered" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="COACode" Font-Size="15px" AllowSorting="True" AllowPaging="True" HorizontalAlign="Center">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="50px" />
                <ItemStyle Width="50px" />
                </asp:CommandField>
                <asp:BoundField DataField="COACode" HeaderText="GL Code" ReadOnly="True" SortExpression="COACode">
                <ControlStyle Width="50px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="50px" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                <ControlStyle Width="300px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="300px" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" >
                
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                
                 <asp:BoundField DataField="Area" HeaderText="Area">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Function" HeaderText="Function">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Organization" HeaderText="Organisation">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                 <asp:BoundField DataField="ServiceType" HeaderText="Service Type">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                 <asp:BoundField DataField="CostCenter" HeaderText="Cost Center">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                 <asp:BoundField DataField="GLType" HeaderText="Type" />
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
                     
                 <asp:BoundField DataField="GLAccount">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
                 <asp:BoundField DataField="DistCode">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
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
              <td colspan="2"><br /></td>
          </tr>
            <tr>
                     

                    <td class="CellFormatADM">Code<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtCOACode" runat="server" MaxLength="50" Height="16px" Width="45%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Description</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Height="16px" Width="45%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                    <td class="CellFormatADM">Type<asp:Label ID="Label24" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                    <td class="CellTextBoxADM">
                <asp:DropDownList runat="server" AppendDataBoundItems="True" Height="20px" Width="45.5%" ID="ddlGLType"><asp:ListItem>--SELECT--</asp:ListItem>
                    <asp:ListItem>TRADE DEBTOR</asp:ListItem>
                    <asp:ListItem>CASH</asp:ListItem>
                    <asp:ListItem>BANK</asp:ListItem>
                    <asp:ListItem>REVENUE</asp:ListItem>
                    <asp:ListItem>SALE</asp:ListItem>
                    <asp:ListItem>GST OUTPUT</asp:ListItem>
                    <asp:ListItem>RETENTION</asp:ListItem>
                    <asp:ListItem>CN</asp:ListItem>
                    <asp:ListItem>OTHER DEBTORS</asp:ListItem>
                    <asp:ListItem Value="LIABILITIES">LIABILITIES</asp:ListItem>
</asp:DropDownList>



                    </td>
                     

                 </tr>
                 <tr>
               <td colspan="2" style="text-align:right">   
                   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
                   <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
               </td>
           </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:DropDownList runat="server" AppendDataBoundItems="True" Height="20px" Width="45.5%" ID="ddlCompanyGrp" Visible="False"><asp:ListItem>--SELECT--</asp:ListItem>
                    </asp:DropDownList>



                    </td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtArea" runat="server" MaxLength="100" Height="16px" Width="45%" Visible="False"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtFunction" runat="server" MaxLength="100" Height="16px" Width="45%" Visible="False"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtOrganisation" runat="server" MaxLength="100" Height="16px" Width="45%" Visible="False"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtServiceType" runat="server" MaxLength="100" Height="16px" Width="45%" Visible="False"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtCostCenter" runat="server" MaxLength="100" Height="16px" Width="45%" Visible="False"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">&nbsp;</td>
                     

                    <td class="CellTextBoxADM">&nbsp;</td>
                     

                 </tr>
        
          </table>
    
       </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblchartofaccounts order by COACode"></asp:SqlDataSource>

       
           <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="5px" Visible="false"></asp:TextBox>
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
     <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
 
    </asp:Content>