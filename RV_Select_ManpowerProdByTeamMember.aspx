<%@ Page Title="Manpower Productivity by Team Member Report Selection" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_ManpowerProdByTeamMember.aspx.vb" Inherits="RV_Select_ManpowerProdTeamMemberDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     
    <script type="text/javascript">

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
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
               
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
          
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
              
                      
      
           <asp:controlBundle name="ModalPopupExtender_Bundle"/>
              <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>
     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">MANPOWER PRODUCTIVITY BY TEAM MEMBER REPORT</h4>
    
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

       <table style="WIDTH:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;line-height:25px;">
      <tr>
                      <td class="CellFormat" style="width:35%">ServiceDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;&nbsp;&nbsp;TO&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="16.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
                  </tr>
             <tr>
                             <td class="CellFormat">Service Staff
                               </td>
                              <td class="CellTextBox" colspan="1"> &nbsp;<asp:TextBox ID="txtIncharge" runat="server" Height="16px" Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                                  <asp:ImageButton ID="ImgBtnInCharge" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                                Height="22" Width="24" />
                                           
                                           
                                           


                                            &nbsp;<asp:DropDownList ID="ddlIncharge" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataSourceID="SqlDSIncharge" DataTextField="StaffId" DataValueField="StaffId" Width="35.5%" Height="25px" Visible="False" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>  
                             </td>
                           </tr>
             <tr>
                      <td class="CellFormat">
                         ContractGroup
                             </td>
                     <td class="CellTextBox" colspan="1"><%-- <asp:dropdownlist ID="txtServiceID" runat="server" Width="35.5%" DataSourceID="SqlDSService" DataTextField="ContractGroup" DataValueField="ContractGroup" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist>--%>
                             <cc1:dropdowncheckboxes ID="txtServiceID" runat="server" Width="35%" UseSelectAllNode = "true" DataSourceID="SqlDSService" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35%" DropDownBoxBoxWidth="35%" DropDownBoxBoxHeight="120" />                    
 </cc1:dropdowncheckboxes>
                     </td>
                          
                 
                          
                               </tr>
             <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox">  <%--<asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="35.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>--%>
                        <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35.5%" DropDownBoxBoxWidth="35.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                           </td>
                   
                  </tr>
              <tr>
                      <td class="CellFormat">Zone</td>
                    <td class="CellTextBox">
                          <cc1:dropdowncheckboxes ID="ddlZone" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSLocateGroup" DataTextField="locationgroup" DataValueField="locationgroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="35.5%" DropDownBoxBoxWidth="35.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                       
                         </td>
                 
                  </tr>

             <tr>
                      <td colspan="3" class="CellFormat" style="text-align:left;padding-left: 30%;padding-top:15px;">
                           <asp:CheckBox ID="chkTimeSheet" runat="server" Text="Get Data from the Service Record Time Sheet" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="false" />
             
                         </td>
                 
                  </tr>
            <tr>
                <td class="CellFormat">DisplayDate</td>
                    <td class="CellTextBox" colspan="2" style="text-align:left;padding-top:20px;padding-bottom:5px; text-align:left; width:400px; word-spacing: 20px; text-indent: 40px;">
                    <asp:RadioButtonList ID="rdbDisplayDate" runat="server" RepeatDirection="Horizontal" Width="350px">
                          <asp:ListItem>ScheduledDate</asp:ListItem>
                          <asp:ListItem Selected="true">ServiceDate</asp:ListItem>
                          
                      </asp:RadioButtonList></td>
              </tr>
                 <tr>
                    <td class="CellTextBox" colspan="3" style="padding-left: 20%;padding-top:20px;padding-bottom:20px; text-align:center; width:600px; word-spacing: 20px; text-indent: 40px;">
                      <asp:RadioButtonList ID="rdbSelect" runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal" Width="550px">
                          <asp:ListItem Selected="True">Detail</asp:ListItem>
                          <asp:ListItem>Summary</asp:ListItem>
                          <asp:ListItem Text="SummaryByTeamMember" Value="SummaryByTeamMember">SummaryByTeamMember</asp:ListItem>
                          
                      </asp:RadioButtonList></td>
              </tr>
              <tr style="text-align:center;">
          
            <td class="CENTERED" colspan="3" style="padding-top: 1%;text-align:center;">      
                           
                                   <asp:CheckBox ID="chkDistribution" runat="server" Text="Display based on Contract Distribution" TextAlign="Right" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Enabled="true" Checked="true" />
           <br />     </td>
                                    
                             </tr>
              <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick="currentdatetime()" Visible="false"/>
                      &nbsp;<asp:Button ID="btnExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true" OnClientClick="currentdatetime()"/>
              &nbsp;<asp:Button ID="btnEmailPDF" Visible="false" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Email PDF" Width="100px" OnClientClick="currentdatetime()"/>
          &nbsp;<asp:Button ID="btnEmailExcel" runat="server" Visible="false" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Email Excel File" Width="120px" OnClientClick="currentdatetime()"/>
          
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>
           <%--  <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick="currentdatetime()"/>
                                  &nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>--%>
           </table>

       <asp:TextBox ID="txtQuery" runat="server" Visible="false"></asp:TextBox>

         <asp:Panel ID="pnlPopUpStaff" runat="server" BackColor="White" Width="700px" Height="80%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
     <table><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Staff</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopupStaff" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkStaff(this, event);" onfocus = "WaterMarkStaff(this, event);" AutoPostBack="True">Search Here for Staff </asp:TextBox>
    <asp:ImageButton ID="btnPopUpStaffSearch" OnClick="btnPopUpStaffSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" /><asp:ImageButton ID="btnPopUpStaffReset" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
       <asp:TextBox ID="txtPopupStaffSearch" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr></table><div style="text-align: center; padding-left: 50px; padding-bottom: 5px;"><div class="AlphabetPager">
    <asp:Repeater ID="rptrStaff" runat="server" Visible="False"><ItemTemplate><asp:LinkButton ID="lbtnStaff" runat="server" Text='<%#Eval("Value")%>'  ForeColor="Black" /></ItemTemplate></asp:Repeater></div><br />
        
        <asp:GridView ID="gvStaff" runat="server" DataSourceID="SqlDSStaff" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
                                    CellPadding="2" GridLines="None" Width="80%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True"><ControlStyle Width="50px" />
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle Width="8%" /></asp:CommandField><asp:BoundField DataField="StaffId" HeaderText="Id" SortExpression="StaffId"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="NRIC" HeaderText="NRIC" SortExpression="NRIC"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div>

          </asp:Panel>


        <asp:ModalPopupExtender ID="mdlPopUpStaff" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlStaffClose" Enabled="True" PopupControlID="pnlPopUpStaff" TargetControlID="btnDummyStaff">
           </asp:ModalPopupExtender>
     <asp:Button ID="btnDummyStaff" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />

      <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblContractGroup order by ContractGroup"></asp:SqlDataSource>
   
        <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId from tblstaff where roles='TECHNICAL' ORDER BY StaffId"></asp:SqlDataSource>
 <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
</asp:Content>


