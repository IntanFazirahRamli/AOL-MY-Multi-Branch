<%@ Page Title="Customer Portal" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CustomerPortal.aspx.vb" Inherits="CustomerPortal" EnableEventValidation="false" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 <%--<%@ Register Assembly="System.Web" Namespace="System.Web.UI.HtmlControls" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link rel="Stylesheet" type="text/css" href="CSS/loader.css" />--%>

     <style type="text/css">
         .gridcell {
             word-break: break-all;
         }
       
         .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}
     
    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:20%;
      
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
      .CellTextBox{
         font-family:'Calibri';
        color:black;
        text-align:left;
     padding-left:20px;
    }
    .CellTextBox1{
         font-family:'Calibri';
        color:black;
        text-align:left;
     
    }
          </style>
    <style type="text/css">
     .cell
{
text-align:left;
}

.righttextbox
{
float:right;

}
        .AlphabetPager a, .AlphabetPager span
        {
            font-size: 8pt;
            display: inline-block;
            /*height: 10px;
            line-height: 15px;*/
            min-width: 15px;
            text-align: center;
            text-decoration: none;
            font-weight: bold;
            /*padding: 0 1px 0 1px;*/
            
        }
        .AlphabetPager a
        {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }
        .AlphabetPager span
        {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }
 
    table.fixed { table-layout:fixed; }
