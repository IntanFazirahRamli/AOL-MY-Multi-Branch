<%@ Page Title="Staff Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"   EnableEventValidation="FALSE" CodeFile="Master_Staff.aspx.vb" Inherits="Master_Staff" Culture="en-GB" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      
    <script type="text/javascript">
    function ConfirmDelete() {

        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (document.getElementById("<%=txtStaffID.ClientID%>").value != '') {

            if (confirm("Do you wish to DELETE Staff ID : " + document.getElementById("<%=txtStaffID.ClientID%>").value + ", Name : " + document.getElementById("<%=txtName.ClientID%>").value + "?")) {
                confirm_value.value = "Yes";

            } else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }
    }

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
    function TabChanged(sender, e) {
        if (sender.get_activeTabIndex() == 0) {

            //     document.getElementById('<%=GridView1.ClientID()%>').style.display = 'block';
            //     document.getElementById("<%=btnADD.ClientID%>").style.display = 'inline';
            //      document.getElementById("<%=btnEdit.ClientID%>").style.display = 'inline';
            //      document.getElementById("<%=btnDelete.ClientID%>").style.display = 'inline';
            //      document.getElementById("<%=btnFilter.ClientID%>").style.display = 'inline';
            //      document.getElementById("<%=btnPrint.ClientID%>").style.display = 'inline';
        }
        else if (sender.get_activeTabIndex() == 1) {
            if (document.getElementById("<%=txtStaffID.ClientID%>").value == '') {
                $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                alert("Please select a staff record to proceed.");
                return;
            }
            //document.getElementById('<%=GridView1.ClientID()%>').style.display = 'none';

            //   document.getElementById("<%=btnADD.ClientID%>").style.display = 'none';
            //   document.getElementById("<%=btnDelete.ClientID%>").style.display = 'none';
            //   document.getElementById("<%=btnFilter.ClientID%>").style.display = 'none';
            //  document.getElementById("<%=btnEdit.ClientID%>").style.display = 'none';
            //  document.getElementById("<%=btnPrint.ClientID%>").style.display = 'none';

        }
        else if (sender.get_activeTabIndex() == 2) {
            if (document.getElementById("<%=txtStaffID.ClientID%>").value == '') {
                $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                alert("Please select a staff record to proceeds.");
                return;
            }
            //     document.getElementById('<%=GridView1.ClientID()%>').style.display = 'none';

            //     document.getElementById("<%=btnADD.ClientID%>").style.display = 'none';
            //     document.getElementById("<%=btnDelete.ClientID%>").style.display = 'none';
            //    document.getElementById("<%=btnFilter.ClientID%>").style.display = 'none';
            //     document.getElementById("<%=btnEdit.ClientID%>").style.display = 'none';
            //    document.getElementById("<%=btnPrint.ClientID%>").style.display = 'none';


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
</script>
      <style type="text/css">
               .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}
   
    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
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
        /*width:20%;*/
        /*table-layout:fixed;
        overflow:hidden;*/
          /*border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;*/
    }
  
          .auto-style2 {
              font-size: 15px;
              font-weight: bold;
              font-family: 'Calibri';
              color: black;
              text-align: left;
              width: 20%;
        /*table-layout:fixed;
        overflow:hidden;*/
              border-collapse: collapse;
              border-spacing: 0;
              line-height: 10px;
          }
  
          .auto-style3 {
              font-size: 15px;
              font-weight: bold;
              font-family: 'Calibri';
              color: black;
              text-align: left;
              width: 23%;
        /*table-layout:fixed;
        overflow:hidden;*/
              border-collapse: collapse;
              border-spacing: 0;
              line-height: 10px;
          }
  
          .auto-style4 {
              width: 14%;
          }
          .auto-style5 {
              font-size: 15px;
              font-weight: bold;
              font-family: 'Calibri';
              color: black;
              text-align: left;
              width: 14%;
        /*table-layout:fixed;
        overflow:hidden;*/
              border-collapse: collapse;
              border-spacing: 0;
              line-height: 10px;
          }
  
          .auto-style6 {
              height: 21px;
          }
  
          </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/> 
                 <asp:controlBundle name="TabContainer_Bundle"/>                
            </ControlBundles>
        </asp:ToolkitScriptManager>
     <div style="text-align:center">
      
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Staff</h3>
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
                  
                     <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" Height="30px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" Height="30px" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "ConfirmDelete(); currentdatetime()" Height="30px"/>
                <asp:Button ID="btnFilter" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="100px" Visible="true" Height="30px" />
      
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Height="30px" />
                     <asp:Button ID="btnPrintLeave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Height="30px" Text="PRINT LEAVE" Width="110px" />
                   
                       <asp:Button ID="btnStatus" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="STATUS CHANGE" Width="140px" Height="30px" Enabled="False" />
 
                       </td>
                <td style="text-align: right">
            
                       <asp:Button ID="btnEncrypt" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Encrypt" Width="140px" Height="30px" Visible="False" />
 
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" Height="30px" />

                </td>
                
            </tr>
                <tr>
     <td colspan="2" style="text-align:right">
           <table style="text-align:right;width:100%">
            <tr>
                <td style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="text-align:right;width:45%;display:inline;">
                    
         <asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="RESET SEARCH" /></td>
                    <td style="text-align:left;width:35%">    <asp:TextBox ID="txtSearchStaff" runat="server" Width="380px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True"></asp:TextBox> &nbsp; <asp:ImageButton ID="btnGo" runat="server" ImageUrl="~/Images/gobutton.jpg" Height="25px" Width="50px" ToolTip="RESET SEARCH" />
                            
               <asp:TextBox ID="txtSearchText" runat="server" CssClass="dummybutton" ></asp:TextBox>                </td>
            </tr>
        </table>
</td>
            </tr>
            <tr class="Centered">
                <td colspan="2">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="StaffId" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" DataSourceID="SqlDataSource1" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="15px"  AllowSorting="True" AllowPaging="True" HorizontalAlign="Center" ForeColor="#333333" GridLines="Vertical">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                  <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false" >
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
                  <asp:BoundField DataField="Rcno">
                  <ControlStyle CssClass="dummybutton" />
                  <HeaderStyle CssClass="dummybutton" />
                  <ItemStyle CssClass="dummybutton" />
                  </asp:BoundField>
                 <asp:BoundField DataField="StaffId" HeaderText="StaffId" ReadOnly="True" SortExpression="StaffId" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
              
                 <asp:BoundField DataField="Salute" HeaderText="Salute" SortExpression="Salute" />
                   <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Wrap="False" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
