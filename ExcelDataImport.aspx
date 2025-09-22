<%@ Page Title="Excel Data Import" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ExcelDataImport.aspx.vb" Inherits="ExcelDataImport" %>

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
      <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
     <asp:TextBox ID="txtWorkBookName" runat="server" CssClass="dummybutton"></asp:TextBox>
    <asp:TextBox ID="txtFailureCount" runat="server" CssClass="dummybutton"></asp:TextBox>
<asp:TextBox ID="txtSuccessCount" runat="server" CssClass="dummybutton"></asp:TextBox>
<asp:TextBox ID="txtFailureString" runat="server" CssClass="dummybutton"></asp:TextBox>

      <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Excel Data Import</h3>

          <table style="width:100%;text-align:center;">                      
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
                       </td>
                <td style="text-align: right">
                   
                  <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />
                </td>
                    </tr>
              </table>
            <table class="Centered" style="border: 1px solid #808080; text-align:right; width:100%; border-radius: 5px;padding: 2px; height:80px; background-color: #F3F3F3;">
          
        <%--  <table style=";width:100%">--%>
                <tr>
                    <td class="CellFormat" style="width:10%">Module</td>
                   <td class="CellFormat" style="text-align: left; padding-right: 0%; padding-left:3%;width:90%">
                   <asp:RadioButtonList ID="rdbModule" runat="server" CausesValidation="True" CellPadding="4" CellSpacing="5" Height="63px" Visible="true" Width="100%" RepeatDirection="Horizontal">
                           <asp:ListItem Selected="True" Value="Corporate">Corporate</asp:ListItem>                          
                           <asp:ListItem Value="CorporateLocation" Enabled="True">Corporate ServiceLocation</asp:ListItem>
                        <asp:ListItem Value="Residential" Enabled="True">Residential</asp:ListItem>
                           <asp:ListItem Value="ResidentialLocation" Enabled="true">Residential ServiceLocation</asp:ListItem>                                                                                                                        
                         </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
              <tr>
                  <td class="CellFormat"> Sample Templates</td>
                  <td style="text-align:left;padding-left:5%">
                       <asp:ImageButton ID="btnCorporateTemplate" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                     <br /><br />
                  </td>
              </tr>
                <tr>
                                                                <td class="CellFormat">Select Excel File to Import Data </td>
                                                                <td class="CellTextBox" colspan="1" style="text-align:center;padding-left:3%">
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="Centered" Width="100%" />
                                                                </td>
                                                            </tr>
              <tr>
                                                                <td colspan="2">
                                                                    <br />
                                                                </td>
                                                            </tr>
             <tr>
                                                                <td class="centered" colspan="2">
                                                                    <asp:Button ID="btnUpload" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" OnClientClick="currentdatetime()" Font-Bold="True" width="100px" Text="UPLOAD" />
                                                                         <asp:Button ID="btnCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" />
                                                 
                                                                </td>
                                                            </tr>
                <tr>
                                                                <td colspan="2">
                                                                    <br />
                                                                </td>
                                                            </tr>
        </table>
          <br />
           <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="25px" OnClientClick="currentdatetime()" />
                 
                      <asp:GridView ID="GridView1" runat="server" Width="744px" CssClass="Centered" >
                          <Columns>
                             <%--  <asp:BoundField DataField="AccountID" HeaderText="AccountID" SortExpression="AccountID" >
                       <ControlStyle Width="150px" />
                       <HeaderStyle Width="150px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>
                              <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>--%>
                          </Columns>
                           <EditRowStyle BackColor="#999999" />
                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                   <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                   <SortedAscendingCellStyle BackColor="#E9E7E2" />
                   <SortedAscendingHeaderStyle BackColor="#506C8C" />
                   <SortedDescendingCellStyle BackColor="#FFFDF8" />
                   <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                      </asp:GridView>
                 
             
         </div>
</asp:Content>

