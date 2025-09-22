<%@ Page Title="Invoice - Progress Claim" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="InvoiceProgressBilling.aspx.vb" Inherits="InvoiceProgressBilling" Culture="en-GB"   %>


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
        if (confirm("Are you sure to POST the Invoice?")) {
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
        if (confirm("Are you sure to REVERSE the Invoice?")) {
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
            document.getElementById("<%=txtInvoiceDate.ClientID%>").value = dd + "/" + mm + "/" + y;
            
        }
    

    function initialize() {
        currentdatetimeinvoice()
        currentdatetime()
    }


        function DoValidation(parameter) {

            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;

            var linvoicedate = document.getElementById("<%=txtInvoiceDate.ClientID%>").value;

            if (linvoicedate == '') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Invoice Date";
                ResetScrollPosition();
                document.getElementById("<%=txtInvoiceDate.ClientID%>").focus();
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

     <asp:UpdatePanel ID="updPanelInvoice" runat="server" UpdateMode="Conditional">
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
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">INVOICE-PROGRESS BILLING</h3>
          
          <asp:UpdatePanel ID="updPnlMsg" runat="server" UpdateMode="Conditional">
              <ContentTemplate>


         <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; "  >
        <tr >

                <td colspan="12"  style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="12"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
            <tr>
                 <td colspan="12"   style="width:100%;text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
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
                      <asp:Button ID="btnReset" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="RESET" Visible="true" Width="95%" />
                  </td>
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnPrint" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="PRINT" Width="95%" Visible="true" />
                      </td>
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnPrintCertificate" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="PRINT CERTIFICATE" Visible="true" Width="95%" />
                    </td>
                  <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnPost" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="POST" Width="95%" OnClientClick="ConfirmPost();" />
                  </td>
                  
                    <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="REVERSE" Width="95%" OnClientClick="ConfirmUnPost();"/>
                  </td>

                   <td style="width:8%;text-align:center;">
                         <asp:Button ID="btnReceipts" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="RECEIPTS" Visible="true" Width="95%" />
                  </td>

                   <td style="width:8%;text-align:center;">
                         <asp:Button ID="btnQuit" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="QUIT" Width="95%" />
                  </td>


                 
                
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
                                    Invoice No.                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:15%; ">
                                 <asp:TextBox ID="txtInvoicenoSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" ></asp:TextBox>         
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
                                    Account Id</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:15%; ">
                                  <asp:TextBox ID="txtAccountIdSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left; " Width="80%"></asp:TextBox>
                                  <asp:ImageButton ID="btnClientSearch" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                </td>

                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">Salesman</td>
                              <td>
                                  <asp:DropDownList ID="ddlSalesmanSearch" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSSalesMan" DataTextField="StaffId" DataValueField="StaffId" Height="25px" Width="80%">
                                      <asp:ListItem Text="--SELECT--" Value="-1" />
                                  </asp:DropDownList>
                                                     </td>

                              <td colspan="1" style="text-align:center">
                                       <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Search" Width="95%" />
                                 </td>
                        </tr>
                          <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Billing Period
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:15%; ">
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
                                    
                                    Client Name</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:15%; ">
                                  <asp:TextBox ID="txtClientNameSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left;padding-left:5px;" Width="80%"></asp:TextBox>
                            </td>
                            


                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">Post Status </td>
                             <td>
                                  <asp:TextBox ID="txtSearch1Status" runat="server" Width="75%" ReadOnly="FALSE"></asp:TextBox>
                                 <asp:ImageButton ID="btnSearch1Status" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" ImageAlign="Middle"
                                    Height="22px" Width="24px" />  
                                 
                                  <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" CancelControlID="btnStatusCancel" PopupControlID="pnlStatusSearch" TargetControlID="btnSearch1Status" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
                     
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

                                          <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSInvoice">
                                                <Columns>
                                                  <asp:CommandField ShowHeader="True" ShowSelectButton="True">
                                                  <ControlStyle Width="4%" />
                                                  <ItemStyle Width="4%" Wrap="False" HorizontalAlign="Left" />
                                                  </asp:CommandField>
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Print St" SortExpression="PostStatus" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="GlStatus" HeaderText="Post St" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="PaidStatus" HeaderText="Paid St" SortExpression="PaidStatus" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="InvoiceNumber" HeaderText="Invoice Number" SortExpression="InvoiceNumber">
                                                    <ControlStyle Width="6%" />
                                                  <ItemStyle Wrap="False" Width="6%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="SalesDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Sales Date" SortExpression="SalesDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GLPeriod" HeaderText="Period" />
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" />
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" />
                                                  <asp:BoundField DataField="CustName" HeaderText="Client Name" SortExpression="CustName">
                                                    <ControlStyle Width="15%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="True" Width="15%" />
                                                  </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="BillAddress1" HeaderText="Bill Address">
                                                    <ControlStyle Width="15%" />
                                                    <ItemStyle Width="15%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="BillStreet" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillBuilding">
                                                    <ControlStyle Width="8%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle HorizontalAlign="Right" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillPostal" HeaderText="Postal" />
                                                    <asp:BoundField DataField="Salesman" HeaderText="Salesman" />
                                                    <asp:BoundField DataField="AppliedBase" HeaderText="Bill Amount">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="True" />
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
                                                    <asp:BoundField DataField="BillCountry" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PoNo" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="OurRef" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="YourRef" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreditTerms" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DiscountAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GSTAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NetAmount" HeaderText="Net Amount">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BatchNo">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comments">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AmountWithDiscount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RetentionAmount" HeaderText="Retention Amount" />
                                                    <asp:BoundField DataField="RetentionGST" HeaderText="Retention GST" />
                                                    <asp:BoundField DataField="RecurringInvoice" HeaderText="Recurring Invoice" />
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
                      <asp:SqlDataSource ID="SQLDSInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, AppliedBase, Salesman, PoNo, OurRef, yourRef, CreditTerms, DiscountAmount, GSTAmount, NetAmount, GLPeriod, CompanyGroup, ContactType, BatchNo, Salesman, Comments, AmountWithDiscount , RetentionAmount, RetentionGST, STBilling,  RecurringInvoice, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno FROM tblsales WHERE (STBilling = 'Y' and GLStatus = 'O') ORDER BY Rcno DESC, CustName">
                </asp:SqlDataSource>
                     </td><td>
              <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
                           </td><td>
               <asp:SqlDataSource ID="SqlDSSalesGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT salesgroup FROM tblsalesgroup order by salesgroup">
                       
            </asp:SqlDataSource>
                                 </td><td>
               <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
                   </td><td>
              <asp:SqlDataSource ID="SqlDSSalesMan" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId FROM tblstaff where roles= 'SALES MAN' ORDER BY STAFFID"></asp:SqlDataSource>
           </td>
                     <td>
                       <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(industry) FROM tblindustry ORDER BY industry"></asp:SqlDataSource>
                  </td> 

                 <td>
                       <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(contractgroup) FROM tblcontractgroup ORDER BY contractgroup"></asp:SqlDataSource>
                 </td>
                          <td>
                       <asp:SqlDataSource ID="SqlDSTerms" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(Terms) FROM tblTerms ORDER BY Seq"></asp:SqlDataSource>
                  </td> 

                               <td>
              <asp:SqlDataSource ID="SqlDSServiceFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Frequency FROM tblServiceFrequency order by Frequency ">
                       
            </asp:SqlDataSource>
                           </td>

                      <td>
              <asp:SqlDataSource ID="SqlDSBillingFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Frequency FROM tblFrequency  order by Frequency ">
                       
            </asp:SqlDataSource>
                           </td>
                 <td>
                     <asp:SqlDataSource ID="SqlDSContractNo" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT ContractNo FROM tblcontract WHERE (Status = 'O' or Status = 'P') and (AccountID = @AccountId) ORDER BY ContractNo">
                         <SelectParameters>
                             <asp:ControlParameter ControlID="txtAccountId" Name="@AccountId" PropertyName="Text" />
                         </SelectParameters>
                     </asp:SqlDataSource>
                     </td>     
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

         
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate>      
        
        <table border="0"  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; border:solid; border-color:ButtonFace; "  >

              <tr style="width:100%">
                 <td colspan="8" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">SEARCH SERVICE RECORDS <tr>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>

                          <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> &nbsp;</td>
                          <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;" colspan="4"> Display Services&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                               <asp:RadioButton ID="rdbCompleted" runat="server" GroupName="ServiceStatus"  Height="20px"  Text="Completed" AutoPostBack="True" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       
                               
                            &nbsp;&nbsp;

                               
                               <asp:RadioButton ID="rdbNotCompleted" runat="server" GroupName="ServiceStatus" Height="20px"  Text="Not Completed" AutoPostBack="True" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                               <asp:RadioButton ID="rdbAll" runat="server" GroupName="ServiceStatus" Height="20px"  Text="All" AutoPostBack="True" />

                               
                            </td>
                                             
                           
                          
                         

            
               <tr>
                            <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Company Group</td>

                          <td style="width:9%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Account Type </td>

                          <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Account Id
                        
                               
                            </td>

                           <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Client Name
                        
                               
                            </td>

                         <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Location
                        
                               
                            
                          
                        </tr>

                        <tr>
                            <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                <asp:DropDownList ID="ddlCompanyGrp" runat="server" Width="90%" Height="25px"    
                                         DataTextField="companygroup" DataValueField="companygroup" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>
                                 
                            </td>
                       
                           <td style="width:9%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                                  <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="95%">
                                      <asp:ListItem Selected="True" Value="COMPANY">CORPORATE</asp:ListItem>
                                      <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                  </asp:DropDownList>
                        
                               
                                  </td>

                           <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtAccountId" runat="server" Height="16px" Width="65%" AutoCompleteType="Disabled" AutoPostBack="True" ></asp:TextBox>
                              <asp:ImageButton runat="server"  ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" CssClass="righttextbox" Height="22px" Width="24px" ID="btnClient"></asp:ImageButton>
                                  <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient" BackgroundCssClass="modalBackground" Enabled="True"  ></asp:ModalPopupExtender> 
                                  </td>

                          <td colspan="2" style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtClientName" runat="server" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>
                            </td>

                           <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtLocationId" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" ></asp:TextBox>
                               <asp:ImageButton ID="BtnLocation"  runat="server"  ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" CssClass="righttextbox" Height="22px" Width="24px" ></asp:ImageButton>        
                            <asp:ModalPopupExtender ID="mdlPopupLocation" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton4" PopupControlID="pnlPopUpLocation" TargetControlID="btnDummyLocation">
                        </asp:ModalPopupExtender>
                            </td>

                        
                        </tr>

                      <tr>
                      </td>
                              <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Contract No</td>
                          
                             <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Service Frequency</td>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Billing Frequency</td>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Contract Group</td>
                         
                            <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;"> Date Range
                               </td>
                        </tr>


                   



               <tr>
                               <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                                  <asp:DropDownList runat="server" AppendDataBoundItems="True" DataTextField="ContractNo" DataValueField="ContractNo" CssClass="chzn-select" Height="25px" Width="90%" ID="ddlContractNo" DataSourceID="SqlDSContractNo">
                                      <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                  </asp:DropDownList>
                                                      
                            </td>
                            
                          
                           <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                <asp:DropDownList ID="ddlServiceFrequency" runat="server" Width="95%" Height="25px"    
                                         DataTextField="Frequency" DataValueField="Frequency" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSServiceFrequency">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>
                                 
                            </td>
                          
                           <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                <asp:DropDownList ID="ddlBillingFrequency" runat="server" Width="90%" Height="25px"    
                                         DataTextField="Frequency" DataValueField="Frequency" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSBillingFrequency">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>
                                 
                            </td>
                          
                           <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                <asp:DropDownList ID="ddlContractGroup" runat="server" Width="95%" Height="25px"    
                                         DataTextField="UPPER(contractgroup)" DataValueField="UPPER(contractgroup)" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSContractGroup">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>
                                 
                            </td>      
                          
                                            
                            <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtDateFrom" runat="server" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>
                              <asp:CalendarExtender ID="calConDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom" Enabled="True" />

                                   </td>

                          <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtDateTo" runat="server" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDateTo" TargetControlID="txtDateTo" Enabled="True" />

                                   </td>
                        </tr>

                     <tr>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                          <asp:TextBox ID="txtAccountIdSelection" runat="server" AutoCompleteType="Disabled" Height="16px" Width="35%" BorderStyle="None"></asp:TextBox>
       
                            </td>
                                                     

                        <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               
                          <asp:Button ID="btnDummyClient" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                           <asp:TextBox ID="txtTaxRatePct" runat="server" AutoCompleteType="Disabled" Height="16px" Width="35%" BorderStyle="None" ForeColor="White"></asp:TextBox>
     
                            </td>
                            
                             <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:Button ID="btnDummyLocation" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
         
                               
                                <asp:Button ID="btnDummyStatusSearch" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                  </td>
                             <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                        
                                 <asp:RadioButtonList ID="rdbGrouping" runat="server" CausesValidation="True" CellPadding="5" CellSpacing="3" Height="63px"  Width="100%" RepeatDirection="Horizontal" Font-Size="12px" Visible="False">
                                           <asp:ListItem Selected="True">Group by Contract No.</asp:ListItem>
                                           <asp:ListItem Value="LocationID">Group by Location</asp:ListItem>
                                           <asp:ListItem Value="ContractNo">Group by Account ID</asp:ListItem>
                                            <asp:ListItem Value="ServiceLocationCode">Group by Service Location</asp:ListItem>
                                       </asp:RadioButtonList>
                               
                  </td>
                        
                          
                          

                          <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                           
                               <asp:Button ID="btnShowRecords" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SHOW RECORDS" Width="65%" />
                              
                                   </td>
                              
                        </tr>
            </table>
       </ContentTemplate>
              </asp:UpdatePanel>
            

         <table border="0" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label43" runat="server" Text="SERVICE BILLING DETAILS"></asp:Label>
                 </td></tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100% ">
        
        
        <asp:UpdatePanel ID="updpnlServiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="grvServiceRecDetails" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="100%"><Columns> 
               
                  <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" TextAlign="Right" OnCheckedChanged="CheckUncheckAll" Width="5%" ></asp:CheckBox></HeaderTemplate>
                                      
                <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Font-Size="11px" Enabled="true" Height="18px"  Width="15%" AutoPostBack="true" OnCheckedChanged="chkSelectGV_CheckedChanged"  CommandName="CHECK" CommandArgument='<%# Eval("RcnoServiceRecord")%>'></asp:CheckBox></ItemTemplate></asp:TemplateField>            
                <asp:TemplateField HeaderText="Company"><ItemTemplate><asp:TextBox ID="txtCompanyGroupGV" runat="server" Visible="true" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Account Id" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="55px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Client Name"><ItemTemplate><asp:TextBox ID="txtClientNameGV" runat="server"  Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="160px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Dept."><ItemTemplate><asp:TextBox ID="txtDeptGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="35px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="130px"></asp:TextBox></ItemTemplate></asp:TemplateField>                             
                <asp:TemplateField HeaderText="Service Record"><ItemTemplate><asp:TextBox ID="txtServiceRecordNoGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="110px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Service Date" ><ItemTemplate><asp:TextBox ID="txtServiceDateGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Status"><ItemTemplate><asp:TextBox ID="txtStatusGV" runat="server" Style="text-align: center" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="35px"></asp:TextBox></ItemTemplate></asp:TemplateField>        
                <asp:TemplateField HeaderText="Billing Freq." HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtBillingFrequencyGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="90px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Location Id"><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="82px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="180px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Bill Amt."><ItemTemplate><asp:TextBox ID="txtToBillAmtGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="55px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Retention Amt."><ItemTemplate><asp:TextBox ID="txtRetentionAmtGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="55px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
            

                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServicebillingdetailGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordDetailGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContactTypeGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtInvoiceDateGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtDiscAmountGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtGSTAmountGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtNetAmountGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              

               <asp:TemplateField><ItemTemplate>
                    <asp:Button ID="btnViewEdit" runat="server" Visible="true" Width="70px" Text="View/Edit" OnClick="btnViewEdit_Click"  />
                   </ItemTemplate>
                   </asp:TemplateField>
                                                                
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtAccountNameGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillAddress1GV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillBuildingGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillStreetGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillCountryGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillPostalGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtOurReferenceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtYourReferenceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtPoNoGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtCreditTermsGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtSalesmanGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
            </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvServiceRecDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr>
             
             <tr>
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdateSelect" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="btnSelect" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                  
            </tr>


               
             <caption>
                 <br />
             </caption>
        </table>

 <br />
        
          <asp:UpdatePanel runat="server" ID="updPnlBillingRecs" UpdateMode="Conditional">
                  <ContentTemplate>

       

          <table border="1" style="width:100%; margin:auto">

               
               <tr style="width:100%">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     &nbsp;<asp:Label ID="Label41" runat="server" Text="INVOICE DETAILS"></asp:Label>

                 </td>

           </tr>


                  <tr style="width:100%">
                     <td colspan="10" style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
               <asp:UpdatePanel ID="updpnlBillingDetails" runat="server" UpdateMode="Conditional"><ContentTemplate>
             <asp:GridView ID="grvBillingDetails"  runat="server" AllowSorting="True" 
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px" 
            ShowFooter="True" Style="text-align: left" Width="95%"><Columns>
                                            
                <asp:TemplateField HeaderText=" Item Type"><ItemTemplate><asp:DropDownList ID="txtItemTypeGV" runat="server" Font-Size="12px" Height="20px" ReadOnly="true" Width="85px"  AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem Text="SERVICE" Value="SERVICE" /><asp:ListItem Text="PRODUCT" Value="PRODUCT" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Item Code"><ItemTemplate><asp:DropDownList ID="txtItemCodeGV" runat="server"  Font-Size="12px"  Height="20px"  Width="70px" AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemCodeGV_SelectedIndexChanged"> <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Item Description"><ItemTemplate><asp:TextBox ID="txtItemDescriptionGV" runat="server" Font-Size="12px" Height="15px"  Width="130px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="GL Code"><ItemTemplate><asp:TextBox ID="txtOtherCodeGV" runat="server" Font-Size="12px" Visible="true" Enabled="false" Height="15px" Width="50px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="UOM"><ItemTemplate><asp:DropDownList ID="txtUOMGV" runat="server"  Font-Size="12px" Height="20px" Width="60px" AppendDataBoundItems="True"><asp:ListItem Text="--SELECT--" Value="-1" /> </asp:DropDownList></ItemTemplate></asp:TemplateField>
             
                <asp:TemplateField HeaderText="Qty."><ItemTemplate><asp:TextBox ID="txtQtyGV" runat="server" Font-Size="12px" style="text-align:right" Height="15px"  Width="30px" AutoPostBack="true" OnTextChanged="txtQtyGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Price Per UOM"><ItemTemplate><asp:TextBox ID="txtPricePerUOMGV" runat="server" Font-Size="12px" style="text-align:right" Height="15px"  Width="60px" AutoPostBack="true" OnTextChanged="txtPricePerUOMGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Retention Amt."><ItemTemplate><asp:TextBox ID="txtRetentionAmtGV" runat="server" Font-Size="12px" style="text-align:right" Height="15px"  Width="60px" AutoPostBack="true" OnTextChanged="txtRetentionAmtGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
       
                 <asp:TemplateField HeaderText="Total Price"><ItemTemplate><asp:TextBox ID="txtTotalPriceGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="Total Retention"><ItemTemplate><asp:TextBox ID="txtTotalRetentionGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
        
              <asp:TemplateField HeaderText="Tax Code"><ItemTemplate><asp:DropDownList ID="txtTaxTypeGV" runat="server" Font-Size="12px" style="text-align:right" Height="20px" Width="50px" AutoPostBack="True" onselectedindexchanged="txtTaxTypeGV_SelectedIndexChanged"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="GST%"><ItemTemplate><asp:TextBox ID="txtGSTPercGV" runat="server"  Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="40px"></asp:TextBox></ItemTemplate></asp:TemplateField>

               <asp:TemplateField HeaderText="GST Amt."><ItemTemplate><asp:TextBox ID="txtGSTAmtGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="50px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="Retention GST Amt."><ItemTemplate><asp:TextBox ID="txtRetentionGSTAmtGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="60px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
                <asp:TemplateField HeaderText="Net Price"><ItemTemplate><asp:TextBox ID="txtTotalPriceWithGSTGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="80px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="Retention Net Price"><ItemTemplate><asp:TextBox ID="txtRetentionWithGSTGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Service Record"><ItemTemplate><asp:TextBox ID="txtServiceRecordGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="120px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                 
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordDetailGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceBillingDetailItemGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               
                 <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtServiceStatusGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                              
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtGLDescriptionGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 
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

                      <table border="0" style="width:100%; margin:auto" >
                          <tr>
              
                               <td colspan="1"  style="width:85px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;">
                                
                                 </td>
                               <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                               <td colspan="1" style="width:150px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                              <td colspan="1" style="width:30px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  &nbsp;</td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  
                                  Total
                                  
                                 </td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>
                                  <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                      <asp:TextBox ID="txtTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                               
                                 </td>
                                 
                             <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:TextBox ID="txtRetentionTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                             
                                 </td>

                              <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  <asp:TextBox ID="txtTotalDiscAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Visible="False" Width="100%"></asp:TextBox>
                             
                                   </td>
                                <td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                    <asp:TextBox ID="txtTotalWithDiscAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%" Visible="False"></asp:TextBox>
                                
                                 </td>
                              
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    <asp:TextBox ID="txtTotalGSTAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                             
                                 </td>

                               <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                   <asp:TextBox ID="txtTotalRetentionGSTAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                             
                                 </td>

                               <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                   <asp:TextBox ID="txtTotalWithGST" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="98%"></asp:TextBox>
                             
                                 </td>
                                <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                     <asp:TextBox ID="txtTotalRetentionWithGST" runat="server" Height="18px" Width="98%" Font-Bold="true" BackColor="#CCCCCC" AutoCompleteType="Disabled" style="text-align:right" BorderStyle="None"></asp:TextBox>
                             
                                 </td>
                              <td colspan="1" style="width:262px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                 </td>
                              
                        </tr>
                        
                             <tr>
              
                               <td colspan="1"  style="width:85px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;">
                                
                                 </td>
                               <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                               <td colspan="1" style="width:150px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                              <td colspan="1" style="width:30px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  </td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  
                                  
                                  
                                 </td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    </td>
                                  <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                               
                                 </td>
                                 
                             <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                 </td>

                              <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                   </td>
                                <td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                
                                 </td>
                              
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                 </td>

                               <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                             
                                 </td>

                               <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                             
                                 </td>
                                <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                         
                                 </td>
                              <td colspan="1" style="width:262px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                          <asp:Button ID="btnSave" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="50%" OnClientClick="return DoValidation()" />
                
                                 </td>
                              
                        </tr>
                         
                          
                   
                        
            </table>
                      </ContentTemplate></asp:UpdatePanel>
               
<br />

          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                          
           <tr style="width:100%">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:2%; color:#800000; background-color: #C0C0C0;">
                     VIEW/EDIT INVOICE 
                 </td>

           </tr>
               <tr style="width:100%">
                 <td colspan="4" style="font-size:15px; font-weight:bold; font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">INVOICE INFORMATION
                 </td>
           </tr>
         
                      <tr>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                                &nbsp;</td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                &nbsp;</td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:CheckBox ID="chkRecurringInvoice" runat="server" Visible="False" />
                              
                            </td>
                          <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                              &nbsp;</td>
                        </tr>
              
               <tr>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtRcnoServiceRecord" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="1%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Invoice No.<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtInvoiceNo" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="20" Width="80%" BackColor="#CCCCCC"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
              </tr>
              
               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtRcnoServiceRecordDetail" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="101"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Invoice Date<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                          <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtInvoiceDate" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="21" AutoPostBack="True" ></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDate" TargetControlID="txtInvoiceDate" Enabled="True" />

                            </td>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;text-align:right; ">   
                         </td>
                </tr>

               <tr>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%;text-align:right; ">Billing Period</td>
                   <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBillingPeriod" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Width="81%" TabIndex="109" BorderStyle="None"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;text-align:right; ">&nbsp;</td>
              </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%;text-align:right; ">   
                                Company Group</td>
                            
                          <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtCompanyGroup" runat="server" Height="16px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="110" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;text-align:right; ">   
                           &nbsp;</td>
                        </tr>

               <tr>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%;text-align:right; ">Account Type</td>
                   <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtAccountType" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Width="81%" TabIndex="111" BorderStyle="None"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;text-align:right; ">&nbsp;</td>
              </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;text-align:right; ">   
                           <asp:TextBox ID="txtRcnoInvoice" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White" TabIndex="102"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%;text-align:right; ">   
                                Account ID
                            </td>
                            
                       <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                               <asp:TextBox ID="txtAccountIdBilling" runat="server" Height="18px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="112" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Account Name
                            </td>
                            
                          <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtAccountName" runat="server" Height="18px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="113" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Billing Address
                            </td>
                            
                          <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtBillAddress" runat="server" Height="18px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="114" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtRowSelected" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" ForeColor="White" TabIndex="130"></asp:TextBox>
                   </td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                   <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBillStreet" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="81%" TabIndex="115" BorderStyle="None"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                      <asp:TextBox ID="txtBatchNo" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" TabIndex="104" ForeColor="White"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                  <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtBillBuilding" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="81%" TabIndex="116" BorderStyle="None"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                      <asp:TextBox ID="txtRcnoservicebillingdetail" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" ForeColor="White" TabIndex="105"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Country</td>
                  <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtBillCountry" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="81%" TabIndex="117" BorderStyle="None"></asp:TextBox>
                  </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtServiceName" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="106"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Postal
                            </td>
                            
                          <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtBillPostal" runat="server" Height="18px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="118" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtRecordNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="107"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Our Reference
                            </td>
                            
                          <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtOurReference" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="22" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtPostStatus" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" TabIndex="131"></asp:TextBox>
                       </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Your Reference
                            </td>
                            
                          <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtYourReference" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="23" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                PO No.
                            </td>
                            
                          <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtPONo" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="24" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>


               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Credit Terms
                            </td>
                            
                          <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                               <asp:DropDownList ID="ddlCreditTerms" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSTerms" DataTextField="UPPER(Terms)" DataValueField="UPPER(Terms)" Height="25px" Width="80.5%" TabIndex="25" AutoPostBack="True">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Credit Days</td>
                   <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtCreditDays" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="23" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Salesman
                            </td>
                            
                          <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                               <asp:DropDownList ID="ddlSalesmanBilling" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSSalesMan" DataTextField="StaffId" DataValueField="StaffId" Height="25px" Width="80.5%" TabIndex="26">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                &nbsp;</td>
                            
                          <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              
                                   &nbsp;</td>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                        </tr>

             

               <tr>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Contract No.</td>
                   <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtContractNo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="103" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Contract Amount</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtContractValue" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Approved Variations</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtApprovedVariations" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="23" Width="80%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Adjusted Contract Value</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtAdjustedContractValue" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

                <tr>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Total Claim</td>
                   <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtTotalClaim" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>


              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Previous Claim/Payments</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtPreviousClaim" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;Current Claim</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtInvoiceAmount" runat="server" AutoCompleteType="Disabled" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="80%" AutoPostBack="True"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
              </tr>

             

             
               <tr>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                   <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">GST Amount </td>
                   <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtGSTAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="121" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
              </tr>

               <tr>
                       <td  style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                            <td  style="font-size:15px; font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Net Amount
                            </td>
                            
                          <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                                  <asp:TextBox ID="txtNetAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Rows="121" style="text-align:right" Text="0.00" Width="81%" BorderStyle="None"></asp:TextBox>
                   
                       
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

                <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Total Retention Amount</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtTotalRetentionAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
            
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Previous Retention Amount</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtPreviousRetentionAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Current Retention Amount</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtRetentionAmount" runat="server" AutoCompleteType="Disabled" Height="18px" Rows="121" style="text-align:right" Text="0.00" Width="80%" AutoPostBack="True"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Balance Not Claimed</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
             
                           <asp:TextBox ID="txtPreviousRetentionAmount0" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
             
                           </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Amount Verified</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Claim Amount Not Verified</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtPreviousRetentionAmount1" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Acumulated Amount Not Verified </td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtAccumulatedAmountNotVerified" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="119" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">Remarks</td>
                  <td style="width:20%; font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                      <asp:TextBox ID="txtComments" runat="server" AutoCompleteType="Disabled" Height="100px" Width="79.5%" TextMode="MultiLine" TabIndex="27" Font-Names="Calibri" Font-Size="15px"></asp:TextBox>
                  </td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
                             <tr>
                                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                              
                                
                                 
   
                  <td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
             <asp:GridView ID="grvGL"  runat="server" AllowSorting="True" 
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px" 
            ShowFooter="True" Style="text-align: left" Width="95%"><Columns>
                                            
                <asp:TemplateField HeaderText="GL Code"><ItemTemplate><asp:TextBox ID="txtGLCodeGV" runat="server" BackColor="#CCCCCC" Font-Size="12px" Height="15px" ReadOnly="true" Width="80px"  AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:TextBox ID="txtGLDescriptionGV" runat="server"  BackColor="#CCCCCC" Font-Size="12px" Visible="true" ReadOnly="true" Height="15px" Width="250px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Debit Amount"><ItemTemplate><asp:TextBox ID="txtDebitAmountGV" runat="server" BackColor="#CCCCCC" style="text-align:right" Font-Size="12px" ReadOnly="true" Height="15px"  Width="80px" AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemCodeGV_SelectedIndexChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Credit Amount"><ItemTemplate><asp:TextBox ID="txtCreditAmountGV" runat="server"  BackColor="#CCCCCC" style="text-align:right" Font-Size="12px" Height="15px" ReadOnly="true" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField>
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



                                  
                                 <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                                              <asp:TextBox ID="txtRetentionGSTAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="121" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                                </td>
                             </tr>
                             <tr>
                                 <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                                     <asp:TextBox ID="txtARDescription" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="10%"></asp:TextBox>
                                 </td>
                                 <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">
                                     <asp:TextBox ID="txtARCode" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                                 </td>
                                 <td style="width:20%; font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                     <asp:TextBox ID="txtARCode10" runat="server" BorderStyle="None" ForeColor="White" Width="10%" TabIndex="108"></asp:TextBox>
                                        <asp:TextBox ID="txtARDescription10" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>

                                 </td>
                                 <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                                                            <asp:TextBox ID="txtGSTOutputCode" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                           <asp:TextBox ID="txtGSTOutputDescription" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
               </td>
                             </tr>
              <tr>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                  <td style="font-size:15px; font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                  <td style="width:20%;font-size:14px; font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                         

                     <td colspan="3" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                         <asp:Button ID="btnSaveInvoice" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE INVOICE" Width="15%" OnClientClick="return DoValidation()" TabIndex="28"/>
                                    
                            </td>
                    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;  ">   
                         <asp:Button ID="btnCancel" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="30%" />
                      
                           </td>
            </tr>
              </table>
          </ContentTemplate>
              </asp:UpdatePanel>
         
                             
        

          <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
             <tr style="text-align:center;width:100%">
                <td colspan="8" style="text-align:left;padding-left:5px;">
                    test
                       dth:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      er" BorderStyle="None" ForeColor="#003366"> </asp:TextBox></td>
                                  <td colspan ="2" style="text-align:left; padding-right:2%">
                                     
                                      </td>

                                 
                                 
                                   <td style="width:10%;font-size:14px; font-weight:bold;font-family:Calibri;color:black;text-align:left;"> 
                                        <asp:TextBox ID="txtIsPopup" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>
                                 </td>
                                  <td style="text-align:left;width:8%">
                                      <asp:Button ID="btnDummy" runat="server" CssClass="roundbutton" Font-Bold="True" Text=" " Width="24px" BackColor="White" BorderStyle="None" />
                                      &nbsp;&nbsp;&nbsp;<asp:Button ID="btnDummyT" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;<asp:Button ID="btnDummyC" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;</td>

                                   <td style="width:10%;font-size:14px; font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                        <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>
                                        </td>
                                  <td style="text-align:left;width:10%">
                           
                         </td>
                                  <td style="text-align: left">
                                      </td>
                                   <td style="text-align: left">
                                        
                                
                                         </td>
                            
                              </tr>

    </table>
         </div>

  
    
         
<asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="80%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
    <table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
    </td></tr><tr><td colspan="2" style="width:10%;font-size:15px; font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160; <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
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


                    <asp:Panel ID="pnlPopUpLocation" runat="server" BackColor="White" Width="1000px" Height="700px" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">

        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Select Location</h4>
                </td>
                <td style="width: 1%; text-align: right;">
    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

     
        <div style="text-align: center; padding-left:50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvLocation" runat="server" DataSourceID="SqlDSCompanyLocation" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="800px"  Font-Size="15px" PageSize="8">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="LocationID" HeaderText="LocationID" SortExpression="LocationID" />
                    <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="ServiceName" />
                    <asp:BoundField DataField="Address1" HeaderText="Address" />
                    <asp:BoundField DataField="AddBuilding" HeaderText="Building" />
                    <asp:BoundField DataField="AddStreet" HeaderText="Street" />
                    <asp:BoundField DataField="AddCity" HeaderText="City" />
                    <asp:BoundField DataField="LocateGRP" HeaderText="Zone" />
                    <asp:BoundField DataField="ContactPerson" >

                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ContactPerson2"  >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact1Position" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Position" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Telephone" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Telephone2" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Fax" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Fax" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Tel" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Tel2" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Mobile" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Mobile" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Email" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Email" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
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
            <asp:SqlDataSource ID="SqlDSCompanyLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="Select LocationID, ServiceName, Address1, AddBuilding, AddStreet, AddCity, LocateGRP, ContactPerson, ContactPerson2, Contact1Position, Contact2Position, Telephone, Telephone2, Fax, Contact2Fax, Contact2Tel, Contact2Tel2, Mobile, Contact2Mobile,  Email, Contact2Email from tblcompanylocation  
where Accountid = @Accountid order by LocationID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtAccountId" Name="@Accountid" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
      
            <asp:SqlDataSource ID="SqlDSPersonLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="Select LocationID, ServiceName, Address1, AddBuilding, AddStreet, AddCity, LocateGRP, ContactPerson, ContactPerson2, Contact1Position, Contact2Position, Telephone, Telephone2, Fax, Contact2Fax, Contact2Tel, Contact2Tel2, Mobile, Contact2Mobile,  Email, Contact2Email from tblpersonlocation 
where Accountid = @Accountid order by LocationID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtAccountId" Name="@Accountid" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
              </div>
    </asp:Panel>
       

         
             <asp:Panel ID="pnlStatusSearch" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          <br /><br />    <table style="font-family: Calibri; font-size:15px; font-weight: bold; color: black;text-align:left;padding-left:20px;">
                  <tr>
                      <td>
                          <asp:RadioButtonList ID="rdbStatusSearch" runat="server" AutoPostBack="True" Visible="False">
                              <asp:ListItem Value="ALL">ALL</asp:ListItem>
                              <asp:ListItem Value="Status">STATUS</asp:ListItem>
                          </asp:RadioButtonList>
                     <asp:checkboxlist ID="chkStatusSearch" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3" CellPadding="2" CellSpacing="2" TextAlign="Right">
                                   <asp:ListItem Value="O">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="P">P - Posted</asp:ListItem>   
                                   <asp:ListItem Value="X">X - Cancelled</asp:ListItem>  
                                  
                               </asp:checkboxlist></td>
                           </tr>
                           
                         <tr>
                             <td colspan="2"><asp:CheckBox ID="chkSearchAll" runat="server"  Text="All" Font-Size="15px" Font-Names="Calibri" Font-Bold="True" onchange="EnableDisableStatusSearch()" /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnStatusSearch" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="120px"/>
                                 <asp:Button ID="btnStatusCancel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
      </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

