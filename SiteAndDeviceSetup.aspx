<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="SiteAndDeviceSetup.aspx.vb" Inherits="SiteAndDeviceSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Style/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .disabledTab{
    pointer-events: none;
}
          .enabledTab{
    pointer-events: visible;
}
          .centeralign {
        text-align: center !important;
    }
        
        .checkbox1 {
            text-align: left;
        }

            .checkbox1 label {
                margin-left: 5px;
            }

        .legend {
            list-style: none;
            color: black;
        }

            .legend li {
                float: left;
                margin-right: 10px;
            }

            .legend span {
                border: 1px solid #ccc;
                float: left;
                width: 12px;
                height: 12px;
                margin: 2px;
            }

        .alignLeft {
            text-align: left !important;
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

        .modal-header .devicedetails {
            float: right;
        }

        .modal-header .close {
            position: absolute;
            right: 19px;
            font-size: 33px;
            top: 8px;
        }

        .devicedetails {
            font-size: 21px;
            font-weight: bold;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #fff;
            filter: alpha(opacity=20);
            /*opacity: .2;*/
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

        .leftalign {
            left: -98px;
        }

        th {
    text-align: center !important;
}

    
    </style>

    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
         <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddCustomer" />
             <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>

        <ContentTemplate>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
                <ControlBundles>
                    <asp:ControlBundle Name="CalendarExtender_Bundle" />
                    <asp:ControlBundle Name="ModalPopupExtender_Bundle" />
                    <asp:ControlBundle Name="ListSearchExtender_Bundle" />
                    <asp:ControlBundle Name="TabContainer_Bundle" />
                    <asp:ControlBundle Name="CollapsiblePanelExtender_Bundle" />
                    <asp:ControlBundle Name="MaskedEditExtender_Bundle" />

                </ControlBundles>
            </asp:ToolkitScriptManager>
            <div class="row" id="idMainblock" style="margin: 14px;">

     <asp:HiddenField ID="hiddenCustomerID" runat="server" />
    <asp:HiddenField ID="hiddenSelectedTab" value="" runat="server" />

                <div class="col-xs-1 col-md-1"></div>
                <div class="col-xs-10 col-md-10">

                    <div class="row">
                        <div class="col-lg-12 col-md-12" style="height: 20px"></div>
                    </div>

                    <div class="row" style="color: black;">
                        <div class="col-md-12 col-xs-12">
                            <div id="Tabs">
                                <ul id="tabsJustified" class="nav nav-tabs">
                                    <li class="nav-item"><a href="" id="idtabviewCustomer" data-target="#viewCustomer" data-toggle="tab" class="nav-link small text-uppercase active" onclick="return onClickSelectedTab(this,'viewCustomer')">Customer</a></li>
                                    <li onclick="onclickSiteTab()"  class="nav-item"><a href="" id="idtabviewSite" data-target="#viewSite" data-toggle="tab" class="nav-link small text-uppercase" onclick="return onClickSelectedTab(this,'viewSite')">Site</a></li>
                                </ul>
                                <br>
                                <div id="tabsJustifiedContent" class="tab-content">
                                    <div id="viewCustomer" class="tab-pane fade" style="text-align: left;">
                                        <div class="row" >
                                            <div class="col-md-1 col-xs-6">
                                                <asp:Button ID="btnAddCustomer" CssClass="btn btn-sm btn-primary" runat="server" Text="ADD"  OnClick="btnAddCustomer_Click" />
                                            </div>
                                            <div class="col-md-1 col-xs-6">
                                         <%--       <asp:Button ID="btnEditCustomer" CssClass="btn btn-sm btn-primary" runat="server" Text="EDIT"  OnClick="btnEditCustomer_Click" />--%>
                                            </div>
                                            <div class="col-md-7" >
                                            </div>
                                            <div class="col-md-3 col-xs-6">
                                                <asp:TextBox ID="txtSearchCustomer" AutoPostBack="true" placeholder="Search Here" class="form-control textbox" OnTextChanged="txtSearchCustomer_TextChanged"  runat="server"> </asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="row" >
                                                <div class="col-md-12 col-xs-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridCustomer" runat="server" CssClass="table table-bordered table" AutoGenerateColumns="false" HeaderStyle-ForeColor="Black" Style="max-width: 1500px;text-align:center" ShowHeaderWhenEmpty="False" EmptyDataText="No records Found">
                                                            <Columns>
                                                          <%--      <asp:HyperLinkField
                                                                    DataNavigateUrlFields="id"
                                                                 Text="Select" HeaderStyle-Width="100px" ControlStyle-Width ="100px" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" />
--%>
                                                                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSelect" CssClass="centeralign" style="text-align:center" runat="server" Text="Select" OnClick="lnkSelect_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" style="text-align:center" runat="server" Text="Edit" ></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="centerHeaderText" ItemStyle-HorizontalAlign="center" Visible ="false" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblid" runat="server"
                                                                            Text='<%#Eval("id")%>' ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                   <asp:TemplateField HeaderText="Customer ID" HeaderStyle-CssClass="centerHeaderText" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblexternalCustomerId" runat="server"
                                                                            Text='<%#Eval("externalCustomerId")%>' ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="centerHeaderText" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="158px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcustomerName" runat="server"
                                                                            Text='<%#Eval("customerName")%>' ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Country" HeaderStyle-CssClass="centerHeaderText" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="150px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcountryName" runat="server"
                                                                            Text='<%#Eval("countryName")%>' ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CountryID" HeaderStyle-CssClass="centerHeaderText" ItemStyle-HorizontalAlign="center" Visible="false" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcountryId" runat="server"
                                                                            Text='<%#Eval("countryId")%>' ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                                                
                                                               
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                          </div>

                                    </div>

                                    <div id="viewSite" style="text-align:left" class="tab-pane fade">

                                        <div class="row" style="color: black;">
                                            <div class="col-md-1 col-xs-12">
                                                 <asp:Label ID="lblcustid" ForeColor="Black" runat="server" class="control-label" Text="Customer ID:"></asp:Label>
                                            </div>
                                            <div class="col-md-3 col-xs-12">
                                                <asp:Label ID="idlblCustomerID" runat="server"  class="control-label" Text=""></asp:Label>
                                            </div>
                                              <div class="col-md-4 col-xs-12">
                                            </div>
                                            <div class="col-md-1 col-xs-12">
                                              <asp:Label ID="lblCustName" runat="server" class="control-label" Text="Name:"></asp:Label>
                                            </div>
                                            <div class="col-md-3 col-xs-12">
                                                <asp:Label ID="idlblCustName" runat="server" class="control-label" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row"><div class="col-md-12 col-xs-12" style="height: 20px"></div></div>  
                                         <div class="row" >
                                            <div class="col-md-1 col-xs-6">
                                                <asp:Button ID="btnAddSite" CssClass="btn btn-sm btn-primary" runat="server" Text="ADD"  OnClick="btnAddSite_Click" />
                                            </div>
                                             <div class="col-md-1 col-xs-6">
                                                <asp:Button ID="btnImportSite" CssClass="btn btn-sm btn-primary" runat="server" Text="IMPORT"  OnClick="btnImportSite_Click" OnClientClick="return ConfirmImport();" />
                                            </div>
                                            <div class="col-md-6" >
                                            </div>
                                            <div class="col-md-4 col-xs-6">
                                                <asp:TextBox ID="txtSearchSite" AutoPostBack="true" placeholder="Search Here" class="form-control textbox" OnTextChanged="txtSearchSite_TextChanged"  runat="server"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row"><div class="col-md-12 col-xs-12" style="height: 2px"></div></div>
                                         <div class="row" >
                                                <div class="col-md-12 col-xs-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridSite" runat="server" CssClass="table table-bordered table" AutoGenerateColumns="false" HeaderStyle-ForeColor="Black" Style="max-width: 1500px;text-align:center"  ShowHeaderWhenEmpty="False" EmptyDataText="No records Found">
                                                            <Columns>
                                                           
                                                                      <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEditSite" runat="server" Text="Edit" style="text-align:center" ></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Site ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblid" runat="server"
                                                                            Text='<%#Eval("id")%>' ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Site ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="150px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExternalSiteId" runat="server"
                                                                            Text='<%#Eval("ExternalSiteId")%>' ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="350px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsiteName" runat="server"
                                                                            Text='<%#Eval("SiteName")%>' ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="350px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAddress" runat="server"
                                                                            Text='<%#Eval("Street")%>' ForeColor="Black"></asp:Label>
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

                </div>
                <div class="col-xs-1 col-md-1"></div>
            </div>
            </div>
        <div aria-hidden="true" role="dialog" tabindex="-1" id="CustomerPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <asp:Label ID="lblPopupHeading" Text="Customer" ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"> </asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div id="snoAlertBox" class="alert alert-success" data-alert="alert" style="display: none"></div>
                        <div id="ErrorMessage" class="alert alert-success" data-alert="alert" style="display: none"></div>
                    </div>

                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblCustomerID" ForeColor="Black" runat="server" class="control-label" Text="Customer ID"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:TextBox ID="txtCustomerID" class="form-control"  runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row"><div class="col-md-12 col-xs-12" style="height: 2px"></div></div>
                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblName" ForeColor="Black" runat="server" class="control-label" Text="Name"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:TextBox ID="txtCustomerName" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                  <div class="row"><div class="col-md-12 col-xs-12" style="height: 2px"></div></div>
                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblCountry" ForeColor="Black" runat="server" class="control-label" Text="Country"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-8 col-xs-12 col-sm-12 alignLeft">
                       <asp:DropDownList ID="ddlCountry" ForeColor="black" class="form-control"  runat="server" ></asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveCustomer" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveCustomer_Click" OnClientClick="return ValidateCustomer();" />
                    <button id="btnCancel" class="btn btn-default"  onclick="return onClickCloseCustomer();">Cancel</button>
                </div>
            </div>
        </div>
    </div>

               <div aria-hidden="true" role="dialog" tabindex="-1" id="SitePopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <asp:Label ID="lblSitePopupHeading" Text="Site" ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"> </asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div id="snoSiteAlertBox" class="alert alert-success" data-alert="alert" style="display: none"></div>
                        <div id="SiteErrorMessage" class="alert alert-success" data-alert="alert" style="display: none"></div>
                    </div>

                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblBranch" ForeColor="Black" runat="server" class="control-label" Text="Branch"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                   <asp:DropDownList ID="ddlBranch" ForeColor="black" class="form-control"  runat="server" ></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                            <div class="row"><div class="col-md-12 col-xs-12" style="height: 2px"></div></div>
                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblExternalSiteID" ForeColor="Black" runat="server" class="control-label" Text="Site ID"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:TextBox ID="txtExternalSiteID" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row"><div class="col-md-12 col-xs-12" style="height: 2px"></div></div>
                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblSiteName" ForeColor="Black" runat="server" class="control-label" Text="Site Name"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:TextBox ID="txtSiteName" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
             
                  <div class="row"><div class="col-md-12 col-xs-12" style="height: 2px"></div></div>
                          <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblStreetAddress" ForeColor="Black" runat="server" class="control-label" Text="Street Address"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:TextBox ID="txtStreetAddress" class="form-control" runat="server" Rows="6"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblZipCode" ForeColor="Black" runat="server" class="control-label" Text="Zip Code"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                   <asp:DropDownList ID="ddlZipCode" ForeColor="black" class="form-control"  runat="server" ></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblState" ForeColor="Black" runat="server" class="control-label" Text="State"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:TextBox ID="txtState" class="form-control" runat="server"></asp:TextBox>
                                   <%--<asp:DropDownList ID="ddlState" ForeColor="black" class="form-control"  runat="server" ></asp:DropDownList>--%>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-3 col-xs-12 alignLeft">
                            <div class="form-group">
                                <asp:Label ID="lblCity" ForeColor="Black" runat="server" class="control-label" Text="City"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8 col-xs-12 alignLeft">
                            <div class="form-group">
                                   <asp:DropDownList ID="ddlCity" ForeColor="black" class="form-control"  runat="server" ></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveSite" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSaveSite_Click" OnClientClick="return ValidateSite();" />
                    <button id="btnCancelSite" class="btn btn-default" onclick="return onClickCloseSite();">Cancel</button>
                </div>
            </div>
        </div>
    </div>

            <div aria-hidden="true" role="dialog" tabindex="-1" id="ImportExcelPopup" class="modal" data-backdrop="static" data-keyboard="false" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4>
                        <asp:Label ID="Label1" Text="Customer" ForeColor="Black" runat="server" class="modal-title alignLeft" Style="padding-left: 10px;"> </asp:Label></h4>
                </div>
                <div class="modal-body">
            
                    <div class="row">
                        <div class="col-md-2 col-xs-12 alignLeft">
                               <input id="fileUpload" type="file" name="file" onchange="return onchangeImportExcel();" runat="server" accept=".xls,.xlsx" />
                            <%--<input id="fileUpload" type="file" name="file" runat="server" accept=".xls,.xlsx" />--%>
                        </div>

                          <div class="col-md-9 col-xs-12">
                                            <label style="color:black;font-weight: 300!important;font-size: 12px;" id="lblSelectedFileName"></label>
                                        </div>
                    </div>
                    <div class="row"><div class="col-md-12 col-xs-12" style="height: 2px"></div></div>
                
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="Upload"  OnClick ="btnUpload_Click" OnClientClick ="return ValidateUploadFile()"/>
                    <button id="btnClose" class="btn btn-default" onclick="return closeFileupload();">Close</button>
                </div>
            </div>
        </div>
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" id="startTime">

        $(document).ready(function () {
            SetTabs();
            $("#idtabviewSite").addClass("disabledTab");
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetTabs();
                }
            });
        };

        function onClickSelectedTab(e, arg) {
            var custID = $('#ContentPlaceHolder1_hiddenCustomerID').val();
            if (custID == "" && arg != "viewCustomer") {
                document.getElementById("ContentPlaceHolder1_hiddenSelectedTab").value = "viewCustomer";
                $("#idtabviewSite").addClass("disabledTab");
                return false;
            }
            else {
                document.getElementById("ContentPlaceHolder1_hiddenSelectedTab").value = arg;
                $("#idtabviewSite").removeClass("disabledTab");
            }
            return false;
        }

        function SetTabs() {

            var defaulttab = "";
            defaulttab = "viewCustomer";
            var tabName = ($("[id*=TabName]").val() != "" && $("[id*=TabName]").val() != undefined) ? $("[id*=TabName]").val() : defaulttab;

            var selectedTab =  document.getElementById("ContentPlaceHolder1_hiddenSelectedTab").value;
            if (selectedTab != "")
            {
                tabName = selectedTab;
            }

            var custid = $('#ContentPlaceHolder1_hiddenCustomerID').val();
            if (custid == "") {
                tabName = "viewCustomer";
                $("#idtabviewSite").addClass("disabledTab");
            }

            $('#Tabs a[data-target="#' + tabName + '"]').tab('show');
          };

        function warningpopup(arg)
        {
            alert(arg);
        }
        
        function onClickCloseCustomer() {
            $('.modal-backdrop').hide();
            $("body").removeClass("modal-open");
            $('#CustomerPopup').modal('hide');
            return false;
        }


        function showCustomerpopup() {
            $('.modal-backdrop').hide();
            $('#CustomerPopup').modal('show');
        }

        function ValidateCustomer() {
            if ($('#<%= txtCustomerID.ClientID%>').val() == '') {
                $("#<%= txtCustomerID.ClientID%>").attr("data-original-title", "Please enter External customer ID.");
                $("#<%= txtCustomerID.ClientID%>").tooltip('show');
                return false;
            }
            $("#<%= txtCustomerID .ClientID%>").tooltip('destroy');

            if ($('#<%= txtCustomerName.ClientID%>').val() == '') {
                $("#<%= txtCustomerName.ClientID%>").attr("data-original-title", "Please enter Customer Name.");
                   $("#<%= txtCustomerName.ClientID%>").tooltip('show');
                   return false;
               }
            $("#<%= txtCustomerName .ClientID%>").tooltip('destroy');

            if ($('#ContentPlaceHolder1_ddlCountry').val() == null || $('#ContentPlaceHolder1_ddlCountry').val() == '' || $('#ContentPlaceHolder1_ddlCountry').val() == '0') {
                alert("Please select the country");
                return false;
            }

            return true;
        }

        function successpopup(arg)
        {
            alert(arg);
        }
        // Site Start
        function onClickCloseSite() {
            $('.modal-backdrop').hide();
            $("body").removeClass("modal-open");
            $('#SitePopup').modal('hide');
            return false;
        }

        function closeCustomerpopup(arg) {
            alert(arg);
            if (arg == "Customer Saved Successfully") {
                onClickCloseCustomer();
            }
            else {
                showCustomerpopup();
                return false;
            }
        }
        function closeSitepopup(arg) {
            alert(arg);
            if (arg == "Site Saved Successfully") {
                onClickCloseSite();
            }
            else {
                showSitePopup();
                return false;
            }
        }
        function ValidateSite() {

            if ($('#ContentPlaceHolder1_ddlBranch').val() == null || $('#ContentPlaceHolder1_ddlBranch').val() == ''|| $('#ContentPlaceHolder1_ddlBranch').val() == '0') {
                alert("Please select the Branch");
                return false;
            }

            if ($('#<%= txtExternalSiteID.ClientID%>').val() == '') {
                $("#<%= txtExternalSiteID.ClientID%>").attr("data-original-title", "Please enter External Site ID.");
                $("#<%= txtExternalSiteID.ClientID%>").tooltip('show');
                return false;
            }
            $("#<%= txtExternalSiteID.ClientID%>").tooltip('destroy');

          if ($('#<%= txtSiteName.ClientID%>').val() == '') {
                $("#<%= txtSiteName.ClientID%>").attr("data-original-title", "Please enter Site Name.");
                $("#<%= txtSiteName.ClientID%>").tooltip('show');
                return false;
            }
            $("#<%= txtSiteName .ClientID%>").tooltip('destroy');

            if ($('#<%= txtStreetAddress.ClientID%>').val() == '') {
                $("#<%= txtStreetAddress.ClientID%>").attr("data-original-title", "Please enter street address.");
                $("#<%= txtStreetAddress.ClientID%>").tooltip('show');
                return false;
            }
            $("#<%= txtStreetAddress.ClientID%>").tooltip('destroy');

            if ($('#ContentPlaceHolder1_ddlZipCode').val() == null || $('#ContentPlaceHolder1_ddlZipCode').val() == ''|| $('#ContentPlaceHolder1_ddlZipCode').val() == '0') {
                alert("Please select the Zipcode");
                return false;
            }

            if ($('#ContentPlaceHolder1_ddlCity').val() == null || $('#ContentPlaceHolder1_ddlCity').val() == ''|| $('#ContentPlaceHolder1_ddlCity').val() == '0') {
                alert("Please select the City");
                return false;
            }

            return true;
        }
        // Site End
        
        function showSiteTab() {

            var custID = $('#ContentPlaceHolder1_hiddenCustomerID').val();
            if (custID == "") {
                $("#idtabviewSite").addClass("disabledTab");
            }
            else {

                $("#idtabviewSite").removeClass("disabledTab");
            }
            document.getElementById("ContentPlaceHolder1_hiddenSelectedTab").value = "viewSite";
        }

        function showSitePopup() {
            $('.modal-backdrop').hide();
            $('#SitePopup').modal('show');
        }

        function ConfirmImport() {

            var custID = $('#ContentPlaceHolder1_idlblCustomerID').text();
            var r = confirm("Do you wish to import Site records for Customer ID: " + custID + " ?");
            if (r) {
                return true;
            }
            return false;
        }

        function ValidateUploadFile() {
            var file = $('#<%= fileUpload.ClientID%>').val();
               var filename = file.substring(file.lastIndexOf('\\') + 1);
               if (filename == "") {
                   alert("Please Select a File to Upload");
                   return false;
               }
        }

        function showImportExcelPopup() {

            $('.modal-backdrop').hide();
            $('#ImportExcelPopup').modal('show');
        }

        function warninguploadpopup(arg) {
            alert(arg);
            closeFileupload();
        }

        function closeFileupload() {
            $('.modal-backdrop').hide();
            $("body").removeClass("modal-open");
            $('#ImportExcelPopup').modal('hide');
            return false;
        }


        function onchangeImportExcel(e) {
            console.log("onchangeImportExcel");

            var file = $('#<%= fileUpload.ClientID%>').val();
            var filename = file.substring(file.lastIndexOf('\\') + 1);

            console.log("Input change fileName", filename);
            $("#lblSelectedFileName").text(filename);
            return false;
        }

        function onclickSiteTab() {
            //var custID = $('#ContentPlaceHolder1_idlblCustomerID').text();
            var custID = $('#ContentPlaceHolder1_hiddenCustomerID').val();
            if (custID == "") {
                alert("Please select Customer to view Sites");
                return false;
            }
        }
    </script>

</asp:Content>