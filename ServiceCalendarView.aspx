<%@ Page Title="Service Calendar View" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"  CodeFile="ServiceCalendarView.aspx.vb" Inherits="ServiceCalendarView" Culture="en-GB" EnableEventValidation="false"  %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>--%>

<%--<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
     <%--<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>--%><%--<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>--%>
    <link href="CSS/popover.css" rel="stylesheet" />
    <link href="CSS/jquery.CalendarHeatmap.min.css" rel="stylesheet" />
    <link href="CSS/fullcalendar.css" rel="stylesheet" />   
    <link rel="Stylesheet" type="text/css" href="CSS/DefaultStyles.css" />
    <link href="CSS/Slidercss/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" />

    <script type="text/javascript" src="JS/jquery-1.6.1.min.js" ></script>
    

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="JS/Dropdownscript.js" ></script>
    <script type="text/javascript" src="JS/Dropdownscript.min.js" ></script>
    <script src="Scripts/moment.min.js"></script>
    <script src="Scripts/jquery.CalendarHeatmap.js"></script>
    <script src="Scripts/fullcalendar.js"></script>
    <script src="JS/jquery.blockUI.Js"></script>
   

      <script type="text/javascript" lang="javascript">
          function pageScroll() {
              window.scrollTo(0, 0); // horizontal and vertical scroll increments
              //  scrolldelay = setTimeout('pageScroll()', 100); // scrolls every 100 milliseconds
          }

            </script>
 
 

      <style>
         
          .checkbox1 tr td:nth-child(1){
            padding-top:5px;
        }

        .fc-event, .fc-event:hover{
                /*height: 40px!important;*/
                width: 97px !important;
                overflow: hidden !important;
                text-overflow: ellipsis !important;
          }
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

            .popover{
               /*max-width: 325px !important;*/    
                color:black;
                /*white-space: nowrap !important;*/
                width: 335px !important;
                max-width: 335px !important;
             }
            
            .recorddetails{
                z-index: 20000 !important;
            } 

               .roundbutton1 {
                    border: 2px solid #a1a1a1;   
                    background: #dddddd;   
                    border-radius: 25px;
                    height:30px;
                    width:100px;
                }

            .legend {
                list-style: none;
                color: black;
            }

            .legend li {
                float: left;
            }

            .legend span {
                border: 1px solid #ccc;
                float: left;
                width: 12px;
                height: 12px;
                margin: 2px;
            }

            .modal-header .devicedetails {
            float:right;
        }
        .modal-header .close {
            position: absolute;
            right: 19px;
            font-size: 33px;
            top: 8px;            
        }
       .devicedetails{
            font-size: 21px;
            font-weight: bold;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #fff;
            filter: alpha(opacity=20);
            /*opacity: .2;*/
       }
          /*.hScrollbar {
         
          width: 500%;
          overflow-y: hidden;
          overflow-x: scroll; 
          }*/

           /*For Horizontal Scroll View of the Calendars Start*/
          .calendarcontainer{   
            width:101em;
            border: 1px solid #1d3f93;
            white-space:nowrap;
            overflow-x:auto;
            z-index:50 !important;
          }
          .scrollbox{
            width:49%;
            height:1050px !important;
            margin:10px;
            display:inline-block;
            /*z-index:50 !important;*/
           }
           .scrollboxTop{
            width:49%;
            height:0px !important;
            margin:10px;
            display:inline-block;
            /*z-index:50 !important;*/
           }

          .box2{
            width:48%;
            height:1050px !important;
            margin:10px;
            z-index:50 !important;
           }


.Flipped, .Flipped .scrollbox
{
    transform:rotateX(180deg);
    -ms-transform:rotateX(180deg); /* IE 9 */
    -webkit-transform:rotateX(180deg); /* Safari and Chrome */
}

