<%@ Page Title="Team Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_Team.aspx.vb" EnableEventValidation="FALSE" Inherits="Master_Team"  Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type="text/css">
    body
    {
        /*font-family: Arial;
        font-size: 10pt;*/
    }
    .GridPager a, .GridPager span
    {
        display: block;
        height: 15px;
        width: 15px;
        font-weight: bold;
        text-align: center;
        text-decoration: none;
    }
    .GridPager a
    {
        background-color: #f5f5f5;
        color: #969696;
        border: 1px solid #969696;
    }
    .GridPager span
    {
        background-color: #A1DCF2;
        color: #000;
        border: 1px solid #3AC0F2;
    }
  
               .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}
   
         .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:left;
        width:30%;
        /*table-layout:fixed;
        overflow:hidden;*/
        border-collapse: collapse;
        border-spacing: 0;
        line-height:10px;
    }
    .CellTextBox{
        
        color:black;
        text-align:left;
        /*width:20%;*/
        /*table-layout:fixed;
        overflow:hidden;*/
        /*border-collapse: collapse;
            border-spacing: 0;
            line-height:10px;*/
    }
      .roundbutton1 {
            border: 2px solid #a1a1a1;   
    background: #dddddd;   
    border-radius: 25px;
    height:30px;
    width:100px;
        }
            </style>
<script type="text/javascript">
 
    var defaultTextAsset = "Search Here";
    function WaterMark1(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextAsset;
        }
        if (txt.value == defaultTextAsset && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }
    function TabChanged(sender, e) {
        if (sender.get_activeTabIndex() == 1 || sender.get_activeTabIndex() == 2) {

            if (document.getElementById("<%=txtTeamID.ClientID%>").value == '') {
                $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                alert("Please select a Team to proceed.");
                return;
            }


        }
  
    }

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
                 width: 720,
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
	</script>--%>
 
      <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Team
        </h3>

          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                 <ControlBundles>
             <asp:controlBundle name="ListSearchExtender_Bundle"/>
                          <asp:controlBundle name="TabContainer_Bundle"/>                
                 <asp:controlBundle name="ModalPopupExtender_Bundle"/>
     
   </ControlBundles>
          </asp:ToolkitScriptManager>
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
                      <asp:Label ID="txtOrigCode" runat="server" Text="" ></asp:Label>
