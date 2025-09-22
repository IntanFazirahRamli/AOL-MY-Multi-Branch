<%@ Page Title="Outstanding Invoice Details By Due Date" Language="VB" MasterPageFile="~/MasterPage_Report.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="RV_Select_OutstandingInvoiceDetailsByDueDate.aspx.vb" Inherits="RV_Select_OutstandingInvoiceDetailsByDueDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>




<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>

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

           function clearLabelValue() {
               document.getElementById("<%= lblAlert.ClientID%>").innerText = "";

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


     <style>
            .loading-panel {
                background: rgba(0, 0, 0, 0.2) none repeat scroll 0 0;
                /*background: #fff none repeat scroll 0 0;*/
                position: relative;
                width: 100%;
            }

            .loading-container {
                /*background: rgba(49, 133, 156, 0.4) none repeat scroll 0 0;*/
                background: rgba(192,192,192,0.3) none repeat scroll 0 0;
                color: #fff;
                font-size: 90px;
                height: 100%;
                left: 0;
                padding-top: 15%;
                position: fixed;
                text-align: center;
                top: 0;
                width: 100%;
                z-index: 999999;
            }
       
         .auto-style1 {
             width: 268435424px;
         }
         .auto-style2 {
             color: black;
             text-align: left;
             font-family: Calibri;
             width: 268435424px;
             padding-left: 20px;
         }
         .auto-style3 {
             font-size: 15px;
             font-weight: bold;
             font-family: Calibri;
             color: black;
             text-align: right;
             width: 268435424px;
             border-collapse: collapse;
             border-spacing: 0;
             line-height: 10px;
         }
       
        </style>
     <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>

      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
     
    <table style="width:100%;">
        <tr>
            <td colspan="2">
                  <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">OUTSTANDING INVOICE DETAILS REPORT</h4>
    
            </td>
        </tr>
           <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
        <tr style="width:100%"><td style="width:90%">
      <table border="0" style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:50px;line-height:20px;">
                 <tr>
                      <td class="CellFormat">InvoiceType</td>
                    <td colspan="1" class="CellTextBox"> <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="chzn-select" Width="86%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                          <asp:ListItem Value="ARCN">CREDIT NOTE</asp:ListItem>
                                          <asp:ListItem Value="ARDN">DEBIT NOTE</asp:ListItem>
                                          <asp:ListItem Value="ARIN">SALES INVOICE</asp:ListItem>
                                     </asp:DropDownList>
                         </td> <td colspan="1" style="text-align:right; padding-right: 5px;">
                          Selected Location</td>
                 
                  <td colspan="2" style="text-align:left;padding-left:20px;" class="auto-style1">
                          <asp:TextBox ID="txtLocation" runat="server" MaxLength="10" Height="16px" Width="85.5%" ReadOnly="True"></asp:TextBox>
                           <asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="REFRESH" />
                  </td>
                
                  </tr>  
             <tr>
                      <td class="CellFormat">AccountingPeriod</td>
                    <td class="CellTextBox" style="width:200px"><asp:TextBox ID="txtAcctPeriodFrom" runat="server" MaxLength="6" Height="16px" Width="85.5%"></asp:TextBox> 
                     <asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtAcctPeriodFrom" OnClientShown="onCalendarShown2" OnClientHidden="onCalendarHidden2" TargetControlID="txtAcctPeriodFrom" Format="yyyyMM" Enabled="True"  BehaviorID="calendar2"></asp:CalendarExtender> 
                       </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="auto-style2" colspan="2"><asp:TextBox ID="txtAcctPeriodTo" runat="server" MaxLength="6" Height="16px" Width="85.5%"></asp:TextBox>                        
                          <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtAcctPeriodTo" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden" TargetControlID="txtAcctPeriodTo" Format="yyyyMM" Enabled="True"  BehaviorID="calendar1"></asp:CalendarExtender> </td>
               
                                  <%--</td>--%>
             
                  </tr>
          
             <tr>
                      <td class="CellFormat">BilledDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtInvDateFrom" TargetControlID="txtInvDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="auto-style2" colspan="2"><asp:TextBox ID="txtInvDateTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtInvDateTo" TargetControlID="txtInvDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
             <tr>
                      <td class="CellFormat">DueDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtDueDateFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDueDateFrom" TargetControlID="txtDueDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="auto-style2" colspan="2"><asp:TextBox ID="txtDueDateTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtDueDateTo" TargetControlID="txtDueDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
              <tr>
                     

                    <td class="CellFormatADM">LedgerCode</td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:TextBox ID="txtGLFrom" runat="server" MaxLength="10" Height="16px" Width="85%"></asp:TextBox><asp:ImageButton ID="btnGL1" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="22px" ImageAlign="Top"/>
   </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="auto-style2" colspan="2"><asp:TextBox ID="txtGLTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox><asp:ImageButton ID="btnGL2" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="22px" ImageAlign="Top"/>
  
                            </td>
                     

                 </tr>
          <%--  <tr>
                      <td class="CellFormat">InvoiceNos</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvoiceNoFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvoiceNoTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 </td>
               
             
                  </tr>
          <tr>
                      <td class="CellFormat">OurRef</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtOurRef" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                </td>
                 <td class="CellFormat" style="text-align: right; ">YourRef</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtYourRef" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 </td>
               
             
                  </tr>--%>
          <tr>
                      <td class="CellFormat">StaffID/Incharge</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtIncharge" runat="server" MaxLength="10" Height="16px" Width="85%"></asp:TextBox><asp:ImageButton ID="btnImgIncharge" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="22px" ImageAlign="Top"/>                        
                                 </td>
                 <td class="CellFormat" style="text-align: right; ">Salesman</td>
                    <td class="auto-style2" colspan="2"><asp:DropDownList ID="ddlSalesMan" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="StaffId" DataValueField="StaffId" Height="25px" Width="85.5%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                                 </td>
               
             
                  </tr>
        <%--  <tr>
                      <td class="CellFormat">Comments</td>
                    <td class="CellTextBox" colspan="3" style="text-align: left;"><asp:TextBox ID="txtComments" runat="server" MaxLength="10" Height="16px" Width="95.5%"></asp:TextBox>  </td>
               
             
                  </tr>--%>
            <tr>
                      <td class="CellFormat">AccountType</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlAccountType" runat="server" Width="86%" Height="20px" AutoPostBack="true" CssClass="chzn-select" >
                                    <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />
                                    <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                                    <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                </asp:DropDownList>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">AccountID</td>
                    <td class="auto-style2" colspan="2"><asp:TextBox ID="txtAccountID" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox><asp:ImageButton ID="btnClient" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="22px" ImageAlign="Top"/>
                    </td>
                  
                  </tr>
                
               <tr>
                      <td class="CellFormat">AccountName</td>
                    <td class="CellTextBox" colspan="4" style="text-align:left;"> <asp:TextBox ID="txtCustName" runat="server" MaxLength="200" Height="16px" Width="94%"></asp:TextBox><asp:ImageButton ID="btnClientName" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="22px" ImageAlign="Top"/>
                              </td>
                  </tr>
             <tr>
                      <td class="CellFormat">CompanyGroup</td>
                    <td class="CellTextBox">  <%--<asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="25.5%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList>--%>
                         <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="16.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="85.5%" DropDownBoxBoxWidth="85.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">LocationGroup</td>
                    <td class="auto-style2" colspan="2"><asp:DropDownList ID="ddlLocateGrp" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSLocateGroup" DataTextField="locationgroup" DataValueField="locationgroup" Width="86%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                          
                           </td>
                 
                  </tr>

           <tr>
                      <td class="CellFormat">Contract Group</td>
                    <td class="CellTextBox"><asp:DropDownList ID="ddlContractGroup" runat="server" AppendDataBoundItems="True" Width="85.5%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                                 </td>
                 <td class="CellFormat" style="text-align: right; "></td>
                    <td class="auto-style2" colspan="2">                        
                                 </td>
                  </tr>


              <tr>
                      <td class="CellFormat">Terms</td>
                    <td class="CellTextBox"><asp:DropDownList ID="ddlTerms" runat="server" AppendDataBoundItems="True" Width="85.5%" Height="25px" DataSourceID="SqlDSTerms" DataTextField="Terms" DataValueField="Terms" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                                 </td>
                 <td class="CellFormat" style="text-align: right; ">GLStatus</td>
                    <td class="auto-style2" colspan="2"><asp:TextBox ID="txtGLStatus" runat="server" MaxLength="1" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 </td>
                  </tr>
             <tr>
                      <td class="CellFormat">
                          
                      </td>
                    <td class="CellTextBox">
                        <asp:CheckBox ID="chkUnpaidBal" runat="server" Text="Include UnPaid Bal < 0" TextAlign="Left" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" Visible="False" />
                    </td>
                 <td colspan="3" class="CellFormat" style="text-align: center; ">
                          &nbsp;</td>
                    <td class="CellTextBox">
                          &nbsp;</td>
                  </tr>
             <tr style="display:none">
                      <td class="CellFormat">PrintDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtPrintDate" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>         
                             <asp:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="txtPrintDate" TargetControlID="txtPrintDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                 </td>
                 <td colspan="3" class="CellFormat" style="text-align: center; ">
                          <asp:CheckBox ID="chkGLBreakdown" runat="server" Text="Show GL Breakdown" TextAlign="Left" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" VISIBLE="FALSE"/>                        
                          </td>
                    <td class="CellTextBox">
                          &nbsp;</td>
                  </tr>
            <tr>
                      <td class="CellFormat">CutOffDate<asp:Label ID="Label3" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            <asp:CheckBox ID="chkCheckCutOff" runat="server" AutoPostBack="True" /></td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCutOffDate" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>         
                             <asp:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="txtCutOffDate" TargetControlID="txtCutOffDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                                 </td>
                 <td colspan="2" class="CellFormat" style="text-align: right; ">
                          <asp:CheckBox ID="chkIncludeCompanyGroupInfo" runat="server" Text="Include Company Group Information" TextAlign="Left" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" />
                         </td>
                 <td colspan="2" class="CellFormat" style="text-align: right; ">
                          &nbsp;</td>
                  </tr>
        <%--   <tr>
                      <td class="CellFormat">CostCode</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCostCode" runat="server" MaxLength="20" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 </td>
                 <td class="CellFormat" style="text-align: right; ">ItemCode</td>
                    <td class="CellTextBox">
                        <asp:DropDownList ID="ddlItemCode" runat="server" DataSourceID="SqlDSBillingCode" AppendDataBoundItems="true" DataTextField="ItemCode" Height="20px" Width="85.5%" DataValueField="ProductCode"  >
                            <asp:ListItem Text="--SELECT--" Value="-1" /> </asp:DropDownList>                        
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
            --%>
            <%-- <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Sort By</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>--%>
         <%--    <tr>
                      <td class="CellFormat">SortBy</td>
                  
                                <td class="CellTextBox" rowspan="2"><asp:ListBox ID="lstSort1" runat="server" width="80%" Rows="4" SelectionMode="Multiple">
                                    <asp:ListItem Text="InvoiceNo" Value="tblsalesdetail.InvoiceNumber">InvoiceNo</asp:ListItem>
                                      <asp:ListItem Text="InvoiceDate" Value="tblsales.SalesDate">InvoiceDate</asp:ListItem>
                                 
                                    <asp:ListItem Text="Account Name" Value="tblsales.CustName">Account Name</asp:ListItem>
                                
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
            <td colspan="2" style="text-align: left;padding-left:20px ">
                <asp:Panel ID="Panel1" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="95%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 10px; width: 312px;">
                        <asp:RadioButtonList ID="rbtnSelectDetSumm" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="horizontal" Width="80%">
                             <asp:ListItem Text="Detail" Value="1" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Summary" Value="2"></asp:ListItem>
                            
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>

               <td colspan="2" class="auto-style3" style="text-align: right; ">
                          <asp:CheckBox ID="chkIncludeReceiptDetails" runat="server" Text="Include Receipt Details" TextAlign="Left" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" />
                         </td>
               
        </tr>
      
         <tr>
                 <td class="CellFormat">Format</td>
            <td colspan="2" style="text-align: left;padding-left:20px ">
                
             <asp:Panel ID="Panel2" runat="server" Height="90%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="100%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 10px;">
                        <asp:RadioButtonList ID="rbtnSelectFormat" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="horizontal" Width="100%">
                             <asp:ListItem Text="Format-1" Value="1" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Format-2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Format-3" Value="3"></asp:ListItem>
                             <asp:ListItem Text="Format-4" Value="4"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>
               <td colspan="2" class="auto-style3" style="text-align: right; ">
                          <asp:CheckBox ID="chkBillingFreq" runat="server" Text="Include Billing Freq" TextAlign="Left" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" />
                   <br />   <asp:CheckBox ID="chkServiceFreq" runat="server" Text="Include Service Freq" TextAlign="Left" Font-Bold="True" Font-Names="Calibri" ForeColor="Black" />
                            
               </td>
        </tr>
      
              <tr>
                  <td colspan="2" style="text-align:right; ">
                      
                      <asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" Visible="False" />
                      
                      <asp:Button ID="btnExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true" OnClientClick="clearLabelValue();"/>
                             <asp:Button ID="btnEmailOSInv" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Email Aging" Width="120px" Visible="true" OnClientClick="clearLabelValue();"/>
                   
                        </td>
                  <td colspan="4" style="text-align:LEFT">   <asp:Button ID="btnCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
              </td>
              </tr>
        </table>

            </td>
            <td></td>
        </tr></table>
    <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
     <asp:TextBox ID="txtQueryRecv" runat="server" CssClass="dummybutton" ></asp:TextBox>
      <asp:TextBox ID="txtQueryRecv1" runat="server" CssClass="dummybutton" ></asp:TextBox>
        <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="950px" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
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
                    </asp:BoundField>--%>
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
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
         
            </asp:SqlDataSource>
        </div>
    </asp:Panel>

                <asp:modalpopupextender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient"
                                    TargetControlID="btndummyClient" BackgroundCssClass="modalBackground">
                                </asp:modalpopupextender>
                <asp:Button ID="btndummyClient" runat="server" Text="Button" CssClass="dummybutton" />
           

           
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
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
      
              </div>
    </asp:Panel>
       <asp:modalpopupextender ID="mdlPopupGLCode" runat="server" CancelControlID="btnPnlGLClose" PopupControlID="pnlPopUpGL"
                                    TargetControlID="btndummyGL" BackgroundCssClass="modalBackground">
                                </asp:modalpopupextender>
                <asp:Button ID="btndummyGL" runat="server" Text="Button" CssClass="dummybutton" />
               <asp:Panel ID="pnlStaff" runat="server" BackColor="White" Width="45%" Height="80%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Staff</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpStaff" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMarkStaff(this, event);" onfocus = "WaterMarkStaff(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpStaffSearch" OnClick="btnPopUpStaffSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpStaffReset" OnClick="btnPopUpStaffReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
       
        <asp:TextBox ID="txtPopupStaffSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvStaff" runat="server" CssClass="Centered" DataSourceID="SqlDSStaffID" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="500px"  OnRowDataBound = "OnRowDataBoundgStaff" OnSelectedIndexChanged = "OnSelectedIndexChangedgStaff" PageSize="10">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
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
           <asp:SqlDataSource ID="SqlDSStaffID" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
   
         
        </div>
        
           </asp:Panel>
     
     <asp:ModalPopupExtender ID="mdlPopupStaff" runat="server" CancelControlID="btnPnlStaffClose" PopupControlID="pnlStaff" TargetControlID="btndummystaff" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
       <asp:Button ID="btndummystaff" runat="server" cssclass="dummybutton" />
        <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSSalesman" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select Frequency from tblfrequency order by frequency">
                       
            </asp:SqlDataSource>
                   <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSBillingCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(ProductCode, ' - ', Description) AS ItemCode,ProductCode FROM tblbillingproducts order by Description "></asp:SqlDataSource>
                         <asp:SqlDataSource ID="SqlDSTerms" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Terms,TermsDay FROM tblterms ORDER BY termsday,terms"></asp:SqlDataSource>
       
        
    
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
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmOk" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelMsg" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />

    <asp:TextBox ID="txtModal" runat="server" Visible="false"></asp:TextBox>
       <asp:TextBox ID="txtCredit" runat="server" CssClass="dummybutton" ></asp:TextBox>
       <asp:TextBox ID="txtReceipt" runat="server" CssClass="dummybutton" ></asp:TextBox>
       <asp:TextBox ID="txtBalance" runat="server" CssClass="dummybutton" ></asp:TextBox>
</asp:Content>




