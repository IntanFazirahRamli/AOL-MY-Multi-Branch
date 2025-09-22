<%@ Page Title="AOL 2.0" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FloorPlan.aspx.vb" Inherits="FloorPlan" %>

<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="Style/bootstrap.min.css" rel="stylesheet" />
     <link href="Style/font-awesome.min.css" rel="stylesheet" />
    <link href="Style/bootstrap-datepicker.min.css" rel="stylesheet" />
       
     <link href="CSS/leaflet.css" rel="stylesheet" />
    <link href="CSS/SetmapCSS.css" rel="stylesheet" />
       
    <link href="CSS/Slidercss/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" />
    <%--<link href="CSS/Slidercss/bootstrap.css" rel="stylesheet" />--%>
    <link href="CSS/Slidercss/style.css" rel="stylesheet" />
    <link href="CSS/font-awesome.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap-switch/bootstrap-switch.min.css" rel="stylesheet" />
    <link href="CSS/evol-colorpicker.min.css" rel="stylesheet" />
     <link href="CSS/PulseIcon.css" rel="stylesheet" />
    <script src="Scripts/leaflet-src.js"></script>
      <style>
       .control-label-labelform{
            color:black;
        }  
             
  </style>
     <style>
         

.dropbtn {
  background-color: #4CAF50;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: none;
}

/* The container <div> - needed to position the dropdown content */
.dropdown {
  position: relative;
  display: inline-block;
}

/* Dropdown Content (Hidden by Default) */
.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f1f1f1;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
  padding: 11px;
  overflow-y: scroll;
  max-height: 600px;
}

fieldset.scheduler-border {
    border: 2px groove #ddd !important;
    padding: 0 1.4em 1.4em 1.4em !important;
    margin: 0 0 1.5em 0 !important;
    -webkit-box-shadow:  0px 0px 0px 0px #000;
            box-shadow:  0px 0px 0px 0px #000;
}

    legend.scheduler-border {
        font-size: 1.2em !important;
        font-weight: bold !important;
        text-align: left !important;
        width:auto;
        padding:0 10px;
        border-bottom:none;
    }

        .alignLeft {
            text-align: left !important;
        }

        .alignCenter {
            text-align: center !important;
        }

        .table > thead > tr > th {
            border-bottom: 1px solid #e1e1e1;
            /*padding: .8rem 2.5rem;*/
        }


        .table th {
            text-align: center;
        }


        .table thead {
            background-color: #f2f2f2;
        }

        .textbox {
            margin-left: 40%;
        }

        body {
            font-size: 13px !important;
        }


        .inline-label label {
            font-weight: normal !important;
            margin-left: 5px;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            color: white;
            cursor: default;
            background-color: #1f4293 !important;
            border: none;
            border-bottom-color: transparent;
            border-top-left-radius: 20px;
            border-top-right-radius: 55px;
        }


        /*tabs override css*/
        .nav-tabs {
            border-bottom: 1px solid #1f4293 !important;
        }

        .nav-item.active {
            color: #ffffff !important;
            background-color: #1f4293 !important;
            border-color: #ddd #ddd #fff;
            padding: 0px 15px !important;
            border-top-left-radius: 20px;
            border-top-right-radius: 55px;
        }

        .nav-tabs .nav-link:hover {
            border-color: #1f4293 #1f4293 #1f4293;
            border-top-left-radius: 20px;
            border-top-right-radius: 55px;
        }

        .nav-link {
            padding: 17px 41px !important;
        }

        .modal-body .list-group-item {
            font-size: 13px !important;
        }

        .modal-header .close {
            position: absolute;
            right: 19px;
            font-size: 33px;
            top: 8px;
        }

        .justify-content-center {
            height: 800px;
        }

        .btn {
            cursor: pointer;
        }
          #idMainblock {
            border: 2px solid #1d3f93;
        }
    </style>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--For Local need to comment this section--%>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" AsyncPostBackTimeout="3600">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                  <asp:controlBundle name="ComboBox_Bundle"/>   
               <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>          
            </ControlBundles>
        </asp:ToolkitScriptManager>
    <style>


        .alignLeft {
            text-align: left !important;
        }

        .modalBackground {
            /*background-color:#454545;
            filter:alpha(opacity=50);
            opacity:0.7;*/
            background-color: Gray;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)"; /* IE 8 Compatibility */
            filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=50); /* IE 7 Compatibility */
            opacity: 0.5; /* Everyone else */
        }
          #idMainblock {
            border: 2px solid #1d3f93;
        }

        .tableStyleClass {
            border: 1px solid black;
            font-weight: 300;
            font-size: 13px;
            text-align: center;
        }

    </style>

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <asp:HiddenField ID="HiddenArrowID" runat="server"  Value="0"/>
            <asp:HiddenField ID="HiddenDatetimeNow" runat="server"  Value="False"/>
            <asp:HiddenField ID="HiddenAliasName" runat="server"  Value=""/>
            <asp:HiddenField ID="HiddenEditAliasname" runat="server"  Value=""/>
            <asp:HiddenField ID="HiddenIsDuplicateDevice" runat="server"  Value="False"/>
            <asp:HiddenField ID="HiddenUserID" runat="server"  Value="False"/>
            <asp:HiddenField ID="HiddenDeletedevices" runat="server"  Value=""/>
            <asp:HiddenField ID="HiddenDeviceDescription" runat="server"  Value=""/>
            <asp:HiddenField ID="HiddenEditDescription" runat="server"  Value=""/>
            <asp:HiddenField ID="HiddenDescriptionDeviceID" runat="server"  Value=""/>

            <asp:HiddenField ID="HiddenToken" runat="server"  Value=""/>
            <asp:HiddenField ID="HiddenLocationID" runat="server"  Value=""/>
              <asp:HiddenField ID="hdnFloorPlanchange" OnValueChanged="hdnFloorPlanchange_ValueChanged" runat="server" />

            <asp:HiddenField ID="HiddenFloorPlanThresholdRCNo" runat="server"  Value=""/>
             <asp:HiddenField ID="HiddenBlinkerObj" Value="" runat="server"/>
      <div class="row">
        <div class="col-md-12 col-xs-12">
              <div id="idMainblock">

   <div class="row">
        <div class="col-md-1 col-xs-12">
        </div>
        <div class="col-md-10 col-xs-12">
            <div class="row"><div class="col-xs-12 col-md-12" style="height: 10px"></div></div>   
             <div class="row">
                 <div class="col-xs-12 col-md-2"></div>
                 <div class="col-xs-12 col-md-8 alignLeft">
                       <span> </span>
                                <span class="fa fa-chevron-left"  runat="server" style="font-size: 20px; color: #337ab7" onclick="onclickBacktoCustomerList()" >&nbsp;&nbsp; <label style="font-size: 15px !important; color: #337ab7" runat="server" id="idCustomerName"> </label></span>
                 </div>
                 <div class="col-xs-12 col-md-2"></div>
            </div> 
                             <div class="row" id="divLocationrow">
                         <div class="col-md-1 col-xs-12"></div>
                            <div class="col-md-3 col-xs-12">
                                <asp:Label ID="lblCustomerName" runat="server" class="control-label-labelform" Font-Bold="true"> </asp:Label>
                            </div>
                             <div class="col-xs-12 col-sm-12 hidden-lg" style="height:10px"></div>
                            <div class="col-md-1 col-xs-12 alignLeft">
                                <asp:Button ID="btnAddFloorPlan" CssClass="btn btn-sm btn-primary" runat="server" Text="New Floor Plan"  OnClick="btnAddFloorPlan_Click" />
                            </div>
                         <div class="col-xs-12 col-sm-12 hidden-lg" style="height:10px"></div>
                          <div class="col-md-1 col-xs-12">
                                <asp:Button ID="btndelete" CssClass="btn btn-sm btn-primary" runat="server" Text="Delete" OnClientClick="return deletefloorplan();" OnClick="btndelete_Click"  />
                                   <%-- <button class="btn btn-primary btn-sm" type="button" onclick="OnClickDelete();">
                            <span class="color-white fw-600">Delete</span>--%>
                            </div>

                         <div class="col-xs-12 col-sm-12 hidden-lg" style="height:10px"></div>
                            <div class="col-md-2 col-xs-12 alignLeft">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlfloorPlanList" class="form-control" runat="server" OnSelectedIndexChanged="ddlfloorPlanList_SelectedIndexChanged" onchange="ddlfloorPlanChange(this);"></asp:DropDownList>
                                </div>
                            </div>                   
                            <div class="col-xs-12 col-sm-12 hidden-lg" style="height:10px"></div>
                          <div class="col-md-1 col-xs-12">
                                <asp:Button ID="btnFloorPlanThreshold" CssClass="btn btn-sm btn-primary" runat="server" Text="Threshold" OnClick="btnFloorPlanThreshold_Click"  />
                            </div>
                                      <div class="col-xs-12 col-sm-12 hidden-lg" style="height:10px"></div>
                          <div class="col-md-1 col-xs-12">
                                 <button id="btnUpdate" class="btn btn-primary"  title="Update" type="button" onclick="OnClickUpdate();">Update
                                      </button>
                          </div>

                        <div class="col-xs-12 col-sm-12 hidden-lg" style="height:10px"></div>
                        <div class="col-md-1 col-xs-12">
                          <button id="btnexporttoexcel" class="btn btn-success" runat="server" onserverclick ="btnexporttoexcel_ServerClick"><i class="fa fa-file-excel-o"></i></button>
                         </div>
                        <div class="col-md-1 col-xs-12">
                            <a onclick="Showhelp()" style="font-size: 25px; color: red; text-align: right;"><i class="fa fa-question-circle"  aria-hidden="true"></i></a>
                            <%--<button id="btnhelp" onclick="Showhelp()" style="font-size: 25px; color: red; text-align: right;"><i class="fa fa-question-circle"  aria-hidden="true" ></i></button>--%>
                            <%--<span style="font-size: 25px; color: red; text-align: right;" onclick="Showhelp()"><i class="fa fa-question-circle"  aria-hidden="true" style="width: 1076px;"></i></span>--%>
                        </div>
                         
                                  <div class="col-md-2 col-xs-9  dropdown " style="width: 287px !important;margin-right: -20px;padding-top:15px;"> 
                                 <input type="text" placeholder="Search by Serial No, Alias and Description" id="txtSearch" onkeyup="return onsearchDevice();"  class="form-control dropdown-toggle"  data-toggle="dropdown" />
                               
                                <div class="dropdown-content col-xs-12" id="divSearchResults" style="display:none;">
                                </div>
                                       </div>
                                <div class="col-md-1 col-xs-6" style="width:3px; padding-top:15px;">
                                    <a  class="btn btn-primary" onclick="return onShowHideSearch();" style="color: white;padding-right: 7px;padding-left: 7px;" title="Close Results"><i class="fa fa-times"  aria-hidden="true"></i></a>
                                </div>
                                 
                    </div>
                    <div class="row">
                            <div class="col-xs-12 col-md-12" style="height: 5px"></div>
                    </div> 
            <div class="row">
                <div class="col-xs-12 col-md-8"></div>
                
                 </div> 

            <div class="row">
                <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="col-xs-12 col-md-1"></div>
                <div class="col-xs-12 col-md-10">
                     <div class="row">
                            <div class="col-xs-12 col-md-12" style="height: 5px"></div>
                    </div>   
                     <div class="row">
                            <asp:Label ID="lblProcess" Text=" " Style="color: Red" runat="server" />
                    </div>  
   


                    <div class="row">
                        <div class="col-xs-12 col-md-12" style="height: 10px">
                        </div>                  
                    </div>
                    <div class="row">
                        <div class="col-md-1 col-xs-12">
                            <span class="inline-label" style="color:Black;">
                                <input id="rdoShowLabelOn" type="radio" name="rdoShowLabel" value="rdoShowLabelOn" onclick="ddlfloorPlanChange();">
                                <label for="rdoShowLabelOn">Show Label ON</label>
                            </span>
                        </div>
                         <div class="col-md-1 col-xs-12">
                             <span class="inline-label" style="color:Black;">
                                 <input id="rdoShowLabelOff" type="radio" name="rdoShowLabel" value="rdoShowLabelOff" checked="checked" onclick="ddlfloorPlanChange();">
                                 <label for="rdoShowLabelOff">Show Label OFF</label>
                             </span>
                        </div>
                         <div class="col-md-7 col-xs-12">
                             <%--<div id="ContentPlaceHolder1_idDevicedisplayArrow">
                               <ul  id='idArrowList' style='overflow: auto;'>

                                  <li id='Arrownode1' >
                                      <span class="drag rotateable" itemtype="ARROW"  RcNo = "0"  devicetype="UpArrow" deviceid="ARROWID" devicename="ARROWNAME"  deviceurl="Images/Arrowup.png" style="z-index:1000;">
                                          <img id="ArrowImage1" src="Images/Arrowup.png" style="height:41px;width:25px;">
                                      </span>
                                  </li>
                                  <li id='Arrownode2' >
                                      <span class="drag" itemtype="ARROW" RcNo = "0" devicetype="DownArrow" deviceid="ARROWID" devicename="ARROWNAME"  deviceurl="Images/Arrowdown.png" style="z-index:1000;">
                                          <img id="ArrowImage2" src="Images/Arrowdown.png" style="height:41px;width:25px;">
                                      </span>
                                  </li>
                                  <li id='Arrownode3' >
                                      <span class="drag" itemtype="ARROW"  RcNo = "0"  devicetype="RightArrow" deviceid="ARROWID" devicename="ARROWNAME" deviceurl="Images/Arrowright.png" style="z-index:1000;">
                                          <img id="ArrowImage3" src="Images/Arrowright.png" style="height:41px;width:25px;">
                                      </span>
                                  </li>
                                       <li id='Arrownode4' >
                                      <span class="drag" itemtype="ARROW" RcNo = "0" devicetype="LeftArrow" deviceid="ARROWID" devicename="ARROWNAME"  deviceurl="Images/Arrowleft.png" style="z-index:1000;">
                                          <img id="ArrowImage4" src="Images/Arrowleft.png" style="height:41px;width:25px;">
                                      </span>
                                  </li>
                                       <li id='Arrownode5' >
                                      <span class="drag" itemtype="ARROW" RcNo = "0" devicetype="UpLeftArrow" deviceid="ARROWID" devicename="ARROWNAME" deviceurl="Images/Arrowupleft.png" style="z-index:1000;">
                                          <img id="ArrowImage5" src="Images/Arrowupleft.png" style="height:41px;width:25px;">
                                      </span>
                                  </li>
                                       <li id='Arrownode6' >
                                      <span class="drag" itemtype="ARROW" RcNo = "0" devicetype="UpRightArrow" deviceid="ARROWID" devicename="ARROWNAME"  deviceurl="Images/Arrowupright.png" style="z-index:1000;">
                                          <img id="ArrowImage6" src="Images/Arrowupright.png" style="height:41px;width:25px;">
                                      </span>
                                  </li>
                                       <li id='Arrownode7' >
                                      <span class="drag" itemtype="ARROW"  RcNo = "0" devicetype="DownLeftArrow" deviceid="ARROWID" devicename="ARROWNAME" deviceurl="Images/Arrowdownleft.png" style="z-index:1000;">
                                          <img id="ArrowImage7" src="Images/Arrowdownleft.png" style="height:41px;width:25px;">
                                      </span>
                                  </li>
                                       <li id='Arrownode8' >
                                      <span class="drag" itemtype="ARROW" RcNo = "0" devicetype="DownRightArrow" deviceid="ARROWID" devicename="ARROWNAME" deviceurl="Images/Arrowdownright.png" style="z-index:1000;">
                                          <img id="ArrowImage8" src="Images/Arrowdownright.png" style="height:41px;width:25px;">
                                      </span>
                                  </li>
                               </ul>
                                  </div>--%>       

                              </div>
                        <div class="col-md-2 col-xs-12">
                           <span style="color:blue">CONNECTED DEVICES</span>
                            <%--<span class="color-white fw-600"><label for="btnUpdate" onclick="OnClickUpdate();">Update</label></span>--%>
                           
                        </div>
                        <div class="col-md-1 hidden-xs hidden-sm">
                            <img src="Images/sync.png" width="25" height="20" title="Sync the Description of All Devices" onclick="onClickSyncAllDevicesDesc();" />
                        </div>
                    </div>
                   
                     <div class="row">
                        <div class="col-xs-12 col-md-12" style="height: 10px">
                        </div>                  
                    </div>

                    <div class="row">
                        <div class="col-xs-12 col-md-9">
