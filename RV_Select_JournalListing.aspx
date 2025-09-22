<%@ Page Title="Journal Listing" Language="VB" MasterPageFile="~/MasterPage_Report.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="RV_Select_JournalListing.aspx.vb" Inherits="RV_Select_JournalListing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
       <script type="text/javascript">
           function onCalendarShown() {

               var cal = $find("calendar1");
               //Setting the default mode to month
               cal._switchMode("months", true);

               //Iterate every month Item and attach click event to it
               if (cal._monthsBody) {
                   for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                       var row = cal._monthsBody.rows[i];
                       for (var j = 0; j < row.cells.length; j++) {
                           Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                       }
                   }
               }
           }

           function onCalendarHidden() {
               var cal = $find("calendar1");
               //Iterate every month Item and remove click event from it
               if (cal._monthsBody) {
                   for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                       var row = cal._monthsBody.rows[i];
                       for (var j = 0; j < row.cells.length; j++) {
                           Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                       }
                   }
               }

           }

           function call(eventElement) {
               var target = eventElement.target;
               switch (target.mode) {
                   case "month":
                       var cal = $find("calendar1");
                       cal._visibleDate = target.date;
                       cal.set_selectedDate(target.date);
                       cal._switchMonth(target.date);
                       cal._blur.post(true);
                       cal.raiseDateSelectionChanged();
                       break;
               }
           }

           function onCalendarShown2() {

               var cal = $find("calendar2");
               //Setting the default mode to month
               cal._switchMode("months", true);

               //Iterate every month Item and attach click event to it
               if (cal._monthsBody) {
                   for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                       var row = cal._monthsBody.rows[i];
                       for (var j = 0; j < row.cells.length; j++) {
                           Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call2);
                       }
                   }
               }
           }

           function onCalendarHidden2() {
               var cal = $find("calendar2");
               //Iterate every month Item and remove click event from it
               if (cal._monthsBody) {
                   for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                       var row = cal._monthsBody.rows[i];
                       for (var j = 0; j < row.cells.length; j++) {
                           Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call2);
                       }
                   }
               }

           }

           function call2(eventElement) {
               var target = eventElement.target;
               switch (target.mode) {
                   case "month":
                       var cal = $find("calendar2");
                       cal._visibleDate = target.date;
                       cal.set_selectedDate(target.date);
                       cal._switchMonth(target.date);
                       cal._blur.post(true);
                       cal.raiseDateSelectionChanged();
                       break;
               }
           }


    </script>

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

           var defaultTextClient = "Search Here for AccountID or Client details";
           function WaterMarkClient(txt, evt) {
               if (txt.value.length == 0 && evt.type == "blur") {
                   txt.style.color = "gray";
                   txt.value = defaultTextClient;
               }
               if (txt.value == defaultTextClient && evt.type == "focus") {
                   txt.style.color = "black";
                   txt.value = "";
               }
           }

           var defaultTextGL = "Search Here for Ledger Code or Description";
           function WaterMarkGL(txt, evt) {
               if (txt.value.length == 0 && evt.type == "blur") {
                   txt.style.color = "gray";
                   txt.value = defaultTextGL;
               }
               if (txt.value == defaultTextGL && evt.type == "focus") {
                   txt.style.color = "black";
                   txt.value = "";
               }
           }

           var defaultTextStaff = "Search Here";
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
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
     
    <table style="width:100%;">
        <tr>
            <td colspan="2">
                  <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">JOURNAL LISTING REPORT</h4>
    
            </td>
        </tr>
           <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
        <tr style="width:100%"><td style="width:90%">
      <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:50px;line-height:20px;">
             
             <tr>
                      <td class="CellFormat">AccountingPeriod</td>
                    <td class="CellTextBox" style="width:200px"><asp:TextBox ID="txtAcctPeriodFrom" runat="server" MaxLength="6" Height="16px" Width="85.5%"></asp:TextBox> 
                     <asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtAcctPeriodFrom" OnClientShown="onCalendarShown2" OnClientHidden="onCalendarHidden2" TargetControlID="txtAcctPeriodFrom" Format="yyyyMM" Enabled="True"  BehaviorID="calendar2"></asp:CalendarExtender> 
                       </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox" style="width:200px"><asp:TextBox ID="txtAcctPeriodTo" runat="server" MaxLength="6" Height="16px" Width="85.5%"></asp:TextBox>                        
                          <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtAcctPeriodTo" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden" TargetControlID="txtAcctPeriodTo" Format="yyyyMM" Enabled="True"  BehaviorID="calendar1"></asp:CalendarExtender> </td>
               
                                  <%--</td>--%>
             
                  </tr>
          
             <tr>
                      <td class="CellFormat">JournalDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtInvDateFrom" TargetControlID="txtInvDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtInvDateTo" TargetControlID="txtInvDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
              <tr>
                     

                    <td class="CellFormatADM">LedgerCode</td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:TextBox ID="txtGLFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox><asp:ImageButton ID="btnGL1" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="22px" ImageAlign="Middle"/>
   </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtGLTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox><asp:ImageButton ID="btnGL2" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="22px" ImageAlign="Middle"/>
  
                            </td>
                     

                 </tr>
            <tr>
                   
                      <td class="CellFormat" style="text-align: right; ">PostStatus</td>
                    <td class="CellTextBox"><asp:DropDownList ID="ddlStatus" runat="server" Width="85.5%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                  <asp:ListItem Value="O">O - Open</asp:ListItem>
                         <asp:ListItem Value="P">P - Posted</asp:ListItem>
                                   <asp:ListItem Value="V">V - Void</asp:ListItem> 
                                   <asp:ListItem Value="C">C - Closed/Completed</asp:ListItem>
                                    
                               </asp:DropDownList>          <asp:CheckBox ID="chkGLBreakdown" runat="server" Text="Show GL Breakdown" TextAlign="Left" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" VISIBLE="FALSE"/>                        
                                 
                                 </td>
                
                  <td class="CellFormat">GLStatus</td>
                    <td class="CellTextBox">   <asp:TextBox ID="txtGLStatus" runat="server" MaxLength="1" Visible="TRUE" Height="16px" Width="85.5%"></asp:TextBox>                        
                            <br />           <asp:CheckBox ID="chkVoid" runat="server" Text="Include Void" TextAlign="Left" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" />
              
                                 </td>
                  </tr>
            
                <tr>
                      <td class="CellFormat">Reference</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtReference" runat="server" MaxLength="20" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 </td>
                 <td class="CellFormat" style="text-align: right; ">SubCode</td>
                    <td class="CellTextBox"><asp:DropDownList ID="ddlSubCode" runat="server" Width="85.5%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                  <asp:ListItem Value="SERVICE">SERVICE</asp:ListItem>
                         <asp:ListItem>STOCK</asp:ListItem>
                                   <asp:ListItem>OTHERS</asp:ListItem> 
                                    
                               </asp:DropDownList>     
                                 </td>
                  </tr>
          <tr>
                      <td class="CellFormat">Comments</td>
                    <td class="CellTextBox" colspan="3" style="text-align: left;"><asp:TextBox ID="txtComments" runat="server" MaxLength="10" Height="16px" Width="95.5%"></asp:TextBox>  </td>
               
             
                  </tr>
       
          <%--   <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox"> 
                         <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="16.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="85.5%" DropDownBoxBoxWidth="85.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">LocationGroup</td>
                    <td class="CellTextBox"><asp:DropDownList ID="ddlLocateGrp" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSLocateGroup" DataTextField="locationgroup" DataValueField="locationgroup" Width="86%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                          
                           </td>
                 
                  </tr>--%>
           
            
            <%-- <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Sort By</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>--%>
     <%--        <tr>
                      <td class="CellFormat">SortBy</td>
                  
                                <td class="CellTextBox" rowspan="2"><asp:ListBox ID="lstSort1" runat="server" width="80%" Rows="4" SelectionMode="Multiple">
                                    <asp:ListItem Text="JournalNo" Value="tblsalesdetail.InvoiceNumber">JournalNo</asp:ListItem>
                                      <asp:ListItem Text="JournalDate" Value="tblsales.SalesDate">JournalDate</asp:ListItem>
                                 
                                    <asp:ListItem Text="AccountName" Value="tblsales.CustName">Account Name</asp:ListItem>
                                
                                    </asp:ListBox></td>
                                <td colspan="1" class="CellFormat" style="text-align: right; padding-right: 50px;"><asp:Button ID="btnSort1" runat="server" Text=">>" /></td>
                                 <td rowspan="2" class="CellTextBox"><asp:ListBox ID="lstSort2" runat="server" width="80%" Rows="4" SelectionMode="Multiple">
                                     
                                                 </asp:ListBox></td>
                            </tr>
                            <tr><td></td>
