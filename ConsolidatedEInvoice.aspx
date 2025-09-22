<%@ page title="Consolidated EInvoice" language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" CodeFile="ConsolidatedEInvoice.aspx.vb" inherits="ConsolidatedEInvoice" culture="en-GB" enableeventvalidation="false" %>


<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<script runat="server">

   
</script>



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


         
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

    
<script type="text/javascript">
    window.history.forward(1);
    window.onbeforeunload = preventMultipleSubmissions();


    window.onload = function () {
        initialize();
    };


    var isSubmitted = false;

    function preventMultipleSubmissions() {
        //  alert("1");
        if (!isSubmitted) {
            //     alert("2");
            $('#<%=btnSaveInvoice.ClientID%>').val('SAVE SCHEDULE..WAIT');
            isSubmitted = true;
            currentdatetime();
            return true;
        }
        else {
            //      alert("3");
            currentdatetime();
            return false;
        }
    }

    function allowSubmissions() {
        //alert("4");
        isSubmitted = false;
        return false;
    }

    var defaultText1 = "Search Here";
    function WaterMarkStaff(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultText1;
        }
        if (txt.value == defaultText1 && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

    function btn_disable() {

        document.getElementById("<%=btnSaveInvoice.ClientID%>").disabled = true;
        return true;
    }

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


    function checkallservicerecs() {
        var table = document.getElementById('<%=grvInvoiceRecDetails.ClientID%>');
        var totbillamt = 0;


        if (table.rows.length > 0) {

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

                    document.getElementById("<%=txtTotalSelected.ClientID%>").value = "0.00";

                 }


                 else {
                     //loop the gridview table
                     for (var i = 1; i < table.rows.length; i++) {

                         //get all the input elements
                         var inputs = table.rows[i].getElementsByTagName("input");

                         for (var j = 0; j < inputs.length; j++) {

                             //get the textbox1
                             if (inputs[j].id.indexOf("txtToBillAmtGV") > -1) {
                                 inputs[0].checked = true;
                                 totbillamt = totbillamt + parseFloat(inputs[j].value);

                             }

                         }
                     }

                     document.getElementById("<%=txtTotalSelected.ClientID%>").value = (totbillamt).toFixed(2);

            }
        }
    }

}


function checkoneservicerec() {
    //alert("1");
    var table = document.getElementById('<%=grvInvoiceRecDetails.ClientID%>');
        var totbillamt = 0;


        if (table.rows.length > 0) {
            //alert("2");

            var input = table.rows[0].getElementsByTagName("input");

            //loop the gridview table
            for (var i = 1; i < table.rows.length; i++) {

                //get all the input elements
                var inputs = table.rows[i].getElementsByTagName("input");

                for (var j = 0; j < inputs.length; j++) {

                    //get the textbox1
                    if (inputs[0].checked == true) {
                        if (inputs[j].id.indexOf("txtToBillAmtGV") > -1) {

                            totbillamt = totbillamt + parseFloat(inputs[j].value);
                        }
                    }
                }
            }

            document.getElementById("<%=txtTotalSelected.ClientID%>").value = (totbillamt).toFixed(2);
        }
    }

