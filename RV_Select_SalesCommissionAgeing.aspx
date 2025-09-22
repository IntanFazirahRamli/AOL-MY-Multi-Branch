<%@ Page Title="Commission Ageing Report" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_SalesCommissionAgeing.aspx.vb" Inherits="RV_Select_SalesCommission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
       <script type="text/javascript">
           //function onCalendarShown() {

           //    var cal = $find("calendar1");
           //    //Setting the default mode to month
           //    cal._switchMode("months", true);

           //    //Iterate every month Item and attach click event to it
           //    if (cal._monthsBody) {
           //        for (var i = 0; i < cal._monthsBody.rows.length; i++) {
           //            var row = cal._monthsBody.rows[i];
           //            for (var j = 0; j < row.cells.length; j++) {
           //                Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
           //            }
           //        }
           //    }
           //}

           //function onCalendarHidden() {
           //    var cal = $find("calendar1");
           //    //Iterate every month Item and remove click event from it
           //    if (cal._monthsBody) {
           //        for (var i = 0; i < cal._monthsBody.rows.length; i++) {
           //            var row = cal._monthsBody.rows[i];
           //            for (var j = 0; j < row.cells.length; j++) {
           //                Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
           //            }
           //        }
           //    }

           //}

           //function call(eventElement) {
           //    var target = eventElement.target;
           //    switch (target.mode) {
           //        case "month":
           //            var cal = $find("calendar1");
           //            cal._visibleDate = target.date;
           //            cal.set_selectedDate(target.date);
           //            cal._switchMonth(target.date);
           //            cal._blur.post(true);
           //            cal.raiseDateSelectionChanged();
           //            break;
           //    }
           //}

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

    <%--   <script type="text/javascript">
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
    </script>--%>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
     
    <table style="width:100%;text-align:center;">
        <tr>
            <td colspan="2">
                  <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">COMMISSION AGEING REPORT</h4>
    
            </td>
        </tr>
           <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
        <tr style="width:100%;text-align:center;"><td colspan="2" style="text-align:center;">
      <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:center;padding-left:0%;line-height:20px;">
            <tr style="text-align:center;">
                      <td class="CellFormat" style="width:40%">CutOff Date</td>
                    <td colspan="2" class="CellTextBox" style="width:60%"><asp:TextBox ID="txtAcctPeriodFrom" runat="server" MaxLength="6" Height="16px" Width="35.5%"></asp:TextBox> 
                     <%--<asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtAcctPeriodFrom" OnClientShown="onCalendarShown2" OnClientHidden="onCalendarHidden2" TargetControlID="txtAcctPeriodFrom" Format="dd/MM/yyyy" Enabled="True"  BehaviorID="calendar2"></asp:CalendarExtender>--%>
                        
                         <asp:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="txtAcctPeriodFrom" TargetControlID="txtAcctPeriodFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                             
                       </td>
                    </tr>
           <tr style="text-align:center;">
                      <td class="CellFormat">Salesman</td>
                    <td colspan="2" class="CellTextBox"><asp:DropDownList ID="ddlSalesMan" runat="server" CssClass="chzn-select" DataSourceID="SqlDSSalesman" DataTextField="staffid" DataValueField="staffid" Width="35.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>              
                                 </td>

           </tr>
          
         <tr><td colspan="3"><br /><br /></td></tr>
          
              <tr>
                  <td colspan="3" style="text-align:center; ">
                      
                      <asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" Visible="false"/>
                      
                      <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="false"/>
                   <asp:Button ID="btnEmailComm" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Email Commission Ageing" Width="190px" Visible="true" OnClientClick="clearLabelValue();"/>
                        &nbsp;&nbsp;&nbsp;&nbsp  <asp:Button ID="btnCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
              </td>
              </tr>
        </table>

            </td>
           
        </tr></table>

       <asp:Panel ID="pnlConfirmMsg" runat="server" BackColor="White" Width="300px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
                 
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label7" runat="server" Text="Report Generation is in process.<br/><br/>The report will be emailed to you."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmOk" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="140px"/>
                                 <%--<asp:Button ID="btnCancelMsg" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>--%>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />
    <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
                <%-- <tr>
                      <td class="CellFormat">InvoiceType</td>
                    <td colspan="1" class="CellTextBox"> <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="chzn-select" Width="86%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                          <asp:ListItem Value="ARCN">CREDIT NOTE</asp:ListItem>
                                          <asp:ListItem Value="ARDN">DEBIT NOTE</asp:ListItem>
                                          <asp:ListItem Value="ARIN">SALES INVOICE</asp:ListItem>
                                     </asp:DropDownList>
                         </td> <td colspan="1" style="text-align:right; padding-right: 20px;">
                          &nbsp;</td>
                 
                  <td colspan="1" style="text-align:left;padding-left:20px;">
                          &nbsp;</td>
                
                  </tr>  --%>
           
               <%--  <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox" style="width:200px"><asp:TextBox ID="txtAcctPeriodTo" runat="server" MaxLength="6" Height="16px" Width="85.5%"></asp:TextBox>                        
                          <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtAcctPeriodTo" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden" TargetControlID="txtAcctPeriodTo" Format="yyyyMM" Enabled="True"  BehaviorID="calendar1"></asp:CalendarExtender> </td>
               --%>
                                  <%--</td>--%>
             
              
          
        <%--     <tr>
                      <td class="CellFormat">InvoiceDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtInvDateFrom" TargetControlID="txtInvDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtInvDateTo" TargetControlID="txtInvDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
            
            <tr>
                      <td class="CellFormat">AccountType</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlAccountType" runat="server" Width="86%" Height="20px" AutoPostBack="true" CssClass="chzn-select" >
                                    <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />
                                    <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                                    <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                </asp:DropDownList>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">AccountID</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtAccountID" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox><asp:ImageButton ID="btnClient" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="22px" ImageAlign="Middle"/>
                    </td>
                  
                  </tr>
                
               <tr>
                      <td class="CellFormat">AccountName</td>
                    <td class="CellTextBox" colspan="3" style="text-align:left;"> <asp:TextBox ID="txtCustName" runat="server" MaxLength="200" Height="16px" Width="95.5%"></asp:TextBox>
                              </td>
                  </tr>
             <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox"> 
                         <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="16.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="85.5%" DropDownBoxBoxWidth="85.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">Department</td>
                    <td class="CellTextBox"><asp:DropDownList ID="ddlContractGroup" runat="server" CssClass="chzn-select" DataSourceID="SqlDSContractGroup" DataTextField="contractgroup" DataValueField="contractgroup" Width="85.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>
                           </td>
                 
                  </tr>
            --%>
            
               <%--  <td class="CellFormat" style="text-align: right; line-height: 15px;">
                          OverdueDays<br />
                          (LessThan) </td>  <td class="CellTextBox">
                         <asp:TextBox ID="txtCommissionDays" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>Days          </td>
            --%>    
          
       <%-- <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="1050px" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
          <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpClient" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for AccountID or Client details" ForeColor = "Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpClientSearch" OnClick="btnPopUpClientSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpClientReset" OnClick="btnPopUpClientReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
       
        <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="700px" OnSelectedIndexChanged="gvClient_SelectedIndexChanged" PageSize="15">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" >
                     <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" ReadOnly="True">
                       <ControlStyle Width="5%" />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="5%" Wrap="False" />
                </asp:BoundField>
                
                    <asp:BoundField DataField="Name" HeaderText="Client" SortExpression="Name">
                        <ControlStyle Width="300px" />
                        <HeaderStyle Width="300px" />
                        <ItemStyle Width="300px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="False" />
                    </asp:BoundField>
                   
