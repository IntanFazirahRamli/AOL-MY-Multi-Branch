<%--<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="Master_MobileDevice.aspx.vb"
    Inherits="SitaPest.Master_MobileDevice" %>--%>

<%@ Page Language="vb" AutoEventWireup="false" Title="Mobile Device Registry" MasterPageFile="~/MasterPage.master" CodeFile="Master_MobileDevice.aspx.vb" EnableEventValidation="false"
    Inherits="Master_MobileDevice" %>


<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script lang="javascript" type="text/javascript">

           function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
               var tbl = document.getElementById(gridId);
               if (tbl) {
                   var DivHR = document.getElementById('DivHeaderRow');
                   var DivMC = document.getElementById('DivMainContent');
                   var DivFR = document.getElementById('DivFooterRow');

                   //*** Set divheaderRow Properties ****
                   DivHR.style.height = headerHeight + 'px';
                   DivHR.style.width = (parseInt(width) - 16) + 'px';
                   DivHR.style.position = 'relative';
                   DivHR.style.top = '0px';
                   DivHR.style.zIndex = '1';
                   DivHR.style.verticalAlign = 'top';

                   //*** Set divMainContent Properties ****
                   DivMC.style.width = width + 'px';
                   DivMC.style.height = height + 'px';
                   DivMC.style.position = 'relative';
                   DivMC.style.top = -headerHeight + 'px';
                   DivMC.style.zIndex = '1';
                   DivMC.style.paddingtop = '2px';

                   //*** Set divFooterRow Properties ****
                   DivFR.style.width = (parseInt(width) - 16) + 'px';
                   DivFR.style.position = 'relative';
                   DivFR.style.top = -headerHeight + 'px';
                   DivFR.style.verticalAlign = 'top';
                   DivFR.style.paddingtop = '2px';

                   if (isFooter) {
                       var tblfr = tbl.cloneNode(true);
                       tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                       var tblBody = document.createElement('tbody');
                       tblfr.style.width = '100%';
                       tblfr.cellSpacing = "0";
                       tblfr.border = "0px";
                       tblfr.rules = "none";
                       //*****In the case of Footer Row *******
                       tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                       tblfr.appendChild(tblBody);
                       DivFR.appendChild(tblfr);
                   }
                   //****Copy Header in divHeaderRow****
                   DivHR.appendChild(tbl.cloneNode(true));
               }
           }



           function OnScrollDiv(Scrollablediv) {
               document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
               document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
           }


