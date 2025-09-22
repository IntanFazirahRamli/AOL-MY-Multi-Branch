<%@ Page Language="vb" AutoEventWireup="false" CodeFile="ContractDetail.aspx.vb" Inherits="ContractDetail" MasterPageFile="~/MasterPage.Master" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            font-family: 'Calibiri';
            color: black;
            text-align: right;
        }
       
       
       
        .auto-style1 {
            width: 161px;
        }
       
       
       
        </style>
       <script type="text/javascript">
      
      function ClearTextBox() {
          document.getElementById("<%=txtClient.ClientID%>").value = '';
    }

     

      function stopRKey(evt) {
         var evt = (evt) ? evt : ((event) ? event : null);
          var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
          if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
      }

      document.onkeypress = stopRKey;


      //function disableMe(me) {
      //    //me.disabled = true;
      //    //alert("1");
      //    //return true;
          
        
      //}

      function btn_disable()
      {
        
          document.getElementById("<%=btnBack.ClientID%>").disabled = true;
          document.getElementById("<%=btnADD.ClientID%>").disabled = true;
          document.getElementById("<%=btnSave.ClientID%>").disabled = true;
          document.getElementById("<%=btnDelete.ClientID%>").disabled = true;
          document.getElementById("<%=btnQuit.ClientID%>").disabled = true;
          return true;
      }

           function btn_disable1() {

               var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
               var valid = true;

               var freq = document.getElementById("<%=txtFrequency.ClientID%>").value;
            if (freq == '') {
                alert("Please Select Frequency");
                valid = false;
                return valid;

            }

            var servid = document.getElementById("<%=txtServiceId.ClientID%>").value;
            if (servid == '') {
                alert("Please Select Service");
                valid = false;
                return valid;

            }

            var duptarget = document.getElementById("<%=txtDuplicateTarget.ClientID%>").value;

            if (duptarget == 'Y') {
                alert("Duplicate Target has been Selected");
                valid = false;
                return valid;

            }
               document.getElementById("<%=btnBack.ClientID%>").disabled = true;
               document.getElementById("<%=btnADD.ClientID%>").disabled = true;
               document.getElementById("<%=btnDelete.ClientID%>").disabled = true;
               document.getElementById("<%=btnQuit.ClientID%>").disabled = true;
               document.getElementById("<%=btnServiceSchedule.ClientID%>").disabled = true;
              
               //return true;
           }

        function DoValidation(parameter) {
            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;
         
            var freq = document.getElementById("<%=txtFrequency.ClientID%>").value;
            if (freq == '') {
                alert("Please Select Frequency");
                valid = false;
                return valid;

            }

            var freq = document.getElementById("<%=txtServiceId.ClientID%>").value;
            if (freq == '') {
                alert("Please Select Service");
                valid = false;
                return valid;

            }

            var duptarget = document.getElementById("<%=txtDuplicateTarget.ClientID%>").value;
            
            if (duptarget == 'Y') {
                alert("Duplicate Target has been Selected");
                valid = false;
                return valid;

            }

            //if (str != "" && regex.test(str)) {
            //    valid = true;
            //}
            //else {
            //    valid = false;
            //    alert("Enter Only Numbers for Agreed Value");
            //}

            //if (str1 != "" && isNaN(str1)) {
            //    alert("Enter Only Numbers for Allocated Time");
            //    valid = false;
            //}

            //if (str2 != "" && isNaN(str2)) {
            //    alert("Enter Only Numbers for Value per month");
            //    valid = false;
            //}
            //if (str3 != "" && isNaN(str3)) {
            //    alert("Enter Only Numbers for Duration");
            //    valid = false;
            //}

            //if (str4 != "" && isNaN(str4)) {
            //    alert("Enter Only Numbers for No. of Services Tot. Amt.");
            //    valid = false;
            //}
            //if (str5 != "" && isNaN(str5)) {
            //    alert("Enter Only Numbers for No. of Services Tot. Services");
            //    valid = false;
            //}
            //if (str6 != "" && isNaN(str6)) {
            //    alert("Enter Only Numbers for No. of Services Completed");
            //    valid = false;
            //}
            //if (str7 != "" && isNaN(str7)) {
            //    alert("Enter Only Numbers for No. of Services Balance");
            //    valid = false;
            //}

            //if (str8 != "" && isNaN(str8)) {
            //    alert("Enter Only Numbers for No. of Hours Tot. Amt.");
            //    valid = false;
            //}
            //if (str9 != "" && isNaN(str9)) {
            //    alert("Enter Only Numbers for No. of Hours Tot. Services");
            //    valid = false;
            //}
            //if (str10 != "" && isNaN(str10)) {
            //    alert("Enter Only Numbers for No. of Hours Service Completed");
            //    valid = false;
            //}
            //if (str11 != "" && isNaN(str11)) {
            //    alert("Enter Only Numbers for No. of Hours Service Balance");
            //    valid = false;
            //}

            //if (str12 != "" && isNaN(str12)) {
            //    alert("Enter Only Numbers for No. of Phone Calls Tot. Amt.");
            //    valid = false;
            //}
            //if (str13 != "" && isNaN(str13)) {
            //    alert("Enter Only Numbers for No. of Phone Calls Tot. Services");
            //    valid = false;
            //}
            //if (str14 != "" && isNaN(str14)) {
            //    alert("Enter Only Numbers for No. of Phone Calls Service Completed");
            //    valid = false;
            //}
            //if (str15 != "" && isNaN(str15)) {
            //    alert("Enter Only Numbers for No. of Phone Calls Service Balance");
            //    valid = false;
            //}

            //if (str16 != "" && isNaN(str16)) {
            //    alert("Enter Only Numbers for No. of Units Tot. Amt.");
            //    valid = false;
            //}
            //if (str17 != "" && isNaN(str17)) {
            //    alert("Enter Only Numbers for No. of Units Tot. Services");
            //    valid = false;
            //}
            //if (str18 != "" && isNaN(str18)) {
            //    alert("Enter Only Numbers for No. of Units Service Completed");
            //    valid = false;
            //}
            //if (str19 != "" && isNaN(str19)) {
            //    alert("Enter Only Numbers for No. of Units Service Balance");
            //    valid = false;
            //}

            //if (str20 != "" && isNaN(str20)) {
            //    alert("Enter Only Numbers for Unexpired Compensation Balance Tot. Service");
            //    valid = false;
            //}
            //if (str21 != "" && isNaN(str21)) {
            //    alert("Enter Only Numbers for Unexpired Compensation Balance");
            //    valid = false;
            //}
          
            //if (str22 != "" && isNaN(str22)) {
            //    alert("Enter Only Numbers for Response");
            //    valid = false;
            //}

            //if (str23 != "" && isNaN(str23)) {
            //    alert("Enter Only Numbers for Cancel Charges");
            //    valid = false;
            //}
            //if (str24 != "" && isNaN(str24)) {
            //    alert("Enter Only Numbers for Min. Duration");
            //    valid = false;
            //}

            //if (str27 != "" && isNaN(str27)) {
            //    alert("Enter Only Numbers for Quote Price");
            //    valid = false;
            //}

            //if (str28 != "" && isNaN(str28)) {
            //    alert("Enter Only Numbers for Contract Det. Value");
            //    valid = false;
            //}
            //if (str29 != "" && isNaN(str29)) {
            //    alert("Enter Only Numbers for Per Service Value");
            //    valid = false;
            //}

            //if (str25 != "" && isNaN(str25)) {
            //    alert("Enter Only Numbers for Discount Percent");
            //    valid = false;
            //}

            //if (str26 != "" && isNaN(str26)) {
            //    alert("Enter Only Numbers for Discount Amount");
            //    valid = false;
            //}

            //if (str30 != "" && isNaN(str30)) {
            //    alert("Enter Only Numbers for Total Contract Value");
            //    valid = false;
            //}


       


            return valid;
        };


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
        <cc1:ControlBundle Name="CalendarExtender_Bundle" />
           <cc1:controlBundle name="ModalPopupExtender_Bundle"/>
              <cc1:controlBundle name="MaskedEditExtender_Bundle"/>
                <cc1:controlBundle name="TabContainer_Bundle"/>
   </ControlBundles>
    </cc1:ToolkitScriptManager>     


    <asp:updateprogress id="UpdateProgress1" runat="server" associatedupdatepanelid="UpdatePanel1"  
    clientidmode="Predictable" viewstatemode="Inherit" DynamicLayout="False">    
    <ProgressTemplate>    
        <div class="div1" style="margin-left: 160px; text-align:center">    
           <asp:Label ID="Label10" runat="server" Font-Size="14px" ForeColor="Red" Text="In Progress... Please Wait"></asp:Label>    
        </div>    
    </ProgressTemplate>    
