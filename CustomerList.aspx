<%@ Page Title="AOL 2.0" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CustomerList.aspx.vb" Inherits="CustomerList" %>

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


  <style>
       .control-label-labelform{
            color:black;
        }        
  </style>
 
   <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
     
   <script src="Scripts/bootstrap.min.js"></script>
   <script src="Scripts/bootstrap-datepicker.min.js"></script>
    <script src="Scripts/autonumeric4.1.0.js"></script>

  
    <script src="JS/SliderJS/slider.js"></script>
    <script src="JS/SliderJS/script.js"></script>
     <script src="JS/jquery.blockUI.Js"></script>

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
            margin-left: 0;
        }

     #idMainblock {
            border: 2px solid #1d3f93;
        }
     #divCustomer{
        padding-left:0px;
    }
     #divGrid{
        padding-left:0px;
    }
     #ContentPlaceHolder1_txtCustomer
     {
         text-align:left;
         Width:250px;
     }
     #ContentPlaceHolder1_btnGetCustomer
     {
         text-align:left;
     }
        @media only screen and (min-width:360px) and (max-width:768px) {

            #divCustomer{
                padding-left:16px;
            }
            #divGrid{
                padding-left:16px;
            }
            #ContentPlaceHolder1_txtCustomer {

                left:50px;
            }
        }
    </style>

    <div class="row">
         <div class="col-md-12 col-xs-12">
                  <div id="idMainblock">
                         <div class="row">
        <div class="col-md-1 col-xs-12">
        </div>
        <div class="col-md-10 col-xs-12">
            <div class="row">
                <div class="col-md-12 alignCenter">
                    <span style="text-align: left; color: black; font-weight: bold; font-size: 30px;">Welcome!</span>
                </div>
            </div>
            <div class="row">
                <div class="hidden-md hidden-lg col-xs-12 col-sm-12">
                    <div class="hidden-md hidden-lg col-xs-12 col-sm-12">
                    Below you can search for a customer Site using any of the filtering options below.Once you have found the right customer site, you can add floor plans and place out parent device(s) and any Child devices that might  exist.
                       </div> 
                </div>
                <div class="col-md-12 hidden-xs hidden-sm" style="color: black">
                    Below you can search for a customer Site using any of the filtering options below.Once you have found the right customer site, you can add floor plans and place out parent device(s) and any Child devices that might  exist.
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12" style="height: 40px"></div>
            </div>

        <div class="row">
            <div class="hidden-lg hidden-md col-xs-12 col-sm-12" style="height:10px"> 
                <div class="col-md-4" style="color: black; font-size: 19px;text-align: left;">
                Search Customer Site
            </div>
                </div>

            <div class="col-md-1 hidden-sm hidden-xs">
            </div>
            <div class="col-md-4 hidden-sm hidden-xs" style="color: black; font-size: 19px;text-align: left;">
                Search Customer Site
            </div>
                </div>
       
            
        <div class="row">
            <div class="col-xs-12 col-md-12" style="height: 10px"></div>
        </div>
            <div class="col-xs-12 col-sm-12 hidden-md" style="height: 10px"></div>
        <div class="row">
            <div class="hidden-lg hidden-md col-xs-12 col-sm-12" >
                 <div class="col-md-4" style="text-align: left;">
                <asp:Label ID="lblcustomer1" runat="server" class="control-label-labelform" Text="Customer Name">Customer Name/ID </asp:Label>
            </div>
            </div>

            <div class="col-md-1 hidden-xs hidden-sm">
            </div>
            <div class="col-md-4 hidden-xs hidden-sm" style="text-align: left;">
                <asp:Label ID="lblcustomer" runat="server" class="control-label-labelform" Text="Customer Name">Customer Name/ID </asp:Label>
            </div>
        
            </div>
        <div class="row">
            <div class="col-xs-12 col-md-12" style="height: 10px"></div>
        </div>
        <div class="row" id ="divCustomer">
           
            <div class="col-md-1 hidden-sm hidden-xs">
            </div>
            <div class="col-md-2 col-xs-12 col-sm-12">
                <asp:TextBox ID="txtCustomer" class="form-control textbox " runat="server"> </asp:TextBox>
            </div>
                <div class="hidden-md hidden-lg col-xs-12 col-sm-12" style="height:10px"></div>
            <div class="col-md-2 col-xs-12 col-sm-12">
                <asp:Button ID="btnGetCustomer" CssClass="btn btn-sm btn-primary" runat="server" Text="Submit" OnClick="btnGetCustomerlist_Click" />
            </div>

       
            </div>
        <div class="row">
            <div class="col-xs-12 col-md-12" style="height: 20px"></div>
        </div>
        <div class="row" id="divGrid">
           <%-- <div class="col-md-12 col-xs-12 col-sm-12">--%>

            
            <div class="col-md-1 hidden-xs hidden-sm"></div>
            <div class="col-md-11 col-xs-12 col-sm-12">

                <div class="table-responsive">

                    <asp:GridView ID="GridCustomer" runat="server" CssClass="table table-bordered table" AutoGenerateColumns="false" HeaderStyle-ForeColor="Black" Style="max-width: 1000px">
                        <Columns>

                            <asp:TemplateField ItemStyle-Width="30px" ControlStyle-CssClass="" HeaderText="AccountID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server"
                                        Text='<%#Eval("AccountID")%>' ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LocationID" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocationID" runat="server"
                                        Text='<%#Eval("LocationID")%>' ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Name" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="350px">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server"
                                        Text='<%#Eval("ServiceName")%>' ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Address1" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="450px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress1" runat="server"
                                        Text='<%#Eval("Address1")%>' ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:HyperLinkField
                                DataNavigateUrlFields="LocationID"
                                DataNavigateUrlFormatString="~\FloorPlan.aspx?LocationID={0}" Text="Select" />
                        </Columns>

                    </asp:GridView>
                </div>

            </div>
            
        <%--</div>--%>
    </div>

     </div>
        <div class="col-md-1 col-xs-12">
        </div>
                              </div>
                       </div>
              </div>
    </div>
</asp:Content>



