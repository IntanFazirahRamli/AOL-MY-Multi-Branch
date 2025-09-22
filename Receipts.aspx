<%@ Page Title="Receipts" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Receipts.aspx.vb" Inherits="Receipts" Culture="en-GB" EnableEventValidation="false"   %>


<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register assembly="DropDownCheckBoxes" namespace="Saplin.Controls" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="CSS/loader2.css" />


      <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }
        .modal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>


       <style type="text/css">
                     

    .CellFormat{
        font-size:16px;
        font-weight:bold;
        font-family:Calibri;
        color:black;
        text-align:left;
        width:10%;
      
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
      .CellTextBox{
        
        color:black;
        text-align:left;
        width:40%;
     
    }
  
       
          </style>
           
    
    

<script type="text/javascript">
    window.history.forward(1);
 
   
    var submit = 0;
  
    var submit1 = 0;
    isSubmitted = false;


   

    function allowSubmissions() {
        //alert("4");
        isSubmitted = false;
        return false;
    }


    function RefreshSubmit() {
        submit = 0;
        submit1 = 0;
        isSubmitted = false;
    }


    function DoValidationSave() {

        var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
        var valid = true;


        //17.05.23

        if (!isSubmitted) {

            $('#<%=btnSave.ClientID%>').val('SAVING..WAIT');
             isSubmitted = true;
             currentdatetime();
             valid = true;
             return valid;
         }
         else {

             currentdatetime();
             valid = false;
             return valid;
         }


         allowSubmissions();

        //17.05.23

        //if (++submit > 1) {
        //    alert('Saving the Receipt is in progress.. Please wait.');
        //    submit = 0;
        //    valid = false;
        //    return valid;
        //}

        var linvoicedate = document.getElementById("<%=txtReceiptDate.ClientID%>").value;

        if (linvoicedate == '') {
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Receipt Date";
                ResetScrollPosition();
                document.getElementById("<%=txtReceiptDate.ClientID%>").focus();
                valid = false;
                return valid;
            }

            var lcompanygroup = document.getElementById("<%=txtCompanyGroup.ClientID%>").value;

         if (lcompanygroup == '--SELECT--') {
             document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Company Group";
              ResetScrollPosition();
              document.getElementById("<%=txtCompanyGroup.ClientID%>").focus();
                valid = false;
                return valid;
            }

            var lpaymentmode = document.getElementById("<%=ddlPaymentMode.ClientID%>").value;

         if ((lpaymentmode == '') || (lpaymentmode == '--SELECT--')) {
             document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Payment Mode";
            ResetScrollPosition();
            document.getElementById("<%=ddlPaymentMode.ClientID%>").focus();
                valid = false;
                return valid;
            }


            var lBankGLCode = document.getElementById("<%=txtBankGLCode.ClientID%>").value;
         if (lBankGLCode == '') {
             document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Bank";
            ResetScrollPosition();
            document.getElementById("<%=txtBankGLCode.ClientID%>").focus();
              valid = false;
              return valid;
          }

          currentdatetime();
        
          return valid;
    };


    //function RefreshSubmit() {
    //    submit = 0;
       
    //}

    function initialize() {
        document.getElementById("<%=txtReceiptNo.ClientID%>").value = '';
        currentdatetimeinvoice();
     

        isSubmitted = false;
        submit = 0;
        submit1 = 0;

        enable();
    }


    function initializeChequeReturn() {
     
         currentdatetimeinvoice();
         submit = 0;
         enable();
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


    function checkallinvoicerecs() {
        //alert("1");
        var table = document.getElementById('<%=grvInvoiceRecDetails.ClientID%>');
          var totbillamt = 0;


          if (table.rows.length > 0) {
              //alert("2");

              var input = table.rows[0].getElementsByTagName("input");

              if (input[0].id.indexOf("chkSelectInvoiceGV") > -1) {

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


                      document.getElementById("<%=txtTotalInvoiceSelected.ClientID%>").value = "0.00";
                }


                else {
                    //loop the gridview table
                    for (var i = 1; i < table.rows.length; i++) {

                        //get all the input elements
                        var inputs = table.rows[i].getElementsByTagName("input");

                        for (var j = 0; j < inputs.length; j++) {

                            //get the textbox1
                            if (inputs[j].id.indexOf("txtOSAmountGVII") > -1) {
                                inputs[0].checked = true;
                                totbillamt = totbillamt + parseFloat(inputs[j].value);

                            }

                        }
                    }
                    document.getElementById("<%=txtTotalInvoiceSelected.ClientID%>").value = (totbillamt).toFixed(2);
            }
        }
    }

}


function checkoneinvoicerec() {
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
                    if (inputs[0].checked == true)
                        if (inputs[j].id.indexOf("txtOSAmountGVII") > -1) {

                            totbillamt = totbillamt + parseFloat(inputs[j].value);
                        }
                }

            }

            document.getElementById("<%=txtTotalInvoiceSelected.ClientID%>").value = (totbillamt).toFixed(2);
        }
    }
    function ConfirmSavePost() {
        currentdatetime();
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you want to POST/UPDATE this Receipt?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }


    function ConfirmPost() {
        currentdatetime();
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Are you sure to POST the Receipt?")) {
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
        if (confirm("Are you sure to REVERSE the Receipt?")) {
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
        var lreceiptno = document.getElementById("<%=txtReceiptNo.ClientID%>").value;
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

           
                document.getElementById("<%=txtReceiptDate.ClientID%>").value = dd + "/" + mm + "/" + y;
                document.getElementById("<%=txtReceiptPeriod.ClientID%>").value = "" + y + mm;
            
            submit = 0;
        }
    

    
     



        function DoValidation(parameter) {
            
            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;

             var linvoicedate = document.getElementById("<%=txtReceiptDate.ClientID%>").value;

            if (linvoicedate == '') {
                document.getElementById("<%=lblAlert1.ClientID%>").innerText = "Please Enter Receipt Date";
                ResetScrollPosition();
                document.getElementById("<%=txtReceiptDate.ClientID%>").focus();
                valid = false;
                return valid;
            }


            var laccountIdbilling = document.getElementById("<%=txtAccountIdII.ClientID%>").value;

            if (laccountIdbilling == '') {
                document.getElementById("<%=lblAlert1.ClientID%>").innerText = "Please Select Account ID";
                ResetScrollPosition();
                document.getElementById("<%=txtAccountIdII.ClientID%>").focus();
                valid = false;
                return valid;
            }


            var lcompanygroup = document.getElementById("<%=ddlCompanyGrpII.ClientID%>").value;

            if (lcompanygroup == '--SELECT--') {
                document.getElementById("<%=lblAlert1.ClientID%>").innerText = "Please Enter Company Group";
                ResetScrollPosition();
                document.getElementById("<%=ddlCompanyGrpII.ClientID%>").focus();
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
      <asp:UpdatePanel ID="updPanelReceipt" runat="server" UpdateMode="Conditional">
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
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">RECEIPTS</h3>
          
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
                 <td colspan="13"   style="width:100%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
                  <asp:Label ID="lblInvoiceNo" runat="server" Visible="false" Text="Invoice No: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblInvoiceNo1" runat="server" Text=""  ></asp:Label>
                           <asp:Label ID="txtMode" runat="server" Text="" Visible="true" CssClass="dummybutton"></asp:Label>
                      </td> 
            </tr>

              <tr>
                <td style="width:8%;text-align:center;"> 
                   <asp:Button ID="btnADD" runat="server" Font-Bold="True" Text="ADD" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="TRUE" OnClientClick="initialize()"  />
                 
                      </td>
                  
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Visible="TRUE" Width="95%" OnClientClick="RefreshSubmit()" />
                    </td>
                     
                 
                      

                  <td style="width:8%;text-align:center;">
                        <%--<a href="RV_ReceiptConfirmation.aspx" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:95%; font-size:15px;text-align:CENTER;font-family:'CALIBRI';" type="button">PRINT</button></a>--%>
                <asp:Button ID="Button2" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="PRINT" Visible="false" Width="95%" />
                    <asp:Button ID="btnPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="PRINT" Visible="True" Width="95%" />
                    </td>
                  <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnPost" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="POST" Width="95%" OnClientClick="currentdatetime()" />
                     </td>
                  
                    <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnReverse" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="REVERSE" Visible="true" Width="95%" OnClientClick="currentdatetime();"/>
              
                    </td>

                   <td style="width:8%;text-align:center;">
                     <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CHANGE STATUS" Width="95%" />
                  
                    </td>

                  

                     <td style="width:8%;text-align:center;">
                         <asp:Button ID="btnFilter" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="FILTER" Width="95%" />
                    </td>
                
                 <td>
                      <asp:Button ID="btnJournal" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Visible="true" Font-Bold="True" Text="JOURNAL & CONTRA" Width="99%" />
                  </td>

                  <td style="width:8%;text-align:center;">
                        <asp:Button ID="btnChequeReturn" runat="server" Font-Bold="True" Text="CHEQUE RETURN" Width="98%"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="tRUE"  OnClientClick="initializeChequeReturn()" />
                  </td>
                  
                  <td style="width:8%;text-align:center;">
                        <asp:Button ID="btnCopy" runat="server" Font-Bold="True" Text="COPY" Width="30%"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="False" />
                  </td>

                   <td style="width:8%;text-align:center;">
                        <asp:Button ID="BtnDelete" runat="server" Font-Bold="True" Text="COPY" Width="30%"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="False" />
                  </td>
                     <td style="width:8%;text-align:center;">
                         <asp:Button ID="btnQuit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="QUIT" Width="95%" />
                  </td>

                  <td style="width:8%;text-align:center;">
                    <asp:Button ID="btnReset" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="RESET" Width="95%" Visible="false" />
                      </td>

                

                   <td style="width:8%;text-align:center;">
                        <asp:Button ID="btnBack" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Height="30px" Text="BACK" Visible="False" Width="95%" />
                  </td>

                  

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
       </ContentTemplate>
              </asp:UpdatePanel>
             

          <asp:UpdatePanel ID="updPnlSearch" runat="server" UpdateMode="Conditional"><ContentTemplate>

             <table id="tablesearch" runat="server" style="border: 1px solid #CC3300; text-align:right; width:100%; border-radius: 25px;padding: 2px; width:100%; height:60px; background-color: #F3F3F3;">
            <tr>
                <td style="text-align:left;width:100%;">
                   
                    <table border="0" style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000;width:100%;padding-left:2px;">
                         <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8%; ">
                                    Receipt No.                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%; ">
                                 <asp:TextBox ID="txtReceiptNoSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left; " Width="95%" AutoPostBack="True"></asp:TextBox>
                            </td>


                                        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:7%; ">
                                            Receipt Date                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:6%; ">
                                 <asp:TextBox ID="txtReceiptDateFromSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" ></asp:TextBox>         
                                 <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender1" ClearMaskOnLostFocus="false"
                                        MaskType="Date" Mask="99/99/9999" TargetControlID="txtReceiptDateFromSearch" UserDateFormat="DayMonthYear">
                                </asp:MaskedEditExtender>   
                                  <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDateFromSearch" TargetControlID="txtReceiptDateFromSearch" Enabled="True" />

                                   </td>

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%; ">
                                    To                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:7%; ">
                                 <asp:TextBox ID="txtReceiptDateToSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="90%" AutoCompleteType="Disabled" ></asp:TextBox>         
                                <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender2" ClearMaskOnLostFocus="false"
                                        MaskType="Date" Mask="99/99/9999" TargetControlID="txtReceiptDateToSearch" UserDateFormat="DayMonthYear">
                                </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDateToSearch" TargetControlID="txtReceiptDateToSearch" Enabled="True" />

                                    </td>

                                                   

                                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%; ">
                                      Remarks                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:8%; ">
                                 <asp:TextBox ID="txtCommentsSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>         
                            </td>

                                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8%; ">
                                    Bank ID                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%; ">
                                  <asp:DropDownList ID="ddlBankIDSearch" runat="server" AppendDataBoundItems="True" Height="20px" Width="95%">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                            </td>


                           <td colspan="1" style="text-align:center;width:12%">
                                       <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Search" Width="75%" />
                                 </td>
                        </tr>
                          <tr>

                        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Comp. Group
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:9%; ">
                                  <asp:DropDownList ID="ddlCompanyGrpSearch" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Height="20px" Width="95%">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                                     
                            <asp:ImageButton ID="ImageButton3" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Middle" Visible="False"     />   
                        
                            </td>
                                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                     Account Type</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:11%; ">
                                  <asp:DropDownList ID="ddlContactTypeSearch" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="80%">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                       <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                                      <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                  </asp:DropDownList>
                                </td>


                                                      
                          
                                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%; ">
                                    Acct. ID</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:9%; ">
                                 <asp:TextBox ID="txtAccountIdSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left; " Width="70%" AutoPostBack="True"></asp:TextBox>
                                 <asp:ImageButton ID="btnClientSearch" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                    
                            </td>


                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%; ">
                                    
                                    Client Name</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%; ">
                                  <asp:TextBox ID="txtClientNameSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left;padding-left:5px;" Width="90%" AutoPostBack="True"></asp:TextBox>
                            </td>
                            
                                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:10%; ">
                                    Cheque No.                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:8%; ">
                                 <asp:TextBox ID="txtChequeNoSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="90%" AutoCompleteType="Disabled" ></asp:TextBox>         
                            </td>
                               <td colspan="1" style="text-align:center;width:12%">
                                       <asp:Button ID="btnQuickReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Reset" Width="75%" OnClientClick="ClearSearch()" />
                                   </td>
                        </tr>
                          <tr>
                           
                           

                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">Invoice No.</td>
                              <td>
                                  <asp:TextBox ID="txtInvoicenoSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left; " Width="95%"></asp:TextBox>
                                                     </td>

                                        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">Post Status </td>
                             <td>
                                  <asp:TextBox ID="txtSearch1Status" runat="server" Width="65%" ReadOnly="FALSE"></asp:TextBox>
                                 <asp:ImageButton ID="btnSearch1Status" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" ImageAlign="Middle"
                                    Height="22px" Width="24px" />  
                                                <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" CancelControlID="btnStatusCancel" PopupControlID="pnlStatusSearch" TargetControlID="btnSearch1Status" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
                          
                            </td>

                             
                              
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                  <asp:Label ID="lblBranch" runat="server" Text="Branch" CssClass="CellFormat"></asp:Label>
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:9%; ">
                                 
                            <cc1:DropDownCheckBoxes ID="ddlLocationSearch" runat="server" AddJQueryReference="true" style="top: 0px; left: 0px" UseButtons="false" UseSelectAllNode="true" Width="17%">
                                       <Style2 SelectBoxWidth="97.5%" DropDownBoxBoxWidth="97.5%" DropDownBoxBoxHeight="120" />
                                   </cc1:DropDownCheckBoxes>
                            </td>
                               <td colspan="2">
                                   <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" DataTextField="locationID" DataValueField="LocationID" Height="20px" Width="46%" DataSourceID="SqlDSLocation">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                               </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:10%; ">
                                    
                                    PaymentMode</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:8%; ">
                             <asp:DropDownList ID="ddlPaymentModeSearch" runat="server" AppendDataBoundItems="True" AutoPostBack="false" Height="20px" Width="96%" >
                           <asp:ListItem>--SELECT--</asp:ListItem>
                       </asp:DropDownList>    </td>
                            <td colspan="3"></td>
                                                   

                        </tr>
                    </table>
                      </td>
                    <td style="text-align:right;width:45%;display:inline;vertical-align:middle;padding-top:10px;">
                 
                </td>
            </tr>
        </table>


     


         <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >

             <tr>
                     <td colspan="11" style="width:100%;text-align:right">
                            <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                       </td>

             </tr>
            <tr>
                <td colspan="11" style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td></tr>
             
                            <tr style="text-align:center;">

                              


                                  <td colspan="11" style="width:100%;text-align:center">
                                     <div style="text-align:center; width:100%; margin-left:auto; margin-right:auto;" >
     
                                      <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Auto" style="overflow:scroll; margin-left:auto; margin-right:auto;" Wrap="False"    Visible="true" Width="1330px">

                                          <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server"  OnRowDataBound = "OnRowDataBoundg1" OnSelectedIndexChanged = "OnSelectedIndexChangedg1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSReceipt" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                                <Columns>
                                                  <asp:CommandField ShowHeader="True" ShowSelectButton="True" Visible="false">
                                                  <ControlStyle Width="4%" />
                                                  <ItemStyle Width="4%" Wrap="False" HorizontalAlign="Left" />
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
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Print St" SortExpression="PostStatus" >
                                                      <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Post St" SortExpression="PostStatus" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="BankStatus" HeaderText="Paid St" SortExpression="BankStatus" >
                                                      <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt Number" SortExpression="ReceiptNumber">
                                                    <ControlStyle Width="6%" />
                                                  <ItemStyle Wrap="False" Width="6%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="ReceiptDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Receipt Date" SortExpression="ReceiptDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cheque" HeaderText="Cheque No." SortExpression="Cheque">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" />
                                                  <asp:BoundField DataField="ReceiptFrom" HeaderText="Client Name" SortExpression="ReceiptFrom">
                                                    <ControlStyle Width="30%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="30%" />
                                                  </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="BaseAmount" HeaderText="Receipt Amount" SortExpression="BaseAmount">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle Width="8%" HorizontalAlign="Right" Wrap="True" />
                                                  </asp:BoundField>
                                                    <asp:BoundField DataField="BankId" HeaderText="Bank" SortExpression="BankId">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PaymentType" HeaderText="Payment Mode" SortExpression="PaymentType" />
                                                    <asp:BoundField DataField="GLPeriod" HeaderText="Period" SortExpression="GLPeriod" />
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup" />
                                                    <asp:BoundField DataField="ChequeDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Cheque Date" SortExpression="ChequeDate">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ChequeReturned" HeaderText="Cheque Returned" />
                                                    <asp:BoundField DataField="Location" HeaderText="Branch" />
                                                    <asp:BoundField DataField="LedgerCode">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LedgerName">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comments">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" HeaderText="Edited On" SortExpression="Edited On" />
                                                    <asp:BoundField DataField="GSTAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NetAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Salesman">
                                                    <ControlStyle Width="8%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="8%" CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>

                                                    
                    <asp:TemplateField ><ItemTemplate> <asp:Button ID="btnEditHistory" runat="server" OnClick="btnEditHistory_Click" Text="History" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top"   Width="80px"   />
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
                                         </div>
                                  </td>
                              </tr>
             
                 <tr>
                 <td>
                      <asp:SqlDataSource ID="SQLDSReceipt" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                </asp:SqlDataSource>
                     </td><td>
              <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT companygroup FROM tblcompanygroup order by companygroup">
                       
            </asp:SqlDataSource>
                           </td><td>
                         <asp:SqlDataSource ID="SqlDSSalesMan" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId FROM tblstaff where roles= 'SALES MAN' ORDER BY STAFFID"></asp:SqlDataSource>
                     </td><td>
               <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
                   </td><td>
              <asp:SqlDataSource ID="SqlDSBankCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(BankId, ' - ', Bank) AS codedes FROM tblBank   ORDER BY BankId">
                         </asp:SqlDataSource>
           </td>
                     <td>
                       <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(industry) FROM tblindustry ORDER BY industry"></asp:SqlDataSource>
                  </td> 

                 <td>
                       <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UPPER(contractgroup) FROM tblcontractgroup ORDER BY contractgroup"></asp:SqlDataSource>
                 </td>
                          <td>
                              <asp:SqlDataSource ID="SqlDSServiceFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Frequency FROM tblServiceFrequency order by Frequency "></asp:SqlDataSource>
                     </td> 
                 <td> <asp:SqlDataSource ID="SqlDSLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="select locationid from tbllocation">
                       
            </asp:SqlDataSource>
                     &nbsp;</td>     
                <td>
                    <asp:SqlDataSource ID="SqlDSBillingFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Frequency FROM tblFrequency  order by Frequency "></asp:SqlDataSource>
                     </td>
                           </tr>
                                  <tr>
                                      <asp:TextBox ID="txt" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtPopupType" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtRcnotblServiceBillingDetail" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtcontractfrom" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtClientFrom" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtEvent" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtCondition" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtOrderBy" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                         <asp:TextBox ID="txtInvoiceSearch" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtCustomerSearch" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtImportService" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtLimit" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>

                                        <asp:TextBox ID="txtBillAddress" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                         <asp:TextBox ID="txtBillBuilding" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtBillStreet" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtBillCountry" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtBillPostal" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                            <asp:TextBox ID="txtBillState" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                            <asp:TextBox ID="txtBillCity" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>


                                      <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox> 
                                       <asp:TextBox ID="txtPostUponSave" runat="server" Visible="False"></asp:TextBox> 
                                        <asp:TextBox ID="txtOnlyEditableByCreator" runat="server" Visible="False"></asp:TextBox>
                                          <asp:TextBox ID="txtRecordCreatedBy" runat="server" Visible="False"></asp:TextBox>  
                                       <asp:TextBox ID="txtAutoEmailReceipt" runat="server" Visible="False"></asp:TextBox> 
                                      
                                           <asp:TextBox ID="txtLogDocNo" runat="server" Visible="False"></asp:TextBox> 
                                          <asp:TextBox ID="txtGrid" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                    <asp:TextBox ID="txtSQLDetail" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                    <asp:TextBox ID="txtTotDetRec" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                  
                                        </tr>
       
             </table>
      
              <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                          
               <tr style="width:100%">
                 <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; color:#800000; width:80% ">TOTAL RECEIPT AMOUNT
                 </td>

                     <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline; color:#800000; width:20% ">
                         <asp:TextBox ID="txtTotalInvoiceAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="16px" TabIndex="20" Width="50%" style="text-align:right;" Font-Bold="true" ></asp:TextBox>
             
                              </td>
           </tr>
               </table>

        </ContentTemplate>
              </asp:UpdatePanel>

              <table border="0" style="width:70%; margin:auto; border:solid; border-color:ButtonFace;text-align:left">
         	 <tr style="text-align:left;width:80%">
                <td style="text-align:left;padding-left:1px;">

            <asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="0" Font-Names="Calibri" Height="100%" Width="95%" CssClass="ajax__tab_xp"  AutoPostBack="true">
         <asp:TabPanel runat="server" HeaderText=" Receipt Info" ID="TabPanel1" TabIndex="0">
             <HeaderTemplate>
Receipt Info
</HeaderTemplate>
<ContentTemplate>

        
          <asp:UpdatePanel runat="server" ID="updPnlBillingRecs" UpdateMode="Conditional">
                  <ContentTemplate>
                      
       
          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                           
          
               <tr style="width:100%">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">RECEIPTS INFORMATION
                 </td>
           </tr>
         <tr >
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                       <asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label>
                   </td>
                   <td style="width:24%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtLocation" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>
                     <tr>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">   
                                &nbsp;</td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Status</td>
                            
                          <td style="width:24%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:TextBox ID="txtPostStatus" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%" BackColor="#CCCCCC" Enabled="False"></asp:TextBox></td>
                          <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">   
                              &nbsp;</td>
                       
                        </tr>
              
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                       <asp:TextBox ID="txtRcnoServiceRecord" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="1%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Receipt No.<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtReceiptNo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; "></td>
               </tr>
              
               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">   
                           <asp:TextBox ID="txtRcnoServiceRecordDetail" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White" TabIndex="101"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Receipt Date<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtReceiptDate" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="51" AutoPostBack="True" ></asp:TextBox>
                              <asp:CalendarExtender ID="calConDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtReceiptDate" TargetControlID="txtReceiptDate" Enabled="True" />

                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                     
                </tr>

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Receipt Period</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtReceiptPeriod" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="81.5%" TabIndex="109" BorderStyle="None"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
            
                     </tr>

              

            
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Account Type</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="81.5%" TabIndex="52">
                          <asp:ListItem>--SELECT--</asp:ListItem>
                            <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                           <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                       </asp:DropDownList>
                       &nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
             
                       </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">   
                           <asp:TextBox ID="txtRcnoInvoice" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White" TabIndex="102"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Account ID <asp:Label ID="Label3" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                       <td style="width:20%;font-size:14px; font-family:'Calibri';color:black;text-align:left;"> 
                               <asp:TextBox ID="txtAccountIdBilling" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="53" AutoPostBack="True" ></asp:TextBox>
                              
             &nbsp;<asp:ImageButton ID="btnClient" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" OnClientClick="ConfirmSelection()" Width="24px" TabIndex="24" />
          
            
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">   
                       Balance : <asp:TextBox ID="txtBalance" runat="server" Height="16px" Width="40%" AutoCompleteType="Disabled" TabIndex="112" style="text-align:right" Font-Bold="True" ForeColor="#339966"  ></asp:TextBox>
                             </td>
                    
                        </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">   
                           <asp:TextBox ID="txtContractNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White" TabIndex="103"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Account Name
                            </td>
                            <td style="width:20%;font-size:14px; font-family:'Calibri';color:black;text-align:left;"> 
                         
                            <asp:TextBox ID="txtAccountName" runat="server" Height="18px" Width="81.5%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="113" BorderStyle="None" ></asp:TextBox>
                      
                                             
                           
                              <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient" BackgroundCssClass="modalBackground" Enabled="True"  ></asp:ModalPopupExtender>

                            
         
              <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="98%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center" ScrollBars="Horizontal">
                    <table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
                    </td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160; <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
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
            <asp:BoundField DataField="BillStreetSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillBuildingSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillCitySvc" HeaderText="City">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillStateSvc" HeaderText="State">
            </asp:BoundField>
            <asp:BoundField DataField="BillCountrySvc" HeaderText="Country">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillPostalSvc" HeaderText="Postal">
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
            <asp:BoundField DataField="CompanyGroupD">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" />
            </asp:BoundField>
            <asp:BoundField DataField="Location" HeaderText="Location" />
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
                
                <asp:SqlDataSource ID="SqlDSPerson" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters>
        <asp:ControlParameter Name="ContType" ControlID="ddlContactType" PropertyName="SelectedValue" Type="String" />
        </FilterParameters>
        <SelectParameters>
        <asp:ControlParameter ControlID="ddlContactType" Name="@contType" PropertyName="Text" />
      
        </SelectParameters>
        </asp:SqlDataSource>
                
        </div></asp:Panel>        
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; margin-left:2%; padding-left:4%; width:20%;  "> 
                         
                         </td>
                    
                        </tr>

               <tr style="display:none">
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Company Group<asp:Label ID="Label59" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                       </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:TextBox ID="txtCompanyGroup" runat="server" AppendDataBoundItems="True" Height="18px" Width="81.5%" BackColor="#CCCCCC" BorderStyle="None"></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;  ">   
                            
                            <asp:Button ID="btnDummyClient" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />

                       </td>
                    
                        </tr>

               <tr>

                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">&nbsp;</td>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Salesman </td>
                     <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                         <asp:TextBox ID="txtSalesman" runat="server" AppendDataBoundItems="True" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="81.5%"></asp:TextBox>
                     </td>
                     <td  style="font-size:15px;font-family:Calibri; color:black; padding-left:1px; width:15%; text-align:left; " rowspan="6" >
                           
                           <asp:Label ID="lblOnHold" runat="server" Text=""></asp:Label>  
                         <br />
            
            <asp:GridView ID="grdViewsqlDSOnHold" runat="server" DataSourceID="sqlDSOnHold" ForeColor="#333333" AutoGenerateColumns="False" HorizontalAlign="Right" 
                CellPadding="4" GridLines="None" Width="500px"  Font-Size="13px" PageSize="8">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>

                    <asp:BoundField DataField="ContractNo" HeaderText="Contract No." >
                    <ControlStyle Width="10%" />
                    <ItemStyle Width="10%" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ServiceAddress" HeaderText="Service Address" >
                    <ControlStyle Width="25%" />
                    <ItemStyle Width="25%" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LastModifiedBy" HeaderText="Last ModifiedBy" >
                     <ControlStyle Width="5%" />
                    <HeaderStyle Width="5%" />
                    <ItemStyle Width="5%" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OnHoldDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="On-Hold Date">
                  
                    <ControlStyle Width="5%" />
                    <HeaderStyle Width="5%" />
                    <ItemStyle Width="5%" Wrap="False" />
                  
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" HorizontalAlign="Left" VerticalAlign="Top" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="sqlDSOnHold" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
      
             

                     </td>
                   </tr>

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                       &nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Amount</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtReceivedAmount" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%" style="text-align:right" TabIndex="54"></asp:TextBox></td>
               </tr>
            
                   <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Payment Mode<asp:Label ID="Label44" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:DropDownList ID="ddlPaymentMode" runat="server" AppendDataBoundItems="True" AutoPostBack="True" Height="20px" Width="81.5%" TabIndex="55">
                           <asp:ListItem>--SELECT--</asp:ListItem>
                       </asp:DropDownList>
                   </td>
           
               </tr>
                      <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                       <asp:TextBox ID="txtRecordNo" runat="server" BorderStyle="None" TabIndex="107" Width="1%" AutoCompleteType="Disabled" Height="16px"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Select Bank<asp:Label ID="Label4" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:DropDownList ID="ddlBankCode" runat="server" AppendDataBoundItems="True" AutoPostBack="True" Height="20px" Width="81.5%" TabIndex="56">
                           <asp:ListItem>--SELECT--</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                       &nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Bank Id</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBankID" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="16px" TabIndex="24" Width="81.5%"></asp:TextBox>
                   </td>
               </tr>
                   <tr>
                    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                       
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Bank Name </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBankName" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="57" Width="80%"></asp:TextBox>
                   </td>
                       </tr>
                     <tr>
                         <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                             <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                         </td>
                         <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Bank GL Code</td>
                         <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtBankGLCode" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="16px" TabIndex="24" Width="81.5%"></asp:TextBox>
                         </td>
                         <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">&nbsp;</td>
                     </tr>
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                          <asp:TextBox ID="txtRecvPrefix" runat="server" Visible="false" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="16px" TabIndex="24" Width="81.5%"></asp:TextBox>
                
                             </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Ledger Name </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtLedgerName" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="58" Width="80%"></asp:TextBox>
                   </td>
                   <td rowspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">&nbsp;</td>
               </tr>
               
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                       <asp:TextBox ID="TextBox2" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" TabIndex="131" Width="1%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Cheque No./ Ref. No. </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtChequeNo" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="59" Width="80%"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">
                       <asp:TextBox ID="TextBox5" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" TabIndex="131" Width="1%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Cheque Date </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtChequeDate" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="60" Width="80%"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtChequeDate" TargetControlID="txtChequeDate" Enabled="True" />

                         </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
               </tr>
                     <tr>
                         <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 12%; text-align: right;">&nbsp;</td>
                         <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 10%; text-align: right;">Cheque Returned</td>
                         <td style="width: 20%; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">
                             <asp:CheckBox ID="chkChequeReturned" runat="server" Enabled="False" />
                         </td>
                         <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">&nbsp;</td>
                     </tr>
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top">Remarks<asp:Label ID="Label60" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                       <asp:TextBox ID="txtComments" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px" Height="100px" TabIndex="61" TextMode="MultiLine" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>
               
                     <tr>
                         <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 12%; text-align: right;">&nbsp;</td>
                         <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 10%; text-align: right;">&nbsp;</td>
                         <td style="width: 20%; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">
                             <asp:TextBox ID="txtFrom0" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="10%"></asp:TextBox>
                         </td>
                         <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">&nbsp;</td>
                     </tr>
               <tr>
                   <td colspan="3" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                       <asp:Button ID="btnShowInvoices" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  TabIndex="28" Text="IMPORT INVOICE" Width="30%" />
                 
                       
          
                         </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtTaxRatePct" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="35%"></asp:TextBox>
                     <asp:Button ID="btnDeleteAll" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Visible="false" Text="DELETE ALL" Width="50%" TabIndex="31" />
               
                         </td>





               </tr>

                   </tr>

              </tr>

              </table>


<div>
    <asp:ModalPopupExtender ID="mdlImportServices" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton5" PopupControlID="pnlImportServices" TargetControlID="btnDummyImportService">
            </asp:ModalPopupExtender>    
  
   <asp:Button ID="btnDummyImportService" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" /> 
      
                              <%--  start--%>


        <asp:Panel ID="pnlImportServices" runat="server" BackColor="White" Width="95%" Height="650px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
                     
        <table border="0"  style="width:100%;text-align:center; padding-top:1px; margin-left: 0%; margin-right:0%; border:solid; border-color:ButtonFace; "  >

                 <tr style="width:100%">
                 <td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;"> SEARCH INVOICE RECORDS 
                     
                     </td>
                  </tr>
             
                     <tr>
                          <td colspan="6"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert1" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td>
                         
                          
                                        
                     
            
                     <tr>
                            <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"></td>

                          <td style="width:8%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> &nbsp;</td>

                          <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;" colspan="4"> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                              <asp:RadioButton ID="rdbCompleted" runat="server" GroupName="ServiceStatus" Height="20px" Text="Completed" Visible="False" />
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                              <asp:RadioButton ID="rdbNotCompleted" runat="server" GroupName="ServiceStatus" Height="20px" Text="Not Completed" Visible="False" />
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                              <asp:RadioButton ID="rdbAll" runat="server" GroupName="ServiceStatus" Height="20px" Text="All" Visible="False" />
                        
                               
                            </td>

                            <tr>
                                <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Company Group</td>
                                <td style="width:9%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Account Type </td>
                                <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Account Id </td>
                                <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Client Name </td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlCompanyGrpII" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Height="25px" Value="-1" Width="90%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:9%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlContactTypeII" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="95%">
                                      <asp:ListItem>--SELECT--</asp:ListItem>
                                          <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                                        <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtAccountIdII" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" Width="80%"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlClientClose" Enabled="True" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient">
                                    </asp:ModalPopupExtender>
                                </td>
                                <td colspan="2" style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtClientNameII" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%"></asp:TextBox>
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtLocationId" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="80%"></asp:TextBox>
                                    <asp:ImageButton ID="BtnLocation" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Visible="False" Width="24px" />
                                </td>
                            </tr>
                            <tr>
                                </td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Invoice No.</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Invoice From</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Invoice To</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlContractNoII" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Visible="False" Width="90%">
                                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtInvoiceNoII" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%"></asp:TextBox>
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtDateFromII" runat="server" AutoCompleteType="Disabled" Height="16px" Width="90%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtDateFrom" TargetControlID="txtDateFromII" />
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtDateToII" runat="server" AutoCompleteType="Disabled" Height="16px" Width="90%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtDateTo" TargetControlID="txtDateToII" />
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlBillingFrequencyII" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSBillingFrequency" DataTextField="Frequency" DataValueField="Frequency" Height="25px" Value="-1" Visible="False" Width="90%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlContractGroup" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSContractGroup" DataTextField="UPPER(contractgroup)" DataValueField="UPPER(contractgroup)" Height="25px" Value="-1" Visible="False" Width="95%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="width:10%;text-align:center;color:brown;font-size:14px;font-weight:bold;font-family:'Calibri';">
                                    <asp:Label ID="txtMessage1" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:Button ID="Button1" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                    <asp:Button ID="btnDummyLocation" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                    <asp:DropDownList ID="ddlServiceFrequencyII" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSServiceFrequency" DataTextField="Frequency" DataValueField="Frequency" Height="25px" Value="-1" Visible="False" Width="15%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="TextBox4" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="35%"></asp:TextBox>
                                    <asp:Button ID="btnDummyStatusSearch" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                </td>
                                <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                </td>
                                  <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                </td>
                                <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    <asp:Button ID="btnShowRecordsII" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidation()" Text="SHOW RECORDS" Width="80%" />
                                </td>
                            </tr>
                            </tr>

                        </table>
     <%--  </ContentTemplate>
              </asp:UpdatePanel>--%>
            
      


    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="99%" Height="375px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
                
         <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label43" runat="server" Text="INVOICE DETAILS"></asp:Label>
            </td>

               </tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100% ">
        
        
        <asp:UpdatePanel ID="updpnlInvoiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="grvInvoiceRecDetails" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="100%">
                
                <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="chkSelectInvoiceGV" runat="server" AutoPostBack="false" TextAlign="Right" onchange="checkallinvoicerecs()" Width="5%" ></asp:CheckBox></HeaderTemplate>
                <ItemTemplate><asp:CheckBox ID="chkSelectGVII" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" onchange="checkoneinvoicerec()"></asp:CheckBox></ItemTemplate></asp:TemplateField>            
            
                <asp:TemplateField HeaderText="Invoice Number" ><ItemTemplate><asp:TextBox ID="txtInvoiceNumberGVII" runat="server" Text='<%# Bind("InvoiceNumber")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="125px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Inv. Date"><ItemTemplate><asp:TextBox ID="txtSalesDateGVII" runat="server" Text='<%# Bind("SalesDate")%>'  DataFormatString="{0:dd/MM/yyyy}"  Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="65px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Inv. Amount" ><ItemTemplate><asp:TextBox ID="txtAppliedBaseGVII" runat="server" Text='<%# Bind("AppliedBase")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="75px" style="text-align:right" ></asp:TextBox></ItemTemplate>
                  
                    </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Rcpt. Amt." ><ItemTemplate><asp:TextBox ID="txtTotalReceiptAmountGVII" runat="server" Text='<%# Bind("ReceiptBase")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="75px" style="text-align:right"></asp:TextBox></ItemTemplate>
                   
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="CN Amt."><ItemTemplate><asp:TextBox ID="txtTotalCNAmountGVII" runat="server" Text='<%# Bind("Creditbase")%>'  Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="75px" style="text-align:right" ></asp:TextBox></ItemTemplate>
               </asp:TemplateField>
                          <asp:TemplateField HeaderText="OS Amt."><ItemTemplate><asp:TextBox ID="txtOSAmountGVII" runat="server" Text='<%# Bind("BalanceBase")%>'  Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="75px" style="text-align:right" ></asp:TextBox></ItemTemplate>
                  
                    </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Company"><ItemTemplate><asp:TextBox ID="txtCompanyGroupGVII" runat="server" Text='<%# Bind("CompanyGroup")%>' Visible="true" Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="65px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Acct. Id" ><ItemTemplate><asp:TextBox ID="txtAccountIdGVII" runat="server" Text='<%# Bind("AccountId")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="65px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Client Name"><ItemTemplate><asp:TextBox ID="txtClientNameGVII" runat="server" Text='<%# Bind("CustName")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="535px"></asp:TextBox></ItemTemplate></asp:TemplateField>      
            
            <asp:TemplateField HeaderText="">
                <ItemTemplate><asp:TextBox ID="txtLocationIDGVII" runat="server"  Font-Size="11px" ReadOnly="true" Visible="false" Text='<%# Bind("DocType")%>'  BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
                     <asp:TemplateField HeaderText="">
                <ItemTemplate><asp:TextBox ID="txtRcnoGVII" runat="server"  Font-Size="11px" ReadOnly="true" Visible="false" Text='<%# Bind("rcno")%>'  BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
               </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
            </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvInvoiceRecDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr>
                          
              <tr>
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdateSelect" runat="server" UpdateMode="Conditional"></asp:UpdatePanel>
                       </td>
                  
            </tr>
                            
            
        </table>


                 </asp:Panel>


             <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                        <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
             
                       </td>
                         
                    <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right">
                            Total
                       </td>

                   <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left">
                   <asp:TextBox ID="txtTotalInvoiceSelected" runat="server" style="text-align:right" AutoCompleteType="Disabled"  ForeColor="Black" Height="16px" Width="35%" BackColor="#CCCCCC"></asp:TextBox>
                             
                       </td>
                 
            </tr>
                    </table>
                <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                    
                         
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 
                            <asp:SqlDataSource ID="SqlDSOSInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtAccountIdII" Name="@AccountID" PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                       <asp:Button ID="btnImportInvoiceII" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="IMPORT INVOICES" Width="15%" Visible="True" />
                       &nbsp;  
                           <asp:Button ID="btnClose" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CANCEL" Width="15%" Visible="True" />
                     
                   
                        </td>
    
                         
                 
            </tr>
                    </table>
         </asp:Panel>

                     
          <%--end--%>
</div>
            
    
                    
                 <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional"><ContentTemplate>
<asp:Panel ID="Panel2" runat="server" >
                         <table border="1" style="width:98%; padding-right:4%;  border:solid; border-color:ButtonFace;"><tr style="width:100%">
                             <td colspan="2" style="width:70%; text-align:left;color:#800000;padding-left:5%; background-color: #C0C0C0;">
                     <asp:Label ID="Label41" runat="server" style="font-size:15px;font-weight:bold;font-family:Calibri;  text-decoration: underline;" Text="RECEIPTS DETAILS"></asp:Label></td>
                             <td colspan="1" style="width:18%; text-align:right;color:#800000;padding-left:0%; background-color: #C0C0C0;"><asp:Label ID="Label21" runat="server" style="font-size:15px;font-weight:bold;font-family:Calibri;  text-decoration: underline;" Text="NO.OF RECORDS DISPLAYING : "></asp:Label></td>
                             <td colspan="1" style="width:11%;text-align:center;color:#800000;padding-left:0%; background-color: #C0C0C0;"><asp:Label ID="lbltotalservices" runat="server" style="font-size:15px;font-weight:bold;font-family:Calibri; " Text="0"></asp:Label></td></tr></table></asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>


   

            <table border="1" style="width:90%; margin:auto">

               
            

                  <tr><td colspan="12" style="text-align:left;"><asp:Label ID="Label22" runat="server" Text="View Records :" CssClass="CellFormat"></asp:Label>
    
    <asp:DropDownList ID="ddlViewServiceRecs" runat="server" AutoPostBack="True">
        <asp:ListItem >15</asp:ListItem>
        <asp:ListItem Selected="True">25</asp:ListItem>
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
        <asp:ListItem>200</asp:ListItem>
        
        </asp:DropDownList></td></tr>

                  <tr style="width:95%; padding-left:10%;">
                     <td colspan="10" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
               <asp:UpdatePanel ID="updpnlBillingDetails" runat="server" UpdateMode="Conditional"><ContentTemplate>
             <asp:GridView ID="grvBillingDetails"  runat="server" AllowSorting="True" 
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"   
            ShowFooter="True" Style="text-align: left;  " Width="70%"><Columns>
              <asp:TemplateField HeaderText=""><ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Enabled="true" Height="15px"  Width="20px" Visible="false"  CommandName="CHECK" AutoPostBack="true" OnCheckedChanged="chkSelectGV_CheckedChanged" ></asp:CheckBox></ItemTemplate></asp:TemplateField>                        
           
             <asp:TemplateField HeaderText=" Item Type">

                           <ItemTemplate><asp:DropDownList ID="txtItemTypeGV" runat="server" Font-Size="11px" Height="20px" ReadOnly="true" Width="70px"  AppendDataBoundItems="True" AutoPostBack="True"  onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem Text="ARIN" Value="ARIN" />
                               <asp:ListItem Text="OTHERS" Value="OTHERS" /></asp:DropDownList></ItemTemplate>
            </asp:TemplateField>
                             
             <asp:TemplateField HeaderText=" Reference No."><ItemTemplate><asp:TextBox ID="txtInvoiceNoGV" runat="server" Enabled="true" Height="15px"  Font-Size="12px"  Width="115px"   AutoPostBack="true" OnTextChanged="txtInvoiceNoGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText=" Invoice Date"><ItemTemplate><asp:TextBox ID="txtInvoiceDateGV" runat="server" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="70px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
          
            <asp:TemplateField HeaderText="Account ID"><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" Visible="true" Enabled="false" Height="15px"  Font-Size="12px"  Width="70px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Name"><ItemTemplate><asp:TextBox ID="txtCustomerNameGV" runat="server" Visible="true" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="90px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                 
                    <asp:TemplateField HeaderText="GL Code"><ItemTemplate><asp:TextBox ID="txtOtherCodeGV" runat="server" Visible="True" Enabled="false" Height="15px"   Font-Size="12px" Width="55px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                 <asp:TemplateField HeaderText=""><ItemTemplate><asp:ImageButton ID="BtnGL" runat="server" OnClick="BtnGL_Click" Visible="true" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                        
                    </ItemTemplate></asp:TemplateField>
                
             <asp:TemplateField HeaderText="GL Desc."><ItemTemplate><asp:TextBox ID="txtGLDescriptionGV" runat="server" Visible="True" Enabled="false" Height="15px"   Font-Size="12px" Width="160px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:TextBox ID="txtRemarksGV" runat="server" Visible="true" Enabled="true" Height="15px"  Font-Size="12px"  Width="200px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              
              <asp:TemplateField HeaderText="Invoice Amount" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtTotalPriceWithGSTGV" runat="server" Enabled="false" style="text-align:right" Height="15px"   Font-Size="12px" Width="72px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Total Rcpt Amount" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtTotalReceiptAmtGV" runat="server" Enabled="false" style="text-align:right" Height="15px"   Font-Size="12px" Width="72px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Total CN Amount" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtTotalCreditNoteAmtGV" runat="server" Enabled="false" style="text-align:right" Height="15px"  Font-Size="12px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Receipt Amount" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtReceiptAmtGV" runat="server" Enabled="true" style="text-align:right" Height="15px"  Font-Size="12px" Width="80px" Align="right" AutoPostBack="true" OnTextChanged="txtReceiptAmtGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
             
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtLocationGV" runat="server" Enabled="true" Visible="false" style="text-align:right" Height="15px"  Font-Size="12px" Width="0px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                  <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtSourceRcnoGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
                
                 <asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True">
                     <FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" />

                 </asp:CommandField>
                   <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtServiceNoGV" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtInvoiceTypeGV" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                          

               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtItemCodeGV" runat="server" Visible="false" Height="15px"  Font-Size="12px" Width="0px" AppendDataBoundItems="True" AutoPostBack="False" > </asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtItemDescriptionGV" runat="server" Visible="false" Height="15px"  Font-Size="12px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:DropDownList ID="txtUOMGV" runat="server" Visible="false" Height="20px"   Font-Size="12px" Width="0px" AppendDataBoundItems="True"> <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtQtyGV" runat="server" Visible="false" style="text-align:right" Height="15px"  Width="0px" AutoPostBack="true" OnTextChanged="txtQtyGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
             
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtTaxTypeGV" runat="server" Visible="false" style="text-align:right" Height="20px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtGSTPercGV" runat="server" Visible="false" Enabled="false" style="text-align:right" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                          
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoReceiptGV" runat="server" Visible="false" Height="15px" Width="0px" Text="0"></asp:TextBox></ItemTemplate></asp:TemplateField>                        
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtARCodeGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtGSTCodeGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                  <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtPriceWithDiscGV" runat="server" Text="0.00" Visible="false" Enabled="false" style="text-align:right" Height="15px"  Font-Size="12px" Width="0px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtGSTAmtGV" runat="server" Visible="false" Enabled="false" style="text-align:right" Height="15px"  Font-Size="12px" Width="0px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                
                <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtLocatioIDGV" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtLocationGroupGV" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceDetailGV" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
    
                                
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
                       <table border="0" style="width:90%; margin-left:3%;  border:solid;  " >
                         <tr style="width:100%; padding-left:5%;">
              
                               <td colspan="1"  style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; ">
                                  
                                 </td>
                               <td colspan="1" style="width:120px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   &nbsp;</td>
                              <td colspan="1" style="width:82px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                  <asp:TextBox ID="txtxRow" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="23" Width="19%"></asp:TextBox>
                               </td>
                               <td colspan="1" style="width:160px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                     &nbsp;</td>
                              <td colspan="1" style="width:125px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               <asp:TextBox ID="txtTotalGSTAmt" runat="server" Height="18px" Width="100%" Font-Bold="true" BackColor="#CCCCCC" AutoCompleteType="Disabled" style="text-align:right" BorderStyle="None" Visible="False"></asp:TextBox>
                            
                                 </td>
                              <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>

                               <td colspan="1" style="width:75px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                   <asp:TextBox ID="txtTotalWithGST" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Visible="False" Width="95%"></asp:TextBox>
                               </td>

                              <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                  &nbsp;</td>
                              <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>

                              <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  </td>

                              <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                            Total
                                      </td>


                              <td colspan="1" style="width:95px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                         <asp:TextBox ID="txtReceiptAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="100%"></asp:TextBox>
                           </td>

                      
                      
                        </tr>
                        
                           
                         
                          
                           <tr style="width:100%; padding-left:2%;">
                               <td colspan="1" style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;">&nbsp;</td>
                               <td colspan="1" style="width:120px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="1" style="width:82px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:TextBox ID="txtTotalWithDiscAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Visible="False" Width="100%"></asp:TextBox>
                                   <asp:GridView ID="grvGL" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " GridLines="None" Height="12px" ShowFooter="True" Style="text-align: left" Width="50%">
                                       <Columns>
                                           <asp:TemplateField HeaderText="GL Code">
                                               <ItemTemplate>
                                                   <asp:TextBox ID="txtGLCodeGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" BackColor="#CCCCCC" Font-Size="12px" Height="15px" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged" ReadOnly="true" Width="70px"></asp:TextBox>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Description">
                                               <ItemTemplate>
                                                   <asp:TextBox ID="txtGLDescriptionGV" runat="server" BackColor="#CCCCCC" Font-Size="12px" Height="15px" ReadOnly="true" Visible="true" Width="200px"></asp:TextBox>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Debit Amount">
                                               <ItemTemplate>
                                                   <asp:TextBox ID="txtDebitAmountGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" BackColor="#CCCCCC" Font-Size="12px" Height="15px" onselectedindexchanged="txtItemCodeGV_SelectedIndexChanged" ReadOnly="true" style="text-align:right" Width="70px"></asp:TextBox>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Credit Amount">
                                               <ItemTemplate>
                                                   <asp:TextBox ID="txtCreditAmountGV" runat="server" BackColor="#CCCCCC" Font-Size="12px" Height="15px" ReadOnly="true" style="text-align:right" Width="70px"></asp:TextBox>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True" Visible="false">
                                           <FooterStyle VerticalAlign="Top" />
                                           <ItemStyle Height="15px" />
                                           </asp:CommandField>
                                           <asp:TemplateField>
                                               <FooterStyle HorizontalAlign="Left" />
                                               <FooterTemplate>
                                                   <asp:Button ID="btnAddDetail0" runat="server" Text="Add New Row" ValidationGroup="VGroup" Visible="false" />
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
                               <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="1" style="width:75px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                               <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="1" style="width:95px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                           </tr>
                           <tr style="width:100%; padding-left:2%;">
                               <td colspan="1" style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;"></td>
                               <td colspan="1" style="width:120px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:82px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:TextBox ID="txtOriginalAccountId" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" MaxLength="10" Width="43%"></asp:TextBox>
                               </td>
                               <td colspan="1" style="width:160px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:125px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:75px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>
                               </td>
                               <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                               <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:Button ID="btnSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidationSave();" Text="SAVE" Width="95%" />
                               </td>
                               <td colspan="1" style="width:95px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:Button ID="btnCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="95%" />
                               </td>
                           </tr>
                        
            </table>

         <div style="text-align:center">
       <asp:LinkButton ID="btnTop" runat="server" Font-Names="Calibri" Font-Bold="True" ForeColor="Brown">Go to Top</asp:LinkButton></div>
   </div>
                      </ContentTemplate></asp:UpdatePanel>
               
          </ContentTemplate>
              </asp:UpdatePanel>
         
                             
        
      </ContentTemplate>
             </asp:TabPanel>
                 <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="File Upload" TabIndex="2">
              <HeaderTemplate>
                  <asp:Label ID="lblFileUploadCount" runat="server" Font-Size="11px" Text="File Upload"></asp:Label>

              </HeaderTemplate>
              <ContentTemplate>
                  <table style="width:100%;height:1000px">
                      <tr style="height:30%">
                          <td>
                              <table class="centered" style="text-align:center;width:100%;padding-top:10px;">
                              <tr><td class="CellFormat">Receipt No </td>
                                  <td class="CellTextBox"><asp:Label ID="lblFileUploadRecNo" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label></td>

                              </tr>
                             
                              <tr><td class="CellFormat">Account Name </td>
                                  <td class="CellTextBox"><asp:Label ID="lblFileUploadName" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label>

                                  </td>

                              </tr>
                              <tr><td><asp:TextBox ID="txtDeleteUploadedFile" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
                                  <asp:TextBox ID="txtFileLink" runat="server" AutoCompleteType="Disabled" AutoPostBack="false" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
                                  <br /></td>

                              </tr>
                              <tr><td class="CellFormat">Select File to Upload </td>
                                  <td class="CellTextBox" colspan="1" style="text-align:center">
                                      <asp:FileUpload ID="FileUpload1" runat="server" CssClass="Centered" Width="100%" /></td>

                              </tr>
                              <tr><td class="CellFormat">Description </td>
                                  <td class="CellTextBox" colspan="1" style="text-align:left">
                                      <asp:TextBox ID="txtFileDescription" runat="server" Width="70%"></asp:TextBox></td>

                              </tr>
                              <tr><td class="centered" colspan="2">
                                  <asp:Button ID="btnUpload" runat="server" CssClass="roundbutton1" OnClientClick="currentdatetime()" Text="Upload" /></td>

                              </tr>
                              <tr>
                                  <td><br /></td></tr>
                              <tr><td colspan="2">
                                  <asp:GridView ID="gvUpload" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="false" CssClass="Centered" DataSourceID="SqlDSUpload" EmptyDataText="No files uploaded" Width="90%">
                                      <Columns>
                                          <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                          <asp:BoundField DataField="FileDescription" HeaderText="File Description" />
                                          <asp:BoundField DataField="CreatedOn" DataFormatString="{0:d}" HeaderText="UploadDate" SortExpression="CreatedOn" />
                                          <asp:BoundField DataField="CreatedBy" HeaderText="UploadedBy" SortExpression="CreatedBy" />
                                          <asp:TemplateField>
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="PreviewFile" Text="Preview" />
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                          <asp:TemplateField>
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DownloadFile" Text="Download">
                                                  </asp:LinkButton>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                          <asp:TemplateField>
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DeleteFile" Text="Delete" />
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                      </Columns>
                                      <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                      <HeaderStyle BackColor="#000066" Font-Bold="True" Font-Names="Calibri" ForeColor="White" />
                                      <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                      <RowStyle BackColor="#E4E4E4" Font-Names="Calibri" ForeColor="#8C4510" />
                                      <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                      <sortedascendingcellstyle backcolor="#E4E4E4" />
                                      <sortedascendingheaderstyle backcolor="#000066" />
                                      <sorteddescendingcellstyle backcolor="#E4E4E4" />
                                      <sorteddescendingheaderstyle backcolor="#000066" />

                                  </asp:GridView>
                                  <asp:SqlDataSource ID="SqlDSUpload" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblfileupload where fileref = 'aa'"></asp:SqlDataSource>

                                  </td></tr></table>

                          </td></tr>
                      <tr style="height:800px;width:100%">
                          <td style="height:100%;width:100%"><br />
                              <iframe id="iframeid" runat="server" height="600" style="width:100%;height:100%"></iframe>

                          </td></tr></table>

              </ContentTemplate></asp:TabPanel>
                                  
           </asp:TabContainer>

</td>
                  </tr>
                  </table>

                           
          <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
             <tr style="text-align:center;width:100%">
                <td colspan="8" style="text-align:left;padding-left:5px;">
                   
                       dth:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" ForeColor="#003366"></asp:TextBox></td>
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
                                          <asp:TextBox ID="txtARCode" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                                    </td>
                                  <td style="text-align:left;width:10%">
                                        <asp:TextBox ID="txtARDescription" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                                   </td>
                                  <td style="text-align: left">
                                      <asp:TextBox ID="txtFrom" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>  </td>
                                   <td style="text-align: left">
                                         <asp:TextBox ID="txtRowCount" runat="server" CssClass="dummybutton"></asp:TextBox>
                                  </td>
                            
                              </tr>

    </table>

       
      <div>
       <asp:ModalPopupExtender ID="mdlPopupGL" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlGLClose" PopupControlID="pnlPopUpGL" TargetControlID="btnDummyGL"></asp:ModalPopupExtender>
  <asp:Button ID="btnDummyGL" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />

            <asp:Panel ID="pnlPopUpGL" runat="server" BackColor="White" Width="1000px" Height="600px" BorderColor="#003366" BorderWidth="1px"    HorizontalAlign="Left" ScrollBars="Vertical">

                     <table>
           <tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Select GL Code</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlGLClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                           
         <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopUpGL" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkGL(this, event);" onfocus = "WaterMarkGL(this, event);" AutoPostBack="True">Search Here for GL Code or Description</asp:TextBox>
    <asp:ImageButton ID="btnPopUpGLSearch"  runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" /><asp:ImageButton ID="btnPopUpGLReset"  runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
                               <asp:TextBox ID="txtPopupTeamSearch" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr>


        </table>

     
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="GrdViewGL" runat="server" DataSourceID="SqlDSGL" OnRowDataBound = "OnRowDataBoundgGL" OnSelectedIndexChanged = "OnSelectedIndexChangedgGL" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="800px"  Font-Size="15px" PageSize="8">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="COACode" HeaderText="Code" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="GLType" >
                    <ControlStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
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
            <asp:SqlDataSource ID="SqlDSGL" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
      
              </div>
    </asp:Panel>

         </div>



         </div>
        
         <asp:Panel ID="pnlConfirmPost" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEvent" runat="server" Text="Confirm POST"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to POST the Receipt?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="if(this.value === 'Posting...') { return false; } else { this.value = 'Posting...'; }"/>
                                 <asp:Button ID="btnConfirmNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmPost" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmPost" TargetControlID="btndummyPost" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyPost" runat="server" CssClass="dummybutton" />
  
         <%--start--%>

            <asp:Panel ID="pnlConfirmReverse" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label5" runat="server" Text="Confirm REVERSE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label6" runat="server" Text="Are you sure to REVERSE the Receipt?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesReverse" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="if(this.value === 'Reversing...') { return false; } else { this.value = 'Reversing...'; }"/>
                                 <asp:Button ID="btnConfirmNoReverse" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmReverse" runat="server" CancelControlID="btnConfirmNoReverse" PopupControlID="pnlConfirmReverse" TargetControlID="btndummyReverse" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyReverse" runat="server" CssClass="dummybutton" />
         <%--end--%>


              <%--start--%>

            <asp:Panel ID="pnlConfirmSavePost" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label7" runat="server" Text="Confirm POST/UPDATE "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label8" runat="server" Text="Do you want to POST/UPDATE the Receipt?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesSavePost" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="if(this.value === 'Posting...') { return false; } else { this.value = 'Posting...'; }"/>
                                 <asp:Button ID="btnConfirmNoSavePost" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmSavePost" runat="server" CancelControlID="btnConfirmNoSavePost" PopupControlID="pnlConfirmSavePost" TargetControlID="btndummySavePost" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummySavePost" runat="server" CssClass="dummybutton" />
         <%--end--%>



                <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="41%" Height="41%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
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
                                  <asp:ListItem Value="O">O - Open</asp:ListItem>
                                   <asp:ListItem Value="V">V - Void</asp:ListItem> 
                                    
                               </asp:DropDownList></td>
                           </tr>
                           
                          <tr>
                             <td class="CellFormat">Reason<asp:Label ID="Label64" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                             </td>
                             <td class="CellTextBox" colspan="1">
                                 <asp:TextBox ID="txtReasonChSt" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px" Height="50px" TabIndex="7" TextMode="MultiLine" Width="94%"></asp:TextBox>
                             </td>
                         </tr>

                         <tr>
                             <td colspan="2"><br /></td>
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


                        <%-- Start:View Edit History--%>
              
              
              <asp:Panel ID="pnlViewEditHistory" runat="server" BackColor="White" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  Width="95%" Height="85%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButtonEditHistory" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">View Record History</h4> 
  </td> <td>
                               <asp:TextBox ID="txtbtnEditHistory1" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="lblEditHistory" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewEditHistory" runat="server" DataSourceID="sqlDSViewEditHistory" ForeColor="#333333" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="8" GridLines="None" Width="95%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                    <asp:Button ID="btnEditHistory3" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
              
        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewEditHistory" runat="server" CancelControlID="btnEditHistory3" PopupControlID="pnlViewEditHistory" TargetControlID="btnDummyViewEditHistory" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewEditHistory" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Edit History--%>

<%--         Search Start--%>

         
            

               <asp:Panel ID="pnlSearch" runat="server" BackColor="White" Width="85%" Height="95%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto" HorizontalAlign="Center">
              <br /><br />
                     <table border="0"  style="width:90%; border:thin;   padding-left:3px; margin-left:auto; margin-right:auto;  " >

                            <tr>
                               <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="6">Search</td>
                           </tr>
                    
                          <tr>
                                <td colspan="6" style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;">
                                    <asp:Label ID="lblAlertSearch" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label></td>
                            </tr>
                       <tr>
                               <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Receipt No.&nbsp; From</td>
                              <td colspan="1" style="text-align:left;padding-left:5px; width:10%">   
                                   <asp:TextBox ID="txtSearchInvoiceNo" runat="server" MaxLength="50" Height="16px" Width="92%"></asp:TextBox>
                            </td> 
                           <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To
                       </td>
                    <td  style="text-align:left;width:15%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchInvoiceNoTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       
                     </td>
                                 <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Status</td>
                              <td style="width:20%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">  
                                  <asp:CheckBoxList ID="chkStatusSearch0" runat="server" CellPadding="2" CellSpacing="2" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table" TextAlign="Right">
                                      <asp:ListItem Value="O" Selected="True">O - Open/Pending</asp:ListItem>
                                      <asp:ListItem Value="P" Selected="true">P - Posted</asp:ListItem>
                                      <asp:ListItem Value="V">V - Void</asp:ListItem>
                                  </asp:CheckBoxList>
                            </td>                              
                           </tr>

                          <tr>
                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Receipt Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtInvoiceDateSearchFrom" runat="server" MaxLength="50" Height="16px" Width="92%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateFrom" TargetControlID="txtInvoiceDateSearchFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To
                       </td>
                    <td  style="text-align:left;width:15%; padding-right:8px; ">
                       <asp:TextBox ID="txtInvoiceDateSearchTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateTo" TargetControlID="txtInvoiceDateSearchTo"/>
                     </td>
                           
                         <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                             Bank ID</td>
                                   <td style="width:12%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                    
                                       <asp:DropDownList ID="ddlSearchBankID" runat="server" AppendDataBoundItems="True" Height="20px" Width="91%">
                                           <asp:ListItem>--SELECT--</asp:ListItem>
                                       </asp:DropDownList>
                              </td>
                               </tr>
                         
                         <tr>
                               <td style="width:10%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account Type</td>
                              <td colspan="3" style="text-align:left;padding-left:5px;width:10%;">  
                                    <asp:DropDownList ID="ddlSearchContactType" runat="server" AppendDataBoundItems="true" Height="25px" Width="89%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                        <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                                        <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                                 <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Payment Mode</td>
                              <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                  <asp:DropDownList ID="ddlSearchPaymentMode" runat="server" AppendDataBoundItems="True" Height="20px" Width="91%">
                                      <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                               </td>
                             </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account ID </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:10%">
                                       <asp:TextBox ID="txtSearchAccountID" runat="server" Height="16px" MaxLength="50" Width="88%"></asp:TextBox>
                                       &nbsp;<asp:ImageButton ID="btnClient2" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Cheque No.</td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:TextBox ID="txtSearchChequeNo" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align: left;" Width="91%"></asp:TextBox>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account Name</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:10%">
                                       <asp:TextBox ID="txtSearchClientName" runat="server" Height="16px" MaxLength="100" Width="88%"></asp:TextBox>
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Salesman</td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:DropDownList ID="ddlSearchSalesman" runat="server" AppendDataBoundItems="true" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>
                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Remarks</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:15%">
                                       <asp:TextBox ID="txtSearchComments" runat="server" Height="16px" MaxLength="100" Width="88%"></asp:TextBox>
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contract Group</td>
                                    <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                 
                                        <asp:DropDownList ID="ddlSearchContractGroup" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSContractGroup" DataTextField="UPPER(contractgroup)" DataValueField="UPPER(contractgroup)" Height="25px" Value="-1" Width="91%">
                                            <asp:ListItem Text="--SELECT--" Value="-1" />
                                        </asp:DropDownList>
                                   </td>
                               </tr>

                  

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Last Edit Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchEditEndFrom" runat="server" MaxLength="50" Height="16px" Width="92%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender18" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndFrom" TargetControlID="txtSearchEditEndFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchEditEndTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender19" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndTo" TargetControlID="txtSearchEditEndTo"/>
                     </td>

                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       Receipt Amount</td>
                                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                          <asp:TextBox ID="txtSearchReceiptAmount" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align: left;" Width="91%"></asp:TextBox>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >
                                       Entry Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchEntryDateFrom" runat="server" MaxLength="50" Height="16px" Width="92%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender20" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateFrom" TargetControlID="txtSearchEntryDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; padding-right:25px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchEntryDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender21" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateTo" TargetControlID="txtSearchEntryDateTo"/>
                     </td>

                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       Last Edited By</td>
                                    <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:DropDownList ID="ddlSearchEditedBy" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>

                               <tr>
                                      <td colspan="4" style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <br />
                                   </td>

                                     <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                         Created By</td>
                                       <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                           <asp:DropDownList ID="ddlSearchCreatedBy" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                               <asp:ListItem Text="--SELECT--" Value="-1" />
                                           </asp:DropDownList>
                                      </td>
                               </tr>
                               


                                 <tr >
                                   <td colspan="1" style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Detail Service Location</td>
                                    <td colspan="3" style="text-align:left;padding-left:5px; width:10%">   
                                   <asp:TextBox ID="txtSearchDetailServiceLocation" runat="server" MaxLength="50" Height="16px" Width="88%"></asp:TextBox>
                            </td> 
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="1" style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Detail Invoice No.<br /> </td>
                               <td colspan="3" style="text-align:left;padding-left:5px; width:10%">   
                                   <asp:TextBox ID="txtSearchDetailReference" runat="server" MaxLength="50" Height="16px" Width="88%"></asp:TextBox>
                            </td> 
                                  <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Detail Ledger Code</td>
                                <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlCOACode" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" TabIndex="25" Width="91.5%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                               
                               <tr>
                                   <td>&nbsp;</td>
                                   <td colspan="3" style="text-align:right">
                                       &nbsp;</td>
                                      <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                          &nbsp;</td>
                              <td>
                                  <asp:TextBox ID="txtSearchDetailContractNo" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" Visible="False" Width="20%"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td colspan="3" style="text-align:center">
                                       <asp:Button ID="btnSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Search" Width="100px" />
                                  
                                       <asp:Button ID="btnCloseSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                                   </td>
                                      <td></td>
                              <td></td>
                               </tr>
                             </tr>

        </table>
           </asp:Panel>
    <asp:ModalPopupExtender ID="mdlPopupSearch" runat="server" CancelControlID="btnCloseSearch" PopupControlID="pnlSearch" TargetControlID="btnDummySearch" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
                <asp:Button ID="btnDummySearch" runat="server" cssclass="dummybutton" />

          <%--Search End--%>


             <%-- start--%>

               <asp:Panel ID="pnlPopUpJournalView" runat="server" BackColor="White" Width="1200" Height="600" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">
        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Journal / Contra</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="btnPopUpJournalClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

       
        
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="GrdJournalView" runat="server" DataSourceID="SqlDSJournal" ForeColor="#333333" 
                AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="99%" RowStyle-HorizontalAlign="Left" PageSize="12" Font-Size="14px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="PostStatus" HeaderText="ST" SortExpression="PostStatus">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VoucherNumber" HeaderText="Voucher Number">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="JournalDate" HeaderText="Journal Date" DataFormatString="{0:dd/MM/yyyy}" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DebitBase" HeaderText="Debit" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreditBase" HeaderText="Credit" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LedgerCode" HeaderText="Ledger Code" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
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
            <asp:SqlDataSource ID="SqlDSJournal" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                     </asp:SqlDataSource>
        </div>
    </asp:Panel>
      <asp:ModalPopupExtender ID="mdlPopUpJournalView" runat="server" CancelControlID="btnPopUpJournalClose" PopupControlID="pnlPopUpJournalView"
                                                TargetControlID="btnJournalView" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
          <asp:Button ID="btnJournalView" runat="server" CssClass="dummybutton" />

        <%-- end--%>
         <%--status--%>

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

         
        <%-- status--%>

             <%-- Print Start --%>
         
         
            <asp:Panel ID="pnlConfirmPrint" runat="server" BackColor="White" Width="350px" Height="160px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
           
                 <tr>
                               <td colspan="1" style="text-align:center;"><%--<h4 style="color: #000000">Customer</h4>--%> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPrintClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>

                         </tr>
             
               <tr>
                      <td class="CellFormat" colspan="2" style="text-align:center; margin-left:auto; margin-right:auto;"">
                            
                      </td>
                           </tr>
                           
                            <tr style="padding-top:0px;">
                           

                                    <td colspan="2" style="text-align:LEFT;padding-left:50px">
                                   
                                 <a href="RV_ReceiptConfirmation.aspx?Export=PDF" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp;
                                  <a href="RV_ReceiptConfirmation.aspx?Export=Word" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp; 
                               <a href="Email.aspx?Type=Receipt" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                 &nbsp;&nbsp; <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px; vertical-align: super;">Print Receipt</label><br /><br />

                               </td>
                         </tr> 
                <tr style="padding-top:0px;">
                           

                                   <td colspan="2" style="text-align:LEFT;padding-left:50px">
                                   <div id="temp" runat="server">                                 
                                       <a href="RV_CollectionNote.aspx?Export=PDF" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp;
                                 <%-- <a href="RV_CollectionNote.aspx?Export=Word" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp; --%>
                               <a href="Email.aspx?Type=CollectionNote" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                 &nbsp;&nbsp; <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px; vertical-align: super;">Collection Note</label><br /><br />
</div>

                               </td>
                         </tr> 
             <tr><td colspan="2"><br /></td></tr>                      
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlConfirmMultiPrint" runat="server" CancelControlID="btnPrintClose" PopupControlID="pnlConfirmPrint" TargetControlID="btndummyconfirmMultiPrint" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyconfirmMultiPrint" runat="server" CssClass="dummybutton" />
            
         <%-- Print End --%>

         
                    <%--Confirm delete uploaded file--%>
                                              
                 <asp:Panel ID="pnlConfirmDeleteUploadedFile" runat="server" BackColor="White" Width="500px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label34" runat="server" Text="Confirm DELETE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label35" runat="server" Text="Are you sure to DELETE the File?"></asp:Label>
                        
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
      <asp:ModalPopupExtender ID="mdlPopupDeleteUploadedFile" runat="server" CancelControlID="btnConfirmDeleteNo" PopupControlID="pnlConfirmDeleteUploadedFile" TargetControlID="btndummyDeleteUploadedFile" BackgroundCssClass="modalBackground" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyDeleteUploadedFile" runat="server" CssClass="dummybutton" />

       <asp:TextBox ID="txtFileUpload" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
       

                </ContentTemplate>

             <Triggers>
             <asp:PostBackTrigger ControlID="btnExportToExcel" />
               <asp:PostBackTrigger ControlID="btnPrint" />
               <asp:PostBackTrigger ControlID="tb1$TabPanel3$btnUpload" />
             <asp:PostBackTrigger ControlID="tb1$TabPanel3$gvUpload" />

            </Triggers>
</asp:UpdatePanel>
       
</asp:Content>

