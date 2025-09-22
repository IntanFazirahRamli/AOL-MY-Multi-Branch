<%@ Page Title="Vehicle Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_Vehicle.aspx.vb" Inherits="Master_Vehicle"  Culture="en-GB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <script type="text/javascript">


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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
.modalBackground
{
background-color: gray;
filter: alpha(opacity=80);
opacity: 0.8;
z-index: 10000;
}
        </style>
     <style type="text/css">
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
             width: 24%;
         }
  
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
           <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="False" LoadScriptsBeforeUI="False">
                 <ControlBundles>
        <ajaxToolkit:ControlBundle Name="CalendarExtender_Bundle" />
                     <ajaxToolkit:controlBundle name="ModalPopupExtender_Bundle"/>
                                 <ajaxToolkit:controlBundle name="ListSearchExtender_Bundle"/>

   </ControlBundles>
           </ajaxToolkit:ToolkitScriptManager> 
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Vehicle</h3>
   <table  border="0" style="width:100%;text-align:center;">
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
            
            <tr>
              
                <td style="text-align:left;width:50%">
                  
                     <asp:Button ID="btnAdd" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>
                <asp:Button ID="btnFilter" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="100px" />
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" />
            
                       </td>
                <td style="text-align: right">
            
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />

                </td>
                
            </tr>
                  <tr>
              <td colspan="2" style="text-align:right;">
               <asp:ImageButton ID="btnResetSearch" runat="server" ImageUrl="~/Images/reset.png" Height="40px" Width="50px" ToolTip="RESET SEARCH" /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">

                      <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="14px" HorizontalAlign="Center" AllowSorting="True" Width="100%" AllowPaging="True" CssClass="Centered">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
                <asp:BoundField DataField="AssetNo" HeaderText="Asset No" SortExpression="AssetNo" ItemStyle-Width="90px">
                 <ItemStyle Width="90px" Wrap="False" />
                </asp:BoundField>
               <asp:BoundField DataField="AssetRegNo" HeaderText="Asset RegNo" SortExpression="AssetRegNo" />
                <asp:BoundField DataField="AssetClass" HeaderText="Asset Class" SortExpression="AssetClass" />  
                  <asp:BoundField DataField="InChargeID" HeaderText="InChargeID" SortExpression="InChargeID" />
                            
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                 <asp:BoundField DataField="OpStatus" HeaderText="OpStatus" SortExpression="OpStatus" />

                 <asp:BoundField DataField="Descrip" HeaderText="Description" SortExpression="Descrip" ItemStyle-Width="250px">
               
                 <ControlStyle Width="250px" />
               
                 <HeaderStyle Width="250px" />
                 <ItemStyle Width="250px" />
                 </asp:BoundField>
               
                 <asp:BoundField DataField="Location">
                 <ControlStyle CssClass="dummybutton" />
                 <HeaderStyle CssClass="dummybutton" />
                 <ItemStyle CssClass="dummybutton" />
                 </asp:BoundField>
               
                <asp:BoundField DataField="AssetGrp" HeaderText="AssetGrp" SortExpression="AssetGrp" Visible="false" />
                 
                <asp:BoundField DataField="AssetCode" HeaderText="AssetCode" SortExpression="AssetCode" Visible="false" />
                 <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" Visible="false" />
                 <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" Visible="false" />
                 <asp:BoundField DataField="MfgYear" HeaderText="MfgYear" SortExpression="MfgYear" Visible="false" />

                 <asp:BoundField DataField="Capacity" HeaderText="Capacity" SortExpression="Capacity" Visible="false" />
                <asp:BoundField DataField="CapacityUnitMS" HeaderText="CapacityUnitMS" SortExpression="CapacityUnitMS" Visible="false" />
                <asp:BoundField DataField="RegDate" HeaderText="RegDate" SortExpression="RegDate" DataFormatString="{0:dd/MM/yyyy}" Visible="false" />
                <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" Visible="false" />
                 
                 <asp:BoundField DataField="NextSvcDate" HeaderText="NextSvcDate" SortExpression="NextSvcDate" DataFormatString="{0:dd/MM/yyyy}" Visible="false"/>
                 <asp:BoundField DataField="LastSvcDate" HeaderText="LastSvcDate" SortExpression="LastSvcDate" DataFormatString="{0:dd/MM/yyyy}" Visible="false"/>
                 <asp:BoundField DataField="PriceCode" HeaderText="PriceCode" SortExpression="PriceCode" Visible="false"/>
                 <asp:BoundField DataField="ReferenceOur" HeaderText="ReferenceOur" SortExpression="ReferenceOur" Visible="false"/>
               
                <asp:BoundField DataField="EstDateIn" HeaderText="EstDateIn" SortExpression="EstDateIn" DataFormatString="{0:dd/MM/yyyy}" Visible="false"/>
                <asp:BoundField DataField="DateOut" HeaderText="DateOut" SortExpression="DateOut" DataFormatString="{0:dd/MM/yyyy}" Visible="false"/>
                  <asp:BoundField DataField="RefDate" HeaderText="RefDate" SortExpression="RefDate" DataFormatString="{0:dd/MM/yyyy}" Visible="false"/>
                <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" Visible="false"/>
                
                <asp:BoundField DataField="GoogleEmail" HeaderText="GoogleEmail" SortExpression="GoogleEmail" Visible="False" />
                 <asp:BoundField DataField="GPSLabel" HeaderText="GPSLabel" SortExpression="GPSLabel" Visible="False" />
                  <asp:BoundField DataField="TechnicalSpecs" HeaderText="TechnicalSpecs" SortExpression="TechnicalSpecs" Visible="false"/>
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" Visible="false"/>
                
                 
                 <asp:BoundField DataField="DefAddr" HeaderText="DefAddr" SortExpression="DefAddr" Visible="False" />
                 <asp:BoundField DataField="Depmtd" HeaderText="Depmtd" SortExpression="Depmtd" Visible="False" />

                 <asp:BoundField DataField="PurfmCType" HeaderText="PurfmCType" SortExpression="PurfmCType" Visible="False" />
                 <asp:BoundField DataField="PurfmCCode" HeaderText="PurfmCCode" SortExpression="PurfmCCode" Visible="False" />
                 <asp:BoundField DataField="PurfmInvNo" HeaderText="PurfmInvNo" SortExpression="PurfmInvNo" Visible="False" />
                 <asp:BoundField DataField="PurchRef" HeaderText="PurchRef" SortExpression="PurchRef" Visible="False" />
                 <asp:BoundField DataField="PurchDt" HeaderText="PurchDt" SortExpression="PurchDt" Visible="False" />
                 <asp:BoundField DataField="PurchVal" HeaderText="PurchVal" SortExpression="PurchVal" Visible="False" />
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
                 <asp:BoundField DataField="SupplierCode" HeaderText="SupplierCode" SortExpression="SupplierCode" Visible="False" />
                 <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" Visible="False" />
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
                   <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="true" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" />
              
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
    
                    </td>
                </tr>
          <tr>
              <td colspan="2"><br /></td>
          </tr>
            <tr>                   

                    <td class="auto-style3">AssetNo<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                    
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtAssetNo" runat="server" MaxLength="30" Height="16px" Width="40%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                  
                    <td class="auto-style3">AssetRegNo</td>
                   
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtAssetRegNo" runat="server" MaxLength="30" Height="16px" Width="40%"></asp:TextBox></td>
                   
                 </tr>
         <tr>
                  
                    <td class="auto-style3">AssetClass</td>
                   
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtAssetClass" runat="server" MaxLength="30" Height="16px" Width="40%"></asp:TextBox></td>
                   
                 </tr>
         <tr>
                  
                    <td class="auto-style3">InchargeID</td>
                   
                    <td class="CellTextBoxADM"> <asp:DropDownList ID="ddlInchargeID" CssClass="chzn-select" runat="server" DataSourceID="SqlDataSource2" DataTextField="StaffId" DataValueField="StaffId" Width="40.5%" AppendDataBoundItems="true">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
                                    <ajaxToolKit:ListSearchExtender ID="ddllsInchargeID" runat="server" TargetControlID="ddlInchargeID" PromptPosition="Bottom"></ajaxToolKit:ListSearchExtender>
    </td>
                   
                 </tr>
         <tr>
                  
                    <td class="auto-style3">Status</td>
                   
                    <td class="CellTextBoxADM"> <asp:DropDownList ID="txtStatus" runat="server" Width="40.5%" CssClass="chzn-select" >
                                   <asp:ListItem Selected="True" Value="O">O - Operational</asp:ListItem>
                                   <asp:ListItem Value="H">H - Hire</asp:ListItem>
                                   <asp:ListItem Value="S">S - Sold</asp:ListItem>
                                   <asp:ListItem Value="X">X - Scrap</asp:ListItem>
                                   <asp:ListItem Value="L">L - Loan</asp:ListItem>
                                   <asp:ListItem Value="F">F - Faulty</asp:ListItem>
                                   <asp:ListItem Value="M">M - Missing</asp:ListItem>
                                   <asp:ListItem Value="N">N - New</asp:ListItem>
                               </asp:DropDownList></td>
                   
                 </tr>
         <tr>
                  
                    <td class="auto-style3">OP Status</td>
                   
                    <td class="CellTextBoxADM"><asp:TextBox ID="txtOPStatus" runat="server" MaxLength="1" Height="16px" Width="40%" Text="O" ReadOnly="True"></asp:TextBox>
                           </td>
                   
                 </tr>
          <tr>
                  
                    <td class="auto-style3">Description</td>
                   
                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtDescription" runat="server" Width="40%" MaxLength="120"></asp:TextBox>
                           </td>
                   
                 </tr>
         <tr >
                              <td class="CellFormatADM">Location</td>
                              <td class="CellTextBoxADM">
                                  <asp:DropDownList ID="txtLocationId" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select" Width="40%">
                                      <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                         </tr>
           <tr>
               <td colspan="2" style="text-align:right">  
                     <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

               </td>
               <td>
                   <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" CausesValidation="False" />
               </td>
           </tr>
          </table> 
       

           <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnClose" PopupControlID="Panel1" TargetControlID="btnFilter" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
   
     