<%--                            <div class="row">
                                <div class="col-xs-12 col-md-12">--%>
                                    <div id="main">
                                        <div id="map"></div>
                                    </div>
                              <%--  </div>                  
                            </div>--%>                           
                        </div>
                        <div class="col-xs-12 col-sm-12 hidden-lg hidden-md">
                            <span style="color:blue">Connected Devices</span>
                        </div>
                        <div class="col-xs-12 col-md-3">
                             
                            <div id="idDevicedisplay" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                
             <div class="col-xs-12 col-md-1"></div>
                </div> 
            </div>
            
               
                  </div>
                    <div class="col-md-1 col-xs-12">
        </div>
        </div>
                   </div>
             </div>
          </div>
           

         <div aria-hidden="true" role="dialog" tabindex="-1" id="uploadFloorPlanPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
                                            <div class="modal-dialog modal-lg">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                        <h4>
                                                            <asp:Label ID="lblPopupHeading"  Text="Create New Floor Plan" ForeColor="Black" runat="server" class="modal-title" Style="padding-left: 10px;"></asp:Label></h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="row">
                                                                    <div id="snoAlertBox" class="alert alert-success" data-alert="alert" style="display: none"></div>
                                                                    <div id="ErrorMessage" class="alert alert-success" data-alert="alert" style="display: none"></div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6 col-xs-12">
                                                                  <div class="form-group">
                                                                    <asp:Label ID="lblFloorPlanName" ForeColor="Black" runat="server" class="control-label" Text="Floor Plan Name"></asp:Label>
                                                            </div>
                                                                </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-xs-12" style="height: 2px"></div>
                                                        </div>
                                                       <div class="row">
                                                            <div class="col-md-6 col-xs-12">
                                                                  <div class="form-group">
                                                                    <asp:TextBox ID="txtFloorPlanName" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                                </div>
                                                            <div class="col-md-6 col-xs-12" style="color: black";>
                                                                    <div class="form-group">
                                                                        <input ID="avatarUpload" type="file" name="file" onchange="previewFile()"  runat="server" />
                                                                        <%--  <asp:FileUpload ID="FileUpload1" runat="server"/>--%><br />

                                                            </div>
                                                                 </div>

                                                      </div>
                                                        <div class="row">
                                                           <div class="col-md-12 col-xs-12">
                                                            <asp:Image ID="Image1" runat="server" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="100%" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-xs-12" style="height: 5px"></div>
                                                        </div>
                                                        <div class="row">
                                                           <div class="col-md-12 col-xs-12">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="Save" OnClick="btnUpload_Click" OnClientClick="return ValidateUser()"/>
                                                                </div>
                                                               </div>
                                                        </div>

                                                    </div>
                                                   <%-- <div class="modal-footer">--%>
                                                     <%--   <asp:Button ID="SavePlant" class="btn btn-success" runat="server" Text="Save" OnClick="SavePlant_Click" OnClientClick="return ValidatePlant()" />
                                                        <asp:Button ID="CancelPlant" class="btn btn-default" runat="server" Text="Cancel" OnClick="CancelPlant_Click" />--%>
                                                   <%-- </div>--%>
                                                </div>
                                            </div>
                                        </div>


       <div aria-hidden="true" role="dialog" tabindex="-1" id="GetDevicePlacedDatePopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header alignLeft">
                    <button type="button" class="close" onclick="onClickCancelMoveDevice();" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <asp:Label ID="Label1" Text="Device Placed Date " ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div id="snoAlertBox1" class="alert alert-success" data-alert="alert" style="display: none"></div>
                        <div id="ErrorMessage1" class="alert alert-success" data-alert="alert" style="display: none"></div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-xs-12 alignLeft">
                            <div class="form-group">
                                <label style="color:black;font-weight: 300!important;font-size:12px!important" id="lblAlertdevicePlacedDate"></label>
                                <%--<asp:Label ID="lblAlertdevicePlacedDate" ForeColor="Black" runat="server" class="control-label" Text=" Are you sure want move this device dated"></asp:Label>--%>
                            </div>
                        </div>

                        <div class="col-md-4 col-xs-12 alignLeft">
                            <div class="form-group">
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="dtpDevicePlacedDate" class="form-control pull-right" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                          <div class="col-md-1 col-xs-12">
                                <asp:Label ID="lblDevicePlacedTimeFrom" runat="server" class="control-label-labelform" Text="Time" Style="color: black;font-size:12px!important">Time</asp:Label>
                            </div>
                          <div class="col-md-1 col-xs-12">
                                <div runat="server" id="DivIdDevicePlacedTimeFrom">
                                        <asp:DropDownList ID="ddlDevicePlacedTimeFrom"  class="form-control" Width="120px" runat="server"></asp:DropDownList>
                                </div>
                          </div>
                                              
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12" style="height: 2px"></div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-xs-12">
                            <asp:Label ID="lblAliasName" runat="server" class="control-label-labelform" Text="Alias" Style="color: black;font-size:12px!important">Alias</asp:Label>
                        </div>
                        <div class="col-md-4 col-xs-12">
                             <asp:TextBox ID="txtAlias" class="form-control pull-right" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12" style="height: 10px"></div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 col-xs-12">
                            <asp:Label ID="lblDescription" runat="server" class="control-label-labelform" Text="Description" Style="color: black;font-size:12px!important">Description</asp:Label>
                        </div>
                        <div class="col-md-8 col-xs-12">
                             <asp:TextBox ID="txtDescription" class="form-control pull-right" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button id="btnMoveDevice"  class="btn btn-primary" onclick="return onClickMoveDevice(false);">Save</button>
                    <button id="btnCancelDeviceMove" class="btn btn-default" onclick="return onClickCancelMoveDevice();">Cancel</button>
                </div>
            </div>
        </div>
    </div>

       <div aria-hidden="true" role="dialog" tabindex="-1" id="ShowDuplicateAlertPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog ">
            <div class="modal-content">
                <%--<div class="modal-header alignLeft">
                                      <h4>
                        <asp:Label ID="Label3" Text="" ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"></asp:Label></h4>
                </div>--%>
                <div class="modal-body">
                   

                    <div class="row">
                        <div class="col-md-12 col-xs-12 alignLeft">
                            <div class="form-group">
                                <label style="color:black;font-weight: 300!important;font-size:12px!important" id="lblShowDuplicateAlert"></label>
                            </div>
                        </div>
                        </div>
                      <div class="row">
                        <div class="col-md-4 col-xs-12 alignLeft">
                             <asp:TextBox ID="txtSerialNo" class="form-control" runat="server"></asp:TextBox>
                        </div>
                                              
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12" style="height: 2px"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnDuplicate"  class="btn btn-primary" onclick="return onClickMoveDevice(true);">Yes</button>
                    <button id="btnCancelDuplicate" class="btn btn-default" onclick="return onClickCloseDuplicateAlertPopup();">No</button>
                </div>
            </div>
        </div>
    </div>

       <div aria-hidden="true" role="dialog" tabindex="-1" id="ShowDeleteAlertPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 alignLeft">
                            <div class="form-group">
                                <label style="color:black;font-weight: 300!important;font-size:12px!important" id="lblShowDeleteAlert"></label>
                                <label style="color:black;font-weight: 300!important;font-size:12px!important;display: none;" id="lblHiddenShowDeleteDeviceID"></label>
                            </div>
                        </div>
                        </div>
                      <div class="row">
                    </div>
                <div class="row">
                        <div class="col-md-12 col-xs-12" style="height: 2px"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnok"  class="btn btn-primary" onclick="return onClickconfirmeddeletedevice();">Ok</button>
                    <button id="btnCancel" class="btn btn-default" onclick="return onClickCloseDeleteAlertPopup();">Cancel</button>
                </div>
            </div>
        </div>
    </div>

       <div aria-hidden="true" role="dialog" tabindex="-1" id="HelpPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
                    <div class="modal-dialog ">
                        <div class="modal-content">
                            <div class="modal-header alignLeft">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4>
                                    <asp:Label ID="Label6" Text="Device Icons " ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"></asp:Label></h4>
                                <div class="modal-body">
                                    <div id="DeviceIconsDetails" runat="server">

                                    </div>
                                </div>
                                
                            </div>
                            <div class="modal-footer">
                                    <button id="btnCancelDevice" class="btn btn-default" onclick="return onClickCancelHelp();">Cancel</button>
                                </div>
                        </div>
                    </div>

                </div>

       <div aria-hidden="true" role="dialog" tabindex="-1" id="DescriptionPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header alignLeft">
                    <button type="button" class="close" onclick="onClickCancelDescription();" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <asp:Label ID="Label2" Text="Device Description " ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <%--<div class="row">
                        <div id="snoAlertBox1" class="alert alert-success" data-alert="alert" style="display: none"></div>
                        <div id="ErrorMessage1" class="alert alert-success" data-alert="alert" style="display: none"></div>
                    </div>--%>
                    
                    <div class="row">
                        <div class="col-md-3 col-xs-12">
                            <asp:Label ID="lblEditDescription" runat="server" class="control-label-labelform" Text="Description" Style="color: black;font-size:12px!important">Description</asp:Label>
                        </div>
                        <div class="col-md-7 col-xs-12">
                             <asp:TextBox ID="txtEditDescription" class="form-control pull-right" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-xs-12">
                            <img src="Images/sync.png" width="20" height="15" title="Sync Device Description" onclick="return onClickSyncDescription();" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnSaveDescription"  class="btn btn-primary" onclick="return onClickSaveDescription();">Save</button>
                    <button id="btnCancelDescription" class="btn btn-default" onclick="return onClickCancelDescription();">Cancel</button>
                </div>
            </div>
        </div>
    </div>

       <div aria-hidden="true" role="dialog" tabindex="-1" id="DeviceHistoryPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
                    <div class="modal-dialog ">
                        <div class="modal-content">
                            <div class="modal-header alignLeft">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4>
                                    <asp:Label ID="Label3" Text="Device History Details " ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"></asp:Label></h4>
                                <div class="modal-body">
                                    <div id="divHistoryDetails" runat="server">

                                    </div>
                                </div>
                                
                            </div>
                            <div class="modal-footer">
                                    <button id="btnCancelHistoryDetails" class="btn btn-default" onclick="return onClickCancelHistoryDetails();">Cancel</button>
                                </div>
                        </div>
                    </div>

                </div>


               <div aria-hidden="true" role="dialog" tabindex="-1" id="ConfirmSyncDeviceDescriptionPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 col-xs-12 alignLeft">
                            <div class="form-group">
                                <label style="color:black;font-weight: 300!important;font-size:16px!important" id="lblconfirm"> Do you wish to synchronize the Description of all devices in this floor plan? </label>
                            </div>
                        </div>
                        </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12" style="height: 2px"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnconfirmSyncAllDevice"  class="btn btn-primary" onclick="return onClickconfirmSyncAllDevice();">Yes</button>
                    <button id="btncancelSyncAllDevice" class="btn btn-default" onclick="return onClickCloseSyncAllDevice();">No</button>
                </div>
            </div>
        </div>
    </div>


            
    <div aria-hidden="true" role="dialog" tabindex="-1" id="FloorPlanThresholdPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <asp:Label ID="lblHeading" Text="Floor Plan Threshold" ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2 alignLeft">
                            <asp:Label ID="lblFloorPlan" ForeColor="Black" runat="server" class="control-label" Text="Floor Plan"></asp:Label>
                        </div>
                        <div class="col-md-3 alignLeft">
                            <div class="form-group">
                            <asp:Label ID="lblFloorPlantext" ForeColor="Black" runat="server" class="control-label" Text=" "></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                            <div id="Tabs">
                                <ul id="tabsJustified" class="nav nav-tabs">
                                    <li class="nav-item"><a href="" data-target="#viewDailytabid" data-toggle="tab" class="nav-link small text-uppercase active">Daily</a></li>
                                    <li class="nav-item"><a href="" data-target="#viewWeeklytabid" data-toggle="tab" class="nav-link small text-uppercase ">Weekly</a></li>
                                    <li class="nav-item"><a href="" data-target="#viewMonthlytabid" data-toggle="tab" class="nav-link small text-uppercase ">Monthly</a></li>
                                    <li class="nav-item"><a href="" data-target="#viewYearlytabid" data-toggle="tab" class="nav-link small text-uppercase ">Yearly</a></li>
                                </ul>
                                <br />


                                <div id="tabsJustifiedContent" class="tab-content">
                                    <div id="viewDailytabid" class="tab-pane fade">


                                          <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label32" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label33" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label34" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label35" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyNoActivityLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyLowLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyMediumLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyHighLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label36" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label4" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label5" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label7" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                         
                                        <div class="row alignLeft">
                                              <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyNoActivity" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyLow" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyMedium" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                              <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label11" ForeColor="Black" runat="server" class="control-label" Text="No Activity Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label8" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label9" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label10" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                </div>
                                            </div>
                                          
                                        </div>
                                        <div class="row alignLeft">
                                             <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyNoActivityColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                           
                                        </div>
                                    </div>
                                    <div id="viewWeeklytabid" class="tab-pane fade">
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label37" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label38" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label39" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label40" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                    </div>
                                        <div class="row alignLeft">
                                              <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtWeeklyNoActivityLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtWeeklyLowLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtWeeklyMediumLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtWeeklyHighLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                          
                                        </div>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label41" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label42" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label43" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label44" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyNoActivity" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyLow" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyMedium" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyHigh" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                                <div class="row alignLeft">
                                                     <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label19" ForeColor="Black" runat="server" class="control-label" Text="No Activity Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label16" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label17" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label18" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                   
                                                </div>
                                                <div class="row alignLeft">
                                                      <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyNoActivityColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                  
                                                </div>
                                    </div>
                                    <div id="viewMonthlytabid" class="tab-pane fade">
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label20" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label21" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label22" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label23" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                             <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyNoActivityLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyLowLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyMediumLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyHighLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                           
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label45" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label46" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label47" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label48" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                              <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyNoActivity" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyLow" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyMedium" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                          
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label27" ForeColor="Black" runat="server" class="control-label" Text="No Activity Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label24" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label25" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label26" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                </div>
                                            </div>
                                            
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyNoActivityColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    <div id="viewYearlytabid" class="tab-pane fade">
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label28" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label29" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label30" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label31" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtYearlyNoActivityLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtYearlyLowLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtYearlyMediumLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtYearlyHighLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        
                                        </div>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label49" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label50" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label51" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label52" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyNoActivity" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyLow" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyMedium" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyHigh" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                    <div class="row alignLeft">
                                                         <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label64" ForeColor="Black" runat="server" class="control-label" Text="No Activity Color"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label61" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label62" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label63" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                       
                                                    </div>
                                                    <div class="row alignLeft">
                                                         <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyNoActivityColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                       
                                                    </div>
                                    </div>
                                </div>                                
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12 col-xs-12" style="height: 2px"></div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-9">
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnSaveFloorPlanThreshold" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveFloorPlanThreshold_Click" OnClientClick="return ValidateFloorPlanThreshold()" />
                            <button id="Cancel" class="btn btn-default" onclick="onClickCloseFloorPlanThreshold()">Cancel</button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
  </ContentTemplate>

       <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
           <asp:AsyncPostBackTrigger ControlID="btndelete" />
        </Triggers>


    </asp:UpdatePanel>




    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
   <script src="Scripts/bootstrap.min.js"></script>
   <script src="Scripts/bootstrap-datepicker.min.js"></script>
    <script src="Scripts/autonumeric4.1.0.js"></script>
    <script src="CSS/bootstrap-switch/bootstrap-switch.min.js"></script>
    <script src="JS/jquery.blockUI.Js"></script>
        <script src="Scripts/autonumeric4.1.0.js"></script>
     <script src="JS/evol-colorpicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">



        $(function () {
            SetTabs();
        });

        function SetTabs() {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "viewDailytabid";
            $('#Tabs a[data-target="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {

                if (typeof $(this).attr("data-target") !== 'undefined') {
                    $("[id*=TabName]").val($(this).attr("data-target").replace("#", ""));
                }
            });
        };


        var FloorPlanSelectedText = "";
        var floorPlanSelectedID = "";

        $(document).ready(function () {

            $('#ContentPlaceHolder1_dtpDevicePlacedDate').datepicker({
                minDate: 0,

                format: 'dd/mm/yyyy',
                todayBtn: "linked",
                autoclose: true
            });

            ddlfloorPlanChange();

            var offset = $('#txtSearch').offset();
            var height = $('#divLocationrow').height();
            var width = $('#txtSearch').width();
            var right = (offset.left - width) + "px";

            //$('#divSearchResults').css({
            //    'left': right,
            //    'margin-left': "-128px",
            //    'top': "121px"
            //    'top': (height + 10)+ "px"
            //});

        });

        function deletefloorplan() {
            var ddlfloorPlanList = document.getElementById("<%=ddlfloorPlanList.ClientID%>");
            var selectedText = ddlfloorPlanList.options[ddlfloorPlanList.selectedIndex].innerHTML;
            if (!confirm('Do you confirm to delete floor plan ' + selectedText + '?')) {
                return false;
            }
            return true;
        }
        function alertpopup() {
            $("#snoAlertBox").text("Please select a File to Upload.");
            $("#snoAlertBox").fadeIn();

            closeSnoAlertBox();
            showpopup();
        }
        function deletepopup() {
            alert("Floor Plan deleted Successfully");
        }

        function OnClickFloorPlan() {
            showpopup();
        }

        function showpopup() {
            $('.modal-backdrop').hide();
            $('#uploadFloorPlanPopup').modal('show');
        }

        function hidepopup() {
            alert("Floor Plan Uploaded Successfully");
            $('.modal-backdrop').hide();
            $('#uploadFloorPlanPopup').modal('hide');

        }

        function closeSnoAlertBox() {
            window.setTimeout(function () {
                $("#snoAlertBox").fadeOut(2500)
            }, 1000);
        }

        $("#ContentPlaceHolder1_FileUpload1").click(function () {


            $("#btnUpload").click();
        });


        function ValidateUser() {

            if ($('#<%= txtFloorPlanName.ClientID%>').val() == '') {
                $("#<%= txtFloorPlanName.ClientID%>").attr("data-original-title", "Please Enter Floor Plan Name.");
                $("#<%= txtFloorPlanName.ClientID%>").tooltip('show');
                return false;
            }
            $("#<%= txtFloorPlanName.ClientID%>").tooltip('destroy');

            return true;
        }

        function previewFile() {
            var preview = document.querySelector('#<%=Image1.ClientID%>');
            var file = document.querySelector('#<%=avatarUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    //imageUrl = '/Images/imagegreen.jpg';
                    imageUrl = '';
                    imageWidth = 754;
                    imageHeight = 510;

                    map = setupMap();
                    markers = [];
                    //var markers = setupImport(map);
                    polylineLatlngs = [];
                    activeItemName = "";
                    hasActiveLine = false;

                    setupItems(map, markers);


                    ddlfloorPlanChange();

                    //LoadDatepicker();
                }
            });
        };

        function ddlfloorPlanChange(ddl) {

            onclickGraph();
            //for (i = 0; i < markers.length; i++) {
            //    map.removeLayer(markers[i]);
            //}
            floorPlanSelectedID = $('#<%= ddlfloorPlanList.ClientID%>').val();
            var ddlfloorplanIdselected = document.getElementById("<%=ddlfloorPlanList.ClientID%>");
           
            if (floorPlanSelectedID != null) {
                var FloorPlanSelectedText = ddlfloorplanIdselected.options[ddlfloorplanIdselected.selectedIndex].innerHTML;
                GetDeviceDislayList();
            }
        }



        function onclickGraph() {


            var floorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();

            var divmain = document.getElementById("main");
            var btndelete = document.getElementById("<%= btndelete.ClientID%>");
            var btnUpdate = document.getElementById("btnUpdate");
            if (floorplanID == null) {
                divmain.style.display = "none";
                btndelete.style.display = "none";
                btnUpdate.style.display = "none";
                return;
            }
            else {
                divmain.style.display = "block";
                btndelete.style.display = "block";
                btnUpdate.style.display = "block";
            }

            $.ajax({
                type: 'POST',
                url: 'FloorPlan.aspx/GetFloorPlanImage',
                data: "{floorplanID:'" + floorplanID + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {
                    for (i = 0; i < markers.length; i++) {
                        map.removeLayer(markers[i]);
                    }
                    markers = [];

                    ////imageUrl = '/Images/imagegreen.jpg';
                    ////imageWidth = 654;
                    ////imageHeight = 435;

                    //////map = setupMap();
                    ////markers = [];
                    //////var markers = setupImport(map);
                    ////polylineLatlngs = [];
                    ////activeItemName = "";
                    ////hasActiveLine = false;
                    ////setupItems(map, markers);


                    if ((msg.d == null) || (msg.d == '')) {
                        //map = setupMap();
                    }
                    else {
                        var result = JSON.parse(msg.d);

                        if (result && $.isArray(result)) {
                            $("#idimg").attr('src', 'data:image/png;base64,' + result[0].name);
                            map.invalidateSize(true);
                        }
                        GetFloorPlanDevicesList();
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr, textStatus, errorThrown);
                }
            });

        }
        function GetFloorPlanDevicesList() {

            var floorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();
            var locationID = '<%= Request.QueryString("LocationID").ToString()%>';
            var showlabel = '';
            if ($('input[id*=rdoShowLabelOn]').is(":checked")) {
                showlabel = "True";
            }
            else {
                showlabel = "False";
            }
            var result = [];

            $.ajax({
                type: 'POST',
                url: 'FloorPlan.aspx/GetFloorPlanDevices',
                data: "{floorplanID:'" + floorplanID + "',ShowLabel:'"+ showlabel + "',LocationID:'"+ locationID + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {
                    //for (i = 0; i < markers.length; i++) {
                    //    map.removeLayer(markers[i]);
                    //}

                    if ((msg.d == null) || (msg.d == '')) {
                        //map = setupMap();
                    }
                    else {                       
                        var result = JSON.parse(msg.d);
                        if (result && $.isArray(result)) {
                            for (var i = 0; i < result.length; i++) {

                                addItem(map, markers, result[i]);
                            }

                        }

                    }

                    var blinkerobjString = $("[id*=HiddenBlinkerObj]").val();
                    if (blinkerobjString != "") {
                        var blinkerobjString1 = JSON.parse(blinkerobjString);
                        onclickDeviceInSearch(blinkerobjString1);
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr, textStatus, errorThrown);
                }
            });
        }

        function GetDeviceDislayList() {

            $.blockUI({
                message: '<img src="Images/loader123.gif" />',
                css: {
                    border: 'none',
                    backgroundColor: 'transparent'
                }
            });

            var floorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();
            var locationID = '<%= Request.QueryString("LocationID").ToString()%>';
            if (floorplanID == null)
            {
                floorplanID = '0';
            }
            var result = [];

            var Token = $("[id*=HiddenToken]").val();

            //$("[id*=HiddenLocationID]").val(locationID);
<%--            var hdnFloorPlanchange = "<%= hdnFloorPlanchange.ClientID()%>";
            document.getElementById(hdnFloorPlanchange).value = floorplanID;
            __doPostBack(hdnFloorPlanchange, "");


            setTimeout(function () {
                setupItems(map, markers);
            }, 500);--%>

            //console.log("GetDeviceDislayList end");
            //$.unblockUI();
            //return false;


        $.ajax({
                type: 'POST',
                url: 'FloorPlan.aspx/GetDeviceDislay',
                data: "{floorplanID:'" + floorplanID + "',LocationID:'" + locationID + "',Token:'" + Token + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {
                    if ((msg.d == null) || (msg.d == '')) {
                        $.unblockUI();
                        $('#<%= idDevicedisplay.ClientID%>').html('');
                        //map = setupMap();
                    }
                    else {
                        var result = JSON.parse(msg.d);
                        if (result && $.isArray(result)) {
                            
                            var devicedisplay = "<ul  id='uldevicetype' style='color: blue!important;height: 500px;overflow: auto;list-style-type: none;text-align: left !important;margin-left: -40px;'>";

                            for (var i = 0; i < result.length; i++) {
                                //$('#<%= txtDescription.ClientID%>').val(result[i].devicedescription); //to default the device description

                                devicedisplay += " <li id='node" + i + "' style='height: 100px;'><span class='drag' itemtype='DEVICE'  RcNo = '0' devicetype='" + result[i].devicetype + "' deviceid='" + result[i].deviceid + "' devicedescription='" + result[i].devicedescription.replace("'", "") + "' devicename='" + result[i].devicename + "' deviceurl='" + result[i].deviceUrl  + "'>" + result[i].devicetype + ' - ' + result[i].deviceid + ' <br/> ' + result[i].devicedescription + "</span><hr></li>"
                            }
                            devicedisplay += "</ul>"
                            //var devicedisplay = "<table  id='uldevicetype' style='color: blue!important;overflow: auto;list-style-type: none;text-align: left !important;margin-left: -40px;'>";

                            //for (var i = 0; i < result.length; i++) {

                            //    devicedisplay += " <tr style='border-bottom: 1px solid;'><td id='node" + i + "'><span class='drag' itemtype='DEVICE'  RcNo = '0' devicetype='" + result[i].devicetype + "' deviceid='" + result[i].deviceid + "' devicedescription='" + result[i].devicedescription + "' devicename='" + result[i].devicename + "' deviceurl='" + result[i].deviceUrl + "'>" + result[i].devicetype + ' - ' + result[i].deviceid + ' <br/> ' + result[i].devicedescription + "</span></td></tr>"
                            //}
                            //devicedisplay += "</table>"
                            $('#<%= idDevicedisplay.ClientID%>').html('');
							$('#<%= idDevicedisplay.ClientID%>').html(devicedisplay);
                            setupItems(map, markers);

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
        
       

        function onClickdeletedevice(deviceid, devicecount) {
            if (devicecount > 1) {
                $("#lblHiddenShowDeleteDeviceID").text(deviceid);
                loaddeletealertpopup(devicecount);
            }
            else {
                finaldeletedevices(deviceid);
            }

        }

        function loaddeletealertpopup(devicecount) {
            var floorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();
            var deviceId = $('#lblHiddenShowDeleteDeviceID').text();


            $.ajax({
                type: 'POST',
                url: 'FloorPlan.aspx/GetDeviceidDetails',
                //data: "{DeviceID:'" + deviceId + "'}",
                data: "{floorplanID:'" + floorplanID + "',DeviceID:'" + deviceId + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {
                    if ((msg.d == null) || (msg.d == '')) {
                    }
                    else {
                        var result = JSON.parse(msg.d);
                        var alertText = "WARNING: Deleting this device in this Floor Plan will delete " + devicecount + " records historical placement/s. <br/> Device Placed Dates:";
                        for (var i = 0; i < result.length; i++) {
                            alertText += result[i].DevicePlaceddate + "<br/>";
                        }
                        $("#lblShowDeleteAlert").html(alertText);
                        $('#ShowDeleteAlertPopup').modal('show');
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr, textStatus, errorThrown);
                }
            });
        }

        function onClickconfirmeddeletedevice() {
            var deviceid = $('#lblHiddenShowDeleteDeviceID').text();
            finaldeletedevices(deviceid);
            return false;
        }
        function finaldeletedevices(deviceid) {
            var markersdelete = null;

                for (var i = 0; i < markers.length; i++) {
                    if (markers[i].deviceid == deviceid) {
                        markersdelete = markers[i];
                        break;
                    }
                }

                if (markersdelete != null) {
                    var index = markers.indexOf(markersdelete);
                    if (index > -1) {
                        markers.splice(index, 1);
                    }

                    map.removeLayer(markersdelete);

                    if (markersdelete.itemtype == "DEVICE") {
                        var parentnode = document.getElementById("uldevicetype");

                        if ($('#uldevicetype li').length == 0) {
                            var devicedisplay = "<ul  id='uldevicetype' style='color: blue!important;height: 500px;overflow: auto;list-style-type: none;text-align: left !important;margin-left: -40px;'>";
                            //devicedisplay += " <li id='node" + 0 + "' style='height: 21px;'><span class='drag' itemtype='DEVICE'  RcNo = '" + markersdelete.RcNo + "'  devicetype='" + markersdelete.devicetype + "' deviceid='" + markersdelete.deviceid + "'  devicename='" + markersdelete.devicename + "' deviceurl='" + markersdelete.deviceurl + "'>" + markersdelete.devicetype + ' - ' + markersdelete.deviceid + ' - ' + markersdelete.Description + "</span><hr></li>"
                            devicedisplay += " <li id='node" + 0 + "' style='height: 100px;'><span class='drag' itemtype='DEVICE'  RcNo = '0'  devicetype='" + markersdelete.devicetype + "' deviceid='" + markersdelete.deviceid + "'  devicename='" + markersdelete.devicename + "' deviceurl='" + markersdelete.deviceurl + "' devicedescription='" + markersdelete.Description + "'>" + markersdelete.devicetype + ' - ' + markersdelete.deviceid + ' - ' + markersdelete.Description + "</span><hr></li>"
                            $('#<%= idDevicedisplay.ClientID%>').html(devicedisplay);
                        }
                        //Table View instead li
             <%--           if ($('#uldevicetype td').length == 0) {
                            var devicedisplay = "<table  id='uldevicetype' style='color: blue!important;overflow: auto;list-style-type: none;text-align: left !important;margin-left: -40px;'>";
                            //devicedisplay += " <li id='node" + 0 + "' style='height: 21px;'><span class='drag' itemtype='DEVICE'  RcNo = '" + markersdelete.RcNo + "'  devicetype='" + markersdelete.devicetype + "' deviceid='" + markersdelete.deviceid + "'  devicename='" + markersdelete.devicename + "' deviceurl='" + markersdelete.deviceurl + "'>" + markersdelete.devicetype + ' - ' + markersdelete.deviceid + ' - ' + markersdelete.Description + "</span><hr></li>"
                            devicedisplay += " <tr style='border-bottom: 1px solid;'><td id='node" + 0 + "'><span class='drag' itemtype='DEVICE'  RcNo = '0'  devicetype='" + markersdelete.devicetype + "' deviceid='" + markersdelete.deviceid + "'  devicename='" + markersdelete.devicename + "' deviceurl='" + markersdelete.deviceurl + "' devicedescription='" + markersdelete.Description + "'>" + markersdelete.devicetype + ' - ' + markersdelete.deviceid + ' - ' + markersdelete.Description + "</span></td>"
                            $('#<%= idDevicedisplay.ClientID%>').html(devicedisplay);
                        }--%>
                        else {

                            var lastchild = $('#uldevicetype li:last-child').attr('id');
                            //var lastchild = $('#uldevicetype td:last-child').attr('id'); //Table Instead li

                            var lastchildnode = lastchild.replace("node", "");
                            var newnode = parseInt(lastchildnode) + 1;

                            var devicetype = " <li id='node" + newnode + "' style='height: 100px;'><span class='drag'  itemtype='DEVICE' RcNo = '0' devicetype='" + markersdelete.devicetype + "' deviceid='" + markersdelete.deviceid + "'  devicename='" + markersdelete.devicename + "' deviceurl='" + markersdelete.deviceurl + "' devicedescription='" + markersdelete.Description + "'>" + markersdelete.devicetype + ' - ' + markersdelete.deviceid + ' - ' + markersdelete.Description + "</span><hr></li>";
                            //Table Instead li
                            //var devicetype = " <tr style='border-bottom: 1px solid;'><td id='node" + newnode + "'><span class='drag'  itemtype='DEVICE' RcNo = '0' devicetype='" + markersdelete.devicetype + "' deviceid='" + markersdelete.deviceid + "'  devicename='" + markersdelete.devicename + "' deviceurl='" + markersdelete.deviceurl + "' devicedescription='" + markersdelete.Description + "'>" + markersdelete.devicetype + ' - ' + markersdelete.deviceid + ' - ' + markersdelete.Description + "</span></td>";
                            $("#uldevicetype").append(devicetype);
                        }
                        var hiddendeletedevices1 = $("[id*=HiddenDeletedevices]").val();
                        if (hiddendeletedevices1 == "")
                        {
                            hiddendeletedevices1 = markersdelete.RcNo;
                        }
                        else
                        {
                            hiddendeletedevices1 = hiddendeletedevices1 + "," + markersdelete.RcNo;
                        }
                        $("[id*=HiddenDeletedevices]").val(hiddendeletedevices1);
                    }
                    setupItems(map, markers);
                }

                onClickCloseDeleteAlertPopup();
        }

        function validateFloorPlan() {
            var floorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();
            if (floorplanID == null || floorplanID == '')
            {
                alert("Please select the floorPlan");
                return false;
            }
            return true;
        }

        function OnClickUpdate() {

            $("#btnUpdate").attr("disabled", true);
            var floorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();
           
            var result = [];

            var userID = $('#<%= HiddenUserID.ClientID%>').val();
            
            for (var i = 0; i < markers.length; i++) {
                    var data = {
                        deviceid: markers[i].deviceid,
                        devicename: markers[i].devicename,
                        devicetype: markers[i].devicetype,
                        Lat: markers[i].Lat,
                        Lng: markers[i].Lng,
                        position: markers[i]._latlng,
                        polyline: markers[i].polyline,
                        RcNo: markers[i].RcNo,
                        DevicePlacedDateText: markers[i].DevicePlacedDate,
                        color: markers[i].color,
                        AliasName: markers[i].AliasName,
                        Description: markers[i].Description.replace("'", ""),
                        CreatedBy: userID,
                        UpdatedBy: userID
                    };
                    result.push(data);
            }

            var JsonFloorPlanDevices1 = JSON.stringify(result);

            var markerArray = []
            var result = JSON.parse(JsonFloorPlanDevices1);

            if (result && $.isArray(result)) {
                for (var i = 0; i < result.length; i++) {

                    var dataToPush = {
                        deviceid: result[i].deviceid,
                        devicename: result[i].devicename,
                        devicetype: result[i].devicetype,
                        lat: result[i].position.lat,
                        lng: result[i].position.lng,
                        RcNo: markers[i].RcNo,
                        DevicePlacedDateText: result[i].DevicePlacedDateText,
                        AliasName: result[i].AliasName,
                        Description: result[i].Description.replace("'", ""),
                        CreatedBy: userID,
                        UpdatedBy: userID
                    };

                    markerArray.push(dataToPush);
                }
            }

            JsonFloorPlanDevices = JSON.stringify(markerArray);
            
            //return;

            var hiddendeletedevices1 = $("[id*=HiddenDeletedevices]").val();

            $.ajax({
                type: 'POST',
                url: 'FloorPlan.aspx/UploadFloorPlanDevice',
                data: "{floorplanID:'" + floorplanID + "',JsonFloorPlanDevices:'" + JsonFloorPlanDevices + "',Deletedevices:'" + hiddendeletedevices1 + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {

                    if (msg.d != '' && msg.d != null) {
                        alert(msg.d);
                    }
                    $("#btnUpdate").attr("disabled", false);
                    $("[id*=HiddenDeletedevices]").val("");
                    ddlfloorPlanChange();
                },
                error: function (xhr, textStatus, errorThrown) {
					 $("#btnUpdate").attr("disabled", false);
                    console.log(xhr, textStatus, errorThrown);
                }
            });
        }

        function onclickBacktoCustomerList() {

            location.href = './CustomerList.aspx';
        }

        function onClickMoveDevice(arg) {


            var DevicePlacedDateyear = ''; var DevicePlacedDatemonth = ''; var DevicePlacedDateday = '';
            var DevicePlacedDateHour = ''; var DevicePlacedDateMin = ''; var DevicePlacedDateSec = '';

            //if (arg) {

            var DevicePlacedDate1 = $('#<%= dtpDevicePlacedDate.ClientID%>').val().split("/");
            var DeviceplacedTime = $('#<%= ddlDevicePlacedTimeFrom.ClientID%>').val();

            var DevicePlacedDate = '';

            //Device placed date
            if (DevicePlacedDate1[1] != "1") {

                DevicePlacedDate = new Date(DevicePlacedDate1[2], DevicePlacedDate1[1] - 1, DevicePlacedDate1[0]);
            }
            else {
                DevicePlacedDate = new Date(DevicePlacedDate1[2], DevicePlacedDate1[1], DevicePlacedDate1[0]);
            }

            //Device placed Time
            if (DeviceplacedTime == "") {
                alert("Please select a Device Placed Time.");
                return false;
            }

            if (DeviceplacedTime != '') {
                DevicePlacedDateHour = ConverttimeformatHour(DeviceplacedTime);
                DevicePlacedDateMin = "0";
                DevicePlacedDateSec = "0";
            }

            if (DevicePlacedDate1 != null && DevicePlacedDate1 != '') {
                DevicePlacedDateyear = DevicePlacedDate.getFullYear();
                DevicePlacedDatemonth = (DevicePlacedDate.getMonth() + 1);
                DevicePlacedDateday = DevicePlacedDate.getDate();
                DevicePlacedDateHour = DevicePlacedDateHour;
                DevicePlacedDateMin = DevicePlacedDateMin;
                DevicePlacedDateSec = DevicePlacedDateSec;
            }
            else {
                alert("Please enter the Device Placed Date.");
                return false;
            }

            //}

            var dataPost = "{DevicePlacedDateyear:'" + DevicePlacedDateyear + "',DevicePlacedDatemonth:'" + DevicePlacedDatemonth + "',DevicePlacedDateday:'" + DevicePlacedDateday + "'," +
            "DevicePlacedDateHour:'" + DevicePlacedDateHour + "',DevicePlacedDateMin:'" + DevicePlacedDateMin + "',DevicePlacedDateSec:'" + DevicePlacedDateSec + "',deviceID:'" + eventAfterDrag.deviceid + "',floorplanID:'" + $('#<%= ddlfloorPlanList.ClientID%>').val() + "'}";

                var formattedDate = "";


                $.ajax({
                    type: 'POST',
                    url: 'FloorPlan.aspx/getDevicePlacedDate',
                    data: dataPost,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {

                        if ((data.d == null) || (data.d == '')) {
                            //map = setupMap();
                        }
                        else {
                            var result = JSON.parse(data.d);
                            if (result && $.isArray(result)) {
                                formattedDate = result[0].datetimeNowValue;
                                $("[id*=HiddenDatetimeNow]").val(formattedDate);
                                if (parseInt(result[0].noOfDuplicates) > 0) {
                                    $("[id*=HiddenIsDuplicateDevice]").val(1);
                                }
                                else {
                                    $("[id*=HiddenIsDuplicateDevice]").val(0);
                                }                                
                                var isDuplicateDevice = $("[id*=HiddenIsDuplicateDevice]").val();

                                if (arg == false) {
                                    if (isDuplicateDevice == 1) {

                                        var alertText = "An existing record has been found for " + eventAfterDrag.deviceid + " placed on " + $("[id*=HiddenDatetimeNow]").val() + ". <br/>To update, please enter the serial number on the box below.";
                                        $("#lblShowDuplicateAlert").html('');
                                        $("#lblShowDuplicateAlert").html(alertText);
                                        $('#GetDevicePlacedDatePopup').modal('hide');
                                        $('#<%= txtSerialNo.ClientID%>').val('');
                                        $('#ShowDuplicateAlertPopup').modal('show');
                                        return false;
                                    }
                                }
                                else {
                                    var serialNo = $('#<%= txtSerialNo.ClientID%>').val();
                                    if (serialNo != eventAfterDrag.deviceid) {
                                        alert("Serial Number mismatched! Record cannot be updated.");
                                        return false;
                                    }
                                    eventAfterDrag.deviceid = $('#<%= txtSerialNo.ClientID%>').val();
                                    $('#ShowDuplicateAlertPopup').modal('hide');
                                }
                                
                                       
                           
                                $("[id*=HiddenAliasName]").val($('#<%= txtAlias.ClientID%>').val());
                                $("[id*=HiddenDeviceDescription]").val($('#<%= txtDescription.ClientID%>').val());
                                // Alias name duplicate check start
                                var locationID = '<%= Request.QueryString("LocationID").ToString()%>';
                                var aliasName = $('#<%= txtAlias.ClientID%>').val();
                                        var dataPost = "{locationID:'" + locationID + "',aliasName:'" + aliasName + "'}";

                                        if ((aliasName != '') && (aliasName != $("#ContentPlaceHolder1_HiddenEditAliasname").val()))
                                        {
                                $.ajax({
                                    type: 'POST',
                                    url: 'FloorPlan.aspx/Checkaliasduplicate',
                                    data: dataPost,
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'JSON',
                                    success: function (msg) {

                                        if ((msg.d == null) || (msg.d == '')) {
                                            //map = setupMap();
                                        }
                                        else {
                                            var result = JSON.parse(msg.d);

                                            if (result && $.isArray(result)) {
                                                if (result[0].duplicate == "True") {

                                                    var ddlfloorPlanList = document.getElementById("<%=ddlfloorPlanList.ClientID%>");
                                                    var selectedText = ddlfloorPlanList.options[ddlfloorPlanList.selectedIndex].innerHTML;
                                                    var alertText = "Alias " + aliasName + " already exists for " + selectedText + " floor plan. Please set a different alias."

                                                    alert(alertText);
                                                    $('#GetDevicePlacedDatePopup').modal('show');
                                                    return false;
                                                }
                                                else {

                                                    if (!isRcNoZero) {
                                                        setTimeout(function () {
                                                            for (var i = 0; i < markers.length; i++) {
                                                                if (markers[i].deviceid == eventAfterDrag.deviceid) {

                                                                    var item = {
                                                                        name: eventAfterDrag.name,
                                                                        devicename: eventAfterDrag.devicename,
                                                                        devicetype: eventAfterDrag.devicetype,
                                                                        itemtype: eventAfterDrag.itemtype,
                                                                        deviceid: eventAfterDrag.deviceid,
                                                                        deviceurl: eventAfterDrag.deviceurl,
                                                                        RcNo: eventAfterDrag.RcNo,
                                                                        Lat: markerlatAfterDrag,
                                                                        Lng: markerlngAfterDrag,
                                                                        position: markerPositionAfterDrag,
                                                                        DevicePlacedDate: $("[id*=HiddenDatetimeNow]").val(),
                                                                        AliasName: $("[id*=HiddenAliasName]").val(),
                                                                        Description: eventAfterDrag.Description
                                                                    };

                                                                    markerRemove = markers[i];

                                                                    var index = markers.indexOf(markerRemove);
                                                                    if (index > -1) {
                                                                        markers.splice(index, 1);
                                                                    }

                                                                    map.removeLayer(markerRemove);
                                                                }
                                                            }

                                                            addItem(map, markers, item);

                                                            hideDevicePlacedDatePopup();

                                                        }, 500);
                                                    }

                                                    else {

                                                        setTimeout(function () {

                                                            var i = markers.length - 1;
                                                            if (markers[i].RcNo == 0) {
                                                                var item = {
                                                                    name: markers[i].name,
                                                                    devicename: markers[i].devicename,
                                                                    devicetype: markers[i].devicetype,
                                                                    itemtype: markers[i].itemtype,
                                                                    deviceid: markers[i].deviceid,
                                                                    deviceurl: markers[i].deviceurl,
                                                                    RcNo: markers[i].RcNo,
                                                                    Lat: markers[i]._latlng.lat,
                                                                    Lng: markers[i]._latlng.lng,
                                                                    position: markers[i]._latlng,
                                                                    DevicePlacedDate: $("[id*=HiddenDatetimeNow]").val(),
                                                                    AliasName: $("[id*=HiddenAliasName]").val(),
                                                                    Description: markers[i].Description
                                                                };

                                                                markerRemove = markers[i];

                                                                var index = markers.indexOf(markerRemove);
                                                                if (index > -1) {
                                                                    markers.splice(index, 1);
                                                                }

                                                                map.removeLayer(markerRemove);
                                                            }

                                                            addItem(map, markers, item);

                                                            hideDevicePlacedDatePopup();

                                                        }, 500);

                                                        //hideDevicePlacedDatePopup();
                                                    }
                                                }
                                            }
                                        }

                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        console.log(xhr, textStatus, errorThrown);
                                    }
                                });
                            }
                            else
                            {

                                if (!isRcNoZero) {
                                    setTimeout(function () {
                                        for (var i = 0; i < markers.length; i++) {
                                            if (markers[i].deviceid == eventAfterDrag.deviceid) {

                                                var item = {
                                                    name: eventAfterDrag.name,
                                                    devicename: eventAfterDrag.devicename,
                                                    devicetype: eventAfterDrag.devicetype,
                                                    itemtype: eventAfterDrag.itemtype,
                                                    deviceid: eventAfterDrag.deviceid,
                                                    deviceurl: eventAfterDrag.deviceurl,
                                                    RcNo: eventAfterDrag.RcNo,
                                                    Lat: markerlatAfterDrag,
                                                    Lng: markerlngAfterDrag,
                                                    position: markerPositionAfterDrag,
                                                    DevicePlacedDate: $("[id*=HiddenDatetimeNow]").val(),
                                                    AliasName: $("[id*=HiddenAliasName]").val(),
                                                    Description: eventAfterDrag.Description

                                                };

                                                markerRemove = markers[i];

                                                var index = markers.indexOf(markerRemove);
                                                if (index > -1) {
                                                    markers.splice(index, 1);
                                                }
                                                map.removeLayer(markerRemove);
                                            }
                                        }

                                        addItem(map, markers, item);

                                        hideDevicePlacedDatePopup();

                                    }, 500);
                                }

                                else {

                                    setTimeout(function () {

                                        var i = markers.length - 1;
                                        if (markers[i].RcNo == 0) {
                                            var item = {
                                                name: markers[i].name,
                                                devicename: markers[i].devicename,
                                                devicetype: markers[i].devicetype,
                                                itemtype: markers[i].itemtype,
                                                deviceid: markers[i].deviceid,
                                                deviceurl: markers[i].deviceurl,
                                                RcNo: markers[i].RcNo,
                                                Lat: markers[i]._latlng.lat,
                                                Lng: markers[i]._latlng.lng,
                                                position: markers[i]._latlng,
                                                DevicePlacedDate: $("[id*=HiddenDatetimeNow]").val(),
                                                AliasName: $("[id*=HiddenAliasName]").val(),
                                                Description: markers[i].Description

                                            };
                                            markerRemove = markers[i];

                                            var index = markers.indexOf(markerRemove);
                                            if (index > -1) {
                                                markers.splice(index, 1);
                                            }

                                            map.removeLayer(markerRemove);
                                        }

                                        addItem(map, markers, item);

                                        hideDevicePlacedDatePopup();

                                    }, 500);

                                    //hideDevicePlacedDatePopup();
                                }

                            }
                            // Alias name duplicate check end

                            }

                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log(xhr, textStatus, errorThrown);
                    }
                });


               
            //}

            //}, 500);
            return false;
        }

        function showDevicePlacedDatePopup() {

            $('.modal-backdrop').hide();
            $("#ContentPlaceHolder1_ddlDevicePlacedTimeFrom").val("");
           // $('#<%= txtAlias.ClientID%>').val('');

            $('#GetDevicePlacedDatePopup').modal('show');
        }

        function hideDevicePlacedDatePopup() {

            $('.modal-backdrop').hide();
            $('#GetDevicePlacedDatePopup').modal('hide');
        }
        
        function onClickCloseDuplicateAlertPopup() {
            $('.modal-backdrop').hide();
            $('#ShowDuplicateAlertPopup').modal('hide');
            return false;
        }

        function onClickCloseDeleteAlertPopup() {
            $('.modal-backdrop').hide();
            $('#ShowDeleteAlertPopup').modal('hide');
            return false;
        }

        function onClickCancelMoveDevice() {
            if (!isRcNoZero) {
                var markerRemove = null;
                for (var i = 0; i < markers.length; i++) {

                    if (markers[i].deviceid == eventAfterDrag.deviceid) {
                        var item = {
                            name: eventAfterDrag.name,
                            devicename: eventAfterDrag.name,
                            devicetype: eventAfterDrag.devicetype,
                            itemtype: eventAfterDrag.itemtype,
                            deviceid: eventAfterDrag.deviceid,
                            deviceurl: eventAfterDrag.deviceurl,                            
                            RcNo: eventAfterDrag.RcNo,
                            Lat: markerlatBeforeDrag,
                            Lng: markerlngBeforeDrag,
                            position: markerPositionBeforeDrag,
                            DevicePlacedDate: eventAfterDrag.DevicePlacedDate,
                            AliasName: eventAfterDrag.AliasName,
                            Description: eventAfterDrag.Description
                        };
                        markerRemove = markers[i];
                        var index = markers.indexOf(markerRemove);
                        if (index > -1) {
                            markers.splice(index, 1);
                        }

                        map.removeLayer(markerRemove);
                    }
                }

                addItem(map, markers, item);
            }
            else {

                onClickdeletedevice(hiddenDeviceID,'');
            }

            hideDevicePlacedDatePopup();
            $('#ShowDuplicateAlertPopup').modal('hide');
            return false;
        }

   
        function ConverttimeformatHour(arg) {
            // var time = $("#starttime").val();
            var time = arg;
            var hrs = Number(time.match(/^(\d+)/)[1]);
            var mnts = Number(time.match(/:(\d+)/)[1]);
            var format = time.match(/\s(.*)$/)[1];
            if (format == "PM" && hrs < 12) hrs = hrs + 12;
            if (format == "AM" && hrs == 12) hrs = hrs - 12;
            var hours = hrs.toString();
            var minutes = mnts.toString();
            if (hrs < 10) hours = "0" + hours;
            if (mnts < 10) minutes = "0" + minutes;
            //alert(hours + ":" + minutes);
            return hours;
        }
        function Showhelp() {


            var result = [];

            $.ajax({
                type: 'POST',
                url: 'FloorPlan.aspx/GetDeviceIconsImage',
                data: "{}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {

                    if ((msg.d == null) || (msg.d == '')) {
                        $('#<%= DeviceIconsDetails.ClientID%>').html('');
                    }
                    else {

                        var result = JSON.parse(msg.d);
                        if (result && $.isArray(result)) {
                            var colcount = 0;


                            var deviceicondisplay = "<table><tr>";
                            for (var i = 0; i < result.length; i++) {
                                if (result[i].name != "") { deviceicondisplay += " <td style='padding: 10px;'><img style='width:30px;height:30px;' id='imgiconid_" + i + "'src='data:image/png;base64, " + result[i].name + "' alt='Device icon'></td><td style='padding: 10px;'><label for='Description' style='color: #337ab7'>" + result[i].Description + "</label>"; colcount += 1; }
                                if (colcount % 2 == 0) {
                                    deviceicondisplay += "</td></tr>";
                                }
                            }

                            deviceicondisplay += "</table>";

                            $('#<%= DeviceIconsDetails.ClientID%>').html('');
                                        $('#<%= DeviceIconsDetails.ClientID%>').html(deviceicondisplay);
                        }
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr, textStatus, errorThrown);
                }
            });
            $('#HelpPopup').modal('show');
        }

        function onClickCancelHelp() {
            hideHelpPopup();
            return false;
        }
        function hideHelpPopup() {
            $('#HelpPopup').modal('hide');
        }

        // DescriptionPopup Start

        //Edit Description Start

            function onclickDescriptionEdit(deviceid,description) {
                $('#<%= txtEditDescription.ClientID%>').val(description);
                $("[id*=HiddenDescriptionDeviceID]").val(deviceid);
                $('#DescriptionPopup').modal('show');
            }

        function onClickSaveDescription() {
            var deviceid = $("[id*=HiddenDescriptionDeviceID]").val();
            var markerRemove = null;
            setTimeout(function () {
            for (var i = 0; i < markers.length; i++) {
                if (markers[i].deviceid == deviceid) {

                    var item = {
                        name: markers[i].name,
                        devicename: markers[i].devicename,
                        devicetype: markers[i].devicetype,
                        itemtype: markers[i].itemtype,
                        deviceid: markers[i].deviceid,
                        deviceurl: markers[i].deviceurl,
                        RcNo: markers[i].RcNo,
                        Lat: markers[i]._latlng.lat,
                        Lng: markers[i]._latlng.lat,
                        position: markers[i]._latlng,
                        DevicePlacedDate: markers[i].DevicePlacedDate,
                        AliasName: markers[i].AliasName,
                        Description: $('#<%= txtEditDescription.ClientID%>').val()
                    };

                    markerRemove = markers[i];

                    var index = markers.indexOf(markerRemove);
                    if (index > -1) {
                        markers.splice(index, 1);
                    }

                    map.removeLayer(markerRemove);
                }
            }

            //setTimeout(function () {
            //    addItem(map, markers, item);
            //    console.log("markers.length 2 ", markers.length);
            //}, 500);

            addItem(map, markers, item);
            }, 500);

            //  setupItems(map, markers);
            $('#DescriptionPopup').modal('hide');
            return false;
        }

        function onClickCancelDescription() {
            $('#DescriptionPopup').modal('hide');
            return false;
            }

        //Edit Description End
    // DescriptionPopup End

    // History Start
        function onclickHistory(deviceId) {
            $('#DeviceHistoryPopup').modal('show');

        var floorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();
        var dataPost = "{floorplanID:'" + floorplanID + "',deviceId:'" + deviceId + "'}";

        
        $.ajax({
            type: 'POST',
            url: 'FloorPlan.aspx/GetDeviceHistory',
            data: dataPost,
            contentType: 'application/json; charset=utf-8',
            dataType: 'JSON',
            success: function (msg) {

                if ((msg.d == null) || (msg.d == '')) {
                    $('#<%= divHistoryDetails.ClientID%>').html('');
                    }
                    else {

                        var result = JSON.parse(msg.d);
                        if (result && $.isArray(result)) {
                            var colcount = 0;

                            var deviceHistorydisplay = "<table class='tableStyleClass' align='center'><tr ><th class='tableStyleClass'  style='color: #337ab7;padding: 3px;'>Date</th><th class='tableStyleClass'  style='color: #337ab7;padding: 3px;'>Time</th><th class='tableStyleClass'  style='color: #337ab7;padding: 3px;'>Description</th></tr>";
                            for (var i = 0; i < result.length; i++) {
                                deviceHistorydisplay += "<tr><td class='tableStyleClass' style='color:black;font-weight: 300;font-size: 13px;'><span for='DevicePlacedDate'>" + result[i].DevicePlacedDate + "</span></td>";
                                deviceHistorydisplay += "<td class='tableStyleClass' style='color:black;font-weight: 300;font-size: 13px;'><span for='DevicePlacedTime'>" + result[i].DevicePlacedTime + "</span></td>"
                                deviceHistorydisplay += "<td class='tableStyleClass' style='color:black;font-weight: 300;font-size: 13px;'><span for='Description'>" + result[i].Description + "</span></td></tr>";
                                colcount += 1;
                            }

                            deviceHistorydisplay += "</table>";

                            $('#<%= divHistoryDetails.ClientID%>').html('');
                            $('#<%= divHistoryDetails.ClientID%>').html(deviceHistorydisplay);
                        }
                    }

                },
            error: function (xhr, textStatus, errorThrown) {
                console.log(xhr, textStatus, errorThrown);
            }
        });
       return false;
     }
     function onClickCancelHistoryDetails() {
         $('#DeviceHistoryPopup').modal('hide');
         return false;
     }
        // History End

     function showFloorPlanThresholdpopup() {
         SetTabs();
         setnumericcontrols();

         $('#Tabs a[data-target="#viewDailytabid"]').tab('show');

         //ColorPicker Start

         //Daily
         $('#ContentPlaceHolder1_txtDailyNoActivityColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtDailyLowColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtDailyMediumColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtDailyHighColor').colorpicker({
             hideButton: true
         });


         //Weekly

         $('#ContentPlaceHolder1_txtWeeklyNoActivityColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtWeeklyLowColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtWeeklyMediumColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtWeeklyHighColor').colorpicker({
             hideButton: true
         });


         //Monthly

         $('#ContentPlaceHolder1_txtMonthlyNoActivityColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtMonthlyLowColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtMonthlyMediumColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtMonthlyHighColor').colorpicker({
             hideButton: true
         });


         //Yearly

         $('#ContentPlaceHolder1_txtYearlyNoActivityColor').colorpicker({
             hideButton: true
         });
         $('#ContentPlaceHolder1_txtYearlyLowColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtYearlyMediumColor').colorpicker({
             hideButton: true
         });

         $('#ContentPlaceHolder1_txtYearlyHighColor').colorpicker({
             hideButton: true
         });

       
         //ColorPicker End

         $('.modal-backdrop').hide();
         $('#FloorPlanThresholdPopup').modal('show');
     }


     function setnumericcontrols() {

         new AutoNumeric('#<%= txtDailyNoActivity.ClientID%>', {
             decimalPlaces: '0',
             digitGroupSeparator: '',
         });

         new AutoNumeric('#<%= txtDailyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtDailyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtDailyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
           
         new AutoNumeric('#<%= txtWeeklyNoActivity.ClientID%>', {
             decimalPlaces: '0',
             digitGroupSeparator: '',
         });

            new AutoNumeric('#<%= txtWeeklyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtWeeklyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtWeeklyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

             new AutoNumeric('#<%= txtMonthlyNoActivity.ClientID%>', {
                 decimalPlaces: '0',
                 digitGroupSeparator: '',
             });
          
            new AutoNumeric('#<%= txtMonthlyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtMonthlyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtMonthlyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
          
            new AutoNumeric('#<%= txtYearlyNoActivity.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            new AutoNumeric('#<%= txtYearlyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtYearlyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtYearlyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
           
     }

        function ValidateFloorPlanThreshold() {

            //Daily Start
            if ($('#<%= txtDailyNoActivity.ClientID%>').val() == '') {
                $("#<%= txtDailyNoActivity.ClientID%>").attr("data-original-title", "Please enter FloorPlan Daily NoActivity.");
                    $("#<%= txtDailyNoActivity.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
              }
               $("#<%= txtDailyNoActivity.ClientID%>").tooltip('destroy');

            if ($('#<%= txtDailyLow.ClientID%>').val() == '') {
                $("#<%= txtDailyLow.ClientID%>").attr("data-original-title", "Please enter FloorPlan Daily Low.");
                 $("#<%= txtDailyLow.ClientID%>").tooltip('show');
                 $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                 return false;
             }
             $("#<%= txtDailyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtDailyMedium.ClientID%>').val() == '') {
                $("#<%= txtDailyMedium.ClientID%>").attr("data-original-title", "Please enter FloorPlan Daily Medium.");
                $("#<%= txtDailyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtDailyHigh.ClientID%>').val() == '') {
                $("#<%= txtDailyHigh.ClientID%>").attr("data-original-title", "Please enter FloorPlan Daily High.");
                $("#<%= txtDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyHigh.ClientID%>").tooltip('destroy');
        
            //Daily End

            //Daily Color Start

            if ($('#<%= txtDailyNoActivityColor.ClientID%>').val() == '') {
                $("#<%= txtDailyNoActivityColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Daily No Activity Color.");
                $("#<%= txtDailyNoActivityColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyNoActivityColor.ClientID%>").tooltip('destroy');
            if ($('#<%= txtDailyLowColor.ClientID%>').val() == '') {
                $("#<%= txtDailyLowColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Daily Low Color.");
                $("#<%= txtDailyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtDailyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtDailyMediumColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Daily Medium Color.");
                $("#<%= txtDailyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtDailyHighColor.ClientID%>').val() == '') {
                $("#<%= txtDailyHighColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Daily High Color.");
                $("#<%= txtDailyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyHighColor.ClientID%>").tooltip('destroy');

            //Daily Color End

            //Weekly Start
            if ($('#<%= txtWeeklyNoActivity.ClientID%>').val() == '') {
                $("#<%= txtWeeklyNoActivity.ClientID%>").attr("data-original-title", "Please enter FloorPlan Weekly NoActivity.");
                $("#<%= txtWeeklyNoActivity.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyNoActivity.ClientID%>").tooltip('destroy');

            if ($('#<%= txtWeeklyLow.ClientID%>').val() == '') {
                $("#<%= txtWeeklyLow.ClientID%>").attr("data-original-title", "Please enter FloorPlan Weekly Low.");
                $("#<%= txtWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtWeeklyMedium.ClientID%>').val() == '') {
                $("#<%= txtWeeklyMedium.ClientID%>").attr("data-original-title", "Please enter FloorPlan Weekly Medium.");
                $("#<%= txtWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtWeeklyHigh.ClientID%>').val() == '') {
                $("#<%= txtWeeklyHigh.ClientID%>").attr("data-original-title", "Please enter FloorPlan Weekly High.");
                $("#<%= txtWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyHigh.ClientID%>").tooltip('destroy');


            //Weekly End
            //Weekly Color Start

            if ($('#<%= txtWeeklyNoActivityColor.ClientID%>').val() == '') {
                $("#<%= txtWeeklyNoActivityColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Weekly No Activity Color.");
                $("#<%= txtWeeklyNoActivityColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyNoActivityColor.ClientID%>").tooltip('destroy');
            if ($('#<%= txtWeeklyLowColor.ClientID%>').val() == '') {
                $("#<%= txtWeeklyLowColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Weekly Low Color.");
                $("#<%= txtWeeklyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtWeeklyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtWeeklyMediumColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Weekly Medium Color.");
                $("#<%= txtWeeklyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtWeeklyHighColor.ClientID%>').val() == '') {
                $("#<%= txtWeeklyHighColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Weekly High Color.");
                $("#<%= txtWeeklyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyHighColor.ClientID%>").tooltip('destroy');

            //Weekly Color End

            //Monthly Start
            if ($('#<%= txtMonthlyNoActivity.ClientID%>').val() == '') {
                $("#<%= txtMonthlyNoActivity.ClientID%>").attr("data-original-title", "Please enter FloorPlan Monthly NoActivity.");
                $("#<%= txtMonthlyNoActivity.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyNoActivity.ClientID%>").tooltip('destroy');

            if ($('#<%= txtMonthlyLow.ClientID%>').val() == '') {
                $("#<%= txtMonthlyLow.ClientID%>").attr("data-original-title", "Please enter FloorPlan Monthly Low.");
                $("#<%= txtMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtMonthlyMedium.ClientID%>').val() == '') {
                $("#<%= txtMonthlyMedium.ClientID%>").attr("data-original-title", "Please enter FloorPlan Monthly Medium.");
                $("#<%= txtMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtMonthlyHigh.ClientID%>').val() == '') {
                $("#<%= txtMonthlyHigh.ClientID%>").attr("data-original-title", "Please enter FloorPlan Monthly High.");
                $("#<%= txtMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyHigh.ClientID%>").tooltip('destroy');


            //Monthly End
            //Monthly Color Start

            if ($('#<%= txtMonthlyNoActivityColor.ClientID%>').val() == '') {
                $("#<%= txtMonthlyNoActivityColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Monthly No Activity Color.");
                $("#<%= txtMonthlyNoActivityColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyNoActivityColor.ClientID%>").tooltip('destroy');
            if ($('#<%= txtMonthlyLowColor.ClientID%>').val() == '') {
                $("#<%= txtMonthlyLowColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Monthly Low Color.");
                $("#<%= txtMonthlyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtMonthlyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtMonthlyMediumColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Monthly Medium Color.");
                $("#<%= txtMonthlyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtMonthlyHighColor.ClientID%>').val() == '') {
                $("#<%= txtMonthlyHighColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Monthly High Color.");
                $("#<%= txtMonthlyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyHighColor.ClientID%>").tooltip('destroy');

            //Monthly Color End

            //Yearly Start
            if ($('#<%= txtYearlyNoActivity.ClientID%>').val() == '') {
                $("#<%= txtYearlyNoActivity.ClientID%>").attr("data-original-title", "Please enter FloorPlan Yearly NoActivity.");
                $("#<%= txtYearlyNoActivity.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyNoActivity.ClientID%>").tooltip('destroy');

            if ($('#<%= txtYearlyLow.ClientID%>').val() == '') {
                $("#<%= txtYearlyLow.ClientID%>").attr("data-original-title", "Please enter FloorPlan Yearly Low.");
                $("#<%= txtYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtYearlyMedium.ClientID%>').val() == '') {
                $("#<%= txtYearlyMedium.ClientID%>").attr("data-original-title", "Please enter FloorPlan Yearly Medium.");
                $("#<%= txtYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtYearlyHigh.ClientID%>').val() == '') {
                $("#<%= txtYearlyHigh.ClientID%>").attr("data-original-title", "Please enter FloorPlan Yearly High.");
                $("#<%= txtYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyHigh.ClientID%>").tooltip('destroy');


            //Yearly End
            //Yearly Color Start

            if ($('#<%= txtYearlyNoActivityColor.ClientID%>').val() == '') {
                $("#<%= txtYearlyNoActivityColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Yearly No Activity Color.");
                $("#<%= txtYearlyNoActivityColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyNoActivityColor.ClientID%>").tooltip('destroy');
            if ($('#<%= txtYearlyLowColor.ClientID%>').val() == '') {
                $("#<%= txtYearlyLowColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Yearly Low Color.");
                $("#<%= txtYearlyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtYearlyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtYearlyMediumColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Yearly Medium Color.");
                $("#<%= txtYearlyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtYearlyHighColor.ClientID%>').val() == '') {
                $("#<%= txtYearlyHighColor.ClientID%>").attr("data-original-title", "Please enter FloorPlan Yearly High Color.");
                $("#<%= txtYearlyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyHighColor.ClientID%>").tooltip('destroy');

            return true;
            //Yearly Color End
        }
        function hideFloorPlanThresholdpopup() {

            alert("FloorPlan threshold saved successfully.");
            $('.modal-backdrop').hide();
            $('#FloorPlanThresholdPopup').modal('hide');
        }

        function onClickCloseFloorPlanThreshold()
        {
            $('.modal-backdrop').hide();
            $('#FloorPlanThresholdPopup').modal('hide');
        }

        function onClickSyncDescription() {
            
            //$.blockUI({
            //    message: '<img src="Images/loader123.gif" />',
            //    css: {
            //        border: 'none',
            //        backgroundColor: 'transparent'
            //    }
            //});

            var deviceid = $("[id*=HiddenDescriptionDeviceID]").val();
            var locationID = '<%= Request.QueryString("LocationID").ToString()%>';

            if (deviceid == null) {
                deviceid = '0';
            }
            var result = [];

            var Token = $("[id*=HiddenToken]").val();


            $.ajax({
                type: 'POST',
                url: 'FloorPlan.aspx/syncIndividualDeviceDescription',
                data: "{DeviceID:'" + deviceid + "',LocationID:'" + locationID + "',Token:'" + Token + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (msg) {
                    if ((msg.d == null) || (msg.d == '')) {
                    }
                    else {
                        var result = JSON.parse(msg.d);
                        if (result && $.isArray(result)) {
                            
                            var devicedesc = result[0].devicedescription;
                            $('#<%= txtEditDescription.ClientID%>').val(devicedesc);

                        }
                        //$.unblockUI();
                    }


                },
            error: function (xhr, textStatus, errorThrown) {
                $.unblockUI();
                console.log(xhr, textStatus, errorThrown);
            }
            });

            return false;
        }

        function onClickSyncAllDevicesDesc() {
            $('.modal-backdrop').hide();
            $('#ConfirmSyncDeviceDescriptionPopup').modal('show');
        }

        function SyncAllDevicesDesc()
        {
                $.blockUI({
                    message: '<img src="Images/loader123.gif" />',
                    css: {
                        border: 'none',
                        backgroundColor: 'transparent'
                    }
                });


                var floorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();
                var locationID = '<%= Request.QueryString("LocationID").ToString()%>';

              
                var result = [];

                var Token = $("[id*=HiddenToken]").val();


                $.ajax({
                    type: 'POST',
                    url: 'FloorPlan.aspx/SyncAllDevicesDescription',
                    data: "{FloorplanID:'" + floorplanID + "',LocationID:'" + locationID + "',Token:'" + Token + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (msg) {
                        if ((msg.d == null) || (msg.d == '')) {
                            $.unblockUI();
                        }
                        else {
                            var result = JSON.parse(msg.d);
                            if (result && $.isArray(result)) {
                                console.log("result", result);
                                var msg = result[0].msg;
                                alert(msg);
                            }
                            $.unblockUI();
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        $.unblockUI();
                        console.log(xhr, textStatus, errorThrown);
                    }
                });

            return false;
        }

        function onClickconfirmSyncAllDevice() {
            SyncAllDevicesDesc();
            onClickCloseSyncAllDevice();
            return false;
        }

        function onClickCloseSyncAllDevice() {
            $('.modal-backdrop').hide();
            $('#ConfirmSyncDeviceDescriptionPopup').modal('hide');
            return false;
        }

        function onsearchDevice() {

            var searchText = $('#txtSearch').val();
            if (searchText != "" && searchText.length > 4) {
                var locationID = '<%= Request.QueryString("LocationID").ToString()%>';
                if (location == "") {
                    alert("Please select the Location");
                    return false;
                }

                $.blockUI({
                    message: '<img src="Images/loader123.gif" />',
                    css: {
                        border: 'none',
                        backgroundColor: 'transparent'
                    }
                });

                searchText = searchText.replace(/'/g, '\"');

                var dataPost = "{searchText:'" + searchText + "', locationid:'" + locationID + "'}";
                $.ajax({
                    type: 'POST',
                    url: 'Floorplan.aspx/getSearchData',
                    data: dataPost,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (msg) {


                        if ((msg.d == null) || (msg.d == '')) {
                            var searchResult = "";
                            searchResult = "<table  id='searchResultsTable1' style='color: black!important;overflow: auto;list-style-type: none;text-align: left !important;'>";
                            searchResult += "<tr style='border-bottom: 1px solid;'>";
                            searchResult += "<td> No records found.<br/>";
                            searchResult += "</tr>";
                            searchResult += "</table>";

                            $("#divSearchResults").show();
                            $("#divSearchResults").html(searchResult);
                            $.unblockUI();
                        }
                        else {
                            var result = JSON.parse(msg.d);

                            if (result && $.isArray(result)) {

                                var searchResult = "";
                                searchResult = "<table  id='searchResultsTable' style='color: black!important;overflow: auto;list-style-type: none;text-align: left !important;'>";
                                
                                for (var i = 0 ; i < result.length; i++) {
                                    //var itemObj = {
                                    //    deviceid: result[i].SerialNo,
                                    //    Alias: result[i].AliasName,
                                    //    itemtype: "Blinker",
                                    //    Description: result[i].Description,
                                    //    DevicePlacedDate: result[i].PlacementDateText,
                                    //    DeviceType: "",
                                    //    lat: result[i].XCoordinate,
                                    //    lng: result[i].YCoordinate,
                                    //    FloorPlanID: result[i].FloorPlanID,
                                    //    ShowDeviceIconInPortal: "False",
                                    //    isBetweenRange: true,
                                    //    deviceUrl: "NoImage",
                                    //    DeviceName: ""
                                    //};

                                    var itemObj = {
                                        deviceid: result[i].SerialNo,
                                        FloorPlanID: result[i].FloorPlanID,
                                    };
                                    var deviceid = result[i].SerialNo;

                                    searchResult += "<tr style='border-bottom: 1px solid;'>";
                                    searchResult += "<td><span><b>Alias: </b>" + result[i].AliasName + "<br/>" +
                                                            "<b>Serial No: </b><a onclick='onclickDeviceInSearch(" + JSON.stringify(itemObj) + ");'>" + result[i].SerialNo + "</a><br/>" +
                                                            "<b>Description: </b>" + result[i].Description + "<br/>" +
                                                            "<b>Location: </b>" + result[i].Location + "<br/>" +
                                                            "<b>Placement Date: </b>" + result[i].PlacementDateText + "<br/></span></td>";
                                    searchResult += "</tr>";
                                   
                                }

                                searchResult += "</table>";

                                //var p = $("#txtSearch").position();
                                //console.log("p position", p);

                                //var p1 = $("#divSearchResults").position();
                                //console.log("divSearchResults position", p1);

                                $("#divSearchResults").show();
                                $("#divSearchResults").html(searchResult);
                                $.unblockUI();
                            }
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log(xhr, textStatus, errorThrown);
                    }
                });
            }
            //return false;
        }

        function onShowHideSearch() {
            $('#txtSearch').val("");
            $("#divSearchResults").hide();
        }

        function onclickSerialNo(markerarray) {

            if (previousMarkerBlink != "") {
                map.removeLayer(previousMarkerBlink);
            }

            for (i = 0; i < markers.length; i++) {
                if (markers[i].name == markerarray.deviceid) {
                    //  previousMarkerBlink = markers[i];

                    var item = {

                        deviceid: markerarray.deviceid,
                        Alias: markerarray.Alias,
                        CountValue: markerarray.CountValue,
                        Description: markerarray.Description,
                        DeviceName: markerarray.DeviceName,
                        DeviceType: markerarray.DeviceType,
                        deviceUrl: markerarray.deviceUrl,
                        DevicePlacedDate: markerarray.DevicePlacedDate,
                        isBetweenRange: markerarray.isBetweenRange,
                        itemtype: markerarray.itemtype,
                        lat: markerarray.lat,
                        lng: markerarray.lng,
                        totalHourswithActivityPercentage: markerarray.totalHourswithActivityPercentage,
                        position: [markerarray.lat, markerarray.lng],
                        enddateday: markerarray.enddateday,
                        enddatemonth: markerarray.enddatemonth,
                        enddateyear: markerarray.enddateyear
                    };

                    addItem(map, markers, item);

                    //    L.marker([lat, lng], { icon: redIcon }).addTo(map);
                    break;
                }

            }
        }

        function onclickDeviceInSearch(arg) {
            console.log("arg", arg);

            var currentfloorplanID = $('#<%= ddlfloorPlanList.ClientID%>').val();

          <%--  var currentFloorplan = $('#<%= ddlfloorPlanList.ClientID%>').val();--%>
            console.log("currentfloorplanID", currentfloorplanID);

            if (currentfloorplanID != arg.FloorPlanID) {
                $('#<%= ddlfloorPlanList.ClientID%>').val(arg.FloorPlanID);
                    $("[id*=HiddenBlinkerObj]").val(JSON.stringify(arg));
                    ddlfloorPlanChange();
                }
                else {
                    $("[id*=HiddenBlinkerObj]").val("");
                }

            var markerarray = markers.filter(function (obj) {
                return (obj.name === arg.deviceid.toString());
                });

                if (previousMarkerBlink != "") {
                    map.removeLayer(previousMarkerBlink);
                }
                console.log("markerarray", markerarray);
                markerarray = markerarray[0];
                if (markerarray != undefined) {
                    
                    var item = {
                        deviceid: markerarray.name,
                        Alias: markerarray.AliasName,
                        CountValue: markerarray.CountValue,
                        Description: markerarray.Description,
                        DevicePlacedDate: markerarray.DevicePlacedDate,
                        DeviceName: markerarray.devicename,
                        devicetype: markerarray.devicetype,
                        deviceUrl: markerarray.deviceUrl,
                        isBetweenRange: markerarray.isBetweenRange,
                        itemtype: "Blinker",
                        //lat: arg.lat,
                        //lng: arg.lng,
                        lat: markerarray._latlng.lat,
                        lng: markerarray._latlng.lng,
                        totalHourswithActivityPercentage: markerarray.totalHourswithActivityPercentage,
                        //position: [markerarray._latlng.lat, markerarray._latlng.lng],
                        position: markerarray._latlng,
                        //enddateday: markerarray.enddateday,
                        //enddatemonth: markerarray.enddatemonth,
                        //enddateyear: markerarray.enddateyear,
                        //isBetweenRange: markerarray.isBetweenRange,

                    };
                    console.log("item", item);
                    addItem(map, markers, item);
                   
                }

                return false;
            }


</script>

<script src="JS/PulseIcon.js"></script>
<script src="Scripts/SetupmapJSFloorPlan.js"></script>
</asp:Content>