.Flipped, .Flipped .box2
{
    transform:rotateX(180deg);
    -ms-transform:rotateX(180deg); /* IE 9 */
    -webkit-transform:rotateX(180deg); /* Safari and Chrome */
}

          /*For Horizontal Scroll View of the Calendars End */

          .btn-primary{
            font-size: 14px !important;
            padding: 5px !important;
            border-radius: 3px !important;
            margin-bottom: 5px !important;
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
              <%--  <div class="ldBar" data-stroke="data:ldbar/res,gradient(0,1,#f99,#ff9)"></div>--%>
            </ProgressTemplate>
        </asp:UpdateProgress>
    <asp:UpdatePanel ID="updPanelService1" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
    
                <asp:HiddenField ID="hdnqueryselected" runat="server" value=""/>
              <asp:HiddenField ID="hdnRcNo" runat="server" value="0" />
              <asp:HiddenField ID="hdnGetCalendarResult" runat="server" value="" />
              <asp:HiddenField ID="hdnTeamIDList" runat="server" value="" />
              <asp:HiddenField ID="hdnCalDisplayView" runat="server" value="" />
              <asp:HiddenField ID="hdnCalDisplayFormat" runat="server" value="" />
               <asp:HiddenField ID="hiddenrandomnumber" runat="server" value=""  OnValueChanged="hiddenrandomnumber_ValueChanged"/>

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
                
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">SERVICE CALENDAR VIEW</h3>
           

        <table border="0" style="width:100%;text-align:center;">
              <tr>
                 <td colspan="4" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';">
                      <asp:Label ID="lblMessage" runat="server" ForeColor="brown" Text=""></asp:Label><br />
                     <asp:Label ID="lblAlert" runat="server" BackColor="Red" ForeColor="White" Text=""></asp:Label>
                 </td>
            </tr>
          


            <tr>
                  <td colspan="1" rowspan="1" style="width:10%; text-align:left;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
          
    
                      </td>
              <td colspan="1" rowspan="1" style="width:6%; text-align:left;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
      
                  &nbsp;</td>
                          
                             <td colspan="1" rowspan="1" style="text-align:left;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';padding-left:10px;padding-top:0px;">
                   

                                  
                  </td>
            <td rowspan="1" style="width:10%; text-align:left;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      &nbsp;</td> 
     
            </tr>


            <%--<tr>
                <td colspan="2"><br /></td>
            </tr>--%>
           <%-- <tr>
               <td colspan="2" style="width:80%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage1" runat="server" Text=""></asp:Label>
               </td> 
            </tr>--%>
           

            </table>


            <table border="0" style="width:100%;text-align:center;">
            <tr>
                 <td colspan="8" style="width:100%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
                       <asp:Label ID="lblContract" runat="server" Visible="false" Text="Contract No: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblContract1" runat="server" Text=""  ></asp:Label>  &nbsp;  &nbsp;  &nbsp;
                    

              <asp:Label ID="lblAccountIdContactLocation" runat="server" Visible="false" Text="Location Id: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblAccountIdContactLocation1" runat="server" Text=""  ></asp:Label>
                  
                      

                         <asp:Label ID="txtMode" runat="server" Text="" Visible="true" CssClass="dummybutton"></asp:Label>
                      <asp:Label ID="txtModeTS" runat="server" Text="" Visible="true" CssClass="dummybutton"></asp:Label>
                   
                      </td> 
            </tr>
            <tr>
                <td colspan="8" style="width:100%;text-align:left;"> 
                 
                    
                       <table border="0" "width:100%">
                        <tr>

                    <td style="text-align:left;">

                     <asp:Button ID="btnFilter" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="90px" Visible="True" Height="30px" />
              
                 </td>            
                      
                       
                 <td style="text-align:right;"> <asp:Button ID="btnBack" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="BACK" Width="90px" Height="30px" />
                     <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px" Height="30px" />
               </td>
                        </tr>
                    </table> 


                </td>
            </tr>
            <tr>
     <td colspan="8" style="text-align:right">
         <%-- <td style="width: 100px">
            <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Insert" />
        </td>--%>
        <table border="0" id="tablesearch" runat="server" style="border: 1px solid #CC3300; text-align:right; border-radius: 25px;padding: 2px; width:100%; height:100px; background-color: #F3F3F3;">
            <tr>
                <td class="CellFormatSearch" style="text-align:left;">ServiceRecord</td>
                <td class="CellTextBoxSearch" style="width:2%">
                    <asp:TextBox ID="txtSearch1Svc" runat="server" Width="88%" AutoPostBack="True"></asp:TextBox></td>
                 <td class="CellFormatSearch" style="text-align:left;">ServiceDateFrom</td>
                <td class="CellFormatSearch" style="text-align:left;">
                     <asp:TextBox ID="txtSearch1SvcDate" runat="server" Width="85%"></asp:TextBox>
                    <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender1" ClearMaskOnLostFocus="false"
    MaskType="Date" Mask="99/99/9999" TargetControlID="txtSearch1SvcDate" UserDateFormat="DayMonthYear">
</asp:MaskedEditExtender>
<%--<asp:MaskedEditValidator runat="server" ID="MaskedEditValidator1" ControlToValidate="txtSearch1SvcDate"
    ControlExtender="MaskedEditExtender1" Display="dynamic" IsValidEmpty="False" InvalidValueMessage="*" />--%>

                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtSearch1SvcDate" TargetControlID="txtSearch1SvcDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                </td>
                
                  <td class="CellFormatSearch" style="text-align:left;">DateTo</td>
                <td class="CellTextBoxSearch" style="width:1%">
                    <asp:TextBox ID="txtSearch2SvcDate" runat="server" Width="85%"></asp:TextBox>
                      <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender2" ClearMaskOnLostFocus="false"
    MaskType="Date" Mask="99/99/9999" TargetControlID="txtSearch2SvcDate" UserDateFormat="DayMonthYear">
</asp:MaskedEditExtender>
<%--<asp:MaskedEditValidator runat="server" ID="MaskedEditValidator2" ControlToValidate="txtSearch2SvcDate"
    ControlExtender="MaskedEditExtender1" Display="dynamic" IsValidEmpty="False" InvalidValueMessage="*" />--%>
                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtSearch2SvcDate" TargetControlID="txtSearch2SvcDate" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender></td>
                  <td class="CellFormatSearch" style="text-align:left;" colspan="1">
                    BillStatus</td>
                    <td class="CellTextBoxSearch" colspan="1" style="text-align:left; width:.6%"><asp:RadioButtonList ID="rdbSearchBillStatus" runat="server" RepeatDirection="Horizontal" CellPadding="2" CellSpacing="2">
                            <asp:ListItem Selected="True">All</asp:ListItem>
                            <asp:ListItem>Billed</asp:ListItem>
                            <asp:ListItem>UnBilled</asp:ListItem>
                        </asp:RadioButtonList></td>

                   <%--<td class="CellFormatSearch" colspan="3" style="text-align:left; width:4%"></td>--%>
        
                </tr>

            <tr>
                 <td class="CellFormatSearch" style="text-align:left;">Status</td>
                <td class="CellTextBoxSearch" style="width:1%">
                   
                        <asp:TextBox ID="txtSearch1Status" runat="server" Width="75%"></asp:TextBox><asp:ImageButton ID="btnSearch1Status" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" /></td>
                  <td class="CellFormatSearch" style="text-align:left;">TeamID</td>
                 <td class="CellFormatSearch" style="text-align:left;">
                     <asp:TextBox ID="txtSearch1Team" runat="server" Width="75%"></asp:TextBox>  <asp:ImageButton OnClick="btnTeamSearch_Click" ID="btnTeamSearch" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" />
                 </td>
              
                 <td class="CellFormatSearch" style="text-align:left;">ServiceBy</td>
                <td class="CellTextBoxSearch" style="width:1%">
                    <asp:TextBox ID="txtSearch1SvcBy" runat="server" Width="75%"></asp:TextBox>  <asp:ImageButton ID="btnSvcBySearch" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" /></td>
              <td class="CellFormatSearch" style="text-align:left;">Incharge</td>
                <td colspan="1" class="CellTextBoxSearch" style="width:1%; text-align: left;">
                    <asp:TextBox ID="txtSearch1Incharge" runat="server" Width="75%" AutoPostBack="True"></asp:TextBox>  <asp:ImageButton ID="btnInchargeSearch" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top"/></td>
              
             <%--   <td class="CellFormatSearch" colspan="1" style="text-align:left;"> </td>
                 <td class="CellFormatSearch" colspan="1" style="text-align:left;"> </td>
                 <td class="CellFormatSearch" colspan="1" style="text-align:left;"> </td>--%>
                   </tr>
               <tr>               
                
                
             <%--<asp:dropdownlist ID="txtTargetDescadd" runat="server" Width="400px" DataSourceID="SqlDSTarget" DataTextField="Descrip1" DataValueField="Descrip1" />--%>
                    <td class="CellFormatSearch" style="text-align:left;">
                      Customer Type  </td>
                <td class="CellTextBoxSearch" style="width:1%">
                    <asp:DropDownList ID="ddlSearchContactType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="95%">
                                   <asp:ListItem>--SELECT--</asp:ListItem>
                                        <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                                     <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                 </asp:DropDownList>
                  </td>
                 <td class="CellFormatSearch" style="text-align:left;" colspan="1">LocationID
                    </td>
                <td class="CellTextBoxSearch" style="width:1%">

                      <asp:TextBox ID="txtSearch1AccountID" runat="server" Width="75%"></asp:TextBox><asp:ImageButton ID="btnAccountIDSearch" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" />
                    </td>                

                  
           
                   <td colspan="1" class="CellFormatSearch" style="text-align:left;"> Service Name</td>
           
             <td colspan="2" class="CellFormatSearch" style="width:1%; text-align:left;"> 
                     <asp:TextBox ID="txtSearch1ClientName" runat="server" Width="90%"></asp:TextBox> 
                    <asp:ImageButton ID="btnAccountNameSearch" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top"/>
             </td>
                 
                   
                       <td class="CellFormatSearch" colspan="1" style="text-align:left;">
                              <asp:CheckBox ID="ChkIncludeZeroValue" runat="server" CellPadding="2" CellSpacing="2" Checked="True" Text="Show Zero Value" />
                       </td>
                        
                       
                      
                   
               
                <%--  </td>
  <td colspan="1" style="text-align:center;">
          --%>
               
                 <%--  <td class="CellFormatSearch" colspan="1" style="text-align:left;"></td>
                     <td class="CellFormatSearch" colspan="1" style="text-align:left;"> </td>
                    <td class="CellFormatSearch" colspan="1" style="text-align:left;"> </td>--%>
                 

                </tr>
         
               <tr>               
                
           
                 <td class="CellFormatSearch" style="text-align:left;">
                    ServiceAddress</td>
                <td colspan="3" class="CellTextBoxSearch" style="width:1%">
                    <asp:TextBox ID="txtSvcAddressSearch" runat="server" Width="95%"></asp:TextBox> 
                    </td>

                    <td class="CellFormatSearch" style="text-align:left;">Scheduler</td>
                <td class="CellTextBoxSearch" style="width:2%">
                         <asp:DropDownList ID="ddlSearchScheduler" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="20px" Width="88%">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                            </asp:DropDownList>
</td>
                    <td class="CellFormatSearch" style="text-align:left;" colspan="1">
                   Schedule Type
              
                     </td>
                      <td class="CellFormatSearch" style="text-align:left;" colspan="1">
                         <asp:DropDownList ID="ddlSearchScheduleType" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="20px" Width="88%">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                            </asp:DropDownList>
                         </td>
                </tr>
                  
            <tr>
                <td class="CellFormatSearch" style="text-align:left;">CompanyGrp</td>
                <td class="CellTextBoxSearch" style="width:1%">
                  <asp:DropDownList ID="ddlCompanyGrpSearch" runat="server" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Width="95%" Height="25px" AppendDataBoundItems="True"><asp:ListItem Text="--SELECT--" Value="-1" />
</asp:DropDownList>
                </td>

                <td class="CellFormatSearch" style="text-align:left; ">ContractGrp</td>
                <td class="CellTextBoxSearch" colspan="1" style="width:2%"><asp:DropDownList ID="ddlContractGrpSearch" runat="server" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Width="95%" Height="25px" AppendDataBoundItems="True"><asp:ListItem Text="--SELECT--" Value="-1" />
</asp:DropDownList> </td>


                
                <td class="CellFormatSearch" style="text-align:left;"><asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label></td>
                <td class="CellTextBoxSearch" style="width:1%">
                      <cc1:dropdowncheckboxes ID="ddlLocationSearch" runat="server" Width="15%" UseSelectAllNode = "true" AddJQueryReference="true" UseButtons="false" style="top: 0px; left: 0px" >
                                                         <Style2 SelectBoxWidth="85.5%" DropDownBoxBoxWidth="85.5%" DropDownBoxBoxHeight="120" />
                    
                                                </cc1:dropdowncheckboxes>
                </td>
                <td class="CellFormatSearch" colspan="1" style="text-align:center;width:1.2%">
                      <asp:CheckBox ID="ChkShowEmailSent" runat="server" CellPadding="2" CellSpacing="2" Checked="True" Text="Show Email Sent" />
                </td>
                <td class="CellFormatSearch" colspan="1" style="text-align:left;">
                     <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" OnClientClick="return onclickbtnQuickSearch()" CssClass="roundbutton1" Font-Bold="True" Text="Search" Height="30px" Width="40%" />
                    <asp:TextBox ID="txtServiceRecord" runat="server" Width="0%" CssClass="dummybutton" BorderStyle="None" ForeColor="#F3F3F3"></asp:TextBox> 
          
                     
 <asp:Button ID="btnResSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Reset" Height="30px" Width="40%" />&nbsp;
              
                </td>
            </tr>
                  
        </table>
         <table border="0" style="text-align:right;width:100%">
       <%--      <tr>
                <td style="text-align:left;width:20%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat" Visible="False"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True" Visible="False">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                       <asp:TextBox ID="txtSelectedIndex" runat="server" AutoCompleteType="Disabled" Height="16px"  style="padding-right: 0%" Visible="False" Width="20%"></asp:TextBox>
                    </td>
                 <td style="width:35%">  
                      <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                       <asp:TextBox ID="txtSearchSvc" runat="server" Width="380px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True" Visible="False"></asp:TextBox>      
                </td>
             </tr>--%>
               <tr>
                <td style="text-align:left;width:40%"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat" Visible="False"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True" Visible="False">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                       <asp:TextBox ID="txtSelectedIndex" runat="server" AutoCompleteType="Disabled" Height="16px"  style="padding-right: 0%" Visible="False" Width="20%"></asp:TextBox>
                 </td>
                 <td style="width:55%;font-family:Calibri;"> 
                        <asp:RadioButton ID="rdoListView" GroupName="rdoDisplayScreen" OnCheckedChanged="rdoListView_CheckedChanged" AutoPostBack="true" Text="List" runat="Server" Font-Bold="true" ForeColor="Black" Enabled="true" Checked="true" CssClass="inline-label"/>
                        <asp:RadioButton ID="rdoCalendarView" GroupName="rdoDisplayScreen" OnCheckedChanged="rdoCalendarView_CheckedChanged" AutoPostBack="true" Text="Calendar" Font-Bold="true"  ForeColor="Black" runat="Server" Enabled="true" CssClass="inline-label"/>
                </td>
                 <td style="width:10%">  
                      <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                       <asp:TextBox ID="txtSearchSvc" runat="server" Width="380px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True" Visible="False"></asp:TextBox>      
                </td>
             </tr>

         </table>     
         <asp:TextBox ID="txtRowCount" runat="server" CssClass="dummybutton"></asp:TextBox>
         <%-- <td style="width: 100px">
            <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Insert" />
        </td>--%>

     </td>
            </tr>

<%--                 <tr class="left">
                        <td style="text-align:left; "  colspan="9" class="auto-style2">
                 <div class="row">
                      <div class="col-xs-12 col-md-12" style="height: 10px"></div>
                 </div>
                    <div id="divIdCalendarView" runat="server">
                            <div class="row">
                                <div  class="col-md-2" style="max-width: 300px;">
                                    <div id="divlegendshow" runat="server" style="height: 1090px; padding: 7px; overflow: auto; border: 1px solid #1d3f93;">
                                        <div id="divlegendid" runat="server"></div>
                                    </div>
                                </div>
                                <div class="col-md-8" id="divWholeCalendars" style="color:black !important">
                                </div>
                            </div>
                    </div>
          <div class="row">
              <div class="col-xs-12 col-md-12" style="height: 10px"></div>
         </div>
            </td>
            </tr>--%>


                               <tr style="width:100%;font-family:Calibri;">
                                   
                                   <td colspan="4">
                                       <div id="divbuttonsShow" runat="server">
                                           <table border="0"  style="padding: 0px;text-align:left;color:black;">
                                                 <tr>
                                                     <td><asp:Button ID="btnDayDisplay" Width="70px" CssClass="roundbutton1" runat="server" Text="Day" OnClick="btnDayDisplay_Click" /></td>
                                                     <td><asp:Button ID="btnWeekDisplay" Width="70px" CssClass="roundbutton1" runat="server" Text="Week" OnClick="btnWeekDisplay_Click" /></td>
                                                     <td><asp:Button ID="btnMonthDisplay" Width="70px" CssClass="roundbutton1" runat="server" Text="Month" OnClick="btnMonthDisplay_Click" /></td>
                                                 </tr>
                                                 <%--<tr>
                                                     <td class="CellTextBox" colspan="3" style="padding-left:0px">
                                                        <asp:TextBox ID="txtCalendarDisplay" runat="server" Height="16px" MaxLength="10" Width="100%" AutoPostBack="true" OnTextChanged="txtCalendarDisplay_TextChanged"></asp:TextBox>
                                                         <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender5" ClearMaskOnLostFocus="false" MaskType="Date" Mask="99/99/9999" TargetControlID="txtCalendarDisplay" UserDateFormat="DayMonthYear"></asp:MaskedEditExtender>
                                                        <asp:CalendarExtender ID="DispCalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtCalendarDisplay" TargetControlID="txtCalendarDisplay"></asp:CalendarExtender>
                                                    </td>
                                                 </tr>--%>
                                                <%-- <tr>
                                                     <div id="divlegendid" runat="server"></div>
                                                 </tr>--%>
                                                 </table>
                                       </div>
                                          

                                         <div id="divlegendshow" runat="server" style="font-size: 14px; height: 1090px;overflow: auto; border: 1px solid #1d3f93;width:270px">
                                  <%--           <table  style="padding: 0px;text-align:left;color:black;">
                                                 <tr>
                                                     <td><asp:Button ID="btnDayDisplay" CssClass="roundbutton1" runat="server" Text="Day" OnClick="btnDayDisplay_Click" /></td>
                                                     <td><asp:Button ID="btnWeekDisplay" CssClass="roundbutton1" runat="server" Text="Week" OnClick="btnWeekDisplay_Click" /></td>
                                                     <td><asp:Button ID="btnMonthDisplay" CssClass="roundbutton1" runat="server" Text="Month" OnClick="btnMonthDisplay_Click" /></td>
                                                 </tr>
                                                 <tr>
                                                     <td class="CellTextBox">
                                                        <asp:TextBox ID="txtCalendarDisplay" runat="server" Height="16px" MaxLength="10" Width="100%"></asp:TextBox>
                                                        <asp:CalendarExtender ID="DispCalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtCalendarDisplay" TargetControlID="txtCalendarDisplay"></asp:CalendarExtender>
                                                    </td>
                                                 </tr>
                                                 <tr>
                                                     <div id="divlegendid" runat="server"></div>
                                                 </tr>
                                                 </table>--%>
                                              <div id="divlegendid" runat="server"></div>
                                          </div>
                                   </td>
                                   <td  colspan="4" >
                                      <%--  <div id="divScrollTop" style="color:black !important">
                                        </div>--%>
                                        <div runat="server" id="divWholeCalendars" style="color:black !important">
                                        </div>
                                   </td>
                                </tr>


            <tr class="Centered">
                <td style="text-align:center; "  colspan="8" class="auto-style2">
                   <table border="0" style="width:800px;">
   
                       
   
</table><br />
                     <%--<asp:dropdownlist ID="txtTargetDescadd" runat="server" Width="400px" DataSourceID="SqlDSTarget" DataTextField="Descrip1" DataValueField="Descrip1" />--%>
                  <div id="DivRoot" align ="center">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll; color: #000000; background-color: #FFFFFF;text-align:center" onscroll="OnScrollDiv(this)" id="DivMainContent">
        
                   <%-- <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" ScrollBars="Auto" style="width:1350px;height:500px;text-align:center; margin-left:auto; margin-right:auto;"  Visible="true" >
                  --%>    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" OnRowDataBound = "OnRowDataBoundg1" OnSelectedIndexChanged = "OnSelectedIndexChangedg1" width="100%" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" CssClass="Centered" Font-Names="Calibri" GridLines="Vertical" ForeColor="#333333" >
             <AlternatingRowStyle BackColor="White" />
            <Columns>   <asp:CommandField HeaderText="Select" ShowHeader="false" ShowSelectButton="True" Visible="false" >
                      <HeaderStyle Width="40px" />
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
                
                     <asp:BoundField DataField="JobStatus" HeaderText="--" />
                
                 <asp:BoundField DataField="Status" HeaderText="ST" SortExpression="Status"  />
                
                      <asp:BoundField DataField="LockST" HeaderText="Lock" />
                

                  <asp:TemplateField HeaderText="ES">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkES" runat="server" Enabled="false" Checked='<%# Eval("EmailSent")%>' />
                          </ItemTemplate>
                  </asp:TemplateField>

                    <asp:BoundField DataField="ServiceBy" HeaderText="Service By" SortExpression="ServiceBy" >
                       <ControlStyle Width="90px" />
                  <ItemStyle Width="90px" Wrap="False"/>
                        
                </asp:BoundField>
            
                    <asp:TemplateField ><ItemTemplate> <asp:ImageButton ID="btnEditServiceDateL" runat="server" CssClass="righttextbox" Height="18px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top" ImageUrl="~/Images/ArrowL.gif" Width="14px" OnClick="btnEditServiceDateL_Click"  />
              </ItemTemplate></asp:TemplateField>
                      <asp:BoundField DataField="SchServiceDate" HeaderText="Sch. Service Date" SortExpression="SchServiceDate" DataFormatString="{0:dd/MM/yyyy}" />
               
                 <asp:TemplateField ><ItemTemplate> <asp:ImageButton ID="btnEditServiceDateR" runat="server" CssClass="righttextbox" Height="18px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top" ImageUrl="~/Images/ArrowR.gif" Width="14px" OnClick="btnEditServiceDateR_Click"  />
              </ItemTemplate></asp:TemplateField>


               <asp:TemplateField ><ItemTemplate> <asp:ImageButton ID="btnEditServiceDate" runat="server" CssClass="righttextbox" Height="18px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="20px" OnClick="btnEditServiceDate_Click"  />
              </ItemTemplate></asp:TemplateField>
                
                     <asp:BoundField DataField="SchServiceTime" HeaderText="Sch. TimeIn" SortExpression="SchServiceTime">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="SchServiceTimeOut" HeaderText="Sch. TimeOut" SortExpression="SchServiceTimeOut">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
              <asp:BoundField DataField="ScheduleType" HeaderText="Schedule Type" SortExpression="ScheduleType">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>

                    <asp:TemplateField Visible="true" ><ItemTemplate> <asp:ImageButton ID="btnSendSMS" runat="server" Visible="TRUE" CssClass="righttextbox" Height="30px" ImageAlign="Top" ImageUrl="~/Images/sms.png" Width="30px" OnClick="btnSendSMS_Click"  />
              </ItemTemplate></asp:TemplateField>
                     <asp:BoundField DataField="LocationID" HeaderText="LocationID" SortExpression="LocationID" >
                        <ControlStyle Width="10%" />
                  <ItemStyle Width="10%" Wrap="False" />
                </asp:BoundField>
                   <asp:TemplateField HeaderText="Service Name" SortExpression="ServiceName" >
                <ItemTemplate>
                    <div style="width: 200px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("ServiceName")%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Service Address" SortExpression="Address1">
                <ItemTemplate>
                   <div style="width: 145px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("Address1")%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            
                  <asp:BoundField DataField="AddPostal" HeaderText="Postal" SortExpression="AddPostal" />
                 <asp:BoundField DataField="BillingFrequency" HeaderText="Billing Frequency" SortExpression="BillingFrequency" >
                  <ItemStyle HorizontalAlign="Left" />
                  </asp:BoundField>
                  <asp:BoundField DataField="BillAmount" HeaderText="Service Value" SortExpression="BillAmount" >
            
                  <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>


               <asp:TemplateField ><ItemTemplate> <asp:ImageButton ID="btnEditBillAmt" runat="server" CssClass="righttextbox" Height="18px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="20px" OnClick="btnEditBillAmt_Click"  />
              </ItemTemplate></asp:TemplateField>
               
                  <asp:BoundField DataField="BilledAmt" HeaderText="Billed Amt" SortExpression="BilledAmt" >
                  <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>

                 <asp:TemplateField ><ItemTemplate> <asp:ImageButton ID="btnEditBilledAmt" runat="server" CssClass="righttextbox" Height="18px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="20px" OnClick="btnEditBilledAmt_Click"  />
              </ItemTemplate></asp:TemplateField>


                  <asp:BoundField DataField="BillNo" HeaderText="BillNo" SortExpression="BillNo" >
            
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                   <asp:BoundField DataField="ContactPersonID" HeaderText="ContactPerson" SortExpression="ContactPersonID" >
                  <ControlStyle Width="160px" />
                      <ItemStyle Width="160px" Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
             <asp:TemplateField HeaderText="ServiceDescription" SortExpression="Notes">
                <ItemTemplate>
                    <div style="width: 300px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("Notes")%>  
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="ServiceInstruction" SortExpression="Comments">
                <ItemTemplate>
                    <div style="width: 300px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("Comments") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:BoundField DataField="Location" HeaderText="Location" />
                
              
                <asp:BoundField DataField="RecordNo" HeaderText="RecordNo" SortExpression="RecordNo">
                     <ControlStyle Width="150px" />
                  <ItemStyle Width="150px" Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="FollowupRecordNo" HeaderText="Follow-up RecordNo">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
               <asp:BoundField DataField="ContractNo" HeaderText="ContractNo" SortExpression="ContractNo">
                     <ControlStyle Width="150px" />
                  <ItemStyle Width="150px" Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="ContractGroup" HeaderText="ContractGroup" SortExpression="ContractGroup">
                     <ControlStyle Width="150px" />
                  <ItemStyle Width="150px" Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="CompanyGroup" HeaderText="CompanyGroup" SortExpression="CompanyGroup">
                     <ControlStyle Width="150px" />
                  <ItemStyle Width="150px" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ContactType" HeaderText="Account Type" SortExpression="ContactType" />
                
                 <asp:BoundField DataField="CustCode" HeaderText="CustCode" SortExpression="CustCode" Visible="false" />
                     <asp:BoundField DataField="AccountID" HeaderText="AccountID" SortExpression="AccountID" />
             
               <asp:TemplateField HeaderText="AccountName" SortExpression="CustName" >
                <ItemTemplate>
                    <div style="width: 200px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("CustName")%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        
                  <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" >
            
                  <ItemStyle HorizontalAlign="Left" />
                  </asp:BoundField>
            
                
            
                 
            
                  <asp:BoundField DataField="Description" HeaderText="Description">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                
                 <asp:TemplateField HeaderText="Remarks" >
                <ItemTemplate>
                    <div style="width: 300px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("Remarks")%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="DL">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkDL" runat="server" Enabled="false" Checked='<%# Eval("TabletDownloaded")%>' />
                          </ItemTemplate>
                          <ControlStyle CssClass="dummybutton" />
                          <HeaderStyle CssClass="dummybutton" />
                          <ItemStyle CssClass="dummybutton" />
                  </asp:TemplateField>

                   
                  <asp:BoundField DataField="TeamId" HeaderText="TeamId" SortExpression="TeamId">
                  <ItemStyle Wrap="False" HorizontalAlign="Left" />
                  </asp:BoundField>
                  <asp:BoundField DataField="Scheduler" HeaderText="Scheduler" SortExpression="Scheduler" >
                


                      <ItemStyle Wrap="False" />
                  </asp:BoundField>
                 
                  <asp:BoundField DataField="TimeIn" HeaderText="TimeIn" SortExpression="TimeIn" />
                  <asp:BoundField DataField="TimeOut" HeaderText="TimeOut" SortExpression="TimeOut" />
                  <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
                  <asp:BoundField DataField="OurRef" HeaderText="Our Ref" SortExpression="OurRef">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="yourRef" HeaderText="Your Ref" SortExpression="yourRef">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="LocateGrp" HeaderText="LocateGrp" SortExpression="LocateGrp" />
                
                  <asp:BoundField DataField="TabletID" HeaderText="Mobile Device ID" SortExpression="TabletID">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="TabletDownloadedDate" HeaderText="Mobile Download Date" SortExpression="TabletDownloadedDate">
                  <HeaderStyle Wrap="True" />
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                


                      <asp:BoundField DataField="UploadDate" HeaderText="Mobile Upload Date" SortExpression="UploadDate" >
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="EmailSentDate" HeaderText="Email Sent Date" SortExpression="EmailSentDate" >
                
                


                      <ItemStyle Wrap="False" />
                  </asp:BoundField>
                
                


                      <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                  <asp:BoundField DataField="CreatedOn" HeaderText="Created On" SortExpression="CreatedOn" >
                


                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                


                  <asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" />
                  <asp:BoundField DataField="LastModifiedOn" HeaderText="Edited On" SortExpression="LastModifiedOn" >
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:TemplateField Visible="False">
                      <EditItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
            
                      <asp:BoundField DataField="Remarks">
                  <ControlStyle CssClass="dummybutton" />
                  <HeaderStyle CssClass="dummybutton" />
                  <ItemStyle CssClass="dummybutton" />
                  </asp:BoundField>
                  <asp:BoundField DataField="SchServiceDate">
                  <ControlStyle CssClass="dummybutton" />
                  <HeaderStyle CssClass="dummybutton" />
                  <ItemStyle CssClass="dummybutton" />
                  </asp:BoundField>
                
                      <asp:BoundField DataField="Rcno" HeaderText="Rcno">
                  <HeaderStyle Wrap="False" />
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                
                      <asp:BoundField />
                

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
     <%--</asp:Panel>--%>
     </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div> 
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" OnSelecting="SqlDataSource1_Selecting">
                </asp:SqlDataSource>
                     <%-- <td style="width: 100px">
            <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Insert" />
        </td>--%>
                </td>
            </tr>
                

                 <tr>

               

                    <td colspan="8" >
                         
          
                    </td>
                  </tr>


                  <tr>
                    <td colspan="8">
                        <br />
                        <asp:SqlDataSource ID="SqlDSLastServiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                    </td>
                </tr>
             <%--     <tr style="width:100%">
                        <td style="width:80px; text-align:left; font-family:Calibri;  ">
                            <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Red" Text=""></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri;  ">
                            <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Red" Text=""></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Red" Text=""></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Red" Text=""></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Red" Text=""></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Red" Text=""></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">&nbsp;</td>
                        <td colspan="2" style="width:80px; text-align:right; font-family:Calibri; ">
                            <asp:Label ID="Label37" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
              
                    <tr style="width:100%">
                        <td style="width:80px; text-align:left; font-family:Calibri;  ">
                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Blue" Text="Service By"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri;  ">
                            <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Blue" Text="Schedule Type"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Blue" Text="Last Service Date"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="lblDay" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Blue" Text="Day"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Blue" Text="Actual Time"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Underline="true" ForeColor="Blue" Text="Duration"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">&nbsp;</td>
                        <td colspan="2" style="width:80px; text-align:right; font-family:Calibri; ">
                            <asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="LabelServDet1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri; ">
                            <asp:Label ID="LabelScheduleType1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri;">
                            <asp:Label ID="LabelLastServiceDate1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri;">
                            <asp:Label ID="LabelLastServiceDay1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelActualTime1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelDuration1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family: Calibri;"></td>
                        <td style="width:80px; text-align:left; font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left; font-family:Calibri">&nbsp;</td>
                    </tr>
                    <tr class="Centered">
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelServDet2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelScheduleType2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelLastServiceDate2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelLastServiceDay2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelActualTime2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelDuration2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left; font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left; font-family:Calibri">&nbsp;</td>
                    </tr>
                    <tr class="Centered">
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelServDet3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelScheduleType3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelLastServiceDate3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelLastServiceDay3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelActualTime3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">
                            <asp:Label ID="LabelDuration3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left; font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left; font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left; font-family:Calibri">&nbsp;</td>
                    </tr>
                    <tr class="Centered">
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelServDet4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelScheduleType4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelLastServiceDate4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelLastServiceDay4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelActualTime4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelDuration4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left;font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left;font-family:Calibri">&nbsp;</td>
                    </tr>
                    <tr class="Centered">
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelServDet5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelScheduleType5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelLastServiceDate5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelLastServiceDay5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelActualTime5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">
                            <asp:Label ID="LabelDuration5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                        <td style="width:80px; text-align:left;font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left;font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left;font-family:Calibri">&nbsp;</td>
                    </tr>
                    <%--  <asp:BoundField DataField="ContType" HeaderText="AccountType" SortExpression="ContType" ReadOnly="True">
                       <ControlStyle Width="5%" />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="5%" Wrap="False" />
                </asp:BoundField>--%>
                   <%--  <tr class="Centered">
                        <td colspan="7" style="text-align:left;font-family:Calibri">
                            &nbsp;</td>
                        <td style="width:80px; text-align:left;font-family:Calibri">&nbsp;</td>
                        <td style="width:80px; text-align:left;font-family:Calibri">&nbsp;</td>
                    </tr>
                --%>

            </table>

                <table border="0" style="width:100%;text-align:center;">
                        <tr style="text-align:center;width:100%">
                        <td colspan="9" style="text-align:center;padding-left:20px;">
                            <asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="0" AutoPostBack="True" CssClass="ajax__tab_xp" Font-Names="Calibri" Height="100%" Width="100%">
                                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Client Information &amp; Schedule"><HeaderTemplate>
Client Information &amp; Schedule
</HeaderTemplate>
<ContentTemplate>
<table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td colspan="5" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Record Details</td></tr><tr><td class="CellFormat"><asp:Label ID="lblBranch2" runat="server" Text="Branch"></asp:Label></td><td class="CellTextBox" colspan="4"><asp:TextBox ID="txtLocation" runat="server" Height="16px" MaxLength="10" Width="20%"></asp:TextBox></td></tr><tr><td class="CellFormat">Status</td><td class="CellTextBox" colspan="4"><asp:DropDownList ID="ddlStatus" runat="server" CssClass="chzn-select" Width="20.5%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem><asp:ListItem Value="O">O - Open/Pending</asp:ListItem><asp:ListItem Value="C">C - Cancelled</asp:ListItem><asp:ListItem Value="T">T - Terminated</asp:ListItem><asp:ListItem Value="H">H - On Hold</asp:ListItem><asp:ListItem Value="V">V - Void</asp:ListItem><asp:ListItem Value="B">B - Job cannot complete</asp:ListItem><asp:ListItem Value="P">P - Posted</asp:ListItem></asp:DropDownList></td></tr><tr><td class="CellFormat">ServiceRecordNo</td>
    <td class="CellTextBox" colspan="4"><asp:TextBox ID="txtSvcRecord" runat="server" Height="16px" MaxLength="25" ReadOnly="True" Width="20%"></asp:TextBox><asp:TextBox ID="txtCopyRecordNo" runat="server" CssClass="dummybutton"></asp:TextBox></td></tr><tr style="display:none"><td class="CellFormat">Sequence</td><td class="CellTextBox" colspan="4"><asp:TextBox ID="txtSequence" runat="server" Height="16px" MaxLength="3" Width="20%"></asp:TextBox></td></tr>
    <tr><td class="CellFormat">ContractNo<asp:Label ID="Label58" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="4"><asp:TextBox ID="txtContractNo" runat="server" Height="16px" MaxLength="25" Width="20%" AutoPostBack="True"></asp:TextBox></td></tr>
     <tr><td class="CellFormat">ContractGroup</td><td class="CellTextBox" colspan="4"><asp:TextBox ID="txtContractGroup" runat="server" Height="16px" MaxLength="25" Width="20%" ReadOnly="True" Enabled="False"></asp:TextBox></td>
         <tr>
             <td class="CellFormat">CompanyGroup<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:DropDownList ID="ddlCompanyGrp" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Height="25px" Width="20.5%">
                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                 </asp:DropDownList>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">Scheduler</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtScheduler" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox>
                 <asp:ImageButton ID="btnSchedule" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
             </td>
             <asp:ModalPopupExtender ID="mdlPopUpScheduler" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlSchedulerClose" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopUpScheduler" TargetControlID="btndummysch">
             </asp:ModalPopupExtender>
             <asp:Button ID="btndummysch" runat="server" CssClass="dummybutton" Text="Button" />
         </tr>
         <tr>
             <td class="CellFormat">ManualReportNo</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtOurRef" runat="server" Height="16px" MaxLength="100" Width="20%"></asp:TextBox>
                 <asp:ImageButton ID="btnEditOurRef" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">ManualContractNo</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtYourRef" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox>
                 <asp:ImageButton ID="btnEditManualContractNo" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">P.O.No.</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtPoNo" runat="server" Height="16px" MaxLength="100" Width="20%"></asp:TextBox>
                 <asp:ImageButton ID="btnEditPONo" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">Work Order No.</td>
             <td class="CellTextBox" colspan="4"><asp:TextBox ID="txtWorkOrderNo" runat="server" Height="16px" MaxLength="100" Width="20%"></asp:TextBox> 
                 <asp:ImageButton ID="btnEditWorkOrderNo" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td colspan="5">
                 <asp:CheckBox ID="chkInactive" runat="server" CssClass="dummybutton" Text=" Inactive" />
                 <br />
             </td>
         </tr>
         <tr>
             <td colspan="5" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Account Information</td>
         </tr>
         <tr>
             <td class="CellFormat">AccountType<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:DropDownList ID="ddlAccountType" runat="server" AutoPostBack="True" CssClass="chzn-select" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="20.5%">
                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                     <asp:ListItem Selected="True" Value="COMPANY">CORPORATE</asp:ListItem>
                     <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                 </asp:DropDownList>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">AccountID<asp:Label ID="Label3" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtAccountID" runat="server" AutoPostBack="True" Height="16px" MaxLength="10" Width="20%"></asp:TextBox>
                 <asp:ImageButton ID="btnClient" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                 <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlClientClose" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopUpClient" TargetControlID="dummy">
                 </asp:ModalPopupExtender>
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="CellFormat">AccountName<asp:Label ID="Label4" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtCustName" runat="server" Height="16px" MaxLength="200" ReadOnly="True" Width="80%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 <asp:TextBox ID="txtLockSt" runat="server" BorderStyle="None" ForeColor="White" Height="16px" MaxLength="25" ReadOnly="True" Width="10%"></asp:TextBox>
             </td>
         </tr>
         <tr style="display:none">
             <td colspan="5">
                 <asp:Panel ID="pnlOffAddrName" runat="server">
                     <table class="Centered" style="padding-top:5px;width:60%">
                         <tr>
                             <td>
                                 <br />
                             </td>
                         </tr>
                         <tr>
                             <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">
                                 <asp:ImageButton ID="imgCollapsible1" runat="server" ImageAlign="Bottom" ImageUrl="~/Images/plus.png" />
                                 &nbsp;Office Address</td>
                         </tr>
                     </table>
                 </asp:Panel>
                 <asp:Panel ID="pnlOffAddr" runat="server">
                     <table class="Centered" style="padding-top:5px;width:60%">
                         <tr>
                             <td class="CellFormat">Street Address1 </td>
                             <td class="CellTextBox" colspan="3">
                                 <asp:TextBox ID="txtOffAddress1" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Street Address2</td>
                             <td class="CellTextBox" colspan="3">
                                 <asp:TextBox ID="txtOffStreet" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Unit No &amp; Building </td>
                             <td class="CellTextBox" colspan="3">
                                 <asp:TextBox ID="txtOffBuilding" runat="server" Height="16px" MaxLength="100" onchange="UpdateBillingDetails()" Width="99%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">City </td>
                             <td class="CellTextBox">
                                 <asp:DropDownList ID="ddlOffCity" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="City" DataValueField="City" onchange="UpdateBillingDetails()" Width="99%">
                                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td class="CellFormat">State </td>
                             <td class="CellTextBox">
                                 <asp:DropDownList ID="ddlOffState" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="State" DataValueField="State" onchange="UpdateBillingDetails()" Width="99%">
                                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Country </td>
                             <td class="CellTextBox">
                                 <asp:DropDownList ID="ddlOffCountry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Country" DataValueField="Country" onchange="UpdateBillingDetails()" Width="99%">
                                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td class="CellFormat">Postal </td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtOffPostal" runat="server" Height="16px" MaxLength="20" onchange="UpdateBillingDetails()" Width="98%"></asp:TextBox>
                             </td>
                         </tr>
                     </table>
                 </asp:Panel>
                 <asp:CollapsiblePanelExtender ID="cpnl1" runat="server" CollapseControlID="pnlOffAddrName" Collapsed="True" CollapsedImage="~/Images/plus.png" CollapsedText="Click to show" Enabled="True" ExpandControlID="pnlOffAddrName" ExpandedImage="~/Images/minus.png" ImageControlID="imgCollapsible1" TargetControlID="pnlOffAddr">
                 </asp:CollapsiblePanelExtender>
             </td>
         </tr>
         <tr style="display:none">
             <td colspan="5">
                 <asp:CollapsiblePanelExtender ID="cpnl2" runat="server" CollapseControlID="pnlBillAddrName" Collapsed="True" CollapsedImage="~/Images/plus.png" CollapsedText="Click to show" Enabled="True" ExpandControlID="pnlBillAddrName" ExpandedImage="~/Images/minus.png" ImageControlID="imgCollapsible2" TargetControlID="pnlBillAddr">
                 </asp:CollapsiblePanelExtender>
                 <asp:Panel ID="pnlBillAddrName" runat="server">
                     <table class="Centered" style="padding-top:5px;width:60%">
                         <tr>
                             <td>
                                 <br />
                             </td>
                         </tr>
                         <tr>
                             <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">
                                 <asp:ImageButton ID="imgCollapsible2" runat="server" ImageAlign="Bottom" ImageUrl="~/Images/plus.png" />
                                 &nbsp;Billing Address</td>
                         </tr>
                     </table>
                 </asp:Panel>
                 <asp:Panel ID="pnlBillAddr" runat="server">
                     <table class="Centered" style="padding-top:5px;width:60%">
                         <tr>
                             <td colspan="4" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:right;">
                                 <asp:CheckBox ID="chkOffAddr" runat="server" Font-Names="Calibri" Font-Underline="False" onclick="UpdateBillingDetails()" Text="Same as Office Address" />
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Billing Name </td>
                             <td class="CellTextBox" colspan="3">
                                 <asp:TextBox ID="txtBillingName" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Street Address1 </td>
                             <td class="CellTextBox" colspan="3">
                                 <asp:TextBox ID="txtBillAddress" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Street Address2</td>
                             <td class="CellTextBox" colspan="3">
                                 <asp:TextBox ID="txtBillStreet" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Unit No &amp; Building </td>
                             <td class="CellTextBox" colspan="3">
                                 <asp:TextBox ID="txtBillBuilding" runat="server" Height="16px" MaxLength="100" Width="99%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">City </td>
                             <td class="CellTextBox">
                                 <asp:DropDownList ID="ddlBillCity" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="City" DataValueField="City" Width="99%">
                                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td class="CellFormat">State </td>
                             <td class="CellTextBox">
                                 <asp:DropDownList ID="ddlBillState" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="State" DataValueField="State" Width="99%">
                                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Country </td>
                             <td class="CellTextBox">
                                 <asp:DropDownList ID="ddlBillCountry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Country" DataValueField="Country" Width="99%">
                                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td class="CellFormat">Postal </td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillPostal" runat="server" Height="16px" MaxLength="20" Width="98%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="4">
                                 <br />
                             </td>
                         </tr>
                     </table>
                 </asp:Panel>
             </td>
         </tr>
         <tr>
             <td colspan="5">
                 <br />
             </td>
         </tr>
         <tr>
             <td colspan="5" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Service Location</td>
         </tr>
         <tr>
             <td colspan="5">
                 <table class="Centered" style="padding-top:5px;width:100%;">
                     <tr>
                         <td class="CellFormat">ServiceLocationID<asp:Label ID="Label8" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                         </td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtLocationID" runat="server" Height="16px" MaxLength="15" Width="25%"></asp:TextBox>
                             <asp:ImageButton ID="btnImgLoc" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                             <asp:ModalPopupExtender ID="mdlPopupLocation" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnLocationClose" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopUpLocation" TargetControlID="btndummy2">
                             </asp:ModalPopupExtender>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">ClientID(ViewOnly) </td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtClientID" runat="server" Enabled="False" Height="16px" MaxLength="15" Width="25%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Service Location Group</td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtServiceLocationGroup" runat="server" Height="16px" MaxLength="100" Width="25%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">ServiceName<asp:Label ID="Label6" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                         </td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtServiceName" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr class="dummybutton">
                         <td class="CellFormat">Description </td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtServiceDescription" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">StreetAddress1<asp:Label ID="Label9" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
                         </td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtAddress" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             <asp:TextBox ID="txtSvcAddr" runat="server" CssClass="dummybutton"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">StreetAddress2</td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtStreet" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">UnitNo &amp; Building </td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtBuilding" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">City </td>
                         <td class="CellTextBox" style="width:20%">
                             <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="City" DataValueField="City" Width="90%">
                                 <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                             </asp:DropDownList>
                         </td>
                         <td class="CellFormat" style="width: 10%">State </td>
                         <td class="CellTextBox" style="width:40%">
                             <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="State" DataValueField="State" Width="40%">
                                 <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Country </td>
                         <td class="CellTextBox">
                             <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Country" DataValueField="Country" Width="90%">
                                 <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                             </asp:DropDownList>
                         </td>
                         <td class="CellFormat" style="width: 8%">Postal </td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtPostal" runat="server" AutoPostBack="True" Height="16px" MaxLength="20" Width="40%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Zone</td>
                         <td class="CellTextBox">
                             <asp:DropDownList ID="ddlLocateGrp" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="locationgroup" DataValueField="locationgroup" Height="25px" Width="90%">
                                 <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                             </asp:DropDownList>
                         </td>
                         <td style="width: 8%"></td>
                         <td></td>
                     </tr>
                     <tr>
                         <td colspan="4">
                             <br />
                         </td>
                     </tr>
                     <tr>
                         <td colspan="4" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%">Service Contact Info</td>
                     </tr>
                     <tr>
                         <td class="CellFormat">ContactPerson1</td>
                         <td class="CellTextBox" colspan="1">
                             <asp:TextBox ID="txtSvcCP1Contact" runat="server" Height="16px" MaxLength="100" Width="90%"></asp:TextBox>
                         </td>
                         <td class="CellFormat" style="width: 8%">Position</td>
                         <td class="CellTextBox" colspan="1">
                             <asp:TextBox ID="txtSvcCP1Position" runat="server" Height="16px" MaxLength="100" Width="40%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Telephone</td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtSvcCP1Telephone" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                         </td>
                         <td class="CellFormat" style="width: 8%">Fax</td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtSvcCP1Fax" runat="server" Height="16px" MaxLength="50" Width="40%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Telephone2</td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtSvcCP1Telephone2" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                         </td>
                         <td class="CellFormat" style="width: 8%">Mobile</td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtSvcCP1Mobile" runat="server" Height="16px" MaxLength="50" Width="40%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Email</td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtSvcCP1Email" runat="server" Font-Names="calibri" Font-Size="15px" Height="50px" MaxLength="100" TextMode="MultiLine" Width="67%"></asp:TextBox>
                             <a href='<%= Me.ResolveUrl("mailto:" + txtSvcCP1Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;">
                             <img height="20" src="Images/email1.png" width="20" />
                             </a></td>
                     </tr>
                     <tr>
                         <td colspan="4">
                             <br />
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">ContactPerson2</td>
                         <td class="CellTextBox" colspan="1">
                             <asp:TextBox ID="txtSvcCP2Contact" runat="server" Height="16px" MaxLength="100" Width="90%"></asp:TextBox>
                         </td>
                         <td class="CellFormat" style="width: 8%">Position</td>
                         <td class="CellTextBox" colspan="1">
                             <asp:TextBox ID="txtSvcCP2Position" runat="server" Height="16px" MaxLength="100" Width="40%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Telephone</td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtSvcCP2Telephone" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                         </td>
                         <td class="CellFormat" style="width: 8%">Fax</td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtSvcCP2Fax" runat="server" Height="16px" MaxLength="50" Width="40%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Telephone2</td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtSvcCP2Tel2" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                         </td>
                         <td class="CellFormat" style="width: 8%">Mobile</td>
                         <td class="CellTextBox">
                             <asp:TextBox ID="txtSvcCP2Mobile" runat="server" Height="16px" MaxLength="50" Width="40%"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Email</td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtSvcCP2Email" runat="server" Font-Names="calibri" Font-Size="15px" Height="50px" MaxLength="100" TextMode="MultiLine" Width="67%"></asp:TextBox>
                             <a href='<%= Me.ResolveUrl("mailto:" + txtSvcCP2Email.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;">
                             <img height="20" src="Images/email1.png" width="20" />
                             </a></td>
                     </tr>
                     <tr>
                         <td class="CellFormat">Other Emails</td>
                         <td class="CellTextBox" colspan="3">
                             <asp:TextBox ID="txtOtherEmail" runat="server" Font-Names="calibri" Font-Size="15px" Height="55px" MaxLength="500" TextMode="MultiLine" Width="67%"></asp:TextBox>
                             <a href='<%= Me.ResolveUrl("mailto:" + txtOtherEmail.Text)%>' style="font-weight: bold; color: #0000FF; font-size: 18px;">
                             <img height="20" src="Images/email1.png" width="20" />
                             </a></td>
                     </tr>
                 </table>
             </td>
         </tr>
         <tr>
             <td colspan="5">
                 <br />
             </td>
         </tr>
         <tr>
             <td colspan="5" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Schedule &amp; Assignment</td>
         </tr>
         <tr>
             <td>
                 <asp:CheckBox ID="chkAccessEdit" runat="server" CssClass="CellFormat" Visible="False" />
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:CheckBox ID="chkUrgent" runat="server" CssClass="CellFormat" Text="Urgent" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">ScheduleType</td>
             <td class="CellTextBox" colspan="4">
                 <asp:DropDownList ID="ddlScheduleType" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="upper(ScheduleType)" DataValueField="upper(ScheduleType)" Height="20px" Width="20%">
                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                 </asp:DropDownList>
                 <asp:ListSearchExtender ID="ddllsScheduleType" runat="server" Enabled="True" PromptPosition="Bottom" TargetControlID="ddlScheduleType">
                 </asp:ListSearchExtender>
                 <asp:ImageButton ID="btnEditSchType" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">ScheduleDate<asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtScheduleDate" runat="server" Height="16px" MaxLength="10" Width="20%"></asp:TextBox>
                 <asp:TextBox ID="txtOldScheduleDate" runat="server" Height="16px" MaxLength="10" Width="20%" CssClass="dummybutton"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtScheduleDate" TargetControlID="txtScheduleDate">
                 </asp:CalendarExtender>
                 <asp:ImageButton ID="btnEditSchDate" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">ScheduleTimeFrom</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtScheduleTimeIn" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="65px"></asp:TextBox>
                 <asp:TextBox ID="txtScheduleTimeOut" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="65px"></asp:TextBox>
                 <asp:MaskedEditExtender ID="txtScheduleTimeIn_MaskedEditExtender" runat="server" AutoComplete="False" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtScheduleTimeIn">
                 </asp:MaskedEditExtender>
                 <asp:MaskedEditExtender ID="txtScheduleTimeOut_MaskedEditExtender" runat="server" AutoComplete="False" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtScheduleTimeOut">
                 </asp:MaskedEditExtender>

                  <asp:TextBox ID="txtOldScheduleTimeIn" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="65px" CssClass="dummybutton"></asp:TextBox>
                 <asp:TextBox ID="txtOldScheduleTimeOut" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="65px" CssClass="dummybutton"></asp:TextBox>
                
             </td>
         </tr>
         <tr>
             <td class="CellFormat">AllocatedTime<asp:Label ID="Label24" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtAllocatedTime" runat="server" Height="16px" MaxLength="18" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">Team</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtTeam" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox>
                 <asp:ImageButton ID="btnTeam" runat="server" CssClass="righttextbox" Height="22px" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                 <asp:ModalPopupExtender ID="mdlPopUpTeamNew" runat="server" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopUpTeamNew" TargetControlID="btndummyNew">
                 </asp:ModalPopupExtender>
                 <asp:ModalPopupExtender ID="mdlPopUpTeam" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlTeamClose" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopUpTeam" TargetControlID="btndummy">
                 </asp:ModalPopupExtender>
                 <asp:ImageButton ID="btnEditTeam" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">Team Incharge</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtIncharge" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">ServiceBy</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtServiceBy" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">Supervisor</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtSupervisor" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">VehicleNo</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtVehNo" runat="server" Height="16px" MaxLength="25" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">ServiceDescription<br />
                 <br />
                 <asp:TextBox ID="txtCharCountServiceDescription" runat="server" BackColor="White" BorderStyle="None" Enabled="False" ForeColor="Red" style="text-align:right" Visible="False"> 500 characters left</asp:TextBox>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtDescription" runat="server" Font-Names="Calibri" Font-Size="12pt" Height="60px" MaxLength="2000" onKeyup="return CalculateCharsDescription(this);" TextMode="MultiLine" Width="80%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">ServiceInstruction<br />
                 <br />
                 <asp:TextBox ID="txtCharCountSI" runat="server" BackColor="White" BorderStyle="None" Enabled="False" ForeColor="Red" style="text-align:right" Visible="False"> 500 characters left</asp:TextBox>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtInstruction" runat="server" Font-Names="Calibri" Font-Size="12pt" Height="140px" MaxLength="500" onKeyup="return CalculateCharsInstruction(this);" TextMode="MultiLine" Width="80%"></asp:TextBox>
                 <asp:ImageButton ID="btnEditServInst" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">Remarks<br />
                 <br />
                 <asp:TextBox ID="txtCharCountRemarks" runat="server" BackColor="White" BorderStyle="None" Enabled="False" ForeColor="Red" style="text-align:right" Visible="False"> 500 characters left</asp:TextBox>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtRemarks" runat="server" Font-Names="Calibri" Font-Size="12pt" Height="40px" MaxLength="4000" onKeyup="return CalculateCharsRemarks(this);" TextMode="MultiLine" Width="80%"></asp:TextBox>
                 <asp:ImageButton ID="btnEditRemarks" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
             </td>
         </tr>
         <tr>
             <td class="CellFormat">Billing Description<br />
                 <br />
                 <asp:TextBox ID="txtCharCountBillingDescription" runat="server" BackColor="White" BorderStyle="None" Enabled="False" ForeColor="Red" style="text-align:right" Visible="False">500 characters left</asp:TextBox>
             </td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtBillingDescription" runat="server" Font-Names="Calibri" Font-Size="12pt" Height="40px" MaxLength="2000" onKeyup="return CalculateCharsBillingDescription(this);" TextMode="MultiLine" Width="80%"></asp:TextBox>
             </td>
         </tr>





           <tr>
             <td colspan="5">
                 <br />
             </td>
         </tr>
         <tr>
             <td colspan="5" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Report Service Info</td>
         </tr>
         <tr>
             <td class="CellFormat">Report Service Start Date</td>
             <td class="CellTextBox" colspan="1">
                 <asp:TextBox ID="txtReportServiceStart" runat="server" Height="16px" MaxLength="10" Width="47%" AutoPostBack="True"></asp:TextBox>
               <asp:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtReportServiceStart" TargetControlID="txtReportServiceStart">
                 </asp:CalendarExtender>
             </td>
             <td class="CellFormat" colspan="2" style="width: 8%">Start Time</td>
             <td class="CellTextBox" colspan="1">
                 <asp:TextBox ID="txtReportServiceStartTime" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="38%" AutoPostBack="True"></asp:TextBox>
                 <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtReportServiceStartTime">
                 </asp:MaskedEditExtender>
             </td>
             <td></td>
         </tr>


          <tr>
             <td class="CellFormat">Report Service End Date</td>
             <td class="CellTextBox" colspan="1">
                 <asp:TextBox ID="txtReportServiceEnd" runat="server" Height="16px" MaxLength="10" Width="47%" AutoPostBack="True"></asp:TextBox>
               <asp:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtReportServiceEnd" TargetControlID="txtReportServiceEnd">
                 </asp:CalendarExtender>
             </td>
             <td class="CellFormat" colspan="2" style="width: 8%">End Time</td>
             <td class="CellTextBox" colspan="1">
                 <asp:TextBox ID="txtReportServiceEndTime" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="38%" AutoPostBack="True"></asp:TextBox>
                 <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtReportServiceEndTime">
                 </asp:MaskedEditExtender>
             </td>
             <td></td>
         </tr>

         <tr>
             <td colspan="5">
                 <br />
             </td>
         </tr>
      

        <%-- 'aa--%>

        
         <tr>
             <td colspan="5" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%;">Actual Service Info</td>
         </tr>
         <tr>
             <td class="CellFormat">Service Start Date</td>
             <td class="CellTextBox" colspan="1">
                 <asp:TextBox ID="txtActSvcDate" runat="server" Height="16px" MaxLength="10" Width="47%"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtActSvcDate" TargetControlID="txtActSvcDate">
                 </asp:CalendarExtender>
             </td>
             <td class="CellFormat" colspan="2" style="width: 8%">Start Time</td>
             <td class="CellTextBox" colspan="1">
                 <asp:TextBox ID="txtActSvcTimeFrom" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="38%"></asp:TextBox>
                 <asp:MaskedEditExtender ID="txtActSvcTimeFrom_MaskedEditExtender" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtActSvcTimeFrom">
                 </asp:MaskedEditExtender>
             </td>
             <td></td>
         </tr>
         <tr>
             <td class="CellFormat">Service End Date</td>
             <td class="CellTextBox" colspan="1">
                 <asp:TextBox ID="txtActSvcEndDate" runat="server" Height="16px" MaxLength="10" Width="47%"></asp:TextBox>
                 <asp:CalendarExtender ID="txtActSvcEndDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtActSvcEndDate" TargetControlID="txtActSvcEndDate">
                 </asp:CalendarExtender>
             </td>
             <td class="CellFormat" colspan="2" style="width: 8%">End Time</td>
             <td class="CellTextBox" colspan="1">
                 <asp:TextBox ID="txtActSvcTimeTo" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="38%"></asp:TextBox>
                 <asp:MaskedEditExtender ID="txtActSvcTimeTo_MaskedEditExtender" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtActSvcTimeTo">
                 </asp:MaskedEditExtender>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">TotalServiceTime</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtServiceTime" runat="server" Height="16px" MaxLength="18" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">TotalActualDuration</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtTotActualDuration" runat="server" Enabled="False" Height="16px" MaxLength="18" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr style="display:none">
             <td class="CellFormat">Report Service Start</td>
             <td class="CellTextBox">&nbsp;</td>
             <td class="CellFormat">Report Service End</td>
             <td class="CellTextBox" colspan="2">&nbsp;</td>
         </tr>
         <tr>
             <td class="CellFormat">ServiceQualityRating</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtServiceRating" runat="server" Height="16px" MaxLength="10" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat">Specific Location</td>
             <td class="CellTextBox" colspan="4">
                 <asp:TextBox ID="txtSpecificLocation" runat="server" Height="16px" MaxLength="500" Width="20%"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td colspan="5">
                 <br />
             </td>
         </tr>

       <%--  aa--%>

         <tr>
             <td colspan="5">
                 <br />
             </td>
         </tr>
       
         <tr>
             <td colspan="5" style="text-align:right;">
                 <asp:Button ID="btnSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" />
                 <asp:Button ID="btnCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" />
                 <asp:TextBox ID="txtFrom" runat="server" Height="16px" MaxLength="25" Visible="False" Width="1%"></asp:TextBox>
                 <asp:TextBox ID="txtRcnoContact" runat="server" Height="16px" MaxLength="25" Visible="False" Width="1%"></asp:TextBox>
             </td>
         </tr>
    </tr>
 
    </table><div style="text-align:center"><asp:LinkButton ID="btnTop" runat="server" Font-Bold="True" Font-Names="Calibri" ForeColor="Brown">Go to Top</asp:LinkButton></div></div>
</ContentTemplate>
</asp:TabPanel>
                                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Service Details"><HeaderTemplate>
<asp:Label ID="lblSvcDetailsCount" runat="server" Font-Size="13px" Text="Service Details"></asp:Label>
</HeaderTemplate>
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div class="modal">
            <div class="center">
                <img alt="" src="Images/loader.gif" />
            </div></div>
    </ProgressTemplate>
</asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="padding-top:5px;width:100%">
                <tr><td colspan="2" style="text-align:left;">
                    <asp:Button ID="btnSvcAdd" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="ADD" Width="90px" />
                    <asp:Button ID="btnSvcEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" VISIBLE="true" Width="90px" />
                    <asp:Button ID="btnSvcDelete" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="ConfirmDeleteSvcDetail()" Text="DELETE" Width="90px" />
                     <asp:Button ID="btnClientImport" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="IMPORT CLIENT REQUEST" Width="200px" />

                    </td></tr>
                <tr><td class="CellFormatSearch" style="width:350px">RecordNo</td>
                    <td class="CellTextBox"><asp:Label ID="lblRecordNo1" runat="server" BackColor="#CCCCCC" Height="20px" width="30%"></asp:Label></td></tr>
                <tr><td class="CellFormatSearch" style="width:350px">ContractNo</td>
                    <td class="CellTextBox"><asp:Label ID="lblContractNo" runat="server" BackColor="#CCCCCC" Height="20px" width="30%"></asp:Label></td></tr>
                <tr><td class="CellFormatSearch" style="width:350px">TotalServiceArea</td>
                    <td class="CellTextBox"><asp:Label ID="lblTotalServiceArea" runat="server" BackColor="#CCCCCC" Height="20px" width="30%"></asp:Label></td></tr>
                <tr><td class="CellFormatSearch" style="width:350px">TotalAreaCompleted</td>
                    <td class="CellTextBox"><asp:Label ID="lblTotalAreaCompleted" runat="server" BackColor="#CCCCCC" Height="20px" width="30%"></asp:Label></td></tr>
                <tr><td class="CellFormatSearch" style="width:350px">TotalPriceOfAreaCompleted</td>
                    <td class="CellTextBox"><asp:Label ID="lblTotalPrice" runat="server" BackColor="#CCCCCC" Height="20px" width="30%"></asp:Label></td></tr>
                <tr><td colspan="2" style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';">
                    <asp:Label ID="txtSvcMode" runat="server" CssClass="dummybutton"></asp:Label></td></tr><tr><td colspan="2"><br /></td></tr>
                <tr>
                    <td style="text-align: center;" colspan="2">
                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Centered" DataKeyNames="Rcno" DataSourceID="SqlDataSource2" Font-Size="15px" ForeColor="#333333" GridLines="Vertical" OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged2" Width="722px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField HeaderText="Select" ShowHeader="True" ShowselectButton="True" Visible="False"><controlstyle width="40px" /><ItemStyle Width="40px" /></asp:CommandField>
                                <asp:TemplateField HeaderText="ServiceID" ItemStyle-Width="130">
                                    <ItemTemplate><asp:Label ID="lblServiceID" runat="server" Text='<%# Eval("ServiceID")%>' Visible="true" Width="130px" /></ItemTemplate>
                                    <EditItemTemplate><asp:Label ID="lblServiceID1" runat="server" Text='<%# Eval("ServiceID")%>' Visible="false" Width="130px" /><asp:DropDownList ID="ddlServices" runat="server" AutoPostBack="true" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" OnTextChanged="svc_TextChanged" Width="130px"></asp:DropDownList></table></EditItemTemplate>
                                    <ItemStyle Width="130px" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="ServiceDescription" ItemStyle-Width="230">
                                    <ItemTemplate><asp:Label ID="lblServiceDescription" runat="server" Text='<%# Eval("ProductServices")%>' Visible="TRUE" width="230px" /></ItemTemplate>
                                    <ItemStyle Width="230px" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="ServiceFrequency" ItemStyle-Width="150">
                                    <ItemTemplate><asp:Label ID="lblFrequency" runat="server" Text='<%# Eval("Frequency")%>' Visible="true" width="150px" /></ItemTemplate>
                                    <EditItemTemplate><asp:Label ID="lblFrequency1" runat="server" Text='<%# Eval("Frequency")%>' Visible="FALSE" width="150px" />
                                        <asp:DropDownList ID="ddlFrequency" runat="server" DataSourceID="SqlDSFrequency" DataTextField="Frequency" DataValueField="Frequency" Width="150px"></asp:DropDownList></EditItemTemplate>
                                    <ItemStyle Width="150px" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="TargetID" ItemStyle-Width="200px">
                                    <ItemTemplate><asp:Label ID="lblTargetID" runat="server" Text='<%# Eval("TargetID")%>' Visible="TRUE" Width="200px" /></ItemTemplate>
                                    <EditItemTemplate><cc1:DropDownCheckBoxes ID="ddlchkTarget" runat="server" AddJQueryReference="False" DataSourceID="SqlDSTarget" DataTextField="TargetIDDesc" DataValueField="Descrip1" OnTextChanged="target_TextChanged" UseButtons="True" UseSelectAllNode="false" Width="200px"><style2 dropdownboxboxheight="200" dropdownboxboxwidth="200px" selectboxwidth="200px" /></cc1:DropDownCheckBoxes>
                                        <asp:Label ID="lblTargetID1" runat="server" Text='<%# Eval("TargetID")%>' Visible="TRUE" Width="200px" />
                                        <asp:Label ID="lblReason" runat="server" Visible="false" Width="200px" />
                                        <div style="height:100px;overflow:auto;display:none;">
                                            <asp:CheckBoxList ID="chkTarget" runat="server" CssClass="CheckBoxList" DataSourceID="SqlDSTarget" DataTextField="TargetIDDesc" DataValueField="Descrip1" Height="60px" Width="250px"></asp:CheckBoxList>
                                            <asp:Button ID="btnOk" runat="server" OnClick="ok_click" Text="ok" /></div></EditItemTemplate>
                                    <ItemStyle Width="200px" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="TargetDescription" ItemStyle-Width="350px">
                                    <ItemTemplate><asp:Label ID="txtTargetDesc" runat="server" Width="350px">
                </asp:Label></ItemTemplate>
                                    <ItemStyle Width="350px" /></asp:TemplateField>
                                <asp:CommandField ButtonType="Link" ControlStyle-CssClass="dummybutton" HeaderStyle-CssClass="dummybutton" ItemStyle-CssClass="dummybutton" itemStyle-Width="100" ShowDeleteButton="true" ShowEditButton="true" ShowInsertButton="false">
                                    <controlstyle cssclass="dummybutton" />
                                    <HeaderStyle CssClass="dummybutton" />
                                    <ItemStyle Width="100px" /></asp:CommandField>
                                <asp:TemplateField SortExpression="Rcno"><EditItemTemplate><asp:Label ID="lblRcno" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="lblRcno" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" />
                                    <ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="Action">
                                    <EditItemTemplate><asp:TextBox ID="lblAction1" runat="server" Text='<%# Bind("Action") %>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblAction" runat="server" Text='<%# Bind("Action") %>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" />
                                    <ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="RemarkOffice">
                                    <EditItemTemplate><asp:TextBox ID="txtRemarkO" runat="server" Text='<%# Bind("RemarkOffice") %>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblRemarkO" runat="server" Text='<%# Bind("RemarkOffice") %>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="RemarkClient">
                                    <EditItemTemplate><asp:TextBox ID="txtRemarkC" runat="server" Text='<%# Bind("RemarkClient") %>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblRemarkC" runat="server" Text='<%# Bind("RemarkClient") %>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="ServiceArea">
                                    <EditItemTemplate><asp:TextBox ID="txtServiceArea" runat="server" Text='<%# Bind("ServiceArea")%>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblServiceArea" runat="server" Text='<%# Bind("ServiceArea")%>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="PricePerServiceArea">
                                    <EditItemTemplate><asp:TextBox ID="txtPricePerServiceArea" runat="server" Text='<%# Bind("PricePerServiceArea")%>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblPricePerServiceArea" runat="server" Text='<%# Bind("PricePerServiceArea")%>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="AreaCompleted">
                                    <EditItemTemplate><asp:TextBox ID="txtAreaCompleted" runat="server" Text='<%# Bind("AreaCompleted")%>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblAreaCompleted" runat="server" Text='<%# Bind("AreaCompleted")%>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="PriceOfAreaCompleted">
                                    <EditItemTemplate><asp:TextBox ID="txtPriceOfAreaCompleted" runat="server" Text='<%# Bind("PriceOfAreaCompleted")%>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblPriceOfAreaCompleted" runat="server" Text='<%# Bind("PriceOfAreaCompleted")%>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="BaitStationInstalled">
                                    <EditItemTemplate><asp:TextBox ID="txtBaitStationInstalled" runat="server" Text='<%# Bind("BaitStationInstalled")%>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblBaitStationInstalled" runat="server" Text='<%# Bind("BaitStationInstalled")%>'></asp:Label></ItemTemplate><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="PricePerBaitStation"><EditItemTemplate><asp:TextBox ID="txtPricePerBaitStation1" runat="server" Text='<%# Bind("PricePerBaitStation")%>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblPricePerBaitStation1" runat="server" Text='<%# Bind("PricePerBaitStation")%>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:TemplateField SortExpression="TotalPriceForBaitStation">
                                    <EditItemTemplate><asp:TextBox ID="txtTotalPriceForBaitStation1" runat="server" Text='<%# Bind("TotalPriceForBaitStation")%>'></asp:TextBox></EditItemTemplate>
                                    <ItemTemplate><asp:Label ID="lblTotalPriceForBaitStation1" runat="server" Text='<%# Bind("TotalPriceForBaitStation")%>'></asp:Label></ItemTemplate>
                                    <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField><asp:TemplateField SortExpression="Material">
                                        <EditItemTemplate><asp:TextBox ID="txtMaterials" runat="server" Text='<%# Bind("Material")%>'></asp:TextBox></EditItemTemplate>
                                        <ItemTemplate><asp:Label ID="lblMaterial" runat="server" Text='<%# Bind("Material") %>'></asp:Label></ItemTemplate>
                                        <HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:TemplateField>
                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" /><asp:BoundField DataField="CreatedOn" HeaderText="Created On" />
                                <asp:BoundField DataField="LastModifiedBy" HeaderText="Modified By" />
                                <asp:BoundField DataField="LastModifiedOn" HeaderText="Last Modified On" />
                                    
                          
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" />
                            <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
                            <SelectedRowStyle BackColor="#00ccff" Font-Bold="True" ForeColor="#333333" />
                            <sortedascendingcellstyle backcolor="#F5F7FB" />
                            <sortedascendingheaderstyle backcolor="#6D95E1" />
                            <sorteddescendingcellstyle backcolor="#E9EBEF" />
                            <sorteddescendingheaderstyle backcolor="#4870BE" />

                        </asp:GridView>
                        <table id="tableaddHeader" runat="server" border="1" cellpadding="2" cellspacing="2" class="Centered" style="border-collapse: collapse; background-color: #F5F5F5; font-family: CALIBRI; text-align: center; font-weight: bold; font-size: 15px; color: #000000;padding-left:10px;">
                                   <tr><td style="width: 130px;text-align:center">SERVICE ID </td>
<td style="width: 230px">ServiceDescription </td>
<td style="width: 150px">Frequency </td>
<td style="width: 200px">TargetID </td>
<td style="width: 350px">TargetDescription </td>
</tr>
</table>
<table id="tableadd" runat="server" border="1" cellpadding="2" cellspacing="2" class="Centered" style="border-collapse: collapse; background-color: #F5F5F5;">
<tr><td style="width: 30px"></td>
<td style="width: 150px"><asp:DropDownList ID="txtServiceIDadd" runat="server" AutoPostBack="True" DataSourceID="SqlDSService" DataTextField="ProductID" DataValueField="ProductDesc" Width="130px"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
<td style="width: 250px"><asp:DropDownList ID="txtServiceDescadd" runat="server" DataSourceID="SqlDSService" DataTextField="ProductDesc" DataValueField="ProductDesc" Enabled="False" Width="230px"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
<td style="width: 150px"><asp:DropDownList ID="txtFrequencyadd" runat="server" DataTextField="Frequency" DataValueField="Frequency" Width="150px"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
<td style="width: 200px"><asp:Label ID="txtTargetIDAdd" runat="server" CssClass="dummybutton" ForeColor="Black" Width="200px"></asp:Label>
<asp:Label ID="txtReasonAdd" runat="server" CssClass="dummybutton" ForeColor="Black" Visible="false" Width="200px"></asp:Label>
<cc1:DropDownCheckBoxes ID="ddlchkTargetAdd" runat="server" AddJQueryReference="False" DataSourceID="SqlDSTarget" DataTextField="TargetIDDesc" DataValueField="Descrip1" OnTextChanged="targetAdd_TextChanged" UseButtons="True" UseSelectAllNode="false" Width="200px"><style2 dropdownboxboxheight="200" dropdownboxboxwidth="200px" selectboxwidth="200px" /></cc1:DropDownCheckBoxes></td>
<td style="width: 400px"><asp:TextBox ID="txtTargetDescAdd" runat="server" Width="350px"></asp:TextBox></td>
</tr>
</table><br />
<asp:Table ID="tableedit" runat="server">
<asp:TableRow Font-Bold="True" Font-Names="Calibri" Font-Size="14px" ForeColor="Black">
<asp:TableCell>ServiceID</asp:TableCell>
<asp:TableCell>ServiceDescription</asp:TableCell>
<asp:TableCell>ServiceFrequency</asp:TableCell>
<asp:TableCell>TargetID</asp:TableCell>
<asp:TableCell>TargetDescription</asp:TableCell></asp:TableRow>
<asp:TableRow>
<asp:TableCell><br /><asp:DropDownList ID="ddlServiceID" runat="server" AppendDataBoundItems="true" AutoPostBack="True" DataTextField="ProductID" DataValueField="ProductDesc" Width="130px"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></asp:TableCell>
<asp:TableCell><br /><asp:TextBox ID="ddlServiceDesc" runat="server" Enabled="False" Width="230px"></asp:TextBox></asp:TableCell>
<asp:TableCell><br /><asp:DropDownList ID="txtFrequencyedit" runat="server" DataTextField="Frequency" DataValueField="Frequency" Width="150px"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></asp:TableCell>
<asp:TableCell> <asp:Label ID="txtTargetIDEdit" runat="server" CssClass="dummybutton" ForeColor="Black" Width="200px"></asp:Label>
                       <asp:Label ID="txtReasonEdit" runat="server" CssClass="dummybutton" ForeColor="Black" Visible="FALSE" Width="200px"></asp:Label>
               <br /><cc1:DropDownCheckBoxes ID="ddlchkTargetEdit" runat="server" AddJQueryReference="False" DataSourceID="SqlDSTarget" DataTextField="TargetIDDesc" DataValueField="Descrip1" OnTextChanged="targetchkAdd_TextChanged" UseButtons="True" UseSelectAllNode="false" Width="200px"><style2 dropdownboxboxheight="200" dropdownboxboxwidth="200px" selectboxwidth="200px" /></cc1:DropDownCheckBoxes>
                     
                   </asp:TableCell>
<asp:TableCell><br /><asp:TextBox ID="txtTargetDescEdit" runat="server" Enabled="false" Width="350px"></asp:TextBox></asp:TableCell></asp:TableRow></asp:Table>
<asp:TextBox ID="txtEditIndex" runat="server" CssClass="dummybutton"></asp:TextBox>
</td>
</tr>
<tr><td colspan="2"><br /></td>
</tr>

<tr><td class="CellFormatSearch" style="width:350px">Action / Service Performed </td>
<td class="CellTextBox"><asp:TextBox ID="txtAction" runat="server" Font-Names="Calibri" Height="60px" MaxLength="3" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormatSearch" style="width:350px">Materials/Chemicals Used </td>
<td class="CellTextBox"><asp:TextBox ID="txtMaterials" runat="server" Font-Names="Calibri" Height="60px" MaxLength="3" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormatSearch" style="width:350px">Remarks to Client </td>
<td class="CellTextBox"><asp:TextBox ID="txtRemarkClient" runat="server" Font-Names="Calibri" Height="60px" MaxLength="3" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormatSearch" style="width:350px">Remarks to Office </td>
<td class="CellTextBox"><asp:TextBox ID="txtRemarkOffice" runat="server" Font-Names="Calibri" Height="60px" MaxLength="3" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
</tr>
<tr><td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Service Area Details</td>
</tr>
<tr><td class="CellFormat" style="width:350px">ServiceArea </td>
<td class="CellTextBox" colspan="1"><asp:TextBox ID="txtServiceArea" runat="server" Height="16px" MaxLength="100" style="text-align:right;" Width="25%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat" style="width:350px">PricePerServiceArea </td>
<td class="CellTextBox" colspan="1"><asp:TextBox ID="txtPricePerServiceArea" runat="server" Height="16px" MaxLength="100" style="text-align:right;" Width="25%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat" style="width:350px">AreaCompleted </td>
<td class="CellTextBox" colspan="1"><asp:TextBox ID="txtAreaCompleted1" runat="server" Height="16px" MaxLength="100" style="text-align:right;" Width="25%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat" style="width:350px">TotalPriceOfAreaCompleted </td>
<td class="CellTextBox" colspan="1"><asp:TextBox ID="txtTotalPriceOfAreaCompleted" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" style="text-align:right;" Width="25%"></asp:TextBox></td>
</tr>
<tr><td colspan="2"><br /></td>
</tr>
                <tr><td colspan="2">
<asp:TextBox ID="txtNoofBaitStation" runat="server" Height="16px" MaxLength="100" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                    <asp:TextBox ID="txtPricePerBaitStaion" runat="server" Height="16px" MaxLength="100" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                    <asp:TextBox ID="txtTotalPriceofBaitStation" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                   <asp:TextBox ID="txtServiceBy2" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                       <asp:TextBox ID="txtSvcRequestNo" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                
                </td></tr>
                    <tr><td colspan="2"><br /></td>
</tr>
<tr><td colspan="2" style="text-align:right;"><asp:Button ID="btnSvcSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" />
    <asp:Button ID="btnSvcCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" /></td>
</tr>
</table></ContentTemplate>
       
    </asp:UpdatePanel>
</ContentTemplate>

</asp:TabPanel>


                                

                                   <asp:TabPanel ID="TabPanel9" runat="server" HeaderText="Pest Count"><HeaderTemplate>
                                    <asp:Label ID="lblPestRowCount" runat="server" Font-Size="13px" Text="Pest Count"></asp:Label>
</HeaderTemplate>
<ContentTemplate>
<table style="padding-top:5px;width:100%;"><tr><td colspan="2" style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"><asp:Label ID="txtPestCountMode" runat="server" CssClass="dummybutton"></asp:Label>
</td></tr><tr style="vertical-align: middle"><td colspan="2" style="text-align:left;"><asp:Button ID="btnPestCountAdd" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="ADD" Width="90px" />
<asp:Button ID="btnPestCountEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="EDIT" Width="90px" />
<asp:Button ID="btnPestCountDelete" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="Confirm()" Text="DELETE" Width="90px" />
</td></tr><tr><td colspan="2"><br /></td></tr><tr><td colspan="2" style="text-align:center">
    <asp:GridView ID="GrdViewPestCount" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="12pt" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
<ControlStyle Width="50px" />

<ItemStyle Width="50px" />
</asp:CommandField>
<asp:BoundField DataField="PestID" HeaderText="Pest ID" SortExpression="PestID">
<HeaderStyle Width="150px" />

<ItemStyle Width="150px" Wrap="False" />
</asp:BoundField>
<asp:BoundField DataField="Qty" DataFormatString="{0:f2}" HeaderText="Qty" SortExpression="Qty">
<ItemStyle Width="100px" />
</asp:BoundField>
<asp:BoundField DataField="LevelofInfestation" >
    <ControlStyle CssClass="dummybutton" />
    <HeaderStyle CssClass="dummybutton" />
    <ItemStyle CssClass="dummybutton" />
    </asp:BoundField>
<asp:BoundField DataField="Gender" HeaderText="Gender" />
<asp:BoundField DataField="LifeStage" HeaderText="LifeStage" />
<asp:BoundField DataField="Species" HeaderText="Species" />
<asp:BoundField DataField="TrapType" HeaderText="Trap Type" />
<asp:BoundField DataField="TrapSerialNo" HeaderText="Trap Serail No." />
<asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" >
<HeaderStyle Width="250px" />

<ItemStyle Width="250px" Wrap="False" HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks" >
    <ItemStyle HorizontalAlign="Left" />
    </asp:BoundField>
<asp:BoundField DataField="RecordNo" HeaderText="RecordNo" SortExpression="RecordNo" Visible="False" />
<asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate>
<asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
<asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False" />
<asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False" />
<asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False" />
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
</td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="width:350px">RecordNo </td><td class="CellTextBox" colspan="1"><asp:Label ID="lblRecordNoPestCount" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="40%"></asp:Label>
</td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="width:350px">Pest ID <asp:Label ID="Label49" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
</td><td class="CellTextBox" colspan="1"><asp:DropDownList ID="ddlPestID" runat="server" AppendDataBoundItems="True" DataTextField="IDNAME" DataValueField="ItemID" Width="40%" AutoPostBack="True"><asp:ListItem Text="--SELECT--" Value="-1" />
</asp:DropDownList>
</td></tr><tr><td class="CellFormat" style="width:350px">Quantity </td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtQuantityPC" runat="server" Height="16px" MaxLength="100" Width="40%"></asp:TextBox>
</td></tr><tr style="display:none"><td class="CellFormat" style="width:350px">Infestation Level</td><td class="CellTextBox" colspan="1"><asp:DropDownList ID="ddlInfestationLevel" runat="server" AppendDataBoundItems="True" DataTextField="IDNAME" DataValueField="ItemID" Width="40%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
        <asp:ListItem>N.A</asp:ListItem>
</asp:DropDownList>
</td></tr><tr><td class="CellFormat" style="width:350px">Gender</td><td class="CellTextBox" colspan="1"><asp:DropDownList ID="ddlGender" runat="server" AppendDataBoundItems="True" DataTextField="IDNAME" DataValueField="ItemID" Width="40%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
<asp:ListItem>N.A</asp:ListItem>
</asp:DropDownList>
</td></tr><tr><td class="CellFormat" style="width:350px">Life Stage</td><td class="CellTextBox" colspan="1"><asp:DropDownList ID="ddlLifeStage" runat="server" AppendDataBoundItems="True" DataTextField="IDNAME" DataValueField="ItemID" Width="40%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
<asp:ListItem>N.A</asp:ListItem>
</asp:DropDownList>
</td></tr><tr><td class="CellFormat" style="width:350px">Species</td><td class="CellTextBox" colspan="1"><asp:DropDownList ID="ddlSpecies" runat="server" AppendDataBoundItems="True" DataTextField="IDNAME" DataValueField="ItemID" Width="40%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
<asp:ListItem>N.A</asp:ListItem>
</asp:DropDownList>
</td></tr><tr><td class="CellFormat" style="width:350px">Section</td><td class="CellTextBox" colspan="1">
        <asp:DropDownList ID="ddlFloorLevel" runat="server" AppendDataBoundItems="True" DataTextField="IDNAME" DataValueField="ItemID" Width="40%">
            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
        </asp:DropDownList>
        </td></tr><tr><td class="CellFormat" style="width:350px">Location</td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtLocationPC" runat="server" Height="16px" MaxLength="100" Width="40%"></asp:TextBox>
</td></tr><tr><td class="CellFormat" style="width:350px">Trap type</td><td class="CellTextBox" colspan="1"><asp:DropDownList ID="ddlTrapType" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataTextField="IDNAME" DataValueField="ItemID" Width="40%"><asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
</asp:DropDownList>
</td></tr><tr><td class="CellFormat" style="width:350px">Trap Serial No.</td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtTrapSerialNo" runat="server" Height="16px" MaxLength="100" Width="40%"></asp:TextBox>
</td></tr><tr><td style="width: 350px;" class="CellFormat">Remarks</td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtRemarksPC" runat="server" Height="16px" MaxLength="100" Width="40%"></asp:TextBox>
</td></tr><tr><td colspan="2" style="text-align:right;"><asp:Button ID="btnPestCountSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" />
<asp:Button ID="btnPestCountCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" />
</td></tr>

      <tr ><td class="left" colspan="2"><asp:Button ID="btnPestCountPhotos" runat="server" Text="View Uploaded Photos" /><br /><br /></td></tr>
    <tr><td class="CellFormat">Select Photo to Upload </td><td class="CellTextBox" colspan="1" style="text-align:center">
        <asp:FileUpload ID="FileUpload3" runat="server" CssClass="Centered" Width="100%" AllowMultiple="True" /></td></tr>
    <tr><td class="CellFormat">Description </td><td class="CellTextBox" colspan="1" style="text-align:left"><asp:TextBox ID="txtPestPhotoDesc" runat="server" Width="70%"></asp:TextBox></td></tr>
    <%--  <tr><td class="CellFormat">LargePhoto </td><td class="CellTextBox" colspan="1" style="text-align:left">
          <asp:CheckBox ID="CheckBox1" runat="server" /></td></tr>--%>
  
    <tr><td class="centered" colspan="2"><asp:Button ID="btnPestPhotoUpload" runat="server" Text="Upload" /></td></tr>
    <tr><td colspan="2" style="text-align:right;padding-right:10%"><br /><asp:Button ID="btnDownloadPestPhotos" runat="server" Text="Download All Photos" /></td></tr>
    <tr><td colspan="2" style="width:100%">
     <asp:GridView ID="gvPestImages" runat="server" AutoGenerateColumns="False" CssClass="Centered" ForeColor="#333333" GridLines="Vertical" AllowPaging="True" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="S.No."><ItemTemplate> <asp:Label ID="lblPestRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" /></ItemTemplate></asp:TemplateField>
              <%--  <asp:TemplateField HeaderText="LargePhoto"> 
                      <ItemTemplate>
           <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
     </ItemTemplate>
               <ItemTemplate>
                         <asp:CheckBox ID="chkSelectLargePhotoGV" runat="server" Checked='<%# Convert.ToBoolean(Eval("LargePhoto"))%>' Enabled="false"/>
</ItemTemplate>
                                                          </asp:TemplateField> 
                 <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkUpdateLargePhoto" runat="server" CommandArgument='<%# Eval("Rcno")%>' OnClick="UpdateLargePhotoField" Text="Update Large photo" /></ItemTemplate></asp:TemplateField>
    
       --%>         <%--    <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Image" Visible="true"><ItemTemplate><asp:Image ID="PestImageView" runat="server" Height="180" Width="280" /></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkPestRotate" runat="server" CommandArgument='<%# Eval("Rcno")%>' OnClick="RotateImage" Text="Rotate" /></ItemTemplate></asp:TemplateField>
     
            <asp:TemplateField HeaderText="Preview Image" Visible="False"><ItemTemplate><asp:ImageButton ID="PestImageButton1" runat="server" Height="200px" Visible="false" Width="200px" /></ItemTemplate></asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkPestEditPhotoDesc" runat="server" CommandArgument='<%# Eval("Rcno")%>' OnClick="EditPhotoDesc" Text="Edit Description" /></ItemTemplate></asp:TemplateField>
            <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkPestDelete" runat="server" CommandArgument='<%# Eval("Rcno")%>' OnClick="DeletePhoto" Text="Delete" /></ItemTemplate></asp:TemplateField>

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

     </asp:GridView></td></tr></table><asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataImagesConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataImagesConnectionString.ProviderName %>" SelectCommand=""></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" FilterExpression="RecordNo = '{0}'" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters>
<asp:ControlParameter ControlID="lblRecordNo" Name="RecordNo" PropertyName="Text" Type="String" />
</FilterParameters>
<SelectParameters>
<asp:ControlParameter ControlID="txtServiceRecord" Name="@RecordNo" PropertyName="Text" />
</SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDSPestCount" runat="server" OnSelected="SqlDSPestCount_Selected" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
<asp:TextBox ID="txtPestCountRcNo" runat="server" Visible="False"></asp:TextBox>


</ContentTemplate>
</asp:TabPanel>


                                
                                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Service Technician"><HeaderTemplate>
 <asp:Label ID="lblSvcTechCount" runat="server" Font-Size="13px" Text="Service Technician"></asp:Label>
</HeaderTemplate>
<ContentTemplate>
<table border="0" style="padding-top:5px;width:100%;"><tr><td colspan="2" style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"><asp:Label ID="txtTechMode" runat="server" CssClass="dummybutton"></asp:Label></td></tr><tr style="vertical-align: middle"><td colspan="2" style="text-align:left;"><asp:Button ID="btnTechAdd" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="ADD" Width="90px" /><asp:Button ID="btnTechEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Width="90px" /><asp:Button ID="btnTechDelete" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="Confirm()" Text="DELETE" Width="90px" /></td></tr><tr><td colspan="2"><br /></td></tr>
    
     <tr>
        
        <td colspan="1" style="text-align:center">

            </td>
        <td colspan="1" style="text-align:center">
            </td>

         <td colspan="1" style="text-align:center; font-size:15px;   font-weight:bold;    font-family:'Calibri';    color:red; ">
             TIME SHEET </td>
         </tr>
    <tr>
        
        <td colspan="1" style="text-align:center">

            </td>
        <td colspan="1" style="text-align:center">

    <asp:GridView ID="GridView3" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSServiceStaff" Font-Size="12pt" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnRowDataBound="OnRowDataBound3" OnSelectedIndexChanged="OnSelectedIndexChanged3"><AlternatingRowStyle BackColor="White" />
        <Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False"><ControlStyle Width="50px" /><ItemStyle Width="50px" /></asp:CommandField>
            <asp:BoundField DataField="StaffID" HeaderText="StaffID" SortExpression="StaffID"><HeaderStyle Width="150px" /><ItemStyle Width="150px" Wrap="False" /></asp:BoundField>
            <asp:BoundField DataField="StaffName" DataFormatString="{0:c2}" HeaderText="StaffName" SortExpression="StaffName"><HeaderStyle Width="250px" /><ItemStyle Width="250px" Wrap="False" /></asp:BoundField>
            <asp:BoundField DataField="CostValue" DataFormatString="{0:f2}" HeaderText="CostValue" SortExpression="CostValue"><ItemStyle Width="100px" /></asp:BoundField>
            <asp:BoundField DataField="RecordNo" HeaderText="RecordNo" SortExpression="RecordNo" Visible="False" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                <EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate>
                <ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField>
             <asp:TemplateField >
<ItemTemplate> 
<asp:Button ID="btnTimeSheet" runat="server" OnClick="btnTimeSheet_Click" Text="ADD TS" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top" Width="80px"/>
 </ItemTemplate>
</asp:TemplateField>

                    


        </Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" />
        
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
           </asp:GridView></td>



        <td colspan="1" style="text-align:right">
    <asp:GridView ID="GridViewTS1" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSTS1" Font-Size="12pt" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnRowDataBound="OnRowDataBound3" OnSelectedIndexChanged="OnSelectedIndexChanged3"><AlternatingRowStyle BackColor="White" />
        <Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False"><ControlStyle Width="50px" /><ItemStyle Width="50px" /></asp:CommandField>
      <asp:BoundField DataField="StaffID" HeaderText="StaffID" SortExpression="StaffID"><HeaderStyle Width="150px" HorizontalAlign="Left" /><ItemStyle Width="150px" Wrap="False" HorizontalAlign="Left" /></asp:BoundField>

  <asp:BoundField DataField="ServiceDateIn" HeaderText="ServiceDate In" SortExpression="ServiceDateIn" DataFormatString="{0:dd/MM/yyyy}">
            <ControlStyle Width="25px" />
            <ItemStyle Width="25px" /></asp:BoundField>

                    <asp:BoundField DataField="TimeIn" HeaderText="Time In" SortExpression="TimeIn"><ItemStyle Width="50px" /></asp:BoundField>
            
             <asp:BoundField DataField="ServiceDateOut" DataFormatString="{0:dd/MM/yyyy}" HeaderText="ServiceDate Out" SortExpression="ServiceDateOut">
            <ControlStyle Width="25px" />
            <ItemStyle Width="25px" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeOut" HeaderText="Time Out" SortExpression="TimeOut">
            <ItemStyle Width="50px" />
            </asp:BoundField>
<asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration"><ItemStyle Width="100px" /></asp:BoundField>
<asp:BoundField DataField="Comments" HeaderText="Comments">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="100px" HorizontalAlign="Left" /></asp:BoundField>
          
            <asp:BoundField DataField="RecordNo" HeaderText="RecordNo" SortExpression="RecordNo" Visible="False" />
	    <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                <EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate>
                <ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField>
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
            <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False" />
            <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False" />
            <asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False" />
         
               <asp:TemplateField >
<ItemTemplate> 
<asp:Button ID="btnEditTimeSheet" runat="server" OnClick="btnEditTimeSheet_Click" Text="EDIT" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top" Width="80px"/>
 </ItemTemplate>
</asp:TemplateField>
        </Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" />
        
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
           </asp:GridView></td>



    </tr>
    
      <tr>
        
        <td colspan="1" style="text-align:center">

            </td>
        <td colspan="1" style="text-align:center">
            </td>

         <td colspan="1" style="text-align:right; font-size:15px;   font-weight:bold;    font-family:'Calibri';    color:red; ">
             Total Duration : <asp:TextBox ID="txtSumTotalDuration" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="20%" style="text-align:center" ></asp:TextBox> </td>
         </tr>
    <tr>
        <td colspan="2">
            <br />
        </td>
    
    <tr><td class="CellFormat" style="width:350px">RecordNo </td><td class="CellTextBox" colspan="1"><asp:Label ID="lblRecordNo2" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="40%"></asp:Label></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="width:350px">Technician ID <asp:Label ID="Label10" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="1"><asp:DropDownList ID="ddlTechID" runat="server" AppendDataBoundItems="True" DataTextField="IDNAME" DataValueField="StaffId" Width="40%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td></tr><tr><td class="CellFormat" style="width:350px">Cost Value </td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtTechCostValue" runat="server" Height="16px" MaxLength="100" Width="40%"></asp:TextBox></td></tr><tr><td colspan="2" style="text-align:right;"><asp:Button ID="btnTechSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" /><asp:Button ID="btnTechCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>
    <asp:SqlDataSource ID="SqlDSServiceStaff" runat="server" OnSelected="SqlDSServiceStaff_Selected" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" FilterExpression="RecordNo = '{0}'" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters><asp:ControlParameter ControlID="lblRecordNo" Name="RecordNo" PropertyName="Text" Type="String" /></FilterParameters><SelectParameters><asp:ControlParameter ControlID="txtServiceRecord" Name="@RecordNo" PropertyName="Text" /></SelectParameters></asp:SqlDataSource><asp:SqlDataSource ID="SqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource><asp:TextBox ID="txtTechRcNo" runat="server" Visible="False"></asp:TextBox>
<asp:SqlDataSource ID="SqlDSTS1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" FilterExpression="RecordNo = '{0}'" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters><asp:ControlParameter ControlID="lblRecordNo" Name="RecordNo" PropertyName="Text" Type="String" /></FilterParameters><SelectParameters><asp:ControlParameter ControlID="txtServiceRecord" Name="@RecordNo" PropertyName="Text" /></SelectParameters></asp:SqlDataSource>

</ContentTemplate>
</asp:TabPanel>
                             
  
                                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Service Area" Visible="false"><HeaderTemplate>
<asp:Label ID="lblSvcAreaCount" runat="server" Font-Size="13px" Text="Service Area"></asp:Label>
</HeaderTemplate>
<ContentTemplate>
<table style="padding-top:5px;width:100%;"><tr><td colspan="2" style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"><asp:Label ID="txtAreaMode" runat="server" CssClass="dummybutton"></asp:Label></td></tr><tr style="vertical-align: middle"><td colspan="2" style="text-align:left;"><asp:Button ID="btnAreaAdd" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="ADD" Width="90px" /><asp:Button ID="btnAreaEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Width="90px" /><asp:Button ID="btnAreaDelete" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="Confirm()" Text="DELETE" Width="90px" /></td></tr><tr><td colspan="2"><br /></td></tr><tr><td colspan="2" style="text-align:center">
    <asp:GridView ID="GridView4" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataSourceID="SqlDSServiceArea" Font-Size="12pt" HorizontalAlign="Center" ><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false"><ControlStyle Width="50px" /><ItemStyle Width="50px" /></asp:CommandField><asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"><HeaderStyle Width="250px" /><ItemStyle Width="250px" Wrap="False" /></asp:BoundField><asp:BoundField DataField="PricePerSquareMeter" DataFormatString="{0:f2}" HeaderText="PricePerSquareMeter" SortExpression="PricePerSquareMeter"><ItemStyle Width="100px" /></asp:BoundField><asp:BoundField DataField="AreaCompleted" DataFormatString="{0:f2}" HeaderText="AreaCompleted" SortExpression="AreaCompleted"><ItemStyle Width="100px" /></asp:BoundField><asp:BoundField DataField="TotalPriceOfCompletedArea" DataFormatString="{0:f2}" HeaderText="TotalPriceOfCompletedArea" SortExpression="TotalPriceOfCompletedArea"><ItemStyle Width="100px" /></asp:BoundField><asp:BoundField DataField="RecordNo" HeaderText="RecordNo" SortExpression="RecordNo" Visible="False" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>
'>
</asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>
'>
</asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" /><asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False" /><asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False" /><asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False" /></Columns><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" Font-Names="Calibri" ForeColor="#8C4510" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#E4E4E4" /><SortedAscendingHeaderStyle BackColor="#000066" /><SortedDescendingCellStyle BackColor="#E4E4E4" /><SortedDescendingHeaderStyle BackColor="#000066" /></asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="width:350px">RecordNo </td><td class="CellTextBox" colspan="1"><asp:Label ID="lblRecordNo3" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" Width="25%"></asp:Label></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="width:350px">Description <asp:Label ID="Label11" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtAreaDescription" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox></td></tr><tr><td class="CellFormat" style="width:350px">PricePerSquareMeter <asp:Label ID="Label12" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label></td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtPricePerSqM" runat="server" Height="16px" MaxLength="100" Width="25%"></asp:TextBox></td></tr><tr><td class="CellFormat" style="width:350px">AreaCompleted </td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtAreaCompleted" runat="server" Height="16px" MaxLength="100" Width="25%"></asp:TextBox></td></tr><tr><td class="CellFormat" style="width:350px">TotalPriceOfCompletedArea </td><td class="CellTextBox" colspan="1"><asp:TextBox ID="txtAreaTotalPrice" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" Width="25%"></asp:TextBox></td></tr><tr><td colspan="2" style="text-align:right;"><asp:Button ID="btnAreaSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" /><asp:Button ID="btnAreaCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>
    <asp:SqlDataSource ID="SqlDSServiceArea" runat="server"  ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" FilterExpression="RecordNo = '{0}'" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT distinct rcno,Description, PricePerSquareMeter, AreaCompleted, TotalPriceOfCompletedArea, RecordNo,CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn from tblservicerecordservicearea  where Recordno =@RecordNo"><FilterParameters><asp:ControlParameter ControlID="lblRecordNo" Name="RecordNo" PropertyName="Text" Type="String" /></FilterParameters><SelectParameters><asp:ControlParameter ControlID="txtServiceRecord" Name="@RecordNo" PropertyName="Text" /></SelectParameters></asp:SqlDataSource><asp:TextBox ID="txtAreaRcNo" runat="server" Visible="False"></asp:TextBox>
</ContentTemplate>
</asp:TabPanel>
                              
                          
   
                            </asp:TabContainer>
                        </td>
                    </tr>
                </table>
           </div>
                 
                     <%--  <asp:BoundField DataField="ContType" HeaderText="AccountType" SortExpression="ContType" ReadOnly="True">
                       <ControlStyle Width="5%" />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="5%" Wrap="False" />
                </asp:BoundField>--%>

 

         
                  <asp:SqlDataSource ID="SqlDataSource2" OnSelected="SqlDataSource2_Selected" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" filterexpression="RecordNo='{0}'" 
              UpdateCommand="UPDATE tblservicerecorddet SET TargetID = @TargetID, ServiceID = @ServiceID, Frequency = @Frequency, ProductServices = @ProductServices WHERE (rcno = @rcno)" >
                                  
                <FilterParameters>
                    <asp:ControlParameter Name="RecordNo" ControlID="ctl00$ContentPlaceHolder1$tb1$TabPanel1$txtsvcrecord" PropertyName="Text" Type="String" />
                </FilterParameters> 
                     
                      <SelectParameters>
                          <asp:ControlParameter ControlID="txtServiceRecord" Name="@RecordNo" PropertyName="Text" />
                      </SelectParameters>
                     
                 <updateParameters>
                  <asp:Parameter Name="ServiceID" Type="String"/>
     
        <asp:Parameter Name="ProductServices" Type="String" />
                <asp:Parameter Name="Frequency" Type="String" />
        <asp:Parameter Name="TargetID" Type="String" />
                    
    </updateParameters>
</asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>            
        <asp:SqlDataSource ID="SqlDSFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDSTarget" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

      <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>
         <asp:TextBox ID="txtSvcRcNo" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox> 

         <asp:TextBox ID="txtRcno" runat="server" Visible="true" style="display:none;"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
                    <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
       <asp:TextBox ID="txtModal" runat="server" Visible="False"></asp:TextBox>
 
     <asp:TextBox ID="txtFilling" runat="server" CSSCLASS="dummybutton"></asp:TextBox>
 

    <asp:TextBox ID="txtCustAddress" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustBuilding" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustStreet" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustCity" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustState" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustCountry" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustPostal" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustTelephone" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustFax" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustTel2" runat="server" Visible="False"></asp:TextBox>
 <asp:TextBox ID="txtCustMobile" runat="server" Visible="False"></asp:TextBox>
  <asp:TextBox ID="txtCustContact" runat="server" Visible="False"></asp:TextBox>
   <asp:TextBox ID="txtIsPopUp" runat="server" Visible="False"></asp:TextBox>
  <asp:TextBox ID="txtIsRequireFollowup" runat="server" CssClass="dummybutton"></asp:TextBox>
  <asp:TextBox ID="txtRcnoTS" runat="server" CssClass="dummybutton"></asp:TextBox>
  <asp:TextBox ID="txtGridIndexTS" runat="server" CssClass="dummybutton"></asp:TextBox>
  <asp:TextBox ID="txtSelectedIndexTS" runat="server" CssClass="dummybutton"></asp:TextBox>  
                            
           <asp:TextBox ID="txtFileAccess" runat="server" Visible="False"></asp:TextBox>
           <asp:TextBox ID="txtFileUpload" runat="server" width="1px" Enabled="false" BorderStyle="None"></asp:TextBox>
           <asp:TextBox ID="txtFileDelete" runat="server" width="1px" Enabled="false" BorderStyle="None"></asp:TextBox>
             <asp:TextBox ID="txtPrefixDocNoService" runat="server" width="1px" Enabled="false" BorderStyle="None"></asp:TextBox>
        <asp:TextBox ID="txtRcnoAction" runat="server" Visible="true" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="txtAllowToEditBilledAmtBillNo" runat="server" width="1px" Enabled="false" BorderStyle="None"></asp:TextBox>
        <asp:TextBox ID="txtEditBilledAmtBillNo" runat="server" width="1px" Enabled="false" BorderStyle="None"></asp:TextBox>
          <asp:TextBox ID="txtGrid" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                             <asp:TextBox ID="txtLimit" runat="server" AutoCompleteType="Disabled" BackColor="White" BorderStyle="None" ClientIDMode="AutoID" Enabled="False" ForeColor="White" Height="16px" Width="5%"></asp:TextBox>
                    

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
                <asp:TextBox ID="txtEditAccess" runat="server" Visible="False"></asp:TextBox>


                   <asp:Panel ID="pnlPopUpScheduler" runat="server" BackColor="White" Width="750px" Height="500px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Scheduler</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlSchedulerClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                                   <asp:ImageButton ID="btnPopUpSchedulerReset" OnClick="btnPopUpSchedulerReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                                   &nbsp;<asp:TextBox ID="txtPopUpScheduler" runat="server" MaxLength="50" Height="16px" Width="300px" Text = "Search Scheduler Here" ForeColor = "Gray" onblur = "WaterMarkScheduler(this, event);" onfocus = "WaterMarkScheduler(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpSchedulerSearch" OnClick="btnPopUpSchedulerSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" visible="true"/>
                                   &nbsp;</td> 
                                      </tr>
                           </table>
      <br />
                           <asp:TextBox ID="txtPopupSchedulerSearch" runat="server" Visible="False"></asp:TextBox>
     
            <asp:GridView ID="gvScheduler" runat="server" CssClass="Centered" DataSourceID="SqlDSScheduler" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="2" GridLines="None" Width="650px" PageSize="15"  OnRowDataBound = "OnRowDataBoundgSch" OnSelectedIndexChanged = "OnSelectedIndexChangedgSch">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="FALSE">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="StaffId" HeaderText="Id" SortExpression="StaffId">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                        <ControlStyle Width="200px" />
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" Wrap="True" />
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
            <asp:SqlDataSource ID="SqlDSScheduler" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
        
                           <br />
    </asp:Panel>

    
               <asp:Panel ID="pnlPopUpTeam" runat="server" BackColor="White" Width="700px" Height="500px" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">

         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Team</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                            <asp:ImageButton ID="btnPopUpTeamReset" OnClick="btnPopUpTeamReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                               <asp:TextBox ID="txtPopUpTeam" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for Team, Incharge or ServiceBy" ForeColor = "Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:Button ID="btnPopUpTeamSearch" OnClick="btnPopUpTeamSearch_Click" runat="server" CssClass="roundbutton1" Text="Go" Height="22px" Width="40px" Visible="true" />
                             </td> <td></td>
                                      </tr>
                           </table>
        <asp:TextBox ID="txtPopupTeamSearch" runat="server" Visible="False"></asp:TextBox>
            <br />

            <asp:GridView ID="gvTeam" CssClass="Centered" runat="server" DataSourceID="SqlDSTeam" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="2" GridLines="None" Width="650px" OnRowDataBound = "OnRowDataBoundgTeam" OnSelectedIndexChanged = "OnSelectedIndexChangedgTeam" Font-Size="15px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="TeamID" HeaderText="Id" SortExpression="TeamID">
                        <ControlStyle Width="215px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="215px" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TeamName" HeaderText="Name" SortExpression="TeamName">
                        <ControlStyle Width="215px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="215px" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InChargeId" HeaderText="InCharge Id" SortExpression="InChargeId">
                        <ControlStyle Width="100px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="100px" Wrap="True" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" SortExpression="Supervisor">
                        <ControlStyle Width="100px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="100px" Wrap="True" />
                    </asp:BoundField>
                     <asp:BoundField DataField="VehNos" HeaderText="Veh No" SortExpression="VehNos">
                        <ControlStyle Width="70px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="70px" Wrap="True" />
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
            <asp:SqlDataSource ID="SqlDSTeam" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
        
    </asp:Panel>


    <asp:Panel ID="pnlPopUpTeamNew" runat="server" BackColor="White" Width="700px" Height="500px" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        <ASP:UPDATEPANEL ID="upnlUpdate" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnselectmultiTeam" />
                        <asp:PostBackTrigger ControlID="btnPnlTeamCloseNew" />
                    </Triggers>
               <CONTENTTEMPLATE>
                    <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Team</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamCloseNew" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                            <asp:ImageButton ID="btnPopUpTeamResetNew" OnClick="btnPopUpTeamResetNew_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                               <asp:TextBox ID="txtPopUpTeamNew" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for Team, Incharge or ServiceBy" ForeColor = "Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:Button ID="btnPopUpTeamSearchNew" OnClick="btnPopUpTeamSearchNew_Click1" runat="server" CssClass="roundbutton1" Text="Go" Height="22px" Width="40px" Visible="true" />
                             </td> <td></td>
                                      </tr>
                           </table>
        <asp:TextBox ID="txtPopupTeamSearchNew" runat="server" Visible="False"></asp:TextBox>
                   <%--<div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">--%>
            <br />
                   <div style="height:300px;overflow-y:scroll">

            <asp:GridView ID="gvTeamNew" CssClass="Centered" runat="server" DataSourceID="SqlDSTeamNew" ForeColor="#333333" AllowPaging="false" AutoGenerateColumns="False"
                CellPadding="2" GridLines="None" Width="650px" OnRowDataBound = "OnRowDataBoundgTeamNew" OnSelectedIndexChanged = "OnSelectedIndexChangedgTeamNew" Font-Size="15px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                        <HeaderTemplate></HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbmultiTeam" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TeamID" HeaderText="Id" SortExpression="TeamID">
                        <ControlStyle Width="215px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="215px" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TeamName" HeaderText="Name" SortExpression="TeamName">
                        <ControlStyle Width="215px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="215px" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InChargeId" HeaderText="InCharge Id" SortExpression="InChargeId">
                        <ControlStyle Width="100px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="100px" Wrap="True" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" SortExpression="Supervisor">
                        <ControlStyle Width="100px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="100px" Wrap="True" />
                    </asp:BoundField>
                     <asp:BoundField DataField="VehNos" HeaderText="Veh No" SortExpression="VehNos">
                        <ControlStyle Width="70px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="70px" Wrap="True" />
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
            <asp:SqlDataSource ID="SqlDSTeamNew" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                       <br />
                   <div class="row">
                       <div class="col-md-12">
                           <div class="col-md-4"></div>
                           <div class="col-md-4">
                               <asp:Button ID="btnselectmultiTeam" runat="server" BackColor="#CFC6C0" OnClick="btnselectmultiTeam_Click" CssClass="roundbutton1" Font-Bold="True" Text="OK" Height="30px" />
                           </div>
                           <div class="col-md-2"></div>
                       </div>
                   </div>
        
                      <%-- </div>--%>
            </CONTENTTEMPLATE>
                    </ASP:UPDATEPANEL>
    </asp:Panel>
   
   
    <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="1284px" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left">
          <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                            <asp:ImageButton ID="btnPopUpClientReset" OnClick="btnPopUpClientReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                                   &nbsp;<asp:TextBox ID="txtPopUpClient" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for AccountID or Client details" ForeColor = "Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpClientSearch" OnClick="btnPopUpClientSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="true" />
                             </td> <td></td>
                                      </tr>
             <%-- <tr>
               
                     <td class="CellFormat" colspan="3">Service Address&nbsp;&nbsp;<asp:TextBox ID="txtCustSvcAddress" runat="server" AutoPostBack="true" Height="16px" Width="49%"></asp:TextBox></td>
                                   
              </tr>--%>
                           </table>
       
        <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvClient" runat="server" Font-Size="15px" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="1050px" OnRowDataBound = "OnRowDataBoundgClient" OnSelectedIndexChanged = "OnSelectedIndexChangedgClient" PageSize="12">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" >
                     <HeaderStyle CssClass="Hide" />
                    <ItemStyle CssClass="Hide" />
                    </asp:BoundField>
                    
                     <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" ReadOnly="True">
                       <ControlStyle Width="5%" CssClass="dummybutton"/>
                  <HeaderStyle Width="100px" Wrap="False" CssClass="dummybutton"/>
                  <ItemStyle Width="5%" Wrap="False" CssClass="dummybutton"/>
                </asp:BoundField>
                 <asp:BoundField DataField="LocationID" HeaderText="Location ID">
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                  
                 <%--     <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
                        <ControlStyle Width="150px" CssClass="dummybutton" />
                        <HeaderStyle Width="150px" CssClass="dummybutton" />
                        <ItemStyle Width="150px" Wrap="False" CssClass="dummybutton" />
                    </asp:BoundField>
                   
                  <asp:BoundField DataField="Address1" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Telephone2" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>--%>
               <%--    <asp:BoundField DataField="Email" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="LocateGrp" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddBlock" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddNos" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddFloor" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddUnit" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>--%>
                    <%-- <asp:BoundField DataField="AddStreet" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddBuilding" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddCity" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="AddState" >                   
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddCountry" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AddPostal" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Fax" >
                       <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                       <asp:BoundField DataField="Mobile" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Telephone" >
                     <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>--%>
                     <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ContID" Visible="False">
                        <ControlStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" Wrap="False" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Name" HeaderText="Client Name" SortExpression="Name">
                        <ControlStyle Width="900px" />
                        <HeaderStyle Width="300px" HorizontalAlign="Left" />
                        <ItemStyle Width="900px" Wrap="True" />
                    </asp:BoundField>
                     <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" SortExpression="ContractGroup">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="False"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="ServiceAddress1" HeaderText="Service Address">
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
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
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
        </div>
    </asp:Panel>

       <asp:Panel ID="pnlPopupLocation" runat="server" BackColor="White" Width="75%" Height="630px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left" ScrollBars="Vertical">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Service Location</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnLocationClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                                   <asp:ImageButton ID="btnPopupLocationReset" OnClick="btnPopUpLocationReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                                <asp:TextBox ID="txtPopupLocation" runat="server" MaxLength="50" Height="16px" Width="300px" Text = "Search Here for Location Address, Postal Code or Description" ForeColor = "Gray" onblur = "WaterMarkLocation(this, event);" onfocus = "WaterMarkLocation(this, event);" AutoPostBack="True"></asp:TextBox>
                                    &nbsp;<asp:ImageButton ID="btnPopUpLocationSearch" runat="server" Height="22px" ImageUrl="~/Images/searchbutton.jpg" Visible="true" Width="24px" />
                                   &nbsp;</td> 
                                      </tr>
                           </table>
      <br />
                           <asp:TextBox ID="txtPopupLocationSearch" runat="server" Visible="False"></asp:TextBox>
     
        <div style="text-align: center; padding-left: 15px; padding-bottom: 5px; width: 750px;">
            
            <asp:GridView ID="gvLocation" runat="server" CssClass="Centered" DataSourceID="SqlDSLocation" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="1" GridLines="None" Width="980px" PageSize="12" OnRowDataBound = "OnRowDataBoundgLoc" OnSelectedIndexChanged = "OnSelectedIndexChangedgLoc" DataKeyNames="Rcno">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>    
                  <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false" >
                                    
                <ControlStyle Width="40px" />
                                    

                <ItemStyle Width="40px" />
                                    
                </asp:CommandField>
                                     
                                    <asp:BoundField DataField="LocationID" HeaderText="Location ID" SortExpression="LocationID" >
                                     <ControlStyle Width="8%" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="12%" />
                                    </asp:BoundField>
                     <asp:BoundField DataField="ContractGroup" HeaderText="ContractGroup" SortExpression="ContractGroup" >
                                     <ControlStyle Width="100px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="Address" SortExpression="Address1">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Address1") & " " & Eval("AddStreet") & " " & Eval("AddBuilding") & " " & Eval("AddCity") & " " & Eval("AddState") & " " & Eval("AddCountry")%>'></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label2" runat="server" Text='<%# Eval("Address1") & " " & Eval("AddStreet") & " " & Eval("AddBuilding") & " " & Eval("AddCity") & " " & Eval("AddState") & " " & Eval("AddCountry")%>'></asp:Label>
                                         </ItemTemplate>
                                         <HeaderStyle Font-Bold="True" Width="310px" HorizontalAlign="center" />
                                         <ItemStyle Font-Names="Calibri" Width="310px" />
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="AddPostal" HeaderText="Postal" SortExpression="AddPostal" >
                                     
                                     <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                                     
                                     <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" >
                                  
                                    
                                    <ControlStyle Width="250px" />
                                    

                                    <HeaderStyle Width="250px" HorizontalAlign="Left" />
                                    

                                    <ItemStyle Width="250px" />
                                    
                                    </asp:BoundField>
                                  
                                    
                                  <%--  <asp:BoundField DataField="CompanyID" HeaderText="CompanyID" SortExpression="CompanyID" Visible="False" />
                                    
                                    <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" Visible="False" />
                                    
                                    <asp:BoundField DataField="BranchID" HeaderText="BranchID" SortExpression="BranchID" Visible="False" />
                                    
                                    <asp:BoundField DataField="ContactPerson" HeaderText="ContactPerson" SortExpression="ContactPerson" Visible="False" />
                                    
                                    <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone" Visible="False" />
                                    
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" Visible="False" />
                                    
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" Visible="False" />
                                    --%>
                                    <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                        <EditItemTemplate>
                                            

                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                                            
 
                                        </EditItemTemplate>
                                        
                                        <ItemTemplate>
                                            

                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                            
 
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    
                                   <%-- <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                                    
                                    <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False" />
                                    
                                    <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False" />
                                    
                                    <asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False" />
                                    
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
                                    
                                    <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" Visible="False" />--%>
                                    
                                <asp:BoundField DataField="ServiceLocationGroup" HeaderText="Location Group" >
                                    
                                <HeaderStyle HorizontalAlign="Left" />
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
            <asp:SqlDataSource ID="SqlDSLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" FilterExpression="AccountID = '{0}'">
                 <FilterParameters>
                    <asp:ControlParameter Name="AccountID" ControlID="ctl00$ContentPlaceHolder1$tb1$TabPanel1$txtAccountID" PropertyName="Text" Type="String" />
                </FilterParameters> 
           </asp:SqlDataSource>
        
                           <br />
    </asp:Panel>
    


    <asp:Panel ID="Panel4" runat="server" BackColor="White" Width="60%" Height="87%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              <br /><br />
                     <table style="width:100%;padding-left:15px">
                          <tr>
                               <td class="CellFormat">Record No
                               </td>
                              <td class="CellTextBox" colspan="1"><asp:TextBox ID="txtSearchSvcRecord" runat="server" MaxLength="30" Height="16px" Width="50%"></asp:TextBox></td>
                          
                           </tr>
                          <tr>
                               <td class="CellFormat">Rcno/Ref No.</td>
                              <td class="CellTextBox" colspan="1"> 
                                  <asp:TextBox ID="txtSearchRcno" runat="server" Height="16px" MaxLength="30" Width="50%"></asp:TextBox>
                               </td>
                          
                           </tr>
                          <tr>
                              <td class="CellFormat">Service Date </td>
                              <td class="CellTextBox" colspan="1">
                                  <asp:TextBox ID="txtSearchSvcDate" runat="server" Height="16px" MaxLength="30" Width="50%"></asp:TextBox>
                              </td>
                              <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtSearchSvcDate" TargetControlID="txtSearchSvcDate">
                              </asp:CalendarExtender>
                          </tr>
                          <tr>
                               <td class="CellFormat">Service By
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:TextBox ID="txtSearchSvcBy" runat="server" MaxLength="30" Height="16px" Width="50%"></asp:TextBox>
                                   <asp:ImageButton ID="btnSearchTeamID" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" />
                            
                           </tr>
                          <tr>
                               <td class="CellFormat">Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlSearchStatus" runat="server" Width="51%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                  <asp:ListItem Value="O">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="C">C - Cancelled</asp:ListItem>
                                     <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                    <asp:ListItem Value="H">H - On Hold</asp:ListItem>  
                                     <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                    <asp:ListItem Value="B">B - Job cannot complete</asp:ListItem>                              
                                    <asp:ListItem Value="P">P - Posted</asp:ListItem>     
                               </asp:DropDownList></td>
                           </tr>
                          <tr>
                               <td class="CellFormat">Service Location ID
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:TextBox ID="txtSearchAccountID" runat="server" MaxLength="30" Height="16px" Width="50%"></asp:TextBox>
                              </td>
                           </tr>
                          <tr>
                               <td class="CellFormat">Service Name
                               </td>
                              <td class="CellTextBox" colspan="1"><asp:TextBox ID="txtSearchClientName" runat="server" MaxLength="30" Height="16px" Width="90%"></asp:TextBox></td>
                          
                           </tr>
                           <tr>
                             
                                   <td class="CellFormat">
                               Service Address</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchServiceAddress" runat="server" MaxLength="30" Height="16px" Width="90%"></asp:TextBox></td>
               
                         
                            
                        </tr>
                           <tr>
                               <td class="CellFormat">Incharge ID
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlSearchIncharge" runat="server" AppendDataBoundItems="True" DataTextField="inchargeId" DataValueField="inchargeId" Width="51%" Height="25px" >
                                     <asp:ListItem Text="--SELECT--" Value="-1" />
                                </asp:DropDownList></td>
                           </tr>
                      
                            <tr>
                             
                                   <td class="CellFormat">
                               Contract No</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchContractNo" runat="server" MaxLength="30" Height="16px" Width="50%"></asp:TextBox></td>
                         
                            
                        </tr>
                           <tr>
                             
                                   <td class="CellFormat">
                               Company Group</td>
                            <td class="CellTextBox"><asp:DropDownList ID="ddlSearchCompanyGroup" runat="server" CssClass="chzn-select" DataSourceID="SqlDSCompanyGroup" DataTextField="companygroup" DataValueField="companygroup" Width="51%" Height="25px" AppendDataBoundItems="True"><asp:ListItem Text="--SELECT--" Value="-1" />
