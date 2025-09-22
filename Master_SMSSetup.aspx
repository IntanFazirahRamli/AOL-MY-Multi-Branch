<%@ Page Title="SMS Setup" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_SMSSetup.aspx.vb" Inherits="Master_SMSSetup" EnableEventValidation="false" ValidateRequest="false"  Culture="en-GB" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
     <%--  <script type="text/javascript" src="//tinymce.cachefly.net/4.0/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({ selector: 'textarea', width: 800 });
    </script>--%>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
 

    <div style="text-align: center">
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">SMS Setup</h3>
        <table style="width:100%;text-align:left;" border="0">
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
              
                <td colspan="2" style="text-align:left;" >
                  
       <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm()"/>
                
      <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" visible="false" />
                
                       </td>
     <td colspan="1" style="text-align: right">
      <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />

                </td>
                
            </tr>
                  <tr>
              <td colspan="3">
               <br /></td>
            </tr>
            <tr >
                <td colspan="3">
                      <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1000px">

     <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" 
                        BorderColor="#DEBA84" BorderStyle="None" Width="100%" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Module" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" AllowPaging="True">
                        <Columns>
                            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                                <ControlStyle Width="4%" />
                                <ItemStyle Width="4%" HorizontalAlign="Left"  />
                            </asp:CommandField>
                            <asp:BoundField DataField="Module" HeaderText="Module" ReadOnly="True" SortExpression="Module">
                                <ControlStyle Width="5%" />
                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Screen" HeaderText="Screen" SortExpression="Screen">
                                <ControlStyle Width="5%" />
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="SMSFormat" HeaderText="SMSFormat" SortExpression="SMSFormat" >
                            <ControlStyle Width="20%" />
                            <ItemStyle Width="20%" Wrap="True" />
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
    </asp:Panel>
                    </td>
                </tr>
          <tr>
              <td colspan="3"><br /></td>
          </tr>
         
              <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style1">Module</td>
                    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:80% "> <asp:TextBox ID="txtModule" runat="server" MaxLength="25" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox></td>
                    <td colspan="1"></td>
                    </tr>
               <tr>
                    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style1">Screen</td>
                    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:80% ">   <asp:TextBox ID="txtScreen" runat="server" MaxLength="25" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox></td>
                    <td colspan="1"></td>
                     </tr>
             
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style1">SMS Format</td>
                    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:80% ">   <asp:TextBox ID="txtSMSFormat" runat="server" MaxLength="500" Height="110px" Width="80%" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox></td>
                  <td colspan="1"></td>
                       </tr>
               <tr>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style1">&nbsp;</td>
                    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:80%; color: #000000;" class="auto-style1">Total No. of Words : <asp:Label ID="lblWordCount" runat="server" ForeColor="Black"></asp:Label></td>
                   <td colspan="1"></td> 
                     </tr>
             <tr>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style1">&nbsp;</td>
                    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:80% ">   &nbsp;</td>
                   <td colspan="1"></td> 
                     </tr>
           <tr>
               <td colspan="3" style="text-align:right"> 
                    <asp:Button ID="btnSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="SAVE" Width="100px"
                         OnClientClick="return DoValidation()" />
<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>

     

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" 
            SelectCommand="SELECT * FROM tblsmsSetup order by Module"></asp:SqlDataSource>

        <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>

</asp:Content>


