<%@ page language="vb" title="Service Schedule" autoeventwireup="false" inherits="ServiceSchedule" CodeFile="ServiceSchedule.aspx.vb" masterpagefile="~/MasterPage.Master" culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="CSS/loader.css" />

    <style type="text/css">
        .Hide {
            display: none;
        }

        .cell {
            text-align: left;
        }

        .righttextbox {
            float: right;
        }

        .AlphabetPager a, .AlphabetPager span {
            font-size: 8pt;
            display: inline-block;
            min-width: 15px;
            text-align: center;
            text-decoration: none;
            font-weight: bold;
        }

        .AlphabetPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .AlphabetPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }

        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .wrp {
            width: 100%;
            text-align: center;
        }

        .frm {
            text-align: left;
            width: 500px;
            margin: auto;
            height: 40px;
        }

        .fldLbl {
            white-space: nowrap;
        }

        .lbl {
            font-size: 15px;
            font-weight: bold;
            font-family: 'Calibri';
            color: black;
            text-align: right;
        }
          
                    .roundbutton1 {
            border: 2px solid #a1a1a1;   
    background: #dddddd;   
    border-radius: 25px;
    height:30px;
    width:90%;
        }
        </style>
    <script>
        function ClearTextBox() {
            document.getElementById("<%=txtClient.ClientID%>").value = '';
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

        function btn_disable() {

            currentdatetime();

            document.getElementById("<%=btnBack.ClientID%>").disabled = true;
             document.getElementById("<%=btnServiceSchedule.ClientID%>").disabled = true;
            document.getElementById("<%=btnGenerateServiceSchedule.ClientID%>").disabled = true;
             return true;
         }

         function btn_disable1() {

             document.getElementById("<%=btnBack.ClientID%>").disabled = true;
               document.getElementById("<%=btnGenerateServiceSchedule.ClientID%>").disabled = true;
             
               return true;
           }
      
        function DoValidation(parameter) {
            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;
         

            if (str != "" && regex.test(str)) {
                valid = true;
            }
            else {
                valid = false;
                alert("Enter Only Numbers for Agreed Value");
            }

            if (str1 != "" && isNaN(str1)) {
                alert("Enter Only Numbers for Allocated Time");
                valid = false;
            }

            if (str2 != "" && isNaN(str2)) {
                alert("Enter Only Numbers for Value per month");
                valid = false;
            }
            if (str3 != "" && isNaN(str3)) {
                alert("Enter Only Numbers for Duration");
                valid = false;
            }

            if (str4 != "" && isNaN(str4)) {
                alert("Enter Only Numbers for No. of Services Tot. Amt.");
                valid = false;
            }
            if (str5 != "" && isNaN(str5)) {
                alert("Enter Only Numbers for No. of Services Tot. Services");
                valid = false;
            }
            if (str6 != "" && isNaN(str6)) {
                alert("Enter Only Numbers for No. of Services Completed");
                valid = false;
            }
            if (str7 != "" && isNaN(str7)) {
                alert("Enter Only Numbers for No. of Services Balance");
                valid = false;
            }

            if (str8 != "" && isNaN(str8)) {
                alert("Enter Only Numbers for No. of Hours Tot. Amt.");
                valid = false;
            }
            if (str9 != "" && isNaN(str9)) {
                alert("Enter Only Numbers for No. of Hours Tot. Services");
                valid = false;
            }
            if (str10 != "" && isNaN(str10)) {
                alert("Enter Only Numbers for No. of Hours Service Completed");
                valid = false;
            }
            if (str11 != "" && isNaN(str11)) {
                alert("Enter Only Numbers for No. of Hours Service Balance");
                valid = false;
            }

            if (str12 != "" && isNaN(str12)) {
                alert("Enter Only Numbers for No. of Phone Calls Tot. Amt.");
                valid = false;
            }
            if (str13 != "" && isNaN(str13)) {
                alert("Enter Only Numbers for No. of Phone Calls Tot. Services");
                valid = false;
            }
            if (str14 != "" && isNaN(str14)) {
                alert("Enter Only Numbers for No. of Phone Calls Service Completed");
                valid = false;
            }
            if (str15 != "" && isNaN(str15)) {
                alert("Enter Only Numbers for No. of Phone Calls Service Balance");
                valid = false;
            }

            if (str16 != "" && isNaN(str16)) {
                alert("Enter Only Numbers for No. of Units Tot. Amt.");
                valid = false;
            }
            if (str17 != "" && isNaN(str17)) {
                alert("Enter Only Numbers for No. of Units Tot. Services");
                valid = false;
            }
            if (str18 != "" && isNaN(str18)) {
                alert("Enter Only Numbers for No. of Units Service Completed");
                valid = false;
            }
            if (str19 != "" && isNaN(str19)) {
                alert("Enter Only Numbers for No. of Units Service Balance");
                valid = false;
            }

            if (str20 != "" && isNaN(str20)) {
                alert("Enter Only Numbers for Unexpired Compensation Balance Tot. Service");
                valid = false;
            }
            if (str21 != "" && isNaN(str21)) {
                alert("Enter Only Numbers for Unexpired Compensation Balance");
                valid = false;
            }
          
            if (str22 != "" && isNaN(str22)) {
                alert("Enter Only Numbers for Response");
                valid = false;
            }

            if (str23 != "" && isNaN(str23)) {
                alert("Enter Only Numbers for Cancel Charges");
                valid = false;
            }
            if (str24 != "" && isNaN(str24)) {
                alert("Enter Only Numbers for Min. Duration");
                valid = false;
            }

            if (str27 != "" && isNaN(str27)) {
                alert("Enter Only Numbers for Quote Price");
                valid = false;
            }

            if (str28 != "" && isNaN(str28)) {
                alert("Enter Only Numbers for Contract Det. Value");
                valid = false;
            }
            if (str29 != "" && isNaN(str29)) {
                alert("Enter Only Numbers for Per Service Value");
                valid = false;
            }

            if (str25 != "" && isNaN(str25)) {
                alert("Enter Only Numbers for Discount Percent");
                valid = false;
            }

            if (str26 != "" && isNaN(str26)) {
                alert("Enter Only Numbers for Discount Amount");
                valid = false;
            }

            if (str30 != "" && isNaN(str30)) {
                alert("Enter Only Numbers for Total Contract Value");
                valid = false;
            }

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
          .auto-style1
          {
              width: 38%;
          }
          .auto-style2
          {
              font-size: 15px;
              font-weight: bold;
              font-family: 'Calibri';
              color: black;
              text-align: right;
              width: 38%;
        /*table-layout:fixed;
        overflow:hidden;*/
              border-collapse: collapse;
              border-spacing: 0;
              line-height: 10px;
          }
          .auto-style3
          {
              width: 50%;
          }
          .auto-style4
          {
              font-size: 15px;
              font-weight: bold;
              font-family: 'Calibri';
              color: black;
              text-align: right;
              width: 50%;
        /*table-layout:fixed;
        overflow:hidden;*/
              border-collapse: collapse;
              border-spacing: 0;
              line-height: 10px;
          }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release" AsyncPostBackTimeout="600" AsyncPostBackErrorMessage="We can not serve your request at this moment. Please try later.">
           <ControlBundles>
        <cc1:ControlBundle Name="CalendarExtender_Bundle" />
           <cc1:controlBundle name="ModalPopupExtender_Bundle"/>
              <cc1:controlBundle name="MaskedEditExtender_Bundle"/>
                <cc1:controlBundle name="TabContainer_Bundle"/>
   </ControlBundles>
    </cc1:ToolkitScriptManager>   
       <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>
         
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>



            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Service Schedule</h3>
       
        <table style="width:100%;text-align:center;">

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
                <td style="width:60%;text-align:center;"> 
                    <asp:Button ID="btnGenerateServiceSchedule" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Generate Service Schedule" Width="17%" OnClientClick= "currentdatetime(); btn_disable();  " />
                     <asp:Button ID="btnBack" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="BACK" Width="16%" />
                 
                       <asp:Button ID="btnServiceSchedule" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Service Records" Width="16%" OnClick="btnServiceSchedule_Click" OnClientClick=" btn_disable1()" Visible="False"/>
                    <asp:Button ID="btnPrint" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="16%" OnClientClick=" btn_disable1()" Enabled="False" Visible="False"/>
                    
                    <asp:Button ID="btnCalculateValues" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ReCalculate Schedule" Width="15%" /></td>
           <asp:Label ID="Label1" Style="color: white" runat="server" Visible="false" Text="Label"></asp:Label>    
                       
            </tr>
</Table>
         <br />

             <asp:Panel ID="Panel5" style="margin-left:auto;margin-right:auto;"    runat="server" BackColor="#F8F8F8"  BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Width="100%" Height="100%"  HorizontalAlign="Center">
                              
                     <table  style="width:98%;text-align:center;padding-top:5px; margin-left:auto; margin-right:auto">
                                
                         <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >ContractNo
                             </td>
                           <td style="text-align:left; width:15%">   
                                 <asp:TextBox ID="txtContractNo" runat="server" MaxLength="50" Height="16px" Width="95%" AutoCompleteType="Disabled" BackColor="#CCCCCC"></asp:TextBox>
                           </td>
                             <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Contract Date </td>
                           <td style="text-align:left;width:10%">  <asp:TextBox ID="txtContractDate" runat="server" Height="16px" Width="90%" AutoCompleteType="Disabled" BackColor="#CCCCCC"></asp:TextBox>
                         </td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >
                                 Service Start</td>
                           <td style="text-align:left;width:10%">  
                                <asp:TextBox ID="txtFrom" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="90%" AutoPostBack="True"></asp:TextBox>
                               <cc1:CalendarExtender ID="calConStart" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtFrom" TargetControlID="txtFrom"></cc1:CalendarExtender>
                             </td>

                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;width:6%">   <asp:Label ID="lblMonths" runat="server" Text="Months"></asp:Label></td>
                             <td style="text-align:left;width:6%;padding-right:1%;">
                                       <asp:TextBox ID="txtMonths" runat="server" AutoCompleteType="Disabled" BackColor="#FFFFFF" Height="16px" Width="90%" AutoPostBack="True"></asp:TextBox>
                         
                             </td>
                           <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >Service End</td>
                           <td style="width:12%; text-align: left;">        
                               <asp:TextBox ID="txtTo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="90%"></asp:TextBox>
                             </td>
                        </tr>

                            <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >ContactType
                             </td>
                           <td style="text-align:left; width:10%"  >     
                               <asp:TextBox ID="txtContactType" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" Width="95%" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                           <td style="width:8%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Account Id</td>
                           <td style="text-align:left; width:10%">   
                               <asp:TextBox ID="txtAccountId" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="90%"></asp:TextBox>
                                </td>
                          
                           <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >Name
                               </td>
                           <td colspan="7" style="text-align:left;">
                               <asp:TextBox ID="txtCustName" runat="server" Height="16px" Width="97%" AutoCompleteType="Disabled" BackColor="#CCCCCC"></asp:TextBox> </td>
                        </tr>

                           <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >Computation Method</td>
                           <td style="text-align:left;" > 
                                 <asp:TextBox ID="txtComputationMethod1" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="95%"></asp:TextBox>
                               
                            </td>
                                                  
                            <td style="width:8%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                                Agree Value</td>
                               
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >
                                <asp:TextBox ID="txtAgreeValue" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="90%"></asp:TextBox>
                               </td>
                           <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               Value / Month</td>
                                <td colspan="1" style="text-align: left">
                                    <asp:TextBox ID="txtValPerMnth" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="90%"></asp:TextBox>
                               </td> 

                                <td style="text-align:left;width:6%">&nbsp;</td>
                               <td style="text-align:left;width:5%">
                                   <asp:TextBox ID="txtMonthsOriginal" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" BackColor="#FFFFFF" Height="16px" Visible="False" Width="50%"></asp:TextBox>
                               </td>

                                <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtBatchNo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" MaxLength="50" Width="80%" Visible="False"></asp:TextBox>
                               </td>
                               <td colspan="1" style="text-align: left">
                                     <asp:TextBox ID="txtContact" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" MaxLength="50" Width="40%" Visible="False"></asp:TextBox>
                                   </td>
                           </tr>


                        
                         <tr>
                             <td  style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                 Technician</td>
                             <td  style="text-align:left;" class="auto-style1">
                                 <asp:TextBox ID="txtTechnician" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="95%"></asp:TextBox>
                             </td>
                             <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                 Time In</td>
                             
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;" >
                                 <asp:TextBox ID="txtTimeIn" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="90%"></asp:TextBox>
                             </td>
                             <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                 Time Out</td>
                             <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                 <asp:TextBox ID="txtTimeOut" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="90%"></asp:TextBox>
                             </td>

                             <td style="text-align:left;width:6%">&nbsp;</td>
                             <td style="text-align:left;width:5%">
                                 <asp:TextBox ID="txtContractStartOriginal" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" BackColor="#FFFFFF" Height="16px" Visible="False" Width="50%"></asp:TextBox>
                             </td>

                             <td style="width:15%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                 Contract Type</td>
                             <td style="width:20%; text-align: left;">
                                 <asp:TextBox ID="txtFixedContinuous" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="90%"></asp:TextBox>
                             </td>
                         </tr>
                        
                                 
                                 
                             <tr>
                                 <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                     Service Address</td>
                                 <td class="auto-style2" style="text-align:left;">
                                     <asp:TextBox ID="txtServiceAddress" runat="server" BackColor="#CCCCCC" Width="95%" ></asp:TextBox></td>
                                 <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                     <asp:TextBox ID="txtClient" runat="server" AutoCompleteType="Disabled" BackColor="#F8F8F8" BorderStyle="None" ForeColor="White" Height="16px" Width="30%"></asp:TextBox>
                                 </td>
                                 <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                     <asp:TextBox ID="txtBillingAmount" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Visible="False" Width="40%"></asp:TextBox>
                                     <asp:TextBox ID="txtComputationMethod" runat="server" AutoCompleteType="Disabled" BackColor="#F8F8F8" BorderStyle="None" ForeColor="#003300" Height="16px" Visible="False" Width="30%"></asp:TextBox>
                                 </td>
                                 <td style="text-align:left;width:150px">
                                     <asp:TextBox ID="txtAgreementType" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Visible="False" Width="60%"></asp:TextBox>
                                 </td>
                                 <td  style="width:10%;">
                                     <asp:TextBox ID="txtMonthStartDate" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Visible="False" Width="60%"></asp:TextBox>
                                 </td>

                                 <td style="text-align:left;width:6%">&nbsp;</td>
                                 <td style="text-align:left;width:5%">&nbsp;</td>

                                 <td style="text-align: left">
                                     <asp:TextBox ID="txtMonthEndDate" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Visible="False" Width="80%"></asp:TextBox>
                                 </td>
                                 <td>
                                     <asp:TextBox ID="txtBillingFreq" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Visible="False" Width="30%"></asp:TextBox>
                                 </td>
                         </tr>
                        
                                 
                                 
                             <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >Schedule</td>
                           <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtServEnd" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Visible="False" Width="30%"></asp:TextBox>
                                 </td>
                             <td style="width:8%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                                 <asp:TextBox ID="txtServStart" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Visible="False" Width="40%"></asp:TextBox>
                                 </td>
                           <td style="text-align:left; width:10%">  </td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >
                                 Total Service Value</td>
                           <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%" >
                               <asp:TextBox ID="txtTotalServiceValue" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%"></asp:TextBox>
                                 </td>
                              </td>
                          
                                 <td style="text-align:left;width:6%">&nbsp;</td>
                                 <td style="text-align:left;width:5%">&nbsp;</td>
                          
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; width:10%">Total Records :</td>
                          <td style="width:12%; text-align: left;"> <asp:TextBox ID="txtTotalRecords" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%"></asp:TextBox>
                                </td>
                          
                               </tr>
                                     
                                        
                                         <tr>
                                             <td align="left" colspan="10" style="text-align:center;padding-left:3px;" style:="" valign="middle" >

                                                  <asp:UpdatePanel ID="updPnlSch" runat="server">
                                                      <ContentTemplate>
                                                          <asp:GridView ID="grvschedule" runat="server" RowStyle-Font-Size="15px" Width="100%" AllowSorting ="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSServiceSchedule" Font-Size="11pt" HorizontalAlign="Center"  ForeColor="#333333" GridLines="Vertical">
                                                           <AlternatingRowStyle BackColor="White" />
                                                              <Columns>

                                                                  <asp:TemplateField HeaderText="S No">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                      <ControlStyle  />
                                                                    <ItemStyle Width="5%" ForeColor="#993300" />
                                                                </asp:TemplateField>

                                                                  <asp:BoundField DataField="ServiceDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Service Date" SortExpression="ServiceDate" >
                                                                  <ControlStyle Width="8%" />
                                                                  <HeaderStyle Wrap="False" />
                                                                  <ItemStyle Width="8%"  />
                                                                  </asp:BoundField>
                                                                  <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                                                      <EditItemTemplate>
                                                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ServiceDate")%>'></asp:TextBox>
                                                                      </EditItemTemplate>
                                                                      <ItemTemplate>
                                                                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("ServiceDate")%>'></asp:Label>
                                                                      </ItemTemplate>
                                                                  </asp:TemplateField>
                                                                  <asp:BoundField DataField="DOW" HeaderText="DOW" >
                                                                  <ControlStyle Width="9%" />
                                                                  <ItemStyle Wrap="True" Width="9%"  />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="PublicHoliday" HeaderText="Public Holiday" >
                                                                  <ControlStyle Width="10%" />
                                                                  <HeaderStyle Wrap="False" />
                                                                  <ItemStyle Wrap="False" Width="10%" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="Frequency" HeaderText="Frequency" >
                                                                  <ControlStyle Width="10%" />
                                                                  <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Left" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="Services" HeaderText="Services" >
                                                                  <ControlStyle Width="10%" />
                                                                  <ItemStyle Width="10%" Wrap="False" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="TargetId" HeaderText="TargetId" >
                                                                  <ControlStyle Width="8%" />
                                                                  <ItemStyle Width="8%" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField HeaderText="Value" DataField="Value" />
                                                                   <asp:BoundField DataField="LocationID" Visible="True" HeaderText="Location Id" >
                                                                  <ControlStyle Width="12%" />
                                                                  <ItemStyle Width="12%" Wrap="False" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="ServiceName" Visible="True" HeaderText="Service Name" >
                                                                  <ControlStyle Width="15%" />
                                                                  <ItemStyle Width="15%" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="Address1" Visible="True" HeaderText="Service Address" >
                                                                  <ControlStyle Width="15%" />
                                                                  <ItemStyle Width="15%" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="LocateGRP" Visible="True" HeaderText="Zone" >
                                                                  <ControlStyle Width="8%" />
                                                                  <ItemStyle Width="8%" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="ServiceLocationGroup" HeaderText="Location Group" />
                                                                  <asp:BoundField DataField="Location" HeaderText="Location" Visible="False" />
                                                                  <asp:BoundField DataField="BranchId" HeaderText="BranchId" Visible="False" />
                                                                  <asp:BoundField DataField="Seq" HeaderText="Seq" SortExpression="Seq" Visible="False" />
                                                                  <asp:BoundField DataField="ContractNo" HeaderText="ContractNo" SortExpression="ContractNo" Visible="False" />
                                                                  <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                                                      <EditItemTemplate>
                                                                          <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                                                      </EditItemTemplate>
                                                                      <ItemTemplate>
                                                                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                                                      </ItemTemplate>
                                                                  </asp:TemplateField>
                                                                  <asp:TemplateField>
                                                                      <ItemTemplate>
                                                                          <asp:Button ID="btnLessGV" runat="server" Height="20px" OnClick="btnLessGV_Click" Style="text-align: center" Text="&lt;" Width="30px" />
                                                                      </ItemTemplate>
                                                                      <ItemStyle Width="3%" />
                                                                  </asp:TemplateField>
                                                                  <asp:TemplateField>
                                                                      <ItemTemplate>
                                                                          <asp:Button ID="btnMoreGV" runat="server" Height="20px" OnClick="btnMoreGV_Click" Style="text-align: center" Text="&gt;" Width="30px" />
                                                                      </ItemTemplate>
                                                                      <ItemStyle Width="3%" />
                                                                  </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                      <ItemTemplate>
                                                                          <asp:Button ID="btnDeleteGV" runat="server" Height="20px" OnClick="btnDeleteGV_Click" Style="text-align: left" Text="Del" Width="35px" ForeColor="Red"  />
                                                                      </ItemTemplate>
                                                                      <ItemStyle Width="5%" />
                                                                  </asp:TemplateField>
                                                              

                                                                  <asp:BoundField DataField="ContractDetSQLID" Visible="True" HeaderStyle-CssClass = "Hide" ItemStyle-CssClass="Hide" >
                                                                  <ControlStyle Width="5px" />
                                                                  <HeaderStyle CssClass="Hide" />
                                                                  <ItemStyle ForeColor="#E4E4E4" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="ContractDetFreqSQLID" Visible="True" HeaderStyle-CssClass = "Hide" ItemStyle-CssClass="Hide">
                                                                   <ControlStyle Width="5px" />
                                                                   <HeaderStyle CssClass="Hide" />
                                                                   <ItemStyle ForeColor="#E4E4E4" />
                                                                  </asp:BoundField>
                                                                   <asp:BoundField DataField="TargetDesc" Visible="True" HeaderStyle-CssClass = "Hide" ItemStyle-CssClass="Hide">
                                                                  <HeaderStyle CssClass="Hide" />
                                                                  <ItemStyle ForeColor="#E4E4E4" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="ServiceId" Visible="True" HeaderStyle-CssClass = "Hide" ItemStyle-CssClass="Hide">
                                                                  <HeaderStyle CssClass="Hide" />
                                                                  <ItemStyle ForeColor="#E4E4E4" />
                                                                  </asp:BoundField>
                                                                 
                                                                  <asp:BoundField DataField="Salesman" >
                                                                 
                                                                  <ControlStyle CssClass="dummybutton" />
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
                                                      </ContentTemplate>
                                                        <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="grvschedule" EventName="SelectedIndexChanged" />
                                         </Triggers>
                                        
                                     </asp:UpdatePanel>
                                             </td>
                                         </tr>
                            
                                    </table>
                               
                              
                          </asp:Panel>
           
         <%-- </div>--%>

       
   <%--</div>--%>
           <asp:SqlDataSource ID="SqlDSServiceSchedule" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="txtContractNo" Name="@ContractNo" PropertyName="Text" />
                 </SelectParameters>
    </asp:SqlDataSource>
           <br /><br />

    
    <asp:TextBox ID="txtMode" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtPrefixDocNoService" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtTotServiceRec" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtnormalorregenerateschedule" runat="server"  CssClass="dummybutton"></asp:TextBox>

         <asp:TextBox ID="txtDuration" runat="server"  CssClass="dummybutton"></asp:TextBox>
         <asp:TextBox ID="txtDurationMs" runat="server"  CssClass="dummybutton"></asp:TextBox>
         
   
          <asp:TextBox ID="txtSourceSQLID" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtRcnoDetailLog" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtWindowsSVCAutoServiceSchedule" runat="server" Visible="False"></asp:TextBox>


             <asp:TextBox ID="txtbilladdress1session" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtbillstreetsession" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtbillbuildingsession" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtbillcountrysession" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtbillpostalsession" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>

               <asp:TextBox ID="txtIndustrySession" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtMarketSegmentSession" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtContractGroupSession" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtCompanyGroupSession" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtSalesmanSession" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtBillingAdressSession" runat="server" Visible="False"></asp:TextBox>
                                     <asp:TextBox ID="txtLocationSession" runat="server" Visible="False"></asp:TextBox>

           <asp:TextBox ID="txtContactPersonsession" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtConPerMobilesession" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtAccCodesession" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtTelephonesession" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtFaxsession" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtPostalsession" runat="server" Visible="False"></asp:TextBox>
                                     <asp:TextBox ID="txtOfficeAddresssession" runat="server" Visible="False"></asp:TextBox>
           <asp:TextBox ID="ddlLocateGrpsession" runat="server" Visible="False"></asp:TextBox>
                                     <asp:TextBox ID="ddlSalesmansession" runat="server" Visible="False"></asp:TextBox>

          <asp:TextBox ID="txtendoflastschedule" runat="server" Visible="False"></asp:TextBox>


             <%--Confirm Generate Service Schedule--%>
                                              
                 <asp:Panel ID="pnlConfirmSchedule" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEvent" runat="server" Text="Confirm"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                           <asp:Label ID="lblConfirm" runat="server" Text="" Visible="false"></asp:Label>

                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to generate Service Schedule?"></asp:Label>
                          <asp:TextBox ID="txtSchedule" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
 
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:20px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnConfirmNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <cc1:ModalPopupExtender ID="mdlPopupSchedule" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmSchedule" TargetControlID="btndummySchedule" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></cc1:ModalPopupExtender>
         <asp:Button ID="btndummySchedule" runat="server" CssClass="dummybutton" />

             <%--Confirm Generate Service Schedule--%>


             <%--Confirm Months--%>
                                              
                 <asp:Panel ID="pnlConfirmMonths" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label7" runat="server" Text="Confirm"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">

                          &nbsp;<asp:Label ID="Label9" runat="server" Text="Do you want to generate 1 month/s of schedule?"></asp:Label>
 
                      </td>
                 </tr>
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">

                          &nbsp;<asp:Label ID="Label8" runat="server" Text="starting from "></asp:Label>
 
                      </td>
                 </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:20px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmMonthsYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnConfirmMonthsNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <cc1:ModalPopupExtender ID="mdlConfirmMonths" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmMonths" TargetControlID="btnDummyConfirmMonths" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></cc1:ModalPopupExtender>
         <asp:Button ID="btnDummyConfirmMonths" runat="server" CssClass="dummybutton" />

             <%--Confirm Months--%>


           <asp:Panel ID="pnlConfirmMsg" runat="server" BackColor="White" Width="480px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                  <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label5" runat="server" Text="Notification"></asp:Label>
                        
                      </td>
                           </tr>
               <tr>
                             <td colspan="2" class="auto-style3"><br /></td>
                         </tr> 
               <tr>
                      <td class="auto-style4" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label3" runat="server" Text="The system will generate the schedule of this contract"></asp:Label>
                        
                      </td>
                           </tr>
               <tr>
                      <td class="auto-style4" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label4" runat="server" Text="and you will receive an email notification when completed."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2" class="auto-style3"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center" class="auto-style3"><asp:Button ID="btnConfirmOk" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelMsg" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <cc1:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></cc1:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />


        <%-- '14.06.23--%>

               <%--Start: Schedule Date Exists--%>


           <asp:Panel ID="pnlDateExists" runat="server" BackColor="White" Width="480px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                  <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label10" runat="server" Text="Service Schedule already Exists"></asp:Label>
                        
                      </td>
                           </tr>
               <tr>
                             <td colspan="2" class="auto-style3"><br /></td>
                         </tr> 
               <tr>
                      <td class="auto-style4" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label11" runat="server" Text="There exists already Service schedule record later than the"></asp:Label>
                        
                      </td>
                           </tr>
               <tr>
                      <td class="auto-style4" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label12" runat="server" Text=" Service Start Date.. Cannot generate schedule.."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2" class="auto-style3"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center" class="auto-style3"><asp:Button ID="Button1" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="Button2" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <cc1:ModalPopupExtender ID="mdlPopupDateExists" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlDateExists" TargetControlID="btndummyDateExists" BackgroundCssClass="modalBackground" DynamicServicePath="" ></cc1:ModalPopupExtender>
         <asp:Button ID="btndummyDateExists" runat="server" CssClass="dummybutton" />

          <%--End: Schedule Date Exists--%>

        <%-- '14.06.23--%>


           <asp:Panel ID="pnlConfirmDelete" runat="server" BackColor="White" Width="500px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label6" runat="server" Text="Confirm"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                           <asp:Label ID="lblDeleteConfirm" runat="server" Text="" Visible="false"></asp:Label>

                          &nbsp;<asp:Label ID="lblDeleteQuery" runat="server" Text="Are you sure to DELETE the File?"></asp:Label>
                          <asp:TextBox ID="txtDeletePhoto" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
 
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmDelete" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnConfirmDeleteNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <cc1:ModalPopupExtender ID="mdlPopupDelete" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmDelete" TargetControlID="btndummyDelete" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></cc1:ModalPopupExtender>
         <asp:Button ID="btndummyDelete" runat="server" CssClass="dummybutton" />

      <%--<asp:Panel ID="Panel1" runat="server" BackColor="White" Width="700" Height="300" BorderColor="#003366" BorderWidth="1" Visible="true" 
         ScrollBars="None"  style="text-align:center;padding-left:50px;">--%>
      
          
      

            <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White"></asp:TextBox>
            <asp:TextBox ID="txtFromForm" runat="server" Visible="False"></asp:TextBox>
      
          
      

   <br /> 
            </ContentTemplate>
                     </asp:UpdatePanel>
        <div style="text-align:center">
       <asp:LinkButton ID="btnTop" runat="server" Font-Names="Calibri" Font-Bold="True" ForeColor="Brown">Go to Top</asp:LinkButton>

           </div>

      </asp:Content>
