<%@ Page Title="Credit Note" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CNProgressBilling.aspx.vb" Inherits="CNProgressBilling" Culture="en-GB"   %>


<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type="text/css">
    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:Calibri;
        color:black;
        text-align:left;
        width:10%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
      .CellTextBox{
        
        color:black;
        text-align:left;
        width:40%;
        /*table-layout:fixed;
        overflow:hidden;*/
          /*border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;*/
    }
  
       
          </style>


         
    
    

<script type="text/javascript">
    window.history.forward(1);
 

    function ConfirmPost() {
        currentdatetime();
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Are you sure to POST the Credit Note?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }

    function ConfirmUnPost() {
        currentdatetime();
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Are you sure to REVERSE the Credit Note?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }

    var defaultTextClient = "Search Here for AccountID or Client Name or Contact Person";
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
   

    function ConfirmSelection() {

        var origAcid = document.getElementById("<%=txtOriginalAccountId.ClientID%>").value;
          if (origAcid == '') {

          }
          else {

              var confirm_value = document.createElement("INPUT");
              confirm_value.type = "hidden";
              confirm_value.name = "confirm_value";
              if (confirm("If you select different Account ID, Contract Details will be Deleted. Do you wish to continue?")) {
                  confirm_value.value = "Yes";
              } else {
                  confirm_value.value = "No";
              }
              document.forms[0].appendChild(confirm_value);
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

        function currentdatetimeinvoice() {
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
            document.getElementById("<%=txtCNDate.ClientID%>").value = dd + "/" + mm + "/" + y;
      

            
        }
    

    function initialize() {
        currentdatetimeinvoice()
        enable();
    }
     



        function DoValidation(parameter) {



            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;



            var linvoicedate = document.getElementById("<%=txtCNDate.ClientID%>").value;

            if (linvoicedate == '') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Credit Note Date";
                ResetScrollPosition();
                document.getElementById("<%=txtCNDate.ClientID%>").focus();
                valid = false;
                return valid;
            }

            var lcompanygroup = document.getElementById("<%=txtCompanyGroup.ClientID%>").value;

            if (lcompanygroup == '') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Company Group";
                ResetScrollPosition();
                document.getElementById("<%=txtCompanyGroup.ClientID%>").focus();
                valid = false;
                return valid;
            }

            var lbillingcode = document.getElementById("<%=ddlItemCode.ClientID%>").value;

            if (lbillingcode == '') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Billing Code";
                ResetScrollPosition();
                document.getElementById("<%=ddlItemCode.ClientID%>").focus();
                valid = false;
                return valid;
            }


            var lcontractno = document.getElementById("<%=ddlContractNo.ClientID%>").value;

            if (lcontractno == '') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Contract No.";
                ResetScrollPosition();
                document.getElementById("<%=ddlContractNo.ClientID%>").focus();
                valid = false;
                return valid;
            }
            currentdatetime();

            return valid;
        };


        function ResetScrollPosition() {
            setTimeout("window.scrollTo(0,0)", 0);
        }

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:UpdatePanel ID="updPanelCN" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                  <asp:controlBundle name="ComboBox_Bundle"/>               
            </ControlBundles>
        </asp:ToolkitScriptManager>
     

    <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />

     <div>
   
     <div style="text-align:center">
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">CREDIT NOTE - Progress Billing</h3>
          
          <asp:UpdatePanel ID="updPnlMsg" runat="server" UpdateMode="Conditional">
              <ContentTemplate>


         <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; "  >
        <tr >

                <td colspan="13"  style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="13"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
            <tr>
                 <td colspan="13"   style="width:100%;text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="txtMode" runat="server" Text="" Visible="true" CssClass="dummybutton"></asp:Label>
                      </td> 
            </tr>

              <tr>
                <td style="width:8%;text-align:center;"> 
                   <asp:Button ID="btnADD" runat="server" Font-Bold="True" Text="CREATE" Width="95%"  CssClass="button" BackColor="#CFC6C0" Visible="TRUE" OnClientClick="initialize()"  />
                 
                      </td>
                  
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnEdit" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="EDIT" Visible="TRUE" Width="95%" />
                    </td>
                     
                   <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnCopy" runat="server" Font-Bold="True" Text="COPY" Width="95%"  CssClass="button" BackColor="#CFC6C0" Visible="False" />
                    </td>
                  <td>
                      <asp:Button ID="btnDelete" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" OnClientClick="Confirm()" Text="DELETE" Width="95%" />
                  </td>
                      

                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnFilter" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="SEARCH" Visible="true" Width="95%" />
                    </td>
                  <td style="width:8%;text-align:center;">
                    <asp:Button ID="btnReset" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="RESET" Width="95%" Visible="true" />
                      </td>
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnPrint" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="PRINT" Visible="true" Width="95%" />
                    </td>
                  <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnPost" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="POST" Width="95%" OnClientClick="ConfirmPost();" />
                     </td>
                  
                    <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="REVERSE" Visible="true" Width="95%" OnClientClick="ConfirmUnPost();"/>
              
                    </td>

                   <td style="width:8%;text-align:center;">
                        <asp:Button ID="btnBack" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Height="30px" Text="BACK" Visible="False" Width="95%" />
                  </td>

                   <td style="width:8%;text-align:center;">
                         &nbsp;</td>

                   <td style="width:8%;text-align:center;">
                         &nbsp;</td>


                  <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="95%" /></td>
                
            </tr>

            <tr>
                 <td style="width:10%;text-align:center;">
                     &nbsp;</td><td style="width:10%;text-align:center;">
                     &nbsp;</td><td style="width:10%;text-align:center;">
                     &nbsp;</td><td style="width:10%;text-align:center;">
                    </td><td style="width:10%;text-align:center;">
                </td><td style="width:10%;text-align:center;">
                     &nbsp;</td><td style="width:10%;text-align:center;">
                </td><td style="width:10%;text-align:center;">
              
                <asp:Label ID="Label40" Style="color: white" runat="server" Visible="false" Text="Label"></asp:Label>    
                     <%-- <asp:Label ID="Label50" Style="color: white" runat="server" Visible="false" Text="Label"></asp:Label>    --%>
             
                 </td>
                
            </tr>
    </table>
       </ContentTemplate>
              </asp:UpdatePanel>
             

          <asp:UpdatePanel ID="updPnlSearch" runat="server" UpdateMode="Conditional"><ContentTemplate>

             <table id="tablesearch" runat="server" style="border: 1px solid #CC3300; text-align:right; width:100%; border-radius: 25px;padding: 2px; width:100%; height:60px; background-color: #F3F3F3;">
            <tr>
                <td style="text-align:left;width:100%;">
                   
                    <table border="0" style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000;width:100%;padding-left:10px;">
                         <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:10%; ">
                                    CN&nbsp; No.                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:15%; ">
                                 <asp:TextBox ID="txtReceiptnoSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" ></asp:TextBox>         
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Company Group
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:15%; ">
                                  <asp:DropDownList ID="ddlCompanyGrpSearch" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Height="20px" Width="80%">
                                      <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                                     
                            <asp:ImageButton ID="ImageButton3" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Middle" Visible="False"     />   
                        
                            </td>

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Account Id
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:15%; ">
                                   <asp:TextBox ID="txtAccountIdSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left; " ></asp:TextBox>         
                                   
                            <asp:ImageButton ID="btnClientSearch" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Middle"     />                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                                                 
                            
                                                      
                            </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    &nbsp;</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:15%; ">
                                  
                                <%--  <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnStatusCancel" DynamicServicePath="" Enabled="True" PopupControlID="pnlStatusSearch" TargetControlID="btnDummyClient">
                                  </asp:ModalPopupExtender>--%>
                             </td>

                             

                              <td colspan="1" style="text-align:center">
                                       <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Search" Width="95%" />
                                 </td>
                        </tr>
                          <tr>

                                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">Billing Period</td>
                              <td>
                                  <asp:TextBox ID="txtBillingPeriodSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left;padding-left:5px;" Width="80%"></asp:TextBox>
                                                     </td>

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Account Type</td>
                             <td>
                                 <asp:DropDownList ID="ddlContactTypeSearch" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="80%">
                                     <asp:ListItem Selected="True" Value="COMPANY">CORPORATE</asp:ListItem>
                                     <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                 </asp:DropDownList>
                    
                            </td>

                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Client Name
                            </td>
                             <td>
                                 <asp:TextBox ID="txtClientNameSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;"></asp:TextBox>         
                    
                            </td>


                                                      


                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">Post Status </td>
                             <td>
                                  <asp:TextBox ID="txtSearch1Status" runat="server" Width="75%" ReadOnly="FALSE"></asp:TextBox>
                                 <asp:ImageButton ID="btnSearch1Status" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" ImageAlign="Middle"
                                    Height="22px" Width="24px" />                       
                            </td>
                            


                              <td> 
                                       <asp:Button ID="btnQuickReset" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Reset" Width="95%" OnClientClick="ClearSearch()" />
                                   </td>
                        </tr>
                    </table>
                      </td>
                    <td style="text-align:right;width:45%;display:inline;vertical-align:middle;padding-top:10px;">
                 
                </td>
            </tr>
        </table>


     


         <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
         
             
                            <tr style="text-align:center;">
                                  <td colspan="11" style="width:100%;text-align:center">
                                      
                                      <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="None"    Visible="true" Width="100%">

                                          <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSCN">
                                                <Columns>
                                                  <asp:CommandField ShowHeader="True" ShowSelectButton="True">
                                                  <ControlStyle Width="4%" />
                                                  <ItemStyle Width="4%" Wrap="False" HorizontalAlign="Left" />
                                                  </asp:CommandField>
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Print St" SortExpression="PostStatus" >
                                                      <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="GlStatus" HeaderText="Post St" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="BankStatus" HeaderText="Paid St" SortExpression="BankStatus" >
                                                      <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CNNumber" HeaderText="CN Number" SortExpression="CNNumber">
                                                    <ControlStyle Width="6%" />
                                                  <ItemStyle Wrap="False" Width="6%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="CNDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CN Date" SortExpression="CNDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GLPeriod" HeaderText="Period" />
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" />
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" />
                                                  <asp:BoundField DataField="CustomerName" HeaderText="Client Name" SortExpression="CustomerName">
                                                    <ControlStyle Width="25%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="True" Width="25%" />
                                                  </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="NetAmount" HeaderText="CN Amount">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Right" Wrap="True" />
                                                  </asp:BoundField>
                                                    <asp:BoundField DataField="PaymentType" HeaderText="Payment Type" Visible="False" />
                                                    <asp:BoundField DataField="Cheque" HeaderText="Cheque No." Visible="False">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ChequeDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Cheque Date" Visible="False">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BankId" Visible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LedgerCode">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LedgerName">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comments">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContractNo" HeaderText="Contract No" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ItemCode" HeaderText="Billing Code" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" >
                                                    <ControlStyle Width="20%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="20%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False" />
                                                  <asp:BoundField DataField="Rcno" InsertVisible="False" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
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
                                                    <asp:BoundField DataField="GSTAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NetAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                              </Columns>
                                              <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                              <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/>
                                              <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                              <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri"/>
                                              <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                              <SortedAscendingCellStyle BackColor="#e4e4e4" />
                                              <SortedAscendingHeaderStyle BackColor="#000066" />
                                              <SortedDescendingCellStyle BackColor="#e4e4e4" />
                                              <SortedDescendingHeaderStyle BackColor="#000066" />
                                      </asp:GridView>


                                              </asp:Panel>
                                  </td>
                              </tr>
             
                 <tr>
                 <td>
                      <asp:SqlDataSource ID="SQLDSCN" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT PostStatus, BankStatus, GlStatus, CNNumber, CNDate, AccountId, AppliedBase, GSTAmount, BaseAmount, CustomerName, NetAmount, GlPeriod, CompanyGroup, ContactType, Cheque, ChequeDate, BankId, LedgerCode, LedgerName, Comments, PaymentType, ItemCode, ItemDescription, ContractNo,CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno FROM tblcn where GLStatus = 'O' and ModuleType='CNPB' ORDER BY Rcno DESC, CustomerName">
                </asp:SqlDataSource>
                     </td><td>
              <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
                           </td><td>
                         &nbsp;</td><td>
               <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
                   </td><td>
              <asp:SqlDataSource ID="SqlDSBankCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(COACode, ' - ', Description) AS codedes FROM tblchartofaccounts WHERE (GLType = 'BANK') ORDER BY COACode"></asp:SqlDataSource>
           </td>
                     <td>
                       <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(industry) FROM tblindustry ORDER BY industry"></asp:SqlDataSource>
                  </td> 

                 <td>
                       <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(contractgroup) FROM tblcontractgroup ORDER BY contractgroup"></asp:SqlDataSource>
                 </td>
                          <td>
                              &nbsp;</td> 
                 <td>
                     &nbsp;</td>     
                <td>&nbsp;</td>
                           </tr>
                                  <tr>
                                      <asp:TextBox ID="txt" runat="server" Height="16px" MaxLength="50" Visible="false" Width="550px"></asp:TextBox>
                                      <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtPopupType" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtRcnotblServiceBillingDetail" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtcontractfrom" runat="server" Visible="False"></asp:TextBox>
                                      
                                     

                              </tr>
       
             </table>
      
        </ContentTemplate>
              </asp:UpdatePanel>

         


        
          <asp:UpdatePanel runat="server" ID="updPnlBillingRecs" UpdateMode="Conditional">
                  <ContentTemplate>
                      
       
          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                           
          
               <tr style="width:100%">
                 <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">CREDIT NOTE INFORMATION
                 </td>
           </tr>
         
                     <tr>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                                <asp:TextBox ID="txtRcnoServiceRecord" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                CN No.<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                          <td style="width:40%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtCNNo" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" BackColor="#CCCCCC" ></asp:TextBox>
                              
                            </td>
                          <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>
              
               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtRcnoServiceRecordDetail" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White" TabIndex="101"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                CN Date<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtCNDate" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="21" AutoPostBack="True" ></asp:TextBox>
                              <asp:CalendarExtender ID="calConDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtReceiptDate" TargetControlID="txtCNDate" Enabled="True" />

                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                </tr>

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">CN Period</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtReceiptPeriod" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Width="80.5%" TabIndex="109" BorderStyle="None"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

              

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Account Type</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="80.5%">
                           <asp:ListItem Selected="True" Value="COMPANY">CORPORATE</asp:ListItem>
                           <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                       </asp:DropDownList>
                       &nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtRcnoInvoice" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White" TabIndex="102"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Account ID <asp:Label ID="Label3" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                       <td style="width:20%;font-size:14px; font-family:'Calibri';color:black;text-align:left;"> 
                               <asp:TextBox ID="txtAccountIdBilling" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="112" ></asp:TextBox>
                              
             &nbsp;<asp:ImageButton ID="btnClient" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" OnClientClick="ConfirmSelection()" Width="24px" />
       
                           
                 <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient" BackgroundCssClass="modalBackground" Enabled="True"  ></asp:ModalPopupExtender>

                            
         
                <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="80%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
                    <table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
                    </td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160; <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
                    <asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Width="24px" Visible="False"></asp:ImageButton>
                    <asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" OnClick="btnPopUpClientReset_Click" Width="24px"></asp:ImageButton>


