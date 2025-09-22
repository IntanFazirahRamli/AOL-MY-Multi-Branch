<%@ Page Title="Service Record Report Selection" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_ServiceRecord.aspx.vb" Inherits="RV_Select_ServiceRecord" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <script type="text/javascript" src="JS/jquery-1.6.1.min.js" ></script>

    <script type="text/javascript" src="JS/Dropdownscript.js" ></script>
    <script type="text/javascript" src="JS/Dropdownscript.min.js" ></script>
   <link rel="Stylesheet" type="text/css" href="CSS/DefaultStyles.css" />
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
          function CheckBoxListSelect(cbControl) {
              var chkBoxList = document.getElementById(cbControl);
              var chkBoxCount = chkBoxList.getElementsByTagName("input");
              // var clicked = this;


              chkBoxCount[7].onclick = function () {
                  for (var i = 0; i < chkBoxCount.length - 1; i++) {

                      chkBoxCount[i].checked = chkBoxCount[7].checked;
                  }
              }
          }
          function EnableDisableStatusSearch() {

              var statussearch = document.getElementById("<%=chkSearchAll.ClientID%>").checked;

                 if (statussearch == false) {

                     var cbl = document.getElementById('<%=chkStatusSearch.ClientID%>').getElementsByTagName("input");
                 for (i = 0; i < cbl.length; i++) cbl[i].checked = false;
             }
             else if (statussearch == true) {

                 var cbl = document.getElementById('<%=chkStatusSearch.ClientID%>').getElementsByTagName("input");
                for (i = 0; i < cbl.length; i++) cbl[i].checked = true;
            }
    }
    </script>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
       <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">SERVICE RECORD LISTING REPORT</h4>
    
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
      <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;line-height:20px;">
               
               <tr>
                      <td class="CellFormat">
                         Status
                             </td>
                      <td colspan="2" style="padding-left:10px;">
                           <asp:checkboxlist ID="chkStatusSearch" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4" CellPadding="2" CellSpacing="2" TextAlign="Right" Enabled="True">
                                   <asp:ListItem Value="O" Selected="true">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="C">C - Cancelled</asp:ListItem>
                                     <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                    <asp:ListItem Value="H" Selected="true">H - On Hold</asp:ListItem>  
                                     <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                    <asp:ListItem Value="B">B - Job cannot complete</asp:ListItem>                              
                                    <asp:ListItem Value="P" Selected="true">P - Posted</asp:ListItem> 
                               <%--<asp:ListItem Value="ALL">ALL STATUS</asp:ListItem>--%>
                               </asp:checkboxlist>
                               <asp:CheckBox ID="chkSearchAll" runat="server"  Text="All Status" Font-Size="15px" Font-Names="Calibri" Font-Bold="True" onchange="EnableDisableStatusSearch()" BorderColor="White" BorderWidth="4px" />
                 
                      </td>
                   
                         </tr>
           <tr>
                      <td class="CellFormat">
                         BillStatus
                             </td>
                      <td colspan="2" style="padding-left:10px;">
                           <asp:radiobuttonlist ID="rdbBillStatus" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3" CellPadding="1" CellSpacing="1" TextAlign="Right" Enabled="True">
                               <asp:ListItem>Billed</asp:ListItem>
                               <asp:ListItem>UnBilled</asp:ListItem>
                               <asp:ListItem Selected="True">All</asp:ListItem>
                                  
                               </asp:radiobuttonlist>
                            
                      </td>
                   
                         </tr>
             <tr>
                      <td class="CellFormat" style="width:25%">CompanyGroup</td>
                    <td class="CellTextBox">  <cc1:dropdowncheckboxes ID="ddlCompanyGrp" runat="server" Width="60.5%" UseSelectAllNode = "true" DataSourceID="SqlDSCompanyGroup" DataTextField="CompanyGroup" DataValueField="CompanyGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="60.5%" DropDownBoxBoxWidth="60.5%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                           </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
           
           <tr>
                      <td class="CellFormat" style="width:25%">ContractNo</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtContractNo" runat="server" MaxLength="30" Height="16px" Width="60.5%"></asp:TextBox>
                           </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
           <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Service Details</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
               <tr>
                      <td class="CellFormat">
                         Service Record
                             </td>
                     <td class="CellTextBox" colspan="1"><asp:TextBox ID="txtServiceRecord" runat="server" MaxLength="30" Height="16px" Width="60%"></asp:TextBox> </td>
                     <td class="CellTextBox" rowspan="6" style="text-align:left;padding-left:0px;">
                           <asp:Panel ID="Panel1" runat="server" Height="95%" BorderStyle="Solid" BorderColor="#FF9900" BorderWidth="2px" Width="200px" HorizontalAlign="Center">
                                  <div style="font-size:15px;padding-top:10px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Display By</div>
                   <div style="padding-left:20px;text-align:left;padding-bottom:20px;">
                               <asp:radiobuttonList ID="chkGrouping" runat="server" CellSpacing="2" CellPadding="2">
                                   <asp:ListItem Text="No Grouping" Value="No Grouping" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Client" Value="Client"></asp:ListItem>
                                    <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                                 
                               </asp:radiobuttonList></div>
                                        </asp:Panel>
                    </td>
                               </tr>
              
               <tr>
                             <td class="CellFormat">Service By
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataSourceID="SqlDSIncharge" DataTextField="inchargeId" DataValueField="inchargeId" Width="60.5%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>         </td>
                              
                           </tr>
             <tr>
                      <td class="CellFormat">ServiceDate</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="26%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> &nbsp;TO&nbsp; <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="26%"></asp:TextBox>                        
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> </td>
               
             
                   
               
             
                  </tr>
            <tr>
                      <td class="CellFormat">
                         ContractGroup
                             </td>
                     <td class="CellTextBox" colspan="1"> <%--<asp:dropdownlist ID="txtServiceID" runat="server" Width="37.5%" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist>--%>
                         <cc1:dropdowncheckboxes ID="ddlContractGroup" runat="server" Width="60%" UseSelectAllNode = "true" DataSourceID="SqlDSContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="60%" DropDownBoxBoxWidth="60%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                     </td>                          
                
                          
                               </tr>

             <tr>
                      <td class="CellFormat">
                         Industry
                             </td>
                     <td class="CellTextBox" colspan="1"> <%--<asp:dropdownlist ID="txtServiceID" runat="server" Width="37.5%" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist>--%>
                         <cc1:dropdowncheckboxes ID="ddlIndustry" runat="server" Width="60%" UseSelectAllNode = "true" DataSourceID="SqlDSIndustry" DataTextField="Industry" DataValueField="Industry" AddJQueryReference="true" UseButtons="false" >
     <Style2 SelectBoxWidth="60%" DropDownBoxBoxWidth="60%" DropDownBoxBoxHeight="120" />
                    
 </cc1:dropdowncheckboxes>
                     </td>                          
                
                          
                               </tr>

           <tr>
                             <td class="CellFormat">Schedule Type
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlSchType" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataSourceID="SqlDSScheduleType" DataTextField="ScheduleType" DataValueField="ScheduleType" Width="60.5%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>         </td>
                              
                           </tr>
            <tr>
                      <td class="CellFormat">
                         Service ID
                             </td>
                     <td class="CellTextBox" colspan="1"> <asp:dropdownlist ID="txtServiceID" runat="server" Width="60.5%" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" AutoPostBack="FALSE" AppendDataBoundItems="TRUE" >
                <asp:ListItem Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
                          
                 
                          
                               </tr>
              <tr>
                  <td class="CellFormat" style="height: 26px">ServiceFrequency</td>
                  <td class="CellTextBox" style="height: 26px">  <asp:dropdownlist ID="ddlServiceFrequency" runat="server" Width="60.5%" DataSourceID="SqlDSFrequency" DataTextField="Frequency" AppendDataBoundItems="TRUE" DataValueField="Frequency" >
                <asp:ListItem Selected="TRUE" Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
                 
              </tr>
              <tr>
                  <td class="CellFormat">BillingFrequency</td>
                  <td class="CellTextBox"> <asp:dropdownlist ID="ddlBillingFrequency" runat="server" AppendDataBoundItems="TRUE" Width="60.5%" DataSourceID="SqlDSBillingFrequency" DataTextField="Frequency" DataValueField="Frequency" >
                <asp:ListItem Selected="TRUE" Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
               
              </tr>
                <tr>
                  <td class="CellFormat">TargetID</td>
                  <td class="CellTextBox"> <asp:dropdownlist ID="ddlTargetID" runat="server" AppendDataBoundItems="TRUE" Width="60.5%" DataSourceID="SqlDSTarget" DataTextField="TargetID" DataValueField="Descrip1" >
                <asp:ListItem Selected="TRUE" Text="--SELECT--" Value="-1" />
                </asp:dropdownlist></td>
                  <td class="CellTextBox"> &nbsp;</td>
              </tr>

                <tr>
                      <td class="CellFormat">To Bill Amt</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtBillAmtFrom" runat="server" MaxLength="10" Height="16px" Width="26%"></asp:TextBox>                        
                                  &nbsp;TO&nbsp; <asp:TextBox ID="txtBillAmtTo" runat="server" MaxLength="10" Height="16px" Width="26%"></asp:TextBox>                        
                          </td>
                    <td class="CellTextBox">&nbsp;</td>
                  </tr>

               <%-- <tr>
                      <td class="CellFormat">Notes</td>
                    <td class="CellTextBox" colspan="2"><asp:TextBox ID="txtNotes" runat="server" MaxLength="10" Height="32px" Width="90%" TextMode="MultiLine"></asp:TextBox>                        
                                    </td>
                  </tr>--%>
           <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Account Information</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
                      
                    <tr>
                      <td class="CellFormat">AccountType</td>
                    <td class="CellTextBox">  <asp:DropDownList ID="ddlAccountType" runat="server" Width="60.5%" Height="20px" AutoPostBack="true"
                                    DataTextField="ContType" DataValueField="ContType" CssClass="chzn-select" >
                                    <asp:ListItem  Selected="True" Text="--SELECT--" Value="-1" />
                                    <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                                    <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                </asp:DropDownList>   </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
                 <tr>
                      <td class="CellFormat">AccountID</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtAccountID" runat="server" MaxLength="10" Height="16px" Width="60%"></asp:TextBox>
                           <asp:ImageButton ID="btnClient" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
 
                 
                    </td>
                    <td class="CellTextBox">  &nbsp;</td>
                  </tr>
               <tr>
                      <td class="CellFormat">AccountName</td>
                    <td class="CellTextBox" colspan="2">  <asp:TextBox ID="txtCustName" runat="server" MaxLength="200" Height="16px" Width="90%"></asp:TextBox>
                               </td>
                  </tr>
             <%--       <tr>
                      <td class="CellFormat" colspan="3" style="text-align:center;padding-left:10%">
                           <table style="border: 1px solid #C0C0C0; text-align:right; border-radius: 10px; width:70%; height:50px; background-color: #F3F3F3;">
      <tr>
          <td>
                 <asp:radiobuttonlist ID="rdbClientSign" runat="server" Font-Bold="True" RepeatDirection="Horizontal" Width="70%">
                              <asp:ListItem Value="1">With Client Sign</asp:ListItem>
                              <asp:ListItem Value="2">Without Client Sign</asp:ListItem>
                              <asp:ListItem Selected="True" Value="3">All Services</asp:ListItem>
                          </asp:radiobuttonlist>
          </td>
      </tr>
                       
                               </table>
                     </td>
                  </tr>--%>
                   
           <tr>
                         <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Sort By</td>
                         <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">&nbsp;</td>
                    </tr>
               
                <tr>
                     <%-- <td class="CellFormat">SortBy</td>--%>
                    <td class="CellTextBox" colspan="2" style="text-align:center;padding-left:150px">
                        <table>
                            <tr>
                                <td rowspan="2"><asp:ListBox ID="lstSort1" runat="server" width="120px" Rows="6" SelectionMode="Multiple">
                                    <asp:ListItem Text="Status" Value="Status">Status</asp:ListItem>
                                    <asp:ListItem Text="Client Name" Value="CustName">Client Name</asp:ListItem>
                                    <asp:ListItem Text="ServiceBy" Value="ServiceBy">Service By</asp:ListItem>
                                    <asp:ListItem Text="ServiceDate" Value="ServiceDate">Service Date</asp:ListItem>
                                    <asp:ListItem Text="TimeIn" Value="TimeIn">Time In</asp:ListItem>
                                    </asp:ListBox></td>
                                <td><asp:Button ID="btnSort1" runat="server" Text=">>" /></td>
                                 <td rowspan="2"><asp:ListBox ID="lstSort2" runat="server" width="120px" Rows="6" SelectionMode="Multiple"></asp:ListBox></td>
                            </tr>
                            <tr>