<ItemStyle Wrap="False" Width="200px"></ItemStyle>
                  </asp:BoundField>
                 <asp:BoundField DataField="PayrollID" HeaderText="PayrollID" SortExpression="PayrollID"/>
                 <asp:BoundField DataField="DateJoin" HeaderText="DateJoin" SortExpression="DateJoin" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" Visible="true"/>
                <asp:BoundField DataField="Appointment" HeaderText="Appointment" SortExpression="Appointment" />
               
                 <asp:BoundField DataField="Nric" HeaderText="Nric" SortExpression="Nric" >
                   <HeaderStyle Width="100px" />
                  <ItemStyle Width="100px" />
                  </asp:BoundField>
                   <asp:BoundField DataField="FIN_NO" HeaderText="FIN_NO" SortExpression="FIN_NO" >
                  <HeaderStyle Width="100px" />
                  <ItemStyle Width="100px" />
                  </asp:BoundField>
                 <asp:BoundField DataField="DateOfBirth" HeaderText="DOB" SortExpression="DateOfBirth" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Profession" HeaderText="Profession" SortExpression="Profession" />
               
                 <asp:BoundField DataField="CountryOfBirth" HeaderText="CountryOfBirth" SortExpression="CountryOfBirth" Visible="false"/>
                 <asp:BoundField DataField="Citizenship" HeaderText="Citizenship" SortExpression="Citizenship" Visible="false"/>
                 <asp:BoundField DataField="Race" HeaderText="Race" SortExpression="Race" Visible="false"/>
                 <asp:BoundField DataField="Sex" HeaderText="Sex" SortExpression="Sex" Visible="false"/>
                 <asp:BoundField DataField="MartialStatus" HeaderText="MartialStatus" SortExpression="MartialStatus" Visible="false"/>
                 <asp:BoundField DataField="AddBlock" HeaderText="AddBlock" SortExpression="AddBlock" Visible="false"/>
                 <asp:BoundField DataField="AddNos" HeaderText="AddNos" SortExpression="AddNos" Visible="false"/>
                 <asp:BoundField DataField="AddFloor" HeaderText="AddFloor" SortExpression="AddFloor" Visible="false"/>
                 <asp:BoundField DataField="AddUnit" HeaderText="AddUnit" SortExpression="AddUnit" Visible="false"/>
                 <asp:BoundField DataField="AddBuilding" HeaderText="AddBuilding" SortExpression="AddBuilding" Visible="false"/>
                 <asp:BoundField DataField="AddStreet" HeaderText="AddStreet" SortExpression="AddStreet" Visible="false"/>
                 <asp:BoundField DataField="AddPostal" HeaderText="AddPostal" SortExpression="AddPostal" Visible="false"/>
                 <asp:BoundField DataField="AddCity" HeaderText="AddCity" SortExpression="AddCity" Visible="false"/>
                 <asp:BoundField DataField="AddState" HeaderText="AddState" SortExpression="AddState" Visible="false"/>
                 <asp:BoundField DataField="AddCountry" HeaderText="AddCountry" SortExpression="AddCountry" Visible="false"/>
                 <asp:BoundField DataField="TelHome" HeaderText="TelHome" SortExpression="TelHome" Visible="false"/>
                 <asp:BoundField DataField="TelMobile" HeaderText="TelMobile" SortExpression="TelMobile" Visible="false"/>
                 <asp:BoundField DataField="TelPager" HeaderText="TelPager" SortExpression="TelPager" Visible="true"/>
                 <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" Visible="true"/>
                 <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" Visible="false"/>
                  <asp:BoundField DataField="DirectLine" HeaderText="DirectLine" SortExpression="DirectLine" Visible="false"/>
                 <asp:BoundField DataField="Extension" HeaderText="Extension" SortExpression="Extension" Visible="false"/>
                 <asp:BoundField DataField="EmailPerson" HeaderText="EmailPerson" SortExpression="EmailPerson" Visible="false"/>
                 <asp:BoundField DataField="EmailCompany" HeaderText="EmailCompany" SortExpression="EmailCompany" Visible="false"/>
                 <asp:BoundField DataField="DateLeft" HeaderText="DateLeft" SortExpression="DateLeft" Visible="false"/>
                 <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" Visible="false"/>
                 <asp:BoundField DataField="SecGroupAuthority" HeaderText="GroupAuthority" SortExpression="SecGroupAuthority" Visible="true"/>
                  <asp:TemplateField HeaderText="MobileUser">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMobileUser" runat="server"  Enabled="false" Checked='<%#If((Eval("SecMobileLoginID") = "" or Eval("SecMobilePassword") = ""), False, True)%>' />
                                                 </ItemTemplate>
                                                   </asp:TemplateField>
                   <%--  <asp:TemplateField HeaderText="MobileUser">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMobileUser1" runat="server"  Enabled="false" Checked='<%#If(Eval("SecMobileLoginID") = "", False, True)%>' />
                                                 </ItemTemplate>
                                                   </asp:TemplateField>
                     <asp:TemplateField HeaderText="MobileUser">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMobileUser2" runat="server"  Enabled="false" Checked='<%#If(Eval("SecMobilePassword") = "", False, True)%>' />
                                                 </ItemTemplate>
                                                   </asp:TemplateField>--%>
                 <asp:BoundField DataField="SecLevel" HeaderText="SecLevel" SortExpression="SecLevel" Visible="false"/>
                 <asp:BoundField DataField="SecLoginId" HeaderText="SecLoginId" SortExpression="SecLoginId" Visible="false"/>
                 <asp:BoundField DataField="SecPassword" HeaderText="SecPassword" SortExpression="SecPassword" Visible="false"/>
                 <asp:BoundField DataField="SecExpiryDate" HeaderText="SecExpiryDate" SortExpression="SecExpiryDate" Visible="false"/>
                 <asp:BoundField DataField="Address1" HeaderText="Address1" SortExpression="Address1" Visible="false"/>
                 <asp:BoundField DataField="SecChangePassword" HeaderText="SecChangePassword" SortExpression="SecChangePassword" Visible="false"/>
                 <asp:BoundField DataField="SecDisablePassword" HeaderText="SecDisablePassword" SortExpression="SecDisablePassword" Visible="false"/>
                 <asp:BoundField DataField="Payroll_Coy" HeaderText="Payroll_Coy" SortExpression="Payroll_Coy" Visible="false"/>
                 <asp:BoundField DataField="EmpGroup" HeaderText="EmpGroup" SortExpression="EmpGroup" Visible="false"/>
                 <asp:BoundField DataField="Trade" HeaderText="Trade" SortExpression="Trade" Visible="false"/>
                 <asp:BoundField DataField="WP_EP_NO" HeaderText="WP_EP_NO" SortExpression="WP_EP_NO" Visible="false"/>
                 <asp:BoundField DataField="DateArrival" HeaderText="DateArrival" SortExpression="DateArrival" Visible="false"/>
                 <asp:BoundField DataField="PP_Expiry" HeaderText="PP_Expiry" SortExpression="PP_Expiry" Visible="false"/>
                 <asp:BoundField DataField="WP_EP_Expiry" HeaderText="WP_EP_Expiry" SortExpression="WP_EP_Expiry" Visible="false"/>
                 <asp:BoundField DataField="MonthLevy" HeaderText="MonthLevy" SortExpression="MonthLevy" Visible="false"/>
                 <asp:BoundField DataField="AgentCode" HeaderText="AgentCode" SortExpression="AgentCode" Visible="false"/>
                 <asp:BoundField DataField="PPNo" HeaderText="PPNo" SortExpression="PPNo" Visible="false"/>
                 <asp:BoundField DataField="PPType" HeaderText="PPType" SortExpression="PPType" Visible="false"/>
                 <asp:BoundField DataField="PPLocation" HeaderText="PPLocation" SortExpression="PPLocation" Visible="false"/>
                 <asp:BoundField DataField="Nationality" HeaderText="Nationality" SortExpression="Nationality"/>
                 <asp:BoundField DataField="SystemUser" HeaderText="SystemUser" SortExpression="SystemUser" Visible="false"/>
                 <asp:BoundField DataField="HLEVEL" HeaderText="HLEVEL" SortExpression="HLEVEL" Visible="false"/>
                 <asp:BoundField DataField="HDEPT" HeaderText="HDEPT" SortExpression="HDEPT" Visible="false"/>
                 <asp:BoundField DataField="WP_EP_ApplyDt" HeaderText="WP_EP_ApplyDt" SortExpression="WP_EP_ApplyDt" Visible="false"/>
                 <asp:BoundField DataField="PassPort_Expiry" HeaderText="PassPort_Expiry" SortExpression="PassPort_Expiry" Visible="false"/>
                 <asp:BoundField DataField="Sec_Bond_No" HeaderText="Sec_Bond_No" SortExpression="Sec_Bond_No" Visible="false"/>
                 <asp:BoundField DataField="Sec_Bond_Expiry" HeaderText="Sec_Bond_Expiry" SortExpression="Sec_Bond_Expiry" Visible="false"/>
                 <asp:BoundField DataField="Soc_Cert_No" HeaderText="Soc_Cert_No" SortExpression="Soc_Cert_No" Visible="false"/>
                 <asp:BoundField DataField="Soc_Cert_Expiry" HeaderText="Soc_Cert_Expiry" SortExpression="Soc_Cert_Expiry" Visible="false"/>
                 <asp:BoundField DataField="InterfaceLanguage" HeaderText="InterfaceLanguage" SortExpression="InterfaceLanguage" Visible="false"/>
                 <asp:BoundField DataField="Language1" HeaderText="Language1" SortExpression="Language1" Visible="false"/>
                 <asp:BoundField DataField="Language2" HeaderText="Language2" SortExpression="Language2" Visible="false"/>
                 <asp:BoundField DataField="Language3" HeaderText="Language3" SortExpression="Language3" Visible="false"/>
                 <asp:BoundField DataField="Language4" HeaderText="Language4" SortExpression="Language4" Visible="false"/>
                 <asp:BoundField DataField="Language5" HeaderText="Language5" SortExpression="Language5" Visible="false"/>
                 <asp:BoundField DataField="Language6" HeaderText="Language6" SortExpression="Language6" Visible="false"/>
                 <asp:BoundField DataField="WebPassword" HeaderText="WebPassword" SortExpression="WebPassword" Visible="false"/>
                 <asp:BoundField DataField="CostCenter" HeaderText="CostCenter" SortExpression="CostCenter" Visible="false"/>
                 <asp:BoundField DataField="SalaryType" HeaderText="SalaryType" SortExpression="SalaryType" Visible="false"/>
                 <asp:BoundField DataField="BasicPay" HeaderText="BasicPay" SortExpression="BasicPay" Visible="false"/>
                 <asp:BoundField DataField="Share" HeaderText="Share" SortExpression="Share" Visible="false"/>
                 <asp:BoundField DataField="DaysPerWeek" HeaderText="DaysPerWeek" SortExpression="DaysPerWeek" Visible="false"/>
                 <asp:BoundField DataField="SDLyn" HeaderText="SDLyn" SortExpression="SDLyn" Visible="false"/>
                 <asp:BoundField DataField="PayBasis" HeaderText="PayBasis" SortExpression="PayBasis" Visible="false"/>
                 <asp:BoundField DataField="ComputeMethod" HeaderText="ComputeMethod" SortExpression="ComputeMethod" Visible="false"/>
                 <asp:BoundField DataField="PayMethod" HeaderText="PayMethod" SortExpression="PayMethod" Visible="false"/>
                 <asp:BoundField DataField="BankCode" HeaderText="BankCode" SortExpression="BankCode" Visible="false"/>
                 <asp:BoundField DataField="BranchCode" HeaderText="BranchCode" SortExpression="BranchCode" Visible="false"/>
                 <asp:BoundField DataField="AccountNo" HeaderText="AccountNo" SortExpression="AccountNo" Visible="false"/>
                 <asp:BoundField DataField="CPFgroup" HeaderText="CPFgroup" SortExpression="CPFgroup" Visible="false"/>
                 <asp:BoundField DataField="CPFsub" HeaderText="CPFsub" SortExpression="CPFsub" Visible="false"/>
                 <asp:BoundField DataField="FWLcode" HeaderText="FWLcode" SortExpression="FWLcode" Visible="false"/>
                 <asp:BoundField DataField="CPFno" HeaderText="CPFno" SortExpression="CPFno" Visible="false"/>
                 <asp:BoundField DataField="FundCode" HeaderText="FundCode" SortExpression="FundCode" Visible="false"/>
                 <asp:BoundField DataField="FundByEmployer" HeaderText="FundByEmployer" SortExpression="FundByEmployer" Visible="false"/>
                 <asp:BoundField DataField="CompanyBank" HeaderText="CompanyBank" SortExpression="CompanyBank" Visible="false"/>
                 <asp:BoundField DataField="CompanyCPF" HeaderText="CompanyCPF" SortExpression="CompanyCPF" Visible="false"/>
                 <asp:BoundField DataField="DateConfirm" HeaderText="DateConfirm" SortExpression="DateConfirm" Visible="false"/>
                 <asp:BoundField DataField="DailyLevy" HeaderText="DailyLevy" SortExpression="DailyLevy" Visible="false"/>
                 <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" Visible="false"/>
                 <asp:BoundField DataField="HoursPerDay" HeaderText="HoursPerDay" SortExpression="HoursPerDay" Visible="false"/>
                 <asp:BoundField DataField="OTyn" HeaderText="OTyn" SortExpression="OTyn" Visible="false"/>
                 <asp:BoundField DataField="DailyBasic" HeaderText="DailyBasic" SortExpression="DailyBasic" Visible="false"/>
                 <asp:BoundField DataField="HourlyBasic" HeaderText="HourlyBasic" SortExpression="HourlyBasic" Visible="false"/>
                 <asp:BoundField DataField="OT1_5" HeaderText="OT1_5" SortExpression="OT1_5" Visible="false"/>
                 <asp:BoundField DataField="OT2" HeaderText="OT2" SortExpression="OT2" Visible="false"/>
                 <asp:BoundField DataField="WorkTimeGRP" HeaderText="WorkTimeGRP" SortExpression="WorkTimeGRP" Visible="false"/>
                 <asp:BoundField DataField="TimeCardNo" HeaderText="TimeCardNo" SortExpression="TimeCardNo" Visible="false"/>
                 <asp:BoundField DataField="PassType" HeaderText="PassType" SortExpression="PassType"/>
                       <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
           
                 <asp:BoundField DataField="ALEntitlement" HeaderText="ALEntitlement" SortExpression="ALEntitlement" Visible="false"/>
                 <asp:BoundField DataField="TimeAllowanceYN" HeaderText="TimeAllowanceYN" SortExpression="TimeAllowanceYN" Visible="false"/>
                 <asp:BoundField DataField="TimeAllowanceStart" HeaderText="TimeAllowanceStart" SortExpression="TimeAllowanceStart" Visible="false"/>
                 <asp:BoundField DataField="TimeAllowanceEnd" HeaderText="TimeAllowanceEnd" SortExpression="TimeAllowanceEnd" Visible="false"/>
                 <asp:BoundField DataField="TimeCardReport" HeaderText="TimeCardReport" SortExpression="TimeCardReport" Visible="false"/>
                 <asp:BoundField DataField="HomeBlock" HeaderText="HomeBlock" SortExpression="HomeBlock" Visible="false"/>
                 <asp:BoundField DataField="HomeNos" HeaderText="HomeNos" SortExpression="HomeNos" Visible="false"/>
                 <asp:BoundField DataField="HomeFloor" HeaderText="HomeFloor" SortExpression="HomeFloor" Visible="false"/>
                 <asp:BoundField DataField="HomeUnit" HeaderText="HomeUnit" SortExpression="HomeUnit" Visible="false"/>
                 <asp:BoundField DataField="HomeBuilding" HeaderText="HomeBuilding" SortExpression="HomeBuilding" Visible="false"/>
                 <asp:BoundField DataField="HomeStreet" HeaderText="HomeStreet" SortExpression="HomeStreet" Visible="false"/>
                 <asp:BoundField DataField="HomePostal" HeaderText="HomePostal" SortExpression="HomePostal" Visible="false"/>
                 <asp:BoundField DataField="HomeCity" HeaderText="HomeCity" SortExpression="HomeCity" Visible="false"/>
                 <asp:BoundField DataField="HomeState" HeaderText="HomeState" SortExpression="HomeState" Visible="false"/>
                 <asp:BoundField DataField="HomeCountry" HeaderText="HomeCountry" SortExpression="HomeCountry" Visible="false"/>
                 <asp:BoundField DataField="HomeAddress1" HeaderText="HomeAddress1" SortExpression="HomeAddress1" Visible="false"/>
                 <asp:BoundField DataField="HomeTel" HeaderText="HomeTel" SortExpression="HomeTel" Visible="false"/>
                 <asp:BoundField DataField="HomeMobile" HeaderText="HomeMobile" SortExpression="HomeMobile" Visible="false"/>
                 <asp:BoundField DataField="HomePager" HeaderText="HomePager" SortExpression="HomePager" Visible="false"/>
                 <asp:BoundField DataField="HomeEmail" HeaderText="HomeEmail" SortExpression="HomeEmail" Visible="false"/>
                 <asp:BoundField DataField="Meal" HeaderText="Meal" SortExpression="Meal" Visible="false"/>
                 <asp:BoundField DataField="Transport" HeaderText="Transport" SortExpression="Transport" Visible="false"/>
                 <asp:BoundField DataField="Shift" HeaderText="Shift" SortExpression="Shift" Visible="false"/>
                 <asp:BoundField DataField="RoomNo" HeaderText="RoomNo" SortExpression="RoomNo" Visible="false"/>
                 <asp:BoundField DataField="ShiftCode" HeaderText="ShiftCode" SortExpression="ShiftCode" Visible="false"/>
                 <asp:BoundField DataField="LocationID" HeaderText="LocationID" SortExpression="LocationID" Visible="false"/>
                 <asp:BoundField DataField="DeptSubLedger" HeaderText="DeptSubLedger" SortExpression="DeptSubLedger" Visible="false"/>
                 <asp:BoundField DataField="IR8SIndicator" HeaderText="IR8SIndicator" SortExpression="IR8SIndicator" Visible="false"/>
                 <asp:BoundField DataField="POSBranch" HeaderText="POSBranch" SortExpression="POSBranch" Visible="false"/>
                 <asp:BoundField DataField="JobType_Auth_Users" HeaderText="JobType_Auth_Users" SortExpression="JobType_Auth_Users" Visible="false"/>
                 <asp:BoundField DataField="WHLocation" HeaderText="WHLocation" SortExpression="WHLocation" Visible="false"/>
                 <asp:BoundField DataField="ZNSecPassword" HeaderText="ZNSecPassword" SortExpression="ZNSecPassword" Visible="false"/>
                  <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                 <asp:BoundField DataField="PayCompute" HeaderText="PayCompute" SortExpression="PayCompute" Visible="false"/>
                 <asp:BoundField DataField="EffectPeriod" HeaderText="EffectPeriod" SortExpression="EffectPeriod" Visible="false"/>
                 <asp:BoundField DataField="CPFYN" HeaderText="CPFYN" SortExpression="CPFYN" Visible="false"/>
                 <asp:BoundField DataField="WorkEnqScreen" HeaderText="WorkEnqScreen" SortExpression="WorkEnqScreen" Visible="false"/>
                  <asp:BoundField DataField="SecMobileLoginID" HeaderText="SecMobileLoginID" SortExpression="SecMobileLoginID" Visible="false"/>
                 <asp:BoundField DataField="SecMobilePassword" HeaderText="SecMobilePassword" SortExpression="SecMobilePassword" Visible="false"/>
                 <asp:BoundField DataField="SecTabletMobileNo" HeaderText="SecTabletMobileNo" SortExpression="SecTabletMobileNo" Visible="false"/>
                 <asp:BoundField DataField="SecWebLoginID" HeaderText="SecWebLoginID" SortExpression="SecWebLoginID" Visible="false"/>
                 <asp:BoundField DataField="SecWebPassword" HeaderText="SecWebPassword" SortExpression="SecWebPassword" Visible="false"/>
                 <asp:BoundField DataField="ProjectCode" HeaderText="ProjectCode" SortExpression="ProjectCode" Visible="false"/>
                 <asp:BoundField DataField="HRSecurityLevel" HeaderText="HRSecurityLevel" SortExpression="HRSecurityLevel" Visible="false"/>
                 <asp:BoundField DataField="SecGoogleEmail" HeaderText="SecGoogleEmail" SortExpression="SecGoogleEmail" Visible="false"/>
                 <asp:BoundField DataField="SecGooglePassword" HeaderText="SecGooglePassword" SortExpression="SecGooglePassword" Visible="false"/>
                 <asp:BoundField DataField="SecGoogleTaskEvent" HeaderText="SecGoogleTaskEvent" SortExpression="SecGoogleTaskEvent" Visible="false"/>
                 <asp:BoundField DataField="SecGoogleJobReqDate" HeaderText="SecGoogleJobReqDate" SortExpression="SecGoogleJobReqDate" Visible="false"/>
                 <asp:BoundField DataField="SecGoogleServDate" HeaderText="SecGoogleServDate" SortExpression="SecGoogleServDate" Visible="false"/>
                 <asp:BoundField DataField="SlsmanYN" HeaderText="SlsmanYN" SortExpression="SlsmanYN" Visible="false"/>
                 <asp:BoundField DataField="WebDisableYN" HeaderText="WebDisableYN" SortExpression="WebDisableYN" Visible="false"/>
                 <asp:BoundField DataField="OTPYN" HeaderText="OTPYN" SortExpression="OTPYN" Visible="false"/>
                 <asp:BoundField DataField="WebID" HeaderText="WebID" SortExpression="WebID" Visible="false"/>
                 <asp:BoundField DataField="EmployerTaxShare" HeaderText="EmployerTaxShare" SortExpression="EmployerTaxShare" Visible="false"/>
                 <asp:BoundField DataField="EmployeeTaxShare" HeaderText="EmployeeTaxShare" SortExpression="EmployeeTaxShare" Visible="false"/>
                 <asp:BoundField DataField="Passcode" HeaderText="Passcode" SortExpression="Passcode" Visible="false"/>
                 <asp:BoundField DataField="LeaveGroup" HeaderText="LeaveGroup" SortExpression="LeaveGroup" Visible="false"/>
                 <asp:BoundField DataField="AnnualLeaveIncrement" HeaderText="AnnualLeaveIncrement" SortExpression="AnnualLeaveIncrement" Visible="false"/>
                 <asp:BoundField DataField="MaxBringForward" HeaderText="MaxBringForward" SortExpression="MaxBringForward" Visible="false"/>
                 <asp:BoundField DataField="MaxLeaveEntitlement" HeaderText="MaxLeaveEntitlement" SortExpression="MaxLeaveEntitlement" Visible="false"/>
                 <asp:BoundField DataField="EvenDistribution" HeaderText="EvenDistribution" SortExpression="EvenDistribution" Visible="false"/>
                 <asp:BoundField DataField="CustomizedDistribution" HeaderText="CustomizedDistribution" SortExpression="CustomizedDistribution" Visible="false"/>
                 <asp:BoundField DataField="Month1" HeaderText="Month1" SortExpression="Month1" Visible="false"/>
                 <asp:BoundField DataField="Month2" HeaderText="Month2" SortExpression="Month2" Visible="false"/>
                 <asp:BoundField DataField="Month3" HeaderText="Month3" SortExpression="Month3" Visible="false"/>
                 <asp:BoundField DataField="Month4" HeaderText="Month4" SortExpression="Month4" Visible="false"/>
                 <asp:BoundField DataField="Month5" HeaderText="Month5" SortExpression="Month5" Visible="false"/>
                 <asp:BoundField DataField="Month6" HeaderText="Month6" SortExpression="Month6" Visible="false"/>
                 <asp:BoundField DataField="Month7" HeaderText="Month7" SortExpression="Month7" Visible="false"/>
                 <asp:BoundField DataField="Month8" HeaderText="Month8" SortExpression="Month8" Visible="false"/>
                 <asp:BoundField DataField="Month9" HeaderText="Month9" SortExpression="Month9" Visible="false"/>
                 <asp:BoundField DataField="Month10" HeaderText="Month10" SortExpression="Month10" Visible="false"/>
                 <asp:BoundField DataField="Month11" HeaderText="Month11" SortExpression="Month11" Visible="false"/>
                 <asp:BoundField DataField="Month12" HeaderText="Month12" SortExpression="Month12" Visible="false"/>
                 <asp:BoundField DataField="SubCompanyNo" HeaderText="SubCompanyNo" SortExpression="SubCompanyNo" Visible="false"/>
                 <asp:BoundField DataField="SourceCompany" HeaderText="SourceCompany" SortExpression="SourceCompany" Visible="false"/>
                 <asp:BoundField DataField="VerifyBySMS" HeaderText="VerifyBySMS" SortExpression="VerifyBySMS" Visible="false"/>
                 <asp:BoundField DataField="CalendarColor" HeaderText="CalendarColor" SortExpression="CalendarColor" Visible="false"/>
                 <asp:BoundField DataField="PreferredName" HeaderText="PreferredName" SortExpression="PreferredName" Visible="false"/>
                 <asp:BoundField DataField="SalesGroup" HeaderText="SalesGroup" SortExpression="SalesGroup" Visible="false"/>
                 <asp:BoundField DataField="CompanyGroup" HeaderText="CompanyGroup" SortExpression="CompanyGroup" Visible="false"/>
                 <asp:BoundField DataField="Roles" HeaderText="Roles" SortExpression="Roles" Visible="false"/>
                 <asp:BoundField DataField="SecGroupAuthorityTablet" HeaderText="SecGroupAuthorityTablet" SortExpression="SecGroupAuthorityTablet" Visible="false"/>
                 <asp:BoundField DataField="SecLoginComments" HeaderText="SecLoginComments" SortExpression="SecLoginComments" Visible="false"/>
                 <asp:BoundField DataField="WebUploadDate" HeaderText="WebUploadDate" SortExpression="WebUploadDate" Visible="false"/>
                 <asp:BoundField DataField="PRIssueDate" HeaderText="PRIssueDate" SortExpression="PRIssueDate" Visible="false"/>
                 <asp:BoundField DataField="UploadDate" HeaderText="UploadDate" SortExpression="UploadDate" Visible="false"/>
                 <asp:BoundField DataField="IncentiveRate" HeaderText="IncentiveRate" SortExpression="IncentiveRate" Visible="false"/>
                 <asp:BoundField DataField="RptDepartment" HeaderText="RptDepartment" SortExpression="RptDepartment" Visible="false"/>
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
        </table>
         <asp:TabContainer ID="tb1" runat="server" Font-Names="Calibri" Height="100%" Width="100%" CssClass="ajax__tab_xp" ActiveTabIndex="0" OnClientActiveTabChanged="TabChanged" >
 
     
      <asp:TabPanel runat="server" HeaderText="Client Information & Schedule" ID="TabPanel1">
        <HeaderTemplate>
            Staff Information
        