</td><td>
                <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>

            </td></tr></table><div style="text-align:center; padding-left: 50px; padding-bottom: 5px;"><div class="AlphabetPager">
         <asp:Repeater ID="rptClientAlphabets" runat="server" Visible="False"><ItemTemplate>
        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>' OnClick="ClientAlphabet_Click" ForeColor="Black" />
        </ItemTemplate>
        </asp:Repeater>
                
</div><br />
                    <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                       CellPadding="4" GridLines="None" Font-Size="15px" Width="90%" OnSelectedIndexChanged="gvClient_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
        <ControlStyle Width="5%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Width="5%" />
        </asp:CommandField>
        <asp:BoundField DataField="AccountID" HeaderText="Account Id" SortExpression="AccountID" >
        <ControlStyle Width="8%" />

        <HeaderStyle Wrap="False" HorizontalAlign="Left" />

        <ItemStyle Width="8%" />
        </asp:BoundField>
        <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ID">
        <ControlStyle Width="8%" />

        <HeaderStyle Width="100px" />

        <ItemStyle Width="8%" Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="Client Name" SortExpression="Name">
        <ControlStyle Width="30%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Width="30%" Wrap="True" />
        </asp:BoundField>
        <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
        <ControlStyle Width="30%" />

        <HeaderStyle Wrap="False" HorizontalAlign="Left" />

        <ItemStyle Width="30%" Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="Address1" >
        <ControlStyle CssClass="dummybutton" />

        <HeaderStyle CssClass="dummybutton" />

        <ItemStyle CssClass="dummybutton" />
        </asp:BoundField>
        <asp:BoundField DataField="Mobile" >
        <ControlStyle CssClass="dummybutton" />

        <HeaderStyle CssClass="dummybutton" />

        <ItemStyle CssClass="dummybutton" />
        </asp:BoundField>
        <asp:BoundField DataField="Email" >
        <ControlStyle CssClass="dummybutton" />

        <HeaderStyle CssClass="dummybutton" />

        <ItemStyle CssClass="dummybutton" />
        </asp:BoundField>
        <asp:BoundField DataField="LocateGRP" >
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
        <asp:BoundField DataField="Salesman" HeaderText="Salesman" />
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
                        SelectCommand="SELECT  AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman from tblCompany order by Name"><FilterParameters>
        <asp:ControlParameter Name="ContType" ControlID="ddlContactType" PropertyName="SelectedValue" Type="String" />
        </FilterParameters>
        <SelectParameters>
        <asp:ControlParameter ControlID="ddlContactType" Name="@contType" PropertyName="Text" />
      
        </SelectParameters>
        </asp:SqlDataSource>
                
        </div></asp:Panel>
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtContractNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White" TabIndex="103"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Account Name
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtAccountName" runat="server" Height="16px" Width="80.5%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="113" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td rowspan="4"  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                       <asp:GridView ID="grvGL"  runat="server" AllowSorting="True" 
                             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
                             GridLines="None" Height="12px" 
                            ShowFooter="True" Style="text-align: left" Width="50%"><Columns>
                                            
                                <asp:TemplateField HeaderText="GL Code"><ItemTemplate><asp:TextBox ID="txtGLCodeGV" runat="server" BackColor="#CCCCCC" Font-Size="12px" Height="15px" ReadOnly="true" Width="70px"  AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:TextBox ID="txtGLDescriptionGV" runat="server"  BackColor="#CCCCCC" Font-Size="12px" Visible="true" ReadOnly="true" Height="15px" Width="200px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit Amount"><ItemTemplate><asp:TextBox ID="txtDebitAmountGV" runat="server" BackColor="#CCCCCC" style="text-align:right" Font-Size="12px" ReadOnly="true" Height="15px"  Width="70px" AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemCodeGV_SelectedIndexChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit Amount"><ItemTemplate><asp:TextBox ID="txtCreditAmountGV" runat="server"  BackColor="#CCCCCC" style="text-align:right" Font-Size="12px" Height="15px" ReadOnly="true" Width="70px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                <asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True" Visible="false">
                                <FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" />

                                 </asp:CommandField>
                                <asp:TemplateField>
                                <FooterStyle HorizontalAlign="Left" />
                                <FooterTemplate><asp:Button ID="btnAddDetail" runat="server" Text="Add New Row" ValidationGroup="VGroup" Visible="false" />

                                  </FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField>
                          </Columns>

                     <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" />
                <RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" />


             </asp:GridView>
                           
                             </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                                     <asp:TextBox ID="txtRecordNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="107"></asp:TextBox>
                 </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Company Group </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:TextBox ID="txtCompanyGroup" runat="server" AppendDataBoundItems="True" Height="16px" Width="80.5%" BackColor="#CCCCCC" BorderStyle="None"></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                  <asp:Button ID="btnDummyClient" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