</asp:DropDownList>
</td>
                         
                            
                        </tr>
                           <tr>
                             
                                   <td class="CellFormat">
                               Manual Report No</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchManualReportNo" runat="server" MaxLength="30" Height="16px" Width="50%"></asp:TextBox></td>
               
                         
                            
                        </tr>
                           <tr>
                             
                                   <td class="CellFormat">
                               Manual Contract No</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchManualContractNo" runat="server" MaxLength="30" Height="16px" Width="50%"></asp:TextBox></td>
               
                         
                            
                        </tr>
                           <tr>
                             
                                   <td class="CellFormat">
                               P.O. No</td>
                            <td class="CellTextBox"> <asp:TextBox ID="txtSearchPONo" runat="server" MaxLength="30" Height="16px" Width="50%"></asp:TextBox></td>
               
                         
                            
                        </tr>
                          <tr>
                              <td class="CellFormat">Billing Frequency</td>
                              <td class="CellTextBox">
                                  <asp:DropDownList ID="ddlSearchBillingFrequency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="51%">
                                      <asp:ListItem Text="--SELECT--" Value="-1" />
                                  </asp:DropDownList>
                              </td>
                          </tr>
                         <tr>
                             <td class="CellFormat" colspan="2" style="text-align:CENTER">
                                 <asp:CheckBox ID="chkAdhocMapping" runat="server" Text="ADHOC MAPPING" Font-Bold="True" /></td>
                         </tr>
                         <tr><td colspan="2"><br /></td></tr>
                         <tr>
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Search" Width="100px"/>
                             <asp:Button ID="btnClose" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
                         </tr>
                          <tr><td colspan="2"><br /></td></tr>
                        
        </table>
           </asp:Panel>
    <%--  <asp:Panel ID="pnlPrint" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td>
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Print Service Report</h4>
                             </td>
                         </tr>
                            <tr>
                                 <td class="CellFormat" style="text-align:left;padding-left:20%;">
                                     <asp:RadioButtonList ID="RadioButtonList1" runat="server" CellSpacing="5" CellPadding="5">
                                         <asp:ListItem Selected="True"> Service Report</asp:ListItem>
                                         <asp:ListItem>Service Report With Technician Signature</asp:ListItem>
                                     </asp:RadioButtonList>
                              </td>
                        </tr>
                         <tr>
                             <td><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td style="text-align:center"><asp:Button ID="btnPrintReport" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClientClick = "SetTarget();"  />
                                 &nbsp;<asp:Button ID="btnEmail" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Email" Width="100px" />
                                 &nbsp;<asp:Button ID="btnpnlPrintClose" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Quit" Width="100px" /></td>
                        
                         </tr>
                          
                 

        </table>
           </asp:Panel>--%>
               <asp:Panel ID="pnlPrint" runat="server" BackColor="White" Width="40%" Height="45%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:30px">
                     
                         <tr>
                               <td colspan="2" style="text-align:center;"><%--<h4 style="color: #000000">Customer</h4>--%> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnpnlPrintClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>

                         </tr>
                          
                             <tr>
                             <td  style="text-align:left;padding-left:0%; width:100%"">
                                 <a href="RV_ServiceReportTechSign.aspx?Export=Word" target="_blank"><imagebutton style="border:none;background-color:white;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp;
                             <a href="Email.aspx?Type=ServiceReportTechSign" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                 &nbsp;&nbsp;
                             <a href="RV_ServiceReportTechSign.aspx?Export=PDF" target="_blank"><linkbutton style="font-weight:bold;color:black;width:100px;background-color:white;font-family:Calibri;font-size:15px" type="button">Print Service Report With Technician Signature</linkbutton></a>
                                   </td>
                        </tr>
                           <tr>
                             <td class="auto-style1"><br /></td>
                         </tr>
                          <tr>
                               <td  style="text-align:left;padding-left:0%; width:100%"">
                           <a href="RV_SvcSupplement.aspx?Export=Word" target="_blank"><imagebutton style="border:none;background-color:white;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                    &nbsp;&nbsp;
                             <a href="Email.aspx?Type=SvcSupplement" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                 &nbsp;&nbsp;
                             <a href="RV_SvcSupplement.aspx?Export=PDF" target="_blank"><linkbutton style="font-weight:bold;color:black;width:100px;background-color:white;font-family:Calibri;font-size:15px" type="button">Print Supplement Report</linkbutton></a>
                                   </td>     </tr>
                          <tr>
                             <td class="auto-style1"><br /></td>
                         </tr> 
                          <tr>
                               <td  style="text-align:left;padding-left:0%; width:100%"">
                                    <a href="RV_ServiceReport.aspx?Export=Word" target="_blank"><imagebutton style="border:none;background-color:white;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                    &nbsp;&nbsp;
                             <a href="Email.aspx?Type=ServiceReport" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                 &nbsp;&nbsp;
                             <a href="RV_ServiceReport.aspx?Export=PDF" target="_blank"><linkbutton style="font-weight:bold;color:black;width:100px;background-color:white;font-family:Calibri;font-size:15px" type="button">Print Service Report Form</linkbutton></a>
                                   </td>     </tr> 
                               
                           <tr>
                             <td class="auto-style1"><br /></td>
                         </tr>
                           <tr>
                               <td  style="text-align:left;padding-left:0%; width:100%"">
                                     <a href="RV_SanitationReport.aspx?Export=Word" target="_blank"><imagebutton style="border:none;background-color:white;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                    &nbsp;&nbsp;
                             <a href="Email.aspx?Type=SanitationReport" target="_blank"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                 &nbsp;&nbsp;
                             <a href="RV_SanitationReport.aspx?Export=PDF" target="_blank"><linkbutton style="font-weight:bold;color:black;width:100px;background-color:white;font-family:Calibri;font-size:15px" type="button">Print Sanitation Report Form for MQ</linkbutton></a>
                                   </td>     </tr> 
                          
                                       <tr>
                             <td class="auto-style1"><br /><br /></td>
                         </tr>
                      
                         <tr>
                             <td class="auto-style1"> <asp:RadioButtonList ID="RadioButtonList1" runat="server" Visible="false" CellSpacing="5" CellPadding="5">
                                         <asp:ListItem Selected="True"> Service Report</asp:ListItem>
                                         <asp:ListItem>Service Report With Technician Signature</asp:ListItem>
                                     </asp:RadioButtonList><asp:Button ID="btnEmail" runat="server" Visible="false" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Email" Width="100px" />
                                  <asp:Button ID="btnPrintReport" runat="server" Visible="false" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" />
                             </td>
                         </tr>
                      
                          
                 

        </table>
           </asp:Panel>
        <asp:Panel ID="pnlPopupClientSearch" runat="server" BackColor="White" Width="98%" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left" ScrollBars="Horizontal">
          <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Search Customer</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnClientSearchClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                             <asp:ImageButton ID="btnppClientReset" OnClick="btnPpClientReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                               <asp:TextBox ID="txtPpClientSearch" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here for AccountID or Client details" ForeColor = "Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpClientSearch0" runat="server" Height="22px" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Visible="true" Width="24px" />
                             </td> <td></td>
                                      </tr>
            <%--    <tr>
               
                     <td class="CellFormat" colspan="3">Service Address&nbsp;&nbsp;<asp:TextBox ID="txtCustPPSvcAddress" runat="server" AutoPostBack="true" Height="16px" Width="40%"></asp:TextBox></td>
                                   
              </tr>--%>
                           </table>
       
        <asp:TextBox ID="txtPpclient" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 2px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvClientSearch" runat="server" Font-Size="15px" DataSourceID="SqlDSClientSearch" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" HorizontalAlign="Center"
                CellPadding="4" GridLines="Vertical" Width="90%" OnRowDataBound = "OnRowDataBoundgClientSearch" OnSelectedIndexChanged = "OnSelectedIndexChangedgClientSearch">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
              <%--  <asp:BoundField DataField="ContType" HeaderText="AccountType" SortExpression="ContType" ReadOnly="True">
                       <ControlStyle Width="5%" />
                  <HeaderStyle Width="100px" Wrap="False" />
                  <ItemStyle Width="5%" Wrap="False" />
                </asp:BoundField>--%>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                     <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" ReadOnly="True">
                       <ControlStyle Width="5%" CssClass="dummybutton" />
                  <HeaderStyle Width="100px" Wrap="False" CssClass="dummybutton" />
                  <ItemStyle Width="5%" Wrap="False" CssClass="dummybutton" />
                </asp:BoundField>
                   
              
                  
                    <asp:BoundField DataField="LocationID" HeaderText="Location ID">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Id" HeaderText="ID" >
                     <HeaderStyle CssClass="Hide" HorizontalAlign="Left" />
                    <ItemStyle CssClass="Hide" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="Name">
                        <HeaderStyle Width="300px" HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
                        <ControlStyle Width="150px" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                   
                <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" SortExpression="ContractGroup">
                        <ControlStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" Wrap="False" />
                    </asp:BoundField>
                  
                    <asp:BoundField DataField="ServiceAddress1" HeaderText="Service Address">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                   
              
                  
                    <asp:BoundField DataField="ContractGroup">
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
            <asp:SqlDataSource ID="SqlDSClientSearch" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
                <asp:modalpopupextender ID="mdlPpClientSearch" runat="server" CancelControlID="btnClientSearchClose" PopupControlID="pnlPopUpClientSearch"
                                    TargetControlID="btndummyClientSearch" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
                                </asp:modalpopupextender>
            <asp:Button ID="btndummyClientSearch" runat="server" Text="Button" CssClass="dummybutton" />
            <asp:TextBox ID="txtClientModal" runat="server" CssClass="dummybutton"></asp:TextBox>
        </div>
    </asp:Panel>
     
                 <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="45%" Height="52%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
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
                             <td class="CellFormat">Record No</td>
                             <td class="CellTextBox">
                                 <asp:Label ID="lblRecordNo" runat="server" Width="60%" BackColor="#CCCCCC"></asp:Label>
                                 <asp:Label ID="lblBillNo" runat="server" Width="70%" BackColor="#CCCCCC" VISIBLE="FALSE"></asp:Label>
                             </td>
                         </tr>
                          <tr>
                               <td class="CellFormat">Service Date                              </td>
                              <td class="CellTextBox" colspan="1"> <asp:Label ID="lblStatusServiceDate" runat="server" width="60%" BackColor="#CCCCCC"></asp:Label></td>
                           </tr>
                          <tr>
                               <td class="CellFormat">Existing Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:Label ID="lblOldStatus" runat="server" width="60%" BackColor="#CCCCCC"></asp:Label></td>
                           </tr>
                          <tr>
                               <td class="CellFormat">New Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlNewStatus" runat="server" Width="61%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                  <asp:ListItem Value="O">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="C">C - Cancelled</asp:ListItem>
                                     <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                    <asp:ListItem Value="H">H - On Hold</asp:ListItem>  
                                     <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                    <asp:ListItem Value="B">B - Job cannot complete</asp:ListItem>                              
                               </asp:DropDownList></td>
                           </tr>
                             <tr>
                               <td class="CellFormat">Remarks
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:TextBox ID="txtChangeStatusRemarks" runat="server" Font-Names="Calibri" Height="60px" MaxLength="2000" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
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

    
     <asp:Panel ID="pnlEditSchDate" runat="server" BackColor="White" Width="40%" Height="97%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Schedule Date and Time</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageSchDate" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertSchDate" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Service Record No.</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtEditRecordNo" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="80%"></asp:TextBox>
                        </td>
                         </tr>
                          <tr>
                              <td class="CellFormat">Client Name</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtEditClientName" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="80%"></asp:TextBox>
                              </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Service Address</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtEditServiceAddress" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">&nbsp;</td>
                             <td class="CellTextBox">&nbsp;</td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Schedule Type</td>
                             <td class="CellTextBox">
                                 <asp:DropDownList ID="ddlEditScheduleType" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="ScheduleType" DataValueField="ScheduleType" Height="20px" Width="50%">
                                     <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                 </asp:DropDownList>
                                 <asp:ListSearchExtender ID="ddlEditScheduleType_ListSearchExtender" runat="server" Enabled="True" PromptPosition="Bottom" TargetControlID="ddlEditScheduleType">
                                 </asp:ListSearchExtender>
                             </td>
                         </tr>
                          <tr>
                              <td class="CellFormat">Schedule Date</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtEditSchDate" runat="server" Height="16px" MaxLength="10" Width="50%"></asp:TextBox>
                                  <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtEditSchDate" TargetControlID="txtEditSchDate">
                                  </asp:CalendarExtender>
                              </td>
                         </tr>
                          <tr>
                               <td class="CellFormat">Schedule TimeIn
                               </td>
                              <td class="CellTextBox" colspan="1">      <asp:TextBox ID="txtEditSchTimeIn" runat="server" MaxLength="8" Height="16px" Width="50%" AutoCompleteType="Disabled"></asp:TextBox>
                  <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtEditSchTimeIn" Mask="99:99" MaskType="Time" AutoComplete="False" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                      </asp:MaskedEditExtender>   </td>
                           </tr>
                            <tr>
                               <td class="CellFormat">Schedule TimeOut
                               </td>
                              <td class="CellTextBox" colspan="1">      <asp:TextBox ID="txtEditSchTimeOut" runat="server" MaxLength="8" Height="16px" Width="50%" AutoCompleteType="Disabled"></asp:TextBox>
                  <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtEditSchTimeOut" Mask="99:99" MaskType="Time" AutoComplete="False" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                      </asp:MaskedEditExtender>   </td>
                           </tr>
                              <tr>
                      <td class="CellFormat">Team</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtEditTeam1" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>
                               <asp:ImageButton ID="btnImgEditTeam1" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" />
                    <%--     <asp:ModalPopupExtender ID="mdlpopupEditTeamSearch" runat="server" CancelControlID="btnPnlTeamClose" PopupControlID="pnlPopUpTeam"
                                    TargetControlID="btndummyTeamSearch" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
                                </asp:ModalPopupExtender>--%>
                      </td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">Team Incharge</td>
                    <td class="CellTextBox"> <asp:TextBox ID="txtEditIncharge1" runat="server" Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                           <asp:ImageButton ID="btnImgEditIncharge1" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" /></td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">ServiceBy</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtEditSvcBy1" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>  
                           <asp:ImageButton ID="btnImgEditSvcBy1" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" />
                    </td>
                                         
                  </tr>
   <tr>
                      <td class="CellFormat">Supervisor</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtEditSupervisor1" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>  
                          <%-- <asp:ImageButton ID="btnImgEditSupervisor" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" />--%>
                        <asp:ImageButton ID="btnImgEditSupervisor1" runat="server" CssClass="righttextbox" Height="22px" ImageUrl="~/Images/searchbutton.jpg" Width="24px" ImageAlign="Top" />
                    </td>
                                         
                  </tr>
                <tr>
                      <td class="CellFormat">VehicleNo</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtEditVehNo1" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>
                           <asp:ImageButton ID="btnImgEditVehNo1" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" />
                    </td>
                                         
                  </tr>
                         <tr>
                             <td class="CellFormat">Remarks<br />
                                 <br />
                                 <asp:TextBox ID="txtCharCountEditRemarks" runat="server" BorderStyle="None" Enabled="False" ForeColor="Red" style="text-align:right" Visible="False" BackColor="White" > 4000 characters left</asp:TextBox>
                             </td>
                             <td class="CellTextBox" colspan="1">
                                 <asp:TextBox ID="txtEditRemarks" runat="server" Font-Names="Calibri" Font-Size="12pt" Height="60px" MaxLength="4000" TextMode="MultiLine" Width="80%" onKeyup="return CalculateCharsEditRemarks();"></asp:TextBox>
                             </td>
                         </tr>
                        
                         <tr>
                             <td class="CellFormat">Service Instruction<br /> <br />
                                 <asp:TextBox ID="txtCharCountEditSI" runat="server" BorderStyle="None" Enabled="False" ForeColor="Red" style="text-align:right" Visible="False" BackColor="White">500 characters left</asp:TextBox>
                                 <br />
                                 <br />
                                 <br />
                                 &nbsp;</td>
                             <td class="CellTextBox" colspan="1">
                                 <asp:TextBox ID="txtEditServiceInstruction" runat="server" Font-Names="Calibri" Font-Size="12pt" Height="60px" MaxLength="500" TextMode="MultiLine" Width="80%" onKeyup="return CalculateCharsEditSI();"></asp:TextBox>
                             </td>
                         </tr>
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSchSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnSchCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
      <asp:Panel ID="pnlEditTeam" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Team</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageTeam" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertTeam" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                          <tr>
                      <td class="CellFormat">Team</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtEditTeam" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>
                               <asp:ImageButton ID="btnImgEditTeam" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" />
                    <%--     <asp:ModalPopupExtender ID="mdlpopupEditTeamSearch" runat="server" CancelControlID="btnPnlTeamClose" PopupControlID="pnlPopUpTeam"
                                    TargetControlID="btndummyTeamSearch" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
                                </asp:ModalPopupExtender>--%>
                      </td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">Team Incharge</td>
                    <td class="CellTextBox"> <asp:TextBox ID="txtEditIncharge" runat="server" Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                           <asp:ImageButton ID="btnImgEditIncharge" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" /></td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">ServiceBy</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtEditSvcBy" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>  
                           <asp:ImageButton ID="btnImgEditSvcBy" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" />
                    </td>
                                         
                  </tr>
   <tr>
                      <td class="CellFormat">Supervisor</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtEditSupervisor" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>  
                          <%-- <asp:ImageButton ID="btnImgEditSupervisor" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" />--%>
                        <asp:ImageButton ID="btnImgEditSupervisor" runat="server" CssClass="righttextbox" Height="22px" ImageUrl="~/Images/searchbutton.jpg" Width="24px" ImageAlign="Top" />
                    </td>
                                         
                  </tr>
                <tr>
                      <td class="CellFormat">VehicleNo</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtEditVehNo" runat="server" MaxLength="25" Height="16px" Width="50%"></asp:TextBox>
                           <asp:ImageButton ID="btnImgEditVehNo" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                    Height="22px" Width="24px" ImageAlign="Top" />
                    </td>
                                         
                  </tr>
                        
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditTeamSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px"/>
                                 <asp:Button ID="btnEditTeamCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

        <asp:Panel ID="PnlSeriesAlert" runat="server" BackColor="White"  BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
             <ASP:UPDATEPANEL ID="upnlUpdateSeriesAlert" runat="server">
                  <Triggers>
                      <asp:PostBackTrigger ControlID="btnSeriesCancel" />
                  </Triggers>
                  <CONTENTTEMPLATE>

                        <table style="font-family:Calibri;color: black;width:459px !important;">
                            <tr>
                                  <td colspan="2" style="text-align:center;">
                                           <h4 id="lblSeriesAlertTitle" style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;"></h4>
                                  </td>
                            </tr>

                             <tr>
                                  <td  colspan="12" style="text-align:center;width:1%;padding: 15px;padding-top: 0px;font-size:18px;">
                                              <div id="SeriesAlertContent"></div>
                                  </td>
                            </tr>
                            <tr>
                                  <td  style="text-align:center;width: 83%;padding-top: 10px;padding-bottom: 5px;">
                                           <div id="SeriesDragAlert" runat="server" style="display:none"></div>         
                                            <div id="SeriesOpenAlert" runat="server" style="display:none"></div>
                                  </td>
                                      <td   colspan="2" style="text-align:left;padding-top: 10px;padding-bottom: 10px;">
                                          <button type="button" id="btnSeriesCancel" runat="server" class="btn btn-primary"> Cancel</button>
                                  </td>
                            </tr>

                        </table>

         <%--             <div class="row" style="color: black;text-align: left;font-family:Calibri;">
                          <div class="col-md-1"></div>
                             <div class="col-md-10">
                                   <div class="row"><div class="col-md-12" style="height:10px"></div></div>
                                <div class="row">
                                 <div class="col-md-12">
                                    <h4 id="lblSeriesAlertTitle" style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;"></h4>
                                 </div>
                                </div>
                                   <div class="row"><div class="col-md-12" style="height:10px"></div></div>

                                <div class="row">
                                    <div class="col-md-12">
                                     <div id="SeriesAlertContent"></div>
                                    </div>
                                </div>
                                     <div class="row"><div class="col-md-12" style="height:10px"></div></div>
                                        <div class="row">
                                            <div id="SeriesDragAlert" runat="server" style="display:none"></div>         
                                            <div id="SeriesOpenAlert" runat="server" style="display:none"></div>
                                            <div class="col-md-3"><button type="button" id="btnSeriesCancel" runat="server" class="btn btn-primary"> Cancel</button></div>
                                            </div>
                            </div>
                          <div class="col-md-1"></div>
                      </div>    --%>                      
                  </CONTENTTEMPLATE>
              </ASP:UPDATEPANEL>
           </asp:Panel>


                <asp:Panel ID="pnlStaff" runat="server" BackColor="White" Width="45%" Height="80%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Staff</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                            <asp:ImageButton ID="btnPopUpStaffReset" OnClick="btnPopUpStaffReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                               <asp:TextBox ID="txtPopUpStaff" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMarkStaff(this, event);" onfocus = "WaterMarkStaff(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpStaffSearch" OnClick="btnPopUpStaffSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="true" />
                             </td> <td></td>
                                      </tr>
                           </table>
       
        <asp:TextBox ID="txtPopupStaffSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvStaff" runat="server" CssClass="Centered" DataSourceID="SqlDSStaffID" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="500px"  OnRowDataBound = "OnRowDataBoundgStaff" OnSelectedIndexChanged = "OnSelectedIndexChangedgStaff" PageSize="10">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                     <asp:BoundField DataField="StaffId" HeaderText="StaffId" SortExpression="StaffId" ReadOnly="True">
                       <ControlStyle Width="80PX" />
                  <HeaderStyle Width="80px" Wrap="False" />
                  <ItemStyle Width="80px" Wrap="False" />
                </asp:BoundField>
                
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                        <ControlStyle Width="120px" />
                        <HeaderStyle Width="120px" />
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
           <asp:SqlDataSource ID="SqlDSStaffID" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
   
         
        </div>
        
           </asp:Panel>
     
     <asp:ModalPopupExtender ID="mdlPopupStaff" runat="server" CancelControlID="btnPnlStaffClose" PopupControlID="pnlStaff" TargetControlID="btndummystaff" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
       <asp:Button ID="btndummystaff" runat="server" cssclass="dummybutton" />

                 <asp:Panel ID="pnlPopUpAsset" runat="server" BackColor="White" Width="55%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="center">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Asset</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlAssetClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                                   <asp:ImageButton ID="btnPopUpAssetReset" OnClick="btnPopUpAssetReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                                <asp:TextBox ID="txtPopUpAsset" runat="server" MaxLength="50" Height="16px" Width="300px" Text = "Search Vehicle Here" ForeColor = "Gray" onblur = "WaterMarkAsset(this, event);" onfocus = "WaterMarkAsset(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpAssetSearch" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" visible="true"/>
                                   &nbsp;</td> 
                                      </tr>
                           </table>
      <br />
                           <asp:TextBox ID="txtPopupAssetSearch" runat="server" Visible="False"></asp:TextBox>
     <asp:GridView ID="gvAsset" runat="server" DataSourceID="SqlDSAsset" AutoGenerateColumns="False" OnRowDataBound = "OnRowDataBoundgAsset" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="14px" HorizontalAlign="Center" AllowSorting="True" Width="98%" AllowPaging="True" CssClass="Centered">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false" >
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
                <asp:BoundField DataField="AssetNo" HeaderText="Asset No" SortExpression="AssetNo" ItemStyle-Width="90px">
                 <ItemStyle Width="90px" Wrap="False" />
                </asp:BoundField>
               <asp:BoundField DataField="AssetRegNo" HeaderText="Asset RegNo" SortExpression="AssetRegNo" />
                <asp:BoundField DataField="AssetClass" HeaderText="Asset Class" SortExpression="AssetClass" Visible="false" />  
                  <asp:BoundField DataField="InChargeID" HeaderText="InChargeID" SortExpression="InChargeID" />
                            
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" Visible="false" />
                 <asp:BoundField DataField="OpStatus" HeaderText="OpStatus" SortExpression="OpStatus" Visible="false" />

                 <asp:BoundField DataField="Descrip" HeaderText="Description" SortExpression="Descrip" ItemStyle-Width="250px">
               
                 <ControlStyle Width="250px" />
               
                 <HeaderStyle Width="250px" />
                 <ItemStyle Width="250px" />
                 </asp:BoundField>
               
             <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
             
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 
                 </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#e4e4e4" />
            <SortedAscendingHeaderStyle BackColor="#000066" />
            <SortedDescendingCellStyle BackColor="#e4e4e4" />
            <SortedDescendingHeaderStyle BackColor="#000066" />
        </asp:GridView>
                            <asp:SqlDataSource ID="SqlDSAsset" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

         
                           <br />
    </asp:Panel>
    
              <asp:modalpopupextender ID="mdlPopUpAsset" runat="server" CancelControlID="btnPnlAssetClose" PopupControlID="pnlPopUpAsset"
          TargetControlID="btnDummyEditVehNo" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="" ></asp:modalpopupextender>
   <asp:Button ID="btnDummyEditVehNo" runat="server" CssClass="dummybutton" />
              
              
               <asp:Panel ID="pnlStatusSearch" runat="server" BackColor="White" Width="250px" Height="300px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          <br />   
         <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;">
                  <tr>
                 <%--      <td>
                        
                     <asp:checkboxlist ID="chkStatusSearch" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4" CellPadding="15" CellSpacing="2" TextAlign="Right" Enabled="True">
                                   <asp:ListItem Value="O">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="C">C - Cancelled</asp:ListItem>
                                     <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                    <asp:ListItem Value="H">H - On Hold</asp:ListItem>  
                                     <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                    <asp:ListItem Value="B">B - Job cannot complete</asp:ListItem>                              
                                    <asp:ListItem Value="P">P - Posted</asp:ListItem> 
                             <asp:ListItem Value="ALL">ALL STATUS</asp:ListItem>
                               </asp:checkboxlist>--%>
                              <td colspan="2" style="padding-left:10px;">
                                    <asp:CheckBox ID="chkSearchAll" runat="server"  Text="All Status" Font-Size="15px" Font-Names="Calibri" Font-Bold="True" onchange="EnableDisableStatusSearch()" BorderColor="White" BorderWidth="0px" />
                 
                           <asp:checkboxlist ID="chkStatusSearch" runat="server" CssClass="checkbox1" Width="100%" RepeatDirection="Vertical" RepeatColumns="1" CellPadding="0" CellSpacing="0" TextAlign="Right" Enabled="True" Height="120px" Font-Bold="True">
                                   <asp:ListItem Value="O">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="C">C - Cancelled</asp:ListItem>
                                     <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                    <asp:ListItem Value="H">H - On Hold</asp:ListItem>  
                                 <asp:ListItem Value="B">B - Job cannot complete</asp:ListItem>   
                                     <asp:ListItem Value="V">V - Void</asp:ListItem>                                                               
                                    <asp:ListItem Value="P">P - Posted</asp:ListItem> 
                               <%--<asp:ListItem Value="ALL">ALL STATUS</asp:ListItem>--%>
                               </asp:checkboxlist>
                            
                      </td>
                          <%--</td>--%>
          <%--            </tr>
               <tr>
                      <td>
                         
                             <asp:CheckBox ID="chkSearchAll" runat="server"  Text="All Status" Font-Size="15px" Font-Names="Calibri" Font-Bold="True" onchange="EnableDisableStatusSearch()"  CellPadding="15" CellSpacing="2" BorderColor="White" BorderWidth="4px" />
                 
                      </td>
                           </tr>--%>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center">
                                 <asp:Button ID="btnStatusSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="80px"/>
                                 <asp:Button ID="btnStatusCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="80px" />
                               </td>
                         </tr>   
                      <tr>
                             <td colspan="2"><br /></td>
                         </tr>                    
        </table>
        
           </asp:Panel>
              
                  <asp:Panel ID="pnlImage" runat="server" BackColor="White" Width="90%" Height="90%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
               <table style="height:100%;width:100%"><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000"></h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnImageClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
               <tr style="height:100%;width:100%"><td> <asp:Image ID="ImageEnlarge" runat="server" width="100%" Height="100%"/> 
</td></tr>   
               </table>
           </asp:Panel>
                 <asp:ModalPopupExtender ID="mdlPopupImage" runat="server" CancelControlID="btnImageClose" PopupControlID="pnlImage" TargetControlID="btndummyImage" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
                  <asp:Button ID="btndummyImage" runat="server" cssclass="dummybutton" />
  
                  <asp:Panel ID="pnlNotesMaster" runat="server" BackColor="White" Width="70%" Height="90%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
             
                  
           </asp:Panel>
                 <asp:ModalPopupExtender ID="mdlPopupNotesMaster" runat="server" CancelControlID="btnCancelNotesMaster" PopupControlID="pnlNotesMaster" TargetControlID="btndummyNotesMaster" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
                  <asp:Button ID="btndummyNotesMaster" runat="server" cssclass="dummybutton" />
  


              <%--Confirm delete uploaded file--%>
                                              
                 <asp:Panel ID="pnlConfirmDeleteUploadedFile" runat="server" BackColor="White" Width="400px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
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
                           <asp:Label ID="lblDeleteConfirm" runat="server" Text="" Visible="false"></asp:Label>

                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to DELETE the File?"></asp:Label>
                          <asp:TextBox ID="txtDeletePhoto" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
 
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

               
      <asp:ModalPopupExtender ID="mdlPopupDeleteUploadedFile" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmDeleteUploadedFile" TargetControlID="btndummyDeleteUploadedFile" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyDeleteUploadedFile" runat="server" CssClass="dummybutton" />

                <%--Confirm delete service detail--%>
                                              
                 <asp:Panel ID="pnlConfirmRequestSvcDetail" runat="server" BackColor="White" Width="450px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lbl1" runat="server" Text="Confirm"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                           <asp:Label ID="lblRequestConfirmRequestNo" runat="server" Text="" Visible="false"></asp:Label>

                          &nbsp;<asp:Label ID="lblRequestQuery" runat="server" Text="Are you sure to DELETE the File?"></asp:Label>
                          <%--<asp:TextBox ID="TextBox6" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>--%>
 
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnRequestSvcDetailConfirm" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnRequestSvcDetailCancel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupRequestSvcDetail" runat="server" CancelControlID="btnRequestSvcDetailCancel" PopupControlID="pnlConfirmRequestSvcDetail" TargetControlID="btndummyRequestSvcDetail" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyRequestSvcDetail" runat="server" CssClass="dummybutton" />


                 <asp:Panel ID="pnlConfirmDeleteAllPhotos" runat="server" BackColor="White" Width="400px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
           
                 <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center" colspan="2">
                         
                          <asp:Label ID="lblWarning" runat="server" Text="WARNING"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2" style="text-align:center; margin-left:auto; margin-right:auto;">
                                 <asp:Label ID="lblWarningAlert" runat="server" Font-Bold="True" ForeColor="Red" Width="90%" ></asp:Label>
                                 /td>
                         </tr> 
             
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                           <asp:Label ID="Label61" runat="server" Text="" Visible="false"></asp:Label>

                          &nbsp;<asp:Label ID="lblQueryDeleteAll" runat="server" Text="Are you sure to DELETE the File?"></asp:Label>
               <br /><br />    <asp:TextBox ID="txtConfirmationCode" runat="server" Visible="True" Width="100px"></asp:TextBox>    </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmDeleteAllPhotos" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelDeleteAllPhotos" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlDeleteAllPhotos" runat="server" CancelControlID="btnCancelDeleteAllPhotos" PopupControlID="pnlConfirmDeleteAllPhotos" TargetControlID="btndummyDeleteAllPhotos" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyDeleteAllPhotos" runat="server" CssClass="dummybutton" />

                  <%--Edit Photo Description--%>
                                              
                 <asp:Panel ID="pnlEditPhotoDesc" runat="server" BackColor="White" Width="400px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center" colspan="2">
                         
                          <asp:Label ID="Label47" runat="server" Text="EDIT PHOTO DESCRIPTION"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;" colspan="2">
                       
                          &nbsp;<asp:Label ID="lblQueryEditPhotoDesc" runat="server" Text=""></asp:Label>
                          <asp:TextBox ID="txtEditPhotoDescRcno" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
 
                      </td>
                           </tr>
              <tr>
                                             <td class="CellFormat">Description </td>
                                             <td class="CellTextBox" colspan="1" style="text-align:left">
                                                 <asp:TextBox ID="txtEditPhotoDesc" runat="server" Width="70%"></asp:TextBox>
                                             </td>
                                         </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveEditPhotoDesc" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelEditPhotoDesc" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupEditPhotoDesc" runat="server" CancelControlID="btnCancelEditPhotoDesc" PopupControlID="pnlEditPhotoDesc" TargetControlID="btndummyEditPhotoDesc" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyEditPhotoDesc" runat="server" CssClass="dummybutton" />


             <%-- Confirm Delete uploaded file--%>


                      <%--Confirm Confirm Zero Amunt To Bill--%>
                                              
                 <asp:Panel ID="pnlConfirmZeroAmountToBill" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label26" runat="server" Text="Confirm ZERO Amount To Bill"></asp:Label>
                          <asp:TextBox ID="txtZeroAmountToBillYesNo" runat="server" AutoCompleteType="Disabled" AutoPostBack="False" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
    
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">

                          &nbsp;<asp:Label ID="Label44" runat="server" Text="Warning: Do you confirm to save this service with Zero Value?"></asp:Label>
                     
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmZeroAmountToBill" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnConfirmZeroAmountToBillNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupZeroAmountToBill" runat="server" CancelControlID="" PopupControlID="pnlConfirmZeroAmountToBill" TargetControlID="btndummyZeroAmountToBill" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyZeroAmountToBill" runat="server" CssClass="dummybutton" />

             <%-- Confirm Zero Amunt To Bill--%>




                           <%--Confirm Change Contract No.--%>
                                              
                 <asp:Panel ID="pnlConfirmChangeContractNo" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label45" runat="server" Text="Confirm Change of Contract No."></asp:Label>
                          
    
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">

                          &nbsp;<asp:Label ID="Label46" runat="server" Text="Do you wish to change the Contract No. and Account Details?"></asp:Label>
                     
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmChangeContractNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnConfirmChangeContractNoNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupChangeContractNo" runat="server" CancelControlID="" PopupControlID="pnlConfirmChangeContractNo" TargetControlID="btndummyChangeContractNo" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyChangeContractNo" runat="server" CssClass="dummybutton" />

             <%-- Confirm Change Contract No.--%>



       <%--Blank Service Detail --%>
                                              
                 <asp:Panel ID="pnlBlankServiceDetail" runat="server" BackColor="White" Width="400px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label38" runat="server" Text="Blank ServiceDetail"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                             <asp:Label ID="lblBlankDetailRecordNo" runat="server" Text="" Visible="true" Height="15px"></asp:Label>
                          &nbsp;<asp:Label ID="Label40" runat="server" Text="- This Service Record does not have Service Details."></asp:Label>
                      </td>
                           
               </tr>

                  <tr>
                             <td colspan="2"><br /></td>
                         </tr>
              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                       
                          &nbsp;<asp:Label ID="Label39" runat="server" Text="Please ADD the Service Details so that it can be " Height="15px"></asp:Label>
                      </td>
                           
               </tr>

              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          &nbsp;<asp:Label ID="Label41" runat="server" Text="downloaded on the Mobile App." Height="15px"></asp:Label>
                      </td>
                           
               </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkBlankServiceDetail" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px" OnClientClick="currentdatetime()"/>
                                
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlBlankServiceDetail" runat="server" CancelControlID="btnOkBlankServiceDetail" PopupControlID="pnlBlankServiceDetail" TargetControlID="btndummyBlankServiceDetail" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyBlankServiceDetail" runat="server" CssClass="dummybutton" />

             <%-- Blank Service Detail--%>


                    <%--Require Follow-up True --%>
                                              
                 <asp:Panel ID="pnlRequireFollowUp" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label48" runat="server" Text="This Service Record requires Follow-up"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label ID="Label51" runat="server" Text="This Service Record "></asp:Label>
                           <asp:Label ID="lblFollowUpRecordNo" runat="server" ></asp:Label>
                           <asp:Label ID="Label53" runat="server" Text=" requires Follow-up."></asp:Label>
                      </td>
                           
               </tr>

              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          &nbsp;</td>
                           
               </tr>
              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          &nbsp;<asp:Label ID="Label50" runat="server" Text="Do you wish to create the Follow-up Service Now?"></asp:Label>
                      </td>
                           
               </tr>
             
             
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnYesRequireFollowUp" runat="server" CssClass="roundbutton1"  BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/> 

                             <asp:Button ID="btnLaterRequireFollowUp" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Later" Width="100px" />
                                
                               </td>
                         </tr>         
             
              </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center">
                                 <asp:CheckBox ID="chkDonotShowThisMessage" runat="server" CssClass="CellFormat" Text="Don't show this message again" />
                                
                               </td>
                         </tr>         
             
              </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"> 

                                             <asp:TextBox ID="txtRemarksRequireFollowUp" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                  
                               </td>
                         </tr>         
             
              </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkRequireFollowUp" runat="server" CssClass="roundbutton1"  BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/> 

                             <asp:Button ID="Button7" runat="server" CssClass="btnCancelRequireFollowUp" BackColor="#CFC6C0"  Font-Bold="True" Text="Later" Width="100px" />
                                
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlRequireFollowUp" runat="server" CancelControlID="btnLaterRequireFollowUp" PopupControlID="pnlRequireFollowUp" TargetControlID="btndummyRequireFollowUp" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyRequireFollowUp" runat="server" CssClass="dummybutton" />

             <%-- Require Follow-up True --%>



              
                                <%-- Start :Confirm Contract is not OPEN do you still wish to ADD a service report.--%>
                                              
                 <asp:Panel ID="pnlContractNotOpenAdd" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label52" runat="server" Text="This contract is not OPEN"></asp:Label>
                          
    
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">

                          &nbsp;<asp:Label ID="Label54" runat="server" Text="This contract is not OPEN.  Do you wish to ADD a service report?"></asp:Label>
                     
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkContractNotOpenAdd" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnNoContractNotOpenNoAdd" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlContractNotOpenAdd" runat="server" CancelControlID="" PopupControlID="pnlContractNotOpenAdd" TargetControlID="btndummyContractNotOpenAdd" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyContractNotOpenAdd" runat="server" CssClass="dummybutton" />

             <%-- Confirm Contract is not OPEN do you still wish to ADD a service report.--%>



           <%-- Start :Confirm Contract is not OPEN do you still wish to COPY a service report.--%>
                                              
                 <asp:Panel ID="pnlContractNotOpenCopy" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label55" runat="server" Text="This contract is not OPEN"></asp:Label>
                          
    
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">

                          &nbsp;<asp:Label ID="Label56" runat="server" Text="This contract is not OPEN.  Do you wish to COPY a service report?"></asp:Label>
                     
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkContractNotOpenCopy" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnNoContractNotOpenNoCopy" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlContractNotOpenCopy" runat="server" CancelControlID="" PopupControlID="pnlContractNotOpenCopy" TargetControlID="btndummyContractNotOpenCopy" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyContractNotOpenCopy" runat="server" CssClass="dummybutton" />

             <%-- End :Confirm Contract is not OPEN do you still wish to COPY a service report.--%>




                         <%-- Start :Check if Report and Actual End Dates are same.--%>
                                              
                 <asp:Panel ID="pnlReportActualEndDates" runat="server" BackColor="White" Width="400px" Height="180px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label30" runat="server" Text="Report and Actual End Dates are NOT same"></asp:Label>
                          
    
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>

                    <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="Label32" runat="server" Height="15px" Text="The Report Service End Date is not the same as"></asp:Label>
                    </td>
                </tr>

              <tr>

                    <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="Label33" runat="server" Height="15px" Text=" the Actual Service End Date."></asp:Label>
                    </td>
                </tr>
                     <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                       
                          &nbsp;<asp:Label ID="Label34" runat="server" Text="Do you wish to overwrite the Actual Service End Date?" Height="15px"></asp:Label>
                      </td>
                           
               </tr>
                 
                        
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnYesReportActualEndDates" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnNoReportActualEndDates" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlReportActualEndDates" runat="server" CancelControlID="" PopupControlID="pnlReportActualEndDates" TargetControlID="btnDummyReportActualEndDates" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyReportActualEndDates" runat="server" CssClass="dummybutton" />

              <%-- End :Check if Report and Actual End Dates are same.--%>

              <%--'25.04.23--%>


                   <%-- Start :Check if Report and Actual Start  Dates are same.--%>
                                              
                 <asp:Panel ID="pnlReportActualStartDates" runat="server" BackColor="White" Width="400px" Height="180px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label25" runat="server" Text="Report and Actual Start Dates are NOT same"></asp:Label>
                          
    
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>

                    <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="Label29" runat="server" Height="15px" Text="The Report Service Start Date is not the same as"></asp:Label>
                    </td>
                </tr>

              <tr>

                    <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="Label28" runat="server" Height="15px" Text=" the Actual Service Start Date."></asp:Label>
                    </td>
                </tr>
                     <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                       
                          &nbsp;<asp:Label ID="Label31" runat="server" Text="Do you wish to overwrite the Actual Service Start Date?" Height="15px"></asp:Label>
                      </td>
                           
               </tr>
                 
                        
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnYesReportActualStartDates" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnNoReportActualStartDates" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlReportActualStartDates" runat="server" CancelControlID="" PopupControlID="pnlReportActualStartDates" TargetControlID="btnDummyReportActualStartDates" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyReportActualStartDates" runat="server" CssClass="dummybutton" />

             <%-- End :Check if Report and Actual Start Dates are same.--%>






                 <%-- Start :Check if Report and Actual Start Time are same.--%>
                                              
                 <asp:Panel ID="pnlReportActualStartTimes" runat="server" BackColor="White" Width="400px" Height="180px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label35" runat="server" Text="Report and Actual Start Times are NOT same"></asp:Label>
                          
    
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>

                    <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="Label36" runat="server" Height="15px" Text="The Report Service Start Time is not the same as"></asp:Label>
                    </td>
                </tr>

              <tr>

                    <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="Label37" runat="server" Height="15px" Text=" the Actual Service Start Time."></asp:Label>
                    </td>
                </tr>
                     <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                       
                          &nbsp;<asp:Label ID="Label42" runat="server" Text="Do you wish to overwrite the Actual Service Start Time?" Height="15px"></asp:Label>
                      </td>
                           
               </tr>
                 
                        
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnYesReportActualStartTime" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnNoReportActualStartTimes" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlReportActualStartTimes" runat="server" CancelControlID="" PopupControlID="pnlReportActualStartTimes" TargetControlID="btnDummyReportActualStartTimes" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyReportActualStartTimes" runat="server" CssClass="dummybutton" />

             <%-- End :Check if Report and Actual Start Time are same.--%>



                  <%-- Start :Check if Report and Actual End Time are same.--%>
                                              
                 <asp:Panel ID="pnlReportActualEndTimes" runat="server" BackColor="White" Width="400px" Height="180px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label62" runat="server" Text="Report and Actual End Times are NOT same"></asp:Label>
                          
    
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>

                    <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="Label63" runat="server" Height="15px" Text="The Report Service End Time is not the same as"></asp:Label>
                    </td>
                </tr>

              <tr>

                    <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="Label64" runat="server" Height="15px" Text=" the Actual Service Start Time."></asp:Label>
                    </td>
                </tr>
                     <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                       
                          &nbsp;<asp:Label ID="Label66" runat="server" Text="Do you wish to overwrite the Actual Service End Time?" Height="15px"></asp:Label>
                      </td>
                           
               </tr>
                 
                        
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnYesReportActualEndTime" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="Button5" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlReportActualEndTimes" runat="server" CancelControlID="" PopupControlID="pnlReportActualEndTimes" TargetControlID="btnDummyReportActualEndTimes" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyReportActualEndTimes" runat="server" CssClass="dummybutton" />

             <%-- End :Check if Report and Actual End Time are same.--%>
              <%--'25.04.23--%>
              
              <asp:Panel ID="pnlPopupInvoiceDetails" runat="server" BackColor="White" Width="1000px" Height="85%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
        <%--<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton4" PopupControlID="pnlPopupInvoiceDetails" TargetControlID="btnDummyInvoice"  Enabled="True" DynamicServicePath=""></asp:ModalPopupExtender>--%>
                       
                <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">Transactions</h4> 
  </td> <td>
                               <asp:TextBox ID="txtRecordNoSelected" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
                  <%--  <tr>
                        <td colspan="1" style="text-align:left;padding-left:50px;">
                            <asp:DropDownList ID="ddlFilter" runat="server" CssClass="CellFormat" Width="250px" AutoPostBack="True">
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
                    </tr>--%>
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="lblAlertTransactions" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewInvoiceDetails" runat="server" DataSourceID="SqlDSInvoiceDetails" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
                <%--  <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-HorizontalAlign="Right" >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>--%>
            <%--   <asp:HyperLinkField 
      DataTextField="ProductName" 
      HeaderText="Product Name" 
      SortExpression="ProductName" 
      DataNavigateUrlFields="ProductID" 
      DataNavigateUrlFormatString="ProductDetails.aspx?ProductID={0}" /> --%>  
                <asp:BoundField DataField="VoucherDate" HeaderText="Voucher Date" DataFormatString="{0:dd/MM/yyyy}" >
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DocType" HeaderText="Document Type" >
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherNumber" HeaderText="Reference Number">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description">
                <HeaderStyle HorizontalAlign="Left" />
               
                </asp:BoundField>
              <asp:BoundField DataField="Reference" HeaderText="Reference">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                <asp:BoundField DataField="Status" >
                <ControlStyle CssClass="dummybutton" />
                <ItemStyle CssClass="dummybutton" />
                </asp:BoundField>
                <asp:HyperLinkField 
         DataTextField="Type"                 
         DataNavigateUrlFields="Type,VoucherNumber" 
         DataNavigateUrlFormatString="{0}.aspx?VoucherNumber={1}&CustomerFrom=Service" HeaderText="View" DataTextFormatString="View" target="_blank" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="White" ItemStyle-ForeColor="Black" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                </asp:HyperLinkField>
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
                <%--  <div style="text-align:right;width:93%;padding-right:80px">
                      <asp:Label ID="Label7" runat="server" Text="Total" CssClass="CellFormat" Visible="false"></asp:Label>
                      <asp:Label ID="lblTotal" runat="server" Text="Total    : 0.00" CssClass="CellFormat"></asp:Label>
                  </div>--%>
                  <asp:SqlDataSource ID="SqlDSInvoiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
           <SelectParameters>
                          <asp:ControlParameter ControlID="txtRecordNoSelected" Name="@RecordNo" PropertyName="Text" />
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
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
               <%-- <tr>
                    <td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp; <asp:TextBox ID="TextBox5" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True" Visible="False">Search Here for Team or In-ChargeId</asp:TextBox>
  </td> <td>
                           </td>


                    </tr>
                            --%>
             

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="ModalPopupInvoice" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlPopupInvoiceDetails" TargetControlID="btnDummyInvoice" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyInvoice" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

     <asp:Panel ID="pnlEditBillAmt" runat="server" BackColor="White" Width="40%" Height="45%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Bill  Amount</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMsgBillAmt" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBillAmt" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Bill Amount</td>
                             <td class="CellTextBox">
                               <asp:TextBox ID="txtEditBillAmt" runat="server" Height="16px" MaxLength="18" Width="50%"></asp:TextBox>
                        </td>
                         </tr>
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveEditBillAmt" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px"/>
                                 <asp:Button ID="btnCancelEditBillAmt" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
                      <asp:ModalPopupExtender ID="mdlPopupEditBillAmt" runat="server" CancelControlID="btnEditBillAmtCancel" PopupControlID="pnlEditBillAmt" TargetControlID="btndummyBillAmt" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btndummyBillAmt" runat="server" Text="Button" cssclass="dummybutton" />


<%--09.12.22--%>


               <asp:Panel ID="pnlEditBilledAmt" runat="server" BackColor="White" Width="30%" Height="35%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="4">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Billed  Amount and Bill No.</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="4" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMsgBilledAmt" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="4" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBilledAmt" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Record No</td>
                             <td class="CellTextBox"><asp:Label ID="lblEditBilledAmtRecordNo" runat="server" ></asp:Label></td>
                            
                             
                         </tr>
                        
                          <tr>
                           
                             <td class="CellFormat">Service Value</td>
                             <td class="CellTextBox">
                                 <asp:Label ID="lblEditBilledAmtServiceValue" runat="server" ></asp:Label></td>
                         </tr>

                            <tr>
                           
                             <td class="CellFormat"></td>
                             <td class="CellTextBox">
                                 <asp:Label ID="Label22" runat="server" ></asp:Label></td>
                         </tr>

                         <tr>
                             <td class="CellFormat" >Billed Amount</td>
                             <td class="CellTextBox" colspan="3">
                                 <asp:TextBox ID="txtEditBilledAmt" runat="server" Height="16px" MaxLength="18" Width="50%"></asp:TextBox>
                             </td>
                         </tr>
                        
                         <tr>
                             <td class="CellFormat" >Bill No.</td>
                             <td class="CellTextBox" colspan="3">
                               <asp:TextBox ID="txtEditBillNo" runat="server" Height="16px" MaxLength="18" Width="50%"></asp:TextBox>
                        </td>
                         </tr>


                         <tr>
                             <td colspan="4"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="4" style="text-align:center"><asp:Button ID="btnSaveEditBilledAmt" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px"/>
                                 <asp:Button ID="btnCancelEditBilledAmt" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
                      <asp:ModalPopupExtender ID="mdlPopupEditBilledAmt" runat="server" CancelControlID="btnEditBilledAmtCancel" PopupControlID="pnlEditBilledAmt" TargetControlID="btndummyBilledAmt" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btndummyBilledAmt" runat="server" Text="Button" cssclass="dummybutton" />

             <%-- 09.12.22--%>



            <asp:Panel ID="pnlEditSign" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Require Client Signature</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMsgSign" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertSign" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat" style="text-align:center" colspan="2"><asp:CheckBox ID="chkEditReqCustSign" runat="server" CssClass="CellFormat" Text="Require Client Signature" />
                        </td>
                         </tr>
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveEditSign" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px"/>
                                 <asp:Button ID="btnCancelEditSign" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
                      <asp:ModalPopupExtender ID="mdlPopupEditSign" runat="server" CancelControlID="btnEditSignCancel" PopupControlID="pnlEditSign" TargetControlID="btndummySign" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btndummySign" runat="server" Text="Button" cssclass="dummybutton" />
   
              <%--Start:Collected Amount--%>

                <asp:Panel ID="pnlEditCollectedAmount" runat="server" BackColor="White" Width="40%" Height="45%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Collection Details</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMsgCollection" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertCollection" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Collected Amount</td>
                             <td class="CellTextBox">
                               <asp:TextBox ID="txtEditAmountReceived" runat="server" Height="16px" MaxLength="18" Width="50%"></asp:TextBox>
                        </td>
                         </tr>
                        
                        
                            <tr>
                             <td class="CellFormat">Payment Type</td>
                             <td class="CellTextBox">
                               <asp:TextBox ID="txtEditPaymentType" runat="server" Height="16px" MaxLength="20" Width="50%"></asp:TextBox>
                        </td>
                         </tr>

                            <tr>
                             <td class="CellFormat">Reference No.</td>
                             <td class="CellTextBox">
                               <asp:TextBox ID="txtEditRefNo" runat="server" Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                        </td>
                         </tr>


                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveEditCollectedAmount" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnEditCollectedAmtCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
                      <asp:ModalPopupExtender ID="mdlPopupEditCollectedAmt" runat="server" CancelControlID="btnEditCollectedAmtCancel" PopupControlID="pnlEditCollectedAmount" TargetControlID="btnCancelEditCollectedAmt" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnCancelEditCollectedAmt" runat="server" Text="Button" cssclass="dummybutton" />

              <%--End:Collected Amount--%>

  <%--Start:Our Ref Amount--%>

                <asp:Panel ID="pnlEditOurRef" runat="server" BackColor="White" Width="40%" Height="45%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Manual Report No.</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMsgManualReportNo" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertManualReportNo" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Manual Report No.</td>
                             <td class="CellTextBox">
                                 &nbsp;</td>
                         </tr>
                        <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveEditOurRef" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnEditOurRefCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr> 

        </table>
           </asp:Panel>
                      <asp:ModalPopupExtender ID="mdlPopupEditOurRef" runat="server" CancelControlID="btnEditOurRefCancel" PopupControlID="pnlEditOurRef" TargetControlID="btnCancelEditOurRef" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnCancelEditOurRef" runat="server" Text="Button" cssclass="dummybutton" />

              <%--End:Our Ref--%>



              

       <%-- start: edit Manual Contract, PO, Work Order--%>

            <asp:Panel ID="pnlEditManualContractPOWOEdit" runat="server" BackColor="White" Width="40%" Height="45%" BorderColor="#003366" BorderWidth="1px" ScrollBars="None">
              
                     <table border="0" style="width:100%;padding-left:5px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Manual Report No., Contract No., P.O. No and Work Order No.</h4>
                             </td>
                         </tr>
                               <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMsgManualContractPOWO" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertManualContractPOWO" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
            
                         
                         <tr>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:30%; text-align:left;">Manual Report No.</td>
                             <td class="CellTextBox">
                                    <asp:TextBox ID="txtEditOurRef" runat="server" Height="16px" MaxLength="18" Width="90%"></asp:TextBox>
                      </td>
                         </tr>
                          <tr>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:30%; text-align:left;">Manual Contract No.</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtManualContractNoEdit" runat="server" Height="16px" MaxLength="25" Width="90%"></asp:TextBox>
                              </td>
                         </tr>
                          <tr>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:left;">P.O. No.</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtPONoEdit" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="100" Width="90%"></asp:TextBox>
                              </td>
                         </tr>
                          <tr>
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:left;">Work Order No.</td>
                              <td class="CellTextBox" colspan="1">   
                                     <asp:TextBox ID="txtWorkOrderNoEdit" runat="server" MaxLength="50" Height="16px" Width="90%"></asp:TextBox>
                
                              </td>
                           </tr>
                        
                        
                       
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnManualContractPoWoEditSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnManualContractEditPoWoCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupManualContractPOWOEdit" runat="server" CancelControlID="btnSchCancel" PopupControlID="pnlEditManualContractPOWOEdit" TargetControlID="btndummyManualContractPOWOEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyManualContractPOWOEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Edit Manual Contract, PO, Work Order --%>


             <%-- Start: address during Copy--%>

                 <asp:Panel ID="pnlCopyAddress" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px" border="0">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Copy Service Contact Information</h4>
                             </td>
                         </tr>
                          <tr>
               <td style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"  colspan="2"> 
                      <asp:Label ID="Label17" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"  colspan="2"> 
                      <asp:Label ID="Label18" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr >
                             <td class="CellFormat" style="text-align:center" colspan="2">
                                          <h5 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">From where do you want to COPY the Service Contact Information?</h5>
                        
                             </td>
                         </tr>
                        
                        
                         <tr>
                             <td class="CellFormat" style="text-align:right">
                                 <asp:RadioButton ID="chkCustomerServiceLocation" runat="server" Checked="True" GroupName="CopyContactInfo" Text="Customer Service Location" />
                             </td>

                              <td class="CellFormat" style="text-align:left">
                                  <asp:RadioButton ID="chkOldServiceRecord" runat="server" GroupName="CopyContactInfo" Text="Selected Service Record" />
                             </td>
                         </tr>
                            <tr>
                                <td class="CellFormat" style="text-align:center" colspan="2">&nbsp;</td>
                         </tr>
                        
                        
                            <tr style="padding-top:40px;">
                            
                                <td style="text-align:center" colspan="2">
                                    <asp:Button ID="btnSaveopyAddress" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Continue" Width="120px" />
                                    <asp:Button ID="btnCancelCopyAddress" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                                </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
                      <asp:ModalPopupExtender ID="mdlPopupCopyAddress" runat="server" CancelControlID="btnCancelCopyAddress" PopupControlID="pnlCopyAddress" TargetControlID="btnCopyAddress" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnCopyAddress" runat="server" Text="Button" cssclass="dummybutton" />

              <%-- End:address during Copy--%>


              <%--Start:SMS--%>

              <asp:Panel ID="pnlSMS" runat="server" BackColor="White" Width="80%" Height="85%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">SMS</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMsgSMS" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertSMS" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                              <tr>
                                <td class="CellFormat" style="width:20%"></td>
                             <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">
                                 <asp:RadioButtonList ID="rdbSMSContactPerson" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                     <asp:ListItem>ContactPerson1</asp:ListItem>
                                     <asp:ListItem>ContactPerson2</asp:ListItem>
                                 </asp:RadioButtonList></td>
                             
                         </tr>
                         <tr>
                             <td class="CellFormat">To&nbsp; Mobile No.</td>
                             <td class="CellTextBox">
                               <asp:TextBox ID="txtToMobileSMS" runat="server" Height="16px" MaxLength="18" Width="50%"></asp:TextBox>
                        </td>
                         </tr>
                          <tr>
                             <td class="CellFormat">Contact Person</td>
                             <td class="CellTextBox">
                               <asp:TextBox ID="txtSMSContactPerson" runat="server" Height="16px" MaxLength="300" Width="50%" AutoPostBack="false"></asp:TextBox>
                           <asp:Button ID="btnUpdateSMSCP" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Update Contact Person" Width="180px" Visible="true" OnClientClick="currentdatetime()"/>
                                    <asp:TextBox ID="txtSMSCPReplace" runat="server" Height="16px" MaxLength="18" Width="50%" cssclass="dummybutton"></asp:TextBox>
                        </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Message</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtMessageSMS" runat="server" Height="90px" MaxLength="18" Width="90%" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                             </td>
                         </tr>
                        <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveSMS" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Send" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnSMSCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr> 

                         <tr>
                             <td colspan="2">
                                   <asp:GridView ID="grdSMS" runat="server" DataSourceID="SqlDSSMS" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="98%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns> 
                <%--<asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False"><ControlStyle Width="50px" /><ItemStyle Width="50px" /></asp:CommandField>--%>
                <asp:BoundField DataField="ToMobile" HeaderText="ToMobile" SortExpression="ToMobile" ><HeaderStyle Width="150px" /><ItemStyle Width="150px" Wrap="False" /></asp:BoundField>
                   <%--<asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" ><HeaderStyle Width="350px" /><ItemStyle Width="350px" Wrap="true" /></asp:BoundField>--%>
                 <asp:TemplateField HeaderText="Message" SortExpression="Message">
                <ItemTemplate>
                    <div style="width: 450px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("Message")%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
                  <asp:BoundField DataField="CreatedBy" HeaderText="SentBy" SortExpression="CreatedBy" ><HeaderStyle Width="120px" /><ItemStyle Width="120px" Wrap="False" /></asp:BoundField>
             <asp:BoundField DataField="CreatedOn" HeaderText="SentOn" SortExpression="CreatedOn" ><HeaderStyle Width="120px" /><ItemStyle Width="120px" Wrap="False" /></asp:BoundField>
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

        </asp:GridView>

                                  <asp:SqlDataSource ID="SqlDSSMS" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
          
                  </asp:SqlDataSource>
                             </td>
                         </tr>

        </table>
           </asp:Panel>
                      <asp:ModalPopupExtender ID="mdlSMS" runat="server" CancelControlID="btnSMSCancel" PopupControlID="pnlSMS" TargetControlID="btndummySMS" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btndummySMS" runat="server" Text="Button" cssclass="dummybutton" />

              <%--End:SMS --%>
             
                       <%-- Start:View Edit History--%>
              
              
              <asp:Panel ID="pnlViewEditHistory" runat="server" BackColor="White" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  Width="95%" Height="85%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0">
                           <tr>
                               <td style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> 
                        <h4 style="color: #000000">View Record History</h4> 
  </td> 
                        
                        <td>
                               <asp:TextBox ID="TextBox5" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                        </td>

                    </tr>
                
           
                    <tr><td style="text-align:left; width: 10%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; padding-left: 40px;">
                        RecordNo&nbsp;&nbsp;  <asp:TextBox ID="txtRecordNoViewHistory" runat="server" Visible="true" Width="20%"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                           </tr>
                
           
                    <tr><td style="text-align:CENTER"><asp:Label ID="Label43" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
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
                    <asp:Button ID="Button1" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

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


                               <%-- Start:View Action Details--%>
              
              
              <asp:Panel ID="pnlViewActionDetails" runat="server" BackColor="White" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  Width="80%" Height="95%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                 
                       <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">View Action Details</h4> 
  </td> <td>
                               <asp:TextBox ID="TextBox6" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
          
        </table>
           

            <table border="0">
      
           
              
             

                
<tr>
<td class="CellFormatSearch" style="width:350px">Action / Service Performed </td>
<td style="font-family:'Calibri'; color:black;    text-align:left;  padding-left:20px; width:80%"><asp:TextBox ID="txtActionView" runat="server" Font-Names="Calibri" Height="60px" MaxLength="3" Enabled="False" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormatSearch" style="width:350px">Materials/Chemicals Used </td>
<td class="CellTextBox"><asp:TextBox ID="txtMaterialsView" runat="server" Font-Names="Calibri" Height="60px" MaxLength="3" Enabled="False" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormatSearch" style="width:350px">Remarks to Client </td>
<td class="CellTextBox"><asp:TextBox ID="txtRemarkClientView" runat="server" Font-Names="Calibri" Height="60px" MaxLength="3" Enabled="False" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormatSearch" style="width:350px">Remarks to Office </td>
<td class="CellTextBox"><asp:TextBox ID="txtRemarkOfficeView" runat="server" Font-Names="Calibri" Height="60px" MaxLength="3" Enabled="False" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
</tr>
<tr><td colspan="4" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:10%; background-color: #C0C0C0;">Service Area Details</td>
</tr>
<tr><td class="CellFormat" style="width:350px">Service Area </td>
<td class="CellTextBox" colspan="1"><asp:TextBox ID="txtServiceAreaView" runat="server" Height="16px" MaxLength="100" style="text-align:right;" Enabled="False" Width="25%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat" style="width:350px">Price Per Service Area </td>
<td class="CellTextBox" colspan="1"><asp:TextBox ID="txtPricePerServiceAreaView" runat="server" Height="16px" MaxLength="100" Enabled="False" style="text-align:right;" Width="25%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat" style="width:350px">Area Completed </td>
<td class="CellTextBox" colspan="1"><asp:TextBox ID="txtAreaCompleted1View" runat="server" Height="16px" MaxLength="100" Enabled="False" style="text-align:right;" Width="25%"></asp:TextBox></td>
</tr>
<tr><td class="CellFormat" style="width:350px">Total Price of Area Completed </td>
<td class="CellTextBox" colspan="1"><asp:TextBox ID="txtTotalPriceOfAreaCompletedView" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" style="text-align:right;" Width="25%"></asp:TextBox></td>
</tr>
<tr><td colspan="2"><br /></td>
</tr>
                <tr><td colspan="2">
<asp:TextBox ID="txtNoofBaitStationView" runat="server" Height="16px" MaxLength="100" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                    <asp:TextBox ID="txtPricePerBaitStaionView" runat="server" Height="16px" MaxLength="100" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                    <asp:TextBox ID="txtTotalPriceofBaitStationView" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                   <asp:TextBox ID="txtServiceBy2View" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                       <asp:TextBox ID="txtSvcRequestNoView" runat="server" Enabled="False" Height="16px" MaxLength="100" ReadOnly="True" style="text-align:right;" Visible="false" Width="25%"></asp:TextBox>
                
                </td></tr>
                    <tr><td colspan="2"><br /></td>
</tr>



        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewActionDetails" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlViewActionDetails" TargetControlID="btnViewActionDetails" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnViewActionDetails" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Action Details--%>



            <%--Start:Timesheet--%>

                        
 <asp:Panel ID="pnlViewTS" runat="server" BackColor="White" Width="98%" Height="95%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table border="0" style="width:100%;padding-left:8px">
       
                 

   <tr>
                             <td colspan="4" style="width:60%; text-align:right">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;"> Time Sheet</h4>

                             </td>

        <td colspan="1" style="width:40%;text-align:right">
                            

                                 <asp:ImageButton ID="btnCloseTS" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
                             </td>
                         </tr>
                          <tr>
               <td colspan="5" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageTS" runat="server"></asp:Label>
                      </td> 
            </tr>
            
                         
                         <tr>
                             <td colspan="5" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';">
                                 <asp:Label ID="lblAlertTS" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td colspan="5" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';">
                                 <asp:Button ID="btnADDTS" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="ADD" Width="100px" />
                                 <asp:Button ID="btnEditTS" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Visible="True" Width="100px" />
                                 <asp:Button ID="btnDeleteTS" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="DELETE" Width="100px" />
                             </td>
                         </tr>
                         <tr>
                             <td colspan="5" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';">&nbsp;</td>
                         </tr>

                             <tr>
                             <td class="CellFormat">Staff Name</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtStaffIDsTS" runat="server" BackColor="#E0E0E0" Height="16px" Width="84%"></asp:TextBox>
                             </td>
                                 <td style="font-size:15px; font-weight:bold; font-family:'Calibri'; color:black; text-align:right;  width:12%;" colspan="2">Service Record No.</td>
                                 <td class="CellTextBox" colspan="1">
                                     <asp:TextBox ID="txtRecordNoTS" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="70%"></asp:TextBox>
                      
                                 </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Location ID</td>
                             <td class="CellTextBox"><asp:TextBox ID="txtLocationIDTS" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="50%"></asp:TextBox></td>
                             <td style="font-size:15px; font-weight:bold; font-family:'Calibri'; color:black; text-align:right;  width:12%;" colspan="2">Client Name</td>
                             <td class="CellTextBox" colspan="1">       
                                  <asp:TextBox ID="txtClientNameTS" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="70%"></asp:TextBox>
                         
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat">Service Address</td>
                             <td class="CellTextBox" colspan="4">
                               <asp:TextBox ID="txtServiceAddressTS" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="70%"></asp:TextBox>
                            
                                     </td>
                         </tr>

                              <tr>
                              <td class="CellFormat">Service Start</td>
                              <td class="CellTextBox">
                                  
<asp:TextBox ID="txtActSvcDateTS" runat="server" Height="16px" MaxLength="10" Width="50%"></asp:TextBox>
                                  
 <asp:TextBox ID="txtActSvcTimeFromTS" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="25%"></asp:TextBox>
                              </td>
                              <td style="font-size:15px; font-weight:bold; font-family:'Calibri'; color:black; text-align:right;  width:12%;" colspan="2">Service End</td>
                              <td class="CellTextBox" colspan="1">
                                  
 <asp:TextBox ID="txtActSvcEndDateTS" runat="server" Height="16px" MaxLength="10" Width="47%"></asp:TextBox>
<asp:TextBox ID="txtActSvcTimeToTS" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="8" Width="15%"></asp:TextBox>
                              </td>
                         </tr>
                        
                         <tr>
                             <td colspan="5" style="width:100%;text-align:center;font-size:18px;font-family:'Calibri';">
                                  <asp:GridView ID="GridViewTS2" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSTS2" Font-Size="12pt" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" Width="80%"><AlternatingRowStyle BackColor="White" />
        <Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False"><ControlStyle Width="50px" /><ItemStyle Width="50px" /></asp:CommandField>
          
              <asp:BoundField DataField="ServiceDateIn" HeaderText="ServiceDate In" SortExpression="ServiceDateIn" DataFormatString="{0:dd/MM/yyyy}">
            <ControlStyle Width="25px" />
            <ItemStyle Width="25px" /></asp:BoundField>
          
              <asp:BoundField DataField="TimeIn" HeaderText="TimeIn" SortExpression="TimeIn">
            <ControlStyle Width="20px" />
            <ItemStyle Width="20px" /></asp:BoundField>
            <asp:BoundField DataField="ServiceDateOut" DataFormatString="{0:dd/MM/yyyy}" HeaderText="ServiceDate Out" SortExpression="ServiceDateOut">
            <ControlStyle Width="25px" />
            <ItemStyle Width="25px" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeOut" HeaderText="TimeOut" SortExpression="TimeOut">
            <ControlStyle Width="20px" />
            <ItemStyle Width="20px" /></asp:BoundField>
<asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration">
            <ControlStyle Width="20px" />
            <ItemStyle Width="20px" /></asp:BoundField>
<asp:BoundField DataField="Comments" HeaderText="Comments"><ItemStyle Width="170px" /></asp:BoundField>

            <asp:BoundField DataField="RecordNo" HeaderText="RecordNo" SortExpression="RecordNo" Visible="False" />
	    <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                <EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate>
                <ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField>
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
            <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False" />
            <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False" />
            <asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False" />
         

        </Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" />
        
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
           </asp:GridView>

                             </td>
                         </tr>
                         
                         <tr>
                             <td colspan="5" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';">
                                 <asp:SqlDataSource ID="SqlDSTS2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" FilterExpression="RecordNo = '{0}'" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters><asp:ControlParameter ControlID="lblRecordNo" Name="RecordNo" PropertyName="Text" Type="String" /></FilterParameters><SelectParameters><asp:ControlParameter ControlID="txtServiceRecord" Name="@RecordNo" PropertyName="Text" /></SelectParameters></asp:SqlDataSource>

                             </td>
                         </tr>
                         
                       
                          <tr style="display:none">
                              <td class="CellFormat">Service Date</td>
                              <td class="CellTextBox">
                                   <asp:TextBox ID="txtServiceDateTS" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="50%"></asp:TextBox></td>
                              <td style="font-size:15px; font-weight:bold; font-family:'Calibri'; color:black; text-align:right;  width:12%;" colspan="2"></td>
                              <td class="CellTextBox"> </td>
                         </tr>
                     
                        
                       
                         
                     
                          <tr>
                              <td class="CellFormat">&nbsp;Time In </td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtTimeInDateTS" runat="server" Height="16px" MaxLength="10" Width="50%"></asp:TextBox>
                                  <asp:CalendarExtender ID="CalendarExtender6TS" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtEditSchDateTS" TargetControlID="txtTimeInDateTS">
                                  </asp:CalendarExtender>
                                  <asp:TextBox ID="txtTimeInTS" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="8" Width="25%"></asp:TextBox>
                                  <asp:MaskedEditExtender ID="MaskedEditExtender3TS" runat="server" AutoComplete="False" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeInTS">
                                  </asp:MaskedEditExtender>
                              </td>
                              <td style="font-size:15px; font-weight:bold; font-family:'Calibri'; color:black; text-align:right;  width:12%;" colspan="2">Time Out</td>
                              <td class="CellTextBox" colspan="1">
                                  <asp:TextBox ID="txtTimeOutDateTS" runat="server" Height="16px" MaxLength="10" Width="47%"></asp:TextBox>
                                  <asp:CalendarExtender ID="txtTimeOutDateTS_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtEditSchDateTS" TargetControlID="txtTimeOutDateTS">
                                  </asp:CalendarExtender>
                                  <asp:TextBox ID="txtTimeOutTS" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="8" Width="15%"></asp:TextBox>
                                  <asp:MaskedEditExtender ID="MaskedEditExtender4TS" runat="server" AutoComplete="False" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeOutTS">
                                  </asp:MaskedEditExtender>
                              </td>
                         </tr>
                          
             
                         <tr>
                             <td class="CellFormat">Duration</td>
                             <td class="CellTextBox" colspan="1">
                                 <asp:TextBox ID="txtDurationTS" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                             </td>

                               <td style="font-size:15px; font-weight:bold; font-family:'Calibri'; color:black; text-align:right;  width:12%;" colspan="2"></td>
                               <td class="CellTextBox" colspan="1">
                                 
                               </td>
                         </tr>
             
                         <tr>
                             <td class="CellFormat">Comments<br />
                                 <br />
                                 <asp:TextBox ID="txtCharCountEditRemarksTS" runat="server" BorderStyle="None" Enabled="False" ForeColor="Red" style="text-align:right" Visible="False" BackColor="White" > 4000 characters left</asp:TextBox>
                             </td>
                             <td class="CellTextBox" colspan="4">
                                 <asp:TextBox ID="txtCommentsTS" runat="server" Font-Names="Calibri" Font-Size="12pt" Height="60px" MaxLength="4000" TextMode="MultiLine" Width="80%" onKeyup="return CalculateCharsEditRemarksTS();"></asp:TextBox>
                             </td>
                         </tr>
                        
                       
                        
                         <tr>
                             <td colspan="5"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="5" style="text-align:center"><asp:Button ID="btnSaveTS" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelTS" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>

        </table>
           </asp:Panel>

              
           <asp:ModalPopupExtender ID="mdlViewTS" runat="server" CancelControlID="btnCloseTS" PopupControlID="pnlViewTS" TargetControlID="btnDummyViewTS" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewTS" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
           <%--   End:TiemSheet--%>


                  <%--Confirm delete uploaded file--%>
                                              
                 <asp:Panel ID="pnlConfirmDeleteRecord" runat="server" BackColor="White" Width="300px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label19" runat="server" Text="Confirm DELETE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label20" runat="server" Text="Are you sure to DELETE the Record?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmDeleteRecordTS" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnCancelDeleteRecordTS" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupDeleteRecord" runat="server" CancelControlID="btnCancelDeleteRecordTS" PopupControlID="pnlConfirmDeleteRecord" TargetControlID="btndummyDeleteRecord" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyDeleteRecord" runat="server" CssClass="dummybutton" />

             <%-- Confirm Delete uploaded file--%>



              <%-- Start:View Message--%>
                 <asp:Panel ID="pnlConfirmMsg" runat="server" BackColor="White" Width="540px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                  <tr style="background-color:navy; color:white;font-size:medium;width:100%">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label60" runat="server" Text="Notification"></asp:Label>
                        
                      </td>
                           </tr>
               <tr>
                             <td colspan="2" class="auto-style3"><br /></td>
                         </tr> 
               <tr>
                       <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                    
                         
                          &nbsp;<asp:Label ID="lblpnlConfirmMsg1" runat="server" Text=""></asp:Label>
                        
                      </td>
                           </tr>
             <%--  <tr>
                       <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                    
                         
                          &nbsp;<asp:Label ID="Label63" runat="server" Text="Please key-in the Start Date and End Date to display fewer records."></asp:Label>
                        
                      </td>
                           </tr>--%>
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

               
      <asp:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />
              <%-- End: View Message--%>


  <%--  12.05.21--%>

          <%--Start : InActive Accounts--%>
                
                                              
                 <asp:Panel ID="pnlInActiveAccount" runat="server" BackColor="White" Width="500px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="None">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium; ">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center; " colspan="2">
                         
                          <asp:Label ID="lblInactiveAccount" runat="server" Text="INACTIVE ACCOUNT"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr style="height:20px;">
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="lblInactiveAccount1" runat="server" Text=""></asp:Label>
                        
                      </td>
                           </tr>

               <tr style="height:20px;">
                      <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto; width:14%; padding-left:2%;">
                         
                           Account ID :</td>
                           <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto; width:80%;;padding-left:2%;">
                               <asp:Label ID="lblInactiveAccount2" runat="server" Text=""></asp:Label>
                      </td>
                           </tr>



               <tr style="height:20px;">
                       <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto;width:14%; padding-left:2%;">
                         
                          Name :
                        
                      </td>
                           <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto; width:80%;padding-left:2%;">
                               <asp:Label ID="lblInactiveAccount3" runat="server"></asp:Label>
                           </td>
                           </tr>

               <tr style="height:25px;">
                      <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto;width:15%; padding-left:2%;">
                         
                         Reason/Remarks: 
                        
                      </td>
                           <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto; width:80%;padding-left:2%;">
                               <asp:Label ID="lblInactiveAccount4" runat="server"></asp:Label>
                           </td>
                           </tr>
              
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkInActiveAccounts" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"   Font-Bold="True" Text="OK" Width="100px"/>
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlInActiveAccount" runat="server" CancelControlID="" PopupControlID="pnlInActiveAccount" TargetControlID="btnDummyInActiveAccount" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyInActiveAccount" runat="server" CssClass="dummybutton" />

            

        <%-- End : Inactive Accounts--%>

        <%-- 12.05.21  --%>



                <asp:Panel ID="pnlCustRequest" runat="server" BackColor="White" Width="600px" Height="550px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Customer Request (View)</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageCustRequest" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertCustRequest" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                          <tr>
                      <td class="CellFormat" style="width:20%">Request No</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtCustRequestNo" ReadOnly="true" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ForeColor="Black"></asp:TextBox>
                               
                    <%--     <asp:ModalPopupExtender ID="mdlpopupEditTeamSearch" runat="server" CancelControlID="btnPnlTeamClose" PopupControlID="pnlPopUpTeam"
                                    TargetControlID="btndummyTeamSearch" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
                                </asp:ModalPopupExtender>--%>
                      </td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">Requestor's Name</td>
                    <td class="CellTextBox"> <asp:TextBox ID="txtCustRequestName" BackColor="#E0E0E0" ReadOnly="true" runat="server" Height="16px" MaxLength="100" Width="70%"></asp:TextBox>
                         </td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">Service Address</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCustRequestSvcAddr" BackColor="#E0E0E0" runat="server" MaxLength="500" Height="40px" Width="70%" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>  
                     <br />   <asp:Label ID="lblCustRequestSvcAddress" runat="server" Text="" ForeColor="#993333" Font-Bold="True" Font-Size="13px"></asp:Label>
                    </td>
                                         
                  </tr>
<%--                           <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>--%>
   <tr>
      <td class="CellFormat">Urgent</td>
                      <td class="CellTextBox">
                        <asp:CheckBox ID="chkCustRequestUrgent" runat="server" Text="" Enabled="FALSE" TextAlign="right" />     

                      </td>
                                         
                  </tr>
                <tr>
                      <td class="CellFormat">Type of Service</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCustRequestSvcType" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ReadOnly="true"></asp:TextBox>
                          
                    </td>
                                         
                  </tr>
                         <tr>
                      <td class="CellFormat">Required Date</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCustRequestRequiredDate" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ReadOnly="true"></asp:TextBox>
                          
                    </td>
                                         
                  </tr>
                         <tr>
                      <td class="CellFormat">Required Time</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCustRequestRequiredTime" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ReadOnly="true"></asp:TextBox>
                          
                    </td>
                                         
                  </tr>
                         
                <tr>
                      <td class="CellFormat">Required Services</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCustRequestRequiredServices" BackColor="#E0E0E0" runat="server" MaxLength="500" Height="40px" Width="70%" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>  
                          
                    </td>
                                         
                  </tr>
                            <tr>
                      <td class="CellFormat">Instructions & Comments</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCustRequestInstructions" BackColor="#E0E0E0" runat="server" MaxLength="500" Height="40px" Width="70%" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>  
                          
                    </td>
                                         
                  </tr>
                           <tr>
                      <td class="CellFormat">Send Notifications to</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtCustRequestNotifications" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ReadOnly="true"></asp:TextBox>
                          
                    </td>
                                         
                  </tr>
                        <%-- <tr>
                      <td class="CellFormat">Status</td>
                    <td class="CellTextBox"><asp:DropDownList ID="ddlCustRequestStatus" runat="server" Width="71%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                  <asp:ListItem Value="OPEN">OPEN</asp:ListItem>
                                   <asp:ListItem Value="SCHEDULED">SCHEDULED</asp:ListItem>                           
                               </asp:DropDownList>
                    </td>
                                         
                  </tr>--%>
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnCustRequestUpdate" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Update" Width="120px" Visible="false"/>
                                 <asp:Button ID="btnCustRequestCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" />
                               </td>
                         </tr>
                          <tr>
                              <td>
                                  <br />
                              </td>
                          </tr>
                 

        </table>
           </asp:Panel>
                <asp:Button ID="btndummyCustRequest" runat="server" Text="Button" cssclass="dummybutton" />
        
               <asp:ModalPopupExtender ID="mdlPopupCustRequest" runat="server" CancelControlID="btnCustRequestCancel" PopupControlID="pnlCustRequest" TargetControlID="btndummyCustRequest" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>


                 <asp:Panel ID="pnlImportRequest" runat="server" BackColor="White" Width="750px" Height="600px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Customer Request (View)</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageImportRequest" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertImportRequest" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         <tr>
                             <td colspan="2">
                                   <asp:TextBox ID="txtSearchImportCustRequest" Visible="true" runat="server" MaxLength="50" Height="16px" Width="300px" Text = "Search Here for Request details" ForeColor = "Gray" onblur = "WaterMarkImportRequest(this, event);" onfocus = "WaterMarkImportRequest(this, event);" AutoPostBack="True"></asp:TextBox>
                     <asp:ImageButton ID="btnResetImportCustRequest" runat="server" ImageUrl="~/Images/reset1.png" Height="16px" Width="18px" ToolTip="RESET CUST REQUEST" />
 
        <asp:TextBox ID="txtSearchImportCustRequest1" runat="server" Visible="False"></asp:TextBox>
             
                             </td>
                         </tr>
                         <tr>
                             <td colspan="2" style="text-align:center">
                                 <asp:TextBox ID="txtImportRequestRcNo" runat="server" Visible="false"></asp:TextBox>
                                                  <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="150px" ScrollBars="Auto" style="text-align:center;margin-left:0%; margin-right:auto;" Visible="true" Width="99%">
                
                                   <asp:GridView ID="grdImportClientRequest" runat="server" AutoGenerateColumns="false" AllowSorting="True" BackColor="White" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataSourceID="SqlDSImportCustRequest" Font-Names="Calibri" Font-Size="15px" ForeColor="#333333" GridLines="Vertical" Height="100%" RowStyle-CssClass="gridcell" Width="100%">
                                      <Columns>
                                            <asp:BoundField DataField="Status" HeaderText="Status">
                    <ItemStyle Wrap="False" Width="45px" />
                    </asp:BoundField>
                  <asp:BoundField DataField="RequestNo" HeaderText="RequestNo">
                    <ItemStyle Wrap="False" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="Requester">
                    <ItemStyle Wrap="False" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Urgent" ItemStyle-HorizontalAlign="Center"> 
                        <ItemStyle Wrap="False" Width="35px" />
               <ItemTemplate>
                         <asp:CheckBox ID="chkUrgentCustRequest" runat="server" Checked='<%# IIf(Eval("UrgentService").ToString().Equals("1"), True, False)%>' Enabled="false"/>
</ItemTemplate>
                                                          </asp:TemplateField> 
                                           <%-- <asp:TemplateField HeaderText="Services Required" >
                                                <HeaderStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <div style="width: 150px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("RequiredServices")%>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>--%>
                                             <asp:BoundField DataField="RequiredDate" HeaderText="RequiredDate" DataFormatString="{0:dd/MM/yyyy}" >
                    <ItemStyle Wrap="True" Width="40px" />
                    </asp:BoundField>
               
                     <asp:BoundField DataField="RequiredServices" HeaderText="Services Required">
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle Wrap="True" Width="150px" />
                    </asp:BoundField>
                                              <asp:BoundField DataField="ServiceAddress" HeaderText="ServiceAddress">
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle Wrap="True" Width="150px" />
                    </asp:BoundField>
                              <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkImportCustRequest" runat="server" CommandArgument='<%# Eval("Rcno")%>' OnClick="ImportCustRequest" Text="View" /></ItemTemplate>
                                    <ItemStyle Wrap="False" Width="38px" />
                              </asp:TemplateField>
     

                   
                </Columns>
                                      <EditRowStyle BackColor="#2461BF" />
                                      <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                      <HeaderStyle BackColor="#2461BF" Font-Bold="True" Font-Names="Calibri" ForeColor="White" />
                                      <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Left" />
                                      <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
                                      <SelectedRowStyle BackColor="#AEE4FF" Font-Bold="True" ForeColor="#333333" />
                                      <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                      <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                      <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                      <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                  </asp:GridView>
                                                             <asp:SqlDataSource ID="SqlDSImportCustRequest" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                          
                                                      </asp:Panel>
                             </td>
                         </tr>
                          <tr>
                      <td class="CellFormat" style="width:20%">Request No</td>
                    <td class="CellTextBox">  <asp:TextBox ID="txtImportRequestNo" ReadOnly="true" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ForeColor="Black"></asp:TextBox>
           
                      </td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">Requestor's Name</td>
                    <td class="CellTextBox"> <asp:TextBox ID="txtImportRequestorName" BackColor="#E0E0E0" ReadOnly="true" runat="server" Height="16px" MaxLength="100" Width="70%"></asp:TextBox>
                         </td>
                                         
                  </tr>

                <tr>
                      <td class="CellFormat">Service Address</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtImportRequestSvcAddr" BackColor="#E0E0E0" runat="server" MaxLength="500" Height="40px" Width="70%" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>  
                     <br />   <asp:Label ID="lblImportRequestSvcAddr" runat="server" Text="" ForeColor="#993333" Font-Bold="True" Font-Size="13px"></asp:Label>
                    </td>
                                         
                  </tr>
<%--                           <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>--%>
   <tr>
      <td class="CellFormat">Urgent</td>
                      <td class="CellTextBox">
                        <asp:CheckBox ID="chkImportRequestUrgent" runat="server" Text="" Enabled="FALSE" TextAlign="right" />     

                      </td>
                                         
                  </tr>
                <tr>
                      <td class="CellFormat">Type of Service</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtImportRequestSvcType" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ReadOnly="true"></asp:TextBox>
                          
                    </td>
                                         
                  </tr>
                         <tr>
                      <td class="CellFormat">Required Date</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtImportRequestRequiredDate" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ReadOnly="true"></asp:TextBox>
                          
                    </td>
                                         
                  </tr>
                         <tr>
                      <td class="CellFormat">Required Time</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtImportRequestRequiredTime" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ReadOnly="true"></asp:TextBox>
                          
                    </td>
                                         
                  </tr>
                         
                <tr>
                      <td class="CellFormat">Required Services</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtImportRequestReqSvcs" BackColor="#E0E0E0" runat="server" MaxLength="500" Height="40px" Width="70%" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>  
                          
                    </td>
                                         
                  </tr>
                            <tr>
                      <td class="CellFormat">Instructions & Comments</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtImportRequestInstructions" BackColor="#E0E0E0" runat="server" MaxLength="500" Height="40px" Width="70%" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>  
                          
                    </td>
                                         
                  </tr>
                           <tr>
                      <td class="CellFormat">Send Notifications to</td>
                    <td class="CellTextBox"><asp:TextBox ID="txtImportRequestEmailNotifications" BackColor="#E0E0E0" runat="server" MaxLength="25" Height="16px" Width="70%" ReadOnly="true"></asp:TextBox>
                          
                    </td>
                                         
                  </tr>
                         
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnImportCustRequestDetails" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Import" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnCancelCustRequestDetails" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          <tr>
                              <td>
                                  <br />
                              </td>
                          </tr>
                 

        </table>
           </asp:Panel>
                <asp:Button ID="btnDummyImportCustRequest" runat="server" Text="Button" cssclass="dummybutton" />
        
               <asp:ModalPopupExtender ID="mdlPopupImportCustRequest" runat="server" CancelControlID="btnCancelCustRequestDetails" PopupControlID="pnlImportRequest" TargetControlID="btnDummyImportCustRequest" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>

      <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" CancelControlID="btnStatusCancel" PopupControlID="pnlStatusSearch" TargetControlID="dummystatussearch" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>

        <asp:ModalPopupExtender ID="mdlPopupPrint" runat="server"  PopupControlID="pnlPrint" TargetControlID="btndummyPrint" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>

     <asp:ModalPopupExtender ID="mdlPopupStatus" runat="server" CancelControlID="btnCancelStatus" PopupControlID="pnlStatus" TargetControlID="btndummy1" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:ModalPopupExtender ID="mdlPopupSchDate" runat="server" CancelControlID="btnSchCancel" PopupControlID="pnlEditSchDate" TargetControlID="btndummySchDate" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:ModalPopupExtender ID="mdlPopupEditTeam" runat="server" CancelControlID="btnEditTeamCancel" PopupControlID="pnlEditTeam" TargetControlID="btndummyEditTeam" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:ModalPopupExtender ID="mdlPopupSeriesAlert" runat="server" CancelControlID="btnSeriesCancel" PopupControlID="PnlSeriesAlert" TargetControlID="btndummySeriesAlert" BehaviorID="mdlPopSeriesAlert" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
     
     <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnClose" PopupControlID="Panel4" TargetControlID="btnFilter" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
            <asp:Button ID="btndummy" runat="server" Text="Button" cssclass="dummybutton" />
              <asp:Button ID="btndummyNew" runat="server" Text="Button" cssclass="dummybutton" />
   
       <asp:Button ID="dummy" runat="server" cssclass="dummybutton" />
      <asp:Button ID="dummystatussearch" runat="server" cssclass="dummybutton" />
     <asp:Button ID="btndummySchDate" runat="server" cssclass="dummybutton" />
      <asp:Button ID="btndummyTeamSearch" runat="server" cssclass="dummybutton" />
     <asp:Button ID="btndummyEditTeam" runat="server" cssclass="dummybutton" />
     <asp:Button ID="btndummySeriesAlert" runat="server" cssclass="dummybutton"  />

            <asp:Button ID="btndummyPrint" runat="server" cssclass="dummybutton" />
          
      <asp:Button ID="btndummy1" runat="server" Text="Button" cssclass="dummybutton" />
      <asp:Button ID="btndummy2" runat="server" Text="Button" cssclass="dummybutton" />
   
                  <asp:Panel ID="pnlConfirmMsg1" runat="server" BackColor="White" Width="300px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label7" runat="server" Text="Message Sent Successfully"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmOk1" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelMsg1" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupMsg1" runat="server" CancelControlID="btnCancelMsg1" PopupControlID="pnlConfirmMsg1" TargetControlID="btndummyMsg1" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg1" runat="server" CssClass="dummybutton" />
               <asp:TextBox ID="TextBox4" runat="server" CssClass="dummybutton"></asp:TextBox>
    <asp:TextBox ID="txtsvc" runat="server" CssClass="dummybutton"></asp:TextBox>
       <asp:TextBox ID="txtGridIndex" runat="server" CssClass="dummybutton"></asp:TextBox>
  
    <asp:SqlDataSource ID="SqlDSScheduleType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
             
                         <asp:SqlDataSource ID="SqlDSState" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSCity" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSCountry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSSalesGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
              <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
    <asp:TextBox ID="txtAccountID1" runat="server" CssClass="dummybutton"></asp:TextBox>   
      <asp:TextBox ID="txtAccountType" runat="server" CssClass="dummybutton"></asp:TextBox>
    <asp:Label ID="lblUserID" runat="server" Text="" Visible="False"></asp:Label>
    <asp:TextBox ID="txtOldSvcBy" runat="server" Visible="false"></asp:TextBox>
              <asp:TextBox ID="txtCopy" runat="server" CssClass="dummybutton"></asp:TextBox>
             
  </ContentTemplate>
        <Triggers>
          
         
   <asp:PostBackTrigger ControlID="btnExportToExcel" />
           
            <asp:PostBackTrigger ControlID="tb1$TabPanel9$btnPestPhotoUpload" />
         
            <asp:PostBackTrigger ControlID="tb1$TabPanel9$btnDownloadPestPhotos" />
            <asp:PostBackTrigger ControlID="tb1$TabPanel2$btnSvcDelete" />
             <asp:PostBackTrigger ControlID="tb1$TabPanel2$btnSvcSave" />
            <asp:PostBackTrigger ControlID="tb1$TabPanel2$btnClientImport" />
         <asp:PostBackTrigger ControlID="grdImportClientRequest" />
            <asp:PostBackTrigger ControlID="btnImportCustRequestDetails" />
             <asp:PostBackTrigger ControlID="hdnqueryselected" />
            <asp:PostBackTrigger ControlID="hdnGetCalendarResult" />
            <asp:PostBackTrigger ControlID="hdnTeamIDList" />
            <asp:PostBackTrigger ControlID="hdnCalDisplayView" />
            <asp:PostBackTrigger ControlID="hdnCalDisplayFormat" />
            <asp:AsyncPostBackTrigger ControlID="btnJustThisOne" />
             <asp:AsyncPostBackTrigger ControlID="btnSchCancel" /> 
             <asp:AsyncPostBackTrigger ControlID="btnSchSave" /> 
             <asp:AsyncPostBackTrigger ControlID="rdoCalendarView" />
             <asp:AsyncPostBackTrigger ControlID="rdoListView" />
          
            </Triggers>
        
</asp:UpdatePanel>
    
   

     <script lang="javascript" type="text/javascript">

         function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
             var tbl = document.getElementById(gridId);
             if (tbl) {
                 var DivHR = document.getElementById('DivHeaderRow');
                 var DivMC = document.getElementById('DivMainContent');
                 var DivFR = document.getElementById('DivFooterRow');

                 //*** Set divheaderRow Properties ****
                 DivHR.style.height = headerHeight + 'px';
                 DivHR.style.width = (parseInt(width) - 16) + 'px';
                 DivHR.style.position = 'relative';
                 DivHR.style.top = '0px';
                 DivHR.style.zIndex = '1';
                 DivHR.style.verticalAlign = 'top';

                 //*** Set divMainContent Properties ****
                 DivMC.style.width = width + 'px';
                 DivMC.style.height = height + 'px';
                 DivMC.style.position = 'relative';
                 DivMC.style.top = -headerHeight + 'px';
                 DivMC.style.zIndex = '1';
                 DivMC.style.paddingtop = '2px';

                 //*** Set divFooterRow Properties ****
                 DivFR.style.width = (parseInt(width) - 16) + 'px';
                 DivFR.style.position = 'relative';
                 DivFR.style.top = -headerHeight + 'px';
                 DivFR.style.verticalAlign = 'top';
                 DivFR.style.paddingtop = '2px';

                 if (isFooter) {
                     var tblfr = tbl.cloneNode(true);
                     tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                     var tblBody = document.createElement('tbody');
                     tblfr.style.width = '100%';
                     tblfr.cellSpacing = "0";
                     tblfr.border = "0px";
                     tblfr.rules = "none";
                     //*****In the case of Footer Row *******
                     tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                     tblfr.appendChild(tblBody);
                     DivFR.appendChild(tblfr);
                 }
                 //****Copy Header in divHeaderRow****
                 DivHR.appendChild(tbl.cloneNode(true));
             }
         }



         function OnScrollDiv(Scrollablediv) {
             document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
             document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
         }