<%--                    <asp:BoundField DataField="Address1" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Telephone2" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                   <asp:BoundField DataField="Email" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="LocateGrp" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="CompanyGroup" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddBlock" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddNos" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddFloor" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddUnit" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddStreet" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddBuilding" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddCity" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddState" >                   
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddCountry" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddPostal" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Fax" >
                       <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                       <asp:BoundField DataField="Mobile" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Telephone" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ContID" Visible="False">
                        <ControlStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" Wrap="False" />
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
            <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT * FROM tblcompany WHERE rcno <> 0 order by name">
         
            </asp:SqlDataSource>
        </div>
    </asp:Panel>--%>

              <%--  <asp:modalpopupextender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient"
                                    TargetControlID="btndummyClient" BackgroundCssClass="modalBackground">
                                </asp:modalpopupextender>
                <asp:Button ID="btndummyClient" runat="server" Text="Button" CssClass="dummybutton" />
           


        <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="SqlDSSalesman" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select StaffId from tblStaff where Roles = 'SALES MAN'">
                       
            </asp:SqlDataSource>
   <%--   <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup">
                       
            </asp:SqlDataSource>--%>
    <asp:TextBox ID="txtModal" runat="server" Visible="false"></asp:TextBox>
         <asp:TextBox ID="txtCommissionDays" runat="server" MaxLength="10" Height="16px" Width="85.5%" Text="120" CssClass="dummybutton"></asp:TextBox>Days          </td>
        
</asp:Content>




