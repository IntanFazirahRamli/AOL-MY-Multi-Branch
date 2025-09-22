<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="AddressVerification.aspx.vb" Inherits="AddressVerification" %>
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
        .alignRight {
            text-align: right !important;
        }
        
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:AsyncPostBackTrigger ControlID="btnVerify" />
        </Triggers>

        <ContentTemplate>

              <div class="col-md-12 col-xs-12">
                <div class="row">
                      <div class="col-md-4 alignRight">
                        <asp:Label ID="lblFile" runat="server" class="control-label-labelform" Text="File">File</asp:Label>
                    </div>
                    <div class="col-md-1">
                            <input id="fileUpload" type="file" name="file" runat="server" accept=".xls,.xlsx" />
                    </div>
                        <div class="col-md-3">
                      <label style="color:black;font-weight: 300!important;font-size: 11px;" id="lblSelectedFileName"></label>
                    </div>

                    <div class="col-md-2">
                        <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="Upload"  OnClick ="btnUpload_Click" OnClientClick ="return validateFileUpload();"/>
                    </div>
                    </div>
                <div class="row">
                    <div class="col-xs-12 col-md-12" style="height: 5px"></div>
                </div>
                    <div class="row">
                         <div class="col-md-4 alignRight">
                        <asp:Label ID="lblworkbook" runat="server" class="control-label-labelform" Text="WorkbookName">Workbook Name</asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtWorkbookName" class="form-control" runat="server"></asp:TextBox>
                    </div>
                        </div>
                      <div class="row">
                    <div class="col-xs-12 col-md-12" style="height: 5px"></div>
                </div>
                     <div class="row">
                    <div class="col-md-4 alignRight">
                        <asp:Label ID="lblSheet" runat="server" class="control-label-labelform" Text="Sheet">Sheet</asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlSheet" class="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                    <div class="row">
                    <div class="col-xs-12 col-md-12" style="height: 5px"></div>
                </div>
                  <div class="row">
                    <div class="col-md-4 alignRight">
                        <asp:Label ID="lblStreetAddress" runat="server" class="control-label-labelform" Text="Sheet">Street Address Column</asp:Label>
                    </div>
                    <div class="col-md-1">
                        <asp:TextBox ID="txtAddress" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblPostalCode" runat="server" class="control-label-labelform" Text="Sheet">Postal Code</asp:Label>
                    </div>
                    <div class="col-md-1">
                        <asp:TextBox ID="txtPostalCode" class="form-control" runat="server" ></asp:TextBox>
                    </div>
                    
                </div>
                 <div class="row">
                    <div class="col-xs-12 col-md-12" style="height: 5px"></div>
                </div>
                <div class ="row">
                     <div class="col-md-4 alignRight">
                        <asp:Label ID="lblRowStart" runat="server" class="control-label-labelform" Text="Sheet">Row to Start</asp:Label>
                    </div>
                    <div class="col-md-1 ">
                        <asp:TextBox ID="txtRowStart" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblRowEnd" runat="server" class="control-label-labelform" Text="Sheet">Row to End</asp:Label>
                    </div>
                    <div class="col-md-1 ">
                        <asp:TextBox ID="txtRowEnd" class="form-control" runat="server" ></asp:TextBox>
                    </div>
                </div>
                       
                <div class="row">
                    <div class="col-xs-12 col-md-12" style="height: 20px"></div>
                </div>

                           <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-10">

                        <div class="table-responsive">
                            <asp:GridView ID="GridAddressdetails" runat="server" CssClass="table table-bordered table" AutoGenerateColumns="false" HeaderStyle-ForeColor="Black" Style="max-width: 1000px">
                                <Columns>
                                 <asp:TemplateField HeaderText="Address" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server"
                                                Text='<%#Eval("Address")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Postal Code" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostalCode" runat="server"
                                                Text='<%#Eval("PostalCode")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </div>
                    </div>
                </div>
                   <div class="row"><div class="col-xs-12 col-md-12" style="height: 5px"></div></div>
                              <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-10">

                        <div class="table-responsive">
                            <asp:GridView ID="gridresults" runat="server" CssClass="table table-bordered table" AutoGenerateColumns="false" HeaderStyle-ForeColor="Black" Style="max-width: 1000px">
                                <Columns>
                                 <asp:TemplateField HeaderText="FileResults" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileResults" runat="server"
                                                Text='<%#Eval("FileResults")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server"
                                                Text='<%#Eval("Total")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Processed" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcessed" runat="server"
                                                Text='<%#Eval("Processed")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Skipped" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSkipped" runat="server"
                                                Text='<%#Eval("Skipped")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </div>
                    </div>
                </div>
                  
                 <div class="row"><div class="col-xs-12 col-md-12" style="height: 5px"></div></div>
                <div class="row">
                    <div class="col-md-4 alignRight"></div>
                    <div class="col-md-2" style="text-align: left;">
                         <asp:Button ID="btnVerify" CssClass="btn btn-sm btn-primary" runat="server" OnClientClick="return validation();" OnClick="btnVerify_Click" Text="Verify" style="width: 118px;"  />
                    </div>
                     <div class="col-md-4" style="text-align: left;">
                         <asp:Button ID="btnClose" CssClass="btn btn-sm btn-primary" runat="server" Text="Close"  OnClick="btnClose_Click" style="width: 118px;"    />
                    </div>
                </div>

                  <div class="row"><div class="col-xs-12 col-md-12" style="height: 5px"></div></div>
                <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2" style="text-align: left;">
                             <asp:Label ID="lblMismatchedRecords" runat="server"  Text ="Mismatched Records" ForeColor="Black"></asp:Label>
                        </div>
                </div>
                <div class="row"><div class="col-xs-12 col-md-12" style="height: 5px"></div></div>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-10">

                        <div class="table-responsive">
                            <asp:GridView ID="GridmismatchedRecords" runat="server" CssClass="table table-bordered table" AutoGenerateColumns="false" HeaderStyle-ForeColor="Black" Style="max-width: 1000px">
                                 <Columns>
                                 <asp:TemplateField HeaderText="Columns" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblColumns" runat="server"
                                                Text='<%#Eval("Columns")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Street Address" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStreetAddress" runat="server"
                                                Text='<%#Eval("StreetAddress")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Postal Code" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostalCode" runat="server"
                                                Text='<%#Eval("PostalCode")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OneMap Address Retrieved Based On Postal Code" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOneMapAddressRetrieved" runat="server"
                                                Text='<%#Eval("OneMapAddressRetrieved")%>' ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </div>
                    </div>
                </div>
                  </div>

        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <script>

        function stopLoading() {
            $.unblockUI();
        }

        function validateFileUpload()
        {
            console.log("validateFileUpload");
            var file = $('#<%= fileUpload.ClientID%>').val();
            var filename = file.substring(file.lastIndexOf('\\') + 1);
            if(filename=="")
            {
                alert("Please Select a File to Upload");
                return false;
            }

            return true;
        }
        function previewFile() {

            var file = $('#<%= fileUpload.ClientID%>').val();

            var fileupload = document.querySelector('#<%=fileUpload.ClientID%>').files[0];

            var filename = file.substring(file.lastIndexOf('\\') + 1);
            $('#<%=txtWorkbookName.ClientID%>').val(filename);
          
                var reader = new FileReader();
                //if (reader.readAsBinaryString) {
                reader.onloadend = function () {
                    var s = reader.result;
                    
                }
                if (fileupload) {
                   
                    var data = reader.readAsBinaryString(fileupload);
                    var workbook = XLSX.read(data, { type: 'binary' });
                }
                else
                {
                   
                }
                   
        }

     

        $(document).ready(function () {

            $('input[type="file"]').change(function (e) {

                    var fileName = e.target.files[0].name;
                    $("#lblSelectedFileName").text(fileName);
            });


            $('input[type="file"]').click(function (e) {

                this.value = null;
            });


            //$("#ContentPlaceHolder1_fileUpload").change(function (e) {
            //    debugger;
            //    //Get the files from Upload control
            //    var files = e.target.files;
            //    console.log("Inside");
            //    console.log("files:" + files);
            //    var i, f;
            //    //Loop through files
            //    for (i = 0, f = files[i]; i != files.length; ++i) {
            //        var reader = new FileReader();
            //        var name = f.name;
            //        console.log("name:" + name);
            //        reader.onload = function (e) {
            //            var data = e.target.result;
            //            console.log("data" + data);
            //            var result;
            //            var workbook = XLSX.read(data, { type: 'binary' });

            //            var sheet_name_list = workbook.SheetNames;
            //            console.log("SheetNames" + sheets_name_list);
            //        }


            //    }
            //});
            
        });
 
        function validation() {

            var sheetID = $('#<%= ddlSheet.ClientID%>').val();
            //$("#ContentPlaceHolder1_ddlSheet").val()

            if (sheetID == null)
            {
                alert("please select the sheet");
                return false;
            }
            if (($('#<%= txtAddress.ClientID%>').val() == '') || ($('#<%= txtPostalCode.ClientID%>').val() == '')) {

                alert("please enter AddressColumn and PostalCode Column to verify");
                return false;
                 return false;
            }
           <%-- if (($('#<%= txtRowStart.ClientID%>').val() == '') || ($('#<%= txtRowEnd.ClientID%>').val() == '')) {

                alert("please enter Row to Start and Row to End  to verify");
                return false;
                return false;
            }--%>
            $.blockUI({ message: '<img src="Images/loader123.gif" /> Processing ...' });
        }

        function alertpopup()
        {
            alert("please enter AddressColumn and PostalCode Column");
            return false;
        }
        function warningpopup() {
            alert("No Valid Postal Code has been detected. Please check the file and columns that you're processing.");
            return false;
        }

    </script>
   
    
</asp:Content>

