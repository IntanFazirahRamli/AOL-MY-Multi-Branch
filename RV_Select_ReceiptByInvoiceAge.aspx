<%@ Page Title="Receipt Listing By Invoice Age" Language="VB" MasterPageFile="~/MasterPage_Report.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="RV_Select_ReceiptByInvoiceAge.aspx.vb" Inherits="RV_Select_ReceiptByInvoiceAge" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
     
    <table style="width:100%;">
        <tr>
            <td colspan="2">
                  <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">RECEIPT LISTING BY INVOICE AGE</h4>
    
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
                      <td class="CellFormat">ReceiptDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtInvDateFrom" TargetControlID="txtInvDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                         </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtInvDateTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtInvDateTo" TargetControlID="txtInvDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                  </tr>
          
         <tr>
                      <td class="CellFormat">DueDateCutOff</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtDueDateFrom" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDueDateFrom" TargetControlID="txtDueDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                         </td>
               <%--  <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtDueDateTo" runat="server" MaxLength="10" Height="16px" Width="85.5%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtDueDateTo" TargetControlID="txtDueDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               --%>
             
                  </tr>

            <tr>
                      <td class="CellFormat">AgeFrom</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtAgeFrom" runat="server" MaxLength="3" Height="16px" Width="85.5%"></asp:TextBox>                        
                            </td>
                 <td class="CellFormat" style="text-align: right; ">TO</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtAgeTo" runat="server" MaxLength="3" Height="16px" Width="85.5%"></asp:TextBox>                        
                     </td>
             
                  </tr>
          <tr>
                      <td class="CellFormat">SelectOption</td>
                    <td class="CellTextBox" colspan="3"><asp:RadioButtonList ID="rbtnSelect" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Horizontal" Width="100%">
                           <%-- <asp:ListItem Text="Contract Group Value Report" Value="1" Selected="true"></asp:ListItem>--%>
                            <asp:ListItem Text="Only Receipts" Value="1" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Include CN,DN,Journals" Value="2"></asp:ListItem>
                         </asp:RadioButtonList>  </td>
             
                  </tr>

             
              <tr>

                  <td colspan="4" style="text-align:center; ">
                      <br />
                      <asp:Button ID="btnPrintServiceRecordList" runat="server" visible="false" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                      
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
    
</asp:Content>




