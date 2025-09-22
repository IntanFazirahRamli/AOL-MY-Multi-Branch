<%@ Page Title="Batch Email" Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeFile="BatchEmail.aspx.vb" Inherits="BatchEmail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
         .Hide
    {
        display: none;
    }
        .CellFormat {
            font-size: 15px;
            font-weight: bold;
            font-family: Calibri;
            color: black;
            text-align: left;
            width: 10%;
            /*table-layout:fixed;
        overflow:hidden;*/
            border-collapse: collapse;
            border-spacing: 0;
            line-height: 10px;
        }

        .CellTextBox {
            color: black;
            text-align: left;
            width: 40%;
            /*table-layout:fixed;
        overflow:hidden;*/
            /*border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;*/
        }

        .auto-style1 {
            color: black;
            text-align: left;
            width: 15%;
            /*table-layout:fixed;
        overflow:hidden;*/
            /*border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;*/
        }

        .auto-style2 {
            width: 202px;
        }

        .auto-style3 {
            width: 90px;
        }

        .auto-style4 {
            width: 110px;
        }

        .auto-style5 {
            width: 104%;
        }

         .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }

    </style>
    <script type="text/javascript">



        function checkmultiprint() {

            var table = document.getElementById('<%=GridView1.ClientID%>');
        var totbillamt = 0;


        if (table.rows.length > 0) {
            //alert("2");

            var input = table.rows[0].getElementsByTagName("input");

            if (input[0].id.indexOf("chkSelectAllMultiPrintGV") > -1) {

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

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });


        function DoValidationSvc(parameter) {
            var stat = document.getElementById("<%=txtSvcDateFrom.ClientID%>").value;
        }

        function ResetScrollPosition() {
            setTimeout("window.scrollTo(0,0)", 0);
        }

        function checkDate() {
            var allowBlank = true;
            var minYear = 1902;
            var maxYear = (new Date()).getFullYear();

            var errorMsg = "";

            // regular expression to match required date format
            re = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
            var field = document.getElementById("<%=txtSvcDateFrom.ClientID%>").value;

            alert(field);


            if (field.value != '') {
                if (regs = field.value.match(re)) {
                    if (regs[1] < 1 || regs[1] > 31) {
                        errorMsg = "Invalid value for day: " + regs[1];
                    } else if (regs[2] < 1 || regs[2] > 12) {
                        errorMsg = "Invalid value for month: " + regs[2];
                    } else if (regs[3] < minYear || regs[3] > maxYear) {
                        errorMsg = "Invalid value for year: " + regs[3] + " - must be between " + minYear + " and " + maxYear;
                    }
                } else {
                    errorMsg = "Invalid date format: " + field.value;
                }
            } else if (!allowBlank) {
                errorMsg = "Empty date not allowed!";
            }

            if (errorMsg != "") {
                alert(errorMsg);
                field.focus();
                return false;
            }

            return true;
        }

        function DoValidationSvc1(parameter) {
            var stat = document.getElementById("<%=txtSvcDateTo.ClientID%>").value;
        }

        function checkDate1() {
            var allowBlank = true;
            var minYear = 1902;
            var maxYear = (new Date()).getFullYear();

            var errorMsg = "";

            // regular expression to match required date format
            re = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
            var field = document.getElementById("<%=txtSvcDateTo.ClientID%>").value;

            alert(field);


            if (field.value != '') {
                if (regs = field.value.match(re)) {
                    if (regs[1] < 1 || regs[1] > 31) {
                        errorMsg = "Invalid value for day: " + regs[1];
                    } else if (regs[2] < 1 || regs[2] > 12) {
                        errorMsg = "Invalid value for month: " + regs[2];
                    } else if (regs[3] < minYear || regs[3] > maxYear) {
                        errorMsg = "Invalid value for year: " + regs[3] + " - must be between " + minYear + " and " + maxYear;
                    }
                } else {
                    errorMsg = "Invalid date format: " + field.value;
                }
            } else if (!allowBlank) {
                errorMsg = "Empty date not allowed!";
            }

            if (errorMsg != "") {
                alert(errorMsg);
                field.focus();
                return false;
            }

            return true;
        }
    </script>
   
    <div>
           <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
        <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">SERVICE REPORT BATCH EMAIL</h4>

        <table style="width: 100%; text-align: center;">
            <tr>
                <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                    <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
        </table>
        <table id="tablesearch" border="0" runat="server" style="border: 1px solid #CC3300; text-align: right; width: 100%; border-radius: 25px; width: 90%; height: 60px; background-color: #F3F3F3;">
            <tr>
                <td style="text-align: left;">

                    <table border="0" style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000; padding-left: 2px;width:90%;text-align:center">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; text-align: right;width:15%">Service Date From               
                            </td>
                            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:15%">
                                <asp:TextBox ID="txtSvcDateFrom" runat="server" Style="text-align: left;" MaxLength="50" Height="20px" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom"
                                    Enabled="True" />
                            </td>
                             <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; text-align: right;width:15%">Service Date To                
                            </td>
                            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:15%">
                                <asp:TextBox ID="txtSvcDateTo" runat="server" Style="text-align: left;" MaxLength="50" Height="20px" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo"
                                    Enabled="True" />
                            </td>
                            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; text-align: right;width:10%;">Service By                
                            </td>
                            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:20%;" colspan="1">
                                <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataSourceID="SqlDSTeam" DataTextField="StaffId"
                                    DataValueField="StaffId" Width="150px" Height="20px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                </asp:DropDownList>
                            </td>
                            
                                 <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:10%;" colspan="1">
                                <asp:Button ID="btnSearch" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Search" Width="88px" 
                                   Style="position: static"  OnClick="btnSearch_Click" />
                            </td>

                        </tr>
                        <tr>
                              <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: right; color: black; text-align: right;" >ClientName                
                            </td>
                              <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:100%" colspan="3" >
                          
                              <asp:TextBox ID="txtSearch1ClientName" runat="server" Width="100%"></asp:TextBox> 
                  <%--  <asp:ImageButton ID="btnAccountNameSearch" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Middle"/>--%>
                                 </td>
                                <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: right; color: black; text-align: right;" >LocationID                
                            </td>
                              <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:100%" colspan="1" >
                                <asp:TextBox ID="txtServiceLocationID" runat="server" MaxLength="17" Height="16px" Width="150px"></asp:TextBox>
                      </td>
                             <%-- <asp:ImageButton ID="btnSvcLocation" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
                        <asp:TextBox ID="txtModal" runat="server" CssClass="dummybutton"></asp:TextBox>--%>
                             <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;" colspan="1">
                                <asp:Button ID="btnReset" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Reset" Style="margin-left: 0" Width="88px" />
                            </td>
                        </tr>
                        <tr>
                              <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: right; color: black; text-align: right;" >ContractNo                
                            </td>
                              <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:100%" colspan="3" >
                          <asp:TextBox ID="txtContractNo" runat="server" MaxLength="30" Height="16px" Width="100%" Visible="True"></asp:TextBox>
                                 </td>
                                 
                            <td class="CellFormat" colspan="2" style="text-align:center;">
                                <asp:CheckBox ID="chkSign" runat="server" Text="Include Unsigned Service Reports" Width="250px" /></td>
                      
                         
                          
                        </tr>
                         <tr>
           <td class="CellFormat" style="text-align:right;" >
               <asp:Label ID="Label1" runat="server" Text="Branch/Location" ></asp:Label></td>
 <td class="CellTextBox" colspan="5">
               <asp:CheckBoxList ID="chkBranch" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"></asp:CheckBoxList>
           </td>
                             <td> <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" CssClass="dummybutton"></asp:TextBox></td>
       </tr>
      

                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table border="0" style="text-align: center; padding-top: 2px; margin-left: 0%; margin-right: 0%" class="nav-justified">
        <tr style="text-align: center;">
            <td colspan="12" style="width: 100%; text-align: center">
                <div style="text-align: center; width: 100%; margin-left: auto; margin-right: auto;">

                    <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="auto"
                        Style="text-align: center; width: 1350px; margin-left: auto; margin-right: auto;" Visible="true" Width="1330px">

                        <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False"
                            BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSService" PageSize="50">
                            <Columns>
<asp:TemplateField> 
               <HeaderTemplate><asp:CheckBox ID="chkSelectAllMultiPrintGV" runat="server" AutoPostBack="false" TextAlign="left" onchange="checkmultiprint()" Width="10px" ></asp:CheckBox></HeaderTemplate>    <HeaderStyle HorizontalAlign="center" />
               <ItemTemplate><asp:CheckBox ID="chkSelectMultiPrintGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="10px" AutoPostBack="false" CommandName="CHECK" ></asp:CheckBox></ItemTemplate>
                                                              <ItemStyle HorizontalAlign="center" />
                                                          </asp:TemplateField>      
                                <%-- <asp:CommandField ShowHeader="True" ShowSelectButton="True" Visible="false"></asp:CommandField>--%>
                             <%--   <asp:TemplateField>
                                 
                                    <ItemStyle Width="1%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="dummybutton" />
                                    <HeaderStyle CssClass="dummybutton" />
                                    <ItemStyle CssClass="dummybutton" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="Status" HeaderText="Status">
                                    <ItemStyle Width="2%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="EmailSent" HeaderText="Email Sent">
                                    <ItemStyle Width="2%" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location">
                                    <ControlStyle Width="6%" />
                                    <ItemStyle Wrap="False" Width="6%" />
                                </asp:BoundField>
                           

                                  <asp:BoundField DataField="RecordNo" HeaderText="Record No" SortExpression="RecordNo">
                                    <ControlStyle Width="6%" />
                                    <ItemStyle Wrap="False" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ServiceDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Service Date" SortExpression="ServiceDate">
                                    <ItemStyle Width="5%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CustName" HeaderText="Client" SortExpression="CustName">
                                    <ControlStyle Width="12%" />
                                    <ItemStyle Width="12%" Wrap="False" HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField DataField="LocationId" HeaderText="Location Id" SortExpression="LocationId">
                                    <ControlStyle Width="6%" />
                                    <ItemStyle Wrap="False" Width="6%" />
                                </asp:BoundField>
                                     <asp:BoundField DataField="ContractNo" HeaderText="Contract No" SortExpression="ContractNo">
                                    <ControlStyle Width="6%" />
                                    <ItemStyle Wrap="False" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ServiceBy" HeaderText="Service By" SortExpression="ServiceBy">
                                    <ControlStyle Width="6%" />
                                    <ItemStyle Width="6%" Wrap="False" HorizontalAlign="Left" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="Email" HeaderText="Email">
                                 <ControlStyle Width="16%" />
                                    <ItemStyle Width="16%" Wrap="False" HorizontalAlign="Left" />
                                   </asp:BoundField>
                                 <asp:BoundField DataField="Contact2Email" HeaderText="Contact2Email">
                                 <ControlStyle Width="16%" />
                                    <ItemStyle Width="16%" Wrap="False" HorizontalAlign="Left" />
                                   </asp:BoundField>
                                 <asp:BoundField DataField="OtherEmail" HeaderText="OtherEmail">
                                    <ControlStyle Width="16%" />
                                    <ItemStyle Width="16%" Wrap="False" HorizontalAlign="Left" />
                                   </asp:BoundField>
                              <%--    <asp:TemplateField HeaderText="AccountId" InsertVisible="False" SortExpression="AccountId" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccountId" runat="server" Text='<%# Bind("AccountId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="dummybutton" />
                                    <HeaderStyle CssClass="dummybutton" />
                                    <ItemStyle CssClass="dummybutton" />
                                </asp:TemplateField>--%>



                                <asp:BoundField DataField="AccountId" HeaderText="AccountId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" >
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="InchargeId" HeaderText="InchargeId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="OurRef" HeaderText="OurRef" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="Address1" HeaderText="Address1" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="AddUnit" HeaderText="AddUnit" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="AddBuilding" HeaderText="AddBuilding" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="AddStreet" HeaderText="AddStreet" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="AddCity" HeaderText="AddCity" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="AddState" HeaderText="AddState" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="AddCountry" HeaderText="AddCountry" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                                  <asp:BoundField DataField="AddPostal" HeaderText="AddPostal" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>
                               
                                <asp:BoundField DataField="CustomerSign" HeaderText="CustomerSign" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
                                   <HeaderStyle CssClass="Hide" />
                                   <ItemStyle CssClass="Hide" />
                                   </asp:BoundField>

                                <%--  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                                <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" />
                                <asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" />
                                <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Edited On" SortExpression="LastModifiedOn" />--%>
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="left" />
                            <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#e4e4e4" />
                            <SortedAscendingHeaderStyle BackColor="#000066" />
                            <SortedDescendingCellStyle BackColor="#e4e4e4" />
                            <SortedDescendingHeaderStyle BackColor="#000066" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>

    <br />

    <table>
        <tr>


            <td colspan="3" style="width: 10%; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right;"></td>
            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%;">
                <%-- <asp:Button ID="btnViewPdf" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True"  TabIndex="28" Text="View as Pdf" Width="150px" />
                        &nbsp;--%>
                <asp:Button ID="btnSendEmail" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Email Service Report(s)" Width="200px" />
    </table>

          <asp:Panel ID="pnlConfirmMsg" runat="server" BackColor="White" Width="330px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Mail Sent Successfully"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmOk" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelMsg" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />

     <asp:Label ID="lblRecordNo" runat="server" Text="" Visible="false"></asp:Label>
     <asp:TextBox ID="txtSubject" runat="server" Visible="False"></asp:TextBox>
     <asp:TextBox ID="txtContent" runat="server" Visible="False"></asp:TextBox>
     <asp:TextBox ID="txtTo" runat="server" Visible="False"></asp:TextBox>
     <asp:TextBox ID="txtFrom" runat="server" Visible="False"></asp:TextBox>
     <asp:TextBox ID="txtCC" runat="server" Visible="False"></asp:TextBox>

    <asp:SqlDataSource ID="SQLDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSTeam" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct StaffId FROM tblStaff where Status='O' and Roles='TECHNICAL' ORDER BY StaffId"></asp:SqlDataSource>
</asp:Content>