</HeaderTemplate>
        
        
<ContentTemplate>
         
         <table>
         <tr>
             <td>
 
                       <asp:Panel ID="Panel5" runat="server" BackColor="#F8F8F8" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%" ToolTip="Currency" HorizontalAlign="Center">
                   
                      <table style="width:100%;text-align:center;padding-left:10px;padding-top:5px;padding-bottom:5px;">
                        <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label1" runat="server" Text="Staff ID" Width="70px"></asp:Label>
<asp:Label ID="Label49" runat="server" Text="*" Font-Size="18px" ForeColor="Red"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtStaffID" runat="server" MaxLength="25" Height="16px" Width="95%"></asp:TextBox>
</td>
                             <td class="CellFormat"> <asp:Label ID="Label2" runat="server" Text="NRIC" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtNRIC" runat="server" MaxLength="20" Height="16px" Width="95%"></asp:TextBox>
</td>
                              <td class="CellFormat">
                                <asp:Label ID="Label6" runat="server" Text="Department" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtDept" runat="server" MaxLength="100" Height="16px" Width="95%"></asp:TextBox>
</td>
                           
                            <td colspan="2" class="CellFormat" rowspan="5" style="height:80px;width:80px;font-size:13px;text-align:right;">
                                  Image
                                    <asp:Panel ID="Panel1" runat="server" Height="95%" BorderStyle="Solid" BorderColor="Silver" BorderWidth="2px" Width="130px" HorizontalAlign="Center">
                              
                    <asp:Image ID="Image2" runat="server" Width="130px" Height="100%" />

                                        </asp:Panel>

                </td>
                        </tr>
                           <tr>
                                 <td class="CellFormat">
                                <asp:Label ID="Label11" runat="server" Text="Salutation" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:DropDownList ID="ddlSalute" runat="server" Width="97%" CssClass="chzn-select" ><asp:ListItem Text="--SELECT--" Value="-1" />
