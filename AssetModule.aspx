<%@ Page Title="Asset" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="AssetModule.aspx.vb" Inherits="AssetModule" EnableEventValidation="FALSE" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <%--  <style>
        #AjaxFileUpload1_UploadOrCancelButton{
            display: none;
        }
    </style>
      <script>
          Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
          function EndRequestHandler(sender, args) {
              var f = $get('AjaxFileUpload1_UploadOrCancelButton');
              f.click();
          }
    </script>--%>
    <script type="text/javascript">
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

        //function savefile() {
        //    if ($(".ajax__fileupload_fileItemInfo").length > 0) {
        //            $(".ajax__fileupload_uploadbutton").trigger("click");
               
        //    }

        //}

        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }

       function uploadComplete(sender) {
           //$get("<%=lblAlertCheckout.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblAlertCheckout.ClientID%>").innerHTML = "File upload failed.";
        }
  
        function uploadComplete2(sender) {
            //$get("<%=lblAlertCheckIn.ClientID%>").innerHTML = "File Uploaded Successfully";
             }

        function uploadError2(sender) {
            $get("<%=lblAlertCheckIn.ClientID%>").innerHTML = "File upload failed.";
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

       var defaultTextAsset = "Search Here";
        function WaterMark1(txt, evt) {
            if (txt.value.length == 0 && evt.type == "blur") {
                txt.style.color = "gray";
                txt.value = defaultTextAsset;
            }
            if (txt.value == defaultTextAsset && evt.type == "focus") {
                txt.style.color = "black";
                txt.value = "";
            }
        }

        function TabChanged(sender, e) {
            if (sender.get_activeTabIndex() == 1 || sender.get_activeTabIndex() == 2) {

                if (document.getElementById("<%=txtAssetNo.ClientID%>").value == '') {
                $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                alert("Please select an Asset to proceed.");
                return;
            }


        }

    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        /*.ajax__fileupload_fileItemInfo {
    line-height: 20px;
    height: 44px;
    margin-bottom: 2px;
    overflow: hidden;
}

.ajax__fileupload_fileItem_commentContainer {
    display: table;
    width: 100%;
}

.ajax__fileupload_fileItem_commentLabel {
    display: table-cell;
    width: 1px;
    white-space: nowrap;
    padding-right: 5px;
}

.ajax__fileupload_fileItem_commentInput {
    display: table-cell;
    width: 100%;
}*/

.modalBackground
{
background-color: gray;
filter: alpha(opacity=80);
opacity: 0.8;
z-index: 10000;
}
     
            .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}
   
    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:left;
        width:10%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
    .CellFormatPopup{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:left;
        width:250px;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
      .CellTextBox{
        
        color:black;
        text-align:left;
        /*width:20%;*/
        /*table-layout:fixed;
        overflow:hidden;*/
          /*border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;*/
    }
  
          .auto-style3 {
             font-size: 15px;
             font-weight: bold;
             font-family: 'Calibri';
             color: black;
             text-align: right; /*width:30%;*/ /*table-layout:fixed;
        overflow:hidden;*/;
             border-collapse: collapse;
             border-spacing: 0;
             line-height: 10px;
             width: 20%;
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

 /*.FileUploadClass
        {
            font-size: 5px;
            z-index: 500;
            position: relative;
            z-index: 10;
        }
        .FileUploadClass input[type=file]
        {
            background: transparent;
            border: Dashed 2px #000000;
            opacity: 0;
            filter: alpha(opacity = 0);
        }
        .FakeFileUpload
        {
            position: relative;
            border: Solid 1px #000000;
            width: 400px;
            z-index:1;
        }
        .FakeFileUploadDiv
        {
            position: absolute;
            opacity: .5;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)";
            filter: alpha(opacity=50);
            z-index:5;
        }*/
          </style>

   <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="JS/gridviewScroll.min.js"></script>
    <link href="CSS/GridviewScroll.css" rel="stylesheet" />
     <script type="text/javascript">
         $(document).ready(function () {
             gridviewScroll();
         });

         function gridviewScroll() {
             gridView1 = $('#<%=GridView1.ClientID %>').gridviewScroll({
                 width: 1090,
                 height: 300,
                 railcolor: "#F0F0F0",
                 barcolor: "#CDCDCD",
                 barhovercolor: "#606060",
                 bgcolor: "#F0F0F0",
                 freezesize: 2,
                 arrowsize: 30,
                 varrowtopimg: "Images/arrowvt.png",
                 varrowbottomimg: "Images/arrowvb.png",
                 harrowleftimg: "Images/arrowhl.png",
                 harrowrightimg: "Images/arrowhr.png",
                 headerrowcount: 1,
                 railsize: 16,
                 barsize: 8
             });
         }
	</script>
   --%>
       <div style="text-align:center">
            <%-- <asp:UpdatePanel ID="updPanelService1" runat="server" UpdateMode="Conditional">
          <ContentTemplate>--%>
           <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>
 
    <%-- <asp:UpdatePanel ID="updPanelAsset" runat="server" UpdateMode="Conditional">
          <ContentTemplate>--%>

           <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="False" LoadScriptsBeforeUI="False">
                 <ControlBundles>
        <ajaxToolkit:ControlBundle Name="CalendarExtender_Bundle" />
                     <ajaxToolkit:controlBundle name="ModalPopupExtender_Bundle"/>
                                 <ajaxToolkit:controlBundle name="ListSearchExtender_Bundle"/>
                    <ajaxtoolkit:controlBundle name="TabContainer_Bundle"/>  
                      <ajaxtoolkit:controlBundle name="AsyncFileUpload_Bundle"/>  
                      <ajaxtoolkit:controlBundle name="AjaxFileUpload_Bundle"/>  
                      <ajaxtoolkit:controlBundle name="CollapsiblePanelExtender_Bundle"/> 
   </ControlBundles>
           </ajaxToolkit:ToolkitScriptManager> 
   <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Asset</h3>
   <table class="Centered"  border="0" style="width:100%;text-align:center;">
            <%--<tr>
                <td colspan="2"><br /></td>
            </tr>--%>
            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
                 <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                       <asp:Label ID="txtMode" runat="server" Text="" CssClass="dummybutton"></asp:Label>
</td>
                     
            </tr>
            
        
       </table>
           <ajaxtoolkit:TabContainer ID="tb1" AutoPostBack="true" runat="server" Font-Names="Calibri" Height="100%" Width="100%" CssClass="ajax__tab_xp" ActiveTabIndex="0" >
 
     
      <ajaxtoolkit:TabPanel runat="server" HeaderText="Asset Information" ID="TabPanel1" Width="100%" CssClass="Centered" TabIndex="0">
        <HeaderTemplate>
           Asset Information
        </HeaderTemplate>
        
        <ContentTemplate>
             <table style="width:100%">
                     <tr>
              
                <td colspan="2" style="text-align:left;">
                  
                     <asp:Button ID="btnAdd" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
                <asp:Button ID="btnFilter" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="100px" />
                     <asp:Button ID="btnStatus" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Width="100px" />
                <asp:Button ID="btnMovement" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="MOVEMENT" Width="120px" Visible="False" />
                               <asp:Button ID="btnChangeStatus" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Width="150px" Text="CHANGE STATUS" />
          
                      <asp:Button ID="btnImportFromExcel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Width="180px" Text="IMPORT FROM EXCEL" />
              <asp:Button ID="btnExportToExcel1" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Width="160px" Text="EXPORT TO EXCEL" visible="False" />
             
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
            
                       </td>
                <td style="text-align: right">
            
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />

                </td>
                
            </tr>
         <tr>
               <td style="text-align:left;"><asp:Label ID="Label57" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label>
                   <asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList></td>
                    <td colspan="2" style="text-align:right;">
                          <asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset1.png" Height="25px" Width="25px" ToolTip="RESET SEARCH"  /> 
                         <asp:TextBox ID="txtSearchAsset" runat="server" Width="370px" Height="25px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMark1(this, event);" onfocus = "WaterMark1(this, event);" AutoPostBack="True"></asp:TextBox>      
                              &nbsp; <asp:Button ID="btnGoCust" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="GO" Width="50px" Height="30px" />
                   <asp:TextBox ID="txtSearchText" runat="server" CssClass="dummybutton" ></asp:TextBox>     </td>
            </tr>
                   <tr>
                         <td colspan="3" style="text-align:right;">
                 <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                             </td>
                       </tr>

            <tr class="Centered">
                <td colspan="3" class="Centered">

                      <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" OnRowDataBound = "OnRowDataBound" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="14px" HorizontalAlign="Center" AllowSorting="True" Width="100%" AllowPaging="True" CssClass="Centered" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ForeColor="#333333" EmptyDataText="NO DATA" SelectedIndex="0">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False" >
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
                    <asp:TemplateField HeaderText="Available">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkStatus" runat="server" Enabled="false" Checked='<%# IIf(Eval("Status").ToString().Equals("1"), True, False)%>' />
                          </ItemTemplate>
                  </asp:TemplateField>
                 <asp:TemplateField HeaderText="Movement" Visible="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnMovement1" runat="server" BackColor="#DDDDFF" CssClass="roundbutton1" CommandArgument='<%# Bind("AssetNo")%>' CommandName="Movement"
                                        Text="Movement" OnClick="btnMovement_Click" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMovement" runat="server" Text='<%# Bind("AssetNo") %>' Visible="false"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                <asp:BoundField DataField="AssetNo" HeaderText="Asset No" SortExpression="AssetNo">
                 <ItemStyle Width="90px" Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="SerialNo" HeaderText="Serial No" SortExpression="SerialNo">
                 <ItemStyle Width="90px" Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="IMEI" HeaderText="IMEI" SortExpression="IMEI">
                 <ItemStyle Width="90px" Wrap="False" />
                </asp:BoundField>
                     <asp:BoundField DataField="AssetGrp" HeaderText="AssetGrp" SortExpression="AssetGrp" />
                <asp:BoundField DataField="AssetClass" HeaderText="Asset Class" SortExpression="AssetClass" Visible="False" />  
                  <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" Visible="False" />
                 <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" Visible="False" />
             <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" Visible="False" />
          
               <asp:BoundField DataField="AssetRegNo" HeaderText="Asset RegNo" SortExpression="AssetRegNo" />
                    <asp:BoundField DataField="Descrip" HeaderText="Description" SortExpression="Descrip">
               
                 <ControlStyle Width="250px" />
               
                 <HeaderStyle Width="250px" />
                 <ItemStyle Width="250px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="FinCoName" HeaderText="CurrentUser" SortExpression="FinCoName" />
                   <asp:BoundField DataField="PurchDt" HeaderText="PurchaseDate" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PurchDt" />
                 <asp:BoundField DataField="SupplierCode" HeaderText="SupplierCode" SortExpression="SupplierCode" Visible="False" />
                 <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
               <asp:BoundField DataField="PurchRef" HeaderText="P.O.No." SortExpression="PurchRef" Visible="False" />
                 <asp:BoundField DataField="PurchVal" HeaderText="PurchasePrice" DataFormatString="{0:N2}" SortExpression="PurchVal" Visible="False" />
                <asp:BoundField DataField="Warranty" HeaderText="Warranty" SortExpression="Warranty" />
                  <asp:BoundField DataField="WarrantyMS" HeaderText="WarrantyMS" SortExpression="WarrantyMS" />
                  <asp:BoundField DataField="WarrantyEnd" HeaderText="WarrantyEnd" DataFormatString="{0:dd/MM/yyyy}" SortExpression="WarrantyEnd" />
               
             
               
                 <asp:BoundField DataField="Location">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
               
                 
                <asp:BoundField DataField="AssetCode" HeaderText="AssetCode" SortExpression="AssetCode" Visible="False" />
                 <asp:BoundField DataField="MfgYear" HeaderText="MfgYear" SortExpression="MfgYear" Visible="False" />

                 <asp:BoundField DataField="Capacity" HeaderText="Capacity" SortExpression="Capacity" Visible="False" />
                <asp:BoundField DataField="CapacityUnitMS" HeaderText="CapacityUnitMS" SortExpression="CapacityUnitMS" Visible="False" />
                <asp:BoundField DataField="RegDate" HeaderText="RegDate" SortExpression="RegDate" DataFormatString="{0:dd/MM/yyyy}" Visible="False" />
                <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" Visible="False" />
                 
                 <asp:BoundField DataField="NextSvcDate" HeaderText="NextSvcDate" SortExpression="NextSvcDate" DataFormatString="{0:dd/MM/yyyy}" Visible="False"/>
                 <asp:BoundField DataField="LastSvcDate" HeaderText="LastSvcDate" SortExpression="LastSvcDate" DataFormatString="{0:dd/MM/yyyy}" Visible="False"/>
                 <asp:BoundField DataField="PriceCode" HeaderText="PriceCode" SortExpression="PriceCode" Visible="False"/>
                 <asp:BoundField DataField="ReferenceOur" HeaderText="ReferenceOur" SortExpression="ReferenceOur" Visible="False"/>
               
                <asp:BoundField DataField="EstDateIn" HeaderText="EstDateIn" SortExpression="EstDateIn" DataFormatString="{0:dd/MM/yyyy}" Visible="False"/>
                <asp:BoundField DataField="DateOut" HeaderText="DateOut" SortExpression="DateOut" DataFormatString="{0:dd/MM/yyyy}" Visible="False"/>
                  <asp:BoundField DataField="RefDate" HeaderText="RefDate" SortExpression="RefDate" DataFormatString="{0:dd/MM/yyyy}" Visible="False"/>
                <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" Visible="False"/>
                
                <asp:BoundField DataField="GoogleEmail" HeaderText="GoogleEmail" SortExpression="GoogleEmail" Visible="False" />
                 <asp:BoundField DataField="GPSLabel" HeaderText="GPSLabel" SortExpression="GPSLabel" Visible="False" />
                  <asp:BoundField DataField="TechnicalSpecs" HeaderText="TechnicalSpecs" SortExpression="TechnicalSpecs" Visible="False"/>
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" Visible="False"/>
                
                 
                 <asp:BoundField DataField="DefAddr" HeaderText="DefAddr" SortExpression="DefAddr" Visible="False" />
                 <asp:BoundField DataField="Depmtd" HeaderText="Depmtd" SortExpression="Depmtd" Visible="False" />

                 <asp:BoundField DataField="PurfmCType" HeaderText="PurfmCType" SortExpression="PurfmCType" Visible="False" />
                 <asp:BoundField DataField="PurfmCCode" HeaderText="PurfmCCode" SortExpression="PurfmCCode" Visible="False" />
                 <asp:BoundField DataField="PurfmInvNo" HeaderText="PurfmInvNo" SortExpression="PurfmInvNo" Visible="False" />
                   <asp:BoundField DataField="ObbkVal" HeaderText="ObbkVal" SortExpression="ObbkVal" Visible="False" />
                 
                 <asp:BoundField DataField="ObbkDate" HeaderText="ObbkDate" SortExpression="ObbkDate" Visible="False" />
                 <asp:BoundField DataField="CurrAddr" HeaderText="CurrAddr" SortExpression="CurrAddr" Visible="False" />
                 <asp:BoundField DataField="InCharge" HeaderText="InCharge" SortExpression="InCharge" Visible="False" />
                 <asp:BoundField DataField="CurrCont" HeaderText="CurrCont" SortExpression="CurrCont" Visible="False" />
                 <asp:BoundField DataField="Phone1" HeaderText="Phone1" SortExpression="Phone1" Visible="False" />
                 <asp:BoundField DataField="Phone2" HeaderText="Phone2" SortExpression="Phone2" Visible="False" />
                  <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" Visible="False" />
                 <asp:BoundField DataField="ActDateIn" HeaderText="ActDateIn" SortExpression="ActDateIn" Visible="False" />
                 <asp:BoundField DataField="LastMoveId" HeaderText="LastMoveId" SortExpression="LastMoveId" Visible="False" />
                 <asp:BoundField DataField="Project" HeaderText="Project" SortExpression="Project" Visible="False" />
                 <asp:BoundField DataField="Purpose" HeaderText="Purpose" SortExpression="Purpose" Visible="False" />
                 
                 <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" Visible="False" />
                
                 <asp:BoundField DataField="EngineNo" HeaderText="EngineNo" SortExpression="EngineNo" Visible="False" />
                 <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" Visible="False" />
                  <asp:BoundField DataField="PurfmCName" HeaderText="PurfmCName" SortExpression="PurfmCName" Visible="False" />
                  <asp:BoundField DataField="SvcFreq" HeaderText="SvcFreq" SortExpression="SvcFreq" Visible="False" />
                <asp:BoundField DataField="SvcBy" HeaderText="SvcBy" SortExpression="SvcBy" Visible="False" />
                 <asp:BoundField DataField="DWM" HeaderText="DWM" SortExpression="DWM" Visible="False" />
                 <asp:BoundField DataField="SoldToCoID" HeaderText="SoldToCoID" SortExpression="SoldToCoID" Visible="False" />
                 <asp:BoundField DataField="SoldToCoName" HeaderText="SoldToCoName" SortExpression="SoldToCoName" Visible="False" />
                 <asp:BoundField DataField="SoldVal" HeaderText="SoldVal" SortExpression="SoldVal" Visible="False" />
                 <asp:BoundField DataField="SoldRef" HeaderText="SoldRef" SortExpression="SoldRef" Visible="False" />
                 <asp:BoundField DataField="SoldDate" HeaderText="SoldDate" SortExpression="SoldDate" Visible="False" />
                 <asp:BoundField DataField="DisposedRef" HeaderText="DisposedRef" SortExpression="DisposedRef" Visible="False" />
                 <asp:BoundField DataField="DisposedDate" HeaderText="DisposedDate" SortExpression="DisposedDate" Visible="False" />
                 <asp:BoundField DataField="AuthorBy" HeaderText="AuthorBy" SortExpression="AuthorBy" Visible="False" />
                 <asp:BoundField DataField="Desc1" HeaderText="Desc1" SortExpression="Desc1" Visible="False" />
                 <asp:BoundField DataField="AltNo" HeaderText="AltNo" SortExpression="AltNo" Visible="False" />
                 
                 <asp:BoundField DataField="CustCode" HeaderText="CustCode" SortExpression="CustCode" Visible="False" />
                 <asp:BoundField DataField="CustName" HeaderText="CustName" SortExpression="CustName" Visible="False" />
                   <asp:BoundField DataField="PROJECTCODE" HeaderText="PROJECTCODE" SortExpression="PROJECTCODE" Visible="False" />
                 <asp:BoundField DataField="PROJECTNAME" HeaderText="PROJECTNAME" SortExpression="PROJECTNAME" Visible="False" />
                 <asp:BoundField DataField="Cost" HeaderText="Cost" SortExpression="Cost" Visible="False" />
                  <asp:BoundField DataField="LocCode" HeaderText="LocCode" SortExpression="LocCode" Visible="False" />
                 <asp:BoundField DataField="ValueDate" HeaderText="ValueDate" SortExpression="ValueDate" Visible="False" />
                 <asp:BoundField DataField="SoldBy" HeaderText="SoldBy" SortExpression="SoldBy" Visible="False" />
                 <asp:BoundField DataField="MarketValue" HeaderText="MarketValue" SortExpression="MarketValue" Visible="False" />
                 <asp:BoundField DataField="ScrapToCoID" HeaderText="ScrapToCoID" SortExpression="ScrapToCoID" Visible="False" />
                 <asp:BoundField DataField="ScrapToCoName" HeaderText="ScrapToCoName" SortExpression="ScrapToCoName" Visible="False" />
                 <asp:BoundField DataField="ScrapVal" HeaderText="ScrapVal" SortExpression="ScrapVal" Visible="False" />
                 <asp:BoundField DataField="ScrapRef" HeaderText="ScrapRef" SortExpression="ScrapRef" Visible="False" />
                 <asp:BoundField DataField="ScrapDate" HeaderText="ScrapDate" SortExpression="ScrapDate" Visible="False" />
                 <asp:BoundField DataField="ScrapBy" HeaderText="ScrapBy" SortExpression="ScrapBy" Visible="False" />
                 <asp:BoundField DataField="EngineBrand" HeaderText="EngineBrand" SortExpression="EngineBrand" Visible="False" />
                 <asp:BoundField DataField="EngineModel" HeaderText="EngineModel" SortExpression="EngineModel" Visible="False" />
                 <asp:BoundField DataField="ArNo" HeaderText="ArNo" SortExpression="ArNo" Visible="False" />
                 <asp:BoundField DataField="DueDate" HeaderText="DueDate" SortExpression="DueDate" Visible="False" />
                 <asp:BoundField DataField="TestNo" HeaderText="TestNo" SortExpression="TestNo" Visible="False" />
                 <asp:BoundField DataField="TestRemarks" HeaderText="TestRemarks" SortExpression="TestRemarks" Visible="False" />
                 <asp:BoundField DataField="CertType" HeaderText="CertType" SortExpression="CertType" Visible="False" />
                 <asp:BoundField DataField="RentalYN" HeaderText="RentalYN" SortExpression="RentalYN" Visible="False" />
                 <asp:BoundField DataField="SelfOwnYN" HeaderText="SelfOwnYN" SortExpression="SelfOwnYN" Visible="False" />
                 <asp:BoundField DataField="PurchBy" HeaderText="PurchBy" SortExpression="PurchBy" Visible="False" />
                 <asp:BoundField DataField="CostOther" HeaderText="CostOther" SortExpression="CostOther" Visible="False" />
                 <asp:BoundField DataField="IncomeOther" HeaderText="IncomeOther" SortExpression="IncomeOther" Visible="False" />
                 <asp:BoundField DataField="DeprDur" HeaderText="DeprDur" SortExpression="DeprDur" Visible="False" />
                 <asp:BoundField DataField="DeprMonthly" HeaderText="DeprMonthly" SortExpression="DeprMonthly" Visible="False" />
                 <asp:BoundField DataField="DeprEnd" HeaderText="DeprEnd" SortExpression="DeprEnd" Visible="False" />
                 <asp:BoundField DataField="EstLife" HeaderText="EstLife" SortExpression="EstLife" Visible="False" />
                 <asp:BoundField DataField="DeprOps" HeaderText="DeprOps" SortExpression="DeprOps" Visible="False" />
                 <asp:BoundField DataField="CostBillPct" HeaderText="CostBillPct" SortExpression="CostBillPct" Visible="False" />
                 <asp:BoundField DataField="PurchOVal" HeaderText="PurchOVal" SortExpression="PurchOVal" Visible="False" />
                 <asp:BoundField DataField="PurchExRate" HeaderText="PurchExRate" SortExpression="PurchExRate" Visible="False" />
                 <asp:BoundField DataField="PurchCurr" HeaderText="PurchCurr" SortExpression="PurchCurr" Visible="False" />
                 <asp:BoundField DataField="RoadTaxExpiry" HeaderText="RoadTaxExpiry" SortExpression="RoadTaxExpiry" Visible="False" />
                 <asp:BoundField DataField="CoeExpiry" HeaderText="CoeExpiry" SortExpression="CoeExpiry" Visible="False" />
                 <asp:BoundField DataField="InspectDate" HeaderText="InspectDate" SortExpression="InspectDate" Visible="False" />
                 <asp:BoundField DataField="VpcExpiry" HeaderText="VpcExpiry" SortExpression="VpcExpiry" Visible="False" />
                 <asp:BoundField DataField="VpcNo" HeaderText="VpcNo" SortExpression="VpcNo" Visible="False" />
                 <asp:BoundField DataField="PaymentType" HeaderText="PaymentType" SortExpression="PaymentType" Visible="False" />
                 <asp:BoundField DataField="MarketCost" HeaderText="MarketCost" SortExpression="MarketCost" Visible="False" />
                 <asp:BoundField DataField="CostDate" HeaderText="CostDate" SortExpression="CostDate" Visible="False" />
                 <asp:BoundField DataField="GroupID" HeaderText="GroupID" SortExpression="GroupID" Visible="False" />
                 <asp:BoundField DataField="GroupName" HeaderText="GroupName" SortExpression="GroupName" Visible="False" />
                 <asp:BoundField DataField="SaleableYN" HeaderText="SaleableYN" SortExpression="SaleableYN" Visible="False" />
                 <asp:BoundField DataField="FinCoID" HeaderText="FinCoID" SortExpression="FinCoID" Visible="False" />
                 <asp:BoundField DataField="FinCoName" HeaderText="FinCoName" SortExpression="FinCoName" Visible="False" />
                 <asp:BoundField DataField="FinDtFrom" HeaderText="FinDtFrom" SortExpression="FinDtFrom" Visible="False" />
                 <asp:BoundField DataField="FinDtTo" HeaderText="FinDtTo" SortExpression="FinDtTo" Visible="False" />
                 <asp:BoundField DataField="GltypeSales" HeaderText="GltypeSales" SortExpression="GltypeSales" Visible="False" />
                 <asp:BoundField DataField="GltypePurchase" HeaderText="GltypePurchase" SortExpression="GltypePurchase" Visible="False" />
                 <asp:BoundField DataField="LedgercodeSales" HeaderText="LedgercodeSales" SortExpression="LedgercodeSales" Visible="False" />
                 <asp:BoundField DataField="LedgercodePurchase" HeaderText="LedgercodePurchase" SortExpression="LedgercodePurchase" Visible="False" />
                 <asp:BoundField DataField="ContactType" HeaderText="ContactType" SortExpression="ContactType" Visible="False" />
                 
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 
                 <asp:BoundField DataField="AgmtNo" HeaderText="AgmtNo" SortExpression="AgmtNo" Visible="False" />
                 <asp:BoundField DataField="LoanAmt" HeaderText="LoanAmt" SortExpression="LoanAmt" Visible="False" />
                 <asp:BoundField DataField="NoInst" HeaderText="NoInst" SortExpression="NoInst" Visible="False" />
                 <asp:BoundField DataField="IntRate" HeaderText="IntRate" SortExpression="IntRate" Visible="False" />
                 <asp:BoundField DataField="TermCharges" HeaderText="TermCharges" SortExpression="TermCharges" Visible="False" />
                 <asp:BoundField DataField="FirstInst" HeaderText="FirstInst" SortExpression="FirstInst" Visible="False" />
                 <asp:BoundField DataField="LastInst" HeaderText="LastInst" SortExpression="LastInst" Visible="False" />
                 <asp:BoundField DataField="MthlyInst" HeaderText="MthlyInst" SortExpression="MthlyInst" Visible="False" />
                 <asp:BoundField DataField="SubLedgercodeSales" HeaderText="SubLedgercodeSales" SortExpression="SubLedgercodeSales" Visible="False" />
                 <asp:BoundField DataField="SubLedgercodePurchase" HeaderText="SubLedgercodePurchase" SortExpression="SubLedgercodePurchase" Visible="False" />
                 <asp:BoundField DataField="UploadDate" HeaderText="UploadDate" SortExpression="UploadDate" Visible="False" />
                 <asp:BoundField DataField="DelGoogleCalendar" HeaderText="DelGoogleCalendar" SortExpression="DelGoogleCalendar" Visible="False" />
                  <asp:BoundField DataField="GooglePassword" HeaderText="GooglePassword" SortExpression="GooglePassword" Visible="False" />
                 <asp:BoundField DataField="ColourCodeHtml" HeaderText="ColourCodeHtml" SortExpression="ColourCodeHtml" Visible="False" />
                 <asp:BoundField DataField="ColourCodeRGB" HeaderText="ColourCodeRGB" SortExpression="ColourCodeRGB" Visible="False" />
                   <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
              
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" />
            <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    
                    </td>
                </tr>
          <tr>
              <td colspan="3"><br /></td>
          </tr>
         <tr runat="server" id="BranchRow">                   

                    <td class="auto-style3" runat="server">Location<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                    
                    <td class="CellTextBoxADM" runat="server"><asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="30%">
                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList></td>
         <td runat="server"></td>
               </tr>
          <tr>                   

                    <td class="auto-style3">Status</td>
                    
                    <td class="CellTextBoxADM"><asp:DropDownList ID="ddlStatus" BackColor="#CCCCCC" CssClass="chzn-select" runat="server" DataSourceID="SqlDSAssetStatus" DataTextField="Status" DataValueField="Available" Width="40.5%" AppendDataBoundItems="True" Enabled="False">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList></td>
                      <td id="Td1" class="CellFormat" rowspan="10" style="width:25%" runat="server"><asp:Panel ID="Panel2" runat="server" Height="85%" BorderStyle="Solid" BorderColor="White" BorderWidth="2px" Width="100%" HorizontalAlign="Center">
                              
                    <asp:Image ID="Image2" runat="server" Width="200px" Height="200px" />

                                        </asp:Panel></td>

                 </tr>
            <tr>                   

                    <td class="auto-style3">AssetNo<asp:Label ID="Label23" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                    
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtAssetNo" BackColor="#CCCCCC" runat="server" MaxLength="30" Height="16px" Width="40%" ReadOnly="True"></asp:TextBox></td>
                     

                 </tr>
    
        <tr>                   

                    <td class="auto-style3">SerialNo</td>
                    
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtSerialNo" runat="server" MaxLength="30" Height="16px" Width="40%"></asp:TextBox></td>
                     

                 </tr>
                <tr>                   

                    <td class="auto-style3">IMEI</td>
                    
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtIMEI" runat="server" MaxLength="100" Height="16px" Width="40%"></asp:TextBox></td>
                     

                 </tr>
               
          <tr>
                  
                    <td class="auto-style3">Group<asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                   
                    <td class="CellTextBoxADM"> <asp:DropDownList ID="ddlGroup" CssClass="chzn-select" runat="server" Width="40.5%" AppendDataBoundItems="True">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
    </td>
                   
                 </tr>
         <tr>
                  
                    <td class="auto-style3">Class</td>
                   
                    <td class="CellTextBoxADM"> <asp:DropDownList ID="ddlClass" CssClass="chzn-select" runat="server" DataSourceID="SqlDSClass" DataTextField="AssetClass" DataValueField="AssetClass" Width="40.5%" AppendDataBoundItems="True">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
    </td>
                   
                 </tr>
         <tr>
                  
                    <td class="auto-style3">Brand</td>
                   
                    <td class="CellTextBoxADM"> <asp:DropDownList ID="ddlBrand" AutoPostBack="True" CssClass="chzn-select" runat="server" DataSourceID="SqlDSBrand" DataTextField="AssetBrand" DataValueField="AssetBrand" Width="40.5%" AppendDataBoundItems="True">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
    </td>
                   
                 </tr>
         <tr>
                  
                    <td class="auto-style3">Model</td>
                   
                    <td class="CellTextBoxADM"> <asp:DropDownList ID="ddlModel" CssClass="chzn-select" runat="server" DataTextField="AssetModel" DataValueField="AssetModel" Width="40.5%" AppendDataBoundItems="True">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
    </td>
                   
                 </tr>
         <tr>
                  
                    <td class="auto-style3">Color</td>
                   
                    <td class="CellTextBoxADM"> <asp:DropDownList ID="ddlColor" CssClass="chzn-select" runat="server" DataSourceID="SqlDSColor" DataTextField="AssetColor" DataValueField="AssetColor" Width="40.5%" AppendDataBoundItems="True">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
    </td>
                   
                 </tr>
        <tr>
                  
                    <td class="auto-style3">AssetRegNo</td>
                   
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtAssetRegNo" runat="server" MaxLength="30" Height="16px" Width="40%"></asp:TextBox></td>
                   
                 </tr>
          <tr>
                  
                    <td class="auto-style3">Description</td>
                   
                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtDescription" runat="server" Width="60%" MaxLength="120"></asp:TextBox>
                           </td>
                   
                 </tr>
      
         <tr>
                  
                    <td class="auto-style3">Supplier</td>
                   
                    <td class="CellTextBoxADM"> <asp:DropDownList ID="ddlSupplier" CssClass="chzn-select" runat="server" DataSourceID="SqlDSSupplier" DataTextField="Supplier" DataValueField="Supplier" Width="60.5%" AppendDataBoundItems="True">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
    </td>
                   
                 </tr>
              <tr>
                  
                    <td class="auto-style3">P.O.Number</td>
                   
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtPONo" runat="server" MaxLength="30" Height="16px" Width="40%"></asp:TextBox></td>
                   
                 </tr>
                  <tr>
           <td class="auto-style3">PurchaseDate<asp:Label ID="Label6" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
<td class="CellTextBox"><asp:TextBox ID="txtPurchaseDate" runat="server" Height="16px" MaxLength="25" Width="40%"></asp:TextBox>
    <ajaxToolKit:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtPurchaseDate" TargetControlID="txtPurchaseDate"></ajaxToolKit:CalendarExtender></td>
</tr>
        <tr>
                  
                    <td class="auto-style3">PurchasePrice</td>
                   
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtPurchasePrice" runat="server" MaxLength="30" Height="16px" Width="40%"></asp:TextBox></td>
                   
                 </tr>
          <tr>
                  
                    <td class="auto-style3">Warranty</td>
                   
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtWarranty" runat="server" AutoPostBack="True" MaxLength="30" Height="16px" Width="20%"></asp:TextBox>
                        <asp:DropDownList ID="ddlDurationType" runat="server" Width="20.5%" CssClass="chzn-select" AutoPostBack="True" >
                                   <asp:ListItem Text="--SELECT--" Value="-1" />   
                                   <asp:ListItem Value="WEEKS">WEEKS</asp:ListItem>
                                   <asp:ListItem Value="MONTHS">MONTHS</asp:ListItem>
                                   <asp:ListItem Value="YEARS">YEARS</asp:ListItem>
                                  
                               </asp:DropDownList>
                    </td>
                   
                 </tr>
          <tr>
                  
                    <td class="auto-style3">WarrantyEnd</td>
                   
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtWarrantyEnd" runat="server" MaxLength="30" Height="16px" Width="40%" Enabled="False" ReadOnly="True"></asp:TextBox></td>
                   
                 </tr>
        <tr>
                  
                    <td class="auto-style3">Notes</td>
                   
                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtNotes" runat="server" Width="60%" MaxLength="120" TextMode="MultiLine" Height="40px"></asp:TextBox>
                           </td>
                   
                 </tr>
      
       
       
           <tr>
               <td colspan="2" style="text-align:right">  
                     <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

                                  <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" CausesValidation="False" />
               </td>
           </tr>
          </table> 
       
            </ContentTemplate>
          </ajaxtoolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Movement" TabIndex="1">
                   <HeaderTemplate>
                      <asp:Label ID="lblMovementCount" runat="server" Font-Size="13px" Text="Movement"></asp:Label>
                   </HeaderTemplate>
                   <ContentTemplate>
                       
                      
                <table style="width:100%;height:100%">
                       <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertMovement" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                       <asp:Label ID="lblMsgMovement" runat="server" Text="" ForeColor="BROWN"></asp:Label>
                    <asp:Label ID="lblMovementRcno" runat="server" Font-Size="13px" Text="" Visible="false"></asp:Label>
                       <asp:Label ID="lblMovementType" runat="server" Font-Size="13px" Text="" Visible="false"></asp:Label>
</td>
                     
            </tr>
            

                     <tr><td class="CellFormat" style="width:10%;padding-left:3%">AssetNo </td>
                         <td class="CellTextBox"><asp:Label ID="lblMovementAssetNo" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="30%"></asp:Label>

                         </td></tr>
                    <tr><td class="CellFormat" style="width:10%;padding-left:3%">Description </td>
                         <td class="CellTextBox"><asp:Label ID="lblMovementDescription" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="70%"></asp:Label>

                         </td></tr>
               <tr><td colspan="2"><br /></td></tr>
                          <%-- <tr>
                               <td colspan="1" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:Button ID="btnCloseMovement" runat="server" CssClass="roundbutton1" Text ="CLOSE" Visible="FALSE" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="5" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:20px;"> <h4 style="color: #000000">View Asset Movement</h4> 
  </td> <td>
                               <asp:TextBox ID="txtMovementAssetNo" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                           <tr>
                               <td colspan="5" style="text-align:LEFT">
                                   <asp:Label ID="lblMovement" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="20px" ForeColor="Red" Text=""></asp:Label>
                               </td>
                           </tr>--%>
                          <tr>
                              <td colspan="2">
                                    <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdMovement" runat="server" DataSourceID="SqlDSMovement" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
              
                
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="MovementType" HeaderText="MovementType" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="7%" HorizontalAlign="left" />
                </asp:BoundField>

                <asp:BoundField DataField="MovementDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="7%" HorizontalAlign="left" />
                </asp:BoundField>
               
                   <asp:BoundField DataField="ReturnDate" HeaderText="ReturnDate" DataFormatString="{0:dd/MM/yyyy}" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="7%" HorizontalAlign="left"/>
                </asp:BoundField>
                 <asp:BoundField DataField="RecipientType" HeaderText="Type" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="7%" HorizontalAlign="left"/>
                </asp:BoundField>
                                
                 <asp:BoundField DataField="Location" HeaderText="Location" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="7%" HorizontalAlign="left"/>
                </asp:BoundField>

                
                 <asp:BoundField DataField="Incharge" HeaderText="InCharge" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="7%" HorizontalAlign="left"/>
                </asp:BoundField>
                
                 <asp:BoundField DataField="Notes" HeaderText="Notes" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="25%" HorizontalAlign="left"/>
                </asp:BoundField>
              <%--    <asp:BoundField DataField="FileName" HeaderText="File Name" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="25%" HorizontalAlign="left"/>
                </asp:BoundField>
                 <asp:TemplateField HeaderText="Preview"><ItemTemplate><asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FileNameLink") %>' OnClick="PreviewFile" Text="Preview  " /></ItemTemplate></asp:TemplateField>
         <asp:TemplateField HeaderText ="Download"><ItemTemplate><asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DownloadFile" Text="Download"></asp:LinkButton></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Email"><ItemTemplate><a href="Email.aspx?Type=FileUpload" target="_blank"><asp:LinkButton ID="lnkEmail" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="EmailFile" OnClientClick="openInNewTab();" Text="Email"></asp:LinkButton></a></ItemTemplate></asp:TemplateField>
               <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DeleteFile" Text="Delete" /></ItemTemplate></asp:TemplateField>--%>

           <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="9%" HorizontalAlign="left"/>
                </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="9%" HorizontalAlign="left"/>
                </asp:BoundField>
                           
            </Columns>
              <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" />
            <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#00ccff" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView><br />
            

                   
              </div>
                              </td>
                          </tr>
                    <tr>
                        <td colspan="2">
                              <ajaxtoolkit:CollapsiblePanelExtender ID="cpnl2" runat="server" CollapseControlID="pnlAddFilesName" CollapsedImage="~/Images/plus.png" CollapsedText="Click to show" Enabled="True" ExpandControlID="pnlAddFilesName" ExpandedImage="~/Images/minus.png" ImageControlID="imgCollapsible2" TargetControlID="pnlAddFiles" Collapsed="True">
                                                        </ajaxtoolkit:CollapsiblePanelExtender>
                              <asp:Panel ID="pnlAddFilesName" runat="server">
                                                            <table class="Centered" style="padding-top:5px;width:60%">
                                                                <tr>
                                                                    <td>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%; background-color: #C0C0C0;">
                                                                        <asp:ImageButton ID="imgCollapsible2" runat="server" ImageAlign="Bottom" ImageUrl="~/Images/plus.png" />
                                                                        Add Files</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                             <asp:Panel ID="pnlAddFiles" runat="server">
                                    <table class="Centered" style="padding-top:5px;width:80%;padding-left:10%">
                                                        
                                   <tr>
             <td class="CellFormat" style="width:20%">Select File to Upload </td>
             <td colspan="2" class="CellTextBox" style="text-align:center">
                 <asp:FileUpload ID="FileUpload3" runat="server" Width="100%" CssClass="Centered" /></td>
         </tr>
         <tr>
             <td class="CellFormat">Description </td>
             <td colspan="2" class="CellTextBox" style="text-align:left">
                 <asp:TextBox ID="txtFileDescription" runat="server" Width="70%"></asp:TextBox></td>
         </tr>
         <tr>
             <td colspan="2" class="centered"><asp:Button ID="btnAddFileUpload" runat="server" Text="Upload" OnClientClick="currentdatetime()" CssClass="roundbutton1" /></td>

         </tr>
                                        </table>
                                 </asp:Panel>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtFileLink" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>
                            <asp:TextBox ID="txtDeleteUploadedFile" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox>

                              <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
        
                            <asp:GridView ID="gvUpload" runat="server" AutoGenerateColumns="False" CssClass="Centered" DataSourceID="SqlDSUpload" EmptyDataText="No files uploaded" Width="90%">
                                <Columns>
                                    <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                    <asp:BoundField DataField="FileDescription" HeaderText="File Description" />
                                    <asp:BoundField DataField="CreatedOn" DataFormatString="{0:d}" HeaderText="UploadDate" SortExpression="CreatedOn" />
                                    <asp:BoundField DataField="CreatedBy" HeaderText="UploadedBy" SortExpression="CreatedBy" />
                                    <asp:TemplateField><ItemTemplate>
                                        <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="PreviewFile" Text="Preview" /></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate><asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DownloadFile" Text="Download"></asp:LinkButton></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField><ItemTemplate><a href="Email.aspx?Type=FileUpload" target="_blank">
                                        <asp:LinkButton ID="lnkEmail" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="EmailFile" OnClientClick="openInNewTab();" Text="Email"></asp:LinkButton></a>
                                        </ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField><ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DeleteFile" Text="Delete" /></ItemTemplate></asp:TemplateField>

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

                            </asp:GridView>
                                  <asp:SqlDataSource ID="SqlDSUpload" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                                  </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <iframe id="iframeid" runat="server" style="width:80%;height:1000px"></iframe>
                        </td>
                    </tr>
        </table>
            

        
                       </ContentTemplate></ajaxToolkit:TabPanel>
               <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Image Gallery" TabIndex="2">
                   <HeaderTemplate>
                       Image Gallery
                   </HeaderTemplate>
                   <ContentTemplate>
                        <asp:Label ID="lblPhotoMessage" runat="server" BackColor="Red" ForeColor="White" Font-Bold="True" Font-Size="18px"></asp:Label>

                       <table class="centered" style="text-align:center;width:70%;padding-top:10px;padding-left:5%">
  
    <tr><td class="CellFormat" style="width:30%">AssetNo </td><td class="CellTextBox"><asp:Label ID="lblPhotoAssetNo" runat="server" BackColor="#CCCCCC" Height="16px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label></td></tr>
                           <tr><td colspan="2"><br /></td></tr>
  <tr><td class="CellFormat">Select Photo to Upload </td><td class="CellTextBox" colspan="1" style="text-align:center">
      <asp:FileUpload ID="FileUpload2" runat="server" CssClass="Centered" Width="100%" AllowMultiple="True" /></td></tr>
  <%-- <tr><td class="CellFormat">PrimaryPhoto </td><td class="CellTextBox" colspan="1" style="text-align:left">
          <asp:CheckBox ID="chkPrimaryPhoto" runat="server" /></td></tr>--%>
    <tr><td class="centered" colspan="2"><asp:Button ID="btnPhotoUpload" runat="server" Text="Upload" /></td></tr>
                             <tr><td colspan="2"><br /></td></tr>
    <tr><td colspan="2" style="width:100%">
        <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="False" CssClass="Centered" ForeColor="#333333" GridLines="Vertical" OnRowDataBound="OnRowDataBound1" OnSelectedIndexChanged="OnSelectedIndexChangedImg" AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="S.No."><ItemTemplate> <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" /></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="PrimaryPhoto"> 
              <%-- <HeaderTemplate><asp:CheckBox ID="chkSelectAllPhotoPrintGV" runat="server" AutoPostBack="false" TextAlign="left" onchange="checkmultiprint()" Width="5%" OnCheckedChanged="chkServicesPrint_CheckedChanged" ></asp:CheckBox></HeaderTemplate>    <HeaderStyle HorizontalAlign="Left" />--%>
                     <ItemTemplate>
           <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
     </ItemTemplate>
               <ItemTemplate>
                         <asp:CheckBox ID="chkSelectPrimaryPhotoGV" runat="server" Checked='<%# Convert.ToBoolean(Eval("PrimaryPhoto"))%>' Enabled="false"/>
</ItemTemplate>
                                                          </asp:TemplateField> 
                 <%--<asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkUpdatePrimaryPhoto" runat="server" CommandArgument='<%# Eval("Rcno")%>' OnClick="UpdatePrimaryPhotoField" Text="Update Primary photo" /></ItemTemplate></asp:TemplateField>--%>
    
             
            <asp:TemplateField HeaderText="Image" Visible="true"><ItemTemplate><asp:Image ID="ImageView" runat="server" Height="180" Width="280" /></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Preview Image" Visible="true"><ItemTemplate><asp:LinkButton ID="lnkViewImage" runat="server" CommandArgument='<%# Eval("Rcno")%>' OnClick="ViewImage" Text="View Image" />
                  <asp:ImageButton ID="ImageButton1" OnClick="ViewImage" runat="server" Height="200px" Visible="FALSE" Width="200px" /></ItemTemplate></asp:TemplateField>
          
          <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Rcno")%>' OnClick="DeletePhoto" Text="Delete" /></ItemTemplate></asp:TemplateField>

        </Columns><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" Font-Names="Calibri" ForeColor="#8C4510" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><sortedascendingcellstyle backcolor="#E4E4E4" /><sortedascendingheaderstyle backcolor="#000066" /><sorteddescendingcellstyle backcolor="#E4E4E4" /><sorteddescendingheaderstyle backcolor="#000066" /></asp:GridView></td></tr></table><asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataImagesConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataImagesConnectionString.ProviderName %>" SelectCommand=""></asp:SqlDataSource>


                   </ContentTemplate>
               </ajaxToolkit:TabPanel>
                       
               </ajaxtoolkit:TabContainer>

</div>

    

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

               
      <ajaxtoolkit:ModalPopupExtender ID="mdlPopupDeleteUploadedFile" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmDeleteUploadedFile" TargetControlID="btndummyDeleteUploadedFile" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></ajaxtoolkit:ModalPopupExtender>
         <asp:Button ID="btndummyDeleteUploadedFile" runat="server" CssClass="dummybutton" />

                                              

                         <asp:Panel ID="pnlImage" runat="server" BackColor="White" Width="90%" Height="90%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
               <table style="height:100%;width:100%"><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000"></h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnImageClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
               <tr style="height:100%;width:100%"><td> <asp:Image ID="ImageEnlarge" runat="server" width="100%" Height="100%"/> 
</td></tr>   
                   <tr><td><asp:CheckBox ID="chkPrimaryPhoto" runat="server" AutoPostBack="True" CssClass="CellFormat" Text="Primary Photo" /><asp:Label ID="lblImageRcNo" runat="server" Text="" Visible="false"></asp:Label></td></tr>
               </table>
           </asp:Panel>
                 <ajaxtoolkit:ModalPopupExtender ID="mdlPopupImage" runat="server" CancelControlID="btnImageClose" PopupControlID="pnlImage" TargetControlID="btndummyImage" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></ajaxtoolkit:ModalPopupExtender>
                  <asp:Button ID="btndummyImage" runat="server" cssclass="dummybutton" />
    
                  <asp:Panel ID="Panel1" runat="server" BackColor="White" Width="550" Height="350" BorderColor="#003366" BorderWidth="1" Visible="true">
              
                     <table style="width:100%;">
                         <tr><td colspan="3"><br /></td></tr>
            <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Asset No
                             </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchAssetNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
                          <tr><td style="width:10%"></td>
                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Serial No
                               </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchSerialNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td><td style="width:10%"></td>
            </tr>
                            <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">IMEI
                             </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchIMEI" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
                           <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">PO No
                             </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchPONO" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
                          <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Current User
                             </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchCurrentUser" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
                             <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Description
                             </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchDescription" runat="server" MaxLength="100" Height="16px" Width="400px"></asp:TextBox>
                            </td>
            </tr>
                           <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Notes
                             </td>
                           <td class="CellFormat">    <asp:TextBox ID="txtSearchNotes" runat="server" MaxLength="100" Height="16px" Width="400px"></asp:TextBox>
                            </td>
            </tr>
           <%-- <tr><td style="width:10%"></td>
                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Asset RegNo
                               </td>
                           <td>    <asp:TextBox ID="txtSearchAssetRegNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td><td style="width:10%"></td>
            </tr>--%>
          <%--  <tr><td style="width:10%"></td>
                  <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">InCharge ID</td>
                           <td>           
                               <asp:DropDownList ID="ddlSearchInchargeID" runat="server" DataSourceID="SqlDataSource2" DataTextField="StaffId" DataValueField="StaffId" Width="155px" AutoPostBack="false">
                               </asp:DropDownList>
                                   <ajaxToolKit:ListSearchExtender ID="ddllsSearchIncharge" runat="server" TargetControlID="ddlSearchInchargeID" PromptPosition="Bottom"></ajaxToolKit:ListSearchExtender>
    
                            </td><td style="width:10%"></td>
            </tr>--%>
            <tr><td style="width:10%"></td>
                 <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Status</td>
                           <td class="CellFormat">      
                            <asp:DropDownList ID="ddlSearchStatus" CssClass="chzn-select" runat="server" DataSourceID="SqlDSAssetStatus" DataTextField="Status" DataValueField="Available" Width="150px" AppendDataBoundItems="true" Enabled="true">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
                               </td><td style="width:10%"></td>
            </tr>
            <tr><td style="width:10%"></td><td><br /></td><td></td></tr>
            <tr><td colspan="2" style="text-align:center"></td><td><asp:Button ID="btnSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Search" Width="100px"/>
                <asp:Button ID="btnClose" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
            <td style="width:10%"></td></tr>
        </table>
           </asp:Panel>

              
           <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnClose" PopupControlID="Panel1" TargetControlID="btnDummyFilter" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
      <asp:Button ID="btnDummyFilter" runat="server" CssClass="dummybutton" />

     
<%-- </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="DataBound" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                      <%--  <asp:TextBox ID="txtGPSLabel" runat="server" MaxLength="25" Height="16px" Width="150px" Visible="False"></asp:TextBox>
                        <asp:DropDownList ID="txtType" runat="server" Visible="False" Width="150px">
                            <asp:ListItem Selected="True">Vehicle</asp:ListItem>
                        </asp:DropDownList>--%>
   
      
           <%-- start: checkout --%>

            <asp:Panel ID="pnlcheckout" runat="server" BackColor="White" Width="70%" Height="95%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
           <%--   <asp:updatepanel id="UpdateCheckOut" runat="server">
                  <contenttemplate>--%>

                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;"><asp:Label ID="lblStatusHeading" runat="server"></asp:Label></h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageCheckout" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertCheckout" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                           <tr>
               <td colspan="2" style="width:100%;text-align:left;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblLastAssetDetails" runat="server"></asp:Label>
                      </td> 
            </tr>  
                         <tr>
                             <td class="CellFormat" style="width:20%">Asset No</td>
                             <td class="CellTextBox" style="width:80%">
                                 <asp:TextBox ID="txtAssetCheckOut" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="60%" ReadOnly="TRUE"></asp:TextBox>
                        </td>
                         </tr>
                          <tr>
                              <td class="CellFormat">Description</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtDescCheckOut" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="60%" ReadOnly="TRUE"></asp:TextBox>
                              </td>
                         </tr>
                     
                         <tr>
                             <td class="CellFormat">&nbsp;</td>
                             <td class="CellTextBox">&nbsp;</td>
                         </tr>


            <tr>
            <td class="CellFormat"> <asp:Label ID="lblCheckType" runat="server"></asp:Label><asp:Label ID="Label28" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
           </td>
        
        <td class="CellTextBox"><asp:DropDownList ID="ddlCheckOutType" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDSOUTMovementType" DataTextField="MovementType" DataValueField="MovementTo" Width="60%" Height="25px" AutoPostBack="True" ClientIDMode="Predictable">
            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
          </asp:DropDownList><asp:DropDownList ID="ddlCheckInType" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDSINMovementType" DataTextField="MovementType" DataValueField="MovementTo" Width="60%" Height="25px" AutoPostBack="True" ClientIDMode="Predictable">
            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
          </asp:DropDownList>
        </td>

                         </tr>
                         <tr runat="server" id="CheckOutLocationRow">
            <td class="CellFormat"><asp:Label ID="lblCheckTo" runat="server"></asp:Label><asp:Label ID="Label3" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
           </td>
        
        <td class="CellTextBox">
            <asp:DropDownList ID="ddlCheckOutLocation" runat="server" AppendDataBoundItems="True" Visible="false" CssClass="chzn-select" Height="25px" Width="60%">
                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList>
        </td>

                         </tr>
                           <tr>
            <td class="CellFormat">Incharge<asp:Label ID="Label4" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
           </td>
        
        <td class="CellTextBox"><asp:DropDownList ID="ddlCheckOutStaff" runat="server" Visible="false" AppendDataBoundItems="True" DataSourceID="SqlDSStaff" DataTextField="IDNAME" DataValueField="StaffId" Width="60%" AutoPostBack="True">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                                      </asp:DropDownList>
        </td>

                         </tr>

                            <tr>
            <td class="CellFormat"><asp:Label ID="lblCheckDate" runat="server"></asp:Label></td>        
        <td class="CellTextBox"><asp:TextBox ID="txtCheckOutDate" runat="server" Height="16px" MaxLength="25" Width="60%"></asp:TextBox><ajaxtoolkit:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtCheckOutDate" TargetControlID="txtCheckOutDate"></ajaxtoolkit:CalendarExtender>
        </td>

                         </tr>
                         <tr id="expchkin" runat="server">
            <td class="CellFormat">ExpectedCheckInDate</td>        
        <td class="CellTextBox"><asp:TextBox ID="txtExpCheckInDate" runat="server" Height="16px" MaxLength="25" Width="60%"></asp:TextBox><ajaxtoolkit:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtExpCheckInDate" TargetControlID="txtExpCheckInDate"></ajaxtoolkit:CalendarExtender>
        </td>

                         </tr>
                          <tr>
            <td class="CellFormat">Notes</td>        
        <td class="CellTextBox"><asp:TextBox ID="txtCheckOutNotes" runat="server" Height="40px" MaxLength="25" Width="60%" TextMode="MultiLine"></asp:TextBox>
        </td>  

                          </tr>
                                <tr>
                                    <td class="CellFormat">Select File to Upload </td>
                                    <td class="CellTextBox" colspan="1" style="text-align:center">
                                    <%--      <div class="FakeFileUpload">
        <div class="FakeFileUploadDiv">
            <input type="text" style="width: 270px" />
            <asp:Button ID="Button1" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Select file" Width="100px" Height="30px"/>
        </div>--%>
                                        <%--<asp:FileUpload ID="FileUpload1" runat="server" CssClass="Centered" Width="100%" />--%>
                                        <ajaxToolkit:AsyncFileUpload OnClientUploadError="uploadError" OnClientUploadComplete="uploadComplete" runat="server" ID="AsyncFileUpload1" Width="400px" UploaderStyle="Traditional" CompleteBackColor="White"
    UploadingBackColor="#CCFFFF" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" CssClass="FileUploadClass" Visible="false" />
                                        <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" MaximumNumberOfFiles="5" AllowedFileTypes="doc,docx,xls,xlsx,gif,png,pdf,jpg,jpeg,bmp,csv,ppt,pptx,txt" Mode="Auto" Font-Names="Calibri" />
                                                <%--</div>--%>
                                 <%--      <asp:Button ID="btnUpload" Text="Save" runat="server" OnClientClick="savefile()" OnClick="SaveUpload" />
     <input id="Submit1" type="submit" value="Custom Upload Button" runat="server" onserverclick="SaveUpload"
        onclick="$('.ajax__fileupload_uploadbutton').trigger('click');" />
                                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
                <asp:Button Id="Btn" runat="server" Text="Ok" />
                <asp:HiddenField Id="Hidden" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel> --%>
                                            <asp:Label ID="lblChkOutFile" runat="server" Text="Label" Visible="false"></asp:Label>
                                           <asp:LinkButton ID="btnRefreshFilesGrid" runat="server" Font-Bold="True" Text="View Uploaded Files" Font-Names="Calibri" />
                     
                                    </td>
                                </tr>
                          <tr>
                              <td class="CellFormat">Files Uploaded  &nbsp;&nbsp;
                                  <%--<asp:ImageButton ID="btnRefreshGrid" runat="server" ImageUrl="~/Images/reset1.png" Height="20px" Width="20px" ToolTip="REFRESH"  />--%> 
                                   </td>
                              <td class="CellTextBox">
                                                      
			 <asp:GridView ID="gvStatusFileList" runat="server" AutoGenerateColumns="False" CssClass="Centered" DataSourceID="SqlDStbwFileUpload" Width="100%">
                                       <Columns>
                                       <%--     <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="PreviewFile1" Text="Preview" /></ItemTemplate></asp:TemplateField>
                   <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DeleteFile" Text="Delete" /></ItemTemplate></asp:TemplateField>--%>

                                              <asp:TemplateField HeaderText="S.No."><ItemTemplate> <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" /></ItemTemplate>
                                                  <ItemStyle Width="40px" />
                                              </asp:TemplateField>
         
                                           <asp:BoundField DataField="FileName" HeaderText="File Name" >
                                              <ItemStyle Width="200px" />
                                              </asp:BoundField>
                                            <asp:TemplateField HeaderText="FileDescription">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgrdFileDesc" runat="server"
                            Text= '<%# Bind("FileDescription") %>' BorderStyle="None" TextMode="MultiLine" width="100%"></asp:TextBox>
                    </ItemTemplate>
                    </asp:TemplateField>

                                              <asp:TemplateField HeaderText="RcNo" Visible="false">
                                                  <EditItemTemplate>
                                                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno")%>'></asp:TextBox>
                                                  </EditItemTemplate>
                                                  <ItemTemplate>
                                                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno")%>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:TemplateField>

                                       </Columns><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" Font-Names="Calibri" ForeColor="White" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" Font-Names="Calibri" ForeColor="#8C4510" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><sortedascendingcellstyle backcolor="#E4E4E4" /><sortedascendingheaderstyle backcolor="#000066" /><sorteddescendingcellstyle backcolor="#E4E4E4" /><sorteddescendingheaderstyle backcolor="#000066" /></asp:GridView>
                                  <asp:SqlDataSource ID="SqlDStbwFileUpload" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                             
                            
			<%--<asp:button id="btnHidddenSubmit" runat="server" onclick="ReloadGrid" style="display: none" />--%>
		
                                      </td>
                          </tr>
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center">
                                 <asp:Button ID="btnSaveCheckOut" runat="server" OnClientClick="return validateUploaded()" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="CheckOut" Width="120px"/>
                                 <asp:Button ID="btnCancelCheckOut" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
                      
                 <%-- </contenttemplate>
                  <Triggers>
                      <asp:PostBackTrigger ControlID="btnSaveCheckOut" />
                        <asp:PostBackTrigger ControlID="btnConfirmYes" />
                  </Triggers>
              </asp:updatepanel>--%>
           </asp:Panel>

                      <ajaxtoolkit:ModalPopupExtender ID="mdlPopupCheckOut" runat="server" CancelControlID="btnCancelCheckOut" PopupControlID="pnlCheckOut" TargetControlID="btnDummyCheckOut" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></ajaxtoolkit:ModalPopupExtender>
    <asp:Button ID="btnDummyCheckOut" runat="server" CssClass="dummybutton" />

           <%-- end: checkout --%>

        <%-- start: checkin --%>

            <asp:Panel ID="pnlCheckIn" runat="server" BackColor="White" Width="65%" Height="72%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">ASSET CHECK-IN</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageCheckIn" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertCheckIn" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat" style="width:30%">Asset No</td>
                             <td class="CellTextBox" style="width:70%">
                                 <asp:TextBox ID="txtCheckInAssetNo" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="60%" ReadOnly="TRUE"></asp:TextBox>
                        </td>
                         </tr>
                          <tr>
                              <td class="CellFormat">Description</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtCheckInDesc" runat="server" BackColor="#E0E0E0" Height="16px" MaxLength="25" Width="60%" ReadOnly="TRUE"></asp:TextBox>
                              </td>
                         </tr>
                     
                         <tr>
                             <td class="CellFormat">&nbsp;</td>
                             <td class="CellTextBox">&nbsp;</td>
                         </tr>


            <tr>
            <td class="CellFormat">CheckOutType<asp:Label ID="Label7" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
           </td>
        
        <td class="CellTextBox">
        </td>

                         </tr>
                         <tr runat="server" id="CheckInLocationRow">
            <td class="CellFormat">CheckOutTo<asp:Label ID="Label8" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
           </td>
        
        <td class="CellTextBox">
            <asp:DropDownList ID="ddlCheckInLocation" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" Width="60%">
                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem></asp:DropDownList>
        </td>

                         </tr>
                           <tr>
            <td class="CellFormat">Incharge<asp:Label ID="Label9" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
           </td>
        
        <td class="CellTextBox"><asp:DropDownList ID="ddlCheckInStaff" runat="server" Visible="false" AppendDataBoundItems="True" DataSourceID="SqlDSStaff" DataTextField="IDNAME" DataValueField="StaffId" Width="60%" AutoPostBack="True">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                                      </asp:DropDownList>
        </td>

                         </tr>

                            <tr>
            <td class="CellFormat">CheckInDate</td>        
        <td class="CellTextBox"><asp:TextBox ID="txtCheckInDate" runat="server" Height="16px" MaxLength="25" Width="60%"></asp:TextBox>
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender10" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtCheckInDate" TargetControlID="txtCheckInDate"></ajaxtoolkit:CalendarExtender>
        </td>

                         </tr>
                       
                          <tr>
            <td class="CellFormat">Notes</td>        
        <td class="CellTextBox"><asp:TextBox ID="txtCheckInNotes" runat="server" Height="40px" MaxLength="25" Width="60%" TextMode="MultiLine"></asp:TextBox>
        </td>

                         </tr>
         
                          
                             <tr>
                                    <td class="CellFormat">Select File to Upload </td>
                                    <td class="CellTextBox" colspan="1" style="text-align:center">
                                        <%--<asp:FileUpload ID="FileUpload1" runat="server" CssClass="Centered" Width="100%" />--%>
                                        <ajaxToolkit:AsyncFileUpload OnClientUploadComplete="uploadComplete2" runat="server" ID="AsyncFileUpload2" Width="400px" UploaderStyle="Traditional" CompleteBackColor="White"
    UploadingBackColor="#CCFFFF" OnUploadedComplete="AsyncFileUpload2_UploadedComplete" Visible="false" />
                                           <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload2" runat="server" MaximumNumberOfFiles="5" AllowedFileTypes="doc,docx,xls,xlsx,gif,png,pdf,jpg,jpeg,bmp,csv,ppt,pptx,txt" />
                                  
                                          <asp:Label ID="lblChkInFile" runat="server" Text="Label" Visible="false"></asp:Label>
                                    </td>
                                </tr>


                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSaveCheckIn" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"   Font-Bold="True" Text="Checkin" Width="120px"/>
                                 <asp:Button ID="btnCancelCheckIn" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

                      <ajaxtoolkit:ModalPopupExtender ID="mdlPopUpCheckIn" runat="server" CancelControlID="btnCancelCheckIn" PopupControlID="pnlCheckIn" TargetControlID="btnDummyCheckIn" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></ajaxtoolkit:ModalPopupExtender>
    <asp:Button ID="btnDummyCheckIn" runat="server" CssClass="dummybutton" />

           <%-- end: checkIN --%>
        <asp:Panel ID="pnlConfirm" runat="server" BackColor="White" Width="40%" Height="30%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
 
                         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
           
                  <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label45" runat="server" Text="Confirm"></asp:Label>
                          
    
                      </td>
                           </tr>
               <tr>
                             <td><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                          <asp:Label ID="lblStatusMessage" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label>
                         
                      </td>
                           </tr>
                            <tr>
                             <td><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="button" OnClientClick="currentdatetime();" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                          
            <asp:Button ID="btnConfirmNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

           <ajaxtoolkit:ModalPopupExtender ID="mdlPopupConfirm" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirm" TargetControlID="btndummy1" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></ajaxtoolkit:ModalPopupExtender>
    <asp:Button ID="btndummy1" runat="server" CssClass="dummybutton" />
       
                        <%-- Start:View Movement--%>
              
              
              <asp:Panel ID="pnlImportExcel" runat="server" BackColor="White" Width="600px" Height="400px" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
              <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" style="text-align:center;">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Import From Excel</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageImportExcel" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertImportExcel" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                   <tr>
                  <td class="CellFormatPopup"> Sample Templates</td>
                  <td style="text-align:left;padding-left:5%">
                       <asp:ImageButton ID="btnAssetTemplate" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                     <br /><br />
                  </td>
              </tr>
                <tr>
                                                                <td class="CellFormatPopup">Select Excel File to Import Data </td>
                                                                <td class="CellTextBox" colspan="1" style="text-align:center;padding-left:3%">
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="Centered" Width="100%" />
                                                                </td>
                                                            </tr>
              <tr>
                                                                <td colspan="2">
                                                                    <br />
                                                                </td>
                                                            </tr>
             <tr>
                                                                <td class="centered" colspan="2" style="text-align:center;">
                                                                    <asp:Button ID="btnImportExcelUpload" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" OnClientClick="currentdatetime()" Font-Bold="True" width="100px" Text="UPLOAD" />
                                                                         <asp:Button ID="btnCancelImportExcel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" />
                                                 
                                                                </td>
                                                            </tr>
                  <tr>
                      <td colspan="2" style="text-align:center">
                          <br />
                              <asp:GridView ID="GridView2" runat="server" Width="544px" CssClass="Centered" >
                          <Columns>
                             <%--  <asp:BoundField DataField="AccountID" HeaderText="AccountID" SortExpression="AccountID" >
                       <ControlStyle Width="150px" />
                       <HeaderStyle Width="150px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>
                              <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>--%>
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
                      </td>
                  </tr>
                  </table>
          </asp:Panel>

                <ajaxtoolkit:ModalPopupExtender ID="mdlPopupImportExcel" runat="server" CancelControlID="btnCloseImportExcel" PopupControlID="pnlImportExcel" TargetControlID="btnDummyImportExcel" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></ajaxtoolkit:ModalPopupExtender>
           <asp:Button ID="btnDummyImportExcel" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Movement--%>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblasset where rcno<>0 order by assetno"></asp:SqlDataSource>

         <asp:SqlDataSource ID="SqlDSMovement" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblassetmovement where assetno='1' order by MovementDate desc"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDSINMovementType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblassetmovementtype where TYPE='IN' order by MovementType desc"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSOUTMovementType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblassetmovementtype where TYPE='OUT' order by MovementType desc"></asp:SqlDataSource>

        
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId FROM tblstaff ORDER BY STAFFID"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT AssetGroup FROM tblassetgroup ORDER BY AssetGroup"></asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlDSClass" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT AssetClass FROM tblassetClass ORDER BY AssetClass"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSBrand" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT AssetBrand FROM tblassetBrand ORDER BY AssetBrand"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSModel" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSColor" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT AssetColor FROM tblassetColor ORDER BY AssetColor"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDSSupplier" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(SupplierID, ' - ', Name) AS Supplier FROM tblSupplier ORDER BY Supplier"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSAssetStatus" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Status,Available FROM tblassetstatus ORDER BY Status"></asp:SqlDataSource>
   <asp:SqlDataSource ID="SqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId, Name, CONCAT(StaffId, ' [', Name, ']') AS IDNAME FROM tblstaff ORDER BY StaffId"></asp:SqlDataSource>
           <asp:TextBox ID="txtGoogleEmail" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="False"></asp:TextBox>

       <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>

        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtWorkBookName" runat="server" CssClass="dummybutton"></asp:TextBox>
    <asp:TextBox ID="txtFailureCount" runat="server" CssClass="dummybutton"></asp:TextBox>
<asp:TextBox ID="txtSuccessCount" runat="server" CssClass="dummybutton"></asp:TextBox>
<asp:TextBox ID="txtFailureString" runat="server" CssClass="dummybutton"></asp:TextBox>

                    <asp:TextBox ID="txtActive" runat="server" Visible="False"></asp:TextBox>

                   <asp:TextBox ID="txtAssetGroup" runat="server" MaxLength="25" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                             <asp:TextBox ID="txtAssetCode" runat="server" MaxLength="30" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                           <asp:TextBox ID="txtAssetMake" runat="server" MaxLength="30" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                           <asp:TextBox ID="txtModel" runat="server" MaxLength="30" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                             <asp:TextBox ID="txtMfgYear" runat="server" MaxLength="10" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                           <asp:TextBox ID="txtCapacity" runat="server" MaxLength="20" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                             <asp:TextBox ID="txtCapacityUnit" runat="server" MaxLength="25" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtRegDate" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                               <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRegDate" PopupButtonID="txtRegDate" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
               
                            <asp:TextBox ID="txtLastSvc" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLastSvc" PopupButtonID="txtLastSvc" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                      
                             <asp:TextBox ID="txtNextSvc" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtNextSvc" PopupButtonID="txtNextSvc" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                 
                               <asp:TextBox ID="txtPriceCode" runat="server" MaxLength="30" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                      
                             <asp:TextBox ID="txtOurRef" runat="server" MaxLength="40" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                  
                            <asp:TextBox ID="txtDateOut" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDateOut" PopupButtonID="txtDateOut" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender> 
                            <asp:TextBox ID="txtEstDateIn" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtEstDateIn" PopupButtonID="txtEstDateIn" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                           <asp:TextBox ID="txtRefDate" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtRefDate" PopupButtonID="txtRefDate" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                     
                          <asp:TextBox ID="txtReference" runat="server" MaxLength="25" Height="16px" Width="150px" Visible="false"></asp:TextBox>
                          <asp:TextBox ID="txtTechSpecs" runat="server" MaxLength="2000" Height="60px" Width="150px" TextMode="MultiLine" Visible="false"></asp:TextBox>
                           <asp:TextBox ID="txtRemarks" runat="server" MaxLength="4000" Height="60px" Width="150px" TextMode="MultiLine" Visible="false"></asp:TextBox>
                         <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>   
<%-- </ContentTemplate>
                 <Triggers>
       
               <asp:PostBackTrigger ControlID="btnSaveCheckOut" />
                      <asp:PostBackTrigger ControlID="btnConfirmYes" />
                      <asp:PostBackTrigger ControlID="ddlCheckOutStaff" />
                     </Triggers>
                 </asp:UpdatePanel>--%>
</asp:Content>