</td>

                     
            </tr>
            
            <tr>
              
                <td style="width:45%;text-align:left;">
                    
                     <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px"  Height="30px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True"  Height="30px" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px"  Height="30px" OnClientClick = "Confirm(); currentdatetime()"/>
                
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px"  Height="30px"/>
            
                            <asp:Button ID="btnFilter" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="100px"  Height="30px"/>
            <asp:Button ID="btnStatus" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="STATUS CHANGE" Width="140px" Height="30px" Enabled="False" />
                       </td>
                <td style="text-align: right">
            
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px"  Height="30px"/>

                </td>
                
            </tr>

          <tr>
               <td style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td>
                    <td style="text-align:right;width:35%">  <asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="RESET SEARCH"  />  <asp:TextBox ID="txtSearchTeam" runat="server" Width="370px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True"></asp:TextBox>      
                              &nbsp; <asp:Button ID="btnGoCust" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="GO" Width="50px" Height="30px" />
                   <asp:TextBox ID="txtSearchText" runat="server" CssClass="dummybutton" ></asp:TextBox>     </td>
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">

                     <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" CssClass="Centered" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="14px" HorizontalAlign="Center" Width="95%" Height="100%" AllowPaging="True" ForeColor="#333333" GridLines="Vertical" AllowSorting="True"> 
                                              <AlternatingRowStyle BackColor="White"/>
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="FALSE" >
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
               
                <asp:BoundField DataField="TeamID" HeaderText="Team ID" SortExpression="TeamID" >
                    <ItemStyle Width="100px" Wrap="False" />
                    </asp:BoundField>
                <asp:BoundField DataField="TeamName" HeaderText="Team Name" SortExpression="TeamName" >
                
                 <ItemStyle Width="150px" Wrap="False" />
                 </asp:BoundField>
                
                <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" SortExpression="DepartmentID">
                      <ItemStyle Width="150px" Wrap="False" />
              
                </asp:BoundField>
               
               
                 <asp:BoundField DataField="InChargeID" HeaderText="InChargeID" SortExpression="InChargeID" >
                      <ItemStyle Width="150px" Wrap="False" />
              
                </asp:BoundField>
               
                <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" SortExpression="Supervisor" >
                      <ItemStyle Width="150px" Wrap="False" />
              
                </asp:BoundField>

                 <asp:BoundField DataField="SecondInChargeID" HeaderText="SecondInCharge ID" SortExpression="SecondInChargeID" >
                          <ItemStyle Width="150px" Wrap="False" />
              
                </asp:BoundField>
               
                 <asp:BoundField DataField="Coordinator" HeaderText="Coordinator" SortExpression="Coordinator" />
               
                 <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" >
                          <ItemStyle Width="150px" Wrap="False" />
              
                </asp:BoundField>
               
                 <asp:BoundField DataField="VehNos" HeaderText="Veh Nos" SortExpression="VehNos" >
                      <ItemStyle Width="150px" Wrap="False" />
              
                </asp:BoundField>
               
                 <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" Visible="true" >
                          <ItemStyle Width="150px" Wrap="False" />
              
                </asp:BoundField>
               
               
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                   <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                <asp:BoundField DataField="AllocationZoneOnly" HeaderText="AllocationZoneOnly" SortExpression="AllocationZoneOnly" Visible="False" />
                    <asp:TemplateField HeaderText="OrigCode" InsertVisible="False" SortExpression="OrigCode" ControlStyle-CssClass="dummybutton" HeaderStyle-CssClass="dummybutton" ItemStyle-CssClass="dummybutton">
                     <EditItemTemplate>
                         <asp:Label ID="Label2" runat="server" Text='<%# Eval("OrigCode")%>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label2" runat="server" Text='<%# Bind("OrigCode")%>'></asp:Label>
                     </ItemTemplate>

