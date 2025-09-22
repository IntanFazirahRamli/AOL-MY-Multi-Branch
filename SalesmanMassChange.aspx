<%@ Page Title="Mass Change of Salesman" Language="vb" AutoEventWireup="false" CodeFile="SalesmanMassChange.aspx.vb" Inherits="SalesmanMassChange" MasterPageFile="~/MasterPage.Master" Culture="en-GB" %>



<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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

           
        </style>
       <script type="text/javascript">



           function stopRKey(evt) {
               var evt = (evt) ? evt : ((event) ? event : null);
               var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
               if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
           }

           document.onkeypress = stopRKey;

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

           var defaultTextStaff = "Search Here for Staff";
           function WaterMarkStaff(txt, evt) {
               if (txt.value.length == 0 && evt.type == "blur") {
                   txt.style.color = "gray";
                   txt.value = defaultTextStaff;
               }
               if (txt.value == defaultTextStaff && evt.type == "focus") {
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

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>

     <asp:UpdatePanel ID="updPanelMassChangeSalesman" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
           <asp:controlBundle name="ModalPopupExtender_Bundle"/>
              <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>
   </ControlBundles>
    </asp:ToolkitScriptManager>     




    <div>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
     <ContentTemplate>


      <div style="text-align:center">
         
       
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099; text-align:center">Mass Change of Salesman</h3>
        
        <table style="width:100%;text-align:center;">
         
          
            <tr>
              <td style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri'; color: brown;">
                  <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
          
            <tr>
              <td style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';">
                  <asp:Label ID="lblAlert" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                </td>
            </tr>
              <tr>
              <td colspan="8" style="width:100%;text-align:right;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                                    <asp:Button ID="btnQuit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="QUIT" Width="100px" />
        </td>
            </tr>    
               <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:5px;" colspan="8">
               
                    
                           <asp:Panel ID="Panel5" runat="server" BackColor="#F8F8F8"   BorderColor="gray" BorderWidth="1" BorderStyle="Solid" Width="100%" Height="90%"  HorizontalAlign="Center">
                              
                     <table border="0" style="width:100%;text-align:center;padding-left:10px;padding-top:0px; padding-bottom:2px; margin-bottom:2px; ">
                                
                           <tr>
                                        <td style="width: 9%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;" >Current Salesman </td>
                                           <td style="text-align: left; width:15%" >
                                               <asp:DropDownList ID="ddlOldSalesman" runat="server" AppendDataBoundItems="True" Height="22px" Width="95%" AutoPostBack="True">
                                                 <asp:ListItem>--SELECT--</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                        <td style="width: 9%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">New Salesman</td>
                                       <td style="text-align: left; width:15%" >
                                            <asp:DropDownList ID="ddlNewSalesman" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Frequency" DataValueField="Frequency" Height="25px" Value="-1" Width="95%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>     
                                       </td>

                             
                              
                                        <td style="width: 9%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right; padding-left:2%">Effective Date
                                         
                                         
                                        </td>
                                       <td style="text-align: left; width:12%" >
                                            <asp:TextBox ID="txtEffectiveDate" runat="server" MaxLength="50" Height="16px" Width="150px"
                                                AutoCompleteType="Disabled" AutoPostBack="True"></asp:TextBox>

                                            <asp:CalendarExtender ID="calTo" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToDate" TargetControlID="txtEffectiveDate" />
                                        </td>

                                        <td style="width: 6%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left:1%">
                                            
                                            &nbsp;</td>

                                  <td style="width:1%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right; padding-left:0%">
                                             <asp:Button ID="btnDummyClient" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="5px" />
                                           <asp:Button ID="btnDummyContract" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="5px" />
                                        </td>
                                      <td style="width: 6%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">
                                          <asp:Button ID="btnGo" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SEARCH" Width="100px" />
                                        </td>
                                        <td style="text-align: left; width: 10%">
                                             <asp:Button ID="btnReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClick="btnReset_Click" Text="RESET" Width="100px" />
                                           
                                        </td>
                                        
                                      
                                    </tr>

                                                         

                                    
                                    </asp:Panel>
               </td>
                    </table>
                    
                    <%--'26.02--%> 
                    
                   

                                 <asp:Panel ID="Panel1" runat="server" style="margin-top:5px; padding-top:5px;" BackColor="#F8F8F8"   BorderColor="gray" BorderWidth="1" BorderStyle="Solid" Width="100%" Height="90%"  HorizontalAlign="Center" >
                              
                     <table border="0" style="width:100%;text-align:center;padding-left:10px;padding-top:5px; margin-top:5px;">
                                                       


                                     <tr style="padding-top: 2px;">
                                        <td style="width: 9%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >&nbsp;</td>
                                        <td style="width: 15%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                                            &nbsp;</td>
                                        <td style="width: 6%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                                        <td  style="text-align: left; width:10%" >
                                            &nbsp;</td>

                                        <td style="width: 8%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                                        <td style="text-align: left; width:18%" >
                                     
                                         
                                         
                                           
                                           
                                        </td>

                                       
                                         <td style="text-align: left; width:13%" >
                                        
                                             
                                        </td>

                                        <td style="text-align: left; width: 6%">
                                            &nbsp;</td>
                                        <td style="text-align: left; width: 6%">
                                            &nbsp;</td>
                                                                               

                                    </tr>

                                    
                                    </asp:Panel>
                    <%--'26.02--%>          
                                    </table>
                               
                                
                        
                    </td>
                   </tr>
            </table>

             <table border="0" style="width: 98%; text-align: center;">
                 
                </table>



         <asp:Panel ID="pnlServiceRecord" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="180px" ScrollBars="Auto" style="text-align:center; width:99%; margin-left:auto; margin-right:auto;"    Visible="true" >
                     
                    <table>
                          <tr>
                        <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>

                         <td style="text-align: left; width:15%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                        Corporate Customers :  
                                

                        </td>
                         <td style="width: 8%; text-align: center;">
                          <asp:TextBox ID="txtCorporateTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%" BorderStyle="None"></asp:TextBox>
                           
                          <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>        

                        </td>
                    </tr>

                  <tr>
                        <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>

                         <td style="text-align: left; width:15%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                        Corporate Service Locators :  
                                

                        </td>
                         <td style="width: 8%; text-align: center;">
                          <asp:TextBox ID="txtCorporateLocationTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%" BorderStyle="None"></asp:TextBox>
                           
                          <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>        

                        </td>
                    </tr>

                  <tr>
                        <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>

                         <td style="text-align: left; width:10%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                        Residential Customers :  
                                

                        </td>
                         <td style="width: 8%; text-align: center;">
                          <asp:TextBox ID="txtResidentialTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%" BorderStyle="None"></asp:TextBox>
                           
                          <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>        

                        </td>
                    </tr>

                  <tr>
                        <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>

                         <td style="text-align: left; width:10%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                        Residential Service Locators :  
                                

                        </td>
                         <td style="width: 8%; text-align: center;">
                          <asp:TextBox ID="txtResidentialLocationTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%" BorderStyle="None"></asp:TextBox>
                           
                          <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>        

                        </td>
                    </tr>

                  <tr>
                        <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>

                         <td style="text-align: left; width:8%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                        Contracts :  
                                

                        </td>
                         <td style="width: 8%; text-align: center;">
                          <asp:TextBox ID="txtContractTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%" BorderStyle="None"></asp:TextBox>
                           
                          <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>        

                        </td>
                    </tr>

                  <tr>
                        <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>

                         <td style="text-align: left; width:8%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                        Sales Invoice :  
                                

                        </td>
                         <td style="width: 8%; text-align: center;">
                          <asp:TextBox ID="txtSalesInvoiceTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%" BorderStyle="None"></asp:TextBox>
                           
                          <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>        

                        </td>
                    </tr>
                    </table>
                </asp:Panel>



        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
            ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

        <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedOn" runat="server" CssClass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtIsPopup" runat="server" Height="16px" Width="200px" AutoCompleteType="Disabled" Visible="False"></asp:TextBox>
          <asp:Button ID="btnDummyStaff" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
          <asp:Button ID="btnDummyTeam" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
        <asp:TextBox ID="txt" runat="server" CssClass="dummybutton"></asp:TextBox>
         <asp:TextBox ID="txtRandom" runat="server" cssClass="dummybutton"></asp:TextBox>  
          
                                                     
    </div>


                <asp:Panel ID="pnlTeamDetails" runat="server" BackColor="#F8F8F8"  BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Width="99%" HorizontalAlign="Center" style="margin-top:4px; " >
                <table border="0" style="width:100%;text-align:center;padding-left:10px;padding-top:1px; ">
                                                     



                    <tr>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:9%;" >
                        </td>
                   
                   
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; width:6%" ></td>
                           <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:13%;" >
                     
                     
                                 </td>
                          <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                          <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:13%;" >
                     
              
                        </td>

                         <td>
                             <asp:TextBox ID="txtProcessed" runat="server" AutoCompleteType="Disabled" Width="6%" BackColor="#F8F8F8" BorderStyle="None" ForeColor="#F8F8F8"></asp:TextBox>
                        </td>

                          <td>
                            
                        </td>
                         <td style="width: 6%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                             </td>
                        <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                        <td style="width: 12%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                    </tr>
                  
                    <tr>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:11%;">
                            &nbsp;</td>
                        <td colspan="3" style="text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; ">
                            &nbsp;</td>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:15%;">
                            &nbsp;</td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            &nbsp;</td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            &nbsp;</td>
                         <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                             &nbsp;</td>
                         <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            <asp:Button ID="btnProcess" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"   OnClientClick="currentdatetime();" Text="PROCESS" Width="100px" />
                        </td>
                        
                    </tr>
                </table>
            </asp:Panel>
          
              </ContentTemplate>
                     </asp:UpdatePanel>


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
                         
                          &nbsp;</td>
                           </tr>
             

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="lblLine4EditAgreeValueSave" runat="server" Text="Please Enter "  Visible="True"></asp:Label>
                        <asp:Label ID="lblRandom" runat="server" Visible="True"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Text=" in the box below and click OK to confirm." Visible="True"></asp:Label>
                        
                      </td>
                           </tr>

             
                  

                   <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="Label1" runat="server"></asp:Label>
                        
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
                          <asp:Button ID="btnEditSalesmanSaveYes" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="calculateportfoliovaluesAgreeValueEdit()" Text="OK" Width="100px" />
                          <asp:Button ID="btnEditAgreeValueSaveNo" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                      </td>
                  </tr>
            
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlWarning" runat="server" CancelControlID="" PopupControlID="pnlWarning" TargetControlID="btnDummyWarning" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyWarning" runat="server" CssClass="dummybutton" />

             <%-- End: SAVE: Edit Agree Value --%>

        <%--'28.02--%>



       

     
    
           <%--     </ContentTemplate>
                     </asp:UpdatePanel>--%>
            </div>
      </ContentTemplate>
        
</asp:UpdatePanel>
    </asp:Content>

