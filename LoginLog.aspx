<%@ Page Title="Event Log" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="LoginLog.aspx.vb" Inherits="LoginLog" %>
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

    var defaultTextModule = "Search Here for Module";
    function WaterMarkModule(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextModule;
        }
        if (txt.value == defaultTextModule && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

    var defaultTextStaff = "Search Here for Staff";
    function WaterMarkStaff(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextStaff;
        }
        if (txt.value == defaultTextStaff && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

</script>
         <style type="text/css">
      
           .roundbutton1 {
            border: 2px solid #a1a1a1;   
    background: #dddddd;   
    border-radius: 25px;
    height:40px;
    width:100px;
        }

          .modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 100px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 98px;
    width: 98px;
}
         .gridcell {
             word-break: break-all;
         }

          .tab {
              display: inline;
              visibility: visible;
          }
         .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}
     
    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:30%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
      .CellTextBox{
         font-family:'Calibri';
        color:black;
        text-align:left;
     padding-left:20px;
    }
    .CellFormatSearch{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:1%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              /*line-height:10px;*/
    }
      .CellTextBoxSearch{
         font-family:'Calibri';
        color:black;
        text-align:left;
     padding-left:20px;
    }
  
          </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
              <asp:controlBundle name="ModalPopupExtender_Bundle"/>
     
   </ControlBundles>
    </asp:ToolkitScriptManager>   
      <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Login Log</h3>
          </div>
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
              
                <td style="width:50%;text-align:left;">
               <%--   <asp:Button ID="btnADD" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="button" BackColor="#CFC6C0" Visible="true" />