<asp:ListItem Selected="True" Value="MISS">MISS</asp:ListItem>
<asp:ListItem Value="MDM">MDM</asp:ListItem>
<asp:ListItem Value="MR.">MR.</asp:ListItem>
<asp:ListItem Value="MRS.">MRS.</asp:ListItem>
<asp:ListItem Value="MS.">MS.</asp:ListItem>
<asp:ListItem Value="PROF">PROF</asp:ListItem>
<asp:ListItem Value="VEN">VEN</asp:ListItem>
<asp:ListItem Value="DR.">DR.</asp:ListItem>
</asp:DropDownList>
</td>
                        
                            <td class="CellFormat">
                                <asp:Label ID="Label3" runat="server" Text="Name" Width="45px"></asp:Label>
<asp:Label ID="Label47" runat="server" Text="*" Font-Size="18px" ForeColor="Red"></asp:Label>
</td>
                            <td colspan="3" class="CellTextBox"> <asp:TextBox ID="txtName" runat="server" MaxLength="100" Height="16px" Width="98%"></asp:TextBox>
</td>
                            
                        </tr>
                              <tr>
                                 <td class="CellFormat">
                                <asp:Label ID="Label12" runat="server" Text="InterfaceLang" Width="90px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtInterfaceLang" runat="server" MaxLength="20" Height="16px" Width="95%"></asp:TextBox>