<td colspan="1" style="text-align: right; padding-right: 50px;"><asp:Button ID="btnSort2" runat="server" Text="<<" /></td>
                            </tr>--%>
                      <%--  </table>
                         
                                    </td>
                  
                  </tr>--%>
                   <%--  <tr>
                          <td class="CellFormat">DisplayBy</td>
            <td colspan="3" style="padding-left: 2%; padding-top: 1%">
                <asp:Panel ID="Panel2" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="80%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 10px;">
                        <asp:RadioButtonList ID="rbtnSelect" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Vertical" RepeatColumns="4" Width="90%">
                               <asp:ListItem Text="Client" Value="1" Selected="true"></asp:ListItem>
                              <asp:ListItem Text="CompanyGroup" Value="2"></asp:ListItem>                       
                        
                             <asp:ListItem Text="SalesPerson" Value="3"></asp:ListItem>
                            <asp:ListItem Text="GL Code" Value="4"></asp:ListItem>
                              <asp:ListItem Text="InvoiceNo" Value="5"></asp:ListItem>                       
                        
                             <asp:ListItem Text="ServiceID" Value="6"></asp:ListItem>
                            <asp:ListItem Text="BillingFrequency" Value="7"></asp:ListItem>
                           
                          </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>
        </tr>--%>
         <tr>
                 <td class="CellFormat">ReportType</td>
            <td colspan="3" style="text-align: left;padding-left:20px ">
                <asp:Panel ID="Panel1" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="98%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 10px;">
                        <asp:RadioButtonList ID="rbtnSelectDetSumm" runat="server" CellSpacing="3" CellPadding="3" RepeatDirection="horizontal" Width="98%">
                             <asp:ListItem Text="Summary" Value="1" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Detail" Value="2"></asp:ListItem>
                             <asp:ListItem Text="Ledger Detail" Value="3"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>
        </tr>
      
              <tr>
                  <td colspan="4" style="text-align:center; ">
                      
                      <asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                      
                      <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                          
                        </td>
                  <td style="text-align:LEFT">   <asp:Button ID="btnCancel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
              </td>
              </tr>
        </table>

            </td>
            <td></td>
        </tr></table>
    <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
     

           
         <asp:Panel ID="pnlPopUpGL" runat="server" BackColor="White" Width="1000px" Height="700px" BorderColor="#003366" BorderWidth="1px"    HorizontalAlign="Left">

                     <table>
           <tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Select Ledger Code</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlGLClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                           
         <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopUpGL" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkGL(this, event);" onfocus = "WaterMarkGL(this, event);" AutoPostBack="True">Search Here for Ledger Code or Description</asp:TextBox>
    <asp:ImageButton ID="btnPopUpGLSearch"  runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" /><asp:ImageButton ID="btnPopUpGLReset"  runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
                               <asp:TextBox ID="txtPopupGLSearch" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr>


        </table>

     
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="GrdViewGL" runat="server" DataSourceID="SqlDSGL" OnRowDataBound = "OnRowDataBoundgGL" OnSelectedIndexChanged = "OnSelectedIndexChangedgGL" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="800px"  Font-Size="15px" PageSize="8">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="COACode" HeaderText="Code" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="GLType" >
                    <ControlStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                        <HeaderStyle CssClass="dummybutton" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDSGL" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="Select COACode, Description, GLType from tblchartofaccounts order by COACode">
            </asp:SqlDataSource>
      
              </div>
    </asp:Panel>
       <asp:modalpopupextender ID="mdlPopupGLCode" runat="server" CancelControlID="btnPnlGLClose" PopupControlID="pnlPopUpGL"
                                    TargetControlID="btndummyGL" BackgroundCssClass="modalBackground">
                                </asp:modalpopupextender>
                <asp:Button ID="btndummyGL" runat="server" Text="Button" CssClass="dummybutton" />
          
       <asp:SqlDataSource ID="SqlDSBillingCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(ProductCode, ' - ', Description) AS ItemCode,ProductCode FROM tblbillingproducts order by Description "></asp:SqlDataSource>
                     
    <asp:TextBox ID="txtModal" runat="server" Visible="false"></asp:TextBox>
</asp:Content>



