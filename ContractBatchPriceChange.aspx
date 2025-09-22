<%@ Page Title="Mass Change of Contract Price" Language="vb" AutoEventWireup="false" CodeFile="ContractBatchPriceChange.aspx.vb" Inherits="ContractBatchPriceChange" MasterPageFile="~/MasterPage.Master" Culture="en-GB" %>


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

           
         .auto-style2 {
             width: 52px;
         }

           
         .auto-style3 {
             width: 100%;
             height: 26px;
         }

           
         .auto-style4 {
             width: 12%;
         }

           
        </style>
       <script type="text/javascript">

           function DisableButton() {
               document.getElementById("<%=btnProcess.ClientID%>").disabled = true;
              }
          

           function validateUploaded() {
               if ($(".ajax__fileupload_fileItemInfo").length > 0) {
                   if ($("div.ajax__fileupload_fileItemInfo").children('div').hasClass("pendingState")) {
                       alert("Upload the selected files");
                       return false;
                   }
                   else {
                       return true;
                   }
               }

           }

          
 

           function uploadComplete(sender) {
               
             }

           function uploadError(sender) {
               
           }

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
              
          //     document.getElementById("<%=txtCreatedOn.ClientID%>").value = dd + "/" + mm + "/" + y + " " + hh + ":" + MM + ":" + ss;
               
      }
           window.onbeforeunload = DisableButton;
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

     <asp:UpdatePanel ID="updPanelMassChange1" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
           <asp:controlBundle name="ModalPopupExtender_Bundle"/>
              <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>

                 <asp:controlBundle name="AsyncFileUpload_Bundle"/>  
                      <asp:controlBundle name="AjaxFileUpload_Bundle"/>  
                      <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/> 
   </ControlBundles>
    </asp:ToolkitScriptManager>     




    <div>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
     <ContentTemplate>


      <div style="text-align:center">
         
       
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099; text-align:center">Mass Change of Contract Price</h3>
        
        <table style="width:100%;text-align:center;" border="0">
         
          
            <tr>
              <td style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri'; color: brown;" colspan="5">
                  <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
          
            <tr>
              <td style="text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';" colspan="5" class="auto-style3">
                  <asp:Label ID="lblAlert" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                </td>
            </tr>


              <tr>
              <td style="width:9%;text-align:right;color:brown;font-weight:bold;font-family:'Calibri';  font-size: 15px;"> 
                      Effective Date            
        </td>
                  <td style="width:5%;text-align:right;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';">
                       <asp:TextBox ID="txtEffectiveDate" runat="server" MaxLength="50" Height="16px" Width="150px"
                                                AutoCompleteType="Disabled" AutoPostBack="True"></asp:TextBox>

                                            <asp:CalendarExtender ID="calTo" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToDate" TargetControlID="txtEffectiveDate" />
                  </td>
                  <td style="width:9%;text-align:right;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';">&nbsp;</td>
                  <td style="width:16%;text-align:right;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';">
                       
                      <asp:TextBox ID="txtActualEffectiveDate" runat="server" AutoCompleteType="Disabled" CssClass="dummybutton" AutoPostBack="True" Height="16px" MaxLength="50" Width="150px"></asp:TextBox>
                      <asp:CalendarExtender ID="txtActualEffectiveDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToDate" TargetControlID="txtActualEffectiveDate" />
                       
                  </td>
                  <td style="width:100%;text-align:right;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';">
                        <asp:Button ID="btnQuit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="QUIT" Width="100px" />
                  </td>
            </tr>    


               <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:5px;" colspan="5">
               
                    
                           <asp:Panel ID="Panel5" runat="server" BackColor="#F8F8F8"   BorderColor="gray" BorderWidth="1" BorderStyle="Solid" Width="100%" Height="90%"  HorizontalAlign="Center">
                              
                     <table border="0" style="width:100%;text-align:center;padding-left:10px;padding-top:0px; padding-bottom:2px; margin-bottom:2px; ">
                                
                           <tr>
                                        <td style="width: 7%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">
                                           

                                                  <asp:RadioButton ID="rdoSearch" GroupName="rdoSelectionCriteria" AutoPostBack="true" Text="Search" runat="Server" Font-Bold="true" ForeColor="Black" Enabled="true" Checked="false" CssClass="inline-label"/>
                  
                                        </td>
                                        <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right;" >
                                            Contract Group</td>
                                           <td style="width: 15%; text-align: left; padding-left: 1%;">
                                               <asp:DropDownList ID="ddlContractGrp" runat="server" AppendDataBoundItems="True" AutoPostBack="True" Height="22px" Width="95%">
                                                   <asp:ListItem>--SELECT--</asp:ListItem>
                                               </asp:DropDownList>
                                        </td>
                                           <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: Calibri; color: black;" class="auto-style2" >
                                               Contract No.</td>
                                        <td style="width: 12%; text-align: left;">
                                            <asp:TextBox ID="txtContractNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%"></asp:TextBox>
                                        </td>
                                       <td style="text-align: right; width:7%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; padding-left: 1%;" >
                                            Account ID</td>

                             
                              
                                        <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left:1%" class="auto-style4">
                                         
                                         
                                            <asp:TextBox ID="txtClient" runat="server" AutoCompleteType="Disabled" Height="16px" Width="70%"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnClient" runat="server" CssClass="righttextbox" Height="22" ImageUrl="~/Images/searchbutton.jpg" Width="24" ImageAlign="Top"/>
                                        </td>
                                        <td style="width: 5%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right; padding-left:1%">
                                            Name</td>
                                        <td style="width: 12%; text-align: left; ">
                                            <asp:TextBox ID="txtName" runat="server" AutoCompleteType="Disabled" Height="16px" Width="75%"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CssClass="righttextbox" Height="22" ImageUrl="~/Images/searchbutton.jpg" Width="24" />
                                        </td>
                                       <td style="text-align: right; width:6%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; padding-left: 1%;" >
                                          
                                           % Change</td>

                                        <td style="width: 6%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left:1%">
                                            
                                            <asp:TextBox ID="txtPercChange" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" Width="60px"></asp:TextBox>
                                        </td>

                                        

                                  <td style="width:6%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; ">
                                             <asp:Label ID="lblIncreaseDecrease" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                     
                                        
                                      
                                    </tr>

                                                         

                                    
                                    <tr>
                                        <td style="width: 7%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">&nbsp;</td>
                                        <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right;">&nbsp;</td>
                                        <td style="width: 15%; text-align: left; padding-left: 1%;">&nbsp;</td>
                                        <td class="auto-style2" style="text-align: right; font-size: 15px; font-weight: bold; font-family: Calibri; color: black;">&nbsp;</td>
                                        <td style="width: 12%; text-align: left;">&nbsp;</td>
                                        <td style="text-align: center; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; padding-left: 1%;" colspan="3">
                                            <asp:CheckBox ID="chkExcludeContractsWithPO" runat="server" Enabled="False" Text="Exclude Contracts with PO Number" />
                                        </td>
                                        <td style="width: 12%;  font-size:15px;font-family:Calibri; text-align:left; color:black; padding-left:1%;">
                                            
                                        </td>
                                        <td style="text-align: right; width: 6%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; padding-left: 1%;">
                                            <asp:Button ID="btnGo" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SEARCH" Width="95px" />
                                        </td>
                                        <td style="width: 6%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left: 1%">
                                            <asp:Button ID="btnReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="RESET" Width="95px" OnClick="btnReset_Click" />
                                        </td>
                                        <td style="width: 6%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right; padding-left: 1%">
                                            &nbsp;</td>
                           </tr>

                                                         

                                    
                                    <tr>
                                        <td style="width: 7%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">
                                            <asp:RadioButton ID="rdoImport" GroupName="rdoSelectionCriteria" AutoPostBack="true" Text="Import" Font-Bold="true"  ForeColor="Black" runat="Server" Enabled="true" CssClass="inline-label"/>
                                        </td>
                                        <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right;">Select Excel File to Import Data</td>
                                     
                                           <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>       
                                           <td style="text-align: left; width: 15%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;padding-left: 1%;">
                                   

                                                  <asp:FileUpload ID="FileUpload1" runat="server" CssClass="Centered" Width="100%" />
      
                                        </td>

          </ContentTemplate>