</td>
                          
                            <td class="CellFormat">
                                <asp:Label ID="Label13" runat="server" Text="Pref.Name " Width="80px"></asp:Label>
</td>
                            <td colspan="1" class="CellTextBox"> <asp:TextBox ID="txtPrefName" runat="server" MaxLength="100" Height="16px" Width="95%"></asp:TextBox>
</td>
                              <td class="CellFormat">
                         
                            <asp:Label ID="Label58" runat="server" Text="Roles" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox">  <asp:DropDownList ID="ddlRoles" runat="server" Width="97%"><asp:ListItem Text="--SELECT--" Value="-1" />
<asp:ListItem Value="SALES MAN">SALES MAN</asp:ListItem>
<asp:ListItem>TECHNICAL</asp:ListItem>
</asp:DropDownList>
</td>
                           
                        </tr>
                           <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label4" runat="server" Text="Date Joined" Width="90px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtDateJoined" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>

                                   <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDateJoined" TargetControlID="txtDateJoined" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
 
                          
                            </td>
                             <td class="CellFormat"> <asp:Label ID="Label5" runat="server" Text="Date Left" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtDateLeft" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>

                                          <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtDateLeft" TargetControlID="txtDateLeft" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>

                       
                            </td>
                              <td class="CellFormat">
                         
                            <asp:Label ID="Label14" runat="server" Text="Profession" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtProfession" runat="server" MaxLength="100" Height="16px" Width="95%"></asp:TextBox>
</td>
                           
                        </tr>
                          <tr>
                              <td class="CellFormat"> <asp:Label ID="Label7" runat="server" Text="Appointment" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtAppt" runat="server" MaxLength="100" Height="16px" Width="95%"></asp:TextBox>
</td>
                              <td class="CellFormat">
                                <asp:Label ID="Label10" runat="server" Text="Email" Width="80px"></asp:Label>
</td>
                            <td colspan="3" class="CellTextBox"> <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox>
</td>
                            
                        </tr>
                          <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label8" runat="server" Text="Telephone" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtTel" runat="server" MaxLength="50" Height="16px" Width="95%"></asp:TextBox>
</td>
                             <td class="CellFormat"> <asp:Label ID="Label9" runat="server" Text="Mobile" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtMobile" runat="server" MaxLength="50" Height="16px" Width="95%"></asp:TextBox>
</td> 
                          <td class="CellFormat"> <asp:Label ID="Label15" runat="server" Text="MaritalStatus" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:DropDownList ID="ddlMaritalStatus" runat="server" Width="97%"><asp:ListItem Text="--SELECT--" Value="-1" />
<asp:ListItem Value="DIVORCED">DIVORCED</asp:ListItem>
<asp:ListItem Value="MARRIED">MARRIED</asp:ListItem>
<asp:ListItem Value="SEPARATED">SEPARATED</asp:ListItem>
<asp:ListItem Value="SINGLE">SINGLE</asp:ListItem>
</asp:DropDownList>
</td> 
                   <td class="CellTextBox">
                       <asp:FileUpload ID="FileUpload1" runat="server" />
</td>
                               </tr>
                             <tr>
                           <td class="CellFormat">
                                       <asp:Label ID="Label50" runat="server" Text="Status" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:DropDownList ID="ddlStatus" runat="server" Width="97%"><asp:ListItem Value="O">O - Open</asp:ListItem>
