<%@ Page Title="Progress Claim" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProgressClaim.aspx.vb" Inherits="ProgressClaim" Culture="en-GB" EnableEventValidation="false"   %>


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
          /*border-collapse: collapse;  Q1
              border-spacing: 0;
              line-height:10px;*/
    }
  
       
          </style>

         
      <script lang ="javascript" type ="text/javascript" >

          function openInNewTab() {
              window.document.forms[0].target = '_blank';
              setTimeout(function () { window.document.forms[0].target = ''; }, 0);
          }

</script>
    

<script type="text/javascript">
    window.history.forward(1);
 
    var submit = 0;

    function DoValidationSave() {

        var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
        var valid = true;


        //if (++submit > 1) {
        //       alert('Saving the Voucher is in progress.. Please wait.');
        //    submit = 0;
        //    valid = false;
        //    return valid;
        //}


           

        var linvoicedate = document.getElementById("<%=txtCNDate.ClientID%>").value;

         if (linvoicedate == '') {
             document.getElementById("<%=lblAlert.ClientID%>").innerText = "PLEASE ENTER CLAIM DATE";
            ResetScrollPosition();
            document.getElementById("<%=txtCNDate.ClientID%>").focus();
                valid = false;
                return valid;
            }

            var lcompanygroup = document.getElementById("<%=txtCompanyGroup.ClientID%>").value;

         if (lcompanygroup == '--SELECT--') {
             document.getElementById("<%=lblAlert.ClientID%>").innerText = "PLEASE ENTER COMPANY GROUP";
              ResetScrollPosition();
              document.getElementById("<%=txtCompanyGroup.ClientID%>").focus();
              valid = false;
              return valid;
          }

          var lBankGLCode = document.getElementById("<%=txtAccountIdBilling.ClientID%>").value;
         if (lBankGLCode == '') {
             document.getElementById("<%=lblAlert.ClientID%>").innerText = "PLEASE SELECT ACCOUNT ID";
              ResetScrollPosition();
              document.getElementById("<%=txtAccountIdBilling.ClientID%>").focus();
            valid = false;
            return valid;
        }

        currentdatetime();

        return valid;
    };

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

    function RefreshSubmit() {
        submit = 0;
        currentdatetime();
    }

    function checkmultiprint() {
        //alert("1");
        var table = document.getElementById('<%=grdViewMultiPrint.ClientID%>');
        var cnno = "";

        if (table.rows.length > 0) {
            //alert("2");

            var input = table.rows[0].getElementsByTagName("input");

            if (input[0].id.indexOf("chkSelectMultiPrintGV") > -1) {

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


                    document.getElementById("<%=txtCNNos.ClientID%>").value = cnno;
                  }


                  else {
                      //loop the gridview table
                      for (var i = 1; i < table.rows.length; i++) {

                          //get all the input elements
                          var inputs = table.rows[i].getElementsByTagName("input");

                          for (var j = 0; j < inputs.length; j++) {

                              //get the textbox1
                              if (inputs[j].id.indexOf("chkSelectMultiPrintGV") > -1) {
                                  inputs[0].checked = true;
                                  cnno = cnno + "," + parseFloat(inputs[j].value);

                              }

                          }
                      }
                      document.getElementById("<%=txtCNNos.ClientID%>").value = cnno
            }
        }
    }
    }

    function checkallservicerecs() {
        //alert("1");
        var table = document.getElementById('<%=grvServiceRecDetails.ClientID%>');
          var totbillamt = 0;


          if (table.rows.length > 0) {
              //alert("2");

              var input = table.rows[0].getElementsByTagName("input");

              if (input[0].id.indexOf("chkSelectServiceGV") > -1) {

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


                      document.getElementById("<%=txtTotalServiceSelected.ClientID%>").value = "0.00";
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
                document.getElementById("<%=txtTotalServiceSelected.ClientID%>").value = (totbillamt).toFixed(2);
                }
            }
        }

    }


    function checkoneservicerec() {
        //alert("1");
        var table = document.getElementById('<%=grvServiceRecDetails.ClientID%>');
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
                        if (inputs[j].id.indexOf("txtToBillAmtGV") > -1) {

                            totbillamt = totbillamt + parseFloat(inputs[j].value);
                        }
                }

            }

            document.getElementById("<%=txtTotalServiceSelected.ClientID%>").value = (totbillamt).toFixed(2);
        }
    }




  



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
        var lcnno = document.getElementById("<%=txtCNNo.ClientID%>").value;
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

            if (lcnno == '') {
                document.getElementById("<%=txtCNDate.ClientID%>").value = dd + "/" + mm + "/" + y;
                document.getElementById("<%=txtReceiptPeriod.ClientID%>").value = "" + y + mm;
            }
            submit = 0;
        }
    

    function initialize() {
        document.getElementById("<%=txtCNNo.ClientID%>").value = '';
        currentdatetimeinvoice()
        //enable();
    }
     



        function DoValidation(parameter) {
            
            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;
                     
           

         
            

            currentdatetime();

            return valid;
        };

    function DoValidationService(parameter) {

        var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
        var valid = true;

        var lcompanygroup = document.getElementById("<%=ddlCompanyGrp.ClientID%>").value;
            var lcontactType = document.getElementById("<%=ddlContactType.ClientID%>").value;
         var laccountid = document.getElementById("<%=txtAccountId.ClientID%>").value;
         var lclientname = document.getElementById("<%=txtClientName.ClientID%>").value;


         if ((lcompanygroup == "--SELECT--") && (lcontactType == "--SELECT--") && (laccountid == "") && (lclientname == "")) {

             document.getElementById("<%=lblAlert2.ClientID%>").innerText = "Please Enter Company Group/Contact Type/ Account ID / Client Name";
                ResetScrollPosition();
                document.getElementById("<%=txtClientName.ClientID%>").focus();
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
         .auto-style1 {
             height: 34px;
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
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">PROGRESS CLAIM</h3>
          
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
                   <asp:Button ID="btnADD" runat="server" Font-Bold="True" Text="ADD" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="TRUE" OnClientClick="initialize()"  />
                 
                      </td>
                  
                  <td style="width:8%;text-align:center;">
                      <asp:Button ID="btnEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Visible="TRUE" Width="95%" OnClientClick="RefreshSubmit();" />
                    </td>
                     
                  
             
                      

              
                  <td style="width:8%;text-align:center;">
                          <%--<a href="RV_CreditNote.aspx" target="_blank"><button  class="button" style="background-color:#CFC6C0;font-weight:bold;width:95%; font-size:15px;text-align:CENTER;font-family:'CALIBRI';" type="button">PRINT</button></a>--%>
                <asp:Button ID="btnPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="PRINT" Visible="true" Width="95%" />
                    </td>

                  <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnPost" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="POST" Width="95%" OnClientClick="currentdatetime();" />
                     </td>
                  
                    <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnReverse" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="REVERSE" Visible="true" Width="95%" OnClientClick="currentdatetime();" />
              
                    </td>

                    <td style="width:8%;text-align:center;">
                     <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CHANGE STATUS" Width="95%" />
                  
                    </td>

                  
                         <td style="width:8%;text-align:center;">
                                 <asp:Button ID="btnMultiPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="MULTI PRINT" Width="95%" Visible="False" />
                      </td>

          

                   <td style="width:8%;text-align:center;">
                       &nbsp;</td>
                   <td style="width:8%;text-align:center;">
                         &nbsp;</td>

                  
                       <td>

                     
                      <asp:Button ID="btnDelete" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Visible="false" Font-Bold="True" OnClientClick="currentdatetime(); Confirm()" Text="DELETE" Width="95%" />
                  </td>

                     <td style="width:8%;text-align:center;">
                       <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="95%" /></td>


                     <td style="width:8%;text-align:center;">
                         <asp:Button ID="btnCopy" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="COPY" Visible="False" Width="95%" />
                  </td>

                
                  <td style="width:8%;text-align:center;">
                    <asp:Button ID="btnReset" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="RESET" Width="95%" Visible="false" />
                      </td>
                
                   
                   <td style="width:8%;text-align:center;">
                        <asp:Button ID="btnBack" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Height="30px" Text="BACK" Visible="False" Width="95%" />
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
                   
                    <table border="0" style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000;width:100%;padding-left:2px;">
                         <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:7%; ">
                                    Claim No.                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%; ">
                                 <asp:TextBox ID="txtReceiptnoSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="95%" AutoCompleteType="Disabled" AutoPostBack="True" ></asp:TextBox>         
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:7%; ">
                                    Claim Date                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:9%; ">
                                 <asp:TextBox ID="txtInvoiceDateFromSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>         
                                 <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender1" ClearMaskOnLostFocus="false"
                                        MaskType="Date" Mask="99/99/9999" TargetControlID="txtInvoiceDateFromSearch" UserDateFormat="DayMonthYear">
                                </asp:MaskedEditExtender> 
                                     <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDateFromSearch" TargetControlID="txtInvoiceDateFromSearch" Enabled="True" />

                                   </td>

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%; ">
                                    To                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:7%; ">
                                 <asp:TextBox ID="txtInvoiceDateToSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="90%" AutoCompleteType="Disabled" ></asp:TextBox>         
                                 <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender2" ClearMaskOnLostFocus="false"
                                        MaskType="Date" Mask="99/99/9999" TargetControlID="txtInvoiceDateToSearch" UserDateFormat="DayMonthYear">
                                </asp:MaskedEditExtender> 
                                   <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDateToSearch" TargetControlID="txtInvoiceDateToSearch" Enabled="True" />

                                    </td>

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                     <asp:Label ID="lblBranch" runat="server" Text="Branch" CssClass="CellFormat"></asp:Label>
                            </td>
                            <td style="text-align:left;width:7%; ">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" DataTextField="locationID" DataValueField="LocationID" Height="20px" Width="96%" DataSourceID="SqlDSLocation">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>           
                            
                                                      
                            </td>
                            

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:7%;  ">
                                    </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:13%; ">
                                  
                               
                                 <asp:TextBox ID="txtInvoiceNoSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align: left;" Width="80%" Visible="False"></asp:TextBox>
                             </td>

                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%;  ">
                                       <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Search" Width="95%" />
                                 </td>
                        </tr>
                          <tr>

                                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right;width:7%; ">Comp. Group</td>
                              <td>
                                    <asp:DropDownList ID="ddlCompanyGrpSearch" runat="server" AppendDataBoundItems="True" DataTextField="companygroup" DataValueField="companygroup" Height="20px" Width="95%">
                                      <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                                     
                            <asp:ImageButton ID="ImageButton3" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Middle" Visible="False"     />   
                            
                              </td>

                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Account Type</td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right;width:9%; ">
                                 <asp:DropDownList ID="ddlContactTypeSearch" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="95%">
                                   <asp:ListItem>--SELECT--</asp:ListItem>
                                       <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                                     <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                 </asp:DropDownList>
                    
                            </td>
                           
                                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right;width:6%; ">
                                    Account Id
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:9%; ">
                                   <asp:TextBox ID="txtAccountIdSearch" runat="server" MaxLength="50" Height="16px" Width="70%" AutoCompleteType="Disabled" style="text-align:left; " AutoPostBack="True" ></asp:TextBox>         
                                   
                            <asp:ImageButton ID="btnClientSearch" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Middle"     />                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                                                 
                            
                                                      
                            </td>

                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right;width:7%;  ">
                                    Client Name
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:12%; ">
                                 <asp:TextBox ID="txtClientNameSearch" runat="server" MaxLength="50" Height="16px" Width="95%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;"></asp:TextBox>         
                    
                            </td>


                          
                            


                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right;width:7%;  ">Post Status </td>
                             <td>
                                  <asp:TextBox ID="txtSearch1Status" runat="server" Width="75%" ReadOnly="FALSE"></asp:TextBox>
                                 <asp:ImageButton ID="btnSearch1Status" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" ImageAlign="Middle"
                                    Height="22px" Width="24px" />                       
                            </td>
                            



                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%;  ">
                                       <asp:Button ID="btnQuickReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Reset" Width="95%" OnClientClick="ClearSearch()" />
                                   </td>
                        </tr>
                    </table>
                      </td>
                    <td style="text-align:right;width:45%;display:inline;vertical-align:middle;padding-top:10px;">
                 
                </td>
            </tr>
        </table>


     


         <table   style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%;  " >
            <tr>
                <td style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td></tr>
             
                            <tr style="text-align:center;">
                                  <td colspan="11" style="width:100%;text-align:center">
                                      
           <%-- <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="None"    Visible="true" Width="100%">--%>
            <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1330px">
          


                                          <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server" OnRowDataBound = "OnRowDataBoundg1" OnSelectedIndexChanged = "OnSelectedIndexChangedg1"  AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSCN" ForeColor="#333333" GridLines="Vertical"> 
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
                                               
                                                    <asp:BoundField DataField="ClaimNumber" HeaderText="Claim No." />
                                                    <asp:BoundField DataField="ProjectCode" HeaderText="Project Code" SortExpression="ProjectCode" >
                                               
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle Width="8%" />
                                                    </asp:BoundField>
                                               
                                                  <asp:BoundField DataField="ClaimID" HeaderText="Claim ID" SortExpression="ClaimID" >
                                                      <ControlStyle Width="6%" />
                                                    <ItemStyle Width="6%" Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ClaimDate" HeaderText="Claim Date" SortExpression="ClaimDate" DataFormatString="{0:dd/MM/yyyy}">
                                                  <ItemStyle Width="5%" />
                                                  </asp:BoundField>
                                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" SortExpression="ContactType" />
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup" />
                                                  <asp:BoundField DataField="CustName" HeaderText="Client Name" SortExpression="CustName" ItemStyle-Wrap="false">
                                                    <ControlStyle Width="25%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="25%" />
                                                  </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                                                  <asp:BoundField DataField="CreatedOn" HeaderText="Created On" SortExpression="CreatedOn" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="Last Modified By" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" HeaderText="Last Modified On" SortExpression="LastModifiedOn" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
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
             
                 <tr>
                 <td>
                      <asp:SqlDataSource ID="SQLDSCN" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                </asp:SqlDataSource>
                     </td><td>
              <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
                           </td><td>
                         &nbsp;</td><td>
               <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT locationgroup FROM tbllocationgroup order by locationgroup">
                       
            </asp:SqlDataSource>
                   </td><td>
              <asp:SqlDataSource ID="SqlDSBankCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(COACode, ' - ', Description) AS codedes FROM tblchartofaccounts WHERE (GLType = 'BANK') ORDER BY COACode"></asp:SqlDataSource>
           </td>
                     <td>
                         &nbsp;</td> 

                 <td>
                       <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                 </td>
                          <td>
                              <asp:SqlDataSource ID="SqlDSServiceFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                     </td> 
                 <td>
                     &nbsp;</td>     
                <td>
                    <asp:SqlDataSource ID="SqlDSBillingFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                     </td>
                     <td>
                         <asp:SqlDataSource ID="SqlDSTerms" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                     </td>

                     <td>
                          <asp:SqlDataSource ID="SqlDSLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="select locationid from tbllocation">
                       
            </asp:SqlDataSource>
                         <asp:SqlDataSource ID="SqlDSSalesMan" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

                     </td>

                     
                            <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                <asp:SqlDataSource ID="SqlDSSalesDetail" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Rcno, ContractType, ClaimID, ContractNo, ContractAmount, CompletedValue, BalanceValue, PreviousClaimedAmount, CurrentClaimAmount, RetentionPerc, RetentionAmount, TotalCurrentClaim, BilledAmount, BalanceNotClaimed from tblprogressclaimdetail where ClaimID= @InvoiceNumber">
                         <SelectParameters>
                             <asp:ControlParameter ControlID="txtCNNo" Name="@InvoiceNumber" PropertyName="Text" />
                         </SelectParameters>
                     </asp:SqlDataSource>

                            </td>

                    
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
                                        <asp:TextBox ID="txtClientFrom" runat="server" Visible="False"></asp:TextBox>
                                         <asp:TextBox ID="txtCondition" runat="server" Height="16px" MaxLength="50" Visible="false" Width="550px"></asp:TextBox>
                                      <asp:TextBox ID="txtOrderBy" runat="server" Height="16px" MaxLength="50" Visible="false" Width="550px"></asp:TextBox>
                                         <asp:TextBox ID="txtInvoiceSearch" runat="server" Height="16px" MaxLength="50" Visible="false" Width="550px"></asp:TextBox>
                                      <asp:TextBox ID="txtCustomerSearch" runat="server" Height="16px" MaxLength="50" Visible="false" Width="550px"></asp:TextBox>
                                      <asp:TextBox ID="txtImportService" runat="server" Height="16px" MaxLength="50" Visible="false" Width="550px"></asp:TextBox>
                                      <asp:TextBox ID="txtFrom" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="10%"></asp:TextBox>
                                    <asp:TextBox ID="txtRcnoSelected" runat="server" Height="16px" MaxLength="50" Visible="false" Width="50px"></asp:TextBox>
                                  <asp:TextBox ID="txtLimit" runat="server" Height="16px" MaxLength="50" Visible="false" Width="550px"></asp:TextBox>
                              <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>
                               <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox> 
                             <asp:TextBox ID="txtPostUponSave" runat="server" Visible="False"></asp:TextBox> 
                                <asp:TextBox ID="txtOnlyEditableByCreatorCN" runat="server" Visible="False"></asp:TextBox>
                                   <asp:TextBox ID="txtOnlyEditableByCreatorDN" runat="server" Visible="False"></asp:TextBox>
                                 <asp:TextBox ID="txtRecordCreatedBy" runat="server" Visible="False"></asp:TextBox>  
                                <asp:TextBox ID="txtDefaultTaxCode" runat="server" Visible="False"></asp:TextBox> 
                                         <asp:TextBox ID="txtLogDocNo" runat="server" Visible="False"></asp:TextBox> 
                              </tr>
       
             </table>
      

              <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                          
               <tr style="width:100%">
                 <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; color:#800000; width:80% ">&nbsp;</td>

                     <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline; color:#800000; width:20% ">
                         <asp:TextBox ID="txtTotalInvoiceAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="16px" TabIndex="20" Width="50%" style="text-align:right;" Font-Bold="true" Visible="False" ></asp:TextBox>
             
                              </td>
           </tr>
               </table>

        </ContentTemplate>
              </asp:UpdatePanel>

         


        
          <asp:UpdatePanel runat="server" ID="updPnlBillingRecs" UpdateMode="Conditional">
                  <ContentTemplate>
                      
       
          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                           
          
               <tr style="width:100%">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label47" runat="server" Text="PROGRESS CLAIM INFORMATION"></asp:Label>
                 </td>
           </tr>
         
                <tr >
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                               <asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label>
                           </td>
                           
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:0%; width:20%; text-align:left; ">
                                
                                   <asp:TextBox ID="txtLocation" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                                
                          </td>
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
                           </tr>
                     <tr>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                                &nbsp;</td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Status</td>
                            
                          <td style="width:40%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:TextBox ID="txtPostStatus" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="16px" Width="80%"></asp:TextBox>

                          </td>
                          <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                              &nbsp;</td>
                        </tr>
              
               <tr style="display:none">
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Voucher Type<asp:Label ID="Label46" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:40%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:DropDownList ID="ddlDocType" runat="server" AutoPostBack="True" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="80%">
                           <asp:ListItem>--SELECT--</asp:ListItem>
                           <asp:ListItem Value="ARCN">CREDIT NOTE</asp:ListItem>
                           <asp:ListItem Value="ARDN">DEBIT NOTE</asp:ListItem>
                       </asp:DropDownList>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>
              
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       &nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Project Code</td>
                   <td style="width:40%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                   <asp:TextBox ID="txtProjectCode" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="21" AutoPostBack="False" ></asp:TextBox>    
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>
              
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtRcnoServiceRecord" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="1%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Claim ID<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:40%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtCNNo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
               </tr>
              
               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Claim No.</td>
                            
                          <td style="width:40%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:TextBox ID="txtClaimNo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="80%"></asp:TextBox>
                       </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                </tr>

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtRcnoServiceRecordDetail" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="101" Width="1%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Claim Date<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:40%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtCNDate" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" TabIndex="21" Width="80%"></asp:TextBox>
                       <asp:CalendarExtender ID="calConDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtReceiptDate" TargetControlID="txtCNDate" />
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
               </tr>

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Period</td>
                   <td style="width:40%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtReceiptPeriod" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Width="81%" TabIndex="109" BorderStyle="None"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

              

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Account Type</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="80.5%" TabIndex="20">
                        <asp:ListItem>--SELECT--</asp:ListItem>
                              <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
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
                               <asp:TextBox ID="txtAccountIdBilling" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="21" AutoPostBack="True" ></asp:TextBox>
                              
             &nbsp;<asp:ImageButton ID="btnClient" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" OnClientClick="ConfirmSelection()" Width="24px" />
   
                 
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
                            
                          <td style="width:20%;font-size:14px;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtAccountName" runat="server" Height="16px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="113" BorderStyle="None" ></asp:TextBox>
                       <asp:ImageButton ID="btnEditBillingName" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
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
            <asp:BoundField DataField="BillStreetSvc" HeaderText="Street">
            <ControlStyle  />
            <HeaderStyle  />
            <ItemStyle  Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillBuildingSvc" HeaderText="Building">
            <ControlStyle  />
            <HeaderStyle  />
            <ItemStyle  Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillCitySvc" HeaderText="City">
            <ControlStyle  />
            <HeaderStyle  />
            <ItemStyle  Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillStateSvc" HeaderText="State">
            <ControlStyle  />
            <HeaderStyle  />
            <ItemStyle  />
            </asp:BoundField>
            <asp:BoundField DataField="BillCountrySvc" HeaderText="Country">
            <ControlStyle  />
            <HeaderStyle  />
            <ItemStyle  Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillPostalSvc" HeaderText="Postal">
            <ControlStyle  />
            <HeaderStyle  />
            <ItemStyle  Wrap="False" />
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
                       
                        </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                                     <asp:TextBox ID="txtRecordNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="107"></asp:TextBox>
                 </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Company Group<asp:Label ID="Label59" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:TextBox ID="txtCompanyGroup" runat="server" AppendDataBoundItems="True" Height="16px" Width="81%" BackColor="#CCCCCC" BorderStyle="None"></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                                        <asp:Button ID="btnDummyClient" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                          </td>
                        </tr>


             <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Contact Person</td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:TextBox ID="txtContactPerson" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="110" Width="80%"></asp:TextBox>
                              
                              <asp:ImageButton ID="btnEditBillingDetails" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           &nbsp;</td>
                        </tr>

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Billing Address </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBillAddress" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" TabIndex="114" Width="81%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
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
                      &nbsp;</td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">City</td>
                  <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                      <asp:TextBox ID="txtBillCity" runat="server" BackColor="#CCCCCC" BorderStyle="None" Height="18px" MaxLength="100" TabIndex="116" Width="81%"></asp:TextBox>
                  </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
              </tr>

               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">State</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBillState" runat="server" BackColor="#CCCCCC" BorderStyle="None" Height="18px" MaxLength="100" TabIndex="116" Width="81%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtRcnoServiceBillingDetail" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="105" Width="1%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Country</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBillCountry" runat="server" BackColor="#CCCCCC" BorderStyle="None" Height="18px" MaxLength="100" TabIndex="117" Width="81%"></asp:TextBox>
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

               <tr style="display:none">
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="TextBox2" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="107"></asp:TextBox>
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Our Reference
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtOurReference" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="22" MaxLength="25" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

               <tr style="display:none">
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                           <asp:TextBox ID="TextBox5" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" TabIndex="131" ForeColor="White"></asp:TextBox>
                       </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Your Reference
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtYourReference" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="23" MaxLength="25" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                        </tr>

              
                      
                       <tr>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top">Claim Title</td>
                           <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                               <asp:TextBox ID="txtComments" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px" Height="80px" TabIndex="28" TextMode="MultiLine" Width="80%"></asp:TextBox>
                           </td>
                           <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>

              
                      
                      
                        
                          
                           <tr>
                               <td colspan="3" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:Button ID="btnShowServices" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  TabIndex="29" Text="IMPORT SERVICES" Width="20%" />
                                   <asp:Button ID="btnShowInvoices" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  TabIndex="28" Text="SHOW INVOICES" Width="10%" Visible="False" />
                                       <asp:Button ID="btnDeleteAll" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Visible="false" Text="DELETE ALL" Width="50%" TabIndex="31" />
               
                                    </td>
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                                   <asp:TextBox ID="txtTaxRatePct" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="35%"></asp:TextBox>
                               </td>
                           </tr>
                       </tr>
                        </tr>

              </table>

                       <%--  start--%>

                      <div>
   
                             

</div>
      
                      <%--end Invoice--%>


           <%--start Service--%>


         <div>
          <asp:ModalPopupExtender ID="mdlImportServices" runat="server" BackgroundCssClass="modalBackground" CancelControlID="" PopupControlID="pnlImportServices" TargetControlID="btnDummyImportService">
            </asp:ModalPopupExtender> 
  
   <asp:Button ID="btnDummyImportService" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" /> 
      
             <asp:Panel ID="pnlImportServices" runat="server" BackColor="White" Width="95%" Height="650px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
                     

               <%--  aaa--%>

                   <table border="0"  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; border:solid; border-color:ButtonFace; "  >

                 <tr style="width:100%">
                 <td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;"> SEARCH SERVICE RECORDS 
                     
                     </td>
                  </tr>
             
                    <tr>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                <asp:SqlDataSource ID="SqlDSUnitMS" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UnitMS FROM tblunitms order by UnitMS"></asp:SqlDataSource>
                            </td>

                          <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                              <asp:TextBox ID="txtGLFrom" runat="server" AutoCompleteType="Disabled" Height="16px" Width="15%" Visible="False"></asp:TextBox>
                            </td>
                          <td colspan="4"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert2" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td>
                                        
                     </tr>
            
                       <tr>
                           <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                 <asp:SqlDataSource ID="SqlDSBillingCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Description FROM tblbillingproducts order by Description "></asp:SqlDataSource>
                      
                           </td>
                           <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                           </td>
                           <td colspan="4" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                               <asp:RadioButton ID="RadioButton1" runat="server" GroupName="ServiceStatus" Height="20px" Text="Completed" Visible="False" />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                               <asp:RadioButton ID="RadioButton2" runat="server" GroupName="ServiceStatus" Height="20px" Text="Not Completed" Visible="False" />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                               <asp:RadioButton ID="RadioButton3" runat="server" GroupName="ServiceStatus" Height="20px" Text="All" Visible="False" />
                           </td>
                       </tr>
            
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
                                         DataTextField="companygroup" DataValueField="companygroup" Value="-1"  AppendDataBoundItems="True" CssClass="chzn-select">
                                 <asp:ListItem Text="--SELECT--" Value="-1" />   
                                     </asp:DropDownList>
                                 
                            </td>
                       
                           <td style="width:9%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                                  <asp:DropDownList ID="ddlContactTypeIS" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="95%">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                       <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                                      <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                  </asp:DropDownList>
                        
                               
                                  </td>

                           <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtAccountId" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" AutoPostBack="True" ></asp:TextBox>
                              <asp:ImageButton runat="server"  ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" CssClass="righttextbox" Height="22px" Width="24px" ID="ImageButton2"></asp:ImageButton>
                                  <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient" BackgroundCssClass="modalBackground" Enabled="True"  ></asp:ModalPopupExtender> 
                                  </td>

                          <td colspan="2" style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtClientName" runat="server" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>
                            </td>

                           <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">         
                                
                              <asp:TextBox ID="txtLocationIDIS" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" ></asp:TextBox>
                               <asp:ImageButton ID="ImageButton4"  runat="server"  ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" CssClass="righttextbox" Height="22px" Width="24px" ></asp:ImageButton>        
                       
                            </td>

                        
                        </tr>

                        </td>
                              <tr>
                                  <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Contract No</td>
                                  <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Service Frequency</td>
                                  <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Service From</td>
                                  <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Service To</td>
                                  <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Billing Frequency</td>
                                  <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Contract Group</td>
                            </tr>
                            <tr>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    &nbsp;<asp:TextBox ID="ddlContractNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%"></asp:TextBox>
                                    &nbsp;<asp:ImageButton ID="ImageButton8" runat="server" CssClass="righttextbox" Height="22px" i="" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlServiceFrequency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Frequency" DataValueField="Frequency" Height="25px" Value="-1" Width="95%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" Height="16px" Width="90%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom" />
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtDateTo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="90%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtDateTo" TargetControlID="txtDateTo" />
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlBillingFrequency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Frequency" DataValueField="Frequency" Height="25px" Value="-1" Width="90%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlContractGroupIS" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="UPPER(contractgroup)" DataValueField="UPPER(contractgroup)" Height="25px" Value="-1" Width="95%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                 <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    Invoice Number</td>
                                <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    &nbsp;</td>
                                <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    &nbsp;</td>
                                <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>
                            </tr>
                        <tr>
                            <td colspan="1" style="width:10%;text-align:left;color:brown;font-size:14px;font-weight:bold;font-family:'Calibri';">
                                <asp:TextBox ID="txtInvoiceNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%"></asp:TextBox>
                            </td>
                            <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                <asp:Button ID="Button2" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                <asp:Button ID="Button3" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                Default GL Code</td>
                            <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                <asp:TextBox ID="txtGLCodeIS" runat="server" AutoCompleteType="Disabled" Enabled="False" Height="16px" Width="80%"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton7" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                            </td>
                            <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                <asp:TextBox ID="txtLedgerNameIS" runat="server" AutoCompleteType="Disabled" Enabled="False" Height="16px" Width="44%"></asp:TextBox>
                                <asp:Button ID="Button4" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                <asp:TextBox ID="TextBox6" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="35%"></asp:TextBox>
                            </td>
                            <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                <asp:Button ID="btnShowRecords" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SHOW RECORDS" Width="80%" />
                            </td>
                       </tr>
                        </tr>

            </table>
    
        

             <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="99%" Height="350px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
       
         
         <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label6" runat="server" Text="SERVICE BILLING DETAILS"></asp:Label>
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
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="chkSelectServiceGV" runat="server" AutoPostBack="false" TextAlign="Right" onchange="checkallservicerecs()" Width="5%" ></asp:CheckBox></HeaderTemplate>
               <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" onchange="checkoneservicerec()"
 CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
            
                        <asp:TemplateField HeaderText="Service Record"><ItemTemplate><asp:TextBox ID="txtServiceRecordNoGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("RecordNo")%>' BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="123px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                       <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("ContractNo")%>' BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="155px"></asp:TextBox></ItemTemplate></asp:TemplateField>                             
                        <asp:TemplateField HeaderText="Customer Name"><ItemTemplate><asp:TextBox ID="txtAccountNameGV" runat="server" Visible="true" Height="15px" Text='<%# Bind("CustName")%>' Width="250px" Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None"  ></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                      <asp:TemplateField HeaderText="Start Date"><ItemTemplate><asp:TextBox ID="txtStartDateGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy}")%>' BackColor="#CCCCCC" BorderStyle="None"  Height="18px" Width="72px"  style="text-align:center" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="End Date"><ItemTemplate><asp:TextBox ID="txtEndDateGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("EndDate", "{0:dd/MM/yyyy}")%>' BackColor="#CCCCCC" BorderStyle="None"  Height="18px" Width="72px"  style="text-align:center" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Service Address" ><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("Address1")%>' BackColor="#CCCCCC" BorderStyle="None" style="text-align:left" Height="18px" Width="350px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField HeaderText="Contract Amt." HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtContractAmountGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("AgreeValue")%>' BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="75px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField Headertext="Bill Amt." HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtBillAmountGV" runat="server" Font-Size="12px" ReadOnly="true" Text='<%# Bind("BillAmount")%>' BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="60px" style="text-align:right" ></asp:TextBox></ItemTemplate></asp:TemplateField>                             
                      <asp:TemplateField ><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Font-Size="12px" ReadOnly="true"  Text='<%# Bind("LocationID")%>' BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="20px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                      <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordGV" runat="server" Visible="false" Height="15px" Width="0px" Text='<%# Bind("rcno")%>'></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                
               </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
            </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvServiceRecDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr>
             

             
              <tr>
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="btnSelect" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                  
            </tr>

               
            
        </table>


                 </asp:Panel>


                   <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                        <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="Button5" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                         
                    <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right">
                            Total
                       </td>

                   <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left">
                   <asp:TextBox ID="txtTotalServiceSelected" runat="server" style="text-align:right" AutoCompleteType="Disabled"  ForeColor="Black" Height="16px" Width="35%" BackColor="#CCCCCC"></asp:TextBox>
                             
                       </td>
                 
            </tr>
                    </table>
  <table border="1" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                      
                         
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                     
                      <asp:SqlDataSource ID="SqlDSServices" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="Select A.Rcno,  A.RecordNo, A.AccountID, A.ContractNo, A.CustName,   A.BillNo,  A.CompanyGroup, A.LocationID , A.BillAmount
 FROM tblServiceRecord A  where 1 = 1  ">
                            </asp:SqlDataSource>
                        <asp:Button ID="btnImportService" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="IMPORT SERVICES" Width="15%" Visible="True" />
                       &nbsp;  
                           <asp:Button ID="Button6" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CANCEL" Width="15%" Visible="True" />
                    
                      
                        </td>
                 
            </tr>
            </table>

                <%-- bbb--%>

                 </asp:Panel>

          </div>

                      <%--end Service--%>
     
               
                      <br />
                      <br />
  
                      <table border="1" style="width:100%; margin:auto">

               
               <tr style="width:100%;">
                 <td colspan="20" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">
                    
                     <asp:Label ID="Label5" runat="server" Text="PROGRESS CLAIM DETAILS"></asp:Label>
                 </td>

           </tr>
    </table>


                                         <%--start--%>
     <table border="1" style= "width:90%; margin:auto">
                     
                  <tr style="width:100%">
                     <td colspan="20" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
               <asp:UpdatePanel ID="updpnlBillingDetailsNew" runat="server" UpdateMode="Conditional"><ContentTemplate>
             <asp:GridView ID="grvBillingDetailsNew"  runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" CellPadding="1"  Height="12px" Font-Size="13px" Style="text-align: left" Width="90%" DataSourceID="SqlDSSalesDetail" GridLines="Horizontal" BorderStyle="None">
                 <Columns>
                                     
                                             
                   <asp:TemplateField HeaderText="Type"><ItemTemplate><asp:TextBox ID="txtContractTypeGVB" runat="server"  Text='<%# Bind("ContractType")%>' Font-Size="11px" Height="15px" ReadOnly="true" Width="140px" Enabled="False" AppendDataBoundItems="True" AutoPostBack="True"  ></asp:TextBox></ItemTemplate></asp:TemplateField>
              

                     <asp:TemplateField HeaderText="Contract No." HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtContractNoGVB" runat="server" Text='<%# Bind("ContractNo")%>'  Font-Size="11px" style="text-align:left" Height="15px" Width="135px" Align="left" Enabled="False" AutoPostBack="false" AppendDataBoundItems="True"    ></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="Contract Amount" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtContractAmountGVB" runat="server" Text='<%# Bind("ContractAmount")%>' Height="15px" Width="80px" Font-Size="11px" Enabled="False" style="text-align:right"> </asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                  
                     </asp:TemplateField>


                       <asp:TemplateField HeaderText="Completed Value" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtCompletedValueGVB" runat="server" Text='<%# Bind("CompletedValue")%>' Font-Size="11px" Enabled="False" OnTextChanged="txtInvoiceNoGVB_TextChanged" style="text-align:right" Height="15px" Width="80px" Align="right"  AutoPostBack="True" AppendDataBoundItems="True" ></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                                  
               
                <asp:TemplateField HeaderText="Balance Value"   ><ItemTemplate><asp:TextBox ID="txtBalanceValueGVB" runat="server" Text='<%# Bind("BalanceValue")%>' Font-Size="11px" Visible="true" Enabled="false" Height="15px" Width="80px" style="text-align:right"> </asp:TextBox> </ItemTemplate> </asp:TemplateField>
               
                   <asp:TemplateField HeaderText="Previous Claim Amount"><ItemTemplate><asp:TextBox ID="txtPreviousClaimedAmountGVB" runat="server" Text='<%# Bind("PreviousClaimedAmount")%>'  Font-Size="11px" Height="15px" Enabled="False" Width="80px" style="text-align:right"></asp:TextBox></ItemTemplate></asp:TemplateField>
         
                <asp:TemplateField HeaderText="Current Claim Amount"><ItemTemplate><asp:TextBox ID="txtCurrentClaimedAmountGVB" runat="server" Text='<%# Bind("CurrentClaimAmount")%>' Font-Size="11px" Height="15px" Enabled="False" Width="80px" style="text-align:right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Retention %"><ItemTemplate><asp:TextBox ID="txtRetentionPercGVB" runat="server" Text='<%# Bind("RetentionPerc", "{0:n2}")%>'  Font-Size="11px"  style="text-align:right" Height="15px"  Width="80px" AutoPostBack="true" OnTextChanged="txtQtyGVB_TextChanged" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Retention Amount"><ItemTemplate><asp:TextBox ID="txtRetentionAmountGVB" runat="server" Text='<%# Bind("RetentionAmount", "{0:n2}")%>'   Font-Size="11px"  style="text-align:right" Height="15px"  Width="80px" AutoPostBack="true" OnTextChanged="txtPricePerUOMGVB_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Total Current Claim"  HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtTotalCurrentClaimGVB" runat="server" Text='<%# Bind("TotalCurrentClaim")%>' Font-Size="11px"   Enabled="false" style="text-align:right" Height="15px" Width="80px" Align="right"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                     </asp:TemplateField>
                <asp:TemplateField HeaderText="Billed Amount"><ItemTemplate><asp:TextBox ID="txtBilledAmountGVB" runat="server"  Text='<%# Bind("BilledAmount")%>' DataSourceID="SQLDSTaxType" DataTextField="TaxType" Font-Size="11px" style="text-align:right" Height="15px" Width="80px" AutoPostBack="False" Enabled="false"  ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Balance Not Claimed" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtBalanceNotClaimedGVB" runat="server" Text='<%# Bind("BalanceNotClaimed", "{0:n2}")%>'  Font-Size="11px" Enabled="false" style="text-align:right" Height="15px" Width="80px"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                               
                                
                        <asp:TemplateField ><ItemTemplate> <asp:ImageButton ID="btnDeleteDetail" runat="server" CssClass="righttextbox" Height="18px" ImageAlign="Top" ImageUrl="~/Images/delete_icon_color.gif" Width="20px" OnClick="btnDeleteDetail_Click" OnClientClick="Confirm()"  />
              </ItemTemplate></asp:TemplateField>
                      
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGVB" runat="server" Text='<%# Bind("Rcno")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                     
                         
                     
                          </Columns>

        
                                              <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/>
                                              <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                              <RowStyle BackColor="#E4E4E4" ForeColor="#666666" Font-Names="Calibri"/>
                                              <SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" />




             </asp:GridView>
                   </ContentTemplate>
                   </asp:UpdatePanel>
            </td></tr>

                            </table>
                      
                      <%--end--%>

    <table border="0" style="width:90%; margin:auto">

                  <tr style="width:100%">
                     <td colspan="20" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
               <asp:UpdatePanel ID="updpnlBillingDetails" runat="server" UpdateMode="Conditional"><ContentTemplate>
             <asp:GridView ID="grvBillingDetails"  runat="server" AllowSorting="True" 
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"   Font-Size="13px"
            ShowFooter="True" Style="text-align: left;  " Width="70%">
                 
                   <Columns>
                                            
              
            <asp:TemplateField HeaderText=" Type"><ItemTemplate><asp:DropDownList ID="txtContractTypeGV" runat="server" Font-Size="11px" Height="20px" ReadOnly="true" Width="150px"  AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem Text="ORIGINAL CONTRACT" Value="ORIGINAL CONTRACT" /><asp:ListItem Text="VARIATION CONTRACT" Value="VARIATION CONTRACT" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
               

                              
               <asp:TemplateField HeaderText="Contract No." HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Height="15px" Width="135px" Visible="true" Enabled="false" Font-Size="11px" > </asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>

             
                <asp:TemplateField HeaderText="Contract Amount"   ><ItemTemplate><asp:TextBox ID="txtContractAmountGV" runat="server" Font-Size="11px" Visible="true" Enabled="false" Height="15px" style="text-align:right" Width="80px"> </asp:TextBox> </ItemTemplate> </asp:TemplateField>
              
                <asp:TemplateField HeaderText="Completed Value"><ItemTemplate><asp:TextBox ID="txtCompletedValueGV" runat="server" Font-Size="11px" Height="15px" Enabled="false" style="text-align:right" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Balance Value"  HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtBalanceValueGV" runat="server" Font-Size="11px" Height="15px" style="text-align:right" Width="80px" Text="0.00" AppendDataBoundItems="True" Enabled="false"> </asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                <asp:TemplateField HeaderText="Previous Claim Amount"><ItemTemplate><asp:TextBox ID="txtPreviousClaimedAmountGV" runat="server" Font-Size="11px" Text="0.00" style="text-align:right" Enabled="false" Height="15px"  Width="80px" AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Current Claim Amount"><ItemTemplate><asp:TextBox ID="txtCurrentClaimedAmountGV" runat="server" Font-Size="11px" Text="0.00" style="text-align:right" Enabled="false" Height="15px"  Width="80px" AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Retention %"  HeaderStyle-HorizontalAlign="center"><ItemTemplate><asp:TextBox ID="txtRetentionPercGV" runat="server" Font-Size="11px" Text="0.00"  Enabled="true" style="text-align:right" Height="15px" Width="80px" Align="right"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                     </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="Retention Amount"><ItemTemplate><asp:TextBox ID="txtRetentionAmountGV" runat="server" Font-Size="11px" style="text-align:right" Height="15px" Width="80px" Text="0.00" AutoPostBack="True" Enabled="true" OnTextChanged="txtRetentionAmountGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Total Current Claim" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtTotalCurrentClaimGV" runat="server" Text="0.00"  Font-Size="11px" Enabled="false" style="text-align:right" Height="15px" Width="80px"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                <asp:TemplateField HeaderText="Billed Amount"><ItemTemplate><asp:TextBox ID="txtBilledAmountGV" runat="server" Font-Size="11px" Enabled="false" Text="0.00" style="text-align:right" Height="15px" Width="80px" Align="right" AutoPostBack="true" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Balance Not Claimed" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtBalanceNotClaimedGV" runat="server" Text="0.00" Font-Size="11px" Enabled="false" style="text-align:right" Height="15px" Width="80px" Align="right"></asp:TextBox></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                     </asp:TemplateField>
              

              
          
                  <asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True"   >
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
                      <table border="0" style="width:95%; padding-left:6%;">
                         <tr style="width:100%; padding-left:1%;">
                         
                               <td colspan="1"  style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; ">
                                  
                                 </td>
                               <td colspan="1" style="width:115px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   &nbsp;</td>
                               <td colspan="1" style="width:82px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                 </td>
                              <td colspan="1" style="width:120px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                            
                                 </td>
                              <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                            
                                 </td>

                               <td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                   <asp:TextBox ID="txtxRow" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="50" Width="19%"></asp:TextBox>
                             
                                  </td>

                              <td colspan="1" style="width:75px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                  &nbsp;</td>
                              <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                      <asp:TextBox ID="txtTotalCNAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="90%" Visible="False"></asp:TextBox>
                           
                                  </td>
                              <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                              <asp:TextBox ID="txtTotalCNGSTAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Width="90%" Visible="False"></asp:TextBox>
                          
                                  </td>

                              <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>
                              <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                          <asp:TextBox ID="txtTotalWithDiscAmt" runat="server" Height="18px" Width="100%" Font-Bold="true" BackColor="#CCCCCC" AutoCompleteType="Disabled" style="text-align:right" BorderStyle="None" Visible="True"></asp:TextBox>
                               
                              </td>
                              <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                            
                                     </td>
                              <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  <asp:TextBox ID="txtTotalGSTAmt" runat="server" Height="18px" Width="100%" Font-Bold="true" BackColor="#CCCCCC" AutoCompleteType="Disabled" style="text-align:right" BorderStyle="None" Visible="True"></asp:TextBox>
                              
                                     </td>
                      
                       <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  <asp:TextBox ID="txtTotalWithGST" runat="server" Height="18px" Width="95%" Font-Bold="true" BackColor="#CCCCCC" AutoCompleteType="Disabled" style="text-align:right" BorderStyle="None" Visible="True"></asp:TextBox>
                                
                               </td>
                        </tr>
                        
                           
                         
                          
                      <tr style="width:100%; padding-left:10%;">
              
                           
                            <td colspan="1" style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;">
                                      <asp:TextBox ID="txtARCode" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                                    
                                 </td>
                               <td colspan="1" style="width:115px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                         <asp:TextBox ID="txtARDescription" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                               
                                 </td>
                               <td colspan="1" style="width:82px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                                   <asp:TextBox ID="txtOriginalAccountId" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" MaxLength="10" Width="43%"></asp:TextBox>
                               
                                 </td>

                           <td colspan="1" style="width:120px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                 <asp:TextBox ID="txtGSTOutputCode" runat="server"  Width="1%" TabIndex="108" Height="60px" Font-Names="Calibri" Font-Size="15px" BorderStyle="None" ForeColor="White"  ></asp:TextBox>   
                           
                                 </td>
                          <td colspan="3" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
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
                                     <asp:TextBox ID="txtARCode10" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox>
                                
                                     <asp:TextBox ID="txtARDescription10" runat="server" BorderStyle="None" Font-Names="Calibri" Font-Size="15px" ForeColor="White" Height="60px" TabIndex="108" Width="1%"></asp:TextBox>
                                
                                 </td>

                          
                           <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             <asp:TextBox ID="txtGSTOutputDescription" runat="server"  Width="1%" TabIndex="108" Height="60px" Font-Names="Calibri" Font-Size="15px" BorderStyle="None" ForeColor="White"  ></asp:TextBox>   
                                  </td>
                            <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                              <asp:TextBox ID="txtARProductCode10" runat="server"  Width="1%" TabIndex="108" Height="60px" Font-Names="Calibri" Font-Size="15px" BorderStyle="None" ForeColor="White"  ></asp:TextBox>   
                    
                                  
                                 </td>

                            <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                       
                                 </td>
                            <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                       
                                 </td>

                            <td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                        
                                 </td>
                            
                          <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                             <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="90%" OnClientClick="if(this.value === 'Saving...') { return false; } else { this.value = 'Saving...'; } ; return DoValidationSave()" />
                    
                               
                                   </td>
                             
                            <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                                 <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="90%" />
           
                               </td>
                        </tr>
                        
            </table>

       <div style="text-align:center">
       <asp:LinkButton ID="btnTop" runat="server" Font-Names="Calibri" Font-Bold="True" ForeColor="Brown">Go to Top</asp:LinkButton></div>
   </div>
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
                                     <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>
                      
                                        </td>
                                  <td style="text-align:left;width:10%">
                                       <asp:TextBox ID="txtRowCount" runat="server" CssClass="dummybutton"></asp:TextBox>
                                   </td>
                                  <td style="text-align: left">
                                      </td>
                                   <td style="text-align: left">
                                  </td>
                            
                              </tr>

    </table>


          <asp:Button ID="btnDummyGL" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
      
  <asp:ModalPopupExtender ID="mdlPopupGL" runat="server" BackgroundCssClass="modalBackground" CancelControlID="" PopupControlID="pnlPopUpGL" TargetControlID="btnDummyGL"></asp:ModalPopupExtender>


            <asp:Panel ID="pnlPopUpGL" runat="server" BackColor="White" Width="1000px" Height="600px" BorderColor="#003366" BorderWidth="1px"    HorizontalAlign="Left" ScrollBars="Vertical" >

                     <table>
           <tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Select GL Code</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                           
         <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopUpGL" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkGL(this, event);" onfocus = "WaterMarkGL(this, event);" AutoPostBack="True">Search Here for GL Code or Description</asp:TextBox>
    <asp:ImageButton ID="btnPopUpGLSearch"  runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" /><asp:ImageButton ID="btnPopUpGLReset"  runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
                               <asp:TextBox ID="txtPopupTeamSearch" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr>


        </table>

     
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="GrdViewGL" runat="server" DataSourceID="SqlDSGL" OnRowDataBound = "OnRowDataBoundgGL" OnSelectedIndexChanged = "OnSelectedIndexChangedgGL" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="750px"  Font-Size="15px" PageSize="8">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="COACode" HeaderText="Code" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-CssClass="dummybutton" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
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
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to POST the Voucher?"></asp:Label>
                        
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
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label11" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 10px; padding-bottom: 3px;">
        
             <asp:GridView ID="grdViewEditHistory" runat="server" DataSourceID="sqlDSViewEditHistory" ForeColor="#333333" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="4" GridLines="None" Width="95%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                    <asp:Button ID="btnEditHistory1" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
              
             

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewEditHistory" runat="server" CancelControlID="btnEditHistory1" PopupControlID="pnlViewEditHistory" TargetControlID="btnDummyViewEditHistory" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewEditHistory" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Edit History--%>


         <%--start--%>

            <asp:Panel ID="pnlConfirmReverse" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label7" runat="server" Text="Confirm REVERSE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label8" runat="server" Text="Are you sure to REVERSE the Voucher?"></asp:Label>
                        
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
                         
                          <asp:Label ID="Label9" runat="server" Text="Confirm POST/UPDATE "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label10" runat="server" Text="Do you want to POST/UPDATE the Voucher?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center" class="auto-style1"><asp:Button ID="btnConfirmYesSavePost" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="if(this.value === 'Posting...') { return false; } else { this.value = 'Posting...'; }"/>
                                 <asp:Button ID="btnConfirmNoSavePost" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmSavePost" runat="server" CancelControlID="btnConfirmNoSavePost" PopupControlID="pnlConfirmSavePost" TargetControlID="btndummySavePost" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummySavePost" runat="server" CssClass="dummybutton" />
         <%--end--%>
  

                           <%-- update GST Code--%>

      <asp:Panel ID="pnlUpdateGSTCode" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label24" runat="server" Text="Change Tax Code "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label25" runat="server" Text="Do you want to CHANGE the Tax Code for this Invoice?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesUpdateGSTCode" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmNoUpdateGSTCode" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlUpdateGSTCode" runat="server" TargetControlID="btnUpdateGSTCode"   PopupControlID="pnlUpdateGSTCode" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnUpdateGSTCode" runat="server" CssClass="dummybutton" />
         <%--end--%>

             ''''''''''
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
                                  <asp:ListItem Value="O">O - Open</asp:ListItem>
                                   <asp:ListItem Value="V">V - Void</asp:ListItem> 
                                    
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


             <%--start: Multi Print--%>


      <asp:Panel ID="pnlMultiPrint" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1"  ScrollBars="auto"  style="text-align:left; width:1300px; height:600px; margin-left:auto; margin-right:auto;"  Visible="true" >

                   <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:center;padding-left:10px; width:100%;">
                           
         <tr style="width:100%"><td  style="width:80%;font-size:18px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:10px;">&nbsp;Select Records to Print&nbsp; </td>
             
                 <td  style="width:20%;font-size:18px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:0px;">
                           <asp:CheckBox ID="chkShowUnprinted" runat="server" Font-Size="12px" Text="Show Unprinted Invoices Only" AutoPostBack="True" Visible="False" />
                           </td></tr>


        </table>

     
        <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
            <br />
            <div style="text-align:center; width:100%; margin-left:auto; margin-right:auto;" >
            
                                      <asp:Panel ID="Panel3" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1"  ScrollBars="auto"  style="text-align:left; width:1300px; height:300px; margin-left:auto; margin-right:auto;"  Visible="true" >

            <asp:GridView ID="grdViewMultiPrint" Width="100%" Font-Size="15px" runat="server"  AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSMultiPrint" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                                <Columns>

                                                          <asp:TemplateField> 
               <HeaderTemplate><asp:CheckBox ID="chkSelectAllMultiPrintGV" runat="server" AutoPostBack="false" TextAlign="left" onchange="checkmultiprint()" Width="5%" ></asp:CheckBox></HeaderTemplate>    <HeaderStyle HorizontalAlign="Left" />
               <ItemTemplate><asp:CheckBox ID="chkSelectMultiPrintGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" CommandName="CHECK" ></asp:CheckBox></ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Left" />
                                                          </asp:TemplateField>      
                                                          <asp:BoundField DataField="PrintCounter" HeaderText="Print Count" >
                                                          <ControlStyle CssClass="dummybutton" />
                                                          <HeaderStyle CssClass="dummybutton" />
                                                          <ItemStyle CssClass="dummybutton" />
                                                          </asp:BoundField>
                                                      <asp:TemplateField HeaderText="CN/DN Number" SortExpression="InvoiceNumber">
                                                              <EditItemTemplate>
                                                                  <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("InvoiceNumber") %>'></asp:TextBox>
                                                              </EditItemTemplate>
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblInvNo" runat="server" Text='<%# Bind("InvoiceNumber") %>'></asp:Label>
                                                              </ItemTemplate>
                                                              <ControlStyle Width="8%" />
                                                              <ItemStyle Width="8%" Wrap="False" HorizontalAlign="LEFT" />
                                                          </asp:TemplateField>
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
                                                  <asp:BoundField DataField="PostStatus" SortExpression="PostStatus" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Post St" SortExpression="PostStatus" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="PaidStatus" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                        
                                                  <asp:BoundField DataField="SalesDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CN/DN Date" SortExpression="SalesDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GLPeriod" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" SortExpression="ContactType" />
                                                  <asp:BoundField DataField="CustName" HeaderText="Client Name" SortExpression="CustName">
                                                    <ControlStyle Width="15%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="15%" />
                                                  </asp:BoundField>
                                                 
                                                    <asp:BoundField DataField="ValueBase" HeaderText="Amount" DataFormatString="{0:N2}">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GSTbase" HeaderText="GST Amount">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AppliedBase" HeaderText="Net Amount" DataFormatString="{0:N2}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BalanceBase" HeaderText="Balance Amount" DataFormatString="{0:N2}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="CustAddress1" HeaderText="Bill Address">
                                                    <ControlStyle Width="17%" />
                                                    <ItemStyle Width="17%" Wrap="False" HorizontalAlign="Left" />
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
                                                    <asp:BoundField DataField="CustAddPostal" HeaderText="Postal" SortExpression="CustAddPostal" />
                                                    <asp:BoundField DataField="StaffCode" HeaderText="Salesman" SortExpression="StaffCode" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Edited On" SortExpression="LastModifiedOn" />
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
                                                    <asp:BoundField DataField="Terms" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DiscountAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GSTBase">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comments">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                              </Columns>
                                              <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                                            <PagerStyle ForeColor="White" HorizontalAlign="Left" BackColor="#507CD1" />
                                            <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
                                            <SelectedRowStyle BackColor="#00ccff" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                      </asp:GridView>

                                          </asp:Panel>
                </div>
            <br /><br />
                          <table border="0" style="WIDTH:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:center;padding-left:20px;">
                    
        <tr style="padding-left:40px; width:100%">
                             <td style="text-align:center; width:100%">
                                       <%--<a href="RV_CreditNote.aspx" target="_blank"> <asp:Button ID="btnPrintMultiPrint" Text = "Print" runat="server" OnClientClick="openInNewTab();" OnClick = "PrintCN"></asp:Button></a>--%>
                   <%--   <a href="RV_CreditNote.aspx" target="_blank"><button id="btn" runat="server" onclick="printCN"  class="button" style="background-color:#CFC6C0;font-weight:bold;width:95%; font-size:15px;text-align:CENTER;font-family:'CALIBRI';" type="button">PRINT</button></a>
     
                             <a href="RV_CreditNote.aspx" target="_blank">   <asp:Button ID="btnPrintMultiPrint1" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick="openInNewTab();" OnClick = "PrintCN"/>
                             </a>  --%>
                                  <asp:Button ID="btnPrintMultiPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Print" Width="100px" />
                                   <asp:Button ID="btnCancelMultiPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>


        </table>
           
            <asp:SqlDataSource ID="SqlDSMultiPrint" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="Select COACode, Description, GLType from tblchartofaccounts
 order by COACode">
            </asp:SqlDataSource>
      
              </div>
    </asp:Panel>

          <asp:ModalPopupExtender ID="mdlPopupMultiPrint" runat="server" CancelControlID="btnCancelMultiPrint" PopupControlID="pnlMultiPrint" TargetControlID="btnDummyMultiPrint" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
         
          <asp:Button ID="btnDummyMultiPrint" runat="server" CssClass="dummybutton" />

         <%--End: Multi print--%>



        <%-- aaaaa--%>

                     <%-- start--%>

                <asp:Panel ID="pnlEditBillingName" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" style="text-align:center;"    >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Billing Name</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageBillingName" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBillingName" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                          <tr>
                      <td class="CellFormat" style="text-align:right;" >Billing Name </td>
                    <td class="CellTextBox">  
                        <asp:TextBox ID="txtBillingNameEdit" runat="server" Height="16px" MaxLength="200" Width="70%"></asp:TextBox>
                          
              
                      </td>
                                         
                  </tr>

             
                        
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditBillingNameSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnEditBillingNameCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEditBillingName" runat="server" CancelControlID="btnEditBillingNameCancel" PopupControlID="pnlEditBillingName" TargetControlID="btndummyEditBillingName" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyEditBillingName" runat="server" cssclass="dummybutton" />
            <%-- popup1--%>

            <%-- start--%>

                <asp:Panel ID="pnlEditBillingDetails" runat="server" BackColor="White" Width="40%" Height="70%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" style="text-align:center;"    >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Billing Details</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageBillingDetails" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBillingDetails" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                          <tr>
                      <td class="CellFormat" style="text-align:right;" >Contact Person</td>
                    <td class="CellTextBox">  
                        <asp:TextBox ID="txtContactPersonEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                          
              
                      </td>
                                         
                  </tr>

             
   
                        
                         <tr>
                             <td class="CellFormat" style="text-align:right;">Address<asp:Label ID="Label63" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                             </td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillAddressEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align:right;">&nbsp;</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillStreetEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align:right;">&nbsp;</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillBuildingEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>

             
   
                        
                         <tr>
                             <td class="CellFormat" style="text-align: right;">City</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillCityEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align: right;">State</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillStateEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align: right;">Country<asp:Label ID="Label62" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                             </td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillCountryEdit" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align: right;">Postal</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillPostalEdit" runat="server" Height="16px" MaxLength="20" Width="80%"></asp:TextBox>
                             </td>
                         </tr>

             
   
                        
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditBillingDetailsSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnEditBillingDetailsCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEditBillingDetails" runat="server" CancelControlID="btnEditBillingDetailsCancel" PopupControlID="pnlEditBillingDetails" TargetControlID="btndummyEditBillingDetails" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyEditBillingDetails" runat="server" cssclass="dummybutton" />
            <%-- popup1--%>

        <%-- aaaaa--%>

<%--         Search Start--%>

            <asp:Panel ID="pnlConfirmPrint" runat="server" BackColor="White" Width="350px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
             <%-- <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label11" runat="server" Text="Confirm Print"></asp:Label>
                        
                      </td>
                           </tr>--%>
                 <tr>
                               <td colspan="1" style="text-align:center;"><%--<h4 style="color: #000000">Customer</h4>--%> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPrintClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>

                         </tr>
             
               <tr>
                      <td class="CellFormat" colspan="2" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label12" visible="false" runat="server" Text="Are you sure to multiprint Credit Note?"></asp:Label>
                           <asp:Label ID="txtCNNos" Visible="false" runat="server"></asp:Label>
                        
                      </td>
                           </tr>
                           
                            <tr style="padding-top:0px;">
                             <%--<td colspan="2" style="text-align:center">
                                      <a href="RV_CreditNote.aspx?Export=Word" target="_blank"><imagebutton style="border:none;background-color:white;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                    &nbsp;&nbsp;
                                   <a href="Email.aspx?Type=CreditNote" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                           &nbsp;&nbsp;
                             <a href="RV_CreditNote.aspx?Export=PDF" target="_blank"><linkbutton style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px" type="button">Print CreditNote</linkbutton></a>
                            
                               </td>--%>

                                   <td colspan="2" style="text-align:center">
                                    <%--  <a href="RV_CreditNote.aspx?Export=Word" target="_blank"><imagebutton style="border:none;background-color:white;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                    &nbsp;&nbsp;
                                   <a href="Email.aspx?Type=CreditNote" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                           &nbsp;&nbsp;
                             <a href="RV_CreditNote.aspx?Export=PDF" target="_blank"><linkbutton style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px" type="button">Print CreditNote</linkbutton></a>
                            --%>

                       <%--            <a href="RV_CreditNote.aspx?Export=PDF" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp;--%>
                                 <a href="RV_CreditNote.aspx?Export=PDF" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp;
                                  <a href="RV_CreditNote.aspx?Export=Word" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp; 
                               <a href="Email.aspx?Type=CreditNote" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                 &nbsp;&nbsp; <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px; vertical-align: super;">Print Credit Note</label><br /><br />

                               </td>
                         </tr> 
             <tr><td colspan="2"><br /></td></tr>                      
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlConfirmMultiPrint" runat="server" CancelControlID="btnPrintClose" PopupControlID="pnlConfirmPrint" TargetControlID="btndummyconfirmMultiPrint" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyconfirmMultiPrint" runat="server" CssClass="dummybutton" />
            

               <asp:Panel ID="pnlSearch" runat="server" BackColor="White" Width="85%" Height="95%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto" HorizontalAlign="Center">
              <br /><br />
                     <table border="0"  style="width:90%; border:thin;   padding-left:3px; margin-left:auto; margin-right:auto;  " >

                            <tr>
                               <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="6">Search</td>
                           </tr>
                    
                       <tr>
                               <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Invoice No.
                               </td>
                              <td colspan="3" style="text-align:left;padding-left:5px; width:10%">   
                                   <asp:TextBox ID="txtSearchInvoiceNo" runat="server" MaxLength="50" Height="16px" Width="88%"></asp:TextBox>
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
                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Invoice Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtInvoiceDateSearchFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateFrom" TargetControlID="txtInvoiceDateSearchFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To
                       </td>
                    <td  style="text-align:left;width:15%; padding-right:8px; ">
                       <asp:TextBox ID="txtInvoiceDateSearchTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateTo" TargetControlID="txtInvoiceDateSearchTo"/>
                     </td>
                           
                         <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                             &nbsp;</td>
                                   <td style="width:12%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                    
                                       <asp:RadioButtonList ID="rdbSearchPaidStatus0" runat="server" RepeatDirection="Horizontal" Visible="False">
                                           <asp:ListItem Selected="True">All</asp:ListItem>
                                           <asp:ListItem>O/S</asp:ListItem>
                                           <asp:ListItem>Fully Paid</asp:ListItem>
                                       </asp:RadioButtonList>
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
                                 <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Our Reference</td>
                              <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                  <asp:TextBox ID="txtSearchOurRef" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                               </td>
                             </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account ID </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:10%">
                                       <asp:TextBox ID="txtSearchAccountID" runat="server" Height="16px" MaxLength="50" Width="88%"></asp:TextBox>
                                       &nbsp;<asp:ImageButton ID="btnClient2" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Your Reference</td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:TextBox ID="txtSearchYourRef" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account Name</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:10%">
                                       <asp:TextBox ID="txtSearchClientName" runat="server" Height="16px" MaxLength="100" Width="88%"></asp:TextBox>
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">PO No.</td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:TextBox ID="txtSearchPONo" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Remarks</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:15%">
                                       <asp:TextBox ID="txtSearchComments" runat="server" Height="16px" MaxLength="100" Width="88%"></asp:TextBox>
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Salesman</td>
                                    <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                 
                                        <asp:DropDownList ID="ddlSearchSalesman" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%" DataSourceID="SqlDSSalesMan">
                                            <asp:ListItem Text="--SELECT--" Value="-1" />
                                        </asp:DropDownList>
                                   </td>
                               </tr>

                  

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Last Edit Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchEditEndFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender18" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndFrom" TargetControlID="txtSearchEditEndFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchEditEndTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender19" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndTo" TargetControlID="txtSearchEditEndTo"/>
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
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >
                                       Entry Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchEntryDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender20" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateFrom" TargetControlID="txtSearchEntryDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; padding-right:25px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchEntryDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender21" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateTo" TargetControlID="txtSearchEntryDateTo"/>
                     </td>

                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       Created By</td>
                                    <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:DropDownList ID="ddlSearchCreatedBy" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>

                               <tr>
                                      <td colspan="4" style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <br />
                                   </td>

                                     <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       &nbsp;</td>
                                       <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
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

            <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" CancelControlID="btnStatusCancel" PopupControlID="pnlStatusSearch" TargetControlID="btnDummyStatusSearch1" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
    <asp:Button ID="btnDummyStatusSearch1" runat="server" CssClass="dummybutton" />
        <%-- status--%>
               <asp:Panel ID="pnlPopUpContractNo" runat="server" BackColor="White" Width="800" Height="600" BorderColor="#003366" BorderWidth="1" Visible="true"
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
                             <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="top"/>
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
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDSContractNo" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                     </asp:SqlDataSource>
        </div>
    </asp:Panel>
      <asp:ModalPopupExtender ID="mdlPopUpContractNo" runat="server" CancelControlID="btnPopUpContractNoClose" PopupControlID="pnlPopUpContractNo"
                                                TargetControlID="btnContractNo" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
          <asp:Button ID="btnContractNo" runat="server" CssClass="dummybutton" />
                   </ContentTemplate>
</asp:UpdatePanel> 
       
</asp:Content>

