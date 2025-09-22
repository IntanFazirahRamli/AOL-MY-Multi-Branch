<%@ Page Title="AR Module Master Setup" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_ARModuleSetup.aspx.vb" Inherits="Master_ServiceRecordSetup" Culture="en-GB" %>
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
</script>
    <style type="text/css">
        .auto-style1 {
            font-size: 15px;
            font-weight: bold;
            font-family: 'Calibri';
            color: black;
            text-align: right;
            vertical-align: top; /*width:30%;*/ /*table-layout:fixed;
        overflow:hidden;*/;
            border-collapse: collapse;
            border-spacing: 0;
            line-height: 10px;
            width: 45%;
            height: 29px;
        }
        .auto-style2 {
            color: black;
            text-align: left;
            padding-left: 20px;
            font-size: 15px;
            font-weight: bold;
            font-family: 'Calibri';
            height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
        <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
       
   </ControlBundles>
    </asp:ToolkitScriptManager>
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">AR Module Setup</h3>
    
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
                  
                     <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" Visible="False" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Visible="False" />
                
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
            
                       </td>
                <td style="text-align: right">
            
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />

                </td>
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
        
            <tr>
                
                  <td colspan="2">

                      <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                           <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">&nbsp;WEB - TABLET SETTING</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left"> DOWNLOAD SETTING</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkWebToZSoft" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Tablet Records - AutoSet Submit Status to 'S'</asp:ListItem>
                                             <asp:ListItem Value="2">Tablet Records - AutoSet File Status to 'P'</asp:ListItem>
                                             <asp:ListItem Value="3">Tablet Records - AutoSet Detail Status to 'C'</asp:ListItem>
                                             <asp:ListItem Value="4">Tablet Records - Auto Update Status to 'P'</asp:ListItem>
                                             <asp:ListItem Value="5">Tablet Records - Auto Generate PDF Copy of Service Record</asp:ListItem>
                                         </asp:CheckBoxList></td></tr>

                              </table></td></tr>

                            <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px; display:none">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left"> UPLOAD SETTING</td>
                                     <td class="CellTextBoxADM">
                                         <asp:TextBox ID="txtUploadDays" runat="server" Width="50px"></asp:TextBox> Days Scheduled Service To Upload</td></tr>

                              </table></td></tr>
                      </table><br />

                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;display:none">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">QUICK SERVICE SETTING</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkQuickServiceSetting" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">New Quick Service Auto File when Save</asp:ListItem>
                                             <asp:ListItem Value="2">New Quick Service Auto Generate PDF Copy of Service Record</asp:ListItem>
                                             <asp:ListItem Value="3">New Quick Service Auto Submit when Save</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table>

                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">SCHEDULE AUTO ASSIGN</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Auto Assign By</td>
                                <td class="CellTextBoxADM" colspan="1">
                                     <asp:RadioButtonList ID="rdbScheduleAutoAssign" runat="server" RepeatDirection="Horizontal" Width="100%">
                                           <asp:ListItem>Vehicle</asp:ListItem>
                                              <asp:ListItem>Team</asp:ListItem>
                                            <asp:ListItem>Incharge</asp:ListItem>
                                     </asp:RadioButtonList>
                                      </td></tr>
                                         <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Allocation Method</td>
                               
                                        <td class="CellTextBoxADM" colspan="1">
                                     <asp:RadioButtonList ID="rdbAllocationMethod" runat="server" RepeatDirection="Horizontal" Width="70%">
                                           <asp:ListItem>TimeOut</asp:ListItem>
                                              <asp:ListItem>AllocateTime</asp:ListItem>
                                    
                                          </asp:RadioButtonList>
                                      </td></tr>

                                      <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Auto Assign Start</td>
                               
                                        <td class="CellTextBoxADM" colspan="1">
                                            <asp:TextBox ID="txtAssignStart" runat="server" Width="80px"></asp:TextBox> 
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b class="CellFormatADM">End</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     <asp:TextBox ID="txtAssignEnd" runat="server" Width="80px"></asp:TextBox>

                                      </td></tr>

                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Auto Assign Interval</td>
                               
                                        <td class="CellTextBoxADM" colspan="1">
                                            <asp:TextBox ID="txtAssignInterval" runat="server" width="50px"></asp:TextBox>

                                   &nbsp;&nbsp;mins
                                      </td></tr>
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Auto Assign Duration</td>
                               
                                        <td class="CellTextBoxADM" colspan="1">
                                            <asp:TextBox ID="txtAssignDuration" runat="server" width="50px"></asp:TextBox>

                                   &nbsp;&nbsp;mins
                                      </td></tr>
                              </table></td></tr>
                      </table>

                      
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">AUTO UPDATE CLIENT DETAILS</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkAutoUpdateClient" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Auto Update Client Details in Service Record</asp:ListItem>
                                             <asp:ListItem Value="2">Auto Update Client Details in Service Contract</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table>

                           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;display:none">
                           <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">JOB ORDER</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Default Printer</td>
                                     <td class="CellTextBoxADM">
                                         <asp:TextBox ID="txtDefaultPrinter" runat="server" Width="150px"></asp:TextBox></td></tr>

                              </table></td></tr>
                      </table>

                      
                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">GENERAL SETTINGS</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Approval Screen View Past Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtApprovalScreenView" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Job Order Record Period</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtJobOrderRecord" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Service Record Record Period</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSvcRecRecord" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Service Contract Record Period</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSvcContractRecord" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                    <tr>
                                <td class="CellTextBoxADM" colspan="2">
                                    <asp:CheckBoxList ID="chkDisableDelete" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Disable Delete Service Record</asp:ListItem>
                                          <asp:ListItem Value="2">Disable Delete Service Contract</asp:ListItem>
                                        
                                    </asp:CheckBoxList>
                                      </td></tr>

                              </table></td></tr>
                      </table><br />


                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">DOCUMENT EDITING</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkDocumentEditing" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Invoice can only be edited by Creator</asp:ListItem>
                                             <asp:ListItem Value="2">Credit Note can only be edited by Creator</asp:ListItem>
                                              <asp:ListItem Value="3">Debit Note can only be edited by Creator</asp:ListItem>
                                              <asp:ListItem Value="4">Receipt can only be edited by Creator</asp:ListItem>
                                              <asp:ListItem Value="5">Journal Adjustment can only be edited by Creator</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table><br />

                       
                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">MAXIMUM GRID RECORDS</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left; " >Invoice Maximum Displayed Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtInvoiceMaxRecs" runat="server" width="150px" ></asp:TextBox>

                                      </td></tr>

                                   <tr ><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Receipt Maximum Displayed Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtReceiptMaxRecs" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Credit Note Maximum Displayed Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtCreditNoteMaxRecs" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Journal Maximum Displayed Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtJournalMaxRecs" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                 </table></td></tr>
                      </table><br />

                      
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">GRID RECORDS ON SCREEN LOAD</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkShowGridRecords" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Show Invoice Records on Screen Load</asp:ListItem>
                                             <asp:ListItem Value="2">Show Receipt Records on Screen Load</asp:ListItem>
                                              <asp:ListItem Value="3">Show Credit/Debit Notes Records on Screen Load</asp:ListItem>
                                              <asp:ListItem Value="4">Show Journals Records on Screen Load</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table><br />
  


                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">AR Currency</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:DropDownList runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="30%" ID="ddlCurrency"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
</asp:DropDownList>

                                      </td></tr>

                                 

                                  <tr><td class="auto-style1" style="padding-left:50px;text-align:left">Default Tax Code</td>
                                <td class="auto-style2" colspan="1">
                               <asp:DropDownList ID="txtGST" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select" DataTextField="TaxType" DataValueField="TaxType" Height="25px" Value="-1" Width="30%">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                               </asp:DropDownList>

                                      </td></tr>

                                 

                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">&nbsp;</td>
                                <td class="CellTextBoxADM" colspan="1">
                                    &nbsp;</td></tr>

                                 

                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">&nbsp;</td>
                                <td class="CellTextBoxADM" colspan="1">
                                <asp:CheckBoxList ID="chkARModuleApplyTax" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Apply Tax to Corporate Customers</asp:ListItem>
                                             <asp:ListItem Value="2">Apply Tax to Residential Customers</asp:ListItem>
                                           
                                           </asp:CheckBoxList>    
                                </td></tr>

                                 

                              </table></td></tr>
                      </table><br />







                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">AR MODULE POST OPTIONS</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkARModulePost" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Post Invoice upon Save</asp:ListItem>
                                             <asp:ListItem Value="2">Post Receipt upon Save</asp:ListItem>
                                              <asp:ListItem Value="3">Post Credit/Debit Note upon Save</asp:ListItem>
                                              <asp:ListItem Value="4">Post Journal upon Save</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table><br />
                      
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Invoice Printing</td>
                                     <td class="CellTextBoxADM">
                                         <asp:RadioButtonList ID="rdbInvoiceSvcReportFile" runat="server">
                                              <asp:ListItem Value="1">Individual File for Invoice & Service Report</asp:ListItem>
                                             <asp:ListItem Value="2">Merged File for Invoice & Service Report</asp:ListItem>
                                         </asp:RadioButtonList>
                                     </td></tr>

                              </table></td></tr>
                      </table><br />

                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                                   <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">Accounts Receivable Auto Email</td>
                    </tr>
                        <%--   <tr><td class="CellFormatADM" style="text-align:left;width:100%;"><asp:Label ID="Label1" runat="server" Text="Invoice Auto Email"></asp:Label></td></tr>
                       --%>  
                            <tr>
                                <td style="text-align:left;width:100%;">
                             <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">                            
                              <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Open the print page after update</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkAutoEmailModules" runat="server"  Width="100%" CellPadding="1" CellSpacing="1" RepeatColumns="1" RepeatDirection="Vertical">
                                             <asp:ListItem Value="1">Invoice</asp:ListItem>
                                             <asp:ListItem Value="2">Debit Note</asp:ListItem>
                                              <asp:ListItem Value="3">Credit Note</asp:ListItem>
                                         <asp:ListItem Value="4">Receipt</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>
                            </table>
                                </td>
                                </tr>

                             <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Interval (in Seconds) of AR documents Batch Email</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtInvoiceEmailInterval" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>
                                  </table></td></tr>

                                  <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Invoice Batch Email Format</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:DropDownList  ID="ddlInvoiceBatchEmailFormat" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="30%">
                                           
                                             <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                    <asp:ListItem>Format1</asp:ListItem>
                    <asp:ListItem>Format2</asp:ListItem>
                    <asp:ListItem>Format3</asp:ListItem>
                    <asp:ListItem>Format4</asp:ListItem>
                    <asp:ListItem>Format5</asp:ListItem>
                    <asp:ListItem>Format6</asp:ListItem>
                    <asp:ListItem>Format7</asp:ListItem>
                    <asp:ListItem>Format8</asp:ListItem>
                                            <asp:ListItem>Format9</asp:ListItem>
                                            <asp:ListItem>Format10</asp:ListItem>
                                            <asp:ListItem>Format11</asp:ListItem>
</asp:DropDownList>
                                    <asp:CheckBox ID="chkAREMailSvcRpt" runat="server" Visible="false" Text="Include the Service Report (no photos) in the AR Email" width="100%"/>
                                 
                                      </td></tr>


                              </table></td></tr>

                             <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Email recipients for Sales Invoice Listing AutoEmail</td>
                                <td class="CellTextBoxADM" colspan="1">
                                      <asp:TextBox ID="txtEmailAddressForSalesInvoiceListing" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                      </td></tr>
                                  </table></td></tr>

                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Email recipients for Receipt Listing AutoEmail</td>
                                <td class="CellTextBoxADM" colspan="1">
                                      <asp:TextBox ID="txtEmailAddressForReceiptListing" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                      </td></tr>
                                  </table></td></tr>
                      </table><br />
                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                           <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">Statement of Accounts Auto Email</td>
                    </tr>
                        <%--   <tr><td class="CellFormatADM" style="text-align:left;width:100%;"><asp:Label ID="Label2" runat="server" Text="Statement of Accounts Auto Email"></asp:Label></td></tr>
                       --%>    <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">SOA Auto Email Interval (in Seconds)</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSOAEmailInterval" runat="server" width="150px"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">SOA Auto Email Schedule Start and End (day of the month)</td>
                                <td class="CellTextBoxADM" colspan="1" style="width:170px">
                                       <asp:TextBox ID="txtSOASchedule" runat="server" width="150px" MaxLength="2"></asp:TextBox>

                                      </td>
                                       <td class="CellFormatADM" style="width:30px">To</td>
                                         <td class="CellTextBoxADM" colspan="1">  <asp:TextBox ID="txtSOAScheduleEnd" runat="server" width="150px" MaxLength="2"></asp:TextBox>

                                     </td>
                                   </tr>
                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Scheduled Time (Time of the day to send AutoEmails)</td>
                               <td class="CellTextBoxADM" colspan="1" style="width:170px">
                                       <asp:TextBox ID="txtAutoEmailStartTime" runat="server" width="150px" MaxLength="5"></asp:TextBox>

                                      </td>
                                       <td class="CellFormatADM" style="width:30px">To</td>
                                         <td class="CellTextBoxADM" colspan="1">  <asp:TextBox ID="txtAutoEmailEndTime" runat="server" width="150px" MaxLength="5"></asp:TextBox>

                                     </td></tr>
                                   <tr>
                                <td class="CellTextBoxADM" colspan="2" style="padding-left:50px;width:45%;text-align:left">
                                        <asp:CheckBox ID="chkSOAWithCreditBalance" runat="server" Text="Send SOA for Accounts with Credit Balance  " TextAlign="Left" />
                                    
                                      </td></tr>


                              </table></td></tr>
                      </table><br />

<%--                      '06.12.24--%>

                      <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:200px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">E-Invoicing</td>
                                     <td class="CellTextBoxADM">
                                         <asp:RadioButtonList ID="rdbEInvoicing" runat="server">
                                              <asp:ListItem Value="1">Disabled</asp:ListItem>
                                             <asp:ListItem Value="2">Malaysia E-invoicing</asp:ListItem>
                                         </asp:RadioButtonList>
                                     </td></tr>
                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Start Date</td>
                                     <td class="CellTextBoxADM">
                                           <asp:TextBox ID="txtStartDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="150px"></asp:TextBox>
                                    <asp:CalendarExtender ID="calStartDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtStartDate" TargetControlID="txtStartDate" />
                               
                                     </td></tr>
                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Cancellation Time Limit</td>
                                     <td class="CellTextBoxADM">
                                           <asp:TextBox ID="txtCancellationTimeLimit" runat="server" AutoCompleteType="Disabled" Height="16px" Width="100px"></asp:TextBox>
                                hrs
                                     </td></tr>
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Category</td>
                                <td class="CellTextBoxADM" colspan="3">
                                       <asp:DropDownList  ID="ddlCategory" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="200px">
                                           
                                             <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                    <asp:ListItem>Business</asp:ListItem>
                  
</asp:DropDownList>
                                    <asp:RadioButtonList ID="rdbCategoryBusiness" runat="server" width="100%">
                                              <asp:ListItem Value="1">Local Business</asp:ListItem>
                                             <asp:ListItem Value="2">Foreign Business</asp:ListItem>
                                         </asp:RadioButtonList>
                                      </td></tr>
                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">TIN</td>
                                     <td class="CellTextBoxADM" style="width:175px" colspan="1">
                                           <asp:TextBox ID="txtTIN" runat="server" AutoCompleteType="Disabled" Height="16px" Width="200px"></asp:TextBox>
                            </td>
                                       <td class="CellTextBoxADM" colspan="2" style="width:100%">   <asp:Label ID="lblTIN" runat="server" Text="* If TIN is not provided, leave TIN blank. E100000000020 (Buyer) or E100000000030(Supplier) will be used to submit e-Invoice." />
                                     </td></tr>
                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Business Reg No</td>
                               <td class="CellTextBoxADM" colspan="1" style="width:170px">
                                       <asp:TextBox ID="txtBRN" runat="server" width="200px" MaxLength="50"></asp:TextBox>

                                      </td>
                                       <td class="CellFormatADM" style="width:30px">SST Reg No</td>
                                         <td class="CellTextBoxADM" colspan="1">  <asp:TextBox ID="txtSSTNo" runat="server" width="200px" MaxLength="50"></asp:TextBox>

                                     </td></tr>
                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">New Business Reg No</td>
                               <td class="CellTextBoxADM" colspan="1" style="width:170px">
                                       <asp:TextBox ID="txtNewBRN" runat="server" width="200px" MaxLength="50"></asp:TextBox>

                                      </td>
                                       <td class="CellFormatADM" style="width:30px"></td>
                                         <td class="CellTextBoxADM" colspan="1">  

                                     </td></tr>
                                  <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">MSIC Code</td>
                               <td class="CellTextBoxADM" colspan="1" style="width:170px">
                                           <asp:DropDownList runat="server" AutoPostback="True" EnableViewState="True" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="200px" ID="ddlMSICCode"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
</asp:DropDownList>

                                      </td>
                                       <td class="CellFormatADM" style="width:30px">MSIC Description</td>
                                         <td class="CellTextBoxADM" colspan="1">  <asp:TextBox ID="txtMSICDesc" runat="server" width="300px" MaxLength="5"></asp:TextBox>

                                     </td></tr>
                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">EInvoice Classification Code</td>
                               <td class="CellTextBoxADM" colspan="1" style="width:170px">
                                           <asp:DropDownList runat="server" AutoPostback="True" EnableViewState="True" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="200px" ID="ddlEInvClassificationCode"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
</asp:DropDownList>

                                      </td>
                                       <td class="CellFormatADM" style="width:30px">EInvoice Classification Description</td>
                                         <td class="CellTextBoxADM" colspan="1">  <asp:TextBox ID="txtEInvClassificationDesc" runat="server" width="300px" MaxLength="5"></asp:TextBox>

                                     </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Consolidated Classification Code</td>
                               <td class="CellTextBoxADM" colspan="1" style="width:170px">
                                           <asp:DropDownList runat="server" AutoPostback="True" EnableViewState="True" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="200px" ID="ddlConsolidatedClassificationCode"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
</asp:DropDownList>

                                      </td>
                                       <td class="CellFormatADM" style="width:30px">Consolidated Classification Description</td>
                                         <td class="CellTextBoxADM" colspan="1">  <asp:TextBox ID="txtConsolidatedClassificationDesc" runat="server" width="300px" MaxLength="5"></asp:TextBox>

                                     </td></tr>
                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">Business Activity Description</td>
                                     <td class="CellTextBoxADM" colspan="3">
                                           <asp:TextBox ID="txtBusinessDesc" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%"></asp:TextBox>
                                      </td></tr>
                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">EInvoice Notification Email Recipients</td>
                                     <td class="CellTextBoxADM" colspan="3">
                                            <asp:TextBox ID="txtEInvoiceEmailRecipients" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                     </td></tr>
                              </table></td></tr>
                      </table><br />

<%--                      '06.12.24--%>

<%--                      29.11.24--%>

                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:45%;text-align:left">BATCH INVOICE</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkBatchInvoiceOption" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Company Group Optional</asp:ListItem>
                                           
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table><br />

<%--                      29.11.24--%>

                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">SERVICE CONTRACT</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBoxList ID="chkServiceContract" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">Schedule Service After Update Contract</asp:ListItem>
                                             <asp:ListItem Value="2">Confirmation Needed for Amend Service Address</asp:ListItem>
                                               <asp:ListItem Value="3">Post Your Ref/Our Ref to Service Record</asp:ListItem>
                                           </asp:CheckBoxList></td></tr>

                              </table></td></tr>
                      </table><br />
  
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">SERVICE RECORD SETTINGS</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Sort Order</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:radiobuttonlist ID="rdbSvcRecOrder" runat="server" RepeatColumns="2" RepeatDirection="Vertical" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                             <asp:ListItem Value="dec">Descending</asp:ListItem>
                                           </asp:radiobuttonlist>
                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Service Record</td>
                                <td class="CellTextBoxADM" colspan="1">
                                      <asp:CheckBoxList ID="chkSvcRecord" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CellPadding="1" CellSpacing="1">
                                             <asp:ListItem Value="1">E-Approval Auto Update Service Record</asp:ListItem>
                                             <asp:ListItem Value="2">Not Allow Save if Detail Status is O</asp:ListItem>
                                               <asp:ListItem Value="3">Calculate Total Duration by Time In/Time Out</asp:ListItem>
                                          <asp:ListItem Value="4">Skip Retry Auto Sending Email after </asp:ListItem>
                                        
                                           </asp:CheckBoxList><asp:TextBox ID="txtAttempts" runat="server" Width="60"></asp:TextBox> attempts
                                      </td></tr>

                             </table></td></tr>
                      </table><br />

                     
                          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">TABLET</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">

                           

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Supervisor Records</td>
                                <td class="CellTextBoxADM" colspan="1">
                                     <asp:TextBox ID="txtSupervisorRec" runat="server" Width="60"> </asp:TextBox> Days
                                      </td></tr>

                             </table></td></tr>
                      </table><br />
                      
                       <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3; display:none">
                        
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px; ">
                                 <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">CALENDAR</td>
                                     <td class="CellTextBoxADM">
                                         <asp:CheckBox ID="chkCalendar" runat="server" Text="Job Order Assignment / Completion" Visible="False" /></td></tr>

                              </table></td></tr>
                      </table><br />  
                  </td>
                 </tr>
             
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblcity where rcno<>0"></asp:SqlDataSource>

       
        
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Country, Citizenship, Comments, Rcno, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, CountryCode, WS FROM tblcountry WHERE (Rcno &lt;&gt; 0)">
                       
                    </asp:SqlDataSource>

           <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT IndustrialClassificationCode,Description FROM tbleinvoicemalaysiaSICode where rcno<>0">
                       
                    </asp:SqlDataSource>
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtCountryCode" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtMSICCode" runat="server" Visible="false"></asp:TextBox>
         <asp:TextBox ID="txtEInvClassificationCode" runat="server" Visible="false"></asp:TextBox>
         <asp:TextBox ID="txtConsolidatedClassificationCode" runat="server" Visible="false"></asp:TextBox>
    </div>
 

</asp:Content>