<asp:ListItem Value="T">T - Terminated</asp:ListItem>
<asp:ListItem Value="R">R - Resigned</asp:ListItem>
<asp:ListItem Value="D">D - Disabled</asp:ListItem>
</asp:DropDownList>
</td>
                          <td class="CellFormat">     <asp:Label ID="Label16" runat="server" Text="Comments" Width="80px"></asp:Label>
</td>
                            <td colspan="5" class="CellTextBox"> <asp:TextBox ID="txtComments" runat="server" MaxLength="500" Height="16px" Width="99.5%"></asp:TextBox>
</td>
                          
                        </tr>
                           <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label17" runat="server" Text="BirthDate" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtDOB" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>

                                      <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtDOB" TargetControlID="txtDOB" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>

                       
                            </td>
                             <td class="CellFormat"> <asp:Label ID="Label18" runat="server" Text="Nationality" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtNationality" runat="server" MaxLength="50" Height="16px" Width="95%"></asp:TextBox>
</td> 
                          <td class="CellFormat"> <asp:Label ID="Label19" runat="server" Text="Citizenship" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtCitizenship" runat="server" MaxLength="100" Height="16px" Width="95%"></asp:TextBox>
</td> 
                    <td class="CellFormat"> <asp:Label ID="Label20" runat="server" Text="SystemUser" Width="70px"></asp:Label>
</td>
                            <td class="CellTextBox">  <asp:DropDownList ID="ddlSystemUser" runat="server" Width="97%"><asp:ListItem Value="N">N</asp:ListItem>
<asp:ListItem Value="Y">Y</asp:ListItem>
</asp:DropDownList>
</td> 
                   
                               </tr>
                           <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label21" runat="server" Text="LocationID" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"><asp:DropDownList ID="txtLocationId" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="95%" AutoPostBack="True" ><asp:ListItem Text="--SELECT--" Value="-1" />
</asp:DropDownList>
</td>
                             <td class="CellFormat"> <asp:Label ID="Label22" runat="server" Text="LocationName" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtLocationName" runat="server" MaxLength="15" Height="16px" Width="95%"></asp:TextBox>
</td> 
                          <td class="CellFormat"> <asp:Label ID="Label23" runat="server" Text="Branch" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtWHBranch" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>
</td> 
                    <td class="CellFormat"> <asp:Label ID="Label24" runat="server" Text="DeptSubLdgr" Width="70px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtDeptSubLdgr" runat="server" MaxLength="6" Height="16px" Width="95%"></asp:TextBox>
</td> 
                   
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label25" runat="server" Text="StaffGroup" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtStaffGroup" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>
</td>
                             <td class="CellFormat"> <asp:Label ID="Label26" runat="server" Text="Type" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:DropDownList ID="ddlType" runat="server" Width="97%"><asp:ListItem Text="--SELECT--" Value="-1" />
<asp:ListItem Value="PERMANENT">PERMANENT</asp:ListItem>
<asp:ListItem Value="CONTRACT">CONTRACT</asp:ListItem>
</asp:DropDownList>
</td> 
                          <td class="CellFormat"> <asp:Label ID="Label27" runat="server" Text="PassType" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtPassType" runat="server" MaxLength="50" Height="16px" Width="95%"></asp:TextBox>
</td> 
                    <td class="CellFormat"> <asp:Label ID="Label28" runat="server" Text="PayrollID" Width="70px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtPayrollID" runat="server" MaxLength="15" Height="16px" Width="95%"></asp:TextBox>
</td> 
                   
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label29" runat="server" Text="PassportNo" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtPassportNo" runat="server" MaxLength="25" Height="16px" Width="95%"></asp:TextBox>
</td>
                             <td class="CellFormat"> <asp:Label ID="Label30" runat="server" Text="Expiry" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtPassportExpiry" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>

                                       <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtPassportExpiry" TargetControlID="txtPassportExpiry" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>

                       
                            </td> 
                          <td class="CellFormat"> <asp:Label ID="Label31" runat="server" Text="WP/EP No" Width="80px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtWPEPNo" runat="server" MaxLength="25" Height="16px" Width="95%"></asp:TextBox>
</td> 
                    <td class="CellFormat"> <asp:Label ID="Label32" runat="server" Text="WP/EPExpiry" Width="70px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtWPEPexpiry" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>

                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtWPEPexpiry" TargetControlID="txtWPEPexpiry" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>

                       
                            </td> 
                   
                               </tr>
                          </table>
                           </asp:Panel>

                          
                      <br />
                    <table style="padding-left:30px;width:100%">
                        <tr>
                            <td style="padding-left:20px;width:100%;">
                                 <asp:Panel ID="Panel4" runat="server" BackColor="LightGray" BorderColor="Gray" BorderWidth="2px" BorderStyle="Solid" Width="100%" Height="100%">
                       <table style="padding-top:5px;padding-bottom:5px;">
                           <tr>
                               <td colspan="8" style="width:10%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;">Residential Address</td>
                           </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label33" runat="server" Text="Block" Width="50px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtResBlock" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>
</td>
                             <td class="CellFormat"> <asp:Label ID="Label34" runat="server" Text="No" Width="30px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtResNo" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>
</td> 
                          <td class="CellFormat"> <asp:Label ID="Label35" runat="server" Text="Floor" Width="40px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtResFloor" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>
</td> 
                    <td class="CellFormat"> <asp:Label ID="Label36" runat="server" Text="Unit" Width="40px"></asp:Label>
</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtResUnit" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>
</td> 
                   
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label37" runat="server" Text="Address" Width="50px"></asp:Label>
</td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtResAddr" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
</td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label38" runat="server" Text="Building" Width="50px"></asp:Label>
</td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtResBuilding" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
</td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label39" runat="server" Text="Street" Width="50px"></asp:Label>
</td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtResStreet" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
</td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label40" runat="server" Text="City" Width="40px"></asp:Label>
</td>
                            <td colspan="2" class="CellTextBox">  <asp:DropDownList ID="ddlResCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCity" DataTextField="City" DataValueField="City"><asp:ListItem Text="--SELECT--" Value="-1" />
</asp:DropDownList>
 </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label41" runat="server" Text="State" Width="40px"></asp:Label>
</td>
                            <td colspan="2" class="CellTextBox"> <asp:DropDownList ID="ddlResState" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State"><asp:ListItem Text="--SELECT--" Value="-1" />
</asp:DropDownList>
  </td>
                             <td class="CellFormat">
                                <asp:Label ID="Label42" runat="server" Text="Country" Width="40px"></asp:Label>
</td>
                            <td colspan="1" class="CellTextBox">  <asp:DropDownList ID="ddlResCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="97%" DataSourceID="SqlDSCountry" DataTextField="Country" DataValueField="Country"><asp:ListItem Text="--SELECT--" Value="-1" />
</asp:DropDownList>
 </td>
                               </tr>
                            <tr>
                          
                            <td class="CellFormat">
                                <asp:Label ID="Label43" runat="server" Text="Postal" Width="40px"></asp:Label>
</td>
                            <td colspan="2" class="CellTextBox"><asp:TextBox ID="txtResPostal" runat="server" MaxLength="20" Height="16px" Width="98%"></asp:TextBox>
  </td>
                           
                          
                            <td class="CellFormat">
                                <asp:Label ID="Label44" runat="server" Text="Tel" Width="40px"></asp:Label>
</td>
                            <td colspan="2" class="CellTextBox"> <asp:TextBox ID="txtResTel" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox>
  </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label45" runat="server" Text="Mobile" Width="40px"></asp:Label>
</td>
                            <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtResMobile" runat="server" MaxLength="50" Height="16px" Width="95%"></asp:TextBox>
  </td>
                           
                               </tr>
                              <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label46" runat="server" Text="Email" Width="40px"></asp:Label>
</td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtResEmail" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
</td>
                           
                               </tr>
                           </table>
                           </asp:Panel>

                                </td>
                            
                     
                </tr>
      </table>
                 </td>
         </tr>
           <tr>
               <td style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>


<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
</td>
           </tr>
          </table> 
                 
</ContentTemplate>

                
</asp:TabPanel>
               <asp:TabPanel runat="server" HeaderText="Client Information & Schedule" ID="TabPanel2">
        <HeaderTemplate>
            User Credentials
        
