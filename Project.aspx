<%@ Page Title="Project" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Project.aspx.vb" Inherits="Project" Culture="en-GB"   %>


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
  
       
           .auto-style2 {
               width: 15%;
           }
  
       
          </style>
           
    
    

<script type="text/javascript">
    window.history.forward(1);
 
    ///

    //window.onload = LoadDefaultDates();

   

    function ClearSearch() {
        document.getElementById("<%=txtContractnoSearch.ClientID%>").value = "";
        document.getElementById("<%=txtAccountIdSearch.ClientID%>").value = "";
        document.getElementById("<%=txtProjectNameSearch.ClientID%>").value = "";
        document.getElementById("<%=txtInchargeSearch.ClientID%>").value = "";
        document.getElementById("<%=txtClientNameSearch.ClientID%>").value = "";
        document.getElementById("<%=txtSearch1Status.ClientID%>").value = "O,P";
        document.getElementById("<%=txt.ClientID%>").value = "";
       
    }


    


    function LoadDefaultDates() {
              
        var dur = 1;    
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
      
        endct.setDate(endct.getDate() - 1);

        var dd = endct.getDate();
        var mm = endct.getMonth() + 1;
        var y = endct.getFullYear() + (dur);
                
        if (dd < 10) {
            dd = "0" + dd;
        }
        if (mm < 10)
            mm = "0" + mm;

                   

        document.getElementById("<%=txtContractEnd.ClientID%>").value = dd + "/" + mm + "/" + y;
        document.getElementById("<%=txtServStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
        document.getElementById("<%=txtServEnd.ClientID%>").value = dd + "/" + mm + "/" + y;

      
        var dateone = (document.getElementById("<%=txtServStart.ClientID%>").value);
        var datetwo = (document.getElementById("<%=txtServEnd.ClientID%>").value);
         
        
        if (dateone != "") {

            var timein = dateone.split('/');
            var startdate = parseInt(timein[0], 10);
            var startmonth = parseInt(timein[1], 10);
            var startyear = parseInt(timein[2], 10);

            var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
            var startTime = new Date(str3);

            var weekdaystart = startTime.getDay();

            if (weekdaystart == 1)
                weekdaystart = "MONDAY";
            else if (weekdaystart == 2)
                weekdaystart = "TUESDAY";
            else if (weekdaystart == 3)
                weekdaystart = "WEDNESDAY";
            else if (weekdaystart == 4)
                weekdaystart = "THURSDAY";
            else if (weekdaystart == 5)
                weekdaystart = "FRIDAY";
            else if (weekdaystart == 6)
                weekdaystart = "SATURDAY";
            else if (weekdaystart == 0)
                weekdaystart = "SUNDAY";
            document.getElementById("<%=txtServStartDay.ClientID%>").value = weekdaystart;

        }

        if (datetwo != "") {

            var timeout = datetwo.split('/');
            var enddate = parseInt(timeout[0], 10);
            var endmonth = parseInt(timeout[1], 10);
            var endyear = parseInt(timeout[2], 10);

            var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";
            var endTime = new Date(str4);

            var weekdayend = endTime.getDay();
                
            if (weekdayend == 1)
                weekdayend = "MONDAY";
            else if (weekdayend == 2)
                weekdayend = "TUESDAY";
            else if (weekdayend == 3)
                weekdayend = "WEDNESDAY";
            else if (weekdayend == 4)
                weekdayend = "THURSDAY";
            else if (weekdayend == 5)
                weekdayend = "FRIDAY";
            else if (weekdayend == 6)
                weekdayend = "SATURDAY";
            else if (weekdayend == 0)
                weekdayend = "SUNDAY";
            document.getElementById("<%=TxtServEndDay.ClientID%>").value = weekdayend;

        }

        var months;
        var year1 = startTime.getFullYear();
        var year2 = endTime.getFullYear();
        var month1 = startTime.getMonth();
        var month2 = endTime.getMonth() + 1;

        if (month1 === 0) {
            month1++;
            month2++;
        }
        var numberOfMonths;
        numberOfMonths = (year2 - year1) * 12 + (month2 - month1) - 1;
      
                
      
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

    
        var defaultTextTeam = "Search Here for Team or In-ChargeId";
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
        function ConfirmSelection() {
        
            var origAcid = document.getElementById("<%=txtOriginalAccountId.ClientID%>").value ;
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

        function getRetentionAmount() {

            var retentionperc = document.getElementById("<%=txtRetentionPerc.ClientID%>").value;
            var agreeval= document.getElementById("<%=txtAgreeVal.ClientID%>").value;


            (document.getElementById("<%=txtRetentionValue.ClientID%>").value) = ((agreeval * retentionperc) * .01).toFixed(2);

        }


        function getContactType() {

            document.getElementById("<%=txtclientid.ClientID%>").value = '';
            //document.getElementById("<%=txtAccountId.ClientID%>").value = '';
            document.getElementById("<%=txtCustName.ClientID%>").value = '';
            document.getElementById("<%=txtOfficeAddress.ClientID%>").value = '';
            document.getElementById("<%=txtPostal.ClientID%>").value = '';
          

            var contacttype = document.getElementById("<%=ddlContactType.ClientID%>").options[document.getElementById("<%=ddlContactType.ClientID%>").selectedIndex].text;

            if (contacttype == "CORPORATE" || contacttype == "COMPANY") {
                (document.getElementById("<%=txtContType1.ClientID%>").value) = "CORPORATE";
                (document.getElementById("<%=txtContType2.ClientID%>").value) = "COMPANY";
                (document.getElementById("<%=txtContType3.ClientID%>").value) = "RESIDENTIAL";
                (document.getElementById("<%=txtContType4.ClientID%>").value) = "PERSON";
            }
            else if (contacttype == "RESIDENTIAL" || contacttype == "PERSON") {
                (document.getElementById("<%=txtContType1.ClientID%>").value) = "RESIDENTIAL";
                (document.getElementById("<%=txtContType2.ClientID%>").value) = "PERSON";
                (document.getElementById("<%=txtContType3.ClientID%>").value) = "CORPORATE";
                (document.getElementById("<%=txtContType4.ClientID%>").value) = "COMPANY";
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



        function TabChanged(sender, e) {
            if (sender.get_activeTabIndex() == 1) {
                if (document.getElementById("<%=txtContractNo.ClientID%>").value == '') {
                    $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                    alert("Please select a Contract record to proceed.");
                    return;
                }
         
            
            }
            if (sender.get_activeTabIndex() == 2) {
                if (document.getElementById("<%=txtContractNo.ClientID%>").value == '') {
                         $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                    alert("Please select a Contract record to proceed.");
                    return;
                }

                

            
                 }
            else {
              
           
            }

        }


        function currentdatetime()
        {
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


            var dateone = (document.getElementById("<%=txtActualEndChSt.ClientID%>").value);

            if (dateone == '')
                document.getElementById("<%=txtActualEndChSt.ClientID%>").value = document.getElementById("<%=txtCreatedOn.ClientID%>").value;
        
        }


        function currentdatetimecontract() {
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
            document.getElementById("<%=txtContractDate.ClientID%>").value = dd + "/" + mm + "/" + y;
            document.getElementById("<%=txtContractStart.ClientID%>").value = dd + "/" + mm + "/" + y;
            document.getElementById("<%=txtServStart.ClientID%>").value = dd + "/" + mm + "/" + y;
           
            var dateone = (document.getElementById("<%=txtActualEndChSt.ClientID%>").value);
          
            if (dateone == '')
                document.getElementById("<%=txtActualEndChSt.ClientID%>").value = dd + "/" + mm + "/" + y;

           
        }


      
    
  
      
       

   
        function ValidateDatesService() {

            var dateone = (document.getElementById("<%=txtServStart.ClientID%>").value);
            var datetwo = (document.getElementById("<%=txtServEnd.ClientID%>").value);

            if (dateone != "" && datetwo != "") {

                var timein = dateone.split('/');
                var timeout = datetwo.split('/');
                var startdate = parseInt(timein[0], 10);
                var startmonth = parseInt(timein[1], 10);
                var startyear = parseInt(timein[2], 10);

                var enddate = parseInt(timeout[0], 10);
                var endmonth = parseInt(timeout[1], 10);
                var endyear = parseInt(timeout[2], 10);

            }

            if (dateone != "" && datetwo != "") {

                var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                var str4 = endyear + "/" + endmonth  + "/" + enddate + " 10:00:00";

                var startTime = new Date(str3);
                var endTime = new Date(str4);

                if (endTime < startTime) {
                    //alert("Service Start Date cannot be later than Contract End Date");
                    document.getElementById("<%=lblAlert.ClientID%>").innerText = "Service Start Date cannot be later than Service End Date";
                    ResetScrollPosition();
                    document.getElementById("<%=txtServStart.ClientID%>").focus();
                    return;
                }
            }

            CalculateServiceDates();
            getWeekDay();

        
        }


      

    
        function Left(str, n) {
            if (n <= 0)
                return "";
            else if (n > String(str).length)
                return str;
            else
                return String(str).substring(0, n);
        }
        function Right(str, n) {
            if (n <= 0)
                return "";
            else if (n > String(str).length)
                return str;
            else {
                var iLen = String(str).length;
                return String(str).substring(iLen, iLen - n);
            }
        }

        function ConfirmChSt() {

            currentdatetime();

            var confirm_valueChSt = "";
            confirm_valueChSt = document.createElement("INPUT");
            confirm_valueChSt.type = "hidden";
            confirm_valueChSt.name = "confirm_valueChSt";

           // alert (document.getElementById("<%=ddlStatusChSt.ClientID%>").options[document.getElementById("<%=ddlStatuschst.ClientID%>").selectedIndex].text);
            var statusChSt = document.getElementById("<%=ddlStatusChSt.ClientID%>").options[document.getElementById("<%=ddlStatuschst.ClientID%>").selectedIndex].text;
            var status= document.getElementById("<%=txtStatus.ClientID%>").value;

        //    alert(Left(statusChSt, 1));
            if ((Left(statusChSt, 1) == status)) {
                
                document.getElementById("<%=txtChangeStatus.ClientID%>").value = "000";
                //document.getElementById("<%=lblAlert.ClientID%>").innerText = "SELECTED STATUS IS SAME AS CURRENT STATUS";
                return;

            }

            if ((Left(statusChSt, 1) == "P") || (Left(statusChSt, 1) == "V") || (Left(statusChSt, 1) == "S")) {
                if (status != "O") {
                    
                    document.getElementById("<%=txtChangeStatus.ClientID%>").value = "001";
                    //document.getElementById("<%=lblAlert.ClientID%>").innerText = "CONTRACT STATUS SHOULD BE [O-OPEN]";
                    return;
                }

            }


            if (statusChSt == "--SELECT--") {
                //alert("Status cannot be blank");
                //document.getElementById("<%=lblAlert.ClientID%>").innerText = "Status cannot be blank";
               
                document.getElementById("<%=txtChangeStatus.ClientID%>").value = "002";
                confirm_valueChSt.value = "002";
                document.forms[0].appendChild(confirm_valueChSt);
                return;
            }
            else {
                confirm_valueChSt.value = "111";
            }


            var actualendchst = document.getElementById("<%=txtActualEndChSt.ClientID%>").value;
            if (actualendchst == "") {
                //alert("Actual End Date cannot be blank");
                //document.getElementById("<%=lblAlert.ClientID%>").innerText = "Actual End Date cannot be blank";
               
                document.getElementById("<%=txtChangeStatus.ClientID%>").value = "003";
                document.forms[0].appendChild(confirm_valueChSt);
                return;
            }
            else {
                confirm_valueChSt.value = "112";

            }

            
            
        
          
            
            if (Left(statusChSt, 1) == "O") {
                if (confirm("Do you want to Continue?")) {
                    confirm_valueChSt.value = "Yes";
                } else {
                    confirm_valueChSt.value = "No";
                }
            }

            if (Left(statusChSt, 1) != "O")
            {
                if (confirm("Do you want to Continue?")) {
                    confirm_valueChSt.value = "Yes";
                } else {
                    confirm_valueChSt.value = "No";
                }
            }
            document.forms[0].appendChild(confirm_valueChSt);
        }


        //       var okayToLeave = false;

        //      window.onbeforeunload = function () {
        //         if (!okayToLeave) {
        //             return "Any UNSAVED work will be LOST..."
        //         }
        //     }

      

        // function OkayToLeave() {
        //     okayToLeave = true;
        //  }
        function ClearTextBox() {
            document.getElementById("<%=txtClient.ClientID%>").value = '';
        }

        //function stopRKey(evt) {
        //    var evt = (evt) ? evt : ((event) ? event : null);
        //    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
        //    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        //}

        //document.onkeypress = stopRKey;

   
    
        function CheckContactType() {
            var contactType = document.getElementById("<%=ddlContactType.ClientID%>").options[document.getElementById("<%=ddlContactType.ClientID%>").selectedIndex].text;

            if (contactType == '--Select--') {
                //alert("Please select Contact Type");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Contact Type";
                ResetScrollPosition();
                document.getElementById("<%=ddlContactType.ClientID%>").focus();
                $find("mdlPopUpClient").hide();
                return false;
            }
            else {
                return true;
            }
        }

        function checkstatus() {
            var status = document.getElementById("<%=txtStatus.ClientID%>").value;

            if(status != "O")
            {
                //alert("Records with status [O-Open] can be saved only");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Records with status [O-Open] can be saved only";
                ResetScrollPosition();
                
                valid = false;
                return valid;

            }

          
            return valid;
        }


       



        function DoValidation(parameter) {
                     
            
            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;

            var stat = document.getElementById("<%=txtStatus.ClientID%>").value;

            currentdatetime();
            if (stat != 'O')
            {
                //alert("Records with status [O-Open] can be saved only");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Records with status [O-Open] can be saved only";
                ResetScrollPosition();
                
                valid = false;
                return valid;
          
            }


            var projectname = document.getElementById("<%=txtProjectName.ClientID%>").value;

            if (projectname == '') {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Project Name";
                ResetScrollPosition();
                document.getElementById("<%=txtProjectName.ClientID%>").focus();
                valid = false;
                return valid;
            }

            var contactType = document.getElementById("<%=ddlContactType.ClientID%>").options[document.getElementById("<%=ddlContactType.ClientID%>").selectedIndex].text;

            if (contactType == '--SELECT--') {
                //alert("Please Select Customer Type");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Account Type";
                ResetScrollPosition();
                document.getElementById("<%=ddlContactType.ClientID%>").focus();
                valid = false;
                return valid;
            }



            var accountid = document.getElementById("<%=txtAccountId.ClientID%>").value;

            if (accountid == '') {
                //alert("Please Select Client");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Client";
                ResetScrollPosition();
                document.getElementById("<%=txtAccountId.ClientID%>").focus();
                valid = false;
                return valid;
            }

          

            var companygrp = document.getElementById("<%=ddlCompanyGrp.ClientID%>").options[document.getElementById("<%=ddlCompanyGrp.ClientID%>").selectedIndex].text;

            if (companygrp == '--SELECT--') {
                //alert("Please Select Company Group");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Company Group";
                ResetScrollPosition();
                document.getElementById("<%=ddlCompanyGrp.ClientID%>").focus();
                valid = false;
                return valid;
            }


            var salesman = document.getElementById("<%=ddlSalesman.ClientID%>").options[document.getElementById("<%=ddlSalesman.ClientID%>").selectedIndex].text;

            if (salesman == '--SELECT--') {
                //alert("Please Select Salesman");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Salesman";
                ResetScrollPosition();
                document.getElementById("<%=ddlSalesman.ClientID%>").focus();
                valid = false;
                return valid;
            }


            var scheduler = document.getElementById("<%=ddlScheduler.ClientID%>").options[document.getElementById("<%=ddlScheduler.ClientID%>").selectedIndex].text;

            if (scheduler == '--SELECT--') {
                //alert("Please Select Scheduler");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Scheduler";
                ResetScrollPosition();
                document.getElementById("<%=ddlScheduler.ClientID%>").focus();
               
                valid = false;
                            
                return valid;
            }

           
            var contractgrp = document.getElementById("<%=ddlContractGrp.ClientID%>").options[document.getElementById("<%=ddlContractGrp.ClientID%>").selectedIndex].text;

            if (contractgrp == '--SELECT--') {
                //alert("Please Select Contract Group");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Department";
                ResetScrollPosition();
                document.getElementById("<%=ddlContractGrp.ClientID%>").focus();
                valid = false;
                return valid;
            }

            var condetval = document.getElementById("<%=txtConDetVal.ClientID%>").value;

            if (condetval == '' || condetval < 1) {
                //alert("Please Enter Valid Contract Value");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Contract Value";
                ResetScrollPosition();
                document.getElementById("<%=txtConDetVal.ClientID%>").focus();
                valid = false;
                return valid;
            }

         
                 

            var agreevalue = document.getElementById("<%=txtAgreeVal.ClientID%>").value;

            if (agreevalue == '' || agreevalue < 1) {
                //alert("Please Enter Valid Agreee Value");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Agreed Value";
                ResetScrollPosition();
                document.getElementById("<%=txtAgreeVal.ClientID%>").focus();
                valid = false;
                return valid;
            }

             

           

          
            var str = document.getElementById("<%=txtAgreeVal.ClientID%>").value;
          
           
          
           

            var str36 = document.getElementById("<%=txtRetentionPerc.ClientID%>").value;
            var str37 = document.getElementById("<%=txtRetentionValue.ClientID%>").value;


            if (str != "" && regex.test(str)) {
                valid = true;
            }
            else {
                valid = false;
                //alert("Enter Only Numbers for Agreed Value");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Agreed Value";
                ResetScrollPosition();
                document.getElementById("<%=txtAgreeVal.ClientID%>").focus();
            }

          
          

                 

                 

         
         

            if (str36 != "" && isNaN(str36)) {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Retention Percent";
                  ResetScrollPosition();
                  document.getElementById("<%=txtRetentionPerc.ClientID%>").focus();
                 valid = false;
             }

             if (str37 != "" && isNaN(str37)) {
                 document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Retention Amount";
                ResetScrollPosition();
                document.getElementById("<%=txtRetentionValue.ClientID%>").focus();
                 valid = false;
             }
           

            return valid;
        };


    
        function ResetScrollPosition() {
            setTimeout("window.scrollTo(0,0)", 0);
        }

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="updPanelContract1" runat="server" UpdateMode="Conditional">
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
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">PROJECT</h3>
          

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
                 <td colspan="13"   style="width:100%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
                     <asp:Label ID="lblAccountIdContact" runat="server" Visible="false" Text="Account Id: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblAccountIdContact1" runat="server" Text=""  ></asp:Label>  &nbsp;  &nbsp;  &nbsp;
              <asp:Label ID="lblAccountIdContactLocation" runat="server" Visible="false" Text="Location Id: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblAccountIdContactLocation1" runat="server" Text=""  ></asp:Label>
                             <asp:Label ID="txtMode" runat="server" Text="" Visible="true" CssClass="dummybutton" ></asp:Label>
                      &nbsp;
                      </td> 
            </tr>

              <tr>
                <td style="width:8%;text-align:center;"> 
                   <asp:Button ID="btnADD" runat="server" Font-Bold="True" Text="ADD" Width="95%"  CssClass="button" BackColor="#CFC6C0" Visible="TRUE" OnClientClick="return currentdatetimecontract()" />
                 
                      </td>
                  
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnCopy" runat="server" Font-Bold="True" Text="COPY" Width="95%"  CssClass="button" BackColor="#CFC6C0" Visible="TRUE" />
                    </td>
                     
                   <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="95%"  CssClass="button" BackColor="#CFC6C0" Visible="TRUE" />
                    </td>
                  <td>
                      <asp:Button ID="btnDelete" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" OnClientClick="Confirm(); currentdatetime()" Text="DELETE" Width="95%" />
                    </td>
                      

                  <td style="width:8%;text-align:center;">
                    <asp:Button ID="btnFilter" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="95%" Visible="true"/>
                    </td>
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnReset" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="RESET" Visible="true" Width="95%" />
                      </td>
                  <td style="width:8%;text-align:center;">
                    <asp:Button ID="btnPrint" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="95%" Visible="true" />
                    </td>
                  <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="CH. ST." Visible="true" Width="95%" />
                     </td>
                  
                    <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnClaim" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="CLAIM" Width="95%" Visible="true" />
              
                    </td>

                   <td style="width:8%;text-align:center;">
                        <asp:Button ID="btnQuit" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="QUIT" Width="95%" />
               
                    </td>

                   <td style="width:8%;text-align:center;">
                         &nbsp;</td>

                   <td style="width:8%;text-align:center;">
                         &nbsp;</td>

                    <td style="width:8%;text-align:center;">
                         &nbsp;</td>
                  <td style="width:8%;text-align:center;">
                       &nbsp;</td>
                
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
      

             <table id="tablesearch" runat="server" style="border: 1px solid #CC3300; text-align:right; width:100%; border-radius: 25px;padding: 2px; width:100%; height:60px; background-color: #F3F3F3;">
            <tr>
                <td style="text-align:left;width:100%;">
                   
                    <table style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000;width:100%;padding-left:10px;">
                                                 <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Project No.                
                            </td>
                            <td colspan="3">
                                 <asp:TextBox ID="txtContractnoSearch" runat="server" style="text-align:left;padding-left:5px;" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" ></asp:TextBox>         
                            </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Project Name
                            </td>
                             <td colspan="3">
                                 <asp:TextBox ID="txtProjectNameSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;"></asp:TextBox>         
                                     
                            <asp:ImageButton ID="ImageButton3" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Middle" Visible="False"     />   
                        
                            </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                            </td>
                              <td colspan="2">
                                  <asp:TextBox ID="txtInchargeSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;" Visible="False"></asp:TextBox>         
                    
                            &nbsp;</td>

                              <td colspan="1" style="text-align:center">
                                       <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Search" Width="95%" />
                                 </td>
                        </tr>
                          <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Account Id
                            </td>
                             <td colspan="3">
                                   <asp:TextBox ID="txtAccountIdSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;" ></asp:TextBox>         
                                   
                            <asp:ImageButton ID="ImageButton2" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Middle"     />                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                                                 
                            
                                                      
                            </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Client Name
                            </td>
                             <td colspan="3">
                                 <asp:TextBox ID="txtClientNameSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;"></asp:TextBox>         
                    
                            </td>


                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Status
                            </td>
                             <td colspan="2">
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
                                  <td colspan="9" style="width:100%;text-align:center">
                                      
                                      <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="None"    Visible="true" Width="100%">

                                          <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSContract">
                                                <Columns>
                                                  <asp:CommandField ShowHeader="True" ShowSelectButton="True">
                                                  <ControlStyle Width="4%" />
                                                  <ItemStyle Width="4%" Wrap="False" HorizontalAlign="Left" />
                                                  </asp:CommandField>
                                                  <asp:BoundField DataField="Status" HeaderText="ST" SortExpression="Status" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ProjectNo" HeaderText="Project No" SortExpression="ProjectNo" >
                                                    <ControlStyle Width="6%" />
                                                    <ItemStyle Width="6%" Wrap="False" />
                                                    </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                                    <ControlStyle Width="15%" />
                                                    <ItemStyle Width="15%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="ContractDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Project Date" SortExpression="ContractDate" >
                                                    <ItemStyle Width="10%" Wrap="True" />
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CustName" HeaderText="Client Name" SortExpression="CustName">
                                                    <ControlStyle Width="20%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="True" Width="18%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="ServiceAddress" HeaderText="Customer Address" SortExpression="ServiceAddress" >
                                                 
                                                    <ControlStyle Width="18%" />
                                                 
                                                    <ItemStyle Wrap="True" Width="18%" />
                                                    </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="AgreeValue" HeaderText="Agreed Value" >
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="StartDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date" SortExpression="StartDate" >
                                                    <ControlStyle Width="5%" />
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="EndDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="End Date" >
                                                    <ControlStyle Width="5%" />
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ActualEnd" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Actual End" Visible="False" >
                                                    <ControlStyle Width="5%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="Scheduler" HeaderText="Scheduler" SortExpression="Scheduler" >
                                                    <ControlStyle Width="7%" />
                                                    <ItemStyle Width="7%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False" />
                                                  <asp:BoundField DataField="Rcno" HeaderText="Rcno" InsertVisible="False" ReadOnly="True" SortExpression="Rcno" Visible="False" />
                                                  <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
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
                      <asp:SqlDataSource ID="SQLDSContract" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="select Status,  ProjectNo, ProjectName, ContractDate, AccountId, CustName, CustAddr, InchargeId, AgreeValue, StartDate, EndDate, ActualEnd, ContractGroup, ServiceAddress,
 Scheduler, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno  From tblProject where (Status ='O' or Status = 'P') order by rcno desc, CustName limit 50">
                </asp:SqlDataSource>
                  </td> 

                 <td>
                     <asp:SqlDataSource ID="SqlDSState" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT State FROM tblstate ORDER BY State"></asp:SqlDataSource>
                  </td>

                     <td>

                         <asp:SqlDataSource ID="SqlDSCity" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT City FROM tblcity ORDER BY City"></asp:SqlDataSource>

                 </td>

                     <td>

                         <asp:SqlDataSource ID="SqlDSCountry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Country FROM tblcountry  ORDER BY Country"></asp:SqlDataSource>

                 </td>

                     <td>
                         <asp:SqlDataSource ID="SqlDSScheduleType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT upper(ScheduleType) FROM tblscheduletype WHERE (Rcno &lt;&gt; 0) ORDER BY upper(Scheduletype)"></asp:SqlDataSource>
                 </td>
                 <td>
                    <asp:SqlDataSource ID="SQLDSContractClientId" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Status, RenewalSt, NotedST, Gst, ContractNo, ContractDate, AccountId, CustName, CustAddr, InchargeId, AgreeValue, StartDate, EndDate, ActualEnd, ContractGroup, ServiceAddress, 
                        Scheduler, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno FROM tblcontract WHERE accountid = @accountid ">
                                      <SelectParameters>
                                          <asp:ControlParameter ControlID="txtclientid" Name="@accountid" PropertyName="Text" />
                                      </SelectParameters>
                                                
                                  </asp:SqlDataSource>
                     </td>     
                        <td>
                    <asp:SqlDataSource ID="SQLDSContractClientIdLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct(tblcontract.ContractNo), tblcontract.Status, tblcontract.RenewalSt, tblcontract.NotedST, tblcontract.Gst,  tblcontract.ContractDate, tblcontract.AccountId, tblcontract.CustName, tblcontract.CustAddr, tblcontract.InchargeId, tblcontract.AgreeValue, tblcontract.StartDate, tblcontract.EndDate, tblcontract.ActualEnd, tblcontract.ContractGroup,
tblcontract.Scheduler, tblcontract.CreatedBy, tblContract.ServiceAddress, tblcontract.CreatedOn, tblcontract.LastModifiedBy, tblcontract.LastModifiedOn, tblcontract.Rcno FROM tblcontract, tblContractDet WHERE tblcontractDet.accountid = @accountid and tblContractDet.LocationId = @LocationId and tblcontract.ContractNo = tblcontractDet.ContractNo ">
                                      <SelectParameters>
                                          <asp:ControlParameter ControlID="txtclientid" Name="@accountid" PropertyName="Text" />
                                          <asp:ControlParameter ControlID="lblAccountIdContactLocation1" Name="@LocationId" PropertyName="Text" />
                                      </SelectParameters>
                                                
                                  </asp:SqlDataSource>
                     </td>     

                               <td>
                                   &nbsp;</td>     
                <td><asp:TextBox ID="txtclientid" runat="server" BorderStyle="None" ForeColor="White"></asp:TextBox></td>
                           </tr>
                                  <tr>
                                      <asp:TextBox ID="txt" runat="server" Height="16px" MaxLength="50" Visible="false" Width="550px"></asp:TextBox>
                                      <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtPopupType" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtcontractfrom" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtRowCount" runat="server" Visible="False"></asp:TextBox>
                                      
                                      <asp:TextBox ID="txtModeRenew" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtRcnoRenew" runat="server" Visible="False"></asp:TextBox>

                              </tr>

        

             <tr style="text-align:center;width:100%">
                <td colspan="10" style="text-align:left;padding-left:5px;">

<asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="0" Font-Names="Calibri" Height="100%" Width="100%" CssClass="ajax__tab_xp" AutoPostBack="True">
    <asp:TabPanel runat="server" HeaderText=" General & Billing Info" ID="TabPanel1"><HeaderTemplate>
Client &amp; Project Info
</HeaderTemplate>
<ContentTemplate>
<table border="0" style="width:75%; margin:auto">
    <caption>
        <br />
<asp:SqlDataSource ID="SqlDSContractNo" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT ContractNo, ProspectId, StaffId, Status, ContactType, CustCode, CustName, CustAddr, InChargeId, EntryDate, ContractDate, Contact, Telephone, Fax, AgreeValue, Duration, DurationMs, StartDate, EndDate, ActualEnd, ServiceNo, ServiceBal, ServiceAmt, HourNo, HourBal, HourAmt, CallNo, CallBal, CallAmt, ResponseHours, CancelCharges, CompensatePct, CompensateMax, WeekDayFrom1, WeekDayFrom2, WeekDayFrom3, WeekDayTo1, WeekDayTo2, WeekDayTo3, WeekEndFrom1, WeekEndFrom2, WeekEndFrom3, WeekEndTo1, WeekEndTo2, WeekEndTo3, DayVisitRate1, DayVisitRate2, DayVisitRate3, DayHourRate1, DayHourRate2, DayHourRate3, DayTransport1, DayTransport2, DayTransport3, EndVisitRate1, EndVisitRate2, EndVisitRate3, EndHourRate1, EndHourRate2, EndHourRate3, EndTransport1, EndTransport2, EndTransport3, Notes, Rcno, Comments, ActualStaff, ServiceNoActual, HourNoActual, CallNoActual, MinDuration, OContractNo, RenewalSt, RenewalDate, UnitNo, UnitBal, UnitAmt, UnitNoActual, NotedSt, NotedDate, settle, ActualServHrs, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, YourReference, ContractGroup, PrintBody, RenewalContractNo, ServDayMethod, ServDay, GSt, ServiceStart, ServiceEnd, ServiceFrequence, TimeIn, TimeOut, WarrantyStart, WarrantyEnd, ContractValue, PerServiceValue, Disc_Percent, DiscAmt, BillingFrequency, DayService1, DayService2, DayService3, DayService4, Support, ScheduleType, TeamID, WebCreateDeviceID, WebCreateSource, WebFlowFrom, WebFlowTo, WebEditSource, WebDeleteStatus, WebLastEditDevice, Postal, LocateGrp, Scheduler, ContactPersonMobile, SalesMan, OurReference, GSTNos, ServiceDescription, PrintingRemarks, Rev, MainContractNo, AmtCompleted, AmtBalance, AllocatedSvcTime, Remarks, QuotePrice, QuoteUnitMS, CompanyGroup, SalesGRP FROM tblcontract WHERE (ContractNo = @contractno)"><FilterParameters>
<asp:ControlParameter ControlID="txtcontractno" Name="ContractNo" PropertyName="Text"></asp:ControlParameter>
</FilterParameters>
<SelectParameters>
<asp:ControlParameter ControlID="txtContractNo" Name="@contractno" PropertyName="Text"></asp:ControlParameter>
</SelectParameters>
</asp:SqlDataSource>


<asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1px" Height="80%" HorizontalAlign="Center" Width="80%">
    <table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" Height="27px" ImageUrl="~/Images/closebutton.png" Width="30px"></asp:ImageButton>


</td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160; 
        <asp:TextBox ID="txtPopUpClient" runat="server" AutoPostBack="True" ForeColor="Gray" Height="16px" MaxLength="50" onblur="WaterMarkClient(this, event);" onfocus="WaterMarkClient(this, event);" Width="400px">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>


<asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Visible="False" Width="24px"></asp:ImageButton>


<asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" OnClick="btnPopUpClientReset_Click" Width="24px"></asp:ImageButton>


</td><td><asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>


</td></tr></table><div style="text-align:center; padding-left: 50px; padding-bottom: 5px;"><div class="AlphabetPager"><asp:Repeater ID="rptClientAlphabets" runat="server" Visible="False"><ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" OnClick="ClientAlphabet_Click" Text='<%#Eval("Value")%>' />
</ItemTemplate>
</asp:Repeater>


</div><br />
        <asp:GridView ID="gvClient" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDSClient" Font-Size="15px" ForeColor="#333333" GridLines="None" Width="99%">
<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
<Columns>
<asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
<controlstyle width="5%"></controlstyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle Width="5%"></ItemStyle>
</asp:CommandField>
<asp:BoundField DataField="AccountID" HeaderText="Account Id" SortExpression="AccountID">
<controlstyle width="8%"></controlstyle>

<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>

<ItemStyle Width="8%"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ID" SortExpression="ID">
<controlstyle cssclass="dummybutton" width="8%"></controlstyle>

<HeaderStyle CssClass="dummybutton" Width="100px"></HeaderStyle>

<ItemStyle CssClass="dummybutton" Width="8%" Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Name" HeaderText="Client Name" SortExpression="Name">
<controlstyle width="30%"></controlstyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle Width="30%" Wrap="True"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
<controlstyle width="30%"></controlstyle>

<HeaderStyle HorizontalAlign="Left" Wrap="False"></HeaderStyle>

<ItemStyle Width="30%" Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Address1">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Mobile">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Email">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="LocateGRP">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CompanyGroup">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddBlock">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddNos">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddFloor">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddUnit">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddStreet">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddBuilding">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddCity">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddState">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddCountry">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AddPostal">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Fax">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Mobile">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Telephone">
<controlstyle cssclass="dummybutton"></controlstyle>

<HeaderStyle CssClass="dummybutton"></HeaderStyle>

<ItemStyle CssClass="dummybutton"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Salesman" HeaderText="Salesman"></asp:BoundField>
<asp:BoundField DataField="Industry" HeaderText="Industry"></asp:BoundField>
</Columns>

<EditRowStyle BackColor="#999999"></EditRowStyle>

<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Names="Calibri" ForeColor="White"></HeaderStyle>

<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

<RowStyle BackColor="#F7F6F3" Font-Names="Calibri" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>

<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<sortedascendingcellstyle backcolor="#E9E7E2"></sortedascendingcellstyle>

<sortedascendingheaderstyle backcolor="#506C8C"></sortedascendingheaderstyle>

<sorteddescendingcellstyle backcolor="#FFFDF8"></sorteddescendingcellstyle>

<sorteddescendingheaderstyle backcolor="#6F8DAE"></sorteddescendingheaderstyle>
</asp:GridView>



        <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT  AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman, Industry from tblCompany where status ='O' order by Name"><FilterParameters>
<asp:ControlParameter ControlID="ddlContactType" Name="ContType" PropertyName="SelectedValue" Type="String"></asp:ControlParameter>
</FilterParameters>
<SelectParameters>
<asp:ControlParameter ControlID="txtContType2" Name="@contType2" PropertyName="Text"></asp:ControlParameter>
<asp:ControlParameter ControlID="txtContType4" Name="@contType4" PropertyName="Text"></asp:ControlParameter>
</SelectParameters>
</asp:SqlDataSource>


</div></asp:Panel>


<table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">Client Details </td></tr>
    
   


        <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Account Type <asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>


</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% ">
                    <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="40.5%"><asp:ListItem Selected="True" Value="COMPANY">CORPORATE</asp:ListItem>
<asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
</asp:DropDownList>


</td></tr>
     
          <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Account Id <asp:Label ID="Label6" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>


</td>
              
              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% ">
         
                  <asp:TextBox ID="txtAccountId" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="200" Width="40%"></asp:TextBox>



             <asp:ImageButton ID="btnClient" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClientClick="ConfirmSelection()" Width="24px"></asp:ImageButton>


<asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlClientClose" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient"></asp:ModalPopupExtender>


</td></tr>
      <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Account Name <asp:Label ID="Label7" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>


</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:TextBox ID="txtCustName" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="200" Width="40%"></asp:TextBox>


</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Our Reference </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtOurRef" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" Width="40%"></asp:TextBox>


</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">Your Reference </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtYourRef" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="25" Width="40%"></asp:TextBox>


</td></td></tr>
        <tr style="width:95%">
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">PO No</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% ">
                <asp:TextBox ID="txtPONo" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="30" Width="40%"></asp:TextBox>



            </td>
        </tr>
        <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">&nbsp;Office Address</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtOfficeAddress" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%"></asp:TextBox>


</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Postal </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtPostal" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="20" Width="40%"></asp:TextBox>


</td></tr>
        <tr style="width:95%">
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Billing Address</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% ">
                <asp:TextBox ID="txtBillingAddress" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="40px" Width="40%" ></asp:TextBox>



            </td>
        </tr>
        <tr style="width:95%">
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Service Address</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% ">
                <asp:TextBox ID="txtServiceAddressCons" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="40px" Width="40%" style="font-size:15px; font-family:Calibri; text-align:left;" ></asp:TextBox>



            </td>
        </tr>
        <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; "><asp:TextBox ID="txtAddress" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="25" Visible="False" Width="40%"></asp:TextBox>


</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:DropDownList ID="ddlLocateGrp" runat="server" AppendDataBoundItems="True" Height="20px" Visible="False" Width="40.5%"><asp:ListItem>--SELECT--</asp:ListItem>
</asp:DropDownList>


</td></tr>
    
    <table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000;  background-color: #C0C0C0;">Project Details </td>
    
    <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right;" class="auto-style2">Project Code</td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%">
        <asp:TextBox ID="txtContractNo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" MaxLength="50" ReadOnly="True" Width="40%"></asp:TextBox>
        </td></tr>
        <tr style="width:95%">
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right;" class="auto-style2">Project Name
                <asp:Label ID="Label57" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%">
                <asp:TextBox ID="txtProjectName" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getNetAmount()" Width="40%"></asp:TextBox>
            </td>
        </tr>
        <tr style="width:95%">
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right;" class="auto-style2">Company Group
                <asp:Label ID="Label12" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%">
                <asp:DropDownList ID="ddlCompanyGrp" runat="server" AppendDataBoundItems="True" Height="20px" Width="40.5%">
                    <asp:ListItem>--SELECT--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">Salesman <asp:Label ID="Label8" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>


</td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:70% "><asp:DropDownList ID="ddlSalesman" runat="server" AppendDataBoundItems="True" Height="20px" Width="40.5%"><asp:ListItem>--SELECT--</asp:ListItem>
</asp:DropDownList>


</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">Scheduler <asp:Label ID="Label9" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>


</td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:70% "><asp:DropDownList ID="ddlScheduler" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="20px" Width="40.5%"><asp:ListItem>--SELECT--</asp:ListItem>
</asp:DropDownList>


</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">Department <asp:Label ID="Label56" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>


</td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:70% "><asp:DropDownList ID="ddlContractGrp" runat="server" AppendDataBoundItems="True" Height="20px" Width="40.5%"><asp:ListItem>--SELECT--</asp:ListItem>
</asp:DropDownList>


</td></tr>
        
        <tr style="width:95%">
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">
        Project Value <asp:Label ID="Label55" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>


</td>
                            <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:20% "><asp:TextBox ID="txtConDetVal" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getNetAmount()" style="text-align:right" Width="40%"></asp:TextBox>


</td></tr>
        
        <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">Agreed Value
            <asp:Label ID="Label58" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
            </td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:20% "><asp:TextBox ID="txtAgreeVal" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getValuePerMonth()" style="text-align:right" Width="40%"></asp:TextBox>


</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">
        Project Start </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>
<asp:TextBox ID="txtContractStart" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" onchange="CalculateContractDates()" Width="90%"></asp:TextBox><asp:CalendarExtender ID="calConStart" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtContractStart" TargetControlID="txtContractStart"></asp:CalendarExtender>
</ContentTemplate>
</asp:UpdatePanel>


</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                <asp:TextBox ID="txtContractDate" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" BorderStyle="None" ForeColor="White" Height="16px" onchange="PopulateOtherDays()" Width="1%"></asp:TextBox>
            </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">End </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtContractEnd" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="ValidateDatesContract()" Width="90%"></asp:TextBox>


<asp:CalendarExtender ID="calConEnd" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtContractEnd" TargetControlID="txtContractEnd"></asp:CalendarExtender>


</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">
        Actual Start </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:UpdatePanel runat="server"><ContentTemplate>
<asp:TextBox ID="txtServStart" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" onchange="ValidateDatesService()" Width="90%"></asp:TextBox><asp:CalendarExtender ID="calServStart" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtServStart" TargetControlID="txtServStart"></asp:CalendarExtender>
</ContentTemplate>
</asp:UpdatePanel>


</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:TextBox ID="txtServStartDay" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="18px" Width="90%"></asp:TextBox>


</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">End </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtServEnd" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="ValidateDatesService()" Width="90%"></asp:TextBox>


<asp:CalendarExtender ID="calServEnd" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtServEnd" TargetControlID="txtServEnd"></asp:CalendarExtender>


</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:TextBox ID="TxtServEndDay" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="18px" Width="90%"></asp:TextBox>


</td></tr>

          <tr style="width:95%">
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">
        Site Address</td>
                            <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:20% "><asp:TextBox ID="txtSiteAddress" runat="server" AutoCompleteType="Disabled" Height="16px"   Width="40%"></asp:TextBox>


</td></tr>

          <tr style="width:95%">
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">
        Street</td>
                            <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:20% "><asp:TextBox ID="txtSiteStreet" runat="server" AutoCompleteType="Disabled" Height="16px"  Width="40%"></asp:TextBox>


</td></tr>

          <tr style="width:95%">
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">
        Building</td>
                            <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:20% "><asp:TextBox ID="txtSiteBuilding" runat="server" AutoCompleteType="Disabled" Height="16px"  Width="40%"></asp:TextBox>


</td></tr>
        <tr style="width:95%">
            <td class="auto-style2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; ">City</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                <asp:DropDownList ID="ddlSiteCity" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSCity" DataTextField="City" DataValueField="City" Width="99%">
                    <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ">&nbsp;</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">State</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                <asp:DropDownList ID="ddlSiteState" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" Width="99%">
                    <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ">&nbsp;</td>
        </tr>
        <tr style="width:95%">
            <td class="auto-style2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; ">Country</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                <asp:DropDownList ID="ddlSiteCountry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSCountry" DataTextField="Country" DataValueField="Country" Width="99%">
                    <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ">&nbsp;</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">Postal</td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                <asp:TextBox ID="txtSitePostal" runat="server" AutoPostBack="True" Height="16px" MaxLength="20" OnTextChanged="txtPostal_TextChanged" Width="98%"></asp:TextBox>
            </td>
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ">&nbsp;</td>
        </tr>
        <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right;" class="auto-style2">
        Project Notes </td><td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:TextBox ID="txtContractNotes" runat="server" AutoCompleteType="Disabled" Font-Names="calibri" Font-Size="15px" Height="40px" MaxLength="500" TextMode="MultiLine" Width="97%"></asp:TextBox>


</td></tr>
    </tr></table><br />
                  
                    
       <table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000;  background-color: #C0C0C0;">Billing Option </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">Retention (%)</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtRetentionPerc" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getRetentionAmount()" style="text-align:right" Width="40%"></asp:TextBox>


&nbsp;&nbsp;<asp:CheckBox ID="chkGenerateCreditNote" runat="server" ForeColor="#003366" Text="Generate Credit Note?" Visible="False"></asp:CheckBox>


</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">Retention Amount</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%;  "><asp:TextBox ID="txtRetentionValue" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:right" Width="40%"></asp:TextBox>


&nbsp;&nbsp;&nbsp; </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right; " class="auto-style2">Retention Release Date</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtRetentionReleaseDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%"></asp:TextBox>


<asp:CalendarExtender ID="txtRetentionReleaseDate_CalendarExtender" runat="server" Enabled="True" Format="MM/dd/yyyy" PopupButtonID="txtRetentionReleaseDate" TargetControlID="txtRetentionReleaseDate"></asp:CalendarExtender>


</td></tr></table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right;" class="auto-style2">
        Remarks</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%"><asp:TextBox ID="txtRemarks" runat="server" AutoCompleteType="Disabled" Font-Names="calibri" Font-Size="15px" Height="40px" MaxLength="4000" TextMode="MultiLine" Width="60%"></asp:TextBox>


</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; text-align:right;" class="auto-style2">Status </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%"><asp:TextBox ID="txtStatus" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="16px" Width="10%"></asp:TextBox>


</td></tr><tr style="width:95%"><td colspan="2" style="text-align:right;"><asp:Button ID="btnSave" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" OnClientClick="return DoValidation()" Text="SAVE" Width="10%"></asp:Button>


<asp:Button ID="btnCancel" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="CANCEL" Width="10%"></asp:Button>


</td></tr></table>
    
    
    <table style="width:75%; margin:auto">
        
        <tr style="width:95%"><td style="width:6%">
        <asp:TextBox ID="txtContType1" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>



        </td><td style="width:6%">
            <asp:TextBox ID="txtContType2" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>



        </td><td style="width:6%">
            <asp:TextBox ID="txtContType3" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>



        </td><td style="width:6%">
            <asp:TextBox ID="txtContType4" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>



        </td><td style="width:6%">  <asp:TextBox ID="txtCreatedOn" runat="server" ForeColor="White" Width="1%" BorderStyle="None"></asp:TextBox>


</td><td>  <asp:TextBox ID="txtContractNoSelected" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>

           </td><td style="width:6%">


</td> <asp:TextBox ID="txtIsPopup" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>
<td style="width:6%">

     <asp:TextBox ID="txtClient" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>

</td><td style="width:6%"><asp:TextBox ID="txtOriginalAccountId" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>


</td><td style="width:6%">
     <asp:TextBox ID="txtAccountIdSelection" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>


</td><td style="width:6%"><asp:TextBox ID="txtRcnoCN" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" style="text-align:right" Width="1%" BorderStyle="None"></asp:TextBox>


</td><td style="width:6%">
     <asp:TextBox ID="txtContVal" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>


</td><td style="width:6%">
     <asp:TextBox ID="txtTotContVal" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>


</td><td style="width:6%"> <asp:TextBox ID="lblAccountID" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>
</td><td style="width:6%"> <asp:TextBox ID="lblName" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>
</td><td style="width:6%"> <asp:TextBox ID="lblContractNo" runat="server" AutoCompleteType="Disabled" ForeColor="White" Height="16px" MaxLength="10" Width="1%" BorderStyle="None"></asp:TextBox>
</td></tr>
        
        
       

    


</ContentTemplate>
</asp:TabPanel>

           
    
              <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
    </asp:TabPanel>
    
            </asp:TabContainer>
                    </td>
                </tr>

                         
                              <tr>
                                  <td  style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      </td>
                                  <td colspan ="2" style="text-align:left; padding-right:2%">
                                     
                                      </td>

                                 
                                 
                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"> 
                                        <asp:Button ID="btnChStatus" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" /></td>
                                  <td style="text-align:left;width:8%">
                                      <asp:Button ID="btnDummy" runat="server" CssClass="roundbutton" Font-Bold="True" Text=" " Width="24px" BackColor="White" BorderStyle="None" />
                                      &nbsp;&nbsp;&nbsp;<asp:Button ID="btnDummyT" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;<asp:Button ID="btnDummyC" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;<asp:Button ID="btnDummyClient" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                                      <asp:Button ID="btnDummyInvoice" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                                  </td>

                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       &nbsp;</td>
                                  <td style="text-align:left;width:10%">
                                      &nbsp;</td>
                                  <td style="text-align: left">
                                      <asp:Button ID="btnDummyClient2" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                                      </td>
                                   <td style="text-align: left">
                                       <asp:Button ID="btnDummyClient3" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" /> 
                                  </td>
                            
                              </tr>

    </table>
          </table>
         </table>
         </table>
         </div>


     
         <div>

    <asp:Panel ID="pnlSearch" runat="server" BackColor="White" Width="85%" Height="95%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto" HorizontalAlign="Center">
              <br /><br />
                     <table  style="width:90%; border:thin;   padding-left:3px; margin-left:auto; margin-right:auto;" >

                            <tr>
                               <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="6">Search</td>
                           </tr>
                    
                       <tr>
                               <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Project No.
                               </td>
                              <td colspan="3" style="text-align:left;padding-left:5px; width:20%">   
                                   <asp:TextBox ID="txtSearchID" runat="server" MaxLength="50" Height="16px" Width="90%"></asp:TextBox>
                            </td> 
                                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Project Name</td>
                              <td  style="width:35%">     
                                  <asp:TextBox ID="txtSearchProjectName" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                            </td>                              
                           </tr>

                         <tr>
                               <td style="width:10%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Client Name</td>
                              <td colspan="3" style="text-align:left;padding-left:5px;width:20%;">  
                                    <asp:TextBox ID="txtSearchCompany" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox>
                            </td>
                                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contact Type</td>
                              <td>
                                  <asp:DropDownList ID="ddlSearchContactType" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                      <asp:ListItem Text="--SELECT--" Value="-1" />
                                      <asp:ListItem>Company</asp:ListItem>
                                      <asp:ListItem>Person</asp:ListItem>
                                  </asp:DropDownList>
                               </td>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Address </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%;">
                                       <asp:TextBox ID="txtSearchAddress" runat="server" Height="16px" MaxLength="100" Width="90%"></asp:TextBox>
                                   </td>
                                          <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account Id</td>
                              <td  style="width:35%">
                                  <asp:TextBox ID="txtSearchCustCode" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:15%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contact Person </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:TextBox ID="txtSearchContact" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                                          <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Status</td>
                              <td>
                                  <asp:DropDownList ID="ddlSearchStatus" runat="server" Width="92%">
                                      <asp:ListItem Value="O">O - Open</asp:ListItem>
                                      <asp:ListItem Value="C">C - Completed</asp:ListItem>
                                      <asp:ListItem Value="H">H - On Hold</asp:ListItem>
                                      <asp:ListItem Value="V">V - Void</asp:ListItem>
                                      <asp:ListItem Value="S">S - Suspenses</asp:ListItem>
                                      <asp:ListItem Value="P">P - Posted</asp:ListItem>
                                  </asp:DropDownList>
                                   </td>

                               </tr>
                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Postal </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:TextBox ID="txtSearchPostal" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contact No</td>
                                   <td colspan="1">
                                       <asp:TextBox ID="txtSearchContactNo" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                               </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Salesman </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:DropDownList ID="ddlSearchSalesman" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                                   <td style="width:12%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">In-charge Id</td>
                                   <td colspan="1" style="width:15%">
                                       <asp:DropDownList ID="ddlSearchInChargeId" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Scheduler </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:DropDownList ID="ddlSearchScheduler" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contract Group</td>
                                   <td colspan="1">
                                       <asp:DropDownList ID="ddlSearchContractGroup" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Our Reference</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:TextBox ID="txtSearchOurRef" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Company Group</td>
                                   <td colspan="1">
                                       <asp:DropDownList ID="ddlSearchCompanyGroup" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>
                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Your Reference</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:TextBox ID="txtSearchYourRef" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Location Group</td>
                                   <td colspan="1">
                                       <asp:DropDownList ID="ddlSearchLocationGroup" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>

                   <tr>
                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Project Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchContractDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateFrom" TargetControlID="txtSearchContractDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchContractDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateTo" TargetControlID="txtSearchContractDateTo"/>
                     </td>
                           

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Start Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchStartDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchStartDateFrom" TargetControlID="txtSearchStartDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchStartDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchStartDateTo" TargetControlID="txtSearchStartDateTo"/>
                     </td>

                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                      <td colspan="1">&nbsp;</td>
                      </tr>

                      <tr>
                        <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">End Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchEndDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEndDateFrom" TargetControlID="txtSearchEndDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchEndDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEndDateTo" TargetControlID="txtSearchEndDateTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Actual Start From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchServiceStartFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchServiceStartFrom" TargetControlID="txtSearchServiceStartFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchServiceStartTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender11" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchServiceStartTo" TargetControlID="txtSearchServiceStartTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Service End From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchServiceEndFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender12" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchServiceEndFrom" TargetControlID="txtSearchServiceEndFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchServiceEndTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender13" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchServiceEndTo" TargetControlID="txtSearchServiceEndTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Actual End From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchActualEndFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender14" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndFrom" TargetControlID="txtSearchActualEndFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchActualEndTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender15" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndTo" TargetControlID="txtSearchActualEndTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >
                                       Entry Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchEntryDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender16" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateFrom" TargetControlID="txtSearchEntryDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; padding-right:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchEntryDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender17" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateTo" TargetControlID="txtSearchEntryDateTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>

                               <tr>
                                   <td colspan="4">
                                       <br />
                                   </td>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td colspan="3" style="text-align:center">
                                       <asp:Button ID="btnSearch" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Search" Width="100px" />
                                       <asp:Button ID="btnClose" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Cancel" Width="100px" />
                                   </td>
                                      <td></td>
                              <td></td>
                               </tr>
                             </tr>

        </table>
           </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnClose" PopupControlID="pnlSearch" TargetControlID="btnDummy" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

         </div>
             


         
    <asp:Panel ID="PnlChSt" runat="server" BackColor="White" Width="50%" Height="50%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto">
              <br />
               <table style="width:100%;padding-left:15px; margin-left:auto; margin-right:auto;">
                 <tr>
                     <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="2">Change Status</td>
                 </tr>
                   <tr>
                       <td colspan="2" style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;">
                           <asp:Label ID="lblMessageStatus" runat="server"></asp:Label>
                       </td>
                   </tr>
                   <tr>
                       <td colspan="2" style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;">
                           <asp:Label ID="lblAlertStatus" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                       </td>
                   </tr>
              <tr>
                              
                        <td style="width:15%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >Status 
                            <asp:Label ID="Label30" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                             </td>
                         
                                 <td colspan="1" style="text-align:left;padding-left:5px;width:80%">  
                                  <asp:DropDownList ID="ddlStatusChSt" runat="server" Width="50%" AutoPostBack="false"  >
                                <asp:ListItem>--SELECT--</asp:ListItem>
                               </asp:DropDownList>
                            </td>                              
                           </tr>


                          <tr>
                              <td style="width:15%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >Actual&nbsp; End 
                                  <asp:Label ID="Label29" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                              </td>
                              <td colspan="1" style="text-align:left;padding-left:5px;width:80%">
                                  <asp:TextBox ID="txtActualEndChSt" runat="server" AutoCompleteType="Disabled" Height="16px" Width="49%"></asp:TextBox>
                                      
                                  <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtActualEndChSt" TargetControlID="txtActualEndChSt" />
                              </td>
                         </tr>
                  
                          <tr>
                              <td class="auto-style6" style="font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Comments</td>
                              <td colspan="1" style="text-align:left;padding-left:5px;width:10%">
                                  <asp:TextBox ID="txtCommentChSt" runat="server" Font-Names="calibri" Font-Size="15px" Height="37px" MaxLength="500" TextMode="MultiLine" Width="49%"></asp:TextBox>
                              </td>
                   </tr>
                          <tr>
                                  
   
                               <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >
                                   &nbsp;</td>
                              <td colspan="1" style="text-align:left;padding-left:5px;width:80%">
                                  <asp:DropDownList ID="ddlRenewalStatus" runat="server" Visible="False" Width="50%">
                                      <asp:ListItem Text="--SELECT--" Value="-1" />
                                      <asp:ListItem Value="O">O - Open</asp:ListItem>
                                      <asp:ListItem Value="R">R - Renewal</asp:ListItem>
                                      <asp:ListItem Value="N">N - No Follow up Needed</asp:ListItem>
                                      <asp:ListItem Value="D">D - Client Declined Renewal</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                             </tr>
                         
                          <tr>
                                  
   
                               <td >
                                   <asp:TextBox ID="txtChangeStatus" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" Width="35%" BorderStyle="None" ForeColor="White" Visible="False"></asp:TextBox>
                                   <br />
                               </td>
                             </tr>
                         
                     <tr><td colspan="1" style="text-align:center" >
                             &nbsp;</td>
                             <td style="text-align:center">
                                 <asp:Button ID="BtnChSt" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Save" Width="100px" OnClientClick="ConfirmChSt()"/>
                                 <asp:Button ID="BtnChStClose" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Cancel" Width="100px" />
                             </td>
                         </tr>
                     

        </table>
           </asp:Panel>

     <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server"  CancelControlID="BtnChStClose" PopupControlID="pnlChSt" TargetControlID="btnChStatus" BackgroundCssClass="modalBackground"  ></asp:ModalPopupExtender>


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
                                   <asp:ListItem Value="C">C - Completed</asp:ListItem>
                                   <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                   <asp:ListItem Value="P">P - Posted</asp:ListItem>   
                                   <asp:ListItem Value="S">S - Suspended</asp:ListItem>  
                                  
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
      <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" CancelControlID="btnStatusCancel" PopupControlID="pnlStatusSearch" TargetControlID="btnDummyClient2" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
            
      
            </div>

         </ContentTemplate>
        
</asp:UpdatePanel>

    
</asp:Content>