<%@ Page Title="AOL 2.0" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="DeviceType.aspx.vb" Inherits="DeviceType" %>

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

    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
     
   <script src="Scripts/bootstrap.min.js"></script>
   <script src="Scripts/bootstrap-datepicker.min.js"></script>
    <script src="Scripts/autonumeric4.1.0.js"></script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            /*margin-top: 1px;*/
            border: 2px solid #1d3f93;
            /*margin-left: 2%;*/
            
        }


    </style>

    <asp:HiddenField ID="hiddenRcNo" runat="server" />
    <asp:HiddenField ID="HiddenisLogoClear" runat="server" />
    <div class="row">
        <div class="col-xs-12 col-md-12" style="height: 10px"></div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-md-12" style="height: 20px"></div>
    </div>
    <div class="row">
        <div class="col-md-12 col-xs-12">
              <div id="idMainblock">
            <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10">

            <div class="row">
                <div class="col-md-1 col-xs-1 col-sm-1"></div>
                <div class="col-md-4 col-xs-11 col-sm-11 alignLeft" style="padding-top:1%">
                    <asp:Button ID="btnAddDeviceType" CssClass="btn btn-sm btn-primary " runat="server" Text="Add Device Type" OnClick="btnAddDeviceType_Click" />
                </div>                
            </div>

            <div class="row">
                <div class="col-md-12 col-xs-12" style="height: 10px"></div>
            </div>
            <div class="row">
                <div class="col-md-12 col-xs-12 ">
                    <div class="table-responsive">
                        <asp:GridView ID="DeviceTypeGrid" runat="server" CssClass="table table-bordered table alignLeft" AutoGenerateColumns="false" HeaderStyle-ForeColor="Black">
                            <Columns>

                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="hdnRcNo" Text='<%#Eval("RcNo")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="200px" HeaderText="Device Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeviceType" runat="server"
                                            Text='<%#Eval("DeviceType")%>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="200px" HeaderText="Device Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeviceDescription" runat="server"
                                            Text='<%#Eval("DeviceDescription")%>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="200px" HeaderText="Image">
                                      <ItemTemplate>
                                        <asp:Image ID="GridImage" runat="server" ImageUrl ='<%# Eval("ImageURL")%>' height="30px" Width="30px" />
                                      </ItemTemplate>
                                  </asp:TemplateField>


                                <asp:TemplateField HeaderText="" ItemStyle-Width="100px">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("RcNo")%>' OnClick="lnkDelete_Click" OnClientClick="javascript:if(!confirm('Are you sure you want to delete this Device Type?')){return false;}"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
            </div>
                   </div>
            </div>
    </div>

    <div aria-hidden="true" role="dialog" tabindex="-1" id="DeviceTypePopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <asp:Label ID="lblPopupHeading" Text=" " ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div id="snoAlertBox" class="alert alert-success" data-alert="alert" style="display: none"></div>
                        <div id="ErrorMessage" class="alert alert-success" data-alert="alert" style="display: none"></div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblDeviceType" ForeColor="Black" runat="server" class="control-label" Text="Device Type"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-6 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:TextBox ID="txtDeviceType" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12" style="height: 2px"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblDeviceDescription" ForeColor="Black" runat="server" class="control-label" Text="Device Description"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-6 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:TextBox ID="txtDeviceDescription" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12" style="height: 2px"></div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblIcon" ForeColor="Black" runat="server" class="control-label" Text="Upload Icon"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                            </div>
                        </div>

                        <div class="col-md-6 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/noImage.png" style="height: 30px;width: 30px;"/>
                              <%--  <button id="btnClearImage" class="btn btn-primary" onclick="onClickClearImage()">Clear Image</button>--%>
                                 <button class="btn btn-primary" type="button" onclick="onClickClearImage();">Clear Image</button>

                            </div>
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveDevicetype" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveDevicetype_Click" OnClientClick="return ValidateDeviceType()" />
                    <button id="CancelPlant" class="btn btn-default" onclick="onClickClose()">Cancel</button>
                </div>
            </div>
        </div>
    </div>


    <script>

        function showpopup() {
            $('.modal-backdrop').hide();
            $('#DeviceTypePopup').modal('show');
        }

        function hidepopup() {
            alert("Device type saved successfully.");
            $('.modal-backdrop').hide();
            $('#DeviceTypePopup').modal('hide');

        }


        function onClickClearImage() {
            
            $('#<%= HiddenisLogoClear.ClientID%>').val('1')
            d = new Date();
            $("#<%= Image1.ClientID%>").attr('src', '../images/noImage.png' + '?' + d.getTime());
            return false;

        }
        

        function ValidateDeviceType() {

            if ($('#<%= txtDeviceType.ClientID%>').val() == '') {
                $("#<%= txtDeviceType.ClientID%>").attr("data-original-title", "Please enter Device Type.");
                $("#<%= txtDeviceType.ClientID%>").tooltip('show');
                return false;
            }
            $("#<%= txtDeviceType.ClientID%>").tooltip('destroy');



            return true;
        }

        function onClickClose() {
            $('.modal-backdrop').hide();
            $('#DeviceTypePopup').modal('hide');
        }


        function deletepopup() {
            alert("Device type deleted successfully.");
            $('.modal-backdrop').hide();
            $('#DeviceTypePopup').modal('hide');

        }

        function previewFile() {
            $('#<%= HiddenisLogoClear.ClientID%>').val('0')
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

        function alertpopup() {
            $("#snoAlertBox").text("Please select a File to Upload.");
            $("#snoAlertBox").fadeIn();

            closeSnoAlertBox();
            showpopup();
        }

        function closeSnoAlertBox() {
            window.setTimeout(function () {
                $("#snoAlertBox").fadeOut(2500)
            }, 1000);
        }


    </script>



</asp:Content>
