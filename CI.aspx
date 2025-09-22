<%@ page title="Consolidated EInvoice" language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" CodeFile="CI.aspx.vb" inherits="CI" culture="en-GB" enableeventvalidation="false" %>


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
            $('#<%=btnSaveInvoice.ClientID%>').val('SAVING..WAIT');
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
           // alert("1");
            var input = table.rows[0].getElementsByTagName("input");

            if (input[0].id.indexOf("CheckBox1") > -1) {

                //start
                if ((input[0].checked) == false) {
                   // alert("1");
                    for (var i = 1; i < table.rows.length; i++) {

                        //get all the input elements
                        var input1 = table.rows[i].getElementsByTagName("input");

                        for (var j = 0; j < input1.length; j++) {
                          //  alert("1")
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
                       //     alert("2");
                            //get the textbox1
                            if (inputs[j].id.indexOf("txtToBillAmtGV") > -1) {
                              //  alert("3");
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
        // alert("11");
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

    function openInNewTab() {
        window.document.forms[0].target = '_blank';
        setTimeout(function () { window.document.forms[0].target = ''; }, 0);
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
   <%--    <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>   
    <asp:UpdatePanel ID="updPanelInvoice" runat="server" UpdateMode="Conditional">
          <ContentTemplate>  --%>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="ComboBox_Bundle"/>  
                <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>             
            </ControlBundles>
        </asp:ToolkitScriptManager>
     

    <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />
               
   
     <div style="text-align:center">
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">CONSOLIDATED INVOICE</h3>
          
    <%--      <asp:UpdatePanel ID="updPnlMsg" runat="server" UpdateMode="Conditional">
              <ContentTemplate> --%>


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
                       <asp:Button ID="btnDelete" runat="server" Font-Bold="True" Text="VOID" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick="currentdatetime();  Confirmvoid()" />
                    </td>
                     
                     
                   <td style="width:8%;text-align:center;">
                   
                          <asp:Button ID="btnPostEinvoice" runat="server" Font-Bold="True" Text="SUBMIT CONSOLIDATED INVOICE" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick="currentdatetime();" />
               
                    </td>
                   <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnPrint" runat="server" Font-Bold="True" Text="PRINT" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" VISIBLE="FALSE" />
                    </td>
                  <td>
                   
                      <asp:Button ID="btnEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT"  Visible="False" Width="95%" />
               
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
    <%--   </ContentTemplate>
              </asp:UpdatePanel> --%>
           </div>

     <%--     <asp:UpdatePanel ID="updPnlSearch" runat="server" UpdateMode="Conditional"><ContentTemplate> --%>

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
                                  <asp:TextBox ID="txtUUIDSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left; " Width="75%"></asp:TextBox>
                                </td>

                             
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8%; ">
                                    Consolidated Invoice Date</td>
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
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8%; ">
                                    Post Status</td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%;padding-right:10px ">
                                  <asp:TextBox ID="txtSearch1Status" runat="server" Width="60%" ReadOnly="FALSE"></asp:TextBox>
                                 <asp:ImageButton ID="btnSearch1Status" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" ImageAlign="Top"
                                    Height="22px" Width="24px" />  
                                 
                                  <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" CancelControlID="btnStatusCancel" PopupControlID="pnlStatusSearch" TargetControlID="btnSearch1Status" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
                     
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


     

    <br />
         <table  style="width:97%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
         
         <%--     <tr><td style="padding-left:100px;width:100px"><asp:Label ID="Label38" runat="server" Text="E-Invoice Status" CssClass="CellFormat"></asp:Label></td>
         <td style="background-color:Yellow;width:40px;height:10px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:140px">Pending Submission</td>
   
            <td style="background-color:Pink;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:110px">Submitted</td>

   
          <td style="background-color:yellowgreen;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:90px;">Valid</td>
   
       
          <td style="background-color:Red;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:90px">Invalid</td>

         <td style="background-color:purple;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:90px">Cancelled</td>
                  </tr>
             <tr>
               <td colspan="11" style="text-align:right">
                       <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                    
                 </td>
    </tr>--%>
             
             <tr>
                 <td colspan="10" style="text-align:left;width:100%">
                     <table border="0" style="width:70%;padding-left:100px;text-align:left">
    <tr><td style="padding-left:100px;width:100px"><asp:Label ID="Label38" runat="server" Text="E-Invoice Status" CssClass="CellFormat"></asp:Label></td>
         <td style="background-color:Yellow;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:140px">Pending Submission</td>
   
            <td style="background-color:Pink;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:110px">Submitted</td>

   
          <td style="background-color:yellowgreen;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:90px;">Valid</td>

                     <td style="background-color:Red;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:90px">Invalid</td>

         <td style="background-color:purple;width:40px;height:20px">&nbsp;</td>
        <td class="CellFormat" style="text-align:left;width:90px">Cancelled</td>
             
    </tr>
                      
   
</table><br />
                 </td>
                 <td colspan="2" style="text-align:right">
                       <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                    
                 </td>
             </tr>
             
                            <tr style="text-align:center;">
                                  <td colspan="11" style="width:100%;text-align:center">
                                      <br />
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
                                                 
                                                     <asp:TemplateField HeaderText=""  ItemStyle-Width="5px">
                                                        <ItemTemplate></ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:BoundField DataField="PostStatus" HeaderText="Post St" >
                                                    <ControlStyle Width="2%" />
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                      <asp:TemplateField HeaderText="EI">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEI" runat="server"  Enabled="false" Checked='<%#If(Eval("EI").ToString() = "Y", True, False)%>' />
                                                 </ItemTemplate>
                                                           <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                      <ItemStyle CssClass="dummybutton" />
                                                   </asp:TemplateField>
                                                      <asp:BoundField DataField="Location" HeaderText="Location" />
                                                  
                                                  <asp:BoundField DataField="ConsolidatedInvoiceNo" HeaderText="Consolidated Inv No." SortExpression="ConsolidatedInvoiceNo" >
                                                    <ControlStyle Width="6%" />
                                                    <ItemStyle Width="6%" Wrap="False" />
                                                    </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="CIDate" HeaderText="Consolidated Inv Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="SalesDate">
                                                    <ControlStyle Width="3%" />
                                                    <ItemStyle Width="3%" />
                                                  </asp:BoundField>
                                                    <asp:BoundField DataField="UUID" HeaderText="UUID" />
                                                   
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId" Visible="False">
                                                    <ControlStyle Width="3%" />
                                                    <ItemStyle Width="3%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContType" HeaderText="Account Type" Visible="False" >
                                                    <ControlStyle Width="3%" />
                                                    <ItemStyle Width="3%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CustName" HeaderText="Customer Name" SortExpression="CustName" Visible="False">
                                                    <ControlStyle Width="16%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="16%" />
                                                  </asp:BoundField>
                                                 
                                                                                               
                                                  <asp:BoundField DataField="InvoiceFrom" HeaderText="Invoice From" DataFormatString="{0:dd/MM/yyyy}" visible="false">
                                                    <ControlStyle Width="2%" />
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="InvoiceTo" HeaderText="Invoice To" DataFormatString="{0:dd/MM/yyyy}" visible="false" >
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
                                                    <asp:BoundField DataField="EInvoiceStatus" HeaderText="E-Inv Status">
                                                          <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                      <ItemStyle CssClass="dummybutton" />
                                                        </asp:BoundField>

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
                       <asp:SqlDataSource ID="SqlDSCEInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
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
                                  <tr><td>
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
                                         <asp:TextBox ID="txtEI" runat="server" Height="16px"  Visible="false" Width="1px"></asp:TextBox>
                                          <asp:TextBox ID="txtLastPostedBy" runat="server" Height="16px"  Visible="false" Width="1px"></asp:TextBox>
                                      
                                          <asp:TextBox ID="txtEInvoiceStatus" runat="server" Height="16px"  Visible="false" Width="1px"></asp:TextBox>
                                                <asp:TextBox ID="txtUUID" runat="server" Height="16px"  Visible="false" Width="1px"></asp:TextBox>
                                    <asp:TextBox ID="txtLongID" runat="server" Height="16px"  Visible="false" Width="1px"></asp:TextBox>
                                       <asp:TextBox ID="txtRejectedDocError" runat="server" Visible="False"></asp:TextBox> 
         <asp:TextBox ID="txtSubmissionDate" runat="server" Visible="False"></asp:TextBox> 
                                           <asp:TextBox ID="txtSubmissionID" runat="server" Visible="False"></asp:TextBox> 
                                       <asp:TextBox ID="txtDeSelectContractGroup" runat="server" Visible="False"></asp:TextBox> 
                                       <asp:TextBox ID="txtMaxNoofInvinCI" runat="server" Visible="False"></asp:TextBox> 
                     </td>         </tr>
       
             </table>
      
     <%--   </ContentTemplate>
              </asp:UpdatePanel> --%>

         
          <%--  <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                  <ContentTemplate>   --%>

          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">

          <tr style="width:100%">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">INVOICE GENERATION INFORMATION
                 td>
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
                    <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">
                        <asp:TextBox ID="txtDefaultTaxCode" runat="server" AutoCompleteType="Disabled" visible="false" BorderStyle="None" ForeColor="#003366" Height="16px" Width="15%" Enabled="False"></asp:TextBox></td>
                </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="txtRcnoConsolidatedEInvoiceDetail" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="101"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Consolidated Invoice Date<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtInvoiceDate" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="21" ></asp:TextBox>
                          <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDate" TargetControlID="txtInvoiceDate" Enabled="True" />
                          </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                </tr>

     <%--         <tr style="display:none">
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Invoice Date<asp:Label ID="Label45" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtInvoiceDate" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" TabIndex="21" Width="80%" OnTextChanged="txtInvoiceDate_TextChanged"></asp:TextBox>
                       <asp:CalendarExtender ID="txtInvoiceDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDate" TargetControlID="txtInvoiceDate" />
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>
         --%>
              

            

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
                   <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 10%; text-align: right;">TaxType<asp:Label ID="Label7" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                   <td style="width: 20%; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">
                      <asp:DropDownList ID="txtGST" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="TaxType" DataValueField="TaxType" Height="25px" Value="-1" Width="80.6%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                   </td>
                   <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">
                    </td>
              </tr>

             <tr style="display:none">
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
                      <%--        <tr">
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">&nbsp;</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                               <asp:TextBox ID="txtComments" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px" Height="12px" TabIndex="27" Visible="False" Width="79.5%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                       </tr">
                       </tr> --%>

</Table> 

       <%--     </ContentTemplate>
              </asp:UpdatePanel>  --%>

<asp:CollapsiblePanelExtender ID="cpnl2" runat="server" CollapseControlID="Panel4" TargetControlID="Panel5" CollapsedImage="~/Images/plus.png" ExpandedImage="~/Images/minus.png" ImageControlID="imgCollapsible2" Enabled="True" ExpandControlID="Panel4" CollapsedText="Click to show" Collapsed="false"></asp:CollapsiblePanelExtender>
  
                <asp:Panel ID="Panel4" runat="server" >

         <table border="1" style="width:95%; border:solid; border-color:ButtonFace;">
              <tr style="width:100%">
                 <td colspan="11" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:0%; color:#800000; background-color: #C0C0C0;"> <asp:ImageButton ID ="imgCollapsible2" runat="server" ImageAlign="Bottom"  ImageUrl="~/Images/plus.png" /> &nbsp; SEARCH RECORD CRITERIA</td>
                  </tr>
             </table>
             </asp:Panel>

     <asp:Panel ID="Panel5" runat="server" style="height:180px" >

                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>    --%>  
        <div style="text-align:center">
        <table border="0"  style="width:87%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; border:solid; border-color:ButtonFace; "  >
            
                     <tr>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                <asp:Button ID="btnDummyClient" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                <asp:Label ID="lblBranch2" runat="server" Text="Select Branch" Visible="False"></asp:Label>
                            </td>

                          <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              
                              <cc1:DropDownCheckBoxes ID="ddlLocationSearch" runat="server" AddJQueryReference="true" style="top: 0px; left: 0px" UseButtons="false" UseSelectAllNode="true" Width="95%" Visible="False">
                                  <style2 DropDownBoxBoxHeight="120" DropDownBoxBoxWidth="85.5%" SelectBoxWidth="85.5%" />
                              </cc1:DropDownCheckBoxes>
                            </td>
                           <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"> Invoice From          <asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                      </td>
                            <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                       <asp:TextBox ID="txtDateFrom" runat="server" Height="16px" Width="90%" AutoCompleteType="Disabled" ></asp:TextBox>
                              <asp:CalendarExtender ID="calConDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom" Enabled="True" />
                             </td>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"> Invoice To  <asp:Label ID="Label6" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td>
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
                                <%--  <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient" BackgroundCssClass="modalBackground" Enabled="True"  ></asp:ModalPopupExtender> 
                            --%>      </td>
                           <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"> Customer Name
                        
                               
                            </td>
          <td colspan="2" style="width:15%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtClientName" runat="server" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>
                            </td>
                        </tr>
           
            </table> 
            </div>
                
    <%--   </ContentTemplate>
              </asp:UpdatePanel> --%>

         </asp:Panel>
            
<%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="95%" Height="400px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">--%>
          <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="500px" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1330px">
          
         <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:0%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label43" runat="server" Text="INVOICE DETAILS"></asp:Label>
&nbsp;</td></tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;width:100%;color:black;">           
     <%--   <asp:UpdatePanel ID="updpnlInvoiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate> --%>
            <asp:GridView ID="grvInvoiceRecDetails" runat="server" AllowSorting="True"
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr" 
             GridLines="None" Height="34px"  Font-Size="12px"
            ShowFooter="True" Style="text-align: left" Width="100%">
                
                <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false" TextAlign="left" onchange="checkallservicerecs()"  Width="5%" ></asp:CheckBox></HeaderTemplate>
                    
              <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false"   onchange="checkoneservicerec()"  ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
                <%--<asp:TemplateField HeaderText="Location Id" SortExpression="LocationId"><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Text='<%# Bind("LocationID")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="82px"></asp:TextBox></ItemTemplate></asp:TemplateField>--%>
      <%--     <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtLocationGV" runat="server" Text='<%# Bind("Location")%>' Height="15px" Font-Size="11px" ReadOnly="true" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             --%>
                        <asp:BoundField DataField="Location" HeaderText="Branch" SortExpression="Location" />
                             <asp:BoundField DataField="InvoiceNumber" HeaderText="InvoiceNumber" SortExpression="InvoiceNumber" />
                                          
                      <asp:BoundField DataField="SalesDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Invoice Date" SortExpression="SalesDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" SortExpression="ContactType" />
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CustName" HeaderText="Client Name" SortExpression="CustName">
                                                    <ControlStyle Width="300px" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
                                                  </asp:BoundField>
                                                 
                                                 <%--     <asp:TemplateField HeaderText="Client Name" >
                                                           <EditItemTemplate>
                                                                <asp:TextBox ID="txtClientNameConcat" runat="server" Text='<%# Eval("CustName")%>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                              <ItemTemplate> 
                                                                   <div style="width: 200px;text-align:left;height:37px;overflow-y:auto;">
                                                                 <asp:Label ID="Label3" runat="server" Text='<%# Eval("CustName")%>'></asp:Label>
                                                           </div>
                                                                          </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Width="270px" />
                                                           <ItemStyle Font-Names="Calibri"  HorizontalAlign="Left" />
                                                      </asp:TemplateField> --%>

                                                    <asp:BoundField DataField="ValueBase" HeaderText="Sales Amount" DataFormatString="{0:N2}" SortExpression="ValueBase">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="True" />
                                                    </asp:BoundField>
                     <asp:BoundField DataField="GstBase" HeaderText="GST Amount" DataFormatString="{0:N2}" SortExpression="GSTBase">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="True" />
                                                    </asp:BoundField>
                                               <%-->     <asp:BoundField DataField="AppliedBase" HeaderText="Net Inv. Amt." DataFormatString="{0:N2}" SortExpression="AppliedBase">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField> --%>
                     <asp:BoundField DataField="AppliedBase" HeaderText="Net Inv Amount" DataFormatString="{0:N2}" SortExpression="AppliedBase">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                <%--    <asp:BoundField DataField="Receiptbase" HeaderText="Receipt Amt." DataFormatString="{0:N2}" SortExpression="Receiptbase" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreditBase" HeaderText="Debit/ Credit Amt." DataFormatString="{0:N2}" SortExpression="CreditBase" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BalanceBase" HeaderText="OS Amount" DataFormatString="{0:N2}" SortExpression="BalanceBase" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField> --%>
                    <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtSalesAmtGV" runat="server" visible="true" Text='<%# Bind("ValueBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="50px" Align="right" ></asp:TextBox></ItemTemplate>
                           <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                      <ItemStyle CssClass="dummybutton" />
                    </asp:TemplateField>
                     <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtGSTAmtGV" runat="server" visible="true" Text='<%# Bind("GstBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="50px" Align="right" ></asp:TextBox></ItemTemplate>
                              <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                      <ItemStyle CssClass="dummybutton" />
                    </asp:TemplateField>
                     <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtToBillAmtGV" runat="server" visible="true" Text='<%# Bind("AppliedBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="50px" Align="right" ></asp:TextBox></ItemTemplate>
                             <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                      <ItemStyle CssClass="dummybutton" />
                    </asp:TemplateField>
                     <asp:HyperLinkField Text="View"    
         DataNavigateUrlFields="InvoiceNumber" 
         DataNavigateUrlFormatString="Invoice.aspx?InvoiceNumber={0}&DocFrom=Consolidate" HeaderText="View" DataTextFormatString="View" target="_blank" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="White" ItemStyle-ForeColor="Black" />

                       <asp:TemplateField><ItemTemplate>
                    <asp:Button ID="btnViewInvoice" runat="server" Visible="false" Text="View" Font-Size="10px" Width="120px"  OnClick="btnViewInvoice_Click" onclientclick="openinnewtab()" />
                   </ItemTemplate>
                   </asp:TemplateField>  
                     <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtInvoiceNoGV" runat="server" Class="dummybutton" Text='<%# Bind("InvoiceNumber")%>'  Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" Visible="true" BorderStyle="None" Height="18px" Width="110px"></asp:TextBox></ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                         <asp:TemplateField SortExpression="SalesDate"><ItemTemplate><asp:TextBox ID="txtSalesDateGV" runat="server" class="dummybutton" Text='<%# Bind("SalesDate")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="60px" ></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" class="dummybutton" Text='<%# Bind("AccountId")%>' Visible="false" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="0px"></asp:TextBox></ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                       <asp:TemplateField ><ItemTemplate><asp:TextBox ID="txtCompanyGroupGV" runat="server" Text='<%# Bind("CompanyGroup")%>' class="dummybutton" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None"  Height="18px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
         
                       <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContactTypeGV" runat="server" Text='<%# Bind("ContactType")%>' Visible="false" class="dummybutton" ReadOnly="true" Font-Size="11px"  BackColor="#CCCCCC" BorderStyle="None"  Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
         
              <asp:TemplateField SortExpression="CustName"><ItemTemplate><asp:TextBox ID="txtClientNameGV" runat="server" class="dummybutton" Text='<%# Bind("CustName")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="125px"></asp:TextBox></ItemTemplate></asp:TemplateField>
       
                       <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGV" runat="server" class="dummybutton" Visible="true" Height="15px" Width="0px" Text='<%# Bind("Rcno")%>'></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtValueBaseGV" runat="server" class="dummybutton" Text='<%# Bind("ValueBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:right" width="55px"></asp:TextBox></ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtAppliedBaseGV" runat="server" class="dummybutton" Text='<%# Bind("AppliedBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:right" width="55px"></asp:TextBox></ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtReceiptBaseGV" runat="server" class="dummybutton" Text='<%# Bind("ReceiptBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:right" width="55px"></asp:TextBox></ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtCreditBaseGV" runat="server" class="dummybutton" Text='<%# Bind("CreditBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:right" width="55px"></asp:TextBox></ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtBalanceBaseGV" runat="server" class="dummybutton" Text='<%# Bind("BalanceBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:right" width="55px"></asp:TextBox></ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtGstBaseGV" runat="server" class="dummybutton" Text='<%# Bind("GstBase")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:right" width="55px"></asp:TextBox></ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                      <asp:BoundField DataField="Rcno" InsertVisible="False" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label4" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                                      </ItemTemplate>
                                                      <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                      <ItemStyle CssClass="dummybutton" />
                                                  </asp:TemplateField>
                 
               
                     </Columns>

         
                 <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#f7dfb5" ForeColor="White" Font-Bold="True" Height="5px" />
                                                    <HeaderStyle HorizontalAlign="center" BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                                                    <PagerStyle ForeColor="White" HorizontalAlign="center" BackColor="#2461BF" />
                                                    <RowStyle BackColor="#EFF3FB" HEIGHT="17px" Font-Names="Calibri" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />

            </asp:GridView>
        <%--    </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvInvoiceRecDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr>
             --%>
              <tr>
                  <td  style="width:80%;font-size:13px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
             <%--    <asp:UpdatePanel ID="UpdateSelect" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="btnSelect" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                            
                </ContentTemplate></asp:UpdatePanel>--%>
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
        <%--         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional"><ContentTemplate> --%>
                          Total Amount <asp:TextBox ID="txtTotalSelected" runat="server" AutoCompleteType="Disabled" Height="16px" Width="10%" style="text-align:right" Enabled="false" ></asp:TextBox>
            <%--    </ContentTemplate></asp:UpdatePanel> --%>
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

      <%--    <asp:UpdatePanel runat="server" ID="updPnlBillingRecs" UpdateMode="Conditional">
                  <ContentTemplate> --%>

  


                <%--      <asp:UpdatePanel ID="updPanelSave" runat="server" UpdateMode="Conditional"><ContentTemplate> --%>

                      <table border="0" style="width:97%; margin:auto;display:none" >
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
                        
                           
              <%--           
                          
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
                        </tr> --%>
                        
            </table>
                         
             <%--         </ContentTemplate>


                      </asp:UpdatePanel>
                </ContentTemplate>
              </asp:UpdatePanel> --%>
         
                      </asp:Panel>   
              
       
                  
    <%--      <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                  <ContentTemplate>    --%>


          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                          
        <tr>
                           <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               &nbsp;</td>
                           <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               <asp:Button ID="btnSaveInvoice" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" TabIndex="28" Text="SAVE" Width="150px" />
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;  ">
                               <asp:Button ID="btnCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="150px" />
                           </td>
                       </tr>
              </table>
      <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;display:none">
     

                       <tr style="display:none">
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
                       <tr style="display:none">
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
                                 <asp:Button ID="btnDeleteUnselected" runat="server" visible="false" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" CommandName="DELETE" Text="DELETE UNSELECTED" Width="31%" />
                               <asp:Button ID="btnGenerateInvoice" runat="server" visible="false" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidation(); btn_disable(); " TabIndex="28" Text="GENERATE INVOICE" Width="30%" />
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
     <%--     </ContentTemplate>
              </asp:UpdatePanel> --%>
       <%--      
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
                
        </div></asp:Panel> --%>
    <%--

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
     <%--        
              
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
                    <asp:Button ID="btnCloseEditHistory" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
                          

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewEditHistory" runat="server" CancelControlID="ImageButton6" PopupControlID="pnlViewEditHistory" TargetControlID="btnDummyViewEditHistory" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewEditHistory" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  --%>

                   <%-- EInvoice Submission Confirm--%>

      <asp:Panel ID="pnlEInvConfirm" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label33" runat="server" Text="Confirm Submit E-Invoice "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
                    <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label36" runat="server" Text="Do you want to SUBMIT this INVOICE "></asp:Label>
                      </td>
                     </tr>

              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label37" runat="server" Text="to MALAYSIA E-INVOICE? "></asp:Label>
                      </td>
                     </tr>

              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                        <%--  &nbsp;<asp:Label ID="Label38" runat="server" Text="[ YES ] / [ No ]"></asp:Label> --%>
                      </td>
                     </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="EInvoiceConfirmYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="EInvoiceConfirmNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlEInvoiceConfirm" runat="server" TargetControlID="btnEInvoiceConfirm"   PopupControlID="pnlEInvConfirm" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnEInvoiceConfirm" runat="server" CssClass="dummybutton" />
         <%--end--%>

    
             <asp:Panel ID="pnlStatusSearch" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          <br /><br />    <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;">
                  <tr>
                      <td>
                          <asp:RadioButtonList ID="rdbStatusSearch" runat="server" AutoPostBack="True" Visible="False">
                              <asp:ListItem Value="ALL">ALL</asp:ListItem>
                              <asp:ListItem Value="Status">STATUS</asp:ListItem>
                          </asp:RadioButtonList>
                     <asp:checkboxlist ID="chkStatusSearch" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3" CellPadding="2" CellSpacing="2" TextAlign="Right">
                                   <asp:ListItem Value="O">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="P">P - Posted</asp:ListItem>   
                                   <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                  
                               </asp:checkboxlist></td>
                           </tr>
                           
                         <tr>
                             <td colspan="2"><asp:CheckBox ID="chkSearchAll" runat="server"  Text="All" Font-Size="15px" Font-Names="Calibri" Font-Bold="True" onchange="EnableDisableStatusSearch()" /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnStatusSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="120px"/>
                                 <asp:Button ID="btnStatusCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>


     <%--         </ContentTemplate>
        <Triggers>
            <PostBackTrigger ControlID="GridView1"></PostBackTrigger>
        </Triggers>
        </asp:UpdatePanel> --%>
</asp:Content>