<%--    function EnableDisableRecurringInvoice() {

        var lInvoiceDate = document.getElementById("<%=txtInvoiceDate.ClientID%>").value;
        var RecurringInvoice = document.getElementById("<%=chkRecurringInvoice.ClientID%>").checked;
        var lBillingFrequency = document.getElementById("<%=ddlBillingFrequency.ClientID%>").value;
        var lDateFrom = document.getElementById("<%=txtDateFrom.ClientID%>").value;
        var lDateTo = document.getElementById("<%=txtDateTo.ClientID%>").value;

        document.getElementById("<%=lblAlert.ClientID%>").value = '';

        if (RecurringInvoice == false) {

            document.getElementById("<%=txtRecurringInvoiceDate.ClientID%>").setAttribute("disabled", "true");
            document.getElementById("<%=txtRecurringServiceDateFrom.ClientID%>").setAttribute("disabled", "true");
            document.getElementById("<%=txtRecurringServiceDateTo.ClientID%>").setAttribute("disabled", "true");
            document.getElementById("<%=ddlRecurringFrequency.ClientID%>").setAttribute("disabled", "true");



            document.getElementById("<%=txtRecurringInvoiceDate.ClientID%>").value = "";
            document.getElementById("<%=txtRecurringServiceDateFrom.ClientID%>").value = "";
            document.getElementById("<%=txtRecurringServiceDateTo.ClientID%>").value = "";
            document.getElementById("<%=ddlRecurringFrequency.ClientID%>").value = "--SELECT--";


        }
        else if (RecurringInvoice == true) {


            if (lBillingFrequency == '-1') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Billing Frequency";
                document.getElementById("<%=chkRecurringInvoice.ClientID%>").checked = false;
                ResetScrollPosition();
                document.getElementById("<%=ddlBillingFrequency.ClientID%>").focus();
                valid = false;
                return valid;
            }

            if (lDateFrom == '') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select DateFrom";
                document.getElementById("<%=chkRecurringInvoice.ClientID%>").checked = false;
                ResetScrollPosition();
                document.getElementById("<%=txtDateFrom.ClientID%>").focus();
                valid = false;
                return valid;
            }


            if (lDateTo == '') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Date To";
                document.getElementById("<%=chkRecurringInvoice.ClientID%>").checked = false;
                ResetScrollPosition();
                document.getElementById("<%=txtDateTo.ClientID%>").focus();
                valid = false;
                return valid;
            }

            document.getElementById("<%=txtRecurringInvoiceDate.ClientID%>").removeAttribute("disabled");
            document.getElementById("<%=txtRecurringServiceDateFrom.ClientID%>").removeAttribute("disabled");
            document.getElementById("<%=txtRecurringServiceDateTo.ClientID%>").removeAttribute("disabled");
            document.getElementById("<%=ddlRecurringFrequency.ClientID%>").removeAttribute("disabled");


            document.getElementById("<%=txtRecurringServiceDateFrom.ClientID%>").style.backgroundColor = document.getElementById("<%=txtInvoiceDate.ClientID%>").style.backgroundColor;
            document.getElementById("<%=txtRecurringServiceDateTo.ClientID%>").style.backgroundColor = document.getElementById("<%=txtInvoiceDate.ClientID%>").style.backgroundColor;
            document.getElementById("<%=txtRecurringInvoiceDate.ClientID%>").style.backgroundColor = document.getElementById("<%=txtInvoiceDate.ClientID%>").style.backgroundColor;


            if (lBillingFrequency != "-1") {
                //      alert (lBillingFrequency);
                if ((lBillingFrequency == "DAILY") || (lBillingFrequency == "WEEKLY") || (lBillingFrequency == "MONTHLY") || (lBillingFrequency == "QUARTERLY") || (lBillingFrequency == "ANNUALLY") || (lBillingFrequency == "BI-MONTHLY") || (lBillingFrequency == "HALF-ANNUALLY")) {
                    document.getElementById("<%=ddlRecurringFrequency.ClientID%>").value = lBillingFrequency;
                }



                ////////////


                var timein = lInvoiceDate.split('/');

                var startdate = parseInt(timein[0], 10);
                var startmonth = parseInt(timein[1], 10);
                var startyear = parseInt(timein[2], 10);

                var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";

                var startTime = new Date(str3);
                var origdate = new Date(str3);

                if (lBillingFrequency == "DAILY") {
                    startTime.setDate(startTime.getDate() + dur);

                    var dd = startTime.getDate() - 1;

                    if (dd == 0)
                        dd = dd + 1;


                    var mm = startTime.getMonth() + 1;
                    var y = startTime.getFullYear();
                }

                else if (lBillingFrequency == "WEEKLY") {
                    startTime.setDate(startTime.getDate());
                    startTime.setDate(startTime.getDate() + (1 * 7));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 1;
                    var y = startTime.getFullYear();
                }

                else if (lBillingFrequency == "MONTHLY") {
                    // alert("aa");
                    startTime.setDate(startTime.getDate());
                    startTime.setMonth(startTime.getMonth() + (1));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 1;
                    var y = startTime.getFullYear();


                }

                else if (lBillingFrequency == "BI-MONTHLY") {
                    // alert("aa");
                    startTime.setDate(startTime.getDate());
                    startTime.setMonth(startTime.getMonth() + (1));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 2;
                    var y = startTime.getFullYear();


                }
                else if (lBillingFrequency == "QUARTERLY") {
                    // alert("aa");
                    startTime.setDate(startTime.getDate());
                    startTime.setMonth(startTime.getMonth() + (1));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 3;
                    var y = startTime.getFullYear();


                }

                else if (lBillingFrequency == "HALF-ANNUALLY") {
                    // alert("aa");
                    startTime.setDate(startTime.getDate());
                    startTime.setMonth(startTime.getMonth() + (1));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 6;
                    var y = startTime.getFullYear();


                }
                else if (lBillingFrequency == "ANNUALLY") {

                    startTime.setDate(startTime.getDate() - 1);

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 1;
                    var y = startTime.getFullYear() + (1);
                }

                if (dd < 10) {
                    dd = "0" + dd;
                }
                if (mm < 10)
                    mm = "0" + mm;



                document.getElementById("<%=txtRecurringInvoiceDate.ClientID%>").value = dd + "/" + mm + "/" + y;





                //////////////////////


            if ((lDateFrom == "") || (lDateTo == "")) {
            }
            else {

                ////////////


                var timein = lDateFrom.split('/');
                // alert(timein);
                var startdate = parseInt(timein[0], 10);
                var startmonth = parseInt(timein[1], 10);
                var startyear = parseInt(timein[2], 10);

                var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";


                var startTime = new Date(str3);
                var origdate = new Date(str3);


                if (lBillingFrequency == "DAILY") {
                    startTime.setDate(startTime.getDate() + dur);

                    var dd = startTime.getDate() - 1;

                    if (dd == 0)
                        dd = dd + 1;


                    var mm = startTime.getMonth() + 1;
                    var y = startTime.getFullYear();
                }

                else if (lBillingFrequency == "WEEKLY") {
                    startTime.setDate(startTime.getDate());
                    startTime.setDate(startTime.getDate() + (1 * 7));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 1;
                    var y = startTime.getFullYear();
                }

                else if (lBillingFrequency == "MONTHLY") {
                    // alert("aa");
                    startTime.setDate(startTime.getDate());
                    startTime.setMonth(startTime.getMonth() + (1));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 1;
                    var y = startTime.getFullYear();


                }

                else if (lBillingFrequency == "BI-MONTHLY") {
                    // alert("aa");
                    startTime.setDate(startTime.getDate());
                    startTime.setMonth(startTime.getMonth() + (1));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 2;
                    var y = startTime.getFullYear();


                }
                else if (lBillingFrequency == "QUARTERLY") {
                    // alert("aa");
                    startTime.setDate(startTime.getDate());
                    startTime.setMonth(startTime.getMonth() + (1));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 3;
                    var y = startTime.getFullYear();


                }

                else if (lBillingFrequency == "HALF-ANNUALLY") {
                    // alert("aa");
                    startTime.setDate(startTime.getDate());
                    startTime.setMonth(startTime.getMonth() + (1));

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 6;
                    var y = startTime.getFullYear();


                }
                else if (lBillingFrequency == "ANNUALLY") {

                    startTime.setDate(startTime.getDate() - 1);

                    var dd = startTime.getDate();
                    var mm = startTime.getMonth() + 1;
                    var y = startTime.getFullYear() + (1);
                }

                if (dd < 10) {
                    dd = "0" + dd;
                }
                if (mm < 10)
                    mm = "0" + mm;



                document.getElementById("<%=txtRecurringServiceDateFrom.ClientID%>").value = dd + "/" + mm + "/" + y;

                ////////////

                    var timein = lDateTo.split('/');
                // alert(timein);
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";


                    var startTime = new Date(str3);
                    var origdate = new Date(str3);


                    if (lBillingFrequency == "DAILY") {
                        startTime.setDate(startTime.getDate() + dur);

                        var ddd = startTime.getDate() - 1;

                        if (ddd == 0)
                            ddd = ddd + 1;


                        var mmm = startTime.getMonth() + 1;
                        var yy = startTime.getFullYear();
                    }

                    else if (lBillingFrequency == "WEEKLY") {
                        startTime.setDate(startTime.getDate());
                        startTime.setDate(startTime.getDate() + (1 * 7));

                        var ddd = startTime.getDate();
                        var mmm = startTime.getMonth() + 1;
                        var yy = startTime.getFullYear();
                    }

                    else if (lBillingFrequency == "MONTHLY") {
                        // alert("aa");
                        startTime.setDate(startTime.getDate() - 1);
                        startTime.setMonth(startTime.getMonth() + (1));

                        var ddd = startTime.getDate();
                        var mmm = startTime.getMonth() + 1;
                        var yy = startTime.getFullYear();


                    }

                    else if (lBillingFrequency == "BI-MONTHLY") {
                        // alert("aa");
                        startTime.setDate(startTime.getDate() - 1);
                        startTime.setMonth(startTime.getMonth() + (1));

                        var ddd = startTime.getDate();
                        var mmm = startTime.getMonth() + 2;
                        var yy = startTime.getFullYear();


                    }
                    else if (lBillingFrequency == "QUARTERLY") {
                        // alert("aa");
                        startTime.setDate(startTime.getDate() - 1);
                        startTime.setMonth(startTime.getMonth() + (1));

                        var ddd = startTime.getDate();
                        var mmm = startTime.getMonth() + 3;
                        var yy = startTime.getFullYear();


                    }

                    else if (lBillingFrequency == "HALF-ANNUALLY") {
                        // alert("aa");
                        startTime.setDate(startTime.getDate() - 1);
                        startTime.setMonth(startTime.getMonth() + (1));

                        var ddd = startTime.getDate();
                        var mmm = startTime.getMonth() + 6;
                        var yy = startTime.getFullYear();


                    }
                    else if (lBillingFrequency == "ANNUALLY") {

                        startTime.setDate(startTime.getDate() - 1);

                        var ddd = startTime.getDate();
                        var mmm = startTime.getMonth() + 1;
                        var yy = startTime.getFullYear() + (1);
                    }

                    if (ddd < 10) {
                        ddd = "0" + ddd;
                    }
                    if (mmm < 10)
                        mmm = "0" + mmm;

                    document.getElementById("<%=txtRecurringServiceDateTo.ClientID%>").value = ddd + "/" + mmm + "/" + yy;
                }
            }
        }


} --%>

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
        document.getElementById("<%=txtBatchDate.ClientID%>").value = dd + "/" + mm + "/" + y;
            document.getElementById("<%=txtInvoiceDate.ClientID%>").value = dd + "/" + mm + "/" + y;
            document.getElementById("<%=txtBillingPeriod.ClientID%>").value = y + mm;
           <%--     document.getElementById("<%=txtBillingPeriodSearch.ClientID%>").value = y + mm;  --%>
        }


    function initialize() {

        currentdatetimeinvoice();

        currentdatetime();

        isSubmitted = false;

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


        function ServiceLock() {
            alert("Service Record is Locked.. Bill Amount cannot be Modified");
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

                 .roundbutton1 {
                border: 2px solid #a1a1a1;   
                background: #dddddd;   
                border-radius: 25px;
                height:30px;
                width:90%;
        }
        </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>

      <asp:UpdatePanel ID="updPanelInvoice" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                <asp:controlBundle name="ComboBox_Bundle"/>  
                <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>             
            </ControlBundles>
        </asp:ToolkitScriptManager>
     

    <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />
               
   
     <div style="text-align:center">
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">CONSOLIDATED INVOICE</h3>
          
          <asp:UpdatePanel ID="updPnlMsg" runat="server" UpdateMode="Conditional">
              <ContentTemplate>


         <table  style="width:95%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; "  >
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
                   <asp:Button ID="btnADD" runat="server" Font-Bold="True" Text="ADD" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="TRUE" OnClientClick="initialize()"  />
                 
                      </td>
                  
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" OnClientClick="return allowSubmissions(); " Visible="TRUE" Width="95%" />
                    </td>
                     
                   <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnDelete" runat="server" Font-Bold="True" Text="VOID" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick="currentdatetime();  Confirmvoid()" />
                    </td>
                  <td>
                   
                          <asp:Button ID="btnPostEinvoice" runat="server" Font-Bold="True" Text="POST CONSOLIDATED INVOICE" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick="currentdatetime();" />
               
                  </td>
                      

                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="RESET" Visible="False" Width="95%" />
                 
                       </td>
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnADDRecurring" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="initialize()" Text="CREATE RECURRING" Visible="False" Width="95%" />
                      </td>
                
                  <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnPost" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="POST" Width="75%" OnClientClick="ConfirmPost();" Visible="False" />
                  </td>
                  
                    <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="REVERSE" Width="75%" OnClientClick="ConfirmUnPost();" Visible="False"/>
                  </td>

                  

                   <td style="width:8%;text-align:center;">
                         <asp:Button ID="btnQuit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="QUIT" Width="95%" />
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
           </div>

          <asp:UpdatePanel ID="updPnlSearch" runat="server" UpdateMode="Conditional"><ContentTemplate>

             <table  id="tablesearch" runat="server" style="border: 1px solid #CC3300; text-align:right;  border-radius: 25px;padding: 2px; width:95%; height:60px; background-color: #F3F3F3;">
            <tr>
                <td style="text-align:left;width:100%;">
                   
                    <table border="0" style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000;width:100%;padding-left:2px;">
                         <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:12%; ">
                                    Consolidated Invoice No.                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:12%; ">
                                 <asp:TextBox ID="txtInvoicenoSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>         
                            </td>
                             
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                   UUID</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:12%; ">
                                  <asp:TextBox ID="txtUUIDIdSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left; " Width="75%"></asp:TextBox>
                                </td>

                             
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8%; ">
                                    Batch Invoice Date</td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%; ">
                                 <asp:TextBox ID="txtFromInvoiceDate" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="100%" AutoCompleteType="Disabled" Visible="TRUE" ></asp:TextBox>         
                                     <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFromInvoiceDate" TargetControlID="txtFromInvoiceDate" Enabled="True" />

                                   </td>

                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8%; ">
                                    To</td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%;padding-right:10px ">
                                 <asp:TextBox ID="txtToInvoiceDate" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="100%" AutoCompleteType="Disabled" Visible="TRUE" ></asp:TextBox>         
                                     <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToInvoiceDate" TargetControlID="txtToInvoiceDate" Enabled="True" />

                                   </td>
                              <td colspan="1" style="text-align:center">
                                       <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Search" Width="95%" />
                                 </td>
                         
                              <td> 
                                       <asp:Button ID="btnQuickReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Reset" Width="95%" OnClientClick="ClearSearch()" />
                                   </td>
                        </tr>
                    </table>
                      </td>
                    <td style="text-align:right;width:45%;display:inline;vertical-align:middle;padding-top:10px;">
                 
                </td>
            </tr>
        </table>


     


         <table  style="width:97%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
         
             
                            <tr style="text-align:center;">
                                  <td colspan="11" style="width:100%;text-align:center">
                                      
                     <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1330px">
      
                                          <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSInvoice" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                                <Columns>
                                                  <asp:CommandField ShowHeader="True" ShowSelectButton="True" Visible="false">
                                                  <ControlStyle Width="2%" />
                                                  <ItemStyle Width="2%" Wrap="False" HorizontalAlign="Left" />
                                                  </asp:CommandField>
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
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Post St" >
                                                    <ControlStyle Width="2%" />
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ConsolidatedInvoiceNo" HeaderText="Consolidated Invoice No." SortExpression="ConsolidatedInvoiceNo" >
                                                    <ControlStyle Width="6%" />
                                                    <ItemStyle Width="6%" Wrap="False" />
                                                    </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="CIDate" HeaderText="Batch Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="SalesDate">
                                                    <ControlStyle Width="3%" />
                                                    <ItemStyle Width="3%" />
                                                  </asp:BoundField>
                                                    <asp:BoundField DataField="UUID" HeaderText="UUID" />
                                                    <asp:BoundField DataField="CompanyGroupSearch" HeaderText="Company Group" />
                                                    <asp:BoundField DataField="GroupByBillingFrequency" HeaderText="Billing Frequency" />
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="3%" />
                                                    <ItemStyle Width="3%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContType" HeaderText="Account Type" >
                                                    <ControlStyle Width="3%" />
                                                    <ItemStyle Width="3%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CustName" HeaderText="Customer Name" SortExpression="CustName">
                                                    <ControlStyle Width="16%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="16%" />
                                                  </asp:BoundField>
                                                 
                                                    <asp:BoundField HeaderText="Service By" DataField="Support" >
                                                 
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GroupContractGroup" HeaderText="Contract Group" />
                                                 
                                                  <asp:BoundField DataField="ServiceFrom" HeaderText="Service From" DataFormatString="{0:dd/MM/yyyy}" >
                                                    <ControlStyle Width="2%" />
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ServiceTo" HeaderText="Service To" DataFormatString="{0:dd/MM/yyyy}" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillAmount" HeaderText="Amount" >
                                                    <ControlStyle Width="5%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="5%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DiscountAmount" HeaderText="Disc. Amt" >
                                                    <ControlStyle Width="3%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="3%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AmountWithDiscount" HeaderText="Amount" >
                                                    <ControlStyle Width="5%" />
                                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GSTAmount" HeaderText="GST Amt." >
                                                    <ControlStyle Width="3%" />
                                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NetAmount" HeaderText="Net Amt." >
                                                    <ControlStyle Width="5%" />
                                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RecurringInvoice" HeaderText="Recur" >
                                                    <ControlStyle Width="2%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NextInvoiceDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Next Inv. Date" >
                                                    <ControlStyle Width="3%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="3%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RecurringScheduled" HeaderText="Schld">
                                                    <ControlStyle Width="2%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="LastModifiedOn" HeaderText="Edited On" SortExpression="LastModifiedOn" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                    </asp:BoundField>
                                                         <asp:TemplateField ><ItemTemplate> <asp:Button ID="btnEditHistory" runat="server"  Text="History" OnClick="btnEditHistory_Click" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top"   Width="80px"   />
                                               </ItemTemplate></asp:TemplateField>
                                              </Columns>
                                               <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                                            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" />
                                            <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
                                            <SelectedRowStyle BackColor="#AEE4FF" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                      </asp:GridView>


                                              </asp:Panel>
                                  </td>
                              </tr>
             
                 <tr>
                 <td>
                      <asp:SqlDataSource ID="SQLDSInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                </asp:SqlDataSource>
                     </td>
                     
                     <td>
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
                     <asp:SqlDataSource ID="SqlDSContractNo" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
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
                                      
                                         <asp:TextBox ID="txtInvoiceSearch" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtCustomerSearch" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtImportService" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtShowRecords" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                     <asp:TextBox ID="txtShowRecordsSort" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtLastSalesDetailRcno" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                    <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>
                                             <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox>
                                                         <asp:TextBox ID="txtDefaultTaxCode" runat="server" visible="false" ></asp:TextBox>
       
                                       <asp:TextBox ID="txtDeSelectContractGroup" runat="server" Visible="False"></asp:TextBox> 
                              </tr>
       
             </table>
      
        </ContentTemplate>
              </asp:UpdatePanel>

         
            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                  <ContentTemplate>  

          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">

          <tr style="width:100%">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">INVOICE GENERATION INFORMATION
                 </td>
           </tr>
         
                   
              
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtRcnoServiceRecord" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="1%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Consolidated Invoice No.<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtInvoiceNo" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="20" Width="80%" BackColor="#CCCCCC"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
              </tr>
              
               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtRcnoServiceRecordDetail" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="101"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Batch Date<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtBatchDate" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="21" BackColor="#CCCCCC" ></asp:TextBox>
            
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                </tr>

              <tr style="display:none">
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Invoice Date<asp:Label ID="Label45" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtInvoiceDate" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" TabIndex="21" Width="80%" OnTextChanged="txtInvoiceDate_TextChanged"></asp:TextBox>
                       <asp:CalendarExtender ID="txtInvoiceDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDate" TargetControlID="txtInvoiceDate" />
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

              

            

               <tr>
                   <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">&nbsp;</td>
                   <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 10%; text-align: right;">Billing Period</td>
                   <td style="width: 20%; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">
                       <asp:TextBox ID="txtBillingPeriod" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" TabIndex="109" Width="81%"></asp:TextBox>
                   </td>
                   <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">
                       <asp:TextBox ID="txtTaxRatePct" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="#003366" Height="16px" Width="15%" Enabled="False" visible="false"></asp:TextBox>
                   </td>
              </tr>

              

            

                <tr>
                    <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">&nbsp;</td>
                    <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 10%; text-align: right;">
                        <asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label>
                        <asp:Label ID="Label48" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td style="width: 20%; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">
                        <asp:DropDownList ID="txtLocation" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="80%">
                            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;"></td>
                </tr>

              

            

          <%--     <tr style="display:none">
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                &nbsp;</td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              
                                  <asp:TextBox ID="txtInvoiceAmount" runat="server" Text="0.00" style="text-align:right" Height="18px" Width="81%" AutoCompleteType="Disabled" BackColor="#CCCCCC" Rows="119" BorderStyle="None" ></asp:TextBox>
                       
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                   </tr>
                           <tr style="display:none">
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                            <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                <asp:TextBox ID="txtDiscountAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="120" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                          <tr style="display:none">
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtAmountWithDiscount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="120" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                          <tr style="display:none">
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:SqlDataSource ID="SqlDSServices" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select A.Rcno,  A.RecordNo, A.AccountID, A.ContractNo, A.CustName,   A.BillNo,  A.CompanyGroup, A.LocationID , A.BillAmount, A.ServiceDate, A.ContractGroup, A.Status, A.ServiceBy, A.Address1,  A.Notes, A.Location
 FROM tblServiceRecord A  where 1 = 1  

"></asp:SqlDataSource>
                              </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtGSTAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="121" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                          <tr style="display:none">
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                           <td style="font-size:15px;font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtNetAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="121" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                          <tr>
                              <tr">
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                               <asp:TextBox ID="txtComments" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px" Height="12px" TabIndex="27" Visible="False" Width="79.5%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr">
                       </tr> --%>

</Table> 

            </ContentTemplate>
              </asp:UpdatePanel>

<asp:CollapsiblePanelExtender ID="cpnl2" runat="server" CollapseControlID="Panel4" TargetControlID="Panel5" CollapsedImage="~/Images/plus.png" ExpandedImage="~/Images/minus.png" ImageControlID="imgCollapsible2" Enabled="True" ExpandControlID="Panel4" CollapsedText="Click to show" Collapsed="false"></asp:CollapsiblePanelExtender>
  
                <asp:Panel ID="Panel4" runat="server" >

         <table border="1" style="width:95%; border:solid; border-color:ButtonFace;">
              <tr style="width:100%">
                 <td colspan="11" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:0%; color:#800000; background-color: #C0C0C0;"> <asp:ImageButton ID ="imgCollapsible2" runat="server" ImageAlign="Bottom"  ImageUrl="~/Images/plus.png" /> &nbsp; SEARCH RECORD CRITERIA</td>
                  </tr>
             </table>
             </asp:Panel>

     <asp:Panel ID="Panel5" runat="server" >

                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate>      
        <div style="text-align:center">
        <table border="0"  style="width:87%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; border:solid; border-color:ButtonFace; "  >
            
                     <tr>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                <asp:Button ID="btnDummyClient" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                <asp:Label ID="lblBranch2" runat="server" Text="Select Branch"></asp:Label>
                            </td>

                          <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              
                              <cc1:DropDownCheckBoxes ID="ddlLocationSearch" runat="server" AddJQueryReference="true" style="top: 0px; left: 0px" UseButtons="false" UseSelectAllNode="true" Width="95%">
                                  <style2 DropDownBoxBoxHeight="120" DropDownBoxBoxWidth="85.5%" SelectBoxWidth="85.5%" />
                              </cc1:DropDownCheckBoxes>
                            </td>
                           <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"> Invoice From</td>
                            <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                       <asp:TextBox ID="txtDateFrom" runat="server" Height="16px" Width="90%" AutoCompleteType="Disabled" ></asp:TextBox>
                              <asp:CalendarExtender ID="calConDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom" Enabled="True" />
                                 
                            </td>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"> Invoice To</td>
                            <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                 <asp:TextBox ID="txtDateTo" runat="server" Height="16px" Width="90%" AutoCompleteType="Disabled" ></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDateTo" TargetControlID="txtDateTo" Enabled="True" />
                                 
                            </td> 
                          <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    <asp:Button ID="btnShowRecords" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SHOW INVOICES" Width="80%" />
                                </td>
                         </tr>

             <tr>
                        
                          <td style="width:9%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"> Account Type </td>
                    <td style="width:9%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                                  <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="95%">
                                     <asp:ListItem Selected="True">--SELECT--</asp:ListItem>
                                        <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                                      <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                  </asp:DropDownList>
                        
                               
                                  </td>

                          <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"> Account Id
                        
                               
                            </td>
                     <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtAccountId" runat="server" Height="16px" Width="75%" AutoCompleteType="Disabled" AutoPostBack="True" ></asp:TextBox>
                              <asp:ImageButton runat="server"  ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" CssClass="righttextbox" Height="22px" Width="24px" ID="btnClient"></asp:ImageButton>
                                  <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient" BackgroundCssClass="modalBackground" Enabled="True"  ></asp:ModalPopupExtender> 
                                  </td>
                           <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"> Customer Name
                        
                               
                            </td>
          <td colspan="2" style="width:15%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtClientName" runat="server" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>
                            </td>
                        </tr>
           
            </table> 
            </div>
                  <%--        <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;" colspan="4"> Display Services&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                               <asp:RadioButton ID="rdbCompleted" runat="server" GroupName="ServiceStatus"  Height="20px"  Text="Completed" AutoPostBack="True" Checked="True" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       
                               
                            &nbsp;&nbsp;

                               
                               <asp:RadioButton ID="rdbNotCompleted" runat="server" GroupName="ServiceStatus" Height="20px"  Text="Not Completed" AutoPostBack="True" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                               <asp:RadioButton ID="rdbAll" runat="server" GroupName="ServiceStatus" Height="20px"  Text="All" AutoPostBack="True" />

                               
                            </td>
                                             
                         
                          
                      

            
                    
                        <tr>
                            <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                <asp:DropDownList ID="ddlCompanyGrp" runat="server" Width="90%" Height="25px"    
                                         DataTextField="companygroup" DataValueField="companygroup" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>
                                 
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
                          
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> Billing Frequency<asp:Label ID="Label49" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                          </td>
                          <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Contract Group<asp:Label ID="Label47" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                          </td>
                        </tr>
                                           



               <tr>
                               <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                                   <asp:TextBox ID="ddlContractNo" runat="server" Height="16px" MaxLength="25" Style="vertical-align:top" Width="80%"></asp:TextBox>
                                   <asp:ImageButton ID="ImageButton5" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                                      
                            </td>
                            
                          
                           <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                <asp:DropDownList ID="ddlServiceFrequency" runat="server" Width="95%" Height="25px"    
                                         DataTextField="Frequency" DataValueField="Frequency" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSServiceFrequency">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>
                                 
                            </td>
                          
                        
                          
                             
                          
                                            
                            <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                           
                                <asp:DropDownList ID="ddlBillingFrequency" runat="server" Width="90%" Height="25px"    
                                         DataTextField="Frequency" DataValueField="Frequency" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSBillingFrequency">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>
                           

                                   </td>

                          <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                         
                                   <asp:DropDownList ID="ddlContractGroup" runat="server" Width="80%" Height="25px"    
                                         DataTextField="UPPER(contractgroup)" DataValueField="UPPER(contractgroup)" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSContractGroup">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>

                            

                                   </td>
                        </tr>
            
                   <tr>
                              <td colspan="1" style="width:10%;text-align:left;color:black;font-size:14px;font-weight:bold;font-family:'Calibri';">
                                    Service By</td>
                                <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    Scheduler</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:CheckBox ID="chkIncludeZeroValueServices" runat="server" Text="Include Zero Value Services" />
                              </td>
                               
                          <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                <asp:Button ID="btnDummyLocation" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                <asp:Button ID="btnDummyStatusSearch" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                <asp:TextBox ID="txtAccountIdSelection" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="20%"></asp:TextBox>
                              </td>

                          <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                           
                               &nbsp;</td>
                              
                        </tr>
                            <tr>
                                <td colspan="1" style="width:10%;text-align:left;color:black;font-size:14px;font-weight:bold;font-family:'Calibri'; vertical-align:top">
                                    <asp:TextBox ID="txtServiceBySearch" runat="server" Height="16px" MaxLength="25" Width="80%" Style="vertical-align:top" ></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                </td>
                                <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;vertical-align:top;">
                                   <asp:DropDownList ID="ddlScheduler" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="20px" Width="95%"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList> </td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:CheckBox ID="chkIncludeServicesWithCN" runat="server" Text="Include Services with Credit Note" />
                        
                                                                </td>
                                <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    <asp:RadioButtonList ID="rdbGrouping" runat="server" CausesValidation="True" CellPadding="4" CellSpacing="2" Font-Size="12px" Height="15px" RepeatDirection="Horizontal" Width="99%">
                                        <asp:ListItem Selected="True" Value="AccountID">Group by Account ID</asp:ListItem>
                                        <asp:ListItem Value="LocationID">Group by Service Location ID</asp:ListItem>
                                        <asp:ListItem Value="ServiceLocationCode">Group by Service Location Sub-Group</asp:ListItem>
                                          <asp:ListItem Value="ContractNo">Group by Contract No.</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                              --%>
       </ContentTemplate>
              </asp:UpdatePanel>

         </asp:Panel>
            
<%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="95%" Height="400px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">--%>
          <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="500px" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1330px">
          
         <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:0%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label43" runat="server" Text="INVOICE DETAILS"></asp:Label>
&nbsp;</td></tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100% ">
                
        <asp:UpdatePanel ID="updpnlServiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="grvInvoiceRecDetails" runat="server" AllowSorting="True"
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="34px"  Font-Size="12px"
            ShowFooter="True" Style="text-align: left" Width="100%">
                
                <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false" TextAlign="Right" onchange="checkallservicerecs()"  Width="5%" ></asp:CheckBox></HeaderTemplate>
                    
              <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false"   onchange="checkoneservicerec()"  ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
                <asp:TemplateField HeaderText="Location Id" SortExpression="LocationId"><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Text='<%# Bind("LocationID")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="82px"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
              <asp:TemplateField HeaderText="CustName" SortExpression="CustName"><ItemTemplate><asp:TextBox ID="txtClientNameGV" runat="server" Text='<%# Bind("CustName")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="125px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Cont. Grp."><ItemTemplate><asp:TextBox ID="txtDeptGV" runat="server"  Text='<%# Bind("ContractGroup")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="50px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Text='<%# Bind("ContractNo")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="135px"></asp:TextBox></ItemTemplate></asp:TemplateField>                             
              <asp:TemplateField HeaderText="St."><ItemTemplate><asp:TextBox ID="txtStatusGV" runat="server" Text='<%# Bind("Status")%>' Style="text-align: center" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="20px"></asp:TextBox></ItemTemplate></asp:TemplateField>        
           
              <asp:TemplateField HeaderText="Service Record"><ItemTemplate><asp:TextBox ID="txtServiceRecordNoGV" runat="server" Text='<%# Bind("RecordNo")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="112px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Service Date" SortExpression="ServiceDate"><ItemTemplate><asp:TextBox ID="txtServiceDateGV" runat="server" Text='<%# Bind("ServiceDate")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="60px" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Text='<%# Bind("Address1")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="180px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                      
                    
              <asp:TemplateField HeaderText="Service By" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtServiceByGV" runat="server" Text='<%# Bind("ServiceBy")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" Visible="true" BorderStyle="None" Height="18px" Width="55px"></asp:TextBox></ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
              <asp:TemplateField HeaderText="Serv. Description" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtServiceIdGV" runat="server" Text='<%# Bind("Notes")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" Visible="true" BorderStyle="None" Height="18px" Width="84px"></asp:TextBox></ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
         
                              <asp:TemplateField HeaderText="Billing Freq." HeaderStyle-HorizontalAlign="Left"><ItemTemplate><asp:TextBox ID="txtBillingFrequencyGV" runat="server" Text='<%# Bind("BillingFrequency")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="75px"></asp:TextBox></ItemTemplate>
                       <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                       <%--  <asp:TemplateField HeaderText="Bill Amt."><ItemTemplate><asp:TextBox ID="txtToBillAmtGV" runat="server" Text='<%# Bind("BillAmount")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="50px" Align="right" OnTextChanged="txtToBillAmtGV_TextChanged"></asp:TextBox></ItemTemplate>
                             <HeaderStyle HorizontalAlign="Right" />
                    </asp:TemplateField>--%>
           
        
                      
                <asp:TemplateField HeaderText="Agree Val" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtAgreeValueGV" runat="server" Text='<%# Bind("AgreeValue")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:right" width="55px"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Dur." HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtDurationGV" runat="server" Text='<%# Bind("Duration")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:center" Width="30px"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Dur. MS" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtDurationMSGV" runat="server" Text='<%# Bind("DurationMS")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="40px"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Contract Val. Per Service" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtPerServiceValueGV" runat="server" Text='<%# Bind("PerServiceValue")%>' Visible="true" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" style="text-align:right" BorderStyle="None" Height="18px"  Width="50px"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
             
               <asp:TemplateField HeaderText="Service Remarks" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtServiceRemarksGV" runat="server" Text='<%# Bind("Remarks")%>'  Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" Visible="true" BorderStyle="None" Height="18px" Width="250px"></asp:TextBox></ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
            
               <asp:TemplateField HeaderText="Invoice No." HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtInvoiceNoGV" runat="server" Text='<%# Bind("BillNo")%>'  Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" Visible="true" BorderStyle="None" Height="18px" Width="110px"></asp:TextBox></ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
          
                   <%--    <asp:TemplateField><ItemTemplate>
                    <asp:Button ID="btnViewInvoice" runat="server" Visible="true" Text="V" Font-Size="10px" Width="20px"  OnClick="btnViewInvoice_Click" />
                   </ItemTemplate>
                   </asp:TemplateField>   --%>

            
           
                 

            <asp:TemplateField HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" Text='<%# Bind("AccountId")%>' Visible="false" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="0px"></asp:TextBox></ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
            <asp:TemplateField ><ItemTemplate><asp:TextBox ID="txtServiceLocationGroupGV" runat="server" Visible="false" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                       <asp:TemplateField ><ItemTemplate><asp:TextBox ID="txtCompanyGroupGV" runat="server" Text='<%# Bind("CompanyGroup")%>' Visible="true" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None"  Height="18px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
         
                      <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContactTypeGV" runat="server" Text='<%# Bind("ContactType")%>' Visible="false" ReadOnly="true" Font-Size="11px"  BackColor="#CCCCCC" BorderStyle="None"  Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
         
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServicebillingdetailGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordGV" runat="server" Text='<%# Bind("Rcno")%>' Visible="false" Height="15px" Font-Size="11px" ReadOnly="true" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                           
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtAccountNameGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtOurReferenceGV" runat="server" Text='<%# Bind("OurRef")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtYourReferenceGV" runat="server" Text='<%# Bind("YourRef")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtPoNoGV" runat="server" Text='<%# Bind("PONo")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtCreditTermsGV" runat="server" Text='<%# Bind("BillAmount")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtSalesmanGV" runat="server"  Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRecordTypeGV" runat="server" Text='<%# Bind("RecordType")%>' Visible="false"  Height="15px" Font-Size="11px" ReadOnly="true" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtLocationGV" runat="server" Text='<%# Bind("Location")%>' Visible="false"  Height="15px" Font-Size="11px" ReadOnly="true" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
               
                     <asp:BoundField DataField="Location" />
              
               
                     </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
            </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvInvoiceRecDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr>
             
              <tr>
                  <td  style="width:80%;font-size:13px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdateSelect" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="btnSelect" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                            
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                  
            </tr>

               
             <caption>
                 <br />
             </caption>
        </table>
         </asp:Panel>
 <br />
         <table border="0" style="width:80%; margin:auto; ">
             <tr style="width:100%">
                   <td  style="width:80%;font-size:13px;font-weight:bold;font-family:'Calibri';color:black;text-align:right">
                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional"><ContentTemplate>
                          Total Amount <asp:TextBox ID="txtTotalSelected" runat="server" AutoCompleteType="Disabled" Height="16px" Width="10%" style="text-align:right"  ></asp:TextBox>
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                 </tr>
              </table>

         <asp:CollapsiblePanelExtender ID="cpnl1" runat="server" CollapseControlID="Panel2" Collapsed="false" SuppressPostBack="false" ImageControlID="imgCollapsible1" TargetControlID="Panel3" CollapsedImage="~/Images/plus.png" ExpandedImage="~/Images/minus.png" Enabled="True" ExpandControlID="Panel2" CollapsedText="Click to show"></asp:CollapsiblePanelExtender>
  
                <asp:Panel ID="Panel2" runat="server" >

         <table border="1" style="width:90%; margin:auto; border:solid; border-color:ButtonFace; display:none">
              <tr style="width:95%">
                 <td colspan="4" style="text-align:left; padding-left:0%; color:#800000; background-color: #C0C0C0;">
              <asp:ImageButton ID ="imgCollapsible1" runat="server" ImageAlign="Bottom"  ImageUrl="~/Images/plus.png" /> &nbsp;       <asp:Label ID="Label41" runat="server" style="font-size:15px;font-weight:bold;font-family:Calibri; text-decoration: underline; " Text="SERVICE DETAILS"></asp:Label>

                 </td></tr>
             </table>
             </asp:Panel>

         
  <asp:Panel ID="Panel3" runat="server" >      

          <asp:UpdatePanel runat="server" ID="updPnlBillingRecs" UpdateMode="Conditional">
                  <ContentTemplate>

  
       
          <%--             <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                  
           

           
   <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Account Type</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtAccountType" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Width="81%" TabIndex="111" BorderStyle="None"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtRcnoInvoice" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White" TabIndex="102"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Account ID
                            </td>
                            
                       <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                               <asp:TextBox ID="txtAccountIdBilling" runat="server" Height="18px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="112" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
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
                            <asp:TextBox ID="txtAccountName" runat="server" Height="18px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="113" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Company Group</td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtCompanyGroup" runat="server" Height="16px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="110" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                        </tr>
               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Billing Address
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtBillAddress" runat="server" Height="18px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="114" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtRowSelected" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" ForeColor="White" TabIndex="130"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBillStreet" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="81%" TabIndex="115" BorderStyle="None"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                      <asp:TextBox ID="txtBatchNo" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" TabIndex="104" ForeColor="White"></asp:TextBox>
                  </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                  <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtBillBuilding" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="81%" TabIndex="116" BorderStyle="None"></asp:TextBox>
                  </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
              <tr>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                      <asp:TextBox ID="txtRcnoServiceBillingDetail" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" ForeColor="White" TabIndex="105"></asp:TextBox>
                  </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Country</td>
                  <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtBillCountry" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="81%" TabIndex="117" BorderStyle="None"></asp:TextBox>
                  </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtServiceName" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="106"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Postal
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtBillPostal" runat="server" Height="18px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="118" BorderStyle="None" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

             
                        

</table>--%>

  <%--        <table border="0"  style="width:97%; margin:auto ; margin-left: 0%; margin-right:0%;  ">

                      


                  <tr style="width:100%">
                     <td colspan="10" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
               <asp:UpdatePanel ID="updpnlBillingDetails" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:GridView ID="grvBillingDetails"  runat="server" AllowSorting="True" 
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px" 
            ShowFooter="True" Style="text-align: left" Width="99%"><Columns>
                                            
                <asp:TemplateField HeaderText=" Item Type" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:DropDownList ID="txtItemTypeGV" runat="server" Font-Size="12px" Height="20px" ReadOnly="true" Width="90px"  AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem Text="SERVICE" Value="SERVICE" /><asp:ListItem Text="PRODUCT" Value="PRODUCT" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Item Code"><ItemTemplate><asp:DropDownList ID="txtItemCodeGV" runat="server" Font-Size="12px"  Height="20px"  Width="100px" AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemCodeGV_SelectedIndexChanged"> <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Item Description"><ItemTemplate><asp:TextBox ID="txtItemDescriptionGV" runat="server" Font-Size="12px" Height="15px"  Width="190px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="GL Code"><ItemTemplate><asp:TextBox ID="txtOtherCodeGV" runat="server" Font-Size="12px" Visible="true" Enabled="false" Height="15px" Width="50px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="UOM"><ItemTemplate><asp:DropDownList ID="txtUOMGV" runat="server" Font-Size="12px" Height="20px" Width="70px" AppendDataBoundItems="True"> </asp:DropDownList></ItemTemplate></asp:TemplateField>
             
                <asp:TemplateField HeaderText="Qty."><ItemTemplate><asp:TextBox ID="txtQtyGV" runat="server" Font-Size="12px" style="text-align:right" Height="15px"  Width="40px" AutoPostBack="true" OnTextChanged="txtQtyGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Price Per UOM"><ItemTemplate><asp:TextBox ID="txtPricePerUOMGV" runat="server" Font-Size="12px" style="text-align:right" Height="15px"  Width="60px" AutoPostBack="true" OnTextChanged="txtPricePerUOMGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Total Price"><ItemTemplate><asp:TextBox ID="txtTotalPriceGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Disc Perc"><ItemTemplate><asp:TextBox ID="txtDiscPercGV" runat="server"  Font-Size="12px" Text="0.00" style="text-align:right" Height="15px"  Width="40px" AutoPostBack="true" OnTextChanged="txtDiscPercGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
       
                <asp:TemplateField HeaderText="Disc Amount"><ItemTemplate><asp:TextBox ID="txtDiscAmountGV" runat="server" Font-Size="12px" Text="0.00" style="text-align:right" Height="15px"  Width="60px" AutoPostBack="true" OnTextChanged="txtDiscAmountGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="Price With Disc"><ItemTemplate><asp:TextBox ID="txtPriceWithDiscGV" runat="server" Font-Size="12px" Text="0.00"  Enabled="false" style="text-align:right" Height="15px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="Tax Code"><ItemTemplate><asp:DropDownList ID="txtTaxTypeGV" runat="server" Font-Size="12px" style="text-align:right" Height="20px" Width="50px" AutoPostBack="True" onselectedindexchanged="txtTaxTypeGV_SelectedIndexChanged"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="GST%"><ItemTemplate><asp:TextBox ID="txtGSTPercGV" runat="server"  Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="40px"></asp:TextBox></ItemTemplate></asp:TemplateField>

               <asp:TemplateField HeaderText="GST Amt."><ItemTemplate><asp:TextBox ID="txtGSTAmtGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="50px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="Net Price"><ItemTemplate><asp:TextBox ID="txtTotalPriceWithGSTGV" runat="server" Font-Size="12px" Enabled="false" style="text-align:right" Height="15px" Width="80px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
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

                            </table>--%>


                      <asp:UpdatePanel ID="updPanelSave" runat="server" UpdateMode="Conditional"><ContentTemplate>

                      <table border="0" style="width:97%; margin:auto" >
                          <tr>
              
                               <td colspan="1"  style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;">
                                
                                 </td>
                               <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                               <td colspan="1" style="width:190px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                              <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                              <td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             Total
                                 </td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  
                                  &nbsp;</td>
                              <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    <asp:TextBox ID="txtTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                               </td>
                                  <td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                      &nbsp;</td>
                                 
                             <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:TextBox ID="txtTotalDiscAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                             
                                 </td>

                              <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  <asp:TextBox ID="txtTotalWithDiscAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                               </td>
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                               <td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                   &nbsp;</td>
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    <asp:TextBox ID="txtTotalGSTAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                             
                                 </td>
                                <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                     <asp:TextBox ID="txtTotalWithGST" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="99%"></asp:TextBox>
                               </td>
                               <td colspan="1" style="width:200px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                        </tr>
                        
                           
                         
                          
                            <tr>
              
                               <td colspan="1"  style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;">
                                
                                 </td>
                               <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                               <td colspan="1" style="width:230px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                              <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                              <td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                            
                                 </td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  
                                 </td>
                              <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>
                                  <td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                 </td>
                                 
                             <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                 </td>

                              <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                   </td>
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                               <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                                <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                 </td>
                                <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                    &nbsp;</td>
                               <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                
                                   <asp:Button ID="btnSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidation()" Text="SAVE" Width="85%" />
                                
                                 </td>
                        </tr>
                        
            </table>
                         
                      </ContentTemplate>


                      </asp:UpdatePanel>
                </ContentTemplate>
              </asp:UpdatePanel>
         
                      </asp:Panel>   
              

                  
          <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                  <ContentTemplate>   


          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                          
     


                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
                               <asp:GridView ID="grvGL" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " GridLines="None" Height="12px" ShowFooter="True" Style="text-align: left" Width="95%">
                                   <Columns>
                                       <asp:TemplateField HeaderText="GL Code">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txtGLCodeGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" BackColor="#CCCCCC" Font-Size="12px" Height="15px" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged" ReadOnly="true" Width="80px"></asp:TextBox>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Description">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txtGLDescriptionGV" runat="server" BackColor="#CCCCCC" Font-Size="12px" Height="15px" ReadOnly="true" Visible="true" Width="250px"></asp:TextBox>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Debit Amount">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txtDebitAmountGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" BackColor="#CCCCCC" Font-Size="12px" Height="15px" onselectedindexchanged="txtItemCodeGV_SelectedIndexChanged" ReadOnly="true" style="text-align:right" Width="80px"></asp:TextBox>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Credit Amount">
                                           <ItemTemplate>
                                               <asp:TextBox ID="txtCreditAmountGV" runat="server" BackColor="#CCCCCC" Font-Size="12px" Height="15px" ReadOnly="true" style="text-align:right" Width="80px"></asp:TextBox>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True" Visible="false">
                                       <FooterStyle VerticalAlign="Top" />
                                       <ItemStyle Height="15px" />
                                       </asp:CommandField>
                                       <asp:TemplateField>
                                           <FooterStyle HorizontalAlign="Left" />
                                           <FooterTemplate>
                                               <asp:Button ID="btnAddDetail" runat="server" Text="Add New Row" ValidationGroup="VGroup" Visible="false" />
                                           </FooterTemplate>
                                           <ItemStyle ForeColor="#507CD1" />
                                       </asp:TemplateField>
                                   </Columns>
                                   <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" />
                                   <RowStyle BackColor="#EFF3FB" Height="17px" />
                                   <EditRowStyle BackColor="#2461BF" />
                                   <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                   <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                   <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                                   <AlternatingRowStyle BackColor="White" />
                               </asp:GridView>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"></td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:TextBox ID="txtARCode" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                               <asp:TextBox ID="txtARDescription" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                           </td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtGSTOutputCode" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:TextBox ID="txtGSTOutputDescription" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               &nbsp;</td>
                           <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               <asp:Button ID="btnDeleteUnselected" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" CommandName="DELETE" Text="DELETE UNSELECTED" Width="31%" />
                               <asp:Button ID="btnGenerateInvoice" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidation(); btn_disable(); " TabIndex="28" Text="GENERATE INVOICE" Width="30%" />
                               <asp:Button ID="btnSaveInvoice" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  OnClientClick="return preventMultipleSubmissions(); "    TabIndex="28" Text="SAVE SCHEDULE" Width="30%" />
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;  ">
                               <asp:Button ID="btnCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="70%" />
                           </td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; "></td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"></td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:TextBox ID="txtRecordNo" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" TabIndex="107" Width="1%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtOurReference" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="22" Visible="False" Width="10%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:TextBox ID="txtPostStatus" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" TabIndex="131" Width="1%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtYourReference" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="23" Visible="False" Width="10%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtPONo" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="24" Visible="False" Width="10%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; "></td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:DropDownList ID="ddlCreditTerms" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select" DataSourceID="SqlDSTerms" DataTextField="UPPER(Terms)" DataValueField="UPPER(Terms)" Height="25px" TabIndex="25" Visible="False" Width="10%">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                               </asp:DropDownList>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtCreditDays" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="23" Visible="False" Width="10%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; "></td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:DropDownList ID="ddlSalesmanBilling" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSSalesMan" DataTextField="StaffId" DataValueField="StaffId" Height="25px" TabIndex="26" Visible="False" Width="10%">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                               </asp:DropDownList>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtPreviousBillSchedule" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Visible="False" Width="10%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:CheckBox ID="chkRecurringInvoice" runat="server" onchange="EnableDisableRecurringInvoice()" Visible="False" />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <asp:CheckBox ID="chkRecurringScheduled" runat="server" Enabled="False" onchange="EnableDisableRecurringInvoice()" Visible="False" />
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:DropDownList ID="ddlRecurringFrequency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Value="-1" Visible="False" Width="10%">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                                   <asp:ListItem>DAILY</asp:ListItem>
                                   <asp:ListItem>WEEKLY</asp:ListItem>
                                   <asp:ListItem>MONTHLY</asp:ListItem>
                                   <asp:ListItem>BI-MONTHLY</asp:ListItem>
                                   <asp:ListItem>QUARTERLY</asp:ListItem>
                                   <asp:ListItem>HALF-ANNUALLY</asp:ListItem>
                                   <asp:ListItem>ANNUALLY</asp:ListItem>
                               </asp:DropDownList>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtRecurringInvoiceDate" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="21" Visible="False" Width="10%"></asp:TextBox>
                               <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtRecurringInvoiceDate" TargetControlID="txtRecurringInvoiceDate" />
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtRecurringServiceDateFrom" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="21" Visible="False" Width="10%"></asp:TextBox>
                               <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtRecurringServiceDateFrom" TargetControlID="txtRecurringServiceDateFrom" />
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtRecurringServiceDateTo" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="21" Visible="False" Width="10%"></asp:TextBox>
                               <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtRecurringServiceDateTo" TargetControlID="txtRecurringServiceDateTo" />
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr>
                       </tr>

               



              </table>
          </ContentTemplate>
              </asp:UpdatePanel>
         
                   <%--           
        

          <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
             <tr style="text-align:center;width:100%">
                <td colspan="8" style="text-align:left;padding-left:5px;">
                    test
                       dth:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      er" BorderStyle="None" ForeColor="#003366"></asp:TextBox></td>
                                  <td colspan ="2" style="text-align:left; padding-right:2%">
                                     
                                      </td>

                                 
                                 
                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"> 
                                  <asp:TextBox ID="txtIsPopup" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>
                                   </td>
                                  <td style="text-align:left;width:8%">
                                      <asp:Button ID="btnDummy" runat="server" CssClass="roundbutton" Font-Bold="True" Text=" " Width="24px" BackColor="White" BorderStyle="None" />
                                      &nbsp;&nbsp;&nbsp;<asp:Button ID="btnDummyT" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;<asp:Button ID="btnDummyC" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;</td>

                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
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

  
    
         
  <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="98%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center" ScrollBars="Horizontal">
  <table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
    </td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160;
         <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
    <asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Width="24px" Visible="False"></asp:ImageButton>
    <asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" OnClick="btnPopUpClientReset_Click" Width="24px"></asp:ImageButton>


</td><td>
                <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>

            </td></tr></table><div style="text-align:center; padding-left: 10px; padding-bottom: 5px;"><div class="AlphabetPager">
                                        <asp:Repeater ID="rptClientAlphabets" runat="server" Visible="False"><ItemTemplate>
        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>' OnClick="ClientAlphabet_Click" ForeColor="Black" />
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
        <asp:BoundField DataField="LocationID" HeaderText="Location ID" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
        <ControlStyle Width="8%" />

        <HeaderStyle Width="100px" HorizontalAlign="Left" />

        <ItemStyle Width="10%" Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="BillingNameSvc" HeaderText="Client Name" SortExpression="Name">
        <ControlStyle Width="35%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="ServiceName" HeaderText="Service Name" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="BillContact1Svc" HeaderText="Contact Person" SortExpression="BillContact1Svc">

        <HeaderStyle Wrap="False" HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="Address1" HeaderText="Service Address" >

            <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" HorizontalAlign="Left" />
        </asp:BoundField>
            <asp:BoundField DataField="BillAddressSvc" HeaderText="Bill Address">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
            </asp:BoundField>
        <asp:BoundField DataField="Telephone" HeaderText="Telephone" >

            <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="Mobile" HeaderText="Mobile" >

            <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
            <ItemStyle Wrap="False" />
        </asp:BoundField>
            <asp:BoundField DataField="ARTermSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="SalesmanSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillBuildingSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillStreetSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillCitySvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillStateSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" />
            </asp:BoundField>
            <asp:BoundField DataField="BillCountrySvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillPostalSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
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
            <asp:BoundField DataField="CompanyGroupD">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" />
            </asp:BoundField>
        </Columns>

        <EditRowStyle BackColor="#999999" />

        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />

        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />

        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />

        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

        <SortedAscendingCellStyle BackColor="#E9E7E2" />

        <SortedAscendingHeaderStyle BackColor="#506C8C" />

        <SortedDescendingCellStyle BackColor="#FFFDF8" />

        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>



             <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters>
        <asp:ControlParameter Name="ContType" ControlID="ddlContactType" PropertyName="SelectedValue" Type="String" />
        </FilterParameters>
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

     
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
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
       
        <asp:Panel ID="pnlStaff" runat="server" BackColor="White" Width="45%" Height="80%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Staff</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpStaff" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMarkStaff(this, event);" onfocus = "WaterMarkStaff(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpStaffSearch" OnClick="btnPopUpStaffSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpStaffReset" OnClick="btnPopUpStaffReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
       
        <asp:TextBox ID="txtPopupStaffSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvStaff" runat="server" CssClass="Centered" DataSourceID="SqlDSStaffID" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="500px"  OnRowDataBound = "OnRowDataBoundgStaff" OnSelectedIndexChanged = "OnSelectedIndexChangedgStaff">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                     <asp:BoundField DataField="StaffId" HeaderText="StaffId" SortExpression="StaffId" ReadOnly="True">
                       <ControlStyle Width="80PX" />
                  <HeaderStyle Width="80px" Wrap="False" HorizontalAlign="Left" />
                  <ItemStyle Width="80px" Wrap="False" />
                </asp:BoundField>
                
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                        <ControlStyle Width="120px" />
                        <HeaderStyle Width="120px" HorizontalAlign="Left" />
                        <ItemStyle Width="120px" Wrap="True" />
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
           <asp:SqlDataSource ID="SqlDSStaffID" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId,Name FROM tblstaff where Status = 'O' ORDER BY STAFFID"></asp:SqlDataSource>
   
         
        </div>
        
           </asp:Panel>
     
     <asp:ModalPopupExtender ID="mdlPopupStaff" runat="server" CancelControlID="btnPnlStaffClose" PopupControlID="pnlStaff" TargetControlID="btndummystaff" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
       <asp:Button ID="btndummystaff" runat="server" cssclass="dummybutton" />
         

               <%--start--%>

    <%--        <asp:Panel ID="pnlConfirmSavePost" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label9" runat="server" Text="GENERATE Invoices "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label10" runat="server" Text="Do you want to GENERATE Invoices?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesSavePost" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmNoSavePost" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmSavePost" runat="server" CancelControlID="btnConfirmNoSavePost" PopupControlID="pnlConfirmSavePost" TargetControlID="btndummySavePost" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummySavePost" runat="server" CssClass="dummybutton" />
         <%--end--%>
          

                  <%-- Start:View Edit History--%>
             
              
      <asp:Panel ID="pnlViewEditHistory" runat="server" BackColor="White" Width="1000px" Height="85%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
                              
               <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">View Record History</h4> 
  </td> <td>
                               <asp:TextBox ID="TextBox5" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label27" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewEditHistory" runat="server" DataSourceID="sqlDSViewEditHistory" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
              
                <asp:BoundField DataField="LogDate" HeaderText="Date &amp; Time" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="18%" />
                </asp:BoundField>
                <asp:BoundField DataField="StaffID" HeaderText="Staff ID" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="Action" HeaderText="Action">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="Comments" HeaderText="Comments">
                <HeaderStyle HorizontalAlign="Left" />
               
                <ItemStyle Width="50%" />
               
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#0099CC" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

        </asp:GridView><br />
            
                  <asp:SqlDataSource ID="sqlDSViewEditHistory" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:40px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="Button8" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
                          

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewEditHistory" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlViewEditHistory" TargetControlID="btnDummyViewEditHistory" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewEditHistory" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Edit History--%>
         <%--            <asp:Panel ID="pnlConfirmPost" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
        <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEvent" runat="server" Text="GENERATE Invoices"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to GENERATE Invoices?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmPost" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmPost" TargetControlID="btndummyPost" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyPost" runat="server" CssClass="dummybutton" />
  
         <%--start--%>



   <%--                    <asp:Panel ID="pnlUpdateServiceRecord" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
        <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label14" runat="server" Text="Update Service Record "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label16" runat="server" Text="Do you wish to change the Bill Amount of the Service Record?"></asp:Label>
                        
                      </td>
                           </tr>


                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesUpdateServiceRecord" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmNoUpdateServiceRecord" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlUpdateServiceRecord" runat="server" TargetControlID="btnUpdateServiceRecord"  PopupControlID="pnlUpdateServiceRecord" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnUpdateServiceRecord" runat="server" CssClass="dummybutton" />




         <asp:Panel ID="pnlLockedServiceRecord" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label17" runat="server" Text="Service Record Locked"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label18" runat="server" Text="Service Record is Locked.. Cannot modify Bill Amount"></asp:Label>
                        
                      </td>
                           </tr>


                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>

                            <tr style="padding-top:40px;">
                           <td colspan="2" style="text-align:center">      <asp:Button ID="btnCancelLockedServiceRecord" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

      
                  
      <asp:ModalPopupExtender ID="mdlLockedServiceRecord" runat="server" TargetControlID="btnLockedServiceRecord"  CancelControlID="btnCancelLockedServiceRecord" PopupControlID="pnlLockedServiceRecord" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnLockedServiceRecord" runat="server" CssClass="dummybutton" />


         <%--03.04.24--%>


     <%--      <asp:Panel ID="pnlNoContractGroupSelected" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
        <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label3" runat="server" Text="No Contract Group Seleceted"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label4" runat="server" Text="Using the Batch Invoice without Contract Group  "></asp:Label>
                        
                      </td>
                           </tr>

              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label5" runat="server" Text="  specified will disregard the Tax Code configuration  "></asp:Label>
                        
                      </td>
                           </tr>


              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label6" runat="server" Text=" in the Contract Group master table and will use "></asp:Label>
                        
                      </td>
                           </tr>

              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label7" runat="server" Text="the default tax code for the Accounting Period."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>

                            <tr style="padding-top:40px;">
                           <td colspan="2" style="text-align:center">      <asp:Button ID="btnCancelNoContractGroupSelected" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

      
                  
      <asp:ModalPopupExtender ID="mdlNoContractGroupSelected" runat="server" TargetControlID="btnNoContractGroupSelected"  CancelControlID="btnCancelNoContractGroupSelected" PopupControlID="pnlNoContractGroupSelected" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnNoContractGroupSelected" runat="server" CssClass="dummybutton" />
         <%--03.04.24--%>


     <%--    <asp:Panel ID="pnlPopUpContractNo" runat="server" BackColor="White" Width="800" Height="600" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">
       <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Contract Number</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="btnPopUpContractNoClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

        <div class="wrp">
            <div class="frm">
                <table style="text-align: center;">
                    <tr>
                        <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right; width:30%;">Search</td>
                        <td style="text-align: left; width:30%">
                            <asp:TextBox ID="txtPopUpContractNo" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Width="98%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width:30%">
                             <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
                       </td>
                     
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvPopUpContractNo" runat="server" DataSourceID="SqlDSContractNo" ForeColor="#333333" 
                AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="80%" RowStyle-HorizontalAlign="Left" PageSize="12" Font-Size="14px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="ContractNo" HeaderText="Contract Number" SortExpression="ContractNo">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustName" HeaderText="Customer Name">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="14px" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div> 
    </asp:Panel> --%>

        <%--   <asp:ModalPopupExtender ID="mdlPopUpContractNo" runat="server" CancelControlID="btnPopUpContractNoClose" PopupControlID="pnlPopUpContractNo"
              TargetControlID="btnContractNo" BackgroundCssClass="modalBackground">
           </asp:ModalPopupExtender>
          <asp:Button ID="btnContractNo" runat="server" CssClass="dummybutton" /> --%>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