</asp:UpdatePanel>
                                        <td style="font-size: 15px; font-weight: bold; font-family: Calibri; color: black; text-align: left; padding-left: 1%" ">
                                               <asp:Button ID="btnImportExcelUpload" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="IMPORT" Width="100px" Visible="True"   />
                                        </td>
                                        <td style="width: 8%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left: 1%">   
                                            <asp:ImageButton ID="btnImportExcelTemplate" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px"  />
                                       </td>
                                        <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left: 1%" colspan="3">
                                            &nbsp;</td>
                                        <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left: 1%">
                                                  <asp:Button ID="btnDummyClient" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="5px" />
                                      
                                        </td>
                                        <td style="width: 1%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right; padding-left: 0%">
                                                  <asp:Button ID="btnDummyContract" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="5px" />
                                      
                                        </td>
                                        <td style="width: 6%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">&nbsp;</td>
                         <td style="width: 6%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;">&nbsp;</td>
                                          </tr>

                                                         

                                    
                                    </asp:Panel>
               </td>
                    </table>
                    
    
        
      

                                 <asp:Panel ID="Panel1" runat="server" style="margin-top:5px; padding-top:5px;" BackColor="#F8F8F8"   BorderColor="gray" BorderWidth="1" BorderStyle="Solid" Width="100%" Height="90%"  HorizontalAlign="Center" >
                              
                     <table border="0" style="width:100%;text-align:center;padding-left:10px;padding-top:5px; margin-top:5px;">
                                                       


                                     <tr style="padding-top: 2px;">
                                        <td style="width: 9%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >&nbsp;</td>
                                        <td style="width: 15%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                                            &nbsp;</td>
                                        <td style="width: 6%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                                        <td  style="text-align: left; width:10%" >
                                            
                                        </td>

                                        <td style="width: 8%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                                        <td style="text-align: left; width:18%" >
                                           
                                               <asp:ModalPopupExtender ID="mdlPopUpTeam" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlTeamClose" Enabled="True" PopupControlID="pnlPopUpTeam" TargetControlID="btnDummyTeam">
                                            </asp:ModalPopupExtender>
                                            <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient">
                                            </asp:ModalPopupExtender>
                                              <asp:ModalPopupExtender ID="mdlPopUpStaff" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlStaffClose" Enabled="True" PopupControlID="pnlPopUpStaff" TargetControlID="btnDummyStaff">
                                            </asp:ModalPopupExtender>
                                           
                                           
                                        </td>

                                       
                                         <td style="text-align: left;font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; width:13%" >
                                            <asp:ModalPopupExtender ID="mdlPopUpContractNo" runat="server" CancelControlID="btnPopUpContractNoClose" PopupControlID="pnlPopUpContractNo"
                                                TargetControlID="btnDummyContract" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
                                             
                                             <asp:CheckBox ID="chkUpdateServiceRecords" runat="server" Text="Update Service Records" CssClass="dummybutton" />
                                             
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
                    <tr>
                        <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>

                         <td style="text-align: left; width:8%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                        Total Records :  
                                

                        </td>
                         <td style="width: 8%; text-align: center;">
                          <asp:TextBox ID="txtTotalRecords" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%" BorderStyle="None"></asp:TextBox>
                           
                          <td style="width: 40%; text-align: center;">
                            &nbsp;&nbsp;</td>        

                        </td>
                    </tr>
                </table>



  <%--<asp:Panel ID="pnlServiceRecord" runat="server" Width="1300px" ScrollBars="Auto" BackColor="#F8F8F8" BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Height="200px">
  --%>      
                    
                    <table>
                        <tr style="text-align: center;">
                            <td style="width: 100%; text-align: left">
  <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="270px" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1330px">
          
           
                                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" 
                                      Font-Size="14px"  CellPadding="1" CellSpacing="2" 
                                    HorizontalAlign="Left" AllowSorting="True" ForeColor="#333333" GridLines="None"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkGrid" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                      <asp:TemplateField> 
                    <HeaderTemplate>
                        </HeaderTemplate>
                <ItemTemplate>
                    </ItemTemplate></asp:TemplateField>            
              <asp:TemplateField HeaderText="Status"><ItemTemplate><asp:TextBox ID="txtStatusGV" runat="server" Text='<%# Bind("Status")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" style="text-align:center" Width="35px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
                 <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Text='<%# Bind("ContractNo")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="150px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Contract Group"><ItemTemplate><asp:TextBox ID="txtContractGroupGV" runat="server" Text='<%# Bind("ContractGroup")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="95px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
                  <asp:TemplateField HeaderText="Account Id"><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" Text='<%# Bind("AccountID")%>' Font-Size="12px" ReadOnly="true" style="text-align:center" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="68px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                <asp:TemplateField HeaderText="Name"><ItemTemplate><asp:TextBox ID="txtClientNameGV" runat="server" Text='<%# Bind("CustName")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="200px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                        <asp:TemplateField HeaderText="Start"><ItemTemplate><asp:TextBox ID="txtStartDateGV" runat="server" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy}")%>' Font-Size="12px" style="text-align:center"  ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="73px" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="End"><ItemTemplate><asp:TextBox ID="txtEndDateGV" runat="server" Text='<%# Bind("EndDate", "{0:dd/MM/yyyy}")%>' Font-Size="12px"  ReadOnly="true" style="text-align:center"  BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="73px" ></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Text='<%# Bind("ServiceAddress")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="200px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
               <asp:TemplateField HeaderText="Total Service Value"><ItemTemplate><asp:TextBox ID="txtTotalServiceValueGV" runat="server" DataFormatString="{0:N2}" Font-Size="12px"  ReadOnly="false"  BorderStyle="None" BackColor="#CCCCCC" style="text-align:right" Height="18px" Width="85px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                   
                <asp:TemplateField HeaderText="Current Contract Value"><ItemTemplate><asp:TextBox ID="txtAgreeValueGV" runat="server" Text='<%# Bind("AgreeValue")%>' Font-Size="12px" DataFormatString="{0:N2}" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="95px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText=" % Change"><ItemTemplate><asp:TextBox ID="txtPercChangeGV" runat="server" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Text='<%# Bind("PercChange")%>' Visible="true"  Height="15px" Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
                                        
                     <asp:TemplateField HeaderText="New Contract Value"><ItemTemplate><asp:TextBox ID="txtNewAgreeValueGV" runat="server" Font-Size="12px" ReadOnly="false" Text='<%# Bind("NewAgreeValue")%>'  BorderStyle="None"  DataFormatString="{0:N2}" BackColor="#CCCCCC" style="text-align:right" Height="18px" Width="85px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Created By"><ItemTemplate><asp:TextBox ID="txtCreatedByGV" runat="server" Text='<%# Bind("CreatedBy")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="100px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Created On"><ItemTemplate><asp:TextBox ID="txtCreatedOnGV" runat="server" Text='<%# Bind("CreatedOn")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="100px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Status"><ItemTemplate><asp:TextBox ID="txtProcessStatusGV" runat="server" Text='<%# Bind("ProcessStatus")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="90px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Procssed On"><ItemTemplate><asp:TextBox ID="txtProcessedOnsGV" runat="server" Text='<%# Bind("ProcessedOn")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="100px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoContractNoGV" runat="server"  Text='<%# Bind("Rcno")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtEndOfLastScheduleGV" runat="server"  Text='<%# Bind("EndOfLastSchedule")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            

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
                    </table>
               