</asp:updateprogress>  



    <div>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>


      <div style="text-align:center">
         
       
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099; text-align:center">Contract Detail</h3>
        
        <table style="width:100%;text-align:center;">
         <tr>
              <td colspan="8" style="width:100%; text-align: right;color:brown;font-size:15px;font-weight:bold;font-family:'Calibiri';"> 
                                    <asp:Label ID="txtMode" runat="server" Text=""></asp:Label>
        </td>
            </tr>      
            <tr>
                <td style="width:80%;text-align:center;" colspan="8">
                     <asp:Button ID="btnBack" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="BACK" Width="10%" />
                     <asp:Button ID="btnADD" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="10%" />
                    <asp:Button ID="btnSave" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="10%"  OnClientClick="return btn_disable1()"/>
                    <asp:Button ID="btnDelete" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="10%" OnClientClick = "Confirm()"/>
                    <asp:Button ID="Button4" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="RESET" Width="10%" Visible="false" />
                    <asp:Button ID="btnCancel" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="RESET" Width="10%" Visible="False" />
                    <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="10%" />
           <asp:Button ID="btnServiceSchedule" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SCHEDULE SERVICE" Width="13%"  OnClientClick="btn_disable()" /></td>
                   <asp:Label ID="Label1" Style="color: white" runat="server" Visible="false" Text="Label"></asp:Label>    
                </td>    

            </tr>
            <tr>
              <td colspan="8"><br /></td>
            </tr>
               <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:25px;" colspan="8">
                      <asp:Panel ID="Panel5" runat="server" BackColor="#F8F8F8"   BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Width="100%" Height="90%"  HorizontalAlign="Center">
                              
                     <table style="width:100%;text-align:center;padding-left:10px;padding-top:5px; ">
                                
                         <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" class="auto-style2">Contract No
                             </td>
                           <td style="text-align:left; width:10%;"  >   
                                <asp:TextBox ID="txtContractNo" runat="server" MaxLength="50" Height="16px" Width="96%" AutoCompleteType="Disabled" BackColor="#FFC6DA"></asp:TextBox>
                           </td>
                           <td style="width:3%;font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;"></td>
                             <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">Contract Date </td>
                           <td style="text-align:left;width:10%">  <asp:TextBox ID="txtContractDate" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" BackColor="#FFC6DA"></asp:TextBox>
                               
                         </td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left; width:10%" >
                                 Agreed Value</td>
                           <td style="text-align:left;width:15%; ">  
                                <asp:TextBox ID="txtAgreedValue" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Width="85%" style="text-align:right" ReadOnly="True"></asp:TextBox>
                             </td>
                           <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                               <asp:TextBox ID="txtDiscAmt" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Visible="False" Width="90px"></asp:TextBox>
                             </td>
                           <td style="width:10%; text-align: left;">        
                               
                             </td>
                        </tr>

                            <tr>
                            <td style="font-size:14px;width:10%;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >Contact Type
                             </td>
                           <td style="text-align:left; width:10%;"  >     
                               <asp:TextBox ID="txtContactType" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" Width="96%" BackColor="#FFC6DA"></asp:TextBox>
                                </td>
                           <td style="width:3%;font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;"></td>
                           <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">Client </td>
                           <td style="text-align:left;width:150px">   <asp:TextBox ID="txtClient" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" BackColor="#FFC6DA"></asp:TextBox>
                                </td>
                          
                           <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >Name
                               </td>
                           <td colspan="3" style="text-align:left;">     <asp:TextBox ID="txtCustName" runat="server" Height="16px" Width="92%" AutoCompleteType="Disabled" BackColor="#FFC6DA"></asp:TextBox> </td>
                        </tr>


                           <tr>
                            <td style="font-size:14px;width:10%;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >Contact</td>
                           <td style="text-align:left;" class="auto-style1" > 
                               <asp:TextBox ID="txtContact" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" MaxLength="50" Width="96%"></asp:TextBox>
                                   
                                       </td>
                           <td style="width:3%;font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                              
                               </td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">Service Start</td>
                                <td style="text-align:left;width:8%"> 
                                  
                                    <asp:TextBox ID="txtServStart" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Width="80%"></asp:TextBox>
                                  
                                  
                               </td>
                               
                                      <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">Service End</td>
                           <td style="text-align:left;width:150px">
                               <asp:TextBox ID="txtServEnd" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Width="85%"></asp:TextBox>
                              
                               </td>
                                 <td c style="text-align: left">&nbsp;
                                    <asp:TextBox ID="txtMonthByWhichMonth" runat="server" AutoCompleteType="Disabled" Height="16px" Width="44px" Visible="False"></asp:TextBox>
                               </td> 
                                <td c style="text-align: left">&nbsp;
                                           <asp:TextBox ID="txtDOWByWhichWeek" runat="server" AutoCompleteType="Disabled" Height="16px" Width="44px" Visible="False"></asp:TextBox>
                             </td> 

                           </tr>

                                           
                           <tr>
                             <td  style="font-size:14px;font-weight:bold; width:10%;font-family:'Calibiri';color:black;text-align:left;">Frequency
                                <asp:Label ID="Label4" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                             </td>
                             <td  style="text-align:left; color: #FF0000;" class="auto-style1">
                                     <asp:TextBox ID="txtFrequency" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%" ReadOnly="True"></asp:TextBox>
                                        
                                 </td>
                             <td style="font-size:14px;font-weight:bold;width:3%;font-family:'Calibiri';color:black;text-align:left; ">
                                 <asp:ImageButton ID="imgBtnTeam" runat="server" CssClass="righttextbox" Height="22" ImageUrl="~/Images/searchbutton.jpg" Width="24" />
                                            <cc1:ModalPopupExtender ID="mdlPopUpTeam" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlTeamClose" PopupControlID="pnlPopUpTeam" TargetControlID="imgBtnTeam">
                                            </cc1:ModalPopupExtender>
                             </td>
                             <td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                                 <asp:TextBox ID="txtFrequencyDesc" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Width="88%" AutoPostBack="True"></asp:TextBox>
                                        </td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >No. of Interval</td>
                             <td style="text-align:left;width:150px">
                                    <asp:TextBox ID="txtNoofInterval" runat="server" AutoCompleteType="Disabled" Height="16px" Width="85%" BackColor="#FFC6DA" ReadOnly="True" style="text-align:right"></asp:TextBox>
                                     <asp:Label ID="lblInterval" runat="server" Text=" " ForeColor="Black"></asp:Label>
                                       
                                     </td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;width:15%" >No. of Service/Interval</td>
                             <td style="text-align: left">
                                              <asp:TextBox ID="txtNoofSvcInterval" runat="server" AutoCompleteType="Disabled" Height="16px" Width="70%" style="text-align:right"></asp:TextBox>
                           </td>
                         </tr>




                         <tr>
                             <td  style="font-size:14px;font-weight:bold; width:10%;font-family:'Calibiri';color:black;text-align:left;">Service Id
                                <asp:Label ID="Label3" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                             </td>
                             <td  style="text-align:left; color: #FF0000;" class="auto-style1">
                                 <asp:TextBox ID="txtServiceId" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%" ReadOnly="True"></asp:TextBox>
                                 </td>
                             <td style="font-size:14px;font-weight:bold;width:3%;font-family:'Calibiri';color:black;text-align:left;">
                                 <asp:ImageButton ID="imgBtnService" runat="server" CssClass="righttextbox" Height="22" ImageUrl="~/Images/searchbutton.jpg" Width="24" />
                                 <cc1:ModalPopupExtender ID="imgBtnService_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlServiceClose" PopupControlID="pnlPopUpService" TargetControlID="imgBtnService">
                                 </cc1:ModalPopupExtender>
                             </td>
                             <td colspan="2" style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                                 <asp:TextBox ID="txtServiceDesc" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Width="88%"></asp:TextBox>
                             </td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >Total No. Service</td>
                             <td style="text-align:left;width:150px">
                                 <asp:TextBox ID="txtNoService" runat="server" AutoCompleteType="Disabled" Height="16px" Width="85%" BackColor="#FFC6DA" ReadOnly="True" style="text-align:right"></asp:TextBox>
                             </td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >Value / Service</td>
                             <td style="text-align: left">
                                 <asp:TextBox ID="txtValuePerService" runat="server" AutoCompleteType="Disabled" Height="16px" Width="70%" style="text-align:right" ></asp:TextBox>
                             </td>
                         </tr>

                          <tr>
                             <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">Service Notes</td>
                             <td  colspan="4" style="text-align:left;">
                                 <asp:TextBox ID="txtServiceNotes" runat="server" AutoCompleteType="Disabled" Height="16px" Width="93%"></asp:TextBox>
                             </td>
                             <td  style="text-align: left">
                                 <asp:TextBox ID="txtBillingAmount" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Width="80%" Visible="False"></asp:TextBox>
                              </td>
                              <td  style="text-align: left">
                                  <asp:TextBox ID="txtRecordAdded" runat="server" AutoCompleteType="Disabled" Height="16px" Width="44px" Visible="False"></asp:TextBox>
                              </td>
                              <td  style="text-align: left">
                                  <asp:TextBox ID="txtStatus" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="44px"></asp:TextBox>
                              </td>
                              <td  style="text-align: left">&nbsp;</td>

                         </tr>
                        
                         <tr>
                             <td  style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;"></td>
                             <td style="text-align:left;" class="auto-style1">
                                 <asp:TextBox ID="txtDuplicateTarget" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="44px"></asp:TextBox>
                             </td>
                             <td style="font-size:14px;width:3%;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                                 &nbsp;</td>
                             <td style="font-size:14px;width:10%;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                             </td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >
                                 <asp:TextBox ID="txtValue" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="40%"></asp:TextBox>
                             </td>
                             <td style="text-align:left;width:150px">
                                 <asp:TextBox ID="txtOrigCode" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="44px"></asp:TextBox>
                             </td>
                             <td  style="text-align: left">
                                 
                                 <asp:TextBox ID="txtSourceSQLID" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="91px"></asp:TextBox>
                                 
                             </td>

                             <td style="text-align: left">
                                 <asp:TextBox ID="txtServiceDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="60px" Visible="False"></asp:TextBox>
                             </td>
                               <td style="font-size:14px;width:5%;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                                 &nbsp;</td>
                         </tr>
                        

                            <tr>
                             <td  style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">Frequency</td>
                             <td style="text-align:left;">
                                 <asp:TextBox ID="txtTargetDesc" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%" Visible="False"></asp:TextBox>
                                </td>
                             <td style="font-size:14px;width:3%;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                                 &nbsp;</td>
                             <td  style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                             </td>
                                  <td style="font-size:14px;width:5%;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;">
                                 &nbsp;</td>
                             <td style="font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >
                                 Targets</td>
                             <td style="text-align:left;width:150px">
                                 <asp:TextBox ID="txtRecordDeleted" runat="server" AutoCompleteType="Disabled" Height="16px" Width="44px" Visible="False"></asp:TextBox>
                             </td>
                             <td  style="text-align: left">
                                
                                 <asp:TextBox ID="txtBillingFreq" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Width="80%" Visible="False"></asp:TextBox>
                                
                             </td>

                             <td style="text-align: left">
                             </td>
                         </tr>
                       
                         
                         <tr> <td colspan="8"> </td></tr>
                       
                                 <tr>
                                       
                                        <td colspan="5" style:="" valign="top" style="text-align:center;padding-left:30px;">
                                             <asp:UpdatePanel ID="updpnlFreq" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvFreqDetails" runat="server" RowStyle-Height="17px"
                                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" 
                                                GridLines="None" CssClass="table_head_bdr "
                                                ShowFooter="True" Style="text-align: left" Width="45%" Height="20px"
                                                onrowdatabound="grvFreqDetails_RowDataBound" 
                                                OnRowDeleting="grvFreqDetails_RowDeleting"  >
                                            
                                                <Columns>

                                                    <asp:TemplateField  HeaderText="Seq. No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSeqNoGV" runat="server" 
                                                                Enabled="false" Height="15px" ReadOnly="true"  Width="60px" Style="text-align: center"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Freq MTD">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlFreqMTDGV" runat="server" AutoPostBack="True" 
                                                                DataTextField="FreMTD" DataValueField="FreMTD" Height="20px" AppendDataBoundItems="True"
                                                                 width="100px" onselectedindexchanged="ddlFreqMTDGV_SelectedIndexChanged">
                                                                <asp:ListItem Text="--Select--" Value="-1" />
                                                                <asp:ListItem>DAY</asp:ListItem>
                                                                 <asp:ListItem>DOW</asp:ListItem>
                                                                 <asp:ListItem>MONTH</asp:ListItem>
                                                                 <asp:ListItem>DATE</asp:ListItem>
                                                            </asp:DropDownList>
                                                           

                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField  HeaderText="Month No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMonthNoGV" runat="server" 
                                                                Enabled="false" Height="15px" AutoPostBack="True"  OnTextChanged="txtMonthNoGV_TextChanged" Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                      <asp:TemplateField  HeaderText="Day No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDayNoGV" runat="server" 
                                                                Enabled="false" Height="15px" AutoPostBack="true"   OnTextChanged="txtDayNoGV_TextChanged"  Width="65px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                       <asp:TemplateField  HeaderText="Week No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtWeekNoGV" runat="server" AutoPostBack="True"  OnTextChanged="txtWeekNoGV_TextChanged"
                                                                Enabled="false" Height="15px"   Width="75px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                     <asp:TemplateField HeaderText="Day of Week">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlWeekDOWGV" runat="server" AutoPostBack="True" 
                                                                DataTextField="WeekDOW" DataValueField="WeekDOW" Height="20px" onselectedindexchanged="ddlWeekDOWGV_SelectedIndexChanged" AppendDataBoundItems="True"
                                                                 width="100px" >
                                                                <asp:ListItem Text="--Select--" Value="-1" />
                                                                 <asp:ListItem>Monday</asp:ListItem>
                                                                 <asp:ListItem>Tuesday</asp:ListItem>
                                                                 <asp:ListItem>Wednesday</asp:ListItem>
                                                                 <asp:ListItem>Thursday</asp:ListItem>
                                                                 <asp:ListItem>Friday</asp:ListItem>
                                                                 <asp:ListItem>Saturday</asp:ListItem>
                                                                 <asp:ListItem style="color:red">Sunday</asp:ListItem>
                                                                 
                                                            </asp:DropDownList>                                                          
                                                                                                                    
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Location" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlLocationGV" runat="server" AutoPostBack="True" Visible="false" 
                                                                DataTextField="Location" DataValueField="Location" Height="20px" AppendDataBoundItems="True"
                                                                 width="150px" >
                                                                <asp:ListItem Text="--Select--" Value="-1" />
                                                            </asp:DropDownList>                                                         
                                                                                                                   
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                      <asp:TemplateField HeaderText="Branch ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlBranchIDGV" runat="server" AutoPostBack="True" Visible="false"
                                                                DataTextField="BranchID" DataValueField="BranchID" Height="20px" AppendDataBoundItems="True"
                                                                 width="150px" >
                                                                <asp:ListItem Text="--Select--" Value="-1" />
                                                            </asp:DropDownList>                                                         
                                                                                                                   
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                 <ItemTemplate>
                                                     <asp:TextBox ID="txtContractNoGVF" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                                  
                                                      <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSourceSQLIDGVF" runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     
                                                   
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRcnoGVF"  runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:CommandField ButtonType="Image"  Visible="false"    
                                                        DeleteImageUrl="~/Images/delete_icon_color.gif" ItemStyle-Height="15px" 
                                                        ShowDeleteButton="True">
                                                        <ItemStyle Height="15px" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddDetail" runat="server" OnClick="btnAddDetail_Click" 
                                                                Text="Add New Row" ValidationGroup="VGroup" Visible="false" />
                                                        </FooterTemplate>
                                                        <ItemStyle ForeColor="#507CD1" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#0099FF" Font-Bold="True" ForeColor="White" 
                                                    Height="5px" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#0099FF" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                                 </ContentTemplate>
                                        
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="grvFreqDetails" EventName="SelectedIndexChanged" />
                                         </Triggers>
                                        
                                     </asp:UpdatePanel>
                                        </td>

                                      <td colspan="3" style:="" valign="top" style="text-align:center;padding-left:30px;">
                                           <asp:UpdatePanel ID="updpanelTarget" runat="server">
                                                  <ContentTemplate>
                                                      <asp:GridView ID="grvTargetDetails" runat="server" AllowSorting="True" 
                                                          AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
                                                          GridLines="None" Height="17px" 
                                                          onpageindexchanging="grvTargetDetails_PageIndexChanging" 
                                                          onrowdatabound="grvTargetDetails_RowDataBound" 
                                                          OnRowDeleting="grvTargetDetails_RowDeleting" 
                                                          RowStyle-Height="17px" ShowFooter="True" Style="text-align: left" Width="50%">
                                                          <Columns>
                                                              <asp:TemplateField HeaderText="Target Description">
                                                                  <ItemTemplate>
                                                                      <asp:DropDownList ID="ddlTargetDescGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataTextField="Descrip1" DataValueField="Descrip1" Height="19px" onselectedindexchanged="ddlTargetDescGV_SelectedIndexChanged" width="250px">
                                                                          <asp:ListItem Text="--Select--" Value="-1" />
                                                                      </asp:DropDownList>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Target ID">
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtTargtIdGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="75px"></asp:TextBox>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtContractNoGV" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtServiceIDGV" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtSourceSQLIDGV" runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtRcnoGV" runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ItemStyle-Height="15px" ShowDeleteButton="True" FooterStyle-VerticalAlign="Top">
                                                              <ItemStyle Height="15px" />
                                                              </asp:CommandField>
                                                              <asp:TemplateField>
                                                                  <FooterStyle HorizontalAlign="Left" />
                                                                  <FooterTemplate>
                                                                      <asp:Button ID="btnAddDetail" runat="server" OnClick="btnAddDetail_Click" Text="Add New Row" ValidationGroup="VGroup" Visible="false" />
                                                                  </FooterTemplate>
                                                                  <ItemStyle ForeColor="#507CD1" />
                                                              </asp:TemplateField>
                                                          </Columns>
                                                          <FooterStyle BackColor="#0099FF" Font-Bold="True" ForeColor="White" Height="5px" />
                                                          <RowStyle BackColor="#EFF3FB" />
                                                          <EditRowStyle BackColor="#2461BF" />
                                                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                          <HeaderStyle BackColor="#0099FF" Font-Bold="True" ForeColor="White" />
                                                          <AlternatingRowStyle BackColor="White" />
                                                      </asp:GridView>
                                                  </ContentTemplate>
                                                  <Triggers>
                                                      <asp:AsyncPostBackTrigger ControlID="grvTargetDetails" EventName="SelectedIndexChanged" />
                                                  </Triggers>
                                              </asp:UpdatePanel>
                                          '''''''''''''
                                          </td>
                                    </tr>
                            
                                    </table>
                               
                                
                          </asp:Panel>
                    </td>
                   </tr>




            <tr>
                <td class="auto-style1">

                    <br />
                </td>
                <td class="auto-style1">

                    &nbsp;</td>
                <td class="auto-style1">

                    &nbsp;</td>
                <td class="auto-style1">

                    </td>
                 <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >Total Records

                </td>
                <td  style="font-size:14px; width:10%;font-weight:bold;font-family:'Calibiri';color:black;text-align:left;" >
                       <asp:TextBox ID="txtTotalRecords" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" Width="59%" style="text-align:center;" ReadOnly="True" ></asp:TextBox>
              
                </td>
                <td class="auto-style1">

                </td>
                <td class="auto-style1">

                    &nbsp;</td>
                <tr style="text-align:center;">
                    <td colspan="8" style="width:100%;text-align:center; padding-left:30px;">
                        <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Vertical" Visible="true" Width="100%">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="ContractNo" DataSourceID="SqlDSContractDet" Font-Size="12pt" HorizontalAlign="Center">
                                <Columns>
                                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                                    <ControlStyle Width="50px" />
                                    <ItemStyle Width="50px" Wrap="False" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="Frequency" HeaderText="Frequency" SortExpression="Frequency" />
                                    <asp:BoundField DataField="ServiceID" HeaderText="ServiceID" />
                             
                                    <asp:BoundField DataField="ServiceDesc" HeaderText="Service Description" SortExpression="ServiceDesc" />
                                    <asp:BoundField DataField="TargetDesc" HeaderText="Target Description" SortExpression="TargetDesc" />
                              
                                    <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                                    <asp:BoundField DataField="NoService" HeaderText="No. Service" SortExpression="NoService" />
                                    <asp:BoundField DataField="NoOfSvcInterval" HeaderText="No. of Svc Interval" SortExpression="NoOfSvcInterval" />
                                    <asp:BoundField DataField="NoOfInterval" HeaderText="No. of Interval" SortExpression="NoOfInterval" />
                                    <asp:BoundField DataField="NoServiceCompleted" HeaderText="No. of Service Completed" SortExpression="NoServiceCompleted" Visible="False" />
                                    <asp:BoundField DataField="Rcno" HeaderText="Rcno" InsertVisible="False" ReadOnly="True" SortExpression="Rcno" Visible="False" />
                                    <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ContractNo" HeaderText="ContractNo" SortExpression="ContractNo" Visible="False" />
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#e4e4e4" />
                                <SortedAscendingHeaderStyle BackColor="#000066" />
                                <SortedDescendingCellStyle BackColor="#e4e4e4" />
                                <SortedDescendingHeaderStyle BackColor="#000066" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </tr>




            </table>
          </div>

      <asp:SqlDataSource ID="SqlDSContractDet" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * From tblContractDet where (Rcno &lt;&gt; 0)   and ContractNo =@ContractNo  order by ServiceId limit 10">
          <SelectParameters>
              <asp:ControlParameter ControlID="txtContractNo" Name="@ContractNo" PropertyName="Text" />
          </SelectParameters>
    </asp:SqlDataSource>
    <br />

    
    <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>


   
      <%--<asp:Panel ID="Panel1" runat="server" BackColor="White" Width="700" Height="300" BorderColor="#003366" BorderWidth="1" Visible="true" 
         ScrollBars="None"  style="text-align:center;padding-left:50px;">--%>
      
          
      

   <br /> 
         <br />
         <br />
      <asp:Panel ID="pnlPopUpTeam" runat="server" BackColor="White" Width="40%" Height="700" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">

        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Select Service Frequency</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

        
        <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvTeam" runat="server" DataSourceID="SqlDSTeam" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="40%" RowStyle-HorizontalAlign="Left" PageSize="8" DataKeyNames="Rcno" HorizontalAlign="Center" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="Frequency" HeaderText="Frequency" SortExpression="Frequency" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MonthByWhichMonth" SortExpression="MonthByWhichMonth"  >
                    <ItemStyle ForeColor="#F7F6F3" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DOWByWhichWeek" SortExpression="DOWByWhichWeek" >
                    <ItemStyle ForeColor="#F7F6F3" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoService" HeaderText="NoService" SortExpression="NoService" Visible="False" />
                    <asp:BoundField DataField="NoDays" HeaderText="NoDays" SortExpression="NoDays" Visible="False" />
                    <asp:BoundField DataField="NoOfWks" HeaderText="NoOfWks" SortExpression="NoOfWks" Visible="False" />
                    <asp:BoundField DataField="NoOfMths" HeaderText="NoOfMths" SortExpression="NoOfMths" Visible="False" />
                    <asp:BoundField DataField="NoOfYears" HeaderText="NoOfYears" SortExpression="NoOfYears" Visible="False" />
                    <asp:BoundField DataField="MaxNoDaySvs" HeaderText="MaxNoDaySvs" SortExpression="MaxNoDaySvs" Visible="False" />
                    <asp:BoundField DataField="MaxNoWeekSvs" HeaderText="MaxNoWeekSvs" SortExpression="MaxNoWeekSvs" Visible="False" />
                    <asp:BoundField DataField="MaxNoSvsInterval" HeaderText="MaxNoSvsInterval" SortExpression="MaxNoSvsInterval" Visible="False" />
                    <asp:BoundField DataField="ByDOW" HeaderText="ByDOW" SortExpression="ByDOW" Visible="False" />
                    <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" Visible="False" />
                    <asp:BoundField DataField="Rcno" HeaderText="Rcno" InsertVisible="False" ReadOnly="True" SortExpression="Rcno" Visible="False" />
                    <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                    <asp:BoundField DataField="ByDate" HeaderText="ByDate" SortExpression="ByDate" Visible="False" />
                    <asp:BoundField DataField="FreqMtd" HeaderText="FreqMtd" SortExpression="FreqMtd" Visible="False" />
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
            <asp:SqlDataSource ID="SqlDSTeam" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT * from tblServiceFrequency where (Rcno &lt;&gt; 0) order by Frequency"></asp:SqlDataSource>
        </div>
    </asp:Panel>
          
    
    
      <asp:Panel ID="pnlPopUpService" runat="server" BackColor="White" Width="1000" Height="700" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">

        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Select Service</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

     
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvService" runat="server" DataSourceID="SqlDSService" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="900px" RowStyle-HorizontalAlign="Left" PageSize="8" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" />
                    <asp:BoundField DataField="ProductDesc" HeaderText="ProductDesc" />
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
            <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT * from tblProduct where (Rcno &lt;&gt; 0)  order by ProductId"></asp:SqlDataSource>
        </div>
    </asp:Panel>



    
      <asp:Panel ID="pnlPopUpContact" runat="server" BackColor="White" Width="1000" Height="700" BorderColor="#003366" BorderWidth="1" Visible="false"
        HorizontalAlign="Left" ScrollBars="None" >

        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">&nbsp;</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" Visible="False" /></td>
            </tr>
        </table>

        <div class="wrp" >
            <div class="frm">
                <table>
                    <tr>
                        <td class="lbl">&nbsp;</td>
                        <td style="text-align: left;">
                            &nbsp;</td>
                        <td style="text-align: right;">
                            &nbsp;</td>
                        <td style="text-align: right;">
                            &nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px; ">
            <div class="AlphabetPager">
            </div>
            <br />

        </div>
          
    </asp:Panel>

                </ContentTemplate>
                     </asp:UpdatePanel>
            </div>
    
    </asp:Content>