</HeaderTemplate>
        
        
<ContentTemplate>
            <table style="width:100%">
                <tr style="width:100%">
                    <td style="padding-left:100px" class="auto-style4"><asp:Button ID="btnEditUser" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="EDIT" Width="100px" /></td>
</td><td></td>
                </tr>
                 
                   <tr> <td class="auto-style5" style="text-align:right;">Staff ID </td>
                        <td class="CellTextBox"><asp:Label ID="lblStaffID1" runat="server" MaxLength="100" Height="16px" Width="60%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label>
                        </td> 
                         <td class="auto-style5"> </td> 

                   </tr>
                       
                  <tr>  <td class="auto-style5" style="text-align:right;">Staff Name </td> 
                      <td class="CellTextBox"><asp:Label ID="lblStaffName1" runat="server" MaxLength="100" Height="16px" Width="60%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label>
                        </td>
                      <td class="auto-style5"> </td> 
                  </tr>
                  <tr style="">
                    <td class="auto-style5" style="text-align:right;">&nbsp;</td>
                    <td class="CellTextBox">
                        &nbsp;</td>
                        <td class="auto-style5"> &nbsp;</td> 
                </tr>
                <tr style="">
                    <td class="auto-style5" style="text-align: right;">Group Authority</td>
                    <td class="CellTextBox">
                        <asp:DropDownList ID="txtGroupAuthority" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Width="60%">
                            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style4"></td>
                    <td class="CellFormat">
                        <asp:CheckBox ID="chkSystemUser" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px" ForeColor="Black" Text="System User" /></td>
                    <caption>
                        <asp:TextBox ID="txtWebLoginID1" runat="server" CssClass="dummybutton"></asp:TextBox>
                        <asp:TextBox ID="txtWebLoginPassword1" runat="server" CssClass="dummybutton"></asp:TextBox>
                          <asp:TextBox ID="txtMobileLoginID1" runat="server" CssClass="dummybutton"></asp:TextBox>
                        <asp:TextBox ID="txtMobileLoginPassword1" runat="server" CssClass="dummybutton"></asp:TextBox>
                    </caption>
                </tr>
                <tr style="">
                    <td class="auto-style5" style="text-align:right;">Web LoginID</td>
                    <td class="CellTextBox">
                        <asp:TextBox ID="txtWebLoginID" runat="server" BackColor="#CCCCCC"></asp:TextBox><br /></td>
                      <td class="auto-style5"> </td> 
                </tr>
                 <tr>
                    <td class="auto-style5" style="text-align:right;">Web Login Password</td>
                    <td class="CellTextBox">
                        <asp:TextBox ID="txtWebLoginPassword" runat="server"></asp:TextBox><br /></td>
                       <td class="auto-style5"> </td> 
                </tr>
                <tr>
                    <td class="auto-style5" style="text-align:right;">&nbsp;</td>
                    <td class="CellTextBox">
                        &nbsp;</td>
                      <td class="auto-style5"> &nbsp;</td> 
                </tr>
                <tr>
                    <td class="auto-style5" style="text-align: right;">Mobile Login ID</td>
                    <td class="CellTextBox">
                        <asp:TextBox ID="txtMobileLoginID" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style5" style="text-align:right;">Mobile Login Password</td>
                    <td class="CellTextBox">
                        <asp:TextBox ID="txtMobileLoginPassword" runat="server"></asp:TextBox>
                    </td>
                      <td class="auto-style5"> </td> 
                </tr>
                <tr>
                    <td class="auto-style5" style="text-align: right;">&nbsp;</td>
                    <td class="CellTextBox">
                        <asp:CheckBox ID="chkManualTimeIn" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px" ForeColor="Black" Text="Manual Time In" />
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5" style="text-align: right;">&nbsp;</td>
                    <td class="CellTextBox">
                        <asp:CheckBox ID="chkManualTimeOut" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px" ForeColor="Black" Text="Manual Time Out" />
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style5" style="text-align:right;">&nbsp;</td>
                    <td class="CellTextBox">&nbsp;</td>
                </tr>
                  <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSaveUser" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancelUser" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
            </table>
            
</ContentTemplate>
                   
</asp:TabPanel>
             <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Leave">
                 <HeaderTemplate>
                     Leave
                 
</HeaderTemplate>

                  
<ContentTemplate>
 
                <table style="padding-top:5px;width:100%;">
                    <tr>
                         <td colspan="2" style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';">
             <asp:Label ID="txtSvcMode" runat="server" CssClass="dummybutton" ></asp:Label>
     </td>
                    </tr>

                    <tr style="vertical-align: middle">
                        
 <td style="width:30%;text-align:left;">
                    <asp:Button ID="btnSvcAdd" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="90px" />
                
       <asp:Button ID="btnSvcEdit" runat="server" Font-Bold="True" Text="EDIT" Width="90px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
         
          <asp:Button ID="btnSvcDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="90px" OnClientClick = "Confirm()"/>
             </td> 

<td>   &nbsp;</td>
 <td>   &nbsp;</td>


      <td colspan="1" style="width:70%;text-align:right;vertical-align: middle">   
                             
         <asp:ImageButton ID="btnSvcSearch"  runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False"  />
                
                        </td><td></td>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align:center">
                                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Centered" DataKeyNames="Rcno" DataSourceID="SqlDataSource2" Font-Size="15px" PageSize="5">
                                    <Columns>
                                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                                        <ControlStyle Width="40px" />
                                        <ItemStyle Width="40px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="ApplicationID" HeaderText="Application ID" SortExpression="ApplicationID">
                                        <ControlStyle Width="15%" CssClass="dummybutton" />
                                        <HeaderStyle CssClass="dummybutton" />
                                        <ItemStyle CssClass="dummybutton" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApplicationDate" HeaderText="ApplicationDate" SortExpression="ApplicationDate" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Date1" HeaderText="From" SortExpression="Date1" DataFormatString="{0:dd/MM/yyyy}">
                                        <ControlStyle Width="100px" />
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Date2" HeaderText="To" SortExpression="Date2" DataFormatString="{0:dd/MM/yyyy}">
                                        <ControlStyle Width="100px" />
                                        <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" SortExpression="LeaveType" />
                                        <asp:BoundField DataField="LeaveDescription" HeaderText="Description" >
                                        <ControlStyle CssClass="dummybutton" />
                                        <HeaderStyle CssClass="dummybutton" />
                                        <ItemStyle CssClass="dummybutton" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeductDays" HeaderText="Total Days" />
                                        <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                                        <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False" />
                                        <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False" />
                                        <asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False" />
                                    </Columns>
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <HeaderStyle BackColor="#000066" Font-Bold="True" Font-Names="Calibri" ForeColor="White" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E4E4E4" Font-Names="Calibri" ForeColor="#8C4510" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#E4E4E4" />
                                    <SortedAscendingHeaderStyle BackColor="#000066" />
                                    <SortedDescendingCellStyle BackColor="#E4E4E4" />
                                    <SortedDescendingHeaderStyle BackColor="#000066" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblleave WHERE StaffID = @StaffId order by Date1">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="lblStaffID" Name="@StaffId" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                    </tr>

                    </table>
                

              <table class="Centered" style="padding-top:5px;width:50%;">
                    

                    <tr>
                        

                        <td colspan="4">
                            

                                <td class="CellFormat">&nbsp;</td>
                           <td class="CellTextBox">
                               <asp:TextBox ID="txtLeaveDescription" runat="server" Height="16px" MaxLength="100" Width="99%" CssClass="dummybutton"></asp:TextBox>
                           </td>

                        </td>
                        

                    </tr>
                    
                  <tr> <td class="auto-style3">Staff ID </td>
                        <td colspan="3" class="CellTextBox"><asp:Label ID="lblStaffID" runat="server" MaxLength="100" Height="16px" Width="60%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label>
                        </td> </tr>
                       
                  <tr>  <td class="auto-style3">Staff Name </td> 
                      <td colspan="3" class="CellTextBox"><asp:Label ID="lblStaffName" runat="server" MaxLength="100" Height="16px" Width="60%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label>
                        </td>

                  </tr>
                  <tr>
                      <td colspan="4"><br /></td>
                  </tr>
                   
                  <tr>
                        

                        <td class="auto-style3">Application Date<asp:Label ID="Label56" runat="server" Font-Size="18px" ForeColor="Red" Height="18px" Text="*"></asp:Label>
                        </td>
                        

                        <td class="CellTextBox">
                            <asp:TextBox ID="txtApplicationDate" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                            <asp:CalendarExtender ID="txtApplicationDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtApplicationDate" TargetControlID="txtApplicationDate">
                            </asp:CalendarExtender>
                        </td>
                        

                           <td class="auto-style2">Leave Type<asp:Label ID="Label55" runat="server" Font-Size="18px" ForeColor="Red" Height="18px" Text="*"></asp:Label>
                        </td>
                           <td class="CellTextBox">
                               <asp:DropDownList ID="ddlLeaveType" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSLeaveType" DataTextField="LeaveType" DataValueField="LeaveType" Width="99%">
                                   <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                               </asp:DropDownList>
                           </td>
                        

                        <td class="CellTextBox">
                            &nbsp;</td>

                        



                    </tr>

                       <tr>
                        

                        <td class="auto-style3">Date From<asp:Label ID="Label53" runat="server" Font-Size="18px" ForeColor="Red" Text="*" Height="18px"></asp:Label></td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtDateFrom" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> 
                               </td>
                         

                         <td class="CellFormat">Date To<asp:Label ID="Label54" runat="server" Font-Size="18px" ForeColor="Red" Height="18px" Text="*"></asp:Label>
                           </td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtDateTo" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="txtDateTo" TargetControlID="txtDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> 
                    
                                 </td>

                        



                    </tr>

                    
                    

                  
                    

                   
                    

                
                        

                        <tr>
                            <td class="auto-style3">Total Days</td>
                            <td class="CellTextBox" colspan="1">
                                <asp:TextBox ID="txtTotalDays" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                               
                              
                            </td>
                            <td class="CellFormat">&nbsp;</td>
                            <td class="CellTextBox" colspan="1">&nbsp;</td>
                    </tr>

                        



                    </tr>
                    

                 
                                     

                                          

                    </tr>
                                  
                                       

           </tr>
                    

                    <tr>
                        <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">
                            &nbsp;</td>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                            <tr>
                                <td colspan="4" style="text-align:right;">
                                    <asp:Button ID="btnLeaveSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" />
                                    <asp:Button ID="btnSvcCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" />
                                    <asp:TextBox ID="txtSvcRcno" runat="server" Visible="False" Width="10%"></asp:TextBox>
                                    1</td>
                            </tr>
                        </tr>
                    </tr>
                    

              </table>
            