table.fixed td { overflow: hidden; }
  </style>

   
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

    var defaultText = "Search Here for Location Address, Postal Code or Description";
    function WaterMark(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultText;
        }
        if (txt.value == defaultText && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

    var defaultTextCopyAccess = "Search Here for User Access details";
    function WaterMarkCopyAccess(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextCopyAccess;
        }
        if (txt.value == defaultTextCopyAccess && evt.type == "focus") {
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

     
        //document.getElementById("<%=txtCreatedOn.ClientID%>").value = '';
  
        var strct = y + "/" + mm + "/" + dd + " " + hh + ":" + MM + ":" + ss;
        var endct = new Date(strct);
        document.getElementById("<%=txtCreatedOn.ClientID%>").value = dd + "/" + mm + "/" + y + " " + hh + ":" + MM + ":" + ss;
       
    }


    function DoValidation(parameter) {

        var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency

        var expr1 = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        var valid = true;

        currentdatetime();

      

        return valid;
    };


  
    function checkalllocationrecs() {
        //alert("1");
        var table = document.getElementById('<%=grvServiceRecDetails.ClientID%>');
       
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

  
    function checkallaccessrecs() {
        //alert("1");
        var table = document.getElementById('<%=grdCopyAccess.ClientID%>');

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
                     ShowModalPopup()
                 }
             }
         }

     }

    function ShowModalPopup() {
        $find("mdlCopyAccess").show();
        return false;
    }

    function ResetScrollPosition() {
        setTimeout("window.scrollTo(0,0)", 0);
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
        .auto-style5 {
            width: 96%;
        }
        </style>
    <%--  <link rel="stylesheet" type="text/css" href="CSS/loading-bar.css"/>
<script type="text/javascript" src="JS/loading-bar.js"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    cc<asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>
 
     <asp:UpdatePanel ID="updPanelCompany" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
           <asp:controlBundle name="ModalPopupExtender_Bundle"/>
            <asp:controlBundle name="ListSearchExtender_Bundle"/>
               <asp:controlBundle name="TabContainer_Bundle"/>
                <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>
   </ControlBundles>
    </asp:ToolkitScriptManager>     


        
       <div style="text-align:center">

     
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">CUSTOMER PORTAL</h3>
       
        <table border="0" style="width:100%;text-align:center;">
            <tr>
                <td><br /></td>
            </tr>
            <tr>
               <td style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
            <tr>
                 <td style="width:100%;text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="txtMode" runat="server" Text="" Visible="true" CssClass="dummybutton"></asp:Label>
                      </td> 
            </tr>
            <tr>
                <td style="width:100%;text-align:left;"> 
                    <table border="0" style="width:100%">
                        <tr>

                    <td style="width:10%;text-align:left;">
                        &nbsp;</td>            
                      
                       
                 <td style="width:8%;text-align:right;"> <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px" Height="30px" />
             <asp:Label ID="Label1" Style="color: white" runat="server" Visible="false" Text="Label"></asp:Label> 
                       </td>
                        </tr>
                    </table> </td>
            </tr>
            <tr>
     <td style="text-align:right">
          
</td>
            </tr>
           
           
            <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:20px;">
<asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="0" Font-Names="Calibri" Height="100%" Width="100%" CssClass="ajax__tab_xp"  AutoPostBack="True">
                   
      <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Notes"><HeaderTemplate>Customer Portal Access</HeaderTemplate><ContentTemplate><div style="text-align:center"><h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">User Information</h3><table style="width:100%;text-align:center;"><tr><td colspan="5" style="text-align:left;"><asp:Button ID="btnAddCP" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px"  /><asp:Button ID="btnEditCP" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /><asp:Button ID="btnChStCP" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CH-ST" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Visible="False"/>
          <asp:Button ID="btnDeleteCP" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Visible="False"/><asp:Button ID="btnPrintCP" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" /><asp:Button ID="btnResetPwd" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="RESET PASSWORD" Width="150px" /><asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CHANGE STATUS" Width="150px" /><asp:Button ID="btnCloseCP" runat="server" Visible="False"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CLOSE" Width="100px" /></td><td style="text-align:left;">&nbsp;</td></tr>
          <tr><td colspan="5"><br /></td><td>&nbsp;</td></tr>
          
          <tr><td colspan="6"><br />

              <table style="text-align:right;width:100%">
            <tr>
                  <td style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="text-align:right;width:65%;display:inline;">
         <asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="RESET SEARCH" /></td>
                    <td style="text-align:left;width:35%">    <asp:TextBox ID="txtSearchCust" runat="server" Width="350px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True"></asp:TextBox>      
                        &nbsp; <asp:Button ID="btnGoCust" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="GO" Width="50px" Height="30px" />
                            <asp:TextBox ID="txtSearchCustText" runat="server" CssClass="dummybutton" ></asp:TextBox>    </td>
            </tr>
        </table>
              </td>
              
          </tr>

          <tr class="Centered"><td colspan="5">
              
                   <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1px" Height="100%" ScrollBars="Auto" style="text-align:center; width:1250px; margin-left:auto; margin-right:auto;" Width="1330px">
          
              <asp:GridView ID="gvCP" runat="server" DataSourceID="SqlDSCP" OnRowDataBound = "OnRowDataBoundgCP" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" GridLines="Vertical" ForeColor="#333333" AllowPaging="True" ><AlternatingRowStyle BackColor="White" />
                  <Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False" ><ControlStyle Width="150px" /><ItemStyle Width="150px" /></asp:CommandField>
                      <asp:BoundField DataField="AccountID" SortExpression="AccountID"><ControlStyle Width="150px" /><HeaderStyle Font-Size="12pt" CssClass="dummybutton" /><ItemStyle Width="60px" HorizontalAlign="Left" CssClass="dummybutton" /></asp:BoundField>
                      <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ><ItemStyle Width="150px" /></asp:BoundField>
                      <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" />
                      <asp:BoundField DataField="UserID" HeaderText="User ID" ><ItemStyle Width="100px" /></asp:BoundField><asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"><ControlStyle Width="300px" /><HeaderStyle Font-Size="12pt" /><ItemStyle Width="60px" HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="StatusRemarks" HeaderText="Status Remarks" SortExpression="StatusRemarks" />
          <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" DataFormatString="{0:dd/MM/yyyy}" />
                       
                    <asp:TemplateField HeaderText="Dashboard Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvDashBoardAccess" runat="server" Checked='<%# IIf(Eval("DashBoardAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Request Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvRequestAccess" runat="server" Checked='<%# IIf(Eval("RequestAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="SMART Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTDashBoardAccess" runat="server" Checked='<%# IIf(Eval("SMARTDashBoardAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="SMART Dashboard Advanced Information">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTDashboardAdvanced" runat="server" Checked='<%# IIf(Eval("SMARTDashboardAdvancedInformation").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
<asp:TemplateField HeaderText="SMART Floor Plan View Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTFloorPlanView" runat="server" Checked='<%# IIf(Eval("SMARTFloorPlanViewAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="SMART Floor Plan Activity CountAccess">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTFloorPlanActivityCount" runat="server" Checked='<%# IIf(Eval("SMARTFloorPlanActivityCountAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
           
                       <asp:TemplateField HeaderText="SMART Floor Plan Exclude Business Hours Data Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTFloorPlanExcludeBusinessHours" runat="server" Checked='<%# IIf(Eval("SMARTFloorPlanExcludeBusinessHoursDataAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SMART Daily Chart Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTDailyChart" runat="server" Checked='<%# IIf(Eval("SMARTDailyChartAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                             <asp:TemplateField HeaderText="SMART Weekly Chart Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTWeeklyChart" runat="server" Checked='<%# IIf(Eval("SMARTWeeklyChartAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="SMART Monthly Chart Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTMonthlyChart" runat="server" Checked='<%# IIf(Eval("SMARTMonthlyChartAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="SMART Yearly Chart Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTYearlyChart" runat="server" Checked='<%# IIf(Eval("SMARTYearlyChartAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="SMART Daily Movement Catch Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSMARTDaiyMovementCatchAccess" runat="server" Checked='<%# IIf(Eval("SMARTDailyMovementCatchAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Smart Zone Email Notification High">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSmartZoneEmailNotificationHigh" runat="server" Checked='<%# IIf(Eval("SmartZoneEmailNotificationHigh").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Smart Zone Email Notification Medium">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSmartZoneEmailNotificationMedium" runat="server" Checked='<%# IIf(Eval("SmartZoneEmailNotificationMedium").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                     <asp:TemplateField HeaderText="Smart Device Email Notification High">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSmartDeviceEmailNotificationHigh" runat="server" Checked='<%# IIf(Eval("SmartDeviceEmailNotificationHigh").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Smart Device Email Notification Medium">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvSmartDeviceEmailNotificationMedium" runat="server" Checked='<%# IIf(Eval("SmartDeviceEmailNotificationMedium").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                         <asp:TemplateField HeaderText="Service Record Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvServiceRecordAccess" runat="server" Checked='<%# IIf(Eval("ServiceRecordAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="View Open Service Records">
                 <HeaderStyle Wrap="True" />
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvViewOpenServiceRecords" runat="server" Checked='<%# IIf(Eval("ViewOpenServiceRecords").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Invoice Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvInvoiceAccess" runat="server" Checked='<%# IIf(Eval("InvoiceAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="CreditNote Access">
                 <HeaderStyle Wrap="True" />
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvCreditNoteAccess" runat="server" Checked='<%# IIf(Eval("CreditNoteAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="DebitNote Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvDebitNoteAccess" runat="server" Checked='<%# IIf(Eval("DebitNoteAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Receipt Access">
                 <HeaderStyle Wrap="True" />
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvReceiptAccess" runat="server" Checked='<%# IIf(Eval("ReceiptAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Journal Adjustment Access">
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvJournalAdjustment" runat="server" Checked='<%# IIf(Eval("JournalAdjustmentAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Reports Access">
                 <HeaderStyle Wrap="True" />
                <ItemTemplate>
                      <asp:CheckBox ID="chkgvReports" runat="server" Checked='<%# IIf(Eval("ReportsAccess").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>
                      
                  <asp:BoundField DataField="LastLogin" HeaderText="Last Login" SortExpression="LastLogin" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="CreatedBy" HeaderText="Entry Staff" SortExpression="CreatedBy" /><asp:BoundField DataField="CreatedOn" HeaderText="Entry Date" SortExpression="CreatedOn" /><asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" /><asp:BoundField DataField="LastModifiedOn" HeaderText="Edited On" SortExpression="LastModifiedOn" /><asp:BoundField DataField="AccountType" SortExpression="AccountType" ><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Password"><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="ChangePasswordOnNextLogin"><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" />

                            </asp:GridView>
                       </asp:Panel>


                       </td><td>&nbsp;</td></tr><tr><td colspan="5"><br /></td><td>&nbsp;</td></tr><tr style="display:none"><td class="CellFormat">Account ID<asp:Label ID="Label44" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtAccountIDCP" runat="server" MaxLength="50" Height="16px" Width="50%"></asp:TextBox></td><td  style="text-align:left"></td><td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat">Name<asp:Label ID="Label47" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="4"><asp:TextBox ID="txtNameCP" runat="server" MaxLength="200" Height="16px" Width="50%"></asp:TextBox></td><td class="CellTextBox">&nbsp;</td></tr><tr style="display:none"><td class="CellFormat">Account Type</td><td class="CellTextBox"colspan="4"><asp:TextBox ID="txtAccountTypeCP" runat="server" Enabled="False" Height="16px" MaxLength="200" Width="50%"></asp:TextBox></td><td class="CellTextBox">&nbsp;</td></tr>
          <tr><td class="CellFormat">Email<asp:Label ID="Label45" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
          <td class="CellTextBox" colspan="4"><asp:TextBox ID="txtEmailCP" runat="server" Height="16px" MaxLength="200" Width="50%"></asp:TextBox></td>
              <td class="CellTextBox">&nbsp;</td></tr><caption>&gt;
                  <tr><td class="CellFormat">User ID</td>
                      <td class="CellTextBox" colspan="4"><asp:TextBox ID="txtUserIDCP" runat="server" Height="16px" MaxLength="25" Width="50%" Enabled="False"></asp:TextBox></td>
                      <td class="CellTextBox">&nbsp;</td></tr>
                  <tr><td class="CellFormat">Password</td>
                      <td class="CellTextBox" colspan="4"><asp:TextBox ID="txtPwdCP" runat="server" Height="16px" MaxLength="50" Width="50%" Enabled="False"></asp:TextBox></td>
                      <td class="CellTextBox">&nbsp;</td></tr>
                  <tr><td class="CellFormat">Status </td>
                      <td class="CellTextBox" colspan="1">
          <asp:DropDownList ID="ddlStatus" runat="server" CssClass="chzn-select" Width="30%" AutoPostBack="True"><asp:ListItem Selected="True" Value="A">A - Active</asp:ListItem><asp:ListItem Value="I">I - InActive</asp:ListItem><asp:ListItem Value="D">D - DeActivated</asp:ListItem></asp:DropDownList></td>
                  <td class="CellFormat">Request Access</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkRequestAccess" runat="server" />
          </td>
              <td class="CellTextBox">&nbsp;</td></tr>

              <tr><td class="CellFormat">Change Password on Next Logon</td>
                  <td class="CellTextBox"><asp:CheckBox ID="chkChangePasswordonNextLogin" runat="server" /></td>
          <td class="CellFormat">Dashboard Access</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkDashBoardAccess" runat="server" />
          </td>
          <td class="CellTextBox">&nbsp;</td></tr>
                  
                             <tr style="display:none"><td class="CellFormat">
                                 </td><td class="CellTextBox" colspan="4">
                                     &nbsp;</td><td class="CellTextBox">&nbsp;</td></tr>
                  <tr style="display:none">
                      <td class="CellFormat"></td>
                      <td class="CellTextBox" colspan="4">
                          &nbsp;</td>
                      <td class="CellTextBox">&nbsp;</td>
          </tr>
                  <tr style="display:none"><td class="CellFormat">Service Location Access</td>
                      <td class="CellTextBox"><asp:CheckBox ID="chkServiceLocationAccess" runat="server" />

                      </td>
                      <td class="CellFormat" colspan="2">Dashboard Access</td>
                      <td class="CellTextBox">  
                          &nbsp;</td>
                      <td class="CellTextBox">&nbsp;</td></tr>

                           <tr><td class="CellFormat">Service Record Access</td><td class="CellTextBox"><asp:CheckBox ID="chkServiceRecordAccess" runat="server" /></td>
                               <td class="CellFormat">SMART Dashboard Access </td>
                               <td class="CellTextBox" colspan="2">
                                   <asp:CheckBox ID="chkSMARTDashBoardAccess" runat="server" />
                               </td>
                               <td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat">View Open Service Records</td><td class="CellTextBox"><asp:CheckBox ID="chkViewOpenServiceRecord" runat="server" /></td>
          <td class="CellFormat">SMART Dashboard Advanced Info</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkSMARTDashboardAdvancedInformation" runat="server" />
          </td>
          <td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat">Invoice Access</td><td class="CellTextBox"><asp:CheckBox ID="chkInvoiceAccess" runat="server" /></td>
          <td class="CellFormat">SMART Floor Plan View</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkSMARTFloorPlanViewAccess" runat="server" />
          </td>
          <td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat">Credit Note Access</td><td class="CellTextBox"><asp:CheckBox ID="chkCNAccess" runat="server" /></td>
          <td class="CellFormat">SMART Floor Plan Show Activity Count</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkSMARTFloorPlanActivityCountAccess" runat="server" />
          </td>
          <td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat">Debit Note Access</td><td class="CellTextBox"><asp:CheckBox ID="chkDNAccess" runat="server" /></td>
          <td class="CellFormat">SMART Floor Plan Exclude Hours</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkSMARTFloorPlanExcludeHoursAccess" runat="server" />
          </td>
          <td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat">Receipt Access</td>
          <td class="CellTextBox"><asp:CheckBox ID="chkReceiptAccess" runat="server" /></td>
          <td class="CellFormat">SMART Daily Chart</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkSMARTDailyChartAccess" runat="server" />
          </td>
          <td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat">Adjustment Note Access</td><td class="CellTextBox"><asp:CheckBox ID="chkAdjustmentAccess" runat="server" /></td>
          <td class="CellFormat">SMART Weekly Chart</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkSMARTWeeklyChartAccess" runat="server" />
          </td>
          <td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat">Report Access</td><td class="CellTextBox"><asp:CheckBox ID="chkReportAccess" runat="server" /></td>
          <td class="CellFormat">SMART Monthly Chart</td>
          <td class="CellTextBox" colspan="2">
              <asp:CheckBox ID="chkSMARTMonthlyChartAccess" runat="server" />
          </td>
          <td class="CellTextBox">&nbsp;</td></tr>
                  
       

          <tr>
              <td class="CellFormat">Expiry Date</td>
              <td class="CellTextBox">
                  <asp:TextBox ID="txtExpiryDate" runat="server" Height="16px" MaxLength="10" Width="20%"></asp:TextBox>
                  <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtExpiryDate" TargetControlID="txtExpiryDate">
                  </asp:CalendarExtender>
              </td>
              <td class="CellFormat">SMART Yearly Chart</td>
              <td class="CellTextBox" colspan="2">
                  <asp:CheckBox ID="chkSMARTYearlyChartAccess" runat="server" />
              </td>
              <td class="CellTextBox">&nbsp;</td>
          </tr>
          <tr><td class="CellFormat">Remarks</td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtStatusRemarks" runat="server" Height="16px" MaxLength="50" Width="50%"></asp:TextBox></td>
              <td class="CellFormat">SMART Daily Movement Catch Access</td>
              <td class="CellTextBox" colspan="2">
                  <asp:CheckBox ID="chkSMARTDailyMovementAccess" runat="server" />
              </td>
              <td class="CellTextBox">&nbsp;</td></tr>
             
              <tr style="border: 1px solid #808080; border-radius: 25px;">
                  <td class="CellFormat"> SMART Email Notification</td>
                  <td colspan="4">
                       <table style="width:100%;text-align:center;border: 1px solid #808080; border-radius: 5px;">                                
                                     <tr>
                                          <td class="CellTextBoxADM">SMART Zone Email Notification - Medium
                                             &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSmartZoneEmailMedium" runat="server" TextAlign="Left" />
                                         <td class="CellTextBoxADM">SMART Zone Email Notification - High
                                             &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSmartZoneEmailHigh" runat="server" TextAlign="Left" /></td>
                                            
                                         </td>
                                     </tr>
                                     <tr>
                                         <td class="CellTextBoxADM">SMART Device Email Notification - Medium
                                             <asp:CheckBox ID="chkSmartDeviceEmailMedium" runat="server" />
                                         <td class="CellTextBoxADM">SMART Device Email Notification - High
                                             <asp:CheckBox ID="chkSmartDeviceEmailHigh" runat="server" /></td>
                                             
                                         </td>

                                     </tr>                               

                              </table>
                  </td>
                  <td class="CellTextBox">&nbsp;</td>
              </tr>
           
          
              <tr><td colspan="5" style="text-align:right"><asp:Button ID="btnSaveCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" /><asp:Button ID="btnCancelCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" /></td><td style="text-align:right">&nbsp;</td></tr>

                                                                                                                                                                                               </caption></table><asp:SqlDataSource ID="SqlDSCP" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div><asp:TextBox ID="txtCPRcno" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtCPMode" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtCPModeUserAccess" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtCPUserAccessRcno" runat="server" CssClass="dummybutton"></asp:TextBox></ContentTemplate></asp:TabPanel>

        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Notes">
            <HeaderTemplate><asp:Label ID="lblAccessCount" runat="server" Font-Size="11px" Text="User Access"></asp:Label></HeaderTemplate>
            <ContentTemplate><div style="text-align:center">
                <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">User Access</h3>
                <table style="width:100%;text-align:center;">
                    <tr><td colspan="2" style="text-align:left;">
                        <asp:Button ID="btnAddCPUserAccess" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px"  />
                        <asp:Button ID="btnEditCPUserAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
                        <asp:Button ID="btnDeleteCPUserAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
                        <asp:Button ID="btnLocations" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="LOCATIONS" Width="180px"  OnClientClick = "currentdatetime()"/>
                            <asp:Button ID="btnCopyAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="COPY ACCESS" Width="120px"  OnClientClick = "currentdatetime()"/>
                    
                        <asp:Button ID="Button6" runat="server" Visible="False"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CLOSE" Width="100px" />

                        </td></tr>
                    <tr><td colspan="2"><br /></td></tr>
                    <tr class="Centered"><td colspan="2">
                        <asp:GridView ID="gvNotesMaster" runat="server" DataSourceID="SqlDSCPUserAccess" OnRowDataBound = "OnRowDataBoundgNotes" OnSelectedIndexChanged = "OnSelectedIndexChangedgNotes" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" GridLines="Vertical" ForeColor="#333333" ><AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False" ><ControlStyle Width="150px" /><ItemStyle Width="150px" /></asp:CommandField>
                                <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" />
                                <asp:BoundField DataField="AccountType" HeaderText="Account Type" SortExpression="AccountType" >
                               
                                </asp:BoundField>
                                <asp:BoundField DataField="AccountName" HeaderText="Account Name" SortExpression="AccountName" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="LocationAccessType" HeaderText="Location Access Type" SortExpression="LocationAccessType" /><asp:BoundField DataField="CreatedBy" HeaderText="EntryStaff" SortExpression="CreatedBy" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EntryDate" SortExpression="CreatedOn" /><asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" /><asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" /></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat">User ID</td><td class="CellTextBox"><asp:TextBox ID="txtUserIDCPUserAccess" runat="server" MaxLength="50" Height="16px" Width="50%" Enabled="False"></asp:TextBox></td></tr><tr><td class="CellFormat">User Name</td><td class="CellTextBox"><asp:TextBox ID="txtUserNameCPUserAccess" runat="server" Enabled="False" Height="16px" MaxLength="50" Width="50%"></asp:TextBox></td></tr><tr><td class="CellFormat">Account Type</td><td class="CellTextBox"><asp:DropDownList ID="txtAccountTypeCPUserAccess" runat="server" AutoPostBack="True" Height="20px" TabIndex="22" Width="50%"><asp:ListItem>--SELECT--</asp:ListItem><asp:ListItem Value="CORPORATE">CORPORATE</asp:ListItem><asp:ListItem Value="RESIDENTIAL">RESIDENTIAL</asp:ListItem></asp:DropDownList></td></tr>
                    <tr><td class="CellFormat">Account ID<asp:Label ID="Label46" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td style="text-align:left; padding-left:20px"  ><asp:TextBox ID="txtAccountIDCPUserAccess" runat="server" Height="16px" MaxLength="50" Width="50%" AutoPostBack="True"></asp:TextBox>&nbsp; <asp:ImageButton ID="btnClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" /></td></tr><tr><td class="CellFormat">Account Name</td><td class="CellTextBox"><asp:TextBox ID="txtAccountNameUserAccess" runat="server" Height="16px" MaxLength="200" Width="50%"></asp:TextBox></td></tr><tr><td class="CellFormat">Location Access Type<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlLocationAccessType" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="50%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem><asp:ListItem>ALL LOCATIONS</asp:ListItem><asp:ListItem>DEFINED LOCATIONS</asp:ListItem></asp:DropDownList></td></tr><tr><td colspan="2" style="text-align:right"><asp:Button ID="btnSaveUserAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/><asp:Button ID="btnCancelUserAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table><asp:SqlDataSource ID="SqlDSCPUserAccess" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div><asp:TextBox ID="TextBox7" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="TextBox8" runat="server" CssClass="dummybutton"></asp:TextBox></ContentTemplate></asp:TabPanel>
            
    
         </asp:TabContainer>
                    
                        <%-- popup1--%>

           <asp:Panel ID="pnlLocation" runat="server" ScrollBars="vertical" style="overflow:scroll" Wrap="false"  BackColor="White" Width="99%" Height="450px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
       
             <table border="0" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label43" runat="server" Text="LOCATION DETAILS"></asp:Label>
            </td>

               </tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100% ">
        
        
        <asp:UpdatePanel ID="updpnlServiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="grvServiceRecDetails" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="100%">
                
                  <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" TextAlign="Right" onchange="checkalllocationrecs()"  Width="5%" ></asp:CheckBox></HeaderTemplate>
                <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" Checked='<%# Bind("Selected")%>'    CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
           
                <asp:TemplateField HeaderText="Location Id"><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Text='<%# Bind("LocationID")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent"  BorderStyle="None" Height="18px"  Width="95px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Contract Group"><ItemTemplate><asp:TextBox ID="txtContractGroupGV" runat="server" Text='<%# Bind("ContractGroup")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent"  BorderStyle="None" Height="18px"  Width="95px"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
                            <asp:TemplateField HeaderText="Client Name"><ItemTemplate><asp:TextBox ID="txtClientNameGV" runat="server" Text='<%# Bind("ServiceName")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px"  Width="340px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Text='<%# Bind("Address1")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="340px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Building"><ItemTemplate><asp:TextBox ID="txtBuildingGV" runat="server" Text='<%# Bind("AddBuilding")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="100px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Street"><ItemTemplate><asp:TextBox ID="txtStreetGV" runat="server" Text='<%# Bind("AddStreet")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="110px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="City"><ItemTemplate><asp:TextBox ID="txtCityGV" runat="server" Text='<%# Bind("AddCity")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="State"><ItemTemplate><asp:TextBox ID="txtStateGV" runat="server" Text='<%# Bind("AddState")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Country"><ItemTemplate><asp:TextBox ID="txtCountryGV" runat="server" Text='<%# Bind("AddCountry")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Postal"><ItemTemplate><asp:TextBox ID="txtPostalGV" runat="server" Text='<%# Bind("AddPostal")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="55px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
              </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" />

            </asp:GridView>
            </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvServiceRecDetails" EventName="SelectedIndexChanged" /></Triggers>
              </asp:UpdatePanel>&nbsp;</td></tr>
                 
        </table>
               
                <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                      
                         
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                       <asp:Button ID="btnSaveLocation" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SAVE" Width="15%" Visible="True" OnClientClick="currentdatetime()"/>
                       &nbsp;  
                           <asp:Button ID="btnClose" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CANCEL" Width="15%" Visible="True" />
                    
                      
                        </td>
                 
            </tr>
                    </table>
         </asp:Panel>

      <asp:ModalPopupExtender ID="mdlPopupLocation" runat="server" CancelControlID="btnClose" PopupControlID="pnlLocation" TargetControlID="btndummyLocation" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyLocation" runat="server" cssclass="dummybutton" />

                     
                        <%-- popup1--%>

           <asp:Panel ID="pnlCopyAccess" runat="server" ScrollBars="vertical" style="overflow:scroll" Wrap="false"  BackColor="White" Width="90%" Height="550px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
       
                  <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">COPY ACCESS FROM EXISTING USER</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageCopyAccess" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertCopyAccess" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                      <tr>
                          <td colspan="2" style="width:100%;text-align:left" class="CellFormat">
                              Enter the UserID from which you want to copy the access &nbsp;
                          <%--</td> 
                          <td colspan="1" style="width:50%;text-align:left">--%>
                              <asp:TextBox ID="txtSearchCopyAccess" Visible="true" runat="server" MaxLength="50" Height="16px" Width="300px" Text = "Search Here for User Access details" ForeColor = "Gray" onblur = "WaterMarkCopyAccess(this, event);" onfocus = "WaterMarkCopyAccess(this, event);" AutoPostBack="True"></asp:TextBox>
                     <%--<asp:ImageButton ID="btnResetCopyAccess" runat="server" ImageUrl="~/Images/reset1.png" Height="16px" Width="18px" ToolTip="RESET CUST REQUEST" />--%>
 
        <asp:TextBox ID="txtSearchCopyAccess1" runat="server" Visible="False"></asp:TextBox>
                                  <%-- <asp:TextBox ID="txtCopyAccessUserID" runat="server" Visible="False"></asp:TextBox>--%>
                          </td>
                      </tr>
                       <tr>
                      <td class="CellFormat" style="width:20%">UserID</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtCopyAccessUserID" ReadOnly="true" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ForeColor="Black"></asp:TextBox>
           
                      </td>
                                         
                  </tr>
                       <tr>
                      <td class="CellFormat" style="width:20%">Name</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtCopyAccessName" ReadOnly="true" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ForeColor="Black"></asp:TextBox>
           
                      </td>
                                         
                  </tr>
                        <tr>
                      <td class="CellFormat" style="width:20%">EmailAddress</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtCopyAccessEmailAddress" ReadOnly="true" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ForeColor="Black"></asp:TextBox>
           
                      </td>
                                         
                  </tr>
             <table border="0" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label5" runat="server" Text="COPY ACCESS FROM EXISTING USER"></asp:Label>
            </td>

               </tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100% ">
        
        
        <asp:UpdatePanel ID="UpdatePanelCopyAccess" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="grdCopyAccess" runat="server" AllowSorting="True"  
             AutoGenerateColumns="false" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="100%" EmptyDataText="THERE ARE NO RECORDS TO BE COPIED" EmptyDataRowStyle-ForeColor="Black" EmptyDataRowStyle-HorizontalAlign="Center">
                
                  <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" TextAlign="Right" onchange="checkallaccessrecs()"  Width="5%" ></asp:CheckBox></HeaderTemplate>
                <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Checked="true" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
                    <asp:TemplateField HeaderText="AccountID"><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" Text='<%# Bind("AccountID")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent"  BorderStyle="None" Height="18px"  Width="95px"></asp:TextBox></ItemTemplate></asp:TemplateField>
      
                <asp:TemplateField HeaderText="Location Id"><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Text='<%# Bind("LocationID")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent"  BorderStyle="None" Height="18px"  Width="95px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Contract Group"><ItemTemplate><asp:TextBox ID="txtContractGroupGV" runat="server" Text='<%# Bind("ContractGroup")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent"  BorderStyle="None" Height="18px"  Width="95px"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
                            <asp:TemplateField HeaderText="Client Name"><ItemTemplate><asp:TextBox ID="txtClientNameGV" runat="server" Text='<%# Bind("ServiceName")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px"  Width="340px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Text='<%# Bind("Address1")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="340px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Building"><ItemTemplate><asp:TextBox ID="txtBuildingGV" runat="server" Text='<%# Bind("AddBuilding")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="100px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Street"><ItemTemplate><asp:TextBox ID="txtStreetGV" runat="server" Text='<%# Bind("AddStreet")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="110px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="City"><ItemTemplate><asp:TextBox ID="txtCityGV" runat="server" Text='<%# Bind("AddCity")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="State"><ItemTemplate><asp:TextBox ID="txtStateGV" runat="server" Text='<%# Bind("AddState")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Country"><ItemTemplate><asp:TextBox ID="txtCountryGV" runat="server" Text='<%# Bind("AddCountry")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Postal"><ItemTemplate><asp:TextBox ID="txtPostalGV" runat="server" Text='<%# Bind("AddPostal")%>' Font-Size="12px" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Height="18px" Width="55px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           <%--  <asp:TemplateField HeaderText="">
                                                              <EditItemTemplate>
                                                                  <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AccessRCNO")%>'></asp:TextBox>
                                                              </EditItemTemplate>
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblAccessRCNO" runat="server" Text='<%# Bind("AccessRCNO")%>'></asp:Label>
                                                              </ItemTemplate>
                                                              <ControlStyle Width="8%" />
                                                              <ItemStyle Width="8%" Wrap="False" HorizontalAlign="LEFT" />
                                                          </asp:TemplateField>--%>
                         <asp:TemplateField HeaderText="">
                                                              <EditItemTemplate>
                                                                  <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("LocationRCNO")%>'></asp:TextBox>
                                                              </EditItemTemplate>
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblLocationRCNO" runat="server" Text='<%# Bind("LocationRCNO")%>'></asp:Label>
                                                              </ItemTemplate>
                                                              <ControlStyle Width="8%" />
                                                              <ItemStyle Width="8%" Wrap="False" HorizontalAlign="LEFT" />
                                                          </asp:TemplateField>
              </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" />

            </asp:GridView>
                         <asp:SqlDataSource ID="SqlDSCopyAccess" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                          
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="grdCopyAccess" EventName="SelectedIndexChanged" />--%>
                <asp:PostBackTrigger ControlID="txtSearchCopyAccess" />
                <asp:PostBackTrigger ControlID="grdCopyAccess" />
            </Triggers>
              </asp:UpdatePanel>&nbsp;</td></tr>
                 
        </table>
               
                <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                      
                         
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                       <asp:Button ID="btnSaveCopyAccess" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SAVE" Width="15%" Visible="True" OnClientClick="currentdatetime()"/>
                       &nbsp;  
                           <asp:Button ID="btnCancelCopyAccess" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CANCEL" Width="15%" Visible="True" />
                    
                      
                        </td>
                 
            </tr>
                    </table>
         </asp:Panel>

      <asp:ModalPopupExtender ID="mdlCopyAccess" runat="server" CancelControlID="btnCancelCopyAccess" PopupControlID="pnlCopyAccess" TargetControlID="btndummyCopyAccess" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyCopyAccess" runat="server" cssclass="dummybutton" />

                   <%--  start--%>

                            <asp:Panel ID="pnlConfirmPost" runat="server" BackColor="White" Width="500px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:20px; width: 90%;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center" class="auto-style5">
                         
                          <asp:Label ID="lblEvent" runat="server" Text="Confirm RESET Password"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td class="auto-style5"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;width:98%">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to RESET Password for "></asp:Label>
                        
                      </td>
                           </tr>
                            <tr >
                                <td class="CellFormat" style="text-align: center; margin-left: auto; margin-right: auto; width:98%">
                                    
                                    &nbsp;<asp:TextBox ID="txtAccountIDCPReset" runat="server" Height="16px" MaxLength="50" Width="98%" BorderStyle="None" style="text-align:center" ></asp:TextBox>

                                </td>
              </tr>
                            <tr>
                             <td class="CellFormat" style="text-align: center; margin-left: auto; margin-right: auto; width:90%">
                                 
                                 &nbsp;
                                </td>
                         </tr>
              <caption>
                  <br />
                  <tr>
                      <td class="CellFormat" style="text-align: center; margin-left: auto; margin-right: auto; width:98%">&nbsp;</td>
                  </tr>
                  <tr style="padding-top:40px;">
                      <td style="text-align: center; width:98%">
                          <asp:Button ID="btnConfirmYes" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Yes" Width="100px" />
                          <asp:Button ID="btnConfirmNo" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="No" Width="100px" />
                      </td>
                  </tr>
              </caption>
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmPost" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmPost" TargetControlID="btndummyPost" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyPost" runat="server" CssClass="dummybutton" />
                   <%-- end--%>

                   <%-- start--%>

                     <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
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
                               <td class="CellFormat">Change Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlNewStatus" runat="server" Width="51%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                  <asp:ListItem Value="A">A - Active</asp:ListItem>
                                   <asp:ListItem Value="I">I - Inactive</asp:ListItem> 
                                   <asp:ListItem Value="D">D - DeActivated</asp:ListItem>
                                    
                               </asp:DropDownList></td>
                           </tr>
                           
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnUpdateStatus" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Update Status" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelStatus" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                        
        </table>
           </asp:Panel>

           <asp:ModalPopupExtender ID="mdlPopupStatus" runat="server" CancelControlID="btnCancelStatus" PopupControlID="pnlStatus" TargetControlID="btndummy1" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
         
          <asp:Button ID="btndummy1" runat="server" CssClass="dummybutton" />


                   <%-- end--%>

                         <%--  start--%>

                            <asp:Panel ID="pnlConfirmRemarks" runat="server" BackColor="White" Width="500px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:20px; width: 90%;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center" class="auto-style5">
                         
                          <asp:Label ID="Label3" runat="server" Text="Confirm Status Remarks"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td class="auto-style5"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;width:98%">
                         
                          &nbsp;<asp:Label ID="Label4" runat="server" Text="Do you wish to clear the Status Remarks? "></asp:Label>
                        
                      </td>
                           </tr>
                            <tr >
                                <td class="CellFormat" style="text-align: center; margin-left: auto; margin-right: auto; width:98%">
                                    
                                    &nbsp;<asp:TextBox ID="txtChangeStatusRemarks" runat="server" Height="16px" MaxLength="50" Width="98%" BorderStyle="None" style="text-align:center" ></asp:TextBox>

                                </td>
              </tr>
                          
              <caption>
                  <br />
                  <tr>
                      <td class="CellFormat" style="text-align: center; margin-left: auto; margin-right: auto; width:98%">&nbsp;</td>
                  </tr>
                  <tr style="padding-top:40px;">
                      <td style="text-align: center; width:98%">
                          <asp:Button ID="btnConfirmRemarksYes" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Yes" Width="100px" />
                          <asp:Button ID="btnConfirmRemarksNo" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="No" Width="100px" />
                      </td>
                  </tr>
              </caption>
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlConfirmRemarks" runat="server" CancelControlID="btnConfirmRemarksNo" PopupControlID="pnlConfirmRemarks" TargetControlID="btndummyRemarks" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyRemarks" runat="server" CssClass="dummybutton" />
                   <%-- end--%>


                   <%-- Start: Select Account ID--%>

      <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="98%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center" ScrollBars="Horizontal">
    <table border="0" style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
    </td></tr>
        
        <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160;
         <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
    <asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg"  Width="24px" Visible="False"></asp:ImageButton>
    <asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" OnClick="btnPopUpClientReset_Click" Width="24px"></asp:ImageButton>


</td><td>
                <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>

            </td></tr>


    </table><div style="text-align:center; padding-left: 20px; padding-bottom: 5px;"><div class="AlphabetPager">
          <asp:Repeater ID="rptClientAlphabets" runat="server" Visible="False"><ItemTemplate>
        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>' ForeColor="Black" />
        </ItemTemplate>
        </asp:Repeater>
                
</div><br />
                    <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                       CellPadding="2" GridLines="Vertical" Font-Size="15px" Width="97%" OnRowDataBound = "OnRowDataBoundgClient" OnSelectedIndexChanged = "OnSelectedIndexChangedgClient" CellSpacing="6">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
        <ControlStyle Width="5%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Width="5%" />
        </asp:CommandField>
            <asp:BoundField DataField="AccountType" HeaderText="Account Type" />
        <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" >
        <ControlStyle Width="8%" />

        <HeaderStyle Wrap="False" HorizontalAlign="Left" />

        </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="Client Name" SortExpression="Name">
        <ControlStyle Width="35%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="Address1" HeaderText="Address" >
            <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="AddStreet" HeaderText="Street">

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" HorizontalAlign="Left" />
        </asp:BoundField>
            <asp:BoundField DataField="AddBuilding" HeaderText="Building">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
        <asp:BoundField DataField="AddCity" HeaderText="City" >

        <ItemStyle Wrap="False" />
        </asp:BoundField>
            <asp:BoundField DataField="AddCountry" HeaderText="Country">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="AddPostal" HeaderText="Postal">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="ContactPerson">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="CompanyGroup">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" />
            </asp:BoundField>
        </Columns>

        <EditRowStyle BackColor="#999999" />

        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

        <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />

        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />

        <RowStyle BackColor="#EFF3FB" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />

        <SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" />

        <SortedAscendingCellStyle BackColor="#E9E7E2" />

        <SortedAscendingHeaderStyle BackColor="#506C8C" />

        <SortedDescendingCellStyle BackColor="#FFFDF8" />

        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>



                <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  
        </asp:SqlDataSource>
                
                <asp:SqlDataSource ID="SqlDSPerson" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
       
        </asp:SqlDataSource>
        </div>

      </asp:Panel>

      <asp:ModalPopupExtender ID="mdlPopupAccountID" runat="server" CancelControlID="btnClose" PopupControlID="pnlPopUpClient" TargetControlID="btndummyAccountID" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyAccountID" runat="server" cssclass="dummybutton" />     
                    <%--End: select Account ID--%>
               
</td>
                </tr>
            <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:20px;">&nbsp;</td>
            </tr>
            </table>
           <br />

          
           


           <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />

        
                 <asp:TextBox ID="txtBlock" runat="server" MaxLength="10" Height="16px" Width="50px" onchange="UpdateBillingDetails()" Visible="false"></asp:TextBox>
                          <asp:TextBox ID="txtNo" runat="server" MaxLength="10" Height="16px" Width="50px" onchange="UpdateBillingDetails()" Visible="false"></asp:TextBox>
                          <asp:TextBox ID="txtFloor" runat="server" MaxLength="10" Height="16px" Width="50px" onchange="UpdateBillingDetails()" Visible="false"></asp:TextBox>
                          <asp:TextBox ID="txtUnit" runat="server" MaxLength="10" Height="16px" Width="50px" onchange="UpdateBillingDetails()" Visible="false"></asp:TextBox>
                          <asp:TextBox ID="txtBillBlock" runat="server" MaxLength="10" Height="16px" Width="50px" Visible="false"></asp:TextBox>
                           <asp:TextBox ID="txtBillNo" runat="server" MaxLength="10" Height="16px" Width="50px" Visible="false"></asp:TextBox>
                           <asp:TextBox ID="txtBillFloor" runat="server" MaxLength="10" Height="16px" Width="50px" Visible="false"></asp:TextBox>
                          <asp:TextBox ID="txtBillUnit" runat="server" MaxLength="10" Height="16px" Width="55px" Visible="false"></asp:TextBox>
                         
                          
                            <asp:TextBox ID="txtSpecCode" runat="server" MaxLength="15" Height="16px" Width="150px" Visible="False"></asp:TextBox>
                         <asp:TextBox ID="txtARCurrency" runat="server" MaxLength="20" Height="16px" Width="70px" Visible="False"></asp:TextBox></td>
                                      <asp:TextBox ID="txtAPCurrency" runat="server" MaxLength="20" Height="16px" Width="70px" Visible="False"></asp:TextBox>
                             <asp:CheckBox ID="chkConsolidate" runat="server" Text="Consolidate" Visible="False" /> 
                                  <asp:TextBox ID="txtROC" runat="server" MaxLength="20" Height="16px" Width="98%" Visible="false"></asp:TextBox>
                           
                          
                            <asp:TextBox ID="txtRegDate" runat="server" MaxLength="25" Height="16px" Width="150px" Visible="False"></asp:TextBox>
                               <asp:CalendarExtender ID="CalendarExtender2" runat="server" targetcontrolid="txtRegDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                         <asp:CheckBox ID="chkCustomer" runat="server" Text="IsCustomer" Visible="False" />
                                 <asp:CheckBox ID="chkSupplier" runat="server" Text="IsSupplier" Visible="False" /><%--    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
            
           
                      
  <%--           <asp:Panel ID="Panel6" runat="server" BackColor="White" Width="500" Height="650" BorderColor="#003366" BorderWidth="1" Visible="true" HorizontalAlign="Left" ScrollBars="None">

                           <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Contact Person</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPanelClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;padding-left:40px;">Search Name
                                 <asp:TextBox ID="txtSearchName" runat="server" MaxLength="50" Height="16px" Width="200px"></asp:TextBox>
                                   <asp:ImageButton ID="btnSearchName" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22" Width="24" />
                            </td> <td></td>
                                      </tr>
                           </table>
              <div style="text-align:center;padding-left:50px;padding-bottom:5px;">      
                 
     
                   <div class="AlphabetPager">
    <asp:Repeater ID="rptAlphabets" runat="server">
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>' OnClick="Alphabet_Click" ForeColor="Black" />
          
        </ItemTemplate>
    </asp:Repeater>
</div>
               <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False" CellPadding="4" GridLines="None" Width="380px" RowStyle-HorizontalAlign="Left" PageSize ="15">
                   <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                   <Columns>
                 
                         <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="50px" />
                <ItemStyle Width="50px" />
                </asp:CommandField>
                       <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" >
                       <ControlStyle Width="150px" />
                       <HeaderStyle Width="150px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
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
                      
                      </div>
              </asp:Panel>--%>             
          
<%--    <asp:ImageButton ID="btnSearchContact" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" Height="22" Width="24" />
                   --%>
      
         
        <asp:SqlDataSource ID="sqlDSLocations" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                </asp:SqlDataSource>

       
        
                 <%--$(document).ready(function () { $("#ddlIndustry").select2(); });--%>
           <asp:TextBox ID="txtGoogleEmail" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="False"></asp:TextBox>

       <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>
             <asp:TextBox ID="txtDetail" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>
           <asp:TextBox ID="txtSelectDate" runat="server" Visible="False"></asp:TextBox>
        <%-- <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>--%>
        <asp:TextBox ID="txtSelectedRow" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>        
        <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox> 
           
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White"  ></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
                    <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtModal" runat="server" Visible="False"></asp:TextBox>
              <asp:TextBox ID="TextBox3" runat="server" Visible="false" ></asp:TextBox>
          <asp:TextBox ID="txtCustomerSearch" runat="server" Visible="false" ></asp:TextBox>
           <asp:Label ID="lblDomainName" runat="server" Font-Names="Calibri" Visible="False" Font-Size="18px" Font-Bold="True" ForeColor="#000099"></asp:Label>

      

        
    
    </div>
            </ContentTemplate> 
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveCopyAccess" />
            <%--<asp:PostBackTrigger ControlID="grdCopyAccess" />--%>
             <asp:PostBackTrigger ControlID="tb1$TabPanel2$btnCopyAccess" />
              <asp:PostBackTrigger ControlID="txtSearchCopyAccess" />
            <%--<asp:PostBackTrigger ControlID="tb1$TabPanel2$grdCopyAccess" />--%>
        </Triggers>
     </asp:UpdatePanel>
</asp:Content>