<%--            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
            ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

        <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
       
        <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtIsPopup" runat="server" Height="16px" Width="200px" AutoCompleteType="Disabled" Visible="False"></asp:TextBox>
        <asp:Button ID="btnDummyStaff" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
        <asp:Button ID="btnDummyTeam" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
        <asp:TextBox ID="txt" runat="server" CssClass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtRandom" runat="server" cssClass="dummybutton"></asp:TextBox>  
          
        <asp:TextBox ID="txtPriceIncreaseLimit" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtPriceDecreaseLimit" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
        <asp:TextBox ID="txtWorkBookName" runat="server" CssClass="dummybutton"></asp:TextBox>  
         <asp:TextBox ID="txtRcnoFirstContractPriceHistory" runat="server" CssClass="dummybutton"></asp:TextBox>  
                            
                                                                
    </div>


                <asp:Panel ID="pnlTeamDetails" runat="server" BackColor="#F8F8F8"  BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Width="99%" HorizontalAlign="Center" style="margin-top:4px; " >
                <table border="0" style="width:100%;text-align:center;padding-left:10px;padding-top:1px; ">
                                                     



                    <tr>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:9%;" >
                            <asp:Button ID="btnReset0" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="." Width="40px" />
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
                              <asp:TextBox ID="txtTeamSelect" runat="server" AutoCompleteType="Disabled" Width="30%" BackColor="#F8F8F8" BorderStyle="None" ForeColor="#F8F8F8"></asp:TextBox>
                              <asp:Button ID="btnDummyTeam0" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                              <asp:ModalPopupExtender ID="btnDummyTeam0_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlTeamClose" Enabled="True" PopupControlID="pnlPopUpTeam" TargetControlID="btnDummyTeam0">
                              </asp:ModalPopupExtender>
                               <asp:TextBox ID="txtStaffSelect" runat="server" AutoCompleteType="Disabled" Width="30%" BackColor="#F8F8F8" BorderStyle="None" ForeColor="#F8F8F8"></asp:TextBox>
                             
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
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" colspan="2">
                            Processing Started On:</td>
                         <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                              <asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ReadOnly="True" ></asp:TextBox>
                         </td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            <asp:Button ID="btnProcess" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"   OnClientClick="currentdatetime();" Text="PROCESS" Width="100px" />
                        </td>
                        
                    </tr>
                </table>
            </asp:Panel>
          
              </ContentTemplate>
                     </asp:UpdatePanel>


        <%--'28.02--%>

        
                        <%--'26.02--%> 
                    
                             <%--Confirm Zeo value--%>
                                              
                 <asp:Panel ID="pnlZeroValue" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label10" runat="server" Text="PROCESS BATCH CONTRACT PRICE CHANGE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label11" runat="server" Text="Do you wish to process records effective on "></asp:Label>
                           
                      </td>
                           </tr>

              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                                                 
                          <%-- <asp:Label ID="Label2" runat="server" Text="[Yes/No]"></asp:Label>--%>
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnZeroValueYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnZeroValueNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupZeroValue" runat="server" CancelControlID="" PopupControlID="pnlZeroValue" TargetControlID="btnZeroValue" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnZeroValue" runat="server" CssClass="dummybutton" />

             <%-- Confirm Zero Value--%>   

        

                           <%-- Confirm Successful Completion--%>   
                                              
                 <asp:Panel ID="pnlSuccess" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label2" runat="server" Text="BATCH CONTRACT PRICE CHANGE PROCESS COMPLETE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label4" runat="server" Text="Do you wish to process records effective on "></asp:Label>
                           
                      </td>
                           </tr>

              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                                                 
                           <asp:Label ID="Label5" runat="server" Text="[Yes/No]"></asp:Label>
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="Button1" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="Button2" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px"  />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupSuccess" runat="server" CancelControlID="" PopupControlID="pnlSuccess" TargetControlID="btnSuccess" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnSuccess" runat="server" CssClass="dummybutton" />

             <%-- Confirm Successful Completion--%>   

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
                         
                          &nbsp;<asp:Label ID="Label17" runat="server" Text="The option to update the Service Reords is not Enabled. This may cause"></asp:Label>
                        
                      </td>
                           </tr>

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="lblLine2EditAgreeValueSave" runat="server" Text=" Contract value and Portfolio value to mismatch with the service value."></asp:Label>
                        
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
                          <asp:Button ID="btnEditAgreeValueSaveYes" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="calculateportfoliovaluesAgreeValueEdit()" Text="OK" Width="100px" />
                          <asp:Button ID="btnEditAgreeValueSaveNo" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                      </td>
                  </tr>
            
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlWarning" runat="server" CancelControlID="" PopupControlID="pnlWarning" TargetControlID="btnDummyWarning" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyWarning" runat="server" CssClass="dummybutton" />

             <%-- End: SAVE: Edit Agree Value --%>

        <%--'28.02--%>



        <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="90%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center"><table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;">
            <asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