</div>
                       
                  <asp:Panel ID="Panel1" runat="server" BackColor="White" Width="300" Height="300" BorderColor="#003366" BorderWidth="1" Visible="true">
              
                     <table style="width:100%;">
                         <tr><td colspan="3"><br /></td></tr>
            <tr><td style="width:10%"></td>
               <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Asset No
                             </td>
                           <td>    <asp:TextBox ID="txtSearchAssetNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td>
            </tr>
            <tr><td style="width:10%"></td>
                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Asset RegNo
                               </td>
                           <td>    <asp:TextBox ID="txtSearchAssetRegNo" runat="server" MaxLength="30" Height="16px" Width="150px"></asp:TextBox>
                            </td><td style="width:10%"></td>
            </tr>
            <tr><td style="width:10%"></td>
                  <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">InCharge ID</td>
                           <td>           
                               <asp:DropDownList ID="ddlSearchInchargeID" runat="server" Width="155px" AutoPostBack="false" AppendDataBoundItems="True" >
                                   <asp:ListItem Text="--SELECT--"  />
                               </asp:DropDownList>
                                   <ajaxToolKit:ListSearchExtender ID="ddllsSearchIncharge" runat="server" TargetControlID="ddlSearchInchargeID" PromptPosition="Bottom"></ajaxToolKit:ListSearchExtender>
    
                            </td><td style="width:10%"></td>
            </tr>
            <tr><td style="width:10%"></td>
                 <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Status</td>
                           <td>      
                               <asp:DropDownList ID="ddlSearchStatus" runat="server" Width="155px">
                                   <asp:ListItem Selected="True" Value="O">O - Operational</asp:ListItem>
                                   <asp:ListItem Value="H">H - Hire</asp:ListItem>
                                   <asp:ListItem Value="S">S - Sold</asp:ListItem>
                                   <asp:ListItem Value="X">X - Scrap</asp:ListItem>
                                   <asp:ListItem Value="L">L - Loan</asp:ListItem>
                                   <asp:ListItem Value="F">F - Faulty</asp:ListItem>
                                   <asp:ListItem Value="M">M - Missing</asp:ListItem>
                                   <asp:ListItem Value="N">N - New</asp:ListItem>
                               </asp:DropDownList>
                               </td><td style="width:10%"></td>
            </tr>
            <tr><td style="width:10%"></td><td><br /></td><td></td></tr>
            <tr><td style="width:10%"></td><td><asp:Button ID="btnSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Search" Width="100px"/></td>
                <td><asp:Button ID="btnClose" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" /></td>
            <td style="width:10%"></td></tr>
        </table>
           </asp:Panel>
<%-- </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="DataBound" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                        <asp:TextBox ID="txtGPSLabel" runat="server" MaxLength="25" Height="16px" Width="150px" Visible="False"></asp:TextBox>
                        <asp:DropDownList ID="txtType" runat="server" Visible="False" Width="150px">
                            <asp:ListItem Selected="True">Vehicle</asp:ListItem>
                        </asp:DropDownList>
    </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblasset where rcno<>0 order by assetno"></asp:SqlDataSource>

       
        
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId FROM tblstaff ORDER BY STAFFID"></asp:SqlDataSource>
           <asp:TextBox ID="txtGoogleEmail" runat="server" MaxLength="50" Height="16px" Width="150px" Visible="False"></asp:TextBox>

       <asp:TextBox ID="txt" runat="server" MaxLength="50" Height="16px" Width="550px" Visible="false"></asp:TextBox>

        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
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
        
    </div>
 
</asp:Content>