<asp:Button ID="btnDelete" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm()"/>
                 --%>    <asp:Button ID="btnPrint" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="true" />
                  
                  
                       </td>
                <td style="text-align: right">
                   
                  <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />
                </td>
                
            </tr>
                <tr>
     <td colspan="2" style="text-align:right">
         <%-- <td style="width: 100px">
            <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Insert" />
        </td>--%>
        <table id="tablesearch" runat="server" style="border: 1px solid #CC3300; text-align:right; padding:2px; border-radius: 25px;width:100%; height:120px; background-color: #F3F3F3;">
            <tr>
                  <td class="CellFormatSearch" style="text-align:left;">Staffid</td>
                <td class="CellTextBoxSearch" style="width:6%">
                    <asp:TextBox ID="txtStaffID" runat="server" Width="85%"></asp:TextBox> <asp:ImageButton ID="btnImgStaff" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Middle" /></td>
                  <td class="CellFormatSearch" style="text-align:left;"> 
                       <asp:TextBox ID="txt" runat="server" Height="16px" MaxLength="50" Visible="false" Width="0px"></asp:TextBox>
                                    </td>
                <td class="CellTextBoxSearch" style="width:6%">
                    &nbsp;</td>
                 <td class="CellFormatSearch" style="text-align:left;">&nbsp;</td>
                
                <td class="CellTextBoxSearch" style="width:6%">
                    &nbsp;</td>
            </tr>
             <tr>
                  <td class="CellFormatSearch" style="text-align:left;">DateFrom</td>
                <td class="CellTextBoxSearch" style="width:6%">
                    <asp:TextBox ID="txtDateFrom" runat="server" Width="95%"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender></td>
                    <td class="CellFormatSearch" style="text-align:left;" rowspan="1">Date To</td>
                <td class="CellTextBoxSearch" style="width:6%" colspan="2" rowspan="1">
                    <asp:TextBox ID="txtDateTo" runat="server" Width="85%"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtDateTo" TargetControlID="txtDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender></td>
              
                  <td colspan="1" rowspan="2" style="text-align:center;vertical-align:middle">
                     <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="roundbutton1" />&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="roundbutton1" />&nbsp;
                    </td>
            </tr>
             <tr>
                  <td class="CellFormatSearch" style="text-align:left;">&nbsp;</td>
                <td class="CellTextBoxSearch" style="width:6%">
                    &nbsp;</td>
                
                </tr>
         
           
          
        </table>
         </td>
                    </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">

      <asp:GridView ID="GridView1" runat="server" CssClass="Centered" AutoGenerateColumns="False" DataKeyNames="Rcno" DataSourceID="SqlDataSource1" AllowPaging="True" Width="50%">
          <Columns>
              <asp:BoundField DataField="LoginID" HeaderText="LoginID" SortExpression="LoginID" >
             <ControlStyle Width="100px" />
                 <ItemStyle Width="100px" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LoggedOn" HeaderText="LoggedOn" SortExpression="LoggedOn" >
                     <ControlStyle Width="80px" />
                 <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
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
         </table>
      <br />
     <asp:Panel ID="pnlModule" runat="server" BackColor="White" Width="45%" Height="80%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Module</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlModuleClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpModule" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for AccountID or Client details" ForeColor = "Gray" onblur = "WaterMarkModule(this, event);" onfocus = "WaterMarkModule(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpModuleSearch" OnClick="btnPopUpModuleSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpModuleReset" OnClick="btnPopUpModuleReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
       
        <asp:TextBox ID="txtPopupModuleSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvModule" runat="server" CssClass="Centered" DataSourceID="SqlDSModule" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="500px" OnSelectedIndexChanged="gvModule_SelectedIndexChanged" PageSize="10">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                     <asp:BoundField DataField="Module" HeaderText="Module" SortExpression="Module" ReadOnly="True">
                       <ControlStyle Width="80PX" />
                  <HeaderStyle Width="80px" Wrap="False" />
                  <ItemStyle Width="80px" Wrap="False" />
                </asp:BoundField>
                
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                        <ControlStyle Width="120px" />
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" Wrap="True" />
                    </asp:BoundField>
                   
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
                 <asp:SqlDataSource ID="SqlDSModule" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblmodule where rcno <> 0"></asp:SqlDataSource>
 
         
        </div>
        
           </asp:Panel>
      <asp:Panel ID="pnlStaff" runat="server" BackColor="White" Width="45%" Height="80%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Staff</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpStaff" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for AccountID or Client details" ForeColor = "Gray" onblur = "WaterMarkStaff(this, event);" onfocus = "WaterMarkStaff(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpStaffSearch" OnClick="btnPopUpStaffSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpStaffReset" OnClick="btnPopUpStaffReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
       
        <asp:TextBox ID="txtPopupStaffSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvStaff" runat="server" CssClass="Centered" DataSourceID="SqlDSStaff" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="500px" OnSelectedIndexChanged="gvStaff_SelectedIndexChanged" PageSize="10">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                     <asp:BoundField DataField="StaffId" HeaderText="StaffId" SortExpression="StaffId" ReadOnly="True">
                       <ControlStyle Width="80PX" />
                  <HeaderStyle Width="80px" Wrap="False" />
                  <ItemStyle Width="80px" Wrap="False" />
                </asp:BoundField>
                
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                        <ControlStyle Width="120px" />
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" Wrap="True" />
                    </asp:BoundField>
                   
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
           <asp:SqlDataSource ID="SqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId,Name FROM tblstaff ORDER BY STAFFID"></asp:SqlDataSource>
   
         
        </div>
        
           </asp:Panel>

     <asp:ModalPopupExtender ID="mdlPopupModule" runat="server" CancelControlID="btnPnlModuleClose" PopupControlID="pnlModule" TargetControlID="btndummy" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
            <asp:Button ID="btndummy" runat="server" Text="Button" cssclass="dummybutton" />
     <asp:ModalPopupExtender ID="mdlPopupStaff" runat="server" CancelControlID="btnPnlStaffClose" PopupControlID="pnlStaff" TargetControlID="dummy" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
       <asp:Button ID="dummy" runat="server" cssclass="dummybutton" />
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblLogin order by loggedOn desc"></asp:SqlDataSource>
          
      <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
       <asp:TextBox ID="txtRowCount" runat="server" CssClass="dummybutton"></asp:TextBox>
</asp:Content>