<ControlStyle CssClass="dummybutton"></ControlStyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
                 </asp:TemplateField>
                 <asp:BoundField DataField="Location">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" />
            <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#00ccff" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    
                    </td>
                </tr>
        </table>
            <asp:TabContainer ID="tb1" runat="server" Font-Names="Calibri" Height="100%" Width="100%" CssClass="ajax__tab_xp" ActiveTabIndex="0" OnClientActiveTabChanged="TabChanged" >
 
     
      <asp:TabPanel runat="server" HeaderText="Team Information" ID="TabPanel1">
        <HeaderTemplate>
           Team Information
        </HeaderTemplate>
        
        <ContentTemplate>
         <table style="width:100%">
          <tr>
              <td colspan="2"><br /></td>
          </tr>
            <tr>
                     

                    <td class="CellFormatADM">Status</td>
                     

                    <td class="CellTextBoxADM">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="20%">
                            <asp:ListItem Value="Y">Active</asp:ListItem>
                            <asp:ListItem Value="N">InActive</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     

                 </tr>
              <tr>
                  <td class="CellFormatADM">Team ID<asp:Label ID="Label23" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                  </td>
                  <td class="CellTextBoxADM">
                      <asp:TextBox ID="txtTeamID" runat="server" Height="16px" MaxLength="25" Width="40%"></asp:TextBox>
                  </td>
             </tr>
              <tr>
                     <td class="CellFormatADM">Team Name</td>
                     <td class="CellTextBoxADM"> <asp:TextBox ID="txtTeamName" runat="server" MaxLength="100" Height="16px" Width="40%"></asp:TextBox></td>
              </tr>
         <tr>
                     <td class="CellFormatADM">DeptID</td>
                     <td class="CellTextBoxADM"> <asp:TextBox ID="txtDeptID" runat="server" MaxLength="10" Height="16px" Width="40%"></asp:TextBox></td>
              </tr>
         <tr>
                     <td class="CellFormatADM">InCharge ID</td>
                     <td class="CellTextBoxADM">  <asp:DropDownList ID="ddlInchargeID" runat="server" CssClass="chzn-select" DataSourceID="SqlDataSource2" DataTextField="StaffId" DataValueField="StaffId" Width="40%">
                               </asp:DropDownList>
    </td>
              </tr>
         <tr>
                     <td class="CellFormatADM">Second InchargeID</td>
                     <td class="CellTextBoxADM"><asp:TextBox ID="txtSecondInchargeID" runat="server" MaxLength="25" Height="16px" Width="40%"></asp:TextBox></td>
              </tr>
              <tr>
                     <td class="CellFormatADM">Supervisor</td>
                     <td class="CellTextBoxADM">  <asp:DropDownList ID="ddlSupervisor" runat="server" CssClass="chzn-select" DataSourceID="SqlDataSource2" DataTextField="StaffId" DataValueField="StaffId" Width="40%">
                               </asp:DropDownList>
    </td>
              </tr>
         <tr>
                     <td class="CellFormatADM">Coordinator</td>
                     <td class="CellTextBoxADM">  
                         <asp:TextBox ID="txtCoordinator" runat="server" Height="16px" MaxLength="10" Width="40%"></asp:TextBox>
                     </td>
                            
              </tr>
        <tr>
                     <td class="CellFormatADM">Notes</td>
                     <td class="CellTextBoxADM">  <asp:TextBox ID="txtNotes" runat="server" MaxLength="500" Height="50px" Width="84%" TextMode="MultiLine"></asp:TextBox></td>
              </tr>
        <tr>
                     <td class="CellFormatADM">VehNos</td>
                     <td class="CellTextBoxADM"><asp:TextBox ID="txtVehNos" runat="server" MaxLength="300" Height="50px" Width="84%" TextMode="MultiLine"></asp:TextBox></td>
              </tr>
                        <tr >
                              <td class="CellFormatADM">Location</td>
                              <td class="CellTextBoxADM">
                                  <asp:DropDownList ID="txtLocationId" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select" Width="84%">
                                      <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                         </tr>
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
    
      </ContentTemplate>
          </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="Team Asset" ID="TabPanel2">
        <HeaderTemplate>
            Team Asset
        </HeaderTemplate>
        
        <ContentTemplate>
                <table style="padding-top:5px;width:100%;">
                    <tr>
                         <td colspan="2" style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';">
             <asp:Label ID="txtAssetMode" runat="server" CssClass="dummybutton" ></asp:Label>
     </td>
                    </tr>

                    <tr style="vertical-align: middle">
                        
 <td colspan="2" style="text-align:left;">
                    <asp:Button ID="btnAssetAdd" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="90px" />
                
       <asp:Button ID="btnAssetEdit" runat="server" Font-Bold="True" Text="EDIT" Width="90px"  CssClass="button" BackColor="#CFC6C0" />
         
          <asp:Button ID="btnAssetDelete" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="90px" OnClientClick = "Confirm()"/>
   </td> 
                        
                         </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        
                        <td colspan="2" style="text-align:center">
                            <asp:GridView ID="GridView3" runat="server" DataSourceID="SqlDSTeamAsset" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="12pt" HorizontalAlign="Center" AllowSorting="True" Width="70%" CssClass="Centered">
                        <Columns>
                               <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="60px" />
                        <ItemStyle Width="60px" />
                    </asp:CommandField>
                            <asp:BoundField DataField="AssetNo" HeaderText="AssetNo" SortExpression="AssetNo" >
                               <HeaderStyle Width="250px" />
                                  <controlStyle Width="250px" />
                             
                               <ItemStyle Width="250px" Wrap="False" />
                               </asp:BoundField>
                            <asp:BoundField DataField="AssetRegNo" HeaderText="AssetRegNo" SortExpression="AssetRegNo" >
                               <HeaderStyle Width="250px" />
                                  <ControlStyle Width="250px" />
                               <ItemStyle Width="250px" Wrap="False" />
                               </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                                <HeaderStyle Width="300px" />
                                  <ControlStyle Width="300px" />
                               <ItemStyle Width="300px" />
                               </asp:BoundField>
                             <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" >
                               <HeaderStyle Width="150px" />
                                   <ControlStyle Width="150px" />
                               <ItemStyle Width="150px" Wrap="False" />
                               </asp:BoundField>
                            <asp:BoundField DataField="TeamSQLID" HeaderText="TeamSQLID" SortExpression="TeamSQLID" Visible="False" />
                               <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                   <EditItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                                   </EditItemTemplate>
                                   <ItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False"/>
                            <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False"/>
                            <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False"/>
                            <asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False"/>
                           </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#E4E4E4" />
            <SortedAscendingHeaderStyle BackColor="#000066" />
            <SortedDescendingCellStyle BackColor="#E4E4E4" />
            <SortedDescendingHeaderStyle BackColor="#000066" />
        </asp:GridView>
                            
                        </td>
                        

                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>

            
                  <tr> <td style="width:350px;text-align:right;" class="CellFormat">TeamID </td>
                        <td colspan="1" class="CellTextBox"><asp:Label ID="lblTeamID" runat="server" MaxLength="100" Height="18px" Width="40%" BackColor="#CCCCCC"></asp:Label>
                        </td> </tr>
                       
               
                  <tr>
                      <td colspan="2"><br /></td>
                  </tr>

                      <tr>
                        <td style="width:350px;text-align:right;" class="CellFormat">Asset No
                            <asp:Label ID="Label10" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                          </td>
                     
                        <td colspan="1" class="CellTextBox"> <asp:TextBox ID="txtAssetNo" runat="server" MaxLength="10" Height="16px" Width="40%"></asp:TextBox>

                           <asp:ImageButton ID="btnAsset" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>

 
                      <asp:modalpopupextender ID="mdlPopUpAsset" runat="server" CancelControlID="btnPnlAssetClose" PopupControlID="pnlPopUpAsset"
                                    TargetControlID="btndummy" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="" ></asp:modalpopupextender>
    <asp:Button ID="btndummy" runat="server" Text="" cssclass="dummybutton" />