</td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:20px;">&#160;Search Name &#160;&#160; <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
<asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Width="24px" Visible="False"></asp:ImageButton>
<asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" OnClick="btnPopUpClientReset_Click" Width="24px"></asp:ImageButton>
</td><td>
                    <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>
                </td></tr></table><div style="text-align: center; padding-left: 10px; padding-bottom: 5px;"><div class="AlphabetPager">
        <asp:Repeater ID="rptClientAlphabets" runat="server" Visible="False"><ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>'  ForeColor="Black" />
</ItemTemplate>
</asp:Repeater>
</div><br />
                    <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                       CellPadding="4" GridLines="None" Font-Size="15px" Width="60%" OnSelectedIndexChanged="gvClient_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
            <ControlStyle Width="5%" />

            <HeaderStyle HorizontalAlign="Left" />

            <ItemStyle Width="5%" />
            </asp:CommandField>
            <asp:BoundField DataField="AccountID" HeaderText="Account Id" SortExpression="AccountID" >
            <ControlStyle Width="8%" />

            <HeaderStyle Wrap="False" HorizontalAlign="Left" />

            <ItemStyle Width="8%" />
            </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                <ControlStyle Width="30%" />
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="False" />
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
            <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
        </div>
    </asp:Panel>

       <asp:Panel ID="pnlPopUpTeam" runat="server" BackColor="White" Width="700px" Height="80%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                       <table><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Team</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopUpTeam" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True">Search Here for Team or In-ChargeId</asp:TextBox>
    <asp:ImageButton ID="btnPopUpTeamSearch" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" />
                           <asp:ImageButton ID="btnPopUpTeamReset" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
                               <asp:TextBox ID="txtPopupTeamSearch" runat="server" Width="20%" Visible="False"></asp:TextBox>
                           </td></tr></table><div style="text-align: center; padding-left: 50px; padding-bottom: 5px;"><div class="AlphabetPager">
    <asp:Repeater ID="rptrTeam" runat="server" Visible="False"><ItemTemplate><asp:LinkButton ID="lbtnTeam" runat="server" Text='<%#Eval("Value")%>' ForeColor="Black" /></ItemTemplate></asp:Repeater></div><br />
        
        <asp:GridView ID="gvTeam" runat="server" DataSourceID="SqlDSTeam" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
                                    CellPadding="2" GridLines="None" Width="90%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True"><ControlStyle Width="50px" />
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle Width="8%" /></asp:CommandField><asp:BoundField DataField="TeamID" HeaderText="Id" SortExpression="TeamID"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="TeamName" HeaderText="Name" SortExpression="TeamName"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="InChargeId" HeaderText="InCharge Id" SortExpression="InChargeId"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField>
                <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
            </Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSTeam" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div>

          </asp:Panel>

   

         <asp:Panel ID="pnlPopUpStaff" runat="server" BackColor="White" Width="700px" Height="80%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                       <table><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Staff</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopupStaff" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkStaff(this, event);" onfocus = "WaterMarkStaff(this, event);" AutoPostBack="True">Search Here for Staff </asp:TextBox>
    <asp:ImageButton ID="btnPopUpStaffSearch" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" /><asp:ImageButton ID="btnPopUpStaffReset" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
                               <asp:TextBox ID="txtPopupStaffSearch" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr></table><div style="text-align: center; padding-left: 50px; padding-bottom: 5px;"><div class="AlphabetPager">
    <asp:Repeater ID="rptrStaff" runat="server" Visible="False"><ItemTemplate>
        <asp:LinkButton ID="lbtnStaff" runat="server" Text='<%#Eval("Value")%>'  ForeColor="Black" /></ItemTemplate></asp:Repeater></div><br />
        
        <asp:GridView ID="gvStaff" runat="server" DataSourceID="SqlDSStaff" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
                                    CellPadding="2" GridLines="None" Width="80%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True"><ControlStyle Width="50px" />
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle Width="8%" /></asp:CommandField><asp:BoundField DataField="StaffId" HeaderText="Id" SortExpression="StaffId"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="NRIC" HeaderText="NRIC" SortExpression="NRIC"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div>

          </asp:Panel>

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
                             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
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
            <asp:SqlDataSource ID="SqlDSContractNo" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
        </div>
    </asp:Panel>
    
           <%--     </ContentTemplate>
                     </asp:UpdatePanel>--%>
            </div>
      </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger ControlID="btnImportExcelUpload" />
            </Triggers>
</asp:UpdatePanel>
    </asp:Content>
