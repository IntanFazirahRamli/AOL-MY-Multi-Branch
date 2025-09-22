<%@ page title="Residential" language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" CodeFile="Person.aspx.vb" inherits="Person" culture="en-GB" enableeventvalidation="false" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>



<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      
     <style type="text/css">
         .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}
    
    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:30%;
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
      .uppercase{
        text-transform: uppercase;
    }

     .cell{
        text-align:left;
    }

    .righttextbox{
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
        if (confirm("Do you wish to DELETE Account ID : " + document.getElementById("<%=txtAccountID.ClientID%>").value + " Name : " + document.getElementById("<%=txtNameE.ClientID%>").value + "?")) {
                 confirm_value.value = "Yes";

             } else {
                 confirm_value.value = "No";
             }

             document.forms[0].appendChild(confirm_value);
         }

         function ConfirmDeleteSvc() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (document.getElementById("<%=txtDescription.ClientID%>").value == '') {
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


        function TabChanged(sender, e) {
        //       if (sender.get_activeTabIndex() != 0) {
        //            //document.getElementById('<%=GridView1.ClientID()%>').style.display = 'none';

            //           if (document.getElementById("<%=txtAccountID.ClientID%>").value == '') {
                //               $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                //               alert("Please select a customer record to proceed.");
                //               return;
                //           }

            //document.getElementById("<%=btnADD.ClientID%>").style.display = 'none';
            //   document.getElementById("<%=btnCopyAdd.ClientID%>").style.display = 'none';
            //document.getElementById("<%=btnDelete.ClientID%>").style.display = 'none';
            //document.getElementById("<%=btnFilter.ClientID%>").style.display = 'none';
            //document.getElementById("<%=btnContract.ClientID%>").style.display = 'none';
            //document.getElementById("<%=btnResetSearch.ClientID%>").style.display = 'none';
            //  document.getElementById("<%=txtSearchCust.ClientID%>").style.display = 'none';
    
            //       }
     
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

        document.getElementById("<%=txtCreatedOn.ClientID%>").value = '';
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
          
            document.getElementById("<%=txtBillAddress.ClientID%>").value = document.getElementById("<%=txtOffAddress1.ClientID%>").value;
            document.getElementById("<%=txtBillStreet.ClientID%>").value = document.getElementById("<%=txtOffStreet.ClientID%>").value;
            document.getElementById("<%=txtBillBuilding.ClientID%>").value = document.getElementById("<%=txtOffBuilding.ClientID%>").value;

            document.getElementById("<%=ddlBillCity.ClientID%>").value = document.getElementById("<%=ddlOffCity.ClientID%>").value;
            document.getElementById("<%=ddlBillState.ClientID%>").value = document.getElementById("<%=ddlOffState.ClientID%>").value;
            document.getElementById("<%=ddlBillCountry.ClientID%>").value = document.getElementById("<%=ddlOffCountry.ClientID%>").value;

            document.getElementById("<%=txtBillPostal.ClientID%>").value = document.getElementById("<%=txtOffPostal.ClientID%>").value;

            document.getElementById("<%=txtBillCP1Contact.ClientID%>").value = document.getElementById("<%=txtOffContactPerson.ClientID%>").value;
            document.getElementById("<%=txtBillCP1Mobile.ClientID%>").value = document.getElementById("<%=txtOffMobile.ClientID%>").value;

            document.getElementById("<%=txtBillCP1Tel.ClientID()%>").value = document.getElementById("<%=txtOffContactNo.ClientID%>").value;
            document.getElementById("<%=txtBillCP1Fax.ClientID%>").value = document.getElementById("<%=txtOffFax.ClientID%>").value;
            document.getElementById("<%=txtBillCP1Tel2.ClientID%>").value = document.getElementById("<%=txtOffContact2.ClientID%>").value;

            document.getElementById("<%=txtBillCP1Email.ClientID%>").value = document.getElementById("<%=txtOffEmail.ClientID%>").value;

            document.getElementById("<%=txtBillCP2Contact.ClientID%>").value = document.getElementById("<%=txtOffCont1Name.ClientID%>").value;
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
             document.getElementById("<%=txtSvcCP1Mobile.ClientID%>").value = document.getElementById("<%=txtOffMobile.ClientID%>").value;

             document.getElementById("<%=txtSvcCP1Telephone.ClientID()%>").value = document.getElementById("<%=txtOffContactNo.ClientID%>").value;
             document.getElementById("<%=txtSvcCP1Fax.ClientID%>").value = document.getElementById("<%=txtOffFax.ClientID%>").value;
             document.getElementById("<%=txtSvcCP1Telephone2.ClientID%>").value = document.getElementById("<%=txtOffContact2.ClientID%>").value;

             document.getElementById("<%=txtSvcCP1Email.ClientID%>").value = document.getElementById("<%=txtOffEmail.ClientID%>").value;

             document.getElementById("<%=txtSvcCP2Contact.ClientID%>").value = document.getElementById("<%=txtOffCont1Name.ClientID%>").value;
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
            document.getElementById("<%=txtBilltelephone12Svc.ClientID%>").value = document.getElementById("<%=txtBillCP1Tel2.ClientID%>").value;

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


     function DoValidation(parameter) {

         //var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
         var valid = true;

         currentdatetime();

    
         //          var expr1 = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
         //          var valid = true;

         //         currentdatetime();


    //     var strOfficeEmail = document.getElementById("<%=txtOffEmail.ClientID%>").value;

   //     if (strOfficeEmail != "") {
    //        if (expr1.test(strOfficeEmail)) {
   //             valid = true;
   //         }
   //         else {
   //             valid = false;
   //             document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Email Residential Contact-1";
   //             ResetScrollPosition();
   //             document.getElementById("<%=txtOffEmail.ClientID%>").focus();
   //             return valid;
   //         }
   //     }


    //    var strOfficeEmail2 = document.getElementById("<%=txtOffCont1Email.ClientID%>").value;

         //       if (strOfficeEmail2 != "") {
         //           if (expr1.test(strOfficeEmail2)) {
         // valid = true;
         //           }
         //         else {
         //               valid = false;
         //            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Email Residential Contact-2";
         //           ResetScrollPosition();
         //             document.getElementById("<%=txtOffCont1Email.ClientID%>").focus();
         //            return valid;
         //         }
    //      }


    //      var strBillCP1email = document.getElementById("<%=txtBillCP1Email.ClientID%>").value;

         //      if (strBillCP1email != "") {
         //      if (expr1.test(strBillCP1email)) {
                 //           valid = true;
         //            }
         //       else {
         //           valid = false;
         //         document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Email Billing Contact-1";
         //          ResetScrollPosition();
         //          document.getElementById("<%=txtBillCP1Email.ClientID%>").focus();
         //          return valid;
    //        }
    //   }


         //   var strBillCP2email = document.getElementById("<%=txtBillCP2Email.ClientID%>").value;

         //    if (strBillCP2email != "") {
         //      if (expr1.test(strBillCP2email)) {
                 //            valid = true;
                 //        }
                 //        else {
                 // valid = false;
                 //            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Email Billing Contact-2";
                 //           ResetScrollPosition();
                 //            document.getElementById("<%=txtBillCP2Email.ClientID%>").focus();
                 //             return valid;
                 //          }
             //      }
                  return valid;
              };


        function DoServiceValidation(parameter) {

             var valid = true;


        currentdatetime();
        return valid;
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
              height: 39px;
          }
          .auto-style2 {
              color: black;
              text-align: left;
              font-family: Calibri;
              height: 39px;
              padding-left: 20px;
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
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">RESIDENTIAL (CUSTOMER)</h3>
       
             <table style="width:100%;text-align:center;">
            <tr>
                <td colspan="8"><br /></td>
            </tr>
            <tr>
               <td colspan="8" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
                 <tr>
               <td colspan="8" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
            <tr>
                 <td colspan="8" style="width:100%;text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="txtMode" runat="server" Text="" CssClass="dummybutton" ></asp:Label>
                      </td> 
            </tr>
            <tr>
                <td style="width:100%;text-align:left;" colspan="8"> 
                    <table style="width:100%">
                        <tr>

                    <td style="width:12%;text-align:left;">
                    <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="90px" />
                      
                         <asp:Button ID="btnCopyAdd" runat="server" Font-Bold="True" Text="EDIT" Width="90px"  CssClass="roundbutton1" BackColor="#CFC6C0" visible="TRUE"  />
               
                    <asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="90px" OnClientClick = "currentdatetime(); ConfirmDelete()"  Visible="False"/>
               
                     <asp:Button ID="btnFilter" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="90px" Visible="True"  />
              
                           <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CH. STATUS" Width="92px" />
                  
                         <asp:Button ID="btnContract" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CONTRACT" Width="115px" Visible="False" />
                                            <asp:Button ID="btnTransactions" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="TRANSACTIONS" Width="120px" Visible="true"  />
                
                        <asp:Button ID="btnUpdateServiceContact" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="UPDATE SERVICE CONTACT" Width="200px" Visible="true" />
                         </td>            
                      
                       
                 <td style="width:8%;text-align:right;"> <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px"  />
               </td>
                        </tr>
                    </table> </td>
            </tr>
                 <tr>
            <td colspan="8" style="text-align:right">
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
                    <td style="text-align:left;width:35%"> <asp:TextBox ID="txtSearchCust" runat="server" Width="350px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True"></asp:TextBox>      
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

                       <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Id" Font-Size="15px"  AllowSorting="True" AllowPaging="True" CssClass="Centered" Font-Names="Calibri" RowStyle-CssClass="gridcell" Width="100%" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
       
            <Columns>
            <%--    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                <ControlStyle Width="35px" />
                      <HeaderStyle Width="20px" Wrap="False" />
                <ItemStyle Width="35px" wrap="false" HorizontalAlign="Left"/>
                </asp:CommandField>
                  <asp:TemplateField HeaderText="InActive">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkINA" runat="server" Enabled="false" Checked='<%# Eval("Inactive")%>' />
                          </ItemTemplate>
                  </asp:TemplateField>
               <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" ReadOnly="True">
                       <ControlStyle Width="10%" />
                  <HeaderStyle Width="150px" Wrap="False" />
                  <ItemStyle Width="10%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Id" SortExpression="Id" ReadOnly="True">
                       <ControlStyle Width="10%" CssClass="dummybutton" />
                  <HeaderStyle Width="150px" Wrap="False" CssClass="dummybutton" />
                  <ItemStyle Width="10%" Wrap="false" CssClass="dummybutton" />
                </asp:BoundField>
                <asp:BoundField DataField="Rcno">
                <ControlStyle CssClass="dummybutton" />
                <HeaderStyle CssClass="dummybutton" />
                <ItemStyle CssClass="dummybutton" />
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                  <ControlStyle Width="28%" />
                  <HeaderStyle Width="450px" Wrap="False" />
                    <ItemStyle Width="28%" Wrap="False" HorizontalAlign="Left" />
                </asp:BoundField>
                 
                <asp:BoundField DataField="ArCurrency" HeaderText="Currency" sortexpression="ArCurrency">
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Bal" DataFormatString="{0:N2}" SortExpression="Bal" HeaderText="Balance" >
                 
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                 
                  <asp:BoundField DataField="Location" HeaderText="Location" >
                  <HeaderStyle Wrap="False" />
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                 
                <asp:BoundField DataField="TelMobile" HeaderText="Telephone" SortExpression="TelMobile">
                <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="TelFax" HeaderText="Fax" SortExpression="TelFax" >
                
                 <ItemStyle Wrap="False" HorizontalAlign="Left" />
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

                


                   <asp:BoundField DataField="AddPostal" HeaderText="Postal" SortExpression="AddPostal" >
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
                    <ItemStyle Font-Names="Calibri" Width="250px" HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>

                  <asp:BoundField DataField="BillPostal" HeaderText="Bill Postal" SortExpression="BillPostal" >
                  <ItemStyle Wrap="False" />
                </asp:BoundField>
                    <asp:BoundField DataField="Nric" HeaderText="NRIC" SortExpression="Nric"  Visible="true" >
                       <ControlStyle Width="10%" />
                  <HeaderStyle Width="150px" Wrap="False" />
                  <ItemStyle Width="10%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                                  <asp:BoundField DataField="ICType" HeaderText="ICType" SortExpression="ICType" Visible="true" >
                        <ControlStyle Width="10%" />
                  <HeaderStyle Width="150px" Wrap="False" />
                  <ItemStyle Width="10%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="Nationality" HeaderText="Nationality" SortExpression="Nationality"  Visible="true" >
                       <ControlStyle Width="10%" />
                  <HeaderStyle Width="150px" Wrap="False" />
                  <ItemStyle Width="10%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="Sex" HeaderText="Sex" SortExpression="Sex" Visible="false" />
                 <asp:BoundField DataField="LocateGRP" HeaderText="LocateGRP" SortExpression="LocateGRP" Visible="false" />
                  <asp:BoundField DataField="PersonGroup" HeaderText="PersonGroup" SortExpression="PersonGroup" >
                       <ControlStyle Width="5%" />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="5%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                   <asp:BoundField DataField="AccountNo" HeaderText="AccountNo" SortExpression="AccountNo" visible="false"/>
                  <asp:BoundField DataField="Salesman" SortExpression="Salesman" >
                       <ControlStyle Width="11%" CssClass="dummybutton" />
                  <HeaderStyle Width="150px" Wrap="False" CssClass="dummybutton" />
                  <ItemStyle Width="11%" Wrap="false" HorizontalAlign="Left" CssClass="dummybutton" />
                </asp:BoundField>
                 
                  <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" >
                       <ControlStyle Width="7%" />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="7%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" Visible="false" />
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
                
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>

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
                    <asp:Label ID="lbl31To60" runat="server" Text="31 - 60"></asp:Label> 
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lbl61To90" runat="server" Text="61 - 90"></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lbl91To180" runat="server" Text="91 - 180"></asp:Label>
                </td>
                 <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lblMoreThan180" runat="server" Text="More Than 180"></asp:Label>
                </td>
                <td style="text-align:right;color:red;font-weight:bold;width:40px; font-family:Calibri;font-size:15px;">
                    <asp:Label ID="lblTotalSum" runat="server" Text="Total" ></asp:Label>

                </td>
                    <td style="text-align:center;color:red;font-weight:bold; width:100px;">&nbsp;</td>
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
                             <td style="text-align:center;color:red;font-weight:bold; width:100px;">&nbsp;</td>
                     
                 </tr>
            <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:20px;" colspan="8">
<asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="0" Font-Names="Calibri" Height="100%" Width="100%" CssClass="ajax__tab_xp" OnClientActiveTabChanged="TabChanged" AutoPostBack="True">
    <asp:TabPanel runat="server" HeaderText=" General & Billing Info" ID="TabPanel1">
        <HeaderTemplate>
            General &amp; Billing Info
        </HeaderTemplate>
        
        <ContentTemplate>
            
  <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                 
  <tr>
                     

                    <td class="CellFormat" style="width:38%">
                        <asp:Label ID="Label65" runat="server" Text="Master Branch"></asp:Label>
                        <asp:Label ID="Label22" runat="server" visible="False" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBox">
                        <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="20%" AutoPostBack="True">
		<asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList>

                    </td>
                     

                 </tr>


                 <tr>
                     

                    <td class="CellFormat" style="width:38%">Account ID<asp:Label ID="Label23" runat="server" visible="False" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBox"><asp:TextBox ID="txtAccountID" ReadOnly="True" Enabled="False" runat="server" MaxLength="10" Height="16px" Width="20%" BackColor="#CCCCCC"></asp:TextBox></td>
                     

                 </tr>
                 

                <tr>
                    

                    <td class="CellFormat">Status</td>
                    

                    <td class="CellTextBox"><asp:DropDownList ID="ddlStatus" runat="server" Width="20%" CssClass="chzn-select"><asp:ListItem Selected="True" Value="O">O - Open</asp:ListItem></asp:DropDownList><asp:CheckBox ID="chkInactive" runat="server" Text=" Inactive" CssClass="CellFormat" /></td>

                    



                </tr>
                 

                <tr style="display:none">
                    

                    <td class="CellFormat">Person Group<asp:Label ID="Label24" runat="server" Font-Size="18px" ForeColor="Red" Text="*" Height="18px"></asp:Label></td>
                    

                    <td class="CellTextBox">
                        <asp:DropDownList ID="ddlCompanyGrp" runat="server" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Width="20%" Height="25px" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                     </asp:DropDownList> 
                    </td>

                </tr>
                 
                <tr>
                    

                    <td class="CellFormat">Name<asp:Label ID="Label25" runat="server" Text="*" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtNameE" runat="server" MaxLength="200" Height="16px" Width="58%" AutoPostBack="True"></asp:TextBox></td>
              
                </tr>
           
                <tr>
          
                    <td class="CellFormat">Old Name</td>
             
                    <td class="CellTextBox"><asp:TextBox ID="txtNameO" runat="server" MaxLength="200" Height="16px" Width="58%"></asp:TextBox></td>
    </tr>
      <tr> 
                  <td class="CellFormat">NRIC/ID No.<asp:Label ID="Label30" runat="server" Font-Size="18px" ForeColor="Red" Text="*"></asp:Label>
                  </td>
                            <td class="CellTextBox"> 
                               <asp:TextBox ID="txtNRIC" runat="server" MaxLength="20" Height="16px" Width="20%" CssClass="uppercase"></asp:TextBox>
                                <asp:Button ID="Button2" runat="server" Text="Button" cssclass="dummybutton" />
                                </td>  
            </tr>
      </tr>
                <tr>
                    <td class="CellFormat">Nationality</td>
                     <td class="CellTextBox"> <asp:DropDownList ID="ddlNationality" runat="server" Width="20%" Height="25px" CssClass="chzn-select" AppendDataBoundItems="True">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                          <asp:ListItem Value="SINGAPOREAN">SINGAPOREAN</asp:ListItem>
                                          <asp:ListItem Value="MALAYSIAN">MALAYSIAN</asp:ListItem>
                                          <asp:ListItem Value="INDIAN">INDIAN</asp:ListItem>
                                          <asp:ListItem Value="INDONESIAN">INDONESIAN</asp:ListItem>
                                          <asp:ListItem Value="CHINESE">CHINESE</asp:ListItem>
                          <asp:ListItem Value="PHILIPPINE">PHILIPPINE</asp:ListItem>
                                          <asp:ListItem>SOUTH KOREAN</asp:ListItem>
                           <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                                     </asp:DropDownList> 
                </tr>
                   
               
                <tr>
                     <td class="CellFormat"><span style="color: rgb(0, 0, 0); font-family: Calibri; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: normal; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Tax Identification No (TIN)</span></td>
                  
                    <td class="CellTextBox">
                        <asp:TextBox ID="txtTIN" runat="server" CssClass="uppercase" Height="16px" MaxLength="20" Width="20%"></asp:TextBox>
                     </td>

                </tr>
             
                <tr>
                    <td class="CellFormat"><span style="color: rgb(0, 0, 0); font-family: Calibri; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 700; letter-spacing: normal; orphans: 2; text-align: right; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: normal; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">SalesTax Registration No (SST)</span></td>
                    <td class="CellTextBox">
                        <asp:TextBox ID="txtSST" runat="server" CssClass="uppercase" Height="16px" MaxLength="20" Width="20%"></asp:TextBox>
                    </td>
      </tr>
      <tr>
          <td class="CellFormat">Customer Since</td>
          <td class="CellTextBox">
              <asp:TextBox ID="txtStartDate" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtStartDate" TargetControlID="txtStartDate">
              </asp:CalendarExtender>
          </td>
      </tr>
             
                <tr style="display:none">
                    

                    <td class="CellFormat">Salesman<asp:Label ID="Label4" runat="server" Font-Size="18px" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    

                    <td class="CellTextBox"> <asp:DropDownList ID="ddlSalesMan" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="StaffId" DataValueField="StaffId" Height="25px" Width="20%">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                               </asp:DropDownList>
                                 <asp:ListSearchExtender ID="ddllsSalesMan" runat="server" TargetControlID="ddlSalesMan" PromptPosition="Bottom" Enabled="True"></asp:ListSearchExtender>
    </td>
                </tr>
                 

                <tr style="display:none">
                    

                    <td class="CellFormat">Person Incharge</td>
                    

                    <td class="CellTextBox"><asp:DropDownList ID="ddlIncharge" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataTextField="inchargeId" DataValueField="inchargeId" Width="20%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>

                </tr>
                   
                     <tr>
                         

                    <td class="CellFormat">Comments</td>
                         

                    <td rowspan="2" class="CellTextBox">        <asp:TextBox ID="txtComments" runat="server" MaxLength="2000" Height="40px" Width="99%" TextMode="MultiLine" Font-Names="Calibri"></asp:TextBox>                     
                    </td>

                </tr>
            <tr>
                <td></td>
            </tr>
                   <tr>
                       

                   <td colspan="2">
                          <asp:Panel ID="pnlOffAddrName" runat="server">

                          

                           <table class="Centered" style="padding-top:5px;width:60%">
                                 

                                 <tr>
                                     

                                   <td colspan="4"><br /></td>
                                     

                               </tr>
                                 

                               <tr>
                                   

                                   <td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;"><asp:ImageButton ID ="imgCollapsible1" runat="server" ImageAlign="Bottom"  ImageUrl="~/Images/plus.png" /> Residential Address</td>
                                
                               
                                
                               </tr>
                               </table>
                              </asp:Panel>
                           <asp:Panel ID="pnlOffAddr" runat="server">

                          

                           <table class="Centered" style="padding-top:5px;width:60%">
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Street Address1 </td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtOffAddress1" runat="server" MaxLength="100" Height="16px" Width="94%" onchange="UpdateBillingDetails()" AutoPostBack="True"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Street Address2</td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtOffStreet" runat="server" MaxLength="100" Height="16px" Width="94%" onchange="UpdateBillingDetails()"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">&nbsp;Building &amp; Unit No.&nbsp; </td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtOffBuilding" runat="server" MaxLength="100" Height="16px" Width="94%" onchange="UpdateBillingDetails()"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">City<asp:Label ID="Label55" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label> </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlOffCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="City" DataValueField="City" onchange="UpdateBillingDetails()"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
                                   

                                   <td class="CellFormat1">State </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlOffState" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="83%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" onchange="UpdateBillingDetails()"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Country<asp:Label ID="Label59" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                      </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlOffCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="Country" DataValueField="Country" onchange="UpdateBillingDetails()"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
                                   

                                   <td class="CellFormat1">Postal<asp:Label ID="Label66" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffPostal" runat="server" MaxLength="20" Height="16px" Width="83%" onchange="UpdateBillingDetails()"></asp:TextBox></td>

                                   



                               </tr>
                                 
                               
                                 <tr>
                                     

                                   <td colspan="4"><br /></td>
                                     

                               </tr>
                                 

                               <tr>
                                   

                                   <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">Contact Info</td>

                                   



                               </tr>
                                   <tr>
                                   

                                   <td class="CellFormat">Contact Person 1</td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtOffContactPerson" runat="server" MaxLength="50" Height="16px" Width="94%" onchange="UpdateBillingDetails()"></asp:TextBox></td>
                           
                               </tr>
                                 
                                <tr>
                                    

                                   <td class="CellFormat">Telephone</td>
                                    

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffContactNo" runat="server" MaxLength="50" Height="16px" Width="99%" onchange="UpdateBillingDetails()"></asp:TextBox></td>
                                    

                                   <td class="CellFormat1">Fax</td>
                                    

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffFax" runat="server" MaxLength="50" Height="16px" Width="83%" onchange="UpdateBillingDetails()"></asp:TextBox></td>

                                    



                               </tr>
                              
                               <tr>
                                   

                                   <td class="CellFormat">Telephone 2</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffContact2" runat="server" MaxLength="50" Height="16px" Width="99%" onchange="UpdateBillingDetails()"></asp:TextBox></td>
                                   

                                  <td class="CellFormat1">Mobile</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffMobile" runat="server" MaxLength="50" Height="16px" Width="83%" onchange="UpdateBillingDetails()"></asp:TextBox></td>
                                   
   

                               </tr>
                                 

                                <tr>
                                    

                        <td class="CellFormat">Email</td>
                                    

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtOffEmail" runat="server" MaxLength="100" Height="16px" Width="94%" onchange="UpdateBillingDetails()"></asp:TextBox>
                            <a href="<%= Me.ResolveUrl("mailto:" + txtOffEmail.Text)%>" style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a>
                        </td>
                                    

                        </tr>
                               <tr>
                                     <td colspan="4"><br /></td>
                                 </tr>
     


                               <tr>
                                   

                                   <td class="CellFormat">Contact Person 2</td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtOffCont1Name" runat="server" MaxLength="50" Height="16px" Width="94%" onchange="UpdateBillingDetails()"></asp:TextBox></td>
                                  
                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffCont1Tel" runat="server" MaxLength="50" Height="16px" Width="99%" onchange="UpdateBillingDetails()"></asp:TextBox></td>
                                   

                                   <td class="CellFormat1">Fax</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffCont1Fax" runat="server" MaxLength="50" Height="16px" Width="83%" onchange="UpdateBillingDetails()"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone 2</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffCont1Tel2" runat="server" MaxLength="50" Height="16px" Width="99%" onchange="UpdateBillingDetails()"></asp:TextBox></td>
                                   

                                   <td class="CellFormat1">Mobile</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtOffCont1Mobile" runat="server" MaxLength="50" Height="16px" Width="83%" onchange="UpdateBillingDetails()"></asp:TextBox></td>

                                   



                               </tr>
                                 

                                <tr>
                                    

                        <td class="CellFormat">Email</td>
                                    

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtOffCont1Email" runat="server" MaxLength="100" Height="16px" Width="94%" onchange="UpdateBillingDetails()"></asp:TextBox>
                            <a href="<%= Me.ResolveUrl("mailto:" + txtOffCont1Email.Text)%>" style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a>
                        </td>
                                    

                        </tr>
                            
                                 
 

                            
                           </table>

                            </asp:Panel>
<asp:CollapsiblePanelExtender ID="cpnl1" runat="server" CollapseControlID="pnlOffAddrName" TargetControlID="pnlOffAddr" CollapsedImage="~/Images/plus.png" ExpandedImage="~/Images/minus.png" Enabled="True" ImageControlID="imgCollapsible1" ExpandControlID="pnlOffAddrName" CollapsedText="Click to show"></asp:CollapsiblePanelExtender>


                   </td>

                       



               </tr>
                 

               <tr>
                   

                   <td colspan="2">
                           <asp:CollapsiblePanelExtender ID="cpnl2" runat="server"  CollapseControlID="pnlBillAddrName" TargetControlID="pnlBillAddr" CollapsedImage="~/Images/plus.png" ExpandedImage="~/Images/minus.png" ImageControlID="imgCollapsible2" Enabled="True" ExpandControlID="pnlBillAddrName" CollapsedText="Click to show"></asp:CollapsiblePanelExtender>

                          <asp:Panel ID="pnlBillAddrName" runat="server">

                          

                           <table class="Centered" style="padding-top:5px;width:60%">
                                 

                                 <tr>
                                     

                                   <td colspan="4"><br /></td>
                                     

                               </tr>
                                 

                               <tr>
                                   

                                   <td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;"><asp:ImageButton ID ="imgCollapsible2" runat="server" ImageAlign="Bottom"  ImageUrl="~/Images/plus.png" /> Main Billing Address</td>
                                
                               
                                
                               </tr>
                               </table>
                              </asp:Panel>
                           <asp:Panel ID="pnlBillAddr" runat="server">

                          
                           <table class="Centered" style="padding-top:5px;width:60%">
                                 


                               <tr>
                                   
     <td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left;">
         <asp:Button ID="btnUpdateBilling" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Height="27px" Text="UPDATE BILLING INFO" Width="180px" />
     </td> <td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:right;">    <asp:CheckBox ID="chkOffAddr" runat="server" Text="Same as Office Address" Font-Names="Calibri" Font-Underline="False"  onclick="UpdateBillingDetails()"/></td>
                                                                      



                               </tr>
                                 
   <tr>
                                   

                                   <td class="CellFormat">Billing Name<asp:Label ID="Label31" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                                   </td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillingName" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox></td>

                                   



                               </tr>
                      <tr>
                                   

                                   <td class="CellFormat">Street Address1<asp:Label ID="Label32" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                   </td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillAddress" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Street Address2</td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillStreet" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">&nbsp;Building &amp; Unit No.&nbsp; </td>
                                   

                                   <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillBuilding" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">City </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlBillCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="City" DataValueField="City"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
                                   

                                   <td class="CellFormat1">State </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlBillState" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="83%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Country<asp:Label ID="Label60" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                   </td>
                                   

                                   <td class="CellTextBox"><asp:DropDownList ID="ddlBillCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="Country" DataValueField="Country"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
                                   

                                   <td class="CellFormat1">Postal<asp:Label ID="Label67" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillPostal" runat="server" MaxLength="20" Height="16px" Width="83%" ></asp:TextBox></td>

                                   



                               </tr>
                                 

                                 <tr>
                                     

                                   <td colspan="4"><br /></td>
                                     

                               </tr>
                                 

                               <tr>
                                   

                                   <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">Billing Contact Info</td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Contact Person 1<asp:Label ID="Label9" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                                   </td>
                                   

                                   <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtBillCP1Contact" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                    

                                    <td class="CellFormat1">Position</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP1Position" runat="server" MaxLength="100" Height="16px" Width="83%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP1Tel" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                   

                                   <td class="CellFormat1">Fax</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP1Fax" runat="server" MaxLength="50" Height="16px" Width="83%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone 2</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP1Tel2" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                   

                                   <td class="CellFormat1">Mobile</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP1Mobile" runat="server" MaxLength="50" Height="16px" Width="83%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                                <tr>
                                    

                        <td class="CellFormat">Email</td>
                                    

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillCP1Email" runat="server" MaxLength="500" Height="16px" Width="94%"></asp:TextBox>
                            <a href="<%= Me.ResolveUrl("mailto:" + txtBillCP1Email.Text)%>" style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a>
                        </td>
                                    

                        </tr>
                                 

                               <tr>
                                   

                                   <td colspan="4"><br /></td>
                                   

                               </tr>
                                 

                                <tr>
                                    

                                   <td class="CellFormat">Contact Person 2</td>
                                    

                                   <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtBillCP2Contact" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                    

                                    <td class="CellFormat1">Position</td>
                                    

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP2Position" runat="server" MaxLength="100" Height="16px" Width="83%"></asp:TextBox></td>

                                    



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP2Tel" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                   

                                   <td class="CellFormat1">Fax</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP2Fax" runat="server" MaxLength="50" Height="16px" Width="83%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td class="CellFormat">Telephone 2</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP2Tel2" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                                   

                                   <td class="CellFormat1">Mobile</td>
                                   

                                   <td class="CellTextBox"><asp:TextBox ID="txtBillCP2Mobile" runat="server" MaxLength="50" Height="16px" Width="83%"></asp:TextBox></td>

                                   



                               </tr>
                                 

                                <tr>
                                    

                        <td class="CellFormat">Email</td>
                                    

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillCP2Email" runat="server" MaxLength="500" Height="16px" Width="94%"></asp:TextBox>
                            <a href="<%= Me.ResolveUrl("mailto:" + txtBillCP2Email.Text)%>" style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a>
                        </td>
                                    

                        </tr>
                                 
                               <tr>
                                   <td colspan="4"><br /></td>
                               </tr>

                               <tr>
                                   

                                   <td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Billing Options</td>
                                
                               
                                
                               </tr>


                                 <tr>
            <td class="CellFormat">Credit Limit</td>
        
        <td class="CellTextBox">
            <asp:TextBox ID="txtCreditLimit" runat="server" Height="16px" MaxLength="50" Width="99%"></asp:TextBox>

        </td>
            <td class="CellTextBox1" style="text-align:left">
                &nbsp;</td>
        </tr>
        
                               <tr>
                                   <td class="CellFormat">Credit Terms<asp:Label ID="Label19" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                                   </td>
                                   <td class="CellTextBox">
                                       <asp:DropDownList ID="ddlTerms" runat="server" AppendDataBoundItems="True" Height="25px" Width="99%">
                                           <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                       </asp:DropDownList>
                                   </td>
                                   <td class="CellTextBox1" style="text-align:left">
                                       <asp:ImageButton ID="btnEditSendStatement" runat="server" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
                                   </td>
                               </tr>
        
        <tr><td class="CellFormat">Currency<asp:Label ID="Label21" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox"><asp:DropDownList ID="ddlCurrency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSCurrency" DataTextField="Currency" DataValueField="Currency" Height="25px" Width="99%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr>
                               <tr>
            <td class="CellFormat">Default Invoice Format</td>
            <td class="CellTextBox">
                <asp:DropDownList ID="ddlDefaultInvoiceFormat" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Width="99%">
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
            <td class="CellTextBox"></td>
        </tr>
                                  <tr>
                                                                    <td class="CellFormat">Send HardCopy</td>
                                                                    <td class="CellTextBox" colspan="2">
                                                                 
                                                                        <asp:CheckBox ID="chkSendStatementInv" runat="server" Text="Invoice" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                        &nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chkSendStatementSOA" runat="server" Text="SOA (Statement of Accounts)" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                    </td>
                                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td class="CellFormat">Auto Email</td>
                                                                    <td class="CellTextBox" colspan="3">
                                                                        <asp:CheckBox ID="chkAutoEmailInvoice" runat="server" Text="Invoice" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/><asp:Label ID="lblAutoEmailInv" runat="server" ForeColor="Black" Text="-" Font-Italic="True"></asp:Label>
                                                                        &nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chkAutoEmailStatement" runat="server" Text="SOA (Statement of Accounts)" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/><asp:Label ID="lblAutoEmailSOA" runat="server" ForeColor="Black" Text="-" Font-Italic="True"></asp:Label>
                                                                    </td>
                                                                 
                                                                </tr>
          <tr>
                                                                    <td class="CellFormat">Requires E-Billing</td>
                                                                    <td class="CellTextBox" colspan="2">
                                                                        <asp:CheckBox ID="chkRequireEBilling" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                    </td>
                                                                </tr>
                                 <tr>
                                                    <td class="CellFormat">Remarks</td>
                                                    <td class="CellTextBox" rowspan="2" colspan="3">
                                                        <asp:TextBox ID="txtBillingOptionRemarks" runat="server" Font-Names="Calibri" Height="40px" MaxLength="1000" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                 <tr>
                                                                    <td colspan="4">
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Notification</td>
                                                                </tr>
                                                                  <tr>
                                                                    <td class="CellFormat"></td>
                                                                    <td class="CellTextBox" colspan="2">
                                                                        <asp:CheckBox ID="chkEmailNotifySchedule" runat="server" Text="Email Notification for Schedule" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                     </td>
                                                                 
                                                                </tr>
                                                                 <tr>
                                                                    <td class="CellFormat"></td>
                                                                    <td class="CellTextBox" colspan="2">
                                                                       <asp:CheckBox ID="chkEmailNotifyJobProgress" runat="server" Text="Email Notification for Job Progress" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                      </td>
                                                                </tr>
                                  <tr>
                                                                    <td colspan="4">
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Service Reports</td>
                                                                </tr>
                                                                  <tr>
                                                                    <td class="CellFormat"></td>
                                                                    <td class="CellTextBox" colspan="2">
                                                                        <asp:CheckBox ID="chkPhotosMandatory" runat="server" Text="Photos are Mandatory" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                     </td>
                                                                 
                                                                </tr>
                                 <tr>
                                                                    <td class="CellFormat"></td>
                                                                    <td class="CellTextBox" colspan="2">
                                                                        <asp:CheckBox ID="chkDisplayTimeInTimeOut" runat="server" Text="Display Time In and Time Out in the Service Report" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                     </td>
                                                                 
                                                                </tr>
       
                                    <tr>
                                   

                                   <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">
                                       
                                       <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" CssClass="dummybutton" ForeColor="White"></asp:TextBox>
                                       
                                      </td>

                                   



                               </tr>
                                 

                               <tr>
                                   

                                   <td colspan="4" class="CellFormat" style="text-align: left; padding-right: 0%; padding-left: 30%;">
                                       <asp:RadioButtonList ID="rdbBillingSettings" runat="server" CausesValidation="True" CellPadding="5" CellSpacing="3" Height="63px" Width="100%" Visible="False">
                                           <asp:ListItem Value="AccountID" Selected="True">Generate Billing by Account ID</asp:ListItem>
                                           <asp:ListItem Value="LocationID">Generate Billing by Location ID</asp:ListItem>
                                           <asp:ListItem Value="ContractNo">Generate Billing by Contract No</asp:ListItem>
                                            <asp:ListItem Value="ServiceLocationCode">Generate Billing by Service Location Code</asp:ListItem>
                                     
                                       </asp:RadioButtonList>
                                       </td>

                               </tr>
                           </table>

                           </asp:Panel>



                   </td>

                   



               </tr>

                <tr>
                    

                    <td colspan="2"><br /></td>
                    

                </tr>
                 

           <tr>
               

               <td colspan="2" style="text-align:right;">        
                   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="if(this.value === 'Saving...') { return false; } else { this.value = 'Saving...'; }; currentdatetime();"/>
                  
                    
 

                  
                    <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   

                </td>
               

           </tr>

                 



            </table>

            
            <div style="text-align:center">
       <asp:LinkButton ID="btnTop" runat="server" Font-Names="Calibri" Font-Bold="True" ForeColor="Brown">Go to Top</asp:LinkButton></div>
   </div>
 

        </ContentTemplate>
        
    </asp:TabPanel>
    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Service Location">
        <HeaderTemplate>
                <asp:Label ID="lblServiceLocationCount" runat="server" Font-Size="11px" Text="Service Location"></asp:Label>
                                      
        </HeaderTemplate>
        
        <ContentTemplate>
                
              <asp:TextBox ID="txtLocatonNo" runat="server" Visible="False"></asp:TextBox><asp:TextBox ID="txtLocationPrefix" runat="server" Visible="False"></asp:TextBox>
 
                <table style="padding-top:5px;width:100%;">
                    <tr>
                         <td style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';">
             <asp:Label ID="txtSvcMode" runat="server" CssClass="dummybutton"></asp:Label>
     </td>
                    </tr>

                    <tr>
                        
 <td style="width:60%;text-align:left;">
                    <asp:Button ID="btnSvcAdd" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="90px" />
                 <asp:Button ID="btnSvcCopy" runat="server" Font-Bold="True" Text="COPY" Width="90px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
       
       <asp:Button ID="btnSvcEdit" runat="server" Font-Bold="True" Text="EDIT" Width="90px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
       
          <asp:Button ID="btnSvcDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="90px" OnClientClick = "currentdatetime(); ConfirmDeleteSvc()"/>
  <asp:Button ID="btnSvcContract" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CONTRACT" Width="115px"/>
  <asp:Button ID="btnSvcService" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SERVICE" Width="105px" />
     <asp:Button ID="btnTransfersSvc" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CHANGE ACCOUNT" Width="140px" />
        <asp:Button ID="btnSpecificLocation" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SPECIFIC LOCATION" Width="180px" />


 </td>


      <td style="width:40%;text-align:right;">  
                  <asp:ImageButton ID="btnReset" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="RESET SEARCH" />

      <asp:TextBox ID="txtSearch" runat="server" Width="370px" Height="25px" Text = "Search Here for Location Address, Postal Code or Description" ForeColor = "Gray" onblur = "WaterMark(this, event);" onfocus = "WaterMark(this, event);" AutoPostBack="True"></asp:TextBox>    
                             
         <asp:ImageButton ID="btnSvcSearch" OnClick="btnSvcSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False"  />
             <asp:Button ID="btnGoSvc" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="GO" Width="50px" Height="30px" />   
                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="dummybutton" ></asp:TextBox>      </td>
                    </tr>
                    <tr>
                     <td style="text-align:left;width:20%"><asp:Label ID="Label12" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView1" runat="server" AutoPostBack="True">
                     <asp:ListItem Selected="True">5</asp:ListItem>
                     <asp:ListItem >10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td>
                        <td colspan="1">  <asp:TextBox ID="txtSelectedIndex" runat="server" AutoCompleteType="Disabled" Height="16px"  style="padding-right: 0%" Visible="False" Width="10%"></asp:TextBox><br /></td>
                    </tr>
                    <tr>
                        
                        <td colspan="2" style="text-align:center">
                            

                            <asp:GridView ID="GridView2" runat="server" CssClass="Centered" OnRowDataBound = "OnRowDataBound2" OnSelectedIndexChanged = "OnSelectedIndexChanged2" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="Rcno" AllowPaging="True" PageSize="5" Font-Size="15px" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                <Columns>    
                  <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                                    
                <ControlStyle Width="40px" />
                                    

                <ItemStyle Width="40px" />
                                    
                </asp:CommandField>
                                     
                                    <asp:BoundField DataField="LocationID" HeaderText="Location ID" SortExpression="LocationID" >
                                     <ControlStyle Width="15%" />
                                    <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                      
                                  
                                    <asp:BoundField DataField="Location" HeaderText="Branch" SortExpression="Location" />
                                    
                                    <asp:BoundField DataField="PersonID" HeaderText="Client ID" SortExpression="PersonID" >
                                 <ControlStyle Width="15%" />
                                    <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    
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
                                           <asp:BoundField DataField="PersonGroupD" HeaderText="Person Group" />
                                            
                                     <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" />
                                        <asp:BoundField DataField="SiteName" HeaderText="Site Name" >                                   
                                    </asp:BoundField>
                                        <asp:BoundField DataField="ContactPerson" HeaderText="Service ContactPerson1" SortExpression="ContactPerson" >                                   
                                    </asp:BoundField>
                                     <asp:BoundField DataField="ContactPerson2" HeaderText="Service ContactPerson2" SortExpression="ContactPerson2" >                                   
                                    <ControlStyle CssClass="dummybutton" />
                                    <ItemStyle CssClass="dummybutton" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="BillContact1Svc" HeaderText="Bill ContactPerson1" SortExpression="BillContact1Svc" >                                   
                                    <HeaderStyle Width="120px" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="BillContact2Svc" HeaderText="Bill ContactPerson2" SortExpression="BillContact2Svc" >                                   
                                    <ControlStyle CssClass="dummybutton" />
                                    <HeaderStyle Width="120px" CssClass="dummybutton" />
                                    <ItemStyle CssClass="dummybutton" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Comments" />
                                     <asp:BoundField DataField="Description" SortExpression="Description" >
                                  
                                    
                                    <ControlStyle CssClass="dummybutton" Width="250px" />
                                    <HeaderStyle CssClass="dummybutton" Width="250px" />
                                    <ItemStyle CssClass="dummybutton" Width="250px" />
                                    
                                    </asp:BoundField>
                                  
                                  
                                    <asp:BoundField DataField="BranchID" HeaderText="BranchID" SortExpression="BranchID" Visible="False" />
                                    
                                    <asp:BoundField DataField="ContactPerson" HeaderText="ContactPerson" SortExpression="ContactPerson" Visible="False" />
                                    
                                    <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone" Visible="False" />
                                    
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" Visible="False" />
                                    
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" Visible="False" />
                                    
                                    <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                        <EditItemTemplate>
                                            

                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                                            
 
                                        </EditItemTemplate>
                                        
                                        <ItemTemplate>
                                            

                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                            
 
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    
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
                                    
                                    <asp:BoundField DataField="AccountNo">
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
                                            <SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                
                            </asp:GridView>
                            


                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblpersonlocation WHERE AccountID = @AccountID order by locationid">
                                <SelectParameters>
                                    
                                    <asp:ControlParameter ControlID="txtAccountIDtab2" Name="AccountID" PropertyName="Text" Type="String" />
                                    
                                </SelectParameters>
                                
                            </asp:SqlDataSource>
                            


                        </td>
                        

                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>

                    </table>
                
             <table border="0" class="Centered" style="padding-top:5px;width:40%;">
                                                      <tr>
                                                    <td  style="width:8% ; font-size:15px;  font-weight:bold;  font-family:'Calibri'; color:red; text-align:right; ">Last Service Done : </td>
                                                       
                                                    
                                                          <td   colspan="1" style="width:5%; font-size:15px; font-family:'Calibri';  color:red;  text-align:left;" >
                                                               <asp:Label ID="lblLastServiceDone" runat="server"  Height="18px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label>
                                            
                                                          </td>
                                                           <td style="width:9%; font-size:15px;  font-weight:bold;  font-family:'Calibri';    color:red; text-align:right;">Next Scheduled Service : </td>

                                                         
                                                           <td  colspan="1" style="width: 5%; font-size:15px; font-family:'Calibri';  color:red;  text-align:left;">
                                                            <asp:Label ID="lblNextScheduledService" runat="server"  Height="18px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label>
                                            
                                                          </td>
                                                </tr>
                                                  </table>

              <table class="Centered" style="padding-top:5px;width:65%;">
                    

                    <tr>
                        

                        <td colspan="4">
                            

                            <asp:TextBox ID="txtAccountIDtab2" runat="server" Enabled="False" Height="16px" MaxLength="10" ReadOnly="True" Visible="False" Width="20%"></asp:TextBox>
                            


                        </td>
                        

                    </tr>
                    
                  <tr> <td class="auto-style1">Account ID </td>
                        <td colspan="3" class="auto-style2"><asp:Label ID="lblAccountID" runat="server" MaxLength="100" Height="16px" Width="93%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label>
                        </td> </tr>
                       
                  <tr>  <td class="CellFormat">Name </td> 
                      <td colspan="3" class="CellTextBox"><asp:Label ID="lblName" runat="server" MaxLength="100" Height="16px" Width="93%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label>
                        </td>

                  </tr>
                  <tr>
                      <td colspan="4"><br /></td>
                  </tr>
                    <tr>
                        

                        <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Service Address</td>
                           

                           <td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:right;"><asp:CheckBox ID="chkSameAddr" runat="server" Text="Same as Residential Address" Font-Names="Calibri" Font-Underline="False"  onclick="UpdateServiceDetails()" AutoPostBack="True"/>
                        </td>

                        



                    </tr>
                    

                      <tr>
                        <td class="CellFormat"><asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label><asp:Label ID="Label20" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                        </td>
                        <td class="CellTextBox" colspan="3">
                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="30%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    

                    <tr>
                        <td class="CellFormat">SMART Site</td>
                        <td class="CellTextBox" colspan="3">
                            <asp:CheckBox ID="chkSmartCustomer" runat="server" />
                        </td>
                    </tr>
                    

                    <tr>
                        <td class="CellFormat">Service Location ID </td>
                        <td class="CellTextBox" colspan="3">
                            <asp:TextBox ID="txtLocationID" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="93%"></asp:TextBox>
                        </td>
                    </tr>
                    

                   <tr>
                        

                        <td class="CellFormat">ClientID (View Only) </td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtClientID" runat="server" MaxLength="100" Height="16px" Width="93%" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                   <tr>
                         <td class="CellFormat">Service Location Sub-Group</td>
                      
                        <td colspan="3" class="CellTextBox">
                            <asp:TextBox ID="txtServiceLocationGroup" runat="server" Height="16px" MaxLength="100" Width="93%" ToolTip="'Account Code' field in Previous System"></asp:TextBox>
                        </td>          
                    </tr>
                    
                     
                    
                   <tr>
                        <td class="CellFormat">Person Group<asp:Label ID="Label62" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                        </td>
                        
                        <td colspan="3" class="CellTextBox">
                            <asp:DropDownList ID="ddlPersonGrpD" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Height="25px" Width="93%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="CellFormat">Contract Group<asp:Label ID="Label38" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                        </td>
                        <td class="CellTextBox" colspan="3">
                            <asp:DropDownList ID="ddlContractGrp" runat="server" AppendDataBoundItems="True" Height="20px" Width="93%">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:ImageButton ID="btnEditContractGroup" runat="server" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="CellFormat">Service Name<asp:Label ID="Label26" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                        </td>
                        <td class="CellTextBox" colspan="3">
                            <asp:TextBox ID="txtServiceName" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox>
                        </td>
                    </tr>
                   <tr>
                        <td class="CellFormat">Site Name</td>
                        <td colspan="3" class="CellTextBox">
                            <asp:TextBox ID="txtSiteName" runat="server" Height="16px" MaxLength="200" Width="93%"></asp:TextBox>
                        </td>

                    </tr>
                      <tr>
                          <td class="CellFormat">Comments</td>
                          <td class="CellTextBox" colspan="3">
                              <asp:TextBox ID="txtCommentsSvc" runat="server" Height="16px" MaxLength="300" Width="93%"></asp:TextBox>
                          </td>
                    </tr>
                      <tr>
                          <td class="CellFormat">Description </td>
                          <td class="CellTextBox" colspan="3">
                              <asp:TextBox ID="txtDescription" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox>
                          </td>
                    </tr>
                      <tr>
                        <td class="CellFormat">Street Address 1<asp:Label ID="Label33" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                          </td>
                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                        </td>

                    </tr>                    

                    <tr>
                        

                        <td class="CellFormat">Street Address 2</td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtStreet" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Building &amp; Unit No.&nbsp;</td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBuilding" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">City<asp:Label ID="Label42" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>&nbsp;</td>
                        

                        <td class="CellTextBox"><asp:DropDownList ID="ddlCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="City" DataValueField="City"><asp:ListItem Text="--SELECT--" Value="-1" />
                            </asp:DropDownList>
                        </td>
                        

                        <td class="CellFormat1">State </td>
                        

                        <td class="CellTextBox"><asp:DropDownList ID="ddlState" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="81%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State"><asp:ListItem Text="--SELECT--" Value="-1" />
                            </asp:DropDownList>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Country<asp:Label ID="Label36" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                        </td>
                        

                        <td class="CellTextBox"><asp:DropDownList ID="ddlCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="Country" DataValueField="Country"><asp:ListItem Text="--SELECT--" Value="-1" />
                            </asp:DropDownList>
                        </td>
                        

                        <td class="CellFormat1">Postal</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtPostal" runat="server" MaxLength="20" Height="16px" Width="80%" AutoPostBack="True" OnTextChanged="txtPostal_TextChanged"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                    <td class="CellFormat">Zone</td>
                        

                    <td class="CellTextBox"> <asp:DropDownList ID="ddlLocateGrp" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" DataTextField="locationgroup" DataValueField="locationgroup" Width="99%" Height="25px" >
                                <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList> 
                        </td>
                        
                         <tr >
                    <td class="CellFormat">Industry<asp:Label ID="Label10" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
                    <td class="CellTextBox" colspan="3"><asp:DropDownList ID="ddlIndustrysvc" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select" DataTextField="industry" DataValueField="industry" Height="25px" Width="93%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td></tr>
               <tr >
                    <td class="CellFormat">Market Segment ID</td><td class="CellTextBox" colspan="3"><asp:TextBox ID="txtMarketSegmentIDsvc" runat="server" Height="16px" MaxLength="100" Width="93%"></asp:TextBox></td>

              </tr>

                         <tr>
                        <td class="CellFormat">
                            
                            Person In Charge</td>
                        <td class="CellTextBox" colspan="3">
                            <asp:DropDownList ID="ddlInchargeSvc" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="inchargeId" DataValueField="inchargeId" Height="25px" Width="93%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="CellFormat">Salesman<asp:Label ID="Label64" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                        </td>
                        <td class="CellTextBox" colspan="3">
                            <asp:DropDownList ID="ddlSalesManSvc" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="StaffId" DataValueField="StaffId" Height="25px" Width="93%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="CellFormat">Terms</td>
                        <td class="CellTextBox" colspan="3">
                            <asp:DropDownList ID="ddlTermsSvc" runat="server" AppendDataBoundItems="True" Height="25px" Width="93%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                        

                </tr>
                    

                    <tr>
                        <td class="CellFormat">&nbsp;</td>
                        <td class="CellTextBox" colspan="3">
                            &nbsp;</td>
                    </tr>
                    

                  <tr><td colspan="4">
                      <asp:TextBox ID="txtAccountCode" runat="server" Height="16px" MaxLength="100" Visible="False" Width="10%"></asp:TextBox>
                      <br /></td></tr>
                    

                    <tr>
                        

                        <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Service Contact Info</td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Contact Person 1<asp:Label ID="Label27" runat="server" Font-Size="18px" ForeColor="Red" Text="*" Height="18px"></asp:Label></td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtSvcCP1Contact" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                         

                         <td class="CellFormat1">Position</td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtSvcCP1Position" runat="server" MaxLength="100" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Telephone</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtSvcCP1Telephone" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        

                        <td class="CellFormat1">Fax</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtSvcCP1Fax" runat="server" MaxLength="50" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Telephone2</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtSvcCP1Telephone2" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        

                        <td class="CellFormat1">Mobile</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtSvcCP1Mobile" runat="server" MaxLength="50" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                   <tr>
                        

                        <td class="CellFormat">Email</td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtSvcCP1Email" runat="server" MaxLength="500" Height="16px" Width="93%"></asp:TextBox>
                            <a href="<%= Me.ResolveUrl("mailto:" + txtSvcCP1Email.Text)%>" style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a>
                        </td>
                        

                        </tr>
                    

                   <tr>
                       

                       <td colspan="4"><br /></td>
                       

                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Contact Person 2</td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtSvcCP2Contact" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        
<td class="CellFormat1">Position</td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtSvcCP2Position" runat="server" MaxLength="100" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Telephone</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtSvcCP2Telephone" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        

                        <td class="CellFormat1">Fax</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtSvcCP2Fax" runat="server" MaxLength="50" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Telephone2</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtSvcCP2Tel2" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        

                        <td class="CellFormat1">Mobile</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtSvcCP2Mobile" runat="server" MaxLength="50" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                   <tr>
                        

                        <td class="CellFormat">Email (Maximum 100 Chars.)</td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtSvcCP2Email" runat="server" MaxLength="500" Height="16px" Width="93%"></asp:TextBox>
                            <a href="<%= Me.ResolveUrl("mailto:" + txtSvcCP2Email.Text)%>" style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a>
                        </td>
                        

                        </tr>

                    

                   

                    

                    <tr>
                        <td class="CellFormat">Email CC</td>
                        <td class="CellTextBox" colspan="3">
                            <asp:TextBox ID="txtSvcEmailCC" runat="server" Height="16px" MaxLength="500" Width="93%"></asp:TextBox>
                        </td>
                    </tr>

                    

                   

                    

                    <tr>
                        <td class="CellFormat">&nbsp;</td>
                        <td class="CellTextBox" colspan="3">
                            <asp:CheckBox ID="chkSendEmailNotificationOnly" runat="server" Font-Names="Calibri" Font-Underline="False" onclick="UpdateServiceBillingDetails()" Text="Send Email Notification to View Service Report in Customer Portal" />
                        </td>
                    </tr>

                    

                   

                    

                    <tr>
                      <td colspan="4"><br /></td>
                  </tr>
                    <tr>
                        

                        <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Billing Address</td>
                           

                           <td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:right;"><asp:CheckBox ID="chkMainBillingInfo" runat="server" Text="Same as Main Billing Informaion " Font-Names="Calibri" Font-Underline="False"  onclick="UpdateServiceBillingDetails()"/>
                        </td>

                        



                    </tr>
                    

                  
                    
                     
                    
                   <tr>
                        <td class="CellFormat">Billing Name<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label> </td>
                        
                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillingNameSvc" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                        </td>
                    </tr>
                  
                    <tr>
                        <td class="CellFormat">Street Address 1</td>
                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillAddressSvc" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                            <asp:TextBox ID="txtSvcAddrSvc" runat="server" CssClass="dummybutton"></asp:TextBox>   </td>

                    </tr>                    

                    <tr>
                        

                        <td class="CellFormat">Street Address 2</td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillStreetSvc" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Building &amp; Unit No.&nbsp; </td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillBuildingSvc" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">City </td>
                        

                        <td class="CellTextBox"><asp:DropDownList ID="ddlBillCitySvc" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="City" DataValueField="City"><asp:ListItem Text="--SELECT--" Value="-1" />
                            </asp:DropDownList>
                        </td>
                        

                        <td class="CellFormat1">State </td>
                        

                        <td class="CellTextBox"><asp:DropDownList ID="ddlBillStateSvc" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="80%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State"><asp:ListItem Text="--SELECT--" Value="-1" />
                            </asp:DropDownList>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Country<asp:Label ID="Label58" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                            </td>
                        

                        <td class="CellTextBox"><asp:DropDownList ID="ddlBillCountrySvc" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataTextField="Country" DataValueField="Country"><asp:ListItem Text="--SELECT--" Value="-1" />
                            </asp:DropDownList>
                        </td>
                        

                        <td class="CellFormat1">Postal<asp:Label ID="Label68" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBillPostalSvc" runat="server" MaxLength="20" Height="16px" Width="80%" AutoPostBack="True" OnTextChanged="txtPostal_TextChanged"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                   
                    

                    <tr >
            <td class="CellFormat">InActive</td>
            <td class="CellTextBox">
                <asp:CheckBox ID="chkInactiveD" runat="server" CssClass="CellFormat" />
            </td>
            <td class="CellFormat1">&nbsp;</td>
            <td class="CellTextBox">
                &nbsp;</td>
        </tr>
                    

                   
                    

                  <tr><td colspan="4">
                      <br /></td></tr>
                    

                    <tr>
                        

                        <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Billing Contact Info</td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Contact Person 1<asp:Label ID="Label14" runat="server" Font-Size="18px" ForeColor="Red" Text="*" Height="18px"></asp:Label></td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtBillContact1Svc" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                         

                         <td class="CellFormat1">Position</td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtBillPosition1Svc" runat="server" MaxLength="100" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Telephone</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBillTelephone1Svc" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        

                        <td class="CellFormat1">Fax</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBillFax1Svc" runat="server" MaxLength="50" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Telephone2</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBilltelephone12Svc" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        

                        <td class="CellFormat1">Mobile</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBillMobile1Svc" runat="server" MaxLength="50" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                   <tr>
                        

                        <td class="CellFormat">Email</td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillEmail1Svc" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                            <a href="<%= Me.ResolveUrl("mailto:" + txtBillEmail1Svc.Text)%>" style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a>
                        </td>
                        

                        </tr>
                    

                    <tr>
                        <td class="CellFormat">Send Service Report to Billing Contact Person 1</td>
                        <td class="CellTextBox" colspan="3">
                            <asp:CheckBox ID="chkServiceReportSendTo1" runat="server" />
                        </td>
                    </tr>
                    

                   <tr>
                       

                       <td colspan="4"><br /></td>
                       

                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Contact Person 2</td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtBillContact2Svc" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        
<td class="CellFormat1">Position</td>
                        

                        <td colspan="1" class="CellTextBox"><asp:TextBox ID="txtBillPosition2Svc" runat="server" MaxLength="100" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Telephone</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBillTelephone2Svc" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        

                        <td class="CellFormat1">Fax</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBillFax2Svc" runat="server" MaxLength="50" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                    <tr>
                        

                        <td class="CellFormat">Telephone2</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBilltelephone22Svc" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox>
                        </td>
                        

                        <td class="CellFormat1">Mobile</td>
                        

                        <td class="CellTextBox"><asp:TextBox ID="txtBillMobile2Svc" runat="server" MaxLength="50" Height="16px" Width="80%"></asp:TextBox>
                        </td>

                        



                    </tr>
                    

                   <tr>
                        

                        <td class="CellFormat">Email</td>
                        

                        <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillEmail2Svc" runat="server" MaxLength="100" Height="16px" Width="93%"></asp:TextBox>
                        <a href="<%= Me.ResolveUrl("mailto:" + txtBillEmail2Svc.Text)%>" style="font-weight: bold; color: #0000FF; font-size: 18px;"><img src="Images/email1.png" width="20" height="20" /></a></td>
                        

                        </tr>

                    



                    <tr>
                        <td class="CellFormat">Send Service Report to Billing Contact Person 2</td>
                        <td class="CellTextBox" colspan="3">
                            <asp:CheckBox ID="chkServiceReportSendTo2" runat="server" />
                        </td>
                    </tr>

                    



                    <tr>
                        <td class="CellFormat">Default Invoice Format </td>
                        <td class="CellTextBox" colspan="3">
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
                            </asp:DropDownList>
                        </td>
                    </tr>

                    

                      <tr>
                                                    <td colspan="4">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%">Service Reports</td>
                                                </tr>
                                                 <tr>
                                                    <td class="CellFormat">Photos are Mandatory</td>
                                                    <td class="CellTextBox" colspan="3">
                                                        <asp:CheckBox ID="chkSvcPhotosMandatory" runat="server" />
                                                    </td>
                                                </tr>

              



                    <tr>
                        

               <td colspan="4" style="text-align:right;">   
                   
        <asp:Button ID="btnSvcSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px"  OnClientClick= "if(this.value === 'Saving...') { return false; } else { this.value = 'Saving...'; }; currentdatetime();"/>
                  
                    

 

                  
                    <asp:Button ID="btnSvcCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
                   
                   <asp:TextBox ID="txtSvcRcno" runat="server" Visible="False"></asp:TextBox>
                   
                   </td>
                        

           </tr>
                    

              </table>
               <div style="text-align:center">
       <asp:LinkButton ID="btnTopDetail" runat="server" Font-Names="Calibri" Font-Bold="True" ForeColor="Brown">Go to Top</asp:LinkButton></div>
   </div> 
 
                </ContentTemplate>
        
    </asp:TabPanel>
       <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="File Upload">
        <HeaderTemplate>
            <asp:Label ID="lblFileUploadCount" runat="server" Font-Size="11px" Text="File Upload"></asp:Label>
        </HeaderTemplate>
        
        <ContentTemplate>
             <table style="width:100%;height:1000px">
                <tr>
                    <td>
                            <table style="text-align:center;width:100%;padding-top:10px;" class="centered">
                     <tr> <td class="CellFormat">Account ID </td>
                        <td class="CellTextBox"><asp:Label ID="lblAccountID2" runat="server" MaxLength="100" Height="16px" Width="50%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label>
                        </td> </tr>
                       
                  <tr>  <td class="CellFormat">Corporate Name </td> 
                      <td class="CellTextBox"><asp:Label ID="lblName2" runat="server" MaxLength="100" Height="16px" Width="50%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label>
                        </td>

                  </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDeleteUploadedFile" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align:left; " Visible="False" Width="5%"></asp:TextBox>
                           <asp:TextBox ID="txtFileLink" runat="server" AutoCompleteType="Disabled" AutoPostBack="false" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox><br /></td></tr>
               <br />
                        </td>
                    </tr>
                 
         <tr>
             <td class="CellFormat">Select File to Upload </td>
             <td colspan="1" class="CellTextBox" style="text-align:center">
                 <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" CssClass="Centered" /></td>
         </tr>
         <tr>
             <td class="CellFormat">Description </td>
             <td colspan="1" class="CellTextBox" style="text-align:left">
                 <asp:TextBox ID="txtFileDescription" runat="server" Width="70%"></asp:TextBox></td>
         </tr>
         <tr>
             <td colspan="2" class="centered"><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="currentdatetime()" CssClass="roundbutton1" /></td>

         </tr>
         <tr><td><br /></td></tr><tr><td colspan="2"><asp:GridView ID="gvUpload" runat="server" AutoGenerateColumns="False" EmptyDataText = "No files uploaded" Width="90%" CssClass="Centered" DataSourceID="SqlDSUpload" AutoGenerateEditButton="false">
             <Columns>
                 <asp:BoundField DataField="FileName" HeaderText="File Name" />
                  <asp:BoundField DataField="FileDescription" HeaderText="File Description" />
                 <%--  <asp:TemplateField ><ItemTemplate> <asp:ImageButton ID="btnEditFileDesc" runat="server" CssClass="righttextbox" Height="18px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="20px" OnClick="btnEditFileDesc_Click"  />
              </ItemTemplate></asp:TemplateField>--%>
                 <asp:BoundField DataField="CreatedOn" DataFormatString="{0:d}" HeaderText="UploadDate" SortExpression="CreatedOn" />
                 <asp:BoundField DataField="CreatedBy" HeaderText="UploadedBy" SortExpression="CreatedBy" />
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton ID = "lnkPreview" Text = "Preview" CommandArgument = '<%# Eval("FileNameLink")%>' runat = "server" OnClick = "PreviewFile" />
                     </ItemTemplate>

                 </asp:TemplateField>
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("FileNameLink")%>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>

                     </ItemTemplate>

                 </asp:TemplateField>
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton ID = "lnkDelete" Text = "Delete" CommandArgument = '<%# Eval("FileNameLink")%>' runat = "server" OnClick = "DeleteFile" />

                     </ItemTemplate>


                 </asp:TemplateField>

             </Columns>
             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
             <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
             <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" />
             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
             <SortedAscendingCellStyle BackColor="#E4E4E4" /><SortedAscendingHeaderStyle BackColor="#000066" />
             <SortedDescendingCellStyle BackColor="#E4E4E4" /><SortedDescendingHeaderStyle BackColor="#000066" />

                                                     </asp:GridView>

             <asp:SqlDataSource ID="SqlDSUpload" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" 
              SelectCommand="SELECT * FROM tblfileupload where fileref = 'aa'"></asp:SqlDataSource>
                                     </td></tr>

                </table>
                    </td>
                    </tr>
                  <tr style="height:800px;width:100%">
                    <td style="height:100%;width:100%">
                        <br />
                         <iframe runat="server" id ="iframeid" style="width:80%;height:80%" ></iframe>
                    </td>
                </tr>
            </table>
            </ContentTemplate>
        </asp:TabPanel>
       <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Notes">
           <HeaderTemplate> <asp:Label ID="lblNotesCount" runat="server" Font-Size="11px" Text="Notes"></asp:Label>

           </HeaderTemplate><ContentTemplate>
            <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Notes</h3>
    
       <table style="width:100%;text-align:center;">
            <%--<tr>
                <td colspan="2"><br /></td>
            </tr>
            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblmessagenotesmaster" runat="server"></asp:Label>
                      </td> 
            </tr>
                 <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertNotesMaster" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                       <asp:Label ID="Label15" runat="server" Text="" CssClass="dummybutton"></asp:Label>
</td>
                     
            </tr>--%>
            
            <tr>
              
                <td colspan="2" style="text-align:left;">
                  
                     <asp:Button ID="btnAddNotesMaster" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEditNotesMaster" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
<asp:Button ID="btnDeleteNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
                
                        <asp:Button ID="btn" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
            
<%--                       </td>
                <td style="text-align: right">--%>
            
                    <asp:Button ID="btnQuitNotesMaster" runat="server" Visible="false"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CLOSE" Width="100px" />

                </td>
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">

                     <asp:GridView ID="gvNotesMaster" runat="server" DataSourceID="SqlDSNotesMaster" OnRowDataBound = "OnRowDataBoundgNotes" OnSelectedIndexChanged = "OnSelectedIndexChangedgNotes" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" GridLines="Vertical" ForeColor="#333333" >
             <AlternatingRowStyle BackColor="White" />
            <Columns>
                  <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false" >
                <ControlStyle Width="150px" />
                <ItemStyle Width="150px" />
                </asp:CommandField>
                <asp:BoundField DataField="StaffID" HeaderText="StaffID" SortExpression="StaffID">
                     <ControlStyle Width="150px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes">
                     <ControlStyle Width="300px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                       <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EntryDate" SortExpression="CreatedOn" Visible="true" />
                                           
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                    <asp:BoundField DataField="CreatedBy" HeaderText="EntryStaff" SortExpression="CreatedBy" Visible="true" />
                                                   <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
                         
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
              <td colspan="2"><br /></td>
          </tr>
           <tr>
               <td class="CellFormat"><%--KeyField--%> </td>
               <td class="CellTextBox"><asp:Label ID="lblNotesKeyField" runat="server" MaxLength="100" Height="16px" Width="40%" ReadOnly="True" BackColor="#CCCCCC" Visible="False"></asp:Label></td>

           </tr>
            <tr>
               <td class="CellFormat">StaffID </td>
               <td class="CellTextBox"><asp:Label ID="lblNotesStaffID" runat="server" MaxLength="100" Height="16px" Width="40%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label></td>

           </tr>
            <tr>
                     

                    <td class="CellFormat">Notes<asp:Label ID="Label8" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBox"> <asp:TextBox ID="txtNotes" runat="server" MaxLength="50" Height="60px" Width="80%" TextMode="MultiLine"></asp:TextBox></td>
                     

                 </tr>
           
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSaveNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancelNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
         <asp:SqlDataSource ID="SqlDSNotesMaster" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" 
              SelectCommand="SELECT * FROM tblnotes where Keyfield = 'aa'">
                     
              
</asp:SqlDataSource>
    </div>
                      <asp:TextBox ID="txtNotesRcNo" runat="server" CssClass="dummybutton"></asp:TextBox>
                       <asp:TextBox ID="txtNotesMode" runat="server" CssClass="dummybutton"></asp:TextBox>
         </ContentTemplate>
         </asp:TabPanel>
                   
  
    <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Notes"><HeaderTemplate>Customer Portal Access</HeaderTemplate><ContentTemplate><div style="text-align:center"><h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Client Access</h3><table style="width:100%;text-align:center;"><tr><td colspan="2" style="text-align:left;">
         <asp:Button ID="btnAddCP" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px"  />
         <asp:Button ID="btnEditCP" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
       <asp:Button ID="btnChStCP" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CH-ST" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Visible="False"/>
      
            <asp:Button ID="btnDeleteCP" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
      
             <asp:Button ID="btnPrintCP" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
         <asp:Button ID="btnCloseCP" runat="server" Visible="False"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CLOSE" Width="100px" />

                                                                                                                                                                                                                                                                                                                     </td></tr><tr><td colspan="2"><br /></td></tr>
         <tr class="Centered"><td colspan="2"><asp:GridView ID="gvCP" runat="server" DataSourceID="SqlDSCP" OnRowDataBound = "OnRowDataBoundgCP" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" GridLines="Vertical" ForeColor="#333333" ><AlternatingRowStyle BackColor="White" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False" ><ControlStyle Width="150px" /><ItemStyle Width="150px" /></asp:CommandField><asp:BoundField DataField="AccountID" HeaderText="AccountID" SortExpression="AccountID"><ControlStyle Width="150px" /><HeaderStyle Font-Size="12pt" /><ItemStyle Width="80px" HorizontalAlign="Left" /></asp:BoundField>
             <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" >
             <ItemStyle Width="150px" />
             </asp:BoundField>
             <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" />
             <asp:BoundField DataField="UserID" HeaderText="User ID" >
             <ItemStyle Width="120px" />
             </asp:BoundField>
             <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"><ControlStyle Width="300px" /><HeaderStyle Font-Size="12pt" /><ItemStyle Width="70px" HorizontalAlign="Left" /></asp:BoundField>
             <asp:BoundField DataField="LastLogin" HeaderText="Last Login" SortExpression="LastLogin" />
             <asp:BoundField DataField="CreatedOn" HeaderText="EntryDate" SortExpression="CreatedOn" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="CreatedBy" HeaderText="EntryStaff" SortExpression="CreatedBy" /><asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" /><asp:BoundField DataField="LastModifiedOn" HeaderText="EditedOn" SortExpression="LastModifiedOn" /></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
             <SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr>
          
          <tr><td class="CellFormat">Account ID</td><td class="CellTextBox"><asp:TextBox ID="txtAccountIDCP" runat="server" MaxLength="50" Height="16px" Width="50%"></asp:TextBox></td></tr>
          
         
          
          <tr><td class="CellFormat">Name<asp:Label ID="Label35" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
              <td class="CellTextBox"><asp:TextBox ID="txtNameCP" runat="server" MaxLength="200" Height="16px" Width="50%"></asp:TextBox></td></tr>
          <tr>
              <td class="CellFormat">Email</td>
              <td class="CellTextBox"><asp:TextBox ID="txtEmailCP" runat="server" MaxLength="200" Height="16px" Width="50%"></asp:TextBox>;</td>
          </tr>
          <tr>
              <td class="CellFormat">User ID</td>
              <td class="CellTextBox"><asp:TextBox ID="txtUserIDCP" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox></td>
          </tr>
          <tr>
              <td class="CellFormat">Password</td>
              <td class="CellTextBox"><asp:TextBox ID="txtPwdCP" runat="server" MaxLength="50" Height="16px" Width="50%" TextMode="Password"></asp:TextBox></td>
          </tr>
           <tr><td class="CellFormat">Active </td>
              <td class="CellTextBox">
                  <asp:CheckBox ID="chkStatusCP" runat="server" />
              </td></tr>
          <tr>
              <td class="CellFormat">Change Password on Next Logon</td>
              <td class="CellTextBox">
                  <asp:CheckBox ID="chkChangePasswordonNextLogin" runat="server" />
              </td>
          </tr>
          <tr><td colspan="2" style="text-align:right">
                                                                                                                                                                                                                                                                                                                         <asp:Button ID="btnSaveCP" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/><asp:Button ID="btnCancelCP" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table><asp:SqlDataSource ID="SqlDSCP" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div>
         <asp:TextBox ID="txtCPRcno" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtCPMode" runat="server" CssClass="dummybutton"></asp:TextBox></ContentTemplate>

      </asp:TabPanel>
                    </asp:TabContainer>
                    <%--   <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label37" runat="server" Text="Address" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label38" runat="server" Text="Building" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBuilding" runat="server" MaxLength="100" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label39" runat="server" Text="Street" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtStreet" runat="server" MaxLength="100" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label40" runat="server" Text="City" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox">  <asp:DropDownList ID="ddlCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCity" DataTextField="City" DataValueField="City" onchange="UpdateBillingDetails()" >
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList> 
                                                <%--<asp:ListSearchExtender ID="ddllsCity" runat="server" TargetControlID="ddlCity" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label41" runat="server" Text="State" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"> <asp:DropDownList ID="ddlState" runat="server" CssClass="chzn-select" AppendDataBoundItems="true" Width="99%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" onchange="UpdateBillingDetails()" >
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList> 
                                     <%--<asp:ListSearchExtender ID="ddllsState" runat="server" TargetControlID="ddlState" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>
                                </tr>
                           <tr>
                             <td class="CellFormat">
                                <asp:Label ID="Label42" runat="server" Text="Country" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox">  <asp:DropDownList ID="ddlCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCountry" DataTextField="Country" DataValueField="Country" onchange="UpdateBillingDetails()" >
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList>
                                   <%--<asp:ListSearchExtender ID="ddllsCountry" runat="server" TargetControlID="ddlCountry" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>
                             
                            <td class="CellFormat">
                                <asp:Label ID="Label43" runat="server" Text="Postal" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtPostal" runat="server" MaxLength="20" Height="16px" Width="98%" onchange="UpdateBillingDetails()" ></asp:TextBox>  </td>
                           
                          </tr>
                           <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label44" runat="server" Text="Tel" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"> <asp:TextBox ID="txtTelephone" runat="server" MaxLength="50" Height="16px" Width="98%" onchange="UpdateBillingDetails()" ></asp:TextBox>  </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label45" runat="server" Text="Fax" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtFax" runat="server" MaxLength="50" Height="16px" Width="98%" onchange="UpdateBillingDetails()" ></asp:TextBox>  </td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label2" runat="server" Text="Mobile" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtMobile" runat="server" MaxLength="100" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                           
                               </tr>
                              <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label46" runat="server" Text="Email" Width="40px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                              <asp:TextBox ID="txtPager" runat="server" MaxLength="50" Height="16px" Width="170px"  onchange="UpdateBillingDetails()" Visible="false"></asp:TextBox>
                 
                               </tr>--%>
                         
</td>
                </tr>
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
                                <td class="CellFormat">Status
                               </td>
                              <td class="CellTextBox" colspan="1">     <asp:DropDownList ID="ddlSearchStatus" runat="server" Width="50%">
                                  <asp:ListItem Text="--SELECT--" Value="-1" />
                                   <asp:ListItem Value="O">O - Open</asp:ListItem>
                                  <%-- <asp:ListItem Value="T">T - Terminated</asp:ListItem>--%>
                               
                               </asp:DropDownList>&nbsp;<asp:CheckBox ID="chkSearchInactive" runat="server" Text=" Inactive" Visible="True" textalign="left" Font-Bold="true" Font-Names ="Comic Sans" />
                            </td>                              
                           </tr>
                          <tr>
                               <td class="CellFormat">Name
                               </td>
                             <td class="CellTextBox" colspan="3">    <asp:TextBox ID="txtSearchCompany" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox>
                            </td>
                             </tr>
                         <tr>
                               <td class="CellFormat">ResidentialAddress
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
                    
                         <tr><td colspan="4"><br /></td></tr>
                         <tr>
                             <td colspan="2" style="text-align:right;padding-right:20px;"><asp:Button ID="btnSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Search" Width="100px"/></td>
                             <td colspan="2" style="text-align:left;"><asp:Button ID="btnClose" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
                         </tr>

        </table>
           </asp:Panel>
         <asp:Panel ID="pnlPopupInvoiceDetails" runat="server" BackColor="White" Width="700px" Height="90%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
        <%--<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton4" PopupControlID="pnlPopupInvoiceDetails" TargetControlID="btnDummyInvoice"  Enabled="True" DynamicServicePath=""></asp:ModalPopupExtender>--%>
                       
                <table border="0" style="width:100%">
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
              <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
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
         DataNavigateUrlFormatString="{0}.aspx?VoucherNumber={1}&CustomerFrom=Residential" HeaderText="View" DataTextFormatString="View" target="_blank" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="White" ItemStyle-ForeColor="Black" />
           
            </Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#0099CC" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView>\
                  <br />
                  <div style="text-align:right;width:93%;padding-right:80px">
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
                    <asp:Button ID="btnCloseInvoice" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

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
                
                        &nbsp;  <a href="RV_ReceiptTransactions.aspx" target="_blank"><button class="roundbutton1"  style="background-color:#CFC6C0;font-weight:bold;width:190px; font-family:Calibri;font-size:14px;" type="button">View Receipt Transactions</button></a>
                
                  &nbsp; 
                    <a href="RV_TransactionSummary.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:190px; font-family:Calibri;font-size:14px;" type="button">View Transaction Summary</button></a>
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
                                   

                                   <td class="CellFormat">Billing Name<asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
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
             <%--start--%>

                  <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="540px" Height="300px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
     <%--                <table style="width:100%;padding-left:15px">
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
                               <td class="CellFormat">Change Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlNewStatus" runat="server" Width="51%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                     <asp:ListItem Value="True">Active</asp:ListItem>
                                   <asp:ListItem Value="False">InActive</asp:ListItem> 
                                    
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
                                             <table border="0" style="width:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:center;padding-left:2px;">
           
                
               <tr>
                             <td><br /><br /></td>
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
                              <td class="CellFormat">Remarks <asp:Label ID="Label44" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtStatusRemarks" runat="server" Height="45px" MaxLength="2000" Width="90%" TextMode="MultiLine"></asp:TextBox>
                              </td>
                         </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="button" OnClientClick="currentdatetime();" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                              <asp:Button ID="Button1" runat="server" CssClass="dummybutton" />

            <asp:Button ID="btnConfirmNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
           </asp:Panel>

           <asp:ModalPopupExtender ID="mdlPopupStatus" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlStatus" TargetControlID="btndummy1" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
    <asp:Button ID="btndummy1" runat="server" CssClass="dummybutton" />

          <%-- end--%>
                  
                  <%-- START--%>

               <asp:Panel ID="pnlCustExists" runat="server" BackColor="White" Width="70%" Height="70%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
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
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnCustExistsOk" runat="server" CssClass="roundbutton1" OnClientClick="currentdatetime();" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                             
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
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmDelete" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmDeleteNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

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
                      <asp:Label ID="Label28" runat="server"></asp:Label>
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
                                    <asp:DropDownList ID="ddlContractGroupEdit" runat="server" AppendDataBoundItems="True" Width="40.5%">
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
                      <asp:Label ID="Label15" runat="server"></asp:Label>
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
                                         <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                             <EditItemTemplate>
                                                 <asp:Label ID="Label63" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                                             </EditItemTemplate>
                                             <ItemTemplate>
                                                 <asp:Label ID="Label63" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:BoundField DataField="Zone" HeaderText="Zone" />
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
                                 <asp:TextBox ID="txtAccountIDEdit" runat="server" ReadOnly="TRUE" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="80%"></asp:TextBox>
                        </td>
                         </tr>
                          <tr>
                              <td class="CellFormat">Client Name</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtNameEdit" runat="server" ReadOnly="TRUE" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="80%"></asp:TextBox>
                              </td>
                         </tr>
                     
                         <tr>
                             <td class="CellFormat">&nbsp;</td>
                             <td class="CellTextBox">&nbsp;</td>
                         </tr>
                          <tr>
            <td class="CellFormat">Credit Terms<asp:Label ID="Label17" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
           </td>
        
        <td class="CellTextBox"><asp:DropDownList ID="ddlTermsEdit" runat="server" AppendDataBoundItems="True" Height="25px" Width="80%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td>

                         </tr>
                         
          <tr><td class="CellFormat">Currency<asp:Label ID="Label18" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td>
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
                              <td class="CellFormat">Send Hard Copy</td>
                              <td class="CellTextBox">
                                          <asp:CheckBox ID="chkSendStatementInvEdit" runat="server" Text="Invoice" TextAlign="right" Font-Bold="True" Font-Names="Calibri" Font-Size="15px" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chkSendStatementSOAEdit" runat="server" Text="SOA (Statement of Accounts)" textalign="right" Font-Bold="true" Font-Names="Calibri" Font-Size="15px"/>
                                                        
                             
                              </td>
                         </tr>
        <tr>
            <td class="CellFormat">Auto Email</td>
            <td class="CellTextBox">
                     <asp:CheckBox ID="chkAutoEmailInvoiceEdit" runat="server" Text="Invoice" TextAlign="right" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"/>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chkAutoEmailStatementEdit" runat="server" Text="SOA (Statement of Accounts)" textalign="right" Font-Bold="true" Font-Names="Calibri" Font-Size="15px"/>
                                                             
            
            </td>
        </tr>
                            <tr>
            <td class="CellFormat">Requires E-Billing</td>
            <td class="CellTextBox">
                <asp:CheckBox ID="chkRequireEBillingEdit" runat="server" />
            </td>
        </tr>
       <%--  <tr>
                              <td class="CellFormat">Send Statement</td>
                              <td class="CellTextBox">
                                  <asp:CheckBox ID="chkSendStatementEdit" runat="server" />
                              </td>
                         </tr>
        <tr>
            <td class="CellFormat">Auto Email Invoice</td>
            <td class="CellTextBox">
                <asp:CheckBox ID="chkAutoEmailInvoiceEdit" runat="server" />
            </td>
        </tr>
                           <tr>
            <td class="CellFormat">Auto Email Statement</td>
            <td class="CellTextBox">
                <asp:CheckBox ID="chkAutoEmailStatementEdit" runat="server" />
            </td>
        </tr>--%>
       

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
                            <td class="CellFormat">&nbsp;</td>
                            <td class="CellTextBox">
                                <asp:TextBox ID="txtSvcCP1PositionUpdateContactInformation" runat="server" Height="16px" MaxLength="100" Width="80%" Visible="False"></asp:TextBox>
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
                   <td class="CellFormat">&nbsp;</td>
                   <td class="CellTextBox">
                                <asp:TextBox ID="txtSvcCP2PositionUpdateContactInformation" runat="server" Height="16px" MaxLength="100" Width="80%" Visible="False"></asp:TextBox>
                            
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
                         
                          &nbsp;<asp:Label ID="Label29" runat="server" Text="Do you wish to update the Contact Information for all the selected sites?"></asp:Label>
                        
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
                         
                          &nbsp;<asp:Label ID="Label41" runat="server"></asp:Label>
                        
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


      <asp:ModalPopupExtender ID="mdlPopupDeleteUploadedFile" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmDeleteUploadedFile" TargetControlID="btndummyDeleteUploadedFile" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyDeleteUploadedFile" runat="server" CssClass="dummybutton" />

             <%-- Confirm Delete uploaded file--%>              
                                <asp:TextBox ID="txtAcctCode" runat="server" Height="16px" MaxLength="50" Width="98%" AutoPostBack="TRUE" Visible="false"></asp:TextBox>
                              <asp:TextBox ID="txtID" ReadOnly="true" Enabled="false" runat="server" MaxLength="10" Height="16px" Width="98%" Visible="false"></asp:TextBox></td>
                             
                             </td>
                
             <asp:DropDownList ID="ddlSalesGrp" runat="server" Visible="false" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSSalesGroup" DataTextField="salesgroup" DataValueField="salesgroup" Height="25px" Width="155px">
                                     <asp:ListItem Text="--SELECT--" Value="-1" />
                                 </asp:DropDownList>
                             
                       
                       <asp:CheckBox ID="chkCustomer" runat="server" Text="IsCustomer" Visible="false" />
                                 &nbsp;&nbsp;<asp:CheckBox ID="chkSupplier" runat="server" Text="IsSupplier" Visible="false" />
                       
                
                          <%--      <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label10" runat="server" Text="Address" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillAddress" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label11" runat="server" Text="Building" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillBuilding" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label12" runat="server" Text="Street" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillStreet" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                           
                               </tr>
                           <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label13" runat="server" Text="City" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox">  <asp:DropDownList ID="ddlBillCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCity" DataTextField="City" DataValueField="City">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList>
                                     <%--<asp:ListSearchExtender ID="ddllsBillCity" runat="server" TargetControlID="ddlBillCity" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label14" runat="server" Text="State" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"> <asp:DropDownList ID="ddlBillState" runat="server" CssClass="chzn-select" AppendDataBoundItems="true" Width="99%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList> 
                                    <asp:ListSearchExtender ID="ddllsBillState" runat="server" TargetControlID="ddlBillState" PromptPosition="Bottom"></asp:ListSearchExtender>
   
                            </td>
                                </tr>
                           <tr>
                             <td class="CellFormat">
                                <asp:Label ID="Label15" runat="server" Text="Country" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox">  <asp:DropDownList ID="ddlBillCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCountry" DataTextField="Country" DataValueField="Country">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList> 
                                <asp:ListSearchExtender ID="ddllsBillCountry" runat="server" TargetControlID="ddlBillCountry" PromptPosition="Bottom"></asp:ListSearchExtender>
                            </td>
                             
                            <td class="CellFormat">
                                <asp:Label ID="Label16" runat="server" Text="Postal" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillPostal" runat="server" MaxLength="20" Height="16px" Width="98%"></asp:TextBox>  </td>
                           
                          </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label17" runat="server" Text="Tel" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"> <asp:TextBox ID="txtBillTel" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox>  </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label18" runat="server" Text="Fax" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillFax" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox>  </td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label19" runat="server" Text="Mobile" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillMobile" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                           
                               </tr>
                              <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label20" runat="server" Text="Email" Width="40px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillEmail" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                              <asp:TextBox ID="TextBox8" runat="server" MaxLength="50" Height="16px" Width="170px"  onchange="UpdateBillingDetails()" Visible="false"></asp:TextBox>
                   <asp:TextBox ID="txtBillPager" runat="server" MaxLength="50" Height="16px" Width="420px" Visible="False"></asp:TextBox>
                      
                               </tr>
                            <tr>                               
                                <td class="CellFormat">
                                    <asp:Label ID="Label21" runat="server" Text="Contact" Width="50px"></asp:Label>
                                </td>
                                <td class="CellTextBox" colspan="7">
                                    <asp:TextBox ID="txtBillContact" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                                </td>
                         --%>
 <%--<asp:TextBox ID="txtBlock" runat="server" MaxLength="10" Height="16px" Width="95%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                         <asp:TextBox ID="txtNo" runat="server" MaxLength="10" Height="16px" Width="95%" onchange="UpdateBillingDetails()" ></asp:TextBox></td> 
                        <asp:TextBox ID="txtFloor" runat="server" MaxLength="10" Height="16px" Width="95%" onchange="UpdateBillingDetails()" ></asp:TextBox></td> 
                   <asp:TextBox ID="txtUnit" runat="server" MaxLength="10" Height="16px" Width="95%" onchange="UpdateBillingDetails()" ></asp:TextBox></td> 
                   --%>
                         <%--   <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label37" runat="server" Text="Address" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label38" runat="server" Text="Building" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBuilding" runat="server" MaxLength="100" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label39" runat="server" Text="Street" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtStreet" runat="server" MaxLength="100" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label40" runat="server" Text="City" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox">  <asp:DropDownList ID="ddlCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCity" DataTextField="City" DataValueField="City" onchange="UpdateBillingDetails()" >
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList> 
                                                <%--<asp:ListSearchExtender ID="ddllsCity" runat="server" TargetControlID="ddlCity" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label41" runat="server" Text="State" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"> <asp:DropDownList ID="ddlState" runat="server" CssClass="chzn-select" AppendDataBoundItems="true" Width="99%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State" onchange="UpdateBillingDetails()" >
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList> 
                                     <%--<asp:ListSearchExtender ID="ddllsState" runat="server" TargetControlID="ddlState" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>
                                </tr>
                           <tr>
                             <td class="CellFormat">
                                <asp:Label ID="Label42" runat="server" Text="Country" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox">  <asp:DropDownList ID="ddlCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCountry" DataTextField="Country" DataValueField="Country" onchange="UpdateBillingDetails()" >
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList>
                                   <%--<asp:ListSearchExtender ID="ddllsCountry" runat="server" TargetControlID="ddlCountry" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>
                             
                            <td class="CellFormat">
                                <asp:Label ID="Label43" runat="server" Text="Postal" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtPostal" runat="server" MaxLength="20" Height="16px" Width="98%" onchange="UpdateBillingDetails()" ></asp:TextBox>  </td>
                           
                          </tr>
                           <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label44" runat="server" Text="Tel" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"> <asp:TextBox ID="txtTelephone" runat="server" MaxLength="50" Height="16px" Width="98%" onchange="UpdateBillingDetails()" ></asp:TextBox>  </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label45" runat="server" Text="Fax" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtFax" runat="server" MaxLength="50" Height="16px" Width="98%" onchange="UpdateBillingDetails()" ></asp:TextBox>  </td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label2" runat="server" Text="Mobile" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtMobile" runat="server" MaxLength="100" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                           
                               </tr>
                              <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label46" runat="server" Text="Email" Width="40px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Height="16px" Width="99%" onchange="UpdateBillingDetails()" ></asp:TextBox></td>
                              <asp:TextBox ID="txtPager" runat="server" MaxLength="50" Height="16px" Width="170px"  onchange="UpdateBillingDetails()" Visible="false"></asp:TextBox>
                 
                               </tr>--%>
                         <%-- <asp:TextBox ID="txtBillBlock" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>
                            <asp:TextBox ID="txtBillNo" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>
                       <asp:TextBox ID="txtBillFloor" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox> 
                 <asp:TextBox ID="txtBillUnit" runat="server" MaxLength="10" Height="16px" Width="95%"></asp:TextBox>--%>
                   
                       <%--      <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label10" runat="server" Text="Address" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillAddress" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label11" runat="server" Text="Building" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillBuilding" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label12" runat="server" Text="Street" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillStreet" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                           
                               </tr>
                           <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label13" runat="server" Text="City" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox">  <asp:DropDownList ID="ddlBillCity" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCity" DataTextField="City" DataValueField="City">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList>
                                     <%--<asp:ListSearchExtender ID="ddllsBillCity" runat="server" TargetControlID="ddlBillCity" PromptPosition="Bottom"></asp:ListSearchExtender>
    
                            </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label14" runat="server" Text="State" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"> <asp:DropDownList ID="ddlBillState" runat="server" CssClass="chzn-select" AppendDataBoundItems="true" Width="99%" DataSourceID="SqlDSState" DataTextField="State" DataValueField="State">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList> 
                                    <asp:ListSearchExtender ID="ddllsBillState" runat="server" TargetControlID="ddlBillState" PromptPosition="Bottom"></asp:ListSearchExtender>
   
                            </td>
                                </tr>
                           <tr>
                             <td class="CellFormat">
                                <asp:Label ID="Label15" runat="server" Text="Country" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox">  <asp:DropDownList ID="ddlBillCountry" runat="server" CssClass="chzn-select" AppendDataBoundItems="True" Width="99%" DataSourceID="SqlDSCountry" DataTextField="Country" DataValueField="Country">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                               </asp:DropDownList> 
                                <asp:ListSearchExtender ID="ddllsBillCountry" runat="server" TargetControlID="ddlBillCountry" PromptPosition="Bottom"></asp:ListSearchExtender>
                            </td>
                             
                            <td class="CellFormat">
                                <asp:Label ID="Label16" runat="server" Text="Postal" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillPostal" runat="server" MaxLength="20" Height="16px" Width="98%"></asp:TextBox>  </td>
                           
                          </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label17" runat="server" Text="Tel" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"> <asp:TextBox ID="txtBillTel" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox>  </td>
                            <td class="CellFormat">
                                <asp:Label ID="Label18" runat="server" Text="Fax" Width="40px"></asp:Label></td>
                            <td colspan="3" class="CellTextBox"><asp:TextBox ID="txtBillFax" runat="server" MaxLength="50" Height="16px" Width="98%"></asp:TextBox>  </td>
                           
                               </tr>
                            <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label19" runat="server" Text="Mobile" Width="50px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillMobile" runat="server" MaxLength="100" Height="16px" Width="99%"></asp:TextBox></td>
                           
                               </tr>
                              <tr>
                            <td class="CellFormat">
                                <asp:Label ID="Label20" runat="server" Text="Email" Width="40px"></asp:Label></td>
                            <td colspan="7" class="CellTextBox"> <asp:TextBox ID="txtBillEmail" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                              <asp:TextBox ID="TextBox8" runat="server" MaxLength="50" Height="16px" Width="170px"  onchange="UpdateBillingDetails()" Visible="false"></asp:TextBox>
                   <asp:TextBox ID="txtBillPager" runat="server" MaxLength="50" Height="16px" Width="420px" Visible="False"></asp:TextBox>
                      
                               </tr>
                            <tr>                               
                                <td class="CellFormat">
                                    <asp:Label ID="Label21" runat="server" Text="Contact" Width="50px"></asp:Label>
                                </td>
                                <td class="CellTextBox" colspan="7">
                                    <asp:TextBox ID="txtBillContact" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                                </td>
                         --%>
                   
            
           <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">

        </asp:SqlDataSource>
              <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSSalesGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT salesgroup FROM tblsalesgroup order by salesgroup">
                       
            </asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
              <asp:SqlDataSource ID="SqlDSSalesMan" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
            
             <asp:SqlDataSource ID="SqlDSState" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT State FROM tblstate WHERE (Rcno &lt;&gt; 0) ORDER BY State"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
            
            <asp:SqlDataSource ID="SqlDSCity" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSCountry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDSTerms" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
       
             <asp:SqlDataSource ID="SqlDSCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Currency FROM tblCurrency   ORDER BY Currency "></asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDSARBal" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT AccountId, Bal FROM companybal"></asp:SqlDataSource>
            <br />
            <br />
       
           <asp:TextBox ID="txtGoogleEmail" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="False"></asp:TextBox>
          
       <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtDetail" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>
           <asp:TextBox ID="txtSelectDate" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>
           <asp:TextBox ID="txtDisplayTimeInTimeOutServiceRecord" runat="server" Visible="False"></asp:TextBox>        
          
           <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox> 
           <asp:TextBox ID="txtPostalValidate" runat="server" Visible="False"></asp:TextBox>
        <asp:DropDownList ID="ddlSex" runat="server" Width="10%" Visible="False">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                                   <asp:ListItem Value="MALE">MALE</asp:ListItem>
                                   <asp:ListItem Value="FEMALE">FEMALE</asp:ListItem>
                                  
                               </asp:DropDownList>
                 
           <asp:DropDownList ID="ddlIC" runat="server" Width="10%" Height="25px" CssClass="chzn-select" AppendDataBoundItems="True" Visible="False">
                                          <asp:ListItem Text="--SELECT--" Value="-1" />
                                          <asp:ListItem Value="S/L/P">S/L/P - Singaporean</asp:ListItem>
                                          <asp:ListItem Value="B">B  -  Permanent Residence</asp:ListItem>
                                          <asp:ListItem Value="W">W  -  Workpass Holder</asp:ListItem>
                                          <asp:ListItem Value="F">F  -  Foreigner</asp:ListItem>
                                          <asp:ListItem Value="O">O  -  Others</asp:ListItem>
                                     </asp:DropDownList>

           <asp:DropDownList ID="ddlSalute" runat="server" CssClass="chzn-select" Width="10%" Visible="False">
    <asp:ListItem Text="--SELECT--" Value="-1" Selected="True" />
                                   <asp:ListItem Value="MISS">MISS</asp:ListItem>
                                   <asp:ListItem Value="MDM">MDM</asp:ListItem>
                                   <asp:ListItem Value="MR.">MR.</asp:ListItem>
                                   <asp:ListItem Value="MRS.">MRS.</asp:ListItem>
                                   <asp:ListItem Value="MS.">MS.</asp:ListItem>
                                   <asp:ListItem Value="PROF">PROF</asp:ListItem>
                                   <asp:ListItem Value="VEN">VEN</asp:ListItem>
                                   <asp:ListItem Value="DR.">DR.</asp:ListItem>
                               </asp:DropDownList>

         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
                    <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtModal" runat="server" Visible="False"></asp:TextBox>
             <asp:TextBox ID="TextBox3" runat="server" Visible="false" ></asp:TextBox>
  <asp:TextBox ID="txtSvcAddr" runat="server" cssclass="dummybutton" ></asp:TextBox>
    </div>
  
                </ContentTemplate> 
           <Triggers>
            <asp:PostBackTrigger ControlID="tb1$TabPanel3$btnUpload" />
             <asp:PostBackTrigger ControlID="tb1$TabPanel3$gvUpload" />
               <%--<asp:PostBackTrigger ControlID="tb1$TabPanel3$btnPhotoPreview" />--%>
        </Triggers>
     </asp:UpdatePanel>
</asp:Content>