</td>

                    </tr>

                   <tr>
                        <td style="width:350px;text-align:right;" class="CellFormat">AssetRegNo </td>
                     
                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtAssetRegNo" runat="server" MaxLength="100" Height="16px" Width="40%"></asp:TextBox>
                        </td>

                    </tr>
                     <tr>
                        <td style="width:350px;text-align:right;" class="CellFormat">Description
                                 </td>
                     
                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtAssetDesc" runat="server" MaxLength="100" Height="16px" Width="40%"></asp:TextBox>
                       </td>

                    </tr>

                   <tr>
                        <td style="width:350px;text-align:right;" class="CellFormat">Type </td>
                     
                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtType" runat="server" MaxLength="100" Height="16px" Width="40%"></asp:TextBox>
                        </td>

                    </tr>
                          <tr>
                        

               <td colspan="2" style="text-align:right;">   
                   
        <asp:Button ID="btnAssetSave" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>
                  
                    

 

                  
                    <asp:Button ID="btnAssetCancel" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   
                   
                   </td>
                        

           </tr>
                    
               </table>     
                        
                     <asp:SqlDataSource ID="SqlDSTeamAsset" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblteamasset  where TeamSQLid =@TeamSQLid order by AssetNo"  FilterExpression="TeamSQLid = '{0}'">
               <FilterParameters>
                    <asp:ControlParameter Name="TeamSQLid" ControlID="txtOrigCode" PropertyName="Text" Type="String" />
                </FilterParameters>
                         <SelectParameters>
                             <asp:ControlParameter ControlID="txtOrigCode" Name="@TeamSQLid" PropertyName="Text" />
                         </SelectParameters>
         </asp:SqlDataSource>
               <asp:TextBox ID="txtAssetRcNo" runat="server" Visible="False"></asp:TextBox>

           
            
            </ContentTemplate>

                    </asp:TabPanel>
                   <asp:TabPanel runat="server" HeaderText="Team Staff" ID="TabPanel3">
        <HeaderTemplate>
            Team Staff
        </HeaderTemplate>
        
        <ContentTemplate>
         
  <table style="padding-top:5px;width:100%;">
                    <tr>
                         <td colspan="2" style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';">
             <asp:Label ID="txtStaffMode" runat="server" CssClass="dummybutton" ></asp:Label>
     </td>
                    </tr>

                    <tr style="vertical-align: middle">
                        
 <td colspan="2" style="text-align:left;">
                    <asp:Button ID="btnStaffAdd" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="90px" />
                
       <asp:Button ID="btnStaffEdit" runat="server" Font-Bold="True" Text="EDIT" Width="90px"  CssClass="button" BackColor="#CFC6C0" />
         
          <asp:Button ID="btnStaffDelete" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="90px" OnClientClick = "Confirm()"/>
   </td> 
                        
                         </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        
                        <td colspan="2" style="text-align:center">
                            <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDSTeamStaff" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="12pt" HorizontalAlign="Center" AllowSorting="True">
                        <Columns>
                               <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>
                            <asp:BoundField DataField="StaffID" HeaderText="StaffID" SortExpression="StaffID" >
                               <HeaderStyle Width="150px" />
                               <ItemStyle Width="150px" Wrap="False" />
                               </asp:BoundField>
                            <asp:BoundField DataField="StaffName" HeaderText="StaffName" SortExpression="StaffName" >
                               <HeaderStyle Width="250px" />
                               <ItemStyle Width="250px" Wrap="False" />
                               </asp:BoundField>
                            <asp:BoundField DataField="Roles" HeaderText="Roles" SortExpression="Roles">
                               <ItemStyle Width="100px" />
                               </asp:BoundField>
                             <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" Visible="False" >
                               <HeaderStyle Width="150px" />
                               <ItemStyle Width="150px" Wrap="False" />
                               </asp:BoundField>
                            <asp:BoundField DataField="TeamSQLID" HeaderText="TeamSQLID" SortExpression="TeamSQLID" Visible="False" />
                               <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                   <EditItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                                   </EditItemTemplate>
                                   <ItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False"/>
                            <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False"/>
                            <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False"/>
                            <asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False"/>
                           </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#E4E4E4" />
            <SortedAscendingHeaderStyle BackColor="#000066" />
            <SortedDescendingCellStyle BackColor="#E4E4E4" />
            <SortedDescendingHeaderStyle BackColor="#000066" />
        </asp:GridView>
                            
                        </td>
                        

                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>

            
                  <tr> 
                      <td style="width:350px;text-align:right;" class="CellFormat">TeamID </td>
                        <td colspan="1" class="CellTextBox"><asp:Label ID="lblTeamID2" runat="server" MaxLength="100" Height="18px" Width="40%" BackColor="#CCCCCC"></asp:Label>
                        </td>

                  </tr>
                       
               
                  <tr>
                      <td colspan="2"><br /></td>
                  </tr>

                      <tr>
                        <td style="width:350px;text-align:right;" class="CellFormat">StaffID
                            <asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                          </td>
                     
                        <td colspan="1" class="CellTextBox"><asp:DropDownList ID="ddlTechID" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDSStaff" DataTextField="IDNAME" DataValueField="StaffId" Width="40%" AutoPostBack="True">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                                      </asp:DropDownList> </td>

                    </tr>

                   <tr>
                        <td style="width:350px;text-align:right;" class="CellFormat">StaffName </td>
                     
                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtStaffName" runat="server" MaxLength="100" Height="16px" Width="40%" ReadOnly="True"></asp:TextBox>
                        </td>

                    </tr>
                     <tr>
                        <td style="width:350px;text-align:right;" class="CellFormat">Roles
                                 </td>
                     
                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtRoles" runat="server" MaxLength="100" Height="16px" Width="40%"></asp:TextBox>
                       </td>

                    </tr>

                 
                          <tr>
                        

               <td colspan="2" style="text-align:right;">   
                   
        <asp:Button ID="btnStaffSave" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>
                  
                    

 

                  
                    <asp:Button ID="btnStaffCancel" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   
                   
                   </td>
                        

           </tr>
                    
               </table>     
                        
                     <asp:SqlDataSource ID="SqlDSTeamStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblteamstaff  where TeamSQLid =@TeamSQLid order by StaffID"  FilterExpression="TeamSQLid = '{0}'">
               <FilterParameters>
                    <asp:ControlParameter Name="TeamSQLid" ControlID="txtOrigCode" PropertyName="Text" Type="String" />
                </FilterParameters>
                         <SelectParameters>
                             <asp:ControlParameter ControlID="txtOrigCode" Name="@TeamSQLid" PropertyName="Text" />
                         </SelectParameters>
         </asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId, Name, CONCAT(StaffId, ' [', Name, ']') AS IDNAME FROM tblstaff ORDER BY StaffId"></asp:SqlDataSource>
               <asp:TextBox ID="txtStaffRcno" runat="server" Visible="False"></asp:TextBox>
                       
              </ContentTemplate>

                    </asp:TabPanel>
                </asp:TabContainer>

          
                   <asp:Panel ID="pnlPopUpAsset" runat="server" BackColor="White" Width="850px" Height="500px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Asset</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlAssetClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                                <asp:TextBox ID="txtPopUpAsset" runat="server" MaxLength="50" Height="16px" Width="300px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMarkAsset(this, event);" onfocus = "WaterMarkAsset(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpAssetSearch" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" visible="False"/>
                                   &nbsp;<asp:ImageButton ID="btnPopUpAssetReset" OnClick="btnPopUpAssetReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                            </td> 
                                      </tr>
                           </table>
      <br />
                           <asp:TextBox ID="txtPopupAssetSearch" runat="server" Visible="False"></asp:TextBox>
     <asp:GridView ID="gvAsset" runat="server" DataSourceID="SqlDSAsset" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="14px" HorizontalAlign="Center" AllowSorting="True" Width="100%" AllowPaging="True" CssClass="Centered">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
                <asp:BoundField DataField="AssetNo" HeaderText="Asset No" SortExpression="AssetNo" ItemStyle-Width="90px">
                 <ItemStyle Width="90px" Wrap="False" />
                </asp:BoundField>
               <asp:BoundField DataField="AssetRegNo" HeaderText="Asset RegNo" SortExpression="AssetRegNo" />
                <asp:BoundField DataField="AssetClass" HeaderText="Asset Class" SortExpression="AssetClass" Visible="false" />  
                  <asp:BoundField DataField="InChargeID" HeaderText="InChargeID" SortExpression="InChargeID" />
                            
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" Visible="false" />
                 <asp:BoundField DataField="OpStatus" HeaderText="OpStatus" SortExpression="OpStatus" Visible="false" />

                 <asp:BoundField DataField="Descrip" HeaderText="Description" SortExpression="Descrip" ItemStyle-Width="250px">
               
                 <ControlStyle Width="250px" />
               
                 <HeaderStyle Width="250px" />
                 <ItemStyle Width="250px" />
                 </asp:BoundField>
               
             <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
             
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
                            <asp:SqlDataSource ID="SqlDSAsset" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblasset where rcno<>0 order by assetno"></asp:SqlDataSource>

         
                           <br />
    </asp:Panel>
    

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblteam where Status &lt;&gt; 'N' order by teamId"></asp:SqlDataSource>

       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId FROM tblstaff ORDER BY STAFFID"></asp:SqlDataSource>
          <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="50px" Visible="false"></asp:TextBox>
          <asp:TextBox ID="txtFrom" runat="server" MaxLength="50" Height="16px" Width="50px" Visible="false"></asp:TextBox>
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
                    <asp:TextBox ID="txtActive" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>

    <asp:Panel ID="Panel2" runat="server" BackColor="White" Width="500px" Height="220" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto">
              <br /><br />
                     <table style="width:100%;padding-left:15px">
                       <tr>
                               <td class="CellFormat">
                                <asp:Label ID="Label48" runat="server" Text="Team ID" Width="80px"></asp:Label></td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchStaffID" runat="server" MaxLength="25" Height="16px" Width="90%" AutoPostBack="true"></asp:TextBox></td>
                       </tr>
                           <tr>
                               <td class="CellFormat">
                                <asp:Label ID="Label51" runat="server" Text="Team Name" Width="80px"></asp:Label></td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchName" runat="server" MaxLength="25" Height="16px" Width="90%" AutoPostBack="true"></asp:TextBox></td>
                       </tr>

                           <tr>
                               <td class="CellFormat">
                                <asp:Label ID="Label3" runat="server" Text="InCharge Id" Width="80px"></asp:Label></td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchInChargeId" runat="server" MaxLength="25" Height="16px" Width="90%" AutoPostBack="true"></asp:TextBox></td>
                       </tr>
                     

                                               
                     


                          <tr>
                              <td class="CellFormat">Show InActive Teams</td>
                              <td class="CellTextBox">
                                  <asp:CheckBox ID="chkInActive" runat="server" Text="" />
                              </td>
                         </tr>
                     


                         
                     


                          <tr>
                             <td colspan="2" style="text-align:center">&nbsp;</td>
                         </tr>

                         <tr>
                             <td colspan="2" style="text-align:center">
                                 <asp:Button ID="btnSearch" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Search" Width="100px" />
                                 <asp:Button ID="btnClose" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Cancel" Width="100px" />
                             </td>
                         </tr>

        </table>
           </asp:Panel>
 <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnClose" PopupControlID="Panel2" TargetControlID="btnFilter" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
       <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="550px" Height="350px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" class="auto-style6">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Status Change</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageStatus" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertStatus" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         <tr>
                             <td class="CellFormat">TeamID</td>
                             <td class="CellTextBox">
                                 <asp:Label ID="lblTeamIDStatus" runat="server" Width="70%" BackColor="#CCCCCC"></asp:Label></td>
                         </tr>
                          <tr>
                               <td class="CellFormat">ExistingStatus
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:Label ID="lblOldStatus" runat="server" width="70%" BackColor="#CCCCCC"></asp:Label></td>
                           </tr>
                          <tr>
                               <td class="CellFormat">NewStatus
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlNewStatus" runat="server" Width="71%" Height="25px" AutoPostBack="false">
                                      <asp:ListItem Value="Y">Active</asp:ListItem>
                            <asp:ListItem Value="N">InActive</asp:ListItem>              
                               </asp:DropDownList></td>
                           </tr>
                        
                                               
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                          
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnUpdateStatus" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Update Status" Width="120px"  OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelStatus" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>                

        </table>
           </asp:Panel>
     <asp:ModalPopupExtender ID="mdlPopupStatus" runat="server" CancelControlID="btnCancelStatus" PopupControlID="pnlStatus" TargetControlID="btndummyStatus" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
      <asp:Button ID="btndummyStatus" runat="server" cssclass="dummybutton" />
</asp:Content>

