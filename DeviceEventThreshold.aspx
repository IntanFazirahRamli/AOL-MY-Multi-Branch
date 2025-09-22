<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="DeviceEventThreshold.aspx.vb" Inherits="DeviceEventThreshold" %>
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
    <link href="CSS/evol-colorpicker.min.css" rel="stylesheet" />

    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
     
   <script src="Scripts/bootstrap.min.js"></script>
   <script src="Scripts/bootstrap-datepicker.min.js"></script>
    <script src="Scripts/autonumeric4.1.0.js"></script>
    <script src="js/evol-colorpicker.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
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

    <asp:HiddenField ID="hiddenRcNo" runat="server" />
    <%--Daily--%>
    <asp:HiddenField ID="hdnisLogoClearDailylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearDailyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearDailyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearDailyVeryHigh" runat="server" />
    <%--Weekly--%>
    <asp:HiddenField ID="hdnisLogoClearWeeklylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearWeeklyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearWeeklyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearWeeklyVeryHigh" runat="server" />
    <%--Monthly--%>
    <asp:HiddenField ID="hdnisLogoClearMonthlylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearMonthlyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearMonthlyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearMonthlyVeryHigh" runat="server" />
    <%--Yearly--%>
    <asp:HiddenField ID="hdnisLogoClearYearlylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearYearlyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearYearlyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearYearlyVeryHigh" runat="server" />

    <%--Total Daily--%>
    <asp:HiddenField ID="hdnisLogoClearTotalDailylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalDailyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalDailyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalDailyVeryHigh" runat="server" />
    <%--Total Weekly--%>
    <asp:HiddenField ID="hdnisLogoClearTotalWeeklylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalWeeklyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalWeeklyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalWeeklyVeryHigh" runat="server" />
    <%--Total Monthly--%>
    <asp:HiddenField ID="hdnisLogoClearTotalMonthlylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalMonthlyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalMonthlyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalMonthlyVeryHigh" runat="server" />
    <%--Total Yearly--%>
    <asp:HiddenField ID="hdnisLogoClearTotalYearlylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalYearlyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalYearlyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalYearlyVeryHigh" runat="server" />


    <%--Total Ratio Daily--%>
    <asp:HiddenField ID="hdnisLogoClearTotalRatioDailylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioDailyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioDailyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioDailyVeryHigh" runat="server" />
    <%--Total Ratio Weekly--%>
    <asp:HiddenField ID="hdnisLogoClearTotalRatioWeeklylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioWeeklyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioWeeklyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioWeeklyVeryHigh" runat="server" />
    <%--Total Ratio Monthly--%>
    <asp:HiddenField ID="hdnisLogoClearTotalRatioMonthlylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioMonthlyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioMonthlyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioMonthlyVeryHigh" runat="server" />
    <%--Total Ratio Yearly--%>
    <asp:HiddenField ID="hdnisLogoClearTotalRatioYearlylow" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioYearlyMedium" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioYearlyHigh" runat="server" />
    <asp:HiddenField ID="hdnisLogoClearTotalRatioYearlyVeryHigh" runat="server" />

    <div class="row">
        <div class="col-xs-12 col-md-12" style="height: 10px"></div>
    </div>
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
        <div class="col-md-1 col-xs-1 col-sm-1"></div>
        <div class="col-md-10 col-xs-11 col-sm-11">

            <div class="row">
                <div class="col-md-3 alignLeft" style="padding-top:1%">

                    <asp:Button ID="btnAddDeviceEventThreshold" CssClass="btn btn-sm btn-primary " runat="server" Text="Add Device Event Threshold" OnClick="btnAddDeviceEventThreshold_Click" />
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-1 col-sm-1" style="height: 10px"></div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-11 col-sm-11">
                        <div class="table-responsive">
                            <asp:GridView ID="GridDeviceEvent" runat="server" CssClass="table table-bordered table alignLeft" AutoGenerateColumns="false"
                                OnRowDataBound="GridDeviceEvent_RowDataBound"
                                 HeaderStyle-ForeColor="Black" Style="max-width: 950px">
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("RcNo")%>' OnClick="lnkDelete_Click" OnClientClick="javascript:if(!confirm('Are you sure you want to delete this Device Event Threshold?')){return false;}"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkDuplicate" runat="server" Text="Duplicate" CommandArgument='<%#Eval("RcNo")%>' OnClick="lnkDuplicate_Click" OnClientClick="javascript:if(!confirm('Are you sure you want to Duplicate this Device Event Threshold?')){return false;}"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="hdnRcNo" Text='<%#Eval("RcNo")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AccountID" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccountID" runat="server"
                                                Text='<%#Eval("AccountID")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Device Type" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeviceType" runat="server"
                                                Text='<%#Eval("DeviceType")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Daily Label Start--%>

                                    <asp:TemplateField HeaderText="Daily Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDailyLow" Text='<%#Eval("DailyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Daily Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDailyMedium" Text='<%#Eval("DailyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Daily High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDailyHigh" Text='<%#Eval("DailyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Daily VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDailyVeryHigh" Text='<%#Eval("DailyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Daily Label End--%>

                                    <%--Weekly Label Start--%>
                                    <asp:TemplateField HeaderText="Weekly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWeeklyLow" Text='<%#Eval("WeeklyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weekly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWeeklyMedium" Text='<%#Eval("WeeklyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weekly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWeeklyHigh" Text='<%#Eval("WeeklyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weekly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWeeklyVeryHigh" Text='<%#Eval("WeeklyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Weekly Label End--%>

                                    <%--Monthly Label Start--%>
                                    <asp:TemplateField HeaderText="Monthly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonthlyLow" Text='<%#Eval("MonthlyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonthlyMedium" Text='<%#Eval("MonthlyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonthlyHigh" Text='<%#Eval("MonthlyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonthlyVeryHigh" Text='<%#Eval("MonthlyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Monthly Label End--%>

                                    <%--Yearly Label Start--%>
                                    <asp:TemplateField HeaderText="Yearly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYearlyLow" Text='<%#Eval("YearlyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Yearly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYearlyMedium" Text='<%#Eval("YearlyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Yearly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYearlyHigh" Text='<%#Eval("YearlyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Yearly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYearlyVeryHigh" Text='<%#Eval("YearlyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Yearly Label End--%>

                                    <%--Total Label Start--%>

                                    <%--Total Label (Daily) Start--%>
                                    <asp:TemplateField HeaderText="Total Daily Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDailyLow" Text='<%#Eval("TotalDailyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Daily Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDailyMedium" Text='<%#Eval("TotalDailyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Daily High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDailyHigh" Text='<%#Eval("TotalDailyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Daily VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDailyVeryHigh" Text='<%#Eval("TotalDailyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Total Label (Daily) End--%>

                                    <%--Total Label (Weekly) Start--%>
                                    <asp:TemplateField HeaderText="Total Weekly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalWeeklyLow" Text='<%#Eval("TotalWeeklyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Weekly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalWeeklyMedium" Text='<%#Eval("TotalWeeklyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Weekly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalWeeklyHigh" Text='<%#Eval("TotalWeeklyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Weekly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalWeeklyVeryHigh" Text='<%#Eval("TotalWeeklyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Total Label (Weekly) End--%>

                                    <%--Total Label (Monthly) Start--%>
                                    <asp:TemplateField HeaderText="Total Monthly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalMonthlyLow" Text='<%#Eval("TotalMonthlyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Monthly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalMonthlyMedium" Text='<%#Eval("TotalMonthlyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Monthly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalMonthlyHigh" Text='<%#Eval("TotalMonthlyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Monthly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalMonthlyVeryHigh" Text='<%#Eval("TotalMonthlyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Total Label (Monthly) End--%>

                                    <%--Total Label (Yearly) Start--%>
                                    <asp:TemplateField HeaderText="Total Yearly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalYearlyLow" Text='<%#Eval("TotalYearlyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Yearly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalYearlyMedium" Text='<%#Eval("TotalYearlyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Yearly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalYearlyHigh" Text='<%#Eval("TotalYearlyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Yearly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalYearlyVeryHigh" Text='<%#Eval("TotalYearlyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Total Label (Yearly) End--%>

                                    <%--Total Label End--%>

                                    <%--Total Ratio Label Start--%>

                                    <%--Total Ratio Label (Daily) Start--%>
                                    <asp:TemplateField HeaderText="Total Ratio Daily Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioDailyLow" Text='<%#Eval("TotalRatioDailyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Daily Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioDailyMedium" Text='<%#Eval("TotalRatioDailyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Daily High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioDailyHigh" Text='<%#Eval("TotalRatioDailyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Daily VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioDailyVeryHigh" Text='<%#Eval("TotalRatioDailyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Total Ratio Label (Daily) End--%>

                                    <%--Total Ratio Label (Weekly) Start--%>
                                    <asp:TemplateField HeaderText="Total Ratio Weekly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioWeeklyLow" Text='<%#Eval("TotalRatioWeeklyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Weekly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioWeeklyMedium" Text='<%#Eval("TotalRatioWeeklyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Weekly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioWeeklyHigh" Text='<%#Eval("TotalRatioWeeklyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Weekly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioWeeklyVeryHigh" Text='<%#Eval("TotalRatioWeeklyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Total Ratio Label (Weekly) End--%>

                                    <%--Total Ratio Label (Monthly) Start--%>
                                    <asp:TemplateField HeaderText="Total Ratio Monthly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioMonthlyLow" Text='<%#Eval("TotalRatioMonthlyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Monthly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioMonthlyMedium" Text='<%#Eval("TotalRatioMonthlyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Monthly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioMonthlyHigh" Text='<%#Eval("TotalRatioMonthlyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Monthly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioMonthlyVeryHigh" Text='<%#Eval("TotalRatioMonthlyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Total Ratio Label (Monthly) End--%>

                                    <%--Total Ratio Label (Yearly) Start--%>
                                    <asp:TemplateField HeaderText="Total Ratio Yearly Low" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioYearlyLow" Text='<%#Eval("TotalRatioYearlyLow")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Yearly Medium" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioYearlyMedium" Text='<%#Eval("TotalRatioYearlyMedium")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Yearly High" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioYearlyHigh" Text='<%#Eval("TotalRatioYearlyHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Ratio Yearly VeryHigh" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioYearlyVeryHigh" Text='<%#Eval("TotalRatioYearlyVeryHigh")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Total Ratio Label (Yearly) End--%>

                                    <%--Total Ratio Label End--%>

                                    <%--Daily Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDailyLowColor" Text='<%#Eval("DailyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDailyMediumColor" Text='<%#Eval("DailyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDailyHighColor" Text='<%#Eval("DailyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDailyVeryHighColor" Text='<%#Eval("DailyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Daily Color End--%>

                                    <%--Weekly Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWeeklyLowColor" Text='<%#Eval("WeeklyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWeeklyMediumColor" Text='<%#Eval("WeeklyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWeeklyHighColor" Text='<%#Eval("WeeklyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWeeklyVeryHighColor" Text='<%#Eval("WeeklyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Weekly Color End--%>

                                    <%--Weekly Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonthlyLowColor" Text='<%#Eval("MonthlyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonthlyMediumColor" Text='<%#Eval("MonthlyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonthlyHighColor" Text='<%#Eval("MonthlyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonthlyVeryHighColor" Text='<%#Eval("MonthlyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Weekly Color End--%>

                                    <%--Yearly Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYearlyLowColor" Text='<%#Eval("YearlyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYearlyMediumColor" Text='<%#Eval("YearlyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYearlyHighColor" Text='<%#Eval("YearlyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYearlyVeryHighColor" Text='<%#Eval("YearlyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Yearly Color End--%>

                                    <%--Total Threshold Color Start--%>
                                    <%--Total Daily Threshold Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDailyLowColor" Text='<%#Eval("TotalDailyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDailyMediumColor" Text='<%#Eval("TotalDailyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDailyHighColor" Text='<%#Eval("TotalDailyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDailyVeryHighColor" Text='<%#Eval("TotalDailyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									
                                    <%--Total Daily Threshold Color End--%>

                                    <%--Total Weekly Threshold Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalWeeklyLowColor" Text='<%#Eval("TotalWeeklyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalWeeklyMediumColor" Text='<%#Eval("TotalWeeklyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalWeeklyHighColor" Text='<%#Eval("TotalWeeklyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalWeeklyVeryHighColor" Text='<%#Eval("TotalWeeklyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Total Weekly Threshold Color End--%>

                                    <%--Total Monthly Threshold Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalMonthlyLowColor" Text='<%#Eval("TotalMonthlyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalMonthlyMediumColor" Text='<%#Eval("TotalMonthlyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalMonthlyHighColor" Text='<%#Eval("TotalMonthlyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalMonthlyVeryHighColor" Text='<%#Eval("TotalMonthlyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Total Monthly Threshold Color End--%>

                                    <%--Total Yearly Threshold Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalYearlyLowColor" Text='<%#Eval("TotalYearlyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalYearlyMediumColor" Text='<%#Eval("TotalYearlyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalYearlyHighColor" Text='<%#Eval("TotalYearlyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalYearlyVeryHighColor" Text='<%#Eval("TotalYearlyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Total Yearly Threshold Color End--%>
                                    <%--Total Threshold Color End--%>
                                    
                                    <%--Total Ratio Threshold Color Start--%>
                                    <%--Total Ratio Daily Threshold Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioDailyLowColor" Text='<%#Eval("TotalRatioDailyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioDailyMediumColor" Text='<%#Eval("TotalRatioDailyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioDailyHighColor" Text='<%#Eval("TotalRatioDailyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioDailyVeryHighColor" Text='<%#Eval("TotalRatioDailyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									
                                    <%--Total Ratio Daily Threshold Color End--%>

                                    <%--Total Ratio Weekly Threshold Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioWeeklyLowColor" Text='<%#Eval("TotalRatioWeeklyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioWeeklyMediumColor" Text='<%#Eval("TotalRatioWeeklyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioWeeklyHighColor" Text='<%#Eval("TotalRatioWeeklyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioWeeklyVeryHighColor" Text='<%#Eval("TotalRatioWeeklyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Total Ratio Weekly Threshold Color End--%>

                                    <%--Total Ratio Monthly Threshold Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioMonthlyLowColor" Text='<%#Eval("TotalRatioMonthlyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioMonthlyMediumColor" Text='<%#Eval("TotalRatioMonthlyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioMonthlyHighColor" Text='<%#Eval("TotalRatioMonthlyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioMonthlyVeryHighColor" Text='<%#Eval("TotalRatioMonthlyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Total Ratio Monthly Threshold Color End--%>

                                    <%--Total Ratio Yearly Threshold Color Start--%>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioYearlyLowColor" Text='<%#Eval("TotalRatioYearlyLowColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioYearlyMediumColor" Text='<%#Eval("TotalRatioYearlyMediumColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioYearlyHighColor" Text='<%#Eval("TotalRatioYearlyHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalRatioYearlyVeryHighColor" Text='<%#Eval("TotalRatioYearlyVeryHighColor")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Total Ratio Yearly Threshold Color End--%>
                                    <%--Total Ratio Threshold Color End--%>


                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="DailyLowLabel" Text='<%#Eval("DailyLowLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="DailyMediumLabel" Text='<%#Eval("DailyMediumLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="DailyHighLabel" Text='<%#Eval("DailyHighLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="DailyVeryHighLabel" Text='<%#Eval("DailyVeryHighLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="WeeklyLowLabel" Text='<%#Eval("WeeklyLowLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="WeeklyMediumLabel" Text='<%#Eval("WeeklyMediumLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="WeeklyHighLabel" Text='<%#Eval("WeeklyHighLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="WeeklyVeryHighLabel" Text='<%#Eval("WeeklyVeryHighLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="MonthlyLowLabel" Text='<%#Eval("MonthlyLowLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="MonthlyMediumLabel" Text='<%#Eval("MonthlyMediumLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="MonthlyHighLabel" Text='<%#Eval("MonthlyHighLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="MonthlyVeryHighLabel" Text='<%#Eval("MonthlyVeryHighLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="YearlyLowLabel" Text='<%#Eval("YearlyLowLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="YearlyMediumLabel" Text='<%#Eval("YearlyMediumLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="YearlyHighLabel" Text='<%#Eval("YearlyHighLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="YearlyVeryHighLabel" Text='<%#Eval("YearlyVeryHighLabel")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="col-md-1"></div>
     
           </div>
            </div>
             </div>
    </div>

    <div aria-hidden="true" role="dialog" tabindex="-1" id="DeviceEventThresholdPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <asp:Label ID="lblHeading" Text="Device & Site Event Threshold" ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2 alignLeft">
                            <asp:Label ID="lblAccountID" ForeColor="Black" runat="server" class="control-label" Text="Account ID"></asp:Label>
                        </div>
                        <div class="col-md-3 alignLeft">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlAccountID" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 alignLeft">
                        </div>
                        <div class="col-md-2 alignLeft">
                            <asp:Label ID="lblDeviceType" ForeColor="Black" runat="server" class="control-label" Text="Device Type"></asp:Label>
                        </div>
                        <div class="col-md-3 alignLeft">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlDeviceType" class="form-control" runat="server"></asp:DropDownList>
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyVeryHighLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                      <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Device</legend>

                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label36" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label3" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label4" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label6" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label8" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDailyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                           </fieldset>
                                      <fieldset class="scheduler-border">
                                        <legend class="scheduler-border">Site</legend>
                                          
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label65" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label66" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label67" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label68" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalDailyLow" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalDailyMedium" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalDailyHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalDailyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label11" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalDailyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalDailyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalDailyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalDailyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                          </fieldset>
                                      <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Activity Ratio</legend>
                                          
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label81" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label82" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label83" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label84" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioDailyLow" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioDailyMedium" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioDailyHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioDailyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label85" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label86" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label87" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label88" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioDailyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioDailyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioDailyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioDailyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                          </fieldset>
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtWeeklyVeryHighLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <fieldset class="scheduler-border">
                                                <legend class="scheduler-border">Device</legend>
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
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                    
                                       
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label12" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label13" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label14" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label15" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row alignLeft">
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
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtWeeklyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                    </fieldset>
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Site</legend>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label69" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label70" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label71" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label72" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalWeeklyLow" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalWeeklyMedium" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalWeeklyHigh" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalWeeklyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label53" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label54" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label55" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label56" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalWeeklyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalWeeklyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalWeeklyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalWeeklyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                    </fieldset>
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Activity Ratio</legend>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label89" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label90" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label91" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label92" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalRatioWeeklyLow" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalRatioWeeklyMedium" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalRatioWeeklyHigh" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalRatioWeeklyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label93" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label94" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label95" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label96" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row alignLeft">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalRatioWeeklyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalRatioWeeklyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalRatioWeeklyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTotalRatioWeeklyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                    </fieldset>
                                    </div>
                                    <div id="viewMonthlytabid" class="tab-pane fade">
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label16" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label17" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label18" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label19" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyVeryHighLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Device</legend>
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label20" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label21" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label22" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label23" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtMonthlyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                            	</fieldset>
                                        <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Site</legend>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label73" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label74" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label75" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label76" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalMonthlyLow" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalMonthlyMedium" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalMonthlyHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalMonthlyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label57" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label58" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label59" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label60" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalMonthlyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalMonthlyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalMonthlyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalMonthlyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                                	</fieldset>
                                        <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Activity Ratio</legend>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label97" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label98" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label99" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label100" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioMonthlyLow" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioMonthlyMedium" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioMonthlyHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioMonthlyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label101" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label102" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label103" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label104" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioMonthlyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioMonthlyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioMonthlyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTotalRatioMonthlyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                                	</fieldset>
                                    </div>
                                    <div id="viewYearlytabid" class="tab-pane fade">
                                        <div class="row alignLeft">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label24" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label25" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label26" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label ID="Label27" ForeColor="Black" runat="server" class="control-label" Text="Label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row alignLeft">
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
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtYearlyVeryHighLabel" class="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                            <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Device</legend>
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
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label28" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label29" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label30" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label31" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row alignLeft">
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
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtYearlyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                        </fieldset>
                                            <fieldset class="scheduler-border">
                                                <legend class="scheduler-border">Site</legend>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label77" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label78" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label79" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label80" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalYearlyLow" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalYearlyMedium" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalYearlyHigh" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalYearlyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                              <div class="row alignLeft">
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
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label64" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalYearlyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalYearlyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalYearlyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalYearlyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                        </fieldset>
                                            <fieldset class="scheduler-border">
                                                <legend class="scheduler-border">Activity Ratio</legend>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label105" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label106" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label107" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label108" ForeColor="Black" runat="server" class="control-label" Text="Threshold"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalRatioYearlyLow" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalRatioYearlyMedium" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalRatioYearlyHigh" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalRatioYearlyVeryHigh" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                              <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label109" ForeColor="Black" runat="server" class="control-label" Text="Low Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label110" ForeColor="Black" runat="server" class="control-label" Text="Medium Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label111" ForeColor="Black" runat="server" class="control-label" Text="High Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label112" ForeColor="Black" runat="server" class="control-label" Text="Very High Color"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row alignLeft">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalRatioYearlyLowColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalRatioYearlyMediumColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalRatioYearlyHighColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtTotalRatioYearlyVeryHighColor" class="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                        </fieldset>
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
                            <asp:Button ID="btnSaveDeviceEventThreshold" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveDeviceEventThreshold_Click" OnClientClick="return ValidateDeviceEventThreshold()" />
                            <button id="Cancel" class="btn btn-default" onclick="onClickClose()">Cancel</button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
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

        $(document).ready(function () {
            SetTabs();
            setnumericcontrols();
        });

        function setnumericcontrols() {
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
            new AutoNumeric('#<%= txtDailyVeryHigh.ClientID%>', {
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
            new AutoNumeric('#<%= txtWeeklyVeryHigh.ClientID%>', {
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
            new AutoNumeric('#<%= txtMonthlyVeryHigh.ClientID%>', {
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
            new AutoNumeric('#<%= txtYearlyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            //Total Daily, Weekly, Monthly, Yearly Start

            //Daily
            new AutoNumeric('#<%= txtTotalDailyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalDailyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalDailyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalDailyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            //Weekly

            new AutoNumeric('#<%= txtTotalWeeklyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalWeeklyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalWeeklyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalWeeklyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            //Monthly

            new AutoNumeric('#<%= txtTotalMonthlyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalMonthlyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalMonthlyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalMonthlyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
         
            //Yearly

            new AutoNumeric('#<%= txtTotalYearlyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalYearlyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalYearlyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalYearlyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            //Total Daily, Weekly, Monthly, Yearly End

            //Total Ratio Daily, Weekly, Monthly, Yearly Start

            //Daily
            new AutoNumeric('#<%= txtTotalRatioDailyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioDailyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioDailyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioDailyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            //Weekly

            new AutoNumeric('#<%= txtTotalRatioWeeklyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioWeeklyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioWeeklyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioWeeklyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            //Monthly

            new AutoNumeric('#<%= txtTotalRatioMonthlyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioMonthlyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioMonthlyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioMonthlyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            //Yearly

            new AutoNumeric('#<%= txtTotalRatioYearlyLow.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioYearlyMedium.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioYearlyHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });
            new AutoNumeric('#<%= txtTotalRatioYearlyVeryHigh.ClientID%>', {
                decimalPlaces: '0',
                digitGroupSeparator: '',
            });

            //Total Ratio Daily, Weekly, Monthly, Yearly End

        }
        function alertpopup() {

            showpopup();
        }
        function OnClickFloorPlan() {
            showpopup();
        }

        function showpopup() {
            $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
            
            //ColorPicker Start

            //Daily
            $('#ContentPlaceHolder1_txtDailyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtDailyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtDailyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtDailyVeryHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalDailyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalDailyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalDailyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalDailyVeryHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioDailyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioDailyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioDailyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioDailyVeryHighColor').colorpicker({
                hideButton: true
            });


            //Weekly

            $('#ContentPlaceHolder1_txtWeeklyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtWeeklyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtWeeklyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtWeeklyVeryHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalWeeklyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalWeeklyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalWeeklyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalWeeklyVeryHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioWeeklyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioWeeklyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioWeeklyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioWeeklyVeryHighColor').colorpicker({
                hideButton: true
            });
            
            //Monthly

            $('#ContentPlaceHolder1_txtMonthlyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtMonthlyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtMonthlyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtMonthlyVeryHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalMonthlyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalMonthlyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalMonthlyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalMonthlyVeryHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioMonthlyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioMonthlyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioMonthlyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioMonthlyVeryHighColor').colorpicker({
                hideButton: true
            });

            //Yearly

            $('#ContentPlaceHolder1_txtYearlyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtYearlyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtYearlyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtYearlyVeryHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalYearlyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalYearlyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalYearlyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalYearlyVeryHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioYearlyLowColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioYearlyMediumColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioYearlyHighColor').colorpicker({
                hideButton: true
            });

            $('#ContentPlaceHolder1_txtTotalRatioYearlyVeryHighColor').colorpicker({
                hideButton: true
            });
            //ColorPicker End

            $('.modal-backdrop').hide();
            $('#DeviceEventThresholdPopup').modal('show');
        }

        function hidepopup() {
            alert("Device event threshold saved successfully.");
            $('.modal-backdrop').hide();
            $('#DeviceEventThresholdPopup').modal('hide');

        }
        function deletepopup() {
            alert("Device event threshold deleted successfully.");
            $('.modal-backdrop').hide();
            $('#DeviceEventThresholdPopup').modal('hide');

        }

        function onClickClose() {
            $('.modal-backdrop').hide();
            $('#DeviceEventThresholdPopup').modal('hide');
        }

        function ValidateDeviceEventThreshold() {

            //Daily Start
            if ($('#<%= txtDailyLow.ClientID%>').val() == '') {
                $("#<%= txtDailyLow.ClientID%>").attr("data-original-title", "Please enter Device Daily Low.");
                    $("#<%= txtDailyLow.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
                }
                $("#<%= txtDailyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtDailyMedium.ClientID%>').val() == '') {
                $("#<%= txtDailyMedium.ClientID%>").attr("data-original-title", "Please enter Device Daily Medium.");
                    $("#<%= txtDailyMedium.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
                }
                $("#<%= txtDailyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtDailyHigh.ClientID%>').val() == '') {
                $("#<%= txtDailyHigh.ClientID%>").attr("data-original-title", "Please enter Device Daily High.");
                    $("#<%= txtDailyHigh.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
                }
                $("#<%= txtDailyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtDailyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtDailyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Device Daily Very High.");
                    $("#<%= txtDailyVeryHigh.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
                }
            $("#<%= txtDailyVeryHigh.ClientID%>").tooltip('destroy');
            //Daily End
            //Daily Color Start
            if ($('#<%= txtDailyLowColor.ClientID%>').val() == '') {
                $("#<%= txtDailyLowColor.ClientID%>").attr("data-original-title", "Please enter Device Daily Low Color.");
                    $("#<%= txtDailyLowColor.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
                }
                $("#<%= txtDailyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtDailyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtDailyMediumColor.ClientID%>").attr("data-original-title", "Please enter Device Daily Medium Color.");
                    $("#<%= txtDailyMediumColor.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
                }
                $("#<%= txtDailyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtDailyHighColor.ClientID%>').val() == '') {
                $("#<%= txtDailyHighColor.ClientID%>").attr("data-original-title", "Please enter Device Daily High Color.");
                    $("#<%= txtDailyHighColor.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
                }
                $("#<%= txtDailyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtDailyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtDailyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Device Daily Very High Color.");
                    $("#<%= txtDailyVeryHighColor.ClientID%>").tooltip('show');
                    $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                    return false;
                }
            $("#<%= txtDailyVeryHighColor.ClientID%>").tooltip('destroy');
            //Daily Color End

            // Total Daily Start

            if ($('#<%= txtTotalDailyLow.ClientID%>').val() == '') {
                $("#<%= txtTotalDailyLow.ClientID%>").attr("data-original-title", "Please enter Site Daily Low.");
                $("#<%= txtTotalDailyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalDailyMedium.ClientID%>').val() == '') {
                $("#<%= txtTotalDailyMedium.ClientID%>").attr("data-original-title", "Please enter Site Daily Medium.");
                $("#<%= txtTotalDailyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalDailyHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalDailyHigh.ClientID%>").attr("data-original-title", "Please enter Site Daily High.");
                $("#<%= txtTotalDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalDailyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalDailyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Site Daily Very High.");
                $("#<%= txtTotalDailyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyVeryHigh.ClientID%>").tooltip('destroy');

            //Total Daily End
            //Total Daily Color Start
            if ($('#<%= txtTotalDailyLowColor.ClientID%>').val() == '') {
                $("#<%= txtTotalDailyLowColor.ClientID%>").attr("data-original-title", "Please enter Site Daily Low Color.");
                $("#<%= txtTotalDailyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalDailyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtTotalDailyMediumColor.ClientID%>").attr("data-original-title", "Please enter Site Daily Medium Color.");
                $("#<%= txtTotalDailyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalDailyHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalDailyHighColor.ClientID%>").attr("data-original-title", "Please enter Site Daily High Color.");
                $("#<%= txtTotalDailyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalDailyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalDailyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Site Daily Very High Color.");
                $("#<%= txtTotalDailyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyVeryHighColor.ClientID%>").tooltip('destroy');
            //Total Daily Color End


            //Total Ratio Start

            if ($('#<%= txtTotalRatioDailyLow.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioDailyLow.ClientID%>").attr("data-original-title", "Please enter Activity Ratio  Daily Low.");
                $("#<%= txtTotalRatioDailyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioDailyMedium.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioDailyMedium.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Daily Medium.");
                $("#<%= txtTotalRatioDailyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioDailyHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioDailyHigh.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Daily High.");
                $("#<%= txtTotalRatioDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioDailyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioDailyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Daily Very High.");
                $("#<%= txtTotalRatioDailyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyVeryHigh.ClientID%>").tooltip('destroy');
            //Total Ratio End

            //Total Ratio Color Start
            if ($('#<%= txtTotalRatioDailyLowColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioDailyLowColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Daily Low Color.");
                $("#<%= txtTotalRatioDailyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioDailyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioDailyMediumColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Daily Medium Color.");
                $("#<%= txtTotalRatioDailyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioDailyHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioDailyHighColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Daily High Color.");
                $("#<%= txtTotalRatioDailyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioDailyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioDailyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Daily Very High Color.");
                $("#<%= txtTotalRatioDailyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyVeryHighColor.ClientID%>").tooltip('destroy');
            //Total Ratio Color End
            
            //Daily end

            //Weekly Start
            if ($('#<%= txtWeeklyLow.ClientID%>').val() == '') {
                $("#<%= txtWeeklyLow.ClientID%>").attr("data-original-title", "Please enter Device Weekly Low.");
                $("#<%= txtWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtWeeklyMedium.ClientID%>').val() == '') {
                $("#<%= txtWeeklyMedium.ClientID%>").attr("data-original-title", "Please enter Device Weekly Medium.");
                $("#<%= txtWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtWeeklyHigh.ClientID%>').val() == '') {
                $("#<%= txtWeeklyHigh.ClientID%>").attr("data-original-title", "Please enter Device Weekly High.");
                $("#<%= txtWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtWeeklyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtWeeklyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Device Weekly Very High.");
                $("#<%= txtWeeklyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyVeryHigh.ClientID%>").tooltip('destroy');

            //Weekly End
            //Weekly Color Start
            if ($('#<%= txtWeeklyLowColor.ClientID%>').val() == '') {
                $("#<%= txtWeeklyLowColor.ClientID%>").attr("data-original-title", "Please enter Device Weekly Low Color.");
                $("#<%= txtWeeklyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtWeeklyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtWeeklyMediumColor.ClientID%>").attr("data-original-title", "Please enter Device Weekly Medium Color.");
                $("#<%= txtWeeklyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtWeeklyHighColor.ClientID%>').val() == '') {
                $("#<%= txtWeeklyHighColor.ClientID%>").attr("data-original-title", "Please enter Device Weekly High Color.");
                $("#<%= txtWeeklyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtWeeklyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtWeeklyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Device Weekly Very High Color.");
                $("#<%= txtWeeklyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyVeryHighColor.ClientID%>").tooltip('destroy');
            //Weekly Color End

            //Total Weekly Start

            if ($('#<%= txtTotalWeeklyLow.ClientID%>').val() == '') {
                $("#<%= txtTotalWeeklyLow.ClientID%>").attr("data-original-title", "Please enter Site Weekly Low.");
                $("#<%= txtTotalWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalWeeklyMedium.ClientID%>').val() == '') {
                $("#<%= txtTotalWeeklyMedium.ClientID%>").attr("data-original-title", "Please enter Site Weekly Medium.");
                $("#<%= txtTotalWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalWeeklyHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalWeeklyHigh.ClientID%>").attr("data-original-title", "Please enter Site Weekly High.");
                $("#<%= txtTotalWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalWeeklyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalWeeklyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Site Weekly Very High.");
                $("#<%= txtTotalWeeklyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyVeryHigh.ClientID%>").tooltip('destroy');

            //Total Weekly Color Start
            if ($('#<%= txtTotalWeeklyLowColor.ClientID%>').val() == '') {
                $("#<%= txtTotalWeeklyLowColor.ClientID%>").attr("data-original-title", "Please enter Site Weekly Low Color.");
                $("#<%= txtTotalWeeklyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalWeeklyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtTotalWeeklyMediumColor.ClientID%>").attr("data-original-title", "Please enter Site Weekly Medium Color.");
                $("#<%= txtTotalWeeklyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalWeeklyHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalWeeklyHighColor.ClientID%>").attr("data-original-title", "Please enter Site Weekly High Color.");
                $("#<%= txtTotalWeeklyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalWeeklyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalWeeklyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Site Weekly Very High Color.");
                $("#<%= txtTotalWeeklyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyVeryHighColor.ClientID%>").tooltip('destroy');
            //Total Weekly Color End

            //Total Weekly End

            //Total Ratio Weekly Start

            if ($('#<%= txtTotalRatioWeeklyLow.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioWeeklyLow.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Weekly Low.");
                $("#<%= txtTotalRatioWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioWeeklyMedium.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Weekly Medium.");
                $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioWeeklyHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Weekly High.");
                $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioWeeklyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioWeeklyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Weekly Very High.");
                $("#<%= txtTotalRatioWeeklyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyVeryHigh.ClientID%>").tooltip('destroy');

            //Total Ratio Weekly Color Start
            if ($('#<%= txtTotalRatioWeeklyLowColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioWeeklyLowColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Weekly Low Color.");
                $("#<%= txtTotalRatioWeeklyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioWeeklyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioWeeklyMediumColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Weekly Medium Color.");
                $("#<%= txtTotalRatioWeeklyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioWeeklyHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioWeeklyHighColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Weekly High Color.");
                $("#<%= txtTotalRatioWeeklyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioWeeklyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioWeeklyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Weekly Very High Color.");
                $("#<%= txtTotalRatioWeeklyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyVeryHighColor.ClientID%>").tooltip('destroy');
            //Total Ratio Weekly Color End

            //Total Ration Weekly End

            //Weekly end

            //Monthly Start
            if ($('#<%= txtMonthlyLow.ClientID%>').val() == '') {
                $("#<%= txtMonthlyLow.ClientID%>").attr("data-original-title", "Please enter Device Monthly Low.");
                $("#<%= txtMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtMonthlyMedium.ClientID%>').val() == '') {
                $("#<%= txtMonthlyMedium.ClientID%>").attr("data-original-title", "Please enter Device Monthly Medium.");
                $("#<%= txtMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtMonthlyHigh.ClientID%>').val() == '') {
                $("#<%= txtMonthlyHigh.ClientID%>").attr("data-original-title", "Please enter Device Monthly High.");
                $("#<%= txtMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtMonthlyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtMonthlyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Device Monthly Very High.");
                $("#<%= txtMonthlyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyVeryHigh.ClientID%>").tooltip('destroy');

            //Monthly End

            //Monthly Color Start
            if ($('#<%= txtMonthlyLowColor.ClientID%>').val() == '') {
                $("#<%= txtMonthlyLowColor.ClientID%>").attr("data-original-title", "Please enter Device Monthly Low Color.");
                $("#<%= txtMonthlyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtMonthlyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtMonthlyMediumColor.ClientID%>").attr("data-original-title", "Please enter Device Monthly Medium Color.");
                $("#<%= txtMonthlyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtMonthlyHighColor.ClientID%>').val() == '') {
                $("#<%= txtMonthlyHighColor.ClientID%>").attr("data-original-title", "Please enter Device Monthly High Color.");
                $("#<%= txtMonthlyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtMonthlyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtMonthlyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Device Monthly Very High Color.");
                $("#<%= txtMonthlyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyVeryHighColor.ClientID%>").tooltip('destroy');

            //Monthly Color End

            //Total Monthly Start

            if ($('#<%= txtTotalMonthlyLow.ClientID%>').val() == '') {
                $("#<%= txtTotalMonthlyLow.ClientID%>").attr("data-original-title", "Please enter Site Monthly Low.");
                $("#<%= txtTotalMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalMonthlyMedium.ClientID%>').val() == '') {
                $("#<%= txtTotalMonthlyMedium.ClientID%>").attr("data-original-title", "Please enter Site Monthly Medium.");
                $("#<%= txtTotalMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalMonthlyHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalMonthlyHigh.ClientID%>").attr("data-original-title", "Please enter Site Monthly High.");
                $("#<%= txtTotalMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalMonthlyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalMonthlyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Site Monthly Very High.");
                $("#<%= txtTotalMonthlyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyVeryHigh.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalMonthlyLowColor.ClientID%>').val() == '') {
                $("#<%= txtTotalMonthlyLowColor.ClientID%>").attr("data-original-title", "Please enter Site Monthly Low Color.");
                $("#<%= txtTotalMonthlyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalMonthlyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtTotalMonthlyMediumColor.ClientID%>").attr("data-original-title", "Please enter Site Monthly Medium Color.");
                $("#<%= txtTotalMonthlyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalMonthlyHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalMonthlyHighColor.ClientID%>").attr("data-original-title", "Please enter Site Monthly High Color.");
                $("#<%= txtTotalMonthlyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalMonthlyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalMonthlyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Site Monthly Very High Color.");
                $("#<%= txtTotalMonthlyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyVeryHighColor.ClientID%>").tooltip('destroy');

            //Total Monthly End

            //Total Ratio Monthly Start

            if ($('#<%= txtTotalRatioMonthlyLow.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioMonthlyLow.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Monthly Low.");
                $("#<%= txtTotalRatioMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioMonthlyMedium.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Monthly Medium.");
                $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioMonthlyHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Monthly High.");
                $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioMonthlyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioMonthlyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Monthly Very High.");
                $("#<%= txtTotalRatioMonthlyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyVeryHigh.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioMonthlyLowColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioMonthlyLowColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Monthly Low Color.");
                $("#<%= txtTotalRatioMonthlyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioMonthlyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioMonthlyMediumColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Monthly Medium Color.");
                $("#<%= txtTotalRatioMonthlyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioMonthlyHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioMonthlyHighColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Monthly High Color.");
                $("#<%= txtTotalRatioMonthlyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioMonthlyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioMonthlyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Monthly Very High Color.");
                $("#<%= txtTotalRatioMonthlyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyVeryHighColor.ClientID%>").tooltip('destroy');

            //Total Ratio Monthly End

            //Monthly end

            //Yearly Start
            if ($('#<%= txtYearlyLow.ClientID%>').val() == '') {
                $("#<%= txtYearlyLow.ClientID%>").attr("data-original-title", "Please enter Device Yearly Low.");
                $("#<%= txtYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtYearlyMedium.ClientID%>').val() == '') {
                $("#<%= txtYearlyMedium.ClientID%>").attr("data-original-title", "Please enter Device Yearly Medium.");
                $("#<%= txtYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtYearlyHigh.ClientID%>').val() == '') {
                $("#<%= txtYearlyHigh.ClientID%>").attr("data-original-title", "Please enter Device Yearly High.");
                $("#<%= txtYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtYearlyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtYearlyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Device Yearly Very High.");
                $("#<%= txtYearlyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyVeryHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtYearlyLowColor.ClientID%>').val() == '') {
                $("#<%= txtYearlyLowColor.ClientID%>").attr("data-original-title", "Please enter Device Yearly Low Color.");
                $("#<%= txtYearlyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtYearlyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtYearlyMediumColor.ClientID%>").attr("data-original-title", "Please enter Device Yearly Medium Color.");
                $("#<%= txtYearlyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtYearlyHighColor.ClientID%>').val() == '') {
                $("#<%= txtYearlyHighColor.ClientID%>").attr("data-original-title", "Please enter Device Yearly High Color.");
                $("#<%= txtYearlyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtYearlyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtYearlyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Device Yearly Very High Color.");
                $("#<%= txtYearlyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyVeryHighColor.ClientID%>").tooltip('destroy');


            //Total Yearly Start

            if ($('#<%= txtTotalYearlyLow.ClientID%>').val() == '') {
                $("#<%= txtTotalYearlyLow.ClientID%>").attr("data-original-title", "Please enter Site Yearly Low.");
                $("#<%= txtTotalYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalYearlyMedium.ClientID%>').val() == '') {
                $("#<%= txtTotalYearlyMedium.ClientID%>").attr("data-original-title", "Please enter Site Yearly Medium.");
                $("#<%= txtTotalYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalYearlyHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalYearlyHigh.ClientID%>").attr("data-original-title", "Please enter Site Yearly High.");
                $("#<%= txtTotalYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalYearlyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalYearlyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Sitet Yearly Very High.");
                $("#<%= txtTotalYearlyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyVeryHigh.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalYearlyLowColor.ClientID%>').val() == '') {
                $("#<%= txtTotalYearlyLowColor.ClientID%>").attr("data-original-title", "Please enter Site Low Color.");
                $("#<%= txtTotalYearlyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalYearlyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtTotalYearlyMediumColor.ClientID%>").attr("data-original-title", "Please enter Site Medium Color.");
                $("#<%= txtTotalYearlyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalYearlyHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalYearlyHighColor.ClientID%>").attr("data-original-title", "Please enter Site High Color.");
                $("#<%= txtTotalYearlyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalYearlyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalYearlyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Site Very High Color.");
                $("#<%= txtTotalYearlyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyVeryHighColor.ClientID%>").tooltip('destroy');

            //Total Yearly End

            //Total Ratio Yearly Start

            if ($('#<%= txtTotalRatioYearlyLow.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioYearlyLow.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Yearly Low.");
                $("#<%= txtTotalRatioYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyLow.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioYearlyMedium.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioYearlyMedium.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Yearly Medium.");
                $("#<%= txtTotalRatioYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyMedium.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioYearlyHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioYearlyHigh.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Yearly High.");
                $("#<%= txtTotalRatioYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyHigh.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioYearlyVeryHigh.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioYearlyVeryHigh.ClientID%>").attr("data-original-title", "Please enter Activity Ratiot Yearly Very High.");
                $("#<%= txtTotalRatioYearlyVeryHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyVeryHigh.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioYearlyLowColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioYearlyLowColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Low Color.");
                $("#<%= txtTotalRatioYearlyLowColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyLowColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioYearlyMediumColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioYearlyMediumColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Medium Color.");
                $("#<%= txtTotalRatioYearlyMediumColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyMediumColor.ClientID%>").tooltip('destroy');


            if ($('#<%= txtTotalRatioYearlyHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioYearlyHighColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio High Color.");
                $("#<%= txtTotalRatioYearlyHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyHighColor.ClientID%>").tooltip('destroy');

            if ($('#<%= txtTotalRatioYearlyVeryHighColor.ClientID%>').val() == '') {
                $("#<%= txtTotalRatioYearlyVeryHighColor.ClientID%>").attr("data-original-title", "Please enter Activity Ratio Very High Color.");
                $("#<%= txtTotalRatioYearlyVeryHighColor.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyVeryHighColor.ClientID%>").tooltip('destroy');

            //Total Ratio Yearly End

            //Yearly end


            //Daily Start

            var Dailylow = parseInt($('#<%= txtDailyLow.ClientID%>').val());
            var DailyMedium =parseInt( $('#<%= txtDailyMedium.ClientID%>').val());
            var DailyHigh = parseInt($('#<%= txtDailyHigh.ClientID%>').val());
            var DailyVeryHigh = parseInt($('#<%= txtDailyVeryHigh.ClientID%>').val());

            if (Dailylow > DailyMedium) {
                $("#<%= txtDailyLow.ClientID%>").attr("data-original-title", "Device Daily Low Value cannot be greater than Device Daily Medium.");
                $("#<%= txtDailyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyLow.ClientID%>").tooltip('destroy');


            if (DailyMedium > DailyHigh) {
                $("#<%= txtDailyMedium.ClientID%>").attr("data-original-title", "Device Daily Medium Value cannot be greater than Device Daily High.");
                $("#<%= txtDailyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyMedium.ClientID%>").tooltip('destroy');

            if (DailyVeryHigh < DailyHigh) {
                $("#<%= txtDailyHigh.ClientID%>").attr("data-original-title", "Device Daily High Value cannot be greater than Device Daily Very High.");
                $("#<%= txtDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyHigh.ClientID%>").tooltip('destroy');

            //Total Daily Start

            var TotalDailyLow = parseInt($('#<%= txtTotalDailyLow.ClientID%>').val());
            var TotalDailyMedium = parseInt($('#<%= txtTotalDailyMedium.ClientID%>').val());
            var TotalDailyHigh = parseInt($('#<%= txtTotalDailyHigh.ClientID%>').val());
            var TotalDailyVeryHigh = parseInt($('#<%= txtTotalDailyVeryHigh.ClientID%>').val());

            if (TotalDailyLow > TotalDailyMedium) {
                $("#<%= txtTotalDailyLow.ClientID%>").attr("data-original-title", "Site Daily Low Value cannot be greater than Site  Daily Medium.");
                $("#<%= txtTotalDailyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyLow.ClientID%>").tooltip('destroy');


            if (TotalDailyMedium > TotalDailyHigh) {
                $("#<%= txtTotalDailyMedium.ClientID%>").attr("data-original-title", "Site  Daily Medium Value cannot be greater than Site  Daily High.");
                $("#<%= txtTotalDailyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyMedium.ClientID%>").tooltip('destroy');

            if (TotalDailyVeryHigh < TotalDailyHigh) {
                $("#<%= txtTotalDailyHigh.ClientID%>").attr("data-original-title", "Site  Daily High Value cannot be greater than Site  Daily Very High.");
                $("#<%= txtTotalDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyHigh.ClientID%>").tooltip('destroy');

            //Total Daily End
            
            //Total Ratio Start

            var TotalRatioDailyLow = parseInt($('#<%= txtTotalRatioDailyLow.ClientID%>').val());
            var TotalRatioDailyMedium = parseInt($('#<%= txtTotalRatioDailyMedium.ClientID%>').val());
            var TotalRatioDailyHigh = parseInt($('#<%= txtTotalRatioDailyHigh.ClientID%>').val());
            var TotalRatioDailyVeryHigh = parseInt($('#<%= txtTotalRatioDailyVeryHigh.ClientID%>').val());

            if (TotalRatioDailyLow > TotalRatioDailyMedium) {
                $("#<%= txtTotalRatioDailyLow.ClientID%>").attr("data-original-title", "Activity Ratio Daily Low Value cannot be greater than Activity Ratio Daily Medium.");
                $("#<%= txtTotalRatioDailyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyLow.ClientID%>").tooltip('destroy');


            if (TotalRatioDailyMedium > TotalRatioDailyHigh) {
                $("#<%= txtTotalRatioDailyMedium.ClientID%>").attr("data-original-title", "Activity Ratio Daily Medium Value cannot be greater than Activity Ratio Daily High.");
                $("#<%= txtTotalRatioDailyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyMedium.ClientID%>").tooltip('destroy');

            if (TotalRatioDailyVeryHigh < TotalRatioDailyHigh) {
                $("#<%= txtTotalRatioDailyHigh.ClientID%>").attr("data-original-title", "Activity Ratio Daily High Value cannot be greater than Activity Ratio Daily Very High.");
                $("#<%= txtTotalRatioDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyHigh.ClientID%>").tooltip('destroy');

            //Total Ratio End

            //Daily end

            //Weekly Start

            var Weeklylow = parseInt($('#<%= txtWeeklyLow.ClientID%>').val());
            var WeeklyMedium = parseInt($('#<%= txtWeeklyMedium.ClientID%>').val());
            var WeeklyHigh = parseInt($('#<%= txtWeeklyHigh.ClientID%>').val());
            var WeeklyVeryHigh = parseInt($('#<%= txtWeeklyVeryHigh.ClientID%>').val());

            if (Weeklylow > WeeklyMedium) {
                $("#<%= txtWeeklyLow.ClientID%>").attr("data-original-title", "Device Weekly Low Value cannot be greater than Device Weekly Medium.");
                $("#<%= txtWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyLow.ClientID%>").tooltip('destroy');


            if (WeeklyMedium > WeeklyHigh) {
                $("#<%= txtWeeklyMedium.ClientID%>").attr("data-original-title", "Device Weekly Medium Value cannot be greater than Device Weekly High.");
                $("#<%= txtWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyMedium.ClientID%>").tooltip('destroy');

            if (WeeklyVeryHigh < WeeklyHigh) {
                $("#<%= txtWeeklyHigh.ClientID%>").attr("data-original-title", "Device Weekly High Value cannot be greater than Device Weekly Very High.");
                $("#<%= txtWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyHigh.ClientID%>").tooltip('destroy');

            //Total weekly Start

            var TotalWeeklyLow = parseInt($('#<%= txtTotalWeeklyLow.ClientID%>').val());
            var TotalWeeklyMedium = parseInt($('#<%= txtTotalWeeklyMedium.ClientID%>').val());
            var TotalWeeklyHigh = parseInt($('#<%= txtTotalWeeklyHigh.ClientID%>').val());
            var TotalWeeklyVeryHigh = parseInt($('#<%= txtTotalWeeklyVeryHigh.ClientID%>').val());

            if (TotalWeeklyLow > TotalWeeklyMedium) {
                $("#<%= txtTotalWeeklyLow.ClientID%>").attr("data-original-title", "Site  Weekly Low Value cannot be greater than Site  Weekly Medium.");
                $("#<%= txtTotalWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyLow.ClientID%>").tooltip('destroy');


            if (TotalWeeklyMedium > TotalWeeklyHigh) {
                $("#<%= txtTotalWeeklyMedium.ClientID%>").attr("data-original-title", "Site  Weekly Medium Value cannot be greater than Site  Weekly High.");
                $("#<%= txtTotalWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyMedium.ClientID%>").tooltip('destroy');

            if (TotalWeeklyVeryHigh < TotalWeeklyHigh) {
                $("#<%= txtTotalWeeklyHigh.ClientID%>").attr("data-original-title", "Site  Weekly High Value cannot be greater than Site  Weekly Very High.");
                $("#<%= txtTotalWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyHigh.ClientID%>").tooltip('destroy');

            //Total Weekly End

            //Total Ratio Weekly Start

            var TotalRatioWeeklyLow = parseInt($('#<%= txtTotalRatioWeeklyLow.ClientID%>').val());
            var TotalRatioWeeklyMedium = parseInt($('#<%= txtTotalRatioWeeklyMedium.ClientID%>').val());
            var TotalRatioWeeklyHigh = parseInt($('#<%= txtTotalRatioWeeklyHigh.ClientID%>').val());
            var TotalRatioWeeklyVeryHigh = parseInt($('#<%= txtTotalRatioWeeklyVeryHigh.ClientID%>').val());

            if (TotalRatioWeeklyLow > TotalRatioWeeklyMedium) {
                $("#<%= txtTotalRatioWeeklyLow.ClientID%>").attr("data-original-title", "Activity Ratio Weekly Low Value cannot be greater than Activity Ratio Weekly Medium.");
                $("#<%= txtTotalRatioWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyLow.ClientID%>").tooltip('destroy');


            if (TotalRatioWeeklyMedium > TotalRatioWeeklyHigh) {
                $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").attr("data-original-title", "Activity Ratio Weekly Medium Value cannot be greater than Activity Ratio Weekly High.");
                $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").tooltip('destroy');

            if (TotalRatioWeeklyVeryHigh < TotalRatioWeeklyHigh) {
                $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").attr("data-original-title", "Activity Ratio Weekly High Value cannot be greater than Activity Ratio Weekly Very High.");
                $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").tooltip('destroy');

            //Total Ratio Weekly End

            //Weekly end

            //Monthly Start

            var Monthlylow = parseInt($('#<%= txtMonthlyLow.ClientID%>').val());
            var MonthlyMedium = parseInt($('#<%= txtMonthlyMedium.ClientID%>').val());
            var MonthlyHigh = parseInt($('#<%= txtMonthlyHigh.ClientID%>').val());
            var MonthlyVeryHigh = parseInt($('#<%= txtMonthlyVeryHigh.ClientID%>').val());

            if (Monthlylow > MonthlyMedium) {
                $("#<%= txtMonthlyLow.ClientID%>").attr("data-original-title", "Device Monthly Low Value cannot be greater than Device Monthly Medium.");
                $("#<%= txtMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyLow.ClientID%>").tooltip('destroy');


            if (MonthlyMedium > MonthlyHigh) {
                $("#<%= txtMonthlyMedium.ClientID%>").attr("data-original-title", "Device Monthly Medium Value cannot be greater than Device Monthly High.");
                $("#<%= txtMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyMedium.ClientID%>").tooltip('destroy');

            if (MonthlyVeryHigh < MonthlyHigh) {
                $("#<%= txtMonthlyHigh.ClientID%>").attr("data-original-title", "Device Monthly High Value cannot be greater than Device Monthly Very High.");
                $("#<%= txtMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyHigh.ClientID%>").tooltip('destroy');

            //Total Monthly Start

            var TotalMonthlyLow = parseInt($('#<%= txtTotalMonthlyLow.ClientID%>').val());
            var TotalMonthlyMedium = parseInt($('#<%= txtTotalMonthlyMedium.ClientID%>').val());
            var TotalMonthlyHigh = parseInt($('#<%= txtTotalMonthlyHigh.ClientID%>').val());
            var TotalMonthlyVeryHigh = parseInt($('#<%= txtTotalMonthlyVeryHigh.ClientID%>').val());

            if (TotalMonthlyLow > TotalMonthlyMedium) {
                $("#<%= txtTotalMonthlyLow.ClientID%>").attr("data-original-title", "Site Monthly Low Value cannot be greater than Site Monthly Medium.");
                $("#<%= txtTotalMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyLow.ClientID%>").tooltip('destroy');


            if (TotalMonthlyMedium > TotalMonthlyHigh) {
                $("#<%= txtTotalMonthlyMedium.ClientID%>").attr("data-original-title", "Site  Monthly Medium Value cannot be greater than Site  Monthly High.");
                $("#<%= txtTotalMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyMedium.ClientID%>").tooltip('destroy');

            if (TotalMonthlyVeryHigh < TotalMonthlyHigh) {
                $("#<%= txtTotalMonthlyHigh.ClientID%>").attr("data-original-title", "Site  Monthly High Value cannot be greater than Site  Monthly Very High.");
                $("#<%= txtTotalMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyHigh.ClientID%>").tooltip('destroy');

            //Total Monthly End

            //Total Ratio Monthly Start

            var TotalRatioMonthlyLow = parseInt($('#<%= txtTotalRatioMonthlyLow.ClientID%>').val());
            var TotalRatioMonthlyMedium = parseInt($('#<%= txtTotalRatioMonthlyMedium.ClientID%>').val());
            var TotalRatioMonthlyHigh = parseInt($('#<%= txtTotalRatioMonthlyHigh.ClientID%>').val());
            var TotalRatioMonthlyVeryHigh = parseInt($('#<%= txtTotalRatioMonthlyVeryHigh.ClientID%>').val());

            if (TotalRatioMonthlyLow > TotalRatioMonthlyMedium) {
                $("#<%= txtTotalRatioMonthlyLow.ClientID%>").attr("data-original-title", "Activity Ratio Monthly Low Value cannot be greater than Activity Ratio Monthly Medium.");
                $("#<%= txtTotalRatioMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyLow.ClientID%>").tooltip('destroy');


            if (TotalRatioMonthlyMedium > TotalRatioMonthlyHigh) {
                $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").attr("data-original-title", "Activity Ratio Monthly Medium Value cannot be greater than Activity Ratio Monthly High.");
                $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").tooltip('destroy');

            if (TotalRatioMonthlyVeryHigh < TotalRatioMonthlyHigh) {
                $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").attr("data-original-title", "Activity Ratio Monthly High Value cannot be greater than Activity Ratio Monthly Very High.");
                $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").tooltip('destroy');

            //Total Ratio Monthly End

            //Monthly end

            //Yearly Start


            var Yearlylow = parseInt($('#<%= txtYearlyLow.ClientID%>').val());
            var YearlyMedium = parseInt($('#<%= txtYearlyMedium.ClientID%>').val());
            var YearlyHigh = parseInt($('#<%= txtYearlyHigh.ClientID%>').val());
            var YearlyVeryHigh = parseInt($('#<%= txtYearlyVeryHigh.ClientID%>').val());

            if (Yearlylow > YearlyMedium) {
                $("#<%= txtYearlyLow.ClientID%>").attr("data-original-title", "Device Yearly Low Value cannot be greater than Device Yearly Medium.");
                $("#<%= txtYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyLow.ClientID%>").tooltip('destroy');


            if (YearlyMedium > YearlyHigh) {
                $("#<%= txtYearlyMedium.ClientID%>").attr("data-original-title", "Device Yearly Medium Value cannot be greater than Device Yearly High.");
                $("#<%= txtYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyMedium.ClientID%>").tooltip('destroy');

            if (YearlyVeryHigh < YearlyHigh) {
                $("#<%= txtYearlyHigh.ClientID%>").attr("data-original-title", "Device Yearly High Value cannot be greater than Device Yearly Very High.");
                $("#<%= txtYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyHigh.ClientID%>").tooltip('destroy');

            //Total Yearly Start

            var TotalYearlyLow = parseInt($('#<%= txtTotalYearlyLow.ClientID%>').val());
            var TotalYearlyMedium = parseInt($('#<%= txtTotalYearlyMedium.ClientID%>').val());
            var TotalYearlyHigh = parseInt($('#<%= txtTotalYearlyHigh.ClientID%>').val());
            var TotalYearlyVeryHigh = parseInt($('#<%= txtTotalYearlyVeryHigh.ClientID%>').val());

            if (TotalYearlyLow > TotalYearlyMedium) {
                $("#<%= txtTotalYearlyLow.ClientID%>").attr("data-original-title", "Site Yearly Low Value cannot be greater than Site Yearly Medium.");
                $("#<%= txtTotalYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyLow.ClientID%>").tooltip('destroy');


            if (TotalYearlyMedium > TotalYearlyHigh) {
                $("#<%= txtTotalYearlyMedium.ClientID%>").attr("data-original-title", "Site Yearly Medium Value cannot be greater than Site Yearly High.");
                $("#<%= txtTotalYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyMedium.ClientID%>").tooltip('destroy');

            if (TotalYearlyVeryHigh < TotalYearlyHigh) {
                $("#<%= txtTotalYearlyHigh.ClientID%>").attr("data-original-title", "Site Yearly High Value cannot be greater than Site yearly Very High.");
                $("#<%= txtTotalYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyHigh.ClientID%>").tooltip('destroy');

            //Total Yearly End

            //Total Ratio Yearly Start

            var TotalRatioYearlyLow = parseInt($('#<%= txtTotalRatioYearlyLow.ClientID%>').val());
            var TotalRatioYearlyMedium = parseInt($('#<%= txtTotalRatioYearlyMedium.ClientID%>').val());
            var TotalRatioYearlyHigh = parseInt($('#<%= txtTotalRatioYearlyHigh.ClientID%>').val());
            var TotalRatioYearlyVeryHigh = parseInt($('#<%= txtTotalRatioYearlyVeryHigh.ClientID%>').val());

            if (TotalRatioYearlyLow > TotalRatioYearlyMedium) {
                $("#<%= txtTotalRatioYearlyLow.ClientID%>").attr("data-original-title", "Activity Ratio Yearly Low Value cannot be greater than Activity Ratio Yearly Medium.");
                $("#<%= txtTotalRatioYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyLow.ClientID%>").tooltip('destroy');


            if (TotalRatioYearlyMedium > TotalRatioYearlyHigh) {
                $("#<%= txtTotalRatioYearlyMedium.ClientID%>").attr("data-original-title", "Activity Ratio Yearly Medium Value cannot be greater than Activity Ratio Yearly High.");
                $("#<%= txtTotalRatioYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyMedium.ClientID%>").tooltip('destroy');

            if (TotalRatioYearlyVeryHigh < TotalRatioYearlyHigh) {
                $("#<%= txtTotalRatioYearlyHigh.ClientID%>").attr("data-original-title", "Activity Ratio Yearly High Value cannot be greater than Activity Ratio yearly Very High.");
                $("#<%= txtTotalRatioYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyHigh.ClientID%>").tooltip('destroy');

            //Total Ratio Yearly End

            //Yearly End

                return true;
        }


        function ValidateDeviceEventThreshold111() {

            //Daily Start

            var Dailylow = $('#<%= txtDailyLow.ClientID%>').val();
            var DailyMedium = $('#<%= txtDailyMedium.ClientID%>').val();
            var DailyHigh = $('#<%= txtDailyHigh.ClientID%>').val();
            var DailyVeryHigh = $('#<%= txtDailyVeryHigh.ClientID%>').val();

            if (DailyVeryHigh < DailyHigh) {
                $("#<%= txtDailyHigh.ClientID%>").attr("data-original-title", "Device Daily High Value cannot be greater than Device Daily Very High.");
                $("#<%= txtDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtDailyHigh.ClientID%>").tooltip('destroy');


            if (DailyHigh < DailyMedium) {
                $("#<%= txtDailyMedium.ClientID%>").attr("data-original-title", "Device Daily Medium Value cannot be greater than Device Daily High.");
                 $("#<%= txtDailyMedium.ClientID%>").tooltip('show');
                 $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                 return false;
             }
             $("#<%= txtDailyMedium.ClientID%>").tooltip('destroy');

            if (DailyMedium < Dailylow) {
                $("#<%= txtDailyLow.ClientID%>").attr("data-original-title", "Device Daily Low Value cannot be greater than Device Daily Medium.");
                   $("#<%= txtDailyLow.ClientID%>").tooltip('show');
                   $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                   return false;
               }
            $("#<%= txtDailyLow.ClientID%>").tooltip('destroy');

            //Total Daily Start

            var TotalDailyLow = parseInt($('#<%= txtTotalDailyLow.ClientID%>').val());
            var TotalDailyMedium = parseInt($('#<%= txtTotalDailyMedium.ClientID%>').val());
            var TotalDailyHigh = parseInt($('#<%= txtTotalDailyHigh.ClientID%>').val());
            var TotalDailyVeryHigh = parseInt($('#<%= txtTotalDailyVeryHigh.ClientID%>').val());

            if (TotalDailyVeryHigh < TotalDailyHigh) {
                $("#<%= txtTotalDailyHigh.ClientID%>").attr("data-original-title", "Site Daily High Value cannot be greater than Site  Daily Very High.");
                $("#<%= txtTotalDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyHigh.ClientID%>").tooltip('destroy');


            if (TotalDailyHigh < TotalDailyMedium) {
                $("#<%= txtTotalDailyMedium.ClientID%>").attr("data-original-title", "Site  Daily Medium Value cannot be greater than Site  Daily High.");
                $("#<%= txtTotalDailyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyMedium.ClientID%>").tooltip('destroy');

            if (TotalDailyMedium < TotalDailyLow) {
                $("#<%= txtTotalDailyLow.ClientID%>").attr("data-original-title", "Site  Daily Low Value cannot be greater than Site  Daily Medium.");
                $("#<%= txtTotalDailyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalDailyLow.ClientID%>").tooltip('destroy');
            
            //Total Daily End

            //Total Ratio Daily Start

            var TotalRatioDailyLow = parseInt($('#<%= txtTotalRatioDailyLow.ClientID%>').val());
            var TotalRatioDailyMedium = parseInt($('#<%= txtTotalRatioDailyMedium.ClientID%>').val());
            var TotalRatioDailyHigh = parseInt($('#<%= txtTotalRatioDailyHigh.ClientID%>').val());
            var TotalRatioDailyVeryHigh = parseInt($('#<%= txtTotalRatioDailyVeryHigh.ClientID%>').val());

            if (TotalRatioDailyVeryHigh < TotalRatioDailyHigh) {
                $("#<%= txtTotalRatioDailyHigh.ClientID%>").attr("data-original-title", "Activity Ratio Daily High Value cannot be greater than Activity Ratio Daily Very High.");
                $("#<%= txtTotalRatioDailyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyHigh.ClientID%>").tooltip('destroy');


            if (TotalRatioDailyHigh < TotalRatioDailyMedium) {
                $("#<%= txtTotalRatioDailyMedium.ClientID%>").attr("data-original-title", "Activity Ratio Daily Medium Value cannot be greater than Activity Ratio Daily High.");
                $("#<%= txtTotalRatioDailyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyMedium.ClientID%>").tooltip('destroy');

            if (TotalRatioDailyMedium < TotalRatioDailyLow) {
                $("#<%= txtTotalRatioDailyLow.ClientID%>").attr("data-original-title", "Activity Ratio Daily Low Value cannot be greater than Activity Ratio Daily Medium.");
                $("#<%= txtTotalRatioDailyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewDailytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioDailyLow.ClientID%>").tooltip('destroy');

            //Total Ratio Daily End

             //Daily end

            //Weekly Start

            var Weeklylow = $('#<%= txtWeeklyLow.ClientID%>').val();
            var WeeklyMedium = $('#<%= txtWeeklyMedium.ClientID%>').val();
            var WeeklyHigh = $('#<%= txtWeeklyHigh.ClientID%>').val();
            var WeeklyVeryHigh = $('#<%= txtWeeklyVeryHigh.ClientID%>').val();

            if (WeeklyVeryHigh < WeeklyHigh) {
                $("#<%= txtWeeklyHigh.ClientID%>").attr("data-original-title", "Device Weekly High Value cannot be greater than Device Weekly Very High.");
                $("#<%= txtWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyHigh.ClientID%>").tooltip('destroy');


            if (WeeklyHigh < WeeklyMedium) {
                $("#<%= txtWeeklyMedium.ClientID%>").attr("data-original-title", "Device Weekly Medium Value cannot be greater than Device Weekly High.");
                $("#<%= txtWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyMedium.ClientID%>").tooltip('destroy');

            if (WeeklyMedium < Weeklylow) {
                $("#<%= txtWeeklyLow.ClientID%>").attr("data-original-title", "Device Weekly Low Value cannot be greater than Device Weekly Medium.");
                $("#<%= txtWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtWeeklyLow.ClientID%>").tooltip('destroy');
         
            //Total Weekly Start

            var TotalWeeklyLow = parseInt($('#<%= txtTotalWeeklyLow.ClientID%>').val());
            var TotalWeeklyMedium = parseInt($('#<%= txtTotalWeeklyMedium.ClientID%>').val());
            var TotalWeeklyHigh = parseInt($('#<%= txtTotalWeeklyHigh.ClientID%>').val());
            var TotalWeeklyVeryHigh = parseInt($('#<%= txtTotalWeeklyVeryHigh.ClientID%>').val());

            if (TotalWeeklyVeryHigh < TotalWeeklyHigh) {
                $("#<%= txtTotalWeeklyHigh.ClientID%>").attr("data-original-title", "Site  Weekly High Value cannot be greater than Site  Weekly Very High.");
                $("#<%= txtTotalWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyHigh.ClientID%>").tooltip('destroy');


            if (TotalWeeklyHigh < TotalWeeklyMedium) {
                $("#<%= txtTotalWeeklyMedium.ClientID%>").attr("data-original-title", "Site  Weekly Medium Value cannot be greater than Site  Weekly High.");
                $("#<%= txtTotalWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyMedium.ClientID%>").tooltip('destroy');

            if (TotalWeeklyMedium < TotalWeeklyLow) {
                $("#<%= txtTotalWeeklyLow.ClientID%>").attr("data-original-title", "Site  Weekly Low Value cannot be greater than Site  Weekly Medium.");
                $("#<%= txtTotalWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalWeeklyLow.ClientID%>").tooltip('destroy');

            //Total Weekly End

            //Total Ratio Weekly Start

            var TotalRatioWeeklyLow = parseInt($('#<%= txtTotalRatioWeeklyLow.ClientID%>').val());
            var TotalRatioWeeklyMedium = parseInt($('#<%= txtTotalRatioWeeklyMedium.ClientID%>').val());
            var TotalRatioWeeklyHigh = parseInt($('#<%= txtTotalRatioWeeklyHigh.ClientID%>').val());
            var TotalRatioWeeklyVeryHigh = parseInt($('#<%= txtTotalRatioWeeklyVeryHigh.ClientID%>').val());

            if (TotalRatioWeeklyVeryHigh < TotalRatioWeeklyHigh) {
                $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").attr("data-original-title", "Site  Weekly High Value cannot be greater than Site  Weekly Very High.");
                $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyHigh.ClientID%>").tooltip('destroy');


            if (TotalRatioWeeklyHigh < TotalRatioWeeklyMedium) {
                $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").attr("data-original-title", "Site  Weekly Medium Value cannot be greater than Site  Weekly High.");
                $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyMedium.ClientID%>").tooltip('destroy');

            if (TotalRatioWeeklyMedium < TotalRatioWeeklyLow) {
                $("#<%= txtTotalRatioWeeklyLow.ClientID%>").attr("data-original-title", "Site  Weekly Low Value cannot be greater than Site  Weekly Medium.");
                $("#<%= txtTotalRatioWeeklyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewWeeklytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioWeeklyLow.ClientID%>").tooltip('destroy');

            //Total Ratio Weekly End

            //Weekly end

            //Monthly Start

            var Monthlylow = $('#<%= txtMonthlyLow.ClientID%>').val();
            var MonthlyMedium = $('#<%= txtMonthlyMedium.ClientID%>').val();
            var MonthlyHigh = $('#<%= txtMonthlyHigh.ClientID%>').val();
            var MonthlyVeryHigh = $('#<%= txtMonthlyVeryHigh.ClientID%>').val();

            if (MonthlyVeryHigh < MonthlyHigh) {
                $("#<%= txtMonthlyHigh.ClientID%>").attr("data-original-title", "Device Monthly High Value cannot be greater than Device Monthly Very High.");
                $("#<%= txtMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyHigh.ClientID%>").tooltip('destroy');


            if (MonthlyHigh < MonthlyMedium) {
                $("#<%= txtMonthlyMedium.ClientID%>").attr("data-original-title", "Device Monthly Medium Value cannot be greater than Device Monthly High.");
                $("#<%= txtMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyMedium.ClientID%>").tooltip('destroy');

            if (MonthlyMedium < Monthlylow) {
                $("#<%= txtMonthlyLow.ClientID%>").attr("data-original-title", "Device Monthly Low Value cannot be greater than Device Monthly Medium.");
                $("#<%= txtMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtMonthlyLow.ClientID%>").tooltip('destroy');
         
            //Total Monthly Start

            var TotalMonthlyLow = parseInt($('#<%= txtTotalMonthlyLow.ClientID%>').val());
            var TotalMonthlyMedium = parseInt($('#<%= txtTotalMonthlyMedium.ClientID%>').val());
            var TotalMonthlyHigh = parseInt($('#<%= txtTotalMonthlyHigh.ClientID%>').val());
            var TotalMonthlyVeryHigh = parseInt($('#<%= txtTotalMonthlyVeryHigh.ClientID%>').val());

            if (TotalMonthlyVeryHigh < TotalMonthlyHigh) {
                $("#<%= txtTotalMonthlyHigh.ClientID%>").attr("data-original-title", "Site  Monthly High Value cannot be greater than Site  Monthly Very High.");
                $("#<%= txtTotalMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyHigh.ClientID%>").tooltip('destroy');


            if (TotalMonthlyHigh < TotalMonthlyMedium) {
                $("#<%= txtTotalMonthlyMedium.ClientID%>").attr("data-original-title", "Site  Monthly Medium Value cannot be greater than Site  Monthly High.");
                $("#<%= txtTotalMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyMedium.ClientID%>").tooltip('destroy');

            if (TotalMonthlyMedium < TotalMonthlyLow) {
                $("#<%= txtTotalMonthlyLow.ClientID%>").attr("data-original-title", "Site  Monthly Low Value cannot be greater than Site  Monthly Medium.");
                $("#<%= txtTotalMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalMonthlyLow.ClientID%>").tooltip('destroy');

            //Total Monthly End

            //Total Ratio Monthly Start

            var TotalRatioMonthlyLow = parseInt($('#<%= txtTotalRatioMonthlyLow.ClientID%>').val());
            var TotalRatioMonthlyMedium = parseInt($('#<%= txtTotalRatioMonthlyMedium.ClientID%>').val());
            var TotalRatioMonthlyHigh = parseInt($('#<%= txtTotalRatioMonthlyHigh.ClientID%>').val());
            var TotalRatioMonthlyVeryHigh = parseInt($('#<%= txtTotalRatioMonthlyVeryHigh.ClientID%>').val());

            if (TotalRatioMonthlyVeryHigh < TotalRatioMonthlyHigh) {
                $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").attr("data-original-title", "Activity Ratio Monthly High Value cannot be greater than Activity Ratio Monthly Very High.");
                $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyHigh.ClientID%>").tooltip('destroy');


            if (TotalRatioMonthlyHigh < TotalRatioMonthlyMedium) {
                $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").attr("data-original-title", "Activity Ratio Monthly Medium Value cannot be greater than Activity Ratio Monthly High.");
                $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyMedium.ClientID%>").tooltip('destroy');

            if (TotalRatioMonthlyMedium < TotalRatioMonthlyLow) {
                $("#<%= txtTotalRatioMonthlyLow.ClientID%>").attr("data-original-title", "Activity Ratio Monthly Low Value cannot be greater than Activity Ratio Monthly Medium.");
                $("#<%= txtTotalRatioMonthlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewMonthlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioMonthlyLow.ClientID%>").tooltip('destroy');

            //Total Ratio Monthly End

             //Monthly end

            //Yearly Start

            var Yearlylow = $('#<%= txtYearlyLow.ClientID%>').val();
            var YearlyMedium = $('#<%= txtYearlyMedium.ClientID%>').val();
            var YearlyHigh = $('#<%= txtYearlyHigh.ClientID%>').val();
            var YearlyVeryHigh = $('#<%= txtYearlyVeryHigh.ClientID%>').val();

            if (YearlyVeryHigh < YearlyHigh) {
                $("#<%= txtYearlyHigh.ClientID%>").attr("data-original-title", "Device Yearly High Value cannot be greater than Device Yearly Very High.");
                $("#<%= txtYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyHigh.ClientID%>").tooltip('destroy');


            if (YearlyHigh < YearlyMedium) {
                $("#<%= txtYearlyMedium.ClientID%>").attr("data-original-title", "Device Yearly Medium Value cannot be greater than Device Yearly High.");
                $("#<%= txtYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyMedium.ClientID%>").tooltip('destroy');

            if (YearlyMedium < Yearlylow) {
                $("#<%= txtYearlyLow.ClientID%>").attr("data-original-title", "Device Yearly Low Value cannot be greater than Device Yearly Medium.");
                $("#<%= txtYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtYearlyLow.ClientID%>").tooltip('destroy');

            //Total Yearly Start

            var TotalYearlyLow = parseInt($('#<%= txtTotalYearlyLow.ClientID%>').val());
            var TotalYearlyMedium = parseInt($('#<%= txtTotalYearlyMedium.ClientID%>').val());
            var TotalYearlyHigh = parseInt($('#<%= txtTotalYearlyHigh.ClientID%>').val());
            var TotalYearlyVeryHigh = parseInt($('#<%= txtTotalYearlyVeryHigh.ClientID%>').val());

            if (TotalYearlyVeryHigh < TotalYearlyHigh) {
                $("#<%= txtTotalYearlyHigh.ClientID%>").attr("data-original-title", "Site Yearly High Value cannot be greater than Site Yearly Very High.");
                $("#<%= txtTotalYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyHigh.ClientID%>").tooltip('destroy');


            if (TotalYearlyHigh < TotalYearlyMedium) {
                $("#<%= txtTotalYearlyMedium.ClientID%>").attr("data-original-title", "Site Yearly Medium Value cannot be greater than Site Yearly High.");
                $("#<%= txtTotalYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyMedium.ClientID%>").tooltip('destroy');

            if (TotalYearlyMedium < TotalYearlyLow) {
                $("#<%= txtTotalYearlyLow.ClientID%>").attr("data-original-title", "Site Yearly Low Value cannot be greater than Site Yearly Medium.");
                $("#<%= txtTotalYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalYearlyLow.ClientID%>").tooltip('destroy');

            //Total Yearly End

            //Total Ratio Yearly Start

            var TotalRatioYearlyLow = parseInt($('#<%= txtTotalRatioYearlyLow.ClientID%>').val());
            var TotalRatioYearlyMedium = parseInt($('#<%= txtTotalRatioYearlyMedium.ClientID%>').val());
            var TotalRatioYearlyHigh = parseInt($('#<%= txtTotalRatioYearlyHigh.ClientID%>').val());
            var TotalRatioYearlyVeryHigh = parseInt($('#<%= txtTotalRatioYearlyVeryHigh.ClientID%>').val());

            if (TotalRatioYearlyVeryHigh < TotalRatioYearlyHigh) {
                $("#<%= txtTotalRatioYearlyHigh.ClientID%>").attr("data-original-title", "Activity Ratio Yearly High Value cannot be greater than Activity Ratio Yearly Very High.");
                $("#<%= txtTotalRatioYearlyHigh.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyHigh.ClientID%>").tooltip('destroy');


            if (TotalRatioYearlyHigh < TotalRatioYearlyMedium) {
                $("#<%= txtTotalRatioYearlyMedium.ClientID%>").attr("data-original-title", "Activity Ratio Yearly Medium Value cannot be greater than Activity Ratio Yearly High.");
                $("#<%= txtTotalRatioYearlyMedium.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyMedium.ClientID%>").tooltip('destroy');

            if (TotalRatioYearlyMedium < TotalRatioYearlyLow) {
                $("#<%= txtTotalRatioYearlyLow.ClientID%>").attr("data-original-title", "Activity Ratio Yearly Low Value cannot be greater than Activity Ratio Yearly Medium.");
                $("#<%= txtTotalRatioYearlyLow.ClientID%>").tooltip('show');
                $('#Tabs a[data-target="#viewYearlytabid"]').tab('show');
                return false;
            }
            $("#<%= txtTotalRatioYearlyLow.ClientID%>").tooltip('destroy');

            //Total Ratio Yearly End

             //Yearly end

             return true;
         }
    function onClickClearImage() {

    }

    function previewFile() {

    }
    </script>
</asp:Content>

