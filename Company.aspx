<%@ page title="Corporate" language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" CodeFile="Company.aspx.vb" inherits="Company" enableeventvalidation="false" culture="en-GB" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 <%@ Register Assembly="System.Web" Namespace="System.Web.UI.HtmlControls" TagPrefix="asp" %>--%>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


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
        width:30%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }

     .CellFormat1{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:15%;
        /*table-layout:fixed;
        overflow:hidden;*/
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


    function getCountry() {
       
        var strOffCity = document.getElementById("<%=ddlOffCity.ClientID%>").options[document.getElementById("<%=ddlOffCity.ClientID%>").selectedIndex].text;
        var strBillCity = document.getElementById("<%=ddlBillCity.ClientID%>").options[document.getElementById("<%=ddlBillCity.ClientID%>").selectedIndex].text;
        var strCity = document.getElementById("<%=ddlCity.ClientID%>").options[document.getElementById("<%=ddlCity.ClientID%>").selectedIndex].text;
        var strBillCitySvc = document.getElementById("<%=ddlBillCitySvc.ClientID%>").options[document.getElementById("<%=ddlBillCitySvc.ClientID%>").selectedIndex].text;
       
        if (strOffCity == "SINGAPORE") {                     
            (document.getElementById("<%=ddlOffCountry.ClientID%>").value) = "SINGAPORE";
            }
         if (strBillCity == "SINGAPORE") {
            (document.getElementById("<%=ddlBillCountry.ClientID%>").value) = "SINGAPORE";           
            }
         if (strCity == "SINGAPORE") {
            (document.getElementById("<%=ddlCountry.ClientID%>").value) = "SINGAPORE";           
        }
         if (strBillCitySvc == "SINGAPORE") {
            (document.getElementById("<%=ddlBillCountrySvc.ClientID%>").value) = "SINGAPORE";            
         }
          
        
        if (strOffCity == "KUALA LUMPUR") {
            (document.getElementById("<%=ddlOffCountry.ClientID%>").value) = "MALAYSIA";
         }
         if (strBillCity == "KUALA LUMPUR") {
            (document.getElementById("<%=ddlBillCountry.ClientID%>").value) = "MALAYSIA";
        }
         if (strCity == "KUALA LUMPUR") {
            (document.getElementById("<%=ddlCountry.ClientID%>").value) = "MALAYSIA";
        }
         if (strBillCitySvc == "KUALA LUMPUR") {
            (document.getElementById("<%=ddlBillCountrySvc.ClientID%>").value) = "MALAYSIA";
        }
              
}




    function ConfirmDelete() {
       
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (document.getElementById("<%=txtAccountID.ClientID%>").value != '') {

                if (confirm("Do you wish to DELETE Account ID : " + document.getElementById("<%=txtAccountID.ClientID%>").value + " Name : " + document.getElementById("<%=txtNameE.ClientID%>").value + "?")) {
                    confirm_value.value = "Yes";

                } else {
                    confirm_value.value = "No";
                }

                document.forms[0].appendChild(confirm_value);
            }
        }
    

    function ConfirmDeleteSvc() {
      
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (document.getElementById("<%=txtAccountID.ClientID%>").value != '') {

            if (document.getElementById("<%=txtDescription.ClientID%>").value == '') {
                //   if (confirm("Do you wish to DELETE Location ID : " + document.getElementById("<%=txtLocationID.ClientID%>").value + "\n Service Name : " + document.getElementById("<%=txtServiceName.ClientID%>").value + "?\n Service Address : " + document.getElementById("<%=txtAddress.ClientID%>").value + " " + document.getElementById("<%=txtStreet.ClientID%>").value + " " + document.getElementById("<%=txtBuilding.ClientID%>").value + " " + document.getElementById("<%=ddlCity.ClientID%>").value + " " + document.getElementById("<%=ddlState.ClientID%>").value + " " + document.getElementById("<%=ddlCountry.ClientID%>").value + " " + document.getElementById("<%=txtPostal.ClientID%>").value)) {
                if (confirm("Do you wish to DELETE Location ID : " + document.getElementById("<%=txtLocationID.ClientID%>").value + "\nService Name : " + document.getElementById("<%=txtServiceName.ClientID%>").value + "?\nService Address : " + document.getElementById("<%=txtSvcAddr.ClientID%>").value)) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
            } else {
                if (confirm("Do you wish to DELETE Location ID : " + document.getElementById("<%=txtLocationID.ClientID%>").value + "\nService Name : " + document.getElementById("<%=txtServiceName.ClientID%>").value + "\nDescription : " + document.getElementById("<%=txtDescription.ClientID%>").value + "?\nService Address : " + document.getElementById("<%=txtSvcAddr.ClientID%>").value)) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
            }
            document.forms[0].appendChild(confirm_value);
        }
    }


    function TabChanged(sender, e) {
    //        if (sender.get_activeTabIndex() == 1 || sender.get_activeTabIndex() == 2) {
           
    //           if (document.getElementById("<%=txtAccountID.ClientID%>").value == '') {
    //               $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
    //              alert("Please select a customer record to proceed.");
    //               return;
    //           }
      
    //        }
      
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


    function checkallcontractESM() {
        var table = document.getElementById('<%=GridViewContractESM.ClientID%>');
          var totbillamt = 0;


          if (table.rows.length > 0) {

              var input = table.rows[0].getElementsByTagName("input");

              if (input[0].id.indexOf("chkSelectAllContractESMGV") > -1) {

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



    function checkallinvoiceESM() {
        var table = document.getElementById('<%=GridViewInvoiceESM.ClientID%>');
         var totbillamt = 0;


         if (table.rows.length > 0) {

             var input = table.rows[0].getElementsByTagName("input");

             if (input[0].id.indexOf("chkSelectAllInvoiceESMGV") > -1) {

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

    function currentdatetimestartdate() {
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
        document.getElementById("<%=txtStartDate.ClientID%>").value = dd + "/" + mm + "/" + y;
 
    }

    function UpdateBillingDetails() {

        var updatebilladdress = document.getElementById("<%=chkOffAddr.ClientID%>");   
        if (updatebilladdress.checked == true) {
       
            document.getElementById("<%=txtBillingName.ClientID%>").value = document.getElementById("<%=txtNameE.ClientID%>").value;
            document.getElementById("<%=txtBillAddress.ClientID%>").value=document.getElementById("<%=txtOffAddress1.ClientID%>").value;
            document.getElementById("<%=txtBillStreet.ClientID%>").value=document.getElementById("<%=txtOffStreet.ClientID%>").value;
            document.getElementById("<%=txtBillBuilding.ClientID%>").value=document.getElementById("<%=txtOffBuilding.ClientID%>").value;

            document.getElementById("<%=ddlBillCity.ClientID%>").value=document.getElementById("<%=ddlOffCity.ClientID%>").value;
            document.getElementById("<%=ddlBillState.ClientID%>").value=document.getElementById("<%=ddlOffState.ClientID%>").value;
            document.getElementById("<%=ddlBillCountry.ClientID%>").value=document.getElementById("<%=ddlOffCountry.ClientID%>").value;

            document.getElementById("<%=txtBillPostal.ClientID%>").value = document.getElementById("<%=txtOffPostal.ClientID%>").value;

            document.getElementById("<%=txtBillCP1Contact.ClientID%>").value = document.getElementById("<%=txtOffContactPerson.ClientID%>").value;
            document.getElementById("<%=txtBillCP1Position.ClientID()%>").value = document.getElementById("<%=txtOffPosition.ClientID%>").value;
            document.getElementById("<%=txtBillCP1Mobile.ClientID%>").value = document.getElementById("<%=txtOffMobile.ClientID%>").value;
           
            document.getElementById("<%=txtBillCP1Tel.ClientID()%>").value = document.getElementById("<%=txtOffContactNo.ClientID%>").value;
            document.getElementById("<%=txtBillCP1Fax.ClientID%>").value = document.getElementById("<%=txtOffFax.ClientID%>").value;
            document.getElementById("<%=txtBillCP1Tel2.ClientID%>").value = document.getElementById("<%=txtOffContact2.ClientID%>").value;

            document.getElementById("<%=txtBillCP1Email.ClientID%>").value = document.getElementById("<%=txtOffEmail.ClientID%>").value;

            document.getElementById("<%=txtBillCP2Contact.ClientID%>").value = document.getElementById("<%=txtOffCont1Name.ClientID%>").value;
            document.getElementById("<%=txtBillCP2Position.ClientID%>").value = document.getElementById("<%=txtOffCont1Position.ClientID%>").value;
            document.getElementById("<%=txtBillCP2Tel.ClientID%>").value = document.getElementById("<%=txtOffCont1Tel.ClientID%>").value;
            document.getElementById("<%=txtBillCP2Fax.ClientID%>").value = document.getElementById("<%=txtOffCont1Fax.ClientID%>").value;
            document.getElementById("<%=txtBillCP2Tel2.ClientID%>").value = document.getElementById("<%=txtOffCont1Tel2.ClientID%>").value;
            document.getElementById("<%=txtBillCP2Mobile.ClientID%>").value = document.getElementById("<%=txtOffCont1Mobile.ClientID%>").value;

            document.getElementById("<%=txtBillCP2Email.ClientID%>").value = document.getElementById("<%=txtOffCont1Email.ClientID%>").value;

         }
        }

    function UpdateServiceDetails() {

        var updatebilladdress = document.getElementById("<%=chkSameAddr.ClientID%>");
         if (updatebilladdress.checked == true) {

             document.getElementById("<%=txtServiceName.ClientID%>").value = document.getElementById("<%=txtNameE.ClientID%>").value;
             document.getElementById("<%=txtAddress.ClientID%>").value = document.getElementById("<%=txtOffAddress1.ClientID%>").value;
            document.getElementById("<%=txtStreet.ClientID%>").value = document.getElementById("<%=txtOffStreet.ClientID%>").value;
            document.getElementById("<%=txtBuilding.ClientID%>").value = document.getElementById("<%=txtOffBuilding.ClientID%>").value;

            document.getElementById("<%=ddlCity.ClientID%>").value = document.getElementById("<%=ddlOffCity.ClientID%>").value;
            document.getElementById("<%=ddlState.ClientID%>").value = document.getElementById("<%=ddlOffState.ClientID%>").value;
            document.getElementById("<%=ddlCountry.ClientID%>").value = document.getElementById("<%=ddlOffCountry.ClientID%>").value;

             document.getElementById("<%=txtPostal.ClientID%>").value = document.getElementById("<%=txtOffPostal.ClientID%>").value;
             document.getElementById("<%=txtSvcCP1Contact.ClientID%>").value = document.getElementById("<%=txtOffContactPerson.ClientID%>").value;
             document.getElementById("<%=txtSvcCP1Position.ClientID()%>").value = document.getElementById("<%=txtOffPosition.ClientID%>").value;
             document.getElementById("<%=txtSvcCP1Mobile.ClientID%>").value = document.getElementById("<%=txtOffMobile.ClientID%>").value;
        
            document.getElementById("<%=txtSvcCP1Telephone.ClientID()%>").value = document.getElementById("<%=txtOffContactNo.ClientID%>").value;
            document.getElementById("<%=txtSvcCP1Fax.ClientID%>").value = document.getElementById("<%=txtOffFax.ClientID%>").value;
            document.getElementById("<%=txtSvcCP1Telephone2.ClientID%>").value = document.getElementById("<%=txtOffContact2.ClientID%>").value;

            document.getElementById("<%=txtSvcCP1Email.ClientID%>").value = document.getElementById("<%=txtOffEmail.ClientID%>").value;

            document.getElementById("<%=txtSvcCP2Contact.ClientID%>").value = document.getElementById("<%=txtOffCont1Name.ClientID%>").value;
            document.getElementById("<%=txtSvcCP2Position.ClientID%>").value = document.getElementById("<%=txtOffCont1Position.ClientID%>").value;
            document.getElementById("<%=txtSvcCP2Telephone.ClientID%>").value = document.getElementById("<%=txtOffCont1Tel.ClientID%>").value;
            document.getElementById("<%=txtSvcCP2Fax.ClientID%>").value = document.getElementById("<%=txtOffCont1Fax.ClientID%>").value;
            document.getElementById("<%=txtSvcCP2Tel2.ClientID%>").value = document.getElementById("<%=txtOffCont1Tel2.ClientID%>").value;
            document.getElementById("<%=txtSvcCP2Mobile.ClientID%>").value = document.getElementById("<%=txtOffCont1Mobile.ClientID%>").value;
            document.getElementById("<%=txtSvcCP2Email.ClientID%>").value = document.getElementById("<%=txtOffCont1Email.ClientID%>").value;


        }
    }


    function UpdateServiceBillingDetails() {

        var updateServicebilladdress = document.getElementById("<%=chkMainBillingInfo.ClientID%>");
        if (updateServicebilladdress.checked == true) {

            document.getElementById("<%=txtBillingNameSvc.ClientID%>").value = document.getElementById("<%=txtBillingName.ClientID%>").value;
         
             document.getElementById("<%=txtBillAddressSvc.ClientID%>").value = document.getElementById("<%=txtBillAddress.ClientID%>").value;
            document.getElementById("<%=txtBillStreetSvc.ClientID%>").value = document.getElementById("<%=txtBillStreet.ClientID%>").value;
            document.getElementById("<%=txtBillBuildingSvc.ClientID%>").value = document.getElementById("<%=txtBillBuilding.ClientID%>").value;

            document.getElementById("<%=ddlBillCitySvc.ClientID%>").value = document.getElementById("<%=ddlBillCity.ClientID%>").value;
            document.getElementById("<%=ddlBillStateSvc.ClientID%>").value = document.getElementById("<%=ddlBillState.ClientID%>").value;
            document.getElementById("<%=ddlBillCountrySvc.ClientID%>").value = document.getElementById("<%=ddlBillCountry.ClientID%>").value;

            document.getElementById("<%=txtBillPostalSvc.ClientID%>").value = document.getElementById("<%=txtBillPostal.ClientID%>").value;

            document.getElementById("<%=txtBillContact1Svc.ClientID%>").value = document.getElementById("<%=txtBillCP1Contact.ClientID%>").value;
            document.getElementById("<%=txtBillPosition1Svc.ClientID()%>").value = document.getElementById("<%=txtBillCP1Position.ClientID%>").value;
            document.getElementById("<%=txtBillMobile1Svc.ClientID%>").value = document.getElementById("<%=txtBillCP1Mobile.ClientID%>").value;

            document.getElementById("<%=txtBillTelephone1Svc.ClientID()%>").value = document.getElementById("<%=txtBillCP1Tel.ClientID%>").value;
            document.getElementById("<%=txtBillFax1Svc.ClientID%>").value = document.getElementById("<%=txtBillCP1Fax.ClientID%>").value;
            document.getElementById("<%=txtBillTelephone12Svc.ClientID%>").value = document.getElementById("<%=txtBillCP1Tel2.ClientID%>").value;

            document.getElementById("<%=txtBillEmail1Svc.ClientID%>").value = document.getElementById("<%=txtBillCP1Email.ClientID%>").value;

            document.getElementById("<%=txtBillContact2Svc.ClientID%>").value = document.getElementById("<%=txtBillCP2Contact.ClientID%>").value;
            document.getElementById("<%=txtBillPosition2Svc.ClientID%>").value = document.getElementById("<%=txtBillCP2Position.ClientID%>").value;
            document.getElementById("<%=txtBillTelephone2Svc.ClientID%>").value = document.getElementById("<%=txtBillCP2Tel.ClientID%>").value;
            document.getElementById("<%=txtBillFax2Svc.ClientID%>").value = document.getElementById("<%=txtBillCP2Fax.ClientID%>").value;
            document.getElementById("<%=txtBilltelephone22Svc.ClientID%>").value = document.getElementById("<%=txtBillCP2Tel2.ClientID%>").value;
            document.getElementById("<%=txtBillMobile2Svc.ClientID%>").value = document.getElementById("<%=txtBillCP2Mobile.ClientID%>").value;

            document.getElementById("<%=txtBillEmail2Svc.ClientID%>").value = document.getElementById("<%=txtBillCP2Email.ClientID%>").value;

        }
    }

    function DoValidation() {

        var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency

        var expr1 = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        var valid = true;

        currentdatetime();

   
        document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
        return valid;
    };


    function DoServiceValidation() {

        //var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
        //var valid = true;

      
        //var contactperson1 = document.getElementById("<%=txtSvcCP1Contact.ClientID%>").value;

        //if (contactperson1 == '') {
        //    alert("Please Enter Contact Person-1");
        //    document.getElementById("<%=txtSvcCP1Contact.ClientID%>").focus;
            //    valid = false;
            //    return valid;
            //}
        currentdatetime();
          //return valid;
    };

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
        .auto-style1 {
            font-size: 15px;
            font-weight: bold;
            font-family: Calibri;
            color: black;
            text-align: right;
            width: 30%;
            border-collapse: collapse;
            border-spacing: 0;
            line-height: 10px;
            height: 50px;
        }
        .auto-style2 {
            color: black;
            text-align: left;
            font-family: Calibri;
            height: 50px;
            padding-left: 20px;
        }
        .auto-style3 {
            width: 60%;
            height: 35px;
        }
        .auto-style4 {
            width: 40%;
            height: 35px;
        }
        </style>
    <%--  <link rel="stylesheet" type="text/css" href="CSS/loading-bar.css"/>
<script type="text/javascript" src="JS/loading-bar.js"></script>--%>

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
 
     <asp:UpdatePanel ID="updPanelCompany" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
      

                  <asp:ControlBundle Name="CalendarExtender_Bundle" />
        <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:controlBundle name="ListSearchExtender_Bundle"/>
        <asp:controlBundle name="TabContainer_Bundle"/>
        <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>
        <asp:controlBundle name="MaskedEditExtender_Bundle"/>

   </ControlBundles>
    </asp:ToolkitScriptManager>     


        
       <div style="text-align:center">

     
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">CORPORATE (CUSTOMER)</h3>
       
        <table style="width:100%;text-align:center;">
            <tr>
                <td colspan="8"><br /></td>
            </tr>
            <tr>
               <td style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';" colspan="8"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';" colspan="8"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
            <tr>
                 <td style="width:100%;text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" colspan="8"> 
                      <asp:Label ID="txtMode" runat="server" Text="" Visible="true" CssClass="dummybutton"></asp:Label>
                      </td> 
            </tr>
            <tr>
                <td style="width:100%;text-align:left;" colspan="8"> 
                    <table style="width:100%">
                        <tr>

                    <td style="width:12%;text-align:left;">
                    <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="90px" />
                      
                         <asp:Button ID="btnCopyAdd" runat="server" Font-Bold="True" Text="EDIT" Width="90px"  CssClass="roundbutton1" BackColor="#CFC6C0" visible="TRUE" />
               
                    <asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="90px" OnClientClick = "currentdatetime(); ConfirmDelete()" Visible="False"/>
               
                     <asp:Button ID="btnFilter" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="90px" Visible="True" />
              
                     <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CH. STATUS" Width="92px" />
                  
                   
                         <asp:Button ID="btnContract" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CONTRACT" Width="115px" Visible="False"/>
             
              <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="false" />
                        <asp:Button ID="btnTransactions" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="TRANSACTIONS" Width="120px" Visible="true" />

                            <asp:Button ID="btnUpdateServiceContact" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="UPDATE SERVICE CONTACT" Width="200px" Visible="true" />
                                          <asp:Button ID="btnImportData" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="IMPORT DATA" Width="200px" Visible="FALSE" />
          

      </td>            
                      
                       
                 <td style="width:8%;text-align:right;"> <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px" Height="30px" />
             <asp:Label ID="Label1" Style="color: white" runat="server" Visible="false" Text="Label"></asp:Label> 
                       <asp:TextBox ID="txtWorkBookName" runat="server" CssClass="dummybutton"></asp:TextBox>
                      <asp:TextBox ID="txtFailureCount" runat="server" CssClass="dummybutton"></asp:TextBox>
<asp:TextBox ID="txtSuccessCount" runat="server" CssClass="dummybutton"></asp:TextBox>
<asp:TextBox ID="txtFailureString" runat="server" CssClass="dummybutton"></asp:TextBox>
                       </td>
                        </tr>
                    </table> </td>
            </tr>
            <tr>
     <td style="text-align:right" colspan="8">
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
            <tr class="Centered">
                <td colspan="8">
                    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    
                      <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="auto"  style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"  Visible="true" Width="1330px">
 
                         <asp:GridView ID="GridView1" runat="server" Width="100%" DataSourceID="SqlDataSource1" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="id" Font-Size="15px"  AllowSorting="True" AllowPaging="True" CssClass="Centered" Font-Names="Calibri" RowStyle-CssClass="gridcell" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
       
            <Columns>
     
                  <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                <ControlStyle Width="35px" />
                      <HeaderStyle Wrap="False" />
                <ItemStyle Width="35px" wrap="false"/>
                </asp:CommandField>
               
                    <asp:TemplateField HeaderText="InActive">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkINA" runat="server" Enabled="false" Checked='<%# Eval("Inactive")%>' />
                          </ItemTemplate>
                  </asp:TemplateField>
                
                  <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" ReadOnly="True">
                       <ControlStyle Width="5%"  />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="5%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                   <asp:BoundField DataField="TaxIdentificationNo" HeaderText="TIN">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="SalesTaxRegistrationNo" HeaderText="SST">
                  <ItemStyle Wrap="False" />
                       </asp:BoundField>
                <asp:BoundField DataField="Id" SortExpression="Id" ReadOnly="True">
                       <ControlStyle Width="5%" CssClass="dummybutton" />
                  <HeaderStyle Width="100px" Wrap="False" CssClass="dummybutton" />
                     <ItemStyle width="5%" Wrap="false" CssClass="dummybutton" />
                </asp:BoundField>
                  <asp:BoundField DataField="Rcno">
                  <ControlStyle CssClass="dummybutton" />
                  <HeaderStyle CssClass="dummybutton" />
                  <ItemStyle CssClass="dummybutton" />
                  </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Company Name" SortExpression="Name">
                  <ControlStyle Width="26%" />
                  <HeaderStyle Width="250px" Wrap="False" />
                    <ItemStyle Width="26%" Wrap="False" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="Name2" HeaderText="Name2" SortExpression="Name2" Visible="false" >
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ARCurrency" HeaderText="Currency" SortExpression="ARCurrenct" >
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="Bal" DataFormatString="{0:N2}" SortExpression="Bal" HeaderText="Balance" >
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                
                  <asp:BoundField DataField="Location">
                  <ControlStyle CssClass="dummybutton" />
                  <HeaderStyle Wrap="False" CssClass="dummybutton" />
                  <ItemStyle Wrap="False" CssClass="dummybutton" />
                  </asp:BoundField>
                
                 <asp:TemplateField HeaderText="Office Address" SortExpression="Address1">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox2" runat="server"  Text='<%# Eval("Address1") & " " & Eval("AddStreet") & " " & Eval("AddBuilding") & " " & Eval("AddCity") & " " & Eval("AddState") & " " & Eval("AddCountry")%>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label3" runat="server" Text='<%# Eval("Address1") & " " & Eval("AddStreet") & " " & Eval("AddBuilding") & " " & Eval("AddCity") & " " & Eval("AddState") & " " & Eval("AddCountry")%>'></asp:Label> 
                      </ItemTemplate>
                      <HeaderStyle Font-Bold="True" Width="250px" />
                      <ItemStyle Font-Names="Calibri" HorizontalAlign="Left" Width="250px" Wrap="False" />
                     </asp:TemplateField>

                  <asp:BoundField DataField="AddPostal" HeaderText="Office Postal" SortExpression="AddPostal" >
                
                  <ItemStyle Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
                
                 <asp:TemplateField HeaderText="Billing Address" SortExpression="BillAddress1">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("BillAddress1") & " " & Eval("BillStreet") & " " & Eval("BillBuilding") & " " & Eval("BillCity") & " " & Eval("BillState") & " " & Eval("BillCountry")%>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label2" runat="server" Text='<%# Eval("BillAddress1") & " " & Eval("BillStreet") & " " & Eval("BillBuilding") & " " & Eval("BillCity") & " " & Eval("BillState") & " " & Eval("BillCountry")%>'></asp:Label>
                     </ItemTemplate>
                     <HeaderStyle Font-Bold="True" Width="250px" />
                     <ItemStyle Font-Names="Calibri" HorizontalAlign="Left" Width="250px" Wrap="False" />
                  </asp:TemplateField>

                  <asp:BoundField DataField="BillPostal" HeaderText="Bill Postal" SortExpression="BillPostal" >
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone" >
                  <ItemStyle Wrap="False" HorizontalAlign="Left"/>
                  </asp:BoundField>
                  <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" >
                  <ItemStyle Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
                       <ControlStyle Width="15%" />
                  <HeaderStyle Width="180px" Wrap="False" />
                       <ItemStyle Width="15%" Wrap="False" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="BillContactPerson" HeaderText="BillContactPerson" SortExpression="BillContactPerson" Visible="False" />
                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup" Visible="true" >
                       <ControlStyle Width="2%" />
                  <HeaderStyle Width="50px" Wrap="True" />
                        <ItemStyle Width="2%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                               <asp:BoundField DataField="ARTERM" HeaderText="Terms" SortExpression="ARTERM" >
                       <ControlStyle Width="3%" />
                  <HeaderStyle Width="80px" Wrap="False" />
                                   <ItemStyle Width="3%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="SalesMan" SortExpression="SalesMan">
                       <ControlStyle Width="10%" CssClass="dummybutton" />
                  <HeaderStyle Width="150px" Wrap="False" CssClass="dummybutton" />
                      <ItemStyle Width="10%" Wrap="false" HorizontalAlign="Left" CssClass="dummybutton" />
                </asp:BoundField>
                   <asp:BoundField DataField="Industry" HeaderText="Industry" SortExpression="Industry" >
                       <ControlStyle Width="17%" />
                  <HeaderStyle Width="180px" Wrap="False" />
                       <ItemStyle Width="17%" Wrap="False" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" >
                      <HeaderStyle Wrap="False" />
                   <ItemStyle Width="5%" Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
                 
                <asp:BoundField DataField="AddBlock" HeaderText="AddBlock" SortExpression="AddBlock" Visible="false" >
                </asp:BoundField>
                <asp:BoundField DataField="AddNos" HeaderText="AddNos" SortExpression="AddNos" Visible="false" />
                <asp:BoundField DataField="AddFloor" HeaderText="AddFloor" SortExpression="AddFloor" Visible="false" />
                
                 <asp:BoundField DataField="AddUnit" HeaderText="AddUnit" SortExpression="AddUnit" Visible="false" />
                 <asp:BoundField DataField="AddBuilding" HeaderText="AddBuilding" SortExpression="AddBuilding" Visible="false" />
                 <asp:BoundField DataField="AddStreet" HeaderText="AddStreet" SortExpression="AddStreet" Visible="false" />
                
                  <asp:BoundField DataField="AddState" HeaderText="AddState" SortExpression="AddState" Visible="false" />
                  <asp:BoundField DataField="AddCity" HeaderText="AddCity" SortExpression="AddCity" Visible="false" />
                  <asp:BoundField DataField="AddCountry" HeaderText="AddCountry" SortExpression="AddCountry" Visible="false" />
                  <asp:BoundField DataField="BillingAddress" HeaderText="BillingAddress" SortExpression="BillingAddress" Visible="false" />
                  <asp:BoundField DataField="BillBlock" HeaderText="BillBlock" SortExpression="BillBlock" Visible="false" />
                  <asp:BoundField DataField="BillNos" HeaderText="BillNos" SortExpression="BillNos" Visible="false" />
                  <asp:BoundField DataField="BillFloor" HeaderText="BillFloor" SortExpression="BillFloor" Visible="false" />
                  <asp:BoundField DataField="BillUnit" HeaderText="BillUnit" SortExpression="BillUnit" Visible="false" />
                  <asp:BoundField DataField="BillBuilding" HeaderText="BillBuilding" SortExpression="BillBuilding" Visible="false" />
                  <asp:BoundField DataField="BillStreet" HeaderText="BillStreet" SortExpression="BillStreet" Visible="false" />
                  <asp:BoundField DataField="BillState" HeaderText="BillState" SortExpression="BillState" Visible="false" />
                  <asp:BoundField DataField="BillCity" HeaderText="BillCity" SortExpression="BillCity" Visible="false" />
                  <asp:BoundField DataField="BillCountry" HeaderText="BillCountry" SortExpression="BillCountry" Visible="false" />
                  <asp:BoundField DataField="ArCurrency" HeaderText="ArCurrency" SortExpression="ArCurrency" Visible="false" />
                  <asp:BoundField DataField="SalesMan" HeaderText="SalesMan" SortExpression="SalesMan" Visible="false" />
                  <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                 <asp:TemplateField HeaderText="AutoEmailInvoice">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkAutoEmail" runat="server" Enabled="false" Checked='<%# Eval("AutoEmailInvoice")%>' />
                          </ItemTemplate>
                  </asp:TemplateField>
                 <asp:BoundField DataField="UnSubscribeAutoEmailDate" HeaderText="UnSubscribeDate" SortExpression="UnSubscribeAutoEmailDate" >
                  <ItemStyle Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" >
                  <ItemStyle Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" >
                  <ItemStyle Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
                  <asp:BoundField DataField="LastModifiedOn" HeaderText="EditedOn" SortExpression="LastModifiedOn" >
                  <ItemStyle Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
                

                   <asp:TemplateField ><ItemTemplate> <asp:Button ID="btnEditHistory" runat="server" OnClick="btnEditHistory_Click" Text="Edit History" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top"   Width="100px"   />
                                               </ItemTemplate></asp:TemplateField>


            </Columns>
            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                                            <PagerStyle ForeColor="White" HorizontalAlign="Left" BackColor="#507CD1" />
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
            <tr >
              
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lblCurrent" runat="server" Text="Current"></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lbl1To30" runat="server" Text="1 - 30"></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lbl31To60" runat="server" Text="31 - 60" ></asp:Label> 
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                     <asp:Label ID="lbl61To90" runat="server" Text="61 - 90" ></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                     <asp:Label ID="lbl91To180" runat="server" Text="91 - 180" ></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                     <asp:Label ID="lblMoreThan180" runat="server" Text="More Than 180" ></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lblTotalSum" runat="server" Text="Total" ></asp:Label></td>
                  <td style="text-align:left;color:red;font-weight:bold; width:100px;"></td>
            </tr>
            <tr >
                
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lblCurrentVal" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lbl1To30Val" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lbl31To60Val" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lbl61To90Val" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                     <asp:Label ID="lbl91To180Val" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lblMoreThan180Val" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                     <asp:Label ID="lblTotalVal" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align:left;color:red;font-weight:bold; width:100px;">&nbsp;</td>
            </tr>
            <caption>
                <br />
                <br />
                <caption>
                    <br />
                    <caption>
                        <br />
                        <tr style="text-align:center;width:100%">
                            <td colspan="8" style="text-align:center;padding-left:20px;">
                                <%--$(document).ready(function () { $("#ddlIndustry").select2(); });--%></td>
                        </tr>
                        <tr style="text-align:center;width:100%">
                            <td colspan="8" style="text-align:center;padding-left:20px;">
                                <asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="1" AutoPostBack="True" CssClass="ajax__tab_xp" Font-Names="Calibri" Height="100%" OnClientActiveTabChanged="TabChanged" Width="100%">
                                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText=" General &amp; Billing Info">
                                        <HeaderTemplate>General &amp; Billing Info</HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="width:38%"><asp:Label ID="Label67" runat="server" Text="Master Branch"></asp:Label><asp:Label ID="Label66" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" Visible="False"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="20%" AutoPostBack="True"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat" style="width:38%">Account ID<asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" Visible="False"></asp:Label></td><td class="CellTextBox"><asp:TextBox ID="txtAccountID" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="10" Width="20%"></asp:TextBox></td></tr><tr><td class="CellFormat">Status</td><td class="CellTextBox"><asp:DropDownList ID="ddlStatus" runat="server" CssClass="chzn-select" Width="20%"><asp:ListItem Selected="True" Value="O">O - Open</asp:ListItem></asp:DropDownList><asp:CheckBox ID="chkInactive" runat="server" CssClass="CellFormat" Text=" Inactive" /></td></tr><tr style="display:none"><td class="CellFormat">Company Group<asp:Label ID="Label22" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlCompanyGrp" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Height="25px" Width="20%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td></tr><tr><td class="CellFormat">Corporate Name<asp:Label ID="Label23" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:TextBox ID="txtNameE" runat="server" AutoPostBack="True" Height="16px" MaxLength="200" Width="62%"></asp:TextBox></td></tr><tr><td class="CellFormat">Old Name</td><td class="CellTextBox"><asp:TextBox ID="txtNameO" runat="server" Height="16px" MaxLength="200" Width="62%"></asp:TextBox></td></tr><tr><td class="CellFormat">Company Registration No<asp:Label ID="Label24" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:TextBox ID="txtRegNo" runat="server" Height="16px" MaxLength="20" Width="20%"></asp:TextBox></td></tr><tr><td class="CellFormat">GST Registration No</td><td class="CellTextBox"><asp:TextBox ID="txtGSTRegNo" runat="server" Height="16px" MaxLength="20" Width="20%"></asp:TextBox></td></tr>
                                                <tr>
                                            <td class="CellFormat">Tax Identification No (TIN)</td>
                                            <td class="CellTextBox">
                                                <asp:TextBox ID="txtTIN" runat="server" Height="16px" MaxLength="20" Width="20%"></asp:TextBox>
                                           <asp:Button ID="btnSearchTIN" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SEARCH TIN" Visible="TRUE" Width="200px" onclick="btnSearchTIN_Click" />
                                                          <asp:TextBox ID="txtTINOld" runat="server" Height="16px" MaxLength="20" Width="20%" class="dummybutton"></asp:TextBox>
                                
                                                        </td>
                                        </tr>
                                        <tr>
                                            <td class="CellFormat">SalesTax Registration No (SST)</td>
                                            <td class="CellTextBox">
                                                <asp:TextBox ID="txtSST" runat="server" Height="16px" MaxLength="20" Width="20%"></asp:TextBox>
                                            </td>
                                        </tr>
                                                <tr><td class="CellFormat">Website</td><td class="CellTextBox"><asp:TextBox ID="txtWebsite" runat="server" Height="16px" MaxLength="50" Width="62%"></asp:TextBox></td></tr><tr><td class="CellFormat">Customer Since</td><td class="CellTextBox"><asp:TextBox ID="txtStartDate" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtStartDate" TargetControlID="txtStartDate"></asp:CalendarExtender></td></tr><tr><td class="CellFormat">Industry<asp:Label ID="Label25" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlIndustry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="industry" DataValueField="industry" Height="25px" Width="20%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td></tr><tr style="display:none"><td class="CellFormat">Salesman<asp:Label ID="Label26" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlSalesMan" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="StaffId" DataValueField="StaffId" Height="25px" Width="20%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td></tr><tr style="display:none"><td class="CellFormat">Person Incharge</td><td class="CellTextBox"><asp:DropDownList ID="ddlIncharge" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="inchargeId" DataValueField="inchargeId" Height="25px" Width="20%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td></tr><tr><td class="CellFormat">Comments</td><td class="CellTextBox" rowspan="2"><asp:TextBox ID="txtComments" runat="server" Font-Names="Calibri" Height="40px" MaxLength="2000" TextMode="MultiLine" Width="62%"></asp:TextBox></td></tr><tr><td></td></tr><tr><td colspan="2"><asp:Panel ID="pnlOffAddrName" runat="server"><table class="Centered" style="padding-top:5px;width:60%"><tr><td><br /></td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;"><asp:ImageButton ID="imgCollapsible1" runat="server" ImageAlign="Bottom" ImageUrl="~/Images/plus.png" />Office Address</td></tr></table></asp:Panel><asp:Panel ID="pnlOffAddr" runat="server"><table class="Centered" style="padding-top:5px;width:60%"><tr><td class="CellFormat">Street Address1 </td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtOffAddress1" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="94%" AutoPostBack="True"></asp:TextBox></td></tr><tr><td class="CellFormat">Street Address2</td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtOffStreet" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="94%"></asp:TextBox></td></tr><tr><td class="CellFormat">&nbsp;Building &amp; Unit No. </td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtOffBuilding" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="94%"></asp:TextBox></td></tr><tr><td class="CellFormat">City<asp:Label ID="Label55" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlOffCity" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="City" DataValueField="City" onchange="UpdateBillingDetails()" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellFormat1">State </td><td class="CellTextBox"><asp:DropDownList ID="ddlOffState" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" onchange="UpdateBillingDetails()" Width="84%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat">Country<asp:Label ID="Label58" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlOffCountry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Country" DataValueField="Country" onchange="UpdateBillingDetails()" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellFormat1">Postal<asp:Label ID="Label70" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>

</td><td class="CellTextBox"><asp:TextBox ID="txtOffPostal" runat="server" Height="16px" MaxLength="20" onchange="UpdateBillingDetails()" Width="84%"></asp:TextBox></td></tr><tr><td colspan="4"><br /></td></tr><tr><td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">Office Contact Info</td></tr><tr><td class="CellFormat">Contact Person 1</td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtOffContactPerson" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox></td><td class="CellFormat1">Position</td><td class="CellTextBox"><asp:TextBox ID="txtOffPosition" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="84%"></asp:TextBox></td></tr><tr><td class="CellFormat">Office Telephone</td><td class="CellTextBox"><asp:TextBox ID="txtOffContactNo" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox></td><td class="CellFormat1">Office Fax</td><td class="CellTextBox"><asp:TextBox ID="txtOffFax" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="84%"></asp:TextBox></td></tr><tr><td class="CellFormat">Office Telephone2</td><td class="CellTextBox"><asp:TextBox ID="txtOffContact2" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox></td><td class="CellFormat1">Mobile</td><td class="CellTextBox"><asp:TextBox ID="txtOffMobile" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="84%"></asp:TextBox></td></tr><tr><td class="CellFormat">Office Email</td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtOffEmail" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="94%"></asp:TextBox><a href='<%= Me.ResolveUrl("mailto:" + txtOffEmail.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;"><img height="20" src="Images/email1.png" width="20" /></a></td></tr><tr><td colspan="4"><br /></td></tr><tr><td class="CellFormat">Contact Person 2</td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtOffCont1Name" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox></td><td class="CellFormat1">Position</td><td class="CellTextBox"><asp:TextBox ID="txtOffCont1Position" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="84%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone</td><td class="CellTextBox"><asp:TextBox ID="txtOffCont1Tel" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox></td><td class="CellFormat1">Fax</td><td class="CellTextBox"><asp:TextBox ID="txtOffCont1Fax" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="84%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone2</td><td class="CellTextBox"><asp:TextBox ID="txtOffCont1Tel2" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox></td><td class="CellFormat1">Mobile</td><td class="CellTextBox"><asp:TextBox ID="txtOffCont1Mobile" runat="server" Height="16px" MaxLength="50" onchange="UpdateBillingDetails()" Width="84%"></asp:TextBox></td></tr><tr><td class="CellFormat">Email</td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtOffCont1Email" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="94%"></asp:TextBox><a href='<%= Me.ResolveUrl("mailto:" + txtOffCont1Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;"><img height="20" src="Images/email1.png" width="20" /></a></td></tr></table></asp:Panel><asp:CollapsiblePanelExtender ID="cpnl1" runat="server" CollapseControlID="pnlOffAddrName" CollapsedImage="~/Images/plus.png" CollapsedText="Click to show" Enabled="True" ExpandControlID="pnlOffAddrName" ExpandedImage="~/Images/minus.png" ImageControlID="imgCollapsible1" TargetControlID="pnlOffAddr"></asp:CollapsiblePanelExtender></td></tr><tr><td colspan="2"><asp:CollapsiblePanelExtender ID="cpnl2" runat="server" CollapseControlID="pnlBillAddrName" CollapsedImage="~/Images/plus.png" CollapsedText="Click to show" Enabled="True" ExpandControlID="pnlBillAddrName" ExpandedImage="~/Images/minus.png" ImageControlID="imgCollapsible2" TargetControlID="pnlBillAddr"></asp:CollapsiblePanelExtender><asp:Panel ID="pnlBillAddrName" runat="server"><table class="Centered" style="padding-top:5px;width:60%"><tr><td><br /></td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;"><asp:ImageButton ID="imgCollapsible2" runat="server" ImageAlign="Bottom" ImageUrl="~/Images/plus.png" />Main Billing Address</td></tr></table></asp:Panel><asp:Panel ID="pnlBillAddr" runat="server"><table class="Centered" style="padding-top:5px;width:60%"><tr><td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left;"><asp:Button ID="btnUpdateBilling" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Height="27px" Text="UPDATE BILLING INFO" Width="180px" /></td><td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:right;"><asp:CheckBox ID="chkOffAddr" runat="server" Font-Names="Calibri" Font-Underline="False" onclick="UpdateBillingDetails()" Text="Same as Office Address" /></td></tr><tr><td class="CellFormat">Billing Name<asp:Label ID="Label18" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtBillingName" runat="server" Height="16px" MaxLength="100" Width="94%"></asp:TextBox></td></tr><tr><td class="CellFormat">Street Address1<asp:Label ID="Label17" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtBillAddress" runat="server" Height="16px" MaxLength="100" Width="94%"></asp:TextBox></td></tr><tr><td class="CellFormat">Street Address2</td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtBillStreet" runat="server" Height="16px" MaxLength="100" Width="94%"></asp:TextBox></td></tr><tr><td class="CellFormat">&nbsp;Building &amp; Unit No.&nbsp; </td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtBillBuilding" runat="server" Height="16px" MaxLength="100" Width="94%"></asp:TextBox></td></tr><tr><td class="CellFormat">City </td><td class="CellTextBox"><asp:DropDownList ID="ddlBillCity" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="City" DataValueField="City" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellFormat1">State </td><td class="CellTextBox"><asp:DropDownList ID="ddlBillState" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" Width="84%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat">Country<asp:Label ID="Label59" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlBillCountry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Country" DataValueField="Country" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellFormat1">Postal<asp:Label ID="Label71" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>

</td><td class="CellTextBox">
    <asp:TextBox ID="txtBillPostal" runat="server" Height="16px" MaxLength="20" Width="84%"></asp:TextBox></td></tr>
    <tr><td colspan="4"><br /></td></tr>
    <tr><td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">Billing Contact Info</td></tr>
    <tr><td class="CellFormat">Contact Person 1<asp:Label ID="Label19" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
        <td class="CellTextBox" colspan="1"><asp:TextBox ID="txtBillCP1Contact" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
        <td class="CellFormat1">Position</td>
        <td class="CellTextBox"><asp:TextBox ID="txtBillCP1Position" runat="server" Height="16px" MaxLength="100" Width="84%"></asp:TextBox></td></tr>
    
    <tr><td class="CellFormat">Telephone</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP1Tel" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
        <td class="CellFormat1">Fax</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP1Fax" runat="server" Height="16px" MaxLength="50" Width="84%"></asp:TextBox></td></tr>
    <tr><td class="CellFormat">Telephone2</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP1Tel2" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
        <td class="CellFormat1">Mobile</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP1Mobile" runat="server" Height="16px" MaxLength="50" Width="84%"></asp:TextBox></td></tr>
    
    <tr><td class="CellFormat">Email</td>
        <td class="CellTextBox" colspan="3">
            <asp:TextBox ID="txtBillCP1Email" runat="server" Height="16px" MaxLength="500" Width="94%"></asp:TextBox>
            <a href='<%= Me.ResolveUrl("mailto:" + txtBillCP1Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;">
                <img height="20" src="Images/email1.png" width="20" /></a></td></tr><tr><td colspan="4"><br /></td>

                                                                                    </tr>
    
    <tr><td class="CellFormat">Contact Person 2</td>
        <td class="CellTextBox" colspan="1">
            <asp:TextBox ID="txtBillCP2Contact" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
        <td class="CellFormat1">Position</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP2Position" runat="server" Height="16px" MaxLength="100" Width="84%"></asp:TextBox></td>

    </tr>
    <tr><td class="CellFormat">Telephone</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP2Tel" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
        <td class="CellFormat1">Fax</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP2Fax" runat="server" Height="16px" MaxLength="50" Width="84%"></asp:TextBox></td></tr>
    
    <tr><td class="CellFormat">Telephone2</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP2Tel2" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
        <td class="CellFormat1">Mobile</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtBillCP2Mobile" runat="server" Height="16px" MaxLength="50" Width="84%"></asp:TextBox></td></tr>
    
    <tr><td class="auto-style1">Email</td>
        <td class="auto-style2" colspan="3">
            <asp:TextBox ID="txtBillCP2Email" runat="server" Height="16px" MaxLength="500" Width="94%"></asp:TextBox>
            <a href='<%= Me.ResolveUrl("mailto:" + txtBillCP2Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;">
                <img height="20" src="Images/email1.png" width="20" /></a></td></tr><tr><td colspan="4"><br /></td></tr>
    
    <tr>
        <td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Billing Options</td></tr>
    
    <tr><td class="CellFormat">Credit Limit</td>
        <td class="CellTextBox">
            <asp:TextBox ID="txtCreditLimit" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td>
        <td class="CellTextBox1" style="text-align:left">&nbsp;</td></tr>
    
    <tr><td class="CellFormat">Credit Terms<asp:Label ID="Label9" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
        <td class="CellTextBox"><asp:DropDownList ID="ddlTerms" runat="server" AppendDataBoundItems="True" Height="25px" Width="99%">
            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td>
        <td class="CellTextBox1" style="text-align: left">
            <asp:ImageButton ID="btnEditSendStatement" runat="server" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr>
    
    <tr><td class="CellFormat">Currency<asp:Label ID="Label10" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
        <td class="CellTextBox"><asp:DropDownList ID="ddlCurrency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSCurrency" DataTextField="Currency" DataValueField="Currency" Height="25px" Width="99%">
            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr>
    
    <tr><td class="CellFormat">Default Invoice Format</td><td class="CellTextBox">
        <asp:DropDownList ID="ddlDefaultInvoiceFormat" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Width="99%">
            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
            <asp:ListItem>Format1</asp:ListItem>
            <asp:ListItem>Format2</asp:ListItem>
            <asp:ListItem>Format3</asp:ListItem>
            <asp:ListItem>Format4</asp:ListItem>
            <asp:ListItem>Format5</asp:ListItem>
            <asp:ListItem>Format6</asp:ListItem>
            <asp:ListItem>Format7</asp:ListItem>
            <asp:ListItem>Format8</asp:ListItem></asp:DropDownList></td>
        <td class="CellTextBox"></td></tr>
    
    <tr><td class="CellFormat">Send HardCopy</td>
        <td class="CellTextBox" colspan="2">
            <asp:CheckBox ID="chkSendStatementInv" runat="server" Text="Invoice" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>&#160;&#160;&#160; 
            <asp:CheckBox ID="chkSendStatementSOA" runat="server" Text="SOA (Statement of Accounts)" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/></td></tr>
    
    <tr><td class="CellFormat">Auto Email</td>
        <td class="CellTextBox" colspan="3"><asp:CheckBox ID="chkAutoEmailInvoice" runat="server" Text="Invoice" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>&nbsp; 
                                        <asp:Label ID="lblAutoEmailInv" runat="server" ForeColor="Black" Text="-" Font-Italic="True"></asp:Label>&#160;&#160;&#160; <asp:CheckBox ID="chkAutoEmailStatement" runat="server" Text="SOA (Statement of Accounts)" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>&nbsp;<asp:Label ID="lblAutoEmailSOA" runat="server" ForeColor="Black" Text="-" Font-Italic="True"></asp:Label></td></tr>
    
    <tr><td class="CellFormat">Requires E-Billing</td>
        <td class="CellTextBox" colspan="2"><asp:CheckBox ID="chkRequireEBilling" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/></td></tr>
    
    <tr><td class="CellFormat">Remarks</td>
        <td class="CellTextBox" rowspan="2" colspan="3">
            <asp:TextBox ID="txtBillingOptionRemarks" runat="server" Font-Names="Calibri" Height="40px" MaxLength="1000" TextMode="MultiLine" Width="100%"></asp:TextBox></td></tr><tr><td colspan="4"><br /></td></tr><tr><td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Notification</td></tr><tr><td class="CellFormat"></td><td class="CellTextBox" colspan="2"><asp:CheckBox ID="chkEmailNotifySchedule" runat="server" Text="Email Notification for Schedule" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/></td></tr><tr><td class="CellFormat"></td><td class="CellTextBox" colspan="2"><asp:CheckBox ID="chkEmailNotifyJobProgress" runat="server" Text="Email Notification for Job Progress" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/></td></tr><tr><td colspan="4"><br /></td></tr><tr><td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Service Reports</td></tr><tr><td class="CellFormat"></td><td class="CellTextBox" colspan="2"><asp:CheckBox ID="chkPhotosMandatory" runat="server" Text="Photos are Mandatory" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/></td></tr><tr><td class="CellFormat"></td><td class="CellTextBox" colspan="2"><asp:CheckBox ID="chkDisplayTimeInTimeOut" runat="server" Text="Display Time In and Time Out in the Service Report" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/></td></tr><tr style="display:none"><td class="CellFormat">Location<asp:Label ID="Label62" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"></td><td class="CellFormat"></td></tr><tr><td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">&nbsp;</td></tr><tr><td class="CellFormat" colspan="4" style="text-align: left; padding-right: 0%; padding-left: 30%;"><asp:RadioButtonList ID="rdbBillingSettings" runat="server" CausesValidation="True" CellPadding="5" CellSpacing="3" Height="63px" Visible="False" Width="100%"><asp:ListItem Selected="True" Value="AccountID">Generate Billing by Account ID</asp:ListItem><asp:ListItem Value="LocationID">Generate Billing by Location ID</asp:ListItem><asp:ListItem Value="ContractNo">Generate Billing by Contract No</asp:ListItem><asp:ListItem Value="ServiceLocationCode">Generate Billing by Service Location Code</asp:ListItem></asp:RadioButtonList></td></tr></table></asp:Panel></td></tr><tr><td colspan="2"><br /></td></tr><tr><td colspan="2" style="text-align:right;"><asp:Button ID="btnSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="if(this.value === 'Saving...') { return false; } else { this.value = 'Saving...'; }; currentdatetime();" Text="SAVE" Width="100px" /><asp:Button ID="btnCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>
                                            <div style="text-align:center">
                                                <asp:LinkButton ID="btnTop" runat="server" Font-Bold="True" Font-Names="Calibri" ForeColor="Brown">Go to Top</asp:LinkButton>

                                            </div></ContentTemplate></asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Service Location">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblServiceLocationCount" runat="server" Font-Size="11px" Text="Service Location"></asp:Label></HeaderTemplate>
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtLocatonNo" runat="server" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtLocationPrefix" runat="server" Visible="False"></asp:TextBox>
                                            <table style="padding-top:5px;width:100%;"><tr><td style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" colspan="4"><asp:Label ID="txtSvcMode" runat="server" CssClass="dummybutton"></asp:Label></td></tr><tr style="vertical-align: middle"><td class="auto-style3" style="text-align:left;" colspan="4"><asp:Button ID="btnSvcAdd" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="ADD" Width="90px" /><asp:Button ID="btnSvcCopy" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="COPY" Width="90px" /><asp:Button ID="btnSvcEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Width="90px" /><asp:Button ID="btnSvcDelete" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime(); ConfirmDeleteSvc()" Text="DELETE" Width="90px" /><asp:Button ID="btnSvcContract" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CONTRACT" Width="115px" /><asp:Button ID="btnSvcService" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SERVICE" Width="110px" /><asp:Button ID="btnTransactionsSvc" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="TRANSACTIONS" Visible="False" Width="120px" /><asp:Button ID="btnTransfersSvc" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CHANGE ACCOUNT" Width="140px" /><asp:Button ID="btnSpecificLocation" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SPECIFIC LOCATION" Width="185px" />
                                        <asp:Button ID="btnChangeStatusLoc" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CH. STATUS" Width="92px" />
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </td><td class="auto-style4" style="text-align:right;vertical-align: middle"><asp:ImageButton ID="btnReset" runat="server" Height="25px" ImageUrl="~/Images/reset1.png" ToolTip="RESET SEARCH" Width="25px" /><asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" ForeColor="Gray" Height="25px" onblur="WaterMark(this, event);" onfocus="WaterMark(this, event);" Text="Search Here for Location Address, Postal Code or Description" Width="370px"></asp:TextBox><asp:Button ID="btnGoSvc" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Height="30px" Text="GO" Width="50px" /><asp:ImageButton ID="btnSvcSearch" runat="server" Height="22px" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnSvcSearch_Click" Visible="False" Width="24px" /><asp:TextBox ID="txtSearchText" runat="server" CssClass="dummybutton"></asp:TextBox></td></tr>
                                        
                                        <tr><td style="text-align:left;width:20%"><asp:Label ID="Label12" runat="server" CssClass="CellFormat" Text="View Records"></asp:Label><asp:DropDownList ID="ddlView1" runat="server" AutoPostBack="True"><asp:ListItem Selected="True">5</asp:ListItem><asp:ListItem>10</asp:ListItem><asp:ListItem>25</asp:ListItem><asp:ListItem>50</asp:ListItem><asp:ListItem>100</asp:ListItem><asp:ListItem>200</asp:ListItem><asp:ListItem>300</asp:ListItem></asp:DropDownList></td>
                                            <td class="CellFormat" style="text-align: right; width: 20%"><asp:Label ID="lblContractGroup" runat="server" CssClass="CellFormat" Text="Search By Contract Group"></asp:Label></td>
                                            <td style="text-align: left; width: 20%">
                                        
                                                 <cc1:dropdowncheckboxes ID="ddlContractGroup" runat="server" AddJQueryReference="True" DataSourceID="SqlDSContractGroup" DataTextField="ContractGroup" DataValueField="ContractGroup" RepeatDirection="Horizontal" UseButtons="False" UseSelectAllNode="True" Width="80.5%">
                                <Style DropDownBoxBoxHeight="120px" DropDownBoxBoxWidth="80.5%" SelectBoxCssClass="" SelectBoxWidth="80.5%" />
                                <Style2 DropDownBoxBoxHeight="120px" DropDownBoxBoxWidth="80.5%" SelectBoxCssClass="" SelectBoxWidth="80.5%" />
                                <Texts SelectAllNode="Select all" />
                    
                                                </cc1:dropdowncheckboxes>


                                                   

                                                

                                            </td>
                                            <td style="text-align: left; width: 20%">
                                                
                                            </td>
                                        <td colspan="1"><asp:TextBox ID="txtSelectedIndex" runat="server" AutoCompleteType="Disabled" Height="16px" style="padding-right: 0%" Visible="False" Width="10%"></asp:TextBox><br /></td></tr><tr><td colspan="5" style="text-align:center">
                                        
                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Centered" DataKeyNames="Rcno" DataSourceID="SqlDataSource2" Font-Size="15px" ForeColor="#333333" GridLines="Vertical" OnRowDataBound="OnRowDataBound2" OnSelectedIndexChanged="OnSelectedIndexChanged2" PageSize="5">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                                                    <controlstyle width="40px" />
                                                    <ItemStyle Width="40px" />

                                                </asp:CommandField>
                                                   <asp:TemplateField HeaderText="Active">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkStLoc" runat="server" Enabled="false" Checked='<%# Eval("Status")%>' />
                          </ItemTemplate>
                  </asp:TemplateField>
                                                <asp:BoundField DataField="LocationID" HeaderText="Location ID" SortExpression="LocationID">
                                                    <controlstyle width="15%" /><ItemStyle Wrap="False" />

                                                </asp:BoundField>
                                                <asp:BoundField DataField="CompanyID" HeaderText="Client ID" SortExpression="CompanyID">
                                                    <controlstyle width="15%" />
                                                    <ItemStyle Wrap="False" /></asp:BoundField>
                                                <asp:BoundField DataField="ServiceZone" HeaderText="Service Zone" />
                                                 <asp:BoundField DataField="ServiceArea" HeaderText="Service Area" />
                                                <asp:TemplateField HeaderText="Address" SortExpression="Address1">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Address1") & " " & Eval("AddStreet") & " " & Eval("AddBuilding") & " " & Eval("AddCity") & " " & Eval("AddState") & " " & Eval("AddCountry")%>'></asp:TextBox>

                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Address1") & " " & Eval("AddStreet") & " " & Eval("AddBuilding") & " " & Eval("AddCity") & " " & Eval("AddState") & " " & Eval("AddCountry")%>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Width="250px" />
                                                    <ItemStyle Font-Names="Calibri" Width="250px" />

                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AddPostal" HeaderText="Postal" SortExpression="AddPostal" />
                                                 <asp:BoundField DataField="CompanyGroupD" HeaderText="Company Group" />
                                                <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" />
                                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                                                <asp:BoundField DataField="ContactPerson" HeaderText="Service ContactPerson1" SortExpression="ContactPerson">
                                                    <HeaderStyle Width="120px" />

                                                </asp:BoundField>
                                                <asp:BoundField DataField="ContactPerson2" HeaderText="Service ContactPerson2" SortExpression="ContactPerson2">
                                                    <controlstyle cssclass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" Width="120px" />
                                                    <ItemStyle CssClass="dummybutton" /></asp:BoundField>
                                                <asp:BoundField DataField="BillContact1Svc" HeaderText="Bill ContactPerson1" SortExpression="BillContact1Svc">
                                                    <HeaderStyle Width="120px" /></asp:BoundField>
                                                <asp:BoundField DataField="BillContact2Svc" HeaderText="Bill ContactPerson2" SortExpression="BillContact2Svc">
                                                    <controlstyle cssclass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" Width="120px" />
                                                    <ItemStyle CssClass="dummybutton" /></asp:BoundField>
                                                <asp:BoundField DataField="Comments" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" /></asp:BoundField>
                                                <asp:BoundField DataField="Description" SortExpression="Description">
                                                    <controlstyle width="250px" CssClass="dummybutton" />
                                                    <HeaderStyle Width="250px" CssClass="dummybutton" />
                                                    <ItemStyle Width="250px" CssClass="dummybutton" /></asp:BoundField>
                                                <asp:BoundField DataField="Location" HeaderText="Branch" SortExpression="Location" />
                                                <asp:BoundField DataField="BranchID" HeaderText="BranchID" SortExpression="BranchID" Visible="False" />
                                                <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone" Visible="False" />
                                                <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" Visible="False" />
                                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" Visible="False" />
                                                <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                                    <EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>

                                                    </EditItemTemplate>
                                                    <ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>

                                                    </ItemTemplate></asp:TemplateField>
                                                <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                                                <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" />
                                                <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                <asp:BoundField DataField="LastModifiedOn" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                                                <asp:BoundField DataField="AddBlock" HeaderText="AddBlock" SortExpression="AddBlock" Visible="False" />
                                                <asp:BoundField DataField="AddNos" HeaderText="AddNos" SortExpression="AddNos" Visible="False" />
                                                <asp:BoundField DataField="AddFloor" HeaderText="AddFloor" SortExpression="AddFloor" Visible="False" />
                                                <asp:BoundField DataField="AddUnit" HeaderText="AddUnit" SortExpression="AddUnit" Visible="False" />
                                                <asp:BoundField DataField="AddBuilding" HeaderText="AddBuilding" SortExpression="AddBuilding" Visible="False" />
                                                <asp:BoundField DataField="AddStreet" HeaderText="AddStreet" SortExpression="AddStreet" Visible="False" />
                                                <asp:BoundField DataField="AddCity" HeaderText="AddCity" SortExpression="AddCity" Visible="False" />
                                                <asp:BoundField DataField="AddState" HeaderText="AddState" SortExpression="AddState" Visible="False" />
                                                <asp:BoundField DataField="AddCountry" HeaderText="AddCountry" SortExpression="AddCountry" Visible="False" />
                                                <asp:BoundField DataField="AddPostal" HeaderText="AddPostal" SortExpression="AddPostal" Visible="False" />
                                                <asp:BoundField DataField="LocateGrp" HeaderText="LocateGrp" SortExpression="LocateGrp" Visible="False" />
                                                <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" Visible="False" />
                                                <asp:BoundField DataField="AccountNo"><controlstyle cssclass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField>

                                            </Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><sortedascendingcellstyle backcolor="#F5F7FB" /><sortedascendingheaderstyle backcolor="#6D95E1" /><sorteddescendingcellstyle backcolor="#E9EBEF" /><sorteddescendingheaderstyle backcolor="#4870BE" /></asp:GridView><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblcompanylocation WHERE AccountID = @AccountID order by locationid"><SelectParameters><asp:ControlParameter ControlID="txtAccountIDtab2" Name="AccountID" PropertyName="Text" Type="String" /></SelectParameters></asp:SqlDataSource></td></tr><tr>
                                        <td colspan="4">
                                        <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup"></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="SqlDSContractGroup0" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT contractgroup FROM tblcontractgroup order by contractgroup"></asp:SqlDataSource>
                                        <br /></td></tr></table>
                                            <table border="0" class="Centered" style="padding-top:5px;width:40%;">
                                            <tr><td  style="width:8% ; font-size:15px;  font-weight:bold;  font-family:'Calibri'; color:red; text-align:right; ">Last Service Done : </td>
                                                <td   colspan="1" style="width:5%; font-size:15px; font-family:'Calibri';  color:red;  text-align:left;" ><asp:Label ID="lblLastServiceDone" runat="server"  Height="18px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label></td>
                                                <td style="width:9%; font-size:15px;  font-weight:bold;  font-family:'Calibri';    color:red; text-align:right;">Next Scheduled Service : </td><td  colspan="1" style="width: 5%; font-size:15px; font-family:'Calibri';  color:red;  text-align:left;"><asp:Label ID="lblNextScheduledService" runat="server"  Height="18px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label></td></tr>

                                            </table>
                                        
                                        <table class="Centered" style="padding-top:5px;width:55%;">
                                            <tr><td colspan="9"><asp:TextBox ID="txtAccountIDtab2" runat="server" Enabled="False" Height="16px" MaxLength="10" ReadOnly="True" Visible="False" Width="20%"></asp:TextBox></td></tr>
                                            <tr><td class="CellFormat">Account ID </td><td class="CellTextBox" colspan="8"><asp:Label ID="lblAccountID" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" ReadOnly="True" Width="100%"></asp:Label></td></tr>
                                            <tr><td class="CellFormat">Corporate Name </td>
                                                <td class="CellTextBox" colspan="8"><asp:Label ID="lblName" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" ReadOnly="True" Width="100%"></asp:Label></td></tr>
                                            <tr><td colspan="9"><br /></td></tr>
                                            <tr><td colspan="3" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Service Address</td>
                                                <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: #800000; text-align: left; text-decoration: underline; padding-left: 0%" colspan="3">&nbsp;</td>
                                                <td colspan="3" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:right;">
                                                    <asp:CheckBox ID="chkSameAddr" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Underline="False" onclick="UpdateServiceDetails()" Text="Same as Office Address" /></td></tr>
                                            <tr><td class="CellFormat"><asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label>
                                                <asp:Label ID="Label64" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
                                                <td class="CellTextBox" colspan="8">
                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="30%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr>
                                            <tr><td class="CellFormat">Status </td>
                                                <td class="CellTextBox" colspan="8"><asp:CheckBox ID="chkStatusLoc" runat="server" Enabled="False" /></td></tr>
                                        
                                               <tr><td class="CellFormat" colspan="2">SMART Site  <asp:CheckBox ID="chkSmartCustomer" runat="server" />                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                

                                        </td>
                                                <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black; text-align:right; width:26%;">Operating Hours From</td>
                                                <td style="font-size:15px;font-family:'Calibri';color:black; text-align:left; padding-left:6px; width:5%"><asp:TextBox ID="txtBusinessHoursStart" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="65px"></asp:TextBox><asp:MaskedEditExtender ID="txtBusinessHoursStart_MaskedEditExtender" runat="server" AutoComplete="False" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtBusinessHoursStart"></asp:MaskedEditExtender></td>
                                                <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black; text-align:right; width:2%;">To</td>
                                                <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black; text-align:right; width:2%;">&nbsp;</td>
                                                <td style="font-size:15px;font-family:'Calibri';color:black; text-align:left; padding-left:6px;"><asp:TextBox ID="txtBusinessHoursEnd" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="65px"></asp:TextBox><asp:MaskedEditExtender ID="txtBusinessHoursEnd_MaskedEditExtender" runat="server" AutoComplete="False" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtBusinessHoursEnd"></asp:MaskedEditExtender></td>
                                                <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black; text-align:center; width:50%"><asp:CheckBox ID="chkExcludePIRDatainBusinessHours" runat="server" Text="Exclude SMART Data during Operating Hours" />

                                                </td></tr>
                                            
                                            <tr><td class="CellFormat">Service Location ID </td>
                                                <td class="CellTextBox" colspan="8"><asp:TextBox ID="txtLocationID" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="93%"></asp:TextBox></td></tr>
                                            <tr><td class="CellFormat">ClientID (View Only) </td>
                                                <td class="CellTextBox" colspan="8"><asp:TextBox ID="txtClientID" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="93%"></asp:TextBox></td></tr>
                                            <tr><td class="CellFormat">Service Location Sub-Group</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtServiceLocationGroup" runat="server" Height="16px" MaxLength="100" ToolTip="'Account Code' field in Previous System" Width="93%"></asp:TextBox></td></tr>
                                            <tr><td class="CellFormat">Company Group<asp:Label ID="Label61" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="8"><asp:DropDownList ID="ddlCompanyGrpD" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Height="25px" Width="93%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr>
                                            <tr><td class="CellFormat">Contract Group<asp:Label ID="Label33" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="8"><asp:DropDownList ID="ddlContractGrp" runat="server" AppendDataBoundItems="True" Height="20px" Width="93%"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList>&nbsp;<asp:ImageButton ID="btnEditContractGroup" runat="server" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr>
                                            <tr><td class="CellFormat">Service Name<asp:Label ID="Label3" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtServiceName" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td></tr>
                                            <tr><td class="CellFormat">
                                        Service Zone</td>
                                            <td class="CellTextBox" colspan="8">
                                            <asp:TextBox ID="txtServiceZone" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox>
                                        </td></tr>
                                              <tr><td class="CellFormat">
                                        Service Area</td>
                                            <td class="CellTextBox" colspan="8">
                                            <asp:TextBox ID="txtServiceArea" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox>
                                        </td></tr>
                                        <tr>
                                            <td class="CellFormat">Site Name</td>
                                            <td class="CellTextBox" colspan="8">
                                                <asp:TextBox ID="txtSiteName" runat="server" Height="16px" MaxLength="200" Width="93%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr><td class="CellFormat">Comments</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtCommentsSvc" runat="server" Height="16px" MaxLength="300" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">Description </td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtDescription" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">Street Address 1<asp:Label ID="Label30" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtAddress" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox><asp:TextBox ID="txtSvcAddr" runat="server" CssClass="dummybutton"></asp:TextBox></td></tr><tr><td class="CellFormat">Street Address 2</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtStreet" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">&nbsp;Building &amp; Unit No.&nbsp;</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtBuilding" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">City<asp:Label ID="Label54" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>&nbsp;</td><td class="CellTextBox" colspan="2"><asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="City" DataValueField="City" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellFormat1" colspan="3">State </td><td class="CellTextBox" colspan="2"><asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" Width="80%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat">Country<asp:Label ID="Label29" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="2"><asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Country" DataValueField="Country" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellFormat1" colspan="3">Postal&nbsp;</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtPostal" runat="server" AutoPostBack="True" Height="16px" MaxLength="20" OnTextChanged="txtPostal_TextChanged" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">
                                        Location Group</td><td class="CellTextBox" colspan="2"><asp:DropDownList ID="ddlLocateGrp" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="locationgroup" DataValueField="locationgroup" Height="25px" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellTextBox" colspan="3">&nbsp;</td><td class="CellFormat1" colspan="2"></td><td></td></tr><tr><td class="CellFormat">Industry<asp:Label ID="Label27" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="8"><asp:DropDownList ID="ddlIndustrysvc" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select" DataTextField="UPPER(industry)" DataValueField="UPPER(industry)" Height="25px" Width="93%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat">Market Segment ID</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtMarketSegmentIDsvc" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">Person In Charge</td><td class="CellTextBox" colspan="8"><asp:DropDownList ID="ddlInchargeSvc" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="inchargeId" DataValueField="inchargeId" Height="25px" Width="93%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat">Salesman<asp:Label ID="Label65" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="8"><asp:DropDownList ID="ddlSalesManSvc" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="StaffId" DataValueField="StaffId" Height="25px" Width="93%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList>&nbsp;<asp:ImageButton ID="btnEditSalesman" runat="server" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr><tr><td class="CellFormat">Terms</td><td class="CellTextBox" colspan="8"><asp:DropDownList ID="ddlTermsSvc" runat="server" AppendDataBoundItems="True" Height="25px" Width="93%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat">&nbsp;</td><td class="CellTextBox" colspan="8">&nbsp;</td></tr><tr><td colspan="9"><asp:TextBox ID="txtAccountCode" runat="server" Height="16px" MaxLength="100" Visible="False" Width="10%"></asp:TextBox><br /></td></tr><tr><td colspan="9" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Service Contact Info</td></tr><tr><td class="CellFormat">Contact Person 1<asp:Label ID="Label21" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP1Contact" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Position</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP1Position" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP1Telephone" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Fax</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP1Fax" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone2</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP1Telephone2" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Mobile</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP1Mobile" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Email</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtSvcCP1Email" runat="server" Font-Names="calibri" Font-Size="15px" Height="50px" MaxLength="500" TextMode="MultiLine" Width="93%"></asp:TextBox><a href='<%= Me.ResolveUrl("mailto:" + txtSvcCP1Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a></td></tr><tr><td colspan="9"><br /></td></tr><tr><td class="CellFormat">Contact Person 2</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP2Contact" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox></td><td class="CellTextBox" colspan="1"></td><td class="CellFormat1" colspan="2">Position</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP2Position" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP2Telephone" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Fax</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP2Fax" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone2</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP2Tel2" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Mobile</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtSvcCP2Mobile" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Email (Maximum 100 Chars.)</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtSvcCP2Email" runat="server" Font-Names="calibri" Font-Size="15px" Height="50px" MaxLength="100" TextMode="MultiLine" Width="93%"></asp:TextBox><a href='<%= Me.ResolveUrl("mailto:" + txtSvcCP2Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;"><img height="20" src="Images/email1.png" width="20" /></a></td></tr><tr><td class="CellFormat">Email CC</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtSvcEmailCC" runat="server" Font-Names="calibri" Font-Size="15px" Height="50px" MaxLength="500" TextMode="MultiLine" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">&nbsp;</td><td class="CellTextBox" colspan="8"><asp:CheckBox ID="chkSendEmailNotificationOnly" runat="server" Font-Names="Calibri" Font-Underline="False" onclick="UpdateServiceBillingDetails()" Text="Send Email Notification to View Service Report in Customer Portal" /></td></tr><tr><td colspan="9"><br /></td></tr><tr><td colspan="3" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Billing Address</td><td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: #800000; text-align: left; text-decoration: underline; padding-left: 0%" colspan="3">&nbsp;</td><td colspan="3" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:right;"><asp:CheckBox ID="chkMainBillingInfo" runat="server" Font-Names="Calibri" Font-Underline="False" onclick="UpdateServiceBillingDetails()" Text="Same as Main Billing Informaion " /></td></tr><tr><td class="CellFormat">Billing Name<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtBillingNameSvc" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">Street Address 1</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtBillAddressSvc" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox><asp:TextBox ID="txtSvcAddrSvc" runat="server" CssClass="dummybutton"></asp:TextBox></td></tr><tr><td class="CellFormat">Street Address 2</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtBillStreetSvc" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">Building &amp; Unit No.&nbsp; </td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtBillBuildingSvc" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td></tr><tr><td class="CellFormat">City </td><td class="CellTextBox" colspan="2"><asp:DropDownList ID="ddlBillCitySvc" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="City" DataValueField="City" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellFormat1" colspan="3">State </td><td class="CellTextBox" colspan="2"><asp:DropDownList ID="ddlBillStateSvc" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" Width="80%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat">Country<asp:Label ID="Label60" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="2"><asp:DropDownList ID="ddlBillCountrySvc" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Country" DataValueField="Country" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td><td class="CellFormat1" colspan="3">Postal<asp:Label ID="Label72" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>

</td>
                                                <td class="CellTextBox" colspan="2">
                                                    <asp:TextBox ID="txtBillPostalSvc" runat="server" AutoPostBack="True" Height="16px" MaxLength="20" OnTextChanged="txtPostal_TextChanged" Width="80%"></asp:TextBox></td></tr>
                                                <tr><td class="CellFormat">Inactive</td>
                                                    <td class="CellTextBox" colspan="2">
                                                        <asp:CheckBox ID="chkInactiveD" runat="server" CssClass="CellFormat" Text=" " /></td>
                                                    <td class="CellTextBox" colspan="3">&nbsp;</td>
                                                    <td class="CellFormat1" colspan="2">&nbsp;</td>
                                                    <td class="CellTextBox">&nbsp;</td></tr>
                                                
                                                <tr><td colspan="9"><br /></td></tr>
                                                <tr><td colspan="9" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Billing Contact Info</td></tr>
                                                <tr><td class="CellFormat">Contact Person 1<asp:Label ID="Label20" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBillContact1Svc" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Position</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBillPosition1Svc" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBillTelephone1Svc" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Fax</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBillFax1Svc" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone2</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBilltelephone12Svc" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Mobile</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBillMobile1Svc" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Email</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtBillEmail1Svc" runat="server" Font-Names="calibri" Font-Size="15px" Height="50px" MaxLength="500" TextMode="MultiLine" Width="93%"></asp:TextBox><a href='<%= Me.ResolveUrl("mailto:" + txtBillEmail1Svc.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a></td></tr>
                                                <tr><td colspan="2" class="CellFormat">Send Service Report to Billing Contact Person 1</td>
                                                    <td class="CellTextBox" colspan="7">
                                                        <asp:CheckBox ID="chkServiceReportSendTo1" runat="server" /></td></tr>
                                                <tr><td colspan="9"><br /></td></tr>
                                                <tr><td class="CellFormat">Contact Person 2</td>
                                                    <td class="CellTextBox" colspan="2">
                                                        <asp:TextBox ID="txtBillContact2Svc" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox></td>
                                                    <td class="CellFormat1" colspan="3">Position</td>
                                                    <td class="CellTextBox" colspan="2">
                                                        <asp:TextBox ID="txtBillPosition2Svc" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox></td></tr>
                                                <tr><td class="CellFormat">Telephone</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBillTelephone2Svc" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Fax</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBillFax2Svc" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Telephone2</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBilltelephone22Svc" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox></td><td class="CellFormat1" colspan="3">Mobile</td><td class="CellTextBox" colspan="2"><asp:TextBox ID="txtBillMobile2Svc" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat">Email</td><td class="CellTextBox" colspan="8"><asp:TextBox ID="txtBillEmail2Svc" runat="server" Font-Names="calibri" Font-Size="15px" Height="50px" MaxLength="100" TextMode="MultiLine" Width="93%"></asp:TextBox><a href='<%= Me.ResolveUrl("mailto:" + txtBillEmail2Svc.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a></td></tr><tr><td class="CellFormat">Send Service Report to Billing Contact Person 2</td><td class="CellTextBox" colspan="8"><asp:CheckBox ID="chkServiceReportSendTo2" runat="server" /></td></tr><tr><td class="CellFormat">Default Invoice Format</td><td class="CellTextBox" colspan="8">
                                                    <asp:DropDownList ID="ddlSvcDefaultInvoiceFormat" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Width="30%">
                                                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem>Format1</asp:ListItem>
                                                        <asp:ListItem>Format2</asp:ListItem>
                                                        <asp:ListItem>Format3</asp:ListItem>
                                                        <asp:ListItem>Format4</asp:ListItem>
                                                        <asp:ListItem>Format5</asp:ListItem>
                                                        <asp:ListItem>Format6</asp:ListItem>
                                                        <asp:ListItem>Format7</asp:ListItem>
                                                        <asp:ListItem>Format8</asp:ListItem>
                                                        <asp:ListItem>Format9</asp:ListItem>
                                                        <asp:ListItem>Format10</asp:ListItem>
                                                        <asp:ListItem>Format11</asp:ListItem>
                                                        <asp:ListItem>Format12</asp:ListItem>
                                                    </asp:DropDownList></td></tr><tr><td colspan="9"><br /></td></tr><tr><td colspan="9" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Service Reports</td></tr><tr><td class="CellFormat">Photos are Mandatory</td><td class="CellTextBox" colspan="8"><asp:CheckBox ID="chkSvcPhotosMandatory" runat="server" /></td></tr><tr><td colspan="9" style="text-align:right;"><asp:Button ID="btnSvcSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="if(this.value === 'Saving...') { return false; } else { this.value = 'Saving...'; }; currentdatetime();" Text="SAVE" Width="100px" /><asp:Button ID="btnSvcCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" /><asp:TextBox ID="txtSvcRcno" runat="server" Visible="False"></asp:TextBox></td></tr></table>
                                            <div style="text-align:center"><asp:LinkButton ID="btnTopDetail" runat="server" Font-Bold="True" Font-Names="Calibri" ForeColor="Brown">Go to Top</asp:LinkButton></div></div>

                                        </ContentTemplate>

                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="File Upload">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblFileUploadCount" runat="server" Font-Size="11px" Text="File Upload"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width:100%;height:1000px">
                                                <tr style="height:30%"><td>
                                                    <table class="centered" style="text-align:center;width:100%;padding-top:10px;">
                                                        <tr>
                                                            <td style="text-align:left">
                                                                <asp:Button ID="btnTransferFiles" runat="server" Text="TRANSFER" class="roundbutton" visible="false" />
                                                            </td>
                                                        </tr>
                                                        <tr><td class="CellFormat">Account ID </td>
                                                            <td class="CellTextBox">
                                                                <asp:Label ID="lblAccountID2" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label>

                                                            </td></tr>
                                                        <tr><td class="CellFormat">Corporate Name </td>
                                                            <td class="CellTextBox"><asp:Label ID="lblName2" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label>

                                                            </td></tr>
                                                        <tr><td><asp:TextBox ID="txtDeleteUploadedFile" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
                                                            <asp:TextBox ID="txtFileLink" runat="server" AutoCompleteType="Disabled" AutoPostBack="false" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox><br /></td></tr>
                                                        <tr><td class="CellFormat">Select File to Upload </td>
                                                            <td class="CellTextBox" colspan="1" style="text-align:center">
                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="Centered" Width="100%" /></td></tr>
                                                        <tr><td class="CellFormat">Description </td>
                                                            <td class="CellTextBox" colspan="1" style="text-align:left">
                                                                <asp:TextBox ID="txtFileDescription" runat="server" Width="70%"></asp:TextBox>

                                                            </td></tr>
                                                        <tr><td class="centered" colspan="2">
                                                            <asp:Button ID="btnUpload" runat="server" CssClass="roundbutton1" OnClientClick="currentdatetime()" Text="Upload" /></td></tr>
                                                        <tr><td><br /></td></tr>
                                                        <tr><td colspan="2">
                                                            <asp:GridView ID="gvUpload" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="false" CssClass="Centered" DataSourceID="SqlDSUpload" EmptyDataText="No files uploaded" Width="90%">
                                                            <Columns><asp:BoundField DataField="FileName" HeaderText="File Name" /><asp:BoundField DataField="FileDescription" HeaderText="File Description" /><asp:BoundField DataField="CreatedOn" DataFormatString="{0:d}" HeaderText="UploadDate" SortExpression="CreatedOn" /><asp:BoundField DataField="CreatedBy" HeaderText="UploadedBy" SortExpression="CreatedBy" /><asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="PreviewFile" Text="Preview" /></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DownloadFile" Text="Download"></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DeleteFile" Text="Delete" /></ItemTemplate></asp:TemplateField></Columns><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" Font-Names="Calibri" ForeColor="#8C4510" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><sortedascendingcellstyle backcolor="#E4E4E4" /><sortedascendingheaderstyle backcolor="#000066" /><sorteddescendingcellstyle backcolor="#E4E4E4" /><sorteddescendingheaderstyle backcolor="#000066" /></asp:GridView><asp:SqlDataSource ID="SqlDSUpload" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblfileupload where fileref = 'aa'"></asp:SqlDataSource></td></tr></table></td></tr><tr style="height:800px;width:100%"><td style="height:100%;width:100%"><br /><iframe id="iframeid" runat="server" height="600" style="width:100%;height:100%"></iframe></td></tr></table></ContentTemplate></asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Notes"><HeaderTemplate><asp:Label ID="lblNotesCount" runat="server" Font-Size="11px" Text="Notes"></asp:Label></HeaderTemplate><ContentTemplate><div style="text-align:center"><h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Notes</h3><table style="width:100%;text-align:center;"><tr><td colspan="2" style="text-align:left;"><asp:Button ID="btnAddNotesMaster" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="ADD" Width="100px" /><asp:Button ID="btnEditNotesMaster" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Visible="True" Width="100px" /><asp:Button ID="btnDeleteNotesMaster" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="Confirm(); currentdatetime()" Text="DELETE" Width="100px" /><asp:Button ID="btn" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="PRINT" Visible="False" Width="100px" /><asp:Button ID="btnQuitNotesMaster" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CLOSE" Visible="false" Width="100px" /></td></tr><tr><td colspan="2"><br /></td></tr><tr class="Centered">
                                        <td colspan="2">
                                            <asp:GridView ID="gvNotesMaster" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSNotesMaster" Font-Size="15px" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnRowDataBound="OnRowDataBoundgNotes" OnSelectedIndexChanged="OnSelectedIndexChangedgNotes"><AlternatingRowStyle BackColor="White" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false"><ControlStyle Width="150px" /><ItemStyle Width="150px" /></asp:CommandField><asp:BoundField DataField="StaffID" HeaderText="StaffID" SortExpression="StaffID"><ControlStyle Width="150px" /><HeaderStyle Font-Size="12pt" /><ItemStyle HorizontalAlign="Left" Width="150px" /></asp:BoundField><asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes"><ControlStyle Width="300px" /><HeaderStyle Font-Size="12pt" /><ItemStyle HorizontalAlign="Left" Width="300px" /></asp:BoundField><asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EntryDate" SortExpression="CreatedOn" Visible="true" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="CreatedBy" HeaderText="EntryStaff" SortExpression="CreatedBy" Visible="true" /><asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" /><asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" /></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00ccff" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat">KeyField </td><td class="CellTextBox"><asp:Label ID="lblNotesKeyField" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="40%"></asp:Label></td></tr><tr><td class="CellFormat">StaffID </td><td class="CellTextBox"><asp:Label ID="lblNotesStaffID" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="40%"></asp:Label></td></tr><tr><td class="CellFormat">Notes<asp:Label ID="Label8" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*" visible="true"></asp:Label></td><td class="CellTextBox"><asp:TextBox ID="txtNotes" runat="server" Height="60px" MaxLength="50" TextMode="MultiLine" Width="80%"></asp:TextBox></td></tr><tr><td colspan="2" style="text-align:right"><asp:Button ID="btnSaveNotesMaster" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" /><asp:Button ID="btnCancelNotesMaster" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table><asp:SqlDataSource ID="SqlDSNotesMaster" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblnotes where Keyfield = 'aa'"></asp:SqlDataSource></div><asp:TextBox ID="txtNotesRcNo" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtNotesMode" runat="server" CssClass="dummybutton"></asp:TextBox></ContentTemplate></asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Notes"><HeaderTemplate>Customer Portal Access</HeaderTemplate><ContentTemplate><div style="text-align:center"><h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Client Access</h3><table style="width:100%;text-align:center;"><tr><td colspan="2" style="text-align:left;"><asp:Button ID="btnAddCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="ADD" Width="100px" /><asp:Button ID="btnEditCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Width="100px" /><asp:Button ID="btnChStCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="Confirm(); currentdatetime()" Text="CH-ST" Visible="False" Width="100px" /><asp:Button ID="btnDeleteCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="Confirm(); currentdatetime()" Text="DELETE" Width="100px" /><asp:Button ID="btnPrintCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="PRINT" Visible="False" Width="100px" /><asp:Button ID="btnCloseCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CLOSE" Visible="False" Width="100px" /></td></tr><tr><td colspan="2"><br /></td></tr><tr class="Centered"><td colspan="2"><asp:GridView ID="gvCP" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSCP" Font-Size="15px" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnRowDataBound="OnRowDataBoundgCP"><AlternatingRowStyle BackColor="White" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False"><ControlStyle Width="150px" /><ItemStyle Width="150px" /></asp:CommandField><asp:BoundField DataField="AccountID" HeaderText="AccountID" SortExpression="AccountID"><ControlStyle Width="150px" /><HeaderStyle Font-Size="12pt" /><ItemStyle HorizontalAlign="Left" Width="80px" /></asp:BoundField><asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"><ItemStyle Width="150px" /></asp:BoundField><asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" /><asp:BoundField DataField="UserID" HeaderText="User ID"><ItemStyle Width="120px" /></asp:BoundField><asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"><ControlStyle Width="300px" /><HeaderStyle Font-Size="12pt" /><ItemStyle HorizontalAlign="Left" Width="70px" /></asp:BoundField><asp:BoundField DataField="LastLogin" HeaderText="Last Login" SortExpression="LastLogin" /><asp:BoundField DataField="CreatedOn" HeaderText="EntryDate" SortExpression="CreatedOn" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="CreatedBy" HeaderText="EntryStaff" SortExpression="CreatedBy" /><asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" /><asp:BoundField DataField="LastModifiedOn" HeaderText="EditedOn" SortExpression="LastModifiedOn" /></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat">Account ID</td><td class="CellTextBox"><asp:TextBox ID="txtAccountIDCP" runat="server" Height="16px" MaxLength="50" Width="50%"></asp:TextBox></td></tr><tr><td class="CellFormat">Name<asp:Label ID="Label35" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:TextBox ID="txtNameCP" runat="server" Height="16px" MaxLength="200" Width="50%"></asp:TextBox></td></tr><tr><td class="CellFormat">Email</td><td class="CellTextBox"><asp:TextBox ID="txtEmailCP" runat="server" Height="16px" MaxLength="200" Width="50%"></asp:TextBox>;</td></tr><tr><td class="CellFormat">User ID</td><td class="CellTextBox"><asp:TextBox ID="txtUserIDCP" runat="server" Height="16px" MaxLength="25" Width="50%"></asp:TextBox></td></tr><tr><td class="CellFormat">Password</td><td class="CellTextBox"><asp:TextBox ID="txtPwdCP" runat="server" Height="16px" MaxLength="50" TextMode="Password" Width="50%"></asp:TextBox></td></tr><tr><td class="CellFormat">Active </td><td class="CellTextBox"><asp:CheckBox ID="chkStatusCP" runat="server" /></td></tr><tr><td class="CellFormat">Change Password on Next Logon</td><td class="CellTextBox"><asp:CheckBox ID="chkChangePasswordonNextLogin" runat="server" /></td></tr><tr><td colspan="2" style="text-align:right"><asp:Button ID="btnSaveCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" /><asp:Button ID="btnCancelCP" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table><asp:SqlDataSource ID="SqlDSCP" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div><asp:TextBox ID="txtCPRcno" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtCPMode" runat="server" CssClass="dummybutton"></asp:TextBox></ContentTemplate></asp:TabPanel>
                                </asp:TabContainer>
                                <%--$(document).ready(function () { $("#ddlIndustry").select2(); });--%></td>
                        </tr>
                        <tr style="text-align:center;width:100%">
                            <td colspan="8" style="text-align:center;padding-left:20px;">&nbsp;</td>
                        </tr>
                    </caption>
                </caption>
            </caption>
            </table>
           <br />

              <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="btnFilter" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
         
              <asp:Panel ID="Panel1" runat="server" BackColor="White" Width="650px" Height="400" BorderColor="#003366" BorderWidth="1">
         
         
               <br />
                   <table style="width:100%;padding-left:15px">
                       <tr>
                               <td class="CellFormat">Account ID
                               </td>
                              <td class="CellTextBox" colspan="1">    <asp:TextBox ID="txtSearchID" runat="server" MaxLength="50" Height="16px" Width="50%"></asp:TextBox>
                            </td> 
                           </tr>
                       <tr>
                                <td class="CellFormat">Status
                               </td>
                              <td class="CellTextBox" colspan="1">     <asp:DropDownList ID="ddlSearchStatus" runat="server" Width="50%">
                                  <asp:ListItem Text="--SELECT--" Value="-1" />
                                   <asp:ListItem Value="O">O - Open</asp:ListItem>
                               <%--    <asp:ListItem Value="T">T - Terminated</asp:ListItem>--%>
                               
                               </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkSearchInactive" runat="server" Text=" Inactive" textalign="left" Font-Bold="true" Font-Names ="Comic Sans" />
                            </td>                              
                           </tr>
                        <tr id="BranchSearch" runat="server">
                                <td class="CellFormat">Branch
                               </td>
                              <td class="CellTextBox" colspan="1">    
                                  <asp:DropDownList ID="ddlBranchSearch" runat="server" Width="50%" AppendDataBoundItems="True">
                                  <asp:ListItem Text="--SELECT--" Value="-1" />                                 
                               </asp:DropDownList>   
                              </td>                              
                           </tr>
                          <tr>
                              <td class="CellFormat">Company </td>
                              <td class="CellTextBox" colspan="3">
                                  <asp:TextBox ID="txtSearchCompany" runat="server" Height="16px" MaxLength="100" Width="90%"></asp:TextBox>
                              </td>
                         <tr>
                               <td class="CellFormat">OfficeAddress
                               </td>
                              <td class="CellTextBox" colspan="3">    <asp:TextBox ID="txtSearchAddress" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox>
                            </td>
                             </tr>
                         <tr>
                               <td class="CellFormat">BillingAddress
                               </td>
                              <td class="CellTextBox" colspan="3">    <asp:TextBox ID="txtSearchBillingAddress" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox>
                            </td>
                             </tr>
                         <tr>
                               <td class="CellFormat">ServiceAddress
                               </td>
                              <td class="CellTextBox" colspan="3">    <asp:TextBox ID="txtSearchServiceAddress" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox>
                            </td>
                             </tr>
                          <tr>
                               <td class="CellFormat">ContactPerson
                               </td>
                              <td class="CellTextBox" colspan="3">    <asp:TextBox ID="txtSearchContact" runat="server" MaxLength="50" Height="16px" Width="90%"></asp:TextBox>
                           <%--    <asp:ImageButton ID="btnSearchContact" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" Height="22" Width="24" />
                   --%>  </td>
                             </tr>
                           <tr>
                               <td class="CellFormat">Postal
                               </td>
                              <td class="CellTextBox" >    <asp:TextBox ID="txtSearchPostal" runat="server" MaxLength="50" Height="16px" Width="50%"></asp:TextBox>
                            </td> 
                              </tr>
                       <tr>
                                 <td class="CellFormat">ContactNo
                               </td>
                              <td colspan="1" class="CellTextBox">      <asp:TextBox ID="txtSearchContactNo" runat="server" MaxLength="50" Height="16px" Width="50%"></asp:TextBox>
                                                
                            </td>                              
                           </tr>
                          <tr>
                               <td class="CellFormat">Salesman
                               </td>
                              <td class="CellTextBox" >     <asp:DropDownList ID="ddlSearchSalesman" runat="server" AppendDataBoundItems="true" DataTextField="StaffId" DataValueField="StaffId" Width="50%" Height="25px" >
                                  <asp:ListItem Text="--SELECT--" Value="-1" />
                                                     </asp:DropDownList>
                                      <asp:ListSearchExtender ID="ddllsSearchSalesman" runat="server" TargetControlID="ddlSearchSalesman" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td> 
                               </tr>
                       <tr>
                                 <td class="CellFormat">Industry
                               </td>
                              <td class="CellTextBox" >  <asp:DropDownList ID="ddlSearchIndustry" runat="server" AppendDataBoundItems="true" DataTextField="salesman" DataValueField="salesman" Width="50%" Height="25px" >
                                  <asp:ListItem Text="--SELECT--" Value="-1" />
                                                     </asp:DropDownList>
                              <asp:ListSearchExtender ID="ddllsSearchIndustry" runat="server" TargetControlID="ddlSearchIndustry" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>                              
                           </tr>
                         
                         <tr><td colspan="4"><br /></td></tr>
                         <tr>
                             <td colspan="2" style="text-align:right;padding-right:20px;"><asp:Button ID="btnSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Search" Width="100px"/></td>
                             <td colspan="2" style="text-align:left;"><asp:Button ID="btnClose" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
                         </tr>

        </table>
           </asp:Panel>
   
           


           <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />

           
          <asp:Panel ID="pnlPopupInvoiceDetails" runat="server" BackColor="White" Width="800px" Height="90%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
        <%--<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton4" PopupControlID="pnlPopupInvoiceDetails" TargetControlID="btnDummyInvoice"  Enabled="True" DynamicServicePath=""></asp:ModalPopupExtender>--%>
                       
                <table border="0" style="width:100%;">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:25%;text-align:right;">
                             <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />

                               </td></tr>
                   
                    
                     <tr><td colspan="3" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">Transactions</h4> 
  </td> <td>
                               <asp:TextBox ID="txtAccountIDSelected" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
                    <tr>
                        <td colspan="1" style="text-align:left;padding-left:20px; width:40%">
                            <asp:DropDownList ID="ddlFilter" runat="server" CssClass="CellFormat" Width="95%" AutoPostBack="True">
                                <asp:ListItem>ALL TRANSACTIONS</asp:ListItem>
                                <asp:ListItem>SALES INVOICE</asp:ListItem>
                                <asp:ListItem>SALES INVOICE (OUTSTANDING)</asp:ListItem>
                                <asp:ListItem>RECEIPT</asp:ListItem>
                                <asp:ListItem>CREDIT NOTE</asp:ListItem>
                                <asp:ListItem>DEBIT NOTE</asp:ListItem>
                                <asp:ListItem>ADJUSTMENT</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="1" style="text-align:right;padding-left:5px;font-size:15px;font-weight:bold;font-family:Calibri;color:black;">
                             Cut Off Date</td>

                         <td colspan="1" style="text-align:left;padding-left:1px;">
                             <asp:TextBox ID="txtCutOffDate" runat="server" Height="16px" MaxLength="10" Width="95%" AutoPostBack="True"></asp:TextBox>
                             <asp:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtCutOffDate" TargetControlID="txtCutOffDate">
                             </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="lblAlertTransactions" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td>

                         <td colspan="1" style="text-align:left;padding-left:1px;">
                         
                            </td>
                    </tr>
        </table>
              <div style="text-align: center; padding-left: 30px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewInvoiceDetails" runat="server" DataSourceID="SqlDSInvoiceDetails" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="95%" AllowSorting="True"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns><asp:BoundField DataField="VoucherDate" HeaderText="VoucherDate" DataFormatString="{0:dd/MM/yyyy}" SortExpression="VoucherDate">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Type" HeaderText="Type">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherNumber" HeaderText="Reference Number">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
               
                <asp:HyperLinkField 
         DataTextField="Type"                 
         DataNavigateUrlFields="Type,VoucherNumber" 
         DataNavigateUrlFormatString="{0}.aspx?VoucherNumber={1}&CustomerFrom=Corporate" HeaderText="View" DataTextFormatString="View" target="_blank" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="White" ItemStyle-ForeColor="Black" />
             <%--   <asp:HyperLinkField 
      DataTextField="ProductName" 
      HeaderText="Product Name" 
      SortExpression="ProductName" 
      DataNavigateUrlFields="ProductID" 
      DataNavigateUrlFormatString="ProductDetails.aspx?ProductID={0}" />--%>
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
                  <div style="text-align:right;width:93%;padding-right:30px">
                      <asp:Label ID="Label11" runat="server" Text="Total" CssClass="CellFormat" Visible="false"></asp:Label>
                      <asp:Label ID="lblTotal" runat="server" Text="Total    : 0.00" CssClass="CellFormat"></asp:Label>
                  </div>
                  <asp:SqlDataSource ID="SqlDSInvoiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
           <SelectParameters>
                          <asp:ControlParameter ControlID="txtAccountIDSelected" Name="@AccountID" PropertyName="Text" />
                      </SelectParameters>
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:40px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="btnCloseInvoiceSvc" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
              
               <%-- <tr>
                    <td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp; <asp:TextBox ID="TextBox5" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True" Visible="False">Search Here for Team or In-ChargeId</asp:TextBox>
  </td> <td>
                           </td>


                    </tr>
                            --%>
                </table>
                 <table class="Centered" border="0" id="tablebutton" runat="server" style="border: 2px solid #996633; text-align:right; border-radius: 25px;padding: 2px; width:95%; height:100px;">
      
                <tr>
                    <td colspan ="2" style="text-align:left;">
                        <asp:Label ID="lblCurDate" runat="server" Text="" CssClass="CellFormat"></asp:Label>
                    </td>
                </tr>
                           <tr style="text-align:center;width:100%">
                   <td colspan="2" style="text-align:center;width:100%">
                       <asp:Button ID="btnEmailSOA" runat="server" Text="Email (O/S Invoice)" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Width="190px" Font-Names="Calibri" Font-Size="14px" />
                         <%--<a href="Email.aspx?Type=CustSOA" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:190px;font-family:Calibri;font-size:14px;" type="button">Email (O/S Invoice)</button></a>--%>
            &nbsp; <a href="Email.aspx?Type=ReceiptTransactions" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:190px;font-family:Calibri;font-size:14px;" type="button">Email Receipt Transactions</button></a>
            &nbsp; <a href="Email.aspx?Type=TransactionSummary" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:190px;font-family:Calibri;font-size:14px;" type="button">Email Transaction Summary</button></a>
           
                       </td></tr>
               <tr style="text-align:center;width:100%">
                   <td colspan="2" style="text-align:center;width:100%">
                           <a href="RV_OutstandingInvoiceStatement.aspx" target="_blank"><button class="roundbutton1" id="btnOSInvoiceStatement" runat="server" style="background-color:#CFC6C0;font-weight:bold;width:190px; font-family:Calibri;font-size:14px;" type="button">View Statement (O/S Invoice)</button></a>
             
                          &nbsp; <a href="RV_ReceiptTransactions.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:190px; font-family:Calibri;font-size:14px;" type="button">View Receipt Transactions</button></a>
                
                  &nbsp; 
                    <a href="RV_TransactionSummary.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:190px; font-family:Calibri;font-size:14px;" type="button">View Transaction Summary</button></a>
                       </td>
               </tr>
                  <tr style="text-align:center;width:100%">
                   <td colspan="2" style="text-align:center;width:100%">
                       
                          &nbsp; <%--<a href="RV_ReceiptTransactions.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:190px; font-family:Calibri;font-size:14px;" type="button">Email Reminder</button></a>--%>
                    <asp:Button ID="btnEmailReminder" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Email Reminder" Width="180px" Visible="true" />
              
                  &nbsp; 
                         </td>
               </tr>
        </table>
          </asp:Panel>
   <asp:ModalPopupExtender ID="ModalPopupInvoice" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlPopupInvoiceDetails" TargetControlID="btnDummyInvoice" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyInvoice" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
          
           
           
             <asp:Panel ID="pnlEditBilling" runat="server" BackColor="White" Width="60%" Height="98%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:5px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Billing Information</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageBilling" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBilling" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         <tr>
                             <td>
                                       <table class="Centered" style="padding-top:5px;width:99%">
                                 


                               <tr>
                                   
     <td colspan="4" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:right;">    <asp:CheckBox ID="chkUpdateBillingAll" runat="server" Text="Update All Service Location" Font-Names="Calibri" Font-Underline="False"  onclick="UpdateBillingDetails()"/></td>
                                                                      



                               </tr>
                                                 <tr>
                                   

                                   <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:1%">Billing Address Info</td>

                                   



                               </tr>
                       
                        <tr>
                                   

                                   <td class="CellFormat">Billing Name<asp:Label ID="Label4" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                     </td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtEditBillingName" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>

                                   



                               </tr>
                                 
                               <tr>
                                   

                                   <td class="CellFormat">Street Address1<asp:Label ID="Label6" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                   </td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtEditBillAddress" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Street Address2</td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtEditBillStreet" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">&nbsp;Building &amp; Unit No.&nbsp; </td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtEditBillBuilding" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">City </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlEditBillCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="City" DataValueField="City"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
                                   

                                   <td class="CellFormat">State </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlEditBillState" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Country </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlEditBillCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="Country" DataValueField="Country"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
                                   

                                   <td class="CellFormat">Postal </td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillPostal" runat="server" MaxLength="20" Height="16px" Width="98%" ></asp:TextBox></td>

                                   



                               </tr>
                                 

                                 <tr>
                                     

                                   <td colspan="4"><br /></td>
                                     

                               </tr>
                                 

                               <tr>
                                   

                                   <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:1%">Billing Contact Info</td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Contact Person 1<asp:Label ID="Label7" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                   </td>
                                   

                                   <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtEditBillCP1Contact" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                    

                                    <td class="CellFormat">Position</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP1Position" runat="server" MaxLength="100" Height="16px" Width="98%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP1Tel" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                   

                                   <td class="CellFormat">Fax</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP1Fax" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone2</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP1Tel2" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                   

                                   <td class="CellFormat">Mobile</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP1Mobile" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                                <tr>
                                    

                        <td class="CellFormat">Email</td>
                                    

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtEditBillCP1Email" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                                    

                        </tr>
                                 

                               <tr>
                                   

                                   <td colspan="4"><br /></td>
                                   

                               </tr>
                                 

                                <tr>
                                    

                                   <td class="CellFormat">Contact Person 2</td>
                                    

                                   <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtEditBillCP2Contact" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                    

                                    <td class="CellFormat">Position</td>
                                    

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP2Position" runat="server" MaxLength="100" Height="16px" Width="98%"></asp:TextBox></td>

                                    



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP2Tel" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                   

                                   <td class="CellFormat">Fax</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP2Fax" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone2</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP2Tel2" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                   

                                   <td class="CellFormat">Mobile</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtEditBillCP2Mobile" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                                <tr>
                                    

                        <td class="CellFormat">Email</td>
                                    

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtEditBillCP2Email" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                                    

                        </tr>
                               <tr>
                                   <td colspan="4"><br /></td>
                               </tr>

                           
                         </table> 
                                       </td>
                         </tr>  
                     
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditBillingSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnEditBillingCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                             <tr>
                                   <td colspan="4"><br /></td>
                               </tr>
                 

        </table>
           </asp:Panel>

             <asp:ModalPopupExtender ID="mdlPopupEditBilling" runat="server" CancelControlID="btnEditBillingCancel" PopupControlID="pnlEditBilling" TargetControlID="btndummyBilling" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btndummyBilling" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
              
         <asp:Panel ID="pnlPopupClientSearch" runat="server" BackColor="White" Width="60%" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left" ScrollBars="Horizontal">
     <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Search TIN</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnClientSearchClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                             <asp:ImageButton ID="btnppClientReset" OnClick="btnPpClientReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                               <asp:TextBox ID="txtPpClientSearch" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for TIN" ForeColor = "Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Visible="true" Width="24px" />
                             </td> <td></td>
                                      </tr>
         
                           </table>
       
       <asp:TextBox ID="txtPpclient" runat="server" Visible="False"></asp:TextBox>
                <div style="text-align: center; padding-left: 2px; padding-bottom: 5px;">
            <br />
               <asp:GridView ID="gvClientSearch" runat="server" Font-Size="15px" DataSourceID="SqlDSClientSearch" ForeColor="#333333" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" HorizontalAlign="Center"
                CellPadding="4" GridLines="Vertical" Width="90%" OnRowDataBound = "OnRowDataBoundgClientSearch" OnSelectedIndexChanged = "OnSelectedIndexChangedgClientSearch">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
            
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                  
                    <asp:BoundField DataField="Name" HeaderText="Company Name" SortExpression="Name">
                        <HeaderStyle Width="300px" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RocNos" HeaderText="Company Registration No" SortExpression="RocNos">
                        <ControlStyle Width="150px" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                   
                <asp:BoundField DataField="TaxIdentificationNo" HeaderText="TIN" SortExpression="TaxIdentificationNo">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="False" />
                    </asp:BoundField>
                  
                    <asp:BoundField DataField="AccountID">
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle Wrap="False" CssClass="dummybutton" />
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
                  </div>
    </asp:Panel>
         <asp:SqlDataSource ID="SqlDSClientSearch" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
     
            <asp:modalpopupextender ID="mdlPopupTIN" runat="server" CancelControlID="btnClientSearchClose" PopupControlID="pnlPopUpClientSearch"
                                    TargetControlID="btndummyClientSearch" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
                                </asp:modalpopupextender>
            <asp:Button ID="btndummyClientSearch" runat="server" Text="Button" CssClass="dummybutton" />
     
                                 <%--Start: Duration in Contract Group --%>
                                              
                 <asp:Panel ID="pnlDurationType" runat="server" BackColor="White" Width="400px" Height="140px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblNoDurationType" runat="server" Text="NO DURATION TYPE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label45" runat="server" Text="The Duration Type in the Contract Group master "></asp:Label>
                        
                      </td>
                           </tr>

                <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label46" runat="server" Text="must be configured first to be able to proceed."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="Button10" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlDurationType" runat="server" CancelControlID="" PopupControlID="pnlDurationType" TargetControlID="btnDummyDurationType" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyDurationType" runat="server" CssClass="dummybutton" />

             <%-- End: Duration in Contract Group--%>

                  <%-- Start: EMail SOA --%>

                <asp:Panel ID="pnlEmailSOA" runat="server" BackColor="White" Width="750px" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Email SOA</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageEmailSOA" runat="server"></asp:Label>
                      </td> 
            </tr>

             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertEmailSOA" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                    
                          <tr>
                              <td class="CellTextBox" colspan="1" rowspan="2" style="width:100px">
                                   <asp:RadioButtonList ID="rdbEmailSOA" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1" Selected="True">Send SOA to Client</asp:ListItem> 
                                       <asp:ListItem Value="2">Internal</asp:ListItem>
                                  </asp:RadioButtonList>
                                  <%--<asp:RadioButton ID="rdbClientEmailSOA" CssClass="CellFormat" runat="server" Text="Send SOA to Client" />--%>
                              </td>
                              <td style="text-align:left">     
                                  <asp:TextBox ID="txtClientEmailAddr" runat="server" Height="16px" MaxLength="150" Width="350px" Enabled="false"></asp:TextBox>
                                                               
                                  </td>
                              </tr>
                         <tr>
                         <%--     <td class="CellTextBox" colspan="1" rowspan="1">
                                 
                                   </td>--%>
                             <td style="text-align:left">                
                                 <asp:TextBox ID="txtInternalEmailAddr" runat="server" Height="16px" MaxLength="150" Width="350px" Enabled="false"></asp:TextBox>
                              </td>                       
                         </tr>
                                                
                     
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSendEmailSOA" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Send" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelEmailSOA" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEmailSOA" runat="server" CancelControlID="btnCancelEmailSOA" PopupControlID="pnlEmailSOA" TargetControlID="btnDummyEmailSOA" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btnDummyEmailSOA" runat="server" cssclass="dummybutton" />  

                 <%-- end: EMail SOA--%>

          <%-- Start: Relocate--%>

                <asp:Panel ID="pnlRelocate" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Reloation of Location ID</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageRelocate" runat="server"></asp:Label>
                      </td> 
            </tr>

             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertRelocate" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Existing Location ID</td>
                             <td class="CellTextBox">
                                    <asp:TextBox ID="txtLocationIDRelocate" runat="server" Height="16px" MaxLength="50" Width="50%" Enabled="False"></asp:TextBox>
                      </td>
                         </tr>
                          <tr>
                              <td class="CellFormat">Existing Account ID to Relocate</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtAccountIDRelocate" runat="server" Height="16px" MaxLength="25" Width="50%" AutoPostBack="True"></asp:TextBox>
                              </td>
                         </tr>
                                                
                        <tr>
                              <td class="CellFormat">Billing Name</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtBillingNameRelocate" runat="server" Height="16px" MaxLength="25" Width="50%" Enabled="False"></asp:TextBox>
                              </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">New Location ID</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtLocationIDRelocateNew" runat="server" Height="16px" MaxLength="50" Width="50%" Enabled="False"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveRelocate" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Relocate" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnRelocateCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupRelocate" runat="server" CancelControlID="btnRelocateCancel" PopupControlID="pnlRelocate" TargetControlID="btndummyRelocate" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyRelocate" runat="server" cssclass="dummybutton" />
  

                 <%-- end: Relocate--%>

        
              <%-- Start: Specific Location--%>

                <asp:Panel ID="pnlSpecificLocaion" runat="server" BackColor="White" Width="60%" Height="75%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Specific Location</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label14" runat="server"></asp:Label>
                      </td> 
            </tr>

             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label16" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';">
                                 <asp:Button ID="btnAddSpecificLocation" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="ADD" Width="100px" />
                                 <asp:Button ID="btnEditSpecificLocation" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Width="100px" OnClientClick="currentdatetime()" />
                                 <asp:Button ID="btnDeleteSpecificLocation" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="Confirm(); currentdatetime()" Text="DELETE" Width="100px" />
                                 <asp:Button ID="btnCloseSpecificLocation" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CLOSE" Width="100px" />
                            
                                  </td>
                         </tr>
                          <tr class="Centered">
                             <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';">
                                 <asp:GridView ID="gvSpecificLocation" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSSpecificLocation" Font-Size="15px" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center">
                                     <AlternatingRowStyle BackColor="White" />
                                     <Columns>
                                         <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                                         <ControlStyle Width="150px" />
                                         <ItemStyle Width="150px" />
                                         </asp:CommandField>
                                         <asp:BoundField DataField="SpecificLocationName" HeaderText="Specific Location" SortExpression="SpecificLocationName">
                                         <ControlStyle Width="300px" />
                                         <HeaderStyle Font-Size="12pt" />
                                         <ItemStyle HorizontalAlign="Left" Width="300px" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="Zone" HeaderText="Zone" />
                                         <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                             <EditItemTemplate>
                                                 <asp:Label ID="Label63" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="Label63" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:BoundField DataField="CreatedBy" HeaderText="EntryStaff" SortExpression="CreatedBy" />
                                         <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EntryDate" SortExpression="CreatedOn" />
                                         <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                         <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                                     </Columns>
                                     <EditRowStyle BackColor="#2461BF" />
                                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                     <HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" />
                                     <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                                     <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
                                     <SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" />
                                     <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                     <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                     <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                     <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                 </asp:GridView>
                             </td>
                         </tr>
                         
                         <tr>
                             <td class="CellFormat"> Location ID</td>
                             <td class="CellTextBox">
                                    <asp:TextBox ID="txtLocationIDSpecificLocation" runat="server" Height="16px" MaxLength="50" Width="50%" Enabled="False"></asp:TextBox>
                      </td>
                         </tr>
                          <tr>
                              <td class="CellFormat">Account ID</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtAccountIDSpecificLocation" runat="server" Height="16px" MaxLength="25" Width="50%" Enabled="False"></asp:TextBox>
                              </td>
                         </tr>
                                                
                        <tr>
                              <td class="CellFormat">Specific Location</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtSpecificLocation" runat="server" Height="16px" MaxLength="100" Width="50%" Enabled="False"></asp:TextBox>
                              </td>
                         </tr>
                      
                         <tr>
                             <td class="CellFormat">Zone</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtZone" runat="server" Enabled="False" Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                             </td>
                         </tr>
                      
                         <tr>
                             <td colspan="2">
                                 <asp:SqlDataSource ID="SqlDSSpecificLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblnotes where Keyfield = 'aa'"></asp:SqlDataSource>
                                <asp:TextBox ID="txtSpecificLocationRcNo" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtSpecificLocationMode" runat="server" CssClass="dummybutton"></asp:TextBox>
                                  <br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveSpecificLocaion" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnSpecificLocationCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               
                                   </td>
                         </tr>
                          
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupSpecificLocaion" runat="server" CancelControlID="btnCloseSpecificLocation" PopupControlID="pnlSpecificLocaion" TargetControlID="btndummySpecificLocaion" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btnDummySpecificLocaion" runat="server" cssclass="dummybutton" />
  

                 <%-- end: Specific Location --%>


                            <%-- start: Contract Group--%>

            <asp:Panel ID="pnlEditContractGroup" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:10px">
                         <tr>
                             <td colspan="2" >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Contract Group </h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label32" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertContractGroup" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Contract Group</td>
                             <td class="CellTextBox">
                                    <asp:DropDownList ID="ddlContractGroupEdit" runat="server" AppendDataBoundItems="True" Width="90%">
                                        <asp:ListItem>--SELECT--</asp:ListItem>
                                    </asp:DropDownList>
                      </td>
                         </tr>
                        
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnContractGroupEditSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnContractGroupCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupContractGroup" runat="server" CancelControlID="btnContractGroupCancel" PopupControlID="pnlEditContractGroup" TargetControlID="btndummyContractGroupEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyContractGroupEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Contract Group--%>

           
                            <%-- start: Reminder--%>

            <asp:Panel ID="pnlReminder" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:10px">
                         <tr>
                             <td colspan="2" >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Manual Reminder</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageReminder" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertReminder" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Account ID</td>
                             <td class="CellTextBox">
                                     <asp:TextBox ID="txtAccountIDReminder" runat="server" Height="16px" MaxLength="100" Width="50%" Enabled="False"></asp:TextBox>
              
                      </td>
                         </tr>
                        
                         <tr>
                             <td class="CellFormat">CustName</td>
                             <td class="CellTextBox">
                                                       <asp:TextBox ID="txtCustNameReminder" runat="server" Height="16px" MaxLength="100" Width="80%" Enabled="False"></asp:TextBox>
              
                      </td>
                         </tr>
                            <tr>
                             <td class="CellFormat">Cut-Off Date</td>
                             <td class="CellTextBox">
                                                       <asp:TextBox ID="txtCutOffDateReminder" runat="server" Height="16px" MaxLength="100" Width="50%" Enabled="False"></asp:TextBox>
                  <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtCutOffDateReminder" TargetControlID="txtCutOffDate">
                             </asp:CalendarExtender>
                      </td>
                         </tr>
                           <tr>
                             <td class="CellFormat">OutStanding Amount</td>
                             <td class="CellTextBox">
                                                       <asp:TextBox ID="txtAmtReminder" runat="server" Height="16px" MaxLength="100" Width="50%" ></asp:TextBox>
              
                      </td>
                         </tr>
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                       <%--     <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnReminderSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                </td>
                         </tr>--%>
                        
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center">
                                       <a href="RV_ReminderFirst.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:220px; font-family:Calibri;font-size:14px;" type="button">Reminder Format1</button></a>
                 <a href="RV_ReminderSecond.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:220px; font-family:Calibri;font-size:14px;" type="button">Reminder Format2</button></a>
           
                          <%--       <asp:Button ID="btnEmailReminder1" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Reminder Letter Format1" Width="220px" />
                                 <asp:Button ID="btnEmailReminder2" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Reminder Letter Format2" Width="220px" />
                       --%>          <asp:Button ID="btnReminderCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                            </td>
                         </tr>
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupReminder" runat="server" CancelControlID="btnReminderCancel" PopupControlID="pnlReminder" TargetControlID="btndummyReminder" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btnDummyReminder" runat="server" cssclass="dummybutton" />
  

        <%-- end:Contract Group--%>
           <%--Start: Location Transaction--%>

                 <asp:Panel ID="pnlPopupInvoiceDetailsSvc" runat="server" BackColor="White" Width="700px" Height="70%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                       
                <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">Locationwise Transactions</h4> 
  </td>
                        
                         <td>
                <asp:TextBox ID="txtLocationIDSelectedsVC" runat="server" Width="2%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
 <asp:TextBox ID="txtAccountIdSelectedSvc" runat="server" Width="2%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td>
                      

                    </tr>
                
                    <tr>
                        <td colspan="1" style="text-align:left;padding-left:50px;">
                            <asp:DropDownList ID="ddlFilterSvc" runat="server" CssClass="CellFormat" Width="250px" AutoPostBack="True">
                                <asp:ListItem>ALL TRANSACTIONS</asp:ListItem>
                                <asp:ListItem>SALES INVOICE</asp:ListItem>
                                <asp:ListItem>SALES INVOICE (OUTSTANDING)</asp:ListItem>
                                <asp:ListItem>RECEIPT</asp:ListItem>
                                <asp:ListItem>CREDIT NOTE</asp:ListItem>
                                <asp:ListItem>DEBIT NOTE</asp:ListItem>
                                <asp:ListItem>ADJUSTMENT</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="1" style="text-align:left;padding-left:50px;">
                             &nbsp;</td>
                    </tr>
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="lblAlertTransactionsSvc" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewInvoiceDetailsSvc" runat="server" DataSourceID="SqlDSInvoiceDetailsSvc" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="95%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns><asp:BoundField DataField="VoucherDate" HeaderText="VoucherDate" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Type" HeaderText="Type">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherNumber" HeaderText="Reference Number">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
               
                <asp:HyperLinkField 
         DataTextField="Type"                 
         DataNavigateUrlFields="Type,VoucherNumber" 
         DataNavigateUrlFormatString="{0}.aspx?VoucherNumber={1}" HeaderText="View" DataTextFormatString="View" target="_blank" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="White" ItemStyle-ForeColor="Black" />
             <%--   <asp:HyperLinkField 
      DataTextField="ProductName" 
      HeaderText="Product Name" 
      SortExpression="ProductName" 
      DataNavigateUrlFields="ProductID" 
      DataNavigateUrlFormatString="ProductDetails.aspx?ProductID={0}" />--%>
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
                  <div style="text-align:right;width:93%;padding-right:80px">
                      <asp:Label ID="Label15" runat="server" Text="Total" CssClass="CellFormat" Visible="false"></asp:Label>
                      <asp:Label ID="lblTotalSvc" runat="server" Text="Total    : 0.00" CssClass="CellFormat"></asp:Label>
                  </div>
                  <asp:SqlDataSource ID="SqlDSInvoiceDetailsSvc" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT tblsales.salesdate as VoucherDate,'INVOICE' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount 
FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARIN' 
AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  ((tblsales.AccountId = @AccountID) AND (tblsalesdetail.LocationId = @LocationId)) 

union select tblrecv.RECEIPTDATE as VoucherDate,'RECEIPTS' as Type,tblrecv.receiptnumber as VoucherNumber,tblrecv.AppliedBase as Amount 
from tblrecv, tblrecvdet, tblsalesdetail WHERE tblsalesdetail.Invoicenumber = tblRecvDet.RefType and tblrecv.Receiptnumber = tblRecvDet.Receiptnumber and  tblrecv.poststatus='P' AND (tblrecv.AccountId = @AccountID) AND (tblsalesdetail.LocationId = @LocationId) 

union SELECT tblsales.salesdate as VoucherDate,'CN' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount 
FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARCN' 
AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  (tblsales.AccountId = @AccountID) AND (tblsalesdetail.LocationId = @LocationId) 


union SELECT tblsales.salesdate as VoucherDate,'DN' as Type, tblsales.invoicenumber as VoucherNumber, tblsalesdetail.AppliedBase as Amount 
FROM tblsales, tblsalesdetail WHERE tblsales.poststatus='P' and tblsales.doctype='ARDN' 
AND tblsales.Invoicenumber = tblsalesDetail.Invoicenumber and  (tblsales.AccountId = @AccountID) AND (tblsalesdetail.LocationId = @LocationId)  ">
           <SelectParameters>
                          <asp:ControlParameter ControlID="txtAccountIDSelectedSvc" Name="@AccountID" PropertyName="Text" />
                          <asp:ControlParameter ControlID="txtLocationIDSelectedsVC" Name="@LocationId" PropertyName="Text" />
                      </SelectParameters>
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:40px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="btnCloseInvoice" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
               <%-- <tr>
                    <td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp; <asp:TextBox ID="TextBox5" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True" Visible="False">Search Here for Team or In-ChargeId</asp:TextBox>
  </td> <td>
                           </td>


                    </tr>
                            --%>
               <tr style="text-align:center;width:100%">
                   <td colspan="2" style="text-align:center;width:100%">
                    <a href="RV_TransactionSummary.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:200px; font-family:Calibri;font-size:14px;" type="button">View Summary</button></a>
                      &nbsp;<a href="RV_OutstandingInvoiceStatement.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:200px; font-family:Calibri;font-size:14px;" type="button">View Statement (O/S Invoices)</button></a>
                   </td>
               </tr>

        </table>
          </asp:Panel>
   <asp:ModalPopupExtender ID="ModalPopupInvoiceSvc" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlPopupInvoiceDetailsSvc" TargetControlID="btnDummyInvoiceSvc" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyInvoiceSvc" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
    

         <%--  End: Location Transaction--%>
           
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
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label43" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
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


           <%--start--%>

                  <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="540px" Height="300px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
 <%--                    <table style="width:100%;padding-left:15px">
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
                             <td class="CellFormat">AccountID</td>
                             <td class="CellTextBox">
                                 <asp:Label ID="lblStatusAccountID" runat="server" Width="50%" BackColor="#CCCCCC"></asp:Label></td>
                         </tr>
                          <tr>
                               <td class="CellFormat">Existing Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:Label ID="lblOldStatus" runat="server" width="50%" BackColor="#CCCCCC"></asp:Label></td>
                           </tr>
                         
                          <tr>
                               <td class="CellFormat">New Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlNewStatus" runat="server" Width="51%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                   <asp:ListItem Value="0">Active</asp:ListItem>
                                   <asp:ListItem Value="1">InActive</asp:ListItem> 
                                                                      
                               </asp:DropDownList></td>
                           </tr>
                           
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnUpdateStatus" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Update Status" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelStatus" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                        
        </table>--%>

                         <table border="0" style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:center;padding-left:0px;">
           
                
               <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr> 
                              <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="lblAlertStatus" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td>

                    </tr>
               <tr>
                      <td colspan="2" class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label ID="lblStatusMessage" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label>
                       
                      </td>
                           </tr>
                              <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                               <tr>
                              <td class="CellFormat">Remarks     <asp:Label ID="Label44" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                        </td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtStatusRemarks" runat="server" Height="45px" MaxLength="2000" Width="90%" TextMode="MultiLine"></asp:TextBox>
                               </td>
                         </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="roundbutton1" OnClientClick="currentdatetime();" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                              <asp:Button ID="Button1" runat="server" CssClass="dummybutton" />

            <asp:Button ID="btnConfirmNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

           <asp:ModalPopupExtender ID="mdlPopupStatus" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlStatus" TargetControlID="btndummy1" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
    <asp:Button ID="btndummy1" runat="server" CssClass="dummybutton" />

          <%-- end--%>

             <%--start - Transfer Files--%>

                  <asp:Panel ID="pnlTransferFiles" runat="server" BackColor="White" Width="940px" Height="500px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              


                         <table border="0" style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:center;padding-left:0px;">
           
                
               <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr> 
                              <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="lblAlertTransferFiles" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td>

                    </tr>
               <tr>
                      <td colspan="2" class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label ID="Label51" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri">TRANSFER FILES</asp:Label>
                       
                      </td>
                           </tr>
                              <tr>
                             <td colspan="2"><br /></td>
                                  
                         </tr>
                             <tr>
                                 <td colspan="2" class="CellFormat">
                                      <asp:GridView ID="gvTransferFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" OnSelectedIndexChanged="OnSelectedIndexChangedTransferFile" onrowdatabound="OnRowDataBoundTransferFiles" CssClass="Centered" DataSourceID="SqlDSTransferUpload" EmptyDataText="No files uploaded" Width="90%" height="50%">
                                                            <Columns>
                                                                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="50px" />
                </asp:CommandField>
                                                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                                <asp:BoundField DataField="FileDescription" HeaderText="File Description" />
                                                                <asp:BoundField DataField="CreatedOn" DataFormatString="{0:d}" HeaderText="UploadDate" SortExpression="CreatedOn" />
                                                                <asp:BoundField DataField="CreatedBy" HeaderText="UploadedBy" SortExpression="CreatedBy" />
                                                               
                                                            </Columns><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" Font-Names="Calibri" ForeColor="#8C4510" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><sortedascendingcellstyle backcolor="#E4E4E4" /><sortedascendingheaderstyle backcolor="#000066" /><sorteddescendingcellstyle backcolor="#E4E4E4" /><sorteddescendingheaderstyle backcolor="#000066" /></asp:GridView>
                                <asp:SqlDataSource ID="SqlDSTransferUpload" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblfileupload where fileref = 'aa'"></asp:SqlDataSource>
                                      </td>
                             </tr>
                             <tr>
                                 <td><br /></td>
                             </tr>
                                <tr>
                              <td class="CellFormat">FileName     <asp:Label ID="Label53" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                        </td>
                              <td class="CellTextBox">

                                  <asp:TextBox ID="txtTransferFileName" runat="server" Height="45px" MaxLength="2000" Width="90%" TextMode="MultiLine"></asp:TextBox>
                               </td>
                         </tr>
                               <tr>
                              <td class="CellFormat">From Account     <asp:Label ID="Label52" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                        </td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtTransferAccountFrom" runat="server" Height="45px" MaxLength="2000" Width="90%" TextMode="MultiLine"></asp:TextBox>
                               </td>
                         </tr>
                                <tr>
                              <td class="CellFormat">To Account     <asp:Label ID="Label50" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                        </td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtTransferAccountTo" runat="server" Height="45px" MaxLength="2000" Width="90%" TextMode="MultiLine"></asp:TextBox>
                               </td>
                         </tr>
                             
                            <tr>
                             <td colspan="2"><br /><asp:TextBox id="txtTransferRcno" visible="false" Text="" runat="server"></asp:TextBox></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center">
                                 <asp:Button ID="btnTransferFile" runat="server" CssClass="roundbutton1" OnClientClick="currentdatetime();" BackColor="#CFC6C0"  Font-Bold="True" Text="Transfer" Width="100px"/>
                              <asp:Button ID="btnd" runat="server" CssClass="dummybutton" />

            <asp:Button ID="btnTransferCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

           <asp:ModalPopupExtender ID="mdlTransferFiles" runat="server" CancelControlID="btnTransferCancel" PopupControlID="pnlTransferFiles" TargetControlID="btnTransferDummy" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
    <asp:Button ID="btnTransferDummy" runat="server" CssClass="dummybutton" />

          <%-- End - Transfer files--%>


               <%--start--%>

                  <asp:Panel ID="pnlStatusLoc" runat="server" BackColor="White" Width="580px" Height="300px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Status Change</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageStatusLoc" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertStatusLoc" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         <tr>
                             <td class="CellFormat">AccountID</td>
                             <td class="CellTextBox">
                                 <asp:Label ID="lblStatusLocationID" runat="server" Width="50%" BackColor="#CCCCCC"></asp:Label></td>
                         </tr>
                        <%--  <tr>
                               <td class="CellFormat">Existing Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:Label ID="lblOldStatus" runat="server" width="50%" BackColor="#CCCCCC"></asp:Label></td>
                           </tr>--%>
                         
                          <tr>
                               <td class="CellFormat">New Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlNewStatus" runat="server" Width="51%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                   <asp:ListItem Value="1">Active</asp:ListItem>
                                   <asp:ListItem Value="0">InActive</asp:ListItem> 
                                                                      
                               </asp:DropDownList></td>
                           </tr>
                             <tr>
                              <td class="CellFormat">Reason for Status Change <asp:Label ID="Label49" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                        </td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtStatusLocReason" runat="server" Height="45px" MaxLength="2000" Width="90%" TextMode="MultiLine"></asp:TextBox>
                               </td>
                         </tr>
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnUpdateStatusLoc" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Update Status" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelStatusLoc" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                        
        </table>

                   
        
           </asp:Panel>

           <asp:ModalPopupExtender ID="mdlPopupStatusLoc" runat="server" CancelControlID="btnCancelStatusLoc" PopupControlID="pnlStatusLoc" TargetControlID="btnDummyStatusLoc" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
    <asp:Button ID="btnDummyStatusLoc" runat="server" CssClass="dummybutton" />

          <%-- end--%>
           <%-- START--%>

               <asp:Panel ID="pnlCustExists" runat="server" BackColor="White" Width="650px" Height="650px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
                         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
                          
               <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr> 
               <tr>
                      <td colspan="2" class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                    Record/s with similar name already exists, please check the following   
                      </td>
                           </tr>
                              <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                              <tr>
                             <td colspan="2">
                                 <asp:GridView ID="GridView3" runat="server" Width="544px" CssClass="Centered" >
                          <Columns>
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
                             </td>
                         </tr>
                              <%-- <tr>
                              <td class="CellFormat">Remarks</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="TextBox4" runat="server" Height="45px" MaxLength="2000" Width="90%" TextMode="MultiLine"></asp:TextBox>
                              </td>
                         </tr>--%>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnCustExistsOk" runat="server" CssClass="roundbutton1" OnClientClick="currentdatetime();" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                             
         <%--   <asp:Button ID="Button7" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
         --%>                      </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

           <asp:ModalPopupExtender ID="mdlPopupCustExists" runat="server" CancelControlID="btnCustExistsOk" PopupControlID="pnlCustExists" TargetControlID="btndummyCustExists" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
    <asp:Button ID="btndummyCustExists" runat="server" CssClass="dummybutton" />

          <%-- end--%>

                  <%--Confirm delete uploaded file--%>
                                              
                 <asp:Panel ID="pnlConfirmDeleteUploadedFile" runat="server" BackColor="White" Width="500px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEvent" runat="server" Text="Confirm DELETE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to DELETE the File?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmDelete" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnConfirmDeleteNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               <%-- start: send statement --%>

            <asp:Panel ID="pnlEditSendStatement" runat="server" BackColor="White" Width="40%" Height="80%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Send Statement</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageSendStatement" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertSendStatement" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Account ID</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtAccountIDEdit" runat="server" BackColor="#E0E0E0" ReadOnly="TRUE" Height="16px" MaxLength="25" Width="80%"></asp:TextBox>
                        </td>
                         </tr>
                          <tr>
                              <td class="CellFormat">Client Name</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtNameEdit" runat="server" BackColor="#E0E0E0" Height="16px" ReadOnly="TRUE" MaxLength="25" Width="80%"></asp:TextBox>
                              </td>
                         </tr>
                     
                         <tr>
                             <td class="CellFormat">&nbsp;</td>
                             <td class="CellTextBox">&nbsp;</td>
                         </tr>


            <tr>
            <td class="CellFormat">Credit Terms<asp:Label ID="Label28" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
           </td>
        
        <td class="CellTextBox"><asp:DropDownList ID="ddlTermsEdit" runat="server" AppendDataBoundItems="True" Height="25px" Width="80%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td>

                         </tr>
                         
          <tr><td class="CellFormat">Currency<asp:Label ID="Label31" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
              <td class="CellTextBox"><asp:DropDownList ID="ddlCurrencyEdit" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSCurrency" DataTextField="Currency" DataValueField="Currency" Height="25px" Width="80%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td>

          </tr>
            <tr>
            <td class="CellFormat">Default Invoice Format</td>
            <td class="CellTextBox">
                <asp:DropDownList ID="ddlDefaultInvoiceFormatEdit" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Width="80%">
                    <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                    <asp:ListItem>Format1</asp:ListItem>
                    <asp:ListItem>Format2</asp:ListItem>
                    <asp:ListItem>Format3</asp:ListItem>
                    <asp:ListItem>Format4</asp:ListItem>
                    <asp:ListItem>Format5</asp:ListItem>
                    <asp:ListItem>Format6</asp:ListItem>
                    <asp:ListItem>Format7</asp:ListItem>
                    <asp:ListItem>Format8</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
                              <td class="CellFormat">Send HardCopy</td>
                              <td class="CellTextBox">
                                          <asp:CheckBox ID="chkSendStatementInvEdit" runat="server" Text="Invoice" TextAlign="right" Font-Bold="True" Font-Names="Calibri" Font-Size="15px" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chkSendStatementSOAEdit" runat="server" Text="SOA (Statement of Accounts)" textalign="right" Font-Bold="true" Font-Names="Calibri" Font-Size="15px"/>
                                                        
                               <%--   <asp:CheckBox ID="chkSendStatementEdit" runat="server" />--%>
                              </td>
                         </tr>
        <tr>
            <td class="CellFormat">Auto Email</td>
            <td class="CellTextBox">
                     <asp:CheckBox ID="chkAutoEmailInvoiceEdit" runat="server" Text="Invoice" TextAlign="right" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chkAutoEmailStatementEdit" runat="server" Text="SOA (Statement of Accounts)" textalign="right" Font-Bold="true" Font-Names="Calibri" Font-Size="15px"/>
                                                             
                <%--<asp:CheckBox ID="chkAutoEmailInvoiceEdit" runat="server" />--%>
            </td>
        </tr>
                           <tr>
            <td class="CellFormat">Requires E-Billing</td>
            <td class="CellTextBox">
                <asp:CheckBox ID="chkRequireEBillingEdit" runat="server" />
            </td>
        </tr>
       
 <tr>
                              <td class="CellFormat">Remarks</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtBillingOptionRemarksEdit" runat="server" Height="70px" MaxLength="2000" Width="80%" TextMode="MultiLine"></asp:TextBox>
                              </td>
                         </tr>                    



                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveSendStatement" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"   Font-Bold="True" Text="Save" Width="120px"/>
                                 <asp:Button ID="btnCancelSendStatement" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

                      <asp:ModalPopupExtender ID="mdlPopupEditSendStatement" runat="server" CancelControlID="btnCancelSendStatement" PopupControlID="pnlEditSendStatement" TargetControlID="btnDummySendStatement" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
    <asp:Button ID="btnDummySendStatement" runat="server" CssClass="dummybutton" />

           <%-- end: send statement --%>



        <%--   start: Update Service Contact--%>


     <%--      <asp:Panel ID="pnlImportServices" runat="server" BackColor="White" Width="99%" Height="100%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
     --%>  
       <asp:Panel ID="pnlUpdateServiceContact" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="70%" Height="600px"  BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
       
         
         <table border="0" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;" colspan="4">
                     <asp:Label ID="Label34" runat="server" Text="UPDATE CONTACT INFORMATION OF SERVICE LOCATIONS"></asp:Label>
            </td>

               </tr>
              
             <tr>
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri;" colspan="4">
                     <asp:Label ID="lblAlertContactServiceUpdate" runat="server" Font-Bold="True" ForeColor="Red" Width="90%" ></asp:Label>
                 </td>
             </tr> 

             


                      <tr>
                     <td class="CellFormat" style="text-align:left"><asp:Label ID="Label40" runat="server" Font-Size="14px"  ForeColor="Brown" Height="20px" Text="Service Contact Info"></asp:Label>
                              <td class="CellFormat" style="text-align:center; color:brown">
                                  <asp:CheckBox ID="chkDefaultContactInfo" runat="server" Text="Default From Main Contact Information" AutoPostBack="True" /></td>
                                                   
                          <td class="CellFormat">&nbsp;</td>
                          <td class="CellTextBox">
                                    &nbsp;</td>
                     
                          
                      
                      </tr>
                                   
                        <tr>
                            <td class="CellFormat">Contact Person 1<asp:Label ID="Label39" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                            </td>
                            <td class="CellTextBox">
                                <asp:TextBox ID="txtSvcCP1ContactUpdateContactInformation" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                            </td>
                            <td class="CellFormat">Position</td>
                            <td class="CellTextBox">
                                <asp:TextBox ID="txtSvcCP1PositionUpdateContactInformation" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                            </td>
               </tr>
                                   
                        <tr>
                            <td class="CellFormat">Telephone</td>
                            <td class="CellTextBox">
                                   <asp:TextBox ID="txtSvcCP1TelephoneUpdateContactInformation" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox>
                        
                            </td>
                            <td class="CellFormat">Fax</td>
                            <td class="CellTextBox">
                                <asp:TextBox ID="txtSvcCP1FaxUpdateContactInformation" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                
                            </td>
                         
               </tr>
               <tr>
                   <td class="CellFormat">Telephone2</td>
                   <td class="CellTextBox">
        <asp:TextBox ID="txtSvcCP1Telephone2UpdateContactInformation" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox>
                     
                   </td>
                   <td class="CellFormat">Mobile</td>
                   <td class="CellTextBox">
                         <asp:TextBox ID="txtSvcCP1MobileUpdateContactInformation" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                    
                   </td>
                 
               </tr>
               <tr>
                   <td class="CellFormat">Email</td>
                   <td class="CellTextBox" colspan="3">
                         <asp:TextBox ID="txtSvcCP1EmailUpdateContactInformation" runat="server" Font-Names="calibri" Font-Size="15px" Height="40px" MaxLength="500" TextMode="MultiLine" Width="93%"></asp:TextBox>
                                 <a href='<%= Me.ResolveUrl("mailto:" + txtSvcCP1Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;">
                                     <img src="Images/email1.png" width="20" height="20" />
                                 </a>
                   </td>
                 
               </tr>
               <tr>
                   <td class="CellFormat">Contact Person 2</td>
                   <td class="CellTextBox">
                           <asp:TextBox ID="txtSvcCP2ContactUpdateContactInformation" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                               
                   </td>
                   <td class="CellFormat">Position 2</td>
                   <td class="CellTextBox">
                                <asp:TextBox ID="txtSvcCP2PositionUpdateContactInformation" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                            
                   </td>
                 
               </tr>
               <tr>
                   <td class="CellFormat">Telephone</td>
                   <td class="CellTextBox">
        <asp:TextBox ID="txtSvcCP2TelephoneUpdateContactInformation" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox>
                         
                   </td>
                   <td class="CellFormat">Fax </td>
                   <td class="CellTextBox">
                <asp:TextBox ID="txtSvcCP2FaxUpdateContactInformation" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                    
                   </td>
                
               </tr>
               <tr>
                   <td class="CellFormat">Telephone 2</td>
                   <td class="CellTextBox">
            <asp:TextBox ID="txtSvcCP2Tel2UpdateContactInformation" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox>
                     
                   </td>
                   <td class="CellFormat">Mobile</td>
                   <td class="CellTextBox">
                  <asp:TextBox ID="txtSvcCP2MobileUpdateContactInformation" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                 
                   </td>
               
               </tr>
               <tr>
                   <td class="CellFormat">Email (Maximum 100 Chars.)</td>
                   <td class="CellTextBox" colspan="3">
                            <asp:TextBox ID="txtSvcCP2EmailUpdateContactInformation" runat="server" Font-Names="calibri" Font-Size="15px" Height="40px" MaxLength="100" TextMode="MultiLine" Width="92%"></asp:TextBox>
                                    <a href='<%= Me.ResolveUrl("mailto:" + txtSvcCP2Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;">
                                  <img height="20" src="Images/email1.png" width="20" />
                         </a>
                   </td>
                 
               </tr>
               <tr>
                   <td class="CellFormat">Email CC</td>
                   <td class="CellTextBox" colspan="3">
                               <asp:TextBox ID="txtSvcEmailCCUpdateContactInformation" runat="server" Font-Names="calibri" Font-Size="15px" Height="50px" MaxLength="500" TextMode="MultiLine" Width="92%"></asp:TextBox>
                             
                   </td>
                 
               </tr>
                                   
                   
                    
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100% " colspan="4">
        
        
        <asp:UpdatePanel ID="updpnlServiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="grvServiceRecDetails" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="100%">
                
                  <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" TextAlign="Right" OnCheckedChanged="chkAll_CheckedChanged"  Width="5%" ></asp:CheckBox></HeaderTemplate>
                <ItemTemplate><asp:CheckBox ID="chkGrid" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false"    onchange="checkoneservicerec()"  CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
             
               <asp:TemplateField HeaderText="Contract Group"><ItemTemplate><asp:TextBox ID="txtContractGroupGV" runat="server" Text='<%# Bind("ContractGroup")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="120px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Location Id"><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Text='<%# Bind("LocationID")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="100px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Text='<%# Bind("Address1")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="600px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
               <asp:TemplateField HeaderText="Postal"><ItemTemplate><asp:TextBox ID="txtServiceRecordNoGV" runat="server" Text='<%# Bind("AddPostal")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordGV" runat="server"  Text='<%# Bind("Rcno")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                                                                             
            
                         </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
            </ContentTemplate>
         <%--   <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grvServiceRecDetails" EventName="SelectedIndexChanged" />

            </Triggers>--%>

        </asp:UpdatePanel>&nbsp;</td></tr>
               
            <tr>
                <td colspan="4">
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  </asp:SqlDataSource>
                </td>
            </tr>
        </table>


          <%-- </asp:Panel>--%>

                    <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                      
                         
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                       <asp:Button ID="btnUpdate" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="UPDATE" Width="15%" Visible="True" />
                       &nbsp;  
                           <asp:Button ID="btnCancelServiceContact" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CANCEL" Width="15%" Visible="True" />
                    
                      
                        </td>
                 
            </tr>
                    </table>

                 </asp:Panel>

           <asp:ModalPopupExtender ID="mdlUpdateServiceContact" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnCancelServiceContact" PopupControlID="pnlUpdateServiceContact" TargetControlID="btnDummyUpdateServiceContact" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   <asp:Button ID="btnDummyUpdateServiceContact" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                   
          <%-- end:Update Service Contact--%>



          <%-- 09.09.20--%>

                 <%--start Salesman--%>


         <div>
          <asp:ModalPopupExtender ID="mdlImportServices" runat="server" BackgroundCssClass="modalBackground" CancelControlID="" PopupControlID="pnlImportServices" TargetControlID="btnDummyImportService">
            </asp:ModalPopupExtender> 
  
   <asp:Button ID="btnDummyImportService" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" /> 
      
             <asp:Panel ID="pnlImportServices" runat="server" BackColor="White" Width="90%" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
                     

               <%--  aaa--%>

                   <table border="0"  style="width:100%;text-align:center; padding-top:1px; margin-left: 0%; margin-right:0%; border:solid; border-color:ButtonFace; "  >

                 <tr style="width:100%">
                 <td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;"> UPDATE SALESMAN</td>
                  </tr>
             
                    <tr>
                            <td style="width:3%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                <asp:SqlDataSource ID="SqlDSContratESM" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                            </td>

                          <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:SqlDataSource ID="SqlDSInvoiceESM" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                            </td>
                          <td colspan="4"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertEditSalesman" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td>
                                        
                     </tr>
            
                        </td>
                              <tr>
                                  <td style="width:3%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                                  <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                      &nbsp;</td>
                                  <td style="width:4%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                                  <td style="width:15%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                      &nbsp;</td>
                                  <td style="width:4%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                                  <td style="width:14%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                      &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:3%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Location ID</td>
                                <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtLocationIDEditSalesman" runat="server" AutoCompleteType="Disabled" Height="16px" Width="75%"></asp:TextBox>
                                </td>
                                <td style="width:4%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Name</td>
                                <td style="width:15%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtCustomerNameEditSalesman" runat="server" AutoCompleteType="Disabled" Height="16px" Width="93%"></asp:TextBox>
                                </td>
                                <td style="width:4%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Service Address</td>
                                <td style="width:14%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtServiceAddressEditSalesman" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%"></asp:TextBox>
                                </td>
                       </tr>
                            <tr>
                                <td style="width:3%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                                <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtOldSalesman" runat="server" AutoCompleteType="Disabled" Height="16px" Width="75%" Visible="False"></asp:TextBox>
                                </td>
                                <td style="width:4%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">New Salesman<asp:Label ID="Label68" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                </td>
                                <td style="width:12%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlNewSalesman" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Frequency" DataValueField="Frequency" Height="25px" Value="-1" Width="95%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:4%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Effective Date<asp:Label ID="Label69" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                </td>
                                <td style="width:12%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">

                                    <asp:TextBox ID="txtEffectiveDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="30%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtEffectiveDate" TargetControlID="txtEffectiveDate" />
                                </td>
                       </tr>
                      
                        <tr>
                            <td colspan="1" style="width:3%;text-align:left;color:brown;font-size:14px;font-weight:bold;font-family:'Calibri';">
                                &nbsp;</td>
                            <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                </td>
                            <td style="width:4%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                &nbsp;</td>
                            <td  style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            </td>

                              <td style="width:4%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                &nbsp;</td>
                            <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                <asp:Button ID="btnShowRecords" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SHOW RECORDS" Width="50%" />
                            </td>
                       </tr>
                        </tr>

            </table>
    
        

             <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="99%" Height="200px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
       
         
         <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label42" runat="server" Text="CONTRACTS TO BE UPDATED"></asp:Label>
            </td>

               </tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100%; padding-left:8% ">
        
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 

            <asp:GridView ID="GridViewContractESM" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="70%">
                
                <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="chkSelectAllContractESMGV" runat="server" AutoPostBack="false" TextAlign="Right" onchange="checkallcontractESM()" Width="5%" ></asp:CheckBox></HeaderTemplate>
               <ItemTemplate><asp:CheckBox ID="chkSelectContractESMGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" 
 CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
            
                        <asp:TemplateField HeaderText="Branch"><ItemTemplate><asp:TextBox ID="txtBranchCESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("Location")%>' BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="123px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                       <asp:TemplateField HeaderText="ContractNo"><ItemTemplate><asp:TextBox ID="txtContractNoCESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("ContractNo")%>' BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="155px"></asp:TextBox></ItemTemplate></asp:TemplateField>                             
                        <asp:TemplateField HeaderText="Salesman"><ItemTemplate><asp:TextBox ID="txtAccountNameCESMGV" runat="server" Visible="true" Height="15px" Text='<%# Bind("Salesman")%>' Width="250px" Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None"  ></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                      <asp:TemplateField HeaderText="StartDate"><ItemTemplate><asp:TextBox ID="txtStartDateCESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy}")%>' BackColor="#CCCCCC" BorderStyle="None"  Height="18px" Width="72px"  style="text-align:center" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="EndDate"><ItemTemplate><asp:TextBox ID="txtEndDateCESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("EndDate", "{0:dd/MM/yyyy}")%>' BackColor="#CCCCCC" BorderStyle="None"  Height="18px" Width="72px"  style="text-align:center" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                     <asp:TemplateField HeaderText="ActualEnd"><ItemTemplate><asp:TextBox ID="txtActualEndCESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("ActualEnd", "{0:dd/MM/yyyy}")%>' BackColor="#CCCCCC" BorderStyle="None"  Height="18px" Width="72px"  style="text-align:center" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Duration" ><ItemTemplate><asp:TextBox ID="txtDurationCESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("Duration")%>' BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="50px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Contract Amount" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtContractAmountCESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("AgreeValue")%>' BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="110px" Align="right"></asp:TextBox></ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                        <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoContractCESMGV" runat="server" Visible="false" Height="15px" Width="0px" Text='<%# Bind("rcnoContract")%>'></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                
               </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" />

            </asp:GridView>
          

    
</div>
            </ContentTemplate>
            
            </asp:UpdatePanel>&nbsp;</td></tr>
             

             
              <tr>
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="btnSelect" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                  
            </tr>

               
            
        </table>


                 </asp:Panel>


                <%-- Invoices--%>

          <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="99%" Height="200px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
       
         
         <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label41" runat="server" Text="INVOICES TO BE UPDATED"></asp:Label>
            </td>

               </tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100%; padding-left:8% ">
        
        
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="GridViewInvoiceESM" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px" 
            ShowFooter="True" Style="text-align: left" Width="70%">
                
                <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="chkSelectAllInvoiceESMGV" runat="server" AutoPostBack="false" TextAlign="Right" onchange="checkallinvoiceESM()"  Width="5%" ></asp:CheckBox></HeaderTemplate>
               <ItemTemplate><asp:CheckBox ID="chkSelectInvoiceESMGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" 
 CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
            
                        <asp:TemplateField HeaderText="Branch"><ItemTemplate><asp:TextBox ID="txtBranchIESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("Location")%>'  BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="123px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                       <asp:TemplateField HeaderText="Invoice No."><ItemTemplate><asp:TextBox ID="txtInvoiceNoIESMGV" runat="server" Font-Size="12px" ReadOnly="true"  BackColor="#CCCCCC" BorderStyle="None" Height="18px" Text='<%# Bind("InvoiceNumber")%>'  Width="155px"></asp:TextBox></ItemTemplate></asp:TemplateField>                             
                        <asp:TemplateField HeaderText="Salesman"><ItemTemplate><asp:TextBox ID="txtAccountNameCESMGV" runat="server" Visible="true" Height="15px" Text='<%# Bind("StaffCode")%>' Width="250px" Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None"  ></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                       <asp:TemplateField HeaderText="Invoice Date"><ItemTemplate><asp:TextBox ID="txtInvoiceDateIESMGV" runat="server" Font-Size="12px" ReadOnly="true"  BackColor="#CCCCCC" BorderStyle="None" Text='<%# Bind("SalesDate", "{0:dd/MM/yyyy}")%>' Height="18px" Width="72px"  style="text-align:center" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Terms" ><ItemTemplate><asp:TextBox ID="txtTermsIESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("Terms")%>' BackColor="#CCCCCC" BorderStyle="None" style="text-align:left" Height="18px" Width="150px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Due Date"><ItemTemplate><asp:TextBox ID="txtDueDateIESMGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("DueDate", "{0:dd/MM/yyyy}")%>' BackColor="#CCCCCC" BorderStyle="None"  Height="18px" Width="72px"  style="text-align:center" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoSalesIESMGV" runat="server" Visible="false" Height="15px" Width="0px" Text='<%# Bind("rcnoInvoice")%>'></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                
               </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>&nbsp;</td></tr>
             

             
              <tr>
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="Button2" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                  
            </tr>
   
        </table>


                 </asp:Panel>

                 <%--Invoices--%>


                   <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                        <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="Button5" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                         
                    <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right">
                            &nbsp;</td>

                   <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left">
                       &nbsp;</td>
                 
            </tr>
                    </table>
                <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace; background-color:white;">
                     <tr>
                      
                         
                  <td  style="width:84%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center; background-color:white" >
                     
                      <asp:SqlDataSource ID="SqlDSServices" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select A.Rcno,  A.RecordNo, A.AccountID, A.ContractNo, A.CustName,   A.BillNo,  A.CompanyGroup, A.LocationID , A.BillAmount
 FROM tblServiceRecord A  where 1 = 1  ">
                            </asp:SqlDataSource>
                        <asp:Button ID="btnUpdateSalesman" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="UPDATE" Width="15%" Visible="True" />
                       &nbsp;  
                           <asp:Button ID="Button6" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CANCEL" Width="15%" Visible="True" />
                    
                      
                        </td>
                 
            </tr>
            </table>

                <%-- bbb--%>

                 </asp:Panel>

          </div>

                      <%--end Salesman--%>

          <%-- 09.09.20--%>



               <%--'28.02--%>

        
            <asp:Panel ID="pnlWarning" runat="server" BackColor="White" Width="470px" Height="210px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center" colspan="2">
                         
                          <asp:Label ID="lblEditAgreeValue" runat="server" Text="WARNING"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2" style="text-align:center; margin-left:auto; margin-right:auto;">
                                 <asp:Label ID="lblWarningAlert" runat="server" Font-Bold="True" ForeColor="Red" Width="90%" ></asp:Label>
                                 /td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto; margin-top:10px" colspan="2">
                         
                          &nbsp;<asp:Label ID="Label36" runat="server" Text="Do you wish to update the Contact Information for all the selected sites?"></asp:Label>
                        
                      </td>
                           </tr>

             

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="lblLine3EditAgreeValueSave" runat="server"></asp:Label>
                        
                      </td>
                           </tr>


               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="lblLine4EditAgreeValueSave" runat="server" Text="Please Enter "  Visible="True"></asp:Label>
                        <asp:Label ID="lblRandom" runat="server" Visible="True"></asp:Label>
                        <asp:Label ID="Label37" runat="server" Text=" in the box below and click OK to confirm." Visible="True"></asp:Label>
                        
                      </td>
                           </tr>

             
                  

                   <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="Label38" runat="server"></asp:Label>
                        
                      </td>
                           </tr>


               <tr>

                   <td class="CellFormat" style="text-align:right; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="lblLine5EditAgreeValueSave" runat="server" Text="Confirmation Code " Visible="True"></asp:Label>
                   </td>
                   <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto;">
                       <asp:TextBox ID="txtConfirmationCode" runat="server" Visible="True" Width="30%"></asp:TextBox>
                   </td>
                  <tr>
                      <td colspan="2">
                          <br />
                      </td>
                  </tr>
                  <tr style="padding-top:40px;">
                      <td colspan="2" style="text-align:center">
                          <asp:Button ID="btnEditServiceContactSaveYes" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="calculateportfoliovaluesAgreeValueEdit()" Text="OK" Width="100px" />
                          <asp:Button ID="btnEditServiceContactSaveNo" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                      </td>
                  </tr>
            
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlWarning" runat="server" CancelControlID="" PopupControlID="pnlWarning" TargetControlID="btnDummyWarning" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyWarning" runat="server" CssClass="dummybutton" />

             <%-- End: SAVE: Edit Agree Value --%>

           
                      <asp:Panel ID="pnlImportData" runat="server" BackColor="White" Width="470px" Height="210px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center" colspan="2">
                         
                          <asp:Label ID="Label48" runat="server" Text="IMPORT DATA"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2" style="text-align:center; margin-left:auto; margin-right:auto;">
                                 <asp:Label ID="lblAlertImportData" runat="server" Font-Bold="True" ForeColor="Red" Width="90%" ></asp:Label>
                                 /td>
                         </tr> 

               <tr>
                  <td class="CellFormat"> Sample Templates</td>
                  <td style="text-align:left;padding-left:5%">
                       <asp:ImageButton ID="btnCorporateTemplate" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                     <br /><br />
                  </td>
              </tr>

               <tr>
                                                                <td class="CellFormat">Select Excel File to Import Data </td>
                                                                <td class="CellTextBox" colspan="1" style="text-align:center;padding-left:3%">
                                                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="Centered" Width="100%" />
                                                                </td>
                                                            </tr>
              <tr>
                                                                <td colspan="2">
                                                                    <br />
                                                                </td>
                                                            </tr>
             <tr>
                                                                <td class="centered" colspan="2">
                                                                    <asp:Button ID="btnExcelUpload" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" OnClientClick="currentdatetime()" Font-Bold="True" width="100px" Text="UPLOAD" />
                                                                         <asp:Button ID="btnExcelCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" />
                                                 
                                                                </td>
                                                            </tr>

        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlImportData" runat="server" CancelControlID="" PopupControlID="pnlImportData" TargetControlID="btnDummyImportData" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyImportData" runat="server" CssClass="dummybutton" />

    
       <asp:Panel ID="pnlConfirmMsg" runat="server" BackColor="White" Width="300px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
                 
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label47" runat="server" Text="The Statement of Account is being generated <br/> <br/> and will be emailed shortly."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmOk" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelMsg" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />



        <%--'28.02--%>

      <asp:ModalPopupExtender ID="mdlPopupDeleteUploadedFile" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmDeleteUploadedFile" TargetControlID="btndummyDeleteUploadedFile" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyDeleteUploadedFile" runat="server" CssClass="dummybutton" />

             <%-- Confirm Delete uploaded file--%>
          
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
                           
                             <asp:DropDownList ID="ddlSalesGrp" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDSSalesGroup" DataTextField="salesgroup" DataValueField="salesgroup" Height="25px" Width="155px" Visible="False">
                                     <asp:ListItem Text="--SELECT--" Value="-1" />
                                 </asp:DropDownList>
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
      
         
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                </asp:SqlDataSource>

       
        
                 <%--$(document).ready(function () { $("#ddlIndustry").select2(); });--%>
              <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSSalesGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT salesgroup FROM tblsalesgroup order by salesgroup">
                       
            </asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
              <asp:SqlDataSource ID="SqlDSSalesMan" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
              <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
             <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
            
             <asp:SqlDataSource ID="SqlDSState" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT State FROM tblstate WHERE (Rcno &lt;&gt; 0) ORDER BY State"></asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSCity" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSCountry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
             <asp:SqlDataSource ID="SqlDSTerms" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
       
             <asp:SqlDataSource ID="SqlDSCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Currency FROM tblCurrency   ORDER BY Currency "></asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSARBal" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT AccountId, Bal FROM companybal"></asp:SqlDataSource>
           <asp:TextBox ID="txtGoogleEmail" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="False"></asp:TextBox>

       <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>
             <asp:TextBox ID="txtDetail" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>
           <asp:TextBox ID="txtSelectDate" runat="server" Visible="False"></asp:TextBox>
        <%-- <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>--%>
        <asp:TextBox ID="txtSelectedRow" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>   
           <asp:TextBox ID="txtDisplayTimeInTimeOutServiceRecord" runat="server" Visible="False"></asp:TextBox>        
             
        <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox> 
           
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White"  ></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
                    <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtModal" runat="server" Visible="False"></asp:TextBox>
              <asp:TextBox ID="TextBox3" runat="server" Visible="false" ></asp:TextBox>
        <asp:TextBox ID="txtPostalValidate" runat="server" Visible="False"></asp:TextBox>

              <%--   <asp:Panel ID="pnlEditFileDesc" runat="server" BackColor="WHITE" ForeColor="BLACK" Width="70%" Height="45%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
            
                     <table style="width:100%;padding-left:15px;background-color:white;color:black;">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit File Description</h4>
                             </td>
                         </tr>
                        <%--  <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageTeam" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertTeam" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>--%>
                   <%--         <tr>
                      <td class="CellFormat">FileName</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtEditFileName" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>
                           
                      </td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">Description</td>
                    <td class="CellTextBox"> <asp:TextBox ID="txtEditFileDescription" runat="server" Height="16px" MaxLength="25" Width="90%"></asp:TextBox>
                           </td>
                                         
                  </tr>

           
                        
                         <tr>
                             <td colspan="2">
                                 <asp:TextBox ID="txtfilercno" runat="server" CssClass="dummybutton"></asp:TextBox><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditFileDescSave" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px"/>
                                 <asp:Button ID="btnEditFileDescCancel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEditFileDesc" runat="server" CancelControlID="btnEditFileDescCancel" PopupControlID="pnlEditFileDesc" TargetControlID="btndummyEditFileDesc" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyEditFileDesc" runat="server" cssclass="dummybutton" />
   --%>
    
    </div>
            </ContentTemplate> 
          <Triggers>
            <asp:PostBackTrigger ControlID="tb1$TabPanel3$btnUpload" />
             <asp:PostBackTrigger ControlID="tb1$TabPanel3$gvUpload" />
               <%--<asp:PostBackTrigger ControlID="tb1$TabPanel3$btnPhotoPreview" />--%>
        </Triggers>
     </asp:UpdatePanel>
    </asp:Content>