</td>
                        </tr>

                 <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Billing Code <asp:Label ID="Label4" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="ddlItemCode" runat="server" TabIndex="108" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

                 <tr>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">Description</td>
                     <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;vertical-align:top">
                        <asp:TextBox ID="txtARDescription10" runat="server"  Width="80%" TabIndex="108" Height="60px" TextMode="MultiLine" Font-Names="Calibri" Font-Size="15px"  ></asp:TextBox>

                     </td>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Contract No. <asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="ddlContractNo" runat="server" TabIndex="108" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>

                 <tr>
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top">CN Amount</td>
                               <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                   <asp:TextBox ID="txtCNAmount" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%"></asp:TextBox>
                               </td>
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           </tr>

                       

              
                      
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top">Remarks</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left; vertical-align:top">
                               <asp:TextBox ID="txtComments" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px" Height="60px" TabIndex="27" TextMode="MultiLine" Width="80%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>

                       

              
                      
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                           
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                                <asp:TextBox ID="txtPostStatus" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" TabIndex="131"></asp:TextBox>
                          </td>
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                           </tr>
                        
                          
                           <tr>

                                   <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:Button ID="Button3" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" OnClientClick="return DoValidation()" TabIndex="28" Text="SHOW INVOICES" Width="30%" Visible="False" />
                               </td>
                               <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:Button ID="btnShowInvoices" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" OnClientClick="return DoValidation()" TabIndex="28" Text="SHOW INVOICES" Width="30%" Visible="False" />
                               </td>
                                 <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                      <asp:Button ID="btnSave" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="30%" />
                                       <asp:Button ID="btnCancel" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="30%" />
                                 </td>
                                 <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    

                                 </td>
                              
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                                   <asp:TextBox ID="txtTaxRatePct" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="35%"></asp:TextBox>
                               </td>
                           </tr>
                       </tr>
                        </tr>

              </table>

                      <table border="1" style="width:90%; margin:auto">

               
               <tr style="width:100%; padding-left:10%;">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">
                     &nbsp;</td>

           </tr>


                  <tr style="width:95%; padding-left:10%;">
                     <td colspan="10" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
               <asp:UpdatePanel ID="updpnlBillingDetails" runat="server" UpdateMode="Conditional"><ContentTemplate>
             <asp:GridView ID="grvBillingDetails"  runat="server" AllowSorting="True" 
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"   
            ShowFooter="True" Style="text-align: left;  " Width="70%" Visible="False"><Columns>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Enabled="true" Height="15px"  Width="20px"  CommandName="CHECK" AutoPostBack="true" OnCheckedChanged="chkSelectGV_CheckedChanged" ></asp:CheckBox></ItemTemplate></asp:TemplateField>                        
               <asp:TemplateField HeaderText=" Contract No."><ItemTemplate><asp:TextBox ID="txtInvoiceNoGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="200px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=" Contract    Date"><ItemTemplate><asp:TextBox ID="txtInvoiceDateGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="72px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
           
           <%--    <asp:TemplateField HeaderText="   Price"><ItemTemplate><asp:TextBox ID="txtPriceWithDiscGV" runat="server" Text="0.00"  Enabled="false" style="text-align:right" Height="15px" Width="90px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
        
               <asp:TemplateField HeaderText="GST Amt."><ItemTemplate><asp:TextBox ID="txtGSTAmtGV" runat="server" Enabled="false" style="text-align:right" Height="15px" Width="75px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>--%>
               <asp:TemplateField HeaderText="Contract Amount"><ItemTemplate><asp:TextBox ID="txtTotalPriceWithGSTGV" runat="server" Enabled="false" style="text-align:right" Height="15px" Width="110px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Retention Amount"><ItemTemplate><asp:TextBox ID="txtTotalReceiptAmtGV" runat="server" Enabled="false" style="text-align:right" Height="15px" Width="110px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
            <%--  <asp:TemplateField HeaderText="Retention Amount"><ItemTemplate><asp:TextBox ID="txtTotalCreditNoteAmtGV" runat="server" Enabled="false" style="text-align:right" Height="15px" Width="110px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
            --%>    
               <asp:TemplateField HeaderText="CN Amt."><ItemTemplate><asp:TextBox ID="txtReceiptAmtGV" runat="server" Enabled="true" style="text-align:right" Height="15px" Width="110px" Align="right" AutoPostBack="true" OnTextChanged="txtReceiptAmtGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
             
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtLocationGV" runat="server" Visible="false" Height="15px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField ><ItemTemplate><asp:DropDownList ID="txtItemTypeGV" runat="server" Visible="false" Height="20px" ReadOnly="true" Width="0px"  AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem Text="SERVICE" Value="SERVICE" /><asp:ListItem Text="PRODUCT" Value="PRODUCT" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtItemCodeGV" runat="server" Visible="false" Height="15px"  Width="0px" AppendDataBoundItems="True" AutoPostBack="False" > </asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtItemDescriptionGV" runat="server" Visible="false" Height="15px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtOtherCodeGV" runat="server" Visible="False" Enabled="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:DropDownList ID="txtUOMGV" runat="server" Visible="false" Height="20px" Width="0px" AppendDataBoundItems="True"> <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtQtyGV" runat="server" Visible="false" style="text-align:right" Height="15px"  Width="0px" AutoPostBack="true" OnTextChanged="txtQtyGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
             
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:DropDownList ID="txtTaxTypeGV" runat="server" Visible="false" style="text-align:right" Height="20px" Width="0px" AutoPostBack="True" onselectedindexchanged="txtTaxTypeGV_SelectedIndexChanged"></asp:DropDownList></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtGSTPercGV" runat="server" Visible="false" Enabled="false" style="text-align:right" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             

                   <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoReceiptGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>                        
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtARCodeGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtGSTCodeGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 
                  <asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True">
                     <FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" />

                 </asp:CommandField>
                <asp:TemplateField>
                <FooterStyle HorizontalAlign="Left" />
                <FooterTemplate><asp:Button ID="btnAddDetail" runat="server" OnClick="btnAddDetail_Click" Text="Add New Row" ValidationGroup="VGroup" Visible="false" />

                  </FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField>
                          </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" />
                <RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" />


             </asp:GridView>




                   </ContentTemplate>
                   </asp:UpdatePanel>
            </td></tr>

                            </table>


                      


                      <asp:UpdatePanel ID="updPanelSave" runat="server" UpdateMode="Conditional"><ContentTemplate>
                       <table border="0" style="width:70%; margin-left:3%;  border:solid;  " >
                         <tr style="width:95%; padding-left:5%;">
              
                               <td colspan="1"  style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; ">
                                  
                                 </td>
                               <td colspan="1" style="width:200px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   &nbsp;</td>
                               <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                     <asp:TextBox ID="txtTotalWithDiscAmt" runat="server" Height="18px" Width="100%" Font-Bold="true" BackColor="#CCCCCC" AutoCompleteType="Disabled" style="text-align:right" BorderStyle="None" Visible="False"></asp:TextBox>
                             
                                 </td>
                              <td colspan="1" style="width:75px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               <asp:TextBox ID="txtTotalGSTAmt" runat="server" Height="18px" Width="100%" Font-Bold="true" BackColor="#CCCCCC" AutoCompleteType="Disabled" style="text-align:right" BorderStyle="None" Visible="False"></asp:TextBox>
                            
                                 </td>
                              <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    <asp:TextBox ID="txtTotalWithGST" runat="server" Height="18px" Width="95%" Font-Bold="true" BackColor="#CCCCCC" AutoCompleteType="Disabled" style="text-align:right" BorderStyle="None" Visible="False"></asp:TextBox>
                            
                                 </td>

                               <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                  </td>

                              <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                  <asp:TextBox ID="txtReceiptAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Visible="False" Width="98%"></asp:TextBox>
                             
                                  </td>
                              <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>

                             
                      
                      
                        </tr>
                        
                           
                         
                          
                      <tr style="width:95%; padding-left:2%;">
              
                            <td colspan="1" style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;">
                                      <asp:TextBox ID="txtARCode" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                                    
                                 </td>
                               <td colspan="1" style="width:200px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                         <asp:TextBox ID="txtARDescription" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                               
                                 </td>
                               <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                   <asp:TextBox ID="txtOriginalAccountId" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" MaxLength="10" Width="43%"></asp:TextBox>
                               
                                 </td>
                          <td colspan="1" style="width:75px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                     <asp:TextBox ID="txtARCode10" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                                
                                 </td>
                              <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                            
                           
                                 </td>
                             
                                    <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                  
                                 </td>

                            <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                          
                    
                                 </td>
                            
                          <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                                
    
                               
                                   </td>
                             

                        </tr>
                        
            </table>
                      </ContentTemplate></asp:UpdatePanel>
               
          </ContentTemplate>
              </asp:UpdatePanel>
         
                             
        



                           
          <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
            
               
                 <tr style="text-align:center;width:100%">
                         <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" ForeColor="#003366"></asp:TextBox></td>
                                  <td colspan ="2" style="text-align:left; padding-right:2%">
                                              <asp:TextBox ID="txtIsPopup" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>
             
                                      </td>                                             
                                 
                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"> 
                                       &nbsp;</td>
                                  <td style="text-align:left;width:8%">
                                      <asp:Button ID="btnDummy" runat="server" CssClass="roundbutton" Font-Bold="True" Text=" " Width="24px" BackColor="White" BorderStyle="None" />
                                      &nbsp;&nbsp;&nbsp;<asp:Button ID="btnDummyT" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;<asp:Button ID="btnDummyC" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;</td>

                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                  <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>  </td>
                                  <td style="text-align:left;width:10%">
                                   </td>
                                  <td style="text-align: left">
                                      </td>
                                   <td style="text-align: left">
                                  </td>
                            
                              </tr>

    </table>
         </div>

        </ContentTemplate>
</asp:UpdatePanel> 
              
       
</asp:Content>