</script>

  <script type="text/javascript">

      var defaultTextAsset = "Search Vehicle Here";
      function WaterMarkAsset(txt, evt) {
          if (txt.value.length == 0 && evt.type == "blur") {
              txt.style.color = "gray";
              txt.value = defaultTextAsset;
          }
          if (txt.value == defaultTextAsset && evt.type == "focus") {
              txt.style.color = "black";
              txt.value = "";
          }
      }

      var defaultTextStaff = "Search Here";
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
      function LoadDiv(url) {

          var img = new Image();
          var bcgDiv = document.getElementById("divBackground");
          var imgDiv = document.getElementById("divImage");
          var imgFull = document.getElementById("imgFull");
          var imgLoader = document.getElementById("imgLoader");
          imgLoader.style.display = "block";
          img.onload = function () {
              imgFull.src = img.src;
              imgFull.style.display = "block";
              imgLoader.style.display = "none";
          };
          img.src = url;
          var width = document.body.clientWidth;
          if (document.body.clientHeight > document.body.scrollHeight) {
              bcgDiv.style.height = document.body.clientHeight + "px";
          }
          else {
              bcgDiv.style.height = document.body.scrollHeight + "px";
          }
          imgDiv.style.left = (width - 650) / 2 + "px";
          imgDiv.style.top = "20px";
          bcgDiv.style.width = "100%";

          bcgDiv.style.display = "block";
          imgDiv.style.display = "block";
          return false;
      }
      function HideDiv() {
          var bcgDiv = document.getElementById("divBackground");
          var imgDiv = document.getElementById("divImage");
          var imgFull = document.getElementById("imgFull");
          if (bcgDiv != null) {
              bcgDiv.style.display = "none";
              imgDiv.style.display = "none";
              imgFull.style.display = "none";
          }
      }

      function SetTarget() {

          document.forms[0].target = "_blank";

      }

      function ResetScrollPosition() {
          setTimeout("window.scrollTo(0,0)", 0);
      }

      function ResetScrollPosition1() {
          setTimeout("window.scrollTo(1000,0)", 0);
      }

      function ResetScrollPosition2() {
          setTimeout("window.scrollTo(0,3000)", 0);
      }


      // $(function () {
      //     $('[id*=txtEditRemarks]').on("keydown", function () {
      //         $('[id*=lblCharCountRemarks]').html($(this).val().length);
      //     });
      // });


      //function countremarkschar() {
      //    document.getElementById("lblCharCountRemarks").innerHTML = document.getElementById("txtEditRemarks").value.length;
      //}

      //function countsichar()
      //{
      //    document.getElementById("lblCharCountSI").innerHTML = document.getElementById("txtEditServiceInstruction").value.length;
      //}
