<%@ Page Title="Service Team Report Selection" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_ServiceTeam.aspx.vb" Inherits="RV_Select_ServiceTeam" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script type="text/javascript">

      var defaultTextTeam = "Search Here for Team, Incharge or ServiceBy";
    function WaterMarkTeam(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextTeam;
        }
        if (txt.value == defaultTextTeam && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }
        </script>
     <style type="text/css">
      
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
              padding-right:5px;
    }
          </style>
   
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
         <asp:controlBundle name="ListSearchExtender_Bundle"/>
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">SERVICE TEAM REPORT</h4>
    
                      <table style="width:100%;text-align:center;">
             <%--  <tr>
                     <td colspan="2" style="text-align:center">
                             <h5 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#800000;text-decoration:underline;">SERVICE RECORD LISTING</h5>
       
                     </td>
                 </tr>--%>
         
<%--            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>  

                      </td> 
            </tr>--%>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
          
          <%--  <tr>
                <td colspan="2" style="width:100%;text-align:RIGHT;"> 
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px" Height="30px" />
            </td>
            </tr>--%>
            </table>

       <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;line-height:20px;width:100%">
                  <tr>
             <td colspan="2"><br /></td>
                    
         </tr>
           
               <tr>
                             <td class="CellFormat" style="width:5%">ServiceTeam
                               </td>
                              <td class="CellTextBox" colspan="2"> <asp:TextBox ID="txtTeamFrom" runat="server" MaxLength="30" Height="16px" Width="30%"></asp:TextBox>
                                   <asp:ImageButton ID="btnTeam" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" />
                           <%-- </td>
                             <td class="CellFormat" style="width:35%">--%>&nbsp;&nbsp;To&nbsp;&nbsp;
                                  <asp:TextBox ID="txtTeamTo" runat="server" MaxLength="30" Height="16px" Width="30%"></asp:TextBox>
                                   <asp:ImageButton ID="btnTeamTo" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" />
                             </td>
                           </tr>
         
            <tr>
                      <td class="CellFormat">
                         InchargeID
                             </td>
                     <td class="CellTextBox" colspan="1">  <asp:DropDownList ID="ddlInchargeID" runat="server" CssClass="chzn-select" DataSourceID="SqlDataSource2" DataTextField="StaffId" DataValueField="StaffId" Width="30.5%" AppendDataBoundItems="true">
                              <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />  </asp:DropDownList>
                             <asp:ListSearchExtender ID="ddllsInchargeID" runat="server" TargetControlID="ddlInchargeID" PromptPosition="Bottom"></asp:ListSearchExtender></td>
                          
           
                               </tr>
              <tr>
                  <td class="CellFormat" style="height: 26px">2nd InchargeID</td>
                  <td class="CellTextBox" style="height: 26px">   <asp:DropDownList ID="ddl2ndInchargeID" runat="server" CssClass="chzn-select" DataSourceID="SqlDataSource2" DataTextField="StaffId" DataValueField="StaffId" Width="30.5%" AppendDataBoundItems="true">
                              <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />  </asp:DropDownList>
                             <asp:ListSearchExtender ID="ddlls2ndInchargeID" runat="server" TargetControlID="ddl2ndInchargeID" PromptPosition="Bottom"></asp:ListSearchExtender></td>
           
              </tr>
         <tr>
             <td colspan="2"><br /></td>
            
         </tr>
             <tr>
                    <td class="CellTextBox" colspan="3" style="text-align:center;padding-left:0px;">
                           <asp:Panel ID="Panel1" runat="server" CssClass="Centered" Height="95%" BorderStyle="Solid" BorderColor="#FF9900" BorderWidth="2px" Width="200px" HorizontalAlign="Center">
                                  <div style="font-size:15px;padding-top:10px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Select</div>
                   <div style="padding-left:20px;text-align:left;padding-bottom:20px;">
                               <asp:radiobuttonList ID="chkGrouping" runat="server" CellSpacing="2" CellPadding="2">
                                   <asp:ListItem Text="Team Member" Value="Team Member" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Team Vehicle" Value="Team Vehicle"></asp:ListItem>
                                    <asp:ListItem Text="Tools & Equipment" Value="Tools & Equipment"></asp:ListItem>
                                 
                               </asp:radiobuttonList></div>
                                        </asp:Panel>
                    </td>
                         <td></td> 
           </tr>
             <tr>
             <td colspan="2"><br /></td>
            
         </tr>
              <tr>
                  <td colspan="3" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                                  &nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>
          
        </table>

    
              <asp:Panel ID="pnlPopUpTeam" runat="server" BackColor="White" Width="700px" Height="500px" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">

         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Team</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpTeam" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for Team, Incharge or ServiceBy" ForeColor = "Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpTeamSearch" OnClick="btnPopUpTeamSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpTeamReset" OnClick="btnPopUpTeamReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
        <asp:TextBox ID="txtPopupTeamSearch" runat="server" Visible="False"></asp:TextBox>
            <br />

            <asp:GridView ID="gvTeam" CssClass="Centered" runat="server" DataSourceID="SqlDSTeam" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="2" GridLines="None" Width="600px" OnSelectedIndexChanged="gvTeam_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="TeamID" HeaderText="Id" SortExpression="TeamID">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TeamName" HeaderText="Name" SortExpression="TeamName">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InChargeId" HeaderText="InCharge Id" SortExpression="InChargeId">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="True" />
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
            <asp:SqlDataSource ID="SqlDSTeam" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblTeam where status <> 'N' order by TeamName"></asp:SqlDataSource>
        
    </asp:Panel>
             <asp:ModalPopupExtender ID="mdlPopUpTeam" runat="server" CancelControlID="btnPnlTeamClose" PopupControlID="pnlPopUpTeam"
                                    TargetControlID="btndummyTeam" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                  <asp:Button ID="btndummyTeam" runat="server" Text="Button" CssClass="dummybutton" />
         

          <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId FROM tblstaff ORDER BY STAFFID"></asp:SqlDataSource>
    <asp:TextBox ID="txtModal" runat="server" Visible="false"></asp:TextBox>
</asp:Content>