</script>

    <script type="text/javascript">
        var defaultText1 = "Search Here";
        function WaterMark1(txt, evt) {
            if (txt.value.length == 0 && evt.type == "blur") {
                txt.style.color = "gray";
                txt.value = defaultText1;
            }
            if (txt.value == defaultText1 && evt.type == "focus") {
                txt.style.color = "black";
                txt.value = "";
            }
        }


        function checkalllocationrecs() {
            //alert("1");
            var table = document.getElementById('<%=GridView2.ClientID%>');

             if (table.rows.length > 0) {
                 //alert("2");

                 var input = table.rows[0].getElementsByTagName("input");

                 if (input[0].id.indexOf("CheckBox1") > -1) {

                     //start
                     if ((input[0].checked) == false) {
                         for (var i = 1; i < table.rows.length; i++) {

                             //get all the input elements
                             var input1 = table.rows[i].getElementsByTagName("input");

                             for (var j = 0; j < input1.length; j++) {

                                 //get the textbox1
                                 input1[0].checked = false;

                             }
                         }

                         //end


                     }


                     else {
                         //loop the gridview table
                         for (var i = 1; i < table.rows.length; i++) {

                             //get all the input elements
                             var inputs = table.rows[i].getElementsByTagName("input");

                             for (var j = 0; j < inputs.length; j++) {

                                 //get the textbox1
                                 inputs[0].checked = true;


                             }
                         }
                     }
                 }
             }

         }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                  <asp:controlBundle name="ComboBox_Bundle"/>               
            </ControlBundles>
        </asp:ToolkitScriptManager>

  <%--  <asp:Panel ID="pnlPopUpAddEndPoint" runat="server" BackColor="White" Width="900" Height="400" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">
        <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: center; padding-left: 20px;">
            <tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">ADD END POINTS</h4> </td>
               
                           </tr>
                              
        </table>


        <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: center; padding-left: 10px;">
             <tr>
                <td class="CellFormatADM">Country<asp:Label ID="Label6" runat="server" Visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                <td class="CellTextBoxADM">
                    <asp:DropDownList ID="ddlCountryAddEndPoint" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Enabled="true" Width="225px">
                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                 
                      
                    </asp:DropDownList></td>
            </tr>
             
               <tr>
                <td class="CellFormatADM">WebServiceURL</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtWebServiceUrlAddEndPoint" runat="server" MaxLength="200" Height="16px" Width="600px" Enabled="true" AutoCompleteType="Disabled"></asp:TextBox></td>
                               <asp:TextBox ID="txtAddEndPointRcNo" runat="server" MaxLength="200" Height="16px" Width="600px" Enabled="true" AutoCompleteType="Disabled"></asp:TextBox></td>
        
            </tr>
               <tr style="padding-top: 40px;">
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnSaveEndPoint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="OK" Width="120px" />
                    <asp:Button ID="btnCancelEndPoint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
       
        </table>
        
    </asp:Panel>

    <asp:Button ID="btnDummyAddEndPoint" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mdlPopupAddEndPoint" runat="server" TargetControlID="btnDummyAddEndPoint"
        PopupControlID="pnlPopUpAddEndPoint" CancelControlID="btnPnlCancelEndPoint" BackgroundCssClass="modalBackground" />--%>

    <div style="text-align: center">
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">Mobile Device Registry</h3>

        <table style="width: 100%; text-align: center;">
            <tr>
                <td colspan="2" style="width: 100%; text-align: center; color: brown; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                    <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                    <asp:Label ID="txtMode" runat="server" Text="" CssClass="dummybutton"></asp:Label>
                </td>

            </tr>

            <tr>
                <td style="width: 40%; text-align: left;">
                    <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="ADD" Width="100px" />
                    <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px" CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="DELETE" Width="100px" OnClientClick="Confirm()" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="PRINT" Width="100px" />
                      <asp:Button ID="btnUpdateURL" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="UPDATE WEBSERVICE" Width="180px" Visible="false" />
            <asp:Button ID="btnFilter" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="SEARCH" Width="100px" />
                  
                       </td>
                <td style="text-align: right">
                    <asp:Button ID="btnQuit" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="QUIT" Width="100px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <table style="text-align: right; width: 100%">
                        <tr>
                            <td style="text-align: right; width: 65%; display: inline;">
                                <asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="RESET SEARCH" /></td>
                            <td style="text-align: left; width: 35%">
                                <asp:TextBox ID="txtSearch" runat="server" Width="370px" Height="25px" Text="Search Here" ForeColor="Gray" onblur="WaterMark1(this, event);" onfocus="WaterMark1(this, event);" AutoPostBack="True"></asp:TextBox>
                             &nbsp; <asp:Button ID="btnGo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="GO" Width="50px" Height="30px" OnClick="btnGo_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:LEFT;">
                       <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                  
                </td>
            </tr>
            <tr class="Centered">
                <td colspan="2">
                   
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" AutoGenerateColumns="False" BackColor="#DEBA84"
                        BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="IMEI" Font-Size="12pt"  ForeColor="#333333"
                        HorizontalAlign="Center" AllowSorting="True" AllowPaging="True">
                              <AlternatingRowStyle BackColor="White"/>
                         <Columns>
                            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="FALSE">
                                <ControlStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="DeviceId" HeaderText="Device ID" SortExpression="DeviceId">
                                <ControlStyle Width="180px" />
                                <ItemStyle Width="180px" HorizontalAlign="Left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeviceType" HeaderText="Device Type" SortExpression="DeviceType">
                                <ControlStyle Width="150px" />
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IMEI" HeaderText="IdentityNumber" ReadOnly="True" SortExpression="IMEI">
                                <ControlStyle Width="180px" />
                                <ItemStyle Width="180px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreatedOn" HeaderText="Requested Date" SortExpression="CreatedOn" DataFormatString="{0:d}">
                                <ControlStyle Width="180px" />
                                <ItemStyle Width="180px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StaffId" HeaderText="Staff Incharge" SortExpression="StaffId">
                                <ControlStyle Width="250px" />
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="ENDPOINT">
                                <ItemTemplate>
                                    <asp:Button ID="btnEndPoint" runat="server" CommandArgument='<%# Bind("Rcno") %>' 
                                        Text="EndPoint" OnClick="btnEndPoint_Click" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEndPoint" runat="server" Text='<%# Bind("Rcno") %>' Visible="false"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                               <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" Visible="false">
                                <ControlStyle Width="125px" />
                                <ItemStyle Width="125px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks">
                                <ControlStyle Width="450px" />
                                <ItemStyle Width="450px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                <ControlStyle Width="125px" />
                                <ItemStyle Width="125px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Change Status">
                                <ItemTemplate>
                                    <asp:Button ID="btnChST" runat="server" CommandArgument='<%# Bind("Rcno") %>' CommandName="ChST"
                                        Text="CH ST" OnClick="btnChST_Click" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtChST" runat="server" Text='<%# Bind("Rcno") %>' Visible="false"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="MobileAppVersion" HeaderText="MobileAppVersion" SortExpression="MobileAppVersion">
                                <ControlStyle Width="125px" />
                                <ItemStyle Width="125px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:BoundField DataField="StaffName" HeaderText="StaffName" SortExpression="StaffName">
                                <ControlStyle Width="250px" />
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                            </asp:BoundField>
                               <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" SortExpression="MobileNo">
                                <ControlStyle Width="250px" />
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                            </asp:BoundField>
                               <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                                <ControlStyle Width="250px" />
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                            </asp:BoundField>
                               <asp:TemplateField HeaderText="Used">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkUsed" runat="server"  Enabled="false" Checked='<%#If(Eval("Used").ToString() = "Y", True, False)%>' />
                                                 </ItemTemplate>
                                                   </asp:TemplateField>
                              <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                                                  <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                                 <asp:TemplateField HeaderText="WebServiceUrl" SortExpression="WebServiceUrl" Visible="False">
                                   <EditItemTemplate>
                                       <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("WebServiceUrl") %>'></asp:TextBox>
                                   </EditItemTemplate>
                                   <ItemTemplate>
                                       <asp:Label ID="lblURL" runat="server" Text='<%# Bind("WebServiceUrl") %>'></asp:Label>
                                   </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="RcNo" HeaderText="RcNo" SortExpression="RcNo" />
                          
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
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                        <table style="width: 100%; text-align: center;">
              
            <tr>
                <td class="CellFormatADM" style="width:30%">DeviceId<asp:Label ID="Label23" runat="server" Visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtDeviceId" runat="server" MaxLength="50" Height="16px" Width="200px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox></td>
            </tr>
                             <tr>
                <td class="CellFormatADM">Registration Number</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtIMEI" runat="server" MaxLength="50" Height="16px" Width="200px" Enabled="false" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CellFormatADM">Confirmation Code</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtConfirmationCode" runat="server" MaxLength="50" Height="16px" Width="200px" Enabled="false" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr>
           
            <tr>
                <td class="CellFormatADM">Device Type</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtDeviceType" runat="server" MaxLength="50" Height="16px" Width="200px" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr>
           
            <tr>
                <td class="CellFormatADM">Staff Incharge</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtStaff" runat="server" MaxLength="50" Height="16px" Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:ModalPopupExtender ID="modPopUPStaff" runat="server" CancelControlID="btnPopUpPnlStaffClose" PopupControlID="pnlPopUpStaff"
                        TargetControlID="imgBtnStaff" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:ImageButton ID="imgBtnStaff" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                        Height="15" Width="20" /></td>
            </tr>
            <tr>
                <td class="CellFormatADM">Status<asp:Label ID="Label4" runat="server" Visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                <td class="CellTextBoxADM">
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="false" AppendDataBoundItems="true"
                        Width="200px">
                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                        <asp:ListItem Text="Approve" Value="APPROVED"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="PENDING"></asp:ListItem>
                        <asp:ListItem Text="Deactivate" Value="DEACTIVATED"></asp:ListItem>
                        <asp:ListItem Text="Decline" Value="DECLINED"></asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
                                        <tr>
                <td class="CellFormatADM">StaffName</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtStaffNameNew" runat="server" MaxLength="50" Height="16px" Width="200px" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr>
                              <tr>
                <td class="CellFormatADM">MobileNumber</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="50" Height="16px" Width="200px" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr>
                             <tr>
                <td class="CellFormatADM">EmailAddress</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" Height="16px" Width="600px" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr>
                <tr>
                <td class="CellFormatADM">Country<asp:Label ID="Label3" runat="server" Visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                <td class="CellTextBoxADM">
                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Enabled="true" Width="200px">
                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>                      
                    </asp:DropDownList>

                    <asp:TextBox ID="txtCountryRcNo" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
               <tr>
                <td class="CellFormatADM">WebServiceURL</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtWebServiceURL" runat="server" MaxLength="200" Height="16px" Width="600px" Enabled="true" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr>
                  
            <tr>
                <td class="CellFormatADM">Remarks</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtRemarks" runat="server" MaxLength="50" Height="16px" Width="600px" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr></table>
                              </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <asp:Button ID="btnSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="SAVE" Width="100px" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="CANCEL" Width="100px" /></td>
            </tr>
        </table>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
            SelectCommand="SELECT * FROM tblMobileDevice  order by rcno desc"></asp:SqlDataSource>
         <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>

        <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedOn" runat="server" Visible="False" CssClass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtChStatusRcNo" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtChStatusDeviceId" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtEndPointRcno" runat="server" Visible="False"></asp:TextBox>
        <%--  <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">IMEI
                             </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchIMEI" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>--%>
    </div>

    <asp:Panel ID="pnlPopUpStaff" runat="server" BackColor="White" Width="900" Height="600" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">
        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Staff</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="btnPopUpPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

        <div class="wrp">
            <div class="frm">
                <table style="text-align: center;">
                    <tr>
                        <td style="font-size: 15px; font-weight: bold; font-family: 'Comic Sans MS'; color: black; text-align: left; padding-left: 40px;">&nbsp;Search Staff Name</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtStaffName" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            <asp:Button ID="btnPopUpStaffSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="SEARCH" Width="100px" Visible="true"
                                OnClick="btnPopUpStaffSearch_Click" />
                        </td>
                        <td style="text-align: right;">
                            <asp:Button ID="btnPopUpStaffReset" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="RESET" Width="100px" Visible="true"
                                OnClick="btnPopUpStaffReset_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvStaff" runat="server" DataSourceID="SqlDSStaff" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="700px" RowStyle-HorizontalAlign="Left" PageSize="12">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="StaffID" HeaderText="Staff ID" SortExpression="StaffID">
                        <ControlStyle Width="250px" />
                        <HeaderStyle Width="250px" />
                        <ItemStyle Width="250px" Wrap="true" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                        <ControlStyle Width="200px" />
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" Wrap="true" />
                    </asp:BoundField>
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
            <asp:SqlDataSource ID="sqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblStaff where (Rcno &lt;&gt; 0)  and Status='O' order by Name"></asp:SqlDataSource>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlPopUpChStatus" runat="server" BackColor="White" Width="800" Height="400" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">
        <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: center; padding-left: 20px;">
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">EDIT RECORD</h4>
                </td>
            </tr>
        </table>


        <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: center; padding-left: 20px;">
            <tr>
                <td class="CellFormatADM">Device Id</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtPopUpChStatusDeviceId" runat="server" MaxLength="50" Height="16px" Width="250px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CellFormatADM">Device Type</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtPopUpChStatusDeviceType" runat="server" MaxLength="50" Height="16px" Width="250px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CellFormatADM">Identity Number</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtPopUpChStatusIMEI" runat="server" MaxLength="50" Height="16px" Width="250px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="CellFormatADM">Status</td>
                <td class="CellTextBoxADM">
                    <asp:RadioButtonList ID="rbtnPopUpChStatus" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Value="APPROVED">Approve</asp:ListItem>
                        <asp:ListItem Value="DECLINED">Decline</asp:ListItem>
                        <asp:ListItem Value="DEACTIVATED">Deactivate</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="CellFormatADM">Staff Incharge</td>
                <td class="CellTextBoxADM">
                    <asp:DropDownList ID="ddlPopUpChStatusStaff" runat="server" AutoPostBack="False" DataSourceID="SqlDSPopUpChStatusStaff"
                        DataTextField="StaffId" DataValueField="Name" Width="255px" AppendDataBoundItems="true">
                        <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
           <%--   <tr>
                <td class="CellFormatADM">Country<asp:Label ID="Label5" runat="server" Visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                <td class="CellTextBoxADM">
                    <asp:DropDownList ID="ddlCountryUpdate" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Enabled="true" Width="225px">
                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                    
                    </asp:DropDownList></td>
            </tr>
             
               <tr>
                <td class="CellFormatADM">WebServiceURL</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtWebServiceURLUpdate" runat="server" MaxLength="200" Height="16px" Width="600px" Enabled="true" AutoCompleteType="Disabled"></asp:TextBox></td>
            </tr>--%>
            <tr>
                <td class="CellFormatADM">Remarks</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtPopUpChStatusRemarks" runat="server" MaxLength="50" Height="16px" Width="250px" 
                        AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
            <tr style="padding-top: 40px;">
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnPopUpChStatusOk" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="OK" Width="120px" OnClick="btnPopUpChStatusOk_Click" />
                    <asp:Button ID="btnPopUPChStatusCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="100px" OnClick="btnPopUPChStatusCancel_Click" />
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDSPopUpChStatusStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
            ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
            SelectCommand="SELECT distinct UPPER(NAME) AS Name,UPPER(STAFFID) AS StaffID from tblStaff where (Rcno &lt;&gt; 0)  and Status='O' order by Name"></asp:SqlDataSource>
    </asp:Panel>

    <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mdlPopUpChStatus" runat="server" TargetControlID="btnDummy"
        PopupControlID="pnlPopUpChStatus" BackgroundCssClass="modalBackground" />


     <asp:Panel ID="pnlPopupEndPoint" runat="server" BackColor="White" ScrollBars="vertical" style="overflow:scroll" Wrap="false" Width="900" Height="500" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left">
        <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: center; padding-left: 20px;">
            <tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">END POINTS</h4> </td>
                  <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlEndPointClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                           
                  </tr>  
                                      <tr>
               <td colspan="3" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageEndPoint" runat="server"></asp:Label>
                      </td> 
            </tr>                           
        </table>


        <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: center; padding-left: 10px;">
            <tr>
                <td class="CellFormatADM" style="width:20%">Device Id</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtEndPointDeviceID" runat="server" MaxLength="50" Height="16px" Width="250px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CellFormatADM">Device Type</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtEndPointDeviceType" runat="server" MaxLength="50" Height="16px" Width="250px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CellFormatADM">Identity Number</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtEndPointIMEI" runat="server" MaxLength="50" Height="16px" Width="250px" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
            </tr>
          <tr>
              <td colspan="2">

                  <%-- <div id="DivRoot" align ="center">--%>
   <%-- <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll; color: #000000; background-color: #FFFFFF;text-align:center" onscroll="OnScrollDiv(this)" id="DivMainContent">
    --%>    

                      <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table_head_bdr " >
                          <Columns>
                                  <asp:TemplateField ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center"> <HeaderTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" TextAlign="Right" onchange="checkalllocationrecs()"  Width="5%" ></asp:CheckBox></HeaderTemplate>
                <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Checked='<%# Bind("Selected")%>' Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
                              <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Font-Names="Calibri" HeaderText="Country"><ItemTemplate><asp:Label ID="txtCountryGV" runat="server" Text='<%# Bind("Country")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px"  Width="100%"></asp:Label></ItemTemplate></asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Font-Names="calibri" HeaderText="WebServiceURL"><ItemTemplate><asp:Label ID="txtWebServiceURLGV" runat="server" Text='<%# Bind("WebServiceURL")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="100%"></asp:Label></ItemTemplate></asp:TemplateField>
          <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Font-Names="calibri" HeaderText="ID" Visible="true"><ItemTemplate><asp:Label ID="txtCountryRcnoGV" runat="server" Text='<%# Bind("RcNo")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="100%"></asp:Label></ItemTemplate></asp:TemplateField>
       
                             <%--  <asp:BoundField DataField="AccountID" HeaderText="AccountID" SortExpression="AccountID" >
                       <ControlStyle Width="150px" />
                       <HeaderStyle Width="150px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>
                              <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>--%>
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

        <%--  </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div> --%>
              </td>
          </tr>

              <tr style="padding-top: 40px;">
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnSaveEndPoint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="SAVE" Width="120px" />
                           <asp:Button ID="btnCancelEndPoint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="CANCEL" Width="120px" />
            
                </td>
            </tr>

        </table>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
            ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
    </asp:Panel>

    <asp:Button ID="btnDummyEndPoint" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mdlPopupEndPoint" runat="server" TargetControlID="btnDummyEndPoint"
        PopupControlID="pnlPopUpEndPoint" CancelControlID="btnCancelEndPoint" BackgroundCssClass="modalBackground" />

   <%--  <asp:Panel ID="pnlPopUpAddEndPoint" runat="server" BackColor="White" Width="900" Height="400" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">
        <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: center; padding-left: 20px;">
            <tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">ADD END POINTS</h4> </td>
               
                           </tr>
                              
        </table>


        <table style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: center; padding-left: 10px;">
             <tr>
                <td class="CellFormatADM">Country<asp:Label ID="Label6" runat="server" Visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                <td class="CellTextBoxADM">
                    <asp:DropDownList ID="ddlCountryAddEndPoint" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Enabled="true" Width="225px">
                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                 
                      
                    </asp:DropDownList></td>
            </tr>
             
               <tr>
                <td class="CellFormatADM">WebServiceURL</td>
                <td class="CellTextBoxADM">
                    <asp:TextBox ID="txtWebServiceUrlAddEndPoint" runat="server" MaxLength="200" Height="16px" Width="600px" Enabled="true" AutoCompleteType="Disabled"></asp:TextBox></td>
                               <asp:TextBox ID="txtAddEndPointRcNo" runat="server" MaxLength="200" Height="16px" Width="600px" Enabled="true" AutoCompleteType="Disabled"></asp:TextBox></td>
        
            </tr>
               <tr style="padding-top: 40px;">
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnSaveEndPoint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="OK" Width="120px" />
                    <asp:Button ID="btnCancelEndPoint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
       
        </table>
        
    </asp:Panel>

    <asp:Button ID="btnDummyAddEndPoint" runat="server" Style="display: none;" />
    <asp:ModalPopupExtender ID="mdlPopupAddEndPoint" runat="server" TargetControlID="btnDummyAddEndPoint"
        PopupControlID="pnlPopUpAddEndPoint" CancelControlID="btnPnlCancelEndPoint" BackgroundCssClass="modalBackground" />--%>


       <asp:Panel ID="pnlConfirmMsg" runat="server" BackColor="White" Width="900px" Height="320px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                
               <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr> 
               <tr>
                      <td colspan="2" class="CellFormat" style="text-align:left;padding-left:100px;">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text=""></asp:Label>
                        
                      </td>
                           </tr>
                <tr>
                             <td colspan="2"><br /></td>
                         </tr>
             
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmOk" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Update" Width="100px"/>
                                 <asp:Button ID="btnCancelMsg" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />

    <asp:Panel ID="pnlSearch" runat="server" BackColor="White" Width="550" Height="350" BorderColor="#003366" BorderWidth="1" Visible="true">
              
                     <table style="width:100%;">
                         <tr><td colspan="3"><br /></td></tr>
            <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">DeviceID
                             </td>
                           <td class="CellTextBoxADM">    <asp:TextBox ID="txtSearchDeviceID" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
                          <tr><td style="width:10%"></td>
                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Identity No
                               </td>
                           <td class="CellTextBoxADM">    <asp:TextBox ID="txtSearchIdentityNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td><td style="width:10%"></td>
            </tr>
                            <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Status
                             </td>
                           <td class="CellTextBoxADM">  
                        <asp:DropDownList ID="ddlSearchStatus" runat="server" AutoPostBack="false" AppendDataBoundItems="true"
                        Width="200px">
                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                        <asp:ListItem Text="Approve" Value="APPROVED"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="PENDING"></asp:ListItem>
                        <asp:ListItem Text="Deactivate" Value="DEACTIVATED"></asp:ListItem>
                        <asp:ListItem Text="Decline" Value="DECLINED"></asp:ListItem>
                    </asp:DropDownList>     </td>
            </tr>
                         <%--  <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">IMEI
                             </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchIMEI" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>--%>
                          <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Staff ID
                             </td>
                           <td class="CellTextBoxADM">    <asp:TextBox ID="txtSearchStaffID" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
                             <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">EmailAddress
                             </td>
                           <td class="CellTextBoxADM">    <asp:TextBox ID="txtSearchEmailAddress" runat="server" MaxLength="100" Height="16px" Width="400px"></asp:TextBox>
                            </td>
            </tr>
                           <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Remarks
                             </td>
                           <td class="CellTextBoxADM">    <asp:TextBox ID="txtSearchRemarks" runat="server" MaxLength="100" Height="16px" Width="400px"></asp:TextBox>
                            </td>
            </tr>
           <%-- <tr><td style="width:10%"></td>
                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Asset RegNo
                               </td>
                           <td>    <asp:TextBox ID="txtSearchAssetRegNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td><td style="width:10%"></td>
            </tr>--%>
          <%--  <tr><td style="width:10%"></td>
                  <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">InCharge ID</td>
                           <td>           
                               <asp:DropDownList ID="ddlSearchInchargeID" runat="server" DataSourceID="SqlDataSource2" DataTextField="StaffId" DataValueField="StaffId" Width="155px" AutoPostBack="false">
                               </asp:DropDownList>
                                   <ajaxToolKit:ListSearchExtender ID="ddllsSearchIncharge" runat="server" TargetControlID="ddlSearchInchargeID" PromptPosition="Bottom"></ajaxToolKit:ListSearchExtender>
    
                            </td><td style="width:10%"></td>
            </tr>--%>
          <%--  <tr><td style="width:10%"></td>
                 <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Status</td>
                           <td class="CellFormat">      
                            <asp:DropDownList ID="ddlSearchStatus" CssClass="chzn-select" runat="server" DataSourceID="SqlDSAssetStatus" DataTextField="Status" DataValueField="Available" Width="150px" AppendDataBoundItems="true" Enabled="true">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
                               </td><td style="width:10%"></td>
            </tr>--%>
            <tr><td style="width:10%"></td><td><br /></td><td></td></tr>
            <tr><td colspan="2" style="text-align:center"></td><td><asp:Button ID="btnSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Search" Width="100px"/>
                <asp:Button ID="btnClose" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
            <td style="width:10%"></td></tr>
        </table>
           </asp:Panel>

              
           <asp:ModalPopupExtender ID="mdlPopupSearch" runat="server" CancelControlID="btnClose" PopupControlID="pnlSearch" TargetControlID="btnDummyFilter" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
      <asp:Button ID="btnDummyFilter" runat="server" CssClass="dummybutton" />

</asp:Content>