</script>
    <style type="text/css">
body
{
    margin: 0;
    padding: 0;
    height: 100%;
}
.modal
{
    display: none;
    position: absolute;
    top: 0px;
    left: 0px;
    background-color: black;
    z-index: 100;
    opacity: 0.8;
    filter: alpha(opacity=60);
    -moz-opacity: 0.8;
    min-height: 100%;
}
#divImage
{
    display: none;
    z-index: 1000;
    position: fixed;
    top: 0;
    left: 0;
    background-color: White;
    height: 550px;
    width: 600px;
    padding: 3px;
    border: solid 1px black;
}
</style>
      <style type="text/css">
      
           .roundbutton1 {
            border: 2px solid #a1a1a1;   
    background: #dddddd;   
    border-radius: 25px;
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
    width: 100px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 98px;
    width: 98px;
}
         .gridcell {
             word-break: break-all;
         }

          .tab {
              display: inline;
              visibility: visible;
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
      .CellTextBox{
         font-family:'Calibri';
        color:black;
        text-align:left;
     padding-left:20px;
    }
          .CellFormatnew
          {
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
      .CellTextBoxnew{
          color:black;
        text-align:left;
        width:40%;
    }
    .CellFormatSearch{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:.5%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
      .CellTextBoxSearch{
         font-family:'Calibri';
        color:black;
        text-align:left;
     padding-left:1px;
    }
  
          .auto-style1 {
              width: 20%;
          }
  
          .auto-style2 {
              width: 88%;
          }
            
          .auto-style3 {
              font-size: 15px;
              font-weight: bold;
              font-family: Calibri;
              color: black;
              text-align: right;
              width: 30%;
              border-collapse: collapse;
              border-spacing: 0;
              line-height: 10px;
              height: 34px;
          }
            
          </style>

       <script lang ="javascript" type ="text/javascript" >

           function openInNewTab() {
               window.document.forms[0].target = '_blank';
               setTimeout(function () { window.document.forms[0].target = ''; }, 0);
           }

</script>
<script type="text/javascript">

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

    function CheckBoxListSelect(cbControl) {
        var chkBoxList = document.getElementById(cbControl);
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        // var clicked = this;


        chkBoxCount[7].onclick = function () {
            for (var i = 0; i < chkBoxCount.length - 1; i++) {

                chkBoxCount[i].checked = chkBoxCount[7].checked;
            }
        }
    }

    function messageText(str) {
        var txt = document.getElementById('<%= lblMessage.ClientID%>');

        txt.innerHTML = str;

    }

    function alertText(str) {
        var txt = document.getElementById('<%= lblAlert.ClientID%>');

        txt.innerHTML = str;

    }

    function ConfirmUpdate() {

        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (document.getElementById("<%=txtSvcRecord.ClientID%>").value != '') {

            if (confirm("Do you wish to " + document.getElementById("ContentPlaceHolder1_btnUpdate").value + " Record No : " + document.getElementById("<%=txtSvcRecord.ClientID%>").value + "?")) {
                confirm_value.value = "Yes";

            } else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }
        currentdatetime();
    }


    function ConfirmDeleteSvc() {

        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (document.getElementById("<%=txtSvcRecord.ClientID%>").value != '') {

            if (confirm("Do you wish to DELETE Record No : " + document.getElementById("<%=txtSvcRecord.ClientID%>").value + "?")) {
                confirm_value.value = "Yes";

            } else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }
    }

    function ConfirmDeleteSvcDetail() {

        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (document.getElementById("<%=txtSvcRecord.ClientID%>").value != '') {

            if (confirm("Do you wish to DELETE Service detail of Record No : " + document.getElementById("<%=txtSvcRecord.ClientID%>").value + "?")) {
                confirm_value.value = "Yes";

            } else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }
    }

    function TabChanged(sender, e) {

        if (sender.get_activeTabIndex() == 0) {
            if ((document.getElementById("<%=txtMode.ClientID%>").value == 'Add') || (document.getElementById("<%=txtMode.ClientID%>").value == 'Edit')) {
                $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                alert("Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!");
                return;
            }
        }
        if (sender.get_activeTabIndex() != 0) {

            if (document.getElementById("<%=txtSvcRecord.ClientID%>").value == '') {
                $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                alert("Please select a Service Record to proceed.");
                return;
            }
           



        }
        else {
          



        }

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

    var defaultTextScheduler = "Search Scheduler Here";
    function WaterMarkScheduler(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextScheduler;
        }
        if (txt.value == defaultTextScheduler && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

    var defaultTextTeam = "Search Here for Team, Incharge or ServiceBy";
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

    var defaultTextClient = "Search Here for AccountID or Client details";
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


    var defaultTextRequest = "Search Here for Request details";
    function WaterMarkRequest(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextRequest;
        }
        if (txt.value == defaultTextRequest && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

    var defaultTextImportRequest = "Search Here for Request details";
    function WaterMarkImportRequest(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextImportRequest;
        }
        if (txt.value == defaultTextImportRequest && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }

    var defaultTextLocation = "Search Here for Location Address, Postal Code or Description";
    function WaterMarkLocation(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultTextLocation;
        }
        if (txt.value == defaultTextLocation && evt.type == "focus") {
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
        document.getElementById("<%=txtCreatedOn.ClientID()%>").value = dd + "/" + mm + "/" + y;

    }

    function UpdateBillingDetails() {

        var updatebilladdress = document.getElementById("<%=chkOffAddr.ClientID%>");
        if (updatebilladdress.checked == true) {

            document.getElementById("<%=txtBillAddress.ClientID%>").value = document.getElementById("<%=txtOffAddress1.ClientID%>").value;
            document.getElementById("<%=txtBillStreet.ClientID%>").value = document.getElementById("<%=txtOffStreet.ClientID%>").value;
            document.getElementById("<%=txtBillBuilding.ClientID%>").value = document.getElementById("<%=txtOffBuilding.ClientID%>").value;

            document.getElementById("<%=ddlBillCity.ClientID%>").value = document.getElementById("<%=ddlOffCity.ClientID%>").value;
            document.getElementById("<%=ddlBillState.ClientID%>").value = document.getElementById("<%=ddlOffState.ClientID%>").value;
            document.getElementById("<%=ddlBillCountry.ClientID%>").value = document.getElementById("<%=ddlOffCountry.ClientID%>").value;

            document.getElementById("<%=txtBillPostal.ClientID%>").value = document.getElementById("<%=txtOffPostal.ClientID%>").value;


        }
    }


    function CalculateCharsEditRemarks() {
        //alert("1");
        var txteditRem = document.getElementById("<%=txtEditRemarks.ClientID%>");
        document.getElementById("<%=txtCharCountEditRemarks.ClientID%>").value = 4000 - document.getElementById("<%=txtEditRemarks.ClientID%>").value.length + " characters left";
    }

    function CalculateCharsEditSI() {
        //alert("2");
        var txteditRem1 = document.getElementById("<%=txtEditServiceInstruction.ClientID%>");
        //alert("3");
        document.getElementById("<%=txtCharCountEditSI.ClientID%>").value = 2000 - document.getElementById("<%=txtEditServiceInstruction.ClientID%>").value.length + " characters left";
    }


    function CalculateCharsDescription(context) {
        //alert("4");
        var txteditRem = document.getElementById("<%=txtDescription.ClientID%>");
        document.getElementById("<%=txtCharCountServiceDescription.ClientID%>").value = 2000 - txteditRem.value.length + " characters left";
    }

    function CalculateCharsInstruction(context) {
        ////alert("5");
        var txteditRem = document.getElementById("<%=txtInstruction.ClientID%>");
        document.getElementById("<%=txtCharCountSI.ClientID%>").value = 2000 - txteditRem.value.length + " characters left";
    }


    function CalculateCharsRemarks(context) {
        //alert("6");
        var txteditRem = document.getElementById("<%=txtRemarks.ClientID%>");
        document.getElementById("<%=txtCharCountRemarks.ClientID%>").value = 4000 - txteditRem.value.length + " characters left";
    }

    function CalculateCharsBillingDescription(context) {
        //alert("");
        var txteditRem = document.getElementById("<%=txtBillingDescription.ClientID%>");
        document.getElementById("<%=txtCharCountBillingDescription.ClientID%>").value = 2000 - txteditRem.value.length + " characters left";
    }


</script>



    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

        //global variables for calendar view
        var addedTeamIdcolors = [];
        var addedLegends = [];
        var legendArray = [];
        var isdraggedfromseries = false;
        var eventText = [];
        var teamIDList = [];
        var TeamIdObj = [];
        var ServiceDatearray = [];
        var startdate;
        var calendarDisplay;

        //function to get colors for the legend
        function getRandomColor(usePrev) {
            if (usePrev && prevColor)
                return prevColor;
            //var letters = '0123456789ABCDEF';  //Dark Colors
            var letters = 'BCDEF'.split(''); //Light Colors
            var color = '#';
            for (var i = 0; i < 6; i++) {
                //color += letters[Math.floor(Math.random() * 16)];
                color += letters[Math.floor(Math.random() * letters.length)];
            }
            prevColor = color;
            return color;
        }
        function alertMessage(str) {
            alert(str);
        }

        
   <%--     $(document).ready(function () {
            $('#ContentPlaceHolder1_rdoCalendarView').prop("disabled", true);

            $('#ContentPlaceHolder1_rdoCalendarView').click(function () {
                console.log("ContentPlaceHolder1_rdoCalendarView");
                var search1TeamID = document.getElementById("<%=txtSearch1Team.ClientID%>").value;

            if (search1TeamID == "") {
                alert("Team Id cannot be empty.");
                return false;
            }
            $('#ContentPlaceHolder1_rdoCalendarView').prop("disabled", false);
            });

            });--%>


        //function that renders the calendar view based on the team id
        function onclickCalendar() {
            
            var view = document.getElementById("<%=rdoCalendarView.ClientID%>").checked;
            var search1TeamID = document.getElementById("<%=txtSearch1Team.ClientID%>").value;
            
            var search1SvcBy = document.getElementById("<%=txtSearch1SvcBy.ClientID%>").value;
           
            if (search1TeamID == "") {
                search1TeamID = search1SvcBy;
            }
          

            var DefaultCalenderdisplayView = document.getElementById("<%= hdnCalDisplayView.ClientID()%>").value;

            console.log("DefaultCalenderdisplayView", DefaultCalenderdisplayView);
            <%--calendarDisplay = $('#<%= txtCalendarDisplay.ClientID%>').val();--%>
            calendarDisplay = $('#<%= txtSearch1SvcDate.ClientID%>').val();


            if (view == true && search1TeamID != "") {

                //empty the arrays when re-rendering the calendar
                eventText = [];
                teamIDList = [];
                TeamIdObj = [];
                ServiceDatearray = [];
                

                $('body').removeClass('modal-open');
                $('.modal-backdrop').hide();

                var query = $("[id*=hdnqueryselected]").val();
                var dataToPost = '{Selected:"' + query + '"}'

                if (query != "") {

                    $.blockUI({
                        message: '<img src="Images/loader123.gif" />',
                        css: {
                            border: 'none',
                            backgroundColor: 'transparent'
                        }
                    });

                    //to disaply the calendar div and hide the List view
                    $("#ContentPlaceHolder1_divlegendshow").show();
                    $("#ContentPlaceHolder1_divbuttonsShow").show();
                    $("#ContentPlaceHolder1_GridView1").hide();
                    $("#ContentPlaceHolder1_divWholeCalendars").show();

                    if (DefaultCalenderdisplayView == "month") {
                        $.ajax({
                            type: 'POST',
                            url: 'Service.aspx/GetCalendarDateDetails',
                            data: dataToPost,
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'JSON',
                            success: function (data) {

                                if ((data.d == null) || (data.d == '')) {
                                    $("#ContentPlaceHolder1_divlegendshow").hide();
                                    $("#ContentPlaceHolder1_divbuttonsShow").hide();
                                    alert('No Events');
                                    $.unblockUI();
                                }
                                else {

                                    var result = JSON.parse(data.d);
                                    if (result && $.isArray(result)) {

                                        var num = 0; // Varibale to set the ServiceDate for the calendar
                                        var calDisplayFormat = document.getElementById("<%= hdnCalDisplayFormat.ClientID()%>").value;

                                        var displayDate = "";

                                        displayDate = calendarDisplay.split("/");
                                        startdate = new Date(displayDate[2], displayDate[1], displayDate[0]);
                                        startdate.setMonth(startdate.getMonth() - 1);


                                        for (var i = 0; i < result.length; i++) {


                                            var element = {};

                                            element.start = new Date(result[i].ServiceDateYear, result[i].ServiceDateMonth - 1, result[i].ServiceDateDay);
                                            element.end = new Date(result[i].ServiceDateYear, result[i].ServiceDateMonth - 1, result[i].ServiceDateDay);

                                            element.id = 'idEventDrag_' + i;
                                            element.title = '';

                                            element.ServiceDateDay = result[i].ServiceDateDay;
                                            element.ServiceDateMonth = result[i].ServiceDateMonth;
                                            element.ServiceDateYear = result[i].ServiceDateYear;
                                            element.EventCount = result[i].EventCount;
                                            element.statusSummary = result[i].statusSummary;
                                            element.TeamId = result[i].TeamId;
                                            element.RemoveTeamIDSpecialCharacter = result[i].RemovedTeamIdSpecialCharacter;
                                            eventText.push(element);
                                            var Teamcolor = getRandomColor();

                                            //Form the unique list of team id start
                                            if (teamIDList.length > 0) {
                                                var Obj = [];
                                                Obj = teamIDList.filter(function (obj) {
                                                    return (obj.TeamId === result[i].TeamId);
                                                });
                                            }
                                            if ((teamIDList.length <= 0) || (Obj.length <= 0)) {
                                                var dataToPush1 = {
                                                    TeamId: result[i].TeamId,
                                                    RemoveTeamIDSpecialCharacter: result[i].RemovedTeamIdSpecialCharacter,
                                                    TeamColor: Teamcolor
                                                }
                                                teamIDList.push(dataToPush1);

                                            }

                                            //Form the unique list of team id end
                                        }


                                        //To form div for calendars based on the number of teams
                                        ///////////////////////////////////////////////////
                                        var calendarDiv = "";
                                        var TeamIDdisplay = "";
                                        var j = 0;

                                        calendarDiv += "<div class='calendarcontainer Flipped '>";
                                        TeamIDdisplay = '<table  class="legend">';

                                        for (j = 0; j < teamIDList.length; j++) {
                                            if (teamIDList[j].TeamId != "") {
                                                var replacedteamid = "";
                                                replacedteamid = teamIDList[j].RemoveTeamIDSpecialCharacter.split(' ').join('_');

                                                var existingTeamIDobj = [];
                                                if (addedTeamIdcolors.length > 0) {
                                                    existingTeamIDobj = addedTeamIdcolors.filter(function (obj) {
                                                        return (obj.TeamId === teamIDList[j].TeamId);
                                                    });
                                                }

                                                if (existingTeamIDobj.length > 0) {
                                                    if (teamIDList.length >= 2) {
                                                        if (j == (teamIDList.length - 1)) {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' style='margin-right: 360px;' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].Teamcolor + ";'>" + teamIDList[j].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                        }
                                                        else {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].Teamcolor + ";'>" + teamIDList[j].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                        }
                                                    }
                                                    else {
                                                        if (j == (teamIDList.length - 1)) {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "'  style='margin-right: 360px;' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].Teamcolor + ";'>" + teamIDList[j].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                        }
                                                        else {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].Teamcolor + ";'>" + teamIDList[j].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                        }
                                                    }
                                                    TeamIDdisplay += '<tr><td style="padding-bottom: 5px; text-align: left;"><b><span style="background-color: ' + existingTeamIDobj[0].Teamcolor + ';"></span>' + teamIDList[j].TeamId.trim() + '</b></td></tr>';
                                                }
                                                else {
                                                    var existingTeamIDobj = [];
                                                    if (addedLegends.length > 0) {
                                                        existingTeamIDobj = addedLegends.filter(function (obj) {
                                                            return (obj.TeamId === teamIDList[j].TeamId);
                                                        });
                                                    }
                                                    if (existingTeamIDobj.length <= 0) {
                                                        if (teamIDList.length >= 2) {
                                                            if (j == (teamIDList.length - 1)) {
                                                                calendarDiv += "<div id='divCalendarBox_" + j + "' style='margin-right: 360px;' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + teamIDList[j].TeamColor + ";'>" + teamIDList[j].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                            }
                                                            else {
                                                                calendarDiv += "<div id='divCalendarBox_" + j + "' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + teamIDList[j].TeamColor + ";'>" + teamIDList[j].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                            }
                                                        }
                                                        else {
                                                            if (j == (teamIDList.length - 1)) {
                                                                calendarDiv += "<div id='divCalendarBox_" + j + "'  style='margin-right: 360px;' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + teamIDList[j].TeamColor + ";'>" + teamIDList[j].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                            }
                                                            else {
                                                                calendarDiv += "<div id='divCalendarBox_" + j + "' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + teamIDList[j].TeamColor + ";'>" + teamIDList[j].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                            }
                                                        }

                                                        TeamIDdisplay += '<tr><td style="padding-bottom: 5px; text-align: left;"><b><span style="background-color: ' + teamIDList[j].TeamColor + ';"></span>' + teamIDList[j].TeamId.trim() + '</b></td></tr>';
                                                        var dataToPush = { TeamId: teamIDList[j].TeamId, TeamColor: teamIDList[j].TeamColor }
                                                        addedLegends.push(dataToPush);
                                                    }
                                                    else {
                                                        if (teamIDList.length >= 2) {
                                                            if (j == (teamIDList.length - 1)) {
                                                                calendarDiv += "<div id='divCalendarBox_" + j + "' style='margin-right: 360px;' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].TeamColor + ";'>" + existingTeamIDobj[0].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                            }
                                                            else {
                                                                calendarDiv += "<div id='divCalendarBox_" + j + "' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].TeamColor + ";'>" + existingTeamIDobj[0].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                            }
                                                        }
                                                        else {
                                                            if (j == (teamIDList.length - 1)) {
                                                                calendarDiv += "<div id='divCalendarBox_" + j + "'  style='margin-right: 360px;' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].TeamColor + ";'>" + existingTeamIDobj[0].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                            }
                                                            else {
                                                                calendarDiv += "<div id='divCalendarBox_" + j + "' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].TeamColor + ";'>" + existingTeamIDobj[0].TeamId + "</div><div id='divCalendar_" + (replacedteamid) + "'></div></div>";
                                                            }
                                                        }
                                                        TeamIDdisplay += '<tr><td style="padding-bottom: 5px; text-align: left;"><b><span style="background-color: ' + existingTeamIDobj[0].TeamColor + ';"></span>' + existingTeamIDobj[0].TeamId.trim() + '</b></td></tr>';
                                                    }
                                                }
                                            }
                                        }

                                        calendarDiv += "</div>";
                                        TeamIDdisplay += '</table>';

                                        $('#<%= divlegendid.ClientID%>').html('');
                                        $('#<%= divlegendid.ClientID%>').html(TeamIDdisplay);

                                        ///////////////////////////////////////////////////
                                        //To place the divs for calendars based on the number of teams in the main div.
                                        ///////////////////////////////////////////////////
                                        $("#ContentPlaceHolder1_divWholeCalendars").html(calendarDiv);
                                        var teamName = "";

                                        //loop through the team list and render the corresponding calendars.
                                        for (var j = 0; j < teamIDList.length; j++) {
                                            $(function () {

                                                var teamId = teamIDList[j].TeamId;
                                                teamName = teamIDList[j].RemoveTeamIDSpecialCharacter.split(' ').join('_');

                                                $('#divCalendar_' + (teamName)).fullCalendar({
                                                    contentHeight: 800,
                                                    //defaultView: Caldisplay,
                                                    defaultView: DefaultCalenderdisplayView,
                                                    //aspectRatio: 1,
                                                    header: {
                                                        left: '',
                                                        center: 'prevYear prev title next nextYear',
                                                        right: ''
                                                       // right: 'agendaDay,agendaWeek,month'
                                                    },
                                                    editable: false, //To make the Calendar editable for drag and drop
                                                    <%--eventDrop: function (event, info, revertFunc) {

                                                    //To check the event in series when drag and drop start
                                                    /* If the event is in series ask for "This appointment belongs to a series, Do you wish to move this appointment only?"
                                                    if yes then drag and drop the event to new date.
                                                    else no drag revert the drag*/
                                                    ////////////////////////////////////////////
                                                    var dataPost = "";
                                                    dataPost = '{ContractNo:"' + event.contractno + '", Status:"' + event.Status + '", TeamId:"' + event.TeamId + '"}';
                                                    var draggedtodate = moment(event.start._i).format("YYYY-MM-DD");

                                                    //var datearray = event.start._i;
                                                    //var draggedtodate4 = new Date(datearray[0], datearray[1], datearray[2], datearray[3], datearray[4], 0);

                                                    $.ajax({
                                                        type: 'POST',
                                                        url: 'Service.aspx/GetSeriesofEvents',
                                                        data: dataPost,
                                                        contentType: 'application/json; charset=utf-8',
                                                        dataType: 'JSON',
                                                        success: function (msg) {

                                                            if ((msg.d == null) || (msg.d == '')) {
                                                                if (event.Status == "P") {
                                                                    //$('#lblSeriesAlertTitle').text("");
                                                                    //$('#SeriesAlertContent').text("");
                                                                    //$('#lblSeriesAlertTitle').text("Drag Recurring Item");
                                                                    //$('#SeriesAlertContent').text("The Status of this event is Closed. You Can't change this event");
                                                                    //$('#ContentPlaceHolder1_btnSeriesCancel').show();
                                                                    //$('#btnYes').hide();
                                                                    //$('#btnNo').hide();
                                                                    //$('#ContentPlaceHolder1_btnJustThisOne').hide();
                                                                    //$('#btnEntireSeries').hide();

                                                                    //$find('mdlPopSeriesAlert').show();
                                                                    alert("The Status of this event is Closed. You Can't change this event");
                                                                    revertFunc();
                                                                }
                                                                else {
                                                                    DraggingEvent(draggedtodate, event.RcNo, event.TeamId,false);
                                                                }
                                                            }
                                                            else {
                                                                var result = JSON.parse(msg.d);

                                                                if (result.length > 1) {
                                                                    if (result && $.isArray(result)) {


                                                                        var seriesDragAlert = '<div class="col-md-1"></div><div class="col-md-2"><input type="submit" id="btnYes" class="btn btn-primary" style="text-align:center;" value="Yes" onclick="DraggingEvent(\'' + draggedtodate + '\',' + event.RcNo + ',\'' + teamId + '\',true);return false;"></div>';
                                                                        seriesDragAlert += '<div class="col-md-2"><input type="submit" id="btnNo" class="btn btn-primary" style="text-align:center;" value="No" onclick="' + revertFunc() + ';' + 'onClickbtnNo();return false;"></div>';

                                                                        $('#<%= SeriesDragAlert.ClientID%>').html('');
                                                                        $('#<%= SeriesDragAlert.ClientID%>').html(seriesDragAlert);
                                                                    }

                                                                    $('#lblSeriesAlertTitle').text("");
                                                                    $('#SeriesAlertContent').text("");

                                                                    $('#btnYes').show();
                                                                    $('#btnNo').show();
                                                                    $('#ContentPlaceHolder1_btnJustThisOne').hide();
                                                                    $('#btnEntireSeries').hide();
                                                                    $('#ContentPlaceHolder1_btnSeriesCancel').hide();

                                                                    $('#lblSeriesAlertTitle').text("Drag Recurring Item");
                                                                    $('#SeriesAlertContent').text("This appointment belongs to a series, Do you wish to move this appointment only?");

                                                                    $('#<%= SeriesDragAlert.ClientID %>').show();
                                                                    $('#<%= SeriesOpenAlert.ClientID %>').hide();


                                                                    $find('mdlPopSeriesAlert').show();
                                                                }
                                                                else {
                                                                    DraggingEvent(draggedtodate, event.RcNo, event.TeamId,false);
                                                                }

                                                            }

                                                            //To check the event in series when drag and drop end
                                                            ////////////////////////////////////////////////
                                                        },
                                                        error: function (xhr, textStatus, errorThrown) {
                                                            console.log(xhr, textStatus, errorThrown);
                                                        }
                                                    });
                                                },--%>

                                                    //dayRender: function (date,cell) {

                                                    //    var sdisplayDate = (date._d).format("dd/mm/yyyy").split("/");

                                                    //    var startdate1 = new Date(sdisplayDate[2], sdisplayDate[1], sdisplayDate[0]);
                                                    //    startdate1.setMonth(startdate1.getMonth() - 1);
                                                    //    startdate1.setFullYear(startdate1.getFullYear() - 2);

                                                    //    if (startdate1.getDate() === startdate.getDate()) {
                                                    //        cell.css("background-color", "grey");
                                                    //    }

                                                    //},
                                                    eventAfterRender: function (event, element, view) {
                                                    },
                                                    eventRender: function (event, element, info) {

                                                        var existingTeamID = [];
                                                        var existingTeam = [];

                                                        element.css('border-color', 'white');
                                                        element.css('white-space', 'normal');


                                                        //To add the unique colors in "addedTeamIdcolors" array for legend based on the team id start
                                                        /////////////////////////////////////
                                                        if (addedTeamIdcolors.length > 0) {
                                                            var existingTeamID = addedTeamIdcolors.filter(function (obj) {
                                                                //return (obj.TeamId === event.TeamId && obj.ServiceName === event.ServiceName && obj.serviceBy === event.serviceBy && obj.SchServiceTime === event.SchServiceTime && obj.SchServiceTimeOut === event.SchServiceTimeOut && obj.Status === event.Status);
                                                                return (obj.TeamId === event.TeamId && obj.EventCount === event.EventCount && obj.ServiceDate === new Date(event.ServiceDateYear, event.ServiceDateMonth - 1, event.ServiceDateDay));
                                                            });
                                                        }

                                                        if (existingTeamID.length <= 0) {
                                                            var existingTeamColor = [];
                                                            if (addedLegends.length > 0) {
                                                                existingTeamColor = addedLegends.filter(function (obj) {
                                                                    return (obj.TeamId === event.TeamId);
                                                                });
                                                            }
                                                            else {
                                                                existingTeamColor = teamIDList.filter(function (obj) {
                                                                    return (obj.TeamId === event.TeamId);
                                                                });
                                                            }

                                                            var dataToPush = {
                                                                ServiceDate: new Date(event.ServiceDateYear, event.ServiceDateMonth - 1, event.ServiceDateDay),
                                                                TeamId: event.TeamId,
                                                                EventCount: event.EventCount,
                                                                statusSummary: event.statusSummary,
                                                                Teamcolor: existingTeamColor[0].TeamColor
                                                            }
                                                            addedTeamIdcolors.push(dataToPush);

                                                           

                                                            element.prepend("<b style='font-size: 12px !important;'>" + event.EventCount + " appointment(s)</b>" + "<br/>" + event.statusSummary);
                                                            element.css('background-color', existingTeamColor[0].TeamColor);
                                                        }
                                                        else {
                                                            
                                                            element.prepend("<b style='font-size: 12px !important;'>" + existingTeamID[0].EventCount + " appointment(s)</b>" + "<br/>" + existingTeamID[0].statusSummary);
                                                            element.css('background-color', existingTeamID[0].Teamcolor);
                                                        }

                                                        //element.prepend("<b style='font-size: 12px !important;'>" + event.EventCount + "appointment(s)</b>");
                                                        //To add the unique colors in "addedTeamIdcolors" array for legend based on the team id  end
                                                        /////////////////////////////////////

                                                        element.css('color', '#000000');


                                                        //Popover details start
                                                        var popTemplate = [
                                                                           '<div class="popover recorddetails" style="max-width:200px;font-family:Calibri;">',
                                                                           '<div class="arrow"></div>',
                                                                           '<div class="popover-header">',
                                                                           '<button onclick="$(this).closest(\'div.popover\').popover(\'hide\');" type="button" class="close" aria-hidden="true">&times;</button>',
                                                                           '<h3 class="popover-title"></h3>',
                                                                           '</div>',
                                                                           '<div class="popover-content" style="max-height:450px;overflow-x: auto;"></div>',
                                                                           '</div>'].join('');


                                                        var datatoPost = "";
                                                        var calanderTeamID = teamId;
                                                        datatoPost = '{ServiceDate:"' + event.ServiceDateDay + '",TeamId:"' + calanderTeamID + '", ServiceMonth:"' + event.ServiceDateMonth + '", ServiceYear:"' + event.ServiceDateYear + '"}';

                                                        var month_int = startdate.getMonth();

                                                        var monthName = monthNames(month_int);
                                                        var servicedate1 = new Date(event.ServiceDateYear, event.ServiceDateMonth - 1, event.ServiceDateDay);
                                                        var date_int = servicedate1.getDay();
                                                        var dateClick = DayNames(date_int);

                                                        $.ajax({
                                                            type: 'POST',
                                                            url: 'Service.aspx/GetEventsdetails',
                                                            data: datatoPost,
                                                            contentType: 'application/json; charset=utf-8',
                                                            dataType: 'JSON',
                                                            success: function (msg) {
                                                                if ((msg.d == null) || (msg.d == '')) { }
                                                                else {
                                                                    var result1 = JSON.parse(msg.d);
                                                                    var EventDetails = "<span><b>" + monthName + " " + event.ServiceDateDay + " (" + dateClick + ") </b></span><br /><br /><div class='row'><div class='col-md-12'>";
                                                                    for (var i = 0 ; i < result1.length; i++) {
                                                                        var ServiceDate = result1[i].ServiceDateDay + "/" + result1[i].ServiceDateMonth + "/" + result1[i].ServiceDateYear;

                                                                        if (result1[i].SchServiceTime != "" && result1[i].SchServiceTimeOut != "") {
                                                                            EventDetails += "<span style='padding-bottom:5px;'>" + result1[i].SchServiceTime + " to " + result1[i].SchServiceTimeOut + " (" + result1[i].TimeDifference + " mins)</span><br /></div></div><div class='row'><div class='col-md-12'><div class='col-md-2'>";

                                                                            //For Completed/Posted service records, the Change Status button should not be visible..
                                                                            if (result1[i].Status1 == "O") {
                                                                                EventDetails += "<input type='submit' id='btnEditAppointment' class='btn btn-primary' style='text-align:center;' value='Edit' onclick='return onClickEdit(\"" + result1[i].ContractNo + "\",\"" + result1[i].Status1 + "\",\"" + result1[i].TeamId + "\",\"" + result1[i].RcNo + "\");return false; '>";
                                                                                EventDetails += "<input type='submit' id='btnChangeStatus' class='btn btn-primary' style='text-align:center;' value='Change Status' onclick='return onClickChangeStatus(\"" + result1[i].LockSt + "\"," + result1[i].RcNo + ",\"" + result1[i].Status1 + "\",\"" + result1[i].RecordNo + "\"); return true;' />";
                                                                            }
                                                                        }
                                                                        else {
                                                                            EventDetails += "</div></div><div class='row'><div class='col-md-12'><div class='col-md-2'>";
                                                                            //For Completed/Posted service records, the Change Status button should not be visible..
                                                                            if (result1[i].Status1 == "O") {
                                                                                var contNo = result1[i].ContractNo;
                                                                                EventDetails += "<input type='submit' id='btnEditAppointment' class='btn btn-primary' style='text-align:center;' value='Edit' onclick='return onClickEdit(\"" + result1[i].ContractNo + "\",\"" + result1[i].Status1 + "\",\"" + result1[i].TeamId + "\",\"" + result1[i].RcNo + "\");return false; '>";
                                                                                EventDetails += "<input type='submit' id='btnChangeStatus' class='btn btn-primary' style='text-align:center;' value='Change Status' onclick='return onClickChangeStatus(\"" + result1[i].LockSt + "\"," + result1[i].RcNo + ",\"" + result1[i].Status1 + "\",\"" + result1[i].RecordNo + "\"); return true;' />";
                                                                            }
                                                                        }

                                                                        //Make the label "Status: Completed/Posted" color green and bold
                                                                        if (result1[i].Status1 == "O") {
                                                                            EventDetails += "</div><div class='col-md-10'><span style='padding-bottom:2px;'>Status: " + result1[i].Status + "</span><br />" + "<span style='padding-bottom:2px;'>Record No: " + result1[i].RecordNo + "</span><br />" + "<span style='padding-bottom:2px;'>Service By: " + result1[i].ServiceBy + "</span> <br />" + "<span style='padding-bottom:2px;'>ServiceDate: " + ServiceDate + " </span><br />" + "<span style='padding-bottom:2px;'>Contract No: " + result1[i].ContractNo + "</span> <br />" + "<span style='padding-bottom:2px;'>Customer: " + result1[i].ServiceName + "</span><br />" + "<span style='padding-bottom:2px;'>Address: " + result1[i].Address1 + "</span><br />" + "<span style='padding-bottom:2px;'>Service Description: " + result1[i].ServiceDescription + "</span><br />----------------------------------------------------<br /></div></div></div>";
                                                                        }
                                                                        else if (result1[i].Status1 == "P") {
                                                                            EventDetails += "</div><div class='col-md-10'><span style='padding-bottom:2px;color:green;font-weight: bold;'>Status: " + result1[i].Status + "</span><br />" + "<span style='padding-bottom:2px;'>Record No: " + result1[i].RecordNo + "</span><br />" + "<span style='padding-bottom:2px;'>Service By: " + result1[i].ServiceBy + "</span> <br />" + "<span style='padding-bottom:2px;'>ServiceDate: " + ServiceDate + " </span><br />" + "<span style='padding-bottom:2px;'>Contract No: " + result1[i].ContractNo + "</span> <br />" + "<span style='padding-bottom:2px;'>Customer: " + result1[i].ServiceName + "</span><br />" + "<span style='padding-bottom:2px;'>Address: " + result1[i].Address1 + "</span><br />" + "<span style='padding-bottom:2px;'>Service Description: " + result1[i].ServiceDescription + "</span><br />----------------------------------------------------<br /></div></div></div>";
                                                                        } else {
                                                                            EventDetails += "</div><div class='col-md-10'><span style='padding-bottom:2px;'>Status: " + result1[i].Status + "</span><br />" + "<span style='padding-bottom:2px;'>Record No: " + result1[i].RecordNo + "</span><br />" + "<span style='padding-bottom:2px;'>Service By: " + result1[i].ServiceBy + "</span> <br />" + "<span style='padding-bottom:2px;'>ServiceDate: " + ServiceDate + " </span><br />" + "<span style='padding-bottom:2px;'>Contract No: " + result1[i].ContractNo + "</span> <br />" + "<span style='padding-bottom:2px;'>Customer: " + result1[i].ServiceName + "</span><br />" + "<span style='padding-bottom:2px;'>Address: " + result1[i].Address1 + "</span><br />" + "<span style='padding-bottom:2px;'>Service Description: " + result1[i].ServiceDescription + "</span><br />----------------------------------------------------<br /></div></div></div>";
                                                                        }
                                                                    }

                                                                    element.popover({
                                                                        title: event.title,
                                                                        content: EventDetails,
                                                                        template: popTemplate,
                                                                        container: 'body',
                                                                        html: 'true',
                                                                        trigger: 'click'
                                                                    });
                                                                    catchElement = element;
                                                                }

                                                            },
                                                            error: function (xhr, textStatus, errorThrown) {
                                                                console.log(xhr, textStatus, errorThrown);
                                                            }
                                                        });

                                                        //Popover details end
                                                    },

                                                });
                                            })

                                            // To find the current teamid obj from list to load
                                            TeamIdObj = eventText.filter(function (obj) {
                                                return (obj.TeamId === teamIDList[j].TeamId);
                                            });

                                            $('#divCalendar_' + (teamName)).fullCalendar('addEventSource', TeamIdObj); // To Load the calendar based on the TeamId
                                            $('#divCalendar_' + (teamName)).fullCalendar('refetchEvents');
                                            $('#divCalendar_' + (teamName)).fullCalendar('gotoDate', startdate); // To Load the calendar based on the TeamId and ServiceDate
                                        }
                                    }
                                    $.unblockUI();
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                $.unblockUI();
                                console.log(xhr, textStatus, errorThrown);
                            }
                        });
                    }
                    else {
                        //day view
                        // day or week view 
                        $.ajax({
                            type: 'POST',
                            url: 'Service.aspx/GetCalendarViewForDayAndWeekDetails',
                            data: dataToPost,
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'JSON',
                            success: function (data) {

                                if ((data.d == null) || (data.d == '')) {
                                    $("#ContentPlaceHolder1_divlegendshow").hide();
                                    $("#ContentPlaceHolder1_divbuttonsShow").hide();
                                    alert('No Events');
                                    $.unblockUI();
                                }
                                else {

                                    var result = JSON.parse(data.d);
                                    if (result && $.isArray(result)) {


                                        var num = 0; // Varibale to set the ServiceDate for the calendar
                                        var calDisplayFormat = document.getElementById("<%= hdnCalDisplayFormat.ClientID()%>").value;

                                    var displayDate = "";

                                    //if (calDisplayFormat!="") {
                                    //    displayDate = calendarDisplay.split("/");
                                    //    startdate = new Date(displayDate[2], displayDate[1], displayDate[0]);
                                    //    startdate.setMonth(startdate.getMonth() - 1);
                                    //}
                                    //else {
                                    displayDate = calendarDisplay.split("/");
                                    startdate = new Date(displayDate[2], displayDate[1], displayDate[0]);
                                    startdate.setMonth(startdate.getMonth() - 1);
                                    //}


                                    for (var i = 0; i < result.length; i++) {


                                        var element = {};

                                        element.start = new Date(result[i].ServiceDateYear, result[i].ServiceDateMonth - 1, result[i].ServiceDateDay);
                                        element.end = new Date(result[i].ServiceDateYear, result[i].ServiceDateMonth - 1, result[i].ServiceDateDay);

                                        if (result[i].SchServiceTime != "") {

                                            //if (result[i].SchServiceTime == "09:00")
                                            //{
                                            //    console.log("service test");
                                            //    result[i].SchServiceTime = "10:00";
                                            //    result[i].SchServiceTimeOut = "11:30";
                                            //}

                                            var servicetimeIn = result[i].SchServiceTime.trim().split(':');
                                            element.start = new Date(result[i].ServiceDateYear, result[i].ServiceDateMonth - 1, result[i].ServiceDateDay, servicetimeIn[0], servicetimeIn[1], 0);
                                        }

                                        if (result[i].SchServiceTimeOut != "") {

                                            var servicetimeOut = result[i].SchServiceTimeOut.trim().split(':');
                                            element.end = new Date(result[i].ServiceDateYear, result[i].ServiceDateMonth - 1, result[i].ServiceDateDay, servicetimeOut[0], servicetimeOut[1], 0);
                                        }


                                        element.id = 'idEventDrag_' + i;
                                        element.title = '';

                                        element.ServiceDateDay = result[i].ServiceDateDay;
                                        element.ServiceDateMonth = result[i].ServiceDateMonth;
                                        element.ServiceDateYear = result[i].ServiceDateYear;
                                        element.Address = result[i].Address;
                                        element.RcNo = result[i].RcNo;
                                        element.ServiceBy = result[i].ServiceBy;
                                        //element.RecordNoList = result[i].RecordNoList;
                                        element.SchServiceTime = result[i].SchServiceTime;
                                        element.SchServiceTimeOut = result[i].SchServiceTimeOut;
                                        element.ServiceName = result[i].ServiceName.trim();
                                        element.Status = result[i].Status;
                                        element.TeamId = result[i].TeamId;
                                        element.RemoveTeamIDSpecialCharacter = result[i].RemovedTeamIdSpecialCharacter;
                                        element.contractno = result[i].ContractNo;
                                        element.appointmentsCount = result[i].appointmentsCount;
                                        eventText.push(element);

                                        var Teamcolor = getRandomColor();

                                        //Form the unique list of team id start
                                        if (teamIDList.length > 0) {
                                            var Obj = [];
                                            Obj = teamIDList.filter(function (obj) {
                                                return (obj.TeamId === result[i].TeamId);
                                            });
                                        }
                                        if ((teamIDList.length <= 0) || (Obj.length <= 0)) {
                                            var dataToPush1 = {
                                                TeamId: result[i].TeamId,
                                                RemoveTeamIDSpecialCharacter: result[i].RemovedTeamIdSpecialCharacter,
                                                appointmentsCount: result[i].appointmentsCount,
                                                TeamColor: Teamcolor
                                            }
                                            teamIDList.push(dataToPush1);

                                        }

                                        //Form the unique list of team id end
                                    }

                                    console.log("eventText", eventText);

                                    //To form div for calendars based on the number of teams
                                    ///////////////////////////////////////////////////
                                    var calendarDiv = "";
                                    var TeamIDdisplay = "";
                                    var j = 0;

                                    calendarDiv += "<div class='calendarcontainer Flipped '>";
                                    TeamIDdisplay = '<table  class="legend">';

                                    for (j = 0; j < teamIDList.length; j++) {
                                        console.log("teamIDList appointmentsCount", teamIDList[j].appointmentsCount);

                                        if (teamIDList[j].TeamId != "") {
                                            var replacedteamid = "";
                                            replacedteamid = teamIDList[j].RemoveTeamIDSpecialCharacter.split(' ').join('_');

                                            var existingTeamIDobj = [];
                                            if (addedTeamIdcolors.length > 0) {
                                                existingTeamIDobj = addedTeamIdcolors.filter(function (obj) {
                                                    return (obj.TeamId === teamIDList[j].TeamId);
                                                });
                                            }

                                            if (existingTeamIDobj.length > 0) {
                                                console.log("test 111111");
                                                if (teamIDList.length >= 2) {
                                                    console.log("test 111111 1");
                                                    if (j == (teamIDList.length - 1)) {
                                                        calendarDiv += "<div id='divCalendarBox_" + j + "' style='margin-right: 360px;' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].Teamcolor + ";'>" + teamIDList[j].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div>";
                                                        calendarDiv += "<div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "' ></div></div>";
                                                    }
                                                    else {
                                                        calendarDiv += "<div id='divCalendarBox_" + j + "' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].Teamcolor + ";'>" + teamIDList[j].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                    }
                                                }
                                                else {
                                                    console.log("test 111111 2");
                                                    if (j == (teamIDList.length - 1)) {
                                                        console.log("test 111111 3");
                                                        calendarDiv += "<div id='divCalendarBox_" + j + "'  style='margin-right: 360px;' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].Teamcolor + ";'>" + teamIDList[j].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div>";
                                                        calendarDiv += "<div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                    }
                                                    else {
                                                        console.log("test 111111 4");
                                                        calendarDiv += "<div id='divCalendarBox_" + j + "' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].Teamcolor + ";'>" + teamIDList[j].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                    }
                                                }
                                                TeamIDdisplay += '<tr><td style="padding-bottom: 5px; text-align: left;"><b><span style="background-color: ' + existingTeamIDobj[0].Teamcolor + ';"></span>' + teamIDList[j].TeamId.trim() + '</b></td></tr>';
                                            }
                                            else {

                                                console.log("test 222222");
                                                var existingTeamIDobj = [];
                                                if (addedLegends.length > 0) {
                                                    existingTeamIDobj = addedLegends.filter(function (obj) {
                                                        return (obj.TeamId === teamIDList[j].TeamId);
                                                    });
                                                }
                                                if (existingTeamIDobj.length <= 0) {
                                                    console.log("test 3333333");

                                                    if (teamIDList.length >= 2) {
                                                        if (j == (teamIDList.length - 1)) {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' style='margin-right: 360px;' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + teamIDList[j].TeamColor + ";'>" + teamIDList[j].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                        }
                                                        else {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + teamIDList[j].TeamColor + ";'>" + teamIDList[j].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                        }
                                                    }
                                                    else {
                                                        if (j == (teamIDList.length - 1)) {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "'  style='margin-right: 360px;' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + teamIDList[j].TeamColor + ";'>" + teamIDList[j].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                        }
                                                        else {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + teamIDList[j].TeamColor + ";'>" + teamIDList[j].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                        }
                                                    }

                                                    TeamIDdisplay += '<tr><td style="padding-bottom: 5px; text-align: left;"><b><span style="background-color: ' + teamIDList[j].TeamColor + ';"></span>' + teamIDList[j].TeamId.trim() + '</b></td></tr>';
                                                    var dataToPush = {
                                                        TeamId: teamIDList[j].TeamId,
                                                        TeamColor: teamIDList[j].TeamColor,
                                                        appointmentsCount: teamIDList[j].appointmentsCount,
                                                    }
                                                    addedLegends.push(dataToPush);
                                                }
                                                else {
                                                    console.log("test 4444444");
                                                    if (teamIDList.length >= 2) {
                                                        if (j == (teamIDList.length - 1)) {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' style='margin-right: 360px;' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].TeamColor + ";'>" + existingTeamIDobj[0].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                        }
                                                        else {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' class='scrollbox'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].TeamColor + ";'>" + existingTeamIDobj[0].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                        }
                                                    }
                                                    else {
                                                        if (j == (teamIDList.length - 1)) {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "'  style='margin-right: 360px;' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].TeamColor + ";'>" + existingTeamIDobj[0].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                        }
                                                        else {
                                                            calendarDiv += "<div id='divCalendarBox_" + j + "' class='box2'><div style='color: black;font-size:30px;text-align:left;background-color: " + existingTeamIDobj[0].TeamColor + ";'>" + existingTeamIDobj[0].TeamId + "<label style='color: black;font-size: 18px;vertical-align: middle;font-weight: bold;' id='idlblAppointmentsCount_" + (replacedteamid) + "'> </label></div><div id='divCalendar_" + (replacedteamid) + "' data-idTeam='" + (replacedteamid) + "'></div></div>";
                                                        }
                                                    }
                                                    TeamIDdisplay += '<tr><td style="padding-bottom: 5px; text-align: left;"><b><span style="background-color: ' + existingTeamIDobj[0].TeamColor + ';"></span>' + existingTeamIDobj[0].TeamId.trim() + '</b></td></tr>';
                                                }
                                            }
                                        }
                                    }

                                    calendarDiv += "</div>";
                                    TeamIDdisplay += '</table>';

                                    $('#<%= divlegendid.ClientID%>').html('');
                                    $('#<%= divlegendid.ClientID%>').html(TeamIDdisplay);

                                    ///////////////////////////////////////////////////
                                    //To place the divs for calendars based on the number of teams in the main div.
                                    ///////////////////////////////////////////////////
                                    $("#ContentPlaceHolder1_divWholeCalendars").html(calendarDiv);
                                    var teamName = "";

                                    //loop through the team list and render the corresponding calendars.
                                    for (var j = 0; j < teamIDList.length; j++) {
                                        $(function () {

                                            var teamId = teamIDList[j].TeamId;
                                            teamName = teamIDList[j].RemoveTeamIDSpecialCharacter.split(' ').join('_');

                                            $('#divCalendar_' + (teamName)).fullCalendar({
                                                contentHeight: 800,
                                                //defaultView: Caldisplay,
                                                defaultView: DefaultCalenderdisplayView,
                                                scrollTime: "00:00:00",
                                                header: {
                                                    left: '',
                                                    center: 'prevYear prev title next nextYear',
                                                    right: ''
                                                    //right: 'agendaDay,agendaWeek,month'
                                                },
                                                editable: true, //To make the Calendar editable for drag and drop
                                                eventDrop: function (event, info, revertFunc) {

                                                    console.log("dragged event ", event);
                                                    console.log("dragged event info", info);

                                                    console.log("dragged event info _data",info._data);

                                                    //To check the event in series when drag and drop start
                                                    /* If the event is in series ask for "This appointment belongs to a series, Do you wish to move this appointment only?"
                                                    if yes then drag and drop the event to new date.
                                                    else no drag revert the drag*/
                                                    ////////////////////////////////////////////
                                                    var dataPost = "";
                                                    dataPost = '{ContractNo:"' + event.contractno + '", Status:"' + event.Status + '", TeamId:"' + event.TeamId + '"}';
                                                    var draggedtodate = moment(event.start._i).format("YYYY-MM-DD HH:mm");

                                                    //var datearray = event.start._i;
                                                    //var draggedtodate4 = new Date(datearray[0], datearray[1], datearray[2], datearray[3], datearray[4], 0);

                                                    $.ajax({
                                                        type: 'POST',
                                                        url: 'Service.aspx/GetSeriesofEvents',
                                                        data: dataPost,
                                                        contentType: 'application/json; charset=utf-8',
                                                        dataType: 'JSON',
                                                        success: function (msg) {

                                                            if ((msg.d == null) || (msg.d == '')) {
                                                                if (event.Status == "P") {
                                                                    //$('#lblSeriesAlertTitle').text("");
                                                                    //$('#SeriesAlertContent').text("");
                                                                    //$('#lblSeriesAlertTitle').text("Drag Recurring Item");
                                                                    //$('#SeriesAlertContent').text("The Status of this event is Closed. You Can't change this event");
                                                                    //$('#ContentPlaceHolder1_btnSeriesCancel').show();
                                                                    //$('#btnYes').hide();
                                                                    //$('#btnNo').hide();
                                                                    //$('#ContentPlaceHolder1_btnJustThisOne').hide();
                                                                    //$('#btnEntireSeries').hide();

                                                                    //$find('mdlPopSeriesAlert').show();
                                                                    alert("The Status of this event is Closed. You Can't change this event");
                                                                    revertFunc();
                                                                }
                                                                else {
                                                                    console.log("DraggingEvent 1 ");
                                                                    DraggingEvent(draggedtodate, event.RcNo, event.TeamId, true);
                                                                }
                                                            }
                                                            else {
                                                                var result = JSON.parse(msg.d);

                                                                if (result.length > 1) {
                                                                    if (result && $.isArray(result)) {


                                                                        var seriesDragAlert = '<div class="col-md-1"></div><div class="col-md-2"><input type="submit" id="btnYes" class="btn btn-primary" style="text-align:center;" value="Yes" onclick="DraggingEvent(\'' + draggedtodate + '\',' + event.RcNo + ',\'' + teamId + '\',true);return false;"></div>';
                                                                        seriesDragAlert += '<div class="col-md-2"><input type="submit" id="btnNo" class="btn btn-primary" style="text-align:center;" value="No" onclick="' + revertFunc() + ';' + 'onClickbtnNo();return false;"></div>';
                                                                        seriesDragAlert += "<input type='submit' id='btnEntireSeries' class='btn btn-primary' style='text-align:center;' value='The entire series' onclick='return onClickEntireSeries(\"" + event.contractno + "\");return false; '>";
                                                                        $('#<%= SeriesDragAlert.ClientID%>').html('');
                                                                        $('#<%= SeriesDragAlert.ClientID%>').html(seriesDragAlert);
                                                                    }

                                                                    $('#lblSeriesAlertTitle').text("");
                                                                    $('#SeriesAlertContent').text("");

                                                                    $('#btnYes').show();
                                                                    $('#btnNo').show();
                                                                    $('#ContentPlaceHolder1_btnJustThisOne').hide();
                                                                    $('#btnEntireSeries').show();
                                                                    $('#ContentPlaceHolder1_btnSeriesCancel').hide();

                                                                    $('#lblSeriesAlertTitle').text("Drag Recurring Item");
                                                                    $('#SeriesAlertContent').text("This appointment belongs to a series, Do you wish to move this appointment only?");

                                                                    $('#<%= SeriesDragAlert.ClientID %>').show();
                                                                    $('#<%= SeriesOpenAlert.ClientID %>').hide();


                                                                    $find('mdlPopSeriesAlert').show();
                                                                }
                                                                else {
                                                                    console.log("DraggingEvent 2 ");
                                                                    DraggingEvent(draggedtodate, event.RcNo, event.TeamId, true);
                                                                }

                                                            }

                                                            //To check the event in series when drag and drop end
                                                            ////////////////////////////////////////////////
                                                        },
                                                        error: function (xhr, textStatus, errorThrown) {
                                                            console.log(xhr, textStatus, errorThrown);
                                                        }
                                                    });
                                                },

                                                eventResize: function (event, delta, revertFunc, jsEvent, ui, view) {

                                                    if (event.Status == "P") {
                                                        alert("The Status of this event is Closed. You Can't change this event");
                                                        revertFunc();
                                                    }
                                                    else {

                                                        var draggedstartdate = moment(event.start._i).format("YYYY-MM-DD HH:mm");
                                                        var draggedenddate = moment(event.end._i).format("YYYY-MM-DD HH:mm");

                                                        console.log("eventResize draggedstartdate", draggedstartdate);
                                                        console.log("eventResize draggedenddate", draggedenddate);
                                                        console.log("eventResize delta", delta);

                                                        ResizeTimeEvent(draggedstartdate,draggedenddate, event.RcNo, event.TeamId, true);
                                                    }
                                                    //var draggedtodate = moment(event.start._i).format("YYYY-MM-DD hh:mm");
                                                    //DraggingEvent(draggedtodate, event.RcNo, event.TeamId, false);

                                                },
                                                eventRender: function (event, element, info) {

                                                    var existingTeamID = [];
                                                    var existingTeam = [];

                                                    element.css('border-color', 'white');
                                                    element.css('white-space', 'normal');


                                                    //To add the unique colors in "addedTeamIdcolors" array for legend based on the team id start
                                                    /////////////////////////////////////
                                                    if (addedTeamIdcolors.length > 0) {
                                                        var existingTeamID = addedTeamIdcolors.filter(function (obj) {
                                                            return (obj.TeamId === event.TeamId && obj.ServiceName === event.ServiceName && obj.serviceBy === event.serviceBy && obj.SchServiceTime === event.SchServiceTime && obj.SchServiceTimeOut === event.SchServiceTimeOut && obj.Status === event.Status);
                                                        });
                                                    }

                                                    var prependText = "";
                                                    if (existingTeamID.length <= 0) {
                                                        var existingTeamColor = [];
                                                        if (addedLegends.length > 0) {
                                                            existingTeamColor = addedLegends.filter(function (obj) {
                                                                return (obj.TeamId === event.TeamId);
                                                            });
                                                        }
                                                        else {
                                                            existingTeamColor = teamIDList.filter(function (obj) {
                                                                return (obj.TeamId === event.TeamId);
                                                            });
                                                        }

                                                        var dataToPush = {
                                                            ServiceName: event.ServiceName,
                                                            ServiceBy: event.ServiceBy,
                                                            SchServiceTime: event.SchServiceTime,
                                                            SchServiceTimeOut: event.SchServiceTimeOut,
                                                            Status: event.Status,
                                                            appointmentsCount: event.appointmentsCount,
                                                            TeamId: event.TeamId,
                                                            Teamcolor: existingTeamColor[0].TeamColor
                                                        }
                                                        addedTeamIdcolors.push(dataToPush);

                                                        //If the status is "P" then the check box in the event tile should be checked
                                                        if (event.Status == "P") {
                                                            element.prepend("<input id='chkServiceName_" + (event.ServiceName.split(' ').join('_')) + "' name='chkServiceName' type='checkbox' checked='checked' disabled='disabled' /><b style='font-size: 12px !important;'>" + event.ServiceName.substring(0, 20) + "</b>");
                                                        }
                                                        //If the status is "H" then the check box in the event tile should not be checked and should have a label "ON HOLD"
                                                        if (event.Status == "H") {
                                                            element.prepend("<input id='chkServiceName_" + (event.ServiceName.split(' ').join('_')) + "' name='chkServiceName' type='checkbox' disabled='disabled' /><b style='font-size: 12px !important;'>ON HOLD <br />" + event.ServiceName.substring(0, 20) + "</b>");
                                                        }
                                                        //If the status is "O" then the check box in the event tile should not be checked 
                                                        if (event.Status == "O") {
                                                            element.prepend("<input id='chkServiceName_" + (event.ServiceName.split(' ').join('_')) + "' name='chkServiceName' type='checkbox' disabled='disabled' /><b style='font-size: 12px !important;'>" + event.ServiceName.substring(0, 20) + "</b>");
                                                        }
                                                        if (event.Status == "C" || event.Status == "T" || event.Status == "V" || event.Status == "B") {
                                                            element.prepend("<strike><input id='chkServiceName_" + (event.ServiceName.split(' ').join('_')) + "' name='chkServiceName' type='checkbox' disabled='disabled' /><b style='font-size: 12px !important;'>" + event.ServiceName.substring(0, 20) + "</b></strike>");
                                                        }
                                                        element.css('background-color', existingTeamColor[0].TeamColor);

                                                        console.log("teamName 1 ", event);
                                                        var RemoveTeamID = event.RemoveTeamIDSpecialCharacter.split(' ').join('_');
                                                        console.log("RemoveTeamID", RemoveTeamID);
                                                        if (DefaultCalenderdisplayView == "agendaDay") {
                                                            $("#idlblAppointmentsCount_" + RemoveTeamID).text("\t\t - \t " + event.appointmentsCount + " Appointment/s");
                                                        }
                                                        else {
                                                            $("#idlblAppointmentsCount_" + RemoveTeamID).text(" ");
                                                        }

                                                    }
                                                    else {
                                                        //If the status is "P" then the check box in the event tile should be checked
                                                        if (existingTeamID[0].Status == "P") {
                                                            element.prepend("<input id='chkServiceName_" + (existingTeamID[0].ServiceName.split(' ').join('_')) + "' name='chkServiceName' type='checkbox' checked='checked' disabled='disabled'/><b style='font-size: 12px !important;'>" + existingTeamID[0].ServiceName.substring(0, 20) + "</b>");
                                                        }
                                                        //If the status is "H" then the check box in the event tile should not be checked and should have a label "ON HOLD"
                                                        if (existingTeamID[0].Status == "H") {
                                                            element.prepend("<input id='chkServiceName_" + (existingTeamID[0].ServiceName.split(' ').join('_')) + "' name='chkServiceName' type='checkbox' disabled='disabled'/><b style='font-size: 12px !important;'>ON HOLD <br /> " + existingTeamID[0].ServiceName.substring(0, 20) + "</b>");
                                                        }
                                                        //If the status is "O" then the check box in the event tile should not be checked 
                                                        if (existingTeamID[0].Status == "O") {
                                                            element.prepend("<input id='chkServiceName_" + (existingTeamID[0].ServiceName.split(' ').join('_')) + "' name='chkServiceName' type='checkbox' disabled='disabled'/><b style='font-size: 12px !important;'>" + existingTeamID[0].ServiceName.substring(0, 20) + "</b>");
                                                        }
                                                        if (existingTeamID[0].Status == "C" || existingTeamID[0].Status == "T" || existingTeamID[0].Status == "V" || existingTeamID[0].Status == "B") {
                                                            element.prepend("<strike><input id='chkServiceName_" + (existingTeamID[0].ServiceName.split(' ').join('_')) + "' name='chkServiceName' type='checkbox' disabled='disabled'/><b style='font-size: 12px !important;'>" + existingTeamID[0].ServiceName.substring(0, 20) + "</b></strike>");
                                                        }

                                                        console.log("teamName 2 ", event);
                                                        var RemoveTeamID = event.RemoveTeamIDSpecialCharacter.split(' ').join('_');
                                                        console.log("RemoveTeamID", RemoveTeamID);
                                                        if (DefaultCalenderdisplayView == "agendaDay") {
                                                            $("#idlblAppointmentsCount_" + RemoveTeamID).text("\t\t - \t" + existingTeamID[0].appointmentsCount + " Appointment/s");
                                                        }
                                                        else {
                                                            $("#idlblAppointmentsCount_" + RemoveTeamID).text(" ");
                                                        }

                                                        element.css('background-color', existingTeamID[0].Teamcolor);
                                                    }

                                                    //To add the unique colors in "addedTeamIdcolors" array for legend based on the team id  end
                                                    /////////////////////////////////////

                                                    element.css('color', '#000000');

                                               

                                                    //Popover details start
                                                    var popTemplate = [
                                                                       '<div class="popover recorddetails" style="max-width:200px;font-family:Calibri;">',
                                                                       '<div class="arrow"></div>',
                                                                       '<div class="popover-header">',
                                                                       '<button onclick="$(this).closest(\'div.popover\').popover(\'hide\');" type="button" class="close" aria-hidden="true">&times;</button>',
                                                                       '<h3 class="popover-title"></h3>',
                                                                       '</div>',
                                                                       '<div class="popover-content" style="max-height:450px;"></div>',
                                                                       '</div>'].join('');


                                                    var datatoPost = "";
                                                    var calanderTeamID = teamId;
                                                    //datatoPost = '{SchServiceDate:"' + event.SchServiceDateDay + '",TeamId:"' + calanderTeamID + '", ServiceName:"' + event.ServiceName + '", ServiceBy:"' + event.ServiceBy + '", SchServiceMonth:"' + event.SchServiceDateMonth + '", SchServiceYear:"' + event.SchServiceDateYear + '", RecordList:"(' + event.RecordNoList + ')"}';
                                                    datatoPost = '{ServiceDate:"' + event.ServiceDateDay + '",TeamId:"' + calanderTeamID + '", ServiceName:"' + event.ServiceName + '", ServiceBy:"' + event.ServiceBy + '", ServiceMonth:"' + event.ServiceDateMonth + '", ServiceYear:"' + event.ServiceDateYear + '" , RcNo:"' + event.RcNo + '"}';
                                                    $.ajax({
                                                        type: 'POST',
                                                        url: 'Service.aspx/GetEventsdetailsForDayAndWeekView',
                                                        data: datatoPost,
                                                        contentType: 'application/json; charset=utf-8',
                                                        dataType: 'JSON',
                                                        success: function (msg) {
                                                            if ((msg.d == null) || (msg.d == '')) { }
                                                            else {
                                                                var result1 = JSON.parse(msg.d);
                                                                var EventDetails = "";
                                                                for (var i = 0 ; i < result1.length; i++) {
                                                                    var ServiceDate = result1[i].ServiceDateDay + "/" + result1[i].ServiceDateMonth + "/" + result1[i].ServiceDateYear;
                                                                    if (EventDetails == "") {
                                                                        EventDetails = "<span style='padding-bottom:2px;'>Status: " + result1[i].Status + "</span><br />" + "<span style='padding-bottom:2px;'>Record No: " + result1[i].RecordNo + "</span><br />" + "<span style='padding-bottom:2px;'>Service By: " + result1[i].ServiceBy + "</span><br />" + "<span style='padding-bottom:2px;'>ServiceDate: " + ServiceDate + "</span> <br />" + "<span style='padding-bottom:2px;'>Sch.TimeIn: " + result1[i].SchServiceTime + "</span> <br />" + "<span style='padding-bottom:2px;'>Sch.TimeOut: " + result1[i].SchServiceTimeOut + "</span><br />" + "<span style='padding-bottom:2px;'>Customer: " + result1[i].ServiceName + "</span><br />" + "<span style='padding-bottom:2px;'>Address: " + result1[i].Address1 + "</span>";
                                                                    }
                                                                    else {
                                                                        EventDetails += "<br />------------------------<br /><span style='padding-bottom:2px;'>Status: " + result1[i].Status + "</span><br /><span style='padding-bottom:2px;'>Record No: " + result1[i].RecordNo + "</span><br /><span style='padding-bottom:2px;'>Service By: " + result1[i].ServiceBy + "</span> <br />" + "<span style='padding-bottom:2px;'>ServiceDate: " + ServiceDate + " </span><br />" + "<span style='padding-bottom:2px; '>Sch.TimeIn:" + result1[i].SchServiceTime + " </span><br />" + "<span style='padding-bottom:2px;'>Sch.TimeOut: " + result1[i].SchServiceTimeOut + "</span><br />" + "<span style='padding-bottom:2px;'>Customer: " + result1[i].ServiceName + "</span><br />" + "<span style='padding-bottom:2px;'>Address: " + result1[i].Address1 + "</span>";
                                                                    }
                                                                }

                                                                element.popover({
                                                                    title: event.title,
                                                                    content: EventDetails,
                                                                    template: popTemplate,
                                                                    container: 'body',
                                                                    html: 'true',
                                                                    trigger: 'hover'
                                                                });
                                                                catchElement = element;
                                                            }

                                                        },
                                                        error: function (xhr, textStatus, errorThrown) {
                                                            console.log(xhr, textStatus, errorThrown);
                                                        }
                                                    });

                                                    //Popover details end


                                                    element.off('dblclick').on('dblclick', function () {
                                                        console.log("dblclick event", event);
                                                        if (event.Status == "O") {
                                                            console.log("dblclick event open");

                                                            var hdnRcNo = "<%= hdnRcNo.ClientID%>";
                                                            document.getElementById(hdnRcNo).value = event.RcNo;

                                                            var hiddenrandomnumberchange = "<%= hiddenrandomnumber.ClientID()%>";
                                                            document.getElementById(hiddenrandomnumberchange).value = Math.random();
                                                            __doPostBack(hiddenrandomnumberchange, "");


                                                            //onClickEdit(event.contractno, event.Status, event.TeamId, event.RcNo);
                                                        }
                                                    });



                                                 <%--   //To check the event is in series and double click 
                                                    //If the event is in series ask for "This is one appointment in a series. What do you want to open?"
                                                    //if "Just this one" then open the mdlPopupSchDate.
                                                    //else SmartMassChange.aspx page will be loaded
                                                    var dataSeriesPost = "";
                                                    dataSeriesPost = '{ContractNo:"' + event.contractno + '", Status:"' + event.Status + '", TeamId:"' + event.TeamId + '"}';
                                                    $.ajax({
                                                        type: 'POST',
                                                        url: 'Service.aspx/GetSeriesofEvents',
                                                        data: dataSeriesPost,
                                                        contentType: 'application/json; charset=utf-8',
                                                        dataType: 'JSON',
                                                        success: function (msg) {
                                                            if ((msg.d == null) || (msg.d == '')) { }
                                                            else {
                                                                var result = JSON.parse(msg.d);
                                                                if (result.length > 1) {
                                                                    if (result && $.isArray(result)) {
                                                                        element.off('dblclick').on('dblclick', function () {

                                                                            var hdnRcNo = "<%= hdnRcNo.ClientID%>";
                                                                            document.getElementById(hdnRcNo).value = event.RcNo;
                                                                            //var seriesOpenAlert = "<div class='col-md-1'></div><div class='col-md-3'><input type='submit' id='btnJustThisOne' class='btn btn-primary' style='text-align:center;' value='Just This One' onclick='return onClickJustThisOne(\"" + event.id + "\"," +  event.RcNo + "); return false;'></div> ";
                                                                            var seriesOpenAlert = '<asp:Button ID="btnJustThisOne" style="margin-right: 30px;" class="btn btn-primary" runat="server" Text="Just This One"  OnClick ="btnJustThisOne_Click"/>';
                                                                            seriesOpenAlert += "<input type='submit' id='btnEntireSeries' class='btn btn-primary' style='text-align:center;' value='The entire series' onclick='return onClickEntireSeries(\"" + event.contractno + "\");return false; '>";


                                                                            $('#<%= SeriesOpenAlert.ClientID%>').html('');
                                                                            $('#<%= SeriesOpenAlert.ClientID%>').html(seriesOpenAlert);

                                                                            $('#lblSeriesAlertTitle').text("");
                                                                            $('#SeriesAlertContent').text("");

                                                                            $('#btnYes').hide();
                                                                            $('#btnNo').hide();
                                                                            $('#ContentPlaceHolder1_btnJustThisOne').show();
                                                                            $('#btnEntireSeries').show();
                                                                            $('#ContentPlaceHolder1_btnSeriesCancel').show();

                                                                            $('#lblSeriesAlertTitle').text("Open Recurring item");
                                                                            $('#SeriesAlertContent').text("This is one appointment in a series. What do you want to open?");
                                                                            $('#<%= SeriesOpenAlert.ClientID %>').show();
                                                                            $('#<%= SeriesDragAlert.ClientID %>').hide();


                                                                            $find('mdlPopSeriesAlert').show();
                                                                        });
                                                                    }
                                                                }
                                                            }
                                                        },
                                                        error: function (xhr, textStatus, errorThrown) {
                                                            console.log(xhr, textStatus, errorThrown);
                                                        }
                                                    });

--%>

                                                    //To check the event is series or not end

                                                },

                                            });
                                        })

                                        // To find the current teamid obj from list to load
                                        TeamIdObj = eventText.filter(function (obj) {
                                            return (obj.TeamId === teamIDList[j].TeamId);
                                        });

                                        $('#divCalendar_' + (teamName)).fullCalendar('addEventSource', TeamIdObj); // To Load the calendar based on the TeamId
                                        $('#divCalendar_' + (teamName)).fullCalendar('refetchEvents');
                                        $('#divCalendar_' + (teamName)).fullCalendar('gotoDate', startdate); // To Load the calendar based on the TeamId and ServiceDate
                                    }
                                }
                                $.unblockUI();
                            }
                        },
                              error: function (xhr, textStatus, errorThrown) {
                                  $.unblockUI();
                                  console.log(xhr, textStatus, errorThrown);
                              }
                          });


                    }
                }

                ResetScrollPosition();
            }
        }


        function onClickEntireSeries(contractNo) {
            $find('mdlPopSeriesAlert').hide();
            var url = "/ServiceMassChange.aspx?ContractNo=" + contractNo + "";
            window.open(url, '_blank');
            //window.location.href = "/ServiceMassChange.aspx?ContractNo=" + contractNo + "";
            return false;
        }

        function onClickbtnNo() {
            $('.modal-backdrop').hide();
            $find('mdlPopSeriesAlert').hide();
            return false;
        }


        //A function that updates the dragged event in the DB
        function DraggingEvent(draggedtodate, rcnumber, teamId, isdraggedfromseries) {
            
            var draggedDate = "";
            var draggedFromTime = "";
            var draggedToTime = "";

            //isdraggedfromseries = true;
            console.log("draggedtodate", draggedtodate);
            var replacedTeamID = "";
            
            var oldeventText = eventText.filter(function (obj) {
                return (obj.TeamId === teamId);
            });
           
            var draggedObj = eventText.filter(function (obj) {

                if (parseInt(obj.RcNo) === parseInt(rcnumber)) {
                   
                    var dragDatearray = draggedtodate.split(" ");
                    var dragDate = dragDatearray[0].split("-");
                    var dragTimearray = dragDatearray[1].split(":");
                
                    draggedDate = dragDatearray[0];

                    console.log("obj.SchServiceTime", obj.SchServiceTime);
                    console.log("obj.SchServiceTimeOut", obj.SchServiceTimeOut);

                    if (obj.SchServiceTimeOut == "") {

                        obj.start = new Date(dragDate[0], dragDate[1] - 1, dragDate[2], dragTimearray[0], dragTimearray[1]);
                        var startdate = obj.start.format("yyyy-MM-dd hh:mm");
                        var startDateArray = startdate.split(" ");
                        draggedFromTime = startDateArray[1];


                        obj.end = new Date(dragDate[0], dragDate[1] - 1, dragDate[2], parseInt(dragTimearray[0]) + 2, dragTimearray[1]);
                        var endDate = obj.end.format("yyyy-MM-dd hh:mm");
                        var endDateArray = endDate.split(" ");
                        draggedToTime = endDateArray[1];

                        console.log("endDate", endDate);
                    }
                    else {

                        console.log(" obj.start ", obj.start);
                        console.log("obj.end ", obj.end);
                        var diffmin = diff_minutes(obj.start, obj.end);

                        var hours = (diffmin / 60);
                        var rhours = Math.floor(hours);
                        var minutes = (hours - rhours) * 60;
                        var rminutes = Math.round(minutes);

                        obj.start = new Date(dragDate[0], dragDate[1] - 1, dragDate[2], dragTimearray[0], dragTimearray[1]);
                        var startdate = obj.start.format("yyyy-MM-dd HH:mm");
                        var startDateArray = startdate.split(" ");
                        draggedFromTime = startDateArray[1];

                        obj.end = new Date(dragDate[0], dragDate[1] - 1, dragDate[2], (parseInt(dragTimearray[0]) +parseInt(rhours)), (parseInt(dragTimearray[1]) + parseInt(rminutes)));
                        var endDate = obj.end.format("yyyy-MM-dd HH:mm");
                        var endDateArray = endDate.split(" ");
                        draggedToTime = endDateArray[1];

                        console.log("endDate", endDate);

                    }
                   
                    obj.ServiceDateDay = dragDate[2];
                    obj.ServiceDateMonth = dragDate[1];
                    obj.ServiceDateYear = dragDate[0];

                    obj.SchServiceTime = draggedFromTime;
                    obj.SchServiceTimeOut = draggedToTime;

                    replacedTeamID = obj.RemoveTeamIDSpecialCharacter.split(' ').join('_');
                }
            });

            var neweventText = eventText.filter(function (obj) {
                return (obj.TeamId === teamId);
            });

            console.log("neweventText", neweventText);

            //var dataToPost = '{DraggedToDate:"' + draggedtodate.toString() + '", TeamID:"' + teamId+ '", RcNo:' + rcnumber + '}';
            var dataToPost = '{DraggedToDate:"' + draggedDate.toString() + '", DraggedFromTime:"' + draggedFromTime.toString() + '", DraggedToTime:"' + draggedToTime.toString() + '", RcNo:' + rcnumber + '}';
            console.log("dataToPost ", dataToPost);

            $.ajax({
                type: 'POST',
                url: 'Service.aspx/UpdateEventsdetails',
                data: dataToPost,
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {
                    if ((msg.d == null) || (msg.d == '')) { }
                    else {
                        console.log("oldeventText", oldeventText);

                        if (isdraggedfromseries)
                        {
                            TeamIdObj = oldeventText;
                            $('#divCalendar_' + replacedTeamID).fullCalendar('removeEvents'); //Hide all events
                            //$('#divCalendar_' + replacedTeamID).fullCalendar('removeEventSource', TeamIdObj);
                            TeamIdObj = neweventText;
                            $('#divCalendar_' + replacedTeamID).fullCalendar('addEventSource', TeamIdObj);
                        }
                       
                        
                        alert(msg.d + " successfully");


                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr, textStatus, errorThrown);
                }
            });
            $('#ContentPlaceHolder1_btnSeriesCancel').text("Cancel");
            onClickbtnNo();
            return false;
        }

        function ResizeTimeEvent(draggedfromdate, draggedenddate, rcnumber, teamId, isdraggedfromseries) {

            var draggedDate = "";
            var draggedFromTime = "";
            var draggedToTime = "";

            //isdraggedfromseries = true;
            console.log("draggedenddate", draggedenddate);
            var replacedTeamID = "";

            var oldeventText = eventText.filter(function (obj) {
                return (obj.TeamId === teamId);
            });

            var draggedObj = eventText.filter(function (obj) {

                if (parseInt(obj.RcNo) === parseInt(rcnumber)) {

                    var dragfromDatearray = draggedfromdate.split(" ");
                    var dragfromDate = dragfromDatearray[0].split("-");
                    var dragfromTimearray = dragfromDatearray[1].split(":");

                    

                    console.log("obj.SchServiceTime", obj.SchServiceTime);
                    console.log("obj.SchServiceTimeOut", obj.SchServiceTimeOut);

                    //if (obj.SchServiceTimeOut == "") {

                    obj.start = new Date(dragfromDate[0], dragfromDate[1] - 1, dragfromDate[2], dragfromTimearray[0], dragfromTimearray[1]);
                    var startdate = obj.start.format("yyyy-MM-dd HH:mm");
                    var startDateArray = startdate.split(" ");
                    draggedFromTime = startDateArray[1];

                    var dragendDatearray = draggedenddate.split(" ");
                    var dragendDate = dragendDatearray[0].split("-");
                    var dragendTimearray = dragendDatearray[1].split(":");
                    draggedDate = dragendDatearray[0];


                    obj.end = new Date(dragendDate[0], dragendDate[1] - 1, dragendDate[2], dragendTimearray[0] , dragendTimearray[1]);
                    var endDate = obj.end.format("yyyy-MM-dd HH:mm");
                    var endDateArray = endDate.split(" ");
                    draggedToTime = endDateArray[1];


                    console.log("endDate", endDate);
        

                    obj.ServiceDateDay = dragendDate[2];
                    obj.ServiceDateMonth = dragendDate[1];
                    obj.ServiceDateYear = dragendDate[0];

                    obj.SchServiceTime = draggedFromTime;
                    obj.SchServiceTimeOut = draggedToTime;

                    replacedTeamID = obj.RemoveTeamIDSpecialCharacter.split(' ').join('_');
                }
            });

            var neweventText = eventText.filter(function (obj) {
                return (obj.TeamId === teamId);
            });

            //var dataToPost = '{DraggedToDate:"' + draggedtodate.toString() + '", TeamID:"' + teamId+ '", RcNo:' + rcnumber + '}';
            var dataToPost = '{DraggedToDate:"' + draggedDate.toString() + '", DraggedFromTime:"' + draggedFromTime.toString() + '", DraggedToTime:"' + draggedToTime.toString() + '", RcNo:' + rcnumber + '}';
            console.log("dataToPost ", dataToPost);

            $.ajax({
                type: 'POST',
                url: 'Service.aspx/UpdateEventsdetails',
                data: dataToPost,
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {
                    if ((msg.d == null) || (msg.d == '')) { }
                    else {

                        if (isdraggedfromseries) {
                            TeamIdObj = oldeventText;
                            $('#divCalendar_' + replacedTeamID).fullCalendar('removeEvents'); //Hide all events
                            //$('#divCalendar_' + replacedTeamID).fullCalendar('removeEventSource', TeamIdObj);
                            TeamIdObj = neweventText;
                            $('#divCalendar_' + replacedTeamID).fullCalendar('addEventSource', TeamIdObj);
                        }

                        alert(msg.d + " successfully");
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr, textStatus, errorThrown);
                }
            });
            $('#ContentPlaceHolder1_btnSeriesCancel').text("Cancel");
            onClickbtnNo();
            return false;
        }



        function diff_minutes(dt2, dt1) {

            var diff = (dt2.getTime() - dt1.getTime()) / 1000;
            diff /= 60;
            return Math.abs(Math.round(diff));

        }

        function onclickbtnQuickSearch() {

            var view = document.getElementById("<%=rdoCalendarView.ClientID%>").checked;
            var search1TeamID = document.getElementById("<%=txtSearch1Team.ClientID%>").value;

            if (view == true && search1TeamID == "") {
                alert("Team Id cannot be empty.");                
                $('#<%= rdoListView.ClientID%>').prop("checked", true);
                return false;
            }
            return true;
        }

        function showAlertSelectTeamID() {
            alert("Team Id cannot be empty.");
            $('#<%= rdoListView.ClientID%>').prop("checked", true);            
        }

        function monthNames(arg) {
            var months = ["January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"];
            var selectedMonthName = months[arg];
            return selectedMonthName;
        }

        function DayNames(arg) {
            var days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            var selectedDayName = days[arg];
            return selectedDayName;
        }

        //function onClickEdit(ContractNo) {
        function onClickEdit(ContractNo, Status, TeamId, RcNo) {
            console.log('onclickedit', ContractNo);
            console.log('Status', Status);
            console.log('TeamId', TeamId);
            console.log('RcNo', RcNo);

            //To check the event is in series and double click 
            //If the event is in series ask for "This is one appointment in a series. What do you want to open?"
            //if "Just this one" then open the mdlPopupSchDate.
            //else SmartMassChange.aspx page will be loaded
           var dataSeriesPost = "";
            //dataSeriesPost = '{ContractNo:"' + event.contractno + '", Status:"' + event.Status + '", TeamId:"' + event.TeamId + '"}';
            dataSeriesPost = '{ContractNo:"' + ContractNo + '", Status:"' + Status + '", TeamId:"' + TeamId + '"}';
            $.ajax({
                type: 'POST',
                url: 'Service.aspx/GetSeriesofEvents',
                data: dataSeriesPost,
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {
                    if ((msg.d == null) || (msg.d == ''))
                    {
                            console.log("ELSE PART");
                        var hdnRcNo = "<%= hdnRcNo.ClientID%>";
                            document.getElementById(hdnRcNo).value = RcNo;

                            var hiddenrandomnumberchange = "<%= hiddenrandomnumber.ClientID()%>";
                            document.getElementById(hiddenrandomnumberchange).value = Math.random();
                            __doPostBack(hiddenrandomnumberchange, "");
                         
                    }
                    else {
                        var result = JSON.parse(msg.d);
                        console.log("result", result);
                        if (result.length > 1) {
                            console.log("if part");
                            if (result && $.isArray(result)) {
                               // element.off('dblclick').on('dblclick', function () {

                                    var hdnRcNo = "<%= hdnRcNo.ClientID%>";
                                    document.getElementById(hdnRcNo).value = RcNo;
                                    //var seriesOpenAlert = "<div class='col-md-1'></div><div class='col-md-3'><input type='submit' id='btnJustThisOne' class='btn btn-primary' style='text-align:center;' value='Just This One' onclick='return onClickJustThisOne(\"" + event.id + "\"," +  event.RcNo + "); return false;'></div> ";
                                    var seriesOpenAlert = '<asp:Button ID="btnJustThisOne" style="margin-right: 50px;" class="btn btn-primary" runat="server" Text="Just This One"  OnClick ="btnJustThisOne_Click"/>';
                                    seriesOpenAlert += "<input type='submit' id='btnEntireSeries' class='btn btn-primary' style='text-align:center;' value='The entire series' onclick='return onClickEntireSeries(\"" + ContractNo + "\");return false; '>";


                                    $('#<%= SeriesOpenAlert.ClientID%>').html('');
                                    $('#<%= SeriesOpenAlert.ClientID%>').html(seriesOpenAlert);

                                    $('#lblSeriesAlertTitle').text("");
                                    $('#SeriesAlertContent').text("");

                                    $('#btnYes').hide();
                                    $('#btnNo').hide();
                                    $('#ContentPlaceHolder1_btnJustThisOne').show();
                                    $('#btnEntireSeries').show();
                                    $('#ContentPlaceHolder1_btnSeriesCancel').show();

                                    $('#lblSeriesAlertTitle').text("Edit Recurring item");
                                //$('#SeriesAlertContent').text("This is one appointment in a series. What do you want to open?");
                                    $('#SeriesAlertContent').text("This appointment belongs to a series.");
                                    $('#<%= SeriesOpenAlert.ClientID %>').show();
                                    $('#<%= SeriesDragAlert.ClientID %>').hide();

                                    $find('mdlPopSeriesAlert').show();
                                //});
                            }
                        }
                        else {
                            console.log("ELSE PART");
                            var hdnRcNo = "<%= hdnRcNo.ClientID%>";
                            document.getElementById(hdnRcNo).value = RcNo;

                            var hiddenrandomnumberchange = "<%= hiddenrandomnumber.ClientID()%>";
                            document.getElementById(hiddenrandomnumberchange).value = Math.random();
                            __doPostBack(hiddenrandomnumberchange, "");
                            <%--$("#<%=btnEditAppointment.ClientID%>").click();--%>
                         
                        }
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr, textStatus, errorThrown);
                }
            });
            //To check the event is series or not end

            return false;
        }

        function onClickChangeStatus(lockSt, RcNo, Status, RecordNo) {
            console.log("lockSt", lockSt);
            console.log("RcNo", RcNo);
            console.log("Status", Status);
            console.log("RecordNo", RecordNo);

            $('#<%= txtRcno.ClientID%>').val(RcNo.toString());
            console.log($('#<%= txtRcno.ClientID%>').val());
            $('#ContentPlaceHolder1_txtRcno').val(RcNo);
            console.log('dtwer',$('#<%= txtRcno.ClientID%>').val());

            <%--$('#<%= txtRcno.ClientID%>').text(RcNo.toString());
            console.log($('#<%= txtRcno.ClientID%>').text());--%>

            $('#<%= txtLockSt.ClientID%>').val(lockSt);
            console.log($('#<%= txtLockSt.ClientID%>').val());
            $('#<%= ddlStatus.ClientID%>').val(Status);
            console.log($('#<%= ddlStatus.ClientID%>').val());
            $('#<%= txtSvcRecord.ClientID%>').val(RecordNo);
            console.log($('#<%= txtSvcRecord.ClientID%>').val());

         

            return true;
        }

        function showRequestFailure(arg) {
            alert("The Default Service ID of the Contract Group [" + arg + "] is not configured. Please contact your administrator.");
            //window.location.reload();
           
        }
    </script>
    </asp:Content>
