<%@ Page Title="AOL 2.0"  Language="VB" MasterPageFile="~/MasterPage.master"  AutoEventWireup="false" CodeFile="DashboardSummary.aspx.vb" Inherits="DashboardSummary" %>

<script runat="server">

</script>

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
     
   <script src="Scripts/bootstrap.min.js"></script>--%>
  <script src="Scripts/bootstrap-datepicker.min.js"></script>
    <script src="Scripts/autonumeric4.1.0.js"></script>
    <script src="JS/SliderJS/slider.js"></script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
        .ModalPanel {
            border: solid 2px grey;
            position: absolute;
            z-index: 9999999;
            width: 400px;
            height: 400px;
            background-color: whitesmoke;
        }

        .topBanner {
            background: #036;
            color: #fff;
            text-align: center;
            font-size: 10px;
            height: 12px;
        }

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
            margin-top: 1px;
            border: 1px solid #1d3f93;
            margin-left: 20px;
        }

        .textboxClassEdit {
        color: black;
        }

       .table th {
    text-align: center;
}

        .inline-label label {
            font-weight: normal !important;
            margin-left: 5px;
        }
    </style>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

            <Triggers>
            
            <asp:PostBackTrigger ControlID="rdoDay" />
            <asp:PostBackTrigger ControlID="rdoWeek" />
            <asp:PostBackTrigger ControlID="rdoMonth" />
            <asp:PostBackTrigger ControlID="rdoYear" />

            <asp:PostBackTrigger ControlID="btnSubmit" />

        </Triggers>

    <ContentTemplate>

    <asp:HiddenField ID="hiddenRcNo" runat="server" />

        <div class="row">
        <div class="col-xs-12 col-md-12" style="height: 20px"></div>
    </div>


    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10">

            <div class="row">
                <div class="col-md-1 alignLeft" style="color: black;">
                    <asp:RadioButton ID="rdoDay" GroupName="rdoLocationStatus" OnCheckedChanged="rdoDay_CheckedChanged" AutoPostBack="true"
                        Text="  Day" runat="Server" Enabled="true" Checked="true" CssClass="inline-label" />

                </div>
                <div class="col-md-1 alignLeft" style="color: black;">
                    <asp:RadioButton ID="rdoWeek" GroupName="rdoLocationStatus" OnCheckedChanged="rdoWeek_CheckedChanged" AutoPostBack="true"
                        Text="  Week" runat="Server" Enabled="true" CssClass="inline-label" />
                </div>
                <div class="col-md-1 alignLeft" style="color: black;">
                    <asp:RadioButton ID="rdoMonth" GroupName="rdoLocationStatus" OnCheckedChanged="rdoMonth_CheckedChanged" AutoPostBack="true"
                        Text="  Month" runat="Server" Enabled="true" CssClass="inline-label" />

                </div>
                <div class="col-md-1 alignLeft" style="color: black;">
                    <asp:RadioButton ID="rdoYear" GroupName="rdoLocationStatus" OnCheckedChanged="rdoYear_CheckedChanged" AutoPostBack="true"
                        Text="Year" runat="Server" Enabled="true" CssClass="inline-label" />
                </div>

            </div>
                <div class="row">
                    <div class="col-xs-12 col-md-12" style="height: 5px"></div>
                </div>
            <div class="row">

                    <div class="col-md-1 alignLeft">
                        <asp:Label ID="lblFromDate" runat="server" class="control-label-labelform" Text="From Date" Style="color: black;">From Date</asp:Label>
                        <asp:Label ID="lblFromWeek" runat="server" class="control-label-labelform" Text="From Week" Style="color: black;">From Week</asp:Label>
                        <asp:Label ID="lblFromMonth" runat="server" class="control-label-labelform" Text="From Month" Style="color: black;">From Month</asp:Label>
                        <asp:Label ID="lblFromYear" runat="server" class="control-label-labelform" Text="From Year" Style="color: black;">From Year</asp:Label>
                    </div>
                    <div class="col-md-3 col-xs-3 alignLeft">

                        <div class="form-group">
                            <div runat="server" id="DivFromlocationStatusDate">
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <asp:TextBox ID="dtpFromLocationStatusDate" class="form-control pull-right" runat="server"></asp:TextBox>
                            </div>
                            </div>

                            <asp:DropDownList ID="ddlfromweekList" class="form-control" Width="250px" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlfrommonthyear" class="form-control" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlfromyear" class="form-control" runat="server"></asp:DropDownList>

                        </div>
                         </div>
                        </div>

            <div class="row">
                    <div class="col-md-1 alignLeft">
                        <div class="form-group">
                           <asp:Label ID="lblToDate" runat="server" class="control-label-labelform" Text="To Date" Style="color: black;">To Date</asp:Label>
                        <asp:Label ID="lblToWeek" runat="server" class="control-label-labelform" Text="To Week" Style="color: black;">To Week</asp:Label>
                        <asp:Label ID="lblToMonth" runat="server" class="control-label-labelform" Text="To Month" Style="color: black;">To Month</asp:Label>
                        <asp:Label ID="lblToYear" runat="server" class="control-label-labelform" Text="To Year" Style="color: black;">To Year</asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3 col-xs-3 alignLeft">
                        <div class="form-group">
                                <div runat="server" id="DivTolocationStatusDate">
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="dtpToLocationStatusDate" class="form-control pull-right" runat="server"></asp:TextBox>
                                </div>
                                </div>

                              <asp:DropDownList ID="ddltoweekList" class="form-control" Width="250px" runat="server"></asp:DropDownList>                            
                              <asp:DropDownList ID="ddltomonthyear" class="form-control" runat="server"></asp:DropDownList>
                              <asp:DropDownList ID="ddltoyear" class="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                     <div class="col-md-2 col-xs-3 alignLeft">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-sm btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>

               </div>

            <div class="row">
                <div class="col-md-12 col-xs-12" style="height: 10px"></div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gridLocationStatus" runat="server" CssClass="table table-bordered table alignLeft" OnRowDataBound ="gridLocationStatus_RowDataBound" AutoGenerateColumns="false" HeaderStyle-ForeColor="Black">
                            <Columns>
                           
                                <asp:TemplateField  HeaderText="LocationID"  ItemStyle-HorizontalAlign="center" ItemStyle-Width="120px" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocationID" runat="server"
                                            Text='<%#Eval("LocationID")%>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="350px"  ItemStyle-HorizontalAlign="center" HeaderText="SiteName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiteName" runat="server"
                                            Text='<%#Eval("SiteName")%>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="450px" HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server"
                                            Text='<%#Eval("Address")%>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"
                                            Text='<%#Eval("Status")%>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100px" ItemStyle-HorizontalAlign="center" HeaderText="Devices w/ Activity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeviceWithActivity" runat="server"
                                            Text='<%#Eval("DeviceWithActivity")%>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="center" HeaderText="Total Hours w/ Activity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotHoursWithActivity" runat="server"
                                            Text='<%#Eval("TotHoursWithActivity")%>' ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                      <asp:HyperLinkField ItemStyle-Width="100px" ItemStyle-HorizontalAlign="center" DataNavigateUrlFields="LocationID" DataNavigateUrlFormatString="~\Dashboard.aspx?LocationID={0}" Text="View" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
           </div>

 
                    </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" id="startTime">

        $(document).ready(function () {

            //$("#divSlidebar").hide();

            LoadDatepicker();

        });

        function LoadDatepicker() {
            $('#ContentPlaceHolder1_dtpFromLocationStatusDate').datepicker({
                minDate: 0,

                format: 'dd/mm/yyyy',
                todayBtn: "linked",
                autoclose: true
            });

            $('#ContentPlaceHolder1_dtpToLocationStatusDate').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: "linked",
                autoclose: true
            });
        }

           </script>

    </asp:Content>