<td colspan="1"><asp:Button ID="btnSort2" runat="server" Text="<<" /></td>
                            </tr>
                        </table>
                         
                                    </td>
                  </tr>
           <%--   <tr>
                  <td><br /></td>
              </tr>--%>
              <tr>
                  <td colspan="2" style="text-align:center"><asp:Button ID="btnPrintServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                      <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="false"/>
                         <asp:Button ID="btnNewExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="150px" Visible="true"/>
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                        </td>
                  <td style="text-align:center">&nbsp;</td>
              </tr>
        </table>

       <asp:GridView ID="GridView1" runat="server" Width="744px" CssClass="Centered" >
                          <Columns>
           
                          </Columns>
                           <EditRowStyle BackColor="#999999" />
                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                   <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                   <SortedAscendingCellStyle BackColor="#E9E7E2" />
                   <SortedAscendingHeaderStyle BackColor="#506C8C" />
                   <SortedDescendingCellStyle BackColor="#FFFDF8" />
                   <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                      </asp:GridView>
                 


                <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
    
        <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="900px" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
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
                   
               <%--     <asp:BoundField DataField="Address1" >
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
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT * FROM tblcompany WHERE rcno <> 0 order by name">
         
            </asp:SqlDataSource>
        </div>
    </asp:Panel>

                <asp:modalpopupextender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient"
                                    TargetControlID="btndummyClient" BackgroundCssClass="modalBackground">
                                </asp:modalpopupextender>
                <asp:Button ID="btndummyClient" runat="server" Text="Button" CssClass="dummybutton" />
           

           <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup">
                       
            </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Industry FROM tblIndustry order by Industry"></asp:SqlDataSource>
   
        <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct * FROM tblservicefrequency order by frequency"></asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlDSBillingFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblfrequency order by frequency"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSTarget" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblTarget order by TargetID"></asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblProduct order by ProductID"></asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (inchargeId) FROM tblteam ORDER BY inchargeID"></asp:SqlDataSource>
 <asp:SqlDataSource ID="SqlDSScheduleType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct (ScheduleType) FROM tblScheduleType ORDER BY ScheduleType"></asp:SqlDataSource>
    <asp:TextBox ID="txtModal" runat="server" Visible="false"></asp:TextBox>
     <asp:TextBox ID="txtModalClient" runat="server" Visible="false"></asp:TextBox>
    </asp:Content>