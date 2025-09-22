<%@ Page Title="UnPrinted Invoice Listing" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_UnprintedInvoiceListing.aspx.vb" Inherits="RV_Select_UnprintedInvoiceListing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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

      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
     
    <table style="width:80%;">
        <tr>
            <td colspan="2">
                  <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">UNPRINTED INVOICE LISTING</h4>
    
            </td>
        </tr>
           <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
        <tr style="width:100%"><td style="width:90%">
      <table style="width:90%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:50px;line-height:20px;">
                       
             <tr>
                      <td class="CellFormat">InvoiceDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtInvDateFrom" TargetControlID="txtInvDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtInvDateTo" TargetControlID="txtInvDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
      <tr>
          <td colspan="5"><br /></td>
      </tr>
      
              <tr>
                  <td colspan="4" style="text-align:center; ">
                      
                      <asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" Visible="TRUE" />
                      
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
     <%--<asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>--%>
    
   
</asp:Content>