</ContentTemplate>
             
</asp:TabPanel>
             </asp:TabContainer>

           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">

        </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSState" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT State FROM tblstate WHERE (Rcno &lt;&gt; 0) ORDER BY State"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSCity" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT City FROM tblcity WHERE (Rcno &lt;&gt; 0) ORDER BY City"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSCountry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Country FROM tblcountry WHERE (Rcno &lt;&gt; 0) ORDER BY Country"></asp:SqlDataSource>
        
            <asp:SqlDataSource ID="SqlDSLeaveType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(LeaveType, '-', LeaveDesc) AS LeaveType FROM tblleavetype ORDER BY LeaveType"></asp:SqlDataSource>
        
            <asp:SqlDataSource ID="SqlDSStaffId" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId  FROM tblStaff ORDER BY StaffId  "></asp:SqlDataSource>
        
            <br />
        <div style="text-align:CENTER">
            <asp:LinkButton ID="btnTop" runat="server" Font-Names="Calibri" Font-Bold="True" ForeColor="Brown">Go to Top</asp:LinkButton></div>
         </div>
         <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="550px" Height="350px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" class="auto-style6">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Status Change</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageStatus" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertStatus" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         <tr>
                             <td class="CellFormat">StaffID</td>
                             <td class="CellTextBox">
                                 <asp:Label ID="lblStaffIDStatus" runat="server" Width="70%" BackColor="#CCCCCC"></asp:Label></td>
                         </tr>
                          <tr>
                               <td class="CellFormat">ExistingStatus
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:Label ID="lblOldStatus" runat="server" width="70%" BackColor="#CCCCCC"></asp:Label></td>
                           </tr>
                          <tr>
                               <td class="CellFormat">NewStatus
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlNewStatus" runat="server" Width="71%" Height="25px" AutoPostBack="True">
                                     <asp:ListItem Value="O">O - Open</asp:ListItem>
                                  <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                <asp:ListItem Value="R">R - Resigned</asp:ListItem>
                                 <asp:ListItem Value="D">D - Disabled</asp:ListItem>                            
                               </asp:DropDownList></td>
                           </tr>
                           
                         <tr>
                             <td class="CellFormat">Date Left</td>
                             <td class="CellTextBox" colspan="1">
                             <asp:TextBox ID="txtDateLeftStatus" runat="server" Height="16px" Width="71%" AutoCompleteType="Disabled" TabIndex="21" ></asp:TextBox>
                              <asp:CalendarExtender ID="calConDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtReceiptDate" TargetControlID="txtDateLeftStatus" Enabled="True" />    
                             </td>
                         </tr>
                           
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                          
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnUpdateStatus" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Update Status" Width="120px"  OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelStatus" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>                

        </table>
           </asp:Panel>
     <asp:ModalPopupExtender ID="mdlPopupStatus" runat="server" CancelControlID="btnCancelStatus" PopupControlID="pnlStatus" TargetControlID="btndummyStatus" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
      <asp:Button ID="btndummyStatus" runat="server" cssclass="dummybutton" />

            <asp:Panel ID="Panel2" runat="server" BackColor="White" Width="400px" Height="220" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto">
              <br /><br />
                     <table style="width:100%;padding-left:15px">
                       <tr>
                               <td class="CellFormat">
                                <asp:Label ID="Label48" runat="server" Text="Staff ID" Width="70px"></asp:Label></td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchStaffID" runat="server" MaxLength="25" Height="16px" Width="95%" AutoPostBack="true"></asp:TextBox></td>
                       </tr>
                           <tr>
                               <td class="CellFormat">
                                <asp:Label ID="Label51" runat="server" Text="Name" Width="70px"></asp:Label></td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchName" runat="server" MaxLength="25" Height="16px" Width="95%" AutoPostBack="true"></asp:TextBox></td>
                       </tr>
                          <tr>
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Search" Width="100px"/>
                            <asp:Button ID="btnClose" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
                         </tr>

        </table>
           </asp:Panel>
 <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnClose" PopupControlID="Panel2" TargetControlID="btnFilter" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
  
        <asp:Panel ID="Panel3" runat="server" BackColor="White" Width="600px" Height="250" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto">
              <br /><br />
                     <table style="width:100%;padding-left:15px">
                       <tr>
                               <td class="CellFormat">
                                <asp:Label ID="Label52" runat="server" Text="Staff ID" Width="70px"></asp:Label></td>
                            <td class="CellTextBox" colspan="1"> 
                                <asp:DropDownList ID="txtLeavePrintStaffId" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSStaffId" DataTextField="StaffId" DataValueField="StaffId" Width="94%">
                                    <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                               </td>
                       </tr>
                           <tr>
                        

                       <td class="CellFormat">Date From</td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtLeavePrintDateFrom" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="txtLeavePrintDateFrom" TargetControlID="txtLeavePrintDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> 
                               </td>
                         

                         <td class="CellFormat">Date To
                           </td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtLeavePrintDateTo" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="txtLeavePrintDateTo" TargetControlID="txtLeavePrintDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender> 
                    
                                 </td>
                               </tr>
                         <tr>
                             <td colspan="4"><br /></td>
                         </tr>
                                               <tr>
                             <td colspan="4" style="text-align:center"><asp:Button ID="btnPrintLeaveRpt" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>
                            <asp:Button ID="btnPrintCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
                         </tr>

        </table>
           </asp:Panel>
 <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="btnPrintCancel" PopupControlID="Panel3" TargetControlID="btnPrintLeave" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

  
   
   
        <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="5px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
      <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
      <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
     <asp:TextBox ID="txtSearchStaffText" runat="server" Visible="False"></asp:TextBox>
</asp:Content>


